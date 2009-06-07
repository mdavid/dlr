/* ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation. 
 *
 * This source code is subject to terms and conditions of the Microsoft Public License. A 
 * copy of the license can be found in the License.html file at the root of this distribution. If 
 * you cannot locate the  Microsoft Public License, please send an email to 
 * ironruby@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
 * by the terms of the Microsoft Public License.
 *
 * You must not remove this notice, or any other, from this software.
 *
 *
 * ***************************************************************************/

using System; using Microsoft;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Linq.Expressions;
using System.Reflection;
using IronRuby.Builtins;
using IronRuby.Compiler;
using Microsoft.Scripting.Actions.Calls;
using Microsoft.Scripting.Generation;
using Microsoft.Scripting.Utils;
using Ast = Microsoft.Linq.Expressions.Expression;
using AstFactory = IronRuby.Compiler.Ast.AstFactory;
using AstUtils = Microsoft.Scripting.Ast.Utils;

namespace IronRuby.Runtime.Calls {

    public enum SelfCallConvention {
        SelfIsInstance,
        SelfIsParameter,
        NoSelf
    }

    /// <summary>
    /// Performs method binding for calling CLR methods.
    /// Currently this is used for all builtin libary methods and interop calls to CLR methods
    /// </summary>
    public abstract class RubyMethodGroupBase : RubyMemberInfo {
        // Not protected by a lock. Immutable after initialized. 
        private MethodBase/*!*/[] _methodBases;
        
        protected RubyMethodGroupBase(MethodBase/*!*/[] methods, RubyMemberFlags flags, RubyModule/*!*/ declaringModule)
            : base(flags, declaringModule) {
            if (methods != null) {
                SetMethodBasesNoLock(methods);
            }
        }

        protected abstract RubyMemberInfo/*!*/ Copy(MethodBase/*!*/[]/*!*/ methods);

        internal protected virtual MethodBase/*!*/[]/*!*/ MethodBases {
            get { return _methodBases; }
        }

        internal MethodBase/*!*/[]/*!*/ SetMethodBasesNoLock(MethodBase/*!*/[]/*!*/ methods) {
            Debug.Assert(
                CollectionUtils.TrueForAll(methods, (method) => method.IsStatic || method.DeclaringType == typeof(Object)) ||
                CollectionUtils.TrueForAll(methods, (method) => !method.IsStatic || CompilerHelpers.IsExtension(method) || RubyClass.IsOperator(method))
            );

            return _methodBases = methods;
        }

        public override MemberInfo/*!*/[]/*!*/ GetMembers() {
            return ArrayUtils.MakeArray(MethodBases);
        }

        internal abstract SelfCallConvention CallConvention { get; }
        internal abstract bool ImplicitProtocolConversions { get; }

        public override int GetArity() {
            int minParameters = Int32.MaxValue;
            int maxParameters = 0;
            bool hasOptional = false;
            foreach (MethodBase method in MethodBases) {
                int mandatory, optional;
                RubyOverloadResolver.GetParameterCount(method, method.GetParameters(), CallConvention, out mandatory, out optional);
                if (mandatory < minParameters) {
                    minParameters = mandatory;
                }
                if (mandatory > maxParameters) {
                    maxParameters = mandatory;
                }
                if (!hasOptional && optional > 0) {
                    hasOptional = true;
                }
            }
            if (hasOptional || maxParameters > minParameters) {
                return -minParameters - 1;
            } else {
                return minParameters;
            }
        }

        #region Generic Parameters, Overloads Selection

        public override RubyMemberInfo TryBindGenericParameters(Type/*!*/[]/*!*/ typeArguments) {
            var boundMethods = new List<MethodBase>();
            foreach (var method in MethodBases) {
                if (method.IsGenericMethodDefinition) {
                    if (typeArguments.Length == method.GetGenericArguments().Length) {
                        Debug.Assert(!(method is ConstructorInfo));
                        boundMethods.Add(((MethodInfo)method).MakeGenericMethod(typeArguments));
                    }
                } else if (typeArguments.Length == 0) {
                    boundMethods.Add(method);
                }
            }

            if (boundMethods.Count == 0) {
                return null;
            }

            return Copy(boundMethods.ToArray());
        }

        /// <summary>
        /// Filters out methods that don't exactly match parameter types except for hidden parameters (RubyContext, RubyScope, site local storage).
        /// </summary>
        public override RubyMemberInfo TrySelectOverload(Type/*!*/[]/*!*/ parameterTypes) {
            var boundMethods = new List<MethodBase>();
            foreach (var method in MethodBases) {
                if (IsOverloadSignature(method, parameterTypes)) {
                    boundMethods.Add(method);
                }
            }

            if (boundMethods.Count == 0) {
                return null;
            }

            return Copy(boundMethods.ToArray());
        }

        private bool IsOverloadSignature(MethodBase/*!*/ method, Type/*!*/[]/*!*/ parameterTypes) {
            var infos = method.GetParameters();
            int firstInfo = RubyOverloadResolver.GetHiddenParameterCount(method, infos, CallConvention);
            
            if (infos.Length - firstInfo != parameterTypes.Length) {
                return false;
            }

            for (int i = 0; i < parameterTypes.Length; i++) {
                if (infos[firstInfo + i].ParameterType != parameterTypes[i]) {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Dynamic Sites

        private static Type/*!*/ GetAssociatedSystemType(RubyModule/*!*/ module) {
            if (module.IsClass) {
                Type type = ((RubyClass)module).GetUnderlyingSystemType();
                if (type != null) {
                    return type;
                }
            }
            return typeof(SuperCallAction);
        }

        protected virtual MethodBase/*!*/[]/*!*/ GetStaticDispatchMethods(Type/*!*/ baseType, string/*!*/ name) {
            return MethodBases;
        }

        internal override void BuildSuperCallNoFlow(MetaObjectBuilder/*!*/ metaBuilder, CallArguments/*!*/ args, string/*!*/ name, RubyModule/*!*/ declaringModule) {
            Assert.NotNull(declaringModule, metaBuilder, args);

            IList<MethodBase> methods;
            if (!declaringModule.IsSingletonClass) {
                Type associatedType = GetAssociatedSystemType(declaringModule);
                methods = GetStaticDispatchMethods(associatedType, name);
            } else {
                methods = MethodBases;
            }

            BuildCallNoFlow(metaBuilder, args, name, methods, CallConvention, ImplicitProtocolConversions);
        }

        internal static BindingTarget/*!*/ ResolveOverload(MetaObjectBuilder/*!*/ metaBuilder, CallArguments/*!*/ args, string/*!*/ name,
            IList<MethodBase>/*!*/ overloads, SelfCallConvention callConvention, bool implicitProtocolConversions, 
            out RubyOverloadResolver/*!*/ resolver) {

            resolver = new RubyOverloadResolver(metaBuilder, args, callConvention, implicitProtocolConversions);
            var bindingTarget = resolver.ResolveOverload(name, overloads, NarrowingLevel.None, NarrowingLevel.All);

            bool calleeHasBlockParam = bindingTarget.Success && HasBlockParameter(bindingTarget.Method);
            
            // At runtime the BlockParam is created with a new RFC instance that identifies the library method frame as 
            // a proc-converter target of a method unwinder triggered by break from a block.
            if (args.Signature.HasBlock) {
                var metaBlock = args.GetMetaBlock();
                if (metaBlock.Value != null && calleeHasBlockParam) {
                    Debug.Assert(metaBuilder.BfcVariable != null);
                    metaBuilder.ControlFlowBuilder = RuleControlFlowBuilder;
                }

                // Overload resolution might not need to distinguish between nil and non-nil block.
                // However, we still do since we construct CF only for non-nil blocks.
                if (metaBlock.Value == null) {
                    metaBuilder.AddRestriction(Ast.Equal(metaBlock.Expression, AstUtils.Constant(null)));
                } else {
                    // don't need to test the exact type of the Proc since the code is subclass agnostic:
                    metaBuilder.AddRestriction(Ast.NotEqual(metaBlock.Expression, AstUtils.Constant(null)));
                }
            }

            // add restrictions used for overload resolution:
            resolver.AddArgumentRestrictions(metaBuilder, bindingTarget);
            
            return bindingTarget;
        }

        /// <summary>
        /// Resolves an library method overload and builds call expression.
        /// The resulting expression on meta-builder doesn't handle block control flow yet.
        /// </summary>
        internal static void BuildCallNoFlow(MetaObjectBuilder/*!*/ metaBuilder, CallArguments/*!*/ args, string/*!*/ name,
            IList<MethodBase>/*!*/ overloads, SelfCallConvention callConvention, bool implicitProtocolConversions) {

            RubyOverloadResolver resolver;
            var bindingTarget = ResolveOverload(metaBuilder, args, name, overloads, callConvention, implicitProtocolConversions, out resolver);
            if (bindingTarget.Success) {
                metaBuilder.Result = bindingTarget.MakeExpression();
            } else {
                metaBuilder.SetError(resolver.MakeInvalidParametersError(bindingTarget).Expression);
            }
        }

        /// <summary>
        /// Takes current result and wraps it into try-filter(MethodUnwinder)-finally block that ensures correct "break" behavior for 
        /// library method calls with block given in bfcVariable (BlockParam).
        /// </summary>
        public static void RuleControlFlowBuilder(MetaObjectBuilder/*!*/ metaBuilder, CallArguments/*!*/ args) {
            if (metaBuilder.Error) {
                return;
            }

            Expression expression = metaBuilder.Result;
            Expression bfcVariable = metaBuilder.BfcVariable;

            // Method call with proc can invoke control flow that returns an arbitrary value from the call, so we need to type result to Object.
            // Otherwise, the result could only be result of targetExpression unless its return type is void.
            Type resultType = (bfcVariable != null) ? typeof(object) : expression.Type;

            Expression resultVariable;
            if (resultType != typeof(void)) {
                resultVariable = metaBuilder.GetTemporary(resultType, "#result");
            } else {
                resultVariable = AstUtils.Empty();
            }

            if (expression.Type != typeof(void)) {
                expression = Ast.Assign(resultVariable, AstUtils.Convert(expression, resultType));
            }

            // a non-null proc is being passed to the callee:
            if (bfcVariable != null) {
                ParameterExpression methodUnwinder = metaBuilder.GetTemporary(typeof(MethodUnwinder), "#unwinder");

                expression = AstFactory.Block(
                    Ast.Assign(bfcVariable, Methods.CreateBfcForLibraryMethod.OpCall(AstUtils.Convert(args.GetBlockExpression(), typeof(Proc)))),
                    AstUtils.Try(
                        expression
                    ).Filter(methodUnwinder, Methods.IsProcConverterTarget.OpCall(bfcVariable, methodUnwinder),
                        Ast.Assign(resultVariable, Ast.Field(methodUnwinder, MethodUnwinder.ReturnValueField)),
                        AstUtils.Default(expression.Type)
                    ).Finally(
                        Methods.LeaveProcConverter.OpCall(bfcVariable)
                    ),
                    resultVariable
                );
            }

            metaBuilder.Result = expression;
        }

        private static bool HasBlockParameter(MethodBase/*!*/ method) {
            foreach (ParameterInfo param in method.GetParameters()) {
                if (param.ParameterType == typeof(BlockParam)) {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}


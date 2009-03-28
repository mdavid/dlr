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
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.Scripting;
using Microsoft.Scripting.Actions;
using Microsoft.Scripting.Actions.Calls;
using Microsoft.Scripting.Generation;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using IronRuby.Builtins;
using IronRuby.Compiler.Generation;
using IronRuby.Compiler;

using AstFactory = IronRuby.Compiler.Ast.AstFactory;
using AstUtils = Microsoft.Scripting.Ast.Utils;
using Ast = Microsoft.Linq.Expressions.Expression;

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
            // either all methods in the group are static or instance, a mixture is not allowed:
            Debug.Assert(
                CollectionUtils.TrueForAll(methods, (method) => method.IsStatic) ||
                CollectionUtils.TrueForAll(methods, (method) => !method.IsStatic)
            );

            return _methodBases = methods;
        }

        public override MemberInfo/*!*/[]/*!*/ GetMembers() {
            return ArrayUtils.MakeArray(MethodBases);
        }

        internal abstract SelfCallConvention CallConvention { get; }

        public override int GetArity() {
            int minParameters = Int32.MaxValue;
            int maxParameters = 0;
            bool hasOptional = false;
            foreach (MethodBase method in MethodBases) {
                int mandatory, optional;
                bool acceptsBlock;
                RubyBinder.GetParameterCount(method.GetParameters(), out mandatory, out optional, out acceptsBlock);
                if (mandatory > 0) {
                    mandatory--; // account for "self"
                }
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

        private static bool IsOverloadSignature(MethodBase/*!*/ method, Type/*!*/[]/*!*/ parameterTypes) {
            var infos = method.GetParameters();
            int firstInfo = 0;
            while (firstInfo < infos.Length && RubyBinder.IsHiddenParameter(infos[firstInfo])) {
                firstInfo++;
            }

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
                RubyClass cls = module as RubyClass;
                Type type = cls.GetUnderlyingSystemType();
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

            BuildCallNoFlow(metaBuilder, args, name, methods, CallConvention);
        }

        internal static BindingTarget/*!*/ ResolveOverload(string/*!*/ name, IList<MethodBase>/*!*/ overloads, CallArguments/*!*/ args, 
            SelfCallConvention callConvention) {

            var methodBinder = MethodBinder.MakeBinder(args.RubyContext.Binder, name, overloads, ArrayUtils.EmptyStrings, NarrowingLevel.None, NarrowingLevel.All);
            var argTypes = GetSignatureToMatch(args, callConvention);
            return methodBinder.MakeBindingTarget(CallTypes.None, argTypes);
        }

        internal override void BuildCallNoFlow(MetaObjectBuilder/*!*/ metaBuilder, CallArguments/*!*/ args, string/*!*/ name) {
            Assert.NotNull(name, metaBuilder, args);

            BuildCallNoFlow(metaBuilder, args, name, MethodBases, CallConvention);
        }

        /// <summary>
        /// Resolves an library method overload and builds call expression.
        /// The resulting expression on meta-builder doesn't handle block control flow yet.
        /// </summary>
        internal static void BuildCallNoFlow(MetaObjectBuilder/*!*/ metaBuilder, CallArguments/*!*/ args, string/*!*/ name, 
            IList<MethodBase>/*!*/ overloads, SelfCallConvention callConvention) {

            var bindingTarget = ResolveOverload(name, overloads, args, callConvention);
            bool calleeHasBlockParam = bindingTarget.Success && HasBlockParameter(bindingTarget.Method);

            // Allocates a variable holding BlockParam. At runtime the BlockParam is created with a new RFC instance that
            // identifies the library method frame as a proc-converter target of a method unwinder triggered by break from a block.
            if (args.Signature.HasBlock) {
                var metaBlock = args.GetMetaBlock();
                if (metaBlock.Value != null && calleeHasBlockParam) {
                    if (metaBuilder.BfcVariable == null) {
                        metaBuilder.BfcVariable = metaBuilder.GetTemporary(typeof(BlockParam), "#bfc");
                    }
                    metaBuilder.ControlFlowBuilder = RuleControlFlowBuilder;
                }

                // Block test - we need to test for a block regardless of whether it is actually passed to the method or not
                // since the information that the block is not null is used for overload resolution.
                if (metaBlock.Value == null) {
                    metaBuilder.AddRestriction(Ast.Equal(metaBlock.Expression, AstUtils.Constant(null)));
                } else {
                    // don't need to test the exact type of the Proc since the code is subclass agnostic:
                    metaBuilder.AddRestriction(Ast.NotEqual(metaBlock.Expression, AstUtils.Constant(null)));
                }
            }

            var actualArgs = MakeActualArgs(metaBuilder, args, callConvention, calleeHasBlockParam, true);

            if (bindingTarget.Success) {
                var parameterBinder = new RubyParameterBinder(args);
                metaBuilder.Result = bindingTarget.MakeExpression(parameterBinder, actualArgs);
            } else {
                metaBuilder.SetError(args.RubyContext.RubyBinder.MakeInvalidParametersError(bindingTarget).Expression);
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

        // Normalizes arguments: inserts self, expands splats, and inserts rhs arg. 
        // Adds any restrictions/conditions applied to the arguments to the given meta-builder.
        public static DynamicMetaObject[]/*!*/ NormalizeArguments(MetaObjectBuilder/*!*/ metaBuilder, CallArguments/*!*/ args,
            SelfCallConvention callConvention, bool calleeHasBlockParam, bool injectMissingBlockParam) {

            var result = new List<DynamicMetaObject>();

            // self (instance):
            if (callConvention == SelfCallConvention.SelfIsInstance) {
                result.Add(args.MetaTarget);
            }

            // block:
            if (calleeHasBlockParam) {
                if (args.Signature.HasBlock) {
                    if (args.GetMetaBlock() == null) {
                        // the user explicitly passed nil as a block arg:
                        result.Add(RubyBinder.NullMetaBlockParam);
                    } else {
                        // pass BlockParam:
                        Debug.Assert(metaBuilder.BfcVariable != null);
                        result.Add(new DynamicMetaObject(metaBuilder.BfcVariable, BindingRestrictions.Empty));
                    }
                } else {
                    // no block passed into a method with a BlockParam:
                    result.Add(RubyBinder.NullMetaBlockParam);
                }
            } else if (injectMissingBlockParam) {
                // no block passed into a method w/o a BlockParam (we still need to fill the missing block argument):
                result.Add(RubyBinder.NullMetaBlockParam);
            }

            // self (parameter):
            if (callConvention == SelfCallConvention.SelfIsParameter) {
                result.Add(args.MetaTarget);
            }

            // simple arguments:
            for (int i = 0; i < args.SimpleArgumentCount; i++) {
                result.Add(args.GetSimpleMetaArgument(i));
            }

            // splat argument:
            int listLength;
            ParameterExpression listVariable;
            if (args.Signature.HasSplattedArgument) {
                var splatted = args.GetSplattedMetaArgument();

                if (metaBuilder.AddSplattedArgumentTest(splatted.Value, splatted.Expression, out listLength, out listVariable)) {

                    // AddTestForListArg only returns 'true' if the argument is a List<object>
                    var list = (List<object>)splatted.Value;

                    // get arguments, add tests
                    for (int j = 0; j < listLength; j++) {
                        result.Add(DynamicMetaObject.Create(
                            list[j], 
                            Ast.Call(listVariable, typeof(List<object>).GetMethod("get_Item"), AstUtils.Constant(j))
                        ));
                    }

                } else {
                    // argument is not an array => add the argument itself:
                    result.Add(splatted);
                }
            }

            // rhs argument:
            if (args.Signature.HasRhsArgument) {
                result.Add(args.GetRhsMetaArgument());
            }

            return result.ToArray();
        }

        // TODO: OBSOLETE
        private static Expression[]/*!*/ MakeActualArgs(MetaObjectBuilder/*!*/ metaBuilder, CallArguments/*!*/ args,
            SelfCallConvention callConvention, bool calleeHasBlockParam, bool injectMissingBlockParam) {

            var actualArgs = new List<Expression>();

            // self (instance):
            if (callConvention == SelfCallConvention.SelfIsInstance) {
                // test already added by method resolver
                Debug.Assert(args.TargetExpression != null);
                AddArgument(actualArgs, args.Target, args.TargetExpression);
            }

            // block:
            if (calleeHasBlockParam) {
                if (args.Signature.HasBlock) {
                    if (args.GetBlock() == null) {
                        // the user explicitly passed nil as a block arg:
                        actualArgs.Add(AstUtils.Constant(null));
                    } else {
                        // pass BlockParam:
                        Debug.Assert(metaBuilder.BfcVariable != null);
                        actualArgs.Add(metaBuilder.BfcVariable);
                    }
                } else {
                    // no block passed into a method with a BlockParam:
                    actualArgs.Add(AstUtils.Constant(null));
                }
            } else if (injectMissingBlockParam) {
                // no block passed into a method w/o a BlockParam (we still need to fill the missing block argument):
                actualArgs.Add(AstUtils.Constant(null));
            }

            // self (non-instance):
            if (callConvention == SelfCallConvention.SelfIsParameter) {
                // test already added by method resolver
                AddArgument(actualArgs, args.Target, args.TargetExpression);
            }

            // simple arguments:
            for (int i = 0; i < args.SimpleArgumentCount; i++) {
                var value = args.GetSimpleArgument(i);
                var expr = args.GetSimpleArgumentExpression(i);

                // TODO: overload-resolution restrictions
                metaBuilder.AddObjectTypeRestriction(value, expr);
                AddArgument(actualArgs, value, expr);
            }

            // splat argument:
            int listLength;
            ParameterExpression listVariable;
            if (args.Signature.HasSplattedArgument) {
                object splattedArg = args.GetSplattedArgument();
                Expression splattedArgExpression = args.GetSplattedArgumentExpression();

                if (metaBuilder.AddSplattedArgumentTest(splattedArg, splattedArgExpression, out listLength, out listVariable)) {

                    // AddTestForListArg only returns 'true' if the argument is a List<object>
                    var list = (List<object>)splattedArg;

                    // get arguments, add tests
                    for (int j = 0; j < listLength; j++) {
                        var value = list[j];
                        var expr = Ast.Call(listVariable, typeof(List<object>).GetMethod("get_Item"), AstUtils.Constant(j));

                        // TODO: overload-resolution restrictions
                        metaBuilder.AddObjectTypeCondition(value, expr);
                        AddArgument(actualArgs, value, expr);
                    }

                } else {
                    // argument is not an array => add the argument itself:
                    AddArgument(actualArgs, splattedArg, splattedArgExpression);
                }
            }

            // rhs argument:
            if (args.Signature.HasRhsArgument) {
                var value = args.GetRhsArgument();
                var expr = args.GetRhsArgumentExpression();

                // TODO: overload-resolution restrictions
                metaBuilder.AddObjectTypeRestriction(value, expr);
                AddArgument(actualArgs, value, expr);
            }

            return actualArgs.ToArray();
        }

        // TODO: OBSOLETE
        private static void AddArgument(List<Expression>/*!*/ actualArgs, object arg, Expression/*!*/ expr) {
            if (arg == null) {
                actualArgs.Add(AstUtils.Constant(null));
            } else {
                var type = CompilerHelpers.GetVisibleType(arg);
                if (type.IsValueType) {
                    actualArgs.Add(expr);
                } else {
                    actualArgs.Add(AstUtils.Convert(expr, type));
                }
            }
        }

        // TODO: OBSOLETE
        private static Type[]/*!*/ GetSignatureToMatch(CallArguments/*!*/ args, SelfCallConvention callConvention) {
            var result = new List<Type>(args.ExplicitArgumentCount);

            // self (instance):
            if (callConvention == SelfCallConvention.SelfIsInstance) {
                result.Add(CompilerHelpers.GetType(args.Target));
            }

            // block:
            if (args.Signature.HasBlock) {
                // use None to let binder know that [NotNull]BlockParam is not applicable
                result.Add(args.GetBlock() != null ? typeof(BlockParam) : typeof(DynamicNull));
            } else {
                result.Add(typeof(MissingBlockParam));
            }

            // self (non-instance):
            if (callConvention == SelfCallConvention.SelfIsParameter) {
                result.Add(CompilerHelpers.GetType(args.Target));
            }

            // simple args:
            for (int i = 0; i < args.SimpleArgumentCount; i++) {
                result.Add(CompilerHelpers.GetType(args.GetSimpleArgument(i)));
            }

            // splat arg:
            if (args.Signature.HasSplattedArgument) {
                object splattedArg = args.GetSplattedArgument();
                
                var list = splattedArg as List<object>;
                if (list != null) {
                    foreach (object obj in list) {
                        result.Add(CompilerHelpers.GetType(obj));
                    }
                } else {
                    result.Add(CompilerHelpers.GetType(splattedArg));
                }
            }

            // rhs arg:
            if (args.Signature.HasRhsArgument) {
                result.Add(CompilerHelpers.GetType(args.GetRhsArgument()));
            }

            return result.ToArray();
        }

        #endregion
    }
}


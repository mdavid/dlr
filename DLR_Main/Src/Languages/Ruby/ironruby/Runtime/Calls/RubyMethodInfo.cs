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

#if CODEPLEX_40
using System;
#else
using System; using Microsoft;
#endif
using System.Reflection;
using IronRuby.Builtins;
using IronRuby.Compiler;
using IronRuby.Compiler.Ast;
using Microsoft.Scripting.Utils;
#if CODEPLEX_40
using Ast = System.Linq.Expressions.Expression;
#else
using Ast = Microsoft.Linq.Expressions.Expression;
#endif
using AstFactory = IronRuby.Compiler.Ast.AstFactory;
using AstUtils = Microsoft.Scripting.Ast.Utils;
using MethodDeclaration = IronRuby.Compiler.Ast.MethodDeclaration;
#if CODEPLEX_40
using MSA = System.Linq.Expressions;
#else
using MSA = Microsoft.Linq.Expressions;
#endif
using System.Diagnostics;

namespace IronRuby.Runtime.Calls {
    public sealed class RubyMethodInfo : RubyMemberInfo {
        // Delegate type for methods with many parameters.
        internal static readonly Type ParamsArrayDelegateType = typeof(Func<object, Proc, object[], object>);

        private RubyMethodBody _body;
        private readonly RubyScope/*!*/ _declaringScope;

        public string/*!*/ DefinitionName { get { return _body.Name; } }
        public int MandatoryParamCount { get { return _body.MandatoryParameterCount; } }
        public int OptionalParamCount { get { return _body.OptionalParameterCount; } }
        public bool HasUnsplatParameter { get { return _body.HasUnsplatParameter; } }
        public RubyScope/*!*/ DeclaringScope { get { return _declaringScope; } }

        // method:
        internal RubyMethodInfo(RubyMethodBody/*!*/ body, RubyScope/*!*/ declaringScope, RubyModule/*!*/ declaringModule, RubyMemberFlags flags)
            : base(flags, declaringModule) {
            Assert.NotNull(body, declaringModule);

            _body = body;
            _declaringScope = declaringScope;
        }

        protected internal override RubyMemberInfo/*!*/ Copy(RubyMemberFlags flags, RubyModule/*!*/ module) {
            return new RubyMethodInfo(_body, _declaringScope, module, flags);
        }
        
        public override RubyMemberInfo TrySelectOverload(Type/*!*/[]/*!*/ parameterTypes) {
            return parameterTypes.Length >= MandatoryParamCount 
                && (HasUnsplatParameter || parameterTypes.Length <= MandatoryParamCount + OptionalParamCount)
                && CollectionUtils.TrueForAll(parameterTypes, (type) => type == typeof(object)) ? this : null;
        }

        public override MemberInfo/*!*/[]/*!*/ GetMembers() {
            return new MemberInfo[] { GetDelegate().Method };
        }

        public override int GetArity() {
            if (_body.HasUnsplatParameter || OptionalParamCount > 0) {
                return -MandatoryParamCount - 1;
            } else {
                return MandatoryParamCount;
            }
        }

        public MethodDeclaration/*!*/ GetSyntaxTree() {
            return _body.Ast;
        }

        internal Delegate/*!*/ GetDelegate() {
            return _body.GetDelegate(_declaringScope, DeclaringModule);
        }

        #region Dynamic Sites

        internal override MethodDispatcher GetDispatcher<T>(RubyCallSignature signature, object target, int version) {
            if (HasUnsplatParameter || OptionalParamCount > 0) {
                return null;
            }

            if (!(target is IRubyObject)) {
                return null;
            }

            return MethodDispatcher.CreateRubyObjectDispatcher(
                typeof(T), GetDelegate(), MandatoryParamCount, signature.HasScope, signature.HasBlock, version
            );
        }

        internal override void BuildCallNoFlow(MetaObjectBuilder/*!*/ metaBuilder, CallArguments/*!*/ args, string/*!*/ name) {
            Assert.NotNull(metaBuilder, args, name);

            // any user method can yield to a block (regardless of whether block parameter is present or not):
            if (args.Signature.HasBlock) {
                metaBuilder.ControlFlowBuilder = RuleControlFlowBuilder;
            }

            // 2 implicit args: self, block
            var argsBuilder = new ArgsBuilder(2, MandatoryParamCount, OptionalParamCount, _body.HasUnsplatParameter);
            argsBuilder.SetImplicit(0, AstFactory.Box(args.TargetExpression));
            argsBuilder.SetImplicit(1, args.Signature.HasBlock ? AstUtils.Convert(args.GetBlockExpression(), typeof(Proc)) : AstFactory.NullOfProc);
            argsBuilder.AddCallArguments(metaBuilder, args);

            if (metaBuilder.Error) {
                return;
            }

            // box explicit arguments:
            var boxedArguments = argsBuilder.GetArguments();
            for (int i = 2; i < boxedArguments.Length; i++) {
                boxedArguments[i] = AstFactory.Box(boxedArguments[i]);
            }

            var method = GetDelegate();
            if (method.GetType() == ParamsArrayDelegateType) {
                // Func<object, Proc, object[], object>
                metaBuilder.Result = AstFactory.CallDelegate(method, new[] { 
                    boxedArguments[0], 
                    boxedArguments[1], 
                    Ast.NewArrayInit(typeof(object), ArrayUtils.ShiftLeft(boxedArguments, 2)) 
                });
            } else {
                metaBuilder.Result = AstFactory.CallDelegate(method, boxedArguments);
            }
        }

        /// <summary>
        /// Takes current result and wraps it into try-filter(MethodUnwinder)-finally block that ensures correct "break" behavior for 
        /// Ruby method calls with a block given in arguments.
        /// 
        /// Sets up a RFC frame similarly to MethodDeclaration.
        /// </summary>
        public static void RuleControlFlowBuilder(MetaObjectBuilder/*!*/ metaBuilder, CallArguments/*!*/ args) {
            Debug.Assert(args.Signature.HasBlock);
            if (metaBuilder.Error) {
                return;
            }

            // TODO (improvement):
            // We don't special case null block here, although we could (we would need a test for that then).
            // We could also statically know (via call-site flag) that the current method is not a proc-converter (passed by ref),
            // which would make such calls faster.
            var rfcVariable = metaBuilder.GetTemporary(typeof(RuntimeFlowControl), "#rfc");
            var methodUnwinder = metaBuilder.GetTemporary(typeof(MethodUnwinder), "#unwinder");
            var resultVariable = metaBuilder.GetTemporary(typeof(object), "#result");

            metaBuilder.Result = Ast.Block(
                // initialize frame (RFC):
                Ast.Assign(rfcVariable, Methods.CreateRfcForMethod.OpCall(AstUtils.Convert(args.GetBlockExpression(), typeof(Proc)))),
                AstUtils.Try(
                    Ast.Assign(resultVariable, metaBuilder.Result)
                ).Filter(methodUnwinder, Ast.Equal(Ast.Field(methodUnwinder, MethodUnwinder.TargetFrameField), rfcVariable),

                    // return unwinder.ReturnValue;
                    Ast.Assign(resultVariable, Ast.Field(methodUnwinder, MethodUnwinder.ReturnValueField))

                ).Finally(
                    // we need to mark the RFC dead snce the block might escape and break later:
                    Methods.LeaveMethodFrame.OpCall(rfcVariable)
                ), 
                resultVariable
            );
        }

        #endregion
    }
}

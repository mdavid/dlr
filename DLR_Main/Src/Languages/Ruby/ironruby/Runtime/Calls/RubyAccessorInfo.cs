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
using System.Diagnostics;
using System.Reflection;
using IronRuby.Builtins;
using IronRuby.Compiler;
using Microsoft.Scripting.Utils;
using AstUtils = Microsoft.Scripting.Ast.Utils;

namespace IronRuby.Runtime.Calls {
    using AstFactory = IronRuby.Compiler.Ast.AstFactory;

    public abstract class RubyAttributeAccessorInfo : RubyMemberInfo {
        private readonly string/*!*/ _instanceVariableName;

        protected string/*!*/ InstanceVariableName { get { return _instanceVariableName; } }

        protected RubyAttributeAccessorInfo(RubyMemberFlags flags, RubyModule/*!*/ declaringModule, string/*!*/ variableName)
            : base(flags, declaringModule) {
            Assert.NotEmpty(variableName);
            Debug.Assert(variableName.StartsWith("@"));
            _instanceVariableName = variableName;
        }

        internal override bool IsRemovable {
            get { return true; }
        }

        internal override bool IsDataMember {
            get { return true; }
        }

        public override MemberInfo/*!*/[]/*!*/ GetMembers() {
            return Utils.EmptyMemberInfos;
        }
    }

    public sealed class RubyAttributeReaderInfo : RubyAttributeAccessorInfo {
        public RubyAttributeReaderInfo(RubyMemberFlags flags, RubyModule/*!*/ declaringModule, string/*!*/ variableName)
            : base(flags, declaringModule, variableName) {
        }

        internal override void BuildCallNoFlow(MetaObjectBuilder/*!*/ metaBuilder, CallArguments/*!*/ args, string/*!*/ name) {
            var actualArgs = RubyOverloadResolver.NormalizeArguments(metaBuilder, args, 0, 0);
            if (!metaBuilder.Error) {
                metaBuilder.Result = Methods.GetInstanceVariable.OpCall(
                    AstUtils.Convert(args.MetaScope.Expression, typeof(RubyScope)),
                    AstFactory.Box(args.TargetExpression),
                    AstUtils.Constant(InstanceVariableName)
                );
            }
        }

        protected internal override RubyMemberInfo/*!*/ Copy(RubyMemberFlags flags, RubyModule/*!*/ module) {
            return new RubyAttributeReaderInfo(flags, module, InstanceVariableName);
        }

        public override RubyMemberInfo TrySelectOverload(Type/*!*/[]/*!*/ parameterTypes) {
            return parameterTypes.Length == 0 ? this : null;
        }
    }

    public sealed class RubyAttributeWriterInfo : RubyAttributeAccessorInfo {
        public RubyAttributeWriterInfo(RubyMemberFlags flags, RubyModule/*!*/ declaringModule, string/*!*/ name)
            : base(flags, declaringModule, name) {
        }

        internal override void BuildCallNoFlow(MetaObjectBuilder/*!*/ metaBuilder, CallArguments/*!*/ args, string/*!*/ name) {
            var actualArgs = RubyOverloadResolver.NormalizeArguments(metaBuilder, args, 1, 1);
            if (!metaBuilder.Error) {
                metaBuilder.Result = Methods.SetInstanceVariable.OpCall(
                    AstFactory.Box(args.TargetExpression),
                    AstFactory.Box(actualArgs[0].Expression),
                    AstUtils.Convert(args.MetaScope.Expression, typeof(RubyScope)),
                    AstUtils.Constant(InstanceVariableName)
                );
            }
        }

        protected internal override RubyMemberInfo/*!*/ Copy(RubyMemberFlags flags, RubyModule/*!*/ module) {
            return new RubyAttributeWriterInfo(flags, module, InstanceVariableName);
        }

        public override RubyMemberInfo TrySelectOverload(Type/*!*/[]/*!*/ parameterTypes) {
            return parameterTypes.Length == 1 && parameterTypes[0] == typeof(object) ? this : null;
        }
    }
}

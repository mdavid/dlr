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
using System.Collections.Generic;
using System.Reflection;
#if CODEPLEX_40
using System.Linq.Expressions;
#else
using Microsoft.Linq.Expressions;
#endif
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using Microsoft.Scripting.Actions;
using IronRuby.Compiler;
using IronRuby.Builtins;
using AstUtils = Microsoft.Scripting.Ast.Utils;

namespace IronRuby.Runtime.Calls {
#if CODEPLEX_40
    using Ast = System.Linq.Expressions.Expression;
#else
    using Ast = Microsoft.Linq.Expressions.Expression;
#endif
    
    public sealed class RubyEventInfo : RubyMemberInfo {
        private readonly EventTracker/*!*/ _tracker;

        // True if the member is defined in Ruby (e.g. via alias) - such definition "detaches" it from the underlying type.
        private readonly bool _isDetached;

        public EventTracker/*!*/ Tracker { get { return _tracker; } }

        public RubyEventInfo(EventTracker/*!*/ tracker, RubyMemberFlags flags, RubyModule/*!*/ declaringModule, bool isDetached)
            : base(flags, declaringModule) {
            Assert.NotNull(tracker, declaringModule);
            _tracker = tracker;
            _isDetached = isDetached;
        }

        protected internal override RubyMemberInfo/*!*/ Copy(RubyMemberFlags flags, RubyModule/*!*/ module) {
            return new RubyEventInfo(_tracker, flags, module, true);
        }

        internal override bool IsDataMember {
            get { return true; }
        }

        internal override bool IsRubyMember {
            get { return _isDetached; }
        }

        public override MemberInfo/*!*/[]/*!*/ GetMembers() {
            return new MemberInfo[] { _tracker.Event };
        }

        public override RubyMemberInfo TrySelectOverload(Type/*!*/[]/*!*/ parameterTypes) {
            return parameterTypes.Length == 0 ? this : null;
        }

        internal override void BuildCallNoFlow(MetaObjectBuilder/*!*/ metaBuilder, CallArguments/*!*/ args, string/*!*/ name) {
            // TODO: splat, rhs, ...
            if (args.Signature.ArgumentCount == 0) {
                if (args.Signature.HasBlock) {
                    metaBuilder.Result = Methods.HookupEvent.OpCall(
                        AstUtils.Constant(this),
                        args.TargetExpression,
                        Ast.Convert(args.GetBlockExpression(), typeof(Proc))
                    );
                } else {
                    metaBuilder.Result = Methods.CreateEvent.OpCall(
                        AstUtils.Constant(this),
                        args.TargetExpression,
                        AstUtils.Constant(name)
                    );
                }
            } else {
                metaBuilder.SetError(Methods.MakeWrongNumberOfArgumentsError.OpCall(Ast.Constant(args.Signature.ArgumentCount), Ast.Constant(0)));
            }
        }

        private static void ReadAll(IList<ParameterExpression> variables, IList<Expression> expressions, int start) {
            for (int i = 0, j = start; i < variables.Count; i++, j++) {
                expressions[j] = variables[i];
            }
        }
    }
}

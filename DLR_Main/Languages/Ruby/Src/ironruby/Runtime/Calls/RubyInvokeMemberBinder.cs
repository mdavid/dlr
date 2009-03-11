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
using Microsoft.Linq.Expressions;
using Microsoft.Scripting;
using Microsoft.Scripting.Actions;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using IronRuby.Compiler;
using IronRuby.Builtins;
using AstUtils = Microsoft.Scripting.Ast.Utils;

namespace IronRuby.Runtime.Calls {
    using Ast = Microsoft.Linq.Expressions.Expression;

    internal sealed class RubyInvokeMemberBinder : InvokeMemberBinder {
        private readonly RubyContext/*!*/ _context;

        public RubyInvokeMemberBinder(RubyContext/*!*/ context, string/*!*/ name, CallInfo /*!*/ callInfo)
            : base(name, false, callInfo) {
            _context = context;
        }

        public override DynamicMetaObject/*!*/ FallbackInvoke(DynamicMetaObject/*!*/ self, DynamicMetaObject/*!*/[]/*!*/ args, DynamicMetaObject/*!*/ onBindingError) {
            var result = TryBind(_context, this, self, args);
            if (result != null) {
                return result;
            }

            // TODO: return ((DefaultBinder)_context.Binder).GetMember(Name, self, Ast.Null(typeof(CodeContext)), true);
            throw new NotImplementedException();
        }

        public override DynamicMetaObject/*!*/ FallbackInvokeMember(DynamicMetaObject/*!*/ self, DynamicMetaObject/*!*/[]/*!*/ args, DynamicMetaObject/*!*/ onBindingError) {
            var result = TryBind(_context, this, self, args);
            if (result != null) {
                return result;
            }

            // TODO: return ((DefaultBinder)_context.Binder).GetMember(Name, self, Ast.Null(typeof(CodeContext)), true);
            throw new NotImplementedException();
        }

        public static DynamicMetaObject TryBind(RubyContext/*!*/ context, InvokeMemberBinder/*!*/ binder, DynamicMetaObject/*!*/ target, DynamicMetaObject/*!*/[]/*!*/ args) {
            Assert.NotNull(context, target);

            var metaBuilder = new MetaObjectBuilder();
            
            RubyCallAction.Bind(metaBuilder, binder.Name,
                new CallArguments(
                    new DynamicMetaObject(AstUtils.Constant(context), BindingRestrictions.Empty, context),
                    target, 
                    args, 
                    RubyCallSignature.Simple(binder.CallInfo.ArgumentCount)
                )
            );

            // TODO: we should return null if we fail, we need to throw exception for now:
            return metaBuilder.CreateMetaObject(binder);
        }
    }
}

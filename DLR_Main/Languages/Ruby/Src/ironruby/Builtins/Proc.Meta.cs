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
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using IronRuby.Runtime;
using IronRuby.Runtime.Calls;

using Ast = Microsoft.Linq.Expressions.Expression;
using AstUtils = Microsoft.Scripting.Ast.Utils;
using IronRuby.Compiler;

namespace IronRuby.Builtins {

    public partial class Proc : IDynamicMetaObjectProvider {
        public DynamicMetaObject/*!*/ GetMetaObject(Expression/*!*/ parameter) {
            return new Meta(parameter, BindingRestrictions.Empty, this);
        }

        internal sealed class Meta : DynamicMetaObject {
            public Meta(Expression/*!*/ expression, BindingRestrictions/*!*/ restrictions, Proc/*!*/ value)
                : base(expression, restrictions, value) {
                ContractUtils.RequiresNotNull(value, "value");
            }

            public override DynamicMetaObject/*!*/ BindInvoke(InvokeBinder/*!*/ action, DynamicMetaObject/*!*/[]/*!*/ args) {
                RubyCallSignature callSignature;
                if (RubyCallSignature.TryCreate(action.CallInfo, out callSignature)) {
                    return action.FallbackInvoke(this, args);
                }

                var context = new DynamicMetaObject(
                    Methods.GetContextFromProc.OpCall(AstUtils.Convert(Expression, typeof(Proc))),
                    BindingRestrictions.Empty,
                    RubyOps.GetContextFromProc((Proc)Value)
                );

                var metaBuilder = new MetaObjectBuilder();
                Proc.SetCallActionRule(metaBuilder, new CallArguments(context, this, args, callSignature), true);
                return metaBuilder.CreateMetaObject(action, args);
            }
        }
    }
}

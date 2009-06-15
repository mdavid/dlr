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
using System.Text;
#if CODEPLEX_40
using System.Dynamic;
using System.Linq.Expressions;
#else
using Microsoft.Scripting;
using Microsoft.Linq.Expressions;
#endif
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using IronRuby.Runtime.Calls;

#if CODEPLEX_40
using Ast = System.Linq.Expressions.Expression;
#else
using Ast = Microsoft.Linq.Expressions.Expression;
#endif
using AstFactory = IronRuby.Compiler.Ast.AstFactory;
using AstUtils = Microsoft.Scripting.Ast.Utils;
using IronRuby.Compiler;
using System.Reflection;
    
namespace IronRuby.Runtime {
    public sealed partial class BlockParam : IRubyDynamicMetaObjectProvider {
        public DynamicMetaObject/*!*/ GetMetaObject(Expression/*!*/ parameter) {
            return new Meta(parameter, BindingRestrictions.Empty, this);
        }

        internal sealed class Meta : RubyMetaObject<BlockParam> {
            public override RubyContext/*!*/ Context {
                get { return Value.RubyContext; }
            }

            protected override MethodInfo/*!*/ ContextConverter {
                get { return Methods.GetContextFromBlockParam; }
            }

            public Meta(Expression/*!*/ expression, BindingRestrictions/*!*/ restrictions, BlockParam/*!*/ value)
                : base(expression, restrictions, value) {
                ContractUtils.RequiresNotNull(value, "value");
            }

            public override DynamicMetaObject/*!*/ BindInvoke(InvokeBinder/*!*/ binder, DynamicMetaObject/*!*/[]/*!*/ args) {
                return InteropBinder.Invoke.Bind(binder, this, args, Value.BuildInvoke);
            }
        }
    }
}

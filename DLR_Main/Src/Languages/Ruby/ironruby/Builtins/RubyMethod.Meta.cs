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
#if CODEPLEX_40
using System.Dynamic;
using System.Dynamic.Utils;
using System.Linq.Expressions;
#else
using Microsoft.Scripting;
using Microsoft.Linq.Expressions;
#endif
using System.Reflection;
using Microsoft.Scripting.Utils;
using Microsoft.Scripting.Runtime;
using IronRuby.Runtime;
using IronRuby.Runtime.Calls;
using IronRuby.Compiler;
using AstUtils = Microsoft.Scripting.Ast.Utils;

namespace IronRuby.Builtins {
#if CODEPLEX_40
    using Ast = System.Linq.Expressions.Expression;
#else
    using Ast = Microsoft.Linq.Expressions.Expression;
#endif

    public partial class RubyMethod : IRubyDynamicMetaObjectProvider {
        public DynamicMetaObject/*!*/ GetMetaObject(Expression/*!*/ parameter) {
            return new Meta(parameter, BindingRestrictions.Empty, this);
        }

        internal sealed class Meta : RubyMetaObject<RubyMethod> {
            public override RubyContext/*!*/ Context {
                get { return Value.Info.Context; }
            }

            protected override MethodInfo/*!*/ ContextConverter {
                get { return Methods.GetContextFromMethod; }
            }

            public Meta(Expression/*!*/ expression, BindingRestrictions/*!*/ restrictions, RubyMethod/*!*/ value)
                : base(expression, restrictions, value) {
            }

            public override DynamicMetaObject/*!*/ BindConvert(ConvertBinder/*!*/ binder) {
                return InteropBinder.TryBindCovertToDelegate(this, binder, Methods.CreateDelegateFromMethod) 
                    ?? base.BindConvert(binder);
            }

            public override DynamicMetaObject/*!*/ BindInvoke(InvokeBinder/*!*/ binder, DynamicMetaObject/*!*/[]/*!*/ args) {
                return InteropBinder.Invoke.Bind(binder, this, args, Value.BuildInvoke);
            }
        }
    }
}

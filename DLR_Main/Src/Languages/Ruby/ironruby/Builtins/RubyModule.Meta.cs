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


#if CODEPLEX_40
using System.Linq.Expressions;
using System.Dynamic;
#else
using Microsoft.Linq.Expressions;
using Microsoft.Scripting;
#endif
using Microsoft.Scripting.Utils;
using IronRuby.Runtime.Calls;
using IronRuby.Runtime;
using System.Reflection;
using IronRuby.Compiler;

namespace IronRuby.Builtins {

    public partial class RubyModule : IRubyDynamicMetaObjectProvider {
        public virtual DynamicMetaObject/*!*/ GetMetaObject(Expression/*!*/ parameter) {
            return new Meta(parameter, BindingRestrictions.Empty, this);
        }

        internal class Meta : RubyMetaObject<RubyModule> {
            public override RubyContext/*!*/ Context {
                get { return Value.Context; }
            }

            protected override MethodInfo/*!*/ ContextConverter {
                get { return Methods.GetContextFromModule; }
            }
            
            public Meta(Expression/*!*/ expression, BindingRestrictions/*!*/ restrictions, RubyModule/*!*/ value)
                : base(expression, restrictions, value) {
            }

            // TODO: GetMember, SetMember, Call
        }
    }
}

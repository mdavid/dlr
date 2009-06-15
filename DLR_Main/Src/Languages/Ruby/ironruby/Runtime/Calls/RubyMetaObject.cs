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

namespace IronRuby.Runtime.Calls {
#if CODEPLEX_40
    using Ast = System.Linq.Expressions.Expression;
#else
    using Ast = Microsoft.Linq.Expressions.Expression;
#endif

    interface IRubyDynamicMetaObjectProvider : IDynamicMetaObjectProvider {
    }

    public abstract class RubyMetaObject : DynamicMetaObject {
        public abstract RubyContext/*!*/ Context { get; }
        public abstract Expression/*!*/ ContextExpression { get; }

        internal RubyMetaObject(Expression/*!*/ expression, BindingRestrictions/*!*/ restrictions, object/*!*/ value)
            : base(expression, restrictions, value) {
            ContractUtils.RequiresNotNull(value, "value");
        }

        internal DynamicMetaObject/*!*/ CreateMetaContext() {
            return new DynamicMetaObject(ContextExpression, BindingRestrictions.Empty, Context);
        }

        public override DynamicMetaObject/*!*/ BindInvokeMember(InvokeMemberBinder/*!*/ binder, params DynamicMetaObject/*!*/[]/*!*/ args) {
            return InteropBinder.InvokeMember.Bind(CreateMetaContext(), binder, this, args, binder.FallbackInvokeMember);
        }

        public override DynamicMetaObject/*!*/ BindGetMember(GetMemberBinder/*!*/ binder) {
            return InteropBinder.GetMember.Bind(CreateMetaContext(), binder, this, binder.FallbackGetMember);
        }

        public override DynamicMetaObject/*!*/ BindSetMember(SetMemberBinder/*!*/ binder, DynamicMetaObject/*!*/ value) {
            return InteropBinder.SetMember.Bind(CreateMetaContext(), binder, this, value, binder.FallbackSetMember);
        }

        public override DynamicMetaObject/*!*/ BindGetIndex(GetIndexBinder/*!*/ binder, DynamicMetaObject/*!*/[]/*!*/ indexes) {
            return InteropBinder.GetIndex.Bind(CreateMetaContext(), binder, this, indexes, binder.FallbackGetIndex);
        }

        public override DynamicMetaObject/*!*/ BindSetIndex(SetIndexBinder/*!*/ binder, DynamicMetaObject/*!*/[]/*!*/ indexes, DynamicMetaObject value) {
            return InteropBinder.SetIndex.Bind(CreateMetaContext(), binder, this, indexes, value, binder.FallbackSetIndex);
        }

        public override DynamicMetaObject/*!*/ BindUnaryOperation(UnaryOperationBinder/*!*/ binder) {
            return InteropBinder.UnaryOperation.Bind(CreateMetaContext(), binder, this, binder.FallbackUnaryOperation);
        }

        public override DynamicMetaObject/*!*/ BindBinaryOperation(BinaryOperationBinder/*!*/ binder, DynamicMetaObject/*!*/ arg) {
            return InteropBinder.BinaryOperation.Bind(CreateMetaContext(), binder, this, arg, binder.FallbackBinaryOperation);
        }
    }
    
    public abstract class RubyMetaObject<T> : RubyMetaObject {
        // TODO: use interface?
        protected abstract MethodInfo/*!*/ ContextConverter { get; }

        public new T/*!*/ Value {
            get { return (T)base.Value; }
        }

        public sealed override Expression/*!*/ ContextExpression {
            get { return ContextConverter.OpCall(AstUtils.Convert(Expression, typeof(T))); }
        }

        public RubyMetaObject(Expression/*!*/ expression, BindingRestrictions/*!*/ restrictions, T/*!*/ value)
            : base(expression, restrictions, value) {
        }
    }
}

/* ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation. 
 *
 * This source code is subject to terms and conditions of the Microsoft Public License. A 
 * copy of the license can be found in the License.html file at the root of this distribution. If 
 * you cannot locate the  Microsoft Public License, please send an email to 
 * dlr@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
 * by the terms of the Microsoft Public License.
 *
 * You must not remove this notice, or any other, from this software.
 *
 *
 * ***************************************************************************/

using System; using Microsoft;
using Microsoft.Scripting;
using Microsoft.Linq.Expressions;
using Microsoft.Scripting.Actions;
using Microsoft.Scripting.Utils;

namespace ToyScript.Binders {
    public class ErrorMetaObject : OperationMetaObject {
        public ErrorMetaObject(Type exception, string message, BindingRestrictions restrictions)
            : base(CreateThrow(exception, message), restrictions) {
        }

        private static Expression CreateThrow(Type exception, string message) {
            ContractUtils.RequiresNotNull(exception, "exception");
            ContractUtils.Requires(typeof(Exception).IsAssignableFrom(exception), "exception");
            return Expression.Throw(
                Expression.New(
                    exception.GetConstructor(new Type[] { typeof(string) }),
                    Expression.Constant(message)
                )
            );
        }

        public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder action, DynamicMetaObject[] args) {
            return this;
        }

        public override DynamicMetaObject BindConvert(ConvertBinder action) {
            return this;
        }

        public override DynamicMetaObject BindCreateInstance(CreateInstanceBinder signature, DynamicMetaObject[] args) {
            return this;
        }

        public override DynamicMetaObject BindDeleteMember(DeleteMemberBinder action) {
            return this;
        }

        [Obsolete]
        public override DynamicMetaObject BindOperation(OperationBinder action, DynamicMetaObject[] args) {
            return this;
        }

        public override DynamicMetaObject BindGetMember(GetMemberBinder action) {
            return this;
        }

        public override DynamicMetaObject BindSetMember(SetMemberBinder action, DynamicMetaObject value) {
            return this;
        }
    }
}

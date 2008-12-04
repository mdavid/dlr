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
using Microsoft.Linq.Expressions;
using System.Reflection;
using Microsoft.Scripting;
using Microsoft.Scripting.Generation;
using Microsoft.Scripting.Runtime;

namespace ToyScript.Binders {
    public abstract class ClsBinder {
        internal static MetaObject GetMemberOnType(Type type, string name, Expression expression, Restrictions restrictions) {
            MemberInfo[] members = type.GetMember(name);
            if (members == null || members.Length != 1) {
                return new ErrorMetaObject(typeof(MissingMemberException), "No or ambiguous member " + name, restrictions);
            }

            MemberInfo member = members[0];
            switch (member.MemberType) {
                case MemberTypes.Field:
                    return new ClsMetaObject(Expression.Field(expression, (FieldInfo)member), restrictions);
                case MemberTypes.Property:
                    return new ClsMetaObject(Expression.Property(expression, (PropertyInfo)member), restrictions);
                default:
                    return new ErrorMetaObject(typeof(MissingMemberException), "Wrong member " + name, restrictions);
            }
        }

        internal static MetaObject BindObjectGetMember(ToyGetMemberBinder action, MetaObject self) {
            Type type = self.RuntimeType;
            if (type == null) {
                return action.Defer(self);
            }

            MetaObject restricted = self.Restrict(type);

            if (CompilerHelpers.IsStrongBox(self.Value)) {
                MetaObject box = new ClsMetaObject(
                    Expression.Field(restricted.Expression, "Value"),
                    restricted.Restrictions
                );
                return box.BindGetMember(action);
            } else {
                return GetMemberOnType(restricted.LimitType, action.Name, restricted.Expression, restricted.Restrictions);
            }
        }

        internal static MetaObject BindTypeGetMember(ToyGetMemberBinder action, MetaObject self) {
            Type type = self.LimitType;
            if (!type.IsSealed) {
                return action.Defer(self);
            }

            return GetMemberOnType(type, action.Name, self.Expression, self.Restrictions);
        }
    }
}

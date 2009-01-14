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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using Microsoft.Scripting.Utils;

namespace Microsoft.Linq.Expressions {
    //CONFORMING
    /// <summary>
    /// Represents initializing the elements of a collection member of a newly created object. 
    /// </summary>
    public sealed class MemberListBinding : MemberBinding {
        ReadOnlyCollection<ElementInit> _initializers;
        internal MemberListBinding(MemberInfo member, ReadOnlyCollection<ElementInit> initializers)
            : base(MemberBindingType.ListBinding, member) {
            _initializers = initializers;
        }

        /// <summary>
        /// Gets the element initializers for initializing a collection member of a newly created object.
        /// </summary>
        public ReadOnlyCollection<ElementInit> Initializers {
            get { return _initializers; }
        }
    }
    

    public partial class Expression {
        //CONFORMING
        ///<summary>Creates a <see cref="T:Microsoft.Linq.Expressions.MemberListBinding" /> where the member is a field or property.</summary>
        ///<returns>A <see cref="T:Microsoft.Linq.Expressions.MemberListBinding" /> that has the <see cref="P:Microsoft.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:Microsoft.Linq.Expressions.MemberBindingType.ListBinding" /> and the <see cref="P:Microsoft.Linq.Expressions.MemberBinding.Member" /> and <see cref="P:Microsoft.Linq.Expressions.MemberListBinding.Initializers" /> properties set to the specified values.</returns>
        ///<param name="member">A <see cref="T:System.Reflection.MemberInfo" /> that represents a field or property to set the <see cref="P:Microsoft.Linq.Expressions.MemberBinding.Member" /> property equal to.</param>
        ///<param name="initializers">An array of <see cref="T:Microsoft.Linq.Expressions.ElementInit" /> objects to use to populate the <see cref="P:Microsoft.Linq.Expressions.MemberListBinding.Initializers" /> collection.</param>
        ///<exception cref="T:System.ArgumentNullException">
        ///<paramref name="member" /> is null. -or-One or more elements of <paramref name="initializers" /> is null.</exception>
        ///<exception cref="T:System.ArgumentException">
        ///<paramref name="member" /> does not represent a field or property.-or-The <see cref="P:System.Reflection.FieldInfo.FieldType" /> or <see cref="P:System.Reflection.PropertyInfo.PropertyType" /> of the field or property that <paramref name="member" /> represents does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>
        public static MemberListBinding ListBind(MemberInfo member, params ElementInit[] initializers) {
            ContractUtils.RequiresNotNull(member, "member");
            ContractUtils.RequiresNotNull(initializers, "initializers");
            return ListBind(member, initializers.ToReadOnly());
        }
        //CONFORMING
        ///<summary>Creates a <see cref="T:Microsoft.Linq.Expressions.MemberListBinding" /> where the member is a field or property.</summary>
        ///<returns>A <see cref="T:Microsoft.Linq.Expressions.MemberListBinding" /> that has the <see cref="P:Microsoft.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:Microsoft.Linq.Expressions.MemberBindingType.ListBinding" /> and the <see cref="P:Microsoft.Linq.Expressions.MemberBinding.Member" /> and <see cref="P:Microsoft.Linq.Expressions.MemberListBinding.Initializers" /> properties set to the specified values.</returns>
        ///<param name="member">A <see cref="T:System.Reflection.MemberInfo" /> that represents a field or property to set the <see cref="P:Microsoft.Linq.Expressions.MemberBinding.Member" /> property equal to.</param>
        ///<param name="initializers">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:Microsoft.Linq.Expressions.ElementInit" /> objects to use to populate the <see cref="P:Microsoft.Linq.Expressions.MemberListBinding.Initializers" /> collection.</param>
        ///<exception cref="T:System.ArgumentNullException">
        ///<paramref name="member" /> is null. -or-One or more elements of <paramref name="initializers" /> is null.</exception>
        ///<exception cref="T:System.ArgumentException">
        ///<paramref name="member" /> does not represent a field or property.-or-The <see cref="P:System.Reflection.FieldInfo.FieldType" /> or <see cref="P:System.Reflection.PropertyInfo.PropertyType" /> of the field or property that <paramref name="member" /> represents does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>
        public static MemberListBinding ListBind(MemberInfo member, IEnumerable<ElementInit> initializers) {
            ContractUtils.RequiresNotNull(member, "member");
            ContractUtils.RequiresNotNull(initializers, "initializers");
            Type memberType;
            ValidateGettableFieldOrPropertyMember(member, out memberType);
            ReadOnlyCollection<ElementInit> initList = initializers.ToReadOnly();
            ValidateListInitArgs(memberType, initList);
            return new MemberListBinding(member, initList);
        }
        //CONFORMING
        ///<summary>Creates a <see cref="T:Microsoft.Linq.Expressions.MemberListBinding" /> object based on a specified property accessor method.</summary>
        ///<returns>A <see cref="T:Microsoft.Linq.Expressions.MemberListBinding" /> that has the <see cref="P:Microsoft.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:Microsoft.Linq.Expressions.MemberBindingType.ListBinding" />, the <see cref="P:Microsoft.Linq.Expressions.MemberBinding.Member" /> property set to the <see cref="T:System.Reflection.MemberInfo" /> that represents the property accessed in <paramref name="propertyAccessor" />, and <see cref="P:Microsoft.Linq.Expressions.MemberListBinding.Initializers" /> populated with the elements of <paramref name="initializers" />.</returns>
        ///<param name="propertyAccessor">A <see cref="T:System.Reflection.MethodInfo" /> that represents a property accessor method.</param>
        ///<param name="initializers">An array of <see cref="T:Microsoft.Linq.Expressions.ElementInit" /> objects to use to populate the <see cref="P:Microsoft.Linq.Expressions.MemberListBinding.Initializers" /> collection.</param>
        ///<exception cref="T:System.ArgumentNullException">
        ///<paramref name="propertyAccessor" /> is null. -or-One or more elements of <paramref name="initializers" /> is null.</exception>
        ///<exception cref="T:System.ArgumentException">
        ///<paramref name="propertyAccessor" /> does not represent a property accessor method.-or-The <see cref="P:System.Reflection.PropertyInfo.PropertyType" /> of the property that the method represented by <paramref name="propertyAccessor" /> accesses does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>  
        public static MemberListBinding ListBind(MethodInfo propertyAccessor, params ElementInit[] initializers) {
            ContractUtils.RequiresNotNull(propertyAccessor, "propertyAccessor");
            ContractUtils.RequiresNotNull(initializers, "initializers");
            return ListBind(propertyAccessor, initializers.ToReadOnly());
        }
        //CONFORMING
        ///<summary>Creates a <see cref="T:Microsoft.Linq.Expressions.MemberListBinding" /> based on a specified property accessor method.</summary>
        ///<returns>A <see cref="T:Microsoft.Linq.Expressions.MemberListBinding" /> that has the <see cref="P:Microsoft.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:Microsoft.Linq.Expressions.MemberBindingType.ListBinding" />, the <see cref="P:Microsoft.Linq.Expressions.MemberBinding.Member" /> property set to the <see cref="T:System.Reflection.MemberInfo" /> that represents the property accessed in <paramref name="propertyAccessor" />, and <see cref="P:Microsoft.Linq.Expressions.MemberListBinding.Initializers" /> populated with the elements of <paramref name="initializers" />.</returns>
        ///<param name="propertyAccessor">A <see cref="T:System.Reflection.MethodInfo" /> that represents a property accessor method.</param>
        ///<param name="initializers">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:Microsoft.Linq.Expressions.ElementInit" /> objects to use to populate the <see cref="P:Microsoft.Linq.Expressions.MemberListBinding.Initializers" /> collection.</param>
        ///<exception cref="T:System.ArgumentNullException">
        ///<paramref name="propertyAccessor" /> is null. -or-One or more elements of <paramref name="initializers" /> are null.</exception>
        ///<exception cref="T:System.ArgumentException">
        ///<paramref name="propertyAccessor" /> does not represent a property accessor method.-or-The <see cref="P:System.Reflection.PropertyInfo.PropertyType" /> of the property that the method represented by <paramref name="propertyAccessor" /> accesses does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>        
        public static MemberListBinding ListBind(MethodInfo propertyAccessor, IEnumerable<ElementInit> initializers) {
            ContractUtils.RequiresNotNull(propertyAccessor, "propertyAccessor");
            ContractUtils.RequiresNotNull(initializers, "initializers");
            return ListBind(GetProperty(propertyAccessor), initializers);
        }

        //CONFORMING
        private static void ValidateListInitArgs(Type listType, ReadOnlyCollection<ElementInit> initializers) {
            if (!typeof(IEnumerable).IsAssignableFrom(listType)) {
                throw Error.TypeNotIEnumerable(listType);
            }
            for (int i = 0, n = initializers.Count; i < n; i++) {
                ElementInit element = initializers[i];
                ContractUtils.RequiresNotNull(element, "initializers");
                ValidateCallInstanceType(listType, element.AddMethod);
            }
        }
    }
}

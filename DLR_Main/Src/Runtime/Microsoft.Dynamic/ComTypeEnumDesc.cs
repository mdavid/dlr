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

#if !SILVERLIGHT // ComObject

#if !MICROSOFT_DYNAMIC
using System.Linq.Expressions;
#else
using Microsoft.Scripting.Ast;
#endif

using System.Runtime.InteropServices;
using System.Dynamic;
using System.Globalization;
using ComTypes = System.Runtime.InteropServices.ComTypes;

namespace System.Dynamic {
    internal sealed class ComTypeEnumDesc : ComTypeDesc {
        public override string ToString() {
            return String.Format(CultureInfo.CurrentCulture, "<enum '{0}'>", TypeName);
        }

        internal ComTypeEnumDesc(ComTypes.ITypeInfo typeInfo) :
            base(typeInfo) {
        }
    }
}

#endif

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

#if CODEPLEX_40
using System;
using System.Dynamic;
using System.Linq.Expressions;
#else
using System; using Microsoft;
using Microsoft.Scripting;
using Microsoft.Linq.Expressions;
#endif

using Microsoft.Scripting.Actions;

#if CODEPLEX_40
using Ast = System.Linq.Expressions.Expression;
#else
using Ast = Microsoft.Linq.Expressions.Expression;
#endif
using Microsoft.Scripting.Runtime;
using System.Runtime.CompilerServices;
#if !CODEPLEX_40
using Microsoft.Runtime.CompilerServices;
#endif


namespace IronPython.Runtime.Binding {
    struct FastBindResult<T> where T : class {
        public readonly T Target;
        public readonly bool ShouldCache;

        public FastBindResult(T target, bool shouldCache) {
            Target = target;
            ShouldCache = shouldCache;
        }

        public FastBindResult(T target) {
            Target = target;
            ShouldCache = false;
        }
    }
}

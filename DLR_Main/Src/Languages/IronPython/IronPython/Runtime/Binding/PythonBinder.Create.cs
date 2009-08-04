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
using System.Linq.Expressions;
#else
using System; using Microsoft;
using Microsoft.Linq.Expressions;
#endif
using System.Reflection;
#if CODEPLEX_40
using System.Dynamic;
#else
using Microsoft.Scripting;
#endif
using Microsoft.Scripting.Generation;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using Microsoft.Scripting.Actions.Calls;
using Microsoft.Scripting.Actions;

namespace IronPython.Runtime.Binding {
#if CODEPLEX_40
    using Ast = System.Linq.Expressions.Expression;
#else
    using Ast = Microsoft.Linq.Expressions.Expression;
#endif

    public sealed partial class PythonBinder : DefaultBinder {
        public DynamicMetaObject Create(CallSignature signature, DynamicMetaObject target, DynamicMetaObject[] args, Expression contextExpression) {

            Type t = GetTargetType(target.Value);

            if (t != null) {

                if (typeof(Delegate).IsAssignableFrom(t) && args.Length == 1) {
                    MethodInfo dc = GetDelegateCtor(t);

                    // BinderOps.CreateDelegate<T>(CodeContext context, object callable);
                    return new DynamicMetaObject(
                        Ast.Call(null, dc, contextExpression, args[0].Expression),
                        target.Restrictions.Merge(BindingRestrictions.GetInstanceRestriction(target.Expression, target.Value))
                    );
                }

                return CallMethod(
                    new PythonOverloadResolver(
                        this,
                        args,
                        signature,
                        contextExpression
                    ), 
                    CompilerHelpers.GetConstructors(t, PrivateBinding),
                    target.Restrictions.Merge(BindingRestrictions.GetInstanceRestriction(target.Expression, target.Value))
                );
            }

            return null;
        }

        private static MethodInfo GetDelegateCtor(Type t) {
            return typeof(BinderOps).GetMethod("CreateDelegate").MakeGenericMethod(t);
        }

        private static Type GetTargetType(object target) {
            TypeTracker tt = target as TypeTracker;
            if (tt != null) {
                return tt.Type;
            }
            return target as Type;
        }
    }
}

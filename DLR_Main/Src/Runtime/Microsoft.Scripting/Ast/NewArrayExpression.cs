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
#else
using System; using Microsoft;
#endif
using System.Collections.Generic;
using System.Collections.ObjectModel;
#if CODEPLEX_40
using System.Linq.Expressions;
#else
using Microsoft.Linq.Expressions;
#endif
using Microsoft.Scripting.Utils;

namespace Microsoft.Scripting.Ast {
    public static partial class Utils {
        public static NewArrayExpression NewArrayHelper(Type type, IEnumerable<Expression> initializers) {
            ContractUtils.RequiresNotNull(type, "type");
            ContractUtils.RequiresNotNull(initializers, "initializers");

            if (type.Equals(typeof(void))) {
                throw new ArgumentException("Argument type cannot be System.Void.");
            }

            ReadOnlyCollection<Expression> initializerList = initializers.ToReadOnly();

            Expression[] clone = null;
            for (int i = 0; i < initializerList.Count; i++) {
                Expression initializer = initializerList[i];
                ContractUtils.RequiresNotNull(initializer, "initializers");

                if (!TypeUtils.AreReferenceAssignable(type, initializer.Type)) {
                    if (clone == null) {
                        clone = new Expression[initializerList.Count];
                        for (int j = 0; j < i; j++) {
                            clone[j] = initializerList[j];
                        }
                    }
                    if (type.IsSubclassOf(typeof(Expression)) && TypeUtils.AreAssignable(type, initializer.GetType())) {
                        initializer = Expression.Quote(initializer);
                    } else {
                        initializer = Convert(initializer, type);
                    }

                }
                if (clone != null) {
                    clone[i] = initializer;
                }
            }

            if (clone != null) {
                initializerList = new ReadOnlyCollection<Expression>(clone);
            }

            return Expression.NewArrayInit(type, initializerList);
        }
    }
}

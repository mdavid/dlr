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
#if CODEPLEX_40
using System.Dynamic;
using System.Linq.Expressions;
#else
using Microsoft.Scripting;
using Microsoft.Linq.Expressions;
#endif
using System.Reflection;

namespace Microsoft.Scripting.Actions.Calls {
#if CODEPLEX_40
    using Ast = System.Linq.Expressions.Expression;
#else
    using Ast = Microsoft.Linq.Expressions.Expression;
#endif

    /// <summary>
    /// Builds a parameter for a reference argument when a StrongBox has not been provided.  The
    /// updated return value is returned as one of the resulting return values.
    /// </summary>
    internal sealed class ReturnReferenceArgBuilder : SimpleArgBuilder {
        private ParameterExpression _tmp;

        public ReturnReferenceArgBuilder(ParameterInfo info, int index)
            : base(info, info.ParameterType.GetElementType(), index, false, false) {
        }

        internal protected override Expression ToExpression(OverloadResolver resolver, RestrictedArguments args, bool[] hasBeenUsed) {
            if (_tmp == null) {
                _tmp = resolver.GetTemporary(Type, "outParam");
            }

            return Ast.Block(Ast.Assign(_tmp, base.ToExpression(resolver, args, hasBeenUsed)), _tmp);
        }

        protected override SimpleArgBuilder Copy(int newIndex) {
            return new ReturnReferenceArgBuilder(ParameterInfo, newIndex);
        }

        internal override Expression ToReturnExpression(OverloadResolver resolver) {
            return _tmp;
        }

        internal override Expression ByRefArgument {
            get { return _tmp; }
        }

        public override int Priority {
            get {
                return 5;
            }
        }
    }
}

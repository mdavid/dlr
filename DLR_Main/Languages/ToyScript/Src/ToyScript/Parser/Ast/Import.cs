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

using Microsoft.Linq.Expressions;
using Microsoft.Scripting;
using AstUtils = Microsoft.Scripting.Ast.Utils;
using System; using Microsoft;

namespace ToyScript.Parser.Ast {
    class Import : Statement {
        private readonly string _name;

        public Import(SourceSpan span, string name)
            : base(span) {
            _name = name;
        }

        protected internal override Expression Generate(ToyGenerator tg) {
            Expression var = tg.GetOrMakeLocal(_name);
            throw new InvalidOperationException();
#if TODO
            return tg.AddSpan(
                Span,
                AstUtils.Assign(
                    var, 
                    Expression.Call(
                        typeof(ToyHelpers).GetMethod("Import"),
                        AstUtils.CodeContext(),
                        AstUtils.Constant(_name)
                    )
                )
            );
#endif
        }
    }
}

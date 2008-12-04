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
using Microsoft.Scripting;
using AstUtils = Microsoft.Scripting.Ast.Utils;

namespace ToyScript.Parser.Ast {
    class Return : Statement {
        private readonly ToyExpression _expression;

        public Return(SourceSpan span)
            : this(span, null) {
        }

        public Return(SourceSpan span, ToyExpression expression)
            : base(span) {
            _expression = expression;
        }

        protected internal override Expression Generate(ToyGenerator tg) {
            Expression value = _expression != null ? _expression.Generate(tg) : Expression.Constant(null, typeof(object));
            return tg.AddSpan(
                Span,
                Expression.Return(tg.Scope.ReturnLabel, AstUtils.Convert(value, typeof(object)))
            );
        }
    }
}

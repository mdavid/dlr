/* ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation. 
 *
 * This source code is subject to terms and conditions of the Microsoft Public License. A 
 * copy of the license can be found in the License.html file at the root of this distribution. If 
 * you cannot locate the  Microsoft Public License, please send an email to 
 * ironruby@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
 * by the terms of the Microsoft Public License.
 *
 * You must not remove this notice, or any other, from this software.
 *
 *
 * ***************************************************************************/
using System; using Microsoft;



using Microsoft.Scripting;
using Microsoft.Scripting.Utils;

namespace IronRuby.Compiler.Ast {
    using MSA = Microsoft.Linq.Expressions;

    public partial class NotExpression : Expression {
        private readonly Expression/*!*/ _expression;

        public Expression/*!*/ Expression {
            get { return _expression; }
        }

        public NotExpression(Expression/*!*/ expression, SourceSpan location) 
            : base(location) {
            Assert.NotNull(expression);

            _expression = expression;
        }

        internal override MSA.Expression/*!*/ TransformRead(AstGenerator/*!*/ gen) {
            return Methods.IsFalse.OpCall(AstFactory.Box(_expression.TransformRead(gen))); 
        }

        internal override Expression/*!*/ ToCondition() {
            _expression.ToCondition();
            return this;
        }
    }
}

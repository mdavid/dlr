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
using IronRuby.Runtime;

namespace IronRuby.Compiler.Ast {
#if CODEPLEX_40
    using Ast = System.Linq.Expressions.Expression;
#else
    using Ast = Microsoft.Linq.Expressions.Expression;
#endif
    using AstUtils = Microsoft.Scripting.Ast.Utils;
#if CODEPLEX_40
    using MSA = System.Linq.Expressions;
#else
    using MSA = Microsoft.Linq.Expressions;
#endif

    public partial class OrExpression : Expression {
        private readonly Expression/*!*/ _left;
        private readonly Expression/*!*/ _right;

        public Expression/*!*/ Left {
            get { return _left; }
        }

        public Expression/*!*/ Right {
            get { return _right; }
        }

        public OrExpression(Expression/*!*/ left, Expression/*!*/ right, SourceSpan location)
            : base(location) {
            Assert.NotNull(left, right);

            _left = left;
            _right = right;
        }

        internal override MSA.Expression/*!*/ TransformRead(AstGenerator/*!*/ gen) {
            return TransformRead(gen, _left.TransformRead(gen), _right.TransformRead(gen));
        }

        internal static MSA.Expression/*!*/ TransformRead(AstGenerator/*!*/ gen, MSA.Expression/*!*/ left, MSA.Expression/*!*/ right) {
            MSA.ParameterExpression temp;

            MSA.Expression result = AstUtils.CoalesceFalse(
                AstFactory.Box(left),
                AstFactory.Box(right),
                Methods.IsTrue,
                out temp
            );

            gen.CurrentScope.AddHidden(temp);
            return result;
        }

        internal override Expression/*!*/ ToCondition() {
            _left.ToCondition();
            _right.ToCondition();
            return this;
        }
    }
}

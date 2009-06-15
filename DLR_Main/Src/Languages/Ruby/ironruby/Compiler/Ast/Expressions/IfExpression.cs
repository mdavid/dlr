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


using System.Collections.Generic;
using Microsoft.Scripting;
using Microsoft.Scripting.Utils;
#if CODEPLEX_40
using MSA = System.Linq.Expressions;
#else
using MSA = Microsoft.Linq.Expressions;
#endif
using AstUtils = Microsoft.Scripting.Ast.Utils;

namespace IronRuby.Compiler.Ast {
#if CODEPLEX_40
    using Ast = System.Linq.Expressions.Expression;
#else
    using Ast = Microsoft.Linq.Expressions.Expression;
#endif

    public partial class IfExpression : Expression {
        private Expression/*!*/ _condition;
        private Statements _body;
        private List<ElseIfClause> _elseIfClauses;

        public Expression/*!*/ Condition {
            get { return _condition; }
        }

        public Statements Body {
            get { return _body; }
        }

        public List<ElseIfClause> ElseIfClauses {
            get { return _elseIfClauses; }
        }

        public IfExpression(Expression/*!*/ condition, Statements/*!*/ body, List<ElseIfClause>/*!*/ elseIfClauses, SourceSpan location)
            : base(location) {
            ContractUtils.RequiresNotNull(body, "body");
            ContractUtils.RequiresNotNull(condition, "condition");
            ContractUtils.RequiresNotNull(elseIfClauses, "elseIfClauses");

            // all but the last clause should have non-null conditions:
            for (int i = 0; i < elseIfClauses.Count - 1; i++) {
                if (elseIfClauses[i].Condition == null) {
                    throw ExceptionUtils.MakeArgumentItemNullException(i, "elseIfClauses");
                }
            }

            _condition = condition;
            _body = body;
            _elseIfClauses = elseIfClauses;
        }

        internal override MSA.Expression/*!*/ TransformRead(AstGenerator/*!*/ gen) {

            MSA.Expression result;

            int i = _elseIfClauses.Count - 1;

            if (i >= 0 && _elseIfClauses[i].Condition == null) {
                // ... else body end
                result = gen.TransformStatementsToExpression(_elseIfClauses[i].Statements);
                i--;
            } else {
                // no else clause => the result of the if-expression is nil:
                result = AstUtils.Constant(null);
            }

            while (i >= 0) {
                // emit: else (if (condition) body else result)
                result = AstFactory.Condition(
                    AstFactory.IsTrue(_elseIfClauses[i].Condition.TransformRead(gen)),
                    gen.TransformStatementsToExpression(_elseIfClauses[i].Statements),
                    result
                );
                i--;
            }

            // if (condition) body else result
            return AstFactory.Condition(
                AstFactory.IsTrue(_condition.TransformRead(gen)),
                gen.TransformStatementsToExpression(_body),
                result
            );
        }
    }
}

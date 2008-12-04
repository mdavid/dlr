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

namespace ToyScript.Parser.Ast {

    class Binary : ToyExpression {
        private readonly Operator _op;
        private readonly ToyExpression _left;
        private readonly ToyExpression _right;

        public Binary(SourceSpan span, Operator op, ToyExpression left, ToyExpression right)
            : base(span) {
            _op = op;
            _left = left;
            _right = right;
        }

        protected internal override Expression Generate(ToyGenerator tg) {
            Expression left = _left.Generate(tg);
            Expression right = _right.Generate(tg);

            switch (_op) {
                // Binary
                case Operator.Add: return tg.Add(left, right);
                case Operator.Subtract: return tg.Subtract(left, right);
                case Operator.Multiply: return tg.Multiply(left, right);
                case Operator.Divide: return tg.Divide(left, right);

                // Comparisons
                case Operator.LessThan: return tg.LessThan(left, right);
                case Operator.LessThanOrEqual: return tg.LessThanOrEqual(left, right);
                case Operator.GreaterThan: return tg.GreaterThan(left, right);
                case Operator.GreaterThanOrEqual: return tg.GreaterThanOrEqual(left, right);
                case Operator.Equals: return tg.Equal(left, right);
                case Operator.NotEquals: return tg.NotEqual(left, right);

                default:
                    throw new System.InvalidOperationException();
            }
        }
    }
}

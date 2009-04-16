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
using Microsoft.Scripting.Runtime;
using AstUtils = Microsoft.Scripting.Ast.Utils;

namespace ToyScript.Parser.Ast {
    class Index : ToyExpression {
        private readonly ToyExpression _target;
        private readonly ToyExpression _index;

        private static bool Helper = false;

        public Index(SourceSpan span, ToyExpression target, ToyExpression index)
            : base(span) {
            _target = target;
            _index = index;
        }

        protected internal override Expression Generate(ToyGenerator tg) {
            if (Helper) {
                return Expression.Call(
                    typeof(ToyHelpers).GetMethod("GetItem"),
                    AstUtils.Convert(_target.Generate(tg), typeof(object)),
                    AstUtils.Convert(_index.Generate(tg), typeof(object))
                );
            } else {
                return tg.Operator(Operators.GetItem, _target.Generate(tg), _index.Generate(tg));
            }
        }

        protected internal override Expression GenerateAssign(ToyGenerator tg, Expression right) {
            if (Helper) {
                return Expression.Call(
                    typeof(ToyHelpers).GetMethod("SetItem"),
                    AstUtils.Convert(_target.Generate(tg), typeof(object)),
                    AstUtils.Convert(_index.Generate(tg), typeof(object)),
                    AstUtils.Convert(right, typeof(object))
                );
            } else {
                return tg.SetItem(
                    _target.Generate(tg),
                    _index.Generate(tg),
                    right
                );
            }
        }
    }
}

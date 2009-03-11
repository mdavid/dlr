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
    class Var : Statement {
        private readonly string _name;
        private readonly ToyExpression _value;

        public Var(SourceSpan span, string name, ToyExpression value)
            : base(span) {
            _name = name;
            _value = value;
        }

        protected internal override Expression Generate(ToyGenerator tg) {
            Expression var = tg.GetOrMakeLocal(_name);

            if (_value != null) {
                return tg.AddSpan(
                    Span,
                    AstUtils.Assign(
                        var,
                        AstUtils.Convert(
                            _value.Generate(tg),
                            var.Type
                        )
                    )
                );
            } else {
                return tg.AddSpan(Span, AstUtils.Empty());
            }
        }
    }
}

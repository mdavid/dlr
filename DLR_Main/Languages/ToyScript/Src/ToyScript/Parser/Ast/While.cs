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
    class While : Statement {
        private readonly ToyExpression _test;
        private readonly Statement _body;

        public While(SourceSpan span, ToyExpression test, Statement body)
            : base(span) {
            _test = test;
            _body = body;
        }

        protected internal override Expression Generate(ToyGenerator tg) {
            // TODO: do we even need a span, since the test & body have their own spans?
            return tg.AddSpan(
                new SourceSpan(Span.Start, _test.End),
                AstUtils.Loop(
                    tg.ConvertTo(typeof(bool), _test.Generate(tg)),
                    null,
                    _body.Generate(tg),
                    null,
                    null,
                    null
                )
            );
        }
    }
}

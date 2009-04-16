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
    class New : ToyExpression {
        private readonly ToyExpression _target;
        private readonly ToyExpression[] _arguments;

        public New(SourceSpan span, ToyExpression target, ToyExpression[] arguments)
            : base(span) {
            _target = target;
            _arguments = arguments;
        }

        protected internal override Expression Generate(ToyGenerator tg) {
            Expression target = _target.Generate(tg);
            Expression[] arguments = new Expression[_arguments.Length];
            for (int i = 0; i < _arguments.Length; i++) {
                arguments[i] = _arguments[i].Generate(tg);
            }

            return tg.New(target, arguments);
        }
    }
}

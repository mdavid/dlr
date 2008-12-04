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
    class Named : ToyExpression {
        private readonly string _name;

        public Named(SourceSpan span, string name)
            : base(span) {
            _name = name;
        }

        protected internal override Expression Generate(ToyGenerator tg) {
            return GetVariable(tg);
        }

        protected internal override Expression GenerateAssign(ToyGenerator tg, Expression right) {
            Expression variable = GetVariable(tg);

            return AstUtils.Assign(
                variable,
                AstUtils.Convert(right, variable.Type)
            );
        }

        private Expression GetVariable(ToyGenerator tg) {
            Expression variable = tg.LookupName(_name);
            if (variable == null) {
                variable = tg.GetOrMakeGlobal(_name);
            }
            return variable;
        }
    }
}

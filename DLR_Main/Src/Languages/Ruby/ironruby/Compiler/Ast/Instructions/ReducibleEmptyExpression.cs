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
using AstUtils = Microsoft.Scripting.Ast.Utils;
using MSA = Microsoft.Linq.Expressions;

namespace IronRuby.Compiler.Ast {
    using Ast = Microsoft.Linq.Expressions.Expression;

    internal abstract class ReducibleEmptyExpression : MSA.Expression {
        public sealed override MSA.ExpressionType NodeType {
            get { return MSA.ExpressionType.Extension; }
        }

        public override Type/*!*/ Type {
            get { return typeof(void); }
        }

        public override bool CanReduce {
            get { return true; }
        }

        public override MSA.Expression/*!*/ Reduce() {
            return Ast.Empty();
        }

        protected override MSA.Expression VisitChildren(Func<MSA.Expression, MSA.Expression> visitor) {
            return this;
        }
    }
}

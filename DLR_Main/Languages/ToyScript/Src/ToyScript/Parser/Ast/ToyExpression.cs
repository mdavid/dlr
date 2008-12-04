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
    abstract class ToyExpression : ToyNode {
        protected ToyExpression(SourceSpan span)
            : base(span) {
        }

        internal protected abstract Expression Generate(ToyGenerator tg);

        internal protected virtual Expression GenerateAssign(ToyGenerator tg, Expression right) {
            throw new System.InvalidOperationException("Assignment to non-lvalue");
        }
    }
}

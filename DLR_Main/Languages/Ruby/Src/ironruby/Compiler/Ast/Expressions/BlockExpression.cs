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
using MSA = Microsoft.Linq.Expressions;
using System.Collections.Generic;
using System.Diagnostics;

namespace IronRuby.Compiler.Ast {

    // #{<statement>; ... ;<statement>}
    // (<statement>; ... ;<statement>)
    public partial class BlockExpression : Expression {
        internal static readonly BlockExpression Empty = new BlockExpression();

        private readonly List<Expression/*!*/>/*!*/ _statements;

        public List<Expression/*!*/>/*!*/ Statements {
            get { return _statements; }
        }

        private BlockExpression() 
            : base(SourceSpan.None) {
            _statements = EmptyList;            
        }
        
        internal BlockExpression(List<Expression/*!*/>/*!*/ statements, SourceSpan location)
            : base(location) {
            Assert.NotNull(statements);
            Debug.Assert(statements.Count > 1);

            _statements = statements;
        }

        internal override MSA.Expression/*!*/ TransformRead(AstGenerator/*!*/ gen) {
            return gen.TransformStatementsToExpression(_statements);
        }
    }
}

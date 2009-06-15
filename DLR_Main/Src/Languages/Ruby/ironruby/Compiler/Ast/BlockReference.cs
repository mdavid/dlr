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
using IronRuby.Builtins;
using IronRuby.Runtime.Calls;
#if CODEPLEX_40
using MSA = System.Linq.Expressions;
#else
using MSA = Microsoft.Linq.Expressions;
#endif

namespace IronRuby.Compiler.Ast {
#if CODEPLEX_40
    using Ast = System.Linq.Expressions.Expression;
#else
    using Ast = Microsoft.Linq.Expressions.Expression;
#endif
    
    public partial class BlockReference : Block {
        private readonly Expression/*!*/ _expression;

        public sealed override bool IsDefinition { get { return false; } }

        public Expression/*!*/ Expression {
            get { return _expression; }
        }

        public BlockReference(Expression/*!*/ expression, SourceSpan location)
            : base(location) {
            Assert.NotNull(expression);

            _expression = expression;
        }

        internal override MSA.Expression/*!*/ Transform(AstGenerator/*!*/ gen) {
            Assert.NotNull(gen);
            return Ast.Dynamic(ConvertToProcAction.Make(gen.Context), typeof(Proc), _expression.TransformRead(gen));
        }
    }
}

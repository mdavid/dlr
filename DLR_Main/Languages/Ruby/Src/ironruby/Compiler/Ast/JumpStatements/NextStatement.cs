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
using MSA = Microsoft.Linq.Expressions;
using AstUtils = Microsoft.Scripting.Ast.Utils;

namespace IronRuby.Compiler.Ast {
    using Ast = Microsoft.Linq.Expressions.Expression;

    public partial class NextStatement : JumpStatement {
        public NextStatement(Arguments arguments, SourceSpan location)
            : base(arguments, location) {
        }

        // see Ruby Language.doc/Runtime/Control Flow Implementation/Next
        internal override MSA.Expression/*!*/ Transform(AstGenerator/*!*/ gen) {

            MSA.Expression transformedReturnValue = TransformReturnValue(gen);

            // eval:
            if (gen.CompilerOptions.IsEval) {
                return Methods.EvalNext.OpCall(gen.CurrentRfcVariable, AstFactory.Box(transformedReturnValue));
            }
            
            // loop:
            if (gen.CurrentLoop != null) {
                return Ast.Block(
                    transformedReturnValue, // evaluate for side-effects
                    Ast.Continue(gen.CurrentLoop.ContinueLabel),
                    AstUtils.Empty()
                );
            }

            // block:
            if (gen.CurrentBlock != null) {
                return gen.Return(transformedReturnValue);
            }

            // method:
            return Methods.MethodNext.OpCall(gen.CurrentRfcVariable, AstFactory.Box(transformedReturnValue));
        }
    }
}

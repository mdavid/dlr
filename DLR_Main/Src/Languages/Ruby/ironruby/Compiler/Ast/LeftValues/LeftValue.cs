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


using System.Collections.Generic;
using Microsoft.Scripting;
#if CODEPLEX_40
using MSA = System.Linq.Expressions;
#else
using MSA = Microsoft.Linq.Expressions;
#endif

namespace IronRuby.Compiler.Ast {

    public abstract class LeftValue : Expression {
        internal static new readonly List<LeftValue>/*!*/ EmptyList = new List<LeftValue>();

        public LeftValue(SourceSpan location)
            : base(location) {
        }

        // Gets an expression that evaluates to the part of the left value that represents a holder (target) of the left value;
        // For example target.bar, target[key], ...
        // This target is passed to the TransformWrite by assignment expressions.
        // This is necessary to prevent redundant evaluation of the target expression in in-place assignment left op= right.
        // Returns null if the left value doesn't have target expression.
        internal abstract MSA.Expression TransformTargetRead(AstGenerator/*!*/ gen);
        
        internal sealed override MSA.Expression/*!*/ TransformRead(AstGenerator/*!*/ gen) {
            return TransformRead(gen, TransformTargetRead(gen), false);
        }
        
        internal MSA.Expression/*!*/ TransformWrite(AstGenerator/*!*/ gen, MSA.Expression/*!*/ rightValue) {
            return TransformWrite(gen, TransformTargetRead(gen), rightValue);
        }

        internal abstract MSA.Expression/*!*/ TransformRead(AstGenerator/*!*/ gen, MSA.Expression targetValue, bool tryRead);
        internal abstract MSA.Expression/*!*/ TransformWrite(AstGenerator/*!*/ gen, MSA.Expression targetValue, MSA.Expression/*!*/ rightValue);

        internal virtual List<LeftValue>/*!*/ ToList() {
            List<LeftValue> result = new List<LeftValue>();
            result.Add(this);
            return result;
        }
    }
}

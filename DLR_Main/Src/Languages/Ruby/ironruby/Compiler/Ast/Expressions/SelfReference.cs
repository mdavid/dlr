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

namespace IronRuby.Compiler.Ast {
    using MSA = Microsoft.Linq.Expressions;

    /// <summary>
    /// self
    /// </summary>
    public partial class SelfReference : Expression {

        public SelfReference(SourceSpan location)
            : base(location) {
        }

        internal override MSA.Expression/*!*/ TransformRead(AstGenerator/*!*/ gen) {
            return gen.CurrentSelfVariable;
        }

        internal override string/*!*/ GetNodeName(AstGenerator/*!*/ gen) {
            return "self";
        }

        internal override MSA.Expression/*!*/ TransformDefinedCondition(AstGenerator/*!*/ gen) {
            return null;
        }
    }
}

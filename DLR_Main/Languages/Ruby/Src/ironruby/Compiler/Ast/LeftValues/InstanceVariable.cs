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


using System.Diagnostics;
using Microsoft.Scripting;
using MSA = Microsoft.Linq.Expressions;
using AstUtils = Microsoft.Scripting.Ast.Utils;

namespace IronRuby.Compiler.Ast {
    using Ast = Microsoft.Linq.Expressions.Expression;

    public partial class InstanceVariable : Variable {
        public InstanceVariable(string/*!*/ name, SourceSpan location)
            : base(name, location) {
            Debug.Assert(name.StartsWith("@"));
        }

        internal override MSA.Expression/*!*/ TransformReadVariable(AstGenerator/*!*/ gen, bool tryRead) {
            return Methods.GetInstanceVariable.OpCall(gen.CurrentScopeVariable, gen.CurrentSelfVariable, AstUtils.Constant(Name));
        }

        internal override MSA.Expression/*!*/ TransformWriteVariable(AstGenerator/*!*/ gen, MSA.Expression/*!*/ rightValue) {
            return Methods.SetInstanceVariable.OpCall(gen.CurrentSelfVariable, AstFactory.Box(rightValue), gen.CurrentScopeVariable, AstUtils.Constant(Name));
        }

        internal override MSA.Expression TransformDefinedCondition(AstGenerator/*!*/ gen) {
            return Methods.IsDefinedInstanceVariable.OpCall(gen.CurrentScopeVariable, gen.CurrentSelfVariable, AstUtils.Constant(Name));
        }

        internal override string/*!*/ GetNodeName(AstGenerator/*!*/ gen) {
            return "instance-variable";
        }
    }
}

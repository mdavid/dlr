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
using AstUtils = Microsoft.Scripting.Ast.Utils;

namespace IronRuby.Compiler.Ast {
    using Ast = Microsoft.Linq.Expressions.Expression;
    using MSA = Microsoft.Linq.Expressions;

    // class Name
    //   <statements>
    // end
    public partial class ClassDeclaration : ModuleDeclaration {
        private readonly Expression _superClass;

        public Expression SuperClass {
            get { return _superClass; }
        }

        public ClassDeclaration(LexicalScope/*!*/ definedScope, ConstantVariable/*!*/ name, Expression superClass, Body/*!*/ body, SourceSpan location)
            : base(definedScope, name, body, location) {
            ContractUtils.RequiresNotNull(name, "name");
            
            _superClass = superClass;
        }

        internal override MSA.Expression/*!*/ MakeDefinitionExpression(AstGenerator/*!*/ gen) {
            MSA.Expression transformedQualifier;
            MSA.Expression name = QualifiedName.TransformName(gen);
            MSA.Expression transformedSuper = (_superClass != null) ? AstFactory.Box(_superClass.TransformRead(gen)) : AstUtils.Constant(null);

            switch (QualifiedName.TransformQualifier(gen, out transformedQualifier)) {
                case StaticScopeKind.Global:
                    return Methods.DefineGlobalClass.OpCall(gen.CurrentScopeVariable, name, transformedSuper);

                case StaticScopeKind.EnclosingModule:
                    return Methods.DefineNestedClass.OpCall(gen.CurrentScopeVariable, name, transformedSuper);

                case StaticScopeKind.Explicit:
                    return Methods.DefineClass.OpCall(gen.CurrentScopeVariable, transformedQualifier, name, transformedSuper);
            }

            throw Assert.Unreachable;
        }
    }
}

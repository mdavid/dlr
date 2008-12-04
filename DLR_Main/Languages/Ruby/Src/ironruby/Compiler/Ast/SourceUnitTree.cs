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
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Scripting;
using System.Text;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using IronRuby.Builtins;
using IronRuby.Runtime;
using IronRuby.Runtime.Calls;
using MSA = Microsoft.Linq.Expressions;
using AstUtils = Microsoft.Scripting.Ast.Utils;

namespace IronRuby.Compiler.Ast {
    using Ast = Microsoft.Linq.Expressions.Expression;

    public partial class SourceUnitTree : Node {

        private readonly LexicalScope/*!*/ _definedScope;
        private readonly List<Initializer> _initializers;
        private readonly List<Expression> _statements;
        private readonly Encoding/*!*/ _encoding;

        // An offset of the first byte after __END__ that can be read via DATA constant or -1 if __END__ is not present.
        private readonly int _dataOffset;

        public List<Initializer> Initializers {
            get { return _initializers; }
        }

        public List<Expression> Statements {
            get { return _statements; }
        }

        public Encoding/*!*/ Encoding {
            get { return _encoding; }
        }

        public SourceUnitTree(LexicalScope/*!*/ definedScope, List<Expression> statements, List<Initializer> initializers, 
            Encoding/*!*/ encoding, int dataOffset)
            : base(SourceSpan.None) {
            Assert.NotNull(definedScope, encoding);

            _definedScope = definedScope;
            _statements = statements;
            _initializers = initializers;
            _encoding = encoding;
            _dataOffset = dataOffset;
        }

        internal MSA.Expression<T>/*!*/ Transform<T>(AstGenerator/*!*/ gen) {
            Debug.Assert(gen != null);

            ScopeBuilder scope = new ScopeBuilder();

            MSA.ParameterExpression[] parameters;
            MSA.Expression selfVariable;
            MSA.Expression rfcVariable;
            MSA.Expression parentScope;
            MSA.Expression language;
            MSA.Expression runtimeScopeVariable;
            MSA.Expression moduleVariable;
            MSA.Expression blockParameter;
            MSA.Expression currentMethodVariable;

            if (gen.CompilerOptions.IsEval) {
                parameters = new MSA.ParameterExpression[6];

                parameters[0] = Ast.Parameter(typeof(RubyScope), "#scope");
                selfVariable = parameters[1] = Ast.Parameter(typeof(object), "#self");
                parameters[2] = Ast.Parameter(typeof(RubyModule), "#module");
                blockParameter = parameters[3] = Ast.Parameter(typeof(Proc), "#block");
                currentMethodVariable = parameters[4] = Ast.Parameter(typeof(RubyMethodInfo), "#method");
                rfcVariable = parameters[5] = Ast.Parameter(typeof(RuntimeFlowControl), "#rfc");

                if (gen.CompilerOptions.IsModuleEval) {
                    runtimeScopeVariable = scope.DefineHiddenVariable("#scope", typeof(RubyScope));
                    parentScope = parameters[0];
                    moduleVariable = parameters[2];
                } else {
                    runtimeScopeVariable = parameters[0];
                    moduleVariable = null;
                    parentScope = null;
                }

                language = null;
            } else {
                parameters = new MSA.ParameterExpression[2];
                parentScope = parameters[0] = Ast.Parameter(typeof(Scope), "#globalScope");
                language = parameters[1] = Ast.Parameter(typeof(LanguageContext), "#language");

                selfVariable = scope.DefineHiddenVariable("#self", typeof(object));
                rfcVariable = scope.DefineHiddenVariable("#rfc", typeof(RuntimeFlowControl));
                runtimeScopeVariable = scope.DefineHiddenVariable("#scope", typeof(RubyScope));
                blockParameter = null;
                currentMethodVariable = null;
                moduleVariable = null;
            }

            gen.EnterSourceUnit(
                scope,
                selfVariable,
                runtimeScopeVariable,
                blockParameter,
                rfcVariable,
                currentMethodVariable,
                gen.CompilerOptions.TopLevelMethodName, // method name
                null                                    // parameters
            );

            _definedScope.TransformLocals(scope);

            MSA.Expression scopeFactoryCall;

            if (gen.CompilerOptions.IsEval) {
                if (gen.CompilerOptions.IsModuleEval) {
                    scopeFactoryCall = Methods.CreateModuleEvalScope.OpCall(
                        scope.VisibleVariables(), parentScope, selfVariable, moduleVariable
                    );
                } else {
                    scopeFactoryCall = null;
                }
            } else if (!gen.CompilerOptions.IsIncluded) {
                scopeFactoryCall = Methods.CreateMainTopLevelScope.OpCall(scope.VisibleVariables(), parentScope, language, selfVariable, rfcVariable, 
                    Ast.Constant(gen.SourceUnit.Path, typeof(string)), Ast.Constant(_dataOffset));
            } else if (gen.CompilerOptions.IsWrapped) {
                scopeFactoryCall = Methods.CreateWrappedTopLevelScope.OpCall(scope.VisibleVariables(), parentScope, language, selfVariable, rfcVariable);
            } else {
                scopeFactoryCall = Methods.CreateTopLevelScope.OpCall(scope.VisibleVariables(), parentScope, language, selfVariable, rfcVariable);
            }

            MSA.Expression prologue, body;

            if (scopeFactoryCall != null) {
                prologue = Ast.Assign(runtimeScopeVariable, scopeFactoryCall);
            } else {
                prologue = null;
            }

            if (gen.SourceUnit.Kind == SourceCodeKind.InteractiveCode) {
                var resultVariable = scope.DefineHiddenVariable("#result", typeof(object));

                var epilogue = Methods.PrintInteractiveResult.OpCall(runtimeScopeVariable,
                    Ast.Dynamic(ConvertToSAction.Instance, typeof(MutableString), gen.CurrentScopeVariable, 
                        Ast.Dynamic(RubyCallAction.Make("inspect", RubyCallSignature.WithScope(0)), typeof(object), 
                            gen.CurrentScopeVariable, resultVariable
                        )
                    )
                );

                body = gen.TransformStatements(prologue, _statements, epilogue, ResultOperation.Store(resultVariable));
            } else {
                body = gen.TransformStatements(prologue, _statements, ResultOperation.Return);
            }

            body = gen.AddReturnTarget(scope.CreateScope(body));
            gen.LeaveSourceUnit();

            return Ast.Lambda<T>(
                body,
                RubyExceptionData.TopLevelMethodName,
                parameters
            );
        }
    }
}

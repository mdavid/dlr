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

#if CODEPLEX_40
using System;
#else
using System; using Microsoft;
#endif
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Scripting.Utils;
using System.Threading;
using Microsoft.Scripting.Ast;
using System.Runtime.CompilerServices;
#if !CODEPLEX_40
using Microsoft.Runtime.CompilerServices;
#endif

using Microsoft.Scripting;

namespace IronRuby.Compiler.Ast {
#if CODEPLEX_40
    using Ast = System.Linq.Expressions.Expression;
    using MSA = System.Linq.Expressions;
#else
    using Ast = Microsoft.Linq.Expressions.Expression;
    using MSA = Microsoft.Linq.Expressions;
#endif
    using IronRuby.Runtime;
    
    internal sealed class ScopeBuilder {
        private readonly MSA.ParameterExpression/*!*/[] _parameters;
        private readonly int _firstClosureParam;
        private readonly int _localCount;
        private readonly LexicalScope/*!*/ _lexicalScope;
        private int _liftedHiddenVariableCount;

        private readonly ReadOnlyCollectionBuilder<MSA.ParameterExpression>/*!*/ _hiddenVariables;

        // Local variables that hold on the closures used within this scope.
        // Closure #0 stores the parent scope's local variables, closure #1 stores grand parent scope's locals, etc.
        private List<MSA.ParameterExpression> _closures;

        // Stores this scope's local variables.
        private readonly MSA.ParameterExpression/*!*/ _localClosure;
        
#if DEBUG
        private static int _Id;
        private int _id;
#endif

        public ScopeBuilder(int localCount, LexicalScope/*!*/ lexicalScope)
            : this(null, -1, localCount, lexicalScope) {
        }

        public ScopeBuilder(MSA.ParameterExpression/*!*/[] parameters, int firstClosureParam, int localCount, LexicalScope/*!*/ lexicalScope) {
#if DEBUG
            _id = Interlocked.Increment(ref _Id);
#endif
            _parameters = parameters;
            _localCount = localCount;
            _firstClosureParam = firstClosureParam;
            _lexicalScope = lexicalScope;
            _hiddenVariables = new ReadOnlyCollectionBuilder<MSA.ParameterExpression>();
            _localClosure = DefineHiddenVariable("#locals", typeof(StrongBox<object>[]));
        }

        internal LexicalScope/*!*/ LexicalScope {
            get { return _lexicalScope; }
        }

        internal MSA.ParameterExpression/*!*/ GetClosure(int definitionDepth) {
            Debug.Assert(definitionDepth >= 0);
            
            int delta = _lexicalScope.Depth - definitionDepth - 1;
            if (delta == -1) {
                return _localClosure;
            }

            if (_closures == null) {
                _closures = new List<MSA.ParameterExpression>();
            }

            while (delta >= _closures.Count) {
                _closures.Add(DefineHiddenVariable("#closure" + delta, typeof(StrongBox<object>[])));
            }
            return _closures[delta];
        }

        public MSA.ParameterExpression/*!*/ DefineHiddenVariable(string/*!*/ name, Type/*!*/ type) {
#if DEBUG
            name += "_" + _id + "_" + _hiddenVariables.Count;
#endif
            return AddHidden(Ast.Variable(type, name));
        }

        public MSA.ParameterExpression/*!*/ AddHidden(MSA.ParameterExpression/*!*/ variable) {
            _hiddenVariables.Add(variable);
            return variable;
        }

        public int AddLiftedHiddenVariable() {
            return LiftedVisibleVariableCount + (_liftedHiddenVariableCount++);
        }

        private int LiftedVisibleVariableCount {
            get { return (_parameters != null ? _parameters.Length - _firstClosureParam : 0) + _localCount; }
        }

        public MSA.Expression/*!*/ MakeClosureDefinition() {
            var initializers = new ReadOnlyCollectionBuilder<MSA.Expression>();

            if (_parameters != null) {
                // parameters map to the initial elements of the array:
                for (int i = _firstClosureParam; i < _parameters.Length; i++) {
                    initializers.Add(Methods.CreateInitializedStrongBox.OpCall(_parameters[i]));
                }
            }

            for (int i = 0; i < _localCount + _liftedHiddenVariableCount; i++) {
                initializers.Add(Methods.CreateEmptyStrongBox.OpCall());
            }

            return Ast.Assign(_localClosure, Ast.NewArrayInit(typeof(StrongBox<object>), initializers));
        }

        public MSA.Expression/*!*/ GetVariableNamesExpression() {
            SymbolId[] symbols = new SymbolId[LiftedVisibleVariableCount];

            foreach (var var in _lexicalScope) {
                symbols[var.Value.ClosureIndex] = SymbolTable.StringToId(var.Value.Name);
            }

            return Ast.Constant(symbols);
        }

        public MSA.Expression/*!*/ CreateScope(MSA.Expression/*!*/ body) {
            Debug.Assert(_closures == null);
            return Ast.Block(_hiddenVariables, body);
        }

        public MSA.Expression/*!*/ CreateScope(MSA.Expression/*!*/ scopeVariable, MSA.Expression/*!*/ scopeInitializer, MSA.Expression/*!*/ body) {
            // #locals variable already assigned (in MakeClosureDefinition).
            // We need to initialize #closureN variables.
            if (_closures != null) {
                if (_closures.Count == 1) {
                    return Ast.Block(_hiddenVariables, Ast.Block(
                        Ast.Assign(_closures[0], Methods.GetParentClosure.OpCall(Ast.Assign(scopeVariable, scopeInitializer))),
                        body
                    ));
                } else {
                    var result = new ReadOnlyCollectionBuilder<MSA.Expression>();
                    MSA.Expression tempScope = DefineHiddenVariable("#s", typeof(RubyScope));
                    result.Add(Ast.Assign(scopeVariable, scopeInitializer));

                    for (int i = 0; i < _closures.Count; i++) {
                        result.Add(
                            Ast.Assign(
                                _closures[i],
                                Methods.GetClosure.OpCall(Ast.Assign(tempScope, Methods.GetParentScope.OpCall(i == 0 ? scopeVariable : tempScope)))
                            )
                        );
                    }
                    result.Add(body);
                    return Ast.Block(_hiddenVariables, result);
                }
            } else {
                return Ast.Block(_hiddenVariables, Ast.Block(Ast.Assign(scopeVariable, scopeInitializer), body));
            }
        }
    }
}

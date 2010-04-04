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

#if !CLR2
using System.Linq.Expressions;
#else
using Microsoft.Scripting.Ast;
#endif

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.Scripting.Interpreter {
    public sealed class LocalVariable {
        private const int IsBoxedFlag = 1;
        private const int InClosureFlag = 2;

        public readonly int Index;
        private int _flags;

        public bool IsBoxed {
            get { return (_flags & IsBoxedFlag) != 0; }
            set {
                if (value) {
                    _flags |= IsBoxedFlag;
                } else {
                    _flags &= ~IsBoxedFlag;
                }
            }
        }

        public bool InClosure {
            get { return (_flags & InClosureFlag) != 0; }
        }

        public bool InClosureOrBoxed {
            get { return InClosure | IsBoxed; }
        }

        internal LocalVariable(int index, bool closure, bool boxed) {
            Index = index;
            _flags = (closure ? InClosureFlag : 0) | (boxed ? IsBoxedFlag : 0);
        }

        internal Expression LoadFromArray(Expression frameData, Expression closure) {
            Expression result = Expression.ArrayAccess(InClosure ? closure : frameData, Expression.Constant(Index));
            return IsBoxed ? Expression.Convert(result, typeof(StrongBox<object>)) : result;
        }

        public override string ToString() {
            return String.Format("{0}: {1} {2}", Index, IsBoxed ? "boxed" : null, InClosure ? "in closure" : null);
        }
    }

    struct LocalDefinition {
        public int Index;
        public ParameterExpression Parameter;

        public LocalDefinition(int localIndex, ParameterExpression parameter) {
            Index = localIndex;
            Parameter = parameter;
        }
    }
        
    public sealed class LocalVariables {
        private readonly Dictionary<ParameterExpression, VariableScope> _variables = new Dictionary<ParameterExpression, VariableScope>();
        private Dictionary<ParameterExpression, LocalVariable> _closureVariables;

        private int _localCount, _maxLocalCount;

        internal LocalVariables() {
        }

        internal LocalDefinition DefineLocal(ParameterExpression variable, int start) {
            LocalVariable result = new LocalVariable(_localCount++, false, false);
            _maxLocalCount = System.Math.Max(_localCount, _maxLocalCount);

            VariableScope existing, newScope;
            if (_variables.TryGetValue(variable, out existing)) {
                newScope = new VariableScope(result, start, existing);
                if (existing.ChildScopes == null) {
                    existing.ChildScopes = new List<VariableScope>();
                }
                existing.ChildScopes.Add(newScope);
            } else {
                newScope = new VariableScope(result, start, null);
            }

            _variables[variable] = newScope;
            return new LocalDefinition(result.Index, variable);
        }

        internal void UndefineLocal(LocalDefinition definition, int end) {
            var scope = _variables[definition.Parameter];
            scope.Stop = end;
            if (scope.Parent != null) {
                _variables[definition.Parameter] = scope.Parent;
            } else {
                _variables.Remove(definition.Parameter);
            }
            
            _localCount--;
        }

        internal void Box(ParameterExpression variable, InstructionList instructions) {
            var scope = _variables[variable];

            LocalVariable local = scope.Variable;
            Debug.Assert(!local.IsBoxed && !local.InClosure);
            _variables[variable].Variable.IsBoxed = true;
                
            int curChild = 0;
            for (int i = scope.Start; i < scope.Stop && i < instructions.Count; i++) {
                if (scope.ChildScopes != null && scope.ChildScopes[curChild].Start == i) {
                    // skip boxing in the child scope
                    var child = scope.ChildScopes[curChild];
                    i = child.Stop;

                    curChild++;
                    continue;
                }

                instructions.SwitchToBoxed(local.Index, i);
            }
        }

        public int LocalCount {
            get { return _maxLocalCount; }
        }

        public int GetLocalIndex(ParameterExpression var) {
            VariableScope loc;
            return _variables.TryGetValue(var, out loc) ? loc.Variable.Index : -1;
        }

        public bool TryGetLocalOrClosure(ParameterExpression var, out LocalVariable local) {
            VariableScope scope;
            if (_variables.TryGetValue(var, out scope)) {
                local = scope.Variable;
                return true;
            }
            if (_closureVariables != null && _closureVariables.TryGetValue(var, out local)) {
                return true;
            }

            local = null;
            return false;
        }

        /// <summary>
        /// Gets a copy of the local variables which are defined in the current scope.
        /// </summary>
        /// <returns></returns>
        internal Dictionary<ParameterExpression, LocalVariable> CopyLocals() {
            var res = new Dictionary<ParameterExpression, LocalVariable>(_variables.Count);
            foreach (var keyValue in _variables) {
                res[keyValue.Key] = keyValue.Value.Variable;
            }
            return res;
        }

        /// <summary>
        /// Checks to see if the given variable is defined within the current local scope.
        /// </summary>
        internal bool ContainsVariable(ParameterExpression variable) {
            return _variables.ContainsKey(variable);
        }

        /// <summary>
        /// Gets the variables which are defined in an outer scope and available within the current scope.
        /// </summary>
        internal Dictionary<ParameterExpression, LocalVariable> ClosureVariables {
            get {
                return _closureVariables;
            }
        }
        
        internal LocalVariable AddClosureVariable(ParameterExpression variable) {
            if (_closureVariables == null) {
                _closureVariables = new Dictionary<ParameterExpression, LocalVariable>();
            }
            LocalVariable result = new LocalVariable(_closureVariables.Count, true, false);
            _closureVariables.Add(variable, result);
            return result;
        }

        /// <summary>
        /// Tracks where a variable is defined and what range of instructions it's used in
        /// </summary>
        private sealed class VariableScope {
            public readonly int Start;
            public int Stop = Int32.MaxValue;
            public readonly LocalVariable Variable;
            public readonly VariableScope Parent;
            public List<VariableScope> ChildScopes;

            public VariableScope(LocalVariable variable, int start, VariableScope parent) {
                Variable = variable;
                Start = start;
                Parent = parent;
            }
        }
    }
}

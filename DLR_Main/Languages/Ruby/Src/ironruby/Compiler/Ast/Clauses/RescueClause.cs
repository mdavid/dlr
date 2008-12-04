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
using Microsoft.Scripting.Ast;
using Microsoft.Scripting.Utils;
using AstUtils = Microsoft.Scripting.Ast.Utils;
using MSA = Microsoft.Linq.Expressions;

namespace IronRuby.Compiler.Ast {
    using Ast = Microsoft.Linq.Expressions.Expression;
    using System.Diagnostics;
    using IronRuby.Runtime.Calls;
    using System.Collections;

    // rescue type
    //   statements
    // rescue type => target
    //   statements
    // rescue types,*type-array
    //   statements
    // rescue types,*type-array => target
    //   statements
    public partial class RescueClause : Node {
        private readonly List<Expression>/*!*/ _types;   // might be empty
        private readonly Expression _splatType;          // optional
        private readonly LeftValue _target;		         // optional
        private readonly List<Expression> _statements;   // optional

        public List<Expression> Types {
            get { return _types; }
        }

        public LeftValue Target {
            get { return _target; }
        }

        public List<Expression> Statements {
            get { return _statements; }
        }

        public RescueClause(LeftValue target, List<Expression> statements, SourceSpan location)
            : base(location) {
            _target = target;
            _types = Expression.EmptyList;
            _statements = statements;
        }
        
        public RescueClause(CompoundRightValue type, LeftValue target, List<Expression> statements, SourceSpan location)
            : base(location) {
            _types = type.RightValues;
            _splatType = type.SplattedValue;
            _target = target;
            _statements = statements;
        }

        public RescueClause(Expression/*!*/ type, LeftValue target, List<Expression> statements, SourceSpan location)
            : base(location) {
            Assert.NotNull(type);
            _types = CollectionUtils.MakeList(type);
            _target = target;
            _statements = statements;
        }

        //
        // rescue stmts                     ... if (StandardError === $!) { stmts; } 
        // rescue <types> stmts             ... temp1 = type1; ...; if (<temp1> === $! || ...) { stmts; }
        // rescue <types> => <lvalue> stmts ... temp1 = type1; ...; if (<temp1> === $! || ...) { <lvalue> = $!; stmts; }
        // 
        internal IfStatementTest/*!*/ Transform(AstGenerator/*!*/ gen, ResultOperation resultOperation) {
            Assert.NotNull(gen);
            
            MSA.Expression condition;
            if (_types.Count != 0 || _splatType != null) {
                if (_types.Count == 0) {
                    // splat only:
                    condition = MakeCompareSplattedExceptions(gen, TransformSplatType(gen));
                } else if (_types.Count == 1 && _splatType == null) {
                    condition = MakeCompareException(gen, _types[0].TransformRead(gen));
                } else {

                    // forall{i}: <temps[i]> = evaluate type[i]
                    var temps = new MSA.Expression[_types.Count + (_splatType != null ? 1 : 0)];
                    var exprs = new MSA.Expression[temps.Length  + 1];
                    
                    int i = 0;
                    while (i < _types.Count) {
                        var tmp = gen.CurrentScope.DefineHiddenVariable("#type_" + i, typeof(object));
                        temps[i] = tmp;
                        exprs[i] = Ast.Assign(tmp, _types[i].TransformRead(gen));
                        i++;
                    }

                    if (_splatType != null) {
                        var tmp = gen.CurrentScope.DefineHiddenVariable("#type_" + i, typeof(object));
                        temps[i] = tmp;
                        exprs[i] = Ast.Assign(tmp, TransformSplatType(gen));

                        i++;
                    }

                    Debug.Assert(i == temps.Length);

                    // CompareException(<temps[0]>) || ... CompareException(<temps[n]>) || CompareSplattedExceptions(<splatTypes>)
                    i = 0;
                    condition = MakeCompareException(gen, temps[i++]);
                    while (i < _types.Count) {
                        condition = Ast.OrElse(condition, MakeCompareException(gen, temps[i++]));
                    }

                    if (_splatType != null) {
                        condition = Ast.OrElse(condition, MakeCompareSplattedExceptions(gen, temps[i++]));
                    }

                    Debug.Assert(i == temps.Length);

                    // (temps[0] = type[0], ..., temps[n] == type[n], condition)
                    exprs[exprs.Length - 1] = condition;
                    condition = AstFactory.Block(exprs);
                }

            } else {
                condition = Methods.CompareDefaultException.OpCall(gen.CurrentScopeVariable, gen.CurrentSelfVariable);
            }

            return AstUtils.IfCondition(condition,
                gen.TransformStatements(
                    // <lvalue> = e;
                    (_target != null) ? _target.TransformWrite(gen, Methods.GetCurrentException.OpCall(gen.CurrentScopeVariable)) : null,

                    // body:
                    _statements,

                    resultOperation
                )
            );
        }

        private MSA.Expression/*!*/ TransformSplatType(AstGenerator/*!*/ gen) {
            return Ast.Dynamic(
                TryConvertToArrayAction.Instance,
                typeof(object),
                gen.CurrentScopeVariable,
                _splatType.TransformRead(gen)
            );
        }

        private MSA.Expression/*!*/ MakeCompareException(AstGenerator/*!*/ gen, MSA.Expression/*!*/ expression) {
            return Methods.CompareException.OpCall(gen.CurrentScopeVariable, AstFactory.Box(expression));
        }

        private MSA.Expression/*!*/ MakeCompareSplattedExceptions(AstGenerator/*!*/ gen, MSA.Expression/*!*/ expression) {
            return Methods.CompareSplattedExceptions.OpCall(gen.CurrentScopeVariable, expression);
        }
    }
}

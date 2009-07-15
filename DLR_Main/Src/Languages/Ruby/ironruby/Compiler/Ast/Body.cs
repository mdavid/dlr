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
using Microsoft.Scripting;
using Microsoft.Scripting.Ast;
using Microsoft.Scripting.Utils;
using IronRuby.Runtime;
using AstUtils = Microsoft.Scripting.Ast.Utils;
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

    // begin/class/def/module/class << {expression}
    //   statements
    // rescue-clauses 
    // else
    //   statements
    // ensure
    //   statements
    // end
    public partial class Body : Expression {
        private readonly Statements/*!*/ _statements;
        private readonly List<RescueClause> _rescueClauses;	// optional
        private readonly Statements _elseStatements;	// optional
        private readonly Statements _ensureStatements;	// optional

        // TODO: readonly
        public Statements/*!*/ Statements { get { return _statements; } }
        public List<RescueClause> RescueClauses { get { return _rescueClauses; } }
        public Statements ElseStatements { get { return _elseStatements; } }
        public Statements EnsureStatements { get { return _ensureStatements; } }

        private bool HasExceptionHandling {
            get {
                return (_statements.Count > 0 && _rescueClauses != null) || _elseStatements != null || _ensureStatements != null;
            }
        }

        public Body(Statements/*!*/ statements, List<RescueClause> rescueClauses, Statements elseStatements,
            Statements ensureStatements, SourceSpan location)
            : base(location) {

            Assert.NotNull(statements);

            _statements = statements;
            _rescueClauses = rescueClauses;
            _elseStatements = elseStatements;
            _ensureStatements = ensureStatements;
        }

        internal override MSA.Expression/*!*/ TransformRead(AstGenerator/*!*/ gen) {
            Assert.NotNull(gen);

            if (HasExceptionHandling) {
                MSA.Expression resultVariable = gen.CurrentScope.DefineHiddenVariable("#block-result", typeof(object));

                return AstFactory.Block(
                    TransformExceptionHandling(gen, ResultOperation.Store(resultVariable)),
                    resultVariable
                );

            } else {
                return gen.TransformStatementsToExpression(_statements);
            }
        }

        internal override MSA.Expression/*!*/ TransformResult(AstGenerator/*!*/ gen, ResultOperation resultOperation) {
            Assert.NotNull(gen);

            if (HasExceptionHandling) {
                return TransformExceptionHandling(gen, resultOperation);
            } else {
                return gen.TransformStatements(_statements, resultOperation);
            }
        }

        private MSA.Expression/*!*/ TransformExceptionHandling(AstGenerator/*!*/ gen, ResultOperation resultOperation) {
            Assert.NotNull(gen);

            MSA.Expression exceptionThrownVariable = gen.CurrentScope.DefineHiddenVariable("#exception-thrown", typeof(bool));
            MSA.ParameterExpression exceptionVariable = gen.CurrentScope.DefineHiddenVariable("#exception", typeof(Exception));
            MSA.Expression exceptionRethrowVariable = gen.CurrentScope.DefineHiddenVariable("#exception-rethrow", typeof(bool));
            MSA.Expression retryingVariable = gen.CurrentScope.DefineHiddenVariable("#retrying", typeof(bool));
            MSA.ParameterExpression evalUnwinder = gen.CurrentScope.DefineHiddenVariable("#unwinder", typeof(EvalUnwinder));
            MSA.Expression oldExceptionVariable = gen.CurrentScope.DefineHiddenVariable("#old-exception", typeof(Exception));

            MSA.Expression transformedBody;
            MSA.Expression transformedEnsure;
            MSA.Expression transformedElse;

            if (_ensureStatements != null) {
                transformedEnsure = Ast.Block(
                    // ensure:
                    Ast.Assign(oldExceptionVariable, Methods.GetCurrentException.OpCall(gen.CurrentScopeVariable)),
                    gen.TransformStatements(_ensureStatements, ResultOperation.Ignore),
                    Methods.SetCurrentException.OpCall(gen.CurrentScopeVariable, oldExceptionVariable),

                    // rethrow:
                    AstUtils.IfThen(
                        Ast.AndAlso(
                            exceptionRethrowVariable,
                            Ast.NotEqual(oldExceptionVariable, AstUtils.Constant(null))
                        ),
                        Ast.Throw(oldExceptionVariable)
                    )
                );
            } else {
                // rethrow:
                transformedEnsure = AstUtils.IfThen(
                    Ast.AndAlso(
                        exceptionRethrowVariable,
                        Ast.NotEqual(
                            Ast.Assign(oldExceptionVariable, Methods.GetCurrentException.OpCall(gen.CurrentScopeVariable)),
                            AstUtils.Constant(null, typeof(Exception)))
                        ),
                    Ast.Throw(oldExceptionVariable)
                );
            }

            if (_elseStatements != null) {
                transformedElse = gen.TransformStatements(_elseStatements, resultOperation);
            } else {
                transformedElse = AstUtils.Empty();
            }

            // body should do return, but else-clause is present => we cannot do return from the guarded statements: 
            // (the value of the last expression in the body cannot be the last executed expression statement => we can ignore it):
            transformedBody = gen.TransformStatements(_statements, (_elseStatements != null) ? ResultOperation.Ignore : resultOperation);

            MSA.Expression enterRescue = null, leaveRescue = null;
            var retryLabel = Ast.Label("retry");

            // make rescue clause:
            MSA.Expression transformedRescue;
            if (_rescueClauses != null) {
                // outer-most EH blocks sets and clears runtime flag RuntimeFlowControl.InTryRescue:
                if (gen.CurrentRescue == null) {
                    enterRescue = Methods.EnterRescue.OpCall(gen.CurrentScopeVariable);
                    leaveRescue = Methods.LeaveRescue.OpCall(gen.CurrentScopeVariable);
                } else {
                    enterRescue = leaveRescue = AstUtils.Empty();
                }

                gen.EnterRescueClause(retryingVariable, retryLabel);

                var handlers = new IfStatementTest[_rescueClauses.Count];
                for (int i = 0; i < handlers.Length; i++) {
                    handlers[i] = _rescueClauses[i].Transform(gen, resultOperation);
                }

                transformedRescue =
                    AstUtils.Try(
                        enterRescue,
                        AstUtils.If(handlers, Ast.Assign(exceptionRethrowVariable, AstUtils.Constant(true)))
                    ).Filter(evalUnwinder, Ast.Equal(Ast.Field(evalUnwinder, EvalUnwinder.ReasonField), AstUtils.Constant(BlockReturnReason.Retry)),
                        Ast.Block(
                            Ast.Assign(retryingVariable, AstUtils.Constant(true)),
                            Ast.Continue(retryLabel),
                            AstUtils.Empty()
                        )
                    );
                

                gen.LeaveRescueClause();

            } else {
                transformedRescue = Ast.Assign(exceptionRethrowVariable, AstUtils.Constant(true));
            }

            if (_elseStatements != null) {
                transformedElse = AstUtils.Unless(exceptionThrownVariable, transformedElse);
            }

            var result = Ast.Block(
                Ast.Label(retryLabel),
                AstUtils.Try(
                    Ast.Assign(exceptionThrownVariable, AstUtils.Constant(false)),
                    Ast.Assign(exceptionRethrowVariable, AstUtils.Constant(false)),
                    Ast.Assign(retryingVariable, AstUtils.Constant(false)),

                    // save exception (old_$! is not used unless there is a rescue clause):
                    (_rescueClauses == null) ? (MSA.Expression)AstUtils.Empty() :
                        Ast.Assign(oldExceptionVariable, Methods.GetCurrentException.OpCall(gen.CurrentScopeVariable)),

                    AstUtils.Try(
                        Ast.Block(transformedBody, AstUtils.Empty())
                    ).Filter(exceptionVariable, Methods.CanRescue.OpCall(gen.CurrentScopeVariable, exceptionVariable),
                        Ast.Assign(exceptionThrownVariable, AstUtils.Constant(true)),
                        transformedRescue,
                        AstUtils.Empty()
                    ).FinallyIf((_rescueClauses != null), 
                        // restore previous exception if the current one has been handled:
                        AstUtils.Unless(exceptionRethrowVariable,
                            Methods.SetCurrentException.OpCall(gen.CurrentScopeVariable, oldExceptionVariable)
                        ),
                        leaveRescue
                    ),

                    // unless (exception_thrown) do <else-statements> end
                    transformedElse,
                    AstUtils.Empty()
                ).FilterIf((_rescueClauses != null || _elseStatements != null),
                    exceptionVariable, Methods.CanRescue.OpCall(gen.CurrentScopeVariable, exceptionVariable),
                    Ast.Assign(exceptionRethrowVariable, AstUtils.Constant(true)),
                    AstUtils.Empty()
                ).FinallyWithJumps(
                    AstUtils.Unless(retryingVariable, transformedEnsure)
                )
            );

            return result;
        }

        internal override Expression/*!*/ ToCondition(LexicalScope/*!*/ currentScope) {
            // propagates 'in condition' property if we have a single element:
            if (_statements != null && _statements.Count == 1 && !HasExceptionHandling) {
                return _statements.First.ToCondition(currentScope);
            }

            return this;
        }
    }
}

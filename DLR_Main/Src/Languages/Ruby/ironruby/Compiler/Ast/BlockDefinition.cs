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

#if !CLR2
using MSA = System.Linq.Expressions;
#else
using MSA = Microsoft.Scripting.Ast;
#endif

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using IronRuby.Builtins;
using IronRuby.Runtime;
using IronRuby.Runtime.Calls;
using Microsoft.Scripting;
using Microsoft.Scripting.Utils;
using AstUtils = Microsoft.Scripting.Ast.Utils;

namespace IronRuby.Compiler.Ast {
    using Ast = MSA.Expression;
    
    public partial class BlockDefinition : Block {
        //	{ |args| body }

        // self, block flow control:
        internal const int HiddenParameterCount = BlockDispatcher.HiddenParameterCount;

        // if the block has more parameters object[] is used:
        internal const int MaxBlockArity = BlockDispatcher.MaxBlockArity;

        private readonly CompoundLeftValue/*!*/ _parameters;
        private readonly Statements/*!*/ _body;
        private readonly LexicalScope/*!*/ _definedScope;

        public CompoundLeftValue/*!*/ Parameters {
            get { return _parameters; }
        }

        public Statements/*!*/ Body {
            get { return _body; }
        }

        public sealed override bool IsDefinition { get { return true; } }

        private bool HasFormalParametersInArray {
            get { return _parameters.LeftValues.Count > MaxBlockArity || HasUnsplatParameter; }
        }

        private bool HasUnsplatParameter {
            get { return _parameters.UnsplattedValue != null; }
        }

        public bool HasSignature {
            get { return _parameters != CompoundLeftValue.UnspecifiedBlockSignature; }
        }

        public BlockDefinition(LexicalScope/*!*/ definedScope, CompoundLeftValue/*!*/ parameters, Statements/*!*/ body, SourceSpan location)
            : base(location) {
            Assert.NotNull(definedScope, parameters, body);

            _definedScope = definedScope;
            _body = body;
            _parameters = parameters;
        }

        private MSA.ParameterExpression[]/*!*/ DefineParameters(out MSA.Expression/*!*/ selfVariable, out MSA.Expression/*!*/ blockParamVariable) {
            var parameters = new MSA.ParameterExpression[
                HiddenParameterCount +
                (HasFormalParametersInArray ? 1 : _parameters.LeftValues.Count) + 
                (HasUnsplatParameter ? 1 : 0)
            ];

            // hidden parameters:
            // #proc must be the first one - it is used as instance target for method invocation:
            blockParamVariable = parameters[0] = Ast.Parameter(typeof(BlockParam), "#proc");
            selfVariable = parameters[1] = Ast.Parameter(typeof(object), "#self");

            int i = HiddenParameterCount;
            if (HasFormalParametersInArray) {
                parameters[i++] = Ast.Parameter(typeof(object[]), "#parameters");
            } else {
                for (; i < parameters.Length; i++) {
                    parameters[i] = Ast.Parameter(typeof(object), "#" + (i - HiddenParameterCount));
                }
            }

            if (HasUnsplatParameter) {
                parameters[i] = Ast.Parameter(typeof(RubyArray), "#array");
            }

            return parameters;
        }

        private ScopeBuilder/*!*/ DefineLocals(ScopeBuilder/*!*/ parentBuilder) {
            return new ScopeBuilder(_definedScope.AllocateClosureSlotsForLocals(0), parentBuilder, _definedScope);
        }

        internal override MSA.Expression/*!*/ Transform(AstGenerator/*!*/ gen) {
            ScopeBuilder scope = DefineLocals(gen.CurrentScope);

            // define hidden parameters and RHS-placeholders (#1..#n will be used as RHS of a parallel assignment):
            MSA.Expression blockParameter, selfParameter;
            MSA.ParameterExpression[] parameters = DefineParameters(out selfParameter, out blockParameter);

            MSA.ParameterExpression scopeVariable = scope.DefineHiddenVariable("#scope", typeof(RubyBlockScope));
            MSA.LabelTarget redoLabel = Ast.Label();

            gen.EnterBlockDefinition(
                scope,
                blockParameter,
                selfParameter,
                scopeVariable,
                redoLabel
            );

            MSA.Expression paramInit = MakeParametersInitialization(gen, parameters);
            MSA.ParameterExpression blockUnwinder, filterVariable;

            MSA.Expression traceCall, traceReturn;
			if (gen.TraceEnabled) {
                int firstStatementLine = _body.Count > 0 ? _body.First.Location.Start.Line : Location.End.Line;
                int lastStatementLine = _body.Count > 0 ? _body.Last.Location.End.Line : Location.End.Line;

                traceCall = Methods.TraceBlockCall.OpCall(scopeVariable, blockParameter, gen.SourcePathConstant, AstUtils.Constant(firstStatementLine));
                traceReturn = Methods.TraceBlockReturn.OpCall(scopeVariable, blockParameter, gen.SourcePathConstant, AstUtils.Constant(lastStatementLine));
            } else {
                traceCall = traceReturn = Ast.Empty();
            }

            MSA.Expression body = AstUtils.Try(
                paramInit,
                traceCall,
                Ast.Label(redoLabel),
                AstUtils.Try(
                    gen.TransformStatements(_body, ResultOperation.Return)
                ).Catch(blockUnwinder = Ast.Parameter(typeof(BlockUnwinder), "#u"),
                    // redo:
                    AstUtils.IfThen(Ast.Field(blockUnwinder, BlockUnwinder.IsRedoField), Ast.Goto(redoLabel)),

                    // next:
                    gen.Return(Ast.Field(blockUnwinder, BlockUnwinder.ReturnValueField))
                )
            ).Filter(filterVariable = Ast.Parameter(typeof(Exception), "#e"),
                Methods.FilterBlockException.OpCall(scopeVariable, filterVariable)
            ).Finally(
                traceReturn,
                Ast.Empty()
            );

            body = gen.AddReturnTarget(
                scope.CreateScope(
                    scopeVariable,
                    Methods.CreateBlockScope.OpCall(
                        scope.MakeLocalsStorage(),
                        scope.GetVariableNamesExpression(),
                        blockParameter, 
                        selfParameter,
                        EnterInterpretedFrameExpression.Instance
                    ),
                    body
                )
            );

            gen.LeaveBlockDefinition();

            int parameterCount = _parameters.LeftValues.Count;
            var attributes = _parameters.GetBlockSignatureAttributes();

            var dispatcher = Ast.Constant(
                BlockDispatcher.Create(parameterCount, attributes, gen.SourcePath, Location.Start.Line), typeof(BlockDispatcher)
            );

            return Ast.Coalesce(
                Methods.InstantiateBlock.OpCall(gen.CurrentScopeVariable, gen.CurrentSelfVariable, dispatcher),
                Methods.DefineBlock.OpCall(gen.CurrentScopeVariable, gen.CurrentSelfVariable, dispatcher,
                    BlockDispatcher.CreateLambda(
                        body,
                        RubyExceptionData.EncodeMethodName(gen.CurrentMethod.MethodName, gen.SourcePath, Location),
                        new ReadOnlyCollection<MSA.ParameterExpression>(parameters),
                        parameterCount,
                        attributes
                    )
                )
            );
        }

        private MSA.Expression/*!*/ MakeParametersInitialization(AstGenerator/*!*/ gen, MSA.Expression[]/*!*/ parameters) {
            Assert.NotNull(gen);
            Assert.NotNullItems(parameters);

            var result = AstFactory.CreateExpressionArray(
                _parameters.LeftValues.Count + 
                (_parameters.UnsplattedValue != null ? 1 : 0) +
                1
            );

            int resultIndex = 0;

            bool paramsInArray = HasFormalParametersInArray;
            for (int i = 0; i < _parameters.LeftValues.Count; i++) {
                var parameter = paramsInArray ?
                    Ast.ArrayAccess(parameters[HiddenParameterCount], AstUtils.Constant(i)) :
                    parameters[HiddenParameterCount + i];

                result[resultIndex++] = _parameters.LeftValues[i].TransformWrite(gen, parameter);
            }

            if (_parameters.UnsplattedValue != null) {
                // the last parameter is unsplat:
                var parameter = parameters[parameters.Length - 1];
                result[resultIndex++] = _parameters.UnsplattedValue.TransformWrite(gen, parameter);
            }

            result[resultIndex++] = AstUtils.Empty();
            Debug.Assert(resultIndex == result.Length);
            return AstFactory.Block(result);
        }
    }
}

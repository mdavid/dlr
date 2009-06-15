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
using System.Diagnostics;
using Microsoft.Scripting;
using Microsoft.Scripting.Actions;
using Microsoft.Scripting.Utils;
using IronRuby.Runtime;
using AstUtils = Microsoft.Scripting.Ast.Utils;

namespace IronRuby.Compiler.Ast {
#if CODEPLEX_40
    using Ast = System.Linq.Expressions.Expression;
	using MSA = System.Linq.Expressions;
#else
    using Ast = Microsoft.Linq.Expressions.Expression;
	using MSA = Microsoft.Linq.Expressions;
#endif

    public partial class Arguments : Node {
        internal static readonly Arguments Empty = new Arguments(SourceSpan.None);

        private readonly Expression[] _expressions;
        private readonly List<Maplet> _maplets;
        private readonly Expression _array;
        
        public bool IsEmpty {
            get { return _expressions == null && _maplets == null && _array == null; }
        }

        public Expression[] Expressions { get { return _expressions; } }
        public List<Maplet> Maplets { get { return _maplets; } }
        public Expression Array { get { return _array; } }
        
        public Arguments(SourceSpan location)
            : base(location) {
            _expressions = null;
            _maplets = null;
            _array = null;
        }

        public Arguments(Expression/*!*/ arg)
            : base(arg.Location) {
            ContractUtils.RequiresNotNull(arg, "arg");

            _expressions = new Expression[] { arg };
            _maplets = null;
            _array = null;
        }

        public Arguments(Expression/*!*/[] arguments, List<Maplet/*!*/> maplets, Expression array, SourceSpan location)
            : base(location) {
            if (arguments != null) {
                Assert.NotNullItems(arguments);
            }
            if (maplets != null) {
                Assert.NotNullItems(maplets);
            }

            _expressions = arguments;
            _maplets = maplets;
            _array = array;
        }

        #region Transform to Call Expression

        private List<MSA.Expression>/*!*/ MakeSimpleArgumentExpressionList(AstGenerator/*!*/ gen) {
            var args = new List<MSA.Expression>();

            if (_expressions != null) {
                gen.TranformExpressions(_expressions, args);
            }

            if (_maplets != null) {
                args.Add(gen.TransformToHashConstructor(_maplets));
            }

            return args;
        }

        internal void TransformToCall(AstGenerator/*!*/ gen, CallBuilder callBuilder) {
            if (_expressions != null) {
                foreach (var arg in _expressions) {
                    callBuilder.Add(arg.TransformRead(gen));
                }
            }

            if (_maplets != null) {
                callBuilder.Add(gen.TransformToHashConstructor(_maplets));
            }

            if (_array != null) {
                callBuilder.SplattedArgument = _array.TransformRead(gen);
            }
        }

        #endregion

        #region Transform To Yield

        internal MSA.Expression/*!*/ TransformToYield(AstGenerator/*!*/ gen, MSA.Expression/*!*/ bfcVariable, MSA.Expression/*!*/ selfExpression) {
            var args = (_expressions != null) ? gen.TranformExpressions(_expressions) : new List<MSA.Expression>();

            if (_maplets != null) {
                args.Add(gen.TransformToHashConstructor(_maplets));
            }

            return AstFactory.YieldExpression(
                args,
                (_array != null) ? _array.TransformRead(gen) : null,         // splatted argument
                null,                                                        // rhs argument
                bfcVariable,
                selfExpression
            );
        }

        #endregion

        #region Transform Read

        internal MSA.Expression/*!*/ TransformRead(AstGenerator/*!*/ gen, bool doSplat) {
            Assert.NotNull(gen);
            return TransformRead(gen, 
                MakeSimpleArgumentExpressionList(gen), 
                (_array != null) ? _array.TransformRead(gen) : null,
                doSplat
            );
        }

        internal static MSA.Expression/*!*/ TransformRead(AstGenerator/*!*/ gen, List<MSA.Expression>/*!*/ rightValues, 
            MSA.Expression splattedValue, bool doSplat) {

            Assert.NotNull(gen, rightValues);

            MSA.Expression result;

            // We need to distinguish various special cases here.
            // For parallel assignment specification, see "Ruby Language.docx/Runtime/Parallel Assignment".
            
            // R(0,*)?
            bool rightNoneSplat = rightValues.Count == 0 && splattedValue != null;

            // R(1,-)?
            bool rightOneNone = rightValues.Count == 1 && splattedValue == null;

            if (rightNoneSplat) {
                result = (doSplat ? Methods.Splat : Methods.Unsplat).OpCall(AstFactory.Box(splattedValue));
            } else if (rightOneNone && doSplat) {
                result = rightValues[0];
            } else {
                result = Methods.MakeArrayOpCall(rightValues);

                if (splattedValue != null) {
                    result = Methods.SplatAppend.OpCall(result, AstFactory.Box(splattedValue));
                }
            }
            return result;
        }

        #endregion

        #region Tranform to Return Value

        private MSA.Expression/*!*/ TransformToReturnValue(AstGenerator/*!*/ gen) {
            Assert.NotNull(gen);
            
            if (_maplets != null && _expressions == null && _array == null) {
                return gen.TransformToHashConstructor(_maplets);
            }

            return TransformRead(gen, true /* Splat */);
        }

        internal static MSA.Expression/*!*/ TransformToReturnValue(AstGenerator/*!*/ gen, Arguments arguments) {
            return (arguments != null) ? arguments.TransformToReturnValue(gen) : AstUtils.Constant(null);
        }

        #endregion

        #region Transform To Array

        internal static MSA.Expression/*!*/ TransformToArray(AstGenerator/*!*/ gen, Arguments arguments) {
            Assert.NotNull(gen);
            return (arguments != null) ? arguments.TransformRead(gen, false /* Unsplat */) : Methods.MakeArray0.OpCall();
        }

        #endregion
    }
}

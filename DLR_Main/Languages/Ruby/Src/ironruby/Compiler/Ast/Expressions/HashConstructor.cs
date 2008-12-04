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
using Microsoft.Scripting.Utils;
using MSA = Microsoft.Linq.Expressions;

namespace IronRuby.Compiler.Ast {
    using Ast = Microsoft.Linq.Expressions.Expression;

    public partial class HashConstructor : Expression {
        // { key1 => value1, key2 => value2, ... }
        // or
        // { key1, value1, key2, value2, ... }

        private readonly List<Maplet> _maplets;
        private readonly List<Expression> _expressions;

        public List<Maplet> Maplets {
            get { return _maplets; }
        }

        public List<Expression> Expressions {
            get { return _expressions; }
        }

        public HashConstructor(List<Maplet> maplets, List<Expression> expressions, SourceSpan location)
            : base(location) {
            ContractUtils.Requires(maplets == null || expressions == null);
            ContractUtils.Requires(expressions == null || expressions.Count % 2 == 0, "expressions");

            _maplets = maplets;
            _expressions = expressions;
        }

        internal override MSA.Expression/*!*/ TransformRead(AstGenerator/*!*/ gen) {
            Assert.NotNull(gen);

            if (_maplets != null) {
                return gen.MakeHashOpCall(gen.TransformMapletsToExpressions(_maplets));
            } else if (_expressions != null) {
                return gen.MakeHashOpCall(gen.TranformExpressions(_expressions));
            } else {
                return Methods.MakeHash0.OpCall(gen.CurrentScopeVariable);
            }
        }
    }
}

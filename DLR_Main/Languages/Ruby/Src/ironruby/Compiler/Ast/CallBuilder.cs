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
using IronRuby.Runtime.Calls;
using IronRuby.Runtime;
using Microsoft.Scripting.Utils;

namespace IronRuby.Compiler.Ast {
    using Ast = Microsoft.Linq.Expressions.Expression;
    using MSA = Microsoft.Linq.Expressions;

    /// <summary>
    /// Simple helper for building up method call actions.
    /// </summary>
    internal class CallBuilder {
        private readonly AstGenerator/*!*/ _gen;

        private readonly List<MSA.Expression>/*!*/ _args = new List<MSA.Expression>();
        
        // TODO:
        public MSA.Expression Instance;
        public MSA.Expression SplattedArgument;
        public MSA.Expression Block;
        public MSA.Expression RhsArgument;

        internal CallBuilder(AstGenerator/*!*/ gen) {
            Assert.NotNull(gen);
            _gen = gen;
        }

        public void Add(MSA.Expression/*!*/ expression) {
            _args.Add(expression);
        }

        private RubyCallSignature MakeCallSignature(bool hasImplicitSelf) {
            return new RubyCallSignature(false, true, hasImplicitSelf, _args.Count, SplattedArgument != null, Block != null, RhsArgument != null);
        }

        public MSA.DynamicExpression/*!*/ MakeCallAction(string/*!*/ name, bool hasImplicitSelf) {
            return MakeCallAction(_gen.Context, name, MakeCallSignature(hasImplicitSelf), GetExpressions());
        }

        private static MSA.DynamicExpression/*!*/ MakeCallAction(RubyContext/*!*/ context, string/*!*/ name, RubyCallSignature signature, 
            params MSA.Expression[]/*!*/ args) {
            RubyCallAction call = RubyCallAction.Make(context, name, signature);
            switch (args.Length) {
                case 0: return Ast.Dynamic(call, typeof(object), AstFactory.EmptyExpressions);
                case 1: return Ast.Dynamic(call, typeof(object), args[0]);
                case 2: return Ast.Dynamic(call, typeof(object), args[0], args[1]);
                case 3: return Ast.Dynamic(call, typeof(object), args[0], args[1], args[2]);
                case 4: return Ast.Dynamic(call, typeof(object), args[0], args[1], args[2], args[3]);
                default:
                    return Ast.Dynamic(
                        call,
                        typeof(object),
                        new ReadOnlyCollection<MSA.Expression>(args)
                    );
            }
        }

        public MSA.Expression/*!*/ MakeSuperCallAction(int lexicalScopeId) {
            return Ast.Dynamic(
                SuperCallAction.Make(_gen.Context, MakeCallSignature(true), lexicalScopeId),
                typeof(object),
                GetExpressions()
            );
        }

        private MSA.Expression/*!*/[]/*!*/ GetExpressions() {
            var result = new List<MSA.Expression>();
            result.Add(_gen.CurrentScopeVariable);
            result.Add(Instance);
            
            if (Block != null) {
                result.Add(Block);
            }

            for (int i = 0; i < _args.Count; i++) {
                result.Add(_args[i]);
            }

            if (SplattedArgument != null) {
                result.Add(SplattedArgument);
            }

            if (RhsArgument != null) {
                result.Add(RhsArgument);
            }

            return result.ToArray();
        }
    }
}

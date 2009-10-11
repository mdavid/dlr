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

using System.Dynamic;
using Microsoft.Scripting;
using Microsoft.Scripting.Actions;
using Microsoft.Scripting.Utils;
using IronRuby.Builtins;
using IronRuby.Runtime.Calls;
using AstUtils = Microsoft.Scripting.Ast.Utils;

namespace IronRuby.Compiler.Ast {
    using Ast = MSA.Expression;

    /// <summary>
    /// super(args)
    /// super
    /// 
    /// The former case passes the arguments explicitly
    /// The latter passes all of the arguments that were passed to the current method
    /// (including the block, if any)
    /// 
    /// Also works from a method defined using define_method (not supported yet!)
    /// </summary>
    public partial class SuperCall : CallExpression {
        public SuperCall(Arguments args, Block block, SourceSpan location)
            : base(args, block, location) {
        }

        internal override MSA.Expression/*!*/ TransformRead(AstGenerator/*!*/ gen) {
            // invoke super member action:
            CallBuilder callBuilder = new CallBuilder(gen);

            // self:
            callBuilder.Instance = gen.CurrentSelfVariable;

            // arguments:
            if (Arguments != null) {
                Arguments.TransformToCall(gen, callBuilder);
            } else {
                // copy parameters from the method:
                // TODO: parameters in top-level eval
                LexicalScope.TransformParametersToSuperCall(gen, callBuilder, gen.CurrentMethod.Parameters);
            }

            // block:
            MSA.Expression transformedBlock;
            if (Block != null) {
                transformedBlock = Block.Transform(gen);
            } else {
                transformedBlock = gen.MakeMethodBlockParameterRead();
            }

            // variable assigned to the transformed block in MakeCallWithBlockRetryable:
            MSA.Expression blockArgVariable = gen.CurrentScope.DefineHiddenVariable("#super-call-block", typeof(Proc));
            callBuilder.Block = blockArgVariable;

            // TODO: this could be improved, currently the right method name and declaring module is always searched for at run-time (at the site):

            return gen.DebugMark(
                MethodCall.MakeCallWithBlockRetryable(gen, 
                    callBuilder.MakeSuperCallAction(gen.CurrentFrame.UniqueId), 
                    blockArgVariable, 
                    transformedBlock,
                    Block != null && Block.IsDefinition
                ),
                "#RB: super call ('" + gen.CurrentMethod.MethodName + "')"
            );
        }

        internal override MSA.Expression/*!*/ TransformDefinedCondition(AstGenerator/*!*/ gen) {
            // MRI doesn't evaluate the arguments 
            return Ast.Dynamic(
                SuperCallAction.Make(gen.Context, RubyCallSignature.IsDefined(true), gen.CurrentFrame.UniqueId),
                typeof(bool),
                gen.CurrentScopeVariable,
                gen.CurrentSelfVariable
            );
        }

        internal override string/*!*/ GetNodeName(AstGenerator/*!*/ gen) {
            return "super";
        }
    }
}

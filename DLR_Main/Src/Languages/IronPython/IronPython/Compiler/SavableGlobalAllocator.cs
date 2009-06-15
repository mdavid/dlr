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

#if CODEPLEX_40
using System;
#else
using System; using Microsoft;
#endif
using System.Collections.Generic;

using Microsoft.Scripting;
using Microsoft.Scripting.Ast;
using Microsoft.Scripting.Runtime;

using IronPython.Runtime.Operations;

#if CODEPLEX_40
using MSAst = System.Linq.Expressions;
#else
using MSAst = Microsoft.Linq.Expressions;
#endif

namespace IronPython.Compiler.Ast {
#if CODEPLEX_40
    using Ast = System.Linq.Expressions.Expression;
#else
    using Ast = Microsoft.Linq.Expressions.Expression;
#endif

    class SavableGlobalAllocator : ArrayGlobalAllocator {
        private readonly List<MSAst.Expression/*!*/>/*!*/ _constants;

        public SavableGlobalAllocator(LanguageContext/*!*/ context)
            : base(context) {
            _constants = new List<MSAst.Expression>();
        }

#if CODEPLEX_40
        public override System.Linq.Expressions.Expression GetConstant(object value) {
#else
        public override Microsoft.Linq.Expressions.Expression GetConstant(object value) {
#endif
            return Utils.Constant(value);
        }

#if CODEPLEX_40
        public override System.Linq.Expressions.Expression[] PrepareScope(AstGenerator gen) {
#else
        public override Microsoft.Linq.Expressions.Expression[] PrepareScope(AstGenerator gen) {
#endif
            gen.AddHiddenVariable(GlobalArray);
            return new MSAst.Expression[] {
                Ast.Assign(
                    GlobalArray, 
                    Ast.Call(
                        typeof(PythonOps).GetMethod("GetGlobalArrayFromContext"),
                        ArrayGlobalAllocator._globalContext
                    )
                )
            };
        }

        public override ScriptCode/*!*/ MakeScriptCode(MSAst.Expression/*!*/ body, CompilerContext/*!*/ context, PythonAst/*!*/ ast) {
            MSAst.ParameterExpression scope = Ast.Parameter(typeof(Scope), "$scope");
            MSAst.ParameterExpression language = Ast.Parameter(typeof(LanguageContext), "$language ");

            // finally build the funcion that's closed over the array and
            var func = Ast.Lambda<Func<Scope, LanguageContext, object>>(
                Ast.Block(
                    new[] { GlobalArray },
                    Ast.Assign(
                        GlobalArray, 
                        Ast.Call(
                            null,
                            typeof(PythonOps).GetMethod("GetGlobalArray"),
                            scope
                        )
                    ),
                    body
                ),
                ((PythonCompilerOptions)context.Options).ModuleName,
                new MSAst.ParameterExpression[] { scope, language }
            );

            PythonCompilerOptions pco = context.Options as PythonCompilerOptions;

            return new SavableScriptCode(func, context.SourceUnit, GetNames(), pco.ModuleName);
        }
    }
}

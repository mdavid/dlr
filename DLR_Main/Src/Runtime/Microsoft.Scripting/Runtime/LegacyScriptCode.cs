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
using System.Text;
#if CODEPLEX_40
using System.Linq.Expressions;
#else
using Microsoft.Linq.Expressions;
#endif
using Microsoft.Scripting.Utils;
using System.Threading;
using Microsoft.Scripting.Generation;
using System.Reflection.Emit;
using System.Reflection;

namespace Microsoft.Scripting.Runtime {
    public class LegacyScriptCode : ScriptCode {
        private DlrMainCallTarget _target;
        private LambdaExpression _code;

        public LegacyScriptCode(LambdaExpression code, SourceUnit sourceUnit)
            : this(code, null, sourceUnit) {
        }

        public LegacyScriptCode(LambdaExpression code, DlrMainCallTarget target, SourceUnit sourceUnit) : base(sourceUnit) {
            ContractUtils.RequiresNotNull(sourceUnit, "sourceUnit");

            _target = target;
            _code = code;
        }

        public override object Run() {
            return EnsureTarget(_code)(CreateScope(), SourceUnit.LanguageContext);
        }

        public override object Run(Scope scope) {
            return EnsureTarget(_code)(scope, SourceUnit.LanguageContext);            
        }

        public void EnsureCompiled() {
            EnsureTarget(_code);
        }

        protected virtual object InvokeTarget(LambdaExpression code, Scope scope) {
            return EnsureTarget(code)(scope, LanguageContext);
        }

        private DlrMainCallTarget EnsureTarget(LambdaExpression code) {
            if (_target == null) {
                var lambda = code as Expression<DlrMainCallTarget>;
                if (lambda == null) {
                    // If language APIs produced the wrong delegate type,
                    // rewrite the lambda with the correct type
                    lambda = Expression.Lambda<DlrMainCallTarget>(code.Body, code.Name, code.Parameters);
                }
                Interlocked.CompareExchange(ref _target, lambda.Compile(SourceUnit.EmitDebugSymbols), null);
            }
            return _target;
        }

        protected override KeyValuePair<MethodBuilder, Type> CompileForSave(TypeGen typeGen, Dictionary<SymbolId, FieldBuilder> symbolDict) {
            var lambda = RewriteForSave(typeGen, _code);

            MethodBuilder mb = typeGen.TypeBuilder.DefineMethod(lambda.Name ?? "lambda_method", CompilerHelpers.PublicStatic | MethodAttributes.SpecialName);
            lambda.CompileToMethod(mb, false);
            return new KeyValuePair<MethodBuilder, Type>(mb, typeof(DlrMainCallTarget));
        }

        public static ScriptCode Load(DlrMainCallTarget method, LanguageContext language, string path) {
            SourceUnit su = new SourceUnit(language, NullTextContentProvider.Null, path, SourceCodeKind.File);
            return new LegacyScriptCode(null, method, su);
        }
    }
}

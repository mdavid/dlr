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
using System.Text;

using Ruby;
using Ruby.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using Ruby.Compiler;
using Ruby.Compiler.AST;
using System.IO;

namespace ParseOnly {
    class Program {
        class LoggingErrorSink : ErrorSink {
            public override void Add(SourceUnit sourceUnit, string message, SourceSpan span, int errorCode, Severity severity) {
                base.Add(sourceUnit, message, span, errorCode, severity);

                Console.Error.WriteLine("{0}({1}:{2}): {3}: RB{4}: {5}", sourceUnit.Name, span.Start.Line, span.Start.Column,
                    severity, errorCode, message);
            }
        }

        private static RubyEngine RB { get { return (RubyEngine)GetEngine("rb"); } }

        private static ScriptEngine GetEngine(string id) {
            return ScriptDomainManager.CurrentManager.GetLanguageProvider(id).GetEngine();
        }

        static int Main(string[] args) {
            SourceUnit unit = new SourceCodeUnit(RB, File.ReadAllText(args[0]));
            LoggingErrorSink log = new LoggingErrorSink();
            new Parser().Parse(new CompilerContext(unit, new RubyCompilerOptions(), log));
            return log.ErrorCount + log.FatalErrorCount; 
        }
    }
}

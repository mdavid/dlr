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

using System;
using IronRuby.Builtins;
using IronRuby.Runtime;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting.Hosting.Shell;
using Microsoft.Scripting.Runtime;
using System.Reflection;
using System.Threading;

namespace IronRuby.Hosting {
   
    /// <summary>
    /// A simple Ruby command-line should mimic the standard irb.exe
    /// </summary>
    public class RubyCommandLine : CommandLine {
        public RubyCommandLine() {
        }

        protected override string Logo {
            get {
                return String.Format("IronRuby {1} on {2}{0}Copyright (c) Microsoft Corporation. All rights reserved.{0}{0}",
                    Environment.NewLine, RubyContext.IronRubyVersion, GetRuntime());
            }
        }

        private static string GetRuntime() {
            Type mono = typeof(object).Assembly.GetType("Mono.Runtime");
            return mono != null ?
                (string)mono.GetMethod("GetDisplayName", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, null)
                : string.Format(".NET {0}", Environment.Version);
        }

        protected override int? TryInteractiveAction() {
            try {
                return base.TryInteractiveAction();
            } catch (ThreadAbortException e) {
                Exception visibleException = RubyUtils.GetVisibleException(e);
                if (visibleException == e || visibleException == null) {
                    throw;
                } else {
                    throw visibleException;
                }
            } catch (SystemExit e) {
                return e.Status;
            }
        }

        // overridden to set the default encoding to KCODE/BINARY
        protected override int RunFile(string fileName) {
            return RunFile(Engine.CreateScriptSourceFromFile(RubyUtils.CanonicalizePath(fileName), (((RubyContext)Language).RubyOptions.KCode ?? RubyEncoding.Binary).Encoding));
        }

        protected override void ExecuteCommand(string/*!*/ command) {
            ExecuteCommand(CreateCommandSource(command, SourceCodeKind.InteractiveCode));
        }

        protected override int RunCommand(string/*!*/ command) {
            return RunFile(CreateCommandSource(command, SourceCodeKind.Statements));
        }

        private ScriptSource/*!*/ CreateCommandSource(string/*!*/ command, SourceCodeKind kind) {
#if SILVERLIGHT
            return Engine.CreateScriptSourceFromString(command, kind);
#else
            var kcode = ((RubyContext)Language).RubyOptions.KCode;
            var encoding = kcode != null ? kcode.Encoding : System.Console.InputEncoding;
            return Engine.CreateScriptSource(new BinaryContentProvider(encoding.GetBytes(command)), null, encoding, kind);
#endif
        }
        
        protected override Scope/*!*/ CreateScope() {
            Scope scope = base.CreateScope();
            RubyOps.ScopeSetMember(scope, "iron_ruby", Engine);
            return scope;
        }

        protected override void UnhandledException(Exception e) {
            // Kernel#at_exit can access $!. So we need to publish the uncaught exception
            Ruby.GetExecutionContext(Engine).CurrentException = e;

            base.UnhandledException(e);
        }

        protected override void Shutdown() {
            try {
                Engine.Runtime.Shutdown();
            } catch (SystemExit e) {
                // Kernel#at_exit runs during shutdown, and it can set the exitcode by calling exit
                ExitCode = e.Status;
            } catch (Exception e) {
                UnhandledException(e);
                ExitCode = 1;
            }
        }
    }
}

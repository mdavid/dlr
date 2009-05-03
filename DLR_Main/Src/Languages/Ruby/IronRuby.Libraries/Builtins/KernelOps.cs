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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Runtime.CompilerServices;

using System.Runtime.InteropServices;
using System.Threading;
using IronRuby.Compiler;
using IronRuby.Runtime;
using IronRuby.Runtime.Calls;
using Microsoft.Scripting;
using Microsoft.Scripting.Math;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;

namespace IronRuby.Builtins {

    [RubyModule("Kernel", Extends = typeof(Kernel))]
    public static class KernelOps {

        #region Private Instance Methods

        [RubyMethod("initialize_copy", RubyMethodAttributes.PrivateInstance)]
        public static object InitializeCopy(RubyContext/*!*/ context, object self, object source) {
            RubyClass selfClass = context.GetClassOf(self);
            RubyClass sourceClass = context.GetClassOf(source);
            if (sourceClass != selfClass) {
                throw RubyExceptions.CreateTypeError("initialize_copy should take same class object");
            }
            
            if (context.IsObjectFrozen(self)) {
                throw RubyExceptions.CreateTypeError(String.Format("can't modify frozen {0}", selfClass.Name));
            }

            return self;
        }

        [RubyMethod("singleton_method_added", RubyMethodAttributes.PrivateInstance)]
        public static void MethodAdded(object self, object methodName) {
            // nop
        }

        [RubyMethod("singleton_method_removed", RubyMethodAttributes.PrivateInstance)]
        public static void MethodRemoved(object self, object methodName) {
            // nop
        }

        [RubyMethod("singleton_method_undefined", RubyMethodAttributes.PrivateInstance)]
        public static void MethodUndefined(object self, object methodName) {
            // nop
        }

        #endregion

        #region Private Instance & Singleton Methods

        [RubyMethod("Array", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("Array", RubyMethodAttributes.PublicSingleton)]
        public static IList/*!*/ ToArray(ConversionStorage<IList>/*!*/ tryToAry, ConversionStorage<IList>/*!*/ tryToA, object self, object obj) {
            IList result = Protocols.TryCastToArray(tryToAry, obj);
            if (result != null) {
                return result;
            }

            // MRI 1.9 calls to_a (MRI 1.8 doesn't):
            //if (context.RubyOptions.Compatibility > RubyCompatibility.Ruby18) {
            result = Protocols.TryConvertToArray(tryToA, obj);
            if (result != null) {
                return result;
            }
            //}

            result = new RubyArray();
            if (obj != null) {
                result.Add(obj);
            }
            return result;
        }

        [RubyMethod("Float", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("Float", RubyMethodAttributes.PublicSingleton)]
        public static double ToFloat(object self, [DefaultProtocol]double value) {
            return value;
        }

        [RubyMethod("Integer", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("Integer", RubyMethodAttributes.PublicSingleton)]
        public static object/*!*/ ToInteger(object self, [NotNull]MutableString/*!*/ value) {
            var str = value.ConvertToString();
            int i = 0;
            object result = Tokenizer.ParseInteger(str, 0, ref i).ToObject();
            
            while (i < str.Length && Tokenizer.IsWhiteSpace(str[i])) {
                i++;
            }

            if (i < str.Length) {
                throw RubyExceptions.CreateArgumentError(String.Format("invalid value for Integer: \"{0}\"", str));
            }

            return result;
        }

        [RubyMethod("Integer", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("Integer", RubyMethodAttributes.PublicSingleton)]
        public static object/*!*/ ToInteger(ConversionStorage<IntegerValue>/*!*/ integerConversion, object self, object value) {
            var integer = Protocols.ConvertToInteger(integerConversion, value);
            return integer.IsFixnum ? ScriptingRuntimeHelpers.Int32ToObject(integer.Fixnum) : integer.Bignum;
        }

        [RubyMethod("String", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("String", RubyMethodAttributes.PublicSingleton)]
        public static object/*!*/ ToString(ConversionStorage<MutableString>/*!*/ tosConversion, object self, object obj) {
            return Protocols.ConvertToString(tosConversion, obj);
        }

        #region `, exec, system

#if !SILVERLIGHT
        // Looks for RUBYSHELL and then COMSPEC under Windows
        // It appears that COMSPEC is a special environment variable that cannot be undefined
        internal static ProcessStartInfo/*!*/ GetShell(RubyContext/*!*/ context, MutableString/*!*/ command) {
            PlatformAdaptationLayer pal = context.DomainManager.Platform;
            string shell = pal.GetEnvironmentVariable("RUBYSHELL");
            if (shell == null) {
                shell = pal.GetEnvironmentVariable("COMSPEC");
            }
            return new ProcessStartInfo(shell, String.Format("/C \"{0}\"", command.ConvertToString()));
        }

        private static MutableString/*!*/ JoinArguments(MutableString/*!*/[]/*!*/ args) {
            MutableString result = MutableString.CreateMutable();

            for (int i = 0; i < args.Length; i++) {
                result.Append(args[i]);
                if (args.Length > 1 && i < args.Length - 1) {
                    result.Append(" ");
                }
            }

            return result;
        }

        private static Process/*!*/ ExecuteProcessAndWait(ProcessStartInfo/*!*/ psi) {
            psi.UseShellExecute = false;
            psi.RedirectStandardError = true;
            try {
                Process p = Process.Start(psi);
                p.WaitForExit();
                return p;
            } catch (Exception e) {
                throw RubyErrno.CreateENOENT(psi.FileName, e);
            }
        }

        internal static Process/*!*/ ExecuteProcessCapturingStandardOutput(ProcessStartInfo/*!*/ psi) {
            psi.UseShellExecute = false;
            psi.RedirectStandardError = true;
            psi.RedirectStandardOutput = true;

            try {
                return Process.Start(psi);
            } catch (Exception e) {
                throw RubyErrno.CreateENOENT(psi.FileName, e);
            }
        }

        // Executes a command in a shell child process
        private static Process/*!*/ ExecuteCommandInShell(RubyContext/*!*/ context, MutableString/*!*/ command) {
            return ExecuteProcessAndWait(GetShell(context, command));
        }

        // Executes a command directly in a child process - command is the name of the executable
        private static Process/*!*/ ExecuteCommand(MutableString/*!*/ command, MutableString[]/*!*/ args) {
            return ExecuteProcessAndWait(new ProcessStartInfo(command.ToString(), JoinArguments(args).ToString()));
        }

        // Backtick always executes the command in a shell child process

        [RubyMethod("`", RubyMethodAttributes.PrivateInstance, BuildConfig = "!SILVERLIGHT")]
        [RubyMethod("`", RubyMethodAttributes.PublicSingleton, BuildConfig = "!SILVERLIGHT")]
        public static MutableString/*!*/ ExecuteCommand(RubyContext/*!*/ context, object self, [DefaultProtocol, NotNull]MutableString/*!*/ command) {
            Process p = ExecuteProcessCapturingStandardOutput(GetShell(context, command));
            MutableString result = MutableString.Create(p.StandardOutput.ReadToEnd());
            context.ChildProcessExitStatus = new RubyProcess.Status(p);
            return result;
        }

        // Overloads of exec and system will always execute using the Windows shell if there is only the command parameter
        // If args parameter is passed, it will execute the command directly without going to the shell.

        [RubyMethod("exec", RubyMethodAttributes.PrivateInstance, BuildConfig = "!SILVERLIGHT")]
        [RubyMethod("exec", RubyMethodAttributes.PublicSingleton, BuildConfig = "!SILVERLIGHT")]
        public static void Execute(RubyContext/*!*/ context, object self, [DefaultProtocol, NotNull]MutableString/*!*/ command) {
            Process p = ExecuteCommandInShell(context, command);
            context.ChildProcessExitStatus = p.ExitCode;
            Exit(self, p.ExitCode);
        }

        [RubyMethod("exec", RubyMethodAttributes.PrivateInstance, BuildConfig = "!SILVERLIGHT")]
        [RubyMethod("exec", RubyMethodAttributes.PublicSingleton, BuildConfig = "!SILVERLIGHT")]
        public static void Execute(RubyContext/*!*/ context, object self, [DefaultProtocol, NotNull]MutableString/*!*/ command,
            [DefaultProtocol, NotNull, NotNullItems]params MutableString[]/*!*/ args) {
            Process p = ExecuteCommand(command, args);
            context.ChildProcessExitStatus = p.ExitCode;
            Exit(self, p.ExitCode);
        }

        [RubyMethod("system", RubyMethodAttributes.PrivateInstance, BuildConfig = "!SILVERLIGHT")]
        [RubyMethod("system", RubyMethodAttributes.PublicSingleton, BuildConfig = "!SILVERLIGHT")]
        public static bool System(RubyContext/*!*/ context, object self, [DefaultProtocol, NotNull]MutableString/*!*/ command) {
            Process p = ExecuteCommandInShell(context, command);
            context.ChildProcessExitStatus = p.ExitCode;
            return p.ExitCode == 0;
        }

        [RubyMethod("system", RubyMethodAttributes.PrivateInstance, BuildConfig = "!SILVERLIGHT")]
        [RubyMethod("system", RubyMethodAttributes.PublicSingleton, BuildConfig = "!SILVERLIGHT")]
        public static bool System(RubyContext/*!*/ context, object self, [DefaultProtocol, NotNull]MutableString/*!*/ command,
            [DefaultProtocol, NotNull, NotNullItems]params MutableString/*!*/[]/*!*/ args) {
            Process p = ExecuteCommand(command, args);
            context.ChildProcessExitStatus = p.ExitCode;
            return p.ExitCode == 0;
        }
#endif
        #endregion

        [RubyMethod("abort", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("abort", RubyMethodAttributes.PublicSingleton)]
        public static void Abort(object/*!*/ self) {
            Exit(self, 1);
        }

        [RubyMethod("abort", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("abort", RubyMethodAttributes.PublicSingleton)]
        public static void Abort(BinaryOpStorage/*!*/ writeStorage, object/*!*/ self, [NotNull]MutableString/*!*/ message) {
            var site = writeStorage.GetCallSite("write", 1);
            site.Target(site, writeStorage.Context.StandardErrorOutput, message);

            Exit(self, 1);
        }

        [RubyMethod("at_exit", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("at_exit", RubyMethodAttributes.PublicSingleton)]
        public static Proc/*!*/ AtExit(BlockParam/*!*/ block, object self) {
            if (block == null) {
                throw RubyExceptions.CreateArgumentError("called without a block");
            }

            block.RubyContext.RegisterShutdownHandler(block);
            return block.Proc;
        }

        [RubyMethod("autoload", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("autoload", RubyMethodAttributes.PublicSingleton)]
        public static void SetAutoloadedConstant(RubyScope/*!*/ scope, object self,
            [DefaultProtocol, NotNull]string/*!*/ constantName, [DefaultProtocol, NotNull]MutableString/*!*/ path) {
            ModuleOps.SetAutoloadedConstant(scope.GetInnerMostModuleForConstantLookup(), constantName, path);
        }

        [RubyMethod("autoload?", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("autoload?", RubyMethodAttributes.PublicSingleton)]
        public static MutableString GetAutoloadedConstantPath(RubyScope/*!*/ scope, object self, [DefaultProtocol, NotNull]string/*!*/ constantName) {
            return ModuleOps.GetAutoloadedConstantPath(scope.GetInnerMostModuleForConstantLookup(), constantName);
        }

        [RubyMethod("binding", RubyMethodAttributes.PrivateInstance)]
        public static Binding/*!*/ GetLocalScope(RubyScope/*!*/ scope, object self) {
            return new Binding(scope);
        }
        
        [RubyMethod("block_given?", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("iterator?", RubyMethodAttributes.PrivateInstance)]
        public static bool HasBlock(RubyScope/*!*/ scope, object self) {
            var methodScope = scope.GetInnerMostMethodScope();
            return methodScope != null && methodScope.BlockParameter != null;
        }

        //callcc

        [RubyMethod("caller", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("caller", RubyMethodAttributes.PublicSingleton)]
        [RubyStackTraceHidden]
        public static RubyArray/*!*/ GetStackTrace(RubyContext/*!*/ context, object self, [DefaultParameterValue(1)]int skipFrames) {
            if (skipFrames < 0) {
                return new RubyArray();
            }

            return RubyExceptionData.CreateBacktrace(context, skipFrames);
        }

        //chomp
        //chomp!
        //chop
        //chop!

        [RubyMethod("eval", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("eval", RubyMethodAttributes.PublicSingleton)]
        public static object Evaluate(RubyScope/*!*/ scope, object self, [NotNull]MutableString/*!*/ code, 
            [Optional]Binding binding, [Optional, NotNull]MutableString file, [DefaultParameterValue(1)]int line) {

            RubyScope targetScope = (binding != null) ? binding.LocalScope : scope;
            return RubyUtils.Evaluate(code, targetScope, targetScope.SelfObject, null, file, line);
        }

        [RubyMethod("eval", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("eval", RubyMethodAttributes.PublicSingleton)]
        public static object Evaluate(RubyScope/*!*/ scope, object self, [NotNull]MutableString/*!*/ code,
            [NotNull]Proc/*!*/ procBinding, [Optional, NotNull]MutableString file, [DefaultParameterValue(1)]int line) {

            return RubyUtils.Evaluate(code, procBinding.LocalScope, procBinding.LocalScope.SelfObject, null, file, line);
        }

        [RubyMethod("exit", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("exit", RubyMethodAttributes.PublicSingleton)]
        public static void Exit(object self) {
            Exit(self, 1);
        }

        [RubyMethod("exit", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("exit", RubyMethodAttributes.PublicSingleton)]
        public static void Exit(object self, bool isSuccessful) {
            Exit(self, isSuccessful ? 0 : 1);
        }

        [RubyMethod("exit", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("exit", RubyMethodAttributes.PublicSingleton)]
        public static void Exit(object self, int exitCode) {
            throw new SystemExit(exitCode, "exit");
        }

        [RubyMethod("exit!", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("exit!", RubyMethodAttributes.PublicSingleton)]
        public static void TerminateExecution(RubyContext/*!*/ context, object self) {
            TerminateExecution(context, self, 1);
        }

        [RubyMethod("exit!", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("exit!", RubyMethodAttributes.PublicSingleton)]
        public static void TerminateExecution(RubyContext/*!*/ context, object self, bool isSuccessful) {
            TerminateExecution(context, self, isSuccessful ? 0 : 1);
        }

        [RubyMethod("exit!", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("exit!", RubyMethodAttributes.PublicSingleton)]
        public static void TerminateExecution(RubyContext/*!*/ context, object self, int exitCode) {
            context.DomainManager.Platform.TerminateScriptExecution(exitCode);
        }

        //fork
        
        [RubyMethod("global_variables", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("global_variables", RubyMethodAttributes.PublicSingleton)]
        public static RubyArray/*!*/ GetGlobalVariableNames(RubyContext/*!*/ context, object self) {
            RubyArray result = new RubyArray();
            lock (context.GlobalVariablesLock) {
                foreach (KeyValuePair<string, GlobalVariable> global in context.GlobalVariables) {
                    if (global.Value.IsEnumerated) {
                        // TODO: Ruby 1.9 returns symbols:
                        result.Add(MutableString.Create(global.Key));
                    }
                }
            }
            return result;
        }

        //gsub
        //gsub!

        // TODO: in Ruby 1.9, these two methods will do different things so we will likely have to have two
        // separate implementations of these methods
        [RubyMethod("lambda", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("lambda", RubyMethodAttributes.PublicSingleton)]
        [RubyMethod("proc", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("proc", RubyMethodAttributes.PublicSingleton)]
        public static Proc/*!*/ CreateLambda(BlockParam/*!*/ block, object self) {
            if (block == null) {
                throw RubyExceptions.CreateArgumentError("tried to create Proc object without a block");
            }

            // doesn't preserve the class:
            return block.Proc.ToLambda();
        }

        #region load, require

        [RubyMethod("load", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("load", RubyMethodAttributes.PublicSingleton)]
        public static bool Load(RubyScope/*!*/ scope, object self,
            [DefaultProtocol, NotNull]MutableString/*!*/ libraryName, [Optional]bool wrap) {
            return scope.RubyContext.Loader.LoadFile(scope.GlobalScope.Scope, self, libraryName, wrap ? LoadFlags.LoadIsolated : LoadFlags.None);
        }

        [RubyMethod("load_assembly", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("load_assembly", RubyMethodAttributes.PublicSingleton)]
        public static bool LoadAssembly(RubyContext/*!*/ context, object self,
            [DefaultProtocol, NotNull]MutableString/*!*/ assemblyName, [DefaultProtocol, Optional, NotNull]MutableString libraryNamespace) {

            string initializer = libraryNamespace != null ? LibraryInitializer.GetFullTypeName(libraryNamespace.ConvertToString()) : null;
            return context.Loader.LoadAssembly(assemblyName.ConvertToString(), initializer, true);
        }

        [RubyMethod("require", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("require", RubyMethodAttributes.PublicSingleton)]
        public static bool Require(RubyScope/*!*/ scope, object self, 
            [DefaultProtocol, NotNull]MutableString/*!*/ libraryName) {
            return scope.RubyContext.Loader.LoadFile(scope.GlobalScope.Scope, self, libraryName, LoadFlags.LoadOnce | LoadFlags.AppendExtensions);
        }

        #endregion

        [RubyMethod("local_variables", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("local_variables", RubyMethodAttributes.PublicSingleton)]
        public static RubyArray/*!*/ GetLocalVariableNames(RubyScope/*!*/ scope, object self) {
            List<string> names = scope.GetVisibleLocalNames();

            RubyArray result = new RubyArray(names.Count);
            for (int i = 0; i < names.Count; i++) {
                result.Add(MutableString.Create(names[i]));
            }
            return result;
        }

        [RubyMethod("loop", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("loop", RubyMethodAttributes.PublicSingleton)]
        public static object Loop(BlockParam/*!*/ block, object self) {
            if (block == null) {
                throw RubyExceptions.NoBlockGiven();
            }

            while (true) {
                object result;
                if (block.Yield(out result)) {
                    return result;
                }
            }
        }

        [RubyMethod("method_missing", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("method_missing", RubyMethodAttributes.PublicSingleton)]
        [RubyStackTraceHidden]
        public static object MethodMissing(RubyContext/*!*/ context, object/*!*/ self, SymbolId symbol, [NotNull]params object[]/*!*/ args) {
            string name = SymbolTable.IdToString(symbol);
            throw RubyExceptions.CreateMethodMissing(context, self, name);            
        }

        #region open

        // TODO: should call File#initialize

        private static object OpenWithBlock(BlockParam/*!*/ block, RubyIO file) {
            try {
                object result;
                block.Yield(file, out result);
                return result;
            } finally {
                file.Close();
            }
        }

        private static void SetPermission(RubyContext/*!*/ context, string/*!*/ fileName, int/*!*/ permission) {
            bool existingFile = context.DomainManager.Platform.FileExists(fileName);

            if (!existingFile) {
                RubyFileOps.Chmod(fileName, permission);
            }
        }

        [RubyMethod("open", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("open", RubyMethodAttributes.PublicSingleton)]
        public static RubyIO/*!*/ Open(
            RubyContext/*!*/ context, 
            object self,
            [DefaultProtocol, NotNull]MutableString/*!*/ path, 
            [DefaultProtocol, Optional]MutableString mode, 
            [DefaultProtocol, DefaultParameterValue(RubyFileOps.ReadWriteMode)]int permission) {

            string fileName = path.ConvertToString();
            if (fileName.Length > 0 && fileName[0] == '|') {
                throw new NotImplementedError();
            }

            RubyIO file = new RubyFile(context, fileName, (mode != null) ? mode.ToString() : "r");

            SetPermission(context, fileName, permission);

            return file;
        }

        [RubyMethod("open", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("open", RubyMethodAttributes.PublicSingleton)]
        public static object Open(
            RubyContext/*!*/ context, 
            [NotNull]BlockParam/*!*/ block, 
            object self,
            [DefaultProtocol, NotNull]MutableString/*!*/ path, 
            [DefaultProtocol, Optional]MutableString mode, 
            [DefaultProtocol, DefaultParameterValue(RubyFileOps.ReadWriteMode)]int permission) {

            RubyIO file = Open(context, self, path, mode, permission);
            return OpenWithBlock(block, file);
        }

        [RubyMethod("open", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("open", RubyMethodAttributes.PublicSingleton)]
        public static RubyIO/*!*/ Open(
            RubyContext/*!*/ context, 
            object self,
            [DefaultProtocol, NotNull]MutableString/*!*/ path, 
            int mode,
            [DefaultProtocol, DefaultParameterValue(RubyFileOps.ReadWriteMode)]int permission) {

            string fileName = path.ConvertToString();
            if (fileName.Length > 0 && fileName[0] == '|') {
                throw new NotImplementedError();
            }

            RubyIO file = new RubyFile(context, fileName, (RubyFileMode)mode);

            SetPermission(context, fileName, permission);

            return file;
        }

        [RubyMethod("open", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("open", RubyMethodAttributes.PublicSingleton)]
        public static object Open(
            RubyContext/*!*/ context, 
            [NotNull]BlockParam/*!*/ block, 
            object self,
            [DefaultProtocol, NotNull]MutableString/*!*/ path, 
            int mode,
            [DefaultProtocol, DefaultParameterValue(RubyFileOps.ReadWriteMode)]int permission) {

            RubyIO file = Open(context, self, path, mode, permission);
            return OpenWithBlock(block, file);
        }

        #endregion

        #region p, print, printf, putc, puts, warn, gets, getc

        [RubyMethod("p", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("p", RubyMethodAttributes.PublicSingleton)]
        public static void PrintInspect(BinaryOpStorage/*!*/ writeStorage, UnaryOpStorage/*!*/ inspectStorage, ConversionStorage<MutableString>/*!*/ tosConversion, 
            object self, [NotNull]params object[]/*!*/ args) {

            var inspect = inspectStorage.GetCallSite("inspect");
            for (int i = 0; i < args.Length; i++) {
                args[i] = inspect.Target(inspect, args[i]);
            }
            
            // no dynamic dispatch to "puts":
            RubyIOOps.Puts(writeStorage, tosConversion, writeStorage.Context.StandardOutput, args);
        }

        [RubyMethod("print", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("print", RubyMethodAttributes.PublicSingleton)]
        public static void Print(BinaryOpStorage/*!*/ writeStorage, RubyScope/*!*/ scope, object self) {
            // no dynamic dispatch to "print":
            RubyIOOps.Print(writeStorage, scope, scope.RubyContext.StandardOutput);
        }

        [RubyMethod("print", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("print", RubyMethodAttributes.PublicSingleton)]
        public static void Print(BinaryOpStorage/*!*/ writeStorage, object self, object val) {
            // no dynamic dispatch to "print":
            RubyIOOps.Print(writeStorage, writeStorage.Context.StandardOutput, val);
        }

        [RubyMethod("print", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("print", RubyMethodAttributes.PublicSingleton)]
        public static void Print(BinaryOpStorage/*!*/ writeStorage, ConversionStorage<MutableString>/*!*/ tosConversion, 
            object self, [NotNull]params object[]/*!*/ args) {

            // no dynamic dispatch to "print":
            RubyIOOps.Print(writeStorage, tosConversion, tosConversion.Context.StandardOutput, args);
        }

        // this overload is called only if the first parameter is string:
        [RubyMethod("printf", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("printf", RubyMethodAttributes.PublicSingleton)]
        public static void PrintFormatted(
            StringFormatterSiteStorage/*!*/ storage,
            ConversionStorage<MutableString>/*!*/ stringCast, 
            BinaryOpStorage/*!*/ writeStorage,
            object self, [NotNull]MutableString/*!*/ format, [NotNull]params object[]/*!*/ args) {

            PrintFormatted(storage, stringCast, writeStorage, self, storage.Context.StandardOutput, format, args);
        }

        [RubyMethod("printf", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("printf", RubyMethodAttributes.PublicSingleton)]
        public static void PrintFormatted(
            StringFormatterSiteStorage/*!*/ storage, 
            ConversionStorage<MutableString>/*!*/ stringCast, 
            BinaryOpStorage/*!*/ writeStorage,
            object self, object io, [NotNull]object/*!*/ format, [NotNull]params object[]/*!*/ args) {

            Debug.Assert(!(io is MutableString));
            
            // TODO: BindAsObject attribute on format?
            // format cannot be strongly typed to MutableString due to ambiguity between signatures (MS, object) vs (object, MS)
            Protocols.Write(writeStorage, io, 
                Sprintf(storage, self, Protocols.CastToString(stringCast, format), args)
            );
        }

        [RubyMethod("putc", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("putc", RubyMethodAttributes.PublicSingleton)]
        public static MutableString/*!*/ Putc(BinaryOpStorage/*!*/ writeStorage, object self, [NotNull]MutableString/*!*/ arg) {
            // no dynamic dispatch:
            return RubyIOOps.Putc(writeStorage, writeStorage.Context.StandardOutput, arg);
        }

        [RubyMethod("putc", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("putc", RubyMethodAttributes.PublicSingleton)]
        public static int Putc(BinaryOpStorage/*!*/ writeStorage, object self, [DefaultProtocol]int arg) {
            // no dynamic dispatch:
            return RubyIOOps.Putc(writeStorage, writeStorage.Context.StandardOutput, arg);
        }

        [RubyMethod("puts", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("puts", RubyMethodAttributes.PublicSingleton)]
        public static void PutsEmptyLine(BinaryOpStorage/*!*/ writeStorage, object self) {
            // call directly, no dynamic dispatch to "self":
            RubyIOOps.PutsEmptyLine(writeStorage, writeStorage.Context.StandardOutput);
        }

        [RubyMethod("puts", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("puts", RubyMethodAttributes.PublicSingleton)]
        public static void PutString(BinaryOpStorage/*!*/ writeStorage, ConversionStorage<MutableString>/*!*/ tosConversion, 
            object self, object arg) {

            // call directly, no dynamic dispatch to "self":
            RubyIOOps.Puts(writeStorage, tosConversion, writeStorage.Context.StandardOutput, arg);
        }

        [RubyMethod("puts", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("puts", RubyMethodAttributes.PublicSingleton)]
        public static void PutString(BinaryOpStorage/*!*/ writeStorage, object self, [NotNull]MutableString/*!*/ arg) {
            // call directly, no dynamic dispatch to "self":
            RubyIOOps.Puts(writeStorage, writeStorage.Context.StandardOutput, arg);
        }

        [RubyMethod("puts", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("puts", RubyMethodAttributes.PublicSingleton)]
        public static void PutString(BinaryOpStorage/*!*/ writeStorage, ConversionStorage<MutableString>/*!*/ tosConversion, 
            object self, [NotNull]params object[]/*!*/ args) {

            // call directly, no dynamic dispatch to "self":
            RubyIOOps.Puts(writeStorage, tosConversion, writeStorage.Context.StandardOutput, args);
        }

        [RubyMethod("warn", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("warn", RubyMethodAttributes.PublicSingleton)]
        public static void ReportWarning(BinaryOpStorage/*!*/ writeStorage, ConversionStorage<MutableString>/*!*/ tosConversion, 
            object self, object message) {

            if (writeStorage.Context.Verbose != null) {
                var output = writeStorage.Context.StandardErrorOutput;
                // MRI: unlike Kernel#puts this outputs \n even if the message ends with \n:
                var site = writeStorage.GetCallSite("write", 1);
                site.Target(site, output, RubyIOOps.ToPrintedString(tosConversion, message));
                RubyIOOps.PutsEmptyLine(writeStorage, output);
            }
        }

        // TODO: not supported in Ruby 1.9
        [RubyMethod("getc", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("getc", RubyMethodAttributes.PublicSingleton)]
        public static object ReadInputCharacter(UnaryOpStorage/*!*/ getcStorage, object self) {
            getcStorage.Context.ReportWarning("getc is obsolete; use STDIN.getc instead");
            var site = getcStorage.GetCallSite("getc", 0);
            return site.Target(site, getcStorage.Context.StandardInput);
        }

        [RubyMethod("gets", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("gets", RubyMethodAttributes.PublicSingleton)]
        public static object ReadInputLine(CallSiteStorage<Func<CallSite, object, MutableString, object>>/*!*/ storage, object self) {
            return ReadInputLine(storage, self, storage.Context.InputSeparator);
        }

        [RubyMethod("gets", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("gets", RubyMethodAttributes.PublicSingleton)]
        public static object ReadInputLine(CallSiteStorage<Func<CallSite, object, MutableString, object>>/*!*/ storage, object self, 
            [NotNull]MutableString/*!*/ separator) {

            var site = storage.GetCallSite("gets", 1);
            return site.Target(site, storage.Context.StandardInput, separator);
        }

        #endregion

        #region raise, fail

        [RubyMethod("raise", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("raise", RubyMethodAttributes.PublicSingleton)]
        [RubyMethod("fail", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("fail", RubyMethodAttributes.PublicSingleton)]
        [RubyStackTraceHidden]
        public static void RaiseException(RubyContext/*!*/ context, object self) {
            Exception exception = context.CurrentException;
            if (exception == null) {
                exception = new RuntimeError();
            }

#if DEBUG && !SILVERLIGHT
            if (RubyOptions.UseThreadAbortForSyncRaise) {
                RubyUtils.RaiseAsyncException(Thread.CurrentThread, exception);
            }
#endif
            // rethrow semantics, preserves the backtrace associated with the exception:
            throw exception;
        }

        [RubyMethod("raise", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("raise", RubyMethodAttributes.PublicSingleton)]
        [RubyMethod("fail", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("fail", RubyMethodAttributes.PublicSingleton)]
        [RubyStackTraceHidden]
        public static void RaiseException(object self, [NotNull]MutableString/*!*/ message) {
            Exception exception = RubyExceptionData.InitializeException(new RuntimeError(message.ToString()), message);

#if DEBUG && !SILVERLIGHT
            if (RubyOptions.UseThreadAbortForSyncRaise) {
                RubyUtils.RaiseAsyncException(Thread.CurrentThread, exception);
            }
#endif
            throw exception;
        }

        [RubyMethod("raise", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("raise", RubyMethodAttributes.PublicSingleton)]
        [RubyMethod("fail", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("fail", RubyMethodAttributes.PublicSingleton)]
        [RubyStackTraceHidden]
        public static void RaiseException(RespondToStorage/*!*/ respondToStorage, UnaryOpStorage/*!*/ storage0, BinaryOpStorage/*!*/ storage1,
            CallSiteStorage<Action<CallSite, Exception, RubyArray>>/*!*/ setBackTraceStorage, 
            object self, object/*!*/ obj, [Optional]object arg, [Optional]RubyArray backtrace) {

            Exception exception = CreateExceptionToRaise(respondToStorage, storage0, storage1, setBackTraceStorage, obj, arg, backtrace);
#if DEBUG && !SILVERLIGHT
            if (RubyOptions.UseThreadAbortForSyncRaise) {
                RubyUtils.RaiseAsyncException(Thread.CurrentThread, exception);
            }
#endif
            // rethrow semantics, preserves the backtrace associated with the exception:
            throw exception;
        }

        internal static Exception/*!*/ CreateExceptionToRaise(RespondToStorage/*!*/ respondToStorage, UnaryOpStorage/*!*/ storage0, BinaryOpStorage/*!*/ storage1,
            CallSiteStorage<Action<CallSite, Exception, RubyArray>>/*!*/ setBackTraceStorage, 
            object/*!*/ obj, object arg, RubyArray backtrace) {

            if (Protocols.RespondTo(respondToStorage, obj, "exception")) {
                Exception e = null;
                if (arg != Missing.Value) {
                    var site = storage1.GetCallSite("exception");
                    e = site.Target(site, obj, arg) as Exception;
                } else {
                    var site = storage0.GetCallSite("exception");
                    e = site.Target(site, obj) as Exception;
                }

                if (e != null) {
                    if (backtrace != null) {
                        var site = setBackTraceStorage.GetCallSite("set_backtrace", 1);
                        site.Target(site, e, backtrace);
                    }
                    return e;
                }
            }

            throw RubyExceptions.CreateTypeError("exception class/object expected");
        }

        #endregion

        [RubyMethod("rand", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("rand", RubyMethodAttributes.PublicSingleton)]
        public static double Rand(RubyContext/*!*/ context, object self) {
            return new Random().NextDouble();
        }

        [RubyMethod("rand", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("rand", RubyMethodAttributes.PublicSingleton)]
        public static object Rand(RubyContext/*!*/ context, object self, int limit) {
            if (limit == 0) {
                return Rand(context, self);
            }

            return ScriptingRuntimeHelpers.Int32ToObject((int)(new Random().NextDouble() * (limit - 1)));
        }

        [RubyMethod("rand", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("rand", RubyMethodAttributes.PublicSingleton)]
        public static object Rand(RubyContext/*!*/ context, object self, double limit) {
            if (limit < 1) {
                return Rand(context, self);
            }

            return ScriptingRuntimeHelpers.Int32ToObject((int)(new Random().NextDouble() * (limit - 1)));
        }

        [RubyMethod("rand", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("rand", RubyMethodAttributes.PublicSingleton)]
        public static object Rand(RubyContext/*!*/ context, object self, [NotNull]BigInteger/*!*/ limit) {
            throw new NotImplementedError("rand(BigInteger) not implemented");
        }

        [RubyMethod("rand", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("rand", RubyMethodAttributes.PublicSingleton)]
        public static object Rand(ConversionStorage<int>/*!*/ conversionStorage, object self, object limit) {
            if (limit == null) {
                return Rand(conversionStorage.Context, self);
            }
            // TODO: to_int can return bignum here
            return Rand(conversionStorage.Context, self, Protocols.CastToFixnum(conversionStorage, limit));
        }

        //readline
        //readlines
        //scan

        [RubyMethod("select", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("select", RubyMethodAttributes.PublicSingleton)]
        public static RubyArray Select(RubyContext/*!*/ context, object self, RubyArray read, [Optional]RubyArray write, [Optional]RubyArray error) {
            return RubyIOOps.Select(context, null, read, write, error);
        }
        
        [RubyMethod("select", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("select", RubyMethodAttributes.PublicSingleton)]
        public static RubyArray Select(RubyContext/*!*/ context, object self, RubyArray read, [Optional]RubyArray write, [Optional]RubyArray error, int timeoutInSeconds) {
            return RubyIOOps.Select(context, null, read, write, error, timeoutInSeconds);
        }

        [RubyMethod("select", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("select", RubyMethodAttributes.PublicSingleton)]
        public static RubyArray Select(RubyContext/*!*/ context, object self, RubyArray read, [Optional]RubyArray write, [Optional]RubyArray error, double timeoutInSeconds) {
            return RubyIOOps.Select(context, null, read, write, error, timeoutInSeconds);
        }

        [RubyMethod("set_trace_func", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("set_trace_func", RubyMethodAttributes.PublicSingleton)]
        public static Proc SetTraceListener(RubyContext/*!*/ context, object self, Proc listener) {
            if (listener != null && !context.RubyOptions.EnableTracing) {
                throw new NotSupportedException("Tracing is not supported unless -trace option is specified.");
            }
            return context.TraceListener = listener;
        }

        [RubyMethod("sleep", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("sleep", RubyMethodAttributes.PublicSingleton)]
        public static void Sleep(object self) {
            ThreadOps.DoSleep();
        }

        [RubyMethod("sleep", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("sleep", RubyMethodAttributes.PublicSingleton)]
        public static int Sleep(object self, int seconds) {
            if (seconds < 0) { 
                throw RubyExceptions.CreateArgumentError("time interval must be positive");
            }

            long ms = seconds * 1000;
            Thread.Sleep(ms > Int32.MaxValue ? Timeout.Infinite : (int)ms);
            return seconds;
        }

        [RubyMethod("sleep", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("sleep", RubyMethodAttributes.PublicSingleton)]
        public static double Sleep(object self, double seconds) {
            if (seconds < 0) {
                throw RubyExceptions.CreateArgumentError("time interval must be positive");
            }

            double ms = seconds * 1000;
            Thread.Sleep(ms > Int32.MaxValue ? Timeout.Infinite : (int)ms);
            return seconds;
        }
        
        //split

        [RubyMethod("format", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("format", RubyMethodAttributes.PublicSingleton)]
        [RubyMethod("sprintf", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("sprintf", RubyMethodAttributes.PublicSingleton)]
        public static MutableString/*!*/ Sprintf(StringFormatterSiteStorage/*!*/ storage, 
            object self, [DefaultProtocol, NotNull]MutableString/*!*/ format, [NotNull]params object[] args) {

            return new StringFormatter(storage, format.ConvertToString(), args).Format();
        }

        //srand
        //sub
        //sub!
        //syscall
        //test

        //trace_var

#if !SILVERLIGHT // Signals dont make much sense in Silverlight as cross-process communication is not allowed
        [RubyMethod("trap", RubyMethodAttributes.PrivateInstance, BuildConfig = "!SILVERLIGHT")]
        [RubyMethod("trap", RubyMethodAttributes.PublicSingleton, BuildConfig = "!SILVERLIGHT")]
        public static object Trap(RubyContext/*!*/ context, object self, object signalId, Proc proc) {
            return Signal.Trap(context, self, signalId, proc);
        }

        [RubyMethod("trap", RubyMethodAttributes.PrivateInstance, BuildConfig = "!SILVERLIGHT")]
        [RubyMethod("trap", RubyMethodAttributes.PublicSingleton, BuildConfig = "!SILVERLIGHT")]
        public static object Trap(RubyContext/*!*/ context, [NotNull]BlockParam/*!*/ block, object self, object signalId) {
            return Signal.Trap(context, block, self, signalId);
        }

#endif

        //untrace_var

        #region throw, catch 

        private sealed class ThrowCatchUnwinder : StackUnwinder {
            public readonly string/*!*/ Label;

            internal ThrowCatchUnwinder(string/*!*/ label, object returnValue)
                : base(returnValue) {
                Label = label;
            }
        }

        [ThreadStatic]
        private static Stack<string/*!*/> _catchSymbols;

        [RubyMethod("catch", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("catch", RubyMethodAttributes.PublicSingleton)]
        public static object Catch(BlockParam/*!*/ block, object self, [DefaultProtocol, NotNull]string/*!*/ label) {
            if (block == null) {
                throw RubyExceptions.NoBlockGiven();
            }

            try {
                if (_catchSymbols == null) {
                    _catchSymbols = new Stack<string>();
                }
                _catchSymbols.Push(label);

                try {
                    object result;
                    block.Yield(label, out result);
                    return result;
                } catch (ThrowCatchUnwinder unwinder) {
                    if (unwinder.Label == label) {
                        return unwinder.ReturnValue;
                    }

                    throw;
                }
            } finally {
                _catchSymbols.Pop();
            }
        }

        [RubyMethod("throw", RubyMethodAttributes.PrivateInstance)]
        [RubyMethod("throw", RubyMethodAttributes.PublicSingleton)]
        public static void Throw(object self, [DefaultProtocol, NotNull]string/*!*/ label, [DefaultParameterValue(null)]object returnValue) {
            if (_catchSymbols == null || !_catchSymbols.Contains(label)) {
                throw RubyExceptions.CreateNameError(String.Format("uncaught throw `{0}'", label));
            }

            throw new ThrowCatchUnwinder(label, returnValue);
        }

        #endregion

        #endregion

        #region Public Instance Methods

        #region ==, ===, =~, eql?, equal?, hash

        [RubyMethod("==")]
        [RubyMethod("eql?")]
        public static bool ValueEquals(object self, object other) {
            return RubyUtils.ValueEquals(self, other);
        }

        [RubyMethod("=~")]
        public static bool Match(object self, object other) {
            // Default implementation of match that is overridden in descendents (notably String and Regexp)
            return false;
        }

        // calls == by default
        [RubyMethod("===")]
        public static bool HashEquals(BinaryOpStorage/*!*/ equals, object self, object other) {
            return Protocols.IsEqual(equals, self, other);
        }

        [RubyMethod("hash")]
        public static int Hash(object self) {
            return RubyUtils.GetHashCode(self);
        }

        [RubyMethod("equal?")]
        public static bool Equal(object self, object other) {
            // Comparing object IDs is (potentially) expensive because it forces us
            // to generate InstanceData and a new object ID
            //return GetObjectId(self) == GetObjectId(other);
            if (self == other) {
                return true;
            }

            if (RubyUtils.IsRubyValueType(self) && RubyUtils.IsRubyValueType(other)) {
                return object.Equals(self, other);
            }

            return false;
        }

        #endregion

        #region __id__, id, object_id, class

        [RubyMethod("id")]
        public static int GetId(RubyContext/*!*/ context, object self) {
            context.ReportWarning("Object#id will be deprecated; use Object#object_id");
            return GetObjectId(context, self);
        }

        [RubyMethod("__id__")]
        [RubyMethod("object_id")]
        public static int GetObjectId(RubyContext/*!*/ context, object self) {
            return RubyUtils.GetObjectId(context, self);
        }

        [RubyMethod("type")]
        public static RubyClass/*!*/ GetClassObsolete(RubyContext/*!*/ context, object self) {
            context.ReportWarning("Object#type will be deprecated; use Object#class");
            return context.GetClassOf(self);
        }

        [RubyMethod("class")]
        public static RubyClass/*!*/ GetClass(RubyContext/*!*/ context, object self) {
            return context.GetClassOf(self);
        }

        #endregion

        #region clone, dup

        [RubyMethod("clone")]
        public static object/*!*/ Clone(
            CallSiteStorage<Func<CallSite, object, object, object>>/*!*/ initializeCopyStorage,
            CallSiteStorage<Func<CallSite, RubyClass, object>>/*!*/ allocateStorage,
            object self) {

            return Clone(initializeCopyStorage, allocateStorage, true, self);
        }

        [RubyMethod("dup")]
        public static object/*!*/ Duplicate(
            CallSiteStorage<Func<CallSite, object, object, object>>/*!*/ initializeCopyStorage,
            CallSiteStorage<Func<CallSite, RubyClass, object>>/*!*/ allocateStorage,
            object self) {

            return Clone(initializeCopyStorage, allocateStorage, false, self);
        }

        private static object/*!*/ Clone(
            CallSiteStorage<Func<CallSite, object, object, object>>/*!*/ initializeCopyStorage,
            CallSiteStorage<Func<CallSite, RubyClass, object>>/*!*/ allocateStorage,
            bool isClone, object self) {

            var context = allocateStorage.Context;

            object result;
            if (!RubyUtils.TryDuplicateObject(initializeCopyStorage, allocateStorage, self, isClone, out result)) {
                throw RubyExceptions.CreateTypeError(String.Format("can't {0} {1}", isClone ? "clone" : "dup", context.GetClassName(self)));
            }
            return context.TaintObjectBy(result, self);
        }

        #endregion

        [RubyMethod("display")]
        public static void Display(RubyContext/*!*/ context, object self) {
            // TODO:
        }

        [RubyMethod("extend")]
        public static object Extend(
            CallSiteStorage<Func<CallSite, RubyModule, object, object>>/*!*/ extendObjectStorage,
            CallSiteStorage<Func<CallSite, RubyModule, object, object>>/*!*/ extendedStorage,
            object self, [NotNull]RubyModule/*!*/ module, [NotNull]params RubyModule/*!*/[]/*!*/ modules) {

            Assert.NotNull(self, modules);
            RubyUtils.RequireMixins(module.SingletonClass, modules);

            var extendObject = extendObjectStorage.GetCallSite("extend_object", 1);
            var extended = extendedStorage.GetCallSite("extended", 1);
            
            // Kernel#extend_object inserts the module at the beginning of the object's singleton ancestors list;
            // ancestors after extend: [modules[0], modules[1], ..., modules[N-1], self-singleton, ...]
            for (int i = modules.Length - 1; i >= 0; i--) {
                extendObject.Target(extendObject, modules[i], self);
                extended.Target(extended, modules[i], self);
            }

            extendObject.Target(extendObject, module, self);
            extended.Target(extended, module, self);

            return self;
        }


        #region frozen?, freeze, tainted?, taint, untaint

        [RubyMethod("frozen?")]
        public static bool Frozen([NotNull]MutableString/*!*/ self) {
            return self.IsFrozen;
        }
        
        [RubyMethod("frozen?")]
        public static bool Frozen(RubyContext/*!*/ context, object self) {
            if (RubyUtils.IsRubyValueType(self)) {
                return false; // can't freeze value types
            }
            return context.IsObjectFrozen(self);
        }

        [RubyMethod("freeze")]
        public static object Freeze(RubyContext/*!*/ context, object self) {
            if (RubyUtils.IsRubyValueType(self)) {
                return self; // can't freeze value types
            }
            context.FreezeObject(self);
            return self;
        }

        [RubyMethod("tainted?")]
        public static bool Tainted(RubyContext/*!*/ context, object self) {
            if (RubyUtils.IsRubyValueType(self)) {
                return false; // can't taint value types
            }
            return context.IsObjectTainted(self);
        }

        [RubyMethod("taint")]
        public static object Taint(RubyContext/*!*/ context, object self) {
            if (RubyUtils.IsRubyValueType(self)) {
                return self;
            }
            context.SetObjectTaint(self, true);
            return self;
        }

        [RubyMethod("untaint")]
        public static object Untaint(RubyContext/*!*/ context, object self) {
            if (RubyUtils.IsRubyValueType(self)) {
                return self;
            }
            context.SetObjectTaint(self, false);
            return self;
        }

        #endregion

        [RubyMethod("instance_eval")]
        public static object Evaluate(RubyScope/*!*/ scope, object self, [NotNull]MutableString/*!*/ code,
            [Optional, NotNull]MutableString file, [DefaultParameterValue(1)]int line) {

            RubyClass singleton = scope.RubyContext.CreateSingletonClass(self);
            return RubyUtils.Evaluate(code, scope, self, singleton, file, line);
        }

        [RubyMethod("instance_eval")]
        public static object InstanceEval([NotNull]BlockParam/*!*/ block, object self) {
            return RubyUtils.EvaluateInSingleton(self, block);
        }

        #region nil?, instance_of?, is_a?, kind_of? (thread-safe)

        // thread-safe:
        [RubyMethod("nil?")]
        public static bool IsNil(object self) {
            return self == null;
        }

        // thread-safe:
        [RubyMethod("is_a?")]
        [RubyMethod("kind_of?")]
        public static bool IsKindOf(object self, RubyModule/*!*/ other) {
            ContractUtils.RequiresNotNull(other, "other");
            return other.Context.GetImmediateClassOf(self).HasAncestor(other);
        }

        // thread-safe:
        [RubyMethod("instance_of?")]
        public static bool IsOfClass(object self, RubyModule/*!*/ other) {
            ContractUtils.RequiresNotNull(other, "other");
            return other.Context.GetClassOf(self) == other;
        }

        #endregion

        #region instance_variables, instance_variable_defined?, instance_variable_get, instance_variable_set, remove_instance_variable

        [RubyMethod("instance_variables")]
        public static RubyArray/*!*/ InstanceVariables(RubyContext/*!*/ context, object self) {
            string[] names = context.GetInstanceVariableNames(self);

            RubyArray result = new RubyArray(names.Length);
            foreach (string name in names) {
                result.Add(MutableString.Create(name));
            }
            return result;
        }

        [RubyMethod("instance_variable_get")]
        public static object InstanceVariableGet(RubyContext/*!*/ context, object self, [DefaultProtocol, NotNull]string/*!*/ name) {
            object value;
            if (!context.TryGetInstanceVariable(self, name, out value)) {
                // We didn't find it, check if the name is valid
                context.CheckInstanceVariableName(name);
                return null;
            }
            return value;
        }

        [RubyMethod("instance_variable_set")]
        public static object InstanceVariableSet(RubyContext/*!*/ context, object self, [DefaultProtocol, NotNull]string/*!*/ name, object value) {
            context.CheckInstanceVariableName(name);
            context.SetInstanceVariable(self, name, value);
            return value;
        }

        [RubyMethod("instance_variable_defined?")]
        public static bool InstanceVariableDefined(RubyContext/*!*/ context, object self, [DefaultProtocol, NotNull]string/*!*/ name) {
            object value;
            if (!context.TryGetInstanceVariable(self, name, out value)) {
                // We didn't find it, check if the name is valid
                context.CheckInstanceVariableName(name);
                return false;
            }

            return true;
        }

        [RubyMethod("remove_instance_variable", RubyMethodAttributes.PrivateInstance)]
        public static object RemoveInstanceVariable(RubyContext/*!*/ context, object/*!*/ self, [DefaultProtocol, NotNull]string/*!*/ name) {
            object value;
            if (!context.TryRemoveInstanceVariable(self, name, out value)) {
                // We didn't find it, check if the name is valid
                context.CheckInstanceVariableName(name);

                throw RubyExceptions.CreateNameError(String.Format("instance variable `{0}' not defined", name));
            }

            return value;
        }

        #endregion

        [RubyMethod("respond_to?")]
        public static bool RespondTo(RubyContext/*!*/ context, object self, 
            [DefaultProtocol, NotNull]string/*!*/ methodName, [DefaultParameterValue(null)]object includePrivate) {

            return context.ResolveMethod(self, methodName, Protocols.IsTrue(includePrivate)).Found;
        }

        #region __send__, send

        [RubyMethod("send"), RubyMethod("__send__")]
        public static object SendMessage(RubyScope/*!*/ scope, object self) {
            throw RubyExceptions.CreateArgumentError("no method name given");
        }
        
        // ARGS: 0
        [RubyMethod("send"), RubyMethod("__send__")]
        public static object SendMessage(RubyScope/*!*/ scope, object self, [DefaultProtocol, NotNull]string/*!*/ methodName) {
            var site = scope.RubyContext.GetOrCreateSendSite<Func<CallSite, RubyScope, object, object>>(
                methodName, new RubyCallSignature(0, RubyCallFlags.HasScope | RubyCallFlags.HasImplicitSelf)
            );
            return site.Target(site, scope, self);
        }

        // ARGS: 0&
        [RubyMethod("send"), RubyMethod("__send__")]
        public static object SendMessage(RubyScope/*!*/ scope, BlockParam block, object self, [DefaultProtocol, NotNull]string/*!*/ methodName) {
            var site = scope.RubyContext.GetOrCreateSendSite<Func<CallSite, RubyScope, object, Proc, object>>(
                methodName, new RubyCallSignature(0, RubyCallFlags.HasScope | RubyCallFlags.HasImplicitSelf | RubyCallFlags.HasBlock)
            );
            return site.Target(site, scope, self, block != null ? block.Proc : null);
        }

        // ARGS: 1
        [RubyMethod("send"), RubyMethod("__send__")]
        public static object SendMessage(RubyScope/*!*/ scope, object self, [DefaultProtocol, NotNull]string/*!*/ methodName,
            object arg1) {

            var site = scope.RubyContext.GetOrCreateSendSite<Func<CallSite, RubyScope, object, object, object>>(
                methodName, new RubyCallSignature(1, RubyCallFlags.HasScope | RubyCallFlags.HasImplicitSelf)
            );

            return site.Target(site, scope, self, arg1);
        }

        // ARGS: 1&
        [RubyMethod("send"), RubyMethod("__send__")]
        public static object SendMessage(RubyScope/*!*/ scope, BlockParam block, object self, [DefaultProtocol, NotNull]string/*!*/ methodName,
            object arg1) {

            var site = scope.RubyContext.GetOrCreateSendSite<Func<CallSite, RubyScope, object, Proc, object, object>>(
                methodName, new RubyCallSignature(1, RubyCallFlags.HasScope | RubyCallFlags.HasImplicitSelf | RubyCallFlags.HasBlock)
            );
            return site.Target(site, scope, self, block != null ? block.Proc : null, arg1);
        }

        // ARGS: 2
        [RubyMethod("send"), RubyMethod("__send__")]
        public static object SendMessage(RubyScope/*!*/ scope, object self, [DefaultProtocol, NotNull]string/*!*/ methodName,
            object arg1, object arg2) {

            var site = scope.RubyContext.GetOrCreateSendSite<Func<CallSite, RubyScope, object, object, object, object>>(
                methodName, new RubyCallSignature(2, RubyCallFlags.HasScope | RubyCallFlags.HasImplicitSelf)
            );

            return site.Target(site, scope, self, arg1, arg2);
        }

        // ARGS: 2&
        [RubyMethod("send"), RubyMethod("__send__")]
        public static object SendMessage(RubyScope/*!*/ scope, BlockParam block, object self, [DefaultProtocol, NotNull]string/*!*/ methodName,
            object arg1, object arg2) {

            var site = scope.RubyContext.GetOrCreateSendSite<Func<CallSite, RubyScope, object, Proc, object, object, object>>(
                methodName, new RubyCallSignature(2, RubyCallFlags.HasScope | RubyCallFlags.HasImplicitSelf | RubyCallFlags.HasBlock)
            );
            return site.Target(site, scope, self, block != null ? block.Proc : null, arg1, arg2);
        }

        // ARGS: 3
        [RubyMethod("send"), RubyMethod("__send__")]
        public static object SendMessage(RubyScope/*!*/ scope, object self, [DefaultProtocol, NotNull]string/*!*/ methodName,
            object arg1, object arg2, object arg3) {

            var site = scope.RubyContext.GetOrCreateSendSite<Func<CallSite, RubyScope, object, object, object, object, object>>(
                methodName, new RubyCallSignature(3, RubyCallFlags.HasScope | RubyCallFlags.HasImplicitSelf)
            );

            return site.Target(site, scope, self, arg1, arg2, arg3);
        }

        // ARGS: 3&
        [RubyMethod("send"), RubyMethod("__send__")]
        public static object SendMessage(RubyScope/*!*/ scope, BlockParam block, object self, [DefaultProtocol, NotNull]string/*!*/ methodName,
            object arg1, object arg2, object arg3) {

            var site = scope.RubyContext.GetOrCreateSendSite<Func<CallSite, RubyScope, object, Proc, object, object, object, object>>(
                methodName, new RubyCallSignature(3, RubyCallFlags.HasScope | RubyCallFlags.HasImplicitSelf | RubyCallFlags.HasBlock)
            );
            return site.Target(site, scope, self, block != null ? block.Proc : null, arg1, arg2, arg3);
        }

        // ARGS: N
        [RubyMethod("send"), RubyMethod("__send__")]
        public static object SendMessage(RubyScope/*!*/ scope, object self, [DefaultProtocol, NotNull]string/*!*/ methodName,
            [NotNull]params object[] args) {

            var site = scope.RubyContext.GetOrCreateSendSite<Func<CallSite, RubyScope, object, RubyArray, object>>(
                methodName, new RubyCallSignature(1, RubyCallFlags.HasScope | RubyCallFlags.HasImplicitSelf | RubyCallFlags.HasSplattedArgument)
            );

            return site.Target(site, scope, self, RubyOps.MakeArrayN(args));
        }

        // ARGS: N&
        [RubyMethod("send"), RubyMethod("__send__")]
        public static object SendMessage(RubyScope/*!*/ scope, BlockParam block, object self, [DefaultProtocol, NotNull]string/*!*/ methodName,
            [NotNull]params object[] args) {

            var site = scope.RubyContext.GetOrCreateSendSite<Func<CallSite, RubyScope, object, Proc, RubyArray, object>>(
                methodName, new RubyCallSignature(1, RubyCallFlags.HasScope | RubyCallFlags.HasImplicitSelf | RubyCallFlags.HasSplattedArgument | 
                    RubyCallFlags.HasBlock)
            );
            return site.Target(site, scope, self, block != null ? block.Proc : null, RubyOps.MakeArrayN(args));
        }

        internal static object SendMessageOpt(RubyScope/*!*/ scope, BlockParam block, object self, string/*!*/ methodName, object[] args) {
            switch ((args != null ? args.Length : 0)) {
                case 0: return SendMessage(scope, block, self, methodName);
                case 1: return SendMessage(scope, block, self, methodName, args[0]);
                case 2: return SendMessage(scope, block, self, methodName, args[0], args[1]);
                case 3: return SendMessage(scope, block, self, methodName, args[0], args[1], args[2]);
                default: return SendMessage(scope, block, self, methodName, args);
            }
        }

        #endregion

        #region method, methods, (private|protected|public|singleton)_methods (thread-safe)

        // thread-safe:
        [RubyMethod("method")]
        public static RubyMethod/*!*/ GetMethod(RubyContext/*!*/ context, object self, [DefaultProtocol, NotNull]string/*!*/ name) {
            RubyMemberInfo info = context.ResolveMethod(self, name, RubyClass.IgnoreVisibility).Info;
            if (info == null) {
                throw RubyExceptions.CreateUndefinedMethodError(context.GetClassOf(self), name);
            }
            return new RubyMethod(self, info, name);
        }

        // thread-safe:
        [RubyMethod("methods")]
        public static RubyArray/*!*/ GetMethods(RubyContext/*!*/ context, object self, [DefaultParameterValue(true)]bool inherited) {
            var foreignMembers = context.GetForeignDynamicMemberNames(self);

            RubyClass immediateClass = context.GetImmediateClassOf(self);
            if (!inherited && !immediateClass.IsSingletonClass) {
                var result = new RubyArray();
                if (foreignMembers.Count > 0) {
                    var symbolicNames = context.RubyOptions.Compatibility > RubyCompatibility.Ruby18;
                    foreach (var name in foreignMembers) {
                        result.Add(ModuleOps.CreateMethodName(name, symbolicNames));
                    }
                }
                return result;
            }

            return ModuleOps.GetMethods(immediateClass, inherited, RubyMethodAttributes.Public | RubyMethodAttributes.Protected, foreignMembers);
        }

        // thread-safe:
        [RubyMethod("singleton_methods")]
        public static RubyArray/*!*/ GetSingletonMethods(RubyContext/*!*/ context, object self, [DefaultParameterValue(true)]bool inherited) {
            RubyClass immediateClass = context.GetImmediateClassOf(self);
            return ModuleOps.GetMethods(immediateClass, inherited, RubyMethodAttributes.Singleton | RubyMethodAttributes.Public | RubyMethodAttributes.Protected);
        }

        // thread-safe:
        [RubyMethod("private_methods")]
        public static RubyArray/*!*/ GetPrivateMethods(RubyContext/*!*/ context, object self, [DefaultParameterValue(true)]bool inherited) {
            return GetMethods(context, self, inherited, RubyMethodAttributes.PrivateInstance);
        }

        // thread-safe:
        [RubyMethod("protected_methods")]
        public static RubyArray/*!*/ GetProtectedMethods(RubyContext/*!*/ context, object self, [DefaultParameterValue(true)]bool inherited) {
            return GetMethods(context, self, inherited, RubyMethodAttributes.ProtectedInstance);
        }

        // thread-safe:
        [RubyMethod("public_methods")]
        public static RubyArray/*!*/ GetPublicMethods(RubyContext/*!*/ context, object self, [DefaultParameterValue(true)]bool inherited) {
            return GetMethods(context, self, inherited, RubyMethodAttributes.PublicInstance);
        }

        private static RubyArray/*!*/ GetMethods(RubyContext/*!*/ context, object self, bool inherited, RubyMethodAttributes attributes) {
            RubyClass immediateClass = context.GetImmediateClassOf(self);
            return ModuleOps.GetMethods(immediateClass, inherited, attributes);
        }

        #endregion

        #region inspect, to_s

        /// <summary>
        /// Returns a string containing a human-readable representation of obj.
        /// If not overridden, uses the to_s method to generate the string. 
        /// </summary>
        [RubyMethod("inspect")]
        public static MutableString/*!*/ Inspect(UnaryOpStorage/*!*/ inspectStorage, ConversionStorage<MutableString>/*!*/ tosConversion, 
            object self) {

            var context = tosConversion.Context;
            if (context.HasInstanceVariables(self)) {
                return RubyUtils.InspectObject(inspectStorage, tosConversion, self);
            } else {
                var site = tosConversion.GetSite(ConvertToSAction.Make(context));
                return site.Target(site, self);
            }
        }

        [RubyMethod("to_s")]
        public static MutableString/*!*/ ToS(RubyContext/*!*/ context, [NotNull]object/*!*/ self) {
            if (self.GetType() == typeof(object)) {
                return RubyUtils.ObjectToMutableString(context, self);
            }

            // maps to CLR's ToString:
            return MutableString.Create(self.ToString()).TaintBy(self, context);
        }

        [RubyMethod("to_s")]
        public static MutableString/*!*/ ToS([NotNull]RubyObject/*!*/ self) {
            // default representation #<{class-name}: {object-id}> 
            return self.ToMutableString();
        }

        [RubyMethod("to_a")]
        public static RubyArray/*!*/ ToA(RubyContext/*!*/ context, object self) {
            RubyArray result = new RubyArray();
            result.Add(self);
            return context.TaintObjectBy(result, self);
        }

        #endregion

        #endregion
    }
}

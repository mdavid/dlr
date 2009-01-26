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
using Microsoft.Scripting;
using Microsoft.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Runtime.CompilerServices;

using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using IronRuby.Builtins;
using IronRuby.Compiler;
using IronRuby.Compiler.Generation;
using IronRuby.Runtime.Calls;
using Microsoft.Scripting.Actions;
using Microsoft.Scripting.Math;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Scripting.Generation;

namespace IronRuby.Runtime {
    public static partial class RubyOps {

        [Emitted]
        public static readonly object/*!*/ DefaultArgument = new object();
        
        [Emitted]
        public static readonly object/*!*/ MethodNotFound = new object();

        #region Scopes

        [Emitted]
        public static RubyTopLevelScope/*!*/ CreateMainTopLevelScope(LocalsDictionary/*!*/ locals, Scope/*!*/ globalScope, LanguageContext/*!*/ language,
            out object self, out RuntimeFlowControl/*!*/ rfc, string dataPath, int dataOffset) {
            Assert.NotNull(locals, globalScope, language);

            RubyContext context = (RubyContext)language;
            RubyGlobalScope rubyGlobalScope = context.InitializeGlobalScope(globalScope, false);

            RubyTopLevelScope scope = new RubyTopLevelScope(rubyGlobalScope, null, locals);
            scope.Initialize(new RuntimeFlowControl(), RubyMethodAttributes.PrivateInstance, rubyGlobalScope.MainObject);
            scope.SetDebugName("top-main");

            var objectClass = context.ObjectClass;
            objectClass.SetConstant("TOPLEVEL_BINDING", new Binding(scope));
            if (dataOffset >= 0) {
                RubyFile dataFile;
                if (File.Exists(dataPath)) {
                    dataFile = new RubyFile(context, dataPath, RubyFileMode.RDONLY);
                    dataFile.Seek(dataOffset, SeekOrigin.Begin);
                } else {
                    dataFile = null;
                }

                objectClass.SetConstant("DATA", dataFile);
            }

            self = scope.SelfObject;
            rfc = scope.RuntimeFlowControl;

            return scope;
        }

        [Emitted]
        public static RubyTopLevelScope/*!*/ CreateTopLevelHostedScope(LocalsDictionary/*!*/ locals, Scope/*!*/ globalScope, LanguageContext/*!*/ language,
            out object self, out RuntimeFlowControl/*!*/ rfc) {

            RubyContext context = (RubyContext)language;
            RubyGlobalScope rubyGlobalScope = context.InitializeGlobalScope(globalScope, true);

            // reuse existing top-level scope if available:
            RubyTopLevelScope scope = rubyGlobalScope.TopLocalScope;
            if (scope == null) {
                scope = new RubyTopLevelScope(rubyGlobalScope, null, locals);
                scope.Initialize(new RuntimeFlowControl(), RubyMethodAttributes.PrivateInstance, rubyGlobalScope.MainObject);
                scope.SetDebugName("top-level-hosted");
                rubyGlobalScope.TopLocalScope = scope;
            }

            self = scope.SelfObject;
            rfc = scope.RuntimeFlowControl;
            return scope;
        }

        [Emitted]
        public static RubyTopLevelScope/*!*/ CreateTopLevelScope(LocalsDictionary/*!*/ locals, Scope/*!*/ globalScope, LanguageContext/*!*/ language,
            out object self, out RuntimeFlowControl/*!*/ rfc) {

            RubyContext context = (RubyContext)language;
            RubyGlobalScope rubyGlobalScope = context.InitializeGlobalScope(globalScope, false);

            RubyTopLevelScope scope = new RubyTopLevelScope(rubyGlobalScope, null, locals);
            scope.Initialize(new RuntimeFlowControl(), RubyMethodAttributes.PrivateInstance, rubyGlobalScope.MainObject);
            scope.SetDebugName("top-level");

            self = scope.SelfObject;
            rfc = scope.RuntimeFlowControl;
            return scope;
        }

        [Emitted]
        public static RubyTopLevelScope/*!*/ CreateWrappedTopLevelScope(LocalsDictionary/*!*/ locals, Scope/*!*/ globalScope, LanguageContext/*!*/ language,
            out object self, out RuntimeFlowControl/*!*/ rfc) {

            RubyContext context = (RubyContext)language;

            RubyModule module = context.CreateModule(null, null, null, null, null, null, null);
            object mainObject = new Object();
            RubyClass mainSingleton = context.CreateMainSingleton(mainObject, new[] { module });

            RubyGlobalScope rubyGlobalScope = context.InitializeGlobalScope(globalScope, false);
            RubyTopLevelScope scope = new RubyTopLevelScope(rubyGlobalScope, null, locals);
            scope.Initialize(new RuntimeFlowControl(), RubyMethodAttributes.PrivateInstance, rubyGlobalScope.MainObject);
            scope.SetDebugName("top-level-wrapped");
            scope.SelfObject = mainObject;
            scope.SetModule(module);

            self = scope.SelfObject;
            rfc = scope.RuntimeFlowControl;
            return scope;
        }

        [Emitted]
        public static RubyModuleScope/*!*/ CreateModuleEvalScope(LocalsDictionary/*!*/ locals, RubyScope/*!*/ parent, object self, RubyModule module) {
            RubyModuleScope scope = new RubyModuleScope(parent, locals, module, true);
            scope.Initialize(parent.RuntimeFlowControl, RubyMethodAttributes.PublicInstance, self);
            scope.SetDebugName("top-module/instance-eval");                
            return scope;
        }
        
        [Emitted]
        public static RubyModuleScope/*!*/ CreateModuleScope(LocalsDictionary/*!*/ locals, RubyScope/*!*/ parent, 
            RuntimeFlowControl/*!*/ rfc, RubyModule/*!*/ module) {
            Assert.NotNull(locals, parent, rfc, module);

            // TODO:
            RubyModuleScope scope = new RubyModuleScope(parent, locals, null, false);
            scope.Initialize(rfc, RubyMethodAttributes.PublicInstance, module);
            scope.SetModule(module);
            scope.SetDebugName((module.IsClass ? "class" : "module") + " " + module.Name);

            return scope;
        }

        [Emitted]
        public static RubyMethodScope/*!*/ CreateMethodScope(LocalsDictionary/*!*/ locals, RubyScope/*!*/ parent, 
            RubyMethodInfo/*!*/ methodDefinition, RuntimeFlowControl/*!*/ rfc, object selfObject, Proc blockParameter) {

            Assert.NotNull(locals, parent, methodDefinition, rfc);

            RubyMethodScope scope = new RubyMethodScope(parent, locals, methodDefinition, blockParameter);
            scope.Initialize(rfc, RubyMethodAttributes.PublicInstance, selfObject);

            scope.SetDebugName("method " + 
                methodDefinition.DefinitionName +
                ((blockParameter != null) ? "&" : null)
            );

            return scope;
        }

        [Emitted]
        public static RubyBlockScope/*!*/ CreateBlockScope(LocalsDictionary/*!*/ locals, RubyScope/*!*/ parent, 
            BlockParam/*!*/ blockParam, object selfObject) {
            Assert.NotNull(locals, parent, blockParam);

            RubyBlockScope scope = new RubyBlockScope(parent, locals);
            // TODO: used to inherit parent.MethodAttributes
            scope.Initialize(parent.RuntimeFlowControl, RubyMethodAttributes.PublicInstance, selfObject); 
            scope.BlockParameter = blockParam;

            return scope;
        }

        [Emitted]
        public static void TraceMethodCall(RubyMethodScope/*!*/ scope, string fileName, int lineNumber) {
            // MRI: 
            // Reports DeclaringModule even though an aliased method in a sub-module is called.
            // Also works for singleton module-function, which shares DeclaringModule with instance module-function.
            RubyModule module = scope.Method.DeclaringModule;
            scope.RubyContext.ReportTraceEvent("call", scope, module, scope.Method.DefinitionName, fileName, lineNumber);
        }

        [Emitted]
        public static void TraceMethodReturn(RubyMethodScope/*!*/ scope, string fileName, int lineNumber) {
            RubyModule module = scope.Method.DeclaringModule;
            scope.RubyContext.ReportTraceEvent("return", scope, module, scope.Method.DefinitionName, fileName, lineNumber);
        }

        [Emitted]
        public static void TraceBlockCall(RubyBlockScope/*!*/ scope, BlockParam/*!*/ block, string fileName, int lineNumber) {
            if (block.ModuleDeclaration != null && block.SuperMethodName != null) {
                scope.RubyContext.ReportTraceEvent("call", scope, block.ModuleDeclaration, block.SuperMethodName, fileName, lineNumber);
            }
        }

        [Emitted]
        public static void TraceBlockReturn(RubyBlockScope/*!*/ scope, BlockParam/*!*/ block, string fileName, int lineNumber) {
            if (block.ModuleDeclaration != null && block.SuperMethodName != null) {
                scope.RubyContext.ReportTraceEvent("return", scope, block.ModuleDeclaration, block.SuperMethodName, fileName, lineNumber);
            }
        }

        // TODO: move to the host
        [Emitted]
        public static void PrintInteractiveResult(RubyScope/*!*/ scope, MutableString/*!*/ value) {
            var writer = scope.RubyContext.DomainManager.SharedIO.OutputStream;
            writer.WriteByte((byte)'=');
            writer.WriteByte((byte)'>');
            writer.WriteByte((byte)' ');
            var bytes = value.ToByteArray();
            writer.Write(bytes, 0, bytes.Length);
            writer.WriteByte((byte)'\r');
            writer.WriteByte((byte)'\n');
        }

        [Emitted]
        public static object GetLocalVariable(RubyScope/*!*/ scope, string/*!*/ name) {
            return scope.ResolveLocalVariable(name);
        }

        [Emitted]
        public static object SetLocalVariable(object value, RubyScope/*!*/ scope, string/*!*/ name) {
            return scope.Frame[SymbolTable.StringToId(name)] = value;
        }

        #endregion

        #region Context

        [Emitted]
        public static RubyContext/*!*/ GetContextFromScope(RubyScope/*!*/ scope) {
            return scope.RubyContext;
        }

        [Emitted]
        public static RubyContext/*!*/ GetContextFromMethod(RubyMethod/*!*/ method) {
            return method.Info.Context;
        }

        [Emitted]
        public static RubyContext/*!*/ GetContextFromBlockParam(BlockParam/*!*/ block) {
            return block.RubyContext;
        }

        [Emitted]
        public static RubyContext/*!*/ GetContextFromProc(Proc/*!*/ proc) {
            return proc.LocalScope.RubyContext;
        }

        [Emitted]
        public static RubyScope/*!*/ GetEmptyScope(RubyContext/*!*/ context) {
            return context.EmptyScope;
        }

        #endregion

        #region Blocks

        [Emitted]
        public static Proc/*!*/ DefineBlock(RubyScope/*!*/ scope, RuntimeFlowControl/*!*/ runtimeFlowControl, object self, Delegate/*!*/ clrMethod,
            int parameterCount, BlockSignatureAttributes attributes) {
            Assert.NotNull(scope, clrMethod);

            // closes block over self and context
            BlockDispatcher dispatcher = BlockDispatcher.Create(clrMethod, parameterCount, attributes);
            Proc result = new Proc(ProcKind.Block, self, scope, dispatcher);

            result.Owner = runtimeFlowControl;
            return result;
        }

        [Emitted] 
        /// <summary>
        /// Used in a method call with a block to reset proc-kind when the call is retried
        /// </summary>
        public static void InitializeBlock(Proc/*!*/ proc) {
            Assert.NotNull(proc);
            proc.Kind = ProcKind.Block;
        }

        #endregion

        #region Yield: TODO: generate

        [Emitted] 
        public static object Yield0(object self, BlockParam/*!*/ blockParam) {
            object result;
            var proc = blockParam.Proc;
            try {
                result = proc.Dispatcher.Invoke(blockParam, self);
            } catch(EvalUnwinder evalUnwinder) {
                result = blockParam.GetUnwinderResult(evalUnwinder);
            }

            return result;
        }

        [Emitted] 
        public static object Yield1(object arg1, object self, BlockParam/*!*/ blockParam) {
            object result;
            var proc = blockParam.Proc;
            try {
                result = proc.Dispatcher.Invoke(blockParam, self, arg1);
            } catch (EvalUnwinder evalUnwinder) {
                result = blockParam.GetUnwinderResult(evalUnwinder);
            }

            return result;
        }

        // YieldNoAutoSplat1 uses InvokeNoAutoSplat instead of Invoke (used by Call1)
        internal static object YieldNoAutoSplat1(object arg1, object self, BlockParam/*!*/ blockParam) {
            object result;
            var proc = blockParam.Proc;
            try {
                result = proc.Dispatcher.InvokeNoAutoSplat(blockParam, self, arg1);
            } catch (EvalUnwinder evalUnwinder) {
                result = blockParam.GetUnwinderResult(evalUnwinder);
            }

            return result;
        }

        [Emitted] 
        public static object Yield2(object arg1, object arg2, object self, BlockParam/*!*/ blockParam) {
            object result;
            var proc = blockParam.Proc;
            try {
                result = proc.Dispatcher.Invoke(blockParam, self, arg1, arg2);
            } catch (EvalUnwinder evalUnwinder) {
                result = blockParam.GetUnwinderResult(evalUnwinder);
            }

            return result;
        }

        [Emitted] 
        public static object Yield3(object arg1, object arg2, object arg3, object self, BlockParam/*!*/ blockParam) {
            object result;
            var proc = blockParam.Proc;
            try {
                result = proc.Dispatcher.Invoke(blockParam, self, arg1, arg2, arg3);
            } catch (EvalUnwinder evalUnwinder) {
                result = blockParam.GetUnwinderResult(evalUnwinder);
            }

            return result;
        }

        [Emitted] 
        public static object Yield4(object arg1, object arg2, object arg3, object arg4, object self, BlockParam/*!*/ blockParam) {
            object result;
            var proc = blockParam.Proc;
            try {
                result = proc.Dispatcher.Invoke(blockParam, self, arg1, arg2, arg3, arg4);
            } catch (EvalUnwinder evalUnwinder) {
                result = blockParam.GetUnwinderResult(evalUnwinder);
            }

            return result;
        }

        [Emitted] 
        public static object YieldN(object[]/*!*/ args, object self, BlockParam/*!*/ blockParam) {
            Debug.Assert(args.Length > BlockDispatcher.MaxBlockArity);

            object result;
            var proc = blockParam.Proc;
            try {
                result = proc.Dispatcher.Invoke(blockParam, self, args);
            } catch (EvalUnwinder evalUnwinder) {
                result = blockParam.GetUnwinderResult(evalUnwinder);
            }

            return result;
        }

        [Emitted] 
        public static object YieldSplat0(object splattee, object self, BlockParam/*!*/ blockParam) {
            object result;
            var proc = blockParam.Proc;
            try {
                result = proc.Dispatcher.InvokeSplat(blockParam, self, splattee);
            } catch (EvalUnwinder evalUnwinder) {
                result = blockParam.GetUnwinderResult(evalUnwinder);
            }

            return result;
        }

        [Emitted] 
        public static object YieldSplat1(object arg1, object splattee, object self, BlockParam/*!*/ blockParam) {
            object result;
            var proc = blockParam.Proc;
            try {
                result = proc.Dispatcher.InvokeSplat(blockParam, self, arg1, splattee);
            } catch (EvalUnwinder evalUnwinder) {
                result = blockParam.GetUnwinderResult(evalUnwinder);
            }

            return result;
        }

        [Emitted] 
        public static object YieldSplat2(object arg1, object arg2, object splattee, object self, BlockParam/*!*/ blockParam) {
            object result;
            var proc = blockParam.Proc;
            try {
                result = proc.Dispatcher.InvokeSplat(blockParam, self, arg1, arg2, splattee);
            } catch (EvalUnwinder evalUnwinder) {
                result = blockParam.GetUnwinderResult(evalUnwinder);
            }

            return result;
        }

        [Emitted] 
        public static object YieldSplat3(object arg1, object arg2, object arg3, object splattee, object self, BlockParam/*!*/ blockParam) {
            object result;
            var proc = blockParam.Proc;
            try {
                result = proc.Dispatcher.InvokeSplat(blockParam, self, arg1, arg2, arg3, splattee);
            } catch (EvalUnwinder evalUnwinder) {
                result = blockParam.GetUnwinderResult(evalUnwinder);
            }

            return result;
        }

        [Emitted] 
        public static object YieldSplat4(object arg1, object arg2, object arg3, object arg4, object splattee, object self, BlockParam/*!*/ blockParam) {
            object result;
            var proc = blockParam.Proc;
            try {
                result = proc.Dispatcher.InvokeSplat(blockParam, self, arg1, arg2, arg3, arg4, splattee);
            } catch (EvalUnwinder evalUnwinder) {
                result = blockParam.GetUnwinderResult(evalUnwinder);
            }

            return result;
        }

        [Emitted] 
        public static object YieldSplatN(object[]/*!*/ args, object splattee, object self, BlockParam/*!*/ blockParam) {
            object result;
            var proc = blockParam.Proc;
            try {
                result = proc.Dispatcher.InvokeSplat(blockParam, self, args, splattee);
            } catch (EvalUnwinder evalUnwinder) {
                result = blockParam.GetUnwinderResult(evalUnwinder);
            }

            return result;
        }

        [Emitted] 
        public static object YieldSplatNRhs(object[]/*!*/ args, object splattee, object rhs, object self, BlockParam/*!*/ blockParam) {
            object result;
            var proc = blockParam.Proc;
            try {
                result = proc.Dispatcher.InvokeSplatRhs(blockParam, self, args, splattee, rhs);
            } catch (EvalUnwinder evalUnwinder) {
                result = blockParam.GetUnwinderResult(evalUnwinder);
            }

            return result;
        }
        #endregion

        #region Methods

        [Emitted] // MethodDeclaration:
        public static RubyMethodInfo/*!*/ DefineMethod(object targetOrSelf, object/*!*/ ast, RubyScope/*!*/ scope,
            bool hasTarget, string/*!*/ name, Delegate/*!*/ clrMethod, int mandatory, int optional, bool hasUnsplatParameter) {

            Assert.NotNull(ast, scope, clrMethod, name);

            RubyModule instanceOwner, singletonOwner;
            RubyMemberFlags instanceFlags, singletonFlags;

            if (hasTarget) {
                if (!RubyUtils.CanCreateSingleton(targetOrSelf)) {
                    throw RubyExceptions.CreateTypeError("can't define singleton method for literals");
                }

                instanceOwner = null;
                instanceFlags = RubyMemberFlags.Invalid;
                singletonOwner = scope.RubyContext.CreateSingletonClass(targetOrSelf);
                singletonFlags = RubyMemberFlags.Public;
            } else {
                // TODO: ???
                var attributesScope = scope.GetMethodAttributesDefinitionScope();
                //var attributesScope = scope;
                if ((attributesScope.MethodAttributes & RubyMethodAttributes.ModuleFunction) == RubyMethodAttributes.ModuleFunction) {
                    // Singleton module-function's scope points to the instance method's RubyMemberInfo.
                    // This affects:
                    // 1) super call
                    //    Super call is looking for Method.DeclaringModule while searching MRO, which would fail if the singleton module-function
                    //    was in MRO. Since module-function can only be used on module the singleton method could only be on module's singleton.
                    //    Module's singleton is never part of MRO so we are safe.
                    // 2) trace
                    //    Method call trace reports non-singleton module.

                    // MRI 1.8: instance method owner is self -> it is possible (via define_method) to define m.f. on a class (bug)
                    // MRI 1.9: instance method owner GetMethodDefinitionOwner
                    // MRI allows to define m.f. on classes but then doesn't work correctly with it.
                    instanceOwner = scope.GetMethodDefinitionOwner();
                    if (instanceOwner.IsClass) {
                        throw RubyExceptions.CreateTypeError("A module function cannot be defined on a class.");
                    }

                    instanceFlags = RubyMemberFlags.ModuleFunction | RubyMemberFlags.Private;
                    singletonOwner = instanceOwner.SingletonClass;
                    singletonFlags = RubyMemberFlags.ModuleFunction | RubyMemberFlags.Public;
                } else {
                    instanceOwner = scope.GetMethodDefinitionOwner();
                    instanceFlags = (RubyMemberFlags)RubyUtils.GetSpecialMethodVisibility(attributesScope.Visibility, name);
                    singletonOwner = null;
                    singletonFlags = RubyMemberFlags.Invalid;
                }
            }
            
            RubyMethodInfo instanceMethod = null, singletonMethod = null;

            if (instanceOwner != null) {
                SetMethod(scope.RubyContext, instanceMethod = 
                    new RubyMethodInfo(ast, clrMethod, instanceOwner, name, mandatory, optional, hasUnsplatParameter, instanceFlags)
                );
            }

            if (singletonOwner != null) {
                SetMethod(scope.RubyContext, singletonMethod =
                    new RubyMethodInfo(ast, clrMethod, singletonOwner, name, mandatory, optional, hasUnsplatParameter, singletonFlags)
                );
            }

            // the method's scope saves the result => singleton module-function uses instance-method
            return instanceMethod ?? singletonMethod;
        }

        private static void SetMethod(RubyContext/*!*/ callerContext, RubyMethodInfo/*!*/ method) {
            var owner = method.DeclaringModule;

            // Do not trigger the add-method event just yet, we need to assign the result into closure before executing any user code.
            // If the method being defined is "method_added" itself, we would call that method before the info gets assigned to the closure.
            owner.SetMethodNoEvent(callerContext, method.DefinitionName, method);

            // expose RubyMethod in the scope (the method is bound to the main singleton instance):
            if (owner.GlobalScope != null) {
                owner.GlobalScope.Scope.SetName(
                    SymbolTable.StringToId(method.DefinitionName),
                    new RubyMethod(owner.GlobalScope.MainObject, method, method.DefinitionName)
                );
            }
        }

        [Emitted]
        public static object MethodDefined(RubyMethodInfo/*!*/ method) {
            method.Context.MethodAdded(method.DeclaringModule, method.DefinitionName);
            
            if (method.IsModuleFunction) {
                Debug.Assert(!method.DeclaringModule.IsClass);
                method.Context.MethodAdded(method.DeclaringModule.SingletonClass, method.DefinitionName);
            }

            return null;
        }

        [Emitted] // AliasStatement:
        public static void AliasMethod(RubyScope/*!*/ scope, string/*!*/ newName, string/*!*/ oldName) {
            scope.GetMethodDefinitionOwner().AddMethodAlias(newName, oldName);
        }

        [Emitted] // UndefineMethod:
        public static void UndefineMethod(RubyScope/*!*/ scope, string/*!*/ name) {
            RubyModule owner = scope.GetInnerMostModule();

            if (owner.ResolveMethod(name, true) == null) {
                throw RubyExceptions.CreateUndefinedMethodError(owner, name);
            }
            owner.UndefineMethod(name);
        }

        [Emitted] // MethodCall:
        public static bool IsDefinedMethod(object self, RubyScope/*!*/ scope, string/*!*/ name) {
            // MRI: this is different from UndefineMethod, it behaves like Kernel#method (i.e. doesn't use lexical scope):
            // TODO: visibility
            return scope.RubyContext.ResolveMethod(self, name, true) != null;
        }

        #endregion

        #region Modules

        [Emitted]
        public static RubyModule/*!*/ DefineGlobalModule(RubyScope/*!*/ scope, string/*!*/ name) {
            return RubyUtils.DefineModule(scope.GlobalScope, scope.Top.TopModuleOrObject, name);
        }

        [Emitted]
        public static RubyModule/*!*/ DefineNestedModule(RubyScope/*!*/ scope, string/*!*/ name) {
            return RubyUtils.DefineModule(scope.GlobalScope, scope.GetInnerMostModule(), name);
        }

        [Emitted]
        public static RubyModule/*!*/ DefineModule(RubyScope/*!*/ scope, object target, string/*!*/ name) {
            Assert.NotNull(scope);
            return RubyUtils.DefineModule(scope.GlobalScope, RubyUtils.GetModuleFromObject(scope.RubyContext, target), name);
        }

        #endregion

        #region Classes

        [Emitted]
        public static RubyClass/*!*/ DefineSingletonClass(RubyScope/*!*/ scope, object obj) {
            if (obj != null && !(obj is bool) && RubyUtils.IsRubyValueType(obj)) {
                throw RubyExceptions.CreateTypeError(String.Format("no virtual class for {0}", scope.RubyContext.GetClassOf(obj).Name));
            }
            return scope.RubyContext.CreateSingletonClass(obj);
        }

        [Emitted] 
        public static RubyModule/*!*/ DefineGlobalClass(RubyScope/*!*/ scope, string/*!*/ name, object superClassObject) {
            return RubyUtils.DefineClass(scope.GlobalScope, scope.Top.TopModuleOrObject, name, superClassObject);
        }

        [Emitted]
        public static RubyModule/*!*/ DefineNestedClass(RubyScope/*!*/ scope, string/*!*/ name, object superClassObject) {
            return RubyUtils.DefineClass(scope.GlobalScope, scope.GetInnerMostModule(), name, superClassObject);
        }

        [Emitted]
        public static RubyModule/*!*/ DefineClass(RubyScope/*!*/ scope, object target, string/*!*/ name, object superClassObject) {
            return RubyUtils.DefineClass(scope.GlobalScope, RubyUtils.GetModuleFromObject(scope.RubyContext, target), name, superClassObject);
        }

        #endregion

        #region Constants

        [Emitted] // ConstantVariable:
        public static object GetGlobalConstant(RubyScope/*!*/ scope, string/*!*/ name) {
            return RubyUtils.GetConstant(scope.GlobalScope, scope.RubyContext.ObjectClass, name, false);
        }

        [Emitted] // ConstantVariable:
        public static object GetUnqualifiedConstant(RubyScope/*!*/ scope, string/*!*/ name) {
            return scope.ResolveConstant(true, name);
        }

        [Emitted] // ConstantVariable:
        public static object GetQualifiedConstant(object target, RubyScope/*!*/ scope, string/*!*/ name) {
            return RubyUtils.GetConstant(scope.GlobalScope, RubyUtils.GetModuleFromObject(scope.RubyContext, target), name, false);
        }


        [Emitted] // ConstantVariable:
        public static bool IsDefinedGlobalConstant(RubyScope/*!*/ scope, string/*!*/ name) {
            object result;
            return scope.RubyContext.ObjectClass.TryResolveConstantNoAutoload(name, out result);
        }

        [Emitted] // ConstantVariable:
        public static bool IsDefinedUnqualifiedConstant(RubyScope/*!*/ scope, string/*!*/ name) {
            object result;
            return scope.TryResolveConstant(false, name, out result);
        }

        [Emitted] // ConstantVariable:
        public static bool IsDefinedQualifiedConstant(object target, RubyScope/*!*/ scope, string/*!*/ name) {
            object result;
            RubyModule module = target as RubyModule;
            if (module == null) {
                return false;
            }
            return module.TryResolveConstantNoAutoload(name, out result);
        }


        [Emitted] // ConstantVariable:
        public static object SetGlobalConstant(object value, RubyScope/*!*/ scope, string/*!*/ name) {
            RubyUtils.SetConstant(scope.RubyContext.ObjectClass, name, value);
            return value;
        }

        [Emitted] // ConstantVariable:
        public static object SetUnqualifiedConstant(object value, RubyScope/*!*/ scope, string/*!*/ name) {
            RubyUtils.SetConstant(scope.GetInnerMostModule(), name, value);
            return value;
        }

        [Emitted] // ConstantVariable:
        public static object SetQualifiedConstant(object value, object target, RubyScope/*!*/ scope, string/*!*/ name) {
            RubyUtils.SetConstant(RubyUtils.GetModuleFromObject(scope.RubyContext, target), name, value);
            return value;
        }

        #endregion

        // MakeArray*
        public const int OptimizedOpCallParamCount = 5;
        
        #region MakeArray
        
        [Emitted]
        public static RubyArray/*!*/ MakeArray0() {
            return new RubyArray(0);
        }

        [Emitted]
        public static RubyArray/*!*/ MakeArray1(object item1) {
            RubyArray result = new RubyArray(1);
            result.Add(item1);
            return result;
        }

        [Emitted]
        public static RubyArray/*!*/ MakeArray2(object item1, object item2) {
            RubyArray result = new RubyArray(2);
            result.Add(item1);
            result.Add(item2);
            return result;
        }

        [Emitted]
        public static RubyArray/*!*/ MakeArray3(object item1, object item2, object item3) {
            RubyArray result = new RubyArray(3);
            result.Add(item1);
            result.Add(item2);
            result.Add(item3);
            return result;
        }

        [Emitted]
        public static RubyArray/*!*/ MakeArray4(object item1, object item2, object item3, object item4) {
            RubyArray result = new RubyArray(4);
            result.Add(item1);
            result.Add(item2);
            result.Add(item3);
            result.Add(item4);
            return result;
        }

        [Emitted]
        public static RubyArray/*!*/ MakeArray5(object item1, object item2, object item3, object item4, object item5) {
            RubyArray result = new RubyArray(5);
            result.Add(item1);
            result.Add(item2);
            result.Add(item3);
            result.Add(item4);
            result.Add(item5);
            return result;
        }

        [Emitted]
        public static RubyArray/*!*/ MakeArrayN(object[]/*!*/ items) {
            Debug.Assert(items != null);
            return new RubyArray(items);
        }

        #endregion

        #region MakeHash

        [Emitted]
        public static Hash/*!*/ MakeHash0(RubyScope/*!*/ scope) {
            return new Hash(scope.RubyContext.EqualityComparer, 0);
        }
        
        [Emitted]
        public static Hash/*!*/ MakeHash(RubyScope/*!*/ scope, object[]/*!*/ items) {
            return RubyUtils.SetHashElements(scope.RubyContext, new Hash(scope.RubyContext.EqualityComparer, items.Length / 2), items);
        }

        #endregion

        #region Array

        [Emitted]
        public static List<object>/*!*/ SplatAppend(List<object>/*!*/ array, object splattee) {
            List<object> list = splattee as List<object>;
            if (list != null) {
                array.AddRange(list);
            } else {
                array.Add(splattee);
            }
            return array;
        }

        [Emitted]
        public static object Splat(object/*!*/ value) {
            List<object> list = value as List<object>;
            if (list == null) {
                return value;
            }

            if (list.Count <= 1) {
                return (list.Count > 0) ? list[0] : null;
            }

            return list;
        }

        [Emitted]
        public static object SplatPair(object value, object array) {
            List<object> list = array as List<object>;
            if (list != null) {
                if (list.Count == 0) {
                    return value;
                }

                RubyArray result = new RubyArray(list.Count + 1);
                result.Add(value);
                result.AddRange(list);
                return result;
            }

            return MakeArray2(value, array);
        }

        [Emitted]
        public static RubyArray/*!*/ Unsplat(object/*!*/ value) {
            RubyArray list = value as RubyArray;
            if (list == null) {
                list = new RubyArray(1);
                list.Add(value);
            }
            return list;
        }

        [Emitted] // parallel assignment:
        public static object GetArrayItem(List<object>/*!*/ array, int index) {
            Debug.Assert(index >= 0);
            return index < array.Count ? array[index] : null;
        }

        [Emitted] // parallel assignment:
        public static List<object>/*!*/ GetArraySuffix(List<object>/*!*/ array, int startIndex) {
            int size = array.Count - startIndex;
            if (size > 0) {
                RubyArray result = new RubyArray(size);
                for (int i = startIndex; i < array.Count; i++) {
                    result.Add(array[i]);
                }
                return result;
            } else {
                return new RubyArray();
            }
        }

        #endregion

        #region Global Variables

        [Emitted]
        public static object GetGlobalVariable(RubyScope/*!*/ scope, string/*!*/ name) {
            object value;
            // no error reported if the variable doesn't exist:
            scope.RubyContext.TryGetGlobalVariable(scope, name, out value);
            return value;
        }

        [Emitted]
        public static bool IsDefinedGlobalVariable(RubyScope/*!*/ scope, string/*!*/ name) {
            GlobalVariable variable;
            return scope.RubyContext.TryGetGlobalVariable(name, out variable) && variable.IsDefined;
        }

        [Emitted]
        public static object SetGlobalVariable(object value, RubyScope/*!*/ scope, string/*!*/ name) {
            scope.RubyContext.SetGlobalVariable(scope, name, value);
            return value;
        }

        [Emitted]
        public static void AliasGlobalVariable(RubyScope/*!*/ scope, string/*!*/ newName, string/*!*/ oldName) {
            scope.RubyContext.AliasGlobalVariable(newName, oldName);
        }

        #endregion

        #region Regex

        [Emitted] //RegexMatchReference:
        public static MutableString GetCurrentMatchGroup(RubyScope/*!*/ scope, int index) {
            Debug.Assert(index >= 0);
            return scope.GetInnerMostClosureScope().GetCurrentMatchGroup(index);
        }

        [Emitted] //RegexMatchReference:
        public static MatchData GetCurrentMatchData(RubyScope/*!*/ scope) {
            return scope.GetInnerMostClosureScope().CurrentMatch;
        }

        [Emitted] //RegexMatchReference:
        public static MutableString GetCurrentMatchLastGroup(RubyScope/*!*/ scope) {
            return scope.GetInnerMostClosureScope().GetCurrentMatchLastGroup();
        }

        [Emitted] //RegexMatchReference:
        public static MutableString GetCurrentMatchPrefix(RubyScope/*!*/ scope) {
            MatchData match = scope.GetInnerMostClosureScope().CurrentMatch;
            if (match == null) {
                return null;
            }
            return match.OriginalString.GetSlice(0, match.Index).TaintBy(match.OriginalString);
        }

        [Emitted] //RegexMatchReference:
        public static MutableString GetCurrentMatchSuffix(RubyScope/*!*/ scope) {
            MatchData match = scope.GetInnerMostClosureScope().CurrentMatch;
            if (match == null) {
                return null;
            }
            return match.OriginalString.GetSlice(match.Index + match.Length);
        }

        [Emitted] //RegularExpression:
        public static bool MatchLastInputLine(RubyRegex/*!*/ regex, RubyScope/*!*/ scope) {
            var str = scope.GetInnerMostClosureScope().LastInputLine as MutableString;
            return (str != null) ? RubyRegex.SetCurrentMatchData(scope, regex, str) != null : false;
        }

        [Emitted] //MatchExpression:
        public static object MatchString(MutableString str, RubyRegex/*!*/ regex, RubyScope/*!*/ scope) {
            var match = RubyRegex.SetCurrentMatchData(scope, regex, str);
            return (match != null) ? ScriptingRuntimeHelpers.Int32ToObject(match.Index) : null;
        }

        #endregion

        public const int MakeStringParamCount = 2;
        public const char SuffixEncoded = 'E';
        public const char SuffixBinary = 'B';
        public const char SuffixMutable = 'M';
        public const char SuffixUTF8 = 'U';

        // TODO: encodings
        #region CreateRegex

        [Emitted]
        public static RubyRegex/*!*/ CreateRegexB(string/*!*/ str1, RubyRegexOptions options) {
            return new RubyRegex(str1, options);
        }

        [Emitted]
        public static RubyRegex/*!*/ CreateRegexU(string/*!*/ str1, RubyRegexOptions options) {
            return new RubyRegex(str1, options);
        }

        [Emitted]
        public static RubyRegex/*!*/ CreateRegexE(string/*!*/ str1, int codepage, RubyRegexOptions options) {
            return new RubyRegex(str1, options);
        }
        
        [Emitted]
        public static RubyRegex/*!*/ CreateRegexM(MutableString str1, RubyRegexOptions options) {
            return new RubyRegex(MutableString.CreateInternal(str1), options);
        }

        [Emitted]
        public static RubyRegex/*!*/ CreateRegexBM(string/*!*/ str1, MutableString str2, RubyRegexOptions options) {
            return new RubyRegex(MutableString.Create(str1).Append(str2), options);
        }

        [Emitted]
        public static RubyRegex/*!*/ CreateRegexUM(string/*!*/ str1, MutableString str2, RubyRegexOptions options) {
            return new RubyRegex(MutableString.Create(str1).Append(str2), options);
        }

        [Emitted]
        public static RubyRegex/*!*/ CreateRegexEM(string/*!*/ str1, MutableString str2, int codepage, RubyRegexOptions options) {
            return new RubyRegex(MutableString.Create(str1).Append(str2), options);
        }

        [Emitted]
        public static RubyRegex/*!*/ CreateRegexMB(MutableString str1, string/*!*/ str2, RubyRegexOptions options) {
            return new RubyRegex(MutableString.CreateInternal(str1).Append(str2), options);
        }

        [Emitted]
        public static RubyRegex/*!*/ CreateRegexMU(MutableString str1, string/*!*/ str2, RubyRegexOptions options) {
            return new RubyRegex(MutableString.CreateInternal(str1).Append(str2), options);
        }

        [Emitted]
        public static RubyRegex/*!*/ CreateRegexME(MutableString str1, string/*!*/ str2, int codepage, RubyRegexOptions options) {
            return new RubyRegex(MutableString.CreateInternal(str1).Append(str2), options);
        }

        [Emitted]
        public static RubyRegex/*!*/ CreateRegexMM(MutableString str1, MutableString str2, RubyRegexOptions options) {
            return new RubyRegex(MutableString.CreateInternal(str1).Append(str2), options);
        }

        [Emitted]
        public static RubyRegex/*!*/ CreateRegexN(object[]/*!*/ strings, int codepage, RubyRegexOptions options) {
            return new RubyRegex(ConcatenateStrings(strings), options);
        }

        #endregion

        // TODO: encodings
        #region CreateMutableString

        [Emitted]
        public static MutableString/*!*/ CreateMutableStringB(string/*!*/ str) {
            return MutableString.Create(str, BinaryEncoding.Instance); 
        }

        [Emitted]
        public static MutableString/*!*/ CreateMutableStringU(string/*!*/ str) {
            return MutableString.Create(str, Encoding.UTF8);
        }

        [Emitted]
        public static MutableString/*!*/ CreateMutableStringE(string/*!*/ str1, int codepage) {
            return MutableString.Create(str1, RubyEncoding.GetEncoding(codepage));
        }

        [Emitted]
        public static MutableString/*!*/ CreateMutableStringM(MutableString str1) {
            return MutableString.CreateInternal(str1);
        }

        [Emitted]
        public static MutableString/*!*/ CreateMutableStringBM(string/*!*/ str1, MutableString str2) {
            // TODO: encoding 
            return MutableString.CreateMutable(str1, BinaryEncoding.Instance).Append(str2);
        }

        [Emitted]
        public static MutableString/*!*/ CreateMutableStringUM(string/*!*/ str1, MutableString str2) {
            // TODO: encoding 
            return MutableString.CreateMutable(str1, BinaryEncoding.UTF8).Append(str2);
        }

        [Emitted]
        public static MutableString/*!*/ CreateMutableStringEM(string/*!*/ str1, MutableString str2, int codepage) {
            return MutableString.CreateMutable(str1, RubyEncoding.GetEncoding(codepage)).Append(str2);
        }

        [Emitted]
        public static MutableString/*!*/ CreateMutableStringMB(MutableString str1, string/*!*/ str2) {
            // TODO: encoding 
            return MutableString.CreateInternal(str1).Append(str2);
        }

        [Emitted]
        public static MutableString/*!*/ CreateMutableStringMU(MutableString str1, string/*!*/ str2) {
            // TODO: encoding 
            return MutableString.CreateInternal(str1).Append(str2);
        }

        [Emitted]
        public static MutableString/*!*/ CreateMutableStringME(MutableString str1, string/*!*/ str2, int codepage) {
            // TODO: encoding 
            return MutableString.CreateInternal(str1).Append(str2);
        }

        [Emitted]
        public static MutableString/*!*/ CreateMutableStringMM(MutableString str1, MutableString str2) {
            // TODO: encoding 
            return MutableString.CreateInternal(str1).Append(str2);
        }

        [Emitted]
        public static MutableString/*!*/ CreateMutableStringN(object[]/*!*/ strings, int codepage) {
            // TODO: encodings of literals in the array needs to be handled
            return ConcatenateStrings(strings);
        }

        #endregion

        // TODO: encodings
        #region CreateSymbol

        [Emitted]
        public static SymbolId/*!*/ CreateSymbolB(string/*!*/ str1) {
            Debug.Assert(str1.Length > 0);
            return SymbolTable.StringToId(str1);
        }
        
        [Emitted]
        public static SymbolId/*!*/ CreateSymbolU(string/*!*/ str1) {
            Debug.Assert(str1.Length > 0);
            return SymbolTable.StringToId(str1);
        }

        [Emitted]
        public static SymbolId/*!*/ CreateSymbolE(string/*!*/ str1, int codepage) {
            Debug.Assert(str1.Length > 0);
            return SymbolTable.StringToId(str1);
        }
        
        [Emitted]
        public static SymbolId/*!*/ CreateSymbolM(MutableString str1) {
            if (MutableString.IsNullOrEmpty(str1)) {
                throw RubyExceptions.CreateArgumentError("interning empty string");
            }
            return SymbolTable.StringToId(str1.ConvertToString());
        }

        [Emitted]
        public static SymbolId/*!*/ CreateSymbolBM(string/*!*/ str1, MutableString str2) {
            Debug.Assert(str1.Length > 0);
            return (str2 != null) ? SymbolTable.StringToId(str1 + str2.ConvertToString()) : SymbolTable.StringToId(str1);
        }

        [Emitted]
        public static SymbolId/*!*/ CreateSymbolUM(string/*!*/ str1, MutableString str2) {
            Debug.Assert(str1.Length > 0);
            return (str2 != null) ? SymbolTable.StringToId(str1 + str2.ConvertToString()) : SymbolTable.StringToId(str1);
        }

        [Emitted]
        public static SymbolId/*!*/ CreateSymbolEM(string/*!*/ str1, MutableString str2, int codepage) {
            Debug.Assert(str1.Length > 0);
            return (str2 != null) ? SymbolTable.StringToId(str1 + str2.ConvertToString()) : SymbolTable.StringToId(str1);
        }

        [Emitted]
        public static SymbolId/*!*/ CreateSymbolMB(MutableString str1, string/*!*/ str2) {
            Debug.Assert(str2.Length > 0);
            return (str1 != null) ? SymbolTable.StringToId(str1.ConvertToString() + str2) : SymbolTable.StringToId(str2);
        }

        [Emitted]
        public static SymbolId/*!*/ CreateSymbolMU(MutableString str1, string/*!*/ str2) {
            Debug.Assert(str2.Length > 0);
            return (str1 != null) ? SymbolTable.StringToId(str1.ConvertToString() + str2) : SymbolTable.StringToId(str2);
        }

        [Emitted]
        public static SymbolId/*!*/ CreateSymbolME(MutableString str1, string/*!*/ str2, int codepage) {
            Debug.Assert(str2.Length > 0);
            return (str1 != null) ? SymbolTable.StringToId(str1.ConvertToString() + str2) : SymbolTable.StringToId(str2);
        }
        
        [Emitted]
        public static SymbolId/*!*/ CreateSymbolMM(MutableString str1, MutableString str2) {
            MutableString str;
            if (MutableString.IsNullOrEmpty(str1)) {
                if (MutableString.IsNullOrEmpty(str2)) {
                    throw RubyExceptions.CreateArgumentError("interning empty string");
                } else {
                    str = str2;
                }
            } else if (MutableString.IsNullOrEmpty(str2)) {
                str = str1;
            } else {
                str = MutableString.Create(str1).Append(str2);
            }

            return SymbolTable.StringToId(str.ToString());
        }

        [Emitted]
        public static SymbolId/*!*/ CreateSymbolN(object[]/*!*/ strings, int codepage) {
            var str = ConcatenateStrings(strings);
            if (str.IsEmpty) {
                throw RubyExceptions.CreateArgumentError("interning empty string");
            }
            return SymbolTable.StringToId(str.ToString());
        }

        #endregion

        // TODO: encodings
        // TODO: could be optimized if we knew that we don't have any string literal:
        private static MutableString/*!*/ ConcatenateStrings(object[]/*!*/ strings) {
            Assert.NotNull(strings);
            Type strType = typeof(string);
            MutableString result = MutableString.CreateMutable();
            for (int i = 0; i < strings.Length; i++) {
                object str = strings[i];
                if (str != null) {
                    if (str.GetType() == strType) {
                        result.Append((string)str);
                    } else {
                        result.Append((MutableString)str);
                    }
                }
            }
            return result;
        }

        [Emitted]
        public static RubyEncoding/*!*/ CreateEncoding(int codepage) {
            Debug.Assert(codepage >= 0, "Null encoding not allowed here");
            return RubyEncoding.GetRubyEncoding(codepage);
        }

        [Emitted]
        public static bool IsTrue(object obj) {
            return (obj is bool) ? (bool)obj == true : obj != null;
        }

        [Emitted]
        public static bool IsFalse(object obj) {
            return (obj is bool) ? (bool)obj == false : obj == null;
        }

        #region Exceptions

        [Emitted]
        public static void CheckForAsyncRaiseViaThreadAbort(RubyScope scope, System.Threading.ThreadAbortException exception) {
            Exception visibleException = RubyUtils.GetVisibleException(exception);
            if (exception == visibleException || visibleException == null) {
                return;
            } else {
                RubyOps.SetCurrentExceptionAndStackTrace(scope, exception);
                // We are starting a new exception throw here (with the downside that we will lose the full stack trace)
                RubyExceptionData.ActiveExceptionHandled(visibleException);

                throw visibleException;
            }
        }
        //
        // NOTE:
        // Exception Ops go directly to the current exception object. MRI ignores potential aliases.
        //
        
        [Emitted] //Body, RescueClause:
        public static Exception GetCurrentException(RubyScope/*!*/ scope) {
            return scope.RubyContext.CurrentException;
        }

        [Emitted] //Body:
        public static void SetCurrentExceptionAndStackTrace(RubyScope/*!*/ scope, Exception/*!*/ exception) {
            if (RubyExceptionData.TryGetInstance(exception) == null) {
                RubyExceptionData.AssociateInstance(exception).SetCompiledTrace();
            }
            scope.RubyContext.CurrentException = exception;
        }

        [Emitted] //Body:
        public static void SetCurrentException(RubyScope/*!*/ scope, Exception exception) {
            scope.RubyContext.CurrentException = exception;
        }

        private static readonly CallSite<Func<CallSite, RubyContext, object, object, bool>>/*!*/ _compareExceptionSite = CallSite<Func<CallSite, RubyContext, object, object, bool>>.Create(
            RubySites.InstanceCallAction("===", 1));

        [Emitted] //RescueClause:
        public static bool CompareException(RubyScope/*!*/ scope, object classObject) {            
            // throw the same exception when classObject is nil
            if (!(classObject is RubyModule)) {
                throw RubyExceptions.CreateTypeError("class or module required for rescue clause");
            }
            
            bool result = _compareExceptionSite.Target(_compareExceptionSite, scope.RubyContext, classObject, scope.RubyContext.CurrentException);
            if (result) {
                RubyExceptionData.ActiveExceptionHandled(scope.RubyContext.CurrentException);
            }
            return result;
        }

        [Emitted] //RescueClause:
        public static bool CompareSplattedExceptions(RubyScope/*!*/ scope, object classObjects) {
            var list = classObjects as IList;
            if (list != null) {
                for (int i = 0; i < list.Count; i++) {
                    if (CompareException(scope, list[i])) {
                        return true;
                    }
                }
                return false;
            } else {
                return CompareException(scope, classObjects);
            }
        }

        [Emitted] //RescueClause:
        public static bool CompareDefaultException(RubyScope/*!*/ scope, object/*!*/ self) {
            RubyContext ec = scope.RubyContext;

            // MRI doesn't call === here;
            return ec.IsInstanceOf(ec.CurrentException, ec.StandardErrorClass);
        }

        [Emitted]
        public static string/*!*/ GetDefaultExceptionMessage(RubyClass/*!*/ exceptionClass) {
            return exceptionClass.Name;
        }

        [Emitted]
        public static ArgumentException/*!*/ MakeWrongNumberOfArgumentsError(int actual, int expected) {
            return new ArgumentException(String.Format("wrong number of arguments ({0} for {1})", actual, expected));
        }

        [Emitted] //SuperCall
        public static Exception/*!*/ MakeTopLevelSuperException() {
            return new MissingMethodException("super called outside of method");
        }

        [Emitted] //SuperCallAction
        public static Exception/*!*/ MakeMissingSuperException(string/*!*/ name) {
            return new MissingMethodException(String.Format("super: no superclass method `{0}'", name));
        }

        [Emitted]
        public static Exception/*!*/ MakeInvalidArgumentTypesError(string/*!*/ methodName) {
            // TODO:
            return new ArgumentException(String.Format("wrong number or type of arguments for `{0}'", methodName));
        }

        [Emitted]
        public static Exception/*!*/ MakeAmbiguousMatchError(string/*!*/ methodName) {
            // TODO:
            return new AmbiguousMatchException(String.Format("Found multiple methods for `{0}'", methodName));
        }

        #endregion

        [Emitted] //RubyBinder
        public static bool IsSuperCallTarget(RubyScope/*!*/ scope, RubyModule/*!*/ module, string/*!*/ methodName, out object self) {
            RubyModule _currentDeclaringModule;
            string _currentMethodName;
            scope.GetSuperCallTarget(out _currentDeclaringModule, out _currentMethodName, out self);
            return module == _currentDeclaringModule && methodName == _currentMethodName;
        }

        [Emitted]
        public static Range/*!*/ CreateInclusiveRange(object begin, object end, RubyScope/*!*/ scope, BinaryOpStorage/*!*/ comparisonStorage) {
            return new Range(comparisonStorage, scope.RubyContext, begin, end, false);
        }

        [Emitted]
        public static Range/*!*/ CreateExclusiveRange(object begin, object end, RubyScope/*!*/ scope, BinaryOpStorage/*!*/ comparisonStorage) {
            return new Range(comparisonStorage, scope.RubyContext, begin, end, true);
        }

        [Emitted]
        public static Range/*!*/ CreateInclusiveIntegerRange(int begin, int end) {
            return new Range(begin, end, false);
        }

        [Emitted]
        public static Range/*!*/ CreateExclusiveIntegerRange(int begin, int end) {
            return new Range(begin, end, true);
        }

        // allocator for struct instances:
        [Emitted]
        public static RubyStruct/*!*/ AllocateStructInstance(RubyClass/*!*/ self) {
            return RubyStruct.Create(self);
        }

        // factory for struct instances:
        [Emitted]
        public static RubyStruct/*!*/ CreateStructInstance(RubyClass/*!*/ self, [NotNull]params object[]/*!*/ items) {
            var result = RubyStruct.Create(self);
            result.SetValues(items);
            return result;
        }

        [Emitted]
        public static DynamicMetaObject/*!*/ GetMetaObject(IRubyObject/*!*/ obj, Expression/*!*/ parameter) {
            return new RubyObject.Meta(parameter, BindingRestrictions.Empty, obj);
        }

        #region Dynamic Actions

        [Emitted] // ProtocolConversionAction
        public static Proc/*!*/ ToProcValidator(string/*!*/ className, object obj) {
            Proc result = obj as Proc;
            if (result == null) {
                throw new InvalidOperationException(String.Format("{0}#to_proc should return Proc", className));
            }
            return result;
        }

        [Emitted] // ProtocolConversionAction
        public static MutableString/*!*/ ToStringValidator(string/*!*/ className, object obj) {
            MutableString result = obj as MutableString;
            if (result == null) {
                throw new InvalidOperationException(String.Format("{0}#to_str should return String", className));
            }
            return result;
        }

        [Emitted] // ProtocolConversionAction
        public static string/*!*/ ToSymbolValidator(string/*!*/ className, object obj) {
            var str = obj as MutableString;
            if (str == null) {
                throw new InvalidOperationException(String.Format("{0}#to_str should return String", className));
            }
            return str.ConvertToString();
        }

        [Emitted] // ProtocolConversionAction
        public static string/*!*/ ConvertSymbolIdToSymbol(SymbolId value) {
            return SymbolTable.IdToString(value);
        }

        [Emitted] // ProtocolConversionAction
        public static string/*!*/ ConvertFixnumToSymbol(RubyContext/*!*/ context, int value) {
            context.ReportWarning("do not use Fixnums as Symbols");

            SymbolId result;
            if (TryConvertFixnumToSymbol(value, out result)) {
                return SymbolTable.IdToString(result);
            } else {
                throw RubyExceptions.CreateArgumentError(String.Format("{0} is not a symbol", value));
            }
        }

        public static bool TryConvertFixnumToSymbol(int number, out SymbolId symbol) {
            symbol = new SymbolId(number);
            return !symbol.IsEmpty && SymbolTable.ContainsId(symbol);
        }

        [Emitted] // ProtocolConversionAction
        public static string/*!*/ ConvertMutableStringToSymbol(MutableString/*!*/ value) {
            return value.ConvertToString();
        }
        
        [Emitted] // ProtocolConversionAction
        public static RubyRegex/*!*/ ToRegexValidator(string/*!*/ className, object obj) {
            return new RubyRegex(RubyRegex.Escape(ToStringValidator(className, obj)), RubyRegexOptions.NONE);
        }

        [Emitted] // ProtocolConversionAction
        public static IList/*!*/ ToArrayValidator(string/*!*/ className, object obj) {
            var result = obj as IList;
            if (result == null) {
                throw new InvalidOperationException(String.Format("{0}#to_ary should return Array", className));
            }
            return result;
        }

        [Emitted] // ProtocolConversionAction
        public static IDictionary<object, object>/*!*/ ToHashValidator(string/*!*/ className, object obj) {
            var result = obj as IDictionary<object, object>;
            if (result == null) {
                throw new InvalidOperationException(String.Format("{0}#to_hash should return Hash", className));
            }
            return result;
        }

        [Emitted] // ProtocolConversionAction
        public static int ToFixnumValidator(string/*!*/ className, object obj) {
            if (obj is int) {
                return (int)obj;
            }

            var bignum = obj as BigInteger;
            if ((object)bignum != null) {
                int fixnum;
                if (bignum.AsInt32(out fixnum)) {
                    return fixnum;
                }
                throw RubyExceptions.CreateRangeError("bignum too big to convert into `long'");
            }

            throw new InvalidOperationException(String.Format("{0}#to_int should return Integer", className));
        }

        [Emitted] // ProtocolConversionAction
        public static IntegerValue ToIntegerValidator(string/*!*/ className, object obj) {
            if (obj is int) {
                return new IntegerValue((int)obj);
            }

            var bignum = obj as BigInteger;
            if ((object)bignum != null) {
                return new IntegerValue(bignum);
            }

            throw new InvalidOperationException(String.Format("{0}#to_int/to_i should return Integer", className));
        }

        [Emitted] // ProtocolConversionAction
        public static Exception/*!*/ CreateTypeConversionError(string/*!*/ fromType, string/*!*/ toType) {
            return RubyExceptions.CreateTypeConversionError(fromType, toType);
        }

        [Emitted] // ConvertToFixnumAction
        public static int ConvertBignumToFixnum(BigInteger/*!*/ bignum) {
            int fixnum;
            if (bignum.AsInt32(out fixnum)) {
                return fixnum;
            }
            throw RubyExceptions.CreateRangeError("bignum too big to convert into `long'");
        }

        [Emitted] // ConvertToSAction
        public static MutableString/*!*/ ToSDefaultConversion(RubyContext/*!*/ context, object obj) {
            return obj as MutableString ?? RubyUtils.ObjectToMutableString(context, obj);
        }

        #endregion

        #region Called by GetHashCode/Equals methods in generated .NET classes

        // we need to get the right execution context here
#if OBSOLETE
        [Emitted]
        public static bool ResolveDeclaredInstanceMethod(Type myType, string name) {
            RubyModule module = RubyUtils.GetExecutionContext(null).GetOrCreateClass(myType);
            return module.ResolveDeclaredMethod(SymbolTable.StringToId(name)) != null;
        }

        [Emitted]
        public static int CallHash(object obj) {
            // TODO: do not use default context:
            return _HashSharedSite.Invoke(RubyContext._DefaultContext, obj);
        }

        [Emitted]
        public static bool CallEql(object lhs, object rhs) {
            // TODO: do not use default context:
            return _EqlSharedSite.Invoke(RubyContext._DefaultContext, lhs, rhs);
        }
#endif

        #endregion

        #region Instance variable support

        [Emitted]
        public static object GetInstanceVariable(RubyScope/*!*/ scope, object self, string/*!*/ name) {
            RubyInstanceData data = scope.RubyContext.TryGetInstanceData(self);
            return (data != null) ? data.GetInstanceVariable(name) : null;
        }

        [Emitted]
        public static bool IsDefinedInstanceVariable(RubyScope/*!*/ scope, object self, string/*!*/ name) {
            RubyInstanceData data = scope.RubyContext.TryGetInstanceData(self);
            if (data == null) return false;
            object value;
            return data.TryGetInstanceVariable(name, out value);
        }

        [Emitted]
        public static object SetInstanceVariable(object self, object value, RubyScope/*!*/ scope, string/*!*/ name) {
            scope.RubyContext.SetInstanceVariable(self, name, value);
            return value;
        }

        #endregion

        #region Class Variables

        [Emitted]
        public static object GetObjectClassVariable(RubyScope/*!*/ scope, string/*!*/ name) {
            return GetClassVariableInternal(scope.RubyContext.ObjectClass, name);
        }

        [Emitted]
        public static object GetClassVariable(RubyScope/*!*/ scope, string/*!*/ name) {
            // owner is the first module in scope:
            RubyModule owner = scope.GetInnerMostModule(true);
            return GetClassVariableInternal(owner, name);
        }

        private static object GetClassVariableInternal(RubyModule/*!*/ module, string/*!*/ name) {
            object value;
            if (module.TryResolveClassVariable(name, out value) == null) {
                throw RubyExceptions.CreateNameError(String.Format("uninitialized class variable {0} in {1}", name, module.Name));
            }
            return value;
        }

        [Emitted]
        public static object TryGetObjectClassVariable(RubyScope/*!*/ scope, string/*!*/ name) {
            object value;
            scope.RubyContext.ObjectClass.TryGetClassVariable(name, out value);
            return value;
        }

        [Emitted]
        public static object TryGetClassVariable(RubyScope/*!*/ scope, string/*!*/ name) {
            object value;
            // owner is the first module in scope:
            scope.GetInnerMostModule(true).TryResolveClassVariable(name, out value);
            return value;
        }

        [Emitted]
        public static bool IsDefinedObjectClassVariable(RubyScope/*!*/ scope, string/*!*/ name) {
            object value;
            return scope.RubyContext.ObjectClass.TryResolveClassVariable(name, out value) != null;
        }

        [Emitted]
        public static bool IsDefinedClassVariable(RubyScope/*!*/ scope, string/*!*/ name) {
            // owner is the first module in scope:
            RubyModule owner = scope.GetInnerMostModule(true);
            object value;
            return owner.TryResolveClassVariable(name, out value) != null;
        }

        [Emitted]
        public static object SetObjectClassVariable(object value, RubyScope/*!*/ scope, string/*!*/ name) {
            return SetClassVariableInternal(scope.RubyContext.ObjectClass, name, value);
        }

        [Emitted]
        public static object SetClassVariable(object value, RubyScope/*!*/ scope, string/*!*/ name) {
            return SetClassVariableInternal(scope.GetInnerMostModule(true), name, value);
        }

        private static object SetClassVariableInternal(RubyModule/*!*/ lexicalOwner, string/*!*/ name, object value) {
            object oldValue;
            RubyModule owner = lexicalOwner.TryResolveClassVariable(name, out oldValue);
            (owner ?? lexicalOwner).SetClassVariable(name, value);
            return value;
        }

        #endregion

        #region Ruby Types

        [Emitted] //RubyTypeBuilder
        public static RubyInstanceData/*!*/ GetInstanceData(ref RubyInstanceData/*!*/ instanceData) {
            if (instanceData == null) {
                Interlocked.CompareExchange(ref instanceData, new RubyInstanceData(), null);
            }
            return instanceData;
        }

#if !SILVERLIGHT
        [Emitted] //RubyTypeBuilder
        public static void DeserializeObject(out RubyInstanceData/*!*/ instanceData, out RubyClass/*!*/ rubyClass, SerializationInfo/*!*/ info) {
            rubyClass = (RubyClass)info.GetValue("#class", typeof(RubyClass));
            RubyInstanceData newInstanceData = null;
            foreach (SerializationEntry entry in info) {
                if (entry.Name.StartsWith("@")) {
                    if (newInstanceData == null) {
                        newInstanceData = new RubyInstanceData();
                    }
                    newInstanceData.SetInstanceVariable(entry.Name, entry.Value);
                }
            }
            instanceData = newInstanceData;
        }

        [Emitted] //RubyTypeBuilder
        public static void SerializeObject(RubyInstanceData instanceData, RubyClass/*!*/ rubyClass, SerializationInfo/*!*/ info) {
            info.AddValue("#class", rubyClass, typeof(RubyClass));
            if (instanceData != null) {
                string[] instanceNames = instanceData.GetInstanceVariableNames();
                foreach (string name in instanceNames) {
                    object value;
                    if (!instanceData.TryGetInstanceVariable(name, out value)) {
                        value = null;
                    }
                    info.AddValue(name, value, typeof(object));
                }
            }
        }
#endif
        #endregion

        #region Delegates, Events

        /// <summary>
        /// Hooks up an event to call a proc at hand.
        /// EventInfo is passed in as object since it is an internal type.
        /// </summary>
        [Emitted]
        public static object HookupEvent(EventInfo/*!*/ eventInfo, object target, Proc/*!*/ proc) {
            Assert.NotNull(eventInfo, proc);

            BlockParam bp = CreateBfcForProcCall(proc);
            Delegate eh = BinderOps.GetDelegate(proc.LocalScope.RubyContext, bp, eventInfo.EventHandlerType);
            MethodInfo mi = eventInfo.GetAddMethod();

            mi.Invoke(target, new object[] { eh });

            return null;
        }

        [Emitted]
        public static Delegate/*!*/ CreateDelegateFromProc(Type/*!*/ type, Proc/*!*/ proc) {
            BlockParam bp = CreateBfcForProcCall(proc);
            return BinderOps.GetDelegate(proc.LocalScope.RubyContext, bp, type);
        }

        [Emitted]
        public static Delegate/*!*/ CreateDelegateFromMethod(Type/*!*/ type, RubyMethod/*!*/ method) {
            return BinderOps.GetDelegate(method.Info.Context, method, type);
        }

        #endregion

        [Emitted]
        public static void X(string marker) {
        }
    }
}

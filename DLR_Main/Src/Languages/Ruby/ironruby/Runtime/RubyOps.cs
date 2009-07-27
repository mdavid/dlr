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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
#if CODEPLEX_40
using System.Dynamic;
#else
#endif
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
#if !CODEPLEX_40
using Microsoft.Runtime.CompilerServices;
#endif

using System.Runtime.Serialization;
using System.Threading;
using IronRuby.Builtins;
using IronRuby.Compiler;
using IronRuby.Compiler.Generation;
using IronRuby.Runtime.Calls;
using Microsoft.Scripting;
using Microsoft.Scripting.Interpreter;
using Microsoft.Scripting.Math;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using IronRuby.Compiler.Ast;
#if CODEPLEX_40
using MSA = System.Linq.Expressions;
#else
using MSA = Microsoft.Linq.Expressions;
#endif

namespace IronRuby.Runtime {
    [ReflectionCached, CLSCompliant(false)]
    public static partial class RubyOps {

        [Emitted]
        public static readonly object DefaultArgument = new object();
        
        // Returned by a virtual site if a base call should be performed.
        [Emitted]
        public static readonly object ForwardToBase = new object();

        #region Scopes

        [Emitted]
        public static MutableTuple GetLocals(RubyScope/*!*/ scope) {
            return scope.Locals;
        }

        [Emitted]
        public static MutableTuple GetParentLocals(RubyScope/*!*/ scope) {
            return scope.Parent.Locals;
        }

        [Emitted]
        public static RubyScope/*!*/ GetParentScope(RubyScope/*!*/ scope) {
            return scope.Parent;
        }

        [Emitted]
        public static Proc GetMethodBlockParameter(RubyScope/*!*/ scope) {
            var methodScope = scope.GetInnerMostMethodScope();
            return methodScope != null ? methodScope.BlockParameter : null;
        }

        [Emitted]
        public static object GetMethodBlockParameterSelf(RubyScope/*!*/ scope) {
            Proc proc = scope.GetInnerMostMethodScope().BlockParameter;
            Debug.Assert(proc != null, "CreateBfcForYield is called before this method and it checks non-nullity");
            return proc.Self;
        }

        [Emitted]
        public static object GetProcSelf(Proc/*!*/ proc) {
            return proc.Self;
        }

        [Emitted]
        public static void InitializeScope(RubyScope/*!*/ scope, MutableTuple locals, SymbolId[]/*!*/ variableNames, 
            InterpretedFrame interpretedFrame) {

            if (!scope.LocalsInitialized) {
                scope.SetLocals(locals, variableNames);
            }
            scope.InterpretedFrame = interpretedFrame;
        }
        
        [Emitted]
        public static void InitializeScopeNoLocals(RubyScope/*!*/ scope, InterpretedFrame interpretedFrame) {
            scope.InterpretedFrame = interpretedFrame;
        }

        [Emitted]
        public static void SetDataConstant(RubyScope/*!*/ scope, string/*!*/ dataPath, int dataOffset) {
            Debug.Assert(dataOffset >= 0);
            RubyFile dataFile;
            RubyContext context = scope.RubyContext;
            if (context.DomainManager.Platform.FileExists(dataPath)) {
                dataFile = new RubyFile(context, dataPath, RubyFileMode.RDONLY);
                dataFile.Seek(dataOffset, SeekOrigin.Begin);
            } else {
                dataFile = null;
            }

            context.ObjectClass.SetConstant("DATA", dataFile);
        }

        [Emitted]
        public static RubyModuleScope/*!*/ CreateModuleScope(MutableTuple locals, SymbolId[]/*!*/ variableNames, 
            RubyScope/*!*/ parent, RubyModule/*!*/ module) {

            RubyModuleScope scope = new RubyModuleScope(parent, module, false, module);
            scope.SetDebugName((module.IsClass ? "class" : "module") + " " + module.Name);
            scope.SetLocals(locals, variableNames);
            return scope;
        }

        [Emitted]
        public static RubyMethodScope/*!*/ CreateMethodScope(MutableTuple locals, SymbolId[]/*!*/ variableNames, 
            RubyScope/*!*/ parentScope, RubyModule/*!*/ declaringModule, string/*!*/ definitionName,
            object selfObject, Proc blockParameter, InterpretedFrame interpretedFrame) {

            return new RubyMethodScope(
                locals, variableNames,
                parentScope, declaringModule, definitionName, selfObject, blockParameter,
                interpretedFrame
            );            
        }

        [Emitted]
        public static RubyBlockScope/*!*/ CreateBlockScope(MutableTuple locals, SymbolId[]/*!*/ variableNames, 
            BlockParam/*!*/ blockParam, object selfObject, InterpretedFrame interpretedFrame) {

            return new RubyBlockScope(locals, variableNames, blockParam, selfObject, interpretedFrame);
        }

        [Emitted]
        public static void TraceMethodCall(RubyMethodScope/*!*/ scope, string fileName, int lineNumber) {
            // MRI: 
            // Reports DeclaringModule even though an aliased method in a sub-module is called.
            // Also works for singleton module-function, which shares DeclaringModule with instance module-function.
            RubyModule module = scope.DeclaringModule;
            scope.RubyContext.ReportTraceEvent("call", scope, module, scope.DefinitionName, fileName, lineNumber);
        }

        [Emitted]
        public static void TraceMethodReturn(RubyMethodScope/*!*/ scope, string fileName, int lineNumber) {
            RubyModule module = scope.DeclaringModule;
            scope.RubyContext.ReportTraceEvent("return", scope, module, scope.DefinitionName, fileName, lineNumber);
        }

        [Emitted]
        public static void TraceBlockCall(RubyBlockScope/*!*/ scope, BlockParam/*!*/ block, string fileName, int lineNumber) {
            if (block.IsMethod) {
                scope.RubyContext.ReportTraceEvent("call", scope, block.MethodLookupModule, block.MethodName, fileName, lineNumber);
            }
        }

        [Emitted]
        public static void TraceBlockReturn(RubyBlockScope/*!*/ scope, BlockParam/*!*/ block, string fileName, int lineNumber) {
            if (block.IsMethod) {
                scope.RubyContext.ReportTraceEvent("return", scope, block.MethodLookupModule, block.MethodName, fileName, lineNumber);
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
            return scope.ResolveLocalVariable(SymbolTable.StringToId(name));
        }

        [Emitted]
        public static object SetLocalVariable(object value, RubyScope/*!*/ scope, string/*!*/ name) {
            return scope.ResolveAndSetLocalVariable(SymbolTable.StringToId(name), value);
        }

        [Emitted]
        public static VersionHandle/*!*/ GetSelfClassVersionHandle(RubyScope/*!*/ scope) {
            return scope.SelfImmediateClass.Version;
        }

        #endregion

        #region Context

        [Emitted]
        public static RubyContext/*!*/ GetContextFromModule(RubyModule/*!*/ module) {
            return module.Context;
        }
        
        [Emitted]
        public static RubyContext/*!*/ GetContextFromIRubyObject(IRubyObject/*!*/ obj) {
            return obj.ImmediateClass.Context;
        }
        
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
        public static Proc InstantiateBlock(RubyScope/*!*/ scope, object self, BlockDispatcher/*!*/ dispatcher) {
            return (dispatcher.Method != null) ? new Proc(ProcKind.Block, self, scope, dispatcher) : null;
        }

        [Emitted]
        public static Proc/*!*/ DefineBlock(RubyScope/*!*/ scope, object self, BlockDispatcher/*!*/ dispatcher, object/*!*/ clrMethod) {
            // DLR closures should not be used:
            Debug.Assert(!(((Delegate)clrMethod).Target is Closure) || ((Closure)((Delegate)clrMethod).Target).Locals == null);
            return new Proc(ProcKind.Block, self, scope, dispatcher.SetMethod(clrMethod));
        }

        /// <summary>
        /// Used in a method call with a block to reset proc-kind when the call is retried
        /// </summary>
        [Emitted]
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
        public static object DefineMethod(object target, RubyScope/*!*/ scope, RubyMethodBody/*!*/ body) {
            Assert.NotNull(body, scope);

            RubyModule instanceOwner, singletonOwner;
            RubyMemberFlags instanceFlags, singletonFlags;
            bool moduleFunction = false;

            if (body.HasTarget) {
                if (!RubyUtils.CanCreateSingleton(target)) {
                    throw RubyExceptions.CreateTypeError("can't define singleton method for literals");
                }

                instanceOwner = null;
                instanceFlags = RubyMemberFlags.Invalid;
                singletonOwner = scope.RubyContext.CreateSingletonClass(target);
                singletonFlags = RubyMemberFlags.Public;
            } else {
                var attributesScope = scope.GetMethodAttributesDefinitionScope();
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

                    instanceFlags = RubyMemberFlags.Private;
                    singletonOwner = instanceOwner.SingletonClass;
                    singletonFlags = RubyMemberFlags.Public;
                    moduleFunction = true;
                } else {
                    instanceOwner = scope.GetMethodDefinitionOwner();
                    instanceFlags = (RubyMemberFlags)RubyUtils.GetSpecialMethodVisibility(attributesScope.Visibility, body.Name);
                    singletonOwner = null;
                    singletonFlags = RubyMemberFlags.Invalid;
                }
            }
            
            RubyMethodInfo instanceMethod = null, singletonMethod = null;

            if (instanceOwner != null) {
                SetMethod(scope.RubyContext, instanceMethod =
                    new RubyMethodInfo(body, scope, instanceOwner, instanceFlags)
                );
            }

            if (singletonOwner != null) {
                SetMethod(scope.RubyContext, singletonMethod =
                    new RubyMethodInfo(body, scope, singletonOwner, singletonFlags)
                );
            }

            // the method's scope saves the result => singleton module-function uses instance-method
            var method = instanceMethod ?? singletonMethod;

            method.DeclaringModule.MethodAdded(body.Name);

            if (moduleFunction) {
                Debug.Assert(!method.DeclaringModule.IsClass);
                method.DeclaringModule.SingletonClass.MethodAdded(body.Name);
            }

            return null;
        }

        private static void SetMethod(RubyContext/*!*/ callerContext, RubyMethodInfo/*!*/ method) {
            var owner = method.DeclaringModule;

            // Do not trigger the add-method event just yet, we need to assign the result into closure before executing any user code.
            // If the method being defined is "method_added" itself, we would call that method before the info gets assigned to the closure.
            owner.SetMethodNoEvent(callerContext, method.DefinitionName, method);

            // expose RubyMethod in the scope (the method is bound to the main singleton instance):
            if (owner.GlobalScope != null) {
                owner.GlobalScope.Scope.SetVariable(
                    SymbolTable.StringToId(method.DefinitionName),
                    new RubyMethod(owner.GlobalScope.MainObject, method, method.DefinitionName)
                );
            }
        }

        [Emitted] // AliasStatement:
        public static void AliasMethod(RubyScope/*!*/ scope, string/*!*/ newName, string/*!*/ oldName) {
            scope.GetMethodDefinitionOwner().AddMethodAlias(newName, oldName);
        }

        [Emitted] // UndefineMethod:
        public static void UndefineMethod(RubyScope/*!*/ scope, string/*!*/ name) {
            RubyModule owner = scope.GetInnerMostModuleForMethodLookup();

            if (!owner.ResolveMethod(name, VisibilityContext.AllVisible).Found) {
                throw RubyExceptions.CreateUndefinedMethodError(owner, name);
            }
            owner.UndefineMethod(name);
        }

        [Emitted] // MethodCall:
        public static bool IsDefinedMethod(object self, RubyScope/*!*/ scope, string/*!*/ name) {
            // MRI: this is different from UndefineMethod, it behaves like Kernel#method (i.e. doesn't use lexical scope):
            // TODO: visibility
            return scope.RubyContext.ResolveMethod(self, name, VisibilityContext.AllVisible).Found;
        }

        #endregion

        #region Modules

        [Emitted]
        public static RubyModule/*!*/ DefineGlobalModule(RubyScope/*!*/ scope, string/*!*/ name) {
            return RubyUtils.DefineModule(scope.GlobalScope, scope.Top.TopModuleOrObject, name);
        }

        [Emitted]
        public static RubyModule/*!*/ DefineNestedModule(RubyScope/*!*/ scope, string/*!*/ name) {
            return RubyUtils.DefineModule(scope.GlobalScope, scope.GetInnerMostModuleForConstantLookup(), name);
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
            return RubyUtils.DefineClass(scope.GlobalScope, scope.GetInnerMostModuleForConstantLookup(), name, superClassObject);
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
            RubyUtils.SetConstant(scope.GetInnerMostModuleForConstantLookup(), name, value);
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
        public static IList/*!*/ SplatAppend(IList/*!*/ array, object splattee) {
            IList list;

            if ((list = splattee as IList) != null) {
                Utils.AddRange(array, list);
            } else {
                array.Add(splattee);
            }
            return array;
        }

        [Emitted]
        public static object Splat(object/*!*/ value) {
            var list = value as IList;
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
            var list = array as IList;
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
        public static IList/*!*/ Unsplat(object/*!*/ splattee) {
            var list = splattee as IList;
            if (list == null) {
                list = new RubyArray(1);
                list.Add(splattee);
            }
            return list;
        }

        // CaseExpression
        [Emitted]
        public static bool ExistsUnsplat(CallSite<Func<CallSite, object, object, object>>/*!*/ comparisonSite, object splattee, object value) {
            var list = splattee as IList;
            if (list != null) {
                foreach (var item in list) {
                    if (IsTrue(comparisonSite.Target(comparisonSite, item, value))) {
                        return true;
                    }
                }
                return false;
            } else {
                return IsTrue(comparisonSite.Target(comparisonSite, splattee, value)); 
            }
        }

        [Emitted] // parallel assignment:
        public static object GetArrayItem(IList/*!*/ array, int index) {
            Debug.Assert(index >= 0);
            return index < array.Count ? array[index] : null;
        }

        [Emitted] // parallel assignment:
        public static RubyArray/*!*/ GetArraySuffix(IList/*!*/ array, int startIndex) {
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

        #region CLR Vectors (factories mimic Ruby Array factories)

        [Emitted, RubyConstructor]
        public static object/*!*/ CreateVector(ConversionStorage<Union<IList, int>>/*!*/ toAryToInt, BlockParam block, RubyClass/*!*/ self,
            [NotNull]object/*!*/ arrayOrSize) {

            var elementType = self.GetUnderlyingSystemType().GetElementType();
            Debug.Assert(elementType != null);

            var site = toAryToInt.GetSite(CompositeConversionAction.Make(self.Context, CompositeConversion.ToAryToInt));
            var union = site.Target(site, arrayOrSize);

            if (union.First != null) {
                // block ignored
                return CreateVectorInternal(self.Context, elementType, union.First);
            } else if (block != null) {
                return PopulateVector(self.Context, CreateVectorInternal(elementType, union.Second), block);
            } else {
                return CreateVectorInternal(elementType, union.Second);
            }
        }

        [Emitted, RubyConstructor]
        public static Array/*!*/ CreateVectorWithValues(RubyClass/*!*/ self, [DefaultProtocol]int size, object value) {
            var elementType = self.GetUnderlyingSystemType().GetElementType();
            Debug.Assert(elementType != null);

            var result = CreateVectorInternal(elementType, size);
            for (int i = 0; i < size; i++) {
                SetVectorItem(self.Context, result, i, value);
            }
            return result;
        }

        public static Array/*!*/ CreateVectorInternal(Type/*!*/ elementType, int size) {
            if (size < 0) {
                throw RubyExceptions.CreateArgumentError("negative array size");
            }

            return Array.CreateInstance(elementType, size);
        }

        private static Array/*!*/ CreateVectorInternal(RubyContext/*!*/ context, Type/*!*/ elementType, IList/*!*/ list) {
            var result = Array.CreateInstance(elementType, list.Count);
            for (int i = 0; i < result.Length; i++) {
                SetVectorItem(context, result, i, list[i]);
            }

            return result;
        }

        private static object PopulateVector(RubyContext/*!*/ context, Array/*!*/ array, BlockParam/*!*/ block) {
            for (int i = 0; i < array.Length; i++) {
                object item;
                if (block.Yield(i, out item)) {
                    return item;
                }
                SetVectorItem(context, array, i, item);
            }
            return array;
        }

        private static void SetVectorItem(RubyContext/*!*/ context, Array/*!*/ array, int index, object value) {
            // TODO: convert to the element type:
            array.SetValue(value, index);
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

        public const char SuffixLiteral = 'L';       // Repr: literal string
        public const char SuffixMutable = 'M';       // non-literal "...#{expr}..."

        /// <summary>
        /// Specialized signatures exist for upto the following number of string parts
        /// </summary>
        public const int MakeStringParamCount = 2;

        #region CreateRegex

        private static RubyRegex/*!*/ CreateRegexWorker(
            RubyRegexOptions options, 
            StrongBox<RubyRegex> regexpCache, 
            bool isLiteralWithoutSubstitutions,
            Func<RubyRegex> createRegex) {

            try {
                bool once = ((options & RubyRegexOptions.Once) == RubyRegexOptions.Once) || isLiteralWithoutSubstitutions;
                if (once) {
                    // Note that the user is responsible for thread synchronization
                    if (regexpCache.Value == null) {
                        regexpCache.Value = createRegex();
                    }
                    return regexpCache.Value;
                } else {
                    // In the future, we can consider caching the last Regexp. For some regexp literals 
                    // with substitution, the substition will be the same most of the time
                    return createRegex();
                }
            } catch (RegexpError e) {
                if (isLiteralWithoutSubstitutions) {
                    // Ideally, this should be thrown during parsing of the source, even if the 
                    // expression happens to be unreachable at runtime.
                    throw new SyntaxError(e.Message);
                } else {
                    throw;
                }
            }
        }

        [Emitted]
        public static RubyRegex/*!*/ CreateRegexL(string/*!*/ str1, RubyEncoding/*!*/ encoding, RubyRegexOptions options, StrongBox<RubyRegex> regexpCache) {
            Func<RubyRegex> createRegex = delegate { return new RubyRegex(CreateMutableStringL(str1, encoding), options); };
            return CreateRegexWorker(options, regexpCache, true, createRegex);
        }
        
        [Emitted]
        public static RubyRegex/*!*/ CreateRegexM(MutableString str1, RubyEncoding/*!*/ encoding, RubyRegexOptions options, StrongBox<RubyRegex> regexpCache) {
            Func<RubyRegex> createRegex = delegate { return new RubyRegex(CreateMutableStringM(str1, encoding), options); };
            return CreateRegexWorker(options, regexpCache, false, createRegex);
        }

        [Emitted]
        public static RubyRegex/*!*/ CreateRegexLM(string/*!*/ str1, MutableString str2, RubyEncoding/*!*/ encoding, RubyRegexOptions options, StrongBox<RubyRegex> regexpCache) {
            Func<RubyRegex> createRegex = delegate { return new RubyRegex(CreateMutableStringLM(str1, str2, encoding), options); };
            return CreateRegexWorker(options, regexpCache, false, createRegex);
        }

        [Emitted]
        public static RubyRegex/*!*/ CreateRegexML(MutableString str1, string/*!*/ str2, RubyEncoding/*!*/ encoding, RubyRegexOptions options, StrongBox<RubyRegex> regexpCache) {
            Func<RubyRegex> createRegex = delegate { return new RubyRegex(CreateMutableStringML(str1, str2, encoding), options); };
            return CreateRegexWorker(options, regexpCache, false, createRegex);
        }

        [Emitted]
        public static RubyRegex/*!*/ CreateRegexMM(MutableString str1, MutableString str2, RubyEncoding/*!*/ encoding, RubyRegexOptions options, StrongBox<RubyRegex> regexpCache) {
            Func<RubyRegex> createRegex = delegate { return new RubyRegex(CreateMutableStringMM(str1, str2, encoding), options); };
            return CreateRegexWorker(options, regexpCache, false, createRegex);
        }

        [Emitted]
        public static RubyRegex/*!*/ CreateRegexN(object[]/*!*/ strings, RubyEncoding/*!*/ encoding, RubyRegexOptions options, StrongBox<RubyRegex> regexpCache) {
            Func<RubyRegex> createRegex = delegate { return new RubyRegex(CreateMutableStringN(strings, encoding), options); };
            return CreateRegexWorker(options, regexpCache, false, createRegex);
        }

        #endregion

        #region CreateMutableString

        [Emitted]
        public static MutableString/*!*/ CreateMutableStringL(string/*!*/ str1, RubyEncoding/*!*/ encoding) {
            return MutableString.Create(str1, encoding);
        }

        [Emitted]
        public static MutableString/*!*/ CreateMutableStringM(MutableString str1, RubyEncoding/*!*/ encoding) {
            return MutableString.CreateInternal(str1, encoding);
        }

        [Emitted]
        public static MutableString/*!*/ CreateMutableStringLM(string/*!*/ str1, MutableString str2, RubyEncoding/*!*/ encoding) {
            return MutableString.CreateMutable(str1, encoding).Append(str2);
        }

        [Emitted]
        public static MutableString/*!*/ CreateMutableStringML(MutableString str1, string/*!*/ str2, RubyEncoding/*!*/ encoding) {
            return MutableString.CreateInternal(str1, encoding).Append(str2);
        }

        [Emitted]
        public static MutableString/*!*/ CreateMutableStringMM(MutableString str1, MutableString str2, RubyEncoding/*!*/ encoding) {
            return MutableString.CreateInternal(str1, encoding).Append(str2);
        }

        [Emitted]
        public static MutableString/*!*/ CreateMutableStringN(object/*!*/[]/*!*/ parts, RubyEncoding/*!*/ encoding) {
            return ConcatStrings(parts, encoding);
        }

        private static MutableString/*!*/ ConcatStrings(object/*!*/[]/*!*/ parts, RubyEncoding/*!*/ encoding) {
            var result = MutableString.CreateMutable(encoding);

            for (int i = 0; i < parts.Length; i++) {
                object part = parts[i];
                byte[] bytes;
                string str;

                if ((str = part as string) != null) {
                    result.Append(str);
                } else if ((bytes = part as byte[]) != null) {
                    result.Append(bytes);
                } else {
                    // TODO: check if encoding of str is compatible with encoding of the result:
                    result.Append((MutableString)part);
                }
            }

            return result;
        }

        #endregion

        #region CreateSymbol

        [Emitted]
        public static SymbolId/*!*/ CreateSymbolL(string/*!*/ str1, RubyEncoding/*!*/ encoding) {
            return ToSymbolChecked(CreateMutableStringL(str1, encoding));
        }
        
        [Emitted]
        public static SymbolId/*!*/ CreateSymbolM(MutableString str1, RubyEncoding/*!*/ encoding) {
            return ToSymbolChecked(CreateMutableStringM(str1, encoding));
        }

        [Emitted]
        public static SymbolId/*!*/ CreateSymbolLM(string/*!*/ str1, MutableString str2, RubyEncoding/*!*/ encoding) {
            return ToSymbolChecked(CreateMutableStringLM(str1, str2, encoding));
        }

        [Emitted]
        public static SymbolId/*!*/ CreateSymbolML(MutableString str1, string/*!*/ str2, RubyEncoding/*!*/ encoding) {
            return ToSymbolChecked(CreateMutableStringML(str1, str2, encoding));
        }
        
        [Emitted]
        public static SymbolId/*!*/ CreateSymbolMM(MutableString str1, MutableString str2, RubyEncoding/*!*/ encoding) {
            return ToSymbolChecked(CreateMutableStringMM(str1, str2, encoding));
        }

        [Emitted]
        public static SymbolId/*!*/ CreateSymbolN(object[]/*!*/ strings, RubyEncoding/*!*/ encoding) {
            return ToSymbolChecked(CreateMutableStringN(strings, encoding));
        }

        private static SymbolId/*!*/ ToSymbolChecked(MutableString/*!*/ str) {
            if (str.IsEmpty) {
                throw RubyExceptions.CreateArgumentError("interning empty string");
            }
            return SymbolTable.StringToId(str.ToString());
        }

        #endregion

        [Emitted]
        public static RubyEncoding/*!*/ CreateEncoding(int codepage) {
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

        [Emitted]
        public static object NullIfFalse(object obj) {
            return (obj is bool && !(bool)obj) ? null : obj;
        }

        [Emitted]
        public static object NullIfTrue(object obj) {
            return (obj is bool && !(bool)obj || obj == null) ? DefaultArgument : null;
        }

        #region Exceptions

        //
        // NOTE:
        // Exception Ops go directly to the current exception object. MRI ignores potential aliases.
        //

        [Emitted]
        public static bool FilterBlockException(RubyScope/*!*/ scope, Exception/*!*/ exception) {
            RubyExceptionData.GetInstance(exception).CaptureExceptionTrace(scope);
            return false;
        }

        [Emitted]
        public static bool CanRescue(RubyScope/*!*/ scope, Exception/*!*/ exception) {
            if (exception is StackUnwinder) {
                return false;
            }

            LocalJumpError lje = exception as LocalJumpError;
            if (lje != null && lje.SkipFrame == scope.FlowControlScope) {
                return false;
            }

            RubyExceptionData.GetInstance(exception).CaptureExceptionTrace(scope);
            scope.RubyContext.CurrentException = exception;
            return true;
        }

        [Emitted]
        public static bool TraceTopLevelCodeFrame(RubyScope/*!*/ scope, Exception/*!*/ exception) {
            RubyExceptionData.GetInstance(exception).CaptureExceptionTrace(scope);
            return false;
        }

        [Emitted] //Body, RescueClause:
        public static Exception GetCurrentException(RubyScope/*!*/ scope) {
            return scope.RubyContext.CurrentException;
        }

        [Emitted] //Body:
        public static void SetCurrentException(RubyScope/*!*/ scope, Exception exception) {
            scope.RubyContext.CurrentException = exception;
        }

        [Emitted] //RescueClause:
        public static bool CompareException(BinaryOpStorage/*!*/ comparisonStorage, RubyScope/*!*/ scope, object classObject) {            
            // throw the same exception when classObject is nil
            if (!(classObject is RubyModule)) {
                throw RubyExceptions.CreateTypeError("class or module required for rescue clause");
            }

            var context = scope.RubyContext;
            var site = comparisonStorage.GetCallSite("===");
            bool result = IsTrue(site.Target(site, classObject, context.CurrentException));
            if (result) {
                RubyExceptionData.ActiveExceptionHandled(context.CurrentException);
            }
            return result;
        }

        [Emitted] //RescueClause:
        public static bool CompareSplattedExceptions(BinaryOpStorage/*!*/ comparisonStorage, RubyScope/*!*/ scope, object classObjects) {
            var list = classObjects as IList;
            if (list != null) {
                for (int i = 0; i < list.Count; i++) {
                    if (CompareException(comparisonStorage, scope, list[i])) {
                        return true;
                    }
                }
                return false;
            } else {
                return CompareException(comparisonStorage, scope, classObjects);
            }
        }

        [Emitted] //RescueClause:
        public static bool CompareDefaultException(RubyScope/*!*/ scope) {
            RubyContext ec = scope.RubyContext;

            // MRI doesn't call === here;
            return ec.IsInstanceOf(ec.CurrentException, ec.StandardErrorClass);
        }

        [Emitted]
        public static string/*!*/ GetDefaultExceptionMessage(RubyClass/*!*/ exceptionClass) {
            return exceptionClass.Name;
        }

        [Emitted]
        public static ArgumentException/*!*/ CreateArgumentsError(string message) {
            return (ArgumentException)RubyExceptions.CreateArgumentError(message);
        }

        [Emitted]
        public static ArgumentException/*!*/ CreateArgumentsErrorForMissingBlock(string message) {
            return (ArgumentException)RubyExceptions.CreateArgumentError("block not supplied");
        }

        [Emitted]
        public static ArgumentException/*!*/ CreateArgumentsErrorForProc(string className) {
            return (ArgumentException)RubyExceptions.CreateArgumentError(String.Format("wrong type argument {0} (should be callable)", className));
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
        public static Exception/*!*/ MakeAbstractMethodCalledError(RuntimeMethodHandle/*!*/ method) {
            return new NotImplementedException(String.Format("Abstract method `{0}' not implemented", MethodInfo.GetMethodFromHandle(method)));
        }

        [Emitted]
        public static Exception/*!*/ MakeInvalidArgumentTypesError(string/*!*/ methodName) {
            // TODO:
            return new ArgumentException(String.Format("wrong number or type of arguments for `{0}'", methodName));
        }

        [Emitted]
        public static Exception/*!*/ MakeTypeConversionError(RubyContext/*!*/ context, object value, Type/*!*/ type) {
            return RubyExceptions.CreateTypeConversionError(context.GetClassDisplayName(value), context.GetTypeName(type, true));
        }

        [Emitted]
        public static Exception/*!*/ MakeAmbiguousMatchError(string/*!*/ message) {
            // TODO:
            return new AmbiguousMatchException(message);
        }

        [Emitted]
        public static Exception/*!*/ MakeAllocatorUndefinedError(RubyClass/*!*/ classObj) {
            return RubyExceptions.CreateAllocatorUndefinedError(classObj);
        }

        [Emitted]
        public static Exception/*!*/ MakeNotClrTypeError(RubyClass/*!*/ classObj) {
            return RubyExceptions.CreateNotClrTypeError(classObj);
        }

        [Emitted]
        public static Exception/*!*/ MakeConstructorUndefinedError(RubyClass/*!*/ classObj) {
            return RubyExceptions.CreateTypeError(String.Format("`{0}' doesn't have a visible CLR constructor", 
                classObj.Context.GetTypeName(classObj.TypeTracker.Type, true)
            ));
        }

        [Emitted]
        public static Exception/*!*/ MakeMissingDefaultConstructorError(RubyClass/*!*/ classObj, string/*!*/ initializerOwnerName) {
            return RubyExceptions.CreateMissingDefaultConstructorError(classObj, initializerOwnerName);
        }

        [Emitted]
        public static Exception/*!*/ MakePrivateMethodCalledError(RubyContext/*!*/ context, object target, string/*!*/ methodName) {
            return RubyExceptions.CreatePrivateMethodCalled(context, target, methodName);
        }

        [Emitted]
        public static Exception/*!*/ MakeProtectedMethodCalledError(RubyContext/*!*/ context, object target, string/*!*/ methodName) {
            return RubyExceptions.CreateProtectedMethodCalled(context, target, methodName);
        }

        [Emitted]
        public static Exception/*!*/ MakeClrProtectedMethodCalledError(RubyContext/*!*/ context, object target, string/*!*/ methodName) {
            return new MissingMethodException(
                RubyExceptions.FormatMethodMissingMessage(context, target, methodName, "CLR protected method `{0}' called for {1}; " +
                "CLR protected methods can only be called with a receiver whose class is a Ruby subclass of the class declaring the method")
            );
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

        [Emitted]
        public static object CreateDefaultInstance() {
            // nop (stub)
            return null;
        }

        #region Dynamic Operations

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
        public static DynamicMetaObject/*!*/ GetMetaObject(IRubyObject/*!*/ obj, MSA.Expression/*!*/ parameter) {
            return new RubyObject.Meta(parameter, BindingRestrictions.Empty, obj);
        }

        [Emitted]
        public static RubyMethod/*!*/ CreateBoundMember(object target, RubyMemberInfo/*!*/ info, string/*!*/ name) {
            return new RubyMethod(target, info, name);
        }

        [Emitted]
        public static RubyMethod/*!*/ CreateBoundMissingMember(object target, RubyMemberInfo/*!*/ info, string/*!*/ name) {
            return new RubyMethod.Curried(target, info, name);
        }

        [Emitted]
        public static bool IsClrSingletonRuleValid(RubyContext/*!*/ context, object/*!*/ target, int expectedVersion) {
            RubyInstanceData data;
            RubyClass immediate;

            // TODO: optimize this (we can have a hashtable of singletons per class: Weak(object) => Struct { ImmediateClass, InstanceVariables, Flags }):
            return context.TryGetClrTypeInstanceData(target, out data) && (immediate = data.ImmediateClass) != null && immediate.IsSingletonClass
                && immediate.Version.Value == expectedVersion;
        }

        [Emitted]
        public static bool IsClrNonSingletonRuleValid(RubyContext/*!*/ context, object/*!*/ target, VersionHandle/*!*/ versionHandle, int expectedVersion) {
            RubyInstanceData data;
            RubyClass immediate;

            return versionHandle.Value == expectedVersion
                // TODO: optimize this (we can have a hashtable of singletons per class: Weak(object) => Struct { ImmediateClass, InstanceVariables, Flags }):
                && !(context.TryGetClrTypeInstanceData(target, out data) && (immediate = data.ImmediateClass) != null && immediate.IsSingletonClass);
        }

        #endregion

        #region Conversions

        [Emitted] // ProtocolConversionAction
        public static Proc/*!*/ ToProcValidator(string/*!*/ className, object obj) {
            Proc result = obj as Proc;
            if (result == null) {
                throw new InvalidOperationException(String.Format("{0}#to_proc should return Proc", className));
            }
            return result;
        }

        // Used for implicit conversions from System.String to MutableString (to_str conversion like).
        [Emitted]
        public static MutableString/*!*/ StringToMutableString(string/*!*/ str) {
            return MutableString.Create(str, RubyEncoding.UTF8);
        }

        // Used for implicit conversions from System.Object to MutableString (to_s conversion like).
        [Emitted]
        public static MutableString/*!*/ ObjectToMutableString(object/*!*/ value) {
            return (value != null) ? MutableString.Create(value.ToString(), RubyEncoding.UTF8) : MutableString.FrozenEmpty;
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
        public static double ToFloatValidator(string/*!*/ className, object obj) {
            if (obj is double) {
                return (double)obj;
            }

            // to_f should not return System.Single in pure Ruby code. However, we allow it in IronRuby code
            if (obj is float) {
                return (double)(float)obj;
            }

            throw new InvalidOperationException(String.Format("{0}#to_f should return Float", className));
        }

        [Emitted]
        public static double ConvertBignumToFloat(BigInteger/*!*/ value) {
            double result;
            return value.TryToFloat64(out result) ? result : (value.IsNegative() ? Double.NegativeInfinity : Double.PositiveInfinity);
        }

        [Emitted]
        public static double ConvertMutableStringToFloat(RubyContext/*!*/ context, MutableString/*!*/ value) {
            return ConvertStringToFloat(context, value.ConvertToString());
        }

        [Emitted]
        public static double ConvertStringToFloat(RubyContext/*!*/ context, string/*!*/ value) {
            double result;
            bool complete;
            if (Tokenizer.TryParseDouble(value, out result, out complete) && complete) {
                return result;
            }

            throw RubyExceptions.InvalidValueForType(context, value, "Float");
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

        [Emitted] // ConvertDoubleToFixnum
        public static int ConvertDoubleToFixnum(double value) {
            try {
                return checked((int)value);
            } catch (OverflowException) {
                throw RubyExceptions.CreateRangeError(String.Format("float {0} out of range of Fixnum", value));
            }
        }

        [Emitted] // ConvertToSAction
        public static MutableString/*!*/ ToSDefaultConversion(RubyContext/*!*/ context, object target, object converted) {
            return converted as MutableString ?? RubyUtils.ObjectToMutableString(context, target);
        }

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
        public static object GetClassVariable(RubyScope/*!*/ scope, string/*!*/ name) {
            // owner is the first module in scope:
            RubyModule owner = scope.GetInnerMostModuleForClassVariableLookup();
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
        public static object TryGetClassVariable(RubyScope/*!*/ scope, string/*!*/ name) {
            object value;
            // owner is the first module in scope:
            scope.GetInnerMostModuleForClassVariableLookup().TryResolveClassVariable(name, out value);
            return value;
        }

        [Emitted]
        public static bool IsDefinedClassVariable(RubyScope/*!*/ scope, string/*!*/ name) {
            // owner is the first module in scope:
            RubyModule owner = scope.GetInnerMostModuleForClassVariableLookup();
            object value;
            return owner.TryResolveClassVariable(name, out value) != null;
        }

        [Emitted]
        public static object SetClassVariable(object value, RubyScope/*!*/ scope, string/*!*/ name) {
            return SetClassVariableInternal(scope.GetInnerMostModuleForClassVariableLookup(), name, value);
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

        [Emitted]
        public static bool IsObjectFrozen(RubyInstanceData instanceData) {
            return instanceData != null && instanceData.Frozen;
        }

        [Emitted]
        public static bool IsObjectTainted(RubyInstanceData instanceData) {
            return instanceData != null && instanceData.Tainted;
        }

        [Emitted]
        public static void FreezeObject(ref RubyInstanceData instanceData) {
            RubyOps.GetInstanceData(ref instanceData).Freeze();
        }

        [Emitted]
        public static void SetObjectTaint(ref RubyInstanceData instanceData, bool value) {
            RubyOps.GetInstanceData(ref instanceData).Tainted = value;
        }

#if !SILVERLIGHT
        [Emitted] //RubyTypeBuilder
        public static void DeserializeObject(out RubyInstanceData/*!*/ instanceData, out RubyClass/*!*/ immediateClass, SerializationInfo/*!*/ info) {
            immediateClass = (RubyClass)info.GetValue(RubyUtils.SerializationInfoClassKey, typeof(RubyClass));
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
        public static void SerializeObject(RubyInstanceData instanceData, RubyClass/*!*/ immediateClass, SerializationInfo/*!*/ info) {
            info.AddValue(RubyUtils.SerializationInfoClassKey, immediateClass, typeof(RubyClass));
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
        public static Proc/*!*/ HookupEvent(RubyEventInfo/*!*/ eventInfo, object/*!*/ target, Proc/*!*/ proc) {
            eventInfo.Tracker.AddHandler(target, proc, eventInfo.Context);
            return proc;
        }

        [Emitted]
        public static RubyEvent/*!*/ CreateEvent(RubyEventInfo/*!*/ eventInfo, object/*!*/ target, string/*!*/ name) {
            return new RubyEvent(target, eventInfo, name);
        }

        [Emitted]
        public static Delegate/*!*/ CreateDelegateFromProc(Type/*!*/ type, Proc/*!*/ proc) {
            BlockParam bp = CreateBfcForProcCall(proc);
            return proc.LocalScope.RubyContext.GetDelegate(bp, type);
        }

        [Emitted]
        public static Delegate/*!*/ CreateDelegateFromMethod(Type/*!*/ type, RubyMethod/*!*/ method) {
            return method.Info.Context.GetDelegate(method, type);
        }

        #endregion

        [Emitted]
        public static void X(string marker) {
        }
    }
}

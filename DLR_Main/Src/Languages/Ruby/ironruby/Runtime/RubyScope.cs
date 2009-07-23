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
using System.Diagnostics;
using System.Runtime.CompilerServices;
#if !CODEPLEX_40
using Microsoft.Runtime.CompilerServices;
#endif

using System.Text.RegularExpressions;
using IronRuby.Builtins;
using Microsoft.Scripting;
using Microsoft.Scripting.Interpreter;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using System.Threading;

namespace IronRuby.Runtime {

    public enum ScopeKind {
        TopLevel,
        Method,
        Module,
        Block
    }

    public class RuntimeFlowControl {
        // null -> this is an inactive flow-control scope (method or top)
        // self -> this is an active RubyMethodScope or light RuntimeFlowControl scope (top scope cannot be active)
        // other -> the inner-most flow-control scope
        internal RuntimeFlowControl _activeFlowControlScope;

        internal void InitializeRfc(Proc proc) {
            if (proc != null && proc.Kind == ProcKind.Block) {
                proc.Kind = ProcKind.Proc;
                proc.Converter = this;
            }
        }

        internal RuntimeFlowControl() {
            // Perf note: Initialization is performed completely in the most derived scope classes to improve perf.
        }

        internal bool IsActiveMethod {
            get { return _activeFlowControlScope == this; }
        }

        internal void LeaveMethod() {
            Debug.Assert(!(this is RubyBlockScope) && !(this is RubyModuleScope));
            _activeFlowControlScope = null;
        }

        internal RuntimeFlowControl/*!*/ FlowControlScope {
            get { return _activeFlowControlScope ?? this; }
        }
    }
        
#if !SILVERLIGHT
    [DebuggerTypeProxy(typeof(RubyScope.DebugView))]
#endif
    public abstract class RubyScope : RuntimeFlowControl {
        internal bool InLoop;
        internal bool InRescue;

        // closure:
        private Dictionary<SymbolId, int> _staticLocalMapping;
        private Dictionary<SymbolId, object> _dynamicLocals;
        internal /*and protected*/ MutableTuple _locals; // null if there are no variables
        internal /*and protected*/ SymbolId[] _variableNames; // empty if there are no variables

        internal /*and protected readonly*/ RubyTopLevelScope/*!*/ _top;
        internal /*and protected readonly*/ RubyScope _parent;

        internal /*and protected readonly*/ object _selfObject;

        // cached ImmediateClassOf(_selfObject):
        private RubyClass _selfImmediateClass;

        // set by private/public/protected/module_function
        internal /*and protected*/ RubyMethodAttributes _methodAttributes;

        internal InterpretedFrame InterpretedFrame { get; set; }

        public abstract ScopeKind Kind { get; }
        public abstract bool InheritsLocalVariables { get; }

        internal RubyScope() {
            // Perf note: Initialization is performed completely in the most derived scope classes to improve perf.
        }

        public virtual RubyModule Module {
            get { return null; }
        }

        public object SelfObject {
            get { return _selfObject; }
        }

        internal RubyClass/*!*/ SelfImmediateClass {
            get {
                if (_selfImmediateClass == null) {
                    // thread-safe, since all threads will calculate the same result:
                    _selfImmediateClass = RubyContext.GetImmediateClassOf(_selfObject);
                }
                return _selfImmediateClass; 
            }
        }

        public RubyMethodVisibility Visibility {
            get { return (RubyMethodVisibility)(_methodAttributes & RubyMethodAttributes.VisibilityMask); }
        }

        public RubyMethodAttributes MethodAttributes {
            get { return _methodAttributes; }
            set { _methodAttributes = value; }
        }

        public RubyGlobalScope/*!*/ GlobalScope {
            get { return _top.RubyGlobalScope; }
        }

        public RubyTopLevelScope/*!*/ Top {
            get { return _top; }
        }

        public RubyContext/*!*/ RubyContext {
            get { return _top.RubyContext; }
        }

        internal MutableTuple Locals {
            get { return _locals; }
        }

        internal bool LocalsInitialized {
            get { return _variableNames != null; }
        }

        public RubyScope Parent {
            get { return _parent; }
        }

        public bool IsEmpty {
            get { return RubyContext.EmptyScope == this; }
        }

        protected virtual bool IsClosureScope {
            get { return false; }
        }

        protected virtual bool IsFlowControlScope {
            get { return false; }
        }

        #region Local Variables

        internal void SetLocals(MutableTuple locals, SymbolId[]/*!*/ variableNames) {
            Debug.Assert(_variableNames == null);
            _locals = locals;
            _variableNames = variableNames;
        }

        internal void SetEmptyLocals() {
            Debug.Assert(_variableNames == null);
            _variableNames = SymbolId.EmptySymbols;
            _locals = null;
        }

        private void EnsureBoxes() {
            if (_staticLocalMapping == null) {
                int count = _variableNames.Length;
                Dictionary<SymbolId, int> boxes = new Dictionary<SymbolId, int>(count);
                for (int i = 0; i < count; i++) {
                    boxes[_variableNames[i]] = i;
                }
                _staticLocalMapping = boxes;
            }
        }

        private bool TryGetLocal(SymbolId name, out object value) {
            EnsureBoxes();

            int index;
            if (_staticLocalMapping.TryGetValue(name, out index)) {
                Debug.Assert(_locals != null);
                value = _locals.GetValue(index);
                return true;
            }

            if (_dynamicLocals == null) {
                value = null;
                return false;
            }

            lock (_dynamicLocals) {
                return _dynamicLocals.TryGetValue(name, out value);
            }
        }

        private bool TrySetLocal(SymbolId name, object value) {
            EnsureBoxes();

            int index;
            if (_staticLocalMapping.TryGetValue(name, out index)) {
                Debug.Assert(_locals != null);
                _locals.SetValue(index, value);
                return true;
            }

            if (_dynamicLocals == null) {
                return false;
            }

            lock (_dynamicLocals) {
                if (!_dynamicLocals.ContainsKey(name)) {
                    return false;
                }

                _dynamicLocals[name] = value;
            }
            return true;
        }

        private IEnumerable<KeyValuePair<SymbolId, object>>/*!*/ GetDeclaredLocalVariables() {
            for (int i = 0; i < _variableNames.Length; i++) {
                Debug.Assert(_locals != null);
                yield return new KeyValuePair<SymbolId, object>(_variableNames[i], _locals.GetValue(i));
            }

            if (_dynamicLocals != null) {
                lock (_dynamicLocals) {
                    foreach (var entry in _dynamicLocals) {
                        yield return entry;
                    }
                }
            }
        }

        private IEnumerable<SymbolId>/*!*/ GetDeclaredLocalSymbols() {
            for (int i = 0; i < _variableNames.Length; i++) {
                yield return _variableNames[i];
            }

            if (_dynamicLocals != null) {
                lock (_dynamicLocals) {
                    foreach (SymbolId name in _dynamicLocals.Keys) {
                        yield return name;
                    }
                }
            }
        }

        public List<string/*!*/>/*!*/ GetVisibleLocalNames() {
            var result = new List<string>();
            RubyScope scope = this;
            while (true) {
                foreach (SymbolId name in scope.GetDeclaredLocalSymbols()) {
                    result.Add(SymbolTable.IdToString(name));
                }

                if (!scope.InheritsLocalVariables) {
                    return result;
                }

                scope = scope.Parent;
            }
        }

        internal object ResolveLocalVariable(SymbolId name) {
            RubyScope scope = this;
            while (true) {
                object value;
                if (scope.TryGetLocal(name, out value)) {
                    return value;
                }

                if (!scope.InheritsLocalVariables) {
                    return null;
                }

                scope = scope.Parent;
            }
        }

        internal object ResolveAndSetLocalVariable(SymbolId name, object value) {
            RubyScope scope = this;
            while (true) {
                if (scope.TrySetLocal(name, value)) {
                    return value;
                }

                if (!scope.InheritsLocalVariables) {
                    if (_dynamicLocals == null) {
                        Interlocked.CompareExchange(ref _dynamicLocals, new Dictionary<SymbolId, object>(), null);
                    }

                    lock (_dynamicLocals) {
                        return _dynamicLocals[name] = value;
                    }
                }

                scope = scope.Parent;
            }
        }

        #endregion

        #region Lexical Resolution

        public RubyModule/*!*/ GetInnerMostModuleForConstantLookup() {
            return GetInnerMostModule(false, RubyContext.ObjectClass);
        }

        public RubyModule/*!*/ GetInnerMostModuleForMethodLookup() {
            return GetInnerMostModule(false, Top.MethodLookupModule ?? RubyContext.ObjectClass);
        }

        public RubyModule/*!*/ GetInnerMostModuleForClassVariableLookup() {
            return GetInnerMostModule(true, RubyContext.ObjectClass);
        }
        
        private RubyModule/*!*/ GetInnerMostModule(bool skipSingletons, RubyModule/*!*/ fallbackModule) {
            RubyScope scope = this;
            do {
                RubyModule result = scope.Module;
                if (result != null && (!skipSingletons || !result.IsSingletonClass)) {
                    return result;
                }
                scope = scope.Parent;
            } while (scope != null);
            return fallbackModule;
        }

        public RubyMethodScope GetInnerMostMethodScope() {
            RubyScope scope = this;
            while (scope != null && scope.Kind != ScopeKind.Method) {
                scope = scope.Parent;
            }
            return (RubyMethodScope)scope;
        }

        public RubyClosureScope/*!*/ GetInnerMostClosureScope() {
            RubyScope scope = this;
            while (scope != null && !scope.IsClosureScope) {
                scope = scope.Parent;
            }
            return (RubyClosureScope)scope;
        }

        public void GetInnerMostBlockOrMethodScope(out RubyBlockScope blockScope, out RubyMethodScope methodScope) {
            methodScope = null;
            blockScope = null;
            RubyScope scope = this;
            while (scope != null) {
                switch (scope.Kind) {
                    case ScopeKind.Block:
                        blockScope = (RubyBlockScope)scope;
                        return;

                    case ScopeKind.Method:
                        methodScope = (RubyMethodScope)scope;
                        return;
                }

                scope = scope.Parent;
            }
        }

        internal void GetSuperCallTarget(out RubyModule declaringModule, out string/*!*/ methodName, out object self) {
            RubyScope scope = this;
            while (true) {
                Debug.Assert(scope != null);

                switch (scope.Kind) {
                    case ScopeKind.Method:
                        RubyMethodScope methodScope = (RubyMethodScope)scope;
                        // See RubyOps.DefineMethod for why we can use Method here.
                        declaringModule = methodScope.DeclaringModule;
                        methodName = methodScope.DefinitionName;
                        self = scope.SelfObject;
                        return;

                    case ScopeKind.Block:
                        BlockParam blockParam = ((RubyBlockScope)scope).BlockFlowControl;
                        if (blockParam.MethodName != null) {
                            declaringModule = blockParam.MethodLookupModule;
                            methodName = blockParam.MethodName;
                            self = scope.SelfObject;
                            return;
                        }
                        break;

                    case ScopeKind.TopLevel:
                        throw RubyOps.MakeTopLevelSuperException();
                }

                scope = scope.Parent;
            }
        }

        public RubyScope/*!*/ GetMethodAttributesDefinitionScope() {
            RubyScope scope = this;
            while (true) {
                if (scope.Kind == ScopeKind.Block) {
                    BlockParam blockParam = ((RubyBlockScope)scope).BlockFlowControl;
                    if (blockParam.MethodLookupModule != null && blockParam.MethodName == null) {
                        return scope;
                    }
                } else {
                    return scope;
                }

                scope = scope.Parent;
            }
        }

        internal RubyModule/*!*/ GetMethodDefinitionOwner() {
            // MRI 1.9: skips all module_eval and define_method blocks.
            // MRI 1.8: skips module_eval and define_method blocks above method scope.
            // IronRuby: Fallback to the top-level singleton class when hosted.
            if (RubyContext.RubyOptions.Compatibility == RubyCompatibility.Ruby19) {
                return GetInnerMostModuleForMethodLookup();
            }

            RubyScope scope = this;
            while (true) {
                Debug.Assert(scope != null);

                switch (scope.Kind) {
                    case ScopeKind.TopLevel:
                        Debug.Assert(scope == Top);
                        return Top.MethodLookupModule ?? Top.TopModuleOrObject;

                    case ScopeKind.Module:
                        Debug.Assert(scope.Module != null);
                        return scope.Module;

                    case ScopeKind.Method:
                        return scope.GetInnerMostModuleForMethodLookup();

                    case ScopeKind.Block:
                        BlockParam blockParam = ((RubyBlockScope)scope).BlockFlowControl;
                        if (blockParam.MethodLookupModule != null) {
                            return blockParam.MethodLookupModule;
                        }
                        break;
                }

                scope = scope.Parent;
            }
        }

        // thread-safe:
        // dynamic dispatch to "const_missing" if not found
        public object ResolveConstant(bool autoload, string/*!*/ name) {
            object result;

            if (TryResolveConstant(autoload, name, out result)) {
                return result;
            }

            RubyContext.CheckConstantName(name);
            var owner = GetInnerMostModuleForConstantLookup();
            return owner.ConstantMissing(name);
        }

        // thread-safe:
        public bool TryResolveConstant(bool autoload, string/*!*/ name, out object result) {
            var context = RubyContext;
            using (context.ClassHierarchyLocker()) {
                RubyGlobalScope autoloadScope = autoload ? GlobalScope : null;
                RubyScope scope = this;

                // lexical lookup first:
                RubyModule innerMostModule = null;
                do {
                    RubyModule module = scope.Module;

                    if (module != null) {
                        if (module.TryGetConstant(context, autoloadScope, name, out result)) {
                            return true;
                        }

                        // remember the module:
                        if (innerMostModule == null) {
                            innerMostModule = module;
                        }
                    }

                    scope = scope.Parent;
                } while (scope != null);

                // check the inner most module and it's base classes/mixins:
                if (innerMostModule != null && innerMostModule.TryResolveConstant(context, autoloadScope, name, out result)) {
                    return true;
                }

                return RubyContext.ObjectClass.TryResolveConstant(context, autoloadScope, name, out result);
            }
        }

        #endregion

        #region Debug View

#if DEBUG
        private string _debugName;

        public override string ToString() {
            return _debugName;
        }
#endif
        [Conditional("DEBUG")]
        public void SetDebugName(string name) {
#if DEBUG
            _debugName = name;
#endif
        }

#if !SILVERLIGHT
        internal sealed class DebugView {
            private readonly RubyScope/*!*/ _scope;
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
            private readonly string/*!*/ _selfClassName;
            
            public DebugView(RubyScope/*!*/ scope) {
                Assert.NotNull(scope);
                _scope = scope;
                _selfClassName = _scope.RubyContext.GetImmediateClassOf(_scope._selfObject).GetDisplayName(_scope.RubyContext, true).ConvertToString();               
            }

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public VariableView[]/*!*/ A0 {
                get {
                    List<VariableView> result = new List<VariableView>();
                    RubyScope scope = _scope;
                    while (true) {
                        foreach (var variable in scope.GetDeclaredLocalVariables()) {
                            string name = SymbolTable.IdToString(variable.Key);
                            string className = _scope.RubyContext.GetImmediateClassOf(variable.Value).GetDisplayName(_scope.RubyContext, true).ConvertToString();
                            if (scope != _scope) {
                                name += " (outer)";
                            }
                            result.Add(new VariableView(name, variable.Value, className));
                        }

                        if (!scope.InheritsLocalVariables) {
                            break;
                        }
                        scope = scope.Parent;
                    }
                    return result.ToArray();
                }
            }

            [DebuggerDisplay("{A1}", Name = "self", Type = "{_selfClassName,nq}")]
            public object A1 {
                get { return _scope._selfObject; }
            }

            [DebuggerDisplay("{B}", Name = "MethodAttributes", Type = "")]
            public RubyMethodAttributes B {
                get { return _scope._methodAttributes; }
            }

            [DebuggerDisplay("{C}", Name = "ParentScope", Type = "")]
            public RubyScope C {
                get { return (RubyScope)_scope.Parent; }
            }

            [DebuggerDisplay("", Name = "RawVariables", Type = "")]
            public System.Collections.Hashtable/*!*/ D {
                get {
                    System.Collections.Hashtable result = new System.Collections.Hashtable();
                    foreach (var variable in _scope.GetDeclaredLocalVariables()) {
                        result.Add(variable.Key, variable.Value);
                    }
                    return result;
                }
            }

            [DebuggerDisplay("{_value}", Name = "{_name,nq}", Type = "{_valueClassName,nq}")]
            internal struct VariableView {
                [DebuggerBrowsable(DebuggerBrowsableState.Never)]
                private readonly string/*!*/ _name;
                [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
                private readonly object _value;
                [DebuggerBrowsable(DebuggerBrowsableState.Never)]
                private readonly string/*!*/ _valueClassName;

                public VariableView(string/*!*/ name, object value, string/*!*/ valueClassName) {
                    _name = name;
                    _value = value;
                    _valueClassName = valueClassName;
                }
            }
        }
#endif
        #endregion
    }

    public abstract class RubyClosureScope : RubyScope {
        // $+
        private MatchData _currentMatch;

        // $_
        private object _lastInputLine;

        internal RubyClosureScope() {
            // Perf note: Initialization is performed completely in the most derived scope classes to improve perf.
        }

        protected override bool IsClosureScope {
            get { return true; }
        }

        public MatchData CurrentMatch {
            get { return _currentMatch; }
            set { _currentMatch = value; }
        }

        public object LastInputLine {
            get { return _lastInputLine; }
            set { _lastInputLine = value; }
        }

        internal MutableString GetCurrentMatchGroup(int index) {
            Debug.Assert(index >= 0);

            // we don't need to check index range, Groups indexer returns an unsuccessful group if out of range:
            Group group;
            if (_currentMatch != null && (group = _currentMatch.Groups[index]).Success) {
                return MutableString.Create(group.Value).TaintBy(_currentMatch.OriginalString);
            }

            return null;
        }

        internal MutableString GetCurrentMatchLastGroup() {
            if (_currentMatch != null) {
                // TODO: cache the last successful group index?
                for (int i = _currentMatch.Groups.Count - 1; i >= 0; i--) {
                    Group group = _currentMatch.Groups[i];
                    if (group.Success) {
                        return MutableString.Create(group.Value).TaintBy(_currentMatch.OriginalString);
                    }
                }
            }

            return null;
        }

#if TODO_DebugView
         [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
            private readonly string/*!*/ _matchClassName;
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
            private readonly string/*!*/ _lastInputLine;
var closureScope = scope as RubyClosureScope;
                if (closureScope != null) {
                    _matchClassName = _scope.RubyContext.GetImmediateClassOf(closureScope.CurrentMatch).GetDisplayName(true).ConvertToString();
                    _lastInputLine = _scope.RubyContext.GetImmediateClassOf(closureScope.LastInputLine).GetDisplayName(true).ConvertToString();
                }
                

            [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
            [DebuggerDisplay("{A2 != null ? A2.ToString() : \"nil\",nq}", Name = "$~", Type = "{_matchClassName,nq}")]
            public Match A2 {
                get { return (_scope._currentMatch != null) ? _scope._currentMatch.Match : null; }
            }

            [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
            [DebuggerDisplay("{A2 != null ? A2.ToString() : \"nil\",nq}", Name = "$~", Type = "{_matchClassName,nq}")]
            public Match A2 {
                get { return (_scope._currentMatch != null) ? _scope._currentMatch.Match : null; }
            }
#endif
    }

    public sealed class RubyMethodScope : RubyClosureScope {
        private readonly RubyModule/*!*/ _declaringModule;
        private readonly string/*!*/ _definitionName;
        private readonly Proc _blockParameter;

        public override ScopeKind Kind { get { return ScopeKind.Method; } }
        public override bool InheritsLocalVariables { get { return false; } }

        // Singleton module-function method shares this pointer with instance method. See RubyOps.DefineMethod for details.
        internal RubyModule/*!*/ DeclaringModule {
            get { return _declaringModule; }
        }

        internal string/*!*/ DefinitionName {
            get { return _definitionName; }
        }
        
        public Proc BlockParameter {
            get { return _blockParameter; }
        }

        internal RubyMethodScope(MutableTuple locals, SymbolId[]/*!*/ variableNames, 
            RubyScope/*!*/ parent, RubyModule/*!*/ declaringModule, string/*!*/ definitionName,
            object selfObject, Proc blockParameter, InterpretedFrame interpretedFrame) {
            Assert.NotNull(parent, declaringModule, definitionName);

            // RuntimeFlowControl:
            _activeFlowControlScope = this;
            
            // RubyScope:
            _parent = parent;
            _top = parent.Top;
            _selfObject = selfObject;
            _methodAttributes = RubyMethodAttributes.PublicInstance;
            _locals = locals;
            _variableNames = variableNames;
            InterpretedFrame = interpretedFrame;
            
            // RubyMethodScope:
            _declaringModule = declaringModule;
            _definitionName = definitionName;
            _blockParameter = blockParameter;

            InitializeRfc(blockParameter);
            SetDebugName("method " + definitionName + ((blockParameter != null) ? "&" : null));
        }
    }

    public sealed class RubyModuleScope : RubyClosureScope {
        // TODO: readonly
        private RubyModule _module;
        private readonly bool _isEval;

        public override ScopeKind Kind { get { return ScopeKind.Module; } }
        public override bool InheritsLocalVariables { get { return _isEval; } }

        public override RubyModule Module { get { return _module; } }

        internal void SetModule(RubyModule/*!*/ module) { _module = module; }

        internal RubyModuleScope(RubyScope/*!*/ parent, RubyModule module, bool isEval, object selfObject) {
            Assert.NotNull(parent);

            // RuntimeFlowControl:
            _activeFlowControlScope = parent.FlowControlScope;

            // RubyScope:
            _parent = parent;
            _top = parent.Top;
            _selfObject = selfObject;
            _methodAttributes = RubyMethodAttributes.PrivateInstance;

            // RubyModuleScope:
            _module = module;
            _isEval = isEval;
            InLoop = parent.InLoop;
            InRescue = parent.InRescue;
            MethodAttributes = RubyMethodAttributes.PublicInstance;
        }
    }

    public sealed class RubyBlockScope : RubyScope {
        private readonly BlockParam/*!*/ _blockFlowControl;

        public override ScopeKind Kind { get { return ScopeKind.Block; } }
        public override bool InheritsLocalVariables { get { return true; } }

        public BlockParam/*!*/ BlockFlowControl {
            get { return _blockFlowControl; }
        }

        internal RubyBlockScope(MutableTuple locals, SymbolId[]/*!*/ variableNames,
            BlockParam/*!*/ blockFlowControl, object selfObject, InterpretedFrame interpretedFrame) {
            var parent = blockFlowControl.Proc.LocalScope;

            // RuntimeFlowControl:
            _activeFlowControlScope = parent.FlowControlScope;

            // RubyScope:
            _parent = parent;
            _top = parent.Top;
            _selfObject = selfObject;
            _methodAttributes = RubyMethodAttributes.PublicInstance;
            _locals = locals;
            _variableNames = variableNames;
            InterpretedFrame = interpretedFrame;
            
            // RubyBlockScope:
            _blockFlowControl = blockFlowControl;
        }
    }

    public sealed class RubyTopLevelScope : RubyClosureScope {
        public override ScopeKind Kind { get { return ScopeKind.TopLevel; } }
        public override bool InheritsLocalVariables { get { return false; } }

        private readonly RubyGlobalScope/*!*/ _globalScope;
        private readonly RubyContext/*!*/ _context;
        private readonly RubyModule _methodLookupModule;
        private readonly RubyModule _wrappingModule;

        public RubyGlobalScope/*!*/ RubyGlobalScope {
            get {
                if (_globalScope == null) {
                    throw new InvalidOperationException("Empty scope has no global scope.");
                }
                return _globalScope; 
            }
        }

        internal new RubyContext/*!*/ RubyContext {
            get { return _context; }
        }

        public override RubyModule Module {
            get { return _wrappingModule; }            
        }

        /// <summary>
        /// Method and class lookup in top-level hosted scope behave like if it was instance_eval'd as a proc in MRI 1.8, i.e.
        /// methods are resolved in the singleton class of the main object.
        /// </summary>
        public RubyModule MethodLookupModule {
            get { return _methodLookupModule; }
        }

        internal RubyModule/*!*/ TopModuleOrObject {
            get { return _wrappingModule ?? _globalScope.Context.ObjectClass; }
        }

        // empty scope:
        internal RubyTopLevelScope(RubyContext/*!*/ context) {
            Assert.NotNull(context);

            // RubyScope:
            _top = this;
            _methodAttributes = RubyMethodAttributes.PrivateInstance;

            // RubyTopLevelScope:
            _context = context;
            SetEmptyLocals();
        }

        internal RubyTopLevelScope(RubyGlobalScope/*!*/ globalScope, RubyModule scopeModule, RubyModule methodLookupModule, 
            RubyObject/*!*/ selfObject) {
            Assert.NotNull(globalScope, selfObject);

            // RubyScope:
            _top = this;
            _selfObject = selfObject;
            _methodAttributes = RubyMethodAttributes.PrivateInstance;

            // RubyTopLevelScope:
            _globalScope = globalScope;
            _context = globalScope.Context;
            _wrappingModule = scopeModule;
            _methodLookupModule = methodLookupModule;
        }

        #region Factories

        internal static RubyTopLevelScope/*!*/ CreateTopLevelScope(Scope/*!*/ globalScope, RubyContext/*!*/ context, bool isMain) {
            RubyGlobalScope rubyGlobalScope = context.InitializeGlobalScope(globalScope, false, false);

            RubyTopLevelScope scope = new RubyTopLevelScope(rubyGlobalScope, null, null, rubyGlobalScope.MainObject);
            if (isMain) {
                scope.SetDebugName("top-main");
                context.ObjectClass.SetConstant("TOPLEVEL_BINDING", new Binding(scope));
            } else {
                scope.SetDebugName("top-required");
            }

            return scope;
        }

        internal static RubyTopLevelScope/*!*/ CreateHostedTopLevelScope(Scope/*!*/ globalScope, RubyContext/*!*/ context, bool bindGlobals) {
            RubyGlobalScope rubyGlobalScope = context.InitializeGlobalScope(globalScope, true, bindGlobals);

            // Reuse existing top-level scope if available:
            RubyTopLevelScope scope = rubyGlobalScope.TopLocalScope;
            if (scope == null) {
                scope = new RubyTopLevelScope(
                    rubyGlobalScope, null, bindGlobals ? rubyGlobalScope.MainSingleton : null, rubyGlobalScope.MainObject
                );

                scope.SetDebugName(bindGlobals ? "top-level-bound" : "top-level");
                rubyGlobalScope.TopLocalScope = scope;
            } else {
                // If we reuse a local scope from previous execution all local variables are accessed dynamically.
                // Therefore we shouldn't have any new static local variables.
            }

            return scope;
        }

        internal static RubyTopLevelScope/*!*/ CreateWrappedTopLevelScope(Scope/*!*/ globalScope, RubyContext/*!*/ context) {
            RubyGlobalScope rubyGlobalScope = context.InitializeGlobalScope(globalScope, false, false);
            
            RubyModule module = context.CreateModule(null, null, null, null, null, null, null, ModuleRestrictions.None);
            RubyObject mainObject = new RubyObject(context.ObjectClass);
            context.CreateMainSingleton(mainObject, new[] { module });

            RubyTopLevelScope scope = new RubyTopLevelScope(rubyGlobalScope, module, null, mainObject);
            scope.SetDebugName("top-level-wrapped");

            return scope;
        }

        // "method_missing" on main singleton in DLR Scope bound code.
        // Might be called via a site -> needs to be public in partial trust.
        public static object TopMethodMissing(RubyScope/*!*/ localScope, BlockParam block, object/*!*/ self, SymbolId name, [NotNull]params object[]/*!*/ args) {
            return ScopeMethodMissing(localScope.RubyContext, localScope.GlobalScope.Scope, block, self, name, args);
        }

        public static object ScopeMethodMissing(RubyContext/*!*/ context, Scope/*!*/ globalScope, BlockParam block, object self, SymbolId name, object[]/*!*/ args) {
            Assert.NotNull(context, globalScope);

            string str = SymbolTable.IdToString(name);
            if (str.LastCharacter() == '=') {
                if (args.Length != 1) {
                    throw RubyOps.MakeWrongNumberOfArgumentsError(args.Length, 1);
                }

                // Consider this case:
                // There is {"Foo" -> 1} in the scope.
                // x.foo += 1
                // Without name mangling this would result to {"Foo" -> 1, "foo" -> 2} while the expected result is {"Foo" -> 2}.

                str = str.Substring(0, str.Length - 1);
                name = SymbolTable.StringToId(str);

                if (!globalScope.ContainsName(name)) {
                    var unmangled = SymbolTable.StringToId(RubyUtils.TryUnmangleName(str));
                    if (!unmangled.IsEmpty && globalScope.ContainsName(unmangled)) {
                        name = unmangled;
                    }
                }

                var value = args[0];
                globalScope.SetName(name, value);
                return value;
            } else {
                if (args.Length != 0) {
                    throw RubyOps.MakeWrongNumberOfArgumentsError(args.Length, 0);
                }

                object value;
                if (globalScope.TryGetName(name, out value)) {
                    return value;
                }

                string unmangled = RubyUtils.TryUnmangleName(str);
                if (unmangled != null && globalScope.TryGetName(SymbolTable.StringToId(unmangled), out value)) {
                    return value;
                }

                if (self != null && str == "scope") {
                    return self;
                }
            }

            // TODO: call super
            throw RubyExceptions.CreateMethodMissing(context, self, SymbolTable.IdToString(name));
        }

        #endregion
    }
}

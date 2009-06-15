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
using System.Diagnostics;
using System.Reflection;
using Microsoft.Scripting;
using Microsoft.Scripting.Actions;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using IronRuby.Builtins;
using IronRuby.Runtime.Calls;
#if CODEPLEX_40
using MSA = System.Linq.Expressions;
using Ast = System.Linq.Expressions.Expression;
#else
using MSA = Microsoft.Linq.Expressions;
using Ast = Microsoft.Linq.Expressions.Expression;
#endif
using AstFactory = IronRuby.Compiler.Ast.AstFactory;
using AstUtils = Microsoft.Scripting.Ast.Utils;
using IronRuby.Compiler.Generation;
#if CODEPLEX_40
using System.Dynamic;
#else
#endif

namespace IronRuby.Runtime {
    
    public enum BlockReturnReason {
        Undefined = 0,
        Retry,
        Break
    }

    public enum BlockCallerKind {
        Yield,
        Call
    }

    internal sealed class MissingBlockParam {
        /// <remarks>
        /// LimitType must be MissingBlockParam (overload resolution, <see cref="RubyParameterBinder.PrepareParametersBinding"/>).
        /// Restriction should be empty: used only for !HasBlock call-sites => the site will never be reused for a call with a block.
        /// </remarks>
        internal sealed class Meta : DynamicMetaObject, IRestrictedMetaObject {
            internal static readonly DynamicMetaObject Instance = new Meta();

            private Meta()
                : base(AstUtils.Constant(null, typeof(MissingBlockParam)), BindingRestrictions.Empty) {
                Debug.Assert(LimitType == typeof(MissingBlockParam));
            }

            public DynamicMetaObject Restrict(Type/*!*/ type) {
                Debug.Assert(type == typeof(BlockParam) || type == typeof(MissingBlockParam));
                return this;
            }
        }

    }

    public sealed partial class BlockParam {
        // -- in --
        private readonly Proc/*!*/ _proc;
        private readonly BlockCallerKind _callerKind;

        // filled by define_method, module_eval, load: if not null than method definition and method alias uses the module
        private RubyModule _methodLookupModule;

        // filled by define_method: if not null then injects a scope in super call method lookup:
        private readonly string _methodName;
        
        // Is the library method call taking this BlockParam a proc converter?
        // Used only for BlockParams that are passed to library method calls.
        // Friend: RubyOps
        internal readonly bool _isLibProcConverter;
        
        // -- out --
        private BlockReturnReason _returnReason;
        private RuntimeFlowControl _targetFrame;
        private ProcKind _sourceProcKind;

        private void ObjectInvariant() {
            ContractUtils.Invariant(_methodName == null || _methodLookupModule != null);
        }

        internal BlockCallerKind CallerKind { get { return _callerKind; } }
        internal ProcKind SourceProcKind { get { return _sourceProcKind; } }
        internal BlockReturnReason ReturnReason { get { return _returnReason; } set { _returnReason = value; } }
        internal RuntimeFlowControl TargetFrame { get { return _targetFrame; } }
        internal RubyModule MethodLookupModule { get { return _methodLookupModule; } set { _methodLookupModule = value; } }
        internal string MethodName { get { return _methodName; } }
        internal bool IsLibProcConverter { get { return _isLibProcConverter; } }
        
        public Proc/*!*/ Proc { get { return _proc; } }

        [Emitted]
        public object Self { get { return _proc.Self; } }

        public RubyContext RubyContext {
            get { return _proc.LocalScope.RubyContext; }
        }

        public bool IsMethod {
            get {
                ObjectInvariant();
                return _methodName != null; 
            }
        }

        internal static PropertyInfo/*!*/ SelfProperty { get { return typeof(BlockParam).GetProperty("Self"); } }

        // friend: RubyOps
        internal BlockParam(Proc/*!*/ proc, BlockCallerKind callerKind, bool isLibProcConverter, RubyModule moduleDeclaration, string methodName) {
            _callerKind = callerKind;
            _proc = proc;
            _isLibProcConverter = isLibProcConverter;
            _methodLookupModule = moduleDeclaration;
            _methodName = methodName;
            ObjectInvariant();
        }

        internal void SetFlowControl(BlockReturnReason reason, RuntimeFlowControl targetFrame, ProcKind sourceProcKind) {
            Debug.Assert((reason == BlockReturnReason.Break) == (targetFrame != null));

            _returnReason = reason;
            _targetFrame = targetFrame;
            _sourceProcKind = sourceProcKind;
        }

        internal object GetUnwinderResult(EvalUnwinder/*!*/ unwinder) {
            Debug.Assert(unwinder != null);
            SetFlowControl(unwinder.Reason, unwinder.TargetFrame, unwinder.SourceProcKind);
            return unwinder.ReturnValue;
        }

        internal void MultipleValuesForBlockParameterWarning(int actualCount) {
            Debug.Assert(actualCount == 0 || actualCount > 1);
            _proc.LocalScope.RubyContext.ReportWarning(String.Format("multiple values for a block parameter ({0} for 1)", actualCount));
        }

        #region Library Block Yield Helpers

        /// <summary>
        /// Used by library methods that take a block. The binder creates an instance of BlockParam holding on RFC if necessary.
        /// A library method that creates a block yet doesn't take one needs to manage RFC on its own.
        /// </summary>
        public bool BlockJumped(object returnValue) {
            // if this method is a proc converter then the current frame is Proc.Converter, otherwise it is not available:
            return RubyOps.MethodYield(_isLibProcConverter ? _proc.Converter : null, this, returnValue);
        }

        public object Break(object returnValue) {
            Debug.Assert(_proc.Converter != null);

            // unwind to proc converter:
            SetFlowControl(BlockReturnReason.Break, _proc.Converter, _proc.Kind);
            return returnValue;
        }

        public bool Yield(out object blockResult) {
            return BlockJumped(blockResult = RubyOps.Yield0(Self, this));
        }

        public bool Yield(object arg1, out object blockResult) {
            return BlockJumped(blockResult = RubyOps.Yield1(arg1, Self, this));
        }

        public bool Yield(object arg1, object arg2, out object blockResult) {
            return BlockJumped(blockResult = RubyOps.Yield2(arg1, arg2, Self, this));
        }

        public bool Yield(object arg1, object arg2, object arg3, out object blockResult) {
            return BlockJumped(blockResult = RubyOps.Yield3(arg1, arg2, arg3, Self, this));
        }

        public bool Yield(object arg1, object arg2, object arg3, object arg4, out object blockResult) {
            return BlockJumped(blockResult = RubyOps.Yield4(arg1, arg2, arg3, arg4, Self, this));
        }

        public bool Yield(object[]/*!*/ args, out object blockResult) {
            ContractUtils.RequiresNotNull(args, "args");
            switch (args.Length) {
                case 0: blockResult = RubyOps.Yield0(Self, this); break;
                case 1: blockResult = RubyOps.Yield1(args[0], Self, this); break;
                case 2: blockResult = RubyOps.Yield2(args[0], args[1], Self, this); break;
                case 3: blockResult = RubyOps.Yield3(args[0], args[1], args[2], Self, this); break;
                case 4: blockResult = RubyOps.Yield4(args[0], args[1], args[2], args[3], Self, this); break;
                default: blockResult = RubyOps.YieldN(args, Self, this); break;
            }
            return BlockJumped(blockResult);
        }

        #endregion

        #region Dynamic Operations

        /// <summary>
        /// "yields" to the proc.
        /// </summary>
        internal void BuildInvoke(MetaObjectBuilder/*!*/ metaBuilder, CallArguments/*!*/ args) {
            Assert.NotNull(metaBuilder, args);
            Debug.Assert(!args.Signature.HasBlock);

            var convertedTarget = AstUtils.Convert(args.TargetExpression, typeof(BlockParam));

            // test for target type:
            metaBuilder.AddTypeRestriction(args.Target.GetType(), args.TargetExpression);

            metaBuilder.Result = AstFactory.YieldExpression(
                args.GetSimpleArgumentExpressions(),
                args.GetSplattedArgumentExpression(),
                args.GetRhsArgumentExpression(),
                convertedTarget,                              // block param
                Ast.Property(convertedTarget, SelfProperty)   // self
            );
        }

        #endregion

    }

    #region RubyOps

    public static partial class RubyOps {

        [Emitted]
        public static bool IsProcConverterTarget(BlockParam/*!*/ bfc, MethodUnwinder/*!*/ unwinder) {
            Debug.Assert(unwinder != null);
            return bfc.IsLibProcConverter && unwinder.TargetFrame == bfc.Proc.Converter;
        }
        
        [Emitted]
        public static BlockParam/*!*/ CreateBfcForYield(Proc proc) {
            if (proc != null) {
                return new BlockParam(proc, BlockCallerKind.Yield, false, null, null);
            } else {
                throw RubyExceptions.NoBlockGiven();
            }
        }

        [Emitted]
        public static BlockParam/*!*/ CreateBfcForMethodProcCall(Proc/*!*/ proc, RubyLambdaMethodInfo/*!*/ method) {
            Assert.NotNull(proc, method);
            return new BlockParam(proc, BlockCallerKind.Call, false, method.DeclaringModule, method.DefinitionName);
        }

        [Emitted]
        public static BlockParam/*!*/ CreateBfcForProcCall(Proc/*!*/ proc) {
            Assert.NotNull(proc);
            return new BlockParam(proc, BlockCallerKind.Call, false, null, null);
        }
        
        [Emitted]
        public static BlockParam/*!*/ CreateBfcForLibraryMethod(Proc/*!*/ proc) {
            Assert.NotNull(proc);
            bool isProcConverter;

            if (proc.Kind == ProcKind.Block) {
                proc.Converter = new RuntimeFlowControl();
                proc.Converter.IsActiveMethod = true;
                proc.Kind = ProcKind.Proc;
                isProcConverter = true;
            } else {
                isProcConverter = false;
            }

            return new BlockParam(proc, BlockCallerKind.Yield, isProcConverter, null, null);
        }

        [Emitted] 
        public static void LeaveProcConverter(BlockParam/*!*/ bfc) {
            Debug.Assert(bfc.Proc != null);
            if (bfc._isLibProcConverter) {
                Debug.Assert(bfc.Proc.Converter != null);
                bfc.Proc.Converter.IsActiveMethod = false;
            }
        }
    }

    #endregion
}

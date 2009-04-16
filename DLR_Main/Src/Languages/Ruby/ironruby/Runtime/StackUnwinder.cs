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
using System.Reflection;
using Microsoft.Scripting.Utils;
using IronRuby.Builtins;
using IronRuby.Compiler.Generation;

namespace IronRuby.Runtime {

    public abstract class StackUnwinder : Exception {
        [Emitted]
        public readonly object ReturnValue;

        internal static FieldInfo ReturnValueField { get { return typeof(StackUnwinder).GetField("ReturnValue"); } }
        //internal static MethodInfo GetMethodCallWithBlockResultMethod { get { return typeof(StackUnwinder).GetMethod("GetMethodCallWithBlockResult"); } }

        public StackUnwinder(object returnValue) {
            ReturnValue = returnValue;
        }
    }
    
    /// <summary>
    /// Return.
    /// </summary>
    public sealed class MethodUnwinder : StackUnwinder {
        [Emitted]
        public readonly RuntimeFlowControl/*!*/ TargetFrame;

        internal static FieldInfo TargetFrameField { get { return typeof(MethodUnwinder).GetField("TargetFrame"); } }

        internal MethodUnwinder(RuntimeFlowControl/*!*/ targetFrame, object returnValue)
            : base(returnValue) {
            Assert.NotNull(targetFrame);
            TargetFrame = targetFrame;
        }
    }

    /// <summary>
    /// Retry/Break.
    /// </summary>
    public sealed class EvalUnwinder : StackUnwinder {
        private readonly RuntimeFlowControl _targetFrame;
        private readonly ProcKind _sourceProcKind;

        [Emitted]
        public readonly BlockReturnReason Reason;
        
        internal static FieldInfo ReasonField { get { return typeof(EvalUnwinder).GetField("Reason"); } }

        internal RuntimeFlowControl TargetFrame { get { return _targetFrame; } }
        internal ProcKind SourceProcKind { get { return _sourceProcKind; } }

        internal EvalUnwinder(BlockReturnReason reason, RuntimeFlowControl targetFrame, ProcKind sourceProcKind, object returnValue)
            : base(returnValue) {

            Reason = reason;
            _targetFrame = targetFrame;
            _sourceProcKind = sourceProcKind;
        }
    }

    /// <summary>
    /// Redo/Next.
    /// </summary>
    public sealed class BlockUnwinder : StackUnwinder {
        [Emitted]
        public readonly bool IsRedo;

        internal static FieldInfo IsRedoField { get { return typeof(BlockUnwinder).GetField("IsRedo"); } }

        internal BlockUnwinder(object returnValue, bool isRedo)
            : base(returnValue) {
            IsRedo = isRedo;
        }
    }
}

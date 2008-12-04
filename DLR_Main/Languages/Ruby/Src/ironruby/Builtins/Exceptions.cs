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
using Microsoft.Scripting.Utils;
using IronRuby.Runtime;

namespace IronRuby.Builtins {

    [Serializable]
    public class LocalJumpError : SystemException {

        private readonly RuntimeFlowControl _skipFrame;

        /// <summary>
        /// The exception cannot be rescued in this frame if set.
        /// </summary>
        internal RuntimeFlowControl SkipFrame {
            get { return _skipFrame; }
        }

        internal LocalJumpError(string/*!*/ message, RuntimeFlowControl/*!*/ skipFrame)
            : this(message, (Exception)null) {
            Assert.NotNull(message, skipFrame);
            _skipFrame = skipFrame;
        }

        public LocalJumpError() : this(null, (Exception)null) { }
        public LocalJumpError(string message) : this(message, (Exception)null) { }
        public LocalJumpError(string message, Exception inner) : base(message, inner) { }

#if !SILVERLIGHT
        protected LocalJumpError(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
#endif
    }

    [Serializable]
    public class SystemExit : Exception {
        private readonly int _status;

        public int Status {
            get { return _status; }
        }

        public SystemExit(int status, string message)
            : this(message) {
            _status = status;
        }

        public SystemExit(int status) 
            : this() {
            _status = status;
        }

        public SystemExit() : this(null, null) { }
        public SystemExit(string message) : this(message, null) { }
        public SystemExit(string message, Exception inner) : base(message, inner) { }

#if !SILVERLIGHT
        protected SystemExit(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
#endif
    }

    [Serializable]
    public class ScriptError : Exception {
        public ScriptError() : this(null, null) { }
        public ScriptError(string message) : this(message, null) { }
        public ScriptError(string message, Exception inner) : base(message, inner) { }

#if !SILVERLIGHT
        protected ScriptError(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
#endif
    }

    [Serializable]
    public class NotImplementedError : ScriptError {
        public NotImplementedError() : this(null, null) { }
        public NotImplementedError(string message) : this(message, null) { }
        public NotImplementedError(string message, Exception inner) : base(message, inner) { }

#if !SILVERLIGHT
        protected NotImplementedError(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
#endif
    }

    [Serializable]
    public class LoadError : ScriptError {
        public LoadError() : this(null, null) { }
        public LoadError(string message) : this(message, null) { }
        public LoadError(string message, Exception inner) : base(message, inner) { }

#if !SILVERLIGHT
        protected LoadError(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
#endif
    }

    [Serializable]
    public class SystemStackError : SystemException {
        public SystemStackError() : this(null, null) { }
        public SystemStackError(string message) : this(message, null) { }
        public SystemStackError(string message, Exception inner) : base(message, inner) { }

#if !SILVERLIGHT
        protected SystemStackError(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
#endif
    }

    [Serializable]
    public class RegexpError : SystemException {
        public RegexpError() : this(null, null) { }
        public RegexpError(string message) : this(message, null) { }
        public RegexpError(string message, Exception inner) : base(message, inner) { }

#if !SILVERLIGHT
        protected RegexpError(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
#endif
    }

    [Serializable]
    public class SyntaxError : ScriptError {
        private readonly string _file;
        private readonly string _lineSourceCode;
        private readonly int _line;
        private readonly int _column;
        private readonly bool _hasLineInfo;

        public SyntaxError() : this(null, null) { }
        public SyntaxError(string message) : this(message, null) { }
        public SyntaxError(string message, Exception inner) : base(message, inner) { }

        internal string File {
            get { return _file; }
        }

        internal int Line {
            get { return _line; }
        }

        internal int Column {
            get { return _column; }
        }

        internal string LineSourceCode {
            get { return _lineSourceCode; }
        }

        internal bool HasLineInfo {
            get { return _hasLineInfo; }
        }

        internal SyntaxError(string/*!*/ message, string file, int line, int column, string lineSourceCode) 
            : base(message) {
            _file = file;
            _line = line;
            _column = column;
            _lineSourceCode = lineSourceCode;
            _hasLineInfo = true;
        }

#if !SILVERLIGHT
        protected SyntaxError(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
#endif
    }

    
}


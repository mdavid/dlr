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


using System.Diagnostics;
using IronRuby.Compiler.Generation;

namespace IronRuby.Runtime.Calls {
    public sealed class VersionHandle {
        [Emitted]
        public int Value;

        internal VersionHandle(int value) {
            Value = value;
        }

#if DEBUG
        private string/*!*/ _className;

        public override string/*!*/ ToString() {
            return _className;
        }
#endif
        [Conditional("DEBUG")]
        internal void SetName(string/*!*/ className) {
#if DEBUG
            _className = className;
#endif
        }
    }
}

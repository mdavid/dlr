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
using IronRuby.Runtime;

namespace IronRuby.Builtins {

    [RubyClass("FalseClass")]
    public static class FalseClass : Object {
        #region Public Instance Methods

        [RubyMethodAttribute("to_s")]
        public static MutableString/*!*/ ToString(bool self) {
            Debug.Assert(self == false);
            return MutableString.Create("false"); 
        }

        [RubyMethodAttribute("&")]
        public static bool And(bool self, object obj) {
            Debug.Assert(self == false);
            return false;
        }

        [RubyMethodAttribute("^")]
        public static bool Xor(bool self, object obj) {
            Debug.Assert(self == false);
            return obj != null;
        }

        [RubyMethodAttribute("^")]
        public static bool Xor(bool self, bool obj) {
            Debug.Assert(self == false);
            return obj;
        }

        [RubyMethodAttribute("|")]
        public static bool Or(bool self, object obj) {
            Debug.Assert(self == false);
            return obj != null;
        }

        [RubyMethodAttribute("|")]
        public static bool Or(bool self, bool obj) {
            Debug.Assert(self == false);
            return obj;
        }

        #endregion
    }
}

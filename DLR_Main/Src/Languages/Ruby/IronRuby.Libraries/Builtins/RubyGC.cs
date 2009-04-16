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
using System.Collections.Generic;
using Microsoft.Scripting;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Actions;
using IronRuby.Runtime;

namespace IronRuby.Builtins {
    [RubyModule("GC")]
    public static class RubyGC {
        [RubyMethod("enable", RubyMethodAttributes.PublicSingleton)]
        public static bool Enable(object self) {
            return false;
        }

        [RubyMethod("disable", RubyMethodAttributes.PublicSingleton)]
        public static bool Disable(object self) {
            return false;
        }

        [RubyMethod("start", RubyMethodAttributes.PublicSingleton)]
        [RubyMethod("garbage_collect", RubyMethodAttributes.PublicInstance)]
        public static void GarbageCollect(object self) {
            GC.Collect();
        }
    }
}

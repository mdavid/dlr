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
using System.Runtime.CompilerServices;
using Microsoft.Runtime.CompilerServices;

using IronRuby.Runtime;
using IronRuby.Runtime.Calls;
using Microsoft.Scripting;
using Microsoft.Scripting.Runtime;

namespace IronRuby.Builtins {

    // TODO: remove
    public static class RubySites {
        public static RubyCallAction InstanceCallAction(string/*!*/ name) {
            return RubyCallAction.Make(name, 0);
        }
        
        public static RubyCallAction InstanceCallAction(string/*!*/ name, int argumentCount) {
            return RubyCallAction.Make(name, argumentCount);
        }

        public static RubyCallAction InstanceCallAction(string/*!*/ name, RubyCallSignature callSignature) {
            return RubyCallAction.Make(name, callSignature);
        }
    }
}

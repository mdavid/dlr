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
using Microsoft.Scripting.Runtime;
using IronRuby.Runtime;

namespace IronRuby.Builtins {
    [RubyClass(Extends = typeof(Type), Restrictions = ModuleRestrictions.None)]
    public static class TypeOps {
        [RubyMethod("to_module")]
        public static RubyModule/*!*/ ToModule(RubyContext/*!*/ context, Type/*!*/ self) {
            return context.GetModule(self);
        }

        [RubyMethod("to_class")]
        public static RubyClass/*!*/ ToClass(RubyContext/*!*/ context, Type/*!*/ self) {
            if (self.IsInterface) {
                RubyExceptions.CreateTypeError("Cannot convert a CLR interface to a Ruby class");
            }
            return context.GetClass(self);
        }
    }
}

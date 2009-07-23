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


using IronRuby.Builtins;

namespace IronRuby.Runtime.Calls {
    public struct VisibilityContext {
        public static readonly VisibilityContext AllVisible = new VisibilityContext(RubyMethodAttributes.VisibilityMask);

        public readonly RubyClass Class;
        public readonly RubyMethodAttributes Visible;

        public VisibilityContext(RubyMethodAttributes mask) {
            Class = null;
            Visible = mask;
        }

        public VisibilityContext(RubyClass cls) {
            Class = cls;
            Visible = RubyMethodAttributes.VisibilityMask;
        }

        public bool IsVisible(RubyMethodVisibility visibility) {
            return ((int)visibility & (int)Visible) != 0;
        }
    }
}

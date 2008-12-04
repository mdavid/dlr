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

using IronRuby.Builtins;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;

namespace IronRuby.Runtime {
    // Even though Ruby types overload Equals & GetHashCode, we can't use them
    // because monkeypatching allows for implementing "hash" and "eql?" on any type
    // (including instances of arbitrary .NET types via singleton methods)
    // TODO: optimize this by caching hash values?
    public class EqualityComparer : IEqualityComparer<object> {
        private readonly RubyContext/*!*/ _context;

        private static readonly CallSite<Func<CallSite, RubyContext, object, object>>/*!*/ _HashSharedSite = CallSite<Func<CallSite, RubyContext, object, object>>.Create(
            RubySites.InstanceCallAction("hash"));

        private static readonly CallSite<Func<CallSite, RubyContext, object, object, bool>>/*!*/ _EqlSharedSite = CallSite<Func<CallSite, RubyContext, object, object, bool>>.Create(
            RubySites.InstanceCallAction("eql?", 1));

        // friend: RubyContext
        internal EqualityComparer(RubyContext/*!*/ context) {
            Assert.NotNull(context);
            _context = context;
        }

        bool IEqualityComparer<object>.Equals(object x, object y) {
            return x == y || _EqlSharedSite.Target(_EqlSharedSite, _context, x, y);
        }

        int IEqualityComparer<object>.GetHashCode(object obj) {
            object result = _HashSharedSite.Target(_HashSharedSite, _context, obj);
            if (result is int) {
                return (int)result;
            }
            return result.GetHashCode();
        }
    }
}

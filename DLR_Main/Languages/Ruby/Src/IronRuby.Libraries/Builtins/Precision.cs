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
using Microsoft.Scripting;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using IronRuby.Runtime;
using Microsoft.Scripting.Generation;
using System.Runtime.CompilerServices;
using Microsoft.Runtime.CompilerServices;


namespace IronRuby.Builtins {

    [RubyModule("Precision")]
    public class Precision {
        #region prec, prec_i, prec_f

        /// <summary>
        /// Converts self into an instance of klass.
        /// </summary>
        /// <remarks>
        /// By default, prec invokes klass.induced_from(self) and returns its value.
        /// So, if <code>klass.induced_from</code> doesn't return an instance of klass, it will be necessary to reimplement prec.
        /// </remarks>
        [RubyMethod("prec")]
        public static object Prec(CallSiteStorage<Func<CallSite, RubyContext, RubyClass, object, object>>/*!*/ inducedFromStorage,
            object self, [NotNull]RubyClass/*!*/ klass) {

            var inducedFrom = inducedFromStorage.GetCallSite("induced_from", 1);
            return inducedFrom.Target(inducedFrom, klass.Context, klass, self);
        }

        /// <summary>
        /// Returns an Integer converted from self. It is equivalent to <code>prec(Integer)</code>.
        /// </summary>
        [RubyMethod("prec_i")]
        public static object PrecInteger(
            CallSiteStorage<Func<CallSite, RubyContext, object, RubyClass, object>>/*!*/ precStorage,
            RubyContext/*!*/ context, object self) {

            var prec = precStorage.GetCallSite("prec", 1);
            return prec.Target(prec, context, self, context.GetClass(typeof(Integer)));
        }

        /// <summary>
        /// Returns a Float converted from self. It is equivalent to <code>prec(Float)</code>.
        /// </summary>
        [RubyMethod("prec_f")]
        public static object PrecFloat(CallSiteStorage<Func<CallSite, RubyContext, object, RubyClass, object>>/*!*/ precStorage, 
            RubyContext/*!*/ context, object self) {

            var prec = precStorage.GetCallSite("prec", 1);
            return prec.Target(prec, context, self, context.GetClass(typeof(double)));
        }

        #endregion

        #region included

        /// <summary>
        /// When the Precision module is mixed-in to a class, via the Module#include method, this included method is called.
        /// Here it is used to add our default induced_from implementation to the host class.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="module">The module being mixed in.</param>
        /// <param name="includedIn">The host class including the module</param>
        [RubyMethod("included", RubyMethodAttributes.PublicSingleton)]
        public static object Included(RubyContext/*!*/ context, RubyModule/*!*/ self, RubyModule/*!*/ includedIn) {
            includedIn.SingletonClass.DefineLibraryMethod("induced_from", (int)RubyMethodAttributes.PublicSingleton, new Func<RubyModule, object, object>(InducedFrom));
            return self;
        }

        private static object InducedFrom(RubyModule/*!*/ rubyClass, object other) {
            throw RubyExceptions.CreateTypeError(String.Format("undefined conversion from {0} into {1}",
                rubyClass.Context.GetClassOf(other).Name, rubyClass.Name));
        }

        #endregion
    }
}

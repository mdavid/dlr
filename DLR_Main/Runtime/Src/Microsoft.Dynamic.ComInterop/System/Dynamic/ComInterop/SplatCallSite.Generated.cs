/* ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation. 
 *
 * This source code is subject to terms and conditions of the Microsoft Public License. A 
 * copy of the license can be found in the License.html file at the root of this distribution. If 
 * you cannot locate the  Microsoft Public License, please send an email to 
 * dlr@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
 * by the terms of the Microsoft Public License.
 *
 * You must not remove this notice, or any other, from this software.
 *
 *
 * ***************************************************************************/
using System; using Microsoft;


using System.Runtime.CompilerServices;
using Microsoft.Runtime.CompilerServices;


namespace Microsoft.Scripting.ComInterop {

    internal sealed partial class SplatCallSite {
        // TODO: is it worth having the generated helpers?

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "args")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [Obsolete("used by generated code", true)]
        public static object CallHelper0(CallSite<Func<CallSite, object>> site, object[] args) {
            return site.Target(site);
        }

        #region Generated SplatCallSite call helpers

        // *** BEGIN GENERATED CODE ***
        // generated by function: gen_splatsite from: generate_dynsites.py

        //
        // Splatting targets for dynamic sites
        //

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [Obsolete("used by generated code", true)]
        public static object CallHelper1(CallSite<Func<CallSite, object, object>> site, object[] args) {
            return site.Target(site, args[0]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [Obsolete("used by generated code", true)]
        public static object CallHelper2(CallSite<Func<CallSite, object, object, object>> site, object[] args) {
            return site.Target(site, args[0], args[1]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [Obsolete("used by generated code", true)]
        public static object CallHelper3(CallSite<Func<CallSite, object, object, object, object>> site, object[] args) {
            return site.Target(site, args[0], args[1], args[2]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [Obsolete("used by generated code", true)]
        public static object CallHelper4(CallSite<Func<CallSite, object, object, object, object, object>> site, object[] args) {
            return site.Target(site, args[0], args[1], args[2], args[3]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [Obsolete("used by generated code", true)]
        public static object CallHelper5(CallSite<Func<CallSite, object, object, object, object, object, object>> site, object[] args) {
            return site.Target(site, args[0], args[1], args[2], args[3], args[4]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [Obsolete("used by generated code", true)]
        public static object CallHelper6(CallSite<Func<CallSite, object, object, object, object, object, object, object>> site, object[] args) {
            return site.Target(site, args[0], args[1], args[2], args[3], args[4], args[5]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [Obsolete("used by generated code", true)]
        public static object CallHelper7(CallSite<Func<CallSite, object, object, object, object, object, object, object, object>> site, object[] args) {
            return site.Target(site, args[0], args[1], args[2], args[3], args[4], args[5], args[6]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [Obsolete("used by generated code", true)]
        public static object CallHelper8(CallSite<Func<CallSite, object, object, object, object, object, object, object, object, object>> site, object[] args) {
            return site.Target(site, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [Obsolete("used by generated code", true)]
        public static object CallHelper9(CallSite<Func<CallSite, object, object, object, object, object, object, object, object, object, object>> site, object[] args) {
            return site.Target(site, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [Obsolete("used by generated code", true)]
        public static object CallHelper10(CallSite<Func<CallSite, object, object, object, object, object, object, object, object, object, object, object>> site, object[] args) {
            return site.Target(site, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [Obsolete("used by generated code", true)]
        public static object CallHelper11(CallSite<Func<CallSite, object, object, object, object, object, object, object, object, object, object, object, object>> site, object[] args) {
            return site.Target(site, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9], args[10]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [Obsolete("used by generated code", true)]
        public static object CallHelper12(CallSite<Func<CallSite, object, object, object, object, object, object, object, object, object, object, object, object, object>> site, object[] args) {
            return site.Target(site, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9], args[10], args[11]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [Obsolete("used by generated code", true)]
        public static object CallHelper13(CallSite<Func<CallSite, object, object, object, object, object, object, object, object, object, object, object, object, object, object>> site, object[] args) {
            return site.Target(site, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9], args[10], args[11], args[12]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [Obsolete("used by generated code", true)]
        public static object CallHelper14(CallSite<Func<CallSite, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>> site, object[] args) {
            return site.Target(site, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9], args[10], args[11], args[12], args[13]);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [Obsolete("used by generated code", true)]
        public static object CallHelper15(CallSite<Func<CallSite, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>> site, object[] args) {
            return site.Target(site, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9], args[10], args[11], args[12], args[13], args[14]);
        }


        // *** END GENERATED CODE ***

        #endregion
    }
}

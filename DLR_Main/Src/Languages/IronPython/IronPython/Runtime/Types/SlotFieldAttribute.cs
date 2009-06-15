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

#if CODEPLEX_40
using System;
#else
using System; using Microsoft;
#endif

namespace IronPython.Runtime.Types {
    /// <summary>
    /// Represents an ops-extension which adds a new slot.  The slot can have arbitrary
    /// get/set behavior above and beyond normal .NET methods or properties.  This is
    /// typically in regards to how it processes access from instances or subtypes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    internal sealed class SlotFieldAttribute : Attribute {
        public SlotFieldAttribute() { }
    }
}

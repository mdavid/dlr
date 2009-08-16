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
using System.Collections.Generic;
#if CODEPLEX_40
using System.Dynamic;
#else
using Microsoft.Scripting;
#endif
using System.Text;

namespace IronPython.Runtime.Binding {
    /// <summary>
    /// An interface that is implemented on DynamicMetaObjects.
    /// 
    /// This allows objects to opt-into custom conversions when calling
    /// COM APIs.  The IronPython binders all call this interface before
    /// doing any COM binding.
    /// </summary>
    interface IComConvertible {
        DynamicMetaObject GetComMetaObject();
    }
}

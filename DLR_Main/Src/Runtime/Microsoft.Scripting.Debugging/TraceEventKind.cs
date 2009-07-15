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
using System.Linq;
#else
using Microsoft.Linq;
#endif
using System.Text;

namespace Microsoft.Scripting.Debugging {
    public enum TraceEventKind {
        // Fired when the execution enters a new frame
        //
        // Payload:
        //   none
        FrameEnter,

        // Fired when the execution leaves a frame
        //
        // Payload:
        //   return value from the function
        FrameExit,

        // Fired when the execution leaves a frame
        //
        // Payload:
        //   none
        ThreadExit,

        // Fired when the execution encounters a trace point
        //
        // Payload:
        //   none
        TracePoint,

        // Fired when an exception is thrown during the execution
        // 
        // Payload:
        //   the exception object that was thrown
        Exception,

        // Fired when an exception is thrown and is not handled by 
        // the current method.
        //
        // Payload:
        //   the exception object that was thrown
        ExceptionUnwind,
    }
}

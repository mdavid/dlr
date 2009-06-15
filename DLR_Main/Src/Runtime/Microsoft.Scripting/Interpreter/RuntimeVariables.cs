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
using System.Runtime.CompilerServices;
#if !CODEPLEX_40
using Microsoft.Runtime.CompilerServices;
#endif


namespace Microsoft.Scripting.Interpreter {
    internal sealed class RuntimeVariables : IRuntimeVariables {
        private readonly IStrongBox[] _boxes;

        private RuntimeVariables(IStrongBox[] boxes) {
            _boxes = boxes;
        }

        int IRuntimeVariables.Count {
            get {
                return _boxes.Length;
            }
        }

        object IRuntimeVariables.this[int index] {
            get {
                return _boxes[index].Value;
            }
            set {
                _boxes[index].Value = value;
            }
        }

        internal static IRuntimeVariables Create(IStrongBox[] boxes) {
            return new RuntimeVariables(boxes);
        }
    }
}

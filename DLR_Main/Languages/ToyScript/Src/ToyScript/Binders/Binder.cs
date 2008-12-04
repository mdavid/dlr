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

using Microsoft.Scripting.Runtime;

namespace ToyScript.Binders {
    public static class Binder {
        public static CallSiteBinder Call() {
            return new CallBinder();
        }

        public static CallSiteBinder Convert(Type to) {
            return new ToyConvertBinder(to);
        }

        public static CallSiteBinder DeleteMember(string name) {
            return new ToyDeleteMemberBinder(name);
        }

        public static CallSiteBinder GetMember(string name) {
            return new ToyGetMemberBinder(name);
        }

        public static CallSiteBinder New() {
            return new NewBinder();
        }

        public static CallSiteBinder Operation(Operators op) {
            // TODO: True operation name mapping
            return new ToyOperationBinder(op.ToString());
        }

        public static CallSiteBinder SetMember(string name) {
            return new ToySetMemberBinder(name);
        }
    }
}

/* ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation. 
 *
 * This source code is subject to terms and conditions of the Apache License, Version 2.0. A 
 * copy of the license can be found in the License.html file at the root of this distribution. If 
 * you cannot locate the  Apache License, Version 2.0, please send an email to 
 * dlr@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
 * by the terms of the Apache License, Version 2.0.
 *
 * You must not remove this notice, or any other, from this software.
 *
 *
 * ***************************************************************************/

using System;
using System.Runtime.CompilerServices;
using Microsoft.Scripting.Utils;

namespace IronPython.Runtime.Operations {
    public static class EnumOps {        
        [SpecialName]
        public static object BitwiseOr(object self, object other) {
            object result = EnumUtils.BitwiseOr(self, other);
            if (result != null) {
                return result;
            }
            throw PythonOps.ValueError("bitwise or cannot be applied to {0} and {1}", self.GetType(), other.GetType());
        }

        [SpecialName]
        public static object BitwiseAnd(object self, object other) {
            object result = EnumUtils.BitwiseAnd(self, other);
            if (result != null) {
                return result;
            }

            throw PythonOps.ValueError("bitwise and cannot be applied to {0} and {1}", self.GetType(), other.GetType());
        }

        [SpecialName]
        public static object ExclusiveOr(object self, object other) {
            object result = EnumUtils.ExclusiveOr(self, other);
            if (result != null) {
                return result;
            }

            throw PythonOps.ValueError("bitwise xor cannot be applied to {0} and {1}", self.GetType(), other.GetType());
        }

        [SpecialName]
        public static object OnesComplement(object self) {
            object result = EnumUtils.OnesComplement(self);
            if (result != null) {
                return result;
            }

            throw PythonOps.ValueError("one's complement cannot be applied to {0}", self.GetType());
        }

        public static bool __nonzero__(object self) {
            if (self is Enum) {
                Type selfType = self.GetType();
                Type underType = Enum.GetUnderlyingType(selfType);

                switch(Type.GetTypeCode(underType)) {
                    case TypeCode.Int16: return (short)self != 0;
                    case TypeCode.Int32: return (int)self != 0;
                    case TypeCode.Int64: return (long)self != 0;
                    case TypeCode.UInt16: return (ushort)self != 0;
                    case TypeCode.UInt32: return (uint)self != 0;
                    case TypeCode.UInt64: return ~(ulong)self != 0;
                    case TypeCode.Byte: return (byte)self != 0;
                    case TypeCode.SByte: return (sbyte)self != 0;
                }
            }

            throw PythonOps.ValueError("__nonzero__ cannot be applied to {0}", self.GetType());
        }

        public static string __repr__(object self) {
            if (Enum.IsDefined(self.GetType(), self)) {
                string name = Enum.GetName(self.GetType(), self);
                return self.GetType().FullName + "." + name;
            }

            return String.Format("<enum {0}: {1}>", self.GetType().FullName, self.ToString());
        }
    }
}

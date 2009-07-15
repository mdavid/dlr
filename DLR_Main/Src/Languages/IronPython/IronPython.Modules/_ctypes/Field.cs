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
using System.Diagnostics;

using Microsoft.Scripting.Math;
using Microsoft.Scripting.Runtime;

using IronPython.Runtime;
using IronPython.Runtime.Operations;
using IronPython.Runtime.Types;

#if !SILVERLIGHT

namespace IronPython.Modules {
    /// <summary>
    /// Provides support for interop with native code from Python code.
    /// </summary>
    public static partial class CTypes {
        /// <summary>
        /// Fields are created when a Structure is defined and provide
        /// introspection of the structure.
        /// </summary>
        [PythonType, PythonHidden]
        public sealed class Field : PythonTypeDataSlot, ICodeFormattable {
            private readonly INativeType _fieldType;
            private readonly int _offset, _index, _bits = -1, _bitsOffset;
            private readonly string _fieldName;

            internal Field(string fieldName, INativeType fieldType, int offset, int index) {
                _offset = offset;
                _fieldType = fieldType;
                _index = index;
                _fieldName = fieldName;
            }

            internal Field(string fieldName, INativeType fieldType, int offset, int index, int? bits, int? bitOffset) {
                _offset = offset;
                _fieldType = fieldType;
                _index = index;
                _fieldName = fieldName;

                if (bits != null) {
                    _bits = bits.Value;
                    _bitsOffset = bitOffset.Value;
                }
            }

            public int offset {
                get {
                    return _offset;
                }
            }

            public int size {
                get {
                    return _fieldType.Size;
                }
            }

            #region Internal APIs

            internal override bool TryGetValue(CodeContext context, object instance, PythonType owner, out object value) {
                if (instance != null) {
                    CData inst = (CData)instance;
                    value = _fieldType.GetValue(inst._memHolder, _offset, false);
                    if (_bits == -1) {
                        return true;
                    }

                    value = ExtractBits(value);
                    return true;
                }

                value = this;
                return true;
            }


            internal override bool GetAlwaysSucceeds {
                get {
                    return true;
                }
            }

            internal override bool TrySetValue(CodeContext context, object instance, PythonType owner, object value) {
                if (instance != null) {
                    SetValue(((CData)instance)._memHolder, 0, value);
                    return true;

                }
                return base.TrySetValue(context, instance, owner, value);
            }

            internal void SetValue(MemoryHolder address, int baseOffset, object value) {
                if (_bits == -1) {
                    object keepAlive = _fieldType.SetValue(address, baseOffset + _offset, value);
                    if (keepAlive != null) {
                        address.AddObject(_index.ToString(), keepAlive);
                    }
                } else {
                    SetBitsValue(address, baseOffset, value);
                }
            }

            internal override bool TryDeleteValue(CodeContext context, object instance, PythonType owner) {
                throw PythonOps.TypeError("cannot delete fields in ctypes structures/unions");
            }

            internal INativeType NativeType {
                get {
                    return _fieldType;
                }
            }

            internal int? BitCount {
                get {
                    if (_bits == -1) {
                        return null;
                    }

                    return _bits;
                }
            }

            internal string FieldName {
                get {
                    return _fieldName;
                }
            }

            #endregion

            #region ICodeFormattable Members

            public string __repr__(CodeContext context) {
                if (_bits == -1) {
                    return String.Format("<Field type={0}, ofs={1}, size={2}>", ((PythonType)_fieldType).Name, offset, size);
                }
                return String.Format("<Field type={0}, ofs={1}:{2}, bits={3}>", ((PythonType)_fieldType).Name, offset, _bitsOffset, _bits);
            }

            #endregion

            #region Private implementation

            /// <summary>
            /// Called for fields which have been limited to a range of bits.  Given the
            /// value for the full type this extracts the individual bits.
            /// </summary>
            private object ExtractBits(object value) {
                if (value is int) {
                    int validBits = ((1 << _bits) - 1);

                    int iVal = (int)value;
                    iVal = (iVal >> _bitsOffset) & validBits;
                    if (IsSignedType) {
                        // need to sign extend if high bit is set
                        if ((iVal & (1 << (_bits - 1))) != 0) {
                            iVal |= (-1) ^ validBits;
                        }
                    }
                    value = ScriptingRuntimeHelpers.Int32ToObject(iVal);
                } else {
                    Debug.Assert(value is BigInteger); // we only return ints or big ints from GetValue
                    ulong validBits = (1UL << _bits) - 1;

                    BigInteger biVal = (BigInteger)value;
                    ulong bits;
                    if (IsSignedType) {
                        bits = (ulong)biVal.ToInt64();
                    } else {
                        bits = biVal.ToUInt64();
                    }

                    bits = (bits >> _bitsOffset) & validBits;

                    if (IsSignedType) {
                        // need to sign extend if high bit is set
                        if ((bits & (1UL << (_bits - 1))) != 0) {
                            bits |= ulong.MaxValue ^ validBits;
                        }
                        value = BigInteger.Create((long)bits);
                    } else {
                        value = BigInteger.Create(bits);
                    }
                }
                return value;
            }

            /// <summary>
            /// Called for fields which have been limited to a range of bits.  Sets the 
            /// specified value into the bits for the field.
            /// </summary>
            private void SetBitsValue(MemoryHolder address, int baseOffset, object value) {
                // get the value in the form of a ulong which can contain the biggest bitfield
                ulong newBits;
                if (value is int) {
                    newBits = (ulong)(int)value;
                } else if (value is BigInteger) {
                    newBits = (ulong)((BigInteger)value).ToInt64();
                } else {
                    throw PythonOps.TypeErrorForTypeMismatch("int or long", value);
                }

                // do the same for the existing value
                int offset = checked(_offset + baseOffset);
                object curValue = _fieldType.GetValue(address, offset, false);
                ulong valueBits;
                if (curValue is int) {
                    valueBits = (ulong)(int)curValue;
                } else {
                    valueBits = (ulong)((BigInteger)curValue).ToInt64();
                }

                // get a mask for the bits this field owns
                ulong targetBits = ((1UL << _bits) - 1) << _bitsOffset;
                // clear the existing bits
                valueBits &= ~targetBits;
                // or in the new bits provided by the user
                valueBits |= (newBits << _bitsOffset) & targetBits;

                // and set the value                    
                if (IsSignedType) {
                    if (_fieldType.Size <= 4) {
                        _fieldType.SetValue(address, offset, (int)(long)valueBits);
                    } else {
                        _fieldType.SetValue(address, offset, BigInteger.Create((long)valueBits));
                    }
                } else {
                    if (_fieldType.Size < 4) {
                        _fieldType.SetValue(address, offset, (int)valueBits);
                    } else {
                        _fieldType.SetValue(address, offset, BigInteger.Create(valueBits));
                    }
                }
            }

            private bool IsSignedType {
                get {
                    switch (((SimpleType)_fieldType)._type) {
                        case SimpleTypeKind.SignedByte:
                        case SimpleTypeKind.SignedInt:
                        case SimpleTypeKind.SignedLong:
                        case SimpleTypeKind.SignedLongLong:
                        case SimpleTypeKind.SignedShort:
                            return true;
                    }
                    return false;
                }
            }

            #endregion
        }
    }
}

#endif

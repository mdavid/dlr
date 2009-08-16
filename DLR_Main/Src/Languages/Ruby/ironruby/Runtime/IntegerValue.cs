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

#if CODEPLEX_40
using System;
#else
using System; using Microsoft;
#endif
using System.Collections.Generic;
using System.Text;
using IronRuby.Compiler.Generation;
using Microsoft.Scripting.Math;
using IronRuby.Builtins;
using System.Diagnostics;
using Microsoft.Scripting.Runtime;

namespace IronRuby.Runtime {
    public struct IntegerValue : IEquatable<IntegerValue> {
        private int _fixnum;
        private BigInteger _bignum;

        public override bool Equals(object obj) {
            if (obj == null) {
                return false;
            }

            if (obj is IntegerValue) {
                return Equals((IntegerValue)obj);
            } else if (obj is int) {
                return Equals(new IntegerValue((int)obj));
            } else if (obj is BigInteger) {
                return Equals(new IntegerValue(obj as BigInteger));
            }

            return false;
        }

        public override int GetHashCode() {
            if (IsFixnum) {
                return _fixnum.GetHashCode();
            } else {
                return _bignum.GetHashCode();
            }
        }

        public int Fixnum { 
            get {
                Debug.Assert(IsFixnum);
                return _fixnum;
            } 
        }

        public BigInteger Bignum { 
            get {
                Debug.Assert(!IsFixnum);
                return _bignum;
            }
        }

        public bool IsFixnum { 
            get { return ReferenceEquals(_bignum, null); } 
        }

        public IntegerValue(int value) {
            _fixnum = value;
            _bignum = null;
        }

        public IntegerValue(BigInteger/*!*/ value) {
            _fixnum = 0;
            _bignum = value;
        }

        public static implicit operator IntegerValue(int value) {
            return new IntegerValue(value);
        }

        public static implicit operator IntegerValue(BigInteger value) {
            return new IntegerValue(value);
        }

        public object/*!*/ ToObject() {
            return (object)_bignum ?? ScriptingRuntimeHelpers.Int32ToObject(_fixnum);
        }

        public long ToInt64() {
            long result;
            if (IsFixnum) {
                result = _fixnum;
            } else if (!_bignum.AsInt64(out result)) {
                throw RubyExceptions.CreateRangeError("Bignum too big to convert into 64-bit signed integer");
            }
            return result;
        }

        public bool Equals(IntegerValue other) {
            if (_fixnum != other.Fixnum) return false;
            if (ReferenceEquals(_bignum, null)) return ReferenceEquals(other._bignum, null);
            return _bignum.Equals(other._bignum);
        }
    }
}

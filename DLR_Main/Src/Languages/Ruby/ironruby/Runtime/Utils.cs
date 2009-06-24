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
using Microsoft.Scripting.Utils;
using System.Diagnostics;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using IronRuby.Builtins;

namespace IronRuby.Runtime {
    public static class Utils {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2105:ArrayFieldsShouldNotBeReadOnly")]
        public static readonly byte[] EmptyBytes = new byte[0];

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2105:ArrayFieldsShouldNotBeReadOnly")]
        public static readonly char[] EmptyChars = new char[0];
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2105:ArrayFieldsShouldNotBeReadOnly")]
        public static readonly MemberInfo[] EmptyMemberInfos = new MemberInfo[0];

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2105:ArrayFieldsShouldNotBeReadOnly")]
        public static readonly Delegate[] EmptyDelegates = new Delegate[0];

#if !SILVERLIGHT
        internal static Type ComObjectType = typeof(object).Assembly.GetType("System.__ComObject");
#endif

        public static bool IsComObjectType(Type/*!*/ type) {
#if SILVERLIGHT
            return false;
#else
            return ComObjectType.IsAssignableFrom(type);
#endif
        }

        public static bool IsComObject(object obj) {
#if SILVERLIGHT
            return false;
#else
            return obj != null && IsComObjectType(obj.GetType());
#endif
        }

        public static int IndexOf(this string[]/*!*/ array, string/*!*/ value, StringComparer/*!*/ comparer) {
            ContractUtils.RequiresNotNull(array, "array");
            ContractUtils.RequiresNotNull(value, "value");
            ContractUtils.RequiresNotNull(comparer, "comparer");

            for (int i = 0; i < array.Length; i++) {
                if (comparer.Equals(array[i], value)) {
                    return i;
                }
            }

            return -1;
        }

        internal static bool IsAscii(this string/*!*/ str) {
            for (int i = 0; i < str.Length; i++) {
                if (str[i] > 0x7f) {
                    return false;
                }
            }
            return true;
        }

        public static int LastCharacter(this string/*!*/ str) {
            return str.Length == 0 ? -1 : str[str.Length - 1];
        }

        public static int IndexOf(this StringBuilder/*!*/ sb, char value) {
            ContractUtils.RequiresNotNull(sb, "sb");

            for (int i = 0; i < sb.Length; i++) {
                if (sb[i] == value) {
                    return i;
                }
            }

            return -1;
        }

        internal const int MinBufferSize = 16;

        private static int GetExpandedSize(int currentLength, int minLength) {
            return Math.Max(minLength, 1 + (currentLength << 1));
        }

        internal static void Resize<T>(ref T[]/*!*/ array, int minLength) {
            if (array.Length < minLength) {
                Array.Resize(ref array, GetExpandedSize(array.Length, minLength));
            }
        }

        internal static void TrimExcess<T>(ref T[] data, int count) {
            if ((long)count * 10 < (long)data.Length * 9) {
                Array.Resize(ref data, count);
            }
        }

        internal static void ResizeForInsertion<T>(ref T[]/*!*/ array, int itemCount, int index, int count) {
            int minLength = itemCount + count;
            T[] a;
            if (array.Length < minLength) {
                a = new T[GetExpandedSize(array.Length, minLength)];
                Array.Copy(array, 0, a, 0, index);
            } else {
                a = array;
            }

            Array.Copy(array, index, a, index + count, itemCount - index);
            array = a;
        }

        private static void Fill<T>(T[]/*!*/ array, int index, T item, int repeatCount) {
            for (int i = index; i < index + repeatCount; i++) {
                array[i] = item;
            }
        }

        private static void Copy(string/*!*/ src, int srcOffset, char[]/*!*/ dst, int dstOffset, int count) {
            for (int i = 0; i < count; i++) {
                dst[dstOffset + i] = src[srcOffset + i];
            }
        }

        internal static int Append<T>(ref T[]/*!*/ array, int itemCount, T item, int repeatCount) {
            Resize(ref array, itemCount + repeatCount);
            Fill(array, itemCount, item, repeatCount);
            return itemCount + repeatCount;
        }

        internal static int Append(ref char[]/*!*/ array, int itemCount, string/*!*/ other, int start, int count) {
            int newCount = itemCount + count;
            Resize(ref array, newCount);
            Copy(other, start, array, itemCount, count);
            return newCount;
        }

        internal static int Append<T>(ref T[]/*!*/ array, int itemCount, T[]/*!*/ other, int start, int count) {
            int newCount = itemCount + count;
            Resize(ref array, newCount);
            Array.Copy(other, start, array, itemCount, count);
            return newCount;
        }

        internal static int InsertAt<T>(ref T[]/*!*/ array, int itemCount, int index, T item, int repeatCount) {
            ResizeForInsertion(ref array, itemCount, index, repeatCount);
            Fill(array, index, item, repeatCount);
            return itemCount + repeatCount;
        }

        internal static int InsertAt(ref char[]/*!*/ array, int itemCount, int index, string/*!*/ other, int start, int count) {
            ResizeForInsertion(ref array, itemCount, index, count);
            Copy(other, start, array, index, count);
            return itemCount + count;
        }

        internal static int InsertAt<T>(ref T[]/*!*/ array, int itemCount, int index, T[]/*!*/ other, int start, int count) {
            ResizeForInsertion(ref array, itemCount, index, count);
            Array.Copy(other, start, array, index, count);
            return itemCount + count;
        }

        internal static int Remove<T>(ref T[]/*!*/ array, int itemCount, int start, int count) {
            T[] a;
            int remaining = itemCount - count;
            if (remaining > MinBufferSize && remaining < itemCount / 2) {
                a = new T[remaining];
                Array.Copy(array, 0, a, 0, start);
            } else {
                a = array;
            }

            Array.Copy(array, start + count, a, start, remaining - start);
            array = a;
            return remaining;
        }

        internal static T[]/*!*/ GetSlice<T>(this T[]/*!*/ array, int start, int count) {
            var copy = new T[count];
            Array.Copy(array, start, copy, 0, count);
            return copy;
        }

        internal static int IndexOf(byte[]/*!*/ array, byte[]/*!*/ bytes, int start, int count) {
            // TODO:
            for (int i = start; i < start + count - bytes.Length + 1; i++) {
                bool match = true;
                for (int j = 0; j < bytes.Length; j++) {
                    if (bytes[j] != array[i + j]) {
                        match = false;
                        break;
                    }
                }

                if (match) {
                    return i;
                }
            }
            return -1;
        }

        internal static int LastIndexOf(byte[]/*!*/ array, byte[]/*!*/ bytes, int start, int count) {
            // TODO:
            int finish = start - count < 0 ? bytes.Length - 1 : start - count + bytes.Length;
            //for (int i = start; i < start + count - bytes.Length + 1; i++) {
            for (int i = start; i >= finish; --i) {
                bool match = true;
                for (int j = 0; j < bytes.Length; j++) {
                    if (bytes[j] != array[i + j]) {
                        match = false;
                        break;
                    }
                }

                if (match) {
                    return i;
                }
            }
            return -1;
        }

        internal const int ReservedHashCode = Int32.MaxValue;

        // never returns ReservedHashCode
        internal static int GetValueHashCode(this string/*!*/ str, out int binarySum) {
            int result = 5381;
            int sum = 0;
            for (int i = 0; i < str.Length; i++) {
                int c = str[i];
                result = unchecked(((result << 5) + result) ^ c);
                sum |= c;
            }
            binarySum = sum;
            return result == ReservedHashCode ? 1 : result;
        }

        // never returns ReservedHashCode
        internal static int GetValueHashCode(this char[]/*!*/ array, int itemCount, out int binarySum) {
            int result = 5381;
            int sum = 0;
            for (int i = 0; i < itemCount; i++) {
                int c = array[i];
                result = unchecked(((result << 5) + result) ^ c);
                sum |= c;
            }
            binarySum = sum;
            return result == ReservedHashCode ? 1 : result;
        }

        // never returns ReservedHashCode
        internal static int GetValueHashCode(this byte[]/*!*/ array, int itemCount, out int binarySum) {
            int result = 5381;
            int sum = 0;
            for (int i = 0; i < itemCount; i++) {
                int c = array[i];
                result = unchecked(((result << 5) + result) ^ c);
                sum |= c;
            }
            binarySum = sum;
            return result == ReservedHashCode ? 1 : result;
        }

        internal static int ValueCompareTo(this byte[]/*!*/ array, int itemCount, byte[]/*!*/ other) {
            int min = itemCount, defaultResult;
            if (min < other.Length) {
                defaultResult = -1;
            } else if (min > other.Length) {
                min = other.Length;
                defaultResult = +1;
            } else {
                defaultResult = 0;
            }

            for (int i = 0; i < min; i++) {
                if (array[i] != other[i]) {
                    return (int)array[i] - other[i];
                }
            }

            return defaultResult;
        }

        internal static int ValueCompareTo(this char[]/*!*/ array, int itemCount, string/*!*/ other) {
            int min = itemCount, defaultResult;
            if (min < other.Length) {
                defaultResult = -1;
            } else if (min > other.Length) {
                min = other.Length;
                defaultResult = +1;
            } else {
                defaultResult = 0;
            }

            for (int i = 0; i < min; i++) {
                if (array[i] != other[i]) {
                    return (int)array[i] - other[i];
                }
            }

            return defaultResult;
        }

        internal static int ValueCompareTo(this string/*!*/ str, string/*!*/ other) {
            int min = str.Length, defaultResult;
            if (min < other.Length) {
                defaultResult = -1;
            } else if (min > other.Length) {
                min = other.Length;
                defaultResult = +1;
            } else {
                defaultResult = 0;
            }

            for (int i = 0; i < min; i++) {
                if (str[i] != other[i]) {
                    return (int)str[i] - other[i];
                }
            }

            return defaultResult;
        }

        public static TOutput[]/*!*/ ConvertAll<TInput, TOutput>(this TInput[]/*!*/ array, Converter<TInput, TOutput>/*!*/ converter) {
            var result = new TOutput[array.Length];
            for (int i = 0; i < array.Length; i++) {
                result[i] = converter(array[i]);
            }
            return result;
        }



        internal static void AddRange(this IList/*!*/ collection, IEnumerable<object>/*!*/ range) {
            Assert.NotNull(collection, range);

            List<object> objList;
            RubyArray array;
            if ((array = collection as RubyArray) != null) {
                array.AddRange(range);
            } else if ((objList = collection as List<object>) != null) {
                objList.AddRange(range);
            } else {
                foreach (var item in range) {
                    collection.Add(item);
                }
            }
        }

        internal static void AddRange(this IList/*!*/ list, IEnumerable/*!*/ range) {
            Assert.NotNull(list, range);

            List<object> objList;
            RubyArray array;
            if ((array = list as RubyArray) != null) {
                array.AddRange(range);
            } else if ((objList = list as List<object>) != null) {
                objList.AddRange(range);
            } else {
                foreach (var item in range) {
                    list.Add(item);
                }
            }
        }

        [Conditional("DEBUG")]
        public static void Log(string/*!*/ message, string/*!*/ category) {
#if !SILVERLIGHT
            Debug.WriteLine((object)message, category);
#endif
        }

        public static long DateTimeTicksFromStopwatch(long elapsedStopwatchTicks) {
#if !SILVERLIGHT
            if (Stopwatch.IsHighResolution) {
                return (long)(((double)elapsedStopwatchTicks) * 10000000.0 / (double)Stopwatch.Frequency);
            }
#endif
            return elapsedStopwatchTicks;
        }

        public static char ToLowerHexDigit(this int digit) {
            return (char)((digit < 10) ? '0' + digit : 'a' + digit - 10);
        }

        public static char ToUpperHexDigit(this int digit) {
            return (char)((digit < 10) ? '0' + digit : 'A' + digit - 10);
        }
    }
}

#if SILVERLIGHT
namespace System.Diagnostics {
    internal struct Stopwatch {
        public void Start() {
        }

        public void Stop() {
        }

        public static long GetTimestamp() {
            return 0;
        }
    }
}
#endif

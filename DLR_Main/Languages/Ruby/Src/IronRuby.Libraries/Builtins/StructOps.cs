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

using System; using Microsoft;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Scripting;
using Microsoft.Linq.Expressions;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using Microsoft.Scripting.Actions;
using IronRuby.Runtime;
using IronRuby.Runtime.Calls;
using System.Runtime.InteropServices;
using Ast = Microsoft.Linq.Expressions.Expression;
using EachSite = Microsoft.Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, object, IronRuby.Builtins.Proc, object>;

namespace IronRuby.Builtins {

    [RubyClass("Struct", Extends = typeof(RubyStruct)), Includes(typeof(Enumerable))]
    public static partial class RubyStructOps {
        [RubyConstructor]
        public static void AllocatorUndefined(RubyClass/*!*/ self, params object[] args) {
            throw RubyExceptions.CreateAllocatorUndefinedError(self);
        }

        [RubyMethod("new", RubyMethodAttributes.PublicSingleton)]
        public static object NewAnonymousStruct(BlockParam block, RubyClass/*!*/ self, int className,
            [NotNull]params object[]/*!*/ attributeNames) {

            return CreateAnonymousWithFirstAttribute(block, self, RubyOps.ConvertFixnumToSymbol(self.Context, className), attributeNames);
        }

        [RubyMethod("new", RubyMethodAttributes.PublicSingleton)]
        public static object NewAnonymousStruct(BlockParam block, RubyClass/*!*/ self, SymbolId className,
            [NotNull]params object[]/*!*/ attributeNames) {

            return CreateAnonymousWithFirstAttribute(block, self, RubyOps.ConvertSymbolIdToSymbol(className), attributeNames);
        }

        [RubyMethod("new", RubyMethodAttributes.PublicSingleton)]
        public static object NewAnonymousStruct(BlockParam block, RubyClass/*!*/ self, [NotNull]string/*!*/ className,
            [NotNull]params object[]/*!*/ attributeNames) {

            return CreateAnonymousWithFirstAttribute(block, self, className, attributeNames);
        }

        [RubyMethod("new", RubyMethodAttributes.PublicSingleton)]
        public static object NewStruct(BlockParam block, RubyClass/*!*/ self, [DefaultProtocol, Optional]MutableString className,
            [NotNull]params object[]/*!*/ attributeNames) {

            string[] symbols = Protocols.CastToSymbols(self.Context, attributeNames);

            if (className == null) {
                return Create(block, self, null, symbols);
            }

            string strName = className.ConvertToString();
            RubyUtils.CheckConstantName(strName);
            return Create(block, self, strName, symbols);
        }

        public static object CreateAnonymousWithFirstAttribute(BlockParam block, RubyClass/*!*/ self,
            string/*!*/ firstAttribute, object[]/*!*/ attributeNames) {

            return Create(block, self, null, ArrayUtils.Insert(firstAttribute, Protocols.CastToSymbols(self.Context, attributeNames)));
        }

        /// <summary>
        /// Struct#new
        /// Creates Struct classes with the specified name and members
        /// </summary>
        private static object Create(BlockParam block, RubyClass/*!*/ self, string className, string/*!*/[]/*!*/ attributeNames) {
            var result = RubyStruct.DefineStruct(self, className, attributeNames);

            if (block != null) {
                return RubyUtils.EvaluateInModule(result, block, result);
            }

            return result;
        }

        // Reinitialization. Called only from derived struct's initializer.
        [RubyMethod("initialize", RubyMethodAttributes.PrivateInstance)]
        public static void Reinitialize(RubyStruct/*!*/ self, [NotNull]params object[]/*!*/ items) {
            self.SetValues(items);
        }

        // Copies data from one Struct instance into another:
        [RubyMethod("initialize_copy", RubyMethodAttributes.PrivateInstance)]
        public static RubyStruct/*!*/ InitializeCopy(RubyStruct/*!*/ self, [NotNull]RubyStruct/*!*/ source) {
            if (self.Class != source.Class) {
                throw RubyExceptions.CreateTypeError("wrong argument class");
            }

            self.SetValues(source.Values);
            return self;
        }

        [RubyMethod("members")]
        public static RubyArray/*!*/ GetMembers(RubyStruct/*!*/ self) {
            return RubyStruct.GetMembers(self);
        }

        [RubyMethod("length")]
        [RubyMethod("size")]
        public static int GetSize(RubyStruct/*!*/ self) {
            return self.ItemCount;
        }

        [RubyMethod("[]")]
        public static object GetValue(RubyStruct/*!*/ self, int index) {
            return self[NormalizeIndex(self.ItemCount, index)];
        }

        [RubyMethod("[]")]
        public static object GetValue(RubyStruct/*!*/ self, SymbolId name) {
            return self[SymbolTable.IdToString(name)];
        }

        [RubyMethod("[]")]
        public static object GetValue(RubyStruct/*!*/ self, MutableString/*!*/ name) {
            return self[name.ConvertToString()];
        }

        [RubyMethod("[]")]
        public static object GetValue(ConversionStorage<int>/*!*/ conversionStorage, RubyStruct/*!*/ self, object index) {
            return self[NormalizeIndex(self.ItemCount, Protocols.CastToFixnum(conversionStorage, self.Class.Context, index))];
        }

        [RubyMethod("[]=")]
        public static object SetValue(RubyStruct/*!*/ self, int index, object value) {
            return self[NormalizeIndex(self.ItemCount, index)] = value;
        }

        [RubyMethod("[]=")]
        public static object SetValue(RubyStruct/*!*/ self, SymbolId name, object value) {
            return self[SymbolTable.IdToString(name)] = value;
        }

        [RubyMethod("[]=")]
        public static object SetValue(RubyStruct/*!*/ self, MutableString/*!*/ name, object value) {
            return self[name.ConvertToString()] = value;
        }

        [RubyMethod("[]=")]
        public static object SetValue(ConversionStorage<int>/*!*/ conversionStorage, RubyStruct/*!*/ self, object index, object value) {
            return self[NormalizeIndex(self.ItemCount, Protocols.CastToFixnum(conversionStorage, self.Class.Context, index))] = value;
        }

        [RubyMethod("each")]
        public static object Each(BlockParam block, RubyStruct/*!*/ self) {
            if (block == null && self.ItemCount > 0) {
                throw RubyExceptions.NoBlockGiven();
            }

            foreach (var value in self.Values) {
                object result;
                if (block.Yield(value, out result)) {
                    return result;
                }
            }

            return self;
        }

        [RubyMethod("each_pair")]
        public static object EachPair(BlockParam block, RubyStruct/*!*/ self) {
            if (block == null && self.ItemCount > 0) {
                throw RubyExceptions.NoBlockGiven();
            }

            foreach (KeyValuePair<string, object> entry in self.GetItems()) {
                object result;
                if (block.Yield(SymbolTable.StringToId(entry.Key), entry.Value, out result)) {
                    return result;
                }
            }

            return self;
        }

        [RubyMethod("to_a")]
        [RubyMethod("values")]
        public static RubyArray/*!*/ Values(RubyStruct/*!*/ self) {
            return new RubyArray(self.Values);
        }

        [RubyMethod("hash")]
        public static int Hash(RubyStruct/*!*/ self) {
            return self.GetHashCode();
        }

        [RubyMethod("eql?")]
        public static bool Equal(RubyStruct/*!*/ self, object other) {
            return self.Equals(other);
        }

        // same pattern as RubyStruct.Equals, but we need to call == instead of eql?
        [RubyMethod("==")]
        public static bool Equals(BinaryOpStorage/*!*/ equals, RubyStruct/*!*/ self, object obj) {
            var other = obj as RubyStruct;
            if (!self.StructReferenceEquals(other)) {
                return false;
            }
            Debug.Assert(self.ItemCount == other.ItemCount);

            if (self.Values.Length > 0) {
                var site = equals.GetCallSite("==");
                for (int i = 0; i < self.Values.Length; i++) {
                    if (RubyOps.IsFalse(site.Target(site, self.Class.Context, self.Values[i], other.Values[i]))) {
                        return false;
                    }
                }
            }

            return true;
        }

        [RubyMethod("to_s")]
        [RubyMethod("inspect")]
        public static MutableString/*!*/ Inspect(RubyStruct/*!*/ self) {
            RubyContext context = self.Class.Context;

            using (IDisposable handle = RubyUtils.InfiniteInspectTracker.TrackObject(self)) {
                // #<struct Struct::Foo name=nil, val=nil>
                MutableString str = MutableString.Create("#<struct ");
                str.Append(RubySites.Inspect(context, context.GetClassOf(self)));

                if (handle == null) {
                    return str.Append(":...>");
                }
                str.Append(' ');

                object[] data = self.Values;
                var members = self.GetNames();
                for (int i = 0; i < data.Length; i++) {
                    if (i != 0) {
                        str.Append(", ");
                    }
                    str.Append(members[i]);
                    str.Append("=");
                    str.Append(RubySites.Inspect(context, data[i]));
                }
                str.Append('>');
                return str;
            }
        }

        // For some unknown reason Struct defines the method even though it is mixed in from Enumerable
        // Until we discover the difference, delegate to Enumerable#select
        [RubyMethod("select")]
        public static RubyArray/*!*/ Select(CallSiteStorage<EachSite>/*!*/ each, BlockParam predicate, RubyStruct/*!*/ self) {
            return Enumerable.Select(each, self.Class.Context, predicate, self);
        }

        // equivalent to Array#values_at over the data array
        [RubyMethod("values_at")]
        public static RubyArray/*!*/ ValuesAt(ConversionStorage<int>/*!*/ fixnumCast, RubyStruct/*!*/ self, [NotNull]params object[] values) {
            RubyArray result = new RubyArray();
            RubyContext context = self.Class.Context;
            object[] data = self.Values;

            for (int i = 0; i < values.Length; ++i) {
                Range range = values[i] as Range;
                if (range != null) {
                    int begin = Protocols.CastToFixnum(fixnumCast, context, range.Begin);
                    int end = Protocols.CastToFixnum(fixnumCast, context, range.End);

                    if (range.ExcludeEnd) {
                        end -= 1;
                    }

                    begin = NormalizeIndex(data.Length, begin);
                    end = NormalizeIndex(data.Length, end);
                    Debug.Assert(end - begin <= data.Length); // because we normalized the indicies

                    if (end - begin > 0) {
                        result.Capacity += (end - begin);
                        for (int j = begin; j <= end; j++) {
                            result.Add(data[j]);
                        }
                    }
                } else {
                    int index = NormalizeIndex(data.Length, Protocols.CastToFixnum(fixnumCast, context, values[i]));
                    result.Add(data[index]);
                }
            }

            return result;
        }

        private static int NormalizeIndex(int itemCount, int index) {
            int normalized = index;
            if (normalized < 0) {
                normalized += itemCount;
            }
            if (normalized >= 0 && normalized < itemCount) {
                return normalized;
            }
            // MRI reports the normalized index, but we'll report the original one
            throw RubyExceptions.CreateIndexError(String.Format("offset {0} too small for struct (size:{1})", index, itemCount));
        }
    }
}

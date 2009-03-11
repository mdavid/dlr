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
using System.Runtime.CompilerServices;
using Microsoft.Runtime.CompilerServices;

using IronRuby.Builtins;
using IronRuby.Runtime;
using IronRuby.Runtime.Calls;
using Microsoft.Scripting;
using Microsoft.Scripting.Runtime;
using System.Runtime.InteropServices;

using EnumerableModule = IronRuby.Builtins.Enumerable;
using EachSite = Microsoft.Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, object, IronRuby.Builtins.Proc, object>;
using Microsoft.Scripting.Utils;

namespace IronRuby.StandardLibrary.Enumerator {

    [RubyModule(Extends = typeof(Kernel))]
    public static class EnumerableKernelOps {
        [RubyMethod("to_enum")]
        [RubyMethod("enum_for")]
        public static Enumerable.Enumerator/*!*/ Create(object self, [DefaultProtocol, Optional]string enumeratorName, 
            [NotNull]params object[] targetParameters) {
            return new Enumerable.Enumerator(self, enumeratorName, targetParameters);
        }
    }

    [RubyModule(Extends = typeof(EnumerableModule))]
    public static class Enumerable {

        #region Enumerator class

        /// <summary>
        /// A wrapper that provides "each" method for an arbitrary object. 
        /// A call to "each" on the instance of Enumerator is forwarded to a call to a method of a given name on the wrapped object.
        /// </summary>
        [RubyClass("Enumerator"), Includes(typeof(EnumerableModule))]
        public class Enumerator {
            private object _targetObject;
            private string/*!*/ _targetName;
            private object[] _targetArguments;

            public Enumerator()
                : this(null, null, null) {
            }

            public Enumerator(object targetObject, string targetName, params object[] targetArguments) {
                Reinitialize(this, targetObject, targetName, targetArguments);
            }

            [RubyConstructor]
            public static Enumerator/*!*/ Create(RubyClass/*!*/ self, object targetObject, [DefaultProtocol, Optional]string targetName, 
                [NotNull]params object[] targetArguments) {

                return Reinitialize(new Enumerator(), targetObject, targetName, targetArguments);
            }

            [RubyMethod("initialize", RubyMethodAttributes.PrivateInstance)]
            public static Enumerator/*!*/ Reinitialize(Enumerator/*!*/ self, object targetObject, [DefaultProtocol, Optional]string targetName, 
                [NotNull]params object[] targetArguments) {

                self._targetObject = targetObject;
                self._targetName = targetName ?? "each";
                self._targetArguments = targetArguments;
                return self;
            }

            [RubyMethod("each")]
            public static object Each(RubyScope/*!*/ scope, BlockParam/*!*/ block, Enumerator/*!*/ self) {
                // MRI doesn't send "send" message: 
                return KernelOps.SendMessageOpt(scope, block, self._targetObject, self._targetName, self._targetArguments);
            }

            // TODO: 1.9:
            // with_index
            // next
            // rewind
        }

        #endregion

        #region each_cons, each_slice

        [RubyMethod("each_cons")]
        public static object EachCons(CallSiteStorage<EachSite>/*!*/ each, RubyContext/*!*/ context, BlockParam/*!*/ block, object self, [DefaultProtocol]int sliceSize) {
            return EachSlice(each, context, block, self, sliceSize, false, (slice) => {
                RubyArray newSlice = new RubyArray(slice.Count);
                for (int i = 1; i < slice.Count; i++) {
                    newSlice.Add(slice[i]);
                }
                return newSlice;
            });
        }

        [RubyMethod("each_slice")]
        public static object EachSlice(CallSiteStorage<EachSite>/*!*/ each, RubyContext/*!*/ context, BlockParam/*!*/ block, object self, [DefaultProtocol]int sliceSize) {
            return EachSlice(each, context, block, self, sliceSize, true, (slice) => null);
        }

        private static object EachSlice(CallSiteStorage<EachSite>/*!*/ each, RubyContext/*!*/ context, BlockParam/*!*/ block, object self, int sliceSize, 
            bool includeIncomplete, Func<RubyArray/*!*/, RubyArray>/*!*/ newSliceFactory) {

            if (sliceSize <= 0) {
                throw RubyExceptions.CreateArgumentError("invalid slice size");
            }

            RubyArray slice = null;

            EnumerableModule.Each(each, context, self, Proc.Create(context, delegate(BlockParam/*!*/ selfBlock, object item) {
                if (slice == null) {
                    slice = new RubyArray(sliceSize);
                }
                
                slice.Add(item);
                
                if (slice.Count == sliceSize) {
                    if (block == null) {
                        throw RubyExceptions.NoBlockGiven();
                    }

                    var completeSlice = slice;
                    slice = newSliceFactory(slice);

                    object blockResult;
                    if (block.Yield(completeSlice, out blockResult)) {
                        return blockResult;
                    }
                }

                return null;
            }));

            if (slice != null && includeIncomplete) {
                if (block == null) {
                    throw RubyExceptions.NoBlockGiven();
                } 

                object blockResult;
                if (block.Yield(slice, out blockResult)) {
                    return blockResult;
                }
            }

            return null;
        }

        #endregion

        #region enum_cons, enum_slice, enum_with_index

        [RubyMethod("enum_cons")]
        public static Enumerator/*!*/ GetConsEnumerator(object self, [DefaultProtocol]int sliceSize) {
            return new Enumerator(self, "each_cons", sliceSize);
        }

        [RubyMethod("enum_slice")]
        public static Enumerator/*!*/ GetSliceEnumerator(object self, [DefaultProtocol]int sliceSize) {
            return new Enumerator(self, "each_slice", sliceSize);
        }

        [RubyMethod("enum_with_index")]
        public static Enumerator/*!*/ GetEnumeratorWithIndex(object self) {
            return new Enumerator(self, "each_with_index", null);
        }

        #endregion
    }
}

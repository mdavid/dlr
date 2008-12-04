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

using BinaryOpSite = Microsoft.Runtime.CompilerServices.CallSite<Microsoft.Func<Microsoft.Runtime.CompilerServices.CallSite,
    IronRuby.Runtime.RubyContext, object, object, object>>;

using UnaryOpSite = Microsoft.Runtime.CompilerServices.CallSite<Microsoft.Func<Microsoft.Runtime.CompilerServices.CallSite,
    IronRuby.Runtime.RubyContext, object, object>>;

using RespondToSite = Microsoft.Runtime.CompilerServices.CallSite<Microsoft.Func<Microsoft.Runtime.CompilerServices.CallSite,
    IronRuby.Runtime.RubyContext, object, Microsoft.Scripting.SymbolId, object>>;

using System; using Microsoft;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Microsoft.Runtime.CompilerServices;

using System.Reflection;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Actions;
using Microsoft.Scripting.Generation;
using IronRuby.Runtime;
using IronRuby.Runtime.Calls;

namespace IronRuby.Builtins {
    /// <summary>
    /// A Range represents an interval�a set of values with a start and an end.
    /// Ranges may be constructed using the s..e and s�e literals, or with Range::new.
    /// Ranges constructed using .. run from the start to the end inclusively.
    /// Those created using � exclude the end value.
    /// When used as an iterator, ranges return each value in the sequence. 
    /// </summary>
    /// <example>
    ///    (-1..-5).to_a      #=> []
    ///    (-5..-1).to_a      #=> [-5, -4, -3, -2, -1]
    ///    ('a'..'e').to_a    #=> ["a", "b", "c", "d", "e"]
    ///    ('a'...'e').to_a   #=> ["a", "b", "c", "d"]
    /// </example>
    /// <remarks>
    /// Ranges can be constructed using objects of any type, as long as the objects can be compared using their <=> operator
    /// and they support the succ method to return the next object in sequence. 
    /// </remarks>
    [RubyClass("Range", Extends = typeof(Range), Inherits = typeof(Object)), Includes(typeof(Enumerable))]
    public static class RangeOps {

        #region Construction and Initialization

        /// <summary>
        /// Construct a new Range object.
        /// </summary>
        /// <returns>
        /// An empty Range object
        /// </returns>
        /// <remarks>
        /// This constructor only creates an empty range object,
        /// which will be initialized subsequently by a separate call through into one of the two initialize methods.
        /// Literal Ranges (e.g. 1..5, 'a'...'b' are created by calls through to RubyOps.CreateInclusiveRange and
        /// RubyOps.CreateExclusiveRange which bypass this constructor/initializer run about and initialize the object directly.
        /// </remarks>
        [RubyConstructor]
        public static Range/*!*/ CreateRange(SiteLocalStorage<BinaryOpSite>/*!*/ comparisonStorage, 
            RubyClass/*!*/ self, object begin, object end, [Optional]bool excludeEnd) {
            return new Range(comparisonStorage, self.Context, begin, end, excludeEnd);
        }

        // Reinitialization. Not called when a factory/non-default ctor is called.
        [RubyMethod("initialize", RubyMethodAttributes.PrivateInstance)]
        public static Range/*!*/ Reinitialize(SiteLocalStorage<BinaryOpSite>/*!*/ comparisonStorage, 
            RubyContext/*!*/ context, Range/*!*/ self, object begin, object end, [Optional]bool excludeEnd) {
            self.Initialize(comparisonStorage, context, begin, end, excludeEnd);
            return self;
        }

        #endregion

        #region begin, first
        /// <summary>
        /// Returns the first object in self
        /// </summary>
        [RubyMethod("begin"), RubyMethod("first")]
        public static object Begin([NotNull]Range/*!*/ self) {
            return self.Begin;
        }
        #endregion

        #region end, last
        /// <summary>
        /// Returns the object that defines the end of self
        /// </summary>
        [RubyMethod("end"), RubyMethod("last")]
        public static object End([NotNull]Range/*!*/ self) {
            return self.End;
        }
        #endregion

        #region exclude_end?
        /// <summary>
        /// Returns true if self excludes its end value. 
        /// </summary>
        [RubyMethod("exclude_end?")]
        public static bool ExcludeEnd([NotNull]Range/*!*/ self) {
            return self.ExcludeEnd;
        }
        #endregion

        #region inspect

        /// <summary>
        /// Convert this range object to a printable form (using inspect to convert the start and end objects). 
        /// </summary>
        [RubyMethod("inspect")]
        public static MutableString/*!*/ Inspect(RubyContext/*!*/ context, Range/*!*/ self) {
            MutableString str = RubySites.Inspect(context, self.Begin);
            str.Append(self.ExcludeEnd ? "..." : "..");
            str.Append(RubySites.Inspect(context, self.End));
            return str;
        }

        #endregion

        #region to_s
        /// <summary>
        /// Convert this range object to a printable form (using to_s to convert the start and end objects).
        /// </summary>
        [RubyMethod("to_s")]
        public static MutableString/*!*/ ToS(RubyContext/*!*/ context, Range/*!*/ self) {
            MutableString str = RubySites.ToS(context, self.Begin);
            str.Append(self.ExcludeEnd ? "..." : "..");
            str.Append(RubySites.ToS(context, self.End));
            return str;
        }
        #endregion

        #region ==, eql?

        /// <summary>
        /// Is other equal to self?  Here other is not a Range so returns false.
        /// </summary>
        [RubyMethod("=="), RubyMethod("eql?")]
        public static bool Equals(Range/*!*/ self, object other) {
            return false;
        }

        /// <summary>
        /// Returns true only if self is a Range, has equivalent beginning and end items (by comparing them with ==),
        /// and has the same exclude_end? setting as <i>other</t>. 
        /// </summary>
        /// <example>
        /// (0..2) == (0..2)            #=> true
        /// (0..2) == Range.new(0,2)    #=> true
        /// (0..2) == (0...2)           #=> false
        /// (0..2).eql?(0.0..2.0)       #=> true
        /// </example>
        [RubyMethod("==")]
        public static bool Equals(RubyContext/*!*/ context, Range/*!*/ self, [NotNull]Range/*!*/ other) {
            if (self == other) {
                return true;
            }
            return (Protocols.IsEqual(context, self.Begin, other.Begin)
                && Protocols.IsEqual(context, self.End, other.End)
                && self.ExcludeEnd == other.ExcludeEnd);
        }

        /// <summary>
        /// Returns true only if self is a Range, has equivalent beginning and end items (by comparing them with eql?),
        /// and has the same exclude_end? setting as <i>other</t>. 
        /// </summary>
        /// <example>
        /// (0..2).eql?(0..2)             #=> true
        /// (0..2).eql?(Range.new(0,2))   #=> true
        /// (0..2).eql?(0...2)            #=> false
        /// (0..2).eql?(0.0..2.0)         #=> false
        /// </example>
        [RubyMethod("eql?")]
        public static bool Eql(SiteLocalStorage<BinaryOpSite>/*!*/ equalsStorage, 
            RubyContext/*!*/ context, Range/*!*/ self, [NotNull]Range/*!*/ other) {

            if (self == other) {
                return true;
            }

            var site = equalsStorage.GetCallSite("eql?", 1);
            return Protocols.IsTrue(site.Target(site, context, self.Begin, other.Begin))
                && Protocols.IsTrue(site.Target(site, context, self.End, other.End))
                && self.ExcludeEnd == other.ExcludeEnd;
        }

        #endregion

        #region ===, member?, include?

        /// <summary>
        /// Returns true if other is an element of self, false otherwise.
        /// Conveniently, === is the comparison operator used by case statements. 
        /// </summary>
        /// <example>
        /// case 79
        ///   when 1..50   then   print "low\n"
        ///   when 51..75  then   print "medium\n"
        ///   when 76..100 then   print "high\n"
        /// end
        /// => "high"
        /// </example>
        [RubyMethod("==="), RubyMethod("member?"), RubyMethod("include?")]
        public static bool CaseEquals(
            SiteLocalStorage<BinaryOpSite>/*!*/ comparisonStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ lessThanStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ greaterThanStorage,
            RubyContext/*!*/ context, [NotNull]Range/*!*/ self, object value) {

            var compare = comparisonStorage.GetCallSite("<=>", 1);

            object result = compare.Target(compare, context, self.Begin, value);
            if (result == null || Protocols.ConvertCompareResult(lessThanStorage, greaterThanStorage, context, result) > 0) {
                return false;
            }

            result = compare.Target(compare, context, value, self.End);
            if (result == null) {
                return false;
            }

            int valueToEnd = Protocols.ConvertCompareResult(lessThanStorage, greaterThanStorage, context, result);
            return valueToEnd < 0 || (!self.ExcludeEnd && valueToEnd == 0);
        }

        #endregion

        #region hash
        /// <summary>
        /// Generate a hash value such that two ranges with the same start and end points,
        /// and the same value for the "exclude end" flag, generate the same hash value. 
        /// </summary>
        [RubyMethod("hash")]
        public static int GetHashCode(Range/*!*/ self) {
            int hash = RubyUtils.GetHashCode(self.Begin);
            hash ^= RubyUtils.GetHashCode(self.End);
            hash ^= RubyUtils.GetHashCode(self.ExcludeEnd);
            return hash;
        }
        #endregion

        #region each

        /// <summary>
        /// Iterates over the elements of self, passing each in turn to the block.
        /// You can only iterate if the start object of the range supports the succ method
        /// (which means that you can�t iterate over ranges of Float objects). 
        /// </summary>
        [RubyMethod("each")]
        public static object Each(
            SiteLocalStorage<RespondToSite>/*!*/ respondToStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ comparisonStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ lessThanStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ greaterThanStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ equalsStorage,
            SiteLocalStorage<UnaryOpSite>/*!*/ succStorage,
            RubyContext/*!*/ context, BlockParam block, Range/*!*/ self) {

            // We check that self.begin responds to "succ" even though some of the implementations don't use it.
            CheckBegin(respondToStorage, context, self.Begin);

            if (self.Begin is int && self.End is int) {
                return StepFixnum(context, block, self, (int)self.Begin, (int)self.End, 1);
            } else if (self.Begin is MutableString) {
                return StepString(comparisonStorage, lessThanStorage, greaterThanStorage, succStorage, context, 
                    block, self, (MutableString)self.Begin, (MutableString)self.End, 1
                );
            } else {
                return StepObject(comparisonStorage, lessThanStorage, greaterThanStorage, equalsStorage, succStorage, context, 
                    block, self, self.Begin, self.End, 1
                );
            }
        }

        #endregion

        #region step

        /// <summary>
        /// Iterates over self, passing each stepth element to the block.
        /// If the range contains numbers or strings, natural ordering is used.
        /// Otherwise step invokes succ to iterate through range elements.
        /// </summary>
        [RubyMethod("step")]
        public static object Step(
            SiteLocalStorage<RespondToSite>/*!*/ respondToStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ comparisonStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ lessThanStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ lessThanEqualsStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ greaterThanStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ equalsStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ addStorage,
            SiteLocalStorage<UnaryOpSite>/*!*/ succStorage,
            RubyContext/*!*/ context, BlockParam block, Range/*!*/ self, [Optional]object step) {

            if (step == Missing.Value) {
                step = ScriptingRuntimeHelpers.Int32ToObject(1);
            }
            
            // We attempt to cast step to Fixnum here even though if we were iterating over Floats, for instance, we use step as is.
            // This prevents cases such as (1.0..2.0).step(0x800000000000000) {|x| x } from working but that is what MRI does.
            if (self.Begin is int && self.End is int) {
                // self.begin is Fixnum; directly call item = item + 1 instead of succ
                int intStep = Protocols.CastToFixnum(context, step);
                return StepFixnum(context, block, self, (int)self.Begin, (int)self.End, intStep);
            } else if (self.Begin is MutableString ) {
                // self.begin is String; use item.succ and item <=> self.end but make sure you check the length of the strings
                int intStep = Protocols.CastToFixnum(context, step);
                return StepString(comparisonStorage, lessThanStorage, greaterThanStorage, succStorage, context,
                    block, self, (MutableString)self.Begin, (MutableString)self.End, intStep
                );
            } else if (context.IsInstanceOf(self.Begin, context.GetClass(typeof(Numeric)))) {
                // self.begin is Numeric; invoke item = item + 1 instead of succ and invoke < or <= for compare
                return StepNumeric(lessThanStorage, lessThanEqualsStorage, equalsStorage, addStorage, context, 
                    block, self, self.Begin, self.End, step
                );
            } else {
                // self.begin is not Numeric or String; just invoke item.succ and item <=> self.end
                CheckBegin(respondToStorage, context, self.Begin);
                int intStep = Protocols.CastToFixnum(context, step);
                return StepObject(comparisonStorage, lessThanStorage, greaterThanStorage, equalsStorage, succStorage, context,
                    block, self, self.Begin, self.End, intStep
                );
            }
        }

        #endregion

        #region Private Helper Stuff

        /// <summary>
        /// Step through a Range of Fixnums.
        /// </summary>
        /// <remarks>
        /// This method is optimized for direct integer operations using &lt; and + directly.
        /// It is not used if either begin or end are outside Fixnum bounds.
        /// </remarks>
        private static object StepFixnum(RubyContext/*!*/ context, BlockParam block, Range/*!*/ self, int begin, int end, int step) {
            CheckStep(step);

            // throw only if there is at least one item that will be enumerated:
            if (block == null && begin != end && !self.ExcludeEnd) {
                throw RubyExceptions.NoBlockGiven();
            }
            
            object result;
            int item = begin;
            while (item < end) {
                if (block.Yield(item, out result)) {
                    return result;
                }
                item += step;
            }

            if (item == end && !self.ExcludeEnd) {
                if (block.Yield(item, out result)) {
                    return result;
                }
            }
            return self;
        }

        /// <summary>
        /// Step through a Range of Strings.
        /// </summary>
        /// <remarks>
        /// This method requires step to be a Fixnum.
        /// It uses a hybrid string comparison to prevent infinite loops and calls String#succ to get each item in the range.
        /// </remarks>
        private static object StepString(
            SiteLocalStorage<BinaryOpSite>/*!*/ comparisonStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ lessThanStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ greaterThanStorage,
            SiteLocalStorage<UnaryOpSite>/*!*/ succStorage, 
            RubyContext/*!*/ context, BlockParam block, Range/*!*/ self, MutableString begin, MutableString end, int step) {

            CheckStep(step);
            object result;
            MutableString item = begin;
            int comp;

            var succSite = succStorage.GetCallSite("succ", 0);
            while ((comp = Protocols.Compare(comparisonStorage, lessThanStorage, greaterThanStorage, context, item, end)) < 0) {
                if (block == null) {
                    throw RubyExceptions.NoBlockGiven();
                }

                if (block.Yield(item, out result)) {
                    return result;
                }

                for (int i = 0; i < step; i++) {
                    item = Protocols.CastToString(context, succSite.Target(succSite, context, item));
                }

                if (item.Length > end.Length) {
                    return self;
                }
            }

            if (comp == 0 && !self.ExcludeEnd) {
                if (block == null) {
                    throw RubyExceptions.NoBlockGiven();
                } 
                
                if (block.Yield(item, out result)) {
                    return result;
                }
            }
            return self;
        }

        /// <summary>
        /// Step through a Range of Numerics.
        /// </summary>
        private static object StepNumeric(
            SiteLocalStorage<BinaryOpSite>/*!*/ lessThanStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ lessThanEqualsStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ equalsStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ addStorage,
            RubyContext/*!*/ context, BlockParam block, Range/*!*/ self, object begin, object end, object step) {

            var lessThan = lessThanStorage.GetCallSite("<", 1);
            var equals = equalsStorage.GetCallSite("==", 1);
            CheckStep(lessThan, equals, context, step);

            object item = begin;
            object result;

            var site = self.ExcludeEnd ? lessThan : lessThanEqualsStorage.GetCallSite("<=", 1);
            while (RubyOps.IsTrue(site.Target(site, context, item, end))) {
                if (block == null) {
                    throw RubyExceptions.NoBlockGiven();
                }

                if (block.Yield(item, out result)) {
                    return result;
                }

                var add = addStorage.GetCallSite("+", 1);
                item = add.Target(add, context, item, step);
            }

            return self;
        }

        /// <summary>
        /// Step through a Range of objects that are not Numeric or String.
        /// </summary>
        private static object StepObject(
            SiteLocalStorage<BinaryOpSite>/*!*/ comparisonStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ lessThanStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ greaterThanStorage,
            SiteLocalStorage<BinaryOpSite>/*!*/ equalsStorage,
            SiteLocalStorage<UnaryOpSite>/*!*/ succStorage,
            RubyContext/*!*/ context, BlockParam block, Range/*!*/ self, object begin, object end, int step) {

            CheckStep(lessThanStorage.GetCallSite("<", 1), equalsStorage.GetCallSite("==", 1), context, step);

            object item = begin, result;
            int comp;

            var succSite = succStorage.GetCallSite("succ", 0);
            while ((comp = Protocols.Compare(comparisonStorage, lessThanStorage, greaterThanStorage, context, item, end)) < 0) {
                if (block == null) {
                    throw RubyExceptions.NoBlockGiven();
                }

                if (block.Yield(item, out result)) {
                    return result;
                }

                for (int i = 0; i < step; ++i) {
                    item = succSite.Target(succSite, context, item);
                }
            }

            if (comp == 0 && !self.ExcludeEnd) {
                if (block == null) {
                    throw RubyExceptions.NoBlockGiven();
                }

                if (block.Yield(item, out result)) {
                    return result;
                }
            }
            return self;
        }

        /// <summary>
        /// Check that the int is not less than or equal to zero.
        /// </summary>
        private static void CheckStep(int step) {
            if (step == 0) {
                throw RubyExceptions.CreateArgumentError("step can't be 0");
            }
            if (step < 0) {
                throw RubyExceptions.CreateArgumentError("step can't be negative");
            }
        }

        /// <summary>
        /// Check that the object, when converted to an integer, is not less than or equal to zero.
        /// </summary>
        private static void CheckStep(
            BinaryOpSite/*!*/ lessThanSite,
            BinaryOpSite/*!*/ equalsSite,
            RubyContext/*!*/ context, object step) {

            if (Protocols.IsTrue(equalsSite.Target(equalsSite, context, step, 0))) {
                throw RubyExceptions.CreateArgumentError("step can't be 0");
            }

            if (RubyOps.IsTrue(lessThanSite.Target(lessThanSite, context, step, 0))) {
                throw RubyExceptions.CreateArgumentError("step can't be negative");
            }
        }

        /// <summary>
        /// Check that the object responds to "succ".
        /// </summary>
        private static void CheckBegin(SiteLocalStorage<RespondToSite>/*!*/ respondToStorage,
            RubyContext/*!*/ context, object begin) {
            if (!Protocols.RespondTo(respondToStorage, context, begin, "succ")) {
                throw RubyExceptions.CreateTypeError(String.Format("can't iterate from {0}", RubyUtils.GetClassName(context, begin)));
            }
        }

        #endregion
    }
}

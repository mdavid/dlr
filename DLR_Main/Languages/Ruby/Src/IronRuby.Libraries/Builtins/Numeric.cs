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
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Actions;
using Microsoft.Scripting.Generation;
using IronRuby.Runtime;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Runtime.Serialization;
using IronRuby.Runtime.Calls;

namespace IronRuby.Builtins {

    [RubyClass("Numeric", Inherits = typeof(object)), Includes(typeof(Comparable))]
    public class Numeric : RubyObject {

        public Numeric(RubyClass/*!*/ cls) 
            : base(cls) { 
        }

#if !SILVERLIGHT
        protected Numeric(SerializationInfo/*!*/ info, StreamingContext context) 
            : base(info, context) {
        }
#endif

        #region +@, -@

        /// <summary>
        /// Unary plus - returns the receivers value
        /// </summary>
        [RubyMethod("+@")]
        public static object UnaryPlus(object self) {
            return self;
        }

        /// <summary>
        /// Unary minus - returns the receivers value, negated.
        /// </summary>
        /// <remarks>
        /// Equivalent to:
        /// <code>
        ///     c = 0.coerce(self);
        ///     c[0] - c[1]
        /// </code>
        /// </remarks>
        [RubyMethod("-@")]
        public static object UnaryMinus(BinaryOpStorage/*!*/ coercionStorage, BinaryOpStorage/*!*/ binaryOpSite, 
            RubyContext/*!*/ context, object self) {

            return Protocols.CoerceAndApply(coercionStorage, binaryOpSite, "-", context, 0, self);
        }

        #endregion

        #region <=>
        /// <summary>
        /// Returns zero if self equals other (and is same type), nil otherwise. 
        [RubyMethod("<=>")]
        public static object Compare(object self, object other) {
            if (self == other) {
                return 0;
            }
            return null;
        }
        #endregion

        #region abs

        /// <summary>
        /// Returns the absolute value of self
        /// </summary>
        /// <remarks>
        /// Dynamically invokes < operator on self and 0
        /// If this is true then invokes @- on self.
        /// Otherwise just returns self
        /// </remarks>
        [RubyMethod("abs")]
        public static object Abs(
            BinaryOpStorage/*!*/ lessThanStorage,
            UnaryOpStorage/*!*/ minusStorage, 
            RubyContext/*!*/ context, object self) {

            var lessThan = lessThanStorage.GetCallSite("<");
            if (RubyOps.IsTrue(lessThan.Target(lessThan, context, self, 0))) {
                var minus = minusStorage.GetCallSite("-@");
                return minus.Target(minus, context, self);
            }
            return self;
        }

        #endregion

        #region coerce

        /// <summary>
        /// If other is the same type as self, returns an array [other, self].
        /// Otherwise, returns an array [floatOther, floatSelf], whose elements are other and self represented as Float objects.
        /// </summary>
        /// <remarks>
        /// This coercion mechanism is used by Ruby to handle mixed-type numeric operations:
        /// It is intended to find a compatible common type between the two operands of the operator. 
        /// </remarks>
        [RubyMethod("coerce")]
        public static RubyArray Coerce(ConversionStorage<double>/*!*/ tof1, ConversionStorage<double>/*!*/ tof2, 
            RubyContext/*!*/ context, object self, object other) {

            if (context.GetClassOf(self) == context.GetClassOf(other)) {
                return RubyOps.MakeArray2(other, self);
            }

            var site1 = tof1.GetSite(ConvertToFAction.Instance);
            var site2 = tof2.GetSite(ConvertToFAction.Instance);
            return RubyOps.MakeArray2(site1.Target(site1, context, other), site2.Target(site2, context, self));
        }

        #endregion

        #region div

        /// <summary>
        /// Dynamically invokes / operator to perform division, then converts the result to an integer.
        /// </summary>
        /// <remarks>
        /// Numeric does not define the / operator; this is left to subclasses.
        /// </remarks>
        [RubyMethod("div")]
        public static object Div(
            BinaryOpStorage/*!*/ divideStorage,
            ConversionStorage<double>/*!*/ tofStorage,
            RubyContext/*!*/ context, object self, object other) {

            var divide = divideStorage.GetCallSite("/");
            var tof = tofStorage.GetSite(ConvertToFAction.Instance);
            return FloatOps.Floor(tof.Target(tof, context, divide.Target(divide, context, self, other)));
        }

        #endregion

        #region divmod

        /// <summary>
        /// Returns an array [quotient, modulus] obtained by dividing self by other.
        /// The quotient is rounded toward -infinity.
        /// If q, r = x.divmod(y), then 
        ///     q = floor(float(x)/float(y))
        ///     x = q*y + r
        /// </summary>
        /// <remarks>
        /// The quotient is found by directly calling Numeric#div
        /// The modulus is found by dynamically invoking modulo method on self passing other.
        /// </remarks>
        [RubyMethod("divmod")]
        public static RubyArray DivMod(
            BinaryOpStorage/*!*/ divideStorage,
            BinaryOpStorage/*!*/ moduloStorage,
            ConversionStorage<double>/*!*/ tofStorage,
            RubyContext/*!*/ context, object self, object other) {

            object div = Div(divideStorage, tofStorage, context, self, other);

            var modulo = moduloStorage.GetCallSite("modulo");
            object mod = modulo.Target(modulo, context, self, other);

            return RubyOps.MakeArray2(div, mod);
        }

        #endregion

        #region eql?

        /// <summary>
        /// Returns true if num and numeric are the same type and have equal values. 
        /// </summary>
        [RubyMethod("eql?")]
        public static bool Eql(BinaryOpStorage/*!*/ equals, RubyContext/*!*/ context, object self, object other) {
            if (context.GetClassOf(self) != context.GetClassOf(other)) {
                return false;
            }
            return Protocols.IsEqual(equals, context, self, other);
        }

        #endregion

        #region round, floor, ceil, truncate

        /// <summary>
        /// Rounds self to the nearest integer.
        /// </summary>
        /// <remarks>
        /// This is achieved by converting self to a Float and directly calling Float#round.
        /// </remarks>
        [RubyMethod("round")]
        public static object Round([DefaultProtocol]double self) {
            return FloatOps.Round(self);
        }

        /// <summary>
        /// Returns the largest integer less than or equal to self.
        /// </summary>
        /// <remarks>
        /// This is achieved by converting self to a Float and directly calling Float#floor. 
        /// </remarks>
        [RubyMethod("floor")]
        public static object Floor([DefaultProtocol]double self) {
            return FloatOps.Floor(self);
        }

        /// <summary>
        /// Returns the smallest Integer greater than or equal to num.
        /// </summary>
        /// <remarks>
        /// This is achieved by converting self to a Float then directly calling Float#ceil.
        /// </remarks>
        [RubyMethod("ceil")]
        public static object Ceil([DefaultProtocol]double self) {
            return FloatOps.Ceil(self);
        }

        /// <summary>
        /// Returns self truncated to an integer.
        /// </summary>
        /// <remarks>
        /// This is achieved by converting self to a float and directly calling Float#truncate. 
        /// </remarks>
        [RubyMethod("truncate")]
        public static object Truncate([DefaultProtocol]double self) {
            return FloatOps.ToInt(self);
        }
        
        #endregion

        #region integer?
        /// <summary>
        /// Returns true if self is an Integer (i.e. Fixnum or Bignum).
        /// </summary>
        [RubyMethod("integer?")]
        public static bool IsInteger(object self) {
            return false;
        }
        #endregion

        #region modulo

        /// <summary>
        /// Equivalent to self.divmod(other)[1]
        /// </summary>
        /// <remarks>
        /// This is achieved by dynamically invoking % operator on self and other.
        /// </remarks>
        [RubyMethod("modulo")]
        public static object Modulo(
            BinaryOpStorage/*!*/ moduloStorage,
            RubyContext/*!*/ context, object self, object other) {

            var modulo = moduloStorage.GetCallSite("%");
            return modulo.Target(modulo, context, self, other);
        }

        #endregion

        #region nonzero?

        /// <summary>
        /// Returns num if num is not zero, nil otherwise.
        /// </summary>
        /// <example>
        /// This behavior is useful when chaining comparisons: 
        ///     a = %w( z Bb bB bb BB a aA Aa AA A )
        ///     b = a.sort {|a,b| (a.downcase <=> b.downcase).nonzero? || a <=> b }
        ///     b   #=> ["A", "a", "AA", "Aa", "aA", "BB", "Bb", "bB", "bb", "z"]
        /// </example>
        /// <remarks>
        /// This is achieved by dynamically invoking IsZero on self;
        /// returning nil if it is or self otherwise.
        /// </remarks>
        [RubyMethod("nonzero?")]
        public static object IsNonZero(UnaryOpStorage/*!*/ isZeroStorage, RubyContext/*!*/ context, object self) {
            var isZero = isZeroStorage.GetCallSite("zero?");
            return Protocols.IsTrue(isZero.Target(isZero, context, self)) ? null : self;
        }

        #endregion

        #region quo
        /// <summary>
        /// Equivalent to invoking Numeric#/; overridden in subclasses
        /// </summary>
        [RubyMethod("quo")]
        public static object Quo(BinaryOpStorage/*!*/ divideStorage,
            RubyContext/*!*/ context, object self, object other) {

            var site = divideStorage.GetCallSite("/");
            return site.Target(site, context, self, other);
        }
        #endregion

        #region remainder

        /// <summary>
        /// If self and other have different signs, returns (self.modulo(other)-other;
        /// otherwise, returns self.modulo(other).
        /// </summary>
        /// <remarks>
        /// This is achieved by dynamically invoking modulo on self;
        /// then invoking &lt; operator on the self and other against 0.
        /// </remarks>
        [RubyMethod("remainder")]
        public static object Remainder(
            BinaryOpStorage/*!*/ equals, 
            BinaryOpStorage/*!*/ greaterThanStorage,
            BinaryOpStorage/*!*/ lessThanStorage,
            BinaryOpStorage/*!*/ minusStorage,
            BinaryOpStorage/*!*/ moduloStorage,
            RubyContext/*!*/ context, object self, object other) {

            var modulo = moduloStorage.GetCallSite("%");
            object remainder = modulo.Target(modulo, context, self, other);

            if (!Protocols.IsEqual(equals, context, remainder, 0)) {
                var greaterThan = greaterThanStorage.GetCallSite(">");
                var lessThan = lessThanStorage.GetCallSite("<");

                // modulo is not zero
                if (RubyOps.IsTrue(lessThan.Target(lessThan, context, self, 0)) && RubyOps.IsTrue(greaterThan.Target(greaterThan, context, other, 0)) ||
                    RubyOps.IsTrue(greaterThan.Target(greaterThan, context, self, 0)) && RubyOps.IsTrue(lessThan.Target(lessThan, context, other, 0))) {
                    // (self is negative and other is positive) OR (self is positive and other is negative)
                    var minus = minusStorage.GetCallSite("-");
                    return minus.Target(minus, context, remainder, other);
                }
            }
            // Either modulo is zero or self and other are not of the same sign
            return remainder;
        }

        #endregion

        #region step

        /// <summary>
        /// Invokes block with the sequence of numbers starting at self, incremented by step on each call.
        /// The loop finishes when the value to be passed to the block is greater than limit (if step is positive) or less than limit (if step is negative).
        /// </summary>
        [RubyMethod("step")]
        public static object Step(RubyContext/*!*/ context, BlockParam block, int self, int limit) {
            return Step(context, block, self, limit, 1);
        }

        /// <summary>
        /// Invokes block with the sequence of numbers starting at self, incremented by step on each call.
        /// The loop finishes when the value to be passed to the block is greater than limit (if step is positive) or less than limit (if step is negative).
        /// </summary>
        [RubyMethod("step")]
        public static object Step(RubyContext/*!*/ context, BlockParam block, int self, int limit, int step) {
            if (step == 0) {
                throw RubyExceptions.CreateArgumentError("step can't be 0");
            }
            if (step > 0) {
                int current = self;
                while (current <= limit) {
                    object result;
                    if (YieldStep(block, current, out result)) {
                        return result;
                    }
                    current += step;
                }
            } else {
                int current = self;
                while (current >= limit) {
                    object result;
                    if (YieldStep(block, current, out result)) {
                        return result;
                    }
                    current += step;
                }
            }
            return self;
        }

        /// <summary>
        /// Invokes block with the sequence of numbers starting at self, incremented by step on each call.
        /// The loop finishes when the value to be passed to the block is greater than limit (if step is positive) or less than limit (if step is negative).
        /// </summary>
        [RubyMethod("step")]
        public static object Step(RubyContext/*!*/ context, BlockParam block, double self, double limit, double step) {
            if (step == 0) {
                throw RubyExceptions.CreateArgumentError("step can't be 0");
            }
            double n = (limit - self) / step;
            int count = ((int)System.Math.Floor(n + n * Double.Epsilon)) + 1;
            double current = self;
            while (count > 0) {
                object result;
                if (YieldStep(block, current, out result)) {
                    return result;
                }
                current += step;
                count--;
            }
            return self;
        }

        /// <summary>
        /// Invokes block with the sequence of numbers starting at self, incremented by step on each call.
        /// The loop finishes when the value to be passed to the block is greater than limit (if step is positive) or less than limit (if step is negative).
        /// </summary>
        [RubyMethod("step")]
        public static object Step(
            BinaryOpStorage/*!*/ equals, 
            BinaryOpStorage/*!*/ greaterThanStorage,
            BinaryOpStorage/*!*/ lessThanStorage,
            BinaryOpStorage/*!*/ addStorage, 
            ConversionStorage<double>/*!*/ tofStorage,
            RubyContext/*!*/ context, BlockParam block, object self, object limit, [Optional]object step) {

            if (step == Missing.Value) {
                step = ScriptingRuntimeHelpers.Int32ToObject(1);
            }

            if (self is double || limit is double || step is double) {
                var site = tofStorage.GetSite(ConvertToFAction.Instance);
                // At least one of the arguments is double so convert all to double and run the Float version of Step
                double floatSelf = self is double ? (double)self : site.Target(site, context, self);
                double floatLimit = limit is double ? (double)self : site.Target(site, context, limit);
                double floatStep = step is double ? (double)self : site.Target(site, context, step);
                return Step(context, block, floatSelf, floatLimit, floatSelf);
            } else {
                #region The generic step algorithm:
                // current = self
                // if step is postive then
                //   while current < limit do
                //     yield(current)
                //     current = current + step
                //   end
                // else
                //   while current > limit do
                //     yield(current)
                //     current = current + step
                //   end
                // return self
                #endregion

                bool isStepZero = Protocols.IsEqual(equals, context, step, 0);
                if (isStepZero) {
                    throw RubyExceptions.CreateArgumentError("step can't be 0");
                }

                var greaterThan = greaterThanStorage.GetCallSite(">");
                bool isStepPositive = RubyOps.IsTrue(greaterThan.Target(greaterThan, context, step, 0));
                var compare = isStepPositive ? greaterThan : lessThanStorage.GetCallSite("<");

                object current = self;
                while (!RubyOps.IsTrue(compare.Target(compare, context, current, limit))) {
                    object result;
                    if (YieldStep(block, current, out result)) {
                        return result;
                    }

                    var add = addStorage.GetCallSite("+");
                    current = add.Target(add, context, current, step);
                }
                return self;
            }
        }

        private static bool YieldStep(BlockParam block, object current, out object result) {
            if (block == null) {
                throw RubyExceptions.NoBlockGiven();
            }

            return block.Yield(current, out result);
        }

        #endregion

        #region to_int
        
        /// <summary>
        /// Invokes the self.to_i method to convert self to an integer.
        /// </summary>
        [RubyMethod("to_int")]
        public static object ToInt(UnaryOpStorage/*!*/ toiStorage, RubyContext/*!*/ context, object self) {
            var site = toiStorage.GetCallSite("to_i");
            return site.Target(site, context, self);
        }
        
        #endregion

        #region zero?
        
        /// <summary>
        /// Returns true if self has a zero value. 
        /// </summary>
        [RubyMethod("zero?")]
        public static bool IsZero(BinaryOpStorage/*!*/ equals, RubyContext/*!*/ context, object self) {
            return Protocols.IsEqual(equals, context, self, 0);
        }
        
        #endregion
    }
}

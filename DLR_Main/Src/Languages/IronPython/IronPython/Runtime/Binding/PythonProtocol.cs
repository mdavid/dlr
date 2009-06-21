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
using System.Dynamic;
using System.Linq.Expressions;
#else
using System; using Microsoft;
using Microsoft.Scripting;
using Microsoft.Linq.Expressions;
#endif
using System.Runtime.CompilerServices;
#if !CODEPLEX_40
using Microsoft.Runtime.CompilerServices;
#endif


using Microsoft.Scripting.Actions;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;

using IronPython.Runtime.Operations;
using IronPython.Runtime.Types;

namespace IronPython.Runtime.Binding {
#if CODEPLEX_40
    using Ast = System.Linq.Expressions.Expression;
#else
    using Ast = Microsoft.Linq.Expressions.Expression;
#endif
    using AstUtils = Microsoft.Scripting.Ast.Utils;

    /// <summary>
    /// Provides binding logic which is implemented to follow various Python protocols.  This includes
    /// things such as calling __call__ to perform calls, calling __nonzero__/__len__ to convert to
    /// bool, calling __add__/__radd__ to do addition, etc...  
    /// 
    /// This logic gets shared between both the IDynamicMetaObjectProvider implementation for Python objects as well
    /// as the Python sites.  This ensures the logic we follow for our builtin types and user defined
    /// types is identical and properly conforming to the various protocols.
    /// </summary>
    static partial class PythonProtocol {

        #region Conversions

        /// <summary>
        /// Gets a MetaObject which converts the provided object to a bool using __nonzero__ or __len__
        /// protocol methods.  This code is shared between both our fallback for a site and our MetaObject
        /// for user defined objects.
        /// </summary>
        internal static DynamicMetaObject ConvertToBool(DynamicMetaObjectBinder/*!*/ conversion, DynamicMetaObject/*!*/ self) {
            Assert.NotNull(conversion, self);

            SlotOrFunction sf = SlotOrFunction.GetSlotOrFunction(
                PythonContext.GetPythonContext(conversion),
                Symbols.NonZero,
                self);

            if (sf.Success) {
                if (sf.Target.Expression.Type != typeof(bool)) {
                    return new DynamicMetaObject(
                        Ast.Call(
                            typeof(PythonOps).GetMethod("ThrowingConvertToNonZero"),
                            sf.Target.Expression
                        ),
                        sf.Target.Restrictions
                    );
                }

                return sf.Target;
            }

            sf = SlotOrFunction.GetSlotOrFunction(
                PythonContext.GetPythonContext(conversion),
                Symbols.Length,
                self);

            if (sf.Success) {
                return new DynamicMetaObject(
                    GetConvertByLengthBody(
                        PythonContext.GetPythonContext(conversion),
                        sf.Target.Expression
                    ),
                    sf.Target.Restrictions
                );
            }

            return null;
        }

        /// <summary>
        /// Used for conversions to bool
        /// </summary>
        private static Expression/*!*/ GetConvertByLengthBody(PythonContext/*!*/ state, Expression/*!*/ call) {
            Assert.NotNull(state, call);

            Expression callAsInt = call;
            if (call.Type != typeof(int)) {
                callAsInt = Ast.Dynamic(
                    state.Convert(typeof(int), ConversionResultKind.ExplicitCast),
                    typeof(int),
                    call
                );
            }

            return Ast.NotEqual(callAsInt, AstUtils.Constant(0));
        }

        #endregion

        #region Calls

        internal static DynamicMetaObject Call(DynamicMetaObjectBinder/*!*/ call, DynamicMetaObject target, DynamicMetaObject/*!*/[]/*!*/ args) {
            Assert.NotNull(call, args);
            Assert.NotNullItems(args);

            if (target.NeedsDeferral()) {
                return call.Defer(ArrayUtils.Insert(target, args));
            }

            foreach (DynamicMetaObject mo in args) {
                if (mo.NeedsDeferral()) {
                    RestrictTypes(args);

                    return call.Defer(
                        ArrayUtils.Insert(target, args)
                    );
                }
            }

            DynamicMetaObject self = target.Restrict(target.GetLimitType());

            ValidationInfo valInfo = BindingHelpers.GetValidationInfo(target);
            PythonType pt = DynamicHelpers.GetPythonType(target.Value);
            PythonContext state = PythonContext.GetPythonContext(call);

            // look for __call__, if it's present dispatch to it.  Otherwise fall back to the
            // default binder
            PythonTypeSlot callSlot;
            if (!typeof(Delegate).IsAssignableFrom(target.GetLimitType()) &&
                pt.TryResolveSlot(state.SharedContext, Symbols.Call, out callSlot)) {
                ConditionalBuilder cb = new ConditionalBuilder(call);
                Expression body;

                callSlot.MakeGetExpression(
                    state.Binder,
                    PythonContext.GetCodeContext(call),
                    self.Expression,
                    GetPythonType(self),
                    cb
                );
                
                if (!cb.IsFinal) {
                    cb.FinishCondition(GetCallError(self));
                }

                Expression[] callArgs = ArrayUtils.Insert(
                    PythonContext.GetCodeContext(call),
                    cb.GetMetaObject().Expression, 
                    DynamicUtils.GetExpressions(args)
                );

                body = Ast.Dynamic(
                    PythonContext.GetPythonContext(call).Invoke(
                        BindingHelpers.GetCallSignature(call)
                    ),
                    typeof(object),
                    callArgs
                );

                return BindingHelpers.AddDynamicTestAndDefer(
                    call,
                    new DynamicMetaObject(body, self.Restrictions.Merge(BindingRestrictions.Combine(args))),
                    args,
                    valInfo
                );
            }

            return null;
        }

        private static Expression/*!*/ GetPythonType(DynamicMetaObject/*!*/ self) {
            Assert.NotNull(self);

            PythonType pt = DynamicHelpers.GetPythonType(self.Value);
            if (pt.IsSystemType) {
                return AstUtils.Constant(pt);
            }

            return Ast.Property(
                Ast.Convert(self.Expression, typeof(IPythonObject)),
                TypeInfo._IPythonObject.PythonType
            );
        }

        private static Expression/*!*/ GetCallError(DynamicMetaObject/*!*/ self) {
            Assert.NotNull(self);

            return Ast.Throw(
                Ast.Call(
                    typeof(PythonOps).GetMethod("UncallableError"),
                    AstUtils.Convert(self.Expression, typeof(object))
                )
            );
        }
        
        #endregion
    }
}

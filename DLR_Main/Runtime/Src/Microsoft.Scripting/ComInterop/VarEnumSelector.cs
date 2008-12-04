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

#if !SILVERLIGHT // ComObject

using System; using Microsoft;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Microsoft.Scripting.ComInterop {

    internal static class VarEnumSelector {
        private static readonly Dictionary<VarEnum, Type> _ComToManagedPrimitiveTypes = CreateComToManagedPrimitiveTypes();

        /// <summary>
        /// Gets the managed type that an object needs to be coverted to in order for it to be able
        /// to be represented as a Variant.
        /// 
        /// In general, there is a many-to-many mapping between Type and VarEnum. However, this method
        /// returns a simple mapping that is needed for the current implementation. The reason for the 
        /// many-to-many relation is:
        /// 1. Int32 maps to VT_I4 as well as VT_ERROR, and Decimal maps to VT_DECIMAL and VT_CY. However,
        ///    this changes if you throw the wrapper types into the mix.
        /// 2. There is no Type to represent COM types. __ComObject is a private type, and Object is too
        ///    general.
        /// </summary>
        internal static Type GetManagedMarshalType(VarEnum varEnum) {
            Debug.Assert((varEnum & VarEnum.VT_BYREF) == 0);

            if (varEnum == VarEnum.VT_CY) {
                return typeof(CurrencyWrapper);
            }

            if (IsPrimitiveType(varEnum)) {
                return _ComToManagedPrimitiveTypes[varEnum];
            }

            switch (varEnum) {
                case VarEnum.VT_EMPTY:
                case VarEnum.VT_NULL:
                case VarEnum.VT_UNKNOWN:
                case VarEnum.VT_DISPATCH:
                case VarEnum.VT_VARIANT:
                    return typeof(Object);

                case VarEnum.VT_ERROR:
                    return typeof(ErrorWrapper);

                default:
                    throw new InvalidOperationException(string.Format("Unexpected VarEnum {0}.", varEnum));
            }
        }

        private static Dictionary<VarEnum, Type> CreateComToManagedPrimitiveTypes() {
            Dictionary<VarEnum, Type> dict = new Dictionary<VarEnum, Type>();

            #region Generated ComToManagedPrimitiveTypes

            // *** BEGIN GENERATED CODE ***
            // generated by function: gen_ComToManagedPrimitiveTypes from: generate_comdispatch.py

            dict[VarEnum.VT_I1] = typeof(SByte);
            dict[VarEnum.VT_I2] = typeof(Int16);
            dict[VarEnum.VT_I4] = typeof(Int32);
            dict[VarEnum.VT_I8] = typeof(Int64);
            dict[VarEnum.VT_UI1] = typeof(Byte);
            dict[VarEnum.VT_UI2] = typeof(UInt16);
            dict[VarEnum.VT_UI4] = typeof(UInt32);
            dict[VarEnum.VT_UI8] = typeof(UInt64);
            dict[VarEnum.VT_INT] = typeof(IntPtr);
            dict[VarEnum.VT_UINT] = typeof(UIntPtr);
            dict[VarEnum.VT_BOOL] = typeof(bool);
            dict[VarEnum.VT_R4] = typeof(Single);
            dict[VarEnum.VT_R8] = typeof(Double);
            dict[VarEnum.VT_DECIMAL] = typeof(Decimal);
            dict[VarEnum.VT_DATE] = typeof(DateTime);
            dict[VarEnum.VT_BSTR] = typeof(String);

            // *** END GENERATED CODE ***

            #endregion

            dict[VarEnum.VT_CY] = typeof(CurrencyWrapper);
            dict[VarEnum.VT_ERROR] = typeof(ErrorWrapper);

            return dict;
        }
        
        /// <summary>
        /// Primitive types are the basic COM types. It includes valuetypes like ints, but also reference tyeps
        /// like BStrs. It does not include composite types like arrays and user-defined COM types (IUnknown/IDispatch).
        /// </summary>
        private static bool IsPrimitiveType(VarEnum varEnum) {
            switch (varEnum) {
                #region Generated Variant IsPrimitiveType

                // *** BEGIN GENERATED CODE ***
                // generated by function: gen_IsPrimitiveType from: generate_comdispatch.py

                case VarEnum.VT_I1:
                case VarEnum.VT_I2:
                case VarEnum.VT_I4:
                case VarEnum.VT_I8:
                case VarEnum.VT_UI1:
                case VarEnum.VT_UI2:
                case VarEnum.VT_UI4:
                case VarEnum.VT_UI8:
                case VarEnum.VT_INT:
                case VarEnum.VT_UINT:
                case VarEnum.VT_BOOL:
                case VarEnum.VT_ERROR:
                case VarEnum.VT_R4:
                case VarEnum.VT_R8:
                case VarEnum.VT_DECIMAL:
                case VarEnum.VT_CY:
                case VarEnum.VT_DATE:
                case VarEnum.VT_BSTR:

                // *** END GENERATED CODE ***

                #endregion
                    return true;
            }

            return false;
        }
    }
}

#endif

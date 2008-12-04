//------------------------------------------------------------------------------
// <copyright from='1997' to='2006' company='Microsoft Corporation'>           
//    Copyright (c) Microsoft Corporation. All Rights Reserved.                
//    Information Contained Herein is Proprietary and Confidential.   
//
//    GENERATED FILE.  DO NOT MODIFY.
//    Source is: System\Linq\Expressions\Microsoft.Linq.Expressions.txt         
//
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace Microsoft.Linq.Expressions {
    using System; using Microsoft;
    using System.Resources;

    // class Strings depends on how strings are retrieved from resources
    //internal static class Strings { ...


    /// <summary>
    ///    Strongly-typed and parameterized exception factory.
    /// </summary>
    internal static partial class Error {
         /// <summary>
        /// ArgumentException with message like "Argument type must be comparable"
        /// </summary>
        internal static Exception ArgumentMustBeComparable() {
            return new ArgumentException(Strings.ArgumentMustBeComparable);
        }
        /// <summary>
        /// ArgumentException with message like "Argument must be of a convertible type"
        /// </summary>
        internal static Exception ArgumentMustBeConvertible() {
            return new ArgumentException(Strings.ArgumentMustBeConvertible);
        }
        /// <summary>
        /// ArgumentException with message like "Argument must be of type Int32"
        /// </summary>
        internal static Exception ArgumentMustBeInt32() {
            return new ArgumentException(Strings.ArgumentMustBeInt32);
        }
        /// <summary>
        /// ArgumentException with message like "Argument for a checked operation must be of type Int32, UInt32, Int64 or UInt64"
        /// </summary>
        internal static Exception ArgumentMustBeCheckable() {
            return new ArgumentException(Strings.ArgumentMustBeCheckable);
        }
         /// <summary>
        /// ArgumentException with message like "Argument must be of an integer or boolean type"
        /// </summary>
        internal static Exception ArgumentMustBeIntegerOrBoolean() {
            return new ArgumentException(Strings.ArgumentMustBeIntegerOrBoolean);
        }
        /// <summary>
        /// ArgumentException with message like "Argument must be of a numeric type"
        /// </summary>
        internal static Exception ArgumentMustBeNumeric() {
            return new ArgumentException(Strings.ArgumentMustBeNumeric);
        }
         /// <summary>
        /// InvalidOperationException with message like "Cannot auto initialize elements of value type through property '{0}', use assignment instead"
        /// </summary>
        internal static Exception CannotAutoInitializeValueTypeElementThroughProperty(object p0) {
            return new InvalidOperationException(Strings.CannotAutoInitializeValueTypeElementThroughProperty(p0));
        }
        /// <summary>
        /// InvalidOperationException with message like "Cannot auto initialize members of value type through property '{0}', use assignment instead"
        /// </summary>
        internal static Exception CannotAutoInitializeValueTypeMemberThroughProperty(object p0) {
            return new InvalidOperationException(Strings.CannotAutoInitializeValueTypeMemberThroughProperty(p0));
        }
        /// <summary>
        /// ArgumentException with message like "An expression of type '{0}' cannot be cast to type '{1}'"
        /// </summary>
        internal static Exception CannotCastTypeToType(object p0, object p1) {
            return new ArgumentException(Strings.CannotCastTypeToType(p0,p1));
        }
         /// <summary>
        /// InvalidOperationException with message like "An expression of type '{0}' cannot be used to initialize an collection of type '{1}'"
        /// </summary>
        internal static Exception ExpressionTypeCannotInitializeCollectionType(object p0, object p1) {
            return new InvalidOperationException(Strings.ExpressionTypeCannotInitializeCollectionType(p0,p1));
        }
        /// <summary>
        /// InvalidOperationException with message like "An expression of type '{0}' cannot be used to initialize an array of type '{1}'"
        /// </summary>
        internal static Exception ExpressionTypeDoesNotMatchArrayType(object p0, object p1) {
            return new InvalidOperationException(Strings.ExpressionTypeDoesNotMatchArrayType(p0,p1));
        }
        /// <summary>
        /// InvalidOperationException with message like "Lambda Parameter not in scope"
        /// </summary>
        internal static Exception LambdaParameterNotInScope() {
            return new InvalidOperationException(Strings.LambdaParameterNotInScope);
        }
        /// <summary>
        /// ArgumentException with message like "Parameter not captured"
        /// </summary>
        internal static Exception ParameterNotCaptured() {
            return new ArgumentException(Strings.ParameterNotCaptured);
        }
        /// <summary>
        /// InvalidOperationException with message like "Type parameter is {0}. Expected a delegate."
        /// </summary>
        internal static Exception TypeParameterIsNotDelegate(object p0) {
            return new InvalidOperationException(Strings.TypeParameterIsNotDelegate(p0));
        }
        /// <summary>
        /// ArgumentException with message like "Unhandled method call: {0}"
        /// </summary>
        internal static Exception UnhandledCall(object p0) {
            return new ArgumentException(Strings.UnhandledCall(p0));
        }
         /// <summary>
        /// ArgumentException with message like "Unhandled binding "
        /// </summary>
        internal static Exception UnhandledBinding() {
            return new ArgumentException(Strings.UnhandledBinding);
        }
        /// <summary>
        /// ArgumentException with message like "Unhandled Binding Type: {0}"
        /// </summary>
        internal static Exception UnhandledBindingType(object p0) {
            return new ArgumentException(Strings.UnhandledBindingType(p0));
        }
        /// <summary>
        /// ArgumentException with message like "Unhandled convert from decimal to {0}"
        /// </summary>
        internal static Exception UnhandledConvertFromDecimal(object p0) {
            return new ArgumentException(Strings.UnhandledConvertFromDecimal(p0));
        }
        /// <summary>
        /// ArgumentException with message like "Unhandled convert to decimal from {0}"
        /// </summary>
        internal static Exception UnhandledConvertToDecimal(object p0) {
            return new ArgumentException(Strings.UnhandledConvertToDecimal(p0));
        }
        /// <summary>
        /// ArgumentException with message like "Unhandled Expression Type: {0}"
        /// </summary>
        internal static Exception UnhandledExpressionType(object p0) {
            return new ArgumentException(Strings.UnhandledExpressionType(p0));
        }
        /// <summary>
        /// ArgumentException with message like "Unhandled member access: {0}"
        /// </summary>
        internal static Exception UnhandledMemberAccess(object p0) {
            return new ArgumentException(Strings.UnhandledMemberAccess(p0));
        }
         /// <summary>
        /// ArgumentException with message like "Unknown binding type"
        /// </summary>
        internal static Exception UnknownBindingType() {
            return new ArgumentException(Strings.UnknownBindingType);
        }
          /// <summary>
        /// ArgumentException with message like "An incorrect number of type args were specified for the declaration of a Func type."
        /// </summary>
        internal static Exception IncorrectNumberOfTypeArgsForFunc() {
            return new ArgumentException(Strings.IncorrectNumberOfTypeArgsForFunc);
        }
        /// <summary>
        /// ArgumentException with message like "An incorrect number of type args were specified for the declaration of an Action type."
        /// </summary>
        internal static Exception IncorrectNumberOfTypeArgsForAction() {
            return new ArgumentException(Strings.IncorrectNumberOfTypeArgsForAction);
        }
        /// <summary>
        /// ArgumentException with message like "Argument type cannot be System.Void."
        /// </summary>
        internal static Exception ArgumentCannotBeOfTypeVoid() {
            return new ArgumentException(Strings.ArgumentCannotBeOfTypeVoid);
        }

        /// <summary>
        /// The exception that is thrown when a null reference (Nothing in Visual Basic) is passed to a method that does not accept it as a valid argument.
        /// </summary>
        internal static Exception ArgumentNull(string paramName) {
            return new ArgumentNullException(paramName);
        }
        
        /// <summary>
        /// The exception that is thrown when the value of an argument is outside the allowable range of values as defined by the invoked method.
        /// </summary>
        internal static Exception ArgumentOutOfRange(string paramName) {
            return new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// The exception that is thrown when the author has yet to implement the logic at this point in the program. This can act as an exception based TODO tag.
        /// </summary>
        internal static Exception NotImplemented() {
            return new NotImplementedException();
        }

        /// <summary>
        /// The exception that is thrown when an invoked method is not supported, or when there is an attempt to read, seek, or write to a stream that does not support the invoked functionality. 
        /// </summary>
        internal static Exception NotSupported() {
            return new NotSupportedException();
        }        
    }
}

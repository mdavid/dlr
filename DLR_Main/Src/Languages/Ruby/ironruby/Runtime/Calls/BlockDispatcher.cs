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
using Microsoft.Scripting;
using Microsoft.Scripting.Actions;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using Microsoft.Linq.Expressions;
using System.Reflection;
using IronRuby.Builtins;
using System.Collections.ObjectModel;

namespace IronRuby.Runtime.Calls {
    using Ast = Microsoft.Linq.Expressions.Expression;
    using AstFactory = IronRuby.Compiler.Ast.AstFactory;
    using System.Collections;

    [Flags]
    public enum BlockSignatureAttributes {
        None = 0,

        // {|(...)|}
        HasSingleCompoundParameter = 1,

        // {|*|}
        // {|...,*|}
        HasUnsplatParameter = 2,

        // bits 31..3 store arity (might be different from formal parameter count)
    }

    public delegate object BlockCallTarget0(BlockParam param, object self);
    public delegate object BlockCallTarget1(BlockParam param, object self, object arg1);
    public delegate object BlockCallTarget2(BlockParam param, object self, object arg1, object arg2);
    public delegate object BlockCallTarget3(BlockParam param, object self, object arg1, object arg2, object arg3);
    public delegate object BlockCallTarget4(BlockParam param, object self, object arg1, object arg2, object arg3, object arg4);
    public delegate object BlockCallTargetN(BlockParam param, object self, object[] args);
    public delegate object BlockCallTargetUnsplatN(BlockParam param, object self, object[] args, RubyArray/*!*/ array);
    
    public abstract class BlockDispatcher {
        private readonly BlockSignatureAttributes _attributesAndArity;

        public bool HasSingleCompoundParameter {
            get { return (_attributesAndArity & BlockSignatureAttributes.HasSingleCompoundParameter) != 0; }
        }

        public bool HasUnsplatParameter {
            get { return (_attributesAndArity & BlockSignatureAttributes.HasUnsplatParameter) != 0; }
        }

        public int Arity {
            get { return ((int)_attributesAndArity >> 2); }
        }

        internal static BlockSignatureAttributes MakeAttributes(BlockSignatureAttributes attributes, int arity) {
            return attributes | (BlockSignatureAttributes)(arity << 2);
        }

        // Doesn't include unsplat parameter. 
        // Includes anonymous parameter.
        public abstract int ParameterCount { get; }
        public abstract Delegate/*!*/ Method { get; }

        public abstract object Invoke(BlockParam/*!*/ param, object self);
        public abstract object InvokeNoAutoSplat(BlockParam/*!*/ param, object self, object arg1);
        public abstract object Invoke(BlockParam/*!*/ param, object self, object arg1);
        public abstract object Invoke(BlockParam/*!*/ param, object self, object arg1, object arg2);
        public abstract object Invoke(BlockParam/*!*/ param, object self, object arg1, object arg2, object arg3);
        public abstract object Invoke(BlockParam/*!*/ param, object self, object arg1, object arg2, object arg3, object arg4);
        public abstract object Invoke(BlockParam/*!*/ param, object self, object[]/*!*/ args);

        public abstract object InvokeSplat(BlockParam/*!*/ param, object self, object splattee);
        public abstract object InvokeSplat(BlockParam/*!*/ param, object self, object arg1, object splattee);
        public abstract object InvokeSplat(BlockParam/*!*/ param, object self, object arg1, object arg2, object splattee);
        public abstract object InvokeSplat(BlockParam/*!*/ param, object self, object arg1, object arg2, object arg3, object splattee);
        public abstract object InvokeSplat(BlockParam/*!*/ param, object self, object arg1, object arg2, object arg3, object arg4, object splattee);
        public abstract object InvokeSplat(BlockParam/*!*/ param, object self, object[]/*!*/ args, object splattee);

        public abstract object InvokeSplatRhs(BlockParam/*!*/ param, object self, object[]/*!*/ args, object splattee, object rhs);

        internal const int MaxBlockArity = 4;
        internal const int HiddenParameterCount = 2;

        internal BlockDispatcher(BlockSignatureAttributes attributesAndArity) {
            _attributesAndArity = attributesAndArity;
        }

        internal static BlockDispatcher/*!*/ Create(Delegate/*!*/ method, int parameterCount, BlockSignatureAttributes attributesAndArity) {
            if ((attributesAndArity & BlockSignatureAttributes.HasUnsplatParameter) == 0) {
                switch (parameterCount) {
                    case 0: return new BlockDispatcher0((BlockCallTarget0)method, attributesAndArity);
                    case 1: return new BlockDispatcher1((BlockCallTarget1)method, attributesAndArity);
                    case 2: return new BlockDispatcher2((BlockCallTarget2)method, attributesAndArity);
                    case 3: return new BlockDispatcher3((BlockCallTarget3)method, attributesAndArity);
                    case 4: return new BlockDispatcher4((BlockCallTarget4)method, attributesAndArity);
                    default: return new BlockDispatcherN((BlockCallTargetN)method, parameterCount, attributesAndArity);
                }
            }

            return new BlockDispatcherUnsplatN((BlockCallTargetUnsplatN)method, parameterCount, attributesAndArity);
        }

        internal static LambdaExpression/*!*/ CreateLambda(Expression body, string name, ReadOnlyCollection<ParameterExpression> parameters,
            int parameterCount, BlockSignatureAttributes attributes) {
            if ((attributes & BlockSignatureAttributes.HasUnsplatParameter) == 0) {
                switch (parameterCount) {
                    case 0: return Ast.Lambda<BlockCallTarget0>(body, name, parameters);
                    case 1: return Ast.Lambda<BlockCallTarget1>(body, name, parameters);
                    case 2: return Ast.Lambda<BlockCallTarget2>(body, name, parameters);
                    case 3: return Ast.Lambda<BlockCallTarget3>(body, name, parameters);
                    case 4: return Ast.Lambda<BlockCallTarget4>(body, name, parameters);
                    default: return Ast.Lambda<BlockCallTargetN>(body, name, parameters);
                }
            }
            return Ast.Lambda<BlockCallTargetUnsplatN>(body, name, parameters);
        }

        private static void CopyArgumentsFromSplattee(object[]/*!*/ args, int initializedArgCount, int parameterCount, 
            out int nextArg, out int nextItem, object splattee) {

            int i = Math.Min(initializedArgCount, parameterCount);
            int j = 0;
            var list = splattee as IList;
            if (list != null) {
                while (i < parameterCount && j < list.Count) {
                    args[i++] = list[j++];
                }
            } else if (i < parameterCount) {
                args[i++] = splattee;
                j++;
            }

            nextArg = i;
            nextItem = j;
        }

        // Expects first "initializeArgCount" slots of "args" array initialized with actual argument values 
        // and fills the rest by splatting "splattee". The size of the array "args" is the number of formal parameters the block takes.
        internal static object[]/*!*/ CopyArgumentsFromSplattee(object[]/*!*/ args, int initializedArgCount, object splattee) {
            int nextArg, nextItem;
            CopyArgumentsFromSplattee(args, initializedArgCount, args.Length, out nextArg, out nextItem, splattee);
            return args;
        }

        internal static void CreateArgumentsFromSplattee(int parameterCount, out int nextArg, out int nextItem, ref object[]/*!*/ args, object splattee) {
            // the args array is passed to the block, we need at least space for all explicit parameters:
            int originalLength = args.Length;
            if (args.Length < parameterCount) {
                Array.Resize(ref args, parameterCount);
            }

            CopyArgumentsFromSplattee(args, originalLength, parameterCount, out nextArg, out nextItem, splattee);
        }

        internal static object[]/*!*/ CreateArgumentsFromSplatteeAndRhs(int parameterCount, object[]/*!*/ args, object splattee, object rhs) {
            int nextArg, nextItem;

            // the args array is passed to the block, we need at least space for all explicit parameters:
            CreateArgumentsFromSplattee(parameterCount, out nextArg, out nextItem, ref args, splattee);

            if (nextArg < args.Length) {
                args[nextArg++] = rhs;
            }

            return args;
        }

#if OBSOLETE
        private Expression/*!*/ AddWarning(Expression/*!*/ codeContextExpression, Expression/*!*/ expression) {
            Assert.NotNull(codeContextExpression, expression);

            // do not report warning if the only parameter is a nested left value:
            if (FirstArgumentIsNestedLValue) {
                return expression;
            }

            return Methods.MultipleValuesForBlockParameterWarning", codeContextExpression, expression);
        }

        private void SetCallRuleArguments(
            Expression/*!*/ blockParameterExpression, // special arg #0
            Expression/*!*/ selfParameterExpression,  // special arg #1
            CallArguments/*!*/ args,                  // user args
            Expression/*!*/ codeContextExpression,
            MetaObjectBuilder/*!*/ rule, 
            ArgsBuilder/*!*/ actualArgs) {

            // mandatory args:
            actualArgs.Add(blockParameterExpression);
            actualArgs.Add(selfParameterExpression);

            int parameterIndex = 0;

            // mimics CompoundLeftValue.TransformWrite //

            // L(1,-)?
            bool leftOneNone = OptionalParamCount == 1 && !HasParamsArray;

            // L(0,*)?
            bool leftNoneSplat = OptionalParamCount == 0 && HasParamsArray;

            // R(0,*)?
            bool rightNoneSplat = !args.Signature.IsSimple && args.Length == 1 && args.GetArgumentKind(0) == ArgumentKind.List;

            // R(1,-)?
            bool rightOneNone = !args.Signature.IsSimple && args.Length == 1 && args.GetArgumentKind(0) == ArgumentKind.Simple
                || args.Signature.IsSimple && args.Length == 1;

            // R(1,*)?
            bool rightOneSplat = !args.Signature.IsSimple && args.Length == 2 &&
                args.GetArgumentKind(0) == ArgumentKind.Simple &&
                args.GetArgumentKind(1) == ArgumentKind.List;

            // R(0,-)?
            bool rightNoneNone = args.Length == 0;

            if (leftOneNone) {
                Expression rvalue;

                if (rightOneNone) {
                    // simple assignment
                    rvalue = args.Expressions[parameterIndex];
                } else if (rightOneSplat && TestEmptyList(rule, args.Values[parameterIndex + 1], args.Expressions[parameterIndex + 1])) {
                    // simple assignment if the splatted value is an empty array:
                    rvalue = args.Expressions[parameterIndex];
                } else if (rightNoneNone) {
                    // nil assignment
                    rvalue = AddWarning(codeContextExpression, AstUtils.Constant(null));
                } else if (rightNoneSplat) {
                    // Splat(RHS[*]):
                    rvalue = MakeArgumentSplatWithWarning(rule, args.Values[parameterIndex], args.Expressions[parameterIndex], codeContextExpression);
                } else {
                    // more than one argument -> pack to an array + warning

                    // MakeArray(RHS) + SplatAppend(RHS*):
                    List<Expression> arguments = new List<Expression>();
                    AddBlockArguments(rule, arguments, args, parameterIndex);
                    rvalue = AddWarning(codeContextExpression, ArgsBuilder.MakeArgsArray(arguments));
                }

                actualArgs.Add(rvalue);

            } else {

                // R(0,*) || R(1,-) && !L(0,*) ==> CompoundLeftValue.TransformWrite does Unsplat, MakeArray otherwise.
                // 
                // However, we are not constructing a materalized resulting array (contrary to CompoundLeftValue.TransformWrite).
                // The resulting array is comprised of slots on the stack (loaded to the formal parameters of the block #1, ..., #n).
                // Therefore, we effectively need to take items of imaginary Unsplat's result and put them into the actualArgs as arguments.
                //
                // Unsplat of x makes an array containing x if x is not an array, otherwise it returns x.
                // So, we just need to take elements of x and push them onto the stack.
                //

                List<Expression> arguments = new List<Expression>();

                if (rightNoneSplat) {
                    ArgsBuilder.SplatListToArguments(rule, arguments, args.Values[parameterIndex], args.Expressions[parameterIndex], false);
                } else if (rightOneNone && !leftNoneSplat) {
                    ArgsBuilder.SplatListToArguments(rule, arguments, args.Values[parameterIndex], args.Expressions[parameterIndex], true);
                } else {
                    AddBlockArguments(rule, arguments, args, parameterIndex);
                }

                actualArgs.AddRange(arguments);
            }

            actualArgs.AddForEachMissingArgument(delegate() { return AstUtils.Constant(null); });

            if (HasParamsArray) {
                actualArgs.AddParamsArray();
            }
        }

        private bool TestEmptyList(MetaObjectBuilder/*!*/ rule, object arg, Expression/*!*/ parameter) {
            int listLength;
            ParameterExpression listVariable;
            return ArgsBuilder.AddTestForListArg(rule, arg, parameter, out listLength, out listVariable) && listLength == 0;
        }

        private Expression/*!*/ MakeArgumentSplatWithWarning(MetaObjectBuilder/*!*/ rule, object arg, Expression/*!*/ parameter,
            Expression/*!*/ codeContextExpression) {

            int listLength;
            ParameterExpression listVariable;
            if (ArgsBuilder.AddTestForListArg(rule, arg, parameter, out listLength, out listVariable)) {
                if (listLength == 0) {
                    // return nil argument + Warning
                    return AddWarning(codeContextExpression, AstUtils.Constant(null));
                } else if (listLength == 1) {
                    // return the only item of the array:
                    return Ast.Call(
                        listVariable,
                        typeof(IList).GetMethod("get_Item"),
                        AstUtils.Constant(0)
                    );
                } else {
                    // return the array itself + Warning:
                    return AddWarning(codeContextExpression, parameter);
                }
            } else {
                // not an array, return the value:
                return parameter;
            }
        }

        private Expression/*!*/ MakeArgumentUnsplat(MetaObjectBuilder/*!*/ rule, object arg, Expression/*!*/ parameter) {
            int listLength;
            ParameterExpression listVariable;
            if (ArgsBuilder.AddTestForListArg(rule, arg, parameter, out listLength, out listVariable)) {
                // an array, return:
                return parameter;
            } else {
                // not an array, wrap:
                return AstFactory.OptimizedOpCall("MakeArray", parameter);
            }
        }

        private void AddBlockArguments(MetaObjectBuilder/*!*/ rule, List<Expression>/*!*/ actualArgs, CallArguments/*!*/ args, int parameterIndex) {

            while (parameterIndex < args.Length) {
                switch (args.GetArgumentKind(parameterIndex)) {
                    case ArgumentKind.Simple:
                        actualArgs.Add(args.Expressions[parameterIndex]);
                        break;

                    case ArgumentKind.List:
                        ArgsBuilder.SplatListToArguments(rule, actualArgs, args.Values[parameterIndex], args.Expressions[parameterIndex], false);
                        break;

                    case ArgumentKind.Instance:
                    case ArgumentKind.Block:
                    default:
                        throw new NotImplementedException();
                }

                parameterIndex++;
            }
        }
#endif
    }
}

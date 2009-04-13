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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Scripting;
using Microsoft.Linq.Expressions;
using System.Reflection;
using System.Text;
using IronRuby.Builtins;
using IronRuby.Compiler;
using Microsoft.Scripting.Actions;
using Microsoft.Scripting.Actions.Calls;
using Microsoft.Scripting.Generation;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using Ast = Microsoft.Linq.Expressions.Expression;
using AstUtils = Microsoft.Scripting.Ast.Utils;

namespace IronRuby.Runtime.Calls {
    internal sealed class RubyOverloadResolver : OverloadResolver {
        private readonly CallArguments/*!*/ _args;
        private readonly MetaObjectBuilder/*!*/ _metaBuilder;
        private readonly SelfCallConvention _callConvention;

        private int _firstRestrictedArg;
        private int _lastSplattedArg;
        private ParameterExpression _listVariable;
        private IList _list;

        public RubyContext/*!*/ Context {
            get { return _args.RubyContext; }
        }

        public Expression/*!*/ ScopeExpression {
            get { return _args.MetaScope.Expression; }
        }

        public Expression/*!*/ ContextExpression {
            get { return _args.MetaContext.Expression; }
        }

        public RubyOverloadResolver(MetaObjectBuilder/*!*/ metaBuilder, CallArguments/*!*/ args, SelfCallConvention callConvention)
            : base(args.RubyContext.Binder) {
            _args = args;
            _metaBuilder = metaBuilder;
            _callConvention = callConvention;
        }

        #region Step 1: Special Parameters

        protected override int PrepareParametersBinding(ParameterInfo/*!*/[]/*!*/ parameterInfos, List<ArgBuilder>/*!*/ arguments,
            List<ParameterWrapper>/*!*/ parameters, ref int index) {

            // Ruby library method signatures:
            // static/factories: [(CallSiteStorage)*, (RubyContext|RubyScope)?, (BlockParam)?, self, args]

            var i = 0;

            while (i < parameterInfos.Length
                && parameterInfos[i].ParameterType.IsSubclassOf(typeof(RubyCallSiteStorage))) {

                arguments.Add(new RubyCallSiteStorageBuilder(parameterInfos[i]));
                i++;
            }

            if (i < parameterInfos.Length) {
                var parameterInfo = parameterInfos[i];

                if (parameterInfo.ParameterType == typeof(RubyScope)) {
                    arguments.Add(new RubyScopeArgBuilder(parameterInfo));
                    i++;
                } else if (parameterInfo.ParameterType == typeof(RubyContext)) {
                    arguments.Add(new RubyContextArgBuilder(parameterInfo));
                    i++;
                }
            }

            // If the method overload doesn't have a BlockParam parameter, we inject MissingBlockParam parameter and arg builder.
            // The parameter is treated as a regular explicit mandatory parameter.
            //
            // The argument builder provides no value for the actual argument expression, which makes the default binder to skip it
            // when emitting a tree for the actual method call (this is necessary since the method doesn't in fact have the parameter).
            // 
            // By injecting the missing block parameter we achieve that all overloads have either BlockParam, [NotNull]BlockParam or 
            // MissingBlockParam parameter. MissingBlockParam and BlockParam are convertible to each other. Default binder prefers 
            // those overloads where no conversion needs to happen, which ensures the desired semantics:
            //
            //                                        conversions with desired priority (the less number the higher priority)
            // Parameters:                call w/o block      call with non-null block       call with null block
            // (implicit, MBP, ... )      MBP -> MBP (1)            BP -> MBP (3)               BP -> MBP (2)
            // (implicit, BP,  ... )      MBP -> BP  (2)            BP -> BP  (2)               BP -> BP  (1)
            // (implicit, BP!, ... )          N/A                   BP -> BP! (1)                  N/A    
            //
            if (i < parameterInfos.Length && parameterInfos[i].ParameterType == typeof(BlockParam)) {
                var info = parameterInfos[i];
                arguments.Add(new SimpleArgBuilder(info, index++));
                parameters.Add(new ParameterWrapper(info, info.ParameterType, info.Name, CompilerHelpers.ProhibitsNull(info), false, false, true));
                i++;
            } else if (i >= parameterInfos.Length || parameterInfos[i].ParameterType != typeof(BlockParam)) {
                arguments.Add(new MissingBlockArgBuilder(index++));
                parameters.Add(new ParameterWrapper(null, typeof(MissingBlockParam), null, false, false, false, true));
            }

            // TODO: wrong error messages
            //if (_callConvention == SelfCallConvention.SelfIsParameter) {
            //    Debug.Assert(i < parameterInfos.Length);

            //    var info = parameterInfos[i];
            //    var method = (MethodBase)info.Member;
            //    Debug.Assert(method.IsStatic);
            //    arguments.Add(new SimpleArgBuilder(info, index++));
            //    parameters.Add(new ParameterWrapper(info, info.ParameterType, info.Name, true, false, false, true));
            //    i++;
            //}

            return i;
        }

        internal static void GetParameterCount(ParameterInfo/*!*/[]/*!*/ parameterInfos, out int mandatory, out int optional, out bool acceptsBlock) {
            acceptsBlock = false;
            mandatory = 0;
            optional = 0;
            foreach (ParameterInfo parameterInfo in parameterInfos) {
                if (IsHiddenParameter(parameterInfo)) {
                    continue;
                } else if (parameterInfo.ParameterType == typeof(BlockParam)) {
                    acceptsBlock = true;
                } else if (CompilerHelpers.IsParamArray(parameterInfo)) {
                    // TODO: indicate splat args separately?
                    optional++;
                } else if (CompilerHelpers.IsOutParameter(parameterInfo)) {
                    // Python allows passing of optional "clr.Reference" to capture out parameters
                    // Ruby should allow similar
                    optional++;
                } else if (CompilerHelpers.IsMandatoryParameter(parameterInfo)) {
                    mandatory++;
                } else {
                    optional++;
                }
            }
        }

        internal static bool IsHiddenParameter(ParameterInfo/*!*/ parameterInfo) {
            return parameterInfo.ParameterType == typeof(RubyScope)
                || parameterInfo.ParameterType == typeof(RubyContext)
                || parameterInfo.ParameterType.IsSubclassOf(typeof(RubyCallSiteStorage));
        }

        #endregion

        #region Step 2: Actual Arguments

        private static readonly DynamicMetaObject NullMetaBlockParam =
            new DynamicMetaObject(
                AstUtils.Constant(null, typeof(BlockParam)),
                BindingRestrictions.Empty,
                null
            );

        // Creates actual/normalized arguments: inserts self, expands splats, and inserts rhs arg. 
        // Adds any restrictions/conditions applied to the arguments to the given meta-builder.
        protected override ActualArguments CreateActualArguments(IList<DynamicMetaObject> namedArgs, IList<string> argNames, int preSplatLimit, int postSplatLimit) {
            var result = new List<DynamicMetaObject>();

            // self (instance):
            if (_callConvention == SelfCallConvention.SelfIsInstance) {
                result.Add(_args.MetaTarget);
            }

            if (_args.Signature.HasBlock) {
                if (_args.GetBlock() == null) {
                    // the user explicitly passed nil as a block arg:
                    result.Add(NullMetaBlockParam);
                } else {
                    // pass BlockParam:
                    if (_metaBuilder.BfcVariable == null) {
                        // we add temporary even though we might not us it if the calee doesn't have block param arg:
                        _metaBuilder.BfcVariable = _metaBuilder.GetTemporary(typeof(BlockParam), "#bfc");
                    }
                    result.Add(new DynamicMetaObject(_metaBuilder.BfcVariable, BindingRestrictions.Empty));
                }
            } else {
                // no block passed into a method with a BlockParam:
                result.Add(MissingBlockParam.Meta.Instance);
            }

            // self (parameter):
            if (_callConvention == SelfCallConvention.SelfIsParameter) {
                result.Add(_args.MetaTarget);
            }

            // the next argument is the first one for which we use restrictions coming from overload resolution:
            _firstRestrictedArg = result.Count;

            return CreateActualArguments(result, _metaBuilder, _args, preSplatLimit, postSplatLimit, out _lastSplattedArg, out _list, out _listVariable);
        }

        public static IList<DynamicMetaObject/*!*/> NormalizeArguments(MetaObjectBuilder/*!*/ metaBuilder, CallArguments/*!*/ args, int minCount, int maxCount) {
            int lastSplattedArg;
            IList list;
            ParameterExpression listVariable;

            var actualArgs = CreateActualArguments(new List<DynamicMetaObject>(), metaBuilder, args, maxCount, maxCount,
                out lastSplattedArg, out list, out listVariable);

            int actualCount = actualArgs.Count + actualArgs.CollapsedCount;

            if (actualCount < minCount) {
                metaBuilder.SetWrongNumberOfArgumentsError(actualCount, minCount);
                return null;
            } else if (actualCount > maxCount) {
                metaBuilder.SetWrongNumberOfArgumentsError(actualCount, maxCount);
                return null;
            }

            // any collapsed args are out of limits:
            return actualArgs.Arguments;
        }

        private static ActualArguments/*!*/ CreateActualArguments(List<DynamicMetaObject>/*!*/ normalized, MetaObjectBuilder/*!*/ metaBuilder,
            CallArguments/*!*/ args, int preSplatLimit, int postSplatLimit, out int lastSplattedArg, out IList list, out ParameterExpression listVariable) {

            int firstSplattedArg, splatIndex, collapsedArgCount;

            // simple arguments:
            for (int i = 0; i < args.SimpleArgumentCount; i++) {
                normalized.Add(args.GetSimpleMetaArgument(i));
            }

            // splat argument:
            list = null;
            listVariable = null;
            if (args.Signature.HasSplattedArgument) {
                firstSplattedArg = normalized.Count;

                int listLength;
                var splatted = args.GetSplattedMetaArgument();
                if (metaBuilder.AddSplattedArgumentTest(splatted.Value, splatted.Expression, out listLength, out listVariable)) {

                    // AddTestForListArg only returns 'true' if the argument is a List<object>
                    list = (IList)splatted.Value;

                    int i = 0;
                    while (i < Math.Min(listLength, preSplatLimit - firstSplattedArg)) {
                        normalized.Add(MakeSplattedItem(list, listVariable, i));
                        i++;
                    }

                    // skip items that are not needed for overload resolution
                    splatIndex = normalized.Count;

                    i = Math.Max(i, listLength - (postSplatLimit - (args.Signature.HasRhsArgument ? 1 : 0)));
                    while (i < listLength) {
                        normalized.Add(MakeSplattedItem(list, listVariable, i));
                        i++;
                    }

                    collapsedArgCount = listLength - (normalized.Count - firstSplattedArg);
                } else {
                    // argument is not an array => add the argument itself:
                    normalized.Add(splatted);
                    listLength = 1;
                    splatIndex = -1;
                    collapsedArgCount = 0;
                }

                lastSplattedArg = normalized.Count - 1;
            } else {
                splatIndex = firstSplattedArg = lastSplattedArg = -1;
                collapsedArgCount = 0;
            }

            Debug.Assert(collapsedArgCount >= 0);

            // rhs argument:
            if (args.Signature.HasRhsArgument) {
                normalized.Add(args.GetRhsMetaArgument());
            }

            return new ActualArguments(
                normalized.ToArray(),
                DynamicMetaObject.EmptyMetaObjects,
                ArrayUtils.EmptyStrings,
                1, // one hidden argument: block
                collapsedArgCount,
                firstSplattedArg,
                splatIndex
            );
        }

        internal static DynamicMetaObject/*!*/ MakeSplattedItem(IList/*!*/ list, Expression/*!*/ listVariable, int index) {
            return DynamicMetaObject.Create(
                list[index],
                Ast.Call(listVariable, typeof(IList).GetMethod("get_Item"), AstUtils.Constant(index))
            );
        }

        #endregion

        #region Step 3: Restrictions

        internal void AddArgumentRestrictions(MetaObjectBuilder/*!*/ metaBuilder, BindingTarget/*!*/ bindingTarget) {
            var args = GetActualArguments();
            var restrictedArgs = bindingTarget.Success ? bindingTarget.RestrictedArguments.Objects : args.Arguments;

            for (int i = _firstRestrictedArg; i < restrictedArgs.Count; i++) {
                var arg = (bindingTarget.Success ? restrictedArgs[i] : restrictedArgs[i].Restrict(restrictedArgs[i].GetLimitType()));

                if (i >= args.FirstSplattedArg && i <= _lastSplattedArg) {
                    metaBuilder.AddCondition(arg.Restrictions.ToExpression());
                } else {
                    metaBuilder.AddRestriction(arg.Restrictions);
                }
            }

            // Adds condition for collapsed arguments - it is the same whether we succeed or not:
            var splatCondition = GetCollapsedArgsCondition();
            if (splatCondition != null) {
                metaBuilder.AddCondition(splatCondition);
            }
        }

        #endregion

        #region Step 4: Argument Building, Conversions

        public override bool CanConvertFrom(Type/*!*/ fromType, ParameterWrapper/*!*/ toParameter, NarrowingLevel level) {
            Type toType = toParameter.Type;

            if (base.CanConvertFrom(fromType, toParameter, level)) {
                return true;
            }

            // blocks:
            if (fromType == typeof(MissingBlockParam)) {
                return toType == typeof(BlockParam) && !toParameter.ProhibitNull;
            }

            if (fromType == typeof(BlockParam) && toType == typeof(MissingBlockParam)) {
                return true;
            }

            // protocol conversions:
            if (toParameter.ParameterInfo != null && toParameter.ParameterInfo.IsDefined(typeof(DefaultProtocolAttribute), false) &&
                // default protocol doesn't apply on param-array/dict itself, only on the expanded parameters:
                !toParameter.IsParamsArray) {

                // any type is potentially convertible, except for nil if [NotNull] is used or the target type is a value type:
                return fromType != typeof(DynamicNull) || !(toParameter.ProhibitNull || toType.IsValueType);
            }

            return false;
        }

        public override Candidate SelectBestConversionFor(Type/*!*/ actualType, ParameterWrapper/*!*/ candidateOne, ParameterWrapper/*!*/ candidateTwo, NarrowingLevel level) {
            Type typeOne = candidateOne.Type;
            Type typeTwo = candidateTwo.Type;

            if (actualType == typeof(DynamicNull)) {
                // if nil is passed as a block argument prefer BlockParam over missing block;
                if (typeOne == typeof(BlockParam) && typeTwo == typeof(MissingBlockParam)) {
                    return Candidate.One;
                }

                if (typeOne == typeof(MissingBlockParam) && typeTwo == typeof(BlockParam)) {
                    return Candidate.Two;
                }
            } else {
                if (actualType == typeOne && candidateOne.ProhibitNull) {
                    return Candidate.One;
                }

                if (actualType == typeTwo && candidateTwo.ProhibitNull) {
                    return Candidate.Two;
                }
            }

            if (actualType == typeOne) {
                return Candidate.One;
            }

            if (actualType == typeTwo) {
                return Candidate.Two;
            }


            return Candidate.Equivalent;
        }

        public override Expression/*!*/ ConvertExpression(Expression/*!*/ expr, ParameterInfo info, Type/*!*/ toType) {
            Type fromType = expr.Type;

            // block:
            if (fromType == typeof(MissingBlockParam)) {
                Debug.Assert(toType == typeof(BlockParam) || toType == typeof(MissingBlockParam));
                return AstUtils.Constant(null);
            }

            if (fromType == typeof(BlockParam) && toType == typeof(MissingBlockParam)) {
                return AstUtils.Constant(null);
            }

            // protocol conversions:
            if (info != null && info.IsDefined(typeof(DefaultProtocolAttribute), false)) {
                var action = RubyConversionAction.TryGetDefaultConversionAction(Context, toType);
                if (action != null) {
                    // TODO: once we work with MetaObjects, we could inline these dynamic sites:
                    return Ast.Dynamic(action, toType, expr);
                }

                throw new InvalidOperationException(String.Format("No default protocol conversion for type {0}.", toType));
            }

            return Binder.ConvertExpression(expr, toType, ConversionResultKind.ExplicitCast, null);
        }

        protected override Expression/*!*/ GetSplattedExpression() {
            return _listVariable;
        }

        protected override object GetSplattedItem(int index) {
            return _list[index];
        }

        internal sealed class RubyContextArgBuilder : ArgBuilder {
            public RubyContextArgBuilder(ParameterInfo/*!*/ info)
                : base(info) {
            }

            public override int Priority {
                get { return -1; }
            }

            protected override Expression ToExpression(OverloadResolver/*!*/ resolver, IList<Expression>/*!*/ parameters, bool[]/*!*/ hasBeenUsed) {
                return ((RubyOverloadResolver)resolver).ContextExpression;
            }
        }

        internal sealed class RubyCallSiteStorageBuilder : ArgBuilder {
            public RubyCallSiteStorageBuilder(ParameterInfo/*!*/ info)
                : base(info) {
            }

            public override int Priority {
                get { return -1; }
            }

            protected override Expression ToExpression(OverloadResolver/*!*/ resolver, IList<Expression>/*!*/ parameters, bool[]/*!*/ hasBeenUsed) {
                return AstUtils.Constant(Activator.CreateInstance(ParameterInfo.ParameterType, ((RubyOverloadResolver)resolver).Context));
            }
        }

        internal sealed class RubyScopeArgBuilder : ArgBuilder {
            public RubyScopeArgBuilder(ParameterInfo/*!*/ info)
                : base(info) {
            }

            public override int Priority {
                get { return -1; }
            }

            protected override Expression ToExpression(OverloadResolver/*!*/ resolver, IList<Expression>/*!*/ parameters, bool[]/*!*/ hasBeenUsed) {
                return ((RubyOverloadResolver)resolver).ScopeExpression;
            }
        }

        internal sealed class MissingBlockArgBuilder : SimpleArgBuilder {
            public MissingBlockArgBuilder(int index)
                : base(typeof(MissingBlockParam), index, false, false) {
            }

            public override int Priority {
                get { return -1; }
            }

            protected override SimpleArgBuilder/*!*/ Copy(int newIndex) {
                return new MissingBlockArgBuilder(newIndex);
            }

            protected override Expression ToExpression(OverloadResolver/*!*/ resolver, IList<Expression>/*!*/ parameters, bool[]/*!*/ hasBeenUsed) {
                Debug.Assert(Index < parameters.Count);
                Debug.Assert(Index < hasBeenUsed.Length);
                Debug.Assert(parameters[Index] != null);
                hasBeenUsed[Index] = true;
                return null;
            }
        }

        #endregion

        #region Setp 5: Errors

        public override Microsoft.Scripting.Actions.ErrorInfo MakeInvalidParametersError(BindingTarget target) {
            Expression exceptionValue;
            switch (target.Result) {
                case BindingResult.AmbiguousMatch:
                    exceptionValue = MakeAmbiguousCallError(target);
                    break;
                case BindingResult.IncorrectArgumentCount:
                    exceptionValue = MakeIncorrectArgumentCountError(target);
                    break;
                case BindingResult.CallFailure:
                    exceptionValue = MakeCallFailureError(target);
                    break;
                default: throw new InvalidOperationException();
            }
            return Microsoft.Scripting.Actions.ErrorInfo.FromException(exceptionValue);
        }

        private Expression MakeAmbiguousCallError(BindingTarget target) {
            StringBuilder sb = new StringBuilder(string.Format("Found multiple methods for '{0}': ", target.Name));
            string outerComma = "";
            foreach (MethodCandidate candidate in target.AmbiguousMatches) {
                Type[] types = candidate.GetParameterTypes();
                string innerComma = "";

                sb.Append(outerComma);
                sb.Append(target.Name);
                sb.Append('(');
                foreach (Type t in types) {
                    sb.Append(innerComma);
                    sb.Append(Binder.GetTypeName(t));
                    innerComma = ", ";
                }

                sb.Append(')');
                outerComma = ", ";
            }

            return Methods.MakeAmbiguousMatchError.OpCall(AstUtils.Constant(sb.ToString()));
        }

        private Expression MakeIncorrectArgumentCountError(BindingTarget target) {
            IList<int> available = target.ExpectedArgumentCount;
            int expected;

            if (available.Count > 0) {
                int minGreater = Int32.MaxValue;
                int maxLesser = Int32.MinValue;
                int max = Int32.MinValue;
                foreach (int arity in available) {
                    if (arity > target.ActualArgumentCount) {
                        minGreater = Math.Min(minGreater, arity);
                    } else {
                        maxLesser = Math.Max(maxLesser, arity);
                    }

                    max = Math.Max(max, arity);
                }

                expected = (target.ActualArgumentCount < maxLesser ? maxLesser : Math.Min(minGreater, max));
            } else {
                // no overload is callable:
                expected = 0;
            }

            return Methods.MakeWrongNumberOfArgumentsError.OpCall(AstUtils.Constant(target.ActualArgumentCount), AstUtils.Constant(expected));
        }

        private Expression MakeCallFailureError(BindingTarget target) {
            foreach (CallFailure cf in target.CallFailures) {
                switch (cf.Reason) {
                    case CallFailureReason.ConversionFailure:
                        foreach (ConversionResult cr in cf.ConversionResults) {
                            if (cr.Failed) {
                                if (typeof(Proc).IsAssignableFrom(cr.To)) {
                                    return Methods.CreateArgumentsErrorForProc.OpCall(AstUtils.Constant(Binder.GetTypeName(cr.From)));
                                }

                                Debug.Assert(typeof(BlockParam).IsSealed);
                                if (cr.To == typeof(BlockParam)) {
                                    Debug.Assert(cr.From == typeof(MissingBlockParam));
                                    return Methods.CreateArgumentsErrorForMissingBlock.OpCall();
                                }

                                return Methods.CreateTypeConversionError.OpCall(
                                        AstUtils.Constant(Binder.GetTypeName(cr.From)),
                                        AstUtils.Constant(Binder.GetTypeName(cr.To)));
                            }
                        }
                        break;

                    case CallFailureReason.DuplicateKeyword:
                    case CallFailureReason.UnassignableKeyword:
                    default: throw new InvalidOperationException();
                }
            }
            throw new InvalidOperationException();
        }

        #endregion
    }
}

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
using System.Linq.Expressions;
#else
using System; using Microsoft;
using Microsoft.Linq.Expressions;
#endif
using System.Reflection;
using System.Runtime.CompilerServices;
#if !CODEPLEX_40
using Microsoft.Runtime.CompilerServices;
#endif

#if CODEPLEX_40
using System.Dynamic;
#else
using Microsoft.Scripting;
#endif
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using AstUtils = Microsoft.Scripting.Ast.Utils;

namespace Microsoft.Scripting.Actions {
#if CODEPLEX_40
    using Ast = System.Linq.Expressions.Expression;
#else
    using Ast = Microsoft.Linq.Expressions.Expression;
#endif

    public partial class DefaultBinder : ActionBinder {

        /// <summary>
        /// Builds a MetaObject for performing a member get.  Supports all built-in .NET members, the OperatorMethod 
        /// GetBoundMember, and StrongBox instances.
        /// </summary>
        /// <param name="name">
        /// The name of the member to retrieve.  This name is not processed by the DefaultBinder and
        /// is instead handed off to the GetMember API which can do name mangling, case insensitive lookups, etc...
        /// </param>
        /// <param name="target">
        /// The MetaObject from which the member is retrieved.
        /// </param>
        public DynamicMetaObject GetMember(string name, DynamicMetaObject target) {
            return GetMember(name, target, AstUtils.Constant(null, typeof(CodeContext)));
        }

        /// <summary>
        /// Builds a MetaObject for performing a member get.  Supports all built-in .NET members, the OperatorMethod 
        /// GetBoundMember, and StrongBox instances.
        /// </summary>
        /// <param name="name">
        /// The name of the member to retrieve.  This name is not processed by the DefaultBinder and
        /// is instead handed off to the GetMember API which can do name mangling, case insensitive lookups, etc...
        /// </param>
        /// <param name="target">
        /// The MetaObject from which the member is retrieved.
        /// </param>
        /// <param name="codeContext">
        /// An expression which provides access to the CodeContext if its required for 
        /// accessing the member (e.g. for an extension property which takes CodeContext).  By default this
        /// a null CodeContext object is passed.
        /// </param>
        public DynamicMetaObject GetMember(string name, DynamicMetaObject target, Expression codeContext) {
            return GetMember(
                name,
                target,
                codeContext,
                false,
                null
            );
        }

        /// <summary>
        /// Builds a MetaObject for performing a member get.  Supports all built-in .NET members, the OperatorMethod 
        /// GetBoundMember, and StrongBox instances.
        /// </summary>
        /// <param name="name">
        /// The name of the member to retrieve.  This name is not processed by the DefaultBinder and
        /// is instead handed off to the GetMember API which can do name mangling, case insensitive lookups, etc...
        /// </param>
        /// <param name="target">
        /// The MetaObject from which the member is retrieved.
        /// </param>
        /// <param name="codeContext">
        /// An expression which provides access to the CodeContext if its required for 
        /// accessing the member (e.g. for an extension property which takes CodeContext).  By default this
        /// a null CodeContext object is passed.
        /// </param>
        /// <param name="isNoThrow">
        /// True if the operation should return Operation.Failed on failure, false if it
        /// should return the exception produced by MakeMissingMemberError.
        /// </param>
        /// <param name="errorSuggestion">
        /// The meta object to be used if the get results in an error.
        /// </param>
        public DynamicMetaObject GetMember(string name, DynamicMetaObject target, Expression codeContext, bool isNoThrow, DynamicMetaObject errorSuggestion) {
            ContractUtils.RequiresNotNull(name, "name");
            ContractUtils.RequiresNotNull(target, "target");
            ContractUtils.RequiresNotNull(codeContext, "codeContext");

            return MakeGetMemberTarget(
                new GetMemberInfo(
                    name,
                    codeContext,
                    isNoThrow,
                    errorSuggestion
                ),
                target
            );
        }

        private DynamicMetaObject MakeGetMemberTarget(GetMemberInfo getMemInfo, DynamicMetaObject target) {
            Type type = target.GetLimitType();
            BindingRestrictions restrictions = target.Restrictions;
            DynamicMetaObject self = target;
            target = target.Restrict(target.GetLimitType());

            // Specially recognized types: TypeTracker, NamespaceTracker, and StrongBox.  
            // TODO: TypeTracker and NamespaceTracker should technically be IDO's.
            MemberGroup members = MemberGroup.EmptyGroup;
            if (typeof(TypeTracker).IsAssignableFrom(type)) {
                restrictions = restrictions.Merge(
                    BindingRestrictions.GetInstanceRestriction(target.Expression, target.Value)
                );

                TypeGroup tg = target.Value as TypeGroup;
                Type nonGen;
                if (tg == null || tg.TryGetNonGenericType(out nonGen)) {
                    members = GetMember(MemberRequestKind.Get, ((TypeTracker)target.Value).Type, getMemInfo.Name);
                    if (members.Count > 0) {
                        // we have a member that's on the type associated w/ the tracker, return that...
                        type = ((TypeTracker)target.Value).Type;
                        self = null;
                    }
                }
            }

            if (members.Count == 0) {
                // Get the members
                members = GetMember(MemberRequestKind.Get, type, getMemInfo.Name);
            }

            if (members.Count == 0) {
                if (typeof(TypeTracker).IsAssignableFrom(type)) {
                    // ensure we don't have a non-generic type, and if we do report an error now.  This matches
                    // the rule version of the default binder but should probably be removed long term
                    Type x = ((TypeTracker)target.Value).Type;
                } else if (type.IsInterface) {
                    // all interfaces have object members
                    type = typeof(object);
                    members = GetMember(MemberRequestKind.Get, type, getMemInfo.Name);
                }
            }

            Expression propSelf = self == null ? null : self.Expression;
            // if lookup failed try the strong-box type if available.
            if (members.Count == 0 && typeof(IStrongBox).IsAssignableFrom(type) && propSelf != null) {
                // properties/fields need the direct value, methods hold onto the strong box.
                propSelf = Ast.Field(AstUtils.Convert(propSelf, type), type.GetField("Value"));

                type = type.GetGenericArguments()[0];

                members = GetMember(
                    MemberRequestKind.Get,
                    type,
                    getMemInfo.Name
                );
            }

            MakeBodyHelper(getMemInfo, self, propSelf, type, members);

            getMemInfo.Body.Restrictions = restrictions;
            return getMemInfo.Body.GetMetaObject(target);
        }

        private void MakeBodyHelper(GetMemberInfo getMemInfo, DynamicMetaObject self, Expression propSelf, Type type, MemberGroup members) {
            if (self != null) {
                MakeOperatorGetMemberBody(getMemInfo, propSelf, type, "GetCustomMember");
            }

            Expression error;
            TrackerTypes memberType = GetMemberType(members, out error);

            if (error == null) {
                MakeSuccessfulMemberAccess(getMemInfo, self, propSelf, type, members, memberType);
            } else {
                getMemInfo.Body.FinishCondition(getMemInfo.ErrorSuggestion != null ? getMemInfo.ErrorSuggestion.Expression : error);
            }
        }

        private void MakeSuccessfulMemberAccess(GetMemberInfo getMemInfo, DynamicMetaObject self, Expression propSelf, Type type, MemberGroup members, TrackerTypes memberType) {
            switch (memberType) {
                case TrackerTypes.TypeGroup:
                case TrackerTypes.Type:
                    MakeTypeBody(getMemInfo, type, members);
                    break;
                case TrackerTypes.Method:
                    // turn into a MethodGroup                    
                    MakeGenericBodyWorker(getMemInfo, type, ReflectionCache.GetMethodGroup(getMemInfo.Name, members), self == null ? null : self.Expression);
                    break;
                case TrackerTypes.Event:
                case TrackerTypes.Field:
                case TrackerTypes.Property:
                case TrackerTypes.Constructor:
                case TrackerTypes.Custom:
                    MakeGenericBody(getMemInfo, type, members, propSelf);
                    break;
                case TrackerTypes.All:
                    // no members were found
                    if (self != null) {
                        MakeOperatorGetMemberBody(getMemInfo, propSelf, type, "GetBoundMember");
                    }

                    MakeMissingMemberRuleForGet(getMemInfo, self, type);
                    break;
                default:
                    throw new InvalidOperationException(memberType.ToString());
            }
        }

        private void MakeGenericBody(GetMemberInfo getMemInfo, Type type, MemberGroup members, Expression instance) {
            MemberTracker bestMember = members[0];
            if (members.Count > 1) {
                // if we were given multiple members pick the member closest to the type...                
                Type bestMemberDeclaringType = members[0].DeclaringType;

                for (int i = 1; i < members.Count; i++) {
                    MemberTracker mt = members[i];
                    if (!IsTrackerApplicableForType(type, mt)) {
                        continue;
                    }

                    if (members[i].DeclaringType.IsSubclassOf(bestMemberDeclaringType) ||
                        !IsTrackerApplicableForType(type, bestMember)) {
                        bestMember = members[i];
                        bestMemberDeclaringType = members[i].DeclaringType;
                    }
                }
            }

            MakeGenericBodyWorker(getMemInfo, type, bestMember, instance);
        }

        private static bool IsTrackerApplicableForType(Type type, MemberTracker mt) {
            return mt.DeclaringType == type || type.IsSubclassOf(mt.DeclaringType);
        }

        private void MakeTypeBody(GetMemberInfo getMemInfo, Type type, MemberGroup members) {
            TypeTracker typeTracker = (TypeTracker)members[0];
            for (int i = 1; i < members.Count; i++) {
                typeTracker = TypeGroup.UpdateTypeEntity(typeTracker, (TypeTracker)members[i]);
            }

            getMemInfo.Body.FinishCondition(typeTracker.GetValue(getMemInfo.CodeContext, this, type));
        }

        private void MakeGenericBodyWorker(GetMemberInfo getMemInfo, Type type, MemberTracker tracker, Expression instance) {
            if (instance != null) {
                tracker = tracker.BindToInstance(instance);
            }

            Expression val = tracker.GetValue(getMemInfo.CodeContext, this, type);

            if (val != null) {
                getMemInfo.Body.FinishCondition(val);
            } else {
                ErrorInfo ei = tracker.GetError(this);
                if (ei.Kind != ErrorInfoKind.Success && getMemInfo.IsNoThrow) {
                    getMemInfo.Body.FinishCondition(MakeOperationFailed());
                } else {
                    getMemInfo.Body.FinishCondition(MakeError(tracker.GetError(this), typeof(object)));
                }
            }
        }

        /// <summary> if a member-injector is defined-on or registered-for this type call it </summary>
        private void MakeOperatorGetMemberBody(GetMemberInfo getMemInfo, Expression instance, Type type, string name) {
            MethodInfo getMem = GetMethod(type, name);
            if (getMem != null && getMem.IsSpecialName) {
                ParameterExpression tmp = Ast.Variable(typeof(object), "getVal");
                getMemInfo.Body.AddVariable(tmp);

                getMemInfo.Body.AddCondition(
                    Ast.NotEqual(
                        Ast.Assign(
                            tmp,
                            MakeCallExpression(
                                getMemInfo.CodeContext,
                                getMem,
                                AstUtils.Convert(instance, type),
                                AstUtils.Constant(getMemInfo.Name)
                            )
                        ),
                        Ast.Field(null, typeof(OperationFailed).GetField("Value"))
                    ),
                    tmp
                );
            }
        }

        private void MakeMissingMemberRuleForGet(GetMemberInfo getMemInfo, DynamicMetaObject self, Type type) {
            if (getMemInfo.ErrorSuggestion != null) {
                getMemInfo.Body.FinishCondition(getMemInfo.ErrorSuggestion.Expression);
            } else if (getMemInfo.IsNoThrow) {
                getMemInfo.Body.FinishCondition(MakeOperationFailed());
            } else {
                getMemInfo.Body.FinishCondition(
                    MakeError(MakeMissingMemberError(type, self, getMemInfo.Name), typeof(object))
                );
            }
        }

        private static MemberExpression MakeOperationFailed() {
            return Ast.Field(null, typeof(OperationFailed).GetField("Value"));
        }


        /// <summary>
        /// Helper class for flowing information about the GetMember request.
        /// </summary>
        private sealed class GetMemberInfo {
            public readonly string Name;
            public readonly Expression CodeContext;
            public readonly bool IsNoThrow;
            public readonly ConditionalBuilder Body = new ConditionalBuilder();
            public readonly DynamicMetaObject ErrorSuggestion;

            public GetMemberInfo(string name, Expression codeContext, bool noThrow, DynamicMetaObject errorSuggestion) {
                Name = name;
                CodeContext = codeContext;
                IsNoThrow = noThrow;
                ErrorSuggestion = errorSuggestion;
            }
        }
    }
}

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
using System.Diagnostics;
using Microsoft.Linq.Expressions;
using System.Reflection;
using Microsoft.Scripting;

using Microsoft.Scripting.Utils;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Actions;

using IronRuby.Builtins;
using IronRuby.Compiler;

using Ast = Microsoft.Linq.Expressions.Expression;
using AstUtils = Microsoft.Scripting.Ast.Utils;
using AstFactory = IronRuby.Compiler.Ast.AstFactory;
using IronRuby.Compiler.Generation;
   
namespace IronRuby.Runtime.Calls {

    public class RubyCallAction : DynamicMetaObjectBinder, IEquatable<RubyCallAction>, IExpressionSerializable {
        private readonly RubyCallSignature _signature;
        private readonly string/*!*/ _methodName;

        public RubyCallSignature Signature {
            get { return _signature; }
        }

        public string/*!*/ MethodName {
            get { return _methodName; }
        }

        protected RubyCallAction(string/*!*/ methodName, RubyCallSignature signature) {
            Assert.NotNull(methodName);
            _methodName = methodName;
            _signature = signature;
        }

        public static RubyCallAction/*!*/ Make(string/*!*/ methodName, int argumentCount) {
            return Make(methodName, RubyCallSignature.Simple(argumentCount));
        }

        [Emitted]
        public static RubyCallAction/*!*/ Make(string/*!*/ methodName, RubyCallSignature signature) {
            return new RubyCallAction(methodName, signature);
        }

        public override object/*!*/ CacheIdentity {
            get { return this; }
        }

        #region Object Overrides, IEquatable

        public override int GetHashCode() {
            return _methodName.GetHashCode() ^ _signature.GetHashCode() ^ GetType().GetHashCode();
        }

        public override bool Equals(object obj) {
            return Equals(obj as RubyCallAction);
        }

        public override string/*!*/ ToString() {
            return _methodName + _signature.ToString();
        }

        public bool Equals(RubyCallAction other) {
            return other != null && _methodName == other._methodName && _signature.Equals(other._signature) && other.GetType() == GetType();
        }

        #endregion

        #region IExpressionSerializable Members

        Expression/*!*/ IExpressionSerializable.CreateExpression() {
            return Expression.Call(
                Methods.GetMethod(typeof(RubyCallAction), "Make", typeof(string), typeof(RubyCallSignature)),
                Expression.Constant(_methodName),
                _signature.CreateExpression()
            );
        }

        #endregion

        public override DynamicMetaObject/*!*/ Bind(DynamicMetaObject/*!*/ context, DynamicMetaObject/*!*/[]/*!*/ args) {
            var mo = new MetaObjectBuilder();
            Bind(mo, _methodName, new CallArguments(context, args, _signature));
            return mo.CreateMetaObject(this, context, args);
        }

        /// <exception cref="MissingMethodException">The resolved method is Kernel#method_missing.</exception>
        internal static void Bind(MetaObjectBuilder/*!*/ metaBuilder, string/*!*/ methodName, CallArguments/*!*/ args) {
            RubyClass targetClass = args.RubyContext.GetImmediateClassOf(args.Target);
            RubyMemberInfo method, methodMissing = null;
            RubyMethodVisibility incompatibleVisibility = RubyMethodVisibility.None;

            using (targetClass.Context.ClassHierarchyLocker()) {
                metaBuilder.AddTargetTypeTest(args.Target, targetClass, args.TargetExpression, args.RubyContext, args.ContextExpression);

                method = targetClass.ResolveMethodForSiteNoLock(methodName, args.Signature.HasImplicitSelf, out incompatibleVisibility);
                if (method == null) {
                    if (args.Signature.IsTryCall) {
                        // TODO: this shouldn't throw. We need to fix caching of non-existing methods.
                        throw new MissingMethodException();
                        // metaBuilder.Result = Ast.Constant(Fields.RubyOps_MethodNotFound);
                    } else {
                        methodMissing = targetClass.ResolveMethodForSiteNoLock(Symbols.MethodMissing, true);
                    }
                }
            }

            if (method != null) {
                method.BuildCall(metaBuilder, args, methodName);
            } else {
                // insert the method name argument into the args
                object symbol = SymbolTable.StringToId(methodName);
                args.InsertSimple(0, new DynamicMetaObject(Ast.Constant(symbol), BindingRestrictions.Empty, symbol));

                BindToMethodMissing(metaBuilder, args, methodName, methodMissing, incompatibleVisibility);
            }
        }

        internal static void BindToMethodMissing(MetaObjectBuilder/*!*/ metaBuilder, CallArguments/*!*/ args, string/*!*/ methodName,
            RubyMemberInfo methodMissing, RubyMethodVisibility incompatibleVisibility) {
            // Assumption: args already contain method name.
            
            // TODO: better check for builtin method
            if (methodMissing == null ||
                methodMissing.DeclaringModule == methodMissing.Context.KernelModule && methodMissing is RubyLibraryMethodInfo) {

                // throw an exception immediately, do not cache the rule:
                if (incompatibleVisibility == RubyMethodVisibility.Private) {
                    throw RubyExceptions.CreatePrivateMethodCalled(args.RubyContext, args.Target, methodName);
                } else {
                    throw RubyExceptions.CreateMethodMissing(args.RubyContext, args.Target, methodName);
                }
            }

            methodMissing.BuildCall(metaBuilder, args, methodName);
        }
    }
}

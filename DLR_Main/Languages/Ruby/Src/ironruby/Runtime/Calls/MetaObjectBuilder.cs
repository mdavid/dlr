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
using System.Collections.Generic;
using Microsoft.Scripting;

using Microsoft.Scripting.Actions;
using Microsoft.Scripting.Utils;

using Ast = Microsoft.Linq.Expressions.Expression;
using AstFactory = IronRuby.Compiler.Ast.AstFactory;
using AstUtils = Microsoft.Scripting.Ast.Utils;
using IronRuby.Builtins;
using IronRuby.Compiler;

namespace IronRuby.Runtime.Calls {
    public sealed class MetaObjectBuilder {
        private Expression _condition;
        private Expression _restriction;
        private Expression _result;
        private List<ParameterExpression> _temps;
        private bool _error;
        private bool _treatRestrictionsAsConditions;

        internal MetaObjectBuilder() {
        }

        public bool Error {
            get { return _error; }
        }

        public Expression Result {
            get { return _result; }
            set { _result = value; }
        }

        public ParameterExpression BfcVariable { get; set; }

        public bool TreatRestrictionsAsConditions {
            get { return _treatRestrictionsAsConditions; }
            set { _treatRestrictionsAsConditions = value; }
        }

        internal MetaObject/*!*/ CreateMetaObject(MetaObjectBinder/*!*/ action, MetaObject/*!*/ context, MetaObject/*!*/[]/*!*/ args) {
            return CreateMetaObject(action, ArrayUtils.Insert(context, args));
        }

        internal MetaObject/*!*/ CreateMetaObject(MetaObjectBinder/*!*/ action, MetaObject/*!*/[]/*!*/ siteArgs) {
            var expr = _error ? Ast.Throw(_result) : _result;

            Restrictions restrictions;
            if (_condition != null) {
                var deferral = action.Defer(siteArgs);
                expr = Ast.Condition(_condition, AstUtils.Convert(expr, typeof(object)), deferral.Expression);
                restrictions = deferral.Restrictions;
            } else {
                restrictions = Restrictions.Empty;
            }

            if (_temps != null) {
                expr = Ast.Block(_temps, expr);
            }

            if (_restriction != null) {
                restrictions = restrictions.Merge(Restrictions.GetExpressionRestriction(_restriction));
            }

            return new MetaObject(expr, restrictions);
        }

        public void SetError(Expression/*!*/ expression) {
            Assert.NotNull(expression);
            Debug.Assert(!_error, "Error already set");

            _result = expression;
            _error = true;
        }

        public void SetWrongNumberOfArgumentsError(int actual, int expected) {
            SetError(Methods.MakeWrongNumberOfArgumentsError.OpCall(Ast.Constant(actual), Ast.Constant(expected)));
        }

        public void AddCondition(Expression/*!*/ condition) {
            Assert.NotNull(condition);
            _condition = (_condition != null) ? Ast.AndAlso(_condition, condition) : condition;
        }

        public void AddRestriction(Expression/*!*/ restriction) {
            Assert.NotNull(restriction);
            if (_treatRestrictionsAsConditions) {
                AddCondition(restriction);
            } else {
                _restriction = (_restriction != null) ? Ast.AndAlso(_restriction, restriction) : restriction;
            }
        }

        public static Expression/*!*/ GetObjectTypeTestExpression(object value, Expression/*!*/ expression) {
            if (value == null) {
                return Ast.Equal(expression, Ast.Constant(null));
            } else {
                return RuleBuilder.MakeTypeTestExpression(value.GetType(), expression);
            }
        }

        public void AddTypeRestriction(Type/*!*/ type, Expression/*!*/ expression) {
            AddRestriction(RuleBuilder.MakeTypeTestExpression(type, expression));
        }

        public void AddObjectTypeRestriction(object value, Expression/*!*/ expression) {
            if (value == null) {
                AddRestriction(Ast.Equal(expression, Ast.Constant(null)));
            } else {
                AddTypeRestriction(value.GetType(), expression);
            }
        }

        public void AddObjectTypeCondition(object value, Expression/*!*/ expression) {
            AddCondition(GetObjectTypeTestExpression(value, expression));
        }

        public void AddTargetTypeTest(CallArguments/*!*/ args) {
            AddTargetTypeTest(args.Target, args.TargetExpression, args.RubyContext, args.ContextExpression);
        }

        // TODO: do not test runtime for runtime bound sites
        // TODO: ResolveMethod invalidates modules that were not initialized yet -> snapshot version after method resolution
        // TODO: thread safety: synchronize version snapshot and method resolution
        public void AddTargetTypeTest(object target, Expression/*!*/ targetParameter, RubyContext/*!*/ context, Expression/*!*/ contextExpression) {

            // singleton nil:
            if (target == null) {
                AddRestriction(Ast.Equal(targetParameter, Ast.Constant(null)));
                context.NilClass.AddFullVersionTest(this, contextExpression);
                return;
            }

            // singletons true, false:
            if (target is bool) {
                AddRestriction(Ast.AndAlso(
                    Ast.TypeIs(targetParameter, typeof(bool)),
                    Ast.Equal(Ast.Convert(targetParameter, typeof(bool)), Ast.Constant(target))
                ));

                if ((bool)target) {
                    context.TrueClass.AddFullVersionTest(this, contextExpression);
                } else {
                    context.FalseClass.AddFullVersionTest(this, contextExpression);
                }
                return;

            }

            RubyClass immediateClass = context.GetImmediateClassOf(target);

            // user defined instance singletons, modules, classes:
            if (immediateClass.IsSingletonClass) {
                AddRestriction(
                    Ast.Equal(
                        Ast.Convert(targetParameter, typeof(object)),
                        Ast.Convert(Ast.Constant(target), typeof(object))
                    )
                );

                // we need to check for a runtime (e.g. "foo" .NET string instance could be shared accross runtimes):
                immediateClass.AddFullVersionTest(this, contextExpression);
                return;
            }

            Type type = target.GetType();
            AddTypeRestriction(type, targetParameter);
            
            if (typeof(IRubyObject).IsAssignableFrom(type)) {
                // Ruby objects (get the method directly to prevent interface dispatch):
                MethodInfo classGetter = type.GetMethod("get_" + RubyObject.ClassPropertyName, BindingFlags.Public | BindingFlags.Instance);
                if (classGetter != null && classGetter.ReturnType == typeof(RubyClass)) {
                    AddCondition(
                        // (#{type})target.Class.Version == #{immediateClass.Version}
                        Ast.Equal(
                            Ast.Call(Ast.Call(Ast.Convert(targetParameter, type), classGetter), RubyModule.VersionProperty.GetGetMethod()),
                            Ast.Constant(immediateClass.Version)
                        )
                    );
                    return;
                }

                // TODO: explicit iface-implementation
                throw new NotSupportedException("Type implementing IRubyObject should have RubyClass getter");
            } else {
                // CLR objects:
                immediateClass.AddFullVersionTest(this, contextExpression);
            }
        }

        internal bool AddSplattedArgumentTest(object value, Expression/*!*/ expression, out int listLength, out ParameterExpression/*!*/ listVariable) {
            if (value == null) {
                AddRestriction(Ast.Equal(expression, Ast.Constant(null)));
            } else {
                // test exact type:
                AddTypeRestriction(value.GetType(), expression);

                List<object> list = value as List<object>;
                if (list != null) {
                    Type type = typeof(List<object>);
                    listLength = list.Count;
                    listVariable = GetTemporary(type, "#list");
                    AddCondition(Ast.Equal(
                        Ast.Property(Ast.Assign(listVariable, Ast.Convert(expression, type)), type.GetProperty("Count")),
                        Ast.Constant(list.Count))
                    );
                    return true;
                }
            }

            listLength = -1;
            listVariable = null;
            return false;
        }

        public ParameterExpression/*!*/ GetTemporary(Type/*!*/ type, string/*!*/ name) {
            if (_temps == null) {
                _temps = new List<ParameterExpression>();
            }

            var variable = Ast.Variable(type, name);
            _temps.Add(variable);
            return variable;
        }
    }
}

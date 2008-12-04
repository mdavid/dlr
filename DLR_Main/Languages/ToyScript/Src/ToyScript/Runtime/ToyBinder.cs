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

using System; using Microsoft;
using System.Collections.Generic;
using Microsoft.Linq.Expressions;
using Microsoft.Scripting.Actions;
using Microsoft.Scripting.Generation;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Actions.Calls;
using AstUtils = Microsoft.Scripting.Ast.Utils;

namespace ToyScript.Runtime {
    class ToyBinder : DefaultBinder {
        public ToyBinder(ScriptDomainManager manager)
            : base(manager) {
        }

        protected override void MakeRule(OldDynamicAction action, object[] args, RuleBuilder rule) {
            object[] extracted;
            CodeContext cc = ExtractCodeContext(args, out extracted);

            //
            // Try IOldDynamicObject
            //
            IOldDynamicObject ido = extracted[0] as IOldDynamicObject;
            if (ido != null) {
                if (ido.GetRule(action, cc, extracted, rule)) {
                    return;
                }
            }

            //
            // Try ToyScript rules
            //
            if (action.Kind == DynamicActionKind.DoOperation) {
                if (MakeDoRule((OldDoOperationAction)action, extracted, rule)) {
                    return;
                }
            }

            //
            // Fall back to DLR default rules
            //
            base.MakeRule(action, args, rule);
        }

        private bool MakeDoRule(OldDoOperationAction action, object[] args, RuleBuilder rule) {
            if (action.Operation == Operators.Add &&
                args[0] is string &&
                args[1] is string) {

                // (arg0 is string && args1 is string)
                rule.Test = Expression.AndAlso(
                    Expression.TypeIs(
                        rule.Parameters[0],
                        typeof(string)
                    ),
                    Expression.TypeIs(
                        rule.Parameters[1],
                        typeof(string)
                    )
                );

                // string.Concat(string str0, string str1);
                rule.Target =
                    rule.MakeReturn(this,
                        Expression.Call(
                            typeof(string).GetMethod("Concat", new Type[] { typeof(string), typeof(string) }),
                            Expression.Convert(rule.Parameters[0], typeof(string)),
                            Expression.Convert(rule.Parameters[1], typeof(string))
                        )
                    );

                return true;
            }

            return false;
        }

        #region ActionBinder overrides

        public override bool CanConvertFrom(Type fromType, Type toType, bool toNotNullable, NarrowingLevel level) {
            return toType.IsAssignableFrom(fromType);
        }

        public override Candidate PreferConvert(Type t1, Type t2) {
            throw new NotImplementedException();
        }

        public override Expression ConvertExpression(Expression expr, Type toType, ConversionResultKind kind, Expression context) {
            return AstUtils.Convert(expr, toType);
        }

        public override IList<Type> GetExtensionTypes(Type t) {
            if (t == typeof(string)) {
                return new Type[] { typeof(StringExtensions) };
            }
            return Type.EmptyTypes;
        }

        #endregion
    }
}

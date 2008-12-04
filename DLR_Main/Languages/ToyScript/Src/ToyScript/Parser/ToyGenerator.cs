/* ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation. 
 *
 * This source code is subject to terms and conditions of the Microsoft Public License. A 
 * copy of the license can be found in the License.html file at the root of this distribution. If 
 * you cannot locate the  Microsoft Public License, please send an email to 
 * ironpy@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
 * by the terms of the Microsoft Public License.
 *
 * You must not remove this notice, or any other, from this software.
 *
 *
 * ***************************************************************************/

using System; using Microsoft;
using Microsoft.Linq.Expressions;
using Microsoft.Scripting;
using Microsoft.Scripting.Actions;
using Microsoft.Scripting.Ast;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using ToyScript.Binders;
using ToyScript.Parser.Ast;

namespace ToyScript.Parser {
    class ToyGenerator {
        private readonly ToyLanguageContext _tlc;
        private ToyScope _scope;

        private ToyGenerator(ToyLanguageContext tlc, SourceUnit sourceUnit) {
            _tlc = tlc;
            PushNewScope(sourceUnit.Path ?? "<toyblock>", sourceUnit.Document);
        }

        internal ToyLanguageContext Tlc {
            get { return _tlc; }
        }

        internal ToyScope Scope {
            get {
                return _scope;
            }
        }

        internal ToyScope PushNewScope(string name, SymbolDocumentInfo document) {
            return _scope = new ToyScope(_scope, name, document);
        }

        internal void PopScope() {
            _scope = _scope.Parent;
        }

        internal Expression LookupName(string name) {
            return _scope.LookupName(name);
        }

        internal Expression GetOrMakeLocal(string name) {
            return _scope.GetOrMakeLocal(name);
        }

        internal Expression GetOrMakeGlobal(string name) {
            return _scope.TopScope.GetOrMakeLocal(name);
        }

        internal static LambdaExpression Generate(ToyLanguageContext tlc, Statement statement, SourceUnit sourceUnit) {
            ToyGenerator tg = new ToyGenerator(tlc, sourceUnit);

            Expression body = statement.Generate(tg);

            return tg.Scope.FinishScope(body);
        }

        internal static bool UseNewBinders = false;

        internal Expression ConvertTo(Type type, Expression expression) {
            if (UseNewBinders) {
                return Expression.Dynamic(Binder.Convert(type), type, expression);
            } else {
                return Expression.Dynamic(
                    OldConvertToAction.Make(_tlc.Binder, typeof(bool), ConversionResultKind.ExplicitCast),
                    typeof(bool),
                    Utils.CodeContext(),
                    expression
                );
            }
        }

        internal Expression Call(Expression target, Expression[] arguments) {
            if (UseNewBinders) {
                return Expression.Dynamic(
                    Binder.Call(),
                    typeof(object),
                    ArrayUtils.Insert(target, arguments)
                );
            } else {
                return Expression.Dynamic(
                    OldCallAction.Make(_tlc.Binder, arguments.Length),
                    typeof(object),
                    ArrayUtils.Insert(
                        Utils.CodeContext(),
                        target,
                        arguments
                   )
                );
            }
        }

        internal Expression GetMember(string member, Expression target) {
            if (UseNewBinders) {
                return Expression.Dynamic(
                    Binder.GetMember(member),
                    typeof(object),
                    target
                );
            } else {
                return Expression.Dynamic(
                    OldGetMemberAction.Make(_tlc.Binder, member),
                    typeof(object),
                    Utils.CodeContext(),
                    target
                );
            }
        }

        internal Expression Operator(Operators op, Expression left, Expression right) {
            if (UseNewBinders) {
                return Expression.Dynamic(
                    Binder.Operation(op),
                    typeof(object),
                    left,
                    right
                );
            } else {
                return Expression.Dynamic(
                    OldDoOperationAction.Make(_tlc.Binder, op),
                    typeof(object),
                    Utils.CodeContext(),
                    left,
                    right
                );
            }
        }

        internal Expression SetItem(Expression target, Expression index, Expression right) {
            if (UseNewBinders) {
                return Expression.Dynamic(
                    Binder.Operation(Operators.SetItem),
                    typeof(object),
                    target,
                    index,
                    right
                );
            } else {
                return Expression.Dynamic(
                    OldDoOperationAction.Make(_tlc.Binder, Operators.SetItem),
                    typeof(object),
                    Utils.CodeContext(),
                    target,
                    index,
                    right
                );
            }
        }

        internal Expression Add(Expression left, Expression right) {
            if (UseNewBinders) {
                return Expression.Dynamic(Binder.Operation(Operators.Add), typeof(object), left, right);
            } else {
                return Expression.Dynamic(OldDoOperationAction.Make(_tlc.Binder, Operators.Add), typeof(object), Utils.CodeContext(), left, right);
            }
        }

        internal Expression Subtract(Expression left, Expression right) {
            if (UseNewBinders) {
                return Expression.Dynamic(Binder.Operation(Operators.Subtract), typeof(object), left, right);
            } else {
                return Expression.Dynamic(OldDoOperationAction.Make(_tlc.Binder, Operators.Subtract), typeof(object), Utils.CodeContext(), left, right);
            }
        }

        internal Expression Multiply(Expression left, Expression right) {
            if (UseNewBinders) {
                return Expression.Dynamic(Binder.Operation(Operators.Multiply), typeof(object), left, right);
            } else {
                return Expression.Dynamic(OldDoOperationAction.Make(_tlc.Binder, Operators.Multiply), typeof(object), Utils.CodeContext(), left, right);
            }
        }

        internal Expression Divide(Expression left, Expression right) {
            if (UseNewBinders) {
                return Expression.Dynamic(Binder.Operation(Operators.Divide), typeof(object), left, right);
            } else {
                return Expression.Dynamic(OldDoOperationAction.Make(_tlc.Binder, Operators.Divide), typeof(object), Utils.CodeContext(), left, right);
            }
        }

        internal Expression LessThan(Expression left, Expression right) {
            if (UseNewBinders) {
                return Expression.Dynamic(Binder.Operation(Operators.LessThan), typeof(object), left, right);
            } else {
                return Expression.Dynamic(OldDoOperationAction.Make(_tlc.Binder, Operators.LessThan), typeof(object), Utils.CodeContext(), left, right);
            }
        }

        internal Expression LessThanOrEqual(Expression left, Expression right) {
            if (UseNewBinders) {
                return Expression.Dynamic(Binder.Operation(Operators.LessThanOrEqual), typeof(object), left, right);
            } else {
                return Expression.Dynamic(OldDoOperationAction.Make(_tlc.Binder, Operators.LessThanOrEqual), typeof(object), Utils.CodeContext(), left, right);
            }
        }

        internal Expression GreaterThan(Expression left, Expression right) {
            if (UseNewBinders) {
                return Expression.Dynamic(Binder.Operation(Operators.GreaterThan), typeof(object), left, right);
            } else {
                return Expression.Dynamic(OldDoOperationAction.Make(_tlc.Binder, Operators.GreaterThan), typeof(object), Utils.CodeContext(), left, right);
            }
        }

        internal Expression GreaterThanOrEqual(Expression left, Expression right) {
            if (UseNewBinders) {
                return Expression.Dynamic(Binder.Operation(Operators.GreaterThanOrEqual), typeof(object), left, right);
            } else {
                return Expression.Dynamic(OldDoOperationAction.Make(_tlc.Binder, Operators.GreaterThanOrEqual), typeof(object), Utils.CodeContext(), left, right);
            }
        }

        internal Expression Equal(Expression left, Expression right) {
            if (UseNewBinders) {
                return Expression.Dynamic(Binder.Operation(Operators.Equals), typeof(object), left, right);
            } else {
                return Expression.Dynamic(OldDoOperationAction.Make(_tlc.Binder, Operators.Equals), typeof(object), Utils.CodeContext(), left, right);
            }
        }

        internal Expression NotEqual(Expression left, Expression right) {
            if (UseNewBinders) {
                return Expression.Dynamic(Binder.Operation(Operators.NotEquals), typeof(object), left, right);
            } else {
                return Expression.Dynamic(OldDoOperationAction.Make(_tlc.Binder, Operators.NotEquals), typeof(object), Utils.CodeContext(), left, right);
            }
        }

        internal Expression New(Expression target, Expression[] arguments) {
            if (UseNewBinders) {
                return Expression.Dynamic(Binder.New(), typeof(object), ArrayUtils.Insert(target, arguments));
            } else {
                return Expression.Dynamic(
                    OldCreateInstanceAction.Make(_tlc.Binder, arguments.Length),
                    typeof(object),
                    ArrayUtils.Insert(Utils.CodeContext(), target, arguments)
                );
            }
        }

        internal Expression AddSpan(SourceSpan span, Expression expression) {
            if (_scope.Document != null) {
                expression = Expression.DebugInfo(expression, _scope.Document, span.Start.Line, span.Start.Column, span.End.Line, span.End.Column);
            }
            return expression;
        }
    }
}

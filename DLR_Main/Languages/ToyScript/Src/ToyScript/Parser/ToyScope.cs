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
using System.Collections.Generic;
using Microsoft.Linq.Expressions;
using Microsoft.Scripting.Ast;

namespace ToyScript.Parser {
    class ToyScope {
        private readonly SymbolDocumentInfo _document;
        private readonly ToyScope _parent;
        private readonly LambdaBuilder _block;
        private readonly Dictionary<string, Expression> _variables = new Dictionary<string, Expression>();
        private readonly LabelTarget _return = Expression.Label(typeof(object));

        public ToyScope(ToyScope parent, string name, SymbolDocumentInfo document) {
            _block = Utils.Lambda(typeof(object), name);
            _parent = parent;
            if (parent == null) {
                _block.Global = true;
            } else if (document == null) {
                document = _parent.Document;
            }
            _document = document;
        }

        public ToyScope Parent {
            get {
                return _parent;
            }
        }

        public ToyScope TopScope {
            get {
                if (_parent == null) {
                    return this;
                } else {
                    return _parent.TopScope;
                }
            }
        }

        public SymbolDocumentInfo Document {
            get { return _document; }
        }

        public LabelTarget ReturnLabel {
            get { return _return; }
        }

        public ParameterExpression CreateParameter(string name) {
            ParameterExpression variable = _block.Parameter(typeof(object), name);
            _variables[name] = variable;
            return variable;
        }

        public Expression GetOrMakeLocal(string name) {
            return GetOrMakeLocal(name, typeof(object));
        }

        public Expression GetOrMakeLocal(string name, Type type) {
            Expression variable;
            if (_variables.TryGetValue(name, out variable)) {
                return variable;
            }
            variable = _block.Variable(type, name);
            _variables[name] = variable;
            return variable;
        }

        public Expression LookupName(string name) {
            Expression var;
            if (_variables.TryGetValue(name, out var)) {
                return var;
            }

            if (_parent != null) {
                return _parent.LookupName(name);
            } else {
                return null;
            }
        }

        public ParameterExpression HiddenVariable(Type type, string name) {
            return _block.HiddenVariable(type, name);
        }

        public LambdaExpression FinishScope(Expression body, Type lambdaType) {
            _block.Body = AddReturnLabel(body);
            return _block.MakeLambda(lambdaType);
        }

        public LambdaExpression FinishScope(Expression body) {
            _block.Body = AddReturnLabel(body);
            return _block.MakeLambda();
        }

        private Expression AddReturnLabel(Expression body) {
            if (body.Type == typeof(void)) {
                body = Expression.Block(body, Expression.Constant(null, typeof(object)));
            }
            return Expression.Label(_return, body);
        }
    }
}


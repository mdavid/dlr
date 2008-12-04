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


using Microsoft.Scripting.Utils;
using MSA = Microsoft.Linq.Expressions;

namespace IronRuby.Compiler {

    internal struct ResultOperation {
        public static readonly ResultOperation Return = new ResultOperation(null, true);
        public static readonly ResultOperation Ignore = new ResultOperation(null, false);

        private MSA.Expression _variable;
        private bool _doReturn;

        public MSA.Expression Variable { get { return _variable; } }
        public bool DoReturn { get { return _doReturn; } }
        public bool IsIgnore { get { return _variable == null && !_doReturn; } } 

        public ResultOperation(MSA.Expression variable, bool doReturn) {
            _variable = variable; 
            _doReturn = doReturn;
        }

        public static ResultOperation Store(MSA.Expression/*!*/ variable) {
            Assert.NotNull(variable);
            return new ResultOperation(variable, false);
        }
    }
}

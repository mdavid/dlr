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
using System.Diagnostics;
using Microsoft.Scripting;
using Microsoft.Scripting.Utils;

namespace IronRuby.Compiler.Ast {

    //	when <expressions>, *<array>: <body>
    public partial class WhenClause : Node {
        
        private readonly List<Expression> _comparisons; // optional
        private readonly Expression _comparisonArray;
        private readonly List<Expression> _statements;  // optional

        public List<Expression> Statements {
            get { return _statements; }
        }

        public List<Expression> Comparisons {
            get { return _comparisons; }
        }

        public Expression ComparisonArray {
            get { return _comparisonArray; }
        }

        public WhenClause(List<Expression> comparisons, Expression comparisonArray, List<Expression> statements, SourceSpan location)
            : base(location) {
            _comparisons = comparisons;
            _comparisonArray = comparisonArray;
            _statements = statements;
        }
    }
}

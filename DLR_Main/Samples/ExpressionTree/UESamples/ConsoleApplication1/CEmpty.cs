#if CODEPLEX_40
using System;
#else
using System; using Microsoft;
#endif
using System.Collections.Generic;
#if CODEPLEX_40
using System.Linq;
using System.Linq.Expressions;
#else
using Microsoft.Linq;
using Microsoft.Linq.Expressions;
#endif
using System.Text;

namespace Samples {
    class CEmpty {
        //Expression.Empty(MethodInfo, Expression[])
        static public void Empty1() {
            //<Snippet1>
            // add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            //This element defines an empty expression.
            DefaultExpression MyEmpty = Expression.Empty();

            //It can be used where an expression is expected, but no action is desired.
            var MyEmptyBlock = Expression.Block(MyEmpty);
                
            //</Snippet1>

        }
    }
}

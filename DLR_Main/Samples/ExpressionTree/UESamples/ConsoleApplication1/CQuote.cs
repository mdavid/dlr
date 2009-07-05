#if CODEPLEX_40
using System;
#else
using System; using Microsoft;
#endif
using System.Collections.Generic;
#if CODEPLEX_40
using System.Linq;
#else
using Microsoft.Linq;
#endif
using System.Text;
#if CODEPLEX_40
using System.Linq.Expressions;
#else
using Microsoft.Linq.Expressions;
#endif

namespace Samples {
    class CQuote {
        //Quote(LabelTarget)
        static public void Quote1() {
            //<Snippet1>
            // Add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            //This expression represents an expression that has a value of type expression
            Expression MyQuote = Expression.Quote(
                Expression.Lambda(
                    Expression.Constant(1)
                )
            );

            

            //The end result should be 1:
            Console.WriteLine(Expression.Lambda<Func<LambdaExpression>>(MyQuote).Compile().Invoke().Compile().DynamicInvoke());
            //</Snippet1>

            //validate sample.
            if ((int)Expression.Lambda<Func<LambdaExpression>>(MyQuote).Compile().Invoke().Compile().DynamicInvoke() != 1) throw new Exception();
        }
    }
}

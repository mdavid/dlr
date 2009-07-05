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
    class CGetDelegateType {
        //Delegate(Expression, Expression)
        static public void Delegate1() {
            //<Snippet1>
            // Add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            //This method returns the type of a delegate with the supplied number and type of arguments
            var MyDelegate = Expression.GetDelegateType(
                typeof(int),
                typeof(string)
            );

            //We should obtain a delegate type with string return type, and an int argument.
            Console.WriteLine(MyDelegate.ToString());
            //</Snippet1>

            //validate sample.
            if (MyDelegate != typeof(Func<int,string>)) throw new Exception();
        }
    }
}

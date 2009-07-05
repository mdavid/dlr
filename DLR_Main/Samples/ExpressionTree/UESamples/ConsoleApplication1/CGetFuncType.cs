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
    class CGetFuncType {
        //Func(Expression, Expression)
        static public void Func1() {
            //<Snippet1>
            // Add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            //This method returns the type of a Func delegate with the supplied number and type of arguments
            var MyFunc = Expression.GetFuncType(
                typeof(int),
                typeof(string)
            );

            //We should obtain a delegate type with string return type, and an int argument.
            Console.WriteLine(MyFunc.ToString());
            //</Snippet1>

            //validate sample.
            if (MyFunc != typeof(Func<int,string>)) throw new Exception();
        }
    }
}

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
    class CTryGetActionType {
        //Action(Expression, Expression)
        static public void Action1() {
            //<Snippet1>
            // Add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            
            Type MyAction;

            //This method sets the second argument to the type of an action delegate with the supplied number and type of arguments.
            //It returns whether it succeeded or not.
            var Result = Expression.TryGetActionType(
                new Type[]{
                    typeof(int),
                    typeof(string)
                },
                out MyAction
            );

            //We should obtain a delegate type with void return type, and int and string arguments.
            Console.WriteLine(Result);            
            Console.WriteLine(MyAction.ToString());
            //</Snippet1>

            //validate sample.
            if (MyAction != typeof(Action<int,string>)) throw new Exception();
        }
    }
}

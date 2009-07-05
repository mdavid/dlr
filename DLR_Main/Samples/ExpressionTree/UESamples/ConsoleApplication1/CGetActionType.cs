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
    class CGetActionType {
        //Action(Expression, Expression)
        static public void Action1() {
            //<Snippet1>
            // Add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            //This method returns the type of an action delegate with the supplied number and type of arguments
            var MyAction = Expression.GetActionType(
                typeof(int),
                typeof(string)
            );

            //We should obtain a delegate type with void return type, and int and string arguments.
            Console.WriteLine(MyAction.ToString());
            //</Snippet1>

            //validate sample.
            if (MyAction != typeof(Action<int,string>)) throw new Exception();
        }
    }
}

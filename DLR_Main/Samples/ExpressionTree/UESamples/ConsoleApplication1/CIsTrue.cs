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
    class CIsTrue {
        //Expression.IsTrue(MethodInfo, Expression[])
        static public void IsTrue1() {
            //<Snippet1>
            // add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            //This element defines an IsTrue expression. It searches for an IsTrue operator in the 
            //object supplied.
            Expression MyIsTrue = Expression.IsTrue(
                Expression.Constant(new System.Data.SqlTypes.SqlBoolean(true))
            );

            //The end result should be "True".
            Console.WriteLine(Expression.Lambda<Func<bool>>(MyIsTrue).Compile().Invoke());
            //</Snippet1>

            //Validate sample
            if(Expression.Lambda<Func<bool>>(MyIsTrue).Compile().Invoke()!= true) throw new Exception();

        }

        //Expression.IsTrue(MethodInfo, Expression[])
        //<Snippet2>
        public static bool MethodIsTrue2(int arg) {
            return arg != 0;
        }
        //</Snippet2>
        static public void IsTrue2() {
            //<Snippet2>
            // add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            //This element defines an IsTrue expression through a user defined operator.
            Expression MyIsTrue = Expression.IsTrue(
                Expression.Constant(0),
                ((Func<int, bool>) MethodIsTrue2).Method
            );

            //The end result should be "False".
            Console.WriteLine(Expression.Lambda<Func<bool>>(MyIsTrue).Compile().Invoke());
            //</Snippet2>

            //Validate sample
            if (Expression.Lambda<Func<bool>>(MyIsTrue).Compile().Invoke() != false) throw new Exception();

        }


    }
}

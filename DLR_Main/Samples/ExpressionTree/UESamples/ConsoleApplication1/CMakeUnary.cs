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
    class CMakeUnary {
        
        //MakeUnary(ExpressionType, Expression, Type)
        static public void MakeUnary1() {
            //<Snippet1>
            // Add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            //This expression represents a unary operation, in this case a not.
            Expression MyMakeUnary = Expression.MakeUnary(
                ExpressionType.Not,
                Expression.Constant(true),
                typeof(bool)
            );


            //The end result should be false:
            Console.WriteLine(Expression.Lambda<Func<bool>>(MyMakeUnary).Compile().Invoke());
            //</Snippet1>

            //validate sample.
            if (Expression.Lambda<Func<bool>>(MyMakeUnary).Compile().Invoke() != false) throw new Exception();
        }


        //MakeUnary(ExpressionType, Expression, Type, MethodInfo)
        //<Snippet2>
        public static bool MyNot(bool arg) {
            return !arg;
        }
        //</Snippet2>
        static public void MakeUnary2() {
            //<Snippet2>
            // Add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            //This expression represents a unary operation, in this case a not, 
            //using a user defined operator.
            Expression MyMakeUnary = Expression.MakeUnary(
                ExpressionType.Not,
                Expression.Constant(true),
                typeof(bool),
                ((Func<bool,bool>)MyNot).Method
            );


            //The end result should be false:
            Console.WriteLine(Expression.Lambda<Func<bool>>(MyMakeUnary).Compile().Invoke());
            //</Snippet2>

            //validate sample.
            if (Expression.Lambda<Func<bool>>(MyMakeUnary).Compile().Invoke() != false) throw new Exception();
        }
    }
}

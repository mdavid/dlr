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
using System.Runtime.CompilerServices;
#if !CODEPLEX_40
using Microsoft.Runtime.CompilerServices;
#endif

#if CODEPLEX_40
using System.Linq.Expressions;
#else
using Microsoft.Linq.Expressions;
#endif

namespace Samples {
    class CRuntimeVariables {
        //RuntimeVariables(ParameterExpression[])
        static public void RuntimeVariables1() {
            //<Snippet1>
            // Add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            ParameterExpression MyVariable = Expression.Variable(typeof(int));

            //This expression allows one to evaluate variables used in an expression tree
            //from outside of that expression tree.
            RuntimeVariablesExpression MyRuntimeVariables = Expression.RuntimeVariables(
                MyVariable
            );

            var MyBlock = Expression.Block(
                new ParameterExpression[] { MyVariable },
                Expression.Assign(MyVariable, Expression.Constant(43)),
                MyRuntimeVariables
            );


            //The end result should be 43:
            Console.WriteLine(Expression.Lambda<Func<IRuntimeVariables>>(MyBlock).Compile().Invoke()[0]);

            //validate sample.
            if ((int)Expression.Lambda<Func<IRuntimeVariables>>(MyBlock).Compile().Invoke()[0] != 43) throw new Exception();
        }



        //RuntimeVariables(IEnumerable<ParameterExpression>)
        static public void RuntimeVariables2() {
            //<Snippet1>
            // Add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            ParameterExpression MyVariable = Expression.Variable(typeof(int));

            //This expression allows one to evaluate variables used in an expression tree
            //from outside of that expression tree.
            RuntimeVariablesExpression MyRuntimeVariables = Expression.RuntimeVariables(
                new List<ParameterExpression>(){MyVariable}
            );

            var MyBlock = Expression.Block(
                new ParameterExpression[] { MyVariable },
                Expression.Assign(MyVariable, Expression.Constant(43)),
                MyRuntimeVariables
            );


            //The end result should be 43:
            Console.WriteLine(Expression.Lambda<Func<IRuntimeVariables>>(MyBlock).Compile().Invoke()[0]);

            //validate sample.
            if ((int)Expression.Lambda<Func<IRuntimeVariables>>(MyBlock).Compile().Invoke()[0] != 43) throw new Exception();
        }
    }
}

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
    class CConstant {
        //Expression.Constant(Object)
        static public void Constant1() {
            //<Snippet1>
            // add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            //This Expression represents a Constant value.
            Expression MyConstant = Expression.Constant(
                                        5.5
                                    );

            //Should print "5.5".
            Console.WriteLine(Expression.Lambda<Func<double>>(MyConstant).Compile().Invoke());

            //</Snippet1>

            //Validate sample
            if (Expression.Lambda<Func<double>>(MyConstant).Compile().Invoke() != 5.5) throw new Exception("");
        }

        //Expression.Constant(Object, Type)
        static public void Constant2() {
            //<Snippet1>
            // add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            //This Expression represents a Constant value.
            //The type can explicitly be given. This can be used, for example, for defining constants of a nullable type.
            Expression MyConstant = Expression.Constant(
                                        5.5,
                                        typeof(double?)
                                    );

            //Should print "5.5".
            Console.WriteLine(Expression.Lambda<Func<double?>>(MyConstant).Compile().Invoke());

            //</Snippet1>

            //Validate sample
            if (Expression.Lambda<Func<double?>>(MyConstant).Compile().Invoke() != 5.5) throw new Exception("");
        }
    }
}

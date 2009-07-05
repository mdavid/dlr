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
    class CDefault {

        //Expression.Default(Type)
        static public void Default1() {
            //<Snippet1>
            // add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            //This Expression represents the default value of a type - for example, 0 for integer, null for a string, etc.
            Expression MyDefault = Expression.Default(
                                        typeof(byte)
                                    );

            //Should print 0.
            Console.WriteLine(Expression.Lambda<Func<byte>>(MyDefault).Compile().Invoke());

            //</Snippet1>

            //Validate sample
            if (Expression.Lambda<Func<byte>>(MyDefault).Compile().Invoke() != 0) throw new Exception("");
        }
    }
}

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
    class CMakeMemberAccess {
        //MakeMemberAccess(Expression, MemberInfo)
        //<Snippet1>
        public class Index1 {
            public int X;
        }
        //</Snippet1>

        public static void MakeMemberAccess1() {
            //<Snippet1>
            // Add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            var MyInstance = new Index1();
            MyInstance.X = 5;

            //This expression represents accessing a non indexed member, for either
            //assigning or reading its value.
            MemberExpression MyMakeMemberAccess = Expression.MakeMemberAccess(
                Expression.Constant(MyInstance),
                typeof(Index1).GetMember("X")[0]
            );
            

            //The end result should 5:
            Console.WriteLine(Expression.Lambda<Func<int>>(MyMakeMemberAccess).Compile().Invoke());
            //</Snippet1>

            //Validate sample
            if (Expression.Lambda<Func<int>>(MyMakeMemberAccess).Compile().Invoke() != 5) throw new Exception();
        }

    }
}

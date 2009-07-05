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
    class CMakeIndex {
        //MakeIndex(Expression, PropertyInfo, Ienumerable<Expression>)
        //<Snippet1>
        public class Index1 {
            private int _this;
            public int this[int x]{
                get {
                    return _this + x;
                }
                set {
                    _this = value;
                }
            }
        }
        //</Snippet1>

        public static void MakeIndex1() {
            //<Snippet1>
            // Add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            var MyInstance = new Index1();
            MyInstance[0] = 5;

            //This expression represents accessing an indexed member, such as 
            //an array or an indexed property.
            IndexExpression MyMakeIndex = Expression.MakeIndex(
                Expression.Constant(MyInstance),
                typeof(Index1).GetProperty("Item"),
                new Expression[] {Expression.Constant(1)}
            );
            

            //The end result should 6:
            Console.WriteLine(Expression.Lambda<Func<int>>(MyMakeIndex).Compile().Invoke());
            //</Snippet1>

            //Validate sample
            if (Expression.Lambda<Func<int>>(MyMakeIndex).Compile().Invoke() != 6) throw new Exception();
        }

    }
}

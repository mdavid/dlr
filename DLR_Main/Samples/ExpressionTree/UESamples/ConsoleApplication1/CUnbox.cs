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
    class CUnbox {
        //Unbox(Expression, Type)
        public static void UnboxSample() {
            //<Snippet2>
            // This creates a boxed integer as a ConstantExpression.
            ConstantExpression boxedIntExpression = Expression.Constant(1, typeof(object));

            // This Expression unboxes the value in the first argument to the type of the second.
            UnaryExpression unboxedInt = Expression.Unbox(boxedIntExpression, typeof(int));

            var Result = Expression.Lambda<Func<int>>(unboxedInt).Compile().Invoke();

            Console.WriteLine("Result = {0} with type = {1}", Result, Result.GetType());
            //</Snippet2>

            if (Result != 1 || Result.GetType() != typeof(int))
                throw new Exception("UnboxSample failed");
        }
    }
}

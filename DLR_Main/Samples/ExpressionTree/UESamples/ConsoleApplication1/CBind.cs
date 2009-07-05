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
    class CBind {
        //Expression.Bind(System.Reflection.MemberInfo, Expression)
        static public void Bind1() {
            
            //<Snippet1>
            // add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif
       
            //This Expression represents initializing a member of a newly constructed object.
            MemberAssignment MyBind = Expression.Bind(
                typeof(System.Diagnostics.ProcessStartInfo).GetMember("FileName")[0],
                Expression.Constant("App1.exe")
            );

            //We use the bind expression to initialize the FileName member of ProcessStartInfo when we create a 
            //new instance here:
            Expression MyNewWithInit = Expression.MemberInit(
                Expression.New(typeof(System.Diagnostics.ProcessStartInfo)),
                MyBind
            );

            //To demonstrate it works, we assign the newly created start info to a variable, and we
            //read the value of the FileName member:
            ParameterExpression var = Expression.Variable(typeof(System.Diagnostics.ProcessStartInfo), "Proc start info");
            Console.WriteLine(Expression.Lambda<Func<String>>(
                Expression.Block(
                    new ParameterExpression[]{var},
                    Expression.Assign(var, MyNewWithInit),
                    Expression.Property(var,"FileName")
                )
            ).Compile().Invoke());

            //</Snippet1>

            //validate snippet
            if (String.Compare((String)Expression.Lambda<Func<String>>(
                Expression.Block(
                    new ParameterExpression[] { var },
                    Expression.Assign(var, MyNewWithInit),
                    Expression.Property(var, "FileName")
                )
            ).Compile().Invoke(), "App1.exe") != 0) throw new Exception();
        }

        //Expression.Bind(System.Reflection.MethodInfo, Expression)
        static public void Bind2() {

            //<Snippet1>
            // add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            //This Expression represents initializing a member of a newly constructed object.
            //In this case, we supply the accessor method for the property we wish to initialize.
            MemberAssignment MyBind = Expression.Bind(
                typeof(System.Diagnostics.ProcessStartInfo).GetMethod("set_FileName"),
                Expression.Constant("App1.exe")
            );

            //We use the bind expression to initialize the FileName member of ProcessStartInfo when we create a 
            //new instance here:
            Expression MyNewWithInit = Expression.MemberInit(
                Expression.New(typeof(System.Diagnostics.ProcessStartInfo)),
                MyBind
            );

            //To demonstrate it works, we assign the newly created start info to a variable, and we
            //read the value of the FileName member:
            ParameterExpression var = Expression.Variable(typeof(System.Diagnostics.ProcessStartInfo), "Proc start info");
            Console.WriteLine(Expression.Lambda<Func<String>>(
                Expression.Block(
                    new ParameterExpression[] { var },
                    Expression.Assign(var, MyNewWithInit),
                    Expression.Property(var, "FileName")
                )
            ).Compile().Invoke());

            //</Snippet1>

            //validate snippet
            if (String.Compare((String)Expression.Lambda<Func<String>>(
                Expression.Block(
                    new ParameterExpression[] { var },
                    Expression.Assign(var, MyNewWithInit),
                    Expression.Property(var, "FileName")
                )
            ).Compile().Invoke(), "App1.exe") != 0) throw new Exception();
        }
    }
}

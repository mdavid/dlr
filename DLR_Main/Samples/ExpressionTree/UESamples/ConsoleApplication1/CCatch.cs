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

namespace Samples
{
    class CCatch
    {
        //Catch(ParameterExpression, Expression)
        static public void Catch1()
        {
            //<Snippet1>
            // add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            //We'll use a ParameterExpression to hold the exception being caught
            ParameterExpression MyException = Expression.Parameter(typeof(ArgumentException), "MyException");

            //This Expression represents a catch block of a try statement.
            //The Block's body is simply a string that lets us know an exception was caught.
            CatchBlock MyCatch = Expression.Catch(
                                    MyException,
                                    Expression.Constant("Exception was caught")
                                );

            //We use the Catch in a Try Expression.
            //This Try Expression has the type of string.
            Expression MyTry = Expression.TryCatch(
                                    Expression.Throw(Expression.New(typeof(ArgumentException).GetConstructor(new Type[] { })), typeof(string)),
                                    MyCatch
                                );

            //Exception was caught?
            Console.WriteLine(Expression.Lambda<Func<string>>(MyTry).Compile().Invoke());

            //</Snippet1>

            //validate sample.
            if (String.Compare(Expression.Lambda<Func<string>>(MyTry).Compile().Invoke(), "Exception was caught") != 0) throw new Exception();
        }

        //Catch(ParameterExpression, Expression, Expression)
        static public void Catch2()
        {
            //<Snippet1>
            // add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            //We'll use a ParameterExpression to hold the exception being caught
            ParameterExpression MyException = Expression.Parameter(typeof(ArgumentException), "MyException");

            //This Expression represents a catch block of a try statement.
            //The Block's body is simply a string that lets us know an exception was caught.
            //We also provide a filter - this exception will not be caught by this handler unless the filter evaluates to true
            CatchBlock MyCatch = Expression.Catch(
                                    MyException,
                                    Expression.Constant("Exception was caught"),
                                    Expression.Equal(
                                        Expression.Constant(1),
                                        Expression.Constant(1)
                                    )
                                );

            //We use the Catch in a Try Expression.
            //This Try Expression has the type of string.
            Expression MyTry = Expression.TryCatch(
                                    Expression.Throw(Expression.New(typeof(ArgumentException).GetConstructor(new Type[] { })), typeof(string)),
                                    MyCatch
                                );

            //Exception was caught?
            Console.WriteLine(Expression.Lambda<Func<string>>(MyTry).Compile().Invoke());

            //</Snippet1>

            //validate sample.
            if (String.Compare(Expression.Lambda<Func<string>>(MyTry).Compile().Invoke(), "Exception was caught") != 0) throw new Exception();
        }

        //Catch(Type, Expression)
        static public void Catch3()
        {
            //<Snippet1>
            // add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif


            //This Expression represents a catch block of a try statement that catches exceptions of a given type.
            //The Block's body is simply a string that lets us know an exception was caught.
            CatchBlock MyCatch = Expression.Catch(
                                    typeof(ArgumentException),
                                    Expression.Constant("Exception was caught")
                                );

            //We use the Catch Expression in a Try Expression.
            //This Try Expression has the type of string.
            Expression MyTry = Expression.TryCatch(
                                    Expression.Throw(Expression.New(typeof(ArgumentException).GetConstructor(new Type[] { })),typeof(string)),
                                    MyCatch
                                );

            //Was exception caught?
            Console.WriteLine(Expression.Lambda<Func<string>>(MyTry).Compile().Invoke());

            //</Snippet1>

            //validate sample.
            if (String.Compare(Expression.Lambda<Func<string>>(MyTry).Compile().Invoke(), "Exception was caught") != 0) throw new Exception();
        }

        //Catch(Type, Expression, Expression)
        static public void Catch4()
        {
            //<Snippet1>
            // add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif


            //This Expression represents a catch block of a try statement that catches exceptions of a given type.
            //The Block's body is simply a string that lets us know an exception was caught.
            //We also provide a filter - this exception will not be caught by this handler unless the filter evaluates to true
            CatchBlock MyCatch = Expression.Catch(
                                    typeof(ArgumentException),
                                    Expression.Constant("Exception was caught"),
                                    Expression.Equal(
                                        Expression.Constant(1),
                                        Expression.Constant(1)
                                    )
                                );

            //We use the Catch Expression in a Try Expression.
            //This Try Expression has the type of string.
            Expression MyTry = Expression.TryCatch(
                                    Expression.Throw(Expression.New(typeof(ArgumentException).GetConstructor(new Type[] { })), typeof(string)),
                                    MyCatch
                                );

            //Was exception caught?
            Console.WriteLine(Expression.Lambda<Func<string>>(MyTry).Compile().Invoke());

            //</Snippet1>

            //validate sample.
            if (String.Compare(Expression.Lambda<Func<string>>(MyTry).Compile().Invoke(), "Exception was caught") != 0) throw new Exception();
        }

    }
}

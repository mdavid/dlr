using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Scripting.Ast;

namespace Samples {
    class CLoop {
        //Expression.Loop(Expression)
        static public void Loop1() {
            //<Snippet1>
            // add the following directive to your file
            // using Microsoft.Scripting.Ast;  

            //A simple loop loops forever, so we need to handle the jump out of the loop.
            //For that we'll create a label to jump out to.
            LabelTarget OutLabel = Expression.Label();

            //An index to condition the loop exit.
            ParameterExpression Index = Expression.Variable(typeof(int));
            

            //This Expression represents a loop.
            //Within the loop body, we'll increment a counter, and exit when it reaches 5.
            LoopExpression MyLoop = Expression.Loop(
                Expression.Block(
                    Expression.IfThen(
                        Expression.GreaterThanOrEqual(
                            Index,
                            Expression.Constant(5)
                        ),
                        Expression.Goto(OutLabel)
                    ),
                    Expression.PreIncrementAssign(Index)
                )
            );
                
            
            //To demonstrate the block use, we'll initialize the index variable,
            //run the loop, and check the index's value at the end (should be 5).
            var Main = Expression.Block(
                new ParameterExpression[]{Index},
                Expression.Assign(Index, Expression.Constant(0)),
                MyLoop,
                Expression.Label(OutLabel),
                Index
            );

            //Should print 5
            Console.WriteLine(Expression.Lambda<Func<int>>(Main).Compile().Invoke());

            //</Snippet1>

            //Validate sample
            if (Expression.Lambda<Func<int>>(Main).Compile().Invoke() != 5) throw new Exception("");
        }

        //Expression.Loop(Expression, LabelTarget)
        static public void Loop2() {
            //<Snippet1>
            // add the following directive to your file
            // using Microsoft.Scripting.Ast;  

            //A simple loop loops forever, so we need to handle the jump out of the loop.
            //For that we'll create a break label.
            LabelTarget BreakLabel = Expression.Label();

            //An index to condition the loop exit.
            ParameterExpression Index = Expression.Variable(typeof(int));


            //This Expression represents a loop.
            //Within the loop body, we'll increment a counter, and exit when it reaches 5.
            LoopExpression MyLoop = Expression.Loop(
                Expression.Block(
                    Expression.IfThen(
                        Expression.GreaterThanOrEqual(
                            Index,
                            Expression.Constant(5)
                        ),
                        Expression.Break(BreakLabel)
                    ),
                    Expression.PreIncrementAssign(Index)
                ),
                BreakLabel 
            );


            //To demonstrate the block use, we'll initialize the index variable,
            //run the loop, and check the index's value at the end (should be 5).
            var Main = Expression.Block(
                new ParameterExpression[] { Index },
                Expression.Assign(Index, Expression.Constant(0)),
                MyLoop,
                Index
            );

            //Should print 5
            Console.WriteLine(Expression.Lambda<Func<int>>(Main).Compile().Invoke());

            //</Snippet1>

            //Validate sample
            if (Expression.Lambda<Func<int>>(Main).Compile().Invoke() != 5) throw new Exception("");
        }



        //Expression.Loop(Expression, LabelTarget)
        static public void Loop3() {
            //<Snippet1>
            // add the following directive to your file
            // using Microsoft.Scripting.Ast;  

            //A simple loop loops forever, so we need to handle the jump out of the loop.
            //For that we'll create a break label.
            LabelTarget BreakLabel = Expression.Label();

            //We'll also create a continue label if we want to skip over a part of the loop.
            LabelTarget ContinueLabel = Expression.Label();

            //An index to condition the loop exit.
            ParameterExpression Index = Expression.Variable(typeof(int));


            //This Expression represents a loop.
            //Within the loop body, we'll increment a counter, and exit when it reaches 5.
            //For sample purposes, we continue over an increment and a decrement.
            LoopExpression MyLoop = Expression.Loop(
                Expression.Block(
                    Expression.IfThen(
                        Expression.GreaterThanOrEqual(
                            Index,
                            Expression.Constant(5)
                        ),
                        Expression.Break(BreakLabel)
                    ),
                    Expression.PreIncrementAssign(Index),
                    Expression.IfThen(
                        Expression.Equal(
                            Index,
                            Expression.Constant(3)
                        ),
                        Expression.Continue(ContinueLabel)
                    ),
                    Expression.PreIncrementAssign(Index),
                    Expression.PreDecrementAssign(Index)
                ),
                BreakLabel,
                ContinueLabel 
            );


            //To demonstrate the block use, we'll initialize the index variable,
            //run the loop, and check the index's value at the end (should be 5).
            var Main = Expression.Block(
                new ParameterExpression[] { Index },
                Expression.Assign(Index, Expression.Constant(0)),
                MyLoop,
                Index
            );

            //Should print 5
            Console.WriteLine(Expression.Lambda<Func<int>>(Main).Compile().Invoke());

            //</Snippet1>

            //Validate sample
            if (Expression.Lambda<Func<int>>(Main).Compile().Invoke() != 5) throw new Exception("");
        }
    }
}

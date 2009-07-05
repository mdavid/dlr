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

namespace Samples
{
    class CDebugInfo
    {
        //ClearDebugInfo(SymbolDocumentInfo)
        static public void DebugInfo1()
        {
            //<Snippet1>
            // add the following directive to your file
#if CODEPLEX_40
            // using System.Linq.Expressions;  
#else
            // using Microsoft.Linq.Expressions;  
#endif

            //This SymbolDocumentInfo represents the source file that resulted in the expressions marked with the DebugInfo.
            SymbolDocumentInfo DocInfo = Expression.SymbolDocument("FakeSourceFile.Fake");

            
            //This Expression represents the start of a section that associates expressions with the source that originated them.
            DebugInfoExpression MyDebugInfo = Expression.DebugInfo(DocInfo, 5, 1, 5, 10);

            //To demonstrate the use of DebugInfo, we create a block with several expressions, some of which are
            //marked by by a sequence of a DebugInfo and a ClearDebugInfo.
            //In this case, we've marked the expression that creates a constant with value 6 to have been generated from source
            //in the file 'FakeSourceFile.Fake', on line 5, between columns 1 and 10. The other expressions will not get associated
            //debug info (they might have been, for example, compiler generated code).
            Expression MyBlock = Expression.Block(
                                    Expression.Constant(5),
                                    MyDebugInfo,
                                    Expression.Constant(6),
                                    Expression.ClearDebugInfo(DocInfo),
                                    Expression.Constant(7)
                                );

            //</Snippet1>
        }
    }
}

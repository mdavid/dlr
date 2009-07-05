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
    class CSymbolDocument {
        //SymbolDocument(string)
        public static void SymbolDocument1() {
            //<Snippet1>
            //Add the following directive to your file
#if CODEPLEX_40
            //using System.Linq.Expressions;
#else
            //using Microsoft.Linq.Expressions;
#endif

            // This represents a source file that is to be referenced by debug information associated to the code
            // by a DebugInfo expression.
            SymbolDocumentInfo MySymbolDocument = Expression.SymbolDocument(
                "MySourceFile.CoolLanguage"
            );
            //<Snippet1>
        }

        //SymbolDocument(string, Guid)
        public static void SymbolDocument2() {
            //<Snippet1>
            //Add the following directive to your file
#if CODEPLEX_40
            //using System.Linq.Expressions;
#else
            //using Microsoft.Linq.Expressions;
#endif

            //We'll create a guid that represents our language. 
            //In real life, this would be a well defined guid, not just created on the fly.
            Guid MyLanguageGuid = Guid.NewGuid();

            // This represents a source file that is to be referenced by debug information associated to the code
            // by a DebugInfo expression.
            //It also identifies the language the source file contains by its guid.
            SymbolDocumentInfo MySymbolDocument = Expression.SymbolDocument(
                "MySourceFile.CoolLanguage",
                MyLanguageGuid 
            );

            //<Snippet1>
        }

        //SymbolDocument(string, Guid, Guid)
        public static void SymbolDocument3() {
            //<Snippet1>
            //Add the following directive to your file
#if CODEPLEX_40
            //using System.Linq.Expressions;
#else
            //using Microsoft.Linq.Expressions;
#endif

            //We'll create a guid that represents our language. 
            //In real life, this would be a well defined guid, not just created on the fly.
            Guid MyLanguageGuid = Guid.NewGuid();

            //We'll create a guid that represents a vendor
            //In real life, this would also be a well defined guid, not just created on the fly.
            Guid MyVendorGuid = Guid.NewGuid();

            // This represents a source file that is to be referenced by debug information associated to the code
            // by a DebugInfo expression.
            //It also identifies the language the source file contains by its guid.
            SymbolDocumentInfo MySymbolDocument = Expression.SymbolDocument(
                "MySourceFile.CoolLanguage",
                MyLanguageGuid,
                MyVendorGuid
            );

            //<Snippet1>
        }

        //SymbolDocument(string, Guid, Guid, Guid)
        public static void SymbolDocument4() {
            //<Snippet1>
            //Add the following directive to your file
#if CODEPLEX_40
            //using System.Linq.Expressions;
#else
            //using Microsoft.Linq.Expressions;
#endif

            //We'll create a guid that represents our language. 
            //In real life, this would be a well defined guid, not just created on the fly.
            Guid MyLanguageGuid = Guid.NewGuid();

            //We'll create a guid that represents a vendor
            //In real life, this would also be a well defined guid, not just created on the fly.
            Guid MyVendorGuid = Guid.NewGuid();

            //We'll create a guid that represents a document type.
            //Again, in real life, this would also be a well defined guid, not just created on the fly.
            Guid MyDocumentTypeGuid = Guid.NewGuid();

            // This represents a source file that is to be referenced by debug information associated to the code
            // by a DebugInfo expression.
            //It also identifies the language the source file contains by its guid.
            SymbolDocumentInfo MySymbolDocument = Expression.SymbolDocument(
                "MySourceFile.CoolLanguage",
                MyLanguageGuid,
                MyVendorGuid,
                MyDocumentTypeGuid
            );

            //<Snippet1>
        }

    }
}

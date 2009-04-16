/* ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation. 
 *
 * This source code is subject to terms and conditions of the Microsoft Public License. A 
 * copy of the license can be found in the License.html file at the root of this distribution. If 
 * you cannot locate the  Microsoft Public License, please send an email to 
 * ironpy@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
 * by the terms of the Microsoft Public License.
 *
 * You must not remove this notice, or any other, from this software.
 *
 *
 * ***************************************************************************/
using System; using Microsoft;


namespace ToyScript.Parser {

    enum TokenKind {
        None,

        Name,

        Number,
        String,

        Add,
        Subtract,
        Multiply,
        Divide,
        AddEqual,
        SubtractEqual,
        MultiplyEqual,
        DivideEqual,

        LessThan,
        GreaterThan,
        LessThanEqual,
        GreaterThanEqual,
        NotEqual,

        Equal,

        EqualEqual,

        Bang,
        Dot,
        Comma,
        SemiColon,

        OpenParen,
        CloseParen,

        OpenCurly,
        CloseCurly,

        OpenBracket,
        CloseBracket,

        //keywords
        KwNew,
        KwIf,
        KwElse,
        KwWhile,
        KwDef,
        KwReturn,
        KwVar,
        KwPrint,
        KwImport,

        EOF
    }

}
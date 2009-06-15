/* ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation. 
 *
 * This source code is subject to terms and conditions of the Microsoft Public License. A 
 * copy of the license can be found in the License.html file at the root of this distribution. If 
 * you cannot locate the  Microsoft Public License, please send an email to 
 * ironruby@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
 * by the terms of the Microsoft Public License.
 *
 * You must not remove this notice, or any other, from this software.
 *
 *
 * ***************************************************************************/

#if CODEPLEX_40
using System;
#else
using System; using Microsoft;
#endif
using System.IO;
using Microsoft.Scripting.Utils;

namespace IronRuby.Builtins {
    /// <summary>
    /// Helper class for creating the corresponding .NET exceptions from the Ruby Errno
    /// </summary>
    public static class RubyErrno {

        public static string/*!*/ MakeMessage(string message, string/*!*/ baseMessage) {
            Assert.NotNull(baseMessage);
            return (message != null) ? String.Concat(baseMessage, " - ", message) : baseMessage;
        }

        public static string/*!*/ MakeMessage(ref MutableString message, string/*!*/ baseMessage) {
            Assert.NotNull(baseMessage);
            string result = MakeMessage(message != null ? message.ConvertToString() : null, baseMessage);
            message = MutableString.Create(result);
            return result;
        }

        public static ExistError/*!*/ CreateEEXIST() {
            return new ExistError();
        }

        public static ExistError/*!*/ CreateEEXIST(string message) {
            return new ExistError(message);
        }

        public static ExistError/*!*/ CreateEEXIST(string message, Exception inner) {
            return new ExistError(message, inner);
        }

        public static InvalidError/*!*/ CreateEINVAL() {
            return new InvalidError();
        }

        public static InvalidError/*!*/ CreateEINVAL(string message) {
            return new InvalidError(message);
        }

        public static InvalidError/*!*/ CreateEINVAL(string message, Exception inner) {
            return new InvalidError(message, inner);
        }

        public static FileNotFoundException/*!*/ CreateENOENT() {
            return new FileNotFoundException();
        }

        public static FileNotFoundException/*!*/ CreateENOENT(string message, Exception inner) {
            return new FileNotFoundException(message, inner);
        }

        public static FileNotFoundException/*!*/ CreateENOENT(string message) {
            return new FileNotFoundException(message);
        }
    }
}

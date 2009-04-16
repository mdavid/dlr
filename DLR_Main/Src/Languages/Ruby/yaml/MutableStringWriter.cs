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

using System; using Microsoft;
using System.IO;
using System.Text;
using IronRuby.Builtins;

namespace IronRuby.StandardLibrary.Yaml {

    internal class MutableStringWriter : TextWriter {
        private readonly MutableString _str = MutableString.Create("");

        public override Encoding Encoding {
            get {
                // TODO: return MutableString encoding
                throw new NotImplementedException();
            }
        }

        public override void  Write(char value) {
            _str.Append(value);
        }

        public override void Write(char[] buffer, int index, int count) {
            // TODO: MutableString needs Append(char[], index, count)
            _str.Append(new string(buffer), index, count);
        }

        internal MutableString String {
            get { return _str; }
        }
    }
}

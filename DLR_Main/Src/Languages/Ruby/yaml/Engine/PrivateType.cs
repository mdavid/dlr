/***** BEGIN LICENSE BLOCK *****
 * Version: CPL 1.0
 *
 * The contents of this file are subject to the Common Public
 * License Version 1.0 (the "License"); you may not use this file
 * except in compliance with the License. You may obtain a copy of
 * the License at http://www.eclipse.org/legal/cpl-v10.html
 *
 * Software distributed under the License is distributed on an "AS
 * IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or
 * implied. See the License for the specific language governing
 * rights and limitations under the License.
 *
 * Copyright (C) 2007 Ola Bini <ola@ologix.com>
 * Copyright (c) Microsoft Corporation.
 * 
 ***** END LICENSE BLOCK *****/

namespace IronRuby.StandardLibrary.Yaml {

    public class PrivateType {
        private readonly string _tag;
        private readonly object _value;

        public PrivateType(string tag, object value) {
            _tag = tag;
            _value = value;
        }

        public string Tag { get { return _tag; } }
        public object Value { get { return _value; } }

        public override string ToString() {
            return "#<PrivateType Tag=\"" + _tag + "\" Value=\"" + _value + "\">";
        }
    }
}

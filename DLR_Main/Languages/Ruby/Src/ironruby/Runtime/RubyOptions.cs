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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Scripting;
using System.Threading;

namespace IronRuby {

    [Serializable]
    public sealed class RubyOptions : LanguageOptions {
        private readonly ReadOnlyCollection<string>/*!*/ _arguments;
        private readonly ReadOnlyCollection<string>/*!*/ _libraryPaths;
        private readonly string _mainFile;
        private readonly bool _enableTracing;
        private readonly int _verbosity;
        private readonly string _savePath;
        private readonly bool _loadFromDisk;
        private readonly bool _profile;
        private readonly bool _hasSearchPaths;
        private readonly RubyCompatibility _compatibility;

        public ReadOnlyCollection<string>/*!*/ Arguments {
            get { return _arguments; }
        }

        public string MainFile {
            get { return _mainFile; }
        }
        
        public int Verbosity {
            get { return _verbosity; }
        }

        public bool EnableTracing {
            get { return _enableTracing; }
        }

        public string SavePath {
            get { return _savePath; }
        }

        public bool LoadFromDisk {
            get { return _loadFromDisk; }
        }

        public bool Profile {
            get { return _profile; }
        }

        public ReadOnlyCollection<string>/*!*/ LibraryPaths {
            get { return _libraryPaths; }
        }

        public bool HasSearchPaths {
            get { return _hasSearchPaths; }
        }

        public RubyCompatibility Compatibility {
            get { return _compatibility; }
        }

        public RubyOptions(IDictionary<string, object>/*!*/ options)
            : base(options) {
            _arguments = GetStringCollectionOption(options, "Arguments") ?? EmptyStringCollection;

            _mainFile = GetOption(options, "MainFile", (string)null);
            _verbosity = GetOption(options, "Verbosity", 1);
            _enableTracing = GetOption(options, "EnableTracing", false);
            _savePath = GetOption(options, "SavePath", (string)null);
            _loadFromDisk = GetOption(options, "LoadFromDisk", false);
            _profile = GetOption(options, "Profile", false);
            _libraryPaths = GetStringCollectionOption(options, "LibraryPaths", ';', ',') ?? new ReadOnlyCollection<string>(new[] { "." });
            _hasSearchPaths = GetOption<object>(options, "SearchPaths", null) != null;
            _compatibility = GetCompatibility(options, "Compatibility", RubyCompatibility.Default);
        }

        private static RubyCompatibility GetCompatibility(IDictionary<string, object>/*!*/ options, string/*!*/ name, RubyCompatibility defaultValue) {
            object value;
            if (options != null && options.TryGetValue(name, out value)) {
                if (value is RubyCompatibility) {
                    return (RubyCompatibility)value;
                }

                string str = value as string;
                if (str != null) {
                    switch (str) {
                        case "1.8": return RubyCompatibility.Ruby18;
                        case "1.9": return RubyCompatibility.Ruby19;
                        case "2.0": return RubyCompatibility.Ruby20;
                    }
                }

                return (RubyCompatibility)Convert.ChangeType(value, typeof(RubyCompatibility), Thread.CurrentThread.CurrentCulture);
            }
            return defaultValue;
        }
    }
}

#if CODEPLEX_40
using System;
#else
using System; using Microsoft;
#endif
using System.Collections.Generic;
using System.Text;

namespace IronPython.Runtime {
    [PythonType]
    public sealed class NullImporter {
        public const string __module__ = "imp";        // logically lives in imp, but physically lives in IronPython.dll so Importer.cs can access it

        public NullImporter(string path_string) {
        }

        public object find_module(params object[] args) {
            return null;
        }
    }
}

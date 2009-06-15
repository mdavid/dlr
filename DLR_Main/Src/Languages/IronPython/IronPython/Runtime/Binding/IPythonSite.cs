#if CODEPLEX_40
using System;
#else
using System; using Microsoft;
#endif
using System.Collections.Generic;
using System.Text;

namespace IronPython.Runtime.Binding {
    interface IPythonSite {
        /// <summary>
        /// Gets the PythonContext which the CallSiteBinder is associated with.
        /// </summary>
        PythonContext/*!*/ Context {
            get;
        }
    }
}

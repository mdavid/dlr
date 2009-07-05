#if CODEPLEX_40
using System;
#else
using System; using Microsoft;
#endif
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Scripting.Actions {
    /// <summary>
    /// Specifies the action for which the default binder is requesting a member.
    /// </summary>
    public enum MemberRequestKind {
        None,
        Get,
        Set,
        Delete,
        Invoke,
        InvokeMember,
        Convert,
        Operation
    }
}

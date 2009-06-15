#if CODEPLEX_40
using System;
using System.Dynamic;
#else
using System; using Microsoft;
using Microsoft.Scripting;
#endif
using Microsoft.Scripting.Runtime;
#if CODEPLEX_40
using System.Linq.Expressions;
#else
using Microsoft.Linq.Expressions;
#endif

namespace Microsoft.Scripting.Runtime {
    public static class BindingRestrictionsHelpers {
        //If the type is Microsoft.Scripting.Runtime.DynamicNull, create an instance restriction to test null
        public static BindingRestrictions GetRuntimeTypeRestriction(Expression expr, Type type) {
            if (type == typeof(DynamicNull)) {
                return BindingRestrictions.GetInstanceRestriction(expr, null);
            }

            return BindingRestrictions.GetTypeRestriction(expr, type);
        }

        public static BindingRestrictions GetRuntimeTypeRestriction(DynamicMetaObject obj) {
            return obj.Restrictions.Merge(GetRuntimeTypeRestriction(obj.Expression, obj.GetLimitType()));
        }
    }
}

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
using Microsoft.Scripting;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using IronRuby.Runtime;
using IronRuby.Runtime.Calls;

namespace IronRuby.Builtins {

    [RubyClass("UnboundMethod")]
    public class UnboundMethod {
        private readonly string/*!*/ _name;
        private readonly RubyMemberInfo/*!*/ _info;
        private readonly RubyModule/*!*/ _targetConstraint;

        internal RubyMemberInfo/*!*/ Info {
            get { return _info; }
        }

        internal string/*!*/ Name {
            get { return _name; }
        }

        internal RubyModule/*!*/ TargetConstraint {
            get { return _targetConstraint; }
        }

        internal UnboundMethod(RubyModule/*!*/ targetConstraint, string/*!*/ name, RubyMemberInfo/*!*/ info) {
            Assert.NotNull(targetConstraint, name, info);

            _name = name;
            _info = info;
            _targetConstraint = targetConstraint;
        }

        #region Public Instance Methods

        [RubyMethod("==")]
        public static bool Equal(UnboundMethod/*!*/ self, [NotNull]UnboundMethod/*!*/ other) {
            return ReferenceEquals(self.Info, other.Info);
        }

        [RubyMethod("==")]
        public static bool Equal(UnboundMethod/*!*/ self, object other) {
            return false;
        }

        [RubyMethod("arity")]
        public static int GetArity(UnboundMethod/*!*/ self) {
            return self.Info.Arity;
        }

        [RubyMethod("bind")]
        public static RubyMethod/*!*/ Bind(UnboundMethod/*!*/ self, object target) {
            RubyContext context = self._targetConstraint.Context;

            if (!context.GetClassOf(target).HasAncestor(self._targetConstraint)) {
                throw RubyExceptions.CreateTypeError(
                    String.Format("bind argument must be an instance of {0}", self._targetConstraint.GetName(context))
                );
            }
            
            return new RubyMethod(target, self._info, self._name);
        }

        [RubyMethod("clone")]
        public static UnboundMethod/*!*/ Clone(UnboundMethod/*!*/ self) {
            return new UnboundMethod(self._targetConstraint, self._name, self._info);
        }

        [RubyMethod("to_s")]
        public static MutableString/*!*/ ToS(RubyContext/*!*/ context, UnboundMethod/*!*/ self) {
            return ToS(context, self.Name, self._info.DeclaringModule, self._targetConstraint, "UnboundMethod");
        }

        internal static MutableString/*!*/ ToS(RubyContext/*!*/ context, string/*!*/ methodName, RubyModule/*!*/ declaringModule, RubyModule/*!*/ targetModule, 
            string/*!*/ classDisplayName) {

            MutableString result = MutableString.CreateMutable();
            result.Append("#<");
            result.Append(classDisplayName);
            result.Append(": ");

            if (ReferenceEquals(targetModule, declaringModule)) {
                result.Append(declaringModule.GetName(context));
            } else {
                result.Append(targetModule.GetName(context));
                result.Append('(');
                result.Append(declaringModule.GetName(context));
                result.Append(')');
            }

            result.Append('#');
            result.Append(methodName);
            result.Append('>');
            return result; 
        }

        [RubyMethod("of")]
        public static UnboundMethod/*!*/ BingGenericParameters(RubyContext/*!*/ context, UnboundMethod/*!*/ self, [NotNull]params object[]/*!*/ typeArgs) {
            return new UnboundMethod(self.TargetConstraint, self.Name, MethodOps.BindGenericParameters(context, self.Info, self.Name, typeArgs));
        }

        [RubyMethod("overloads")]
        public static UnboundMethod/*!*/ GetOverloads(RubyContext/*!*/ context, UnboundMethod/*!*/ self, [NotNull]params object[]/*!*/ parameterTypes) {
            return new UnboundMethod(self.TargetConstraint, self.Name, MethodOps.GetOverloads(context, self.Info, self.Name, parameterTypes));
        }

        [RubyMethod("clr_members")]
        public static RubyArray/*!*/ GetClrMembers(UnboundMethod/*!*/ self) {
            return new RubyArray(self.Info.GetMembers());
        }

        #endregion
    }
}

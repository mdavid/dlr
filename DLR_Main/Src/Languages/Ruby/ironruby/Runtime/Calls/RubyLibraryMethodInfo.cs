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
using System.Collections.Generic;
using System.Diagnostics;
#if CODEPLEX_40
using System.Linq.Expressions;
#else
using Microsoft.Linq.Expressions;
#endif
using System.Reflection;
using System.Reflection.Emit;
#if CODEPLEX_40
using System.Dynamic;
#else
#endif
using Microsoft.Scripting;
using Microsoft.Scripting.Actions;
using Microsoft.Scripting.Actions.Calls;
using Microsoft.Scripting.Generation;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using IronRuby.Builtins;
using IronRuby.Compiler.Generation;
using IronRuby.Compiler;

using AstFactory = IronRuby.Compiler.Ast.AstFactory;
using AstUtils = Microsoft.Scripting.Ast.Utils;
#if CODEPLEX_40
using Ast = System.Linq.Expressions.Expression;
#else
using Ast = Microsoft.Linq.Expressions.Expression;
#endif

namespace IronRuby.Runtime.Calls {
    
    /// <summary>
    /// Performs method binding for calling CLR methods.
    /// Currently this is used for all builtin libary methods and interop calls to CLR methods
    /// </summary>
    public sealed class RubyLibraryMethodInfo : RubyMethodGroupBase {
        private readonly Delegate/*!*/[]/*!*/ _overloads;

        /// <summary>
        /// Creates a Ruby method implemented by a method group of CLR methods.
        /// </summary>
        internal RubyLibraryMethodInfo(Delegate/*!*/[]/*!*/ overloads, RubyMemberFlags flags, RubyModule/*!*/ declaringModule)
            : base(null, flags, declaringModule) {
            Assert.NotNullItems(overloads);
            Assert.NotEmpty(overloads);
            _overloads = overloads;
        }

        // copy ctor
        private RubyLibraryMethodInfo(RubyLibraryMethodInfo/*!*/ info, MethodBase/*!*/[]/*!*/ methods)
            : base(methods, info.Flags, info.DeclaringModule) {
        }

        internal override bool IsRemovable {
            get { return true; }
        }

        internal Delegate/*!*/[]/*!*/ Overloads {
            get { return _overloads; }
        }

        internal override SelfCallConvention CallConvention {
            get { return SelfCallConvention.SelfIsParameter; }
        }

        internal override bool ImplicitProtocolConversions {
            get { return false; }
        }

        internal protected override MethodBase/*!*/[]/*!*/ MethodBases {
            get {
                Debug.Assert(base.MethodBases != null || _overloads != null);

                // don't need to lock MethodBases since all values calculated by multiple threads are the same: 
                return base.MethodBases ?? SetMethodBasesNoLock(_overloads.ConvertAll((d) => d.Method));
            }
        }

        public override MemberInfo/*!*/[]/*!*/ GetMembers() {
            return ArrayUtils.MakeArray(MethodBases);
        }

        protected internal override RubyMemberInfo/*!*/ Copy(RubyMemberFlags flags, RubyModule/*!*/ module) {
            return new RubyLibraryMethodInfo(_overloads, flags, module);
        }

        protected override RubyMemberInfo/*!*/ Copy(MethodBase/*!*/[]/*!*/ methods) {
            return new RubyLibraryMethodInfo(this, methods);
        }

        internal override MethodDispatcher GetDispatcher<T>(RubyCallSignature signature, object target, int version) {
            if (!(target is IRubyObject)) {
                return null;
            }

            int arity;
            if (!IsEmpty || (arity = GetArity()) != 1) {
                return null;
            }

            return MethodDispatcher.CreateRubyObjectDispatcher(
                typeof(T), new Func<object, Proc, object, object>(EmptyRubyMethodStub1), arity, signature.HasScope, signature.HasBlock, version
            );
        }

        public static object EmptyRubyMethodStub1(object self, Proc block, object arg0) {
            // nop
            return null;
        }

        internal override void BuildCallNoFlow(MetaObjectBuilder/*!*/ metaBuilder, CallArguments/*!*/ args, string/*!*/ name) {
            BuildCallNoFlow(metaBuilder, args, name, MethodBases, CallConvention, ImplicitProtocolConversions);
        }
    }
}


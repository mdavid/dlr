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

#if !CLR2
using System.Linq.Expressions;
#else
using Microsoft.Scripting.Ast;
#endif

using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Microsoft.Scripting.Utils;
using IronRuby.Builtins;
using System.Collections.Generic;

namespace IronRuby.Runtime.Calls {
    public abstract partial class MemberDispatcher {
        internal const int /*$$*/MaxPrecompiledArity = 5;
        internal const int /*$$*/MaxInterpretedArity = 15;
        internal const int /*$$*/MaxRubyMethodArity = 14;

        internal static LambdaExpression CreateRubyMethodLambda(Expression/*!*/ body, string/*!*/ name, ICollection<ParameterExpression>/*!*/ parameters) {
            switch (parameters.Count - 2) {
#if GENERATOR
                def generate; $MaxRubyMethodArity.times { |n| @n = n + 1; super }; end
#else
                case /*$n{*/0/*}*/: return Expression.Lambda<Func<object, Proc/*$Objects*/, object>>(body, name, parameters);
#endif
#region Generated by MethodDispatcher.Generator.rb

                case 1: return Expression.Lambda<Func<object, Proc, object, object>>(body, name, parameters);
                case 2: return Expression.Lambda<Func<object, Proc, object, object, object>>(body, name, parameters);
                case 3: return Expression.Lambda<Func<object, Proc, object, object, object, object>>(body, name, parameters);
                case 4: return Expression.Lambda<Func<object, Proc, object, object, object, object, object>>(body, name, parameters);
                case 5: return Expression.Lambda<Func<object, Proc, object, object, object, object, object, object>>(body, name, parameters);
                case 6: return Expression.Lambda<Func<object, Proc, object, object, object, object, object, object, object>>(body, name, parameters);
                case 7: return Expression.Lambda<Func<object, Proc, object, object, object, object, object, object, object, object>>(body, name, parameters);
                case 8: return Expression.Lambda<Func<object, Proc, object, object, object, object, object, object, object, object, object>>(body, name, parameters);
                case 9: return Expression.Lambda<Func<object, Proc, object, object, object, object, object, object, object, object, object, object>>(body, name, parameters);
                case 10: return Expression.Lambda<Func<object, Proc, object, object, object, object, object, object, object, object, object, object, object>>(body, name, parameters);
                case 11: return Expression.Lambda<Func<object, Proc, object, object, object, object, object, object, object, object, object, object, object, object>>(body, name, parameters);
                case 12: return Expression.Lambda<Func<object, Proc, object, object, object, object, object, object, object, object, object, object, object, object, object>>(body, name, parameters);
                case 13: return Expression.Lambda<Func<object, Proc, object, object, object, object, object, object, object, object, object, object, object, object, object, object>>(body, name, parameters);
                case 14: return Expression.Lambda<Func<object, Proc, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>>(body, name, parameters);

#endregion
                default: return null;
            }
        }

        internal static readonly Type[] RubyObjectMethodDispatchers = new[] {
#if GENERATOR
            def generate; add_generic_types($MaxPrecompiledArity); end
#else
            typeof(RubyObjectMethodDispatcher<>),
#endif
#region Generated by MethodDispatcher.Generator.rb

            typeof(RubyObjectMethodDispatcher<,>),
            typeof(RubyObjectMethodDispatcher<,,>),
            typeof(RubyObjectMethodDispatcher<,,,>),
            typeof(RubyObjectMethodDispatcher<,,,,>),

#endregion
        };

        internal static readonly Type[] RubyObjectMethodDispatchersWithBlock = new[] {
#if GENERATOR
            def generate; add_generic_types($MaxPrecompiledArity); end
#else
            typeof(RubyObjectMethodDispatcherWithBlock<>),
#endif
#region Generated by MethodDispatcher.Generator.rb

            typeof(RubyObjectMethodDispatcherWithBlock<,>),
            typeof(RubyObjectMethodDispatcherWithBlock<,,>),
            typeof(RubyObjectMethodDispatcherWithBlock<,,,>),
            typeof(RubyObjectMethodDispatcherWithBlock<,,,,>),

#endregion
        };

        internal static readonly Type[] RubyObjectMethodDispatchersWithScope = new[] {
#if GENERATOR
            def generate; add_generic_types($MaxPrecompiledArity); end
#else
            typeof(RubyObjectMethodDispatcherWithScope<>),
#endif
#region Generated by MethodDispatcher.Generator.rb

            typeof(RubyObjectMethodDispatcherWithScope<,>),
            typeof(RubyObjectMethodDispatcherWithScope<,,>),
            typeof(RubyObjectMethodDispatcherWithScope<,,,>),
            typeof(RubyObjectMethodDispatcherWithScope<,,,,>),

#endregion
        };

        internal static readonly Type[] RubyObjectMethodDispatchersWithScopeAndBlock = new[] {
#if GENERATOR
            def generate; add_generic_types($MaxPrecompiledArity); end
#else
            typeof(RubyObjectMethodDispatcherWithScopeAndBlock<>),
#endif
#region Generated by MethodDispatcher.Generator.rb

            typeof(RubyObjectMethodDispatcherWithScopeAndBlock<,>),
            typeof(RubyObjectMethodDispatcherWithScopeAndBlock<,,>),
            typeof(RubyObjectMethodDispatcherWithScopeAndBlock<,,,>),
            typeof(RubyObjectMethodDispatcherWithScopeAndBlock<,,,,>),

#endregion
        };

        internal static readonly Type[] RubyObjectAttributeWriterDispatchersWithScope = new[] {
            typeof(RubyObjectAttributeWriterDispatcherWithScope<>)
        };

        // TODO: an array, if all interpreted sites were untyped
        internal static readonly HashSet<Type> UntypedFuncs = new HashSet<Type>() {
#if GENERATOR
    def generate
      $MaxInterpretedArity.times { |n| @n = n + 1; super }
    end
#else
           typeof(Func<CallSite, object/*$Objects*/>),
#endif
#region Generated by MethodDispatcher.Generator.rb

           typeof(Func<CallSite, object, object>),
           typeof(Func<CallSite, object, object, object>),
           typeof(Func<CallSite, object, object, object, object>),
           typeof(Func<CallSite, object, object, object, object, object>),
           typeof(Func<CallSite, object, object, object, object, object, object>),
           typeof(Func<CallSite, object, object, object, object, object, object, object>),
           typeof(Func<CallSite, object, object, object, object, object, object, object, object>),
           typeof(Func<CallSite, object, object, object, object, object, object, object, object, object>),
           typeof(Func<CallSite, object, object, object, object, object, object, object, object, object, object>),
           typeof(Func<CallSite, object, object, object, object, object, object, object, object, object, object, object>),
           typeof(Func<CallSite, object, object, object, object, object, object, object, object, object, object, object, object>),
           typeof(Func<CallSite, object, object, object, object, object, object, object, object, object, object, object, object, object>),
           typeof(Func<CallSite, object, object, object, object, object, object, object, object, object, object, object, object, object, object>),
           typeof(Func<CallSite, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>),
           typeof(Func<CallSite, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>),

#endregion
        }; 
    }

#if GENERATOR
    def generate
      $MaxPrecompiledArity.times { |n| @n = n + 1; super }
    end
#else
    public class RubyObjectMethodDispatcher/*$GenericDecl*/ : MethodDispatcher<Func<object, Proc, object/*$Objects*/>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return new Func<CallSite, object /*$GenericParams*/, object>(Invoke);
        }

        public object Invoke(CallSite/*!*/ callSite, object self /*$Parameters*/) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                return Method(self, null /*$Arguments*/);
            } else {
                return ((CallSite<Func<CallSite, object /*$GenericParams*/, object>>)callSite).
                    Update(callSite, self /*$Arguments*/);
            }
        }
    }

    public class RubyObjectMethodDispatcherWithScope/*$GenericDecl*/ : MethodDispatcher<Func<object, Proc, object/*$Objects*/>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return isUntyped ?
                (object)new Func<CallSite, object, object /*$GenericParams*/, object>(Invoke<object>) :
                (object)new Func<CallSite, RubyScope, object /*$GenericParams*/, object>(Invoke<RubyScope>);
        }

        public object Invoke<TScope>(CallSite/*!*/ callSite, TScope/*!*/ scope, object self /*$Parameters*/) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                return Method(self, null /*$Arguments*/);
            } else {
                return ((CallSite<Func<CallSite, TScope, object /*$GenericParams*/, object>>)callSite).
                    Update(callSite, scope, self /*$Arguments*/);
            }
        }
    }

    public class RubyObjectMethodDispatcherWithBlock/*$GenericDecl*/ : MethodDispatcher<Func<object, Proc, object/*$Objects*/>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return isUntyped ? 
                (object)new Func<CallSite, object, object /*$GenericParams*/, object>(Invoke<object>) :
                (object)new Func<CallSite, object, Proc /*$GenericParams*/, object>(Invoke<Proc>);
        }

        public object Invoke<TProc>(CallSite/*!*/ callSite, object self, TProc proc /*$Parameters*/) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                // Dispatching to a Ruby method - the method scope sets the proc's converter to itself, hence we don't need to do it here:
                return Method(self, (Proc)(object)proc /*$Arguments*/);
            } else {
                return ((CallSite<Func<CallSite, object, TProc /*$GenericParams*/, object>>)callSite).
                    Update(callSite, self, proc /*$Arguments*/);
            }
        }
    }

    public class RubyObjectMethodDispatcherWithScopeAndBlock/*$GenericDecl*/ : MethodDispatcher<Func<object, Proc, object/*$Objects*/>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return isUntyped ?
                (object)new Func<CallSite, object, object, object /*$GenericParams*/, object>(Invoke<object, object>) :
                (object)new Func<CallSite, RubyScope, object, Proc /*$GenericParams*/, object>(Invoke<RubyScope, Proc>);
            
        }

        public object Invoke<TScope, TProc>(CallSite/*!*/ callSite, TScope/*!*/ scope, object self, TProc proc /*$Parameters*/) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                // Dispatching to a Ruby method - the method scope sets the proc's converter to itself, hence we don't need to do it here:
                return Method(self, (Proc)(object)proc /*$Arguments*/);
            } else {
                return ((CallSite<Func<CallSite, TScope, object, TProc /*$GenericParams*/, object>>)callSite).
                    Update(callSite, scope, self, proc /*$Arguments*/);
            }
        }
    }
#endif
#region Generated by MethodDispatcher.Generator.rb

    public class RubyObjectMethodDispatcher<T0> : MethodDispatcher<Func<object, Proc, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return new Func<CallSite, object , T0, object>(Invoke);
        }

        public object Invoke(CallSite/*!*/ callSite, object self ,T0 arg0) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                return Method(self, null ,arg0);
            } else {
                return ((CallSite<Func<CallSite, object , T0, object>>)callSite).
                    Update(callSite, self ,arg0);
            }
        }
    }

    public class RubyObjectMethodDispatcherWithScope<T0> : MethodDispatcher<Func<object, Proc, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return isUntyped ?
                (object)new Func<CallSite, object, object , T0, object>(Invoke<object>) :
                (object)new Func<CallSite, RubyScope, object , T0, object>(Invoke<RubyScope>);
        }

        public object Invoke<TScope>(CallSite/*!*/ callSite, TScope/*!*/ scope, object self ,T0 arg0) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                return Method(self, null ,arg0);
            } else {
                return ((CallSite<Func<CallSite, TScope, object , T0, object>>)callSite).
                    Update(callSite, scope, self ,arg0);
            }
        }
    }

    public class RubyObjectMethodDispatcherWithBlock<T0> : MethodDispatcher<Func<object, Proc, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return isUntyped ? 
                (object)new Func<CallSite, object, object , T0, object>(Invoke<object>) :
                (object)new Func<CallSite, object, Proc , T0, object>(Invoke<Proc>);
        }

        public object Invoke<TProc>(CallSite/*!*/ callSite, object self, TProc proc ,T0 arg0) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                // Dispatching to a Ruby method - the method scope sets the proc's converter to itself, hence we don't need to do it here:
                return Method(self, (Proc)(object)proc ,arg0);
            } else {
                return ((CallSite<Func<CallSite, object, TProc , T0, object>>)callSite).
                    Update(callSite, self, proc ,arg0);
            }
        }
    }

    public class RubyObjectMethodDispatcherWithScopeAndBlock<T0> : MethodDispatcher<Func<object, Proc, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return isUntyped ?
                (object)new Func<CallSite, object, object, object , T0, object>(Invoke<object, object>) :
                (object)new Func<CallSite, RubyScope, object, Proc , T0, object>(Invoke<RubyScope, Proc>);
            
        }

        public object Invoke<TScope, TProc>(CallSite/*!*/ callSite, TScope/*!*/ scope, object self, TProc proc ,T0 arg0) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                // Dispatching to a Ruby method - the method scope sets the proc's converter to itself, hence we don't need to do it here:
                return Method(self, (Proc)(object)proc ,arg0);
            } else {
                return ((CallSite<Func<CallSite, TScope, object, TProc , T0, object>>)callSite).
                    Update(callSite, scope, self, proc ,arg0);
            }
        }
    }
    public class RubyObjectMethodDispatcher<T0, T1> : MethodDispatcher<Func<object, Proc, object, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return new Func<CallSite, object , T0 , T1, object>(Invoke);
        }

        public object Invoke(CallSite/*!*/ callSite, object self ,T0 arg0, T1 arg1) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                return Method(self, null ,arg0, arg1);
            } else {
                return ((CallSite<Func<CallSite, object , T0 , T1, object>>)callSite).
                    Update(callSite, self ,arg0, arg1);
            }
        }
    }

    public class RubyObjectMethodDispatcherWithScope<T0, T1> : MethodDispatcher<Func<object, Proc, object, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return isUntyped ?
                (object)new Func<CallSite, object, object , T0 , T1, object>(Invoke<object>) :
                (object)new Func<CallSite, RubyScope, object , T0 , T1, object>(Invoke<RubyScope>);
        }

        public object Invoke<TScope>(CallSite/*!*/ callSite, TScope/*!*/ scope, object self ,T0 arg0, T1 arg1) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                return Method(self, null ,arg0, arg1);
            } else {
                return ((CallSite<Func<CallSite, TScope, object , T0 , T1, object>>)callSite).
                    Update(callSite, scope, self ,arg0, arg1);
            }
        }
    }

    public class RubyObjectMethodDispatcherWithBlock<T0, T1> : MethodDispatcher<Func<object, Proc, object, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return isUntyped ? 
                (object)new Func<CallSite, object, object , T0 , T1, object>(Invoke<object>) :
                (object)new Func<CallSite, object, Proc , T0 , T1, object>(Invoke<Proc>);
        }

        public object Invoke<TProc>(CallSite/*!*/ callSite, object self, TProc proc ,T0 arg0, T1 arg1) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                // Dispatching to a Ruby method - the method scope sets the proc's converter to itself, hence we don't need to do it here:
                return Method(self, (Proc)(object)proc ,arg0, arg1);
            } else {
                return ((CallSite<Func<CallSite, object, TProc , T0 , T1, object>>)callSite).
                    Update(callSite, self, proc ,arg0, arg1);
            }
        }
    }

    public class RubyObjectMethodDispatcherWithScopeAndBlock<T0, T1> : MethodDispatcher<Func<object, Proc, object, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return isUntyped ?
                (object)new Func<CallSite, object, object, object , T0 , T1, object>(Invoke<object, object>) :
                (object)new Func<CallSite, RubyScope, object, Proc , T0 , T1, object>(Invoke<RubyScope, Proc>);
            
        }

        public object Invoke<TScope, TProc>(CallSite/*!*/ callSite, TScope/*!*/ scope, object self, TProc proc ,T0 arg0, T1 arg1) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                // Dispatching to a Ruby method - the method scope sets the proc's converter to itself, hence we don't need to do it here:
                return Method(self, (Proc)(object)proc ,arg0, arg1);
            } else {
                return ((CallSite<Func<CallSite, TScope, object, TProc , T0 , T1, object>>)callSite).
                    Update(callSite, scope, self, proc ,arg0, arg1);
            }
        }
    }
    public class RubyObjectMethodDispatcher<T0, T1, T2> : MethodDispatcher<Func<object, Proc, object, object, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return new Func<CallSite, object , T0 , T1 , T2, object>(Invoke);
        }

        public object Invoke(CallSite/*!*/ callSite, object self ,T0 arg0, T1 arg1, T2 arg2) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                return Method(self, null ,arg0, arg1, arg2);
            } else {
                return ((CallSite<Func<CallSite, object , T0 , T1 , T2, object>>)callSite).
                    Update(callSite, self ,arg0, arg1, arg2);
            }
        }
    }

    public class RubyObjectMethodDispatcherWithScope<T0, T1, T2> : MethodDispatcher<Func<object, Proc, object, object, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return isUntyped ?
                (object)new Func<CallSite, object, object , T0 , T1 , T2, object>(Invoke<object>) :
                (object)new Func<CallSite, RubyScope, object , T0 , T1 , T2, object>(Invoke<RubyScope>);
        }

        public object Invoke<TScope>(CallSite/*!*/ callSite, TScope/*!*/ scope, object self ,T0 arg0, T1 arg1, T2 arg2) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                return Method(self, null ,arg0, arg1, arg2);
            } else {
                return ((CallSite<Func<CallSite, TScope, object , T0 , T1 , T2, object>>)callSite).
                    Update(callSite, scope, self ,arg0, arg1, arg2);
            }
        }
    }

    public class RubyObjectMethodDispatcherWithBlock<T0, T1, T2> : MethodDispatcher<Func<object, Proc, object, object, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return isUntyped ? 
                (object)new Func<CallSite, object, object , T0 , T1 , T2, object>(Invoke<object>) :
                (object)new Func<CallSite, object, Proc , T0 , T1 , T2, object>(Invoke<Proc>);
        }

        public object Invoke<TProc>(CallSite/*!*/ callSite, object self, TProc proc ,T0 arg0, T1 arg1, T2 arg2) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                // Dispatching to a Ruby method - the method scope sets the proc's converter to itself, hence we don't need to do it here:
                return Method(self, (Proc)(object)proc ,arg0, arg1, arg2);
            } else {
                return ((CallSite<Func<CallSite, object, TProc , T0 , T1 , T2, object>>)callSite).
                    Update(callSite, self, proc ,arg0, arg1, arg2);
            }
        }
    }

    public class RubyObjectMethodDispatcherWithScopeAndBlock<T0, T1, T2> : MethodDispatcher<Func<object, Proc, object, object, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return isUntyped ?
                (object)new Func<CallSite, object, object, object , T0 , T1 , T2, object>(Invoke<object, object>) :
                (object)new Func<CallSite, RubyScope, object, Proc , T0 , T1 , T2, object>(Invoke<RubyScope, Proc>);
            
        }

        public object Invoke<TScope, TProc>(CallSite/*!*/ callSite, TScope/*!*/ scope, object self, TProc proc ,T0 arg0, T1 arg1, T2 arg2) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                // Dispatching to a Ruby method - the method scope sets the proc's converter to itself, hence we don't need to do it here:
                return Method(self, (Proc)(object)proc ,arg0, arg1, arg2);
            } else {
                return ((CallSite<Func<CallSite, TScope, object, TProc , T0 , T1 , T2, object>>)callSite).
                    Update(callSite, scope, self, proc ,arg0, arg1, arg2);
            }
        }
    }
    public class RubyObjectMethodDispatcher<T0, T1, T2, T3> : MethodDispatcher<Func<object, Proc, object, object, object, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return new Func<CallSite, object , T0 , T1 , T2 , T3, object>(Invoke);
        }

        public object Invoke(CallSite/*!*/ callSite, object self ,T0 arg0, T1 arg1, T2 arg2, T3 arg3) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                return Method(self, null ,arg0, arg1, arg2, arg3);
            } else {
                return ((CallSite<Func<CallSite, object , T0 , T1 , T2 , T3, object>>)callSite).
                    Update(callSite, self ,arg0, arg1, arg2, arg3);
            }
        }
    }

    public class RubyObjectMethodDispatcherWithScope<T0, T1, T2, T3> : MethodDispatcher<Func<object, Proc, object, object, object, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return isUntyped ?
                (object)new Func<CallSite, object, object , T0 , T1 , T2 , T3, object>(Invoke<object>) :
                (object)new Func<CallSite, RubyScope, object , T0 , T1 , T2 , T3, object>(Invoke<RubyScope>);
        }

        public object Invoke<TScope>(CallSite/*!*/ callSite, TScope/*!*/ scope, object self ,T0 arg0, T1 arg1, T2 arg2, T3 arg3) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                return Method(self, null ,arg0, arg1, arg2, arg3);
            } else {
                return ((CallSite<Func<CallSite, TScope, object , T0 , T1 , T2 , T3, object>>)callSite).
                    Update(callSite, scope, self ,arg0, arg1, arg2, arg3);
            }
        }
    }

    public class RubyObjectMethodDispatcherWithBlock<T0, T1, T2, T3> : MethodDispatcher<Func<object, Proc, object, object, object, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return isUntyped ? 
                (object)new Func<CallSite, object, object , T0 , T1 , T2 , T3, object>(Invoke<object>) :
                (object)new Func<CallSite, object, Proc , T0 , T1 , T2 , T3, object>(Invoke<Proc>);
        }

        public object Invoke<TProc>(CallSite/*!*/ callSite, object self, TProc proc ,T0 arg0, T1 arg1, T2 arg2, T3 arg3) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                // Dispatching to a Ruby method - the method scope sets the proc's converter to itself, hence we don't need to do it here:
                return Method(self, (Proc)(object)proc ,arg0, arg1, arg2, arg3);
            } else {
                return ((CallSite<Func<CallSite, object, TProc , T0 , T1 , T2 , T3, object>>)callSite).
                    Update(callSite, self, proc ,arg0, arg1, arg2, arg3);
            }
        }
    }

    public class RubyObjectMethodDispatcherWithScopeAndBlock<T0, T1, T2, T3> : MethodDispatcher<Func<object, Proc, object, object, object, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return isUntyped ?
                (object)new Func<CallSite, object, object, object , T0 , T1 , T2 , T3, object>(Invoke<object, object>) :
                (object)new Func<CallSite, RubyScope, object, Proc , T0 , T1 , T2 , T3, object>(Invoke<RubyScope, Proc>);
            
        }

        public object Invoke<TScope, TProc>(CallSite/*!*/ callSite, TScope/*!*/ scope, object self, TProc proc ,T0 arg0, T1 arg1, T2 arg2, T3 arg3) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                // Dispatching to a Ruby method - the method scope sets the proc's converter to itself, hence we don't need to do it here:
                return Method(self, (Proc)(object)proc ,arg0, arg1, arg2, arg3);
            } else {
                return ((CallSite<Func<CallSite, TScope, object, TProc , T0 , T1 , T2 , T3, object>>)callSite).
                    Update(callSite, scope, self, proc ,arg0, arg1, arg2, arg3);
            }
        }
    }
    public class RubyObjectMethodDispatcher<T0, T1, T2, T3, T4> : MethodDispatcher<Func<object, Proc, object, object, object, object, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return new Func<CallSite, object , T0 , T1 , T2 , T3 , T4, object>(Invoke);
        }

        public object Invoke(CallSite/*!*/ callSite, object self ,T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                return Method(self, null ,arg0, arg1, arg2, arg3, arg4);
            } else {
                return ((CallSite<Func<CallSite, object , T0 , T1 , T2 , T3 , T4, object>>)callSite).
                    Update(callSite, self ,arg0, arg1, arg2, arg3, arg4);
            }
        }
    }

    public class RubyObjectMethodDispatcherWithScope<T0, T1, T2, T3, T4> : MethodDispatcher<Func<object, Proc, object, object, object, object, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return isUntyped ?
                (object)new Func<CallSite, object, object , T0 , T1 , T2 , T3 , T4, object>(Invoke<object>) :
                (object)new Func<CallSite, RubyScope, object , T0 , T1 , T2 , T3 , T4, object>(Invoke<RubyScope>);
        }

        public object Invoke<TScope>(CallSite/*!*/ callSite, TScope/*!*/ scope, object self ,T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                return Method(self, null ,arg0, arg1, arg2, arg3, arg4);
            } else {
                return ((CallSite<Func<CallSite, TScope, object , T0 , T1 , T2 , T3 , T4, object>>)callSite).
                    Update(callSite, scope, self ,arg0, arg1, arg2, arg3, arg4);
            }
        }
    }

    public class RubyObjectMethodDispatcherWithBlock<T0, T1, T2, T3, T4> : MethodDispatcher<Func<object, Proc, object, object, object, object, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return isUntyped ? 
                (object)new Func<CallSite, object, object , T0 , T1 , T2 , T3 , T4, object>(Invoke<object>) :
                (object)new Func<CallSite, object, Proc , T0 , T1 , T2 , T3 , T4, object>(Invoke<Proc>);
        }

        public object Invoke<TProc>(CallSite/*!*/ callSite, object self, TProc proc ,T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                // Dispatching to a Ruby method - the method scope sets the proc's converter to itself, hence we don't need to do it here:
                return Method(self, (Proc)(object)proc ,arg0, arg1, arg2, arg3, arg4);
            } else {
                return ((CallSite<Func<CallSite, object, TProc , T0 , T1 , T2 , T3 , T4, object>>)callSite).
                    Update(callSite, self, proc ,arg0, arg1, arg2, arg3, arg4);
            }
        }
    }

    public class RubyObjectMethodDispatcherWithScopeAndBlock<T0, T1, T2, T3, T4> : MethodDispatcher<Func<object, Proc, object, object, object, object, object, object>> {
        public override object/*!*/ CreateDelegate(bool isUntyped) {
            return isUntyped ?
                (object)new Func<CallSite, object, object, object , T0 , T1 , T2 , T3 , T4, object>(Invoke<object, object>) :
                (object)new Func<CallSite, RubyScope, object, Proc , T0 , T1 , T2 , T3 , T4, object>(Invoke<RubyScope, Proc>);
            
        }

        public object Invoke<TScope, TProc>(CallSite/*!*/ callSite, TScope/*!*/ scope, object self, TProc proc ,T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            IRubyObject obj = self as IRubyObject;
            if (obj != null && obj.ImmediateClass.Version.Method == Version) {
                // Dispatching to a Ruby method - the method scope sets the proc's converter to itself, hence we don't need to do it here:
                return Method(self, (Proc)(object)proc ,arg0, arg1, arg2, arg3, arg4);
            } else {
                return ((CallSite<Func<CallSite, TScope, object, TProc , T0 , T1 , T2 , T3 , T4, object>>)callSite).
                    Update(callSite, scope, self, proc ,arg0, arg1, arg2, arg3, arg4);
            }
        }
    }

#endregion
}


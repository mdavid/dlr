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


using System.Runtime.Serialization;
using Microsoft.Scripting.Actions;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using IronRuby.Runtime;
using IronRuby.Runtime.Calls;
using System.Security.Permissions;
using IronRuby.Compiler.Generation;
using System.Diagnostics;

namespace IronRuby.Builtins {
    /// <summary>
    /// The type to represent user objects that inherit from Object
    /// 
    /// Note that for classes that inherit from some other class, RubyTypeDispenser gets used
    /// </summary>
    [DebuggerDisplay("{Inspect().ConvertToString()}")]
    public partial class RubyObject : IRubyObject, IDuplicable, ISerializable {
        private RubyInstanceData _instanceData;
        private RubyClass/*!*/ _immediateClass;

        public RubyObject(RubyClass/*!*/ cls) {
            Assert.NotNull(cls);
            Debug.Assert(!cls.IsSingletonClass);
            _immediateClass = cls;
        }

        public override string/*!*/ ToString() {
#if DEBUG && !SILVERLIGHT && !SYSTEM_CORE
            if (RubyBinder._DumpingExpression) {
                return ToMutableString().ToString();
            }
#endif
            // Translate ToString to to_s conversion for .NET callers.
            var site = _immediateClass.StringConversionSite;
            return site.Target(site, this).ToString();
        }

        public override bool Equals(object other) {
            if (ReferenceEquals(this, other)) {
                // Handle this directly here. Otherwise it can cause infinite recurion when running
                // script code below as the DLR code needs to call Equals for templating of rules
                return true;
            }
            
            var site = _immediateClass.EqualsSite;
            object equalsResult = site.Target(site, this, other);
            if (equalsResult == RubyOps.ForwardToBase) {
                return base.Equals(other);
            }

            return RubyOps.IsTrue(equalsResult);
        }

        public bool BaseEquals(object other) {
            return base.Equals(other);
        }

        public override int GetHashCode() {
            var site = _immediateClass.GetHashCodeSite;
            object hashResult = site.Target(site, this);
            if (ReferenceEquals(hashResult, RubyOps.ForwardToBase)) {
                return base.GetHashCode();
            }

            return Protocols.ToHashCode(hashResult);
        }

        public int BaseGetHashCode() {
            return base.GetHashCode();
        }

        public MutableString/*!*/ ToMutableString() {
            return RubyUtils.FormatObject(_immediateClass.GetNonSingletonClass().Name, GetInstanceData().ObjectId, IsTainted);
        }

        public MutableString/*!*/ Inspect() {
            return _immediateClass.Context.Inspect(this);
        }

#if !SILVERLIGHT
        protected RubyObject(SerializationInfo/*!*/ info, StreamingContext context) {
            RubyOps.DeserializeObject(out _instanceData, out _immediateClass, info);
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(SerializationInfo/*!*/ info, StreamingContext context) {
            RubyOps.SerializeObject(_instanceData, _immediateClass, info);
        }
#endif

        protected virtual RubyObject/*!*/ CreateInstance() {
            return new RubyObject(_immediateClass.NominalClass);
        }

        object IDuplicable.Duplicate(RubyContext/*!*/ context, bool copySingletonMembers) {
            var result = CreateInstance();
            context.CopyInstanceData(this, result, copySingletonMembers);
            return result;
        }

        #region IRubyObject

        [Emitted]
        public RubyClass/*!*/ ImmediateClass {
            get { return _immediateClass; }
            set { _immediateClass = value; }
        }

        public RubyInstanceData/*!*/ GetInstanceData() {
            return RubyOps.GetInstanceData(ref _instanceData);
        }

        public RubyInstanceData TryGetInstanceData() {
            return _instanceData;
        }

        public bool IsFrozen {
            get { return _instanceData != null && _instanceData.Frozen; }
        }

        public bool IsTainted {
            get { return _instanceData != null && _instanceData.Tainted; }
            set { GetInstanceData().Tainted = value; }
        }

        public void Freeze() {
            GetInstanceData().Freeze();
        }

        #endregion
    }
}

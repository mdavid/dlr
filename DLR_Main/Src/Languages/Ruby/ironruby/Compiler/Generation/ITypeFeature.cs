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
using System.Reflection.Emit;

namespace IronRuby.Compiler.Generation {
    internal interface ITypeFeature {
        bool CanInherit { get; }
        bool IsImplementedBy(Type type);
        IFeatureBuilder MakeBuilder(TypeBuilder tb);
    }

    internal interface IFeatureBuilder {
        void Implement(ClsTypeEmitter emitter);
    }
}

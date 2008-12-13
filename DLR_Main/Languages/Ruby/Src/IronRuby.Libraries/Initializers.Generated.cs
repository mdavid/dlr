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


[assembly: IronRuby.Runtime.RubyLibraryAttribute(typeof(IronRuby.Builtins.BuiltinsLibraryInitializer))]
[assembly: IronRuby.Runtime.RubyLibraryAttribute(typeof(IronRuby.StandardLibrary.Threading.ThreadingLibraryInitializer))]
[assembly: IronRuby.Runtime.RubyLibraryAttribute(typeof(IronRuby.StandardLibrary.Sockets.SocketsLibraryInitializer))]
[assembly: IronRuby.Runtime.RubyLibraryAttribute(typeof(IronRuby.StandardLibrary.OpenSsl.OpenSslLibraryInitializer))]
[assembly: IronRuby.Runtime.RubyLibraryAttribute(typeof(IronRuby.StandardLibrary.Digest.DigestLibraryInitializer))]
[assembly: IronRuby.Runtime.RubyLibraryAttribute(typeof(IronRuby.StandardLibrary.Zlib.ZlibLibraryInitializer))]
[assembly: IronRuby.Runtime.RubyLibraryAttribute(typeof(IronRuby.StandardLibrary.StringIO.StringIOLibraryInitializer))]
[assembly: IronRuby.Runtime.RubyLibraryAttribute(typeof(IronRuby.StandardLibrary.StringScanner.StringScannerLibraryInitializer))]
[assembly: IronRuby.Runtime.RubyLibraryAttribute(typeof(IronRuby.StandardLibrary.Enumerator.EnumeratorLibraryInitializer))]
[assembly: IronRuby.Runtime.RubyLibraryAttribute(typeof(IronRuby.StandardLibrary.FunctionControl.FunctionControlLibraryInitializer))]
[assembly: IronRuby.Runtime.RubyLibraryAttribute(typeof(IronRuby.StandardLibrary.FileControl.FileControlLibraryInitializer))]
[assembly: IronRuby.Runtime.RubyLibraryAttribute(typeof(IronRuby.StandardLibrary.BigDecimal.BigDecimalLibraryInitializer))]
[assembly: IronRuby.Runtime.RubyLibraryAttribute(typeof(IronRuby.StandardLibrary.Iconv.IconvLibraryInitializer))]
[assembly: IronRuby.Runtime.RubyLibraryAttribute(typeof(IronRuby.StandardLibrary.IronRubyModule.IronRubyModuleLibraryInitializer))]
[assembly: IronRuby.Runtime.RubyLibraryAttribute(typeof(IronRuby.StandardLibrary.ParseTree.ParseTreeLibraryInitializer))]

namespace IronRuby.Builtins {
    public sealed class BuiltinsLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            Context.RegisterPrimitives(
                new Action<IronRuby.Builtins.RubyModule>(Load__ClassSingleton_Instance),
                new Action<IronRuby.Builtins.RubyModule>(Load__ClassSingletonSingleton_Instance),
                new Action<IronRuby.Builtins.RubyModule>(Load__MainSingleton_Instance),
                new Action<IronRuby.Builtins.RubyModule>(LoadKernel_Instance),
                new Action<IronRuby.Builtins.RubyModule>(LoadObject_Instance),
                new Action<IronRuby.Builtins.RubyModule>(LoadModule_Instance),
                new Action<IronRuby.Builtins.RubyModule>(LoadClass_Instance),
                new Action<IronRuby.Builtins.RubyModule>(LoadKernel_Class),
                new Action<IronRuby.Builtins.RubyModule>(LoadObject_Class),
                new Action<IronRuby.Builtins.RubyModule>(LoadModule_Class),
                new Action<IronRuby.Builtins.RubyModule>(LoadClass_Class)
            );
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(IronRuby.Builtins.RubyObject));
            
            
            // Skipped primitive: __ClassSingleton
            // Skipped primitive: __MainSingleton
            IronRuby.Builtins.RubyModule def30 = DefineGlobalModule("Comparable", typeof(IronRuby.Builtins.Comparable), true, new Action<IronRuby.Builtins.RubyModule>(LoadComparable_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def21 = DefineGlobalModule("Enumerable", typeof(IronRuby.Builtins.Enumerable), true, new Action<IronRuby.Builtins.RubyModule>(LoadEnumerable_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def3 = DefineGlobalModule("Errno", typeof(IronRuby.Builtins.Errno), true, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def17 = DefineModule("File::Constants", typeof(IronRuby.Builtins.RubyFileOps.Constants), true, new Action<IronRuby.Builtins.RubyModule>(LoadFile__Constants_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalModule("GC", typeof(IronRuby.Builtins.RubyGC), true, new Action<IronRuby.Builtins.RubyModule>(LoadGC_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadGC_Class), IronRuby.Builtins.RubyModule.EmptyArray);
            // Skipped primitive: Kernel
            DefineGlobalModule("Marshal", typeof(IronRuby.Builtins.RubyMarshal), true, new Action<IronRuby.Builtins.RubyModule>(LoadMarshal_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadMarshal_Class), IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalModule("Math", typeof(IronRuby.Builtins.RubyMath), true, new Action<IronRuby.Builtins.RubyModule>(LoadMath_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadMath_Class), IronRuby.Builtins.RubyModule.EmptyArray);
            ExtendClass(typeof(Microsoft.Scripting.Actions.TypeTracker), new Action<IronRuby.Builtins.RubyModule>(LoadMicrosoft__Scripting__Actions__TypeTracker_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            DefineGlobalModule("ObjectSpace", typeof(IronRuby.Builtins.ObjectSpace), true, null, new Action<IronRuby.Builtins.RubyModule>(LoadObjectSpace_Class), IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def26 = DefineGlobalModule("Precision", typeof(IronRuby.Builtins.Precision), true, new Action<IronRuby.Builtins.RubyModule>(LoadPrecision_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadPrecision_Class), IronRuby.Builtins.RubyModule.EmptyArray);
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyModule def18 = DefineGlobalModule("Process", typeof(IronRuby.Builtins.RubyProcess), true, new Action<IronRuby.Builtins.RubyModule>(LoadProcess_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadProcess_Class), IronRuby.Builtins.RubyModule.EmptyArray);
            #endif
            #if !SILVERLIGHT
            DefineGlobalModule("Signal", typeof(IronRuby.Builtins.Signal), true, null, new Action<IronRuby.Builtins.RubyModule>(LoadSignal_Class), IronRuby.Builtins.RubyModule.EmptyArray);
            #endif
            ExtendClass(typeof(System.Type), new Action<IronRuby.Builtins.RubyModule>(LoadSystem__Type_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            // Skipped primitive: __ClassSingletonSingleton
            #if !SILVERLIGHT
            object def1 = DefineSingleton(new Action<IronRuby.Builtins.RubyModule>(Load__Singleton_ArgFilesSingletonOps_Instance), null, new IronRuby.Builtins.RubyModule[] {def21, });
            #endif
            object def2 = DefineSingleton(new Action<IronRuby.Builtins.RubyModule>(Load__Singleton_EnvironmentSingletonOps_Instance), null, new IronRuby.Builtins.RubyModule[] {def21, });
            ExtendClass(typeof(Microsoft.Scripting.Actions.TypeGroup), new Action<IronRuby.Builtins.RubyModule>(LoadMicrosoft__Scripting__Actions__TypeGroup_Instance), null, new IronRuby.Builtins.RubyModule[] {def21, }, null);
            // Skipped primitive: Object
            DefineGlobalClass("Struct", typeof(IronRuby.Builtins.RubyStruct), false, classRef0, new Action<IronRuby.Builtins.RubyModule>(LoadStruct_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadStruct_Class), new IronRuby.Builtins.RubyModule[] {def21, }, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyClass, System.Object[]>(IronRuby.Builtins.RubyStructOps.AllocatorUndefined),
            });
            ExtendModule(typeof(System.Collections.Generic.IDictionary<System.Object, System.Object>), new Action<IronRuby.Builtins.RubyModule>(LoadSystem__Collections__Generic__IDictionary_Instance), null, new IronRuby.Builtins.RubyModule[] {def21, });
            IronRuby.Builtins.RubyModule def35 = ExtendModule(typeof(System.Collections.IEnumerable), new Action<IronRuby.Builtins.RubyModule>(LoadSystem__Collections__IEnumerable_Instance), null, new IronRuby.Builtins.RubyModule[] {def21, });
            ExtendModule(typeof(System.Collections.IList), new Action<IronRuby.Builtins.RubyModule>(LoadSystem__Collections__IList_Instance), null, new IronRuby.Builtins.RubyModule[] {def21, });
            ExtendModule(typeof(System.IComparable), new Action<IronRuby.Builtins.RubyModule>(LoadSystem__IComparable_Instance), null, new IronRuby.Builtins.RubyModule[] {def30, });
            DefineGlobalClass("Array", typeof(IronRuby.Builtins.RubyArray), false, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadArray_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadArray_Class), new IronRuby.Builtins.RubyModule[] {def21, }, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.CreateArray),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, System.Object, System.Object>(IronRuby.Builtins.ArrayOps.CreateArray),
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.CreateArray),
            });
            DefineGlobalClass("Binding", typeof(IronRuby.Builtins.Binding), false, Context.ObjectClass, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            DefineGlobalClass("ClrString", typeof(System.String), false, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadClrString_Instance), null, new IronRuby.Builtins.RubyModule[] {def35, }, null);
            DefineGlobalClass("Dir", typeof(IronRuby.Builtins.RubyDir), true, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadDir_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadDir_Class), new IronRuby.Builtins.RubyModule[] {def21, }, null);
            #if !SILVERLIGHT
            DefineGlobalClass("Encoding", typeof(IronRuby.Builtins.RubyEncoding), false, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadEncoding_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadEncoding_Class), IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyClass>(IronRuby.Builtins.RubyEncodingOps.Error),
            });
            #endif
            IronRuby.Builtins.RubyClass def31 = Context.ExceptionClass = DefineGlobalClass("Exception", typeof(System.Exception), false, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadException_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadException_Class), IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__Exception) });
            Context.FalseClass = DefineGlobalClass("FalseClass", typeof(IronRuby.Builtins.FalseClass), true, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadFalseClass_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def20 = DefineClass("File::Stat", typeof(System.IO.FileSystemInfo), false, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadFile__Stat_Instance), null, new IronRuby.Builtins.RubyModule[] {def30, }, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.IO.FileSystemInfo>(IronRuby.Builtins.RubyFileOps.RubyStatOps.Create),
            });
            #endif
            DefineGlobalClass("FileTest", typeof(IronRuby.Builtins.FileTestOps), true, Context.ObjectClass, null, new Action<IronRuby.Builtins.RubyModule>(LoadFileTest_Class), IronRuby.Builtins.RubyModule.EmptyArray, null);
            DefineGlobalClass("Hash", typeof(IronRuby.Builtins.Hash), false, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadHash_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadHash_Class), new IronRuby.Builtins.RubyModule[] {def21, }, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.CreateHash),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, System.Object, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.CreateHash),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.CreateHash),
            });
            IronRuby.Builtins.RubyClass def32 = DefineGlobalClass("IO", typeof(IronRuby.Builtins.RubyIO), false, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadIO_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadIO_Class), new IronRuby.Builtins.RubyModule[] {def17, }, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.CreateIO),
                new Func<IronRuby.Builtins.RubyClass, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.CreateIO),
            });
            DefineGlobalClass("MatchData", typeof(IronRuby.Builtins.MatchData), false, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadMatchData_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadMatchData_Class), IronRuby.Builtins.RubyModule.EmptyArray, null);
            DefineGlobalClass("Method", typeof(IronRuby.Builtins.RubyMethod), false, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadMethod_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            // Skipped primitive: Module
            Context.NilClass = DefineGlobalClass("NilClass", typeof(Microsoft.Scripting.DynamicNull), false, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadNilClass_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            IronRuby.Builtins.RubyClass def25 = DefineGlobalClass("Numeric", typeof(IronRuby.Builtins.Numeric), true, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadNumeric_Instance), null, new IronRuby.Builtins.RubyModule[] {def30, }, null);
            DefineGlobalClass("Proc", typeof(IronRuby.Builtins.Proc), false, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadProc_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadProc_Class), IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyClass, System.Object[]>(IronRuby.Builtins.ProcOps.Error),
            });
            #if !SILVERLIGHT && !SILVERLIGHT
            IronRuby.Builtins.RubyClass def19 = DefineClass("Process::Status", typeof(IronRuby.Builtins.RubyProcess.Status), true, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadProcess__Status_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            #endif
            DefineGlobalClass("Range", typeof(IronRuby.Builtins.Range), false, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadRange_Instance), null, new IronRuby.Builtins.RubyModule[] {def21, }, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Builtins.RubyClass, System.Object, System.Object, System.Boolean, IronRuby.Builtins.Range>(IronRuby.Builtins.RangeOps.CreateRange),
            });
            DefineGlobalClass("Regexp", typeof(IronRuby.Builtins.RubyRegex), false, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadRegexp_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadRegexp_Class), new IronRuby.Builtins.RubyModule[] {def21, }, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Create),
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyRegex, System.Int32, System.Object, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Create),
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyRegex, System.Object, System.Object, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Create),
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Create),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Create),
            });
            DefineGlobalClass("String", typeof(IronRuby.Builtins.MutableString), false, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadString_Instance), null, new IronRuby.Builtins.RubyModule[] {def21, def30, }, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Create),
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Create),
            });
            DefineGlobalClass("Symbol", typeof(Microsoft.Scripting.SymbolId), false, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadSymbol_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadSymbol_Class), IronRuby.Builtins.RubyModule.EmptyArray, null);
            DefineGlobalClass("Thread", typeof(System.Threading.Thread), false, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadThread_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadThread_Class), IronRuby.Builtins.RubyModule.EmptyArray, null);
            DefineGlobalClass("ThreadGroup", typeof(IronRuby.Builtins.ThreadGroup), true, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadThreadGroup_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            DefineGlobalClass("Time", typeof(System.DateTime), false, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadTime_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadTime_Class), new IronRuby.Builtins.RubyModule[] {def30, }, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.DateTime>(IronRuby.Builtins.TimeOps.Create),
            });
            Context.TrueClass = DefineGlobalClass("TrueClass", typeof(IronRuby.Builtins.TrueClass), true, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadTrueClass_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            DefineGlobalClass("UnboundMethod", typeof(IronRuby.Builtins.UnboundMethod), true, Context.ObjectClass, new Action<IronRuby.Builtins.RubyModule>(LoadUnboundMethod_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            // Skipped primitive: Class
            IronRuby.Builtins.RubyClass def16 = DefineGlobalClass("File", typeof(IronRuby.Builtins.RubyFile), false, def32, new Action<IronRuby.Builtins.RubyModule>(LoadFile_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadFile_Class), IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyFileOps.CreateIO),
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyFileOps.CreateIO),
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyFileOps.CreateIO),
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyFileOps.CreateIO),
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, System.Int32, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyFileOps.CreateIO),
            });
            DefineGlobalClass("Float", typeof(System.Double), false, def25, new Action<IronRuby.Builtins.RubyModule>(LoadFloat_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadFloat_Class), new IronRuby.Builtins.RubyModule[] {def26, }, null);
            IronRuby.Builtins.RubyClass def33 = DefineGlobalClass("Integer", typeof(IronRuby.Builtins.Integer), true, def25, new Action<IronRuby.Builtins.RubyModule>(LoadInteger_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadInteger_Class), new IronRuby.Builtins.RubyModule[] {def26, }, null);
            DefineGlobalClass("NoMemoryError", typeof(IronRuby.Builtins.NoMemoryError), true, def31, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__NoMemoryError) });
            IronRuby.Builtins.RubyClass def28 = DefineGlobalClass("ScriptError", typeof(IronRuby.Builtins.ScriptError), false, def31, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__ScriptError) });
            IronRuby.Builtins.RubyClass def27 = DefineGlobalClass("SignalException", typeof(IronRuby.Builtins.SignalException), true, def31, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__SignalException) });
            IronRuby.Builtins.RubyClass def29 = Context.StandardErrorClass = DefineGlobalClass("StandardError", typeof(System.SystemException), false, def31, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__StandardError) });
            DefineGlobalClass("SystemExit", typeof(IronRuby.Builtins.SystemExit), false, def31, new Action<IronRuby.Builtins.RubyModule>(LoadSystemExit_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.Object, IronRuby.Builtins.SystemExit>(IronRuby.Builtins.SystemExitOps.Factory),
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Object, IronRuby.Builtins.SystemExit>(IronRuby.Builtins.SystemExitOps.Factory),
            });
            DefineGlobalClass("ArgumentError", typeof(System.ArgumentException), false, def29, new Action<IronRuby.Builtins.RubyModule>(LoadArgumentError_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__ArgumentError) });
            DefineGlobalClass("Bignum", typeof(Microsoft.Scripting.Math.BigInteger), false, def33, new Action<IronRuby.Builtins.RubyModule>(LoadBignum_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            DefineGlobalClass("Fixnum", typeof(System.Int32), false, def33, new Action<IronRuby.Builtins.RubyModule>(LoadFixnum_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadFixnum_Class), IronRuby.Builtins.RubyModule.EmptyArray, null);
            DefineGlobalClass("IndexError", typeof(System.IndexOutOfRangeException), false, def29, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__IndexError) });
            DefineGlobalClass("Interrupt", typeof(IronRuby.Builtins.Interrupt), true, def27, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__Interrupt) });
            IronRuby.Builtins.RubyClass def22 = DefineGlobalClass("IOError", typeof(System.IO.IOException), false, def29, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__IOError) });
            DefineGlobalClass("LoadError", typeof(IronRuby.Builtins.LoadError), false, def28, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__LoadError) });
            DefineGlobalClass("LocalJumpError", typeof(IronRuby.Builtins.LocalJumpError), false, def29, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__LocalJumpError) });
            IronRuby.Builtins.RubyClass def34 = DefineGlobalClass("NameError", typeof(System.MemberAccessException), false, def29, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__NameError) });
            DefineGlobalClass("NotImplementedError", typeof(IronRuby.Builtins.NotImplementedError), false, def28, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__NotImplementedError) });
            IronRuby.Builtins.RubyClass def24 = DefineGlobalClass("RangeError", typeof(System.ArgumentOutOfRangeException), false, def29, new Action<IronRuby.Builtins.RubyModule>(LoadRangeError_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__RangeError) });
            DefineGlobalClass("RegexpError", typeof(IronRuby.Builtins.RegexpError), false, def29, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__RegexpError) });
            DefineGlobalClass("RuntimeError", typeof(IronRuby.Builtins.RuntimeError), true, def29, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__RuntimeError) });
            DefineGlobalClass("SecurityError", typeof(System.Security.SecurityException), false, def29, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__SecurityError) });
            DefineGlobalClass("SyntaxError", typeof(IronRuby.Builtins.SyntaxError), false, def28, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__SyntaxError) });
            IronRuby.Builtins.RubyClass def23 = DefineGlobalClass("SystemCallError", typeof(System.Runtime.InteropServices.ExternalException), false, def29, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Runtime.InteropServices.ExternalException>(IronRuby.Builtins.SystemCallErrorOps.Factory),
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Runtime.InteropServices.ExternalException>(IronRuby.Builtins.SystemCallErrorOps.Factory),
            });
            DefineGlobalClass("SystemStackError", typeof(IronRuby.Builtins.SystemStackError), false, def29, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__SystemStackError) });
            DefineGlobalClass("ThreadError", typeof(IronRuby.Builtins.ThreadError), true, def29, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__ThreadError) });
            DefineGlobalClass("TypeError", typeof(System.InvalidOperationException), false, def29, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__TypeError) });
            DefineGlobalClass("ZeroDivisionError", typeof(System.DivideByZeroException), false, def29, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__ZeroDivisionError) });
            DefineGlobalClass("EOFError", typeof(IronRuby.Builtins.EOFError), true, def22, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__EOFError) });
            IronRuby.Builtins.RubyClass def4 = DefineClass("Errno::EACCES", typeof(IronRuby.Builtins.Errno.AccessError), true, def23, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            IronRuby.Builtins.RubyClass def5 = DefineClass("Errno::EADDRINUSE", typeof(IronRuby.Builtins.Errno.AddressInUseError), true, def23, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            IronRuby.Builtins.RubyClass def6 = DefineClass("Errno::EBADF", typeof(IronRuby.Builtins.Errno.BadFileDescriptorError), true, def23, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            IronRuby.Builtins.RubyClass def7 = DefineClass("Errno::ECONNABORTED", typeof(IronRuby.Builtins.Errno.ConnectionAbortError), true, def23, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            IronRuby.Builtins.RubyClass def8 = DefineClass("Errno::ECONNRESET", typeof(IronRuby.Builtins.Errno.ConnectionResetError), true, def23, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            IronRuby.Builtins.RubyClass def9 = DefineClass("Errno::EDOM", typeof(IronRuby.Builtins.Errno.DomainError), true, def23, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            IronRuby.Builtins.RubyClass def10 = DefineClass("Errno::EEXIST", typeof(IronRuby.Builtins.Errno.ExistError), true, def23, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            IronRuby.Builtins.RubyClass def11 = DefineClass("Errno::EINVAL", typeof(IronRuby.Builtins.Errno.InvalidError), true, def23, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            IronRuby.Builtins.RubyClass def12 = DefineClass("Errno::ENOENT", typeof(IronRuby.Builtins.Errno.NoEntryError), true, def23, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            IronRuby.Builtins.RubyClass def13 = DefineClass("Errno::ENOTCONN", typeof(IronRuby.Builtins.Errno.NotConnectedError), true, def23, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            IronRuby.Builtins.RubyClass def14 = DefineClass("Errno::ENOTDIR", typeof(IronRuby.Builtins.Errno.NotDirectoryError), true, def23, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            IronRuby.Builtins.RubyClass def15 = DefineClass("Errno::EPIPE", typeof(IronRuby.Builtins.Errno.PipeError), true, def23, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            DefineGlobalClass("FloatDomainError", typeof(IronRuby.Builtins.FloatDomainError), true, def24, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__FloatDomainError) });
            DefineGlobalClass("NoMethodError", typeof(System.MissingMethodException), false, def34, new Action<IronRuby.Builtins.RubyModule>(LoadNoMethodError_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__NoMethodError) });
            def16.SetConstant("Constants", def17);
            #if !SILVERLIGHT
            Context.ObjectClass.SetConstant("ARGF", def1);
            #endif
            Context.ObjectClass.SetConstant("ENV", def2);
            #if !SILVERLIGHT
            def16.SetConstant("Stat", def20);
            #endif
            #if !SILVERLIGHT && !SILVERLIGHT
            def18.SetConstant("Status", def19);
            #endif
            def3.SetConstant("EACCES", def4);
            def3.SetConstant("EADDRINUSE", def5);
            def3.SetConstant("EBADF", def6);
            def3.SetConstant("ECONNABORTED", def7);
            def3.SetConstant("ECONNRESET", def8);
            def3.SetConstant("EDOM", def9);
            def3.SetConstant("EEXIST", def10);
            def3.SetConstant("EINVAL", def11);
            def3.SetConstant("ENOENT", def12);
            def3.SetConstant("ENOTCONN", def13);
            def3.SetConstant("ENOTDIR", def14);
            def3.SetConstant("EPIPE", def15);
        }
        
        private void Load__ClassSingleton_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("allocate", 0x51, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyClass>(IronRuby.Builtins.ClassSingletonOps.Allocate),
            });
            
            module.DefineLibraryMethod("inherited", 0x52, new System.Delegate[] {
                new Action<System.Object, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.ClassSingletonOps.Inherited),
            });
            
            module.DefineLibraryMethod("initialize", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Object>(IronRuby.Builtins.ClassSingletonOps.Initialize),
            });
            
            module.DefineLibraryMethod("initialize_copy", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Object, System.Object>(IronRuby.Builtins.ClassSingletonOps.InitializeCopy),
            });
            
            module.DefineLibraryMethod("new", 0x51, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyClass>(IronRuby.Builtins.ClassSingletonOps.New),
            });
            
            module.DefineLibraryMethod("superclass", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.ClassSingletonOps.GetSuperClass),
            });
            
        }
        
        private void Load__ClassSingleton_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
        }
        
        private void Load__ClassSingletonSingleton_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            Load__ClassSingleton_Instance(module);
            module.DefineLibraryMethod("constants", 0x51, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ClassSingletonSingletonOps.GetConstants),
            });
            
            module.DefineLibraryMethod("nesting", 0x51, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ClassSingletonSingletonOps.GetNesting),
            });
            
        }
        
        private void Load__ClassSingletonSingleton_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            Load__ClassSingleton_Class(module);
        }
        
        private void Load__MainSingleton_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("include", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyModule[], IronRuby.Builtins.RubyClass>(IronRuby.Builtins.MainSingletonOps.Include),
            });
            
            module.DefineLibraryMethod("initialize", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Object>(IronRuby.Builtins.MainSingletonOps.Initialize),
            });
            
            module.DefineLibraryMethod("private", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.Object[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.MainSingletonOps.SetPrivateVisibility),
            });
            
            module.DefineLibraryMethod("public", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.Object[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.MainSingletonOps.SetPublicVisibility),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MainSingletonOps.ToS),
            });
            
        }
        
        private void Load__MainSingleton_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
        }
        
        #if !SILVERLIGHT
        private void Load__Singleton_ArgFilesSingletonOps_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("filename", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ArgFilesSingletonOps.GetCurrentFileName),
            });
            
        }
        #endif
        
        private void LoadArgumentError_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.HideMethod("message");
        }
        
        private void LoadArray_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            LoadSystem__Collections__IList_Instance(module);
            module.DefineLibraryMethod("initialize", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.Reinitialize),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyArray, System.Object, System.Object>(IronRuby.Builtins.ArrayOps.Reinitialize),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyArray, System.Int32, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.ReinitializeByRepeatedValue),
            });
            
            module.DefineLibraryMethod("pack", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyArray, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ArrayOps.Pack),
            });
            
            module.DefineLibraryMethod("reverse!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.InPlaceReverse),
            });
            
            module.DefineLibraryMethod("reverse_each", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyArray, System.Object>(IronRuby.Builtins.ArrayOps.ReverseEach),
            });
            
            module.DefineLibraryMethod("sort", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.Sort),
            });
            
            module.DefineLibraryMethod("sort!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.SortInPlace),
            });
            
            module.DefineLibraryMethod("to_a", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.ToArray),
            });
            
            module.DefineLibraryMethod("to_ary", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.ToArray),
            });
            
        }
        
        private void LoadArray_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("[]", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.Object[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.MakeArray),
            });
            
        }
        
        private void LoadBignum_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("-", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.Subtract),
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, System.Object>(IronRuby.Builtins.BignumOps.Subtract),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, System.Object>(IronRuby.Builtins.BignumOps.Subtract),
            });
            
            module.DefineLibraryMethod("%", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.Modulo),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, System.Object>(IronRuby.Builtins.BignumOps.ModuloOp),
            });
            
            module.DefineLibraryMethod("&", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.BignumOps.And),
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.And),
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, System.Object>(IronRuby.Builtins.BignumOps.And),
            });
            
            module.DefineLibraryMethod("*", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.Multiply),
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, System.Object>(IronRuby.Builtins.BignumOps.Multiply),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, System.Object>(IronRuby.Builtins.BignumOps.Multiply),
            });
            
            module.DefineLibraryMethod("**", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.Power),
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.BignumOps.Power),
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, System.Object>(IronRuby.Builtins.BignumOps.Power),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, System.Object>(IronRuby.Builtins.BignumOps.Power),
            });
            
            module.DefineLibraryMethod("/", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.Divide),
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, System.Object>(IronRuby.Builtins.BignumOps.Divide),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, System.Object>(IronRuby.Builtins.BignumOps.Divide),
            });
            
            module.DefineLibraryMethod("-@", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.Negate),
            });
            
            module.DefineLibraryMethod("[]", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Int32>(IronRuby.Builtins.BignumOps.Bit),
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Int32>(IronRuby.Builtins.BignumOps.Bit),
            });
            
            module.DefineLibraryMethod("^", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.BignumOps.Xor),
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.Xor),
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, System.Object>(IronRuby.Builtins.BignumOps.Xor),
            });
            
            module.DefineLibraryMethod("|", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.BignumOps.BitwiseOr),
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.BitwiseOr),
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, System.Object>(IronRuby.Builtins.BignumOps.BitwiseOr),
            });
            
            module.DefineLibraryMethod("~", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.Invert),
            });
            
            module.DefineLibraryMethod("+", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.Add),
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, System.Object>(IronRuby.Builtins.BignumOps.Add),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, System.Object>(IronRuby.Builtins.BignumOps.Add),
            });
            
            module.DefineLibraryMethod("<<", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.BignumOps.LeftShift),
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.LeftShift),
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, System.Object>(IronRuby.Builtins.BignumOps.LeftShift),
            });
            
            module.DefineLibraryMethod("<=>", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Int32>(IronRuby.Builtins.BignumOps.Compare),
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Double, System.Object>(IronRuby.Builtins.BignumOps.Compare),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, System.Object>(IronRuby.Builtins.BignumOps.Compare),
            });
            
            module.DefineLibraryMethod("==", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Boolean>(IronRuby.Builtins.BignumOps.Equal),
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, System.Boolean>(IronRuby.Builtins.BignumOps.Equal),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, System.Boolean>(IronRuby.Builtins.BignumOps.Equal),
            });
            
            module.DefineLibraryMethod(">>", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.BignumOps.RightShift),
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.RightShift),
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, System.Object>(IronRuby.Builtins.BignumOps.RightShift),
            });
            
            module.DefineLibraryMethod("abs", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.Abs),
            });
            
            module.DefineLibraryMethod("coerce", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.BignumOps.Coerce),
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.BignumOps.Coerce),
            });
            
            module.DefineLibraryMethod("div", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.Divide),
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, System.Object>(IronRuby.Builtins.BignumOps.Divide),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, System.Object>(IronRuby.Builtins.BignumOps.Div),
            });
            
            module.DefineLibraryMethod("divmod", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.BignumOps.DivMod),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, System.Object>(IronRuby.Builtins.BignumOps.DivMod),
            });
            
            module.DefineLibraryMethod("eql?", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Boolean>(IronRuby.Builtins.BignumOps.Eql),
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Boolean>(IronRuby.Builtins.BignumOps.Eql),
                new Func<Microsoft.Scripting.Math.BigInteger, System.Object, System.Boolean>(IronRuby.Builtins.BignumOps.Eql),
            });
            
            module.DefineLibraryMethod("hash", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32>(IronRuby.Builtins.BignumOps.Hash),
            });
            
            module.DefineLibraryMethod("modulo", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.Modulo),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, System.Object>(IronRuby.Builtins.BignumOps.Modulo),
            });
            
            module.DefineLibraryMethod("quo", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.Quotient),
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, System.Object>(IronRuby.Builtins.BignumOps.Quotient),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, System.Object>(IronRuby.Builtins.BignumOps.Quotient),
            });
            
            module.DefineLibraryMethod("remainder", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.Remainder),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, System.Object>(IronRuby.Builtins.BignumOps.Remainder),
            });
            
            module.DefineLibraryMethod("size", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32>(IronRuby.Builtins.BignumOps.Size),
            });
            
            module.DefineLibraryMethod("to_f", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Double>(IronRuby.Builtins.BignumOps.ToFloat),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.BignumOps.ToString),
                new Func<Microsoft.Scripting.Math.BigInteger, System.UInt32, System.Object>(IronRuby.Builtins.BignumOps.ToString),
            });
            
        }
        
        private void LoadClass_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.UndefineLibraryMethod("append_features");
            module.UndefineLibraryMethod("extend_object");
            module.UndefineLibraryMethod("module_function");
            module.DefineRuleGenerator("allocate", 0x51, IronRuby.Builtins.ClassOps.GetInstanceAllocator());
            
            module.DefineLibraryMethod("inherited", 0x52, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyClass, System.Object>(IronRuby.Builtins.ClassOps.Inherited),
            });
            
            module.DefineLibraryMethod("initialize", 0x52, new System.Delegate[] {
                new Action<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.ClassOps.Reinitialize),
            });
            
            module.DefineLibraryMethod("initialize_copy", 0x52, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.ClassOps.InitializeCopy),
            });
            
            module.DefineRuleGenerator("new", 0x51, IronRuby.Builtins.ClassOps.GetInstanceConstructor());
            
            module.DefineLibraryMethod("superclass", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.ClassOps.GetSuperclass),
            });
            
        }
        
        private void LoadClass_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
        }
        
        private void LoadClrString_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("==", 0x51, new System.Delegate[] {
                new Func<System.String, System.Object, System.Boolean>(IronRuby.Builtins.StringOps.Equals),
            });
            
            module.DefineLibraryMethod("===", 0x51, new System.Delegate[] {
                new Func<System.String, System.Object, System.Boolean>(IronRuby.Builtins.StringOps.Equals),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.StringOps.Inspect),
            });
            
            module.DefineLibraryMethod("to_clr_string", 0x51, new System.Delegate[] {
                new Func<System.String, System.String>(IronRuby.Builtins.StringOps.ToClrString),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.StringOps.ToStr),
            });
            
            module.DefineLibraryMethod("to_str", 0x51, new System.Delegate[] {
                new Func<System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.StringOps.ToStr),
            });
            
        }
        
        private void LoadComparable_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("<", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.Comparable.Less),
            });
            
            module.DefineLibraryMethod("<=", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.Comparable.LessOrEqual),
            });
            
            module.DefineLibraryMethod("==", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.Comparable.Equal),
            });
            
            module.DefineLibraryMethod(">", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.Comparable.Greater),
            });
            
            module.DefineLibraryMethod(">=", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.Comparable.GreaterOrEqual),
            });
            
            module.DefineLibraryMethod("between?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.Comparable.Between),
            });
            
        }
        
        private void LoadDir_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("close", 0x51, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyDir>(IronRuby.Builtins.RubyDir.Close),
            });
            
            module.DefineLibraryMethod("each", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyDir, IronRuby.Builtins.RubyDir>(IronRuby.Builtins.RubyDir.Each),
            });
            
            module.DefineLibraryMethod("path", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyDir, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyDir.GetPath),
            });
            
            module.DefineLibraryMethod("pos", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyDir, System.Int32>(IronRuby.Builtins.RubyDir.GetCurrentPosition),
            });
            
            module.DefineLibraryMethod("pos=", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyDir, System.Int32, System.Int32>(IronRuby.Builtins.RubyDir.SetPosition),
            });
            
            module.DefineLibraryMethod("read", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyDir, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyDir.Read),
            });
            
            module.DefineLibraryMethod("rewind", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyDir, IronRuby.Builtins.RubyDir>(IronRuby.Builtins.RubyDir.Rewind),
            });
            
            module.DefineLibraryMethod("seek", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyDir, System.Int32, IronRuby.Builtins.RubyDir>(IronRuby.Builtins.RubyDir.Seek),
            });
            
            module.DefineLibraryMethod("tell", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyDir, System.Int32>(IronRuby.Builtins.RubyDir.GetCurrentPosition),
            });
            
        }
        
        private void LoadDir_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("[]", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyDir.Glob),
            });
            
            module.DefineLibraryMethod("chdir", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyDir.ChangeDirectory),
                new Func<System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyDir.ChangeDirectory),
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.RubyDir.ChangeDirectory),
            });
            
            module.DefineLibraryMethod("chroot", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Int32>(IronRuby.Builtins.RubyDir.ChangeRoot),
            });
            
            module.DefineLibraryMethod("delete", 0x61, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyDir.RemoveDirectory),
            });
            
            module.DefineLibraryMethod("entries", 0x61, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyDir.GetEntries),
            });
            
            module.DefineLibraryMethod("foreach", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyDir.ForEach),
            });
            
            module.DefineLibraryMethod("getwd", 0x61, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyDir.GetCurrentDirectory),
            });
            
            module.DefineLibraryMethod("glob", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.RubyDir.Glob),
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyDir.Glob),
            });
            
            module.DefineLibraryMethod("mkdir", 0x61, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.MutableString, System.Object, System.Int32>(IronRuby.Builtins.RubyDir.MakeDirectory),
            });
            
            module.DefineLibraryMethod("open", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyDir.Open),
                new Func<System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyDir.Open),
            });
            
            module.DefineLibraryMethod("pwd", 0x61, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyDir.GetCurrentDirectory),
            });
            
            module.DefineLibraryMethod("rmdir", 0x61, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyDir.RemoveDirectory),
            });
            
            module.DefineLibraryMethod("unlink", 0x61, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyDir.RemoveDirectory),
            });
            
        }
        
        #if !SILVERLIGHT
        private void LoadEncoding_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("_dump", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyEncoding, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyEncodingOps.ToS),
            });
            
            module.DefineLibraryMethod("based_encoding", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyEncoding, IronRuby.Builtins.RubyEncoding>(IronRuby.Builtins.RubyEncodingOps.BasedEncoding),
            });
            
            module.DefineLibraryMethod("dummy?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyEncoding, System.Boolean>(IronRuby.Builtins.RubyEncodingOps.IsDummy),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyEncoding, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyEncodingOps.Inspect),
            });
            
            module.DefineLibraryMethod("name", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyEncoding, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyEncodingOps.ToS),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyEncoding, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyEncodingOps.ToS),
            });
            
        }
        #endif
        
        #if !SILVERLIGHT
        private void LoadEncoding_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("_load?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.Boolean>(IronRuby.Builtins.RubyEncodingOps.Load),
            });
            
            module.DefineLibraryMethod("compatible?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyEncoding, IronRuby.Builtins.RubyEncoding, System.Boolean>(IronRuby.Builtins.RubyEncodingOps.IsCompatible),
            });
            
            module.DefineLibraryMethod("default_external", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyEncoding>(IronRuby.Builtins.RubyEncodingOps.GetDefaultEncoding),
            });
            
            module.DefineLibraryMethod("find", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyEncoding>(IronRuby.Builtins.RubyEncodingOps.GetEncoding),
            });
            
            module.DefineLibraryMethod("list", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyEncodingOps.GetAvailableEncodings),
            });
            
            module.DefineLibraryMethod("locale_charmap", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyEncodingOps.GetDefaultCharmap),
            });
            
        }
        #endif
        
        private void LoadEnumerable_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("all?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.TrueForAll),
            });
            
            module.DefineLibraryMethod("any?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.TrueForAny),
            });
            
            module.DefineLibraryMethod("collect", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Enumerable.Map),
            });
            
            module.DefineLibraryMethod("detect", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, System.Object>>, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.Enumerable.Find),
            });
            
            module.DefineLibraryMethod("each_with_index", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.EachWithIndex),
            });
            
            module.DefineLibraryMethod("entries", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Enumerable.ToArray),
            });
            
            module.DefineLibraryMethod("find", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, System.Object>>, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.Enumerable.Find),
            });
            
            module.DefineLibraryMethod("find_all", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Enumerable.Select),
            });
            
            module.DefineLibraryMethod("grep", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Enumerable.Grep),
            });
            
            module.DefineLibraryMethod("include?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.Enumerable.Contains),
            });
            
            module.DefineLibraryMethod("inject", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.Enumerable.Inject),
            });
            
            module.DefineLibraryMethod("map", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Enumerable.Map),
            });
            
            module.DefineLibraryMethod("max", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.GetMaximum),
            });
            
            module.DefineLibraryMethod("member?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.Enumerable.Contains),
            });
            
            module.DefineLibraryMethod("min", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.GetMinimum),
            });
            
            module.DefineLibraryMethod("partition", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Enumerable.Partition),
            });
            
            module.DefineLibraryMethod("reject", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Enumerable.Reject),
            });
            
            module.DefineLibraryMethod("select", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Enumerable.Select),
            });
            
            module.DefineLibraryMethod("sort", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.Sort),
            });
            
            module.DefineLibraryMethod("sort_by", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Enumerable.SortBy),
            });
            
            module.DefineLibraryMethod("to_a", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Enumerable.ToArray),
            });
            
            module.DefineLibraryMethod("zip", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Enumerable.Zip),
            });
            
        }
        
        private void Load__Singleton_EnvironmentSingletonOps_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("[]", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.EnvironmentSingletonOps.GetVariable),
            });
            
            module.DefineLibraryMethod("[]=", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.EnvironmentSingletonOps.SetVariable),
            });
            
            module.DefineLibraryMethod("clear", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.Clear),
            });
            
            module.DefineLibraryMethod("delete", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.Delete),
            });
            
            module.DefineLibraryMethod("delete_if", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.DeleteIf),
            });
            
            module.DefineLibraryMethod("each", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.Each),
            });
            
            module.DefineLibraryMethod("each_key", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.EachKey),
            });
            
            module.DefineLibraryMethod("each_pair", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.Each),
            });
            
            module.DefineLibraryMethod("each_value", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.EachValue),
            });
            
            module.DefineLibraryMethod("empty?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Boolean>(IronRuby.Builtins.EnvironmentSingletonOps.IsEmpty),
            });
            
            module.DefineLibraryMethod("fetch", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.EnvironmentSingletonOps.GetVariable),
            });
            
            module.DefineLibraryMethod("has_key?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.EnvironmentSingletonOps.HasKey),
            });
            
            module.DefineLibraryMethod("has_value?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.EnvironmentSingletonOps.HasValue),
            });
            
            module.DefineLibraryMethod("include?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.EnvironmentSingletonOps.HasKey),
            });
            
            module.DefineLibraryMethod("index", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.EnvironmentSingletonOps.Index),
            });
            
            module.DefineLibraryMethod("indexes", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.EnvironmentSingletonOps.Index),
            });
            
            module.DefineLibraryMethod("indices", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.EnvironmentSingletonOps.Indices),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.EnvironmentSingletonOps.Inspect),
            });
            
            module.DefineLibraryMethod("invert", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Hash>(IronRuby.Builtins.EnvironmentSingletonOps.Invert),
            });
            
            module.DefineLibraryMethod("key?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.EnvironmentSingletonOps.HasKey),
            });
            
            module.DefineLibraryMethod("keys", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.EnvironmentSingletonOps.Keys),
            });
            
            module.DefineLibraryMethod("length", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32>(IronRuby.Builtins.EnvironmentSingletonOps.Length),
            });
            
            module.DefineLibraryMethod("rehash", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.Rehash),
            });
            
            module.DefineLibraryMethod("reject!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.DeleteIf),
            });
            
            module.DefineLibraryMethod("replace", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Hash, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.Replace),
            });
            
            module.DefineLibraryMethod("shift", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.Shift),
            });
            
            module.DefineLibraryMethod("size", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32>(IronRuby.Builtins.EnvironmentSingletonOps.Length),
            });
            
            module.DefineLibraryMethod("store", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.EnvironmentSingletonOps.SetVariable),
            });
            
            module.DefineLibraryMethod("to_hash", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Hash>(IronRuby.Builtins.EnvironmentSingletonOps.ToHash),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.EnvironmentSingletonOps.ToString),
            });
            
            module.DefineLibraryMethod("update", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Hash, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.Update),
            });
            
            module.DefineLibraryMethod("value?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.EnvironmentSingletonOps.HasValue),
            });
            
            module.DefineLibraryMethod("values", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.EnvironmentSingletonOps.Values),
            });
            
            module.DefineLibraryMethod("values_at", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.EnvironmentSingletonOps.ValuesAt),
            });
            
        }
        
        private void LoadException_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("backtrace", 0x51, new System.Delegate[] {
                new Func<System.Exception, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ExceptionOps.GetBacktrace),
            });
            
            module.DefineRuleGenerator("exception", 0x51, IronRuby.Builtins.ExceptionOps.GetException());
            
            module.DefineLibraryMethod("initialize", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Exception, System.Object, System.Exception>(IronRuby.Builtins.ExceptionOps.ReinitializeException),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, System.Exception, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ExceptionOps.Inspect),
            });
            
            module.DefineLibraryMethod("message", 0x51, new System.Delegate[] {
                new Func<System.Exception, System.Object>(IronRuby.Builtins.ExceptionOps.GetMessage),
            });
            
            module.DefineLibraryMethod("set_backtrace", 0x51, new System.Delegate[] {
                new Func<System.Exception, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ExceptionOps.SetBacktrace),
                new Func<System.Exception, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ExceptionOps.SetBacktrace),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, System.Exception, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ExceptionOps.GetMessage),
            });
            
            module.DefineLibraryMethod("to_str", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, System.Exception, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ExceptionOps.GetMessage),
            });
            
        }
        
        private void LoadException_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineRuleGenerator("exception", 0x61, IronRuby.Builtins.ExceptionOps.CreateException());
            
        }
        
        private void LoadFalseClass_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("&", 0x51, new System.Delegate[] {
                new Func<System.Boolean, System.Object, System.Boolean>(IronRuby.Builtins.FalseClass.And),
            });
            
            module.DefineLibraryMethod("^", 0x51, new System.Delegate[] {
                new Func<System.Boolean, System.Object, System.Boolean>(IronRuby.Builtins.FalseClass.Xor),
                new Func<System.Boolean, System.Boolean, System.Boolean>(IronRuby.Builtins.FalseClass.Xor),
            });
            
            module.DefineLibraryMethod("|", 0x51, new System.Delegate[] {
                new Func<System.Boolean, System.Object, System.Boolean>(IronRuby.Builtins.FalseClass.Or),
                new Func<System.Boolean, System.Boolean, System.Boolean>(IronRuby.Builtins.FalseClass.Or),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<System.Boolean, IronRuby.Builtins.MutableString>(IronRuby.Builtins.FalseClass.ToString),
            });
            
        }
        
        private void LoadFile_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("ALT_SEPARATOR", IronRuby.Builtins.RubyFileOps.ALT_SEPARATOR);
            module.SetConstant("PATH_SEPARATOR", IronRuby.Builtins.RubyFileOps.PATH_SEPARATOR);
            module.SetConstant("Separator", IronRuby.Builtins.RubyFileOps.Separator);
            module.SetConstant("SEPARATOR", IronRuby.Builtins.RubyFileOps.SEPARATOR);
            
            module.DefineLibraryMethod("atime", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyFile, System.DateTime>(IronRuby.Builtins.RubyFileOps.AccessTime),
            });
            
            module.DefineLibraryMethod("ctime", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyFile, System.DateTime>(IronRuby.Builtins.RubyFileOps.CreateTime),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyFile, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.Inspect),
            });
            
            module.DefineLibraryMethod("lstat", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyFile, System.IO.FileSystemInfo>(IronRuby.Builtins.RubyFileOps.Stat),
            });
            
            module.DefineLibraryMethod("mtime", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyFile, System.DateTime>(IronRuby.Builtins.RubyFileOps.ModifiedTime),
            });
            
            module.DefineLibraryMethod("path", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyFile, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.GetPath),
            });
            
        }
        
        private void LoadFile_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("atime", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.DateTime>(IronRuby.Builtins.RubyFileOps.AccessTime),
            });
            
            module.DefineLibraryMethod("basename", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.Basename),
            });
            
            module.DefineLibraryMethod("blockdev?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsBlockDevice),
            });
            
            module.DefineLibraryMethod("chardev?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsCharDevice),
            });
            
            module.DefineLibraryMethod("chmod", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.Int32, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyFileOps.Chmod),
            });
            
            module.DefineLibraryMethod("ctime", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.DateTime>(IronRuby.Builtins.RubyFileOps.CreateTime),
            });
            
            module.DefineLibraryMethod("delete", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyFileOps.Delete),
                new Func<IronRuby.Builtins.RubyClass, System.Object[], System.Int32>(IronRuby.Builtins.RubyFileOps.Delete),
            });
            
            module.DefineLibraryMethod("directory?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsDirectory),
            });
            
            module.DefineLibraryMethod("dirname", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.DirName),
            });
            
            module.DefineLibraryMethod("executable?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsExecutable),
            });
            
            module.DefineLibraryMethod("executable_real?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsExecutable),
            });
            
            module.DefineLibraryMethod("exist?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.Exists),
            });
            
            module.DefineLibraryMethod("exists?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.Exists),
            });
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("expand_path", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.ExpandPath),
            });
            
            #endif
            module.DefineLibraryMethod("extname", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.GetExtension),
            });
            
            module.DefineLibraryMethod("file?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsAFile),
            });
            
            module.DefineLibraryMethod("fnmatch", 0x61, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, System.Boolean>(IronRuby.Builtins.RubyFileOps.FnMatch),
            });
            
            module.DefineLibraryMethod("fnmatch?", 0x61, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, System.Boolean>(IronRuby.Builtins.RubyFileOps.FnMatch),
            });
            
            module.DefineLibraryMethod("ftype", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.FileType),
            });
            
            module.DefineLibraryMethod("grpowned?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsGroupOwned),
            });
            
            module.DefineLibraryMethod("join", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Builtins.RubyClass, System.Object[], IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.Join),
            });
            
            module.DefineLibraryMethod("lstat", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.IO.FileSystemInfo>(IronRuby.Builtins.RubyFileOps.Stat),
            });
            
            module.DefineLibraryMethod("mtime", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.DateTime>(IronRuby.Builtins.RubyFileOps.ModifiedTime),
            });
            
            module.DefineLibraryMethod("open", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyFileOps.Open),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, System.Object, System.Object, System.Object>(IronRuby.Builtins.RubyFileOps.Open),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyFileOps.Open),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, System.Object, System.Object>(IronRuby.Builtins.RubyFileOps.Open),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.RubyFileOps.Open),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, System.Object, System.Int32, System.Object>(IronRuby.Builtins.RubyFileOps.Open),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, System.Object, System.Int32, System.Int32, System.Object>(IronRuby.Builtins.RubyFileOps.Open),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.RubyFileOps.Open),
            });
            
            module.DefineLibraryMethod("owned?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsUserOwned),
            });
            
            module.DefineLibraryMethod("pipe?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsPipe),
            });
            
            module.DefineLibraryMethod("readable?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsReadable),
            });
            
            module.DefineLibraryMethod("readable_real?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsReadable),
            });
            
            module.DefineLibraryMethod("readlink", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.Readlink),
            });
            
            module.DefineLibraryMethod("rename", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyFileOps.Rename),
            });
            
            module.DefineLibraryMethod("setgid?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsSetGid),
            });
            
            module.DefineLibraryMethod("setuid?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsSetUid),
            });
            
            module.DefineLibraryMethod("size", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyFileOps.Size),
            });
            
            module.DefineLibraryMethod("size?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyFileOps.NullableSize),
            });
            
            module.DefineLibraryMethod("socket?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsSocket),
            });
            
            module.DefineLibraryMethod("split", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyFileOps.Split),
            });
            
            module.DefineLibraryMethod("stat", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.IO.FileSystemInfo>(IronRuby.Builtins.RubyFileOps.Stat),
            });
            
            module.DefineLibraryMethod("sticky?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsSticky),
            });
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("symlink", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyFileOps.SymLink),
            });
            
            #endif
            #if !SILVERLIGHT
            module.DefineLibraryMethod("symlink?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsSymLink),
            });
            
            #endif
            module.DefineLibraryMethod("unlink", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyFileOps.Delete),
                new Func<IronRuby.Builtins.RubyClass, System.Object[], System.Int32>(IronRuby.Builtins.RubyFileOps.Delete),
            });
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("utime", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.DateTime, System.DateTime, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyFileOps.UpdateTimes),
                new Func<IronRuby.Builtins.RubyClass, System.Object, System.Object, System.Object[], System.Int32>(IronRuby.Builtins.RubyFileOps.UpdateTimes),
            });
            
            #endif
            module.DefineLibraryMethod("writable?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsWritable),
            });
            
            module.DefineLibraryMethod("writable_real?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsWritable),
            });
            
            module.DefineLibraryMethod("zero?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.IsZeroLength),
            });
            
        }
        
        private void LoadFile__Constants_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("APPEND", IronRuby.Builtins.RubyFileOps.Constants.APPEND);
            module.SetConstant("BINARY", IronRuby.Builtins.RubyFileOps.Constants.BINARY);
            module.SetConstant("CREAT", IronRuby.Builtins.RubyFileOps.Constants.CREAT);
            module.SetConstant("EXCL", IronRuby.Builtins.RubyFileOps.Constants.EXCL);
            module.SetConstant("FNM_CASEFOLD", IronRuby.Builtins.RubyFileOps.Constants.FNM_CASEFOLD);
            module.SetConstant("FNM_DOTMATCH", IronRuby.Builtins.RubyFileOps.Constants.FNM_DOTMATCH);
            module.SetConstant("FNM_NOESCAPE", IronRuby.Builtins.RubyFileOps.Constants.FNM_NOESCAPE);
            module.SetConstant("FNM_PATHNAME", IronRuby.Builtins.RubyFileOps.Constants.FNM_PATHNAME);
            module.SetConstant("FNM_SYSCASE", IronRuby.Builtins.RubyFileOps.Constants.FNM_SYSCASE);
            module.SetConstant("LOCK_EX", IronRuby.Builtins.RubyFileOps.Constants.LOCK_EX);
            module.SetConstant("LOCK_NB", IronRuby.Builtins.RubyFileOps.Constants.LOCK_NB);
            module.SetConstant("LOCK_SH", IronRuby.Builtins.RubyFileOps.Constants.LOCK_SH);
            module.SetConstant("LOCK_UN", IronRuby.Builtins.RubyFileOps.Constants.LOCK_UN);
            module.SetConstant("NONBLOCK", IronRuby.Builtins.RubyFileOps.Constants.NONBLOCK);
            module.SetConstant("RDONLY", IronRuby.Builtins.RubyFileOps.Constants.RDONLY);
            module.SetConstant("RDWR", IronRuby.Builtins.RubyFileOps.Constants.RDWR);
            module.SetConstant("SEEK_CUR", IronRuby.Builtins.RubyFileOps.Constants.SEEK_CUR);
            module.SetConstant("SEEK_END", IronRuby.Builtins.RubyFileOps.Constants.SEEK_END);
            module.SetConstant("SEEK_SET", IronRuby.Builtins.RubyFileOps.Constants.SEEK_SET);
            module.SetConstant("TRUNC", IronRuby.Builtins.RubyFileOps.Constants.TRUNC);
            module.SetConstant("WRONLY", IronRuby.Builtins.RubyFileOps.Constants.WRONLY);
            
        }
        
        #if !SILVERLIGHT
        private void LoadFile__Stat_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("<=>", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.IO.FileSystemInfo, System.Int32>(IronRuby.Builtins.RubyFileOps.RubyStatOps.Compare),
                new Func<System.IO.FileSystemInfo, System.Object, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.Compare),
            });
            
            module.DefineLibraryMethod("atime", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.DateTime>(IronRuby.Builtins.RubyFileOps.RubyStatOps.AccessTime),
            });
            
            module.DefineLibraryMethod("blksize", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.BlockSize),
            });
            
            module.DefineLibraryMethod("blockdev?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsBlockDevice),
            });
            
            module.DefineLibraryMethod("blocks", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.Blocks),
            });
            
            module.DefineLibraryMethod("chardev?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsCharDevice),
            });
            
            module.DefineLibraryMethod("ctime", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.DateTime>(IronRuby.Builtins.RubyFileOps.RubyStatOps.CreateTime),
            });
            
            module.DefineLibraryMethod("dev", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.DeviceId),
            });
            
            module.DefineLibraryMethod("dev_major", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.DeviceIdMajor),
            });
            
            module.DefineLibraryMethod("dev_minor", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.DeviceIdMinor),
            });
            
            module.DefineLibraryMethod("directory?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsDirectory),
            });
            
            module.DefineLibraryMethod("executable?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsExecutable),
            });
            
            module.DefineLibraryMethod("executable_real?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsExecutable),
            });
            
            module.DefineLibraryMethod("file?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsFile),
            });
            
            module.DefineLibraryMethod("ftype", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.RubyStatOps.FileType),
            });
            
            module.DefineLibraryMethod("gid", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Int32>(IronRuby.Builtins.RubyFileOps.RubyStatOps.GroupId),
            });
            
            module.DefineLibraryMethod("grpowned?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsGroupOwned),
            });
            
            module.DefineLibraryMethod("ino", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Int32>(IronRuby.Builtins.RubyFileOps.RubyStatOps.Inode),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.IO.FileSystemInfo, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.RubyStatOps.Inspect),
            });
            
            module.DefineLibraryMethod("mode", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Int32>(IronRuby.Builtins.RubyFileOps.RubyStatOps.Mode),
            });
            
            module.DefineLibraryMethod("mtime", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.DateTime>(IronRuby.Builtins.RubyFileOps.RubyStatOps.ModifiedTime),
            });
            
            module.DefineLibraryMethod("nlink", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Int32>(IronRuby.Builtins.RubyFileOps.RubyStatOps.NumberOfLinks),
            });
            
            module.DefineLibraryMethod("owned?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsUserOwned),
            });
            
            module.DefineLibraryMethod("pipe?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsPipe),
            });
            
            module.DefineLibraryMethod("rdev", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.DeviceId),
            });
            
            module.DefineLibraryMethod("rdev_major", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.DeviceIdMajor),
            });
            
            module.DefineLibraryMethod("rdev_minor", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.DeviceIdMinor),
            });
            
            module.DefineLibraryMethod("readable?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsReadable),
            });
            
            module.DefineLibraryMethod("readable_real?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsReadable),
            });
            
            module.DefineLibraryMethod("setgid?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsSetGid),
            });
            
            module.DefineLibraryMethod("setuid?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsSetUid),
            });
            
            module.DefineLibraryMethod("size", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Int32>(IronRuby.Builtins.RubyFileOps.RubyStatOps.Size),
            });
            
            module.DefineLibraryMethod("size?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.NullableSize),
            });
            
            module.DefineLibraryMethod("socket?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsSocket),
            });
            
            module.DefineLibraryMethod("sticky?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsSticky),
            });
            
            module.DefineLibraryMethod("symlink?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsSymLink),
            });
            
            module.DefineLibraryMethod("uid", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Int32>(IronRuby.Builtins.RubyFileOps.RubyStatOps.UserId),
            });
            
            module.DefineLibraryMethod("writable?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsWritable),
            });
            
            module.DefineLibraryMethod("writable_real?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsWritable),
            });
            
            module.DefineLibraryMethod("zero?", 0x51, new System.Delegate[] {
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsZeroLength),
            });
            
        }
        #endif
        
        private void LoadFileTest_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("exist?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTestOps.Exists),
            });
            
            module.DefineLibraryMethod("exists?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTestOps.Exists),
            });
            
        }
        
        private void LoadFixnum_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("-", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.FixnumOps.Subtract),
                new Func<System.Int32, System.Double, System.Double>(IronRuby.Builtins.FixnumOps.Subtract),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.FixnumOps.Subtract),
            });
            
            module.DefineLibraryMethod("%", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.FixnumOps.ModuloOp),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.FixnumOps.ModuloOp),
            });
            
            module.DefineLibraryMethod("&", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.FixnumOps.BitwiseAnd),
                new Func<System.Int32, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.FixnumOps.BitwiseAnd),
                new Func<IronRuby.Runtime.RubyContext, System.Int32, System.Object, System.Object>(IronRuby.Builtins.FixnumOps.BitwiseAnd),
            });
            
            module.DefineLibraryMethod("*", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.FixnumOps.Multiply),
                new Func<System.Int32, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.FixnumOps.Multiply),
                new Func<System.Int32, System.Double, System.Double>(IronRuby.Builtins.FixnumOps.Multiply),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.FixnumOps.Multiply),
            });
            
            module.DefineLibraryMethod("**", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.FixnumOps.Power),
                new Func<System.Int32, System.Double, System.Double>(IronRuby.Builtins.FixnumOps.Power),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Int32, System.Object, System.Object>(IronRuby.Builtins.FixnumOps.Power),
            });
            
            module.DefineLibraryMethod("/", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.FixnumOps.DivideOp),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.FixnumOps.DivideOp),
            });
            
            module.DefineLibraryMethod("-@", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32>(IronRuby.Builtins.FixnumOps.UnaryMinus),
            });
            
            module.DefineLibraryMethod("[]", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Int32>(IronRuby.Builtins.FixnumOps.Bit),
                new Func<System.Int32, Microsoft.Scripting.Math.BigInteger, System.Int32>(IronRuby.Builtins.FixnumOps.Bit),
            });
            
            module.DefineLibraryMethod("^", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.FixnumOps.BitwiseXor),
                new Func<System.Int32, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.FixnumOps.BitwiseXor),
                new Func<IronRuby.Runtime.RubyContext, System.Int32, System.Object, System.Object>(IronRuby.Builtins.FixnumOps.BitwiseXor),
            });
            
            module.DefineLibraryMethod("|", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.FixnumOps.BitwiseOr),
                new Func<System.Int32, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.FixnumOps.BitwiseOr),
                new Func<IronRuby.Runtime.RubyContext, System.Int32, System.Object, System.Object>(IronRuby.Builtins.FixnumOps.BitwiseOr),
            });
            
            module.DefineLibraryMethod("~", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32>(IronRuby.Builtins.FixnumOps.OnesComplement),
            });
            
            module.DefineLibraryMethod("+", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.FixnumOps.Add),
                new Func<System.Int32, System.Double, System.Double>(IronRuby.Builtins.FixnumOps.Add),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.FixnumOps.Add),
            });
            
            module.DefineLibraryMethod("<", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Boolean>(IronRuby.Builtins.FixnumOps.LessThan),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.FixnumOps.LessThan),
            });
            
            module.DefineLibraryMethod("<<", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.FixnumOps.LeftShift),
                new Func<IronRuby.Runtime.RubyContext, System.Int32, System.Object, System.Object>(IronRuby.Builtins.FixnumOps.LeftShift),
            });
            
            module.DefineLibraryMethod("<=", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Boolean>(IronRuby.Builtins.FixnumOps.LessThanOrEqual),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Int32, System.Object, System.Boolean>(IronRuby.Builtins.FixnumOps.LessThanOrEqual),
            });
            
            module.DefineLibraryMethod("<=>", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Int32>(IronRuby.Builtins.FixnumOps.Compare),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Int32, System.Object, System.Object>(IronRuby.Builtins.FixnumOps.Compare),
            });
            
            module.DefineLibraryMethod("==", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Boolean>(IronRuby.Builtins.FixnumOps.Equal),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Int32, System.Object, System.Boolean>(IronRuby.Builtins.FixnumOps.Equal),
            });
            
            module.DefineLibraryMethod(">", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Boolean>(IronRuby.Builtins.FixnumOps.GreaterThan),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Int32, System.Object, System.Boolean>(IronRuby.Builtins.FixnumOps.GreaterThan),
            });
            
            module.DefineLibraryMethod(">=", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Boolean>(IronRuby.Builtins.FixnumOps.GreaterThanOrEqual),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Int32, System.Object, System.Boolean>(IronRuby.Builtins.FixnumOps.GreaterThanOrEqual),
            });
            
            module.DefineLibraryMethod(">>", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.FixnumOps.RightShift),
                new Func<IronRuby.Runtime.RubyContext, System.Int32, System.Object, System.Object>(IronRuby.Builtins.FixnumOps.RightShift),
            });
            
            module.DefineLibraryMethod("abs", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32>(IronRuby.Builtins.FixnumOps.Abs),
            });
            
            module.DefineLibraryMethod("div", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.FixnumOps.DivideOp),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.FixnumOps.Div),
            });
            
            module.DefineLibraryMethod("divmod", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.FixnumOps.DivMod),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Int32, System.Object, System.Object>(IronRuby.Builtins.FixnumOps.DivMod),
            });
            
            module.DefineLibraryMethod("id2name", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Object>(IronRuby.Builtins.FixnumOps.Id2Name),
            });
            
            module.DefineLibraryMethod("modulo", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.FixnumOps.ModuloOp),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.FixnumOps.Modulo),
            });
            
            module.DefineLibraryMethod("quo", 0x51, new System.Delegate[] {
                new Func<System.Int32, Microsoft.Scripting.Math.BigInteger, System.Double>(IronRuby.Builtins.FixnumOps.Quotient),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Int32, System.Object, System.Object>(IronRuby.Builtins.FixnumOps.Quotient),
            });
            
            module.DefineLibraryMethod("size", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Int32>(IronRuby.Builtins.FixnumOps.Size),
            });
            
            module.DefineLibraryMethod("to_f", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Double>(IronRuby.Builtins.FixnumOps.ToFloat),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Object>(IronRuby.Builtins.FixnumOps.ToString),
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.FixnumOps.ToString),
            });
            
            module.DefineLibraryMethod("to_sym", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Object>(IronRuby.Builtins.FixnumOps.ToSymbol),
            });
            
            module.DefineLibraryMethod("zero?", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Boolean>(IronRuby.Builtins.FixnumOps.IsZero),
            });
            
        }
        
        private void LoadFixnum_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("induced_from", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Int32>(IronRuby.Builtins.FixnumOps.InducedFrom),
                new Func<IronRuby.Builtins.RubyClass, System.Double, System.Int32>(IronRuby.Builtins.FixnumOps.InducedFrom),
            });
            
        }
        
        private void LoadFloat_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("DIG", IronRuby.Builtins.FloatOps.DIG);
            module.SetConstant("EPSILON", IronRuby.Builtins.FloatOps.EPSILON);
            module.SetConstant("MANT_DIG", IronRuby.Builtins.FloatOps.MANT_DIG);
            module.SetConstant("MAX", IronRuby.Builtins.FloatOps.MAX);
            module.SetConstant("MAX_10_EXP", IronRuby.Builtins.FloatOps.MAX_10_EXP);
            module.SetConstant("MAX_EXP", IronRuby.Builtins.FloatOps.MAX_EXP);
            module.SetConstant("MIN", IronRuby.Builtins.FloatOps.MIN);
            module.SetConstant("MIN_10_EXP", IronRuby.Builtins.FloatOps.MIN_10_EXP);
            module.SetConstant("MIN_EXP", IronRuby.Builtins.FloatOps.MIN_EXP);
            module.SetConstant("RADIX", IronRuby.Builtins.FloatOps.RADIX);
            module.SetConstant("ROUNDS", IronRuby.Builtins.FloatOps.ROUNDS);
            
            module.DefineLibraryMethod("-", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Int32, System.Double>(IronRuby.Builtins.FloatOps.Subtract),
                new Func<System.Double, Microsoft.Scripting.Math.BigInteger, System.Double>(IronRuby.Builtins.FloatOps.Subtract),
                new Func<System.Double, System.Double, System.Double>(IronRuby.Builtins.FloatOps.Subtract),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Double, System.Object, System.Object>(IronRuby.Builtins.FloatOps.Subtract),
            });
            
            module.DefineLibraryMethod("%", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Int32, System.Double>(IronRuby.Builtins.FloatOps.Modulo),
                new Func<System.Double, Microsoft.Scripting.Math.BigInteger, System.Double>(IronRuby.Builtins.FloatOps.Modulo),
                new Func<System.Double, System.Double, System.Double>(IronRuby.Builtins.FloatOps.Modulo),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Double, System.Object, System.Object>(IronRuby.Builtins.FloatOps.ModuloOp),
            });
            
            module.DefineLibraryMethod("*", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Int32, System.Double>(IronRuby.Builtins.FloatOps.Multiply),
                new Func<System.Double, Microsoft.Scripting.Math.BigInteger, System.Double>(IronRuby.Builtins.FloatOps.Multiply),
                new Func<System.Double, System.Double, System.Double>(IronRuby.Builtins.FloatOps.Multiply),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Double, System.Object, System.Object>(IronRuby.Builtins.FloatOps.Multiply),
            });
            
            module.DefineLibraryMethod("**", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Int32, System.Double>(IronRuby.Builtins.FloatOps.Power),
                new Func<System.Double, Microsoft.Scripting.Math.BigInteger, System.Double>(IronRuby.Builtins.FloatOps.Power),
                new Func<System.Double, System.Double, System.Double>(IronRuby.Builtins.FloatOps.Power),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Double, System.Object, System.Object>(IronRuby.Builtins.FloatOps.Power),
            });
            
            module.DefineLibraryMethod("/", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Int32, System.Double>(IronRuby.Builtins.FloatOps.Divide),
                new Func<System.Double, Microsoft.Scripting.Math.BigInteger, System.Double>(IronRuby.Builtins.FloatOps.Divide),
                new Func<System.Double, System.Double, System.Double>(IronRuby.Builtins.FloatOps.Divide),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Double, System.Object, System.Object>(IronRuby.Builtins.FloatOps.Divide),
            });
            
            module.DefineLibraryMethod("+", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Int32, System.Double>(IronRuby.Builtins.FloatOps.Add),
                new Func<System.Double, Microsoft.Scripting.Math.BigInteger, System.Double>(IronRuby.Builtins.FloatOps.Add),
                new Func<System.Double, System.Double, System.Double>(IronRuby.Builtins.FloatOps.Add),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Double, System.Object, System.Object>(IronRuby.Builtins.FloatOps.Add),
            });
            
            module.DefineLibraryMethod("<", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Double, System.Boolean>(IronRuby.Builtins.FloatOps.LessThan),
                new Func<System.Double, System.Int32, System.Boolean>(IronRuby.Builtins.FloatOps.LessThan),
                new Func<System.Double, Microsoft.Scripting.Math.BigInteger, System.Boolean>(IronRuby.Builtins.FloatOps.LessThan),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Double, System.Object, System.Boolean>(IronRuby.Builtins.FloatOps.LessThan),
            });
            
            module.DefineLibraryMethod("<=", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Double, System.Boolean>(IronRuby.Builtins.FloatOps.LessThanOrEqual),
                new Func<System.Double, System.Int32, System.Boolean>(IronRuby.Builtins.FloatOps.LessThanOrEqual),
                new Func<System.Double, Microsoft.Scripting.Math.BigInteger, System.Boolean>(IronRuby.Builtins.FloatOps.LessThanOrEqual),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Double, System.Object, System.Boolean>(IronRuby.Builtins.FloatOps.LessThanOrEqual),
            });
            
            module.DefineLibraryMethod("<=>", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Double, System.Object>(IronRuby.Builtins.FloatOps.Compare),
                new Func<System.Double, System.Int32, System.Object>(IronRuby.Builtins.FloatOps.Compare),
                new Func<System.Double, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.FloatOps.Compare),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Double, System.Object, System.Object>(IronRuby.Builtins.FloatOps.Compare),
            });
            
            module.DefineLibraryMethod("==", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Double, System.Boolean>(IronRuby.Builtins.FloatOps.Equal),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Double, System.Object, System.Boolean>(IronRuby.Builtins.FloatOps.Equal),
            });
            
            module.DefineLibraryMethod(">", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Double, System.Boolean>(IronRuby.Builtins.FloatOps.GreaterThan),
                new Func<System.Double, System.Int32, System.Boolean>(IronRuby.Builtins.FloatOps.GreaterThan),
                new Func<System.Double, Microsoft.Scripting.Math.BigInteger, System.Boolean>(IronRuby.Builtins.FloatOps.GreaterThan),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Double, System.Object, System.Boolean>(IronRuby.Builtins.FloatOps.GreaterThan),
            });
            
            module.DefineLibraryMethod(">=", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Double, System.Boolean>(IronRuby.Builtins.FloatOps.GreaterThanOrEqual),
                new Func<System.Double, System.Int32, System.Boolean>(IronRuby.Builtins.FloatOps.GreaterThanOrEqual),
                new Func<System.Double, Microsoft.Scripting.Math.BigInteger, System.Boolean>(IronRuby.Builtins.FloatOps.GreaterThanOrEqual),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Double, System.Object, System.Boolean>(IronRuby.Builtins.FloatOps.GreaterThanOrEqual),
            });
            
            module.DefineLibraryMethod("abs", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Double>(IronRuby.Builtins.FloatOps.Abs),
            });
            
            module.DefineLibraryMethod("ceil", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Object>(IronRuby.Builtins.FloatOps.Ceil),
            });
            
            module.DefineLibraryMethod("coerce", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Double, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.FloatOps.Coerce),
            });
            
            module.DefineLibraryMethod("divmod", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.FloatOps.DivMod),
                new Func<System.Double, Microsoft.Scripting.Math.BigInteger, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.FloatOps.DivMod),
                new Func<System.Double, System.Double, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.FloatOps.DivMod),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Double, System.Object, System.Object>(IronRuby.Builtins.FloatOps.DivMod),
            });
            
            module.DefineLibraryMethod("finite?", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Boolean>(IronRuby.Builtins.FloatOps.IsFinite),
            });
            
            module.DefineLibraryMethod("floor", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Object>(IronRuby.Builtins.FloatOps.Floor),
            });
            
            module.DefineLibraryMethod("hash", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Int32>(IronRuby.Builtins.FloatOps.Hash),
            });
            
            module.DefineLibraryMethod("infinite?", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Object>(IronRuby.Builtins.FloatOps.IsInfinite),
            });
            
            module.DefineLibraryMethod("modulo", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Int32, System.Double>(IronRuby.Builtins.FloatOps.Modulo),
                new Func<System.Double, Microsoft.Scripting.Math.BigInteger, System.Double>(IronRuby.Builtins.FloatOps.Modulo),
                new Func<System.Double, System.Double, System.Double>(IronRuby.Builtins.FloatOps.Modulo),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Double, System.Object, System.Object>(IronRuby.Builtins.FloatOps.Modulo),
            });
            
            module.DefineLibraryMethod("nan?", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Boolean>(IronRuby.Builtins.FloatOps.IsNan),
            });
            
            module.DefineLibraryMethod("round", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Object>(IronRuby.Builtins.FloatOps.Round),
            });
            
            module.DefineLibraryMethod("to_f", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Double>(IronRuby.Builtins.FloatOps.ToFloat),
            });
            
            module.DefineLibraryMethod("to_i", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Object>(IronRuby.Builtins.FloatOps.ToInt),
            });
            
            module.DefineLibraryMethod("to_int", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Object>(IronRuby.Builtins.FloatOps.ToInt),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Double, IronRuby.Builtins.MutableString>(IronRuby.Builtins.FloatOps.ToS),
            });
            
            module.DefineLibraryMethod("truncate", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Object>(IronRuby.Builtins.FloatOps.ToInt),
            });
            
            module.DefineLibraryMethod("zero?", 0x51, new System.Delegate[] {
                new Func<System.Double, System.Boolean>(IronRuby.Builtins.FloatOps.IsZero),
            });
            
        }
        
        private void LoadFloat_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("induced_from", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.Double, System.Double>(IronRuby.Builtins.FloatOps.InducedFrom),
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Builtins.RubyClass, System.Int32, System.Object>(IronRuby.Builtins.FloatOps.InducedFrom),
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.FloatOps.InducedFrom),
                new Func<IronRuby.Builtins.RubyClass, System.Object, System.Double>(IronRuby.Builtins.FloatOps.InducedFrom),
            });
            
        }
        
        private void LoadGC_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("garbage_collect", 0x51, new System.Delegate[] {
                new Action<System.Object>(IronRuby.Builtins.RubyGC.GarbageCollect),
            });
            
        }
        
        private void LoadGC_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("disable", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Boolean>(IronRuby.Builtins.RubyGC.Disable),
            });
            
            module.DefineLibraryMethod("enable", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Boolean>(IronRuby.Builtins.RubyGC.Enable),
            });
            
            module.DefineLibraryMethod("start", 0x61, new System.Delegate[] {
                new Action<System.Object>(IronRuby.Builtins.RubyGC.GarbageCollect),
            });
            
        }
        
        private void LoadHash_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            LoadSystem__Collections__Generic__IDictionary_Instance(module);
            module.DefineLibraryMethod("[]", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.Hash, System.Object, System.Object>>, IronRuby.Runtime.RubyContext, IronRuby.Builtins.Hash, System.Object, System.Object>(IronRuby.Builtins.HashOps.GetElement),
            });
            
            module.DefineLibraryMethod("default", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.Hash, System.Object>(IronRuby.Builtins.HashOps.GetDefaultValue),
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.Proc, IronRuby.Builtins.Hash, System.Object, System.Object>>, IronRuby.Runtime.RubyContext, IronRuby.Builtins.Hash, System.Object, System.Object>(IronRuby.Builtins.HashOps.GetDefaultValue),
            });
            
            module.DefineLibraryMethod("default_proc", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.Hash, IronRuby.Builtins.Proc>(IronRuby.Builtins.HashOps.GetDefaultProc),
            });
            
            module.DefineLibraryMethod("default=", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.Hash, System.Object, System.Object>(IronRuby.Builtins.HashOps.SetDefaultValue),
            });
            
            module.DefineLibraryMethod("initialize", 0x52, new System.Delegate[] {
                new Func<IronRuby.Builtins.Hash, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.Initialize),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.Hash, System.Object, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.Initialize),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.Hash, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.Initialize),
            });
            
            module.DefineLibraryMethod("initialize_copy", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.Hash, IronRuby.Builtins.Hash, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.InitializeCopy),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.Hash, IronRuby.Builtins.MutableString>(IronRuby.Builtins.HashOps.Inspect),
            });
            
            module.DefineLibraryMethod("replace", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.Hash, System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.Replace),
            });
            
            module.DefineLibraryMethod("shift", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.Hash, System.Object, System.Object>>, IronRuby.Runtime.RubyContext, IronRuby.Builtins.Hash, System.Object>(IronRuby.Builtins.HashOps.Shift),
            });
            
        }
        
        private void LoadHash_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("[]", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.CreateSubclass),
                new Func<IronRuby.Builtins.RubyClass, System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.CreateSubclass),
                new Func<IronRuby.Builtins.RubyClass, System.Object[], IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.CreateSubclass),
            });
            
        }
        
        private void LoadInteger_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("ceil", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Object>(IronRuby.Builtins.Integer.ToInteger),
            });
            
            module.DefineLibraryMethod("chr", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.Integer.ToChr),
            });
            
            module.DefineLibraryMethod("downto", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, System.Int32, System.Int32, System.Object>(IronRuby.Builtins.Integer.DownTo),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.Integer.DownTo),
            });
            
            module.DefineLibraryMethod("floor", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Object>(IronRuby.Builtins.Integer.ToInteger),
            });
            
            module.DefineLibraryMethod("integer?", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Boolean>(IronRuby.Builtins.Integer.IsInteger),
            });
            
            module.DefineLibraryMethod("next", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Object>(IronRuby.Builtins.Integer.Next),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.Integer.Next),
            });
            
            module.DefineLibraryMethod("round", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Object>(IronRuby.Builtins.Integer.ToInteger),
            });
            
            module.DefineLibraryMethod("succ", 0x51, new System.Delegate[] {
                new Func<System.Int32, System.Object>(IronRuby.Builtins.Integer.Next),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.Integer.Next),
            });
            
            module.DefineLibraryMethod("times", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, System.Int32, System.Object>(IronRuby.Builtins.Integer.Times),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Integer.Times),
            });
            
            module.DefineLibraryMethod("to_i", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Object>(IronRuby.Builtins.Integer.ToInteger),
            });
            
            module.DefineLibraryMethod("to_int", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Object>(IronRuby.Builtins.Integer.ToInteger),
            });
            
            module.DefineLibraryMethod("truncate", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Object>(IronRuby.Builtins.Integer.ToInteger),
            });
            
            module.DefineLibraryMethod("upto", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, System.Int32, System.Int32, System.Object>(IronRuby.Builtins.Integer.UpTo),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.Integer.UpTo),
            });
            
        }
        
        private void LoadInteger_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("induced_from", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Object>(IronRuby.Builtins.Integer.InducedFrom),
                new Func<IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.Integer.InducedFrom),
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Builtins.RubyClass, System.Double, System.Object>(IronRuby.Builtins.Integer.InducedFrom),
                new Func<IronRuby.Builtins.RubyClass, System.Object, System.Int32>(IronRuby.Builtins.Integer.InducedFrom),
            });
            
        }
        
        private void LoadIO_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("SEEK_CUR", IronRuby.Builtins.RubyIOOps.SEEK_CUR);
            module.SetConstant("SEEK_END", IronRuby.Builtins.RubyIOOps.SEEK_END);
            module.SetConstant("SEEK_SET", IronRuby.Builtins.RubyIOOps.SEEK_SET);
            
            module.DefineLibraryMethod("<<", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyIO, System.Object, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Output),
            });
            
            module.DefineLibraryMethod("binmode", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Binmode),
            });
            
            module.DefineLibraryMethod("close", 0x51, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Close),
            });
            
            module.DefineLibraryMethod("close_read", 0x51, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.CloseReader),
            });
            
            module.DefineLibraryMethod("close_write", 0x51, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.CloseWriter),
            });
            
            module.DefineLibraryMethod("closed?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, System.Boolean>(IronRuby.Builtins.RubyIOOps.Closed),
            });
            
            module.DefineLibraryMethod("each", 0x51, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Each),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyIOOps.Each),
            });
            
            module.DefineLibraryMethod("each_byte", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyIO, System.Object>(IronRuby.Builtins.RubyIOOps.EachByte),
            });
            
            module.DefineLibraryMethod("each_line", 0x51, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Each),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyIOOps.Each),
            });
            
            module.DefineLibraryMethod("eof", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, System.Boolean>(IronRuby.Builtins.RubyIOOps.Eof),
            });
            
            module.DefineLibraryMethod("eof?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, System.Boolean>(IronRuby.Builtins.RubyIOOps.Eof),
            });
            
            module.DefineLibraryMethod("external_encoding", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Builtins.RubyEncoding>(IronRuby.Builtins.RubyIOOps.GetExternalEncoding),
            });
            
            module.DefineLibraryMethod("fcntl", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, System.Int32, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyIOOps.FileControl),
                new Func<IronRuby.Builtins.RubyIO, System.Int32, System.Int32, System.Int32>(IronRuby.Builtins.RubyIOOps.FileControl),
            });
            
            module.DefineLibraryMethod("fileno", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, System.Int32>(IronRuby.Builtins.RubyIOOps.FileNo),
            });
            
            module.DefineLibraryMethod("flush", 0x51, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Flush),
            });
            
            module.DefineLibraryMethod("fsync", 0x51, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Flush),
            });
            
            module.DefineLibraryMethod("getc", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, System.Object>(IronRuby.Builtins.RubyIOOps.Getc),
            });
            
            module.DefineLibraryMethod("gets", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.Gets),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.Gets),
            });
            
            module.DefineLibraryMethod("internal_encoding", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Builtins.RubyEncoding>(IronRuby.Builtins.RubyIOOps.GetInternalEncoding),
            });
            
            module.DefineLibraryMethod("isatty", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, System.Boolean>(IronRuby.Builtins.RubyIOOps.IsAtty),
            });
            
            module.DefineLibraryMethod("lineno", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyIO, System.Int32>(IronRuby.Builtins.RubyIOOps.GetLineNo),
            });
            
            module.DefineLibraryMethod("lineno=", 0x51, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyIO, System.Int32>(IronRuby.Builtins.RubyIOOps.SetLineNo),
            });
            
            module.DefineLibraryMethod("pid", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, System.Object>(IronRuby.Builtins.RubyIOOps.Pid),
            });
            
            module.DefineLibraryMethod("pos", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, System.Object>(IronRuby.Builtins.RubyIOOps.Pos),
            });
            
            module.DefineLibraryMethod("pos=", 0x51, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyIO, System.Int32>(IronRuby.Builtins.RubyIOOps.Pos),
            });
            
            module.DefineLibraryMethod("print", 0x51, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyScope, System.Object>(IronRuby.Builtins.RubyIOOps.Print),
                new Action<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.RubyIOOps.Print),
                new Action<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object[]>(IronRuby.Builtins.RubyIOOps.Print),
            });
            
            module.DefineLibraryMethod("printf", 0x51, new System.Delegate[] {
                new Action<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString, System.Object[]>(IronRuby.Builtins.RubyIOOps.PrintFormatted),
            });
            
            module.DefineLibraryMethod("putc", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.Putc),
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32, System.Int32>(IronRuby.Builtins.RubyIOOps.Putc),
            });
            
            module.DefineLibraryMethod("puts", 0x51, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, System.Object>(IronRuby.Builtins.RubyIOOps.PutsEmptyLine),
                new Action<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.Puts),
                new Action<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.RubyIOOps.Puts),
                new Action<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object[]>(IronRuby.Builtins.RubyIOOps.Puts),
            });
            
            module.DefineLibraryMethod("read", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.Read),
                new Func<IronRuby.Builtins.RubyIO, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.Read),
            });
            
            module.DefineLibraryMethod("readchar", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, System.Int32>(IronRuby.Builtins.RubyIOOps.ReadChar),
            });
            
            module.DefineLibraryMethod("readline", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.ReadLine),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.ReadLine),
            });
            
            module.DefineLibraryMethod("readlines", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyIO, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyIOOps.ReadLines),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyIOOps.ReadLines),
            });
            
            module.DefineLibraryMethod("rewind", 0x51, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Rewind),
            });
            
            module.DefineLibraryMethod("seek", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, System.Int32, System.Int32, System.Int32>(IronRuby.Builtins.RubyIOOps.Seek),
                new Func<IronRuby.Builtins.RubyIO, Microsoft.Scripting.Math.BigInteger, System.Int32, System.Int32>(IronRuby.Builtins.RubyIOOps.Seek),
            });
            
            module.DefineLibraryMethod("sync", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, System.Boolean>(IronRuby.Builtins.RubyIOOps.Sync),
            });
            
            module.DefineLibraryMethod("sync=", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, System.Boolean, System.Boolean>(IronRuby.Builtins.RubyIOOps.Sync),
            });
            
            module.DefineLibraryMethod("sysread", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.SystemRead),
            });
            
            module.DefineLibraryMethod("tell", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, System.Object>(IronRuby.Builtins.RubyIOOps.Pos),
            });
            
            module.DefineLibraryMethod("to_i", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, System.Int32>(IronRuby.Builtins.RubyIOOps.FileNo),
            });
            
            module.DefineLibraryMethod("to_io", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.ToIO),
            });
            
            module.DefineLibraryMethod("tty?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, System.Boolean>(IronRuby.Builtins.RubyIOOps.IsAtty),
            });
            
            module.DefineLibraryMethod("write", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyIOOps.Write),
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyIO, System.Object, System.Int32>(IronRuby.Builtins.RubyIOOps.Write),
            });
            
        }
        
        private void LoadIO_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("for_fd", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.ForFd),
            });
            
            module.DefineLibraryMethod("foreach", 0x61, new System.Delegate[] {
                new Action<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.ForEach),
                new Action<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.ForEach),
            });
            
            module.DefineLibraryMethod("open", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, System.Int32, System.Object>(IronRuby.Builtins.RubyIOOps.Open),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, System.Int32, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyIOOps.Open),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, System.Int32, System.Object, System.Object>(IronRuby.Builtins.RubyIOOps.Open),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.RubyIOOps.Open),
            });
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("popen", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyIOOps.OpenPipe),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.OpenPipe),
            });
            
            #endif
            module.DefineLibraryMethod("read", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.ReadFile),
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.Read),
            });
            
            module.DefineLibraryMethod("readlines", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyIOOps.ReadLines),
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyIOOps.ReadLines),
            });
            
            module.DefineLibraryMethod("select", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyIOOps.Select),
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyIOOps.Select),
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, System.Double, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyIOOps.Select),
            });
            
        }
        
        private void LoadKernel_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("__id__", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32>(IronRuby.Builtins.KernelOps.GetObjectId),
            });
            
            module.DefineLibraryMethod("__send__", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.String, System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, System.Object, System.String, System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.String, System.Object, System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, System.Object, System.String, System.Object, System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.String, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, System.Object, System.String, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.String, System.Object, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, System.Object, System.String, System.Object, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.String, System.Object[], System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, System.Object, System.String, System.Object[], System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
            });
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("`", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.ExecuteCommand),
            });
            
            #endif
            module.DefineLibraryMethod("=~", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.Match),
            });
            
            module.DefineLibraryMethod("==", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.ValueEquals),
            });
            
            module.DefineLibraryMethod("===", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.HashEquals),
            });
            
            module.DefineLibraryMethod("Array", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Collections.IList>(IronRuby.Builtins.KernelOps.ToArray),
            });
            
            module.DefineLibraryMethod("at_exit", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.Proc>(IronRuby.Builtins.KernelOps.AtExit),
            });
            
            module.DefineLibraryMethod("autoload", 0x52, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyScope, System.Object, System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.SetAutoloadedConstant),
            });
            
            module.DefineLibraryMethod("autoload?", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.GetAutoloadedConstantPath),
            });
            
            module.DefineLibraryMethod("binding", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.Binding>(IronRuby.Builtins.KernelOps.GetLocalScope),
            });
            
            module.DefineLibraryMethod("block_given?", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.HasBlock),
            });
            
            module.DefineLibraryMethod("caller", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetStackTrace),
            });
            
            module.DefineLibraryMethod("catch", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, System.Object, System.String, System.Object>(IronRuby.Builtins.KernelOps.Catch),
            });
            
            module.DefineLibraryMethod("class", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.KernelOps.GetClass),
            });
            
            module.DefineLibraryMethod("clone", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Clone),
            });
            
            module.DefineLibraryMethod("display", 0x51, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, System.Object>(IronRuby.Builtins.KernelOps.Display),
            });
            
            module.DefineLibraryMethod("dup", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Duplicate),
            });
            
            module.DefineLibraryMethod("eql?", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.ValueEquals),
            });
            
            module.DefineLibraryMethod("equal?", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.Equal),
            });
            
            module.DefineLibraryMethod("eval", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.Binding, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Evaluate),
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.Proc, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Evaluate),
            });
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("exec", 0x52, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Execute),
                new Action<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object[]>(IronRuby.Builtins.KernelOps.Execute),
            });
            
            #endif
            module.DefineLibraryMethod("exit", 0x52, new System.Delegate[] {
                new Action<System.Object>(IronRuby.Builtins.KernelOps.Exit),
                new Action<System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.Exit),
                new Action<System.Object, System.Int32>(IronRuby.Builtins.KernelOps.Exit),
            });
            
            module.DefineLibraryMethod("exit!", 0x52, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, System.Object>(IronRuby.Builtins.KernelOps.TerminateExecution),
                new Action<IronRuby.Runtime.RubyContext, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.TerminateExecution),
                new Action<IronRuby.Runtime.RubyContext, System.Object, System.Int32>(IronRuby.Builtins.KernelOps.TerminateExecution),
            });
            
            module.DefineLibraryMethod("extend", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, System.Object, System.Object>>, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule[], System.Object>(IronRuby.Builtins.KernelOps.Extend),
            });
            
            module.DefineLibraryMethod("fail", 0x52, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, System.Object>(IronRuby.Builtins.KernelOps.RaiseException),
                new Action<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.RaiseException),
                new Action<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.RaiseException),
            });
            
            module.DefineLibraryMethod("Float", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Double>(IronRuby.Builtins.KernelOps.ToFloat),
            });
            
            module.DefineLibraryMethod("format", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object[], IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Sprintf),
            });
            
            module.DefineLibraryMethod("freeze", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Freeze),
            });
            
            module.DefineLibraryMethod("frozen?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.KernelOps.Frozen),
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.Frozen),
            });
            
            module.DefineLibraryMethod("getc", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ReadInputCharacter),
            });
            
            module.DefineLibraryMethod("gets", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object>>, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ReadInputLine),
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object>>, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.KernelOps.ReadInputLine),
            });
            
            module.DefineLibraryMethod("global_variables", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetGlobalVariableNames),
            });
            
            module.DefineLibraryMethod("hash", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Int32>(IronRuby.Builtins.KernelOps.Hash),
            });
            
            module.DefineLibraryMethod("id", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32>(IronRuby.Builtins.KernelOps.GetId),
            });
            
            module.DefineLibraryMethod("initialize_copy", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.InitializeCopy),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Inspect),
            });
            
            module.DefineLibraryMethod("instance_eval", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Evaluate),
                new Func<IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.KernelOps.InstanceEval),
            });
            
            module.DefineLibraryMethod("instance_of?", 0x51, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.RubyModule, System.Boolean>(IronRuby.Builtins.KernelOps.IsOfClass),
            });
            
            module.DefineLibraryMethod("instance_variable_defined?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.String, System.Boolean>(IronRuby.Builtins.KernelOps.InstanceVariableDefined),
            });
            
            module.DefineLibraryMethod("instance_variable_get", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.String, System.Object>(IronRuby.Builtins.KernelOps.InstanceVariableGet),
            });
            
            module.DefineLibraryMethod("instance_variable_set", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.String, System.Object, System.Object>(IronRuby.Builtins.KernelOps.InstanceVariableSet),
            });
            
            module.DefineLibraryMethod("instance_variables", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.InstanceVariables),
            });
            
            module.DefineLibraryMethod("Integer", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ToInteger),
            });
            
            module.DefineLibraryMethod("is_a?", 0x51, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.RubyModule, System.Boolean>(IronRuby.Builtins.KernelOps.IsKindOf),
            });
            
            module.DefineLibraryMethod("iterator?", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.HasBlock),
            });
            
            module.DefineLibraryMethod("kind_of?", 0x51, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.RubyModule, System.Boolean>(IronRuby.Builtins.KernelOps.IsKindOf),
            });
            
            module.DefineLibraryMethod("lambda", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.Proc>(IronRuby.Builtins.KernelOps.CreateLambda),
            });
            
            module.DefineLibraryMethod("load", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, System.Boolean, System.Boolean>(IronRuby.Builtins.KernelOps.Load),
            });
            
            module.DefineLibraryMethod("load_assembly", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.KernelOps.LoadAssembly),
            });
            
            module.DefineLibraryMethod("local_variables", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetLocalVariableNames),
            });
            
            module.DefineLibraryMethod("loop", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Loop),
            });
            
            module.DefineLibraryMethod("method", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.String, IronRuby.Builtins.RubyMethod>(IronRuby.Builtins.KernelOps.GetMethod),
            });
            
            module.DefineLibraryMethod("method_missing", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, Microsoft.Scripting.SymbolId, System.Object[], System.Object>(IronRuby.Builtins.KernelOps.MethodMissing),
            });
            
            module.DefineLibraryMethod("methods", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetMethods),
            });
            
            module.DefineLibraryMethod("nil?", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.IsNil),
            });
            
            module.DefineLibraryMethod("object_id", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32>(IronRuby.Builtins.KernelOps.GetObjectId),
            });
            
            module.DefineLibraryMethod("open", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.KernelOps.Open),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.KernelOps.Open),
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.KernelOps.Open),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Open),
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.KernelOps.Open),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.KernelOps.Open),
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.KernelOps.Open),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Open),
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.KernelOps.Open),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Open),
            });
            
            module.DefineLibraryMethod("p", 0x52, new System.Delegate[] {
                new Action<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object[]>(IronRuby.Builtins.KernelOps.PrintInspect),
            });
            
            module.DefineLibraryMethod("print", 0x52, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyScope, System.Object>(IronRuby.Builtins.KernelOps.Print),
                new Action<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Print),
                new Action<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object[]>(IronRuby.Builtins.KernelOps.Print),
            });
            
            module.DefineLibraryMethod("printf", 0x52, new System.Delegate[] {
                new Action<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object[]>(IronRuby.Builtins.KernelOps.PrintFormatted),
                new Action<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object, System.Object[]>(IronRuby.Builtins.KernelOps.PrintFormatted),
            });
            
            module.DefineLibraryMethod("private_methods", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetPrivateMethods),
            });
            
            module.DefineLibraryMethod("proc", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.Proc>(IronRuby.Builtins.KernelOps.CreateLambda),
            });
            
            module.DefineLibraryMethod("protected_methods", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetProtectedMethods),
            });
            
            module.DefineLibraryMethod("public_methods", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetPublicMethods),
            });
            
            module.DefineLibraryMethod("putc", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Putc),
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32, System.Int32>(IronRuby.Builtins.KernelOps.Putc),
            });
            
            module.DefineLibraryMethod("puts", 0x52, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, System.Object>(IronRuby.Builtins.KernelOps.PutsEmptyLine),
                new Action<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.PutString),
                new Action<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.PutString),
                new Action<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object[]>(IronRuby.Builtins.KernelOps.PutString),
            });
            
            module.DefineLibraryMethod("raise", 0x52, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, System.Object>(IronRuby.Builtins.KernelOps.RaiseException),
                new Action<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.RaiseException),
                new Action<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.RaiseException),
            });
            
            module.DefineLibraryMethod("rand", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Double>(IronRuby.Builtins.KernelOps.Rand),
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Rand),
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Double, System.Object>(IronRuby.Builtins.KernelOps.Rand),
                new Func<IronRuby.Runtime.RubyContext, System.Object, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.KernelOps.Rand),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Rand),
            });
            
            module.DefineLibraryMethod("remove_instance_variable", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.String, System.Object>(IronRuby.Builtins.KernelOps.RemoveInstanceVariable),
            });
            
            module.DefineLibraryMethod("require", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.KernelOps.Require),
            });
            
            module.DefineLibraryMethod("respond_to?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.String, System.Boolean, System.Boolean>(IronRuby.Builtins.KernelOps.RespondTo),
            });
            
            module.DefineLibraryMethod("select", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.Select),
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.Select),
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, System.Double, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.Select),
            });
            
            module.DefineLibraryMethod("send", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.String, System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, System.Object, System.String, System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.String, System.Object, System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, System.Object, System.String, System.Object, System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.String, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, System.Object, System.String, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.String, System.Object, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, System.Object, System.String, System.Object, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.String, System.Object[], System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, System.Object, System.String, System.Object[], System.Object>(IronRuby.Builtins.KernelOps.SendMessage),
            });
            
            module.DefineLibraryMethod("set_trace_func", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, IronRuby.Builtins.Proc>(IronRuby.Builtins.KernelOps.SetTraceListener),
            });
            
            module.DefineLibraryMethod("singleton_method_added", 0x52, new System.Delegate[] {
                new Action<System.Object, System.Object>(IronRuby.Builtins.KernelOps.MethodAdded),
            });
            
            module.DefineLibraryMethod("singleton_method_removed", 0x52, new System.Delegate[] {
                new Action<System.Object, System.Object>(IronRuby.Builtins.KernelOps.MethodRemoved),
            });
            
            module.DefineLibraryMethod("singleton_method_undefined", 0x52, new System.Delegate[] {
                new Action<System.Object, System.Object>(IronRuby.Builtins.KernelOps.MethodUndefined),
            });
            
            module.DefineLibraryMethod("singleton_methods", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetSingletonMethods),
            });
            
            module.DefineLibraryMethod("sleep", 0x52, new System.Delegate[] {
                new Action<System.Object>(IronRuby.Builtins.KernelOps.Sleep),
                new Func<System.Object, System.Int32, System.Int32>(IronRuby.Builtins.KernelOps.Sleep),
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.KernelOps.Sleep),
            });
            
            module.DefineLibraryMethod("sprintf", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object[], IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Sprintf),
            });
            
            module.DefineLibraryMethod("String", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ToString),
            });
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("system", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.KernelOps.System),
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object[], System.Boolean>(IronRuby.Builtins.KernelOps.System),
            });
            
            #endif
            module.DefineLibraryMethod("taint", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Taint),
            });
            
            module.DefineLibraryMethod("tainted?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.Tainted),
            });
            
            module.DefineLibraryMethod("throw", 0x52, new System.Delegate[] {
                new Action<System.Object, System.String, System.Object>(IronRuby.Builtins.KernelOps.Throw),
            });
            
            module.DefineLibraryMethod("to_a", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.ToA),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.ToS),
            });
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("trap", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Trap),
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.KernelOps.Trap),
            });
            
            #endif
            module.DefineLibraryMethod("type", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.KernelOps.GetClassObsolete),
            });
            
            module.DefineLibraryMethod("untaint", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Untaint),
            });
            
            module.DefineLibraryMethod("warn", 0x52, new System.Delegate[] {
                new Action<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ReportWarning),
            });
            
        }
        
        private void LoadKernel_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            #if !SILVERLIGHT
            module.DefineLibraryMethod("`", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.ExecuteCommand),
            });
            
            #endif
            module.DefineLibraryMethod("Array", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Collections.IList>(IronRuby.Builtins.KernelOps.ToArray),
            });
            
            module.DefineLibraryMethod("at_exit", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.Proc>(IronRuby.Builtins.KernelOps.AtExit),
            });
            
            module.DefineLibraryMethod("autoload", 0x61, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyScope, System.Object, System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.SetAutoloadedConstant),
            });
            
            module.DefineLibraryMethod("autoload?", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.GetAutoloadedConstantPath),
            });
            
            module.DefineLibraryMethod("caller", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetStackTrace),
            });
            
            module.DefineLibraryMethod("catch", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, System.Object, System.String, System.Object>(IronRuby.Builtins.KernelOps.Catch),
            });
            
            module.DefineLibraryMethod("eval", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.Binding, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Evaluate),
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.Proc, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Evaluate),
            });
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("exec", 0x61, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Execute),
                new Action<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object[]>(IronRuby.Builtins.KernelOps.Execute),
            });
            
            #endif
            module.DefineLibraryMethod("exit", 0x61, new System.Delegate[] {
                new Action<System.Object>(IronRuby.Builtins.KernelOps.Exit),
                new Action<System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.Exit),
                new Action<System.Object, System.Int32>(IronRuby.Builtins.KernelOps.Exit),
            });
            
            module.DefineLibraryMethod("exit!", 0x61, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, System.Object>(IronRuby.Builtins.KernelOps.TerminateExecution),
                new Action<IronRuby.Runtime.RubyContext, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.TerminateExecution),
                new Action<IronRuby.Runtime.RubyContext, System.Object, System.Int32>(IronRuby.Builtins.KernelOps.TerminateExecution),
            });
            
            module.DefineLibraryMethod("fail", 0x61, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, System.Object>(IronRuby.Builtins.KernelOps.RaiseException),
                new Action<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.RaiseException),
                new Action<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.RaiseException),
            });
            
            module.DefineLibraryMethod("Float", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Double>(IronRuby.Builtins.KernelOps.ToFloat),
            });
            
            module.DefineLibraryMethod("format", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object[], IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Sprintf),
            });
            
            module.DefineLibraryMethod("getc", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ReadInputCharacter),
            });
            
            module.DefineLibraryMethod("gets", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object>>, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ReadInputLine),
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object>>, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.KernelOps.ReadInputLine),
            });
            
            module.DefineLibraryMethod("global_variables", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetGlobalVariableNames),
            });
            
            module.DefineLibraryMethod("Integer", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ToInteger),
            });
            
            module.DefineLibraryMethod("lambda", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.Proc>(IronRuby.Builtins.KernelOps.CreateLambda),
            });
            
            module.DefineLibraryMethod("load", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, System.Boolean, System.Boolean>(IronRuby.Builtins.KernelOps.Load),
            });
            
            module.DefineLibraryMethod("load_assembly", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.KernelOps.LoadAssembly),
            });
            
            module.DefineLibraryMethod("local_variables", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetLocalVariableNames),
            });
            
            module.DefineLibraryMethod("loop", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Loop),
            });
            
            module.DefineLibraryMethod("method_missing", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, Microsoft.Scripting.SymbolId, System.Object[], System.Object>(IronRuby.Builtins.KernelOps.MethodMissing),
            });
            
            module.DefineLibraryMethod("open", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.KernelOps.Open),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.KernelOps.Open),
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.KernelOps.Open),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Open),
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.KernelOps.Open),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.KernelOps.Open),
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.KernelOps.Open),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Open),
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.KernelOps.Open),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Open),
            });
            
            module.DefineLibraryMethod("p", 0x61, new System.Delegate[] {
                new Action<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object[]>(IronRuby.Builtins.KernelOps.PrintInspect),
            });
            
            module.DefineLibraryMethod("print", 0x61, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyScope, System.Object>(IronRuby.Builtins.KernelOps.Print),
                new Action<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Print),
                new Action<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object[]>(IronRuby.Builtins.KernelOps.Print),
            });
            
            module.DefineLibraryMethod("printf", 0x61, new System.Delegate[] {
                new Action<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object[]>(IronRuby.Builtins.KernelOps.PrintFormatted),
                new Action<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object, System.Object[]>(IronRuby.Builtins.KernelOps.PrintFormatted),
            });
            
            module.DefineLibraryMethod("proc", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.Proc>(IronRuby.Builtins.KernelOps.CreateLambda),
            });
            
            module.DefineLibraryMethod("putc", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Putc),
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32, System.Int32>(IronRuby.Builtins.KernelOps.Putc),
            });
            
            module.DefineLibraryMethod("puts", 0x61, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, System.Object>(IronRuby.Builtins.KernelOps.PutsEmptyLine),
                new Action<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.PutString),
                new Action<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.PutString),
                new Action<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object[]>(IronRuby.Builtins.KernelOps.PutString),
            });
            
            module.DefineLibraryMethod("raise", 0x61, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, System.Object>(IronRuby.Builtins.KernelOps.RaiseException),
                new Action<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.RaiseException),
                new Action<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.RaiseException),
            });
            
            module.DefineLibraryMethod("rand", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Double>(IronRuby.Builtins.KernelOps.Rand),
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Rand),
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Double, System.Object>(IronRuby.Builtins.KernelOps.Rand),
                new Func<IronRuby.Runtime.RubyContext, System.Object, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.KernelOps.Rand),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Rand),
            });
            
            module.DefineLibraryMethod("require", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.KernelOps.Require),
            });
            
            module.DefineLibraryMethod("select", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.Select),
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.Select),
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, System.Double, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.Select),
            });
            
            module.DefineLibraryMethod("set_trace_func", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, IronRuby.Builtins.Proc>(IronRuby.Builtins.KernelOps.SetTraceListener),
            });
            
            module.DefineLibraryMethod("sleep", 0x61, new System.Delegate[] {
                new Action<System.Object>(IronRuby.Builtins.KernelOps.Sleep),
                new Func<System.Object, System.Int32, System.Int32>(IronRuby.Builtins.KernelOps.Sleep),
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.KernelOps.Sleep),
            });
            
            module.DefineLibraryMethod("sprintf", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object[], IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Sprintf),
            });
            
            module.DefineLibraryMethod("String", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ToString),
            });
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("system", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.KernelOps.System),
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object[], System.Boolean>(IronRuby.Builtins.KernelOps.System),
            });
            
            #endif
            module.DefineLibraryMethod("throw", 0x61, new System.Delegate[] {
                new Action<System.Object, System.String, System.Object>(IronRuby.Builtins.KernelOps.Throw),
            });
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("trap", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Trap),
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.KernelOps.Trap),
            });
            
            #endif
            module.DefineLibraryMethod("warn", 0x61, new System.Delegate[] {
                new Action<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ReportWarning),
            });
            
        }
        
        private void LoadMarshal_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("MAJOR_VERSION", IronRuby.Builtins.RubyMarshal.MAJOR_VERSION);
            module.SetConstant("MINOR_VERSION", IronRuby.Builtins.RubyMarshal.MINOR_VERSION);
            
        }
        
        private void LoadMarshal_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("dump", 0x61, new System.Delegate[] {
                new Func<Microsoft.Scripting.Generation.SiteLocalStorage<IronRuby.Builtins.RubyMarshal.WriterSites>, IronRuby.Builtins.RubyModule, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyMarshal.Dump),
                new Func<Microsoft.Scripting.Generation.SiteLocalStorage<IronRuby.Builtins.RubyMarshal.WriterSites>, IronRuby.Builtins.RubyModule, System.Object, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyMarshal.Dump),
                new Func<Microsoft.Scripting.Generation.SiteLocalStorage<IronRuby.Builtins.RubyMarshal.WriterSites>, IronRuby.Builtins.RubyModule, System.Object, IronRuby.Builtins.RubyIO, System.Nullable<System.Int32>, System.Object>(IronRuby.Builtins.RubyMarshal.Dump),
                new Func<Microsoft.Scripting.Generation.SiteLocalStorage<IronRuby.Builtins.RubyMarshal.WriterSites>, IronRuby.Runtime.RespondToStorage, IronRuby.Builtins.RubyModule, System.Object, System.Object, System.Nullable<System.Int32>, System.Object>(IronRuby.Builtins.RubyMarshal.Dump),
            });
            
            module.DefineLibraryMethod("load", 0x61, new System.Delegate[] {
                new Func<Microsoft.Scripting.Generation.SiteLocalStorage<IronRuby.Builtins.RubyMarshal.ReaderSites>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.RubyMarshal.Load),
                new Func<Microsoft.Scripting.Generation.SiteLocalStorage<IronRuby.Builtins.RubyMarshal.ReaderSites>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyIO, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.RubyMarshal.Load),
                new Func<Microsoft.Scripting.Generation.SiteLocalStorage<IronRuby.Builtins.RubyMarshal.ReaderSites>, IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.Object, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.RubyMarshal.Load),
            });
            
            module.DefineLibraryMethod("restore", 0x61, new System.Delegate[] {
                new Func<Microsoft.Scripting.Generation.SiteLocalStorage<IronRuby.Builtins.RubyMarshal.ReaderSites>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.RubyMarshal.Load),
                new Func<Microsoft.Scripting.Generation.SiteLocalStorage<IronRuby.Builtins.RubyMarshal.ReaderSites>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyIO, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.RubyMarshal.Load),
                new Func<Microsoft.Scripting.Generation.SiteLocalStorage<IronRuby.Builtins.RubyMarshal.ReaderSites>, IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.Object, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.RubyMarshal.Load),
            });
            
        }
        
        private void LoadMatchData_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("[]", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MatchData, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MatchDataOps.GetGroup),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MatchData, System.Int32, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MatchDataOps.GetGroup),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.Builtins.MatchData, IronRuby.Builtins.Range, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MatchDataOps.GetGroup),
            });
            
            module.DefineLibraryMethod("begin", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MatchData, System.Int32, System.Object>(IronRuby.Builtins.MatchDataOps.Begin),
            });
            
            module.DefineLibraryMethod("captures", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MatchData, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MatchDataOps.Captures),
            });
            
            module.DefineLibraryMethod("end", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MatchData, System.Int32, System.Object>(IronRuby.Builtins.MatchDataOps.End),
            });
            
            module.DefineLibraryMethod("initialize_copy", 0x52, new System.Delegate[] {
                new Func<IronRuby.Builtins.MatchData, IronRuby.Builtins.MatchData, IronRuby.Builtins.MatchData>(IronRuby.Builtins.MatchDataOps.InitializeCopy),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MatchData, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MatchDataOps.Inspect),
            });
            
            module.DefineLibraryMethod("length", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MatchData, System.Int32>(IronRuby.Builtins.MatchDataOps.Length),
            });
            
            module.DefineLibraryMethod("offset", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MatchData, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MatchDataOps.Offset),
            });
            
            module.DefineLibraryMethod("post_match", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MatchData, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MatchDataOps.PostMatch),
            });
            
            module.DefineLibraryMethod("pre_match", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MatchData, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MatchDataOps.PreMatch),
            });
            
            module.DefineLibraryMethod("select", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MatchData, System.Object>(IronRuby.Builtins.MatchDataOps.Select),
            });
            
            module.DefineLibraryMethod("size", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MatchData, System.Int32>(IronRuby.Builtins.MatchDataOps.Length),
            });
            
            module.DefineLibraryMethod("string", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MatchData, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MatchDataOps.ReturnFrozenString),
            });
            
            module.DefineLibraryMethod("to_a", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MatchData, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MatchDataOps.ToArray),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MatchData, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MatchDataOps.ToS),
            });
            
            module.DefineLibraryMethod("values_at", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.Builtins.MatchData, System.Object[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MatchDataOps.ValuesAt),
            });
            
        }
        
        private void LoadMatchData_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.UndefineLibraryMethod("new");
        }
        
        private void LoadMath_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("E", IronRuby.Builtins.RubyMath.E);
            module.SetConstant("PI", IronRuby.Builtins.RubyMath.PI);
            
            module.DefineLibraryMethod("acos", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Acos),
            });
            
            module.DefineLibraryMethod("acosh", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Acosh),
            });
            
            module.DefineLibraryMethod("asin", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Asin),
            });
            
            module.DefineLibraryMethod("asinh", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Asinh),
            });
            
            module.DefineLibraryMethod("atan", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Atan),
            });
            
            module.DefineLibraryMethod("atan2", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Atan2),
            });
            
            module.DefineLibraryMethod("atanh", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Atanh),
            });
            
            module.DefineLibraryMethod("cos", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Cos),
            });
            
            module.DefineLibraryMethod("cosh", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Cosh),
            });
            
            module.DefineLibraryMethod("erf", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Erf),
            });
            
            module.DefineLibraryMethod("erfc", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Erfc),
            });
            
            module.DefineLibraryMethod("exp", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Exp),
            });
            
            module.DefineLibraryMethod("frexp", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyMath.Frexp),
            });
            
            module.DefineLibraryMethod("hypot", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Hypot),
            });
            
            module.DefineLibraryMethod("ldexp", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Ldexp),
            });
            
            module.DefineLibraryMethod("log", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Log),
            });
            
            module.DefineLibraryMethod("log10", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Log10),
            });
            
            module.DefineLibraryMethod("sin", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Sin),
            });
            
            module.DefineLibraryMethod("sinh", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Sinh),
            });
            
            module.DefineLibraryMethod("sqrt", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Sqrt),
            });
            
            module.DefineLibraryMethod("tan", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Tan),
            });
            
            module.DefineLibraryMethod("tanh", 0x52, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Tanh),
            });
            
        }
        
        private void LoadMath_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("acos", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Acos),
            });
            
            module.DefineLibraryMethod("acosh", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Acosh),
            });
            
            module.DefineLibraryMethod("asin", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Asin),
            });
            
            module.DefineLibraryMethod("asinh", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Asinh),
            });
            
            module.DefineLibraryMethod("atan", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Atan),
            });
            
            module.DefineLibraryMethod("atan2", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Atan2),
            });
            
            module.DefineLibraryMethod("atanh", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Atanh),
            });
            
            module.DefineLibraryMethod("cos", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Cos),
            });
            
            module.DefineLibraryMethod("cosh", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Cosh),
            });
            
            module.DefineLibraryMethod("erf", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Erf),
            });
            
            module.DefineLibraryMethod("erfc", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Erfc),
            });
            
            module.DefineLibraryMethod("exp", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Exp),
            });
            
            module.DefineLibraryMethod("frexp", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyMath.Frexp),
            });
            
            module.DefineLibraryMethod("hypot", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Hypot),
            });
            
            module.DefineLibraryMethod("ldexp", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Ldexp),
            });
            
            module.DefineLibraryMethod("log", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Log),
            });
            
            module.DefineLibraryMethod("log10", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Log10),
            });
            
            module.DefineLibraryMethod("sin", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Sin),
            });
            
            module.DefineLibraryMethod("sinh", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Sinh),
            });
            
            module.DefineLibraryMethod("sqrt", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Sqrt),
            });
            
            module.DefineLibraryMethod("tan", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Tan),
            });
            
            module.DefineLibraryMethod("tanh", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Tanh),
            });
            
        }
        
        private void LoadMethod_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineRuleGenerator("[]", 0x51, IronRuby.Builtins.MethodOps.Call());
            
            module.DefineLibraryMethod("==", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyMethod, IronRuby.Builtins.RubyMethod, System.Boolean>(IronRuby.Builtins.MethodOps.Equal),
                new Func<IronRuby.Builtins.RubyMethod, System.Object, System.Boolean>(IronRuby.Builtins.MethodOps.Equal),
            });
            
            module.DefineLibraryMethod("arity", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyMethod, System.Int32>(IronRuby.Builtins.MethodOps.GetArity),
            });
            
            module.DefineRuleGenerator("call", 0x51, IronRuby.Builtins.MethodOps.Call());
            
            module.DefineLibraryMethod("clone", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyMethod, IronRuby.Builtins.RubyMethod>(IronRuby.Builtins.MethodOps.Clone),
            });
            
            module.DefineLibraryMethod("to_proc", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyMethod, IronRuby.Builtins.Proc>(IronRuby.Builtins.MethodOps.ToProc),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyMethod, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MethodOps.ToS),
            });
            
            module.DefineLibraryMethod("unbind", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyMethod, IronRuby.Builtins.UnboundMethod>(IronRuby.Builtins.MethodOps.Unbind),
            });
            
        }
        
        private void LoadMicrosoft__Scripting__Actions__TypeGroup_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("[]", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Actions.TypeGroup, System.Object[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.TypeGroupOps.Of),
            });
            
            module.DefineLibraryMethod("each", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, Microsoft.Scripting.Actions.TypeGroup, System.Object>(IronRuby.Builtins.TypeGroupOps.EachType),
            });
            
            module.DefineLibraryMethod("name", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Actions.TypeGroup, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TypeGroupOps.GetName),
            });
            
            module.DefineRuleGenerator("new", 0x51, IronRuby.Builtins.TypeGroupOps.GetInstanceConstructor());
            
            module.DefineLibraryMethod("of", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Actions.TypeGroup, System.Object[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.TypeGroupOps.Of),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.Actions.TypeGroup, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TypeGroupOps.GetName),
            });
            
        }
        
        private void LoadMicrosoft__Scripting__Actions__TypeTracker_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("to_class", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Actions.TypeTracker, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.TypeTrackerOps.ToClass),
            });
            
            module.DefineLibraryMethod("to_module", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Actions.TypeTracker, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.TypeTrackerOps.ToModule),
            });
            
        }
        
        private void LoadModule_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("[]", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.Object[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.Of),
            });
            
            module.DefineLibraryMethod("<", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.IsSubclassOrIncluded),
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Object>(IronRuby.Builtins.ModuleOps.InvalidComparison),
            });
            
            module.DefineLibraryMethod("<=", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.IsSubclassSameOrIncluded),
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Object>(IronRuby.Builtins.ModuleOps.InvalidComparison),
            });
            
            module.DefineLibraryMethod("<=>", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.Comparison),
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Object>(IronRuby.Builtins.ModuleOps.Comparison),
            });
            
            module.DefineLibraryMethod("==", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Boolean>(IronRuby.Builtins.ModuleOps.Equals),
            });
            
            module.DefineLibraryMethod("===", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Boolean>(IronRuby.Builtins.ModuleOps.CaseEquals),
            });
            
            module.DefineLibraryMethod(">", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.IsNotSubclassOrIncluded),
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Object>(IronRuby.Builtins.ModuleOps.InvalidComparison),
            });
            
            module.DefineLibraryMethod(">=", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.IsNotSubclassSameOrIncluded),
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Object>(IronRuby.Builtins.ModuleOps.InvalidComparison),
            });
            
            module.DefineLibraryMethod("alias_method", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, System.String, System.String, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.AliasMethod),
            });
            
            module.DefineLibraryMethod("ancestors", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.Ancestors),
            });
            
            module.DefineLibraryMethod("append_features", 0x52, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.AppendFeatures),
            });
            
            module.DefineLibraryMethod("attr", 0x52, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String, System.Boolean>(IronRuby.Builtins.ModuleOps.Attr),
            });
            
            module.DefineLibraryMethod("attr_accessor", 0x52, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String>(IronRuby.Builtins.ModuleOps.AttrAccessor),
                new Action<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.Object[]>(IronRuby.Builtins.ModuleOps.AttrAccessor),
            });
            
            module.DefineLibraryMethod("attr_reader", 0x52, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String>(IronRuby.Builtins.ModuleOps.AttrReader),
                new Action<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.Object[]>(IronRuby.Builtins.ModuleOps.AttrReader),
            });
            
            module.DefineLibraryMethod("attr_writer", 0x52, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String>(IronRuby.Builtins.ModuleOps.AttrWriter),
                new Action<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.Object[]>(IronRuby.Builtins.ModuleOps.AttrWriter),
            });
            
            module.DefineLibraryMethod("autoload", 0x51, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyModule, System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ModuleOps.SetAutoloadedConstant),
            });
            
            module.DefineLibraryMethod("autoload?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ModuleOps.GetAutoloadedConstantPath),
            });
            
            module.DefineLibraryMethod("class_eval", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.ModuleOps.Evaluate),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.Evaluate),
            });
            
            module.DefineLibraryMethod("class_variable_defined?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Boolean>(IronRuby.Builtins.ModuleOps.ClassVariableDefined),
            });
            
            module.DefineLibraryMethod("class_variable_get", 0x52, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Object>(IronRuby.Builtins.ModuleOps.GetClassVariable),
            });
            
            module.DefineLibraryMethod("class_variable_set", 0x52, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Object, System.Object>(IronRuby.Builtins.ModuleOps.ClassVariableSet),
            });
            
            module.DefineLibraryMethod("class_variables", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.ClassVariables),
            });
            
            module.DefineLibraryMethod("const_defined?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Boolean>(IronRuby.Builtins.ModuleOps.IsConstantDefined),
            });
            
            module.DefineLibraryMethod("const_get", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String, System.Object>(IronRuby.Builtins.ModuleOps.GetConstantValue),
            });
            
            module.DefineLibraryMethod("const_missing", 0x51, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyModule, System.String>(IronRuby.Builtins.ModuleOps.ConstantMissing),
            });
            
            module.DefineLibraryMethod("const_set", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Object, System.Object>(IronRuby.Builtins.ModuleOps.SetConstantValue),
            });
            
            module.DefineLibraryMethod("constants", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetDefinedConstants),
            });
            
            module.DefineLibraryMethod("define_method", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, System.String, IronRuby.Builtins.RubyMethod, IronRuby.Builtins.RubyMethod>(IronRuby.Builtins.ModuleOps.DefineMethod),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, System.String, IronRuby.Builtins.UnboundMethod, IronRuby.Builtins.UnboundMethod>(IronRuby.Builtins.ModuleOps.DefineMethod),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyModule, System.String, IronRuby.Builtins.Proc>(IronRuby.Builtins.ModuleOps.DefineMethod),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String, IronRuby.Builtins.Proc, IronRuby.Builtins.Proc>(IronRuby.Builtins.ModuleOps.DefineMethod),
            });
            
            module.DefineLibraryMethod("extend_object", 0x52, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.ExtendObject),
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Object>(IronRuby.Builtins.ModuleOps.ExtendObject),
            });
            
            module.DefineLibraryMethod("extended", 0x52, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.ObjectExtended),
            });
            
            module.DefineLibraryMethod("freeze", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.Freeze),
            });
            
            module.DefineLibraryMethod("include", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, System.Object>>, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.Include),
            });
            
            module.DefineLibraryMethod("include?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, System.Boolean>(IronRuby.Builtins.ModuleOps.IncludesModule),
            });
            
            module.DefineLibraryMethod("included", 0x52, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.Included),
            });
            
            module.DefineLibraryMethod("included_modules", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetIncludedModules),
            });
            
            module.DefineLibraryMethod("initialize", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.Reinitialize),
            });
            
            module.DefineLibraryMethod("initialize_copy", 0x52, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.Object, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.InitializeCopy),
            });
            
            module.DefineLibraryMethod("instance_method", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.String, IronRuby.Builtins.UnboundMethod>(IronRuby.Builtins.ModuleOps.GetInstanceMethod),
            });
            
            module.DefineLibraryMethod("instance_methods", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetInstanceMethods),
                new Func<IronRuby.Builtins.RubyModule, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetInstanceMethods),
            });
            
            module.DefineLibraryMethod("method_added", 0x52, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.MethodAdded),
            });
            
            module.DefineLibraryMethod("method_defined?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Boolean>(IronRuby.Builtins.ModuleOps.MethodDefined),
            });
            
            module.DefineLibraryMethod("method_removed", 0x52, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.MethodRemoved),
            });
            
            module.DefineLibraryMethod("method_undefined", 0x52, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.MethodUndefined),
            });
            
            module.DefineLibraryMethod("module_eval", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.ModuleOps.Evaluate),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.Evaluate),
            });
            
            module.DefineLibraryMethod("module_function", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.Object[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.CopyMethodsToModuleSingleton),
            });
            
            module.DefineLibraryMethod("name", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ModuleOps.GetName),
            });
            
            module.DefineLibraryMethod("of", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.Object[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.Of),
            });
            
            module.DefineLibraryMethod("private", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.Object[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.SetPrivateVisibility),
            });
            
            module.DefineLibraryMethod("private_class_method", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, System.Object[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.MakeClassMethodsPrivate),
            });
            
            module.DefineLibraryMethod("private_instance_methods", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetPrivateInstanceMethods),
                new Func<IronRuby.Builtins.RubyModule, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetPrivateInstanceMethods),
            });
            
            module.DefineLibraryMethod("private_method_defined?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Boolean>(IronRuby.Builtins.ModuleOps.PrivateMethodDefined),
            });
            
            module.DefineLibraryMethod("protected", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.Object[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.SetProtectedVisibility),
            });
            
            module.DefineLibraryMethod("protected_instance_methods", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetProtectedInstanceMethods),
                new Func<IronRuby.Builtins.RubyModule, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetProtectedInstanceMethods),
            });
            
            module.DefineLibraryMethod("protected_method_defined?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Boolean>(IronRuby.Builtins.ModuleOps.ProtectedMethodDefined),
            });
            
            module.DefineLibraryMethod("public", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.Object[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.SetPublicVisibility),
            });
            
            module.DefineLibraryMethod("public_class_method", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, System.Object[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.MakeClassMethodsPublic),
            });
            
            module.DefineLibraryMethod("public_instance_methods", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetPublicInstanceMethods),
                new Func<IronRuby.Builtins.RubyModule, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetPublicInstanceMethods),
            });
            
            module.DefineLibraryMethod("public_method_defined?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Boolean>(IronRuby.Builtins.ModuleOps.PublicMethodDefined),
            });
            
            module.DefineLibraryMethod("remove_class_variable", 0x52, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Object>(IronRuby.Builtins.ModuleOps.RemoveClassVariable),
            });
            
            module.DefineLibraryMethod("remove_const", 0x52, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Object>(IronRuby.Builtins.ModuleOps.RemoveConstant),
            });
            
            module.DefineLibraryMethod("remove_method", 0x52, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.String, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.RemoveMethod),
            });
            
            module.DefineLibraryMethod("to_clr_type", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.Type>(IronRuby.Builtins.ModuleOps.ToClrType),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ModuleOps.ToS),
            });
            
            module.DefineLibraryMethod("undef_method", 0x52, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.String, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.UndefineMethod),
            });
            
        }
        
        private void LoadModule_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("constants", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetGlobalConstants),
            });
            
            module.DefineLibraryMethod("nesting", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetLexicalModuleNesting),
            });
            
        }
        
        private void LoadNilClass_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("&", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Object, System.Boolean>(IronRuby.Builtins.NilClassOps.And),
            });
            
            module.DefineLibraryMethod("^", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Object, System.Boolean>(IronRuby.Builtins.NilClassOps.Xor),
                new Func<System.Object, System.Boolean, System.Boolean>(IronRuby.Builtins.NilClassOps.Xor),
            });
            
            module.DefineLibraryMethod("|", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Object, System.Boolean>(IronRuby.Builtins.NilClassOps.Or),
                new Func<System.Object, System.Boolean, System.Boolean>(IronRuby.Builtins.NilClassOps.Or),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.NilClassOps.Inspect),
            });
            
            module.DefineLibraryMethod("nil?", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Boolean>(IronRuby.Builtins.NilClassOps.IsNil),
            });
            
            module.DefineLibraryMethod("to_a", 0x51, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.NilClassOps.ToArray),
            });
            
            module.DefineLibraryMethod("to_f", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Double>(IronRuby.Builtins.NilClassOps.ToDouble),
            });
            
            module.DefineLibraryMethod("to_i", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Int32>(IronRuby.Builtins.NilClassOps.ToInteger),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.NilClassOps.ToString),
            });
            
        }
        
        private void LoadNoMethodError_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.HideMethod("message");
        }
        
        private void LoadNumeric_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("-@", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.Numeric.UnaryMinus),
            });
            
            module.DefineLibraryMethod("+@", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Object>(IronRuby.Builtins.Numeric.UnaryPlus),
            });
            
            module.DefineLibraryMethod("<=>", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Object, System.Object>(IronRuby.Builtins.Numeric.Compare),
            });
            
            module.DefineLibraryMethod("abs", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.Numeric.Abs),
            });
            
            module.DefineLibraryMethod("ceil", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.Numeric.Ceil),
            });
            
            module.DefineLibraryMethod("coerce", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Numeric.Coerce),
            });
            
            module.DefineLibraryMethod("div", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.Numeric.Div),
            });
            
            module.DefineLibraryMethod("divmod", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Numeric.DivMod),
            });
            
            module.DefineLibraryMethod("eql?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.Numeric.Eql),
            });
            
            module.DefineLibraryMethod("floor", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.Numeric.Floor),
            });
            
            module.DefineLibraryMethod("integer?", 0x51, new System.Delegate[] {
                new Func<System.Object, System.Boolean>(IronRuby.Builtins.Numeric.IsInteger),
            });
            
            module.DefineLibraryMethod("modulo", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.Numeric.Modulo),
            });
            
            module.DefineLibraryMethod("nonzero?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.Numeric.IsNonZero),
            });
            
            module.DefineLibraryMethod("quo", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.Numeric.Quo),
            });
            
            module.DefineLibraryMethod("remainder", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.Numeric.Remainder),
            });
            
            module.DefineLibraryMethod("round", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.Numeric.Round),
            });
            
            module.DefineLibraryMethod("step", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Int32, System.Int32, System.Object>(IronRuby.Builtins.Numeric.Step),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Int32, System.Int32, System.Int32, System.Object>(IronRuby.Builtins.Numeric.Step),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Double, System.Double, System.Double, System.Object>(IronRuby.Builtins.Numeric.Step),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object, System.Object>(IronRuby.Builtins.Numeric.Step),
            });
            
            module.DefineLibraryMethod("to_int", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.Numeric.ToInt),
            });
            
            module.DefineLibraryMethod("truncate", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.Numeric.Truncate),
            });
            
            module.DefineLibraryMethod("zero?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Boolean>(IronRuby.Builtins.Numeric.IsZero),
            });
            
        }
        
        private void LoadObject_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("FALSE", IronRuby.Builtins.ObjectOps.FALSE);
            module.SetConstant("NIL", IronRuby.Builtins.ObjectOps.NIL);
            module.SetConstant("TRUE", IronRuby.Builtins.ObjectOps.TRUE);
            
            module.DefineLibraryMethod("initialize", 0x5a, new System.Delegate[] {
                new Action<System.Object>(IronRuby.Builtins.ObjectOps.Reinitialize),
            });
            
        }
        
        private void LoadObject_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
        }
        
        private void LoadObjectSpace_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("define_finalizer", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.Object, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.ObjectSpace.DefineFinalizer),
            });
            
            module.DefineLibraryMethod("each_object", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyClass, System.Object>(IronRuby.Builtins.ObjectSpace.EachObject),
            });
            
            module.DefineLibraryMethod("garbage_collect", 0x61, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ObjectSpace.GarbageCollect),
            });
            
            module.DefineLibraryMethod("undefine_finalizer", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Object>(IronRuby.Builtins.ObjectSpace.DefineFinalizer),
            });
            
        }
        
        private void LoadPrecision_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("prec", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object, System.Object>>, System.Object, IronRuby.Builtins.RubyClass, System.Object>(IronRuby.Builtins.Precision.Prec),
            });
            
            module.DefineLibraryMethod("prec_f", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.Precision.PrecFloat),
            });
            
            module.DefineLibraryMethod("prec_i", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.Precision.PrecInteger),
            });
            
        }
        
        private void LoadPrecision_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("included", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.Precision.Included),
            });
            
        }
        
        private void LoadProc_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("[]", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.ProcOps.Call),
                new Func<IronRuby.Builtins.Proc, System.Object, System.Object>(IronRuby.Builtins.ProcOps.Call),
                new Func<IronRuby.Builtins.Proc, System.Object, System.Object, System.Object>(IronRuby.Builtins.ProcOps.Call),
                new Func<IronRuby.Builtins.Proc, System.Object, System.Object, System.Object, System.Object>(IronRuby.Builtins.ProcOps.Call),
                new Func<IronRuby.Builtins.Proc, System.Object, System.Object, System.Object, System.Object, System.Object>(IronRuby.Builtins.ProcOps.Call),
                new Func<IronRuby.Builtins.Proc, System.Object[], System.Object>(IronRuby.Builtins.ProcOps.Call),
            });
            
            module.DefineLibraryMethod("==", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.Proc, IronRuby.Builtins.Proc, System.Boolean>(IronRuby.Builtins.ProcOps.Equal),
                new Func<IronRuby.Builtins.Proc, System.Object, System.Boolean>(IronRuby.Builtins.ProcOps.Equal),
            });
            
            module.DefineLibraryMethod("arity", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.Proc, System.Int32>(IronRuby.Builtins.ProcOps.GetArity),
            });
            
            module.DefineLibraryMethod("binding", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.Proc, IronRuby.Builtins.Binding>(IronRuby.Builtins.ProcOps.GetLocalScope),
            });
            
            module.DefineLibraryMethod("call", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.ProcOps.Call),
                new Func<IronRuby.Builtins.Proc, System.Object, System.Object>(IronRuby.Builtins.ProcOps.Call),
                new Func<IronRuby.Builtins.Proc, System.Object, System.Object, System.Object>(IronRuby.Builtins.ProcOps.Call),
                new Func<IronRuby.Builtins.Proc, System.Object, System.Object, System.Object, System.Object>(IronRuby.Builtins.ProcOps.Call),
                new Func<IronRuby.Builtins.Proc, System.Object, System.Object, System.Object, System.Object, System.Object>(IronRuby.Builtins.ProcOps.Call),
                new Func<IronRuby.Builtins.Proc, System.Object[], System.Object>(IronRuby.Builtins.ProcOps.Call),
            });
            
            module.DefineLibraryMethod("clone", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.Proc, IronRuby.Builtins.Proc>(IronRuby.Builtins.ProcOps.Clone),
            });
            
            module.DefineLibraryMethod("dup", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.Proc, IronRuby.Builtins.Proc>(IronRuby.Builtins.ProcOps.Clone),
            });
            
            module.DefineLibraryMethod("to_proc", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.Proc, IronRuby.Builtins.Proc>(IronRuby.Builtins.ProcOps.ToProc),
            });
            
        }
        
        private void LoadProc_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("new", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.Proc, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyClass, IronRuby.Builtins.Proc>(IronRuby.Builtins.ProcOps.CreateNew),
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.Proc, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.Proc>(IronRuby.Builtins.ProcOps.CreateNew),
            });
            
        }
        
        #if !SILVERLIGHT
        private void LoadProcess_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("kill", 0x52, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Object, System.Object>(IronRuby.Builtins.RubyProcess.Kill),
            });
            
        }
        #endif
        
        #if !SILVERLIGHT
        private void LoadProcess_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("euid", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.Int32>(IronRuby.Builtins.RubyProcess.EffectiveUserId),
            });
            
            module.DefineLibraryMethod("kill", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Object, System.Object>(IronRuby.Builtins.RubyProcess.Kill),
            });
            
            module.DefineLibraryMethod("pid", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.Int32>(IronRuby.Builtins.RubyProcess.GetPid),
            });
            
            module.DefineLibraryMethod("ppid", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.Int32>(IronRuby.Builtins.RubyProcess.GetParentPid),
            });
            
            module.DefineLibraryMethod("times", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyStruct>(IronRuby.Builtins.RubyProcess.GetTimes),
            });
            
            module.DefineLibraryMethod("uid", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.Int32>(IronRuby.Builtins.RubyProcess.UserId),
            });
            
        }
        #endif
        
        #if !SILVERLIGHT && !SILVERLIGHT
        private void LoadProcess__Status_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("coredump?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyProcess.Status, System.Boolean>(IronRuby.Builtins.RubyProcess.Status.CoreDump),
            });
            
            module.DefineLibraryMethod("exited?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyProcess.Status, System.Boolean>(IronRuby.Builtins.RubyProcess.Status.Exited),
            });
            
            module.DefineLibraryMethod("exitstatus", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyProcess.Status, System.Int32>(IronRuby.Builtins.RubyProcess.Status.ExitStatus),
            });
            
            module.DefineLibraryMethod("pid", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyProcess.Status, System.Int32>(IronRuby.Builtins.RubyProcess.Status.Pid),
            });
            
            module.DefineLibraryMethod("stopped?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyProcess.Status, System.Boolean>(IronRuby.Builtins.RubyProcess.Status.Stopped),
            });
            
            module.DefineLibraryMethod("stopsig", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyProcess.Status, System.Object>(IronRuby.Builtins.RubyProcess.Status.StopSig),
            });
            
            module.DefineLibraryMethod("success?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyProcess.Status, System.Boolean>(IronRuby.Builtins.RubyProcess.Status.Success),
            });
            
            module.DefineLibraryMethod("termsig", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyProcess.Status, System.Object>(IronRuby.Builtins.RubyProcess.Status.TermSig),
            });
            
        }
        #endif
        
        private void LoadRange_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("==", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.Range, System.Object, System.Boolean>(IronRuby.Builtins.RangeOps.Equals),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Builtins.Range, IronRuby.Builtins.Range, System.Boolean>(IronRuby.Builtins.RangeOps.Equals),
            });
            
            module.DefineLibraryMethod("===", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Builtins.Range, System.Object, System.Boolean>(IronRuby.Builtins.RangeOps.CaseEquals),
            });
            
            module.DefineLibraryMethod("begin", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.Range, System.Object>(IronRuby.Builtins.RangeOps.Begin),
            });
            
            module.DefineLibraryMethod("each", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.Range, System.Object>(IronRuby.Builtins.RangeOps.Each),
            });
            
            module.DefineLibraryMethod("end", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.Range, System.Object>(IronRuby.Builtins.RangeOps.End),
            });
            
            module.DefineLibraryMethod("eql?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.Range, System.Object, System.Boolean>(IronRuby.Builtins.RangeOps.Equals),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Builtins.Range, IronRuby.Builtins.Range, System.Boolean>(IronRuby.Builtins.RangeOps.Eql),
            });
            
            module.DefineLibraryMethod("exclude_end?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.Range, System.Boolean>(IronRuby.Builtins.RangeOps.ExcludeEnd),
            });
            
            module.DefineLibraryMethod("first", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.Range, System.Object>(IronRuby.Builtins.RangeOps.Begin),
            });
            
            module.DefineLibraryMethod("hash", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.Range, System.Int32>(IronRuby.Builtins.RangeOps.GetHashCode),
            });
            
            module.DefineLibraryMethod("include?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Builtins.Range, System.Object, System.Boolean>(IronRuby.Builtins.RangeOps.CaseEquals),
            });
            
            module.DefineLibraryMethod("initialize", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Builtins.Range, System.Object, System.Object, System.Boolean, IronRuby.Builtins.Range>(IronRuby.Builtins.RangeOps.Reinitialize),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.Range, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RangeOps.Inspect),
            });
            
            module.DefineLibraryMethod("last", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.Range, System.Object>(IronRuby.Builtins.RangeOps.End),
            });
            
            module.DefineLibraryMethod("member?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Builtins.Range, System.Object, System.Boolean>(IronRuby.Builtins.RangeOps.CaseEquals),
            });
            
            module.DefineLibraryMethod("step", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.Range, System.Object, System.Object>(IronRuby.Builtins.RangeOps.Step),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Builtins.Range, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RangeOps.ToS),
            });
            
        }
        
        private void LoadRangeError_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.HideMethod("message");
        }
        
        private void LoadRegexp_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("EXTENDED", IronRuby.Builtins.RegexpOps.EXTENDED);
            module.SetConstant("IGNORECASE", IronRuby.Builtins.RegexpOps.IGNORECASE);
            module.SetConstant("MULTILINE", IronRuby.Builtins.RegexpOps.MULTILINE);
            
            module.DefineLibraryMethod("~", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyRegex, System.Object>(IronRuby.Builtins.RegexpOps.ImplicitMatch),
            });
            
            module.DefineLibraryMethod("=~", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RegexpOps.MatchIndex),
            });
            
            module.DefineLibraryMethod("==", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyRegex, System.Object, System.Boolean>(IronRuby.Builtins.RegexpOps.Equals),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.RubyRegex, System.Boolean>(IronRuby.Builtins.RegexpOps.Equals),
            });
            
            module.DefineLibraryMethod("===", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RegexpOps.CaseCompare),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyRegex, System.Object, System.Boolean>(IronRuby.Builtins.RegexpOps.CaseCompare),
            });
            
            module.DefineLibraryMethod("casefold?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyRegex, System.Boolean>(IronRuby.Builtins.RegexpOps.IsCaseInsensitive),
            });
            
            module.DefineLibraryMethod("eql?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyRegex, System.Object, System.Boolean>(IronRuby.Builtins.RegexpOps.Equals),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.RubyRegex, System.Boolean>(IronRuby.Builtins.RegexpOps.Equals),
            });
            
            module.DefineLibraryMethod("hash", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyRegex, System.Int32>(IronRuby.Builtins.RegexpOps.GetHash),
            });
            
            module.DefineLibraryMethod("initialize", 0x52, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyRegex, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Reinitialize),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.RubyRegex, System.Int32, System.Object, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Reinitialize),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.RubyRegex, System.Object, System.Object, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Reinitialize),
                new Func<IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Reinitialize),
                new Func<IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Reinitialize),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RegexpOps.Inspect),
            });
            
            module.DefineLibraryMethod("kcode", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RegexpOps.GetEncoding),
            });
            
            module.DefineLibraryMethod("match", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, IronRuby.Builtins.MatchData>(IronRuby.Builtins.RegexpOps.Match),
            });
            
            module.DefineLibraryMethod("options", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyRegex, System.Int32>(IronRuby.Builtins.RegexpOps.GetOptions),
            });
            
            module.DefineLibraryMethod("source", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RegexpOps.Source),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RegexpOps.ToS),
            });
            
        }
        
        private void LoadRegexp_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineRuleGenerator("compile", 0x61, IronRuby.Builtins.RegexpOps.Compile());
            
            module.DefineLibraryMethod("escape", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RegexpOps.Escape),
            });
            
            module.DefineLibraryMethod("last_match", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MatchData>(IronRuby.Builtins.RegexpOps.LastMatch),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyClass, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RegexpOps.LastMatch),
            });
            
            module.DefineLibraryMethod("quote", 0x61, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RegexpOps.Escape),
            });
            
            module.DefineLibraryMethod("union", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Builtins.RubyClass, System.Object[], IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Union),
            });
            
        }
        
        #if !SILVERLIGHT
        private void LoadSignal_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("list", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, IronRuby.Builtins.Hash>(IronRuby.Builtins.Signal.List),
            });
            
            module.DefineLibraryMethod("trap", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.Signal.Trap),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.Signal.Trap),
            });
            
        }
        #endif
        
        private void LoadString_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.HideMethod("clone");
            module.HideMethod("version");
            module.DefineLibraryMethod("%", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Format),
            });
            
            module.DefineLibraryMethod("*", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Repeat),
            });
            
            module.DefineLibraryMethod("[]", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.GetChar),
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.Range, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring),
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring),
            });
            
            module.DefineLibraryMethod("[]=", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ReplaceCharacter),
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Int32, System.Int32>(IronRuby.Builtins.MutableStringOps.SetCharacter),
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ReplaceSubstring),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.Range, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ReplaceSubstring),
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ReplaceSubstring),
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ReplaceSubstring),
            });
            
            module.DefineLibraryMethod("+", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Concatenate),
            });
            
            module.DefineLibraryMethod("<<", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Append),
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Append),
            });
            
            module.DefineLibraryMethod("<=>", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.MutableStringOps.Compare),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, System.Object, System.Object>(IronRuby.Builtins.MutableStringOps.Compare),
            });
            
            module.DefineLibraryMethod("=~", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Object>(IronRuby.Builtins.MutableStringOps.Match),
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.Match),
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object>>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, System.Object, System.Object>(IronRuby.Builtins.MutableStringOps.Match),
            });
            
            module.DefineLibraryMethod("==", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.MutableStringOps.StringEquals),
                new Func<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, System.Object, System.Boolean>(IronRuby.Builtins.MutableStringOps.Equals),
            });
            
            module.DefineLibraryMethod("===", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.MutableStringOps.StringEquals),
                new Func<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, System.Object, System.Boolean>(IronRuby.Builtins.MutableStringOps.Equals),
            });
            
            module.DefineLibraryMethod("capitalize", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Capitalize),
            });
            
            module.DefineLibraryMethod("capitalize!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.CapitalizeInPlace),
            });
            
            module.DefineLibraryMethod("casecmp", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.MutableStringOps.Casecmp),
            });
            
            module.DefineLibraryMethod("center", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Center),
            });
            
            module.DefineLibraryMethod("chomp", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Chomp),
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Chomp),
            });
            
            module.DefineLibraryMethod("chomp!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ChompInPlace),
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ChompInPlace),
            });
            
            module.DefineLibraryMethod("chop", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Chop),
            });
            
            module.DefineLibraryMethod("chop!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ChopInPlace),
            });
            
            module.DefineLibraryMethod("concat", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Append),
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Append),
            });
            
            module.DefineLibraryMethod("count", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, System.Object[], System.Object>(IronRuby.Builtins.MutableStringOps.Count),
            });
            
            module.DefineLibraryMethod("delete", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, System.Object[], IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Delete),
            });
            
            module.DefineLibraryMethod("delete!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, System.Object[], IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.DeleteInPlace),
            });
            
            module.DefineLibraryMethod("downcase", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.DownCase),
            });
            
            module.DefineLibraryMethod("downcase!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.DownCaseInPlace),
            });
            
            module.DefineLibraryMethod("dump", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Dump),
            });
            
            module.DefineLibraryMethod("each", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.EachLine),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.EachLine),
            });
            
            module.DefineLibraryMethod("each_byte", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.EachByte),
            });
            
            module.DefineLibraryMethod("each_line", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.EachLine),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.EachLine),
            });
            
            module.DefineLibraryMethod("empty?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.MutableStringOps.Empty),
            });
            
            module.DefineLibraryMethod("encoding", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyEncoding>(IronRuby.Builtins.MutableStringOps.GetEncoding),
            });
            
            module.DefineLibraryMethod("gsub", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ReplaceAll),
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Object>(IronRuby.Builtins.MutableStringOps.BlockReplaceAll),
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.BlockReplaceAll),
            });
            
            module.DefineLibraryMethod("gsub!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Object>(IronRuby.Builtins.MutableStringOps.BlockReplaceAllInPlace),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ReplaceAllInPlace),
            });
            
            module.DefineLibraryMethod("hex", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.ToIntegerHex),
            });
            
            module.DefineLibraryMethod("include?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.MutableStringOps.Include),
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Boolean>(IronRuby.Builtins.MutableStringOps.Include),
            });
            
            module.DefineLibraryMethod("index", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.Index),
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.Index),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.Index),
            });
            
            module.DefineLibraryMethod("initialize", 0x52, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Reinitialize),
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Reinitialize),
            });
            
            module.DefineLibraryMethod("initialize_copy", 0x52, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Reinitialize),
            });
            
            module.DefineLibraryMethod("insert", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Insert),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Dump),
            });
            
            module.DefineLibraryMethod("intern", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, Microsoft.Scripting.SymbolId>(IronRuby.Builtins.MutableStringOps.ToSym),
            });
            
            module.DefineLibraryMethod("length", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.MutableStringOps.MutableStringLength),
            });
            
            module.DefineLibraryMethod("ljust", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.LeftJustify),
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.LeftJustify),
            });
            
            module.DefineLibraryMethod("lstrip", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.StripLeft),
            });
            
            module.DefineLibraryMethod("lstrip!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.StripLeftInPlace),
            });
            
            module.DefineLibraryMethod("match", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, System.Object>>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Object>(IronRuby.Builtins.MutableStringOps.MatchRegexp),
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, System.Object>>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.MatchObject),
            });
            
            module.DefineLibraryMethod("next", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Succ),
            });
            
            module.DefineLibraryMethod("next!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.SuccInPlace),
            });
            
            module.DefineLibraryMethod("oct", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.ToIntegerOctal),
            });
            
            module.DefineLibraryMethod("replace", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Replace),
            });
            
            module.DefineLibraryMethod("reverse", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetReversed),
            });
            
            module.DefineLibraryMethod("reverse!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Reverse),
            });
            
            module.DefineLibraryMethod("rindex", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.ReverseIndex),
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.ReverseIndex),
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.ReverseIndex),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Object>(IronRuby.Builtins.MutableStringOps.ReverseIndex),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.ReverseIndex),
            });
            
            module.DefineLibraryMethod("rjust", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.RightJustify),
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.RightJustify),
            });
            
            module.DefineLibraryMethod("rstrip", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.StripRight),
            });
            
            module.DefineLibraryMethod("rstrip!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.StripRightInPlace),
            });
            
            module.DefineLibraryMethod("scan", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MutableStringOps.Scan),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Object>(IronRuby.Builtins.MutableStringOps.Scan),
            });
            
            module.DefineLibraryMethod("size", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.MutableStringOps.MutableStringLength),
            });
            
            module.DefineLibraryMethod("slice", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.GetChar),
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.Range, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring),
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring),
            });
            
            module.DefineLibraryMethod("slice!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.RemoveCharInPlace),
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.RemoveSubstringInPlace),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.Range, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.RemoveSubstringInPlace),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.RemoveSubstringInPlace),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.RemoveSubstringInPlace),
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.RemoveSubstringInPlace),
            });
            
            module.DefineLibraryMethod("split", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MutableStringOps.Split),
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MutableStringOps.Split),
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MutableStringOps.Split),
            });
            
            module.DefineLibraryMethod("squeeze", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, System.Object[], IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Squeeze),
            });
            
            module.DefineLibraryMethod("squeeze!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, System.Object[], IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.SqueezeInPlace),
            });
            
            module.DefineLibraryMethod("strip", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Strip),
            });
            
            module.DefineLibraryMethod("strip!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.StripInPlace),
            });
            
            module.DefineLibraryMethod("sub", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Object>(IronRuby.Builtins.MutableStringOps.BlockReplaceFirst),
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.BlockReplaceFirst),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ReplaceFirst),
            });
            
            module.DefineLibraryMethod("sub!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Object>(IronRuby.Builtins.MutableStringOps.BlockReplaceFirstInPlace),
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ReplaceFirstInPlace),
            });
            
            module.DefineLibraryMethod("succ", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Succ),
            });
            
            module.DefineLibraryMethod("succ!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.SuccInPlace),
            });
            
            module.DefineLibraryMethod("sum", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.GetChecksum),
            });
            
            module.DefineLibraryMethod("swapcase", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.SwapCase),
            });
            
            module.DefineLibraryMethod("swapcase!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.SwapCaseInPlace),
            });
            
            module.DefineLibraryMethod("to_clr_string", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, System.String>(IronRuby.Builtins.MutableStringOps.ToClrString),
            });
            
            module.DefineLibraryMethod("to_f", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, System.Double>(IronRuby.Builtins.MutableStringOps.ToDouble),
            });
            
            module.DefineLibraryMethod("to_i", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.ToInteger),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.ToInteger),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ToS),
            });
            
            module.DefineLibraryMethod("to_str", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ToS),
            });
            
            module.DefineLibraryMethod("to_sym", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, Microsoft.Scripting.SymbolId>(IronRuby.Builtins.MutableStringOps.ToSym),
            });
            
            module.DefineLibraryMethod("tr", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Tr),
            });
            
            module.DefineLibraryMethod("tr!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.TrInPlace),
            });
            
            module.DefineLibraryMethod("tr_s", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.TrSqueeze),
            });
            
            module.DefineLibraryMethod("tr_s!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.TrSqueezeInPlace),
            });
            
            module.DefineLibraryMethod("unpack", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MutableStringOps.Unpack),
            });
            
            module.DefineLibraryMethod("upcase", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.UpCase),
            });
            
            module.DefineLibraryMethod("upcase!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.UpCaseInPlace),
            });
            
            module.DefineLibraryMethod("upto", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.UpTo),
            });
            
        }
        
        private void LoadStruct_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            #if !SILVERLIGHT
            module.SetConstant("Tms", IronRuby.Builtins.RubyStructOps.CreateTmsClass(module));
            #endif
            
            module.DefineLibraryMethod("[]", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyStruct, System.Int32, System.Object>(IronRuby.Builtins.RubyStructOps.GetValue),
                new Func<IronRuby.Builtins.RubyStruct, Microsoft.Scripting.SymbolId, System.Object>(IronRuby.Builtins.RubyStructOps.GetValue),
                new Func<IronRuby.Builtins.RubyStruct, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyStructOps.GetValue),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyStruct, System.Object, System.Object>(IronRuby.Builtins.RubyStructOps.GetValue),
            });
            
            module.DefineLibraryMethod("[]=", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyStruct, System.Int32, System.Object, System.Object>(IronRuby.Builtins.RubyStructOps.SetValue),
                new Func<IronRuby.Builtins.RubyStruct, Microsoft.Scripting.SymbolId, System.Object, System.Object>(IronRuby.Builtins.RubyStructOps.SetValue),
                new Func<IronRuby.Builtins.RubyStruct, IronRuby.Builtins.MutableString, System.Object, System.Object>(IronRuby.Builtins.RubyStructOps.SetValue),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyStruct, System.Object, System.Object, System.Object>(IronRuby.Builtins.RubyStructOps.SetValue),
            });
            
            module.DefineLibraryMethod("==", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Builtins.RubyStruct, System.Object, System.Boolean>(IronRuby.Builtins.RubyStructOps.Equals),
            });
            
            module.DefineLibraryMethod("each", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyStruct, System.Object>(IronRuby.Builtins.RubyStructOps.Each),
            });
            
            module.DefineLibraryMethod("each_pair", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyStruct, System.Object>(IronRuby.Builtins.RubyStructOps.EachPair),
            });
            
            module.DefineLibraryMethod("eql?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyStruct, System.Object, System.Boolean>(IronRuby.Builtins.RubyStructOps.Equal),
            });
            
            module.DefineLibraryMethod("hash", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyStruct, System.Int32>(IronRuby.Builtins.RubyStructOps.Hash),
            });
            
            module.DefineLibraryMethod("initialize", 0x52, new System.Delegate[] {
                new Action<IronRuby.Builtins.RubyStruct, System.Object[]>(IronRuby.Builtins.RubyStructOps.Reinitialize),
            });
            
            module.DefineLibraryMethod("initialize_copy", 0x52, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyStruct, IronRuby.Builtins.RubyStruct, IronRuby.Builtins.RubyStruct>(IronRuby.Builtins.RubyStructOps.InitializeCopy),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyStruct, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyStructOps.Inspect),
            });
            
            module.DefineLibraryMethod("length", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyStruct, System.Int32>(IronRuby.Builtins.RubyStructOps.GetSize),
            });
            
            module.DefineLibraryMethod("members", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyStruct, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyStructOps.GetMembers),
            });
            
            module.DefineLibraryMethod("select", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyStruct, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyStructOps.Select),
            });
            
            module.DefineLibraryMethod("size", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyStruct, System.Int32>(IronRuby.Builtins.RubyStructOps.GetSize),
            });
            
            module.DefineLibraryMethod("to_a", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyStruct, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyStructOps.Values),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyStruct, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyStructOps.Inspect),
            });
            
            module.DefineLibraryMethod("values", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyStruct, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyStructOps.Values),
            });
            
            module.DefineLibraryMethod("values_at", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyStruct, System.Object[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyStructOps.ValuesAt),
            });
            
        }
        
        private void LoadStruct_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("new", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, System.Int32, System.Object[], System.Object>(IronRuby.Builtins.RubyStructOps.NewAnonymousStruct),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, Microsoft.Scripting.SymbolId, System.Object[], System.Object>(IronRuby.Builtins.RubyStructOps.NewAnonymousStruct),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, System.String, System.Object[], System.Object>(IronRuby.Builtins.RubyStructOps.NewAnonymousStruct),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object[], System.Object>(IronRuby.Builtins.RubyStructOps.NewStruct),
            });
            
        }
        
        private void LoadSymbol_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("id2name", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.SymbolId, IronRuby.Builtins.MutableString>(IronRuby.Builtins.SymbolOps.ToString),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.SymbolId, IronRuby.Builtins.MutableString>(IronRuby.Builtins.SymbolOps.Inspect),
            });
            
            module.DefineLibraryMethod("to_clr_string", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.SymbolId, System.String>(IronRuby.Builtins.SymbolOps.ToClrString),
            });
            
            module.DefineLibraryMethod("to_i", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.SymbolId, System.Int32>(IronRuby.Builtins.SymbolOps.ToInteger),
            });
            
            module.DefineLibraryMethod("to_int", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.SymbolId, System.Int32>(IronRuby.Builtins.SymbolOps.ToInteger),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.SymbolId, IronRuby.Builtins.MutableString>(IronRuby.Builtins.SymbolOps.ToString),
            });
            
            module.DefineLibraryMethod("to_sym", 0x51, new System.Delegate[] {
                new Func<Microsoft.Scripting.SymbolId, Microsoft.Scripting.SymbolId>(IronRuby.Builtins.SymbolOps.ToSymbol),
            });
            
        }
        
        private void LoadSymbol_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("all_symbols", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Collections.Generic.List<System.Object>>(IronRuby.Builtins.SymbolOps.GetAllSymbols),
            });
            
        }
        
        private void LoadSystem__Collections__Generic__IDictionary_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("[]", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Object>(IronRuby.Builtins.IDictionaryOps.GetElement),
            });
            
            module.DefineLibraryMethod("[]=", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Object, System.Object>(IronRuby.Builtins.IDictionaryOps.SetElement),
            });
            
            module.DefineLibraryMethod("==", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Boolean>(IronRuby.Builtins.IDictionaryOps.Equals),
            });
            
            module.DefineLibraryMethod("clear", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Collections.Generic.IDictionary<System.Object, System.Object>>(IronRuby.Builtins.IDictionaryOps.Clear),
            });
            
            module.DefineLibraryMethod("default", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Object>(IronRuby.Builtins.IDictionaryOps.GetDefaultValue),
            });
            
            module.DefineLibraryMethod("default_proc", 0x51, new System.Delegate[] {
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.Proc>(IronRuby.Builtins.IDictionaryOps.GetDefaultProc),
            });
            
            module.DefineLibraryMethod("delete", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Object>(IronRuby.Builtins.IDictionaryOps.Delete),
            });
            
            module.DefineLibraryMethod("delete_if", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.DeleteIf),
            });
            
            module.DefineLibraryMethod("each", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.Each),
            });
            
            module.DefineLibraryMethod("each_key", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.EachKey),
            });
            
            module.DefineLibraryMethod("each_pair", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.EachPair),
            });
            
            module.DefineLibraryMethod("each_value", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.EachValue),
            });
            
            module.DefineLibraryMethod("empty?", 0x51, new System.Delegate[] {
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Boolean>(IronRuby.Builtins.IDictionaryOps.Empty),
            });
            
            module.DefineLibraryMethod("fetch", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Object, System.Object>(IronRuby.Builtins.IDictionaryOps.Fetch),
            });
            
            module.DefineLibraryMethod("has_key?", 0x51, new System.Delegate[] {
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Boolean>(IronRuby.Builtins.IDictionaryOps.HasKey),
            });
            
            module.DefineLibraryMethod("has_value?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Boolean>(IronRuby.Builtins.IDictionaryOps.HasValue),
            });
            
            module.DefineLibraryMethod("include?", 0x51, new System.Delegate[] {
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Boolean>(IronRuby.Builtins.IDictionaryOps.HasKey),
            });
            
            module.DefineLibraryMethod("index", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Object>(IronRuby.Builtins.IDictionaryOps.Index),
            });
            
            module.DefineLibraryMethod("indexes", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IDictionaryOps.Indexes),
            });
            
            module.DefineLibraryMethod("indices", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IDictionaryOps.Indexes),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.MutableString>(IronRuby.Builtins.IDictionaryOps.Inspect),
            });
            
            module.DefineLibraryMethod("invert", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.Hash>(IronRuby.Builtins.IDictionaryOps.Invert),
            });
            
            module.DefineLibraryMethod("key?", 0x51, new System.Delegate[] {
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Boolean>(IronRuby.Builtins.IDictionaryOps.HasKey),
            });
            
            module.DefineLibraryMethod("keys", 0x51, new System.Delegate[] {
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IDictionaryOps.GetKeys),
            });
            
            module.DefineLibraryMethod("length", 0x51, new System.Delegate[] {
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Int32>(IronRuby.Builtins.IDictionaryOps.Length),
            });
            
            module.DefineLibraryMethod("member?", 0x51, new System.Delegate[] {
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Boolean>(IronRuby.Builtins.IDictionaryOps.HasKey),
            });
            
            module.DefineLibraryMethod("merge", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.Merge),
            });
            
            module.DefineLibraryMethod("merge!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.Update),
            });
            
            module.DefineLibraryMethod("rehash", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Collections.Generic.IDictionary<System.Object, System.Object>>(IronRuby.Builtins.IDictionaryOps.Rehash),
            });
            
            module.DefineLibraryMethod("reject", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.Reject),
            });
            
            module.DefineLibraryMethod("reject!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.RejectMutate),
            });
            
            module.DefineLibraryMethod("replace", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.Hash, System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.Hash>(IronRuby.Builtins.IDictionaryOps.Replace),
            });
            
            module.DefineLibraryMethod("select", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.Select),
            });
            
            module.DefineLibraryMethod("shift", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.Shift),
            });
            
            module.DefineLibraryMethod("size", 0x51, new System.Delegate[] {
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Int32>(IronRuby.Builtins.IDictionaryOps.Length),
            });
            
            module.DefineLibraryMethod("sort", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.Sort),
            });
            
            module.DefineLibraryMethod("store", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Object, System.Object>(IronRuby.Builtins.IDictionaryOps.SetElement),
            });
            
            module.DefineLibraryMethod("to_a", 0x51, new System.Delegate[] {
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IDictionaryOps.ToArray),
            });
            
            module.DefineLibraryMethod("to_hash", 0x51, new System.Delegate[] {
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Collections.Generic.IDictionary<System.Object, System.Object>>(IronRuby.Builtins.IDictionaryOps.ToHash),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.MutableString>(IronRuby.Builtins.IDictionaryOps.ToString),
            });
            
            module.DefineLibraryMethod("update", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.Update),
            });
            
            module.DefineLibraryMethod("value?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Boolean>(IronRuby.Builtins.IDictionaryOps.HasValue),
            });
            
            module.DefineLibraryMethod("values", 0x51, new System.Delegate[] {
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IDictionaryOps.GetValues),
            });
            
            module.DefineLibraryMethod("values_at", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IDictionaryOps.ValuesAt),
            });
            
        }
        
        private void LoadSystem__Collections__IEnumerable_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("each", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, System.Collections.IEnumerable, System.Object>(IronRuby.Builtins.IEnumerableOps.Each),
            });
            
        }
        
        private void LoadSystem__Collections__IList_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("-", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Collections.IList, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IListOps.Difference),
            });
            
            module.DefineLibraryMethod("&", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Collections.IList, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IListOps.Intersection),
            });
            
            module.DefineLibraryMethod("*", 0x51, new System.Delegate[] {
                new Func<System.Collections.IList, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IListOps.Repetition),
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.IList, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.IListOps.Repetition),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object, System.Object>(IronRuby.Builtins.IListOps.Repetition),
            });
            
            module.DefineLibraryMethod("[]", 0x51, new System.Delegate[] {
                new Func<System.Collections.IList, System.Int32, System.Object>(IronRuby.Builtins.IListOps.GetElement),
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Int32, System.Int32, System.Collections.IList>(IronRuby.Builtins.IListOps.GetElements),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, System.Collections.IList, IronRuby.Builtins.Range, System.Collections.IList>(IronRuby.Builtins.IListOps.GetElement),
            });
            
            module.DefineLibraryMethod("[]=", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Int32, System.Object, System.Object>(IronRuby.Builtins.IListOps.SetElement),
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Int32, System.Int32, System.Object, System.Object>(IronRuby.Builtins.IListOps.SetElement),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, System.Collections.IList, IronRuby.Builtins.Range, System.Object, System.Object>(IronRuby.Builtins.IListOps.SetElement),
            });
            
            module.DefineLibraryMethod("|", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Collections.IList, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IListOps.Union),
            });
            
            module.DefineLibraryMethod("+", 0x51, new System.Delegate[] {
                new Func<System.Collections.IList, System.Collections.IList, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IListOps.Concatenate),
            });
            
            module.DefineLibraryMethod("<<", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object, System.Collections.IList>(IronRuby.Builtins.IListOps.Append),
            });
            
            module.DefineLibraryMethod("<=>", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.Compare),
            });
            
            module.DefineLibraryMethod("==", 0x51, new System.Delegate[] {
                new Func<System.Collections.IList, System.Object, System.Boolean>(IronRuby.Builtins.IListOps.Equals),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Collections.IList, System.Boolean>(IronRuby.Builtins.IListOps.Equals),
            });
            
            module.DefineLibraryMethod("assoc", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object, System.Collections.IList>(IronRuby.Builtins.IListOps.GetContainerOfFirstItem),
            });
            
            module.DefineLibraryMethod("at", 0x51, new System.Delegate[] {
                new Func<System.Collections.IList, System.Int32, System.Object>(IronRuby.Builtins.IListOps.At),
            });
            
            module.DefineLibraryMethod("clear", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.Clear),
            });
            
            module.DefineLibraryMethod("collect!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.CollectInPlace),
            });
            
            module.DefineLibraryMethod("compact", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.Compact),
            });
            
            module.DefineLibraryMethod("compact!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.CompactInPlace),
            });
            
            module.DefineLibraryMethod("concat", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.Concat),
            });
            
            module.DefineLibraryMethod("delete", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object, System.Object>(IronRuby.Builtins.IListOps.Delete),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object, System.Object>(IronRuby.Builtins.IListOps.Delete),
            });
            
            module.DefineLibraryMethod("delete_at", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Int32, System.Object>(IronRuby.Builtins.IListOps.DeleteAt),
            });
            
            module.DefineLibraryMethod("delete_if", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.DeleteIf),
            });
            
            module.DefineLibraryMethod("each", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.Each),
            });
            
            module.DefineLibraryMethod("each_index", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.EachIndex),
            });
            
            module.DefineLibraryMethod("empty?", 0x51, new System.Delegate[] {
                new Func<System.Collections.IList, System.Boolean>(IronRuby.Builtins.IListOps.Empty),
            });
            
            module.DefineLibraryMethod("eql?", 0x51, new System.Delegate[] {
                new Func<System.Collections.IList, System.Object, System.Boolean>(IronRuby.Builtins.IListOps.HashEquals),
            });
            
            module.DefineLibraryMethod("fetch", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.IList, System.Int32, System.Object, System.Object>(IronRuby.Builtins.IListOps.Fetch),
            });
            
            module.DefineLibraryMethod("fill", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object, System.Int32, System.Collections.IList>(IronRuby.Builtins.IListOps.Fill),
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object, System.Int32, System.Int32, System.Collections.IList>(IronRuby.Builtins.IListOps.Fill),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object, System.Object, System.Object, System.Collections.IList>(IronRuby.Builtins.IListOps.Fill),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object, IronRuby.Builtins.Range, System.Collections.IList>(IronRuby.Builtins.IListOps.Fill),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.IList, System.Int32, System.Object>(IronRuby.Builtins.IListOps.Fill),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.IList, System.Int32, System.Int32, System.Object>(IronRuby.Builtins.IListOps.Fill),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object, System.Object, System.Object>(IronRuby.Builtins.IListOps.Fill),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.IList, IronRuby.Builtins.Range, System.Object>(IronRuby.Builtins.IListOps.Fill),
            });
            
            module.DefineLibraryMethod("first", 0x51, new System.Delegate[] {
                new Func<System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.First),
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Int32, System.Collections.IList>(IronRuby.Builtins.IListOps.First),
            });
            
            module.DefineLibraryMethod("flatten", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object>>, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.Flatten),
            });
            
            module.DefineLibraryMethod("flatten!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object>>, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.FlattenInPlace),
            });
            
            module.DefineLibraryMethod("hash", 0x51, new System.Delegate[] {
                new Func<System.Collections.IList, System.Int32>(IronRuby.Builtins.IListOps.GetHashCode),
            });
            
            module.DefineLibraryMethod("include?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object, System.Boolean>(IronRuby.Builtins.IListOps.Include),
            });
            
            module.DefineLibraryMethod("index", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object, System.Object>(IronRuby.Builtins.IListOps.Index),
            });
            
            module.DefineLibraryMethod("indexes", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object[], System.Object>(IronRuby.Builtins.IListOps.Indexes),
            });
            
            module.DefineLibraryMethod("indices", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object[], System.Object>(IronRuby.Builtins.IListOps.Indexes),
            });
            
            module.DefineLibraryMethod("initialize_copy", 0x52, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.Replace),
            });
            
            module.DefineLibraryMethod("insert", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Int32, System.Object[], System.Collections.IList>(IronRuby.Builtins.IListOps.Insert),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, IronRuby.Builtins.MutableString>(IronRuby.Builtins.IListOps.Inspect),
            });
            
            module.DefineLibraryMethod("join", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.IList, IronRuby.Builtins.MutableString>(IronRuby.Builtins.IListOps.Join),
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.IList, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.IListOps.Join),
            });
            
            module.DefineLibraryMethod("last", 0x51, new System.Delegate[] {
                new Func<System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.Last),
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Int32, System.Collections.IList>(IronRuby.Builtins.IListOps.Last),
            });
            
            module.DefineLibraryMethod("length", 0x51, new System.Delegate[] {
                new Func<System.Collections.IList, System.Int32>(IronRuby.Builtins.IListOps.Length),
            });
            
            module.DefineLibraryMethod("map!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.CollectInPlace),
            });
            
            module.DefineLibraryMethod("nitems", 0x51, new System.Delegate[] {
                new Func<System.Collections.IList, System.Int32>(IronRuby.Builtins.IListOps.NumberOfNonNilItems),
            });
            
            module.DefineLibraryMethod("pop", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.Pop),
            });
            
            module.DefineLibraryMethod("push", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object[], System.Collections.IList>(IronRuby.Builtins.IListOps.Push),
            });
            
            module.DefineLibraryMethod("rassoc", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object, System.Collections.IList>(IronRuby.Builtins.IListOps.GetContainerOfSecondItem),
            });
            
            module.DefineLibraryMethod("reject!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.RejectInPlace),
            });
            
            module.DefineLibraryMethod("replace", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.Replace),
            });
            
            module.DefineLibraryMethod("reverse", 0x51, new System.Delegate[] {
                new Func<System.Collections.IList, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IListOps.Reverse),
            });
            
            module.DefineLibraryMethod("reverse!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.InPlaceReverse),
            });
            
            module.DefineLibraryMethod("rindex", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object, System.Object>(IronRuby.Builtins.IListOps.ReverseIndex),
            });
            
            module.DefineLibraryMethod("shift", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.Shift),
            });
            
            module.DefineLibraryMethod("size", 0x51, new System.Delegate[] {
                new Func<System.Collections.IList, System.Int32>(IronRuby.Builtins.IListOps.Length),
            });
            
            module.DefineLibraryMethod("slice", 0x51, new System.Delegate[] {
                new Func<System.Collections.IList, System.Int32, System.Object>(IronRuby.Builtins.IListOps.GetElement),
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Int32, System.Int32, System.Collections.IList>(IronRuby.Builtins.IListOps.GetElements),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, System.Collections.IList, IronRuby.Builtins.Range, System.Collections.IList>(IronRuby.Builtins.IListOps.GetElement),
            });
            
            module.DefineLibraryMethod("slice!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Int32, System.Object>(IronRuby.Builtins.IListOps.SliceInPlace),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, System.Collections.IList, IronRuby.Builtins.Range, System.Object>(IronRuby.Builtins.IListOps.SliceInPlace),
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Int32, System.Int32, System.Collections.IList>(IronRuby.Builtins.IListOps.SliceInPlace),
            });
            
            module.DefineLibraryMethod("sort", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.Sort),
            });
            
            module.DefineLibraryMethod("sort!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.SortInPlace),
            });
            
            module.DefineLibraryMethod("to_a", 0x51, new System.Delegate[] {
                new Func<System.Collections.IList, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IListOps.ToArray),
            });
            
            module.DefineLibraryMethod("to_ary", 0x51, new System.Delegate[] {
                new Func<System.Collections.IList, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IListOps.ToArray),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, System.Collections.IList, IronRuby.Builtins.MutableString>(IronRuby.Builtins.IListOps.Join),
            });
            
            module.DefineLibraryMethod("transpose", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IListOps.Transpose),
            });
            
            module.DefineLibraryMethod("uniq", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.Unique),
            });
            
            module.DefineLibraryMethod("uniq!", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.UniqueSelf),
            });
            
            module.DefineLibraryMethod("unshift", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object[], System.Collections.IList>(IronRuby.Builtins.IListOps.Unshift),
            });
            
            module.DefineLibraryMethod("values_at", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.RubyContext, System.Collections.IList, System.Object[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IListOps.ValuesAt),
            });
            
        }
        
        private void LoadSystem__IComparable_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("<=>", 0x51, new System.Delegate[] {
                new Func<System.IComparable, System.Object, System.Int32>(IronRuby.Builtins.IComparableOps.Compare),
            });
            
        }
        
        private void LoadSystem__Type_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("to_class", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Type, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.TypeOps.ToClass),
            });
            
            module.DefineLibraryMethod("to_module", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Type, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.TypeOps.ToModule),
            });
            
        }
        
        private void LoadSystemExit_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("status", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.SystemExit, System.Int32>(IronRuby.Builtins.SystemExitOps.GetStatus),
            });
            
            module.DefineLibraryMethod("success?", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.SystemExit, System.Boolean>(IronRuby.Builtins.SystemExitOps.IsSuccessful),
            });
            
        }
        
        private void LoadThread_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("[]", 0x51, new System.Delegate[] {
                new Func<System.Threading.Thread, Microsoft.Scripting.SymbolId, System.Object>(IronRuby.Builtins.ThreadOps.GetElement),
                new Func<System.Threading.Thread, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.ThreadOps.GetElement),
                new Func<IronRuby.Runtime.RubyContext, System.Threading.Thread, System.Object, System.Object>(IronRuby.Builtins.ThreadOps.GetElement),
            });
            
            module.DefineLibraryMethod("[]=", 0x51, new System.Delegate[] {
                new Func<System.Threading.Thread, Microsoft.Scripting.SymbolId, System.Object, System.Object>(IronRuby.Builtins.ThreadOps.SetElement),
                new Func<System.Threading.Thread, IronRuby.Builtins.MutableString, System.Object, System.Object>(IronRuby.Builtins.ThreadOps.SetElement),
                new Func<IronRuby.Runtime.RubyContext, System.Threading.Thread, System.Object, System.Object, System.Object>(IronRuby.Builtins.ThreadOps.SetElement),
            });
            
            module.DefineLibraryMethod("abort_on_exception", 0x51, new System.Delegate[] {
                new Func<System.Threading.Thread, System.Object>(IronRuby.Builtins.ThreadOps.AbortOnException),
            });
            
            module.DefineLibraryMethod("abort_on_exception=", 0x51, new System.Delegate[] {
                new Func<System.Threading.Thread, System.Boolean, System.Object>(IronRuby.Builtins.ThreadOps.AbortOnException),
            });
            
            module.DefineLibraryMethod("alive?", 0x51, new System.Delegate[] {
                new Func<System.Threading.Thread, System.Boolean>(IronRuby.Builtins.ThreadOps.IsAlive),
            });
            
            module.DefineLibraryMethod("exit", 0x51, new System.Delegate[] {
                new Func<System.Threading.Thread, System.Threading.Thread>(IronRuby.Builtins.ThreadOps.Kill),
            });
            
            module.DefineLibraryMethod("group", 0x51, new System.Delegate[] {
                new Func<System.Threading.Thread, IronRuby.Builtins.ThreadGroup>(IronRuby.Builtins.ThreadOps.Group),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Threading.Thread, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ThreadOps.Inspect),
            });
            
            module.DefineLibraryMethod("join", 0x51, new System.Delegate[] {
                new Func<System.Threading.Thread, System.Threading.Thread>(IronRuby.Builtins.ThreadOps.Join),
                new Func<System.Threading.Thread, System.Double, System.Threading.Thread>(IronRuby.Builtins.ThreadOps.Join),
            });
            
            module.DefineLibraryMethod("key?", 0x51, new System.Delegate[] {
                new Func<System.Threading.Thread, Microsoft.Scripting.SymbolId, System.Object>(IronRuby.Builtins.ThreadOps.HasKey),
                new Func<System.Threading.Thread, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.ThreadOps.HasKey),
                new Func<IronRuby.Runtime.RubyContext, System.Threading.Thread, System.Object, System.Object>(IronRuby.Builtins.ThreadOps.HasKey),
            });
            
            module.DefineLibraryMethod("keys", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Threading.Thread, System.Object>(IronRuby.Builtins.ThreadOps.Keys),
            });
            
            module.DefineLibraryMethod("kill", 0x51, new System.Delegate[] {
                new Func<System.Threading.Thread, System.Threading.Thread>(IronRuby.Builtins.ThreadOps.Kill),
            });
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("run", 0x51, new System.Delegate[] {
                new Action<System.Threading.Thread>(IronRuby.Builtins.ThreadOps.Run),
            });
            
            #endif
            module.DefineLibraryMethod("status", 0x51, new System.Delegate[] {
                new Func<System.Threading.Thread, System.Object>(IronRuby.Builtins.ThreadOps.Status),
            });
            
            module.DefineLibraryMethod("terminate", 0x51, new System.Delegate[] {
                new Func<System.Threading.Thread, System.Threading.Thread>(IronRuby.Builtins.ThreadOps.Kill),
            });
            
            module.DefineLibraryMethod("value", 0x51, new System.Delegate[] {
                new Func<System.Threading.Thread, System.Object>(IronRuby.Builtins.ThreadOps.Value),
            });
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("wakeup", 0x51, new System.Delegate[] {
                new Action<System.Threading.Thread>(IronRuby.Builtins.ThreadOps.Run),
            });
            
            #endif
        }
        
        private void LoadThread_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("abort_on_exception", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Object>(IronRuby.Builtins.ThreadOps.GlobalAbortOnException),
            });
            
            module.DefineLibraryMethod("abort_on_exception=", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Boolean, System.Object>(IronRuby.Builtins.ThreadOps.GlobalAbortOnException),
            });
            
            module.DefineLibraryMethod("critical", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Boolean>(IronRuby.Builtins.ThreadOps.Critical),
            });
            
            module.DefineLibraryMethod("critical=", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Boolean, System.Boolean>(IronRuby.Builtins.ThreadOps.Critical),
            });
            
            module.DefineLibraryMethod("current", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Threading.Thread>(IronRuby.Builtins.ThreadOps.Current),
            });
            
            module.DefineLibraryMethod("list", 0x61, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ThreadOps.List),
            });
            
            module.DefineLibraryMethod("new", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object[], System.Threading.Thread>(IronRuby.Builtins.ThreadOps.CreateThread),
            });
            
            module.DefineLibraryMethod("pass", 0x61, new System.Delegate[] {
                new Action<System.Object>(IronRuby.Builtins.ThreadOps.Yield),
            });
            
            module.DefineLibraryMethod("start", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object[], System.Threading.Thread>(IronRuby.Builtins.ThreadOps.CreateThread),
            });
            
            module.DefineLibraryMethod("stop", 0x61, new System.Delegate[] {
                new Action<System.Object>(IronRuby.Builtins.ThreadOps.Stop),
            });
            
        }
        
        private void LoadThreadGroup_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("Default", IronRuby.Builtins.ThreadGroup.Default);
            
            module.DefineLibraryMethod("add", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.ThreadGroup, System.Threading.Thread, IronRuby.Builtins.ThreadGroup>(IronRuby.Builtins.ThreadGroup.Add),
            });
            
            module.DefineLibraryMethod("list", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.ThreadGroup, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ThreadGroup.List),
            });
            
        }
        
        private void LoadTime_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("-", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Double, System.DateTime>(IronRuby.Builtins.TimeOps.SubtractSeconds),
                new Func<System.DateTime, System.DateTime, System.Double>(IronRuby.Builtins.TimeOps.SubtractTime),
            });
            
            module.DefineLibraryMethod("_dump", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.DateTime, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TimeOps.Dump),
            });
            
            module.DefineLibraryMethod("+", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Double, System.DateTime>(IronRuby.Builtins.TimeOps.AddSeconds),
                new Func<System.DateTime, System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.AddTime),
            });
            
            module.DefineLibraryMethod("<=>", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Double, System.Int32>(IronRuby.Builtins.TimeOps.CompareSeconds),
                new Func<System.DateTime, System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.CompareTo),
            });
            
            module.DefineLibraryMethod("asctime", 0x51, new System.Delegate[] {
                new Func<System.DateTime, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TimeOps.ToString),
            });
            
            module.DefineLibraryMethod("ctime", 0x51, new System.Delegate[] {
                new Func<System.DateTime, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TimeOps.ToString),
            });
            
            module.DefineLibraryMethod("day", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.Day),
            });
            
            module.DefineLibraryMethod("dst?", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Boolean>(IronRuby.Builtins.TimeOps.IsDST),
            });
            
            module.DefineLibraryMethod("dup", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.Clone),
            });
            
            module.DefineLibraryMethod("eql?", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.DateTime, System.Boolean>(IronRuby.Builtins.TimeOps.Eql),
                new Func<System.DateTime, System.Object, System.Boolean>(IronRuby.Builtins.TimeOps.Eql),
            });
            
            module.DefineLibraryMethod("getgm", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.GetUTC),
            });
            
            module.DefineLibraryMethod("getlocal", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.GetLocal),
            });
            
            module.DefineLibraryMethod("getutc", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.GetUTC),
            });
            
            module.DefineLibraryMethod("gmt?", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Boolean>(IronRuby.Builtins.TimeOps.IsUTC),
            });
            
            module.DefineLibraryMethod("gmt_offset", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Object>(IronRuby.Builtins.TimeOps.Offset),
            });
            
            module.DefineLibraryMethod("gmtime", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.ToUTC),
            });
            
            module.DefineLibraryMethod("gmtoff", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Object>(IronRuby.Builtins.TimeOps.Offset),
            });
            
            module.DefineLibraryMethod("hash", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.GetHash),
            });
            
            module.DefineLibraryMethod("hour", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.Hour),
            });
            
            module.DefineLibraryMethod("inspect", 0x51, new System.Delegate[] {
                new Func<System.DateTime, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TimeOps.ToString),
            });
            
            module.DefineLibraryMethod("isdst", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Boolean>(IronRuby.Builtins.TimeOps.IsDST),
            });
            
            module.DefineLibraryMethod("localtime", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.ToLocalTime),
            });
            
            module.DefineLibraryMethod("mday", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.Day),
            });
            
            module.DefineLibraryMethod("min", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.Minute),
            });
            
            module.DefineLibraryMethod("mon", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.Month),
            });
            
            module.DefineLibraryMethod("month", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.Month),
            });
            
            module.DefineLibraryMethod("sec", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.Second),
            });
            
            module.DefineLibraryMethod("strftime", 0x51, new System.Delegate[] {
                new Func<System.DateTime, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TimeOps.FormatTime),
            });
            
            module.DefineLibraryMethod("succ", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.SuccessiveSecond),
            });
            
            module.DefineLibraryMethod("to_a", 0x51, new System.Delegate[] {
                new Func<System.DateTime, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.TimeOps.ToArray),
            });
            
            module.DefineLibraryMethod("to_f", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Double>(IronRuby.Builtins.TimeOps.ToFloatSeconds),
            });
            
            module.DefineLibraryMethod("to_i", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Object>(IronRuby.Builtins.TimeOps.ToSeconds),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<System.DateTime, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TimeOps.ToString),
            });
            
            module.DefineLibraryMethod("tv_sec", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Object>(IronRuby.Builtins.TimeOps.ToSeconds),
            });
            
            module.DefineLibraryMethod("tv_usec", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Object>(IronRuby.Builtins.TimeOps.GetMicroSeconds),
            });
            
            module.DefineLibraryMethod("usec", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Object>(IronRuby.Builtins.TimeOps.GetMicroSeconds),
            });
            
            module.DefineLibraryMethod("utc", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.ToUTC),
            });
            
            module.DefineLibraryMethod("utc?", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Boolean>(IronRuby.Builtins.TimeOps.IsUTC),
            });
            
            module.DefineLibraryMethod("utc_offset", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Object>(IronRuby.Builtins.TimeOps.Offset),
            });
            
            module.DefineLibraryMethod("wday", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.DayOfWeek),
            });
            
            module.DefineLibraryMethod("yday", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.DayOfYear),
            });
            
            module.DefineLibraryMethod("year", 0x51, new System.Delegate[] {
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.Year),
            });
            
            module.DefineLibraryMethod("zone", 0x51, new System.Delegate[] {
                new Func<System.DateTime, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TimeOps.GetZone),
            });
            
        }
        
        private void LoadTime_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("_load", 0x61, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.DateTime>(IronRuby.Builtins.TimeOps.Load),
            });
            
            module.DefineLibraryMethod("at", 0x61, new System.Delegate[] {
                new Func<System.Object, System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.Create),
                new Func<System.Object, System.Double, System.DateTime>(IronRuby.Builtins.TimeOps.Create),
                new Func<System.Object, System.Int64, System.Int64, System.DateTime>(IronRuby.Builtins.TimeOps.Create),
            });
            
            module.DefineLibraryMethod("gm", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime),
                new Func<System.Object, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, System.Object, System.Object[], System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime),
            });
            
            module.DefineLibraryMethod("local", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime),
                new Func<System.Object, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, System.Object, System.Object[], System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime),
            });
            
            module.DefineLibraryMethod("mktime", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime),
                new Func<System.Object, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, System.Object, System.Object[], System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime),
            });
            
            module.DefineLibraryMethod("now", 0x61, new System.Delegate[] {
                new Func<System.Object, System.DateTime>(IronRuby.Builtins.TimeOps.CreateTime),
            });
            
            module.DefineLibraryMethod("today", 0x61, new System.Delegate[] {
                new Func<System.Object, System.DateTime>(IronRuby.Builtins.TimeOps.Today),
            });
            
            module.DefineLibraryMethod("utc", 0x61, new System.Delegate[] {
                new Func<System.Object, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime),
                new Func<System.Object, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime),
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, System.Object, System.Object[], System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime),
            });
            
        }
        
        private void LoadTrueClass_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("&", 0x51, new System.Delegate[] {
                new Func<System.Boolean, System.Object, System.Boolean>(IronRuby.Builtins.TrueClass.And),
                new Func<System.Boolean, System.Boolean, System.Boolean>(IronRuby.Builtins.TrueClass.And),
            });
            
            module.DefineLibraryMethod("^", 0x51, new System.Delegate[] {
                new Func<System.Boolean, System.Object, System.Boolean>(IronRuby.Builtins.TrueClass.Xor),
                new Func<System.Boolean, System.Boolean, System.Boolean>(IronRuby.Builtins.TrueClass.Xor),
            });
            
            module.DefineLibraryMethod("|", 0x51, new System.Delegate[] {
                new Func<System.Boolean, System.Object, System.Boolean>(IronRuby.Builtins.TrueClass.Or),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<System.Boolean, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TrueClass.ToString),
            });
            
        }
        
        private void LoadUnboundMethod_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("==", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.UnboundMethod, IronRuby.Builtins.UnboundMethod, System.Boolean>(IronRuby.Builtins.UnboundMethod.Equal),
                new Func<IronRuby.Builtins.UnboundMethod, System.Object, System.Boolean>(IronRuby.Builtins.UnboundMethod.Equal),
            });
            
            module.DefineLibraryMethod("arity", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.UnboundMethod, System.Int32>(IronRuby.Builtins.UnboundMethod.GetArity),
            });
            
            module.DefineLibraryMethod("bind", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.UnboundMethod, System.Object, IronRuby.Builtins.RubyMethod>(IronRuby.Builtins.UnboundMethod.Bind),
            });
            
            module.DefineLibraryMethod("clone", 0x51, new System.Delegate[] {
                new Func<IronRuby.Builtins.UnboundMethod, IronRuby.Builtins.UnboundMethod>(IronRuby.Builtins.UnboundMethod.Clone),
            });
            
            module.DefineLibraryMethod("to_s", 0x51, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.UnboundMethod, IronRuby.Builtins.MutableString>(IronRuby.Builtins.UnboundMethod.ToS),
            });
            
        }
        
        public static System.Exception/*!*/ ExceptionFactory__EOFError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.Builtins.EOFError(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__FloatDomainError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.Builtins.FloatDomainError(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__Interrupt(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.Builtins.Interrupt(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__LoadError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.Builtins.LoadError(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__LocalJumpError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.Builtins.LocalJumpError(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__NoMemoryError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.Builtins.NoMemoryError(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__NotImplementedError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.Builtins.NotImplementedError(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__RegexpError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.Builtins.RegexpError(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__RuntimeError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.Builtins.RuntimeError(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__ScriptError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.Builtins.ScriptError(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__SignalException(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.Builtins.SignalException(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__SyntaxError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.Builtins.SyntaxError(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__SystemExit(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.Builtins.SystemExit(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__SystemStackError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.Builtins.SystemStackError(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__ThreadError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.Builtins.ThreadError(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__ArgumentError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new System.ArgumentException(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__RangeError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new System.ArgumentOutOfRangeException(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__ZeroDivisionError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new System.DivideByZeroException(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__Exception(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new System.Exception(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__IndexError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new System.IndexOutOfRangeException(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__TypeError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new System.InvalidOperationException(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__IOError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new System.IO.IOException(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__NameError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new System.MemberAccessException(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__NoMethodError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new System.MissingMethodException(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__SystemCallError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new System.Runtime.InteropServices.ExternalException(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__SecurityError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new System.Security.SecurityException(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__StandardError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new System.SystemException(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
    }
}

namespace IronRuby.StandardLibrary.Threading {
    public sealed class ThreadingLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(System.Object));
            
            
            DefineGlobalClass("ConditionVariable", typeof(IronRuby.StandardLibrary.Threading.RubyConditionVariable), true, classRef0, new Action<IronRuby.Builtins.RubyModule>(LoadConditionVariable_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            DefineGlobalClass("Mutex", typeof(IronRuby.StandardLibrary.Threading.RubyMutex), true, classRef0, new Action<IronRuby.Builtins.RubyModule>(LoadMutex_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            IronRuby.Builtins.RubyClass def1 = DefineGlobalClass("Queue", typeof(IronRuby.StandardLibrary.Threading.RubyQueue), true, classRef0, new Action<IronRuby.Builtins.RubyModule>(LoadQueue_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            DefineGlobalClass("SizedQueue", typeof(IronRuby.StandardLibrary.Threading.SizedQueue), true, def1, new Action<IronRuby.Builtins.RubyModule>(LoadSizedQueue_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, null);
        }
        
        private void LoadConditionVariable_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("broadcast", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.RubyConditionVariable, IronRuby.StandardLibrary.Threading.RubyConditionVariable>(IronRuby.StandardLibrary.Threading.RubyConditionVariable.Broadcast),
            });
            
            module.DefineLibraryMethod("signal", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.RubyConditionVariable, IronRuby.StandardLibrary.Threading.RubyConditionVariable>(IronRuby.StandardLibrary.Threading.RubyConditionVariable.Signal),
            });
            
            module.DefineLibraryMethod("wait", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.RubyConditionVariable, IronRuby.StandardLibrary.Threading.RubyMutex, IronRuby.StandardLibrary.Threading.RubyConditionVariable>(IronRuby.StandardLibrary.Threading.RubyConditionVariable.Wait),
            });
            
        }
        
        private void LoadMutex_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("exclusive_unlock", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, IronRuby.StandardLibrary.Threading.RubyMutex, System.Boolean>(IronRuby.StandardLibrary.Threading.RubyMutex.ExclusiveUnlock),
            });
            
            module.DefineLibraryMethod("lock", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.RubyMutex, IronRuby.StandardLibrary.Threading.RubyMutex>(IronRuby.StandardLibrary.Threading.RubyMutex.Lock),
            });
            
            module.DefineLibraryMethod("locked?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.RubyMutex, System.Boolean>(IronRuby.StandardLibrary.Threading.RubyMutex.IsLocked),
            });
            
            module.DefineLibraryMethod("synchronize", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, IronRuby.StandardLibrary.Threading.RubyMutex, System.Object>(IronRuby.StandardLibrary.Threading.RubyMutex.Synchronize),
            });
            
            module.DefineLibraryMethod("try_lock", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.RubyMutex, System.Boolean>(IronRuby.StandardLibrary.Threading.RubyMutex.TryLock),
            });
            
            module.DefineLibraryMethod("unlock", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.RubyMutex, IronRuby.StandardLibrary.Threading.RubyMutex>(IronRuby.StandardLibrary.Threading.RubyMutex.Unlock),
            });
            
        }
        
        private void LoadQueue_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("<<", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Object, IronRuby.StandardLibrary.Threading.RubyQueue>(IronRuby.StandardLibrary.Threading.RubyQueue.Enqueue),
            });
            
            module.DefineLibraryMethod("clear", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, IronRuby.StandardLibrary.Threading.RubyQueue>(IronRuby.StandardLibrary.Threading.RubyQueue.Clear),
            });
            
            module.DefineLibraryMethod("deq", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Boolean, System.Object>(IronRuby.StandardLibrary.Threading.RubyQueue.Dequeue),
            });
            
            module.DefineLibraryMethod("empty?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Boolean>(IronRuby.StandardLibrary.Threading.RubyQueue.IsEmpty),
            });
            
            module.DefineLibraryMethod("enq", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Object, IronRuby.StandardLibrary.Threading.RubyQueue>(IronRuby.StandardLibrary.Threading.RubyQueue.Enqueue),
            });
            
            module.DefineLibraryMethod("length", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Int32>(IronRuby.StandardLibrary.Threading.RubyQueue.GetCount),
            });
            
            module.DefineLibraryMethod("num_waiting", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Int32>(IronRuby.StandardLibrary.Threading.RubyQueue.GetNumberOfWaitingThreads),
            });
            
            module.DefineLibraryMethod("pop", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Boolean, System.Object>(IronRuby.StandardLibrary.Threading.RubyQueue.Dequeue),
            });
            
            module.DefineLibraryMethod("push", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Object, IronRuby.StandardLibrary.Threading.RubyQueue>(IronRuby.StandardLibrary.Threading.RubyQueue.Enqueue),
            });
            
            module.DefineLibraryMethod("shift", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Boolean, System.Object>(IronRuby.StandardLibrary.Threading.RubyQueue.Dequeue),
            });
            
            module.DefineLibraryMethod("size", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Int32>(IronRuby.StandardLibrary.Threading.RubyQueue.GetCount),
            });
            
        }
        
        private void LoadSizedQueue_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("<<", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.SizedQueue, System.Object, IronRuby.StandardLibrary.Threading.SizedQueue>(IronRuby.StandardLibrary.Threading.SizedQueue.Enqueue),
            });
            
            module.DefineLibraryMethod("deq", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.SizedQueue, System.Object[], System.Object>(IronRuby.StandardLibrary.Threading.SizedQueue.Dequeue),
            });
            
            module.DefineLibraryMethod("enq", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.SizedQueue, System.Object, IronRuby.StandardLibrary.Threading.SizedQueue>(IronRuby.StandardLibrary.Threading.SizedQueue.Enqueue),
            });
            
            module.DefineLibraryMethod("initialize", 0x12, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.SizedQueue, System.Int32, IronRuby.StandardLibrary.Threading.SizedQueue>(IronRuby.StandardLibrary.Threading.SizedQueue.Reinitialize),
            });
            
            module.DefineLibraryMethod("max", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.SizedQueue, System.Int32>(IronRuby.StandardLibrary.Threading.SizedQueue.GetLimit),
            });
            
            module.DefineLibraryMethod("max=", 0x11, new System.Delegate[] {
                new Action<IronRuby.StandardLibrary.Threading.SizedQueue, System.Int32>(IronRuby.StandardLibrary.Threading.SizedQueue.SetLimit),
            });
            
            module.DefineLibraryMethod("pop", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.SizedQueue, System.Object[], System.Object>(IronRuby.StandardLibrary.Threading.SizedQueue.Dequeue),
            });
            
            module.DefineLibraryMethod("push", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.SizedQueue, System.Object, IronRuby.StandardLibrary.Threading.SizedQueue>(IronRuby.StandardLibrary.Threading.SizedQueue.Enqueue),
            });
            
            module.DefineLibraryMethod("shift", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Threading.SizedQueue, System.Object[], System.Object>(IronRuby.StandardLibrary.Threading.SizedQueue.Dequeue),
            });
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.Sockets {
    public sealed class SocketsLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(IronRuby.Builtins.RubyIO));
            IronRuby.Builtins.RubyClass classRef1 = GetClass(typeof(System.SystemException));
            
            
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def3 = DefineGlobalClass("BasicSocket", typeof(IronRuby.StandardLibrary.Sockets.RubyBasicSocket), true, classRef0, new Action<IronRuby.Builtins.RubyModule>(LoadBasicSocket_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadBasicSocket_Class), IronRuby.Builtins.RubyModule.EmptyArray, null);
            #endif
            #if !SILVERLIGHT && !SILVERLIGHT
            IronRuby.Builtins.RubyModule def2 = DefineModule("Socket::Constants", typeof(IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants), true, new Action<IronRuby.Builtins.RubyModule>(LoadSocket__Constants_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray);
            #endif
            #if !SILVERLIGHT
            DefineGlobalClass("SocketError", typeof(System.Net.Sockets.SocketException), false, classRef1, new Action<IronRuby.Builtins.RubyModule>(LoadSocketError_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(IronRuby.StandardLibrary.Sockets.SocketErrorOps.Create),
            });
            #endif
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def5 = DefineGlobalClass("IPSocket", typeof(IronRuby.StandardLibrary.Sockets.IPSocket), true, def3, new Action<IronRuby.Builtins.RubyModule>(LoadIPSocket_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadIPSocket_Class), IronRuby.Builtins.RubyModule.EmptyArray, null);
            #endif
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def1 = DefineGlobalClass("Socket", typeof(IronRuby.StandardLibrary.Sockets.RubySocket), true, def3, new Action<IronRuby.Builtins.RubyModule>(LoadSocket_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadSocket_Class), IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, System.Object, System.Int32, System.Int32, IronRuby.StandardLibrary.Sockets.RubySocket>(IronRuby.StandardLibrary.Sockets.RubySocket.CreateSocket),
            });
            #endif
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def4 = DefineGlobalClass("TCPSocket", typeof(IronRuby.StandardLibrary.Sockets.TCPSocket), true, def5, null, new Action<IronRuby.Builtins.RubyModule>(LoadTCPSocket_Class), IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object, IronRuby.StandardLibrary.Sockets.TCPSocket>(IronRuby.StandardLibrary.Sockets.TCPSocket.CreateTCPSocket),
            });
            #endif
            #if !SILVERLIGHT
            DefineGlobalClass("UDPSocket", typeof(IronRuby.StandardLibrary.Sockets.UDPSocket), true, def5, new Action<IronRuby.Builtins.RubyModule>(LoadUDPSocket_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.StandardLibrary.Sockets.UDPSocket>(IronRuby.StandardLibrary.Sockets.UDPSocket.CreateUDPSocket),
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, System.Object, IronRuby.StandardLibrary.Sockets.UDPSocket>(IronRuby.StandardLibrary.Sockets.UDPSocket.CreateUDPSocket),
            });
            #endif
            #if !SILVERLIGHT
            DefineGlobalClass("TCPServer", typeof(IronRuby.StandardLibrary.Sockets.TCPServer), true, def4, new Action<IronRuby.Builtins.RubyModule>(LoadTCPServer_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object, IronRuby.StandardLibrary.Sockets.TCPServer>(IronRuby.StandardLibrary.Sockets.TCPServer.CreateTCPServer),
            });
            #endif
            #if !SILVERLIGHT && !SILVERLIGHT
            def1.SetConstant("Constants", def2);
            #endif
        }
        
        #if !SILVERLIGHT
        private void LoadBasicSocket_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("close_read", 0x11, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.CloseRead),
            });
            
            module.DefineLibraryMethod("close_write", 0x11, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.CloseWrite),
            });
            
            module.DefineLibraryMethod("getpeername", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Sockets.RubyBasicSocket, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.GetPeerName),
            });
            
            module.DefineLibraryMethod("getsockname", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Sockets.RubyBasicSocket, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.GetSocketName),
            });
            
            module.DefineLibraryMethod("getsockopt", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, System.Int32, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.GetSocketOption),
            });
            
            module.DefineLibraryMethod("recv", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, System.Int32, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.Receive),
            });
            
            module.DefineLibraryMethod("recv_nonblock", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, System.Int32, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.ReceiveNonBlocking),
            });
            
            module.DefineLibraryMethod("send", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, IronRuby.Builtins.MutableString, System.Object, System.Int32>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.Send),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, IronRuby.Builtins.MutableString, System.Object, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.Send),
            });
            
            module.DefineLibraryMethod("setsockopt", 0x11, new System.Delegate[] {
                new Action<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, System.Int32, System.Int32, System.Int32>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.SetSocketOption),
                new Action<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, System.Int32, System.Int32, System.Boolean>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.SetSocketOption),
                new Action<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, System.Int32, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.SetSocketOption),
            });
            
            module.DefineLibraryMethod("shutdown", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, System.Int32, System.Int32>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.Shutdown),
            });
            
        }
        #endif
        
        #if !SILVERLIGHT
        private void LoadBasicSocket_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("do_not_reverse_lookup", 0x21, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Boolean>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.GetDoNotReverseLookup),
            });
            
            module.DefineLibraryMethod("do_not_reverse_lookup=", 0x21, new System.Delegate[] {
                new Action<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Boolean>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.SetDoNotReverseLookup),
            });
            
            module.DefineLibraryMethod("for_fd", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.Int32, IronRuby.StandardLibrary.Sockets.RubyBasicSocket>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.ForFileDescriptor),
            });
            
        }
        #endif
        
        #if !SILVERLIGHT
        private void LoadIPSocket_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("addr", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.IPSocket, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.IPSocket.GetLocalAddress),
            });
            
            module.DefineLibraryMethod("peeraddr", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.IPSocket, System.Object>(IronRuby.StandardLibrary.Sockets.IPSocket.GetPeerAddress),
            });
            
            module.DefineLibraryMethod("recvfrom", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.IPSocket, System.Int32, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.IPSocket.ReceiveFrom),
            });
            
        }
        #endif
        
        #if !SILVERLIGHT
        private void LoadIPSocket_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("getaddress", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.IPSocket.GetAddress),
            });
            
        }
        #endif
        
        #if !SILVERLIGHT
        private void LoadSocket_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("AF_APPLETALK", IronRuby.StandardLibrary.Sockets.RubySocket.AF_APPLETALK);
            module.SetConstant("AF_ATM", IronRuby.StandardLibrary.Sockets.RubySocket.AF_ATM);
            module.SetConstant("AF_CCITT", IronRuby.StandardLibrary.Sockets.RubySocket.AF_CCITT);
            module.SetConstant("AF_CHAOS", IronRuby.StandardLibrary.Sockets.RubySocket.AF_CHAOS);
            module.SetConstant("AF_DATAKIT", IronRuby.StandardLibrary.Sockets.RubySocket.AF_DATAKIT);
            module.SetConstant("AF_DLI", IronRuby.StandardLibrary.Sockets.RubySocket.AF_DLI);
            module.SetConstant("AF_ECMA", IronRuby.StandardLibrary.Sockets.RubySocket.AF_ECMA);
            module.SetConstant("AF_HYLINK", IronRuby.StandardLibrary.Sockets.RubySocket.AF_HYLINK);
            module.SetConstant("AF_IMPLINK", IronRuby.StandardLibrary.Sockets.RubySocket.AF_IMPLINK);
            module.SetConstant("AF_INET", IronRuby.StandardLibrary.Sockets.RubySocket.AF_INET);
            module.SetConstant("AF_IPX", IronRuby.StandardLibrary.Sockets.RubySocket.AF_IPX);
            module.SetConstant("AF_ISO", IronRuby.StandardLibrary.Sockets.RubySocket.AF_ISO);
            module.SetConstant("AF_LAT", IronRuby.StandardLibrary.Sockets.RubySocket.AF_LAT);
            module.SetConstant("AF_MAX", IronRuby.StandardLibrary.Sockets.RubySocket.AF_MAX);
            module.SetConstant("AF_NETBIOS", IronRuby.StandardLibrary.Sockets.RubySocket.AF_NETBIOS);
            module.SetConstant("AF_NS", IronRuby.StandardLibrary.Sockets.RubySocket.AF_NS);
            module.SetConstant("AF_OSI", IronRuby.StandardLibrary.Sockets.RubySocket.AF_OSI);
            module.SetConstant("AF_PUP", IronRuby.StandardLibrary.Sockets.RubySocket.AF_PUP);
            module.SetConstant("AF_SNA", IronRuby.StandardLibrary.Sockets.RubySocket.AF_SNA);
            module.SetConstant("AF_UNIX", IronRuby.StandardLibrary.Sockets.RubySocket.AF_UNIX);
            module.SetConstant("AF_UNSPEC", IronRuby.StandardLibrary.Sockets.RubySocket.AF_UNSPEC);
            module.SetConstant("AI_ADDRCONFIG", IronRuby.StandardLibrary.Sockets.RubySocket.AI_ADDRCONFIG);
            module.SetConstant("AI_ALL", IronRuby.StandardLibrary.Sockets.RubySocket.AI_ALL);
            module.SetConstant("AI_CANONNAME", IronRuby.StandardLibrary.Sockets.RubySocket.AI_CANONNAME);
            module.SetConstant("AI_DEFAULT", IronRuby.StandardLibrary.Sockets.RubySocket.AI_DEFAULT);
            module.SetConstant("AI_MASK", IronRuby.StandardLibrary.Sockets.RubySocket.AI_MASK);
            module.SetConstant("AI_NUMERICHOST", IronRuby.StandardLibrary.Sockets.RubySocket.AI_NUMERICHOST);
            module.SetConstant("AI_PASSIVE", IronRuby.StandardLibrary.Sockets.RubySocket.AI_PASSIVE);
            module.SetConstant("AI_V4MAPPED", IronRuby.StandardLibrary.Sockets.RubySocket.AI_V4MAPPED);
            module.SetConstant("AI_V4MAPPED_CFG", IronRuby.StandardLibrary.Sockets.RubySocket.AI_V4MAPPED_CFG);
            module.SetConstant("EAI_ADDRFAMILY", IronRuby.StandardLibrary.Sockets.RubySocket.EAI_ADDRFAMILY);
            module.SetConstant("EAI_AGAIN", IronRuby.StandardLibrary.Sockets.RubySocket.EAI_AGAIN);
            module.SetConstant("EAI_BADFLAGS", IronRuby.StandardLibrary.Sockets.RubySocket.EAI_BADFLAGS);
            module.SetConstant("EAI_BADHINTS", IronRuby.StandardLibrary.Sockets.RubySocket.EAI_BADHINTS);
            module.SetConstant("EAI_FAIL", IronRuby.StandardLibrary.Sockets.RubySocket.EAI_FAIL);
            module.SetConstant("EAI_FAMILY", IronRuby.StandardLibrary.Sockets.RubySocket.EAI_FAMILY);
            module.SetConstant("EAI_MAX", IronRuby.StandardLibrary.Sockets.RubySocket.EAI_MAX);
            module.SetConstant("EAI_MEMORY", IronRuby.StandardLibrary.Sockets.RubySocket.EAI_MEMORY);
            module.SetConstant("EAI_NODATA", IronRuby.StandardLibrary.Sockets.RubySocket.EAI_NODATA);
            module.SetConstant("EAI_NONAME", IronRuby.StandardLibrary.Sockets.RubySocket.EAI_NONAME);
            module.SetConstant("EAI_PROTOCOL", IronRuby.StandardLibrary.Sockets.RubySocket.EAI_PROTOCOL);
            module.SetConstant("EAI_SERVICE", IronRuby.StandardLibrary.Sockets.RubySocket.EAI_SERVICE);
            module.SetConstant("EAI_SOCKTYPE", IronRuby.StandardLibrary.Sockets.RubySocket.EAI_SOCKTYPE);
            module.SetConstant("EAI_SYSTEM", IronRuby.StandardLibrary.Sockets.RubySocket.EAI_SYSTEM);
            module.SetConstant("INADDR_ALLHOSTS_GROUP", IronRuby.StandardLibrary.Sockets.RubySocket.INADDR_ALLHOSTS_GROUP);
            module.SetConstant("INADDR_ANY", IronRuby.StandardLibrary.Sockets.RubySocket.INADDR_ANY);
            module.SetConstant("INADDR_BROADCAST", IronRuby.StandardLibrary.Sockets.RubySocket.INADDR_BROADCAST);
            module.SetConstant("INADDR_LOOPBACK", IronRuby.StandardLibrary.Sockets.RubySocket.INADDR_LOOPBACK);
            module.SetConstant("INADDR_MAX_LOCAL_GROUP", IronRuby.StandardLibrary.Sockets.RubySocket.INADDR_MAX_LOCAL_GROUP);
            module.SetConstant("INADDR_NONE", IronRuby.StandardLibrary.Sockets.RubySocket.INADDR_NONE);
            module.SetConstant("INADDR_UNSPEC_GROUP", IronRuby.StandardLibrary.Sockets.RubySocket.INADDR_UNSPEC_GROUP);
            module.SetConstant("IPPORT_RESERVED", IronRuby.StandardLibrary.Sockets.RubySocket.IPPORT_RESERVED);
            module.SetConstant("IPPORT_USERRESERVED", IronRuby.StandardLibrary.Sockets.RubySocket.IPPORT_USERRESERVED);
            module.SetConstant("IPPROTO_GGP", IronRuby.StandardLibrary.Sockets.RubySocket.IPPROTO_GGP);
            module.SetConstant("IPPROTO_ICMP", IronRuby.StandardLibrary.Sockets.RubySocket.IPPROTO_ICMP);
            module.SetConstant("IPPROTO_IDP", IronRuby.StandardLibrary.Sockets.RubySocket.IPPROTO_IDP);
            module.SetConstant("IPPROTO_IGMP", IronRuby.StandardLibrary.Sockets.RubySocket.IPPROTO_IGMP);
            module.SetConstant("IPPROTO_IP", IronRuby.StandardLibrary.Sockets.RubySocket.IPPROTO_IP);
            module.SetConstant("IPPROTO_MAX", IronRuby.StandardLibrary.Sockets.RubySocket.IPPROTO_MAX);
            module.SetConstant("IPPROTO_ND", IronRuby.StandardLibrary.Sockets.RubySocket.IPPROTO_ND);
            module.SetConstant("IPPROTO_PUP", IronRuby.StandardLibrary.Sockets.RubySocket.IPPROTO_PUP);
            module.SetConstant("IPPROTO_RAW", IronRuby.StandardLibrary.Sockets.RubySocket.IPPROTO_RAW);
            module.SetConstant("IPPROTO_TCP", IronRuby.StandardLibrary.Sockets.RubySocket.IPPROTO_TCP);
            module.SetConstant("IPPROTO_UDP", IronRuby.StandardLibrary.Sockets.RubySocket.IPPROTO_UDP);
            module.SetConstant("MSG_DONTROUTE", IronRuby.StandardLibrary.Sockets.RubySocket.MSG_DONTROUTE);
            module.SetConstant("MSG_OOB", IronRuby.StandardLibrary.Sockets.RubySocket.MSG_OOB);
            module.SetConstant("MSG_PEEK", IronRuby.StandardLibrary.Sockets.RubySocket.MSG_PEEK);
            module.SetConstant("NI_DGRAM", IronRuby.StandardLibrary.Sockets.RubySocket.NI_DGRAM);
            module.SetConstant("NI_MAXHOST", IronRuby.StandardLibrary.Sockets.RubySocket.NI_MAXHOST);
            module.SetConstant("NI_MAXSERV", IronRuby.StandardLibrary.Sockets.RubySocket.NI_MAXSERV);
            module.SetConstant("NI_NAMEREQD", IronRuby.StandardLibrary.Sockets.RubySocket.NI_NAMEREQD);
            module.SetConstant("NI_NOFQDN", IronRuby.StandardLibrary.Sockets.RubySocket.NI_NOFQDN);
            module.SetConstant("NI_NUMERICHOST", IronRuby.StandardLibrary.Sockets.RubySocket.NI_NUMERICHOST);
            module.SetConstant("NI_NUMERICSERV", IronRuby.StandardLibrary.Sockets.RubySocket.NI_NUMERICSERV);
            module.SetConstant("PF_APPLETALK", IronRuby.StandardLibrary.Sockets.RubySocket.PF_APPLETALK);
            module.SetConstant("PF_ATM", IronRuby.StandardLibrary.Sockets.RubySocket.PF_ATM);
            module.SetConstant("PF_CCITT", IronRuby.StandardLibrary.Sockets.RubySocket.PF_CCITT);
            module.SetConstant("PF_CHAOS", IronRuby.StandardLibrary.Sockets.RubySocket.PF_CHAOS);
            module.SetConstant("PF_DATAKIT", IronRuby.StandardLibrary.Sockets.RubySocket.PF_DATAKIT);
            module.SetConstant("PF_DLI", IronRuby.StandardLibrary.Sockets.RubySocket.PF_DLI);
            module.SetConstant("PF_ECMA", IronRuby.StandardLibrary.Sockets.RubySocket.PF_ECMA);
            module.SetConstant("PF_HYLINK", IronRuby.StandardLibrary.Sockets.RubySocket.PF_HYLINK);
            module.SetConstant("PF_IMPLINK", IronRuby.StandardLibrary.Sockets.RubySocket.PF_IMPLINK);
            module.SetConstant("PF_INET", IronRuby.StandardLibrary.Sockets.RubySocket.PF_INET);
            module.SetConstant("PF_IPX", IronRuby.StandardLibrary.Sockets.RubySocket.PF_IPX);
            module.SetConstant("PF_ISO", IronRuby.StandardLibrary.Sockets.RubySocket.PF_ISO);
            module.SetConstant("PF_LAT", IronRuby.StandardLibrary.Sockets.RubySocket.PF_LAT);
            module.SetConstant("PF_MAX", IronRuby.StandardLibrary.Sockets.RubySocket.PF_MAX);
            module.SetConstant("PF_NS", IronRuby.StandardLibrary.Sockets.RubySocket.PF_NS);
            module.SetConstant("PF_OSI", IronRuby.StandardLibrary.Sockets.RubySocket.PF_OSI);
            module.SetConstant("PF_PUP", IronRuby.StandardLibrary.Sockets.RubySocket.PF_PUP);
            module.SetConstant("PF_SNA", IronRuby.StandardLibrary.Sockets.RubySocket.PF_SNA);
            module.SetConstant("PF_UNIX", IronRuby.StandardLibrary.Sockets.RubySocket.PF_UNIX);
            module.SetConstant("PF_UNSPEC", IronRuby.StandardLibrary.Sockets.RubySocket.PF_UNSPEC);
            module.SetConstant("SHUT_RD", IronRuby.StandardLibrary.Sockets.RubySocket.SHUT_RD);
            module.SetConstant("SHUT_RDWR", IronRuby.StandardLibrary.Sockets.RubySocket.SHUT_RDWR);
            module.SetConstant("SHUT_WR", IronRuby.StandardLibrary.Sockets.RubySocket.SHUT_WR);
            module.SetConstant("SO_ACCEPTCONN", IronRuby.StandardLibrary.Sockets.RubySocket.SO_ACCEPTCONN);
            module.SetConstant("SO_BROADCAST", IronRuby.StandardLibrary.Sockets.RubySocket.SO_BROADCAST);
            module.SetConstant("SO_DEBUG", IronRuby.StandardLibrary.Sockets.RubySocket.SO_DEBUG);
            module.SetConstant("SO_DONTROUTE", IronRuby.StandardLibrary.Sockets.RubySocket.SO_DONTROUTE);
            module.SetConstant("SO_ERROR", IronRuby.StandardLibrary.Sockets.RubySocket.SO_ERROR);
            module.SetConstant("SO_KEEPALIVE", IronRuby.StandardLibrary.Sockets.RubySocket.SO_KEEPALIVE);
            module.SetConstant("SO_LINGER", IronRuby.StandardLibrary.Sockets.RubySocket.SO_LINGER);
            module.SetConstant("SO_OOBINLINE", IronRuby.StandardLibrary.Sockets.RubySocket.SO_OOBINLINE);
            module.SetConstant("SO_RCVBUF", IronRuby.StandardLibrary.Sockets.RubySocket.SO_RCVBUF);
            module.SetConstant("SO_RCVLOWAT", IronRuby.StandardLibrary.Sockets.RubySocket.SO_RCVLOWAT);
            module.SetConstant("SO_RCVTIMEO", IronRuby.StandardLibrary.Sockets.RubySocket.SO_RCVTIMEO);
            module.SetConstant("SO_REUSEADDR", IronRuby.StandardLibrary.Sockets.RubySocket.SO_REUSEADDR);
            module.SetConstant("SO_SNDBUF", IronRuby.StandardLibrary.Sockets.RubySocket.SO_SNDBUF);
            module.SetConstant("SO_SNDLOWAT", IronRuby.StandardLibrary.Sockets.RubySocket.SO_SNDLOWAT);
            module.SetConstant("SO_SNDTIMEO", IronRuby.StandardLibrary.Sockets.RubySocket.SO_SNDTIMEO);
            module.SetConstant("SO_TYPE", IronRuby.StandardLibrary.Sockets.RubySocket.SO_TYPE);
            module.SetConstant("SO_USELOOPBACK", IronRuby.StandardLibrary.Sockets.RubySocket.SO_USELOOPBACK);
            module.SetConstant("SOCK_DGRAM", IronRuby.StandardLibrary.Sockets.RubySocket.SOCK_DGRAM);
            module.SetConstant("SOCK_RAW", IronRuby.StandardLibrary.Sockets.RubySocket.SOCK_RAW);
            module.SetConstant("SOCK_RDM", IronRuby.StandardLibrary.Sockets.RubySocket.SOCK_RDM);
            module.SetConstant("SOCK_SEQPACKET", IronRuby.StandardLibrary.Sockets.RubySocket.SOCK_SEQPACKET);
            module.SetConstant("SOCK_STREAM", IronRuby.StandardLibrary.Sockets.RubySocket.SOCK_STREAM);
            module.SetConstant("SOL_SOCKET", IronRuby.StandardLibrary.Sockets.RubySocket.SOL_SOCKET);
            module.SetConstant("TCP_NODELAY", IronRuby.StandardLibrary.Sockets.RubySocket.TCP_NODELAY);
            
            module.DefineLibraryMethod("accept", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubySocket, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.Accept),
            });
            
            module.DefineLibraryMethod("accept_nonblock", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubySocket, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.AcceptNonBlocking),
            });
            
            module.DefineLibraryMethod("bind", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubySocket, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.StandardLibrary.Sockets.RubySocket.Bind),
            });
            
            module.DefineLibraryMethod("connect", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubySocket, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.StandardLibrary.Sockets.RubySocket.Connect),
            });
            
            module.DefineLibraryMethod("connect_nonblock", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubySocket, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.StandardLibrary.Sockets.RubySocket.ConnectNonBlocking),
            });
            
            module.DefineLibraryMethod("listen", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubySocket, System.Int32, System.Int32>(IronRuby.StandardLibrary.Sockets.RubySocket.Listen),
            });
            
            module.DefineLibraryMethod("recvfrom", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubySocket, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.ReceiveFrom),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubySocket, System.Int32, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.ReceiveFrom),
            });
            
            module.DefineLibraryMethod("sysaccept", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubySocket, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.SysAccept),
            });
            
        }
        #endif
        
        #if !SILVERLIGHT
        private void LoadSocket_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("getaddrinfo", 0x21, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.GetAddressInfo),
            });
            
            module.DefineLibraryMethod("gethostbyaddr", 0x21, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.GetHostByAddress),
            });
            
            module.DefineLibraryMethod("gethostbyname", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.GetHostByName),
            });
            
            module.DefineLibraryMethod("gethostname", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.RubySocket.GetHostname),
            });
            
            module.DefineLibraryMethod("getnameinfo", 0x21, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyArray, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.GetNameInfo),
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.GetNameInfo),
            });
            
            module.DefineLibraryMethod("getservbyname", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.StandardLibrary.Sockets.RubySocket.GetServiceByName),
            });
            
            module.DefineLibraryMethod("pack_sockaddr_in", 0x21, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, System.Object, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.RubySocket.PackInetSockAddr),
            });
            
            module.DefineLibraryMethod("pair", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.Object, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.CreateSocketPair),
            });
            
            module.DefineLibraryMethod("sockaddr_in", 0x21, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, System.Object, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.RubySocket.PackInetSockAddr),
            });
            
            module.DefineLibraryMethod("socketpair", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.Object, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.CreateSocketPair),
            });
            
            module.DefineLibraryMethod("unpack_sockaddr_in", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.UnPackInetSockAddr),
            });
            
        }
        #endif
        
        #if !SILVERLIGHT && !SILVERLIGHT
        private void LoadSocket__Constants_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("AF_APPLETALK", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_APPLETALK);
            module.SetConstant("AF_ATM", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_ATM);
            module.SetConstant("AF_CCITT", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_CCITT);
            module.SetConstant("AF_CHAOS", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_CHAOS);
            module.SetConstant("AF_DATAKIT", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_DATAKIT);
            module.SetConstant("AF_DLI", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_DLI);
            module.SetConstant("AF_ECMA", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_ECMA);
            module.SetConstant("AF_HYLINK", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_HYLINK);
            module.SetConstant("AF_IMPLINK", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_IMPLINK);
            module.SetConstant("AF_INET", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_INET);
            module.SetConstant("AF_IPX", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_IPX);
            module.SetConstant("AF_ISO", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_ISO);
            module.SetConstant("AF_LAT", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_LAT);
            module.SetConstant("AF_MAX", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_MAX);
            module.SetConstant("AF_NETBIOS", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_NETBIOS);
            module.SetConstant("AF_NS", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_NS);
            module.SetConstant("AF_OSI", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_OSI);
            module.SetConstant("AF_PUP", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_PUP);
            module.SetConstant("AF_SNA", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_SNA);
            module.SetConstant("AF_UNIX", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_UNIX);
            module.SetConstant("AF_UNSPEC", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_UNSPEC);
            module.SetConstant("AI_ADDRCONFIG", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AI_ADDRCONFIG);
            module.SetConstant("AI_ALL", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AI_ALL);
            module.SetConstant("AI_CANONNAME", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AI_CANONNAME);
            module.SetConstant("AI_DEFAULT", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AI_DEFAULT);
            module.SetConstant("AI_MASK", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AI_MASK);
            module.SetConstant("AI_NUMERICHOST", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AI_NUMERICHOST);
            module.SetConstant("AI_PASSIVE", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AI_PASSIVE);
            module.SetConstant("AI_V4MAPPED", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AI_V4MAPPED);
            module.SetConstant("AI_V4MAPPED_CFG", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AI_V4MAPPED_CFG);
            module.SetConstant("EAI_ADDRFAMILY", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.EAI_ADDRFAMILY);
            module.SetConstant("EAI_AGAIN", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.EAI_AGAIN);
            module.SetConstant("EAI_BADFLAGS", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.EAI_BADFLAGS);
            module.SetConstant("EAI_BADHINTS", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.EAI_BADHINTS);
            module.SetConstant("EAI_FAIL", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.EAI_FAIL);
            module.SetConstant("EAI_FAMILY", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.EAI_FAMILY);
            module.SetConstant("EAI_MAX", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.EAI_MAX);
            module.SetConstant("EAI_MEMORY", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.EAI_MEMORY);
            module.SetConstant("EAI_NODATA", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.EAI_NODATA);
            module.SetConstant("EAI_NONAME", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.EAI_NONAME);
            module.SetConstant("EAI_PROTOCOL", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.EAI_PROTOCOL);
            module.SetConstant("EAI_SERVICE", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.EAI_SERVICE);
            module.SetConstant("EAI_SOCKTYPE", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.EAI_SOCKTYPE);
            module.SetConstant("EAI_SYSTEM", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.EAI_SYSTEM);
            module.SetConstant("INADDR_ALLHOSTS_GROUP", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.INADDR_ALLHOSTS_GROUP);
            module.SetConstant("INADDR_ANY", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.INADDR_ANY);
            module.SetConstant("INADDR_BROADCAST", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.INADDR_BROADCAST);
            module.SetConstant("INADDR_LOOPBACK", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.INADDR_LOOPBACK);
            module.SetConstant("INADDR_MAX_LOCAL_GROUP", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.INADDR_MAX_LOCAL_GROUP);
            module.SetConstant("INADDR_NONE", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.INADDR_NONE);
            module.SetConstant("INADDR_UNSPEC_GROUP", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.INADDR_UNSPEC_GROUP);
            module.SetConstant("IPPORT_RESERVED", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.IPPORT_RESERVED);
            module.SetConstant("IPPORT_USERRESERVED", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.IPPORT_USERRESERVED);
            module.SetConstant("IPPROTO_GGP", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.IPPROTO_GGP);
            module.SetConstant("IPPROTO_ICMP", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.IPPROTO_ICMP);
            module.SetConstant("IPPROTO_IDP", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.IPPROTO_IDP);
            module.SetConstant("IPPROTO_IGMP", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.IPPROTO_IGMP);
            module.SetConstant("IPPROTO_IP", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.IPPROTO_IP);
            module.SetConstant("IPPROTO_MAX", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.IPPROTO_MAX);
            module.SetConstant("IPPROTO_ND", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.IPPROTO_ND);
            module.SetConstant("IPPROTO_PUP", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.IPPROTO_PUP);
            module.SetConstant("IPPROTO_RAW", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.IPPROTO_RAW);
            module.SetConstant("IPPROTO_TCP", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.IPPROTO_TCP);
            module.SetConstant("IPPROTO_UDP", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.IPPROTO_UDP);
            module.SetConstant("MSG_DONTROUTE", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.MSG_DONTROUTE);
            module.SetConstant("MSG_OOB", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.MSG_OOB);
            module.SetConstant("MSG_PEEK", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.MSG_PEEK);
            module.SetConstant("NI_DGRAM", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.NI_DGRAM);
            module.SetConstant("NI_MAXHOST", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.NI_MAXHOST);
            module.SetConstant("NI_MAXSERV", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.NI_MAXSERV);
            module.SetConstant("NI_NAMEREQD", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.NI_NAMEREQD);
            module.SetConstant("NI_NOFQDN", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.NI_NOFQDN);
            module.SetConstant("NI_NUMERICHOST", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.NI_NUMERICHOST);
            module.SetConstant("NI_NUMERICSERV", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.NI_NUMERICSERV);
            module.SetConstant("PF_APPLETALK", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_APPLETALK);
            module.SetConstant("PF_ATM", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_ATM);
            module.SetConstant("PF_CCITT", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_CCITT);
            module.SetConstant("PF_CHAOS", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_CHAOS);
            module.SetConstant("PF_DATAKIT", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_DATAKIT);
            module.SetConstant("PF_DLI", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_DLI);
            module.SetConstant("PF_ECMA", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_ECMA);
            module.SetConstant("PF_HYLINK", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_HYLINK);
            module.SetConstant("PF_IMPLINK", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_IMPLINK);
            module.SetConstant("PF_INET", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_INET);
            module.SetConstant("PF_IPX", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_IPX);
            module.SetConstant("PF_ISO", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_ISO);
            module.SetConstant("PF_LAT", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_LAT);
            module.SetConstant("PF_MAX", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_MAX);
            module.SetConstant("PF_NS", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_NS);
            module.SetConstant("PF_OSI", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_OSI);
            module.SetConstant("PF_PUP", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_PUP);
            module.SetConstant("PF_SNA", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_SNA);
            module.SetConstant("PF_UNIX", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_UNIX);
            module.SetConstant("PF_UNSPEC", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.PF_UNSPEC);
            module.SetConstant("SHUT_RD", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SHUT_RD);
            module.SetConstant("SHUT_RDWR", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SHUT_RDWR);
            module.SetConstant("SHUT_WR", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SHUT_WR);
            module.SetConstant("SO_ACCEPTCONN", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SO_ACCEPTCONN);
            module.SetConstant("SO_BROADCAST", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SO_BROADCAST);
            module.SetConstant("SO_DEBUG", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SO_DEBUG);
            module.SetConstant("SO_DONTROUTE", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SO_DONTROUTE);
            module.SetConstant("SO_ERROR", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SO_ERROR);
            module.SetConstant("SO_KEEPALIVE", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SO_KEEPALIVE);
            module.SetConstant("SO_LINGER", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SO_LINGER);
            module.SetConstant("SO_OOBINLINE", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SO_OOBINLINE);
            module.SetConstant("SO_RCVBUF", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SO_RCVBUF);
            module.SetConstant("SO_RCVLOWAT", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SO_RCVLOWAT);
            module.SetConstant("SO_RCVTIMEO", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SO_RCVTIMEO);
            module.SetConstant("SO_REUSEADDR", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SO_REUSEADDR);
            module.SetConstant("SO_SNDBUF", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SO_SNDBUF);
            module.SetConstant("SO_SNDLOWAT", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SO_SNDLOWAT);
            module.SetConstant("SO_SNDTIMEO", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SO_SNDTIMEO);
            module.SetConstant("SO_TYPE", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SO_TYPE);
            module.SetConstant("SO_USELOOPBACK", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SO_USELOOPBACK);
            module.SetConstant("SOCK_DGRAM", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SOCK_DGRAM);
            module.SetConstant("SOCK_RAW", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SOCK_RAW);
            module.SetConstant("SOCK_RDM", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SOCK_RDM);
            module.SetConstant("SOCK_SEQPACKET", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SOCK_SEQPACKET);
            module.SetConstant("SOCK_STREAM", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SOCK_STREAM);
            module.SetConstant("SOL_SOCKET", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.SOL_SOCKET);
            module.SetConstant("TCP_NODELAY", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.TCP_NODELAY);
            
        }
        #endif
        
        #if !SILVERLIGHT
        private void LoadSocketError_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.HideMethod("message");
        }
        #endif
        
        #if !SILVERLIGHT
        private void LoadTCPServer_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("accept", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.TCPServer, IronRuby.StandardLibrary.Sockets.TCPSocket>(IronRuby.StandardLibrary.Sockets.TCPServer.Accept),
            });
            
            module.DefineLibraryMethod("accept_nonblock", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.TCPServer, IronRuby.StandardLibrary.Sockets.TCPSocket>(IronRuby.StandardLibrary.Sockets.TCPServer.AcceptNonBlocking),
            });
            
            module.DefineLibraryMethod("listen", 0x11, new System.Delegate[] {
                new Action<IronRuby.StandardLibrary.Sockets.TCPServer, System.Int32>(IronRuby.StandardLibrary.Sockets.TCPServer.Listen),
            });
            
            module.DefineLibraryMethod("sysaccept", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.TCPServer, System.Int32>(IronRuby.StandardLibrary.Sockets.TCPServer.SysAccept),
            });
            
        }
        #endif
        
        #if !SILVERLIGHT
        private void LoadTCPSocket_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("gethostbyname", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.TCPSocket.GetHostByName),
            });
            
        }
        #endif
        
        #if !SILVERLIGHT
        private void LoadUDPSocket_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("bind", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.UDPSocket, System.Object, System.Object, System.Int32>(IronRuby.StandardLibrary.Sockets.UDPSocket.Bind),
            });
            
            module.DefineLibraryMethod("connect", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.UDPSocket, System.Object, System.Object, System.Int32>(IronRuby.StandardLibrary.Sockets.UDPSocket.Connect),
            });
            
            module.DefineLibraryMethod("recvfrom_nonblock", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.IPSocket, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.UDPSocket.ReceiveFromNonBlocking),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.IPSocket, System.Int32, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.UDPSocket.ReceiveFromNonBlocking),
            });
            
            module.DefineLibraryMethod("send", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, IronRuby.Builtins.MutableString, System.Object, System.Object, System.Object, System.Int32>(IronRuby.StandardLibrary.Sockets.UDPSocket.Send),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, IronRuby.Builtins.MutableString, System.Object, System.Int32>(IronRuby.StandardLibrary.Sockets.UDPSocket.Send),
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, IronRuby.Builtins.MutableString, System.Object, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.StandardLibrary.Sockets.UDPSocket.Send),
            });
            
        }
        #endif
        
    }
}

namespace IronRuby.StandardLibrary.OpenSsl {
    public sealed class OpenSslLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(System.Object));
            
            
            IronRuby.Builtins.RubyModule def1 = DefineGlobalModule("OpenSSL", typeof(IronRuby.StandardLibrary.OpenSsl.OpenSsl), true, new Action<IronRuby.Builtins.RubyModule>(LoadOpenSSL_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def2 = DefineClass("OpenSSL::BN", typeof(IronRuby.StandardLibrary.OpenSsl.OpenSsl.BN), true, classRef0, null, new Action<IronRuby.Builtins.RubyModule>(LoadOpenSSL__BN_Class), IronRuby.Builtins.RubyModule.EmptyArray, null);
            IronRuby.Builtins.RubyModule def3 = DefineModule("OpenSSL::Digest", typeof(IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory), true, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def4 = DefineClass("OpenSSL::Digest::Digest", typeof(IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest), true, classRef0, new Action<IronRuby.Builtins.RubyModule>(LoadOpenSSL__Digest__Digest_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest.CreateDigest),
            });
            IronRuby.Builtins.RubyClass def5 = DefineClass("OpenSSL::HMAC", typeof(IronRuby.StandardLibrary.OpenSsl.OpenSsl.HMAC), true, classRef0, null, new Action<IronRuby.Builtins.RubyModule>(LoadOpenSSL__HMAC_Class), IronRuby.Builtins.RubyModule.EmptyArray, null);
            IronRuby.Builtins.RubyModule def6 = DefineModule("OpenSSL::Random", typeof(IronRuby.StandardLibrary.OpenSsl.OpenSsl.RandomModule), true, null, new Action<IronRuby.Builtins.RubyModule>(LoadOpenSSL__Random_Class), IronRuby.Builtins.RubyModule.EmptyArray);
            def1.SetConstant("BN", def2);
            def1.SetConstant("Digest", def3);
            def3.SetConstant("Digest", def4);
            def1.SetConstant("HMAC", def5);
            def1.SetConstant("Random", def6);
        }
        
        private void LoadOpenSSL_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("OPENSSL_VERSION", IronRuby.StandardLibrary.OpenSsl.OpenSsl.OPENSSL_VERSION);
            module.SetConstant("OPENSSL_VERSION_NUMBER", IronRuby.StandardLibrary.OpenSsl.OpenSsl.OPENSSL_VERSION_NUMBER);
            module.SetConstant("VERSION", IronRuby.StandardLibrary.OpenSsl.OpenSsl.VERSION);
            
        }
        
        private void LoadOpenSSL__BN_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("rand", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Int32, System.Boolean, Microsoft.Scripting.Math.BigInteger>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.BN.Rand),
            });
            
        }
        
        private void LoadOpenSSL__Digest__Digest_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("initialize", 0x12, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest.Initialize),
            });
            
        }
        
        private void LoadOpenSSL__HMAC_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("hexdigest", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.HMAC.HexDigest),
            });
            
        }
        
        private void LoadOpenSSL__Random_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("seed", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.RandomModule.Seed),
            });
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.Digest {
    public sealed class DigestLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(System.Object));
            
            
            IronRuby.Builtins.RubyModule def1 = DefineGlobalModule("Digest", typeof(IronRuby.StandardLibrary.Digest.Digest), true, null, new Action<IronRuby.Builtins.RubyModule>(LoadDigest_Class), IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def4 = DefineModule("Digest::Instance", typeof(IronRuby.StandardLibrary.Digest.Digest.Instance), true, new Action<IronRuby.Builtins.RubyModule>(LoadDigest__Instance_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def3 = DefineClass("Digest::Class", typeof(IronRuby.StandardLibrary.Digest.Digest.Class), true, classRef0, null, new Action<IronRuby.Builtins.RubyModule>(LoadDigest__Class_Class), new IronRuby.Builtins.RubyModule[] {def4, }, null);
            IronRuby.Builtins.RubyClass def2 = DefineClass("Digest::Base", typeof(IronRuby.StandardLibrary.Digest.Digest.Base), true, def3, new Action<IronRuby.Builtins.RubyModule>(LoadDigest__Base_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def5 = DefineClass("Digest::MD5", typeof(IronRuby.StandardLibrary.Digest.Digest.MD5), true, def2, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            #endif
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def6 = DefineClass("Digest::SHA1", typeof(IronRuby.StandardLibrary.Digest.Digest.SHA1), true, def2, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            #endif
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def7 = DefineClass("Digest::SHA256", typeof(IronRuby.StandardLibrary.Digest.Digest.SHA256), true, def2, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            #endif
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def8 = DefineClass("Digest::SHA384", typeof(IronRuby.StandardLibrary.Digest.Digest.SHA384), true, def2, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            #endif
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def9 = DefineClass("Digest::SHA512", typeof(IronRuby.StandardLibrary.Digest.Digest.SHA512), true, def2, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            #endif
            def1.SetConstant("Instance", def4);
            def1.SetConstant("Class", def3);
            def1.SetConstant("Base", def2);
            #if !SILVERLIGHT
            def1.SetConstant("MD5", def5);
            #endif
            #if !SILVERLIGHT
            def1.SetConstant("SHA1", def6);
            #endif
            #if !SILVERLIGHT
            def1.SetConstant("SHA256", def7);
            #endif
            #if !SILVERLIGHT
            def1.SetConstant("SHA384", def8);
            #endif
            #if !SILVERLIGHT
            def1.SetConstant("SHA512", def9);
            #endif
        }
        
        private void LoadDigest_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("const_missing", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Object>(IronRuby.StandardLibrary.Digest.Digest.ConstantMissing),
            });
            
            module.DefineLibraryMethod("hexencode", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.HexEncode),
            });
            
        }
        
        private void LoadDigest__Base_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("<<", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Digest.Digest.Base, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Digest.Digest.Base>(IronRuby.StandardLibrary.Digest.Digest.Base.Update),
            });
            
            module.DefineLibraryMethod("finish", 0x12, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Digest.Digest.Base, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Base.Finish),
            });
            
            module.DefineLibraryMethod("reset", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Digest.Digest.Base, IronRuby.StandardLibrary.Digest.Digest.Base>(IronRuby.StandardLibrary.Digest.Digest.Base.Reset),
            });
            
            module.DefineLibraryMethod("update", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Digest.Digest.Base, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Digest.Digest.Base>(IronRuby.StandardLibrary.Digest.Digest.Base.Update),
            });
            
        }
        
        private void LoadDigest__Class_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("digest", 0x21, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>>, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Class.Digest),
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Class.Digest),
            });
            
            module.DefineLibraryMethod("hexdigest", 0x21, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>>, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Class.HexDigest),
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Class.HexDigest),
            });
            
        }
        
        private void LoadDigest__Instance_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("digest", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, System.Object>>, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Instance.Digest),
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, System.Object>>, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Instance.Digest),
            });
            
            module.DefineLibraryMethod("digest!", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, System.Object>>, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Instance.DigestNew),
            });
            
            module.DefineLibraryMethod("hexdigest", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, System.Object>>, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Instance.HexDigest),
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, System.Object>>, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Instance.HexDigest),
            });
            
            module.DefineLibraryMethod("hexdigest!", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, System.Object>>, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Instance.HexDigestNew),
            });
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.Zlib {
    public sealed class ZlibLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(IronRuby.Builtins.RuntimeError));
            IronRuby.Builtins.RubyClass classRef1 = GetClass(typeof(System.Object));
            
            
            IronRuby.Builtins.RubyModule def1 = DefineGlobalModule("Zlib", typeof(IronRuby.StandardLibrary.Zlib.Zlib), true, new Action<IronRuby.Builtins.RubyModule>(LoadZlib_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def2 = DefineClass("Zlib::DataError", typeof(IronRuby.StandardLibrary.Zlib.Zlib.DataError), true, classRef0, null, null, IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] { new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(ZlibLibraryInitializer.ExceptionFactory__Zlib__DataError) });
            IronRuby.Builtins.RubyClass def3 = DefineClass("Zlib::GzipFile", typeof(IronRuby.StandardLibrary.Zlib.Zlib.GZipFile), true, classRef1, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            IronRuby.Builtins.RubyClass def4 = DefineClass("Zlib::GzipFile::Error", typeof(IronRuby.StandardLibrary.Zlib.Zlib.GZipFile.Error), true, classRef0, null, null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            IronRuby.Builtins.RubyClass def7 = DefineClass("Zlib::ZStream", typeof(IronRuby.StandardLibrary.Zlib.Zlib.ZStream), true, classRef1, new Action<IronRuby.Builtins.RubyModule>(LoadZlib__ZStream_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray, null);
            IronRuby.Builtins.RubyClass def5 = DefineClass("Zlib::GzipReader", typeof(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader), true, def3, new Action<IronRuby.Builtins.RubyModule>(LoadZlib__GzipReader_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadZlib__GzipReader_Class), IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyIO, IronRuby.StandardLibrary.Zlib.Zlib.GZipReader>(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.Create),
                new Func<IronRuby.Runtime.RespondToStorage, IronRuby.Builtins.RubyClass, System.Object, IronRuby.StandardLibrary.Zlib.Zlib.GZipReader>(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.Create),
            });
            IronRuby.Builtins.RubyClass def6 = DefineClass("Zlib::Inflate", typeof(IronRuby.StandardLibrary.Zlib.Zlib.Inflate), true, def7, new Action<IronRuby.Builtins.RubyModule>(LoadZlib__Inflate_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadZlib__Inflate_Class), IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.StandardLibrary.Zlib.Zlib.Inflate>(IronRuby.StandardLibrary.Zlib.Zlib.Inflate.Create),
                new Func<IronRuby.Builtins.RubyClass, System.Int32, IronRuby.StandardLibrary.Zlib.Zlib.Inflate>(IronRuby.StandardLibrary.Zlib.Zlib.Inflate.Create),
            });
            def1.SetConstant("DataError", def2);
            def1.SetConstant("GzipFile", def3);
            def3.SetConstant("Error", def4);
            def1.SetConstant("ZStream", def7);
            def1.SetConstant("GzipReader", def5);
            def1.SetConstant("Inflate", def6);
        }
        
        private void LoadZlib_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("FIXLCODES", IronRuby.StandardLibrary.Zlib.Zlib.FIXLCODES);
            module.SetConstant("MAX_WBITS", IronRuby.StandardLibrary.Zlib.Zlib.MAX_WBITS);
            module.SetConstant("MAXBITS", IronRuby.StandardLibrary.Zlib.Zlib.MAXBITS);
            module.SetConstant("MAXCODES", IronRuby.StandardLibrary.Zlib.Zlib.MAXCODES);
            module.SetConstant("MAXDCODES", IronRuby.StandardLibrary.Zlib.Zlib.MAXDCODES);
            module.SetConstant("MAXLCODES", IronRuby.StandardLibrary.Zlib.Zlib.MAXLCODES);
            module.SetConstant("VERSION", IronRuby.StandardLibrary.Zlib.Zlib.VERSION);
            module.SetConstant("Z_DEFLATED", IronRuby.StandardLibrary.Zlib.Zlib.Z_DEFLATED);
            module.SetConstant("ZLIB_VERSION", IronRuby.StandardLibrary.Zlib.Zlib.ZLIB_VERSION);
            
        }
        
        private void LoadZlib__GzipReader_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("OSES", IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.OSES);
            
            module.DefineLibraryMethod("close", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.GZipReader, IronRuby.StandardLibrary.Zlib.Zlib.GZipReader>(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.Close),
            });
            
            module.DefineLibraryMethod("comment", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.GZipReader, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.Comment),
            });
            
            module.DefineLibraryMethod("open", 0x12, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.GZipReader, IronRuby.StandardLibrary.Zlib.Zlib.GZipReader>(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.Open),
            });
            
            module.DefineLibraryMethod("original_name", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.GZipReader, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.OriginalName),
            });
            
            module.DefineLibraryMethod("read", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.GZipReader, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.Read),
            });
            
            module.DefineLibraryMethod("xtra_field", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.GZipReader, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.ExtraField),
            });
            
        }
        
        private void LoadZlib__GzipReader_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("open", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Zlib.Zlib.GZipReader>(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.Open),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object>(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.Open),
            });
            
        }
        
        private void LoadZlib__Inflate_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("close", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.Inflate, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.Inflate.Close),
            });
            
            module.DefineLibraryMethod("inflate", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.Inflate, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.Inflate.InflateStream),
            });
            
        }
        
        private void LoadZlib__Inflate_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("inflate", 0x21, new System.Delegate[] {
                new Func<IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<Microsoft.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>>, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.Inflate.InflateStream),
            });
            
        }
        
        private void LoadZlib__ZStream_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("adler", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Int32>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.Adler),
            });
            
            module.DefineLibraryMethod("avail_in", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Int32>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.AvailIn),
            });
            
            module.DefineLibraryMethod("avail_out", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Int32>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.GetAvailOut),
            });
            
            module.DefineLibraryMethod("avail_out=", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Int32, System.Int32>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.SetAvailOut),
            });
            
            module.DefineLibraryMethod("close", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Boolean>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.Close),
            });
            
            module.DefineLibraryMethod("closed?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Boolean>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.IsClosed),
            });
            
            module.DefineLibraryMethod("data_type", 0x11, new System.Delegate[] {
                new Action<IronRuby.StandardLibrary.Zlib.Zlib.ZStream>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.DataType),
            });
            
            module.DefineLibraryMethod("finish", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Boolean>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.Close),
            });
            
            module.DefineLibraryMethod("finished?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Boolean>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.IsClosed),
            });
            
            module.DefineLibraryMethod("flush_next_in", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Collections.Generic.List<System.Byte>>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.FlushNextIn),
            });
            
            module.DefineLibraryMethod("flush_next_out", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Collections.Generic.List<System.Byte>>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.FlushNextOut),
            });
            
            module.DefineLibraryMethod("reset", 0x11, new System.Delegate[] {
                new Action<IronRuby.StandardLibrary.Zlib.Zlib.ZStream>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.Reset),
            });
            
            module.DefineLibraryMethod("stream_end?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Boolean>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.IsClosed),
            });
            
            module.DefineLibraryMethod("total_in", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Int32>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.TotalIn),
            });
            
            module.DefineLibraryMethod("total_out", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Int32>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.TotalOut),
            });
            
        }
        
        public static System.Exception/*!*/ ExceptionFactory__Zlib__DataError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.StandardLibrary.Zlib.Zlib.DataError(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
    }
}

namespace IronRuby.StandardLibrary.StringIO {
    public sealed class StringIOLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(IronRuby.Builtins.RubyIO));
            
            
            DefineGlobalClass("StringIO", typeof(IronRuby.StandardLibrary.StringIO.StringIO), true, classRef0, new Action<IronRuby.Builtins.RubyModule>(LoadStringIO_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadStringIO_Class), IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyIO>(IronRuby.StandardLibrary.StringIO.StringIO.CreateIO),
            });
        }
        
        private void LoadStringIO_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("length", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.GetLength),
            });
            
            module.DefineLibraryMethod("path", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Object>(IronRuby.StandardLibrary.StringIO.StringIO.GetPath),
            });
            
            module.DefineLibraryMethod("size", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.GetLength),
            });
            
            module.DefineLibraryMethod("string", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringIO.StringIO.GetString),
            });
            
            module.DefineLibraryMethod("string=", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringIO.StringIO.SetString),
            });
            
            module.DefineLibraryMethod("truncate", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Int32, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.SetLength),
            });
            
        }
        
        private void LoadStringIO_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("open", 0x21, new System.Delegate[] {
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.StandardLibrary.StringIO.StringIO.OpenIO),
            });
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.StringScanner {
    public sealed class StringScannerLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(IronRuby.Builtins.RubyObject));
            
            
            DefineGlobalClass("StringScanner", typeof(IronRuby.StandardLibrary.StringScanner.StringScanner), true, classRef0, new Action<IronRuby.Builtins.RubyModule>(LoadStringScanner_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadStringScanner_Class), IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.StringScanner.StringScanner>(IronRuby.StandardLibrary.StringScanner.StringScanner.Create),
            });
        }
        
        private void LoadStringScanner_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("[]", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.GetMatchSubgroup),
            });
            
            module.DefineLibraryMethod("<<", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.StringScanner.StringScanner>(IronRuby.StandardLibrary.StringScanner.StringScanner.Concat),
            });
            
            module.DefineLibraryMethod("beginning_of_line?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Boolean>(IronRuby.StandardLibrary.StringScanner.StringScanner.BeginningOfLine),
            });
            
            module.DefineLibraryMethod("bol?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Boolean>(IronRuby.StandardLibrary.StringScanner.StringScanner.BeginningOfLine),
            });
            
            module.DefineLibraryMethod("check", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.Check),
            });
            
            module.DefineLibraryMethod("check_until", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.CheckUntil),
            });
            
            module.DefineLibraryMethod("clear", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.StandardLibrary.StringScanner.StringScanner>(IronRuby.StandardLibrary.StringScanner.StringScanner.Clear),
            });
            
            module.DefineLibraryMethod("concat", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.StringScanner.StringScanner>(IronRuby.StandardLibrary.StringScanner.StringScanner.Concat),
            });
            
            module.DefineLibraryMethod("empty?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Boolean>(IronRuby.StandardLibrary.StringScanner.StringScanner.EndOfLine),
            });
            
            module.DefineLibraryMethod("eos?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Boolean>(IronRuby.StandardLibrary.StringScanner.StringScanner.EndOfLine),
            });
            
            module.DefineLibraryMethod("exist?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, System.Nullable<System.Int32>>(IronRuby.StandardLibrary.StringScanner.StringScanner.Exist),
            });
            
            module.DefineLibraryMethod("get_byte", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.GetByte),
            });
            
            module.DefineLibraryMethod("getbyte", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.GetByte),
            });
            
            module.DefineLibraryMethod("getch", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.GetChar),
            });
            
            module.DefineLibraryMethod("initialize", 0x12, new System.Delegate[] {
                new Action<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.Reinitialize),
            });
            
            module.DefineLibraryMethod("initialize_copy", 0x12, new System.Delegate[] {
                new Action<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.StandardLibrary.StringScanner.StringScanner>(IronRuby.StandardLibrary.StringScanner.StringScanner.InitializeFrom),
            });
            
            module.DefineLibraryMethod("inspect", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.ToString),
            });
            
            module.DefineLibraryMethod("match?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, System.Nullable<System.Int32>>(IronRuby.StandardLibrary.StringScanner.StringScanner.Match),
            });
            
            module.DefineLibraryMethod("matched", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.Matched),
            });
            
            module.DefineLibraryMethod("matched?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Boolean>(IronRuby.StandardLibrary.StringScanner.StringScanner.WasMatched),
            });
            
            module.DefineLibraryMethod("matched_size", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Nullable<System.Int32>>(IronRuby.StandardLibrary.StringScanner.StringScanner.MatchedSize),
            });
            
            module.DefineLibraryMethod("matchedsize", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Nullable<System.Int32>>(IronRuby.StandardLibrary.StringScanner.StringScanner.MatchedSize),
            });
            
            module.DefineLibraryMethod("peek", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.Peek),
            });
            
            module.DefineLibraryMethod("peep", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.Peek),
            });
            
            module.DefineLibraryMethod("pointer", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Int32>(IronRuby.StandardLibrary.StringScanner.StringScanner.GetCurrentPosition),
            });
            
            module.DefineLibraryMethod("pointer=", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Int32, System.Int32>(IronRuby.StandardLibrary.StringScanner.StringScanner.SetCurrentPosition),
            });
            
            module.DefineLibraryMethod("pos", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Int32>(IronRuby.StandardLibrary.StringScanner.StringScanner.GetCurrentPosition),
            });
            
            module.DefineLibraryMethod("pos=", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Int32, System.Int32>(IronRuby.StandardLibrary.StringScanner.StringScanner.SetCurrentPosition),
            });
            
            module.DefineLibraryMethod("post_match", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.PostMatch),
            });
            
            module.DefineLibraryMethod("pre_match", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.PreMatch),
            });
            
            module.DefineLibraryMethod("reset", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.StandardLibrary.StringScanner.StringScanner>(IronRuby.StandardLibrary.StringScanner.StringScanner.Reset),
            });
            
            module.DefineLibraryMethod("rest", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.Rest),
            });
            
            module.DefineLibraryMethod("rest?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Boolean>(IronRuby.StandardLibrary.StringScanner.StringScanner.IsRestLeft),
            });
            
            module.DefineLibraryMethod("rest_size", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Int32>(IronRuby.StandardLibrary.StringScanner.StringScanner.RestSize),
            });
            
            module.DefineLibraryMethod("restsize", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Int32>(IronRuby.StandardLibrary.StringScanner.StringScanner.RestSize),
            });
            
            module.DefineLibraryMethod("scan", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.Scan),
            });
            
            module.DefineLibraryMethod("scan_full", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, System.Boolean, System.Boolean, System.Object>(IronRuby.StandardLibrary.StringScanner.StringScanner.ScanFull),
            });
            
            module.DefineLibraryMethod("scan_until", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.ScanUntil),
            });
            
            module.DefineLibraryMethod("search_full", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, System.Boolean, System.Boolean, System.Object>(IronRuby.StandardLibrary.StringScanner.StringScanner.SearchFull),
            });
            
            module.DefineLibraryMethod("skip", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, System.Nullable<System.Int32>>(IronRuby.StandardLibrary.StringScanner.StringScanner.Skip),
            });
            
            module.DefineLibraryMethod("skip_until", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, System.Nullable<System.Int32>>(IronRuby.StandardLibrary.StringScanner.StringScanner.SkipUntil),
            });
            
            module.DefineLibraryMethod("string", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.GetString),
            });
            
            module.DefineLibraryMethod("string=", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.SetString),
            });
            
            module.DefineLibraryMethod("terminate", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.StandardLibrary.StringScanner.StringScanner>(IronRuby.StandardLibrary.StringScanner.StringScanner.Clear),
            });
            
            module.DefineLibraryMethod("to_s", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.ToString),
            });
            
            module.DefineLibraryMethod("unscan", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.StandardLibrary.StringScanner.StringScanner>(IronRuby.StandardLibrary.StringScanner.StringScanner.Unscan),
            });
            
        }
        
        private void LoadStringScanner_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("must_C_version", 0x21, new System.Delegate[] {
                new Func<System.Object, System.Object>(IronRuby.StandardLibrary.StringScanner.StringScanner.MustCVersion),
            });
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.Enumerator {
    public sealed class EnumeratorLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(System.Object));
            
            
            IronRuby.Builtins.RubyModule def1 = ExtendModule(typeof(IronRuby.Builtins.Enumerable), null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def2 = DefineClass("IronRuby::Builtins::Enumerable::Enumerator", typeof(IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator), true, classRef0, null, null, new IronRuby.Builtins.RubyModule[] {def1, }, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.Object, IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator>(IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator.CreateForEach),
                new Func<IronRuby.Builtins.RubyClass, System.Object, Microsoft.Scripting.SymbolId, IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator>(IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator.Create),
                new Func<IronRuby.Builtins.RubyClass, System.Object, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator>(IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator.Create),
            });
            def1.SetConstant("Enumerator", def2);
        }
        
    }
}

namespace IronRuby.StandardLibrary.FunctionControl {
    public sealed class FunctionControlLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.FileControl {
    public sealed class FileControlLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            
            
            DefineGlobalModule("Fcntl", typeof(IronRuby.StandardLibrary.FileControl.Fcntl), true, new Action<IronRuby.Builtins.RubyModule>(LoadFcntl_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray);
        }
        
        private void LoadFcntl_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("F_SETFL", IronRuby.StandardLibrary.FileControl.Fcntl.F_SETFL);
            module.SetConstant("O_ACCMODE", IronRuby.StandardLibrary.FileControl.Fcntl.O_ACCMODE);
            module.SetConstant("O_APPEND", IronRuby.StandardLibrary.FileControl.Fcntl.O_APPEND);
            module.SetConstant("O_CREAT", IronRuby.StandardLibrary.FileControl.Fcntl.O_CREAT);
            module.SetConstant("O_EXCL", IronRuby.StandardLibrary.FileControl.Fcntl.O_EXCL);
            module.SetConstant("O_NONBLOCK", IronRuby.StandardLibrary.FileControl.Fcntl.O_NONBLOCK);
            module.SetConstant("O_RDONLY", IronRuby.StandardLibrary.FileControl.Fcntl.O_RDONLY);
            module.SetConstant("O_RDWR", IronRuby.StandardLibrary.FileControl.Fcntl.O_RDWR);
            module.SetConstant("O_TRUNC", IronRuby.StandardLibrary.FileControl.Fcntl.O_TRUNC);
            module.SetConstant("O_WRONLY", IronRuby.StandardLibrary.FileControl.Fcntl.O_WRONLY);
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.BigDecimal {
    public sealed class BigDecimalLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(IronRuby.Builtins.Numeric));
            
            
            DefineGlobalClass("BigDecimal", typeof(IronRuby.StandardLibrary.BigDecimal.BigDecimal), false, classRef0, new Action<IronRuby.Builtins.RubyModule>(LoadBigDecimal_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadBigDecimal_Class), IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.CreateBigDecimal),
            });
            ExtendModule(typeof(IronRuby.Builtins.Kernel), new Action<IronRuby.Builtins.RubyModule>(LoadIronRuby__Builtins__Kernel_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadIronRuby__Builtins__Kernel_Class), IronRuby.Builtins.RubyModule.EmptyArray);
        }
        
        private void LoadBigDecimal_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("BASE", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.BASE);
            module.SetConstant("EXCEPTION_ALL", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.EXCEPTION_ALL);
            module.SetConstant("EXCEPTION_INFINITY", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.EXCEPTION_INFINITY);
            module.SetConstant("EXCEPTION_NaN", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.EXCEPTION_NaN);
            module.SetConstant("EXCEPTION_OVERFLOW", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.EXCEPTION_OVERFLOW);
            module.SetConstant("EXCEPTION_UNDERFLOW", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.EXCEPTION_UNDERFLOW);
            module.SetConstant("EXCEPTION_ZERODIVIDE", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.EXCEPTION_ZERODIVIDE);
            module.SetConstant("ROUND_CEILING", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ROUND_CEILING);
            module.SetConstant("ROUND_DOWN", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ROUND_DOWN);
            module.SetConstant("ROUND_FLOOR", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ROUND_FLOOR);
            module.SetConstant("ROUND_HALF_DOWN", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ROUND_HALF_DOWN);
            module.SetConstant("ROUND_HALF_EVEN", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ROUND_HALF_EVEN);
            module.SetConstant("ROUND_HALF_UP", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ROUND_HALF_UP);
            module.SetConstant("ROUND_MODE", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ROUND_MODE);
            module.SetConstant("ROUND_UP", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ROUND_UP);
            module.SetConstant("SIGN_NaN", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.SIGN_NaN);
            module.SetConstant("SIGN_NEGATIVE_FINITE", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.SIGN_NEGATIVE_FINITE);
            module.SetConstant("SIGN_NEGATIVE_INFINITE", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.SIGN_NEGATIVE_INFINITE);
            module.SetConstant("SIGN_NEGATIVE_ZERO", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.SIGN_NEGATIVE_ZERO);
            module.SetConstant("SIGN_POSITIVE_FINITE", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.SIGN_POSITIVE_FINITE);
            module.SetConstant("SIGN_POSITIVE_INFINITE", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.SIGN_POSITIVE_INFINITE);
            module.SetConstant("SIGN_POSITIVE_ZERO", IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.SIGN_POSITIVE_ZERO);
            
            module.DefineLibraryMethod("-", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract),
            });
            
            module.DefineLibraryMethod("%", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Modulo),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Modulo),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Modulo),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ModuloOp),
            });
            
            module.DefineLibraryMethod("*", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Multiply),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Multiply),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Multiply),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Multiply),
            });
            
            module.DefineLibraryMethod("**", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Power),
            });
            
            module.DefineLibraryMethod("/", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Divide),
            });
            
            module.DefineLibraryMethod("-@", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Negate),
            });
            
            module.DefineLibraryMethod("_dump", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Dump),
            });
            
            module.DefineLibraryMethod("+", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add),
            });
            
            module.DefineLibraryMethod("+@", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Identity),
            });
            
            module.DefineLibraryMethod("<", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThan),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThan),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThan),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThan),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThan),
            });
            
            module.DefineLibraryMethod("<=", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThanOrEqual),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThanOrEqual),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThanOrEqual),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThanOrEqual),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThanOrEqual),
            });
            
            module.DefineLibraryMethod("<=>", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Compare),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Compare),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Compare),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Compare),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Compare),
            });
            
            module.DefineLibraryMethod("==", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal),
            });
            
            module.DefineLibraryMethod("===", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal),
            });
            
            module.DefineLibraryMethod(">", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThan),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThan),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThan),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThan),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThan),
            });
            
            module.DefineLibraryMethod(">=", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThanOrEqual),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThanOrEqual),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThanOrEqual),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThanOrEqual),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThanOrEqual),
            });
            
            module.DefineLibraryMethod("abs", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Abs),
            });
            
            module.DefineLibraryMethod("add", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add),
            });
            
            module.DefineLibraryMethod("ceil", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Ceil),
            });
            
            module.DefineLibraryMethod("coerce", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Coerce),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Coerce),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Coerce),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Coerce),
            });
            
            module.DefineLibraryMethod("div", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Div),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Div),
            });
            
            module.DefineLibraryMethod("divmod", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.DivMod),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.DivMod),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.DivMod),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.DivMod),
            });
            
            module.DefineLibraryMethod("eql?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal),
            });
            
            module.DefineLibraryMethod("exponent", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Exponent),
            });
            
            module.DefineLibraryMethod("finite?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.IsFinite),
            });
            
            module.DefineLibraryMethod("fix", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Fix),
            });
            
            module.DefineLibraryMethod("floor", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Floor),
            });
            
            module.DefineLibraryMethod("frac", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Fraction),
            });
            
            module.DefineLibraryMethod("hash", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Hash),
            });
            
            module.DefineLibraryMethod("infinite?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.IsInfinite),
            });
            
            module.DefineLibraryMethod("inspect", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Inspect),
            });
            
            module.DefineLibraryMethod("modulo", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Modulo),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Modulo),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Modulo),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Modulo),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Modulo),
            });
            
            module.DefineLibraryMethod("mult", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Multiply),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Multiply),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Multiply),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Multiply),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Multiply),
            });
            
            module.DefineLibraryMethod("nan?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.IsNaN),
            });
            
            module.DefineLibraryMethod("nonzero?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.IsNonZero),
            });
            
            module.DefineLibraryMethod("power", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Power),
            });
            
            module.DefineLibraryMethod("precs", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Precision),
            });
            
            module.DefineLibraryMethod("quo", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Divide),
            });
            
            module.DefineLibraryMethod("remainder", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Remainder),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Remainder),
            });
            
            module.DefineLibraryMethod("round", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Round),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Round),
            });
            
            module.DefineLibraryMethod("sign", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Sign),
            });
            
            module.DefineLibraryMethod("split", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Split),
            });
            
            module.DefineLibraryMethod("sqrt", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.SquareRoot),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.SquareRoot),
            });
            
            module.DefineLibraryMethod("sub", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract),
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract),
            });
            
            module.DefineLibraryMethod("to_f", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ToFloat),
            });
            
            module.DefineLibraryMethod("to_i", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ToI),
            });
            
            module.DefineLibraryMethod("to_int", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ToI),
            });
            
            module.DefineLibraryMethod("to_s", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ToString),
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ToString),
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ToString),
            });
            
            module.DefineLibraryMethod("truncate", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Truncate),
            });
            
            module.DefineLibraryMethod("zero?", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.IsZero),
            });
            
        }
        
        private void LoadBigDecimal_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("_load", 0x21, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Load),
            });
            
            module.DefineLibraryMethod("double_fig", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, System.Int32>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.DoubleFig),
            });
            
            module.DefineLibraryMethod("induced_from", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.InducedFrom),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.InducedFrom),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.InducedFrom),
                new Func<IronRuby.Builtins.RubyClass, System.Object, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.InducedFrom),
            });
            
            module.DefineLibraryMethod("limit", 0x21, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Int32, System.Int32>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Limit),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object, System.Int32>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Limit),
            });
            
            module.DefineLibraryMethod("mode", 0x21, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Int32, System.Int32>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Mode),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Int32, System.Object, System.Int32>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Mode),
            });
            
            module.DefineLibraryMethod("ver", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Version),
            });
            
        }
        
        private void LoadIronRuby__Builtins__Kernel_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("BigDecimal", 0x12, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.KernelOps.CreateBigDecimal),
            });
            
        }
        
        private void LoadIronRuby__Builtins__Kernel_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("BigDecimal", 0x21, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.KernelOps.CreateBigDecimal),
            });
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.Iconv {
    public sealed class IconvLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(System.Object));
            
            
            DefineGlobalClass("Iconv", typeof(IronRuby.StandardLibrary.Iconv.Iconv), true, classRef0, new Action<IronRuby.Builtins.RubyModule>(LoadIconv_Instance), new Action<IronRuby.Builtins.RubyModule>(LoadIconv_Class), IronRuby.Builtins.RubyModule.EmptyArray, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Iconv.Iconv>(IronRuby.StandardLibrary.Iconv.Iconv.Create),
            });
        }
        
        private void LoadIconv_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("close", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Iconv.Iconv, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Iconv.Iconv.Close),
            });
            
            module.DefineLibraryMethod("iconv", 0x11, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Iconv.Iconv, IronRuby.Builtins.MutableString, System.Int32, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Iconv.Iconv.iconv),
            });
            
            module.DefineLibraryMethod("initialize", 0x12, new System.Delegate[] {
                new Func<IronRuby.StandardLibrary.Iconv.Iconv, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Iconv.Iconv>(IronRuby.StandardLibrary.Iconv.Iconv.Initialize),
            });
            
        }
        
        private void LoadIconv_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("charset_map", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.Hash>(IronRuby.StandardLibrary.Iconv.Iconv.CharsetMap),
            });
            
            module.DefineLibraryMethod("conv", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Iconv.Iconv.Convert),
            });
            
            module.DefineLibraryMethod("iconv", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString[], IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Iconv.Iconv.iconv),
            });
            
            module.DefineLibraryMethod("open", 0x21, new System.Delegate[] {
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Iconv.Iconv>(IronRuby.StandardLibrary.Iconv.Iconv.Create),
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Iconv.Iconv.Open),
            });
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.IronRubyModule {
    public sealed class IronRubyModuleLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            
            
            IronRuby.Builtins.RubyModule def1 = DefineGlobalModule("IronRuby", typeof(IronRuby.Ruby), false, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def2 = DefineModule("IronRuby::Clr", typeof(IronRuby.StandardLibrary.IronRubyModule.IronRubyOps.ClrOps), true, null, new Action<IronRuby.Builtins.RubyModule>(LoadIronRuby__Clr_Class), IronRuby.Builtins.RubyModule.EmptyArray);
            def1.SetConstant("Clr", def2);
        }
        
        private void LoadIronRuby__Clr_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("profile", 0x21, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Hash>(IronRuby.StandardLibrary.IronRubyModule.IronRubyOps.ClrOps.GetProfile),
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.StandardLibrary.IronRubyModule.IronRubyOps.ClrOps.GetProfile),
            });
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.ParseTree {
    public sealed class ParseTreeLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            
            
            IronRuby.Builtins.RubyModule def1 = DefineGlobalModule("IronRuby", typeof(IronRuby.Ruby), false, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def2 = DefineModule("IronRuby::ParseTree", typeof(IronRuby.StandardLibrary.ParseTree.IronRubyOps.ParseTreeOps), true, new Action<IronRuby.Builtins.RubyModule>(LoadIronRuby__ParseTree_Instance), null, IronRuby.Builtins.RubyModule.EmptyArray);
            def1.SetConstant("ParseTree", def2);
        }
        
        private void LoadIronRuby__ParseTree_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            
            module.DefineLibraryMethod("parse_tree_for_meth", 0x11, new System.Delegate[] {
                new Func<System.Object, IronRuby.Builtins.RubyModule, System.String, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.ParseTree.IronRubyOps.ParseTreeOps.CreateParseTreeForMethod),
            });
            
            module.DefineLibraryMethod("parse_tree_for_str", 0x11, new System.Delegate[] {
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.ParseTree.IronRubyOps.ParseTreeOps.CreateParseTreeForString),
            });
            
        }
        
    }
}


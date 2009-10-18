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
[assembly: IronRuby.Runtime.RubyLibraryAttribute(typeof(IronRuby.StandardLibrary.ParseTree.ParseTreeLibraryInitializer))]
[assembly: IronRuby.Runtime.RubyLibraryAttribute(typeof(IronRuby.StandardLibrary.Open3.Open3LibraryInitializer))]
[assembly: IronRuby.Runtime.RubyLibraryAttribute(typeof(IronRuby.StandardLibrary.Win32API.Win32APILibraryInitializer))]

namespace IronRuby.Builtins {
    using System;
    using Microsoft.Scripting.Utils;
    
    public sealed class BuiltinsLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            Context.RegisterPrimitives(
                Load__ClassSingleton_Instance,
                Load__ClassSingletonSingleton_Instance,
                Load__MainSingleton_Instance,
                LoadKernel_Instance, LoadKernel_Class, null,
                LoadObject_Instance, LoadObject_Class, LoadObject_Constants,
                LoadModule_Instance, LoadModule_Class, null,
                LoadClass_Instance, LoadClass_Class, null
            );
            
            
            // Skipped primitive: __ClassSingleton
            // Skipped primitive: __MainSingleton
            IronRuby.Builtins.RubyModule def45 = DefineGlobalModule("Comparable", typeof(IronRuby.Builtins.Comparable), 0x00000107, LoadComparable_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def36 = DefineGlobalModule("Enumerable", typeof(IronRuby.Builtins.Enumerable), 0x00000107, LoadEnumerable_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def2 = DefineGlobalModule("Errno", typeof(IronRuby.Builtins.Errno), 0x00000107, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def27 = DefineModule("File::Constants", typeof(IronRuby.Builtins.RubyFileOps.Constants), 0x00000107, null, null, LoadFile__Constants_Constants, IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalModule("FileTest", typeof(IronRuby.Builtins.FileTest), 0x00000107, null, LoadFileTest_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalModule("GC", typeof(IronRuby.Builtins.RubyGC), 0x00000107, LoadGC_Instance, LoadGC_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def23 = DefineGlobalModule("IronRuby", typeof(IronRuby.Ruby), 0x00000004, null, LoadIronRuby_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def4 = DefineModule("IronRuby::Clr", typeof(IronRuby.Builtins.IronRubyOps.ClrOps), 0x00000100, null, LoadIronRuby__Clr_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def5 = DefineModule("IronRuby::Clr::BigInteger", typeof(IronRuby.Builtins.ClrBigInteger), 0x00000107, LoadIronRuby__Clr__BigInteger_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def21 = DefineModule("IronRuby::Clr::FlagEnumeration", typeof(IronRuby.Builtins.FlagEnumeration), 0x00000000, LoadIronRuby__Clr__FlagEnumeration_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def6 = DefineModule("IronRuby::Clr::Float", typeof(IronRuby.Builtins.ClrFloat), 0x00000107, LoadIronRuby__Clr__Float_Instance, LoadIronRuby__Clr__Float_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def7 = DefineModule("IronRuby::Clr::Integer", typeof(IronRuby.Builtins.ClrInteger), 0x00000107, LoadIronRuby__Clr__Integer_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def24 = DefineModule("IronRuby::Clr::MultiDimensionalArray", typeof(IronRuby.Builtins.MultiDimensionalArray), 0x00000000, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def8 = DefineModule("IronRuby::Clr::String", typeof(IronRuby.Builtins.ClrString), 0x00000107, LoadIronRuby__Clr__String_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def25 = DefineModule("IronRuby::Print", typeof(IronRuby.Builtins.PrintOps), 0x00000107, LoadIronRuby__Print_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            // Skipped primitive: Kernel
            DefineGlobalModule("Marshal", typeof(IronRuby.Builtins.RubyMarshal), 0x00000107, null, LoadMarshal_Class, LoadMarshal_Constants, IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalModule("Math", typeof(IronRuby.Builtins.RubyMath), 0x00000107, LoadMath_Instance, LoadMath_Class, LoadMath_Constants, IronRuby.Builtins.RubyModule.EmptyArray);
            ExtendClass(typeof(Microsoft.Scripting.Actions.TypeTracker), 0x00000000, null, LoadMicrosoft__Scripting__Actions__TypeTracker_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalModule("ObjectSpace", typeof(IronRuby.Builtins.ObjectSpace), 0x00000107, null, LoadObjectSpace_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def42 = DefineGlobalModule("Precision", typeof(IronRuby.Builtins.Precision), 0x00000107, LoadPrecision_Instance, LoadPrecision_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyModule def28 = DefineGlobalModule("Process", typeof(IronRuby.Builtins.RubyProcess), 0x00000107, LoadProcess_Instance, LoadProcess_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            #endif
            #if !SILVERLIGHT
            DefineGlobalModule("Signal", typeof(IronRuby.Builtins.Signal), 0x00000107, null, LoadSignal_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            #endif
            ExtendClass(typeof(System.Type), 0x00000000, null, LoadSystem__Type_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            // Skipped primitive: __ClassSingletonSingleton
            #if !SILVERLIGHT
            object def1 = DefineSingleton(Load__Singleton_ArgFilesSingletonOps_Instance, null, null, def36);
            #endif
            object def9 = DefineSingleton(Load__Singleton_EnvironmentSingletonOps_Instance, null, null, def36);
            ExtendClass(typeof(Microsoft.Scripting.Actions.TypeGroup), 0x00000000, null, LoadMicrosoft__Scripting__Actions__TypeGroup_Instance, null, null, new IronRuby.Builtins.RubyModule[] {def36});
            // Skipped primitive: Object
            ExtendClass(typeof(System.Char), 0x00000000, null, LoadSystem__Char_Instance, null, null, new IronRuby.Builtins.RubyModule[] {def8, def36, def45}, 
                new Func<IronRuby.Builtins.RubyClass, System.Char, System.Char>(IronRuby.Builtins.CharOps.Create), 
                new Func<IronRuby.Builtins.RubyClass, System.Char[], System.Char>(IronRuby.Builtins.CharOps.Create), 
                new Func<IronRuby.Builtins.RubyClass, System.String, System.Char>(IronRuby.Builtins.CharOps.Create), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Char>(IronRuby.Builtins.CharOps.Create)
            );
            ExtendModule(typeof(System.Collections.Generic.IDictionary<System.Object, System.Object>), 0x00000000, LoadSystem__Collections__Generic__IDictionary_Instance, null, null, def36);
            ExtendModule(typeof(System.Collections.IEnumerable), 0x00000000, LoadSystem__Collections__IEnumerable_Instance, null, null, def36);
            ExtendModule(typeof(System.Collections.IList), 0x00000000, LoadSystem__Collections__IList_Instance, null, null, def36);
            ExtendModule(typeof(System.IComparable), 0x00000000, LoadSystem__IComparable_Instance, null, null, def45);
            ExtendClass(typeof(System.String), 0x00000000, null, LoadSystem__String_Instance, null, null, new IronRuby.Builtins.RubyModule[] {def8, def36, def45}, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.String>(IronRuby.Builtins.ClrStringOps.Create), 
                new Func<IronRuby.Builtins.RubyClass, System.Char, System.Int32, System.String>(IronRuby.Builtins.ClrStringOps.Create), 
                new Func<IronRuby.Builtins.RubyClass, System.Char[], System.String>(IronRuby.Builtins.ClrStringOps.Create), 
                new Func<IronRuby.Builtins.RubyClass, System.Char[], System.Int32, System.Int32, System.String>(IronRuby.Builtins.ClrStringOps.Create)
            );
            DefineGlobalClass("Array", typeof(IronRuby.Builtins.RubyArray), 0x00000007, Context.ObjectClass, LoadArray_Instance, LoadArray_Class, null, new IronRuby.Builtins.RubyModule[] {def36}, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.CreateArray), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Runtime.Union<System.Collections.IList, System.Int32>>, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, System.Object, System.Object>(IronRuby.Builtins.ArrayOps.CreateArray), 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, System.Int32, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.CreateArray), 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.CreateArray)
            );
            DefineGlobalClass("Binding", typeof(IronRuby.Builtins.Binding), 0x00000007, Context.ObjectClass, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalClass("Dir", typeof(IronRuby.Builtins.RubyDir), 0x00000107, Context.ObjectClass, LoadDir_Instance, LoadDir_Class, null, new IronRuby.Builtins.RubyModule[] {def36});
            #if !SILVERLIGHT
            if (Context.RubyOptions.Compatibility >= RubyCompatibility.Ruby19) {
            DefineGlobalClass("Encoding", typeof(IronRuby.Builtins.RubyEncoding), 0x00000007, Context.ObjectClass, LoadEncoding_Instance, LoadEncoding_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            }
            #endif
            IronRuby.Builtins.RubyClass def46 = Context.ExceptionClass = DefineGlobalClass("Exception", typeof(System.Exception), 0x00000007, Context.ObjectClass, LoadException_Instance, LoadException_Class, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__Exception));
            Context.FalseClass = DefineGlobalClass("FalseClass", typeof(IronRuby.Builtins.FalseClass), 0x00000107, Context.ObjectClass, LoadFalseClass_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def34 = DefineClass("File::Stat", typeof(System.IO.FileSystemInfo), 0x00000007, Context.ObjectClass, LoadFile__Stat_Instance, null, null, new IronRuby.Builtins.RubyModule[] {def45}, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.IO.FileSystemInfo>(IronRuby.Builtins.RubyFileOps.RubyStatOps.Create)
            );
            #endif
            DefineGlobalClass("Hash", typeof(IronRuby.Builtins.Hash), 0x00000007, Context.ObjectClass, LoadHash_Instance, LoadHash_Class, null, new IronRuby.Builtins.RubyModule[] {def36}, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.CreateHash), 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, System.Object, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.CreateHash), 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.CreateHash)
            );
            IronRuby.Builtins.RubyClass def47 = DefineGlobalClass("IO", typeof(IronRuby.Builtins.RubyIO), 0x00000007, Context.ObjectClass, LoadIO_Instance, LoadIO_Class, LoadIO_Constants, new IronRuby.Builtins.RubyModule[] {def27, def36}, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Create), 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Int32, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Create)
            );
            IronRuby.Builtins.RubyClass def30 = DefineClass("IronRuby::Clr::Name", typeof(IronRuby.Runtime.ClrName), 0x00000007, Context.ObjectClass, LoadIronRuby__Clr__Name_Instance, LoadIronRuby__Clr__Name_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def31 = DefineClass("IronRuby::Clr::Scope", typeof(Microsoft.Scripting.Runtime.Scope), 0x00000007, Context.ObjectClass, LoadIronRuby__Clr__Scope_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalClass("MatchData", typeof(IronRuby.Builtins.MatchData), 0x00000007, Context.ObjectClass, LoadMatchData_Instance, LoadMatchData_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalClass("Method", typeof(IronRuby.Builtins.RubyMethod), 0x00000007, Context.ObjectClass, LoadMethod_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            // Skipped primitive: Module
            Context.NilClass = DefineGlobalClass("NilClass", typeof(Microsoft.Scripting.Runtime.DynamicNull), 0x00000007, Context.ObjectClass, LoadNilClass_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def41 = DefineGlobalClass("Numeric", typeof(IronRuby.Builtins.Numeric), 0x00000107, Context.ObjectClass, LoadNumeric_Instance, null, null, new IronRuby.Builtins.RubyModule[] {def45});
            DefineGlobalClass("Proc", typeof(IronRuby.Builtins.Proc), 0x00000007, Context.ObjectClass, LoadProc_Instance, LoadProc_Class, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Action<IronRuby.Builtins.RubyClass, System.Object[]>(IronRuby.Builtins.ProcOps.Error)
            );
            #if !SILVERLIGHT && !SILVERLIGHT
            IronRuby.Builtins.RubyClass def29 = DefineClass("Process::Status", typeof(IronRuby.Builtins.RubyProcess.Status), 0x00000107, Context.ObjectClass, LoadProcess__Status_Instance, LoadProcess__Status_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            #endif
            DefineGlobalClass("Range", typeof(IronRuby.Builtins.Range), 0x00000007, Context.ObjectClass, LoadRange_Instance, null, null, new IronRuby.Builtins.RubyModule[] {def36}, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Builtins.RubyClass, System.Object, System.Object, System.Boolean, IronRuby.Builtins.Range>(IronRuby.Builtins.RangeOps.CreateRange)
            );
            DefineGlobalClass("Regexp", typeof(IronRuby.Builtins.RubyRegex), 0x00000007, Context.ObjectClass, LoadRegexp_Instance, LoadRegexp_Class, LoadRegexp_Constants, new IronRuby.Builtins.RubyModule[] {def36}, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Create), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyRegex, System.Int32, System.Object, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Create), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyRegex, System.Object, System.Object, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Create), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Create), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Create)
            );
            DefineGlobalClass("String", typeof(IronRuby.Builtins.MutableString), 0x00000007, Context.ObjectClass, LoadString_Instance, null, null, new IronRuby.Builtins.RubyModule[] {def36, def45}, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Create), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Create)
            );
            DefineGlobalClass("Struct", typeof(IronRuby.Builtins.RubyStruct), 0x00000007, Context.ObjectClass, LoadStruct_Instance, LoadStruct_Class, LoadStruct_Constants, new IronRuby.Builtins.RubyModule[] {def36}, 
                new Action<IronRuby.Builtins.RubyClass, System.Object[]>(IronRuby.Builtins.RubyStructOps.AllocatorUndefined)
            );
            DefineGlobalClass("Symbol", typeof(Microsoft.Scripting.SymbolId), 0x00000007, Context.ObjectClass, LoadSymbol_Instance, LoadSymbol_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalClass("Thread", typeof(System.Threading.Thread), 0x00000007, Context.ObjectClass, LoadThread_Instance, LoadThread_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalClass("ThreadGroup", typeof(IronRuby.Builtins.ThreadGroup), 0x00000107, Context.ObjectClass, LoadThreadGroup_Instance, null, LoadThreadGroup_Constants, IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalClass("Time", typeof(System.DateTime), 0x00000007, Context.ObjectClass, LoadTime_Instance, LoadTime_Class, null, new IronRuby.Builtins.RubyModule[] {def45}, 
                new Func<IronRuby.Builtins.RubyClass, System.DateTime>(IronRuby.Builtins.TimeOps.Create)
            );
            Context.TrueClass = DefineGlobalClass("TrueClass", typeof(IronRuby.Builtins.TrueClass), 0x00000107, Context.ObjectClass, LoadTrueClass_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalClass("UnboundMethod", typeof(IronRuby.Builtins.UnboundMethod), 0x00000107, Context.ObjectClass, LoadUnboundMethod_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            // Skipped primitive: Class
            IronRuby.Builtins.RubyClass def26 = DefineGlobalClass("File", typeof(IronRuby.Builtins.RubyFile), 0x00000007, def47, LoadFile_Instance, LoadFile_Class, LoadFile_Constants, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Runtime.Union<System.Int32, IronRuby.Builtins.MutableString>, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.RubyFile>(IronRuby.Builtins.RubyFileOps.CreateFile), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Runtime.Union<System.Int32, IronRuby.Builtins.MutableString>, System.Int32, System.Int32, IronRuby.Builtins.RubyFile>(IronRuby.Builtins.RubyFileOps.CreateFile), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyFile>(IronRuby.Builtins.RubyFileOps.CreateFile), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyFile>(IronRuby.Builtins.RubyFileOps.CreateFile), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.RubyFile>(IronRuby.Builtins.RubyFileOps.CreateFile)
            );
            DefineGlobalClass("Float", typeof(System.Double), 0x00000007, def41, LoadFloat_Instance, LoadFloat_Class, LoadFloat_Constants, new IronRuby.Builtins.RubyModule[] {def42});
            IronRuby.Builtins.RubyClass def48 = DefineGlobalClass("Integer", typeof(IronRuby.Builtins.Integer), 0x00000107, def41, LoadInteger_Instance, LoadInteger_Class, null, new IronRuby.Builtins.RubyModule[] {def42});
            DefineGlobalClass("NoMemoryError", typeof(IronRuby.Builtins.NoMemoryError), 0x00000107, def46, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__NoMemoryError));
            IronRuby.Builtins.RubyClass def44 = DefineGlobalClass("ScriptError", typeof(IronRuby.Builtins.ScriptError), 0x00000007, def46, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__ScriptError));
            IronRuby.Builtins.RubyClass def43 = DefineGlobalClass("SignalException", typeof(IronRuby.Builtins.SignalException), 0x00000107, def46, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__SignalException));
            IronRuby.Builtins.RubyClass def38 = Context.StandardErrorClass = DefineGlobalClass("StandardError", typeof(System.SystemException), 0x00000007, def46, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__StandardError));
            ExtendClass(typeof(System.Decimal), 0x00000000, def41, LoadSystem__Decimal_Instance, LoadSystem__Decimal_Class, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyModule, System.Double, System.Decimal>(IronRuby.Builtins.DecimalOps.InducedFrom)
            );
            ExtendClass(typeof(System.Single), 0x00000000, def41, LoadSystem__Single_Instance, LoadSystem__Single_Class, null, new IronRuby.Builtins.RubyModule[] {def42}, 
                new Func<IronRuby.Builtins.RubyClass, System.Double, System.Single>(IronRuby.Builtins.SingleOps.Create)
            );
            DefineGlobalClass("SystemExit", typeof(IronRuby.Builtins.SystemExit), 0x00000007, def46, LoadSystemExit_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, System.Object, IronRuby.Builtins.SystemExit>(IronRuby.Builtins.SystemExitOps.Factory), 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Object, IronRuby.Builtins.SystemExit>(IronRuby.Builtins.SystemExitOps.Factory)
            );
            DefineGlobalClass("ArgumentError", typeof(System.ArgumentException), 0x00000007, def38, LoadArgumentError_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__ArgumentError));
            DefineGlobalClass("Bignum", typeof(Microsoft.Scripting.Math.BigInteger), 0x00000007, def48, LoadBignum_Instance, LoadBignum_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalClass("EncodingError", typeof(IronRuby.Builtins.EncodingError), 0x00000007, def38, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__EncodingError));
            DefineGlobalClass("Fixnum", typeof(System.Int32), 0x00000007, def48, LoadFixnum_Instance, LoadFixnum_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalClass("IndexError", typeof(System.IndexOutOfRangeException), 0x00000007, def38, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__IndexError));
            DefineGlobalClass("Interrupt", typeof(IronRuby.Builtins.Interrupt), 0x00000107, def43, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__Interrupt));
            IronRuby.Builtins.RubyClass def39 = DefineGlobalClass("IOError", typeof(System.IO.IOException), 0x00000007, def38, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__IOError));
            DefineGlobalClass("LoadError", typeof(IronRuby.Builtins.LoadError), 0x00000007, def44, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__LoadError));
            DefineGlobalClass("LocalJumpError", typeof(IronRuby.Builtins.LocalJumpError), 0x00000007, def38, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__LocalJumpError));
            IronRuby.Builtins.RubyClass def49 = DefineGlobalClass("NameError", typeof(System.MemberAccessException), 0x00000007, def38, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__NameError));
            DefineGlobalClass("NotImplementedError", typeof(IronRuby.Builtins.NotImplementedError), 0x00000007, def44, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__NotImplementedError));
            IronRuby.Builtins.RubyClass def40 = DefineGlobalClass("RangeError", typeof(System.ArgumentOutOfRangeException), 0x00000007, def38, LoadRangeError_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__RangeError));
            DefineGlobalClass("RegexpError", typeof(IronRuby.Builtins.RegexpError), 0x00000007, def38, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__RegexpError));
            DefineGlobalClass("RuntimeError", typeof(IronRuby.Builtins.RuntimeError), 0x00000107, def38, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__RuntimeError));
            DefineGlobalClass("SecurityError", typeof(System.Security.SecurityException), 0x00000007, def38, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__SecurityError));
            DefineGlobalClass("SyntaxError", typeof(IronRuby.Builtins.SyntaxError), 0x00000007, def44, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__SyntaxError));
            ExtendClass(typeof(System.Byte), 0x00000000, def48, LoadSystem__Byte_Instance, LoadSystem__Byte_Class, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Byte>(IronRuby.Builtins.ByteOps.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, System.Byte>(IronRuby.Builtins.ByteOps.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, System.Double, System.Byte>(IronRuby.Builtins.ByteOps.InducedFrom)
            );
            ExtendClass(typeof(System.Int16), 0x00000000, def48, LoadSystem__Int16_Instance, LoadSystem__Int16_Class, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Int16>(IronRuby.Builtins.Int16Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, System.Int16>(IronRuby.Builtins.Int16Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, System.Double, System.Int16>(IronRuby.Builtins.Int16Ops.InducedFrom)
            );
            ExtendClass(typeof(System.Int64), 0x00000000, def48, LoadSystem__Int64_Instance, LoadSystem__Int64_Class, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Int64>(IronRuby.Builtins.Int64Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, System.Int64>(IronRuby.Builtins.Int64Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, System.Double, System.Int64>(IronRuby.Builtins.Int64Ops.InducedFrom)
            );
            ExtendClass(typeof(System.SByte), 0x00000000, def48, LoadSystem__SByte_Instance, LoadSystem__SByte_Class, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.SByte>(IronRuby.Builtins.SByteOps.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, System.SByte>(IronRuby.Builtins.SByteOps.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, System.Double, System.SByte>(IronRuby.Builtins.SByteOps.InducedFrom)
            );
            ExtendClass(typeof(System.UInt16), 0x00000000, def48, LoadSystem__UInt16_Instance, LoadSystem__UInt16_Class, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.UInt16>(IronRuby.Builtins.UInt16Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, System.UInt16>(IronRuby.Builtins.UInt16Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, System.Double, System.UInt16>(IronRuby.Builtins.UInt16Ops.InducedFrom)
            );
            ExtendClass(typeof(System.UInt32), 0x00000000, def48, LoadSystem__UInt32_Instance, LoadSystem__UInt32_Class, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.UInt32>(IronRuby.Builtins.UInt32Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, System.UInt32>(IronRuby.Builtins.UInt32Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, System.Double, System.UInt32>(IronRuby.Builtins.UInt32Ops.InducedFrom)
            );
            ExtendClass(typeof(System.UInt64), 0x00000000, def48, LoadSystem__UInt64_Instance, LoadSystem__UInt64_Class, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.UInt64>(IronRuby.Builtins.UInt64Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, System.UInt64>(IronRuby.Builtins.UInt64Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, System.Double, System.UInt64>(IronRuby.Builtins.UInt64Ops.InducedFrom)
            );
            IronRuby.Builtins.RubyClass def37 = DefineGlobalClass("SystemCallError", typeof(System.Runtime.InteropServices.ExternalException), 0x00000007, def38, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Runtime.InteropServices.ExternalException>(IronRuby.Builtins.SystemCallErrorOps.Factory), 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Runtime.InteropServices.ExternalException>(IronRuby.Builtins.SystemCallErrorOps.Factory)
            );
            DefineGlobalClass("SystemStackError", typeof(IronRuby.Builtins.SystemStackError), 0x00000007, def38, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__SystemStackError));
            DefineGlobalClass("ThreadError", typeof(IronRuby.Builtins.ThreadError), 0x00000107, def38, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__ThreadError));
            DefineGlobalClass("TypeError", typeof(System.InvalidOperationException), 0x00000007, def38, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__TypeError));
            DefineGlobalClass("ZeroDivisionError", typeof(System.DivideByZeroException), 0x00000007, def38, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__ZeroDivisionError));
            DefineGlobalClass("EOFError", typeof(IronRuby.Builtins.EOFError), 0x00000107, def39, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__EOFError));
            IronRuby.Builtins.RubyClass def35 = DefineClass("Errno::EACCES", typeof(System.UnauthorizedAccessException), 0x00000007, def37, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.UnauthorizedAccessException>(IronRuby.Builtins.Errno.UnauthorizedAccessExceptionOps.Create)
            );
            IronRuby.Builtins.RubyClass def10 = DefineClass("Errno::EADDRINUSE", typeof(IronRuby.Builtins.Errno.AddressInUseError), 0x00000107, def37, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def3 = DefineClass("Errno::EBADF", typeof(IronRuby.Builtins.BadFileDescriptorError), 0x00000007, def37, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.BadFileDescriptorError>(IronRuby.Builtins.Errno.BadFileDescriptorErrorOps.Create)
            );
            IronRuby.Builtins.RubyClass def11 = DefineClass("Errno::ECHILD", typeof(IronRuby.Builtins.Errno.ChildError), 0x00000107, def37, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def12 = DefineClass("Errno::ECONNABORTED", typeof(IronRuby.Builtins.Errno.ConnectionAbortError), 0x00000107, def37, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def13 = DefineClass("Errno::ECONNREFUSED", typeof(IronRuby.Builtins.Errno.ConnectionRefusedError), 0x00000107, def37, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def14 = DefineClass("Errno::ECONNRESET", typeof(IronRuby.Builtins.Errno.ConnectionResetError), 0x00000107, def37, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def15 = DefineClass("Errno::EDOM", typeof(IronRuby.Builtins.Errno.DomainError), 0x00000107, def37, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def20 = DefineClass("Errno::EEXIST", typeof(IronRuby.Builtins.ExistError), 0x00000007, def37, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.ExistError>(IronRuby.Builtins.Errno.ExistErrorOps.Create)
            );
            IronRuby.Builtins.RubyClass def22 = DefineClass("Errno::EINVAL", typeof(IronRuby.Builtins.InvalidError), 0x00000007, def37, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.InvalidError>(IronRuby.Builtins.Errno.InvalidErrorOps.Create)
            );
            IronRuby.Builtins.RubyClass def33 = DefineClass("Errno::ENOENT", typeof(System.IO.FileNotFoundException), 0x00000007, def37, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.IO.FileNotFoundException>(IronRuby.Builtins.Errno.FileNotFoundExceptionOps.Create)
            );
            IronRuby.Builtins.RubyClass def19 = DefineClass("Errno::ENOEXEC", typeof(IronRuby.Builtins.ExecFormatError), 0x00000007, def37, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.BadFileDescriptorError>(IronRuby.Builtins.Errno.ExecFormatErrorOps.Create)
            );
            IronRuby.Builtins.RubyClass def17 = DefineClass("Errno::ENOTCONN", typeof(IronRuby.Builtins.Errno.NotConnectedError), 0x00000107, def37, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def32 = DefineClass("Errno::ENOTDIR", typeof(System.IO.DirectoryNotFoundException), 0x00000007, def37, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.IO.DirectoryNotFoundException>(IronRuby.Builtins.Errno.DirectoryNotFoundExceptionOps.Create)
            );
            IronRuby.Builtins.RubyClass def18 = DefineClass("Errno::EPIPE", typeof(IronRuby.Builtins.Errno.PipeError), 0x00000107, def37, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def16 = DefineClass("Errno::EXDEV", typeof(IronRuby.Builtins.Errno.ImproperLinkError), 0x00000107, def37, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalClass("ESPIPE", typeof(IronRuby.Builtins.InvalidSeekError), 0x00000107, def37, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalClass("FloatDomainError", typeof(IronRuby.Builtins.FloatDomainError), 0x00000107, def40, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__FloatDomainError));
            DefineGlobalClass("NoMethodError", typeof(System.MissingMethodException), 0x00000007, def49, LoadNoMethodError_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(BuiltinsLibraryInitializer.ExceptionFactory__NoMethodError));
            def26.SetBuiltinConstant("Constants", def27);
            def23.SetBuiltinConstant("Clr", def4);
            def4.SetBuiltinConstant("BigInteger", def5);
            def4.SetBuiltinConstant("FlagEnumeration", def21);
            def4.SetBuiltinConstant("Float", def6);
            def4.SetBuiltinConstant("Integer", def7);
            def4.SetBuiltinConstant("MultiDimensionalArray", def24);
            def4.SetBuiltinConstant("String", def8);
            def23.SetBuiltinConstant("Print", def25);
            #if !SILVERLIGHT
            Context.ObjectClass.SetBuiltinConstant("ARGF", def1);
            #endif
            Context.ObjectClass.SetBuiltinConstant("ENV", def9);
            #if !SILVERLIGHT
            def26.SetBuiltinConstant("Stat", def34);
            #endif
            def4.SetBuiltinConstant("Name", def30);
            def4.SetBuiltinConstant("Scope", def31);
            #if !SILVERLIGHT && !SILVERLIGHT
            def28.SetBuiltinConstant("Status", def29);
            #endif
            def2.SetBuiltinConstant("EACCES", def35);
            def2.SetBuiltinConstant("EADDRINUSE", def10);
            def2.SetBuiltinConstant("EBADF", def3);
            def2.SetBuiltinConstant("ECHILD", def11);
            def2.SetBuiltinConstant("ECONNABORTED", def12);
            def2.SetBuiltinConstant("ECONNREFUSED", def13);
            def2.SetBuiltinConstant("ECONNRESET", def14);
            def2.SetBuiltinConstant("EDOM", def15);
            def2.SetBuiltinConstant("EEXIST", def20);
            def2.SetBuiltinConstant("EINVAL", def22);
            def2.SetBuiltinConstant("ENOENT", def33);
            def2.SetBuiltinConstant("ENOEXEC", def19);
            def2.SetBuiltinConstant("ENOTCONN", def17);
            def2.SetBuiltinConstant("ENOTDIR", def32);
            def2.SetBuiltinConstant("EPIPE", def18);
            def2.SetBuiltinConstant("EXDEV", def16);
        }
        
        private static void Load__ClassSingleton_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("allocate", 0x51, 
                new Action<IronRuby.Builtins.RubyClass>(IronRuby.Builtins.ClassSingletonOps.Allocate)
            );
            
            module.DefineLibraryMethod("inherited", 0x52, 
                new Action<System.Object, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.ClassSingletonOps.Inherited)
            );
            
            module.DefineLibraryMethod("initialize", 0x52, 
                new Func<System.Object, System.Object>(IronRuby.Builtins.ClassSingletonOps.Initialize)
            );
            
            module.DefineLibraryMethod("initialize_copy", 0x52, 
                new Func<System.Object, System.Object, System.Object>(IronRuby.Builtins.ClassSingletonOps.InitializeCopy)
            );
            
            module.DefineLibraryMethod("new", 0x51, 
                new Action<IronRuby.Builtins.RubyClass>(IronRuby.Builtins.ClassSingletonOps.New)
            );
            
            module.DefineLibraryMethod("superclass", 0x51, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.ClassSingletonOps.GetSuperClass)
            );
            
        }
        
        private static void Load__ClassSingleton_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
        }
        
        private static void Load__ClassSingletonSingleton_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            Load__ClassSingleton_Instance(module);
            module.DefineLibraryMethod("constants", 0x51, 
                new Func<System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ClassSingletonSingletonOps.GetConstants)
            );
            
            module.DefineLibraryMethod("nesting", 0x51, 
                new Func<System.Object, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ClassSingletonSingletonOps.GetNesting)
            );
            
        }
        
        private static void Load__ClassSingletonSingleton_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            Load__ClassSingleton_Class(module);
        }
        
        private static void Load__MainSingleton_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("include", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyModule[], IronRuby.Builtins.RubyClass>(IronRuby.Builtins.MainSingletonOps.Include)
            );
            
            module.DefineLibraryMethod("initialize", 0x52, 
                new Func<System.Object, System.Object>(IronRuby.Builtins.MainSingletonOps.Initialize)
            );
            
            module.DefineLibraryMethod("private", 0x51, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.String[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.MainSingletonOps.SetPrivateVisibility)
            );
            
            module.DefineLibraryMethod("public", 0x51, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.String[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.MainSingletonOps.SetPublicVisibility)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MainSingletonOps.ToS)
            );
            
        }
        
        private static void Load__MainSingleton_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
        }
        
        #if !SILVERLIGHT
        private static void Load__Singleton_ArgFilesSingletonOps_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("filename", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ArgFilesSingletonOps.GetCurrentFileName)
            );
            
        }
        #endif
        
        private static void LoadArgumentError_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.HideMethod("message");
        }
        
        private static void LoadArray_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            LoadSystem__Collections__IList_Instance(module);
            module.DefineLibraryMethod("initialize", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.Reinitialize), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Runtime.Union<System.Collections.IList, System.Int32>>, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyArray, System.Object, System.Object>(IronRuby.Builtins.ArrayOps.Reinitialize), 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyArray, System.Int32, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.Reinitialize), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyArray, System.Int32, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.ReinitializeByRepeatedValue)
            );
            
            module.DefineLibraryMethod("pack", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Runtime.IntegerValue>, IronRuby.Runtime.ConversionStorage<System.Double>, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Builtins.RubyArray, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ArrayOps.Pack)
            );
            
            module.DefineLibraryMethod("reverse!", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.InPlaceReverse)
            );
            
            module.DefineLibraryMethod("reverse_each", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyArray, System.Object>(IronRuby.Builtins.ArrayOps.ReverseEach)
            );
            
            module.DefineLibraryMethod("sort", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyArray, System.Object>(IronRuby.Builtins.ArrayOps.Sort)
            );
            
            module.DefineLibraryMethod("sort!", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyArray, System.Object>(IronRuby.Builtins.ArrayOps.SortInPlace)
            );
            
            module.DefineLibraryMethod("to_a", 0x51, 
                new Func<IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.ToArray)
            );
            
            module.DefineLibraryMethod("to_ary", 0x51, 
                new Func<IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.ToExplicitArray)
            );
            
        }
        
        private static void LoadArray_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("[]", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.Object[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ArrayOps.MakeArray)
            );
            
        }
        
        private static void LoadBignum_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            LoadIronRuby__Clr__BigInteger_Instance(module);
            module.HideMethod("<");
            module.HideMethod("<=");
            module.HideMethod(">");
            module.HideMethod(">=");
            module.DefineLibraryMethod("size", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32>(IronRuby.Builtins.BignumOps.Size)
            );
            
        }
        
        private static void LoadBignum_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.UndefineMethodNoEvent("new");
        }
        
        private static void LoadClass_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.UndefineMethodNoEvent("append_features");
            module.UndefineMethodNoEvent("extend_object");
            module.UndefineMethodNoEvent("module_function");
            module.DefineRuleGenerator("allocate", 0x51, IronRuby.Builtins.ClassOps.Allocate());
            
            module.DefineLibraryMethod("clr_constructor", 0x51, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyMethod>(IronRuby.Builtins.ClassOps.GetClrConstructor)
            );
            
            module.DefineLibraryMethod("clr_ctor", 0x51, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyMethod>(IronRuby.Builtins.ClassOps.GetClrConstructor)
            );
            
            module.DefineRuleGenerator("clr_new", 0x51, IronRuby.Builtins.ClassOps.ClrNew());
            
            module.DefineLibraryMethod("inherited", 0x5a, 
                new Action<System.Object, System.Object>(IronRuby.Builtins.ClassOps.Inherited)
            );
            
            module.DefineLibraryMethod("initialize", 0x52, 
                new Action<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.ClassOps.Reinitialize)
            );
            
            module.DefineLibraryMethod("initialize_copy", 0x52, 
                new Action<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.ClassOps.InitializeCopy)
            );
            
            module.DefineRuleGenerator("new", 0x51, IronRuby.Builtins.ClassOps.New());
            
            module.DefineLibraryMethod("superclass", 0x51, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.ClassOps.GetSuperclass)
            );
            
        }
        
        private static void LoadClass_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
        }
        
        private static void LoadComparable_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("<", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.Comparable.Less)
            );
            
            module.DefineLibraryMethod("<=", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.Comparable.LessOrEqual)
            );
            
            module.DefineLibraryMethod("==", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.Comparable.Equal)
            );
            
            module.DefineLibraryMethod(">", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.Comparable.Greater)
            );
            
            module.DefineLibraryMethod(">=", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.Comparable.GreaterOrEqual)
            );
            
            module.DefineLibraryMethod("between?", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.Comparable.Between)
            );
            
        }
        
        private static void LoadDir_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("close", 0x51, 
                new Action<IronRuby.Builtins.RubyDir>(IronRuby.Builtins.RubyDir.Close)
            );
            
            module.DefineLibraryMethod("each", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyDir, IronRuby.Builtins.RubyDir>(IronRuby.Builtins.RubyDir.Each)
            );
            
            module.DefineLibraryMethod("path", 0x51, 
                new Func<IronRuby.Builtins.RubyDir, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyDir.GetPath)
            );
            
            module.DefineLibraryMethod("pos", 0x51, 
                new Func<IronRuby.Builtins.RubyDir, System.Int32>(IronRuby.Builtins.RubyDir.GetCurrentPosition)
            );
            
            module.DefineLibraryMethod("pos=", 0x51, 
                new Func<IronRuby.Builtins.RubyDir, System.Int32, System.Int32>(IronRuby.Builtins.RubyDir.SetPosition)
            );
            
            module.DefineLibraryMethod("read", 0x51, 
                new Func<IronRuby.Builtins.RubyDir, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyDir.Read)
            );
            
            module.DefineLibraryMethod("rewind", 0x51, 
                new Func<IronRuby.Builtins.RubyDir, IronRuby.Builtins.RubyDir>(IronRuby.Builtins.RubyDir.Rewind)
            );
            
            module.DefineLibraryMethod("seek", 0x51, 
                new Func<IronRuby.Builtins.RubyDir, System.Int32, IronRuby.Builtins.RubyDir>(IronRuby.Builtins.RubyDir.Seek)
            );
            
            module.DefineLibraryMethod("tell", 0x51, 
                new Func<IronRuby.Builtins.RubyDir, System.Int32>(IronRuby.Builtins.RubyDir.GetCurrentPosition)
            );
            
        }
        
        private static void LoadDir_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("[]", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyDir.Glob)
            );
            
            module.DefineLibraryMethod("chdir", 0x61, 
                new Func<IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyDir.ChangeDirectory), 
                new Func<System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyDir.ChangeDirectory), 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.RubyDir.ChangeDirectory)
            );
            
            module.DefineLibraryMethod("chroot", 0x61, 
                new Func<System.Object, System.Int32>(IronRuby.Builtins.RubyDir.ChangeRoot)
            );
            
            module.DefineLibraryMethod("delete", 0x61, 
                new Func<System.Object, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyDir.RemoveDirectory)
            );
            
            module.DefineLibraryMethod("entries", 0x61, 
                new Func<System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyDir.GetEntries)
            );
            
            module.DefineLibraryMethod("foreach", 0x61, 
                new Func<IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyDir.ForEach)
            );
            
            module.DefineLibraryMethod("getwd", 0x61, 
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyDir.GetCurrentDirectory)
            );
            
            module.DefineLibraryMethod("glob", 0x61, 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.RubyDir.Glob), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyDir.Glob)
            );
            
            module.DefineLibraryMethod("mkdir", 0x61, 
                new Func<System.Object, IronRuby.Builtins.MutableString, System.Object, System.Int32>(IronRuby.Builtins.RubyDir.MakeDirectory)
            );
            
            module.DefineLibraryMethod("open", 0x61, 
                new Func<IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyDir.Open), 
                new Func<System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyDir.Open)
            );
            
            module.DefineLibraryMethod("pwd", 0x61, 
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyDir.GetCurrentDirectory)
            );
            
            module.DefineLibraryMethod("rmdir", 0x61, 
                new Func<System.Object, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyDir.RemoveDirectory)
            );
            
            module.DefineLibraryMethod("unlink", 0x61, 
                new Func<System.Object, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyDir.RemoveDirectory)
            );
            
        }
        
        #if !SILVERLIGHT
        private static void LoadEncoding_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("_dump", 0x51, 
                new Func<IronRuby.Builtins.RubyEncoding, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyEncodingOps.ToS)
            );
            
            module.DefineLibraryMethod("based_encoding", 0x51, 
                new Func<IronRuby.Builtins.RubyEncoding, IronRuby.Builtins.RubyEncoding>(IronRuby.Builtins.RubyEncodingOps.BasedEncoding)
            );
            
            module.DefineLibraryMethod("dummy?", 0x51, 
                new Func<IronRuby.Builtins.RubyEncoding, System.Boolean>(IronRuby.Builtins.RubyEncodingOps.IsDummy)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyEncoding, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyEncodingOps.Inspect)
            );
            
            module.DefineLibraryMethod("name", 0x51, 
                new Func<IronRuby.Builtins.RubyEncoding, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyEncodingOps.ToS)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<IronRuby.Builtins.RubyEncoding, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyEncodingOps.ToS)
            );
            
        }
        #endif
        
        #if !SILVERLIGHT
        private static void LoadEncoding_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("_load?", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.Boolean>(IronRuby.Builtins.RubyEncodingOps.Load)
            );
            
            module.DefineLibraryMethod("compatible?", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyEncoding, IronRuby.Builtins.RubyEncoding, System.Boolean>(IronRuby.Builtins.RubyEncodingOps.IsCompatible)
            );
            
            module.DefineLibraryMethod("default_external", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyEncoding>(IronRuby.Builtins.RubyEncodingOps.GetDefaultEncoding)
            );
            
            module.DefineLibraryMethod("find", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyEncoding>(IronRuby.Builtins.RubyEncodingOps.GetEncoding)
            );
            
            module.DefineLibraryMethod("list", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyEncodingOps.GetAvailableEncodings)
            );
            
            module.DefineLibraryMethod("locale_charmap", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyEncodingOps.GetDefaultCharmap)
            );
            
        }
        #endif
        
        private static void LoadEnumerable_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("all?", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.TrueForAll)
            );
            
            module.DefineLibraryMethod("any?", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.TrueForAny)
            );
            
            module.DefineLibraryMethod("collect", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.Map)
            );
            
            module.DefineLibraryMethod("detect", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object>>, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.Enumerable.Find)
            );
            
            module.DefineLibraryMethod("each_with_index", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.EachWithIndex)
            );
            
            module.DefineLibraryMethod("entries", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Enumerable.ToArray)
            );
            
            module.DefineLibraryMethod("find", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object>>, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.Enumerable.Find)
            );
            
            module.DefineLibraryMethod("find_all", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.Select)
            );
            
            module.DefineLibraryMethod("grep", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.Enumerable.Grep)
            );
            
            module.DefineLibraryMethod("include?", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.Enumerable.Contains)
            );
            
            module.DefineLibraryMethod("inject", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.Enumerable.Inject)
            );
            
            module.DefineLibraryMethod("map", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.Map)
            );
            
            module.DefineLibraryMethod("max", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.GetMaximum)
            );
            
            module.DefineLibraryMethod("member?", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.Enumerable.Contains)
            );
            
            module.DefineLibraryMethod("min", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.GetMinimum)
            );
            
            module.DefineLibraryMethod("partition", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.Partition)
            );
            
            module.DefineLibraryMethod("reject", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.Reject)
            );
            
            module.DefineLibraryMethod("select", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.Select)
            );
            
            module.DefineLibraryMethod("sort", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.Sort)
            );
            
            module.DefineLibraryMethod("sort_by", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Enumerable.SortBy)
            );
            
            module.DefineLibraryMethod("to_a", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Enumerable.ToArray)
            );
            
            module.DefineLibraryMethod("zip", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.ConversionStorage<System.Collections.IList>, IronRuby.Runtime.BlockParam, System.Object, System.Object[], System.Object>(IronRuby.Builtins.Enumerable.Zip)
            );
            
        }
        
        private static void Load__Singleton_EnvironmentSingletonOps_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("[]", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.EnvironmentSingletonOps.GetVariable)
            );
            
            module.DefineLibraryMethod("[]=", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.EnvironmentSingletonOps.SetVariable)
            );
            
            module.DefineLibraryMethod("clear", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.Clear)
            );
            
            module.DefineLibraryMethod("delete", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.Delete)
            );
            
            module.DefineLibraryMethod("delete_if", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.DeleteIf)
            );
            
            module.DefineLibraryMethod("each", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.Each)
            );
            
            module.DefineLibraryMethod("each_key", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.EachKey)
            );
            
            module.DefineLibraryMethod("each_pair", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.Each)
            );
            
            module.DefineLibraryMethod("each_value", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.EachValue)
            );
            
            module.DefineLibraryMethod("empty?", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Boolean>(IronRuby.Builtins.EnvironmentSingletonOps.IsEmpty)
            );
            
            module.DefineLibraryMethod("fetch", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.EnvironmentSingletonOps.GetVariable)
            );
            
            module.DefineLibraryMethod("has_key?", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.EnvironmentSingletonOps.HasKey)
            );
            
            module.DefineLibraryMethod("has_value?", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.EnvironmentSingletonOps.HasValue)
            );
            
            module.DefineLibraryMethod("include?", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.EnvironmentSingletonOps.HasKey)
            );
            
            module.DefineLibraryMethod("index", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.EnvironmentSingletonOps.Index)
            );
            
            module.DefineLibraryMethod("indexes", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.EnvironmentSingletonOps.Index)
            );
            
            module.DefineLibraryMethod("indices", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.EnvironmentSingletonOps.Indices)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.EnvironmentSingletonOps.Inspect)
            );
            
            module.DefineLibraryMethod("invert", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Hash>(IronRuby.Builtins.EnvironmentSingletonOps.Invert)
            );
            
            module.DefineLibraryMethod("key?", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.EnvironmentSingletonOps.HasKey)
            );
            
            module.DefineLibraryMethod("keys", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.EnvironmentSingletonOps.Keys)
            );
            
            module.DefineLibraryMethod("length", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32>(IronRuby.Builtins.EnvironmentSingletonOps.Length)
            );
            
            module.DefineLibraryMethod("rehash", 0x51, 
                new Func<System.Object, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.Rehash)
            );
            
            module.DefineLibraryMethod("reject!", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.DeleteIf)
            );
            
            module.DefineLibraryMethod("replace", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, System.Object, IronRuby.Builtins.Hash, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.Replace)
            );
            
            module.DefineLibraryMethod("shift", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.Shift)
            );
            
            module.DefineLibraryMethod("size", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32>(IronRuby.Builtins.EnvironmentSingletonOps.Length)
            );
            
            module.DefineLibraryMethod("store", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.EnvironmentSingletonOps.SetVariable)
            );
            
            module.DefineLibraryMethod("to_hash", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Hash>(IronRuby.Builtins.EnvironmentSingletonOps.ToHash)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.EnvironmentSingletonOps.ToString)
            );
            
            module.DefineLibraryMethod("update", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, System.Object, IronRuby.Builtins.Hash, System.Object>(IronRuby.Builtins.EnvironmentSingletonOps.Update)
            );
            
            module.DefineLibraryMethod("value?", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.EnvironmentSingletonOps.HasValue)
            );
            
            module.DefineLibraryMethod("values", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.EnvironmentSingletonOps.Values)
            );
            
            module.DefineLibraryMethod("values_at", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.EnvironmentSingletonOps.ValuesAt)
            );
            
        }
        
        private static void LoadException_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("backtrace", 0x51, 
                new Func<System.Exception, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ExceptionOps.GetBacktrace)
            );
            
            module.DefineRuleGenerator("exception", 0x51, IronRuby.Builtins.ExceptionOps.GetException());
            
            module.DefineLibraryMethod("initialize", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, System.Exception, System.Object, System.Exception>(IronRuby.Builtins.ExceptionOps.ReinitializeException)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, System.Exception, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ExceptionOps.Inspect)
            );
            
            module.DefineLibraryMethod("message", 0x51, 
                new Func<IronRuby.Runtime.UnaryOpStorage, System.Exception, System.Object>(IronRuby.Builtins.ExceptionOps.GetMessage)
            );
            
            module.DefineLibraryMethod("set_backtrace", 0x51, 
                new Func<System.Exception, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ExceptionOps.SetBacktrace), 
                new Func<System.Exception, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ExceptionOps.SetBacktrace)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<System.Exception, System.Object>(IronRuby.Builtins.ExceptionOps.StringRepresentation)
            );
            
            module.DefineLibraryMethod("to_str", 0x51, 
                new Func<System.Exception, System.Object>(IronRuby.Builtins.ExceptionOps.StringRepresentation)
            );
            
        }
        
        private static void LoadException_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineRuleGenerator("exception", 0x61, IronRuby.Builtins.ExceptionOps.CreateException());
            
        }
        
        private static void LoadFalseClass_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("&", 0x51, 
                new Func<System.Boolean, System.Object, System.Boolean>(IronRuby.Builtins.FalseClass.And)
            );
            
            module.DefineLibraryMethod("^", 0x51, 
                new Func<System.Boolean, System.Object, System.Boolean>(IronRuby.Builtins.FalseClass.Xor), 
                new Func<System.Boolean, System.Boolean, System.Boolean>(IronRuby.Builtins.FalseClass.Xor)
            );
            
            module.DefineLibraryMethod("|", 0x51, 
                new Func<System.Boolean, System.Object, System.Boolean>(IronRuby.Builtins.FalseClass.Or), 
                new Func<System.Boolean, System.Boolean, System.Boolean>(IronRuby.Builtins.FalseClass.Or)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<System.Boolean, IronRuby.Builtins.MutableString>(IronRuby.Builtins.FalseClass.ToString)
            );
            
        }
        
        private static void LoadFile_Constants(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetBuiltinConstant("ALT_SEPARATOR", IronRuby.Builtins.RubyFileOps.ALT_SEPARATOR);
            module.SetBuiltinConstant("PATH_SEPARATOR", IronRuby.Builtins.RubyFileOps.PATH_SEPARATOR);
            module.SetBuiltinConstant("Separator", IronRuby.Builtins.RubyFileOps.Separator);
            module.SetBuiltinConstant("SEPARATOR", IronRuby.Builtins.RubyFileOps.SEPARATOR);
            
        }
        
        private static void LoadFile_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("atime", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyFile, System.DateTime>(IronRuby.Builtins.RubyFileOps.AccessTime)
            );
            
            module.DefineLibraryMethod("chmod", 0x51, 
                new Func<IronRuby.Builtins.RubyFile, System.Int32, System.Int32>(IronRuby.Builtins.RubyFileOps.Chmod)
            );
            
            module.DefineLibraryMethod("chown", 0x51, 
                new Func<IronRuby.Builtins.RubyFile, System.Int32, System.Int32, System.Int32>(IronRuby.Builtins.RubyFileOps.ChangeOwner), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyFile, System.Object, System.Object, System.Int32>(IronRuby.Builtins.RubyFileOps.ChangeOwner)
            );
            
            module.DefineLibraryMethod("ctime", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyFile, System.DateTime>(IronRuby.Builtins.RubyFileOps.CreateTime)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyFile, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.Inspect)
            );
            
            module.DefineLibraryMethod("lstat", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyFile, System.IO.FileSystemInfo>(IronRuby.Builtins.RubyFileOps.Stat)
            );
            
            module.DefineLibraryMethod("mtime", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyFile, System.DateTime>(IronRuby.Builtins.RubyFileOps.ModifiedTime)
            );
            
            module.DefineLibraryMethod("path", 0x51, 
                new Func<IronRuby.Builtins.RubyFile, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.GetPath)
            );
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("truncate", 0x51, 
                new Func<IronRuby.Builtins.RubyFile, System.Int32, System.Int32>(IronRuby.Builtins.RubyFileOps.Truncate)
            );
            
            #endif
        }
        
        private static void LoadFile_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            LoadFileTest_Class(module);
            module.DefineLibraryMethod("atime", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.DateTime>(IronRuby.Builtins.RubyFileOps.AccessTime)
            );
            
            module.DefineLibraryMethod("basename", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.Basename)
            );
            
            module.DefineLibraryMethod("chmod", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyFileOps.Chmod)
            );
            
            module.DefineLibraryMethod("chown", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Int32, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyFileOps.ChangeOwner), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object, System.Object, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyFileOps.ChangeOwner)
            );
            
            module.DefineLibraryMethod("ctime", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.DateTime>(IronRuby.Builtins.RubyFileOps.CreateTime)
            );
            
            module.DefineLibraryMethod("delete", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyFileOps.Delete), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString[], System.Int32>(IronRuby.Builtins.RubyFileOps.Delete)
            );
            
            module.DefineLibraryMethod("dirname", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.DirName)
            );
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("expand_path", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.ExpandPath)
            );
            
            #endif
            module.DefineLibraryMethod("extname", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.GetExtension)
            );
            
            module.DefineLibraryMethod("fnmatch", 0x61, 
                new Func<System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, System.Boolean>(IronRuby.Builtins.RubyFileOps.FnMatch)
            );
            
            module.DefineLibraryMethod("fnmatch?", 0x61, 
                new Func<System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, System.Boolean>(IronRuby.Builtins.RubyFileOps.FnMatch)
            );
            
            module.DefineLibraryMethod("ftype", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.FileType)
            );
            
            module.DefineLibraryMethod("join", 0x61, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Builtins.RubyClass, System.Object[], IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.Join)
            );
            
            module.DefineLibraryMethod("lstat", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.IO.FileSystemInfo>(IronRuby.Builtins.RubyFileOps.Stat)
            );
            
            module.DefineLibraryMethod("mtime", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.DateTime>(IronRuby.Builtins.RubyFileOps.ModifiedTime)
            );
            
            module.DefineRuleGenerator("open", 0x61, IronRuby.Builtins.RubyFileOps.Open());
            
            module.DefineLibraryMethod("readlink", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.RubyFileOps.Readlink)
            );
            
            module.DefineLibraryMethod("rename", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyFileOps.Rename)
            );
            
            module.DefineLibraryMethod("split", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyFileOps.Split)
            );
            
            module.DefineLibraryMethod("stat", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.IO.FileSystemInfo>(IronRuby.Builtins.RubyFileOps.Stat)
            );
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("symlink", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyFileOps.SymLink)
            );
            
            #endif
            #if !SILVERLIGHT
            module.DefineLibraryMethod("truncate", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, System.Int32>(IronRuby.Builtins.RubyFileOps.Truncate)
            );
            
            #endif
            module.DefineLibraryMethod("umask", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Int32>(IronRuby.Builtins.RubyFileOps.GetUmask), 
                new Func<IronRuby.Builtins.RubyClass, System.Int32>(IronRuby.Builtins.RubyFileOps.GetUmask)
            );
            
            module.DefineLibraryMethod("unlink", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyFileOps.Delete), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString[], System.Int32>(IronRuby.Builtins.RubyFileOps.Delete)
            );
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("utime", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.DateTime, System.DateTime, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyFileOps.UpdateTimes), 
                new Func<IronRuby.Builtins.RubyClass, System.Object, System.Object, IronRuby.Builtins.MutableString[], System.Int32>(IronRuby.Builtins.RubyFileOps.UpdateTimes)
            );
            
            #endif
        }
        
        private static void LoadFile__Constants_Constants(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetBuiltinConstant("APPEND", IronRuby.Builtins.RubyFileOps.Constants.APPEND);
            module.SetBuiltinConstant("BINARY", IronRuby.Builtins.RubyFileOps.Constants.BINARY);
            module.SetBuiltinConstant("CREAT", IronRuby.Builtins.RubyFileOps.Constants.CREAT);
            module.SetBuiltinConstant("EXCL", IronRuby.Builtins.RubyFileOps.Constants.EXCL);
            module.SetBuiltinConstant("FNM_CASEFOLD", IronRuby.Builtins.RubyFileOps.Constants.FNM_CASEFOLD);
            module.SetBuiltinConstant("FNM_DOTMATCH", IronRuby.Builtins.RubyFileOps.Constants.FNM_DOTMATCH);
            module.SetBuiltinConstant("FNM_NOESCAPE", IronRuby.Builtins.RubyFileOps.Constants.FNM_NOESCAPE);
            module.SetBuiltinConstant("FNM_PATHNAME", IronRuby.Builtins.RubyFileOps.Constants.FNM_PATHNAME);
            module.SetBuiltinConstant("FNM_SYSCASE", IronRuby.Builtins.RubyFileOps.Constants.FNM_SYSCASE);
            module.SetBuiltinConstant("LOCK_EX", IronRuby.Builtins.RubyFileOps.Constants.LOCK_EX);
            module.SetBuiltinConstant("LOCK_NB", IronRuby.Builtins.RubyFileOps.Constants.LOCK_NB);
            module.SetBuiltinConstant("LOCK_SH", IronRuby.Builtins.RubyFileOps.Constants.LOCK_SH);
            module.SetBuiltinConstant("LOCK_UN", IronRuby.Builtins.RubyFileOps.Constants.LOCK_UN);
            module.SetBuiltinConstant("NONBLOCK", IronRuby.Builtins.RubyFileOps.Constants.NONBLOCK);
            module.SetBuiltinConstant("RDONLY", IronRuby.Builtins.RubyFileOps.Constants.RDONLY);
            module.SetBuiltinConstant("RDWR", IronRuby.Builtins.RubyFileOps.Constants.RDWR);
            module.SetBuiltinConstant("TRUNC", IronRuby.Builtins.RubyFileOps.Constants.TRUNC);
            module.SetBuiltinConstant("WRONLY", IronRuby.Builtins.RubyFileOps.Constants.WRONLY);
            
        }
        
        #if !SILVERLIGHT
        private static void LoadFile__Stat_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("<=>", 0x51, 
                new Func<System.IO.FileSystemInfo, System.IO.FileSystemInfo, System.Int32>(IronRuby.Builtins.RubyFileOps.RubyStatOps.Compare), 
                new Func<System.IO.FileSystemInfo, System.Object, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.Compare)
            );
            
            module.DefineLibraryMethod("atime", 0x51, 
                new Func<System.IO.FileSystemInfo, System.DateTime>(IronRuby.Builtins.RubyFileOps.RubyStatOps.AccessTime)
            );
            
            module.DefineLibraryMethod("blksize", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.BlockSize)
            );
            
            module.DefineLibraryMethod("blockdev?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsBlockDevice)
            );
            
            module.DefineLibraryMethod("blocks", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.Blocks)
            );
            
            module.DefineLibraryMethod("chardev?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsCharDevice)
            );
            
            module.DefineLibraryMethod("ctime", 0x51, 
                new Func<System.IO.FileSystemInfo, System.DateTime>(IronRuby.Builtins.RubyFileOps.RubyStatOps.CreateTime)
            );
            
            module.DefineLibraryMethod("dev", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.DeviceId)
            );
            
            module.DefineLibraryMethod("dev_major", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.DeviceIdMajor)
            );
            
            module.DefineLibraryMethod("dev_minor", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.DeviceIdMinor)
            );
            
            module.DefineLibraryMethod("directory?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsDirectory)
            );
            
            module.DefineLibraryMethod("executable?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsExecutable)
            );
            
            module.DefineLibraryMethod("executable_real?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsExecutable)
            );
            
            module.DefineLibraryMethod("file?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsFile)
            );
            
            module.DefineLibraryMethod("ftype", 0x51, 
                new Func<System.IO.FileSystemInfo, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.RubyStatOps.FileType)
            );
            
            module.DefineLibraryMethod("gid", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Int32>(IronRuby.Builtins.RubyFileOps.RubyStatOps.GroupId)
            );
            
            module.DefineLibraryMethod("grpowned?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsGroupOwned)
            );
            
            module.DefineLibraryMethod("ino", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Int32>(IronRuby.Builtins.RubyFileOps.RubyStatOps.Inode)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.IO.FileSystemInfo, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyFileOps.RubyStatOps.Inspect)
            );
            
            module.DefineLibraryMethod("mode", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Int32>(IronRuby.Builtins.RubyFileOps.RubyStatOps.Mode)
            );
            
            module.DefineLibraryMethod("mtime", 0x51, 
                new Func<System.IO.FileSystemInfo, System.DateTime>(IronRuby.Builtins.RubyFileOps.RubyStatOps.ModifiedTime)
            );
            
            module.DefineLibraryMethod("nlink", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Int32>(IronRuby.Builtins.RubyFileOps.RubyStatOps.NumberOfLinks)
            );
            
            module.DefineLibraryMethod("owned?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsUserOwned)
            );
            
            module.DefineLibraryMethod("pipe?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsPipe)
            );
            
            module.DefineLibraryMethod("rdev", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.DeviceId)
            );
            
            module.DefineLibraryMethod("rdev_major", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.DeviceIdMajor)
            );
            
            module.DefineLibraryMethod("rdev_minor", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.DeviceIdMinor)
            );
            
            module.DefineLibraryMethod("readable?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsReadable)
            );
            
            module.DefineLibraryMethod("readable_real?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsReadable)
            );
            
            module.DefineLibraryMethod("setgid?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsSetGid)
            );
            
            module.DefineLibraryMethod("setuid?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsSetUid)
            );
            
            module.DefineLibraryMethod("size", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Int32>(IronRuby.Builtins.RubyFileOps.RubyStatOps.Size)
            );
            
            module.DefineLibraryMethod("size?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Object>(IronRuby.Builtins.RubyFileOps.RubyStatOps.NullableSize)
            );
            
            module.DefineLibraryMethod("socket?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsSocket)
            );
            
            module.DefineLibraryMethod("sticky?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsSticky)
            );
            
            module.DefineLibraryMethod("symlink?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsSymLink)
            );
            
            module.DefineLibraryMethod("uid", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Int32>(IronRuby.Builtins.RubyFileOps.RubyStatOps.UserId)
            );
            
            module.DefineLibraryMethod("writable?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsWritable)
            );
            
            module.DefineLibraryMethod("writable_real?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsWritable)
            );
            
            module.DefineLibraryMethod("zero?", 0x51, 
                new Func<System.IO.FileSystemInfo, System.Boolean>(IronRuby.Builtins.RubyFileOps.RubyStatOps.IsZeroLength)
            );
            
        }
        #endif
        
        private static void LoadFileTest_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("blockdev?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsBlockDevice)
            );
            
            module.DefineLibraryMethod("chardev?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsCharDevice)
            );
            
            module.DefineLibraryMethod("directory?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsDirectory)
            );
            
            module.DefineLibraryMethod("executable?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsExecutable)
            );
            
            module.DefineLibraryMethod("executable_real?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsExecutable)
            );
            
            module.DefineLibraryMethod("exist?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.Exists)
            );
            
            module.DefineLibraryMethod("exists?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.Exists)
            );
            
            module.DefineLibraryMethod("file?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsAFile)
            );
            
            module.DefineLibraryMethod("grpowned?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsGroupOwned)
            );
            
            module.DefineLibraryMethod("owned?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsUserOwned)
            );
            
            module.DefineLibraryMethod("pipe?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsPipe)
            );
            
            module.DefineLibraryMethod("readable?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsReadable)
            );
            
            module.DefineLibraryMethod("readable_real?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsReadable)
            );
            
            module.DefineLibraryMethod("setgid?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsSetGid)
            );
            
            module.DefineLibraryMethod("setuid?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsSetUid)
            );
            
            module.DefineLibraryMethod("size", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.FileTest.Size)
            );
            
            module.DefineLibraryMethod("size?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.FileTest.NullableSize)
            );
            
            module.DefineLibraryMethod("socket?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsSocket)
            );
            
            module.DefineLibraryMethod("sticky?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsSticky)
            );
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("symlink?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsSymLink)
            );
            
            #endif
            module.DefineLibraryMethod("writable?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsWritable)
            );
            
            module.DefineLibraryMethod("writable_real?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsWritable)
            );
            
            module.DefineLibraryMethod("zero?", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.FileTest.IsZeroLength)
            );
            
        }
        
        private static void LoadFixnum_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            LoadIronRuby__Clr__Integer_Instance(module);
            module.DefineLibraryMethod("id2name", 0x51, 
                new Func<System.Int32, System.Object>(IronRuby.Builtins.Int32Ops.Id2Name)
            );
            
            module.DefineLibraryMethod("size", 0x51, 
                new Func<System.Int32, System.Int32>(IronRuby.Builtins.Int32Ops.Size)
            );
            
            module.DefineLibraryMethod("to_sym", 0x51, 
                new Func<System.Int32, System.Object>(IronRuby.Builtins.Int32Ops.ToSymbol)
            );
            
        }
        
        private static void LoadFixnum_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.UndefineMethodNoEvent("new");
            module.DefineLibraryMethod("induced_from", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Int32>(IronRuby.Builtins.Int32Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, System.Double, System.Int32>(IronRuby.Builtins.Int32Ops.InducedFrom)
            );
            
        }
        
        private static void LoadFloat_Constants(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetBuiltinConstant("DIG", IronRuby.Builtins.FloatOps.DIG);
            module.SetBuiltinConstant("EPSILON", IronRuby.Builtins.FloatOps.EPSILON);
            module.SetBuiltinConstant("MANT_DIG", IronRuby.Builtins.FloatOps.MANT_DIG);
            module.SetBuiltinConstant("MAX", IronRuby.Builtins.FloatOps.MAX);
            module.SetBuiltinConstant("MAX_10_EXP", IronRuby.Builtins.FloatOps.MAX_10_EXP);
            module.SetBuiltinConstant("MAX_EXP", IronRuby.Builtins.FloatOps.MAX_EXP);
            module.SetBuiltinConstant("MIN", IronRuby.Builtins.FloatOps.MIN);
            module.SetBuiltinConstant("MIN_10_EXP", IronRuby.Builtins.FloatOps.MIN_10_EXP);
            module.SetBuiltinConstant("MIN_EXP", IronRuby.Builtins.FloatOps.MIN_EXP);
            module.SetBuiltinConstant("RADIX", IronRuby.Builtins.FloatOps.RADIX);
            module.SetBuiltinConstant("ROUNDS", IronRuby.Builtins.FloatOps.ROUNDS);
            
        }
        
        private static void LoadFloat_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            LoadIronRuby__Clr__Float_Instance(module);
        }
        
        private static void LoadFloat_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            LoadIronRuby__Clr__Float_Class(module);
            module.UndefineMethodNoEvent("new");
        }
        
        private static void LoadGC_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("garbage_collect", 0x51, 
                new Action<System.Object>(IronRuby.Builtins.RubyGC.GarbageCollect)
            );
            
        }
        
        private static void LoadGC_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("disable", 0x61, 
                new Func<System.Object, System.Boolean>(IronRuby.Builtins.RubyGC.Disable)
            );
            
            module.DefineLibraryMethod("enable", 0x61, 
                new Func<System.Object, System.Boolean>(IronRuby.Builtins.RubyGC.Enable)
            );
            
            module.DefineLibraryMethod("start", 0x61, 
                new Action<System.Object>(IronRuby.Builtins.RubyGC.GarbageCollect)
            );
            
        }
        
        private static void LoadHash_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            LoadSystem__Collections__Generic__IDictionary_Instance(module);
            module.DefineLibraryMethod("[]", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.Hash, System.Object, System.Object>>, IronRuby.Builtins.Hash, System.Object, System.Object>(IronRuby.Builtins.HashOps.GetElement)
            );
            
            module.DefineLibraryMethod("default", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.Hash, System.Object>(IronRuby.Builtins.HashOps.GetDefaultValue), 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.Proc, IronRuby.Builtins.Hash, System.Object, System.Object>>, IronRuby.Builtins.Hash, System.Object, System.Object>(IronRuby.Builtins.HashOps.GetDefaultValue)
            );
            
            module.DefineLibraryMethod("default_proc", 0x51, 
                new Func<IronRuby.Builtins.Hash, IronRuby.Builtins.Proc>(IronRuby.Builtins.HashOps.GetDefaultProc)
            );
            
            module.DefineLibraryMethod("default=", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.Hash, System.Object, System.Object>(IronRuby.Builtins.HashOps.SetDefaultValue)
            );
            
            module.DefineLibraryMethod("initialize", 0x52, 
                new Func<IronRuby.Builtins.Hash, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.Initialize), 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.Hash, System.Object, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.Initialize), 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.Hash, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.Initialize)
            );
            
            module.DefineLibraryMethod("initialize_copy", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.Hash, IronRuby.Builtins.Hash, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.InitializeCopy)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.Hash, IronRuby.Builtins.MutableString>(IronRuby.Builtins.HashOps.Inspect)
            );
            
            module.DefineLibraryMethod("replace", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.Hash, System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.Replace)
            );
            
            module.DefineLibraryMethod("shift", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.Hash, System.Object, System.Object>>, IronRuby.Builtins.Hash, System.Object>(IronRuby.Builtins.HashOps.Shift)
            );
            
        }
        
        private static void LoadHash_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("[]", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.CreateSubclass), 
                new Func<IronRuby.Builtins.RubyClass, System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.CreateSubclass), 
                new Func<IronRuby.Builtins.RubyClass, System.Object[], IronRuby.Builtins.Hash>(IronRuby.Builtins.HashOps.CreateSubclass)
            );
            
        }
        
        private static void LoadInteger_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("ceil", 0x51, 
                new Func<System.Object, System.Object>(IronRuby.Builtins.Integer.ToInteger)
            );
            
            module.DefineLibraryMethod("chr", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.Integer.ToChr)
            );
            
            module.DefineLibraryMethod("downto", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, System.Int32, System.Int32, System.Object>(IronRuby.Builtins.Integer.DownTo), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.Integer.DownTo)
            );
            
            module.DefineLibraryMethod("floor", 0x51, 
                new Func<System.Object, System.Object>(IronRuby.Builtins.Integer.ToInteger)
            );
            
            module.DefineLibraryMethod("integer?", 0x51, 
                new Func<System.Object, System.Boolean>(IronRuby.Builtins.Integer.IsInteger)
            );
            
            module.DefineLibraryMethod("next", 0x51, 
                new Func<System.Int32, System.Object>(IronRuby.Builtins.Integer.Next), 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object>(IronRuby.Builtins.Integer.Next)
            );
            
            module.DefineLibraryMethod("round", 0x51, 
                new Func<System.Object, System.Object>(IronRuby.Builtins.Integer.ToInteger)
            );
            
            module.DefineLibraryMethod("succ", 0x51, 
                new Func<System.Int32, System.Object>(IronRuby.Builtins.Integer.Next), 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object>(IronRuby.Builtins.Integer.Next)
            );
            
            module.DefineLibraryMethod("times", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, System.Int32, System.Object>(IronRuby.Builtins.Integer.Times), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.Integer.Times)
            );
            
            module.DefineLibraryMethod("to_i", 0x51, 
                new Func<System.Object, System.Object>(IronRuby.Builtins.Integer.ToInteger)
            );
            
            module.DefineLibraryMethod("to_int", 0x51, 
                new Func<System.Object, System.Object>(IronRuby.Builtins.Integer.ToInteger)
            );
            
            module.DefineLibraryMethod("truncate", 0x51, 
                new Func<System.Object, System.Object>(IronRuby.Builtins.Integer.ToInteger)
            );
            
            module.DefineLibraryMethod("upto", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, System.Int32, System.Int32, System.Object>(IronRuby.Builtins.Integer.UpTo), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.Integer.UpTo)
            );
            
        }
        
        private static void LoadInteger_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("induced_from", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Object>(IronRuby.Builtins.Integer.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.Integer.InducedFrom), 
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Builtins.RubyClass, System.Double, System.Object>(IronRuby.Builtins.Integer.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, System.Object, System.Int32>(IronRuby.Builtins.Integer.InducedFrom)
            );
            
        }
        
        private static void LoadIO_Constants(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetBuiltinConstant("SEEK_CUR", IronRuby.Builtins.RubyIOOps.SEEK_CUR);
            module.SetBuiltinConstant("SEEK_END", IronRuby.Builtins.RubyIOOps.SEEK_END);
            module.SetBuiltinConstant("SEEK_SET", IronRuby.Builtins.RubyIOOps.SEEK_SET);
            
        }
        
        private static void LoadIO_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            LoadIronRuby__Print_Instance(module);
            module.DefineLibraryMethod("binmode", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Binmode)
            );
            
            module.DefineLibraryMethod("close", 0x51, 
                new Action<IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Close)
            );
            
            module.DefineLibraryMethod("close_read", 0x51, 
                new Action<IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.CloseReader)
            );
            
            module.DefineLibraryMethod("close_write", 0x51, 
                new Action<IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.CloseWriter)
            );
            
            module.DefineLibraryMethod("closed?", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Boolean>(IronRuby.Builtins.RubyIOOps.Closed)
            );
            
            module.DefineLibraryMethod("each", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyIO, System.Object>(IronRuby.Builtins.RubyIOOps.Each), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyIOOps.Each)
            );
            
            module.DefineLibraryMethod("each_byte", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyIO, System.Object>(IronRuby.Builtins.RubyIOOps.EachByte)
            );
            
            module.DefineLibraryMethod("each_line", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyIO, System.Object>(IronRuby.Builtins.RubyIOOps.Each), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyIOOps.Each)
            );
            
            module.DefineLibraryMethod("eof", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Boolean>(IronRuby.Builtins.RubyIOOps.Eof)
            );
            
            module.DefineLibraryMethod("eof?", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Boolean>(IronRuby.Builtins.RubyIOOps.Eof)
            );
            
            module.DefineLibraryMethod("external_encoding", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Builtins.RubyEncoding>(IronRuby.Builtins.RubyIOOps.GetExternalEncoding)
            );
            
            module.DefineLibraryMethod("fcntl", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Int32, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyIOOps.FileControl), 
                new Func<IronRuby.Builtins.RubyIO, System.Int32, System.Int32, System.Int32>(IronRuby.Builtins.RubyIOOps.FileControl)
            );
            
            module.DefineLibraryMethod("fileno", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Int32>(IronRuby.Builtins.RubyIOOps.FileNo)
            );
            
            module.DefineLibraryMethod("flush", 0x51, 
                new Action<IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Flush)
            );
            
            module.DefineLibraryMethod("fsync", 0x51, 
                new Action<IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Flush)
            );
            
            module.DefineLibraryMethod("getc", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Object>(IronRuby.Builtins.RubyIOOps.Getc)
            );
            
            module.DefineLibraryMethod("gets", 0x51, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.Gets), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.Gets)
            );
            
            module.DefineLibraryMethod("initialize", 0x52, 
                new Func<IronRuby.Builtins.RubyIO, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Reinitialize), 
                new Func<IronRuby.Builtins.RubyIO, System.Int32, System.Int32, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Reinitialize)
            );
            
            module.DefineLibraryMethod("initialize_copy", 0x52, 
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Builtins.RubyIO, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.InitializeCopy)
            );
            
            module.DefineLibraryMethod("internal_encoding", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Builtins.RubyEncoding>(IronRuby.Builtins.RubyIOOps.GetInternalEncoding)
            );
            
            module.DefineLibraryMethod("ioctl", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Int32, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyIOOps.FileControl), 
                new Func<IronRuby.Builtins.RubyIO, System.Int32, System.Int32, System.Int32>(IronRuby.Builtins.RubyIOOps.FileControl)
            );
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("isatty", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Boolean>(IronRuby.Builtins.RubyIOOps.IsAtty)
            );
            
            #endif
            module.DefineLibraryMethod("lineno", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Int32>(IronRuby.Builtins.RubyIOOps.GetLineNumber)
            );
            
            module.DefineLibraryMethod("lineno=", 0x51, 
                new Action<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyIO, System.Int32>(IronRuby.Builtins.RubyIOOps.SetLineNumber)
            );
            
            module.DefineLibraryMethod("pid", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Object>(IronRuby.Builtins.RubyIOOps.Pid)
            );
            
            module.DefineLibraryMethod("pos", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Object>(IronRuby.Builtins.RubyIOOps.Pos)
            );
            
            module.DefineLibraryMethod("pos=", 0x51, 
                new Action<IronRuby.Builtins.RubyIO, IronRuby.Runtime.IntegerValue>(IronRuby.Builtins.RubyIOOps.Pos)
            );
            
            module.DefineLibraryMethod("read", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.Read), 
                new Func<IronRuby.Builtins.RubyIO, Microsoft.Scripting.Runtime.DynamicNull, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.Read), 
                new Func<IronRuby.Builtins.RubyIO, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.Read)
            );
            
            module.DefineLibraryMethod("read_nonblock", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.ReadNoBlock)
            );
            
            module.DefineLibraryMethod("readchar", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Int32>(IronRuby.Builtins.RubyIOOps.ReadChar)
            );
            
            module.DefineLibraryMethod("readline", 0x51, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.ReadLine), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.ReadLine)
            );
            
            module.DefineLibraryMethod("readlines", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyIO, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyIOOps.ReadLines), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyIOOps.ReadLines)
            );
            
            module.DefineLibraryMethod("reopen", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Reopen), 
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Reopen), 
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Builtins.RubyIO, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Reopen)
            );
            
            module.DefineLibraryMethod("rewind", 0x51, 
                new Action<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.Rewind)
            );
            
            module.DefineLibraryMethod("seek", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Runtime.IntegerValue, System.Int32, System.Int32>(IronRuby.Builtins.RubyIOOps.Seek)
            );
            
            module.DefineLibraryMethod("sync", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Boolean>(IronRuby.Builtins.RubyIOOps.Sync)
            );
            
            module.DefineLibraryMethod("sync=", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Boolean, System.Boolean>(IronRuby.Builtins.RubyIOOps.Sync)
            );
            
            module.DefineLibraryMethod("sysread", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.SystemRead)
            );
            
            module.DefineLibraryMethod("sysseek", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Runtime.IntegerValue, System.Int32, System.Object>(IronRuby.Builtins.RubyIOOps.SysSeek)
            );
            
            module.DefineLibraryMethod("syswrite", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyIOOps.SysWrite), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyIO, System.Object, System.Int32>(IronRuby.Builtins.RubyIOOps.SysWrite)
            );
            
            module.DefineLibraryMethod("tell", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Object>(IronRuby.Builtins.RubyIOOps.Pos)
            );
            
            module.DefineLibraryMethod("to_i", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Int32>(IronRuby.Builtins.RubyIOOps.FileNo)
            );
            
            module.DefineLibraryMethod("to_io", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.ToIO)
            );
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("tty?", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, System.Boolean>(IronRuby.Builtins.RubyIOOps.IsAtty)
            );
            
            #endif
            module.DefineLibraryMethod("ungetc", 0x51, 
                new Action<IronRuby.Builtins.RubyIO, System.Int32>(IronRuby.Builtins.RubyIOOps.SetPreviousByte)
            );
            
            module.DefineLibraryMethod("write", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyIOOps.Write), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Builtins.RubyIO, System.Object, System.Int32>(IronRuby.Builtins.RubyIOOps.Write)
            );
            
            module.DefineLibraryMethod("write_nonblock", 0x51, 
                new Func<IronRuby.Builtins.RubyIO, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.RubyIOOps.WriteNoBlock), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Builtins.RubyIO, System.Object, System.Int32>(IronRuby.Builtins.RubyIOOps.WriteNoBlock)
            );
            
        }
        
        private static void LoadIO_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineRuleGenerator("for_fd", 0x61, IronRuby.Builtins.RubyIOOps.ForFileDescriptor());
            
            module.DefineLibraryMethod("foreach", 0x61, 
                new Action<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.ForEach), 
                new Action<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.ForEach)
            );
            
            module.DefineRuleGenerator("open", 0x61, IronRuby.Builtins.RubyIOOps.Open());
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("pipe", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyIOOps.OpenPipe)
            );
            
            #endif
            #if !SILVERLIGHT
            module.DefineLibraryMethod("popen", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyIOOps.OpenPipe), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.RubyIOOps.OpenPipe)
            );
            
            #endif
            module.DefineLibraryMethod("read", 0x61, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyIOOps.Read)
            );
            
            module.DefineLibraryMethod("readlines", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyIOOps.ReadLines), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyIOOps.ReadLines)
            );
            
            module.DefineLibraryMethod("select", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyIOOps.Select), 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyIOOps.Select), 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, System.Double, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyIOOps.Select)
            );
            
        }
        
        private static void LoadIronRuby_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("configuration", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, Microsoft.Scripting.Runtime.DlrConfiguration>(IronRuby.Builtins.IronRubyOps.GetConfiguration)
            );
            
            module.DefineLibraryMethod("globals", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, Microsoft.Scripting.Runtime.Scope>(IronRuby.Builtins.IronRubyOps.GetGlobalScope)
            );
            
            module.DefineLibraryMethod("load", 0x61, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.IronRubyOps.Load)
            );
            
            module.DefineLibraryMethod("loaded_assemblies", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IronRubyOps.GetLoadedAssemblies)
            );
            
            module.DefineLibraryMethod("loaded_scripts", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, System.Collections.Generic.IDictionary<System.String, Microsoft.Scripting.Runtime.Scope>>(IronRuby.Builtins.IronRubyOps.GetLoadedScripts)
            );
            
            module.DefineLibraryMethod("require", 0x61, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.IronRubyOps.Require)
            );
            
        }
        
        private static void LoadIronRuby__Clr_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("profile", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Hash>(IronRuby.Builtins.IronRubyOps.ClrOps.GetProfile), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.IronRubyOps.ClrOps.GetProfile)
            );
            
        }
        
        private static void LoadIronRuby__Clr__BigInteger_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("-", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrBigInteger.Subtract), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.ClrBigInteger.Subtract), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, System.Object>(IronRuby.Builtins.ClrBigInteger.Subtract), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrBigInteger.Subtract)
            );
            
            module.DefineLibraryMethod("%", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrBigInteger.Modulo), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.ClrBigInteger.Modulo), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, System.Object>(IronRuby.Builtins.ClrBigInteger.Modulo), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrBigInteger.ModuloOp)
            );
            
            module.DefineLibraryMethod("&", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.ClrBigInteger.And), 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrBigInteger.And), 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, IronRuby.Runtime.IntegerValue, System.Object>(IronRuby.Builtins.ClrBigInteger.And)
            );
            
            module.DefineLibraryMethod("*", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrBigInteger.Multiply), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.ClrBigInteger.Multiply), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, System.Object>(IronRuby.Builtins.ClrBigInteger.Multiply), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrBigInteger.Multiply)
            );
            
            module.DefineLibraryMethod("**", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrBigInteger.Power), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.ClrBigInteger.Power), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, System.Object>(IronRuby.Builtins.ClrBigInteger.Power), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrBigInteger.Power)
            );
            
            module.DefineLibraryMethod("/", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrBigInteger.Divide), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.ClrBigInteger.Divide), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, System.Object>(IronRuby.Builtins.ClrBigInteger.DivideOp), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrBigInteger.Divide)
            );
            
            module.DefineLibraryMethod("-@", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrBigInteger.Negate)
            );
            
            module.DefineLibraryMethod("[]", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Int32>(IronRuby.Builtins.ClrBigInteger.Bit), 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Int32>(IronRuby.Builtins.ClrBigInteger.Bit)
            );
            
            module.DefineLibraryMethod("^", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.ClrBigInteger.Xor), 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrBigInteger.Xor), 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, IronRuby.Runtime.IntegerValue, System.Object>(IronRuby.Builtins.ClrBigInteger.Xor)
            );
            
            module.DefineLibraryMethod("|", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.ClrBigInteger.BitwiseOr), 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrBigInteger.BitwiseOr), 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, IronRuby.Runtime.IntegerValue, System.Object>(IronRuby.Builtins.ClrBigInteger.BitwiseOr)
            );
            
            module.DefineLibraryMethod("~", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrBigInteger.Invert)
            );
            
            module.DefineLibraryMethod("+", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrBigInteger.Add), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.ClrBigInteger.Add), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, System.Object>(IronRuby.Builtins.ClrBigInteger.Add), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrBigInteger.Add)
            );
            
            module.DefineLibraryMethod("<<", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.ClrBigInteger.LeftShift), 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrBigInteger.LeftShift), 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, IronRuby.Runtime.IntegerValue, System.Object>(IronRuby.Builtins.ClrBigInteger.LeftShift)
            );
            
            module.DefineLibraryMethod("<=>", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Int32>(IronRuby.Builtins.ClrBigInteger.Compare), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Int32>(IronRuby.Builtins.ClrBigInteger.Compare), 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Double, System.Object>(IronRuby.Builtins.ClrBigInteger.Compare), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, Microsoft.Scripting.Math.BigInteger, System.Object, System.Object>(IronRuby.Builtins.ClrBigInteger.Compare)
            );
            
            module.DefineLibraryMethod("==", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Boolean>(IronRuby.Builtins.ClrBigInteger.Equal), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Boolean>(IronRuby.Builtins.ClrBigInteger.Equal), 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Double, System.Boolean>(IronRuby.Builtins.ClrBigInteger.Equal), 
                new Func<IronRuby.Runtime.BinaryOpStorage, Microsoft.Scripting.Math.BigInteger, System.Object, System.Boolean>(IronRuby.Builtins.ClrBigInteger.Equal)
            );
            
            module.DefineLibraryMethod(">>", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.ClrBigInteger.RightShift), 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrBigInteger.RightShift), 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, IronRuby.Runtime.IntegerValue, System.Object>(IronRuby.Builtins.ClrBigInteger.RightShift)
            );
            
            module.DefineLibraryMethod("abs", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrBigInteger.Abs)
            );
            
            module.DefineLibraryMethod("coerce", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ClrBigInteger.Coerce), 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ClrBigInteger.Coerce)
            );
            
            module.DefineLibraryMethod("div", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrBigInteger.Divide), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.ClrBigInteger.Divide), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, System.Object>(IronRuby.Builtins.ClrBigInteger.Divide), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrBigInteger.Div)
            );
            
            module.DefineLibraryMethod("divmod", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ClrBigInteger.DivMod), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ClrBigInteger.DivMod), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ClrBigInteger.DivMod), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrBigInteger.DivMod)
            );
            
            module.DefineLibraryMethod("eql?", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Boolean>(IronRuby.Builtins.ClrBigInteger.Eql), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Boolean>(IronRuby.Builtins.ClrBigInteger.Eql), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Object, System.Boolean>(IronRuby.Builtins.ClrBigInteger.Eql)
            );
            
            module.DefineLibraryMethod("hash", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32>(IronRuby.Builtins.ClrBigInteger.Hash)
            );
            
            module.DefineLibraryMethod("modulo", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrBigInteger.Modulo), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.ClrBigInteger.Modulo), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, System.Object>(IronRuby.Builtins.ClrBigInteger.Modulo), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrBigInteger.Modulo)
            );
            
            module.DefineLibraryMethod("quo", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrBigInteger.Quotient), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.ClrBigInteger.Quotient), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, System.Object>(IronRuby.Builtins.ClrBigInteger.Quotient), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrBigInteger.Quotient)
            );
            
            module.DefineLibraryMethod("remainder", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrBigInteger.Remainder), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.ClrBigInteger.Remainder), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Double, System.Double>(IronRuby.Builtins.ClrBigInteger.Remainder), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrBigInteger.Remainder)
            );
            
            module.DefineLibraryMethod("to_f", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Math.BigInteger, System.Double>(IronRuby.Builtins.ClrBigInteger.ToFloat)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<Microsoft.Scripting.Math.BigInteger, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ClrBigInteger.ToString), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ClrBigInteger.ToString)
            );
            
        }
        
        private static void LoadIronRuby__Clr__FlagEnumeration_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("&", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.FlagEnumOps.BitwiseAnd)
            );
            
            module.DefineLibraryMethod("^", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.FlagEnumOps.Xor)
            );
            
            module.DefineLibraryMethod("|", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.FlagEnumOps.BitwiseOr)
            );
            
            module.DefineLibraryMethod("~", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.FlagEnumOps.OnesComplement)
            );
            
        }
        
        private static void LoadIronRuby__Clr__Float_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("-", 0x51, 
                new Func<System.Double, System.Int32, System.Double>(IronRuby.Builtins.ClrFloat.Subtract), 
                new Func<IronRuby.Runtime.RubyContext, System.Double, Microsoft.Scripting.Math.BigInteger, System.Double>(IronRuby.Builtins.ClrFloat.Subtract), 
                new Func<System.Double, System.Double, System.Double>(IronRuby.Builtins.ClrFloat.Subtract), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Double, System.Object, System.Object>(IronRuby.Builtins.ClrFloat.Subtract)
            );
            
            module.DefineLibraryMethod("%", 0x51, 
                new Func<System.Double, System.Int32, System.Double>(IronRuby.Builtins.ClrFloat.Modulo), 
                new Func<IronRuby.Runtime.RubyContext, System.Double, Microsoft.Scripting.Math.BigInteger, System.Double>(IronRuby.Builtins.ClrFloat.Modulo), 
                new Func<System.Double, System.Double, System.Double>(IronRuby.Builtins.ClrFloat.Modulo), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Double, System.Object, System.Object>(IronRuby.Builtins.ClrFloat.ModuloOp)
            );
            
            module.DefineLibraryMethod("*", 0x51, 
                new Func<System.Double, System.Int32, System.Double>(IronRuby.Builtins.ClrFloat.Multiply), 
                new Func<IronRuby.Runtime.RubyContext, System.Double, Microsoft.Scripting.Math.BigInteger, System.Double>(IronRuby.Builtins.ClrFloat.Multiply), 
                new Func<System.Double, System.Double, System.Double>(IronRuby.Builtins.ClrFloat.Multiply), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Double, System.Object, System.Object>(IronRuby.Builtins.ClrFloat.Multiply)
            );
            
            module.DefineLibraryMethod("**", 0x51, 
                new Func<System.Double, System.Int32, System.Double>(IronRuby.Builtins.ClrFloat.Power), 
                new Func<IronRuby.Runtime.RubyContext, System.Double, Microsoft.Scripting.Math.BigInteger, System.Double>(IronRuby.Builtins.ClrFloat.Power), 
                new Func<System.Double, System.Double, System.Double>(IronRuby.Builtins.ClrFloat.Power), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Double, System.Object, System.Object>(IronRuby.Builtins.ClrFloat.Power)
            );
            
            module.DefineLibraryMethod("/", 0x51, 
                new Func<System.Double, System.Int32, System.Double>(IronRuby.Builtins.ClrFloat.Divide), 
                new Func<IronRuby.Runtime.RubyContext, System.Double, Microsoft.Scripting.Math.BigInteger, System.Double>(IronRuby.Builtins.ClrFloat.Divide), 
                new Func<System.Double, System.Double, System.Double>(IronRuby.Builtins.ClrFloat.Divide), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Double, System.Object, System.Object>(IronRuby.Builtins.ClrFloat.Divide)
            );
            
            module.DefineLibraryMethod("+", 0x51, 
                new Func<System.Double, System.Int32, System.Double>(IronRuby.Builtins.ClrFloat.Add), 
                new Func<IronRuby.Runtime.RubyContext, System.Double, Microsoft.Scripting.Math.BigInteger, System.Double>(IronRuby.Builtins.ClrFloat.Add), 
                new Func<System.Double, System.Double, System.Double>(IronRuby.Builtins.ClrFloat.Add), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Double, System.Object, System.Object>(IronRuby.Builtins.ClrFloat.Add)
            );
            
            module.DefineLibraryMethod("<", 0x51, 
                new Func<System.Double, System.Double, System.Boolean>(IronRuby.Builtins.ClrFloat.LessThan), 
                new Func<System.Double, System.Int32, System.Boolean>(IronRuby.Builtins.ClrFloat.LessThan), 
                new Func<IronRuby.Runtime.RubyContext, System.Double, Microsoft.Scripting.Math.BigInteger, System.Boolean>(IronRuby.Builtins.ClrFloat.LessThan), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Double, System.Object, System.Boolean>(IronRuby.Builtins.ClrFloat.LessThan)
            );
            
            module.DefineLibraryMethod("<=", 0x51, 
                new Func<System.Double, System.Double, System.Boolean>(IronRuby.Builtins.ClrFloat.LessThanOrEqual), 
                new Func<System.Double, System.Int32, System.Boolean>(IronRuby.Builtins.ClrFloat.LessThanOrEqual), 
                new Func<IronRuby.Runtime.RubyContext, System.Double, Microsoft.Scripting.Math.BigInteger, System.Boolean>(IronRuby.Builtins.ClrFloat.LessThanOrEqual), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Double, System.Object, System.Boolean>(IronRuby.Builtins.ClrFloat.LessThanOrEqual)
            );
            
            module.DefineLibraryMethod("<=>", 0x51, 
                new Func<System.Double, System.Double, System.Object>(IronRuby.Builtins.ClrFloat.Compare), 
                new Func<System.Double, System.Int32, System.Object>(IronRuby.Builtins.ClrFloat.Compare), 
                new Func<IronRuby.Runtime.RubyContext, System.Double, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrFloat.Compare), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Double, System.Object, System.Object>(IronRuby.Builtins.ClrFloat.Compare)
            );
            
            module.DefineLibraryMethod("==", 0x51, 
                new Func<System.Double, System.Double, System.Boolean>(IronRuby.Builtins.ClrFloat.Equal), 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Double, System.Object, System.Boolean>(IronRuby.Builtins.ClrFloat.Equal)
            );
            
            module.DefineLibraryMethod(">", 0x51, 
                new Func<System.Double, System.Double, System.Boolean>(IronRuby.Builtins.ClrFloat.GreaterThan), 
                new Func<System.Double, System.Int32, System.Boolean>(IronRuby.Builtins.ClrFloat.GreaterThan), 
                new Func<IronRuby.Runtime.RubyContext, System.Double, Microsoft.Scripting.Math.BigInteger, System.Boolean>(IronRuby.Builtins.ClrFloat.GreaterThan), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Double, System.Object, System.Boolean>(IronRuby.Builtins.ClrFloat.GreaterThan)
            );
            
            module.DefineLibraryMethod(">=", 0x51, 
                new Func<System.Double, System.Double, System.Boolean>(IronRuby.Builtins.ClrFloat.GreaterThanOrEqual), 
                new Func<System.Double, System.Int32, System.Boolean>(IronRuby.Builtins.ClrFloat.GreaterThanOrEqual), 
                new Func<IronRuby.Runtime.RubyContext, System.Double, Microsoft.Scripting.Math.BigInteger, System.Boolean>(IronRuby.Builtins.ClrFloat.GreaterThanOrEqual), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Double, System.Object, System.Boolean>(IronRuby.Builtins.ClrFloat.GreaterThanOrEqual)
            );
            
            module.DefineLibraryMethod("abs", 0x51, 
                new Func<System.Double, System.Double>(IronRuby.Builtins.ClrFloat.Abs)
            );
            
            module.DefineLibraryMethod("ceil", 0x51, 
                new Func<System.Double, System.Object>(IronRuby.Builtins.ClrFloat.Ceil)
            );
            
            module.DefineLibraryMethod("coerce", 0x51, 
                new Func<System.Double, System.Double, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ClrFloat.Coerce)
            );
            
            module.DefineLibraryMethod("divmod", 0x51, 
                new Func<System.Double, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ClrFloat.DivMod), 
                new Func<IronRuby.Runtime.RubyContext, System.Double, Microsoft.Scripting.Math.BigInteger, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ClrFloat.DivMod), 
                new Func<System.Double, System.Double, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ClrFloat.DivMod), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Double, System.Object, System.Object>(IronRuby.Builtins.ClrFloat.DivMod)
            );
            
            module.DefineLibraryMethod("finite?", 0x51, 
                new Func<System.Double, System.Boolean>(IronRuby.Builtins.ClrFloat.IsFinite)
            );
            
            module.DefineLibraryMethod("floor", 0x51, 
                new Func<System.Double, System.Object>(IronRuby.Builtins.ClrFloat.Floor)
            );
            
            module.DefineLibraryMethod("hash", 0x51, 
                new Func<System.Double, System.Int32>(IronRuby.Builtins.ClrFloat.Hash)
            );
            
            module.DefineLibraryMethod("infinite?", 0x51, 
                new Func<System.Double, System.Object>(IronRuby.Builtins.ClrFloat.IsInfinite)
            );
            
            module.DefineLibraryMethod("modulo", 0x51, 
                new Func<System.Double, System.Int32, System.Double>(IronRuby.Builtins.ClrFloat.Modulo), 
                new Func<IronRuby.Runtime.RubyContext, System.Double, Microsoft.Scripting.Math.BigInteger, System.Double>(IronRuby.Builtins.ClrFloat.Modulo), 
                new Func<System.Double, System.Double, System.Double>(IronRuby.Builtins.ClrFloat.Modulo), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Double, System.Object, System.Object>(IronRuby.Builtins.ClrFloat.Modulo)
            );
            
            module.DefineLibraryMethod("nan?", 0x51, 
                new Func<System.Double, System.Boolean>(IronRuby.Builtins.ClrFloat.IsNan)
            );
            
            module.DefineLibraryMethod("round", 0x51, 
                new Func<System.Double, System.Object>(IronRuby.Builtins.ClrFloat.Round)
            );
            
            module.DefineLibraryMethod("to_f", 0x51, 
                new Func<System.Double, System.Double>(IronRuby.Builtins.ClrFloat.ToFloat)
            );
            
            module.DefineLibraryMethod("to_i", 0x51, 
                new Func<System.Double, System.Object>(IronRuby.Builtins.ClrFloat.ToInt)
            );
            
            module.DefineLibraryMethod("to_int", 0x51, 
                new Func<System.Double, System.Object>(IronRuby.Builtins.ClrFloat.ToInt)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Double, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ClrFloat.ToS)
            );
            
            module.DefineLibraryMethod("truncate", 0x51, 
                new Func<System.Double, System.Object>(IronRuby.Builtins.ClrFloat.ToInt)
            );
            
            module.DefineLibraryMethod("zero?", 0x51, 
                new Func<System.Double, System.Boolean>(IronRuby.Builtins.ClrFloat.IsZero)
            );
            
        }
        
        private static void LoadIronRuby__Clr__Float_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("induced_from", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, System.Double, System.Double>(IronRuby.Builtins.ClrFloat.InducedFrom), 
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Builtins.RubyModule, System.Int32, System.Object>(IronRuby.Builtins.ClrFloat.InducedFrom), 
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Builtins.RubyModule, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrFloat.InducedFrom), 
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Double>(IronRuby.Builtins.ClrFloat.InducedFrom)
            );
            
        }
        
        private static void LoadIronRuby__Clr__Integer_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("-", 0x51, 
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.ClrInteger.Subtract), 
                new Func<System.Int32, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrInteger.Subtract), 
                new Func<System.Int32, System.Double, System.Double>(IronRuby.Builtins.ClrInteger.Subtract), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrInteger.Subtract)
            );
            
            module.DefineLibraryMethod("%", 0x51, 
                new Func<System.Int32, System.Int32, System.Int32>(IronRuby.Builtins.ClrInteger.Modulo), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrInteger.ModuloOp)
            );
            
            module.DefineLibraryMethod("&", 0x51, 
                new Func<System.Int32, System.Int32, System.Int32>(IronRuby.Builtins.ClrInteger.BitwiseAnd), 
                new Func<System.Int32, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrInteger.BitwiseAnd), 
                new Func<IronRuby.Runtime.RubyContext, System.Int32, IronRuby.Runtime.IntegerValue, System.Object>(IronRuby.Builtins.ClrInteger.BitwiseAnd)
            );
            
            module.DefineLibraryMethod("*", 0x51, 
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.ClrInteger.Multiply), 
                new Func<System.Int32, Microsoft.Scripting.Math.BigInteger, Microsoft.Scripting.Math.BigInteger>(IronRuby.Builtins.ClrInteger.Multiply), 
                new Func<System.Int32, System.Double, System.Double>(IronRuby.Builtins.ClrInteger.Multiply), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrInteger.Multiply)
            );
            
            module.DefineLibraryMethod("**", 0x51, 
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.ClrInteger.Power), 
                new Func<System.Int32, System.Double, System.Double>(IronRuby.Builtins.ClrInteger.Power), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Int32, System.Object, System.Object>(IronRuby.Builtins.ClrInteger.Power)
            );
            
            module.DefineLibraryMethod("/", 0x51, 
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.ClrInteger.Divide), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrInteger.DivideOp)
            );
            
            module.DefineLibraryMethod("-@", 0x51, 
                new Func<System.Int32, System.Object>(IronRuby.Builtins.ClrInteger.Minus)
            );
            
            module.DefineLibraryMethod("[]", 0x51, 
                new Func<System.Int32, System.Int32, System.Int32>(IronRuby.Builtins.ClrInteger.Bit), 
                new Func<System.Int32, Microsoft.Scripting.Math.BigInteger, System.Int32>(IronRuby.Builtins.ClrInteger.Bit)
            );
            
            module.DefineLibraryMethod("^", 0x51, 
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.ClrInteger.BitwiseXor), 
                new Func<System.Int32, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrInteger.BitwiseXor), 
                new Func<IronRuby.Runtime.RubyContext, System.Int32, IronRuby.Runtime.IntegerValue, System.Object>(IronRuby.Builtins.ClrInteger.BitwiseXor)
            );
            
            module.DefineLibraryMethod("|", 0x51, 
                new Func<System.Int32, System.Int32, System.Int32>(IronRuby.Builtins.ClrInteger.BitwiseOr), 
                new Func<System.Int32, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrInteger.BitwiseOr), 
                new Func<IronRuby.Runtime.RubyContext, System.Int32, IronRuby.Runtime.IntegerValue, System.Object>(IronRuby.Builtins.ClrInteger.BitwiseOr)
            );
            
            module.DefineLibraryMethod("~", 0x51, 
                new Func<System.Int32, System.Int32>(IronRuby.Builtins.ClrInteger.OnesComplement)
            );
            
            module.DefineLibraryMethod("+", 0x51, 
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.ClrInteger.Add), 
                new Func<System.Int32, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.Builtins.ClrInteger.Add), 
                new Func<System.Int32, System.Double, System.Double>(IronRuby.Builtins.ClrInteger.Add), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrInteger.Add)
            );
            
            module.DefineLibraryMethod("<", 0x51, 
                new Func<System.Int32, System.Int32, System.Boolean>(IronRuby.Builtins.ClrInteger.LessThan), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.ClrInteger.LessThan)
            );
            
            module.DefineLibraryMethod("<<", 0x51, 
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.ClrInteger.LeftShift), 
                new Func<IronRuby.Runtime.RubyContext, System.Int32, IronRuby.Runtime.IntegerValue, System.Object>(IronRuby.Builtins.ClrInteger.LeftShift)
            );
            
            module.DefineLibraryMethod("<=", 0x51, 
                new Func<System.Int32, System.Int32, System.Boolean>(IronRuby.Builtins.ClrInteger.LessThanOrEqual), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.ClrInteger.LessThanOrEqual)
            );
            
            module.DefineLibraryMethod("<=>", 0x51, 
                new Func<System.Int32, System.Int32, System.Int32>(IronRuby.Builtins.ClrInteger.Compare), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrInteger.Compare)
            );
            
            module.DefineLibraryMethod("==", 0x51, 
                new Func<System.Int32, System.Int32, System.Boolean>(IronRuby.Builtins.ClrInteger.Equal), 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Int32, System.Object, System.Boolean>(IronRuby.Builtins.ClrInteger.Equal)
            );
            
            module.DefineLibraryMethod(">", 0x51, 
                new Func<System.Int32, System.Int32, System.Boolean>(IronRuby.Builtins.ClrInteger.GreaterThan), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.ClrInteger.GreaterThan)
            );
            
            module.DefineLibraryMethod(">=", 0x51, 
                new Func<System.Int32, System.Int32, System.Boolean>(IronRuby.Builtins.ClrInteger.GreaterThanOrEqual), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.ClrInteger.GreaterThanOrEqual)
            );
            
            module.DefineLibraryMethod(">>", 0x51, 
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.ClrInteger.RightShift), 
                new Func<IronRuby.Runtime.RubyContext, System.Int32, IronRuby.Runtime.IntegerValue, System.Object>(IronRuby.Builtins.ClrInteger.RightShift)
            );
            
            module.DefineLibraryMethod("abs", 0x51, 
                new Func<System.Int32, System.Object>(IronRuby.Builtins.ClrInteger.Abs)
            );
            
            module.DefineLibraryMethod("div", 0x51, 
                new Func<System.Int32, System.Int32, System.Object>(IronRuby.Builtins.ClrInteger.Divide), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrInteger.Divide)
            );
            
            module.DefineLibraryMethod("divmod", 0x51, 
                new Func<System.Int32, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ClrInteger.DivMod), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Int32, System.Object, System.Object>(IronRuby.Builtins.ClrInteger.DivMod)
            );
            
            module.DefineLibraryMethod("modulo", 0x51, 
                new Func<System.Int32, System.Int32, System.Int32>(IronRuby.Builtins.ClrInteger.Modulo), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.ClrInteger.Modulo)
            );
            
            module.DefineLibraryMethod("quo", 0x51, 
                new Func<System.Int32, System.Int32, System.Double>(IronRuby.Builtins.ClrInteger.Quotient), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Int32, System.Object, System.Object>(IronRuby.Builtins.ClrInteger.Quotient)
            );
            
            module.DefineLibraryMethod("to_f", 0x51, 
                new Func<System.Int32, System.Double>(IronRuby.Builtins.ClrInteger.ToFloat)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<System.Object, System.Object>(IronRuby.Builtins.ClrInteger.ToString), 
                new Func<Microsoft.Scripting.Math.BigInteger, System.Int32, System.Object>(IronRuby.Builtins.ClrInteger.ToString)
            );
            
            module.DefineLibraryMethod("zero?", 0x51, 
                new Func<System.Int32, System.Boolean>(IronRuby.Builtins.ClrInteger.IsZero)
            );
            
        }
        
        private static void LoadIronRuby__Clr__Name_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("<=>", 0x51, 
                new Func<IronRuby.Runtime.ClrName, System.String, System.Int32>(IronRuby.Builtins.ClrNameOps.Compare), 
                new Func<IronRuby.Runtime.ClrName, IronRuby.Runtime.ClrName, System.Int32>(IronRuby.Builtins.ClrNameOps.Compare), 
                new Func<IronRuby.Runtime.ClrName, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.ClrNameOps.Compare), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.ClrName, System.Object, System.Object>(IronRuby.Builtins.ClrNameOps.Compare)
            );
            
            module.DefineLibraryMethod("==", 0x51, 
                new Func<IronRuby.Runtime.ClrName, System.String, System.Boolean>(IronRuby.Builtins.ClrNameOps.IsEqual), 
                new Func<IronRuby.Runtime.ClrName, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.ClrNameOps.IsEqual), 
                new Func<IronRuby.Runtime.ClrName, IronRuby.Runtime.ClrName, System.Boolean>(IronRuby.Builtins.ClrNameOps.IsEqual)
            );
            
            module.DefineLibraryMethod("clr_name", 0x51, 
                new Func<IronRuby.Runtime.ClrName, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ClrNameOps.GetClrName)
            );
            
            module.DefineLibraryMethod("dump", 0x51, 
                new Func<IronRuby.Runtime.ClrName, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ClrNameOps.Dump)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Runtime.ClrName, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ClrNameOps.Inspect)
            );
            
            module.DefineLibraryMethod("ruby_name", 0x51, 
                new Func<IronRuby.Runtime.ClrName, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ClrNameOps.GetRubyName)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<IronRuby.Runtime.ClrName, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ClrNameOps.GetRubyName)
            );
            
            module.DefineLibraryMethod("to_str", 0x51, 
                new Func<IronRuby.Runtime.ClrName, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ClrNameOps.GetRubyName)
            );
            
            module.DefineLibraryMethod("to_sym", 0x51, 
                new Func<IronRuby.Runtime.ClrName, Microsoft.Scripting.SymbolId>(IronRuby.Builtins.ClrNameOps.ToSymbol)
            );
            
        }
        
        private static void LoadIronRuby__Clr__Name_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("clr_to_ruby", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ClrNameOps.Mangle)
            );
            
            module.DefineLibraryMethod("mangle", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ClrNameOps.Mangle)
            );
            
            module.DefineLibraryMethod("ruby_to_clr", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ClrNameOps.Unmangle)
            );
            
            module.DefineLibraryMethod("unmangle", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ClrNameOps.Unmangle)
            );
            
        }
        
        private static void LoadIronRuby__Clr__Scope_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("method_missing", 0x52, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, Microsoft.Scripting.Runtime.Scope, Microsoft.Scripting.SymbolId, System.Object[], System.Object>(IronRuby.Builtins.ScopeOps.MethodMissing)
            );
            
        }
        
        private static void LoadIronRuby__Clr__String_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("%", 0x51, 
                new Func<IronRuby.Builtins.StringFormatterSiteStorage, System.String, System.Object, System.String>(IronRuby.Builtins.ClrString.Format)
            );
            
            module.DefineLibraryMethod("*", 0x51, 
                new Func<System.String, System.Int32, System.String>(IronRuby.Builtins.ClrString.Repeat)
            );
            
            module.DefineLibraryMethod("[]", 0x51, 
                new Func<System.String, System.Int32, System.Object>(IronRuby.Builtins.ClrString.GetChar), 
                new Func<System.String, System.Int32, System.Int32, System.String>(IronRuby.Builtins.ClrString.GetSubstring), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, System.String, IronRuby.Builtins.Range, System.String>(IronRuby.Builtins.ClrString.GetSubstring), 
                new Func<System.String, System.String, System.String>(IronRuby.Builtins.ClrString.GetSubstring), 
                new Func<IronRuby.Runtime.RubyScope, System.String, IronRuby.Builtins.RubyRegex, System.String>(IronRuby.Builtins.ClrString.GetSubstring), 
                new Func<IronRuby.Runtime.RubyScope, System.String, IronRuby.Builtins.RubyRegex, System.Int32, System.String>(IronRuby.Builtins.ClrString.GetSubstring)
            );
            
            module.DefineLibraryMethod("+", 0x51, 
                new Func<System.String, IronRuby.Builtins.MutableString, System.String>(IronRuby.Builtins.ClrString.Concatenate)
            );
            
            module.DefineLibraryMethod("<=>", 0x51, 
                new Func<System.String, System.String, System.Int32>(IronRuby.Builtins.ClrString.Compare), 
                new Func<System.String, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.ClrString.Compare), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RespondToStorage, System.String, System.Object, System.Object>(IronRuby.Builtins.ClrString.Compare)
            );
            
            module.DefineLibraryMethod("=~", 0x51, 
                new Func<IronRuby.Runtime.RubyScope, System.String, IronRuby.Builtins.RubyRegex, System.Object>(IronRuby.Builtins.ClrString.Match), 
                new Func<System.String, System.String, System.Object>(IronRuby.Builtins.ClrString.Match), 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyScope, System.Object, System.String, System.Object>>, IronRuby.Runtime.RubyScope, System.String, System.Object, System.Object>(IronRuby.Builtins.ClrString.Match)
            );
            
            module.DefineLibraryMethod("==", 0x51, 
                new Func<System.String, System.String, System.Boolean>(IronRuby.Builtins.ClrString.StringEquals), 
                new Func<System.String, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.ClrString.StringEquals), 
                new Func<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.BinaryOpStorage, System.String, System.Object, System.Boolean>(IronRuby.Builtins.ClrString.Equals)
            );
            
            module.DefineLibraryMethod("===", 0x51, 
                new Func<System.String, System.String, System.Boolean>(IronRuby.Builtins.ClrString.StringEquals), 
                new Func<System.String, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.ClrString.StringEquals), 
                new Func<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.BinaryOpStorage, System.String, System.Object, System.Boolean>(IronRuby.Builtins.ClrString.Equals)
            );
            
            module.DefineLibraryMethod("dump", 0x51, 
                new Func<System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ClrString.Dump)
            );
            
            module.DefineLibraryMethod("empty?", 0x51, 
                new Func<System.String, System.Boolean>(IronRuby.Builtins.ClrString.IsEmpty)
            );
            
            module.DefineLibraryMethod("encoding", 0x51, 
                new Func<System.String, IronRuby.Builtins.RubyEncoding>(IronRuby.Builtins.ClrString.GetEncoding)
            );
            
            module.DefineLibraryMethod("hex", 0x51, 
                new Func<System.String, System.Object>(IronRuby.Builtins.ClrString.ToIntegerHex)
            );
            
            module.DefineLibraryMethod("include?", 0x51, 
                new Func<System.String, System.String, System.Boolean>(IronRuby.Builtins.ClrString.Include)
            );
            
            module.DefineLibraryMethod("insert", 0x51, 
                new Func<System.String, System.Int32, System.String, System.String>(IronRuby.Builtins.ClrString.Insert)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ClrString.Inspect)
            );
            
            module.DefineLibraryMethod("intern", 0x51, 
                new Func<System.String, Microsoft.Scripting.SymbolId>(IronRuby.Builtins.ClrString.ToSymbol)
            );
            
            module.DefineLibraryMethod("method_missing", 0x52, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, System.String, Microsoft.Scripting.SymbolId, System.Object[], System.Object>(IronRuby.Builtins.ClrString.MethodMissing)
            );
            
            module.DefineLibraryMethod("oct", 0x51, 
                new Func<System.String, System.Object>(IronRuby.Builtins.ClrString.ToIntegerOctal)
            );
            
            module.DefineLibraryMethod("reverse", 0x51, 
                new Func<System.String, System.String>(IronRuby.Builtins.ClrString.GetReversed)
            );
            
            module.DefineLibraryMethod("size", 0x51, 
                new Func<System.String, System.Int32>(IronRuby.Builtins.ClrString.GetLength)
            );
            
            module.DefineLibraryMethod("slice", 0x51, 
                new Func<System.String, System.Int32, System.Object>(IronRuby.Builtins.ClrString.GetChar), 
                new Func<System.String, System.Int32, System.Int32, System.String>(IronRuby.Builtins.ClrString.GetSubstring), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, System.String, IronRuby.Builtins.Range, System.String>(IronRuby.Builtins.ClrString.GetSubstring), 
                new Func<System.String, System.String, System.String>(IronRuby.Builtins.ClrString.GetSubstring), 
                new Func<IronRuby.Runtime.RubyScope, System.String, IronRuby.Builtins.RubyRegex, System.String>(IronRuby.Builtins.ClrString.GetSubstring), 
                new Func<IronRuby.Runtime.RubyScope, System.String, IronRuby.Builtins.RubyRegex, System.Int32, System.String>(IronRuby.Builtins.ClrString.GetSubstring)
            );
            
            module.DefineLibraryMethod("split", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, System.String, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ClrString.Split), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, System.String, System.String, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ClrString.Split), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, System.String, IronRuby.Builtins.RubyRegex, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ClrString.Split)
            );
            
            module.DefineLibraryMethod("to_clr_string", 0x51, 
                new Func<System.String, System.String>(IronRuby.Builtins.ClrString.ToClrString)
            );
            
            module.DefineLibraryMethod("to_f", 0x51, 
                new Func<System.String, System.Double>(IronRuby.Builtins.ClrString.ToDouble)
            );
            
            module.DefineLibraryMethod("to_i", 0x51, 
                new Func<System.String, System.Int32, System.Object>(IronRuby.Builtins.ClrString.ToInteger)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ClrString.ToStr)
            );
            
            module.DefineLibraryMethod("to_str", 0x51, 
                new Func<System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ClrString.ToStr)
            );
            
            module.DefineLibraryMethod("to_sym", 0x51, 
                new Func<System.String, Microsoft.Scripting.SymbolId>(IronRuby.Builtins.ClrString.ToSymbol)
            );
            
        }
        
        private static void LoadIronRuby__Print_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("<<", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.PrintOps.Output)
            );
            
            module.DefineLibraryMethod("print", 0x51, 
                new Action<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyScope, System.Object>(IronRuby.Builtins.PrintOps.Print), 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object[]>(IronRuby.Builtins.PrintOps.Print), 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object>(IronRuby.Builtins.PrintOps.Print)
            );
            
            module.DefineLibraryMethod("printf", 0x51, 
                new Action<IronRuby.Builtins.StringFormatterSiteStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.BinaryOpStorage, System.Object, IronRuby.Builtins.MutableString, System.Object[]>(IronRuby.Builtins.PrintOps.PrintFormatted)
            );
            
            module.DefineLibraryMethod("putc", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.PrintOps.Putc), 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Int32, System.Int32>(IronRuby.Builtins.PrintOps.Putc)
            );
            
            module.DefineLibraryMethod("puts", 0x51, 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object>(IronRuby.Builtins.PrintOps.PutsEmptyLine), 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.PrintOps.Puts), 
                new Action<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Collections.IList>, System.Object, System.Object>(IronRuby.Builtins.PrintOps.Puts), 
                new Action<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Collections.IList>, System.Object, System.Object[]>(IronRuby.Builtins.PrintOps.Puts)
            );
            
        }
        
        private static void LoadKernel_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("__id__", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.GetObjectId)
            );
            
            module.DefineLibraryMethod("__send__", 0x51, 
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
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, System.Object, System.String, System.Object[], System.Object>(IronRuby.Builtins.KernelOps.SendMessage)
            );
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("`", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.ExecuteCommand)
            );
            
            #endif
            module.DefineLibraryMethod("=~", 0x51, 
                new Func<System.Object, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.Match)
            );
            
            module.DefineLibraryMethod("==", 0x51, 
                new Func<IronRuby.Runtime.IRubyObject, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.ValueEquals), 
                new Func<System.Object, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.ValueEquals)
            );
            
            module.DefineLibraryMethod("===", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.CaseEquals)
            );
            
            module.DefineLibraryMethod("abort", 0x52, 
                new Action<System.Object>(IronRuby.Builtins.KernelOps.Abort), 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Abort)
            );
            
            module.DefineLibraryMethod("Array", 0x52, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Collections.IList>, IronRuby.Runtime.ConversionStorage<System.Collections.IList>, System.Object, System.Object, System.Collections.IList>(IronRuby.Builtins.KernelOps.ToArray)
            );
            
            module.DefineLibraryMethod("at_exit", 0x52, 
                new Func<IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.Proc>(IronRuby.Builtins.KernelOps.AtExit)
            );
            
            module.DefineLibraryMethod("autoload", 0x52, 
                new Action<IronRuby.Runtime.RubyScope, System.Object, System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.SetAutoloadedConstant)
            );
            
            module.DefineLibraryMethod("autoload?", 0x52, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.GetAutoloadedConstantPath)
            );
            
            module.DefineLibraryMethod("binding", 0x52, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.Binding>(IronRuby.Builtins.KernelOps.GetLocalScope)
            );
            
            module.DefineLibraryMethod("block_given?", 0x52, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.HasBlock)
            );
            
            module.DefineLibraryMethod("caller", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetStackTrace)
            );
            
            module.DefineLibraryMethod("catch", 0x52, 
                new Func<IronRuby.Runtime.BlockParam, System.Object, System.String, System.Object>(IronRuby.Builtins.KernelOps.Catch)
            );
            
            module.DefineLibraryMethod("class", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.KernelOps.GetClass)
            );
            
            module.DefineLibraryMethod("clone", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Clone)
            );
            
            module.DefineLibraryMethod("clr_member", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, System.String, IronRuby.Builtins.RubyMethod>(IronRuby.Builtins.KernelOps.GetClrMember)
            );
            
            module.DefineLibraryMethod("display", 0x51, 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object>(IronRuby.Builtins.KernelOps.Display)
            );
            
            module.DefineLibraryMethod("dup", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Duplicate)
            );
            
            module.DefineLibraryMethod("eql?", 0x51, 
                new Func<IronRuby.Runtime.IRubyObject, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.ValueEquals), 
                new Func<System.Object, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.ValueEquals)
            );
            
            module.DefineLibraryMethod("equal?", 0x51, 
                new Func<System.Object, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.IsEqual)
            );
            
            module.DefineLibraryMethod("eval", 0x52, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.Binding, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Evaluate), 
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.Proc, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Evaluate)
            );
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("exec", 0x52, 
                new Action<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Execute), 
                new Action<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString[]>(IronRuby.Builtins.KernelOps.Execute)
            );
            
            #endif
            module.DefineLibraryMethod("exit", 0x52, 
                new Action<System.Object>(IronRuby.Builtins.KernelOps.Exit), 
                new Action<System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.Exit), 
                new Action<System.Object, System.Int32>(IronRuby.Builtins.KernelOps.Exit)
            );
            
            module.DefineLibraryMethod("exit!", 0x52, 
                new Action<IronRuby.Runtime.RubyContext, System.Object>(IronRuby.Builtins.KernelOps.TerminateExecution), 
                new Action<IronRuby.Runtime.RubyContext, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.TerminateExecution), 
                new Action<IronRuby.Runtime.RubyContext, System.Object, System.Int32>(IronRuby.Builtins.KernelOps.TerminateExecution)
            );
            
            module.DefineLibraryMethod("extend", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyModule, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyModule, System.Object, System.Object>>, System.Object, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule[], System.Object>(IronRuby.Builtins.KernelOps.Extend)
            );
            
            module.DefineLibraryMethod("fail", 0x52, 
                new Action<IronRuby.Runtime.RubyContext, System.Object>(IronRuby.Builtins.KernelOps.RaiseException), 
                new Action<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.RaiseException), 
                new Action<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.CallSiteStorage<Action<System.Runtime.CompilerServices.CallSite, System.Exception, IronRuby.Builtins.RubyArray>>, System.Object, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.RaiseException)
            );
            
            module.DefineLibraryMethod("Float", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.KernelOps.ToFloat)
            );
            
            module.DefineLibraryMethod("format", 0x52, 
                new Func<IronRuby.Builtins.StringFormatterSiteStorage, System.Object, IronRuby.Builtins.MutableString, System.Object[], IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Sprintf)
            );
            
            module.DefineLibraryMethod("freeze", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Freeze)
            );
            
            module.DefineLibraryMethod("frozen?", 0x51, 
                new Func<IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.KernelOps.Frozen), 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.Frozen)
            );
            
            module.DefineLibraryMethod("getc", 0x52, 
                new Func<IronRuby.Runtime.UnaryOpStorage, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ReadInputCharacter)
            );
            
            module.DefineLibraryMethod("gets", 0x52, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.MutableString, System.Object>>, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ReadInputLine), 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.MutableString, System.Object>>, System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.KernelOps.ReadInputLine)
            );
            
            module.DefineLibraryMethod("global_variables", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetGlobalVariableNames)
            );
            
            module.DefineLibraryMethod("hash", 0x51, 
                new Func<IronRuby.Runtime.IRubyObject, System.Int32>(IronRuby.Builtins.KernelOps.Hash), 
                new Func<System.Object, System.Int32>(IronRuby.Builtins.KernelOps.Hash)
            );
            
            module.DefineLibraryMethod("id", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.GetId)
            );
            
            module.DefineLibraryMethod("initialize_copy", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.InitializeCopy)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Inspect)
            );
            
            module.DefineLibraryMethod("instance_eval", 0x51, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Evaluate), 
                new Func<IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.KernelOps.InstanceEval)
            );
            
            module.DefineLibraryMethod("instance_exec", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, System.Object, System.Object[], System.Object>(IronRuby.Builtins.KernelOps.InstanceExec)
            );
            
            module.DefineLibraryMethod("instance_of?", 0x51, 
                new Func<System.Object, IronRuby.Builtins.RubyModule, System.Boolean>(IronRuby.Builtins.KernelOps.IsOfClass)
            );
            
            module.DefineLibraryMethod("instance_variable_defined?", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.String, System.Boolean>(IronRuby.Builtins.KernelOps.InstanceVariableDefined)
            );
            
            module.DefineLibraryMethod("instance_variable_get", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.String, System.Object>(IronRuby.Builtins.KernelOps.InstanceVariableGet)
            );
            
            module.DefineLibraryMethod("instance_variable_set", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.String, System.Object, System.Object>(IronRuby.Builtins.KernelOps.InstanceVariableSet)
            );
            
            module.DefineLibraryMethod("instance_variables", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.InstanceVariables)
            );
            
            module.DefineLibraryMethod("Integer", 0x52, 
                new Func<System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.KernelOps.ToInteger), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Runtime.IntegerValue>, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ToInteger)
            );
            
            module.DefineLibraryMethod("is_a?", 0x51, 
                new Func<System.Object, IronRuby.Builtins.RubyModule, System.Boolean>(IronRuby.Builtins.KernelOps.IsKindOf)
            );
            
            module.DefineLibraryMethod("iterator?", 0x52, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.HasBlock)
            );
            
            module.DefineLibraryMethod("kind_of?", 0x51, 
                new Func<System.Object, IronRuby.Builtins.RubyModule, System.Boolean>(IronRuby.Builtins.KernelOps.IsKindOf)
            );
            
            module.DefineLibraryMethod("lambda", 0x52, 
                new Func<IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.Proc>(IronRuby.Builtins.KernelOps.CreateLambda)
            );
            
            module.DefineLibraryMethod("load", 0x52, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, System.Boolean, System.Boolean>(IronRuby.Builtins.KernelOps.Load)
            );
            
            module.DefineLibraryMethod("load_assembly", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.KernelOps.LoadAssembly)
            );
            
            module.DefineLibraryMethod("local_variables", 0x52, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetLocalVariableNames)
            );
            
            module.DefineLibraryMethod("loop", 0x52, 
                new Func<IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Loop)
            );
            
            module.DefineLibraryMethod("method", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.String, IronRuby.Builtins.RubyMethod>(IronRuby.Builtins.KernelOps.GetMethod)
            );
            
            module.DefineLibraryMethod("method_missing", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, Microsoft.Scripting.SymbolId, System.Object[], System.Object>(IronRuby.Builtins.KernelOps.MethodMissing)
            );
            
            module.DefineLibraryMethod("methods", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetMethods)
            );
            
            module.DefineLibraryMethod("nil?", 0x51, 
                new Func<System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.IsNil)
            );
            
            module.DefineLibraryMethod("object_id", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.GetObjectId)
            );
            
            module.DefineLibraryMethod("open", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.KernelOps.Open), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Open), 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Int32, System.Int32, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.KernelOps.Open), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.MutableString, System.Int32, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Open)
            );
            
            module.DefineLibraryMethod("p", 0x52, 
                new Action<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, System.Object, System.Object[]>(IronRuby.Builtins.KernelOps.PrintInspect)
            );
            
            module.DefineLibraryMethod("print", 0x52, 
                new Action<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyScope, System.Object>(IronRuby.Builtins.KernelOps.Print), 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Print), 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object[]>(IronRuby.Builtins.KernelOps.Print)
            );
            
            module.DefineLibraryMethod("printf", 0x52, 
                new Action<IronRuby.Builtins.StringFormatterSiteStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.BinaryOpStorage, System.Object, IronRuby.Builtins.MutableString, System.Object[]>(IronRuby.Builtins.KernelOps.PrintFormatted), 
                new Action<IronRuby.Builtins.StringFormatterSiteStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object, System.Object[]>(IronRuby.Builtins.KernelOps.PrintFormatted)
            );
            
            module.DefineLibraryMethod("private_methods", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetPrivateMethods)
            );
            
            module.DefineLibraryMethod("proc", 0x52, 
                new Func<IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.Proc>(IronRuby.Builtins.KernelOps.CreateLambda)
            );
            
            module.DefineLibraryMethod("protected_methods", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetProtectedMethods)
            );
            
            module.DefineLibraryMethod("public_methods", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetPublicMethods)
            );
            
            module.DefineLibraryMethod("putc", 0x52, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Putc), 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Int32, System.Int32>(IronRuby.Builtins.KernelOps.Putc)
            );
            
            module.DefineLibraryMethod("puts", 0x52, 
                new Action<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Collections.IList>, System.Object, System.Object[]>(IronRuby.Builtins.KernelOps.PutString), 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object>(IronRuby.Builtins.KernelOps.PutsEmptyLine), 
                new Action<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Collections.IList>, System.Object, System.Object>(IronRuby.Builtins.KernelOps.PutString), 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.PutString)
            );
            
            module.DefineLibraryMethod("raise", 0x52, 
                new Action<IronRuby.Runtime.RubyContext, System.Object>(IronRuby.Builtins.KernelOps.RaiseException), 
                new Action<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.RaiseException), 
                new Action<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.CallSiteStorage<Action<System.Runtime.CompilerServices.CallSite, System.Exception, IronRuby.Builtins.RubyArray>>, System.Object, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.RaiseException)
            );
            
            module.DefineLibraryMethod("rand", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Double>(IronRuby.Builtins.KernelOps.Random), 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Random), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Runtime.IntegerValue>, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Random)
            );
            
            module.DefineLibraryMethod("remove_instance_variable", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.String, System.Object>(IronRuby.Builtins.KernelOps.RemoveInstanceVariable)
            );
            
            module.DefineLibraryMethod("require", 0x52, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.KernelOps.Require)
            );
            
            module.DefineLibraryMethod("respond_to?", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.String, System.Boolean, System.Boolean>(IronRuby.Builtins.KernelOps.RespondTo)
            );
            
            module.DefineLibraryMethod("select", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.Select), 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.Select), 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, System.Double, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.Select)
            );
            
            module.DefineLibraryMethod("send", 0x51, 
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
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, System.Object, System.String, System.Object[], System.Object>(IronRuby.Builtins.KernelOps.SendMessage)
            );
            
            module.DefineLibraryMethod("set_trace_func", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, IronRuby.Builtins.Proc>(IronRuby.Builtins.KernelOps.SetTraceListener)
            );
            
            module.DefineLibraryMethod("singleton_method_added", 0x5a, 
                new Action<System.Object, System.Object>(IronRuby.Builtins.KernelOps.MethodAdded)
            );
            
            module.DefineLibraryMethod("singleton_method_removed", 0x5a, 
                new Action<System.Object, System.Object>(IronRuby.Builtins.KernelOps.MethodRemoved)
            );
            
            module.DefineLibraryMethod("singleton_method_undefined", 0x5a, 
                new Action<System.Object, System.Object>(IronRuby.Builtins.KernelOps.MethodUndefined)
            );
            
            module.DefineLibraryMethod("singleton_methods", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetSingletonMethods)
            );
            
            module.DefineLibraryMethod("sleep", 0x52, 
                new Action<System.Object>(IronRuby.Builtins.KernelOps.Sleep), 
                new Func<System.Object, System.Int32, System.Int32>(IronRuby.Builtins.KernelOps.Sleep), 
                new Func<System.Object, System.Double, System.Int32>(IronRuby.Builtins.KernelOps.Sleep)
            );
            
            module.DefineLibraryMethod("sprintf", 0x52, 
                new Func<IronRuby.Builtins.StringFormatterSiteStorage, System.Object, IronRuby.Builtins.MutableString, System.Object[], IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Sprintf)
            );
            
            module.DefineLibraryMethod("srand", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Runtime.IntegerValue, System.Object>(IronRuby.Builtins.KernelOps.SeedRandomNumberGenerator)
            );
            
            module.DefineLibraryMethod("String", 0x52, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ToString)
            );
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("system", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.KernelOps.System), 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString[], System.Boolean>(IronRuby.Builtins.KernelOps.System)
            );
            
            #endif
            module.DefineLibraryMethod("taint", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Taint)
            );
            
            module.DefineLibraryMethod("tainted?", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.Tainted)
            );
            
            module.DefineLibraryMethod("test", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.KernelOps.Test), 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.KernelOps.Test)
            );
            
            module.DefineLibraryMethod("throw", 0x52, 
                new Action<System.Object, System.String, System.Object>(IronRuby.Builtins.KernelOps.Throw)
            );
            
            module.DefineLibraryMethod("to_a", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.ToA)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<IronRuby.Runtime.IRubyObject, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.ToS), 
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.ToS)
            );
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("trap", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.KernelOps.Trap), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Trap)
            );
            
            #endif
            module.DefineLibraryMethod("type", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.KernelOps.GetClassObsolete)
            );
            
            module.DefineLibraryMethod("untaint", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Untaint)
            );
            
            module.DefineLibraryMethod("warn", 0x52, 
                new Action<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ReportWarning)
            );
            
        }
        
        private static void LoadKernel_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            #if !SILVERLIGHT
            module.DefineLibraryMethod("`", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.ExecuteCommand)
            );
            
            #endif
            module.DefineLibraryMethod("abort", 0x61, 
                new Action<System.Object>(IronRuby.Builtins.KernelOps.Abort), 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Abort)
            );
            
            module.DefineLibraryMethod("Array", 0x61, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Collections.IList>, IronRuby.Runtime.ConversionStorage<System.Collections.IList>, System.Object, System.Object, System.Collections.IList>(IronRuby.Builtins.KernelOps.ToArray)
            );
            
            module.DefineLibraryMethod("at_exit", 0x61, 
                new Func<IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.Proc>(IronRuby.Builtins.KernelOps.AtExit)
            );
            
            module.DefineLibraryMethod("autoload", 0x61, 
                new Action<IronRuby.Runtime.RubyScope, System.Object, System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.SetAutoloadedConstant)
            );
            
            module.DefineLibraryMethod("autoload?", 0x61, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.GetAutoloadedConstantPath)
            );
            
            module.DefineLibraryMethod("binding", 0x61, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.Binding>(IronRuby.Builtins.KernelOps.GetLocalScope)
            );
            
            module.DefineLibraryMethod("block_given?", 0x61, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.HasBlock)
            );
            
            module.DefineLibraryMethod("caller", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetStackTrace)
            );
            
            module.DefineLibraryMethod("catch", 0x61, 
                new Func<IronRuby.Runtime.BlockParam, System.Object, System.String, System.Object>(IronRuby.Builtins.KernelOps.Catch)
            );
            
            module.DefineLibraryMethod("eval", 0x61, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.Binding, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Evaluate), 
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.Proc, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Evaluate)
            );
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("exec", 0x61, 
                new Action<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Execute), 
                new Action<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString[]>(IronRuby.Builtins.KernelOps.Execute)
            );
            
            #endif
            module.DefineLibraryMethod("exit", 0x61, 
                new Action<System.Object>(IronRuby.Builtins.KernelOps.Exit), 
                new Action<System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.Exit), 
                new Action<System.Object, System.Int32>(IronRuby.Builtins.KernelOps.Exit)
            );
            
            module.DefineLibraryMethod("exit!", 0x61, 
                new Action<IronRuby.Runtime.RubyContext, System.Object>(IronRuby.Builtins.KernelOps.TerminateExecution), 
                new Action<IronRuby.Runtime.RubyContext, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.TerminateExecution), 
                new Action<IronRuby.Runtime.RubyContext, System.Object, System.Int32>(IronRuby.Builtins.KernelOps.TerminateExecution)
            );
            
            module.DefineLibraryMethod("fail", 0x61, 
                new Action<IronRuby.Runtime.RubyContext, System.Object>(IronRuby.Builtins.KernelOps.RaiseException), 
                new Action<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.RaiseException), 
                new Action<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.CallSiteStorage<Action<System.Runtime.CompilerServices.CallSite, System.Exception, IronRuby.Builtins.RubyArray>>, System.Object, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.RaiseException)
            );
            
            module.DefineLibraryMethod("Float", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.KernelOps.ToFloat)
            );
            
            module.DefineLibraryMethod("format", 0x61, 
                new Func<IronRuby.Builtins.StringFormatterSiteStorage, System.Object, IronRuby.Builtins.MutableString, System.Object[], IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Sprintf)
            );
            
            module.DefineLibraryMethod("getc", 0x61, 
                new Func<IronRuby.Runtime.UnaryOpStorage, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ReadInputCharacter)
            );
            
            module.DefineLibraryMethod("gets", 0x61, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.MutableString, System.Object>>, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ReadInputLine), 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.MutableString, System.Object>>, System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.KernelOps.ReadInputLine)
            );
            
            module.DefineLibraryMethod("global_variables", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetGlobalVariableNames)
            );
            
            module.DefineLibraryMethod("Integer", 0x61, 
                new Func<System.Object, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.KernelOps.ToInteger), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Runtime.IntegerValue>, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ToInteger)
            );
            
            module.DefineLibraryMethod("iterator?", 0x61, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, System.Boolean>(IronRuby.Builtins.KernelOps.HasBlock)
            );
            
            module.DefineLibraryMethod("lambda", 0x61, 
                new Func<IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.Proc>(IronRuby.Builtins.KernelOps.CreateLambda)
            );
            
            module.DefineLibraryMethod("load", 0x61, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, System.Boolean, System.Boolean>(IronRuby.Builtins.KernelOps.Load)
            );
            
            module.DefineLibraryMethod("load_assembly", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.KernelOps.LoadAssembly)
            );
            
            module.DefineLibraryMethod("local_variables", 0x61, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.GetLocalVariableNames)
            );
            
            module.DefineLibraryMethod("loop", 0x61, 
                new Func<IronRuby.Runtime.BlockParam, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Loop)
            );
            
            module.DefineLibraryMethod("method_missing", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, Microsoft.Scripting.SymbolId, System.Object[], System.Object>(IronRuby.Builtins.KernelOps.MethodMissing)
            );
            
            module.DefineLibraryMethod("open", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.KernelOps.Open), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Open), 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Int32, System.Int32, IronRuby.Builtins.RubyIO>(IronRuby.Builtins.KernelOps.Open), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.MutableString, System.Int32, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Open)
            );
            
            module.DefineLibraryMethod("p", 0x61, 
                new Action<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, System.Object, System.Object[]>(IronRuby.Builtins.KernelOps.PrintInspect)
            );
            
            module.DefineLibraryMethod("print", 0x61, 
                new Action<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyScope, System.Object>(IronRuby.Builtins.KernelOps.Print), 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Print), 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object[]>(IronRuby.Builtins.KernelOps.Print)
            );
            
            module.DefineLibraryMethod("printf", 0x61, 
                new Action<IronRuby.Builtins.StringFormatterSiteStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.BinaryOpStorage, System.Object, IronRuby.Builtins.MutableString, System.Object[]>(IronRuby.Builtins.KernelOps.PrintFormatted), 
                new Action<IronRuby.Builtins.StringFormatterSiteStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object, System.Object[]>(IronRuby.Builtins.KernelOps.PrintFormatted)
            );
            
            module.DefineLibraryMethod("proc", 0x61, 
                new Func<IronRuby.Runtime.BlockParam, System.Object, IronRuby.Builtins.Proc>(IronRuby.Builtins.KernelOps.CreateLambda)
            );
            
            module.DefineLibraryMethod("putc", 0x61, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Putc), 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Int32, System.Int32>(IronRuby.Builtins.KernelOps.Putc)
            );
            
            module.DefineLibraryMethod("puts", 0x61, 
                new Action<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Collections.IList>, System.Object, System.Object[]>(IronRuby.Builtins.KernelOps.PutString), 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object>(IronRuby.Builtins.KernelOps.PutsEmptyLine), 
                new Action<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Collections.IList>, System.Object, System.Object>(IronRuby.Builtins.KernelOps.PutString), 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.PutString)
            );
            
            module.DefineLibraryMethod("raise", 0x61, 
                new Action<IronRuby.Runtime.RubyContext, System.Object>(IronRuby.Builtins.KernelOps.RaiseException), 
                new Action<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.RaiseException), 
                new Action<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.CallSiteStorage<Action<System.Runtime.CompilerServices.CallSite, System.Exception, IronRuby.Builtins.RubyArray>>, System.Object, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.RaiseException)
            );
            
            module.DefineLibraryMethod("rand", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Double>(IronRuby.Builtins.KernelOps.Random), 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32, System.Object>(IronRuby.Builtins.KernelOps.Random), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Runtime.IntegerValue>, IronRuby.Runtime.RubyContext, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Random)
            );
            
            module.DefineLibraryMethod("require", 0x61, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.KernelOps.Require)
            );
            
            module.DefineLibraryMethod("select", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.Select), 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.Select), 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, IronRuby.Builtins.RubyArray, System.Double, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.KernelOps.Select)
            );
            
            module.DefineLibraryMethod("set_trace_func", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.Proc, IronRuby.Builtins.Proc>(IronRuby.Builtins.KernelOps.SetTraceListener)
            );
            
            module.DefineLibraryMethod("sleep", 0x61, 
                new Action<System.Object>(IronRuby.Builtins.KernelOps.Sleep), 
                new Func<System.Object, System.Int32, System.Int32>(IronRuby.Builtins.KernelOps.Sleep), 
                new Func<System.Object, System.Double, System.Int32>(IronRuby.Builtins.KernelOps.Sleep)
            );
            
            module.DefineLibraryMethod("sprintf", 0x61, 
                new Func<IronRuby.Builtins.StringFormatterSiteStorage, System.Object, IronRuby.Builtins.MutableString, System.Object[], IronRuby.Builtins.MutableString>(IronRuby.Builtins.KernelOps.Sprintf)
            );
            
            module.DefineLibraryMethod("srand", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Runtime.IntegerValue, System.Object>(IronRuby.Builtins.KernelOps.SeedRandomNumberGenerator)
            );
            
            module.DefineLibraryMethod("String", 0x61, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ToString)
            );
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("system", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.KernelOps.System), 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString[], System.Boolean>(IronRuby.Builtins.KernelOps.System)
            );
            
            #endif
            module.DefineLibraryMethod("test", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.KernelOps.Test), 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.KernelOps.Test)
            );
            
            module.DefineLibraryMethod("throw", 0x61, 
                new Action<System.Object, System.String, System.Object>(IronRuby.Builtins.KernelOps.Throw)
            );
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("trap", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.KernelOps.Trap), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.KernelOps.Trap)
            );
            
            #endif
            module.DefineLibraryMethod("warn", 0x61, 
                new Action<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, System.Object, System.Object>(IronRuby.Builtins.KernelOps.ReportWarning)
            );
            
        }
        
        private static void LoadMarshal_Constants(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetBuiltinConstant("MAJOR_VERSION", IronRuby.Builtins.RubyMarshal.MAJOR_VERSION);
            module.SetBuiltinConstant("MINOR_VERSION", IronRuby.Builtins.RubyMarshal.MINOR_VERSION);
            
        }
        
        private static void LoadMarshal_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("dump", 0x61, 
                new Func<IronRuby.Builtins.RubyMarshal.WriterSites, IronRuby.Builtins.RubyModule, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyMarshal.Dump), 
                new Func<IronRuby.Builtins.RubyMarshal.WriterSites, IronRuby.Builtins.RubyModule, System.Object, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyMarshal.Dump), 
                new Func<IronRuby.Builtins.RubyMarshal.WriterSites, IronRuby.Builtins.RubyModule, System.Object, IronRuby.Builtins.RubyIO, System.Nullable<System.Int32>, System.Object>(IronRuby.Builtins.RubyMarshal.Dump), 
                new Func<IronRuby.Builtins.RubyMarshal.WriterSites, IronRuby.Runtime.RespondToStorage, IronRuby.Builtins.RubyModule, System.Object, System.Object, System.Nullable<System.Int32>, System.Object>(IronRuby.Builtins.RubyMarshal.Dump)
            );
            
            module.DefineLibraryMethod("load", 0x61, 
                new Func<IronRuby.Builtins.RubyMarshal.ReaderSites, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.RubyMarshal.Load), 
                new Func<IronRuby.Builtins.RubyMarshal.ReaderSites, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyIO, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.RubyMarshal.Load), 
                new Func<IronRuby.Builtins.RubyMarshal.ReaderSites, IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.Object, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.RubyMarshal.Load)
            );
            
            module.DefineLibraryMethod("restore", 0x61, 
                new Func<IronRuby.Builtins.RubyMarshal.ReaderSites, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.RubyMarshal.Load), 
                new Func<IronRuby.Builtins.RubyMarshal.ReaderSites, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyIO, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.RubyMarshal.Load), 
                new Func<IronRuby.Builtins.RubyMarshal.ReaderSites, IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.Object, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.RubyMarshal.Load)
            );
            
        }
        
        private static void LoadMatchData_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("[]", 0x51, 
                new Func<IronRuby.Builtins.MatchData, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MatchDataOps.GetGroup), 
                new Func<IronRuby.Builtins.MatchData, System.Int32, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MatchDataOps.GetGroup), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.MatchData, IronRuby.Builtins.Range, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MatchDataOps.GetGroup)
            );
            
            module.DefineLibraryMethod("begin", 0x51, 
                new Func<IronRuby.Builtins.MatchData, System.Int32, System.Object>(IronRuby.Builtins.MatchDataOps.Begin)
            );
            
            module.DefineLibraryMethod("captures", 0x51, 
                new Func<IronRuby.Builtins.MatchData, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MatchDataOps.Captures)
            );
            
            module.DefineLibraryMethod("end", 0x51, 
                new Func<IronRuby.Builtins.MatchData, System.Int32, System.Object>(IronRuby.Builtins.MatchDataOps.End)
            );
            
            module.DefineLibraryMethod("initialize_copy", 0x52, 
                new Func<IronRuby.Builtins.MatchData, IronRuby.Builtins.MatchData, IronRuby.Builtins.MatchData>(IronRuby.Builtins.MatchDataOps.InitializeCopy)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MatchData, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MatchDataOps.Inspect)
            );
            
            module.DefineLibraryMethod("length", 0x51, 
                new Func<IronRuby.Builtins.MatchData, System.Int32>(IronRuby.Builtins.MatchDataOps.Length)
            );
            
            module.DefineLibraryMethod("offset", 0x51, 
                new Func<IronRuby.Builtins.MatchData, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MatchDataOps.Offset)
            );
            
            module.DefineLibraryMethod("post_match", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MatchData, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MatchDataOps.PostMatch)
            );
            
            module.DefineLibraryMethod("pre_match", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MatchData, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MatchDataOps.PreMatch)
            );
            
            module.DefineLibraryMethod("select", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.MatchData, System.Object>(IronRuby.Builtins.MatchDataOps.Select)
            );
            
            module.DefineLibraryMethod("size", 0x51, 
                new Func<IronRuby.Builtins.MatchData, System.Int32>(IronRuby.Builtins.MatchDataOps.Length)
            );
            
            module.DefineLibraryMethod("string", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MatchData, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MatchDataOps.ReturnFrozenString)
            );
            
            module.DefineLibraryMethod("to_a", 0x51, 
                new Func<IronRuby.Builtins.MatchData, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MatchDataOps.ToArray)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<IronRuby.Builtins.MatchData, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MatchDataOps.ToS)
            );
            
            module.DefineLibraryMethod("values_at", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.MatchData, System.Int32[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MatchDataOps.ValuesAt)
            );
            
        }
        
        private static void LoadMatchData_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.UndefineMethodNoEvent("new");
        }
        
        private static void LoadMath_Constants(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetBuiltinConstant("E", IronRuby.Builtins.RubyMath.E);
            module.SetBuiltinConstant("PI", IronRuby.Builtins.RubyMath.PI);
            
        }
        
        private static void LoadMath_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("acos", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Acos)
            );
            
            module.DefineLibraryMethod("acosh", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Acosh)
            );
            
            module.DefineLibraryMethod("asin", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Asin)
            );
            
            module.DefineLibraryMethod("asinh", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Asinh)
            );
            
            module.DefineLibraryMethod("atan", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Atan)
            );
            
            module.DefineLibraryMethod("atan2", 0x52, 
                new Func<System.Object, System.Double, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Atan2)
            );
            
            module.DefineLibraryMethod("atanh", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Atanh)
            );
            
            module.DefineLibraryMethod("cos", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Cos)
            );
            
            module.DefineLibraryMethod("cosh", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Cosh)
            );
            
            module.DefineLibraryMethod("erf", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Erf)
            );
            
            module.DefineLibraryMethod("erfc", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Erfc)
            );
            
            module.DefineLibraryMethod("exp", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Exp)
            );
            
            module.DefineLibraryMethod("frexp", 0x52, 
                new Func<System.Object, System.Double, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyMath.Frexp)
            );
            
            module.DefineLibraryMethod("hypot", 0x52, 
                new Func<System.Object, System.Double, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Hypot)
            );
            
            module.DefineLibraryMethod("ldexp", 0x52, 
                new Func<System.Object, System.Double, IronRuby.Runtime.IntegerValue, System.Double>(IronRuby.Builtins.RubyMath.Ldexp)
            );
            
            module.DefineLibraryMethod("log", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Log)
            );
            
            module.DefineLibraryMethod("log10", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Log10)
            );
            
            module.DefineLibraryMethod("sin", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Sin)
            );
            
            module.DefineLibraryMethod("sinh", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Sinh)
            );
            
            module.DefineLibraryMethod("sqrt", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Sqrt)
            );
            
            module.DefineLibraryMethod("tan", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Tan)
            );
            
            module.DefineLibraryMethod("tanh", 0x52, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Tanh)
            );
            
        }
        
        private static void LoadMath_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("acos", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Acos)
            );
            
            module.DefineLibraryMethod("acosh", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Acosh)
            );
            
            module.DefineLibraryMethod("asin", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Asin)
            );
            
            module.DefineLibraryMethod("asinh", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Asinh)
            );
            
            module.DefineLibraryMethod("atan", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Atan)
            );
            
            module.DefineLibraryMethod("atan2", 0x61, 
                new Func<System.Object, System.Double, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Atan2)
            );
            
            module.DefineLibraryMethod("atanh", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Atanh)
            );
            
            module.DefineLibraryMethod("cos", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Cos)
            );
            
            module.DefineLibraryMethod("cosh", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Cosh)
            );
            
            module.DefineLibraryMethod("erf", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Erf)
            );
            
            module.DefineLibraryMethod("erfc", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Erfc)
            );
            
            module.DefineLibraryMethod("exp", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Exp)
            );
            
            module.DefineLibraryMethod("frexp", 0x61, 
                new Func<System.Object, System.Double, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyMath.Frexp)
            );
            
            module.DefineLibraryMethod("hypot", 0x61, 
                new Func<System.Object, System.Double, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Hypot)
            );
            
            module.DefineLibraryMethod("ldexp", 0x61, 
                new Func<System.Object, System.Double, IronRuby.Runtime.IntegerValue, System.Double>(IronRuby.Builtins.RubyMath.Ldexp)
            );
            
            module.DefineLibraryMethod("log", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Log)
            );
            
            module.DefineLibraryMethod("log10", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Log10)
            );
            
            module.DefineLibraryMethod("sin", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Sin)
            );
            
            module.DefineLibraryMethod("sinh", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Sinh)
            );
            
            module.DefineLibraryMethod("sqrt", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Sqrt)
            );
            
            module.DefineLibraryMethod("tan", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Tan)
            );
            
            module.DefineLibraryMethod("tanh", 0x61, 
                new Func<System.Object, System.Double, System.Double>(IronRuby.Builtins.RubyMath.Tanh)
            );
            
        }
        
        private static void LoadMethod_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineRuleGenerator("[]", 0x51, IronRuby.Builtins.MethodOps.Call());
            
            module.DefineLibraryMethod("==", 0x51, 
                new Func<IronRuby.Builtins.RubyMethod, IronRuby.Builtins.RubyMethod, System.Boolean>(IronRuby.Builtins.MethodOps.Equal), 
                new Func<IronRuby.Builtins.RubyMethod, System.Object, System.Boolean>(IronRuby.Builtins.MethodOps.Equal)
            );
            
            module.DefineLibraryMethod("arity", 0x51, 
                new Func<IronRuby.Builtins.RubyMethod, System.Int32>(IronRuby.Builtins.MethodOps.GetArity)
            );
            
            module.DefineRuleGenerator("call", 0x51, IronRuby.Builtins.MethodOps.Call());
            
            module.DefineLibraryMethod("clone", 0x51, 
                new Func<IronRuby.Builtins.RubyMethod, IronRuby.Builtins.RubyMethod>(IronRuby.Builtins.MethodOps.Clone)
            );
            
            module.DefineLibraryMethod("clr_members", 0x51, 
                new Func<IronRuby.Builtins.RubyMethod, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MethodOps.GetClrMembers)
            );
            
            module.DefineLibraryMethod("of", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyMethod, System.Object[], IronRuby.Builtins.RubyMethod>(IronRuby.Builtins.MethodOps.BindGenericParameters)
            );
            
            module.DefineLibraryMethod("overload", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyMethod, System.Object[], IronRuby.Builtins.RubyMethod>(IronRuby.Builtins.MethodOps.SelectOverload)
            );
            
            module.DefineLibraryMethod("overloads", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyMethod, System.Object[], IronRuby.Builtins.RubyMethod>(IronRuby.Builtins.MethodOps.SelectOverload_old)
            );
            
            module.DefineLibraryMethod("to_proc", 0x51, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyMethod, IronRuby.Builtins.Proc>(IronRuby.Builtins.MethodOps.ToProc)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyMethod, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MethodOps.ToS)
            );
            
            module.DefineLibraryMethod("unbind", 0x51, 
                new Func<IronRuby.Builtins.RubyMethod, IronRuby.Builtins.UnboundMethod>(IronRuby.Builtins.MethodOps.Unbind)
            );
            
        }
        
        private static void LoadMicrosoft__Scripting__Actions__TypeGroup_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("[]", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Actions.TypeGroup, System.Object[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.TypeGroupOps.Of), 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Actions.TypeGroup, System.Int32, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.TypeGroupOps.Of)
            );
            
            module.DefineLibraryMethod("clr_constructor", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Actions.TypeGroup, IronRuby.Builtins.RubyMethod>(IronRuby.Builtins.TypeGroupOps.GetClrConstructor)
            );
            
            module.DefineLibraryMethod("clr_ctor", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Actions.TypeGroup, IronRuby.Builtins.RubyMethod>(IronRuby.Builtins.TypeGroupOps.GetClrConstructor)
            );
            
            module.DefineLibraryMethod("clr_new", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object, System.Object>>, Microsoft.Scripting.Actions.TypeGroup, System.Object[], System.Object>(IronRuby.Builtins.TypeGroupOps.ClrNew), 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object, System.Object, System.Object>>, IronRuby.Runtime.BlockParam, Microsoft.Scripting.Actions.TypeGroup, System.Object[], System.Object>(IronRuby.Builtins.TypeGroupOps.ClrNew)
            );
            
            module.DefineLibraryMethod("each", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, Microsoft.Scripting.Actions.TypeGroup, System.Object>(IronRuby.Builtins.TypeGroupOps.EachType)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Actions.TypeGroup, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TypeGroupOps.Inspect)
            );
            
            module.DefineLibraryMethod("name", 0x51, 
                new Func<Microsoft.Scripting.Actions.TypeGroup, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TypeGroupOps.GetName)
            );
            
            module.DefineLibraryMethod("new", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object, System.Object>>, Microsoft.Scripting.Actions.TypeGroup, System.Object[], System.Object>(IronRuby.Builtins.TypeGroupOps.New), 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object, System.Object, System.Object>>, IronRuby.Runtime.BlockParam, Microsoft.Scripting.Actions.TypeGroup, System.Object[], System.Object>(IronRuby.Builtins.TypeGroupOps.New)
            );
            
            module.DefineLibraryMethod("of", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Actions.TypeGroup, System.Object[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.TypeGroupOps.Of), 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Actions.TypeGroup, System.Int32, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.TypeGroupOps.Of)
            );
            
            module.DefineLibraryMethod("superclass", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Actions.TypeGroup, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.TypeGroupOps.GetSuperclass)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<Microsoft.Scripting.Actions.TypeGroup, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TypeGroupOps.GetName)
            );
            
        }
        
        private static void LoadMicrosoft__Scripting__Actions__TypeTracker_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("to_class", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Actions.TypeTracker, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.TypeTrackerOps.ToClass)
            );
            
            module.DefineLibraryMethod("to_module", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.Actions.TypeTracker, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.TypeTrackerOps.ToModule)
            );
            
        }
        
        private static void LoadModule_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("[]", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, System.Object[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.Of), 
                new Func<IronRuby.Builtins.RubyModule, System.Int32, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.Of)
            );
            
            module.DefineLibraryMethod("<", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.IsSubclassOrIncluded), 
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Object>(IronRuby.Builtins.ModuleOps.InvalidComparison)
            );
            
            module.DefineLibraryMethod("<=", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.IsSubclassSameOrIncluded), 
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Object>(IronRuby.Builtins.ModuleOps.InvalidComparison)
            );
            
            module.DefineLibraryMethod("<=>", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.Comparison), 
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Object>(IronRuby.Builtins.ModuleOps.Comparison)
            );
            
            module.DefineLibraryMethod("==", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Boolean>(IronRuby.Builtins.ModuleOps.Equals)
            );
            
            module.DefineLibraryMethod("===", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Boolean>(IronRuby.Builtins.ModuleOps.CaseEquals)
            );
            
            module.DefineLibraryMethod(">", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.IsNotSubclassOrIncluded), 
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Object>(IronRuby.Builtins.ModuleOps.InvalidComparison)
            );
            
            module.DefineLibraryMethod(">=", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.IsNotSubclassSameOrIncluded), 
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Object>(IronRuby.Builtins.ModuleOps.InvalidComparison)
            );
            
            module.DefineLibraryMethod("alias_method", 0x52, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, System.String, System.String, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.AliasMethod)
            );
            
            module.DefineLibraryMethod("ancestors", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.Ancestors)
            );
            
            module.DefineLibraryMethod("append_features", 0x52, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.AppendFeatures)
            );
            
            module.DefineLibraryMethod("attr", 0x52, 
                new Action<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String, System.Boolean>(IronRuby.Builtins.ModuleOps.Attr)
            );
            
            module.DefineLibraryMethod("attr_accessor", 0x52, 
                new Action<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String>(IronRuby.Builtins.ModuleOps.AttrAccessor), 
                new Action<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String[]>(IronRuby.Builtins.ModuleOps.AttrAccessor)
            );
            
            module.DefineLibraryMethod("attr_reader", 0x52, 
                new Action<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String>(IronRuby.Builtins.ModuleOps.AttrReader), 
                new Action<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String[]>(IronRuby.Builtins.ModuleOps.AttrReader)
            );
            
            module.DefineLibraryMethod("attr_writer", 0x52, 
                new Action<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String>(IronRuby.Builtins.ModuleOps.AttrWriter), 
                new Action<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String[]>(IronRuby.Builtins.ModuleOps.AttrWriter)
            );
            
            module.DefineLibraryMethod("autoload", 0x51, 
                new Action<IronRuby.Builtins.RubyModule, System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ModuleOps.SetAutoloadedConstant)
            );
            
            module.DefineLibraryMethod("autoload?", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, System.String, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ModuleOps.GetAutoloadedConstantPath)
            );
            
            module.DefineLibraryMethod("class_eval", 0x51, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.ModuleOps.Evaluate), 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.Evaluate)
            );
            
            module.DefineLibraryMethod("class_exec", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyModule, System.Object[], System.Object>(IronRuby.Builtins.ModuleOps.Execute)
            );
            
            module.DefineLibraryMethod("class_variable_defined?", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Boolean>(IronRuby.Builtins.ModuleOps.IsClassVariableDefined)
            );
            
            module.DefineLibraryMethod("class_variable_get", 0x52, 
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Object>(IronRuby.Builtins.ModuleOps.GetClassVariable)
            );
            
            module.DefineLibraryMethod("class_variable_set", 0x52, 
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Object, System.Object>(IronRuby.Builtins.ModuleOps.ClassVariableSet)
            );
            
            module.DefineLibraryMethod("class_variables", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.ClassVariables)
            );
            
            module.DefineLibraryMethod("const_defined?", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Boolean>(IronRuby.Builtins.ModuleOps.IsConstantDefined)
            );
            
            module.DefineLibraryMethod("const_get", 0x51, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String, System.Object>(IronRuby.Builtins.ModuleOps.GetConstantValue)
            );
            
            module.DefineLibraryMethod("const_missing", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Object>(IronRuby.Builtins.ModuleOps.ConstantMissing)
            );
            
            module.DefineLibraryMethod("const_set", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Object, System.Object>(IronRuby.Builtins.ModuleOps.SetConstantValue)
            );
            
            module.DefineLibraryMethod("constants", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetDefinedConstants)
            );
            
            module.DefineLibraryMethod("define_method", 0x52, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String, IronRuby.Builtins.RubyMethod, IronRuby.Builtins.RubyMethod>(IronRuby.Builtins.ModuleOps.DefineMethod), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, IronRuby.Runtime.ClrName, IronRuby.Builtins.RubyMethod, IronRuby.Builtins.RubyMethod>(IronRuby.Builtins.ModuleOps.DefineMethod), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String, IronRuby.Builtins.UnboundMethod, IronRuby.Builtins.UnboundMethod>(IronRuby.Builtins.ModuleOps.DefineMethod), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, IronRuby.Runtime.ClrName, IronRuby.Builtins.UnboundMethod, IronRuby.Builtins.UnboundMethod>(IronRuby.Builtins.ModuleOps.DefineMethod), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyModule, System.String, IronRuby.Builtins.Proc>(IronRuby.Builtins.ModuleOps.DefineMethod), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyModule, IronRuby.Runtime.ClrName, IronRuby.Builtins.Proc>(IronRuby.Builtins.ModuleOps.DefineMethod), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String, IronRuby.Builtins.Proc, IronRuby.Builtins.Proc>(IronRuby.Builtins.ModuleOps.DefineMethod), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, IronRuby.Runtime.ClrName, IronRuby.Builtins.Proc, IronRuby.Builtins.Proc>(IronRuby.Builtins.ModuleOps.DefineMethod)
            );
            
            module.DefineLibraryMethod("extend_object", 0x52, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.ExtendObject), 
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Object>(IronRuby.Builtins.ModuleOps.ExtendObject)
            );
            
            module.DefineLibraryMethod("extended", 0x52, 
                new Action<IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.ObjectExtended)
            );
            
            module.DefineLibraryMethod("freeze", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.Freeze)
            );
            
            module.DefineLibraryMethod("include", 0x52, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, System.Object>>, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.Include)
            );
            
            module.DefineLibraryMethod("include?", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, System.Boolean>(IronRuby.Builtins.ModuleOps.IncludesModule)
            );
            
            module.DefineLibraryMethod("included", 0x52, 
                new Action<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.Included)
            );
            
            module.DefineLibraryMethod("included_modules", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetIncludedModules)
            );
            
            module.DefineLibraryMethod("initialize", 0x52, 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.Reinitialize)
            );
            
            module.DefineLibraryMethod("initialize_copy", 0x52, 
                new Func<IronRuby.Builtins.RubyModule, System.Object, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.InitializeCopy)
            );
            
            module.DefineLibraryMethod("instance_method", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, System.String, IronRuby.Builtins.UnboundMethod>(IronRuby.Builtins.ModuleOps.GetInstanceMethod)
            );
            
            module.DefineLibraryMethod("instance_methods", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetInstanceMethods), 
                new Func<IronRuby.Builtins.RubyModule, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetInstanceMethods)
            );
            
            module.DefineLibraryMethod("method_added", 0x5a, 
                new Action<System.Object, System.Object>(IronRuby.Builtins.ModuleOps.MethodAdded)
            );
            
            module.DefineLibraryMethod("method_defined?", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Boolean>(IronRuby.Builtins.ModuleOps.MethodDefined)
            );
            
            module.DefineLibraryMethod("method_removed", 0x5a, 
                new Action<System.Object, System.Object>(IronRuby.Builtins.ModuleOps.MethodRemoved)
            );
            
            module.DefineLibraryMethod("method_undefined", 0x5a, 
                new Action<System.Object, System.Object>(IronRuby.Builtins.ModuleOps.MethodUndefined)
            );
            
            module.DefineLibraryMethod("module_eval", 0x51, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.ModuleOps.Evaluate), 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.ModuleOps.Evaluate)
            );
            
            module.DefineLibraryMethod("module_exec", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyModule, System.Object[], System.Object>(IronRuby.Builtins.ModuleOps.Execute)
            );
            
            module.DefineLibraryMethod("module_function", 0x52, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.CopyMethodsToModuleSingleton)
            );
            
            module.DefineLibraryMethod("name", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ModuleOps.GetName)
            );
            
            module.DefineLibraryMethod("of", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, System.Object[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.Of), 
                new Func<IronRuby.Builtins.RubyModule, System.Int32, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.Of)
            );
            
            module.DefineLibraryMethod("private", 0x52, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.SetPrivateVisibility)
            );
            
            module.DefineLibraryMethod("private_class_method", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, System.String[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.MakeClassMethodsPrivate)
            );
            
            module.DefineLibraryMethod("private_instance_methods", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetPrivateInstanceMethods), 
                new Func<IronRuby.Builtins.RubyModule, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetPrivateInstanceMethods)
            );
            
            module.DefineLibraryMethod("private_method_defined?", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Boolean>(IronRuby.Builtins.ModuleOps.PrivateMethodDefined)
            );
            
            module.DefineLibraryMethod("protected", 0x52, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.SetProtectedVisibility)
            );
            
            module.DefineLibraryMethod("protected_instance_methods", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetProtectedInstanceMethods), 
                new Func<IronRuby.Builtins.RubyModule, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetProtectedInstanceMethods)
            );
            
            module.DefineLibraryMethod("protected_method_defined?", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Boolean>(IronRuby.Builtins.ModuleOps.ProtectedMethodDefined)
            );
            
            module.DefineLibraryMethod("public", 0x52, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, System.String[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.SetPublicVisibility)
            );
            
            module.DefineLibraryMethod("public_class_method", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, System.String[], IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.MakeClassMethodsPublic)
            );
            
            module.DefineLibraryMethod("public_instance_methods", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetPublicInstanceMethods), 
                new Func<IronRuby.Builtins.RubyModule, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetPublicInstanceMethods)
            );
            
            module.DefineLibraryMethod("public_method_defined?", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Boolean>(IronRuby.Builtins.ModuleOps.PublicMethodDefined)
            );
            
            module.DefineLibraryMethod("remove_class_variable", 0x52, 
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Object>(IronRuby.Builtins.ModuleOps.RemoveClassVariable)
            );
            
            module.DefineLibraryMethod("remove_const", 0x52, 
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Object>(IronRuby.Builtins.ModuleOps.RemoveConstant)
            );
            
            module.DefineLibraryMethod("remove_method", 0x52, 
                new Func<IronRuby.Builtins.RubyModule, System.String, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.RemoveMethod)
            );
            
            module.DefineLibraryMethod("to_clr_type", 0x51, 
                new Func<IronRuby.Builtins.RubyModule, System.Type>(IronRuby.Builtins.ModuleOps.ToClrType)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ModuleOps.ToS)
            );
            
            module.DefineLibraryMethod("undef_method", 0x52, 
                new Func<IronRuby.Builtins.RubyModule, System.String, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ModuleOps.UndefineMethod)
            );
            
        }
        
        private static void LoadModule_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("constants", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetGlobalConstants)
            );
            
            module.DefineLibraryMethod("nesting", 0x61, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ModuleOps.GetLexicalModuleNesting)
            );
            
        }
        
        private static void LoadNilClass_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("&", 0x51, 
                new Func<System.Object, System.Object, System.Boolean>(IronRuby.Builtins.NilClassOps.And)
            );
            
            module.DefineLibraryMethod("^", 0x51, 
                new Func<System.Object, System.Object, System.Boolean>(IronRuby.Builtins.NilClassOps.Xor), 
                new Func<System.Object, System.Boolean, System.Boolean>(IronRuby.Builtins.NilClassOps.Xor)
            );
            
            module.DefineLibraryMethod("|", 0x51, 
                new Func<System.Object, System.Object, System.Boolean>(IronRuby.Builtins.NilClassOps.Or), 
                new Func<System.Object, System.Boolean, System.Boolean>(IronRuby.Builtins.NilClassOps.Or)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.NilClassOps.Inspect)
            );
            
            module.DefineLibraryMethod("nil?", 0x51, 
                new Func<System.Object, System.Boolean>(IronRuby.Builtins.NilClassOps.IsNil)
            );
            
            module.DefineLibraryMethod("to_a", 0x51, 
                new Func<System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.NilClassOps.ToArray)
            );
            
            module.DefineLibraryMethod("to_f", 0x51, 
                new Func<System.Object, System.Double>(IronRuby.Builtins.NilClassOps.ToDouble)
            );
            
            module.DefineLibraryMethod("to_i", 0x51, 
                new Func<System.Object, System.Int32>(IronRuby.Builtins.NilClassOps.ToInteger)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.NilClassOps.ToString)
            );
            
        }
        
        private static void LoadNoMethodError_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.HideMethod("message");
        }
        
        private static void LoadNumeric_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("-@", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object>(IronRuby.Builtins.Numeric.UnaryMinus)
            );
            
            module.DefineLibraryMethod("+@", 0x51, 
                new Func<System.Object, System.Object>(IronRuby.Builtins.Numeric.UnaryPlus)
            );
            
            module.DefineLibraryMethod("<=>", 0x51, 
                new Func<System.Object, System.Object, System.Object>(IronRuby.Builtins.Numeric.Compare)
            );
            
            module.DefineLibraryMethod("abs", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.UnaryOpStorage, System.Object, System.Object>(IronRuby.Builtins.Numeric.Abs)
            );
            
            module.DefineLibraryMethod("ceil", 0x51, 
                new Func<System.Double, System.Object>(IronRuby.Builtins.Numeric.Ceil)
            );
            
            module.DefineLibraryMethod("coerce", 0x51, 
                new Func<System.Int32, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Numeric.Coerce), 
                new Func<System.Double, System.Double, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Numeric.Coerce), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Double>, IronRuby.Runtime.ConversionStorage<System.Double>, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Numeric.Coerce)
            );
            
            module.DefineLibraryMethod("div", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.ConversionStorage<System.Double>, System.Object, System.Object, System.Object>(IronRuby.Builtins.Numeric.Div)
            );
            
            module.DefineLibraryMethod("divmod", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.ConversionStorage<System.Double>, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.Numeric.DivMod)
            );
            
            module.DefineLibraryMethod("eql?", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.Numeric.Eql)
            );
            
            module.DefineLibraryMethod("floor", 0x51, 
                new Func<System.Double, System.Object>(IronRuby.Builtins.Numeric.Floor)
            );
            
            module.DefineLibraryMethod("integer?", 0x51, 
                new Func<System.Object, System.Boolean>(IronRuby.Builtins.Numeric.IsInteger)
            );
            
            module.DefineLibraryMethod("modulo", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.Numeric.Modulo)
            );
            
            module.DefineLibraryMethod("nonzero?", 0x51, 
                new Func<IronRuby.Runtime.UnaryOpStorage, System.Object, System.Object>(IronRuby.Builtins.Numeric.IsNonZero)
            );
            
            module.DefineLibraryMethod("quo", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.Numeric.Quo)
            );
            
            module.DefineLibraryMethod("remainder", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.Numeric.Remainder)
            );
            
            module.DefineLibraryMethod("round", 0x51, 
                new Func<System.Double, System.Object>(IronRuby.Builtins.Numeric.Round)
            );
            
            module.DefineLibraryMethod("step", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, System.Int32, System.Int32, System.Object>(IronRuby.Builtins.Numeric.Step), 
                new Func<IronRuby.Runtime.BlockParam, System.Int32, System.Int32, System.Int32, System.Object>(IronRuby.Builtins.Numeric.Step), 
                new Func<IronRuby.Runtime.BlockParam, System.Double, System.Double, System.Double, System.Object>(IronRuby.Builtins.Numeric.Step), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.ConversionStorage<System.Double>, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object, System.Object>(IronRuby.Builtins.Numeric.Step)
            );
            
            module.DefineLibraryMethod("to_int", 0x51, 
                new Func<IronRuby.Runtime.UnaryOpStorage, System.Object, System.Object>(IronRuby.Builtins.Numeric.ToInt)
            );
            
            module.DefineLibraryMethod("truncate", 0x51, 
                new Func<System.Double, System.Object>(IronRuby.Builtins.Numeric.Truncate)
            );
            
            module.DefineLibraryMethod("zero?", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Boolean>(IronRuby.Builtins.Numeric.IsZero)
            );
            
        }
        
        private static void LoadObject_Constants(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetBuiltinConstant("FALSE", IronRuby.Builtins.ObjectOps.FALSE);
            module.SetBuiltinConstant("NIL", IronRuby.Builtins.ObjectOps.NIL);
            module.SetBuiltinConstant("TRUE", IronRuby.Builtins.ObjectOps.TRUE);
            
        }
        
        private static void LoadObject_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("initialize", 0x5a, 
                new Action<System.Object>(IronRuby.Builtins.ObjectOps.Reinitialize)
            );
            
        }
        
        private static void LoadObject_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
        }
        
        private static void LoadObjectSpace_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("define_finalizer", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, System.Object, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.ObjectSpace.DefineFinalizer)
            );
            
            module.DefineLibraryMethod("each_object", 0x61, 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyClass, System.Object>(IronRuby.Builtins.ObjectSpace.EachObject)
            );
            
            module.DefineLibraryMethod("garbage_collect", 0x61, 
                new Action<IronRuby.Builtins.RubyModule>(IronRuby.Builtins.ObjectSpace.GarbageCollect)
            );
            
            module.DefineLibraryMethod("undefine_finalizer", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Object>(IronRuby.Builtins.ObjectSpace.DefineFinalizer)
            );
            
        }
        
        private static void LoadPrecision_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("prec", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object, System.Object>>, System.Object, IronRuby.Builtins.RubyClass, System.Object>(IronRuby.Builtins.Precision.Prec)
            );
            
            module.DefineLibraryMethod("prec_f", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.RubyClass, System.Object>>, System.Object, System.Object>(IronRuby.Builtins.Precision.PrecFloat)
            );
            
            module.DefineLibraryMethod("prec_i", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.RubyClass, System.Object>>, System.Object, System.Object>(IronRuby.Builtins.Precision.PrecInteger)
            );
            
        }
        
        private static void LoadPrecision_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("included", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.Precision.Included)
            );
            
        }
        
        private static void LoadProc_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("[]", 0x51, 
                new Func<IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.ProcOps.Call), 
                new Func<IronRuby.Builtins.Proc, System.Object, System.Object>(IronRuby.Builtins.ProcOps.Call), 
                new Func<IronRuby.Builtins.Proc, System.Object, System.Object, System.Object>(IronRuby.Builtins.ProcOps.Call), 
                new Func<IronRuby.Builtins.Proc, System.Object, System.Object, System.Object, System.Object>(IronRuby.Builtins.ProcOps.Call), 
                new Func<IronRuby.Builtins.Proc, System.Object, System.Object, System.Object, System.Object, System.Object>(IronRuby.Builtins.ProcOps.Call), 
                new Func<IronRuby.Builtins.Proc, System.Object[], System.Object>(IronRuby.Builtins.ProcOps.Call)
            );
            
            module.DefineLibraryMethod("==", 0x51, 
                new Func<IronRuby.Builtins.Proc, IronRuby.Builtins.Proc, System.Boolean>(IronRuby.Builtins.ProcOps.Equal), 
                new Func<IronRuby.Builtins.Proc, System.Object, System.Boolean>(IronRuby.Builtins.ProcOps.Equal)
            );
            
            module.DefineLibraryMethod("arity", 0x51, 
                new Func<IronRuby.Builtins.Proc, System.Int32>(IronRuby.Builtins.ProcOps.GetArity)
            );
            
            module.DefineLibraryMethod("binding", 0x51, 
                new Func<IronRuby.Builtins.Proc, IronRuby.Builtins.Binding>(IronRuby.Builtins.ProcOps.GetLocalScope)
            );
            
            module.DefineLibraryMethod("call", 0x51, 
                new Func<IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.ProcOps.Call), 
                new Func<IronRuby.Builtins.Proc, System.Object, System.Object>(IronRuby.Builtins.ProcOps.Call), 
                new Func<IronRuby.Builtins.Proc, System.Object, System.Object, System.Object>(IronRuby.Builtins.ProcOps.Call), 
                new Func<IronRuby.Builtins.Proc, System.Object, System.Object, System.Object, System.Object>(IronRuby.Builtins.ProcOps.Call), 
                new Func<IronRuby.Builtins.Proc, System.Object, System.Object, System.Object, System.Object, System.Object>(IronRuby.Builtins.ProcOps.Call), 
                new Func<IronRuby.Builtins.Proc, System.Object[], System.Object>(IronRuby.Builtins.ProcOps.Call)
            );
            
            module.DefineLibraryMethod("clone", 0x51, 
                new Func<IronRuby.Builtins.Proc, IronRuby.Builtins.Proc>(IronRuby.Builtins.ProcOps.Clone)
            );
            
            module.DefineLibraryMethod("dup", 0x51, 
                new Func<IronRuby.Builtins.Proc, IronRuby.Builtins.Proc>(IronRuby.Builtins.ProcOps.Clone)
            );
            
            module.DefineLibraryMethod("to_proc", 0x51, 
                new Func<IronRuby.Builtins.Proc, IronRuby.Builtins.Proc>(IronRuby.Builtins.ProcOps.ToProc)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<IronRuby.Builtins.Proc, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ProcOps.ToS)
            );
            
        }
        
        private static void LoadProc_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("new", 0x61, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.Proc, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyClass, IronRuby.Builtins.Proc>(IronRuby.Builtins.ProcOps.CreateNew), 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.Proc, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.Proc>(IronRuby.Builtins.ProcOps.CreateNew)
            );
            
        }
        
        #if !SILVERLIGHT
        private static void LoadProcess_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("kill", 0x52, 
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Object, System.Object>(IronRuby.Builtins.RubyProcess.Kill)
            );
            
        }
        #endif
        
        #if !SILVERLIGHT
        private static void LoadProcess_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("euid", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, System.Int32>(IronRuby.Builtins.RubyProcess.EffectiveUserId)
            );
            
            module.DefineLibraryMethod("kill", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Object, System.Object>(IronRuby.Builtins.RubyProcess.Kill)
            );
            
            module.DefineLibraryMethod("pid", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, System.Int32>(IronRuby.Builtins.RubyProcess.GetPid)
            );
            
            module.DefineLibraryMethod("ppid", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, System.Int32>(IronRuby.Builtins.RubyProcess.GetParentPid)
            );
            
            module.DefineLibraryMethod("times", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyStruct>(IronRuby.Builtins.RubyProcess.GetTimes)
            );
            
            module.DefineLibraryMethod("uid", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, System.Int32>(IronRuby.Builtins.RubyProcess.UserId)
            );
            
            module.DefineLibraryMethod("uid=", 0x61, 
                new Action<IronRuby.Builtins.RubyModule, System.Object>(IronRuby.Builtins.RubyProcess.SetUserId)
            );
            
            module.DefineLibraryMethod("wait", 0x61, 
                new Action<IronRuby.Builtins.RubyModule>(IronRuby.Builtins.RubyProcess.Wait)
            );
            
            module.DefineLibraryMethod("wait2", 0x61, 
                new Action<IronRuby.Builtins.RubyModule>(IronRuby.Builtins.RubyProcess.Wait2)
            );
            
            module.DefineLibraryMethod("waitall", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyProcess.Waitall)
            );
            
        }
        #endif
        
        #if !SILVERLIGHT && !SILVERLIGHT
        private static void LoadProcess__Status_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("coredump?", 0x51, 
                new Func<IronRuby.Builtins.RubyProcess.Status, System.Boolean>(IronRuby.Builtins.RubyProcess.Status.CoreDump)
            );
            
            module.DefineLibraryMethod("exited?", 0x51, 
                new Func<IronRuby.Builtins.RubyProcess.Status, System.Boolean>(IronRuby.Builtins.RubyProcess.Status.Exited)
            );
            
            module.DefineLibraryMethod("exitstatus", 0x51, 
                new Func<IronRuby.Builtins.RubyProcess.Status, System.Int32>(IronRuby.Builtins.RubyProcess.Status.ExitStatus)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Builtins.RubyProcess.Status, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyProcess.Status.Inspect)
            );
            
            module.DefineLibraryMethod("pid", 0x51, 
                new Func<IronRuby.Builtins.RubyProcess.Status, System.Int32>(IronRuby.Builtins.RubyProcess.Status.Pid)
            );
            
            module.DefineLibraryMethod("stopped?", 0x51, 
                new Func<IronRuby.Builtins.RubyProcess.Status, System.Boolean>(IronRuby.Builtins.RubyProcess.Status.Stopped)
            );
            
            module.DefineLibraryMethod("stopsig", 0x51, 
                new Func<IronRuby.Builtins.RubyProcess.Status, System.Object>(IronRuby.Builtins.RubyProcess.Status.StopSig)
            );
            
            module.DefineLibraryMethod("success?", 0x51, 
                new Func<IronRuby.Builtins.RubyProcess.Status, System.Boolean>(IronRuby.Builtins.RubyProcess.Status.Success)
            );
            
            module.DefineLibraryMethod("termsig", 0x51, 
                new Func<IronRuby.Builtins.RubyProcess.Status, System.Object>(IronRuby.Builtins.RubyProcess.Status.TermSig)
            );
            
        }
        #endif
        
        #if !SILVERLIGHT && !SILVERLIGHT
        private static void LoadProcess__Status_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.HideMethod("new");
        }
        #endif
        
        private static void LoadRange_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("==", 0x51, 
                new Func<IronRuby.Builtins.Range, System.Object, System.Boolean>(IronRuby.Builtins.RangeOps.Equals), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Builtins.Range, IronRuby.Builtins.Range, System.Boolean>(IronRuby.Builtins.RangeOps.Equals)
            );
            
            module.DefineLibraryMethod("===", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Builtins.Range, System.Object, System.Boolean>(IronRuby.Builtins.RangeOps.CaseEquals)
            );
            
            module.DefineLibraryMethod("begin", 0x51, 
                new Func<IronRuby.Builtins.Range, System.Object>(IronRuby.Builtins.RangeOps.Begin)
            );
            
            module.DefineLibraryMethod("each", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BlockParam, IronRuby.Builtins.Range, System.Object>(IronRuby.Builtins.RangeOps.Each)
            );
            
            module.DefineLibraryMethod("end", 0x51, 
                new Func<IronRuby.Builtins.Range, System.Object>(IronRuby.Builtins.RangeOps.End)
            );
            
            module.DefineLibraryMethod("eql?", 0x51, 
                new Func<IronRuby.Builtins.Range, System.Object, System.Boolean>(IronRuby.Builtins.RangeOps.Equals), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Builtins.Range, IronRuby.Builtins.Range, System.Boolean>(IronRuby.Builtins.RangeOps.Eql)
            );
            
            module.DefineLibraryMethod("exclude_end?", 0x51, 
                new Func<IronRuby.Builtins.Range, System.Boolean>(IronRuby.Builtins.RangeOps.ExcludeEnd)
            );
            
            module.DefineLibraryMethod("first", 0x51, 
                new Func<IronRuby.Builtins.Range, System.Object>(IronRuby.Builtins.RangeOps.Begin)
            );
            
            module.DefineLibraryMethod("hash", 0x51, 
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Builtins.Range, System.Int32>(IronRuby.Builtins.RangeOps.GetHashCode)
            );
            
            module.DefineLibraryMethod("include?", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Builtins.Range, System.Object, System.Boolean>(IronRuby.Builtins.RangeOps.CaseEquals)
            );
            
            module.DefineLibraryMethod("initialize", 0x52, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.Builtins.Range, System.Object, System.Object, System.Boolean, IronRuby.Builtins.Range>(IronRuby.Builtins.RangeOps.Reinitialize)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.Range, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RangeOps.Inspect)
            );
            
            module.DefineLibraryMethod("last", 0x51, 
                new Func<IronRuby.Builtins.Range, System.Object>(IronRuby.Builtins.RangeOps.End)
            );
            
            module.DefineLibraryMethod("member?", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Builtins.Range, System.Object, System.Boolean>(IronRuby.Builtins.RangeOps.CaseEquals)
            );
            
            module.DefineLibraryMethod("step", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BlockParam, IronRuby.Builtins.Range, System.Object, System.Object>(IronRuby.Builtins.RangeOps.Step)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Builtins.Range, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RangeOps.ToS)
            );
            
        }
        
        private static void LoadRangeError_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.HideMethod("message");
        }
        
        private static void LoadRegexp_Constants(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetBuiltinConstant("EXTENDED", IronRuby.Builtins.RegexpOps.EXTENDED);
            module.SetBuiltinConstant("IGNORECASE", IronRuby.Builtins.RegexpOps.IGNORECASE);
            module.SetBuiltinConstant("MULTILINE", IronRuby.Builtins.RegexpOps.MULTILINE);
            
        }
        
        private static void LoadRegexp_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("~", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyRegex, System.Object>(IronRuby.Builtins.RegexpOps.ImplicitMatch)
            );
            
            module.DefineLibraryMethod("=~", 0x51, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RegexpOps.MatchIndex)
            );
            
            module.DefineLibraryMethod("==", 0x51, 
                new Func<IronRuby.Builtins.RubyRegex, System.Object, System.Boolean>(IronRuby.Builtins.RegexpOps.Equals), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.RubyRegex, System.Boolean>(IronRuby.Builtins.RegexpOps.Equals)
            );
            
            module.DefineLibraryMethod("===", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyRegex, System.Object, System.Boolean>(IronRuby.Builtins.RegexpOps.CaseCompare)
            );
            
            module.DefineLibraryMethod("casefold?", 0x51, 
                new Func<IronRuby.Builtins.RubyRegex, System.Boolean>(IronRuby.Builtins.RegexpOps.IsCaseInsensitive)
            );
            
            module.DefineLibraryMethod("encoding", 0x51, 
                new Func<IronRuby.Builtins.RubyRegex, IronRuby.Builtins.RubyEncoding>(IronRuby.Builtins.RegexpOps.GetEncoding)
            );
            
            module.DefineLibraryMethod("eql?", 0x51, 
                new Func<IronRuby.Builtins.RubyRegex, System.Object, System.Boolean>(IronRuby.Builtins.RegexpOps.Equals), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.RubyRegex, System.Boolean>(IronRuby.Builtins.RegexpOps.Equals)
            );
            
            module.DefineLibraryMethod("hash", 0x51, 
                new Func<IronRuby.Builtins.RubyRegex, System.Int32>(IronRuby.Builtins.RegexpOps.GetHash)
            );
            
            module.DefineLibraryMethod("initialize", 0x52, 
                new Func<IronRuby.Builtins.RubyRegex, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Reinitialize), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.RubyRegex, System.Int32, System.Object, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Reinitialize), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.RubyRegex, System.Object, System.Object, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Reinitialize), 
                new Func<IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Reinitialize), 
                new Func<IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, System.Boolean, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Reinitialize)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RegexpOps.Inspect)
            );
            
            module.DefineLibraryMethod("kcode", 0x51, 
                new Func<IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RegexpOps.GetKCode)
            );
            
            module.DefineLibraryMethod("match", 0x51, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, IronRuby.Builtins.MatchData>(IronRuby.Builtins.RegexpOps.Match)
            );
            
            module.DefineLibraryMethod("options", 0x51, 
                new Func<IronRuby.Builtins.RubyRegex, System.Int32>(IronRuby.Builtins.RegexpOps.GetOptions)
            );
            
            module.DefineLibraryMethod("source", 0x51, 
                new Func<IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RegexpOps.Source)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RegexpOps.ToS)
            );
            
        }
        
        private static void LoadRegexp_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineRuleGenerator("compile", 0x61, IronRuby.Builtins.RegexpOps.Compile());
            
            module.DefineLibraryMethod("escape", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RegexpOps.Escape)
            );
            
            module.DefineLibraryMethod("last_match", 0x61, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MatchData>(IronRuby.Builtins.RegexpOps.LastMatch), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyClass, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RegexpOps.LastMatch)
            );
            
            module.DefineLibraryMethod("quote", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RegexpOps.Escape)
            );
            
            module.DefineLibraryMethod("union", 0x61, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Builtins.RubyClass, System.Object[], IronRuby.Builtins.RubyRegex>(IronRuby.Builtins.RegexpOps.Union)
            );
            
        }
        
        #if !SILVERLIGHT
        private static void LoadSignal_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("list", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyModule, IronRuby.Builtins.Hash>(IronRuby.Builtins.Signal.List)
            );
            
            module.DefineLibraryMethod("trap", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Object, IronRuby.Builtins.Proc, System.Object>(IronRuby.Builtins.Signal.Trap), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object, System.Object>(IronRuby.Builtins.Signal.Trap)
            );
            
        }
        #endif
        
        private static void LoadString_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("%", 0x51, 
                new Func<IronRuby.Builtins.StringFormatterSiteStorage, IronRuby.Builtins.MutableString, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Format)
            );
            
            module.DefineLibraryMethod("*", 0x51, 
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Repeat)
            );
            
            module.DefineLibraryMethod("[]", 0x51, 
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.GetChar), 
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.MutableString, IronRuby.Builtins.Range, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring), 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring)
            );
            
            module.DefineLibraryMethod("[]=", 0x51, 
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ReplaceCharacter), 
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Int32, System.Int32>(IronRuby.Builtins.MutableStringOps.SetCharacter), 
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ReplaceSubstring), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.MutableString, IronRuby.Builtins.Range, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ReplaceSubstring), 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ReplaceSubstring), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ReplaceSubstring)
            );
            
            module.DefineLibraryMethod("+", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Concatenate)
            );
            
            module.DefineLibraryMethod("<<", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Append), 
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Append)
            );
            
            module.DefineLibraryMethod("<=>", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.MutableStringOps.Compare), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RespondToStorage, System.Object, System.Object, System.Object>(IronRuby.Builtins.MutableStringOps.Compare)
            );
            
            module.DefineLibraryMethod("=~", 0x51, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Object>(IronRuby.Builtins.MutableStringOps.Match), 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.Match), 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, System.Object>>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, System.Object, System.Object>(IronRuby.Builtins.MutableStringOps.Match)
            );
            
            module.DefineLibraryMethod("==", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.MutableStringOps.StringEquals), 
                new Func<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.MutableStringOps.Equals)
            );
            
            module.DefineLibraryMethod("===", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.MutableStringOps.StringEquals), 
                new Func<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Boolean>(IronRuby.Builtins.MutableStringOps.Equals)
            );
            
            module.DefineLibraryMethod("capitalize", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Capitalize)
            );
            
            module.DefineLibraryMethod("capitalize!", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.CapitalizeInPlace)
            );
            
            module.DefineLibraryMethod("casecmp", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.MutableStringOps.Casecmp)
            );
            
            module.DefineLibraryMethod("center", 0x51, 
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Center)
            );
            
            module.DefineLibraryMethod("chomp", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Chomp), 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Chomp)
            );
            
            module.DefineLibraryMethod("chomp!", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ChompInPlace), 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ChompInPlace)
            );
            
            module.DefineLibraryMethod("chop", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Chop)
            );
            
            module.DefineLibraryMethod("chop!", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ChopInPlace)
            );
            
            module.DefineLibraryMethod("concat", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Append), 
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Append)
            );
            
            module.DefineLibraryMethod("count", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString[], System.Object>(IronRuby.Builtins.MutableStringOps.Count)
            );
            
            module.DefineLibraryMethod("delete", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString[], IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Delete)
            );
            
            module.DefineLibraryMethod("delete!", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString[], IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.DeleteInPlace)
            );
            
            module.DefineLibraryMethod("downcase", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.DownCase)
            );
            
            module.DefineLibraryMethod("downcase!", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.DownCaseInPlace)
            );
            
            module.DefineLibraryMethod("dump", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Dump)
            );
            
            module.DefineLibraryMethod("each", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.EachLine), 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.EachLine)
            );
            
            module.DefineLibraryMethod("each_byte", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.EachByte)
            );
            
            module.DefineLibraryMethod("each_line", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.EachLine), 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.EachLine)
            );
            
            module.DefineLibraryMethod("empty?", 0x51, 
                new Func<IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.MutableStringOps.IsEmpty)
            );
            
            module.DefineLibraryMethod("encoding", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyEncoding>(IronRuby.Builtins.MutableStringOps.GetEncoding)
            );
            
            module.DefineLibraryMethod("eql?", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.MutableStringOps.Eql), 
                new Func<IronRuby.Builtins.MutableString, System.Object, System.Boolean>(IronRuby.Builtins.MutableStringOps.Eql)
            );
            
            module.DefineLibraryMethod("gsub", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Object>(IronRuby.Builtins.MutableStringOps.BlockReplaceAll), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.BlockReplaceAll), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ReplaceAll)
            );
            
            module.DefineLibraryMethod("gsub!", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Object>(IronRuby.Builtins.MutableStringOps.BlockReplaceAllInPlace), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ReplaceAllInPlace)
            );
            
            module.DefineLibraryMethod("hex", 0x51, 
                new Func<IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.ToIntegerHex)
            );
            
            module.DefineLibraryMethod("include?", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Boolean>(IronRuby.Builtins.MutableStringOps.Include), 
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Boolean>(IronRuby.Builtins.MutableStringOps.Include)
            );
            
            module.DefineLibraryMethod("index", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.Index), 
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.Index), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.Index)
            );
            
            module.DefineLibraryMethod("initialize", 0x52, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Reinitialize), 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Reinitialize)
            );
            
            module.DefineLibraryMethod("initialize_copy", 0x52, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Reinitialize)
            );
            
            module.DefineLibraryMethod("insert", 0x51, 
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Insert)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Inspect)
            );
            
            module.DefineLibraryMethod("intern", 0x51, 
                new Func<IronRuby.Builtins.MutableString, Microsoft.Scripting.SymbolId>(IronRuby.Builtins.MutableStringOps.ToSymbol)
            );
            
            module.DefineLibraryMethod("length", 0x51, 
                new Func<IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.MutableStringOps.GetLength)
            );
            
            module.DefineLibraryMethod("ljust", 0x51, 
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.LeftJustify), 
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.LeftJustify)
            );
            
            module.DefineLibraryMethod("lstrip", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.StripLeft)
            );
            
            module.DefineLibraryMethod("lstrip!", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.StripLeftInPlace)
            );
            
            module.DefineLibraryMethod("match", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, System.Object>>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Object>(IronRuby.Builtins.MutableStringOps.MatchRegexp), 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Runtime.RubyScope, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, System.Object>>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.MatchObject)
            );
            
            module.DefineLibraryMethod("next", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Succ)
            );
            
            module.DefineLibraryMethod("next!", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.SuccInPlace)
            );
            
            module.DefineLibraryMethod("oct", 0x51, 
                new Func<IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.ToIntegerOctal)
            );
            
            module.DefineLibraryMethod("replace", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Replace)
            );
            
            module.DefineLibraryMethod("reverse", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetReversed)
            );
            
            module.DefineLibraryMethod("reverse!", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Reverse)
            );
            
            module.DefineLibraryMethod("rindex", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.LastIndexOf), 
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.LastIndexOf), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.LastIndexOf)
            );
            
            module.DefineLibraryMethod("rjust", 0x51, 
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.RightJustify), 
                new Func<IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.RightJustify)
            );
            
            module.DefineLibraryMethod("rstrip", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.StripRight)
            );
            
            module.DefineLibraryMethod("rstrip!", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.StripRightInPlace)
            );
            
            module.DefineLibraryMethod("scan", 0x51, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MutableStringOps.Scan), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Object>(IronRuby.Builtins.MutableStringOps.Scan)
            );
            
            module.DefineLibraryMethod("size", 0x51, 
                new Func<IronRuby.Builtins.MutableString, System.Int32>(IronRuby.Builtins.MutableStringOps.GetLength)
            );
            
            module.DefineLibraryMethod("slice", 0x51, 
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.GetChar), 
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.MutableString, IronRuby.Builtins.Range, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring), 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetSubstring)
            );
            
            module.DefineLibraryMethod("slice!", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.RemoveCharInPlace), 
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.RemoveSubstringInPlace), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.MutableString, IronRuby.Builtins.Range, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.RemoveSubstringInPlace), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.RemoveSubstringInPlace), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.RemoveSubstringInPlace), 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.RemoveSubstringInPlace)
            );
            
            module.DefineLibraryMethod("split", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MutableStringOps.Split), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MutableStringOps.Split), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MutableStringOps.Split)
            );
            
            module.DefineLibraryMethod("squeeze", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString[], IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Squeeze)
            );
            
            module.DefineLibraryMethod("squeeze!", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString[], IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.SqueezeInPlace)
            );
            
            module.DefineLibraryMethod("strip", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Strip)
            );
            
            module.DefineLibraryMethod("strip!", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.StripInPlace)
            );
            
            module.DefineLibraryMethod("sub", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Object>(IronRuby.Builtins.MutableStringOps.BlockReplaceFirst), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.BlockReplaceFirst), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ReplaceFirst)
            );
            
            module.DefineLibraryMethod("sub!", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, System.Object>(IronRuby.Builtins.MutableStringOps.BlockReplaceFirstInPlace), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ReplaceFirstInPlace)
            );
            
            module.DefineLibraryMethod("succ", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Succ)
            );
            
            module.DefineLibraryMethod("succ!", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.SuccInPlace)
            );
            
            module.DefineLibraryMethod("sum", 0x51, 
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.GetChecksum)
            );
            
            module.DefineLibraryMethod("swapcase", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.SwapCase)
            );
            
            module.DefineLibraryMethod("swapcase!", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.SwapCaseInPlace)
            );
            
            module.DefineLibraryMethod("to_clr_string", 0x51, 
                new Func<IronRuby.Builtins.MutableString, System.String>(IronRuby.Builtins.MutableStringOps.ToClrString)
            );
            
            module.DefineLibraryMethod("to_f", 0x51, 
                new Func<IronRuby.Builtins.MutableString, System.Double>(IronRuby.Builtins.MutableStringOps.ToDouble)
            );
            
            module.DefineLibraryMethod("to_i", 0x51, 
                new Func<IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.Builtins.MutableStringOps.ToInteger)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ToS)
            );
            
            module.DefineLibraryMethod("to_str", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.ToS)
            );
            
            module.DefineLibraryMethod("to_sym", 0x51, 
                new Func<IronRuby.Builtins.MutableString, Microsoft.Scripting.SymbolId>(IronRuby.Builtins.MutableStringOps.ToSymbol)
            );
            
            module.DefineLibraryMethod("tr", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.GetTranslated)
            );
            
            module.DefineLibraryMethod("tr!", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.Translate)
            );
            
            module.DefineLibraryMethod("tr_s", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.TrSqueeze)
            );
            
            module.DefineLibraryMethod("tr_s!", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.TrSqueezeInPlace)
            );
            
            module.DefineLibraryMethod("unpack", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.MutableStringOps.Unpack)
            );
            
            module.DefineLibraryMethod("upcase", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.UpCase)
            );
            
            module.DefineLibraryMethod("upcase!", 0x51, 
                new Func<IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.MutableStringOps.UpCaseInPlace)
            );
            
            module.DefineLibraryMethod("upto", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BlockParam, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.MutableStringOps.UpTo)
            );
            
        }
        
        private static void LoadStruct_Constants(IronRuby.Builtins.RubyModule/*!*/ module) {
            #if !SILVERLIGHT
            module.SetBuiltinConstant("Tms", IronRuby.Builtins.RubyStructOps.CreateTmsClass(module));
            #endif
            
        }
        
        private static void LoadStruct_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("[]", 0x51, 
                new Func<IronRuby.Builtins.RubyStruct, System.Int32, System.Object>(IronRuby.Builtins.RubyStructOps.GetValue), 
                new Func<IronRuby.Builtins.RubyStruct, Microsoft.Scripting.SymbolId, System.Object>(IronRuby.Builtins.RubyStructOps.GetValue), 
                new Func<IronRuby.Builtins.RubyStruct, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.RubyStructOps.GetValue), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyStruct, System.Object, System.Object>(IronRuby.Builtins.RubyStructOps.GetValue)
            );
            
            module.DefineLibraryMethod("[]=", 0x51, 
                new Func<IronRuby.Builtins.RubyStruct, System.Int32, System.Object, System.Object>(IronRuby.Builtins.RubyStructOps.SetValue), 
                new Func<IronRuby.Builtins.RubyStruct, Microsoft.Scripting.SymbolId, System.Object, System.Object>(IronRuby.Builtins.RubyStructOps.SetValue), 
                new Func<IronRuby.Builtins.RubyStruct, IronRuby.Builtins.MutableString, System.Object, System.Object>(IronRuby.Builtins.RubyStructOps.SetValue), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyStruct, System.Object, System.Object, System.Object>(IronRuby.Builtins.RubyStructOps.SetValue)
            );
            
            module.DefineLibraryMethod("==", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Builtins.RubyStruct, System.Object, System.Boolean>(IronRuby.Builtins.RubyStructOps.Equals)
            );
            
            module.DefineLibraryMethod("each", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyStruct, System.Object>(IronRuby.Builtins.RubyStructOps.Each)
            );
            
            module.DefineLibraryMethod("each_pair", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyStruct, System.Object>(IronRuby.Builtins.RubyStructOps.EachPair)
            );
            
            module.DefineLibraryMethod("eql?", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Builtins.RubyStruct, System.Object, System.Boolean>(IronRuby.Builtins.RubyStructOps.Equal)
            );
            
            module.DefineLibraryMethod("hash", 0x51, 
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyStruct, System.Int32>(IronRuby.Builtins.RubyStructOps.Hash)
            );
            
            module.DefineLibraryMethod("initialize", 0x52, 
                new Action<IronRuby.Builtins.RubyStruct, System.Object[]>(IronRuby.Builtins.RubyStructOps.Reinitialize)
            );
            
            module.DefineLibraryMethod("initialize_copy", 0x52, 
                new Func<IronRuby.Builtins.RubyStruct, IronRuby.Builtins.RubyStruct, IronRuby.Builtins.RubyStruct>(IronRuby.Builtins.RubyStructOps.InitializeCopy)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Builtins.RubyStruct, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyStructOps.Inspect)
            );
            
            module.DefineLibraryMethod("length", 0x51, 
                new Func<IronRuby.Builtins.RubyStruct, System.Int32>(IronRuby.Builtins.RubyStructOps.GetSize)
            );
            
            module.DefineLibraryMethod("members", 0x51, 
                new Func<IronRuby.Builtins.RubyStruct, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyStructOps.GetMembers)
            );
            
            module.DefineLibraryMethod("select", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyStruct, System.Object>(IronRuby.Builtins.RubyStructOps.Select)
            );
            
            module.DefineLibraryMethod("size", 0x51, 
                new Func<IronRuby.Builtins.RubyStruct, System.Int32>(IronRuby.Builtins.RubyStructOps.GetSize)
            );
            
            module.DefineLibraryMethod("to_a", 0x51, 
                new Func<IronRuby.Builtins.RubyStruct, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyStructOps.Values)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<IronRuby.Builtins.RubyStruct, IronRuby.Builtins.MutableString>(IronRuby.Builtins.RubyStructOps.Inspect)
            );
            
            module.DefineLibraryMethod("values", 0x51, 
                new Func<IronRuby.Builtins.RubyStruct, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyStructOps.Values)
            );
            
            module.DefineLibraryMethod("values_at", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyStruct, System.Object[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.RubyStructOps.ValuesAt)
            );
            
        }
        
        private static void LoadStruct_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("new", 0x61, 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, Microsoft.Scripting.SymbolId, System.String[], System.Object>(IronRuby.Builtins.RubyStructOps.NewAnonymousStruct), 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, System.String, System.String[], System.Object>(IronRuby.Builtins.RubyStructOps.NewAnonymousStruct), 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.String[], System.Object>(IronRuby.Builtins.RubyStructOps.NewStruct)
            );
            
        }
        
        private static void LoadSymbol_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.HideMethod("==");
            module.DefineLibraryMethod("id2name", 0x51, 
                new Func<Microsoft.Scripting.SymbolId, IronRuby.Builtins.MutableString>(IronRuby.Builtins.SymbolOps.ToString)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, Microsoft.Scripting.SymbolId, IronRuby.Builtins.MutableString>(IronRuby.Builtins.SymbolOps.Inspect)
            );
            
            module.DefineLibraryMethod("to_clr_string", 0x51, 
                new Func<Microsoft.Scripting.SymbolId, System.String>(IronRuby.Builtins.SymbolOps.ToClrString)
            );
            
            module.DefineLibraryMethod("to_i", 0x51, 
                new Func<Microsoft.Scripting.SymbolId, System.Int32>(IronRuby.Builtins.SymbolOps.ToInteger)
            );
            
            module.DefineLibraryMethod("to_int", 0x51, 
                new Func<Microsoft.Scripting.SymbolId, System.Int32>(IronRuby.Builtins.SymbolOps.ToInteger)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<Microsoft.Scripting.SymbolId, IronRuby.Builtins.MutableString>(IronRuby.Builtins.SymbolOps.ToString)
            );
            
            module.DefineLibraryMethod("to_sym", 0x51, 
                new Func<Microsoft.Scripting.SymbolId, Microsoft.Scripting.SymbolId>(IronRuby.Builtins.SymbolOps.ToSymbol)
            );
            
        }
        
        private static void LoadSymbol_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("all_symbols", 0x61, 
                new Func<System.Object, System.Collections.Generic.List<System.Object>>(IronRuby.Builtins.SymbolOps.GetAllSymbols)
            );
            
        }
        
        private static void LoadSystem__Byte_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            LoadIronRuby__Clr__Integer_Instance(module);
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ByteOps.Inspect)
            );
            
            module.DefineLibraryMethod("size", 0x51, 
                new Func<System.Byte, System.Int32>(IronRuby.Builtins.ByteOps.Size)
            );
            
        }
        
        private static void LoadSystem__Byte_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("induced_from", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Byte>(IronRuby.Builtins.ByteOps.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, System.Byte>(IronRuby.Builtins.ByteOps.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, System.Double, System.Byte>(IronRuby.Builtins.ByteOps.InducedFrom)
            );
            
        }
        
        private static void LoadSystem__Char_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<System.Char, IronRuby.Builtins.MutableString>(IronRuby.Builtins.CharOps.Inspect)
            );
            
        }
        
        private static void LoadSystem__Collections__Generic__IDictionary_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("[]", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Object>(IronRuby.Builtins.IDictionaryOps.GetElement)
            );
            
            module.DefineLibraryMethod("[]=", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Object, System.Object>(IronRuby.Builtins.IDictionaryOps.SetElement)
            );
            
            module.DefineLibraryMethod("==", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Boolean>(IronRuby.Builtins.IDictionaryOps.Equals)
            );
            
            module.DefineLibraryMethod("clear", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Collections.Generic.IDictionary<System.Object, System.Object>>(IronRuby.Builtins.IDictionaryOps.Clear)
            );
            
            module.DefineLibraryMethod("default", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Object>(IronRuby.Builtins.IDictionaryOps.GetDefaultValue)
            );
            
            module.DefineLibraryMethod("default_proc", 0x51, 
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.Proc>(IronRuby.Builtins.IDictionaryOps.GetDefaultProc)
            );
            
            module.DefineLibraryMethod("delete", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Object>(IronRuby.Builtins.IDictionaryOps.Delete)
            );
            
            module.DefineLibraryMethod("delete_if", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.DeleteIf)
            );
            
            module.DefineLibraryMethod("each", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.Each)
            );
            
            module.DefineLibraryMethod("each_key", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.EachKey)
            );
            
            module.DefineLibraryMethod("each_pair", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.EachPair)
            );
            
            module.DefineLibraryMethod("each_value", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.EachValue)
            );
            
            module.DefineLibraryMethod("empty?", 0x51, 
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Boolean>(IronRuby.Builtins.IDictionaryOps.Empty)
            );
            
            module.DefineLibraryMethod("fetch", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Object, System.Object>(IronRuby.Builtins.IDictionaryOps.Fetch)
            );
            
            module.DefineLibraryMethod("has_key?", 0x51, 
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Boolean>(IronRuby.Builtins.IDictionaryOps.HasKey)
            );
            
            module.DefineLibraryMethod("has_value?", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Boolean>(IronRuby.Builtins.IDictionaryOps.HasValue)
            );
            
            module.DefineLibraryMethod("include?", 0x51, 
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Boolean>(IronRuby.Builtins.IDictionaryOps.HasKey)
            );
            
            module.DefineLibraryMethod("index", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Object>(IronRuby.Builtins.IDictionaryOps.Index)
            );
            
            module.DefineLibraryMethod("indexes", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IDictionaryOps.Indexes)
            );
            
            module.DefineLibraryMethod("indices", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IDictionaryOps.Indexes)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.MutableString>(IronRuby.Builtins.IDictionaryOps.Inspect)
            );
            
            module.DefineLibraryMethod("invert", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.Hash>(IronRuby.Builtins.IDictionaryOps.Invert)
            );
            
            module.DefineLibraryMethod("key?", 0x51, 
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Boolean>(IronRuby.Builtins.IDictionaryOps.HasKey)
            );
            
            module.DefineLibraryMethod("keys", 0x51, 
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IDictionaryOps.GetKeys)
            );
            
            module.DefineLibraryMethod("length", 0x51, 
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Int32>(IronRuby.Builtins.IDictionaryOps.Length)
            );
            
            module.DefineLibraryMethod("member?", 0x51, 
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Boolean>(IronRuby.Builtins.IDictionaryOps.HasKey)
            );
            
            module.DefineLibraryMethod("merge", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.Merge)
            );
            
            module.DefineLibraryMethod("merge!", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.Update)
            );
            
            module.DefineLibraryMethod("rehash", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Collections.Generic.IDictionary<System.Object, System.Object>>(IronRuby.Builtins.IDictionaryOps.Rehash)
            );
            
            module.DefineLibraryMethod("reject", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.Reject)
            );
            
            module.DefineLibraryMethod("reject!", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.RejectMutate)
            );
            
            module.DefineLibraryMethod("replace", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.Hash, System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.Hash>(IronRuby.Builtins.IDictionaryOps.Replace)
            );
            
            module.DefineLibraryMethod("select", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.Select)
            );
            
            module.DefineLibraryMethod("shift", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.Shift)
            );
            
            module.DefineLibraryMethod("size", 0x51, 
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Int32>(IronRuby.Builtins.IDictionaryOps.Length)
            );
            
            module.DefineLibraryMethod("sort", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.Sort)
            );
            
            module.DefineLibraryMethod("store", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Object, System.Object>(IronRuby.Builtins.IDictionaryOps.SetElement)
            );
            
            module.DefineLibraryMethod("to_a", 0x51, 
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IDictionaryOps.ToArray)
            );
            
            module.DefineLibraryMethod("to_hash", 0x51, 
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Collections.Generic.IDictionary<System.Object, System.Object>>(IronRuby.Builtins.IDictionaryOps.ToHash)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.MutableString>(IronRuby.Builtins.IDictionaryOps.ToMutableString)
            );
            
            module.DefineLibraryMethod("update", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object>(IronRuby.Builtins.IDictionaryOps.Update)
            );
            
            module.DefineLibraryMethod("value?", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object, System.Boolean>(IronRuby.Builtins.IDictionaryOps.HasValue)
            );
            
            module.DefineLibraryMethod("values", 0x51, 
                new Func<System.Collections.Generic.IDictionary<System.Object, System.Object>, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IDictionaryOps.GetValues)
            );
            
            module.DefineLibraryMethod("values_at", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Collections.Generic.IDictionary<System.Object, System.Object>, System.Object[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IDictionaryOps.ValuesAt)
            );
            
        }
        
        private static void LoadSystem__Collections__IEnumerable_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("each", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, System.Collections.IEnumerable, System.Object>(IronRuby.Builtins.IEnumerableOps.Each)
            );
            
        }
        
        private static void LoadSystem__Collections__IList_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("-", 0x51, 
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Collections.IList, System.Collections.IList, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IListOps.Difference)
            );
            
            module.DefineLibraryMethod("&", 0x51, 
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Collections.IList, System.Collections.IList, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IListOps.Intersection)
            );
            
            module.DefineLibraryMethod("*", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, System.Collections.IList, System.Int32, System.Collections.IList>(IronRuby.Builtins.IListOps.Repetition), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, System.Collections.IList, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.IListOps.Repetition), 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, System.Collections.IList, IronRuby.Runtime.Union<IronRuby.Builtins.MutableString, System.Int32>, System.Object>(IronRuby.Builtins.IListOps.Repetition)
            );
            
            module.DefineLibraryMethod("[]", 0x51, 
                new Func<System.Collections.IList, System.Int32, System.Object>(IronRuby.Builtins.IListOps.GetElement), 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, System.Collections.IList, System.Int32, System.Int32, System.Collections.IList>(IronRuby.Builtins.IListOps.GetElements), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, System.Collections.IList, IronRuby.Builtins.Range, System.Collections.IList>(IronRuby.Builtins.IListOps.GetElement)
            );
            
            module.DefineLibraryMethod("[]=", 0x51, 
                new Func<IronRuby.Builtins.RubyArray, System.Int32, System.Object, System.Object>(IronRuby.Builtins.IListOps.SetElement), 
                new Func<System.Collections.IList, System.Int32, System.Object, System.Object>(IronRuby.Builtins.IListOps.SetElement), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Collections.IList>, System.Collections.IList, System.Int32, System.Int32, System.Object, System.Object>(IronRuby.Builtins.IListOps.SetElement), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Collections.IList>, IronRuby.Runtime.ConversionStorage<System.Int32>, System.Collections.IList, IronRuby.Builtins.Range, System.Object, System.Object>(IronRuby.Builtins.IListOps.SetElement)
            );
            
            module.DefineLibraryMethod("|", 0x51, 
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Collections.IList, System.Collections.IList, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IListOps.Union)
            );
            
            module.DefineLibraryMethod("+", 0x51, 
                new Func<System.Collections.IList, System.Collections.IList, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IListOps.Concatenate)
            );
            
            module.DefineLibraryMethod("<<", 0x51, 
                new Func<System.Collections.IList, System.Object, System.Collections.IList>(IronRuby.Builtins.IListOps.Append)
            );
            
            module.DefineLibraryMethod("<=>", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Collections.IList, System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.Compare)
            );
            
            module.DefineLibraryMethod("==", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Collections.IList>, IronRuby.Runtime.BinaryOpStorage, System.Collections.IList, System.Object, System.Boolean>(IronRuby.Builtins.IListOps.Equals), 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Collections.IList, System.Collections.IList, System.Boolean>(IronRuby.Builtins.IListOps.Equals)
            );
            
            module.DefineLibraryMethod("assoc", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Collections.IList, System.Object, System.Collections.IList>(IronRuby.Builtins.IListOps.GetContainerOfFirstItem)
            );
            
            module.DefineLibraryMethod("at", 0x51, 
                new Func<System.Collections.IList, System.Int32, System.Object>(IronRuby.Builtins.IListOps.At)
            );
            
            module.DefineLibraryMethod("clear", 0x51, 
                new Func<System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.Clear)
            );
            
            module.DefineLibraryMethod("collect!", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.CollectInPlace)
            );
            
            module.DefineLibraryMethod("compact", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.Compact)
            );
            
            module.DefineLibraryMethod("compact!", 0x51, 
                new Func<System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.CompactInPlace)
            );
            
            module.DefineLibraryMethod("concat", 0x51, 
                new Func<System.Collections.IList, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.Concat)
            );
            
            module.DefineLibraryMethod("delete", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Collections.IList, System.Object, System.Object>(IronRuby.Builtins.IListOps.Delete), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object, System.Object>(IronRuby.Builtins.IListOps.Delete)
            );
            
            module.DefineLibraryMethod("delete_at", 0x51, 
                new Func<System.Collections.IList, System.Int32, System.Object>(IronRuby.Builtins.IListOps.DeleteAt)
            );
            
            module.DefineLibraryMethod("delete_if", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.DeleteIf)
            );
            
            module.DefineLibraryMethod("each", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.Each)
            );
            
            module.DefineLibraryMethod("each_index", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.EachIndex)
            );
            
            module.DefineLibraryMethod("empty?", 0x51, 
                new Func<System.Collections.IList, System.Boolean>(IronRuby.Builtins.IListOps.Empty)
            );
            
            module.DefineLibraryMethod("eql?", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Collections.IList, System.Object, System.Boolean>(IronRuby.Builtins.IListOps.HashEquals)
            );
            
            module.DefineLibraryMethod("fetch", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object, System.Object, System.Object>(IronRuby.Builtins.IListOps.Fetch)
            );
            
            module.DefineLibraryMethod("fill", 0x51, 
                new Func<System.Collections.IList, System.Object, System.Int32, System.Collections.IList>(IronRuby.Builtins.IListOps.Fill), 
                new Func<System.Collections.IList, System.Object, System.Int32, System.Int32, System.Collections.IList>(IronRuby.Builtins.IListOps.Fill), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, System.Collections.IList, System.Object, System.Object, System.Object, System.Collections.IList>(IronRuby.Builtins.IListOps.Fill), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, System.Collections.IList, System.Object, IronRuby.Builtins.Range, System.Collections.IList>(IronRuby.Builtins.IListOps.Fill), 
                new Func<IronRuby.Runtime.BlockParam, System.Collections.IList, System.Int32, System.Object>(IronRuby.Builtins.IListOps.Fill), 
                new Func<IronRuby.Runtime.BlockParam, System.Collections.IList, System.Int32, System.Int32, System.Object>(IronRuby.Builtins.IListOps.Fill), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object, System.Object, System.Object>(IronRuby.Builtins.IListOps.Fill), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.BlockParam, System.Collections.IList, IronRuby.Builtins.Range, System.Object>(IronRuby.Builtins.IListOps.Fill)
            );
            
            module.DefineLibraryMethod("first", 0x51, 
                new Func<System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.First), 
                new Func<System.Collections.IList, System.Int32, System.Collections.IList>(IronRuby.Builtins.IListOps.First)
            );
            
            module.DefineLibraryMethod("flatten", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.ConversionStorage<System.Collections.IList>, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.Flatten)
            );
            
            module.DefineLibraryMethod("flatten!", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Collections.IList>, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.FlattenInPlace)
            );
            
            module.DefineLibraryMethod("hash", 0x51, 
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.ConversionStorage<System.Int32>, System.Collections.IList, System.Int32>(IronRuby.Builtins.IListOps.GetHashCode)
            );
            
            module.DefineLibraryMethod("include?", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Collections.IList, System.Object, System.Boolean>(IronRuby.Builtins.IListOps.Include)
            );
            
            module.DefineLibraryMethod("index", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Collections.IList, System.Object, System.Object>(IronRuby.Builtins.IListOps.Index)
            );
            
            module.DefineLibraryMethod("indexes", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, System.Collections.IList, System.Object[], System.Object>(IronRuby.Builtins.IListOps.Indexes)
            );
            
            module.DefineLibraryMethod("indices", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, System.Collections.IList, System.Object[], System.Object>(IronRuby.Builtins.IListOps.Indexes)
            );
            
            module.DefineLibraryMethod("initialize_copy", 0x52, 
                new Func<System.Collections.IList, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.Replace)
            );
            
            module.DefineLibraryMethod("insert", 0x51, 
                new Func<System.Collections.IList, System.Int32, System.Object[], System.Collections.IList>(IronRuby.Builtins.IListOps.Insert)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Collections.IList, IronRuby.Builtins.MutableString>(IronRuby.Builtins.IListOps.Inspect)
            );
            
            module.DefineLibraryMethod("join", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, System.Collections.IList, IronRuby.Builtins.MutableString>(IronRuby.Builtins.IListOps.Join), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, System.Collections.IList, System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.IListOps.Join)
            );
            
            module.DefineLibraryMethod("last", 0x51, 
                new Func<System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.Last), 
                new Func<System.Collections.IList, System.Int32, System.Collections.IList>(IronRuby.Builtins.IListOps.Last)
            );
            
            module.DefineLibraryMethod("length", 0x51, 
                new Func<System.Collections.IList, System.Int32>(IronRuby.Builtins.IListOps.Length)
            );
            
            module.DefineLibraryMethod("map!", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.CollectInPlace)
            );
            
            module.DefineLibraryMethod("nitems", 0x51, 
                new Func<System.Collections.IList, System.Int32>(IronRuby.Builtins.IListOps.NumberOfNonNilItems)
            );
            
            module.DefineLibraryMethod("pop", 0x51, 
                new Func<System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.Pop)
            );
            
            module.DefineLibraryMethod("push", 0x51, 
                new Func<System.Collections.IList, System.Object[], System.Collections.IList>(IronRuby.Builtins.IListOps.Push)
            );
            
            module.DefineLibraryMethod("rassoc", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Collections.IList, System.Object, System.Collections.IList>(IronRuby.Builtins.IListOps.GetContainerOfSecondItem)
            );
            
            module.DefineLibraryMethod("reject!", 0x51, 
                new Func<IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.RejectInPlace)
            );
            
            module.DefineLibraryMethod("replace", 0x51, 
                new Func<System.Collections.IList, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.Replace)
            );
            
            module.DefineLibraryMethod("reverse", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.Reverse)
            );
            
            module.DefineLibraryMethod("reverse!", 0x51, 
                new Func<System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.InPlaceReverse)
            );
            
            module.DefineLibraryMethod("rindex", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Collections.IList, System.Object, System.Object>(IronRuby.Builtins.IListOps.ReverseIndex)
            );
            
            module.DefineLibraryMethod("shift", 0x51, 
                new Func<System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.Shift)
            );
            
            module.DefineLibraryMethod("size", 0x51, 
                new Func<System.Collections.IList, System.Int32>(IronRuby.Builtins.IListOps.Length)
            );
            
            module.DefineLibraryMethod("slice", 0x51, 
                new Func<System.Collections.IList, System.Int32, System.Object>(IronRuby.Builtins.IListOps.GetElement), 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, System.Collections.IList, System.Int32, System.Int32, System.Collections.IList>(IronRuby.Builtins.IListOps.GetElements), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, System.Collections.IList, IronRuby.Builtins.Range, System.Collections.IList>(IronRuby.Builtins.IListOps.GetElement)
            );
            
            module.DefineLibraryMethod("slice!", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Collections.IList>, System.Collections.IList, System.Int32, System.Object>(IronRuby.Builtins.IListOps.SliceInPlace), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Collections.IList>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, System.Collections.IList, IronRuby.Builtins.Range, System.Object>(IronRuby.Builtins.IListOps.SliceInPlace), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Collections.IList>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, System.Collections.IList, System.Int32, System.Int32, System.Collections.IList>(IronRuby.Builtins.IListOps.SliceInPlace)
            );
            
            module.DefineLibraryMethod("sort", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.Sort)
            );
            
            module.DefineLibraryMethod("sort!", 0x51, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BlockParam, System.Collections.IList, System.Object>(IronRuby.Builtins.IListOps.SortInPlace)
            );
            
            module.DefineLibraryMethod("to_a", 0x51, 
                new Func<System.Collections.IList, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IListOps.ToArray)
            );
            
            module.DefineLibraryMethod("to_ary", 0x51, 
                new Func<System.Collections.IList, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IListOps.ToArray)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, System.Collections.IList, IronRuby.Builtins.MutableString>(IronRuby.Builtins.IListOps.Join)
            );
            
            module.DefineLibraryMethod("transpose", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Collections.IList>, System.Collections.IList, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IListOps.Transpose)
            );
            
            module.DefineLibraryMethod("uniq", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.Unique)
            );
            
            module.DefineLibraryMethod("uniq!", 0x51, 
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BinaryOpStorage, System.Collections.IList, System.Collections.IList>(IronRuby.Builtins.IListOps.UniqueSelf)
            );
            
            module.DefineLibraryMethod("unshift", 0x51, 
                new Func<System.Collections.IList, System.Object, System.Collections.IList>(IronRuby.Builtins.IListOps.Unshift), 
                new Func<System.Collections.IList, System.Object[], System.Collections.IList>(IronRuby.Builtins.IListOps.Unshift)
            );
            
            module.DefineLibraryMethod("values_at", 0x51, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, System.Collections.IList, System.Object[], IronRuby.Builtins.RubyArray>(IronRuby.Builtins.IListOps.ValuesAt)
            );
            
            module.DefineLibraryMethod("zip", 0x51, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.ConversionStorage<System.Collections.IList>, IronRuby.Runtime.BlockParam, System.Object, System.Collections.IList[], System.Object>(IronRuby.Builtins.IListOps.Zip)
            );
            
        }
        
        private static void LoadSystem__Decimal_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("==", 0x51, 
                new Func<System.Decimal, System.Double, System.Boolean>(IronRuby.Builtins.DecimalOps.Equal), 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Decimal, System.Object, System.Boolean>(IronRuby.Builtins.DecimalOps.Equal)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.DecimalOps.Inspect)
            );
            
            module.DefineLibraryMethod("size", 0x51, 
                new Func<System.Object, System.Int32>(IronRuby.Builtins.DecimalOps.Size)
            );
            
            module.DefineLibraryMethod("to_f", 0x51, 
                new Func<System.Decimal, System.Double>(IronRuby.Builtins.DecimalOps.ToDouble)
            );
            
            module.DefineLibraryMethod("to_i", 0x51, 
                new Func<System.Decimal, System.Object>(IronRuby.Builtins.DecimalOps.ToInt)
            );
            
            module.DefineLibraryMethod("to_int", 0x51, 
                new Func<System.Decimal, System.Object>(IronRuby.Builtins.DecimalOps.ToInt)
            );
            
        }
        
        private static void LoadSystem__Decimal_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("induced_from", 0x61, 
                new Func<IronRuby.Builtins.RubyModule, System.Double, System.Decimal>(IronRuby.Builtins.DecimalOps.InducedFrom), 
                new Func<IronRuby.Builtins.RubyModule, System.Decimal, System.Decimal>(IronRuby.Builtins.DecimalOps.InducedFrom), 
                new Func<IronRuby.Builtins.RubyModule, System.Int32, System.Decimal>(IronRuby.Builtins.DecimalOps.InducedFrom), 
                new Func<IronRuby.Builtins.RubyModule, Microsoft.Scripting.Math.BigInteger, System.Decimal>(IronRuby.Builtins.DecimalOps.InducedFrom), 
                new Func<IronRuby.Builtins.RubyModule, System.Object, System.Double>(IronRuby.Builtins.DecimalOps.InducedFrom)
            );
            
        }
        
        private static void LoadSystem__IComparable_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("<=>", 0x51, 
                new Func<System.IComparable, System.Object, System.Int32>(IronRuby.Builtins.IComparableOps.Compare)
            );
            
        }
        
        private static void LoadSystem__Int16_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            LoadIronRuby__Clr__Integer_Instance(module);
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.Int16Ops.Inspect)
            );
            
            module.DefineLibraryMethod("size", 0x51, 
                new Func<System.Int16, System.Int32>(IronRuby.Builtins.Int16Ops.Size)
            );
            
        }
        
        private static void LoadSystem__Int16_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("induced_from", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Int16>(IronRuby.Builtins.Int16Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, System.Int16>(IronRuby.Builtins.Int16Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, System.Double, System.Int16>(IronRuby.Builtins.Int16Ops.InducedFrom)
            );
            
        }
        
        private static void LoadSystem__Int64_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            LoadIronRuby__Clr__BigInteger_Instance(module);
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.Int64Ops.Inspect)
            );
            
            module.DefineLibraryMethod("size", 0x51, 
                new Func<System.Int64, System.Int32>(IronRuby.Builtins.Int64Ops.Size)
            );
            
        }
        
        private static void LoadSystem__Int64_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("induced_from", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Int64>(IronRuby.Builtins.Int64Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, System.Int64>(IronRuby.Builtins.Int64Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, System.Double, System.Int64>(IronRuby.Builtins.Int64Ops.InducedFrom)
            );
            
        }
        
        private static void LoadSystem__SByte_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            LoadIronRuby__Clr__Integer_Instance(module);
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.SByteOps.Inspect)
            );
            
            module.DefineLibraryMethod("size", 0x51, 
                new Func<System.SByte, System.Int32>(IronRuby.Builtins.SByteOps.Size)
            );
            
        }
        
        private static void LoadSystem__SByte_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("induced_from", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.SByte>(IronRuby.Builtins.SByteOps.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, System.SByte>(IronRuby.Builtins.SByteOps.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, System.Double, System.SByte>(IronRuby.Builtins.SByteOps.InducedFrom)
            );
            
        }
        
        private static void LoadSystem__Single_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            LoadIronRuby__Clr__Float_Instance(module);
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.SingleOps.Inspect)
            );
            
        }
        
        private static void LoadSystem__Single_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            LoadIronRuby__Clr__Float_Class(module);
        }
        
        private static void LoadSystem__String_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.HideMethod("[]");
            module.HideMethod("==");
            module.HideMethod("clone");
            module.HideMethod("insert");
            module.HideMethod("split");
        }
        
        private static void LoadSystem__Type_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("to_class", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Type, IronRuby.Builtins.RubyClass>(IronRuby.Builtins.TypeOps.ToClass)
            );
            
            module.DefineLibraryMethod("to_module", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Type, IronRuby.Builtins.RubyModule>(IronRuby.Builtins.TypeOps.ToModule)
            );
            
        }
        
        private static void LoadSystem__UInt16_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            LoadIronRuby__Clr__Integer_Instance(module);
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.UInt16Ops.Inspect)
            );
            
            module.DefineLibraryMethod("size", 0x51, 
                new Func<System.UInt16, System.Int32>(IronRuby.Builtins.UInt16Ops.Size)
            );
            
        }
        
        private static void LoadSystem__UInt16_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("induced_from", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.UInt16>(IronRuby.Builtins.UInt16Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, System.UInt16>(IronRuby.Builtins.UInt16Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, System.Double, System.UInt16>(IronRuby.Builtins.UInt16Ops.InducedFrom)
            );
            
        }
        
        private static void LoadSystem__UInt32_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            LoadIronRuby__Clr__BigInteger_Instance(module);
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.UInt32Ops.Inspect)
            );
            
            module.DefineLibraryMethod("size", 0x51, 
                new Func<System.UInt32, System.Int32>(IronRuby.Builtins.UInt32Ops.Size)
            );
            
        }
        
        private static void LoadSystem__UInt32_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("induced_from", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.UInt32>(IronRuby.Builtins.UInt32Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, System.UInt32>(IronRuby.Builtins.UInt32Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, System.Double, System.UInt32>(IronRuby.Builtins.UInt32Ops.InducedFrom)
            );
            
        }
        
        private static void LoadSystem__UInt64_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            LoadIronRuby__Clr__BigInteger_Instance(module);
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<System.Object, IronRuby.Builtins.MutableString>(IronRuby.Builtins.UInt64Ops.Inspect)
            );
            
            module.DefineLibraryMethod("size", 0x51, 
                new Func<System.UInt64, System.Int32>(IronRuby.Builtins.UInt64Ops.Size)
            );
            
        }
        
        private static void LoadSystem__UInt64_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("induced_from", 0x61, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.UInt64>(IronRuby.Builtins.UInt64Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, System.UInt64>(IronRuby.Builtins.UInt64Ops.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, System.Double, System.UInt64>(IronRuby.Builtins.UInt64Ops.InducedFrom)
            );
            
        }
        
        private static void LoadSystemExit_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("status", 0x51, 
                new Func<IronRuby.Builtins.SystemExit, System.Int32>(IronRuby.Builtins.SystemExitOps.GetStatus)
            );
            
            module.DefineLibraryMethod("success?", 0x51, 
                new Func<IronRuby.Builtins.SystemExit, System.Boolean>(IronRuby.Builtins.SystemExitOps.IsSuccessful)
            );
            
        }
        
        private static void LoadThread_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("[]", 0x51, 
                new Func<System.Threading.Thread, Microsoft.Scripting.SymbolId, System.Object>(IronRuby.Builtins.ThreadOps.GetElement), 
                new Func<System.Threading.Thread, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.ThreadOps.GetElement), 
                new Func<IronRuby.Runtime.RubyContext, System.Threading.Thread, System.Object, System.Object>(IronRuby.Builtins.ThreadOps.GetElement)
            );
            
            module.DefineLibraryMethod("[]=", 0x51, 
                new Func<System.Threading.Thread, Microsoft.Scripting.SymbolId, System.Object, System.Object>(IronRuby.Builtins.ThreadOps.SetElement), 
                new Func<System.Threading.Thread, IronRuby.Builtins.MutableString, System.Object, System.Object>(IronRuby.Builtins.ThreadOps.SetElement), 
                new Func<IronRuby.Runtime.RubyContext, System.Threading.Thread, System.Object, System.Object, System.Object>(IronRuby.Builtins.ThreadOps.SetElement)
            );
            
            module.DefineLibraryMethod("abort_on_exception", 0x51, 
                new Func<System.Threading.Thread, System.Object>(IronRuby.Builtins.ThreadOps.AbortOnException)
            );
            
            module.DefineLibraryMethod("abort_on_exception=", 0x51, 
                new Func<System.Threading.Thread, System.Boolean, System.Object>(IronRuby.Builtins.ThreadOps.AbortOnException)
            );
            
            module.DefineLibraryMethod("alive?", 0x51, 
                new Func<System.Threading.Thread, System.Boolean>(IronRuby.Builtins.ThreadOps.IsAlive)
            );
            
            module.DefineLibraryMethod("exit", 0x51, 
                new Func<System.Threading.Thread, System.Threading.Thread>(IronRuby.Builtins.ThreadOps.Kill)
            );
            
            module.DefineLibraryMethod("group", 0x51, 
                new Func<System.Threading.Thread, IronRuby.Builtins.ThreadGroup>(IronRuby.Builtins.ThreadOps.Group)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Threading.Thread, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ThreadOps.Inspect)
            );
            
            module.DefineLibraryMethod("join", 0x51, 
                new Func<System.Threading.Thread, System.Threading.Thread>(IronRuby.Builtins.ThreadOps.Join), 
                new Func<System.Threading.Thread, System.Double, System.Threading.Thread>(IronRuby.Builtins.ThreadOps.Join)
            );
            
            module.DefineLibraryMethod("key?", 0x51, 
                new Func<System.Threading.Thread, Microsoft.Scripting.SymbolId, System.Object>(IronRuby.Builtins.ThreadOps.HasKey), 
                new Func<System.Threading.Thread, IronRuby.Builtins.MutableString, System.Object>(IronRuby.Builtins.ThreadOps.HasKey), 
                new Func<IronRuby.Runtime.RubyContext, System.Threading.Thread, System.Object, System.Object>(IronRuby.Builtins.ThreadOps.HasKey)
            );
            
            module.DefineLibraryMethod("keys", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.Threading.Thread, System.Object>(IronRuby.Builtins.ThreadOps.Keys)
            );
            
            module.DefineLibraryMethod("kill", 0x51, 
                new Func<System.Threading.Thread, System.Threading.Thread>(IronRuby.Builtins.ThreadOps.Kill)
            );
            
            module.DefineLibraryMethod("raise", 0x51, 
                new Action<IronRuby.Runtime.RubyContext, System.Threading.Thread>(IronRuby.Builtins.ThreadOps.RaiseException), 
                new Action<System.Threading.Thread, IronRuby.Builtins.MutableString>(IronRuby.Builtins.ThreadOps.RaiseException), 
                new Action<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.CallSiteStorage<Action<System.Runtime.CompilerServices.CallSite, System.Exception, IronRuby.Builtins.RubyArray>>, System.Threading.Thread, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ThreadOps.RaiseException)
            );
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("run", 0x51, 
                new Func<System.Threading.Thread, System.Threading.Thread>(IronRuby.Builtins.ThreadOps.Run)
            );
            
            #endif
            module.DefineLibraryMethod("status", 0x51, 
                new Func<System.Threading.Thread, System.Object>(IronRuby.Builtins.ThreadOps.Status)
            );
            
            module.DefineLibraryMethod("stop?", 0x51, 
                new Func<System.Threading.Thread, System.Boolean>(IronRuby.Builtins.ThreadOps.IsStopped)
            );
            
            module.DefineLibraryMethod("terminate", 0x51, 
                new Func<System.Threading.Thread, System.Threading.Thread>(IronRuby.Builtins.ThreadOps.Kill)
            );
            
            module.DefineLibraryMethod("value", 0x51, 
                new Func<System.Threading.Thread, System.Object>(IronRuby.Builtins.ThreadOps.Value)
            );
            
            #if !SILVERLIGHT
            module.DefineLibraryMethod("wakeup", 0x51, 
                new Func<System.Threading.Thread, System.Threading.Thread>(IronRuby.Builtins.ThreadOps.Run)
            );
            
            #endif
        }
        
        private static void LoadThread_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("abort_on_exception", 0x61, 
                new Func<System.Object, System.Object>(IronRuby.Builtins.ThreadOps.GlobalAbortOnException)
            );
            
            module.DefineLibraryMethod("abort_on_exception=", 0x61, 
                new Func<System.Object, System.Boolean, System.Object>(IronRuby.Builtins.ThreadOps.GlobalAbortOnException)
            );
            
            module.DefineLibraryMethod("critical", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, System.Boolean>(IronRuby.Builtins.ThreadOps.Critical)
            );
            
            module.DefineLibraryMethod("critical=", 0x61, 
                new Action<IronRuby.Runtime.RubyContext, System.Object, System.Boolean>(IronRuby.Builtins.ThreadOps.Critical)
            );
            
            module.DefineLibraryMethod("current", 0x61, 
                new Func<System.Object, System.Threading.Thread>(IronRuby.Builtins.ThreadOps.Current)
            );
            
            module.DefineLibraryMethod("list", 0x61, 
                new Func<System.Object, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ThreadOps.List)
            );
            
            module.DefineLibraryMethod("main", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Threading.Thread>(IronRuby.Builtins.ThreadOps.GetMainThread)
            );
            
            module.DefineLibraryMethod("new", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object[], System.Threading.Thread>(IronRuby.Builtins.ThreadOps.CreateThread)
            );
            
            module.DefineLibraryMethod("pass", 0x61, 
                new Action<System.Object>(IronRuby.Builtins.ThreadOps.Yield)
            );
            
            module.DefineLibraryMethod("start", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, System.Object, System.Object[], System.Threading.Thread>(IronRuby.Builtins.ThreadOps.CreateThread)
            );
            
            module.DefineLibraryMethod("stop", 0x61, 
                new Action<IronRuby.Runtime.RubyContext, System.Object>(IronRuby.Builtins.ThreadOps.Stop)
            );
            
        }
        
        private static void LoadThreadGroup_Constants(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetBuiltinConstant("Default", IronRuby.Builtins.ThreadGroup.Default);
            
        }
        
        private static void LoadThreadGroup_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("add", 0x51, 
                new Func<IronRuby.Builtins.ThreadGroup, System.Threading.Thread, IronRuby.Builtins.ThreadGroup>(IronRuby.Builtins.ThreadGroup.Add)
            );
            
            module.DefineLibraryMethod("list", 0x51, 
                new Func<IronRuby.Builtins.ThreadGroup, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.ThreadGroup.List)
            );
            
        }
        
        private static void LoadTime_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("-", 0x51, 
                new Func<System.DateTime, System.Double, System.DateTime>(IronRuby.Builtins.TimeOps.SubtractSeconds), 
                new Func<System.DateTime, System.DateTime, System.Double>(IronRuby.Builtins.TimeOps.SubtractTime)
            );
            
            module.DefineLibraryMethod("_dump", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, System.DateTime, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TimeOps.Dump)
            );
            
            module.DefineLibraryMethod("+", 0x51, 
                new Func<System.DateTime, System.Double, System.DateTime>(IronRuby.Builtins.TimeOps.AddSeconds), 
                new Func<System.DateTime, System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.AddSeconds)
            );
            
            module.DefineLibraryMethod("<=>", 0x51, 
                new Func<System.DateTime, System.Object, System.Object>(IronRuby.Builtins.TimeOps.CompareSeconds), 
                new Func<System.DateTime, System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.CompareTo)
            );
            
            module.DefineLibraryMethod("==", 0x51, 
                new Func<System.DateTime, System.DateTime, System.Boolean>(IronRuby.Builtins.TimeOps.Eql), 
                new Func<IronRuby.Runtime.RubyContext, System.DateTime, System.Object, System.Object>(IronRuby.Builtins.TimeOps.Equals)
            );
            
            module.DefineLibraryMethod("asctime", 0x51, 
                new Func<System.DateTime, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TimeOps.ToString)
            );
            
            module.DefineLibraryMethod("ctime", 0x51, 
                new Func<System.DateTime, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TimeOps.ToString)
            );
            
            module.DefineLibraryMethod("day", 0x51, 
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.Day)
            );
            
            module.DefineLibraryMethod("dst?", 0x51, 
                new Func<System.DateTime, System.Boolean>(IronRuby.Builtins.TimeOps.IsDST)
            );
            
            module.DefineLibraryMethod("dup", 0x51, 
                new Func<System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.Clone)
            );
            
            module.DefineLibraryMethod("eql?", 0x51, 
                new Func<System.DateTime, System.DateTime, System.Boolean>(IronRuby.Builtins.TimeOps.Eql), 
                new Func<System.DateTime, System.Object, System.Boolean>(IronRuby.Builtins.TimeOps.Eql)
            );
            
            module.DefineLibraryMethod("getgm", 0x51, 
                new Func<System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.GetUTC)
            );
            
            module.DefineLibraryMethod("getlocal", 0x51, 
                new Func<System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.GetLocal)
            );
            
            module.DefineLibraryMethod("getutc", 0x51, 
                new Func<System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.GetUTC)
            );
            
            module.DefineLibraryMethod("gmt?", 0x51, 
                new Func<System.DateTime, System.Boolean>(IronRuby.Builtins.TimeOps.IsUTC)
            );
            
            module.DefineLibraryMethod("gmt_offset", 0x51, 
                new Func<System.DateTime, System.Object>(IronRuby.Builtins.TimeOps.Offset)
            );
            
            module.DefineLibraryMethod("gmtime", 0x51, 
                new Func<System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.ToUTC)
            );
            
            module.DefineLibraryMethod("gmtoff", 0x51, 
                new Func<System.DateTime, System.Object>(IronRuby.Builtins.TimeOps.Offset)
            );
            
            module.DefineLibraryMethod("hash", 0x51, 
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.GetHash)
            );
            
            module.DefineLibraryMethod("hour", 0x51, 
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.Hour)
            );
            
            module.DefineLibraryMethod("inspect", 0x51, 
                new Func<System.DateTime, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TimeOps.ToString)
            );
            
            module.DefineLibraryMethod("isdst", 0x51, 
                new Func<System.DateTime, System.Boolean>(IronRuby.Builtins.TimeOps.IsDST)
            );
            
            module.DefineLibraryMethod("localtime", 0x51, 
                new Func<System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.ToLocalTime)
            );
            
            module.DefineLibraryMethod("mday", 0x51, 
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.Day)
            );
            
            module.DefineLibraryMethod("min", 0x51, 
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.Minute)
            );
            
            module.DefineLibraryMethod("mon", 0x51, 
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.Month)
            );
            
            module.DefineLibraryMethod("month", 0x51, 
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.Month)
            );
            
            module.DefineLibraryMethod("sec", 0x51, 
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.Second)
            );
            
            module.DefineLibraryMethod("strftime", 0x51, 
                new Func<System.DateTime, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TimeOps.FormatTime)
            );
            
            module.DefineLibraryMethod("succ", 0x51, 
                new Func<System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.SuccessiveSecond)
            );
            
            module.DefineLibraryMethod("to_a", 0x51, 
                new Func<System.DateTime, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.TimeOps.ToArray)
            );
            
            module.DefineLibraryMethod("to_f", 0x51, 
                new Func<System.DateTime, System.Double>(IronRuby.Builtins.TimeOps.ToFloatSeconds)
            );
            
            module.DefineLibraryMethod("to_i", 0x51, 
                new Func<System.DateTime, System.Object>(IronRuby.Builtins.TimeOps.ToSeconds)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<System.DateTime, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TimeOps.ToString)
            );
            
            module.DefineLibraryMethod("tv_sec", 0x51, 
                new Func<System.DateTime, System.Object>(IronRuby.Builtins.TimeOps.ToSeconds)
            );
            
            module.DefineLibraryMethod("tv_usec", 0x51, 
                new Func<System.DateTime, System.Object>(IronRuby.Builtins.TimeOps.GetMicroSeconds)
            );
            
            module.DefineLibraryMethod("usec", 0x51, 
                new Func<System.DateTime, System.Object>(IronRuby.Builtins.TimeOps.GetMicroSeconds)
            );
            
            module.DefineLibraryMethod("utc", 0x51, 
                new Func<System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.ToUTC)
            );
            
            module.DefineLibraryMethod("utc?", 0x51, 
                new Func<System.DateTime, System.Boolean>(IronRuby.Builtins.TimeOps.IsUTC)
            );
            
            module.DefineLibraryMethod("utc_offset", 0x51, 
                new Func<System.DateTime, System.Object>(IronRuby.Builtins.TimeOps.Offset)
            );
            
            module.DefineLibraryMethod("wday", 0x51, 
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.DayOfWeek)
            );
            
            module.DefineLibraryMethod("yday", 0x51, 
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.DayOfYear)
            );
            
            module.DefineLibraryMethod("year", 0x51, 
                new Func<System.DateTime, System.Int32>(IronRuby.Builtins.TimeOps.Year)
            );
            
            module.DefineLibraryMethod("zone", 0x51, 
                new Func<System.DateTime, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TimeOps.GetZone)
            );
            
        }
        
        private static void LoadTime_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("_load", 0x61, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.DateTime>(IronRuby.Builtins.TimeOps.Load)
            );
            
            module.DefineLibraryMethod("at", 0x61, 
                new Func<System.Object, System.DateTime, System.DateTime>(IronRuby.Builtins.TimeOps.Create), 
                new Func<System.Object, System.Double, System.DateTime>(IronRuby.Builtins.TimeOps.Create), 
                new Func<System.Object, System.Int64, System.Int64, System.DateTime>(IronRuby.Builtins.TimeOps.Create)
            );
            
            module.DefineLibraryMethod("gm", 0x61, 
                new Func<System.Object, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime), 
                new Func<System.Object, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, System.Object, System.Object[], System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime)
            );
            
            module.DefineLibraryMethod("local", 0x61, 
                new Func<System.Object, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime), 
                new Func<System.Object, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, System.Object, System.Object[], System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime)
            );
            
            module.DefineLibraryMethod("mktime", 0x61, 
                new Func<System.Object, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime), 
                new Func<System.Object, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, System.Object, System.Object[], System.DateTime>(IronRuby.Builtins.TimeOps.CreateLocalTime)
            );
            
            module.DefineLibraryMethod("now", 0x61, 
                new Func<System.Object, System.DateTime>(IronRuby.Builtins.TimeOps.CreateTime)
            );
            
            module.DefineLibraryMethod("today", 0x61, 
                new Func<System.Object, System.DateTime>(IronRuby.Builtins.TimeOps.Today)
            );
            
            module.DefineLibraryMethod("utc", 0x61, 
                new Func<System.Object, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime), 
                new Func<System.Object, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime), 
                new Func<System.Object, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.Int32, System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, System.Object, System.Object[], System.DateTime>(IronRuby.Builtins.TimeOps.CreateGmtTime)
            );
            
        }
        
        private static void LoadTrueClass_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("&", 0x51, 
                new Func<System.Boolean, System.Object, System.Boolean>(IronRuby.Builtins.TrueClass.And), 
                new Func<System.Boolean, System.Boolean, System.Boolean>(IronRuby.Builtins.TrueClass.And)
            );
            
            module.DefineLibraryMethod("^", 0x51, 
                new Func<System.Boolean, System.Object, System.Boolean>(IronRuby.Builtins.TrueClass.Xor), 
                new Func<System.Boolean, System.Boolean, System.Boolean>(IronRuby.Builtins.TrueClass.Xor)
            );
            
            module.DefineLibraryMethod("|", 0x51, 
                new Func<System.Boolean, System.Object, System.Boolean>(IronRuby.Builtins.TrueClass.Or)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<System.Boolean, IronRuby.Builtins.MutableString>(IronRuby.Builtins.TrueClass.ToString)
            );
            
        }
        
        private static void LoadUnboundMethod_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("==", 0x51, 
                new Func<IronRuby.Builtins.UnboundMethod, IronRuby.Builtins.UnboundMethod, System.Boolean>(IronRuby.Builtins.UnboundMethod.Equal), 
                new Func<IronRuby.Builtins.UnboundMethod, System.Object, System.Boolean>(IronRuby.Builtins.UnboundMethod.Equal)
            );
            
            module.DefineLibraryMethod("arity", 0x51, 
                new Func<IronRuby.Builtins.UnboundMethod, System.Int32>(IronRuby.Builtins.UnboundMethod.GetArity)
            );
            
            module.DefineLibraryMethod("bind", 0x51, 
                new Func<IronRuby.Builtins.UnboundMethod, System.Object, IronRuby.Builtins.RubyMethod>(IronRuby.Builtins.UnboundMethod.Bind)
            );
            
            module.DefineLibraryMethod("clone", 0x51, 
                new Func<IronRuby.Builtins.UnboundMethod, IronRuby.Builtins.UnboundMethod>(IronRuby.Builtins.UnboundMethod.Clone)
            );
            
            module.DefineLibraryMethod("clr_members", 0x51, 
                new Func<IronRuby.Builtins.UnboundMethod, IronRuby.Builtins.RubyArray>(IronRuby.Builtins.UnboundMethod.GetClrMembers)
            );
            
            module.DefineLibraryMethod("of", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.UnboundMethod, System.Object[], IronRuby.Builtins.UnboundMethod>(IronRuby.Builtins.UnboundMethod.BingGenericParameters)
            );
            
            module.DefineLibraryMethod("overload", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.UnboundMethod, System.Object[], IronRuby.Builtins.UnboundMethod>(IronRuby.Builtins.UnboundMethod.SelectOverload)
            );
            
            module.DefineLibraryMethod("overloads", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyMethod, System.Object[], IronRuby.Builtins.RubyMethod>(IronRuby.Builtins.UnboundMethod.SelectOverload_old)
            );
            
            module.DefineLibraryMethod("to_s", 0x51, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.UnboundMethod, IronRuby.Builtins.MutableString>(IronRuby.Builtins.UnboundMethod.ToS)
            );
            
        }
        
        public static System.Exception/*!*/ ExceptionFactory__EncodingError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.Builtins.EncodingError(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
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
    using System;
    using Microsoft.Scripting.Utils;
    
    public sealed class ThreadingLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(System.Object));
            
            
            DefineGlobalClass("ConditionVariable", typeof(IronRuby.StandardLibrary.Threading.RubyConditionVariable), 0x00000100, classRef0, LoadConditionVariable_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalClass("Mutex", typeof(IronRuby.StandardLibrary.Threading.RubyMutex), 0x00000100, classRef0, LoadMutex_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def1 = DefineGlobalClass("Queue", typeof(IronRuby.StandardLibrary.Threading.RubyQueue), 0x00000100, classRef0, LoadQueue_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            DefineGlobalClass("SizedQueue", typeof(IronRuby.StandardLibrary.Threading.SizedQueue), 0x00000100, def1, LoadSizedQueue_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
        }
        
        private static void LoadConditionVariable_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("broadcast", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.RubyConditionVariable, IronRuby.StandardLibrary.Threading.RubyConditionVariable>(IronRuby.StandardLibrary.Threading.RubyConditionVariable.Broadcast)
            );
            
            module.DefineLibraryMethod("signal", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.RubyConditionVariable, IronRuby.StandardLibrary.Threading.RubyConditionVariable>(IronRuby.StandardLibrary.Threading.RubyConditionVariable.Signal)
            );
            
            module.DefineLibraryMethod("wait", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.RubyConditionVariable, IronRuby.StandardLibrary.Threading.RubyMutex, IronRuby.StandardLibrary.Threading.RubyConditionVariable>(IronRuby.StandardLibrary.Threading.RubyConditionVariable.Wait)
            );
            
        }
        
        private static void LoadMutex_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("exclusive_unlock", 0x11, 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.StandardLibrary.Threading.RubyMutex, System.Boolean>(IronRuby.StandardLibrary.Threading.RubyMutex.ExclusiveUnlock)
            );
            
            module.DefineLibraryMethod("lock", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.RubyMutex, IronRuby.StandardLibrary.Threading.RubyMutex>(IronRuby.StandardLibrary.Threading.RubyMutex.Lock)
            );
            
            module.DefineLibraryMethod("locked?", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.RubyMutex, System.Boolean>(IronRuby.StandardLibrary.Threading.RubyMutex.IsLocked)
            );
            
            module.DefineLibraryMethod("synchronize", 0x11, 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.StandardLibrary.Threading.RubyMutex, System.Object>(IronRuby.StandardLibrary.Threading.RubyMutex.Synchronize)
            );
            
            module.DefineLibraryMethod("try_lock", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.RubyMutex, System.Boolean>(IronRuby.StandardLibrary.Threading.RubyMutex.TryLock)
            );
            
            module.DefineLibraryMethod("unlock", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.RubyMutex, IronRuby.StandardLibrary.Threading.RubyMutex>(IronRuby.StandardLibrary.Threading.RubyMutex.Unlock)
            );
            
        }
        
        private static void LoadQueue_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("<<", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Object, IronRuby.StandardLibrary.Threading.RubyQueue>(IronRuby.StandardLibrary.Threading.RubyQueue.Enqueue)
            );
            
            module.DefineLibraryMethod("clear", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, IronRuby.StandardLibrary.Threading.RubyQueue>(IronRuby.StandardLibrary.Threading.RubyQueue.Clear)
            );
            
            module.DefineLibraryMethod("deq", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Boolean, System.Object>(IronRuby.StandardLibrary.Threading.RubyQueue.Dequeue)
            );
            
            module.DefineLibraryMethod("empty?", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Boolean>(IronRuby.StandardLibrary.Threading.RubyQueue.IsEmpty)
            );
            
            module.DefineLibraryMethod("enq", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Object, IronRuby.StandardLibrary.Threading.RubyQueue>(IronRuby.StandardLibrary.Threading.RubyQueue.Enqueue)
            );
            
            module.DefineLibraryMethod("length", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Int32>(IronRuby.StandardLibrary.Threading.RubyQueue.GetCount)
            );
            
            module.DefineLibraryMethod("num_waiting", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Int32>(IronRuby.StandardLibrary.Threading.RubyQueue.GetNumberOfWaitingThreads)
            );
            
            module.DefineLibraryMethod("pop", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Boolean, System.Object>(IronRuby.StandardLibrary.Threading.RubyQueue.Dequeue)
            );
            
            module.DefineLibraryMethod("push", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Object, IronRuby.StandardLibrary.Threading.RubyQueue>(IronRuby.StandardLibrary.Threading.RubyQueue.Enqueue)
            );
            
            module.DefineLibraryMethod("shift", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Boolean, System.Object>(IronRuby.StandardLibrary.Threading.RubyQueue.Dequeue)
            );
            
            module.DefineLibraryMethod("size", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.RubyQueue, System.Int32>(IronRuby.StandardLibrary.Threading.RubyQueue.GetCount)
            );
            
        }
        
        private static void LoadSizedQueue_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("<<", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.SizedQueue, System.Object, IronRuby.StandardLibrary.Threading.SizedQueue>(IronRuby.StandardLibrary.Threading.SizedQueue.Enqueue)
            );
            
            module.DefineLibraryMethod("deq", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.SizedQueue, System.Object[], System.Object>(IronRuby.StandardLibrary.Threading.SizedQueue.Dequeue)
            );
            
            module.DefineLibraryMethod("enq", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.SizedQueue, System.Object, IronRuby.StandardLibrary.Threading.SizedQueue>(IronRuby.StandardLibrary.Threading.SizedQueue.Enqueue)
            );
            
            module.DefineLibraryMethod("initialize", 0x12, 
                new Func<IronRuby.StandardLibrary.Threading.SizedQueue, System.Int32, IronRuby.StandardLibrary.Threading.SizedQueue>(IronRuby.StandardLibrary.Threading.SizedQueue.Reinitialize)
            );
            
            module.DefineLibraryMethod("max", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.SizedQueue, System.Int32>(IronRuby.StandardLibrary.Threading.SizedQueue.GetLimit)
            );
            
            module.DefineLibraryMethod("max=", 0x11, 
                new Action<IronRuby.StandardLibrary.Threading.SizedQueue, System.Int32>(IronRuby.StandardLibrary.Threading.SizedQueue.SetLimit)
            );
            
            module.DefineLibraryMethod("pop", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.SizedQueue, System.Object[], System.Object>(IronRuby.StandardLibrary.Threading.SizedQueue.Dequeue)
            );
            
            module.DefineLibraryMethod("push", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.SizedQueue, System.Object, IronRuby.StandardLibrary.Threading.SizedQueue>(IronRuby.StandardLibrary.Threading.SizedQueue.Enqueue)
            );
            
            module.DefineLibraryMethod("shift", 0x11, 
                new Func<IronRuby.StandardLibrary.Threading.SizedQueue, System.Object[], System.Object>(IronRuby.StandardLibrary.Threading.SizedQueue.Dequeue)
            );
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.Sockets {
    using System;
    using Microsoft.Scripting.Utils;
    
    public sealed class SocketsLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(IronRuby.Builtins.RubyIO));
            IronRuby.Builtins.RubyClass classRef1 = GetClass(typeof(System.SystemException));
            
            
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def3 = DefineGlobalClass("BasicSocket", typeof(IronRuby.StandardLibrary.Sockets.RubyBasicSocket), 0x00000100, classRef0, LoadBasicSocket_Instance, LoadBasicSocket_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            #endif
            #if !SILVERLIGHT && !SILVERLIGHT
            IronRuby.Builtins.RubyModule def2 = DefineModule("Socket::Constants", typeof(IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants), 0x00000100, null, null, LoadSocket__Constants_Constants, IronRuby.Builtins.RubyModule.EmptyArray);
            #endif
            #if !SILVERLIGHT
            DefineGlobalClass("SocketError", typeof(System.Net.Sockets.SocketException), 0x00000000, classRef1, LoadSocketError_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(IronRuby.StandardLibrary.Sockets.SocketErrorOps.Create)
            );
            #endif
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def5 = DefineGlobalClass("IPSocket", typeof(IronRuby.StandardLibrary.Sockets.IPSocket), 0x00000100, def3, LoadIPSocket_Instance, LoadIPSocket_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            #endif
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def1 = DefineGlobalClass("Socket", typeof(IronRuby.StandardLibrary.Sockets.RubySocket), 0x00000100, def3, LoadSocket_Instance, LoadSocket_Class, LoadSocket_Constants, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, System.Object, System.Int32, System.Int32, IronRuby.StandardLibrary.Sockets.RubySocket>(IronRuby.StandardLibrary.Sockets.RubySocket.CreateSocket)
            );
            #endif
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def4 = DefineGlobalClass("TCPSocket", typeof(IronRuby.StandardLibrary.Sockets.TCPSocket), 0x00000100, def5, null, LoadTCPSocket_Class, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object, IronRuby.StandardLibrary.Sockets.TCPSocket>(IronRuby.StandardLibrary.Sockets.TCPSocket.CreateTCPSocket), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object, System.Int32, IronRuby.StandardLibrary.Sockets.TCPSocket>(IronRuby.StandardLibrary.Sockets.TCPSocket.CreateTCPSocket), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Sockets.TCPSocket>(IronRuby.StandardLibrary.Sockets.TCPSocket.CreateTCPSocket)
            );
            #endif
            #if !SILVERLIGHT
            DefineGlobalClass("UDPSocket", typeof(IronRuby.StandardLibrary.Sockets.UDPSocket), 0x00000100, def5, LoadUDPSocket_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.StandardLibrary.Sockets.UDPSocket>(IronRuby.StandardLibrary.Sockets.UDPSocket.CreateUDPSocket), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, System.Object, IronRuby.StandardLibrary.Sockets.UDPSocket>(IronRuby.StandardLibrary.Sockets.UDPSocket.CreateUDPSocket)
            );
            #endif
            #if !SILVERLIGHT
            DefineGlobalClass("TCPServer", typeof(IronRuby.StandardLibrary.Sockets.TCPServer), 0x00000100, def4, LoadTCPServer_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object, IronRuby.StandardLibrary.Sockets.TCPServer>(IronRuby.StandardLibrary.Sockets.TCPServer.CreateTCPServer)
            );
            #endif
            #if !SILVERLIGHT && !SILVERLIGHT
            def1.SetConstant("Constants", def2);
            #endif
        }
        
        #if !SILVERLIGHT
        private static void LoadBasicSocket_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("close_read", 0x11, 
                new Action<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.CloseRead)
            );
            
            module.DefineLibraryMethod("close_write", 0x11, 
                new Action<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.CloseWrite)
            );
            
            module.DefineLibraryMethod("getpeername", 0x11, 
                new Func<IronRuby.StandardLibrary.Sockets.RubyBasicSocket, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.GetPeerName)
            );
            
            module.DefineLibraryMethod("getsockname", 0x11, 
                new Func<IronRuby.StandardLibrary.Sockets.RubyBasicSocket, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.GetSocketName)
            );
            
            module.DefineLibraryMethod("getsockopt", 0x11, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, System.Int32, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.GetSocketOption)
            );
            
            module.DefineLibraryMethod("recv", 0x11, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, System.Int32, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.Receive)
            );
            
            module.DefineLibraryMethod("recv_nonblock", 0x11, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, System.Int32, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.ReceiveNonBlocking)
            );
            
            module.DefineLibraryMethod("send", 0x11, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, IronRuby.Builtins.MutableString, System.Object, System.Int32>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.Send), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, IronRuby.Builtins.MutableString, System.Object, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.Send)
            );
            
            module.DefineLibraryMethod("setsockopt", 0x11, 
                new Action<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, System.Int32, System.Int32, System.Int32>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.SetSocketOption), 
                new Action<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, System.Int32, System.Int32, System.Boolean>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.SetSocketOption), 
                new Action<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, System.Int32, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.SetSocketOption)
            );
            
            module.DefineLibraryMethod("shutdown", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, System.Int32, System.Int32>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.Shutdown)
            );
            
        }
        #endif
        
        #if !SILVERLIGHT
        private static void LoadBasicSocket_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("do_not_reverse_lookup", 0x21, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Boolean>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.GetDoNotReverseLookup)
            );
            
            module.DefineLibraryMethod("do_not_reverse_lookup=", 0x21, 
                new Action<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Boolean>(IronRuby.StandardLibrary.Sockets.RubyBasicSocket.SetDoNotReverseLookup)
            );
            
            module.DefineRuleGenerator("for_fd", 0x21, IronRuby.StandardLibrary.Sockets.RubyBasicSocket.ForFileDescriptor());
            
        }
        #endif
        
        #if !SILVERLIGHT
        private static void LoadIPSocket_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("addr", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.IPSocket, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.IPSocket.GetLocalAddress)
            );
            
            module.DefineLibraryMethod("peeraddr", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.IPSocket, System.Object>(IronRuby.StandardLibrary.Sockets.IPSocket.GetPeerAddress)
            );
            
            module.DefineLibraryMethod("recvfrom", 0x11, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.StandardLibrary.Sockets.IPSocket, System.Int32, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.IPSocket.ReceiveFrom)
            );
            
        }
        #endif
        
        #if !SILVERLIGHT
        private static void LoadIPSocket_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("getaddress", 0x21, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Builtins.RubyClass, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.IPSocket.GetAddress)
            );
            
        }
        #endif
        
        #if !SILVERLIGHT
        private static void LoadSocket_Constants(IronRuby.Builtins.RubyModule/*!*/ module) {
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
            module.SetConstant("AF_INET6", IronRuby.StandardLibrary.Sockets.RubySocket.AF_INET6);
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
            
        }
        #endif
        
        #if !SILVERLIGHT
        private static void LoadSocket_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("accept", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubySocket, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.Accept)
            );
            
            module.DefineLibraryMethod("accept_nonblock", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubySocket, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.AcceptNonBlocking)
            );
            
            module.DefineLibraryMethod("bind", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubySocket, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.StandardLibrary.Sockets.RubySocket.Bind)
            );
            
            module.DefineLibraryMethod("connect", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubySocket, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.StandardLibrary.Sockets.RubySocket.Connect)
            );
            
            module.DefineLibraryMethod("connect_nonblock", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubySocket, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.StandardLibrary.Sockets.RubySocket.ConnectNonBlocking)
            );
            
            module.DefineLibraryMethod("listen", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubySocket, System.Int32, System.Int32>(IronRuby.StandardLibrary.Sockets.RubySocket.Listen)
            );
            
            module.DefineLibraryMethod("recvfrom", 0x11, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.StandardLibrary.Sockets.RubySocket, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.ReceiveFrom), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.StandardLibrary.Sockets.RubySocket, System.Int32, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.ReceiveFrom)
            );
            
            module.DefineLibraryMethod("sysaccept", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.RubySocket, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.SysAccept)
            );
            
        }
        #endif
        
        #if !SILVERLIGHT
        private static void LoadSocket_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("getaddrinfo", 0x21, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, System.Object, System.Object, System.Object, System.Object, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.GetAddressInfo)
            );
            
            module.DefineLibraryMethod("gethostbyaddr", 0x21, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.GetHostByAddress)
            );
            
            module.DefineLibraryMethod("gethostbyname", 0x21, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.GetHostByName), 
                new Func<IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.GetHostByName), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.GetHostByName)
            );
            
            module.DefineLibraryMethod("gethostname", 0x21, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.RubySocket.GetHostname)
            );
            
            module.DefineLibraryMethod("getnameinfo", 0x21, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, IronRuby.Builtins.RubyArray, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.GetNameInfo), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.GetNameInfo)
            );
            
            module.DefineLibraryMethod("getservbyname", 0x21, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.StandardLibrary.Sockets.RubySocket.GetServiceByName)
            );
            
            module.DefineLibraryMethod("pack_sockaddr_in", 0x21, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, System.Object, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.RubySocket.PackInetSockAddr)
            );
            
            module.DefineLibraryMethod("pair", 0x21, 
                new Func<IronRuby.Builtins.RubyClass, System.Object, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.CreateSocketPair)
            );
            
            module.DefineLibraryMethod("sockaddr_in", 0x21, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Builtins.RubyClass, System.Object, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Sockets.RubySocket.PackInetSockAddr)
            );
            
            module.DefineLibraryMethod("socketpair", 0x21, 
                new Func<IronRuby.Builtins.RubyClass, System.Object, System.Object, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.CreateSocketPair)
            );
            
            module.DefineLibraryMethod("unpack_sockaddr_in", 0x21, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.RubySocket.UnPackInetSockAddr)
            );
            
        }
        #endif
        
        #if !SILVERLIGHT && !SILVERLIGHT
        private static void LoadSocket__Constants_Constants(IronRuby.Builtins.RubyModule/*!*/ module) {
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
            module.SetConstant("AF_INET6", IronRuby.StandardLibrary.Sockets.RubySocket.SocketConstants.AF_INET6);
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
        private static void LoadSocketError_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.HideMethod("message");
        }
        #endif
        
        #if !SILVERLIGHT
        private static void LoadTCPServer_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("accept", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.TCPServer, IronRuby.StandardLibrary.Sockets.TCPSocket>(IronRuby.StandardLibrary.Sockets.TCPServer.Accept)
            );
            
            module.DefineLibraryMethod("accept_nonblock", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.TCPServer, IronRuby.StandardLibrary.Sockets.TCPSocket>(IronRuby.StandardLibrary.Sockets.TCPServer.AcceptNonBlocking)
            );
            
            module.DefineLibraryMethod("listen", 0x11, 
                new Action<IronRuby.StandardLibrary.Sockets.TCPServer, System.Int32>(IronRuby.StandardLibrary.Sockets.TCPServer.Listen)
            );
            
            module.DefineLibraryMethod("sysaccept", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Sockets.TCPServer, System.Int32>(IronRuby.StandardLibrary.Sockets.TCPServer.SysAccept)
            );
            
        }
        #endif
        
        #if !SILVERLIGHT
        private static void LoadTCPSocket_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("gethostbyname", 0x21, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Builtins.RubyClass, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.TCPSocket.GetHostByName)
            );
            
        }
        #endif
        
        #if !SILVERLIGHT
        private static void LoadUDPSocket_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("bind", 0x11, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.StandardLibrary.Sockets.UDPSocket, System.Object, System.Object, System.Int32>(IronRuby.StandardLibrary.Sockets.UDPSocket.Bind)
            );
            
            module.DefineLibraryMethod("connect", 0x11, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.StandardLibrary.Sockets.UDPSocket, System.Object, System.Object, System.Int32>(IronRuby.StandardLibrary.Sockets.UDPSocket.Connect)
            );
            
            module.DefineLibraryMethod("recvfrom_nonblock", 0x11, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.StandardLibrary.Sockets.IPSocket, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.UDPSocket.ReceiveFromNonBlocking), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.StandardLibrary.Sockets.IPSocket, System.Int32, System.Object, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Sockets.UDPSocket.ReceiveFromNonBlocking)
            );
            
            module.DefineLibraryMethod("send", 0x11, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, IronRuby.Builtins.MutableString, System.Object, System.Object, System.Object, System.Int32>(IronRuby.StandardLibrary.Sockets.UDPSocket.Send), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, IronRuby.Builtins.MutableString, System.Object, System.Int32>(IronRuby.StandardLibrary.Sockets.UDPSocket.Send), 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.StandardLibrary.Sockets.RubyBasicSocket, IronRuby.Builtins.MutableString, System.Object, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.StandardLibrary.Sockets.UDPSocket.Send)
            );
            
        }
        #endif
        
    }
}

namespace IronRuby.StandardLibrary.OpenSsl {
    using System;
    using Microsoft.Scripting.Utils;
    
    public sealed class OpenSslLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(System.Object));
            IronRuby.Builtins.RubyClass classRef1 = GetClass(typeof(System.Runtime.InteropServices.ExternalException));
            
            
            IronRuby.Builtins.RubyModule def1 = DefineGlobalModule("OpenSSL", typeof(IronRuby.StandardLibrary.OpenSsl.OpenSsl), 0x00000100, null, null, LoadOpenSSL_Constants, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def2 = DefineClass("OpenSSL::BN", typeof(IronRuby.StandardLibrary.OpenSsl.OpenSsl.BN), 0x00000100, classRef0, null, LoadOpenSSL__BN_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def3 = DefineModule("OpenSSL::Digest", typeof(IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory), 0x00000100, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def4 = DefineClass("OpenSSL::Digest::Digest", typeof(IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest), 0x00000100, classRef0, LoadOpenSSL__Digest__Digest_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest.CreateDigest)
            );
            IronRuby.Builtins.RubyClass def5 = DefineClass("OpenSSL::HMAC", typeof(IronRuby.StandardLibrary.OpenSsl.OpenSsl.HMAC), 0x00000100, classRef0, null, LoadOpenSSL__HMAC_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def6 = DefineModule("OpenSSL::PKey", typeof(IronRuby.StandardLibrary.OpenSsl.OpenSsl.PKey), 0x00000100, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def7 = DefineClass("OpenSSL::PKey::RSA", typeof(IronRuby.StandardLibrary.OpenSsl.OpenSsl.PKey.RSA), 0x00000100, classRef0, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def8 = DefineModule("OpenSSL::Random", typeof(IronRuby.StandardLibrary.OpenSsl.OpenSsl.RandomModule), 0x00000100, null, LoadOpenSSL__Random_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def9 = DefineModule("OpenSSL::X509", typeof(IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509), 0x00000100, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def10 = DefineClass("OpenSSL::X509::Certificate", typeof(IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate), 0x00000100, classRef0, LoadOpenSSL__X509__Certificate_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate.CreateCertificate), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate.CreateCertificate)
            );
            IronRuby.Builtins.RubyClass def12 = DefineClass("OpenSSL::X509::CertificateError", typeof(System.Security.Cryptography.CryptographicException), 0x00000000, classRef1, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Security.Cryptography.CryptographicException>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.CryptographicExceptionOps.Create)
            );
            IronRuby.Builtins.RubyClass def11 = DefineClass("OpenSSL::X509::Name", typeof(IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Name), 0x00000100, classRef0, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            def1.SetConstant("BN", def2);
            def1.SetConstant("Digest", def3);
            def3.SetConstant("Digest", def4);
            def1.SetConstant("HMAC", def5);
            def1.SetConstant("PKey", def6);
            def6.SetConstant("RSA", def7);
            def1.SetConstant("Random", def8);
            def1.SetConstant("X509", def9);
            def9.SetConstant("Certificate", def10);
            def9.SetConstant("CertificateError", def12);
            def9.SetConstant("Name", def11);
        }
        
        private static void LoadOpenSSL_Constants(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("OPENSSL_VERSION", IronRuby.StandardLibrary.OpenSsl.OpenSsl.OPENSSL_VERSION);
            module.SetConstant("OPENSSL_VERSION_NUMBER", IronRuby.StandardLibrary.OpenSsl.OpenSsl.OPENSSL_VERSION_NUMBER);
            module.SetConstant("VERSION", IronRuby.StandardLibrary.OpenSsl.OpenSsl.VERSION);
            
        }
        
        private static void LoadOpenSSL__BN_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("rand", 0x21, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32, System.Int32, System.Boolean, Microsoft.Scripting.Math.BigInteger>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.BN.Rand)
            );
            
        }
        
        private static void LoadOpenSSL__Digest__Digest_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("digest", 0x11, 
                new Func<IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest.BlankDigest)
            );
            
            module.DefineLibraryMethod("digest_size", 0x11, 
                new Func<IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest, System.Int32>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest.Seed)
            );
            
            module.DefineLibraryMethod("hexdigest", 0x11, 
                new Func<IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest.BlankHexDigest)
            );
            
            module.DefineLibraryMethod("initialize", 0x12, 
                new Func<IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest.Initialize)
            );
            
            module.DefineLibraryMethod("name", 0x11, 
                new Func<IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest.Name)
            );
            
            module.DefineLibraryMethod("reset", 0x11, 
                new Func<IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest, IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest.Reset)
            );
            
        }
        
        private static void LoadOpenSSL__HMAC_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("digest", 0x21, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.HMAC.Digest)
            );
            
            module.DefineLibraryMethod("hexdigest", 0x21, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.StandardLibrary.OpenSsl.OpenSsl.DigestFactory.Digest, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.HMAC.HexDigest)
            );
            
        }
        
        private static void LoadOpenSSL__Random_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("pseudo_bytes", 0x21, 
                new Func<IronRuby.Builtins.RubyModule, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.RandomModule.RandomBytes)
            );
            
            module.DefineLibraryMethod("random_bytes", 0x21, 
                new Func<IronRuby.Builtins.RubyModule, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.RandomModule.RandomBytes)
            );
            
            module.DefineLibraryMethod("seed", 0x21, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.RandomModule.Seed)
            );
            
        }
        
        private static void LoadOpenSSL__X509__Certificate_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("initialize", 0x12, 
                new Func<IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate.Initialize)
            );
            
            module.DefineLibraryMethod("inspect", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate.ToString)
            );
            
            module.DefineLibraryMethod("issuer", 0x11, 
                new Func<IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate.Issuer)
            );
            
            module.DefineLibraryMethod("public_key", 0x11, 
                new Func<IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate.PublicKey)
            );
            
            module.DefineLibraryMethod("serial", 0x11, 
                new Func<IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate, System.Int32>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate.Serial)
            );
            
            module.DefineLibraryMethod("subject", 0x11, 
                new Func<IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate.Subject)
            );
            
            module.DefineLibraryMethod("to_s", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate.ToString)
            );
            
            module.DefineLibraryMethod("version", 0x11, 
                new Func<IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate, System.Int32>(IronRuby.StandardLibrary.OpenSsl.OpenSsl.X509.Certificate.Version)
            );
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.Digest {
    using System;
    using Microsoft.Scripting.Utils;
    
    public sealed class DigestLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(System.Object));
            
            
            IronRuby.Builtins.RubyModule def1 = DefineGlobalModule("Digest", typeof(IronRuby.StandardLibrary.Digest.Digest), 0x00000100, null, LoadDigest_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def4 = DefineModule("Digest::Instance", typeof(IronRuby.StandardLibrary.Digest.Digest.Instance), 0x00000100, LoadDigest__Instance_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def3 = DefineClass("Digest::Class", typeof(IronRuby.StandardLibrary.Digest.Digest.Class), 0x00000100, classRef0, null, LoadDigest__Class_Class, null, new IronRuby.Builtins.RubyModule[] {def4});
            IronRuby.Builtins.RubyClass def2 = DefineClass("Digest::Base", typeof(IronRuby.StandardLibrary.Digest.Digest.Base), 0x00000100, def3, LoadDigest__Base_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def5 = DefineClass("Digest::MD5", typeof(IronRuby.StandardLibrary.Digest.Digest.MD5), 0x00000100, def2, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            #endif
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def6 = DefineClass("Digest::SHA1", typeof(IronRuby.StandardLibrary.Digest.Digest.SHA1), 0x00000100, def2, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            #endif
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def7 = DefineClass("Digest::SHA256", typeof(IronRuby.StandardLibrary.Digest.Digest.SHA256), 0x00000100, def2, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            #endif
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def8 = DefineClass("Digest::SHA384", typeof(IronRuby.StandardLibrary.Digest.Digest.SHA384), 0x00000100, def2, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            #endif
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def9 = DefineClass("Digest::SHA512", typeof(IronRuby.StandardLibrary.Digest.Digest.SHA512), 0x00000100, def2, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
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
        
        private static void LoadDigest_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("const_missing", 0x21, 
                new Func<IronRuby.Builtins.RubyModule, System.String, System.Object>(IronRuby.StandardLibrary.Digest.Digest.ConstantMissing)
            );
            
            module.DefineLibraryMethod("hexencode", 0x21, 
                new Func<IronRuby.Builtins.RubyModule, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.HexEncode)
            );
            
        }
        
        private static void LoadDigest__Base_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("<<", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Digest.Digest.Base, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Digest.Digest.Base>(IronRuby.StandardLibrary.Digest.Digest.Base.Update)
            );
            
            module.DefineLibraryMethod("finish", 0x12, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Digest.Digest.Base, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Base.Finish)
            );
            
            module.DefineLibraryMethod("reset", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Digest.Digest.Base, IronRuby.StandardLibrary.Digest.Digest.Base>(IronRuby.StandardLibrary.Digest.Digest.Base.Reset)
            );
            
            module.DefineLibraryMethod("update", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Digest.Digest.Base, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Digest.Digest.Base>(IronRuby.StandardLibrary.Digest.Digest.Base.Update)
            );
            
        }
        
        private static void LoadDigest__Class_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("digest", 0x21, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.MutableString, System.Object>>, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Class.Digest), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Class.Digest)
            );
            
            module.DefineLibraryMethod("hexdigest", 0x21, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.MutableString, System.Object>>, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Class.HexDigest), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Class.HexDigest)
            );
            
        }
        
        private static void LoadDigest__Instance_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("digest", 0x11, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object>>, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Instance.Digest), 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.MutableString, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object>>, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Instance.Digest)
            );
            
            module.DefineLibraryMethod("digest!", 0x11, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object>>, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Instance.DigestNew)
            );
            
            module.DefineLibraryMethod("hexdigest", 0x11, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, IronRuby.Builtins.RubyClass, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object>>, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Instance.HexDigest), 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.MutableString, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object>>, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Instance.HexDigest)
            );
            
            module.DefineLibraryMethod("hexdigest!", 0x11, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object>>, IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, System.Object>>, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Digest.Digest.Instance.HexDigestNew)
            );
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.Zlib {
    using System;
    using Microsoft.Scripting.Utils;
    
    public sealed class ZlibLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(System.SystemException));
            IronRuby.Builtins.RubyClass classRef1 = GetClass(typeof(System.Object));
            IronRuby.Builtins.RubyClass classRef2 = GetClass(typeof(IronRuby.Builtins.RuntimeError));
            
            
            IronRuby.Builtins.RubyModule def1 = DefineGlobalModule("Zlib", typeof(IronRuby.StandardLibrary.Zlib.Zlib), 0x00000100, null, null, LoadZlib_Constants, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def5 = DefineClass("Zlib::Error", typeof(IronRuby.StandardLibrary.Zlib.Zlib.Error), 0x00000100, classRef0, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(ZlibLibraryInitializer.ExceptionFactory__Zlib__Error));
            IronRuby.Builtins.RubyClass def6 = DefineClass("Zlib::GzipFile", typeof(IronRuby.StandardLibrary.Zlib.Zlib.GZipFile), 0x00000100, classRef1, LoadZlib__GzipFile_Instance, LoadZlib__GzipFile_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def7 = DefineClass("Zlib::GzipFile::Error", typeof(IronRuby.StandardLibrary.Zlib.Zlib.GZipFile.Error), 0x00000100, classRef2, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def12 = DefineClass("Zlib::ZStream", typeof(IronRuby.StandardLibrary.Zlib.Zlib.ZStream), 0x00000100, classRef1, LoadZlib__ZStream_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def2 = DefineClass("Zlib::BufError", typeof(IronRuby.StandardLibrary.Zlib.Zlib.BufError), 0x00000100, def5, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(ZlibLibraryInitializer.ExceptionFactory__Zlib__BufError));
            IronRuby.Builtins.RubyClass def3 = DefineClass("Zlib::DataError", typeof(IronRuby.StandardLibrary.Zlib.Zlib.DataError), 0x00000100, def5, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(ZlibLibraryInitializer.ExceptionFactory__Zlib__DataError));
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def4 = DefineClass("Zlib::Deflate", typeof(IronRuby.StandardLibrary.Zlib.Zlib.Deflate), 0x00000100, def12, LoadZlib__Deflate_Instance, LoadZlib__Deflate_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            #endif
            IronRuby.Builtins.RubyClass def8 = DefineClass("Zlib::GzipReader", typeof(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader), 0x00000100, def6, LoadZlib__GzipReader_Instance, LoadZlib__GzipReader_Class, LoadZlib__GzipReader_Constants, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Runtime.RespondToStorage, IronRuby.Builtins.RubyClass, System.Object, IronRuby.StandardLibrary.Zlib.Zlib.GZipReader>(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.Create)
            );
            #if !SILVERLIGHT
            IronRuby.Builtins.RubyClass def9 = DefineClass("Zlib::GzipWriter", typeof(IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter), 0x00000100, def6, LoadZlib__GzipWriter_Instance, LoadZlib__GzipWriter_Class, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Runtime.RespondToStorage, IronRuby.Builtins.RubyClass, System.Object, System.Int32, System.Int32, IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter>(IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter.Create)
            );
            #endif
            IronRuby.Builtins.RubyClass def10 = DefineClass("Zlib::Inflate", typeof(IronRuby.StandardLibrary.Zlib.Zlib.Inflate), 0x00000100, def12, LoadZlib__Inflate_Instance, LoadZlib__Inflate_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def11 = DefineClass("Zlib::StreamError", typeof(IronRuby.StandardLibrary.Zlib.Zlib.StreamError), 0x00000100, def5, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
            new Func<IronRuby.Builtins.RubyClass, System.Object, System.Exception>(ZlibLibraryInitializer.ExceptionFactory__Zlib__StreamError));
            def1.SetConstant("Error", def5);
            def1.SetConstant("GzipFile", def6);
            def6.SetConstant("Error", def7);
            def1.SetConstant("ZStream", def12);
            def1.SetConstant("BufError", def2);
            def1.SetConstant("DataError", def3);
            #if !SILVERLIGHT
            def1.SetConstant("Deflate", def4);
            #endif
            def1.SetConstant("GzipReader", def8);
            #if !SILVERLIGHT
            def1.SetConstant("GzipWriter", def9);
            #endif
            def1.SetConstant("Inflate", def10);
            def1.SetConstant("StreamError", def11);
        }
        
        private static void LoadZlib_Constants(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("ASCII", IronRuby.StandardLibrary.Zlib.Zlib.ASCII);
            module.SetConstant("BEST_COMPRESSION", IronRuby.StandardLibrary.Zlib.Zlib.BEST_COMPRESSION);
            module.SetConstant("BEST_SPEED", IronRuby.StandardLibrary.Zlib.Zlib.BEST_SPEED);
            module.SetConstant("BINARY", IronRuby.StandardLibrary.Zlib.Zlib.BINARY);
            module.SetConstant("DEFAULT_COMPRESSION", IronRuby.StandardLibrary.Zlib.Zlib.DEFAULT_COMPRESSION);
            module.SetConstant("DEFAULT_STRATEGY", IronRuby.StandardLibrary.Zlib.Zlib.DEFAULT_STRATEGY);
            module.SetConstant("FILTERED", IronRuby.StandardLibrary.Zlib.Zlib.FILTERED);
            module.SetConstant("FINISH", IronRuby.StandardLibrary.Zlib.Zlib.FINISH);
            module.SetConstant("FIXLCODES", IronRuby.StandardLibrary.Zlib.Zlib.FIXLCODES);
            module.SetConstant("FULL_FLUSH", IronRuby.StandardLibrary.Zlib.Zlib.FULL_FLUSH);
            module.SetConstant("HUFFMAN_ONLY", IronRuby.StandardLibrary.Zlib.Zlib.HUFFMAN_ONLY);
            module.SetConstant("MAX_WBITS", IronRuby.StandardLibrary.Zlib.Zlib.MAX_WBITS);
            module.SetConstant("MAXBITS", IronRuby.StandardLibrary.Zlib.Zlib.MAXBITS);
            module.SetConstant("MAXCODES", IronRuby.StandardLibrary.Zlib.Zlib.MAXCODES);
            module.SetConstant("MAXDCODES", IronRuby.StandardLibrary.Zlib.Zlib.MAXDCODES);
            module.SetConstant("MAXLCODES", IronRuby.StandardLibrary.Zlib.Zlib.MAXLCODES);
            module.SetConstant("NO_COMPRESSION", IronRuby.StandardLibrary.Zlib.Zlib.NO_COMPRESSION);
            module.SetConstant("NO_FLUSH", IronRuby.StandardLibrary.Zlib.Zlib.NO_FLUSH);
            module.SetConstant("SYNC_FLUSH", IronRuby.StandardLibrary.Zlib.Zlib.SYNC_FLUSH);
            module.SetConstant("UNKNOWN", IronRuby.StandardLibrary.Zlib.Zlib.UNKNOWN);
            module.SetConstant("VERSION", IronRuby.StandardLibrary.Zlib.Zlib.VERSION);
            module.SetConstant("Z_DEFLATED", IronRuby.StandardLibrary.Zlib.Zlib.Z_DEFLATED);
            module.SetConstant("ZLIB_VERSION", IronRuby.StandardLibrary.Zlib.Zlib.ZLIB_VERSION);
            
        }
        
        #if !SILVERLIGHT
        private static void LoadZlib__Deflate_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("deflate", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.Deflate, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.Deflate.DeflateString)
            );
            
        }
        #endif
        
        #if !SILVERLIGHT
        private static void LoadZlib__Deflate_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("deflate", 0x21, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.Deflate.DeflateString)
            );
            
        }
        #endif
        
        private static void LoadZlib__GzipFile_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("closed?", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.GZipFile, System.Boolean>(IronRuby.StandardLibrary.Zlib.Zlib.GZipFile.IsClosed)
            );
            
            module.DefineLibraryMethod("comment", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.GZipFile, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.GZipFile.Comment)
            );
            
            module.DefineLibraryMethod("orig_name", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.GZipFile, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.GZipFile.OriginalName)
            );
            
            module.DefineLibraryMethod("original_name", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.GZipFile, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.GZipFile.OriginalName)
            );
            
        }
        
        private static void LoadZlib__GzipFile_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("wrap", 0x21, 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, System.Object, System.Object>(IronRuby.StandardLibrary.Zlib.Zlib.GZipFile.Wrap)
            );
            
        }
        
        private static void LoadZlib__GzipReader_Constants(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.SetConstant("OSES", IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.OSES);
            
        }
        
        private static void LoadZlib__GzipReader_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("close", 0x11, 
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Zlib.Zlib.GZipReader, System.Object>(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.Close)
            );
            
            module.DefineLibraryMethod("finish", 0x11, 
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Zlib.Zlib.GZipReader, System.Object>(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.Finish)
            );
            
            module.DefineLibraryMethod("open", 0x12, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.GZipReader, IronRuby.StandardLibrary.Zlib.Zlib.GZipReader>(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.Open)
            );
            
            module.DefineLibraryMethod("read", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.GZipReader, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.Read)
            );
            
            module.DefineLibraryMethod("xtra_field", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.GZipReader, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.ExtraField)
            );
            
        }
        
        private static void LoadZlib__GzipReader_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("open", 0x21, 
                new Func<IronRuby.Runtime.RespondToStorage, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Zlib.Zlib.GZipReader>(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.Open), 
                new Func<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object>(IronRuby.StandardLibrary.Zlib.Zlib.GZipReader.Open)
            );
            
        }
        
        #if !SILVERLIGHT
        private static void LoadZlib__GzipWriter_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("<<", 0x11, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter>(IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter.Output)
            );
            
            module.DefineLibraryMethod("close", 0x11, 
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter, System.Object>(IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter.Close)
            );
            
            module.DefineLibraryMethod("comment=", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter.Comment)
            );
            
            module.DefineLibraryMethod("finish", 0x11, 
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter, System.Object>(IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter.Finish)
            );
            
            module.DefineLibraryMethod("flush", 0x11, 
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter, System.Object, IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter>(IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter.Flush), 
                new Func<IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter, System.Int32, IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter>(IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter.Flush)
            );
            
            module.DefineLibraryMethod("orig_name=", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter.OriginalName)
            );
            
            module.DefineLibraryMethod("write", 0x11, 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter.Write)
            );
            
        }
        #endif
        
        #if !SILVERLIGHT
        private static void LoadZlib__GzipWriter_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("open", 0x21, 
                new Func<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, System.Int32, System.Object>(IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter.Open), 
                new Func<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Object, System.Object, System.Object>(IronRuby.StandardLibrary.Zlib.Zlib.GzipWriter.Open)
            );
            
        }
        #endif
        
        private static void LoadZlib__Inflate_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("close", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.Inflate, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.Inflate.Close)
            );
            
            module.DefineLibraryMethod("inflate", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.Inflate, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.Inflate.InflateString)
            );
            
        }
        
        private static void LoadZlib__Inflate_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("inflate", 0x21, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Zlib.Zlib.Inflate.InflateString)
            );
            
        }
        
        private static void LoadZlib__ZStream_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("adler", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Int32>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.Adler)
            );
            
            module.DefineLibraryMethod("avail_in", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Int32>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.AvailIn)
            );
            
            module.DefineLibraryMethod("avail_out", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Int32>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.GetAvailOut)
            );
            
            module.DefineLibraryMethod("avail_out=", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Int32, System.Int32>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.SetAvailOut)
            );
            
            module.DefineLibraryMethod("close", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Boolean>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.Close)
            );
            
            module.DefineLibraryMethod("closed?", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Boolean>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.IsClosed)
            );
            
            module.DefineLibraryMethod("data_type", 0x11, 
                new Action<IronRuby.StandardLibrary.Zlib.Zlib.ZStream>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.DataType)
            );
            
            module.DefineLibraryMethod("finish", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Boolean>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.Close)
            );
            
            module.DefineLibraryMethod("finished?", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Boolean>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.IsClosed)
            );
            
            module.DefineLibraryMethod("flush_next_in", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Collections.Generic.List<System.Byte>>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.FlushNextIn)
            );
            
            module.DefineLibraryMethod("flush_next_out", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Collections.Generic.List<System.Byte>>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.FlushNextOut)
            );
            
            module.DefineLibraryMethod("reset", 0x11, 
                new Action<IronRuby.StandardLibrary.Zlib.Zlib.ZStream>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.Reset)
            );
            
            module.DefineLibraryMethod("stream_end?", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Boolean>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.IsClosed)
            );
            
            module.DefineLibraryMethod("total_in", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Int32>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.TotalIn)
            );
            
            module.DefineLibraryMethod("total_out", 0x11, 
                new Func<IronRuby.StandardLibrary.Zlib.Zlib.ZStream, System.Int32>(IronRuby.StandardLibrary.Zlib.Zlib.ZStream.TotalOut)
            );
            
        }
        
        public static System.Exception/*!*/ ExceptionFactory__Zlib__BufError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.StandardLibrary.Zlib.Zlib.BufError(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__Zlib__DataError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.StandardLibrary.Zlib.Zlib.DataError(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__Zlib__Error(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.StandardLibrary.Zlib.Zlib.Error(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
        public static System.Exception/*!*/ ExceptionFactory__Zlib__StreamError(IronRuby.Builtins.RubyClass/*!*/ self, [System.Runtime.InteropServices.DefaultParameterValueAttribute(null)]object message) {
            return IronRuby.Runtime.RubyExceptionData.InitializeException(new IronRuby.StandardLibrary.Zlib.Zlib.StreamError(IronRuby.Runtime.RubyExceptionData.GetClrMessage(self, message), (System.Exception)null), message);
        }
        
    }
}

namespace IronRuby.StandardLibrary.StringIO {
    using System;
    using Microsoft.Scripting.Utils;
    
    public sealed class StringIOLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyModule moduleRef0 = GetModule(typeof(IronRuby.Builtins.Enumerable));
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(System.Object));
            
            
            DefineGlobalClass("StringIO", typeof(IronRuby.StandardLibrary.StringIO.StringIO), 0x00000100, classRef0, LoadStringIO_Instance, LoadStringIO_Class, null, new IronRuby.Builtins.RubyModule[] {moduleRef0}, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.Create), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.Create), 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.Create)
            );
        }
        
        private static void LoadStringIO_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("<<", 0x11, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object, System.Object>(IronRuby.StandardLibrary.StringIO.StringIO.Output)
            );
            
            module.DefineLibraryMethod("binmode", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.SetBinaryMode)
            );
            
            module.DefineLibraryMethod("close", 0x11, 
                new Action<IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.Close)
            );
            
            module.DefineLibraryMethod("close_read", 0x11, 
                new Action<IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.CloseRead)
            );
            
            module.DefineLibraryMethod("close_write", 0x11, 
                new Action<IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.CloseWrite)
            );
            
            module.DefineLibraryMethod("closed?", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Boolean>(IronRuby.StandardLibrary.StringIO.StringIO.IsClosed)
            );
            
            module.DefineLibraryMethod("closed_read?", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Boolean>(IronRuby.StandardLibrary.StringIO.StringIO.IsClosedRead)
            );
            
            module.DefineLibraryMethod("closed_write?", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Boolean>(IronRuby.StandardLibrary.StringIO.StringIO.IsClosedWrite)
            );
            
            module.DefineLibraryMethod("each", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.StandardLibrary.StringIO.StringIO, System.Object>(IronRuby.StandardLibrary.StringIO.StringIO.EachLine), 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString, System.Object>(IronRuby.StandardLibrary.StringIO.StringIO.EachLine)
            );
            
            module.DefineLibraryMethod("each_byte", 0x11, 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.StandardLibrary.StringIO.StringIO, System.Object>(IronRuby.StandardLibrary.StringIO.StringIO.EachByte)
            );
            
            module.DefineLibraryMethod("each_line", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Runtime.BlockParam, IronRuby.StandardLibrary.StringIO.StringIO, System.Object>(IronRuby.StandardLibrary.StringIO.StringIO.EachLine), 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString, System.Object>(IronRuby.StandardLibrary.StringIO.StringIO.EachLine)
            );
            
            module.DefineLibraryMethod("eof", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Boolean>(IronRuby.StandardLibrary.StringIO.StringIO.Eof)
            );
            
            module.DefineLibraryMethod("eof?", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Boolean>(IronRuby.StandardLibrary.StringIO.StringIO.Eof)
            );
            
            module.DefineLibraryMethod("fcntl", 0x11, 
                new Action<IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.FileControl)
            );
            
            module.DefineLibraryMethod("fileno", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Object>(IronRuby.StandardLibrary.StringIO.StringIO.GetDescriptor)
            );
            
            module.DefineLibraryMethod("flush", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.Flush)
            );
            
            module.DefineLibraryMethod("fsync", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.FSync)
            );
            
            module.DefineLibraryMethod("getc", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Object>(IronRuby.StandardLibrary.StringIO.StringIO.GetByte)
            );
            
            module.DefineLibraryMethod("gets", 0x11, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringIO.StringIO.Gets), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringIO.StringIO.Gets)
            );
            
            module.DefineLibraryMethod("initialize", 0x12, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.Reinitialize), 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.Reinitialize), 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString, System.Int32, IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.Reinitialize)
            );
            
            module.DefineLibraryMethod("initialize_copy", 0x12, 
                new Func<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.StandardLibrary.StringIO.StringIO, System.Object, IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.Reopen), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.Reopen)
            );
            
            module.DefineLibraryMethod("isatty", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Boolean>(IronRuby.StandardLibrary.StringIO.StringIO.IsConsole)
            );
            
            module.DefineLibraryMethod("length", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.GetLength)
            );
            
            module.DefineLibraryMethod("lineno", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.GetLineNo)
            );
            
            module.DefineLibraryMethod("lineno=", 0x11, 
                new Action<IronRuby.StandardLibrary.StringIO.StringIO, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.SetLineNo)
            );
            
            module.DefineLibraryMethod("path", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Object>(IronRuby.StandardLibrary.StringIO.StringIO.GetDescriptor)
            );
            
            module.DefineLibraryMethod("pid", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Object>(IronRuby.StandardLibrary.StringIO.StringIO.GetDescriptor)
            );
            
            module.DefineLibraryMethod("pos", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.GetPosition)
            );
            
            module.DefineLibraryMethod("pos=", 0x11, 
                new Action<IronRuby.StandardLibrary.StringIO.StringIO, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.Pos)
            );
            
            module.DefineLibraryMethod("print", 0x11, 
                new Action<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyScope, System.Object>(IronRuby.StandardLibrary.StringIO.StringIO.Print), 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object[]>(IronRuby.StandardLibrary.StringIO.StringIO.Print), 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Object>(IronRuby.StandardLibrary.StringIO.StringIO.Print)
            );
            
            module.DefineLibraryMethod("printf", 0x11, 
                new Action<IronRuby.Builtins.StringFormatterSiteStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.BinaryOpStorage, IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString, System.Object[]>(IronRuby.StandardLibrary.StringIO.StringIO.PrintFormatted)
            );
            
            module.DefineLibraryMethod("putc", 0x11, 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringIO.StringIO.Putc), 
                new Func<IronRuby.Runtime.BinaryOpStorage, System.Object, System.Int32, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.Putc)
            );
            
            module.DefineLibraryMethod("puts", 0x11, 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object>(IronRuby.StandardLibrary.StringIO.StringIO.PutsEmptyLine), 
                new Action<IronRuby.Runtime.BinaryOpStorage, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringIO.StringIO.Puts), 
                new Action<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Collections.IList>, System.Object, System.Object>(IronRuby.StandardLibrary.StringIO.StringIO.Puts), 
                new Action<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Runtime.ConversionStorage<System.Collections.IList>, System.Object, System.Object[]>(IronRuby.StandardLibrary.StringIO.StringIO.Puts)
            );
            
            module.DefineLibraryMethod("read", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, Microsoft.Scripting.Runtime.DynamicNull, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringIO.StringIO.Read), 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, Microsoft.Scripting.Runtime.DynamicNull, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringIO.StringIO.Read), 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringIO.StringIO.Read)
            );
            
            module.DefineLibraryMethod("readchar", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.ReadChar)
            );
            
            module.DefineLibraryMethod("readline", 0x11, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringIO.StringIO.ReadLine), 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringIO.StringIO.ReadLine)
            );
            
            module.DefineLibraryMethod("readlines", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.StringIO.StringIO.ReadLines), 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.StringIO.StringIO.ReadLines)
            );
            
            module.DefineLibraryMethod("reopen", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.Reopen), 
                new Func<IronRuby.Runtime.RespondToStorage, IronRuby.Runtime.UnaryOpStorage, IronRuby.StandardLibrary.StringIO.StringIO, System.Object, IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.Reopen), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.Reopen), 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.Reopen), 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.Reopen), 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString, System.Int32, IronRuby.StandardLibrary.StringIO.StringIO>(IronRuby.StandardLibrary.StringIO.StringIO.Reopen)
            );
            
            module.DefineLibraryMethod("rewind", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.Rewind)
            );
            
            module.DefineLibraryMethod("seek", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Int32, System.Int32, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.Seek)
            );
            
            module.DefineLibraryMethod("size", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.GetLength)
            );
            
            module.DefineLibraryMethod("string", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringIO.StringIO.GetString)
            );
            
            module.DefineLibraryMethod("string=", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringIO.StringIO.SetString)
            );
            
            module.DefineLibraryMethod("sync", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Boolean>(IronRuby.StandardLibrary.StringIO.StringIO.Sync)
            );
            
            module.DefineLibraryMethod("sync=", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Boolean, System.Boolean>(IronRuby.StandardLibrary.StringIO.StringIO.SetSync)
            );
            
            module.DefineLibraryMethod("sysread", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, Microsoft.Scripting.Runtime.DynamicNull, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringIO.StringIO.SystemRead), 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, Microsoft.Scripting.Runtime.DynamicNull, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringIO.StringIO.SystemRead), 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Int32, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringIO.StringIO.SystemRead)
            );
            
            module.DefineLibraryMethod("syswrite", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.Write), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.StandardLibrary.StringIO.StringIO, System.Object, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.Write)
            );
            
            module.DefineLibraryMethod("tell", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.GetPosition)
            );
            
            module.DefineLibraryMethod("truncate", 0x11, 
                new Func<IronRuby.Runtime.ConversionStorage<System.Int32>, IronRuby.StandardLibrary.StringIO.StringIO, System.Object, System.Object>(IronRuby.StandardLibrary.StringIO.StringIO.SetLength)
            );
            
            module.DefineLibraryMethod("tty?", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, System.Boolean>(IronRuby.StandardLibrary.StringIO.StringIO.IsConsole)
            );
            
            module.DefineLibraryMethod("ungetc", 0x11, 
                new Action<IronRuby.StandardLibrary.StringIO.StringIO, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.SetPreviousByte)
            );
            
            module.DefineLibraryMethod("write", 0x11, 
                new Func<IronRuby.StandardLibrary.StringIO.StringIO, IronRuby.Builtins.MutableString, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.Write), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.StandardLibrary.StringIO.StringIO, System.Object, System.Int32>(IronRuby.StandardLibrary.StringIO.StringIO.Write)
            );
            
        }
        
        private static void LoadStringIO_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineRuleGenerator("open", 0x21, IronRuby.StandardLibrary.StringIO.StringIO.Open());
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.StringScanner {
    using System;
    using Microsoft.Scripting.Utils;
    
    public sealed class StringScannerLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(IronRuby.Builtins.RubyObject));
            
            
            DefineGlobalClass("StringScanner", typeof(IronRuby.StandardLibrary.StringScanner.StringScanner), 0x00000100, classRef0, LoadStringScanner_Instance, LoadStringScanner_Class, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.StringScanner.StringScanner>(IronRuby.StandardLibrary.StringScanner.StringScanner.Create)
            );
        }
        
        private static void LoadStringScanner_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("[]", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.GetMatchSubgroup)
            );
            
            module.DefineLibraryMethod("<<", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.StringScanner.StringScanner>(IronRuby.StandardLibrary.StringScanner.StringScanner.Concat)
            );
            
            module.DefineLibraryMethod("beginning_of_line?", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Boolean>(IronRuby.StandardLibrary.StringScanner.StringScanner.BeginningOfLine)
            );
            
            module.DefineLibraryMethod("bol?", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Boolean>(IronRuby.StandardLibrary.StringScanner.StringScanner.BeginningOfLine)
            );
            
            module.DefineLibraryMethod("check", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.Check)
            );
            
            module.DefineLibraryMethod("check_until", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.CheckUntil)
            );
            
            module.DefineLibraryMethod("clear", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.StandardLibrary.StringScanner.StringScanner>(IronRuby.StandardLibrary.StringScanner.StringScanner.Clear)
            );
            
            module.DefineLibraryMethod("concat", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.StringScanner.StringScanner>(IronRuby.StandardLibrary.StringScanner.StringScanner.Concat)
            );
            
            module.DefineLibraryMethod("empty?", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Boolean>(IronRuby.StandardLibrary.StringScanner.StringScanner.EndOfLine)
            );
            
            module.DefineLibraryMethod("eos?", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Boolean>(IronRuby.StandardLibrary.StringScanner.StringScanner.EndOfLine)
            );
            
            module.DefineLibraryMethod("exist?", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, System.Nullable<System.Int32>>(IronRuby.StandardLibrary.StringScanner.StringScanner.Exist)
            );
            
            module.DefineLibraryMethod("get_byte", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.GetByte)
            );
            
            module.DefineLibraryMethod("getbyte", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.GetByte)
            );
            
            module.DefineLibraryMethod("getch", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.GetChar)
            );
            
            module.DefineLibraryMethod("initialize", 0x12, 
                new Action<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.Reinitialize)
            );
            
            module.DefineLibraryMethod("initialize_copy", 0x12, 
                new Action<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.StandardLibrary.StringScanner.StringScanner>(IronRuby.StandardLibrary.StringScanner.StringScanner.InitializeFrom)
            );
            
            module.DefineLibraryMethod("inspect", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.ToString)
            );
            
            module.DefineLibraryMethod("match?", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, System.Nullable<System.Int32>>(IronRuby.StandardLibrary.StringScanner.StringScanner.Match)
            );
            
            module.DefineLibraryMethod("matched", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.Matched)
            );
            
            module.DefineLibraryMethod("matched?", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Boolean>(IronRuby.StandardLibrary.StringScanner.StringScanner.WasMatched)
            );
            
            module.DefineLibraryMethod("matched_size", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Nullable<System.Int32>>(IronRuby.StandardLibrary.StringScanner.StringScanner.MatchedSize)
            );
            
            module.DefineLibraryMethod("matchedsize", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Nullable<System.Int32>>(IronRuby.StandardLibrary.StringScanner.StringScanner.MatchedSize)
            );
            
            module.DefineLibraryMethod("peek", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.Peek)
            );
            
            module.DefineLibraryMethod("peep", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.Peek)
            );
            
            module.DefineLibraryMethod("pointer", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Int32>(IronRuby.StandardLibrary.StringScanner.StringScanner.GetCurrentPosition)
            );
            
            module.DefineLibraryMethod("pointer=", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Int32, System.Int32>(IronRuby.StandardLibrary.StringScanner.StringScanner.SetCurrentPosition)
            );
            
            module.DefineLibraryMethod("pos", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Int32>(IronRuby.StandardLibrary.StringScanner.StringScanner.GetCurrentPosition)
            );
            
            module.DefineLibraryMethod("pos=", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Int32, System.Int32>(IronRuby.StandardLibrary.StringScanner.StringScanner.SetCurrentPosition)
            );
            
            module.DefineLibraryMethod("post_match", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.PostMatch)
            );
            
            module.DefineLibraryMethod("pre_match", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.PreMatch)
            );
            
            module.DefineLibraryMethod("reset", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.StandardLibrary.StringScanner.StringScanner>(IronRuby.StandardLibrary.StringScanner.StringScanner.Reset)
            );
            
            module.DefineLibraryMethod("rest", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.Rest)
            );
            
            module.DefineLibraryMethod("rest?", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Boolean>(IronRuby.StandardLibrary.StringScanner.StringScanner.IsRestLeft)
            );
            
            module.DefineLibraryMethod("rest_size", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Int32>(IronRuby.StandardLibrary.StringScanner.StringScanner.RestSize)
            );
            
            module.DefineLibraryMethod("restsize", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, System.Int32>(IronRuby.StandardLibrary.StringScanner.StringScanner.RestSize)
            );
            
            module.DefineLibraryMethod("scan", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.Scan)
            );
            
            module.DefineLibraryMethod("scan_full", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, System.Boolean, System.Boolean, System.Object>(IronRuby.StandardLibrary.StringScanner.StringScanner.ScanFull)
            );
            
            module.DefineLibraryMethod("scan_until", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.ScanUntil)
            );
            
            module.DefineLibraryMethod("search_full", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, System.Boolean, System.Boolean, System.Object>(IronRuby.StandardLibrary.StringScanner.StringScanner.SearchFull)
            );
            
            module.DefineLibraryMethod("skip", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, System.Nullable<System.Int32>>(IronRuby.StandardLibrary.StringScanner.StringScanner.Skip)
            );
            
            module.DefineLibraryMethod("skip_until", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.RubyRegex, System.Nullable<System.Int32>>(IronRuby.StandardLibrary.StringScanner.StringScanner.SkipUntil)
            );
            
            module.DefineLibraryMethod("string", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.GetString)
            );
            
            module.DefineLibraryMethod("string=", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.SetString)
            );
            
            module.DefineLibraryMethod("terminate", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.StandardLibrary.StringScanner.StringScanner>(IronRuby.StandardLibrary.StringScanner.StringScanner.Clear)
            );
            
            module.DefineLibraryMethod("to_s", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.StringScanner.StringScanner.ToString)
            );
            
            module.DefineLibraryMethod("unscan", 0x11, 
                new Func<IronRuby.StandardLibrary.StringScanner.StringScanner, IronRuby.StandardLibrary.StringScanner.StringScanner>(IronRuby.StandardLibrary.StringScanner.StringScanner.Unscan)
            );
            
        }
        
        private static void LoadStringScanner_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("must_C_version", 0x21, 
                new Func<System.Object, System.Object>(IronRuby.StandardLibrary.StringScanner.StringScanner.MustCVersion)
            );
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.Enumerator {
    using System;
    using Microsoft.Scripting.Utils;
    
    public sealed class EnumeratorLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(System.Object));
            
            
            IronRuby.Builtins.RubyModule def1 = DefineGlobalModule("Enumerable", typeof(IronRuby.Builtins.Enumerable), 0x00000000, LoadEnumerable_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            ExtendModule(typeof(IronRuby.Builtins.Kernel), 0x00000000, LoadIronRuby__Builtins__Kernel_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyClass def2 = DefineClass("Enumerable::Enumerator", typeof(IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator), 0x00000100, classRef0, LoadEnumerable__Enumerator_Instance, null, null, new IronRuby.Builtins.RubyModule[] {def1}, 
                new Func<IronRuby.Builtins.RubyClass, System.Object, IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator>(IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator.Create), 
                new Func<IronRuby.Builtins.RubyClass, System.Object, System.String, System.Object[], IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator>(IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator.Create)
            );
            def1.SetConstant("Enumerator", def2);
        }
        
        private static void LoadEnumerable_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("each_cons", 0x11, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BlockParam, System.Object, System.Int32, System.Object>(IronRuby.StandardLibrary.Enumerator.Enumerable.EachCons)
            );
            
            module.DefineLibraryMethod("each_slice", 0x11, 
                new Func<IronRuby.Runtime.CallSiteStorage<Func<System.Runtime.CompilerServices.CallSite, System.Object, IronRuby.Builtins.Proc, System.Object>>, IronRuby.Runtime.BlockParam, System.Object, System.Int32, System.Object>(IronRuby.StandardLibrary.Enumerator.Enumerable.EachSlice)
            );
            
            module.DefineLibraryMethod("enum_cons", 0x11, 
                new Func<System.Object, System.Int32, IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator>(IronRuby.StandardLibrary.Enumerator.Enumerable.GetConsEnumerator)
            );
            
            module.DefineLibraryMethod("enum_slice", 0x11, 
                new Func<System.Object, System.Int32, IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator>(IronRuby.StandardLibrary.Enumerator.Enumerable.GetSliceEnumerator)
            );
            
            module.DefineLibraryMethod("enum_with_index", 0x11, 
                new Func<System.Object, IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator>(IronRuby.StandardLibrary.Enumerator.Enumerable.GetEnumeratorWithIndex)
            );
            
        }
        
        private static void LoadEnumerable__Enumerator_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("each", 0x11, 
                new Func<IronRuby.Runtime.RubyScope, IronRuby.Runtime.BlockParam, IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator, System.Object>(IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator.Each)
            );
            
            module.DefineLibraryMethod("initialize", 0x12, 
                new Func<IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator, System.Object, IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator>(IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator.Reinitialize), 
                new Func<IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator, System.Object, System.String, System.Object[], IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator>(IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator.Reinitialize)
            );
            
        }
        
        private static void LoadIronRuby__Builtins__Kernel_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("enum_for", 0x11, 
                new Func<System.Object, System.String, System.Object[], IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator>(IronRuby.StandardLibrary.Enumerator.EnumerableKernelOps.Create)
            );
            
            module.DefineLibraryMethod("to_enum", 0x11, 
                new Func<System.Object, System.String, System.Object[], IronRuby.StandardLibrary.Enumerator.Enumerable.Enumerator>(IronRuby.StandardLibrary.Enumerator.EnumerableKernelOps.Create)
            );
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.FunctionControl {
    using System;
    using Microsoft.Scripting.Utils;
    
    public sealed class FunctionControlLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.FileControl {
    using System;
    using Microsoft.Scripting.Utils;
    
    public sealed class FileControlLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            
            
            DefineGlobalModule("Fcntl", typeof(IronRuby.StandardLibrary.FileControl.Fcntl), 0x00000100, null, null, LoadFcntl_Constants, IronRuby.Builtins.RubyModule.EmptyArray);
        }
        
        private static void LoadFcntl_Constants(IronRuby.Builtins.RubyModule/*!*/ module) {
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
    using System;
    using Microsoft.Scripting.Utils;
    
    public sealed class BigDecimalLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(IronRuby.Builtins.Numeric));
            
            
            DefineGlobalClass("BigDecimal", typeof(IronRuby.StandardLibrary.BigDecimal.BigDecimal), 0x00000000, classRef0, LoadBigDecimal_Instance, LoadBigDecimal_Class, LoadBigDecimal_Constants, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.CreateBigDecimal)
            );
            ExtendModule(typeof(IronRuby.Builtins.Kernel), 0x00000000, LoadIronRuby__Builtins__Kernel_Instance, LoadIronRuby__Builtins__Kernel_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
        }
        
        private static void LoadBigDecimal_Constants(IronRuby.Builtins.RubyModule/*!*/ module) {
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
            
        }
        
        private static void LoadBigDecimal_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("-", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract)
            );
            
            module.DefineLibraryMethod("%", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Modulo), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Modulo), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Modulo), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ModuloOp)
            );
            
            module.DefineLibraryMethod("*", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Multiply), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Multiply), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Multiply), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Multiply)
            );
            
            module.DefineLibraryMethod("**", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Power)
            );
            
            module.DefineLibraryMethod("/", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Divide)
            );
            
            module.DefineLibraryMethod("-@", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Negate)
            );
            
            module.DefineLibraryMethod("_dump", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Dump)
            );
            
            module.DefineLibraryMethod("+", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add)
            );
            
            module.DefineLibraryMethod("+@", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Identity)
            );
            
            module.DefineLibraryMethod("<", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThan), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThan), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThan), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThan), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThan)
            );
            
            module.DefineLibraryMethod("<=", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThanOrEqual), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThanOrEqual), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThanOrEqual), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThanOrEqual), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.LessThanOrEqual)
            );
            
            module.DefineLibraryMethod("<=>", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Compare), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Compare), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Compare), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Compare), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Compare)
            );
            
            module.DefineLibraryMethod("==", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal)
            );
            
            module.DefineLibraryMethod("===", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal)
            );
            
            module.DefineLibraryMethod(">", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThan), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThan), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThan), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThan), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThan)
            );
            
            module.DefineLibraryMethod(">=", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThanOrEqual), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThanOrEqual), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThanOrEqual), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThanOrEqual), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.GreaterThanOrEqual)
            );
            
            module.DefineLibraryMethod("abs", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Abs)
            );
            
            module.DefineLibraryMethod("add", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Add)
            );
            
            module.DefineLibraryMethod("ceil", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Ceil)
            );
            
            module.DefineLibraryMethod("coerce", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Coerce), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Coerce), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Coerce), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Coerce)
            );
            
            module.DefineLibraryMethod("div", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Div), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Div)
            );
            
            module.DefineLibraryMethod("divmod", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.DivMod), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.DivMod), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.DivMod), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.DivMod)
            );
            
            module.DefineLibraryMethod("eql?", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Equal)
            );
            
            module.DefineLibraryMethod("exponent", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Exponent)
            );
            
            module.DefineLibraryMethod("finite?", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.IsFinite)
            );
            
            module.DefineLibraryMethod("fix", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Fix)
            );
            
            module.DefineLibraryMethod("floor", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Floor)
            );
            
            module.DefineLibraryMethod("frac", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Fraction)
            );
            
            module.DefineLibraryMethod("hash", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Hash)
            );
            
            module.DefineLibraryMethod("infinite?", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.IsInfinite)
            );
            
            module.DefineLibraryMethod("inspect", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Inspect)
            );
            
            module.DefineLibraryMethod("modulo", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Modulo), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Modulo), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Modulo), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Modulo), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Modulo)
            );
            
            module.DefineLibraryMethod("mult", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Multiply), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Multiply), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Multiply), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Multiply), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Multiply)
            );
            
            module.DefineLibraryMethod("nan?", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.IsNaN)
            );
            
            module.DefineLibraryMethod("nonzero?", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.IsNonZero)
            );
            
            module.DefineLibraryMethod("power", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Power)
            );
            
            module.DefineLibraryMethod("precs", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Precision)
            );
            
            module.DefineLibraryMethod("quo", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Divide)
            );
            
            module.DefineLibraryMethod("remainder", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Remainder), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Remainder)
            );
            
            module.DefineLibraryMethod("round", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Round), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Round)
            );
            
            module.DefineLibraryMethod("sign", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Sign)
            );
            
            module.DefineLibraryMethod("split", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Split)
            );
            
            module.DefineLibraryMethod("sqrt", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.SquareRoot), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.SquareRoot)
            );
            
            module.DefineLibraryMethod("sub", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, Microsoft.Scripting.Math.BigInteger, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract), 
                new Func<IronRuby.Runtime.BinaryOpStorage, IronRuby.Runtime.BinaryOpStorage, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Subtract)
            );
            
            module.DefineLibraryMethod("to_f", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Double>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ToFloat)
            );
            
            module.DefineLibraryMethod("to_i", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ToI)
            );
            
            module.DefineLibraryMethod("to_int", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Object>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ToI)
            );
            
            module.DefineLibraryMethod("to_s", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ToString), 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ToString), 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.ToString)
            );
            
            module.DefineLibraryMethod("truncate", 0x11, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Truncate)
            );
            
            module.DefineLibraryMethod("zero?", 0x11, 
                new Func<IronRuby.StandardLibrary.BigDecimal.BigDecimal, System.Boolean>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.IsZero)
            );
            
        }
        
        private static void LoadBigDecimal_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("_load", 0x21, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Load)
            );
            
            module.DefineLibraryMethod("double_fig", 0x21, 
                new Func<IronRuby.Builtins.RubyClass, System.Int32>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.DoubleFig)
            );
            
            module.DefineLibraryMethod("induced_from", 0x21, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.StandardLibrary.BigDecimal.BigDecimal, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.InducedFrom), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Int32, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.InducedFrom), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, Microsoft.Scripting.Math.BigInteger, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.InducedFrom), 
                new Func<IronRuby.Builtins.RubyClass, System.Object, IronRuby.StandardLibrary.BigDecimal.BigDecimal>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.InducedFrom)
            );
            
            module.DefineLibraryMethod("limit", 0x21, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Int32, System.Int32>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Limit), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Object, System.Int32>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Limit)
            );
            
            module.DefineLibraryMethod("mode", 0x21, 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Int32, System.Int32>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Mode), 
                new Func<IronRuby.Runtime.RubyContext, IronRuby.Builtins.RubyClass, System.Int32, System.Object, System.Int32>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Mode)
            );
            
            module.DefineLibraryMethod("ver", 0x21, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.BigDecimal.BigDecimalOps.Version)
            );
            
        }
        
        private static void LoadIronRuby__Builtins__Kernel_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("BigDecimal", 0x12, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.KernelOps.CreateBigDecimal)
            );
            
        }
        
        private static void LoadIronRuby__Builtins__Kernel_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("BigDecimal", 0x21, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, System.Int32, System.Object>(IronRuby.StandardLibrary.BigDecimal.KernelOps.CreateBigDecimal)
            );
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.Iconv {
    using System;
    using Microsoft.Scripting.Utils;
    
    public sealed class IconvLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(System.Object));
            
            
            DefineGlobalClass("Iconv", typeof(IronRuby.StandardLibrary.Iconv.Iconv), 0x00000100, classRef0, LoadIconv_Instance, LoadIconv_Class, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Iconv.Iconv>(IronRuby.StandardLibrary.Iconv.Iconv.Create)
            );
        }
        
        private static void LoadIconv_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("close", 0x11, 
                new Func<IronRuby.StandardLibrary.Iconv.Iconv, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Iconv.Iconv.Close)
            );
            
            module.DefineLibraryMethod("iconv", 0x11, 
                new Func<IronRuby.StandardLibrary.Iconv.Iconv, IronRuby.Builtins.MutableString, System.Int32, System.Int32, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Iconv.Iconv.iconv)
            );
            
            module.DefineLibraryMethod("initialize", 0x12, 
                new Func<IronRuby.StandardLibrary.Iconv.Iconv, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Iconv.Iconv>(IronRuby.StandardLibrary.Iconv.Iconv.Initialize)
            );
            
        }
        
        private static void LoadIconv_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("charset_map", 0x21, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.Hash>(IronRuby.StandardLibrary.Iconv.Iconv.CharsetMap)
            );
            
            module.DefineLibraryMethod("conv", 0x21, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Iconv.Iconv.Convert)
            );
            
            module.DefineLibraryMethod("iconv", 0x21, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString[], IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Iconv.Iconv.iconv)
            );
            
            module.DefineLibraryMethod("open", 0x21, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Iconv.Iconv>(IronRuby.StandardLibrary.Iconv.Iconv.Create), 
                new Func<IronRuby.Runtime.BlockParam, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString>(IronRuby.StandardLibrary.Iconv.Iconv.Open)
            );
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.ParseTree {
    using System;
    using Microsoft.Scripting.Utils;
    
    public sealed class ParseTreeLibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            
            
            IronRuby.Builtins.RubyModule def1 = DefineGlobalModule("IronRuby", typeof(IronRuby.Ruby), 0x00000000, null, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            IronRuby.Builtins.RubyModule def2 = DefineModule("IronRuby::ParseTree", typeof(IronRuby.StandardLibrary.ParseTree.IronRubyOps.ParseTreeOps), 0x00000100, LoadIronRuby__ParseTree_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray);
            def1.SetConstant("ParseTree", def2);
        }
        
        private static void LoadIronRuby__ParseTree_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineLibraryMethod("parse_tree_for_meth", 0x11, 
                new Func<System.Object, IronRuby.Builtins.RubyModule, System.String, System.Boolean, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.ParseTree.IronRubyOps.ParseTreeOps.CreateParseTreeForMethod)
            );
            
            module.DefineLibraryMethod("parse_tree_for_str", 0x11, 
                new Func<IronRuby.Runtime.RubyScope, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Int32, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.ParseTree.IronRubyOps.ParseTreeOps.CreateParseTreeForString)
            );
            
        }
        
    }
}

namespace IronRuby.StandardLibrary.Open3 {
    using System;
    using Microsoft.Scripting.Utils;
    
    public sealed class Open3LibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            
            
            DefineGlobalModule("Open3", typeof(IronRuby.StandardLibrary.Open3.Open3), 0x00000100, null, LoadOpen3_Class, null, IronRuby.Builtins.RubyModule.EmptyArray);
        }
        
        private static void LoadOpen3_Class(IronRuby.Builtins.RubyModule/*!*/ module) {
            #if !SILVERLIGHT
            module.DefineLibraryMethod("popen3", 0x21, 
                new Func<IronRuby.Runtime.RubyContext, System.Object, IronRuby.Builtins.MutableString, IronRuby.Builtins.RubyArray>(IronRuby.StandardLibrary.Open3.Open3.OpenPipe)
            );
            
            #endif
        }
        
    }
}

namespace IronRuby.StandardLibrary.Win32API {
    using System;
    using Microsoft.Scripting.Utils;
    
    public sealed class Win32APILibraryInitializer : IronRuby.Builtins.LibraryInitializer {
        protected override void LoadModules() {
            IronRuby.Builtins.RubyClass classRef0 = GetClass(typeof(System.Object));
            
            
            #if !SILVERLIGHT
            DefineGlobalClass("Win32API", typeof(IronRuby.StandardLibrary.Win32API.Win32API), 0x00000100, classRef0, LoadWin32API_Instance, null, null, IronRuby.Builtins.RubyModule.EmptyArray, 
                new Func<IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Win32API.Win32API>(IronRuby.StandardLibrary.Win32API.Win32API.Create), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.Builtins.RubyClass, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Collections.IList, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Win32API.Win32API>(IronRuby.StandardLibrary.Win32API.Win32API.Create)
            );
            #endif
        }
        
        #if !SILVERLIGHT
        private static void LoadWin32API_Instance(IronRuby.Builtins.RubyModule/*!*/ module) {
            module.DefineRuleGenerator("call", 0x11, IronRuby.StandardLibrary.Win32API.Win32API.Call());
            
            module.DefineRuleGenerator("Call", 0x11, IronRuby.StandardLibrary.Win32API.Win32API.Call());
            
            module.DefineLibraryMethod("initialize", 0x12, 
                new Func<IronRuby.StandardLibrary.Win32API.Win32API, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Win32API.Win32API>(IronRuby.StandardLibrary.Win32API.Win32API.Reinitialize), 
                new Func<IronRuby.Runtime.ConversionStorage<IronRuby.Builtins.MutableString>, IronRuby.StandardLibrary.Win32API.Win32API, IronRuby.Builtins.MutableString, IronRuby.Builtins.MutableString, System.Collections.IList, IronRuby.Builtins.MutableString, IronRuby.StandardLibrary.Win32API.Win32API>(IronRuby.StandardLibrary.Win32API.Win32API.Reinitialize)
            );
            
        }
        #endif
        
    }
}


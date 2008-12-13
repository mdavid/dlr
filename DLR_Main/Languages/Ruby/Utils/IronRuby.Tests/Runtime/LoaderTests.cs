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
using System.Diagnostics;
using System.IO;
using Microsoft.Scripting;
using IronRuby.Builtins;
using IronRuby.Runtime;

namespace IronRuby.Tests {
    public partial class Tests {

        public void Loader_Assemblies1() {
            string assembly;
            string type;
            string str;
            bool b;
            
            str = "a.rb";
            b = Loader.TryParseAssemblyName(str, out type, out assembly);
            Assert(b == false);

            str = "IronRuby";
            b = Loader.TryParseAssemblyName(str, out type, out assembly);
            Assert(b == false);
            
            str = @"..\foo\bar\a,b.rb";
            b = Loader.TryParseAssemblyName(str, out type, out assembly);
            Assert(b == false);
            
            str = "IronRuby.Runtime.RubyContext, IronRuby, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
            b = Loader.TryParseAssemblyName(str, out type, out assembly);
            Assert(b == true && 
                assembly == "IronRuby, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" &&
                type == "IronRuby.Runtime.RubyContext"
            );
            
            str = "IronRuby, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
            b = Loader.TryParseAssemblyName(str, out type, out assembly);
            Assert(b == true && assembly == str && type == null);

            str = "IronRuby, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
            b = Loader.TryParseAssemblyName(str, out type, out assembly);
            Assert(b == true && assembly == str && type == null);

            str = "IronRuby, Version=1.0.0.0";
            b = Loader.TryParseAssemblyName(str, out type, out assembly);
            Assert(b == true && assembly == str && type == null);
        }

        public void Require1() {
            if (_driver.PartialTrust) return;

            try {
                string temp = _driver.MakeTempDir();
                Context.Loader.SetLoadPaths(new[] { temp });
                File.WriteAllText(Path.Combine(temp, "a.rb"), @"C = 123");

                AssertOutput(delegate() {
                    CompilerTest(@"
puts(require('a'))
puts C
");
                }, @"
true
123
");

                AssertOutput(delegate() {
                    CompilerTest(@"
puts(require('a.rb'))
puts C
");
                }, @"
false
123
");
            } finally {
                File.Delete("a.rb");
            }
        }

        public void Load1() {
            if (_driver.PartialTrust) return;

            try {
                string temp = _driver.MakeTempDir();
                Context.Loader.SetLoadPaths(new[] { temp });

                File.WriteAllText(Path.Combine(temp, "a.rb"), @"C = 123");
                
                AssertOutput(delegate() {
                    CompilerTest(@"
puts(load('a.rb', true))
puts C rescue puts 'error'
");
                }, @"
true
error
");
            } finally {
                File.Delete("a.rb");
            }
        }

        public void RequireInterop1() {
            if (_driver.PartialTrust) return;

            try {
                string temp = _driver.MakeTempDir();
                Context.Loader.SetLoadPaths(new[] { temp });

                File.WriteAllText(Path.Combine(temp, "a.py"), @"
print 'Hello from Python'
");
                AssertOutput(delegate() {
                    CompilerTest(@"
require('a')
");
                }, @"
Hello from Python
");

            } finally {
                File.Delete("b.py");
            }
        }

        public class TestLibraryInitializer1 : LibraryInitializer {
            protected override void LoadModules() {
                Context.ObjectClass.SetConstant("TEST_LIBRARY", "hello from library");
                DefineGlobalModule("Object", typeof(Object), true, ObjectMonkeyPatch, null, RubyModule.EmptyArray);  
            }

            private void ObjectMonkeyPatch(RubyModule/*!*/ module) {
                Debug.Assert(module == Context.ObjectClass);

                module.DefineLibraryMethod("object_monkey", 0x9, new System.Delegate[] {
                    new Func<object, string>(MonkeyWorker),
                });
            }

            // TODO: might be called by MI.Invoke -> needs to be public in partial trust
            public static string MonkeyWorker(object obj) {
                return "This is monkey!";
            }
        }

        public void LibraryLoader1() {
            Context.DefineGlobalVariable("lib_name", MutableString.Create(typeof(TestLibraryInitializer1).AssemblyQualifiedName));

            AssertOutput(delegate() {
                CompilerTest(@"
require($lib_name)
puts TEST_LIBRARY
puts object_monkey
");
            }, @"
hello from library
This is monkey!
");

        }
    }
}

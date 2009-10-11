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

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting.Utils;

using IronRuby.Builtins;
using IronRuby.Runtime.Calls;

namespace IronRuby.Tests {
    public partial class Tests { 
        public void RubyHosting_DelegateConversions() {
            // TODO: C# bug - this fails if lambda is typed to dynamic
            object lambda = Engine.Execute(@"lambda { |a| a + 1 }");
            var result = Engine.Operations.Invoke(lambda, 5);
            Assert((int)result == 6);

            var func = Engine.Operations.ConvertTo<Func<int, int>>(lambda);
            Assert(func(10) == 11);

            var method = Engine.Execute(@"def foo(a,b); a + b; end; method(:foo)");
            var func2 = Engine.Operations.ConvertTo<Func<int, int, int>>(method);
            Assert(func2(1, 2) == 3);

            Engine.Runtime.Globals.SetVariable("F1", typeof(Func<int, int>));
            var func3 = (Func<int, int>)Engine.Execute(@"F1.to_class.new { |a| a + 3 }");
            Assert(func3(1) == 4);

            var func4 = (Func<int, int>)Engine.Execute(@"F1.to_class.new lambda { |a| a + 4 }");
            Assert(func4(1) == 5);

            var func5 = (Func<int, int>)Engine.Execute(@"F1.to_class.new(*[lambda { |a| a + 5 }])");
            Assert(func5(1) == 6);

            AssertExceptionThrown<ArgumentException>(() => Engine.Execute(@"F1.to_class.new(*[])"));

            if (_driver.RunPython) {
                var py = Runtime.GetEngine("python");

                Engine.Runtime.Globals.SetVariable("F2", typeof(Func<string[], string[], string[]>));
                var pyAdd = py.Execute(@"
def py_add(a, b): 
  return a + b
py_add
");
                Engine.Runtime.Globals.SetVariable("PyAdd", pyAdd);
                var pyFunc = (Func<string[], string[], string[]>)Engine.Execute(@"F2.to_class.new PyAdd");
                Assert(String.Join(";", pyFunc(new[] { "x" }, new[] { "y" })) == "x;y");
            }
        }

        public void RubyHosting1A() {
            ScriptScope scope = Engine.Runtime.CreateScope();
            scope.SetVariable("x", 1);
            scope.SetVariable("y", 2);

            // Reads x, y from scope via method_missing.
            // The code is instance-eval'd as a proc against the scope so both instance and 
            // singleton method definitions define a singleton method on main object.
            // Method definition on main object bound to a scope copies the method to the scope.
            Engine.Execute("def z; x + y; end", scope);
            Engine.Execute("def self.w(a); a + 1; end", scope);

            int z = scope.GetVariable<Func<int>>("z")();
            Assert(z == 3);

            int w = scope.GetVariable<Func<int, int>>("w")(1);
            Assert(w == 2);
        }

        public void RubyHosting1B() {
            ScriptScope scope = Engine.CreateScope();
            scope.SetVariable("SomeValue", 1);
            scope.SetVariable("other_value", 2);

            // Method names are unmangled for scope lookups.
            // "tmp" is defined in the top-level binding, which is associated with the scope:
            Engine.Execute("tmp = some_value + other_value", scope);
            
            // "tmp" symbol is extracted from scope's top-level binding and passed to the compiler as a compiler option
            // so that the parser treats it as a local variable.
            string tmpDefined = Engine.Execute<MutableString>("defined?(tmp)", scope).ToString();
            Assert(tmpDefined == "local-variable");

            // The code is eval'd against the existing top-level local scope created by the first execution.
            // tmp2 local is looked up dynamically:
            Engine.Execute("tmp2 = 10", scope);

            // result= is turned into a scope variable assignment in method_missing:
            Engine.Execute("self.result = tmp", scope);

            // If "scope" variable is not defined on "self" and in the DLR scope we alias it for "self":
            Engine.Execute("scope.result += tmp2", scope);

            int result = scope.GetVariable<int>("result");
            Assert(result == 13);

            // Ruby local variables are not exposed:
            Assert(scope.ContainsVariable("tmp") == false);
        }

        public void RubyHosting1C() {
            // Main singleton in a scope-unbound code doesn't define method_missing:
            AssertExceptionThrown<MemberAccessException>(
                () => Engine.Execute("class << self; remove_method(:method_missing); end")
            );
            

            // Main singleton in a scope-bound code defines method_missing:
            Engine.Execute("class << self; remove_method(:method_missing); end", Engine.CreateScope());

            var scope = Engine.CreateScope();
            Engine.Execute("self.tmp = 1", scope);
            Assert(scope.ContainsVariable("tmp"));
            
            AssertExceptionThrown<MissingMethodException>(() => Engine.Execute("self.tmp = 1"));


            // method_missing on top-level scope is defined dynamically, not at compile time:
            var compiled = Engine.CreateScriptSourceFromString("some_variable").Compile();
            scope = Engine.CreateScope();

            scope.SetVariable("some_variable", 123);
            Assert(compiled.Execute<int>(scope) == 123);
            
            AssertExceptionThrown<MissingMethodException>(() => compiled.Execute());

            scope.SetVariable("some_variable", "foo");
            Assert(compiled.Execute<string>(scope) == "foo");
        }

        public void RubyHosting1D() {
            // When executed without a scope top-level methods are defined on Object (as in MRI):
            Engine.Execute("def foo; 1; end");
            Assert(Context.ObjectClass.GetMethod("foo") != null);

            // The method is private and shouldn't be invokable via InvokeMember:
            AssertExceptionThrown<MissingMethodException>(() => Engine.Operations.InvokeMember(new object(), "foo"));

            // When executed against a scope top-level methods are defined on main singleton and also stored in the scope.
            // This is equivalent to instance_evaling the code against the main singleton.
            var scope = Engine.CreateScope();
            Engine.Execute("def bar; 1; end", scope);
            Assert(Context.ObjectClass.GetMethod("bar") == null);
            Assert(scope.GetVariable("bar") != null);
            
            // we can invoke the method on a scope:
            Assert((int)Engine.Operations.InvokeMember(scope, "bar") == 1);
            
            // Since we don't define top-level methods on Object when executing against a scope, 
            // executions against different scopes don’t step on each other:
            var scope1 = Engine.CreateScope();
            var scope2 = Engine.CreateScope();
            Engine.Execute("def foo(a,b); a + b; end", scope1);
            Engine.Execute("def foo(a,b); a - b; end", scope2);
            Assert(Engine.Execute<int>("foo(1,2)", scope1) == 3);
            Assert(Engine.Execute<int>("foo(1,2)", scope2) == -1);

            // Contrary, last one wins when executing w/o scope:
            Engine.Execute("def baz(a,b); a + b; end");
            Engine.Execute("def baz(a,b); a - b; end");
            Assert(Engine.Execute<int>("baz(1,2)") == -1);
        }

        public void RubyHosting2() {
            Hashtable hash = new Hashtable();
            hash.Add("foo", "bar");
            
            ScriptScope scope = Engine.CreateScope();
            scope.SetVariable("h", hash);

            AssertOutput(() => 
                Engine.Execute(@"
def h.method_missing name
  get_Item(name.to_clr_string)
end

puts h.foo
", scope), @"
bar
");
        }

        public void RubyHosting3() {
            object value;
            Engine.Execute("C = 1");

            // non-module values are not published:
            Assert(!Runtime.Globals.TryGetVariable("C", out value));

            // built-ins are not published:
            Assert(!Runtime.Globals.TryGetVariable("Object", out value));
            Assert(!Runtime.Globals.TryGetVariable("String", out value));

            // global modules and classes are published:
            Engine.Execute("class D; end");
            Assert(Runtime.Globals.TryGetVariable("D", out value));
            Assert(((RubyClass)value).Name == "D");
            
            // assignment to a constant on Object class also publishes modules and classes:
            Engine.Execute("E = Module.new");
            Assert(Runtime.Globals.TryGetVariable("E", out value));
            Assert(((RubyModule)value).Name == "E");

            // TODO:
            // the library paths are incorrect (not combined with location of .exe file) in partial trust:
            if (_driver.PartialTrust) return;

            var searchPaths = Engine.GetSearchPaths();
            Assert(new List<string>(searchPaths)[searchPaths.Count - 1] == ".");

            bool result = Engine.RequireRubyFile("fcntl");
            Assert(result == true);

            // built-in class:
            Assert(Context.ObjectClass.TryGetConstant(null, "String", out value) 
                && ((RubyModule)value).Restrictions == ModuleRestrictions.Builtin);

            // IronRuby specific built-in class:
            Assert(Context.ObjectClass.TryGetConstant(null, "IronRuby", out value)
                && ((RubyModule)value).Restrictions == ModuleRestrictions.NotPublished);

            // a class from standard library:
            Assert(Context.ObjectClass.TryGetConstant(null, "Fcntl", out value)
                && ((RubyModule)value).Restrictions == ModuleRestrictions.None);

            // standard library classes are also published (whether implemented in C# or not):
            var module = Runtime.Globals.GetVariable("Fcntl");
            Assert(module is RubyModule && ((RubyModule)module).Name == "Fcntl");
        }

        public void RubyHosting4() {
            Runtime.Globals.SetVariable("foo_bar", 1);
            Engine.Execute(@"
IronRuby.globals.x = 2
IronRuby.globals.z = IronRuby.globals.x + FooBar
");
            Assert(Runtime.Globals.GetVariable<int>("z") == 3);
        }

        public void Scenario_RubyEngine1() {
            ScriptScope scope = Runtime.CreateScope();
            object x = Engine.CreateScriptSourceFromString("1 + 1").Execute(scope);
            AssertEquals(x, 2);
        }

        public void Scenario_RubyInteractive1() {
            ScriptScope scope = Runtime.CreateScope();
            AssertOutput(() => Engine.CreateScriptSourceFromString("", SourceCodeKind.InteractiveCode).Execute(scope), "");
            AssertOutput(() => Engine.CreateScriptSourceFromString("x = 1 + 1", SourceCodeKind.InteractiveCode).Execute(scope), "=> 2");
        }

        public void Scenario_RubyInteractive2() {
            string s;
            Assert((s = Engine.Operations.Format(new RubyArray(new[] { 1,2,3 }))) == "[1, 2, 3]");
            
            var obj = Engine.Execute(@"class C; def to_s; 'hello'; end; new; end");
            Assert((s = Engine.Operations.Format(obj)) == "hello");

            obj = Engine.Execute(@"class C; def to_s; 'bye'; end; new; end");
            Assert((s = Engine.Operations.Format(obj)) == "bye");
            
            obj = Engine.Execute(@"class C; def inspect; [7,8,9]; end; new; end");
            Assert((s = Engine.Operations.Format(obj)) == "789");

            var scope = Engine.CreateScope();
            scope.SetVariable("ops", Engine.Operations);
            s = Engine.Execute<string>(@"ops.format({1 => 2, 3 => 4})", scope);
            Assert(s == "{1=>2, 3=>4}");
        }

        public void Scenario_RubyConsole1() {
            ScriptScope module = Runtime.CreateScope();
            AssertOutput(delegate() {
                Engine.CreateScriptSourceFromString("class C; def foo() puts 'foo'; end end", SourceCodeKind.InteractiveCode).Execute(module);
                Engine.CreateScriptSourceFromString("C.new.foo", SourceCodeKind.InteractiveCode).Execute(module);
            }, @"
=> nil
foo
=> nil
");
        }
        
        public void CrossRuntime1() {
            AssertOutput(() => {
                CompilerTest(@"
engine = IronRuby.create_engine
puts engine.execute('1+1')
puts engine.execute('Fixnum')
engine.execute('class Fixnum; def + other; 123; end; end')
puts 1+1
puts engine.execute('1+1')
");
            }, @"
2
Fixnum@*
2
123
", OutputFlags.Match);
        }
        
        public void CrossRuntime2() {
            Engine.Execute(@"
C = IronRuby.create_engine.execute <<end
  class C
    def bar
    end
  end
  C
end
");
            AssertExceptionThrown<InvalidOperationException>(() => Engine.Execute("class C; def foo; end; end"));
            
            // alias operates in the runtime of the class within which scope it is used:
            Engine.Execute("class C; alias foo bar; end");

            AssertExceptionThrown<InvalidOperationException>(() => Engine.Execute("class C; define_method(:goo) {}; end"));
            AssertExceptionThrown<InvalidOperationException>(() => Engine.Execute(@"
module M; end
class C; include M; end
"));
        }
        
        public void ObjectOperations1() {
            var cls = Engine.Execute(@"
class C
  def foo *a
    p a
  end
  self
end
");
            var obj = Engine.Operations.CreateInstance(cls) as RubyObject;
            Assert(obj != null && obj.ImmediateClass.Name == "C");

            obj = Engine.Operations.InvokeMember(cls, "new") as RubyObject;
            Assert(obj != null && obj.ImmediateClass.Name == "C");

            var foo = Engine.Operations.GetMember(obj, "foo") as RubyMethod;
            Assert(foo != null && foo.Name == "foo" && foo.Target == obj);

            AssertOutput(() => Engine.Operations.Invoke(foo, 1, 2), "[1, 2]");
            AssertOutput(() => Engine.Operations.InvokeMember(obj, "foo", 1, 2), "[1, 2]");

            var str = Engine.Operations.ConvertTo<string>(MutableString.CreateAscii("foo"));
            Assert(str == "foo");

            str = Engine.Operations.ConvertTo<string>(Engine.Execute("class C; def to_str; 'bar'; end; new; end"));
            Assert(str == "bar");

            var b = Engine.Operations.ConvertTo<byte>(Engine.Execute("class C; def to_int; 123; end; new; end"));
            Assert(b == 123);

            var lambda = Engine.Operations.ConvertTo<Func<int, int>>(Engine.Execute("lambda { |x| x * 2 }"));
            Assert(lambda(10) == 20);
        }

        public void ObjectOperations2() {
            var cls = Engine.Execute(@"
class C
  def foo *a
    p a
  end
  def self.bar
  end
  undef_method :freeze
  self
end
");
            var names = Engine.Operations.GetMemberNames(cls);
            Assert(!names.Contains("foo"));
            Assert(names.Contains("taint"));
            Assert(names.Contains("bar"));

            var obj = Engine.Operations.CreateInstance(cls);
            names = Engine.Operations.GetMemberNames(obj);
            Assert(names.Contains("foo"));
            Assert(names.Contains("taint"));
            Assert(!names.Contains("freeze"));
            Assert(!names.Contains("bar"));
        }

        public void PythonInterop1() {
            if (!_driver.RunPython) return;

            var py = Runtime.GetEngine("python");
            Engine.Execute(@"
class C
  def foo
    123
  end
end
");

            var result = py.Execute<int>(@"
import C
C().foo()
");
            Assert(result == 123);
        }

        public void PythonInterop2() {
            if (!_driver.RunPython) return;

            var py = Runtime.GetEngine("python");

            py.Execute(@"
class C(object):
  def foo(self, a, b):
    return a + b
", Runtime.Globals);

            Assert(Engine.Execute<int>("C.new.foo(3,4)") == 7);
        }

        public void PythonInterop3() {
            if (!_driver.RunPython) return;

            var py = Runtime.GetEngine("python");

            var scope = py.CreateScope();
            py.Execute(@"
def python():
  return 'Python'
", scope);

            Engine.Execute(@"
def self.ruby
  python.call + ' + Ruby'
end
", scope);

            AssertOutput(() => py.Execute(@"
print ruby()
", scope), @"
Python + Ruby
");
        }

        public void PythonInterop4() {
            if (!_driver.RunPython) return;

            var py = Runtime.GetEngine("python");

            var scope = py.CreateScope();
            py.Execute(@"
def get_python_class():
  class C(object): 
    x = 123  
    def __str__(self):
      return 'this is C'
    

  return C()
", scope);

            Engine.Execute(@"self.c = get_python_class.call", scope);

            var s = Engine.Execute<MutableString>(@"c.to_str", scope);
            Assert(s.ToString() == @"this is C");

            var i = Engine.Execute<int>(@"c.x", scope);
            Assert(i == 123);

            // TODO: test
            // c.y, where y is a delegate
            // c.p, where p is a Ruby Proc
        }

        public void PythonInterop5() {
            if (!_driver.RunPython) return;

            var py = Runtime.GetEngine("python");

            var scope = py.CreateScope();
            py.Execute(@"
from System.Collections import ArrayList
class A(ArrayList):
  def Count(self): return 123
", scope);

            Assert(Engine.Execute<int>(@"
a = A().new
a.Count
", scope) == 123);
            
        }

        public void PythonInterop6() {
            if (!_driver.RunPython) return;

            var py = Runtime.GetEngine("python");

            var scope = py.CreateScope();
            py.Execute(@"def foo(): pass", scope);

            // TODO:
            var s = Engine.Execute("foo.inspect", scope);
        }

        public void CustomTypeDescriptor1() {
            var cls = Engine.Execute(@"
class C
  attr_accessor :a
  def b
  end
  def b= x
    @written = true
  end
  def c
    @written
  end
  def d= x
  end
  def e x
  end
  def e= x
  end
  def initialize
    @a = 'Hello world'
    @written = false
  end
  self
end
");
            var obj = Engine.Operations.CreateInstance(cls);
            Assert(obj != null);
            var ictd = Engine.Operations.CreateInstance(cls) as ICustomTypeDescriptor;
            Assert(ictd != null);
            Assert(ictd.GetClassName() == "C");
            var props = ictd.GetProperties();
            Assert(props.Count == 2);
            props[0].SetValue(obj, "abc");
            props[1].SetValue(obj, "abc");
            Assert(Engine.Operations.InvokeMember(obj, "a").Equals("abc"));
            Assert(Engine.Operations.InvokeMember(obj, "c").Equals(true));
        }

        public void CustomTypeDescriptor2() {
            Context.ObjectClass.SetConstant("C", Context.GetClass(typeof(ArrayList)));
            var cls = Engine.Execute(@"
class D < C
  attr_accessor :a
  self
end
");
            var obj = Engine.Operations.CreateInstance(cls);
            Assert(obj != null);
            var ictd = Engine.Operations.CreateInstance(cls) as ICustomTypeDescriptor;
            Assert(ictd != null);
            Assert(ictd.GetClassName() == "D");
            var props = ictd.GetProperties();
            Assert(props.Count == 1);
            props[0].SetValue(obj, "abc");
            Assert(Engine.Operations.InvokeMember(obj, "a").Equals("abc"));
        }
    }
}

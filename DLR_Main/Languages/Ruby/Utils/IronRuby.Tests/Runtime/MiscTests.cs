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
using IronRuby.Compiler;
using IronRuby.Compiler.Ast;
using IronRuby.Builtins;
using Microsoft.Scripting.Math;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.Scripting.Utils;
using Microsoft.Scripting.Generation;
using System.Collections.Generic;
using IronRuby.Runtime;
using IronRuby.Runtime.Calls;

namespace IronRuby.Tests {

    public partial class Tests {
        public void Scenario_Globals1() {
            Context.DefineGlobalVariable("x", 123);
            CompilerTest(@"$z = $x");
            object z = Context.GetGlobalVariable("z");
            AssertEquals(z, 123);
        }

        public void Scenario_RubyLocals1() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo()
  x = 1
  y = 2
  puts x + y
end

foo
");
            }, "3");
        }

        public void Scenario_UninitializedVars1() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo
  x = 1 if false
  puts x
end

foo
");
            }, "nil");
        }

        public void Scenario_UninitializedVars2() {
            AssertExceptionThrown<MemberAccessException>(delegate() {
                CompilerTest(@"
def foo
  puts x
end

foo
");
            });
        }

        // Fixnum doesn't have identity in Ruby
        public void InstanceVariables1() {
            AssertOutput(delegate() {
                CompilerTest(@"
class Fixnum
  def foo= a
    @x = a
  end
  def foo
    @x
  end
end

a = 1
b = 1
c = 2

a.foo = 1
b.foo = 2
c.foo = 3
puts a.foo, b.foo, c.foo
");
            },
            @"
2
2
3");
        }

        // Float has an identity in Ruby
        public void InstanceVariables2() {
            AssertOutput(delegate() {
                CompilerTest(@"
class Float
  def foo= a
    @x = a
  end
  def foo
    @x
  end
end

a = 1.0
b = 1.0

a.foo = 1
b.foo = 2
puts a.foo, b.foo
");
            },
            @"
1
2");
        }


        public void Scenario_RubyParams1() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo(a,b)
    puts a + b
end

puts foo(1,2)
");
            },
            @"
3
nil");
        }

        public void Scenario_RubyParams2() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo(a, b, c = a + b, d = c + 1)
  puts a,b,c,d
end

foo(1,2) 
");
            },
            @"
1
2
3
4
");
        }

        public void Scenario_RubyMethodMissing1() {
            AssertOutput(delegate {
                CompilerTest(@"
class C
  def method_missing(name, *a)
    puts name, a
  end
end

C.new.foo 1,2,3
");
            }, @"
foo
1
2
3
");

        }

        public void Scenario_RubyMethodMissing2() {
            AssertExceptionThrown<MissingMethodException>(delegate {
                CompilerTest(@"unknown_method");
            }, delegate(MissingMethodException e) {
                return e.Message.StartsWith("undefined method");
            });
        }

        public void Scenario_RubySingletonConstants1() {
            AssertOutput(delegate {
                CompilerTest(@"
puts nil.to_s
puts true.to_s
puts false.to_s

puts nil | false
puts true & []
puts false ^ []
puts nil.nil?
puts nil.to_i
puts nil.to_f
");
            }, @"

true
false
false
true
true
true
0
0.0
");

        }

        public void Scenario_RubyMath1() {
            AssertOutput(delegate {
                CompilerTest(@"
puts Math.acos(1.0)
puts Math.acos(1)
");
            }, @"
0.0
0.0");

        }

        public void Scenario_RubyScopeParsing() {
            LoggingErrorSink log = new LoggingErrorSink();

            SourceUnitTree p;
            SourceUnit unit;

            unit = Context.CreateSnippet(@"
                class c << G
                end
            ", SourceCodeKind.File);

            p = new Parser().Parse(unit, new RubyCompilerOptions(), log);
            Assert(p == null && log.FatalErrorCount == 1);
            log.ClearCounters();

            unit = Context.CreateSnippet(@"
                def goo(&b)
                    end

                    class C
                        def foo()
                           x.goo() { 
                                goo() {
                                    class << M
                                        def bar()
                                            goo() {
                                            }
                                        end
                                    end
                                }
                           } 
                        end
                    end

                BEGIN { goo1() { } }
                END { goo2() { } } 
            ", SourceCodeKind.File);

            p = new Parser().Parse(unit, new RubyCompilerOptions(), ErrorSink.Null);
            Assert(p != null && !log.AnyError);
            log.ClearCounters();

            unit = Context.CreateSnippet(@"
                for x in array do
                    goo()
                end
            ", SourceCodeKind.File);

            p = new Parser().Parse(unit, new RubyCompilerOptions(), ErrorSink.Null);
            Assert(p != null && !log.AnyError);
            log.ClearCounters();
        }

        /// <summary>
        /// Tests that class lexical scopes are applied to method definitions.
        /// </summary>
        public void Scenario_RubyScopes1() {
            AssertOutput(delegate() {
                CompilerTest(@"
class A; def foo; print 'A'; end; end
class B; def foo; print 'B'; end; end
A.new.foo
B.new.foo
");
            }, @"AB");
        }

        /// <summary>
        /// Uninitialized local variables.
        /// </summary>
        public void Scenario_RubyScopes2A() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo
  x = 1 if false
  p x
end

foo
");
            }, @"nil");
        }

        /// <summary>
        /// Uninitialized local variables in closure.
        /// </summary>
        public void Scenario_RubyScopes2B() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo
  x = 1 if false
  1.times { p x }
end

foo
");
            }, @"nil");
        }

        /// <summary>
        /// Tests that variables defined in module/class locals scope are not visible outside.
        /// </summary>
        public void Scenario_RubyScopes3() {
            AssertOutput(delegate() {
                CompilerTest(@"
y = 'var'
class C
    x = 'var'
end

def x; 'method'; end
def y; 'method'; end

puts x
puts y
");
            }, @"
method
var");
        }

        public void Scenario_RubyScopes4() {
            AssertOutput(delegate() {
                CompilerTest(@"
module Bar
    class C
      module M
        puts self
      end
    end
end
");
            }, @"
Bar::C::M
");
        }

        public void Scenario_RubyScopes5() {
            AssertOutput(delegate() {
                CompilerTest(@"
module M
  module Z
    E = self
  end
end

include M::Z

puts E
");
            }, @"
M::Z
");
        }

        /// <summary>
        /// Nested module scopes - they don't have DLR tuples and therefore need to be skipped when looking up a storage from closure.
        /// </summary>
        public void Scenario_RubyScopes6() {
            CompilerTest(@"
1.times {
  module M
    module N
      z = 1
      1.times {
        x = z
      }
    end
  end
}
");
        }

        public void NumericLiterals1() {
            AssertOutput(delegate() {
                CompilerTest(@"
puts(1)
puts(-1)
puts(+1)
puts(1.1)
puts(-1.1)
puts(+1.1)

x = 2.0
puts x
puts 2.0
puts 2.1560
");
            }, @"
1
-1
1
1.1
-1.1
1.1
2.0
2.0
2.156
");
        }

        public void NumericOps1() {
            // overflow tests:
            Assert((BigInteger)ClrInteger.Minus(Int32.MinValue) == -(BigInteger)Int32.MinValue);
            Assert((BigInteger)ClrInteger.Abs(Int32.MinValue) == -(BigInteger)Int32.MinValue);

            Assert((BigInteger)ClrInteger.Divide(Int32.MinValue, -1) == -(BigInteger)Int32.MinValue);
            Assert(ClrInteger.Modulo(Int32.MinValue, -1) == 0);

            var dm = ClrInteger.DivMod(Int32.MinValue, -1);
            Assert((BigInteger)dm[0] == -(BigInteger)Int32.MinValue);
            Assert((int)dm[1] == 0);

            Assert((int)ClrInteger.LeftShift(1, Int32.MinValue) == 0);
            AssertExceptionThrown<ArgumentOutOfRangeException>(() => ClrInteger.RightShift(1, Int32.MinValue));
        }

        public void Scenario_RubyInclusions1() {
            AssertOutput(delegate() {
                CompilerTest(@"
module M
  def foo
    puts 'foo'
  end
end

class C
  include M
end

C.new.foo
");
            }, "foo");
        }

        public void Scenario_RubyClassVersions1() {
            AssertOutput(delegate() {
                CompilerTest(@"
class C
  def m
    puts 'v1'
  end
end

C.new.m

class C
  def m
    puts 'v2'
  end
end

C.new.m
");
            },
@"
v1
v2
");
        }

        public void Scenario_RubyClassVersions2() {
            AssertOutput(delegate() {
                CompilerTest(@"
module M
  def m; puts 'v1'; end
end

module N
  def m; puts 'v2'; end
end

class C
  include M
end

class D < C 
end

D.new.m

class D
  include N
end

D.new.m
");
            },
@"
v1
v2
");
        }

        public void InvokeMemberCache1() {
            AssertOutput(delegate() {
                CompilerTest(@"
def try_call
  B.m rescue puts 'error' 
end

class B; end

try_call       # m not defined

class Class
  def m; puts 'ok' end
end

try_call       # m defined on Class
");
            },
@"
error
ok
");
        }

        public void Scenario_RubyBlockExpressions1() {
            AssertOutput(delegate() {
                CompilerTest(@"
puts class Foo
  'A'
end

puts def bar
  'B'
end

puts module M
  'C'
end

puts begin
  'D'
end

x = ('E'; 'F')
puts x
");
            }, @"
A
nil
C
D
F");
        }

        public void Scenario_ClassVariables1() {
            AssertOutput(delegate() {
                CompilerTest(@"
module M
  @@m = 1
  
  def goo
    @@n = 2
  end
end

class C
  include M

  def foo
    @@a = 1
  end
  
  @@b = 2
  @@c = 1
end

class D < C
  def bar
    @@c = 3
  end
  
  @@d = 4
end

C.new.foo
D.new.bar
C.new.goo

p C.class_variables.sort
p D.class_variables.sort
p M.class_variables.sort");
            }, @"
[""@@a"", ""@@b"", ""@@c"", ""@@m"", ""@@n""]
[""@@a"", ""@@b"", ""@@c"", ""@@d"", ""@@m"", ""@@n""]
[""@@m"", ""@@n""]
");
        }

        public void Scenario_ClassVariables2() {
            AssertOutput(delegate() {
                CompilerTest(@"
class C
  @@x = 1
end

class D < C
  remove_class_variable :@@x rescue puts 'Error'
  @@y = 'foo'
  puts remove_class_variable(:@@y)
end
");
            }, @"
Error
foo
");
        }

        public void Scenario_RubyReturnValues1() {
            AssertOutput(delegate() {
                CompilerTest(@"
x = while true do
  break 1,2,3
end
puts x
");
            }, @"
1
2
3
");
        }

        public void Scenario_RubyReturnValues2() {
            AssertOutput(delegate() {
                CompilerTest(@"
x = while true do
  break 1 => 2, 3 => 4
end
puts x
");
            }, @"1234");
        }

        public void Scenario_RubyReturnValues3() {
            AssertOutput(delegate() {
                CompilerTest(@"
x = while true do
  break 'a', 'b', 1 => 2, 3 => 4
end
puts x
");
            }, @"
a
b
1234");
        }

        public void Scenario_RubyReturnValues4() {
            AssertOutput(delegate() {
                CompilerTest(@"
x = while true do
  break 'a', 'b', 1 => 2, 3 => 4, *['A', 'B']
end
puts x
");
            }, @"
a
b
1234
A
B");
        }

        public void Scenario_RubyReturnValues5() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo
  return 1,2,3, 4 => 5, *[6,7]
end
puts foo
");
            }, @"
1
2
3
45
6
7
");
        }

        public void Scenario_RubyReturnValues6() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo
  return *$x = [1,2]
end
$y = foo

puts $x.object_id == $y.object_id
puts $x.inspect
");
            }, @"
true
[1, 2]");
        }

        public void Scenario_RubyClosures1() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo
    y = 10
    3.times { |x| puts x + y }
end

foo
");
            }, @"
10
11
12");
        }

        public void Scenario_RubyReturn1() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo()
  return 'foo'
end

puts foo
");
            },
            "foo");
        }
        
        public void ClassVariables1() {
            AssertOutput(delegate() {
                CompilerTest(@"
module M
  @@m = 3
  def moo
    puts @@m
    puts @@a rescue puts '!a'
  end
end

class A
  @@a = 1

  include M  

  class B
    @@b = 2
    def foo
      puts @@b
    end
  end  

  def foo
    puts @@a
  end
end

x = A.new
y = A::B.new
x.foo
x.moo
y.foo
");
            }, @"
1
3
!a
2
");
        }

        public void Scenario_RubyThreads1() { 
            AssertOutput(delegate() {
                CompilerTest(@"
t = Thread.new 1,2,3 do |a,b,c| 
    puts a,b,c 
end
t.join
puts 4
");
            }, @"
1
2
3
4
");
        }

        public void Scenario_YieldCodeGen() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo
    puts 1, yield
end
foo { 2 }
");
            }, @"
1
2
");
        }

        public void Scenario_MainSingleton() { 
            AssertOutput(delegate() {
                CompilerTest(@"
require 'mscorlib'
include System::Collections
puts ArrayList
");
            }, @"System::Collections::ArrayList");
        }

        public void Scenario_ModuleOps_Methods() {
            AssertOutput(delegate() {
                CompilerTest(@"
class C
  def ifoo
    puts 'ifoo'
  end
end

class << C
  $C1 = self
  
  def foo
    puts 'foo'
  end
end

class C
  alias_method(:bar,:foo) rescue puts 'Error 1'
  instance_method(:foo) rescue puts 'Error 2'
  puts method_defined?(:foo)
  foo
  
  alias_method(:ibar,:ifoo)
  instance_method(:ifoo)
  puts method_defined?(:ifoo)
  ifoo rescue puts 'Error 3'
  
  remove_method(:ifoo)
end

C.new.ifoo rescue puts 'Error 4'
C.new.ibar
");
            }, @"
Error 1
Error 2
false
foo
true
Error 3
Error 4
ifoo
");
        }

        public void Methods1() {
            AssertOutput(delegate() {
                CompilerTest(@"
class C
  def foo a,b
    puts a + b
  end
end

class D < C
end

c = C.new
p m = c.method(:foo)
p u = m.unbind
p n = u.bind(D.new)

m[1,2]
n[1,2]
");
            }, @"
#<Method: C#foo>
#<UnboundMethod: C#foo>
#<Method: D(C)#foo>
3
3
");
        }
    }
}

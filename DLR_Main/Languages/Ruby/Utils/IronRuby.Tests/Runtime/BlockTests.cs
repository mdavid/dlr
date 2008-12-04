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

namespace IronRuby.Tests {

    public partial class Tests {
        public void BlockEmpty() {
            CompilerTest("1.times { }");
        }

        public void Scenario_RubyBlocks0() {
            AssertOutput(delegate() {
                CompilerTest(@"
3.times { |x| print x }
");
            }, "012");
        }

        public void Scenario_RubyBlocks_Params1() {
            AssertOutput(delegate() {
                CompilerTest(@"
def y; yield 0,1,2,3,4,5,6,7,8,9; end

y { |x0,x1,x2,x3,x4,x5,x6,x7,x8,x9| print x0,x1,x2,x3,x4,x5,x6,x7,x8,x9 }
");
            }, "0123456789");
        }

        public void Scenario_RubyBlocks_Params2() {
            AssertOutput(delegate() {
                CompilerTest(@"
def y; yield 0,1,2,3,4,5,6,7,8,9; end

y { |x0,x1,x2,x3,x4,x5,x6,x7,*z| print x0,x1,x2,x3,x4,x5,x6,x7,z[0],z[1]; }
");
            }, "0123456789");
        }

        public void ProcYieldCaching1() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo 
  yield
end

foo { print 'A' }
foo { print 'B' }
foo { print 'C' }
");
            }, "ABC");
        }

        public void ProcCallCaching1() {
            AssertOutput(delegate() {
                CompilerTest(@"
$p = lambda { puts 1 }
$q = lambda { puts 2 } 

$p.call
$q.call
");
            }, @"
1
2
");
        }

        public void ProcSelf1() {
            AssertOutput(delegate() {
                CompilerTest(@"
module M
  1.times { p self }
end        
");
            }, @"M");
        }

        public void Scenario_RubyBlocks2() {
            AssertExceptionThrown<MissingMethodException>(delegate() {
                CompilerTest(@"
3.times { |x| z = 1 }
puts z # undef z
");
            });
        }

        public void Scenario_RubyBlocks3() {
            AssertOutput(delegate() {
                CompilerTest(@"
class C
  def foo
    puts 'X',yield(1,2,3)
  end
end

C.new.foo { |a,b,c| puts a,b,c; 'foo' }
");
            }, @"
1
2
3
X
foo
");
        }

        public void Scenario_RubyBlocks5() {
            AssertOutput(delegate() {
                CompilerTest(@"
class C
  def foo 
    puts block_given?
  end
end

C.new.foo { puts 'goo' }
C.new.foo
            ");
            }, @"
true
false
            ");
        }

        /// <summary>
        /// Return, yield and retry in a method.
        /// </summary>
        public void Scenario_RubyBlocks6() {
            AssertOutput(delegate() {
                CompilerTest(@"
def do_until(cond)
  if cond then return end
  yield
  retry
end

i = 0
do_until(i > 4) do
  puts i
  i = i + 1
end
");
            }, @"
0
1
2
3
4
");
        }

        /// <summary>
        /// Break in a block.
        /// </summary>
        public void Scenario_RubyBlocks7() {
            AssertOutput(delegate() {
                CompilerTest(@"
x = 4.times { |x|
  puts x
  break 'foo'
}
puts x
");
            }, @"
0
foo
");
        }

        /// <summary>
        /// Redo in a block.
        /// </summary>
        public void Scenario_RubyBlocks8() {
            AssertOutput(delegate() {
                CompilerTest(@"
i = 0
x = 2.times { |x|
  puts x
  i = i + 1
  if i < 3 then redo end
}
puts x
");
            }, @"
0
0
0
1
2
");
        }

        /// <summary>
        /// Next in a block.
        /// </summary>
        public void Scenario_RubyBlocks9() {
            AssertOutput(delegate() {
                CompilerTest(@"
i = 0
x = 5.times { |x|
  puts x
  i = i + 1  
  if i < 3 then next end
  puts 'bar'
}
");
            }, @"
0
1
2
bar
3
bar
4
bar
");
        }

        /// <summary>
        /// Retry in a block.
        /// </summary>
        public void Scenario_RubyBlocks10() {
            AssertOutput(delegate() {
                CompilerTest(@"
i = 0
3.times { |x| 
  puts x
  i = i + 1
  if i == 2 then retry end
}
");
            }, @"
0
1
0
1
2
");
        }

        /// <summary>
        /// Return with stack unwinding.
        /// </summary>
        public void Scenario_RubyBlocks11() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo
    puts 'begin'
    1.times {
        1.times {
            puts 'block'
            return 'result'
        }
    }
    puts 'end'
ensure
    puts 'ensure'
end

puts foo
");
            }, @"
begin
block
ensure
result
");
        }

        public void Scenario_RubyBlocks12() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo
  yield 1,2,3
end

foo { |a,*b| puts a,'-',b }
");
            }, @"
1
-
2
3
");
        }

        public void Scenario_RubyBlocks13() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo
  yield 1,2,3
end

foo { |a,b| puts a,b }
");
            }, @"
1
2");
        }

        /// <summary>
        /// Nested yielding.
        /// </summary>
        public void Scenario_RubyBlocks14() {
            AssertOutput(delegate() {
                CompilerTest(@"
def bar
  yield
  yield  # shouldn't be called
end

def foo
  bar {
    print 'x'
	yield
  } 
end

foo { 
  break 
}");
            }, @"x");
        }

        /// <summary>
        /// Retry for-loop: for-loop should behave like x.each { } method call with a block, that is x is reevaluted on retry.
        /// </summary>
        public void Scenario_RubyBlocks15() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo x
  puts ""foo(#{x})""
  x * ($i + 1)
end

$i = 0

for i in [foo(1), foo(2), foo(3)] do
  puts ""i = #{i}""
  
  if $i == 0 then
    $i = 1
    retry
  end  
end
");
            }, @"
foo(1)
foo(2)
foo(3)
i = 1
foo(1)
foo(2)
foo(3)
i = 2
i = 4
i = 6
");
        }

        /// <summary>
        /// Tests optimization of block break from another block. 
        /// 
        /// Yield yields to a block that breaks to its proc-converter, which is foo.
        /// So the example should retrun 1 from the foo call.
        /// 
        /// Break is propagated thru yields in two ways:
        /// 1) returning ReturnReason == Break via BlockParam (fast path)
        /// 2) throwing MethodUnwinder exception (slow path)
        /// 
        /// ReturnReason should be propagated by yields as long as the owner of the block that contains the yield 
        /// is the target frame for the break. That's the case for for-loop blocks in this test.
        /// </summary>
        public void Scenario_RubyBlocks16() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo
    for j in [0]
        for i in [1]
            yield
        end 
        puts 'Unreachable'
    end
    puts 'Unreachable'
end 

x = foo do
    break 1
end
puts x
");
            }, @"1");
        }

        /// <summary>
        /// Retry is propagated to the 'each' call.
        /// </summary>
        public void Scenario_RubyBlocks17() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo
    for i in [1, 2, 3]
        puts ""i = #{i}""
        x = yield
    end 
    puts x
end 

def bar
    $c = 0
    foo do
		puts $c
        $c += 1
        retry if $c < 3
        'done'
    end 
end 

bar");
            }, @"
i = 1
0
i = 1
1
i = 1
2
i = 2
3
i = 3
4
done");
        }

        public void Scenario_RubyBlockArgs1() {
            AssertOutput(delegate() {
                CompilerTest(@"
def a; yield; end 
def b; yield 1; end 
def c; yield 1,2; end 
def d; yield []; end 
def e; yield [1]; end 
def f; yield [1,2]; end 
def g; yield *[]; end;

a { |x| puts x.inspect }
b { |x| puts x.inspect }
c { |x| puts x.inspect }
d { |x| puts x.inspect }
e { |x| puts x.inspect }
f { |x| puts x.inspect }
g { |(x,)| puts x.inspect }
", 0, 2); // 2 runtime warnings
            }, @"
nil
1
[1, 2]
[]
[1]
[1, 2]
nil
");
        }

        public void Scenario_RubyProcCallArgs1A() {
            AssertOutput(delegate() {
                CompilerTest(@"
lambda { |x| puts x.inspect }.call
lambda { |x| puts x.inspect }.call 1
lambda { |x| puts x.inspect }.call 1,2
lambda { |x| puts x.inspect }.call []
lambda { |x| puts x.inspect }.call [1]
lambda { |x| puts x.inspect }.call [1,2]
lambda { |x| puts x.inspect }.call *[1]
lambda { |(x,)| puts x.inspect }.call
lambda { |(x,)| puts x.inspect }.call 1,2,3,4 
lambda { |(x,y)| puts x.inspect }.call rescue puts 'error'
", 0, 2); // 2 runtime warnings
            }, @"
nil
1
[1, 2]
[]
[1]
[1, 2]
1
nil
1
error
");
        }

        public void Scenario_RubyProcCallArgs1B() {
            AssertOutput(delegate() {
                CompilerTest(@"
Proc.new { |x| puts x.inspect }.call
Proc.new { |x| puts x.inspect }.call 1
Proc.new { |x| puts x.inspect }.call 1,2
Proc.new { |x| puts x.inspect }.call []
Proc.new { |x| puts x.inspect }.call [1]
Proc.new { |x| puts x.inspect }.call [1,2]
Proc.new { |x| puts x.inspect }.call *[1]
Proc.new { |(x,)| puts x.inspect }.call *[]
", 0, 2); // 2 runtime warnings
            }, @"
nil
1
[1, 2]
[]
[1]
[1, 2]
1
nil
");
        }

        public void Scenario_RubyBlockArgs2() {
            AssertOutput(delegate() {
                CompilerTest(@"
def a; yield; end 
def b; yield 1; end 
def c; yield 1,2; end 
def d; yield []; end 
def e; yield [1]; end 
def f; yield [1,2]; end 

a { |x,y| p [x, y] }
b { |x,y| p [x, y] }
c { |x,y| p [x, y] }
d { |x,y| p [x, y] }
e { |x,y| p [x, y] }
f { |x,y| p [x, y] }
");       
            }, @"
[nil, nil]
[1, nil]
[1, 2]
[nil, nil]
[1, nil]
[1, 2]
");
        }

        public void Scenario_RubyProcCallArgs2A() {
            AssertOutput(delegate() {
                CompilerTest(@"
lambda { |x,y| p [x, y] }.call rescue puts 'error'
lambda { |x,y| p [x, y] }.call 1 rescue puts 'error'
lambda { |x,y| p [x, y] }.call 1,2
lambda { |x,y| p [x, y] }.call [] rescue puts 'error'
lambda { |x,y| p [x, y] }.call [1] rescue puts 'error'
lambda { |x,y| p [x, y] }.call [1,2] rescue puts 'error'
lambda { |x,y| p [x, y] }.call *[1,2] 
lambda { |x,y| p [x, y] }.call *[[1]] rescue puts 'error'
lambda { |x,y| p [x, y] }.call *[[1,2]] rescue puts 'error'
lambda { |x,y| p [x, y] }.call *[[1,2,3]] rescue puts 'error'
");
            }, @"
error
error
[1, 2]
error
error
error
[1, 2]
error
error
error
");
        }

        public void Scenario_RubyProcCallArgs2B() {
            AssertOutput(delegate() {
                CompilerTest(@"
Proc.new { |x,y| p [x, y] }.call 
Proc.new { |x,y| p [x, y] }.call 1
Proc.new { |x,y| p [x, y] }.call 1,2
Proc.new { |x,y| p [x, y] }.call []
Proc.new { |x,y| p [x, y] }.call [1]
Proc.new { |x,y| p [x, y] }.call [1,2]
Proc.new { |x,y| p [x, y] }.call *[1,2] 
Proc.new { |x,y| p [x, y] }.call *[[1]]
Proc.new { |x,y| p [x, y] }.call *[[1,2]]
Proc.new { |x,y| p [x, y] }.call *[[1,2,3]]
");
            }, @"
[nil, nil]
[1, nil]
[1, 2]
[nil, nil]
[1, nil]
[1, 2]
[1, 2]
[1, nil]
[1, 2]
[1, 2]
");
        }

        public void Scenario_RubyProcCallArgs2C() {
            AssertOutput(delegate() {
                CompilerTest(@"
Proc.new { || p [] }.call 
Proc.new { |x| p [x] }.call 1
Proc.new { |x,y| p [x,y] }.call 1,2
Proc.new { |x,y,z| p [x,y,z] }.call 1,2,3
Proc.new { |x,y,z,w| p [x,y,z,w] }.call 1,2,3,4
Proc.new { |x,y,z,w,u| p [x,y,z,w,u] }.call 1,2,3,4,5
Proc.new { |x,y,z,w,u,v| p [x,y,z,w,u,v] }.call 1,2,3,4,5,6
");
            }, @"
[]
[1]
[1, 2]
[1, 2, 3]
[1, 2, 3, 4]
[1, 2, 3, 4, 5]
[1, 2, 3, 4, 5, 6]
");
        }

        /// <summary>
        /// Tests MRI inconsistency in Yield1 vs YieldNoSplat1 when invoked from Call1.
        /// </summary>
        public void Scenario_RubyProcCallArgs2D() {
            AssertOutput(delegate() {
                CompilerTest(@"
f = proc{|x,| x}
p f.call(1)
p f.call([1])
p f.call([[1]])
p f.call([1,2])
");
            }, @"
1
[1]
[[1]]
[1, 2]
");
        }


        public void Scenario_RubyProcYieldArgs1() {
            AssertOutput(delegate() {
                CompilerTest(@"
def y *a
  yield *a
end

y() { || p [] }
y(1) { |x| p [x] }
y(1,2) { |x,y| p [x,y] }
y(1,2,3) { |x,y,z| p [x,y,z] }
y(1,2,3,4) { |x,y,z,w| p [x,y,z,w] }
y(1,2,3,4,5) { |x,y,z,w,u| p [x,y,z,w,u] }
y(1,2,3,4,5,6) { |x,y,z,w,u,v| p [x,y,z,w,u,v] }
puts '---'
y(1,2,3,4,5,6) { || p [] }
y(1,2,3,4,5,6) { |x| p [x] }
y(1,2,3,4,5,6) { |x,y| p [x,y] }
y(1,2,3,4,5,6) { |x,y,z| p [x,y,z] }
y(1,2,3,4,5,6) { |x,y,z,w| p [x,y,z,w] }
y(1,2,3,4,5,6) { |x,y,z,w,u| p [x,y,z,w,u] }
y(1,2,3,4,5,6) { |x,y,z,w,u,v| p [x,y,z,w,u,v] }
puts '---'
y(1,2,3) { || p [] }
y(1,2,3) { |x| p [x] }
y(1,2,3) { |x,y| p [x,y] }
y(1,2,3) { |x,y,z| p [x,y,z] }
y(1,2,3) { |x,y,z,w| p [x,y,z,w] }
y(1,2,3) { |x,y,z,w,u| p [x,y,z,w,u] }
y(1,2,3) { |x,y,z,w,u,v| p [x,y,z,w,u,v] }
", 0, 2);
            }, @"
[]
[1]
[1, 2]
[1, 2, 3]
[1, 2, 3, 4]
[1, 2, 3, 4, 5]
[1, 2, 3, 4, 5, 6]
---
[]
[[1, 2, 3, 4, 5, 6]]
[1, 2]
[1, 2, 3]
[1, 2, 3, 4]
[1, 2, 3, 4, 5]
[1, 2, 3, 4, 5, 6]
---
[]
[[1, 2, 3]]
[1, 2]
[1, 2, 3]
[1, 2, 3, nil]
[1, 2, 3, nil, nil]
[1, 2, 3, nil, nil, nil]
");
        }

        /// <summary>
        /// RHS is list, LHS is not simple, but contains splatting.
        /// </summary>
        public void Scenario_RubyBlockArgs3() {
            AssertOutput(delegate() {
                CompilerTest(@"
def baz
   yield [1,2,3]
end
baz { |*a| puts a.inspect }
");
            }, @"[[1, 2, 3]]");
        }

        /// <summary>
        /// !L(1,-) && R(0,*), empty array to splat.
        /// </summary>
        public void Scenario_RubyBlockArgs4A() {
            AssertOutput(delegate() {
                CompilerTest(@"
def y
   yield *[]
end

y { |*a| puts a.inspect }
");
            }, @"[]");
        }

        /// <summary>
        /// Anonymous unsplat parameters.
        /// </summary>
        public void Scenario_RubyBlockArgs4B() {
            AssertOutput(delegate() {
                CompilerTest(@"
def y
  a = [1,2,3,4,5]
  yield a,[6]
end

y { |(x,y,*),*| p x,y }
puts '-'
y { |(x,y,*a),*| p x,y,a }
puts '-'
y { |(x,y,*),*a| p x,y,a }
");
            }, @"
1
2
-
1
2
[3, 4, 5]
-
1
2
[[6]]
");
        }

        

        /// <summary>
        /// L(M,*) := R(N,*,=) where M is less then N.
        /// </summary>
        public void Scenario_RubyBlockArgs5() {
            AssertOutput(delegate() {
                CompilerTest(@"
class C
  define_method('[]=') { |a,b,c,*p|
    print a,b,c,'|',*p
  }
end

c = C.new
c[1,2,3,4,5,6,7,*[8]] = 9
");
            }, @"123|456789");
        }

        /// <summary>
        /// L(M,*) := R(N,*,=) where M is greater then N.
        /// </summary>
        public void Scenario_RubyBlockArgs6() {
            AssertOutput(delegate() {
                CompilerTest(@"
class C
  define_method('[]=') { |a,b,c,*p|
    print a,b,c,'|',*p
  }
end

c = C.new
c[1,*[2]] = 3
");
            }, @"123|");
        }

        /// <summary>
        /// Wrong number of arguments.
        /// </summary>
        public void Scenario_RubyBlockArgs7() {
            AssertExceptionThrown<ArgumentException>(delegate() {
                CompilerTest(@"
class C
  define_method('[]=') { |a,b| }
end

c = C.new
c[1,2,*[]] = 3
");
            });
        }

        /// <summary>
        /// L(1, -) := R(0,*0,=)
        /// </summary>
        public void Scenario_RubyBlockArgs8() {
            AssertOutput(delegate() {
                CompilerTest(@"
class C
  define_method('[]=') { |a|
    p a
  }
end

c = C.new
c[*[]] = 1
");
            }, @"1");
        }

        /// <summary>
        /// L(1, -) := R(N,*,=)
        /// </summary>
        public void Scenario_RubyBlockArgs9() {
            AssertOutput(delegate() {
                CompilerTest(@"
class C
  define_method('[]=') { |a|
    p a
  }
end

c = C.new
c[1,*[2]] = 3
", 0, 1);
            }, @"[1, 2, 3]");
        }

        /// <summary>
        /// L(2..5, -) := R(N,*,=)
        /// </summary>
        public void Scenario_RubyBlockArgs10() {
            // L(2, -) := R(N,*,=)
            AssertOutput(delegate() {
                CompilerTest(@"
class C
  define_method('[]=') { |a,b|
    print a,',',b
  }
end

c = C.new
c[1,*[]] = 2
");
            }, @"1,2");

            // L(3, -) := R(N,*,=)
            AssertOutput(delegate() {
                CompilerTest(@"
class C
  define_method('[]=') { |a,b,c|
    print a,',',b,',',c
  }
end

c = C.new
c[1,2,*[]] = 3
");
            }, @"1,2,3");

            // L(4, -) := R(N,*,=)
            AssertOutput(delegate() {
                CompilerTest(@"
class C
  define_method('[]=') { |a,b,c,d|
    print a,',',b,',',c,',',d
  }
end

c = C.new
c[1,*[2,3]] = 4
");
            }, @"1,2,3,4");

            // L(5, -) := R(N,*,=)
            AssertOutput(delegate() {
                CompilerTest(@"
class C
  define_method('[]=') { |a,b,c,d,e|
    print a,',',b,',',c,',',d,',',e
  }
end

c = C.new
c[1,2,*[3,4]] = 5
");
            }, @"1,2,3,4,5");
        }
        
        public void Scenario_RubyProcs1() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo x,y,&f
  yield x,y
  f[3,4]
end

foo(1,2) do |a,b|
  puts a,b
end
");
            }, @"
1
2
3
4
");
        }

        public void RubyProcArgConversion1() {
            AssertOutput(delegate() {
                CompilerTest(@"
class C
  def to_proc
    lambda { |x| puts x }
  end
end

class D
  def to_proc
    lambda { |x| puts x + 1 }
  end
end

class E  
end

1.times(&C.new)
1.times(&D.new)
1.times(&E.new) rescue puts 'error'
");
            }, @"
0
1
error
");
        }

        public void RubyProcArgConversion2() {
            AssertOutput(delegate() {
                CompilerTest(@"
class C
  def to_proc; 1; end
end

class D
  def to_proc; lambda { puts 'ok2' }; end
end

1.times(&lambda { puts 'ok1' })
1.times(&C.new) rescue puts $!
1.times(&D.new)
");
            }, @"
ok1
C#to_proc should return Proc
ok2
");
        }

        public void RubyProcArgConversion3() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo &b
  p b
end

foo(&nil)
");
            }, @"nil");
        }

        public void RubyProcArgConversion4() {
            AssertOutput(delegate() {
                CompilerTest(@"
class C
  def respond_to? name
    puts name
    $has_to_proc
  end

  def to_proc
    lambda { puts 'ok' }
  end
end

c = C.new

$has_to_proc = false
1.times(&c) rescue puts 'error'

$has_to_proc = true
1.times(&c)
");
            }, @"
to_proc
error
to_proc
ok
");
        }

        public void ProcNew1() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo
  1.times { |x| $x = Proc.new }
end

y = lambda { puts 'foo' }
foo(&y)
p $x.object_id == y.object_id
");
            }, @"true");
        }

        public void ProcNew2() {
            AssertOutput(delegate() {
                CompilerTest(@"
class P < Proc
end

def foo
  1.times { |x| $x = P.new }
end

y = lambda { puts 'foo' }
foo(&y)
p $x.object_id == y.object_id
p $x.class
");
            }, @"
false
P
");
        }

        public void DefineMethod1() {
            AssertOutput(delegate() {
                CompilerTest(@"
class C
  def foo
    $x = lambda {
      puts self.class
    }
    
    $x.call
  end
end

C.new.foo

class D
  define_method :goo, &$x
end

D.new.goo
");
            }, @"
C
D");
        }

        /// <summary>
        /// define_method and class_eval change owner of the method definition.
        /// </summary>
        public void DefineMethod2() {
            AssertOutput(delegate() {
                CompilerTest(@"
module M
  $p = lambda {    
    def goo
      def bar
        puts 'bar'
      end
    end  
  }      
end

class C
  define_method :foo,&$p
end

class D
 class_eval(&$p)
end

c = C.new
d = D.new

d.goo
c.foo
c.goo

p M.instance_methods(false).sort
p C.instance_methods(false).sort
p D.instance_methods(false).sort
");
            }, @"
[""bar""]
[""foo"", ""goo""]
[""goo""]
");

        }

        public void BlockArity1() {
            AssertOutput(delegate() {
                CompilerTest(@"
puts '== -4'

p Proc.new{|(a,b,c,*)|}.arity

puts '== -2'

p Proc.new{|(a,b,c,*),*|}.arity

puts '== -1'

p Proc.new{|(*)|}.arity
p Proc.new{}.arity
p Proc.new{|*|}.arity

puts '== 0'

p Proc.new{||}.arity

puts '== 1'

p Proc.new{|x|}.arity    
p Proc.new{|x,|}.arity    
p Proc.new{|(x,)|}.arity 
p Proc.new{|(x,),|}.arity
p Proc.new{|((x,))|}.arity
p Proc.new{|((x,y))|}.arity
p Proc.new{|((x,y,))|}.arity
p Proc.new{|(x,y,),|}.arity
p Proc.new{|(*),|}.arity
p Proc.new{|(x,*),|}.arity

puts '== 2'

p Proc.new{|x,y|}.arity  
p Proc.new{|x,y,|}.arity 
p Proc.new{|x,y,|}.arity 
p Proc.new{|(x,y)|}.arity
p Proc.new{|(x,y,)|}.arity

puts '== 3'

p Proc.new{|x,y,z|}.arity
p Proc.new{|x,y,z,|}.arity
p Proc.new{|(x,y,z)|}.arity
p Proc.new{|(x,y,z,)|}.arity
");
            }, @"
== -4
-4
== -2
-2
== -1
-1
-1
-1
== 0
0
== 1
1
1
1
1
1
1
1
1
1
1
== 2
2
2
2
2
2
== 3
3
3
3
3
");
        }
        
        public void Proc_RhsAndBlockArguments1() {
            AssertOutput(() => CompilerTest(@"
class Proc
  alias []= []
end

x = Proc.new { |*a| p a, block_given? }

x.call(1,2,&lambda {})
x[1,2] = 3

"), @"
[1, 2]
false
[1, 2, 3]
false
");
        }
    }
}

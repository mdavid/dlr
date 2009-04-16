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
        public void Scenario_RubySimpleCall1() {
            AssertOutput(delegate {
                CompilerTest(@"
puts nil
");
            }, "nil");

        }

        public void Scenario_RubySimpleCall2() {
            AssertExceptionThrown<ArgumentException>(delegate {
                CompilerTest(@"
def foo a,c=1,*e
end

foo

");
            });

        }

        public void Scenario_RubySimpleCall3() {
            AssertOutput(delegate {
                CompilerTest(@"
y = nil
puts y

x = 123
puts x
");
            }, @"
nil
123");

        }

        /// <summary>
        /// LambdaExpression gets converted to a wrapper.
        /// </summary>
        public void Scenario_RubySimpleCall4() {
            AssertOutput(delegate {
                CompilerTest(@"
def foo a,b,c,d,e,f,g,h,i,j
  puts 123
end
foo 1,2,3,4,5,6,7,8,9,10
");
            }, @"123");

        }

        public void Scenario_RubySimpleCall5() {
            AssertOutput(delegate {
                Engine.CreateScriptSourceFromString(@"
class A
end

class B < A
end

B.new.foo rescue 0

class A
  def foo
    puts 'foo'
  end
end

B.new.foo
").ExecuteProgram();
            }, @"foo");
        }
        
        public void MethodCallCaching1() {
            AssertOutput(() => CompilerTest(@"
module N
  def foo
    print 1
  end
end

module M
end

class A
  include N
  include M
end

A.new.foo

module M
  def foo
    print 2
  end
end

A.new.foo
"), 
"12");
        }

        public void MethodCallCaching2() {
            AssertOutput(() => CompilerTest(@"
module M
end

class C
  include M
end

module N
  def foo
    puts 'foo'
  end
end

module M
  include N
end

C.new.foo rescue puts 'error'

class C
  include M
end

C.new.foo
"),
@"
error
foo
");
        }

        /// <summary>
        /// A method defined in a module is overridden by another module's method.
        /// </summary>
        public void MethodCallCaching3() {
            AssertOutput(() => CompilerTest(@"
module N0; def f; 0; end; end
module N1; def f; 1; end; end
module N2; def f; 2; end; end

class C; end
class D < C; include N2; end

print D.new.f                                   # cache N2::f in a dynamic site

class C
  include N0, N1, N2                            # def in N1 should invalidate site bound to def in N2
                                                # def in N0 shouldn't prevent invalidation
end

print C.new.f
"),
@"20");
        }

        /// <summary>
        /// method_missing
        /// </summary>
        public void MethodCallCaching4() {
            LoadTestLibrary();
            
            AssertOutput(() => CompilerTest(@"
class A
  def method_missing name; name; end
end
class B < A
end
class C < B
end

puts C.new.h   
v1 = TestHelpers.get_class_version(C)   

class B
  def g; 'g:B'; end
end

v2 = TestHelpers.get_class_version(C)   

puts C.new.g

class B
  def h; 'h:B'; end
end

v3 = TestHelpers.get_class_version(C)   

puts C.new.h   
puts v1 == v2, v2 == v3

class B
  remove_method(:h)
  remove_method(:g)
end

puts C.new.g
puts C.new.h
"),
@"
h
g:B
h:B
true
false
g
h
");
        }

        /// <summary>
        /// method_missing
        /// </summary>
        public void MethodCallCaching5() {
            AssertOutput(() => CompilerTest(@"
class A
  def method_missing name; name.to_s + ':A'; end
end
class B < A
end
class C < B
end

puts C.new.f

class B
  def method_missing name; name.to_s + ':B'; end 
end

puts C.new.f

class B
  remove_method :method_missing
end

puts C.new.f

class A
  remove_method :method_missing
end

C.new.f rescue puts 'error'
"),
@"
f:A
f:B
f:A
error
");
        }

        /// <summary>
        /// method_missing
        /// </summary>
        public void MethodCallCaching6() {
            AssertOutput(() => CompilerTest(@"
class A
  def f; 'f:A' end
end
class B < A
end
class C < B
end

puts C.new.f

class B
  def method_missing name; name.to_s + ':B'; end 
end

puts C.new.f

class A
  remove_method :f
end

puts C.new.f
"),
@"
f:A
f:A
f:B
");
        }

        /// <summary>
        /// Checks that if the same site is used twice and the first use failes on parameter conversion the second use is not affected.
        /// </summary>
        public void MethodCallCaching7() {
            AssertOutput(delegate {
                CompilerTest(@"
'hello'.send(:slice, nil) rescue puts 'error'
puts 'hello'.send(:slice, 1)
");
            }, @"
error
101
");

        }

        public void Send1() {
            AssertOutput(delegate {
                CompilerTest(@"
class C
  def foo *a
    puts ""C::foo *#{a.inspect}, &#{block_given?}""
  end
  
  alias []= :send
end

x = C.new
q = lambda {}

x.send :foo
x.send :foo, &q
x.send :foo, &nil
x.send :foo, 1
x.send :foo, 1, &q
x.send :foo, 1, &nil
x.send :foo, 1, 2
x.send :foo, 1, 2, &q
x.send :foo, 1, 2, &nil
x.send :foo, 1, 2, 3
x.send :foo, 1, 2, 3, &q
x.send :foo, 1, 2, 3, &nil

x.send *[:foo, 1,2,3]
x.send :foo, 1, *[2,3], &q
x[:foo,*[1,2]] = 3
x[*:foo] = 1
x[] = :foo
", 1, 0);
            }, @"
C::foo *[], &false
C::foo *[], &true
C::foo *[], &false
C::foo *[1], &false
C::foo *[1], &true
C::foo *[1], &false
C::foo *[1, 2], &false
C::foo *[1, 2], &true
C::foo *[1, 2], &false
C::foo *[1, 2, 3], &false
C::foo *[1, 2, 3], &true
C::foo *[1, 2, 3], &false
C::foo *[1, 2, 3], &false
C::foo *[1, 2, 3], &true
C::foo *[1, 2, 3], &false
C::foo *[1], &false
C::foo *[], &false
");
        }

        /// <summary>
        /// Send propagates the current scope.
        /// </summary>
        public void Send2() {
             AssertOutput(delegate {
                CompilerTest(@"
class C
  public
  send :private
  
  def foo
  end   
  
  p C.private_instance_methods(false)
end
");
             }, @"
[""foo""]
");
        }

        public void AttributeAccessors1() {
            AssertOutput(delegate {
                CompilerTest(@"
class C
  attr_accessor :foo
  alias :bar :foo
  alias :bar= :foo=
end

x = C.new
x.foo = 123
x.bar = x.foo + 1
puts x.foo, x.bar
");
            }, @"
124
124
");
        }

        public void AttributeAccessors2() {
            AssertOutput(() => CompilerTest(@"
class C
  attr_accessor :foo
end

x = C.new
p x.send('foo=', 123)
p x.send('foo')

"), @"
123
123
");
        }

        public void AttributeAccessors3() {
            AssertOutput(() => CompilerTest(@"
class C
  attr_accessor :foo 
  
  alias set_foo foo=
end

c = C.new

c.foo = 1
c.set_foo(*[2])
c.set_foo(3,4) rescue p $!
p c.foo(*[])
p c.foo(1) rescue p $!
"), @"
#<ArgumentError: wrong number of arguments (2 for 1)>
2
#<ArgumentError: wrong number of arguments (1 for 0)>
");
        }

        public void MethodAdded1() {
            AssertOutput(delegate {
                CompilerTest(@"
class Module
  def method_added name    
    puts name
  end
end
");
            }, @"
method_added
");
        }

        public void VisibilityCaching1() {
            AssertOutput(delegate {
                CompilerTest(@"
class C
  def foo
    puts 'foo'
  end

  def method_missing name
    puts 'mm'
  end 
end

x = C.new
4.times do |$i|
  class C
    case $i
      when 0: private :foo
      when 1: public :foo
      when 2: private :foo
    end
  end
  x.foo  
end
");
            }, @"
mm
foo
mm
mm
");
        }

        public void VisibilityCaching2() {
            AssertOutput(() => CompilerTest(@"
class B
  def method_missing name; name; end
end

class C < B
  private
  def foo; 'foo:C'; end  
end

class D < C
end

puts D.new.foo

class D
  def foo; 'foo:D'; end  
end

class B
  remove_method :method_missing
end

puts D.new.foo
D.new.bar rescue puts 'error'
"), @"
foo
foo:D
error
");
        }

        public void Visibility1() {
            AssertOutput(() => CompilerTest(@"
class A
  def foo
  end
end

module M
  def bar
  end
end

class A
  include M
  
  alias_method :foo, :bar
  
  p instance_method(:foo)           # DeclaringModule of foo should be M
  
  private :foo                      # should make a copy of foo that rewrites the current foo, not define a super-forwarder
  
  alias_method :xxx, :foo           # should find foo
end
"), @"
#<UnboundMethod: A(M)#foo>
");
        }

        /// <summary>
        /// public/private/protected define a super-forwarder - a method that calls super.
        /// </summary>
        public void Visibility2() {
            AssertOutput(() => CompilerTest(@"
class A
  private
  def foo
    puts 'A::foo'
  end
end

class B < A
end

class C < B
  public :foo      # declaring module of D#foo is A 
  
  p instance_method(:foo) 
end

class A
  remove_method(:foo)
end

class B
  private
  def foo
    puts 'B::foo'
  end
end

C.new.foo
"), @"
#<UnboundMethod: C(A)#foo>
B::foo
");
        }

        /// <summary>
        /// Protected visibility and singletons.
        /// </summary>
        public void Visibility3() {
            AssertOutput(() => CompilerTest(@"
c = class C; new; end

class << c
  protected
  def foo; end
end

c.foo rescue p $!
"), @"
#<NoMethodError: protected method `foo' called for #<C:*>>
", OutputFlags.Match);
        }

        /// <summary>
        /// Protected visibility + caching.
        /// </summary>
        public void Visibility4() {
            AssertOutput(() => CompilerTest(@"
class C
  protected
  def foo
    puts 'foo'
  end
end

class D < C
  def method_missing name
    puts 'mm'
  end
end

class X; end

c,d,x = C.new,D.new,X.new

# test visibility caching:
2.times do
  [[d,c], [c,d], [x,c], [x,d]].each do |s,r| 
    s.instance_eval { r.foo } rescue p $! 
  end
end
"), @"
foo
foo
#<NoMethodError: protected method `foo' called for #<C:*>>
mm
foo
foo
#<NoMethodError: protected method `foo' called for #<C:*>>
mm
", OutputFlags.Match);
        }

        public void ModuleFunctionVisibility1() {
            AssertOutput(delegate {
                CompilerTest(@"
module M
  private
  def f
  end
  
  module_function :f
end

p M.singleton_methods(false)
p M.private_instance_methods(false)
p M.public_instance_methods(false)
");
            }, @"
[""f""]
[""f""]
[]
");            
        }

        /// <summary>
        /// module_function/private/protected/public doesn't copy a method that is already private/private/protected/public.
        /// </summary>
        public void ModuleFunctionVisibility2() {
            AssertOutput(delegate {
                CompilerTest(@"
module A
  private
  def pri; end
  protected
  def pro; end
  public
  def pub; end
end

module B
  include A
  module_function :pri
  private :pri
  protected :pro
  public :pub
  
  p private_instance_methods(false)
  p protected_instance_methods(false)
  p public_instance_methods(false)
  p singleton_methods(false)
end
");
            }, @"
[]
[]
[]
[""pri""]
");
        }

        /// <summary>
        /// define_method copies given method and sets its visibility according to the the current scope flags.
        /// </summary>
        public void DefineMethodVisibility1() {
            AssertOutput(() => CompilerTest(@"
class A
  def foo
    puts 'foo'
  end
end

class B < A
  private
  define_method(:foo, instance_method(:foo))
end

B.new.foo rescue p $!

class A
  remove_method :foo
end

B.new.send :foo
"), @"
#<NoMethodError: private method `foo' called for #<B:*>>
foo
", OutputFlags.Match);
        }

        /// <summary>
        /// alias, alias_method ignore the current scope visibility flags and copy methods with their visibility unmodified.
        /// </summary>
        public void AliasedMethodVisibility1() {
            AssertOutput(() => CompilerTest(@"
class A
  def pub; end
  private
  def pri; end
  protected
  def pro; end
end

class B < A
  private
  alias a_pub pub
  protected
  alias a_pri pri
  public
  alias a_pro pro
  
  p public_instance_methods(false).sort
  p private_instance_methods(false).sort
  
  private
  alias_method :am_pub, :pub
  protected
  alias_method :am_pri, :pri
  public
  alias_method :am_pro, :pro
                       
  p public_instance_methods(false).sort
  p private_instance_methods(false).sort
end
    "), @"
[""a_pub""]
[""a_pri""]
[""a_pub"", ""am_pub""]
[""a_pri"", ""am_pri""]
");
        }
        
        private string MethodDefinitionInDefineMethodCode1 = @"
class A
  $p = lambda { def foo; end }
end

class B
  define_method :f, &$p    
end

B.new.f

puts A.send(:remove_method, :foo) rescue puts B.send(:remove_method, :foo)
";

        [Options(Compatibility = RubyCompatibility.Ruby19)]
        public void MethodDefinitionInDefineMethod1A() {
            AssertOutput(() => CompilerTest(MethodDefinitionInDefineMethodCode1), "A");
        }

        [Options(Compatibility = RubyCompatibility.Ruby18)]
        public void MethodDefinitionInDefineMethod1B() {
            AssertOutput(() => CompilerTest(MethodDefinitionInDefineMethodCode1), "B");
        }

        private string MethodDefinitionInDefineMethodCode2 = @"
class B
  define_method :m do    
    def foo; end
  end
end

class A < B
end

A.new.m

puts A.send(:remove_method, :foo) rescue puts B.send(:remove_method, :foo)
";
        [Options(Compatibility = RubyCompatibility.Ruby19)]
        public void MethodDefinitionInDefineMethod2A() {
            AssertOutput(() => CompilerTest(MethodDefinitionInDefineMethodCode2), "B");
        }

        /// <summary>
        /// MRI 1.8 actually prints A. We consider it a bug that we won't copy.
        /// </summary>
        [Options(Compatibility = RubyCompatibility.Ruby18)]
        public void MethodDefinitionInDefineMethod2B() {
            AssertOutput(() => CompilerTest(MethodDefinitionInDefineMethodCode2), "B");
        }

        private string MethodDefinitionInModuleEvalCode = @"
class A
  $p = lambda { def foo; end }
end

class B
  module_eval(&$p)
end

puts A.send(:remove_method, :foo) rescue puts B.send(:remove_method, :foo)
";

        [Options(Compatibility = RubyCompatibility.Ruby19)]
        public void MethodDefinitionInModuleEval1A() {
            AssertOutput(() => CompilerTest(MethodDefinitionInModuleEvalCode), "A");
        }

        [Options(Compatibility = RubyCompatibility.Ruby18)]
        public void MethodDefinitionInModuleEval1B() {
            AssertOutput(() => CompilerTest(MethodDefinitionInModuleEvalCode), "B");
        }
    }
}

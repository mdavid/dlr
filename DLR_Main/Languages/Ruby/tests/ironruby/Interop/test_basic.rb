# ****************************************************************************
#
# Copyright (c) Microsoft Corporation. 
#
# This source code is subject to terms and conditions of the Microsoft Public License. A 
# copy of the license can be found in the License.html file at the root of this distribution. If 
# you cannot locate the  Microsoft Public License, please send an email to 
# ironruby@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
# by the terms of the Microsoft Public License.
#
# You must not remove this notice, or any other, from this software.
#
#
# ****************************************************************************

require "../util/assert.rb"

require "mscorlib"

def test_stringbuilder
    x = System::Text::StringBuilder.new
    x.Append("abc")
    x.Append("def")
    x.Append(100)
    x.Insert(3, "012")
    
    assert_equal(x.ToString, System::String::Concat("abc012def100"))
    
    x.Capacity = 20
    assert_equal(x.Capacity, 20)
    assert_equal(x.Length, 12)
end

def test_string
    a = System::Char::Parse("a")
    b = System::Char::Parse("b")
    x = System::String.new(a, 2)
    y = System::String.new(b, 3)
    
    str = System::String
    assert_equal(str.Concat(x, y), System::String::Concat("aabbb"))
    
    assert_equal(str.Compare(x, y), -1)
    assert_equal(str.Compare(y, x), 1)
    assert_equal(str.Compare(x, x), 0)
end 

def test_field
    # const field
    assert_equal(System::Int32.MaxValue, 2147483647)
    # enum
    assert_equal(System::DayOfWeek.Sunday.ToString().to_str, 'Sunday')
    # readonly field
    assert_equal(System::DateTime.MaxValue.ToString().to_str, '12/31/9999 11:59:59 PM')
    
    assert_raise(NoMethodError) { print System::Int32.MaxValue2 }
    
    # setting to a read only field raises NoMethodError
    assert_raise(NoMethodError) { System::Int32.MaxValue = 5 }
    assert_raise(NoMethodError) { System::DayOfWeek.Sunday = 5 }
    assert_raise(NoMethodError) { System::DateTime.MaxValue = 5 }
    
    # TODO: set field test (need a type with a writable static field)
end 

def test_event
    require "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    
    t = System::Timers::Timer.new(1000)
    $flag = 0
    t.Elapsed do |sender, e|
        $flag += 1
        sender.Enabled = false
    end 
    t.Enabled = true
    t.Start()
    System::Threading::Thread.Sleep(3000)
    t.Stop()
    assert_equal($flag, 1)
end

def test_generictypes
    # TODO: more interesting tests when more features of .NET interop are working
    list = System::Collections::Generic::List
    
    IntList = list.of(System::Int32)
    a = IntList.new
    a.add 1
    a.add 2
    a.add 3
    assert_equal(a.count, 3)

    IntList2 = list[Fixnum]
    assert_equal(IntList2.new.to_string, 'System.Collections.Generic.List`1[System.Int32]'.to_clr_string)

    Dict = System::Collections::Generic::Dictionary[System::String, System::String]
    assert_equal(Dict.new.to_string, 'System.Collections.Generic.Dictionary`2[System.String,System.String]'.to_clr_string)
    
    assert_raise(ArgumentError) { System::Type.of(Fixnum) }
    assert_raise(ArgumentError) { list[System] }
    assert_raise(ArgumentError) { list[1] }
    assert_raise(ArgumentError) { list[] }
    assert_raise(ArgumentError) { list.of(Fixnum, Fixnum) }
    
    #System::Nullable.of(System::Int32)
    #System::IComparable.of(Fixnum)
    System::Predicate.of(System::Int32)
end


def test_ienumerable
    # .NET types that implement IEnumerable should
    # implement each & include Enumerable
    a = System::Collections::ArrayList.new
    a.add 1
    a.add 2
    a.add 3
    b = []
    a.each { |i| b << i }
    assert_equal(b, [1, 2, 3])
    c = a.collect { |i| i + 3 }
    assert_equal(c, [4, 5, 6])
end

def test_icomparable
    a = System::DateTime.new 1900, 1, 1
    b = System::DateTime.new 1900, 1, 15
    c = System::DateTime.new 1900, 2, 1
    assert_true { a < b }
    assert_true { b <= c }
    assert_equal(a <=> c, -1)
    assert_true { b.between?(a,c) }
    assert_false { b.between?(c,a) }
end

def test_idictionary
    d = System::Collections::Generic::Dictionary[Object, Object].new
    d[:abc] = 'def'
    assert_equal(d[:abc], 'def')
    assert_equal(d.inspect, '{:abc=>"def"}')
end

def test_ilist
    a = System::Collections::ArrayList.new
    a.Add(1)
    a.Add(3)
    a.Add(2)
    a.Add(3)
    assert_equal(a, [1, 3, 2, 3])
    assert_equal(a[1], 3)
    b = System::Collections::ArrayList.new
    b.Add(5)
    b.Add(4)
    b.Add(3)
    b.Add(4)
    b.Add(6)
    assert_equal(b, [5, 4, 3, 4, 6])
    c = a | b
    assert_equal(c, [1, 3, 2, 5, 4, 6])
end

  # TODO: more interesting tests when more features of .NET interop are working
  class Bob_test_inherit < System::Collections::ArrayList
    def foo
      count
    end
  end

def test_inherit

  a = Bob_test_inherit.new
  a.add 1
  a.Add 2
  a.add 3
  assert_equal(a.foo, 3)
  assert_equal(a.Count, 3)

end

class System::Collections::ArrayList
        def total
            sum = 0
            each { |i| sum += i }
            sum
        end
end

def test_monkeypatch
    a = System::Collections::ArrayList.new
    
    b = System::Collections::ArrayList.new
    a.add 3
    a << 2 << 1
    assert_equal(a.total, 6)
    b.replace [4,5,6]
    assert_equal(b.total, 15)
  end
  
def test_unmangling
  max = 2147483647
  assert_equal(System::Int32.MaxValue, max)
  assert_equal(System::Int32.max_value, max)
  
  # Can't unmangle names with leading, trailing, or consecutive underscores
  assert_raise(NoMethodError) { System::Int32.max_value_ }
  assert_raise(NoMethodError) { System::Int32._max_value }
  assert_raise(NoMethodError) { System::Int32.max__value }
  assert_raise(NoMethodError) { System::Int32.MaxValue_ }
  assert_raise(NoMethodError) { System::Int32._MaxValue }
  
  # Also can't unmangle names with uppercase letters
  assert_raise(NoMethodError) { System::Int32.maxValue }
  assert_raise(NoMethodError) { System::Int32.max_Value }
  assert_raise(NoMethodError) { System::Int32.Maxvalue }
  assert_raise(NoMethodError) { System::Int32.Max_value }  
end

def test_invisible_types
  # we should be able to call methods on a type
  # that's not visible--as long as the method itself is
  # on a visible type somewhere in the heirarchy
  
  # Test returning a non-visible type (RuntimeType in this case)
  type = System::Type.get_type('System.Int32'.to_clr_string)
  
  # Calling properties/methods on a non-visible type
  assert_equal(type.full_name, 'System.Int32'.to_clr_string)
  assert_equal(type.is_assignable_from(type), true)
end

def test_generics_give_type_error
  assert_raise(TypeError) do
    Class.new(System::Collections::Generic::List)
  end
  assert_raise(TypeError) do
    Class.new do
      include System::Collections::Generic::List
    end
  end
end

def test_include_interface_after_type_creation
  c = Class.new do
    include System::Collections::Generic::List[System::Int32]
  end

  # No object has been instantiated so we should be able to reopen the class and add a new interface
  c.class_eval do
    include System::IDisposable
  end

  # After instantiating the object, this will give us an error
  tmp = c.new
  assert_raise(TypeError) do
    c.class_eval do
      include System::IDisposable
    end
  end
end

test_event
test_field
test_string
test_stringbuilder
test_generictypes
test_ienumerable
# TODO: disabling this test until we figure out how to enable creating DateTime
# objects using their ctors and not Time's RubyConstructors
#test_icomparable
test_idictionary
test_ilist
test_inherit
test_monkeypatch
test_unmangling
test_invisible_types

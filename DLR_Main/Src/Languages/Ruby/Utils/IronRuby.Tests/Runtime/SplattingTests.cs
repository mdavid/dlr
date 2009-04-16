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
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using IronRuby.Builtins;
using IronRuby.Runtime.Calls;
using Microsoft.Scripting.Generation;
using Microsoft.Scripting.Utils;

namespace IronRuby.Tests {

    public partial class Tests {

        internal static MethodInfo/*!*/ CreateParamsArrayMethod(string/*!*/ name, Type/*!*/[]/*!*/ paramTypes, int paramsArrayIndex, int returnValue) {
            var tb = Snippets.Shared.DefineType("<T>", typeof(object), false, false).TypeBuilder;
            var mb = tb.DefineMethod(name, CompilerHelpers.PublicStatic, typeof(KeyValuePair<int, Array>), paramTypes);
            var pb = mb.DefineParameter(1 + paramsArrayIndex, ParameterAttributes.None, "ps");
            pb.SetCustomAttribute(new CustomAttributeBuilder(typeof(ParamArrayAttribute).GetConstructor(Type.EmptyTypes), ArrayUtils.EmptyObjects));

            var il = mb.GetILGenerator();
            il.Emit(OpCodes.Ldc_I4, returnValue);
            il.Emit(OpCodes.Ldarg, paramsArrayIndex);
            il.Emit(OpCodes.Newobj, typeof(KeyValuePair<int, Array>).GetConstructor(new[] { typeof(int), typeof(Array) }));
            il.Emit(OpCodes.Ret);
            return tb.CreateType().GetMethod(name, BindingFlags.Public | BindingFlags.Static);

#if FALSE
            var array = il.DeclareLocal(typeof(object[]));
            il.Emit(OpCodes.Ldc_I4, paramTypes.Length);
            il.Emit(OpCodes.Newarr, typeof(object[]));
            il.Emit(OpCodes.Stloc, array);

            for (int i = 0; i < paramTypes.Length; i++) {
                il.Emit(OpCodes.Ldloc, array);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldarg, i);
                il.Emit(OpCodes.Box, typeof(object));
                il.Emit(OpCodes.Stelem, typeof(object));
            }

            il.Emit(OpCodes.Ldloc, array);
            il.Emit(OpCodes.Ldc_I4, returnValue);
            il.Emit(OpCodes.Newobj, typeof(KeyValuePair<Array, int>).GetConstructor(new[] { typeof(Array), typeof(int) }));
            il.Emit(OpCodes.Ret);
#endif
        }

        public void Scenario_RubyArgSplatting1() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo(a,b,c)
  print a,b,c
end

foo(*[1,2,3])
");
            }, @"123");
        }

        public void Scenario_RubyArgSplatting2() {
            AssertOutput(delegate() {
                CompilerTest(@"
class C
    def []=(a,b,c)
      print a,b,c
    end
end

x = [1,2]
C.new[*x] = 3
C.new[1, *[2]] = 3
");
            }, @"123123");
        }

        public void Scenario_RubyArgSplatting3() {
            AssertOutput(delegate() {
                CompilerTest(@"
def foo(a,b,c)
  print a,b,c
  puts
end

foo(1,2,*3)
foo(1,2,*nil)
");
            }, @"
123
12nil");
        }

        /// <summary>
        /// Splat anything that's IList (including arrays and values passed via out parameters).
        /// </summary>
        public void Scenario_RubyArgSplatting4() {
            AssertOutput(delegate() {
                CompilerTest(@"
a,b,c = System::Array[Fixnum].new([1,2,3])
p [a,b,c]

def y1; yield System::Array[Fixnum].new([4]); end
def y2; yield System::Array[Fixnum].new([4,5]); end
def y3; yield System::Array[Fixnum].new([4,5,6]); end
def y10; yield System::Array[Fixnum].new([1,2,3,4,5,6,7,8,9,10]); end

y1 { |x| p [x] }
y2 { |x,y| p [x,y] }
y3 { |x,y,z| p [x,y,z] }
y10 { |a1,a2,a3,a4,a5,a6,a7,a8,a9,a10| p [a1,a2,a3,a4,a5,a6,a7,a8,a9,a10] }

dict = System::Collections::Generic::Dictionary[Fixnum, Fixnum].new
dict.add(1,1)
has_value, value = dict.try_get_value(1)
p [has_value, value]
");
            }, @"
[1, 2, 3]
[[4]]
[4, 5]
[4, 5, 6]
[1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
[true, 1]
");
        }

        /// <summary>
        /// Splat anything that implements to_ary.
        /// </summary>
        public void Scenario_RubyArgSplatting5() {
            // TODO:
//            XAssertOutput(delegate() {
//                CompilerTest(@"
//");
//            }, @"
//");
        }

        public void Scenario_RubyArgSplatting6() {
            var c = Context.GetClass(typeof(MethodsWithParamArrays));
            Context.SetGlobalConstant("C", new MethodsWithParamArrays());

            // The post-param-array arguments might decide which overload to call:
            c.SetLibraryMethod("bar", new RubyMethodGroupInfo(new[] { 
                CreateParamsArrayMethod("B0", new[] { typeof(int), typeof(int), typeof(int[]), typeof(bool) }, 2, 0),
                CreateParamsArrayMethod("B1", new[] { typeof(int), typeof(int[]), typeof(int), typeof(int) }, 1, 1),
            }, c, null, true), true);

            AssertOutput(delegate() {
                CompilerTest(@"
x = C.bar(*[0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,true])
p x.key, x.value
x = C.bar(*[0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15])
p x.key, x.value
");
            }, @"
0
[2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14]
1
[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
");

            // Huge splattees.
            AssertOutput(delegate() {
                CompilerTest(@"
[
[1],
[1] * 2,
[1] * 3,
[1] * 4,
[1] * 5,
[1] * 10003 + [true],
[1] * 10003,
].each do |s| 
  begin
    x = C.bar(*s)
    puts ""B#{x.key} -> #{x.value.length}""
  rescue 
    p $!
  end
end
");
            }, @"
#<ArgumentError: wrong number of arguments (1 for 3)>
#<ArgumentError: wrong number of arguments (2 for 3)>
B1 -> 0
B1 -> 1
B1 -> 2
B0 -> 10001
B1 -> 10000
");

            // Overloads might differ only in the element types of params-array.
            // If binder decision is not based upon all splatted item types
            c.SetLibraryMethod("baz", new RubyMethodGroupInfo(new[] { 
                CreateParamsArrayMethod("Z0", new[] { typeof(int), typeof(object[]) }, 1, 0),
                CreateParamsArrayMethod("Z1", new[] { typeof(int), typeof(MutableString[]) }, 1, 1),
                CreateParamsArrayMethod("Z2", new[] { typeof(int), typeof(int[]) }, 1, 2),
            }, c, null, true), true);

            AssertOutput(delegate() {
                CompilerTest(@"
[
[1] * 20 + ['x'] + [1] * 20,
[1] * 10001,
[1] * 10000 + [true],
[1] + ['x'] * 10000,
].each do |s| 
  x = C.baz(*s)
  puts ""Z#{x.key} -> #{x.value.length}""
end
");
            }, @"
Z0 -> 40
Z2 -> 10000
Z0 -> 10000
Z1 -> 10000
");

            // Tests error handling and caching.
            c.SetLibraryMethod("error", new RubyMethodGroupInfo(new[] { 
                CreateParamsArrayMethod("E1", new[] { typeof(int), typeof(MutableString[]) }, 1, 1),
                CreateParamsArrayMethod("E2", new[] { typeof(int), typeof(int[]) }, 1, 2),
            }, c, null, true), true);

            AssertOutput(delegate() {
                CompilerTest(@"
[
[1] + [2] * 10000,
[1] * 20 + ['zzz'] + [1] * 20,
[1] + ['x'] * 10000,
].each do |s| 
  begin  
    x = C.error(*s)
    puts ""Z#{x.key} -> #{x.value.length}""
  rescue 
    p $!
  end
end
");
            }, @"
Z2 -> 10000
#<TypeError: Cannot convert MutableString(zzz) to Int32>
Z1 -> 10000
");

            // TODO: test GetPreferredParameters with collapsed arguments
        }

        public void Scenario_CaseSplatting1() {
            AssertOutput(() => CompilerTest(@"
[0,2,5,8,6,7,9,4].each do |x|
  case x
    when 0,1,*[2,3,4]: print 0
    when *[5]: print 1
    when *[6,7]: print 2
    when *8: print 3
    when *System::Array[Fixnum].new([9]): print 4
  end
end
"), @"
00132240
");
        }
        
    }
}

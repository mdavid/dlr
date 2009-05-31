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
using IronRuby.Builtins;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using IronRuby.Runtime;
using Microsoft.Scripting;
using System.Runtime.CompilerServices;
using Microsoft.Runtime.CompilerServices;


namespace IronRuby.Tests {
    public partial class Tests {
        public void StringsPlus() {
            AssertExceptionThrown<InvalidOperationException>(delegate() {
                CompilerTest(@"
puts 'foo' + nil
");
            });
        }

        public void Strings0() {
            AssertOutput(delegate() {
                CompilerTest(@"
puts ""foo""
puts ""foo"" 'bar' ""baz""
");
            }, @"
foo
foobarbaz
");
        }

        public void Strings1() {
            AssertOutput(delegate() {
                CompilerTest(@"
puts ""foo#{""bar""}baz""
");
            }, @"
foobarbaz
");
        }

        public void Strings2() {
            AssertOutput(delegate() {
                CompilerTest(@"
puts ""foo#{1;2;3}baz""
");
            }, @"
foo3baz
");
        }

        public void Strings3() {
            AssertOutput(delegate() {
                CompilerTest(@"
class String; def to_s; 'S'; end; end
class Fixnum; def to_s; 'N'; end; end

p """"
puts ""#{1}""
puts ""#{1}-""
puts ""-#{1}""
puts ""-#{1}-""
puts ""#{1}#{1}""
puts ""#{1}+#{1}""
puts ""-#{1}+#{1}""
puts ""-#{1}+#{1}-""

puts ""-#{x = 'bob'}-""
");
            }, @"
""""
N
N-
-N
-N-
NN
N+N
-N+N
-N+N-
-bob-
");
        }

        public void Strings4() {
            AssertOutput(delegate() {
                CompilerTest(@"
p ""#{nil}""
p ""#{nil}-""
p ""-#{nil}""
p ""-#{nil}-""
p ""#{nil}#{nil}""
p ""-#{nil}+#{nil}-""
");
            }, @"
""""
""-""
""-""
""--""
""""
""-+-""
");
        }

        public void Strings5() {
            AssertOutput(delegate() {
                CompilerTest(@"
class String; def to_s; 'S'; end; end
class Fixnum; def to_s; 'N'; end; end

puts :""#{1}""
puts :""#{1}-""
puts :""-#{1}""
puts :""-#{1}-""
puts :""#{1}#{1}""
puts :""#{1}+#{1}""
puts :""-#{1}+#{1}""
puts :""-#{1}+#{1}-""

puts ""-#{x = 'bob'}-""
");
            }, @"
N
N-
-N
-N-
NN
N+N
-N+N
-N+N-
-bob-
");
        }

        public void Strings6() {
            AssertOutput(delegate() {
                CompilerTest(@"
p :""#{}"" rescue p $!
p :""#{}#{}"" rescue p $!
p :""#{}#{''}#{}"" rescue p $!

p :""#{nil}a""
p :""a#{nil}""
p :""a#{nil}b""
p :""a#{nil}b#{nil}c""
");
            }, @"
#<ArgumentError: interning empty string>
#<ArgumentError: interning empty string>
#<ArgumentError: interning empty string>
:a
:a
:ab
:abc
");
        }

        public void Strings7() {
            AssertOutput(delegate() {
                CompilerTest(@"
puts 'foobarbaz'[3,3]
");
            }, "bar");
        }

        public void Strings8() {
            AssertOutput(delegate() {
                CompilerTest(@"
puts 'foo  bar'.split
");
            }, @"
foo
bar
");
        }

        [Options(Compatibility = RubyCompatibility.Ruby19)]
        public void Strings9() {
            // TODO:
            var source = Engine.CreateScriptSource(new BinaryContentProvider(BinaryEncoding.Instance.GetBytes(@"""\u03a3""")), null, BinaryEncoding.Instance);
            AssertExceptionThrown<SyntaxErrorException>(() => source.Execute<MutableString>());

            // TODO: mixing incompatible encodings at compile time (literals "foo" "bar") or runtime "foo" + "bar"
        }

        /// <summary>
        /// Embedded string does call "to_s" w/o calling "respond_to?" first.
        /// </summary>
        public void ToSConversion1() {
            AssertOutput(delegate() {
                CompilerTest(@"
class X
  def respond_to? name
    puts name
  end
  
  def to_s
    'TO_S'
  end

  puts ""#{new}""
end
");
            }, @"
TO_S
");
        }

        public void ToSConversion2() {
            AssertOutput(delegate() {
                CompilerTest(@"
class NilClass
  def to_s; 'NULL1'; end
  puts ""#{nil}""

  def to_s; 'NULL2'; end
  puts ""#{nil}""
end

class SubString < String
  def to_s; 'XXX'; end
  puts ""#{new 'SUB'}""
end
");
            }, @"
NULL1
NULL2
SUB
");
        }

        public class ClassWithToStringHashEquals1 {
            public override string ToString() {
                return "hello";
            }

            public override int GetHashCode() {
                return 1234;
            }

            public override bool Equals(object obj) {
                return true;
            }
        }

        public void ToSConversionClr1() {
            Engine.Runtime.Globals.SetVariable("B", Context.GetClass(typeof(ClassWithToStringHashEquals1)));

            var objs = Engine.Execute<RubyArray>(@"
class C
  def to_s
    '123'
  end
end

class D
end

class E < B
end

class F < B
  def to_s
    'abc'
  end
end

[C.new, D.new, E.new, E.new.to_s, F.new]
");

            Assert(objs[0].ToString() == "123");

            string s = objs[1].ToString();
            Assert(s.StartsWith("#<D:0x") && s.EndsWith(">"));

            s = objs[2].ToString();
            Assert(s == "hello");

            s = objs[3].ToString();
            Assert(s == "hello");

            //TODO:
            //s = objs[4].ToString();
            //Assert(s == "abc");

            var range = new Range(1, 2, true);
            Assert(range.ToString() == "1...2");

            var regex = new RubyRegex("hello", RubyRegexOptions.IgnoreCase | RubyRegexOptions.Multiline);
            Assert(regex.ToString() == "(?mi-x:hello)");
        }

        public void HashClr1() {
            // TODO: 
            // Test_HashClr("get_hash_code");
        }

        public void HashClr2() {
            // TODO:  
            // Test_HashClr("hash");
        }

        private void Test_HashClr(string/*!*/ methodName) {
            Engine.Runtime.Globals.SetVariable("B", Context.GetClass(typeof(ClassWithToStringHashEquals1)));

            var objs = Engine.Execute<RubyArray>(String.Format(@"
class C
  def {0}
    789
  end
end

class D
end

class E < B
end

class F < B
  def {0}
    1000
  end
end

[C.new, D.new, E.new, E.new.{0}, F.new]
", methodName));

            int h = objs[0].GetHashCode();
            Assert(h == 789);

            h = objs[1].GetHashCode();
            Assert(h == RuntimeHelpers.GetHashCode(objs[1]));

            h = objs[2].GetHashCode();
            Assert(h == 1234);

            h = (int)objs[3];
            Assert(h == 1234);

            h = objs[4].GetHashCode();
            Assert(h == 1000);
        }

        [Options(Compatibility = RubyCompatibility.Ruby18)]
        private void Inspect1() {
            const char sq = '\'';

            var sjisEncoding = RubyEncoding.GetRubyEncoding("SJIS");
            // あ
            var sjisWide = new byte[] { 0x82, 0xa0 };
            // \u{12345} in UTF-8:
            var utf8 = new byte[] { 0xF0, 0x92, 0x8D, 0x85 };
            // surrogates: U+d808 U+df45 
            var utf16 = Encoding.UTF8.GetString(utf8);

            string s;

            s = MutableStringOps.GetQuotedStringRepresentation(MutableString.CreateBinary(utf8, RubyEncoding.Binary), Context, false, sq).ToString();
            Assert(s == @"'\360\222\215\205'");

            s = MutableStringOps.GetQuotedStringRepresentation(MutableString.CreateBinary(utf8, RubyEncoding.Binary), Context, true, sq).ToString();
            Assert(s == @"'\360\222\215\205'");

            s = MutableStringOps.GetQuotedStringRepresentation(MutableString.CreateBinary(utf8, RubyEncoding.KCodeUTF8), Context, false, sq).ToString();
            Assert(s == "'" + utf16 + "'");

            s = MutableStringOps.GetQuotedStringRepresentation(MutableString.CreateBinary(utf8, RubyEncoding.KCodeUTF8), Context, true, sq).ToString();
            Assert(s == @"'\360\222\215\205'");

            s = MutableStringOps.GetQuotedStringRepresentation(MutableString.CreateBinary(sjisWide, RubyEncoding.KCodeSJIS), Context, false, sq).ToString();
            Assert(s == @"'あ'");

            s = MutableStringOps.GetQuotedStringRepresentation(MutableString.CreateBinary(sjisWide, RubyEncoding.KCodeSJIS), Context, true, sq).ToString();
            Assert(s == @"'\202\240'");
        }

        [Options(Compatibility = RubyCompatibility.Ruby19)]
        private void Inspect2() {
            const char sq = '\'';

            var sjisEncoding = RubyEncoding.GetRubyEncoding("SJIS");
            // あ
            var sjisWide = new byte[] { 0x82, 0xa0 };
            // \u{12345} in UTF-8:
            var utf8 = new byte[] { 0xF0, 0x92, 0x8D, 0x85 };
            // \u{12345} in UTF-16: U+d808 U+df45 
            var utf16 = Encoding.UTF8.GetString(utf8);

            string s;

            s = MutableStringOps.GetQuotedStringRepresentation(MutableString.CreateBinary(utf8, RubyEncoding.Binary), Context, false, sq).ToString();
            Assert(s == @"'\xF0\x92\x8D\x85'");

            s = MutableStringOps.GetQuotedStringRepresentation(MutableString.CreateBinary(utf8, RubyEncoding.Binary), Context, true, sq).ToString();
            Assert(s == @"'\xF0\x92\x8D\x85'");

            s = MutableStringOps.GetQuotedStringRepresentation(MutableString.CreateBinary(utf8, RubyEncoding.UTF8), Context, false, sq).ToString();
            Assert(s == "'" + utf16 + "'");

            s = MutableStringOps.GetQuotedStringRepresentation(MutableString.CreateBinary(utf8, RubyEncoding.UTF8), Context, true, sq).ToString();
            Assert(s == @"'\u{12345}'");

            s = MutableStringOps.GetQuotedStringRepresentation(MutableString.CreateBinary(sjisWide, sjisEncoding), Context, false, sq).ToString();
            Assert(s == @"'あ'");

            s = MutableStringOps.GetQuotedStringRepresentation(MutableString.CreateBinary(sjisWide, sjisEncoding), Context, true, sq).ToString();
            Assert(s == @"'\x82\xA0'");
        }

    }
}

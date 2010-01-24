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
using System.Runtime.CompilerServices;
using System.Dynamic;
using IronRuby.Builtins;
using IronRuby.Runtime;
using Microsoft.Scripting;
using Microsoft.Scripting.Math;
using Microsoft.Scripting.Runtime;
using System.Runtime.InteropServices;
using Microsoft.Scripting.Generation;
using Microsoft.Scripting.Utils;
using System.Collections.Generic;
using System.Globalization;

namespace IronRuby.StandardLibrary.Yaml {

    [RubyClass(Extends = typeof(object))]
    public static class YamlObjectOps {        

        [RubyMethod("to_yaml_properties")]
        public static RubyArray/*!*/ ToYamlProperties(
            BinaryOpStorage/*!*/ comparisonStorage,
            BinaryOpStorage/*!*/ lessThanStorage,
            BinaryOpStorage/*!*/ greaterThanStorage,
            object self) {
            return ArrayOps.SortInPlace(comparisonStorage, lessThanStorage, greaterThanStorage,
                KernelOps.InstanceVariables(comparisonStorage.Context, self)
            );
        }

        [RubyMethod("to_yaml_style")]
        public static object ToYamlStyle(object self) {
            return null;
        }

        [RubyMethod("to_yaml_node", RubyMethodAttributes.PrivateInstance)]
        public static object ToYamlProperties(object self, [NotNull]RubyRepresenter/*!*/ rep) {
            var map = new Dictionary<MutableString, object>();
            rep.AddYamlProperties(self, map);
            return rep.Map(self, map);
        }

        [RubyMethod("to_yaml")]
        public static object ToYaml(RubyContext/*!*/ context, object self, params object[] args) {
            return RubyYaml.DumpAll(context, new object[] { self }, null);
        }

        [RubyMethod("taguri")]
        public static MutableString/*!*/ TagUri(RubyContext/*!*/ context, object self) {
            return MutableString.Create("!ruby/object:", RubyEncoding.ClassName).
                Append(context.GetClassName(self)).
                Append(' ');
        }
    }

    [RubyClass(Extends = typeof(RubyClass))]
    public static class YamlClassOps {
        [RubyMethod("to_yaml_node", RubyMethodAttributes.PrivateInstance)]
        public static Node ToYamlNode(RubyContext/*!*/ context, object self, RubyRepresenter rep) {
            throw RubyExceptions.CreateTypeError("can't dump anonymous class " + context.GetClassDisplayName(self));
        }
    }

    [RubyClass(Extends = typeof(RubyModule))]
    public static class YamlModuleOps {
        [RubyMethod("yaml_as")]
        public static object YamlAs(RubyScope/*!*/ scope, RubyModule/*!*/ self, object tag) {
            RubyModule yamlModule;
            scope.RubyContext.TryGetModule(scope.GlobalScope, "YAML", out yamlModule);
            return RubyYaml.TagClass(yamlModule, tag, self);
        }
    }

    [RubyClass(Extends = typeof(Hash))]
    public static class YamlHashOps {
        [RubyMethod("to_yaml_node", RubyMethodAttributes.PrivateInstance)]
        public static Node ToYamlNode(Hash/*!*/ self, [NotNull]RubyRepresenter/*!*/ rep) {
            return rep.Map(self, self);
        }

        [RubyMethod("taguri")]
        public static MutableString/*!*/ TagUri(RubyContext/*!*/ context, object self) {
            return MutableString.CreateAscii("tag:yaml.org,2002:map");
        }
    }

    [RubyModule(Extends = typeof(RubyArray))]
    public static class YamlArrayOps {
        [RubyMethod("to_yaml_node", RubyMethodAttributes.PrivateInstance)]
        public static Node ToYamlNode(RubyContext/*!*/ context, RubyArray/*!*/ self, [NotNull]RubyRepresenter/*!*/ rep) {
            return rep.Sequence(self, self);
        }

        [RubyMethod("taguri")]
        public static MutableString/*!*/ TagUri(RubyContext/*!*/ context, object self) {
            return MutableString.CreateAscii("tag:yaml.org,2002:seq");
        }
    }

    [RubyModule(Extends = typeof(RubyStruct))]
    public static class YamlStructOps {
        [RubyMethod("to_yaml_node", RubyMethodAttributes.PrivateInstance)]
        public static Node ToYamlNode(RubyStruct/*!*/ self, [NotNull]RubyRepresenter/*!*/ rep) {
            var fieldNames = self.GetNames();
            var map = new Dictionary<MutableString, object>(fieldNames.Count);
            for (int i = 0; i < fieldNames.Count; i++) {
                // TODO: symbol encodings
                map[MutableString.Create(fieldNames[i], RubyEncoding.UTF8)] = self[i];
            }
            rep.AddYamlProperties(self, map);
            return rep.Map(self, map);
        }

        [RubyMethod("taguri")]
        public static MutableString/*!*/ TagUri(RubyStruct/*!*/ self) {
            MutableString str = MutableString.CreateMutable("tag:ruby.yaml.org,2002:struct:", RubyEncoding.ClassName);
            string name = self.ImmediateClass.GetNonSingletonClass().Name;
            if (name != null) {
                string structPrefix = "Struct::";
                if (name.StartsWith(structPrefix)) {
                    name = name.Substring(structPrefix.Length);
                }
            }
            return str.Append(name);
        }
    }

    [RubyModule(Extends = typeof(Exception))]
    public static class YamlExceptionOps {
        [RubyMethod("to_yaml_node", RubyMethodAttributes.PrivateInstance)]
        public static Node ToYamlNode(CallSiteStorage<Func<CallSite, Exception, object>>/*!*/ messageStorage,
            Exception/*!*/ self, [NotNull]RubyRepresenter/*!*/ rep) {

            var site = messageStorage.GetCallSite("message", 0);
            var map = new Dictionary<MutableString, object>() {
                { MutableString.CreateAscii("message"), site.Target(site, self) }
            };
            
            rep.AddYamlProperties(self, map);
            return rep.Map(self, map);
        }

        [RubyMethod("taguri")]
        public static MutableString TagUri(RubyContext/*!*/ context, object self) {
            return MutableString.CreateMutable(RubyEncoding.ClassName).
                Append("!ruby/exception:").
                Append(context.GetClassName(self));
        }
    }

    [RubyModule(Extends = typeof(MutableString))]
    public static class YamlStringOps {
        [RubyMethod("is_complex_yaml?")]
        public static bool IsComplexYaml(
            CallSiteStorage<Func<CallSite, object, MutableString>>/*!*/ toYamlStyleStorage,
            CallSiteStorage<Func<CallSite, object, RubyArray>>/*!*/ toYamlPropertiesStorage,
            MutableString/*!*/ self) {

            var toYamlStyleSite = toYamlStyleStorage.GetCallSite("to_yaml_style", 0);
            var toYamlPropertiesSite = toYamlPropertiesStorage.GetCallSite("to_yaml_properties", 0);

            return RubyOps.IsTrue(toYamlStyleSite.Target(toYamlStyleSite, self)) ||
                   toYamlPropertiesSite.Target(toYamlPropertiesSite, self).Count == 0 ||
                   AfterNewLine(self.ConvertToString());
        }

        // True if has a newline & something is after it
        private static bool AfterNewLine(string str) {
            int i = str.IndexOf('\n');
            return i >= 0 && i < str.Length - 1;
        }

        [RubyMethod("is_binary_data?")]
        public static object IsBinaryData(UnaryOpStorage/*!*/ isEmptyStorage, MutableString/*!*/ self) {

            var site = isEmptyStorage.GetCallSite("empty?");
            if (RubyOps.IsTrue(site.Target(site, self))) {
                return null;
            }

            return ScriptingRuntimeHelpers.BooleanToObject((self.IsBinary ? self.IndexOf(0) : self.IndexOf('\0')) >= 0);
        }

        [RubyMethod("to_yaml_node", RubyMethodAttributes.PrivateInstance)]
        public static Node/*!*/ ToYamlNode(UnaryOpStorage/*!*/ isBinaryDataStorage, MutableString/*!*/ self, [NotNull]RubyRepresenter/*!*/ rep) {

            var site = isBinaryDataStorage.GetCallSite("is_binary_data?");
            if (RubyOps.IsTrue(site.Target(site, self))) {
                return rep.BaseCreateNode(self.ConvertToBytes());
            }

            string str = self.ConvertToString();
            RubyArray props = rep.ToYamlProperties(self);
            if (props.Count == 0) {
                MutableString taguri = rep.GetTagUri(self);

                char style = '\0';
                if (str.StartsWith(":")) {
                    style = '"';
                } else {
                    MutableString styleStr = rep.ToYamlStyle(self);
                    if (styleStr != null && styleStr.Length > 0) {
                        style = styleStr.GetChar(0);
                    }
                }

                return rep.Scalar(taguri != null ? taguri.ConvertToString() : "", str, style);
            }

            var map = new Dictionary<MutableString, object>() {
                { MutableString.CreateAscii("str"), str }
            };
            rep.AddYamlProperties(self, map, props);
            return rep.Map(self, map);
        }

        [RubyMethod("taguri")]
        public static MutableString/*!*/ TagUri(object self) {
            return MutableString.CreateAscii("tag:yaml.org,2002:str");
        }
    }

    [RubyModule(Extends = typeof(Integer))]
    public static class YamlIntegerOps {
        [RubyMethod("to_yaml_node", RubyMethodAttributes.PrivateInstance)]
        public static Node/*!*/ ToYaml(ConversionStorage<MutableString>/*!*/ tosConversion, object self, [NotNull]RubyRepresenter/*!*/ rep) {
            return rep.Scalar(self, Protocols.ConvertToString(tosConversion, self));
        }          

        [RubyMethod("taguri")]
        public static MutableString/*!*/ TagUri(object self) {
            return MutableString.CreateAscii("tag:yaml.org,2002:int");
        }
    }

    [RubyModule(Extends = typeof(BigInteger))]
    public static class YamlBigIntegerOps {
        [RubyMethod("to_yaml_node", RubyMethodAttributes.PrivateInstance)]
        public static Node/*!*/ ToYaml(ConversionStorage<MutableString>/*!*/ tosConversion, [NotNull]BigInteger self, [NotNull]RubyRepresenter/*!*/ rep) {
            return YamlIntegerOps.ToYaml(tosConversion, self, rep);
        } 

        [RubyMethod("taguri")]
        public static MutableString/*!*/ TagUri([NotNull]BigInteger self) {
            return MutableString.CreateAscii("tag:yaml.org,2002:int:Bignum");            
        }
    }

    [RubyModule(Extends = typeof(double))]
    public static class YamlDoubleOps {
        [RubyMethod("to_yaml_node", RubyMethodAttributes.PrivateInstance)]
        public static Node/*!*/ ToYaml(ConversionStorage<MutableString>/*!*/ tosConversion, double self, [NotNull]RubyRepresenter/*!*/ rep) {
            MutableString str = Protocols.ConvertToString(tosConversion, self);
            if (str != null) {
                if (str.Equals("Infinity")) {
                    str = MutableString.CreateAscii(".Inf");
                } else if (str.Equals("-Infinity")) {
                    str = MutableString.CreateAscii("-.Inf");
                } else if (str.Equals("NaN")) {
                    str = MutableString.CreateAscii(".NaN");
                }
            }
            return rep.Scalar(self, str);
        }    

        [RubyMethod("taguri")]
        public static MutableString/*!*/ TagUri(double self) {
            return MutableString.CreateAscii("tag:yaml.org,2002:float");
        }
    }

    [RubyModule(Extends = typeof(Range))]
    public static class YamlRangeOps {
        [RubyMethod("to_yaml_node", RubyMethodAttributes.PrivateInstance)]
        public static Node/*!*/ ToYaml(UnaryOpStorage/*!*/ beginStorage, UnaryOpStorage/*!*/ endStorage, UnaryOpStorage/*!*/ exclStorage, 
            Range/*!*/ self, [NotNull]RubyRepresenter/*!*/ rep) {

            var begin = beginStorage.GetCallSite("begin");
            var end = endStorage.GetCallSite("end");

            var map = new Dictionary<MutableString, object>() {
                { MutableString.CreateAscii("begin"), begin.Target(begin, self) },
                { MutableString.CreateAscii("end"), end.Target(end, self) },
                { MutableString.CreateAscii("excl"), self.ExcludeEnd },
            };

            rep.AddYamlProperties(self, map);
            return rep.Map(self, map);
        }

        [RubyMethod("taguri")]
        public static MutableString TagUri([NotNull]Range self) {
            return MutableString.CreateAscii("tag:ruby.yaml.org,2002:range");
        }
    }

    [RubyModule(Extends = typeof(RubyRegex))]
    public static class YamlRegexpOps {
        [RubyMethod("to_yaml_node", RubyMethodAttributes.PrivateInstance)]
        public static Node/*!*/ ToYaml(RubyRegex/*!*/ self, [NotNull]RubyRepresenter/*!*/ rep) {
            return rep.Scalar(self, rep.Context.Inspect(self));
        }

        [RubyMethod("taguri")]
        public static MutableString/*!*/ TagUri(RubyRegex/*!*/ self) {
            return MutableString.CreateAscii("tag:ruby.yaml.org,2002:regexp");
        }
    }

    [RubyModule(Extends = typeof(RubyTime))]
    public static class TimeOps {
        [RubyMethod("to_yaml_node", RubyMethodAttributes.PrivateInstance)]
        public static Node/*!*/ ToYaml(RubyTime self, [NotNull]RubyRepresenter/*!*/ rep) {
            TimeSpan offset = self.GetCurrentZoneOffset();
            long fractional = self.Microseconds;
            return rep.Scalar(self, MutableString.CreateAscii(String.Format(CultureInfo.InvariantCulture,
                "{0:yyyy-MM-dd HH:mm:ss}" + (fractional == 0 ? "" : ".{1:D6}") + (self.Kind == DateTimeKind.Utc ? " Z" : " {2}{3:D2}:{4:D2}"),
                self.DateTime,
                fractional,
                offset.Hours >= 0 ? "+" : "",
                offset.Hours, 
                offset.Minutes
            )));
        }

        [RubyMethod("taguri")]
        public static MutableString/*!*/ TagUri(RubyTime self) {
            return MutableString.CreateAscii("tag:yaml.org,2002:timestamp");
        }
    }

    [RubyModule(Extends = typeof(SymbolId))]
    public static class YamlSymbolOps {
        [RubyMethod("to_yaml_node", RubyMethodAttributes.PrivateInstance)]
        public static Node/*!*/ ToYaml(object self, [NotNull]RubyRepresenter/*!*/ rep) {
            return rep.Scalar(self, rep.Context.Inspect(self));
        }
        
        [RubyMethod("taguri")]
        public static MutableString/*!*/ TagUri(object self) {
            return MutableString.CreateAscii("tag:yaml.org,2002:str");
        }
    }

    [RubyModule(Extends = typeof(TrueClass))]
    public static class YamlTrueOps {
        [RubyMethod("to_yaml_node", RubyMethodAttributes.PrivateInstance)]
        public static Node/*!*/ ToYaml(ConversionStorage<MutableString>/*!*/ tosConversion, object self, [NotNull]RubyRepresenter/*!*/ rep) {
            return rep.Scalar(self, Protocols.ConvertToString(tosConversion, self));
        }

        [RubyMethod("taguri")]
        public static MutableString/*!*/ TagUri(object self) {
            return MutableString.CreateAscii("tag:yaml.org,2002:bool");
        }
    }

    [RubyModule(Extends = typeof(FalseClass))]
    public static class YamlFalseOps {
        [RubyMethod("to_yaml_node", RubyMethodAttributes.PrivateInstance)]
        public static Node/*!*/ ToYaml(ConversionStorage<MutableString>/*!*/ tosConversion, object self, [NotNull]RubyRepresenter/*!*/ rep) {
            return rep.Scalar(self, Protocols.ConvertToString(tosConversion, self));
        }

        [RubyMethod("taguri")]
        public static MutableString/*!*/ TagUri(object self) {
            return MutableString.CreateAscii("tag:yaml.org,2002:bool");
        }
    }

    [RubyModule(Extends = typeof(DynamicNull))]
    public static class YamlNilOps {
        [RubyMethod("to_yaml_node", RubyMethodAttributes.PrivateInstance)]
        public static Node/*!*/ ToYaml(object self, [NotNull]RubyRepresenter/*!*/ rep) {
            return rep.Scalar(self, null);
        }

        [RubyMethod("taguri")]
        public static MutableString/*!*/ TagUri(object self) {
            return MutableString.CreateAscii("tag:yaml.org,2002:null");
        }
    }

    [RubyClass(Extends = typeof(Node))]
    public static class YamlNodeOps {
        [RubyMethod("transform")]
        public static object Transform(RubyScope/*!*/ scope, Node/*!*/ self) {
            return new RubyConstructor(scope.GlobalScope, new SimpleNodeProvider(self)).GetData();
        }
    }
}

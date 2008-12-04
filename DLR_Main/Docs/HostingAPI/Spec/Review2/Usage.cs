// Should we move all DLR stuff in Microsoft.Scripting.Runtime and move hosting stuff to Microsoft.Scripting?
using Microsoft.Scripting.Hosting;

using System.Text;
using IronRuby;
using System; using Microsoft;

namespace LanguageAgnostic {

    static class Level1 {
        private static readonly ScriptEnvironment env = ScriptEnvironment.Create();

        public static void EarlyBound() {
            var scope = Ruby.CreateScope(env); // level 1 gate
            scope.Execute("puts 'foo'");
            scope = env.ExecuteFile("my.rb"); // returns Ruby scope
        }

        public static void RunSnippet() {
            var scope = new ScriptScope(env, "py");
            scope.Execute("def add5(x): return x + 5");
            var add5 = scope.GetVariable<Func<int, int>>("add5");
            int fifteen = add5(10);
        }

        /// my_file.py:
        /// x = 'Hello'
        /// print x
        public static void RunFile1() {
            var scope = new ScriptScope(env, "py");
            scope.IncludeFile("my_file.py2");
            object x;
            bool variableExists = scope.TryGetVariable("x", out x); // true
        }

        // TODO: Level II?

        /// Operations with objects.
        /// my_file.rb:
        /// def foo()
        ///     X + 'Hello'
        /// end
        public static void ObjectOperations() {
            var scope = new ScriptScope(env, "py");
            scope.SetVariable("X", "Hello");
            scope.IncludeFile("my_file.rb");

            object foo = scope.GetVariable("foo");

            // static operation (type):
            // the user expects string (note that MutableString is converterd to a CLR string)
            object result = scope.Operations.CallObject(foo);
            string str = scope.Operations.ConvertObjectTo<string>(result);
            bool b = str.StartsWith("Global x is Hello"); // true

            // dynamic operation (protocol):
            // the user expects something that has "capitalize" method
            object dstr = scope.Operations.CallObject(foo);
            object capitalize = scope.Operations.GetObjectMember(dstr, "capitalize");
            object capitalized = scope.Operations.CallObject(capitalize);
            b = (scope.Operations.ConvertObjectTo<string>(capitalized) == "Global X Is Hello");
        }

        public class MyNamespace {
            public string Hello(string name) {
                return "Hello " + name;
            }
        }

        /// Shows: executing a file that fetches a property bag created and published
        /// by the host for app programmability.
        /// foo.py:
        /// import some_namespace
        /// print some_namespace.Hello()
        public static void Import1() {
            var ns = new MyNamespace();
            var scope = new ScriptScope(env, "py");
            env.Globals.SetVariable("some_namespace", ns);
            scope.IncludeFile("foo.py");
        }
    }

    // multilingual modules, pre-compilation
    static class Level2 {
        private static ScriptEnvironment/*!*/ env = ScriptEnvironment.Create();

        // DLR console:
        public static void Multilingual() {
            SourceUnit command;
            object result;
            var scope = env.CreateScope(null);

            // execute commands in a loop:
            var py = env.GetEngine("py");
            command = SourceUnit.CreateSnippet(py, "x = 'foo'", SourceCodeKind.InteractiveCode);
            result = py.Execute(scope, command);
            System.Console.WriteLine(py.ObjectOperations.GetObjectCodeRepresentation(result));

            var rb = env.GetEngine("rb");
            command = SourceUnit.CreateSnippet(rb, "puts x", SourceCodeKind.InteractiveCode);
            result = rb.Execute(scope, command);
            System.Console.WriteLine(rb.ObjectOperations.GetObjectCodeRepresentation(result));
        }

        // MerlinWeb:
        public static void Compilation() {
            var engine = env.GetEngine("py");
            var compiledCode = engine.Compile(SourceUnit.CreateFileUnit(engine, "foo.py"));

            // on each request:
            compiledCode.Execute();
            var onLoad = compiledCode.Scope.GetVariable<OnLoadDelegate>("on_Load");
            onLoad();
        }

        public delegate string OnLoadDelegate();
    }

    // remoting
    static class Level3 {

        // executes a file within a remote environment;
        // note almost transparent remoting (host only needs to care when converting to a non-remotable object.
        public static void Remotable1(System.AppDomain appDomain) {
            using (ScriptEnvironment env = ScriptEnvironment.Create(appDomain)) {
                var engine = env.GetEngine("py");
                var scope = env.ExecuteFile("foo.py");
                var loader = scope.GetVariableAndWrap("on_load");
                var result = engine.ObjectOperations.CallObject(loader, "arg1", "arg2");

                object localResult;
                try {
                    localResult = engine.ObjectOperations.ConvertObjectTo<object>(result);
                } catch (System.Runtime.Serialization.SerializationException) {
                    // error
                }
            }
        }

        // RoR/PHP like web server
        public class RequestHandler : System.Web.IHttpHandler {
            public void ProcessRequest(System.Web.HttpContext context) {
                using (ScriptEnvironment env = ScriptEnvironment.Create()) {
                    env.ExecuteFile(context.Request.FilePath);
                }
            }

            public bool IsReusable {
                get { return true; }
            }
        }
    }
}

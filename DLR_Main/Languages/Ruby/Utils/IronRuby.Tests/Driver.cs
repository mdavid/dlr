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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using Microsoft.Scripting.Generation;
using Microsoft.Scripting.Hosting;
using IronRuby.Runtime;

namespace IronRuby.Tests {
    public class TestCase {
        public Action TestMethod { get; set; }
        public RubyCompatibility Compatibility { get; set; }
        public string Name { get; set; }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class RunAttribute : Attribute {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OptionsAttribute : Attribute {
        public RubyCompatibility Compatibility { get; set; }
    }

    public class TestRuntime {
        private readonly Driver/*!*/ _driver;
        private readonly string/*!*/ _testName;
        private readonly ScriptRuntime/*!*/ _env;
        private readonly ScriptEngine/*!*/ _engine;
        private readonly RubyContext/*!*/ _context;

        public string/*!*/ TestName { get { return _testName; } }
        public ScriptEngine/*!*/ Engine { get { return _engine; } }
        public ScriptRuntime/*!*/ ScriptRuntime { get { return _engine.Runtime; } }
        public RubyContext/*!*/ Context { get { return _context; } }

        public TestRuntime(Driver/*!*/ driver, TestCase/*!*/ testCase) {
            _driver = driver;
            _testName = testCase.Name;

            if (_driver.IsDebug) {
                Environment.SetEnvironmentVariable("DLR_AssembliesFileName", _testName);
            }

            var runtimeSetup = ScriptRuntimeSetup.ReadConfiguration();
            LanguageSetup langaugeSetup = null;
            foreach (var language in runtimeSetup.LanguageSetups) {
                if (language.TypeName == typeof(RubyContext).AssemblyQualifiedName) {
                    langaugeSetup = language;
                    break;
                }
            }

            // TODO: dynamic modules with symbols are not available in partial trust
            runtimeSetup.DebugMode = !driver.PartialTrust;
            langaugeSetup.Options["InterpretedMode"] = _driver.Interpret;
            langaugeSetup.Options["Verbosity"] = 2;
            langaugeSetup.Options["Compatibility"] = testCase.Compatibility;

            _env = Ruby.CreateRuntime(runtimeSetup);
            _engine = Ruby.GetEngine(_env);
            _context = Ruby.GetExecutionContext(_engine);
        }
    }

    public class Driver {

        private Tests _tests;

        private TestRuntime _testRuntime; 
        private static bool _interpretedMode;
        private static bool _excludeSelectedCases;
        private static bool _isDebug;
        private static bool _runTokenizerDriver;
        private static bool _displayList;
        private static bool _partialTrust;
        private static bool _interpret;
        private static bool _runPython;

        public TestRuntime TestRuntime {
            get { return _testRuntime; }
        }

        public bool IsDebug {
            get { return _isDebug; }
        }

        public bool InterpretedMode {
            get { return _interpretedMode; }
        }

        public bool PartialTrust {
            get { return _partialTrust; }
        }

        public bool Interpret {
            get { return _interpret; }
        }

        public bool RunPython {
            get { return _runPython; }
        }

        private static bool ParseArguments(List<string>/*!*/ args) {
            if (args.Contains("/help") || args.Contains("-?") || args.Contains("/?") || args.Contains("-help")) {
                Console.WriteLine("Run All Tests              : [-X:Interpret]");
                Console.WriteLine("Partial trust              : /partial");
                Console.WriteLine("Interpret                  : /interpret");
                Console.WriteLine("Run Python interop tests   : /py");
                Console.WriteLine("Run Specific Tests         : [/debug] [/exclude] [test_to_run ...]");
                Console.WriteLine("List Tests                 : /list");
                Console.WriteLine("Tokenizer baseline         : /tokenizer <target-dir> <sources-file>");
                Console.WriteLine("Productions dump           : /tokenizer /prod <target-dir> <sources-file>");
                Console.WriteLine("Benchmark                  : /tokenizer /bm <target-dir> <sources-file>");
            }

            if (args.Contains("/list")) {
                _displayList = true;
                return true;
            }

            if (args.Contains("/debug")) {
                args.Remove("/debug");
                _isDebug = true;
            }

            if (args.Contains("/partial")) {
                args.Remove("/partial");
                _partialTrust = true;
            }

            if (args.Contains("/interpret")) {
                args.Remove("/interpret");
                _interpret = true;
            }

            if (args.Contains("/py")) {
                args.Remove("/py");
                _runPython = true;
            }

            if (args.Contains("/exclude")) {
                _excludeSelectedCases = true;
                args.Remove("/exclude");
            }

            if (args.Contains("-X:Interpret")) {
                args.Remove("-X:Interpret");
                _interpretedMode = true;
            }

            if (args.Contains("/tokenizer")) {
                args.Remove("/tokenizer");
                _runTokenizerDriver = true;
            }

            return true;
        }

        public static void Main(string[]/*!*/ arguments) {
            List<string> args = new List<string>(arguments);
            if (args.Contains("/partial")) {
                Console.WriteLine("Running in partial trust");

                PermissionSet ps = CreatePermissionSetByName();
                AppDomainSetup setup = new AppDomainSetup();
                setup.ApplicationBase = Environment.CurrentDirectory;
                AppDomain domain = AppDomain.CreateDomain("Tests", null, setup, ps);

                Loader loader = new Loader(args);
                domain.DoCallBack(new CrossAppDomainDelegate(loader.Run));
                
                Environment.ExitCode = loader.ExitCode;
            } else {
                Environment.ExitCode = Run(args);
            }
        }

        public sealed class Loader : MarshalByRefObject {
            public int ExitCode;
            public readonly List<string>/*!*/ Args;

            public Loader(List<string>/*!*/ args) {
                Args = args;
            }

            public void Run() {
                ExitCode = Driver.Run(Args);
            }
        }

        private static PermissionSet/*!*/ CreatePermissionSetByName() {
            string name = "Internet";
            bool foundName = false;
            PermissionSet setIntersection = new PermissionSet(PermissionState.Unrestricted);

            // iterate over each policy level
            IEnumerator e = SecurityManager.PolicyHierarchy();
            while (e.MoveNext()) {
                PolicyLevel level = (PolicyLevel)e.Current;
                PermissionSet levelSet = level.GetNamedPermissionSet(name);
                if (levelSet != null) {
                    foundName = true;
                    setIntersection = setIntersection.Intersect(levelSet);
                }
            }

            if (setIntersection == null || !foundName) {
                setIntersection = new PermissionSet(PermissionState.None);
            } else {
                setIntersection = new NamedPermissionSet(name, setIntersection);
            }

            return setIntersection;
        }       

        public static int Run(List<string>/*!*/ args) {
            if (!ParseArguments(args)) {
                return -3;
            }

            int status = 0;

            if (_runTokenizerDriver) {
                TokenizerTestDriver driver = new TokenizerTestDriver(Ruby.GetExecutionContext(Ruby.CreateRuntime()));
                if (!driver.ParseArgs(args)) {
                    return -3;
                }

                status = driver.RunTests();
            } else {
                InitializeDomain();
                Driver driver = new Driver();

                if (Manual.TestCode.Trim().Length == 0) {
                    status = driver.RunUnitTests(args);
                } else {
                    driver.RunManualTest();

                    // for case the test is forgotten, this would fail the test suite:
                    status = -2;
                }
            }

            // return failure on bad filter (any real failures throw)
            return status;
        }

        private static void InitializeDomain() {
            if (_isDebug) {
                string _dumpDir = Path.Combine(Path.GetTempPath(), "RubyTests");

                if (Directory.Exists(_dumpDir)) {
                    Array.ForEach(Directory.GetFiles(_dumpDir), delegate(string file) {
                        try { File.Delete(Path.Combine(_dumpDir, file)); } catch { /* nop */ }
                    });
                } else {
                    Directory.CreateDirectory(_dumpDir);
                }
                    
                Console.WriteLine("Generating binaries to {0}", _dumpDir);

                Snippets.SetSaveAssemblies(true, _dumpDir);
            }
        }
        
        private void RunManualTest() {
            Console.WriteLine("Running hardcoded test case");
            
            int failureCount = 0;

            if (Manual.ParseOnly) {
                _testRuntime = new TestRuntime(this, new TestCase { Name = "<manual>" });
                Tests.GetRubyTokens(_testRuntime.Context, new LoggingErrorSink(false), Manual.TestCode, !Manual.DumpReductions, Manual.DumpReductions);                
            } else {
                try {

                    for (int i = 0; i < Manual.RequiredFiles.Length; i += 2) {
                        File.CreateText(Manual.RequiredFiles[i]).WriteLine(Manual.RequiredFiles[i + 1]);
                    }

                    _tests = new Tests(this);
                    RunTestCase(new TestCase() {
                        Name = "$manual$",
                        TestMethod = () => _tests.CompilerTest(Manual.TestCode),
                    }, ref failureCount);

                } finally {
                    for (int i = 0; i < Manual.RequiredFiles.Length; i += 2) {
                        try {
                            File.Delete(Manual.RequiredFiles[i]);
                        } catch {
                            // nop
                        }
                    }
                }
            }
        }

        private int RunUnitTests(List<string>/*!*/ largs) {

            int failureCount = 0;

            _tests = new Tests(this);
            
            if (_displayList) {
                for (int i = 0; i < _tests.TestMethods.Length; i++) {
                    Console.WriteLine(_tests.TestMethods[i].Method.Name);
                }
                return -1;
            }

            // check whether there is a preselected case:
            IList<TestCase> selectedCases = new List<TestCase>();

            foreach (var m in _tests.TestMethods) {
                if (m.Method.IsDefined(typeof(RunAttribute), false)) {
                    AddTestCases(selectedCases, m);
                }
            }

            if (selectedCases.Count == 0 && largs.Count > 0) {
                foreach (var m in _tests.TestMethods) {
                    bool caseIsSpecified = largs.Contains(m.Method.Name);
                    if ((caseIsSpecified && !_excludeSelectedCases) || (!caseIsSpecified && _excludeSelectedCases)) {
                        AddTestCases(selectedCases, m);
                    }
                }
            } else if (selectedCases.Count > 0 && largs.Count > 0) {
                Console.WriteLine("Arguments overrided by Run attribute.");
            } else if (selectedCases.Count == 0 && largs.Count == 0) {
                foreach (var m in _tests.TestMethods) {
                    AddTestCases(selectedCases, m);
                }
            }

            foreach (TestCase testCase in selectedCases) {
                RunTestCase(testCase, ref failureCount);
            }

            return failureCount;
        }

        private void AddTestCases(IList<TestCase>/*!*/ cases, Action/*!*/ testMethod) {
            var attrs = testMethod.Method.GetCustomAttributes(typeof(OptionsAttribute), false);
            if (attrs.Length > 0) {
                foreach (OptionsAttribute options in attrs) {
                    cases.Add(new TestCase {
                        Name = testMethod.Method.Name,
                        TestMethod = testMethod,
                        Compatibility = options.Compatibility,
                    });
                }
            } else {
                cases.Add(new TestCase {
                    Name = testMethod.Method.Name,
                    TestMethod = testMethod,
                });
            }
        }

        private void RunTestCase(TestCase/*!*/ testCase, ref int failedCount) {
            _testRuntime = new TestRuntime(this, testCase);

            Console.WriteLine("Executing {0}", testCase.Name);

            try {
                testCase.TestMethod();
            } catch (Exception e) {
                Console.Error.WriteLine(e);
                failedCount++;
            } finally {
                Snippets.SaveAndVerifyAssemblies();
            }
        }

        internal string/*!*/ MakeTempDir() {
            string dir = Path.Combine(Path.GetTempPath(), _testRuntime.TestName);
            Directory.CreateDirectory(dir);
            return dir;
        }
    }
}

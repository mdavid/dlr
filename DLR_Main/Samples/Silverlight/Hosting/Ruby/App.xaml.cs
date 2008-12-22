using System; using Microsoft;
using System.Collections.Generic;
using Microsoft.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Scripting.Silverlight;
using Microsoft.Scripting.Hosting;
using IronRuby;

namespace SilverlightHostingRuby {

    class RubyEngine {
        private ScriptEngine _engine;

        public RubyEngine() {
            var setup = CreateRuntimeSetup();
            setup.Options["InterpretedMode"] = true;
            var runtime = new ScriptRuntime(setup);
            _engine = Ruby.GetEngine(runtime);
        }

        public object Execute(string code) {
            return _engine.Execute(code);
        }

        private static ScriptRuntimeSetup CreateRuntimeSetup() {
            ScriptRuntimeSetup setup = Configuration.TryParseFile();
            if (setup == null) {
                setup = Configuration.LoadFromAssemblies(Package.GetManifestAssemblies());
            }
            setup.HostType = typeof(BrowserScriptHost);
            return setup;
        }
    }

    public partial class App : Application {

        public App() {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e) {
            this.RootVisual = new Page();
        }

        private void Application_Exit(object sender, EventArgs e) {

        }
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e) {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached) {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }
        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e) {
            try {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight 2 Application " + errorMsg + "\");");
            } catch (Exception) {
            }
        }
    }
}

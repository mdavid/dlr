using Microsoft.Scripting.Hosting;
using System.IO;

namespace IronRuby {
    public static class Ruby {
        public static ScriptScope/*!*/ CreateScope(ScriptEnvironment/*!*/ environment) { return null; }
        public static ScriptEngine/*!*/ GetEngine(ScriptEnvironment/*!*/ environment) { return null; }
    }

    public static class RubyScriptEnvironmentExtensions {
        public static ScriptEngine/*!*/ GetRubyEngine(this ScriptEnvironment/*!*/ environment) { return null; }
    }
}


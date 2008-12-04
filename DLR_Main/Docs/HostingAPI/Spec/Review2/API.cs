namespace Microsoft.Scripting.Hosting {
	using System; using Microsoft;
	using System.Runtime.CompilerServices;
using Microsoft.Runtime.CompilerServices;

    using System.Runtime.Remoting;
    using System.IO;
    using System.Collections.Generic;
    using System.Text;

    public class ScriptScope : IRemotable {
        // Scope/*!*/ _scope
        // ObjectOperations/*!*/ _operations
        // ScriptEngine/*!*/ _engine;

        // TODO:??? 
        // equivalent of env.CreateScope just for better navigation... could also be ScriptScope.Create(env, langId)
        //
        // don't need to provide lang id (could be null) but it's not usual case -> don't need overload (in fact having overload is worse)
        // null means invariant context/engine
        public ScriptScope(ScriptEnvironment/*!*/ environment, string languageId) { }

        // execution:
        public object Execute(string/*!*/ code) { return null; }
        public T Execute<T>(string/*!*/ code) { return default(T); }

        // includes a file into the script's scope (i.e. executes it) 
        // the extension of the path is not significant 
        // (it doesn't need throw an exception if the extension is of other language because parser blows anyway)
        public object IncludeFile(string/*!*/ path) { return null; }
        
        public object GetVariable(string/*!*/ name) { return null; }
        public T GetVariable<T>(string/*!*/ name) { return default(T); }
        public ObjectHandle GetVariableAndWrap(string/*!*/ name) { return null; }

        public bool TryGetVariable(string/*!*/ name, out object value) { value = null; return false; }
        public bool TryGetVariable<T>(string/*!*/ name, out T value) { value = default(T); return false; }
        public bool TryGetVariableAndWrap(string/*!*/ name, out ObjectHandle value) { value = null; return false; }
        
        public void SetVariable(string/*!*/ name, object value) { }
        public void SetVariable(string/*!*/ name, ObjectHandle value) { }
        
        public void RemoveVariable(string/*!*/ name) { }
	    
        public void Clear() { }

        public IList<string>/*!*/ GetVariableNames() { return null; }

        public ObjectOperations/*!*/ Operations { get { return null; } }

        // a gate to the next level:
        public ScriptEngine/*!*/ LanguageEngine { get { return null; }}
        public bool IsRemote { get { return false; } }
    }

    // add Stream?
    // add example with open dialog
    // default language in Script -- 
    // ScriptScope has default language? new Script("languge") or new RubyScript() (get variable, As<T>, ...)
    // ScriptEnvironment.ExecuteFile() -> create a new ScriptScope
	// new Script() -> maybe create a new ScriptEnvironment

	// multiple per app-domain:
    // the only subclass: LocalScriptEnvironment
    public class ScriptEnvironment : IDisposable, IRemotable {
		// creates a new local environment:
		public static ScriptEnvironment/*!*/ Create() { return null; }
		public static ScriptEnvironment/*!*/ Create(AppDomain/*!*/ domain) { return null; }

        public ScriptScope/*!*/ Globals { get; set; }
        public ScriptHost/*!*/ Host { get { return null; } }
        
        public ScriptScope/*!*/ ExecuteFile(string/*!*/ path) { return null; }

        public ScriptEngine/*!*/ GetEngine(string/*!*/ languageId) { return null; }
        public ScriptEngine/*!*/ GetEngineByFileExtension(string/*!*/ extension) { return null; }

        // TODO: bool return already loaded?
        public void LoadReference(string/*!*/ path) { }

        public Stream/*!*/ OutputStream { get; set; }
        public Stream/*!*/ InputStream { get; set; }
        public Stream/*!*/ ErrorStream { get; set; }
        
        public IList<string>/*!*/ GetRegisteredFileExtensions() { return null; }
        public IList<string>/*!*/ GetRegisteredLanguageIdentifiers() { return null; }

        public void Close() { }
        public bool IsRemote { get { return false; } }
        void IDisposable.Dispose() { } // notifies engines about shutdown/end-of-request

        // TODO: options and configuration
        // public ScriptEnvironmentOptions GlobalOptions { get; set; }
    }

    public class ScriptHost : IRemotable {
        // source units:
        public SourceUnit TryGetSourceFileUnit(ScriptEngine/*!*/ engine, string/*!*/ path, Encoding encoding) { return null; }
        public SourceUnit TryResolveSourceFileUnit(string/*!*/ name) { return null; }

        // TODO:??
        // public IList<string>/*!*/ GetSourceFileNames(string/*!*/ mask, string/*!*/ searchPattern);

        public bool IsRemote { get { return false; } }
    }

    // created as needed, an engine has a default context
    // performes dynamic operations on specified (possibly remote) object 
    // caches dynamic sites
    // the only subclass: LocalObjectOperations
    public class ObjectOperations : IRemotable {
        // private DynamicSiteCache;
        // private LanguageContext! or ScriptEngine!;

        public ScriptEngine/*!*/ LanguageEngine { get { return null; } }
        public bool IsRemote { get { return false; } }

        // -- Object overloads ------------------------------

        // implicit language:
        public object/*!*/ CallObject<A1>(object obj, A1 arg1) { return null; }
        public object/*!*/ CallObject<A1, A2>(object obj, A1 arg1, A2 arg2) { return null; }
        public object/*!*/ CallObject<A1, A2, A3>(object obj, A1 arg1, A2 arg2, A3 arg3) { return null; }
        public object/*!*/ CallObject<A1, A2, A3, A4>(object obj, A1 arg1, A2 arg2, A3 arg3, A4 arg4) { return null; }
        public object/*!*/ CallObject<A1, A2, A3, A4, A5>(object obj, A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5) { return null; }
        public object/*!*/ CallObject<A1, A2, A3, A4, A5, A6>(object obj, A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6) { return null; }
        public object/*!*/ CallObject<A1, A2, A3, A4, A5, A6, A7>(object obj, A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6, A7 arg7) { return null; }
        public object/*!*/ CallObject(object obj, params object[]/*!*/ parameters) { return null; }

        public void SetObjectMember(object obj, string/*!*/ name, object/*!*/ value) { }
        public void SetObjectMember<T>(object obj, string/*!*/ name, T value) { }

        public object/*!*/ GetObjectMember(object obj, string/*!*/ name) { return null; }
        public T GetObjectMember<T>(object obj, string/*!*/ name) { return default(T); }

        public bool RemoveObjectMember(object obj, string/*!*/ name) { return false; }

        public T ConvertObjectTo<T>(object obj) { return default(T); }

        // TODO: more operations

        // reflection API:
        public string/*!*/ GetObjectCodeRepresentation(object obj) { return null; }
        public IList<string>/*!*/ GetObjectMemberNames(object obj) { return null; }
        public string/*!*/ GetObjectDocumentation(object obj) { return null; }

        // -- Handle overloads ------------------------------

        // implicit language:
        public ObjectHandle/*!*/ CallObject<A1>(ObjectHandle/*!*/ obj, A1 arg1) { return null; }
        public ObjectHandle/*!*/ CallObject<A1, A2>(ObjectHandle/*!*/ obj, A1 arg1, A2 arg2) { return null; }
        public ObjectHandle/*!*/ CallObject<A1, A2, A3>(ObjectHandle/*!*/ obj, A1 arg1, A2 arg2, A3 arg3) { return null; }
        public ObjectHandle/*!*/ CallObject<A1, A2, A3, A4>(ObjectHandle/*!*/ obj, A1 arg1, A2 arg2, A3 arg3, A4 arg4) { return null; }
        public ObjectHandle/*!*/ CallObject<A1, A2, A3, A4, A5>(ObjectHandle/*!*/ obj, A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5) { return null; }
        public ObjectHandle/*!*/ CallObject<A1, A2, A3, A4, A5, A6>(ObjectHandle/*!*/ obj, A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6) { return null; }
        public ObjectHandle/*!*/ CallObject<A1, A2, A3, A4, A5, A6, A7>(ObjectHandle/*!*/ obj, A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6, A7 arg7) { return null; }
        public ObjectHandle/*!*/ CallObject(ObjectHandle/*!*/ obj, params ObjectHandle/*!*/[]/*!*/ parameters) { return null; }

        public void SetObjectMember(ObjectHandle/*!*/ obj, string/*!*/ name, ObjectHandle/*!*/ value) { }
        public void SetObjectMember<T>(ObjectHandle/*!*/ obj, string/*!*/ name, T value) { }

        public object/*!*/ GetObjectMember(ObjectHandle/*!*/ obj, string/*!*/ name) { return null; }
        public T GetObjectMember<T>(ObjectHandle/*!*/ obj, string/*!*/ name) { return default(T); }

        public bool RemoveObjectMember(ObjectHandle/*!*/ obj, string/*!*/ name) { return false; }

        public ObjectHandle/*!*/ ConvertObjectTo<T>(ObjectHandle/*!*/ obj) { return null; }
        public T UnwrapObject<T>(ObjectHandle/*!*/ obj) { return default(T); }

        // TODO: more operations

        // reflection API:
        public string/*!*/ GetObjectCodeRepresentation(ObjectHandle obj) { return null; }
        public IList<string>/*!*/ GetObjectMemberNames(ObjectHandle obj) { return null; }
        public string/*!*/ GetObjectDocumentation(ObjectHandle obj) { return null; }
    }

    // singleton per ScriptEnvironment
    // the only subclass: LocalScriptEngine
    public class ScriptEngine : IRemotable {
        // private LanguageContext _language (null for LocalScriptEngine)

        public ScriptEnvironment/*!*/ Environment { get { return null; } }
        
        // script context:
        public ObjectOperations/*!*/ ObjectOperations { get { return null; } }
        public ObjectOperations/*!*/ CreateObjectOperations() { return null; } // TODO: rename

        public ScriptScope/*!*/ CreateScope() { return null; }
        public ScriptScope/*!*/ CreateScope(IAttributesCollection/*!*/ storage) { return null; }

        // services (TODO: more services like CommandLine, ...):
        public TokenCategorizer/*!*/ GetTokenCategorizer() { return null; }
        public OptionsParser/*!*/ GetOptionsParser() { return null; }

        public ServiceType/*!*/ GetService<ServiceType>(params object[]/*!*/ args) { return default(ServiceType); }
        
        // compilation:
        public CompiledCode/*!*/ Compile(SourceUnit/*!*/ sourceUnit) { return null; }
        public CompiledCode/*!*/ Compile(System.CodeDom.CodeMemberMethod/*!*/ code) { return null; }

        // execution:
        public object Execute(ScriptScope/*!*/ scope, string/*!*/ code) { return null; }
        public object Execute(ScriptScope/*!*/ scope, SourceUnit/*!*/ code) { return null; }
        public ObjectHandle ExecuteAndWrap(ScriptScope/*!*/ scope, string/*!*/ code) { return null; }
        public ObjectHandle ExecuteAndWrap(ScriptScope/*!*/ scope, SourceUnit/*!*/ code) { return null; }
        public ScriptScope/*!*/ ExecuteFile(SourceUnit/*!*/ sourceUnit) { return null; }
        public int ExecuteProgram(SourceUnit/*!*/ sourceUnit) { return 0; }
        
        // TODO: this sounds weird - EvaluateAs sounds better
        public T Execute<T>(ScriptScope/*!*/ scope, string/*!*/ code) { return default(T); }
        public T Execute<T>(ScriptScope/*!*/ scope, SourceUnit/*!*/ code) { return default(T); }

        public string/*!*/ FormatException(Exception/*!*/ exception) { return null; }
        public void SetSourceUnitSearchPaths(string[] paths) { }
        public void Shutdown() { }
        
        public string/*!*/ LanguageDisplayName { get { return null; } }
        public string/*!*/ VersionString { get { return null; } }
        public IList<string>/*!*/ GetRegisteredIdentifiers() { return null; }
        public IList<string>/*!*/ GetRegisteredExtensions() { return null; }

        public ScriptScope/*!*/ GetScopeBySourceUnit(SourceUnit/*!*/ sourceUnit) { return null; }

        // TODO: options/configuration
        // public EngineOptions Options { get; }
        
        public bool IsRemote { get { return false; } }
	}
	
	/*
	// impl detail: 
	// ensures that the scopes's members are accessible from a dynamic language
	// CodeContext overrides the default engine (Python gets an object from Ruby engine via HAPI and gets its member)
	public sealed class {Local}ScriptScope : ScriptScope {
		[SpecialName]
		public object op_GetMember(CodeContext context, object name) { return null; }
		
		[SpecialName]
		public void op_SetMember(CodeContext context, object name, object value) {}
	}
	*/
	
	
	public class CompiledCode : IRemotable {
        // private ScriptScope!
        // lazy-init
        public ScriptScope/*!*/ Scope { get { return null; } } // TODO: DefaultScope? 

        public object Execute() { return null; }
        public ObjectHandle ExecuteAndWrap() { return null; }
        public T Execute<T>() { return default(T); }

        public object Execute(ScriptScope/*!*/ scope) { return null;  }
        public ObjectHandle ExecuteAndWrap(ScriptScope/*!*/ scope) { return null; }
        public T Execute<T>(ScriptScope/*!*/ scope) { return default(T); }

        public bool IsRemote { get { return false; } }
	}
	
	public interface IRemotable {
		bool IsRemote { get; }
	}

	/// <summary>
    /// Defines a kind of the source code. The parser sets its initial state accordingly.
    /// </summary>
    public enum SourceCodeKind {
        /// <summary>
        /// The kind of code is unknown. Parser should choose the least restrictive initial state (i.e. initial non-terminal).
        /// </summary>
        Default,

        /// <summary>
        /// The code is an expression.
        /// </summary>
        Expression,

        /// <summary>
        /// The code is a sequence of statements.
        /// </summary>
        Statements,

        /// <summary>
        /// The code is a single statement.
        /// </summary>
        SingleStatement,

        /// <summary>
        /// The code is a content of a file.
        /// </summary>
        File,

        /// <summary>
        /// The code is an interactive command.
        /// </summary>
        InteractiveCode,
    }

    public enum SourceCodeProperties {
        None,

        /// <summary>
        /// Source code is already invalid and no suffix can make it syntactically correct.
        /// </summary>
        IsInvalid,

        /// <summary>
        /// Last token is incomplete. Source code can still be completed correctly.
        /// </summary>
        IsIncompleteToken,

        /// <summary>
        /// Last statement is incomplete. Source code can still be completed correctly.
        /// </summary>
        IsIncompleteStatement,

        /// <summary>
        /// String represents an empty statement/expression.
        /// </summary>
        IsEmpty
    }

    public class SourceUnit : MarshalByRefObject {
        // TODO: rename to Identifier?
        public string Id { get { return null; } } 

        public SourceCodeKind Kind { get { return default(SourceCodeKind); } }
        public ScriptEngine Engine { get { return null; } }

        public SourceCodeProperties? CodeProperties { get; set; }
        public bool IsVisibleToDebugger { get; set; }
        public bool DisableLineFeedLineSeparator { get; set; }
        public Action<SourceUnit> ContentReloader { get; set; }
        public bool IsReloadable { get; set; }

        public static SourceUnit/*!*/ Create(ScriptEngine/*!*/ engine, SourceContentProvider/*!*/ contentProvider, string id, SourceCodeKind kind) { return null; }
        
        public static SourceUnit/*!*/ CreateSnippet(ScriptEngine/*!*/ engine, string/*!*/ code) { return null; }
        public static SourceUnit/*!*/ CreateSnippet(ScriptEngine/*!*/ engine, string/*!*/ code, SourceCodeKind kind) { return null; }
        public static SourceUnit/*!*/ CreateSnippet(ScriptEngine/*!*/ engine, string/*!*/ code, string/*!*/ id) { return null; }
        public static SourceUnit/*!*/ CreateSnippet(ScriptEngine/*!*/ engine, string/*!*/ code, string/*!*/ id, SourceCodeKind kind) { return null; }

        public static SourceUnit/*!*/ CreateFileUnit(ScriptEngine/*!*/ engine, string/*!*/ path) { return null; }
        public static SourceUnit/*!*/ CreateFileUnit(ScriptEngine/*!*/ engine, string/*!*/ path, System.Text.Encoding encoding) { return null; }
        public static SourceUnit/*!*/ CreateFileUnit(ScriptEngine/*!*/ engine, string/*!*/ path, string/*!*/ content) { return null; }
        public static SourceUnit/*!*/ CreateFileUnit(ScriptEngine/*!*/ engine, string/*!*/ path, string/*!*/ content, Action<SourceUnit> contentReloader) { return null; }

        public SourceUnitReader/*!*/ GetReader() { return null; }
        public void Reload() { }
        public void SetContent(string/*!*/ content) { }

        public string/*!*/ GetCode() { return null; }
        public string/*!*/ GetCodeLine(int line) { return null; }
        public IList<string>/*!*/ GetCodeLines(int start, int count) { return null; }

        public SourceSpan MapLine(SourceSpan span) { return default(SourceSpan); }
        public SourceLocation MapLine(SourceLocation loc) { return default(SourceLocation); }

        public int MapLine(int line) { return 0; }
        public string/*!*/ GetSymbolDocument(int line) { return null; }
        public void SetLineMapping(IList<KeyValuePair<int, int>>/*!*/ lineMap) { }
        public void SetDocumentMapping(IList<KeyValuePair<int, string>>/*!*/ fileMap) { }
    }

    [Serializable]
    public abstract class SourceContentProvider {
        public abstract TextReader/*!*/ GetReader();

        // Reloads the content from its source if it could be out of sync.
        public void Reload() { } 
    }

    [Serializable]
    public class SourceFileContentProvider : SourceContentProvider {
        public override TextReader/*!*/ GetReader() { return null; }

        // Reloads the content from its source if it could be out of sync.
        protected Stream/*!*/ OpenStream() { return null; }
    }

    [Serializable]
    public class SourceStringContentProvider : SourceContentProvider {
        public override TextReader/*!*/ GetReader() { return null; } 
    }

    public sealed class SourceUnitReader : TextReader {
        // TODO
    }
	
	public class TokenCategorizer : IRemotable {
		// TODO...
        public bool IsRemote { get { return false; } }
	}

    public class OptionsParser : IRemotable {
        // TODO...
        public bool IsRemote { get { return false; } }
    }

    public class ErrorSink : MarshalByRefObject {
        public int FatalErrorCount { get { return 0; } }
        public int ErrorCount { get { return 0; } }
        public int WarningCount { get { return 0; } }
        public bool AnyError { get { return false; } } 

        public ErrorSink() { }

        protected void CountError(Severity severity) { }
        public void ClearCounters() { }

        public void Add(SourceUnit sourceUnit, string message, SourceSpan span, int errorCode, Severity severity) { }

        public SyntaxErrorException Add(SyntaxErrorException/*!*/ exception) { return null;  } 
    }

    public enum Severity {
        Ignore,
        Warning,
        Error,
        FatalError,
    }

    // Microsoft.Scripting:
    public class SyntaxErrorException : Exception { }
    public interface IAttributesCollection { } 
    public struct SourceSpan { }
    public struct SourceLocation { }
}

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

#if CODEPLEX_40
using System;
#else
using System; using Microsoft;
#endif
using System.Collections.Generic;
using System.Diagnostics;
#if CODEPLEX_40
using System.Dynamic;
#else
#endif
using System.Text;
using Microsoft.Scripting;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Utils;
using IronRuby.Compiler.Ast;
using IronRuby.Runtime;
using IronRuby.Builtins;

namespace IronRuby.Compiler {

    public interface ILexicalVariableResolver {
        bool IsLocalVariable(string/*!*/ identifier);
    }

    internal sealed class DummyVariableResolver : ILexicalVariableResolver {
        public static readonly ILexicalVariableResolver AllVariableNames = new DummyVariableResolver(true);
        public static readonly ILexicalVariableResolver AllMethodNames = new DummyVariableResolver(false);
        private readonly bool _isVariable;

        private DummyVariableResolver(bool isVariable) {
            _isVariable = isVariable;
        }

        public bool IsLocalVariable(string/*!*/ identifier) {
            return _isVariable;
        }
    }

    // The non-autogenerated part of the Parser class.
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
    public partial class Parser : ILexicalVariableResolver {
        internal sealed class InternalSyntaxError : Exception {
        }

        private int _inSingletonMethodDefinition = 0;
        private int _inInstanceMethodDefinition = 0;

        private SourceUnitTree _ast;
        private List<Initializer> _initializers; // lazy

        private Stack<LexicalScope>/*!*/ _lexicalScopes = new Stack<LexicalScope>();
        private SourceUnit _sourceUnit;
        private readonly Tokenizer/*!*/ _tokenizer;
        private Action<Tokens, SourceSpan> _tokenSink;
        private int _generatedNameId;

        // current encoding (used for __ENCODING__ pseudo-constant, literal string, symbol, regex encodings):
        internal RubyEncoding/*!*/ Encoding {
            get { return _tokenizer.Encoding; }
        }

        private bool InMethod {
            get { return _inSingletonMethodDefinition > 0 || _inInstanceMethodDefinition > 0; }
        }

        public Tokenizer/*!*/ Tokenizer {
            get { return _tokenizer; }
        }

        public ErrorSink/*!*/ ErrorSink {
            get { return _tokenizer.ErrorSink; }
        }

        public Action<Tokens, SourceSpan> TokenSink {
            get { return _tokenSink; }
            set { _tokenSink = value; }
        }

        private SourceSpan GetTokenSpan() {
            return _tokenizer.TokenSpan;
        }

        private TokenValue GetTokenValue() {
            return _tokenizer.TokenValue;
        }

        private int GetNextToken() {
            Tokens token = _tokenizer.GetNextToken();
            if (_tokenSink != null) {
                _tokenSink(token, _tokenizer.TokenSpan);
            }
            return (int)token;
        }

        private void ReportSyntaxError(string message) {
            ErrorSink.Add(_sourceUnit, message, GetTokenSpan(), -1, Severity.FatalError);
            throw new InternalSyntaxError();
        }

        internal void ReportSyntaxError(ErrorInfo error) {
            ErrorSink.Add(_sourceUnit, error.GetMessage(), GetTokenSpan(), error.Code, Severity.FatalError);
            throw new InternalSyntaxError();
        }

        private string/*!*/ GenerateErrorLocalName() {
            return "error#" + _generatedNameId++;
        }

        private string/*!*/ GenerateErrorConstantName() {
            return "Error#" + _generatedNameId++;
        }

        public Parser() 
            : this(ErrorSink.Default) {
        }

        public Parser(ErrorSink/*!*/ errorSink) {
            _tokenizer = new Tokenizer(false, this) { 
                ErrorSink = errorSink 
            };
            InitializeTables();
        }

        public SourceUnitTree Parse(SourceUnit/*!*/ sourceUnit, RubyCompilerOptions/*!*/ options, ErrorSink/*!*/ errorSink) {
            Assert.NotNull(sourceUnit, options, errorSink);

            ErrorCounter counter = new ErrorCounter(errorSink);
            _tokenizer.ErrorSink = counter;
            _tokenizer.Compatibility = options.Compatibility;

            _lexicalScopes.Clear();

            EnterScope(CreateTopScope(options.LocalNames));

            using (SourceCodeReader reader = sourceUnit.GetReader()) {
                _sourceUnit = sourceUnit;
                _tokenizer.Initialize(null, reader, sourceUnit, options.InitialLocation);

                // Default encoding when hosted (ignore KCODE, we are reading from Unicode buffer):
                _tokenizer.Encoding = (reader.Encoding != null) ? RubyEncoding.GetRubyEncoding(reader.Encoding) : RubyEncoding.UTF8;
                _tokenizer.AllowNonAsciiIdentifiers = _tokenizer.Encoding != RubyEncoding.Binary;
                
                try {
                    Parse();
                    LeaveScope();
                } catch (InternalSyntaxError) {
                    _ast = null;
                    _lexicalScopes.Clear();
                } finally {
                    ScriptCodeParseResult props;
                    if (counter.AnyError) {
                        _ast = null;

                        if (_tokenizer.UnterminatedToken) {
                            props = ScriptCodeParseResult.IncompleteToken;
                        } else if (_tokenizer.EndOfFileReached) {
                            props = ScriptCodeParseResult.IncompleteStatement;
                        } else {
                            props = ScriptCodeParseResult.Invalid;
                        }
                        
                    } else {
                        props = ScriptCodeParseResult.Complete;
                    }

                    sourceUnit.CodeProperties = props;
                }

                return _ast;
            }
        }

        // Top level scope is created for top level code. 
        // Variables defined outside of compilation unit (we are compiling eval) are stored in "outer scope", 
        // to which the top level scope is nested in such case.
        private static LexicalScope/*!*/ CreateTopScope(List<string> localVariableNames) {
            LexicalScope outer;
            if (localVariableNames != null) {
                outer = new RuntimeLexicalScope(localVariableNames);
            } else {
                outer = null;
            }

            return new TopLexicalScope(outer);
        }

        private LocalVariable/*!*/ DefineParameter(string/*!*/ name, SourceSpan location) {
            // we are in a method:
            Debug.Assert(CurrentScope.OuterScope == null);

            LocalVariable variable;
            if (CurrentScope.TryGetValue(name, out variable)) {
                _tokenizer.ReportError(Errors.DuplicateParameterName);
                return variable;
            }

            return CurrentScope.AddVariable(name, location);
        }

        private Initializer/*!*/ AddInitializer(Initializer/*!*/ initializer) {
            if (_initializers == null) {
                _initializers = new List<Initializer>();
            }

            _initializers.Add(initializer);

            return initializer;
        }
        
        private SourceSpan MergeLocations(SourceSpan start, SourceSpan end) {
            Debug.Assert(start.IsValid && end.IsValid);

            return new SourceSpan(start.Start, end.End);
        }

        private LexicalScope/*!*/ EnterScope(LexicalScope/*!*/ scope) {
            Assert.NotNull(scope);
            _lexicalScopes.Push(scope);
            return scope;
        }

        /// <summary>
        /// Block scope.
        /// </summary>
        private LexicalScope EnterNestedScope() {
            LexicalScope result = new BlockLexicalScope(CurrentScope);
            _lexicalScopes.Push(result);
            return result;
        }

        /// <summary>
        /// for-loop scope.
        /// </summary>
        private LexicalScope EnterPaddingScope() {
            LexicalScope result = new PaddingLexicalScope(CurrentScope);
            _lexicalScopes.Push(result);
            return result;
        }

        /// <summary>
        /// Method, module and source unit scopes.
        /// </summary>
        private LexicalScope EnterTopScope() {
            LexicalScope result = new TopLexicalScope(null);
            _lexicalScopes.Push(result);
            return result;
        }

        private LexicalScope LeaveScope() {
            return _lexicalScopes.Pop();
        }

        public LexicalScope CurrentScope {
            get {
                Debug.Assert(_lexicalScopes.Count > 0);
                return _lexicalScopes.Peek();
            }
        }

        public bool IsLocalVariable(string/*!*/ identifier) {
            return CurrentScope.ResolveVariable(identifier) != null;
        }

        private Expression/*!*/ ToCondition(Expression/*!*/ expression) {            
            return expression.ToCondition(CurrentScope);
        }

        private Body/*!*/ MakeBody(Statements/*!*/ statements, List<RescueClause> rescueClauses, ElseIfClause elseIf,
            SourceSpan elseIfLocation, Statements ensureStatements, SourceSpan location) {
            Debug.Assert(elseIf == null || elseIf.Condition == null);

            if (elseIf != null && rescueClauses == null) {
                ErrorSink.Add(_sourceUnit, "else without rescue is useless", elseIfLocation, -1, Severity.Warning);
            }

            return new Body(
                statements,
                rescueClauses,
                (elseIf != null) ? elseIf.Statements : null,
                ensureStatements,
                location
            );
        }

        // __FILE__
        internal Expression/*!*/ GetCurrentFileExpression(SourceSpan location) {
            if (_sourceUnit.Path == null && _sourceUnit.Kind == SourceCodeKind.Statements) {
                return new StringLiteral("-e", location);
            } else if (_sourceUnit.Path == null) {
                return new StringLiteral("(eval)", location);
            } else {
                return new StringLiteral(_sourceUnit.Path, location);
            }
        }

        internal LeftValue/*!*/ CannotAssignError(string/*!*/ constantName, SourceSpan location) {
            Tokenizer.ReportError(Errors.CannotAssignTo, constantName);
            return CurrentScope.ResolveOrAddVariable(GenerateErrorLocalName(), location);
        }

        private void MatchReferenceReadOnlyError(RegexMatchReference/*!*/ matchRef) {
            Tokenizer.ReportError(Errors.MatchGroupReferenceReadOnly, matchRef.VariableName);
        }

        private AliasStatement/*!*/ MakeGlobalAlias(string/*!*/ newVar, string/*!*/ existingVar, SourceSpan location) {
            return new AliasStatement(false, newVar, existingVar, location);
        }

        private Expression/*!*/ MakeGlobalAlias(string/*!*/ newVar, RegexMatchReference/*!*/ existingVar, SourceSpan location) {
            if (existingVar.CanAlias) {
                return new AliasStatement(false, newVar, existingVar.VariableName, location);
            } else {
                _tokenizer.ReportError(Errors.CannotAliasGroupMatchVariable);
                return new ErrorExpression(location);
            }
        }

        private AliasStatement/*!*/ MakeGlobalAlias(RegexMatchReference/*!*/ newVar, string/*!*/ existingVar, SourceSpan location) {
            return new AliasStatement(false, newVar.VariableName, existingVar, location);
        }

        private Expression/*!*/ MakeGlobalAlias(RegexMatchReference/*!*/ newVar, RegexMatchReference/*!*/ existingVar, SourceSpan location) {
            if (existingVar.CanAlias) {
                return new AliasStatement(false, newVar.VariableName, existingVar.VariableName, location);
            } else {
                _tokenizer.ReportError(Errors.CannotAliasGroupMatchVariable);
                return new ErrorExpression(location);
            }
        }

        private List<T>/*!*/ MakeListAddOpt<T>(T item) {
            List<T> result = new List<T>();
            if (item != null) {
                result.Add(item);
            }
            return result;
        }

        // BlockExpression behaves like an expression, so we don't need to create one that comprises of a single expression:
        private Expression/*!*/ MakeBlockExpression(Statements/*!*/ statements, SourceSpan location) {
            if (statements.Count == 0) {
                return BlockExpression.Empty;
            } else if (statements.Count == 1) {
                return statements.First;
            } else {
                return new BlockExpression(statements, location);
            }
        }

        private IfExpression/*!*/ MakeIfExpression(Expression/*!*/ condition, Statements/*!*/ body, List<ElseIfClause>/*!*/ elseIfClauses, SourceSpan location) {
            // last else-if/else clause is the first one in the list:            
            elseIfClauses.Reverse();
            return new IfExpression(condition, body, elseIfClauses, location);
        }

        private ArrayConstructor/*!*/ MakeVerbatimWords(List<Expression>/*!*/ words, SourceSpan wordsLocation, SourceSpan location) {
            Debug.Assert(CollectionUtils.TrueForAll(words, (word) => word is StringLiteral), "all words are string literals");

            return new ArrayConstructor(new Arguments(words.ToArray(), null, null, wordsLocation), location);
        }

        private Expression/*!*/[]/*!*/ PopHashArguments(int argumentCount, SourceSpan location) {
            if (argumentCount % 2 != 0) {
                ErrorSink.Add(_sourceUnit, "odd number list for Hash", location, -1, Severity.Error);
                return PopArguments(argumentCount, Literal.Nil(SourceSpan.None));
            } else {
                return PopArguments(argumentCount);
            }
        }

        private Arguments/*!*/ RequireNoBlockArg(TokenValue arguments) {
            if (arguments.Block != null) {
                ErrorSink.Add(_sourceUnit, "block argument should not be given", arguments.Block.Location, -1, Severity.Error);
                arguments.Block = null;
            }

            return arguments.Arguments;
        }

        private static MethodCall/*!*/ MakeMethodCall(Expression target, string/*!*/ methodName, TokenValue args, SourceSpan location) {
            return new MethodCall(target, methodName, args.Arguments, args.Block, location);
        }

        private static MethodCall/*!*/ MakeMethodCall(Expression target, string/*!*/ methodName, TokenValue args, Block block, SourceSpan location) {
            Debug.Assert(args.Block == null);
            return new MethodCall(target, methodName, args.Arguments, block, location);
        }

        private static Expression/*!*/ MakeMatch(Expression/*!*/ left, Expression/*!*/ right, SourceSpan location) {
            var regex = left as RegularExpression;
            if (regex != null) {
                return new MatchExpression(regex, right, location);
            } else {
                return new MethodCall(left, Symbols.Match, new Arguments(right), location);
            }
        }

        private static SuperCall/*!*/ MakeSuperCall(TokenValue args, SourceSpan location) {
            return new SuperCall(args.Arguments, args.Block, location);
        }

        private Expression[]/*!*/ _argumentStack = new Expression[10];
        private int _argumentCount;

        private void PushArgument(int argumentCount, Expression/*!*/ argument) {
            Assert.NotNull(argument);
            if (_argumentCount == _argumentStack.Length) {
                Array.Resize(ref _argumentStack, _argumentCount * 2);
            }
            _argumentStack[_argumentCount++] = argument;
            yyval.ArgumentCount = argumentCount + 1;
        }

        private Expression/*!*/[]/*!*/ PopArguments(int count) {
            var result = new Expression[count];
            _argumentCount -= count;
            Array.Copy(_argumentStack, _argumentCount, result, 0, count);
            return result;
        }

        private Expression/*!*/[]/*!*/ PopArguments(int count, Expression/*!*/ additionalArgument) {
            var result = new Expression[count + 1];
            _argumentCount -= count;
            Array.Copy(_argumentStack, _argumentCount, result, 0, count);
            result[count] = additionalArgument;
            return result;
        }

        private Expression/*!*/[]/*!*/ PopArguments(Expression/*!*/ additionalArgument, int count) {
            var result = new Expression[count + 1];
            _argumentCount -= count;
            Array.Copy(_argumentStack, _argumentCount, result, 1, count);
            result[0] = additionalArgument;
            return result;
        }

        // foo 
        // foo {}
        private void SetNoArguments(Block block) {
            yyval.Arguments = null;
            yyval.Block = block;
        }

        // foo()
        private void SetArguments() {
            yyval.Arguments = Arguments.Empty;
            yyval.Block = null;
        }
        
        // foo()
        // foo() {}
        // foo(&p)
        // foo &p
        private void SetArguments(Block block) {
            yyval.Arguments = Arguments.Empty;
            yyval.Block = block;
        }

        // foo(expr)
        private void SetArguments(Expression/*!*/ expression) {
            yyval.Arguments = new Arguments(expression);
            yyval.Block = null;
        }

        // foo(expr)
        // foo(expr) {}
        // foo(expr, &p)
        private void SetArguments(Expression/*!*/ expression, Block block) {
            yyval.Arguments = new Arguments(expression);
            yyval.Block = block;
        }

        // foo(expr1, ..., exprN, k1 => v1, ..., kN => vN, *a)
        // foo(expr1, ..., exprN, k1 => v1, ..., kN => vN, *a) {}
        // foo(expr1, ..., exprN, k1 => v1, ..., kN => vN, *a, &p)
        private void PopAndSetArguments(int argumentCount, List<Maplet/*!*/> maplets, Expression array, Block block, SourceSpan location) {
            yyval.Arguments = new Arguments(PopArguments(argumentCount), maplets, array, location);
            yyval.Block = block;
        }

        private void SetArguments(Expression/*!*/[] arguments, List<Maplet/*!*/> maplets, Expression array, Block block, SourceSpan location) {
            yyval.Arguments = new Arguments(arguments, maplets, array, location);
            yyval.Block = block;
        }

        private void SetWhenClauseArguments(int argumentCount, Expression/*!*/ splat) {
            yyval.ArgumentCount = argumentCount;
            yyval.Expression = splat;
        }

        private WhenClause/*!*/ MakeWhenClause(TokenValue whenArgs, Statements/*!*/ statements, SourceSpan location) {
            return new WhenClause(whenArgs.ArgumentCount != 0 ? PopArguments(whenArgs.ArgumentCount) : null, whenArgs.Expression, statements, location);
        }

        private static Expression/*!*/ MakeLoopStatement(Expression/*!*/ statement, Expression/*!*/ condition, bool isWhileLoop, SourceSpan location) {
            return new WhileLoopExpression(condition, isWhileLoop, statement is Body, new Statements(statement), location);
        }

        public static string/*!*/ TerminalToString(int terminal) {
            Debug.Assert(terminal >= 0);
            if (((Tokens)terminal).ToString() != terminal.ToString()) {
                return IronRuby.Runtime.RubyUtils.TryMangleName(((Tokens)terminal).ToString()).ToUpper();
            } else {
                return CharToString((char)terminal);
            }
        }

        public static StringLiteral/*!*/ MakeStringLiteral(TokenValue token, SourceSpan location) {
            return new StringLiteral(token.StringContent, location);
        }

        private StringConstructor/*!*/ MakeSymbolConstructor(List<Expression>/*!*/ content, SourceSpan location) {
            if (content.Count == 0) {
                _tokenizer.ReportError(Errors.EmptySymbolLiteral);
            }
            return new StringConstructor(content, StringKind.Immutable, location);
        }

        // TODO: utils

        public static string/*!*/ CharToString(char ch) {
            switch (ch) {
                case '\a': return @"'\a'";
                case '\b': return @"'\b'";
                case '\f': return @"'\f'";
                case '\n': return @"'\n'";
                case '\r': return @"'\r'";
                case '\t': return @"'\t'";
                case '\v': return @"'\v'";
                case '\0': return @"'\0'";
                default: return String.Concat("'", ch.ToString(), "'");
            }
        }

        public static string/*!*/ EscapeString(string str) {
            return (str == null) ? String.Empty :
                new StringBuilder(str).
                Replace(@"\", @"\\").
                Replace(@"""", @"\""").
                Replace("\a", @"\a").
                Replace("\b", @"\b").
                Replace("\f", @"\f").
                Replace("\n", @"\n").
                Replace("\r", @"\r").
                Replace("\t", @"\t").
                Replace("\v", @"\v").
                Replace("\0", @"\0").ToString();
        }
    }
}



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
using System.Diagnostics;
using Microsoft.Scripting;
using IronRuby.Builtins;
using IronRuby.Compiler;
using IronRuby.Runtime;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace IronRuby.Tests {
    internal class AssertTokenizer {
        private readonly RubyContext/*!*/ _context;
        private Tokenizer _tokenizer;
        private Tokens _actualToken;
        private TokenValue _actualValue;
        private SourceSpan _actualSpan;
        private LoggingErrorSink _log;
        private List<Tokens>/*!*/ _allTokens;
        private List<object>/*!*/ _allValues;

        public AssertTokenizer(RubyContext/*!*/ context) {
            _log = new LoggingErrorSink();
            _context = context;
        }

        public AssertTokenizer/*!*/ B {
            get { if (Debugger.IsAttached) Debugger.Break(); return this; }
        }

        public void EOF() {
            Read(Tokens.EndOfFile);
            Expect();
        }

        public AssertTokenizer/*!*/ Load(string/*!*/ source) {
            Tests.Assert(_log.Errors.Count == 0, "Previous test case reported unexpected error/warning(s)");

            _tokenizer = new Tokenizer(false, _log);
            _tokenizer.Compatibility = _context.RubyOptions.Compatibility;
            _tokenizer.Initialize(_context.CreateSnippet(source, SourceCodeKind.File));
            _allTokens = new List<Tokens>();
            _allValues = new List<object>();
            return this;
        }

        public AssertTokenizer/*!*/ Load(byte[]/*!*/ source) {
            Tests.Assert(_log.Errors.Count == 0, "Previous test case reported unexpected error/warning(s)");

            _tokenizer = new Tokenizer(false, _log);
            _tokenizer.Compatibility = _context.RubyOptions.Compatibility;
            _tokenizer.Initialize(_context.CreateSourceUnit(
                new BinaryContentProvider(source), null, BinaryEncoding.Instance, SourceCodeKind.File)
            );
            _allTokens = new List<Tokens>();
            _allValues = new List<object>();
            return this;
        }

        public AssertTokenizer/*!*/ Skip(int count) {
            while (count-- > 0) {
                Next();
            }
            return this;
        }

        public AssertTokenizer/*!*/ Next() {
            _actualToken = _tokenizer.GetNextToken();
            _actualValue = _tokenizer.TokenValue;
            _actualSpan = _tokenizer.TokenSpan;
            _allTokens.Add(_actualToken);
            _allValues.Add(_actualValue);
            return this;
        }

        public AssertTokenizer/*!*/ Read(Tokens token) {
            Next();
            Tests.Assert(_actualToken == token);
            return this;
        }

        public AssertTokenizer/*!*/ Read(Tokens token, TokenValueType tokenValue) {
            Next();
            Tests.Assert(_actualToken == token);
            Tests.Assert(_actualValue.Type == tokenValue);
            return this;
        }

        public AssertTokenizer/*!*/ Read(int expected) {
            Next();
            Tests.Assert(_actualToken == Tokens.Integer);
            Tests.Assert(_actualValue.Type == TokenValueType.Integer);
            Tests.Assert(expected == _actualValue.Integer);
            return this;
        }

        public AssertTokenizer/*!*/ Read(string/*!*/ expected) {
            Next();
            Tests.Assert(_actualToken == Tokens.StringContent);
            Tests.Assert(_actualValue.Type == TokenValueType.String);
            Tests.Assert(expected == _actualValue.String);
            return this;
        }

        public AssertTokenizer/*!*/ ReadSymbol(Tokens token, string expected) {
            Next();
            Tests.Assert(_actualToken == token);
            Tests.Assert(_actualValue.Type == TokenValueType.String);
            Tests.Assert(expected == _actualValue.String);
            return this;
        }

        public AssertTokenizer/*!*/ Read(RubyRegexOptions expected) {
            Next();
            Tests.Assert(_actualToken == Tokens.RegexpEnd);
            Tests.Assert(_actualValue.Type == TokenValueType.RegexOptions);
            Tests.Assert(expected == _actualValue.RegExOptions);
            return this;
        }

        public AssertTokenizer/*!*/ ReadBigInteger(string/*!*/ expected, uint @base) {
            Next();
            Tests.Assert(_actualToken == Tokens.BigInteger);
            Tests.Assert(_actualValue.Type == TokenValueType.BigInteger);
            Tests.Assert(StringComparer.OrdinalIgnoreCase.Compare(_actualValue.BigInteger.ToString(@base), expected) == 0);
            return this;
        }

        public AssertTokenizer/*!*/ Read(double expected) {
            Next();
            Tests.Assert(_actualToken == Tokens.Float);
            Tests.Assert(_actualValue.Type == TokenValueType.Double);

            if (Double.IsNaN(expected)) {
                Tests.Assert(Double.IsNaN(_actualValue.Double));
            } else if (Double.IsNegativeInfinity(expected)) {
                Tests.Assert(Double.IsNegativeInfinity(_actualValue.Double));
            } else if (Double.IsPositiveInfinity(expected)) {
                Tests.Assert(Double.IsPositiveInfinity(_actualValue.Double));
            } else {
                // TODO: is this correct?
                Tests.Assert(System.Math.Abs(_actualValue.Double - expected) < Double.Epsilon);
            }
            return this;
        }

        public AssertTokenizer/*!*/ Expect(params ErrorInfo[] errors) {
            if (errors == null || errors.Length == 0) {
                Tests.Assert(_log.Errors.Count == 0, "Unexpected error/warning(s)");
            } else {
                Tests.Assert(_log.Errors.Count == errors.Length, String.Format("Expected {0} error/warning(s)", errors.Length));
                for (int i = 0; i < errors.Length; i++) {
                    Tests.Assert(_log.Errors[i].Code == errors[i].Code);
                }
            }
            _log.Errors.Clear();
            return this;
        }

        public AssertTokenizer/*!*/ this[Tokens token] {
            get { return Read(token); }
        }

        public AssertTokenizer/*!*/ this[string/*!*/ expected] {
            get { return Read(expected); }
        }
    }
}

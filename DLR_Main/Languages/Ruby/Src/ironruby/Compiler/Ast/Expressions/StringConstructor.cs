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
using System.Text;
using IronRuby.Builtins;
using IronRuby.Runtime;
using IronRuby.Runtime.Calls;
using Microsoft.Scripting;
using Microsoft.Scripting.Utils;

namespace IronRuby.Compiler.Ast {
    using Ast = Microsoft.Linq.Expressions.Expression;
    using MSA = Microsoft.Linq.Expressions;

    public enum StringKind {
        Mutable,
        Immutable,
        Command
    }

    /// <summary>
    /// Sequence of string literals and/or string embedded expressions.
    /// </summary>
    public partial class StringConstructor : Expression {
        private readonly StringKind _kind;
        private readonly List<Expression>/*!*/ _parts;

        public StringKind Kind {
            get { return _kind; }
        }

        public List<Expression>/*!*/ Parts {
            get { return _parts; }
        }

        public StringConstructor(List<Expression>/*!*/ parts, StringKind kind, SourceSpan location) 
            : base(location) {
            ContractUtils.RequiresNotNullItems(parts, "parts");

            _parts = parts;
            _kind = kind;
        }

        internal override MSA.Expression/*!*/ TransformRead(AstGenerator/*!*/ gen) {
            switch (_kind) {
                case StringKind.Mutable:
                    return TransformConcatentation(gen, _parts, Methods.CreateMutableString, null);

                case StringKind.Immutable:
                    return TransformConcatentation(gen, _parts, Methods.CreateSymbol, null);

                case StringKind.Command:
                    return Ast.Dynamic(RubyCallAction.Make("`", new RubyCallSignature(1, RubyCallFlags.HasScope | RubyCallFlags.HasImplicitSelf)), typeof(object),
                        gen.CurrentScopeVariable,
                        gen.CurrentSelfVariable,
                        TransformConcatentation(gen, _parts, Methods.CreateMutableString, null)
                    );
            }

            throw Assert.Unreachable;
        }

        internal static MSA.Expression/*!*/ TransformConcatentation(AstGenerator/*!*/ gen, List<Expression>/*!*/ parts, 
            Func<string, MethodInfo>/*!*/ opFactory, MSA.Expression additionalArg) {

            var opSuffix = new StringBuilder(Math.Min(parts.Count, 4));

            List<MSA.Expression> merged = ConcatLiteralsAndTransform(gen, parts, opSuffix);

            if (merged.Count <= RubyOps.MakeStringParamCount) {
                if (merged.Count == 0) {
                    merged.Add(Ast.Constant(String.Empty));
                    opSuffix.Append(RubyOps.SuffixBinary);
                }

                if (opSuffix.IndexOf(RubyOps.SuffixEncoded) != -1) {
                    merged.Add(Ast.Constant(RubyEncoding.GetCodePage(gen.Encoding)));
                }

                if (additionalArg != null) {
                    merged.Add(additionalArg);
                }

                return opFactory(opSuffix.ToString()).OpCall(merged);
            } else {
                var paramArray = Ast.NewArrayInit(typeof(object), merged);
                var codePage = Ast.Constant(RubyEncoding.GetCodePage(gen.Encoding));
                
                return (additionalArg != null) ?
                    opFactory("N").OpCall(paramArray, codePage, additionalArg) :
                    opFactory("N").OpCall(paramArray, codePage);
            } 
        }

        private static List<MSA.Expression>/*!*/ ConcatLiteralsAndTransform(AstGenerator/*!*/ gen, List<Expression>/*!*/ parts, StringBuilder/*!*/ opName) {
            var result = new List<MSA.Expression>();
            var literals = new List<string>();
            int concatLength = 0;
            var concatEncoding = StringLiteralEncoding.Ascii;
            ConcatLiteralsAndTransformRecursive(gen, parts, literals, ref concatLength, ref concatEncoding, result, opName);

            // finish trailing literals:
            if (literals.Count > 0) {
                result.Add(Ast.Constant(Concat(literals, concatLength)));
                opName.Append(OpSuffix(gen, concatEncoding));
            }

            return result;
        }

        //
        // Traverses expressions in "parts" and concats all contiguous literal strings.
        // Notes: 
        //  - Instead of usign StringBuilder we place the string values that can be concatenated so far in to "literals" list and keep track
        //    of their total length in "concatLength" and encoding in "concatEncoding". 
        //    If we reach a non-literal expression and we have some literals ready in "literals" array we do the concat and clear the list 
        //    and "concatLength" and "concatEncoding".
        //  - "result" list contains argument expressions to the CreateMutableString* overloads.
        //  - "opName" contains the name of the operation. This method appends suffix based on the argument types (see RubyOps.Suffix*).
        //
        private static void ConcatLiteralsAndTransformRecursive(AstGenerator/*!*/ gen, List<Expression>/*!*/ parts, 
            List<string>/*!*/ literals, ref int concatLength, ref StringLiteralEncoding concatEncoding, List<MSA.Expression>/*!*/ result,
            StringBuilder/*!*/ opName) {

            for (int i = 0; i < parts.Count; i++) {
                Expression part = parts[i];
                StringLiteral literal;
                StringConstructor ctor;

                if ((literal = part as StringLiteral) != null) {
                    literals.Add(literal.Value);
                    concatEncoding = CombineEncoding(concatEncoding, literal);
                    concatLength += literal.Value.Length;
                } else if ((ctor = part as StringConstructor) != null) {
                    ConcatLiteralsAndTransformRecursive(gen, ctor.Parts, literals, ref concatLength, ref concatEncoding, result, opName);
                } else {
                    if (literals.Count > 0) {
                        result.Add(Ast.Constant(Concat(literals, concatLength)));
                        opName.Append(OpSuffix(gen, concatEncoding));
                        concatLength = 0;
                        concatEncoding = StringLiteralEncoding.Ascii;
                        literals.Clear();
                    }

                    result.Add(
                        Ast.Dynamic(
                            ConvertToSAction.Instance,
                            typeof(MutableString),
                            gen.CurrentScopeVariable, part.TransformRead(gen)
                        )
                    );
                    opName.Append(RubyOps.SuffixMutable);
                }
            }
        }

        private static StringLiteralEncoding CombineEncoding(StringLiteralEncoding encoding, StringLiteral literal) {
            if (encoding == StringLiteralEncoding.UTF8 || literal.IsUTF8) {
                return StringLiteralEncoding.UTF8;
            }

            if (encoding == StringLiteralEncoding.Ascii && literal.IsAscii) {
                return StringLiteralEncoding.Ascii;
            }

            return StringLiteralEncoding.Default;
        }

        private static char OpSuffix(AstGenerator/*!*/ gen, StringLiteralEncoding encoding) {
            if (encoding == StringLiteralEncoding.Ascii) {
                return RubyOps.SuffixBinary;
            } else if (encoding == StringLiteralEncoding.UTF8 || gen.Encoding == Encoding.UTF8) {
                return RubyOps.SuffixUTF8;
            } else if (gen.Encoding == BinaryEncoding.Instance) {
                return RubyOps.SuffixBinary;
            } else {
                return RubyOps.SuffixEncoded;
            }
        }

        private static string/*!*/ Concat(List<string>/*!*/ literals, int capacity) {
            if (literals.Count == 1) {
                return literals[0];
            }
         
            StringBuilder sb = new StringBuilder(capacity);
            for (int j = 0; j < literals.Count; j++) {
                sb.Append(literals[j]);
            }
            return sb.ToString();
        }
    }
}

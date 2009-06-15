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
using System.Dynamic;
#else
using System; using Microsoft;
#endif
using Microsoft.Scripting;
using Microsoft.Scripting.Utils;
#if CODEPLEX_40
using MSA = System.Linq.Expressions;
#else
using MSA = Microsoft.Linq.Expressions;
#endif

namespace IronRuby.Compiler.Ast {
#if CODEPLEX_40
    using Ast = System.Linq.Expressions.Expression;
#else
    using Ast = Microsoft.Linq.Expressions.Expression;
#endif

    // left.id op= rigth
    public partial class MemberAssignmentExpression : AssignmentExpression {
        private readonly Expression/*!*/ _leftTarget;
        private readonly Expression/*!*/ _right;
        private readonly string/*!*/ _memberName;

        public Expression/*!*/ LeftTarget { 
            get { return _leftTarget; } 
        }

        public Expression/*!*/ Right { 
            get { return _right; } 
        }

        public string/*!*/ MemberName {
            get { return _memberName; }
        }

        public MemberAssignmentExpression(Expression/*!*/ leftTarget, string/*!*/ memberName, string/*!*/ operation, Expression/*!*/ right, SourceSpan location)
            : base(operation, location) {
            ContractUtils.RequiresNotNull(leftTarget, "leftTarget");
            ContractUtils.RequiresNotNull(operation, "operation");
            ContractUtils.RequiresNotNull(right, "right");
            ContractUtils.RequiresNotNull(memberName, "memberName");
            ContractUtils.RequiresNotNull(operation, "operation");

            _memberName = memberName;
            _leftTarget = leftTarget;
            _right = right;
        }

        internal override MSA.Expression/*!*/ TransformRead(AstGenerator/*!*/ gen) {
            string setterName = _memberName + "=";

            MSA.Expression transformedLeftTarget = _leftTarget.TransformRead(gen);
            MSA.Expression transformedRight = _right.TransformRead(gen);

            MSA.Expression leftTemp = gen.CurrentScope.DefineHiddenVariable(String.Empty, transformedLeftTarget.Type);

            bool leftIsSelf = _leftTarget.NodeType == NodeTypes.SelfReference;

            // lhs &&= rhs  -->  left.member && (left.member = rhs)
            // lhs ||= rhs  -->  left.member || (left.member = rhs)
            if (Operation == Symbols.And || Operation == Symbols.Or) {
                MSA.Expression leftMemberRead = MethodCall.TransformRead(this, gen, false, _memberName, Ast.Assign(leftTemp, transformedLeftTarget), null, null, null, null);
                MSA.Expression transformedWrite = MethodCall.TransformRead(this, gen, leftIsSelf, setterName, leftTemp, null, null, null, transformedRight);

                if (Operation == Symbols.And) {
                    return AndExpression.TransformRead(gen, leftMemberRead, transformedWrite);
                } else {
                    return OrExpression.TransformRead(gen, leftMemberRead, transformedWrite);
                }
            } else {
                // left.member= left.member().op(right)
                MSA.Expression leftMemberRead = MethodCall.TransformRead(this, gen, false, _memberName, leftTemp, null, null, null, null);
                MSA.Expression operationCall = MethodCall.TransformRead(this, gen, false, Operation, leftMemberRead, null, null, transformedRight, null);
                MSA.Expression transformedWrite = MethodCall.TransformRead(this, gen, leftIsSelf, setterName, Ast.Assign(leftTemp, transformedLeftTarget), null, null, null, operationCall);
                return transformedWrite;
            }
        }
    }
}

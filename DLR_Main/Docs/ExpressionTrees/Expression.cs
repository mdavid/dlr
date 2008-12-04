using System; using Microsoft;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Permissions;
using System.Text;

namespace Microsoft.Linq.Expressions {
    using Microsoft.Linq;
    using System.Reflection;

    public enum ExpressionType {
        Lambda,
    }

    public abstract class Expression {

        //PORTED
        public static MemberAssignment Bind(MemberInfo member, Expression expression) {
            if (member == null)
                throw Error.ArgumentNull("member");
            if (expression == null)
                throw Error.ArgumentNull("expression");
            Type memberType;
            ValidateSettableFieldOrPropertyMember(member, out memberType);
            if (!AreAssignable(memberType, expression.Type))
                throw Error.ArgumentTypesMustMatch();
            return new MemberAssignment(member, expression);
        }


        //PORTED
        public static MemberAssignment Bind(MethodInfo propertyAccessor, Expression expression) {
            if (propertyAccessor == null)
                throw Error.ArgumentNull("propertyAccessor");
            if (expression == null)
                throw Error.ArgumentNull("expression");
            ValidateMethodInfo(propertyAccessor);
            return Bind(GetProperty(propertyAccessor), expression);
        }





        //PORTED
        internal static void ValidateLift(IEnumerable<ParameterExpression> parameters, IEnumerable<Expression> arguments) {
            System.Diagnostics.Debug.Assert(parameters != null);
            System.Diagnostics.Debug.Assert(arguments != null);

            ReadOnlyCollection<ParameterExpression> paramList = parameters.ToReadOnlyCollection();
            ReadOnlyCollection<Expression> argList = arguments.ToReadOnlyCollection();
            if (paramList.Count != argList.Count)
                throw Error.IncorrectNumberOfIndexes();
            for (int i = 0, n = paramList.Count; i < n; i++) {
                if (!AreReferenceAssignable(paramList[i].Type, GetNonNullableType(argList[i].Type)))
                    throw Error.ArgumentTypesMustMatch();
            }
        }


        //PORTED
        public static ListInitExpression ListInit(NewExpression newExpression, params Expression[] initializers) {
            if (newExpression == null)
                throw Error.ArgumentNull("newExpression");
            if (initializers == null)
                throw Error.ArgumentNull("initializers");
            return ListInit(newExpression, initializers as IEnumerable<Expression>);
        }
        //PORTED
        public static ListInitExpression ListInit(NewExpression newExpression, IEnumerable<Expression> initializers) {
            if (newExpression == null)
                throw Error.ArgumentNull("newExpression");
            if (initializers == null)
                throw Error.ArgumentNull("initializers");
            if (!initializers.Any())
                throw Error.ListInitializerWithZeroMembers();

            MethodInfo addMethod = FindMethod(newExpression.Type, "Add", null, new Expression[] { initializers.First() }, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return ListInit(newExpression, addMethod, initializers);
        }
        //PORTED
        public static ListInitExpression ListInit(NewExpression newExpression, MethodInfo addMethod, params Expression[] initializers) {
            if (newExpression == null)
                throw Error.ArgumentNull("newExpression");
            if (initializers == null)
                throw Error.ArgumentNull("initializers");
            if (addMethod == null)
                return ListInit(newExpression, initializers as IEnumerable<Expression>);
            return ListInit(newExpression, addMethod, initializers as IEnumerable<Expression>);
        }
        //PORTED
        public static ListInitExpression ListInit(NewExpression newExpression, MethodInfo addMethod, IEnumerable<Expression> initializers) {
            if (newExpression == null)
                throw Error.ArgumentNull("newExpression");
            if (initializers == null)
                throw Error.ArgumentNull("initializers");
            if (!initializers.Any())
                throw Error.ListInitializerWithZeroMembers();
            if (addMethod == null)
                return ListInit(newExpression, initializers);
            List<ElementInit> initList = new List<ElementInit>();
            foreach (Expression initializer in initializers)
                initList.Add(ElementInit(addMethod, initializer));
            return ListInit(newExpression, initList);
        }
        //PORTED
        public static ListInitExpression ListInit(NewExpression newExpression, params ElementInit[] initializers) {
            if (newExpression == null)
                throw Error.ArgumentNull("newExpression");
            if (initializers == null)
                throw Error.ArgumentNull("initializers");
            return ListInit(newExpression, initializers.ToReadOnlyCollection());
        }
        //PORTED
        public static ListInitExpression ListInit(NewExpression newExpression, IEnumerable<ElementInit> initializers) {
            if (newExpression == null)
                throw Error.ArgumentNull("newExpression");
            if (initializers == null)
                throw Error.ArgumentNull("initializers");
            if (!initializers.Any())
                throw Error.ListInitializerWithZeroMembers();
            ReadOnlyCollection<ElementInit> initList = initializers.ToReadOnlyCollection();
            ValidateListInitArgs(newExpression.Type, initList);
            return new ListInitExpression(newExpression, initList);
        }



        //PORTED
        public static ElementInit ElementInit(MethodInfo addMethod, params Expression[] arguments) {
            return ElementInit(addMethod, arguments as IEnumerable<Expression>);
        }
        //PORTED
        public static ElementInit ElementInit(MethodInfo addMethod, IEnumerable<Expression> arguments) {
            if (addMethod == null)
                throw Error.ArgumentNull("addMethod");
            if (arguments == null)
                throw Error.ArgumentNull("arguments");
            ValidateElementInitAddMethodInfo(addMethod);
            ReadOnlyCollection<Expression> argumentsRO = arguments.ToReadOnlyCollection();
            ValidateArgumentTypes(addMethod, ref argumentsRO);
            return new ElementInit(addMethod, argumentsRO);
        }




        //PORTED
        public static MemberListBinding ListBind(MemberInfo member, params ElementInit[] initializers) {
            if (member == null)
                throw Error.ArgumentNull("member");
            if (initializers == null)
                throw Error.ArgumentNull("initializers");
            return ListBind(member, initializers.ToReadOnlyCollection());
        }
        //PORTED
        public static MemberListBinding ListBind(MemberInfo member, IEnumerable<ElementInit> initializers) {
            if (member == null)
                throw Error.ArgumentNull("member");
            if (initializers == null)
                throw Error.ArgumentNull("initializers");
            Type memberType;
            ValidateGettableFieldOrPropertyMember(member, out memberType);
            ReadOnlyCollection<ElementInit> initList = initializers.ToReadOnlyCollection();
            ValidateListInitArgs(memberType, initList);
            return new MemberListBinding(member, initList);
        }
        //PORTED
        public static MemberListBinding ListBind(MethodInfo propertyAccessor, params ElementInit[] initializers) {
            if (propertyAccessor == null)
                throw Error.ArgumentNull("propertyAccessor");
            if (initializers == null)
                throw Error.ArgumentNull("initializers");
            return ListBind(propertyAccessor, initializers.ToReadOnlyCollection());
        }
        //PORTED
        public static MemberListBinding ListBind(MethodInfo propertyAccessor, IEnumerable<ElementInit> initializers) {
            if (propertyAccessor == null)
                throw Error.ArgumentNull("propertyAccessor");
            if (initializers == null)
                throw Error.ArgumentNull("initializers");
            return ListBind(GetProperty(propertyAccessor), initializers);
        }



        //PORTED
        private static void ValidateListInitArgs(Type listType, ReadOnlyCollection<ElementInit> initializers) {
            if (!AreAssignable(typeof(IEnumerable), listType))
                throw Error.TypeNotIEnumerable(listType);
            for (int i = 0, n = initializers.Count; i < n; i++) {
                ElementInit element = initializers[i];
                if (element == null)
                    throw Error.ArgumentNull("initializers");
                ValidateCallInstanceType(listType, element.AddMethod);
            }
        }




        //PORTED
        public static MemberInitExpression MemberInit(NewExpression newExpression, params MemberBinding[] bindings) {
            if (newExpression == null)
                throw Error.ArgumentNull("newExpression");
            if (bindings == null)
                throw Error.ArgumentNull("bindings");
            return MemberInit(newExpression, bindings.ToReadOnlyCollection());
        }
        //PORTED
        public static MemberInitExpression MemberInit(NewExpression newExpression, IEnumerable<MemberBinding> bindings) {
            if (newExpression == null)
                throw Error.ArgumentNull("newExpression");
            if (bindings == null)
                throw Error.ArgumentNull("bindings");
            ReadOnlyCollection<MemberBinding> roBindings = bindings.ToReadOnlyCollection();
            ValidateMemberInitArgs(newExpression.Type, roBindings);
            return new MemberInitExpression(newExpression, roBindings);
        }



        //PORTED
        public static MemberMemberBinding MemberBind(MemberInfo member, params MemberBinding[] bindings) {
            if (member == null)
                throw Error.ArgumentNull("member");
            if (bindings == null)
                throw Error.ArgumentNull("bindings");
            return MemberBind(member, bindings.ToReadOnlyCollection());
        }
        //PORTED
        public static MemberMemberBinding MemberBind(MemberInfo member, IEnumerable<MemberBinding> bindings) {
            if (member == null)
                throw Error.ArgumentNull("member");
            if (bindings == null)
                throw Error.ArgumentNull("bindings");
            ReadOnlyCollection<MemberBinding> roBindings = bindings.ToReadOnlyCollection();
            Type memberType;
            ValidateGettableFieldOrPropertyMember(member, out memberType);
            ValidateMemberInitArgs(memberType, roBindings);
            return new MemberMemberBinding(member, roBindings);
        }
        //PORTED
        public static MemberMemberBinding MemberBind(MethodInfo propertyAccessor, params MemberBinding[] bindings) {
            if (propertyAccessor == null)
                throw Error.ArgumentNull("propertyAccessor");
            return MemberBind(GetProperty(propertyAccessor), bindings);
        }
        //PORTED
        public static MemberMemberBinding MemberBind(MethodInfo propertyAccessor, IEnumerable<MemberBinding> bindings) {
            if (propertyAccessor == null)
                throw Error.ArgumentNull("propertyAccessor");
            return MemberBind(GetProperty(propertyAccessor), bindings);
        }



        //PORTED
        private static void ValidateSettableFieldOrPropertyMember(MemberInfo member, out Type memberType) {
            FieldInfo fi = member as FieldInfo;
            if (fi == null) {
                PropertyInfo pi = member as PropertyInfo;
                if (pi == null)
                    throw Error.ArgumentMustBeFieldInfoOrPropertInfo();
                if (!pi.CanWrite)
                    throw Error.PropertyDoesNotHaveSetter(pi);
                memberType = pi.PropertyType;
            } else {
                memberType = fi.FieldType;
            }
        }

        
        //PORTED
        private static void ValidateGettableFieldOrPropertyMember(MemberInfo member, out Type memberType) {
            FieldInfo fi = member as FieldInfo;
            if (fi == null) {
                PropertyInfo pi = member as PropertyInfo;
                if (pi == null)
                    throw Error.ArgumentMustBeFieldInfoOrPropertInfo();
                if (!pi.CanRead)
                    throw Error.PropertyDoesNotHaveGetter(pi);
                memberType = pi.PropertyType;
            } else {
                memberType = fi.FieldType;
            }
        }
        //PORTED
        private static void ValidateMemberInitArgs(Type type, ReadOnlyCollection<MemberBinding> bindings) {
            for (int i = 0, n = bindings.Count; i < n; i++) {
                MemberBinding b = bindings[i];
                if (!AreAssignable(b.Member.DeclaringType, type))
                    throw Error.NotAMemberOfType(b.Member.Name, type);
            }
        }


        private static void ValidateIntegerArg(Type type) {
            if (!IsInteger(type))
                throw Error.ArgumentMustBeInteger();
        }
        private static void ValidateIntegerOrBoolArg(Type type) {
            if (!IsIntegerOrBool(type))
                throw Error.ArgumentMustBeIntegerOrBoolean();
        }
        private static void ValidateNumericArg(Type type) {
            if (!IsNumeric(type))
                throw Error.ArgumentMustBeNumeric();
        }
        private static void ValidateConvertibleArg(Type type) {
            if (!IsConvertible(type))
                throw Error.ArgumentMustBeConvertible();
        }
        private static void ValidateBoolArg(Type type) {
            if (!IsBool(type))
                throw Error.ArgumentMustBeBoolean();
        }



        private static void ValidateSameArgTypes(Type left, Type right) {
            if (left != right)
                throw Error.ArgumentTypesMustMatch();
        }


        //PORTED
        private static void ValidateElementInitAddMethodInfo(MethodInfo addMethod) {
            ValidateMethodInfo(addMethod);
            if (addMethod.GetParameters().Length == 0)
                throw Error.ElementInitializerMethodWithZeroArgs();
            if (!addMethod.Name.Equals("Add", StringComparison.OrdinalIgnoreCase))
                throw Error.ElementInitializerMethodNotAdd();
            if (addMethod.IsStatic)
                throw Error.ElementInitializerMethodStatic();
            foreach (ParameterInfo pi in addMethod.GetParameters()) {
                if (pi.ParameterType.IsByRef)
                    throw Error.ElementInitializerMethodNoRefOutParam(pi.Name, addMethod.Name);
            }
        }


        //PORTED  ConstantCheck
        private static bool IsNullConstant(Expression expr) {
            ConstantExpression c = expr as ConstantExpression;
            if (c == null) return false;
            return c.Value == null;
        }

    }

    //PORTED
    public enum MemberBindingType {
        Assignment,
        MemberBinding,
        ListBinding
    }

    //PORTED
    public abstract class MemberBinding {
        MemberBindingType type;
        MemberInfo member;
        protected MemberBinding(MemberBindingType type, MemberInfo member) {
            this.type = type;
            this.member = member;
        }
        public MemberBindingType BindingType {
            get { return this.type; }
        }
        public MemberInfo Member {
            get { return this.member; }
        }
        internal abstract void BuildString(StringBuilder builder);
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            this.BuildString(sb);
            return sb.ToString();
        }
    }

    //PORTED
    public sealed class MemberAssignment : MemberBinding {
        Expression expression;
        internal MemberAssignment(MemberInfo member, Expression expression)
            : base(MemberBindingType.Assignment, member) {
            this.expression = expression;
        }
        public Expression Expression {
            get { return this.expression; }
        }
        internal override void BuildString(StringBuilder builder) {
            if (builder == null) {
                throw Error.ArgumentNull("builder");
            }

            builder.Append(this.Member.Name);
            builder.Append(" = ");
            this.expression.BuildString(builder);
        }
    }

    //PORTED
    public sealed class MemberMemberBinding : MemberBinding {
        ReadOnlyCollection<MemberBinding> bindings;
        internal MemberMemberBinding(MemberInfo member, ReadOnlyCollection<MemberBinding> bindings)
            : base(MemberBindingType.MemberBinding, member) {
            this.bindings = bindings;
        }
        public ReadOnlyCollection<MemberBinding> Bindings {
            get { return this.bindings; }
        }
        internal override void BuildString(StringBuilder builder) {
            builder.Append(this.Member.Name);
            builder.Append(" = {");
            for (int i = 0, n = this.bindings.Count; i < n; i++) {
                if (i > 0) builder.Append(", ");
                this.bindings[i].BuildString(builder);
            }
            builder.Append("}");
        }
    }

    //PORTED
    public sealed class MemberListBinding : MemberBinding {
        ReadOnlyCollection<ElementInit> initializers;
        internal MemberListBinding(MemberInfo member, ReadOnlyCollection<ElementInit> initializers)
            : base(MemberBindingType.ListBinding, member) {
            this.initializers = initializers;
        }
        public ReadOnlyCollection<ElementInit> Initializers {
            get { return this.initializers; }
        }
        internal override void BuildString(StringBuilder builder) {
            builder.Append(this.Member.Name);
            builder.Append(" = {");
            for (int i = 0, n = this.initializers.Count; i < n; i++) {
                if (i > 0) builder.Append(", ");
                this.initializers[i].BuildString(builder);
            }
            builder.Append("}");
        }
    }
    
    //PORTED
    public sealed class ElementInit {
        private MethodInfo addMethod;
        private ReadOnlyCollection<Expression> arguments;

        internal ElementInit(MethodInfo addMethod, ReadOnlyCollection<Expression> arguments) {
            this.addMethod = addMethod;
            this.arguments = arguments;
        }
        public MethodInfo AddMethod {
            get { return this.addMethod; }
        }
        public ReadOnlyCollection<Expression> Arguments {
            get { return this.arguments; }
        }
        internal void BuildString(StringBuilder builder) {
            builder.Append(AddMethod);
            builder.Append("(");
            bool first = true;
            foreach (Expression argument in arguments) {
                if (first) {
                    first = false;
                } else {
                    builder.Append(",");
                }
                argument.BuildString(builder);
            }
            builder.Append(")");
        }
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            this.BuildString(sb);
            return sb.ToString();
        }
    }


    //PORTED
    public sealed class MemberInitExpression : Expression {
        NewExpression newExpression;
        ReadOnlyCollection<MemberBinding> bindings;
        internal MemberInitExpression(NewExpression newExpression, ReadOnlyCollection<MemberBinding> bindings)
            : base(ExpressionType.MemberInit, newExpression.Type) {
            this.newExpression = newExpression;
            this.bindings = bindings;
        }
        public NewExpression NewExpression {
            get { return this.newExpression; }
        }
        public ReadOnlyCollection<MemberBinding> Bindings {
            get { return this.bindings; }
        }
        internal override void BuildString(StringBuilder builder) {
            if (this.newExpression.Arguments.Count == 0 &&
                this.newExpression.Type.Name.Contains("<")) {
                // anonymous type constructor
                builder.Append("new");
            } else {
                this.newExpression.BuildString(builder);
            }
            builder.Append(" {");
            for (int i = 0, n = this.bindings.Count; i < n; i++) {
                MemberBinding b = this.bindings[i];
                if (i > 0) builder.Append(", ");
                b.BuildString(builder);
            }
            builder.Append("}");
        }
    }

    //PORTED
    public sealed class ListInitExpression : Expression {
        NewExpression newExpression;
        ReadOnlyCollection<ElementInit> initializers;
        internal ListInitExpression(NewExpression newExpression, ReadOnlyCollection<ElementInit> initializers)
            : base(ExpressionType.ListInit, newExpression.Type) {
            this.newExpression = newExpression;
            this.initializers = initializers;
        }
        public NewExpression NewExpression {
            get { return this.newExpression; }
        }
        public ReadOnlyCollection<ElementInit> Initializers {
            get { return this.initializers; }
        }
        internal override void BuildString(StringBuilder builder) {
            this.newExpression.BuildString(builder);
            builder.Append(" {");
            for (int i = 0, n = this.initializers.Count; i < n; i++) {
                if (i > 0) builder.Append(", ");
                this.initializers[i].BuildString(builder);
            }
            builder.Append("}");
        }
    }


    internal static class ReadOnlyCollectionExtensions {
        internal static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> sequence) {
            if (sequence == null)
                return DefaultReadOnlyCollection<T>.Empty;
            ReadOnlyCollection<T> col = sequence as ReadOnlyCollection<T>;
            if (col != null)
                return col;
            return new ReadOnlyCollection<T>(sequence.ToArray());
        }
        private static class DefaultReadOnlyCollection<T> {
            private static ReadOnlyCollection<T> _defaultCollection;
            internal static ReadOnlyCollection<T> Empty {
                get {
                    if (_defaultCollection == null)
                        _defaultCollection = new ReadOnlyCollection<T>(new T[] { });
                    return _defaultCollection;
                }
            }
        }
    }
}


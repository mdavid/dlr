using System; using Microsoft;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Permissions;
using System.Security;
using System.Runtime.CompilerServices;
using Microsoft.Runtime.CompilerServices;


namespace Microsoft.Linq.Expressions {

    internal class ExpressionCompiler {

        internal class LambdaInfo {
            internal LambdaExpression Lambda;
            internal List<LambdaInfo> Lambdas;
            internal MethodInfo Method;
            internal Dictionary<ParameterExpression, int> HoistedLocals;
            internal LambdaInfo(LambdaExpression lambda, MethodInfo method, Dictionary<ParameterExpression, int> hoistedLocals, List<LambdaInfo> lambdas) {
                this.Lambda = lambda;
                this.Method = method;
                this.HoistedLocals = hoistedLocals;
                this.Lambdas = lambdas;
            }
        }

        class CompileScope {
            internal CompileScope Parent;
            internal LambdaExpression Lambda;
            internal Dictionary<ParameterExpression, LocalBuilder> Locals;
            internal Dictionary<ParameterExpression, int> HoistedLocals;
            internal LocalBuilder HoistedLocalsVar;
            internal CompileScope(CompileScope parent, LambdaExpression lambda) {
                this.Parent = parent;
                this.Lambda = lambda;
                this.Locals = new Dictionary<ParameterExpression, LocalBuilder>();
                this.HoistedLocals = new Dictionary<ParameterExpression, int>();
                // this.HoistedLocalsVar = null;
            }
        }

        enum StackType
        {
            Value,
            Address
        }

        List<LambdaInfo> lambdas;
        List<object> globals;
        CompileScope scope;

        internal ExpressionCompiler()
        {
            this.lambdas = new List<LambdaInfo>();
            this.globals = new List<object>();
        }
        
        public D Compile<D>(Expression<D> lambda) 
        {
            if (!typeof(Delegate).IsAssignableFrom(typeof(D))) 
            {
                throw Error.TypeParameterIsNotDelegate(typeof(D));
            }
            return (D)(object)this.Compile((LambdaExpression)lambda);
        }

        public Delegate Compile(LambdaExpression lambda)
        {
            Delegate d = this.CompileDynamicLambda(lambda);
            return d;
        }

        private Delegate CompileDynamicLambda(LambdaExpression lambda) {
            this.lambdas = new List<LambdaInfo>();
            this.globals = new List<object>();
            LambdaInfo info;
            ExecutionScope executionScope;

            int iLambda = this.GenerateLambda(lambda);
            info = this.lambdas[iLambda];
            executionScope = new ExecutionScope(null, info, this.globals.ToArray(), null);

            return ((DynamicMethod)info.Method).CreateDelegate(lambda.Type, executionScope);
        }

        private static void GenerateLoadExecutionScope(ILGenerator gen)
        {
            gen.Emit(OpCodes.Ldarg_0);
        }

        private void GenerateLoadHoistedLocals(ILGenerator gen)
        {
            Debug.Assert(this.scope.HoistedLocalsVar != null);
            gen.Emit(OpCodes.Ldloc, this.scope.HoistedLocalsVar);
        }
            
        private int GenerateLambda(LambdaExpression lambda) {
            this.scope = new CompileScope(this.scope, lambda);

            MethodInfo mi = lambda.Type.GetMethod("Invoke");

            // hoist locals referenced in other scopes
            new Hoister().Hoist(this.scope);

            ILGenerator gen;
            MethodInfo genMethod;

            
            DynamicMethod dm = new DynamicMethod(
                "lambda_method",
                mi.ReturnType,
                this.GetParameterTypes(mi),
                true // Restricted skip visibility on
                );
            // With restricted skip visibility off, the generated method can access all
            // publics but the jitter enforces that access to privates is forbidden.
            // With it on, the jitter relaxes slightly. When a dynamic method attempts to
            // access a private the access is allowed if a demand for RestrictedMemberAccess
            // succeeds against the call stack in place at the time that the dynamic method
            // is created, ie, here.
            //
            // If restricted member access is granted now then the dynamic method can access
            // privates members in assemblies which are at the current trust level or lower.
            gen = dm.GetILGenerator();
            genMethod = dm;

            this.GenerateInitHoistedLocals(gen);
            this.Generate(gen, lambda.Body, StackType.Value);
            if (mi.ReturnType == typeof(void) && lambda.Body.Type != typeof(void))
            {
                gen.Emit(OpCodes.Pop);
            }
            gen.Emit(OpCodes.Ret);

            int iLambda = this.lambdas.Count;
            this.lambdas.Add(new LambdaInfo(lambda, genMethod, this.scope.HoistedLocals, this.lambdas));

            this.scope = this.scope.Parent;
            return iLambda;
        }

        private void GenerateInitHoistedLocals(ILGenerator gen)
        {
            // Suppose we have something like (string s)=>()=>s.  We wish to generate the outer as:
            //      Func<string> OuterMethod(ExecutionScope scope, string s)
            //      {
            //          object[] locals = scope.CreateHoistedLocals();
            //          locals[0] = s;
            //          return scope.CreateDelegate(0, locals);
            //      }
            // and the inner as
            //      string InnerMethod(ExecutionScope scope)
            //      {
            //          return scope.Locals[0];
            //      }
            //
            // In this method we generate the code which creates the hoisted locals object
            // for this function activation and fills it in with the hoisted parameters.
            if (this.scope.HoistedLocals.Count == 0)
                return;
            this.scope.HoistedLocalsVar = gen.DeclareLocal(typeof(object[]));
            GenerateLoadExecutionScope(gen);
            // PERF: [EricLi] We could be storing these methodinfos in statics rather than
            // PERF: calling GetMethod over and over again.
            gen.Emit(OpCodes.Callvirt, typeof(ExecutionScope).GetMethod("CreateHoistedLocals", BindingFlags.Instance | BindingFlags.Public));
            gen.Emit(OpCodes.Stloc, this.scope.HoistedLocalsVar);
            int n = this.scope.Lambda.Parameters.Count;
            for (int iArg = 0; iArg < n; iArg ++)
            {
                ParameterExpression p = this.scope.Lambda.Parameters[iArg];
                if (this.IsHoisted(p))
                {
                    this.PrepareInitLocal(gen, p);
                    this.GenerateArgAccess(gen, iArg + 1, StackType.Value);
                    this.GenerateInitLocal(gen, p);
                }
            }
        }

        private bool IsHoisted(ParameterExpression p) {
            return this.scope.HoistedLocals.ContainsKey(p);
        }

        //PORTED: _scope.AddLocal
        private void PrepareInitLocal(ILGenerator gen, ParameterExpression p)
        {
            int hoistIndex;
            if (this.scope.HoistedLocals.TryGetValue(p, out hoistIndex))
            {
                GenerateLoadHoistedLocals(gen);
                this.GenerateConstInt(gen, hoistIndex);
            }
            else {
                LocalBuilder local = gen.DeclareLocal(p.Type);
                this.scope.Locals.Add(p, local);
            }
        }

        private static Type MakeStrongBoxType(Type type)
        {
            return typeof(StrongBox<>).MakeGenericType(type);
        }

        //PORTED: _scope.EmitSet
        private void GenerateInitLocal(ILGenerator gen, ParameterExpression p) {
            int hoistIndex;
            if (this.scope.HoistedLocals.TryGetValue(p, out hoistIndex)) {
                Type varType = MakeStrongBoxType(p.Type);
                ConstructorInfo ci = varType.GetConstructor(new Type[] { p.Type });
                gen.Emit(OpCodes.Newobj, ci);
                gen.Emit(OpCodes.Stelem_Ref);
            }
            else {
                LocalBuilder local;
                if (this.scope.Locals.TryGetValue(p, out local)) {
                    gen.Emit(OpCodes.Stloc, local);
                }
                else {
                    throw Error.NotSupported();
                }
            }
        }

        class Hoister : ExpressionVisitor {
            CompileScope expressionScope;
            LambdaExpression current;
            List<ParameterExpression> locals;

            internal Hoister() {
            }

            internal void Hoist(CompileScope scope) {
                this.expressionScope = scope;
                this.current = scope.Lambda;
                this.locals = new List<ParameterExpression>(scope.Lambda.Parameters);
                this.Visit(scope.Lambda.Body);
            }

            internal override Expression VisitParameter(ParameterExpression p) {
                // look for uses of this parameter outside the scope in question
                if (this.locals.Contains(p) && this.expressionScope.Lambda != current) {
                    if (!this.expressionScope.HoistedLocals.ContainsKey(p)) {
                        this.expressionScope.HoistedLocals.Add(p, this.expressionScope.HoistedLocals.Count);
                    }
                }
                return p;
            }

            internal override Expression VisitInvocation(InvocationExpression iv) {
                if (this.expressionScope.Lambda == current) {
                    if (iv.Expression.NodeType == ExpressionType.Lambda) {
                        LambdaExpression lambda = (LambdaExpression) iv.Expression;
                        this.locals.AddRange(lambda.Parameters);
                    } 
                    else if (iv.Expression.NodeType == ExpressionType.Quote &&
                             iv.Expression.Type.IsSubclassOf(typeof(LambdaExpression))) {
                        LambdaExpression lambda = (LambdaExpression)((UnaryExpression)iv.Expression).Operand;
                        this.locals.AddRange(lambda.Parameters);
                    }
                }
                return base.VisitInvocation(iv);
            }

            internal override Expression VisitLambda(LambdaExpression l) {
                LambdaExpression save = this.current;
                this.current = l;
                this.Visit(l.Body);
                this.current = save;
                return l;
            }
        }
 
        private Type[] GetParameterTypes(MethodInfo mi) {
            ParameterInfo[] pis = mi.GetParameters();
            Type[] types = new Type[pis.Length + 1];
            for (int i = 0, n = pis.Length; i < n; i++) {
                types[i + 1] = pis[i].ParameterType;
            }
            types[0] = typeof(ExecutionScope);
            return types;
        }

        private StackType Generate(ILGenerator gen, Expression node, StackType ask) {
            switch (node.NodeType) {
                case ExpressionType.NegateChecked:
                case ExpressionType.Negate:
                case ExpressionType.UnaryPlus:
                case ExpressionType.Not:
                case ExpressionType.ArrayLength:
                case ExpressionType.TypeAs:
                    return this.GenerateUnary(gen, (UnaryExpression)node, ask);
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.Power:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.ArrayIndex:
                case ExpressionType.Coalesce:
                case ExpressionType.LeftShift:
                case ExpressionType.RightShift:
                case ExpressionType.ExclusiveOr:
                    return this.GenerateBinary(gen, (BinaryExpression)node, ask);
                case ExpressionType.TypeIs:
                    this.GenerateTypeIs(gen, (TypeBinaryExpression)node);
                    return StackType.Value;
                case ExpressionType.Constant:
                    return this.GenerateConstant(gen, (ConstantExpression)node, ask);
                case ExpressionType.Conditional:
                    return this.GenerateConditional(gen, (ConditionalExpression)node);
                case ExpressionType.Parameter:
                    return this.GenerateParameterAccess(gen, (ParameterExpression)node, ask);
                case ExpressionType.MemberAccess:
                    return this.GenerateMemberAccess(gen, (MemberExpression)node, ask);
                case ExpressionType.Call:
                    return this.GenerateMethodCall(gen, (MethodCallExpression)node, ask);
                case ExpressionType.Lambda:
                    this.GenerateCreateDelegate(gen, (LambdaExpression)node);
                    return StackType.Value;
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    this.GenerateConvert(gen, (UnaryExpression)node);
                    return StackType.Value;
                case ExpressionType.New:
                    return this.GenerateNew(gen, (NewExpression)node, ask);
                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds:
                    this.GenerateNewArray(gen, (NewArrayExpression)node);
                    return StackType.Value;
                case ExpressionType.ListInit:
                    return this.GenerateListInit(gen, (ListInitExpression)node);
                case ExpressionType.MemberInit:
                    return this.GenerateMemberInit(gen, (MemberInitExpression)node);
                case ExpressionType.Invoke:
                    return this.GenerateInvoke(gen, (InvocationExpression)node, ask);
                case ExpressionType.Quote:
                    this.GenerateQuote(gen, (UnaryExpression)node);
                    return StackType.Value;
                default:
                    throw Error.UnhandledExpressionType(node.NodeType);
            }
        }

        //PORTED: EmitNewExpression
        private StackType GenerateNew(ILGenerator gen, NewExpression nex, StackType ask) {
            LocalBuilder loc = null;
            if (nex.Type.IsValueType) {
                loc = gen.DeclareLocal(nex.Type);
            }
            if (nex.Constructor != null) {
                ParameterInfo[] pis = nex.Constructor.GetParameters();
                this.GenerateArgs(gen, pis, nex.Arguments);

                gen.Emit(OpCodes.Newobj, nex.Constructor);
                if (nex.Type.IsValueType) {
                    gen.Emit(OpCodes.Stloc, loc);
                }
            }
            else if (nex.Type.IsValueType) {
                gen.Emit(OpCodes.Ldloca, loc);
                gen.Emit(OpCodes.Initobj, nex.Type);
            }
            else {
                ConstructorInfo ci = nex.Type.GetConstructor(System.Type.EmptyTypes);
                gen.Emit(OpCodes.Newobj, ci);
            }
            if (nex.Type.IsValueType) {
                return this.ReturnFromLocal(gen, ask, loc);
            }
            return StackType.Value;
        }

        //PORTED: EmitInvocationExpression
        private StackType GenerateInvoke(ILGenerator gen, InvocationExpression invoke, StackType ask) {
            LambdaExpression lambda = (invoke.Expression.NodeType == ExpressionType.Quote)
                ? (LambdaExpression)((UnaryExpression)invoke.Expression).Operand
                : (invoke.Expression as LambdaExpression);

            // optimization: inline code for literal lambda's directly
            if (lambda != null) {
                // evaluate args and store them in locals associated with their parameter
                for (int i = 0, n = invoke.Arguments.Count; i < n; i++) {
                    ParameterExpression p = lambda.Parameters[i];
                    this.PrepareInitLocal(gen, p);
                    this.Generate(gen, invoke.Arguments[i], StackType.Value);
                    this.GenerateInitLocal(gen, p);
                }
                return this.Generate(gen, lambda.Body, ask);
            }
            else {
                Expression expr = invoke.Expression;
                if (typeof(LambdaExpression).IsAssignableFrom(expr.Type)) {
                    // if the invoke target is a lambda expression tree, first compile it into a delegate
                    expr = Expression.Call(expr, expr.Type.GetMethod("Compile", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
                }
                expr = Expression.Call(expr, expr.Type.GetMethod("Invoke"), invoke.Arguments);

                return this.Generate(gen, expr, ask);
            }
        }

        private void GenerateQuote(ILGenerator gen, UnaryExpression quote)
        {
            GenerateLoadExecutionScope(gen);
            int iGlobal = this.AddGlobal(typeof(Expression), quote.Operand);
            this.GenerateGlobalAccess(gen, iGlobal, typeof(Expression), StackType.Value);
            if (this.scope.HoistedLocalsVar != null)
                GenerateLoadHoistedLocals(gen);
            else
                gen.Emit(OpCodes.Ldnull);
            MethodInfo miIsolate = typeof(ExecutionScope).GetMethod("IsolateExpression", BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.Public);
            gen.Emit(OpCodes.Callvirt, miIsolate);
            Type actualType = quote.Operand.GetType();
            if (actualType != typeof(Expression)) {
                gen.Emit(OpCodes.Castclass, actualType);
            }
        }

        private void GenerateBinding(ILGenerator gen, MemberBinding binding, Type objectType) {
            switch (binding.BindingType) {
                case MemberBindingType.Assignment:
                    this.GenerateMemberAssignment(gen, (MemberAssignment)binding, objectType);
                    break;
                case MemberBindingType.ListBinding:
                    this.GenerateMemberListBinding(gen, (MemberListBinding)binding);
                    break;
                case MemberBindingType.MemberBinding:
                    this.GenerateMemberMemberBinding(gen, (MemberMemberBinding)binding);
                    break;
                default:
                    throw Error.UnknownBindingType();
            }
        }

        private void GenerateMemberAssignment(ILGenerator gen, MemberAssignment binding, Type objectType) {
            this.Generate(gen, binding.Expression, StackType.Value);         
            FieldInfo fi = binding.Member as FieldInfo;
            if (fi != null) {
                gen.Emit(OpCodes.Stfld, fi);
            }
            else {
                PropertyInfo pi = binding.Member as PropertyInfo;
                MethodInfo mi = pi.GetSetMethod(true);
                if (pi != null) {
                    if (UseVirtual(mi)) {
                        if (objectType.IsValueType) {
                            gen.Emit(OpCodes.Constrained, objectType);
                        }
                        gen.Emit(OpCodes.Callvirt, mi);
                    }
                    else {
                        gen.Emit(OpCodes.Call, mi);
                    }
                }
                else {
                    throw Error.UnhandledBinding();
                }
            }
        }

        private void GenerateMemberMemberBinding(ILGenerator gen, MemberMemberBinding binding) {
            Type type = this.GetMemberType(binding.Member);
            if (binding.Member is PropertyInfo && type.IsValueType) {
                throw Error.CannotAutoInitializeValueTypeMemberThroughProperty(binding.Member);
            }
            StackType eAsk = type.IsValueType ? StackType.Address : StackType.Value;
            StackType eResult = this.GenerateMemberAccess(gen, binding.Member, eAsk);
            if (eResult != eAsk && type.IsValueType) {
                LocalBuilder loc = gen.DeclareLocal(type);
                gen.Emit(OpCodes.Stloc, loc);
                gen.Emit(OpCodes.Ldloca, loc);
            }
            if (binding.Bindings.Count == 0)
            {
                gen.Emit(OpCodes.Pop);
            }
            else
            {
                this.GenerateMemberInit(gen, binding.Bindings, false, type);
            }
        }

        private void GenerateMemberListBinding(ILGenerator gen, MemberListBinding binding) {
            Type type = this.GetMemberType(binding.Member);
            if (binding.Member is PropertyInfo && type.IsValueType) {
                throw Error.CannotAutoInitializeValueTypeElementThroughProperty(binding.Member);
            }
            StackType eAsk = type.IsValueType ? StackType.Address : StackType.Value;
            StackType eResult = this.GenerateMemberAccess(gen, binding.Member, eAsk);
            if (eResult != StackType.Address && type.IsValueType) {
                LocalBuilder loc = gen.DeclareLocal(type);
                gen.Emit(OpCodes.Stloc, loc);
                gen.Emit(OpCodes.Ldloca, loc);
            }
            this.GenerateListInit(gen, binding.Initializers, false, type);
        }

        private StackType GenerateMemberInit(ILGenerator gen, MemberInitExpression init) {
            this.Generate(gen, init.NewExpression, StackType.Value);
            LocalBuilder loc = null;
            if (init.NewExpression.Type.IsValueType && init.Bindings.Count > 0)
            {
                loc = gen.DeclareLocal(init.NewExpression.Type);
                gen.Emit(OpCodes.Stloc, loc);
                gen.Emit(OpCodes.Ldloca, loc);
            }
            this.GenerateMemberInit(gen, init.Bindings, loc == null, init.NewExpression.Type);
            if (loc != null)
            {
                gen.Emit(OpCodes.Ldloc, loc);
            }
            return StackType.Value;
        }

        private void GenerateMemberInit(ILGenerator gen, ReadOnlyCollection<MemberBinding> bindings, bool keepOnStack, Type objectType) {
            for (int i = 0, n = bindings.Count; i < n; i++) {
                if (keepOnStack || i < n - 1) 
                    gen.Emit(OpCodes.Dup);
                this.GenerateBinding(gen, bindings[i], objectType);
            }
        }

        private StackType GenerateListInit(ILGenerator gen, ListInitExpression init) {
            this.Generate(gen, init.NewExpression, StackType.Value);
            LocalBuilder loc = null;
            if (init.NewExpression.Type.IsValueType) {
                loc = gen.DeclareLocal(init.NewExpression.Type);
                gen.Emit(OpCodes.Stloc, loc);
                gen.Emit(OpCodes.Ldloca, loc);
            }
            this.GenerateListInit(gen, init.Initializers, loc == null, init.NewExpression.Type);
            if (loc != null) {
                gen.Emit(OpCodes.Ldloc, loc);
            }
            return StackType.Value;
        }

        private void GenerateListInit(ILGenerator gen, ReadOnlyCollection<ElementInit> initializers, bool keepOnStack, Type objectType) {
            for (int i = 0, n = initializers.Count; i < n; i++) {
                if (keepOnStack || i < n - 1)
                    gen.Emit(OpCodes.Dup);
                this.GenerateMethodCall(gen, initializers[i].AddMethod, initializers[i].Arguments, objectType);

                //EDMAURER some add methods, ArrayList.Add for example, return non-void
                if (initializers[i].AddMethod.ReturnType != typeof(void))
                {
                    gen.Emit(OpCodes.Pop);
                }
            }
        }

        //PORTED: EmitNewArrayExpression
        private void GenerateNewArray(ILGenerator gen, NewArrayExpression nex)
        {
            Type elemType = nex.Type.GetElementType();
            if (nex.NodeType == ExpressionType.NewArrayInit)
            {
                this.GenerateConstInt(gen, nex.Expressions.Count);
                gen.Emit(OpCodes.Newarr, elemType);
                for (int i = 0, n = nex.Expressions.Count; i < n; i++)
                {
                    gen.Emit(OpCodes.Dup);
                    this.GenerateConstInt(gen, i);
                    this.Generate(gen, nex.Expressions[i], StackType.Value);
                    this.GenerateArrayAssign(gen, elemType);
                }
            }
            else
            {
                Type[] types = new Type[nex.Expressions.Count];
                for (int i = 0, n = types.Length; i < n; i++)
                {
                    types[i] = typeof(int);
                }
                for (int i = 0, n = nex.Expressions.Count; i < n; i++)
                {
                    Expression x = nex.Expressions[i];
                    this.Generate(gen, x, StackType.Value);
                    if (x.Type != typeof(int))
                    {
                        this.GenerateConvertToType(gen, x.Type, typeof(int), true);
                    }
                }
                if (nex.Expressions.Count > 1)
                {
                    int[] bounds = new int[nex.Expressions.Count];
                    ConstructorInfo ci = Array.CreateInstance(elemType, bounds).GetType().GetConstructor(types);
                    gen.Emit(OpCodes.Newobj, ci);
                }
                else
                {
                    gen.Emit(OpCodes.Newarr, elemType);
                }
            }
        }

        private void GenerateConvert(ILGenerator gen, UnaryExpression u) {
            if (u.Method != null) {
                // User-defined conversions are only lifted if both source and
                // destination types are value types.  The C# compiler gets this wrong.
                // In C#, if you have an implicit conversion from int->MyClass and you
                // "lift" the conversion to int?->MyClass then a null int? goes to a
                // null MyClass.  This is contrary to the specification, which states
                // that the correct behaviour is to unwrap the int?, throw an exception
                // if it is null, and then call the conversion.
                //
                // We cannot fix this in C# but there is no reason why we need to
                // propagate this bug into the expression tree API.  Unfortunately
                // this means that when the C# compiler generates the lambda
                // (int? i)=>(MyClass)i, we will get different results for converting
                // that lambda to a delegate directly and converting that lambda to
                // an expression tree and then compiling it.  We can live with this
                // discrepancy however.

                if (u.IsLifted && (!u.Type.IsValueType || !u.Operand.Type.IsValueType))
                {
                    ParameterInfo[] pis = u.Method.GetParameters();
                    Debug.Assert(pis != null && pis.Length == 1);
                    Type paramType = pis[0].ParameterType;
                    if (paramType.IsByRef)
                        paramType = paramType.GetElementType();
                    Expression e = Expression.Convert(
                        Expression.Call(null, u.Method,
                            Expression.Convert(u.Operand, pis[0].ParameterType)),
                        u.Type);
                    this.Generate(gen, e, StackType.Value);
                }
                else
                {
                    this.GenerateUnaryMethod(gen, u, StackType.Value);
                }
            }
            else {
                this.Generate(gen, u.Operand, StackType.Value);
                this.GenerateConvertToType(gen, u.Operand.Type, u.Type, u.NodeType == ExpressionType.ConvertChecked);
            }
        }

        private void GenerateCreateDelegate(ILGenerator gen, LambdaExpression lambda) {
            int iLambda = this.GenerateLambda(lambda);
            GenerateLoadExecutionScope(gen);
            this.GenerateConstInt(gen, iLambda);
            if (this.scope.HoistedLocalsVar != null)
                GenerateLoadHoistedLocals(gen);
            else
                gen.Emit(OpCodes.Ldnull);
            gen.Emit(OpCodes.Callvirt, typeof(ExecutionScope).GetMethod("CreateDelegate", BindingFlags.Instance | BindingFlags.Public));
            gen.Emit(OpCodes.Castclass, lambda.Type);
        }

        //PORTED: EmitMethodCallExpression
        private StackType GenerateMethodCall(ILGenerator gen, MethodCallExpression mc, StackType ask)
        {
            StackType ret = StackType.Value;
            MethodInfo mi = mc.Method;
            if (!mc.Method.IsStatic)
            {
                StackType eAsk = mc.Object.Type.IsValueType ? StackType.Address : StackType.Value;
                StackType eResult = this.Generate(gen, mc.Object, eAsk);
                if (eResult != eAsk)
                {
                    Debug.Assert(eResult == StackType.Value && eAsk == StackType.Address);
                    LocalBuilder loc = gen.DeclareLocal(mc.Object.Type);
                    gen.Emit(OpCodes.Stloc, loc);
                    gen.Emit(OpCodes.Ldloca, loc);
                }
                // An array index of a multi-dimensional array is represented by a call to Array.Get,
                // rather than having its own array-access node. This means that when we are trying to
                // get the address of a member of a multi-dimensional array, we'll be trying to
                // get the address of a Get method, and it will fail to do so. Instead, detect
                // this situation and replace it with a call to the Address method.
                // See DevDiv Bugs #132062 for more details.
                if (ask == StackType.Address && mc.Object.Type.IsArray &&
                    mi == mc.Object.Type.GetMethod("Get", BindingFlags.Public|BindingFlags.Instance))
                {
                    mi = mc.Object.Type.GetMethod("Address", BindingFlags.Public|BindingFlags.Instance);
                    ret = StackType.Address;
                }
            }
            this.GenerateMethodCall(gen, mi, mc.Arguments, mc.Object == null ? null : mc.Object.Type);
            return ret;
        }

        //PORTED: EmitMethodCallExpression
        // assumes 'object' of non-static call is already on stack
        private void GenerateMethodCall(ILGenerator gen, MethodInfo mi, ReadOnlyCollection<Expression> args, Type objectType) {
            ParameterInfo[] pis = mi.GetParameters();
            List<WriteBack> locals = this.GenerateArgs(gen, pis, args);
            
            OpCode callOp = UseVirtual(mi) ? OpCodes.Callvirt : OpCodes.Call;
            // A static method had better not be virtual.
            Debug.Assert(callOp != OpCodes.Callvirt || objectType != null);
            if (callOp == OpCodes.Callvirt && objectType.IsValueType)
            {
                // This automatically boxes value types if necessary.
                gen.Emit(OpCodes.Constrained, objectType);
            }
            if (mi.CallingConvention == CallingConventions.VarArgs) {
                Type[] optionalParamTypes = new Type[args.Count];
                for (int i = 0, n = optionalParamTypes.Length; i < n; i++) {
                    optionalParamTypes[i] = args[i].Type;
                }

                gen.EmitCall(callOp, mi, optionalParamTypes);
            }
            else {
                gen.Emit(callOp, mi);
            }

            foreach (WriteBack local in locals)
            {
                GenerateWriteBack(gen, local);
            }
        }

        //PORTED
        private struct WriteBack
        {
            public LocalBuilder loc;
            public Expression arg;
            public WriteBack(LocalBuilder loc, Expression arg)
            {
                this.loc = loc;
                this.arg = arg;
            }
        }

        //PORTED: EmitArguments
        private List<WriteBack> GenerateArgs(ILGenerator gen, ParameterInfo[] pis, ReadOnlyCollection<Expression> args){
            List<WriteBack> locals = new List<WriteBack>();
            for (int i = 0, n = pis.Length; i < n; i++) {
                ParameterInfo pi = pis[i];
                Expression arg = args[i];
                StackType eAsk = pi.ParameterType.IsByRef ? StackType.Address : StackType.Value;
                StackType eHas = this.Generate(gen, arg, eAsk);
                if (eAsk == StackType.Address && eHas != StackType.Address) {
                    LocalBuilder loc = gen.DeclareLocal(arg.Type);
                    gen.Emit(OpCodes.Stloc, loc);
                    gen.Emit(OpCodes.Ldloca, loc);
                    // CONSIDER: [Eric Lippert] Are there any other situations where we
                    // CONSIDER: have a value on the stack but need to do a copy-out
                    // CONSIDER: to implement by-reference-like semantics?
                    if(args[i] is MemberExpression)
                        locals.Add(new WriteBack(loc, args[i]));
                }
            }
            return locals;
        }

        //PORTED
        private StackType GenerateLift(ILGenerator gen, ExpressionType nodeType, Type resultType, MethodCallExpression mc, IEnumerable<ParameterExpression> parameters, IEnumerable<Expression> arguments) {
            System.Diagnostics.Debug.Assert(GetNonNullableType(resultType) == GetNonNullableType(mc.Type));
            ReadOnlyCollection<ParameterExpression> paramList = parameters.ToReadOnlyCollection();
            ReadOnlyCollection<Expression> argList = arguments.ToReadOnlyCollection();

            switch (nodeType){
                default:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                    {
                        Label exit = gen.DefineLabel();
                        Label exitNull = gen.DefineLabel();
                        LocalBuilder anyNull = gen.DeclareLocal(typeof(bool));
                        for (int i = 0, n = paramList.Count; i < n; i++){
                            ParameterExpression p = paramList[i];
                            Expression arg = argList[i];
                            if (IsNullable(arg.Type)){
                                this.PrepareInitLocal(gen, p);
                                StackType result = this.Generate(gen, arg, StackType.Address);
                                if (result == StackType.Value){
                                    LocalBuilder tmp = gen.DeclareLocal(arg.Type);
                                    gen.Emit(OpCodes.Stloc, tmp);
                                    gen.Emit(OpCodes.Ldloca, tmp);
                                }
                                gen.Emit(OpCodes.Dup);
                                this.GenerateHasValue(gen, arg.Type);
                                gen.Emit(OpCodes.Ldc_I4_0);
                                gen.Emit(OpCodes.Ceq);
                                gen.Emit(OpCodes.Stloc, anyNull);
                                this.GenerateGetValueOrDefault(gen, arg.Type);
                                this.GenerateInitLocal(gen, p);
                            }
                            else{
                                this.PrepareInitLocal(gen, p);
                                this.Generate(gen, arg, StackType.Value);
                                if (!arg.Type.IsValueType){
                                    gen.Emit(OpCodes.Dup);
                                    gen.Emit(OpCodes.Ldnull);
                                    gen.Emit(OpCodes.Ceq);
                                    gen.Emit(OpCodes.Stloc, anyNull);
                                }
                                this.GenerateInitLocal(gen, p);
                            }
                            gen.Emit(OpCodes.Ldloc, anyNull);
                            gen.Emit(OpCodes.Brtrue, exitNull);
                        }
                        this.Generate(gen, mc, StackType.Value);
                        if (IsNullable(resultType) && resultType != mc.Type){
                            ConstructorInfo ci = resultType.GetConstructor(new Type[] { mc.Type });
                            gen.Emit(OpCodes.Newobj, ci);
                        }
                        gen.Emit(OpCodes.Br_S, exit);
                        gen.MarkLabel(exitNull);
                        if (resultType == Expression.GetNullableType(mc.Type)){
                            if (resultType.IsValueType){
                                LocalBuilder result = gen.DeclareLocal(resultType);
                                gen.Emit(OpCodes.Ldloca, result);
                                gen.Emit(OpCodes.Initobj, resultType);
                                gen.Emit(OpCodes.Ldloc, result);
                            }
                            else{
                                gen.Emit(OpCodes.Ldnull);
                            }
                        }
                        else{
                            switch (nodeType){
                                case ExpressionType.LessThan:
                                case ExpressionType.LessThanOrEqual:
                                case ExpressionType.GreaterThan:
                                case ExpressionType.GreaterThanOrEqual:
                                    gen.Emit(OpCodes.Ldc_I4_0);
                                    break;
                                default:
                                    Debug.Assert("Unknown Lift Type" == null);
                                    break;
                            }
                        }
                        gen.MarkLabel(exit);
                        return StackType.Value;
                    }
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:{
                    if (resultType == Expression.GetNullableType(mc.Type))
                        goto default;
                        Label exit = gen.DefineLabel();
                        Label exitAllNull = gen.DefineLabel();
                        Label exitAnyNull = gen.DefineLabel();

                        LocalBuilder anyNull = gen.DeclareLocal(typeof(bool));
                        LocalBuilder allNull = gen.DeclareLocal(typeof(bool));
                        gen.Emit(OpCodes.Ldc_I4_0);
                        gen.Emit(OpCodes.Stloc, anyNull);
                        gen.Emit(OpCodes.Ldc_I4_1);
                        gen.Emit(OpCodes.Stloc, allNull);

                        for (int i = 0, n = paramList.Count; i < n; i++){
                            ParameterExpression p = paramList[i];
                            Expression arg = argList[i];
                            this.PrepareInitLocal(gen, p);
                            if (IsNullable(arg.Type)){
                                StackType result = this.Generate(gen, arg, StackType.Address);
                                if (result == StackType.Value){
                                    LocalBuilder tmp = gen.DeclareLocal(arg.Type);
                                    gen.Emit(OpCodes.Stloc, tmp);
                                    gen.Emit(OpCodes.Ldloca, tmp);
                                }
                                gen.Emit(OpCodes.Dup);
                                this.GenerateHasValue(gen, arg.Type);
                                gen.Emit(OpCodes.Ldc_I4_0);
                                gen.Emit(OpCodes.Ceq);
                                gen.Emit(OpCodes.Dup);
                                gen.Emit(OpCodes.Ldloc, anyNull);
                                gen.Emit(OpCodes.Or);
                                gen.Emit(OpCodes.Stloc, anyNull);
                                gen.Emit(OpCodes.Ldloc, allNull);
                                gen.Emit(OpCodes.And);
                                gen.Emit(OpCodes.Stloc, allNull);
                                this.GenerateGetValueOrDefault(gen, arg.Type);
                            }
                            else{
                                this.Generate(gen, arg, StackType.Value);
                                if (!arg.Type.IsValueType){
                                    gen.Emit(OpCodes.Dup);
                                    gen.Emit(OpCodes.Ldnull);
                                    gen.Emit(OpCodes.Ceq);
                                    gen.Emit(OpCodes.Dup);
                                    gen.Emit(OpCodes.Ldloc, anyNull);
                                    gen.Emit(OpCodes.Or);
                                    gen.Emit(OpCodes.Stloc, anyNull);
                                    gen.Emit(OpCodes.Ldloc, allNull);
                                    gen.Emit(OpCodes.And);
                                    gen.Emit(OpCodes.Stloc, allNull);
                                }
                                else{
                                    gen.Emit(OpCodes.Ldc_I4_0);
                                    gen.Emit(OpCodes.Stloc, allNull);
                                }
                            }
                            this.GenerateInitLocal(gen, p);
                        }
                        gen.Emit(OpCodes.Ldloc, allNull);
                        gen.Emit(OpCodes.Brtrue, exitAllNull);
                        gen.Emit(OpCodes.Ldloc, anyNull);
                        gen.Emit(OpCodes.Brtrue, exitAnyNull);

                        this.Generate(gen, mc, StackType.Value);
                        if (IsNullable(resultType) && resultType != mc.Type){
                            ConstructorInfo ci = resultType.GetConstructor(new Type[] { mc.Type });
                            gen.Emit(OpCodes.Newobj, ci);
                        }
                        gen.Emit(OpCodes.Br_S, exit);

                        gen.MarkLabel(exitAllNull);
                        bool value = nodeType == ExpressionType.Equal;
                        this.GenerateConstant(gen, Expression.Constant(value), StackType.Value);
                        gen.Emit(OpCodes.Br_S, exit);

                        gen.MarkLabel(exitAnyNull);
                        value = nodeType == ExpressionType.NotEqual;
                        this.GenerateConstant(gen, Expression.Constant(value), StackType.Value);

                        gen.MarkLabel(exit);
                        return StackType.Value;
                    }
            }
        }

        //PORTED
        private StackType GenerateMemberAccess(ILGenerator gen, MemberExpression m, StackType ask) {
            return this.GenerateMemberAccess(gen, m.Expression, m.Member, ask);
        }

        //PORTED
        private StackType GenerateMemberAccess(ILGenerator gen, Expression expression, MemberInfo member, StackType ask){
            FieldInfo fi = member as FieldInfo;
            if (fi != null) {
                if (!fi.IsStatic) {
                    StackType eAsk = expression.Type.IsValueType ? StackType.Address : StackType.Value; 
                    StackType eResult = this.Generate(gen, expression, eAsk);
                    if (eResult != eAsk) {
                        LocalBuilder loc = gen.DeclareLocal(expression.Type);
                        gen.Emit(OpCodes.Stloc, loc);
                        gen.Emit(OpCodes.Ldloca, loc);
                    }
                }
               return this.GenerateMemberAccess(gen, member, ask);
            }
            else {
                PropertyInfo pi = member as PropertyInfo;
                if (pi != null) {
                    MethodInfo mi = pi.GetGetMethod(true);
                    if (!mi.IsStatic) {
                        StackType eAsk = expression.Type.IsValueType ? StackType.Address : StackType.Value;
                        StackType eResult = this.Generate(gen, expression, eAsk);
                        if (eResult != eAsk) {
                            LocalBuilder loc = gen.DeclareLocal(expression.Type);
                            gen.Emit(OpCodes.Stloc, loc);
                            gen.Emit(OpCodes.Ldloca, loc);
                        }
                    }
                    return this.GenerateMemberAccess(gen, member, ask);
                }
                else {
                    throw Error.UnhandledMemberAccess(member);
                }
            }
        }

        //PORTED: EmitWriteBack
        private void GenerateWriteBack(ILGenerator gen, WriteBack writeback)
        {
            MemberExpression memberExpression = writeback.arg as MemberExpression;
            if (memberExpression != null)
            {
                GenerateMemberWriteBack(gen, memberExpression.Expression, memberExpression.Member, writeback.loc);
                return;
            }
            Debug.Fail("Unexpected writeback kind");
        }

        //PORTED: EmitWriteBack
        private void GenerateMemberWriteBack(ILGenerator gen, Expression expression, MemberInfo member, LocalBuilder loc)
        {
            FieldInfo fi = member as FieldInfo;
            if (fi != null)
            {
                if (!fi.IsStatic)
                {
                    StackType eAsk = expression.Type.IsValueType ? StackType.Address : StackType.Value;
                    this.Generate(gen, expression, eAsk);
                    gen.Emit(OpCodes.Ldloc, loc);
                    gen.Emit(OpCodes.Stfld, fi);
                }
                else
                {
                    gen.Emit(OpCodes.Ldloc, loc);
                    gen.Emit(OpCodes.Stsfld, fi);
                }
            }
            else
            {
                PropertyInfo pi = member as PropertyInfo;
                if (pi != null)
                {
                    MethodInfo mi = pi.GetSetMethod(true);
                    if (mi != null)
                    {
                        if (!mi.IsStatic)
                        {
                            StackType eAsk = expression.Type.IsValueType ? StackType.Address : StackType.Value;
                            this.Generate(gen, expression, eAsk);
                        }
                        gen.Emit(OpCodes.Ldloc, loc);
                        gen.Emit(UseVirtual(mi) ? OpCodes.Callvirt : OpCodes.Call, mi);
                    }
                }
                else
                {
                    throw Error.UnhandledMemberAccess(member);
                }
            }
        }

        private bool UseVirtual(MethodInfo mi)
        {
            // There are two factors: is the method static, virtual or non-virtual instance?
            // And is the object ref or value?
            // The cases are:
            //
            // static, ref:     call
            // static, value:   call
            // virtual, ref:    callvirt
            // virtual, value:  call -- eg, double.ToString must be a non-virtual call to be verifiable.
            // instance, ref:   callvirt -- this looks wrong, but is verifiable and gives us a free null check.
            // instance, value: call
            //
            // We never need to generate a nonvirtual call to a virtual method on a reference type because
            // expression trees do not support "base.Foo()" style calling.
            // 
            // We could do an optimization here for the case where we know that the object is a non-null
            // reference type and the method is a non-virtual instance method.  For example, if we had
            // (new Foo()).Bar() for instance method Bar we don't need the null check so we could do a
            // call rather than a callvirt.  However that seems like it would not be a very big win for
            // most dynamically generated code scenarios, so let's not do that for now.
            
            if (mi.IsStatic)
                return false;
            if (mi.DeclaringType.IsValueType)
                return false;
            return true;
        }

        private void GenerateFieldAccess(ILGenerator gen, FieldInfo fi, StackType ask)
        {
            // Verifiable code may not take the address of an init-only field.
            // If we are asked to do so then get the value out of the field, stuff it
            // into a local of the same type, and then take the address of the local.
            // Typically this is what we want to do anyway; if we are saying
            // Foo.bar.ToString() for a static value-typed field bar then we don't need
            // the address of field bar to do the call.  The address of a local which
            // has the same value as bar is sufficient.

            // CONSIDER: The C# compiler will not compile a lambda expression tree 
            // CONSIDER: which writes to the address of an init-only field. But one could
            // CONSIDER: probably use the expression tree API to build such an expression.
            // CONSIDER: (When compiled, such an expression would fail silently.)  It might
            // CONSIDER: be worth it to add checking to the expression tree API to ensure
            // CONSIDER: that it is illegal to attempt to write to an init-only field,
            // CONSIDER: the same way that it is illegal to write to a read-only property.
            // CONSIDER: The same goes for literal fields.

            StackType got;

            if (fi.IsLiteral)
            {
                got = GenerateConstant(gen, fi.FieldType, fi.GetRawConstantValue(), ask);
            }
            else
            {
                OpCode op;
                if (ask == StackType.Value || fi.IsInitOnly)
                {
                    op = fi.IsStatic ? OpCodes.Ldsfld : OpCodes.Ldfld;
                    got = StackType.Value;
                }
                else
                {
                    op = fi.IsStatic ? OpCodes.Ldsflda : OpCodes.Ldflda;
                    got = StackType.Address;
                }
                gen.Emit(op, fi);
            }
            if (ask == StackType.Address && got == StackType.Value)
            {
                LocalBuilder loc = gen.DeclareLocal(fi.FieldType);
                gen.Emit(OpCodes.Stloc, loc);
                gen.Emit(OpCodes.Ldloca, loc);
            }
        }
        
        //PORTED
        private StackType GenerateMemberAccess(ILGenerator gen, MemberInfo member, StackType ask) {           
            FieldInfo fi = member as FieldInfo;
            if (fi != null) {
                GenerateFieldAccess(gen, fi, ask);
                return ask;
            }
            else {
                PropertyInfo pi = member as PropertyInfo;
                if (pi != null) {
                    MethodInfo mi = pi.GetGetMethod(true);
                    gen.Emit(UseVirtual(mi) ? OpCodes.Callvirt : OpCodes.Call, mi);
                    return StackType.Value;
                }
                else {
                    throw Error.UnhandledMemberAccess(member);
                }
            }
        }

        private StackType GenerateParameterAccess(ILGenerator gen, ParameterExpression p, StackType ask)
        {
            // check for parameter mapped to local
            LocalBuilder local;
            if (this.scope.Locals.TryGetValue(p, out local))
            {
                if (ask == StackType.Value)
                {
                    gen.Emit(OpCodes.Ldloc, local);
                }
                else
                {
                    gen.Emit(OpCodes.Ldloca, local);
                }
                return ask;
            }

            // check for parameter mapped to hoisted local
            int hoistIndex;
            if (this.scope.HoistedLocals.TryGetValue(p, out hoistIndex))
            {
                GenerateLoadHoistedLocals(gen);
                return this.GenerateHoistedLocalAccess(gen, hoistIndex, p.Type, ask);
            }

            // check for local scope's arg
            for (int i = 0, n = this.scope.Lambda.Parameters.Count; i < n; i++)
            {
                if (this.scope.Lambda.Parameters[i] == p)
                {
                    return this.GenerateArgAccess(gen, i + 1, ask);
                }
            }

            // check for hoisted local from outer scope

            // See the comment in GenerateInitHoistedLocals for an explanation of
            // what this code is supposed to be doing.
            
            GenerateLoadExecutionScope(gen);
            for (CompileScope s = this.scope.Parent; s != null; s = s.Parent) 
            {
                if (s.HoistedLocals.TryGetValue(p, out hoistIndex)) 
                {
                    gen.Emit(OpCodes.Ldfld, typeof(ExecutionScope).GetField("Locals", BindingFlags.Public | BindingFlags.Instance));
                    return this.GenerateHoistedLocalAccess(gen, hoistIndex, p.Type, ask);
                }
                gen.Emit(OpCodes.Ldfld, typeof(ExecutionScope).GetField("Parent", BindingFlags.Public | BindingFlags.Instance));
            }
            throw Error.LambdaParameterNotInScope();
        }

        //PORTED: EmitConstantExpression
        private StackType GenerateConstant(ILGenerator gen, ConstantExpression c, StackType ask)
        {
            return GenerateConstant(gen, c.Type, c.Value, ask);
        }

        //PORTED: EmitConstant
        private StackType GenerateConstant(ILGenerator gen, Type type, object value, StackType ask)
        {
            if (value == null) {
                if (type.IsValueType) {
                    LocalBuilder loc = gen.DeclareLocal(type);
                    gen.Emit(OpCodes.Ldloca, loc);
                    gen.Emit(OpCodes.Initobj, type);
                    gen.Emit(OpCodes.Ldloc, loc);
                }
                else {
                    gen.Emit(OpCodes.Ldnull);
                }
            } 
            else {
                TypeCode tc = Type.GetTypeCode(type);
                switch (tc) {
                    case TypeCode.Boolean:
                        this.GenerateConstInt(gen, (bool)value ? 1 : 0);
                        break;
                    case TypeCode.SByte:
                        this.GenerateConstInt(gen, (SByte)value);
                        gen.Emit(OpCodes.Conv_I1);
                        break;
                    case TypeCode.Int16:
                        this.GenerateConstInt(gen, (Int16)value);
                        gen.Emit(OpCodes.Conv_I2);
                        break;
                    case TypeCode.Int32:
                        this.GenerateConstInt(gen, (Int32)value);
                        break;
                    case TypeCode.Int64:
                        gen.Emit(OpCodes.Ldc_I8, (Int64)value);
                        break;
                    case TypeCode.Single:
                        gen.Emit(OpCodes.Ldc_R4, (float)value);
                        break;
                    case TypeCode.Double:
                        gen.Emit(OpCodes.Ldc_R8, (double)value);
                        break;
                    default:
                        int iGlobal = this.AddGlobal(type, value);
                        return this.GenerateGlobalAccess(gen, iGlobal, type, ask);
                }
            }
            return StackType.Value;
        }

        //PORTED
        private StackType GenerateUnary(ILGenerator gen, UnaryExpression u, StackType ask) {
            if (u.Method != null) {
                return this.GenerateUnaryMethod(gen, u, ask);
            }
            else if (u.NodeType == ExpressionType.NegateChecked && IsInteger(u.Operand.Type)) {
                this.GenerateConstInt(gen, 0);
                this.GenerateConvertToType(gen, typeof(int), u.Operand.Type, false);
                this.Generate(gen, u.Operand, StackType.Value);
                return this.GenerateBinaryOp(gen, ExpressionType.SubtractChecked, u.Operand.Type, u.Operand.Type, u.Type, false, ask);
            }
            else {
                this.Generate(gen, u.Operand, StackType.Value);
                return this.GenerateUnaryOp(gen, u.NodeType, u.Operand.Type, u.Type, ask);
            }
        }

        //PORTED: TypeUtils
        private static bool IsInteger(Type type) {
            type = GetNonNullableType(type);
            if (type.IsEnum)
                return false;
            TypeCode tc = Type.GetTypeCode(type);
            switch (tc) {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                default:
                    return false;
            }
        }

        private StackType GenerateUnaryMethod(ILGenerator gen, UnaryExpression u, StackType ask) {
            if (u.IsLifted) {
                ParameterExpression p = Expression.Parameter(Expression.GetNonNullableType(u.Operand.Type), null);
                MethodCallExpression mc = Expression.Call(null, u.Method, p);

                Type resultType = Expression.GetNullableType(mc.Type);
                this.GenerateLift(gen, u.NodeType, resultType, mc, new ParameterExpression[] {p}, new Expression[] {u.Operand});
                this.GenerateConvertToType(gen, resultType, u.Type, false);
                return StackType.Value;
            }
            else {
                MethodCallExpression mc = Expression.Call(null, u.Method, u.Operand);
                return this.Generate(gen, mc, ask);
            }
        }

        //PORTED
        private StackType GenerateConditional(ILGenerator gen, ConditionalExpression b) {
            System.Diagnostics.Debug.Assert(b.Test.Type == typeof(bool) && b.IfTrue.Type == b.IfFalse.Type);
            Label labFalse = gen.DefineLabel();
            Label labEnd = gen.DefineLabel();

            this.Generate(gen, b.Test, StackType.Value);           
            gen.Emit(OpCodes.Brfalse, labFalse);

            this.Generate(gen, b.IfTrue, StackType.Value);
            gen.Emit(OpCodes.Br, labEnd);

            gen.MarkLabel(labFalse);
            this.Generate(gen, b.IfFalse, StackType.Value);

            gen.MarkLabel(labEnd);
            return StackType.Value;
        }

        //PORTED
        private void GenerateCoalesce(ILGenerator gen, BinaryExpression b) {
            if (IsNullable(b.Left.Type)) 
                GenerateNullableCoalesce(gen, b);
            else if (b.Left.Type.IsValueType) 
                throw Error.CoalesceUsedOnNonNullType();
            else if (b.Conversion != null)
                GenerateLambdaReferenceCoalesce(gen, b);
            else if (b.Method != null)
            // methodinfo coalesces will probably be deprecated/removed before beta 2.
                GenerateUserDefinedReferenceCoalesce(gen, b);
            else 
                GenerateReferenceCoalesceWithoutConversion(gen, b);
        }

        //PORTED
        private void GenerateNullableCoalesce(ILGenerator gen, BinaryExpression b) {
            LocalBuilder loc = gen.DeclareLocal(b.Left.Type);
            Label labIfNull = gen.DefineLabel();
            Label labEnd = gen.DefineLabel();
            this.Generate(gen, b.Left, StackType.Value);
            gen.Emit(OpCodes.Stloc, loc);
            gen.Emit(OpCodes.Ldloca, loc);
            this.GenerateHasValue(gen, b.Left.Type);
            gen.Emit(OpCodes.Brfalse, labIfNull);

            Type nnLeftType = GetNonNullableType(b.Left.Type);
            if (b.Method != null) {
                ParameterInfo[] parameters = b.Method.GetParameters();
                Debug.Assert(b.Method.IsStatic);
                Debug.Assert(parameters.Length == 1);
                Debug.Assert(parameters[0].ParameterType.IsAssignableFrom(b.Left.Type) ||
                             parameters[0].ParameterType.IsAssignableFrom(nnLeftType));
                if (!parameters[0].ParameterType.IsAssignableFrom(b.Left.Type))
                {
                    gen.Emit(OpCodes.Ldloca, loc);
                    this.GenerateGetValueOrDefault(gen, b.Left.Type);
                }
                else                    
                    gen.Emit(OpCodes.Ldloc, loc);
                gen.Emit(OpCodes.Call, b.Method);
            }
            else if (b.Conversion != null)
            {
                Debug.Assert(b.Conversion.Parameters.Count == 1);
                ParameterExpression p = b.Conversion.Parameters[0];
                Debug.Assert(p.Type.IsAssignableFrom(b.Left.Type) ||
                             p.Type.IsAssignableFrom(nnLeftType));
                this.PrepareInitLocal(gen, p);
                if (!p.Type.IsAssignableFrom(b.Left.Type))
                {
                    gen.Emit(OpCodes.Ldloca, loc);
                    this.GenerateGetValueOrDefault(gen, b.Left.Type);
                }
                else
                    gen.Emit(OpCodes.Ldloc, loc);
                this.GenerateInitLocal(gen, p);
                Generate(gen, b.Conversion.Body, StackType.Value);
            }
            else if (b.Type != nnLeftType) {
                gen.Emit(OpCodes.Ldloca, loc);
                this.GenerateGetValueOrDefault(gen, b.Left.Type);
                this.GenerateConvertToType(gen, nnLeftType, b.Type, true);
            }
            else
            {
                gen.Emit(OpCodes.Ldloca, loc);
                this.GenerateGetValueOrDefault(gen, b.Left.Type);
            }
            
            gen.Emit(OpCodes.Br, labEnd);
            gen.MarkLabel(labIfNull);
            this.Generate(gen, b.Right, StackType.Value);
            if (b.Right.Type != b.Type) {
                this.GenerateConvertToType(gen, b.Right.Type, b.Type, true);
            }
            gen.MarkLabel(labEnd);
        }

        //PORTED
        private void GenerateLambdaReferenceCoalesce(ILGenerator gen, BinaryExpression b)
        {
            Label labEnd = gen.DefineLabel();
            Label labNotNull = gen.DefineLabel();
            this.Generate(gen, b.Left, StackType.Value);
            gen.Emit(OpCodes.Dup);
            gen.Emit(OpCodes.Ldnull);
            gen.Emit(OpCodes.Ceq);
            gen.Emit(OpCodes.Brfalse, labNotNull);
            gen.Emit(OpCodes.Pop);
            this.Generate(gen, b.Right, StackType.Value);
            gen.Emit(OpCodes.Br, labEnd);
            gen.MarkLabel(labNotNull);
            Debug.Assert(b.Conversion.Parameters.Count == 1);
            ParameterExpression p = b.Conversion.Parameters[0];
            this.PrepareInitLocal(gen, p);
            this.GenerateInitLocal(gen, p);
            Generate(gen, b.Conversion.Body, StackType.Value);
            gen.MarkLabel(labEnd);
        }

        //PORTED
        private void GenerateUserDefinedReferenceCoalesce(ILGenerator gen, BinaryExpression b) {
            Label labEnd = gen.DefineLabel();
            Label labNotNull = gen.DefineLabel();
            this.Generate(gen, b.Left, StackType.Value);
            gen.Emit(OpCodes.Dup);
            gen.Emit(OpCodes.Ldnull);
            gen.Emit(OpCodes.Ceq);
            gen.Emit(OpCodes.Brfalse, labNotNull);
            gen.Emit(OpCodes.Pop);
            this.Generate(gen, b.Right, StackType.Value);
            gen.Emit(OpCodes.Br_S, labEnd);
            gen.MarkLabel(labNotNull);
            Debug.Assert(b.Method.IsStatic);
            gen.Emit(OpCodes.Call, b.Method);
            gen.MarkLabel(labEnd);
        }

        //PORTED
        private void GenerateReferenceCoalesceWithoutConversion(ILGenerator gen, BinaryExpression b) {
            Label labEnd = gen.DefineLabel();
            Label labCast = gen.DefineLabel();
            this.Generate(gen, b.Left, StackType.Value);
            gen.Emit(OpCodes.Dup);
            gen.Emit(OpCodes.Ldnull);
            gen.Emit(OpCodes.Ceq);
            gen.Emit(OpCodes.Brfalse, labCast);
            gen.Emit(OpCodes.Pop);
            this.Generate(gen, b.Right, StackType.Value);
            if (b.Right.Type != b.Type)
            {
                gen.Emit(OpCodes.Castclass, b.Type);
            }
            gen.Emit(OpCodes.Br_S, labEnd);
            gen.MarkLabel(labCast);
            if (b.Left.Type != b.Type)
            {
                gen.Emit(OpCodes.Castclass, b.Type);
            }
            gen.MarkLabel(labEnd);
        }

        // for a userdefined type T which has Op_False defined and Lhs, Rhs are nullable L AndAlso R  is computed as
        // L.HasValue 
        //      ? (T.False(L.Value) 
        //          ? L 
        //            : (R.HasValue 
        //            ? (T?)(T.&(L.Value, R.Value)) 
        //               : R)
        //        : L

        //PORTED
        private StackType GenerateUserdefinedLiftedAndAlso(ILGenerator gen, BinaryExpression b, StackType ask) {
            Type type = b.Left.Type;
            Type nnType = GetNonNullableType(type);
            Label labReturnLeft = gen.DefineLabel();
            Label labReturnRight = gen.DefineLabel();
            Label labExit = gen.DefineLabel();

            LocalBuilder locLeft = gen.DeclareLocal(type);
            LocalBuilder locRight = gen.DeclareLocal(type);
            LocalBuilder locNNLeft = gen.DeclareLocal(nnType);
            LocalBuilder locNNRight = gen.DeclareLocal(nnType);

            // load left
            this.Generate(gen, b.Left, StackType.Value);
            gen.Emit(OpCodes.Stloc, locLeft);
            //load right 
            this.Generate(gen, b.Right, StackType.Value);
            gen.Emit(OpCodes.Stloc, locRight);
            
            //check left
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateHasValue(gen, type);
            gen.Emit(OpCodes.Brfalse, labExit);
            
            //try false on left
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateGetValueOrDefault(gen, type);
            Type[] types = new Type[] { nnType };
            MethodInfo opTrue = nnType.GetMethod("op_False",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
            gen.Emit(OpCodes.Call, opTrue);
            gen.Emit(OpCodes.Brtrue, labExit);

            // Check right
            gen.Emit(OpCodes.Ldloca, locRight);
            this.GenerateHasValue(gen, type);
            gen.Emit(OpCodes.Brfalse, labReturnRight);

            //Compute bitwise And
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateGetValueOrDefault(gen, type);
            gen.Emit(OpCodes.Stloc, locNNLeft);
            gen.Emit(OpCodes.Ldloca, locRight);
            this.GenerateGetValueOrDefault(gen, type);
            gen.Emit(OpCodes.Stloc, locNNRight);
            types = new Type[] {  nnType,  nnType };
            MethodInfo opAnd = nnType.GetMethod("op_BitwiseAnd",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
            gen.Emit(OpCodes.Ldloc, locNNLeft);
            gen.Emit(OpCodes.Ldloc, locNNRight);
            gen.Emit(OpCodes.Call, opAnd);
            if(opAnd.ReturnType != type)
                GenerateConvertToType(gen, opAnd.ReturnType, type, true);
            gen.Emit(OpCodes.Stloc, locLeft);
            gen.Emit(OpCodes.Br, labExit);

            //return right
            gen.MarkLabel(labReturnRight);
            gen.Emit(OpCodes.Ldloc, locRight);
            gen.Emit(OpCodes.Stloc, locLeft);
            gen.MarkLabel(labExit);
            //return left
            return this.ReturnFromLocal(gen, ask, locLeft);
        }

        //PORTED
        private StackType GenerateLiftedAndAlso(ILGenerator gen, BinaryExpression b, StackType ask) {
            Type type = typeof(bool?);
            Label labComputeRight = gen.DefineLabel();
            Label labReturnFalse = gen.DefineLabel();
            Label labReturnNull = gen.DefineLabel();
            Label labReturnValue = gen.DefineLabel();
            Label labExit = gen.DefineLabel();
            LocalBuilder locLeft = gen.DeclareLocal(type);
            LocalBuilder locRight = gen.DeclareLocal(type);
            this.Generate(gen, b.Left, StackType.Value);
            gen.Emit(OpCodes.Stloc, locLeft);
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateHasValue(gen, type);
            gen.Emit(OpCodes.Brfalse, labComputeRight);
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateGetValueOrDefault(gen, type);
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.Emit(OpCodes.Ceq);
            gen.Emit(OpCodes.Brtrue, labReturnFalse);
            // compute right
            gen.MarkLabel(labComputeRight);
            this.Generate(gen, b.Right, StackType.Value);
            gen.Emit(OpCodes.Stloc, locRight);
            gen.Emit(OpCodes.Ldloca, locRight);
            this.GenerateHasValue(gen, type);
            gen.Emit(OpCodes.Brfalse_S, labReturnNull);
            gen.Emit(OpCodes.Ldloca, locRight);
            this.GenerateGetValueOrDefault(gen, type);
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.Emit(OpCodes.Ceq);
            gen.Emit(OpCodes.Brtrue_S, labReturnFalse);
            // check left for null again
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateHasValue(gen, type);
            gen.Emit(OpCodes.Brfalse, labReturnNull);
            // return true
            gen.Emit(OpCodes.Ldc_I4_1);
            gen.Emit(OpCodes.Br_S, labReturnValue);
            // return false
            gen.MarkLabel(labReturnFalse);
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.Emit(OpCodes.Br_S, labReturnValue);
            gen.MarkLabel(labReturnValue);
            ConstructorInfo ci = type.GetConstructor(new Type[] { typeof(bool) });
            gen.Emit(OpCodes.Newobj, ci);
            gen.Emit(OpCodes.Stloc, locLeft);
            gen.Emit(OpCodes.Br, labExit);
            // return null
            gen.MarkLabel(labReturnNull);
            gen.Emit(OpCodes.Ldloca, locLeft);
            gen.Emit(OpCodes.Initobj, type);
            gen.MarkLabel(labExit);
            return this.ReturnFromLocal(gen, ask, locLeft);
        }

        //PORTED
        private void GenerateMethodAndAlso(ILGenerator gen, BinaryExpression b) {
            Label labEnd = gen.DefineLabel();
            this.Generate(gen, b.Left, StackType.Value);
            gen.Emit(OpCodes.Dup);
            Type type = b.Method.GetParameters()[0].ParameterType;
            Type[] types = new Type[]{type};
            MethodInfo opFalse = type.GetMethod("op_False",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
            gen.Emit(OpCodes.Call, opFalse);
            gen.Emit(OpCodes.Brtrue, labEnd);
            this.Generate(gen, b.Right, StackType.Value);
            Debug.Assert(b.Method.IsStatic);
            gen.Emit(OpCodes.Call, b.Method);
            gen.MarkLabel(labEnd);
        }

        //PORTED
        private void GenerateUnliftedAndAlso(ILGenerator gen, BinaryExpression b) {
            this.Generate(gen, b.Left, StackType.Value);
            Label labEnd = gen.DefineLabel();
            gen.Emit(OpCodes.Dup);
            gen.Emit(OpCodes.Brfalse, labEnd);
            gen.Emit(OpCodes.Pop);
            this.Generate(gen, b.Right, StackType.Value);
            gen.MarkLabel(labEnd);
        }

        //PORTED
        private StackType GenerateAndAlso(ILGenerator gen, BinaryExpression b, StackType ask) {
            if (b.Method != null && !IsLiftedLogicalBinaryOperator(b.Left.Type, b.Right.Type, b.Method))
                GenerateMethodAndAlso(gen, b);
            else if (b.Left.Type == typeof(bool?))
                return GenerateLiftedAndAlso(gen, b, ask);
            else if (IsLiftedLogicalBinaryOperator(b.Left.Type, b.Right.Type, b.Method))
                return GenerateUserdefinedLiftedAndAlso(gen, b, ask);
            else
                GenerateUnliftedAndAlso(gen, b);
            return StackType.Value;
        }

        //PORTED
        private static bool IsLiftedLogicalBinaryOperator(Type left, Type right, MethodInfo method){
            return right == left && IsNullable(left) && method != null && method.ReturnType == GetNonNullableType(left);
        }

        // for a userdefined type T which has Op_True defined and Lhs, Rhs are nullable L OrElse R  is computed as
        // L.HasValue 
        //      ? (T.True(L.Value) 
        //          ? L 
        //            : (R.HasValue 
        //            ? (T?)(T.|(L.Value, R.Value)) 
        //               : R)
        //        : R

        //PORTED
        private StackType GenerateUserdefinedLiftedOrElse(ILGenerator gen, BinaryExpression b, StackType ask){
            Type type = b.Left.Type;
            Type nnType = GetNonNullableType(type);
            Label labReturnLeft = gen.DefineLabel();
            Label labReturnRight = gen.DefineLabel();
            Label labExit = gen.DefineLabel();

            LocalBuilder locLeft = gen.DeclareLocal(type);
            LocalBuilder locRight = gen.DeclareLocal(type);
            LocalBuilder locNNLeft = gen.DeclareLocal(nnType);
            LocalBuilder locNNRight = gen.DeclareLocal(nnType);

            // Load left
            this.Generate(gen, b.Left, StackType.Value);
            gen.Emit(OpCodes.Stloc, locLeft);
            // Load right
            this.Generate(gen, b.Right, StackType.Value);
            gen.Emit(OpCodes.Stloc, locRight);

            // Check left
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateHasValue(gen, type);
            gen.Emit(OpCodes.Brfalse, labReturnRight);
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateGetValueOrDefault(gen, type);
            Type[] types = new Type[] { nnType };
            MethodInfo opTrue = nnType.GetMethod("op_True",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
            gen.Emit(OpCodes.Call, opTrue);
            gen.Emit(OpCodes.Brtrue, labExit);
            // Check right
            gen.Emit(OpCodes.Ldloca, locRight);
            this.GenerateHasValue(gen, type);
            gen.Emit(OpCodes.Brfalse, labReturnRight);
            //Compute bitwise Or
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateGetValueOrDefault(gen, type);
            gen.Emit(OpCodes.Stloc, locNNLeft);
            gen.Emit(OpCodes.Ldloca, locRight);
            this.GenerateGetValueOrDefault(gen, type);
            gen.Emit(OpCodes.Stloc, locNNRight);
            types = new Type[] { nnType, nnType };
            MethodInfo opAnd = nnType.GetMethod("op_BitwiseOr",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
            gen.Emit(OpCodes.Ldloc, locNNLeft);
            gen.Emit(OpCodes.Ldloc, locNNRight);
            gen.Emit(OpCodes.Call, opAnd);
            if (opAnd.ReturnType != type)
                GenerateConvertToType(gen, opAnd.ReturnType, type, true);
            gen.Emit(OpCodes.Stloc, locLeft);
            gen.Emit(OpCodes.Br, labExit);
            //return right
            gen.MarkLabel(labReturnRight);
            gen.Emit(OpCodes.Ldloc, locRight);
            gen.Emit(OpCodes.Stloc, locLeft);
            gen.MarkLabel(labExit);
            //return left
            return this.ReturnFromLocal(gen, ask, locLeft);
        }

        //PORTED
        private StackType GenerateLiftedOrElse(ILGenerator gen, BinaryExpression b, StackType ask) {
            Type type = typeof(bool?);
            Label labComputeRight = gen.DefineLabel();
            Label labReturnTrue = gen.DefineLabel();
            Label labReturnNull = gen.DefineLabel();
            Label labReturnValue = gen.DefineLabel();
            Label labExit = gen.DefineLabel();
            LocalBuilder locLeft = gen.DeclareLocal(type);
            LocalBuilder locRight = gen.DeclareLocal(type);
            this.Generate(gen, b.Left, StackType.Value);
            gen.Emit(OpCodes.Stloc, locLeft);
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateHasValue(gen, type);
            gen.Emit(OpCodes.Brfalse, labComputeRight);
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateGetValueOrDefault(gen, type);
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.Emit(OpCodes.Ceq);
            gen.Emit(OpCodes.Brfalse, labReturnTrue);
            // compute right
            gen.MarkLabel(labComputeRight);
            this.Generate(gen, b.Right, StackType.Value);
            gen.Emit(OpCodes.Stloc, locRight);
            gen.Emit(OpCodes.Ldloca, locRight);
            this.GenerateHasValue(gen, type);
            gen.Emit(OpCodes.Brfalse_S, labReturnNull);
            gen.Emit(OpCodes.Ldloca, locRight);
            this.GenerateGetValueOrDefault(gen, type);
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.Emit(OpCodes.Ceq);
            gen.Emit(OpCodes.Brfalse_S, labReturnTrue);
            // check left for null again
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateHasValue(gen, type);
            gen.Emit(OpCodes.Brfalse, labReturnNull);
            // return false
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.Emit(OpCodes.Br_S, labReturnValue);
            // return true
            gen.MarkLabel(labReturnTrue);
            gen.Emit(OpCodes.Ldc_I4_1);
            gen.Emit(OpCodes.Br_S, labReturnValue);
            gen.MarkLabel(labReturnValue);
            ConstructorInfo ci = type.GetConstructor(new Type[] { typeof(bool) });
            gen.Emit(OpCodes.Newobj, ci);
            gen.Emit(OpCodes.Stloc, locLeft);
            gen.Emit(OpCodes.Br, labExit);
            // return null
            gen.MarkLabel(labReturnNull);
            gen.Emit(OpCodes.Ldloca, locLeft);
            gen.Emit(OpCodes.Initobj, type);
            gen.MarkLabel(labExit);
            return this.ReturnFromLocal(gen, ask, locLeft);
        }

        //PORTED
        private void GenerateUnliftedOrElse(ILGenerator gen, BinaryExpression b) {
            this.Generate(gen, b.Left, StackType.Value);
            Label labEnd = gen.DefineLabel();
            gen.Emit(OpCodes.Dup);
            gen.Emit(OpCodes.Brtrue, labEnd);
            gen.Emit(OpCodes.Pop);
            this.Generate(gen, b.Right, StackType.Value);
            gen.MarkLabel(labEnd);
        }

        //PORTED
        private void GenerateMethodOrElse(ILGenerator gen, BinaryExpression b) {
            Label labEnd = gen.DefineLabel();
            this.Generate(gen, b.Left, StackType.Value);
            gen.Emit(OpCodes.Dup);
            Type type = b.Method.GetParameters()[0].ParameterType;
            Type[] types = new Type[]{type};
            MethodInfo opTrue = type.GetMethod("op_True",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
            gen.Emit(OpCodes.Call, opTrue);
            gen.Emit(OpCodes.Brtrue, labEnd);
            this.Generate(gen, b.Right, StackType.Value);
            Debug.Assert(b.Method.IsStatic);
            gen.Emit(OpCodes.Call, b.Method);
            gen.MarkLabel(labEnd);
        }

        //PORTED
        private StackType GenerateOrElse(ILGenerator gen, BinaryExpression b, StackType ask) {
            if (b.Method != null && !IsLiftedLogicalBinaryOperator(b.Left.Type, b.Right.Type, b.Method))
                GenerateMethodOrElse(gen, b);
            else if (b.Left.Type == typeof(bool?))
                return GenerateLiftedOrElse(gen, b, ask);
            else if (IsLiftedLogicalBinaryOperator(b.Left.Type, b.Right.Type, b.Method))
                return GenerateUserdefinedLiftedOrElse(gen, b, ask);
            else 
                GenerateUnliftedOrElse(gen, b);
            return StackType.Value;
        }

        private static bool IsNullConstant(Expression e)
        {
            return e.NodeType == ExpressionType.Constant && ((ConstantExpression)e).Value == null;
        }

        //PORTED
        private StackType GenerateBinary(ILGenerator gen, BinaryExpression b, StackType ask) {
            switch (b.NodeType) {
                case ExpressionType.Coalesce:
                    GenerateCoalesce(gen, b);
                    return StackType.Value;
                case ExpressionType.AndAlso:
                    return GenerateAndAlso(gen, b, ask);
                case ExpressionType.OrElse:
                    return GenerateOrElse(gen, b, ask);
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                    // If we have x==null, x!=null, null==x or null!=x where x is
                    // nullable but not null, then generate a call to x.HasValue.
                    Debug.Assert(!b.IsLiftedToNull || b.Type == typeof(bool?));
                    if (IsNullConstant(b.Left) && !IsNullConstant(b.Right) && IsNullable(b.Right.Type))
                        return GenerateNullEquality(gen, b.NodeType, b.Right, b.IsLiftedToNull);
                    if (IsNullConstant(b.Right) && !IsNullConstant(b.Left) && IsNullable(b.Left.Type))
                        return GenerateNullEquality(gen, b.NodeType, b.Left, b.IsLiftedToNull);
                    // Otherwise generate it normally.
                    break;
            }
            if (b.Method != null) 
                return this.GenerateBinaryMethod(gen, b, ask);
            this.Generate(gen, b.Left, StackType.Value);
            this.Generate(gen, b.Right, StackType.Value);
            return this.GenerateBinaryOp(gen, b.NodeType, b.Left.Type, b.Right.Type, b.Type, b.IsLiftedToNull, ask);
        }

        //PORTED
        private StackType GenerateNullEquality(ILGenerator gen, ExpressionType op, Expression e,
                bool isLiftedToNull) {
            Debug.Assert(IsNullable(e.Type));
            Debug.Assert(op == ExpressionType.Equal || op == ExpressionType.NotEqual);
            // If we are lifted to null then just evaluate the expression for its side effects, discard,
            // and generate null.  If we are not lifted to null then generate a call to HasValue.
            this.Generate(gen, e, StackType.Value);
            if (isLiftedToNull) {
                gen.Emit(OpCodes.Pop);
                GenerateConstant(gen, Expression.Constant(null, typeof(bool?)), StackType.Value);
            }
            else {
                LocalBuilder local = gen.DeclareLocal(e.Type);
                gen.Emit(OpCodes.Stloc, local);
                gen.Emit(OpCodes.Ldloca, local);
                this.GenerateHasValue(gen, e.Type);
                if (op == ExpressionType.Equal) {
                    gen.Emit(OpCodes.Ldc_I4_0);
                    gen.Emit(OpCodes.Ceq);
                }
            }
            return StackType.Value;
        }

        //PORTED
        private StackType GenerateBinaryMethod(ILGenerator gen, BinaryExpression b, StackType ask) {
            if (b.IsLifted) {
                ParameterExpression p1 = Expression.Parameter(Expression.GetNonNullableType(b.Left.Type), null);
                ParameterExpression p2 = Expression.Parameter(Expression.GetNonNullableType(b.Right.Type), null);
                MethodCallExpression mc = Expression.Call(null, b.Method, p1, p2);
                Type resultType = null;
                if (b.IsLiftedToNull) {
                    resultType = Expression.GetNullableType(mc.Type);
                }
                else {
                    switch (b.NodeType) {
                        case ExpressionType.Equal:
                        case ExpressionType.NotEqual:
                        case ExpressionType.LessThan:
                        case ExpressionType.LessThanOrEqual:
                        case ExpressionType.GreaterThan:
                        case ExpressionType.GreaterThanOrEqual:
                            if (mc.Type != typeof(bool))
                                throw Error.ArgumentMustBeBoolean();
                            resultType = typeof(bool);                            
                            break;
                        default:
                            resultType = Expression.GetNullableType(mc.Type);
                            break;
                    }
                }
                IEnumerable<ParameterExpression> parameters = new ParameterExpression[] { p1, p2 };
                IEnumerable<Expression> arguments = new Expression[] { b.Left, b.Right };
                Expression.ValidateLift(parameters, arguments);
                return this.GenerateLift(gen, b.NodeType, resultType, mc, parameters, arguments);
            }
            else {
                MethodCallExpression mc = Expression.Call(null, b.Method, b.Left, b.Right);
                return this.Generate(gen, mc, ask);
            }
        }

        //PORTED
        private void GenerateTypeIs(ILGenerator gen, TypeBinaryExpression b) {

            this.Generate(gen, b.Expression, StackType.Value);

            // Oddly enough, it is legal for an "is" expression to have a void-returning
            // method call on its left hand side.  In that case, evaluate the operand
            // for its side effects and always return false.

            if (b.Expression.Type == typeof(void))
            {
                gen.Emit(OpCodes.Ldc_I4_0);
                return;
            }
            
            if (b.Expression.Type.IsValueType) {
                gen.Emit(OpCodes.Box, b.Expression.Type);
            }
            gen.Emit(OpCodes.Isinst, b.TypeOperand);
            gen.Emit(OpCodes.Ldnull);
            gen.Emit(OpCodes.Cgt_Un);
        }

        private StackType GenerateHoistedLocalAccess(ILGenerator gen, int hoistIndex, Type type, StackType ask) {
            // This assumes that the array load is already generated.
            this.GenerateConstInt(gen, hoistIndex);
            gen.Emit(OpCodes.Ldelem_Ref);
            Type varType = MakeStrongBoxType(type);
            gen.Emit(OpCodes.Castclass, varType);
            FieldInfo fi = varType.GetField("Value", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            if (ask == StackType.Value)
            {
                gen.Emit(OpCodes.Ldfld, fi);
            }
            else
            {
                gen.Emit(OpCodes.Ldflda, fi);
            }
            return ask;
        }

        private StackType GenerateGlobalAccess(ILGenerator gen, int iGlobal, Type type, StackType ask) {
            GenerateLoadExecutionScope(gen);
            gen.Emit(OpCodes.Ldfld, typeof(ExecutionScope).GetField("Globals", BindingFlags.Public | BindingFlags.Instance));
            this.GenerateConstInt(gen, iGlobal);
            gen.Emit(OpCodes.Ldelem_Ref);
            Type varType = MakeStrongBoxType(type);
            gen.Emit(OpCodes.Castclass, varType);
            FieldInfo fi = varType.GetField("Value", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            if (ask == StackType.Value) {
                gen.Emit(OpCodes.Ldfld, fi);
            }
            else {
                gen.Emit(OpCodes.Ldflda, fi);
            }
            return ask;
        }

        private int AddGlobal(Type type, object value) {
            int iGlobal = this.globals.Count;
            this.globals.Add(Activator.CreateInstance(MakeStrongBoxType(type), new object[] { value }));
            return iGlobal;
        }

        //PORTED: ILGen.EmitCastToType
        private void GenerateCastToType(ILGenerator gen, Type typeFrom, Type typeTo) {
            if (!typeFrom.IsValueType && typeTo.IsValueType) {
                gen.Emit(OpCodes.Unbox_Any, typeTo);
            }
            else if (typeFrom.IsValueType && !typeTo.IsValueType) {
                gen.Emit(OpCodes.Box, typeFrom);
                if (typeTo != typeof(object)) {
                    gen.Emit(OpCodes.Castclass, typeTo);
                }
            }
            else if (!typeFrom.IsValueType && !typeTo.IsValueType) {
                gen.Emit(OpCodes.Castclass, typeTo);
            }
            else {
                throw Error.InvalidCast(typeFrom, typeTo);
            }
        }

        private void GenerateNullableToNullableConversion(ILGenerator gen, Type typeFrom, Type typeTo, bool isChecked)  {
            Debug.Assert(IsNullable(typeFrom));
            Debug.Assert(IsNullable(typeTo));
            Label labIfNull = default(Label);
            Label labEnd = default(Label);
            LocalBuilder locFrom = null;
            LocalBuilder locTo = null;
            locFrom = gen.DeclareLocal(typeFrom);
            gen.Emit(OpCodes.Stloc, locFrom);
            locTo = gen.DeclareLocal(typeTo);
            // test for null
            gen.Emit(OpCodes.Ldloca, locFrom);
            this.GenerateHasValue(gen, typeFrom);
            labIfNull = gen.DefineLabel();
            gen.Emit(OpCodes.Brfalse_S, labIfNull);
            gen.Emit(OpCodes.Ldloca, locFrom);
            this.GenerateGetValueOrDefault(gen, typeFrom);
            Type nnTypeFrom = GetNonNullableType(typeFrom);
            Type nnTypeTo = GetNonNullableType(typeTo);
            this.GenerateConvertToType(gen, nnTypeFrom, nnTypeTo, isChecked);
            // construct result type
            ConstructorInfo ci = typeTo.GetConstructor(new Type[] { nnTypeTo });
            gen.Emit(OpCodes.Newobj, ci);
            gen.Emit(OpCodes.Stloc, locTo);
            labEnd = gen.DefineLabel();
            gen.Emit(OpCodes.Br_S, labEnd);
            // if null then create a default one
            gen.MarkLabel(labIfNull);
            gen.Emit(OpCodes.Ldloca, locTo);
            gen.Emit(OpCodes.Initobj, typeTo);
            gen.MarkLabel(labEnd);
            gen.Emit(OpCodes.Ldloc, locTo);
        }
        private void GenerateNonNullableToNullableConversion(ILGenerator gen, Type typeFrom, Type typeTo, bool isChecked) {
            Debug.Assert(!IsNullable(typeFrom));
            Debug.Assert(IsNullable(typeTo));
            LocalBuilder locTo = null;
            locTo = gen.DeclareLocal(typeTo);
            Type nnTypeTo = GetNonNullableType(typeTo);
            this.GenerateConvertToType(gen, typeFrom, nnTypeTo, isChecked);
            ConstructorInfo ci = typeTo.GetConstructor(new Type[] { nnTypeTo });
            gen.Emit(OpCodes.Newobj, ci);
            gen.Emit(OpCodes.Stloc, locTo);
            gen.Emit(OpCodes.Ldloc, locTo);
        }

        private void GenerateNullableToNonNullableConversion(ILGenerator gen, Type typeFrom, Type typeTo, bool isChecked){
            Debug.Assert(IsNullable(typeFrom));
            Debug.Assert(!IsNullable(typeTo));
            if (typeTo.IsValueType)
                GenerateNullableToNonNullableStructConversion(gen, typeFrom, typeTo, isChecked);
            else
                GenerateNullableToReferenceConversion(gen, typeFrom);
        }

        private void GenerateNullableToNonNullableStructConversion(ILGenerator gen, Type typeFrom, Type typeTo, bool isChecked){
            Debug.Assert(IsNullable(typeFrom));
            Debug.Assert(!IsNullable(typeTo));
            Debug.Assert(typeTo.IsValueType);
            LocalBuilder locFrom = null;
            locFrom = gen.DeclareLocal(typeFrom);
            gen.Emit(OpCodes.Stloc, locFrom);
            gen.Emit(OpCodes.Ldloca, locFrom);
            this.GenerateGetValue(gen, typeFrom);
            Type nnTypeFrom = GetNonNullableType(typeFrom);
            this.GenerateConvertToType(gen, nnTypeFrom, typeTo, isChecked);
        }
        private void GenerateNullableToReferenceConversion(ILGenerator gen, Type typeFrom){
            Debug.Assert(IsNullable(typeFrom));
            // We've got a conversion from nullable to Object, ValueType, Enum, etc.  Just box it so that
            // we get the nullable semantics.  
            gen.Emit(OpCodes.Box, typeFrom);
        }
        private void GenerateNullableConversion(ILGenerator gen, Type typeFrom, Type typeTo, bool isChecked) {
            bool isTypeFromNullable = IsNullable(typeFrom);
            bool isTypeToNullable = IsNullable(typeTo);
            Debug.Assert(isTypeFromNullable || isTypeToNullable);
            if (isTypeFromNullable && isTypeToNullable)
                GenerateNullableToNullableConversion(gen, typeFrom, typeTo, isChecked);
            else if (isTypeFromNullable)
                GenerateNullableToNonNullableConversion(gen, typeFrom, typeTo, isChecked);
            else
                GenerateNonNullableToNullableConversion(gen, typeFrom, typeTo, isChecked);
        }
        //PORTED: ILGen.EmitNumericConversion
        private void GenerateNumericConversion(ILGenerator gen, Type typeFrom, Type typeTo, bool isChecked) {
            bool isFromUnsigned = IsUnsigned(typeFrom);
            bool isFromFloatingPoint = IsFloatingPoint(typeFrom);
            if (typeTo == typeof(Single)) {
                if (isFromUnsigned)
                    gen.Emit(OpCodes.Conv_R_Un);
                gen.Emit(OpCodes.Conv_R4);
            }
            else if (typeTo == typeof(Double)) {
                if (isFromUnsigned)
                    gen.Emit(OpCodes.Conv_R_Un);
                gen.Emit(OpCodes.Conv_R8);
            }
            else {
                TypeCode tc = Type.GetTypeCode(typeTo);
                if (isChecked) {
                    if (isFromUnsigned) {
                        switch (tc) {
                            case TypeCode.SByte:
                                gen.Emit(OpCodes.Conv_Ovf_I1_Un);
                                break;
                            case TypeCode.Int16:
                                gen.Emit(OpCodes.Conv_Ovf_I2_Un);
                                break;
                            case TypeCode.Int32:
                                gen.Emit(OpCodes.Conv_Ovf_I4_Un);
                                break;
                            case TypeCode.Int64:
                                gen.Emit(OpCodes.Conv_Ovf_I8_Un);
                                break;
                            case TypeCode.Byte:
                                gen.Emit(OpCodes.Conv_Ovf_U1_Un);
                                break;
                            case TypeCode.UInt16:
                            case TypeCode.Char:
                                gen.Emit(OpCodes.Conv_Ovf_U2_Un);
                                break;
                            case TypeCode.UInt32:
                                gen.Emit(OpCodes.Conv_Ovf_U4_Un);
                                break;
                            case TypeCode.UInt64:
                                gen.Emit(OpCodes.Conv_Ovf_U8_Un);
                                break;
                            default:
                                throw Error.UnhandledConvert(typeTo);
                        }
                    }
                    else {
                        switch (tc) {
                            case TypeCode.SByte:
                                gen.Emit(OpCodes.Conv_Ovf_I1);
                                break;
                            case TypeCode.Int16:
                                gen.Emit(OpCodes.Conv_Ovf_I2);
                                break;
                            case TypeCode.Int32:
                                gen.Emit(OpCodes.Conv_Ovf_I4);
                                break;
                            case TypeCode.Int64:
                                gen.Emit(OpCodes.Conv_Ovf_I8);
                                break;
                            case TypeCode.Byte:
                                gen.Emit(OpCodes.Conv_Ovf_U1);
                                break;
                            case TypeCode.UInt16:
                            case TypeCode.Char:
                                gen.Emit(OpCodes.Conv_Ovf_U2);
                                break;
                            case TypeCode.UInt32:
                                gen.Emit(OpCodes.Conv_Ovf_U4);
                                break;
                            case TypeCode.UInt64:
                                gen.Emit(OpCodes.Conv_Ovf_U8);
                                break;
                            default:
                                throw Error.UnhandledConvert(typeTo);
                        }
                    }
                }
                else {
                    if (isFromUnsigned) {
                        switch (tc) {
                            case TypeCode.SByte:
                            case TypeCode.Byte:
                                gen.Emit(OpCodes.Conv_U1);
                                break;
                            case TypeCode.Int16:
                            case TypeCode.UInt16:
                            case TypeCode.Char:
                                gen.Emit(OpCodes.Conv_U2);
                                break;
                            case TypeCode.Int32:
                            case TypeCode.UInt32:
                                gen.Emit(OpCodes.Conv_U4);
                                break;
                            case TypeCode.Int64:
                            case TypeCode.UInt64:
                                gen.Emit(OpCodes.Conv_U8);
                                break;
                            default:
                                throw Error.UnhandledConvert(typeTo);
                        }
                    }
                    else {
                        switch (tc) {
                            case TypeCode.SByte:
                            case TypeCode.Byte:
                                gen.Emit(OpCodes.Conv_I1);
                                break;
                            case TypeCode.Int16:
                            case TypeCode.UInt16:
                            case TypeCode.Char:
                                gen.Emit(OpCodes.Conv_I2);
                                break;
                            case TypeCode.Int32:
                            case TypeCode.UInt32:
                                gen.Emit(OpCodes.Conv_I4);
                                break;
                            case TypeCode.Int64:
                            case TypeCode.UInt64:
                                gen.Emit(OpCodes.Conv_I8);
                                break;
                            default:
                                throw Error.UnhandledConvert(typeTo);
                        }
                    }
                }
            }
        }

        //PORTED: ILGen.EmitConvertToType
        private void GenerateConvertToType(ILGenerator gen, Type typeFrom, Type typeTo, bool isChecked) {
            if (typeFrom == typeTo)
                return;

            bool isTypeFromNullable = IsNullable(typeFrom);
            bool isTypeToNullable = IsNullable(typeTo);

            Type nnExprType = GetNonNullableType(typeFrom);
            Type nnType = GetNonNullableType(typeTo);

            if(typeFrom.IsInterface || // interface cast
               typeTo.IsInterface ||
               typeFrom == typeof(object) || // boxing cast
               typeTo == typeof(object))
            {
                GenerateCastToType(gen, typeFrom, typeTo);
            } 
            else if (isTypeFromNullable || isTypeToNullable) 
            {
                GenerateNullableConversion(gen, typeFrom, typeTo, isChecked);
            }
            else if (!(IsConvertible(typeFrom) && IsConvertible(typeTo)) // primitive runtime conversion
                     &&
                     (nnExprType.IsAssignableFrom(nnType) || // down cast
                     nnType.IsAssignableFrom(nnExprType))) // up cast
            {
                GenerateCastToType(gen, typeFrom, typeTo);
            }
            else if (typeFrom.IsArray && typeTo.IsArray)
            {
                // See DevDiv Bugs #94657.
                GenerateCastToType(gen, typeFrom, typeTo);
            }
            else
            {
                GenerateNumericConversion(gen, typeFrom, typeTo, isChecked);
            }
        }

        private StackType ReturnFromLocal(ILGenerator gen, StackType ask, LocalBuilder local) {
            if (ask == StackType.Address) {
                gen.Emit(OpCodes.Ldloca, local);
            }
            else {
                gen.Emit(OpCodes.Ldloc, local);
            }
            return ask;
        }

        private StackType GenerateUnaryOp(ILGenerator gen, ExpressionType op, Type operandType, Type resultType, StackType ask) {
            bool operandIsNullable = IsNullable(operandType);

            if (op == ExpressionType.ArrayLength) {
                gen.Emit(OpCodes.Ldlen);
                return StackType.Value;
            }

            if (operandIsNullable) {
                switch (op) {
                    case ExpressionType.Not: {
                            if (operandType != typeof(bool?))
                                goto case ExpressionType.Negate;

                            Label labIfNull = gen.DefineLabel();
                            Label labEnd = gen.DefineLabel();
                            LocalBuilder loc = gen.DeclareLocal(operandType);

                            // store values (reverse order since they are already on the stack)
                            gen.Emit(OpCodes.Stloc, loc);

                            // test for null
                            gen.Emit(OpCodes.Ldloca, loc);
                            this.GenerateHasValue(gen, operandType);
                            gen.Emit(OpCodes.Brfalse_S, labEnd);

                            // do op on non-null value
                            gen.Emit(OpCodes.Ldloca, loc);
                            this.GenerateGetValueOrDefault(gen, operandType);
                            Type nnOperandType = GetNonNullableType(operandType);
                            this.GenerateUnaryOp(gen, op, nnOperandType, typeof(bool), StackType.Value);

                            // construct result
                            ConstructorInfo ci = resultType.GetConstructor(new Type[] { typeof(bool) });
                            gen.Emit(OpCodes.Newobj, ci);
                            gen.Emit(OpCodes.Stloc, loc);

                            gen.MarkLabel(labEnd);
                            return this.ReturnFromLocal(gen, ask, loc);
                        }
                    case ExpressionType.UnaryPlus:
                    case ExpressionType.NegateChecked:
                    case ExpressionType.Negate: {
                            System.Diagnostics.Debug.Assert(operandType == resultType);
                            Label labIfNull = gen.DefineLabel();
                            Label labEnd = gen.DefineLabel();
                            LocalBuilder loc = gen.DeclareLocal(operandType);

                            // check for null
                            gen.Emit(OpCodes.Stloc, loc);
                            gen.Emit(OpCodes.Ldloca, loc);
                            this.GenerateHasValue(gen, operandType);
                            gen.Emit(OpCodes.Brfalse_S, labIfNull);

                            // apply operator to non-null value
                            gen.Emit(OpCodes.Ldloca, loc);
                            this.GenerateGetValueOrDefault(gen, operandType);
                            Type nnOperandType = GetNonNullableType(resultType);
                            this.GenerateUnaryOp(gen, op, nnOperandType, nnOperandType, StackType.Value);

                            // construct result
                            ConstructorInfo ci = resultType.GetConstructor(new Type[] { nnOperandType });
                            gen.Emit(OpCodes.Newobj, ci);
                            gen.Emit(OpCodes.Stloc, loc);
                            gen.Emit(OpCodes.Br_S, labEnd);

                            // if null then create a default one
                            gen.MarkLabel(labIfNull);
                            gen.Emit(OpCodes.Ldloca, loc);
                            gen.Emit(OpCodes.Initobj, resultType);

                            gen.MarkLabel(labEnd);
                            return this.ReturnFromLocal(gen, ask, loc);
                        }
                    case ExpressionType.TypeAs:
                        gen.Emit(OpCodes.Box, operandType);
                        gen.Emit(OpCodes.Isinst, resultType);
                        if (IsNullable(resultType)) {
                            gen.Emit(OpCodes.Unbox_Any, resultType);
                        }
                        return StackType.Value;
                    default:
                        throw Error.UnhandledUnary(op);
                }
            }
            else {
                switch (op) {
                    case ExpressionType.Not:
                        if (operandType == typeof(bool)) {
                            gen.Emit(OpCodes.Ldc_I4_0);
                            gen.Emit(OpCodes.Ceq);
                        }
                        else {
                            gen.Emit(OpCodes.Not);
                        }
                        break;
                    case ExpressionType.UnaryPlus:
                        gen.Emit(OpCodes.Nop);
                        break;
                    case ExpressionType.Negate:
                    case ExpressionType.NegateChecked:
                        gen.Emit(OpCodes.Neg);
                        break;
                    case ExpressionType.TypeAs:
                        if (operandType.IsValueType) {
                            gen.Emit(OpCodes.Box, operandType);
                        }
                        gen.Emit(OpCodes.Isinst, resultType);
                        if (IsNullable(resultType)) {
                            gen.Emit(OpCodes.Unbox_Any, resultType);
                        }
                        break;
                    default:
                    throw Error.UnhandledUnary(op);
                }

                return StackType.Value;
            }
        }

        //PORTED
        private StackType GenerateLiftedBinaryArithmetic(ILGenerator gen, ExpressionType op, Type leftType, Type rightType, Type resultType, StackType ask) {
            bool leftIsNullable = IsNullable(leftType);
            bool rightIsNullable = IsNullable(rightType);

            Debug.Assert(leftIsNullable);

            Label labIfNull = gen.DefineLabel();
            Label labEnd = gen.DefineLabel();
            LocalBuilder locLeft = gen.DeclareLocal(leftType);
            LocalBuilder locRight = gen.DeclareLocal(rightType);
            LocalBuilder locResult = gen.DeclareLocal(resultType);

            // store values (reverse order since they are already on the stack)
            gen.Emit(OpCodes.Stloc, locRight);
            gen.Emit(OpCodes.Stloc, locLeft);

            // test for null
            if(leftIsNullable && rightIsNullable) {
                gen.Emit(OpCodes.Ldloca, locLeft);
                this.GenerateHasValue(gen, leftType);
                gen.Emit(OpCodes.Ldloca, locRight);
                this.GenerateHasValue(gen, rightType);
                gen.Emit(OpCodes.And);
                gen.Emit(OpCodes.Brfalse_S, labIfNull);
            }
            else if (leftIsNullable) {
                gen.Emit(OpCodes.Ldloca, locLeft);
                this.GenerateHasValue(gen, leftType);
                gen.Emit(OpCodes.Brfalse_S, labIfNull);
            }
            // UNDONE: [EricLi] Dead code.  leftIsNullable is always true.
            else if (rightIsNullable) {
                gen.Emit(OpCodes.Ldloca, locRight);
                this.GenerateHasValue(gen, rightType);
                gen.Emit(OpCodes.Brfalse_S, labIfNull);
            }

            // do op on values
            if (leftIsNullable) {
                gen.Emit(OpCodes.Ldloca, locLeft);
                this.GenerateGetValueOrDefault(gen, leftType);
            }
            else {
                // UNDONE: [EricLi] Dead code.  leftIsNullable is always true.
                gen.Emit(OpCodes.Ldloc, locLeft);
            }
            if (rightIsNullable) {
                gen.Emit(OpCodes.Ldloca, locRight);
                this.GenerateGetValueOrDefault(gen, rightType);
            }
            else {
                gen.Emit(OpCodes.Ldloc, locRight);
            }
            this.GenerateBinaryOp(gen, op, GetNonNullableType(leftType), GetNonNullableType(rightType), GetNonNullableType(resultType), false, StackType.Value);

            // construct result type
            ConstructorInfo ci = resultType.GetConstructor(new Type[] { GetNonNullableType(resultType) });
            gen.Emit(OpCodes.Newobj, ci);
            gen.Emit(OpCodes.Stloc, locResult);
            gen.Emit(OpCodes.Br_S, labEnd);

            // if null then create a default one
            gen.MarkLabel(labIfNull);
            gen.Emit(OpCodes.Ldloca, locResult);
            gen.Emit(OpCodes.Initobj, resultType);

            gen.MarkLabel(labEnd);
            return this.ReturnFromLocal(gen, ask, locResult);
        }

        //PORTED
        private StackType GenerateLiftedRelational(ILGenerator gen, ExpressionType op, Type leftType, Type rightType, Type resultType, bool liftedToNull, StackType ask) {
            Debug.Assert(IsNullable(leftType));

            Label labIfLiftToNull = gen.DefineLabel();
            Label labIfNotLisftToNull = gen.DefineLabel();
            Label labEnd = gen.DefineLabel();
            LocalBuilder locLeft = gen.DeclareLocal(leftType);
            LocalBuilder locRight = gen.DeclareLocal(rightType);

            // store values (reverse order since they are already on the stack)
            gen.Emit(OpCodes.Stloc, locRight);
            gen.Emit(OpCodes.Stloc, locLeft);

            if (op == ExpressionType.Equal) {
                // test for both null -> true
                gen.Emit(OpCodes.Ldloca, locLeft);
                this.GenerateHasValue(gen, leftType);
                gen.Emit(OpCodes.Ldc_I4_0);
                gen.Emit(OpCodes.Ceq);
                gen.Emit(OpCodes.Ldloca, locRight);
                this.GenerateHasValue(gen, rightType);
                gen.Emit(OpCodes.Ldc_I4_0);
                gen.Emit(OpCodes.Ceq);
                gen.Emit(OpCodes.And);
                gen.Emit(OpCodes.Dup);
                if (liftedToNull)
                    gen.Emit(OpCodes.Brtrue_S, labIfLiftToNull);
                else
                    gen.Emit(OpCodes.Brtrue_S, labIfNotLisftToNull);
                gen.Emit(OpCodes.Pop);

                // test for either is null -> false
                gen.Emit(OpCodes.Ldloca, locLeft);
                this.GenerateHasValue(gen, leftType);
                gen.Emit(OpCodes.Ldloca, locRight);
                this.GenerateHasValue(gen, rightType);
                gen.Emit(OpCodes.And);

                gen.Emit(OpCodes.Dup);
                if (liftedToNull)
                    gen.Emit(OpCodes.Brfalse_S, labIfLiftToNull);
                else
                    gen.Emit(OpCodes.Brfalse_S, labIfNotLisftToNull);
                gen.Emit(OpCodes.Pop);

            }
            else if (op == ExpressionType.NotEqual) {
                // test for both null -> false
                gen.Emit(OpCodes.Ldloca, locLeft);
                this.GenerateHasValue(gen, leftType);
                gen.Emit(OpCodes.Ldloca, locRight);
                this.GenerateHasValue(gen, rightType);
                gen.Emit(OpCodes.Or);
                gen.Emit(OpCodes.Dup);
                if (liftedToNull)
                    gen.Emit(OpCodes.Brfalse_S, labIfLiftToNull);
                else
                    gen.Emit(OpCodes.Brfalse_S, labIfNotLisftToNull);
                gen.Emit(OpCodes.Pop);

                // test for either is null -> true
                gen.Emit(OpCodes.Ldloca, locLeft);
                this.GenerateHasValue(gen, leftType);
                gen.Emit(OpCodes.Ldc_I4_0);
                gen.Emit(OpCodes.Ceq);
                gen.Emit(OpCodes.Ldloca, locRight);
                this.GenerateHasValue(gen, rightType);
                gen.Emit(OpCodes.Ldc_I4_0);
                gen.Emit(OpCodes.Ceq);
                gen.Emit(OpCodes.Or);
                gen.Emit(OpCodes.Dup);
                if (liftedToNull)
                    gen.Emit(OpCodes.Brtrue_S, labIfLiftToNull);
                else
                    gen.Emit(OpCodes.Brtrue_S, labIfNotLisftToNull);
                gen.Emit(OpCodes.Pop);
            }
            else {
                // test for either is null -> false
                gen.Emit(OpCodes.Ldloca, locLeft);
                this.GenerateHasValue(gen, leftType);
                gen.Emit(OpCodes.Ldloca, locRight);
                this.GenerateHasValue(gen, rightType);
                gen.Emit(OpCodes.And);
                gen.Emit(OpCodes.Dup);
                if (liftedToNull)
                    gen.Emit(OpCodes.Brfalse_S, labIfLiftToNull);
                else
                    gen.Emit(OpCodes.Brfalse_S, labIfNotLisftToNull);
                gen.Emit(OpCodes.Pop);
            }

            // do op on values
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateGetValueOrDefault(gen, leftType);
            gen.Emit(OpCodes.Ldloca, locRight);
            this.GenerateGetValueOrDefault(gen, rightType);

            StackType r = this.GenerateBinaryOp(gen, op, GetNonNullableType(leftType), GetNonNullableType(rightType), GetNonNullableType(resultType), false, ask);

            gen.MarkLabel(labIfNotLisftToNull);
            if (resultType != GetNonNullableType(resultType))
                GenerateConvertToType(gen, GetNonNullableType(resultType), resultType, true);
            gen.Emit(OpCodes.Br, labEnd);

            gen.MarkLabel(labIfLiftToNull);
            gen.Emit(OpCodes.Pop);
            gen.Emit(OpCodes.Ldnull);
            gen.Emit(OpCodes.Unbox_Any, resultType);

            gen.MarkLabel(labEnd);
            return r;
        }

        //PORTED
        private StackType GenerateLiftedBooleanAnd(ILGenerator gen, StackType ask) {
            Type type = typeof(bool?);
            Label labComputeRight = gen.DefineLabel();
            Label labReturnFalse = gen.DefineLabel();
            Label labReturnNull = gen.DefineLabel();
            Label labReturnValue = gen.DefineLabel();
            Label labExit = gen.DefineLabel();

            // store values (reverse order since they are already on the stack)
            LocalBuilder locLeft = gen.DeclareLocal(type);
            LocalBuilder locRight = gen.DeclareLocal(type);
            gen.Emit(OpCodes.Stloc, locRight);
            gen.Emit(OpCodes.Stloc, locLeft);

            // compute left
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateHasValue(gen, type);
            gen.Emit(OpCodes.Brfalse, labComputeRight);
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateGetValueOrDefault(gen, type);
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.Emit(OpCodes.Ceq);
            gen.Emit(OpCodes.Brtrue, labReturnFalse);

            // compute right
            gen.MarkLabel(labComputeRight);
            gen.Emit(OpCodes.Ldloca, locRight);
            this.GenerateHasValue(gen, type);
            gen.Emit(OpCodes.Brfalse_S, labReturnNull);
            gen.Emit(OpCodes.Ldloca, locRight);
            this.GenerateGetValueOrDefault(gen, type);
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.Emit(OpCodes.Ceq);
            gen.Emit(OpCodes.Brtrue_S, labReturnFalse);

            // check left for null again
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateHasValue(gen, type);
            gen.Emit(OpCodes.Brfalse, labReturnNull);

            // return true
            gen.Emit(OpCodes.Ldc_I4_1);
            gen.Emit(OpCodes.Br_S, labReturnValue);

            // return false
            gen.MarkLabel(labReturnFalse);
            gen.Emit(OpCodes.Ldc_I4_0);                            
            gen.Emit(OpCodes.Br_S, labReturnValue);

            gen.MarkLabel(labReturnValue);
            ConstructorInfo ci = type.GetConstructor(new Type[] { typeof(bool) });
            gen.Emit(OpCodes.Newobj, ci);
            gen.Emit(OpCodes.Stloc, locLeft);
            gen.Emit(OpCodes.Br, labExit);

            // return null
            gen.MarkLabel(labReturnNull);
            gen.Emit(OpCodes.Ldloca, locLeft);
            gen.Emit(OpCodes.Initobj, type);

            gen.MarkLabel(labExit);
            return this.ReturnFromLocal(gen, ask, locLeft);
        }

        //PORTED
        private StackType GenerateLiftedBooleanOr(ILGenerator gen, StackType ask) {
            Type type = typeof(bool?);
            Label labComputeRight = gen.DefineLabel();
            Label labReturnTrue = gen.DefineLabel();
            Label labReturnNull = gen.DefineLabel();
            Label labReturnValue = gen.DefineLabel();
            Label labExit = gen.DefineLabel();

            // store values (reverse order since they are already on the stack)
            LocalBuilder locLeft = gen.DeclareLocal(type);
            LocalBuilder locRight = gen.DeclareLocal(type);
            gen.Emit(OpCodes.Stloc, locRight);
            gen.Emit(OpCodes.Stloc, locLeft);

            // compute left
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateHasValue(gen, type);
            gen.Emit(OpCodes.Brfalse, labComputeRight);
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateGetValueOrDefault(gen, type);
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.Emit(OpCodes.Ceq);
            gen.Emit(OpCodes.Brfalse, labReturnTrue);

            // compute right
            gen.MarkLabel(labComputeRight);
            gen.Emit(OpCodes.Ldloca, locRight);
            this.GenerateHasValue(gen, type);
            gen.Emit(OpCodes.Brfalse_S, labReturnNull);
            gen.Emit(OpCodes.Ldloca, locRight);
            this.GenerateGetValueOrDefault(gen, type);
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.Emit(OpCodes.Ceq);
            gen.Emit(OpCodes.Brfalse_S, labReturnTrue);

            // check left for null again
            gen.Emit(OpCodes.Ldloca, locLeft);
            this.GenerateHasValue(gen, type);
            gen.Emit(OpCodes.Brfalse, labReturnNull);

            // return false
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.Emit(OpCodes.Br_S, labReturnValue);

            // return true
            gen.MarkLabel(labReturnTrue);
            gen.Emit(OpCodes.Ldc_I4_1);
            gen.Emit(OpCodes.Br_S, labReturnValue);

            gen.MarkLabel(labReturnValue);
            ConstructorInfo ci = type.GetConstructor(new Type[] { typeof(bool) });
            gen.Emit(OpCodes.Newobj, ci);
            gen.Emit(OpCodes.Stloc, locLeft);
            gen.Emit(OpCodes.Br, labExit);

            // return null
            gen.MarkLabel(labReturnNull);
            gen.Emit(OpCodes.Ldloca, locLeft);
            gen.Emit(OpCodes.Initobj, type);

            gen.MarkLabel(labExit);
            return this.ReturnFromLocal(gen, ask, locLeft);
        }

        //PORTED
        private StackType GenerateLiftedBinaryOp(ILGenerator gen, ExpressionType op, Type leftType, Type rightType, Type resultType, bool liftedToNull, StackType ask) {
            Debug.Assert(IsNullable(leftType));
            switch (op) {
                case ExpressionType.And:
                    if (leftType == typeof(bool?))
                        return GenerateLiftedBooleanAnd(gen, ask);
                    return GenerateLiftedBinaryArithmetic(gen, op, leftType, rightType, resultType, ask);
                case ExpressionType.Or:
                    if (leftType == typeof(bool?))
                        return GenerateLiftedBooleanOr(gen, ask);
                    return GenerateLiftedBinaryArithmetic(gen, op, leftType, rightType, resultType, ask);
                case ExpressionType.ExclusiveOr:
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.LeftShift:
                case ExpressionType.RightShift:
                    return GenerateLiftedBinaryArithmetic(gen, op, leftType, rightType, resultType, ask);
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                    return GenerateLiftedRelational(gen, op, leftType, rightType, resultType, liftedToNull, ask);
                case ExpressionType.AndAlso:
                case ExpressionType.OrElse:
                default:
                    Debug.Assert("UnhandledLiftedBinaryOperator" == null);
                    return StackType.Value;
            }
        }

        //PORTED
        static private void GenerateUnliftedEquality(ILGenerator gen, ExpressionType op, Type type)
        {
            Debug.Assert(op == ExpressionType.Equal || op == ExpressionType.NotEqual);
            if (!type.IsPrimitive && type.IsValueType && !type.IsEnum)
            {
                throw Error.OperatorNotImplementedForType(op, type);
            }
            gen.Emit(OpCodes.Ceq);
            if (op == ExpressionType.NotELiftedBinaryOpqual)
            {
                gen.Emit(OpCodes.Ldc_I4_0);
                gen.Emit(OpCodes.Ceq);
            }
        }

        //PORTED
        private StackType GenerateUnliftedBinaryOp(ILGenerator gen, ExpressionType op, Type leftType, Type rightType) {
            Debug.Assert(!IsNullable(leftType));
            if (op == ExpressionType.Equal || op == ExpressionType.NotEqual)
            {
                GenerateUnliftedEquality(gen, op, leftType);
                return StackType.Value;
            }
            if (!leftType.IsPrimitive)
            {
                throw Error.OperatorNotImplementedForType(op, leftType);
            }
            switch (op) {
                case ExpressionType.Add:
                    gen.Emit(OpCodes.Add);
                    break;
                case ExpressionType.AddChecked: {
                    LocalBuilder left = gen.DeclareLocal(leftType);
                    LocalBuilder right = gen.DeclareLocal(rightType);
                    gen.Emit(OpCodes.Stloc, right);
                    gen.Emit(OpCodes.Stloc, left);
                    gen.Emit(OpCodes.Ldloc, left);
                    gen.Emit(OpCodes.Ldloc, right);
                    if (IsFloatingPoint(leftType))
                    {
                        gen.Emit(OpCodes.Add);
                    }
                    else if (IsUnsigned(leftType)) 
                    {
                        gen.Emit(OpCodes.Add_Ovf_Un);
                    }
                    else 
                    {
                        gen.Emit(OpCodes.Add_Ovf);
                    }
                    break;
                }
                case ExpressionType.Subtract:
                    gen.Emit(OpCodes.Sub);
                    break;
                case ExpressionType.SubtractChecked: {
                    LocalBuilder left = gen.DeclareLocal(leftType);
                    LocalBuilder right = gen.DeclareLocal(rightType);
                    gen.Emit(OpCodes.Stloc, right);
                    gen.Emit(OpCodes.Stloc, left);
                    gen.Emit(OpCodes.Ldloc, left);
                    gen.Emit(OpCodes.Ldloc, right);
                    if (IsFloatingPoint(leftType))
                    {
                        gen.Emit(OpCodes.Sub);
                    }
                    else if (IsUnsigned(leftType))
                    {
                        gen.Emit(OpCodes.Sub_Ovf_Un);
                    }
                    else
                    {
                        gen.Emit(OpCodes.Sub_Ovf);
                    }
                    break;
                }
                case ExpressionType.Multiply:
                    gen.Emit(OpCodes.Mul);
                    break;
                case ExpressionType.MultiplyChecked: {
                    LocalBuilder left = gen.DeclareLocal(leftType);
                    LocalBuilder right = gen.DeclareLocal(rightType);
                    gen.Emit(OpCodes.Stloc, right);
                    gen.Emit(OpCodes.Stloc, left);
                    gen.Emit(OpCodes.Ldloc, left);
                    gen.Emit(OpCodes.Ldloc, right);

                    if (IsFloatingPoint(leftType))
                    {
                        gen.Emit(OpCodes.Mul);
                    }
                    else if (IsUnsigned(leftType))
                    {
                        gen.Emit(OpCodes.Mul_Ovf_Un);
                    }
                    else
                    {
                        gen.Emit(OpCodes.Mul_Ovf);
                    }
                    break;
                }
                case ExpressionType.Divide:
                    if (IsUnsigned(leftType)) {
                        gen.Emit(OpCodes.Div_Un);
                    }
                    else {
                        gen.Emit(OpCodes.Div);
                    }
                    break;
                case ExpressionType.Modulo:
                    if (IsUnsigned(leftType)) {
                        gen.Emit(OpCodes.Rem_Un);
                    }
                    else {
                        gen.Emit(OpCodes.Rem);
                    }
                    break;
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    gen.Emit(OpCodes.And);
                    break;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    gen.Emit(OpCodes.Or);
                    break;
                case ExpressionType.LessThan:
                    if (IsUnsigned(leftType)) {
                        gen.Emit(OpCodes.Clt_Un);
                    }
                    else {
                        gen.Emit(OpCodes.Clt);
                    }
                    break;
                case ExpressionType.LessThanOrEqual: {
                        Label labFalse = gen.DefineLabel();
                        Label labEnd = gen.DefineLabel();
                        if (IsUnsigned(leftType)) {
                            gen.Emit(OpCodes.Ble_Un_S, labFalse);
                        }
                        else {
                            gen.Emit(OpCodes.Ble_S, labFalse);
                        }
                        gen.Emit(OpCodes.Ldc_I4_0);
                        gen.Emit(OpCodes.Br_S, labEnd);
                        gen.MarkLabel(labFalse);
                        gen.Emit(OpCodes.Ldc_I4_1);
                        gen.MarkLabel(labEnd);
                        break;
                    }
                case ExpressionType.GreaterThan:
                    if (IsUnsigned(leftType)) {
                        gen.Emit(OpCodes.Cgt_Un);
                    }
                    else {
                        gen.Emit(OpCodes.Cgt);
                    }
                    break;
                case ExpressionType.GreaterThanOrEqual: {
                        Label labFalse = gen.DefineLabel();
                        Label labEnd = gen.DefineLabel();
                        if (IsUnsigned(leftType)) {
                            gen.Emit(OpCodes.Bge_Un_S, labFalse);
                        }
                        else {
                            gen.Emit(OpCodes.Bge_S, labFalse);
                        }
                        gen.Emit(OpCodes.Ldc_I4_0);
                        gen.Emit(OpCodes.Br_S, labEnd);
                        gen.MarkLabel(labFalse);
                        gen.Emit(OpCodes.Ldc_I4_1);
                        gen.MarkLabel(labEnd);
                        break;
                    }
                case ExpressionType.ExclusiveOr:
                    gen.Emit(OpCodes.Xor);
                    break;
                case ExpressionType.LeftShift: {
                    Type shiftType = GetNonNullableType(rightType);
                    if (shiftType != typeof(int)) {
                        this.GenerateConvertToType(gen, shiftType, typeof(int), true);
                    }
                    gen.Emit(OpCodes.Shl);
                    break;
                }
                case ExpressionType.RightShift: {
                    Type shiftType = GetNonNullableType(rightType);
                    if (shiftType != typeof(int)) {
                        this.GenerateConvertToType(gen, shiftType, typeof(int), true);
                    }
                    if (IsUnsigned(leftType)) {
                        gen.Emit(OpCodes.Shr_Un);
                    }
                    else {
                        gen.Emit(OpCodes.Shr);
                    }
                    break;
                }
                default:
                    throw Error.UnhandledBinary(op);
            }
            return StackType.Value;
        }

        //PORTED
        private StackType GenerateBinaryOp(ILGenerator gen, ExpressionType op, Type leftType, Type rightType, Type resultType, bool liftedToNull, StackType ask) {
            bool leftIsNullable = IsNullable(leftType);
            bool rightIsNullable = IsNullable(rightType);
            switch (op) {
                //PORTED
                case ExpressionType.ArrayIndex:
                    if (rightIsNullable) {
                        LocalBuilder loc = gen.DeclareLocal(rightType);
                        gen.Emit(OpCodes.Stloc, loc);
                        gen.Emit(OpCodes.Ldloca, loc);
                        this.GenerateGetValue(gen, rightType);
                    }
                    Type indexType = GetNonNullableType(rightType);
                    if (indexType != typeof(int)) {
                        this.GenerateConvertToType(gen, indexType, typeof(int), true);
                    }
                    return this.GenerateArrayAccess(gen, leftType.GetElementType(), ask);
                case ExpressionType.Coalesce:
                    throw Error.UnexpectedCoalesceOperator();
            }

            if (leftIsNullable) 
                return GenerateLiftedBinaryOp(gen, op, leftType, rightType, resultType, liftedToNull, ask);
            return GenerateUnliftedBinaryOp(gen, op, leftType, rightType);
        }

        private StackType GenerateArgAccess(ILGenerator gen, int iArg, StackType ask) {
            if (ask == StackType.Value) {
                switch (iArg) {
                    case 0:
                        gen.Emit(OpCodes.Ldarg_0);
                        break;
                    case 1:
                        gen.Emit(OpCodes.Ldarg_1);
                        break;
                    case 2:
                        gen.Emit(OpCodes.Ldarg_2);
                        break;
                    case 3:
                        gen.Emit(OpCodes.Ldarg_3);
                        break;
                    default:
                        if (iArg < 128) {
                            gen.Emit(OpCodes.Ldarg_S, (byte)iArg);
                        }
                        else {
                            gen.Emit(OpCodes.Ldarg, iArg);
                        }
                        break;
                }
            }
            else {
                if (iArg < 128) {
                    gen.Emit(OpCodes.Ldarga_S, (byte)iArg);
                }
                else {
                    gen.Emit(OpCodes.Ldarga, iArg);
                }
            }
            return ask;
        }

        private void GenerateConstInt(ILGenerator gen, int value) {
            switch (value) {
                case 0:
                    gen.Emit(OpCodes.Ldc_I4_0);
                    break;
                case 1:
                    gen.Emit(OpCodes.Ldc_I4_1);
                    break;
                case 2:
                    gen.Emit(OpCodes.Ldc_I4_2);
                    break;
                case 3:
                    gen.Emit(OpCodes.Ldc_I4_3);
                    break;
                case 4:
                    gen.Emit(OpCodes.Ldc_I4_4);
                    break;
                case 5:
                    gen.Emit(OpCodes.Ldc_I4_5);
                    break;
                case 6:
                    gen.Emit(OpCodes.Ldc_I4_6);
                    break;
                case 7:
                    gen.Emit(OpCodes.Ldc_I4_7);
                    break;
                case 8:
                    gen.Emit(OpCodes.Ldc_I4_8);
                    break;
                default:
                    if (value == -1) {
                        gen.Emit(OpCodes.Ldc_I4_M1);
                    }
                    else if (value >= -127 && value < 128) {
                        gen.Emit(OpCodes.Ldc_I4_S, (sbyte)value);
                    }
                    else {
                        gen.Emit(OpCodes.Ldc_I4, value);
                    }
                    break;
            }
        }

        private void GenerateArrayAssign(ILGenerator gen, Type type) {
            if (type.IsEnum) {
                gen.Emit(OpCodes.Stelem, type);
            }
            else {
                TypeCode tc = Type.GetTypeCode(type);
                switch (tc) {
                    case TypeCode.SByte:
                    case TypeCode.Byte:
                        gen.Emit(OpCodes.Stelem_I1);
                        break;
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                        gen.Emit(OpCodes.Stelem_I2);
                        break;
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                        gen.Emit(OpCodes.Stelem_I4);
                        break;
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                        gen.Emit(OpCodes.Stelem_I8);
                        break;
                    case TypeCode.Single:
                        gen.Emit(OpCodes.Stelem_R4);
                        break;
                    case TypeCode.Double:
                        gen.Emit(OpCodes.Stelem_R8);
                        break;
                    default:
                        if (type.IsValueType) {
                            gen.Emit(OpCodes.Stelem, type);
                        }
                        else {
                            gen.Emit(OpCodes.Stelem_Ref);
                        }
                        break;
                }
            }
        }

        //PORTED
        private StackType GenerateArrayAccess(ILGenerator gen, Type type, StackType ask) {
            if (ask == StackType.Address) {
                gen.Emit(OpCodes.Ldelema, type);
            }
            else if (!type.IsValueType) {
                gen.Emit(OpCodes.Ldelem_Ref);
            }
            else if (type.IsEnum) {
                gen.Emit(OpCodes.Ldelem, type);
            }
            else {
                TypeCode tc = Type.GetTypeCode(type);
                switch (tc) {
                    case TypeCode.SByte:
                        gen.Emit(OpCodes.Ldelem_I1);
                        break;
                    case TypeCode.Int16:
                        gen.Emit(OpCodes.Ldelem_I2);
                        break;
                    case TypeCode.Int32:
                        gen.Emit(OpCodes.Ldelem_I4);
                        break;
                    case TypeCode.Int64:
                        gen.Emit(OpCodes.Ldelem_I8);
                        break;
                    case TypeCode.Single:
                        gen.Emit(OpCodes.Ldelem_R4);
                        break;
                    case TypeCode.Double:
                        gen.Emit(OpCodes.Ldelem_R8);
                        break;
                    default:
                        gen.Emit(OpCodes.Ldelem, type);
                        break;
                }
            }
            return ask;
        }

        //PORTED: ILGen
        private void GenerateHasValue(ILGenerator gen, Type nullableType) {
            MethodInfo mi = nullableType.GetMethod("get_HasValue", BindingFlags.Instance | BindingFlags.Public);
            Debug.Assert(nullableType.IsValueType);
            gen.Emit(OpCodes.Call, mi);
        }

        //PORTED: ILGen
        private void GenerateGetValue(ILGenerator gen, Type nullableType) {
            MethodInfo mi = nullableType.GetMethod("get_Value", BindingFlags.Instance | BindingFlags.Public);
            Debug.Assert(nullableType.IsValueType);
            gen.Emit(OpCodes.Call, mi);
        }

        //PORTED: ILGen
        private void GenerateGetValueOrDefault(ILGenerator gen, Type nullableType) {
            MethodInfo mi = nullableType.GetMethod("GetValueOrDefault", System.Type.EmptyTypes);
            Debug.Assert(nullableType.IsValueType);
            gen.Emit(OpCodes.Call, mi);
        }

        //PORTED: TypeUtils
        private static bool IsNullable(Type type) {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        //PORTED: TypeUtils
        private static Type GetNonNullableType(Type type) {
            if (IsNullable(type)) {
                Type[] args = type.GetGenericArguments();
                return args[0];
            }
            return type;
        }

        //PORTED: TypeUtils
        private static bool IsConvertible(Type type)
        {
            type = GetNonNullableType(type);
            if (type.IsEnum)
                return true;
            TypeCode tc = Type.GetTypeCode(type);
            switch (tc)
            {
                case TypeCode.Boolean:
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Char:
                    return true;
                default:
                    return false;
            }
        }

        //PORTED: TypeUtils
        private static bool IsUnsigned(Type type) {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                type = type.GetGenericArguments()[0];
            }
            TypeCode tc = Type.GetTypeCode(type);
            switch (tc) {
                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.Char:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                default:
                    return false;
            }
        }

        //PORTED: TypeUtils
        private static bool IsFloatingPoint(Type type) {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                type = type.GetGenericArguments()[0];
            }
            TypeCode tc = Type.GetTypeCode(type);
            switch (tc) {
                case TypeCode.Single:
                case TypeCode.Double:
                    return true;
                default:
                    return false;
            }
        }
    }
}

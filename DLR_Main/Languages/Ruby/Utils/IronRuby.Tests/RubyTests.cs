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
namespace IronRuby.Tests {

    public partial class Tests {
        public Tests(Driver/*!*/ driver) {
            _driver = driver;

            _methods = new Action[] {
                Scenario_Startup, // must be the first test
                Scenario_ParserLogging,
                Scenario_RubyTokenizer1,
                Identifiers1,
                Identifiers2,
                Scenario_ParseBigInts1,
                ParseIntegers1,
                Scenario_ParseNumbers1,
                Scenario_ParseInstanceClassVariables1,
                ParseGlobalVariables1,
                ParseEscapes1,
                ParseEolns1,
                Scenario_ParseRegex1,

                Scenario_RubyCategorizer1,
                Scenario_RubyNameMangling1,
                Scenario_RubyNameMangling2,

                OverloadResolution_Block,
                AmbiguousMatch,

                Scenario_RubySimpleCall1,
                Scenario_RubySimpleCall2, 
                Scenario_RubySimpleCall3, 
                Scenario_RubySimpleCall4, 
                Scenario_RubySimpleCall5, 
                MethodCallCaching1,
                MethodCallCaching2,
                MethodCallCaching3,
                MethodCallCaching4,
                MethodCallCaching5,
                MethodCallCaching6,

                NumericLiterals1,
                StringLiterals1,
                Escapes1,
                UnicodeEscapes1,
                UnicodeEscapes2,

                Heredoc1,
                Symbols1,
                UnicodeIdentifiers1,

                Encoding1,
                Encoding2,
                Encoding3,
                Encoding4,
                Encoding5,
                Encoding_Host1,
                Encoding_Host2,

                AstLocations1,

                Scenario_Globals1,

                Scenario_RubyMethodMissing1, 
                Scenario_RubyMethodMissing2, 
                Scenario_RubySingletonConstants1,
                Scenario_RubyMath1,

                StringsPlus,
                Strings0,
                Strings1,
                Strings2,
                Strings3,
                Strings4,
                Strings5,
                Strings6,
                Strings7,
                Strings8,
                ToSConversion1,
                ToSConversion2,

                Regex1,
                Regex2,
                RegexTransform1,
                RegexTransform2,
                RegexEscape1,
                RegexCondition1,
                RegexCondition2,
                
                Scenario_RubyScopeParsing,
                Scenario_RubyScopes1,
                Scenario_RubyScopes2A,
                Scenario_RubyScopes2B,
                Scenario_RubyScopes3,
                Scenario_RubyScopes4,
                Scenario_RubyScopes5,
                Scenario_RubyScopes6,

                Send1,
                
                AttributeAccessors1,
                AttributeAccessors2,
                
                Scenario_RubyDeclarations1,
                Scenario_RubyDeclarations1A,
                Scenario_RubyDeclarations1B,
                Scenario_RubyDeclarations1C,
                Scenario_RubyDeclarations2,
                Scenario_RubyDeclarations3,
                Scenario_RubyDeclarations4,
                Scenario_RubyInclusions1,
                Scenario_RubyClassVersions1,
                Scenario_RubyClassVersions2,
                InvokeMemberCache1,
                Scenario_RubyBlockExpressions1,
                
                Constants1A,
                Constants1B,
                ConstantNames,
                Constants3,
                Constants4,
                UnqualifiedConstants1,
                LoadAndGlobalConstants,
                GlobalConstantsInterop,
                
                Scenario_ClassVariables1,
                Scenario_ClassVariables2,
                Scenario_RubyLocals1,
                Scenario_MethodAliases1,
                Scenario_MethodAliases2,
                Scenario_MethodUndef1,
                Scenario_MethodUndef2,
                MethodUndefExpression,
                Scenario_Assignment1,
                SetterCallValue,
                SimpleInplaceAsignmentToIndirectLeftValues1,
                MemberAssignmentExpression1,
                MemberAssignmentExpression2,
                MemberAssignmentExpression3,

                Scenario_ParallelAssignment1,
                Scenario_ParallelAssignment2,
                Scenario_ParallelAssignment4,
                Scenario_ParallelAssignment5,
                Scenario_ParallelAssignment6,
                Scenario_ParallelAssignment7,
                Scenario_ParallelAssignment8,
                Scenario_ParallelAssignment9,
                Scenario_ParallelAssignment10,

                BlockEmpty,
                Scenario_RubyBlocks0,
                Scenario_RubyBlocks_Params1,
                Scenario_RubyBlocks_Params2,
                ProcYieldCaching1,
                ProcCallCaching1,
                ProcSelf1,
                Scenario_RubyBlocks2,
                Scenario_RubyBlocks3,
                Scenario_RubyBlocks5,
                Scenario_RubyBlocks6,
                Scenario_RubyBlocks7,
                Scenario_RubyBlocks8,
                Scenario_RubyBlocks9,
                Scenario_RubyBlocks10,
                Scenario_RubyBlocks11,
                Scenario_RubyBlocks12,
                Scenario_RubyBlocks13,
                Scenario_RubyBlocks14,
                Scenario_RubyBlocks15,
                Scenario_RubyBlocks16,
                Scenario_RubyBlocks17,
                BlockArity1,
                
                Scenario_RubyBlockArgs1,
                Scenario_RubyProcYieldArgs1,
                Scenario_RubyProcCallArgs1A,
                Scenario_RubyProcCallArgs1B,
                Scenario_RubyBlockArgs2,
                Scenario_RubyProcCallArgs2A,
                Scenario_RubyProcCallArgs2B,
                Scenario_RubyProcCallArgs2C,
                Scenario_RubyProcCallArgs2D,
                Scenario_RubyBlockArgs3,
                Scenario_RubyBlockArgs4A,
                Scenario_RubyBlockArgs4B,
                Scenario_RubyBlockArgs5,
                Scenario_RubyBlockArgs6,
                // TODO: Scenario_RubyBlockArgs7,
                Scenario_RubyBlockArgs8,
                Scenario_RubyBlockArgs9,
                Scenario_RubyBlockArgs10,
                Proc_RhsAndBlockArguments1,

                Scenario_RubyProcs1,
                RubyProcArgConversion1,
                RubyProcArgConversion2,
                RubyProcArgConversion3,
                RubyProcArgConversion4,
                ProcNew1,
                ProcNew2,
                DefineMethod1,
                DefineMethod2,
                
                Scenario_RubyInitializers0,
                Scenario_RubyInitializers1,
                Scenario_RubyInitializers2A,
                Scenario_RubyInitializers2B,
                Scenario_RubyInitializers3,
                Scenario_RubyInitializers4A,
                Scenario_RubyInitializers4B,
                Scenario_RubyInitializers4C,
                Scenario_RubyInitializers5,
                RubyInitializersCaching1,
                RubyInitializersCaching2,
                RubyAllocators1,

                Scenario_RubyForLoop1,
                // TODO: Python interop: Scenario_RubyForLoop2,
                Scenario_RubyWhileLoop1,
                Scenario_RubyWhileLoop2,
                Scenario_RubyWhileLoop3,
                Scenario_RubyWhileLoop4,
                Scenario_RubyWhileLoop5,
                Scenario_RubyWhileLoop6,
                Scenario_RubyUntilLoop1,
                Scenario_WhileLoopCondition1,
                PostTestWhile1,
                PostTestUntil1,
                WhileModifier1,
                UntilModifier1,
                WhileModifier2,
                UntilModifier2,

                RangeConditionInclusive1,
                RangeConditionExclusive1,
                RangeCondition1,
                RangeCondition2,
                
                Scenario_RubyClosures1,
                Scenario_RubyParams1,
                Scenario_RubyParams2,
                Scenario_RubyReturn1,
                Scenario_RubyArrays1,
                Scenario_RubyArrays2,
                Scenario_RubyArrays3,
                Scenario_RubyArrays4,
                Scenario_RubyArrays5,
                Scenario_RubyArrays6,
                Scenario_RubyHashes1A,
                Scenario_RubyHashes1B,
                Scenario_RubyHashes1C,
                Scenario_RubyHashes2,
                Scenario_RubyHashes3,
                Scenario_RubyHashes4,
                Scenario_RubyArgSplatting1,
                Scenario_RubyArgSplatting2,
                Scenario_RubyArgSplatting3,
                Scenario_RubyBoolExpressions1,
                Scenario_RubyBoolExpressions2,
                Scenario_RubyBoolExpressions3,
                Scenario_RubyBoolExpressions4,
                Scenario_RubyBoolExpressionsWithReturn1,
                Scenario_RubyBoolExpressionsWithReturn2,
                TernaryConditionalWithJumpStatements1,
                TernaryConditionalWithJumpStatements2,
                Scenario_RubyBoolAssignment,
                Scenario_RubyIfExpression1,
                Scenario_RubyIfExpression2,
                Scenario_RubyUnlessExpression1,
                Scenario_RubyConditionalExpression1,
                ConditionalStatement1,
                ConditionalStatement2,

                Scenario_UninitializedVars1,
                Scenario_UninitializedVars2,
                InstanceVariables1,
                InstanceVariables2,
                RubyHosting1A,
                RubyHosting1B,
                RubyHosting1C,
                RubyHosting2,
                RubyHosting3,
                RubyHosting4,
                CrossRuntime1,
                CrossRuntime2,

                Scenario_RubyConsole1,
                // TODO: interop, hosting: Scenario_RubyConsole2,
                Scenario_RubyConsole3,
                Scenario_RubyConsole4,
                ObjectOperations1,
                ObjectOperations2,
                PythonInterop1,
                PythonInterop2,
                CustomTypeDescriptor1,
                CustomTypeDescriptor2,
                
                Loader_Assemblies1,

                Require1,
                RequireInterop1,
                Load1,
                LibraryLoader1,

                ClrFields1,
                ClrTypes1,
                ClrGenerics1,
                ClrMethods1,
                ClrMethodsVisibility1,
                ClrOverloadInheritance1,
                ClrOverloadInheritance2,
                ClrOverloadInheritance3,
                ClrOverloadInheritance4,
                ClrOverloadInheritance5,
                ClrMethodEnumeration1,
                ClrIndexers1,
                ClrGenericMethods1,
                ClrOverloadSelection1,
                ClrInterfaces1,
                ClrRequireAssembly1,
                ClrInclude1,
                ClrNew1,
                ClrAlias1,
                // TODO: ClrEnums1, 
                ClrDelegates1,
                ClrDelegates2,
                ClrEvents1,
                ClrOverride1,
                ClrConstructor1,
                // TODO: ClrConstructor2,
                Scenario_RubyEngine1,
                Scenario_RubyInteractive1,
                Scenario_RubyInteractive2,
                
                Scenario_RubyReturnValues1,
                Scenario_RubyReturnValues2,
                Scenario_RubyReturnValues3,
                Scenario_RubyReturnValues4,
                Scenario_RubyReturnValues5,
                Scenario_RubyReturnValues6,

                Scenario_RubyExceptions1,
                Scenario_RubyExceptions1A,
                Scenario_RubyExceptions2A,
                Scenario_RubyExceptions2B,
                Scenario_RubyExceptions2C,
                Scenario_RubyExceptions2D,
                Scenario_RubyExceptions3,
                Scenario_RubyExceptions4,
                Scenario_RubyExceptions5,
                Scenario_RubyExceptions6,
                Scenario_RubyExceptions7,
                Scenario_RubyExceptions8,
                Scenario_RubyExceptions9,
                Scenario_RubyExceptions10,
                Scenario_RubyExceptions11,
                Scenario_RubyExceptions12,
                Scenario_RubyExceptions12A,
                Scenario_RubyExceptions13,
                Scenario_RubyExceptions14,
                Scenario_RubyExceptions15,
                Scenario_RubyExceptions16,
                Scenario_RubyExceptions_Globals,
                Scenario_RubyRescueStatement1,
                Scenario_RubyRescueExpression1,
                Scenario_RubyRescueExpression2,
                ExceptionArg1,
                ExceptionArg2,
                RescueSplat1,
                RescueSplat2,
                RescueSplat3,

                ClassVariables1,
                UnqualifiedConstants2,

                AliasMethodLookup1,
                
                UndefMethodLookup,
                MethodAdded1,
                MethodLookup1,
                VisibilityCaching1,
                VisibilityCaching2,
                Visibility1,
                Visibility2,
                DefineMethodVisibility1,
                AliasedMethodVisibility1,
                ModuleFunctionVisibility1,
                ModuleFunctionVisibility2,
                MethodDefinitionInDefineMethod1A,
                MethodDefinitionInDefineMethod1B,
                MethodDefinitionInDefineMethod2A,
                MethodDefinitionInDefineMethod2B,
                MethodDefinitionInModuleEval1A,
                MethodDefinitionInModuleEval1B,

                Scenario_Singletons1,
                Scenario_Singletons2,
                Scenario_Singletons3,
                Scenario_Singletons4,
                Scenario_Singletons5,
                SingletonCaching1,
                Scenario_ClassVariables_Singletons,
                AllowedSingletons1,

                Super1,
                SuperParameterless1,
                SuperParameterless2,
                SuperParameterless3,
                Super2,
                SuperAndMethodMissing1,
                SuperAndMethodMissing2,
                SuperCaching1,
                SuperInDefineMethod1,
                SuperInDefineMethod2,
                // TODO: SuperInDefineMethod3,
                SuperInTopLevelCode1,
                SuperInAliasedDefinedMethod1,

                Scenario_RubyDefinedOperator_Globals1,
                Scenario_RubyDefinedOperator_Globals2,
                Scenario_RubyDefinedOperator_Methods1,
                Scenario_RubyDefinedOperator_Methods2,
                Scenario_RubyDefinedOperator_Constants1,
                Scenario_RubyDefinedOperator_Constants2,
                Scenario_RubyDefinedOperator_Expressions1,
                Scenario_RubyDefinedOperator_InstanceVariables1,
                Scenario_RubyDefinedOperator_ClassVariables1,
                Scenario_RubyDefinedOperator_ClassVariables2,
                Scenario_RubyDefinedOperator_Yield1,
                Scenario_RubyDefinedOperator_Locals1,

                Scenario_ModuleOps_Methods,
                Scenario_MainSingleton,

                Scenario_RubyThreads1,
                Scenario_YieldCodeGen,
                Methods1, 
                ToIntegerConversion1,
                ToIntToStrConversion1,
                MethodAliasExpression,
                ClassDuplication1,
                ClassDuplication2,
                ClassDuplication3,
                ClassDuplication4,
                ClassDuplication5,
                StructDup1,
                ClassDuplication6,
                Clone1,
                Dup1,
                MetaModules1,
                MetaModulesDuplication1,
  
                // eval, binding:
                Eval1,
                Eval2,
                Eval3,
                EvalReturn1,
                EvalReturn2,
                LocalNames1,
                LocalNames2,
                LocalNames3,
                LocalNames4,
                LiftedParameters1,
                Binding1,
                TopLevelBinding_RubyProgram,
                EvalWithProcBinding1,
                ModuleEvalProc1,
                ModuleEvalProc2,
                ModuleEvalProc3,
                InstanceEvalProc1,
                // TODO: InstanceEvalProc2,
                ModuleInstanceEvalProc3,
                ModuleClassNew1,
                ModuleClassNew2,
                ModuleEvalString1,
                InstanceEvalString1,
                ModuleEvalString2,
                InstanceEvalString2,
                ModuleInstanceEvalString3,
                AliasInModuleEval1,
                MethodAliasInModuleEval1,
                SuperInModuleEval1,
                
                SuperEval1,
                // TODO: SuperParameterlessEval1,
                // TODO: SuperParameterlessEval2,
                SuperInDefineMethodEval1,

                Backtrace1,
                Backtrace2,
                Backtrace3,
            };
        }
    }
}

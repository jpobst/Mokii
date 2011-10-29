using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mono.Compilers.VisualBasic;
using System.IO;

namespace MokiiTests
{
	[TestClass]
	public class TokenParserTests
	{
		private string input_location = @"D:\Documents\Visual Studio 2010\Projects\Mokii\MokiiTests\bin\Debug\Inputs";
		private string output_location = @"D:\Documents\Visual Studio 2010\Projects\Mokii\MokiiTests\bin\Debug\Outputs";

		[TestMethod]
		public void Accessibility1 ()
		{
			CompareTest ("Accessibility1");
		}

		[TestMethod]
		public void Accessibility2 ()
		{
			CompareTest ("Accessibility2");
		}

		[TestMethod]
		public void AddAssignStatement1 ()
		{
			CompareTest ("AddAssignStatement1");
		}

		[TestMethod]
		public void AddHandler1 ()
		{
			CompareTest ("AddHandler1");
		}

		[TestMethod]
		public void AddHandler2 ()
		{
			CompareTest ("AddHandler2");
		}

		[TestMethod]
		public void AddressOfExpression1 ()
		{
			CompareTest ("AddressOfExpression1");
		}

		[TestMethod]
		public void AddressOfExpression2 ()
		{
			CompareTest ("AddressOfExpression2");
		}

		[TestMethod]
		public void AddressOfExpression3 ()
		{
			CompareTest ("AddressOfExpression3");
		}

		[TestMethod]
		public void AndAlsoConstantExpression1 ()
		{
			CompareTest ("AndAlsoConstantExpression1");
		}

		[TestMethod]
		public void AndAlsoExpression1 ()
		{
			CompareTest ("AndAlsoExpression1");
		}

		[TestMethod]
		public void AndConstantExpression1 ()
		{
			CompareTest ("AndConstantExpression1");
		}

		[TestMethod]
		public void AndExpression2 ()
		{
			CompareTest ("AndExpression2");
		}

		[TestMethod]
		public void Array1 ()
		{
			CompareTest ("Array1");
		}

		[TestMethod]
		public void Array2 ()
		{
			CompareTest ("Array2");
		}

		[TestMethod]
		public void Array3 ()
		{
			CompareTest ("Array3");
		}

		[TestMethod]
		public void Array4 ()
		{
			CompareTest ("Array4");
		}

		[TestMethod]
		public void Array5 ()
		{
			CompareTest ("Array5");
		}

		[TestMethod]
		public void ArrayCreationExpression1 ()
		{
			CompareTest ("ArrayCreationExpression1");
		}

		[TestMethod]
		public void ArrayCreationExpression2 ()
		{
			CompareTest ("ArrayCreationExpression2");
		}

		[TestMethod]
		public void ArrayCreationExpression3 ()
		{
			CompareTest ("ArrayCreationExpression3");
		}

		[TestMethod]
		public void ArrayCreationExpression4 ()
		{
			CompareTest ("ArrayCreationExpression4");
		}

		[TestMethod]
		public void ArrayCreationExpression5 ()
		{
			CompareTest ("ArrayCreationExpression5");
		}

		[TestMethod]
		public void ArrayCreationExpression6 ()
		{
			CompareTest ("ArrayCreationExpression6");
		}

		[TestMethod]
		public void ArrayInitializer1 ()
		{
			CompareTest ("ArrayInitializer1");
		}

		[TestMethod]
		public void ArrayInitializer2 ()
		{
			CompareTest ("ArrayInitializer2");
		}

		[TestMethod]
		public void ArrayInitializer3 ()
		{
			CompareTest ("ArrayInitializer3");
		}

		[TestMethod]
		public void ArrayRef1 ()
		{
			CompareTest ("ArrayRef1");
		}

		[TestMethod]
		public void ArrayRef2 ()
		{
			CompareTest ("ArrayRef2");
		}

		[TestMethod]
		public void Assignment1 ()
		{
			CompareTest ("Assignment1");
		}

		[TestMethod]
		public void Attributes1 ()
		{
			CompareTest ("Attributes1");
		}

		[TestMethod]
		public void Attributes2 ()
		{
			CompareTest ("Attributes2");
		}

		[TestMethod]
		public void Attributes3 ()
		{
			CompareTest ("Attributes3");
		}

		[TestMethod]
		public void Attributes4 ()
		{
			CompareTest ("Attributes4");
		}

		[TestMethod]
		public void Attributes5 ()
		{
			CompareTest ("Attributes5");
		}

		[TestMethod]
		public void Attributes6 ()
		{
			CompareTest ("Attributes6");
		}

		[TestMethod]
		public void Attributes7 ()
		{
			CompareTest ("Attributes7");
		}

		[TestMethod]
		public void BinaryAddConstantExpression1 ()
		{
			CompareTest ("BinaryAddConstantExpression1");
		}

		[TestMethod]
		public void BinaryAddExpression1 ()
		{
			CompareTest ("BinaryAddExpression1");
		}

		[TestMethod]
		public void BinarySubConstantExpression1 ()
		{
			CompareTest ("BinarySubConstantExpression1");
		}

		[TestMethod]
		public void BinarySubExpression1 ()
		{
			CompareTest ("BinarySubExpression1");
		}

		[TestMethod]
		public void BuiltInType1 ()
		{
			CompareTest ("BuiltInType1");
		}

		[TestMethod]
		public void ByRefProperty1 ()
		{
			CompareTest ("ByRefProperty1");
		}

		[TestMethod]
		public void ByRefStore1 ()
		{
			CompareTest ("ByRefStore1");
		}

		[TestMethod]
		public void ByRefStructureReturn1 ()
		{
			CompareTest ("ByRefStructureReturn1");
		}

		[TestMethod]
		public void Call1 ()
		{
			CompareTest ("Call1");
		}

		[TestMethod]
		public void CBoolConstantExpression1 ()
		{
			CompareTest ("CBoolConstantExpression1");
		}

		[TestMethod]
		public void CByteConstantExpression1 ()
		{
			CompareTest ("CByteConstantExpression1");
		}

		[TestMethod]
		public void CCharConstantExpression1 ()
		{
			CompareTest ("CCharConstantExpression1");
		}

		[TestMethod]
		public void CDblConstantExpression1 ()
		{
			CompareTest ("CDblConstantExpression1");
		}

		[TestMethod]
		public void CIntConstantExpression1 ()
		{
			CompareTest ("CIntConstantExpression1");
		}

		[TestMethod]
		public void CIntExpression1 ()
		{
			CompareTest ("CIntExpression1");
		}

		[TestMethod]
		public void ClassVariable1 ()
		{
			CompareTest ("ClassVariable1");
		}

		[TestMethod]
		public void CObjExpression1 ()
		{
			CompareTest ("CObjExpression1");
		}

		[TestMethod]
		public void ConcatAssignStatement1 ()
		{
			CompareTest ("ConcatAssignStatement1");
		}

		[TestMethod]
		public void ConcatExpression1 ()
		{
			CompareTest ("ConcatExpression1");
		}

		[TestMethod]
		public void ConcatExpression2 ()
		{
			CompareTest ("ConcatExpression2");
		}

		[TestMethod]
		[Ignore]
		public void ConditionalCompilation1 ()
		{
			CompareTest ("ConditionalCompilation1");
		}

		[TestMethod]
		[Ignore]
		public void ConditionalConst1 ()
		{
			CompareTest ("ConditionalConst1");
		}

		[TestMethod]
		[Ignore]
		public void ConditionalIf1 ()
		{
			CompareTest ("ConditionalIf1");
		}

		[TestMethod]
		[Ignore]
		public void ConditionalIf2 ()
		{
			CompareTest ("ConditionalIf2");
		}

		[TestMethod]
		[Ignore]
		public void ConditionalIf3 ()
		{
			CompareTest ("ConditionalIf3");
		}

		[TestMethod]
		[Ignore]
		public void ConditionalIf4 ()
		{
			CompareTest ("ConditionalIf4");
		}

		[TestMethod]
		[Ignore]
		public void ConditionalRegion1 ()
		{
			CompareTest ("ConditionalRegion1");
		}

		[TestMethod]
		[Ignore]
		public void ConstantBounds1 ()
		{
			CompareTest ("ConstantBounds1");
		}

		[TestMethod]
		public void ConstantExpression1 ()
		{
			CompareTest ("ConstantExpression1");
		}

		[TestMethod]
		public void ConstantExpression2 ()
		{
			CompareTest ("ConstantExpression2");
		}

		[TestMethod]
		public void ConstantExpression3 ()
		{
			CompareTest ("ConstantExpression3");
		}

		[TestMethod]
		public void ConstantExpression4 ()
		{
			CompareTest ("ConstantExpression4");
		}

		[TestMethod]
		public void ConstantExpression5 ()
		{
			CompareTest ("ConstantExpression5");
		}

		[TestMethod]
		public void ConstantExpression6 ()
		{
			CompareTest ("ConstantExpression6");
		}

		[TestMethod]
		public void ConstantExpression7 ()
		{
			CompareTest ("ConstantExpression7");
		}

		[TestMethod]
		public void ConstantExpression8 ()
		{
			CompareTest ("ConstantExpression8");
		}

		[TestMethod]
		public void ConstantExpression9 ()
		{
			CompareTest ("ConstantExpression9");
		}

		[TestMethod]
		public void Constraint1 ()
		{
			CompareTest ("Constraint1");
		}

		[TestMethod]
		public void Constructors1 ()
		{
			CompareTest ("Constructors1");
		}

		[TestMethod]
		public void Constructors2 ()
		{
			CompareTest ("Constructors2");
		}

		[TestMethod]
		public void Continue1 ()
		{
			CompareTest ("Continue1");
		}

		[TestMethod]
		public void Continue2 ()
		{
			CompareTest ("Continue2");
		}

		[TestMethod]
		public void Continue3 ()
		{
			CompareTest ("Continue3");
		}

		[TestMethod]
		public void Conversion1 ()
		{
			CompareTest ("Conversion1");
		}

		[TestMethod]
		public void CreateInstance1 ()
		{
			CompareTest ("CreateInstance1");
		}

		[TestMethod]
		public void CSByteConstantExpression1 ()
		{
			CompareTest ("CSByteConstantExpression1");
		}

		[TestMethod]
		public void CShortConstantExpression1 ()
		{
			CompareTest ("CShortConstantExpression1");
		}

		[TestMethod]
		public void CSngConstantExpression1 ()
		{
			CompareTest ("CSngConstantExpression1");
		}

		[TestMethod]
		public void CStrConstantExpression1 ()
		{
			CompareTest ("CStrConstantExpression1");
		}

		[TestMethod]
		public void CTypeExpression1 ()
		{
			CompareTest ("CTypeExpression1");
		}

		[TestMethod]
		public void CUIntConstantExpression1 ()
		{
			CompareTest ("CUIntConstantExpression1");
		}

		[TestMethod]
		public void CULngConstantExpression1 ()
		{
			CompareTest ("CULngConstantExpression1");
		}

		[TestMethod]
		public void CUShortConstantExpression1 ()
		{
			CompareTest ("CUShortConstantExpression1");
		}

		[TestMethod]
		public void DataSet1 ()
		{
			CompareTest ("DataSet1");
		}

		[TestMethod]
		public void DebugInfo1 ()
		{
			CompareTest ("DebugInfo1");
		}

		[TestMethod]
		public void DefaultCtor ()
		{
			CompareTest ("DefaultCtor");
		}

		[TestMethod]
		public void DefaultMember1 ()
		{
			CompareTest ("DefaultMember1");
		}

		[TestMethod]
		public void DelegateCreationExpression1 ()
		{
			CompareTest ("DelegateCreationExpression1");
		}

		[TestMethod]
		public void DelegateCreationExpression2 ()
		{
			CompareTest ("DelegateCreationExpression2");
		}

		[TestMethod]
		public void DictionaryLookupExpression1 ()
		{
			CompareTest ("DictionaryLookupExpression1");
		}

		[TestMethod]
		public void DictionaryLookupExpression2 ()
		{
			CompareTest ("DictionaryLookupExpression2");
		}

		[TestMethod]
		public void DirectCast1 ()
		{
			CompareTest ("DirectCast1");
		}

		[TestMethod]
		public void DirectCastExpression1 ()
		{
			CompareTest ("DirectCastExpression1");
		}

		[TestMethod]
		public void DirectCastExpression2 ()
		{
			CompareTest ("DirectCastExpression2");
		}

		[TestMethod]
		public void DivConstantExpression1 ()
		{
			CompareTest ("DivConstantExpression1");
		}

		[TestMethod]
		public void DivisionAssignStatement1 ()
		{
			CompareTest ("DivisionAssignStatement1");
		}

		[TestMethod]
		public void Do1 ()
		{
			CompareTest ("Do1");
		}

		[TestMethod]
		public void DoLoop1 ()
		{
			CompareTest ("DoLoop1");
		}

		[TestMethod]
		public void DoLoopUntil1 ()
		{
			CompareTest ("DoLoopUntil1");
		}

		[TestMethod]
		public void DoLoopWhile1 ()
		{
			CompareTest ("DoLoopWhile1");
		}

		[TestMethod]
		public void DoLoopWhile3 ()
		{
			CompareTest ("DoLoopWhile3");
		}

		[TestMethod]
		public void DotExpression1 ()
		{
			CompareTest ("DotExpression1");
		}

		[TestMethod]
		public void DuplicateReference1 ()
		{
			CompareTest ("DuplicateReference1");
		}

		[TestMethod]
		public void EmitLocalVariables1 ()
		{
			CompareTest ("EmitLocalVariables1");
		}

		[TestMethod]
		public void EnumConstant1 ()
		{
			CompareTest ("EnumConstant1");
		}

		[TestMethod]
		public void EnumConstant2 ()
		{
			CompareTest ("EnumConstant2");
		}

		[TestMethod]
		public void EnumConstant3 ()
		{
			CompareTest ("EnumConstant3");
		}

		[TestMethod]
		public void EnumConstant4 ()
		{
			CompareTest ("EnumConstant4");
		}

		[TestMethod]
		public void Enumerator1 ()
		{
			CompareTest ("Enumerator1");
		}

		[TestMethod]
		public void EnumWithAttribute1 ()
		{
			CompareTest ("EnumWithAttribute1");
		}

		[TestMethod]
		public void EqualsConstantExpression1 ()
		{
			CompareTest ("EqualsConstantExpression1");
		}

		[TestMethod]
		public void EqualsExpression1 ()
		{
			CompareTest ("EqualsExpression1");
		}

		[TestMethod]
		public void Erase1 ()
		{
			CompareTest ("Erase1");
		}

		[TestMethod]
		public void Error1 ()
		{
			CompareTest ("Error1");
		}

		[TestMethod]
		public void EventMember1 ()
		{
			CompareTest ("EventMember1");
		}

		[TestMethod]
		public void EventMember2 ()
		{
			CompareTest ("EventMember2");
		}

		[TestMethod]
		public void EventMember3 ()
		{
			CompareTest ("EventMember3");
		}

		[TestMethod]
		public void EventMember4 ()
		{
			CompareTest ("EventMember4");
		}

		[TestMethod]
		public void EventMember5 ()
		{
			CompareTest ("EventMember5");
		}

		[TestMethod]
		public void EventMember6 ()
		{
			CompareTest ("EventMember6");
		}

		[TestMethod]
		public void EventMember7 ()
		{
			CompareTest ("EventMember7");
		}

		[TestMethod]
		public void EventMember8 ()
		{
			CompareTest ("EventMember8");
		}

		[TestMethod]
		public void Exit1 ()
		{
			CompareTest ("Exit1");
		}

		[TestMethod]
		public void Exit2 ()
		{
			CompareTest ("Exit2");
		}

		[TestMethod]
		public void ExitFunction1 ()
		{
			CompareTest ("ExitFunction1");
		}

		[TestMethod]
		public void ExitProperty1 ()
		{
			CompareTest ("ExitProperty1");
		}

		[TestMethod]
		public void ExitSub1 ()
		{
			CompareTest ("ExitSub1");
		}

		[TestMethod]
		public void ExponentExpression1 ()
		{
			CompareTest ("ExponentExpression1");
		}

		[TestMethod]
		public void For1 ()
		{
			CompareTest ("For1");
		}

		[TestMethod]
		public void For2 ()
		{
			CompareTest ("For2");
		}

		[TestMethod]
		public void ForEach1 ()
		{
			CompareTest ("ForEach1");
		}

		[TestMethod]
		public void ForEach2 ()
		{
			CompareTest ("ForEach2");
		}

		[TestMethod]
		public void ForStep1 ()
		{
			CompareTest ("ForStep1");
		}

		[TestMethod]
		public void GEConstantExpression1 ()
		{
			CompareTest ("GEConstantExpression1");
		}

		[TestMethod]
		public void GenericDelegate1 ()
		{
			CompareTest ("GenericDelegate1");
		}

		[TestMethod]
		public void GenericInterface1 ()
		{
			CompareTest ("GenericInterface1");
		}

		[TestMethod]
		public void GenericInterface2 ()
		{
			CompareTest ("GenericInterface2");
		}

		[TestMethod]
		public void GenericInterface3 ()
		{
			CompareTest ("GenericInterface3");
		}

		[TestMethod]
		public void GenericInterface4 ()
		{
			CompareTest ("GenericInterface4");
		}

		[TestMethod]
		public void GenericNestedType1 ()
		{
			CompareTest ("GenericNestedType1");
		}

		[TestMethod]
		public void GenericTypeParameter1 ()
		{
			CompareTest ("GenericTypeParameter1");
		}

		[TestMethod]
		public void GenericTypeParameter2 ()
		{
			CompareTest ("GenericTypeParameter2");
		}

		[TestMethod]
		public void GenericTypeParameter3 ()
		{
			CompareTest ("GenericTypeParameter3");
		}

		[TestMethod]
		public void GenericTypeParameter4 ()
		{
			CompareTest ("GenericTypeParameter4");
		}

		[TestMethod]
		public void GenericTypeParameter5 ()
		{
			CompareTest ("GenericTypeParameter5");
		}

		[TestMethod]
		public void GenericTypeParameter6 ()
		{
			CompareTest ("GenericTypeParameter6");
		}

		[TestMethod]
		public void GenericTypeParameter7 ()
		{
			CompareTest ("GenericTypeParameter7");
		}

		[TestMethod]
		public void GenericTypeParameter8 ()
		{
			CompareTest ("GenericTypeParameter8");
		}

		[TestMethod]
		public void GenericTypeParameter9 ()
		{
			CompareTest ("GenericTypeParameter9");
		}

		[TestMethod]
		public void GetTypeExpression1 ()
		{
			CompareTest ("GetTypeExpression1");
		}

		[TestMethod]
		public void GetTypeExpression2 ()
		{
			CompareTest ("GetTypeExpression2");
		}

		[TestMethod]
		public void GetTypeExpression3 ()
		{
			CompareTest ("GetTypeExpression3");
		}

		[TestMethod]
		public void GetUpperBound ()
		{
			CompareTest ("GetUpperBound");
		}

		[TestMethod]
		public void Global1 ()
		{
			CompareTest ("Global1");
		}

		[TestMethod]
		public void Goto1 ()
		{
			CompareTest ("Goto1");
		}

		[TestMethod]
		public void Goto2 ()
		{
			CompareTest ("Goto2");
		}

		[TestMethod]
		public void GTConstantExpression1 ()
		{
			CompareTest ("GTConstantExpression1");
		}

		[TestMethod]
		public void GTExpression1 ()
		{
			CompareTest ("GTExpression1");
		}

		[TestMethod]
		public void HideBySig1 ()
		{
			CompareTest ("HideBySig1");
		}

		[TestMethod]
		public void If1 ()
		{
			CompareTest ("If1");
		}

		[TestMethod]
		public void If2 ()
		{
			CompareTest ("If2");
		}

		[TestMethod]
		public void If3 ()
		{
			CompareTest ("If3");
		}

		[TestMethod]
		public void If4 ()
		{
			CompareTest ("If4");
		}

		[TestMethod]
		public void If5 ()
		{
			CompareTest ("If5");
		}

		[TestMethod]
		public void If6 ()
		{
			CompareTest ("If6");
		}

		[TestMethod]
		public void IfElse1 ()
		{
			CompareTest ("IfElse1");
		}

		[TestMethod]
		public void IfElse2 ()
		{
			CompareTest ("IfElse2");
		}

		[TestMethod]
		public void IfElse3 ()
		{
			CompareTest ("IfElse3");
		}

		[TestMethod]
		public void IfElse4 ()
		{
			CompareTest ("IfElse4");
		}

		[TestMethod]
		public void IfElseIf1 ()
		{
			CompareTest ("IfElseIf1");
		}

		[TestMethod]
		public void IfElseIf2 ()
		{
			CompareTest ("IfElseIf2");
		}

		[TestMethod]
		public void IfElseIf3 ()
		{
			CompareTest ("IfElseIf3");
		}

		[TestMethod]
		public void IfElseIfElse1 ()
		{
			CompareTest ("IfElseIfElse1");
		}

		[TestMethod]
		public void IfElseIfElse2 ()
		{
			CompareTest ("IfElseIfElse2");
		}

		[TestMethod]
		public void ImplementsClause1 ()
		{
			CompareTest ("ImplementsClause1");
		}

		[TestMethod]
		public void ImplementsClause2 ()
		{
			CompareTest ("ImplementsClause2");
		}

		[TestMethod]
		public void Imports1 ()
		{
			CompareTest ("Imports1");
		}

		[TestMethod]
		public void Imports2 ()
		{
			CompareTest ("Imports2");
		}

		[TestMethod]
		public void Imports3 ()
		{
			CompareTest ("Imports3");
		}

		[TestMethod]
		public void Imports4 ()
		{
			CompareTest ("Imports4");
		}

		[TestMethod]
		public void Imports5 ()
		{
			CompareTest ("Imports5");
		}

		[TestMethod]
		public void ImportsNamespace1 ()
		{
			CompareTest ("ImportsNamespace1");
		}

		[TestMethod]
		public void ImportsType1 ()
		{
			CompareTest ("ImportsType1");
		}

		[TestMethod]
		public void ImportsType2 ()
		{
			CompareTest ("ImportsType2");
		}

		[TestMethod]
		public void InferFor1 ()
		{
			CompareTest ("InferFor1");
		}

		[TestMethod]
		public void IntDivConstantExpression1 ()
		{
			CompareTest ("IntDivConstantExpression1");
		}

		[TestMethod]
		public void IntDivExpression1 ()
		{
			CompareTest ("IntDivExpression1");
		}

		[TestMethod]
		public void IntDivisionAssignStatement1 ()
		{
			CompareTest ("IntDivisionAssignStatement1");
		}

		[TestMethod]
		public void IntegralTypes1 ()
		{
			CompareTest ("IntegralTypes1");
		}

		[TestMethod]
		public void InterfaceInheritance1 ()
		{
			CompareTest ("InterfaceInheritance1");
		}

		[TestMethod]
		public void InterfaceMemberSpecifier1 ()
		{
			CompareTest ("InterfaceMemberSpecifier1");
		}

		[TestMethod]
		public void InterfaceMemberSpecifier2 ()
		{
			CompareTest ("InterfaceMemberSpecifier2");
		}

		[TestMethod]
		public void Is1 ()
		{
			CompareTest ("Is1");
		}

		[TestMethod]
		public void IsNotExpression1 ()
		{
			CompareTest ("IsNotExpression1");
		}

		[TestMethod]
		public void JaggedArray1 ()
		{
			CompareTest ("JaggedArray1");
		}

		[TestMethod]
		public void JaggedArray2 ()
		{
			CompareTest ("JaggedArray2");
		}

		[TestMethod]
		public void JaggedArray3 ()
		{
			CompareTest ("JaggedArray3");
		}

		[TestMethod]
		public void Label1 ()
		{
			CompareTest ("Label1");
		}

		[TestMethod]
		public void LEConstantExpression1 ()
		{
			CompareTest ("LEConstantExpression1");
		}

		[TestMethod]
		public void LEExpression1 ()
		{
			CompareTest ("LEExpression1");
		}

		[TestMethod]
		public void Like1 ()
		{
			CompareTest ("Like1");
		}

		[TestMethod]
		public void LikeExpression1 ()
		{
			CompareTest ("LikeExpression1");
		}

		[TestMethod]
		public void Literals1 ()
		{
			CompareTest ("Literals1");
		}

		[TestMethod]
		public void LocalArrayVariable1 ()
		{
			CompareTest ("LocalArrayVariable1");
		}

		[TestMethod]
		public void LocalConstDeclaration1 ()
		{
			CompareTest ("LocalConstDeclaration1");
		}

		[TestMethod]
		public void LocalVariable1 ()
		{
			CompareTest ("LocalVariable1");
		}

		[TestMethod]
		public void LocalVariable2 ()
		{
			CompareTest ("LocalVariable2");
		}

		[TestMethod]
		public void LocalVariableAssignment1 ()
		{
			CompareTest ("LocalVariableAssignment1");
		}

		[TestMethod]
		public void LShiftAssignStatement1 ()
		{
			CompareTest ("LShiftAssignStatement1");
		}

		[TestMethod]
		public void LShiftConstantExpression1 ()
		{
			CompareTest ("LShiftConstantExpression1");
		}

		[TestMethod]
		public void LShiftExpression1 ()
		{
			CompareTest ("LShiftExpression1");
		}

		[TestMethod]
		public void LTConstantExpression1 ()
		{
			CompareTest ("LTConstantExpression1");
		}

		[TestMethod]
		public void LTExpression1 ()
		{
			CompareTest ("LTExpression1");
		}

		[TestMethod]
		public void MarshalAs ()
		{
			CompareTest ("MarshalAs");
		}

		[TestMethod]
		public void MeExpression1 ()
		{
			CompareTest ("MeExpression1");
		}

		[TestMethod]
		public void MeExpression2 ()
		{
			CompareTest ("MeExpression2");
		}

		[TestMethod]
		public void MemberAccessExpression1 ()
		{
			CompareTest ("MemberAccessExpression1");
		}

		[TestMethod]
		public void MemberAccessExpression2 ()
		{
			CompareTest ("MemberAccessExpression2");
		}

		[TestMethod]
		public void MemberAccessExpression3 ()
		{
			CompareTest ("MemberAccessExpression3");
		}

		[TestMethod]
		public void MemberAccessExpression4 ()
		{
			CompareTest ("MemberAccessExpression4");
		}

		[TestMethod]
		public void MemberAccessExpression5 ()
		{
			CompareTest ("MemberAccessExpression5");
		}

		[TestMethod]
		public void MemberAttributes1 ()
		{
			CompareTest ("MemberAttributes1");
		}

		[TestMethod]
		public void MethodAttributes1 ()
		{
			CompareTest ("MethodAttributes1");
		}

		[TestMethod]
		public void MethodInvocation1 ()
		{
			CompareTest ("MethodInvocation1");
		}

		[TestMethod]
		public void MethodResolution1 ()
		{
			CompareTest ("MethodResolution1");
		}

		[TestMethod]
		public void MethodsWithPointers1 ()
		{
			CompareTest ("MethodsWithPointers1");
		}

		[TestMethod]
		public void MethodVariable1 ()
		{
			CompareTest ("MethodVariable1");
		}

		[TestMethod]
		public void MethodVariable2 ()
		{
			CompareTest ("MethodVariable2");
		}

		[TestMethod]
		public void MethodVariable3 ()
		{
			CompareTest ("MethodVariable3");
		}

		[TestMethod]
		public void MidAssignStatement1 ()
		{
			CompareTest ("MidAssignStatement1");
		}

		[TestMethod]
		public void MidAssignStatement2 ()
		{
			CompareTest ("MidAssignStatement2");
		}

		[TestMethod]
		public void ModConstantExpression1 ()
		{
			CompareTest ("ModConstantExpression1");
		}

		[TestMethod]
		public void ModExpression1 ()
		{
			CompareTest ("ModExpression1");
		}

		[TestMethod]
		public void ModuleFunction1 ()
		{
			CompareTest ("ModuleFunction1");
		}

		[TestMethod]
		public void ModuleProperty1 ()
		{
			CompareTest ("ModuleProperty1");
		}

		[TestMethod]
		public void MultConstantExpression1 ()
		{
			CompareTest ("MultConstantExpression1");
		}

		[TestMethod]
		public void MultConstantExpression2 ()
		{
			CompareTest ("MultConstantExpression2");
		}

		[TestMethod]
		public void MultExpression1 ()
		{
			CompareTest ("MultExpression1");
		}

		[TestMethod]
		public void MultiFileTest1 ()
		{
			CompareTest ("MultiFileTest1");
		}

		[TestMethod]
		public void MultiFileTest2 ()
		{
			CompareTest ("MultiFileTest2");
		}

		[TestMethod]
		public void MultiplicationAssignStatement1 ()
		{
			CompareTest ("MultiplicationAssignStatement1");
		}

		[TestMethod]
		public void MultiVar1 ()
		{
			CompareTest ("MultiVar1");
		}

		[TestMethod]
		public void MultiVar2 ()
		{
			CompareTest ("MultiVar2");
		}

		[TestMethod]
		public void MultiVar3 ()
		{
			CompareTest ("MultiVar3");
		}

		[TestMethod]
		public void MultiVar4 ()
		{
			CompareTest ("MultiVar4");
		}

		[TestMethod]
		public void MyBaseExpression1 ()
		{
			CompareTest ("MyBaseExpression1");
		}

		[TestMethod]
		public void MyClassExpression1 ()
		{
			CompareTest ("MyClassExpression1");
		}

		[TestMethod]
		public void NameResolution1 ()
		{
			CompareTest ("NameResolution1");
		}

		[TestMethod]
		public void NameResolution2 ()
		{
			CompareTest ("NameResolution2");
		}

		[TestMethod]
		public void NameResolution3 ()
		{
			CompareTest ("NameResolution3");
		}

		[TestMethod]
		public void NameResolution4 ()
		{
			CompareTest ("NameResolution4");
		}

		[TestMethod]
		public void Narrowing1 ()
		{
			CompareTest ("Narrowing1");
		}

		[TestMethod]
		public void NestedType1 ()
		{
			CompareTest ("NestedType1");
		}

		[TestMethod]
		public void NestedType2 ()
		{
			CompareTest ("NestedType2");
		}

		[TestMethod]
		public void NestedType3 ()
		{
			CompareTest ("NestedType3");
		}

		[TestMethod]
		public void NewAssignment1 ()
		{
			CompareTest ("NewAssignment1");
		}

		[TestMethod]
		public void NewExpression1 ()
		{
			CompareTest ("NewExpression1");
		}

		[TestMethod]
		public void NewExpression2 ()
		{
			CompareTest ("NewExpression2");
		}

		[TestMethod]
		public void NewExpression3 ()
		{
			CompareTest ("NewExpression3");
		}

		[TestMethod]
		public void NewExpression4 ()
		{
			CompareTest ("NewExpression4");
		}

		[TestMethod]
		public void NotConstantExpression1 ()
		{
			CompareTest ("NotConstantExpression1");
		}

		[TestMethod]
		public void NotEqualsConstantExpression1 ()
		{
			CompareTest ("NotEqualsConstantExpression1");
		}

		[TestMethod]
		public void NotEqualsExpression1 ()
		{
			CompareTest ("NotEqualsExpression1");
		}

		[TestMethod]
		public void NotEqualsExpression2 ()
		{
			CompareTest ("NotEqualsExpression2");
		}

		[TestMethod]
		public void ObjectCreationExpression1 ()
		{
			CompareTest ("ObjectCreationExpression1");
		}

		[TestMethod]
		public void ObjectCreationExpression2 ()
		{
			CompareTest ("ObjectCreationExpression2");
		}

		[TestMethod]
		public void ObjectCreationExpression3 ()
		{
			CompareTest ("ObjectCreationExpression3");
		}

		[TestMethod]
		public void ObjectCreationExpression4 ()
		{
			CompareTest ("ObjectCreationExpression4");
		}

		[TestMethod]
		public void OnErroGoto_1 ()
		{
			CompareTest ("OnErroGoto_1");
		}

		[TestMethod]
		public void OnErrorGoto0_1 ()
		{
			CompareTest ("OnErrorGoto0_1");
		}

		[TestMethod]
		public void OnErrorGotoMinus1_1 ()
		{
			CompareTest ("OnErrorGotoMinus1_1");
		}

		[TestMethod]
		public void OnErrorResumeNext_1 ()
		{
			CompareTest ("OnErrorResumeNext_1");
		}

		[TestMethod]
		public void OptionalArguments1 ()
		{
			CompareTest ("OptionalArguments1");
		}

		[TestMethod]
		public void OptionalArguments2 ()
		{
			CompareTest ("OptionalArguments2");
		}

		[TestMethod]
		public void OrConstantExpression1 ()
		{
			CompareTest ("OrConstantExpression1");
		}

		[TestMethod]
		public void OrElseConstantExpression1 ()
		{
			CompareTest ("OrElseConstantExpression1");
		}

		[TestMethod]
		public void OrElseExpression1 ()
		{
			CompareTest ("OrElseExpression1");
		}

		[TestMethod]
		public void OrExpression1 ()
		{
			CompareTest ("OrExpression1");
		}

		[TestMethod]
		public void Partial1 ()
		{
			CompareTest ("Partial1");
		}

		[TestMethod]
		public void PowerAssignStatement1 ()
		{
			CompareTest ("PowerAssignStatement1");
		}

		[TestMethod]
		public void PropertyAccess5 ()
		{
			CompareTest ("PropertyAccess5");
		}

		[TestMethod]
		public void RaiseEvent1 ()
		{
			CompareTest ("RaiseEvent1");
		}

		[TestMethod]
		public void RaiseEvent2 ()
		{
			CompareTest ("RaiseEvent2");
		}

		[TestMethod]
		public void RealDivConstantExpression1 ()
		{
			CompareTest ("RealDivConstantExpression1");
		}

		[TestMethod]
		public void RealDivExpression1 ()
		{
			CompareTest ("RealDivExpression1");
		}

		[TestMethod]
		public void Redim1 ()
		{
			CompareTest ("Redim1");
		}

		[TestMethod]
		public void RedimPreserve1 ()
		{
			CompareTest ("RedimPreserve1");
		}

		[TestMethod]
		public void RedimPreserve2 ()
		{
			CompareTest ("RedimPreserve2");
		}

		[TestMethod]
		public void RedimPreserve3 ()
		{
			CompareTest ("RedimPreserve3");
		}

		[TestMethod]
		public void RemoveHandler1 ()
		{
			CompareTest ("RemoveHandler1");
		}

		[TestMethod]
		public void Resume1 ()
		{
			CompareTest ("Resume1");
		}

		[TestMethod]
		public void ResumeNext1 ()
		{
			CompareTest ("ResumeNext1");
		}

		[TestMethod]
		public void Return1 ()
		{
			CompareTest ("Return1");
		}

		[TestMethod]
		public void RShiftAssignStatement1 ()
		{
			CompareTest ("RShiftAssignStatement1");
		}

		[TestMethod]
		public void RShiftConstantExpression1 ()
		{
			CompareTest ("RShiftConstantExpression1");
		}

		[TestMethod]
		public void RShiftExpression1 ()
		{
			CompareTest ("RShiftExpression1");
		}

		[TestMethod]
		public void SecurityAttribute1 ()
		{
			CompareTest ("SecurityAttribute1");
		}

		[TestMethod]
		public void SecurityAttribute2 ()
		{
			CompareTest ("SecurityAttribute2");
		}

		[TestMethod]
		public void SelectCase1 ()
		{
			CompareTest ("SelectCase1");
		}

		[TestMethod]
		public void SingleLineIfWithBlockStatements1 ()
		{
			CompareTest ("SingleLineIfWithBlockStatements1");
		}

		[TestMethod]
		public void SingleLineIfWithNonBlockStatements1 ()
		{
			CompareTest ("SingleLineIfWithNonBlockStatements1");
		}

		[TestMethod]
		public void SpecBug1 ()
		{
			CompareTest ("SpecBug1");
		}

		[TestMethod]
		public void StaticVariable1 ()
		{
			CompareTest ("StaticVariable1");
		}

		[TestMethod]
		public void StaticVariable6 ()
		{
			CompareTest ("StaticVariable6");
		}

		[TestMethod]
		public void Stop1 ()
		{
			CompareTest ("Stop1");
		}

		[TestMethod]
		public void StructureAccess1 ()
		{
			CompareTest ("StructureAccess1");
		}

		[TestMethod]
		public void SubtractionAssignStatement1 ()
		{
			CompareTest ("SubtractionAssignStatement1");
		}

		[TestMethod]
		public void SyncLock1 ()
		{
			CompareTest ("SyncLock1");
		}

		[TestMethod]
		public void test1 ()
		{
			CompareTest ("test1");
		}

		[TestMethod]
		public void Throw1 ()
		{
			CompareTest ("Throw1");
		}

		[TestMethod]
		public void Throw2 ()
		{
			CompareTest ("Throw2");
		}

		[TestMethod]
		public void TryCastExpression1 ()
		{
			CompareTest ("TryCastExpression1");
		}

		[TestMethod]
		public void TryCatch1 ()
		{
			CompareTest ("TryCatch1");
		}

		[TestMethod]
		public void TryCatch5 ()
		{
			CompareTest ("TryCatch5");
		}

		[TestMethod]
		public void TryCatchFinally1 ()
		{
			CompareTest ("TryCatchFinally1");
		}

		[TestMethod]
		public void TryFinally1 ()
		{
			CompareTest ("TryFinally1");
		}

		[TestMethod]
		[Ignore]
		public void TypeLoadEx1 ()
		{
			CompareTest ("TypeLoadEx1");
		}

		[TestMethod]
		public void TypeLoadEx2 ()
		{
			CompareTest ("TypeLoadEx2");
		}

		[TestMethod]
		public void TypeNameResolution1 ()
		{
			CompareTest ("TypeNameResolution1");
		}

		[TestMethod]
		public void TypeNameResolution2 ()
		{
			CompareTest ("TypeNameResolution2");
		}

		[TestMethod]
		public void TypeNameResolution3 ()
		{
			CompareTest ("TypeNameResolution3");
		}

		[TestMethod]
		public void TypeOfExpression1 ()
		{
			CompareTest ("TypeOfExpression1");
		}

		[TestMethod]
		public void UnaryAddExpression1 ()
		{
			CompareTest ("UnaryAddExpression1");
		}

		[TestMethod]
		public void UnaryMinusExpression1 ()
		{
			CompareTest ("UnaryMinusExpression1");
		}

		[TestMethod]
		public void UnaryNotExpression1 ()
		{
			CompareTest ("UnaryNotExpression1");
		}

		[TestMethod]
		public void UsingStatement1 ()
		{
			CompareTest ("UsingStatement1");
		}

		[TestMethod]
		public void UsingStatement2 ()
		{
			CompareTest ("UsingStatement2");
		}

		[TestMethod]
		public void UsingStatement3 ()
		{
			CompareTest ("UsingStatement3");
		}

		[TestMethod]
		public void UsingStatement4 ()
		{
			CompareTest ("UsingStatement4");
		}

		[TestMethod]
		public void VariableInitializer1 ()
		{
			CompareTest ("VariableInitializer1");
		}

		[TestMethod]
		public void VariableInitializer2 ()
		{
			CompareTest ("VariableInitializer2");
		}

		[TestMethod]
		public void While1 ()
		{
			CompareTest ("While1");
		}

		[TestMethod]
		public void With1 ()
		{
			CompareTest ("With1");
		}

		[TestMethod]
		public void XorConstantExpression1 ()
		{
			CompareTest ("XorConstantExpression1");
		}

		[TestMethod]
		public void XOrExpression1 ()
		{
			CompareTest ("XOrExpression1");
		}

		private void CompareTest (string source_file)
		{
			var source = File.ReadAllText (Path.Combine (input_location, source_file + ".vb"));

			var tree = SyntaxTree.ParseCompilationUnit (source);
			var root = (CompilationUnitSyntax)tree.Root;

			TokenComparer.Compare (root, Path.Combine (output_location, source_file + ".xml"));
		}
	}
}

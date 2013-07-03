﻿// 
// Copyright (c) 2011 Jonathan Pobst
//  
// Author:
//       Jonathan Pobst <monkey@jpobst.com>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace Mono.Compilers.VisualBasic
{
	public enum SyntaxKind
	{
		None = 0,
		List = 1,
		EmptyStatement = 2,
		EndIfStatement = 3,
		EndUsingStatement = 4,
		EndWithStatement = 5,
		EndSelectStatement = 6,
		EndStructureStatement = 7,
		EndEnumStatement = 8,
		EndInterfaceStatement = 9,
		EndClassStatement = 10,
		EndModuleStatement = 11,
		EndNamespaceStatement = 12,
		EndSubStatement = 13,
		EndFunctionStatement = 14,
		EndGetStatement = 15,
		EndSetStatement = 16,
		EndPropertyStatement = 17,
		EndOperatorStatement = 18,
		EndEventStatement = 19,
		EndAddHandlerStatement = 20,
		EndRemoveHandlerStatement = 21,
		EndRaiseEventStatement = 22,
		EndWhileStatement = 23,
		EndTryStatement = 24,
		EndSyncLockStatement = 25,
		CompilationUnit = 26,
		OptionStatement = 27,
		ImportsStatement = 28,
		AliasImportsClause = 29,
		MembersImportsClause = 30,
		XmlNamespaceImportsClause = 31,
		NamespaceBlock = 32,
		NamespaceStatement = 33,
		ModuleBlock = 34,
		StructureBlock = 35,
		InterfaceBlock = 36,
		ClassBlock = 37,
		EnumBlock = 38,
		InheritsStatement = 39,
		ImplementsStatement = 40,
		ModuleStatement = 41,
		StructureStatement = 42,
		InterfaceStatement = 43,
		ClassStatement = 44,
		EnumStatement = 45,
		TypeParameterList = 46,
		TypeParameter = 47,
		TypeParameterSingleConstraintClause = 48,
		TypeParameterMultipleConstraintClause = 49,
		NewConstraint = 50,
		ClassConstraint = 51,
		StructureConstraint = 52,
		TypeConstraint = 53,
		EnumMemberDeclaration = 54,
		SubBlock = 55,
		FunctionBlock = 56,
		ConstructorBlock = 57,
		OperatorBlock = 58,
		PropertyGetBlock = 59,
		PropertySetBlock = 60,
		AddHandlerBlock = 61,
		RemoveHandlerBlock = 62,
		RaiseEventBlock = 63,
		PropertyBlock = 64,
		EventBlock = 65,
		ParameterList = 66,
		SubStatement = 67,
		FunctionStatement = 68,
		ConstructorStatement = 69,
		DeclareSubStatement = 70,
		DeclareFunctionStatement = 71,
		DelegateSubStatement = 72,
		DelegateFunctionStatement = 73,
		EventStatement = 74,
		OperatorStatement = 75,
		PropertyStatement = 76,
		GetAccessorStatement = 77,
		SetAccessorStatement = 78,
		AddHandlerAccessorStatement = 79,
		RemoveHandlerAccessorStatement = 80,
		RaiseEventHandlerAccessorStatement = 81,
		ImplementsClause = 82,
		HandlesClause = 83,
		KeywordEventContainer = 84,
		IdentifierEventContainer = 85,
		HandlesClauseItem = 86,
		IncompleteMember = 87,
		FieldDeclaration = 88,
		VariableDeclarator = 89,
		SimpleAsClause = 90,
		AsNewClause = 91,
		ObjectMemberInitializer = 92,
		ObjectCollectionInitializer = 93,
		InferredFieldInitializer = 94,
		NamedFieldInitializer = 95,
		EqualsValue = 96,
		Parameter = 97,
		ModifiedIdentifier = 98,
		ArrayRankSpecifier = 99,
		AttributeList = 100,
		Attribute = 101,
		AttributeTarget = 102,
		AttributesStatement = 103,
		ExpressionStatement = 104,
		WhileBlock = 105,
		UsingBlock = 106,
		SyncLockBlock = 107,
		WithBlock = 108,
		LocalDeclarationStatement = 109,
		LabelStatement = 110,
		GoToStatement = 111,
		IdentifierLabel = 112,
		NumericLabel = 113,
		NextLabel = 114,
		StopStatement = 115,
		EndStatement = 116,
		ExitDoStatement = 117,
		ExitForStatement = 118,
		ExitSubStatement = 119,
		ExitFunctionStatement = 120,
		ExitOperatorStatement = 121,
		ExitPropertyStatement = 122,
		ExitTryStatement = 123,
		ExitSelectStatement = 124,
		ExitWhileStatement = 125,
		ContinueWhileStatement = 126,
		ContinueDoStatement = 127,
		ContinueForStatement = 128,
		ReturnStatement = 129,
		SingleLineIfStatement = 130,
		SingleLineIfPart = 131,
		SingleLineElsePart = 132,
		MultiLineIfBlock = 133,
		IfPart = 134,
		ElseIfPart = 135,
		ElsePart = 136,
		IfStatement = 137,
		ElseIfStatement = 138,
		ElseStatement = 139,
		TryBlock = 140,
		TryPart = 141,
		CatchPart = 142,
		FinallyPart = 143,
		TryStatement = 144,
		CatchStatement = 145,
		CatchFilterClause = 146,
		FinallyStatement = 147,
		ErrorStatement = 148,
		OnErrorGoToZeroStatement = 149,
		OnErrorGoToMinusOneStatement = 150,
		OnErrorGoToLabelStatement = 151,
		OnErrorResumeNextStatement = 152,
		ResumeStatement = 153,
		ResumeLabelStatement = 154,
		ResumeNextStatement = 155,
		SelectBlock = 156,
		SelectStatement = 157,
		CaseBlock = 158,
		CaseElseBlock = 159,
		CaseStatement = 160,
		CaseElseStatement = 161,
		CaseElseClause = 162,
		CaseValueClause = 163,
		CaseRangeClause = 164,
		CaseEqualsClause = 165,
		CaseNotEqualsClause = 166,
		CaseLessThanClause = 167,
		CaseLessThanOrEqualClause = 168,
		CaseGreaterThanOrEqualClause = 169,
		CaseGreaterThanClause = 170,
		SyncLockStatement = 171,
		DoLoopTopTestBlock = 172,
		DoLoopBottomTestBlock = 173,
		DoLoopForeverBlock = 174,
		DoStatement = 175,
		LoopStatement = 176,
		WhileClause = 177,
		UntilClause = 178,
		WhileStatement = 179,
		ForBlock = 180,
		ForEachBlock = 181,
		ForStatement = 182,
		ForStepClause = 183,
		ForEachStatement = 184,
		NextStatement = 185,
		UsingStatement = 186,
		ThrowStatement = 187,
		AssignmentStatement = 188,
		MidAssignment = 189,
		AddAssignment = 190,
		SubtractAssignment = 191,
		MultiplyAssignment = 192,
		DivideAssignment = 193,
		IntegerDivideAssignment = 194,
		PowerAssignment = 195,
		LeftShiftAssignment = 196,
		RightShiftAssignment = 197,
		ConcatenateAssignment = 198,
		CallStatement = 199,
		AddHandlerStatement = 200,
		RemoveHandlerStatement = 201,
		RaiseEventStatement = 202,
		WithStatement = 203,
		ReDimStatement = 204,
		ReDimPreserveStatement = 205,
		EraseStatement = 206,
		CharacterLiteralExpression = 207,
		TrueLiteralExpression = 208,
		FalseLiteralExpression = 209,
		NumericLiteralExpression = 210,
		DateLiteralExpression = 211,
		StringLiteralExpression = 212,
		NothingLiteralExpression = 213,
		ParenthesizedExpression = 214,
		MeExpression = 215,
		MyBaseExpression = 216,
		MyClassExpression = 217,
		GetTypeExpression = 218,
		TypeOfIsExpression = 219,
		TypeOfIsNotExpression = 220,
		GetXmlNamespaceExpression = 221,
		MemberAccessExpression = 222,
		DictionaryAccessExpression = 223,
		XmlElementAccessExpression = 224,
		XmlDescendantAccessExpression = 225,
		XmlAttributeAccessExpression = 226,
		InvocationExpression = 227,
		ObjectCreationExpression = 228,
		AnonymousObjectCreationExpression = 229,
		ArrayCreationExpression = 230,
		CollectionInitializer = 231,
		CTypeExpression = 232,
		DirectCastExpression = 233,
		TryCastExpression = 234,
		PredefinedCastExpression = 235,
		AddExpression = 236,
		SubtractExpression = 237,
		MultiplyExpression = 238,
		DivideExpression = 239,
		IntegerDivideExpression = 240,
		PowerExpression = 241,
		LeftShiftExpression = 242,
		RightShiftExpression = 243,
		ConcatenateExpression = 244,
		ModuloExpression = 245,
		EqualsExpression = 246,
		NotEqualsExpression = 247,
		LessThanExpression = 248,
		LessThanOrEqualExpression = 249,
		GreaterThanOrEqualExpression = 250,
		GreaterThanExpression = 251,
		IsExpression = 252,
		IsNotExpression = 253,
		LikeExpression = 254,
		OrExpression = 255,
		XorExpression = 256,
		AndExpression = 257,
		OrElseExpression = 258,
		AndAlsoExpression = 259,
		PlusExpression = 260,
		NegateExpression = 261,
		NotExpression = 262,
		AddressOfExpression = 263,
		BinaryConditionalExpression = 264,
		TernaryConditionalExpression = 265,
		SingleLineFunctionLambdaExpression = 266,
		SingleLineSubLambdaExpression = 267,
		MultiLineFunctionLambdaExpression = 268,
		MultiLineSubLambdaExpression = 269,
		SubLambdaHeader = 270,
		FunctionLambdaHeader = 271,
		ArgumentList = 272,
		OmittedArgument = 273,
		SimpleArgument = 274,
		NamedArgument = 275,
		RangeArgument = 276,
		QueryExpression = 277,
		CollectionRangeVariable = 278,
		ExpressionRangeVariable = 279,
		AggregationRangeVariable = 280,
		VariableNameEquals = 281,
		FunctionAggregation = 282,
		GroupAggregation = 283,
		FromClause = 284,
		LetClause = 285,
		AggregateClause = 286,
		DistinctClause = 287,
		WhereClause = 288,
		SkipWhileClause = 289,
		TakeWhileClause = 290,
		SkipClause = 291,
		TakeClause = 292,
		GroupByClause = 293,
		JoinClause = 294,
		JoinCondition = 295,
		GroupJoinClause = 296,
		OrderByClause = 297,
		AscendingOrdering = 298,
		DescendingOrdering = 299,
		SelectClause = 300,
		XmlDocument = 301,
		XmlDeclaration = 302,
		XmlDeclarationOption = 303,
		XmlElement = 304,
		XmlText = 305,
		XmlElementStartTag = 306,
		XmlElementEndTag = 307,
		XmlEmptyElement = 308,
		XmlAttribute = 309,
		XmlString = 310,
		XmlPrefixName = 311,
		XmlName = 312,
		XmlBracketedName = 313,
		XmlPrefix = 314,
		XmlComment = 315,
		XmlProcessingInstruction = 316,
		XmlCDataSection = 317,
		XmlEmbeddedExpression = 318,
		ArrayType = 319,
		NullableType = 320,
		PredefinedType = 321,
		IdentifierName = 322,
		GenericName = 323,
		QualifiedName = 324,
		GlobalName = 325,
		TypeArgumentList = 326,
		AddHandlerKeyword = 327,
		AddressOfKeyword = 328,
		AliasKeyword = 329,
		AndKeyword = 330,
		AndAlsoKeyword = 331,
		AsKeyword = 332,
		BooleanKeyword = 333,
		ByRefKeyword = 334,
		ByteKeyword = 335,
		ByValKeyword = 336,
		CallKeyword = 337,
		CaseKeyword = 338,
		CatchKeyword = 339,
		CBoolKeyword = 340,
		CByteKeyword = 341,
		CCharKeyword = 342,
		CDateKeyword = 343,
		CDecKeyword = 344,
		CDblKeyword = 345,
		CharKeyword = 346,
		CIntKeyword = 347,
		ClassKeyword = 348,
		CLngKeyword = 349,
		CObjKeyword = 350,
		ConstKeyword = 351,
		ReferenceKeyword = 352,
		ContinueKeyword = 353,
		CSByteKeyword = 354,
		CShortKeyword = 355,
		CSngKeyword = 356,
		CStrKeyword = 357,
		CTypeKeyword = 358,
		CUIntKeyword = 359,
		CULngKeyword = 360,
		CUShortKeyword = 361,
		DateKeyword = 362,
		DecimalKeyword = 363,
		DeclareKeyword = 364,
		DefaultKeyword = 365,
		DelegateKeyword = 366,
		DimKeyword = 367,
		DirectCastKeyword = 368,
		DoKeyword = 369,
		DoubleKeyword = 370,
		EachKeyword = 371,
		ElseKeyword = 372,
		ElseIfKeyword = 373,
		EndKeyword = 374,
		EnumKeyword = 375,
		EraseKeyword = 376,
		ErrorKeyword = 377,
		EventKeyword = 378,
		ExitKeyword = 379,
		FalseKeyword = 380,
		FinallyKeyword = 381,
		ForKeyword = 382,
		FriendKeyword = 383,
		FunctionKeyword = 384,
		GetKeyword = 385,
		GetTypeKeyword = 386,
		GetXmlNamespaceKeyword = 387,
		GlobalKeyword = 388,
		GoToKeyword = 389,
		HandlesKeyword = 390,
		IfKeyword = 391,
		ImplementsKeyword = 392,
		ImportsKeyword = 393,
		InKeyword = 394,
		InheritsKeyword = 395,
		IntegerKeyword = 396,
		InterfaceKeyword = 397,
		IsKeyword = 398,
		IsNotKeyword = 399,
		LetKeyword = 400,
		LibKeyword = 401,
		LikeKeyword = 402,
		LongKeyword = 403,
		LoopKeyword = 404,
		MeKeyword = 405,
		ModKeyword = 406,
		ModuleKeyword = 407,
		MustInheritKeyword = 408,
		MustOverrideKeyword = 409,
		MyBaseKeyword = 410,
		MyClassKeyword = 411,
		NamespaceKeyword = 412,
		NarrowingKeyword = 413,
		NextKeyword = 414,
		NewKeyword = 415,
		NotKeyword = 416,
		NothingKeyword = 417,
		NotInheritableKeyword = 418,
		NotOverridableKeyword = 419,
		ObjectKeyword = 420,
		OfKeyword = 421,
		OnKeyword = 422,
		OperatorKeyword = 423,
		OptionKeyword = 424,
		OptionalKeyword = 425,
		OrKeyword = 426,
		OrElseKeyword = 427,
		OverloadsKeyword = 428,
		OverridableKeyword = 429,
		OverridesKeyword = 430,
		ParamArrayKeyword = 431,
		PartialKeyword = 432,
		PrivateKeyword = 433,
		PropertyKeyword = 434,
		ProtectedKeyword = 435,
		PublicKeyword = 436,
		RaiseEventKeyword = 437,
		ReadOnlyKeyword = 438,
		ReDimKeyword = 439,
		REMKeyword = 440,
		RemoveHandlerKeyword = 441,
		ResumeKeyword = 442,
		ReturnKeyword = 443,
		SByteKeyword = 444,
		SelectKeyword = 445,
		SetKeyword = 446,
		ShadowsKeyword = 447,
		SharedKeyword = 448,
		ShortKeyword = 449,
		SingleKeyword = 450,
		StaticKeyword = 451,
		StepKeyword = 452,
		StopKeyword = 453,
		StringKeyword = 454,
		StructureKeyword = 455,
		SubKeyword = 456,
		SyncLockKeyword = 457,
		ThenKeyword = 458,
		ThrowKeyword = 459,
		ToKeyword = 460,
		TrueKeyword = 461,
		TryKeyword = 462,
		TryCastKeyword = 463,
		TypeOfKeyword = 464,
		UIntegerKeyword = 465,
		ULongKeyword = 466,
		UShortKeyword = 467,
		UsingKeyword = 468,
		WhenKeyword = 469,
		WhileKeyword = 470,
		WideningKeyword = 471,
		WithKeyword = 472,
		WithEventsKeyword = 473,
		WriteOnlyKeyword = 474,
		XorKeyword = 475,
		EndIfKeyword = 476,
		GosubKeyword = 477,
		VariantKeyword = 478,
		WendKeyword = 479,
		AggregateKeyword = 480,
		AllKeyword = 481,
		AnsiKeyword = 482,
		AscendingKeyword = 483,
		AssemblyKeyword = 484,
		AutoKeyword = 485,
		BinaryKeyword = 486,
		ByKeyword = 487,
		CompareKeyword = 488,
		CustomKeyword = 489,
		DescendingKeyword = 490,
		DistinctKeyword = 491,
		EqualsKeyword = 492,
		ExplicitKeyword = 493,
		ExternalSourceKeyword = 494,
		ExternalChecksumKeyword = 495,
		FromKeyword = 496,
		GroupKeyword = 497,
		InferKeyword = 498,
		IntoKeyword = 499,
		IsFalseKeyword = 500,
		IsTrueKeyword = 501,
		JoinKeyword = 502,
		KeyKeyword = 503,
		MidKeyword = 504,
		OffKeyword = 505,
		OrderKeyword = 506,
		OutKeyword = 507,
		PreserveKeyword = 508,
		RegionKeyword = 509,
		SkipKeyword = 510,
		StrictKeyword = 511,
		TakeKeyword = 512,
		TextKeyword = 513,
		UnicodeKeyword = 514,
		UntilKeyword = 515,
		WhereKeyword = 516,
		TypeKeyword = 517,
		XmlKeyword = 518,
		ExclamationToken = 519,
		AtToken = 520,
		CommaToken = 521,
		HashToken = 522,
		AmpersandToken = 523,
		SingleQuoteToken = 524,
		OpenParenToken = 525,
		CloseParenToken = 526,
		OpenBraceToken = 527,
		CloseBraceToken = 528,
		SemicolonToken = 529,
		AsteriskToken = 530,
		PlusToken = 531,
		MinusToken = 532,
		DotToken = 533,
		SlashToken = 534,
		ColonToken = 535,
		LessThanToken = 536,
		LessThanEqualsToken = 537,
		LessThanGreaterThanToken = 538,
		EqualsToken = 539,
		GreaterThanToken = 540,
		GreaterThanEqualsToken = 541,
		BackslashToken = 542,
		CaretToken = 543,
		ColonEqualsToken = 544,
		AmpersandEqualsToken = 545,
		AsteriskEqualsToken = 546,
		PlusEqualsToken = 547,
		MinusEqualsToken = 548,
		SlashEqualsToken = 549,
		BackslashEqualsToken = 550,
		CaretEqualsToken = 551,
		LessThanLessThanToken = 552,
		GreaterThanGreaterThanToken = 553,
		LessThanLessThanEqualsToken = 554,
		GreaterThanGreaterThanEqualsToken = 555,
		QuestionToken = 556,
		DoubleQuoteToken = 557,
		StatementTerminatorToken = 558,
		EndOfFileToken = 559,
		EmptyToken = 560,
		SlashGreaterThanToken = 561,
		LessThanSlashToken = 562,
		LessThanMinusMinusToken = 563,
		MinusMinusGreaterThanToken = 564,
		LessThanQuestionToken = 565,
		QuestionGreaterThanToken = 566,
		LessThanPercentEqualsToken = 567,
		PercentGreaterThanToken = 568,
		BeginCDataToken = 569,
		EndCDataToken = 570,
		EndOfXmlToken = 571,
		BadToken = 572,
		XmlNameToken = 573,
		XmlTextLiteralToken = 574,
		XmlEntityLiteralToken = 575,
		DocumentationCommentLineBreakToken = 576,
		IdentifierToken = 577,
		IntegerLiteralToken = 578,
		FloatingLiteralToken = 579,
		DecimalLiteralToken = 580,
		DateLiteralToken = 581,
		StringLiteralToken = 582,
		CharacterLiteralToken = 583,
		SkippedTokensTrivia = 584,
		DocumentationCommentTrivia = 585,
		DirectiveTrivia = 586,
		WhitespaceTrivia = 587,
		EndOfLineTrivia = 588,
		ColonTrivia = 589,
		CommentTrivia = 590,
		LineContinuationTrivia = 591,
		ImplicitLineContinuationTrivia = 592,
		DocumentationCommentExteriorTrivia = 593,
		DisabledTextTrivia = 594,
		ConstDirective = 595,
		IfDirective = 596,
		ElseIfDirective = 597,
		ElseDirective = 598,
		EndIfDirective = 599,
		RegionDirective = 600,
		EndRegionDirective = 601,
		ExternalSourceDirective = 602,
		EndExternalSourceDirective = 603,
		ExternalChecksumDirective = 604,
		ReferenceDirective = 605,
		BadDirective = 606
	}
}
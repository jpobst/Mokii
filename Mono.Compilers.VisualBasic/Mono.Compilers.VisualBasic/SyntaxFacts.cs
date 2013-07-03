// 
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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Mono.Compilers.VisualBasic
{
	public sealed class SyntaxFacts
	{
		private static Dictionary<string, SyntaxKind> contextual_lookup;
		private static Dictionary<string, SyntaxKind> keyword_lookup;
		private static Dictionary<string, SyntaxKind> reserved_keyword_lookup;

		static SyntaxFacts ()
		{
			contextual_lookup = new Dictionary<string, SyntaxKind> ();
			keyword_lookup = new Dictionary<string, SyntaxKind> ();
			reserved_keyword_lookup = new Dictionary<string, SyntaxKind> ();

			foreach (var keyword in contextual_keywords)
				contextual_lookup.Add (keyword.ToString ().ToLowerInvariant ().Substring (0, keyword.ToString ().Length - 7), keyword);

			foreach (var keyword in reserved_keywords)
				reserved_keyword_lookup.Add (keyword.ToString ().ToLowerInvariant ().Substring (0, keyword.ToString ().Length - 7), keyword);

			foreach (var keyword in GetKeywordKinds ())
				keyword_lookup.Add (keyword.ToString ().ToLowerInvariant ().Substring (0, keyword.ToString ().Length - 7), keyword);
		}

		#region Public Methods
		public static SyntaxKind GetContextualKeywordKind (string text)
		{
			SyntaxKind kind;

			if (contextual_lookup.TryGetValue (text, out kind))
				return kind;

			return SyntaxKind.None;
		}

		public static IEnumerable<SyntaxKind> GetContextualKeywordKinds ()
		{
			return contextual_keywords;
		}

		public static SyntaxKind GetKeywordKind (string text)
		{
			SyntaxKind kind;

			if (keyword_lookup.TryGetValue (text, out kind))
				return kind;

			return SyntaxKind.None;
		}

		public static IEnumerable<SyntaxKind> GetKeywordKinds ()
		{
			return reserved_keywords.Union (contextual_keywords);
		}

		public static IEnumerable<SyntaxKind> GetPreprocessorKeywordKinds ()
		{
			return preprocessor_keywords;
		}

		public static IEnumerable<SyntaxKind> GetPunctuationKinds ()
		{
			return punctuation_tokens;
		}

		public static bool IsContextualKeyword (SyntaxKind kind)
		{
			return contextual_keywords.Contains (kind);
		}

		public static bool IsKeyword (SyntaxToken token)
		{
			switch (token.Kind) {
				case SyntaxKind.ImportsKeyword:
				case SyntaxKind.NamespaceKeyword:
				case SyntaxKind.ModuleKeyword:
				case SyntaxKind.SubKeyword:
				case SyntaxKind.AsKeyword:
				case SyntaxKind.DimKeyword:
				case SyntaxKind.IfKeyword:
				case SyntaxKind.ThenKeyword:
				case SyntaxKind.EndKeyword:
					return true;
			}

			return false;
		}
		#endregion

		#region Internal Methods
		internal static SyntaxKind GetReservedKeywordKind (string text)
		{
			SyntaxKind kind;
			if (reserved_keyword_lookup.TryGetValue (text.ToLowerInvariant (), out kind)) {
				return kind;
			}

			return SyntaxKind.None;
		}
		#endregion

		private static readonly SyntaxKind[] reserved_keywords = new SyntaxKind[]
		{
			SyntaxKind.AddHandlerKeyword, 
			SyntaxKind.AddressOfKeyword, 
			SyntaxKind.AliasKeyword, 
			SyntaxKind.AndKeyword, 
			SyntaxKind.AndAlsoKeyword, 
			SyntaxKind.AsKeyword, 
			SyntaxKind.BooleanKeyword, 
			SyntaxKind.ByRefKeyword, 
			SyntaxKind.ByteKeyword, 
			SyntaxKind.ByValKeyword, 
			SyntaxKind.CallKeyword, 
			SyntaxKind.CaseKeyword, 
			SyntaxKind.CatchKeyword, 
			SyntaxKind.CBoolKeyword, 
			SyntaxKind.CByteKeyword, 
			SyntaxKind.CCharKeyword, 
			SyntaxKind.CDateKeyword, 
			SyntaxKind.CDecKeyword, 
			SyntaxKind.CDblKeyword, 
			SyntaxKind.CharKeyword, 
			SyntaxKind.CIntKeyword, 
			SyntaxKind.ClassKeyword, 
			SyntaxKind.CLngKeyword, 
			SyntaxKind.CObjKeyword, 
			SyntaxKind.ConstKeyword, 
			SyntaxKind.ContinueKeyword, 
			SyntaxKind.CSByteKeyword, 
			SyntaxKind.CShortKeyword, 
			SyntaxKind.CSngKeyword, 
			SyntaxKind.CStrKeyword, 
			SyntaxKind.CTypeKeyword, 
			SyntaxKind.CUIntKeyword, 
			SyntaxKind.CULngKeyword, 
			SyntaxKind.CUShortKeyword, 
			SyntaxKind.DateKeyword, 
			SyntaxKind.DecimalKeyword, 
			SyntaxKind.DeclareKeyword, 
			SyntaxKind.DefaultKeyword, 
			SyntaxKind.DelegateKeyword, 
			SyntaxKind.DimKeyword, 
			SyntaxKind.DirectCastKeyword, 
			SyntaxKind.DoKeyword, 
			SyntaxKind.DoubleKeyword, 
			SyntaxKind.EachKeyword, 
			SyntaxKind.ElseKeyword, 
			SyntaxKind.ElseIfKeyword, 
			SyntaxKind.EndKeyword, 
			SyntaxKind.EnumKeyword, 
			SyntaxKind.EraseKeyword, 
			SyntaxKind.ErrorKeyword, 
			SyntaxKind.EventKeyword, 
			SyntaxKind.ExitKeyword, 
			SyntaxKind.FalseKeyword, 
			SyntaxKind.FinallyKeyword, 
			SyntaxKind.ForKeyword, 
			SyntaxKind.FriendKeyword, 
			SyntaxKind.FunctionKeyword, 
			SyntaxKind.GetKeyword, 
			SyntaxKind.GetTypeKeyword, 
			SyntaxKind.GetXmlNamespaceKeyword, 
			SyntaxKind.GlobalKeyword, 
			SyntaxKind.GoToKeyword, 
			SyntaxKind.HandlesKeyword, 
			SyntaxKind.IfKeyword, 
			SyntaxKind.ImplementsKeyword, 
			SyntaxKind.ImportsKeyword, 
			SyntaxKind.InKeyword, 
			SyntaxKind.InheritsKeyword, 
			SyntaxKind.IntegerKeyword, 
			SyntaxKind.InterfaceKeyword, 
			SyntaxKind.IsKeyword, 
			SyntaxKind.IsNotKeyword, 
			SyntaxKind.LetKeyword, 
			SyntaxKind.LibKeyword, 
			SyntaxKind.LikeKeyword, 
			SyntaxKind.LongKeyword, 
			SyntaxKind.LoopKeyword, 
			SyntaxKind.MeKeyword, 
			SyntaxKind.ModKeyword, 
			SyntaxKind.ModuleKeyword, 
			SyntaxKind.MustInheritKeyword, 
			SyntaxKind.MustOverrideKeyword, 
			SyntaxKind.MyBaseKeyword, 
			SyntaxKind.MyClassKeyword,
			SyntaxKind.NamespaceKeyword, 
			SyntaxKind.NarrowingKeyword, 
			SyntaxKind.NextKeyword, 
			SyntaxKind.NewKeyword, 
			SyntaxKind.NotKeyword, 
			SyntaxKind.NothingKeyword, 
			SyntaxKind.NotInheritableKeyword, 
			SyntaxKind.NotOverridableKeyword, 
			SyntaxKind.ObjectKeyword, 
			SyntaxKind.OfKeyword, 
			SyntaxKind.OnKeyword, 
			SyntaxKind.OperatorKeyword, 
			SyntaxKind.OptionKeyword, 
			SyntaxKind.OptionalKeyword, 
			SyntaxKind.OrKeyword, 
			SyntaxKind.OrElseKeyword, 
			SyntaxKind.OverloadsKeyword, 
			SyntaxKind.OverridableKeyword, 
			SyntaxKind.OverridesKeyword, 
			SyntaxKind.ParamArrayKeyword, 
			SyntaxKind.PartialKeyword, 
			SyntaxKind.PrivateKeyword, 
			SyntaxKind.PropertyKeyword, 
			SyntaxKind.ProtectedKeyword,
			SyntaxKind.PublicKeyword, 
			SyntaxKind.RaiseEventKeyword, 
			SyntaxKind.ReadOnlyKeyword, 
			SyntaxKind.ReDimKeyword, 
			SyntaxKind.REMKeyword, 
			SyntaxKind.RemoveHandlerKeyword, 
			SyntaxKind.ResumeKeyword, 
			SyntaxKind.ReturnKeyword, 
			SyntaxKind.SByteKeyword, 
			SyntaxKind.SelectKeyword, 
			SyntaxKind.SetKeyword, 
			SyntaxKind.ShadowsKeyword, 
			SyntaxKind.SharedKeyword, 
			SyntaxKind.ShortKeyword, 
			SyntaxKind.SingleKeyword, 
			SyntaxKind.StaticKeyword, 
			SyntaxKind.StepKeyword, 
			SyntaxKind.StopKeyword, 
			SyntaxKind.StringKeyword, 
			SyntaxKind.StructureKeyword, 
			SyntaxKind.SubKeyword, 
			SyntaxKind.SyncLockKeyword, 
			SyntaxKind.ThenKeyword, 
			SyntaxKind.ThrowKeyword, 
			SyntaxKind.ToKeyword, 
			SyntaxKind.TrueKeyword, 
			SyntaxKind.TryKeyword, 
			SyntaxKind.TryCastKeyword, 
			SyntaxKind.TypeOfKeyword, 
			SyntaxKind.UIntegerKeyword, 
			SyntaxKind.ULongKeyword, 
			SyntaxKind.UShortKeyword, 
			SyntaxKind.UsingKeyword, 
			SyntaxKind.WhenKeyword, 
			SyntaxKind.WhileKeyword, 
			SyntaxKind.WideningKeyword, 
			SyntaxKind.WithKeyword, 
			SyntaxKind.WithEventsKeyword, 
			SyntaxKind.WriteOnlyKeyword, 
			SyntaxKind.XorKeyword, 
			SyntaxKind.EndIfKeyword, 
			SyntaxKind.GosubKeyword, 
			SyntaxKind.VariantKeyword, 
			SyntaxKind.WendKeyword
		};

		private static readonly SyntaxKind[] contextual_keywords = new SyntaxKind[]
		{
			SyntaxKind.AggregateKeyword, 
			SyntaxKind.AllKeyword, 
			SyntaxKind.AnsiKeyword, 
			SyntaxKind.AscendingKeyword, 
			SyntaxKind.AssemblyKeyword, 
			SyntaxKind.AutoKeyword, 
			SyntaxKind.BinaryKeyword, 
			SyntaxKind.ByKeyword, 
			SyntaxKind.CompareKeyword, 
			SyntaxKind.CustomKeyword, 
			SyntaxKind.DescendingKeyword, 
			SyntaxKind.DistinctKeyword, 
			SyntaxKind.EqualsKeyword, 
			SyntaxKind.ExplicitKeyword, 
			SyntaxKind.ExternalSourceKeyword, 
			SyntaxKind.ExternalChecksumKeyword, 
			SyntaxKind.FromKeyword, 
			SyntaxKind.GroupKeyword, 
			SyntaxKind.InferKeyword, 
			SyntaxKind.IntoKeyword, 
			SyntaxKind.IsFalseKeyword, 
			SyntaxKind.IsTrueKeyword, 
			SyntaxKind.JoinKeyword, 
			SyntaxKind.KeyKeyword, 
			SyntaxKind.MidKeyword, 
			SyntaxKind.OffKeyword, 
			SyntaxKind.OrderKeyword, 
			SyntaxKind.OutKeyword, 
			SyntaxKind.PreserveKeyword, 
			SyntaxKind.RegionKeyword, 
			SyntaxKind.SkipKeyword, 
			SyntaxKind.StrictKeyword, 
			SyntaxKind.TakeKeyword, 
			SyntaxKind.TextKeyword, 
			SyntaxKind.UnicodeKeyword, 
			SyntaxKind.UntilKeyword, 
			SyntaxKind.WhereKeyword, 
			SyntaxKind.TypeKeyword, 
			SyntaxKind.XmlKeyword
		};

		private static readonly SyntaxKind[] preprocessor_keywords = new SyntaxKind[]
		{
			SyntaxKind.IfKeyword, 
			SyntaxKind.ThenKeyword, 
			SyntaxKind.ElseIfKeyword, 
			SyntaxKind.ElseKeyword, 
			SyntaxKind.EndKeyword, 
			SyntaxKind.RegionKeyword, 
			SyntaxKind.ConstKeyword, 
			SyntaxKind.ExternalSourceKeyword, 
			SyntaxKind.ExternalChecksumKeyword
		};

		private static readonly SyntaxKind[] punctuation_tokens = new SyntaxKind[]
		{
			SyntaxKind.ExclamationToken, 
			SyntaxKind.AtToken, 
			SyntaxKind.CommaToken, 
			SyntaxKind.HashToken, 
			SyntaxKind.AmpersandToken, 
			SyntaxKind.SingleQuoteToken, 
			SyntaxKind.OpenParenToken, 
			SyntaxKind.CloseParenToken, 
			SyntaxKind.OpenBraceToken, 
			SyntaxKind.CloseBraceToken, 
			SyntaxKind.SemicolonToken, 
			SyntaxKind.AsteriskToken, 
			SyntaxKind.PlusToken, 
			SyntaxKind.MinusToken, 
			SyntaxKind.DotToken, 
			SyntaxKind.SlashToken, 
			SyntaxKind.ColonToken, 
			SyntaxKind.LessThanToken, 
			SyntaxKind.LessThanEqualsToken, 
			SyntaxKind.LessThanGreaterThanToken, 
			SyntaxKind.EqualsToken, 
			SyntaxKind.GreaterThanToken, 
			SyntaxKind.GreaterThanEqualsToken, 
			SyntaxKind.BackslashToken, 
			SyntaxKind.CaretToken, 
			SyntaxKind.ColonEqualsToken, 
			SyntaxKind.AmpersandEqualsToken, 
			SyntaxKind.AsteriskEqualsToken, 
			SyntaxKind.PlusEqualsToken, 
			SyntaxKind.MinusEqualsToken, 
			SyntaxKind.SlashEqualsToken, 
			SyntaxKind.BackslashEqualsToken, 
			SyntaxKind.CaretEqualsToken, 
			SyntaxKind.LessThanLessThanToken, 
			SyntaxKind.GreaterThanGreaterThanToken, 
			SyntaxKind.LessThanLessThanEqualsToken, 
			SyntaxKind.GreaterThanGreaterThanEqualsToken, 
			SyntaxKind.QuestionToken, 
			SyntaxKind.DoubleQuoteToken, 
			SyntaxKind.StatementTerminatorToken, 
			SyntaxKind.EndOfFileToken, 
			SyntaxKind.EmptyToken
		};
	}
}
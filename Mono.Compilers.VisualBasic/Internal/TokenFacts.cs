// 
// Copyright (c) 2011 Jonathan Pobst
//  
// Author:
//       Jonathan Pobst <monkey@jpobst.com>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Linq;
using Microsoft.VisualBasic;

namespace Mono.Compilers.VisualBasic
{
	class TokenFacts
	{
		private static char[] single_tokens = new char[] { '(', ')', '.', '=', ':' };
		private static char[] separators = new char[] { '(', ')', '{', '}', '!', '#', ',', '.', ':' };
		private static char[] operators = new char[] { '&', '*', '+', '-', '<', '>', '^', '/', '\\', '=', ':' };

		public static SyntaxKind GetKeywordOrIdentifierKind (string text)
		{
			var kind = SyntaxFacts.GetReservedKeywordKind (text);

			if (kind == SyntaxKind.None)
				return SyntaxKind.IdentifierToken;

			return kind;
		}

		public static SyntaxKind GetOneLetterNumberKind (char c)
		{
			switch (c) {
				case 'S':
				case 'I':
				case 'L':
					return SyntaxKind.IntegerLiteralToken;
				case 'D':
				case '@':
					return SyntaxKind.DecimalLiteralToken;
				case 'F':
				case 'R':
				case '!':
				case '#':
					return SyntaxKind.FloatingLiteralToken;
			}

			return SyntaxKind.None;
		}

		public static SyntaxKind GetOperatorKind (string text)
		{
			switch (text) {
				case ">>=":
					return SyntaxKind.GreaterThanGreaterThanEqualsToken;
				case "<<=":
					return SyntaxKind.LessThanLessThanEqualsToken;
				case ">=":
					return SyntaxKind.GreaterThanEqualsToken;
				case ">>":
					return SyntaxKind.GreaterThanGreaterThanToken;
				case "<>":
					return SyntaxKind.LessThanGreaterThanToken;
				case "<=":
					return SyntaxKind.LessThanEqualsToken;
				case "<<":
					return SyntaxKind.LessThanLessThanToken;
				case "*=":
					return SyntaxKind.AsteriskEqualsToken;
				case "+=":
					return SyntaxKind.PlusEqualsToken;
				case "-=":
					return SyntaxKind.MinusEqualsToken;
				case "^=":
					return SyntaxKind.CaretEqualsToken;
				case "\\=":
					return SyntaxKind.BackslashEqualsToken;
				case "/=":
					return SyntaxKind.SlashEqualsToken;
				case ":=":
					return SyntaxKind.ColonEqualsToken;
				case "&=":
					return SyntaxKind.AmpersandEqualsToken;
				case "</":
					return SyntaxKind.LessThanSlashToken;
				case ">":
					return SyntaxKind.GreaterThanToken;
				case "<":
					return SyntaxKind.LessThanToken;
				case "*":
					return SyntaxKind.AsteriskToken;
				case "+":
					return SyntaxKind.PlusToken;
				case "-":
					return SyntaxKind.MinusToken;
				case "^":
					return SyntaxKind.CaretToken;
				case "\\":
					return SyntaxKind.BackslashToken;
				case "/":
					return SyntaxKind.SlashToken;
				case ":":
					return SyntaxKind.ColonToken;
				case "&":
					return SyntaxKind.AmpersandToken;
			}

			return SyntaxKind.None;
		}

		public static SyntaxKind GetSingleLengthTokenKind (char c)
		{
			switch (c) {
				case ';':
					return SyntaxKind.SemicolonToken;
				case ':':
					return SyntaxKind.ColonToken;
				case ',':
					return SyntaxKind.CommaToken;
				case '(':
					return SyntaxKind.OpenParenToken;
				case ')':
					return SyntaxKind.CloseParenToken;
				case '{':
					return SyntaxKind.OpenBraceToken;
				case '}':
					return SyntaxKind.CloseBraceToken;
				case '=':
					return SyntaxKind.EqualsToken;
				case '!':
					return SyntaxKind.ExclamationToken;
				case '?':
					return SyntaxKind.QuestionToken;
				case '.':
					return SyntaxKind.DotToken;
				case '#':
					return SyntaxKind.HashToken;
				case '@':
					return SyntaxKind.AtToken;
				case ']':
					return SyntaxKind.BadToken;
			}

			return SyntaxKind.None;
		}

		public static SyntaxKind GetTwoLetterNumberKind (string text)
		{
			switch (text.ToUpperInvariant ()) {
				case "UL":
				case "UI":
				case "US":
					return SyntaxKind.IntegerLiteralToken;
			}

			return SyntaxKind.None;
		}

		public static TypeCharacter GetTypeCharacter (string text)
		{
			// Too short to have a type char
			if (text.Length < 2)
				return TypeCharacter.None;

			// Check for 2 character type character
			if (text.Length > 2) {
				var type = text.Substring (text.Length - 2);

				switch (type.ToUpperInvariant ()) {
					case "UL":
						return TypeCharacter.ULongLiteral;
					case "UI":
						return TypeCharacter.UIntegerLiteral;
					case "US":
						return TypeCharacter.UShortLiteral;
				}
			}

			// Check for 1 character type character
			var type1 = text.Substring (text.Length - 1);

			switch (type1.ToUpperInvariant ()) {
				case "S":
					return TypeCharacter.ShortLiteral;
				case "I":
					return TypeCharacter.IntegerLiteral;
				case "%":
					return TypeCharacter.Integer;
				case "&":
					return TypeCharacter.Long;
				case "L":
					return TypeCharacter.LongLiteral;
				case "F":
					return TypeCharacter.SingleLiteral;
				case "!":
					return TypeCharacter.Single;
				case "R":
					return TypeCharacter.DoubleLiteral;
				case "#":
					return TypeCharacter.Double;
				case "D":
					return TypeCharacter.DecimalLiteral;
				case "@":
					return TypeCharacter.Decimal;
			}

			return TypeCharacter.None;
		}

		public static bool IsCommentStartCharacter (char c)
		{
			switch (c) {
				case '\'':
				case '\u2018':
				case '\u2019':
					return true;
			}

			return false;
		}

		public static bool IsDoubleQuoteCharacter (char c)
		{
			return c == '"' || c == '\u201c' || c == '\u201d';
		}

		public static bool IsIdentifierAdditionalChar (char c)
		{
			switch (char.GetUnicodeCategory (c)) {
				case System.Globalization.UnicodeCategory.ConnectorPunctuation:
				case System.Globalization.UnicodeCategory.LowercaseLetter:
				case System.Globalization.UnicodeCategory.ModifierLetter:
				case System.Globalization.UnicodeCategory.OtherLetter:
				case System.Globalization.UnicodeCategory.TitlecaseLetter:
				case System.Globalization.UnicodeCategory.UppercaseLetter:
				case System.Globalization.UnicodeCategory.LetterNumber:
				case System.Globalization.UnicodeCategory.DecimalDigitNumber:
				case System.Globalization.UnicodeCategory.SpacingCombiningMark:
				case System.Globalization.UnicodeCategory.NonSpacingMark:
				case System.Globalization.UnicodeCategory.Format:
					return true;
			}

			return false;
		}

		public static bool IsIdentifierStartChar (char c)
		{
			return char.IsLetter (c) || char.GetUnicodeCategory (c) == System.Globalization.UnicodeCategory.ConnectorPunctuation;
		}

		public static bool IsLineTerminatorCharacter (char c)
		{
			return c == '\r' || c == '\n';
		}

		public static bool IsOperator (char text)
		{
			return operators.Contains (text);
		}

		public static bool IsSingleToken (char text)
		{
			return single_tokens.Contains (text);
		}

		public static bool IsSeparator (char text)
		{
			return separators.Contains (text);
		}

		public static bool IsTypeCharacter (char c)
		{
			switch (c) {
				case '%':
				case '&':
				case '@':
				case '!':
				case '#':
				case '$':
					return true;
			}

			return false;
		}
	}
}
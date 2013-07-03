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
using System.Text;

namespace Mono.Compilers.VisualBasic
{
	class TokenParser : BaseParser
	{
		public TokenParser (string text) : base (text)
		{
		}

		public IEnumerable<SyntaxToken> GetTokens ()
		{
			var seen_token = false;

			while (true) {
				// Consume any leading trivia
				var lead_trivia = ConsumeLeadingTrivia (!seen_token);

				// Consume token
				SyntaxToken token;
				start_ptr = curr;

				if (AtEOF ())
					token = Syntax.Token (SyntaxKind.EndOfFileToken, string.Empty);
				else if (AtQuote ())
					token = ConsumeStringLiteral ();
				else if (AtNumber (true))
					// This needs to be done before SingleLengthToken to check for period
					token = ConsumeNumber ();
				else if (AtSingleLengthToken ())
					token = ConsumeSingleLengthToken ();
				else if (AtOperator ())
					token = ConsumeOperator ();
				else if (AtBadToken ())
					token = ConsumeBadToken ();
				else if (AtEOL ())
					token = ConsumeEOL ();
				else
					token = ConsumeToken ();

				token.SetStart (start_ptr);
				token.leading_trivia = lead_trivia;
				seen_token = true;

				// Everything but EOF and EOL consume trailing trivia
				if (token.Kind != SyntaxKind.EndOfFileToken && token.Kind != SyntaxKind.StatementTerminatorToken)
					token.trailing_trivia = ConsumeTrailingTrivia ();

				yield return token;

				// If we hit EOF, terminate the iterator
				if (token.Kind == SyntaxKind.EndOfFileToken)
					yield break;
			}
		}

		#region Consume Functions
		private SyntaxToken ConsumeBadToken ()
		{
			var c = CurrentChar ();
			Advance ();

			return Syntax.Token (SyntaxKind.BadToken, c.ToString ());
		}

		private SyntaxToken ConsumeEOL ()
		{
			// Consume a CRLF, CR, or LF
			if (AtCRLF ()) {
				Advance (2);
				return Syntax.Token (SyntaxKind.StatementTerminatorToken, vbCrLf);
			} else {
				var c = CurrentChar ();
				Advance ();
				return Syntax.Token (SyntaxKind.StatementTerminatorToken, c.ToString ());
			}
		}

		private SyntaxTriviaList ConsumeLeadingTrivia (bool bof)
		{
			var list = Syntax.ParseLeadingTrivia (text, curr, bof);
			Advance (list.Length);
			return list;
		}

		private SyntaxToken ConsumeNumber ()
		{
			var kind = SyntaxKind.IntegerLiteralToken;
			var seen_decimal = false;

			// Look for numbers and up to 1 decimal point
			while (!AtEOF () && AtNumber (!seen_decimal)) {
				if (CurrentChar () == '.')
					seen_decimal = true;

				Advance ();

				// If we found a decimal, it's a float
				if (ConsumedText.Contains ("."))
					kind = SyntaxKind.FloatingLiteralToken;
			}

			// Look for a trailing 2 letter number specifier
			while (!AtEOF () && AtTwoLetterNumberSpecifier ()) {
				kind = TokenFacts.GetTwoLetterNumberKind (CurrentChar ().ToString () + NextChar.ToString ());
				Advance (2);
			}

			// Look for a trailing 1 letter number specifier
			while (!AtEOF () && AtOneLetterNumberSpecifier ()) {
				kind = TokenFacts.GetOneLetterNumberKind (CurrentChar ());
				Advance ();
			}

			return Syntax.Token (kind, ConsumedText);
		}

		private SyntaxToken ConsumeOperator ()
		{
			// Get the first operator character
			var full_oper = CurrentChar ().ToString ();
			Advance ();

			// Find the next 2 non-whitespace character
			var next = FindNextNonWhiteSpaceChar (curr);

			if (next > -1) {
				full_oper += text.Substring (curr, next - curr + 1);

				var nextnext = FindNextNonWhiteSpaceChar (next + 1);

				if (nextnext > -1)
					full_oper += text.Substring (next + 1, nextnext - next);
			}

			// Get the operator without whitespace
			var oper = StripWhiteSpace (full_oper);

			// Check for 3 character operator
			if (oper.Length == 3) {
				var kind = TokenFacts.GetOperatorKind (oper);

				if (kind != SyntaxKind.None) {
					Advance (full_oper.Length - 1);
					return Syntax.Token (kind, ConsumedText);
				}
			}

			// Check for 2 character operator
			if (oper.Length >= 2) {
				var kind = TokenFacts.GetOperatorKind (oper.Substring (0, 2));

				if (kind != SyntaxKind.None) {
					Advance (next - curr + 1);
					return Syntax.Token (kind, ConsumedText);
				}
			}

			// Check for 1 character operator
			var oper_kind = TokenFacts.GetOperatorKind (oper.Substring (0, 1));

			if (oper_kind != SyntaxKind.None)
				return Syntax.Token (oper_kind, ConsumedText);

			throw new ApplicationException ("unknown operator");
		}

		private SyntaxToken ConsumeSingleLengthToken ()
		{
			var c = CurrentChar ();
			Advance ();

			var kind = TokenFacts.GetSingleLengthTokenKind (c);

			if (kind == SyntaxKind.None)
				throw new ApplicationException ("unknown single length token");

			return Syntax.Token (kind, c.ToString ());
		}

		private SyntaxToken ConsumeStringLiteral ()
		{
			// Consume the open quote
			Advance ();

			// Consume everything until EOF or a close quote
			while (!AtEOF () && !AtEOL () && (!AtQuote () || AtQuoteQuote ())) {
				// Check for double (escaped) quotation mark ("")
				if (AtQuoteQuote ())
					Advance ();

				Advance ();
			}

			// Consume the close quote
			if (AtQuote ())
				Advance ();

			// Check if this is a character token instead of a string
			if ((curr - start_ptr == 3 && CurrentChar () == 'c')) {
				Advance ();
				return Syntax.Token (SyntaxKind.CharacterLiteralToken, ConsumedText);
			}

			return Syntax.Token (SyntaxKind.StringLiteralToken, ConsumedText);
		}

		private SyntaxToken ConsumeToken ()
		{
			var escaped = false;

			// If this is a single character token, like "=", we're done
			if (AtSingleToken ()) {
				Advance ();
				return Syntax.Token (TokenFacts.GetKeywordOrIdentifierKind (ConsumedText), ConsumedText);
			}

			// Check for identifier escape char
			if (CurrentChar () == '[') {
				escaped = true;
				Advance ();
			}

			// Check for identifier start char
			if (TokenFacts.IsIdentifierStartChar (CurrentChar ()))
				Advance ();
			else
				throw new ApplicationException ("bad identifier!");

			// Consume until we hit something that can't be in an identifier
			while (!AtEOF () && TokenFacts.IsIdentifierAdditionalChar (CurrentChar ()))
				Advance ();

			// See if we have a type specifier
			if (!AtEOF () && TokenFacts.IsTypeCharacter (CurrentChar ())) {
				// ! is only a type character if the next char can't start an identifier
				if (CurrentChar () != '!' || !TokenFacts.IsIdentifierStartChar (NextChar))
					Advance ();
			}

			// If we're escaping, look for the closing escape char
			if (escaped && !AtEOF () && CurrentChar () == ']')
				Advance ();

			return Syntax.Token (TokenFacts.GetKeywordOrIdentifierKind (ConsumedText), ConsumedText);
		}

		private SyntaxTriviaList ConsumeTrailingTrivia ()
		{
			var list = Syntax.ParseTrailingTriviaWithStartOffset (text, curr);
			Advance (list.Length);

			return list;
		}
		#endregion

		#region Text Functions
		private int FindNextNonWhiteSpaceChar (int start)
		{
			var ptr = start;

			while (ptr < total) {
				if (!char.IsWhiteSpace (text[ptr]))
					return ptr;

				ptr++;
			}

			return -1;
		}

		private string StripWhiteSpace (string text)
		{
			var sb = new StringBuilder ();

			foreach (var c in text)
				if (!char.IsWhiteSpace (c))
					sb.Append (c);

			return sb.ToString ();
		}
		#endregion
	}
}
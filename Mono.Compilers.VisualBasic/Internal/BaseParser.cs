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

using Microsoft.VisualBasic;

namespace Mono.Compilers.VisualBasic
{
	public abstract class BaseParser
	{
		public const string vbLf = Constants.vbLf;
		public const string vbCr = Constants.vbCr;
		public const string vbCrLf = Constants.vbCrLf;
		public const string vbTab = Constants.vbTab;

		protected string text;
		protected int total;
		protected int curr;
		protected int start_ptr;

		protected internal BaseParser (string text)
		{
			this.text = text;
			this.total = text.Length;
		}

		#region Text Functions
		protected void Advance (int count = 1)
		{
			curr += count;
		}

		protected string ConsumedText { get { return text.Substring (start_ptr, curr - start_ptr); } }

		protected char CurrentChar ()
		{
			return text[curr];
		}

		protected char NextChar {
			get {
				if (curr + 1 == total)
					return (char)0;

				return text[curr + 1];
			}
		}

		protected char PreviousChar {
			get {
				if (curr == 0)
					return (char)0;

				return text[curr - 1];
			}
		}
		#endregion

		// These functions check what the current character is
		#region At Functions
		protected bool AtBadToken ()
		{
			return CurrentChar () == '_' && !TokenFacts.IsIdentifierAdditionalChar (NextChar);
		}

		protected bool AtComment ()
		{
			return TokenFacts.IsCommentStartCharacter (CurrentChar ());
		}

		protected bool AtColon ()
		{
			return CurrentChar () == ':';
		}

		protected bool AtCR ()
		{
			return CurrentChar () == '\r';
		}

		protected bool AtCRLF ()
		{
			return CurrentChar () == '\r' && NextChar == '\n';
		}

		protected bool AtEOF ()
		{
			return curr == total;
		}

		protected bool AtEOL ()
		{
			return AtCR () || AtLF ();
		}

		protected bool AtLF ()
		{
			return CurrentChar () == '\n';
		}

		protected bool AtLineContinuation ()
		{
			return CurrentChar () == '_' && TokenFacts.IsLineTerminatorCharacter (NextChar);
		}

		protected bool AtNumber (bool allowDecimal)
		{
			// A number can either start with a number..
			if (char.IsNumber (CurrentChar ()))
				return true;

			// Or a decimal point and then a number
			return allowDecimal && CurrentChar () == '.' && char.IsNumber (NextChar);
		}

		protected bool AtOneLetterNumberSpecifier ()
		{
			switch (char.ToUpperInvariant (CurrentChar ())) {
				case 'S':
				case 'I':
				case 'L':
				case 'D':
				case 'F':
				case 'R':
				case '!':
				case '#':
				case '@':
					return true;
			}

			return false;
		}

		protected bool AtOperator ()
		{
			return TokenFacts.IsOperator (CurrentChar ());
		}

		protected bool AtQuote ()
		{
			return TokenFacts.IsDoubleQuoteCharacter (CurrentChar ());
		}

		protected bool AtQuoteQuote ()
		{
			if (!AtQuote ())
				return false;

			return TokenFacts.IsDoubleQuoteCharacter (NextChar);
		}

		protected bool AtSeparator ()
		{
			return TokenFacts.IsSeparator (CurrentChar ());
		}

		protected bool AtSingleToken ()
		{
			return TokenFacts.IsSingleToken (CurrentChar ());
		}

		protected bool AtTwoLetterNumberSpecifier ()
		{
			if (char.ToUpperInvariant (CurrentChar ()) != 'U')
				return false;

			switch (char.ToUpperInvariant (NextChar)) {
				case 'L':
				case 'I':
				case 'S':
					return true;
			}

			return false;
		}

		protected bool AtSingleLengthToken ()
		{
			switch (CurrentChar ()) {
				case ';':
				case ',':
				case '(':
				case ')':
				case '{':
				case '}':
				case '=':
				case '!':
				case '?':
				case '.':
				case '#':
				case ']':
				case '@':
					return true;
			}

			return false;
		}

		protected bool AtTrivia (bool includeContinuation = false, bool foundContinuation = false)
		{
			if (includeContinuation) {
				if (AtLineContinuation ())
					return true;

				// If we found a continuation, the EOL is considered trivia
				if (foundContinuation && AtEOL ())
					return true;
			}

			return char.IsWhiteSpace (CurrentChar ()) && (int)(CurrentChar ()) != 13 && (int)(CurrentChar ()) != 10;
		}

		protected bool AtWhitespace ()
		{
			return CurrentChar () == ' ' || CurrentChar () == '\t';
		}
		#endregion
	}
}

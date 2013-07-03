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

namespace Mono.Compilers.VisualBasic
{
	class TriviaParser : BaseParser
	{
		private bool _leading;
		private bool _bof;
		private int _start;

		internal TriviaParser (string text, int offset, bool leading, bool bof, int start) : base (text)
		{
			curr = offset;
			_leading = leading;
			_start = start;

			// Trivia at the beginning of a file consumes a
			// lot more stuff until it hits the first token
			_bof = bof;
		}

		public IEnumerable<SyntaxTrivia> GetTrivia ()
		{
			while (!AtEOF ()) {
				start_ptr = curr;
				SyntaxTrivia trivia;

				if (_bof && AtEOL ())
					trivia = ConsumeEndOfLine ();
				else if (AtComment ())
					trivia = ConsumeComment ();
				else if ((!_leading || _bof) && AtLineContinuation ())
					trivia = ConsumeLineContinuation ();
				else if (_bof && AtColon ())
					trivia = ConsumeColonTrivia ();
				else if (AtWhitespace ())
					trivia = ConsumeWhitespace ();
				else
					yield break;

				trivia.SetStart (start_ptr - _start);

				yield return trivia;
			}
		}

		#region Consume Functions
		private SyntaxTrivia ConsumeComment ()
		{
			// Comments go until they hit the end of a line
			while (!AtEOF () && !AtEOL ())
				Advance ();

			return Syntax.CommentTrivia (ConsumedText);
		}

		private SyntaxTrivia ConsumeEndOfLine ()
		{
			// End of Line only returns 1 EOL, even if several in a row
			if (AtCRLF ()) {
				Advance (2);
				return Syntax.EndOfLineTrivia (vbCrLf);
			}

			if (AtCR ()) {
				Advance ();
				return Syntax.EndOfLineTrivia (vbCr);
			}

			if (AtLF ()) {
				Advance ();
				return Syntax.EndOfLineTrivia (vbLf);
			}

			throw new ArgumentOutOfRangeException ("not eol");
		}

		private SyntaxTrivia ConsumeLineContinuation ()
		{
			// Line continuation consumes underscore and whitespace/EOL after it

			// Consume whitespace
			while (!AtEOL ())
				Advance ();

			// Consume EOL
			if (AtCRLF ())
				Advance (2);
			else
				Advance ();

			return Syntax.LineContinuationTrivia (ConsumedText);
		}

		private SyntaxTrivia ConsumeWhitespace ()
		{
			// Whitespace goes until we hit !whitespace
			while (!AtEOF () && AtWhitespace ())
				Advance ();

			return Syntax.WhitespaceTrivia (ConsumedText);
		}

		private SyntaxTrivia ConsumeColonTrivia ()
		{
			// Colons go until we hit !colon
			while (!AtEOF () && AtColon ())
				Advance ();

			return Syntax.ColonTrivia (ConsumedText);
		}
		#endregion
	}
}
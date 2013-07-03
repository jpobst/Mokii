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

namespace Mono.Compilers.VisualBasic
{
	public struct SyntaxTrivia
	{
		private SyntaxKind _kind;
		private string _text;
		private int _start;
		private SyntaxToken _token;

		#region Properties
		public bool ContainsDiagnostics { get { return false; } }
		public TextSpan FullSpan { get { return new TextSpan (_start, _text.Length); } }
		public bool HasStructure { get { return false; } }
		public bool IsDirective { get { return false; } }
		public SyntaxKind Kind { get { return _kind; } }
		public string Language { get { return "Visual Basic"; } }
		public TextSpan Span { get { return new TextSpan (_start, _text.Length); } }
		// public SyntaxTree SyntaxTree { get { return _token.SyntaxTree; } }
		public SyntaxToken Token { get { return _token; } }
		#endregion

		#region Constructors
		internal SyntaxTrivia (SyntaxKind kind, string text) : this ()
		{
			_kind = kind;
			_start = 0;
			_text = text;
		}
		#endregion

		#region Methods
		internal void SetStart (int start)
		{
			_start = start;
		}

		public string ToFullString ()
		{
			return _text;
		}

		public override string ToString ()
		{
			return _text;
		}
		#endregion
	}
}
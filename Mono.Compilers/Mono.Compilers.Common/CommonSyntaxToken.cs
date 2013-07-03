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

namespace Mono.Compilers.Common
{
	public struct CommonSyntaxToken
	{
		private int kind;
		private int start;
		private int length;
		private object value;

		public CommonSyntaxToken (int kind, int start, int length, object value)
		{
			this.kind = kind;
			this.start = start;
			this.length = length;
			this.value = value;
		}

		#region Properties
		public bool ContainsDiagnostics { get { return false; } }
		public bool ContainsDirectives { get { return false; } }
		public TextSpan FullSpan { get { return new TextSpan (start, length); } }
		public bool HasLeadingTrivia { get { return false; } }
		public bool HasTrailingTrivia { get { return false; } }
		public bool IsMissing { get { return false; } }
		public int Kind { get { return kind; } }
		public string Language { get { return ""; } }
		//public CommonSyntaxTriviaList LeadingTrivia { get { return null; } }
		public CommonSyntaxNode Parent { get { return null; } }
		public TextSpan Span { get { return new TextSpan (start, length); } }
		//public CommonSyntaxTree SyntaxTree { get { return null; } }
		//public CommonSyntaxTriviaList TrailingTrivia { get { return null; } }
		public object Value { get { return value; } }
		public string ValueText { get { return Value.ToString (); } }
		#endregion

		#region Protected Methods
		#endregion
	}
}

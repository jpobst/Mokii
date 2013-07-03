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
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Mono.Compilers.Common
{
	[DebuggerDisplay ("{DebuggerDisplay}")]
	public abstract class CommonSyntaxNode
	{
		protected CommonSyntaxNode ()
		{
		}

		#region Properties
		//public abstract bool ContainsAnnotations { get; }
		public abstract bool ContainsDiagnostics { get; }
		public abstract bool ContainsDirectives { get; }
		protected internal virtual string DebuggerDisplay { get { return string.Empty; } }
		public abstract TextSpan FullSpan { get; }
		public abstract bool HasLeadingTrivia { get; }
		public abstract bool HasTrailingTrivia { get; }
		public abstract bool IsMissing { get; }
		public abstract bool IsStructuredTrivia { get; }
		public int Kind { get { return 0; } }
		public abstract string Language { get; }
		public CommonSyntaxNode Parent { get; private set; }
		public abstract TextSpan Span { get; }
		#endregion

		#region Public Methods
		//public IEnumerable<CommonSyntaxNode> ChildNodes ()
		//{
		//	return ChildNodesAndTokensCore().List.Where (p => p.IsNode).Select (p => p.AsNode ());
		//}

		//public CommonChildSyntaxList ChildNodesAndTokens ()
		//{
		//	return ChildNodesAndTokensCore ();
		//}
		#endregion

		#region Protected Methods
		//protected abstract CommonChildSyntaxList ChildNodesAndTokensCore ();
		#endregion
	}
}

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
using System.Linq;
using Mono.Compilers.Common;
using Mono.Compilers;
using System.Diagnostics;

namespace Mono.Compilers.VisualBasic
{
	[DebuggerDisplay ("{ToDebugString()}")]
	public abstract class SyntaxNode : CommonSyntaxNode
	{
		protected ChildSyntaxList _list;
		private SyntaxKind _kind;
		private SyntaxNode _parent;

		protected SyntaxNode (SyntaxKind kind)
		{
			_kind = kind;
		}

		#region Properties
		//public override bool ContainsAnnotations { get { return _list.Any (p => p.ContainsAnnotations); } }
		public sealed override bool ContainsDiagnostics { get { return _list.Any (p => p.ContainsDiagnostics); } }
		public sealed override bool ContainsDirectives { get { return _list.Any (p => p.ContainsDirectives); } }
		public sealed override TextSpan FullSpan { get { return new TextSpan (GetFirstToken ().Span.Start, 0); } }
		public sealed override bool HasLeadingTrivia { get { return GetFirstToken ().HasLeadingTrivia; } }
		public sealed override bool HasTrailingTrivia { get { return GetLastToken ().HasTrailingTrivia; } }
		public bool IsDirective { get { return false; } }
		public sealed override bool IsMissing { get { return false; } }
		public sealed override bool IsStructuredTrivia { get { return false; } }
		public SyntaxKind Kind { get { return _kind; } }
		public override string Language { get { return "Visual Basic"; } }
		public SyntaxNode Parent { get { return _parent; } }
		public sealed override TextSpan Span { get { return new TextSpan (GetFirstToken ().Span.Start, 0); } }
		//public SyntaxTree SyntaxTree { get { return null; } }
		#endregion

		#region Public Methods
		public IEnumerable<SyntaxNode> ChildNodes ()
		{
			return _list.OfType<SyntaxNode> ();
		}

		public ChildSyntaxList ChildNodesAndTokens ()
		{
			return _list;
		}

		public SyntaxToken GetFirstToken (bool includeZeroWidthTokens = false)
		{
			foreach (var node_token in _list) {
				if (node_token.IsToken)
					return node_token.AsToken ();

				var child_token = node_token.AsNode ().GetFirstToken (includeZeroWidthTokens);

				if (child_token.Kind != SyntaxKind.None)
					return child_token;
			}

			return new SyntaxToken ();
		}

		public SyntaxToken GetLastToken (bool includeZeroWidthTokens = false)
		{
			foreach (var node_token in _list.Reverse ()) {
				if (node_token.IsToken)
					return node_token.AsToken ();

				var child_token = node_token.AsNode ().GetLastToken (includeZeroWidthTokens);

				if (child_token.Kind != SyntaxKind.None)
					return child_token;
			}

			return new SyntaxToken ();
		}

		public override string ToString ()
		{
			var sb = new StringBuilder ();

			foreach (var child in ChildNodesAndTokens ())
				sb.Append (child);

			return sb.ToString ();
		}
		#endregion

		#region Internal Methods
		internal void Add (SyntaxNodeOrToken syntax)
		{
			_list.List.Add (syntax);
		}

		internal void AddRange (SyntaxNodeOrTokenList syntax)
		{
			_list.List.AddRange (syntax.list);
		}

		private string ToDebugString ()
		{
			return string.Format ("{0} {1} {2}", GetType ().Name, Kind, ToString ());
		}
		#endregion
	}
}
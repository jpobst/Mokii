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
using Mono.Compilers;
using Mono.Compilers.Common;

namespace Mono.Compilers.VisualBasic
{
	public class SyntaxNodeOrToken
	{
		private SyntaxNode _node;
		private SyntaxToken _token;

		#region Constructors
		internal SyntaxNodeOrToken (SyntaxNode node)
		{
			_node = node;
		}

		internal SyntaxNodeOrToken (SyntaxToken token)
		{
			_token = token;
		}
		#endregion

		#region Properties
		//public bool ContainsAnnotations { get { return IsNode ? _node.HasAnnotations : _token.ContainsAnnotations; } }
		public bool ContainsDiagnostics { get { return IsNode ? _node.ContainsDiagnostics : _token.ContainsDiagnostics; } }
		public bool ContainsDirectives { get { return IsNode ? _node.ContainsDirectives : _token.ContainsDirectives; } }
		public TextSpan FullSpan { get { return IsNode ? _node.FullSpan : _token.FullSpan; } }
		public bool HasLeadingTrivia { get { return IsNode ? _node.HasLeadingTrivia : _token.HasLeadingTrivia; } }
		public bool HasTrailingTrivia { get { return IsNode ? _node.HasTrailingTrivia : _token.HasTrailingTrivia; } }
		public bool IsMissing { get { return IsNode ? _node.IsMissing : _token.IsMissing; } }
		public bool IsNode { get { return _node != null; } }
		public bool IsToken { get { return _node == null; } }
		public SyntaxKind Kind { get { return IsNode ? _node.Kind : _token.Kind; } }
		//public string Language { get { return IsNode ? _node.Language : _token.Language; } }
		//public SyntaxNode Parent { get { return IsNode ? _node.Parent : _token.Parent; } }
		public TextSpan Span { get { return IsNode ? _node.Span : _token.Span; } }
		//public SyntaxTree SyntaxTree { get { return IsNode ? _node.SyntaxTree : _token.SyntaxTree; } }
		#endregion

		#region Public Methods
		public SyntaxNode AsNode ()
		{
			return _node;
		}

		public SyntaxToken AsToken ()
		{
			return _token;
		}

		//public ChildSyntaxList ChildNodesAndTokens ()
		//{
		//	if (IsNode)
		//		return AsNode ().ChildNodesAndTokens ();
		//	else
		//		return new ChildSyntaxList ();
		//}

		//public string ToFullString ()
		//{
		//	return IsNode ? _node.ToFullString () : _token.ToFullString ();
		//}

		public override string ToString ()
		{
			return IsNode ? _node.ToString () : _token.ToString ();
		}
		#endregion

		#region Operators
		public static implicit operator CommonSyntaxNodeOrToken (SyntaxNodeOrToken nodeOrToken)
		{
			if (nodeOrToken.IsNode) {
				return new CommonSyntaxNodeOrToken (nodeOrToken.AsNode ());
			}

			return new CommonSyntaxNodeOrToken (nodeOrToken.AsToken ());
		}

		public static implicit operator SyntaxNodeOrToken (SyntaxToken token)
		{
			return new SyntaxNodeOrToken (token);
		}

		public static implicit operator SyntaxNodeOrToken (SyntaxNode node)
		{
			return new SyntaxNodeOrToken (node);
		}
		#endregion
	}
}
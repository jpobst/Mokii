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
	public struct CommonSyntaxNodeOrToken
	{
		private CommonSyntaxNode _node;
		private CommonSyntaxToken _token;

		public CommonSyntaxNodeOrToken (CommonSyntaxNode node) : this ()
		{
			_node = node;
		}

		public CommonSyntaxNodeOrToken (CommonSyntaxToken token) : this ()
		{
			_token = token;
		}

		#region Properties
		//public TextSpan FullSpan { get { return _node.FullSpan; } }
		//public int FullWidth { get { return _node.FullWidth; } }
		//public bool HasAnnotations { get { return _node.HasAnnotations; } }
		//public bool HasChildren { get { return _node.HasChildren; } }
		//public bool HasDiagnostics { get { return _node.HasDiagnostics; } }
		//public bool HasDirectives { get { return _node.HasDirectives; } }
		//public bool HasLeadingTrivia { get { return _node.HasLeadingTrivia; } }
		//public bool HasStructuredTrivia { get { return _node.HasStructuredTrivia; } }
		//public bool HasTrailingTrivia { get { return _node.HasTrailingTrivia; } }
		//public bool IsMissing { get { return _node.IsMissing; } }
		//public bool IsNode { get { return _node.IsNode; } }
		//public bool IsToken { get { return _node.IsToken; } }
		//public int Kind { get { return _node.Kind; } }
		//public CommonSyntaxNode Parent { get { return _node.Parent; } }
		//public TextSpan Span { get { return _node.Span; } }
		//public int Width { get { return _node.Width; } }
		#endregion

		#region Public Methods
		public CommonSyntaxNode AsNode ()
		{
			return (CommonSyntaxNode)_node;
		}

		public CommonSyntaxToken AsToken ()
		{
			return (CommonSyntaxToken)_token;
		}
		#endregion

		#region Operators
		public static explicit operator CommonSyntaxNode (CommonSyntaxNodeOrToken node)
		{
			return node.AsNode ();
		}

		public static explicit operator CommonSyntaxToken (CommonSyntaxNodeOrToken node)
		{
			return node.AsToken ();
		}

		public static implicit operator CommonSyntaxNodeOrToken (CommonSyntaxNode node)
		{
			return new CommonSyntaxNodeOrToken (node);
		}

		public static implicit operator CommonSyntaxNodeOrToken (CommonSyntaxToken token)
		{
			return new CommonSyntaxNodeOrToken (token);
		}
		#endregion
	}
}

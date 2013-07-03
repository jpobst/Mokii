// 
// Copyright (c) 2013 Jonathan Pobst
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

namespace Mono.Compilers.VisualBasic
{
	[DebuggerDisplay ("{ToDebugString ()}")]
	public struct SeparatedSyntaxList<TNode> where TNode : SyntaxNode
	{
		private List<TNode> _nodes;
		private List<SyntaxToken> _separators;

		public int Count { get { return Nodes.Count; } }
		public int SeparatorCount { get { return Separators.Count; } }
		public TNode this[int index] { get { return Nodes[index]; } }

		internal void Add (TNode node)
		{
			Nodes.Add (node);
		}

		internal void Add (SyntaxToken separator)
		{
			Separators.Add (separator);
		}

		#region Public Methods
		public SyntaxNodeOrTokenList GetWithSeparators ()
		{
			var list = new List<SyntaxNodeOrToken> ();

			for (var i = 0; i < _nodes.Count; i++) {
				list.Add (_nodes[i]);

				if (_separators != null && i < _separators.Count)
					list.Add (_separators[i]);
			}

			return new SyntaxNodeOrTokenList (list);
		}

		public TNode Last ()
		{
			return Nodes.Last ();
		}
		#endregion

		#region Internal Methods
		internal TNode GetAndRemoveLastSyntax ()
		{
			var node = Nodes.Last ();
			Nodes.RemoveAt (Nodes.Count - 1);

			return node;
		}

		internal SyntaxToken GetAndRemoveLastSeparator ()
		{
			var sep = Separators.Last ();
			Separators.RemoveAt (Separators.Count - 1);

			return sep;
		}

		private object ToDebugString ()
		{
			var strings = Nodes.Select (p => p.ToString ());

			return string.Join (", ", strings);
		}
		#endregion

		public System.Collections.Generic.IEnumerator<SyntaxNodeOrToken> GetEnumerator ()
		{
			for (var i = 0; i < Nodes.Count - 2; i++) {
				yield return Nodes[i];
				yield return Separators[i];
			}

			yield return Nodes[Nodes.Count - 1];
		}

		private List<TNode> Nodes
		{
			get
			{
				if (_nodes == null) {
					_nodes = new List<TNode> ();
				}

				return _nodes;
			}
		}

		private List<SyntaxToken> Separators
		{
			get
			{
				if (_separators == null) {
					_separators = new List<SyntaxToken> ();
				}

				return _separators;
			}
		}

		public struct Enumerator : IEnumerator<TNode>
		{
			private IEnumerator<SyntaxNode> _enumerator;
			internal Enumerator (List<SyntaxNode> nodes)
			{
				_enumerator = nodes.GetEnumerator ();
			}

			public TNode Current
			{
				get
				{
					return (TNode)_enumerator.Current;
				}
			}

			object Current1
			{
				get
				{
					return Current;
				}
			}

			public bool MoveNext ()
			{
				return _enumerator.MoveNext ();
			}

			public void Reset ()
			{
				_enumerator.Reset ();
			}

			/* #Region "IDisposable Support"
	 */
			// This code added by Visual Basic to correctly implement the disposable pattern.
			void Dispose ()
			{
				_enumerator.Dispose ();
			}

			#region IDisposable Members

			void System.IDisposable.Dispose ()
			{
				throw new System.NotImplementedException ();
			}

			#endregion

			#region IEnumerator Members

			object System.Collections.IEnumerator.Current
			{
				get { throw new System.NotImplementedException (); }
			}

			#endregion
		}
	}
}
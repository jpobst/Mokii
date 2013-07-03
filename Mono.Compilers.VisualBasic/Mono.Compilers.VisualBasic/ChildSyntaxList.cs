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
	public struct ChildSyntaxList : IEnumerable<SyntaxNodeOrToken>
	{
		private List<SyntaxNodeOrToken> _list;

		#region Properties
		public int Count { get { if (_list == null) return 0; return _list.Count; } }
		public SyntaxNodeOrToken this[int index] { get { return List[index]; } }
		#endregion

		public ChildSyntaxList.Enumerator GetEnumerator ()
		{
			return new ChildSyntaxList.Enumerator (List);
		}

		#region IEnumerable<SyntaxTrivia> Members
		IEnumerator<SyntaxNodeOrToken> IEnumerable<SyntaxNodeOrToken>.GetEnumerator ()
		{
			return new ChildSyntaxList.Enumerator (List);
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return new ChildSyntaxList.Enumerator (List);
		}
		#endregion

		internal List<SyntaxNodeOrToken> List {
			get {
				if (_list == null)
					_list = new List<SyntaxNodeOrToken> ();

				return _list;
			}
		}

		public struct Enumerator : IEnumerator<SyntaxNodeOrToken>
		{
			private IEnumerator<SyntaxNodeOrToken> enumerator;

			internal Enumerator (IEnumerable<SyntaxNodeOrToken> list)
			{
				this.enumerator = list.GetEnumerator ();
			}

			public SyntaxNodeOrToken Current { get { return enumerator.Current; } }

			public bool MoveNext ()
			{
				return enumerator.MoveNext ();
			}

			public void Reset ()
			{
				enumerator.Reset ();
			}

			#region IEnumerator<SyntaxNodeOrToken> Members
			SyntaxNodeOrToken IEnumerator<SyntaxNodeOrToken>.Current
			{
				get { return enumerator.Current; }
			}
			#endregion

			#region IDisposable Members
			void IDisposable.Dispose ()
			{
				enumerator.Dispose ();
			}
			#endregion

			#region IEnumerator Members
			object System.Collections.IEnumerator.Current
			{
				get { return enumerator.Current; }
			}

			bool System.Collections.IEnumerator.MoveNext ()
			{
				return enumerator.MoveNext ();
			}

			void System.Collections.IEnumerator.Reset ()
			{
				enumerator.Reset ();
			}
			#endregion
		}
	}
}
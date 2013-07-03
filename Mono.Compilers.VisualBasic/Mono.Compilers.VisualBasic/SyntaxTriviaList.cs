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
using System.Linq;

namespace Mono.Compilers.VisualBasic
{
	public struct SyntaxTriviaList : IEnumerable<SyntaxTrivia>
	{
		private List<SyntaxTrivia> list;

		public static readonly SyntaxTriviaList Empty = new SyntaxTriviaList (null);

		private SyntaxTriviaList (object dummy)
		{
			list = new List<SyntaxTrivia> ();
		}

		internal void Initialize (SyntaxTrivia trivia)
		{
			list = new List<SyntaxTrivia> ();
			list.Add (trivia);
		}

		internal SyntaxTriviaList (IEnumerable<SyntaxTrivia> trivias)
		{
			list = new List<SyntaxTrivia> ();

			if (trivias != null)
				list.AddRange (trivias);
		}

		#region Properties
		public bool ContainsDiagnostics {
			get { return Count > 0 && list.Any (p => p.ContainsDiagnostics); }
		}

		public int Count {
			get { 
				if (list == null)
					return 0;

				return list.Count;
			}
		}

		public SyntaxTrivia this[int index] { get { return list[index]; } }
		#endregion

		#region Methods
		public SyntaxTrivia First ()
		{
			return list.First ();
		}

		public SyntaxTriviaList.Enumerator GetEnumerator ()
		{
			if (list == null)
				list = new List<SyntaxTrivia> ();

			return new SyntaxTriviaList.Enumerator (list);
		}
		#endregion

		#region Internal Methods
		internal int Length {
			get {
				if (list == null)
					return 0;

				return list.Sum (p => p.FullSpan.Length);
			}
		}
		#endregion

		public struct Enumerator : IEnumerator<SyntaxTrivia>
		{
			private IEnumerator<SyntaxTrivia> enumerator;

			internal Enumerator (IEnumerable<SyntaxTrivia> list)
			{
				this.enumerator = list.GetEnumerator ();
			}

			public SyntaxTrivia Current { get { return enumerator.Current; } }
			object System.Collections.IEnumerator.Current { get { return enumerator.Current; } }

			public bool MoveNext ()
			{
				return enumerator.MoveNext ();
			}

			public void Reset ()
			{
				enumerator.Reset ();
			}

			#region IDisposable Members
			public void Dispose ()
			{
				enumerator.Dispose ();
			}
			#endregion
		}

		#region IEnumerable<SyntaxTrivia> Members

		IEnumerator<SyntaxTrivia> IEnumerable<SyntaxTrivia>.GetEnumerator ()
		{
			return GetEnumerator ();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}

		#endregion
	}
}
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
	public struct SyntaxTokenList
	{
		internal List<SyntaxToken> list;

		internal SyntaxTokenList (IEnumerable<SyntaxToken> list)
		{
			this.list = list.ToList ();
		}

		#region Properties
		public int Count { get { return list.Count; } }
		public SyntaxToken this[int index] { get { return list[index]; } }
		#endregion

		public SyntaxTokenList.Enumerator GetEnumerator ()
		{
			return new SyntaxTokenList.Enumerator (list);
		}

		public struct Enumerator
		{
			private IEnumerator<SyntaxToken> enumerator;

			internal Enumerator (IEnumerable<SyntaxToken> list)
			{
				this.enumerator = list.GetEnumerator ();
			}

			public SyntaxToken Current { get { return enumerator.Current; } }

			public bool MoveNext ()
			{
				return enumerator.MoveNext ();
			}
		}
	}
}
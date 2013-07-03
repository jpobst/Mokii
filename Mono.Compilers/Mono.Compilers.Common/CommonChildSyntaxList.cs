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

namespace Mono.Compilers.Common
{
	public struct CommonChildSyntaxList
	{
		private List<CommonSyntaxNodeOrToken> list;

		public CommonChildSyntaxList (List<CommonSyntaxNodeOrToken> list)
		{
			this.list = list;
		}

		public Enumerator GetEnumerator ()
		{
			return new Enumerator (List);
		}

		public int Count { get { return List.Count; } }
		public CommonSyntaxNodeOrToken this[int index] { get { return List[index]; } }

		private List<CommonSyntaxNodeOrToken> List {
			get {
				if (list == null) list = new List<CommonSyntaxNodeOrToken> ();
					return list;
			}
		}

		public struct Enumerator : IEnumerator<CommonSyntaxNodeOrToken>
		{
			private IEnumerator<CommonSyntaxNodeOrToken> enumerator;

			public Enumerator (IEnumerable<CommonSyntaxNodeOrToken> list)
			{
				this.enumerator = list.GetEnumerator ();
			}

			#region IEnumerator<CommonSyntaxNode> Members
			public CommonSyntaxNodeOrToken Current {
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
			object System.Collections.IEnumerator.Current {
				get { return enumerator.Current; }
			}

			public bool MoveNext ()
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

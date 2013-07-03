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
using System.Text;
using System.Linq;
using Mono.Compilers.Common;
using Mono.Compilers;
using System.Diagnostics;

namespace Mono.Compilers.VisualBasic
{
	[DebuggerDisplay ("SyntaxToken {Kind} {Value}")]
	public struct SyntaxToken
	{
		private int _start;
		private SyntaxKind _kind;
		private bool _bracketed;
		private string _value;

		internal SyntaxTriviaList leading_trivia;
		internal SyntaxTriviaList trailing_trivia;

		#region Properties
		public LiteralBase? Base {
			get {
				if (Kind == SyntaxKind.IntegerLiteralToken)
					return LiteralBase.Decimal;

				return null;
			}
		}

		public bool ContainsDiagnostics { get { return false; } }
		public bool ContainsDirectives { get { return false; } }

		public TextSpan FullSpan {
			get { return new TextSpan (Span.Start - leading_trivia.Length, Span.Length + leading_trivia.Length + trailing_trivia.Length); }
		}

		public bool HasLeadingTrivia { get { return leading_trivia.Count > 0; } }
		public bool HasTrailingTrivia { get { return trailing_trivia.Count > 0; } }
		public bool IsBracketed { get { return _bracketed; } }
		public bool IsMissing { get; internal set; }
		public SyntaxKind Kind { get { return _kind; } }
		public string Language { get { return "Visual Basic"; } }
		public SyntaxTriviaList LeadingTrivia { get { return leading_trivia; } }
		// public SyntaxNode Parent { get { return null; } }
		public TextSpan Span { get { return new TextSpan (_start, _value.Length); } }
		// public SyntaxTree SyntaxTree { get { return null; } }
		public SyntaxTriviaList TrailingTrivia { get { return trailing_trivia; } }
		
		public TypeCharacter TypeCharacter {
			get {
				switch (Kind) {
					case SyntaxKind.IntegerLiteralToken:
					case SyntaxKind.DecimalLiteralToken:
					case SyntaxKind.FloatingLiteralToken:
						return TokenFacts.GetTypeCharacter (_value);
				}

				return VisualBasic.TypeCharacter.None;
			}
		}

		public object Value {
			get {
				if (Kind == SyntaxKind.NothingKeyword)
					return null;
				if (Kind == SyntaxKind.StringLiteralToken)
					return TrimString (_value).Replace ("\"\"", "\"");
				if (IsBracketed)
					return _value.Substring (1, _value.Length - 2);
				if (Kind == SyntaxKind.CharacterLiteralToken)
					return char.Parse (_value.Substring (1, _value.Length - 3));
				if (Kind == SyntaxKind.FalseKeyword)
					return false;
				if (Kind == SyntaxKind.TrueKeyword)
					return true;

				switch (TypeCharacter) {
					case TypeCharacter.Decimal:
					case TypeCharacter.DecimalLiteral:
						return decimal.Parse (_value.Substring (0, _value.Length - 1));
					case TypeCharacter.Double:
					case TypeCharacter.DoubleLiteral:
						return double.Parse (_value.Substring (0, _value.Length - 1));
					case TypeCharacter.Integer:
					case TypeCharacter.IntegerLiteral:
						return int.Parse (_value.Substring (0, _value.Length - 1));
					case TypeCharacter.Long:
					case TypeCharacter.LongLiteral:
						return long.Parse (_value.Substring (0, _value.Length - 1));
					case TypeCharacter.ShortLiteral:
						return short.Parse (_value.Substring (0, _value.Length - 1));
					case TypeCharacter.Single:
					case TypeCharacter.SingleLiteral:
						return float.Parse (_value.Substring (0, _value.Length - 1));
					case TypeCharacter.UIntegerLiteral:
						return uint.Parse (_value.Substring (0, _value.Length - 2));
					case TypeCharacter.ULongLiteral:
						return ulong.Parse (_value.Substring (0, _value.Length - 2));
					case TypeCharacter.UShortLiteral:
						return ushort.Parse (_value.Substring (0, _value.Length - 2));
				}

				if (Kind == SyntaxKind.FloatingLiteralToken)
					return double.Parse (_value);

				if (Kind == SyntaxKind.IntegerLiteralToken) {
					int i = 0;

					if (int.TryParse (_value, out i))
						return i;

					return long.Parse (_value);
				}

				return _value;
			}
		}
		
		public string ValueText {
			get {
				if (Value == null)
					return "Nothing";

				if (Kind == SyntaxKind.DecimalLiteralToken)
					return _value;

				if (Kind == SyntaxKind.CharacterLiteralToken)
					return _value;
				if (Kind == SyntaxKind.FalseKeyword)
					return _value;
				if (Kind == SyntaxKind.TrueKeyword)
					return _value;

				return Value.ToString ();
				if (Kind == SyntaxKind.StringLiteralToken)
					return _value.Substring (1, _value.Length - 2);
				if (IsBracketed)
					return _value.Substring (1, _value.Length - 2);

				switch (TypeCharacter) {
					case TypeCharacter.UIntegerLiteral:
					case TypeCharacter.ULongLiteral:
					case TypeCharacter.UShortLiteral:
						return _value.Substring (0, _value.Length - 2);
					case TypeCharacter.None:
					case TypeCharacter.DecimalLiteral:
						return _value;
					default:
						return _value.Substring (0, _value.Length - 1);
				}

				return _value;
			}
		}
		#endregion

		#region Constructors
		//private SyntaxToken ()
		//{
		//	_start = 0;
		//	_bracketed = false;

		//	_kind = SyntaxKind.None;
		//}

		internal SyntaxToken (SyntaxTriviaList leading, SyntaxKind kind, SyntaxTriviaList trailing, string text) : this ()
		{
			_kind = kind;
			leading_trivia = leading;
			trailing_trivia = trailing;

			if (text == null)
				return;

			// See if our text is bracketed
			if (text.StartsWith ("[") && text.EndsWith ("]"))
				_bracketed = true;

			_value = text;
		}
		#endregion

		#region Public Methods
		public string ToFullString ()
		{
			var sb = new StringBuilder ();

			foreach (var lt in LeadingTrivia)
				sb.Append (lt);

			sb.Append (_value);

			foreach (var tt in TrailingTrivia)
				sb.Append (tt);

			return sb.ToString ();
		}

		public override string ToString ()
		{
			return _value;
		}
		#endregion

		#region Operators
		public static implicit operator CommonSyntaxNodeOrToken (SyntaxToken token)
		{
			return new CommonSyntaxNodeOrToken (token);
		}

		public static implicit operator CommonSyntaxToken (SyntaxToken token)
		{
			return new CommonSyntaxToken ((int)token.Kind, token._start, token._value.Length, token.Value);
		}
		#endregion

		internal SyntaxToken SetStart (int start)
		{
			_start = start;
			return this;
		}

		private string TrimString (string text)
		{
			string retval = text;

			if (IsQuote (retval[0]))
				retval = retval.Substring (1);

			if (retval.Length > 0 && IsQuote (retval[retval.Length - 1]))
				retval = retval.Substring (0, retval.Length - 1);

			return retval;
		}

		private bool IsQuote (char c)
		{
			switch ((int)c) {
				case 147:
				case 148:
				case 34:
				case 8220:
				case 8221:
					return true;
			}

			return false;
		}
	}
}
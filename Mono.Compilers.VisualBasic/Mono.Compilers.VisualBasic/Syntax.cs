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
	public class Syntax
	{
		#region Fields
		public static readonly SyntaxTrivia CarriageReturn = new SyntaxTrivia (SyntaxKind.EndOfLineTrivia, "\r");
		public static readonly SyntaxTrivia CarriageReturnLineFeed = new SyntaxTrivia (SyntaxKind.EndOfLineTrivia, "\r\n");
		public static readonly SyntaxTrivia ElasticCarriageReturn = new SyntaxTrivia (SyntaxKind.EndOfLineTrivia, "\r");
		public static readonly SyntaxTrivia ElasticCarriageReturnLineFeed = new SyntaxTrivia (SyntaxKind.EndOfLineTrivia, "\r\n");
		public static readonly SyntaxTrivia ElasticLineFeed = new SyntaxTrivia (SyntaxKind.EndOfLineTrivia, "\n");
		public static readonly SyntaxTrivia ElasticMarker = new SyntaxTrivia (SyntaxKind.WhitespaceTrivia, "");
		public static readonly SyntaxTrivia ElasticSpace = new SyntaxTrivia (SyntaxKind.WhitespaceTrivia, " ");
		public static readonly SyntaxTrivia ElasticTab = new SyntaxTrivia (SyntaxKind.WhitespaceTrivia, "\t");
		public static readonly SyntaxTrivia LineFeed = new SyntaxTrivia (SyntaxKind.EndOfLineTrivia, "\n");
		public static readonly SyntaxTrivia Space = new SyntaxTrivia (SyntaxKind.WhitespaceTrivia, " ");
		public static readonly SyntaxTrivia Tab = new SyntaxTrivia (SyntaxKind.WhitespaceTrivia, "\t");
		#endregion

		public static SyntaxTrivia CommentTrivia (string text)
		{
			return new SyntaxTrivia (SyntaxKind.CommentTrivia, text);
		}

		public static SyntaxTrivia ColonTrivia (string text)
		{
			return new SyntaxTrivia (SyntaxKind.ColonTrivia, text);
		}

		public static SyntaxTrivia EndOfLineTrivia (string text)
		{
			return new SyntaxTrivia (SyntaxKind.EndOfLineTrivia, text);
		}

		public static GenericNameSyntax GenericName (SyntaxToken identifier, TypeArgumentListSyntax typeArgumentList)
		{
			return new GenericNameSyntax (identifier, typeArgumentList);
		}

		public static GlobalNameSyntax GlobalName (SyntaxToken globalKeyword)
		{
			return new GlobalNameSyntax (globalKeyword);
		}

		public static IdentifierNameSyntax IdentifierName (SyntaxToken identifier)
		{
			return new IdentifierNameSyntax (identifier);
		}

		public static SyntaxTrivia LineContinuationTrivia (string text)
		{
			return new SyntaxTrivia (SyntaxKind.LineContinuationTrivia, text);
		}

		public static SyntaxToken MissingToken (SyntaxKind kind)
		{
			var token = Token (SyntaxTriviaList.Empty, kind, SyntaxTriviaList.Empty, string.Empty);
			token.IsMissing = true;

			return token;
		}

		public static NameSyntax ParseName (string text, int offset = 0)
		{
			var tokens = ParseTokens (text, offset);
			return NameParser.Parse (tokens);
			var first_token = tokens.First ();

			if (first_token.Kind == SyntaxKind.IdentifierToken && first_token.IsBracketed)
				return IdentifierName (first_token);

			var token_string = first_token.ToString ();

			if (token_string.Length > 0 && TokenFacts.IsIdentifierStartChar (token_string[0])) {
				if (token_string[0] != '_')			
					return IdentifierName (first_token);

				// If the first character is an underscore, we have to have another character after it
				if (token_string.Length > 1 && TokenFacts.IsIdentifierAdditionalChar (token_string[1]))
					return IdentifierName (first_token);
			}

			return IdentifierName (Token (SyntaxKind.IdentifierToken, ""));
		}

		public static SyntaxTriviaList ParseLeadingTrivia (string text, int offset = 0)
		{
			return TriviaList (new TriviaParser (text, offset, true, true, offset).GetTrivia ());
		}

		internal static SyntaxTriviaList ParseLeadingTrivia (string text, int offset = 0, bool bof = true)
		{
			return TriviaList (new TriviaParser (text, offset, true, bof, 0).GetTrivia ());
		}

		public static IEnumerable<SyntaxToken> ParseTokens (string text, int offset = 0)
		{
			return new TokenParser (text.Substring (offset)).GetTokens ();
		}

		public static SyntaxTriviaList ParseTrailingTrivia (string text, int offset = 0)
		{
			return TriviaList (new TriviaParser (text, offset, false, false, offset).GetTrivia ());
		}

		internal static SyntaxTriviaList ParseTrailingTriviaWithStartOffset (string text, int offset = 0)
		{
			return TriviaList (new TriviaParser (text, offset, false, false, 0).GetTrivia ());
		}

		public static QualifiedNameSyntax QualifiedName (NameSyntax left, SyntaxToken dotToken, SimpleNameSyntax right)
		{
			return new QualifiedNameSyntax (left, dotToken, right);
		}

		public static SyntaxToken StringLiteralToken (string text, string value, SyntaxTriviaList leadingTrivia, SyntaxTriviaList trailingTrivia)
		{
			return Token (leadingTrivia, SyntaxKind.StringLiteralToken, trailingTrivia, text);
		}

		public static SyntaxTrivia SyntaxTrivia (SyntaxKind kind, string text)
		{
			return new SyntaxTrivia (kind, text);
		}

		public static SyntaxToken Token (SyntaxTriviaList leading, SyntaxKind kind, SyntaxTriviaList trailing, string text = null)
		{
			return new SyntaxToken (leading, kind, trailing, text);
		}

		public static SyntaxToken Token (SyntaxTriviaList leading, SyntaxKind kind, string text = null)
		{
			return Token (leading, kind, SyntaxTriviaList.Empty, text);
		}

		public static SyntaxToken Token (SyntaxKind kind, SyntaxTriviaList trailing, string text = null)
		{
			return Token (SyntaxTriviaList.Empty, kind, trailing, text);
		}

		public static SyntaxToken Token (SyntaxKind kind, string text = null)
		{
			return Token (SyntaxTriviaList.Empty, kind, SyntaxTriviaList.Empty, text);
		}

		public static SyntaxTokenList TokenList (IEnumerable<SyntaxToken> tokens)
		{
			return new SyntaxTokenList (tokens);
		}

		public static SyntaxTokenList TokenList (params SyntaxToken[] tokens)
		{
			return new SyntaxTokenList (tokens);
		}

		public static SyntaxTokenList TokenList (SyntaxToken token)
		{
			return new SyntaxTokenList (new SyntaxToken[] { token });
		}

		public static SyntaxTokenList TokenList ()
		{
			return new SyntaxTokenList ();
		}

		public static SyntaxTriviaList TriviaList (IEnumerable<SyntaxTrivia> trivias)
		{
			return new SyntaxTriviaList (trivias);
		}

		public static SyntaxTriviaList TriviaList (params SyntaxTrivia[] trivias)
		{
			return new SyntaxTriviaList (trivias);
		}

		public static SyntaxTriviaList TriviaList (SyntaxTrivia trivia)
		{
			return new SyntaxTriviaList (new SyntaxTrivia[] { trivia });
		}

		public static SyntaxTriviaList TriviaList ()
		{
			return new SyntaxTriviaList ();
		}

		public static TypeArgumentListSyntax TypeArgumentList (SyntaxToken openParenToken, SyntaxToken ofKeyword, SeparatedSyntaxList<TypeSyntax> arguments, SyntaxToken closeParenToken)
		{
			return new TypeArgumentListSyntax (arguments, openParenToken, ofKeyword, closeParenToken);
		}

		public static SyntaxTrivia WhitespaceTrivia (string text)
		{
			return new SyntaxTrivia (SyntaxKind.WhitespaceTrivia, text);
		}

		//public static ImportsStatementSyntax ImportsStatement (SyntaxToken importsKeyword, SeparatedSyntaxList<ImportsClauseSyntax> importsClauses)
		//{
		//	return new ImportsStatementSyntax (importsKeyword, importsClauses);
		//}

		//public static MembersImportsClauseSyntax MembersImportsClause (NameSyntax name)
		//{
		//	return new MembersImportsClauseSyntax (name);
		//}

		//public static AliasImportsClauseSyntax AliasImportsClause (SyntaxToken alias, SyntaxToken equalsToken, NameSyntax name)
		//{
		//	return new AliasImportsClauseSyntax (alias, equalsToken, name);
		//}

		//public static ModuleStatementSyntax ModuleStatement (object attributes, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier)
		//{
		//	return new ModuleStatementSyntax (attributes, modifiers, keyword, identifier);
		//}
	}
}

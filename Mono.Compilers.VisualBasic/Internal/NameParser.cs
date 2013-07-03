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
using System.Linq;

namespace Mono.Compilers.VisualBasic
{
	class NameParser
	{
		public static NameSyntax Parse (IEnumerable<SyntaxToken> tokens)
		{
			return ConsumeQualifiedName (tokens.ToList ());
		}

		private static NameSyntax ConsumeQualifiedName (List<SyntaxToken> tokens)
		{
			var list = new SeparatedSyntaxList<NameSyntax> ();
			var last_was_dot = true;
			var can_be_global = true;

			// Get all the name pieces and dot tokens
			while (true) {
				var token = tokens.First ();

				if (token.Kind == SyntaxKind.DotToken) {
					// Handle a malformed case of starting with a period
					if (list.Count == 0) {
						var missing_token = Syntax.MissingToken (SyntaxKind.IdentifierToken);
						tokens.Insert (0, missing_token);

						var name = ConsumeName (tokens);
						list.Add (name);
						can_be_global = false;
					}

					// Remove the dot from tokens and add it to syntax list
					tokens.RemoveAt (0);
					list.Add (token);

					// Implicit continuation after dot
					while (tokens.Count > 0 && tokens.First ().Kind == SyntaxKind.StatementTerminatorToken) {
						var term = tokens.First ();
						tokens.RemoveAt (0);
					}

					last_was_dot = true;
				} else if (IsValidIdentifier (token) && last_was_dot) {
					// Remove the name from tokens and add it to syntax list
					var name = ConsumeName (tokens, can_be_global);
					list.Add (name);
					last_was_dot = false;
					can_be_global = false;
				} else if (token.Kind == SyntaxKind.OpenParenToken && list.Count == 0) {
					// Handle a malformed case with nothing before the open parenthesis
					var missing_token = Syntax.MissingToken (SyntaxKind.IdentifierToken);
					tokens.Insert (0, missing_token);

					var name = ConsumeName (tokens);
					list.Add (name);
					last_was_dot = false;
				} else {
					break;
				}
			}

			// No valid name case
			if (list.Count == 0) {
				var missing_token = Syntax.MissingToken (SyntaxKind.IdentifierToken);
				tokens.Insert (0, missing_token);
				list.Add (ConsumeName (tokens));
			}

			// Trails with a period and no valid identifier after
			if (list.Count == list.SeparatorCount) {
				var missing_token = Syntax.MissingToken (SyntaxKind.IdentifierToken);
				tokens.Insert (0, missing_token);
				list.Add (ConsumeName (tokens));
			}

			// Build them into a QualifiedNameSyntax
			return BuildQualifiedSyntax (list);
		}

		private static NameSyntax ConsumeName (List<SyntaxToken> tokens, bool canBeGlobal = false)
		{
			// If we're here, we know the first token is a valid identifier
			var identifier = tokens.First ();
			tokens.RemoveAt (0);

			// The only valid next thing could be open parenthesis + "Of" for generic
			if (tokens.Count < 2)
				return Syntax.IdentifierName (identifier);

			if (tokens.First ().Kind != SyntaxKind.OpenParenToken)
				return CreateIdentifer (identifier, canBeGlobal);

			if (tokens.ElementAt (1).Kind != SyntaxKind.OfKeyword)
				return Syntax.IdentifierName (identifier);

			var open_param = tokens.First ();
			tokens.RemoveAt (0);

			var of_keyword = tokens.First ();
			tokens.RemoveAt (0);

			// Parse generic arguments
			var arguments = new SeparatedSyntaxList<TypeSyntax> ();

			while (true) {
				// Get a generic argument type
				var type = ConsumeQualifiedName (tokens);
				arguments.Add (type);

				// No closing parenthesis, return a IsMissing one
				if (tokens.Count == 0)
					return Syntax.GenericName (identifier, Syntax.TypeArgumentList (open_param, of_keyword, arguments, Syntax.MissingToken (SyntaxKind.CloseParenToken)));

				var token = tokens.First ();

				// We hit a closing parenthesis, return our generic name
				if (token.Kind == SyntaxKind.CloseParenToken) {
					tokens.RemoveAt (0);
					return Syntax.GenericName (identifier, Syntax.TypeArgumentList (open_param, of_keyword, arguments, token));
				}

				// We hit a comma, store it and restart to find the next token
				if (token.Kind == SyntaxKind.CommaToken) {
					tokens.RemoveAt (0);
					arguments.Add (token);
				} else {
					// Something else we don't handle, add a missing close parenthesis and return
					var missing_token = Syntax.MissingToken (SyntaxKind.CloseParenToken);
					return Syntax.GenericName (identifier, Syntax.TypeArgumentList (open_param, of_keyword, arguments, missing_token));
				}
			}
		}

		private static NameSyntax CreateIdentifer (SyntaxToken token, bool canBeGlobal)
		{
			// Global is a special name as an identifier
			if (canBeGlobal && token.Kind == SyntaxKind.GlobalKeyword)
				return Syntax.GlobalName (token);

			return Syntax.IdentifierName (token);
		}

		private static bool IsValidIdentifier (SyntaxToken token)
		{
			// Identifiers are obviously valid identifiers
			if (token.Kind == SyntaxKind.IdentifierToken)
				return true;

			var token_string = token.ToString ();

			if (token_string.Length > 0 && TokenFacts.IsIdentifierStartChar (token_string[0])) {
				if (token_string[0] != '_')
					return true;

				// If the first character is an underscore, we have to have another character after it
				if (token_string.Length > 1 && TokenFacts.IsIdentifierAdditionalChar (token_string[1]))
					return true;
			}

			return false;
		}

		private static NameSyntax BuildQualifiedSyntax (SeparatedSyntaxList<NameSyntax> tokens)
		{
			SyntaxToken? dot = null;
			NameSyntax right = null;

			// Start grabbing NameSyntaxii from the back
			right = tokens.GetAndRemoveLastSyntax ();

			// There's only 1 NameSyntax, so it's a SimpleNameSyntax
			if (tokens.SeparatorCount == 0)
				return right;

			// Remove and save the dot
			dot = tokens.GetAndRemoveLastSeparator ();

			// Recurse to build the left part
			var left = BuildQualifiedSyntax (tokens);

			return Syntax.QualifiedName (left, dot.Value, (SimpleNameSyntax)right);
		}
	}
}

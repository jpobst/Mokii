using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

//using Roslyn.Compilers.VisualBasic;
using Mono.Compilers.VisualBasic;

namespace MokiiSample
{
	class Program
	{
		static void Main (string[] args)
		{
			var source_text = File.ReadAllText ("input.txt");

			// Print out the input
			Console.WriteLine ("---Input---\n");
			Console.WriteLine (source_text);

			var tree = SyntaxTree.ParseCompilationUnit(source_text);
			var root = (CompilationUnitSyntax)tree.Root;

			// Replace "Hello World!" with "Goodbye World!"
			var output = root.GetFirstToken (p => p.Kind == SyntaxKind.StringLiteralToken);
			var new_output = Syntax.StringLiteralToken ("\"Goodbye World!\"", "Goodbye World!", output.LeadingTrivia, output.TrailingTrivia);

			root = root.ReplaceToken (output, new_output);

			// Add a space in front of all open parentheses
			var parens = root.DescendentTokens ().Where (p => p.Kind == SyntaxKind.OpenParenToken && p.LeadingWidth == 0);
			var new_parans = parens.Select (p => p.WithLeadingTrivia (Syntax.WhitespaceTrivia (" ")));
			
			root = root.ReplaceTokens (parens, (p, q) => p.WithLeadingTrivia (Syntax.WhitespaceTrivia (" ")));

			// Print out the output
			Console.WriteLine ("\n---Output---\n");
			OutputNode (root);

			Console.WriteLine ("\n\nPress <Enter> to exit..");
			Console.ReadLine ();
		}

		private static void OutputNode (CompilationUnitSyntax token)
		{
			var default_color = Console.ForegroundColor;

			foreach (var t in token.DescendentTokens ()) {
				// Make keywords blue
				if (SyntaxFacts.IsKeyword (t))
					Console.ForegroundColor = ConsoleColor.DarkCyan;

				Console.Write (t.ToString ());
				Console.ForegroundColor = default_color;
			}
		}

		private static void OutputTokens (CompilationUnitSyntax token)
		{
		        foreach (var t in token.DescendentTokens ()) {
				Console.WriteLine ("[{0}] - {1} ({2})", t.Kind, t.ValueText, t.LeadingWidth);

				foreach (var tr in t.LeadingTrivia)
				        Console.WriteLine ("  LEAD: {0} - {1}", tr.Kind, tr.FullWidth);

				foreach (var tr in t.TrailingTrivia)
				        Console.WriteLine ("  TAIL: {0} - {1}", tr.Kind, tr.FullWidth);
		        }
		}
	}
}

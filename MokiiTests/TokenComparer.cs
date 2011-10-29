using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mono.Compilers.VisualBasic;

namespace MokiiTests
{
	class TokenComparer
	{
		public static void Compare (CompilationUnitSyntax root, string filename)
		{
			var doc = XDocument.Load (filename);

			var x_tokens = doc.Root.Elements ("token").ToList ();
			var tokens = root.DescendentTokens ().ToList ();

			for (int i = 0; i < Math.Max (x_tokens.Count, tokens.Count); i++) {
				if (x_tokens.Count == i)
					Assert.Fail ("More tokens than expected");

				if (tokens.Count == i)
					Assert.Fail ("Less tokens than expected");

				var t = tokens[i];
				var t2 = x_tokens[i];

				CompareTokens (t, t2);
			}
		}

		private static void CompareTokens (SyntaxToken t, XElement t2)
		{
			Assert.AreEqual (t2.Element ("Kind").Value, t.Kind.ToString ());
			Assert.AreEqual (t2.Element ("IsBracketed").Value, t.IsBracketed.ToString ());
			Assert.AreEqual (t2.Element ("ValueText").Value.Replace ("[LF]", ((char)10).ToString ()).Replace ("[CR]", ((char)13).ToString ()), t.ValueText ?? string.Empty);

			CompareTriviaList (t.LeadingTrivia, t2.Element ("LeadingTrivia"));
			CompareTriviaList (t.TrailingTrivia, t2.Element ("TrailingTrivia"));
		}

		private static void CompareTriviaList (SyntaxTriviaList t_list, XElement t2_list)
		{
			var x_trivia = t2_list.Elements ("trivia").ToList ();

			for (int i = 0; i < Math.Max (t_list.Count, x_trivia.Count); i++) {
				Assert.AreEqual (x_trivia.Count, t_list.Count, "Trivia Count Mismatch");


				var t = t_list.list[i];
				var t2 = x_trivia[i];

				Assert.AreEqual (t2.Element ("Kind").Value, t.Kind.ToString ());
			}
			
		}
	}
}

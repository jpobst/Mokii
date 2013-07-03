using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

using Roslyn.Compilers.VisualBasic;
using Roslyn.Compilers.Common;
//using Mono.Compilers.VisualBasic;

namespace MokiiSample
{
	class Program
	{
		static void Main (string[] args)
		{

			// Generate Test Output
			var test_dir = @"C:\Users\Jonathan\Documents\Visual Studio 2012\Projects\Mokii-Compiler\MokiiTests";
			var a = SyntaxFacts.GetOperatorKind (">=");
			var c = Syntax.ElasticSpace;
			var d = Syntax.ElasticTab;
			var b = Mono.Compilers.VisualBasic.Syntax.CarriageReturn;

			//for (var i = 0; i <= 606; i++)
			//	if (SyntaxFacts.IsName ((SyntaxKind)i))
			//		Console.WriteLine ((SyntaxKind)i);

			foreach (var file in Directory.EnumerateFiles (Path.Combine (test_dir, "Inputs"), "*.vb")) {
				var test_input = File.ReadAllText (file, System.Text.UnicodeEncoding.UTF8);

				if (Path.GetFileName (file).StartsWith ("Conditional"))
					continue;
				if (Path.GetFileName (file).StartsWith ("Literals"))
					continue;
				//if (Path.GetFileName (file) == "DataSet1.vb")
				//	continue;
				//if (Path.GetFileName (file) == "TypeLoadEx1.vb")
				//	continue;
				//if (Path.GetFileName (file) == "NameResolution1.vb")
				//	continue;
				//if (Path.GetFileName (file) != "LShiftAssignStatement1.vb")
				//	continue;

				try {
					for (var x = 0; x < Math.Min (100, test_input.Length); x++) {
						//// ParseTokens
						//var roslyn_tokens = Syntax.ParseTokens (test_input, x).ToList ();
						//var mono_tokens = Mono.Compilers.VisualBasic.Syntax.ParseTokens (test_input, x).ToList ();
					
						//for (var i = 0; i < Math.Min (mono_tokens.Count, roslyn_tokens.Count); i++)
						//	CompareTokens (roslyn_tokens[i], mono_tokens[i]);

						//// ParseLeadingTrivia
						//var roslyn_lead_trivias = Syntax.ParseLeadingTrivia (test_input, x).ToList ();
						//var mono_lead_trivias = Mono.Compilers.VisualBasic.Syntax.ParseLeadingTrivia (test_input, x).ToList ();

						//for (var i = 0; i < Math.Min (roslyn_lead_trivias.Count, mono_lead_trivias.Count); i++)
						//	CompareTrivia (roslyn_lead_trivias[i], mono_lead_trivias[i]);

						//// ParseTrailingTrivia
						//var roslyn_trail_trivias = Syntax.ParseTrailingTrivia (test_input, x).ToList ();
						//var mono_trail_trivias = Mono.Compilers.VisualBasic.Syntax.ParseTrailingTrivia (test_input, x).ToList ();

						//for (var i = 0; i < Math.Min (roslyn_trail_trivias.Count, mono_trail_trivias.Count); i++)
						//	CompareTrivia (roslyn_trail_trivias[i], mono_trail_trivias[i]);

						//var parsed = Syntax.ParseCompilationUnit (test_input);
				
						// ParseName
						var roslyn_name = Syntax.ParseName (test_input, x);
						var mono_name = Mono.Compilers.VisualBasic.Syntax.ParseName (test_input, x);

						//CompareTokens ((roslyn_name as GenericNameSyntax).Identifier, (mono_name as Mono.Compilers.VisualBasic.GenericNameSyntax).Identifier);

						if (roslyn_name is IdentifierNameSyntax && mono_name is Mono.Compilers.VisualBasic.IdentifierNameSyntax) {
							if (roslyn_name.ToString () != mono_name.ToString ())
								Console.WriteLine ("{0} {1}", roslyn_name, mono_name);
						} else if (roslyn_name is QualifiedNameSyntax && mono_name is Mono.Compilers.VisualBasic.QualifiedNameSyntax) {
							if (Strip (roslyn_name) != Strip (mono_name))
								Console.WriteLine ("{0} {1}", Strip (roslyn_name), Strip (mono_name));
						} else if (roslyn_name is GenericNameSyntax && mono_name is Mono.Compilers.VisualBasic.GenericNameSyntax) {
							if (Strip (roslyn_name) != Strip (mono_name))
								Console.WriteLine ("{0} {1}", Strip (roslyn_name), Strip (mono_name));
						} else if (roslyn_name is GlobalNameSyntax && mono_name is Mono.Compilers.VisualBasic.GlobalNameSyntax) {
							if (Strip (roslyn_name) != Strip (mono_name))
								Console.WriteLine ("{0} {1}", Strip (roslyn_name), Strip (mono_name));
						} else 
							Console.WriteLine ("{0}: {1}, {2}", counter++, roslyn_name.Kind, mono_name.Kind);

						// ParseArgumentList
						//var roslyn_args = Syntax.ParseArgumentList (test_input, 36);
						//var mono_name = Mono.Compilers.VisualBasic.Syntax.ParseName (test_input, x);

					//Console.WriteLine ("{0} [{1}]: {2}ms", Path.GetFileName (file), test_input.Length, sw.ElapsedMilliseconds);
					//using (var sw = new StreamWriter (File.OpenWrite (@"C:\Users\Jonathan\Desktop\test.csv"))) {
					//	foreach (var token in mono_tokens)
					//		sw.WriteLine ("{0} [{1}]: {2}", token.Kind, token.FullSpan, token.ToString ());
					var a2 = Syntax.ParseTrailingTrivia (test_input, 23);

					//	sw.WriteLine ();
					//	sw.WriteLine ();
					//Console.WriteLine (x);
					//	foreach (var token in roslyn_tokens)
					//		sw.WriteLine ("{0} [{1}]: {2}", token.Kind, token.FullSpan, token.ToString ());
					//}
					//for (var i = 0; i < Math.Min (mono_tokens.Count, roslyn_tokens.Count); i++)
					//	if (roslyn_tokens[i].Value != null && roslyn_tokens[i].Value.ToString () != roslyn_tokens[i].ValueText)
					//		Console.WriteLine ("whaaaaa??");

					//if (mono_tokens.Count != roslyn_tokens.Count)
					//	Console.WriteLine ("{0}:{3} {1}, {2}", Path.GetFileName (file), mono_tokens.Count, roslyn_tokens.Count, x);
					//else {
						//for (var i = 0; i < Math.Min (mono_tokens.Count, roslyn_tokens.Count); i++)
						//	//if (roslyn_tokens[i].ToString () != mono_tokens[i].ToString ())
						//		CompareTokens (roslyn_tokens[i], mono_tokens[i]);
								//Console.WriteLine ("-{3} {0}: {1}, {2}", mono_tokens[i].ValueText, mono_tokens[i], roslyn_tokens[i], Path.GetFileName (file));

					}//}
				} catch (Exception) {
					Console.WriteLine ("error: {0}", Path.GetFileName (file));
					continue;
				}

				//return;
			}



			//using (var sww = new StreamWriter (@"C:\tests.cs")) { 
			//        foreach (var file in Directory.EnumerateFiles (Path.Combine (test_dir, "Inputs"), "*.vb")) {
			//                var test_input = File.ReadAllText (file);

			//                var tree7 = Roslyn.Compilers.VisualBasic.SyntaxTree.ParseCompilationUnit (test_input);
			//                var root7 = (Roslyn.Compilers.VisualBasic.CompilationUnitSyntax)tree7.Root;

			//                SyntaxTokenWriter.WriteNode (root7, Path.Combine (test_dir, "Outputs", Path.ChangeExtension (Path.GetFileName (file), ".xml")));

			//                sww.WriteLine ("[TestMethod]");
			//                sww.WriteLine ("public void {0} ()", Path.GetFileNameWithoutExtension (file));
			//                sww.WriteLine ("{");
			//                sww.WriteLine ("CompareTest (\"{0}\");", Path.GetFileNameWithoutExtension (file));
			//                sww.WriteLine ("}");
			//                sww.WriteLine ();
			//        }
			//}

			//return;

			// Output enums
			using (var sww = new StreamWriter (@"C:\Users\Jonathan\Desktop\enums.txt")) {
				foreach (var en in Enum.GetNames (typeof (TypeCharacter)))
					sww.WriteLine ("{0} = {1},", en, (int)Enum.Parse (typeof (TypeCharacter), en));
			}

			//return;

			// Output keywords
			//using (var sww = new StreamWriter (@"C:\keywords.txt")) {
			//        foreach (var en in SyntaxFacts.GetPunctuationKinds ())
			//                sww.WriteLine ("SyntaxKind.{0},", en);
			//}

			//return;
			//Mono.Compilers.
			//var kinds = Mono.Compilers.VisualBasic.SyntaxFacts.GetKeywordKinds ();
			//var kinds2 = SyntaxFacts.GetContextualKeywordKinds ().ToList ();
			//var kinds3 = SyntaxFacts.GetPreprocessorKeywordKinds ().ToList ();
			//var kinds4 = SyntaxFacts.GetReservedKeywordKinds ().ToList ();

			//var source_text = File.ReadAllText (@"input.txt");
			//var sw = new Stopwatch ();
			//sw.Start ();

			//// Roslyn
			//var tree = Roslyn.Compilers.VisualBasic.SyntaxTree.ParseText (source_text);
			//var root = (Roslyn.Compilers.VisualBasic.CompilationUnitSyntax)tree.GetRoot ();
			//Console.WriteLine ("{0}ms", sw.ElapsedMilliseconds);
			//sw.Restart ();

			//// Mono
			////var mono_tokens = Mono.Compilers.VisualBasic.Syntax.

			//var tree2 = Mono.Compilers.VisualBasic.SyntaxTree.ParseCompilationUnit (source_text);
			//var root2 = (Mono.Compilers.VisualBasic.CompilationUnitSyntax)tree2.Root;
			//Console.WriteLine ("{0}ms", sw.ElapsedMilliseconds);

			////OutputCompare (root2, root);
			////OutputNodes (root, 0);
			//OutputNodes (root2, 0);
			////OutputTokens (root2);
			////OutputStatements (root2);

			//if (root.ContainsDiagnostics) {
			//	foreach (var d in tree.GetDiagnostics ())
			//		Console.WriteLine ("{0}: {1}", d.Location.SourceSpan, d.Info);
			//}

			//Console.ReadLine ();
			//return;

			//// Print out the input
			////Console.WriteLine ("---Input---\n");
			////Console.WriteLine (source_text);

			////SyntaxTokenWriter.WriteNode (root, @"D:\Documents\Visual Studio 2010\Projects\Mokii\MokiiTests\Outputs\Accessibility1.xml");
			//Console.WriteLine ("{0}ms", sw.ElapsedMilliseconds);

			//Console.WriteLine (root.DescendantTokens ().Count ());
			//// Replace "Hello World!" with "Goodbye World!"
			////var output = root.GetFirstToken (p => p.Kind == SyntaxKind.StringLiteralToken);
			////var new_output = Syntax.StringLiteralToken ("\"Goodbye World!\"", "Goodbye World!", output.LeadingTrivia, output.TrailingTrivia);

			////root = root.ReplaceToken (output, new_output);

			////// Add a space in front of all open parentheses
			////var parens = root.DescendentTokens ().Where (p => p.Kind == SyntaxKind.OpenParenToken && p.LeadingWidth == 0);
			////var new_parans = parens.Select (p => p.WithLeadingTrivia (Syntax.WhitespaceTrivia (" ")));

			////root = root.ReplaceTokens (parens, (p, q) => p.WithLeadingTrivia (Syntax.WhitespaceTrivia (" ")));

			//// Print out the output
			//Console.WriteLine ("\n---Output---\n");
			//OutputNode (root);

			Console.WriteLine ("\n\nPress <Enter> to exit..");
			Console.ReadLine ();
		}

		private static string Strip (SyntaxNode text)
		{
			return text.ToString ().Replace (" ", "").Replace ("\n", "").Replace ("\r", "").Trim ();
		}

		private static string Strip (Mono.Compilers.VisualBasic.SyntaxNode text)
		{
			return text.ToString ().Replace (" ", "").Replace ("\n", "").Replace ("\r", "").Trim ();
		}

		private static void CompareTokens (SyntaxToken roslyn, Mono.Compilers.VisualBasic.SyntaxToken mono)
		{
			if (roslyn.Kind.ToString () != mono.Kind.ToString ())
				Console.WriteLine ("- Kind: {0}, {1}", roslyn.Kind, mono.Kind);
			if ((int?)roslyn.Base != (int?)mono.Base)
				Console.WriteLine ("- Base: {0}, {1}", roslyn.Base, mono.Base);
			if (roslyn.FullSpan.ToString () != mono.FullSpan.ToString ())
				Console.WriteLine ("- FullSpan: {0}, {1}", roslyn.FullSpan, mono.FullSpan);
			if (roslyn.HasLeadingTrivia != mono.HasLeadingTrivia)
				Console.WriteLine ("- HasLeadingTrivia: {0}, {1}", roslyn.HasLeadingTrivia, mono.HasLeadingTrivia);
			if (roslyn.HasTrailingTrivia != mono.HasTrailingTrivia)
				Console.WriteLine ("- HasTrailingTrivia: {0}, {1}", roslyn.HasTrailingTrivia, mono.HasTrailingTrivia);
			if (roslyn.IsBracketed != mono.IsBracketed)
				Console.WriteLine ("- IsBracketed: {0}, {1}", roslyn.IsBracketed, mono.IsBracketed);
			if (roslyn.IsMissing != mono.IsMissing)
				Console.WriteLine ("- IsMissing: {0}, {1}", roslyn.IsMissing, mono.IsMissing);
			if (roslyn.Span.ToString () != mono.Span.ToString ())
				Console.WriteLine ("- Span: {0}, {1}", roslyn.Span, mono.Span);
			if (roslyn.TypeCharacter.ToString () != mono.TypeCharacter.ToString ())
				Console.WriteLine ("- TypeCharacter: {0}, {1}", roslyn.TypeCharacter, mono.TypeCharacter);
			if (roslyn.ValueText != mono.ValueText)
				Console.WriteLine ("- ValueText: {0}, {1}", roslyn.ValueText, mono.ValueText);
			if (roslyn.Value == null && mono.Value == null) {
				// Equals
			} else if (roslyn.Value == null && mono.Value != null)
				Console.WriteLine ("- Value: {0}, {1}", "<null>", mono.Value);
			else if (roslyn.Value != null && mono.Value == null)
				Console.WriteLine ("- Value: {0}, {1}", roslyn.Value, "<null>");
			else if (roslyn.Value.ToString () != mono.Value.ToString ())
				Console.WriteLine ("- Value: {0}, {1}", roslyn.Value, mono.Value);
			else if (roslyn.Value.GetType ().ToString () != mono.Value.GetType ().ToString ())
				Console.WriteLine ("- ValueType: {0}, {1}", roslyn.Value.GetType (), mono.Value.GetType ());
			if (roslyn.ToString () != mono.ToString ())
				Console.WriteLine ("- ToString: {0}, {1}", roslyn, mono);
			if (roslyn.FullSpan.ToString () == mono.FullSpan.ToString () && roslyn.ToFullString () != mono.ToFullString ())
				Console.WriteLine ("- ToFullString: {0}, {1}", roslyn.ToFullString (), mono.ToFullString ());

			if (roslyn.LeadingTrivia.Count != mono.LeadingTrivia.Count)
				Console.WriteLine ("boo");
			if (roslyn.TrailingTrivia.Count != mono.TrailingTrivia.Count)
				Console.WriteLine ("boo");

			for (int i = 0; i < Math.Min (roslyn.LeadingTrivia.Count, mono.LeadingTrivia.Count); i++)
				CompareTrivia (roslyn.LeadingTrivia[i], mono.LeadingTrivia[i]);

			for (int i = 0; i < Math.Min (roslyn.TrailingTrivia.Count, mono.TrailingTrivia.Count); i++)
				CompareTrivia (roslyn.TrailingTrivia[i], mono.TrailingTrivia[i]);
		}
		static int counter = 0;

		private static void CompareTrivia (SyntaxTrivia roslyn, Mono.Compilers.VisualBasic.SyntaxTrivia mono)
		{
			//Console.WriteLine (counter++);
			if (roslyn.FullSpan.ToString () != mono.FullSpan.ToString ())
				Console.WriteLine ("- TV FullSpan: {0}, {1}", roslyn.FullSpan, mono.FullSpan);
			if (roslyn.HasStructure != mono.HasStructure)
				Console.WriteLine ("- TV HasStructure: {0}, {1}", roslyn.HasStructure, mono.HasStructure);
			if (roslyn.IsDirective != mono.IsDirective)
				Console.WriteLine ("- TV IsDirective: {0}, {1}", roslyn.IsDirective, mono.IsDirective);
			if (roslyn.Kind.ToString () != mono.Kind.ToString ())
				Console.WriteLine ("- TV Kind: {0}, {1}", roslyn.Kind, mono.Kind);
			if (roslyn.Span.ToString () != mono.Span.ToString ())
				Console.WriteLine ("- TV Span: {0}, {1}", roslyn.Span, mono.Span);
			if (roslyn.ToString () != mono.ToString ())
				Console.WriteLine ("- TV ToString: {0}, {1}", roslyn, mono);
			if (roslyn.FullSpan.ToString () == mono.FullSpan.ToString () && roslyn.ToFullString () != mono.ToFullString ())
				Console.WriteLine ("- ToFullString: {0}, {1}", roslyn.ToFullString (), mono.ToFullString ());
		}

		private static void OutputNode (CompilationUnitSyntax token)
		{
			var default_color = Console.ForegroundColor;

			foreach (var t in token.DescendantTokens ()) {
				// Make keywords blue
				if (SyntaxFacts.IsKeyword (t))
					Console.ForegroundColor = ConsoleColor.DarkCyan;

				Console.Write (t.ToString ());
				Console.ForegroundColor = default_color;
			}
		}

		//private static void OutputStatements (Mono.Compilers.VisualBasic.CompilationUnitSyntax token)
		//{
		//	var parser = new Mono.Compilers.VisualBasic.StatementParser (token.DescendentTokens ());

		//	foreach (var s in parser.GetStatements ())
		//		Console.WriteLine ("{0}: {1}", s.Count, s[0].Kind);
		//}

		//private static void OutputTokens (Mono.Compilers.VisualBasic.CompilationUnitSyntax token)
		//{
		//	int i = 0;
		//	foreach (var t in token.DescendentTokens ()) {
		//		Console.WriteLine ("[{0}] - {1} ({2}, {3})", t.Kind, t.ValueText, t.LeadingWidth, t.LeadingWidth);
		//		i++;
		//		if (i > 100) return;
		//		//foreach (var tr in t.LeadingTrivia)
		//		//        Console.WriteLine ("  LEAD: {0} - {1}", tr.Kind, tr.FullWidth);

		//		//foreach (var tr in t.TrailingTrivia)
		//		//        Console.WriteLine ("  TAIL: {0} - {1}", tr.Kind, tr.FullWidth);
		//	}
		//}

		//private static void OutputCompare (Mono.Compilers.VisualBasic.SyntaxNode node1, Roslyn.Compilers.VisualBasic.SyntaxNode node2)
		//{
		//	int start = 0;
		//	int limit = 100;

		//	var l1 = node1.DescendentTokens ().ToList ();
		//	var l2 = node2.DescendantTokens ().ToList ();

		//	for (int i = start; i < Math.Max (l1.Count, l2.Count); i++) {
		//		if (i == limit)
		//			return;

		//		string s1 = string.Empty;
		//		string s2 = string.Empty;
		//		string lt1 = string.Empty;
		//		string lt2 = string.Empty;
		//		string tt1 = string.Empty;
		//		string tt2 = string.Empty;

		//		if (i < l1.Count) {
		//			s1 = string.Format ("{0} [{1}] {2}", l1[i].Kind, Sanitize (l1[i].ValueText), l1[i].Span);
		//		}

		//		if (i < l2.Count) {
		//			s2 = string.Format ("{0} [{1}] {2}", l2[i].Kind, Sanitize (l2[i].ValueText), l2[i].Span);
		//		}

		//		OutputLine (s1, s2);

		//		if (i == l1.Count || l2.Count == i) {
		//			Console.WriteLine ("ran out of nodes for trivia");
		//		} else {
		//			OutputTrivias ("L", l1[i].LeadingTrivia, l2[i].LeadingTrivia);
		//			OutputTrivias ("T", l1[i].TrailingTrivia, l2[i].TrailingTrivia);
		//		}
		//	}

		//	Console.WriteLine ("{0} errors", errors);
		//}

		private static void OutputTrivias (string type, Mono.Compilers.VisualBasic.SyntaxTriviaList l1, Roslyn.Compilers.VisualBasic.SyntaxTriviaList l2)
		{
			for (int i = 0; i < Math.Max (l1.Count, l2.Count); i++) {
				string s1 = string.Empty;
				string s2 = string.Empty;

				if (i < l1.Count) {
					s1 = string.Format ("{0} [{1}:{2}]", l1[i].Kind, l1[i].FullSpan.Length, Sanitize (l1[i].ToString ()));
				}

				if (i < l2.Count) {
					s2 = string.Format ("{0} [{1}:{2}]", l2[i].Kind, "" /* l2[i].FullWidth */, Sanitize (l2[i].ToString ()));
				}

				OutputLine (" " + type + " " + s1, " " + type + " " + s2);
			}
		}

		static int errors = 0;

		private static void OutputLine (string s1, string s2)
		{
			int pad = 37;

			var color = Console.ForegroundColor;

			if (string.Compare (s1, s2) != 0) {
				Console.ForegroundColor = ConsoleColor.Red;
				errors++;
			}

			if (!string.IsNullOrWhiteSpace (s1) && !string.IsNullOrWhiteSpace (s2))
				Console.WriteLine ("{0}| {1}", s1.PadRight (pad), s2);

			Console.ForegroundColor = color;
		}

		private static string Sanitize (string s)
		{
			if (s == null)
				return s;

			return s.Replace (((char)10).ToString (), "[LF]").Replace (((char)13).ToString (), "[CR]");
		}

		private static string Left (string text, int length)
		{
			return text.Substring (0, Math.Min (length, text.Length));
		}

		//private static void OutputNodes (Mono.Compilers.VisualBasic.SyntaxNodeOrToken node, int indent)
		////private static void OutputNodes (SyntaxNodeOrToken node, int indent)
		//{
		//	var color = Console.ForegroundColor;

		//	foreach (var t in node.ChildNodesAndTokens ()) {
		//		if (t.IsMissing)
		//			Console.ForegroundColor = ConsoleColor.DarkGray;
		//		if (t.HasDiagnostics)
		//			Console.ForegroundColor = ConsoleColor.Red;
		//		//t.HasDiagnostics
		//		Console.WriteLine ("{4}: {2}[{0} - {3}] - {1}", t.Kind, Left (t.ToString (), 40).Replace (Environment.NewLine, "LF"), ws.Substring (0, indent), t.Width, t.Span);
		//		Console.ForegroundColor = color;

		//		OutputNodes (t, indent + 1);
		//		//foreach (var tr in t.chil)
		//		//        Console.WriteLine ("  LEAD: {0} - {1}", tr.Kind, tr.FullWidth);

		//		//foreach (var tr in t.TrailingTrivia)
		//		//        Console.WriteLine ("  TAIL: {0} - {1}", tr.Kind, tr.FullWidth);
		//	}
		//}

		private static string ws = "                                         ";
	}
}

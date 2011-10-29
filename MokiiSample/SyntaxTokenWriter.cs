using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

//using Mono.Compilers.VisualBasic;
using Roslyn.Compilers.VisualBasic;
using Roslyn.Compilers;

namespace MokiiSample
{
	static class SyntaxTokenWriter
	{
		public static void WriteNode (CompilationUnitSyntax root, string outputFile)
		{
			var xws = new XmlWriterSettings ();
			xws.Indent = true;
			
			var xw = XmlWriter.Create (outputFile, xws);

			var tokens = root.DescendentTokens ();

			xw.WriteStartElement ("root");

			foreach (var token in tokens) {
				xw.WriteStartElement ("token");

				// Properties
				xw.WriteElementString ("ContextualKind", token.ContextualKind.ToString ());
				xw.WriteElementString ("FullWidth", token.FullWidth.ToString ());
				xw.WriteElementString ("HasAnnotations", token.HasAnnotations.ToString ());
				xw.WriteElementString ("HasDiagnostics", token.HasDiagnostics.ToString ());
				xw.WriteElementString ("HasDirectives", token.HasDirectives.ToString ());
				xw.WriteElementString ("HasLeadingTrivia", token.HasLeadingTrivia.ToString ());
				xw.WriteElementString ("HasStructuredTrivia", token.HasStructuredTrivia.ToString ());
				xw.WriteElementString ("HasTrailingTrivia", token.HasTrailingTrivia.ToString ());
				xw.WriteElementString ("IsBracketed", token.IsBracketed.ToString ());
				xw.WriteElementString ("IsMissing", token.IsMissing.ToString ());
				xw.WriteElementString ("Kind", token.Kind.ToString ());
				xw.WriteElementString ("LeadingWidth", token.LeadingWidth.ToString ());
				xw.WriteElementString ("TrailingWidth", token.TrailingWidth.ToString ());
				xw.WriteElementString ("TypeCharacter", token.TypeCharacter.ToString ());
				xw.WriteElementString ("Value", (token.Value ?? string.Empty).ToString ());
				xw.WriteElementString ("ValueText", token.ValueText.ToString ().Replace (((char)10).ToString (), "[LF]").Replace (((char)13).ToString (), "[CR]"));
				xw.WriteElementString ("Width", token.Width.ToString ());

				// Trivia
				WriteTrivia (xw, "LeadingTrivia", token.LeadingTrivia);
				WriteTrivia (xw, "TrailingTrivia", token.TrailingTrivia);
				
				// Span
				WriteSpan (xw, "FullSpan", token.FullSpan);
				WriteSpan (xw, "Span", token.Span);

				xw.WriteEndElement ();
			}

			xw.WriteEndElement ();
			xw.Close ();
		}

		private static void WriteTrivia (XmlWriter xw, string name, SyntaxTriviaList list)
		{
			xw.WriteStartElement (name);

			foreach (var trivia in list) {
				xw.WriteStartElement ("trivia");

				// Properties
				xw.WriteElementString ("FullWidth", trivia.FullWidth.ToString ());
				xw.WriteElementString ("HasAnnotations", trivia.HasAnnotations.ToString ());
				xw.WriteElementString ("HasDiagnostics", trivia.HasDiagnostics.ToString ());
				xw.WriteElementString ("HasStructure", trivia.HasStructure.ToString ());
				xw.WriteElementString ("IsDirective", trivia.IsDirective.ToString ());
				xw.WriteElementString ("IsElastic", trivia.IsElastic.ToString ());
				xw.WriteElementString ("Kind", trivia.Kind.ToString ());
				xw.WriteElementString ("Width", trivia.Width.ToString ());
				
				// Span
				WriteSpan (xw, "FullSpan", trivia.FullSpan);
				WriteSpan (xw, "Span", trivia.Span);

				xw.WriteEndElement ();
			}

			xw.WriteEndElement ();
		}

		private static void WriteSpan (XmlWriter xw, string name, TextSpan span)
		{
			xw.WriteStartElement (name);

			xw.WriteElementString ("End", span.End.ToString ());
			xw.WriteElementString ("IsEmpty", span.IsEmpty.ToString ());
			xw.WriteElementString ("Length", span.Length.ToString ());
			xw.WriteElementString ("Start", span.Start.ToString ());

			xw.WriteEndElement ();
		}
	}
}

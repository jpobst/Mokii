using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Compilers.VisualBasic
{
	public class GenericNameSyntax : SimpleNameSyntax
	{
		public SyntaxToken Identifier { get; private set; }
		public TypeArgumentListSyntax TypeArgumentList { get; private set; }

		internal GenericNameSyntax (SyntaxToken identifer, TypeArgumentListSyntax argumentList) : base (SyntaxKind.GenericName)
		{
			Add (identifer);
			Add (argumentList);

			Identifier = identifer;
			TypeArgumentList = argumentList;
		}
	}
}

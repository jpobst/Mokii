using System.Collections.Generic;
public class ModuleStatementSyntax : StatementSyntax
{
    internal ModuleStatementSyntax(object attributes, SyntaxTokenList modifiers, SyntaxToken keyword, SyntaxToken identifier): base (SyntaxKind.ModuleStatement)
    {
        AddNodeOrToken(keyword);
        AddNodeOrToken(identifier);
    }

    internal static ModuleStatementSyntax FromMatcher(TokenMatcher matcher)
    {
        List<SyntaxNodeOrToken> list;
        list = matcher.TryMatch(SyntaxKind.ModuleKeyword, SyntaxKind.IdentifierToken);
	//if (list != null)
	//{
	//    return Syntax.ModuleStatement(null, null, list[0].AsToken(), list[1].AsToken());
	//}

        return null;
    }
}
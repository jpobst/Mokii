using System.Collections.Generic;
public class NamespaceStatementSyntax : StatementSyntax
{
    internal NamespaceStatementSyntax(): base (SyntaxKind.NamespaceStatement)
    {
    }

    internal NamespaceStatementSyntax(List<SyntaxNodeOrToken> statement): base (SyntaxKind.NamespaceStatement)
    {
        foreach (var node in statement)
        {
            AddNodeOrToken(node);
        }
    }

    internal static NamespaceStatementSyntax FromMatcher(TokenMatcher matcher)
    {
        List<SyntaxNodeOrToken> list;
        list = matcher.TryMatch(SyntaxKind.NamespaceKeyword, SyntaxKind.QualifiedName);
        if (list != null)
        {
            return new NamespaceStatementSyntax(list);
        }

        return null;
    }

    public NameSyntax Name
    {
        get
        {
            return ChildNodes.First();
        }
    }

    public SyntaxToken NamespaceKeyword
    {
        get
        {
            return ChildNodesAndTokens()(0).AsToken();
        }
    }
}
using System;
public sealed class ImportsStatementSyntax : StatementSyntax
{
    private SyntaxToken imports_keyword;
    private SeparatedSyntaxList<ImportsClauseSyntax> imports_clauses;
    internal ImportsStatementSyntax(SyntaxToken importsKeyword, SeparatedSyntaxList<ImportsClauseSyntax> importsClauses): base (SyntaxKind.ImportsStatement)
    {
        AddNodeOrToken(importsKeyword);
        foreach (var node in importsClauses.GetWithSeparators())
        {
            AddNodeOrToken(node);
        }
    }

    internal static ImportsStatementSyntax FromMatcher(TokenMatcher matcher)
    {
        var keyword = matcher.ConsumeToken();
        var clauses = ParseImportsClauses(matcher);
        return Syntax.ImportsStatement(keyword, clauses);
    }

    private static SeparatedSyntaxList<ImportsClauseSyntax> ParseImportsClauses(TokenMatcher matcher)
    {
        var clauses = new SeparatedSyntaxList<ImportsClauseSyntax>();
        while (true)
        {
            var clause = ParseImportsClause(matcher);
            clauses.Add(clause);
            if (!matcher.AtToken(SyntaxKind.CommaToken))
            {
                return clauses;
            }

            // Consume the CommaToken
            clauses.Add(matcher.ConsumeToken());
        }

        return clauses;
    }

    private static ImportsClauseSyntax ParseImportsClause(TokenMatcher matcher)
    {
        if (!matcher.AtIdentifier ())
        {
            throw new ArgumentOutOfRangeException("Identifier expected");
        }

        if ((SyntaxKind)matcher.Peek().Kind == SyntaxKind.EqualsToken)
        {
            var identifier = matcher.ConsumeToken();
            var equals = matcher.ConsumeToken();
            if (!matcher.AtIdentifier ())
            {
                throw new ArgumentOutOfRangeException("Identifier expected");
            }

            var name = matcher.ConsumeIdentifer();
            return Syntax.AliasImportsClause(identifier, equals, name);
        }
        else
        {
            var identifier = matcher.ConsumeIdentifer();
            return Syntax.MembersImportsClause(identifier);
        }
    }

    public SyntaxToken ImportsKeyword
    {
        get
        {
            return ChildNodesAndTokens()[0].AsToken();
        }
    }

    public SeparatedSyntaxList<ImportsClauseSyntax> ImportsClauses
    {
        get
        {
		return imports_clauses;
        }
    }
}
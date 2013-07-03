public class AliasImportsClauseSyntax : ImportsClauseSyntax
{
    private SyntaxToken _alias;
    private SyntaxToken _equals;
    private NameSyntax _name;
    internal AliasImportsClauseSyntax(SyntaxToken alias, SyntaxToken equalsToken, NameSyntax name): base (SyntaxKind.AliasImportsClause)
    {
        AddNodeOrToken(alias);
        AddNodeOrToken(equalsToken);
        AddNodeOrToken(name);
        _alias = alias;
        _equals = equalsToken;
        _name = name;
    }

    public SyntaxToken Alias
    {
        get
        {
            return _alias;
        }
    }

    public SyntaxToken EqualsToken
    {
        get
        {
            return _equals;
        }
    }

    public NameSyntax Name
    {
        get
        {
            return _name;
        }
    }
}
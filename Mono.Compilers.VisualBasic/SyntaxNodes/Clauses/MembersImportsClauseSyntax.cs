public sealed class MembersImportsClauseSyntax : ImportsClauseSyntax
{
    private NameSyntax _name;
    internal MembersImportsClauseSyntax(NameSyntax name): base (SyntaxKind.MembersImportsClause)
    {
        AddNodeOrToken(name);
        _name = name;
    }

    public NameSyntax Name
    {
        get
        {
            return _name;
        }
    }
}
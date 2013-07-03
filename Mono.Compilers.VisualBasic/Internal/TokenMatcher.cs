using System.Collections.Generic;
namespace Mono.Compilers.VisualBasic
{
class TokenMatcher
{
    private SyntaxToken[] tokens;
    protected int curr = 0;
    public TokenMatcher(List<SyntaxToken> tokens)
    {
        this.tokens = tokens.ToArray();
    }

    /* #Region "Position Functions"
 */
    protected void Advance(int count = 1)
    {
        curr = count;
    }

    private SyntaxToken Current()
    {
        return tokens[curr];
    }

    public SyntaxToken Peek()
    {
        if (curr + 1 == tokens.Length)
        {
            return SyntaxToken.None;
        }

        return tokens[curr + 1];
    }

    /* #End Region
 */
    /* #Region "At Functions"
 */
    public bool AtComma()
    {
        return AtToken(SyntaxKind.CommaToken);
    }

    public bool AtDot()
    {
        return AtToken(SyntaxKind.DotToken);
    }

    private bool AtEndOfStatement()
    {
        return curr == tokens.Length;
    }

    public bool AtIdentifier()
    {
        return AtToken(SyntaxKind.IdentifierToken);
    }

    public bool AtToken(SyntaxKind kind)
    {
        if (AtEndOfStatement())
        {
            return false;
        }

        switch (kind)
        {
            case SyntaxKind.IdentifierName:
                break;
            case SyntaxKind.QualifiedName:
                if (AtIdentifier())
                {
                    return true;
                }

                break;
        }

        return (SyntaxKind)Current().Kind == kind;
    }

    /* #End Region
 */
    /* #Region "Consume Functions"
 */
    public NameSyntax ConsumeIdentifer()
    {
        var identifier = new IdentifierNameSyntax(ConsumeToken());
        // This is just "System"
        if (!AtDot())
        {
            return identifier;
        }

        return ConsumeQualifiedName(identifier);
    }

    public QualifiedNameSyntax ConsumeQualifiedName(NameSyntax left)
    {
        var period = ConsumeToken();
        var id2 = new IdentifierNameSyntax(ConsumeToken());
        var qual = new QualifiedNameSyntax(left, period, id2);
        if (AtDot())
        {
            return ConsumeQualifiedName(qual);
        }

        return qual;
    }

    public SyntaxNodeOrToken ConsumeToken(SyntaxKind kind)
    {
        var token = Current();
        switch (kind)
        {
            case SyntaxKind.IdentifierName:
                break;
            case SyntaxKind.QualifiedName:
                if (AtIdentifier())
                {
                    return ConsumeIdentifer();
                }

                break;
            default:
                if ((SyntaxKind)token.Kind == kind)
                {
                    return ConsumeToken();
                }
                break;
        }

        return null;
    }

    public SyntaxToken ConsumeToken()
    {
        var token = Current();
        curr += 1;
        return token;
    }

    /* #End Region
 */
    public List<SyntaxNodeOrToken> TryMatch(params SyntaxKind[] kinds)
    {
        var original_cur = curr;
        var list = new List<SyntaxNodeOrToken>();
        foreach (var kind in kinds)
        {
            if (!AtToken(kind))
            {
                // Not a match, let's reset anything we consumed
                curr = original_cur;
                return null;
            }

            list.Add(ConsumeToken(kind));
        }

        return list;
    }

    public void Reset()
    {
        curr = 0;
    }
}}
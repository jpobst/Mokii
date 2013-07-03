using System.Collections.Generic;
namespace Mono.Compilers.VisualBasic
{
public class StatementParser
{
    private IEnumerable<SyntaxToken> tokens;
    public StatementParser(IEnumerable<SyntaxToken> tokens)
    {
        this.tokens = tokens;
    }

    //    'Public Iterator Function GetStatements() As IEnumerable(Of List(Of SyntaxToken))
    //'    Dim list As New List(Of SyntaxToken)
    //'    Dim enumerator = tokens.GetEnumerator()

    //'    While enumerator.MoveNext()
    //'        Select Case enumerator.Current.Kind
    //'            Case SyntaxKind.StatementTerminatorToken
    //'                Yield list
    //'                list.Clear()

    //'                ' Return the Statement Terminator
    //'                list.Add(enumerator.Current)
    //'                Yield list
    //'                list.Clear()
    //'            Case SyntaxKind.EndOfFileToken
    //'                If list.Count > 0 Then Yield list
    //'                Return
    //'            Case Else
    //'                list.Add(enumerator.Current)
    //'        End Select
    //'    End While
    //'End Function

    //'Public Iterator Function GetNodes() As IEnumerable(Of SyntaxNodeOrToken)
    //'    For Each statement In GetStatements()

    //'        Dim matcher = New TokenMatcher(statement)

    //'        ' Many statements we can figure out from the first token
    //'        Select Case statement(0).Kind
    //'            Case SyntaxKind.ImportsKeyword
    //'                Dim syntax = ImportsStatementSyntax.FromMatcher(matcher)
    //'                If syntax IsNot Nothing Then Yield syntax
    //'            Case SyntaxKind.NamespaceKeyword
    //'                Dim syntax = NamespaceStatementSyntax.FromMatcher(matcher)
    //'                If syntax IsNot Nothing Then Yield syntax
    //'            Case SyntaxKind.ModuleKeyword
    //'                Dim syntax = ModuleStatementSyntax.FromMatcher(matcher)
    //'                If syntax IsNot Nothing Then Yield syntax
    //'            Case SyntaxKind.StatementTerminatorToken
    //'                Yield statement(0)
    //'        End Select
    //'    Next
    //'End Function

}}
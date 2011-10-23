' 
' Copyright (c) 2011 Jonathan Pobst
'  
' Author:
'       Jonathan Pobst <monkey@jpobst.com>
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy
' of this software and associated documentation files (the "Software"), to deal
' in the Software without restriction, including without limitation the rights
' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
' copies of the Software, and to permit persons to whom the Software is
' furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in
' all copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
' THE SOFTWARE.

Imports System.Runtime.CompilerServices

Public Module SyntaxExtensions
    <Extension()> _
    Public Function ReplaceToken(root As CompilationUnitSyntax, oldToken As SyntaxToken, newToken As SyntaxToken) As CompilationUnitSyntax
        ' Create a new SyntaxTokenList from the old one
        Dim list = New List(Of SyntaxToken)(root.DescendentTokens())

        ' Remove the old token
        Dim index = list.IndexOf(oldToken)
        list.RemoveAt(index)

        ' Add the new token
        list.Insert(index, newToken)

        ' Create the new Node
        Dim new_root = New CompilationUnitSyntax(New SyntaxTokenList(list))

        Return new_root
    End Function

    <Extension()> _
    Public Function ReplaceTokens(root As CompilationUnitSyntax, oldTokens As IEnumerable(Of SyntaxToken), computeReplacementToken As Func(Of SyntaxToken, SyntaxToken, SyntaxToken)) As CompilationUnitSyntax
        ' Create a new SyntaxTokenList from the old one
        Dim list = New List(Of SyntaxToken)(root.DescendentTokens())

        For Each token In oldTokens
            Dim new_token = computeReplacementToken(token, token)

            ' Remove the old token
            Dim index = list.IndexOf(token)
            list.RemoveAt(index)

            ' Add the new token
            list.Insert(index, new_token)
        Next

        ' Create the new Node
        Dim new_root = New CompilationUnitSyntax(New SyntaxTokenList(list))

        Return new_root
    End Function

    <Extension()> _
    Public Function WithLeadingTrivia(token As SyntaxToken, trivia As SyntaxTrivia) As SyntaxToken
        Dim list = Syntax.TriviaList(trivia)

        Return Syntax.Token(list, token.Kind, token.TrailingTrivia, token.ValueText)
    End Function
End Module

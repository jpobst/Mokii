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

Public Class Syntax
    Public Shared Function Token(ByVal kind As SyntaxKind, Optional ByVal text As String = Nothing) As SyntaxToken
        Return New SyntaxToken(Nothing, kind, Nothing, text)
    End Function

    Public Shared Function Token(ByVal kind As SyntaxKind, trailing As SyntaxTriviaList, Optional ByVal text As String = Nothing) As SyntaxToken
        Return New SyntaxToken(Nothing, kind, trailing, text)
    End Function

    Public Shared Function Token(leading As SyntaxTriviaList, ByVal kind As SyntaxKind, trailing As SyntaxTriviaList, Optional ByVal text As String = Nothing) As SyntaxToken
        Return New SyntaxToken(leading, kind, trailing, text)
    End Function

    Public Shared Function SyntaxTrivia(kind As SyntaxKind, text As String) As SyntaxTrivia
        Return New SyntaxTrivia(kind, text)
    End Function

    Public Shared Function TriviaList(trivia As SyntaxTrivia) As SyntaxTriviaList
        Dim list = New SyntaxTriviaList
        list.Initialize(trivia)

        Return list
    End Function

    Public Shared Function TriviaList(trivias As IEnumerable(Of SyntaxTrivia)) As SyntaxTriviaList
        Dim list = New SyntaxTriviaList
        list.Initialize(trivias)

        Return list
    End Function

    Public Shared Function SyntaxTokenList(tokens As IEnumerable(Of SyntaxToken)) As SyntaxTokenList
        Dim list = New SyntaxTokenList(tokens)

        Return list
    End Function

    Public Shared Function StringLiteralToken(text As String, value As String, Optional leadingTrivia As SyntaxTriviaList = Nothing, Optional trailingTrivia As SyntaxTriviaList = Nothing) As SyntaxToken
        Return Token(leadingTrivia, SyntaxKind.StringLiteralToken, trailingTrivia, text)
    End Function

    Public Shared Function WhitespaceTrivia(text As String) As SyntaxTrivia
        Return New SyntaxTrivia(SyntaxKind.WhitespaceTrivia, text)
    End Function

    Public Shared Function ParseLeadingTrivia(text As String, Optional offset As Integer = 0) As SyntaxTriviaList
        While Char.IsWhiteSpace(text.Chars(offset)) OrElse text(offset) = "_"
            offset += 1
        End While

        offset -= 1

        If offset = -1 Then Return SyntaxTriviaList.Empty

        Return ParseTrivia(text.Substring(0, offset))
    End Function

    Public Shared Function ParseTrailingTrivia(text As String, Optional offset As Integer = 0) As SyntaxTriviaList
        Dim index = text.Length - 1

        While Char.IsWhiteSpace(text.Chars(index))
            index -= 1
        End While

        index += 1
        If index = text.Length Then Return SyntaxTriviaList.Empty

        Return ParseTrivia(text.Substring(index))
    End Function

    Public Shared Function EndOfLineTrivia(text As String)
        Return New SyntaxTrivia(SyntaxKind.EndOfLineTrivia, text)
    End Function

    Public Shared Function CommentTrivia(text As String)
        Return New SyntaxTrivia(SyntaxKind.CommentTrivia, text)
    End Function

    Public Shared Function LineContinuationTrivia(text As String)
        Return New SyntaxTrivia(SyntaxKind.LineContinuationTrivia, text)
    End Function

    Friend Shared Function ParseTrivia(text As String) As SyntaxTriviaList
        Dim parser = New VBTriviaParser(text)

        Return TriviaList(parser.GetTrivia())
    End Function
End Class

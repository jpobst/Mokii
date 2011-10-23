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

Class VBTokenParser
    Inherits BaseParser

    Public Sub New(text As String)
        MyBase.New(text)
    End Sub

    Public Iterator Function GetTokens() As IEnumerable(Of SyntaxToken)
        While Not AtEOF()
            ' Consume any leading trivia
            Dim lead_trivia = ConsumeLeadingTrivia()

            ' Consume the current token
            Dim token As SyntaxToken

            If AtQuote() Then
                token = ConsumeStringLiteral()
            ElseIf AtNumber() Then
                token = ConsumeNumber()
            Else
                token = ConsumeToken()
            End If

            ' Consume any trailing trivia
            Dim trail_trivia = ConsumeTrailingTrivia()

            ' Add the trivia to the token and return it
            token.leading_trivia = lead_trivia
            token.trailing_trivia = trail_trivia

            Yield token

            ' If we're at EOL, go ahead and return an EOL token
            ' Do this here because these tokens should not consume trivia
            If Not AtEOF() AndAlso AtEOL() Then Yield ConsumeEOL()
        End While

        ' We're done, return EOF
        Yield Syntax.Token(SyntaxKind.EndOfFileToken)
    End Function

#Region "At* Functions"
    Private Function AtSingleToken() As Boolean
        Return TokenFacts.IsSingleToken(CurrentChar())
    End Function
#End Region

#Region "Consume Functions"
    Private Function ConsumeEOL() As SyntaxToken
        If AtCRLF() Then
            curr += 2
            Return Syntax.Token(SyntaxKind.EndOfLineTrivia, vbCrLf)
        Else
            Dim token = Syntax.Token(SyntaxKind.EndOfLineTrivia, CurrentChar())
            curr += 1
            Return token
        End If
    End Function

    Private Function ConsumeLeadingTrivia() As SyntaxTriviaList
        Dim trivia_len = 0

        While Not AtEOF()
            If AtTrivia() OrElse AtCR() OrElse AtLF() Then
                trivia_len += 1
                curr += 1
            Else
                If trivia_len > 0 Then
                    Return Syntax.ParseTrivia(Me.text.Substring(curr - trivia_len, trivia_len))
                Else
                    Return SyntaxTriviaList.Empty
                End If
            End If
        End While

        Return SyntaxTriviaList.Empty
    End Function

    Private Function ConsumeNumber() As SyntaxToken
        Dim start = curr

        While Not AtEOF() AndAlso AtNumber()
            curr += 1
        End While

        Return Syntax.Token(SyntaxKind.IntegerLiteralToken, text.Substring(start, curr - start))
    End Function

    Private Function ConsumeToken() As SyntaxToken
        Dim start = curr

        ' If this is a single character token, like "=", we're done
        If AtSingleToken() Then
            curr += 1
            Return CreateToken(text.Substring(start, curr - start))
        End If

        While Not AtEOF()
            ' If we run into another token or EOL or trivia, we're done
            If AtSeparator() OrElse AtOperator() OrElse AtEOL() OrElse AtTrivia() Then
                Return CreateToken(text.Substring(start, curr - start))
            End If

            ' Still part of the token, loop
            curr += 1
        End While

        ' We hit the EOF, return the last token
        Return CreateToken(text.Substring(start, curr - start))
    End Function

    Private Function ConsumeStringLiteral() As SyntaxToken
        Dim start = curr

        ' Consume the open quote
        curr += 1

        ' Consume everything until EOF or a close quote
        While Not AtEOF() AndAlso Not AtQuote()
            curr += 1
        End While

        ' Consume the close quote
        curr += 1

        Dim text = Me.text.Substring(start, curr - start)

        Return Syntax.Token(SyntaxKind.StringLiteralToken, text)
    End Function

    Private Function ConsumeTrailingTrivia() As SyntaxTriviaList
        Dim trivia_len = 0

        While Not AtEOF()
            If AtTrivia() Then
                trivia_len += 1
                curr += 1
            Else
                If trivia_len > 0 Then
                    Return Syntax.ParseTrivia(Me.text.Substring(curr - trivia_len, trivia_len))
                Else
                    Return SyntaxTriviaList.Empty
                End If
            End If
        End While

        Return SyntaxTriviaList.Empty
    End Function
#End Region

    Private Function CreateToken(text As String) As SyntaxToken
        Dim kind As SyntaxKind

        ' Check if this is a known token
        If TokenFacts.TryGetKnownToken(text.ToLowerInvariant(), kind) Then
            Return Syntax.Token(kind, text)
        End If

        ' Must just be an identifier token
        Return Syntax.Token(SyntaxKind.IdentifierToken, text)
    End Function
End Class

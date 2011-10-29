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
        While True
            ' Consume any leading trivia
            Dim lead_trivia = ConsumeLeadingTrivia()

            ' Consume the current token
            Dim token As SyntaxToken

            If AtEOF() Then
                token = Syntax.Token(SyntaxKind.EndOfFileToken)
                token.leading_trivia = lead_trivia
                Yield token
                Return
            ElseIf AtQuote() Then
                token = ConsumeStringLiteral()
            ElseIf AtNumber() Then
                ' This needs to be done before SingleLengthToken to check for period
                token = ConsumeNumber()
            ElseIf AtSingleLengthToken() Then
                token = ConsumeSingleLengthToken()
            ElseIf AtOperator() Then
                token = ConsumeOperator()
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
    End Function

#Region "At* Functions"
    Private Function AtSingleToken() As Boolean
        Return TokenFacts.IsSingleToken(CurrentChar())
    End Function
#End Region

#Region "Consume Functions"
    Private Function ConsumeEOL() As SyntaxToken
        If AtCRLF() Then
            Advance(2)
            Return Syntax.Token(SyntaxKind.StatementTerminatorToken, vbCrLf)
        Else
            Dim token = Syntax.Token(SyntaxKind.StatementTerminatorToken, CurrentChar())
            Advance()
            Return token
        End If
    End Function

    Private Function ConsumeLeadingTrivia() As SyntaxTriviaList
        Dim trivia_len = 0
        Dim in_comment = False

        While Not AtEOF()
            If in_comment AndAlso Not AtEOL() Then
                trivia_len += 1
                Advance()
                Continue While
            ElseIf in_comment AndAlso AtEOL() Then
                in_comment = False
            End If

            If AtComment() Then
                in_comment = True
                trivia_len += 1
                Advance()
                Continue While
            End If

            If AtTrivia() OrElse AtCR() OrElse AtLF() Then
                trivia_len += 1
                Advance()
            Else
                If trivia_len > 0 Then
                    Return Syntax.ParseTrivia(Me.text.Substring(curr - trivia_len, trivia_len))
                Else
                    Return SyntaxTriviaList.Empty
                End If
            End If
        End While

        If trivia_len > 0 Then
            Return Syntax.ParseTrivia(Me.text.Substring(curr - trivia_len, trivia_len))
        Else
            Return SyntaxTriviaList.Empty
        End If
    End Function

    Private Function ConsumeNumber() As SyntaxToken
        Dim start = curr

        While Not AtEOF() AndAlso AtNumber()
            curr += 1
        End While

        Return Syntax.Token(SyntaxKind.IntegerLiteralToken, text.Substring(start, curr - start))
    End Function

    Private Function ConsumeOperator() As SyntaxToken
        ' Store the current character and advance
        Dim oper_string As String = CurrentChar()
        Advance()

        ' Find the next non-whitespace character
        Dim oper_string_2 = FindNextNonWhitespaceChar(curr)
        Dim oper_2 = oper_string & Right(oper_string_2, 1)

        ' Find the third non-whitespace character
        Dim oper_string_3 = FindNextNonWhitespaceChar(curr + oper_string_2.Length)
        Dim oper_3 = oper_2 & Right(oper_string_3, 1)

        Dim all_2 = oper_string & oper_string_2
        Dim all_3 = all_2 & oper_string_3

        ' Check for 3 character operators
        Select Case oper_3
            Case ">>="
                Advance(all_3.Length - 1)
                Return Syntax.Token(SyntaxKind.GreaterThanGreaterThanEqualsToken, all_3)
            Case "<<="
                Advance(all_3.Length - 1)
                Return Syntax.Token(SyntaxKind.LessThanLessThanEqualsToken, all_3)
        End Select

        ' Check for 2 character operators
        Select Case oper_2
            Case ">="
                Advance(all_2.Length - 1)
                Return Syntax.Token(SyntaxKind.GreaterThanEqualsToken, all_2)
            Case ">>"
                Advance(all_2.Length - 1)
                Return Syntax.Token(SyntaxKind.GreaterThanGreaterThanToken, all_2)
            Case "<>"
                Advance(all_2.Length - 1)
                Return Syntax.Token(SyntaxKind.LessThanGreaterThanToken, all_2)
            Case "<="
                Advance(all_2.Length - 1)
                Return Syntax.Token(SyntaxKind.LessThanEqualsToken, all_2)
            Case "<<"
                Advance(all_2.Length - 1)
                Return Syntax.Token(SyntaxKind.LessThanLessThanToken, all_2)
            Case "*="
                Advance(all_2.Length - 1)
                Return Syntax.Token(SyntaxKind.AsteriskEqualsToken, all_2)
            Case "+="
                Advance(all_2.Length - 1)
                Return Syntax.Token(SyntaxKind.PlusEqualsToken, all_2)
            Case "-="
                Advance(all_2.Length - 1)
                Return Syntax.Token(SyntaxKind.MinusEqualsToken, all_2)
            Case "^="
                Advance(all_2.Length - 1)
                Return Syntax.Token(SyntaxKind.CaretEqualsToken, all_2)
            Case "\="
                Advance(all_2.Length - 1)
                Return Syntax.Token(SyntaxKind.BackslashEqualsToken, all_2)
            Case "/="
                Advance(all_2.Length - 1)
                Return Syntax.Token(SyntaxKind.SlashEqualsToken, all_2)
        End Select

        ' Must be a 1 character operator
        Select Case oper_string
            Case ">"
                Return Syntax.Token(SyntaxKind.GreaterThanToken, oper_string)
            Case "<"
                Return Syntax.Token(SyntaxKind.LessThanToken, oper_string)
            Case "*"
                Return Syntax.Token(SyntaxKind.AsteriskToken, oper_string)
            Case "+"
                Return Syntax.Token(SyntaxKind.PlusToken, oper_string)
            Case "-"
                Return Syntax.Token(SyntaxKind.MinusToken, oper_string)
            Case "^"
                Return Syntax.Token(SyntaxKind.CaretToken, oper_string)
            Case "\"
                Return Syntax.Token(SyntaxKind.BackslashToken, oper_string)
            Case "/"
                Return Syntax.Token(SyntaxKind.SlashToken, oper_string)
        End Select

        Return Nothing
    End Function

    Private Function ConsumeSingleLengthToken() As SyntaxToken
        Dim c = CurrentChar()
        Advance()

        Select Case c
            Case ";"c
                Return Syntax.Token(SyntaxKind.SemicolonToken, ";")
            Case ":"c
                Return Syntax.Token(SyntaxKind.ColonToken, ":")
            Case ","c
                Return Syntax.Token(SyntaxKind.CommaToken, ",")
            Case "("c
                Return Syntax.Token(SyntaxKind.OpenParenToken, "(")
            Case ")"c
                Return Syntax.Token(SyntaxKind.CloseParenToken, ")")
            Case "{"c
                Return Syntax.Token(SyntaxKind.OpenBraceToken, "{")
            Case "}"c
                Return Syntax.Token(SyntaxKind.CloseBraceToken, "}")
            Case "="c
                Return Syntax.Token(SyntaxKind.EqualsToken, "=")
            Case "!"c
                Return Syntax.Token(SyntaxKind.ExclamationToken, "!")
            Case "?"c
                Return Syntax.Token(SyntaxKind.QuestionToken, "?")
            Case "."c
                Return Syntax.Token(SyntaxKind.DotToken, ".")
        End Select

        Return Nothing
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
        Dim found_continuation = False

        While Not AtEOF()
            If AtTrivia(True, found_continuation) Then
                If CurrentChar() = "_"c Then found_continuation = True

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
        Dim kind = SyntaxFacts.GetKeywordKind(text.ToLowerInvariant())

        If kind = SyntaxKind.None Then
            ' Must just be an identifier token
            Return Syntax.Token(SyntaxKind.IdentifierToken, text)
        Else
            ' Keyword
            Return Syntax.Token(kind, text)
        End If
    End Function

    Private Function FindNextNonWhitespaceChar(start As Integer) As String
        Dim retval As String = String.Empty
        Dim ptr As Integer = start

        While (ptr < total)
            Dim c = text(ptr)
            retval &= c

            If Not Char.IsWhiteSpace(c) Then Return retval

            ptr += 1
        End While

        Return retval
    End Function
End Class

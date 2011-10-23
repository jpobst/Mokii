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

Class VBTriviaParser
    Inherits BaseParser

    Friend Sub New(text As String)
        MyBase.New(text)
    End Sub

    Public Iterator Function GetTrivia() As IEnumerable(Of SyntaxTrivia)
        While Not AtEOF()
            start_ptr = curr

            If AtEOL() Then
                Yield ConsumeEndOfLine()
            ElseIf AtComment() Then
                Yield ConsumeComment()
            ElseIf AtLineContinuation() Then
                Yield ConsumeLineContinuation()
            Else
                Yield ConsumeWhitespace()
            End If
        End While
    End Function

#Region "Consume Functions"
    ' These functions consume trivia types and return SyntaxTrivia
    Private Function ConsumeComment() As SyntaxTrivia
        ' Comments go until they hit the end of a line
        While Not AtEOL()
            curr += 1
        End While

        Return Syntax.CommentTrivia(text.Substring(start_ptr, curr - start_ptr))
    End Function

    Private Function ConsumeEndOfLine() As SyntaxTrivia
        ' End of Line just returns 1 EOL
        If AtCRLF() Then
            curr += 2
            Return Syntax.EndOfLineTrivia(vbCrLf)
        End If

        If AtCR() Then
            curr += 1
            Return Syntax.EndOfLineTrivia(vbCr)
        End If

        If AtLF() Then
            curr += 1
            Return Syntax.EndOfLineTrivia(vbLf)
        End If

        Throw New ArgumentOutOfRangeException("not eol")
    End Function

    Private Function ConsumeLineContinuation() As SyntaxTrivia
        ' Line continuation consumes whitespace and the EOL after it

        ' Consume whitespace
        While Not AtEOL()
            curr += 1
        End While

        ' Consume EOL
        If AtCRLF() Then
            curr += 2
        Else
            curr += 1
        End If

        Return Syntax.LineContinuationTrivia(text.Substring(start_ptr, curr - start_ptr))
    End Function

    Private Function ConsumeWhitespace() As SyntaxTrivia
        ' Whitespace goes until we hit !whitespace
        While Not AtEOF() AndAlso AtWhitespace()
            curr += 1
        End While

        Return Syntax.WhitespaceTrivia(text.Substring(start_ptr, curr - start_ptr))
    End Function
#End Region

End Class

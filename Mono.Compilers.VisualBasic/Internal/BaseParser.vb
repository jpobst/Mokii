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

Public MustInherit Class BaseParser
    Protected text As String
    Protected total As String
    Protected curr As Integer = 0
    Protected start_ptr As Integer = 0

    Protected Friend Sub New(text As String)
        Me.text = text
        Me.total = text.Length
    End Sub

#Region "Position Functions"
    Protected Sub Advance(Optional count As Integer = 1)
        curr += count
    End Sub

    Protected Function CurrentChar() As Char
        Return text(curr)
    End Function

    Protected Function NextChar() As Char
        If curr + 1 = total Then Return Chr(0)

        Return text(curr + 1)
    End Function

    Protected Function PreviousChar() As Char
        If curr = 0 Then Return Chr(0)

        Return text(curr - 1)
    End Function
#End Region

#Region "At* Functions"
    ' These functions check what the current character is
    Protected Function AtComment() As Boolean
        Return TokenFacts.IsCommentStart(CurrentChar())
    End Function

    Protected Function AtCR() As Boolean
        Return text(curr) = vbCr
    End Function

    Protected Function AtCRLF() As Boolean
        If curr + 1 < total Then
            If (CurrentChar() = vbCr AndAlso text(curr + 1) = vbLf) Then
                Return True
            End If
        End If

        Return False
    End Function

    Protected Function AtEOL() As Boolean
        Return AtCRLF() OrElse TokenFacts.IsLineTerminator(CurrentChar())
    End Function

    Protected Function AtEOF() As Boolean
        Return curr = total
    End Function

    Protected Function AtLF() As Boolean
        Return CurrentChar() = vbLf
    End Function

    Protected Function AtLineContinuation() As Boolean
        Return CurrentChar() = "_" 'AndAlso TokenFacts.IsWhitespace(PreviousChar())
    End Function

    Protected Function AtNumber() As Boolean
        Dim c = CurrentChar()

        If Char.IsNumber(c) Then Return True

        If c = "." AndAlso Char.IsNumber(NextChar()) Then Return True

        Return False
    End Function

    Protected Function AtOperator() As Boolean
        Return TokenFacts.IsOperator(CurrentChar())
    End Function

    Protected Function AtQuote() As Boolean
        Return Asc(CurrentChar()) = 147 OrElse Asc(CurrentChar()) = 148 OrElse Asc(CurrentChar()) = 34
    End Function

    Protected Function AtSeparator() As Boolean
        Return TokenFacts.IsSeparator(CurrentChar())
    End Function

    Protected Function AtSingleLengthToken() As Boolean
        Select Case CurrentChar()
            Case ";"c, ","c, "("c, ")"c, "{"c, "}"c, "="c, "!"c, "?"c, "."c, ":"c
                Return True
        End Select

        Return False
    End Function

    Protected Function AtTrivia(Optional includeContinuation As Boolean = False, Optional foundContinuation As Boolean = False) As Boolean
        If includeContinuation Then
            If CurrentChar() = "_"c Then Return True
            If foundContinuation AndAlso AtEOL() Then Return True
        End If

        Return Char.IsWhiteSpace(CurrentChar()) AndAlso Asc(CurrentChar()) <> 13 AndAlso Asc(CurrentChar()) <> 10
    End Function

    Protected Function AtWhitespace() As Boolean
        Return CurrentChar() = " " OrElse CurrentChar() = vbTab
    End Function
#End Region
End Class

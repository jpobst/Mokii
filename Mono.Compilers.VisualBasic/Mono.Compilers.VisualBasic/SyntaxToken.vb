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

Imports System.Text
Imports System.Linq

Public Structure SyntaxToken
    Dim leading_trivia As SyntaxTriviaList
    Dim trailing_trivia As SyntaxTriviaList

    Public Property Kind As SyntaxKind
    Public Property FullWidth As Integer
    Public Property IsBracketed As Boolean
    Public Property Value As Object
    Public Property ValueText As String

    Public ReadOnly Property LeadingTrivia As SyntaxTriviaList
        Get
            Return leading_trivia
        End Get
    End Property

    Public ReadOnly Property TrailingTrivia As SyntaxTriviaList
        Get
            Return trailing_trivia
        End Get
    End Property

    Friend Sub New(leading As SyntaxTriviaList, kind As SyntaxKind, trailing As SyntaxTriviaList, value As String)
        Me.Kind = kind
        leading_trivia = leading
        trailing_trivia = trailing

        If (value IsNot Nothing AndAlso value.StartsWith("[") AndAlso value.EndsWith("]")) Then
            IsBracketed = True
            value = value.Substring(1, value.Length - 2)
        End If

        If (value IsNot Nothing AndAlso kind = SyntaxKind.StringLiteralToken) Then
            value = value.Substring(1, value.Length - 2)
        End If

        ValueText = value
    End Sub

    Public Overrides Function ToString() As String
        Dim sb As New StringBuilder()

        For Each lt In LeadingTrivia
            sb.Append(lt)
        Next

        If IsBracketed Then
            sb.AppendFormat("[{0}]", ValueText)
        ElseIf Kind = SyntaxKind.StringLiteralToken Then
            sb.AppendFormat("""{0}""", ValueText)
        Else
            sb.Append(ValueText)
        End If

        For Each tt In TrailingTrivia
            sb.Append(tt)
        Next

        Return sb.ToString()
    End Function

    Public ReadOnly Property LeadingWidth As Integer
        Get
            Dim width = 0

            For Each lt In LeadingTrivia
                width += lt.FullWidth
            Next

            Return width
        End Get
    End Property

End Structure

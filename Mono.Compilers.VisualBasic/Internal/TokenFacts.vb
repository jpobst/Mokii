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

Module TokenFacts
    Private single_tokens As Char() = New Char() {"(", ")", ".", "="}
    Private separators As Char() = New Char() {"(", ")", "{", "}", "!", "#", ",", ".", ":"}
    Private operators As Char() = New Char() {"&", "*", "+", "-", "<", ">", "^", "/", "\", "="}

    Private known_tokens As Dictionary(Of String, SyntaxKind) = New Dictionary(Of String, SyntaxKind)()

    Sub New()
        known_tokens.Add("as", SyntaxKind.AsKeyword)
        known_tokens.Add("imports", SyntaxKind.ImportsKeyword)
        known_tokens.Add(".", SyntaxKind.DotToken)
        known_tokens.Add("namespace", SyntaxKind.NamespaceKeyword)
        known_tokens.Add("module", SyntaxKind.ModuleKeyword)
        known_tokens.Add("sub", SyntaxKind.SubKeyword)
        known_tokens.Add("(", SyntaxKind.OpenParenToken)
        known_tokens.Add("string", SyntaxKind.StringKeyword)
        known_tokens.Add(")", SyntaxKind.CloseParenToken)
        known_tokens.Add("dim", SyntaxKind.DimKeyword)
        known_tokens.Add("integer", SyntaxKind.IntegerKeyword)
        known_tokens.Add("if", SyntaxKind.IfKeyword)
        known_tokens.Add("=", SyntaxKind.EqualsToken)
        known_tokens.Add("then", SyntaxKind.ThenKeyword)
        known_tokens.Add("end", SyntaxKind.EndKeyword)
        known_tokens.Add(vbCrLf, SyntaxKind.StatementTerminatorToken)
        known_tokens.Add("text", SyntaxKind.TextKeyword)
    End Sub

    Public Function IsCommentStart(text As String) As Boolean
        Select Case text.ToLowerInvariant()
            Case "'", ChrW(&H2018), ChrW(&H2019), "rem"
                Return True
        End Select

        Return False
    End Function

    Public Function IsLineTerminator(text As String) As Boolean
        Select Case text
            Case vbCr, vbLf, vbCrLf, ChrW(&H2028), ChrW(&H2029)
                Return True
        End Select

        Return False
    End Function

    Public Function IsSingleToken(text As String) As Boolean
        Return single_tokens.Contains(text)
    End Function

    Public Function IsSeparator(text As String) As Boolean
        Return separators.Contains(text)
    End Function

    Public Function IsOperator(text As String) As Boolean
        Return operators.Contains(text)
    End Function

    Public Function IsWhitespace(text As String) As Boolean
        Return text = vbTab OrElse Char.GetUnicodeCategory(text) = Globalization.UnicodeCategory.SpaceSeparator
    End Function

    Public Function TryGetKnownToken(text As String, ByRef kind As SyntaxKind) As Boolean
        Return known_tokens.TryGetValue(text, kind)
    End Function
End Module

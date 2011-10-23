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

Public Structure SyntaxTokenList
    Friend list As IEnumerable(Of SyntaxToken)

    Friend Sub New(list As IEnumerable(Of SyntaxToken))
        Me.list = list.ToList()
    End Sub

    Public Function GetEnumerator() As SyntaxTokenList.Enumerator
        Return New SyntaxTokenList.Enumerator(list)
    End Function

    Public Structure Enumerator
        Implements IEnumerator(Of SyntaxToken)

        Private enumerator As IEnumerator(Of SyntaxToken)

        Friend Sub New(list As IEnumerable(Of SyntaxToken))
            Me.enumerator = list.GetEnumerator()
        End Sub

        Public ReadOnly Property Current As SyntaxToken Implements System.Collections.Generic.IEnumerator(Of SyntaxToken).Current
            Get
                Return enumerator.Current
            End Get
        End Property

        Public ReadOnly Property Current1 As Object Implements System.Collections.IEnumerator.Current
            Get
                Return enumerator.Current
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            Return enumerator.MoveNext
        End Function

        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            enumerator.Reset()
        End Sub

        Public Sub Dispose() Implements System.IDisposable.Dispose
            enumerator.Dispose()
        End Sub
    End Structure
End Structure

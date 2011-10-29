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

Imports System.Linq

Public NotInheritable Class SyntaxFacts
    Private Shared contextual_lookup As Dictionary(Of String, SyntaxKind)
    Private Shared keyword_lookup As Dictionary(Of String, SyntaxKind)

    Shared Sub New()
        contextual_lookup = New Dictionary(Of String, SyntaxKind)
        keyword_lookup = New Dictionary(Of String, SyntaxKind)

        For Each keyword In contextual_keywords
            contextual_lookup.Add(Left(keyword.ToString().ToLowerInvariant(), keyword.ToString().Length - 7), keyword)
        Next

        For Each keyword In GetKeywordKinds()
            keyword_lookup.Add(Left(keyword.ToString().ToLowerInvariant(), keyword.ToString().Length - 7), keyword)
        Next
    End Sub

    Public Shared Function GetContextualKeywordKind(text As String) As SyntaxKind
        Dim kind As SyntaxKind

        If contextual_lookup.TryGetValue(text, kind) Then
            Return kind
        End If

        Return SyntaxKind.None
    End Function

    Public Shared Function GetContextualKeywordKinds() As IEnumerable(Of SyntaxKind)
        Return contextual_keywords
    End Function

    Public Shared Function GetKeywordKind(text As String) As SyntaxKind
        Dim kind As SyntaxKind

        If keyword_lookup.TryGetValue(text, kind) Then
            Return kind
        End If

        Return SyntaxKind.None
    End Function

    Public Shared Function GetKeywordKinds() As IEnumerable(Of SyntaxKind)
        Return reserved_keywords.Union(contextual_keywords)
    End Function

    Public Shared Function GetReservedKeywordKinds() As IEnumerable(Of SyntaxKind)
        Return reserved_keywords
    End Function

    Public Shared Function GetPreprocessorKeywordKinds() As IEnumerable(Of SyntaxKind)
        Return preprocessor_keywords
    End Function

    Public Shared Function GetPunctuationKinds() As IEnumerable(Of SyntaxKind)
        Return punctuation_tokens
    End Function

    Public Shared Function IsContextualKeyword(kind As SyntaxKind) As Boolean
        Return contextual_keywords.Contains(kind)
    End Function

    Public Shared Function IsKeyword(token As SyntaxToken) As Boolean
        Select Case token.Kind
            Case SyntaxKind.ImportsKeyword,
            SyntaxKind.NamespaceKeyword,
            SyntaxKind.ModuleKeyword,
            SyntaxKind.SubKeyword,
            SyntaxKind.AsKeyword,
            SyntaxKind.DimKeyword,
            SyntaxKind.IfKeyword,
            SyntaxKind.ThenKeyword,
            SyntaxKind.EndKeyword
                Return True
        End Select

        Return False
    End Function

    Private Shared ReadOnly reserved_keywords As SyntaxKind() = New SyntaxKind() {
        SyntaxKind.AddHandlerKeyword,
        SyntaxKind.AddressOfKeyword,
        SyntaxKind.AliasKeyword,
        SyntaxKind.AndKeyword,
        SyntaxKind.AndAlsoKeyword,
        SyntaxKind.AsKeyword,
        SyntaxKind.BooleanKeyword,
        SyntaxKind.ByRefKeyword,
        SyntaxKind.ByteKeyword,
        SyntaxKind.ByValKeyword,
        SyntaxKind.CallKeyword,
        SyntaxKind.CaseKeyword,
        SyntaxKind.CatchKeyword,
        SyntaxKind.CBoolKeyword,
        SyntaxKind.CByteKeyword,
        SyntaxKind.CCharKeyword,
        SyntaxKind.CDateKeyword,
        SyntaxKind.CDecKeyword,
        SyntaxKind.CDblKeyword,
        SyntaxKind.CharKeyword,
        SyntaxKind.CIntKeyword,
        SyntaxKind.ClassKeyword,
        SyntaxKind.CLngKeyword,
        SyntaxKind.CObjKeyword,
        SyntaxKind.ConstKeyword,
        SyntaxKind.ContinueKeyword,
        SyntaxKind.CSByteKeyword,
        SyntaxKind.CShortKeyword,
        SyntaxKind.CSngKeyword,
        SyntaxKind.CStrKeyword,
        SyntaxKind.CTypeKeyword,
        SyntaxKind.CUIntKeyword,
        SyntaxKind.CULngKeyword,
        SyntaxKind.CUShortKeyword,
        SyntaxKind.DateKeyword,
        SyntaxKind.DecimalKeyword,
        SyntaxKind.DeclareKeyword,
        SyntaxKind.DefaultKeyword,
        SyntaxKind.DelegateKeyword,
        SyntaxKind.DimKeyword,
        SyntaxKind.DirectCastKeyword,
        SyntaxKind.DoKeyword,
        SyntaxKind.DoubleKeyword,
        SyntaxKind.EachKeyword,
        SyntaxKind.ElseKeyword,
        SyntaxKind.ElseIfKeyword,
        SyntaxKind.EndKeyword,
        SyntaxKind.EnumKeyword,
        SyntaxKind.EraseKeyword,
        SyntaxKind.ErrorKeyword,
        SyntaxKind.EventKeyword,
        SyntaxKind.ExitKeyword,
        SyntaxKind.FalseKeyword,
        SyntaxKind.FinallyKeyword,
        SyntaxKind.ForKeyword,
        SyntaxKind.FriendKeyword,
        SyntaxKind.FunctionKeyword,
        SyntaxKind.GetKeyword,
        SyntaxKind.GetTypeKeyword,
        SyntaxKind.GetXmlNamespaceKeyword,
        SyntaxKind.GlobalKeyword,
        SyntaxKind.GoToKeyword,
        SyntaxKind.HandlesKeyword,
        SyntaxKind.IfKeyword,
        SyntaxKind.ImplementsKeyword,
        SyntaxKind.ImportsKeyword,
        SyntaxKind.InKeyword,
        SyntaxKind.InheritsKeyword,
        SyntaxKind.IntegerKeyword,
        SyntaxKind.InterfaceKeyword,
        SyntaxKind.IsKeyword,
        SyntaxKind.IsNotKeyword,
        SyntaxKind.LetKeyword,
        SyntaxKind.LibKeyword,
        SyntaxKind.LikeKeyword,
        SyntaxKind.LongKeyword,
        SyntaxKind.LoopKeyword,
        SyntaxKind.MeKeyword,
        SyntaxKind.ModKeyword,
        SyntaxKind.ModuleKeyword,
        SyntaxKind.MustInheritKeyword,
        SyntaxKind.MustOverrideKeyword,
        SyntaxKind.MyBaseKeyword,
        SyntaxKind.MyClassKeyword,
        SyntaxKind.NamespaceKeyword,
        SyntaxKind.NarrowingKeyword,
        SyntaxKind.NextKeyword,
        SyntaxKind.NewKeyword,
        SyntaxKind.NotKeyword,
        SyntaxKind.NothingKeyword,
        SyntaxKind.NotInheritableKeyword,
        SyntaxKind.NotOverridableKeyword,
        SyntaxKind.ObjectKeyword,
        SyntaxKind.OfKeyword,
        SyntaxKind.OnKeyword,
        SyntaxKind.OperatorKeyword,
        SyntaxKind.OptionKeyword,
        SyntaxKind.OptionalKeyword,
        SyntaxKind.OrKeyword,
        SyntaxKind.OrElseKeyword,
        SyntaxKind.OverloadsKeyword,
        SyntaxKind.OverridableKeyword,
        SyntaxKind.OverridesKeyword,
        SyntaxKind.ParamArrayKeyword,
        SyntaxKind.PartialKeyword,
        SyntaxKind.PrivateKeyword,
        SyntaxKind.PropertyKeyword,
        SyntaxKind.ProtectedKeyword,
        SyntaxKind.PublicKeyword,
        SyntaxKind.RaiseEventKeyword,
        SyntaxKind.ReadOnlyKeyword,
        SyntaxKind.ReDimKeyword,
        SyntaxKind.REMKeyword,
        SyntaxKind.RemoveHandlerKeyword,
        SyntaxKind.ResumeKeyword,
        SyntaxKind.ReturnKeyword,
        SyntaxKind.SByteKeyword,
        SyntaxKind.SelectKeyword,
        SyntaxKind.SetKeyword,
        SyntaxKind.ShadowsKeyword,
        SyntaxKind.SharedKeyword,
        SyntaxKind.ShortKeyword,
        SyntaxKind.SingleKeyword,
        SyntaxKind.StaticKeyword,
        SyntaxKind.StepKeyword,
        SyntaxKind.StopKeyword,
        SyntaxKind.StringKeyword,
        SyntaxKind.StructureKeyword,
        SyntaxKind.SubKeyword,
        SyntaxKind.SyncLockKeyword,
        SyntaxKind.ThenKeyword,
        SyntaxKind.ThrowKeyword,
        SyntaxKind.ToKeyword,
        SyntaxKind.TrueKeyword,
        SyntaxKind.TryKeyword,
        SyntaxKind.TryCastKeyword,
        SyntaxKind.TypeOfKeyword,
        SyntaxKind.UIntegerKeyword,
        SyntaxKind.ULongKeyword,
        SyntaxKind.UShortKeyword,
        SyntaxKind.UsingKeyword,
        SyntaxKind.WhenKeyword,
        SyntaxKind.WhileKeyword,
        SyntaxKind.WideningKeyword,
        SyntaxKind.WithKeyword,
        SyntaxKind.WithEventsKeyword,
        SyntaxKind.WriteOnlyKeyword,
        SyntaxKind.XorKeyword,
        SyntaxKind.EndIfKeyword,
        SyntaxKind.GosubKeyword,
        SyntaxKind.VariantKeyword,
        SyntaxKind.WendKeyword
}

    Private Shared ReadOnly contextual_keywords As SyntaxKind() = New SyntaxKind() {
        SyntaxKind.AggregateKeyword,
        SyntaxKind.AllKeyword,
        SyntaxKind.AnsiKeyword,
        SyntaxKind.AscendingKeyword,
        SyntaxKind.AssemblyKeyword,
        SyntaxKind.AutoKeyword,
        SyntaxKind.BinaryKeyword,
        SyntaxKind.ByKeyword,
        SyntaxKind.CompareKeyword,
        SyntaxKind.CustomKeyword,
        SyntaxKind.DescendingKeyword,
        SyntaxKind.DistinctKeyword,
        SyntaxKind.EqualsKeyword,
        SyntaxKind.ExplicitKeyword,
        SyntaxKind.ExternalSourceKeyword,
        SyntaxKind.ExternalChecksumKeyword,
        SyntaxKind.FromKeyword,
        SyntaxKind.GroupKeyword,
        SyntaxKind.InferKeyword,
        SyntaxKind.IntoKeyword,
        SyntaxKind.IsFalseKeyword,
        SyntaxKind.IsTrueKeyword,
        SyntaxKind.JoinKeyword,
        SyntaxKind.KeyKeyword,
        SyntaxKind.MidKeyword,
        SyntaxKind.OffKeyword,
        SyntaxKind.OrderKeyword,
        SyntaxKind.OutKeyword,
        SyntaxKind.PreserveKeyword,
        SyntaxKind.RegionKeyword,
        SyntaxKind.SkipKeyword,
        SyntaxKind.StrictKeyword,
        SyntaxKind.TakeKeyword,
        SyntaxKind.TextKeyword,
        SyntaxKind.UnicodeKeyword,
        SyntaxKind.UntilKeyword,
        SyntaxKind.WhereKeyword,
        SyntaxKind.TypeKeyword,
        SyntaxKind.XmlKeyword
}

    Private Shared ReadOnly preprocessor_keywords As SyntaxKind() = New SyntaxKind() {
        SyntaxKind.IfKeyword,
        SyntaxKind.ThenKeyword,
        SyntaxKind.ElseIfKeyword,
        SyntaxKind.ElseKeyword,
        SyntaxKind.EndKeyword,
        SyntaxKind.RegionKeyword,
        SyntaxKind.ConstKeyword,
        SyntaxKind.ExternalSourceKeyword,
        SyntaxKind.ExternalChecksumKeyword
}

    Private Shared ReadOnly punctuation_tokens As SyntaxKind() = New SyntaxKind() {
        SyntaxKind.ExclamationToken,
        SyntaxKind.AtToken,
        SyntaxKind.CommaToken,
        SyntaxKind.HashToken,
        SyntaxKind.AmpersandToken,
        SyntaxKind.SingleQuoteToken,
        SyntaxKind.OpenParenToken,
        SyntaxKind.CloseParenToken,
        SyntaxKind.OpenBraceToken,
        SyntaxKind.CloseBraceToken,
        SyntaxKind.SemicolonToken,
        SyntaxKind.AsteriskToken,
        SyntaxKind.PlusToken,
        SyntaxKind.MinusToken,
        SyntaxKind.DotToken,
        SyntaxKind.SlashToken,
        SyntaxKind.ColonToken,
        SyntaxKind.LessThanToken,
        SyntaxKind.LessThanEqualsToken,
        SyntaxKind.LessThanGreaterThanToken,
        SyntaxKind.EqualsToken,
        SyntaxKind.GreaterThanToken,
        SyntaxKind.GreaterThanEqualsToken,
        SyntaxKind.BackslashToken,
        SyntaxKind.CaretToken,
        SyntaxKind.ColonEqualsToken,
        SyntaxKind.AmpersandEqualsToken,
        SyntaxKind.AsteriskEqualsToken,
        SyntaxKind.PlusEqualsToken,
        SyntaxKind.MinusEqualsToken,
        SyntaxKind.SlashEqualsToken,
        SyntaxKind.BackslashEqualsToken,
        SyntaxKind.CaretEqualsToken,
        SyntaxKind.LessThanLessThanToken,
        SyntaxKind.GreaterThanGreaterThanToken,
        SyntaxKind.LessThanLessThanEqualsToken,
        SyntaxKind.GreaterThanGreaterThanEqualsToken,
        SyntaxKind.QuestionToken,
        SyntaxKind.DoubleQuoteToken,
        SyntaxKind.StatementTerminatorToken,
        SyntaxKind.EndOfFileToken,
        SyntaxKind.EmptyToken
}
End Class

Option Infer On

Enum B As Byte
    A
    B
End Enum
Enum SB As SByte
    A
    B
End Enum
Enum S As Short
    A
    B
End Enum
Enum US As UShort
    A
    B
End Enum
Enum I As Integer
    A
    B
End Enum
Enum UI As UInteger
    A
    B
End Enum
Enum L As Long
    A
    B
End Enum
Enum UL As ULong
    A
    B
End Enum

Class InferFor1
    Private Shared Errors As Integer

    Public Shared Function Main() As Integer
        'same type
        For i = CByte(1) To CByte(2)
        Next
        For i = CSByte(1) To CSByte(2) Step CSByte(1)
        Next
        For i = 1S To 2S
        Next
        For i = 1US To 2US Step 1US
        Next
        For i = 1I To 2I
        Next
        For i = 1UI To 2UI
        Next
        For i = 1L To 2L Step 1L
        Next
        For i = 1UL To 2UL
        Next
        For i = 1D To 2D
        Next
        For i = 1.0F To 2.0F Step 0.5F
        Next
        For i = 1.0R To 2.0R
        Next
        'different types
        For i = CByte(1) To CSByte(2) Step 1S
        Next
        For i = 1UI To 1I Step 1L
        Next
        For i = 1L To 1I Step 1UI
        Next
        For i = 1UI To 1L Step 1I
        Next
        'enums
        For i = B.a To B.b
        Next
        For i = sb.a To sb.b
        Next
        For i = s.a To s.b
        Next
        For i = us.a To us.b
        Next
        For ii = i.a To i.b
        Next
        For i = ui.a To ui.b
        Next
        For i = l.a To l.b
        Next
        For i = ul.a To ul.b
        Next
        'integral + enum
        For i = ui.a To 2UI
            VerifyType(i.GetType(), GetType(UInteger), "ui.a to 2UI")
        Next
        For i = 1UI To ui.b
            VerifyType(i.GetType(), GetType(UInteger), "1UI to ui.b")
        Next

        Return Errors
    End Function

    Shared Sub VerifyType(ByVal Actual As Type, ByVal Expected As Type, ByVal Message As String)
        If Actual Is Expected Then Return
        Errors += 1
        Console.WriteLine("Expected '{0}' Got '{1}': {2}", Expected.FullName, Actual.FullName, Message)
    End Sub

End Class
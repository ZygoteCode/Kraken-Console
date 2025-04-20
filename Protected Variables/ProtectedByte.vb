Public Class ProtectedByte
    Dim protectedValue As Decimal = 0
    Dim encryptionKey As Integer = 0
    Dim rnd As Random = New Random()
    Public Sub New(ByVal valueToProtect As Byte)
        encryptionKey = valueToProtect.ToString().Length + rnd.Next(2, 5)
        protectedValue = ProtectByte(valueToProtect)
    End Sub
    Private Function ProtectByte(ByVal valueToProtect As Integer) As Decimal
        Dim theProtected As Decimal = 0
        theProtected = (valueToProtect * 3) - (50 * encryptionKey)
        Return theProtected
    End Function
    Private Function UnprotectByte(ByVal valueToUnprotect As Decimal) As Byte
        Dim theUnprotected As Byte = 0
        Dim coso As Decimal = (valueToUnprotect + (50 * encryptionKey)) / 3
        theUnprotected = Byte.Parse(coso)
        Return theUnprotected
    End Function
    Public Function GetValue() As Byte
        Return UnprotectByte(protectedValue)
    End Function
    Public Sub SetValue(ByVal valueToProtect As Byte)
        protectedValue = ProtectByte(valueToProtect)
    End Sub
    Public Sub Dispose()
        protectedValue = Nothing
        encryptionKey = Nothing
        rnd = Nothing
        GC.Collect()
        GC.SuppressFinalize(protectedValue)
        GC.SuppressFinalize(encryptionKey)
        GC.SuppressFinalize(rnd)
    End Sub
End Class
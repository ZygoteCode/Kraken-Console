Public Class ProtectedDecimal
    Dim protectedValue As Decimal = 0
    Dim encryptionKey As Integer = 0
    Dim rnd As Random = New Random()
    Public Sub New(ByVal valueToProtect As Decimal)
        encryptionKey = valueToProtect.ToString().Length + rnd.Next(2, 5)
        protectedValue = ProtectDecimal(valueToProtect)
    End Sub
    Private Function ProtectDecimal(ByVal valueToProtect As Integer) As Decimal
        Dim theProtected As Decimal = 0
        theProtected = (valueToProtect * 3) - (50 * encryptionKey)
        Return theProtected
    End Function
    Private Function UnprotectDecimal(ByVal valueToUnprotect As Decimal) As Decimal
        Dim theUnprotected As Decimal = 0
        Dim coso As Decimal = (valueToUnprotect + (50 * encryptionKey)) / 3
        theUnprotected = Decimal.Parse(coso)
        Return theUnprotected
    End Function
    Public Function GetValue() As Decimal
        Return UnprotectDecimal(protectedValue)
    End Function
    Public Sub SetValue(ByVal valueToProtect As Decimal)
        protectedValue = ProtectDecimal(valueToProtect)
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
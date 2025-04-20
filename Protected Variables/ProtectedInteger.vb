Public Class ProtectedInteger
    Dim protectedValue As Decimal = 0
    Dim encryptionKey As Integer = 0
    Dim rnd As Random = New Random()
    Public Sub New(ByVal valueToProtect As Integer)
        encryptionKey = valueToProtect.ToString().Length + rnd.Next(2, 5)
        protectedValue = ProtectInteger(valueToProtect)
    End Sub
    Private Function ProtectInteger(ByVal valueToProtect As Integer) As Decimal
        Dim theProtected As Decimal = 0
        theProtected = (valueToProtect * 3) - (50 * encryptionKey)
        Return theProtected
    End Function
    Private Function UnprotectInteger(ByVal valueToUnprotect As Decimal) As Integer
        Dim theUnprotected As Integer = 0
        Dim coso As Decimal = (valueToUnprotect + (50 * encryptionKey)) / 3
        theUnprotected = Integer.Parse(coso)
        Return theUnprotected
    End Function
    Public Function GetValue() As Integer
        Return UnprotectInteger(protectedValue)
    End Function
    Public Sub SetValue(ByVal valueToProtect As Integer)
        protectedValue = ProtectInteger(valueToProtect)
    End Sub
    Public Sub Dispose()
        protectedValue = Nothing
        encryptionKey = Nothing
        rnd = Nothing
        GC.Collect()
        GC.SuppressFinalize(protectedValue)
        GC.SuppressFinalize(encryptionKey)
    End Sub
End Class
Public Class ProtectedByteArray
    Dim protectedValue As Byte()
    Public Sub New(ByVal valueToProtect As Byte())
        protectedValue = New Byte() {}
        protectedValue = ProtectByteArray(valueToProtect)
    End Sub
    Private Function ProtectByteArray(ByVal valueToProtect As Byte()) As Byte()
        Dim protectedThing As Byte() = New Byte() {}
        Array.Reverse(protectedThing)
        Return protectedThing
    End Function
    Private Function UnprotectByteArray(ByVal valueToUnprotect As Byte()) As Byte()
        Dim unprotectedThing As Byte() = New Byte() {}
        Array.Reverse(unprotectedThing)
        Return unprotectedThing
    End Function
    Public Function GetValue() As Byte()
        Return UnprotectByteArray(protectedValue)
    End Function
    Public Sub SetValue(ByVal valueToProtect As Byte())
        protectedValue = ProtectByteArray(valueToProtect)
    End Sub
    Public Sub Dispose()
        protectedValue = Nothing
        GC.Collect()
        GC.SuppressFinalize(protectedValue)
    End Sub
End Class
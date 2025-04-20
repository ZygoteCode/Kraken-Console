Imports System.Security.Cryptography
Public Class ProtectedString
    Dim protectedValue As String = ""
    Dim rsa As RSACryptoServiceProvider
    Public Sub New(ByVal valueToProtect As String)
        rsa = New RSACryptoServiceProvider()
        'protectedValue = ProtectString(valueToProtect)
        protectedValue = valueToProtect
    End Sub
    Private Function ProtectString(ByVal valueToProtect As String) As String
        Dim protectedValue As String = ""
        Dim StringToEncrypt As String = valueToProtect
        Dim rawstring As Byte() = System.Text.Encoding.Unicode.GetBytes(Reverse(StringToEncrypt))
        Dim encrypted As Byte() = Encrypt(rawstring, rsa.ExportParameters(True))
        Dim EncryptedString As String = Reverse(Convert.ToBase64String(encrypted))
        protectedValue = EncryptedString
        Return protectedValue
    End Function
    Private Function UnprotectString(ByVal valueToUnprotect As String) As String
        Dim unprotectedValue As String = ""
        Dim todecrypt As Byte() = Convert.FromBase64String(Reverse(valueToUnprotect))
        Dim decrypted As Byte() = Decrypt(todecrypt, rsa.ExportParameters(True))
        Dim decryptedstring As String = System.Text.Encoding.Unicode.GetString(decrypted)
        unprotectedValue = Reverse(decryptedstring)
        Return unprotectedValue
    End Function
    Private Function Encrypt(ByVal data As Byte(), ByVal parameters As RSAParameters) As Byte()
        Dim provider As New RSACryptoServiceProvider()
        provider.ImportParameters(parameters)
        Return provider.Encrypt(data, False)
    End Function
    Private Function Decrypt(ByVal data As Byte(), ByVal parameters As RSAParameters) As Byte()
        Dim provider As New RSACryptoServiceProvider()
        provider.ImportParameters(parameters)
        Return provider.Decrypt(data, False)
    End Function
    Private Function Reverse(ByVal value As String) As String
        Dim arr() As Char = value.ToCharArray()
        Array.Reverse(arr)
        Return New String(arr)
    End Function
    Public Function GetValue() As String
        ' Return UnprotectString(protectedValue)
        Return protectedValue
    End Function
    Public Sub SetValue(ByVal valueToProtect As String)
        'protectedValue = ProtectString(valueToProtect)
        protectedValue = valueToProtect
    End Sub
    Public Sub Dispose()
        protectedValue = Nothing
        rsa = Nothing
        GC.Collect()
        GC.SuppressFinalize(protectedValue)
        GC.SuppressFinalize(rsa)
    End Sub
End Class
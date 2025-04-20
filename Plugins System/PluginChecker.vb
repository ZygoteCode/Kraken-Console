Public Class PluginChecker
    Public Function CheckPluginIntegrity(ByVal fileName As String) As Boolean
        Dim readData As String = System.IO.File.ReadAllText(fileName)
        Dim readLines As New TextBox With {.Text = readData}

        Return True
    End Function
End Class
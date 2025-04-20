Public Class BufferedWebBrowser
    Inherits WebBrowser
    Public Sub New()
        Me.DoubleBuffered = True
    End Sub
End Class
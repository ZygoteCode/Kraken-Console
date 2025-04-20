Public Class BufferedTextBox
    Inherits TextBox
    Public Sub New()
        Me.DoubleBuffered = True
        Me.CheckForIllegalCrossThreadCalls = False
    End Sub
End Class
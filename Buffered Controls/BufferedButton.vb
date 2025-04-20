Public Class BufferedButton
    Inherits Button
    Public Sub New()
        Me.DoubleBuffered = True
        Me.CheckForIllegalCrossThreadCalls = False
    End Sub
End Class
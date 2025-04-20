Public Class BufferedTabControl
    Inherits TabControl
    Public Sub New()
        Me.DoubleBuffered = True
        Me.CheckForIllegalCrossThreadCalls = False
    End Sub
End Class
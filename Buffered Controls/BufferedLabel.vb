Public Class BufferedLabel
    Inherits Label
    Public Sub New()
        Me.CheckForIllegalCrossThreadCalls = False
        Me.DoubleBuffered = True
    End Sub
End Class
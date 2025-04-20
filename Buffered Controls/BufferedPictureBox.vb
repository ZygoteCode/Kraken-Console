Public Class BufferedPictureBox
    Inherits PictureBox
    Public Sub New()
        Me.DoubleBuffered = True
        Me.CheckForIllegalCrossThreadCalls = False
    End Sub
End Class
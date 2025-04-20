Public Class BufferedRichTextBox
    Inherits RichTextBox
    Public Sub New()
        Me.DoubleBuffered = True
        Me.CheckForIllegalCrossThreadCalls = False
    End Sub
End Class
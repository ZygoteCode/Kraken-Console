Public Class DataAdder
    Private Shared emptyChar As ProtectedString = New ProtectedString("")
    Private Shared buffered As ProtectedString = New ProtectedString("BufferedRichTextBox")
    Private Shared newLine As ProtectedString = New ProtectedString(vbNewLine)
    Public Shared htmlPage As String = ""
    Public Shared htmlFile As String = ""
    Public Shared asmCode As String = ""
    Public Shared asmFile As String = ""
    Public Shared Sub AddData(ByVal Data As String)
        Data = "[" + DateTime.Now.ToLongTimeString() + "] " + Data
        Try
            For Each control As Control In KrakenConsole.ActiveForm.Controls
                If control.Name.StartsWith(buffered.GetValue()) Then
                    Dim bufferedRichTextBox As BufferedRichTextBox = DirectCast(control, BufferedRichTextBox)
                    If bufferedRichTextBox.Text = emptyChar.GetValue() Then
                        bufferedRichTextBox.Text = Data
                    Else
                        bufferedRichTextBox.Text += newLine.GetValue() + Data
                    End If
                    bufferedRichTextBox.ScrollToCaret()
                    bufferedRichTextBox.SelectionStart = bufferedRichTextBox.Text.Length
                End If
            Next
        Catch ex As Exception
            Application.Exit()
        End Try
    End Sub
    Public Shared Sub Clear()
        Try
            For Each control As Control In KrakenConsole.ActiveForm.Controls
                If control.Name.StartsWith(buffered.GetValue()) Then
                    Dim bufferedRichTextBox As BufferedRichTextBox = DirectCast(control, BufferedRichTextBox)
                    bufferedRichTextBox.Text = emptyChar.GetValue()
                End If
            Next
        Catch ex As Exception
            Application.Exit()
        End Try
    End Sub
End Class
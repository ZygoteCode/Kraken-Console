Public Class HTMLDebug
    Private Sub HTMLDebug_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If DataAdder.htmlPage = "" Then
            WebBrowser1.DocumentText = RichTextBox1.Text
        Else
            WebBrowser1.DocumentText = DataAdder.htmlPage
            RichTextBox1.Text = DataAdder.htmlPage
        End If
    End Sub
    Private Sub WebBrowser1_DocumentTitleChanged(sender As Object, e As EventArgs) Handles WebBrowser1.DocumentTitleChanged
        If WebBrowser1.DocumentTitle = "" Then
            Me.Text = "Kraken Console - HTML Debug - No title"
        Else
            Me.Text = "Kraken Console - HTML Debug - " + WebBrowser1.DocumentTitle
        End If
    End Sub
    Private Sub HTMLDebug_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Not DataAdder.htmlFile = "" Then
            If MessageBox.Show("Do you wanna save the changes to that file?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                System.IO.File.WriteAllText(DataAdder.htmlFile, RichTextBox1.Text)
            End If
        End If
    End Sub
    Private Sub RichTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles RichTextBox1.KeyDown
        If e.KeyValue = Keys.F5 Then
            WebBrowser1.DocumentText = RichTextBox1.Text
        End If
    End Sub
End Class
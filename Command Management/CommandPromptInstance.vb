Public Class CommandPromptInstance
    Dim WithEvents process As Process
    Public Sub New()
        process = New Process()
        process.StartInfo.FileName = "cmd.exe"
        process.StartInfo.UseShellExecute = False
        process.StartInfo.RedirectStandardOutput = True
        process.StartInfo.RedirectStandardInput = True
        process.StartInfo.CreateNoWindow = True
        process.StartInfo.Verb = "runas"
    End Sub
    Public Sub Spawn()
        process.Start()
        process.PriorityClass = ProcessPriorityClass.High
        process.EnableRaisingEvents = True
        process.BeginOutputReadLine()
    End Sub
    Private Sub process_OutputDataReceived(sender As Object, e As DataReceivedEventArgs) Handles process.OutputDataReceived
        DataAdder.AddData(e.Data)
    End Sub
    Public Function GetProcess() As Process
        Return process
    End Function
End Class
Public Class CommandInitializer
    Dim trigger As String
    Dim commandExecutor As CommandExecutor
    Dim commandTranslator As CommandTranslator
    Public Sub New()
        trigger = "."
        commandExecutor = New CommandExecutor()
        commandTranslator = New CommandTranslator()
    End Sub
    Public Sub InitializeCommand(ByVal cmd As String, ByVal commandPromptInstance As CommandPromptInstance, ByVal pluginInitializer As PluginInitializer)
        cmd = commandTranslator.TranslateCommand(cmd)
        cmd = cmd.Replace("{clipboard}", Clipboard.GetText())
        cmd = cmd.Replace("{path}", Application.StartupPath)
        If cmd.Replace(" ", "") = "" Then
            Exit Sub
        End If
        If cmd.StartsWith(trigger) Then
            commandExecutor.ExecuteCommand(cmd.Substring(1, cmd.Length - 1), Me, commandPromptInstance, pluginInitializer)
        Else
            commandPromptInstance.GetProcess().StandardInput.WriteLine(cmd)
        End If
    End Sub
    Public Function GetTrigger()
        Return trigger
    End Function
    Public Sub SetTrigger(ByVal trigger As String)
        Me.trigger = trigger
    End Sub
End Class
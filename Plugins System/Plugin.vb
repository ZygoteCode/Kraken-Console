Public Class Plugin
    Dim pluginName As String
    Dim scriptData As TextBox
    Dim commandTranslator As CommandTranslator
    Public Sub New(ByVal pluginName As String, ByVal scriptData As TextBox)
        Me.pluginName = pluginName
        Me.scriptData = scriptData
        Me.commandTranslator = New CommandTranslator()
    End Sub
    Public Function GetPluginName() As String
        Return pluginName
    End Function
    Public Function GetScriptData() As TextBox
        Return scriptData
    End Function
    Public Function ExecuteCommand(ByVal cmdName As String, ByVal arguments() As String, ByVal otherString As String, ByVal commandInitializer As CommandInitializer, ByVal commandPromptInstance As CommandPromptInstance, ByVal pluginInitializer As PluginInitializer)
        For i = 0 To scriptData.Lines.Count - 1
            Dim line As String = scriptData.Lines(i)
            Dim translatedLine As String = commandTranslator.TranslateCommand(line)
            If translatedLine = ".DECLARE COMMAND " + cmdName Then
                ExecuteRealCommand(cmdName, arguments, otherString, i, commandInitializer, commandPromptInstance, pluginInitializer)
                Return True
            End If
        Next
        Return False
    End Function
    Public Sub ExecuteRealCommand(ByVal cmdName As String, ByVal arguments() As String, ByVal otherString As String, ByVal index As Integer, ByVal commandInitializer As CommandInitializer, ByVal commandPromptInstance As CommandPromptInstance, ByVal pluginInitializer As PluginInitializer)
        Dim startIndex As Integer = index
        For i = 0 To scriptData.Lines.Count - 1
            If i >= startIndex Then
                Dim line As String = scriptData.Lines(i)
                Dim translatedLine As String = commandTranslator.TranslateCommand(line)
                If translatedLine.Equals(".END COMMAND") Then
                    Exit Sub
                End If
                If translatedLine.StartsWith("BATCH CMD ") Then
                    Dim cmdToExec As String = translatedLine.Substring(10, translatedLine.Length - 10)
                    commandInitializer.InitializeCommand(cmdToExec, commandPromptInstance, pluginInitializer)
                End If
            End If
        Next
    End Sub
End Class
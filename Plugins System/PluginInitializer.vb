Public Class PluginInitializer
    Dim pluginChecker As PluginChecker
    Dim loadedPlugins As List(Of Plugin)
    Public Sub New()
        pluginChecker = New PluginChecker()
        loadedPlugins = New List(Of Plugin)
    End Sub
    Public Sub InitializePluginsLoading()
        If Not System.IO.Directory.Exists(Application.StartupPath & "\plugins") Then
            DataAdder.AddData("Can not find the plugins folder. Plugin loading failed.")
            Exit Sub
        End If
        For Each f As String In System.IO.Directory.GetFiles(Application.StartupPath & "\plugins")
            If System.IO.Path.GetExtension(f).ToLower() = ".pl" Then
                If pluginChecker.CheckPluginIntegrity(f) Then
                    LoadPlugin(f)
                End If
            End If
        Next
        DataAdder.AddData(loadedPlugins.Count.ToString() + " plugins succesfully loaded.")
    End Sub
    Public Function GetLoadedPlugins() As List(Of Plugin)
        Return loadedPlugins
    End Function
    Public Sub LoadPlugin(ByVal fileName As String)
        Dim readData As String = System.IO.File.ReadAllText(fileName)
        Dim readLines As New TextBox With {.Text = readData}
        Dim newPlugin As Plugin = New Plugin(System.IO.Path.GetFileNameWithoutExtension(fileName), readLines)
        loadedPlugins.Add(newPlugin)
    End Sub
    Public Sub UnloadPlugin(ByVal pluginName As String)
        For Each plugin As Plugin In loadedPlugins
            If plugin.GetPluginName().ToLower() = pluginName.ToLower() Then
                loadedPlugins.Remove(plugin)
            End If
        Next
    End Sub
    Public Function IsPluginLoaded(ByVal pluginName As String) As Boolean
        For Each plugin As Plugin In loadedPlugins
            If plugin.GetPluginName().ToLower() = pluginName.ToLower() Then
                Return True
            End If
        Next
        Return False
    End Function
End Class
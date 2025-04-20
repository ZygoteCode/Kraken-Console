Imports System.Security.Cryptography
Imports System.Text
Imports System.IO.Compression
Imports System.ComponentModel
Imports System.Net
Imports LuaInterface
Public Class CommandExecutor
    Dim scriptControl As MSScriptControl.ScriptControl
    Dim vbScript As ProtectedString = New ProtectedString("VBSCRIPT")
    Dim zero As ProtectedInteger = New ProtectedInteger(0)
    Dim byteMeasure As ProtectedInteger = New ProtectedInteger(1024)
    Dim spaceChar As ProtectedString = New ProtectedString(" ")
    Dim emptyChar As ProtectedString = New ProtectedString("")
    Dim valueOne As ProtectedInteger = New ProtectedInteger(1)
    Dim WithEvents downloader As New System.Net.WebClient
    Dim isDownloading As Boolean = False
    Dim luaInterface As Lua
    Private Function GetMD5Hash(theInput As String) As String
        Using hasher As MD5 = MD5.Create()
            Dim dbytes As Byte() = hasher.ComputeHash(Encoding.Unicode.GetBytes(theInput))
            Dim sBuilder As New StringBuilder()
            For n As Integer = 0 To dbytes.Length - 1
                sBuilder.Append(dbytes(n).ToString("X2"))
            Next n
            Return sBuilder.ToString()
        End Using
    End Function
    Public Sub New()
        scriptControl = New MSScriptControl.ScriptControl()
        scriptControl.Language = vbScript.GetValue()
        luaInterface = New Lua()
        luaInterface.RegisterFunction("print", Me, Me.GetType().GetMethod("print"))
    End Sub
    Public Sub ExecuteCommand(ByVal cmd As String, ByVal commandInitializer As CommandInitializer, ByVal commandPromptInstance As CommandPromptInstance, ByVal pluginInitializer As PluginInitializer)
        Dim cmdName As String = emptyChar.GetValue()
        Dim arguments() As String = New String() {}
        Dim otherString As String = emptyChar.GetValue()
        If cmd.Contains(spaceChar.GetValue()) Then
            cmdName = cmd.Split(spaceChar.GetValue())(zero.GetValue())
        Else
            cmdName = cmd
        End If
        Dim newString As String = emptyChar.GetValue()
        If cmd.Contains(spaceChar.GetValue()) Then
            newString = cmd.Replace(cmdName + spaceChar.GetValue(), emptyChar.GetValue())
        End If
        otherString = newString
        arguments = otherString.Split(spaceChar.GetValue())
        ExecuteCmd(cmdName, arguments, otherString, commandInitializer, commandPromptInstance, pluginInitializer, True)
    End Sub
    Public Sub ExecuteCmd(ByVal cmdName As String, ByVal arguments() As String, ByVal otherString As String, ByVal commandInitializer As CommandInitializer, ByVal commandPromptInstance As CommandPromptInstance, ByVal pluginInitializer As PluginInitializer, ByVal repeat As Boolean)
        cmdName = cmdName.ToLower()
        If cmdName = "help" Then
            Dim trigger As String = commandInitializer.GetTrigger()
            Dim maxPages As Byte = 2
            Dim page As Byte = zero.GetValue()
            Try
                page = Byte.Parse(arguments(zero.GetValue()))
            Catch ex As Exception
                page = 1
            End Try
            If page > maxPages Then
                page = maxPages
            End If
            Dim commands As String = emptyChar.GetValue()
            If page = 1 Then
                commands = trigger + "help <page> - Show the list of all commands for Kraken Console." + vbNewLine +
                    trigger + "clear - Clear all the console." + vbNewLine +
                    trigger + "info - Get all informations about the Kraken Console." + vbNewLine +
                    trigger + "plugin - Use all commands about the plugin system." + vbNewLine +
                    trigger + "process - Use all commands to interact with the process system." + vbNewLine +
                    trigger + "calc <operation> - Execute a calculation." + vbNewLine +
                    trigger + "isup <ip/host> - Check if a host or ip address is online or offline." + vbNewLine +
                    trigger + "ip2host <ip> - Get the host address from a ip address." + vbNewLine +
                    trigger + "host2ip <host> - Get the ip address from a host address." + vbNewLine +
                    trigger + "skresolve <name> - Get a ip address from a skype name." + vbNewLine +
                    trigger + "dbresolve <name> - Get all ip addresses from a skype name." + vbNewLine +
                    trigger + "ip2skype <ip> - Get the skype names from a ip address." + vbNewLine +
                    trigger + "email2skype <email> - Get the skype names from a email address." + vbNewLine +
                    trigger + "geoip <ip> <method (0/1)> - Get informations about an ip address." + vbNewLine +
                    trigger + "dnsresolve <domain> - Get the dns records from a domain." + vbNewLine +
                    trigger + "cfresolve <domain> - Get all the cloudflare ip addresses from a domain." + vbNewLine +
                    trigger + "inject <name/id> <path> - Inject a dll library to a process." + vbNewLine +
                    trigger + "headers <domain> - Get the header informations from a domain." + vbNewLine +
                    trigger + "iplogger - Create a ip logger." + vbNewLine +
                    trigger + "disposable <email> - Check if a email is disposable." + vbNewLine +
                    trigger + "ip - Get your IPv4 ip address." + vbNewLine +
                    trigger + "bypass <game> - Bypass the launch of a game (Paladins)." + vbNewLine +
                    trigger + "colorinfo <name/hex/argb/palette> - Get the info about a color." + vbNewLine +
                    trigger + "define <word> - Give the definition of a word." + vbNewLine +
                    trigger + "nlookup <number> - Get the informations of a phone number." + vbNewLine +
                    trigger + "name2uuid <name> - Converts a Minecraft name to a UUID." + vbNewLine +
                    trigger + "example <word> - Return an example on how to use a word." + vbNewLine +
                    trigger + "ytstats <id> - Returns details about provided youtube video." + vbNewLine +
                    trigger + "nightcore - Returns a random nightcore video." + vbNewLine +
                    trigger + "imgconv <from-path> <to-path> - Convert a target image to another image."
            ElseIf page = 2 Then
                commands = trigger + "ping <ip/host> - Get the response time from a host." + vbNewLine +
                    trigger + "animepic - Returns a random anime image." + vbNewLine +
                    trigger + "joke - Returns a random joke." + vbNewLine +
                    trigger + "say <string> - Say a string." + vbNewLine +
                    trigger + "skpic <name> - Get the Skype profile picture of a specific profile." + vbNewLine +
                    trigger + "host2ips <host> - Get all possible ip addresses from a specified host." + vbNewLine +
                    trigger + "md5 <string> - Get the MD5 hash of a string." + vbNewLine +
                    trigger + "qrcode <string> - Generate a QR code from a string." + vbNewLine +
                    trigger + "package - Main command to interact with the package system." + vbNewLine +
                    trigger + "xor <key> <string> - Get the XOR of a string." + vbNewLine +
                    trigger + "short <url> - Make long urls shortened." + vbNewLine +
                    trigger + "wget <url> - Download a file from a url." + vbNewLine +
                    trigger + "urlcontent <url> - Get the content of a url." + vbNewLine +
                    trigger + "html <path> - Run and edit an HTML page." + vbNewLine +
                    trigger + "asm8086 <path> - Run and edit a 8086 Assembly source." + vbNewLine +
                    trigger + "python <path> - Execute a Python script." + vbNewLine +
                    trigger + "lua <path> - Execute a Lua script." + vbNewLine +
                    trigger + "randomnum <min> <max> - Get a random number." + vbNewLine +
                    trigger + "mcnames <name/uuid> - Get all Minecraft names." + vbNewLine +
                    trigger + "base64 <string> - Convert a simple string to a Base64 string." + vbNewLine +
                    trigger + "unbase64 <string> - Convert a Base64 string to a simple string." + vbNewLine +
                    trigger + "length <string> - Get the length of a string." + vbNewLine +
                    trigger + "compressfile <from-path> <to-path> - Compress a file." + vbNewLine +
                    trigger + "uncompressfile <from-path> <to-path> - Uncompress a file." + vbNewLine +
                    trigger + "audio - Use all commands to interact with the audio system." + vbNewLine +
                    trigger + "sort <ascending/descending> <list-of-numbers> - Sort a list of numbers."
            End If
            DataAdder.AddData("Here is the list of commands for Kraken Console (" + page.ToString() + "/" + maxPages.ToString() + "):" + vbNewLine + commands)
        ElseIf cmdName = "clear" Then
            DataAdder.Clear()
        ElseIf cmdName = "info" Then
            DataAdder.AddData("Infos about Kraken Console:" + vbNewLine + vbNewLine +
                              "Product name: Kraken Console" + vbNewLine +
                              "Author: ZygoteCode" + vbNewLine +
                              "Your console ID: " + HWUtils.GetCpuId() + vbNewLine +
                              "Copyright 2025, ZygoteCode. Do not distribute.")
        ElseIf cmdName = "process" Then
            Try
                Dim use As String = arguments(zero.GetValue())
                use = use.ToLower()
                If use = "status" Then
                    Dim thing As String = arguments(valueOne.GetValue())
                    thing = thing.ToLower()
                    Dim process As Process = New Process()
                    Dim found As Boolean = False
                    If IsNumeric(thing) Then
                        Dim processID As Integer = Int32.Parse(thing)
                        For Each proc As Process In Process.GetProcesses()
                            If proc.Id = processID Then
                                found = True
                                process = proc
                                Exit For
                            End If
                        Next
                    Else
                        For Each proc As Process In Process.GetProcesses()
                            If proc.ProcessName.ToLower().Equals(thing) Then
                                found = True
                                process = proc
                                Exit For
                            End If
                        Next
                    End If
                    If found Then
                        DataAdder.AddData("Informations about the selected process: " + vbNewLine + vbNewLine +
                                          "Process ID: " + process.Id.ToString() + vbNewLine +
                                          "Process name: " + process.ProcessName + vbNewLine +
                                          "Process responding: " + process.Responding.ToString() + vbNewLine +
                                          "Process main window title: " + process.MainWindowTitle + vbNewLine +
                                          "Process priority: " + process.PriorityClass.ToString() + vbNewLine +
                                          "Process threads: " + process.Threads.Count.ToString() + vbNewLine +
                                          "Process memory: " + ((((process.VirtualMemorySize64) / byteMeasure.GetValue()) / byteMeasure.GetValue())).ToString() + "MB" + vbNewLine +
                                          "Process start time: " + process.StartTime.ToLongTimeString() + spaceChar.GetValue() + process.StartTime.ToShortDateString())
                    Else
                        DataAdder.AddData("Could not find that process.")
                    End If
                ElseIf use = "help" Then
                    Dim trigger As String = commandInitializer.GetTrigger()
                    DataAdder.AddData("Here is the list of all commands to interact with the process system: " + vbNewLine + vbNewLine +
                                      trigger + "process help - Get all commands to interact with the process system." + vbNewLine +
                                      trigger + "process status <id/name> - Get the status of a process." + vbNewLine +
                                      trigger + "process kill <id/name> - Kill a process." + vbNewLine +
                                      trigger + "process suspend <id/name> - Suspend a process." + vbNewLine +
                                      trigger + "process resume <id/name> - Resume a process.")
                ElseIf use = "kill" Then
                    Dim thing As String = arguments(valueOne.GetValue())
                    thing = thing.ToLower()
                    Dim process As Process = New Process()
                    Dim found As Boolean = False
                    If IsNumeric(thing) Then
                        Dim processID As Integer = Int32.Parse(thing)
                        For Each proc As Process In Process.GetProcesses()
                            If proc.Id = processID Then
                                found = True
                                process = proc
                                Exit For
                            End If
                        Next
                    Else
                        For Each proc As Process In Process.GetProcesses()
                            If proc.ProcessName.ToLower().Equals(thing) Then
                                found = True
                                process = proc
                                Exit For
                            End If
                        Next
                    End If
                    If found Then
                        Try
                            process.Kill()
                            DataAdder.AddData("Succesfully killed the process.")
                        Catch ex As Exception
                            DataAdder.AddData("Failed to kill the process.")
                        End Try
                    Else
                        DataAdder.AddData("Could not find that process.")
                    End If
                ElseIf use = "suspend" Then
                    Dim thing As String = arguments(valueOne.GetValue())
                    thing = thing.ToLower()
                    Dim process As Process = New Process()
                    Dim found As Boolean = False
                    If IsNumeric(thing) Then
                        Dim processID As Integer = Int32.Parse(thing)
                        For Each proc As Process In Process.GetProcesses()
                            If proc.Id = processID Then
                                found = True
                                process = proc
                                Exit For
                            End If
                        Next
                    Else
                        For Each proc As Process In Process.GetProcesses()
                            If proc.ProcessName.ToLower().Equals(thing) Then
                                found = True
                                process = proc
                                Exit For
                            End If
                        Next
                    End If
                    If found Then
                        Try
                            ProcessUtils.SuspendProcess(process)
                            DataAdder.AddData("Succesfully suspended the process.")
                        Catch ex As Exception
                            DataAdder.AddData("Failed to suspend the process.")
                        End Try
                    Else
                        DataAdder.AddData("Could not find that process.")
                    End If
                ElseIf use = "resume" Then
                    Dim thing As String = arguments(valueOne.GetValue())
                    thing = thing.ToLower()
                    Dim process As Process = New Process()
                    Dim found As Boolean = False
                    If IsNumeric(thing) Then
                        Dim processID As Integer = Int32.Parse(thing)
                        For Each proc As Process In Process.GetProcesses()
                            If proc.Id = processID Then
                                found = True
                                process = proc
                                Exit For
                            End If
                        Next
                    Else
                        For Each proc As Process In Process.GetProcesses()
                            If proc.ProcessName.ToLower().Equals(thing) Then
                                found = True
                                process = proc
                                Exit For
                            End If
                        Next
                    End If
                    If found Then
                        Try
                            ProcessUtils.ResumeProcess(process)
                            DataAdder.AddData("Succesfully resumed the process.")
                        Catch ex As Exception
                            DataAdder.AddData("Failed to resume the process.")
                        End Try
                    Else
                        DataAdder.AddData("Could not find that process.")
                    End If
                Else
                    DataAdder.AddData("Unknown syntax of the command. Please, type " + commandInitializer.GetTrigger() + "process help to get all the commands to interact with the process system.")
                End If
            Catch ex As Exception
                DataAdder.AddData("Unknown syntax of the command. Please, type " + commandInitializer.GetTrigger() + "process help to get all the commands to interact with the process system.")
            End Try
        ElseIf cmdName = "plugin" Then
            Try
                If System.IO.Directory.Exists(Application.StartupPath & "\plugins") Then
                    Dim use As String = arguments(zero.GetValue())
                    use = use.ToLower()
                    If use = "load" Then
                        Dim totalOtherString As String = otherString
                        totalOtherString = otherString.Replace("load ", emptyChar.GetValue())
                        If System.IO.File.Exists(totalOtherString) Then
                            If pluginInitializer.IsPluginLoaded(IO.Path.GetFileNameWithoutExtension(totalOtherString)) Then
                                DataAdder.AddData("This plugin is already loaded in the plugins system.")
                            Else
                                pluginInitializer.LoadPlugin(totalOtherString)
                            End If
                        Else
                            Dim totalPath As String = Application.StartupPath & "\plugins\" + totalOtherString + ".pl"
                            If System.IO.File.Exists(totalPath) Then
                                If pluginInitializer.IsPluginLoaded(totalOtherString) Then
                                    DataAdder.AddData("This plugin is already loaded in the plugin system.")
                                Else
                                    pluginInitializer.LoadPlugin(totalPath)
                                    DataAdder.AddData("Plugin succesfully loaded.")
                                End If
                            Else
                                DataAdder.AddData("The plugin that you are trying to load cannot be loaded because the file does not exist on your plugins folder.")
                            End If
                        End If
                    ElseIf use = "unload" Then
                        otherString = otherString.ToLower()
                        If pluginInitializer.IsPluginLoaded(otherString) Then
                            pluginInitializer.UnloadPlugin(otherString)
                            DataAdder.AddData("Plugin succesfully unloaded.")
                        Else
                            DataAdder.AddData("This plugin is not loaded in the plugin system.")
                        End If
                    ElseIf use = "list" Then
                        Dim plugins As String = emptyChar.GetValue()
                        For Each plugin As Plugin In pluginInitializer.GetLoadedPlugins()
                            If plugins = emptyChar.GetValue() Then
                                plugins = plugin.GetPluginName()
                            Else
                                plugins += ", " + plugin.GetPluginName()
                            End If
                        Next
                        If plugins = emptyChar.GetValue() Then
                            DataAdder.AddData("No plugins have been loaded in the plugin system.")
                        Else
                            DataAdder.AddData("Loaded plugins (" + pluginInitializer.GetLoadedPlugins().Count.ToString() + "): " + plugins)
                        End If
                    ElseIf use = "help" Then
                        Dim trigger As String = commandInitializer.GetTrigger()
                        DataAdder.AddData("Here is the list of all commands to use the plugin system: " + vbNewLine + vbNewLine +
                                          trigger + "plugin load <name/path> - Load a plugin by its name or path." + vbNewLine +
                                          trigger + "plugin unload <name> - Unload a plugin." + vbNewLine +
                                          trigger + "plugin list - Get all the loaded plugins.")
                    Else
                        DataAdder.AddData("Unknown syntax of the command. Please, type " + commandInitializer.GetTrigger() + "plugin help to get all commands to use the plugin system.")
                    End If
                Else
                    DataAdder.AddData("The plugin system cannot be used without the plugins folder.")
                End If
            Catch ex As Exception
                DataAdder.AddData("Unknown syntax of the command. Please, type " + commandInitializer.GetTrigger() + "plugin help to get all commands to use the plugin system.")
            End Try
        ElseIf cmdName = "calc" Or cmdName = "calculate" Then
            Dim calc As String = otherString
            calc = calc.Replace(spaceChar.GetValue(), emptyChar.GetValue())
            Try
                DataAdder.AddData("The result of this operation is: " + scriptControl.Eval(calc).ToString())
            Catch ex As Exception
                DataAdder.AddData("This calculation can not be executed.")
            End Try
        ElseIf cmdName = "isup" Then
            Try
                If My.Computer.Network.Ping(arguments(zero.GetValue())) Then
                    DataAdder.AddData("This host is online.")
                Else
                    DataAdder.AddData("This host is offline.")
                End If
            Catch ex As Exception
                DataAdder.AddData("This host is offline.")
            End Try
        ElseIf cmdName = "ip2host" Then
            Try
                DataAdder.AddData("The host address for this ip address is: " + System.Net.Dns.GetHostByAddress(arguments(zero.GetValue())).HostName)
            Catch ex As Exception
                DataAdder.AddData("Can not get the host address from this ip address.")
            End Try
        ElseIf cmdName = "host2ip" Then
            Try
                DataAdder.AddData("The ip address for this host address is: " + System.Net.Dns.GetHostByName(arguments(zero.GetValue())).AddressList(zero.GetValue()).ToString())
            Catch ex As Exception
                DataAdder.AddData("Can not get the host address from this ip address.")
            End Try
        ElseIf cmdName = "host2ips" Then
            Try
                Dim ips As String = ""
                For Each ip As System.Net.IPAddress In System.Net.Dns.GetHostByName(arguments(zero.GetValue())).AddressList
                    If ips = "" Then
                        ips = ip.ToString()
                    Else
                        ips += ", " + ip.ToString()
                    End If
                Next
                Dim web As New System.Net.WebClient
                Try
                    ips += ", " + web.DownloadString("https://api.apithis.net/host2ip.php?hostname=" + arguments(zero.GetValue())) + "."
                Catch ex As Exception
                    ips += "."
                End Try
                DataAdder.AddData("The ip addresses for this host address are: " + ips)
            Catch ex As Exception
                DataAdder.AddData("Can not get the host address from this ip address.")
            End Try
        ElseIf cmdName = "skresolve" Then
            Try
                Dim web As New System.Net.WebClient
                Dim thing As String = web.DownloadString("https://webresolver.nl/api.php?key=4BMRN-A9HUE-E1NC2-6JWQ7&action=resolve&string=" + arguments(zero.GetValue()))
                If thing.Contains("f") Or thing.Contains("c") Then
                    DataAdder.AddData("This username was not found on the skype database.")
                Else
                    DataAdder.AddData("The ip address from this skype name is: " + thing)
                End If
            Catch ex As Exception
                DataAdder.AddData("Can not get the ip address from this skype name.")
            End Try
        ElseIf cmdName = "dbresolve" Then
            Try
                Dim web As New System.Net.WebClient
                Dim thing As String = web.DownloadString("https://webresolver.nl/api.php?key=4BMRN-A9HUE-E1NC2-6JWQ7&action=resolvedb&string=" + arguments(zero.GetValue()))
                If thing.Contains("f") Or thing.Contains("c") Then
                    DataAdder.AddData("This username was not found on the skype database.")
                Else
                    DataAdder.AddData("The ip addresses from this skype name are: " + thing)
                End If
            Catch ex As Exception
                DataAdder.AddData("Can not get the ip addresses from this skype name.")
            End Try
        ElseIf cmdName = "ip2skype" Then
            Try
                Dim web As New System.Net.WebClient
                Dim thing As String = web.DownloadString("https://webresolver.nl/api.php?key=4BMRN-A9HUE-E1NC2-6JWQ7&action=ip2skype&string=" + arguments(zero.GetValue()))
                If thing.Contains("f") Or thing.Contains("c") Then
                    DataAdder.AddData("This ip address was not found in the skype database.")
                Else
                    DataAdder.AddData("The skype names from this ip address are: " + thing)
                End If
            Catch ex As Exception
                DataAdder.AddData("Can not get the skype names from this ip address.")
            End Try
        ElseIf cmdName = "email2skype" Then
            Try
                Dim web As New System.Net.WebClient
                Dim thing As String = web.DownloadString("https://webresolver.nl/api.php?key=4BMRN-A9HUE-E1NC2-6JWQ7&action=email2skype&string=" + arguments(zero.GetValue()))
                If thing.Contains("f") Or thing.Contains("c") Then
                    DataAdder.AddData("This email address was not found in the skype database.")
                Else
                    DataAdder.AddData("The skype names from this email address are: " + thing)
                End If
            Catch ex As Exception
                DataAdder.AddData("Can not get the skype names from this email address.")
            End Try
        ElseIf cmdName = "geoip" Then
            Try
                If arguments(1).Equals("0") Then
                    Dim web As New System.Net.WebClient
                    Dim thing As String = web.DownloadString("https://webresolver.nl/api.php?key=4BMRN-A9HUE-E1NC2-6JWQ7&action=geoip&string=" + arguments(zero.GetValue())).Replace("<br>", Environment.NewLine)
                    If thing.Contains("database") Then
                        DataAdder.AddData("Can not get the informations about this ip address.")
                    Else
                        DataAdder.AddData("Informations about this ip address are: " + vbNewLine + vbNewLine + thing)
                    End If
                Else
                    Dim web As New System.Net.WebClient
                    Dim thing As String = web.DownloadString("https://api.apithis.net/geoip.php?ip=" + arguments(zero.GetValue())).Replace("<br />", Environment.NewLine)
                    If thing.Contains("database") Then
                        DataAdder.AddData("Can not get the informations about this ip address.")
                    Else
                        DataAdder.AddData("Informations about this ip address are: " + vbNewLine + vbNewLine + thing)
                    End If
                End If
            Catch ex As Exception
                DataAdder.AddData("Can not get the informations about this ip address.")
            End Try
        ElseIf cmdName = "dnsresolve" Then
            Try
                Dim web As New System.Net.WebClient
                Dim thing As String = web.DownloadString("https://webresolver.nl/api.php?key=4BMRN-A9HUE-E1NC2-6JWQ7&action=dns&string=" + arguments(zero.GetValue())).Replace("<br>", Environment.NewLine)
                If thing.Contains("database") Then
                    DataAdder.AddData("Can not get the dns records from this domain.")
                Else
                    DataAdder.AddData("The dns records from this domain are: " + vbNewLine + vbNewLine + thing)
                End If
            Catch ex As Exception
                DataAdder.AddData("Can not get the dns records from this domain.")
            End Try
        ElseIf cmdName = "cfresolve" Then
            Try
                Dim web As New System.Net.WebClient
                Dim thing As String = web.DownloadString("https://webresolver.nl/api.php?key=4BMRN-A9HUE-E1NC2-6JWQ7&action=cloudflare&string=" + arguments(zero.GetValue())).Replace("<br>", Environment.NewLine)
                If thing.Contains("find") Then
                    DataAdder.AddData("Can not get the cloudflare ip addresses from this domain.")
                Else
                    DataAdder.AddData("The cloudflare ip addresses from this domain are: " + vbNewLine + vbNewLine + thing)
                End If
            Catch ex As Exception
                DataAdder.AddData("Can not get the cloudflare ip addresses from this domain.")
            End Try
        ElseIf cmdName = "inject" Then
            Try
                Dim targetProcess As Process = New Process()
                Dim found As Boolean = False
                For Each process As Process In Process.GetProcesses()
                    If IsNumeric(arguments(zero.GetValue())) Then
                        If process.Id = Int32.Parse(arguments(zero.GetValue())) Then
                            targetProcess = process
                            found = True
                            Exit For
                        End If
                    Else
                        If process.ProcessName.ToLower() = arguments(zero.GetValue()).ToLower() Then
                            targetProcess = process
                            found = True
                            Exit For
                        End If
                    End If
                Next
                If found Then
                    Dim totalOtherString As String = otherString
                    totalOtherString = totalOtherString.Replace(arguments(zero.GetValue()) + spaceChar.GetValue(), emptyChar.GetValue())
                    If System.IO.File.Exists(totalOtherString) Then
                        If inject(targetProcess.Id, totalOtherString) Then
                            DataAdder.AddData("Succesfully injected to process.")
                        Else
                            DataAdder.AddData("Injection to process failed.")
                        End If
                    Else
                        DataAdder.AddData("Can not find the specified dll file.")
                    End If
                Else
                    DataAdder.AddData("Can not find the specified process.")
                End If
            Catch ex As Exception
                DataAdder.AddData("Injection to process failed.")
            End Try
        ElseIf cmdName = "headers" Then
            Try
                Dim web As New System.Net.WebClient
                Dim thing As String = web.DownloadString("https://webresolver.nl/api.php?key=4BMRN-A9HUE-E1NC2-6JWQ7&action=header&string=" + arguments(zero.GetValue())).Replace("<br>", Environment.NewLine).Replace("<br />", emptyChar.GetValue())
                If thing.Contains("resolve") Or thing.Contains("find") Then
                    DataAdder.AddData("Can not get the header informations from this domain.")
                Else
                    DataAdder.AddData("The header informations from this domain are: " + vbNewLine + vbNewLine + thing)
                End If
            Catch ex As Exception
                DataAdder.AddData("Can not get the header informations from this domain.")
            End Try
        ElseIf cmdName = "iplogger" Then
            Try
                Dim web As New System.Net.WebClient
                Dim thing As String = web.DownloadString("https://webresolver.nl/api.php?key=4BMRN-A9HUE-E1NC2-6JWQ7&action=iplogger&string=&logger=youtube").Replace("<br>", Environment.NewLine)
                DataAdder.AddData("Here is your usable ip logger: " + vbNewLine + vbNewLine + thing)
                Clipboard.SetText(thing)
            Catch ex As Exception
                DataAdder.AddData("Can not create the ip logger.")
            End Try
        ElseIf cmdName = "disposable" Then
            Try
                Dim web As New System.Net.WebClient
                Dim thing As String = web.DownloadString("https://webresolver.nl/api.php?key=4BMRN-A9HUE-E1NC2-6JWQ7&action=disposable_email&string=" + arguments(zero.GetValue())).Replace("<br>", Environment.NewLine)
                If thing.Contains("is a disposable") Then
                    DataAdder.AddData("This is a disposable email.")
                Else
                    DataAdder.AddData("This is a not disposable email.")
                End If
            Catch ex As Exception
                DataAdder.AddData("Can not check this email.")
            End Try
        ElseIf cmdName = "ip" Then
            Try
                Dim web As New System.Net.WebClient
                DataAdder.AddData("Your IPv4 ip address is: " + web.DownloadString("https://api.ipify.org"))
            Catch ex As Exception
                DataAdder.AddData("Can not get your IPv4 ip address.")
            End Try
        ElseIf cmdName = "bypass" Then
            Try
                Dim game As String = arguments(zero.GetValue()).ToLower()
                If game = "paladins" Then
                    Dim theFile As String = Environment.GetFolderPath(Environment.SpecialFolder.CDBurning) + "\" + (Rnd() * 6000).ToString() + ".bat"
                    System.IO.File.WriteAllText(theFile, My.Resources.Bypass_Paladins)
                    Process.Start(theFile)
                    DataAdder.AddData("Succesfully started bypass Paladins.")
                End If
            Catch ex As Exception
                DataAdder.AddData("Can not bypass Paladins.")
            End Try
        ElseIf cmdName = "colorinfo" Then
            Dim color As Color
            If arguments(0).ToLower().Equals("palette") Then
                Dim colorDialog As ColorDialog = New ColorDialog()
                colorDialog.AllowFullOpen = True
                colorDialog.AnyColor = True
                colorDialog.FullOpen = True
                colorDialog.SolidColorOnly = False
                DataAdder.AddData("Opening the palette to select your color to get infos...")
                If colorDialog.ShowDialog() = DialogResult.OK Then
                    color = colorDialog.Color
                Else
                    DataAdder.AddData("Failed to get the color.")
                    Exit Sub
                End If
            Else
                If IsNumeric(arguments(0)) Or arguments(0).StartsWith("#") Or Color.FromName(arguments(0)).IsKnownColor Then
                    Try
                        color = ColorTranslator.FromHtml(arguments(0).ToUpper())
                    Catch ex As Exception
                        Try
                            color = Color.FromName(arguments(0).ToUpper())
                        Catch e As Exception
                            Try
                                color = Color.FromArgb(Int32.Parse(arguments(0)))
                            Catch em As Exception
                                DataAdder.AddData("The color you have written is not correct.")
                                Exit Sub
                            End Try
                        End Try
                    End Try
                Else
                    DataAdder.AddData("The color you have written is not correct.")
                    Exit Sub
                End If
            End If
            DataAdder.AddData("Infos about this color: " + vbNewLine + vbNewLine +
                              "Red (R): " + color.R.ToString() + vbNewLine +
                              "Green (G): " + color.G.ToString() + vbNewLine +
                              "Blue (B): " + color.B.ToString() + vbNewLine +
                              "RGB (Red, Green, Blue): " + color.R.ToString() + ", " + color.G.ToString() + ", " + color.B.ToString() + vbNewLine +
                              "HEX (HTML code): #" + color.ToArgb().ToString("X6").ToUpper() + vbNewLine +
                              "Alpha (A): " + color.A.ToString() + vbNewLine +
                              "ARGB (Alpha, Red, Green, Blue): " + color.ToArgb().ToString() + vbNewLine +
                              "Color name: " + color.Name.ToString().ToUpper())
        ElseIf cmdName = "define" Then
            Try
                Dim web As New System.Net.WebClient
                DataAdder.AddData("The definition of this word is: " + vbNewLine + vbNewLine + web.DownloadString("https://api.apithis.net/dictionary.php?define=" + arguments(0)))
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "nlookup" Then
            Try
                Dim web As New System.Net.WebClient
                DataAdder.AddData("Informations about this number: " + vbNewLine + vbNewLine + web.DownloadString("https://api.apithis.net/numberinfo.php?number=" + arguments(0)).Replace("<br />", vbNewLine))
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "name2uuid" Then
            Try
                Dim web As New System.Net.WebClient
                DataAdder.AddData("The UUID for this Minecraft name is: " + web.DownloadString("https://api.apithis.net/name2uuid.php?name=" + arguments(0)))
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "example" Then
            Try
                Dim web As New System.Net.WebClient
                DataAdder.AddData("Example of how to use this word: " + vbNewLine + vbNewLine + web.DownloadString("https://api.apithis.net/dictionary.php?example=" + arguments(0)))
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "ytstats" Then
            Try
                Dim web As New System.Net.WebClient
                DataAdder.AddData("Informations about this video: " + vbNewLine + vbNewLine + web.DownloadString("https://api.apithis.net/ytstats.php?data=all&id=" + arguments(0)).Replace("<br />", vbNewLine))
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "nightcore" Then
            Try
                Dim web As New System.Net.WebClient
                DataAdder.AddData("Here is a random nightcore song: " + web.DownloadString("https://api.apithis.net/nightcore.php"))
            Catch ex As Exception
                DataAdder.AddData("Can not reach the host.")
            End Try
        ElseIf cmdName = "imgconv" Then
            Try
                Dim fromPath As String = arguments(0)
                If System.IO.File.Exists(fromPath) Then
                    Dim toPath As String = arguments(1)
                    If System.IO.File.Exists(toPath) Then
                        System.IO.File.Delete(toPath)
                    End If
                    Dim image As Bitmap = New Bitmap(fromPath)
                    image.Save(toPath)
                    DataAdder.AddData("The image has been succesfully converted.")
                Else
                    DataAdder.AddData("This file does not exist.")
                End If
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "ping" Then
            Try
                If My.Computer.Network.Ping(arguments(0)) Then
                    Dim ping As New System.Net.NetworkInformation.Ping
                    Dim informations As System.Net.NetworkInformation.PingReply = ping.Send(arguments(0))
                    DataAdder.AddData("Ping of (" + arguments(0) + ") is: " + informations.RoundtripTime.ToString() + "ms [TTL = " + informations.Options.Ttl.ToString() + "].")
                Else
                    DataAdder.AddData("Can not reach that host.")
                End If
            Catch ex As Exception
                DataAdder.AddData("Can not ping that host.")
            End Try
        ElseIf cmdName = "animepic" Then
            Try
                Dim web As New System.Net.WebClient
                Dim stringhed As String = web.DownloadString("https://api.apithis.net/animepic.php")
                DataAdder.AddData("Here is a random anime picture: " + stringhed)
                If Not System.IO.Directory.Exists(Application.StartupPath & "\anime_pics") Then
                    System.IO.Directory.CreateDirectory(Application.StartupPath & "\anime_pics")
                End If
                System.IO.File.WriteAllBytes(Application.StartupPath & "\anime_pics\" + (Rnd() * 60000).ToString() + ".png", web.DownloadData(stringhed))
            Catch ex As Exception
                DataAdder.AddData("Can not reach the host.")
            End Try
        ElseIf cmdName = "joke" Then
            Try
                Dim web As New System.Net.WebClient
                DataAdder.AddData(web.DownloadString("https://api.apithis.net/yomama.php"))
            Catch ex As Exception
                DataAdder.AddData("Can not reach the host.")
            End Try
        ElseIf cmdName = "say" Then
            DataAdder.AddData(otherString)
        ElseIf cmdName = "skpic" Then
            Try
                Dim web As New System.Net.WebClient
                DataAdder.AddData("Here is the profile picture of this profile: " + "https://api.skype.com/users/" + arguments(0) + "/profile/avatar?size=s")
                If Not System.IO.Directory.Exists(Application.StartupPath & "\skype_pics") Then
                    System.IO.Directory.CreateDirectory(Application.StartupPath & "\skype_pics")
                End If
                System.IO.File.WriteAllBytes(Application.StartupPath & "\skype_pics\" + arguments(0) + ".png", web.DownloadData("https://api.skype.com/users/" + arguments(0) + "/profile/avatar?size=s"))
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "md5" Then
            Try
                DataAdder.AddData("Here is your MD5 hash: " + GetMD5Hash(otherString).ToLower())
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "qrcode" Then
            Try
                Dim web As New System.Net.WebClient
                If Not System.IO.Directory.Exists(Application.StartupPath & "\qr_codes") Then
                    System.IO.Directory.CreateDirectory(Application.StartupPath & "\qr_codes")
                End If
                System.IO.File.WriteAllBytes(Application.StartupPath & "\qr_codes\" + (Rnd() * 60000).ToString() + ".png", web.DownloadData("https://api.qrserver.com/v1/create-qr-code/?data=" + otherString + "&size=220x220&margin=0"))
                DataAdder.AddData("Here is your generated QR code: https://api.qrserver.com/v1/create-qr-code/?data=" + otherString + "&size=220x220&margin=0")
            Catch ex As Exception
                MsgBox(ex.Message.ToString())
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "package" Then
            Try
                Dim use As String = arguments(0).ToLower()
                If use = "create" Then
                    Dim web As New System.Net.WebClient
                    Dim lol As Byte = Byte.Parse(web.DownloadString("http://mathplusplus.altervista.org/KrakenConsole/packages/create-package.php?packagename=" + arguments(1) + "&key=JREKJEWRLKJRLKWJERLKWJER"))
                    If lol = 0 Then
                        DataAdder.AddData("This package already exists.")
                    Else
                        DataAdder.AddData("The package has been succesfully created.")
                    End If
                ElseIf use = "exists" Then
                    Dim web As New System.Net.WebClient
                    Dim lol As Byte = Byte.Parse(web.DownloadString("http://mathplusplus.altervista.org/KrakenConsole/packages/is-package.php?packagename=" + arguments(1)))
                    If lol = 0 Then
                        DataAdder.AddData("This package does not exists.")
                    Else
                        DataAdder.AddData("This package exists in the package system.")
                    End If
                ElseIf use = "install" Then
                    Dim web As New System.Net.WebClient
                    Dim lol As Byte = Byte.Parse(web.DownloadString("http://mathplusplus.altervista.org/KrakenConsole/packages/is-package.php?packagename=" + arguments(1)))
                    If lol = 0 Then
                        DataAdder.AddData("This package does not exists.")
                    Else
                        If Not System.IO.Directory.Exists(Application.StartupPath & "\plugins") Then
                            System.IO.Directory.CreateDirectory(Application.StartupPath & "\plugins")
                        End If
                        Dim link As String = "http://mathplusplus.altervista.org/KrakenConsole/packages/" + arguments(1) + "/package.zip"
                        web.DownloadFile(link, Application.StartupPath & "\plugins\package.zip")
                        ZipFile.ExtractToDirectory(Application.StartupPath & "\plugins\package.zip", Application.StartupPath & "\plugins\")
                        System.IO.File.Delete(Application.StartupPath & "\plugins\package.zip")
                        DataAdder.AddData("The plugin has been succesfully installed.")
                    End If
                ElseIf use = "remove" Then
                    If System.IO.Directory.Exists(Application.StartupPath & "\plugins") Then
                        If System.IO.Directory.Exists(Application.StartupPath & "\plugins\" + arguments(1)) Then
                            System.IO.Directory.Delete(Application.StartupPath & "\plugins\" + arguments(1))
                            DataAdder.AddData("This package has been succesfully removed from your computer.")
                        Else
                            DataAdder.AddData("This package is not in your computer.")
                        End If
                    Else
                        DataAdder.AddData("This package is not in your computer.")
                    End If
                ElseIf use = "help" Then
                    Dim trigger As String = commandInitializer.GetTrigger()
                    DataAdder.AddData("Here is the list of all commands to interact with the package system: " + vbNewLine + vbNewLine +
                                      trigger + "package help - Get all the commands to interact with the package system." + vbNewLine +
                                      trigger + "package create <name> - Create a package." + vbNewLine +
                                      trigger + "package exists <name> - Check if a package exists." + vbNewLine +
                                      trigger + "package install <name> - Install a package." + vbNewLine +
                                      trigger + "package remove <name> - Remove a package." + vbNewLine +
                                      trigger + "package list - Get the list of all installed packages.")
                ElseIf use = "list" Then
                    Dim packages As String = ""
                    Dim packagesCount As Integer = 0
                    For Each f As String In System.IO.Directory.GetDirectories(Application.StartupPath & "\plugins")
                        f = f.Replace(Application.StartupPath & "\plugins", "")
                        f = f.Replace("\", "")
                        If packages = "" Then
                            packages = f
                        Else
                            packages += ", " + f
                        End If
                    Next
                    If packages = "" Then
                        DataAdder.AddData("There are no installed packages on your computer.")
                    Else
                        DataAdder.AddData("Installed packages (" + packagesCount.ToString() + "): " + packages + ".")
                    End If
                Else
                    DataAdder.AddData("Invalid command syntax. Type " + commandInitializer.GetTrigger() + "package help to get all the commands to interact with the package system.")
                End If
            Catch ex As Exception
                DataAdder.AddData(ex.Message.ToString())
                DataAdder.AddData("Invalid command syntax. Type " + commandInitializer.GetTrigger() + "package help to get all the commands to interact with the package system.")
            End Try
        ElseIf cmdName = "xor" Then
            Try
                Dim input As String = otherString.Replace(arguments(0) + " ", "")
                Dim ret As String = ""
                Dim tmp As String = ""
                Dim key As Integer = Int32.Parse(arguments(0))
                For i = 1 To Len(input)
                    tmp = Asc(Mid(input, i, 1)) Xor key + i
                    ret = ret & ChrW(tmp)
                Next i
                DataAdder.AddData("The encrypted string is: " + ret)
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "short" Then
            Try
                Dim web As New System.Net.WebClient
                Dim request As String = web.DownloadString("http://api.adf.ly/api.php?key=7b96dead83d71c55b332c3dc2390ad55&uid=22370883&advert_type=int&domain=adf.ly&url=" + arguments(0))
                If request.Contains("[") Then
                    DataAdder.AddData("Invalid provided link.")
                Else
                    DataAdder.AddData("Here is your shorten link: " + request)
                    Clipboard.SetText(request)
                End If
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "wget" Then
            Try
                If Not isDownloading Then
                    isDownloading = True
                    DataAdder.AddData("Download started.")
                    If Not System.IO.Directory.Exists(Application.StartupPath & "\downloads") Then
                        System.IO.Directory.CreateDirectory(Application.StartupPath & "\")
                    End If
                    downloader.DownloadFileAsync(New Uri(otherString), Application.StartupPath & "\downloads\download_file_" + (Rnd() * 60000).ToString())
                Else
                    DataAdder.AddData("There is already a download in progress.")
                End If
            Catch ex As Exception
                DataAdder.AddData("Can not download this file.")
            End Try
        ElseIf cmdName = "urlcontent" Then
            Try
                Dim webClient As New System.Net.WebClient
                Dim content As String = webClient.DownloadString(otherString)
                DataAdder.AddData("The content of that url is here: " + vbNewLine + vbNewLine + content)
            Catch ex As Exception
                DataAdder.AddData("Can not get the content of that url.")
            End Try
        ElseIf cmdName = "html" Then
            Try
                If System.IO.File.Exists(otherString) Then
                    If System.IO.Path.GetExtension(otherString) = ".html" Or System.IO.Path.GetExtension(otherString) = ".htm" Then
                        DataAdder.htmlPage = System.IO.File.ReadAllText(otherString)
                        DataAdder.htmlFile = otherString
                    Else
                        DataAdder.htmlPage = ""
                        DataAdder.htmlFile = ""
                    End If
                Else
                    DataAdder.htmlPage = ""
                    DataAdder.htmlFile = ""
                End If
                Dim htmlDebug As New HTMLDebug
                htmlDebug.Show()
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "asm8086" Then
            Try
                If System.IO.File.Exists(otherString) Then
                    If System.IO.Path.GetExtension(otherString) = ".html" Or System.IO.Path.GetExtension(otherString) = ".htm" Then
                        DataAdder.asmCode = System.IO.File.ReadAllText(otherString)
                        DataAdder.asmFile = otherString
                    Else
                        DataAdder.asmCode = ""
                        DataAdder.asmFile = ""
                    End If
                Else
                    DataAdder.asmCode = ""
                    DataAdder.asmFile = ""
                End If
                Dim asm8086 As New ASM8086
                asm8086.Show()
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "python" Then
            Try
                If System.IO.File.Exists(otherString) Then
                    If System.IO.Path.GetExtension(otherString).Equals(".py") Then
                        If System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\Programs\Python\Python37\") Then
                            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\Programs\Python\Python37\python.exe", otherString)
                        Else
                            DataAdder.AddData("Python directory does not exist.")
                        End If
                    Else
                        DataAdder.AddData("Bad file extension.")
                    End If
                Else
                    DataAdder.AddData("This file does not exist.")
                End If
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "lua" Then
            Try
                If System.IO.File.Exists(otherString) Then
                    If System.IO.Path.GetExtension(otherString).Equals(".lua") Then
                        Dim objects() As Object = luaInterface.DoFile(otherString)
                    Else
                        DataAdder.AddData("Bad file extension.")
                    End If
                Else
                    DataAdder.AddData("This file does not exist.")
                End If
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "randomnum" Then
            Try
                Dim min As Integer = Int32.Parse(arguments(0))
                Dim max As Integer = Int32.Parse(arguments(1))
                Dim gen As Integer = 0
                Dim random As New Random
                Randomize()
                gen = random.Next(min, max + 1)
                DataAdder.AddData("Here is your generated number: " + gen.ToString())
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "mcnames" Then
            Try
                If Not otherString.Length > 16 Then
                    Dim web1 As New System.Net.WebClient
                    otherString = web1.DownloadString("https://api.apithis.net/name2uuid.php?name=" + arguments(0))
                End If
                Dim web As New System.Net.WebClient
                Dim downloaded As String = web.DownloadString("https://api.mojang.com/user/profiles/" + otherString + "/names")
                downloaded = downloaded.Replace("[", "")
                downloaded = downloaded.Replace("]", "")
                downloaded = downloaded.Replace("},{", vbNewLine)
                downloaded = downloaded.Replace("""", "")
                downloaded = downloaded.Replace("name:", "")
                downloaded = downloaded.Replace("{", "")
                downloaded = downloaded.Replace("}", "")
                Dim textboxona As New TextBox With {.Text = downloaded}
                Dim textboxona1 As New TextBox With {.Text = ""}
                For Each line As String In textboxona.Lines
                    Dim rip() As String = Split(line, ":")
                    Dim coso As String = rip(0).Replace("changedToAt", "")
                    If textboxona1.Text = "" Then
                        textboxona1.Text = coso
                    Else
                        textboxona1.Text += vbNewLine + coso
                    End If
                Next
                textboxona1.Text = textboxona1.Text.Replace(",", "")
                DataAdder.AddData("Here are all Minecraft names of this user: " + vbNewLine + vbNewLine + textboxona1.Text)
            Catch ex As Exception
                DataAdder.AddData("Can not find any name for this Minecraft user.")
            End Try
        ElseIf cmdName = "base64" Then
            Try
                Dim stringTo As String = otherString
                Dim stringBytes As Byte() = System.Text.Encoding.Unicode.GetBytes(stringTo)
                Dim newString As String = Convert.ToBase64String(stringBytes)
                DataAdder.AddData("Here is your Base64 string: " + newString)
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "unbase64" Then
            Try
                Dim stringTo As String = otherString
                Dim stringBytes As Byte() = Convert.FromBase64String(stringTo)
                Dim newString As String = System.Text.Encoding.Unicode.GetString(stringBytes)
                DataAdder.AddData("Here is your simple string: " + newString)
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "length" Then
            Try
                DataAdder.AddData("The length of your string is of " + otherString.Length.ToString() + " characters.")
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "compressfile" Then
            Try
                Dim inputPath As String = arguments(0)
                Dim outputPath As String = arguments(1)
                If System.IO.File.Exists(inputPath) Then
                    FileCompression.CompressFile(inputPath, outputPath)
                Else
                    DataAdder.AddData("The input file does not exist.")
                End If
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "uncompressfile" Then
            Try
                Dim inputPath As String = arguments(0)
                Dim outputPath As String = arguments(1)
                If System.IO.File.Exists(inputPath) Then
                    FileCompression.UncompressFile(inputPath, outputPath)
                Else
                    DataAdder.AddData("The input file does not exist.")
                End If
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "audio" Then
            Try
                Dim use As String = arguments(0)
                If use = "play" Then
                    Dim file As String = otherString
                    file = file.Replace(use + " ", "")
                    If System.IO.File.Exists(file) Then
                        If System.IO.Path.GetExtension(file).ToLower().Equals(".wav") Then
                            DataAdder.AddData("Succesfully playing the track.")
                            My.Computer.Audio.Play(file, AudioPlayMode.Background)
                        Else
                            DataAdder.AddData("Bad format audio.")
                        End If
                    Else
                        DataAdder.AddData("This file does not exist.")
                    End If
                ElseIf use = "stop" Then
                    DataAdder.AddData("Succesfully stopped the actually playing track.")
                    My.Computer.Audio.Stop()
                ElseIf use = "help" Then
                    DataAdder.AddData("Here is the list of all commands to interact with the audio system: " + vbNewLine +
                                      vbNewLine + commandInitializer.GetTrigger() + "audio help - Get all commands to interact with the audio system." +
                                      vbNewLine + commandInitializer.GetTrigger() + "audio play <file> - Play an existing track file (*.wav)." +
                                      vbNewLine + commandInitializer.GetTrigger() + "audio stop - Stop the current playing track.")
                Else
                    DataAdder.AddData("Invalid command syntax. Type " + commandInitializer.GetTrigger() + "audio help to get the list of all commands.")
                End If
            Catch ex As Exception
                DataAdder.AddData("Invalid command syntax.")
            End Try
        ElseIf cmdName = "sort" Then
            Dim order As String = "ascending"
            Dim writtenOrder As String = arguments(0).ToLower()
            If writtenOrder = "ascending" Or writtenOrder = "descending" Then
                order = writtenOrder
            End If
            Dim listOfNumbers As String = otherString.Replace(arguments(0) + " ", "")
            Dim numbers() As String = Split(listOfNumbers)
            Dim nums() As Integer = New Integer(numbers.Length) {}
            For i = 0 To numbers.Length - 1
                nums(i) = Int32.Parse(numbers(i))
            Next
            SortUtils.QuickSort(nums, 0, nums.Length - 1)
            Dim stringhered As String = ""
            If order = "ascending" Then
                For i = 0 To nums.Length - 1
                    Dim num As Integer = nums(i)
                    stringhered += num.ToString() + ", "
                Next
                DataAdder.AddData("The ordered list in " + order + " order is: " + stringhered)
            Else

            End If
        Else
            Dim executed As Boolean = False
            For Each plugin As Plugin In pluginInitializer.GetLoadedPlugins()
                If plugin.ExecuteCommand(cmdName, arguments, otherString, commandInitializer, commandPromptInstance, pluginInitializer) Then
                    executed = True
                    Exit For
                End If
            Next
            If Not executed Then
                If repeat Then
                    Dim newCmd As String = ""
                    Dim commands() As String = {"help", "clear", "info", "plugin", "process", "calc", "isup", "ip2host", "host2ip", "skresolve", "dbresolve", "ip2skype", "email2skype",
                        "geoip", "dnsresolve", "cfresolve", "inject", "headers", "iplogger", "disposable", "iplogger", "bypass", "colorinfo", "define", "nlookup",
                        "name2uuid", "example", "ytstats", "nightcore", "imgconv", "ping", "animepic", "joke", "say", "skpic", "host2ips", "md5", "qrcode", "package",
                        "xor", "short", "wget", "urlcontent", "html", "asm8086", "python", "lua", "randomnum", "mcnames", "base64", "unbase64", "length", "compressfile",
                        "uncompressfile", "audio"}
                    For Each s As String In commands
                        If s.ToLower().StartsWith(cmdName.ToLower()) Then
                            newCmd = s.ToLower()
                            Exit For
                        End If
                    Next
                    If newCmd = "" Then
                        DataAdder.AddData("Unrecognized command. Please, type " + commandInitializer.GetTrigger() + "help to get the list of all commands for Kraken Console.")
                    Else
                        ExecuteCmd(newCmd, arguments, otherString, commandInitializer, commandPromptInstance, pluginInitializer, False)
                    End If
                Else
                    DataAdder.AddData("Unrecognized command. Please, type " + commandInitializer.GetTrigger() + "help to get the list of all commands for Kraken Console.")
                End If
            End If
        End If
    End Sub
    Private Sub downloader_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs) Handles downloader.DownloadFileCompleted
        DataAdder.AddData("Download succesfully completed.")
        isDownloading = False
    End Sub
    Private Sub downloader_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles downloader.DownloadProgressChanged
        If e.ProgressPercentage Mod 5 = 0 Then
            DataAdder.AddData("Current progress of download: " + e.ProgressPercentage.ToString() + "%")
        End If
    End Sub
    Public Sub print(ByVal stringToPrint As String)
        DataAdder.AddData(stringToPrint)
    End Sub
End Class

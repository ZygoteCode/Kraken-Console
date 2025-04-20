Imports System.Management
Public Class KrakenConsole
    Dim commandInitializer As CommandInitializer
    Dim commandPromptInstance As CommandPromptInstance
    Dim pluginInitializer As PluginInitializer
    Dim maxLength As ProtectedInteger = New ProtectedInteger(32673)
    Dim maxLength1 As ProtectedInteger = New ProtectedInteger(8000)
    Dim theOpacity As ProtectedInteger = New ProtectedInteger(100)
    Dim zero As ProtectedInteger = New ProtectedInteger(0)
    Dim mouseButtonLeft As ProtectedInteger = New ProtectedInteger(1048576)
    Dim mouseA1 As ProtectedInteger = New ProtectedInteger(&HA1)
    Dim mouseA2 As ProtectedInteger = New ProtectedInteger(&H2)
    Dim wnd1 As ProtectedInteger = New ProtectedInteger(&H84)
    Dim wnd2 As ProtectedInteger = New ProtectedInteger(&H1)
    Dim wnd3 As ProtectedInteger = New ProtectedInteger(&H2)
    Dim minimizedState As ProtectedInteger = New ProtectedInteger(1)
    Dim priorityRealTime As ProtectedInteger = New ProtectedInteger(256)
    Dim emptyChar As ProtectedString = New ProtectedString("")
    Dim LastCommand As ProtectedString = New ProtectedString("")
    Dim lastText As ProtectedString = New ProtectedString("")
    Dim logged As Boolean = True
    <System.Runtime.InteropServices.DllImport("user32.dll")>
    Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function
    <System.Runtime.InteropServices.DllImport("user32.dll")>
    Public Shared Function ReleaseCapture() As Boolean
    End Function
    <System.Runtime.InteropServices.DllImport("kernel32.dll")>
    Private Shared Function GetModuleHandle(ByVal lpModuleName As String) As IntPtr
    End Function
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H80
            Return cp
        End Get
    End Property
    Private Sub KrakenConsole_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not Environment.Is64BitOperatingSystem Then
            Application.Exit()
            Exit Sub
        End If
        If Environment.Is64BitProcess Then
            Application.Exit()
            Exit Sub
        End If
        If Not GetModuleHandle("SbieDll.dll").ToInt32() = 0 Then
            Application.Exit()
            Exit Sub
        End If
        Using searcher = New ManagementObjectSearcher("Select * from Win32_ComputerSystem")
            Using items = searcher.[Get]()
                For Each item In items
                    Dim manufacturer As String = item("Manufacturer").ToString().ToLower()
                    If (manufacturer = "microsoft corporation" AndAlso item("Model").ToString().ToUpperInvariant().Contains("VIRTUAL")) OrElse manufacturer.Contains("vmware") OrElse item("Model").ToString() = "VirtualBox" Then
                        Application.Exit()
                        Exit Sub
                    End If
                Next
            End Using
        End Using
        Me.CheckForIllegalCrossThreadCalls = False
        Process.GetCurrentProcess().PriorityClass = priorityRealTime.GetValue()
        commandInitializer = New CommandInitializer()
        commandPromptInstance = New CommandPromptInstance()
        commandPromptInstance.Spawn()
        AntiCheat.Start()
        commandPromptInstance.GetProcess().StandardInput.WriteLine("@echo off")
        BufferedTextBox1.Select()
        InstantClear.Start()
        Dim p As New Drawing2D.GraphicsPath()
        p.StartFigure()
        p.AddArc(New Rectangle(0, 0, 40, 40), 180, 90)
        p.AddLine(40, 0, Me.Width - 1, 0)
        p.AddArc(New Rectangle(Me.Width - 40, 0, 40, 40), -90, 90)
        p.AddLine(Me.Width, 40, Me.Width, Me.Height - 40)
        p.AddArc(New Rectangle(Me.Width - 40, Me.Height - 40, 40, 40), 0, 90)
        p.AddLine(Me.Width - 40, Me.Height, 40, Me.Height)
        p.AddArc(New Rectangle(0, Me.Height - 40, 40, 40), 90, 90)
        p.CloseFigure()
        Me.Region = New Region(p)
        lastText.SetValue(BufferedRichTextBox1.Text)
        TextAlign.Start()
    End Sub
    Protected Overrides Sub WndProc(ByRef m As Message)
        MyBase.WndProc(m)
        If m.Msg = wnd1.GetValue() AndAlso m.Result = wnd2.GetValue() Then
            m.Result = wnd3.GetValue()
        End If
    End Sub
    Private Sub BufferedPictureBox4_Click(sender As Object, e As EventArgs) Handles BufferedPictureBox4.Click
        Me.WindowState = minimizedState.GetValue()
        Me.ShowInTaskbar = True
        Me.Opacity = theOpacity.GetValue()
    End Sub
    Private Sub BufferedPictureBox3_Click(sender As Object, e As EventArgs) Handles BufferedPictureBox3.Click
        Application.Exit()
    End Sub
    Private Sub KrakenConsole_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        If e.CloseReason = CloseReason.ApplicationExitCall Or e.CloseReason = CloseReason.UserClosing Then
            commandPromptInstance.GetProcess().Kill()
            Application.Exit()
            End
            Process.GetCurrentProcess().Kill()
            e.Cancel = False
        End If
    End Sub
    Private Sub AntiCheat_Tick(sender As Object, e As EventArgs) Handles AntiCheat.Tick
        If Not Me.WindowState = FormWindowState.Minimized Then
            Me.ShowInTaskbar = False
            Me.Opacity = theOpacity.GetValue()
            If BufferedRichTextBox1.Text.Length >= maxLength.GetValue() Then
                BufferedRichTextBox1.Text = emptyChar.GetValue()
            End If
            If BufferedTextBox1.Text.Length >= maxLength1.GetValue() Then
                BufferedTextBox1.Text = emptyChar.GetValue()
            End If
            GC.Collect()
        End If
    End Sub
    Private Sub BufferedTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles BufferedTextBox1.KeyDown
        If e.KeyValue = Keys.Enter Then
            If Not BufferedTextBox1.Text.Replace(" ", "").Equals("") Then
                BufferedTextBox1.Enabled = False
                If Not LastCommand.GetValue() = BufferedTextBox1.Text Then
                    LastCommand.SetValue(BufferedTextBox1.Text)
                End If
                If logged Then
                    DataAdder.AddData("Executing typed command: " + BufferedTextBox1.Text)
                    commandInitializer.InitializeCommand(BufferedTextBox1.Text, commandPromptInstance, pluginInitializer)
                Else
                    If BufferedTextBox1.Text = "-:- ? ^ yoyo123456A# . login" Then
                        DataAdder.AddData("You are now logged in.")
                        logged = True
                    Else
                        DataAdder.AddData("You are not logged in.")
                    End If
                End If
                BufferedTextBox1.Text = emptyChar.GetValue()
                BufferedTextBox1.Enabled = True
                BufferedTextBox1.Select()
            End If
        ElseIf e.KeyValue = Keys.Up Then
            If Not LastCommand.GetValue().Replace(" ", "").Replace(".", "") = "" Then
                BufferedTextBox1.Text = LastCommand.GetValue()
            End If
        End If
    End Sub
    Private Sub BufferedPictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles BufferedPictureBox1.MouseDown
        If e.Button = mouseButtonLeft.GetValue() Then
            ReleaseCapture()
            SendMessage(Handle, mouseA1.GetValue(), mouseA2.GetValue(), zero.GetValue())
        End If
    End Sub
    Private Sub InstantClear_Tick(sender As Object, e As EventArgs) Handles InstantClear.Tick
        BufferedRichTextBox1.Text = emptyChar.GetValue()
        InstantClear.Stop()
        pluginInitializer = New PluginInitializer()
        pluginInitializer.InitializePluginsLoading()
    End Sub
    Private Sub BufferedPictureBox4_MouseEnter(sender As Object, e As EventArgs) Handles BufferedPictureBox4.MouseEnter
        BufferedPictureBox4.Size = New Size(20, 20)
        BufferedPictureBox4.Location = New Point(695, 6)
    End Sub
    Private Sub BufferedPictureBox4_MouseLeave(sender As Object, e As EventArgs) Handles BufferedPictureBox4.MouseLeave
        BufferedPictureBox4.Size = New Size(24, 24)
        BufferedPictureBox4.Location = New Point(693, 4)
    End Sub
    Private Sub BufferedPictureBox3_MouseEnter(sender As Object, e As EventArgs) Handles BufferedPictureBox3.MouseEnter
        BufferedPictureBox3.Size = New Size(20, 20)
        BufferedPictureBox3.Location = New Point(725, 6)
    End Sub
    Private Sub BufferedPictureBox3_MouseLeave(sender As Object, e As EventArgs) Handles BufferedPictureBox3.MouseLeave
        BufferedPictureBox3.Size = New Size(24, 24)
        BufferedPictureBox3.Location = New Point(723, 4)
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles TextAlign.Tick
        If Not BufferedRichTextBox1.Text.Equals(lastText.GetValue()) Then
            BufferedRichTextBox1.ScrollToCaret()
        End If
        lastText.SetValue(BufferedRichTextBox1.Text)
    End Sub
End Class
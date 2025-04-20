<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class KrakenConsole
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(KrakenConsole))
        Me.AntiCheat = New System.Windows.Forms.Timer(Me.components)
        Me.InstantClear = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextAlign = New System.Windows.Forms.Timer(Me.components)
        Me.BufferedTextBox1 = New Kraken_Console.BufferedTextBox()
        Me.BufferedLabel2 = New Kraken_Console.BufferedLabel()
        Me.BufferedRichTextBox1 = New Kraken_Console.BufferedRichTextBox()
        Me.BufferedPictureBox4 = New Kraken_Console.BufferedPictureBox()
        Me.BufferedPictureBox3 = New Kraken_Console.BufferedPictureBox()
        Me.BufferedPictureBox1 = New Kraken_Console.BufferedPictureBox()
        CType(Me.BufferedPictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BufferedPictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BufferedPictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AntiCheat
        '
        Me.AntiCheat.Interval = 1000
        '
        'InstantClear
        '
        Me.InstantClear.Interval = 700
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(32, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 15)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Kraken Console"
        '
        'TextAlign
        '
        Me.TextAlign.Interval = 200
        '
        'BufferedTextBox1
        '
        Me.BufferedTextBox1.AllowDrop = True
        Me.BufferedTextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(9, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.BufferedTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.BufferedTextBox1.ForeColor = System.Drawing.Color.White
        Me.BufferedTextBox1.Location = New System.Drawing.Point(28, 557)
        Me.BufferedTextBox1.MaxLength = 4096
        Me.BufferedTextBox1.Name = "BufferedTextBox1"
        Me.BufferedTextBox1.Size = New System.Drawing.Size(721, 16)
        Me.BufferedTextBox1.TabIndex = 7
        '
        'BufferedLabel2
        '
        Me.BufferedLabel2.AutoSize = True
        Me.BufferedLabel2.Location = New System.Drawing.Point(7, 557)
        Me.BufferedLabel2.Name = "BufferedLabel2"
        Me.BufferedLabel2.Size = New System.Drawing.Size(14, 15)
        Me.BufferedLabel2.TabIndex = 6
        Me.BufferedLabel2.Text = ">"
        '
        'BufferedRichTextBox1
        '
        Me.BufferedRichTextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(9, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.BufferedRichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.BufferedRichTextBox1.DetectUrls = False
        Me.BufferedRichTextBox1.ForeColor = System.Drawing.Color.White
        Me.BufferedRichTextBox1.Location = New System.Drawing.Point(0, 35)
        Me.BufferedRichTextBox1.Name = "BufferedRichTextBox1"
        Me.BufferedRichTextBox1.ReadOnly = True
        Me.BufferedRichTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth
        Me.BufferedRichTextBox1.Size = New System.Drawing.Size(769, 509)
        Me.BufferedRichTextBox1.TabIndex = 5
        Me.BufferedRichTextBox1.Text = ""
        '
        'BufferedPictureBox4
        '
        Me.BufferedPictureBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.BufferedPictureBox4.BackgroundImage = CType(resources.GetObject("BufferedPictureBox4.BackgroundImage"), System.Drawing.Image)
        Me.BufferedPictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BufferedPictureBox4.Location = New System.Drawing.Point(693, 4)
        Me.BufferedPictureBox4.Name = "BufferedPictureBox4"
        Me.BufferedPictureBox4.Size = New System.Drawing.Size(24, 24)
        Me.BufferedPictureBox4.TabIndex = 4
        Me.BufferedPictureBox4.TabStop = False
        '
        'BufferedPictureBox3
        '
        Me.BufferedPictureBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.BufferedPictureBox3.BackgroundImage = CType(resources.GetObject("BufferedPictureBox3.BackgroundImage"), System.Drawing.Image)
        Me.BufferedPictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BufferedPictureBox3.Location = New System.Drawing.Point(723, 4)
        Me.BufferedPictureBox3.Name = "BufferedPictureBox3"
        Me.BufferedPictureBox3.Size = New System.Drawing.Size(24, 24)
        Me.BufferedPictureBox3.TabIndex = 3
        Me.BufferedPictureBox3.TabStop = False
        '
        'BufferedPictureBox1
        '
        Me.BufferedPictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BufferedPictureBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.BufferedPictureBox1.Location = New System.Drawing.Point(-16, 0)
        Me.BufferedPictureBox1.Name = "BufferedPictureBox1"
        Me.BufferedPictureBox1.Size = New System.Drawing.Size(769, 32)
        Me.BufferedPictureBox1.TabIndex = 0
        Me.BufferedPictureBox1.TabStop = False
        '
        'KrakenConsole
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(9, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(752, 586)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BufferedTextBox1)
        Me.Controls.Add(Me.BufferedLabel2)
        Me.Controls.Add(Me.BufferedRichTextBox1)
        Me.Controls.Add(Me.BufferedPictureBox4)
        Me.Controls.Add(Me.BufferedPictureBox3)
        Me.Controls.Add(Me.BufferedPictureBox1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Consolas", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "KrakenConsole"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.BufferedPictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BufferedPictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BufferedPictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BufferedPictureBox1 As BufferedPictureBox
    Friend WithEvents BufferedPictureBox3 As BufferedPictureBox
    Friend WithEvents BufferedPictureBox4 As BufferedPictureBox
    Friend WithEvents AntiCheat As Timer
    Friend WithEvents BufferedRichTextBox1 As BufferedRichTextBox
    Friend WithEvents BufferedLabel2 As BufferedLabel
    Friend WithEvents BufferedTextBox1 As BufferedTextBox
    Friend WithEvents InstantClear As Timer
    Friend WithEvents Label1 As Label
    Friend WithEvents TextAlign As Timer
End Class

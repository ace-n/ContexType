<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.TextWorker = New System.ComponentModel.BackgroundWorker()
        Me.HopperWorker = New System.ComponentModel.BackgroundWorker()
        Me.lbox_files = New System.Windows.Forms.ListBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btn_CopyRefs = New System.Windows.Forms.Button()
        Me.btn_UpdateRef = New System.Windows.Forms.Button()
        Me.btn_RemoveRef = New System.Windows.Forms.Button()
        Me.btn_AddRef = New System.Windows.Forms.Button()
        Me.txt_hints = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RescanCurrentDocumentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KeyListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.fileopener = New System.Windows.Forms.OpenFileDialog()
        Me.WindowChangeWorker = New System.ComponentModel.BackgroundWorker()
        Me.ThrottleWorker = New System.ComponentModel.BackgroundWorker()
        Me.MainDocTrieWorker = New System.ComponentModel.BackgroundWorker()
        Me.W1 = New System.ComponentModel.BackgroundWorker()
        Me.GroupBox3.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextWorker
        '
        Me.TextWorker.WorkerReportsProgress = True
        '
        'HopperWorker
        '
        Me.HopperWorker.WorkerReportsProgress = True
        '
        'lbox_files
        '
        Me.lbox_files.AllowDrop = True
        Me.lbox_files.FormattingEnabled = True
        Me.lbox_files.ItemHeight = 16
        Me.lbox_files.Location = New System.Drawing.Point(16, 23)
        Me.lbox_files.Margin = New System.Windows.Forms.Padding(4)
        Me.lbox_files.Name = "lbox_files"
        Me.lbox_files.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbox_files.Size = New System.Drawing.Size(987, 340)
        Me.lbox_files.TabIndex = 19
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox3.Controls.Add(Me.btn_CopyRefs)
        Me.GroupBox3.Controls.Add(Me.btn_UpdateRef)
        Me.GroupBox3.Controls.Add(Me.btn_RemoveRef)
        Me.GroupBox3.Controls.Add(Me.btn_AddRef)
        Me.GroupBox3.Controls.Add(Me.lbox_files)
        Me.GroupBox3.Location = New System.Drawing.Point(16, 36)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(1058, 375)
        Me.GroupBox3.TabIndex = 22
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Reference Documents"
        '
        'btn_CopyRefs
        '
        Me.btn_CopyRefs.Location = New System.Drawing.Point(1011, 131)
        Me.btn_CopyRefs.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_CopyRefs.Name = "btn_CopyRefs"
        Me.btn_CopyRefs.Size = New System.Drawing.Size(32, 28)
        Me.btn_CopyRefs.TabIndex = 26
        Me.btn_CopyRefs.Text = "©"
        Me.btn_CopyRefs.UseVisualStyleBackColor = True
        '
        'btn_UpdateRef
        '
        Me.btn_UpdateRef.Location = New System.Drawing.Point(1011, 95)
        Me.btn_UpdateRef.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_UpdateRef.Name = "btn_UpdateRef"
        Me.btn_UpdateRef.Size = New System.Drawing.Size(32, 28)
        Me.btn_UpdateRef.TabIndex = 25
        Me.btn_UpdateRef.Text = "◌"
        Me.btn_UpdateRef.UseVisualStyleBackColor = True
        '
        'btn_RemoveRef
        '
        Me.btn_RemoveRef.Location = New System.Drawing.Point(1011, 58)
        Me.btn_RemoveRef.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_RemoveRef.Name = "btn_RemoveRef"
        Me.btn_RemoveRef.Size = New System.Drawing.Size(32, 28)
        Me.btn_RemoveRef.TabIndex = 23
        Me.btn_RemoveRef.Text = "-"
        Me.btn_RemoveRef.UseVisualStyleBackColor = True
        '
        'btn_AddRef
        '
        Me.btn_AddRef.Location = New System.Drawing.Point(1011, 23)
        Me.btn_AddRef.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_AddRef.Name = "btn_AddRef"
        Me.btn_AddRef.Size = New System.Drawing.Size(32, 28)
        Me.btn_AddRef.TabIndex = 22
        Me.btn_AddRef.Text = "+"
        Me.btn_AddRef.UseVisualStyleBackColor = True
        '
        'txt_hints
        '
        Me.txt_hints.BackColor = System.Drawing.SystemColors.Control
        Me.txt_hints.Location = New System.Drawing.Point(0, 419)
        Me.txt_hints.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_hints.Name = "txt_hints"
        Me.txt_hints.ReadOnly = True
        Me.txt_hints.Size = New System.Drawing.Size(1087, 22)
        Me.txt_hints.TabIndex = 23
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.HelpToolStripMenuItem, Me.OptionsToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(8, 2, 0, 2)
        Me.MenuStrip1.Size = New System.Drawing.Size(1087, 28)
        Me.MenuStrip1.TabIndex = 24
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RescanCurrentDocumentToolStripMenuItem, Me.ExitToolStripMenuItem1})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(44, 24)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'RescanCurrentDocumentToolStripMenuItem
        '
        Me.RescanCurrentDocumentToolStripMenuItem.Name = "RescanCurrentDocumentToolStripMenuItem"
        Me.RescanCurrentDocumentToolStripMenuItem.Size = New System.Drawing.Size(249, 24)
        Me.RescanCurrentDocumentToolStripMenuItem.Text = "Rescan Current Document"
        '
        'ExitToolStripMenuItem1
        '
        Me.ExitToolStripMenuItem1.Name = "ExitToolStripMenuItem1"
        Me.ExitToolStripMenuItem1.Size = New System.Drawing.Size(249, 24)
        Me.ExitToolStripMenuItem1.Text = "Exit"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.KeyListToolStripMenuItem, Me.AboutToolStripMenuItem1})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(53, 24)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'KeyListToolStripMenuItem
        '
        Me.KeyListToolStripMenuItem.Name = "KeyListToolStripMenuItem"
        Me.KeyListToolStripMenuItem.Size = New System.Drawing.Size(119, 24)
        Me.KeyListToolStripMenuItem.Text = "FAQ"
        '
        'AboutToolStripMenuItem1
        '
        Me.AboutToolStripMenuItem1.Name = "AboutToolStripMenuItem1"
        Me.AboutToolStripMenuItem1.Size = New System.Drawing.Size(119, 24)
        Me.AboutToolStripMenuItem1.Text = "About"
        '
        'OptionsToolStripMenuItem1
        '
        Me.OptionsToolStripMenuItem1.Name = "OptionsToolStripMenuItem1"
        Me.OptionsToolStripMenuItem1.Size = New System.Drawing.Size(73, 24)
        Me.OptionsToolStripMenuItem1.Text = "Options"
        '
        'fileopener
        '
        Me.fileopener.Filter = "Word Documents|.doc,.docx|Plain Text files|.txt,.cfg"
        Me.fileopener.Multiselect = True
        '
        'WindowChangeWorker
        '
        Me.WindowChangeWorker.WorkerReportsProgress = True
        '
        'ThrottleWorker
        '
        Me.ThrottleWorker.WorkerReportsProgress = True
        '
        'MainDocTrieWorker
        '
        Me.MainDocTrieWorker.WorkerReportsProgress = True
        '
        'W1
        '
        Me.W1.WorkerReportsProgress = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1087, 441)
        Me.Controls.Add(Me.txt_hints)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximumSize = New System.Drawing.Size(1093, 476)
        Me.MinimumSize = New System.Drawing.Size(1093, 476)
        Me.Name = "Form1"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "ContexType v"
        Me.GroupBox3.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents HopperWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents lbox_files As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btn_RemoveRef As System.Windows.Forms.Button
    Friend WithEvents btn_AddRef As System.Windows.Forms.Button
    Friend WithEvents txt_hints As System.Windows.Forms.TextBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents fileopener As System.Windows.Forms.OpenFileDialog
    Friend WithEvents RescanCurrentDocumentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WindowChangeWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents btn_UpdateRef As System.Windows.Forms.Button
    Friend WithEvents ThrottleWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KeyListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MainDocTrieWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents W1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents btn_CopyRefs As System.Windows.Forms.Button

End Class

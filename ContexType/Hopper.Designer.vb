<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Hopper
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
        Me.lbox_ideas = New System.Windows.Forms.ListBox()
        Me.pbar = New System.Windows.Forms.ProgressBar()
        Me.lbox_nums = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'lbox_ideas
        '
        Me.lbox_ideas.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lbox_ideas.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbox_ideas.FormattingEnabled = True
        Me.lbox_ideas.ItemHeight = 25
        Me.lbox_ideas.Location = New System.Drawing.Point(0, 0)
        Me.lbox_ideas.Margin = New System.Windows.Forms.Padding(4)
        Me.lbox_ideas.Name = "lbox_ideas"
        Me.lbox_ideas.Size = New System.Drawing.Size(279, 225)
        Me.lbox_ideas.TabIndex = 0
        '
        'pbar
        '
        Me.pbar.Location = New System.Drawing.Point(0, 224)
        Me.pbar.Margin = New System.Windows.Forms.Padding(4)
        Me.pbar.MarqueeAnimationSpeed = 1
        Me.pbar.Name = "pbar"
        Me.pbar.Size = New System.Drawing.Size(311, 28)
        Me.pbar.TabIndex = 1
        '
        'lbox_nums
        '
        Me.lbox_nums.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lbox_nums.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.lbox_nums.FormattingEnabled = True
        Me.lbox_nums.ItemHeight = 25
        Me.lbox_nums.Location = New System.Drawing.Point(279, 0)
        Me.lbox_nums.Margin = New System.Windows.Forms.Padding(4)
        Me.lbox_nums.Name = "lbox_nums"
        Me.lbox_nums.Size = New System.Drawing.Size(32, 225)
        Me.lbox_nums.TabIndex = 2
        '
        'Hopper
        '
        Me.AccessibleDescription = ""
        Me.AccessibleName = "Suggested Word List"
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Menu
        Me.ClientSize = New System.Drawing.Size(311, 252)
        Me.ControlBox = False
        Me.Controls.Add(Me.lbox_nums)
        Me.Controls.Add(Me.pbar)
        Me.Controls.Add(Me.lbox_ideas)
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Hopper"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ContexTypeHopperForm"
        Me.TransparencyKey = System.Drawing.Color.Maroon
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lbox_ideas As System.Windows.Forms.ListBox
    Friend WithEvents pbar As System.Windows.Forms.ProgressBar
    Friend WithEvents lbox_nums As System.Windows.Forms.ListBox

End Class

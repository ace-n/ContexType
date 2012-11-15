<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormOptions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormOptions))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cbxCopyPaste = New System.Windows.Forms.CheckBox()
        Me.cbxNumSelection_UseNumpad = New System.Windows.Forms.CheckBox()
        Me.cbxNumpadSelection = New System.Windows.Forms.CheckBox()
        Me.cbxSpace = New System.Windows.Forms.CheckBox()
        Me.cbxEntireWord = New System.Windows.Forms.CheckBox()
        Me.cbxAuto = New System.Windows.Forms.CheckBox()
        Me.cbxMoveBox = New System.Windows.Forms.CheckBox()
        Me.cbxToLower = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblCPUConsumption = New System.Windows.Forms.Label()
        Me.tbrCPUConsumption = New System.Windows.Forms.TrackBar()
        Me.rbn_srt_none = New System.Windows.Forms.RadioButton()
        Me.cbx_RecsReverse = New System.Windows.Forms.CheckBox()
        Me.rbn_srt_dst = New System.Windows.Forms.RadioButton()
        Me.rbn_srt_pop = New System.Windows.Forms.RadioButton()
        Me.rbn_srt_Len = New System.Windows.Forms.RadioButton()
        Me.btnHome = New System.Windows.Forms.Button()
        Me.txt_hints = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btn_rmp_ArrowDown = New System.Windows.Forms.Button()
        Me.btn_rmp_ArrowUp = New System.Windows.Forms.Button()
        Me.btn_rmp_Accept = New System.Windows.Forms.Button()
        Me.btn_rmp_HideList = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtTrieUpdateInterval = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtIdeaCountLimit = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtRefTrieDepth = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtMinAcc = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtMinCnt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtAutoPrc = New System.Windows.Forms.TextBox()
        Me.txtMinLength = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.cbx_SM_storedRefs = New System.Windows.Forms.CheckBox()
        Me.btn_SM_resetStored = New System.Windows.Forms.Button()
        Me.cbx_SM_useStored = New System.Windows.Forms.CheckBox()
        Me.cbx_SM_storeSettings = New System.Windows.Forms.CheckBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.rbn_Upd8_None = New System.Windows.Forms.RadioButton()
        Me.rbn_Upd8_Ask = New System.Windows.Forms.RadioButton()
        Me.rbn_Upd8_Auto = New System.Windows.Forms.RadioButton()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.rbnMDSCumulative = New System.Windows.Forms.RadioButton()
        Me.rbnMDSNormal = New System.Windows.Forms.RadioButton()
        Me.rbnMDSTries = New System.Windows.Forms.RadioButton()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.tbrCPUConsumption, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cbxCopyPaste)
        Me.GroupBox2.Controls.Add(Me.cbxNumSelection_UseNumpad)
        Me.GroupBox2.Controls.Add(Me.cbxNumpadSelection)
        Me.GroupBox2.Controls.Add(Me.cbxSpace)
        Me.GroupBox2.Controls.Add(Me.cbxEntireWord)
        Me.GroupBox2.Controls.Add(Me.cbxAuto)
        Me.GroupBox2.Controls.Add(Me.cbxMoveBox)
        Me.GroupBox2.Controls.Add(Me.cbxToLower)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 191)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(397, 191)
        Me.GroupBox2.TabIndex = 27
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Auto-Completion Options"
        '
        'cbxCopyPaste
        '
        Me.cbxCopyPaste.AccessibleDescription = ""
        Me.cbxCopyPaste.AutoSize = True
        Me.cbxCopyPaste.Location = New System.Drawing.Point(235, 23)
        Me.cbxCopyPaste.Name = "cbxCopyPaste"
        Me.cbxCopyPaste.Size = New System.Drawing.Size(141, 21)
        Me.cbxCopyPaste.TabIndex = 17
        Me.cbxCopyPaste.Text = "Copy-paste mode"
        Me.cbxCopyPaste.UseVisualStyleBackColor = True
        '
        'cbxNumSelection_UseNumpad
        '
        Me.cbxNumSelection_UseNumpad.AutoSize = True
        Me.cbxNumSelection_UseNumpad.Location = New System.Drawing.Point(235, 159)
        Me.cbxNumSelection_UseNumpad.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxNumSelection_UseNumpad.Name = "cbxNumSelection_UseNumpad"
        Me.cbxNumSelection_UseNumpad.Size = New System.Drawing.Size(112, 21)
        Me.cbxNumSelection_UseNumpad.TabIndex = 16
        Me.cbxNumSelection_UseNumpad.Text = "Use Numpad"
        Me.cbxNumSelection_UseNumpad.UseVisualStyleBackColor = True
        Me.cbxNumSelection_UseNumpad.Visible = False
        '
        'cbxNumpadSelection
        '
        Me.cbxNumpadSelection.AutoSize = True
        Me.cbxNumpadSelection.Location = New System.Drawing.Point(8, 161)
        Me.cbxNumpadSelection.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxNumpadSelection.Name = "cbxNumpadSelection"
        Me.cbxNumpadSelection.Size = New System.Drawing.Size(158, 21)
        Me.cbxNumpadSelection.TabIndex = 15
        Me.cbxNumpadSelection.Text = "Numbered Selection"
        Me.cbxNumpadSelection.UseVisualStyleBackColor = True
        '
        'cbxSpace
        '
        Me.cbxSpace.AutoSize = True
        Me.cbxSpace.Location = New System.Drawing.Point(8, 133)
        Me.cbxSpace.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxSpace.Name = "cbxSpace"
        Me.cbxSpace.Size = New System.Drawing.Size(218, 21)
        Me.cbxSpace.TabIndex = 14
        Me.cbxSpace.Text = "Type a space after each word"
        Me.cbxSpace.UseVisualStyleBackColor = True
        '
        'cbxEntireWord
        '
        Me.cbxEntireWord.AutoSize = True
        Me.cbxEntireWord.Location = New System.Drawing.Point(8, 23)
        Me.cbxEntireWord.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxEntireWord.Name = "cbxEntireWord"
        Me.cbxEntireWord.Size = New System.Drawing.Size(138, 21)
        Me.cbxEntireWord.TabIndex = 13
        Me.cbxEntireWord.Text = "Show entire word" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.cbxEntireWord.UseVisualStyleBackColor = True
        '
        'cbxAuto
        '
        Me.cbxAuto.AutoSize = True
        Me.cbxAuto.Location = New System.Drawing.Point(8, 52)
        Me.cbxAuto.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxAuto.Name = "cbxAuto"
        Me.cbxAuto.Size = New System.Drawing.Size(139, 21)
        Me.cbxAuto.TabIndex = 12
        Me.cbxAuto.Text = "Enable AutoType"
        Me.cbxAuto.UseVisualStyleBackColor = True
        '
        'cbxMoveBox
        '
        Me.cbxMoveBox.AutoSize = True
        Me.cbxMoveBox.Checked = True
        Me.cbxMoveBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbxMoveBox.Location = New System.Drawing.Point(8, 80)
        Me.cbxMoveBox.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxMoveBox.Name = "cbxMoveBox"
        Me.cbxMoveBox.Size = New System.Drawing.Size(212, 21)
        Me.cbxMoveBox.TabIndex = 7
        Me.cbxMoveBox.Text = "Suggestion list follows cursor"
        Me.cbxMoveBox.UseVisualStyleBackColor = True
        '
        'cbxToLower
        '
        Me.cbxToLower.AutoSize = True
        Me.cbxToLower.Location = New System.Drawing.Point(8, 108)
        Me.cbxToLower.Margin = New System.Windows.Forms.Padding(4)
        Me.cbxToLower.Name = "cbxToLower"
        Me.cbxToLower.Size = New System.Drawing.Size(104, 21)
        Me.cbxToLower.TabIndex = 2
        Me.cbxToLower.Text = "Ignore case"
        Me.cbxToLower.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblCPUConsumption)
        Me.GroupBox1.Controls.Add(Me.tbrCPUConsumption)
        Me.GroupBox1.Controls.Add(Me.rbn_srt_none)
        Me.GroupBox1.Controls.Add(Me.cbx_RecsReverse)
        Me.GroupBox1.Controls.Add(Me.rbn_srt_dst)
        Me.GroupBox1.Controls.Add(Me.rbn_srt_pop)
        Me.GroupBox1.Controls.Add(Me.rbn_srt_Len)
        Me.GroupBox1.Location = New System.Drawing.Point(824, 15)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(247, 277)
        Me.GroupBox1.TabIndex = 26
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Sorting Method"
        '
        'lblCPUConsumption
        '
        Me.lblCPUConsumption.AutoSize = True
        Me.lblCPUConsumption.Location = New System.Drawing.Point(20, 253)
        Me.lblCPUConsumption.MaximumSize = New System.Drawing.Size(210, 17)
        Me.lblCPUConsumption.MinimumSize = New System.Drawing.Size(210, 17)
        Me.lblCPUConsumption.Name = "lblCPUConsumption"
        Me.lblCPUConsumption.Size = New System.Drawing.Size(210, 17)
        Me.lblCPUConsumption.TabIndex = 37
        Me.lblCPUConsumption.Text = "Target CPU Consumption: 15%"
        Me.lblCPUConsumption.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'tbrCPUConsumption
        '
        Me.tbrCPUConsumption.Location = New System.Drawing.Point(23, 214)
        Me.tbrCPUConsumption.Minimum = 1
        Me.tbrCPUConsumption.Name = "tbrCPUConsumption"
        Me.tbrCPUConsumption.Size = New System.Drawing.Size(207, 56)
        Me.tbrCPUConsumption.SmallChange = 5
        Me.tbrCPUConsumption.TabIndex = 36
        Me.tbrCPUConsumption.Value = 3
        '
        'rbn_srt_none
        '
        Me.rbn_srt_none.AutoSize = True
        Me.rbn_srt_none.Location = New System.Drawing.Point(8, 108)
        Me.rbn_srt_none.Margin = New System.Windows.Forms.Padding(4)
        Me.rbn_srt_none.Name = "rbn_srt_none"
        Me.rbn_srt_none.Size = New System.Drawing.Size(63, 21)
        Me.rbn_srt_none.TabIndex = 5
        Me.rbn_srt_none.Text = "None"
        Me.rbn_srt_none.UseVisualStyleBackColor = True
        '
        'cbx_RecsReverse
        '
        Me.cbx_RecsReverse.AutoSize = True
        Me.cbx_RecsReverse.Location = New System.Drawing.Point(8, 137)
        Me.cbx_RecsReverse.Margin = New System.Windows.Forms.Padding(4)
        Me.cbx_RecsReverse.Name = "cbx_RecsReverse"
        Me.cbx_RecsReverse.Size = New System.Drawing.Size(121, 21)
        Me.cbx_RecsReverse.TabIndex = 4
        Me.cbx_RecsReverse.Text = "Reverse order"
        Me.cbx_RecsReverse.UseVisualStyleBackColor = True
        '
        'rbn_srt_dst
        '
        Me.rbn_srt_dst.AutoSize = True
        Me.rbn_srt_dst.Location = New System.Drawing.Point(8, 81)
        Me.rbn_srt_dst.Margin = New System.Windows.Forms.Padding(4)
        Me.rbn_srt_dst.Name = "rbn_srt_dst"
        Me.rbn_srt_dst.Size = New System.Drawing.Size(122, 21)
        Me.rbn_srt_dst.TabIndex = 3
        Me.rbn_srt_dst.Text = "Word Distance"
        Me.rbn_srt_dst.UseVisualStyleBackColor = True
        '
        'rbn_srt_pop
        '
        Me.rbn_srt_pop.AutoSize = True
        Me.rbn_srt_pop.Checked = True
        Me.rbn_srt_pop.Location = New System.Drawing.Point(8, 52)
        Me.rbn_srt_pop.Margin = New System.Windows.Forms.Padding(4)
        Me.rbn_srt_pop.Name = "rbn_srt_pop"
        Me.rbn_srt_pop.Size = New System.Drawing.Size(92, 21)
        Me.rbn_srt_pop.TabIndex = 2
        Me.rbn_srt_pop.TabStop = True
        Me.rbn_srt_pop.Text = "Popularity"
        Me.rbn_srt_pop.UseVisualStyleBackColor = True
        '
        'rbn_srt_Len
        '
        Me.rbn_srt_Len.AutoSize = True
        Me.rbn_srt_Len.Location = New System.Drawing.Point(8, 23)
        Me.rbn_srt_Len.Margin = New System.Windows.Forms.Padding(4)
        Me.rbn_srt_Len.Name = "rbn_srt_Len"
        Me.rbn_srt_Len.Size = New System.Drawing.Size(111, 21)
        Me.rbn_srt_Len.TabIndex = 1
        Me.rbn_srt_Len.Text = "Word Length"
        Me.rbn_srt_Len.UseVisualStyleBackColor = True
        '
        'btnHome
        '
        Me.btnHome.Location = New System.Drawing.Point(457, 382)
        Me.btnHome.Margin = New System.Windows.Forms.Padding(4)
        Me.btnHome.Name = "btnHome"
        Me.btnHome.Size = New System.Drawing.Size(100, 28)
        Me.btnHome.TabIndex = 29
        Me.btnHome.Text = "Done"
        Me.btnHome.UseVisualStyleBackColor = True
        '
        'txt_hints
        '
        Me.txt_hints.Location = New System.Drawing.Point(0, 417)
        Me.txt_hints.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_hints.Name = "txt_hints"
        Me.txt_hints.ReadOnly = True
        Me.txt_hints.Size = New System.Drawing.Size(1101, 22)
        Me.txt_hints.TabIndex = 30
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btn_rmp_ArrowDown)
        Me.GroupBox3.Controls.Add(Me.btn_rmp_ArrowUp)
        Me.GroupBox3.Controls.Add(Me.btn_rmp_Accept)
        Me.GroupBox3.Controls.Add(Me.btn_rmp_HideList)
        Me.GroupBox3.Location = New System.Drawing.Point(580, 15)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(244, 177)
        Me.GroupBox3.TabIndex = 31
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Keyboard Actions"
        '
        'btn_rmp_ArrowDown
        '
        Me.btn_rmp_ArrowDown.Location = New System.Drawing.Point(8, 127)
        Me.btn_rmp_ArrowDown.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_rmp_ArrowDown.Name = "btn_rmp_ArrowDown"
        Me.btn_rmp_ArrowDown.Size = New System.Drawing.Size(221, 28)
        Me.btn_rmp_ArrowDown.TabIndex = 3
        Me.btn_rmp_ArrowDown.Text = "Move through word list (down)"
        Me.btn_rmp_ArrowDown.UseVisualStyleBackColor = True
        '
        'btn_rmp_ArrowUp
        '
        Me.btn_rmp_ArrowUp.Location = New System.Drawing.Point(9, 96)
        Me.btn_rmp_ArrowUp.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_rmp_ArrowUp.Name = "btn_rmp_ArrowUp"
        Me.btn_rmp_ArrowUp.Size = New System.Drawing.Size(220, 28)
        Me.btn_rmp_ArrowUp.TabIndex = 2
        Me.btn_rmp_ArrowUp.Text = "Move through word list (up)"
        Me.btn_rmp_ArrowUp.UseVisualStyleBackColor = True
        '
        'btn_rmp_Accept
        '
        Me.btn_rmp_Accept.Location = New System.Drawing.Point(8, 23)
        Me.btn_rmp_Accept.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_rmp_Accept.Name = "btn_rmp_Accept"
        Me.btn_rmp_Accept.Size = New System.Drawing.Size(221, 28)
        Me.btn_rmp_Accept.TabIndex = 1
        Me.btn_rmp_Accept.Text = "Type selected word"
        Me.btn_rmp_Accept.UseVisualStyleBackColor = True
        '
        'btn_rmp_HideList
        '
        Me.btn_rmp_HideList.Location = New System.Drawing.Point(8, 59)
        Me.btn_rmp_HideList.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_rmp_HideList.Name = "btn_rmp_HideList"
        Me.btn_rmp_HideList.Size = New System.Drawing.Size(221, 28)
        Me.btn_rmp_HideList.TabIndex = 0
        Me.btn_rmp_HideList.Text = "Hide words list"
        Me.btn_rmp_HideList.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtTrieUpdateInterval)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.txtIdeaCountLimit)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.txtRefTrieDepth)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.txtMinAcc)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.txtMinCnt)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.txtAutoPrc)
        Me.GroupBox4.Controls.Add(Me.txtMinLength)
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Location = New System.Drawing.Point(16, 15)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Size = New System.Drawing.Size(556, 177)
        Me.GroupBox4.TabIndex = 32
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "General Settings"
        '
        'txtTrieUpdateInterval
        '
        Me.txtTrieUpdateInterval.Location = New System.Drawing.Point(13, 146)
        Me.txtTrieUpdateInterval.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTrieUpdateInterval.Name = "txtTrieUpdateInterval"
        Me.txtTrieUpdateInterval.Size = New System.Drawing.Size(132, 22)
        Me.txtTrieUpdateInterval.TabIndex = 19
        Me.txtTrieUpdateInterval.Text = "5"
        Me.txtTrieUpdateInterval.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 127)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(133, 17)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Trie Update Interval"
        Me.Label7.Visible = False
        '
        'txtIdeaCountLimit
        '
        Me.txtIdeaCountLimit.Location = New System.Drawing.Point(12, 39)
        Me.txtIdeaCountLimit.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIdeaCountLimit.Name = "txtIdeaCountLimit"
        Me.txtIdeaCountLimit.Size = New System.Drawing.Size(132, 22)
        Me.txtIdeaCountLimit.TabIndex = 17
        Me.txtIdeaCountLimit.Text = "5"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 20)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(109, 17)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Idea Count Limit"
        '
        'txtRefTrieDepth
        '
        Me.txtRefTrieDepth.Location = New System.Drawing.Point(367, 39)
        Me.txtRefTrieDepth.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRefTrieDepth.Name = "txtRefTrieDepth"
        Me.txtRefTrieDepth.Size = New System.Drawing.Size(132, 22)
        Me.txtRefTrieDepth.TabIndex = 15
        Me.txtRefTrieDepth.Text = "4"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(363, 20)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(145, 17)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Reference Trie Depth"
        '
        'txtMinAcc
        '
        Me.txtMinAcc.Location = New System.Drawing.Point(184, 39)
        Me.txtMinAcc.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMinAcc.Name = "txtMinAcc"
        Me.txtMinAcc.Size = New System.Drawing.Size(132, 22)
        Me.txtMinAcc.TabIndex = 13
        Me.txtMinAcc.Text = "0.1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(363, 73)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(141, 17)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Autotype Percentage"
        Me.Label4.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(180, 20)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 17)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Minimum Accuracy"
        '
        'txtMinCnt
        '
        Me.txtMinCnt.Location = New System.Drawing.Point(184, 92)
        Me.txtMinCnt.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMinCnt.Name = "txtMinCnt"
        Me.txtMinCnt.Size = New System.Drawing.Size(132, 22)
        Me.txtMinCnt.TabIndex = 6
        Me.txtMinCnt.Text = "1"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(183, 73)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(134, 17)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Minimum Population"
        '
        'txtAutoPrc
        '
        Me.txtAutoPrc.Location = New System.Drawing.Point(367, 92)
        Me.txtAutoPrc.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAutoPrc.Name = "txtAutoPrc"
        Me.txtAutoPrc.Size = New System.Drawing.Size(132, 22)
        Me.txtAutoPrc.TabIndex = 11
        Me.txtAutoPrc.Text = "0.3"
        Me.txtAutoPrc.Visible = False
        '
        'txtMinLength
        '
        Me.txtMinLength.Location = New System.Drawing.Point(12, 92)
        Me.txtMinLength.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMinLength.Name = "txtMinLength"
        Me.txtMinLength.Size = New System.Drawing.Size(132, 22)
        Me.txtMinLength.TabIndex = 4
        Me.txtMinLength.Text = "4"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 73)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 17)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Length Minimum"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.cbx_SM_storedRefs)
        Me.GroupBox5.Controls.Add(Me.btn_SM_resetStored)
        Me.GroupBox5.Controls.Add(Me.cbx_SM_useStored)
        Me.GroupBox5.Controls.Add(Me.cbx_SM_storeSettings)
        Me.GroupBox5.Location = New System.Drawing.Point(421, 191)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox5.Size = New System.Drawing.Size(236, 191)
        Me.GroupBox5.TabIndex = 33
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Setting Memory Options"
        '
        'cbx_SM_storedRefs
        '
        Me.cbx_SM_storedRefs.AutoSize = True
        Me.cbx_SM_storedRefs.Location = New System.Drawing.Point(9, 80)
        Me.cbx_SM_storedRefs.Margin = New System.Windows.Forms.Padding(4)
        Me.cbx_SM_storedRefs.Name = "cbx_SM_storedRefs"
        Me.cbx_SM_storedRefs.Size = New System.Drawing.Size(171, 21)
        Me.cbx_SM_storedRefs.TabIndex = 3
        Me.cbx_SM_storedRefs.Text = "Use stored references"
        Me.cbx_SM_storedRefs.UseVisualStyleBackColor = True
        '
        'btn_SM_resetStored
        '
        Me.btn_SM_resetStored.Location = New System.Drawing.Point(8, 108)
        Me.btn_SM_resetStored.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_SM_resetStored.Name = "btn_SM_resetStored"
        Me.btn_SM_resetStored.Size = New System.Drawing.Size(155, 28)
        Me.btn_SM_resetStored.TabIndex = 2
        Me.btn_SM_resetStored.Text = "Reset stored settings"
        Me.btn_SM_resetStored.UseVisualStyleBackColor = True
        '
        'cbx_SM_useStored
        '
        Me.cbx_SM_useStored.AutoSize = True
        Me.cbx_SM_useStored.Location = New System.Drawing.Point(9, 52)
        Me.cbx_SM_useStored.Margin = New System.Windows.Forms.Padding(4)
        Me.cbx_SM_useStored.Name = "cbx_SM_useStored"
        Me.cbx_SM_useStored.Size = New System.Drawing.Size(152, 21)
        Me.cbx_SM_useStored.TabIndex = 1
        Me.cbx_SM_useStored.Text = "Use stored settings"
        Me.cbx_SM_useStored.UseVisualStyleBackColor = True
        '
        'cbx_SM_storeSettings
        '
        Me.cbx_SM_storeSettings.AutoSize = True
        Me.cbx_SM_storeSettings.Location = New System.Drawing.Point(9, 25)
        Me.cbx_SM_storeSettings.Margin = New System.Windows.Forms.Padding(4)
        Me.cbx_SM_storeSettings.Name = "cbx_SM_storeSettings"
        Me.cbx_SM_storeSettings.Size = New System.Drawing.Size(117, 21)
        Me.cbx_SM_storeSettings.TabIndex = 0
        Me.cbx_SM_storeSettings.Text = "Store settings"
        Me.cbx_SM_storeSettings.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.rbn_Upd8_None)
        Me.GroupBox6.Controls.Add(Me.rbn_Upd8_Ask)
        Me.GroupBox6.Controls.Add(Me.rbn_Upd8_Auto)
        Me.GroupBox6.Location = New System.Drawing.Point(665, 191)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox6.Size = New System.Drawing.Size(159, 101)
        Me.GroupBox6.TabIndex = 34
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Updating Options"
        '
        'rbn_Upd8_None
        '
        Me.rbn_Upd8_None.AutoSize = True
        Me.rbn_Upd8_None.Checked = True
        Me.rbn_Upd8_None.Location = New System.Drawing.Point(8, 80)
        Me.rbn_Upd8_None.Margin = New System.Windows.Forms.Padding(4)
        Me.rbn_Upd8_None.Name = "rbn_Upd8_None"
        Me.rbn_Upd8_None.Size = New System.Drawing.Size(110, 21)
        Me.rbn_Upd8_None.TabIndex = 2
        Me.rbn_Upd8_None.TabStop = True
        Me.rbn_Upd8_None.Text = "Don't update"
        Me.rbn_Upd8_None.UseVisualStyleBackColor = True
        '
        'rbn_Upd8_Ask
        '
        Me.rbn_Upd8_Ask.AutoSize = True
        Me.rbn_Upd8_Ask.Location = New System.Drawing.Point(8, 52)
        Me.rbn_Upd8_Ask.Margin = New System.Windows.Forms.Padding(4)
        Me.rbn_Upd8_Ask.Name = "rbn_Upd8_Ask"
        Me.rbn_Upd8_Ask.Size = New System.Drawing.Size(79, 21)
        Me.rbn_Upd8_Ask.TabIndex = 1
        Me.rbn_Upd8_Ask.Text = "Ask first"
        Me.rbn_Upd8_Ask.UseVisualStyleBackColor = True
        '
        'rbn_Upd8_Auto
        '
        Me.rbn_Upd8_Auto.AutoSize = True
        Me.rbn_Upd8_Auto.Location = New System.Drawing.Point(8, 23)
        Me.rbn_Upd8_Auto.Margin = New System.Windows.Forms.Padding(4)
        Me.rbn_Upd8_Auto.Name = "rbn_Upd8_Auto"
        Me.rbn_Upd8_Auto.Size = New System.Drawing.Size(91, 21)
        Me.rbn_Upd8_Auto.TabIndex = 0
        Me.rbn_Upd8_Auto.Text = "Automatic"
        Me.rbn_Upd8_Auto.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.rbnMDSCumulative)
        Me.GroupBox7.Controls.Add(Me.rbnMDSNormal)
        Me.GroupBox7.Controls.Add(Me.rbnMDSTries)
        Me.GroupBox7.Location = New System.Drawing.Point(665, 299)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(161, 111)
        Me.GroupBox7.TabIndex = 35
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Main Doc Options"
        '
        'rbnMDSCumulative
        '
        Me.rbnMDSCumulative.AutoSize = True
        Me.rbnMDSCumulative.Location = New System.Drawing.Point(8, 79)
        Me.rbnMDSCumulative.Margin = New System.Windows.Forms.Padding(4)
        Me.rbnMDSCumulative.Name = "rbnMDSCumulative"
        Me.rbnMDSCumulative.Size = New System.Drawing.Size(137, 21)
        Me.rbnMDSCumulative.TabIndex = 38
        Me.rbnMDSCumulative.Text = "Cumulative (WIP)"
        Me.rbnMDSCumulative.UseVisualStyleBackColor = True
        Me.rbnMDSCumulative.Visible = False
        '
        'rbnMDSNormal
        '
        Me.rbnMDSNormal.AutoSize = True
        Me.rbnMDSNormal.Checked = True
        Me.rbnMDSNormal.Location = New System.Drawing.Point(8, 22)
        Me.rbnMDSNormal.Margin = New System.Windows.Forms.Padding(4)
        Me.rbnMDSNormal.Name = "rbnMDSNormal"
        Me.rbnMDSNormal.Size = New System.Drawing.Size(74, 21)
        Me.rbnMDSNormal.TabIndex = 36
        Me.rbnMDSNormal.TabStop = True
        Me.rbnMDSNormal.Text = "Normal"
        Me.rbnMDSNormal.UseVisualStyleBackColor = True
        '
        'rbnMDSTries
        '
        Me.rbnMDSTries.AutoSize = True
        Me.rbnMDSTries.Location = New System.Drawing.Point(8, 51)
        Me.rbnMDSTries.Margin = New System.Windows.Forms.Padding(4)
        Me.rbnMDSTries.Name = "rbnMDSTries"
        Me.rbnMDSTries.Size = New System.Drawing.Size(105, 21)
        Me.rbnMDSTries.TabIndex = 37
        Me.rbnMDSTries.Text = "Trie Method"
        Me.rbnMDSTries.UseVisualStyleBackColor = True
        '
        'FormOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1087, 441)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.txt_hints)
        Me.Controls.Add(Me.btnHome)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FormOptions"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "FormOptions"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.tbrCPUConsumption, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cbxSpace As System.Windows.Forms.CheckBox
    Friend WithEvents cbxEntireWord As System.Windows.Forms.CheckBox
    Friend WithEvents cbxAuto As System.Windows.Forms.CheckBox
    Friend WithEvents cbxMoveBox As System.Windows.Forms.CheckBox
    Friend WithEvents cbxToLower As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cbx_RecsReverse As System.Windows.Forms.CheckBox
    Friend WithEvents rbn_srt_dst As System.Windows.Forms.RadioButton
    Friend WithEvents rbn_srt_pop As System.Windows.Forms.RadioButton
    Friend WithEvents rbn_srt_Len As System.Windows.Forms.RadioButton
    Friend WithEvents btnHome As System.Windows.Forms.Button
    Friend WithEvents txt_hints As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btn_rmp_Accept As System.Windows.Forms.Button
    Friend WithEvents btn_rmp_HideList As System.Windows.Forms.Button
    Friend WithEvents btn_rmp_ArrowDown As System.Windows.Forms.Button
    Friend WithEvents btn_rmp_ArrowUp As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtMinAcc As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMinCnt As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtAutoPrc As System.Windows.Forms.TextBox
    Friend WithEvents txtMinLength As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btn_SM_resetStored As System.Windows.Forms.Button
    Friend WithEvents cbx_SM_useStored As System.Windows.Forms.CheckBox
    Friend WithEvents cbx_SM_storeSettings As System.Windows.Forms.CheckBox
    Friend WithEvents cbx_SM_storedRefs As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents rbn_Upd8_None As System.Windows.Forms.RadioButton
    Friend WithEvents rbn_Upd8_Ask As System.Windows.Forms.RadioButton
    Friend WithEvents rbn_Upd8_Auto As System.Windows.Forms.RadioButton
    Friend WithEvents txtRefTrieDepth As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtIdeaCountLimit As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents rbn_srt_none As System.Windows.Forms.RadioButton
    Friend WithEvents cbxNumpadSelection As System.Windows.Forms.CheckBox
    Friend WithEvents cbxNumSelection_UseNumpad As System.Windows.Forms.CheckBox
    Friend WithEvents txtTrieUpdateInterval As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents rbnMDSCumulative As System.Windows.Forms.RadioButton
    Friend WithEvents rbnMDSNormal As System.Windows.Forms.RadioButton
    Friend WithEvents rbnMDSTries As System.Windows.Forms.RadioButton
    Friend WithEvents cbxCopyPaste As System.Windows.Forms.CheckBox
    Friend WithEvents lblCPUConsumption As System.Windows.Forms.Label
    Friend WithEvents tbrCPUConsumption As System.Windows.Forms.TrackBar
End Class

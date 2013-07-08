Public Class FormOptions

    Public SettingsChanged As Boolean = False
    Private Sub FormOptions_Load() Handles MyBase.Load

        Me.Text = Form1.Text & " - Options"
        Me.Location = Form1.MasterLocation

    End Sub

    Private Sub UpdateLocation() Handles Me.Move
        Form1.MasterLocation = Me.Location
    End Sub

    ' Show-me button
    Public WithEvents ShowMeBtn As MenuItem
    Public Sub ShowMe() Handles ShowMeBtn.Click

        ' Hide other stuff
        Form1.Hide()
        FormFAQ.Hide()
        Form1.Sleep(100)

        ' Show me
        Me.Show()

    End Sub

    ' Minimization hook (hides the form on minimization so that the context menu works properly)
    Private Sub HideMe() Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.WindowState = FormWindowState.Normal
            Me.Hide()
            If (Not Form1.HasBeenMinimizedBefore) Then
                Form1.ShowMinimizationHint()
                Form1.HasBeenMinimizedBefore = True
            End If
        End If
    End Sub

#Region "Hints"
    Private Sub H_Main() Handles Me.MouseEnter
        txt_hints.Text = "Mouse over something to learn more about it."
    End Sub

    ' Groupboxes
    Private Sub H_gbx1() Handles GroupBox1.MouseEnter
        txt_hints.Text = "Mouse over something to learn more about it."
    End Sub
    Private Sub H_gbx2() Handles GroupBox2.MouseEnter
        txt_hints.Text = "Mouse over something to learn more about it."
    End Sub
    Private Sub H_gbx3() Handles GroupBox3.MouseEnter
        txt_hints.Text = "Mouse over something to learn more about it."
    End Sub
    Private Sub H_gbx4() Handles GroupBox4.MouseEnter
        txt_hints.Text = "Mouse over something to learn more about it."
    End Sub
    Private Sub H_gbx5() Handles GroupBox5.MouseEnter
        txt_hints.Text = "Mouse over something to learn more about it."
    End Sub
    Private Sub H_gbx6() Handles GroupBox6.MouseEnter
        txt_hints.Text = "Mouse over something to learn more about it."
    End Sub

    ' Textboxes
    Private Sub H_MinLength() Handles txtMinLength.MouseEnter
        txt_hints.Text = "Specifies the minimum number of letters a word must have for it to be suggested."
    End Sub
    Private Sub H_MinCnt() Handles txtMinCnt.MouseEnter
        txt_hints.Text = "Specifies the minimum number of times a word must appear in either the main or reference documents for it to be suggested."
    End Sub
    Private Sub H_AutoPrc() Handles txtAutoPrc.MouseEnter
        txt_hints.Text = "Specifies how many letters (number or percentage) a partial word must share with a suggested one before the program autotypes it. Percentages are less than 1 (ex 0.55)"
    End Sub
    Private Sub H_MinAcc() Handles txtMinAcc.MouseEnter
        txt_hints.Text = "Specifies how relevant the suggestions provided must be before they will be shown in a drop-down list."
    End Sub
    Private Sub H_TrieDepth() Handles txtRefTrieDepth.MouseEnter
        txt_hints.Text = "Specifies how many layers the trie will use. More layers need more memory, but improve sorting speed for large numbers of words. This system is only for reference documents."
    End Sub
    Private Sub H_IdeaCountLimit() Handles txtIdeaCountLimit.MouseEnter
        txt_hints.Text = "Specifies the maximum number of ideas that can be suggested at a time. Setting this number to 0 shows all suggestions."
    End Sub

    ' Boolean options
    Private Sub H_ShowWord() Handles cbxEntireWord.MouseEnter
        txt_hints.Text = "If checked, the entire suggestion word is shown. Otherwise, only the letters you haven't typed yet are shown."
    End Sub
    Private Sub H_AutoType() Handles cbxAuto.MouseEnter
        txt_hints.Text = "If checked, the program will automatically type what it thinks you are typing without asking."
    End Sub
    Private Sub H_MoveHopper() Handles cbxMoveBox.MouseEnter
        txt_hints.Text = "If checked, the suggestions window will follow the cursor. Otherwise, it will stay in one place."
    End Sub
    Private Sub H_CapitalSense() Handles cbxToLower.MouseEnter
        txt_hints.Text = "If checked, word case (capitalization) will be ignored. Otherwise, it will not be."
    End Sub
    Private Sub H_SpaceAppend() Handles cbxSpace.MouseEnter
        txt_hints.Text = "If checked, a space will be added after each autocompleted word."
    End Sub '
    Private Sub H_RootCnt() Handles cbx_SM_storedRefs.MouseEnter
        txt_hints.Text = "If checked, ContexType will save references between uses."
    End Sub
    Private Sub H_CopyPasteMethod() Handles cbxCopyPaste.MouseEnter
        txt_hints.Text = "If checked, selected suggestions will be copy-pasted into the document in one block. If not, they will be auto-typed letter by letter."
    End Sub

    ' Hide on startup
    Private Sub H_HideOnStartUp() Handles cbxHideOnStart.MouseEnter
        txt_hints.Text = "If checked, the main ContexType window will be minimized to the System Tray on startup. If not, it will be displayed normally."
    End Sub

    ' Numpad stuff
    Private Sub H_NumSelect() Handles cbxNumpadSelection.MouseEnter
        txt_hints.Text = "If checked, a number will appear next to some ideas allowing you to select a specific idea."
    End Sub
    Private Sub H_NumpadSelectEnabled() Handles cbxNumSelection_UseNumpad.MouseEnter
        txt_hints.Text = "If checked, numerical suggestion selection will use the numpad. Otherwise, the horizontal number line (at the top of most keyboards) will be used."
    End Sub

    ' Sorting method
    Private Sub H_LengthSort() Handles rbn_srt_Len.MouseEnter
        txt_hints.Text = "Sort words by length."
    End Sub
    Private Sub H_NumSort() Handles rbn_srt_pop.MouseEnter
        txt_hints.Text = "Sort words by the number of them present in main and reference document(s)."
    End Sub
    Private Sub H_DistSort() Handles rbn_srt_dst.MouseEnter
        txt_hints.Text = "Sort words by closest number of words between current and suggested words in main document."
    End Sub
    Private Sub H_NoSort(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbn_srt_none.MouseEnter
        txt_hints.Text = "Don't sort words at all"
    End Sub
    Private Sub H_ReverseRecs() Handles cbx_RecsReverse.MouseEnter
        txt_hints.Text = "If checked, the order of words in the word lists will be reversed."
    End Sub
    Private Sub H_CPUConsumption(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbrCPUConsumption.MouseEnter, lblCPUConsumption.MouseEnter
        txt_hints.Text = "Move the trackbar to control how much CPU power is used. A higher percentage speeds up searching, but can slow down other programs and use more electricity."
    End Sub

    ' Main document search method
    Private Sub H_MD_UseTries(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txt_hints.Text = "If checked, active document suggestions are found using tries. The word bank doesn't update instantly, but the recommendation search is fast for large documents."
    End Sub
    Private Sub H_MD_Normal(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txt_hints.Text = "If checked, active document suggestions are found using a standard method. The word bank updates instantly, but the recommendation search is slow for large documents."
    End Sub
    Private Sub H_TrieInterval(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTrieUpdateInterval.MouseEnter
        txt_hints.Text = "The amount of time (in seconds) between trie/word bank updates."
    End Sub

    ' Auto-update
    Private Sub H_UpdateAuto(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbn_Upd8_Auto.MouseEnter
        txt_hints.Text = "If checked, ContexType will try to automatically check and install updates every time it is ran."
    End Sub
    Private Sub H_UpdateAsk(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbn_Upd8_Ask.MouseEnter
        txt_hints.Text = "If checked, ContexType will try to automatically check for updates every time it is ran. If an update is found, ContexType will ask you for permission to install it."
    End Sub
    Private Sub H_UpdateNone(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbn_Upd8_None.MouseEnter
        txt_hints.Text = "If checked, no automatic updating will be performed."
    End Sub

    ' Remappers
    Private Sub H_RemapAccept(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_rmp_Accept.MouseEnter
        txt_hints.Text = "Click this button to change the word auto-completion key. (Default: Tab key)"
    End Sub
    Private Sub H_RemapHideList(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_rmp_HideList.MouseEnter
        txt_hints.Text = "Click this button to change the list hiding key. (Default: Esc key)"
    End Sub
    Private Sub H_RemapArrowUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_rmp_ArrowUp.MouseEnter
        txt_hints.Text = "Click this button to change the previous list item key. (Default: Up Arrow key)"
    End Sub
    Private Sub H_RemapArrowDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_rmp_ArrowDown.MouseEnter
        txt_hints.Text = "Click this button to change the next list item key. (Default: Down Arrow key)"
    End Sub

    ' Settings memory
    Private Sub H_SM_Reset(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_SM_resetStored.MouseEnter
        txt_hints.Text = "Click this button to reset the stored settings to their defaults."
    End Sub
    Private Sub H_SM_UseStored(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbx_SM_useStored.MouseEnter
        txt_hints.Text = "If checked, ContexType will try to use its previous settings. Otherwise, it will use the default settings."
    End Sub
    Private Sub H_SM_StoreSettings(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbx_SM_storeSettings.MouseEnter
        txt_hints.Text = "If checked, ContexType will save the current settings and use them next time if possible."
    End Sub
#End Region

    Private Sub btnHome_Click() Handles btnHome.Click

        If SettingsChanged Then
            Dim Result As Integer = Settings.UpdateSettingsFile()
            If Result = 1 Then
                MsgBox("Settings successfully saved!")
            Else
                MsgBox("Settings were not saved.")
            End If
        End If

        Me.Hide()
        Form1.Show()

    End Sub

#Region "Settings Updates"
    Private Sub cbxEntireWord_CheckedChanged() Handles cbxEntireWord.CheckedChanged
        Form1.O_EntireWord = cbxEntireWord.Checked
        SettingsChanged = True
    End Sub

    Private Sub cbxAuto_CheckedChanged() Handles cbxAuto.CheckedChanged
        Form1.O_AutoType = cbxAuto.Checked
        txtAutoPrc.Visible = cbxAuto.Checked
        Label4.Visible = cbxAuto.Checked
        SettingsChanged = True
    End Sub

    Private Sub cbxMoveBox_CheckedChanged() Handles cbxMoveBox.CheckedChanged
        Form1.O_RecsFollowCursor = cbxMoveBox.Checked
        SettingsChanged = True
    End Sub

    Private Sub cbxToLower_CheckedChanged() Handles cbxToLower.CheckedChanged
        Form1.O_IgnoreCase = cbxToLower.Checked
        SettingsChanged = True
    End Sub

    Private Sub cbxSpace_CheckedChanged() Handles cbxSpace.CheckedChanged
        Form1.O_TypeSpace = cbxSpace.Checked
        SettingsChanged = True
    End Sub

    Private Sub cbx_RecsReverse_CheckedChanged() Handles cbx_RecsReverse.CheckedChanged
        Form1.O_Reverse = cbx_RecsReverse.Checked
        SettingsChanged = True
    End Sub
#End Region

    Private Sub OnClose(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        ' Close main form
        Form1.Close()

    End Sub

#Region "Settings Updates #2"
    ' Update settings
    Private Sub txtMinCnt_TextChanged() Handles txtMinCnt.TextChanged

        Try
            If CInt(txtMinCnt.Text) > 0 Then
                Form1.MinCnt = CInt(txtMinCnt.Text)
                txtMinCnt.BackColor = Color.White
                SettingsChanged = True
            Else
                txtMinCnt.BackColor = Color.Red
            End If
        Catch
            txtMinCnt.BackColor = Color.Red
        End Try

    End Sub

    Private Sub txtMinLength_TextChanged() Handles txtMinLength.TextChanged

        Try
            If CInt(txtMinLength.Text) > 1 Then
                Form1.MinLength = CInt(txtMinLength.Text)
                txtMinLength.BackColor = Color.White
                SettingsChanged = True
            Else
                txtMinLength.BackColor = Color.Red
            End If
        Catch
            txtMinLength.BackColor = Color.Red
        End Try

    End Sub

    Private Sub txtMinAcc_TextChanged() Handles txtMinAcc.TextChanged
        Try
            If CDbl(txtMinAcc.Text) < 0 Or CDbl(txtMinAcc.Text) >= 1 Then
                txtMinAcc.BackColor = Color.Red
                SettingsChanged = True
            Else
                Form1.MinAccuracy = CDbl(txtMinAcc.Text)
                txtMinAcc.BackColor = Color.White
            End If
        Catch
            txtMinAcc.BackColor = Color.Red
        End Try
    End Sub

    Private Sub txtAutoPrc_TextChanged() Handles txtAutoPrc.TextChanged
        Try
            If CDbl(txtAutoPrc.Text) > 0 Then
                Form1.AutoPercentage = CDbl(txtAutoPrc.Text)
                txtAutoPrc.BackColor = Color.White
                SettingsChanged = True
            Else
                txtAutoPrc.BackColor = Color.Red
            End If
        Catch
            txtAutoPrc.BackColor = Color.Red
        End Try
    End Sub

    ' Filtering methods
    Private Sub rbn_srt_pop_CheckedChanged() Handles rbn_srt_pop.CheckedChanged
        Form1.O_S_Popln = rbn_srt_pop.Checked
        SettingsChanged = True
    End Sub
    Private Sub rbn_srt_Len_CheckedChanged() Handles rbn_srt_Len.CheckedChanged
        Form1.O_S_Length = rbn_srt_Len.Checked
        SettingsChanged = True
    End Sub
    Private Sub rbn_srt_none_CheckedChanged() Handles rbn_srt_none.CheckedChanged
        Form1.O_S_None = rbn_srt_none.Checked
        SettingsChanged = True
    End Sub
    Private Sub rbn_srt_dst_CheckedChanged() Handles rbn_srt_dst.CheckedChanged
        Form1.O_S_Dist = rbn_srt_dst.Checked
        SettingsChanged = True
    End Sub

    ' --- Key remapping ---
    Private Sub AcceptRemap() Handles btn_rmp_Accept.Click
        Form1.Remap(Form1.VK_Accept)
        SettingsChanged = True
    End Sub

    Private Sub HideWordsRemap() Handles btn_rmp_HideList.Click
        Form1.Remap(Form1.VK_ClearList)
        SettingsChanged = True
    End Sub

    Private Sub ArrowUpRemap() Handles btn_rmp_ArrowUp.Click
        Form1.Remap(Form1.VK_ArrowUp)
        SettingsChanged = True
    End Sub
    Private Sub ArrowDownRemap() Handles btn_rmp_ArrowDown.Click
        Form1.Remap(Form1.VK_ArrowDown)
        SettingsChanged = True
    End Sub

    Private Sub btn_SM_resetStored_Click() Handles btn_SM_resetStored.Click

        ' Get settings file location
        Dim EPath As String = Process.GetCurrentProcess.MainModule.FileName
        EPath = EPath.Substring(0, Math.Max(EPath.LastIndexOf("\"), EPath.LastIndexOf("/"))) & "\" & Form1.SettingsFile

        ' Check for presence of settings
        If Dir(EPath) <> "" Then
            Try
                IO.File.Delete(EPath)
                MsgBox("Stored settings successfully reset!")
            Catch
                MsgBox("Failed to reset stored settings.")
            End Try
        Else
            MsgBox("No stored settings exist. (There was nothing to reset.)")
        End If

    End Sub

    Private Sub cbx_SM_useStored_CheckedChanged() Handles cbx_SM_useStored.CheckedChanged

        If cbx_SM_useStored.Checked Then

            ' Attempt to use stored settings
            Dim Result As Integer = Settings.QuerySettingsFile
            If Result = 1 And Form1.MainFormLoaded Then
                MsgBox("Stored settings successfully loaded!")
            ElseIf Result <> 1 Then
                MsgBox("Stored settings were not loaded successfully. Default settings will be used.")
            End If

        End If

    End Sub

    Private Sub cbx_SM_storedRefs_CheckedChanged() Handles cbx_SM_storedRefs.CheckedChanged
        Form1.SM_UseStoredRefs = cbx_SM_storedRefs.Checked
        If cbx_SM_storedRefs.Checked Then
            Dim Result As Integer = Settings.QueryReferences()
            If Result = 1 And Form1.MainFormLoaded Then
                MsgBox("Stored references successfully loaded!")
            ElseIf Result <> 1 Then
                MsgBox("Stored references were not loaded successfully.")
            End If
        End If
    End Sub

    Private Sub cbx_SM_storeSettings_CheckedChanged() Handles cbx_SM_storeSettings.CheckedChanged
        ' This sub is triggered while the program is loading (before FormOptions is visible). This if statement stops it from attempting to update the settings file while it is being read from.
        If Me.Visible Then
            Form1.SM_StoreSettings = cbx_SM_storeSettings.Checked
            Settings.UpdateSettingsFile()
            SettingsChanged = True
        End If
    End Sub

    Private Sub txtRefTrieDepth_TextChanged() Handles txtRefTrieDepth.TextChanged
        Try
            If CInt(txtRefTrieDepth.Text) > 0 Then
                Form1.TrieDepth = CDbl(txtRefTrieDepth.Text)
                txtRefTrieDepth.BackColor = Color.White
                SettingsChanged = True
            Else
                txtRefTrieDepth.BackColor = Color.Red
            End If
        Catch
            txtRefTrieDepth.BackColor = Color.Red
        End Try
    End Sub

    Private Sub txtIdeaCountLimit_TextChanged() Handles txtIdeaCountLimit.TextChanged
        Try
            If CInt(txtIdeaCountLimit.Text) >= 0 Then
                Form1.IdeaCountLimit = CDbl(txtIdeaCountLimit.Text)
                txtIdeaCountLimit.BackColor = Color.White
                SettingsChanged = True
            Else
                txtIdeaCountLimit.BackColor = Color.Red
            End If
        Catch
            txtIdeaCountLimit.BackColor = Color.Red
        End Try
    End Sub

    Private Sub rbnUpdate_Auto() Handles rbn_Upd8_Auto.CheckedChanged
        Form1.UpdateMode = 2
        SettingsChanged = True
    End Sub
    Private Sub rbnUpdate_Ask() Handles rbn_Upd8_Ask.CheckedChanged
        Form1.UpdateMode = 1
        SettingsChanged = True
    End Sub
    Private Sub rbnUpdate_None() Handles rbn_Upd8_None.CheckedChanged
        Form1.UpdateMode = 0
        SettingsChanged = True
    End Sub

#End Region

    Private Sub cbxNumpadSelection_CheckedChanged() Handles cbxNumpadSelection.CheckedChanged

        ' Standard settings update component
        Form1.O_NumSelection = cbxNumpadSelection.Checked
        SettingsChanged = True

        ' Numberpad checkbox
        cbxNumSelection_UseNumpad.Visible = cbxNumpadSelection.Checked

        If cbxNumpadSelection.Checked Then

            ' Show numbers list box
            Hopper.lbox_ideas.Size = New Size(Hopper.Width - Hopper.lbox_nums.Width, Hopper.lbox_ideas.Height)
            Hopper.lbox_nums.Location = Hopper.lbox_ideas.Location + New Point(Hopper.lbox_ideas.Width, 0)

        Else

            ' Hide numbers list box
            Hopper.lbox_ideas.Size = New Size(Hopper.Width, Hopper.lbox_ideas.Height)
            Hopper.lbox_nums.Location = New Point(-100, -100)

        End If
    End Sub

    Private Sub cbxNumSelection_UseNumpad_CheckedChanged() Handles cbxNumSelection_UseNumpad.CheckedChanged

        ' Update settings
        SettingsChanged = True
        Form1.O_UseNumberpad = cbxNumSelection_UseNumpad.Checked

        ' Reregister hotkeys accordingly
        Form1.NumpadHotkeysOff()
        Form1.NumpadHotkeysOn()

    End Sub

    Private Sub txtTrieUpdateInterval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTrieUpdateInterval.TextChanged
        Try
            If CInt(txtTrieUpdateInterval.Text) > 0 Then
                Form1.O_MDSTrieSrcInterval = CInt(txtTrieUpdateInterval.Text)
                txtTrieUpdateInterval.BackColor = Color.White
                SettingsChanged = True
            Else
                txtTrieUpdateInterval.BackColor = Color.Red
            End If
        Catch
            txtTrieUpdateInterval.BackColor = Color.Red
        End Try
    End Sub

    Private Sub cbxCopyPaste_CheckedChanged() Handles cbxCopyPaste.CheckedChanged
        SettingsChanged = True
        Form1.O_UseCopyPaste = cbxCopyPaste.Checked
    End Sub

    Private Sub tbrCPUConsumption_Scroll() Handles tbrCPUConsumption.Scroll
        SettingsChanged = True
        Form1.TargetCPUUse = CInt(tbrCPUConsumption.Value * 5)
        lblCPUConsumption.Text = "Target CPU Consumption: " & Form1.TargetCPUUse & "%"
    End Sub

    Private Sub cbxCopyPaste_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxCopyPaste.CheckedChanged

    End Sub

    Private Sub cbxHideOnStart_CheckedChanged() Handles cbxHideOnStart.CheckedChanged
        SettingsChanged = True
        Form1.O_HideOnStart = cbxHideOnStart.Checked
    End Sub

End Class
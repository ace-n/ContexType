' Settings handling
Public Class Settings

    ' Boolean to integer converter
    Shared Function BoolToInt(ByVal Bool As Boolean) As Integer
        If Bool Then
            Return 1
        End If

        Return 0

    End Function

    ' Integer to boolean converter
    Shared Function IntToBool(ByVal Int As Integer) As Integer
        Return (Int = 1)
    End Function

    ' Update FormOptions regarding the settings
    Shared Sub UpdateOptionsForm()

        ' Number settings
        FormOptions.txtMinCnt.Text = CStr(Form1.MinCnt)
        FormOptions.txtMinLength.Text = CStr(Form1.MinLength)
        FormOptions.txtAutoPrc.Text = CStr(Form1.AutoPercentage)
        FormOptions.txtMinAcc.Text = CStr(Form1.MinAccuracy)
        FormOptions.txtRefTrieDepth.Text = CStr(Form1.TrieDepth)
        FormOptions.tbrCPUConsumption.Value = Form1.TargetCPUUse / 5
        FormOptions.lblCPUConsumption.Text = "Target CPU Consumption: " & Form1.TargetCPUUse & "%"
        FormOptions.txtIdeaCountLimit.Text = CStr(Form1.IdeaCountLimit)

        ' Booleans
        FormOptions.cbxToLower.Checked = Form1.O_IgnoreCase
        FormOptions.cbxSpace.Checked = Form1.O_TypeSpace
        FormOptions.cbxAuto.Checked = Form1.O_AutoType
        FormOptions.cbxMoveBox.Checked = Form1.O_RecsFollowCursor
        FormOptions.cbxEntireWord.Checked = Form1.O_EntireWord
        FormOptions.cbxNumpadSelection.Checked = Form1.O_NumSelection
        FormOptions.cbxNumSelection_UseNumpad.Checked = Form1.O_UseNumberpad
        FormOptions.cbxCopyPaste.Checked = Form1.O_UseCopyPaste

        ' Sorting method booleans
        FormOptions.rbn_srt_Len.Checked = Form1.O_S_Length
        FormOptions.rbn_srt_pop.Checked = Form1.O_S_Popln
        FormOptions.rbn_srt_dst.Checked = Form1.O_S_Dist
        FormOptions.rbn_srt_none.Checked = Form1.O_S_None
        FormOptions.cbx_RecsReverse.Checked = Form1.O_Reverse

        ' Settings memory is ignored here on purpose

    End Sub

    ' Reset available settings to default
    Shared Sub DefaultSettings()

        ' Global settings variables
        Form1.MinCnt = 1
        Form1.MinLength = 4
        Form1.AutoPercentage = 0.2
        Form1.MinAccuracy = 0
        Form1.IdeaCountLimit = 0

        ' Boolean transmits
        Form1.O_Reverse = False
        Form1.O_IgnoreCase = True
        Form1.O_TypeSpace = False
        Form1.O_AutoType = False
        Form1.O_EntireWord = False
        Form1.O_RecsFollowCursor = True
        Form1.O_NumSelection = False
        Form1.O_UseNumberpad = True
        Form1.O_UseCopyPaste = False

        ' Sorting method
        Form1.TrieDepth = 4
        Form1.TargetCPUUse = 15
        Form1.O_S_Length = False
        Form1.O_S_Popln = False
        Form1.O_S_Dist = True ' Starting setting
        Form1.O_S_None = False
        Form1.O_MDSTrieSrcInterval = 30

        ' Customizable key constants [5]
        Form1.VK_Accept = &H9               ' TAB key
        Form1.VK_Switch = &H12              ' ALT key
        Form1.VK_ClearList = Keys.Escape    ' ESC key
        Form1.VK_ArrowUp = &H26
        Form1.VK_ArrowDown = &H28
        Form1.VK_ArrowLeft = &H25
        Form1.VK_ArrowRight = &H27

        ' Settings memory
        Form1.SM_StoreSettings = False
        Form1.SM_UseStoredStgs = True
        Form1.SM_UseStoredRefs = True
        Form1.UpdateMode = 1

    End Sub

    ' Query settings
    '   1=success, 0=no settings file found
    Shared Function QuerySettingsFile() As Integer

        ' Get settings file location
        Dim EPath As String = Process.GetCurrentProcess.MainModule.FileName
        EPath = EPath.Substring(0, Math.Max(EPath.LastIndexOf("\"), EPath.LastIndexOf("/"))) & "\" & Form1.SettingsFile

        ' If the settings file can't be found, silently exit this part (display notifications using calling code if appropriate)
        If Dir(EPath) = "" Then
            Return 0
        End If

        ' Get file text
        Try

            Dim Reader As New IO.StreamReader(EPath)

            ' Read each line
            While Reader.Peek <> -1

                ' Get line and its value
                Dim SLine As String = Reader.ReadLine
                Dim SValue As String = SLine.Substring(SLine.IndexOf("=") + 1)

                ' Get boolean conversion (if integer)
                Dim Bool As Boolean = False
                If SValue = "0" Or SValue = "1" Then
                    Bool = BoolToInt(CInt(SValue))
                End If

                Try

                    ' ----- Settings querying -----
                    ' "Anti-setting" setting - disables use of stored settings
                    If SLine.StartsWith("UseStoredStgs=0") Then
                        Return 1
                    End If

                    ' Number settings
                    If SLine.StartsWith("MinCnt=") Then
                        Form1.MinCnt = CInt(SValue)
                        FormOptions.txtMinCnt.Text = SValue
                        Continue While
                    ElseIf SLine.StartsWith("MinLength=") Then
                        Form1.MinLength = CInt(SValue)
                        FormOptions.txtMinLength.Text = SValue
                        Continue While
                    ElseIf SLine.StartsWith("AutoPercentage=") Then
                        Form1.AutoPercentage = CDbl(SValue)
                        FormOptions.txtAutoPrc.Text = SValue
                        Continue While
                    ElseIf SLine.StartsWith("MinAccuracy=") Then
                        Form1.MinAccuracy = CDbl(SValue)
                        FormOptions.txtMinAcc.Text = SValue
                        Continue While
                    ElseIf SLine.StartsWith("IdeaCntLimit=") Then
                        Form1.IdeaCountLimit = CInt(SValue)
                        FormOptions.txtIdeaCountLimit.Text = SValue
                        Continue While
                    End If

                    ' Booleans
                    If SLine.StartsWith("IgnoreCase=") Then
                        Form1.O_IgnoreCase = Bool
                        FormOptions.cbxToLower.Checked = Bool
                        Continue While
                    ElseIf SLine.StartsWith("TypeSpace=") Then
                        Form1.O_TypeSpace = Bool
                        FormOptions.cbxSpace.Checked = Bool
                        Continue While
                    ElseIf SLine.StartsWith("AutoType=") Then
                        Form1.O_AutoType = Bool
                        FormOptions.cbxAuto.Checked = Bool
                        Continue While
                    ElseIf SLine.StartsWith("RecsFollowCursor=") Then
                        Form1.O_RecsFollowCursor = Bool
                        FormOptions.cbxMoveBox.Checked = Bool
                        Continue While
                    ElseIf SLine.StartsWith("NumSelection=") Then
                        Form1.O_NumSelection = Bool
                        FormOptions.cbxNumpadSelection.Checked = Bool
                        Continue While
                    ElseIf SLine.StartsWith("UseNumpad=") Then
                        Form1.O_UseNumberpad = Bool
                        FormOptions.cbxNumSelection_UseNumpad.Checked = Bool
                        Continue While
                    ElseIf SLine.StartsWith("UseCopyPaste=") Then
                        Form1.O_UseCopyPaste = Bool
                        FormOptions.cbxCopyPaste.Checked = Bool
                        Continue While
                    End If


                    ' --- Sorting method ---

                    ' Sort Settings
                    If SLine.StartsWith("TrieDepth=") Then
                        Form1.TrieDepth = CInt(SValue)
                        FormOptions.txtRefTrieDepth.Text = Form1.TrieDepth
                        Continue While
                    ElseIf SLine.StartsWith("TargetCPUUse=") Then
                        Form1.TargetCPUUse = CInt(SValue)
                        FormOptions.tbrCPUConsumption.Value = Form1.TargetCPUUse / 5
                        FormOptions.lblCPUConsumption.Text = "Target CPU Consumption: " & Form1.TargetCPUUse & "%"
                        Continue While
                    End If

                    ' Booleans
                    If SLine.StartsWith("SortLength=") Then
                        Form1.O_S_Length = Bool
                        FormOptions.rbn_srt_Len.Checked = Bool
                        Continue While
                    ElseIf SLine.StartsWith("SortPopln=") Then
                        Form1.O_S_Popln = Bool
                        FormOptions.rbn_srt_pop.Checked = Bool
                        Continue While
                    ElseIf SLine.StartsWith("SortDist=") Then
                        Form1.O_S_Dist = Bool
                        FormOptions.rbn_srt_dst.Checked = Bool
                        Continue While
                    ElseIf SLine.StartsWith("SortNone=") Then
                        Form1.O_S_None = Bool
                        FormOptions.rbn_srt_none.Checked = Bool
                        Continue While
                    End If

                    ' Main document sorting method
                    If SLine.StartsWith("MainDocTriePrd=") Then
                        Form1.O_MDSTrieSrcInterval = CInt(SValue)
                        FormOptions.txtTrieUpdateInterval.Text = Form1.O_MDSTrieSrcInterval
                        Continue While
                    End If

                    ' Customizable key constants (defaults were provided at [5])
                    '   Note: these do not need to update anything on the Options form
                    If SLine.StartsWith("AcceptKey=") Then
                        Form1.VK_Accept = CInt(SValue)
                        Continue While
                    ElseIf SLine.StartsWith("ListUp=") Then
                        Form1.VK_ArrowUp = CInt(SValue)
                        Continue While
                    ElseIf SLine.StartsWith("ListDown=") Then
                        Form1.VK_ArrowDown = CInt(SValue)
                        Continue While
                    ElseIf SLine.StartsWith("SwitchKey=") Then
                        Form1.VK_Switch = CInt(SValue)
                        Continue While
                    ElseIf SLine.StartsWith("ListHide=") Then
                        Form1.VK_ClearList = CInt(SValue)
                        Continue While
                    End If

                    ' Settings memory
                    If SLine.StartsWith("StoreSettings=") Then
                        Form1.SM_StoreSettings = Bool
                        FormOptions.cbx_SM_storeSettings.Checked = Bool
                        Continue While
                    ElseIf SLine.StartsWith("UseStoredStgs=") Then
                        Form1.SM_UseStoredStgs = Bool
                        FormOptions.cbx_SM_useStored.Checked = Bool
                        Continue While
                    ElseIf SLine.StartsWith("UseStoredRefs=") Then
                        Form1.SM_UseStoredRefs = Bool
                        FormOptions.cbx_SM_storedRefs.Checked = Bool
                        Continue While
                    ElseIf SLine.StartsWith("UpdateMode=") Then
                        Form1.UpdateMode = CInt(SValue)
                        FormOptions.rbn_Upd8_Auto.Checked = (CInt(SValue) = 2)
                        FormOptions.rbn_Upd8_Ask.Checked = (CInt(SValue) = 1)
                        FormOptions.rbn_Upd8_None.Checked = (CInt(SValue) = 0)
                        Continue While
                    End If

                Catch
                End Try

            End While

            Reader.Close()

        Catch

            ' Query failed
            Return 0

        End Try

        ' Success!
        Return 1

    End Function

    ' Update settings
    Shared Function UpdateSettingsFile() As Integer

        Dim StrList As New List(Of String)

        ' --- Query settings ---

        ' Settings Memory (Part 1)
        StrList.Add("UseStoredStgs=" & BoolToInt(Form1.SM_UseStoredStgs))

        ' Number settings
        StrList.Add("MinCnt=" & CStr(Form1.MinCnt))
        StrList.Add("MinLength=" & CStr(Form1.MinLength))
        StrList.Add("AutoPercentage=" & CStr(Form1.AutoPercentage))
        StrList.Add("MinAccuracy=" & CStr(Form1.MinAccuracy))
        StrList.Add("IdeaCntLimit=" & CStr(Form1.IdeaCountLimit))

        ' Booleans
        StrList.Add("IgnoreCase=" & BoolToInt(Form1.O_IgnoreCase))
        StrList.Add("TypeSpace=" & BoolToInt(Form1.O_TypeSpace))
        StrList.Add("AutoType=" & BoolToInt(Form1.O_AutoType))
        StrList.Add("RecsFollowCursor=" & BoolToInt(Form1.O_RecsFollowCursor))
        StrList.Add("NumSelection=" & BoolToInt(Form1.O_NumSelection))
        StrList.Add("UseNumpad=" & BoolToInt(Form1.O_UseNumberpad))
        StrList.Add("UseCopyPaste=" & BoolToInt(Form1.O_UseCopyPaste))

        ' Sorting settings
        StrList.Add("TrieDepth=" & CStr(Form1.TrieDepth))
        StrList.Add("TargetCPUUse=" & CStr(Form1.TargetCPUUse))

        ' Sorting method booleans
        StrList.Add("SortLength=" & BoolToInt(Form1.O_S_Length))
        StrList.Add("SortPopln=" & BoolToInt(Form1.O_S_Popln))
        StrList.Add("SortDist=" & BoolToInt(Form1.O_S_Dist))
        StrList.Add("SortNone=" & BoolToInt(Form1.O_S_None))

        ' Main document sorting method
        StrList.Add("MainDocTriePrd=" & Form1.O_MDSTrieSrcInterval)

        ' Customizable key constants [5]
        StrList.Add("AcceptKey=" & CStr(Form1.VK_Accept))
        StrList.Add("ListUp=" & CStr(Form1.VK_ArrowUp))
        StrList.Add("ListDown=" & CStr(Form1.VK_ArrowDown))
        StrList.Add("SwitchKey=" & CStr(Form1.VK_Switch))
        StrList.Add("ListHide=" & CStr(Form1.VK_ClearList))

        ' Settings Memory (part 2)
        StrList.Add("StoreSettings=" & BoolToInt(Form1.SM_StoreSettings))
        StrList.Add("UseStoredRefs=" & BoolToInt(Form1.SM_UseStoredRefs))
        StrList.Add("UpdateMode=" & CStr(Form1.UpdateMode))

        ' --- Write settings to file ---
        '   Try-catch in case file write is denied
        Try

            ' Get filepath
            Dim EPath As String = Process.GetCurrentProcess.MainModule.FileName ' Executable path
            EPath = EPath.Substring(0, Math.Max(EPath.LastIndexOf("\"), EPath.LastIndexOf("/"))) ' File executable is in

            ' Write to target file
            IO.File.WriteAllLines(EPath & "\" & Form1.SettingsFile, StrList)

            ' Report success
            Return 1

        Catch

            ' Report failure
            Return 0

        End Try

    End Function

    ' Validate reference files
    Shared Function ValidateReference(ByVal R_FilePath As String, ByVal DisplayMsgBoxes As Boolean, ByVal AllowTextFiles As Boolean) As Integer

        ' This function returns an error code based on what about the reference is invalid (if anything)
        ' Error code guide
        '   0 - File path is VALID
        '   1 - File path is null
        '   2 - File does not exist
        '   3 - Invalid file extension
        '   4 - Referencing a reference list (recursive referencing)
        '   5 - File is already referenced

        ' Null files are invalid
        If String.IsNullOrWhiteSpace(R_FilePath) Then
            Return 1
        End If

        ' If file path isn't valid, display the appropriate error message
        If Not IO.File.Exists(R_FilePath) Then

            If DisplayMsgBoxes Then
                MsgBox("One or more of the files you specified for referencing does not exist. Please only use files that exist as references.", MsgBoxStyle.Exclamation)
            End If

            Return 2
        End If

        ' Get extension
        Dim R_FileExt As String = Form1.GetFileExt(R_FilePath)

        ' If extension isn't compatible, the file is invalid
        If Not Form1.DocFileExts.Contains(R_FileExt) Then

            ' Avoid recursive references (file lists that reference other file lists) - this capability isn't supported
            If Form1.PlainTextFileExts.Contains(R_FileExt) And Not AllowTextFiles Then

                If DisplayMsgBoxes Then
                    MsgBox("One or more of the files you specified in the reference list(s) is another list of references. ContexType does not have support for this capability. These files will be ignored.", MsgBoxStyle.Exclamation)
                End If

                Return 4
            ElseIf Not Form1.PlainTextFileExts.Contains(R_FileExt) Then

                If DisplayMsgBoxes Then
                    MsgBox("One or more of the files you specified for referencing is not an acceptable type. Please only use plain text files (" & String.Join(", ", Form1.PlainTextFileExts) & ") and Microsoft Word documents (" & String.Join(", ", Form1.DocFileExts) & ").", MsgBoxStyle.Exclamation)
                End If

                Return 3
            End If

        ElseIf Form1.EntirePathFileList.Contains(R_FilePath) Then

            If DisplayMsgBoxes Then
                MsgBox("One or more of the files you specified for referencing is referenced multiple times. Please remove the current references. Alternatively, select them and use the REFRESH (O) button.", MsgBoxStyle.Exclamation)
            End If

            Return 5

        End If

        ' File is valid
        Return 0

    End Function

    ' Get references
    Shared Function QueryReferences() As Integer

        ' Get parent path
        Dim EPath As String = Process.GetCurrentProcess.MainModule.FileName
        EPath = EPath.Substring(0, Math.Max(EPath.LastIndexOf("\"), EPath.LastIndexOf("/")) + 1) & Form1.RefsFile

        ' Get reference file
        Dim RefsPathFileList As New List(Of String)
        If String.IsNullOrWhiteSpace(Dir(EPath)) Then
            Return 0 ' Return 0 if the reference file doesn't exist at the proper path
        End If

        Try

            ' Clear past references
            Form1.lbox_files.Items.Clear()
            Form1.EntirePathFileList.Clear()

            ' Get references
            Dim Reader As New IO.StreamReader(EPath)
            While Reader.Peek <> -1

                Try
                    Dim Line As String = Reader.ReadLine
                    Dim LineExt As String = Line.Substring(Line.LastIndexOf("."))

                    If Settings.ValidateReference(Line, False, False) = 0 Then
                        Form1.AddReference(Line)
                    End If
                Catch
                End Try

            End While
            Reader.Close()

            Return 1

        Catch
            Return 0
        End Try

    End Function

End Class
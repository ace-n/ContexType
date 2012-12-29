#Region "References"
' Open source code released by Aessa Nassri.

' Source references --> [#] denotes that the designated source (listed below) was instrumental/useful in creating that code
'   Note that this labeling is NOT EXHAUSTIVE! (All sources are included, but not all uses are labeled)
' [1]: http://dotnetdud.blogspot.com/2008/10/how-to-get-number-of-processors-in.html
' [2]: http://www.vb-helper.com/howto_net_custom_sort_array.html
' [3]: http://msdn.microsoft.com/en-us/library/84787k22.aspx
' [4]: http://www.officekb.com/Uwe/Forum.aspx/word-vba/11087/Determing-screen-X-Y-coords-from-current-Word-text-cursor-position
' [5]: http://msdn.microsoft.com/en-us/library/dd375731%28v=VS.85%29.aspx [VK Codes]
' [6]: Teachers (terminology)
' http://msdn.microsoft.com/en-us/library/aa289508%28v=vs.71%29.aspx#vbtchimpdragdropanchor6
' [7]: http://msdn.microsoft.com/en-us/library/windows/desktop/ms646301%28v=vs.85%29.aspx (GetKeyState - used for key mapping)

' [8]: http://xlinux.nist.gov/dads//HTML/trie.html
' [9]: http://community.topcoder.com/tc?module=Static&d1=tutorials&d2=usingTries
' [10]: http://social.msdn.microsoft.com/Forums/en-US/vbgeneral/thread/c1a24688-d844-4adc-9d85-416a7158c6ba/ [WndProc + Hotkeys]
' [11]: http://www.techrepublic.com/blog/programming-and-development/download-files-over-the-web-with-nets-webclient-class/695

' Updater (cmd.exe syntax)
' http://ss64.com/nt/del.html
' http://ss64.com/nt/waitfor.html
' http://ss64.com/nt/copy.html
' http://ss64.com/nt/echo.html

#End Region

Imports Microsoft.Office.Interop
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Net

Public Class Form1

    ' ----- Google project info - used in autoupdate -----
    ' Current version (MUST BE AN INTEGER)
    Public Version As Integer = 33

    ' Latest version path
    Public VersionURL As String = "http://contextype.googlecode.com/svn/latestversion.txt"

    ' Release path
    Public ReleaseURL As String = "http://contextype.googlecode.com/svn/release/"

    ' TO DO

    '   Add referencing functionality to update references if reference documents are updated
    '   Add auto-reference options (reference documents in same folder? other folders?)
    '   Add notepad/edit control support?

    ' NOTE: When they are loaded, references do not update if they are changed (saved) - fix this or leave it?

#Region "Declarations"

    ' --------- Settings variables ---------

    ' Global settings variables
    Public TrieDepth As Integer = 4
    Public MinCnt As Integer = 1
    Public MinLength As Integer = 4
    Public AutoPercentage As Double = 0.2
    Public MinAccuracy As Double = 0
    Public AutoTypeCoolingDown As Boolean = False
    Public TargetCPUUse As Integer = 15
    Public MasterLocation As Point
    Public IdeaCountLimit As Integer
    Public UpdateMode As Integer

    Public SettingsFile As String = "ctype_settings.txt"
    Public RefsFile As String = "ctype_references.txt"

    ' Boolean transmits
    Public O_Reverse As Boolean = False
    Public O_IgnoreCase As Boolean = True
    Public O_TypeSpace As Boolean = False
    Public O_AutoType As Boolean = False
    Public O_EntireWord As Boolean = False
    Public O_RecsFollowCursor As Boolean = True
    Public O_NumSelection As Boolean = False
    Public O_UseNumberpad As Boolean = True
    Public O_UseCopyPaste As Boolean = False

    ' Sorting method
    Public O_S_Length As Boolean
    Public O_S_Popln As Boolean
    Public O_S_Dist As Boolean = True ' Starting setting
    Public O_S_None As Boolean

    ' Main document sorting data
    Public O_MDSMethodIdx As Integer = 0
    Public O_MDSTrieSrcInterval As Integer = 0

    ' Customizable key constants [5]
    Public VK_Accept As Integer = &H9               ' TAB key
    Public VK_Switch As Integer = &H12              ' ALT key
    Public VK_ClearList As Integer = Keys.Escape    ' ESC key
    Public VK_ArrowUp As Integer = &H26
    Public VK_ArrowDown As Integer = &H28
    Public VK_ArrowLeft As Integer = &H25
    Public VK_ArrowRight As Integer = &H27

    ' Settings memory
    Public SM_StoreSettings As Boolean = False
    Public SM_UseStoredStgs As Boolean = True
    Public SM_UseStoredRefs As Boolean = True

    ' ------ Misc. Global Variables ------

    ' Fixed key constants [5]
    Public VK_Enter As Integer = Keys.Enter
    Public VK_Space As Integer = Keys.Space

    ' Word document interfaces
    Shared WordAppBrowser As New Word.Application
    Public WordApp As Word.Application
    Public WordDoc As Word.Document

    ' List of sorted recommendations
    Public SortedRecommendations As New List(Of String)

    ' Current word being modified
    Public WordCurrent As String = ""

    ' Cumulative list of words in trie format
    Public ReferenceTries As New List(Of NamedCountedList)

    ' List of allowed file types
    '   Only use Microsoft Word DOM compatible or plaintext files - anything else CAN MESS UP THE SYSTEM!

    ' Microsoft Word Documents
    Public DocFileExts As String() = {".doc", ".docx"}

    ' Plain text files
    Public PlainTextFileExts As String() = {".txt", ".cfg"}

    ' Misc
    Public WordText As String                   ' Text of active Word Document
    Public RandGen As New Random
    Public TitleLength As Integer = 150         ' Length of title searches to determine if a window is a Word Document
    Public FreezeActiveText As Boolean = False  ' If this is TRUE, the active document text will not be updated
    Public FreezeRecs As Boolean = False
    Public CurProcPercentage As Integer = 0
    Public MainFormLoaded As Boolean = False

    ' Inter-document data lists
    Dim InterDocWords As New List(Of List(Of String))
    Dim InterDocCount As New List(Of List(Of Integer))

    ' File list (uses complete paths, not just partial ones)
    Public EntirePathFileList As New List(Of String)

    ' ----- Text Scanning/Handling -----

    ' Old data
    Dim RecsOld As New List(Of Recommendation)

    ' Text strings
    Dim WordTextPrev, WordTextTest As String
    Dim DocStr, TxtStr As String

#End Region

#Region "P/invoke functions"
    Declare Function GetKeyState Lib "user32" (nVirtKey As Integer) As Short
    Declare Function GetForegroundWindow Lib "user32" () As Integer
    Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hWnd As Integer, ByVal lpString As String, ByVal nMaxCount As Integer) As Integer
    Declare Function Sleep Lib "kernel32" (ByVal dwMilliseconds As Integer) As Integer
    Declare Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
#End Region

    ' Start-up functions
    Public Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        ' Auto-version title
        Me.Text &= " " & CStr(Version / 100 + 1)

        ' Misc
        MasterLocation = Me.Location

        ' Set up word browser (an invisible instance of Word used for accessing referenced Word Documents)
        WordAppBrowser.Top = 0
        WordAppBrowser.Visible = False

        ' Start trie worker
        MainDocTrieWorker.RunWorkerAsync()

        ' Update settings
        MinAccuracy = 0.1
        MinCnt = 1
        MinLength = 4

        ' Get strings of file types
        For Each Type As String In DocFileExts
            DocStr = DocStr & Type & ","
        Next
        DocStr = DocStr.Substring(0, DocStr.Length - 1)

        For Each Type As String In PlainTextFileExts
            TxtStr = TxtStr & Type & ","
        Next
        TxtStr = TxtStr.Substring(0, TxtStr.Length - 1)

        ' Query for stored settings (the system automatically uses stored settings if they are
        '   available; thus, the user must de-activate them each launch or reset them outright)
        Dim QuerySuccess As Integer = Settings.QuerySettingsFile

        ' Update checking (if enabled in settings)
        If UpdateMode <> 0 Then

            Dim UpdatesOK As Integer = Updates.CheckForUpdate(VersionURL)
            If UpdatesOK = -1 Then
                MsgBox("The ContexType Auto-Updater could not check whether the current version is the most up to date." & vbCrLf & vbCrLf & _
                       "Check your internet connectivity and make sure that http://contextype.googlecode.com is accessible.")
            ElseIf UpdatesOK = 1 And UpdateMode <> 0 Then

                If UpdateMode = 1 Then

                    ' Ask user for permission to download
                    Dim MBox As MsgBoxResult = MsgBox("A new ContexType update is available. Would you like to download it?", MsgBoxStyle.YesNo)
                    If MBox = MsgBoxResult.Yes Then
                        Updates.ExecuteUpdate()
                    End If

                ElseIf UpdateMode = 2 Then
                    ' Auto-update
                    Updates.ExecuteUpdate()
                End If

            End If

        End If


        ' If query failed, revert to default
        If QuerySuccess = 0 Then
            Settings.DefaultSettings()
        End If

        ' Start throttling procedure
        ThrottleWorker.RunWorkerAsync()

        ' Start text monitoring procedure
        TextWorker.WorkerReportsProgress = True
        TextWorker.RunWorkerAsync()

        ' Start hopper procedure
        HopperWorker.WorkerReportsProgress = True
        HopperWorker.RunWorkerAsync()

        ' Start window change procedure
        WindowChangeWorker.RunWorkerAsync()

        ' Main form is loaded in next operation
        '   This allows notification messages about references/settings to be shown
        MainFormLoaded = True

    End Sub

    ' Screen position handlers
    Public Sub UpdateLocation() Handles Me.Move
        MasterLocation = Me.Location
    End Sub
    Public Sub Reposition() Handles Me.VisibleChanged
        Me.Location = MasterLocation
    End Sub

    ' Key remapping
    Public Sub Remap(ByRef RemapTarget As Integer)

        Dim MBR As MsgBoxResult = MsgBox("Do you want to remap this key?", MsgBoxStyle.YesNo)
        If MBR = MsgBoxResult.No Then
            MsgBox("Key changing operation was cancelled")
            Exit Sub
        Else
            MsgBox("Press and hold the new key you want to use.")
        End If

        ' Wait a bit
        Sleep(1000)

        ' Refresh active key list
        Dim KeyList As New List(Of Integer)

        For i = 0 To 254

            ' Get key state
            If GetKeyState(i) < 0 Then
                RemapTarget = i

                ' Refresh hotkeys if necessary
                If HotkeysActive Then
                    HotkeysOff()
                    HotkeysOn()
                End If

                ' Report success
                MsgBox("Key remapping operation successful!")

                Exit Sub
            End If

        Next

        ' Report failure
        MsgBox("Key mapping operation failed because no key was pressed. The old settings have not been changed.")

    End Sub

    ' Settings storage

    ' Hide hopper if mouse is clicked elsewhere
    Dim WordOldSelection As Word.Range

    ' Update hopper if recommendations have changed
    Public Sub MoveHopper(ByVal sender As System.Object, ByVal e As ProgressChangedEventArgs) Handles TextWorker.ProgressChanged

        ' Make hopper form (in)visible
        Dim Similarity As Integer = 0
        If Not String.IsNullOrWhiteSpace(WordCurrent) Then
            Similarity = StringManipulation.GetAccuracyPercentage(WordCurrent, SortedRecommendations, Not O_EntireWord)
        End If
        Dim HopperVisible As Boolean = (SortedRecommendations.Count > 0) And (Similarity > (MinAccuracy * 100)) And (GetActiveWindowTitle(TitleLength).Contains("Microsoft Word"))
        If HopperVisible <> Hopper.Visible Then
            Hopper.Visible = HopperVisible
        End If

        ' Get current selection index
        Dim SelectedIndexCur As Integer = Hopper.lbox_ideas.SelectedIndex

        ' Get frozen-in-time sorted recommendations
        Dim SortedNow As New List(Of String)
        SortedNow.AddRange(SortedRecommendations)

        ' If main form is active, hide hopper
        If GetActiveWindowTitle(TitleLength).Contains(Me.Text) Then
            Hopper.TopMost = False
        End If

        ' If there is an active current word, work with the hopper form
        If WordCurrent <> "" Then

            ' Keep hopper active
            If SortedNow.Count > 0 And GetActiveWindowTitle(TitleLength).Contains("Microsoft Word") Then
                Hopper.TopMost = True
            End If

            ' Get position of word text [4]
            Dim WordWin As Word.Window = WordApp.ActiveWindow
            Dim PosX, PosY As Integer

            Try
                WordWin.GetPoint(PosX, PosY, New Integer, New Integer, WordApp.Selection.Range)
                PosY += WordApp.Selection.Font.Size * 2 ' So it appears below the text
            Catch
                ' If range is null, just get current hopper position 
                PosX = Hopper.Location.X
                PosY = Hopper.Location.Y
            End Try

            ' Position hopper form
            If O_RecsFollowCursor Then
                Hopper.Location = New Point(PosX, PosY)
            End If

            ' Update the recommendation box iff there are new recommendations
            If (e.ProgressPercentage = 1) Then

                ' Clear hopper form
                Hopper.lbox_ideas.Items.Clear()

                ' Update hopper form
                Hopper.lbox_ideas.Items.Clear()
                For i = 0 To SortedNow.Count - 1

                    ' In loop exception handler
                    If i > SortedNow.Count - 1 Then
                        Exit For
                    End If
                    Hopper.lbox_ideas.Items.Add(SortedNow.Item(i))

                Next

                ' Update the numpad selection list (the list box to the right of the idea list) if applicable
                If O_NumSelection And SortedNow.Count <> 0 Then

                    Hopper.lbox_nums.Items.Clear() ' Reset the number list

                    For i = 0 To SortedNow.Count - 1
                        If i < Hopper.lbox_ideas.SelectedIndex Then
                            Hopper.lbox_nums.Items.Add("")
                        Else
                            Hopper.lbox_nums.Items.Add(CStr(i - Hopper.lbox_ideas.SelectedIndex))
                        End If
                    Next

                End If

            End If
        End If

        ' Reselect previous word (if possible)
        If SelectedIndexCur + 1 <= Hopper.lbox_ideas.Items.Count And SelectedIndexCur >= 0 Then
            Hopper.lbox_ideas.SetSelected(SelectedIndexCur, True)
        End If

        ' --------------------------------- Keyboard handling ---------------------------------
        ' If character similarity between top suggestion and current word is within a tolerance amount AND autotype is enabled,
        ' automatically type the best selection
        If Hopper.lbox_ideas.Items.Count > 0 And Not AutoTypeCoolingDown Then

            ' Get complete word
            Dim CompleteWord As String = Hopper.lbox_ideas.Items.Item(0)
            If Not O_EntireWord Then
                CompleteWord = WordCurrent & CStr(Hopper.lbox_ideas.Items.Item(0))
            End If

            ' Autotype
            If O_AutoType And CompleteWord <> WordCurrent And CompleteWord.Contains(WordCurrent) Then

                ' Get similarity
                Dim SimilarityActual As Integer = StringManipulation.GetAccuracyPercentage(WordCurrent, SortedNow, Not O_EntireWord) / 100
                Dim Similar As Boolean = False

                ' Check similarity
                If AutoPercentage < 1 Then
                    Similar = SimilarityActual > AutoPercentage
                Else
                    Similar = (SimilarityActual * WordCurrent.Length) > Math.Round(AutoPercentage)
                End If

                ' Type
                If Similar Then

                    ' Send keys
                    StringManipulation.SendText(CompleteWord.Substring(WordCurrent.Length) & " ", O_UseCopyPaste)

                    ' Cool down
                    WordCurrent = CompleteWord
                    CompleteWord = ""
                    Sleep(100)

                End If

            End If

        End If
    End Sub

    ' Toggle hotkeys [10]
    Public Sub HotkeysOn()
        Call RegisterHotKey(Me.Handle, 1, 0, VK_ArrowUp)
        Call RegisterHotKey(Me.Handle, 2, 0, VK_ArrowDown)
        Call RegisterHotKey(Me.Handle, 3, 0, VK_Accept)
        Call RegisterHotKey(Me.Handle, 4, 0, VK_ClearList)
        Call RegisterHotKey(Me.Handle, 5, 0, VK_Space)
        Call RegisterHotKey(Me.Handle, 6, 0, VK_Enter)

        If O_NumSelection Then
            NumpadHotkeysOn()
        End If

    End Sub
    Public Sub HotkeysOff()
        Call UnregisterHotKey(Me.Handle, 1)
        Call UnregisterHotKey(Me.Handle, 2)
        Call UnregisterHotKey(Me.Handle, 3)
        Call UnregisterHotKey(Me.Handle, 4)
        Call UnregisterHotKey(Me.Handle, 5)
        Call UnregisterHotKey(Me.Handle, 6)

        ' Note - there is no if statement around this
        '   This is so that the numpad hotkeys won't be left on if the numpad enable status is changed during typing
        NumpadHotkeysOff()

    End Sub

    Public Sub NumpadHotkeysOn()

        Dim Add As Integer = 0
        If O_UseNumberpad Then
            Add = 48
        End If

        For i = 0 To 8
            Call RegisterHotKey(Me.Handle, 7 + i, 0, 48 + Add + i)
        Next

    End Sub
    Public Sub NumpadHotkeysOff()

        Dim Add As Integer = 0

        For i = 0 To 8
            Call UnregisterHotKey(Me.Handle, 7 + i)
        Next

    End Sub

    ' Window changing
    Public PastDocumentHWND As Integer = 0
    Public Sub WindowChangeUpdate(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles WindowChangeWorker.DoWork

        While True

            ' Reduce processor strain
            Throttle(False)

            ' Skip if no active word doc
            If Not GetActiveWindowTitle(TitleLength).Contains("Microsoft Word") Then
                Continue While
            End If


            ' Get active document HWND
            Dim CurDocumentHWND As Integer = GetForegroundWindow()

            ' If no document change has occurred, don't bother updating the window data
            If WordApp IsNot Nothing Then
                If CurDocumentHWND = PastDocumentHWND Then
                    Continue While
                End If
            End If

            ' If word text is frozen, prevent updating
            If FreezeActiveText Then
                Continue While
            End If

            ' --- Everything below here only occurs if the word document has changed ---

            ' Wait for a bit (to prevent VB going too fast and causing errors)
            Throttle(True)

            ' Update HWND
            PastDocumentHWND = CurDocumentHWND

            ' Update global word application/document
            WordApp = CType(Marshal.GetActiveObject("Word.Application"), Word.Application)

            ' If document is nil, skip it
            Try
                WordDoc = WordApp.ActiveDocument
            Catch
                Continue While
            End Try

            ' Trigger document re-scan
            RescanDocument()

        End While

    End Sub


    ' Get active window title
    Private Function GetActiveWindowTitle(ByVal StrLen As Integer) As String

        ' Get active title to specified length
        Dim ActiveTitle As New String(" ", StrLen)
        GetWindowText(GetForegroundWindow(), ActiveTitle, StrLen)

        ' Trim spaces from end of active title
        While (ActiveTitle.Length > 3 And String.IsNullOrWhiteSpace(ActiveTitle.Substring(ActiveTitle.Length - 1)))
            ActiveTitle = ActiveTitle.Substring(0, ActiveTitle.Length - 1)

            ' If the title is getting too short (such that continuing will cause an error), stop the trimming
            If ActiveTitle.Length < 4 Then
                Exit While
            End If

        End While

        ' Return the title
        Return ActiveTitle

    End Function

#Region "Hotkeys"

    ' Hotkey activation
    Public HotkeysActive As Boolean = False
    Public Sub ActivationUpdate(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles HopperWorker.ProgressChanged

        ' --- Hotkey stuff ---

        ' Keep track of change
        Dim HotkeysWereActive As Boolean = HotkeysActive

        ' Get hotkey's ideal status
        If SortedRecommendations.Count > 0 And GetActiveWindowTitle(TitleLength).Contains("Microsoft Word") Then
            HotkeysActive = Hopper.Visible
        Else
            HotkeysActive = False
        End If

        ' If settings have changed, run the appropriate subroutine
        If HotkeysActive <> HotkeysWereActive Then
            If HotkeysActive Then
                HotkeysOn()
            Else
                HotkeysOff()
            End If
        End If

        ' --- End hotkey stuff ---

        ' Hide the hopper (if applicable)
        If Not GetActiveWindowTitle(TitleLength).Contains("Microsoft Word") And Hopper.Visible Then
            If GetActiveWindowTitle(Hopper.Text.Length) = Hopper.Text Then ' 4/18/2012: Possible fix for (still) disappearing hopper (when clicked by mouse - this is the 2nd fix)
                WordApp.Activate()
            Else
                Hopper.Visible = False
            End If
        End If

        ' If recommendations are nil, exit sub
        If SortedRecommendations.Count = 0 Then
            Exit Sub
        End If

        ' Automatically select best recommendation
        If Hopper.lbox_ideas.Items.Count > 0 And Hopper.Visible Then
            If Hopper.lbox_ideas.SelectedIndex = -1 Then
                Hopper.lbox_ideas.SelectedIndex = 0
            End If
        End If

        ' Update hopper progress bar
        '   Try-catch statement is only because this has produced a "random error" before
        Try
            Hopper.pbar.Value = StringManipulation.GetAccuracyPercentage(WordCurrent, SortedRecommendations, Not O_EntireWord)
        Catch
        End Try

    End Sub


    ' Remove hotkeys
    Private Sub OnClose(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        ' Save settings
        If FormOptions.cbx_SM_storeSettings.Checked Then
            Settings.UpdateSettingsFile()
        End If

        ' Save references
        Dim EPath As String = Process.GetCurrentProcess.MainModule.FileName ' Executable path
        EPath = EPath.Substring(0, Math.Max(EPath.LastIndexOf("\"), EPath.LastIndexOf("/")) + 1) ' Executable folder directory
        EPath &= RefsFile ' Path to reference file

        Try
            IO.File.WriteAllLines(EPath, EntirePathFileList.ToArray)
        Catch
        End Try

        ' Close browser document
        Try
            WordAppBrowser.Quit(SaveChanges:=False)
        Catch ex As Exception
        End Try


        ' Close workers
        HopperWorker.Dispose()
        TextWorker.Dispose()

        ' Close hopper
        Hopper.Close()

        ' Turn hotkeys off
        HotkeysOff()

    End Sub

    Private Sub ExitToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ExitToolStripMenuItem1.Click
        ' Close forms
        Hopper.Close()
        Me.Close()
    End Sub

    ' Message interceptor (for hotkeys) [10]
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)

        ' TEST


        ' Check if message is a hotkey
        Dim ActiveTitle As String = GetActiveWindowTitle(TitleLength)
        If m.Msg = &H312 Then

            ' Handle hotkeys if Microsoft Word is active and at least 1 recommendation exists
            If SortedRecommendations.Count > 0 And ActiveTitle.Contains("Microsoft Word") Then

                ' If the keypress is a space and the hopper is active, send a space key to supplement the lost one
                If Hopper.Visible And m.WParam = 5 Then
                    HotkeysOff()
                    StringManipulation.SendText(" ", O_UseCopyPaste)
                End If

                ' Arrow keys - numbers correspond to hotkey ID's
                Dim SelectIndex As Integer = Hopper.lbox_ideas.SelectedIndex
                If m.WParam = 2 Then      ' Down
                    SelectIndex += 1
                ElseIf m.WParam = 1 Then ' Up
                    SelectIndex -= 1
                End If

                ' Cycle from bottom of text to top and vice versa (if using arrow keys)
                If m.WParam.ToInt32 < 3 Then

                    ' If the select index is out of the bounds of the recommendations list, reset it so that it is back within bounds
                    '   This works so that if the user presses the next solution key on the last recommendation, the first recommendation is selected
                    If SelectIndex = -1 Then
                        SelectIndex = Hopper.lbox_ideas.Items.Count - 1
                    ElseIf SelectIndex > Hopper.lbox_ideas.Items.Count - 1 Then
                        SelectIndex = 0
                    End If

                    ' Word switching
                    If Hopper.lbox_ideas.Items.Count > 0 Then
                        SelectIndex = SelectIndex - Math.Floor(SelectIndex / Hopper.lbox_ideas.Items.Count)
                        Hopper.lbox_ideas.SelectedIndex = SelectIndex
                    Else
                        SelectIndex = 0
                    End If

                End If

                ' Tab key - used to enter recommended text
                If m.WParam = 3 Then

                    ' If no word is selected, select the top one
                    If Hopper.lbox_ideas.SelectedIndex = -1 Then
                        Hopper.lbox_ideas.SelectedIndex = 0
                    End If

                    ' Get word
                    Dim SendWord As String = Hopper.lbox_ideas.SelectedItem

                    ' Remove invalid characters from SendWord
                    SendWord = SendWord.Replace("{", "").Replace("}", "")

                    ' Entire word option handling
                    Try
                        If O_EntireWord Then
                            StringManipulation.SendText(SendWord.Substring(WordCurrent.Length), O_UseCopyPaste)
                        Else
                            StringManipulation.SendText(SendWord, O_UseCopyPaste)
                        End If
                    Catch
                    End Try

                    ' Set current word equal to sent word (to avoid recommending the typed word)
                    If Not O_EntireWord Then
                        WordCurrent += SendWord + "q"
                    Else
                        WordCurrent = SendWord + "q"
                    End If

                End If

                ' Clear the recommendation list if ESC, space, tab, or enter is pressed
                If m.WParam.ToInt32 > 2 And m.WParam.ToInt32 < 7 Then

                    If Not m.WParam = 3 Then
                        WordCurrent = ""
                    End If

                    ' Repeat the keypress
                    Hopper.Visible = False
                    SortedRecommendations.Clear()
                    FreezeRecs = True

                End If

                ' Handle numpad recommendation selection
                If m.WParam.ToInt32 > 6 Then

                    Dim Idx As Integer = -1
                    For i = 0 To Hopper.lbox_nums.Items.Count - 1 ' Added the -1 on 10/14/12 - this was causing an index out of range exception with the numpad selection system
                        If CInt(Hopper.lbox_nums.Items.Item(i)) = m.WParam.ToInt32 - 7 Then
                            Idx = i
                            Exit For
                        End If
                    Next

                    If Idx <> -1 Then

                        ' --- This section is basically copied from the tab recommendation insertion ---

                        ' Get word
                        Dim SendWord As String = Hopper.lbox_ideas.Items.Item(Idx)

                        ' Remove invalid characters from SendWord
                        SendWord = SendWord.Replace("{", "").Replace("}", "")

                        ' Entire word option handling
                        Try
                            If O_EntireWord Then
                                StringManipulation.SendText(SendWord.Substring(WordCurrent.Length), O_UseCopyPaste)
                            Else
                                StringManipulation.SendText(SendWord, O_UseCopyPaste)
                            End If
                        Catch
                        End Try

                        ' Set current word equal to sent word (to avoid recommending the typed word)
                        If Not O_EntireWord Then
                            WordCurrent += SendWord + "q"
                        Else
                            WordCurrent = SendWord + "q"
                        End If

                    End If

                End If

                ' Return if hotkey pressed
                Return

            Else

                ' Don't block the keypress
                MyBase.WndProc(m)
                Return

            End If

        Else

            ' Don't block the keypress
            MyBase.WndProc(m)
            Return

        End If
    End Sub


#End Region

#Region "Document re-scanning"
    ' Rescan document if case is changed
    Public Sub RescanDocument()

        ' Reset lists
        RecsOld.Clear()


        ' Trigger rescan
        WordTextPrev = ""

    End Sub

    Private Sub cbxToLower_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RescanDocument()
    End Sub

    ' Rescan document
    Private Sub btnRescan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RescanCurrentDocumentToolStripMenuItem.Click
        RescanDocument()
    End Sub
#End Region

#Region "Reference controls"

    ' Add reference button
    Private Sub btn_AddRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_AddRef.Click

        Dim NoErrorsSoFar As Boolean = True

        ' Display dialog
        fileopener.ShowDialog()

        ' Add any selected files
        If fileopener.FileNames.Count > 0 Then
            For Each File In fileopener.FileNames

                ' Get file extension
                Dim FileExt As String = GetFileExt(File)

                ' If file is a word document (.doc, .docx), use the add reference method
                If Settings.ValidateReference(File, NoErrorsSoFar, False) = 0 Then
                    AddReference(File)
                Else
                    NoErrorsSoFar = True
                End If

            Next
        End If

    End Sub

    ' Remove reference(s) button
    Private Sub btn_RemoveRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_RemoveRef.Click

        If lbox_files.SelectedIndices.Count > 0 Then

            ' Sort indices in descending order (note: this prevents selection order errors)
            Dim SelectedIndices As New List(Of Integer)
            For Each I As Integer In lbox_files.SelectedIndices
                SelectedIndices.Add(I)
            Next
            SelectedIndices.Sort()
            SelectedIndices.Reverse()

            ' Remove references
            For Each Index In SelectedIndices

                ' Get corresponding index in references
                Dim RefIndex As Integer = -1
                For i = 0 To ReferenceTries.Count - 1

                    ' Check to see if the current selection index matches the specified reference
                    If ReferenceTries.Item(i).Name = EntirePathFileList.Item(Index) Then
                        RefIndex = i
                        Exit For
                    End If

                Next

                ' Annull the selected filepath from the reference and entire filepaths list
                lbox_files.Items.Item(Index) = ""
                EntirePathFileList.RemoveAt(Index)

                ' Remove the specified reference from the reference table if it exists
                If RefIndex > -1 Then
                    ReferenceTries.RemoveAt(RefIndex)
                End If

            Next

            ' Remove any null entries from the reference filepath list
            Dim PastFiles As New List(Of String)
            For Each FilePath As String In lbox_files.Items
                PastFiles.Add(FilePath)
            Next

            ' Update file list in main window
            lbox_files.Items.Clear()
            For Each FilePath In PastFiles
                If Not String.IsNullOrWhiteSpace(FilePath) Then
                    lbox_files.Items.Add(FilePath)
                End If
            Next

        Else
            MsgBox("No references selected for removal.")
        End If

    End Sub

    ' Copy references to clipboard (one per line)
    Private Sub btn_CopyRefs_Click() Handles btn_CopyRefs.Click

        ' Clear clipboard
        Clipboard.Clear()

        ' Get selected references
        Dim ReferencesStr As String = ""
        If lbox_files.SelectedIndices.Count > 0 Then
            For Each Index As Integer In lbox_files.SelectedIndices
                ReferencesStr &= lbox_files.Items.Item(Index).ToString & vbCrLf
            Next
        Else
            For Each Item As String In lbox_files.Items
                ReferencesStr &= Item & vbCrLf
            Next
        End If

        ' Populate clipboard
        Clipboard.SetText(ReferencesStr)

    End Sub

    ' Update reference(s) button (removes them, then adds them back)
    Private Sub btnUpdateRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_UpdateRef.Click

        If lbox_files.SelectedIndices.Count > 0 Then

            ' Refreshed references
            Dim RefreshedRefs As New List(Of String)

            ' Remove references
            Dim RemoveRefs As New List(Of Integer)
            For Each Index In lbox_files.SelectedIndices

                ' Get corresponding index in references
                Dim RefIndex As Integer = -1
                For i = 0 To ReferenceTries.Count - 1

                    ' Check to see if the current selection index matches the specified reference
                    If ReferenceTries.Item(i).Name = EntirePathFileList.Item(Index) Then
                        RemoveRefs.Add(Index)
                        RefIndex = i
                        Exit For
                    End If

                Next

                ' Add the selected entire filepath to the refreshed references list
                RefreshedRefs.Add(EntirePathFileList.Item(Index))

                ' Remove the specified reference from the reference table if it exists
                If RefIndex > -1 Then
                    ReferenceTries.RemoveAt(RefIndex)
                End If

            Next

            ' Remove any refreshed entries from the reference filepath list
            RemoveRefs.Sort()
            RemoveRefs.Reverse() ' Ends up in descending order
            For Each Index As Integer In RemoveRefs
                lbox_files.Items.Item(Index) = ""
                EntirePathFileList.RemoveAt(Index)
            Next

            ' Remove any null entries from the reference filepath list
            Dim PastFiles As New List(Of String)
            For Each FilePath As String In lbox_files.Items
                PastFiles.Add(FilePath)
            Next

            ' Update file list in main window
            lbox_files.Items.Clear()
            For Each FilePath In PastFiles
                If Not String.IsNullOrWhiteSpace(FilePath) Then
                    lbox_files.Items.Add(FilePath)
                End If
            Next

            ' File error booleans (to avoid spamming errors)
            Dim FileNotExistError As Boolean = False
            Dim WrongTypeError As Boolean = False
            Dim FileRepeatedError As Boolean = False

            ' Add files back
            For Each FilePath In RefreshedRefs


                ' Make sure file exists - if not, skip it
                If Not Dir(FilePath) <> "" And Not FileNotExistError Then
                    FileNotExistError = True
                    MsgBox("One or more of the files you specified for referencing does not exist. Please only use files that exist as references.", MsgBoxStyle.Exclamation)
                    Continue For
                End If

                ' Get extension
                Dim FileExt As String = FilePath.Substring(FilePath.LastIndexOf("."))

                ' Make sure file extension is accepted - otherwise skip it
                If Not (PlainTextFileExts.Contains(FileExt) Or DocFileExts.Contains(FileExt)) And Not WrongTypeError Then
                    WrongTypeError = True
                    MsgBox("One or more of the files you specified for referencing is not an acceptable type. Please only use plain text files (" & TxtStr & ") and Microsoft Word documents (" & DocStr & ").", MsgBoxStyle.Exclamation)
                    Continue For
                End If

                ' Make sure the reference isn't repeated
                If EntirePathFileList.Contains(FilePath) And Not FileRepeatedError Then
                    MsgBox("One or more of the files you specified for referencing is referenced multiple times. Please remove the current references. Alternatively, select them and use the REFRESH (O) button.", MsgBoxStyle.Exclamation)
                    FileRepeatedError = True
                    Continue For
                End If

                AddReference(FilePath)
            Next

        Else
            MsgBox("No references selected for refreshment.")
        End If

    End Sub

    ' File List management
    Private Sub ListBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lbox_files.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then

            ' Thrown Error Boolean - to avoid spamming messages over and over again
            Dim NoErrorsSoFar As Boolean = True

            ' File list
            Dim RefList As String()
            Dim i As Integer

            ' Get dropped files
            RefList = e.Data.GetData(DataFormats.FileDrop)

            ' Act on each file
            For i = 0 To RefList.Count - 1

                ' Get filepath
                Dim FilePath As String = CStr(RefList.GetValue(i))

                ' Validate file
                Dim ValidationResult As Integer = Settings.ValidateReference(FilePath, True, True)

                ' Skip files with errors
                If ValidationResult <> 0 Then
                    NoErrorsSoFar = NoErrorsSoFar AndAlso ValidationResult < 2 ' One way flag that changes iff. ValidationResult isn't 0 or 1
                    Continue For
                End If

                ' Get file extension
                Dim FileExt As String = FilePath.Substring(FilePath.LastIndexOf("."))

                ' If file is a Word document, add its words to the counting system and its filepath to the list box
                If DocFileExts.Contains(FileExt) Then
                    AddReference(FilePath)
                End If

                ' If file is a text file, attempt to add each of its filepaths
                If PlainTextFileExts.Contains(FileExt) Then

                    ' Error bolean
                    Dim R_NoErrorsSoFar As Boolean = True

                    ' Get file text
                    Dim FileRdr As New IO.StreamReader(FilePath)

                    ' Read each line
                    While Not FileRdr.EndOfStream

                        ' Get filepath
                        Dim R_FilePath As String = FileRdr.ReadLine

                        ' Validate file
                        Dim R_ValidationResult As Integer = Settings.ValidateReference(R_FilePath, R_NoErrorsSoFar, False)

                        ' Skip files with errors
                        If R_ValidationResult <> 0 Then
                            R_NoErrorsSoFar = R_NoErrorsSoFar AndAlso R_ValidationResult < 2 ' One way flag that changes iff. ValidationResult isn't 0 or 1
                            Continue For
                        End If

                        ' Add the file (that by this point must be a Word document - validateResults() checks for recursive referencing) to the reference list
                        AddReference(R_FilePath)

                    End While

                End If

            Next

        End If
    End Sub

    ' Drag-drop for references (modified from a Microsoft example HERE: http://msdn.microsoft.com/en-us/library/aa289508%28v=vs.71%29.aspx#vbtchimpdragdropanchor6)
    Private Sub files_DragPtA(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lbox_files.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub

    ' Add a reference
    Shared Sub AddReference(ByVal FilePath As String)

        ' Get text from Word document (make sure document is read-only)
        For i = 1 To 5
            Try
                WordAppBrowser.Documents.Open(FilePath, [ReadOnly]:=True)
                WordAppBrowser.Visible = False
                Exit For
            Catch ex As Exception
                If ex.Message.Contains("The RPC server is unavailable") Then
                    Sleep(100)
                End If
            End Try
        Next

        ' Error checking
        Try
            If WordAppBrowser.ActiveDocument.Equals(Nothing) Then
                MsgBox("ContexType ERROR: AddReference: Error in scanning referenced document. Document has not been referenced.")
                Exit Sub
            End If
        Catch
            MsgBox("ContexType ERROR: AddReference: Error in scanning referenced document. Document has not been referenced.")
            Exit Sub
        End Try

        ' Add shortened file path to the list box
        Dim ShortPath As String = FilePath
        While ShortPath.Length > 150 And (ShortPath.Contains("/") Or ShortPath.Contains("\"))
            ShortPath = ShortPath.Substring(Math.Max(ShortPath.IndexOf("/"), ShortPath.IndexOf("\")) + 1)
        End While
        Form1.lbox_files.Items.Add(ShortPath)

        ' Add full file path to entire path file list
        Form1.EntirePathFileList.Add(FilePath)

        Dim FileText As String = WordAppBrowser.ActiveDocument.Content.Text

        ' Format text
        FileText = FileText.Replace("(", "").Replace(")", "").Replace(Chr(34), "").Replace("[", "").Replace("]", "").
                   Replace("?", "").Replace("!", "").Replace(".", "").Replace("/", " ").Replace("-", " ").
                   Replace(":", " ").Replace(";", " ").Replace(",", "").Replace(ControlChars.Tab, " ")

        ' Word/count arrays
        Dim FileRecs As New List(Of Recommendation)

        ' Get word/count data
        StringManipulation.GetWordData(FileText, FileRecs, New Integer, True)

        ' Get Trie data
        FileRecs.Sort(New RecSortString)
        Dim ReferenceTrie As NamedCountedList = Trie.CreateTrie(FileRecs, Form1.TrieDepth)
        ReferenceTrie.Name = FilePath
        Form1.ReferenceTries.Add(ReferenceTrie)

        ' Close connection with word document
        For Each D As Word.Document In WordAppBrowser.Documents
            D.Close()
        Next

    End Sub

#End Region

#Region "Continuous/normal document scanning"

    ' Update hopper
    Public Sub HopperWorker_DoWork() Handles HopperWorker.DoWork
        While True

            Try

                ' Reduce processor load
                Sleep(30)

                ' Don't update if recommendations list is empty
                If SortedRecommendations.Count = 0 Then
                    HopperWorker.ReportProgress(0)
                End If

                ' Trigger recommendation list update
                HopperWorker.ReportProgress(1)

            Catch
            End Try

        End While
    End Sub

    ' Variables used in document scanning process
    Public RecsNew As New List(Of Recommendation)

    '   Main document trie sorting variables
    Public TextWorkerFreeze As Boolean = False
    Public MainDocTrie As NamedCountedList

    '   Main document cumulative sorting variables
    Public MainDocCumulativeList As New List(Of String)
    Public MainDocCumulativeActiveWord As String = "  "
    Public MainDocCumulativeIdxList As New List(Of List(Of Integer))

    ' Analyzes documents and gets recommendation list
    Public Sub TextWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles TextWorker.DoWork

        ' This block contains the instructions for the TextWorker BackgroundWorker object
        ' This object continuously monitors the text of the active document and updates the list of active words

        ' Previous word text
        'WordText = ""
        'WordTextPrev = ""

        ' Previous word
        Dim WordPast As String = ""

        While True

            Try

                ' If document isn't active, loop again
                Dim SZ As String = GetActiveWindowTitle(TitleLength)
                If Not GetActiveWindowTitle(TitleLength).Contains("Microsoft Word") Then
                    Throttle(False)
                    Continue While
                End If

                ' If TextWorker is frozen (to prevent it from modifying word lists), loop again
                If TextWorkerFreeze Then
                    Continue While
                End If

                ' If document is closed, loop again
                Try
                    WordDoc = WordApp.ActiveDocument
                Catch ex As Exception

                    ' Known bug with no known (to author) fix - band aid warning to restart CType (which usually fixes things)
                    '   NOTE: Auto-restart was removed because it never worked.
                    If ex.Message.Contains("The RPC server is unavailable.") Then
                        MsgBox("A minor error has occurred within the ContexType system (RPC server error) that requires that ContexType restart. ContexType will now exit.")

                        ' Launch another ContexType window
                        'Shell(Process.GetCurrentProcess.MainModule.FileName)

                        ' Close the active one
                        Me.Close()

                    End If

                    Continue While
                End Try

                ' Freeze text
                FreezeActiveText = True

                ' Get text
                Dim WordTextSpaces As String
                Try
                    WordTextSpaces = WordDoc.Content.Text '.Replace(vbCr, " ")
                Catch
                    Throttle(False)
                    Continue While
                End Try

                ' Remove punctuation
                WordTextSpaces = StringManipulation.RemovePunctuation(WordTextSpaces)
                If O_IgnoreCase Then
                    WordTextSpaces = WordTextSpaces.ToLower
                End If
                WordText = WordTextSpaces.Replace("  ", " ").Replace("  ", " ")

                ' Make sure WordText isn't nil (first go)
                If String.IsNullOrWhiteSpace(WordText) Then
                    Throttle(True)
                    Continue While
                End If

                ' Case-insensitive option
                If O_IgnoreCase Then
                    WordText = WordText.ToLower
                End If

                ' If no edit happened, go around
                If WordTextPrev.Length = WordText.Length Then ' Using .length doesn't exactly check for equality, but it is MUCH faster and accurate enough for our purposes
                    WordPast = WordCurrent
                    Throttle(True)
                    Continue While
                End If

                ' Get changes from main lists
                Dim WordTextCopy As String = WordText
                Dim WordTextPrevCopy As String = WordTextPrev

                ' Get new data
                RecsNew = New List(Of Recommendation)
                Dim TotalWordsNew, TotalWordsOld As Integer

                Dim WordListNoCounting As String() = StringManipulation.GetWordData(WordTextCopy, RecsNew, TotalWordsNew, False, False)

                ' Exception handler - If new data is nil, continue while
                If RecsNew.Count = 0 OrElse (RecsNew.Count = 1 AndAlso String.IsNullOrEmpty(RecsNew.Item(0).Text)) Then
                    Continue While
                End If

                ' Deactivate the hopper if it is active to prevent bugs with it not displaying properly
                If GetActiveWindowTitle(Hopper.Text.Length) = Hopper.Text Then
                    WordApp.Activate()
                End If

                ' If the total number of words has decreased, a word has just been entirely deleted
                '   As such, hide the recommendations
                If TotalWordsOld > TotalWordsNew Then

                    Hopper.Visible = False
                    TotalWordsOld = TotalWordsNew
                    WordTextPrev = WordText
                    WordCurrent = " "

                    If O_MDSMethodIdx <> 2 Then
                        SortedRecommendations.Clear()
                    End If

                    TextWorker.ReportProgress(0)    ' Trigger an outside-of-worker update

                    ' Update data
                    TotalWordsOld = TotalWordsNew

                    RecsOld.Clear()
                    RecsOld.AddRange(RecsNew)

                    Continue While

                End If

                ' If this is the first document edit, go around
                If String.IsNullOrWhiteSpace(WordTextPrev) Then
                    WordTextPrev = ""
                End If

                ' Hold on to old WordCurrent for future comparison
                Dim WordOld As String = ""
                If Not String.IsNullOrWhiteSpace(WordCurrent) Then
                    WordOld = WordCurrent
                End If

                ' Remove first null word from WordsNew (if applicable)
                If RecsNew.Item(0).Text.Length = 0 Then
                    RecsNew.RemoveAt(0)
                End If

                ' Find changes (add new words)
                Try

                    Dim ListOffset As Integer = 0 ' Amount the new list has been offset compared to the old
                    For i = 0 To RecsNew.Count - 1

                        ' Get current word
                        Dim ArrWord As String = RecsNew.Item(i).Text

                        ' Get corresponding index in "old" lists
                        Dim IndexPartner As Integer = -1

                        ' Check to see if the word hasn't moved much between the old and new lists
                        '   This avoids conducting lengthy searches of WordsOld for words that haven't moved/moved slightly
                        If RecsOld.Count <> 0 Then

                            ' Conduct preliminary search with the offset that worked last time
                            '   This prevents having to scan multiple items if the offset hasn't changed
                            Dim ILO As Integer = i + ListOffset
                            If ILO < 0 And ILO > RecsOld.Count - 1 Then
                                If RecsOld.Item(ILO).Text = ArrWord Then
                                    IndexPartner = ILO
                                End If
                            End If


                            For j = -1 To 1

                                ' Exception handler
                                ILO = i + j
                                If ILO < 0 Or ILO > RecsOld.Count - 1 Then
                                    Continue For
                                End If

                                If RecsOld.Item(ILO).Text = ArrWord Then
                                    ListOffset = j
                                    IndexPartner = ILO
                                    Exit For
                                End If

                            Next

                            ' If the word isn't close by, search for it exhaustively
                            If IndexPartner = -1 Then
                                For j = 0 To RecsOld.Count - 1
                                    If RecsOld.Item(i).Text = ArrWord Then
                                        IndexPartner = j
                                    End If
                                Next
                            End If

                        End If

                        ' Get current word's count (in the other array)
                        Dim ArrCountOther As Integer
                        If IndexPartner = -1 Then
                            ArrCountOther = 0   ' Word isn't known to other (old) list
                        Else
                            ArrCountOther = RecsOld.Item(IndexPartner).Number
                        End If

                        ' Deduct as many of ArrCountOther as can fit from the current count
                        Dim ArrCountDelta As Integer = RecsNew.Item(i).Number - ArrCountOther ' --> THIS IS THE CHANGE IN TOTAL COUNT

                        If ArrCountDelta = 1 And WordTextPrev.Length <> 0 Then
                            WordCurrent = ArrWord ' If the current ArrWord is the one last changed, mark it as the current word
                        ElseIf ArrCountDelta = -1 And Not WordCurrent.StartsWith(ArrWord) Then

                            If TotalWordsNew < TotalWordsOld Then

                                ' If the current word is 1 character and its count has been reduced by 1, hide the recommendations hopper
                                '   This is so that if the user backspaces the first character of the current word (and thus removes the word from the document),
                                '   the recommendations hopper will be hidden
                                Hopper.Visible = False
                                WordTextPrev = WordText

                                If O_MDSMethodIdx <> 2 Then
                                    SortedRecommendations.Clear()
                                End If

                                TextWorker.ReportProgress(0)    ' Trigger an outside-of-worker update

                                ' Update data
                                TotalWordsOld = TotalWordsNew
                                RecsOld.Clear()

                                RecsOld.AddRange(RecsNew)

                                Continue While

                            End If

                        End If

                    Next

                Catch ex As Exception
                End Try

                ' Autotype toggle (only the starting of a new word causes it to change)
                If O_AutoType Then
                    Try
                        If StringManipulation.GetWordSimilarity(WordCurrent, WordPast) = 0 Then

                            ' If current word is different then past, enable autotype
                            AutoTypeCoolingDown = False

                        ElseIf WordCurrent.Length < WordPast.Length Then

                            ' If current word is shorter than past one, disable autotype
                            AutoTypeCoolingDown = True

                        End If
                    Catch
                    End Try
                End If

                ' If current text change is the first for the document, don't analyze it
                If WordTextPrev = "" Then

                    WordTextPrev = WordText
                    WordPast = WordCurrent

                    TotalWordsOld = TotalWordsNew

                    RecsOld.AddRange(RecsNew)


                    Continue While

                End If

                ' Unfreeze text
                FreezeActiveText = False

                ' ------------------------------- At this point, the main word lists are fully updated! -------------------------------
                ' If current word is nil, go around
                If String.IsNullOrWhiteSpace(WordCurrent) Then
                    WordTextPrev = WordText
                    TotalWordsOld = TotalWordsNew

                    ' Update data
                    TotalWordsOld = TotalWordsNew
                    RecsOld.Clear()


                    RecsOld.AddRange(RecsNew)


                    Continue While
                End If

                ' Find list of recommended words
                Dim RecommendationsUnsorted As New List(Of Recommendation)

                If O_MDSMethodIdx = 0 Then ' Normal main document search

                    ' Search known words for recommendations (current document)
                    For i = 0 To RecsNew.Count - 1

                        ' Get current word
                        Dim KnownWord As String = RecsNew.Item(i).Text

                        ' If current word fits the known one, add it to the recommendations list
                        If KnownWord.StartsWith(WordCurrent) And KnownWord.Length >= MinLength And RecsNew.Item(i).Number >= MinCnt Then
                            If KnownWord.Replace(" ", "").Length <> WordCurrent.Length Then

                                ' If necessary, removed already typed component from word suggestion
                                If O_EntireWord Then
                                    RecommendationsUnsorted.Add(New Recommendation(KnownWord.Replace(" ", ""), RecsNew.Item(i).Number))
                                Else
                                    RecommendationsUnsorted.Add(New Recommendation(KnownWord.Substring(WordCurrent.Length).Replace(" ", ""), RecsNew.Item(i).Number))
                                End If

                            End If
                        End If
                    Next

                ElseIf O_MDSMethodIdx = 1 Then

                    ' Use trie search
                    RecommendationsUnsorted = Trie.SearchTrie(MainDocTrie, WordCurrent, MinCnt, MinLength, O_IgnoreCase)

                Else

                    MsgBox("Cumulative search is commented out because it doesn't work!")

                    '' Use cumulative sorting

                    '' If current word is only one character long and the first character <> the cumulative's first, do some updating/data recording
                    'If WordCurrent.Length > 0 Then

                    '    If MainDocCumulativeActiveWord.First <> WordCurrent.First Or String.IsNullOrWhiteSpace(MainDocCumulativeActiveWord) Then

                    '        ' Update stored cumulative word
                    '        MainDocCumulativeActiveWord = WordCurrent

                    '        ' Clear the index list
                    '        MainDocCumulativeIdxList.Clear()

                    '        ' --- Copied from normal sorting ---
                    '        ' Search known words for recommendations (current document)
                    '        For i = 0 To WordsNew.Count - 1

                    '            ' Get current word
                    '            Dim KnownWord As String = WordsNew.Item(i)

                    '            ' If current word fits the known one, add it to the recommendations list
                    '            If KnownWord.StartsWith(WordCurrent) And KnownWord.Length >= MinLength And CountNew.Item(i) >= MinCnt Then
                    '                If KnownWord.Replace(" ", "").Length <> WordCurrent.Length Then

                    '                    ' Add entire recommendation (required)
                    '                    RecommendationsUnsorted.Add(New Recommendation(KnownWord.Replace(" ", ""), CountNew.Item(i)))

                    '                End If
                    '            End If
                    '        Next

                    '    Else

                    '        Try

                    '            ' Sort through old sorted recommendations and isolate valid ones
                    '            '   Note: this refers to items in the sorted recommendations list through their indexes

                    '            ' Remove entries from the end of the cumulative index list, if necessary
                    '            '   This is to make it have a length equal to( WordCurrent.Length-1)
                    '            While WordCurrent.Length - 1 <= MainDocCumulativeIdxList.Count And MainDocCumulativeIdxList.Count <> 0
                    '                MainDocCumulativeIdxList.RemoveAt(MainDocCumulativeIdxList.Count - 1)
                    '            End While

                    '            ' Populate the cumulative index list
                    '            While WordCurrent.Length - 1 > MainDocCumulativeIdxList.Count

                    '                Dim ValidRecs As New List(Of Integer)

                    '                Dim PartCurrent As String = WordCurrent.Substring(0, MainDocCumulativeIdxList.Count + 2)

                    '                ' Get valid suggestions
                    '                If MainDocCumulativeIdxList.Count = 0 Then

                    '                    ' First layer of numerically indexed recommendations
                    '                    For i = 0 To SortedRecommendations.Count - 1

                    '                        Dim CI As String = SortedRecommendations.Item(i)

                    '                        ' Check for equivalence
                    '                        If O_IgnoreCase Then
                    '                            CI = CI.ToLower
                    '                            If CI.StartsWith(PartCurrent.ToLower) Then
                    '                                ValidRecs.Add(i)
                    '                            End If
                    '                        Else
                    '                            If CI.StartsWith(PartCurrent) Then
                    '                                ValidRecs.Add(i)
                    '                            End If
                    '                        End If
                    '                    Next

                    '                    MainDocCumulativeIdxList.Add(ValidRecs)
                    '                Else

                    '                    ' Second and onward recommendation layers (that depend on previous index layers)
                    '                    ValidRecs = MainDocCumulativeIdxList.Item(MainDocCumulativeIdxList.Count - 1)
                    '                    Dim NextValidRecs As New List(Of Integer)

                    '                    ' Sort through previous recommendations and mark the valid ones
                    '                    For i = 0 To ValidRecs.Count - 1

                    '                        Dim CIdx As Integer = ValidRecs.Item(i)
                    '                        Dim CRec As String = SortedRecommendations.Item(CIdx)

                    '                        ' Check for equivalence
                    '                        If O_IgnoreCase Then
                    '                            CRec = CRec.ToLower
                    '                            If CRec.StartsWith(PartCurrent.ToLower) Then
                    '                                NextValidRecs.Add(i)
                    '                            End If
                    '                        Else
                    '                            If CRec.StartsWith(PartCurrent) Then
                    '                                NextValidRecs.Add(i)
                    '                            End If
                    '                        End If

                    '                    Next

                    '                    ' Add current recommendations list to main cumulative one
                    '                    If PartCurrent.Length - 2 > MainDocCumulativeIdxList.Count - 1 Then
                    '                        MainDocCumulativeIdxList.Add(NextValidRecs)
                    '                    Else
                    '                        MainDocCumulativeIdxList.Item(PartCurrent.Length - 2) = NextValidRecs
                    '                    End If

                    '                End If

                    '            End While

                    '            ' Recommendations are finished (if the current word is longer than 1 character)
                    '            If WordCurrent.Length > 1 Then

                    '                ' Report update
                    '                TextWorker.ReportProgress(1)

                    '                ' Update data
                    '                TotalWordsOld = TotalWordsNew
                    '                WordsOld.Clear()
                    '                

                    '                WordsOld.AddRange(WordsNew)
                    '                

                    '                ' Update stored cumulative word
                    '                MainDocCumulativeActiveWord = WordCurrent

                    '                Continue While
                    '            End If

                    '        Catch
                    '        End Try

                    '    End If
                    'End If

                End If

                ' Get complete word list (for word distance - only if necessary)
                Dim WordListIndices As New List(Of Integer)
                Dim CurWordIndex As Integer ' Index of current typing word in word list

                If O_S_Dist And Not String.IsNullOrWhiteSpace(WordCurrent) Then

                    ' Get current word index (index of word that is being modified)
                    '   This prevents words that are the same as the current one that occur earlier from being marked as changed
                    Dim CurStrIndex As Integer = WordApp.Selection.Range.Start

                    ' Find current word index
                    Dim SearchIndex As Integer = 0

                    ' This links the index of the item in WordsListNoCounting to the location in the main string
                    ' Number of target word skipped to reach destination
                    Dim SkipCount As Integer = 0
                    Dim MustExit As Boolean = False
                    While WordTextSpaces.IndexOf(WordCurrent, SearchIndex + WordCurrent.Length) <> -1

                        Try

                            ' Check if typing index is within current word
                            If CurStrIndex >= SearchIndex - 1 And CurStrIndex <= SearchIndex + WordCurrent.Length + 1 Then
                                CurWordIndex = SearchIndex ' Update current word index (bugfix on 10/6/2012)
                                Exit While
                            End If

                            ' Search for next occurrence of the given word
                            SearchIndex = WordTextSpaces.IndexOf(" " & WordCurrent & " ", SearchIndex + 1)

                            ' Add 1 to the current word count (number of occurences of the current word skipped to arrive at destination)
                            SkipCount += 1

                            ' If index is negative (i.e. word couldn't be found close enough to the active workspace in the document),
                            '   go around
                            If SearchIndex = -1 Then
                                WordTextPrev = WordText
                                TotalWordsOld = TotalWordsNew

                                ' --- Update old recommendations list ---
                                RecsOld.Clear()


                                RecsOld.AddRange(RecsNew)


                                MustExit = True

                                Exit While
                            End If

                        Catch
                        End Try

                        If MustExit Then
                            Exit While
                        End If

                    End While

                    ' Go around again
                    If MustExit Then
                        Continue While
                    End If

                    ' Band aid exception handler
                    If String.IsNullOrWhiteSpace(WordCurrent) Then

                        WordTextPrev = WordText
                        TotalWordsOld = TotalWordsNew

                        ' --- Update old recommendations list ---
                        RecsOld.Clear()


                        RecsOld.AddRange(RecsNew)


                        Continue While

                    End If

                    ' If skip count is 0, just use first word (because no occurrences of the word were skipped)
                    If SkipCount = 0 Then
                        CurWordIndex = Array.IndexOf(WordListNoCounting, WordCurrent)
                    Else

                        ' Otherwise, use skip counting procedure
                        '   Convert skip count into actual word count (if skip count isn't 0)
                        SearchIndex = WordTextSpaces.IndexOf(WordCurrent)
                        For i = 0 To WordListNoCounting.Count - 1

                            ' If current word is a match, subtract from skip count
                            If CStr(WordListNoCounting.GetValue(i)) = WordCurrent Then
                                SkipCount -= 1

                                ' Exit loop
                                If SkipCount = 0 Then
                                    CurWordIndex = i
                                    Exit For
                                End If

                            End If

                        Next
                    End If

                    ' If CurWordIndex is negative, continue the loop
                    If CurWordIndex < 0 Or CurWordIndex > WordListNoCounting.Count - 1 Then
                        WordTextPrev = WordText
                        TotalWordsOld = TotalWordsNew

                        ' --- Update old recommendations list ---
                        RecsOld.Clear()


                        RecsOld.AddRange(RecsNew)


                        Continue While

                    End If

                End If

                ' ------------------------------ Reference document recommendations ------------------------------

                ' Get combined counts of reference words across all reference documents
                Dim RecsUnsortedRef As New List(Of Recommendation)
                Dim CountsUnsortedRef As New List(Of Double)

                If ReferenceTries.Count > 0 Then

                    ' Parse through the reference documents (each of which is stored in trie format)
                    For i = 0 To ReferenceTries.Count - 1

                        ' Recursively identify any matching tries
                        RecsUnsortedRef.AddRange(Trie.SearchTrie(ReferenceTries.Item(i), WordCurrent, MinCnt, MinLength, O_IgnoreCase))

                    Next

                    ' Create/populate the unsorted counts list
                    If O_S_Length Then
                        For i = 0 To RecsUnsortedRef.Count - 1
                            CountsUnsortedRef.Add(RecsUnsortedRef.Item(i).Text.Length)
                        Next
                    Else
                        For i = 0 To RecsUnsortedRef.Count - 1
                            CountsUnsortedRef.Add(RecsUnsortedRef.Item(i).Number)
                        Next
                    End If

                End If

                ' ------------------- Total recommendations (current and reference documents) -------------------

                ' If recommendations are sorted based on population, combine the current and reference to-be-sorted lists
                If O_S_Popln And ReferenceTries.Count > 0 Then

                    ' Get the words in recommendation format
                    Dim AllRecs As New List(Of Recommendation)
                    AllRecs.AddRange(RecommendationsUnsorted)

                    If O_EntireWord Then
                        For Each R As Recommendation In RecsUnsortedRef
                            AllRecs.Add(New Recommendation(WordCurrent & R.Text, R.Number))
                        Next
                    Else
                        AllRecs.AddRange(RecsUnsortedRef)
                    End If


                    ' Sort based on recommendation string
                    Dim Comparer2 As New RecSortString
                    AllRecs.Sort(Comparer2)

                    ' Add recommendation populations together
                    Dim Index As Integer = 0
                    Dim RemoveIndex As Integer = -1

                    While True

                        ' Account for repeats
                        For i = Index To AllRecs.Count - 1

                            ' Check for repeats
                            For j = i + 1 To AllRecs.Count - 1
                                If AllRecs.Item(i).Text = AllRecs.Item(j).Text Then
                                    AllRecs.Item(i).Number += AllRecs.Item(j).Number
                                    RemoveIndex = j
                                    Exit For
                                End If
                            Next

                            ' If a repeat has occurred, pause searching for repeats
                            If RemoveIndex <> -1 Then
                                Exit For
                            End If

                        Next

                        ' If a repeat has occurred, remove it from AllRecs
                        If RemoveIndex <> -1 Then
                            If RemoveIndex <> 0 Then
                                Index = RemoveIndex - 1
                            Else
                                Index = 0
                            End If
                            AllRecs.RemoveAt(RemoveIndex)
                            RemoveIndex = -1
                        Else
                            ' If no repeats have occurred, this process is complete
                            Exit While
                        End If

                    End While

                    ' Update main recommendations list (RecommendationsUnsorted)
                    RecommendationsUnsorted.Clear()
                    RecommendationsUnsorted.AddRange(AllRecs)

                End If

                ' Sort them according to the specified parameters (commonality, length, distance to reoccurence) - each is assigned an index of (count + length of word)

                ' Current document recommendations
                If Not O_S_None Then
                    GradeRecs(RecommendationsUnsorted, CurWordIndex, WordListNoCounting.ToList)
                    If Not O_S_Popln And ReferenceTries.Count > 0 Then ' If population mode is on, the recommendations from the references are included in the main recommendations list
                        GradeRecs(RecsUnsortedRef, CurWordIndex, WordListNoCounting.ToList)
                    End If

                    ' Sort unsorted recommendations lists
                    Dim Comparer As New RecSort
                    RecommendationsUnsorted.Sort(Comparer)
                    RecsUnsortedRef.Sort(Comparer)

                End If

                SortedRecommendations.Clear()
                For Each R As Recommendation In RecommendationsUnsorted

                    ' If recommendations exceed maximum recommendation count, don't bother adding more
                    If SortedRecommendations.Count >= IdeaCountLimit And IdeaCountLimit > 0 Then
                        Exit For
                    End If

                    ' Add recommendation if allowed
                    SortedRecommendations.Add(R.Text)

                Next

                For Each R As Recommendation In RecsUnsortedRef

                    ' Check for repeats
                    Dim B As Boolean = True
                    For Each S As String In SortedRecommendations
                        If R.Text = S Then
                            B = False
                            Exit For
                        End If
                    Next

                    ' If recommendations exceed maximum recommendation count, don't bother adding more
                    If SortedRecommendations.Count >= IdeaCountLimit And IdeaCountLimit > 0 Then
                        Exit For
                    End If

                    ' Add recommendation if it isn't a repeat
                    If B Then
                        SortedRecommendations.Add(R.Text)
                    End If

                Next

                ' If records are frozen, don't update them
                '   Note: FreezeRecs is supposed to temporarily delay recommendations; this is why this is so far along in the sequence
                '         (An instantaneous go-around would defeat its purpose)
                If FreezeRecs Then
                    FreezeRecs = False

                    ' --- Update old recommendations list ---
                    RecsOld.Clear()


                    RecsOld.AddRange(RecsNew)


                    Continue While

                End If

                ' ---------------------- At this point, recommendations have been obtained ----------------------

                ' If current word equals only recommended word, skip the recommendation
                If SortedRecommendations.Count = 1 And WordCurrent <> "" Then
                    If WordCurrent = SortedRecommendations.Item(0) Then
                        WordCurrent = ""
                    End If
                End If

                ' Update previous word text
                WordTextPrev = WordText
                WordPast = WordCurrent

                ' If append spaces checkbox is checked, append spaces
                If O_TypeSpace Then
                    For i = 0 To SortedRecommendations.Count - 1
                        SortedRecommendations.Item(i) = SortedRecommendations.Item(i) & " "
                    Next
                End If

                ' Remove words with common beginnings based on their length
                'TO ADD

                ' If a maximum number of suggestions has been specified, remove all but those suggestions
                'TO ADD

                ' --- Update old recommendations list ---
                RecsOld.Clear()


                RecsOld.AddRange(RecsNew)


                ' Clear memory
                RecsNew.Clear()

                ' Reverse recommendations (if appropriate)
                If O_Reverse Then
                    SortedRecommendations.Reverse()
                End If

                ' Update cumulative recommendations if applicable
                If WordCurrent.Length = 1 And WordCurrent <> MainDocCumulativeActiveWord.FirstOrDefault Then
                    MainDocCumulativeList = SortedRecommendations
                ElseIf MainDocCumulativeActiveWord.Length = 0 Then
                    MainDocCumulativeList = SortedRecommendations
                End If

                ' Report back progress (in this case, use 1 because the suggestions were updated)
                TextWorker.ReportProgress(1)

                TotalWordsOld = TotalWordsNew

            Catch ex As Exception
            End Try

        End While

    End Sub

    ' Text worker helper (clean up repeated parts)
    Public Sub GradeRecs(ByRef Recs As List(Of Recommendation), ByVal CurWordIndex As Integer, ByVal WordListNoCounting As List(Of String))
        For i = 0 To Recs.Count - 1

            ' Get current [recommended] word and its data
            Dim RecCurrent As String = Recs.Item(i).Text
            Dim RecStrength As Double = 0

            ' Get recommendation strength
            If O_S_Length Then
                RecStrength = RecCurrent.Length * 1.001
            ElseIf O_S_Popln Then
                RecStrength = Recs.Item(i).Number + RecCurrent.Length / 1000
            Else
                ' NOTE: If WordCurrent already exists as its own word in the document, this BREAKS!
                RecStrength = WordListNoCounting.Count - StringManipulation.GetWordDistance(CurWordIndex, WordCurrent & RecCurrent, WordListNoCounting) ' Invert the distance between the current word and the target word (so that closer = more ideal = LARGER value)
            End If

            ' Assign current word an index
            Recs.Item(i).Number = RecStrength

        Next
    End Sub

    ' Creates tries for main document
    Private Sub MainDocTrieWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles MainDocTrieWorker.DoWork

        While True

            Try

                If O_MDSMethodIdx = 1 Then

                    ' Copy main document data list (because it can change during TextWorker's operation) -
                    Dim WordsListSortedFreeze As New List(Of Recommendation)
                    TextWorkerFreeze = True

                    WordsListSortedFreeze.AddRange(RecsOld)

                    ' Sort main document data
                    WordsListSortedFreeze.Sort(New RecSortString)

                    ' Create trie
                    MainDocTrie = Trie.CreateTrie(WordsListSortedFreeze, TrieDepth)
                    TextWorkerFreeze = False

                    ' Wait proper time interval
                    Sleep(1000 * O_MDSTrieSrcInterval)

                End If

            Catch ex As Exception
                Dim Q = 1
            End Try

        End While

    End Sub

#End Region

#Region "Hints"

    ' Referencing operations hints
    Public Sub H_Main(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.MouseEnter
        txt_hints.Text = "Mouse over something to learn more about it."
    End Sub
    Public Sub H_LboxFiles(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbox_files.MouseEnter
        txt_hints.Text = "This box contains the list of imported reference files. Drag and drop a file on top of this box to reference the file. Click on a file in the box to select it."
    End Sub

    Public Sub H_BtnAddRef(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_AddRef.MouseEnter
        txt_hints.Text = "Click to browse for reference files using a file browsing dialog."
    End Sub
    Public Sub H_BtnRemoveRef(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_RemoveRef.MouseEnter
        txt_hints.Text = "Click to remove the selected reference(s). Use SHIFT and CTRL to select multiple files at the same time."
    End Sub
    Public Sub H_BtnUpdateRef(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_UpdateRef.MouseEnter
        txt_hints.Text = "Click to refresh the selected reference(s). Use SHIFT and CTRL to select multiple files at the same time."
    End Sub
    Public Sub H_BtnCopyRef(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_CopyRefs.MouseEnter
        txt_hints.Text = "Click to copy the file paths of the selected reference(s). Use SHIFT and CTRL to select multiple files at the same time. If nothing is selected, all file paths will be copied."
    End Sub

#End Region

#Region "Settings"
    ' Settings updates
    Private Sub txtMinAcc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MinAccuracy = CDbl(FormOptions.txtMinAcc.Text)
    End Sub

    Private Sub txtAutoPrc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        AutoPercentage = CDbl(FormOptions.txtAutoPrc.Text)
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If AboutBox1.Visible Then

        End If

    End Sub

    ' This worker keeps track of the amount of processor power that the ContexType .exe is consuming
    '   This data is then used to "throttle" the process
    Private Sub ThrottleWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles ThrottleWorker.DoWork

        While True

            Dim PTime = Process.GetCurrentProcess.TotalProcessorTime.TotalMilliseconds
            Sleep(25)
            CurProcPercentage = Math.Round((Process.GetCurrentProcess.TotalProcessorTime.TotalMilliseconds - PTime) / (0.4 * Environment.ProcessorCount)) ' [1]

        End While

    End Sub

    ' Throttling function
    Private Function Throttle(ByVal IsActive As Boolean) As Integer
        If IsActive Then
            While CurProcPercentage > TargetCPUUse
                Sleep(20)
            End While
        Else
            While CurProcPercentage > Math.Ceiling(TargetCPUUse / 3)
                Sleep(20)
            End While
        End If

        Return 0
    End Function

    Private Sub AboutToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem1.Click
        AboutBox1.Visible = Not AboutBox1.Visible
    End Sub

    Private Sub OptionsToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionsToolStripMenuItem1.Click
        FormOptions.SettingsChanged = False
        FormOptions.Show()
        Me.Hide()
    End Sub

    Private Sub KeyListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KeyListToolStripMenuItem.Click
        FormFAQ.Show()
        Me.Hide()
    End Sub

#End Region

    ' Obtains file extensions from filepaths
    Public Shared Function GetFileExt(ByVal FilePath As String) As String
        Return FilePath.Substring(Math.Max(FilePath.LastIndexOf("."), 0))
    End Function

End Class

' String manipulation
Public Class StringManipulation

    ' Inputs selected suggestions into active document
    Shared Sub SendText(ByVal Text As String, ByVal UseCopyPaste As Boolean)

        ' Copy paste method
        If UseCopyPaste Then

            ' Get initial clipboard
            Dim Clip = My.Computer.Clipboard.GetDataObject

            ' Send text
            My.Computer.Clipboard.SetText(Text)

            'MsgBox("Fix copy-pasting system!")
            SendKeys.Send("^v") ' TODO: Replace with WM_COPY message

            ' Revert initial clipboard
            My.Computer.Clipboard.SetDataObject(Clip)

            ' Return
            Return

        End If

        ' Normal method
        SendKeys.Send(Text)
        SendKeys.Flush()

        ' Return
        Return

    End Sub

    ' Get accuracy percentage of a recommendation list
    Shared Function GetAccuracyPercentage(ByVal WordCurrent As String, ByVal EntireRecommendations As List(Of String), ByVal AppendIdeaToWordCurrent As Boolean) As Integer

        ' Freeze entire recommendations list (to prevent simultaneous thread problems)
        Dim EntireRecommendationsFreeze As New List(Of String)
        EntireRecommendationsFreeze.AddRange(EntireRecommendations)

        If EntireRecommendationsFreeze.Count > 0 And WordCurrent.Length > 0 Then

            ' Get similarity of all recommendations to current word
            Dim CumulativeSimilarity As Double = 0
            Dim CumulativeCharacters As Double = 0
            For Each IdeaA As String In EntireRecommendationsFreeze

                ' Set up full word
                Dim Idea As String = IdeaA
                If AppendIdeaToWordCurrent Then
                    Idea = WordCurrent & Idea
                End If

                ' Check similarity
                If Not (String.IsNullOrWhiteSpace(Idea) Or String.IsNullOrWhiteSpace(WordCurrent)) Then
                    CumulativeSimilarity += StringManipulation.GetWordSimilarity(Idea, WordCurrent) * Idea.Length
                    CumulativeCharacters += Idea.Length
                End If

            Next

            ' Report similarity
            Return 100 * CumulativeSimilarity / CumulativeCharacters

        Else
            Return 0
        End If

    End Function

    ' Get number of common characters between two words 
    '   NOTE: this doesn't work well for words such as "listen" and "silent", which have the same letters
    Shared Function GetWordSimilarity(ByVal Str1 As String, ByVal Str2 As String) As Double

        ' Make sure strings are of valid length
        If Str1.Length = 0 Or Str2.Length = 0 Then
            Return 0
        End If

        ' Split strings into lists of characters
        Dim Chars1 As New List(Of String)
        For i = 0 To Str1.Length - 1
            Chars1.Add(Str1.Chars(i))
        Next
        Dim Chars2 As New List(Of String)
        For i = 0 To Str2.Length - 1
            Chars2.Add(Str2.Chars(i))
        Next

        ' Count how many characters are in common between the lists
        Dim CommonCnt As Integer = 0
        For Each Letter As String In Chars1

            ' Get index of letter in Chars2
            Dim Index As Integer = Chars2.IndexOf(Letter)

            ' If index is null, skip the letter
            If Index = -1 Then
                Continue For
            End If

            ' Annull letter in Chars2
            If Index > -1 Then
                Chars2.Item(Index) = ""
            End If

            ' Add to common count
            CommonCnt += 1
        Next

        ' Return # of common characters divided by # of characters in longest word
        Return CommonCnt / Math.Max(Str1.Length, Str2.Length)

    End Function

    ' Get words / word counts in a string
    Shared Function GetWordData(ByVal Haystack As String, ByRef UniqueRecList As List(Of Recommendation), ByRef TotalWordCount As Integer, Optional ByVal SortWordList As Boolean = False, Optional ByVal IgnoreOneLtrWords As Boolean = True) As String()

        ' Get word count for each word
        Dim UncountedString As String = Haystack.Replace(Chr(12), " ").Replace(vbCr, " ").Replace(vbLf, " ").Replace("  ", " ").Replace("  ", " ")

        ' Get raw words
        Dim WordsListAll As String() = UncountedString.Split(" ")
        TotalWordCount = WordsListAll.Count

        ' Sort raw words (to optimize processing)
        '   Note: using the indexOf function to search an array is O(n) and so is iterating through anothe array; therefore, the old approach was
        '   O(n*m) --> O(n^2)...using an O(n) sorting algorithm and then an O(n) iteration system is simply O(2n). (Thus, this theoretically is more efficient)

        ' Define a new and unsorted array of the words to be used later in the TextWorker process
        Dim WordsListAllNoSort As String() = WordsListAll.Clone

        Dim CaseStringSorter As New CaseStringSort

        Array.Sort(WordsListAll)
        Array.Sort(WordsListAll, CaseStringSorter)

        ' Iterate through raw words
        Dim WordPast As String = ""
        Dim WordCnt As Integer = 1

        For i = 0 To WordsListAll.Length - 1

            ' Get word
            Dim WordCur As String = WordsListAll.GetValue(i)

            ' If word is nil, skip it
            If String.IsNullOrWhiteSpace(WordCur) Or (WordCur.Length < 2 And IgnoreOneLtrWords) Then
                Continue For
            End If

            ' If word is the same as the previous one, add one to the word count
            If WordPast = WordCur Then
                WordCnt += 1
            Else

                ' Add past word to datalists
                UniqueRecList.Add(New Recommendation(WordPast, WordCnt))

                ' Reset the word count
                WordCnt = 1

            End If

            ' Update past word
            WordPast = WordCur

        Next

        ' Add very last word to datalists
        If UniqueRecList.Count <> 0 Then
            If CStr(WordsListAll.GetValue(WordsListAll.Count - 1)) = UniqueRecList.Last.Text Then

                ' Add 1 to count of last item
                UniqueRecList.Last.Number += 1

            Else
                ' Add last item
                UniqueRecList.Add(New Recommendation(WordsListAll.Last, WordCnt))

            End If
        Else
           ' Add last item
            UniqueRecList.Add(New Recommendation(WordsListAll.Last, WordCnt))

        End If

        ' Return the list of uncounted words (sorted or unsorted, depending on specified parameters)
        If SortWordList Then
            Return WordsListAll
        Else
            Return WordsListAllNoSort
        End If

    End Function

    ' Get word distance (returns -1 if a word doesn't exist)
    Shared Function GetWordDistance(IndexWord1 As Integer, NeedleWord As String, WordsListNoCounting As List(Of String)) As Integer

        ' Exception handler
        If WordsListNoCounting.Count = 0 Then
            Return 9999999
        End If

        ' Find closest instance of second word (backwards)
        Dim Index2 As Double = -1
        If IndexWord1 <> 0 Then
            For i = IndexWord1 - 1 To 0 Step -1

                ' If word is found, record it and exit loop
                If WordsListNoCounting.Item(i) = NeedleWord Then
                    Index2 = i
                    Exit For
                End If

            Next
        End If

        ' Find closest instance of second word (forwards)
        Dim Index3 As Double = -1
        If IndexWord1 <> WordsListNoCounting.Count - 1 Then
            For i = IndexWord1 + 1 To WordsListNoCounting.Count - 1

                ' If word is found, record it and exit loop
                If WordsListNoCounting.Item(i) = NeedleWord Then

                    ' If current word is closer to main index than past index, use current one
                    If Index3 = -1 Or Math.Abs(Index2 - IndexWord1) > Math.Abs(i - IndexWord1) Then
                        Index3 = i
                    End If
                    Exit For

                End If
            Next
        End If

        ' Return result
        If Index3 = -1 And Index2 = -1 Then
            Return WordsListNoCounting.Count + 1
        ElseIf Index3 = -1 Then ' Implies that Index2 != -1 because of above if statement; note: using "ElseIf Index2 != -1" would skip cases in which both Index2 and Index3 were != -1
            Return Math.Abs(IndexWord1 - Index2)
        ElseIf Index2 = -1 Then ' Once again, implies that Index3 != -1
            Return Math.Abs(Index3 - IndexWord1)
        Else
            Return Math.Min(Math.Abs(Index3 - IndexWord1), Math.Abs(IndexWord1 - Index2))
        End If

    End Function

    ' Remove starting spaces from a string
    Shared Function RemoveStarterSpaces(ByVal Str As String) As String
        Dim Index As Integer = 0
        While String.IsNullOrWhiteSpace(Str.Chars(Index))
            Index += 1
        End While
        Return Str.Substring(Index)
    End Function


    ' Remove punctuation (everything is replaced with a space because length must remain the same)
    Shared Function RemovePunctuation(ByVal StrIn As String) As String
        Return StrIn.Replace("(", " ").Replace(")", " ").Replace(Chr(34), " ").Replace("[", " ").Replace("]", " ").
            Replace("?", " ").Replace("!", " ").Replace(".", " ").Replace("/", " ").Replace("-", " ").Replace(":", " ").
            Replace(";", " ").Replace(",", " ").Replace(ControlChars.Tab, " ").Replace(Chr(&HAB), " ").
            Replace(Chr(&HBB), " ").Replace(Chr(11), " ").Replace(vbCr, " ").Replace(vbLf, " ") '.Replace(Chr(&H201C), "").Replace(Chr(&H201D), "")
    End Function

    ' Get changed text (forward-backward method)
    Shared Function GetChangedText(ByVal StrA As String, ByVal StrB As String) As String

        ' Create string copies
        Dim Str1 As String = StrA
        Dim Str2 As String = StrB

        ' Make sure longest string is #2
        If Str1.Length > Str2.Length Then
            Str1 = StrB
            Str2 = StrA
        End If

        ' Search forward (from start)
        Dim FwdIndex As Integer = 0
        While Str1.Chars(FwdIndex) = Str2.Chars(FwdIndex)
            FwdIndex += 1

            ' If the next FwdIndex is greater than the minimum string length, return the smaller string
            '   If this occurs, one string contains the other
            If FwdIndex > Str1.Length - 1 Then
                Return Str2.Replace(Str1, "")
            End If
        End While

        ' Update new string
        Str1 = Str1.Substring(FwdIndex)

        ' Search backward (from end)
        Dim RearIndex1 As Integer = Str1.Length - 2
        Dim RearIndex2 As Integer = Str2.Length - 2
        While Str1.Chars(RearIndex1) = Str2.Chars(RearIndex2)

            ' Subtract from indices
            RearIndex1 -= 1
            RearIndex2 -= 1

            ' If next rear indices are negative (out of bounds), exit string removal loop
            If RearIndex1 < 0 Or RearIndex2 < 0 Then
                Exit While
            End If

        End While

        ' Return final value
        Return Str2

    End Function

End Class

#Region "Various Sorting Methods"
' By case string sorting (e.x. A,a,B,b,C,c...)
Public Class CaseStringSort

    Implements IComparer(Of String)
    Public Function Compare(x As String, y As String) As Integer Implements IComparer(Of String).Compare

        ' Sort by character
        For i = 0 To Math.Min(x.Length, y.Length) - 1

            ' Find main index
            Dim Case1 As Integer
            If Char.IsUpper(x.Chars(i)) Then
                Case1 = 1
            Else
                Case1 = 0
            End If

            Dim Case2 As Integer
            If Char.IsUpper(y.Chars(i)) Then
                Case2 = 1
            Else
                Case2 = 0
            End If

            ' If there is a difference, sort the two terms
            If Case1 <> Case2 And x.Substring(0, i + 1).ToLower = y.Substring(0, i + 1).ToLower Then
                Return Case2 - Case1
            End If

        Next

        ' No sorting possible - return nil
        Return String.Compare(x, y)

    End Function

End Class

' Combined string-number storing class
Public Class Recommendation

    Dim Str As String = ""
    Dim Num As Double = 0

    Public Sub New(ByVal S As String, Optional ByVal N As Double = 0)

        Str = S
        Num = N

    End Sub

    Property Text As String
        Get
            Return Str
        End Get
        Set(v As String)
            Str = v
        End Set
    End Property

    Property Number As Double
        Get
            Return Num
        End Get
        Set(v As Double)
            Num = v
        End Set
    End Property

End Class

' Recommendation sorting [2], [3]
Public Class RecSort

    Implements IComparer(Of ContexType.Recommendation)
    Public Function Compare(x As ContexType.Recommendation, y As ContexType.Recommendation) As Integer Implements IComparer(Of ContexType.Recommendation).Compare

        Return Math.Sign(y.Number - x.Number)

    End Function

End Class
Public Class RecSortString

    Implements IComparer(Of ContexType.Recommendation)
    Public Function Compare(x As ContexType.Recommendation, y As ContexType.Recommendation) As Integer Implements IComparer(Of ContexType.Recommendation).Compare

        Return String.Compare(x.Text, y.Text)

    End Function

End Class
#End Region

' Settings handling
Public Class Settings

    ' Boolean to integer converter
    Shared Function BoolToInt(Bool As Boolean) As Integer
        If Bool Then
            Return 1
        End If

        Return 0

    End Function

    ' Integer to boolean converter
    Shared Function IntToBool(Int As Integer) As Integer
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

        ' Main document sorting method stuff
        FormOptions.rbnMDSNormal.Checked = Form1.O_MDSMethodIdx = 0
        FormOptions.rbnMDSTries.Checked = Form1.O_MDSMethodIdx = 1
        FormOptions.rbnMDSCumulative.Checked = Form1.O_MDSMethodIdx = 2
        FormOptions.txtTrieUpdateInterval.Text = CStr(Form1.O_MDSTrieSrcInterval)

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
        Form1.O_MDSMethodIdx = 0
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
                    If SLine.StartsWith("MainDocSortMtd=") Then
                        Form1.O_MDSMethodIdx = CInt(SValue)
                        FormOptions.rbnMDSNormal.Checked = Form1.O_MDSMethodIdx = 0
                        FormOptions.rbnMDSTries.Checked = Form1.O_MDSMethodIdx = 1
                        FormOptions.rbnMDSCumulative.Checked = Form1.O_MDSMethodIdx = 2
                        Continue While
                    ElseIf SLine.StartsWith("MainDocTriePrd=") Then
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
        StrList.Add("MainDocSortMtd=" & Form1.O_MDSMethodIdx)
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


#Region "Tries"
' Tries implementation [8], [9]
Public Class Trie

    ' Given word and count lists, create a set of appropriate tries
    Shared Function CreateTrie(ByVal RecsListSorted As List(Of Recommendation), ByVal TrieLength As Integer) As NamedCountedList

        ' Set up lists programmatically so they are of the proper depth
        Dim Trie As New NamedCountedList("", New List(Of Object))

        ' Create tries by parsing through the text
        '   Each intermediate trie contains its respective letter
        '   Each terminating trie contains the rest of the word along with the word's count

        ' Prep the last word tried recording list
        Dim LastSplitWord As New List(Of String)
        For i = 0 To TrieLength
            LastSplitWord.Add("")
        Next

        For i = 0 To RecsListSorted.Count - 1

            ' Get current word
            Dim CurWord As String = RecsListSorted.Item(i).Text

            ' Skip if current word is nil
            If String.IsNullOrWhiteSpace(CurWord) Then
                Continue For
            End If

            ' Split current word up into trie format
            Dim SplitWord As New List(Of String)
            For j = 0 To TrieLength - 1

                ' If current word is shorter than full trie distance, exit the splitting loop
                If j > CurWord.Length - 1 Then
                    Exit For
                End If

                ' Split the word further
                If j <> TrieLength - 1 Then
                    SplitWord.Add(CurWord.Chars(j))
                Else
                    SplitWord.Add(CurWord.Substring(j))
                End If

            Next

            ' Determine where to place splitted word in master trie list
            Dim StartingList As NamedCountedList = Trie
            For j = 0 To Math.Min(TrieLength, CurWord.Length) - 1

                ' Check equality
                If SplitWord.Item(j) <> LastSplitWord.Item(j) Then

                    ' The two lists aren't equal - add the current word into the trie (in its entirety)

                    ' --- Create trie addition ---
                    Dim TrieAddition As NamedCountedList = ListGen_Generate(SplitWord.Count - j)
                    Dim TrieAddRecursor As NamedCountedList
                    TrieAddRecursor = TrieAddition

                    ' Intermediate layers of trie addition
                    For k = j To SplitWord.Count - 2
                        TrieAddRecursor.Name = SplitWord.Item(k)
                        TrieAddRecursor = TrieAddRecursor.List.Item(0)
                    Next

                    ' Final layer of trie addition
                    TrieAddRecursor.Name = SplitWord.Item(SplitWord.Count - 1)
                    TrieAddRecursor.Count = RecsListSorted.Item(i).Number

                    ' --- Add trie addition to trie ---
                    StartingList.List.Add(TrieAddition)

                    ' --- Update last split word ---
                    LastSplitWord.Clear()
                    LastSplitWord.AddRange(SplitWord)

                    ' Make sure last split word list is of the proper minimum length (to prevent errors)
                    While LastSplitWord.Count < TrieLength
                        LastSplitWord.Add("")
                    End While

                    ' Continue with next word
                    Exit For

                Else
                    ' The two lists are equal - recurse into the next list
                    StartingList = StartingList.List.Item(StartingList.List.Count - 1)
                End If

            Next

        Next

        ' Return final Trie
        Return Trie

    End Function

    ' Recursive dynamic depth list generator - master function
    Shared Function ListGen_Generate(ByVal ListDepth As Integer) As NamedCountedList

        ' Define main list
        Dim List As New NamedCountedList("", New List(Of Object))

        ' Recurse
        ListGen_Recurse(List, ListDepth - 1)

        ' Return the final list
        Return List

    End Function

    ' Recursive dynamic depth list generator - recursor
    Shared Sub ListGen_Recurse(ByRef L2 As NamedCountedList, ByRef ListDepth As Integer)

        If ListDepth = 0 Then
            Return
        Else

            ' Add
            L2.List.Add(New NamedCountedList("", New List(Of Object)))

            ' Recurse
            ListGen_Recurse(L2.List.Item(0), ListDepth - 1)

        End If
    End Sub

    ' Recursively search a trie and return all its members that satisfy a given condition
    Shared Function SearchTrie(ByVal Trie As NamedCountedList, ByVal Needle As String, ByVal MinCnt As Integer, ByVal MinLength As Integer, ByVal IgnoreCase As Boolean) As List(Of Recommendation)

        ' List of search matches in the trie
        Dim Matches As New List(Of Recommendation)

        ' Conduct recursive search
        Try
            SearchTrie_Recursor(Trie, Needle, "", 0, Matches, MinCnt, MinLength, IgnoreCase)
        Catch
        End Try

        ' Return matches
        Return Matches

    End Function

    ' Trie search recursor
    Shared Sub SearchTrie_Recursor(ByRef CurTrie As NamedCountedList, ByVal Needle As String, ByVal CurWord As String, ByVal TrieLayer As Integer, ByRef Matches As List(Of Recommendation), ByVal MinCnt As Integer, ByVal MinLength As Integer, ByVal IgnoreCase As Boolean)

        ' If current base trie layer and needle aren

        ' STEPS: recurse(givenTrie as ?)
        '1: Add any tries (in givenTrie) that terminate to a list
        '2: Find valid subtries (before recursing?)
        '3: Recurse into all valid subtries

        ' If the current trie layer is final and matches the needle, add it to the matches list
        '   Ifs are nested for efficiency (to optimize processing power use)
        If CurTrie.Count >= MinCnt AndAlso CurWord.Length >= MinLength AndAlso (CurWord.Length > Needle.Length) AndAlso ( _
            (IgnoreCase AndAlso CurWord.ToLower.StartsWith(Needle.ToLower)) OrElse _
            (CurWord.StartsWith(Needle))) _
        Then

            Try

                ' Add the recommendation to the matches list
                If Form1.O_EntireWord Then
                    Matches.Add(New Recommendation(CurWord, CurTrie.Count))
                Else
                    Matches.Add(New Recommendation(CurWord.Remove(0, Needle.Length), CurTrie.Count))
                End If

            Catch
            End Try

            Return

        End If

        ' -- Variables used in the recursion-calling loop --
        ' Get current needle character
        'Dim CurNeedleChar As Char = Needle.Chars(TrieLayer)
        'If IgnoreCase Then
        '    CurNeedleChar = Char.ToLowerInvariant(CurNeedleChar)
        'End If

        ' Get next trie layer

        ' Current needle character
        Dim CurNeedleChar As String = ""
        If TrieLayer < Needle.Length Then
            If IgnoreCase Then
                CurNeedleChar = Char.ToLowerInvariant(Needle.Chars(TrieLayer))
            Else
                CurNeedleChar = Needle.Chars(TrieLayer)
            End If
        End If

        ' -- Recurse into any matching trie layers --
        For i = 0 To CurTrie.List.Count - 1

            Dim CurTrieLayer As NamedCountedList = CurTrie.List.Item(i)
            Try

                ' Name of next trie to be recursively parsed
                Dim CurTrieName As String = CurTrieLayer.Name
                Dim NextName As String = ""
                If CurTrie.Count = 0 OrElse CurTrie.List.Count <> 0 Then
                    NextName = CurTrieLayer.Name
                End If

                ' Complete recursion
                If TrieLayer < Needle.Length AndAlso CurTrieName.Length > 0 Then
                    If IgnoreCase AndAlso Char.ToLowerInvariant(CurTrieName.Chars(0)) = CurNeedleChar Then
                        SearchTrie_Recursor(CurTrieLayer, Needle, CurWord & NextName, TrieLayer + 1, Matches, MinCnt, MinLength, IgnoreCase)
                    ElseIf CurTrieName.Chars(0) = CurNeedleChar Then
                        SearchTrie_Recursor(CurTrieLayer, Needle, CurWord & NextName, TrieLayer + 1, Matches, MinCnt, MinLength, IgnoreCase)
                    End If
                Else
                    SearchTrie_Recursor(CurTrie.List.Item(i), Needle, CurWord & NextName, TrieLayer + 1, Matches, MinCnt, MinLength, IgnoreCase)
                End If

            Catch
            End Try

        Next

        ' Return
        Return

    End Sub


End Class

' Used in tries
Public Class NamedCountedList

    Dim StrName As String
    Dim LstList As List(Of Object)
    Dim CountInt As Integer = 0

    Public Sub New(Name As String, List As List(Of Object))

        StrName = Name
        LstList = List

    End Sub

    Public Property Name As String
        Set(value As String)
            StrName = value
        End Set
        Get
            Return StrName
        End Get
    End Property

    Public Property List As List(Of Object)
        Set(value As List(Of Object))
            LstList = value
        End Set
        Get
            Return LstList
        End Get
    End Property

    Public Property Count As Integer
        Set(value As Integer)
            CountInt = value
        End Set
        Get
            Return CountInt
        End Get
    End Property

End Class
#End Region

' Update handling
Public Class Updates

    ' Check for update by comparing the version of this file to the Google Code one
    '   Reads/downloads files using the technique described in [11]
    Shared Function CheckForUpdate(VersionAddress As String) As Integer ' -1=failed, 0=no update, 1=update

        ' Get latest version
        Dim LatestVersion As Integer
        Try
            Dim WebClient As New WebClient
            LatestVersion = CInt(WebClient.DownloadString(VersionAddress))
        Catch
            Return -1
        End Try

        ' Compare the two, if the downloaded one is larger, prompt for download
        If LatestVersion > Form1.Version Then
            Return 1
        Else
            Return 0
        End If

    End Function

    ' Execute updating process
    Shared Sub ExecuteUpdate()

        ' Get current ContexType path
        Dim EPath As String = Process.GetCurrentProcess.MainModule.FileName
        EPath = EPath.Substring(0, Math.Max(EPath.LastIndexOf("\"), EPath.LastIndexOf("/")) + 1)

        ' Download required files (new ContexType ones)
        Dim Q = Form1.ReleaseURL
        Dim WebClient As New WebClient
        Try
            WebClient.DownloadFile(Form1.ReleaseURL & "ContexType.exe", EPath & "ContexType_New.exe")
            WebClient.DownloadFile(Form1.ReleaseURL & "ContexType.pdb", EPath & "ContexType_New.pdb")
            WebClient.DownloadFile(Form1.ReleaseURL & "ContexType.xml", EPath & "ContexType_New.xml")

            ' Updater script
            Dim HelperPath As String = EPath & "ctype_update.bat"

            ' Write updater script
            Dim HelperFile As New List(Of String)
            HelperFile.Add("cd " & EPath)
            HelperFile.Add("set COPYCMD=/Y")
            HelperFile.Add("waitfor /t 20")
            HelperFile.Add("echo Updating...")
            HelperFile.Add("copy ContexType_New.exe ContexType.exe /Y")
            HelperFile.Add("copy ContexType_New.pdb ContexType.pdb /Y")
            HelperFile.Add("copy ContexType_New.xml ContexType.xml /Y")
            HelperFile.Add("del ContexType_New.exe /Q")
            HelperFile.Add("del ContexType_New.pdb /Q")
            HelperFile.Add("del ContexType_New.xml /Q")
            HelperFile.Add("echo Update complete. Restarting...")
            HelperFile.Add("run ContexType.exe")

            ' Create updater script file
            IO.File.WriteAllLines(HelperPath, HelperFile)

            ' Run updater script
            Try
                Shell(HelperPath)
            Catch
                ' Throw an exception so the next catch (the one that watches for failures) is triggered
                Throw New IO.FileNotFoundException
            End Try

        Catch
            MsgBox("Updating process failed. The update has not been installed.")
            Try
                IO.File.Delete(EPath & "ContexType_New.exe")
                IO.File.Delete(EPath & "ContexType_New.pdb")
                IO.File.Delete(EPath & "ContexType_New.xml")
                Exit Sub
            Catch
            End Try
        End Try

        ' Close down ContexType (so the files can be updated)
        Form1.Close()

    End Sub

End Class
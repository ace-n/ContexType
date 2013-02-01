' Update handling
Public Class Updates

    ' Check for update by comparing the version of this file to the Google Code one
    '   Reads/downloads files using the technique described in [11]
    Shared Function CheckForUpdate(ByVal VersionAddress As String) As Integer ' -1=failed, 0=no update, 1=update

        ' Get latest version
        Dim LatestVersion As Integer
        Try
            Dim WebClient As New Net.WebClient
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
        Dim WebClient As New Net.WebClient
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
Public Class FormFAQ

    ' Show-me button
    Public WithEvents ShowMeBtn As MenuItem
    Public Sub ShowMe() Handles ShowMeBtn.Click

        ' Hide other stuff
        FormOptions.Hide()
        Form1.Hide()
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

    Private Sub FormFAQ_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = Form1.Text + " - Help"
    End Sub

    Private Sub MoveToLocation() Handles Me.VisibleChanged
        Me.Location = Form1.MasterLocation
    End Sub

    Private Sub btnDone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDone.Click

        Form1.Show()
        Me.Hide()

    End Sub

    Private Sub OnClose(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        ' Close main form
        Form1.Close()

    End Sub

    Private Sub UpdateLocation() Handles Me.Move
        Form1.MasterLocation = Me.Location
    End Sub

End Class
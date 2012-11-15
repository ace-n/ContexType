Public Class FormFAQ

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
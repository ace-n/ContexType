Imports System.Windows.Forms

Public Class Hopper

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Sub MouseSelectSuggestion(sender As Object, e As System.EventArgs) Handles lbox_ideas.MouseDown
        HandleSuggestionsClick(True)
    End Sub

    ' Bug fixes (permanent): fixes the bug which caused it to not activate properly
    Public Sub BugFix(sender As Object, e As System.EventArgs) Handles Me.MouseDown
        HandleSuggestionsClick(False)
    End Sub
    Public Sub BugFix2(sender As Object, e As System.EventArgs) Handles pbar.MouseDown
        HandleSuggestionsClick(False)
    End Sub

    ' Handles clicks on the suggestions list form
    '   Fixes the previous bug which caused it to not activate properly
    '   Also allows for functionality to click-and-type words (requested by Mrs. Guetter)
    Public Sub HandleSuggestionsClick(ConsiderIdeas As Boolean)

        ' Activate the main Word app (to fix the bug)
        Form1.WordApp.Activate()

        ' If text is selected, type the rest of it accordingly
        If ConsiderIdeas And lbox_ideas.SelectedIndex <> -1 Then

            ' Type out text
            Dim Text As String = lbox_ideas.SelectedItem

            ' If entire word setting enabled, remove the start of the current word
            If Form1.O_EntireWord Then
                SendKeys.Send(Text.Substring(Form1.WordCurrent.Length))
            Else
                SendKeys.Send(Text)
            End If

        End If

    End Sub

End Class

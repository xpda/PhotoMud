'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Public Class frmOverwrite
  Inherits Form

  Private Sub cmdYes_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdYes.Click
    If chkAll.Checked Then
      Me.DialogResult = DialogResult.OK
      OverwriteResponse = "all"
    Else
      Me.DialogResult = DialogResult.Yes
      OverwriteResponse = "yes"
    End If
    Me.Close()
  End Sub

  Private Sub cmdNo_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdNo.Click
    If chkAll.Checked Then
      Me.DialogResult = DialogResult.None
      OverwriteResponse = "none"
    Else
      Me.DialogResult = DialogResult.No
      OverwriteResponse = "no"
    End If
    Me.Close()
  End Sub

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
    Me.DialogResult = DialogResult.Cancel
    OverwriteResponse = "cancel" ' cancel
  End Sub

  Private Sub fmOverwrite_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Label1.Text = strOverWriteFile & " already exists." & crlf & "Do you want to overwrite it?"
    chkAll.Checked = False

  End Sub

End Class
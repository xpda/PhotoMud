'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Public Class frmAskAssociate
  Inherits Form

  Private Sub cmdYN_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdYes.Click, cmdNo.Click

    If chkAskAgain.Checked Then
      iniAskAssociate = False
    Else
      iniAskAssociate = True
    End If

    If Sender Is cmdYes Then
      Using frm As New frmFileAssoc
        frm.ShowDialog()
      End Using
    End If
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub frmAskAssociate_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, "frmfileassoc.html")

    Label1.Text = "No files are currently associated with " & AppName & "." & crlf & crlf & _
      "Do you want to select the image file types for Windows to associate with " & AppName & "?"
  End Sub

  Private Sub cmdHelp_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdHelp.Click
    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.Topic, "frmfileassoc.html")
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

End Class
'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Public Class frmAbout
  Inherits Form

  Private Sub about_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles about.Click
    Me.DialogResult = DialogResult.OK
    Me.Close()
  End Sub

  Private Sub frmAbout_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.TableOfContents)
    helpProvider1.SetHelpKeyword(Me, "")

    Me.Text = "About " & appLongTitle
    lbTitle.Text = appLongTitle
    lbVersion.Text = "Version " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build
    lbUrl.Text = urlMain
    lbCompany.Text = Application.CompanyName

    ' center labels
    lbTitle.Left = (Frame1.Width - lbTitle.Width) / 2
    lbVersion.Left = (Frame1.Width - lbVersion.Width) / 2
    lbUrl.Left = (Frame1.Width - lbUrl.Width) / 2
    lbCompany.Left = (Frame1.Width - lbCompany.Width) / 2

  End Sub

  Private Sub lbUrl_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles lbUrl.Click
    If lbUrl.Text <> "" Then HelpBrowse(urlMain) ' show the url in a browser
  End Sub

  Private Sub lbUrl_MouseMove(ByVal Sender As Object, ByVal e As MouseEventArgs) Handles lbUrl.MouseMove
    lbUrl.Cursor = Cursors.Hand
  End Sub

End Class
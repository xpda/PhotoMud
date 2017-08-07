Public Class frmSaveVerify
  Inherits Form

  Private Sub cmdNoSave_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdNoSave.Click

    Dim frmWeb As frmWebPage = callingForm

    frmWeb.webResize = -1

    Me.DialogResult = DialogResult.No
    Me.Close()

  End Sub

  Private Sub cmdSave_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdSave.Click

    Dim frmWeb As frmWebPage = callingForm

    Dim x As Double

    If IsNumeric(txtXres.Text) Then
      x = CDbl(txtXres.Text)
    End If
    If Not IsNumeric(txtXres.Text) Or x <= 0 Then
      MsgBox("Invalid Resolution: " & txtXres.Text & ".", MsgBoxStyle.OkOnly)
      txtXres.select()
      Exit Sub
    Else
      frmWeb.Xres = x
      iniSaveXSize = x
    End If

    If IsNumeric(txtYres.Text) Then
      x = CDbl(txtYres.Text)
    End If
    If Not IsNumeric(txtYres.Text) Or x <= 0 Then
      MsgBox("Invalid Resolution: " & txtYres.Text & ".", MsgBoxStyle.OkOnly)
      txtYres.select()
      Exit Sub
    Else
      frmWeb.Yres = x
      iniSaveYSize = x
    End If

    If IsNumeric(txtQuality.Text) Then
      x = CDbl(txtQuality.Text)
    End If
    If Not IsNumeric(txtXres.Text) Or x < 2 Or x > 255 Then
      MsgBox("Invalid Compression Value: " & txtQuality.Text & ".", MsgBoxStyle.OkOnly)
      txtQuality.select()
      Exit Sub
    Else
      frmWeb.JPGQuality = x
    End If

    If chkResize.Checked Then frmWeb.webResize = 1 Else frmWeb.webResize = 0

    Me.DialogResult = DialogResult.Yes
    Me.Close()

  End Sub

  Private Sub fmSaveVerify_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim i As Integer

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    If frmWebPage.webResize >= 0 Then chkResize.Checked = frmWebPage.webResize Else chkResize.Checked = False
    i = frmWebPage.Xres
    If i > 0 And i <= MaxRes Then txtXres.Text = i Else txtXres.Text = 1280
    i = frmWebPage.Yres
    If i > 0 And i <= MaxRes Then txtYres.Text = i Else txtYres.Text = 1280

    Label1.Text = "The photos for the web page can be saved to the folder """ & frmWebPage.webPath & """ now, or you can save them later. If you copy them later, be sure that all images are a type that can be displayed by a web browser -- .jpg is the most popular image format for browsers."

    If frmWebPage.webPath = iniExplorePath Then
      Label1.Text = "The photos for the web page are already in the destination folder """ & frmWebPage.webPath & """. If you want to change the resolution or convert them to .jpg format now, press the ""Save Photos"" button below. Otherwise, press the ""Do Not Save"" button."
    End If

    txtQuality.Text = CStr(iniJpgQuality)
    lbCompression.Text = "0 is highest compression and lowest quality." & crlf & _
      "100 is highest quality and lowest compression." & crlf & "95 is a good value for most applications."
    txtXres.Text = CStr(iniSaveXSize)
    txtYres.Text = CStr(iniSaveYSize)

  End Sub

  Private Sub txtXres_Enter(ByVal Sender As Object, ByVal e As EventArgs) Handles txtXres.Enter

    txtXres.SelectionStart = 0
    txtXres.SelectionLength = Len(txtXres.Text)

  End Sub
  Private Sub txtYres_Enter(ByVal Sender As Object, ByVal e As EventArgs) Handles txtYres.Enter

    txtYres.SelectionStart = 0
    txtYres.SelectionLength = Len(txtYres.Text)

  End Sub

End Class
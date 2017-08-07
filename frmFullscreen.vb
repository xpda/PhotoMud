Imports System.IO

Public Class frmFullscreen
  Inherits Form

  Dim fPic As Integer
  Dim picpath As String
  Dim WithEvents rview As New pViewer
  Dim imgTagCheck As New ImageList

  Private Sub form_KeyDown(ByVal Sender As Object, ByVal e As KeyEventArgs) _
    Handles Me.KeyDown, rview.KeyDown
    globalkey(e)
  End Sub

  Private Sub frmFullscreen_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load
    ' qimage is already assigned by the calling form

    Dim tagSize As Integer = 21

    picTagCheck.Visible = False

    rview.Dock = DockStyle.Fill
    Me.Controls.Add(rview)

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    If callingForm Is frmExplore Then
      fPic = frmExplore.iPic
      ' get image to show tag
      getStateImages(imgTagCheck, System.Drawing.SystemColors.Control, tagSize, tagSize)
      picTagCheck.Width = tagSize
      picTagCheck.Height = tagSize
      picTagCheck.Image = imgTagCheck.Images(1)
    ElseIf callingForm.Name = "frmWebPage" Then
      fPic = frmWebPage.ipic
    ElseIf callingForm.Name = "frmCalendar" Then
      fPic = frmCalendar.ipic
    ElseIf callingForm Is frmMain Then
      fPic = frmMain.iPic
    End If

    Me.WindowState = FormWindowState.Maximized
    picpath = currentpicPath
    rview.setBitmap(qImage)

  End Sub

  Private Sub frmFullscreen_MouseDown(ByVal Sender As Object, ByVal e As MouseEventArgs) _
    Handles MyBase.MouseDown, rview.MouseDown
    'If e.Button = MouseButtons.Left Then
    '  currentpicPath = picpath
    '  Me.Close()
    '  End If

    If e.Button = MouseButtons.Left Then
      Movenpic(1)
    ElseIf e.Button = MouseButtons.Left Then
      Movenpic(-1)
    End If

  End Sub

  Private Sub globalkey(ByVal e As KeyEventArgs)

    If e.KeyCode = Keys.Oemplus Or e.KeyCode = Keys.OemMinus Or _
      e.KeyCode = Keys.Add Or e.KeyCode = Keys.Subtract Or _
      e.KeyCode = Keys.Oemcomma Or e.KeyCode = Keys.OemPeriod Or _
      e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.PageUp Or _
      e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Then
      If e.KeyCode = Keys.Left Or e.KeyCode = Keys.OemMinus Or e.KeyCode = Keys.Subtract Or _
        e.KeyCode = Keys.Oemcomma Or e.KeyCode = Keys.PageUp Then
        Movenpic(-1)
      Else
        Movenpic(1)
      End If

    ElseIf e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Enter Then
      currentpicPath = picpath
      Me.DialogResult = DialogResult.Cancel
      Me.Close()
    End If

  End Sub

  Private Sub mnxPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnxPrevious.Click
    Movenpic(-1)
  End Sub

  Private Sub mnxNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnxNext.Click
    Movenpic(1)
  End Sub

  Sub Movenpic(ByVal increment As Integer)

    fPic = fPic + increment
    If callingForm Is frmExplore Then
      picpath = frmExplore.getNextPath(fPic)
    ElseIf callingForm.Name = "frmWebPage" Then
      picpath = frmWebPage.getNextPath(fPic)
    ElseIf callingForm.Name = "frmCalendar" Then
      picpath = frmCalendar.getNextPath(fPic)
      'ElseIf callingForm Is frmMain Then
      '  picpath = frmMain.getNextPath(fPic)
    Else
      Exit Sub
    End If

    Me.Cursor = Cursors.WaitCursor
    showPicture(picpath, rview, False, Nothing, -1)
    rview.Refresh()
    showtag()
    Me.Cursor = Cursors.Default

  End Sub

  Private Sub mnxInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnxInfo.Click

    Dim LastCallingform As Form

    If rview.Bitmap IsNot Nothing Then
      LastCallingform = callingForm
      callingForm = Me
      currentpicPath = picpath
      qImage = rview.Bitmap.Clone
      Using frm As New frmInfo
        frm.ShowDialog()
      End Using

      callingForm = LastCallingform
    End If

  End Sub

  Private Sub mnxComment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnxComment.Click

    Dim LastCallingform As Form

    If rview.Bitmap IsNot Nothing Then
      LastCallingform = callingForm
      callingForm = Me
      currentpicPath = picpath
      qImage = rview.Bitmap.Clone
      Using frm As New frmComment
        frm.ShowDialog()
      End Using
      callingForm = LastCallingform
    End If

  End Sub

  Private Sub mnxDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnxDelete.Click

    Dim mResult As MsgBoxResult
    Dim k As Integer

    If callingForm IsNot frmExplore Then Exit Sub ' only delete from frmexplore

    mResult = MsgBox("Do you really want to delete """ & picpath & """?", MsgBoxStyle.YesNoCancel)

    If mResult = MsgBoxResult.Yes Then
      ' delete the file
      Try
        File.Delete(picpath)
        If iniDelRawFiles Then delMatchingRawFile(picpath)
      Catch ex As Exception
        MsgBox(ex.Message)
      End Try

      k = tagged(picpath)

      If k > 0 Then ' clear the tag
        tagPath.RemoveAt(k)
      End If

      Movenpic(0) ' go to the previous file
    End If

  End Sub

  Private Sub mnxTag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnxTag.Click

    Dim k As Integer

    If File.Exists(picpath) Then
      k = tagged(picpath)
      If k > 0 Then
        ' it's tagged -- remove it from the tag list
        tagPath.RemoveAt(k)
      Else
        ' not tagged -- add it to the tag list
        tagPath.Add(picpath)
      End If
    End If

    showtag()

  End Sub

  Private Sub mnx_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mnx.Opening

    If callingForm Is frmExplore Then
      mnxTag.Visible = True
      mnxDelete.Visible = True
      If tagged(picpath) > 0 Then mnxTag.Checked = True Else mnxTag.Checked = False
    Else
      mnxTag.Visible = False
      mnxDelete.Visible = False
    End If

  End Sub

  Function tagged(ByVal fpath As String) As Integer
    ' returns -1 if file is not tagged, the tag index otherwise

    Dim i As Integer

    tagged = -1
    For i = 0 To tagPath.Count - 1
      If eqstr(tagPath(i), fpath) Then
        tagged = i
        Exit For
      End If
    Next i

  End Function

  Sub showtag()

    Dim r As Rectangle

    If callingForm Is frmExplore AndAlso tagged(picpath) > 0 Then
      r = rview.ClientRectangle
      picTagCheck.Left = r.Left + r.Width - picTagCheck.Width + 1
      picTagCheck.Top = r.Top + 2
      picTagCheck.Visible = True
    Else
      picTagCheck.Visible = False
    End If

  End Sub

  Private Sub frmFullscreen_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    rview.Zoom(-1)
    showtag()
  End Sub

  Private Sub frmFullscreen_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    Me.Dispose()
  End Sub

End Class
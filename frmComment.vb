'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.IO
Imports System.Collections.Generic
Imports ImageMagick

Public Class frmComment

  Inherits Form
  Dim picpath As String
  Dim commentChanged As Boolean = False
  Dim msg As String

  Dim processing As Boolean

  Dim filenames As New List(Of String)
  Dim iPic As Integer
  Dim lastComment As String
  'Dim picInfo As pictureInfo
  Dim pComments As New List(Of PropertyItem)

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
    Me.DialogResult = DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub cmdHelp_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdHelp.Click
    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.Topic, Me.Name & ".html")
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Private Sub cmdOK_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdOK.Click

    msg = ""
    If rText2.Visible Then rtext1_Leave(rText2, New EventArgs()) ' validate date
    If rText1.Visible AndAlso msg = "" Then rtext1_Leave(rText1, New EventArgs()) ' validate date
    If rText3.Visible AndAlso msg = "" Then rtext3_Leave(rText3, New EventArgs()) ' validate GPS


    If msg = "" Then
      If commentChanged Then
        Me.DialogResult = saveComments()
      Else
        Me.DialogResult = DialogResult.OK
      End If

      Me.Close()
    End If

  End Sub

  Private Sub frmComment_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim i As Integer
    Dim s1 As String
    Dim sComment(3) As String
    Dim xLat, xLon As Double
    Dim iAltitude As Integer

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    processing = True

    If iniDateinCommentCommand Then
      GroupBox1.Visible = True
    Else
      GroupBox1.Visible = False
    End If

    filenames = New List(Of String)
    iPic = -1

    picpath = currentpicPath
    If callingForm Is frmExplore Then
      If frmExplore.rview.Bitmap Is Nothing Then ' load the file into rview.bitmap
        pComments = New List(Of PropertyItem)
        showPicture(picpath, pView, False, pComments)
      Else
        Try
          pView.setBitmap(frmExplore.rview.Bitmap)
        Catch ex As Exception
        End Try
        pComments = frmExplore.pComments
      End If

      cmdNext.Visible = True
      cmdBack.Visible = True

    ElseIf callingForm Is frmMain Then
      pView.setBitmap(frmMain.mView.Bitmap)
      pComments = frmMain.mView.pComments
      cmdNext.Visible = False
      cmdBack.Visible = False

    ElseIf callingForm.Name = "frmFullscreen" Then
      pView.setBitmap(qImage)
      pComments = readPropertyItems(currentpicPath)
      cmdNext.Visible = False
      cmdBack.Visible = False
    End If

    pView.Zoom(0)

    lbPicPath.Text = Path.GetFileName(picpath)

    s1 = LCase(Path.GetExtension(picpath))

    sComment(0) = CStr(getBmpComment(propID.ImageDescription, pComments))
    sComment(1) = CStr(getBmpComment(propID.DateTime, pComments))
    sComment(2) = CStr(getBmpComment(propID.DateTimeOriginal, pComments))
    getGPSLocation(pComments, sComment(3), s1, xLat, xLon, iAltitude)

    For i = 1 To 2
      If Len(sComment(i)) = 19 Then
        sComment(i) = Mid(sComment(i), 6, 2) & "/" & Mid(sComment(i), 9, 2) & "/" & _
          sComment(i).Substring(0, 4) & sComment(i).Substring(sComment(i).Length - 9)
      End If
    Next i

    rText0.Text = sComment(0)
    rText1.Text = sComment(1)
    rText2.Text = sComment(2)
    rText3.Text = sComment(3)

    commentChanged = False

    ' load filenames in current folder, find currently displayed one (iPic)
    i = getFilePaths(iniExplorePath, filenames, False)
    For i = 0 To filenames.Count - 1
      If eqstr(currentpicPath, filenames(i)) Then
        iPic = i
        Exit For
      End If
    Next i

    If iPic < 0 Then
      filenames = New List(Of String)
      filenames.Add(currentpicPath) ' should never happen
      iPic = 0
    End If

    processing = False

  End Sub

  Private Sub rtext_textchanged(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles rText0.TextChanged, rText1.TextChanged, rText2.TextChanged, rText3.TextChanged
    commentChanged = True
  End Sub

  Private Sub cmdNext_Click(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles cmdNext.Click, cmdBack.Click

    Dim i As Integer
    Dim s As String = ""
    Dim sComment(3) As String
    Dim result As DialogResult
    Dim xLat, xLon As Double
    Dim iAltitude As Integer

    If processing Then Exit Sub
    processing = True

    lastComment = rText0.Text ' for F3

    If commentChanged Then
      result = saveComments()
      If result = DialogResult.Cancel Then
        processing = False
        Exit Sub
      End If
    End If

    If filenames.Count <= 0 Then
      processing = False
      Exit Sub
    End If
    If Sender Is cmdBack Then iPic -= 1 Else iPic += 1
    If iPic < 0 Then iPic = filenames.Count - 1
    If iPic >= filenames.Count Then iPic = 0

    showPicture(filenames(iPic), pView, False, pComments) ' true is for show thumbnail if possible
    currentpicPath = filenames(iPic)

    lbPicPath.Text = Path.GetFileName(filenames(iPic))
    picpath = filenames(iPic)

    sComment(0) = CStr(getBmpComment(propID.ImageDescription, pComments))
    sComment(1) = CStr(getBmpComment(propID.DateTime, pComments))
    sComment(2) = CStr(getBmpComment(propID.DateTimeOriginal, pComments))
    getGPSLocation(pComments, sComment(3), s, xLat, xLon, ialtitude)

    For i = 1 To 2
      If Len(sComment(i)) = 19 Then
        s = sComment(i)
        'sComment(i) = Mid(s, 6, 2) & "/" & Mid(s, 9, 2) & "/" & vb.Left(s, 4) & vb.Right(s, 9)
        sComment(i) = s.Substring(5, 2) & "/" & s.Substring(8, 2) & "/" & s.Substring(0, 4) & s.Substring(s.Length - 9)

      End If
    Next i

    rText0.Text = sComment(0)
    rText1.Text = sComment(1)
    rText2.Text = sComment(2)
    rText3.Text = sComment(3)
    commentChanged = False

    If rText0.Visible Then rText0.Focus()

    ' s1 = LCase(vb.Right(picpath, 4))
    ' s1 = LCase(picpath.Substring(picpath.Length - 4))

    processing = False

  End Sub

  Private Function saveComments() As DialogResult

    Dim d As Date
    Dim sComment(3) As String
    Dim xLat As Double, xLon As Double
    Dim imgsave As New ImageSave

    sComment(0) = rText0.Text.Trim
    sComment(1) = rText1.Text.Trim
    sComment(2) = rText2.Text.Trim
    sComment(3) = rText3.Text.Trim

    For i As Integer = 1 To 2
      If sComment(i) <> "" Then
        Try
          d = CDate(sComment(i))
          sComment(i) = Format(d, "yyyy:MM:dd HH:mm:ss")
        Catch ex As Exception
        End Try
      End If
    Next i

    If Trim(sComment(0)) <> "" Then setBmpComment(propID.ImageDescription, pComments, sComment(0), exifType.typeAscii)
    If Trim(sComment(1)) <> "" Then setBmpComment(propID.DateTime, pComments, sComment(1), exifType.typeAscii)
    If Trim(sComment(2)) <> "" Then setBmpComment(propID.DateTimeOriginal, pComments, sComment(2), exifType.typeAscii)
    If Trim(sComment(3)) <> "" Then
      msg = latlonVerify(sComment(3), xLat, xLon)
      If msg = "" Then
        setGPSLatLon(xLat, xLon, pComments)
      Else
        MsgBox(msg)
      End If
    End If

    For i As Integer = pComments.Count - 1 To 0 Step -1 ' remove empty comments
      If (pComments(i).Id = propID.ImageDescription AndAlso Trim(sComment(0)) = "") OrElse
        (pComments(i).Id = propID.DateTime AndAlso Trim(sComment(1)) = "") OrElse
        (pComments(i).Id = propID.DateTimeOriginal AndAlso Trim(sComment(2)) = "") OrElse
        (pComments(i).Id = propID.GpsLatitude AndAlso Trim(sComment(3)) = "") OrElse
        (pComments(i).Id = propID.GpsLatitudeRef AndAlso Trim(sComment(3)) = "") OrElse
        (pComments(i).Id = propID.GpsLongitude AndAlso Trim(sComment(3)) = "") OrElse
        (pComments(i).Id = propID.GpsLongitudeRef AndAlso Trim(sComment(3)) = "") Then
        pComments.RemoveAt(i)
      End If
    Next i

    If callingForm Is frmMain OrElse Len(picpath) = 0 Then ' save comments to callingform, not to disk (picpath = "" should not happen)
      If callingForm Is frmExplore Then ' have to use separate variables for the frmExplore and frmMain
        frmExplore.pComments = New List(Of PropertyItem)
        frmExplore.pComments.AddRange(pComments)
      Else
        frmMain.mView.pComments = New List(Of PropertyItem)
        frmMain.mView.pComments.AddRange(pComments)
        frmMain.mView.newComment = True
      End If

    Else ' save comments to file
      'picInfo = getPicinfo(picpath)
      imgsave.pComments = New List(Of PropertyItem)
      imgsave.pComments.AddRange(pComments)
      imgsave.Quality = iniJpgQuality
      imgsave.saveExif = True
      imgsave.copyProfiles = False

      msg = imgsave.write(pView.Bitmap, picpath, True)

      If msg <> "" Then Return DialogResult.Abort
    End If

    Return DialogResult.OK

  End Function

  Private Sub pview_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pView.MouseClick

    Dim lastCallingForm As Form

    If pView.Bitmap Is Nothing Then Exit Sub
    If pView.Bitmap.Width < 600 And pView.Bitmap.Height < 600 Then
      showPicture(filenames(iPic), pView, False, Nothing)
      Exit Sub
    End If
    If callingForm.Name <> "frmFullscreen" Then
      lastCallingForm = callingForm ' preserve for next, previous commands
      callingForm = Me
      currentpicPath = filenames(iPic)
      clearBitmap(qImage)
      qImage = pView.Bitmap.Clone
      Using frm As New frmFullscreen
        frm.ShowDialog()
      End Using

      clearBitmap(qImage)
      callingForm = lastCallingForm
    End If

  End Sub

  Private Sub rText_Enter(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles rText0.Enter, rText1.Enter, rText3.Enter
    Dim rText As RichTextBox
    rText = Sender

    If Not rText.Multiline Then ' highlight text
      rText.SelectionStart = 0
      rText.SelectionLength = Len(rText.Text)
    End If

  End Sub

  Private Sub rtext1_Leave(ByVal Sender As Object, ByVal e As EventArgs) Handles rText1.Leave, rText2.Leave

    Dim rTex As RichTextBox
    rTex = Sender

    If Trim(rTex.Text) <> "" Then
      Try
        rTex.Text = Format(CDate(rTex.Text), "g")
      Catch ex As Exception
        MsgBox("Invalid date:" & ex.Message)
        msg = ex.Message ' for cmdOK
        rTex.Focus()
      End Try

    End If

  End Sub

  Private Sub rtext3_Leave(ByVal Sender As Object, ByVal e As EventArgs) Handles rText3.Leave

    Dim gmsg As String
    Dim xLat, xLon As Double
    Dim s, s1 As String

    If Trim(rText3.Text) = "" Then Exit Sub

    gmsg = latlonVerify(rText3.Text, xLat, xLon)

    If gmsg <> "" Then
      MsgBox(gmsg)
      msg = gmsg ' for cmdOK
      rText3.Focus()
    Else
      If xLat > 0 Then s1 = "N" Else s1 = "S"
      xLat = Abs(xLat)
      s = Int(xLat) & "°" & Format((xLat - Int(xLat)) * 60, "#0.####") & "'" & s1
      If xLon > 0 Then s1 = "E" Else s1 = "W"
      xLon = Abs(xLon)
      s &= " " & Int(xLon) & "°" & Format((xLon - Int(xLon)) * 60, "#0.####") & "'" & s1
      rText3.Text = s
    End If

  End Sub

  Private Sub pview_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pView.MouseMove
    If pView.Bitmap IsNot Nothing And (Not processing) Then
      pView.Cursor = Cursors.Hand
    Else
      pView.Cursor = Cursors.Default
    End If
  End Sub

  Private Sub r_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) _
    Handles cmdNext.KeyDown, cmdHelp.KeyDown
    globalkey(e)
  End Sub
  Private Sub rText_KeyDown(ByVal Sender As Object, ByVal e As KeyEventArgs) _
    Handles rText0.KeyDown, rText1.KeyDown
    globalkey(e)
  End Sub

  Private Sub globalkey(ByRef e As KeyEventArgs)

    Dim s As String

    Select Case e.KeyCode
      Case Keys.F2 ' 113 ' F2 = clear
        rText0.Text = ""
        rText1.Text = ""
        e.Handled = True

      Case Keys.F3 ' 114 ' F3 = last values
        rText0.Text = lastComment
        rText0.Refresh()
        e.Handled = True

      Case Keys.F4 ' F4 = filename
        s = Path.GetFileNameWithoutExtension(picpath)
        If rText0.Text = "" Then rText0.Text = s Else rText0.Text &= vbCrLf & s
        rText0.Refresh()
        e.Handled = True

      Case Keys.PageDown ' arrow keys cause problems with rtext
        If cmdNext.Visible Then
          cmdNext_Click(cmdNext, Nothing)
          e.Handled = True
        End If

      Case Keys.PageUp
        If cmdNext.Visible Then
          cmdNext_Click(cmdBack, Nothing)
          e.Handled = True
        End If

      Case Else
        e.Handled = False
    End Select

  End Sub

  Private Sub pView_SizeChanged(sender As Object, e As EventArgs) Handles pView.SizeChanged
    pView.Zoom(0)
  End Sub

  Private Sub frmComment_Shown(sender As Object, e As EventArgs) Handles Me.Shown
    rText0.Focus()
  End Sub

End Class
'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

Imports System.IO
Imports System.Math
Imports ImageMagick
Imports System.Collections.Generic

Public Class frmSend
  Inherits Form

  Dim OriginalXres As Double
  Dim OriginalYres As Double
  Dim picInfo As pictureInfo

  Private Sub cmbFiletype_SelectedIndexChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles cmbFiletype.SelectedIndexChanged
    checkfields()
  End Sub

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

  Private Sub cmdSend_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdSend.Click

    Dim msg As String

    If optResize.Checked Or forceConverted Then
      msg = sendconverted()
    Else
      SendOriginal()
      msg = ""
    End If

    If msg = "" Then
      Me.DialogResult = DialogResult.OK
      Me.Close() ' done unless invalid paramter.
    End If

  End Sub
  Function SendOriginal() As String

    Dim fNames As New List(Of String)
    Dim attachmentList As String = ""
    Dim msg As String

    If Not MultiFile Then ' single image
      fnames.add(currentpicPath)
    Else ' multiple images
      fNames.AddRange(tagPath)
    End If

    msg = sendMail(txFromAddress.Text, txToAddress.Text,
                   iniEmailHost, iniEmailPort, iniEmailAccount, iniEmailPassword,
                   rText1.Text, txSubject.Text, txFromName.Text, fNames)
    Return msg

  End Function

  Private Sub frmSend_Activated(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Activated
    Me.BringToFront()
  End Sub

  Private Sub frmSend_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim i As Integer

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    If MultiFile Then
      OptOriginal.Text = "Send the &Original Photos"
      optResize.Text = "&Convert or Resize the Photos"
      lbPct.Visible = False
      txtPct.Visible = False
      chkResize.Text = "&Resize Photos"
    Else
      OptOriginal.Text = "Send the &Original Photo"
      optResize.Text = "&Convert or Resize the Photo"
      lbPct.Visible = True
      txtPct.Visible = True
      chkResize.Text = "&Resize Photo"
      If callingForm Is frmMain Then
        OriginalXres = frmMain.mView.Bitmap.Width
        OriginalYres = frmMain.mView.Bitmap.Height
      ElseIf callingForm Is frmExplore Then
        If frmExplore.rview.Bitmap IsNot Nothing Then
          OriginalXres = frmExplore.rview.Bitmap.Width
          OriginalYres = frmExplore.rview.Bitmap.Height
        Else
          picInfo = getPicinfo(currentpicPath, False)
          OriginalXres = picInfo.Width
          OriginalYres = picInfo.Height
        End If
        End If
        txtXres.Text = CStr(OriginalXres)
        txtYres.Text = CStr(OriginalYres)
        txtPct.Text = "100%"
    End If

    cmbFiletype.SelectedIndex = -1
    For i = 0 To fmtCommon.Count - 1
      cmbFiletype.Items.Insert(i, fmtCommon(i).Description & " (" & fmtCommon(i).Ext & ")")
      If fmtCommon(i).ID = ".jpg" Then cmbFiletype.SelectedIndex = i
    Next i

    If cmbFiletype.SelectedIndex < 0 Then cmbFiletype.SelectedIndex = 0

    nmQuality.Value = iniSendJPGQuality
    lbCompression.Text = "0 is highest compression and lowest quality." & crlf & _
      "100 is highest quality and lowest compression." & crlf & "95 is a good value for most applications."

    If iniSendOriginal Then
      OptOriginal.Checked = True ' send original image
    Else
      optResize.Checked = True
    End If

    checkfields()

  End Sub

  Private Sub OptOriginal_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles OptOriginal.CheckedChanged

    If OptOriginal.Checked Then
      cmbFiletype.Enabled = False
      label3.Enabled = False
      lbCompression.Enabled = False
      lbCompression1.Enabled = False
      nmQuality.Enabled = False
      Frame1.Enabled = False
      lbPct.Enabled = False
      txtPct.Enabled = False
      lbXres.Enabled = False
      txtXres.Enabled = False
      lbYres.Enabled = False
      txtYres.Enabled = False
      chkResize.Enabled = False
    ElseIf optResize.Checked Then
      cmbFiletype.Enabled = True
      label3.Enabled = True
      lbCompression.Enabled = True
      lbCompression1.Enabled = True
      nmQuality.Enabled = True
      Frame1.Enabled = True
      lbPct.Enabled = True
      txtPct.Enabled = True
      lbXres.Enabled = True
      txtXres.Enabled = True
      lbYres.Enabled = True
      txtYres.Enabled = True
      chkResize.Enabled = True
      checkfields()
    End If

  End Sub
  Private Sub checkfields()

    ' enabled or disable fields based on data values

    If chkResize.Checked Then
      lbPct.Enabled = True
      txtPct.Enabled = True
      lbXres.Enabled = True
      txtXres.Enabled = True
      lbYres.Enabled = True
      txtYres.Enabled = True
    Else
      lbPct.Enabled = False
      txtPct.Enabled = False
      lbXres.Enabled = False
      txtXres.Enabled = False
      lbYres.Enabled = False
      txtYres.Enabled = False
    End If

    If fmtCommon(cmbFiletype.SelectedIndex).ID = ".jpg" Then
      lbCompression1.Visible = True
      lbCompression.Visible = True
      nmQuality.Visible = True
    Else
      lbCompression1.Visible = False
      lbCompression.Visible = False
      nmQuality.Visible = False
    End If

  End Sub
  Private Sub txtXres_Leave(ByVal Sender As Object, ByVal e As EventArgs) Handles txtXres.Leave

    Dim x As Double

    If IsNumeric(txtXres.Text) And Not MultiFile Then
      x = CDbl(txtXres.Text)
      If x > 0 And x <= MaxRes Then
        txtPct.Text = Round(x / OriginalXres * 100) & "%"
        txtYres.Text = CStr(Round(OriginalYres / OriginalXres * x))
      Else
        txtPct.Text = ""
      End If
    Else
      txtPct.Text = ""
    End If

  End Sub
  Private Sub txtPct_Leave(ByVal Sender As Object, ByVal e As EventArgs) Handles txtPct.Leave

    Dim i As Short
    Dim s As String
    Dim x As Double
    s = txtPct.Text
    i = s.indexof("%")
    If i >= 0 Then s = s.substring(0, i - 1)

    If IsNumeric(s) Then
      x = CDbl(s)
      If x > 0 And x < MaxRes Then
        txtXres.Text = CStr(Round(x * OriginalXres / 100))
        txtYres.Text = CStr(Round(x * OriginalYres / 100))
      Else
        txtPct.Text = ""
      End If
    Else
      txtPct.Text = ""
    End If

  End Sub
  Private Sub txtYres_Leave(ByVal Sender As Object, ByVal e As EventArgs) Handles txtYres.Leave

    Dim x As Double

    If IsNumeric(txtYres.Text) And Not MultiFile Then
      x = CDbl(txtYres.Text)
      If x > 0 And x < MaxRes Then
        txtPct.Text = Round(x / OriginalYres * 100) & "%"
        txtXres.Text = CStr(Round(OriginalXres / OriginalYres * x))
      Else
        txtPct.Text = ""
      End If
    Else
      txtPct.Text = ""
    End If

  End Sub

  Private Sub txtXres_Enter(ByVal Sender As Object, ByVal e As EventArgs) Handles txtXres.Enter

    txtXres.SelectionStart = 0
    txtXres.SelectionLength = Len(txtXres.Text)

  End Sub
  Private Sub txtPct_Enter(ByVal Sender As Object, ByVal e As EventArgs) Handles txtPct.Enter

    txtPct.SelectionStart = 0
    txtPct.SelectionLength = Len(txtPct.Text)

  End Sub

  Private Sub txtYres_Enter(ByVal Sender As Object, ByVal e As EventArgs) Handles txtYres.Enter

    txtYres.SelectionStart = 0
    txtYres.SelectionLength = Len(txtYres.Text)

  End Sub

  Private Sub chkResize_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles chkResize.CheckedChanged

    If chkResize.Checked Then
      lbXres.Enabled = True
      lbYres.Enabled = True
      lbPct.Enabled = True
      txtXres.Enabled = True
      txtYres.Enabled = True
      txtPct.Enabled = True
    Else
      lbXres.Enabled = False
      lbYres.Enabled = False
      lbPct.Enabled = False
      txtXres.Enabled = False
      txtYres.Enabled = False
      txtPct.Enabled = False
    End If

  End Sub

  Function sendconverted() As String

    ' this sub copies files to a temp directory and sends them from there,
    ' using the compression and sizing options on the form.
    Dim targetPath As String
    Dim targetName As String
    Dim targetFile As String
    Dim Xres As Double
    Dim Yres As Double
    Dim s As String
    Dim mResult As MsgBoxResult
    Dim msg As String = ""
    Dim fNames As New List(Of String)
    Dim fName As String
    Dim saver As New ImageSave
    Dim saveSize As New Size
    Dim bmp As Bitmap = Nothing

    If forceConverted Then saveSize = New Size(OriginalXres, OriginalYres)

    If optResize.Checked Then
      ' set parameters
      If Not checknumber((txtXres.Text), 1, MaxRes, Xres) Then
        MsgBox("Enter a number between 1 and " & MaxRes)
        txtXres.Select()
        Return MsgBoxResult.Cancel
      End If

      If Not checknumber((txtYres.Text), 1, MaxRes, Yres) Then
        MsgBox("Enter a number between 1 and " & MaxRes)
        txtYres.Select()
        Return MsgBoxResult.Cancel
      End If

      saveSize = New Size(Xres, Yres)

    End If

    targetPath = UndoPath & "~temp~314159265359\"
    If Not Directory.Exists(targetPath) Then
      Directory.CreateDirectory(targetPath)
    Else
      killdir(targetPath) ' remove files in temp directory
    End If

    If Not MultiFile Then ' single image
      If callingForm Is frmExplore Then
        bmp = readBitmap(currentpicPath, msg)
        If bmp Is Nothing Then
          MsgBox(msg)
          Return msg
        End If

      ElseIf callingForm Is frmMain Then
        bmp = frmMain.mView.Bitmap.Clone
      End If

      If bmp Is Nothing Then
        Me.Cursor = Cursors.Default
        Return "Error"
      End If

      If currentpicPath <> "" Then s = currentpicPath Else s = "image.jpg"

      targetName = Path.GetFileName(s)
      targetName = Path.ChangeExtension(targetName, fmtCommon(cmbFiletype.SelectedIndex).ID)
      targetFile = targetPath & targetName

      saver = New ImageSave

      saver.Quality = nmQuality.Value
      saver.saveExif = True
      saver.compressionAmount = iniJpgQuality
      saver.PngIndexed = False
      saver.compressionMethod = ImageMagick.CompressionMethod.LZW
      saver.saveSize = saveSize
      saver.Format = fmtCommon(cmbFiletype.SelectedIndex).MagickFmt
      msg = saver.write(bmp, targetFile, True)
      If msg <> "" Then
        mResult = MsgBox("Error.  " & targetName & " could not be saved. " & crlf & msg, MsgBoxStyle.OkCancel)
        If mResult = MsgBoxResult.Cancel Then Return msg
      End If

      fNames.Add(targetFile)

    Else ' multiple images

      For Each fName In tagPath
        bmp = readBitmap(fName, msg)

        If bmp Is Nothing Then
          mResult = MsgBox("Error.  " & fName & " could not be loaded. " & crlf & msg, MsgBoxStyle.OkCancel)
          If mResult = MsgBoxResult.Cancel Then Exit For
        End If

        targetName = Path.GetFileName(fName)
        targetName = Path.ChangeExtension(targetName, fmtCommon(cmbFiletype.SelectedIndex).ID)
        targetFile = targetPath & targetName

        saver.Quality = nmQuality.Value
        saver.saveExif = True
        saver.compressionAmount = iniJpgQuality
        saver.PngIndexed = False
        saver.compressionMethod = ImageMagick.CompressionMethod.LZW
        saver.saveSize = New Size(Xres, Yres)
        saver.Format = fmtCommon(cmbFiletype.SelectedIndex).MagickFmt
        saver.sourceFilename = fName
        saver.copyProfiles = True
        msg = saver.write(bmp, targetFile, True)
        If msg <> "" Then
          mResult = MsgBox("Error.  " & targetFile & " could not be saved. " & crlf & msg, MsgBoxStyle.OkCancel)
          If mResult = MsgBoxResult.Cancel Then Exit For
          msg = ""
        Else
          fNames.Add(targetFile)
        End If

        clearBitmap(bmp)
      Next fName
    End If

    If msg = "" Then
      msg = sendMail(txFromAddress.Text, txToAddress.Text,
                     iniEmailHost, iniEmailPort, iniEmailAccount, iniEmailPassword,
                     rText1.Text, txSubject.Text, txFromName.Text, fNames)
    End If

    Try
      killdir(targetPath)
      Directory.Delete(targetPath)
    Catch
    End Try

    clearBitmap(bmp)
    Me.Cursor = Cursors.Default

    Return msg

  End Function

  Private Sub killdir(ByRef strpath As String)

    ' strpath should include backslash

    Dim strFile As String
    Dim kFiles() As String

    Try
      kFiles = Directory.GetFiles(strpath)
      For Each strFile In kFiles
        File.Delete(strFile)
      Next strFile
    Catch ex As System.IO.IOException
    End Try

  End Sub

End Class
Imports System.Math
Imports System.IO
Imports ImageMagick
Imports System.Collections.Generic
Imports System.Drawing.Imaging

Imports vb = Microsoft.VisualBasic

Public Class frmSaveAs
  Inherits Form

  Dim Loading As Boolean = True

  Dim picinfo As pictureInfo
  Dim JPGQuality As Short

  Dim Xres As Double
  Dim Yres As Double

  Dim OriginalXres As Double
  Dim OriginalYres As Double
  Dim OriginalFName As String

  Dim realPct As Double
  Dim pctChanged As Boolean

  Dim WithEvents colordialog1 As New ColorDialog

  Dim ixFmt(fmtCommon.Count - 1) As Integer
  Dim nix As Integer

  Dim targetExt As String
  Dim picFormat As MagickFormat

  Private Sub chkResize_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles chkResize.CheckedChanged
    If Loading Then Exit Sub

    If chkResize.Checked Then
      lbXres.Enabled = True
      lbYres.Enabled = True
      lbPct.Enabled = True
      txXres.Enabled = True
      txYres.Enabled = True
      txPct.Enabled = True
    Else
      lbXres.Enabled = False
      lbYres.Enabled = False
      lbPct.Enabled = False
      txXres.Enabled = False
      txYres.Enabled = False
      txPct.Enabled = False
    End If

  End Sub

  Private Sub cmbFiletype_SelectedIndexChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles cmbFiletype.SelectedIndexChanged
    If Loading Then Exit Sub

    Dim i As Short
    Dim fName As String

    EnableOptions()

    fName = Trim(txFileName.Text)
    If fName <> "" And vb.Right(fName, 1) <> "\" Then
      i = InStr(fName, ".") - 1
      If i > 0 Then
        txFileName.Text = vb.Left(fName, i) & fmtCommon(ixFmt(cmbFiletype.SelectedIndex)).ID
      Else
        txFileName.Text = fName & fmtCommon(ixFmt(cmbFiletype.SelectedIndex)).ID
      End If
    End If

  End Sub

  Private Sub cmdBrowse_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdBrowse.Click

    Dim i As Integer
    Dim sFilter As String
    Dim fName As String
    Dim dirName As String
    Dim ext As String
    Dim result As DialogResult

    fName = Path.GetFileName(txFileName.Text)
    ext = fmtCommon(ixFmt(cmbFiletype.SelectedIndex)).Ext
    If Trim(txFileName.Text) <> "" Then
      dirName = Path.GetDirectoryName(txFileName.Text)
    Else
      dirName = ""
    End If
    saveDialog1.FilterIndex = 1

    sFilter = "All Files|*.*"  ' if no extension on current file
    For i = 0 To fmtCommon.Count - 1
      sFilter = sFilter & "|" & fmtCommon(i).Description & "|*" & fmtCommon(i).Ext
      If (ext = ".jpg" Or ext = ".jpeg") And fmtCommon(i).ID = ".jpg" Or _
         (ext = ".tif" Or ext = ".tiff") And fmtCommon(i).ID = ".tif" Then
        saveDialog1.FilterIndex = i + 2
      End If
    Next i

    saveDialog1.Filter = sFilter
    If eqstr(vb.Right(fName, Len(ext)), ext) Then fName = vb.Left(fName, Len(fName) - Len(ext))
    saveDialog1.FileName = fName
    saveDialog1.DefaultExt = ext
    If dirName <> "" Then
      saveDialog1.InitialDirectory = dirName
    Else
      saveDialog1.InitialDirectory = iniSavePath
    End If
    saveDialog1.CheckPathExists = True
    saveDialog1.OverwritePrompt = False ' ask later
    i = File.Exists(fName)

    Try
      result = saveDialog1.ShowDialog()
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    If result = DialogResult.OK And Len(Trim(saveDialog1.FileName)) <> 0 Then
      txFileName.Text = saveDialog1.FileName
      CheckExt() ' make sure file extension matches format type
    End If

  End Sub

  Private Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click
    Me.DialogResult = DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub cmdSave_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdSave.Click

    Dim i As Short
    Dim fName As String
    Dim s As String

    CheckExt()
    fName = Trim(txFileName.Text)
    If fName = "" Then Exit Sub
    If vb.Right(fName, 1) = "\" Then
      MsgBox(fName & " could not be saved.")
      Exit Sub
    End If

    s = Path.GetDirectoryName(txFileName.Text)
    If s = "" And iniSavePath <> "" Then fName = iniSavePath & "\" & fName
    i = CheckFolder(s, True) ' ask to create folder if not there
    If i < 0 Then Exit Sub

    If callingForm Is frmExplore Then
      saveFromExplorer(fName)
    Else ' from frmmain
      saveFromEditor(fName)
    End If

  End Sub

  Sub saveFromEditor(ByVal fName As String)

    Dim i As Short
    Dim overwriteFlag As String = ""
    Dim saver As New ImageSave
    Dim msg As String

    If frmMain.mView Is Nothing OrElse frmMain.mView.Bitmap Is Nothing Then Exit Sub

    JPGQuality = nmQuality.Value

    If cmbFiletype.SelectedIndex >= 0 Then
      targetExt = fmtCommon(i).ID
    Else
      targetExt = cmbFiletype.Text
    End If

    picFormat = getFormat(targetExt)
    If picFormat < 0 Then
      MsgBox("Unsupported file format.")
      cmbFiletype.Select()
      Exit Sub
    End If

    i = askOverwrite(fName, False, overwriteFlag)
    If i = 0 Then Exit Sub
    If i < 0 Then
      Me.DialogResult = DialogResult.Cancel
      Me.Close()
      Exit Sub
    End If

    saver.sourceFilename = frmMain.mView.originalFileName

    Me.Cursor = Cursors.WaitCursor
    If frmMain.mView.FloaterBitmap IsNot Nothing Then ' save the floater only
      setSaveParms(saver, frmMain.mView.FloaterBitmap, frmMain.mView.pComments)
      msg = saver.write(frmMain.mView.FloaterBitmap, fName, True)
    Else  ' save the entire image
      setSaveParms(saver, frmMain.mView.Bitmap, frmMain.mView.pComments)
      msg = saver.write(frmMain.mView.Bitmap, fName, True, frmMain.mView.pageBmp)
    End If
    Me.Cursor = Cursors.Default

    If msg <> "" Then
      MsgBox(msg)
    Else
      frmMain.mView.picName = fName
      frmMain.mView.picPage = ""
      frmMain.mView.Text = Path.GetFileName(fName)
      frmMain.mView.picModified = False
      AddMruFile(fName)
      saveParms(fName)
      Me.DialogResult = DialogResult.OK
      Me.Close()
    End If

  End Sub

  Sub saveFromExplorer(ByVal fName As String)

    Dim x As Double
    Dim msg As String = ""
    Dim overwriteFlag As String = ""
    Dim pview As New pViewer
    Dim pComments As List(Of PropertyItem)
    Dim bmpFrames As New List(Of Bitmap)

    Dim saver As New ImageSave

    If askOverwrite(fName, False, overwriteFlag) < 1 Then
      Me.Cursor = Cursors.Default
      Exit Sub ' don't overwrite
    End If

    picinfo = getPicinfo(currentpicPath, True)

    If Not picinfo.hasPages Then
      Using bmp As Bitmap = readBitmap(currentpicPath, msg)
        If bmp Is Nothing Then
          MsgBox("Oops!  Couldn't read " & currentpicPath & crlf & msg, MsgBoxStyle.OkOnly)
          Exit Sub
        End If
        bmpFrames.Add(bmp.Clone)
      End Using
    Else
      bmpFrames = readMultiframeBitmap(currentpicPath, msg)
      If bmpFrames Is Nothing OrElse bmpFrames.Count = 0 Then
        MsgBox("Oops!  Couldn't read " & currentpicPath & crlf & msg, MsgBoxStyle.OkOnly)
        Exit Sub
      End If
    End If

    pComments = readPropertyItems(currentpicPath)
    JPGQuality = nmQuality.Value

    ' resize info
    Xres = 0 : Yres = 0
    If chkResize.Checked Then
      If checknumber(txXres.Text, 1, MaxRes, x) Then
        Xres = x
      Else
        MsgBox("Invalid horizontal resolution.", MsgBoxStyle.OkOnly)
        txXres.Select()
        Exit Sub
      End If
      If checknumber(txYres.Text, 1, MaxRes, x) Then
        Yres = x
      Else
        MsgBox("Invalid vertical resolution.", MsgBoxStyle.OkOnly)
        txYres.Select()
        Exit Sub
      End If
    End If

    setSaveParms(saver, bmpFrames(0), pComments)
    saver.sourceFilename = currentpicPath

    Me.Cursor = Cursors.WaitCursor
    msg = saver.write(bmpFrames(0), fName, True, bmpFrames)
    Me.Cursor = Cursors.Default

    If msg <> "" Then
      MsgBox(msg)
    Else
      Me.Cursor = Cursors.Default
      saveParms(fName)
      Me.DialogResult = DialogResult.OK
      Me.Close()
    End If

  End Sub

  Sub setSaveParms(saver As ImageSave, qImage As Bitmap, pComments As List(Of PropertyItem))
    ' set the parameters to save the image with imageSave class

    If Xres > qImage.Width And Yres > qImage.Height Then
      saver.saveSize = Size.Empty
    Else
      saver.saveSize = New Size(Xres, Yres)
    End If
    saver.Quality = nmQuality.Value
    saver.saveExif = chkExif.Checked
    saver.compressionAmount = nmPngCompression.Value
    saver.PngIndexed = chkPngIndexed.Checked
    If pComments IsNot Nothing Then
      saver.pComments = New List(Of PropertyItem)
      saver.pComments.AddRange(pComments)
    End If

  End Sub


  Private Sub frmSaveAs_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim i, k As Integer
    Dim strpath As String
    Dim strName As String
    Dim fType As String

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Loading = True
    cmbFiletype.SelectedIndex = -1

    fType = iniSaveFiletype
    If callingForm Is frmExplore Then
      OriginalFName = currentpicPath
    ElseIf callingForm Is frmMain Then
      OriginalFName = frmMain.mView.picName
      If OriginalFName <> "" Then fType = Path.GetExtension(OriginalFName)
    End If

    nix = -1
    For i = 0 To fmtCommon.Count - 1
      If fmtCommon(i).isWritable Then
        nix = nix + 1
        ixFmt(nix) = i
        cmbFiletype.Items.Insert(nix, fmtCommon(i).Description & " (" & fmtCommon(i).Ext & ")")
        If cmbFiletype.SelectedIndex < 0 Then
          If eqstr(fmtCommon(i).ID, fType) Then cmbFiletype.SelectedIndex = nix '' multiple ext
          If eqstr(fmtCommon(i).ID, ".jpg") Then k = nix ' default to .jpg
        End If
      End If
    Next i

    If cmbFiletype.SelectedIndex < 0 Then cmbFiletype.SelectedIndex = k

    Me.Height = Me.Height - ((fmPng.Location.Y + fmPng.Size.Height) - (fmResize.Location.Y + fmResize.Size.Height))
    If callingForm IsNot frmExplore Then Me.Height = Me.Height - ((fmResize.Location.Y + fmResize.Size.Height) - (cmdCancel.Location.Y + cmdCancel.Size.Height))
    fmPng.Left = fmJpg.Left : fmPng.Top = fmJpg.Top

    EnableOptions()

    nmPngCompression.Value = iniPngCompression
    chkPngIndexed.Checked = iniPngIndexed

    nmQuality.Value = iniJpgQuality

    lbCompression.Text = "0 is highest compression and lowest quality." & crlf & _
          "100 is highest quality and lowest compression." & crlf & "95 is a good value for most applications."

    If callingForm Is frmExplore Then ' called from Explorer -- allow resize
      If qImage Is Nothing Then ' have not read the file yet
        picinfo = getPicinfo(currentpicPath, False)
        OriginalXres = picinfo.Width
        OriginalYres = picinfo.Height
      Else
        OriginalXres = qImage.Width
        OriginalYres = qImage.Height
      End If

      fmResize.Visible = True
      chkResize.Checked = iniSaveResize
      If chkResize.Checked Then
        lbXres.Enabled = True
        lbYres.Enabled = True
        lbPct.Enabled = True
        txXres.Enabled = True
        txYres.Enabled = True
        txPct.Enabled = True
        txPct.Text = Format(iniSavePct, "###0.0") & "%"
        realPct = iniSavePct
        txXres.Text = CStr(Round(realPct * OriginalXres / 100))
        txYres.Text = CStr(Round(realPct * OriginalYres / 100))
        pctChanged = False
        'pctChanged = true
        'txtPct_lostfocus
      Else
        lbXres.Enabled = False
        lbYres.Enabled = False
        lbPct.Enabled = False
        txXres.Enabled = False
        txYres.Enabled = False
        txPct.Enabled = False
        txXres.Text = CStr(OriginalXres)
        txYres.Text = CStr(OriginalYres)
        pctChanged = False
        txPct.Text = "100%"
      End If
    Else
      fmResize.Visible = False
    End If

    If OriginalFName <> "" Then strName = Path.GetFileName(OriginalFName) Else strName = ""
    i = InStr(strName, ".")
    If i <= 0 Then i = Len(strName) + 1
    If strName <> "" Then strName = vb.Left(strName, i - 1) & fmtCommon(ixFmt(cmbFiletype.SelectedIndex)).ID

    If iniSavePath = "" Then
      strpath = iniExplorePath
    Else
      strpath = iniSavePath
    End If
    If strpath <> "" Then If vb.Right(strpath, 1) <> "\" Then strpath = strpath & "\"
    txFileName.Text = strpath & strName

    Loading = False

  End Sub

  Private Sub txtFilename_Enter(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles txFileName.Enter
    Dim tx As TextBox
    tx = Sender
    tx.SelectionStart = 0
    tx.SelectionLength = Len(tx.Text)
  End Sub

  Private Sub txtFileName_Leave(ByVal Sender As Object, ByVal e As EventArgs) Handles txFileName.Leave

    Dim s As String
    Dim i As Integer

    CheckExt() ' make sure file extension matches format type
    Try
      s = Path.GetDirectoryName(txFileName.Text)
      i = CheckFolder(s, True)
    Catch ex As Exception
      s = ""
    End Try

  End Sub

  Private Sub txtPct_TextChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles txPct.TextChanged
    If Loading Then Exit Sub
    pctChanged = True

  End Sub

  Private Sub txtXres_Leave(ByVal Sender As Object, ByVal e As EventArgs) Handles txXres.Leave

    Dim x As Double

    If IsNumeric(txXres.Text) Then
      x = Val(txXres.Text)
      If x < 1 Or x > MaxRes Then x = Xres
      realPct = x / OriginalXres * 100
      txPct.Text = Format(realPct, "###0.0") & "%"
      txYres.Text = CStr(Round(OriginalYres / OriginalXres * x))
    Else
      txPct.Text = ""
    End If
    pctChanged = False

  End Sub
  Private Sub txtPct_Leave(ByVal Sender As Object, ByVal e As EventArgs) Handles txPct.Leave

    Dim i As Short
    Dim strTmp As String

    If Not pctChanged Then Exit Sub
    pctChanged = False

    i = InStr(txPct.Text, "%")
    If i > 0 Then
      strTmp = vb.Left(txPct.Text, i - 1)
    Else
      strTmp = txPct.Text
    End If

    If IsNumeric(strTmp) Then
      realPct = CDbl(strTmp)
      txXres.Text = CStr(Round(realPct * OriginalXres / 100))
      txYres.Text = CStr(Round(realPct * OriginalYres / 100))
    End If

  End Sub
  Private Sub txtYres_Leave(ByVal Sender As Object, ByVal e As EventArgs) Handles txYres.Leave

    Dim x As Double

    If IsNumeric(txYres.Text) Then
      x = Val(txYres.Text)
      If x < 1 Or x > MaxRes Then x = Yres
      realPct = x / OriginalYres * 100
      txPct.Text = Format(realPct, "####.0") & "%"
      txXres.Text = CStr(Round(OriginalXres / OriginalYres * x))
    Else
      txPct.Text = ""
    End If

    pctChanged = False

  End Sub

  Private Sub txtXres_Enter(ByVal Sender As Object, ByVal e As EventArgs) Handles txXres.Enter

    txXres.SelectionStart = 0
    txXres.SelectionLength = Len(txXres.Text)

  End Sub
  Private Sub txtPct_Enter(ByVal Sender As Object, ByVal e As EventArgs) Handles txPct.Enter

    txPct.SelectionStart = 0
    txPct.SelectionLength = Len(txPct.Text)

  End Sub

  Private Sub txtYres_Enter(ByVal Sender As Object, ByVal e As EventArgs) Handles txYres.Enter

    txYres.SelectionStart = 0
    txYres.SelectionLength = Len(txYres.Text)

  End Sub

  Sub CheckExt()

    Dim i As Short
    Dim ext As String
    Dim fName As String

    fName = Trim(txFileName.Text)

    ext = Path.GetExtension(fName)

    If fName <> "" And vb.Right(fName, 1) <> "\" Then
      If ext = "" Then
        txFileName.Text = txFileName.Text & fmtCommon(ixFmt(cmbFiletype.SelectedIndex)).ID
      Else
        If ext.ToLower <> fmtCommon(ixFmt(cmbFiletype.SelectedIndex)).ID Then
          For i = 0 To cmbFiletype.Items.Count
            If ext.ToLower = fmtCommon(ixFmt(i)).ID Then
              cmbFiletype.SelectedIndex = i
              Exit For
            End If
          Next i
        End If
      End If
    End If

  End Sub

  Sub EnableOptions()

    If cmbFiletype.SelectedIndex < 0 Then Exit Sub

    Select Case fmtCommon(ixFmt(cmbFiletype.SelectedIndex)).ID

      Case ".jpg"
        fmJpg.Visible = True
        fmPng.Visible = False

      Case ".png"
        fmJpg.Visible = False
        fmPng.Visible = True

      Case Else
        fmJpg.Visible = False
        fmPng.Visible = False

    End Select

  End Sub

  Private Sub chkPNGIndexed_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs)
    If Loading Then Exit Sub
    iniPngIndexed = chkPngIndexed.Checked
  End Sub

  Sub saveParms(ByVal fName As String)

    iniSavePath = Path.GetDirectoryName(fName)
    iniSaveFiletype = fmtCommon(ixFmt(cmbFiletype.SelectedIndex)).ID

    If callingForm Is frmExplore Then
      iniSaveResize = chkResize.Checked
      If iniSaveResize = 1 Then
        iniSaveXSize = Xres
        iniSaveYSize = Yres
        iniSavePct = Xres / OriginalXres * 100
      End If
    End If

  End Sub

End Class
Imports vb = Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.IO
Imports ImageMagick

Imports System.Data
Imports MySql.Data.MySqlClient

Public Class frmWebPage
  Inherits Form

  Dim imgPath As New List(Of String)
  Dim imgName As New List(Of String)
  Dim imgComments As New List(Of String) ' from exif
  Dim imgDate As New List(Of Date) ' from exif
  Dim imgLatLon As New List(Of String) ' from exif
  Dim imgXLat As New List(Of Double)
  Dim imgXLon As New List(Of Double)
  Dim imgAltitude As New List(Of String) ' from exif
  Dim imgX As New List(Of Integer) ' from picinfo
  Dim imgY As New List(Of Integer) ' from picinfo
  Dim ix As New List(Of Integer)  ' sort index

  Dim webCaption As New List(Of String) ' user-entered data

  Dim nimages As Integer ' number of images - 1 (0-based)
  Dim nColumns As Integer
  Dim lastCaption As Integer
  Dim fPath As String

  Dim htmFilePath As String
  Public webPath As String

  Dim boxXsize As Double
  Dim boxYsize As Double
  Dim xoff As Double

  Dim thumbXres As Double
  Dim thumbYres As Double

  ' used in fmSaveVerify
  Public Xres As Double
  Public Yres As Double
  Public JPGQuality As Integer
  Public webResize As Integer ' -1 = no save, 0 = save without resize, 1 = save with resize
  Public ipic As Integer  ' for frmFullScreen

  Dim Loading As Boolean = True
  Dim Processing As Boolean = False
  Dim UTCoff As TimeSpan

  Dim proc As Process
  Dim WithEvents procTimer As New Timer


  Private Sub cmdDefaults_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdDefaults.Click

    SetWebDefaults()
    GetSettings()

  End Sub

  Private Sub cmdDel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdDel.Click

    Dim iFrom As Integer
    Dim i As Integer

    iFrom = lstFiles.SelectedIndex
    If lstFiles.Items.Count = 0 Then Exit Sub

    For i = iFrom To lstFiles.Items.Count - 2
      ix(i) = ix(i + 1)
    Next i

    lstFiles.Items.RemoveAt(lstFiles.SelectedIndex)

    nimages = nimages - 1

    picWeb.Refresh()

    If iFrom <= lstFiles.Items.Count - 1 Then
      lstFiles.SetSelected(iFrom, True)
    ElseIf lstFiles.Items.Count >= 1 Then
      lstFiles.SetSelected(lstFiles.Items.Count - 1, True)
    End If

  End Sub

  Private Sub cmdMove_Click(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles cmdMoveUp.Click, cmdMoveDown.Click

    Dim i As Integer
    Dim iTo As Integer
    Dim iFrom As Integer
    Dim s As String

    iFrom = lstFiles.SelectedIndex

    If Sender Is cmdMoveUp Then iTo = iFrom - 1 Else iTo = iFrom + 1

    If iTo < 0 Or iTo > nimages Then Exit Sub

    Processing = True
    ' swap ix pointers.
    i = ix(iFrom)
    ix(iFrom) = ix(iTo)
    ix(iTo) = i

    ' swap list captions.
    lstFiles.SelectedIndex = Nothing
    s = lstFiles.Items(iFrom)
    lstFiles.Items(iFrom) = lstFiles.Items(iTo)
    lstFiles.Items(iTo) = s

    ' swap 7 variables.
    lstFiles.SelectedIndex = iTo
    picWeb.Refresh()
    Processing = False


  End Sub

  Private Sub cmdSave_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdSave.Click

    SaveWebPage()
    iniWebResize = webResize
    If webPath <> "" Then iniSavePath = webPath

    If webResize > 0 Then ' save defaults
      iniWebImageX = Xres
      iniWebImageY = Yres
    End If

  End Sub

  Private Sub cmdClose_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdClose.Click
    Me.DialogResult = DialogResult.OK
    Me.Close()
  End Sub

  Private Sub cmdSort_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdSort.Click

    Dim i As Integer

    MergeSort(imgDate, ix, 0, nimages)

    lstFiles.Items.Clear()

    lstFiles.Visible = False
    For i = 0 To nimages
      lstFiles.Items.Insert(i, imgName(ix(i)))
    Next i
    If nimages >= 0 Then lstFiles.SelectedIndex = 0
    lstFiles.Visible = True

  End Sub

  Private Sub cmdView_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdView.Click

    Dim s As String
    Dim i As Integer
    Dim c As Char

    s = "file:///"
    For i = 1 To Len(htmFilePath)
      c = htmFilePath.Chars(i - 1)
      Select Case c
        Case " "
          s = s & "%20"
        Case "\"
          s = s & "/"
        Case Else
          s = s & c
      End Select
    Next i

    Try
      proc = System.Diagnostics.Process.Start(s)  ' show the url in a browser
    Catch ex As Exception
      MsgBox("Couldn't launch the browser." & crlf & ex.Message)
    End Try

  End Sub

  Private Sub pview_mouseClick(ByVal Sender As Object, ByVal e As EventArgs) Handles pView.MouseClick

    If pView.Bitmap Is Nothing Then Exit Sub
    'If rview.bitmap.Width < 600 Then
    '  Me.Cursor = Cursors.WaitCursor
    '  showPicture(fPath, rview, False, Nothing)
    '  Me.Cursor = Cursors.Default
    '  Exit Sub
    '  End If

    callingForm = Me
    ipic = lstFiles.SelectedIndex
    qImage = pView.Bitmap.Clone
    currentpicPath = fPath
    Using frm As New frmFullscreen
      frm.ShowDialog()
    End Using
    clearBitmap(qImage)
    Me.Select()

  End Sub

  Private Sub fmWebPage_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim exif As ExifProfile = Nothing

    Dim j As Integer

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Loading = True
    webResize = 0

    If iniWebConvertUTCtoLocal > 0 Then
      UTCoff = TimeZoneInfo.Local.BaseUtcOffset  ' Add UTCoff to UTC to get local time.
    Else
      UTCoff = New TimeSpan(0)
    End If

    GetSettings()

    Progressbar1.Value = Progressbar1.Minimum

    picWeb.Refresh()

    lstFiles.Items.Clear()

    lstFiles.Visible = False
    nimages = tagPath.Count - 1 ' nimages is zero based

    For j = 0 To tagPath.Count - 1
      imgPath.Add(tagPath(j))
      imgName.Add(Path.GetFileName(imgPath(j)))
      lstFiles.Items.Insert(j, imgName(j))
      webCaption.Add("")
      imgComments.Add("")
      imgDate.Add(Nothing)
      imgLatLon.Add("")
      imgXLat.Add(0)
      imgXLon.Add(0)
      imgAltitude.Add("")
      imgX.Add(0)
      imgY.Add(0)
      ix.Add(j)
    Next j

    getDescriptions()

    lstFiles.Visible = True

    lbTagged.Text = nimages + 1 & " photos are tagged."

    htmFilePath = ""
    cmdView.Enabled = False
    chkHtmlOnly.Checked = False
    Progressbar1.Visible = False

    If nimages >= 0 Then lstFiles.SelectedIndex = 0

    thumbXres = iniWebThumbX
    thumbYres = iniWebThumbY
    Loading = False

    If nimages < 0 Then
      MsgBox("No photos are tagged to be included in the web page.", MsgBoxStyle.OkOnly)
      Me.Close()
      Exit Sub
    End If

    If lstFiles.SelectedIndex >= 0 Then
      Me.Cursor = Cursors.WaitCursor
      fitFile(imgPath(ix(lstFiles.SelectedIndex)), pView)
      Me.Cursor = Cursors.Default
    End If
  End Sub

  Private Sub GetSettings()

    Xres = iniWebImageX
    Yres = iniWebImageY
    webResize = iniWebResize
    nColumns = iniWebnColumns
    JPGQuality = iniJpgQuality
    webPath = iniWebPath

  End Sub

  Private Sub lstFiles_SelectedIndexChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles lstFiles.SelectedIndexChanged

    If lstFiles.SelectedIndex >= lstFiles.Items.Count - 1 Then
      cmdMoveDown.Enabled = False
    Else
      cmdMoveDown.Enabled = True
    End If

    If lstFiles.SelectedIndex <= 0 Then
      cmdMoveUp.Enabled = False
    Else
      cmdMoveUp.Enabled = True
    End If

    If Loading Or Processing Then Exit Sub

    If lstFiles.SelectedIndex < 0 Or lstFiles.SelectedIndex > lstFiles.Items.Count - 1 Then Exit Sub
    Me.Cursor = Cursors.WaitCursor
    fitFile(imgPath(ix(lstFiles.SelectedIndex)), pView)
    Me.Cursor = Cursors.Default
    fPath = imgPath(ix(lstFiles.SelectedIndex)) ' save path for fullscreen

    If (pView.Bitmap IsNot Nothing) Then
      lbStatus.Text = "Image: " & imgName(ix(lstFiles.SelectedIndex)) & ",  Resolution: " & pView.Bitmap.Width & " x " & pView.Bitmap.Height
    Else
      lbStatus.Text = ""
    End If

    txCaption.Text = webCaption(ix(lstFiles.SelectedIndex))
    lastCaption = lstFiles.SelectedIndex

    picWeb.Refresh()

  End Sub

  Sub drawboxes(ByRef g As Graphics)

    Dim j, i, k As Integer

    Dim nrows As Integer
    Dim bHeight, bWidth As Double
    Dim r As RectangleF

    ' update pic box
    nrows = Ceiling((nimages + 1) / nColumns)
    bHeight = nrows * 3.2
    bWidth = nColumns * 4.2

    r = g.VisibleClipBounds

    If bHeight / bWidth > (r.Height) / (r.Width) Then
      bWidth = (r.Height) / bHeight * bWidth
      bHeight = (r.Height)
      xoff = (r.Width - bWidth) / 2
    Else
      bHeight = (r.Width) / bWidth * bHeight
      bWidth = (r.Width)
      xoff = 0
    End If

    boxXsize = bWidth / nColumns
    boxYsize = bHeight / nrows

    Using gPen As New Pen(picWeb.ForeColor, 1), _
      gbrush As New SolidBrush(Color.Black)

      k = 0

      For i = 0 To nrows - 1
        r.Y = (i + 0.1) * boxYsize
        r.Height = 0.8 * boxYsize
        For j = 0 To nColumns - 1
          r.X = xoff + (j + 0.1) * boxXsize
          r.Width = 0.8 * boxXsize
          If k = lstFiles.SelectedIndex Then
            gbrush.Color = Color.FromArgb(255, 50, 50, 50)
            g.FillRectangle(gbrush, r)
            g.DrawRectangle(gPen, Rectangle.Round(r))
          Else
            gbrush.Color = Color.FromArgb(255, 200, 200, 200)
            g.FillRectangle(gbrush, r)
            g.DrawRectangle(gPen, Rectangle.Round(r))
          End If
          k = k + 1
          If k >= lstFiles.Items.Count Then Exit For
        Next j
      Next i
    End Using

  End Sub

  Private Sub picWeb_MouseDown(ByVal Sender As Object, ByVal e As MouseEventArgs) Handles picWeb.MouseDown

    Dim x As Single
    Dim y As Single

    x = e.X
    y = e.Y

    Dim row As Integer
    Dim column As Integer
    Dim i As Integer

    If e.Button = MouseButtons.Right Then Exit Sub ' ignore right button

    webCaption(ix(lastCaption)) = txCaption.Text.Trim ' for some reason, txtcaption.lostfocus doesn't fire

    row = Int(y / boxYsize)
    column = Int((x - xoff) / boxXsize)

    i = row * nColumns + column
    If i <= lstFiles.Items.Count - 1 Then
      lstFiles.SelectedIndex = i
      txCaption.Text = webCaption(ix(lstFiles.SelectedIndex))
      lastCaption = lstFiles.SelectedIndex
    End If

  End Sub

  Private Sub txtCaption_KeyDown(ByVal Sender As Object, ByVal e As KeyEventArgs) Handles txCaption.KeyDown

    If e.KeyCode = 9 Then
      e.Handled = True
      lstFiles.Select()
    End If

  End Sub

  Private Sub txtCaption_Leave(ByVal Sender As Object, ByVal e As EventArgs) Handles txCaption.Leave

    ' the variable lastCaption is used because listindex gets out of sync
    webCaption(ix(lastCaption)) = txCaption.Text.Trim

  End Sub
  Sub SaveWebPage()

    Dim i As Integer
    Dim result As DialogResult

    saveDialog1.Filter = "HTML file (*.htm)|*.htm;*.html"
    saveDialog1.OverwritePrompt = True
    saveDialog1.FileName = ""
    saveDialog1.InitialDirectory = webPath

    Try
      result = saveDialog1.ShowDialog()
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    Me.Select()

    If result <> DialogResult.OK OrElse Len(saveDialog1.FileName) = 0 Then Exit Sub ' cancel or error

    Me.Cursor = Cursors.WaitCursor

    htmFilePath = saveDialog1.FileName
    webPath = Path.GetDirectoryName(htmFilePath)

    ' progress only if 0 return code
    Progressbar1.Visible = True
    lbStatus.Visible = False
    Progressbar1.Minimum = 0
    If nimages > 0 Then Progressbar1.Maximum = nimages Else Progressbar1.Maximum = 1
    i = 0
    If Not chkHtmlOnly.Checked Then i = SaveThumbnails(webPath)
    If i = 0 Then
      Progressbar1.Value = 0
      result = SaveHtml(htmFilePath)
      If result = DialogResult.OK Then
        Progressbar1.Value = 0
        If Not chkHtmlOnly.Checked Then i = SaveImages(webPath)
      End If
    End If
    Progressbar1.Visible = False
    lbStatus.Visible = True

    If i = 0 Then cmdView.Enabled = True

    Me.Cursor = Cursors.Default
    'Turn on automated behavior
    pView.setBitmap(Nothing)
    lstFiles_SelectedIndexChanged(lstFiles, New EventArgs())

  End Sub
  Function SaveThumbnails(ByRef strpath As String) As Short

    Dim strName As String

    Dim i, k As Integer

    Dim xSize As Integer
    Dim ySize As Integer
    Dim gBitmap As Bitmap = Nothing
    Dim picInfo As pictureInfo
    Dim mResult As MsgBoxResult
    Dim msg As String = ""
    Dim overwriteFlag As String = ""
    Dim saver As New ImageSave

    OverwriteResponse = "" ' parameter for overwrite question
    SaveThumbnails = 0

    ' save thumbnails at destination
    For i = 0 To nimages
      If i <= Progressbar1.Maximum Then Progressbar1.Value = i

      strName = "small_" & Path.ChangeExtension(imgName(ix(i)), ".jpg")
      strOverWriteFile = strpath & "\" & strName

      k = askOverwrite(strOverWriteFile, True, overwriteFlag)
      If k = -1 Then ' cancel
        Exit For
      ElseIf k = 1 Then ' OK to write the file
        picInfo = getPicinfo(imgPath(ix(i)), 1)
        If picInfo.isNull Then
          xSize = thumbXres
          ySize = thumbYres
        End If

        thumbXres = iniWebThumbX
        thumbYres = iniWebThumbY

        If thumbXres <= 0 Then thumbXres = 192
        If thumbYres <= 0 Then thumbYres = 192

        If Not picInfo.isNull Then
          If thumbYres / thumbXres > picInfo.Aspect Then
            ySize = Round(thumbXres * picInfo.Aspect)
            xSize = thumbXres
          Else
            xSize = Round(thumbYres / picInfo.Aspect)
            ySize = thumbYres
          End If
        End If

        Try
          Using bmp As Bitmap = readBitmap(imgPath(ix(i)), msg)
            If bmp IsNot Nothing Then
              gBitmap = New Bitmap(xSize, ySize, PixelFormat.Format32bppPArgb)
              Using g As Graphics = Graphics.FromImage(gBitmap)
                g.InterpolationMode = InterpolationMode.High
                g.DrawImage(bmp, New Rectangle(0, 0, xSize, ySize))
              End Using
            End If
          End Using
        Catch ex As Exception
          msg = ex.Message
        End Try

        If gBitmap Is Nothing Or msg <> "" Then
          mResult = MsgBox("Woops!  File Error reading " & imgPath(ix(i)) & "." & crlf & msg, MsgBoxStyle.OkCancel)
          If mResult = MsgBoxResult.Cancel Then
            SaveThumbnails = MsgBoxResult.Cancel
            Exit For
          End If

        Else  ' loaded OK
          saver.Quality = JPGQuality
          saver.saveExif = False
          saver.Format = MagickFormat.Jpg
          msg = saver.write(gBitmap, strOverWriteFile, True)

          If msg <> "" Then
            mResult = MsgBox("This file could not be saved: " & strOverWriteFile & crlf & msg, MsgBoxStyle.OkCancel)
            If mResult = MsgBoxResult.Cancel Then
              SaveThumbnails = MsgBoxResult.Cancel
              Exit For
            End If
          End If
        End If ' loaded OK
        Application.DoEvents()
      End If  ' OK to write

    Next i

    clearBitmap(gBitmap)

  End Function
  Function SaveHtml(ByRef fName As String) As Short

    Dim cellwidth As Integer
    Dim i As Integer
    Dim j As Integer
    Dim k As Integer
    Dim nrows As Integer
    Dim red As Integer
    Dim green As Integer
    Dim blue As Integer
    Dim wForeColor As String
    Dim wBackColor As String
    Dim wFrameColor As String
    Dim s As String
    Dim strName As String
    Dim sComment As String
    Dim d As Date
    Dim x As Double
    Dim sf As New List(Of String)
    Dim sfl As String
    Dim alt As String
    Dim img As Bitmap
    Dim locale As String = ""
    Dim county As String = ""
    Dim state As String = ""
    Dim country As String = "" ' for gps location

    ' bug stuff
    Dim ds As DataSet
    Dim pic As pixClass

    nrows = Int((nimages + 1) / nColumns + 0.999999)

    wBackColor = ColorTranslator.ToHtml(iniWebBackColor)
    wForeColor = ColorTranslator.ToHtml(iniWebForeColor)

    If iniWebForeColor.R > iniWebBackColor.R Then x = 1 / 3 Else x = 2 / 3
    red = Min(iniWebForeColor.R, iniWebBackColor.R) + Abs(CInt(iniWebForeColor.R) - CInt(iniWebBackColor.R)) * x
    green = Min(iniWebForeColor.G, iniWebBackColor.G) + Abs(CInt(iniWebForeColor.G) - CInt(iniWebBackColor.G)) * x
    blue = Min(iniWebForeColor.B, iniWebBackColor.B) + Abs(CInt(iniWebForeColor.B) - CInt(iniWebBackColor.B)) * x
    wFrameColor = "#" & vb.Right("00" & Hex(red), 2) & vb.Right("00" & Hex(green), 2) & vb.Right("00" & Hex(blue), 2)

    sf = New List(Of String)

    sf.Add("<!DOCTYPE html>")
    sf.Add("<html>")
    sf.Add("<head>")
    sf.Add("<meta charset=""utf-8"">")
    sf.Add("<title>" & txTitle.Text & "</title>")
    sf.Add("<meta name=""description"" content=""" & txTitle.Text & """>")
    sf.Add("<style>")
    sf.Add("")
    sf.Add("body {")
    sf.Add("  background: " & wBackColor & ";")
    sf.Add("  color: " & wForeColor & ";")
    sf.Add("  text-align: center;")
    If iniWebCaptionSize > 0 Then
      sf.Add("  font-size:" & iniWebCaptionSize & "pt;")
    End If
    sf.Add("  font-family: Arial, Helvetica, sans-serif;")
    sf.Add("}")
    sf.Add("a:link { color: " & wForeColor & " }")
    sf.Add("a:visited { color: " & wForeColor & " }")
    sf.Add("h1 {")
    sf.Add("  text-align: center;")
    sf.Add("  font-family: Arial, Helvetica, sans-serif;")
    If iniWebTitleSize > 0 Then
      sf.Add("  font-size:" & iniWebTitleSize & "pt;")
    End If
    sf.Add("}")
    sf.Add("td {")
    sf.Add("  border: " & iniWebTableBorder & "px " & wFrameColor & " solid;")
    sf.Add("  vertical-align: top;")
    cellwidth = Round(100 / nColumns)
    sf.Add("  width: " & cellwidth & "%;")
    sf.Add("  padding: " & iniWebCellPadding & "px;")
    sf.Add("  border-spacing: " & iniWebCellSpacing & "px;")
    sf.Add("}")
    sf.Add("table { ")
    sf.Add("  border-collapse:collapse;")
    sf.Add("  border-width:0;")
    sf.Add("  margin:0 auto; ")
    i = (iniWebThumbX + iniWebCellPadding * 2) * nColumns + (iniWebCellSpacing + iniWebTableBorder) * (nColumns + 1)
    sf.Add("  width:" & i & "px;")
    sf.Add("  }")
    sf.Add("img {border:0; margin:3px; box-shadow: 4px 4px 7px #222;}")
    sf.Add(".heading {")
    sf.Add("  width:60%;")
    sf.Add("  margin:0 auto;")
    sf.Add("}")
    sf.Add(".caption {")
    sf.Add("  font-family: Arial,Helvetica,sans-serif;")
    sf.Add("  line-size: normal;")
    If iniWebCaptionSize > 0 Then
      sf.Add("  font-size:" & iniWebCaptionSize & "pt;")
    End If
    sf.Add("  text-align:" & iniWebCaptionAlign & ";")
    sf.Add("}")
    sf.Add("</style>")

    If Trim(iniWebGoogleAnalytics) <> "" Then
      sf.Add("<script>")
      sf.Add("  var _gaq = _gaq || [];")
      sf.Add("  _gaq.push(['_setAccount', '" & iniWebGoogleAnalytics & "']);")
      sf.Add("  _gaq.push(['_trackPageview']);")
      sf.Add("  (function() {")
      sf.Add("    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;")
      sf.Add("    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';")
      sf.Add("    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);")
      sf.Add("  })();")
      sf.Add("</script>")
    End If

    sf.Add("</head>")

    sf.Add("<body><div>")
    sf.Add("<div class=heading>")
    sf.Add("<h1>" & txTitle.Text & "</h1>")
    If txSubtitle.Text.Trim <> "" Then
      sf.Add(txSubtitle.Text & "<br><br>")
    End If
    sf.Add("</div>")

    sf.Add("<table>")
    k = 0
    For j = 1 To nrows
      sf.Add("<tr>")
      For i = 1 To nColumns
        sf.Add("<td>")

        If k <= nimages Then ' add column content
          ' title, for tooltip
          s = Trim(webCaption(ix(k)))
          If InStr(s, crlf) > 0 Then s = Trim(Mid(s, 1, InStr(s, crlf) - 1))
          If s = "" Then s = imgName(ix(k)) & ", " & imgX(ix(k)) & "x" & imgY(ix(k)) ' title, for tooltip
          alt = imgName(ix(k))
          sfl = "<a href=""" & imgName(ix(k)) & """"
          If iniWebTarget = 1 Then sfl &= " target=""_blank"""
          If iniWebGoogleEvents And Len(iniWebGoogleAnalytics) > 0 Then
            sfl &= " onclick=""ga('send', 'event', 'Photo', 'Download', '" & txTitle.Text & " - " & imgName(ix(k)) & "');"""
          End If
          sf.Add(sfl & ">")

          strName = "small_" & Path.ChangeExtension(imgName(ix(k)), ".jpg")
          Try ' read thumbnail to get width and height
            img = Bitmap.FromFile(Path.GetDirectoryName(fName) & "\" & strName)
          Catch ex As Exception
            img = Nothing
          End Try
          If img IsNot Nothing Then
            sf.Add("<img src=""" & strName & """ alt=""" & alt & """ title=""" & s & """ width=""" & img.Width & """ height=""" & img.Height & """></a><br>")
            img.Dispose()
          Else
            sf.Add("<img src=""" & strName & """ alt=""" & alt & """ title=""" & s & """></a><br>")
          End If

          sComment = ""

          If webCaption(ix(k)).Trim <> "" Then
            sComment = sComment & addBreaks(webCaption(ix(k)))
            If (imgDate(ix(k)) <> Nothing And chkCapDate.Checked) Or _
               (imgDate(ix(k)) <> Nothing And chkCapTime.Checked) Or _
               (Len(Trim(imgName(ix(k)))) <> 0 And chkCapName.Checked) Or _
               (Len(Trim(imgLatLon(ix(k)))) <> 0 And chkCapLatLon.Checked) Or _
               (Len(Trim(imgAltitude(ix(k)))) <> 0 And chkCapAltitude.Checked) Or _
               (Len(Trim(imgLatLon(ix(k)))) <> 0 And chkCapLocation.Checked) Or _
               (Len(Trim(imgLatLon(ix(k)))) <> 0 And chkCapMaplink.Checked) Or _
               (Trim(imgX(ix(k))) > 0 And chkCapResolution.Checked) Then
              sComment = sComment & "<br>"
            End If
          End If

          If chkCapDescription.Checked Then
            If imgComments(ix(k)) <> "" Then
              s = addBreaks(imgComments(ix(k)))
              sComment = sComment & s & "<br>"
            End If

            If useQuery Then ' add bug descriptions
              ds = getDS("select * from images, taxatable where filename = @parm1 and images.taxonid = taxatable.taxid", imgName(ix(k)))
              If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                pic = New pixClass(ds.Tables(0).Rows(0))
                s = getCaption(pic)
                sComment = sComment & s
              Else
                ds = getDS("select * from images, gbif.tax where filename = @parm1 and substring(images.taxonid, 2) = gbif.tax.taxid", imgName(ix(k)))
                If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                  pic = New pixClass(ds.Tables(0).Rows(0))
                  s = getCaption(pic)
                  sComment = sComment & s
                End If
              End If
            End If
          End If

          If (imgDate(ix(k)) <> Nothing And chkCapDate.Checked) Or _
             (imgDate(ix(k)) <> Nothing And chkCapTime.Checked) Or _
             (Len(Trim(imgName(ix(k)))) <> 0 And chkCapName.Checked) Or _
             (Len(Trim(imgLatLon(ix(k)))) <> 0 And chkCapLatLon.Checked) Or _
             (Len(Trim(imgAltitude(ix(k)))) <> 0 And chkCapAltitude.Checked) Or _
             (Len(Trim(imgLatLon(ix(k)))) <> 0 And chkCapLocation.Checked) Or _
             (Len(Trim(imgLatLon(ix(k)))) <> 0 And chkCapMaplink.Checked) Or _
             (Trim(imgX(ix(k))) > 0 And chkCapResolution.Checked) Then
            If sComment <> "" Then
              sComment = sComment & crlf & "<div style=""text-align:center; font-size:smaller""><br>"
            Else
              sComment = sComment & "<div style=""text-align:center; font-size:smaller"">"
            End If
          End If

          If imgDate(ix(k)) <> Nothing And (chkCapDate.Checked Or chkCapTime.Checked) Then
            s = ""
            d = imgDate(ix(k))
            x = nmTimeOffset.Value
            d = d.Add(New TimeSpan(x, 0, 0))

            If chkCapDate.Checked Then s = Format(d, "short date") & " "
            If chkCapTime.Checked Then s = s & Format(d, "short time")
            sComment = sComment & s & "<br>"
          End If

          If imgName(ix(k)) <> "" And chkCapName.Checked Then
            sComment = sComment & imgName(ix(k)) & "<br>"
          End If

          If imgX(ix(k)) > 0 And chkCapResolution.Checked Then
            sComment = sComment & imgX(ix(k)) & " x " & imgY(ix(k)) & "<br>"
          End If

          If imgLatLon(ix(k)) <> "" And chkCapLatLon.Checked Then
            sComment = sComment & imgLatLon(ix(k)) & "<br>"
          End If

          If imgAltitude(ix(k)) <> "" And chkCapAltitude.Checked Then
            sComment = sComment & imgAltitude(ix(k)) & "<br>"
          End If

          If imgLatLon(ix(k)) <> "" And chkCapLocation.Checked Then
            GPSLocate(imgLatLon(ix(k)), locale, county, state, country)
            If locale = "" And county = "" And state = "" And country = "" Then GPSLocate(imgLatLon(ix(k)), locale, county, state, country) ' sometimes it doesn't work.
            If locale = "" And county = "" And state = "" And country = "" Then GPSLocate(imgLatLon(ix(k)), locale, county, state, country)

            s = locale
            ' If county = "" Then s = locale Else s = "" ' skip town if there's a county
            If county <> "" Then
              If s <> "" Then s &= ", "
              s &= county
              If country = "USA" Or country = "" Then s &= " County"
            End If
            If state <> "" Then
              If s <> "" Then s &= ", "
              s &= state
            End If
            If country <> "" And country <> "USA" Then
              If s <> "" Then s &= ", "
              s &= country
            End If
            sComment &= crlf & s & "<br>"
          End If

          If imgLatLon(ix(k)) <> "" And chkCapMaplink.Checked Then
            s = imgLatLon(ix(k))
            s = s.Replace("°", " ")
            If s.Contains("""") Then s = s.Replace("'", " ") Else s = s.Replace("'", "")
            s = s.Replace("""", "")
            's = s & "(" & imgName(ix(k)) & ")"
            sComment = sComment & crlf &
              "<a href=""https://www.google.com/maps/place/" & s & "/@" &
              Format(imgXLat(ix(k)), "0.0#####") & "," & Format(imgXLon(ix(k)), "0.0#####") & ",14z/" &
              "data=!4m2!3m1!1s0x0:0x0!5m1!1e4"">-map-</a><br>"
            ' "<a href=""http://maps.google.com/maps?z=14&amp;t=h&amp;q=" & s & """ target=_blank title=""" & imgName(ix(k)) & """>-map-</a><br>" ' old google maps format
          End If
          If vb.Right(sComment, 6) = "<br>" Then sComment = vb.Left(sComment, Len(sComment) - 4)

          If (imgDate(ix(k)) <> Nothing And chkCapDate.Checked) Or _
             (imgDate(ix(k)) <> Nothing And chkCapTime.Checked) Or _
             (Len(Trim(imgName(ix(k)))) <> 0 And chkCapName.Checked) Or _
             (Len(Trim(imgLatLon(ix(k)))) <> 0 And chkCapLatLon.Checked) Or _
             (Len(Trim(imgAltitude(ix(k)))) <> 0 And chkCapAltitude.Checked) Or _
             (Len(Trim(imgLatLon(ix(k)))) <> 0 And chkCapLocation.Checked) Or _
             (Len(Trim(imgLatLon(ix(k)))) <> 0 And chkCapMaplink.Checked) Or _
             (Trim(imgX(ix(k))) > 0 And chkCapResolution.Checked) Then
            sComment = sComment & "</div>"
          End If

          k = k + 1
          If sComment <> "" Then
            sComment = "<div class=caption>" & sComment & "</div>"
            sf.Add(sComment)
          End If
        End If ' column content

        sf.Add("</td>")
      Next i

      sf.Add("</tr>")
    Next j

    sf.Add("</table>")
    sf.Add("<br><br><br>")

    sf.Add("</div></body>")
    sf.Add("</html>")

    SaveHtml = DialogResult.OK
    Try
      File.WriteAllLines(fName, sf.ToArray, UTF8bare)
    Catch ex As Exception
      MsgBox(ex.Message)
      SaveHtml = DialogResult.Cancel
    End Try

  End Function
  Function SaveImages(ByRef strpath As String) As Short

    Dim strToFile As String
    Dim i, j As Integer
    Dim x, y As Double
    Dim gBitmap As Bitmap = Nothing
    Dim mResult As MsgBoxResult
    Dim dResult As DialogResult
    Dim msg As String = ""
    Dim overwriteFlag As String = ""
    Dim saver As New ImageSave

    Using frm As New frmSaveVerify
      callingForm = Me
      dResult = frm.ShowDialog()
    End Using
    Me.Select()
    Me.Refresh()
    If dResult = DialogResult.No Then Return 0 ' don't save

    Me.Cursor = Cursors.WaitCursor

    If webResize > 0 Then
      x = Xres : y = Yres
    Else
      x = 0 : y = 0
    End If
    saver.saveSize = New Size(x, y)
    saver.Quality = JPGQuality

    For j = 0 To nimages
      If j <= Progressbar1.Maximum Then Progressbar1.Value = j
      strToFile = strpath & "\" & imgName(ix(j))

      i = askOverwrite(strToFile, True, overwriteFlag)
      If i < 0 Then ' cancel
        SaveImages = MsgBoxResult.Cancel
        Exit For
      ElseIf i = 0 Then
        gBitmap = Nothing ' don't overwrite
      Else ' read the image
        gBitmap = readBitmap(imgPath(ix(j)), msg)
        If gBitmap Is Nothing Then
          mResult = MsgBox("Error.  " & imgPath(ix(j)) & " could not be loaded. & crlf & msg", MsgBoxStyle.OkCancel)
          If mResult = MsgBoxResult.Cancel Then
            SaveImages(MsgBoxResult.Cancel)
            Exit For
          End If
        End If
      End If

      If gBitmap IsNot Nothing Then
        saver.sourceFilename = imgPath(ix(j))
        msg = saver.write(gBitmap, strToFile, True)
        '        If convertfile(strToFile, targetExt, x, y, JPGQuality, gBitmap, imgPath(ix(j)), pView) < 0 Then
        If msg <> "" Then
          SaveImages = MsgBoxResult.Cancel
          Exit For
        End If
      End If
      Application.DoEvents()
    Next j

    clearBitmap(gBitmap)

  End Function

  Sub getDescriptions()

    Dim i As Integer
    Dim s As String
    Dim d As Date
    Dim picinfo As pictureInfo = Nothing
    Dim Location As String = ""
    Dim Altitude As String = ""
    Dim pcomments As List(Of PropertyItem)
    Dim xLat, xLon As Double
    Dim iAltitude As Integer

    For i = 0 To nimages
      ' description
      imgComments(ix(i)) = readPhotoDescription(imgPath(ix(i))).Trim
      ' old Photo Mud and some cameras put in their own comments
      If eqstr(imgComments(ix(i)), "Minolta DSC") OrElse
        eqstr(imgComments(ix(i)), "OLYMPUS DIGITAL CAMERA") OrElse
        eqstr(imgComments(ix(i)), "LEAD Technologies Inc. V1.01") Then imgComments(ix(i)) = ""

      picinfo = getPicinfo(imgPath(ix(i)), False, 1)

      If Not picinfo.isNull AndAlso (picinfo.Width > 0 And picinfo.Height > 0) Then
        imgX(ix(i)) = picinfo.Width
        imgY(ix(i)) = picinfo.Height
      End If

      ' date
      s = readPhotoDate(imgPath(ix(i)))
      Try
        d = CDate(s)
        d = d.Add(UTCoff)
        imgDate(ix(i)) = d
      Catch ex As Exception
      End Try

      ' gps coordinates

      pcomments = readPropertyItems(imgPath(ix(i)))
      getGPSLocation(pcomments, Location, Altitude, xLat, xLon, iAltitude)
      If Location <> "" Then
        imgLatLon(ix(i)) = Location.Replace("°", " ")
        imgLatLon(ix(i)) = imgLatLon(ix(i)).Replace("'", "")
        imgXLat(ix(i)) = xLat
        imgXLon(ix(i)) = xLon
      End If
      imgAltitude(ix(i)) = ""
      If Altitude <> "" Then imgAltitude(ix(i)) = "Altitude " & Altitude

    Next i

  End Sub

  Function addBreaks(ByVal s As String) As String

    Dim i As Integer

    i = 1
    Do While i <= Len(s)
      If i < Len(s) Then
        If Mid(s, i, 2) = crlf Then
          s = vb.Left(s, i - 1) & "<br>" & Mid(s, i + 2)
          i = i + 3
        End If
      End If
      If s.Chars(i - 1) = ChrW(10) Or s.Chars(i - 1) = ChrW(13) Then
        s = vb.Left(s, i - 1) & "<br>" & Mid(s, i + 1)
        i = i + 3
      End If
      i = i + 1
    Loop

    addBreaks = s

  End Function

  Public Function getNextPath(ByRef i As Integer) As String
    getNextPath = ""
    If nimages = 0 Then Exit Function
    If i < 0 Then i = nimages - 1
    If i > nimages Then i = 0
    getNextPath = imgPath(ix(i))
    ipic = i
  End Function

  Private Sub picWeb_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles picWeb.Paint
    drawboxes(e.Graphics)
  End Sub

  Private Sub cmdOptions_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmdOptions.Click
    Using frm As New frmOptions
      OptionTab = frm.tabWebPage
      frm.ShowDialog()
    End Using
    Me.Select()
    GetSettings()
  End Sub

End Class

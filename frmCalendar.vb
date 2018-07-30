Imports System.Runtime.InteropServices
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Drawing.Text
Imports System.Math
Imports System.IO
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Data
Imports MySql.Data.MySqlClient

Public Class frmCalendar

  Dim calCaption As New List(Of String) ' photo descriptions, passed between calendar forms.
  Dim ixCal As New List(Of Integer) ' index to tagpaths, passed between calendar forms.
  Dim nCalPaths As Integer ' number of calendar images
  Dim calBegin As DateTime
  Dim nPages As Integer

  Dim cal As Calendar = CultureInfo.CurrentCulture.Calendar

  Dim fpath As String
  Public ipic As Integer  ' for frmFullScreen
  Dim lastCaption As Integer

  Dim calDate As DateTime
  Dim iPicm As Integer
  Dim qCal As clsCalendar
  Dim Loading As Boolean = True
  Dim Processing As Boolean = False
  Dim printerDisplayScale As Double
  Dim pd As PrintDocument

  Dim gImage As Bitmap
  Dim img As Bitmap
  Dim displayedPicm As Integer = -1


  Dim PrinterWidth As Integer  ' printable area, adjusted for landscape mode, 100ths of inch
  Dim PrinterHeight As Integer ' printable area, adjusted for landscape mode, 100ths of inch
  Dim PrinterLeft As Integer ' printable area, adjusted for landscape mode, 100ths of inch
  Dim PrinterTop As Integer ' printable area, adjusted for landscape mode, 100ths of inch
  Dim PaperWidth As Integer ' paper width, 100ths of inch, adjusted for landscape mode
  Dim PaperHeight As Integer ' paper width, 100ths of inch, adjusted for landscape mode
  Dim pSize As PaperSize ' paper size
  Dim pArea As RectangleF ' printable area

  Dim pageNumber As Integer ' page number being printed 
  Dim nCopies As Integer
  Dim fontName As String
  Dim calTitle As String

  Dim hMargin, vMargin As Single ' horizontal and vertical margins
  Dim marginsChanged As Boolean
  Public chkCat As New List(Of CheckBox)

  Dim gFont As Font
  Dim gBrush As SolidBrush

  Dim inCombo As Boolean

  Sub setPreview()

    Dim rBox, rPic As New RectangleF
    Dim z As Single
    Dim msg As String = ""

    If Me.WindowState = FormWindowState.Minimized Or Picture1.ClientSize.Width <= 0 Or Picture1.ClientSize.Height <= 0 Then Exit Sub
    Me.Cursor = Cursors.WaitCursor
    Picture1.Image = New Bitmap(Picture1.ClientSize.Width, Picture1.ClientSize.Height, PixelFormat.Format32bppPArgb)

    Using g As Graphics = Graphics.FromImage(Picture1.Image)

      z = Picture1.ClientSize.Width / PaperWidth
      rBox = g.VisibleClipBounds

      If (PaperWidth * z - hMargin * z * 100 * 2) < rBox.Width Then
        rBox.X = hMargin * z * 100
        rBox.Width = PaperWidth * z - hMargin * z * 100 * 2
      End If
      If (PaperHeight * z - vMargin * z * 100 * 2) < rBox.Height Then
        rBox.Y = vMargin * z * 100
        rBox.Height = PaperHeight * z - vMargin * z * 100 * 2
      End If

      If chkSamePage.Checked Or chkDaily.Checked Then ' print a picture every page

        rBox.Height = rBox.Height / 2 - vMargin * z * 100

        previewPic(rBox, rPic, g, calDate)
        rBox.Y = Picture1.ClientSize.Height / 2 + vMargin * z * 200

      End If

      If Not chkDaily.Checked Then ' show month calendar page
        qCal.Categories = getcats()
        calTitle = txTitle.Text
        If calTitle = "" Then calTitle = calDate.Year
        qCal.showMonth(g, calDate, rBox, calTitle, fontName)
      Else '   show second image 
        iPicm += 1
        previewPic(rBox, rPic, g, DateAdd(DateInterval.Day, 1, calDate))
      End If

    End Using

    Me.Cursor = Cursors.Default

  End Sub

  Sub previewPic(ByVal rBox As RectangleF, ByVal rPic As RectangleF, ByRef g As Graphics, ByVal dt As Date)

    Dim msg As String = ""

    If iPicm <> displayedPicm Then ' load the image from disk
      clearBitmap(img)
      gImage = readBitmap(tagPath(ixCal(iPicm)), msg)

      If gImage Is Nothing Then
        MsgBox(msg)
        Me.Cursor = Cursors.Default
        Exit Sub
      End If

      img = gImage.Clone

    End If

    clearBitmap(gImage)

    positionPic(img.Width, img.Height, rBox, rPic)
    If chkDaily.Checked Then
      If Trim(calCaption(ixCal(iPicm))) <> "" Then
        drawDailyCaption(rBox, rPic, calCaption(ixCal(iPicm)), g, dt)
      End If
      g.DrawImage(img, rPic)  ' actually do the printing (or at least draw to the print buffer)
      displayedPicm = iPicm

    Else ' monthly output
      If Trim(calCaption(ixCal(iPicm))) <> "" Then
        drawCaption(rBox, rPic, calCaption(ixCal(iPicm)), g)
      End If
      g.DrawImage(img, rPic)  ' actually do the printing (or at least draw to the print buffer)
      displayedPicm = iPicm
    End If
  End Sub

  Private Sub frmCalendar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    Dim installedFonts As InstalledFontCollection

    ' bug stuff
    Dim ds As DataSet
    Dim pic As pixClass

    Dim i, j As Integer
    Dim dt As DateTime
    Dim s As String
    Dim fam As FontFamily
    Dim sp As String

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    Loading = True

    If tagPath.Count <= 0 Then
      MsgBox("No photos are tagged to be included in the calendar.", MsgBoxStyle.OkOnly)
      Me.DialogResult = DialogResult.Cancel
      Me.Close()
      Exit Sub
    End If

    nCalPaths = tagPath.Count

    lstFiles.Items.Clear()
    For j = 0 To tagPath.Count - 1
      lstFiles.Items.Insert(j, Path.GetFileName(tagPath(j)))

      s = readPhotoDate(tagPath(j))
      dt = Nothing
      If Len(s) > 0 Then
        Try
          dt = CDate(s)
        Catch ex As Exception
        End Try
      End If

      s = readPhotoDescription(tagPath(j))
      calCaption.Add("")
      ixCal.Add(j)
      If useQuery Then ' add bug descriptions
        ds = getDS("select * from images join taxatable on images.taxonid = taxatable.id where filename = @parm1", Path.GetFileName(tagPath(j + 1)))
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
          pic = New pixClass(ds.Tables(0).Rows(0))
          calCaption(j) = getCalCaption(pic)
        End If

      Else ' normal caption -- exif comment or date
        If dt <> Nothing AndAlso dt.Year > 1 Then
          If s = "" Then
            s = "Photo taken "
          Else
            s = s & ", "
          End If
          calCaption(j) = s & Format(dt, "MMMM") & " " & Format(dt, "yyyy")
        Else
          calCaption(j) = s
        End If
      End If

    Next j

    If nCalPaths >= 0 Then lstFiles.SelectedIndex = 0

    lbNCalPaths.Text = "Number of Photos: " & tagPath.Count
    nmMonths.Value = tagPath.Count

    cmBeginMonth.Items.Clear()
    For i = 0 To 11
      cmBeginMonth.Items.Add(MonthName(i + 1))
    Next i

    calBegin = Today.AddMonths(1)
    calBegin = calBegin.Month & "/1/" & calBegin.Year
    calDate = calBegin
    txTitle.Text = ""
    nmBeginYear.Value = calBegin.Year
    cmBeginMonth.SelectedItem = Format(calBegin, "MMMM")
    lbMonth.Text = "Photo for " & Format(calBegin.AddMonths(lstFiles.SelectedIndex), "MMMM yyyy")

    If lstFiles.SelectedIndex >= 0 Then
      Me.Cursor = Cursors.WaitCursor
      i = ixCal(lstFiles.SelectedIndex)
      showPicture(tagPath(i), pView, False, Nothing)
      txCaption.Text = calCaption(i)
      Me.Cursor = Cursors.Default
    End If

    ' for tab 2

    qCal = New clsCalendar

    pd = New PrintDocument
    marginsChanged = False
    Me.CancelButton = cmdCancel1

    ' load installed font names
    installedFonts = New InstalledFontCollection
    For Each fam In installedFonts.Families
      cmbFonts.Items.Add(fam.Name)
      If eqstr(fam.Name, "times new roman") Then cmbFonts.SelectedIndex = cmbFonts.Items.Count - 1
    Next fam

    If cmbFonts.SelectedIndex >= 0 Then
      fontName = cmbFonts.SelectedItem
    Else
      fontName = "Times New Roman"
    End If

    cmbPrinter.Items.Clear()

    sp = pd.PrinterSettings.PrinterName ' default (or previous) printer name
    For Each s In PrinterSettings.InstalledPrinters
      cmbPrinter.Items.Add(s)
      If (s = sp) Then ' use previous (or default) printer
        cmbPrinter.SelectedIndex = cmbPrinter.Items.Count - 1
      End If
    Next s

    printerDisplayScale = iniPrintUnits
    hMargin = 0.25
    vMargin = 0.25
    txHMargin.Text = Format(hMargin * printerDisplayScale, "##0.0##")
    txVMargin.Text = Format(vMargin * printerDisplayScale, "##0.0##")

    If printerDisplayScale = 1 Then
      lbUnits0.Text = "inches"
      lbUnits1.Text = "inches"
    Else
      lbUnits0.Text = "mm"
      lbUnits1.Text = "mm"
    End If

    optAllPages.Checked = True
    optLandscape.Checked = True
    pd.DefaultPageSettings.Landscape = True

    cmbHorizontal.SelectedIndex = 0 ' center
    cmbVertical.SelectedIndex = 0 ' center
    iPicm = 0

    Loading = False

  End Sub

  Private Sub cmdNextTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNextTab.Click
    tabControl1.SelectTab(1)
  End Sub

  Sub enableNext()
    ' iPicm is 0 to  numberofmonths - 1 (or number of days - 1)

    If chkDaily.Checked Then ' two pics per page
      If iPicm < nCalPaths - 2 Then cmdNextMonth.Enabled = True Else cmdNextMonth.Enabled = False
      If iPicm > 1 Then cmdPreviousMonth.Enabled = True Else cmdPreviousMonth.Enabled = False
    Else
      If iPicm < nCalPaths - 1 Then cmdNextMonth.Enabled = True Else cmdNextMonth.Enabled = False
      If iPicm > 0 Then cmdPreviousMonth.Enabled = True Else cmdPreviousMonth.Enabled = False
    End If

  End Sub

  Private Sub cmdPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPreviousMonth.Click
    If chkDaily.Checked Then
      calDate = DateAdd(DateInterval.Day, -2, calDate)
      iPicm = iPicm - 2 ' iPicm is 0 to  numberofmonths - 1
    Else
      calDate = DateAdd(DateInterval.Month, -1, calDate)
      iPicm = iPicm - 1 ' iPicm is 0 to  numberofmonths - 1
    End If
    setPreview()
    enableNext()
  End Sub

  Private Sub cmdNextMonth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNextMonth.Click
    If chkDaily.Checked Then
      calDate = DateAdd(DateInterval.Day, 2, calDate)
      iPicm = iPicm + 2 ' iPicm is 0 to  numberofmonths - 1
    Else
      calDate = DateAdd(DateInterval.Month, 1, calDate)
      iPicm = iPicm + 1 ' iPicm is 0 to  numberofmonths - 1
    End If
    setPreview()
    enableNext()
  End Sub

  Function getcats() As String  ' categories of comments to show on calendar. Should match the ones in holidays.dat and calendarevents.dat

    Dim i As Integer

    getcats = ""
    For i = 0 To chkCat.Count - 1
      If chkCat(i).Checked Then getcats = getcats & chkCat(i).Tag & crlf
    Next i

  End Function

  Sub SetPaper()
    ' sets the "paper," shadow, and rview position and size on the form

    Dim i As Integer
    Dim ipad As Integer
    Dim heightallowed As Double
    Dim widthallowed As Double
    Dim z As Double
    Dim vdif, hdif As Integer

    If Loading OrElse Processing OrElse Picture1.Image Is Nothing Then Exit Sub

    If Picture1.Image IsNot Nothing Then
      Using g As Graphics = Graphics.FromImage(Picture1.Image)
        g.Clear(Color.White)
      End Using
      Picture1.Refresh()
    End If

    pArea = pd.DefaultPageSettings.PrintableArea  ' slow
    pSize = pd.DefaultPageSettings.PaperSize ' slow

    PrinterTop = pArea.Top ' these are in inches*100, the same scale used for output
    PrinterLeft = pArea.Left
    PrinterWidth = pArea.Width
    PrinterHeight = pArea.Height
    PaperWidth = pSize.Width
    PaperHeight = pSize.Height

    If optLandscape.Checked Then  ' landscape - swap x and y for these settings
      i = PrinterWidth : PrinterWidth = PrinterHeight : PrinterHeight = i
      i = PrinterLeft : PrinterLeft = PrinterTop : PrinterTop = i
      i = PaperWidth : PaperWidth = PaperHeight : PaperHeight = i
    End If

    ' make sure these values won't crash the program
    If PrinterWidth <= 0 Then PrinterWidth = Picture1.ClientSize.Width
    If PrinterHeight <= 0 Then PrinterHeight = Picture1.ClientSize.Height
    If PaperWidth <= 0 Then PaperWidth = Picture1.ClientSize.Width
    If PaperHeight <= 0 Then PaperHeight = Picture1.ClientSize.Height

    ipad = 20
    widthallowed = Me.ClientSize.Width - (Frame1.Left + Frame1.Width) - ipad * 2
    heightallowed = cmdNextMonth.Top - ipad * 2

    If (widthallowed * PrinterHeight / PrinterWidth) < heightallowed Then
      ' wide
      pnPaperFrame.Width = widthallowed
      vdif = pnPaperFrame.Height - pnPaper.Height
      pnPaperFrame.Height = (widthallowed - vdif) * PaperHeight / PaperWidth + vdif
    Else
      ' tall
      pnPaperFrame.Height = heightallowed
      hdif = pnPaperFrame.Width - pnPaper.Width
      pnPaperFrame.Width = (heightallowed - hdif) * PaperWidth / PaperHeight + hdif
    End If
    pnPaperFrame.Left = (widthallowed - pnPaperFrame.Width) / 2 + (Frame1.Left + Frame1.Width) + ipad
    pnPaperFrame.Top = (heightallowed - pnPaperFrame.Height) / 2 + ipad

    ' add paper margin and set picture1
    z = pnPaper.Width / PrinterWidth
    Picture1.Left = (PaperWidth - PrinterWidth) * z + pnPaper.ClientRectangle.X
    Picture1.Top = (PaperHeight - PrinterHeight) * z + pnPaper.ClientRectangle.Y
    Picture1.Width = pnPaper.ClientRectangle.Width - (PaperWidth - PrinterWidth) * z * 2
    Picture1.Height = pnPaper.ClientRectangle.Height - (PaperHeight - PrinterHeight) * z * 2

  End Sub

  Private Sub cmbPrinter_SelectedIndexChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles cmbPrinter.SelectedIndexChanged

    If inCombo Or Loading Then Exit Sub
    inCombo = True
    Me.Cursor = Cursors.WaitCursor

    Try
      pd.PrinterSettings.PrinterName = cmbPrinter.Text
    Catch
      MsgBox("Error -- cannot use printer " & cmbPrinter.Text)
      Me.DialogResult = DialogResult.Cancel
      Me.Close()
      Exit Sub
    End Try

    getPrinterStuff()
    SetPaper()
    setPreview()

    inCombo = False
    Me.Cursor = Cursors.Default

  End Sub

  Private Sub optOrientation_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles optPortrait.CheckedChanged, optLandscape.CheckedChanged

    If Loading Then Exit Sub
    Me.Cursor = Cursors.WaitCursor

    If Sender.Checked Then
      If Sender Is optPortrait Then
        pd.DefaultPageSettings.Landscape = False
      Else ' landscape -- swap x and y for these settings
        pd.DefaultPageSettings.Landscape = True
      End If

      SetPaper()
      setPreview()
    End If
    Me.Cursor = Cursors.Default
  End Sub

  Sub getPrinterStuff()
    ' take data from pd and put it into some variables. It's slow sometimes
    ' should be called initially and when the printer is changed or docprop called

    Dim i As Integer
    Dim s As String

    nCopies = pd.PrinterSettings.Copies
    nmNcopies.Value = nCopies
    If pd.DefaultPageSettings.Landscape = False Then
      optPortrait.Checked = True
    Else ' 2
      optLandscape.Checked = True
    End If

    s = pd.PrinterSettings.PrinterName
    For i = 0 To cmbPrinter.Items.Count - 1
      If CStr(cmbPrinter.Items(i)) = s Then
        If cmbPrinter.SelectedIndex <> i Then cmbPrinter.SelectedIndex = i
        Exit For
      End If
    Next i

    pArea = pd.DefaultPageSettings.PrintableArea  ' slow
    pSize = pd.DefaultPageSettings.PaperSize ' slow

  End Sub

  Sub setupPanel()

    Dim i, k As Integer
    Dim s As String
    Dim ss() As String
    Dim iline As Integer
    Dim wasChecked(20) As Boolean

    For i = 0 To UBound(wasChecked) : wasChecked(i) = False : Next

    If chkCat IsNot Nothing Then
      For i = chkCat.Count - 1 To 0 Step -1
        wasChecked(i) = chkCat(i).Checked
        If chkCat(i) IsNot Nothing Then chkCat(i).Dispose() : chkCat(i) = Nothing
        chkCat.RemoveAt(i)
      Next i
    End If

    chkCat.Add(New CheckBox)
    chkCat(chkCat.Count - 1).Text = "Sun and Moon"
    chkCat(chkCat.Count - 1).Tag = "lunar"
    chkCat.Add(New CheckBox)
    chkCat(chkCat.Count - 1).Text = "Holidays and Events"
    chkCat(chkCat.Count - 1).Tag = "general"
    chkCat.Add(New CheckBox)
    chkCat(chkCat.Count - 1).Text = "Turing Award Winners"
    chkCat(chkCat.Count - 1).Tag = "turing"

    ReDim ss(-1) ' null
    If File.Exists(dataPath & customCalFile) Then
      Try
        ss = File.ReadAllLines(dataPath & customCalFile)
      Catch ex As Exception
        MsgBox(ex.Message)
      End Try
    End If

    iline = 0
    If ss IsNot Nothing Then
      Do While iline <= UBound(ss)
        s = ss(iline) : iline = iline + 1
        If IsNumeric(s) AndAlso CInt(s) = -1 Then ' -1 means a new category is on the next line
          chkCat.Add(New CheckBox)
          chkCat(chkCat.Count - 1).Text = Trim(ss(iline))
          chkCat(chkCat.Count - 1).Tag = Trim(ss(iline))
          iline = iline + 1
        End If
      Loop
    End If

    For i = 0 To chkCat.Count - 1
      chkCat(i).Parent = Panel1
      chkCat(i).Location = New Point(18, i * 28 + 16)
      chkCat(i).Size = New Size(Panel1.Width - 48, 21)
      chkCat(i).Checked = wasChecked(i)
      AddHandler chkCat(i).CheckedChanged, AddressOf chkcat_checkedChanged
    Next i
    chkCat(0).Checked = True
    chkCat(1).Checked = True

    i = chkCat.Count * 28 + 32 ' height of all the check boxes
    k = cmdPreviousMonth.Top + cmdPreviousMonth.Height - Panel1.Top ' maximum height for panel1
    If i < k Then Panel1.Height = i Else Panel1.Height = k

  End Sub

  Private Sub chkcat_checkedChanged(ByVal Sender As Object, ByVal e As EventArgs)
    If Loading Then Exit Sub
    setPreview()
  End Sub

  Private Sub cmdEditCustom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditCustom.Click
    Using frm As New frmCalendarEvents
      frm.ShowDialog()
    End Using
    setupPanel()
    setPreview()
  End Sub

  Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click

    ' pd is global PrintDlgType
    Dim mResult As MsgBoxResult
    Dim lastdate As Date

    nPages = nmMonths.Value
    If chkDaily.Checked Then
      lastdate = DateAdd(DateInterval.Month, nPages, calDate)
      nPages = DateDiff(DateInterval.Day, calDate, lastdate)
    End If

    If nPages > nCalPaths Then
      mResult = MsgBox("There are " & nPages & " months specified for the calendar, but only " & nCalPaths & _
        " photos tagged for the calendar. The first " & nPages - nCalPaths & " photos will be repeated.", MsgBoxStyle.OkCancel)
      If mResult = MsgBoxResult.Cancel Then Exit Sub
    End If

    If nPages < nCalPaths Then
      mResult = MsgBox("There are " & nCalPaths & " photos tagged, but only " & nPages & _
        " months specified to be printed. The last " & nCalPaths - nPages & " photos will be skipped.", MsgBoxStyle.OkCancel)
      If mResult = MsgBoxResult.Cancel Then Exit Sub
    End If

    Me.Cursor = Cursors.WaitCursor
    pd.DocumentName = AppName & " Calendar"

    pageNumber = 0

    If chkDaily.Checked Then
      RemoveHandler pd.PrintPage, AddressOf printPage
      RemoveHandler pd.PrintPage, AddressOf printDoublePage
      AddHandler pd.PrintPage, AddressOf printDailyPage
      pd.Print()

    Else
      RemoveHandler pd.PrintPage, AddressOf printDailyPage

      If chkSamePage.Checked Then
        If nPages > 1 Or Not optEvenPages.Checked Then AddHandler pd.PrintPage, AddressOf printDoublePage
      Else
        AddHandler pd.PrintPage, AddressOf printPage
      End If
      pd.Print()
      If chkSamePage.Checked Then
        If nPages > 1 Or Not optEvenPages.Checked Then RemoveHandler pd.PrintPage, AddressOf printDoublePage
      Else
        RemoveHandler pd.PrintPage, AddressOf printPage
      End If
    End If

    Me.Cursor = Cursors.Default

  End Sub

  Sub printPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
    ' print photo and calendar on separate pages

    Dim i As Integer
    Dim rBox, rPic As RectangleF
    Dim img As Bitmap = Nothing
    Dim kCal As Integer ' calendar and photo to print
    Dim dt As DateTime
    Dim hm, vm As Single
    Dim msg As String = ""

    If optEvenPages.Checked Then pageNumber = pageNumber + 1

    kCal = Floor(pageNumber / 2)
    dt = calBegin.AddMonths(kCal)
    If kCal > nCalPaths Then kCal = kCal Mod (nCalPaths) ' wrap in case there are not enough photos

    e.Graphics.ResetTransform()
    e.Graphics.InterpolationMode = InterpolationMode.Bicubic

    pArea = e.Graphics.VisibleClipBounds ' pd.printablearea is not accurate. This is better, but only available here.
    PrinterTop = pArea.Top ' these are in inches*100, the same scale used for output
    PrinterLeft = pArea.Left
    PrinterWidth = pArea.Width
    PrinterHeight = pArea.Height
    If optLandscape.Checked Then  ' landscape - swap x and y for these settings
      i = PrinterWidth : PrinterWidth = PrinterHeight : PrinterHeight = i
      i = PrinterLeft : PrinterLeft = PrinterTop : PrinterTop = i
    End If

    hm = Max(hMargin * 100 - (PaperWidth - pArea.Width) / 2, 0)
    vm = Max(vMargin * 100 - (PaperHeight - pArea.Height) / 2, 0)

    If pageNumber Mod 2 = 0 Then ' print photo
      rBox.X = pArea.Left + hm
      rBox.Y = pArea.Top + vm
      rBox.Width = pArea.Width - hm * 2
      rBox.Height = pArea.Height - vm * 2

      ' read gimage
      img = readBitmap(tagPath(ixCal(kCal)), msg)
      If img Is Nothing Then
        MsgBox(msg)
        Exit Sub
      End If

      positionPic(img.Width, img.Height, rBox, rPic)
      If Trim(calCaption(ixCal(kCal))) <> "" Then
        drawCaption(rBox, rPic, calCaption(ixCal(kCal)), e.Graphics)
      End If
      e.Graphics.DrawImage(img, rPic)  ' actually do the printing (or at least draw to the print buffer)
      clearBitmap(img)

    Else ' print calendar on a page
      rBox.X = pArea.Left + hMargin
      rBox.Y = pArea.Top + vMargin
      rBox.Width = pArea.Width - hMargin * 2
      rBox.Height = pArea.Height - vMargin * 2

      If (PaperWidth - hMargin * 100 * 2) < rBox.Width Then
        rBox.X = hMargin * 100
        rBox.Width = PaperWidth - hMargin * 100 * 2
      End If
      If (PaperHeight - vMargin * 100 * 2) < rBox.Height Then
        rBox.Y = vMargin * 100
        rBox.Height = PaperHeight - vMargin * 100 * 2
      End If

      calTitle = txTitle.Text
      If calTitle = "" Then calTitle = dt.Year
      qCal.showMonth(e.Graphics, dt, rBox, calTitle, fontName)
    End If

    'Dim p(5) As PointF
    'p(0) = New PointF(pArea.X, pArea.Y)
    'p(1) = New PointF(pArea.Width, p(0).Y)
    'p(2) = New PointF(p(1).X, pArea.Height)
    'p(3) = New PointF(p(0).X, p(2).Y)
    'p(4) = New PointF(p(0).X, p(0).Y)
    'p(5) = New PointF(p(2).X, p(2).Y)
    'e.Graphics.DrawLines(Pens.DarkGreen, p)

    pageNumber = pageNumber + 1
    If optOddPages.Checked Then pageNumber = pageNumber + 1

    If Floor(pageNumber / 2) >= nPages Then e.HasMorePages = False Else e.HasMorePages = True

  End Sub

  Sub printDoublePage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
    ' print photo and calendar on the same page

    Dim n As Integer
    Dim rBox, rPic As RectangleF
    Dim img As Bitmap = Nothing
    Dim kPic, kCal As Integer ' calendar and photo to print
    Dim dt As DateTime
    Dim halfPage As Integer
    Dim hm, vm As Single
    Dim msg As String = ""

    If chkCollate.Checked Then
      If optOddPages.Checked Then
        If nPages Mod 2 = 0 Then
          kPic = pageNumber - 1
          kCal = nPages - pageNumber - 1
        Else
          kPic = pageNumber
          kCal = nPages - pageNumber - 1
        End If
      Else ' even pages
        kPic = Ceiling(nPages / 2) + pageNumber
        kCal = nPages - Floor(nPages / 2) - pageNumber - 2
      End If

    Else ' no collating
      kPic = pageNumber
      kCal = pageNumber
    End If

    dt = calBegin.AddMonths(kCal)

    If kCal > nCalPaths Then kCal = kCal Mod (nCalPaths) ' wrap in case there are not enough photos

    e.Graphics.ResetTransform()
    e.Graphics.InterpolationMode = InterpolationMode.Bicubic

    pArea = e.Graphics.VisibleClipBounds ' pd.printablearea is not accurate. This is better, but only available here.

    hm = Max(hMargin * 100 - (PaperWidth - pArea.Width) / 2, 0)
    vm = Max(vMargin * 100 - (PaperHeight - pArea.Height) / 2, 0)

    halfPage = pArea.Height / 2 - (PaperHeight - pArea.Height) / 2
    rBox.X = pArea.Left + hm
    rBox.Y = pArea.Top + vm
    rBox.Width = pArea.Width - hm * 2
    rBox.Height = halfPage - vm * 2

    If kPic >= 0 Then ' it may have to skip the first one -- even nPages, first page
      ' print the picture in the top half
      img = readBitmap(tagPath(ixCal(kPic)), msg)
      If img Is Nothing Then
        MsgBox(msg)
        e.HasMorePages = False
        Exit Sub
      End If

      positionPic(img.Width, img.Height, rBox, rPic)
      If Trim(calCaption(ixCal(kCal))) <> "" Then
        drawCaption(rBox, rPic, calCaption(ixCal(kCal)), e.Graphics)
      End If
      e.Graphics.DrawImage(img, rPic)  ' actually do the printing (or at least draw to the print buffer)
      clearBitmap(img)
    End If

    If kCal >= 0 Then
      ' print calendar part, bottom half
      rBox.X = pArea.Left + hm
      rBox.Y = pArea.Height - halfPage + vm
      rBox.Width = pArea.Width - hm * 2
      rBox.Height = halfPage - vm * 2

      If (PaperWidth - hm * 2) < rBox.Width Then
        rBox.X = hm * 100
        rBox.Width = PaperWidth - hm * 100 * 2
      End If
      If (PaperHeight - vm * 2) < rBox.Height Then
        rBox.Y = vm
        rBox.Height = PaperHeight - vm * 2
      End If

      calTitle = txTitle.Text
      If calTitle = "" Then calTitle = dt.Year
      qCal.showMonth(e.Graphics, dt, rBox, calTitle, fontName)
    End If

    If chkCollate.Checked Then
      If optOddPages.Checked Then
        n = Floor(nPages / 2) + 1
      Else ' even pages
        n = Floor(nPages / 2)
      End If
    Else ' no collating
      n = nPages
    End If

    pageNumber = pageNumber + 1
    If pageNumber >= n Then e.HasMorePages = False Else e.HasMorePages = True

  End Sub

  Sub printDailyPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
    ' print 2 pages for daily calendar
    ' pageNumber is the page number to be printed

    Dim n As Integer
    Dim rBox As RectangleF
    Dim gImage As Bitmap = Nothing
    Dim kCal As Integer ' calendar and photo to print
    Dim dt As DateTime
    Dim hm, vm As Single
    Dim msg As String = ""

    e.HasMorePages = True

    dt = calBegin.AddMonths(kCal)

    If kCal > nCalPaths Then kCal = kCal Mod (nCalPaths) ' wrap in case there are not enough photos

    e.Graphics.ResetTransform()
    e.Graphics.InterpolationMode = InterpolationMode.Bicubic

    pArea = e.Graphics.VisibleClipBounds ' pd.printablearea is not accurate. This is better, but only available here.

    hm = Max(hMargin * 100 - (PaperWidth - pArea.Width) / 2, 0)
    vm = Max(vMargin * 100 - (PaperHeight - pArea.Height) / 2, 0)

    ' print the picture in the top half
    rBox.X = pArea.Left + hm
    rBox.Y = pArea.Top + vm
    rBox.Width = pArea.Width - hm * 2
    rBox.Height = pArea.Height / 2 - (vm + vMargin * 100)
    printDailyHalf(rBox, e)
    pageNumber = pageNumber + 1

    If e.HasMorePages Then
      ' print the picture in the bottom half
      rBox.Y += rBox.Height + vMargin * 100 * 2
      printDailyHalf(rBox, e)
      pageNumber = pageNumber + 1
    End If

    n = nPages

    If pageNumber >= n Then e.HasMorePages = False

  End Sub

  Sub printDailyHalf(ByRef rbox As RectangleF, ByRef e As PrintPageEventArgs)

    Dim msg As String = ""
    Dim rPic As RectangleF
    Dim iPage As Integer
    Dim dt As Date


    If chkCollate.Checked Then ' print them so they can be cut in half
      iPage = pageNumber \ 2
      If (pageNumber Mod 2) = 1 Then iPage += nPages \ 2
    Else
      iPage = pageNumber
    End If

    iPage = iPage Mod ixCal.Count - 1

    ' print one of two daily pics per page
    img = readBitmap(tagPath(ixCal(iPage)), msg)
    If img Is Nothing Then
      MsgBox(msg)
      e.HasMorePages = False
      Exit Sub
    End If

    positionPic(img.Width, img.Height, rbox, rPic)
    dt = DateAdd(DateInterval.Day, iPage, calDate)
    drawDailyCaption(rbox, rPic, calCaption(ixCal(iPage)), e.Graphics, dt)

    e.Graphics.DrawImage(img, rPic)  ' actually do the printing (or at least draw to the print buffer)
    clearBitmap(img)

  End Sub

  Sub drawCaption(ByRef rBox As RectangleF, ByRef rPic As RectangleF, ByRef calCaption As String, ByRef g As Graphics)

    Dim tScale As Double
    Dim x As Double
    Dim fmt As New StringFormat
    Dim ix, iy As Single
    Dim tSize As Double

    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias  ' not sure which of these works
    g.SmoothingMode = SmoothingMode.AntiAlias  ' not sure which of these works
    g.PixelOffsetMode = PixelOffsetMode.HighQuality  ' not sure which of these works

    tSize = 2 ' text size = 2/100 of the max width or height of rbox 

    tScale = Max(rBox.Width, rBox.Height) / 100
    x = (rPic.Height - tScale * tSize * 1.5) / rPic.Height ' scale down the drawing just a bit.
    rPic.Height = rPic.Height * x
    rPic.X = rPic.X + (rPic.Width - rPic.Width * x) / 2
    rPic.Width = rPic.Width * x
    fmt = StringFormat.GenericDefault
    fmt.LineAlignment = StringAlignment.Center ' center text under the drawing
    fmt.Alignment = StringAlignment.Center
    fmt.FormatFlags = 0

    ix = rPic.X + rPic.Width / 2
    iy = rPic.Y + rPic.Height + tScale * tSize
    gFont = New Font(fontName, tScale * tSize, FontStyle.Regular, GraphicsUnit.Pixel, CharSet.Unicode)
    gBrush = New SolidBrush(Color.Gray)
    g.DrawString(calCaption, gFont, gBrush, ix, iy, fmt)

    'gFont.Dispose()
    'gBrush.Dispose()

  End Sub

  Sub drawDailyCaption(ByRef rBox As RectangleF, ByRef rPic As RectangleF, ByRef calCaption As String, ByRef g As Graphics, ByVal dt As Date)

    Dim tScale As Double
    Dim x As Double
    Dim fmt As New StringFormat
    Dim ix, iy As Single
    Dim tSize As Double
    Dim dx, dy As Double
    Dim d1, d2 As Double

    Dim ptf As PointF

    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias  ' not sure which of these works
    g.SmoothingMode = SmoothingMode.AntiAlias  ' not sure which of these works
    g.PixelOffsetMode = PixelOffsetMode.HighQuality  ' not sure which of these works

    tSize = 2 ' text size = 2/100 of the max width or height of rbox 
    tScale = Max(rBox.Width, rBox.Height) / 100

    ' get the height of the bottom and width of the side text for picture scaling
    fmt = StringFormat.GenericDefault
    fmt.LineAlignment = StringAlignment.Center ' center text under the drawing
    fmt.Alignment = StringAlignment.Near
    fmt.FormatFlags = 0

    gFont = New Font(fontName, tScale * tSize, FontStyle.Regular, GraphicsUnit.Pixel, CharSet.Unicode)
    ptf = g.MeasureString(calCaption, gFont, rBox.Width, fmt)
    dy = ptf.Y + tScale * tSize

    fmt = StringFormat.GenericDefault
    fmt.LineAlignment = StringAlignment.Center ' center text under the drawing
    fmt.Alignment = StringAlignment.Near
    fmt.FormatFlags = StringFormatFlags.DirectionVertical

    gFont = New Font(fontName, tScale * tSize * 2, FontStyle.Regular, GraphicsUnit.Pixel, CharSet.Unicode)
    ptf = g.MeasureString(Format(dt, "D"), gFont, rBox.Width, fmt)
    dx = ptf.X + tScale * tSize

    d1 = rBox.Height / rBox.Width ' aspect ratio of box
    d2 = (rPic.Height + dy) / (rPic.Width + dx) ' aspect ratio of picture

    If d2 > d1 Then ' picture is taller than box
      x = (rPic.Height - dy) / rPic.Height
      rPic.X += (rPic.Width - rPic.Width * x) / 2
    Else
      x = (rPic.Width - dx) / rPic.Width
      rPic.Y += (rPic.Height - rPic.Height * x) / 2
    End If

    rPic.Height = rPic.Height * x
    rPic.Width = rPic.Width * x

    fmt = StringFormat.GenericDefault
    fmt.LineAlignment = StringAlignment.Center ' center text under the drawing
    fmt.Alignment = StringAlignment.Near
    fmt.FormatFlags = 0

    ix = rPic.X
    iy = rPic.Y + rPic.Height + tScale * tSize * 2
    gBrush = New SolidBrush(Color.Navy)
    gFont = New Font(fontName, tScale * tSize, FontStyle.Regular, GraphicsUnit.Pixel, CharSet.Unicode)
    g.DrawString(calCaption, gFont, gBrush, ix, iy, fmt)

    ' now the date
    fmt = StringFormat.GenericDefault
    fmt.LineAlignment = StringAlignment.Center ' center text under the drawing
    fmt.Alignment = StringAlignment.Near
    fmt.FormatFlags = StringFormatFlags.DirectionVertical

    ix = rPic.X + rPic.Width + tScale * tSize * 1.5
    iy = rPic.Y
    gFont = New Font(fontName, tScale * tSize * 2, FontStyle.Regular, GraphicsUnit.Pixel, CharSet.Unicode)
    gBrush = New SolidBrush(Color.LightBlue)
    g.DrawString(Format(dt, "D"), gFont, gBrush, ix, iy, fmt)


    'gFont.Dispose()
    'gBrush.Dispose()

  End Sub

  Sub positionPic(ByVal pWidth As Integer, ByVal pHeight As Integer, ByVal rBox As RectangleF, ByRef rPic As RectangleF)
    ' returns rpic, location for the pic inside the space rbox
    ' subtracts border from rbox, uses cmb alignment

    Dim w, h As Integer ' useable box size

    rPic.Width = pWidth
    rPic.Height = pHeight
    If pWidth <= 0 Or pHeight <= 0 Then Exit Sub

    w = rBox.Width  ' box useable size
    h = rBox.Height

    If pWidth / pHeight >= w / h Then   ' picture limited by width
      rPic.Width = pWidth * w / pWidth
      rPic.Height = pHeight * w / pWidth
    Else   ' picture limited by height
      rPic.Width = pWidth * h / pHeight
      rPic.Height = pHeight * h / pHeight
    End If
    If rPic.Width <= 0 Or rPic.Height <= 0 Then Exit Sub

    ' place the image
    Select Case cmbHorizontal.Text
      Case "Center"
        rPic.X = rBox.X + (w - rPic.Width) / 2
      Case "Left"
        rPic.X = rBox.X
      Case "Right"
        rPic.X = rBox.X + (w - rPic.Width)
    End Select

    Select Case cmbVertical.Text
      Case "Center"
        rPic.Y = rBox.Y + (h - rPic.Height) / 2
      Case "Top"
        rPic.Y = rBox.Y
      Case "Bottom"
        rPic.Y = rBox.Y + (h - rPic.Height)
    End Select

  End Sub

  Private Sub frmCalendar_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
    If Loading Then Exit Sub
    setPreview()
  End Sub


  Private Sub cmbFonts_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmbFonts.SelectedIndexChanged

    If Loading Then Exit Sub
    If cmbFonts.SelectedIndex >= 0 Then
      fontName = cmbFonts.SelectedItem
    Else
      fontName = "Times New Roman"
    End If

    setPreview()

  End Sub

  Private Sub txTitle_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txTitle.Leave
    setPreview()
  End Sub

  Private Sub txtMargin_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txHMargin.Leave, txVMargin.Leave

    Dim txMargin As TextBox
    Dim max As Double
    Dim s As String
    Dim x As Double

    If Loading Then Exit Sub
    If Me.ActiveControl Is cmdCancel Then Exit Sub
    txMargin = sender

    If marginsChanged Then
      If printerDisplayScale = 1 Then
        max = PaperWidth / 4
        s = "inches"
      Else
        max = 13
        s = "mm"
      End If

      If checknumber(txMargin.Text, 0, max, x) Then
        txMargin.Text = Format(x, "##0.0##")
        If sender Is txHMargin Then
          hMargin = x / printerDisplayScale
        Else
          vMargin = x / printerDisplayScale
        End If
      Else
        MsgBox("Enter the margin size in " & s & ", from 0 to " & max)
        txMargin.Select()
        Exit Sub
      End If

      setPreview()
      marginsChanged = False
    End If

  End Sub

  Private Sub txMargin_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles txHMargin.TextChanged, txVMargin.TextChanged

    If Not Loading Then marginsChanged = True
  End Sub

  Private Sub cmdPreferences_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdPreferences.Click

    Dim result As Integer
    Dim i As Integer

    result = docProp(DM_IN_BUFFER Or DM_IN_PROMPT Or DM_OUT_BUFFER, pd, Me)
    getPrinterStuff()

    If result = IDOK Or result = IDCANCEL Then
      SetPaper()
      setPreview()
    Else
      i = Err.LastDllError
      MsgBox("Error - " & ErrorDescription(i))
    End If

  End Sub

  Private Sub cmdPreviousTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPreviousTab.Click
    tabControl1.SelectTab(0)
  End Sub

  Private Sub chkDaily_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDaily.CheckedChanged

    Dim s As String

    If chkDaily.Checked Then
      s = chkCollate.Text
      chkCollate.Text = s.Replace("stapling", "cutting")
      chkCollate.Checked = True
      chkSamePage.Enabled = False
      optAllPages.Enabled = False
      optOddPages.Enabled = False
      optEvenPages.Enabled = False

      cmdEditCustom.Enabled = False
      Label1.Enabled = False
      Panel1.Enabled = False
      Frame3.Enabled = False
      Label2.Enabled = False
      txTitle.Enabled = False

      If optLandscape.Checked Then
        optPortrait.Checked = True ' this calls setpreview
      Else
        setPreview()
      End If
    Else
      s = chkCollate.Text
      chkCollate.Text = s.Replace("cutting", "stapling")
      chkCollate.Checked = False
      chkSamePage.Enabled = True
      optAllPages.Enabled = True
      optOddPages.Enabled = True
      optEvenPages.Enabled = True

      cmdEditCustom.Enabled = True
      Label1.Enabled = True
      Panel1.Enabled = True
      Frame3.Enabled = True
      Label2.Enabled = True
      txTitle.Enabled = True

      If optPortrait.Checked Then
        optLandscape.Checked = True ' this calls setpreview
      Else
        setPreview()
      End If
    End If

  End Sub

  Private Sub chkSamePage_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkSamePage.CheckedChanged

    If chkSamePage.Checked Then
      chkCollate.Enabled = True
      If optLandscape.Checked Then
        optPortrait.Checked = True ' this calls setpreview
      Else
        setPreview()
      End If
    Else
      chkCollate.Enabled = False
      If optPortrait.Checked Then
        optLandscape.Checked = True ' this calls setpreview
      Else
        setPreview()
      End If
    End If

  End Sub

  Private Sub rview_mouseClick(ByVal Sender As Object, ByVal e As EventArgs) Handles pView.MouseClick

    If pView.Bitmap Is Nothing Then Exit Sub
    callingForm = Me
    If pView.Bitmap.Width < 600 Then
      showPicture(fpath, pView, False, Nothing)
      Exit Sub
    End If

    ipic = lstFiles.SelectedIndex
    qImage = pView.Bitmap.Clone
    currentpicPath = fpath
    Using frm As New frmFullscreen
      frm.ShowDialog()
    End Using

    clearBitmap(qImage)

  End Sub

  Private Sub cmdDel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdDel.Click

    Dim iFrom As Integer
    Dim i As Integer

    iFrom = lstFiles.SelectedIndex
    If lstFiles.Items.Count = 0 Then Exit Sub

    For i = iFrom To lstFiles.Items.Count - 2
      ixCal(i) = ixCal(i + 1)
    Next i

    lstFiles.Items.RemoveAt(lstFiles.SelectedIndex)

    nCalPaths = nCalPaths - 1
    lbNCalPaths.Text = "Number of Photos: " & nCalPaths

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
    Processing = True

    If Sender Is cmdMoveUp Then iTo = iFrom - 1 Else iTo = iFrom + 1

    If iTo < 0 Or iTo > nCalPaths - 1 Then Exit Sub

    ' swap ix pointers.
    i = ixCal(iFrom)
    ixCal(iFrom) = ixCal(iTo)
    ixCal(iTo) = i

    ' swap list captions.
    s = lstFiles.Items(iFrom)
    lstFiles.Items(iFrom) = lstFiles.Items(iTo)
    lstFiles.Items(iTo) = s

    lstFiles.SelectedIndex = iTo
    Processing = False

  End Sub

  Private Sub lstFiles_SelectedIndexChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles lstFiles.SelectedIndexChanged

    If Loading Or Processing Then Exit Sub
    If lstFiles.SelectedIndex < 0 Or lstFiles.SelectedIndex > lstFiles.Items.Count - 1 Then Exit Sub

    Me.Cursor = Cursors.WaitCursor
    showPicture(tagPath(ixCal(lstFiles.SelectedIndex)), pView, False, Nothing)
    fpath = tagPath(ixCal(lstFiles.SelectedIndex)) ' save path for fullscreen

    txCaption.Text = calCaption(ixCal(lstFiles.SelectedIndex))
    lastCaption = lstFiles.SelectedIndex

    If lstFiles.SelectedIndex <= 0 Then
      cmdMoveUp.Enabled = False
    Else
      cmdMoveUp.Enabled = True
    End If

    If lstFiles.SelectedIndex >= lstFiles.Items.Count - 1 Then
      cmdMoveDown.Enabled = False
    Else
      cmdMoveDown.Enabled = True
    End If

    lbMonth.Text = "Photo for " & Format(calBegin.AddMonths(lstFiles.SelectedIndex), "MMMM yyyy")
    Me.Cursor = Cursors.Default

  End Sub

  Private Sub txtCaption_KeyDown(ByVal Sender As Object, ByVal e As KeyEventArgs) Handles txCaption.KeyDown

    If e.KeyCode = 9 Then
      e.Handled = True
      lstFiles.Select()
    End If
  End Sub

  Private Sub txtCaption_Leave(ByVal Sender As Object, ByVal e As EventArgs) Handles txCaption.Leave

    ' the variable lastCaption is used because listindex gets out of synch
    calCaption(ixCal(lastCaption)) = txCaption.Text.Trim

  End Sub

  Private Sub cmBeginMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles cmBeginMonth.SelectedIndexChanged, nmBeginYear.ValueChanged
    If Loading Then Exit Sub
    calBegin = New DateTime(nmBeginYear.Value, cmBeginMonth.SelectedIndex + 1, 1)
    calDate = calBegin
    lbMonth.Text = "Photo for " & Format(calBegin.AddMonths(lstFiles.SelectedIndex), "MMMM yyyy")

  End Sub

  Public Function getNextPath(ByRef i As Integer) As String
    If i < 0 Then i = nCalPaths - 1
    If i > nCalPaths - 1 Then i = 0
    ipic = i
    Return tagPath(ixCal(i))
  End Function

  Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles cmdCancel.Click, cmdCancel1.Click
    Me.DialogResult = DialogResult.Cancel
    Me.Close()
  End Sub

  Private Sub tabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabControl1.SelectedIndexChanged

    If tabControl1.SelectedIndex = 0 Then
      ' first page stuff
      Me.CancelButton = cmdCancel1
    ElseIf tabControl1.SelectedIndex = 1 Then
      ' second page stuff
      Loading = True
      setupPanel()
      Loading = False
      enableNext()
      SetPaper()
      setPreview()
      Me.CancelButton = cmdCancel
    End If
  End Sub

  Private Sub cmdShuffle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShuffle.Click

    Dim j As Integer

    shuffle(ixCal, 23)

    lstFiles.Items.Clear()
    For j = 0 To tagPath.Count - 1
      lstFiles.Items.Insert(j, Path.GetFileName(tagPath(ixCal(j))))
    Next j
    If nCalPaths >= 0 Then lstFiles.SelectedIndex = 0

  End Sub

  Private Sub frmCalendar_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    If qCal IsNot Nothing Then qCal.Dispose()
    clearBitmap(img)
    clearBitmap(gImage)
    If gBrush IsNot Nothing Then gBrush.Dispose() : gBrush = Nothing
    If gFont IsNot Nothing Then gFont.Dispose() : gFont = Nothing
  End Sub

End Class
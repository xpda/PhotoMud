
Imports vb = Microsoft.VisualBasic
Imports System.Runtime.InteropServices
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.IO

Public Class frmPrint
  Inherits Form

  ' printer declares ====================================
  Dim loading As Boolean = True

  Dim nCopies As Integer
  Dim nRows As Integer
  Dim nCols As Integer
  Dim borderSize As Double
  Dim Overlap As Double = 0.127 ' 3mm (~1/8") overlap for tiled printing

  Dim picWidth As Double ' pic x size in inches
  Dim picHeight As Double ' pic y size in inches

  Dim txtWidthChanged As Boolean
  Dim txtHeightChanged As Boolean
  Dim borderChanged As Boolean

  Dim PrinterWidth As Integer  ' printable area, adjusted for landscape mode, 100ths of inch
  Dim PrinterHeight As Integer ' printable area, adjusted for landscape mode, 100ths of inch
  Dim PrinterLeft As Integer ' printable area, adjusted for landscape mode, 100ths of inch
  Dim PrinterTop As Integer ' printable area, adjusted for landscape mode, 100ths of inch
  Dim PaperWidth As Integer ' paper width, 100ths of inch, adjusted for landscape mode
  Dim PaperHeight As Integer ' paper width, 100ths of inch, adjusted for landscape mode
  Dim pSize As PaperSize ' paper size
  Dim pArea As RectangleF ' printable area
  Dim pageNumber As Integer ' page number being printed 
  Dim nPages As Integer ' number of pages to print
  Dim nTiles As Integer ' number of pages to print

  Dim PrinterDisplayScale As Double

  Dim inCombo As Boolean
  Dim Processing As Boolean = False

  Dim gImage As Bitmap
  Dim picInfo As pictureInfo
  Dim img As Bitmap

  Dim pd As PrintDocument

  Private Sub chkFitPage_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles chkFitPage.CheckedChanged

    If loading Then Exit Sub

    If chkFitPage.Checked Then
      lbWidth.Enabled = False
      lbHeight.Enabled = False
      txtWidth.Enabled = False
      txtHeight.Enabled = False
      chkTile.Enabled = False
      chkOverlap.Enabled = False
      lbTile.Visible = False
      cmdActualSize.Enabled = False

    ElseIf Not chkFitPage.Checked Then
      lbWidth.Enabled = True
      lbHeight.Enabled = True
      txtWidth.Enabled = True
      txtHeight.Enabled = True
      If qImage Is Nothing Then
        cmdActualSize.Enabled = False
        chkTile.Enabled = False
      Else
        cmdActualSize.Enabled = True ' actual size only for single pictures
        chkTile.Enabled = True
      End If
    End If

    setTile()
    setPreview()

  End Sub

  Sub setTile()

    Static busy As Boolean = False
    Dim ix, iy As Integer

    If busy Or loading Then Exit Sub
    busy = True

    If chkTile.Enabled And chkTile.Checked Then
      lbCopies.Enabled = False
      nmNcopies.Enabled = False
      chkOverlap.Enabled = True

      ix = Ceiling((picWidth - Overlap) / ((PrinterWidth / 100) - Overlap) - 0.01) ' .01" fudge factor
      iy = Ceiling((picHeight - Overlap) / ((PrinterHeight / 100) - Overlap) - 0.01)
      nTiles = ix * iy
      If ix > 1 Or iy > 1 Then
        lbTile.Enabled = True : lbTile.Visible = True
        lbTile.Text = nTiles & " pages (" & ix & " x " & iy & ")"
      Else
        lbTile.Visible = False
      End If
    Else
      lbCopies.Enabled = True
      nmNcopies.Enabled = True
      lbTile.Visible = False
      chkOverlap.Enabled = False
    End If

    busy = False

  End Sub

  Private Sub chkMulti_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles chkMulti.CheckedChanged

    If chkMulti.Checked Then
      lbRows.Enabled = True
      lbCols.Enabled = True
      nmRows.Enabled = True
      nmCols.Enabled = True
      lbBorder.Enabled = True
      txtBorder.Enabled = True
    Else
      lbRows.Enabled = False
      lbCols.Enabled = False
      nmRows.Enabled = False
      nmCols.Enabled = False
      lbBorder.Enabled = False
      txtBorder.Enabled = False
      lbBorder.Enabled = False
      txtBorder.Enabled = False
      nRows = 1
      nCols = 1
      nmRows.Value = 1
      nmCols.Value = 1
    End If

    setPreview()

  End Sub

  Private Sub cmbHorizontal_SelectedIndexChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles cmbHorizontal.SelectedIndexChanged
    If loading Then Exit Sub
    setPreview()
  End Sub

  Private Sub cmbvertical_SelectedIndexChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles cmbVertical.SelectedIndexChanged
    If loading Then Exit Sub
    setPreview()
  End Sub

  Private Sub cmdActualSize_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdActualSize.Click

    If qImage IsNot Nothing AndAlso (qImage.HorizontalResolution > 0 And qImage.VerticalResolution > 0) Then
      picWidth = qImage.Width / qImage.HorizontalResolution
      picHeight = qImage.Height / qImage.VerticalResolution
      SetText()
    End If

  End Sub

  Private Sub cmdHelp_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdHelp.Click
    Try
      Help.ShowHelp(Me, helpFile, HelpNavigator.Topic, Me.Name & ".html")
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
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

  Private Sub cmdPrint_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdPrint.Click

    ' pd is global PrintDlgType

    Dim i As Integer
    Dim mResult As MsgBoxResult

    txtWidth_Leave(txtWidth, e)
    txtHeight_Leave(txtHeight, e)

    Me.Cursor = Cursors.WaitCursor

    If callingForm Is frmMain Then
      If frmMain.mView.picName <> "" Then
        pd.DocumentName = frmMain.mView.picName
      Else
        pd.DocumentName = AppName
      End If
    Else
      If currentpicPath <> "" Then
        pd.DocumentName = Path.GetFileName(currentpicPath)
      Else
        pd.DocumentName = AppName
      End If
    End If

    If qImage IsNot Nothing Then img = qImage.Clone ' convert to img for printing

    If qImage IsNot Nothing And i = 0 And chkTile.Enabled And chkTile.Checked And nTiles > 1 Then ' print tiled image
      If chkOverlap.Checked Then
        If optInches.Checked Then Overlap = 0.125 Else Overlap = 0.127
      Else
        Overlap = 0
      End If

      mResult = MsgBox(nCopies * nTiles & " pages will be printed.", MsgBoxStyle.OkCancel)
      If mResult = MsgBoxResult.Cancel Then
        Me.Cursor = Cursors.Default
        Exit Sub
      End If

      For i = 1 To nCopies
        pageNumber = 0
        AddHandler pd.PrintPage, AddressOf printTilePage
        pd.Print()
      Next i

    ElseIf i = 0 Or qImage Is Nothing Then ' print non-tiled
      If qImage Is Nothing Then ' multifile
        nPages = Ceiling(nCopies * tagPath.Count / (nCols * nRows))
      Else ' print only qimage
        nPages = Ceiling(nCopies / (nCols * nRows))
      End If
      pageNumber = 0
      AddHandler pd.PrintPage, AddressOf printPage
      pd.Print()
    End If

    Me.Cursor = Cursors.Default
    ExitForm()

  End Sub

  Sub printPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

    Dim j, i, k, n As Integer
    Dim iErr As Integer = 0
    Dim msg As String = ""
    Dim rBox, rPic As Rectangle
    Dim pWidth, pHeight As Integer
    Dim cellWidth, cellHeight As Double
    Dim Bord As Double ' border size 
    Dim specWidth, specHeight As Double

    e.Graphics.ResetTransform()
    e.Graphics.InterpolationMode = InterpolationMode.High

    pArea = e.Graphics.VisibleClipBounds ' pd.printablearea is not accurate. This is better, but only available here.
    PrinterTop = pArea.Top ' these are in inches*100, the same scale used for output
    PrinterLeft = pArea.Left
    PrinterWidth = pArea.Width
    PrinterHeight = pArea.Height
    'If optLandscape.Checked Then  ' landscape - swap x and y for these settings 3-22-09
    '  i = PrinterWidth : PrinterWidth = PrinterHeight : PrinterHeight = i
    '  i = PrinterLeft : PrinterLeft = PrinterTop : PrinterTop = i
    '  End If

    cellWidth = PrinterWidth / nCols
    cellHeight = PrinterHeight / nRows

    If nRows > 1 Or nCols > 1 Then Bord = borderSize * 100 Else Bord = 0
    specWidth = picWidth * 100
    specHeight = picHeight * 100

    For j = 0 To nRows - 1
      For i = 0 To nCols - 1
        k = (pageNumber * nRows * nCols) + j * nCols + i + 1
        If qImage Is Nothing Then ' multifile
          If k > tagPath.Count * nCopies Then Exit For
        Else ' print only qimage
          If k > nCopies Then Exit For
        End If
        rBox.Width = cellWidth
        rBox.Height = cellHeight
        rBox.X = i * cellWidth + PrinterLeft
        rBox.Y = j * cellHeight + PrinterTop

        n = (pageNumber * nRows * nCols + j * nCols + i) \ nCopies ' n is the image number in tagpath (if used)
        ' get image size
        If qImage IsNot Nothing Then
          pWidth = qImage.Width  ' qimage is a copy of the current image from the calling form. img is already assigned if qimage is being printed.
          pHeight = qImage.Height
          iErr = 0
        Else ' load the image and get the size
          img = readBitmap(tagPath(n), msg)
          If img Is Nothing Then iErr = -1
        End If
        If iErr >= 0 Then
          pWidth = img.Width
          pHeight = img.Height
          iErr = positionPic(pWidth, pHeight, specWidth, specHeight, Bord, rBox, rPic)
          If iErr <> 0 Then msg = "Printing position error."
        End If

        If iErr >= 0 Then
          Try
            e.Graphics.DrawImage(img, rPic)  ' actually do the printing (or at least draw to the print buffer)
          Catch ex As Exception
            MsgBox("Printing error: " & ex.Message)
          End Try

        End If
      Next i
    Next j
    If iErr <> 0 Then
      MsgBox(msg)
    End If

    'Dim p(5) As PointF
    'p(0) = New PointF(pArea.X, pArea.Y)
    'p(1) = New PointF(pArea.Width, p(0).Y)
    'p(2) = New PointF(p(1).X, pArea.Height)
    'p(3) = New PointF(p(0).X, p(2).Y)
    'p(4) = New PointF(p(0).X, p(0).Y)
    'p(5) = New PointF(p(2).X, p(2).Y)
    'g.DrawLines(Pens.DarkGreen, p)

    pageNumber = pageNumber + 1
    If pageNumber >= nPages Then
      e.HasMorePages = False
      clearBitmap(img)
    Else
      e.HasMorePages = True
    End If

  End Sub

  Sub printTilePage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

    Dim nx, ny As Integer ' number of pages per direction
    Dim ix, iy As Integer ' coordinates of current page
    Dim r As RectangleF  ' coordinates of image, offset for printing

    e.Graphics.ResetTransform()
    e.Graphics.InterpolationMode = InterpolationMode.High

    nx = Ceiling((picWidth - Overlap) / ((e.Graphics.VisibleClipBounds.Width / 100) - Overlap))
    ny = Ceiling((picHeight - Overlap) / ((e.Graphics.VisibleClipBounds.Height / 100) - Overlap))

    ix = pageNumber Mod nx
    iy = pageNumber \ (ny * nx)
    r.X = -(ix * e.Graphics.VisibleClipBounds.Width - ix * Overlap * 100)
    r.Y = -(iy * e.Graphics.VisibleClipBounds.Height - iy * Overlap * 100)
    r.Width = picWidth * 100
    r.Height = picHeight * 100

    ' img is already converted
    e.Graphics.DrawImage(img, r)  ' actually do the printing (or at least draw to the print buffer)

    pageNumber = pageNumber + 1
    If pageNumber >= nx * ny Then e.HasMorePages = False Else e.HasMorePages = True

  End Sub

  Private Sub cmbPrinter_SelectedIndexChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles cmbPrinter.SelectedIndexChanged

    If inCombo Or loading Then Exit Sub
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

  Private Sub frmPageSetup_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    Dim s As String = ""
    Dim sp As String

    helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    loading = True
    pd = New PrintDocument

    nRows = 1
    nCols = 1

    cmbHorizontal.Items.Clear()
    cmbHorizontal.Items.Insert(0, "Center")
    cmbHorizontal.Items.Insert(1, "Left")
    cmbHorizontal.Items.Insert(2, "Right")
    cmbHorizontal.Text = "Center"

    cmbVertical.Items.Clear()
    cmbVertical.Items.Insert(0, "Center")
    cmbVertical.Items.Insert(1, "Top")
    cmbVertical.Items.Insert(2, "Bottom")
    cmbVertical.Text = "Center"

    ' set form fields
    chkFitPage.Checked = iniPrintFit
    picWidth = iniPrintWidth
    picHeight = iniPrintHeight
    If pd.DefaultPageSettings.PrintableArea.Width / 100 < picWidth Or pd.DefaultPageSettings.PrintableArea.Height / 100 < picHeight Then chkFitPage.Checked = True
    nCopies = iniPrintNCopies
    nmNcopies.Value = nCopies
    cmbHorizontal.SelectedIndex = iniPrintHJustify
    cmbVertical.SelectedIndex = iniPrintVJustify

    If qImage Is Nothing Then ' print tagged files
      Me.Text = "Print Photos"

      If tagPath.Count <= 0 Then
        MsgBox("No photos are tagged to be printed.", MsgBoxResult.Ok)
        ExitForm()
        Exit Sub
      Else
        lbCount.Text = tagPath.Count & " photos are tagged to be printed."
      End If

    Else
      Set32bppPArgb(qImage)
      Me.Text = "Print Photo"
      lbCount.Text = ""
    End If

    cmbPrinter.Items.Clear()

    sp = pd.PrinterSettings.PrinterName ' default (or previous) printer name
    For Each s In PrinterSettings.InstalledPrinters
      cmbPrinter.Items.Add(s)
      If (s = sp) Then ' use previous (or default) printer
        cmbPrinter.SelectedIndex = cmbPrinter.Items.Count - 1
      End If
    Next s

    PrinterDisplayScale = iniPrintUnits
    If PrinterDisplayScale = 1 Then ' inch
      optInches.Checked = True
      optUnits_CheckedChanged(optInches, New EventArgs())
    Else ' mm
      optMillimeters.Checked = True
      optUnits_CheckedChanged(optMillimeters, New EventArgs())
    End If

    If iniPrintOrientation <= 1 Then ' Should be 1 or 2, 1 = portrait
      optPortrait.Checked = True
      pd.DefaultPageSettings.Landscape = False
    Else ' landscape
      optLandscape.Checked = True
      pd.DefaultPageSettings.Landscape = True
    End If

    If (qImage Is Nothing) And (tagPath.Count > 1) Then
      chkMulti.Enabled = True
      chkMulti.Checked = False
    Else
      chkMulti.Enabled = False ' only enabled when ncopies > 1
    End If

    nmRows.Enabled = False
    nmCols.Enabled = False
    lbRows.Enabled = False
    lbCols.Enabled = False
    lbBorder.Enabled = False
    txtBorder.Enabled = False
    chkTile.Enabled = False
    chkOverlap.Enabled = False
    lbTile.Visible = False
    nmRows.Value = 1
    nmCols.Value = 1

    borderSize = 0.125
    If PrinterDisplayScale = 1 Then ' inches
      txtBorder.Text = Format(borderSize * PrinterDisplayScale, "##0.0##")
    Else ' millimeters
      txtBorder.Text = Format(borderSize * PrinterDisplayScale, "##,##0.0")
    End If

    loading = False

    SetText()
    getPrinterStuff()
    SetPaper()
    setPreview()

  End Sub

  Private Sub optOrientation_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles optPortrait.CheckedChanged, optLandscape.CheckedChanged

    If loading Then Exit Sub

    If Sender.Checked Then
      If Sender Is optPortrait Then
        pd.DefaultPageSettings.Landscape = False
      Else ' landscape -- swap x and y for these settings
        pd.DefaultPageSettings.Landscape = True
      End If

      SetPaper()
      setPreview()
    End If
  End Sub
  Private Sub optUnits_CheckedChanged(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles optInches.CheckedChanged, optMillimeters.CheckedChanged

    If loading Then Exit Sub

    If Sender.Checked Then

      If Sender Is optInches Then ' inches
        PrinterDisplayScale = 1
        txtHeight.Text = Format(picHeight * PrinterDisplayScale, "##0.0")
        txtWidth.Text = Format(picWidth * PrinterDisplayScale, "##0.0")
        txtBorder.Text = Format(borderSize * PrinterDisplayScale, "##0.0##")
        lbUnits0.Text = "(in.)"
        lbUnits1.Text = "(in.)"
        chkOverlap.Text = "Overlap Tiles 1/8"""
      Else ' millimeters
        PrinterDisplayScale = 25.4
        txtHeight.Text = Format(picHeight * PrinterDisplayScale, "##,###")
        txtWidth.Text = Format(picWidth * PrinterDisplayScale, "##,###")
        txtBorder.Text = Format(borderSize * PrinterDisplayScale, "##,##0.0")
        lbUnits0.Text = "(mm)"
        lbUnits1.Text = "(mm)"
        chkOverlap.Text = "Overlap Tiles 3mm"
      End If

      setPreview()

    End If
  End Sub

  Sub cmdCancel_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdCancel.Click

    ExitForm()

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
      If cmbPrinter.Items(i) = s Then
        If cmbPrinter.SelectedIndex <> i Then cmbPrinter.SelectedIndex = i
        Exit For
      End If
    Next i

    pArea = pd.DefaultPageSettings.PrintableArea  ' slow
    pSize = pd.DefaultPageSettings.PaperSize ' slow

  End Sub

  Sub SetPaper()
    ' sets the "paper," shadow, and pview position and size on the form

    Dim i As Integer
    Dim ipad As Integer
    Dim heightallowed As Double
    Dim widthallowed As Double
    Dim z As Double
    Dim vdif, hdif As Integer

    If loading Then Exit Sub

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
    If PrinterWidth <= 0 Then PrinterWidth = pView.ClientSize.Width
    If PrinterHeight <= 0 Then PrinterHeight = pView.ClientSize.Height
    If PaperWidth <= 0 Then PaperWidth = pView.ClientSize.Width
    If PaperHeight <= 0 Then PaperHeight = pView.ClientSize.Height

    ipad = 20
    widthallowed = Me.ClientSize.Width - (Frame5.Left + Frame5.Width) - ipad * 2
    heightallowed = cmdHelp.Top - ipad * 2

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
    pnPaperFrame.Left = (widthallowed - pnPaperFrame.Width) / 2 + (Frame5.Left + Frame5.Width) + ipad
    pnPaperFrame.Top = (heightallowed - pnPaperFrame.Height) / 2 + ipad

    ' add paper margin and set pview
    z = pnPaper.Width / PrinterWidth
    pView.Left = (PaperWidth - PrinterWidth) * z + pnPaper.ClientRectangle.X
    pView.Top = (PaperHeight - PrinterHeight) * z + pnPaper.ClientRectangle.Y
    pView.Width = pnPaper.ClientRectangle.Width - (PaperWidth - PrinterWidth) * z * 2
    pView.Height = pnPaper.ClientRectangle.Height - (PaperHeight - PrinterHeight) * z * 2

  End Sub

  Sub SetText()

    Processing = True
    If PrinterDisplayScale = 25.4 Then
      txtHeight.Text = Format(picHeight * 25.4, "##,##0")
      txtWidth.Text = Format(picWidth * 25.4, "##,##0")
      txtBorder.Text = Format(borderSize * 25.4, "##,##0")
    Else
      txtHeight.Text = Format(picHeight, "#,##0.0##")
      txtWidth.Text = Format(picWidth, "#,##0.0##")
      txtBorder.Text = Format(borderSize, "#,##0.0##")
    End If

    nmCols.Value = nCols
    nmRows.Value = nRows
    Processing = False

  End Sub

  Private Sub txtBorder_TextChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles txtBorder.TextChanged
    If loading Then Exit Sub
    borderChanged = True
  End Sub

  Private Sub txtBorder_Enter(ByVal Sender As Object, ByVal e As EventArgs) Handles txtBorder.Enter
    txtBorder.SelectionStart = 0
    txtBorder.SelectionLength = Len(txtBorder.Text)
  End Sub

  Private Sub txtBorder_Leave(ByVal Sender As Object, ByVal e As EventArgs) Handles txtBorder.Leave

    Dim max As Double
    Dim s As String
    Dim x As Double

    If Me.ActiveControl Is cmdCancel Then Exit Sub

    If borderChanged Then
      If PrinterDisplayScale = 1 Then
        max = 0.5
        s = "inches"
      Else
        max = 13
        s = "mm"
      End If

      If checknumber((txtBorder.Text), 0, max, x) Then
        borderSize = x / PrinterDisplayScale
      Else
        MsgBox("Enter the border size in " & s & ", from 0 to " & max)
        txtBorder.select()
        Exit Sub
      End If

      setPreview()
    End If

  End Sub

  Private Sub txtWidth_Leave(ByVal Sender As Object, ByVal e As EventArgs) Handles txtWidth.Leave

    Dim x As Double
    Dim xMax As Double

    If Me.ActiveControl Is cmdCancel Then Exit Sub

    If txtWidthChanged Then
      If chkTile.Checked And chkTile.Enabled Then xMax = 32000 Else xMax = PrinterWidth * PrinterDisplayScale / 100
      If checknumber((txtWidth.Text), 0.0001, xMax, x) Then
        picWidth = x / PrinterDisplayScale
        If (qImage IsNot Nothing) Then picHeight = qImage.Height * picWidth / qImage.Width
      Else
        MsgBox("Please enter a number between 0 and " & xMax & ".")
        txtWidth.select()
        Exit Sub
      End If

      If chkTile.Checked And chkTile.Enabled Then setTile()
      SetText()
      setPreview()

      txtWidthChanged = False
    End If

  End Sub

  Private Sub txtHeight_Leave(ByVal Sender As Object, ByVal e As EventArgs) Handles txtHeight.Leave

    Dim x As Double
    Dim xmax As Double

    If Me.ActiveControl Is cmdCancel Then Exit Sub

    If txtHeightChanged Then
      If chkTile.Checked And chkTile.Enabled Then xmax = 32000 Else xmax = PrinterHeight * PrinterDisplayScale / 100
      If checknumber((txtHeight.Text), 0.0001, 32000, x) Then
        picHeight = x / PrinterDisplayScale
        If (qImage IsNot Nothing) Then picWidth = qImage.Width * picHeight / qImage.Height
      Else
        MsgBox("Please enter a number between 0 and " & xmax & ".")
        txtHeight.select()
        Exit Sub
      End If

      If chkTile.Checked And chkTile.Enabled Then setTile()
      SetText()
      setPreview()

      txtHeightChanged = False
    End If

  End Sub

  Private Sub nmNcopies_valuechanged(ByVal Sender As Object, ByVal e As EventArgs) Handles nmNcopies.ValueChanged

    Static Dim busy As Boolean

    If busy Or loading Then Exit Sub
    busy = True

    nCopies = nmNcopies.Value

    If ((qImage Is Nothing) And (tagPath.Count > 1)) Or (nCopies > 1) Then
      If Not chkMulti.Enabled Then ' only change it if it needs it
        chkMulti.Enabled = True
        If chkMulti.Checked Then
          nmCols.Enabled = True
          nmRows.Enabled = True
          lbCols.Enabled = True
          lbRows.Enabled = True
          lbBorder.Enabled = True
          txtBorder.Enabled = True
        Else
          nmCols.Enabled = False
          nmRows.Enabled = False
          lbCols.Enabled = False
          lbRows.Enabled = False
          lbBorder.Enabled = False
          txtBorder.Enabled = False
        End If
      End If
    Else
      If chkMulti.Enabled Then ' only change it if it needs it
        chkMulti.Enabled = False
        nmCols.Enabled = False
        nmRows.Enabled = False
        lbCols.Enabled = False
        lbRows.Enabled = False
        lbBorder.Enabled = False
        txtBorder.Enabled = False
      End If
    End If

    setPreview()
    busy = False

  End Sub

  Private Sub nm_valueChanged(ByVal Sender As Object, ByVal e As EventArgs) _
    Handles nmRows.ValueChanged, nmCols.ValueChanged

    Static busy As Boolean
    Dim max As Integer
    If loading Or Processing Or busy Then Exit Sub
    busy = True

    If Not chkFitPage.Checked And (Not chkTile.Enabled Or Not chkTile.Checked) Then ' make sure there's enough room
      If Sender Is nmCols Then
        max = Int((PrinterWidth / 100) / (picWidth + borderSize * 2))
        If max <= 0 Then max = 1
        If max < nmCols.Value Then nmCols.Value = max
      Else ' nmrows
        max = Int((PrinterHeight / 100) / (picHeight + borderSize * 2))
        If max <= 0 Then max = 1
        If max < nmRows.Value Then nmRows.Value = max
      End If
    End If

    nRows = nmRows.Value
    nCols = nmCols.Value
    setPreview()
    busy = False

  End Sub

  Private Sub txtWidth_TextChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles txtWidth.TextChanged
    If loading Or Processing Then Exit Sub
    txtWidthChanged = True
  End Sub

  Private Sub txtHeight_TextChanged(ByVal Sender As Object, ByVal e As EventArgs) Handles txtHeight.TextChanged
    If loading Or Processing Then Exit Sub
    txtHeightChanged = True
  End Sub

  Private Sub txtWidth_Enter(ByVal Sender As Object, ByVal e As EventArgs) Handles txtWidth.Enter
    txtWidth.SelectionStart = 0
    txtWidth.SelectionLength = Len(txtWidth.Text)
  End Sub

  Private Sub txtHeight_Enter(ByVal Sender As Object, ByVal e As EventArgs) Handles txtHeight.Enter
    txtHeight.SelectionStart = 0
    txtHeight.SelectionLength = Len(txtHeight.Text)
  End Sub

  Sub ExitForm()
    If chkSaveSettings.Checked Then SaveSettings()
    Me.DialogResult = DialogResult.OK
    Me.Close()
  End Sub

  Sub SaveSettings()

    iniPrintFit = chkFitPage.Checked
    iniPrintWidth = picWidth
    iniPrintHeight = picHeight
    If optPortrait.Checked Then iniPrintOrientation = 1 Else iniPrintOrientation = 2
    iniPrintNCopies = nCopies
    iniPrintHJustify = cmbHorizontal.SelectedIndex
    iniPrintVJustify = cmbVertical.SelectedIndex
    iniPrintUnits = PrinterDisplayScale

  End Sub

  Function positionPic(ByVal pWidth As Integer, ByVal pHeight As Integer, ByVal specWidth As Integer, ByVal specHeight As Integer, ByVal bord As Integer, _
     ByVal rBox As Rectangle, ByRef rPic As Rectangle) As Integer
    ' returns rpic, location for the pic inside the space rbox
    ' subtracts border from rbox, uses cmb alignment

    Dim w, h As Integer ' useable box size
    Dim pw, ph As Integer ' useable specified box size

    positionPic = -1
    rPic.Width = pWidth
    rPic.Height = pHeight
    If pWidth <= 0 Or pHeight <= 0 Then Exit Function

    w = rBox.Width - bord * 2 ' box useable size
    h = rBox.Height - bord * 2

    If chkFitPage.Checked Or (chkTile.Enabled And chkTile.Checked) Then
      If pWidth / pHeight >= w / h Then   ' picture limited by width
        rPic.Width = pWidth * w / pWidth
        rPic.Height = pHeight * w / pWidth
      Else   ' picture limited by height
        rPic.Width = pWidth * h / pHeight
        rPic.Height = pHeight * h / pHeight
      End If
    Else ' globals picwidth and picheight specify the size
      pw = specWidth
      ph = specHeight
      If pWidth / pHeight >= pw / ph Then   ' picture limited by width
        rPic.Width = pWidth * pw / pWidth
        rPic.Height = pHeight * pw / pWidth
      Else   ' picture limited by height
        rPic.Width = pWidth * ph / pHeight
        rPic.Height = pHeight * ph / pHeight
      End If
    End If
    If rPic.Width <= 0 Or rPic.Height <= 0 Then Exit Function
    If rPic.Width > w Or rPic.Height > h Then
      positionPic = 1 ' picture doesn't fit
    Else
      positionPic = 0
    End If

    ' place the image
    Select Case cmbHorizontal.Text
      Case "Center"
        rPic.X = rBox.X + bord + (w - rPic.Width) / 2
      Case "Left"
        rPic.X = rBox.X + bord
      Case "Right"
        rPic.X = rBox.X + bord + (w - rPic.Width)
    End Select

    Select Case cmbVertical.Text
      Case "Center"
        rPic.Y = rBox.Y + bord + (h - rPic.Height) / 2
      Case "Top"
        rPic.Y = rBox.Y + bord
      Case "Bottom"
        rPic.Y = rBox.Y + bord + (h - rPic.Height)
    End Select

  End Function

  Sub setPreview()

    ' loads a single file onto the picture to display on the screen
    ' if multiple files or copies, put the same file in all the spaces.

    Static busy As Boolean = False
    Dim j, i, k, n As Integer
    Dim iErr As Integer = 0
    Dim z As Double
    Dim rBox, rPic As Rectangle
    Dim pWidth, pHeight As Integer
    Dim cellWidth, cellHeight As Double
    Dim paperXoff, paperYoff As Integer
    Dim Bord As Double ' border size 
    Dim specWidth, specHeight As Double
    Dim nx, ny As Integer
    Dim ix, iy As Single
    Dim msg As String = ""

    If busy Then Exit Sub
    busy = True

    Me.Cursor = Cursors.WaitCursor
    pView.InterpolationMode = InterpolationMode.High
    pView.newBitmap(pView.ClientSize.Width, pView.ClientSize.Height, Color.White)
    pView.Zoom(0)

    z = pView.ClientSize.Width / (PaperWidth / 100) ' factor from inches to pview

    cellWidth = PrinterWidth * z / nCols / 100
    cellHeight = PrinterHeight * z / nRows / 100
    paperXoff = PrinterLeft * z / 100
    paperYoff = PrinterTop / 2 * z / 100

    If nRows > 1 Or nCols > 1 Then Bord = borderSize * z Else Bord = 0
    specWidth = picWidth * z
    specHeight = picHeight * z

    For j = 0 To nRows - 1
      For i = 0 To nCols - 1
        k = j * nCols + i + 1
        If qImage Is Nothing Then ' multiple files
          If k > tagPath.Count * nCopies Then Exit For
        Else ' print qimage only
          If k > nCopies Then Exit For
        End If
        rBox.Width = cellWidth
        rBox.Height = cellHeight
        rBox.X = i * cellWidth + paperXoff
        rBox.Y = j * cellHeight + paperYoff
        n = (j * nCols + i) \ nCopies
        If qImage Is Nothing Then ' qimage is a copy of the current image from the calling form
          picInfo = getPicinfo(tagPath(n), True)
          If picInfo.isNull OrElse (pWidth <= 0 Or pHeight <= 0) Then
            pWidth = 800 ' just something to prevent error
            pHeight = 600
            iErr = -1
          Else
            pWidth = picInfo.Width
            pHeight = picInfo.Height
          End If
        Else
          pWidth = qImage.Width
          pHeight = qImage.Height
        End If

        k = positionPic(pWidth, pHeight, specWidth, specHeight, Bord, rBox, rPic)

        If i = 0 And j = 0 And chkFitPage.Checked Then ' set size textboxes, first photo only
          picHeight = rPic.Height / z
          picWidth = rPic.Width / z
          SetText()
        End If
        If k <> 0 Then iErr = k

        If qImage Is Nothing Then ' qimage is a copy of the current image from the calling form
          ' use tagpath
          gImage = Nothing
          n = (j * nCols + i) \ nCopies
          ' load the picture and resize
          gImage = readBitmap(tagPath(n), msg)
          pView.ResizeBitmap(New Size(rPic.Width, rPic.Height), gImage, gImage)

          If gImage Is Nothing Then ' last resort -- display a box.
            gImage = New Bitmap(rPic.Width, rPic.Height, PixelFormat.Format32bppPArgb)
            Using g As Graphics = Graphics.FromImage(gImage)
              g.Clear(Color.FromArgb(255, 150, 150, 200))
            End Using
          End If

        Else
          pView.ResizeBitmap(New Size(rPic.Width, rPic.Height), qImage, gImage)
        End If

        Using g As Graphics = Graphics.FromImage(pView.Bitmap)
          g.DrawImage(gImage, rPic)
          pView.Zoom()
        End Using
      Next i
    Next j

    If chkTile.Enabled And chkTile.Checked And nTiles > 1 Then ' draw tile lines
      nx = Ceiling((picWidth - Overlap) / ((PrinterWidth / 100) - Overlap) - 0.01) ' .01" fudge factor
      ny = Ceiling((picHeight - Overlap) / ((PrinterHeight / 100) - Overlap) - 0.01)

      Using g As Graphics = Graphics.FromImage(pView.Bitmap)
        For i = 1 To nx - 1
          ix = rPic.X + i * PrinterWidth / 100 * (rPic.Width / picWidth)
          g.DrawLine(Pens.DarkBlue, ix, rPic.Y, ix, rPic.Y + rPic.Height)
        Next i
        For i = 1 To ny - 1
          iy = rPic.Y + i * PrinterHeight / 100 * (rPic.Width / picWidth)
          g.DrawLine(Pens.DarkBlue, rPic.X, iy, rPic.X + rPic.Width, iy)
        Next i
      End Using

    End If

    busy = False
    Me.Cursor = Cursors.Default

  End Sub

  Private Sub chkTile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTile.CheckedChanged
    setTile()
    setPreview()
  End Sub

  Private Sub chkOverlap_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOverlap.CheckedChanged
    setTile()
    setPreview()
  End Sub

  Private Sub frmPrint_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
    If loading Then Exit Sub
    SetPaper()
    setPreview()
  End Sub

  Private Sub form_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
    clearBitmap(qImage)
    clearBitmap(img)
    clearBitmap(gImage)
    If pd IsNot Nothing Then pd.Dispose() : pd = Nothing
  End Sub

End Class
﻿Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Collections.Generic
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Math
Imports System.IO
'Imports ImageMagick

Imports System.Data
Imports MySql.Data.MySqlClient

Imports System.Xml
Imports System.Xml.Serialization

'Photo Mud is licensed under Creative Commons BY-NC-SA 4.0
'https://creativecommons.org/licenses/by-nc-sa/4.0/

' alter user bugs with login=bugs
' (query after database restore, sql server only)

' backup database
'   cd \Program Files\MySQL\MySQL Server 5.5\bin
'   mysqldump -u root -pp taxa > c:\bugs\121028dump.sql

' set image counts!
'   select id from taxatable where taxon = "arthropoda" into @id;
'   setImageCount(@id, @i);

' update bugguide ids:
'UPDATE images, bugguide
'    SET images.bugguide=bugguide.bugguideid
'	where (images.filename = bugguide.filename or 
'	bugguide.filename like concat(mid(images.filename, 1, length(images.filename)-1), "%"))
'	and abs(datediff(images.photodate, bugguide.photodate)) <= 2;
'UPDATE images, bugguide
'    SET images.size=bugguide.size
'	where (images.filename = bugguide.filename or 
'	bugguide.filename like concat(mid(images.filename, 1, length(images.filename)-1), "%"))
'	and (images.size = "")
'	and abs(datediff(images.photodate, bugguide.photodate)) <= 2;

' updates:
' update imagecounters
' update bugguide site stuff?
' move data to server -- mysqldump (c:\bugs\dump.bat)
' file convert to 480 small
' file convert to 240 smaller
' upload new images
' rebuild and upload bugpage.aspx


Public Class frmBugPhotos

  Inherits Form

  Dim picpath As String

  Dim editMode As Boolean ' editing the picture in inibugpath
  Dim processing As Boolean = False
  Dim Loading As Boolean = True
  Dim cropping As Boolean = False
  Dim measuring As Boolean = False
  Dim picChanged As Boolean = False
  Dim dataChanged As Boolean = False
  Dim filenameChanged As Boolean = False
  Dim imagesetChanged As Boolean = False
  Dim rbX, rbY As Integer ' rubber box origin

  Dim filenames As New List(Of String)
  Dim ix As New List(Of Integer)
  Dim iPic As Integer
  Dim pComments As List(Of PropertyItem)

  Dim taxonid As String
  Dim txTaxid As String
  Dim iElevation As Integer
  Dim abort As Boolean = False

  Dim Crlf As String = Chr(13) & Chr(10)
  Dim mResult As MsgBoxResult

  Private Sub cmdSave_Click(ByVal Sender As Object, ByVal e As EventArgs) Handles cmdSaveAll.Click, cmdSaveData.Click

    Dim i As Integer

    If processing Then Exit Sub

    If txTaxon.Text.Trim = "" Then
      MsgBox("Blank taxon. Dummy.")
      Exit Sub
    End If

    processing = True
    If Sender Is cmdSaveAll Then
      i = savePic()
      If i < 0 Then
        processing = False
        Exit Sub
      End If
      If i > 0 Then ' saved OK
        pView.BackColor = System.Drawing.Color.CadetBlue
        editMode = True
      End If
    End If

    i = saveData()
    If i > 0 Then setChkLink()

    processing = False

  End Sub

  Function saveData() As Integer

    ' 0 = not saved, 1 = saved

    Dim cmd As MySqlCommand
    Dim sql As String
    Dim fName As String
    Dim id As String
    Dim k As Integer
    Dim matches As List(Of taxrec)
    Dim setID As Integer
    Dim oldTaxid As String = ""

    Me.Cursor = Cursors.WaitCursor
    fName = Trim(txFileName.Text)

    ' don't save unless the file is there
    If Not File.Exists(getTargetPath) Then
      MsgBox("The image has not been saved.")
      Me.Cursor = Cursors.Default
      Return 0
    End If

    Try
      ' does it exist?
      id = getScalar("select imageid from images where filename = @parm1", fName)
      If id <> Nothing And id <> "" Then ' record already exists if id <> ""
        mResult = MsgBox("Database record for " & fName & " already exists. Overwrite?", MsgBoxStyle.YesNoCancel)
        If mResult <> MsgBoxResult.Yes Then
          Me.Cursor = Cursors.Default
          Return 0
        End If
        oldTaxid = getScalar("select taxonid from images where imageid = @parm1", id)
      Else
        oldTaxid = ""
      End If

      If txTaxid = "" Then
        ' make sure there is one taxon
        matches = TaxonSearch(txTaxon.Text, False)
        taxonid = ""

        If matches.Count <= 0 Then
          MsgBox(txTaxon.Text & " is not in the Database.", MsgBoxStyle.OkOnly)
          cmdTaxon_Click(Nothing, Nothing)
          Me.Cursor = Cursors.Default
          Return 0
        End If

        If matches.Count = 1 OrElse (matches.Count = 2 And eqstr(matches(1).rank, "subgenus")) Then
          taxonid = matches(0).taxid
          txCommon.Text = matches(0).descr

        ElseIf matches.Count > 1 Then
          taxonid = ""
          MsgBox("There is more than one " & txTaxon.Text & " in the Database.", MsgBoxStyle.OkOnly)
          cmdTaxon_Click(Nothing, Nothing)
          Me.Cursor = Cursors.Default
          Return 0
        End If
      End If

      Using conn As New MySqlConnection(iniDBConnStr)
        conn.Open()

        If id <> "" Then
          ' "on duplicate key update" autoincrements, messes up stuff.
          sql = "update images set " & _
            " filename = @filename, " & _
            " photodate = @photodate, " & _
            " dateadded = @dateadded, " & _
            " modified = @modified, " & _
            " taxonid = @taxonid, " & _
            " location = @location, " & _
            " county = @county, " & _
            " state = @state, " & _
            " country = @country, " & _
            " size = @size, " & _
            " gps = @gps, " & _
            " elevation = @elevation, " & _
            " rating = @rating, " & _
            " confidence = @confidence, " & _
            " remarks = @remarks, " & _
            " bugguide = @bugguide," & _
            " inaturalist = @inaturalist" & _
            " where imageid = @id"

        Else
          sql = "insert into images " & _
            "(filename, photodate, dateadded, modified, taxonid, location, county, state, country, size, " & _
              "gps, elevation, rating, confidence, remarks, bugguide, inaturalist, originalpath) " & _
              "values (@filename, @photodate, @dateadded, @modified, @taxonid, @location, @county, @state, @country, @size, " & _
              "@gps, @elevation, @rating, @confidence, @remarks, @bugguide, @inaturalist, @originalpath)"
        End If

        cmd = New MySqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@filename", fName)
        If IsDate(txDate.Text) Then cmd.Parameters.AddWithValue("@photodate", CDate(txDate.Text))
        cmd.Parameters.AddWithValue("@dateadded", CDate(txDateAdded.Text))
        cmd.Parameters.AddWithValue("@modified", Now)
        cmd.Parameters.AddWithValue("@taxonid", taxonid)
        cmd.Parameters.AddWithValue("@location", Trim(txLocation.Text))
        cmd.Parameters.AddWithValue("@county", Trim(txCounty.Text))
        cmd.Parameters.AddWithValue("@state", Trim(txState.Text))
        cmd.Parameters.AddWithValue("@country", Trim(txCountry.Text))
        cmd.Parameters.AddWithValue("@size", Trim(txSize.Text))
        cmd.Parameters.AddWithValue("@gps", Trim(txGPS.Text))
        cmd.Parameters.AddWithValue("@elevation", iElevation)
        If IsNumeric(txRating.Text) Then cmd.Parameters.AddWithValue("@rating", Int(txRating.Text)) Else cmd.Parameters.AddWithValue("@rating", "0")
        If IsNumeric(txConfidence.Text) Then cmd.Parameters.AddWithValue("@confidence", Int(txConfidence.Text)) Else cmd.Parameters.AddWithValue("@confidence", "0")
        cmd.Parameters.AddWithValue("@remarks", Trim(txRemarks.Text))
        If IsNumeric(txBugguide.Text) Then cmd.Parameters.AddWithValue("@bugguide", Int(txBugguide.Text)) Else cmd.Parameters.AddWithValue("@bugguide", 0)
        If IsNumeric(txiNaturalist.Text) Then cmd.Parameters.AddWithValue("@inaturalist", Int(txiNaturalist.Text)) Else cmd.Parameters.AddWithValue("@inaturalist", 0)
        cmd.Parameters.AddWithValue("@originalpath", Trim(txOriginalPath.Text))
        If id <> "" Then cmd.Parameters.AddWithValue("@id", id)

        k = cmd.ExecuteNonQuery()
        If k <= 0 Then MsgBox("savedata: Could not add image.")
      End Using

    Catch ex As Exception
      If k <= 0 Then MsgBox("Error, savedata: " & ex.Message)
    End Try

    If imagesetChanged Then ' save the imageset
      If IsNumeric(txImageSet.Text) Then
        setID = txImageSet.Text
        saveImageSet(fName, setID)
      ElseIf txImageSet.Text.Trim = "" Then
        deleteImageSets(fName)
        loadimageset() ' clear the combo box
      Else
        MsgBox("Invalid Image Set ID", MsgBoxStyle.OkOnly)
      End If

    ElseIf chkLink.Checked And Not imagesetChanged Then
      linkImageSet(fName, bugPrevFilename) ' link to previous image
    End If

    If oldTaxid <> taxonid Then ' increment image counters
      If oldTaxid <> "" Then incImageCounter(oldTaxid, -1) ' decrement old
      incImageCounter(taxonid, 1) ' increment new
    End If

    ' save for F3 only if data is saved
    lastbugTaxon = txTaxon.Text
    lastbugTaxid = txTaxid
    lastbugTaxonID = taxonid
    lastbugLocation = txLocation.Text
    lastbugCounty = txCounty.Text
    lastbugState = txState.Text
    lastbugCountry = txCountry.Text
    lastbugCommon = txCommon.Text
    ' lastbugGPS = txGPS.Text
    lastbugSize = txSize.Text
    lastbugRating = txRating.Text()
    lastbugConfidence = txConfidence.Text()
    lastbugRemarks = txRemarks.Text
    lastbugBugguide = txBugguide.Text
    lastbugiNaturalist = txiNaturalist.Text
    ' lastbugLocationAutocomplete = txLocation.AutoCompleteCustomSource
    bugPrevFilename = fName

    dataChanged = False
    imagesetChanged = False
    Me.Cursor = Cursors.Default
    Return 1 ' saved

  End Function

  Sub initFields(Optional ByVal fName As String = "")

    Dim uDescription, uDate As String
    Dim sGPS, sElevation As String
    Dim s As String
    Dim s1 As String = ""
    Dim i, i1 As Integer
    Dim lens, camera As String
    Dim uComments As uExif
    Dim xLat, xLon As Double

    Dim match As New taxrec
    Dim matches As List(Of taxrec)

    Dim dr As DataRow

    Dim picInfo As pictureInfo

    processing = True
    lbPicPath.Text = Path.GetFileName(picpath)

    If fName <> "" Then ' filename passed as parameter
      txFileName.Text = fName ' fname is passed from txfilename.leave
      If File.Exists(getTargetPath()) Then
        cmdDelete.Enabled = True
        editMode = True
      Else
        cmdDelete.Enabled = False
        editMode = False
      End If

    Else
      If iniBugPath.EndsWith("\") Then iniBugPath = Mid(iniBugPath, 1, Len(iniBugPath) - 1) ' for old data
      If eqstr(iniBugPath, Path.GetDirectoryName(picpath)) Then  ' editing
        cmdDelete.Enabled = True
        editMode = True
        txFileName.Text = Path.GetFileName(picpath)

      Else ' copy from one location to another
        txFileName.Text = getTargetFilename(iniBugPath, picpath, editMode)

        If editMode Then
          cmdDelete.Enabled = True
        Else
          cmdDelete.Enabled = False
        End If
      End If


    End If

    loadInitialPic()

    uDescription = getBmpComment(propID.ImageDescription, pComments)
    uDate = getBmpComment(propID.DateTimeOriginal, pComments)

    If (eqstr(uDescription, "Minolta DSC") OrElse
        eqstr(uDescription, "OLYMPUS DIGITAL CAMERA") OrElse
        eqstr(uDescription, "LEAD Technologies Inc. V1.01")) Then uDescription = ""

    If Len(uDate) = 19 Then
      uDate = Mid(uDate, 6, 2) & "/" & Mid(uDate, 9, 2) & "/" & uDate.Substring(0, 4) & uDate.Substring(uDate.Length - 9)
    End If

    txTaxon.Text = ""
    txTaxid = ""
    txCommon.Text = ""
    txSize.Text = ""
    txRating.Text = "60"
    txConfidence.Text = "0"
    txRemarks.Text = ""
    txBugguide.Text = ""
    txiNaturalist.Text = ""

    setChkLink()

    rText0.Text = uDescription
    txDate.Text = uDate

    sGPS = "" : sElevation = "" : iElevation = 0
    getGPSLocation(pComments, sGPS, sElevation, xLat, xLon, iElevation)
    txGPS.Text = sGPS
    txElevation.Text = sElevation

    If txGPS.Text = "" Then
      cmdGPSLocate.Enabled = False
    Else
      cmdGPSLocate.Enabled = True
    End If

    If iElevation = -32768 Or iElevation = 0 Then
      txElevation.Text = ""
    Else
      txElevation.Text = iElevation.ToString
      formatElevation() ' format it
    End If

    txLocation.Text = lastbugLocation
    txCounty.Text = lastbugCounty
    txState.Text = lastbugState
    txCountry.Text = lastbugCountry
    txOriginalPath.Text = picpath ' this will be changed from the database if possible

    taxonid = "" ' global

    txDateAdded.Text = Format(Now, "MM/dd/yyyy HH:mm")


    Using ds As DataSet = getDS("select * from images where filename = @parm1 limit 1", txFileName.Text)

      If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
        dr = ds.Tables(0).Rows(0)
        If Not IsDBNull(dr("taxonid")) Then
          taxonid = dr("taxonid")
          match = getTaxrecByID(taxonid, True)
        End If

        If Not IsDBNull(dr("photodate")) Then txDate.Text = Format(dr("photodate"), "MM/dd/yyyy HH:mm:ss")
        If Not IsDBNull(dr("dateadded")) Then txDateAdded.Text = Format(dr("dateadded"), "MM/dd/yyyy")
        txLocation.Text = dr("location")
        txCounty.Text = dr("county")
        txState.Text = dr("state")
        txCountry.Text = dr("country")
        txSize.Text = dr("size")
        txRating.Text = dr("rating")
        txConfidence.Text = dr("confidence")
        txRemarks.Text = dr("remarks")
        txBugguide.Text = dr("bugguide")
        txiNaturalist.Text = dr("inaturalist")
        txSize.Text = dr("size")
        txOriginalPath.Text = dr("originalpath")
        txGPS.Text = dr("gps")

        If Not IsDBNull(dr("elevation")) Then
          iElevation = dr("elevation")
          If iElevation = -32768 Or iElevation = 0 Then
            txElevation.Text = ""
          Else
            txElevation.Text = Str(iElevation)
            formatElevation() ' format it, assign to ielevation
          End If
        Else
          iElevation = -32768
          txElevation.Text = ""
        End If

        If txGPS.Text = "" Then
          cmdGPSLocate.Enabled = False
        Else
          cmdGPSLocate.Enabled = True
        End If
      End If

      If match.taxon = "" Then  ' try to get the taxon from the jpg comment
        matches = TaxonSearch(uDescription, False)
        If matches.Count >= 1 Then
          taxonid = matches(0).taxid
          txTaxid = matches(0).taxid
          txTaxon.Text = matches(0).taxon
          txCommon.Text = getDescr(matches(0), False)
        Else
          taxonid = ""
        End If
      End If
      txTaxid = match.taxid
      txTaxon.Text = match.taxon
      txCommon.Text = getDescr(match, False)

      ' put into the label the count of database records for this original path
      ' i = getScalar("select count(*) from images where originalpath = @parm1", txOriginalPath.Text)

      ' add the setid, if there is one
      i = getScalar("select setid from imagesets where imageid = (select imageid from images where filename = @parm1)", txFileName.Text)
      If i > 0 Then
        txImageSet.Text = i
      Else
        txImageSet.Text = ""
      End If
      loadimageset()

      If i = 0 Then ' no records
        lbOriginalPath.Text = "Original Path:"
      End If

      ' check to see if it was taken with manual focus, if so enable cmdMeasure. Specific for Panasonic GX-1 or GH-1
      s = ""
      picInfo = getPicinfo(picpath)
      uComments = readComments(picpath, True, True)
      pComments = readPropertyItems(picpath)
      formatExifComments(True, False, False, False, s, uComments, picInfo, pComments) ' s has the answer

      i = InStr(s, "Focus Mode:")
      If i > 0 Then
        i += 12
        i1 = InStr(i, s, "\")
        s1 = Mid(s, i, i1 - i)
      End If
      If s1 = "Manual" Or s1 = "MF, MF" Then txPixelsPerMM.Enabled = True Else txPixelsPerMM.Enabled = False

      lens = ""
      i = InStr(s, "Lens Type:")
      If i > 0 Then
        i += 11
        i1 = InStr(i, s, "\")
        lens = Mid(s, i, i1 - i)
      End If

      camera = ""
      i = InStr(s, "Model:")
      If i > 0 Then
        i += 7
        i1 = InStr(i, s, "\")
        camera = Mid(s, i, i1 - i)
      End If

      ' set the pixels per mm according to the camera and lens
      ' gx1: 268.4 macro, 56.5 for zoom, gh4: 274.5 macro, 57.8 zoom
      If lens.Contains("MACRO-ELMARIT 45/F2.8") Or lens.Contains("Leica DG Macro-Elmarit 45mm F2.8 Asph. Mega OIS") Then
        If s.Contains("DMC-GX1") Then
          txPixelsPerMM.Text = "268.4"
        ElseIf s.Contains("DMC-GH4") Then
          txPixelsPerMM.Text = "275.4"
        ElseIf s.Contains("E-M1MarkII") Or s.Contains("E-M1 Mark II") Then
          txPixelsPerMM.Text = "303"
        ElseIf s.Contains("DC-G95") Then
          txPixelsPerMM.Text = "306.6"
        End If
        '
      ElseIf lens.Contains("Olympus M.Zuiko Digital ED 30mm F3.5 Macro") Then
        If s.Contains("E-M1MarkII") Or s.Contains("E-M1 Mark II") Then
          txPixelsPerMM.Text = "409"
        End If

      ElseIf lens.Contains("VARIO 100-300/F4.0-5.6") Then
        If s.Contains("DMC-GX1") Then
          txPixelsPerMM.Text = "56.5"
        ElseIf s.Contains("DMC-GH4") Then
          txPixelsPerMM.Text = "57.8"
        End If

      ElseIf s.Contains("DSC-RX10M4") Then
        txPixelsPerMM.Text = "79.4"
      End If

    End Using

    If tagged(picpath) >= 0 Then mnxTagged.Checked = True Else mnxTagged.Checked = False

    dataChanged = False
    picChanged = False
    processing = False
    filenameChanged = False
    txTaxon.Select()

  End Sub

  Private Sub cmdNext_Click(ByVal Sender As Object, ByVal e As Object) Handles cmdNext.Click
    nextPic(iPic + 1)
  End Sub

  Private Sub cmdBack_Click(ByVal Sender As Object, ByVal e As Object) Handles cmdBack.Click
    nextPic(iPic - 1)
  End Sub

  Sub nextPic(ByVal n As Integer)

    Dim picNotSaved As Boolean

    If processing Then
      Exit Sub
    End If

    If filenames.Count = 0 Or n < 0 Or n >= filenames.Count Then Exit Sub

    If (Not File.Exists(getTargetPath) And dataChanged) Or picChanged Then
      ' picchanged is used later to allow a copy instead of a lossy save
      picNotSaved = True
    Else
      picNotSaved = False
    End If

    mResult = 0
    If picNotSaved And dataChanged Then
      mResult = MsgBox("Do you want to save the photo and data?", MsgBoxStyle.YesNoCancel)
      If mResult = MsgBoxResult.Yes Then
        savePic()
        saveData()
      End If
    ElseIf picNotSaved Then
      mResult = MsgBox("Do you want to save the photo?", MsgBoxStyle.YesNoCancel)
      If mResult = MsgBoxResult.Yes Then
        savePic()
      End If
    ElseIf dataChanged Then
      mResult = MsgBox("Do you want to save the data?", MsgBoxStyle.YesNoCancel)
      If mResult = MsgBoxResult.Yes Then
        saveData()
      End If
    End If

    If mResult = MsgBoxResult.Cancel Then Exit Sub

    processing = True
    Me.Cursor = Cursors.WaitCursor

    iPic = n

    pComments = New List(Of PropertyItem)
    currentpicPath = filenames(ix(iPic))
    lbPicPath.Text = Path.GetFileName(filenames(ix(iPic)))
    picpath = filenames(ix(iPic))

    initFields()

    txTaxon.Select()

    processing = False
    Me.Cursor = Cursors.Default

  End Sub

  Function getTargetPath() As String

    Dim s As String

    s = iniBugPath
    If Not s.EndsWith("\") Then s &= "\"
    s &= txFileName.Text

    Return s

  End Function

  Function savePic() As Integer
    ' -1 = cancel, 0 = not saved, 1 = saved
    ' save the image, overwrite without prompt
    ' picinfo was loaded earlier

    Dim imgSave As New ImageSave
    Dim targetPath As String
    Dim msg As String

    targetPath = getTargetPath()

    If File.Exists(targetPath) Then
      If editMode And Not picChanged Then Return 0
      mResult = MsgBox(targetPath & " exists. Overwrite?", MsgBoxStyle.YesNoCancel)
      If mResult = MsgBoxResult.No Then Return 0
      If mResult = MsgBoxResult.Cancel Then Return -1
    End If

    Me.Cursor = Cursors.WaitCursor

    If Not picChanged Then ' copy original file
      Try
        File.Copy(picpath, targetPath, True)
      Catch ex As Exception
        Me.Cursor = Cursors.Default
        MsgBox(ex.Message, MsgBoxStyle.OkCancel)
        If mResult = MsgBoxResult.Ok Then Return 0
        If mResult = MsgBoxResult.Cancel Then Return -1
      End Try

    Else ' save the jpg file

      imgSave.sourceFilename = txOriginalPath.Text
      imgSave.copyProfiles = True
      msg = imgSave.write(pView.Bitmap, targetPath, True)

      If msg <> "" Then
        Me.Cursor = Cursors.Default
        mResult = MsgBox("""" & targetPath & """ could not be saved." & Crlf & msg, MsgBoxStyle.OkCancel)
        If mResult = MsgBoxResult.Ok Then Return 0
        If mResult = MsgBoxResult.Cancel Then Return -1
      Else
        picChanged = False
      End If
    End If

    cmdDelete.Enabled = True
    Me.Cursor = Cursors.Default
    Return 1

  End Function

  Private Sub pview_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pView.MouseClick

    Dim lastCallingForm As Form

    If e.Button = MouseButtons.Left Then
      If pView.Bitmap Is Nothing Or cropping Or measuring Or processing Then Exit Sub

      lastCallingForm = callingForm ' preserve for next, previous commands
      callingForm = Me
      currentpicPath = filenames(ix(iPic))
      If qImage IsNot Nothing Then qImage.Dispose()
      qImage = pView.Bitmap.Clone
      Using frm As New frmFullscreen
        frm.ShowDialog()
      End Using
      clearBitmap(qImage)
      callingForm = lastCallingForm
    End If

  End Sub

  Sub measureDone(ByVal e As MouseEventArgs)

    Dim r As RectangleF
    Dim rBoxLeft, rBoxTop, rBoxWidth, rBoxHeight As Integer
    Dim d As Double
    Dim i As Integer

    Dim xp As Integer
    Dim yp As Integer

    measuring = False
    pView.Cursor = Cursors.Default

    xp = e.X
    yp = e.Y

    If (xp - rbX) ^ 2 + (yp - rbY) ^ 2 < 10 Then Exit Sub ' click, not drag

    If xp < rbX Then
      rBoxLeft = xp
      rBoxWidth = rbX - xp
    Else
      rBoxLeft = rbX
      rBoxWidth = xp - rbX
    End If

    If yp < rbY Then
      rBoxTop = yp
      rBoxHeight = rbY - yp
    Else
      rBoxTop = rbY
      rBoxHeight = yp - rbY
    End If

    r = pView.ControlToBitmap(New Rectangle(rBoxLeft, rBoxTop, rBoxWidth, rBoxHeight))

    If txPixelsPerMM.Enabled Then
      If (r.Width > 0 Or r.Height > 0) And IsNumeric(txPixelsPerMM.Text) Then
        d = Sqrt(r.Width ^ 2 + r.Height ^ 2) / Val(txPixelsPerMM.Text)
        txSize.Text = Format(d, "####0.#") & " mm"
      End If
    Else
      If (r.Width > 0 Or r.Height > 0) Then
        i = CInt(Sqrt(r.Width ^ 2 + r.Height ^ 2))
        MsgBox(i & " pixels")
      End If
    End If

    pView.Zoom(0)

  End Sub

  Sub cropdone(ByVal e As MouseEventArgs)
    Dim r As Rectangle
    Dim rBoxLeft, rBoxTop, rBoxWidth, rBoxHeight As Integer

    Dim xp As Integer
    Dim yp As Integer

    cropping = False
    pView.Cursor = Cursors.Default

    xp = e.X
    yp = e.Y

    If (xp - rbX) ^ 2 + (yp - rbY) ^ 2 < 10 Then Exit Sub ' click, not drag

    If xp < rbX Then
      rBoxLeft = xp
      rBoxWidth = rbX - rBoxLeft
    Else
      rBoxLeft = rbX
      rBoxWidth = xp - rbX
    End If

    If yp < rbY Then
      rBoxTop = yp
      rBoxHeight = rbY - yp
    Else
      rBoxTop = rbY
      rBoxHeight = yp - rbY
    End If

    If rBoxWidth <= 1 Or rBoxHeight <= 1 Then Exit Sub
    r = pView.ControlToBitmap(New Rectangle(rBoxLeft, rBoxTop, rBoxWidth, rBoxHeight))

    pView.Crop(r)

    picChanged = True
    pView.Zoom(0)
    txTaxon.Select()

  End Sub

  Private Sub tx_Enter(ByVal Sender As Object, ByVal e As EventArgs) Handles txBugguide.Enter, txConfidence.Enter, txCountry.Enter, _
    txCounty.Enter, txElevation.Enter, txFileName.Enter, txGPS.Enter, txImageSet.Enter, txLocation.Enter, txPixelsPerMM.Enter, _
    txRating.Enter, txRemarks.Enter, txSize.Enter, txState.Enter, txTaxon.Enter, txiNaturalist.Enter

    Dim tx As TextBox
    Dim rtx As RichTextBox

    If Loading Then Exit Sub

    If TypeOf (Sender) Is TextBox Then
      tx = Sender
      If Not tx.Multiline Then ' highlight text
        tx.SelectionStart = 0
        tx.SelectionLength = Len(tx.Text)
      End If

    ElseIf TypeOf (Sender) Is RichTextBox Then
      rtx = Sender
      If Not rtx.Multiline Then ' highlight text
        rtx.SelectionStart = 0
        rtx.SelectionLength = Len(rtx.Text)
      End If
    End If

  End Sub

  Private Sub txGPS_Leave(ByVal Sender As Object, ByVal e As EventArgs) Handles txGPS.Leave

    Dim msg As String
    Dim xLat, xLon As Double
    Dim s, s1 As String

    If Trim(txGPS.Text) = "" Then
      cmdGPSLocate.Enabled = False
      Exit Sub
    End If

    msg = latlonVerify(txGPS.Text, xLat, xLon)

    If msg <> "" Then
      MsgBox(msg)
      txGPS.Select()
    Else
      If xLat > 0 Then s1 = "N" Else s1 = "S"
      xLat = Abs(xLat)
      s = Int(xLat) & "°" & Format((xLat - Int(xLat)) * 60, "#0.####") & "'" & s1
      If xLon > 0 Then s1 = "E" Else s1 = "W"
      xLon = Abs(xLon)
      s &= " " & Int(xLon) & "°" & Format((xLon - Int(xLon)) * 60, "#0.####") & "'" & s1
      txGPS.Text = s
      cmdGPSLocate.Enabled = True
    End If

  End Sub

  Private Sub pview_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pView.MouseDown

    Dim rControl As Rectangle

    If cropping Or measuring Then
      rbX = e.X : rbY = e.Y
      rControl = New Rectangle(rbX, rbY, 0, 0)
    End If

    If cropping Then
      pView.RubberEnabled = True
      pView.RubberShape = shape.Box
      pView.RubberBoxCrop = True
      pView.RubberDashed = True
      pView.RubberColor = Color.Black
      pView.rubberBackColor = Color.White
      pView.RubberBox = rControl
      pView.Cursor = cursorSelRectangle
      pView.Invalidate() ' draw pview.rubber box

    ElseIf measuring Then
      pView.RubberEnabled = True
      pView.RubberShape = shape.Measure
      pView.RubberBoxCrop = False
      pView.RubberDashed = False
      pView.RubberLineWidth = 7
      pView.RubberColor = Color.Yellow
      pView.Cursor = Cursors.Cross
      pView.Invalidate() ' draw pview.rubber box

    ElseIf e.Button = MouseButtons.XButton2 AndAlso cmdNext.Visible Then
      nextPic(iPic + 1)
    ElseIf e.Button = MouseButtons.XButton1 AndAlso cmdBack.Visible Then
      nextPic(iPic - 1)
    End If

  End Sub

  Private Sub pview_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pView.MouseMove

    If e.Button = MouseButtons.Left AndAlso pView.Bitmap IsNot Nothing AndAlso (Not processing) Then
      If cropping Then
        pView.RubberBox = New Rectangle(Min(rbX, e.X), Min(rbY, e.Y), Abs(rbX - e.X), Abs(rbY - e.Y))
        pView.Invalidate()
      ElseIf measuring Then
        pView.RubberPoints = {New Point(rbX, rbY), New Point(e.X, e.Y)}.ToList
        pView.Invalidate()
      End If
    ElseIf pView.Focused Then
      txTaxon.Select() ' this allows the focus to go to txTaxon after form load when the cursor is on pview.
    End If
  End Sub

  Private Sub pview_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pView.MouseUp

    processing = True
    pView.RubberClear()
    If cropping Then
      cropdone(e)
    ElseIf measuring Then
      measureDone(e)
    End If
    processing = False

  End Sub

  Private Sub rText_KeyDown(ByVal Sender As Object, ByVal e As KeyEventArgs) Handles rText0.KeyDown, _
    txCommon.KeyDown, txConfidence.KeyDown, txDate.KeyDown, txDateAdded.KeyDown, txFileName.KeyDown, txGPS.KeyDown, _
    txOriginalPath.KeyDown, txRating.KeyDown, txTaxon.KeyDown, txPixelsPerMM.KeyDown, chkLink.KeyDown, txRemarks.KeyDown, _
    txLocation.KeyDown, txCounty.KeyDown, txState.KeyDown, txCountry.KeyDown, txElevation.KeyDown, _
    txBugguide.KeyDown, txSize.KeyDown, cmdMeasure.KeyDown, cmdTaxon.KeyDown, cmdGPSLocate.KeyDown, _
    cmdNext.KeyDown, cmdBack.KeyDown, cmdSaveAll.KeyDown, cmdSaveData.KeyDown, cmdDelete.KeyDown, cmdCrop.KeyDown, Me.KeyDown, _
    txImageSet.KeyDown, cbImageSet.KeyDown, txiNaturalist.KeyDown

    globalkey(e)
  End Sub

  Private Sub globalkey(ByRef e As KeyEventArgs)

    Dim inMatch As New bugMain.taxrec
    Dim k As Integer

    Select Case e.KeyCode
      Case 113 ' F2 = clear
        initFields()
        e.Handled = True

      Case 114 ' F3 = restore the previous values
        txTaxon.Text = lastbugTaxon
        txTaxid = lastbugTaxid
        inMatch.taxon = lastbugTaxon
        taxonid = lastbugTaxonID
        inMatch.taxid = lastbugTaxonID
        txCommon.Text = lastbugCommon

        txLocation.Text = lastbugLocation
        txCounty.Text = lastbugCounty
        txState.Text = lastbugState
        txCountry.Text = lastbugCountry
        ' txGPS.Text = lastbugGPS
        ' txSize.Text = lastbugSize
        ' txRating.Text = lastbugRating
        txConfidence.Text = lastbugConfidence
        txRemarks.Text = lastbugRemarks
        txBugguide.Text = lastbugBugguide
        txiNaturalist.Text = lastbuginaturalist
        e.Handled = True

      Case Keys.Home
        If cmdNext.Visible AndAlso e.Alt Then
          nextPic(0)
          e.Handled = True
        End If

      Case Keys.End
        If cmdNext.Visible AndAlso e.Alt Then
          nextPic(filenames.Count - 1)
          e.Handled = True
        End If

      Case Keys.PageDown ' arrow keys cause problems with rtext
        If cmdNext.Visible Then
          nextPic(iPic + 1)
          e.Handled = True
        End If

      Case Keys.PageUp
        If cmdBack.Visible Then
          nextPic(iPic - 1)
          e.Handled = True
        End If

      Case Keys.Left
        If tvTaxon.Visible And e.Alt Then
          tvTaxon.Visible = False
          cmdCloseTree.Visible = False
          e.Handled = True
        End If

      Case Keys.Right
        If Not tvTaxon.Visible And e.Alt Then
          tvTaxon.Visible = True
          cmdCloseTree.Visible = True
          e.Handled = True
        End If

      Case Keys.Z
        If e.Control And picChanged Then
          Me.Cursor = Cursors.WaitCursor
          loadInitialPic()
          picChanged = False
          Me.Cursor = Cursors.Default
        End If

      Case Keys.T
        If e.Alt Then ' tag or untag
          Me.Cursor = Cursors.WaitCursor
          k = tagged(picpath)
          If k >= 0 Then
            ' it's tagged -- remove it from the tag list
            tagPath.RemoveAt(k)
            mnxTagged.Checked = False
          Else
            ' not tagged -- add it to the tag list
            tagPath.Add(picpath)
            mnxTagged.Checked = True
          End If
          Me.Cursor = Cursors.Default
        End If

      Case Else
        e.Handled = False
    End Select

  End Sub

  Function tagged(ByVal fpath As String) As Integer
    ' returns -1 if file is not tagged, the tag index otherwise
    For i As Integer = 0 To tagPath.Count - 1
      If eqstr(tagPath(i), fpath) Then Return i
    Next i
    Return -1
  End Function


  Private Sub cmdTaxon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTaxon.Click

    Dim match As bugMain.taxrec
    Dim matches As List(Of taxrec)
    Dim nd As TreeNode = Nothing

    Me.Cursor = Cursors.WaitCursor

    tvTaxon.Nodes.Clear()
    tvTaxon.Visible = True
    txTaxid = ""
    cmdCloseTree.Visible = True

    matches = queryTax("select * from taxatable where taxon = @parm1 order by taxon", "life")

    If matches.Count > 0 Then
      nd = tvTaxon.Nodes.Add(taxaLabel(matches(0), False, False))
      nd.Tag = matches(0).taxid
    End If

    populate(nd, False)  ' load root node (animalia) commented out 6/22/19
    nd.ExpandAll()
    ' Now get down through root
    'For Each ndc As TreeNode In nd.Nodes
    'If ndc.Text.StartsWith("Animalia") Then
    ' populate(ndc, False)  ' load everything
    ' ndc.Expand()
    ' Exit For
    ' End If
    ' Next ndc

    If txTaxon.Text <> "" Then
      cmdCloseTree.Visible = True
      match = popuTaxon(txTaxon.Text, tvTaxon, False)
      txCommon.Text = getDescr(match, False)
    End If

    Me.Cursor = Cursors.Default

  End Sub

  Private Sub tvTaxon_AfterCollapse(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles tvTaxon.AfterCollapse

    ' clear the children

    Dim nd As TreeNode

    For Each nd In e.Node.Nodes
      nd.Nodes.Clear()
    Next nd

  End Sub


  Private Sub tvTaxon_BeforeSelect(ByVal sender As Object, ByVal e As TreeViewCancelEventArgs) Handles tvTaxon.BeforeSelect

    ' load the children

    If e.Node.Nodes.Count = 0 Then
      populate(e.Node, False)
    End If

  End Sub

  Private Sub tvTaxon_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles tvTaxon.KeyDown

    Select Case e.KeyCode

      Case Keys.PageUp
        If e.Alt Then
          globalkey(e)
          e.Handled = True
        End If

      Case Keys.PageDown
        If e.Alt Then
          globalkey(e)
          e.Handled = True
        End If

      Case Keys.Escape
        tvTaxon.Visible = False
        cmdCloseTree.Visible = False
        e.Handled = True

      Case Keys.Enter
        tvTaxon_Doubleclick(sender, e)
        e.Handled = True

      Case Keys.Space
        gotoNextNode(tvTaxon)
        e.Handled = True

      Case Else

    End Select

  End Sub

  Private Sub tvTaxon_Doubleclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvTaxon.NodeMouseDoubleClick

    Dim match As taxrec

    If tvTaxon.SelectedNode Is Nothing Then Exit Sub

    taxonid = tvTaxon.SelectedNode.Tag

    match = getTaxrecByID(taxonid, True)

    txTaxon.Text = match.taxon
    txTaxid = match.taxid
    txCommon.Text = getDescr(match, False)
    txLocation.Select()

    tvTaxon.Visible = False
    cmdCloseTree.Visible = False

  End Sub

  Private Sub cmdCrop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCrop.Click
    cropping = True
    pView.Cursor = cursorSelRectangle
  End Sub

  Private Sub cmdMeasure_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMeasure.Click
    measuring = True
    pView.Cursor = Cursors.Cross
  End Sub

  Sub loadInitialPic()

    If callingForm Is frmExplore Then
      If editMode Then  ' show the pic from the target folder
        pComments = New List(Of PropertyItem)
        showPicture(getTargetPath(), pView, False, pComments)
        pView.BackColor = System.Drawing.Color.CadetBlue
      Else
        pView.BackColor = Me.BackColor
        If frmExplore.rview.Bitmap Is Nothing Then ' load the file into pview.bitmap
          pComments = New List(Of PropertyItem)
          showPicture(picpath, pView, False, pComments)
        Else
          pView.setBitmap(frmExplore.rview.Bitmap)
          pComments = frmExplore.pComments
        End If
      End If

      cmdNext.Visible = True
      cmdBack.Visible = True

    ElseIf callingForm Is frmMain Then
      pView.BackColor = Me.BackColor
      pView.setBitmap(frmMain.mView.Bitmap)
      pComments = frmMain.mView.pComments
      cmdNext.Visible = False
      cmdBack.Visible = False
    End If

    pView.Zoom(0)

  End Sub

  Private Sub cmdCloseTree_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCloseTree.Click
    tvTaxon.Visible = False
    cmdCloseTree.Visible = False
  End Sub

  Private Sub frmBugPhotos_Load(ByVal Sender As Object, ByVal e As EventArgs) Handles MyBase.Load

    processing = True
    cmdReadweb.Enabled = True

    Me.WindowState = FormWindowState.Maximized
    'helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    'helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    ' If lastbugLocationAutocomplete IsNot Nothing Then txLocation.AutoCompleteCustomSource = lastbugLocationAutocomplete
    txPixelsPerMM.Text = Format(iniBugPixelsPerMM, "#0.#")

    tvTaxon.Visible = False
    cmdCloseTree.Visible = False
    tvTaxon.Left = pView.Left
    tvTaxon.Width = pView.Width
    tvTaxon.Top = pView.Top
    tvTaxon.Height = pView.Height
    tvTaxon.ShowNodeToolTips = True
    cmdCloseTree.Top = tvTaxon.Top + 3
    cmdCloseTree.Left = tvTaxon.Left + tvTaxon.Width - cmdCloseTree.Width - 24
    cmdCloseTree.Anchor = AnchorStyles.Right Or AnchorStyles.Top

    filenames = New List(Of String)
    iPic = -1

    picpath = currentpicPath

    Loading = False

  End Sub

  Private Sub frmBugPhotos_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

    Dim i As Integer

    If Not checkDB(iniDBConnStr) Then
      MsgBox("The database could not be found.", MsgBoxStyle.OkOnly)
      Me.DialogResult = DialogResult.Cancel
      Me.Close()
      Exit Sub
    End If

    Me.Cursor = Cursors.WaitCursor

    initFields() ' sets processing to false

    If useQuery Then
      filenames = New List(Of String)
      filenames.AddRange(queryNames)
    Else
      ' load filenames in current folder, find currently displayed one (iPic)
      i = getFilePaths(iniExplorePath, filenames, False)
    End If

    For i = 0 To filenames.Count - 1 : ix.Add(i) : Next
    If Not useQuery Then MergeSort(filenames, ix, 0, filenames.Count - 1)

    For i = 0 To filenames.Count - 1
      If eqstr(currentpicPath, filenames(ix(i))) Then
        iPic = i
        Exit For
      End If
    Next i

    If iPic < 0 Then
      filenames = New List(Of String)
      filenames.Add(currentpicPath) ' should never happen
      iPic = 0
    End If

    i = tagged(picpath)
    If i >= 0 Then mnxTagged.Checked = True Else mnxTagged.Checked = False

    processing = False
    Me.Cursor = Cursors.Default

    txTaxon.Select()

  End Sub

  Private Sub frmBugPhotos_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
    If Loading Then Exit Sub
    pView.Zoom(0)
  End Sub

  'Private Sub txAutocomplete_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txLocation.Leave, txTaxon.Leave
  '
  'Dim tx As TextBox
  '
  'tx = sender
  'If Len(Trim(tx.Text)) > 1 Then
  '  If Not tx.AutoCompleteCustomSource.Contains(Trim(tx.Text)) Then tx.AutoCompleteCustomSource.Add(tx.Text)
  '  End If
  '
  'End Sub

  Private Sub txFileName_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txFileName.Leave

    Dim s As String

    If filenameChanged Then
      If File.Exists(getTargetPath()) Then
        cmdDelete.Enabled = True
        editMode = True
        pView.BackColor = System.Drawing.Color.CadetBlue
      Else
        cmdDelete.Enabled = False
        editMode = False
        pView.BackColor = Me.BackColor
        showPicture(picpath, pView, False, pComments)
      End If

      filenameChanged = False

      s = txFileName.Text
      initFields(s)
    End If

  End Sub

  Sub txElevation_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txElevation.Leave
    ' format txelevation and assign it to ielevation, which is used for the database

    formatElevation()

  End Sub

  Sub formatElevation()

    Dim x As Double

    If Not IsNumeric(txElevation.Text) Then
      txElevation.Text = ""
      iElevation = -32768
    Else
      x = Val(txElevation.Text)
      iElevation = Round(x)
      txElevation.Text = Format(Round(iElevation / 100) * 100, "###,##0") & " ft " & "(" & Format(Round(iElevation * 0.3048 / 100) * 100, "###,##0") & " m)"
    End If

  End Sub


  Private Sub tx_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txTaxon.TextChanged, _
    txCommon.TextChanged, txLocation.TextChanged, txSize.TextChanged, txGPS.TextChanged, txRating.TextChanged, txConfidence.TextChanged, _
    txFileName.TextChanged, txImageSet.TextChanged, txCounty.TextChanged, txState.TextChanged, txCountry.TextChanged, _
    txElevation.TextChanged

    If processing Then Exit Sub ' processing = true for the auto updates of taxon, etc.
    dataChanged = True
    If sender Is txFileName Then filenameChanged = True
    If sender Is txImageSet Then imagesetChanged = True
    If sender Is txTaxon Then txTaxid = ""

  End Sub

  Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click

    If picChanged Or dataChanged Then
      mResult = MsgBox("Do you want to save the photo and data?", MsgBoxStyle.YesNoCancel)
      If mResult = MsgBoxResult.Yes Then
        savePic()
        saveData()
      ElseIf mResult = MsgBoxResult.Cancel Then
        abort = True
      End If
    End If

    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click

    Dim fName As String

    fName = getTargetPath()
    If File.Exists(fName) Then
      mResult = MsgBox("Delete " & fName & "?", MsgBoxStyle.YesNoCancel)
      If mResult = MsgBoxResult.Yes Then
        Try
          File.Delete(fName)
        Catch ex As Exception
          MsgBox(ex.Message)
        End Try
      Else
        Exit Sub
      End If
    Else
      MsgBox("File doesn't exist: " & fName, MsgBoxStyle.OkCancel)
    End If

    ' delete from database
    bugDelete(Trim(txFileName.Text))

    If eqstr(picpath, iniBugPath) Then
      nextPic(iPic + 1) ' deleted current file
    Else
      initFields()
    End If

    Me.Cursor = Cursors.Default

  End Sub

  Function countLinks(ByVal prevName As String, ByVal newName As String, ByRef isLinked As Boolean) As Integer

    ' returns the number of links in the imageset linked to by newName
    ' isLinked is true if newName is linked to prevName

    Dim setid As Integer = 0
    Dim previmageID As Integer
    Dim newimageID As Integer

    countLinks = 0
    isLinked = False

    previmageID = getScalar("select imageid from images where filename = @parm1 limit 1", prevName)
    newimageID = getScalar("select imageid from images where filename = @parm1 limit 1", newName)

    If newimageID > 0 Then
      ' find existing imageset
      setid = getScalar("select setid from imagesets where imageid = @parm1 limit 1", newimageID)

      If setid > 0 Then ' how many images in set?
        countLinks = getScalar("select count(*) from imagesets where setid = @parm1", setid)
      End If
    End If

    If previmageID > 0 Then ' is the previous image in the imageset?
      isLinked = getScalar("select count(*) from imagesets where setid = @parm1 and imageid = @parm2", setid, previmageID) > 0
    End If

  End Function

  Private Sub chkLink_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLink.CheckedChanged

    If processing Then Exit Sub ' processing = true for the auto updates of taxon, etc.

    dataChanged = True

  End Sub

  Sub setChkLink()

    Dim i As Integer
    Dim islinked As Boolean = False

    chkLink.Checked = False
    If bugPrevFilename = "" Or bugPrevFilename = txFileName.Text Then
      chkLink.Enabled = False
      chkLink.Text = "Lin&k to previous image"
    Else
      i = countLinks(bugPrevFilename, txFileName.Text, islinked)
      If islinked Then ' it is already linked to prevFilename
        chkLink.Enabled = False
        If i <= 2 Then
          chkLink.Text = "(Already linked to " & bugPrevFilename & ")"
        ElseIf i = 3 Then
          chkLink.Text = "(Already linked to " & bugPrevFilename & " and 1 other)"
        ElseIf i > 3 Then
          chkLink.Text = "(Already linked to " & bugPrevFilename & " and " & i - 2 & " others)"
        Else ' should not happen
          chkLink.Text = "Lin&k to previous image (" & bugPrevFilename & ")"
        End If
      ElseIf i >= 2 Then ' linked somewhere besides prevFilename
        chkLink.Text = "Lin&k to previous (" & bugPrevFilename & "), unlink from " & i - 1 & " other(s)."
        chkLink.Enabled = True
      Else ' not linked
        chkLink.Text = "Lin&k to previous image (" & bugPrevFilename & ")"
        chkLink.Enabled = True
      End If
    End If

  End Sub

  Function setimageCount(ByVal taxid As String) As Integer

    ' sets the childimagecounter of taxid, descends recursively 

    Dim imageCounter As Integer
    Dim childImageCounter As Integer
    Dim i As Integer
    Dim matches As List(Of taxrec)

    imageCounter = getScalar("select count(*) from images where images.taxonid = @parm1", taxid)

    matches = queryTax("select * from taxatable where parentid = @parm1", taxid)

    childImageCounter = imageCounter
    For Each m As taxrec In matches ' children
      i = setimageCount(m.taxid)
      childImageCounter += i
    Next m

    If childImageCounter <> 0 Then
      i = nonQuery("update taxatable set imagecounter = @parm1, childimagecounter = @parm2 where taxid = @parm3", _
              imageCounter, childImageCounter, taxid)
      If i <> 1 Then Stop
    End If

    Return childImageCounter

  End Function

  Function rankCounts() As String

    Dim i, iCount As Integer
    Dim s As String
    Dim sb As New StringBuilder

    Dim matches As List(Of taxrec)
    Dim amatches As New List(Of taxrec)
    Dim anc As List(Of taxrec)

    Dim ranks() As String = {
        "stateofmatter",
        "kingdom",
        "phylum",
        "subphylum",
        "superclass",
        "class",
        "subclass",
        "superorder",
        "order",
        "suborder",
        "infraorder",
        "superfamily",
        "epifamily",
        "family",
        "subfamily",
        "supertribe",
        "tribe",
        "subtribe",
        "genus",
        "species",
        "subspecies",
        "variety"}

    Dim rankCount(UBound(ranks)) As Integer
    Dim rankCountTotal(UBound(ranks)) As Integer
    Dim arthropodCount(UBound(ranks)) As Integer
    Dim arthropodCountTotal(UBound(ranks)) As Integer


    matches = queryTax("select * from taxatable where childimagecounter > 0 order by taxon", "")

    For Each m As taxrec In matches
      anc = getancestors(m, "phylum")
      i = Array.IndexOf(ranks, LCase(m.rank))
      If i < 0 Then ' get next ancestor with legit rank
        For i1 As Integer = 1 To anc.Count - 1
          i = Array.IndexOf(ranks, LCase(anc(i1).rank))
          If i >= 0 Then Exit For
        Next i1
      End If

      If i >= 0 Then
        rankCountTotal(i) += 1
        If m.imageCounter > 0 Then rankCount(i) += 1

        If eqstr(anc(anc.Count - 1).taxon, "life") Then
          arthropodCountTotal(i) += 1
          If m.imageCounter > 0 Then arthropodCount(i) += 1
        End If
      End If
    Next m

    iCount = 0
    For i = 0 To UBound(ranks)
      If rankCountTotal(i) - arthropodCountTotal(i) > 0 Then
        If rankCount(i) - arthropodCount(i) > 0 Then
          s = rankCountTotal(i) - arthropodCountTotal(i) & Chr(9) & "(" & rankCount(i) - arthropodCount(i) & ")"
        Else
          s = rankCountTotal(i) - arthropodCountTotal(i) & Chr(9)
        End If
        sb.AppendLine(s & Chr(9) & ranks(i))
        iCount += (rankCount(i) - arthropodCount(i))
      End If
    Next i

    sb.AppendLine(iCount & Chr(9) & "total")

    sb.AppendLine()
    iCount = 0
    For i = 0 To UBound(ranks)
      If arthropodCountTotal(i) > 0 Then
        If arthropodCount(i) > 0 Then
          s = arthropodCountTotal(i) & Chr(9) & "(" & arthropodCount(i) & ")"
        Else
          s = arthropodCountTotal(i) & Chr(9)
        End If
        sb.AppendLine(s & Chr(9) & ranks(i))
        iCount += arthropodCount(i)
      End If
    Next i

    sb.AppendLine(iCount & Chr(9) & "total arthropods")


    Return sb.ToString

  End Function


  Private Sub cmdImageUpdate_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImageUpdate.Click

    Dim id As String
    Dim cmd As MySqlCommand
    Dim dr As DataRow
    Dim i, k As Integer
    Dim setid, imageid, lastimage As Integer
    Dim s As String
    Dim ss() As String
    Dim sl As New List(Of String)
    Dim delrecs As New List(Of Integer)
    Dim fNames As New List(Of String)
    Dim fName As String

    Dim gps As String = ""
    Dim country As String
    Dim state As String
    Dim county As String
    Dim location As String
    Dim lastDate As DateTime
    Dim lastTaxon As String
    Dim lastFilename As String
    Dim mres As MsgBoxResult
    Dim xLat, xLon As Double
    Dim matches As List(Of taxrec)

    Me.Cursor = Cursors.WaitCursor

    s = rankCounts()
    Me.Cursor = Cursors.Default
    mres = MsgBox(s, MsgBoxStyle.OkCancel)
    If mres = MsgBoxResult.Cancel Then Exit Sub

    ' should only be necessary after database changes - slow
    If 1 = 1 Then
      Me.Cursor = Cursors.WaitCursor
      i = nonQuery("update taxatable set imagecounter = 0, childimagecounter = 0 where childimagecounter <> 0")
      matches = queryTax("select * from taxatable where taxon = 'life'", "")
      id = matches(0).taxid
      i = setimageCount(id)
      k = getScalar("select count(*) from taxatable where imagecounter > 0")

      MsgBox(Format(i, "#,#") & " photos of " & Format(k, "#,#") & " bugs.")

      Me.Cursor = Cursors.Default
      If 1 = 1 Then Exit Sub
    End If

    mResult = MsgBox("Count: " & i & ".  Clean up database?", MsgBoxStyle.YesNoCancel)
    If mResult <> MsgBoxResult.Yes Then Exit Sub

    ' remove empty and duplicate imagesets
    Using ds As DataSet = getDS("select distinct a.* from imagesets as a, imagesets as b where a.imageid = b.imageid and a.setid = b.setid and a.id <> b.id order by a.imageid, a.setid")

      lastimage = -1
      If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
        For Each dr In ds.Tables(0).Rows
          id = dr("id")
          setid = dr("setid")
          imageid = dr("imageid")
          i = getScalar("select count(*) from imagesets where setid = @parm1", setid)
          If i <= 2 Then ' delete both (or a single) records
            i = nonQuery("delete from imagesets where setid = @parm1", setid)
          Else ' delete all but one record with imageid
            If imageid = lastimage Then ' delete the record except for the first one
              i = nonQuery("delete from imagesets where id = @parm1", id)
            Else
              lastimage = imageid
            End If
          End If
        Next dr
      End If
    End Using

    ' delete single member image sets
    Using ds As DataSet = getDS("select * from imagesets as a where (select count(*) from imagesets as b where a.setid = b.setid) = 1")

      If ds IsNot Nothing AndAlso ds.Tables.Count = 1 Then
        For Each dr In ds.Tables(0).Rows
          i = nonQuery("delete from imagesets where id = @parm1", dr("id"))
        Next dr
      End If
    End Using

    delrecs = New List(Of Integer)
    ' get records without images
    Using ds As DataSet = getDS("select * from images")
      If ds IsNot Nothing Then
        For Each dr In ds.Tables(0).Rows
          s = iniBugPath
          If Not s.EndsWith("\") Then s &= "\"
          s &= dr("filename")

          If Not File.Exists(s) Then
            sl.Add(dr("originalpath"))
            delrecs.Add(dr("imageid"))
          End If
        Next dr
      End If
    End Using

    sl.Add(Crlf & Crlf & "unattached files" & Crlf)

    fNames = New List(Of String)
    For Each s In Directory.GetFiles(iniBugPath)
      i = getScalar("select count(*) from images where filename=@parm1", Path.GetFileName(s))
      If i = 0 Then
        fNames.Add(s)
        sl.Add(s)
      End If
    Next s

    File.WriteAllLines("c:\tmp\tmp.txt", sl.ToArray)

    ' MsgBox(fNames.Count & " unattached files.")

    'For Each s In fNames
    '  File.Delete(s)
    '  Next s

    If 1 = 1 Then Exit Sub


    ' find images that should be in imagesets
    ' uncomment section at bottom of frmexplore mnuToolsComment_Click
    Using ds As DataSet = getDS("select * from images where (select count(*) from imagesets where images.imageid = imageid) = 0 order by photodate;")
      lastDate = "1/1/1974"
      lastTaxon = 0
      lastFilename = ""

      queryNames = New List(Of String)
      useQuery = True

      For Each dr In ds.Tables(0).Rows
        If IsDate(dr("photodate")) AndAlso DateDiff(DateInterval.Minute, lastDate, dr("photodate")) < 2 AndAlso lastTaxon = dr("taxonid") Then ' add to potential imagesets
          If lastFilename <> "" Then queryNames.Add(iniBugPath & "\" & lastFilename)
          lastFilename = ""
          queryNames.Add(iniBugPath & "\" & dr("filename"))
        Else
          lastFilename = dr("filename")
        End If
        lastTaxon = dr("taxonid")
        If IsDate(dr("photodate")) Then lastDate = dr("photodate")
      Next dr

      useQuery = True

      Me.Cursor = Cursors.Default
      Me.DialogResult = DialogResult.OK
      Me.Close()

      If 1 = 1 Then Exit Sub
    End Using



    ' update gps data (shouldn't need any more)
    Using ds As DataSet = getDS("select * from images where gps <> ''")

      If ds IsNot Nothing Then
        For Each dr In ds.Tables(0).Rows
          fName = iniBugPath
          If Not fName.EndsWith("\") Then fName &= "\"
          fName &= dr("filename")
          pComments = readPropertyItems(fName)
          getGPSLocation(pComments, gps, s, xLat, xLon, k)

          If k <> 0 Then
            i = nonQuery("update images set elevation = @parm2 where imageid = @parm1", dr("imageid"), k)
          End If
        Next dr
      End If
    End Using

    ' get county, state, country from location
    Using ds As DataSet = getDS("select * from images")

      If ds IsNot Nothing Then
        For Each dr In ds.Tables(0).Rows
          ' If Not IsDBNull(dr("location")) Then location = dr("location") Else location = ""
          location = ""
          id = dr("imageid")
          If Not IsDBNull(dr("state")) Then state = dr("state") Else state = ""
          If Not IsDBNull(dr("county")) Then county = dr("county") Else county = ""
          If Not IsDBNull(dr("country")) Then country = dr("country") Else country = ""

          If Not IsDBNull(dr("location")) AndAlso dr("location") <> "" And state = "" And county = "" And country = "" Then
            s = dr("location")
            s = s.Trim
            s = s.Replace("  ", " ")
            'If vb.Right(s, 5) = ", USA" Then s = Mid(s, 1, Len(s) - 5)
            If s.EndsWith(", USA") Then s = s.Substring(0, s.Length - 6)

            ss = s.Split(",")
            For i = 0 To UBound(ss) : ss(i) = ss(i).Trim : Next i
            i = UBound(ss)
            Select Case ss(i)
              Case "Cuba, Australia, Nepal, Canada, UK, U.K., New Zealand, France"
                country = ss(i)
              Case "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", _
                      "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", _
                      "WV", "WI", "WY", "AS", "DC", "FM", "GU", "MH", "MP", "PW", "PR", "VI"
                state = ss(i)
              Case "Oklahoma"
                state = "OK"
              Case "Colorado"
                state = "CO"
              Case "Florida"
                state = "FL"
              Case "Virginia"
                state = "VA"
              Case "Maryland"
                state = "MD"
              Case Else
                location = ss(i)
            End Select

            If UBound(ss) >= 1 And location = "" Then
              i = UBound(ss) - 1
              'If LCase(vb.Right(ss(i), 6)) = "county" Then county = vb.Left(ss(i), Len(ss(i)) - 7)
              If LCase(ss(i)).EndsWith("county") Then county = ss(i).Substring(0, ss(i).Length - 7)

              If county = "" Then
                If location = "" Then
                  location = ss(i)
                Else
                  location = ss(i) & ", " & location
                End If
              End If
            End If

            For i = UBound(ss) - 2 To 0 Step -1
              If location = "" Then
                location = ss(i)
              Else
                location = ss(i) & ", " & location
              End If
            Next i

            Using conn As New MySqlConnection(iniDBConnStr)
              conn.Open()
              cmd = New MySqlCommand("update images set location = @location, county = @county, state = @state, country = @country  where imageid = @id", conn)
              cmd.Parameters.AddWithValue("@id", id)
              cmd.Parameters.AddWithValue("@location", location)
              cmd.Parameters.AddWithValue("@county", county)
              cmd.Parameters.AddWithValue("@state", state)
              cmd.Parameters.AddWithValue("@country", country)
              cmd.ExecuteNonQuery()
            End Using
          End If

        Next dr
      End If
    End Using

  End Sub

  Private Sub cmdReadweb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReadweb.Click
    ' read data from bugguide, load it into bugguide table.
    ' save the "subscribed" page to c:\tmp\buglinks.txt first.
    ' update the cookie first, from firefox, and enable the button

    ' to copy bugguideid to images:
    '  update images, bugguide set bugguide = bugguideid where 
    '    images.filename = bugguide.filename and abs(bugguideid-bugguide) > 6 and
    '    abs(datediff(images.photodate, bugguide.photodate)) < 2;

    ' to find different taxons in an imageset:
    '   select distinct a.* from 
    '     (select setid, imageid, images.taxonid, filename from images join imagesets on images.id = imagesets.imageid) as a join	
    '     (select setid, imageid, images.taxonid, filename from images join imagesets on images.id = imagesets.imageid) as b
    '     on a.setid = b.setid and a.imageid <> b.imageid and a.taxonid <> b.taxonid order by setid, imageid;



    Dim cmd As MySqlCommand

    Dim client As New cWebClient

    Dim s, s1, s2, sp, link As String
    Dim fName As String = ""
    Dim location As String
    Dim size As String
    Dim photoDate, lastUpdate As DateTime
    Dim taxKey As String
    Dim taxID As String
    Dim bugguideID As Integer

    Dim comments As String

    Dim links() As String
    Dim i, i1, i2, i3, k As Integer

    Dim matches As List(Of taxrec)

    Me.Cursor = Cursors.WaitCursor

    links = File.ReadAllLines("c:\tmp\buglinks.txt")

    For Each link In links
      s1 = "<tr><td>Page: <a href="""
      i = InStr(link, s1)
      If i > 0 Then i1 = InStr(i, link, """>")
      If i > 0 And i1 > 0 Then
        link = Mid(link, i + Len(s1), i1 - i - Len(s1))
        i = InStr(link, "/view/")
        bugguideID = Mid(link, i + 6)
        location = ""
        size = ""

        System.Threading.Thread.Sleep(200)
        s = client.DownloadString(link)

        s1 = "<td align=""right"" class=""bgimage-id"">P1"
        i1 = InStr(s, s1)
        If i1 <= 0 Then
          s1 = "<td align=""right"" class=""bgimage-id"">IM"
          i1 = InStr(s, s1)
          If i1 = 0 Then fName = ""
        End If

        If i1 > 0 Then ' skip it all if there's no filename (no images, or invalid filename)

          i1 = i1 + Len(s1) - 2
          i2 = InStr(i1, s, "<")
          fName = Mid(s, i1, i2 - i1) & ".jpg"

          s1 = "<div class=""bgimage-where-when"">"
          i1 = InStr(s, s1)
          If i1 > 0 Then
            i1 += Len(s1)
            i2 = InStr(i1, s, "</div>")
            s1 = Mid(s, i1, i2 - i1)
            i = InStr(s1, "<br />")
            location = Mid(s1, 1, i - 1)
            s1 = Mid(s1, i + 6)
            i = InStr(s1, "<br />")
            If i > 0 AndAlso IsDate(Mid(s1, 1, i - 1)) Then
              photoDate = CDate(Mid(s1, 1, i - 1))
            Else
              photoDate = "1/1/1900"
            End If
            i2 = InStr(i1, s, "<br />Size:")
            If i2 > 0 Then
              i3 = InStr(i2 + 1, s, "<br />")
              If i3 > i2 Then size = Mid(s, i2 + 11, i3 - i2 - 11).Trim
            End If
          Else
            photoDate = "1/1/1900"
          End If

          lastUpdate = "1/1/1900"
          s1 = "<div class=""node-byline"">"
          k = InStr(s, s1)
          If k > 0 Then
            s1 = "</a> on "
            i1 = InStr(k + Len(s1), s, s1)
            If i1 > k And i1 - k < 300 Then
              i2 = InStr(i1 + 1, s, "<")
              s2 = Mid(s, i1 + Len(s1), i2 - i1 - Len(s1))
              s2 = s2.Replace(" - ", " ").Trim
              If IsDate(s2) Then lastUpdate = s2
            End If

            s1 = "<br />Last updated "
            i1 = InStr(s, s1)
            If i1 > k And i1 - k < 300 Then
              i2 = InStr(i1 + 1, s, "<")
              s2 = Mid(s, i1 + Len(s1), i2 - i1 - Len(s1))
              s2 = s2.Replace(" - ", " ").Trim
              If IsDate(s2) Then lastUpdate = s2
            End If
          End If

          s1 = "<div class=""bgpage-roots"">"
          i1 = InStr(s, s1) + Len(s1)
          i2 = InStr(i1, s, "</div>")
          s1 = Mid(s, i1, i2 - i1)
          s1 = s1.Replace("&raquo;", "»")
          s1 = s1.Replace("&nbsp;", " ")

          i = InStr(s1, "<")
          Do While i > 0
            i1 = InStr(i, s1, ">")
            s1 = Mid(s1, 1, i - 1) & Mid(s1, i1 + 1)
            i = InStr(s1, "<")
          Loop
          'ancestry = s1

          i = s1.LastIndexOf("»")
          taxKey = Mid(s1, i + 3)

          comments = ""
          sp = "<div class=""comment-subject"">"
          i = InStr(s, sp)
          Do While i > 0
            i1 = InStr(i, s, "</div>")
            s1 = Mid(s, i + Len(sp), i1 - i - Len(sp))
            If s1 <> "Moved" Then ' save the comment

              comments &= s1 & Crlf
              s1 = "<div class=""comment-body"">"
              k = InStr(i, s, s1)
              If k > 0 Then
                i1 = InStr(k, s, "</div>")
                comments &= Mid(s, k + Len(s1), i1 - k - Len(s1)) & Crlf
              End If

              comments &= Crlf & Crlf
            End If

            i = InStr(i + 1, s, sp)
          Loop

          comments = comments.Replace("<br />", Crlf)
          i = InStr(comments, "<")
          Do While i > 0
            i1 = InStr(i, comments, ">")
            comments = Mid(comments, 1, i - 1) & Mid(comments, i1 + 1)
            i = InStr(comments, "<")
          Loop
          comments = comments.Trim

          i = InStr(taxKey, "(")
          If i > 0 Then ' taxkey is inside the parentheses
            i1 = InStr(taxKey, ")")
            taxKey = Mid(taxKey, i + 1, i1 - i - 1)
          End If

          matches = TaxonSearch(taxKey, False)
          If matches.Count > 0 Then taxID = matches(0).taxid Else taxID = ""

          Using conn As New MySqlConnection(iniDBConnStr)
            conn.Open()

            i = getScalar("select count(*) from bugguide where filename = @parm1", fName)

            If i <= 0 Then ' record not found -- add it.
              cmd = New MySqlCommand( _
                "insert into bugguide (filename, photodate, bugguideid, taxonkey, taxonid, location, size, comments, lastupdate)" & _
                "values (@filename, @photodate, @bugguideid, @taxonkey, @taxonid, @location, @size, @comments, @lastupdate)", conn)
            Else ' update it
              cmd = New MySqlCommand("update bugguide set " & _
              " photodate = @photodate, " & _
              " bugguideid = @bugguideid, " & _
              " taxonkey = @taxonkey, " & _
              " taxonid = @taxonid, " & _
              " location = @location, " & _
              " size = @size, " & _
              " comments = @comments, " & _
              " lastupdate = @lastupdate" & _
              " where filename = @filename", conn)
            End If

            cmd.Parameters.AddWithValue("@filename", fName)
            cmd.Parameters.AddWithValue("@photodate", photoDate)
            cmd.Parameters.AddWithValue("@bugguideid", bugguideID)
            cmd.Parameters.AddWithValue("@taxonkey", taxKey)
            cmd.Parameters.AddWithValue("@taxonid", taxID)
            cmd.Parameters.AddWithValue("@location", location)
            cmd.Parameters.AddWithValue("@size", size)
            cmd.Parameters.AddWithValue("@comments", comments)
            cmd.Parameters.AddWithValue("@lastupdate", lastUpdate)

            cmd.ExecuteNonQuery()
          End Using

        End If
      End If ' link OK
    Next link

    Me.Cursor = Cursors.Default

    ' File.WriteAllText("c:\tmp\tmp.txt", s & Crlf & s1)

  End Sub

  Private Sub cmdGPSLocate_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGPSLocate.Click

    Dim locale As String = ""
    Dim county As String = ""
    Dim state As String = ""
    Dim country As String = ""

    processing = True
    Me.Cursor = Cursors.WaitCursor
    GPSLocate(txGPS.Text, locale, county, state, country)

    txLocation.Text = locale
    txCounty.Text = county
    txState.Text = state
    txCountry.Text = country

    Me.Cursor = Cursors.Default
    processing = False

  End Sub

  Private Sub txImageSet_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txImageSet.Leave

    updateImageSet()
    loadimageset()

  End Sub

  Sub loadimageset()

    Dim dr As DataRow
    Dim setid, imageid As Integer
    Dim fName As String
    Dim i As Integer

    cbImageSet.Items.Clear()
    cbImageSet.Text = ""

    If txImageSet.Text.Trim = "" OrElse Not IsNumeric(txImageSet.Text) Then Exit Sub

    setid = txImageSet.Text

    Using ds As DataSet = getDS("select * from imagesets where setid = @parm1", setid)

      i = -1
      If ds IsNot Nothing AndAlso ds.Tables.Count >= 0 Then
        For Each dr In ds.Tables(0).Rows
          If Not IsDBNull(dr("imageid")) Then
            imageid = dr("imageid")
            fName = getScalar("SELECT filename FROM images WHERE imageid = @parm1", imageid)
            If fName <> "" Then
              cbImageSet.Items.Add(fName)
              If fName = txFileName.Text Then i = cbImageSet.Items.Count - 1
            End If
          End If
        Next dr
        If i >= 0 Then cbImageSet.SelectedIndex = i
      End If
    End Using

  End Sub

  Sub updateImageSet()

  End Sub

  Private Sub cbImageSet_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbImageSet.SelectedIndexChanged

    Dim i As Integer
    Dim s As String

    If processing Then Exit Sub
    s = cbImageSet.SelectedItem

    If eqstr(Path.GetDirectoryName(filenames(ix(0))), iniBugPath) Then
      For i = 0 To filenames.Count - 1
        If eqstr(s, Path.GetFileName(filenames(ix(i)))) Then ' found it
          nextPic(i)
          Exit Sub
        End If
      Next i
    End If

    'If useQuery Then ' show a picture not selected
    processing = True

    Me.Cursor = Cursors.WaitCursor
    lbPicPath.Text = s
    txFileName.Text = s
    picpath = getTargetPath()
    currentpicPath = getTargetPath()
    pComments = New List(Of PropertyItem)

    initFields()

    txTaxon.Select()

    processing = False
    Me.Cursor = Cursors.Default
    '  End If

  End Sub

  Private Sub frmBugPhotos_Closing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing

    If abort Or cropping Or measuring Then
      e.Cancel = True
      pView.Zoom(0)
      cropping = False ' just in case
      measuring = False ' just in case
      abort = False
      Me.Cursor = Cursors.Default
      Exit Sub
    End If

  End Sub

End Class






Public Class cWebClient

  Inherits WebClient
  Public cc As New CookieContainer()
  Private lastPage As String


  Protected Overrides Function GetWebRequest(ByVal address As System.Uri) As System.Net.WebRequest

    Dim R As WebRequest = MyBase.GetWebRequest(address)

    If cc.Count = 0 Then
      Dim cook As New Cookie
      cook.Domain = "bugguide.net"
      cook.Name = "PHPSESSID"
      cook.Value = "lai76ps3ijbjkgdb95fg8vt5h2"
      cc.Add(cook)
    End If

    If TypeOf R Is HttpWebRequest Then

      With DirectCast(R, HttpWebRequest)
        .CookieContainer = cc
        If lastPage IsNot Nothing Then
          .Referer = lastPage
        End If
      End With
    End If

    lastPage = address.ToString()
    Return R

  End Function




End Class

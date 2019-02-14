Imports vb = Microsoft.VisualBasic
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.Text
Imports System.IO
Imports System
Imports System.Collections.Generic
Imports ImageMagick

Public Module ImageInfo

  Public sonyColorMode As New Dictionary(Of Integer, String)
  Public olympusLens As New Dictionary(Of String, String)(System.StringComparer.OrdinalIgnoreCase)
  Public olympusCamera As New Dictionary(Of String, String)(System.StringComparer.OrdinalIgnoreCase)
  Public olympusMagicFilter As New Dictionary(Of Integer, String)
  Public olympusSceneMode As New Dictionary(Of Integer, String)
  Public olympusArtFilter As New Dictionary(Of Integer, String)
  Public olympusArtFilterEffect As New Dictionary(Of Integer, String)
  Public sonyPictureEffect As New Dictionary(Of Integer, String)
  Public sonyModelID As New Dictionary(Of Integer, String)

  Dim pformatcode As Object

  Dim uz As uExif
  Dim tg As uTag

  Dim strInfo As String

  Dim shortMake As String

  Public ux As uExif ' main tag. This is nothing if lead tags are used.

  Function readComments(filename As String, processIFDs As Boolean, processMakerNote As Boolean) As uExif

    ' get information from the image, both exif and regular. The result go into picinfo and readcomments.

    Dim i As Integer
    Dim make, model As String
    Dim tg As uTag
    Dim exifTags, gpsTags As Collection
    Dim ux As uExif = Nothing
    Dim ux1 As uExif ' exif ifd
    Dim ux2 As uExif ' gps ifd
    Dim ux4 As uExif = Nothing ' makernote ifd -- temporary
    Dim s As String

    If filename = "" Then Return Nothing

    '' add png metadata and picinfo for other formats
    s = ".jpg" & ".jpeg" & ".tif" & ".tiff"
    If s.IndexOf(Path.GetExtension(filename), StringComparison.OrdinalIgnoreCase) >= 0 Then ' try to read exif directly
      ux = New uExif
      i = ux.readExif(filename)
    Else
      i = -1
    End If

    If i >= 0 Then  ' successful - got comments in ux
      ' there are now four places tags can be:
      ' 1. the main tag area
      ' 2. the exif sub area (IFD)
      ' 3. the gps (IFD)
      ' 4. the interop (IFD) -- (removed)
      ' there may be some others
      If processIFDs Then

        tagVerify(ux.Tags)
        If ux.tagExists(uExif.TagID.exifpointer) Then
          tg = ux.Tags.Item(sTag(uExif.TagID.exifpointer))
          If tg.IFD IsNot Nothing Then
            exifTags = tg.IFD.Tags
            tagVerify(exifTags)

            ' makernote
            If processMakerNote AndAlso tg.IFD.tagExists(uExif.TagID.makernote) Then
              If tg.Link > 0 And ux.tagExists(uExif.TagID.make) And ux.tagExists(uExif.TagID.model) Then
                If ux.IntelNumbers Then i = 1 Else i = 0
                tg = tg.IFD.Tags.Item(sTag(uExif.TagID.makernote))
                make = ux.Tags(sTag(uExif.TagID.make)).singlevalue
                model = ux.Tags(sTag(uExif.TagID.model)).singlevalue
                readMakernote(ux.fdata, tg, i, 1, make, model) ' the makernote tag has both a value (for saving) and an IFD (for display)
              End If
            End If
          End If
        End If

        If ux.tagExists(uExif.TagID.GPSpointer) Then
          tg = ux.Tags.Item(sTag(uExif.TagID.GPSpointer))
          If tg.IFD IsNot Nothing Then
            gpsTags = tg.IFD.Tags
            tagVerify(gpsTags)
          End If
        End If
      End If

      If ux.tagExists(uExif.TagID.description) Then
        tg = ux.Tags(sTag(uExif.TagID.description))
        s = tg.singleValue
        If eqstr(s, "Minolta DSC") Or
           eqstr(s, "OLYMPUS DIGITAL CAMERA") Or
           eqstr(s, "LEAD Technologies Inc. V1.01") Then ' get rid of advertising
          tg.Value = ""
        End If
      End If

      Return ux ' done with exif reading
    End If

    '----------------------------------------------------------------------------------

    ux1 = Nothing
    ux2 = Nothing
    ux = New uExif

    Return Nothing

  End Function

  Sub makeNewIfd(ByRef ux As uExif, ByRef uxIFD As uExif, ByVal tagID As uExif.TagID)
    Dim tz As uTag
    ' make a tag for this ifd
    tz = New uTag
    tz.tag = tagID
    tz.Value = 0
    uxIFD = New uExif
    tz.IFD = uxIFD
    ux.Tags.Add(tz, tz.key)
  End Sub

  Sub formatPicinfo(picinfo As pictureInfo, ByRef strInfo As String)

    Dim parm As String

    If picinfo.FormatID <> MagickFormat.Unknown Then
      parm = System.Enum.GetName(GetType(MagickFormat), picinfo.FormatID).ToLower & ", " & picinfo.FormatDescription
      strInfo &= "File Type: " & tb & parm & "\par "
    End If
    If picinfo.Width > 0 And picinfo.Height > 0 Then strInfo &= "Image Size: " & tb & picinfo.Width & " x " & picinfo.Height & "\par "

    ' output resolution and units
    If picinfo.ResolutionX > 0 And picinfo.ResolutionY > 0 Then
      parm = Format(picinfo.ResolutionX, "###,##0") & " x " & Format(picinfo.ResolutionY, "###,##0")
      Select Case picinfo.resolutionUnit
        Case 1
          parm = parm & " dots per centimeter"
        Case 2
          parm = parm & " dots per inch"
        Case Else
          parm = parm & " dots per inch" ' default
      End Select
      strInfo &= "Output resolution: " & tb & parm & "\par "

      If picinfo.fileSize > 0 Then strInfo &= "Size on Disk: " & tb & Format(Round(picinfo.fileSize / 1000), "###,###,##0") & " KB" & "\par "
      strInfo &= "Has multiple pages: " & tb & picinfo.hasPages & "\par "

      If picinfo.colorSpace <> ImageMagick.ColorSpace.Undefined Then
        parm = System.Enum.GetName(GetType(ImageMagick.ColorSpace), picinfo.colorSpace)
        strInfo &= "Color Space: " & tb & parm & "\par "
      End If
      If picinfo.colorDepth > 0 Then strInfo &= "Color Depth: " & tb & picinfo.colorDepth & "\par "
      If picinfo.Compression <> CompressionMethod.Undefined Then strInfo &= "Compression Method: " & tb & picinfo.Compression.ToString & "\par "
      If picinfo.hasAlpha Then
        strInfo &= "Alpha Channel is present." & "\par "
      Else
        strInfo &= "Alpha Channel is not present." & "\par "
      End If

    End If ' picinfo legit

  End Sub

  Public Sub formatExifComments(ByVal ShowMakernote As Boolean, ByVal ShowJPEGDetails As Boolean, _
    ByVal showXmp As Boolean, ByVal ShowAllTags As Boolean, _
    ByRef sText As String, ByRef ux As uExif, ByRef picinfo As pictureInfo, pComments As List(Of PropertyItem))

    Dim v As Object
    Dim j, i, k As Integer
    Dim i2 As Integer
    Dim parm As String
    Dim x As Double
    Dim nCols, nrows As Integer
    Dim xTags As Collection
    Dim bb As Byte()
    Dim s1 As String

    If ux Is Nothing Then Exit Sub

    strInfo = "" ' this contains the info
    parm = ""

    ' put the description first
    If ux.tagExists(uExif.TagID.description) Then
      parm = ux.TagValue(uExif.TagID.description, 0)
      If parm Is Nothing Then parm = ""
      parm = splitString(parm)
      If Len(parm) > 0 Then strInfo &= "Description:  " & parm & "\par " & "\par " ' String ' 3
    End If

    formatPicinfo(picinfo, strInfo)

    If ShowAllTags Then ' dump all the tags without descriptions
      strInfo &= "\par " & "{\b Main Tags\b0 } " & "\par "
      strInfo &= dumpTags(ux.Tags)

      If ux.tagExists(uExif.TagID.exifpointer) Then
        xTags = ux.Tags(sTag(uExif.TagID.exifpointer)).IFD.Tags
        If xTags IsNot Nothing AndAlso xTags.Count > 0 Then
          strInfo &= "\par " & "{\b Exif Tags\b0 } " & "\par "
          strInfo &= dumpTags(xTags)
        End If
      End If

      If ux.tagExists(uExif.TagID.GPSpointer) Then
        xTags = ux.Tags(sTag(uExif.TagID.GPSpointer)).IFD.Tags
        If xTags IsNot Nothing AndAlso xTags.Count > 0 Then
          strInfo &= "\par " & "{\b GPS Tags\b0 } " & "\par "
          strInfo &= dumpTags(xTags)
        End If
      End If

      If ux.iptcTags IsNot Nothing AndAlso ux.iptcTags.Count > 0 Then
        strInfo &= "\par " & "{\b IPTC Tags\b0 } " & "\par "
        strInfo &= dumpTags(ux.iptcTags)
      End If

      If showXmp AndAlso ux.xmp IsNot Nothing Then
        strInfo &= "\par " & "{\b XMP Info\b0 } " & "\par "
        strInfo &= formatXmp(ux.xmp)
      End If

      If ShowMakernote And ux.tagExists(uExif.TagID.exifpointer) Then
        uz = ux.tagIFD(uExif.TagID.exifpointer)
        If uz.tagExists(uExif.TagID.makernote) Then
          tg = uz.Tags.Item(sTag(uExif.TagID.makernote))
          If ux.IntelNumbers Then i = 1 Else i = 0
          getMakernote(ux, parm, i, True)
          If Len(parm) > 0 Then strInfo &= "\par " & "{\b Maker Note Tags\b0 } " & "\par " & parm & "\par "
        End If
      End If
      sText = strInfo
      Exit Sub
    End If ' dumping tags
    '--------------------------------------------------------
    ' regular format
    If ux.iptcTagExists(120) Then ' caption
      parm = ux.iptcTags(sTag(120)).singlevalue
      parm = Trim(parm)
      If Len(parm) > 0 Then strInfo &= "IPTC Caption: " & tb & parm & "\par "
    End If

    If ux.tagExists(uExif.TagID.xpComment) Then
      v = ux.TagValue(uExif.TagID.xpComment)
      parm = ""
      If ux.Tags.Item(sTag(uExif.TagID.xpComment)).dataType = 2 Then
        parm = ux.TagValue(uExif.TagID.xpComment, 0)
      Else ' double bytes
        If IsArray(v) Then
          For i = 0 To UBound(v) - 1 Step 2
            parm = parm & ChrW(word(v, i, True)) ' 2-byte characters
          Next i
          parm = splitString(parm)
        End If
      End If

      If Len(parm) > 0 Then strInfo &= "Windows Comment: " & tb & parm & "\par "
    End If

    If ux.tagExists(uExif.TagID.xpKeywords) Then
      v = ux.TagValue(uExif.TagID.xpKeywords)
      parm = ""
      If ux.Tags.Item(sTag(uExif.TagID.xpKeywords)).dataType = 2 Then
        parm = ux.TagValue(uExif.TagID.xpKeywords, 0)
      Else ' double bytes
        If IsArray(v) Then
          For i = 0 To UBound(v) - 1 Step 2
            parm = parm & ChrW(word(v, i, True)) ' 2-byte characters
          Next i
          parm = splitString(parm)
        End If
      End If
      If Len(parm) > 0 Then strInfo &= "Windows Keywords: " & tb & parm & "\par "
    End If

    If ux.tagExists(uExif.TagID.xpTitle) Then
      v = ux.TagValue(uExif.TagID.xpTitle)
      parm = ""
      If ux.Tags.Item(sTag(uExif.TagID.xpTitle)).dataType = 2 Then
        parm = ux.TagValue(uExif.TagID.xpTitle, 0)
      Else ' double bytes
        If IsArray(v) Then
          For i = 0 To UBound(v) - 1 Step 2
            parm = parm & ChrW(word(v, i, True)) ' 2-byte characters
          Next i
          parm = splitString(parm)
        End If
      End If
      If Len(parm) > 0 Then strInfo &= "Windows Title: " & tb & parm & "\par "
    End If

    If ux.tagExists(uExif.TagID.xpSubject) Then
      v = ux.TagValue(uExif.TagID.xpSubject)
      parm = ""
      If ux.Tags.Item(sTag(uExif.TagID.xpSubject)).dataType = 2 Then
        parm = ux.TagValue(uExif.TagID.xpSubject, 0)
      Else ' double bytes
        If IsArray(v) Then
          For i = 0 To UBound(v) - 1 Step 2
            parm = parm & ChrW(word(v, i, True)) ' 2-byte characters
          Next i
          parm = splitString(parm)
        End If
      End If
      If Len(parm) > 0 Then strInfo &= "Windows Subject: " & tb & parm & "\par "
    End If

    If ux.tagExists(uExif.TagID.xpAuthor) Then
      v = ux.TagValue(uExif.TagID.xpAuthor)
      parm = ""
      If ux.Tags.Item(sTag(uExif.TagID.xpAuthor)).dataType = 2 Then
        parm = ux.TagValue(uExif.TagID.xpAuthor, 0)
      Else ' double bytes
        If IsArray(v) Then
          For i = 0 To UBound(v) - 1 Step 2
            parm = parm & ChrW(word(v, i, True)) ' 2-byte characters
          Next i
          parm = splitString(parm)
        End If
      End If
      If Len(parm) > 0 Then strInfo &= "Windows Author: " & tb & parm & "\par "
    End If

    '-----------------------
    If ux.Tags.Count() > 0 Then
      strInfo &= "\par " & "{\b Exif Information\b0 }" & "  \par "
    End If

    If ux.tagExists(uExif.TagID.artist) Then
      parm = ux.TagValue(uExif.TagID.artist, 0)
      parm = Trim(parm)
      strInfo &= "Artist: " & tb & parm & "\par "
    End If

    If ux.tagExists(uExif.TagID.copyright) Then
      parm = ux.TagValue(uExif.TagID.copyright, 0)
      If parm <> "" Then strInfo &= "Copyright: " & tb & ux.TagValue(uExif.TagID.copyright, 0) & "\par "
    End If

    If ux.tagExists(uExif.TagID.dateTime) Then
      parm = ux.TagValue(uExif.TagID.dateTime, 0)
      If parm Is Nothing Then parm = "" Else parm = parm.Trim(whiteSpace)
      If parm <> "0000:00:00 00:00:00" And Len(parm) = 19 Then
        If parm.Chars(4) = ":" Then
          parm = Replace(parm, ":", "/", 1, 2)
          'parm = Mid(parm, 6, 2) & "/" & Mid(parm, 9, 2) & "/" & Left(parm, 4) & Right(parm, 9)
        End If
        If ux.tagExists(uExif.TagID.exifpointer) Then
          uz = ux.tagIFD(uExif.TagID.exifpointer)
          If uz IsNot Nothing AndAlso uz.tagExists(uExif.TagID.subsectime) Then
            parm = parm & "." & uz.TagValue(uExif.TagID.subsectime, 0)
          End If
        End If
        strInfo &= "Date and Time: " & tb & parm & "\par "
      End If
    End If

    If ux.tagExists(uExif.TagID.exifpointer) And ux.tagIFD(uExif.TagID.exifpointer) IsNot Nothing Then
      uz = ux.tagIFD(uExif.TagID.exifpointer)
      If uz.tagExists(uExif.TagID.dateTimeOriginal) Then
        parm = uz.TagValue(uExif.TagID.dateTimeOriginal, 0)
        If parm Is Nothing Then parm = "" Else parm = parm.Trim(whiteSpace)
        If parm <> "0000:00:00 00:00:00" And Len(parm) >= 19 Then
          If parm.Chars(4) = ":" Then
            parm = Replace(parm, ":", "/", 1, 2)
            'parm = Mid(parm, 6, 2) & "/" & Mid(parm, 9, 2) & "/" & Left(parm, 4) & Right(parm, 9)
          End If
          If uz.tagExists(uExif.TagID.subsectimeoriginal) Then
            parm = parm & "." & uz.TagValue(uExif.TagID.subsectimeoriginal, 0)
          End If
          strInfo &= "Original Date and Time: " & tb & parm & "\par "
        End If
      End If

      If uz.tagExists(uExif.TagID.dateTimeDigitized) Then
        parm = uz.TagValue(uExif.TagID.dateTimeDigitized, 0)
        If parm Is Nothing Then parm = "" Else parm = parm.Trim(whiteSpace)
        If parm <> "0000:00:00 00:00:00" And Len(parm) >= 19 Then
          If parm.Chars(4) = ":" Then
            parm = Replace(parm, ":", "/", 1, 2)
            'parm = Mid(parm, 6, 2) & "/" & Mid(parm, 9, 2) & "/" & Left(parm, 4) & Right(parm, 9)
          End If
          If uz.tagExists(uExif.TagID.subsectimedigitized) Then
            parm = parm & "." & uz.TagValue(uExif.TagID.subsectimedigitized, 0)
          End If
          strInfo &= "Date and Time Digitized: " & tb & parm & "\par "
        End If
      End If
    End If

    If ux.tagExists(uExif.TagID.make) Then
      strInfo &= "Make: " & tb & ux.TagValue(uExif.TagID.make, 0) & "\par "
    End If

    If ux.tagExists(uExif.TagID.model) Then
      strInfo &= "Model: " & tb & ux.TagValue(uExif.TagID.model, 0) & "\par "
    End If

    If ux.tagExists(uExif.TagID.software) Then
      strInfo &= "Software: " & tb & ux.TagValue(uExif.TagID.software, 0) & "\par "
    End If

    '  If ux.tagexists(XResolution) And ux.tagexists(YResolution) Then
    '    parm = Format(ux.TagValue(XResolution, 0), "###,###") & " x " & Format(ux.TagValue(YResolution, 0), "###,###")
    '    If ux.tagexists(ResolutionUnit) Then
    '      Select Case ux.TagValue(ResolutionUnit, 0)
    '        Case 2
    '          parm = parm & " dots per inch"
    '        Case 3
    '          parm = parm & " dots per centimeter"
    '        Case Else
    '          parm = parm & " units: " & ux.TagValue(ResolutionUnit, 0)
    '          end Select
    '      parm = parm
    '        end If
    '    strInfo &= "Output Resolution: " & tb & parm & "\par "
    '      end If

    '---------------

    If ux.tagExists(uExif.TagID.exifpointer) And ux.tagIFD(uExif.TagID.exifpointer) IsNot Nothing Then
      uz = ux.tagIFD(uExif.TagID.exifpointer)

      If uz.tagExists(uExif.TagID.hostcomputer) Then
        strInfo &= "Host Computer: " & tb & uz.TagValue(uExif.TagID.hostcomputer, 0) & "\par "
      End If

      If uz.tagExists(uExif.TagID.documentName) Then
        strInfo &= "Document Name: " & tb & uz.TagValue(uExif.TagID.documentName, 0) & "\par "
      End If

      If uz.tagExists(uExif.TagID.pagename) Then
        strInfo &= "Page Name: " & tb & uz.TagValue(uExif.TagID.pagename, 0) & "\par "
      End If

      If uz.tagExists(uExif.TagID.pageNumber) Then
        strInfo &= "Page Number: " & tb & uz.TagValue(uExif.TagID.pageNumber, 0) & "\par "
      End If

      If uz.tagExists(uExif.TagID.imageuniqueid) Then
        strInfo &= "Unique Identifier: " & tb
        v = uz.TagValue(uExif.TagID.imageuniqueid)
        parm = ""
        If uuBound(v) >= 0 Then
          For i = 0 To UBound(v)
            parm = parm & v(i)
          Next i
        Else
          parm = v
        End If
        strInfo &= parm & "\par "
      End If

      If uz.tagExists(uExif.TagID.usercomment) Then
        v = uz.TagValue(uExif.TagID.usercomment)
        parm = ""
        For j = 8 To UBound(v)
          parm = parm & ChrW(v(j))
        Next j

        parm = parm.Trim(whiteSpace)
        parm = splitString(parm)
        If Len(parm) > 0 Then strInfo &= "User Comment: " & tb & parm & "\par "
      End If

      If Right(strInfo, 10) <> "\par \par " Then strInfo &= "\par "

      If uz.tagExists(uExif.TagID.cameraOwnerName) Then
        strInfo &= "Camera owner name: " & tb & uz.TagValue(uExif.TagID.cameraOwnerName, 0) & "\par "
      End If

      If uz.tagExists(uExif.TagID.bodySerialNumber) Then
        strInfo &= "Body serial number: " & tb & uz.TagValue(uExif.TagID.bodySerialNumber, 0) & "\par "
      End If

      If uz.tagExists(uExif.TagID.lensSpecification) Then
        v = uz.TagValue(uExif.TagID.lensSpecification)
        parm = ""
        If v.length >= 2 And IsNumeric(v(0)) Then
          parm = "Lens specification:" & tb & Format(v(0), "#") & "-" & Format(v(1), "# mm")
          If v.length >= 3 Then parm &= ", F" & Format(v(2), "#.0")
          If v.length = 4 AndAlso v(2) <> v(3) Then parm &= "-" & Format(v(3), "#.0")
        End If
        If parm <> "" Then strInfo &= parm & "\par "
      End If

      If uz.tagExists(uExif.TagID.lensMake) Then
        strInfo &= "Lens make: " & tb & uz.TagValue(uExif.TagID.lensMake, 0) & "\par "
      End If

      If uz.tagExists(uExif.TagID.lensModel) Then
        strInfo &= "Lens model: " & tb & uz.TagValue(uExif.TagID.lensModel, 0) & "\par "
      End If

      If uz.tagExists(uExif.TagID.lensSerialNumber) Then
        v = uz.TagValue(uExif.TagID.lensSerialNumber, 0)
        strInfo &= "Lens serial number: " & tb & uz.TagValue(uExif.TagID.lensSerialNumber, 0) & "\par "
      End If

      If Right(strInfo, 10) <> "\par \par " Then strInfo &= "\par "

      If uz.tagExists(uExif.TagID.shutterspeed) Then
        x = uz.TagValue(uExif.TagID.shutterspeed, 0)
        If Abs(x) < 1000 Then x = 1 / (2 ^ x) Else x = 0
        If x < 1 And x <> 0 Then
          strInfo &= "Shutter Speed:" & tb & Format(x, "##0.0###") & " (1/" & Format(1 / x, "##,###") & ") sec." & "\par "
        Else
          strInfo &= "Shutter Speed:" & tb & Format(x, "##0.0") & " sec." & "\par "
        End If
      End If

      If uz.tagExists(uExif.TagID.exposuretime) Then
        x = uz.TagValue(uExif.TagID.exposuretime, 0)
        If x > 0.00000000001 Then
          If x < 1 Then
            strInfo &= "Exposure Time:" & tb & Format(x, "##0.0###") & " (1/" & Format(1 / x, "##,###") & ") sec." & "\par "
          Else
            strInfo &= "Exposure Time:" & tb & Format(x, "##0.0") & " sec." & "\par "
          End If
        End If
      End If

      If uz.tagExists(uExif.TagID.exposurebias) Then
        x = uz.TagValue(uExif.TagID.exposurebias, 0)
        strInfo &= "Exposure Bias: " & tb & Format(x, "##0.00") & "\par "
      End If

      If uz.tagExists(uExif.TagID.exposureprogram) Then
        Select Case uz.TagValue(uExif.TagID.exposureprogram, 0)
          Case 1
            parm = "Manual"
          Case 2
            parm = "Normal Program"
          Case 3
            parm = "Aperture Priority"
          Case 4
            parm = "Shutter Priority"
          Case 5
            parm = "Creative Program (biased toward depth of field)"
          Case 6
            parm = "Action Program (biased toward fast shutter speed)"
          Case 7
            parm = "Portrait Mode (for closeup with background out of focus)"
          Case 8
            parm = "Landscape Mode (for landscape with background in focus)"
          Case Else
            parm = uz.TagValue(uExif.TagID.exposureprogram, 0)
        End Select
        strInfo &= "Exposure Program: " & tb & parm & "\par " ' Integer ' 160
      End If

      If uz.tagExists(uExif.TagID.exposureMode) Then
        Select Case uz.TagValue(uExif.TagID.exposureMode, 0)
          Case 0
            parm = "Auto Exposure"
          Case 1
            parm = "Manual Exposure"
          Case 2
            parm = "Auto Bracket"
          Case Else
            parm = uz.TagValue(uExif.TagID.exposureMode, 0)
        End Select
        strInfo &= "Exposure Mode: " & tb & parm & "\par "
      End If

      If uz.tagExists(uExif.TagID.fnumber) Then
        x = uz.TagValue(uExif.TagID.fnumber, 0)
        If x > 0 Then strInfo &= "F-Number:" & tb & Format(x, "##0.0") & "\par "
      End If

      If uz.tagExists(uExif.TagID.aperture) Then
        x = uz.TagValue(uExif.TagID.aperture, 0)
        If x > 0 Then strInfo &= "Aperture: " & tb & Format(x, "##0.0") & "\par "
      End If

      If uz.tagExists(uExif.TagID.maxaperture) Then
        x = uz.TagValue(uExif.TagID.maxaperture, 0)
        If x > 0 Then strInfo &= "Max Aperture: " & tb & Format(x, "##0.0") & "\par "
      End If

      If uz.tagExists(uExif.TagID.PhotographicSensitivity) Then
        v = uz.TagValue(uExif.TagID.PhotographicSensitivity)
        s1 = "" : parm = ""
        For i = 0 To UBound(v)
          s1 = s1 & v(i) & " "
        Next i
        If s1 <> "0" And s1 <> "" Then
          If IsNumeric(s1) Then s1 = Format(Val(s1), "#,0.##")
          If uz.tagExists(uExif.TagID.sensitivityType) Then
            x = uz.TagValue(uExif.TagID.sensitivityType, 0)
            If IsNumeric(x) Then i = x Else i = 0
            Select Case i
              Case 1
                parm = "Standard output sensitivity:"
              Case 2
                parm = "Recommended exposure index (1):"
              Case 3
                parm = "ISO speed:"
              Case 4
                parm = "Standard output sensitivity and recommended exposure index:"
              Case 5
                parm = "Standard output sensitivity and ISO speed:"
              Case 6
                parm = "Recommended exposure index and ISO speed:"
              Case 7
                parm = "Standard output sensitivity, recommended exposure index, and ISO speed:"
            End Select
          Else
            parm = "ISO speed:"
          End If
        End If
        If parm <> "" Then strInfo &= parm & tb & s1 & "\par "
      End If

      If uz.tagExists(uExif.TagID.standardOutputSensitivity) Then
        x = uz.TagValue(uExif.TagID.standardOutputSensitivity, 0)
        If x > 0 Then strInfo &= "Standard Output Sensitivity:" & tb & Format(x, "#,#") & "\par "
      End If

      If uz.tagExists(uExif.TagID.iSOSpeed) Then
        x = uz.TagValue(uExif.TagID.iSOSpeed, 0)
        If x > 0 Then strInfo &= "ISO speed:" & tb & Format(x, "#,#") & "\par "
      End If

      If uz.tagExists(uExif.TagID.recommendedExposureIndex) Then
        x = uz.TagValue(uExif.TagID.recommendedExposureIndex, 0)
        If x > 0 Then strInfo &= "Recommended exposure index (2):" & tb & Format(x, "#,#") & "\par "
      End If

      If uz.tagExists(uExif.TagID.iSOSpeedLatitudeyyy) Then
        x = uz.TagValue(uExif.TagID.iSOSpeedLatitudeyyy, 0)
        If x > 0 Then strInfo &= "ISO speed latitude yyy:" & tb & Format(x, "#,#") & "\par "
      End If

      If uz.tagExists(uExif.TagID.iSOSpeedLatitudezzz) Then
        x = uz.TagValue(uExif.TagID.iSOSpeedLatitudezzz, 0)
        If x > 0 Then strInfo &= "ISO speed latitude zzz:" & tb & Format(x, "#,#") & "\par "
      End If

      If Right(strInfo, 10) <> "\par \par " Then strInfo &= "\par "

      If uz.tagExists(uExif.TagID.subjectdistance) Then
        x = uz.TagValue(uExif.TagID.subjectdistance, 0)
        If x > 0 Then strInfo &= "Subject Distance (meters): " & tb & Format(x, "##0.0") & "\par "
      End If

      If uz.tagExists(uExif.TagID.subjectDistanceRange) Then
        Select Case uz.TagValue(uExif.TagID.subjectDistanceRange, 0)
          Case 0
            parm = "Unknown"
          Case 1
            parm = "Macro"
          Case 2
            parm = "Close"
          Case 3
            parm = "Distant"
          Case Else
            parm = uz.TagValue(uExif.TagID.subjectDistanceRange, 0)
        End Select
        strInfo &= "Subject Distance Range: " & tb & parm & "\par "
      End If

      If uz.tagExists(uExif.TagID.focallength) Then
        x = uz.TagValue(uExif.TagID.focallength, 0)
        If x > 0 Then strInfo &= "Focal Length: " & tb & Format(x, "###,##0") & " mm" & "\par "
      End If

      If uz.tagExists(uExif.TagID.focalLengthIn35mmFilm) Then
        x = uz.TagValue(uExif.TagID.focalLengthIn35mmFilm, 0)
        If x > 0 Then strInfo &= "Focal Length (35mm): " & tb & Format(x, "###,##0") & " mm" & "\par "
      End If

      If uz.tagExists(uExif.TagID.digitalZoomRatio) Then
        x = uz.TagValue(uExif.TagID.digitalZoomRatio, 0)
        If x <> 0 Then
          parm = Format(x, "###,##0.0")
        Else
          parm = "Not Used"
        End If
        strInfo &= "Digital Zoom: " & tb & parm & "\par "
      End If

      If uz.tagExists(uExif.TagID.brightness) Then
        x = uz.TagValue(uExif.TagID.brightness, 0)
        strInfo &= "Brightness: " & tb & Format(x, "##0.0") & "\par "
      End If

      If uz.tagExists(uExif.TagID.contrast) Then
        Select Case uz.TagValue(uExif.TagID.contrast, 0)
          Case 0
            parm = "Normal"
          Case 1
            parm = "Soft"
          Case 2
            parm = "Hard"
          Case Else
            parm = uz.TagValue(uExif.TagID.contrast, 0)
        End Select
        strInfo &= "Contrast: " & tb & parm & "\par "
      End If

      If uz.tagExists(uExif.TagID.saturation) Then
        Select Case uz.TagValue(uExif.TagID.saturation, 0)
          Case 0
            parm = "Normal"
          Case 1
            parm = "Low Saturation"
          Case 2
            parm = "High Saturation"
          Case Else
            parm = uz.TagValue(uExif.TagID.saturation, 0)
        End Select
        strInfo &= "Saturation: " & tb & parm & "\par "
      End If

      If uz.tagExists(uExif.TagID.sharpness) Then
        Select Case uz.TagValue(uExif.TagID.sharpness, 0)
          Case 0
            parm = "Normal"
          Case 1
            parm = "Soft"
          Case 2
            parm = "Hard"
          Case Else
            parm = uz.TagValue(uExif.TagID.sharpness, 0)
        End Select
        strInfo &= "Sharpness: " & tb & parm & "\par "
      End If

      If uz.tagExists(uExif.TagID.whiteBalance) Then
        Select Case uz.TagValue(uExif.TagID.whiteBalance, 0)
          Case 0
            parm = "Auto"
          Case 1
            parm = "Manual"
          Case Else
            parm = uz.TagValue(uExif.TagID.whiteBalance, 0)
        End Select
        strInfo &= "White Balance: " & tb & parm & "\par "
      End If

      If uz.tagExists(uExif.TagID.lightsource) Then
        Select Case uz.TagValue(uExif.TagID.lightsource, 0)
          Case 0
            parm = "Unknown"
          Case 1
            parm = "Daylight"
          Case 2
            parm = "Fluorescent Light"
          Case 3
            parm = "Tungsten Lamp"
          Case 4
            parm = "Flash"
          Case 9
            parm = "Sunny"
          Case 10
            parm = "Cloudy"
          Case 11
            parm = "Shade"
          Case 12
            parm = "Daylight Fluorescent (D5700 - 7100K)"
          Case 13
            parm = "Day White Fluorescent (N4600 - 5400K)"
          Case 14
            parm = "Daylight Fluorescent (W3900 - 4500K)"
          Case 15
            parm = "White Fluorescent (WW3200 - 3700K)"
          Case 17
            parm = "Standard Light Source A"
          Case 18
            parm = "Standard Light Source B"
          Case 19
            parm = "Standard Light Source C"
          Case 20
            parm = "D55"
          Case 21
            parm = "D65"
          Case 22
            parm = "D75"
          Case 23
            parm = "D50"
          Case 24
            parm = "ISO Studio Tungsten"
          Case 255
            parm = "Other Light Source"
          Case Else
            parm = uz.TagValue(uExif.TagID.lightsource, 0)
        End Select
        strInfo &= "Light Source: " & tb & parm & "\par " ' Integer ' 31
      End If

      If uz.tagExists(uExif.TagID.sceneCaptureType) Then
        Select Case uz.TagValue(uExif.TagID.sceneCaptureType, 0)
          Case 0
            parm = "Standard"
          Case 1
            parm = "Landscape"
          Case 2
            parm = "Portrait"
          Case 3
            parm = "Night Scene"
          Case Else
            parm = uz.TagValue(uExif.TagID.sceneCaptureType, 0)
        End Select
        strInfo &= "Scene Capture Type: " & tb & parm & "\par "
      End If

      If uz.tagExists(uExif.TagID.meteringmode) Then
        Select Case uz.TagValue(uExif.TagID.meteringmode, 0)
          Case 1
            parm = "Average"
          Case 2
            parm = "Center Weighted Average"
          Case 3
            parm = "Spot"
          Case 4
            parm = "Multi Spot"
          Case 5
            parm = "Pattern"
          Case 6
            parm = "Partial"
          Case Else
            parm = uz.TagValue(uExif.TagID.meteringmode, 0)
        End Select
        strInfo &= "Metering Mode: " & tb & parm & "\par " ' Integer ' 30
      End If

      If uz.tagExists(uExif.TagID.flash) Then
        x = uz.TagValue(uExif.TagID.flash, 0)
        x = x Mod 8
        Select Case x
          Case 0
            parm = "Not Fired"
          Case 1
            parm = "Fired"
          Case 5
            parm = "Strobe Return Light Not Detected"
          Case 7
            parm = "Strobe Return Light Detected"
          Case Else
            parm = CStr(x)
        End Select
        strInfo &= "Flash: " & tb & parm & "\par " ' Integer ' 32
      End If

      If uz.tagExists(uExif.TagID.flashenergy) Then
        x = uz.TagValue(uExif.TagID.flashenergy, 0)
        strInfo &= "Flash Energy (BCPS): " & tb & Format(x, "###,##0.0") & "\par "
      End If

      If uz.tagExists(uExif.TagID.spectralsensitivity) Then
        strInfo &= "Spectral Sensitivity: " & tb & uz.TagValue(uExif.TagID.spectralsensitivity, 0) & "\par " ' String ' 161
      End If

      If uz.tagExists(uExif.TagID.focalplaneXresolution) And uz.tagExists(uExif.TagID.focalplaneYresolution) Then
        x = uz.TagValue(uExif.TagID.focalplaneXresolution, 0)
        parm = Format(x, "###,##0")
        x = uz.TagValue(uExif.TagID.focalplaneYresolution, 0)
        parm = parm & " x " & Format(x, "###,##0")
        If uz.tagExists(uExif.TagID.focalPlaneResolutionUnit) Then
          Select Case uz.TagValue(uExif.TagID.focalPlaneResolutionUnit, 0)
            Case 2
              parm = parm & " pixels per inch"
            Case 3
              parm = parm & " pixels per centimeter"
            Case Else
              parm = parm & " units: " & uz.TagValue(uExif.TagID.focalPlaneResolutionUnit, 0)
          End Select
        End If
        strInfo &= "Focal Plane Resolution: " & tb & parm & "\par "
      End If

      If uz.tagExists(uExif.TagID.subjectlocation) Then
        v = uz.TagValue(uExif.TagID.subjectlocation)
        If UBound(v) >= 1 Then strInfo &= "Subject Location: " & tb & v(0) & ", " & v(1) & "\par "
      End If

      If uz.tagExists(uExif.TagID.subjectArea) Then
        v = uz.TagValue(uExif.TagID.subjectArea)
        parm = ""
        Select Case UBound(v)
          Case 1 ' point
            parm = Format(v(0), "##,###") & ", " & Format(v(1), "##,###")
          Case 2 ' center, radius
            parm = "Center: " & Format(v(0), "#####") & ", " & Format(v(1), "#####") & ", Diameter: " & Format(v(2), "####0.0")
          Case 3 ' rectangle
            parm = "Center " & Format(v(0), "#####") & "," & Format(v(1), "#####") & ", Width " & Format(v(2), "#####") & ", Height " & Format(v(3), "#####")
          Case Else
            For i = 0 To UBound(v)
              parm = parm & v(i) & " "
            Next i
        End Select
        strInfo &= "Subject Area: " & tb & parm & "\par "
      End If

      If uz.tagExists(uExif.TagID.exposureindex) Then
        x = uz.TagValue(uExif.TagID.exposureindex, 0)
        strInfo &= "Exposure Index: " & tb & Format(x, "###,###") & "\par "
      End If

      If Right(strInfo, 10) <> "\par \par " Then strInfo &= "\par "

      If uz.tagExists(uExif.TagID.colorspace) Then
        If uz.TagValue(uExif.TagID.colorspace, 0) = 1 Then
          strInfo &= "Color Space: " & tb & "sRGB" & "\par "
        Else
          strInfo &= "Color Space: " & tb & uz.TagValue(uExif.TagID.colorspace, 0) & "\par " ' Integer ' 159
        End If
      End If

      If uz.tagExists(uExif.TagID.sensingmethod) Then
        Select Case uz.TagValue(uExif.TagID.sensingmethod, 0)
          Case 2
            parm = "One-Chip Color Area Sensor"
          Case 3
            parm = "Two-Chip Color Area Sensor"
          Case 4
            parm = "Three-Chip Color Area Sensor"
          Case 5
            parm = "Color Sequential Area Sensor"
          Case 7
            parm = "Trilinear Sensor"
          Case 8
            parm = "Color Sequential Sensor"
          Case Else
            parm = uz.TagValue(uExif.TagID.sensingmethod, 0)
        End Select
        strInfo &= "Sensing Method: " & tb & parm & "\par " ' Integer ' 171
      End If

      If uz.tagExists(uExif.TagID.filesource) Then
        j = uz.TagValue(uExif.TagID.filesource, 0)
        If j = 3 Then parm = "Digital Still Camera" Else parm = CStr(j)
        strInfo &= "File Source: " & tb & parm & "\par "
      End If

      If uz.tagExists(uExif.TagID.scenetype) Then
        j = uz.TagValue(uExif.TagID.scenetype, 0)
        If j = 1 Then parm = "Directly Photographed Image" Else parm = CStr(j)
        strInfo &= "Scene Type: " & tb & parm & "\par " ' Variant ' 173
      End If

      If uz.tagExists(uExif.TagID.customRendered) Then
        Select Case uz.TagValue(uExif.TagID.customRendered, 0)
          Case 0
            parm = "No"
          Case 1
            parm = "Yes"
          Case Else
            parm = uz.TagValue(uExif.TagID.customRendered, 0)
        End Select
        strInfo &= "Custom Rendered: " & tb & parm & "\par "
      End If

      If uz.tagExists(uExif.TagID.deviceSettingDescription) Then
        v = uz.TagValue(uExif.TagID.deviceSettingDescription)
        strInfo &= "Device Setting Description: " & tb
        For j = 0 To UBound(v)
          If v(j) = 0 Then
            strInfo &= "\par "
          Else
            strInfo &= ChrW(v(j))
          End If
        Next j
        strInfo &= "\par "
      End If

      If uz.tagExists(uExif.TagID.gainControl) Then
        Select Case uz.TagValue(uExif.TagID.gainControl, 0)
          Case 0
            parm = "(none)"
          Case 1
            parm = "Low Gain Up"
          Case 2
            parm = "High Gain Up"
          Case 3
            parm = "Low Gain Down"
          Case 4
            parm = "High Gain Down"
          Case Else
            parm = uz.TagValue(uExif.TagID.gainControl, 0)
        End Select
        strInfo &= "Gain Control:" & tb & parm & "\par "
      End If

      If uz.tagExists(uExif.TagID.spatialfrequencyresponse) Then
        strInfo &= "Spatial Frequency Response:"
        v = uz.TagValue(uExif.TagID.spatialfrequencyresponse)
        infoMatrix(v, parm)
        strInfo &= parm
      End If

      If uz.tagExists(uExif.TagID.oecf) Then
        strInfo &= "Opto-Electric Coefficient: " & "\par " ' Variant ' 163
        v = uz.TagValue(uExif.TagID.oecf)
        infoMatrix(v, parm)
        strInfo &= parm
      End If

      If uz.tagExists(uExif.TagID.cfapattern) Then
        v = uz.TagValue(uExif.TagID.cfapattern)
        If UBound(v) > 3 Then

          nCols = v(0) + v(1) * 256
          nrows = v(2) + v(3) * 256
          If nCols > 255 And nCols * nrows > UBound(v) Then ' non-intel format
            nCols = v(1) + v(0) * 256
            nrows = v(3) + v(2) * 256
          End If

          If UBound(v) <= nCols * nrows + 4 Then
            strInfo &= "CFA Pattern: " ' Variant ' 174
            parm = ""
            k = 4 ' add the data
            For j = 1 To nrows
              parm = parm & tb
              For i2 = 1 To nCols
                parm = parm & v(k)
                If i2 <> nCols Then parm = parm & ",  "
                k = k + 1
              Next i2
              parm = parm & "\par "
            Next j
            strInfo &= parm
          End If
        End If
      End If

      If uz.tagExists(uExif.TagID.exifversion) Then
        v = uz.TagValue(uExif.TagID.exifversion)
        If IsArray(v) Then
          If UBound(v) >= 1 Then
            parm = ""
            For i = 0 To UBound(v)
              parm = parm & ChrW(v(i))
            Next i
          ElseIf UBound(v) >= 0 Then
            parm = v(0)
          End If
        End If

        strInfo &= "Exif Version:" & tb & parm & "\par "
      End If

      If uz.tagExists(uExif.TagID.flashpixVersion) Then
        strInfo &= "Supported Flashpix Version:" & tb
        v = uz.TagValue(uExif.TagID.flashpixVersion)
        If TypeOf v Is String AndAlso Not IsArray(v) Then
          strInfo &= v
        ElseIf IsArray(v) AndAlso uuBound(v) = 0 AndAlso TypeOf v(0) Is String Then
          strInfo &= v(0)
        Else
          For j = 0 To UBound(v) : strInfo &= ChrW(v(j)) : Next j
        End If
        strInfo &= "\par "
      End If

      If uz.tagExists(uExif.TagID.relatedsoundfile) Then
        parm = Trim(uz.TagValue(uExif.TagID.relatedsoundfile, 0))
        If Len(parm) > 0 Then strInfo &= "Related Audio File: " & tb & parm & "\par " ' String ' 164
      End If

      strInfo &= "Related Audio File: " & tb & parm & "\par " ' String ' 164
    End If

    bb = getBmpComment(propID.ThumbnailData, pComments)
    If bb IsNot Nothing AndAlso UBound(bb) > 10 Then parm = "Yes" Else parm = "No"
    strInfo &= "Embedded Thumbnail Image: " & tb & parm & "\par "

    '--------------------------

    If ux.tagExists(uExif.TagID.GPSpointer) Then
      uz = ux.tagIFD(uExif.TagID.GPSpointer)
      If uz IsNot Nothing AndAlso uz.Tags.Count() > 1 Then ' skip it if there's only the version ID (or nothing)
        If Right(strInfo, 10) <> "\par \par " Then strInfo &= "\par "
        strInfo &= "{\b GPS Information\b0 }" & "\par "

        If uz.tagExists(uExif.TagID.GPSlatitude) And uz.tagExists(uExif.TagID.GPSlongitude) Then
          ' lat - long coordinates together
          v = uz.TagValue(uExif.TagID.GPSlatitude)
          strInfo &= "GPS location: " & tb & v(0) & "°"
          If v(1) >= 0 And v(1) < 60 Then strInfo &= v(1) & "'"
          If v(2) > 0 And v(2) < 60 Then strInfo &= v(2) & """"
          strInfo &= uz.TagValue(uExif.TagID.GPSlatituderef, 0)
          v = uz.TagValue(uExif.TagID.GPSlongitude)
          strInfo &= ", " & v(0) & "°"
          If v(1) >= 0 And v(1) < 60 Then strInfo &= v(1) & "'"
          If v(2) > 0 And v(2) < 60 Then strInfo &= v(2) & """"
          strInfo &= uz.TagValue(uExif.TagID.GPSlongituderef, 0) & "\par "
        End If

        If uz.tagExists(uExif.TagID.GPSaltituderef) Then
          k = uz.TagValue(uExif.TagID.GPSaltituderef, 0)
        Else
          k = 0
        End If

        If uz.tagExists(uExif.TagID.GPSaltitude) Then
          x = uz.TagValue(uExif.TagID.GPSaltitude, 0)
          If k = 1 And x > 0 Then x = -x
          strInfo &= "GPS Altitude: " & tb & Format(x / 0.3048, "###,##0") & " feet " & "(" & Format(x, "###,##0") & " meters)" & "\par "
        End If

        If uz.tagExists(uExif.TagID.GPSsatellites) Then
          strInfo &= "GPS Satellites: " & tb & uz.TagValue(uExif.TagID.GPSsatellites, 0) & "\par " ' String
        End If

        If uz.tagExists(uExif.TagID.GPSstatus) Then
          strInfo &= "GPS Status: " & tb & uz.TagValue(uExif.TagID.GPSstatus, 0) & "\par " ' String
        End If


        If uz.tagExists(uExif.TagID.GPSmeasuremode) Then
          strInfo &= "GPS Measure Mode: " & tb & uz.TagValue(uExif.TagID.GPSmeasuremode, 0) & "\par " ' String
        End If

        If uz.tagExists(uExif.TagID.GPSsatellites) Then
          strInfo &= "GPS Satellites: " & tb & uz.TagValue(uExif.TagID.GPSsatellites, 0) & "\par " ' String
        End If

        If uz.tagExists(uExif.TagID.GPSdop) Then
          x = uz.TagValue(uExif.TagID.GPSdop, 0)
          strInfo &= "GPS Data Precision: " & tb & x & "\par "
        End If

        If uz.tagExists(uExif.TagID.GPSspeed) Then
          x = uz.TagValue(uExif.TagID.GPSspeed, 0)
          parm = Format(x, "##0.0")
          If uz.tagExists(uExif.TagID.GPSspeedref) Then
            Select Case UCase(uz.TagValue(uExif.TagID.GPSspeedref, 0))
              Case "K"
                parm = parm & " kph"
              Case "M"
                parm = parm & " mph"
              Case "N"
                parm = parm & " knots"
            End Select
          End If
          strInfo &= "GPS Speed: " & tb & parm & "\par "
        End If

        If uz.tagExists(uExif.TagID.GPStrack) Then
          x = uz.TagValue(uExif.TagID.GPStrack, 0)
          parm = Format(x, "##0.0") & "°"
          If uz.tagExists(uExif.TagID.GPStrackref) Then
            If uz.TagValue(uExif.TagID.GPStrackref, 0) = "T" Then parm = parm & " true" Else parm = parm & " magnetic"
          End If
          strInfo &= "GPS Track: " & tb & parm & "\par "
        End If

        If uz.tagExists(uExif.TagID.GPSimgdirection) Then
          x = uz.TagValue(uExif.TagID.GPSimgdirection, 0)
          parm = Format(x, "##0.0") & "°"
          If uz.tagExists(uExif.TagID.GPSimgdirectionref) Then
            If uz.TagValue(uExif.TagID.GPSimgdirectionref, 0) = "T" Then parm = parm & " true" Else parm = parm & " magnetic"
          End If
          strInfo &= "GPS Image Direction Reference: " & tb & parm & "\par "
        End If

        If uz.tagExists(uExif.TagID.GPSmapdatum) Then
          strInfo &= "GPS Map Datum: " & tb & uz.TagValue(uExif.TagID.GPSmapdatum, 0) & "\par "
        End If

        If uz.tagExists(uExif.TagID.GPSdestlatitude) Then
          v = uz.TagValue(uExif.TagID.GPSdestlatitude)
          strInfo &= "GPS Destination Latitude: " & tb & uz.TagValue(uExif.TagID.GPSdestlatituderef, 0) & " " & v(0) & "°" & v(1) & "'"
          If v(2) <> 0 Then strInfo &= v(2) & """"
          strInfo &= "\par "
        End If

        If uz.tagExists(uExif.TagID.GPSdestlongitude) Then
          v = uz.TagValue(uExif.TagID.GPSdestlongitude)
          strInfo &= "GPS Destination Longitude: " & tb & uz.TagValue(uExif.TagID.GPSdestlongituderef, 0) & " " & v(0) & "°" & v(1) & "'"
          If v(2) <> 0 Then strInfo &= v(2) & """"
          strInfo &= "\par "
        End If

        If uz.tagExists(uExif.TagID.GPSdestbearing) Then
          x = uz.TagValue(uExif.TagID.GPSdestbearing, 0)
          parm = Format(x, "##0.0") & "°"
          If uz.tagExists(uExif.TagID.GPSdestbearingref) Then
            If uz.TagValue(uExif.TagID.GPSdestbearingref, 0) = "T" Then parm = parm & " true" Else parm = parm & " magnetic"
          End If
          strInfo &= "GPS Destination Bearing: " & tb & parm & "\par "
        End If

        If uz.tagExists(uExif.TagID.GPSdestdistance) Then
          x = uz.TagValue(uExif.TagID.GPSdestdistance, 0)
          parm = Format(x, "##0.0")
          If uz.tagExists(uExif.TagID.GPSdestdistanceref) Then
            Select Case UCase(uz.TagValue(uExif.TagID.GPSdestdistanceref, 0))
              Case "K"
                parm = parm & " km."
              Case "M"
                parm = parm & " mi."
              Case "N"
                parm = parm & " nm."
            End Select
          End If
          strInfo &= "GPS Destination Distance: " & tb & parm & "\par "
        End If

        If uz.tagExists(uExif.TagID.GPSareaInformation) Then
          v = uz.TagValue(uExif.TagID.GPSareaInformation)
          parm = ""
          For j = 1 To UBound(v) ' skip first byte
            If v(j) >= 9 Then parm = parm & ChrW(v(j))
          Next j
          parm = splitString(parm)
          If Len(parm) > 0 Then strInfo &= "GPS Area Information: " & tb & parm & "\par "
        End If

        parm = ""
        If uz.tagExists(uExif.TagID.GPSdateStamp) Then
          parm = uz.TagValue(uExif.TagID.GPSdateStamp, 0)
          parm = Replace(parm, ":", "/", 1, 2) & " "
        End If
        If uz.tagExists(uExif.TagID.GPStimestamp) Then
          v = uz.TagValue(uExif.TagID.GPStimestamp)
          parm = parm & Format(v(0), "00") & ":" & Format(v(1), "00") & ":" & Format(v(2), "00")
        End If
        If Len(parm) > 0 Then
          strInfo &= "GPS Time Stamp: " & tb & parm & "\par "
        End If

        If uz.tagExists(uExif.TagID.GPSdifferential) Then
          Select Case uz.TagValue(uExif.TagID.GPSdifferential, 0)
            Case 0
              parm = "No"
            Case 1
              parm = "Yes"
            Case Else
              parm = ""
          End Select
          If Len(parm) > 0 Then strInfo &= "Differential Correction:" & tb & parm & "\par "
        End If

        If uz.tagExists(uExif.TagID.GPShPositioningError) Then
          x = uz.TagValue(uExif.TagID.GPShPositioningError, 0)
          If x > 0 Then
            parm = Format(x, "#,##0.00") & " meters"
            strInfo &= "GPS positioning error:" & tb & parm & "\par "
          End If
        End If

        If uz.tagExists(uExif.TagID.GPSprocessingMethod) Then
          v = uz.TagValue(uExif.TagID.GPSprocessingMethod)
          parm = ""
          For j = 1 To UBound(v) ' skip first byte
            If v(j) >= 9 Then parm = parm & ChrW(v(j))
          Next j
          parm = splitString(parm)
          If Len(parm) > 0 Then strInfo &= "GPS Processing Method: " & tb & parm & "\par "
        End If

        If uz.tagExists(uExif.TagID.GPSversionid) Then
          v = uz.TagValue(uExif.TagID.GPSversionid)
          parm = ""
          If UBound(v) >= 3 Then
            If v(0) <> 0 Or v(1) <> 0 Then parm = v(0) & "." & v(1)
            If v(2) <> 0 Or v(3) <> 0 Then parm = parm & "." & v(2)
            If v(3) <> 0 Then parm = parm & "." & v(3)
          End If
          If uuBound(v) >= 3 Then strInfo &= "GPS Version: " & tb & parm & "\par "
        End If

      End If  ' gps tags.count > 1

    End If ' gps tags

    '--------------------------

    ' makernote
    If ShowMakernote AndAlso ux.tagExists(uExif.TagID.exifpointer) AndAlso ux.tagIFD(uExif.TagID.exifpointer) IsNot Nothing Then
      uz = ux.tagIFD(uExif.TagID.exifpointer)
      If uz.tagExists(uExif.TagID.makernote) Then
        tg = uz.Tags.Item(sTag(uExif.TagID.makernote))
        If ux.IntelNumbers Then i = 1 Else i = 0
        If tg.Link > 0 Then
          getMakernote(ux, parm, i, False) ' send the exif.makernote string, receive parm, exif.szmake is the make of the camera
        End If
        If Len(parm) > 0 Then strInfo &= "\par " & "{\b Maker Note\b0 } " & "\par " & parm
      End If
    End If
    '--------------------------

    If showXmp AndAlso ux.xmp IsNot Nothing Then
      strInfo &= "\par " & "{\b XMP Info\b0 } " & "\par "
      strInfo &= formatXmp(ux.xmp)
    End If

    If ShowJPEGDetails Then
      strInfo &= "\par " & "{\b JPEG Details\b0 } " & "\par "

      If ux.tagExists(uExif.TagID.exifpointer) Then
        uz = ux.tagIFD(uExif.TagID.exifpointer)
      Else
        uz = ux
      End If ' prevents error


      parm = ""

      If ux.tagExists(uExif.TagID.bitspersample) Then
        v = ux.TagValue(uExif.TagID.bitspersample)
        strInfo &= "Bits per Sample: " & tb & v(0) & " " & v(1) & " " & v(2) & "\par "
      End If

      If uz.tagExists(uExif.TagID.componentsConfiguration) Then
        strInfo &= "Components Configuration: " & tb
        v = uz.TagValue(uExif.TagID.componentsConfiguration)
        parm = ""
        For i = 0 To UBound(v)
          Select Case v(i)
            Case 1
              parm = parm & "Y" & " "
            Case 2
              parm = parm & "Cb" & " "
            Case 3
              parm = parm & "Cr" & " "
            Case 4
              parm = parm & "R" & " "
            Case 5
              parm = parm & "G" & " "
            Case 6
              parm = parm & "B" & " "
            Case Else
              parm = parm & v(i) & " "
          End Select
        Next i
        strInfo &= parm & "\par "
      End If

      If uz.tagExists(uExif.TagID.compressedBitsPerPixel) Then
        strInfo &= "Compressed Bits per Pixel: " & tb & Format(uz.TagValue(uExif.TagID.compressedBitsPerPixel, 0), "###,##0.0") & "\par "
      End If

      If ux.tagExists(uExif.TagID.compression) Then
        strInfo &= "Compression Scheme: " & tb & ux.TagValue(uExif.TagID.compression, 0) & "\par "
      End If

      If uz.tagExists(uExif.TagID.imageWidth) And uz.tagExists(uExif.TagID.imageHeight) Then
        strInfo &= "EXIF Image Size: " & tb & uz.TagValue(uExif.TagID.imageWidth, 0) & " x " & uz.TagValue(uExif.TagID.imageHeight, 0) & "\par "
      End If

      If ux.tagExists(uExif.TagID.jPEGInterchangeFormat) Then
        strInfo &= "JPEG Interchange Format: " & tb & ux.TagValue(uExif.TagID.jPEGInterchangeFormat, 0) & "\par "
      End If

      If ux.tagExists(uExif.TagID.jPEGInterchangeFormatLength) Then
        strInfo &= "JPEG Interchange Format Length: " & tb & ux.TagValue(uExif.TagID.jPEGInterchangeFormatLength, 0) & "\par "
      End If

      If ux.tagExists(uExif.TagID.orientation) Then
        strInfo &= "JPEG Orientation: " & tb & ux.TagValue(uExif.TagID.orientation, 0) & "\par "
      End If

      If ux.tagExists(uExif.TagID.photometricInterpretation) Then
        Select Case ux.TagValue(uExif.TagID.photometricInterpretation, 0)
          Case 2
            parm = "RGB"
          Case 6
            parm = "YCbCr"
          Case Else
            parm = ux.TagValue(uExif.TagID.photometricInterpretation, 0)
        End Select
        strInfo &= "JPEG Photometric Interpretation:" & tb & parm & "\par "
      End If

      If uz.tagExists(uExif.TagID.pixelXDimension) And uz.tagExists(uExif.TagID.pixelYDimension) Then
        strInfo &= "Valid Image Size: " & tb & uz.TagValue(uExif.TagID.pixelXDimension, 0) & " x " & uz.TagValue(uExif.TagID.pixelYDimension, 0) & "\par "
      End If

      If ux.tagExists(uExif.TagID.planarConfiguration) Then
        Select Case ux.TagValue(uExif.TagID.planarConfiguration, 0)
          Case 1
            parm = "Chunky"
          Case 2
            parm = "Planar"
          Case Else
            parm = ux.TagValue(uExif.TagID.planarConfiguration, 0)
        End Select
        strInfo &= "Planar Configuration:" & tb & parm & "\par "
      End If

      If ux.tagExists(uExif.TagID.primaryChromaticities) Then
        v = ux.TagValue(uExif.TagID.primaryChromaticities)
        parm = ""
        For i = 0 To UBound(v)
          parm = parm & Format(v(i), "##,##0.0# ")
        Next i
        strInfo &= "Primary Chromaticities: " & tb & parm & "\par "
      End If

      If ux.tagExists(uExif.TagID.referenceBlackWhite) Then
        v = ux.TagValue(uExif.TagID.referenceBlackWhite)
        parm = ""
        For i = 0 To UBound(v)
          parm = parm & Format(v(i), "##0.0# ")
        Next i
        strInfo &= "Reference Black and White: " & tb & parm & "\par "
      End If

      If ux.tagExists(uExif.TagID.rowsPerStrip) Then
        strInfo &= "Rows per Strip: " & tb & Format(ux.TagValue(uExif.TagID.rowsPerStrip, 0), "###,###") & "\par "
      End If

      If ux.tagExists(uExif.TagID.samplesPerPixel) Then
        strInfo &= "Samples per Pixel: " & tb & ux.TagValue(uExif.TagID.samplesPerPixel, 0) & "\par "
      End If

      If ux.tagExists(uExif.TagID.stripByteCounts) Then
        strInfo &= "Strip Byte Counts: " & tb
        v = ux.TagValue(uExif.TagID.stripByteCounts)
        parm = ""
        For i = 0 To UBound(v)
          parm = parm & Format(v(i), "###,###,###") & " "
          If (i + 1) Mod 20 = 0 Then parm = parm & "\par " & tb
        Next i
        strInfo &= parm & "\par "
      End If

      If ux.tagExists(uExif.TagID.stripOffsets) Then
        strInfo &= "Strip Offsets: " & tb
        v = ux.TagValue(uExif.TagID.stripOffsets)
        parm = ""
        For i = 0 To UBound(v)
          parm = parm & Format(v(i), "###,###,###") & " "
          If (i + 1) Mod 20 = 0 Then parm = parm & "\par " & tb
        Next i
        strInfo &= parm & "\par "
      End If

      If ux.tagExists(uExif.TagID.transferFunction) Then
        strInfo &= "Transfer Function: " & tb
        v = ux.TagValue(uExif.TagID.transferFunction)
        parm = ""
        For i = 0 To UBound(v)
          parm = parm & v(i) & " "
          If (i + 1) Mod 48 = 0 Then parm = parm & "\par " & tb
        Next i
        strInfo &= parm & "\par "
      End If

      If ux.tagExists(uExif.TagID.whitePoint) Then
        strInfo &= "White Point: " & tb
        v = ux.TagValue(uExif.TagID.whitePoint)
        parm = ""
        For i = 0 To UBound(v)
          parm = parm & v(i) & " "
        Next i
        strInfo &= parm & "\par "
      End If

      If ux.tagExists(uExif.TagID.yCbCrCoefficients) Then
        strInfo &= "YCbCr Coefficients: " & tb
        v = ux.TagValue(uExif.TagID.yCbCrCoefficients)
        parm = ""
        For i = 0 To UBound(v)
          parm = parm & v(i) & " "
        Next i
        strInfo &= parm & "\par "
      End If

      If ux.tagExists(uExif.TagID.yCbCrPositioning) Then
        Select Case ux.TagValue(uExif.TagID.yCbCrPositioning, 0)
          Case 1
            parm = "Centered"
          Case 2
            parm = "Co-sited"
          Case Else
            parm = ux.TagValue(uExif.TagID.yCbCrPositioning, 0)
        End Select
        strInfo &= "YCbCr Positioning: " & tb & parm & "\par "
      End If

      If ux.tagExists(uExif.TagID.yCbCrSubSampling) Then
        strInfo &= "YCbCr SubSampling: " & tb
        v = ux.TagValue(uExif.TagID.yCbCrSubSampling)
        If v(0) = 2 And v(1) = 1 Then
          parm = "YCbCr4:2:2"
        ElseIf v(0) = 2 And v(1) = 2 Then
          parm = "YCbCr4:2:2"
        Else
          parm = ""
          For i = 0 To UBound(v)
            parm = parm & v(i) & " "
          Next i
          strInfo &= parm & "\par "
        End If
      End If

    End If

    sText = strInfo

  End Sub

  Sub getMakernote(ByRef uz As uExif, ByRef note As String, ByVal iintel As Short, _
    ByRef DumpAll As Boolean)

    Dim i As Integer
    Dim intel As Boolean
    Dim make As String
    Dim makerTags As Collection

    make = uz.TagValue(uExif.TagID.make, 0)
    note = ""

    ' get the makernote tags into makertags
    ' the tags are stored in the uExif under uComments (uz) - Exif - Makernote
    makerTags = Nothing
    If uz.tagExists(uExif.TagID.exifpointer) Then
      tg = uz.Tags.Item(sTag(uExif.TagID.exifpointer))
      If tg.IFD IsNot Nothing Then
        If tg.IFD.tagExists(uExif.TagID.makernote) Then
          tg = tg.IFD.Tags.Item(sTag(uExif.TagID.makernote))
          If tg.IFD IsNot Nothing Then makerTags = tg.IFD.Tags
        End If
      End If
    End If

    note = ""
    If makerTags Is Nothing OrElse makerTags.Count = 0 Then Exit Sub

    If DumpAll Then
      note = dumpTags(makerTags)
      Exit Sub
    End If

    If iintel = 0 Then
      intel = False
    ElseIf iintel = 1 Then
      intel = True
    End If

    i = InStr(make, " ")
    If i <= 0 Then shortMake = LCase(make) Else shortMake = LCase(Left(make, i - 1))

    Select Case shortMake

      Case "pentax"
        pentaxMakernote(makerTags, note)
      Case "ricoh"
        ricohMakernote(makerTags, note, intel)
      Case "canon"
        canonMakernote(makerTags, note, intel)
      Case "nikon"
        nikonMakernote(makerTags, note, intel)
      Case "minolta"
        minoltaMakernote(makerTags, note, intel)
      Case "olympus"
        olympusMakernote(makerTags, note)
      Case "sanyo"
        sanyoMakernote(makerTags, note)
      Case "fujifilm"
        fujiMakernote(makerTags, note)
      Case "epson", "seiko"
        epsonMakernote(makerTags, note)
      Case "casio"
        casioMakernote(makerTags, note)
      Case "kodak", "eastman"
        kodakMakernote(makerTags, note)
      Case "panasonic"
        panasonicMakernote(makerTags, note)
      Case "sony"
        sonyMakernote(makerTags, note, intel)

    End Select

  End Sub ' getmakernote

  Sub readMakernoteIFD(ByRef ux As uExif, ByVal mTag As uTag, ByRef fdata As Byte(), ByVal voffset As Integer, _
    ByVal iintel As Short, ByRef intel As Boolean, ByVal relativelinks As Short)

    ' ux will contain the ifd
    ' mtag is the tag from the parent uexif
    ' fdata is the data to be read (from the parent uexif)
    ' voffset it the offset within fdata

    ' iintel: 0 - false, 1 - true, 2 - unknown
    ' relativelinks: 0 - absolute, 1 - relative, 2 - disable links
    ' nikon and fuji use links relative to the makernote data, the others use absolute file links

    Dim i, j As Integer
    iintel = 2
    ux = New uExif
    mTag.IFD = ux
    'k = mTag.Link + voffset
    If iintel = 2 Then ' set intel based on the tag count -- we don't get it from gImage.
      i = word(fdata, voffset, True) : j = word(fdata, voffset, False)
      If (j < 0) Or (j > 500) Or (i < j And i > 0) Then intel = True Else intel = False ' based on tag count
    End If
    ux.getIFDirectory(fdata, voffset, intel, relativelinks)


  End Sub ' readMakernoteIFD

  ' for i = 0 to 49: print "' " & i * 50 & " -  ";: for j = 0 to 49: print v(i*50 + j);: next j: print: next i
  Sub readMakernote(ByRef fdata() As Byte, ByRef mTag As uTag, ByVal iintel As Short, _
    ByVal relativeLinks As Short, ByVal make As String, ByVal model As String)

    ' iintel: 0 - false, 1 - true, 2 - unknown
    ' relativelinks: 0 - absolute, 1 - relative, 2 - disable links
    ' nikon and fuji use links relative to the makernote data, the others use absolute file links

    Dim i As Integer
    Dim k As Integer
    Dim ii() As Integer
    Dim s As String
    Dim parm As String = ""
    Dim vOffset As Integer ' offset in V array of IFD part.
    Dim v As Object
    Dim b() As Byte
    Dim intel As Boolean
    Dim olyNew As Boolean
    Dim uz As uExif ' temp




    ' IFD Format
    ' 2 - ntags - number of direction Entries
    ' for each tag:
    '    2 - tag number
    '    2 - format
    '         1 - 1 byte - unsigned byte
    '         2 - 1 byte - ascii string
    '         3 - 2 bytes - unsigned short
    '         4 - 4 bytes - unsigned long
    '         5 - 8 bytes - unsigned rational
    '         6 - 1 byte - signed byte
    '         7 - 1 byte - undefined
    '         8 - 2 bytes - signed short
    '         9 - 4 bytes - signed long
    '         10 - 8 bytes - signed rational
    '         11 - 4 bytes - single float
    '         12 - 8 bytes - double float
    '   4 - number of elements
    '   4 - data if 4 bytes or less, else link to data

    '  14  0  //  1  0  3  0  46  0  0  0  92  4  0  0  // 2  0  3  0  4  0  0  0  184  4  0  0  // 3  0  3  0  4  0  0  0  192  4  0  0  // 4  0  3  0  34  0  0  0  200  4  0  0
    '  // 0  0  3  0  6  0  0  0  12  5  0  0  // 0  0  3  0  4  0  0  0  24  5  0  0  // 18  0  3  0  36  0  0  0  32  5  0  0  // 19  0  3  0  4  0  0  0  104  5  0  0  // 6  0
    '  2  0  32  0  0  0  112  5  0  0  // 7  0  2  0  24  0  0  0  144  5  0  0  // 8  0  4  0  1  0  0  0  186  42  49  0  // 9  0  2  0  32  0  0  0  168  5  0  0  // 16  0  4  0
    '  1  0  0  0  0  0  33  1  // 13  0  3  0  34  0  0  0  200  5  0  0  //// 0  0  0  0  //// 92  0  2  0  0  0  5  0  2  0  0  0  0  0  4  0  0  0  1  0  0  0  1  0  0  0
    '  0  0  0  0  0  0  16  0  3  0  1  0  5  32  1  0  255  255  255  255  170  2  227  0  32  0  99  0  192  0  1  0  8  32  0  0  0  0  0  0  0  0  255  255  0  0  224  8  224  8
    '  0  0  1  0  0  0  0  0  255  127  0  0  0  0  0  0  2  0  227  0  30  1  215  0  0  0  0  4  0  0  0  0  68  0  0  0  128  0  195  254  95  0  189  0  0  0  0  0  0  0
    '  0  0  0  0  0  0  0  0  120  0  0  0  0  0  0  0  0  0  1  0  106  0  0  0  99  0  192  0  0  0  0  0  208  255  250  0  0  0  0  0  0  0  0  0  0  0  0  0  26  0
    '  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  9  0  1  0  224  8  168  6  224  8  212  0  153  1  38  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0
    '  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0
    '  73  77  71  58  80  111  119  101  114  83  104  111  116  32  83  52  53  32  74  80  69  71  0  0  0  0  0  0  0  0  0  0  70  105  114  109  119  97  114  101  32  86  101  114  115  105  111  110  32  49
    '  46  48  48  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  68  0  9  0  92  1  1  128  1  128  1  128
    '  1  128  1  128  1  128  1  128  1  128  69  0  0  0  0  0  104  0  0  0  0  0  10  0  224  255  208  255  10  0  133  2  156  253  93  0  80  0  240  3  0  0  0  0  0  0  0  0  0  0
    '  84  0  0  0  235  255

    'File.WriteAllBytes("c:\tmp1.txt", fdata)

    If iintel = 0 Then
      intel = False
    ElseIf iintel = 1 Then
      intel = True
    End If

    i = InStr(make, " ")
    If i <= 0 Then shortMake = LCase(make) Else shortMake = LCase(Left(make, i - 1))

    Select Case shortMake
      Case "pentax"
        If InStr(LCase(model), "k100d") > 0 Or model = "PENTAX *ist D" Then intel = False ' odd.
        vOffset = 6
        readMakernoteIFD(ux, mTag, fdata, mTag.Link + vOffset, iintel, intel, relativeLinks)
        ' end pentax

      Case "ricoh"
        vOffset = 8
        readMakernoteIFD(ux, mTag, fdata, mTag.Link + vOffset, iintel, intel, relativeLinks)

      Case "canon"
        readMakernoteIFD(ux, mTag, fdata, mTag.Link, iintel, intel, 1) ' always use relative links

      Case "nikon"
        ' for i = 0 to 49: print "' " & i * 50 & " -  ";: for j = 0 to 49: print v(i*50 + j);: next j: print: next i
        ' 0 -  78  105  107  111  110  0  2  0  0  0  73  73  42  0  8  0  0  0  21  0  1  0  7  0  4  0  0  0  0  2  0  0  2  0  3  0  2  0  0  0  0  0  0  0  3  0  2  0  6  0
        ' 50 -  0  0  10  1  0  0  4  0  2  0  7  0  0  0  16  1  0  0  5  0  2  0  13  0  0  0  24  1  0  0  6  0  2  0  7  0  0  0  38  1  0  0  7  0  2  0  7  0  0  0
        ' 100 -  46  1  0  0  8  0  2  0  8  0  0  0  54  1  0  0  10  0  5  0  1  0  0  0  62  1  0  0  15  0  2  0  7  0  0  0  70  1  0  0  128  0  2  0  14  0  0  0  78  1
        ' 150 -  0  0  130  0  2  0  13  0  0  0  92  1  0  0  133  0  5  0  1  0  0  0  106  1  0  0  134  0  5  0  1  0  0  0  114  1  0  0  136  0  7  0  4  0  0  0  0  3  0  0
        ' 200 -  143  0  2  0  16  0  0  0  122  1  0  0  148  0  8  0  1  0  0  0  0  0  0  0  149  0  2  0  5  0  0  0  138  1  0  0  16  0  7  0  254  0  0  0  144  1  0  0  17  0
        ' 250 -  4  0  1  0  0  0  142  2  0  0  155  0  1  0  2  0  0  0  0  0  0  0  0  0  0  0  67  79  76  79  82  0  78  79  82  77  65  76  0  0  65  85  84  79  32  32  32  32  32  32
        ' 300 -  32  32  0  0  78  79  78  69  32  32  0  0  65  70  45  67  32  32  0  0  32  32  32  32  32  32  32  0  128  34  0  0  232  3  0  0  65  85  84  79  32  32  0  0  78  79  82  77  65  76
        ' 350 -  32  32  32  32  32  32  32  0  79  70  70  32  32  32  32  32  32  32  32  32  0  0  0  0  0  0  0  0  0  0  100  0  0  0  100  0  0  0  32  32  32  32  32  32  32  32  32  32  32  32
        ' 400 -  32  32  32  0  79  70  70  32  0  0  5  2  0  0  0  0  0  0  0  0  255  1  0  1  0  0  2  172  50  19  0  0  0  0  2  164  0  0  38  204  0  0  4  118  0  0  47  120  0  0
        ' 450 -  47  120  0  0  31  4  22  137  0  39  11  7  1  0  1  140  1  165  1  178  1  237  208  1  5  72  0  0  0  0  47  43  26  0  0  0  0  0  1  0  0  0  0  0  0  0  42  43  0  0
        ' 500 -  61  1  16  130  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  8  236  255  240  103  5  0  0  12  0  5  72  0  23  0  0  0  0  153  153  153  153  25  97  18  49  118  118
        ' 550 -  137  139  28  24  17  24  20  21  21  23  0  0  0  139  0  12  0  1  1  2  96  78  0  2  3  62  0  0  0  15  75  234  5  82  15  192  238  96  16  80  250  95  0  100  0  191  15  19  9  6
        ' 600 -  255  255  255  255  1  2  16  65  0  2  0  190  0  64  0  77  0  0  0  15  0  0  0  0  17  17  17  17  6  90  2  3  0  16  0  0  14  17  19  15  0  0  0  0  0  0  9  223  11  81
        ' 650 -  3  0  14  16  17  16  15  18  17  16  135  101  67  33  7  0  3  1  3  0  1  0  0  0  6  0  0  0  26  1  5  0  1  0  0  0  232  2  0  0  27  1  5  0  1  0  0  0  240  2
        ' 700 -  0  0  40  1  3  0  1  0  0  0  2  0  0  0  1  2  4  0  1  0  0  0  19  52  0  0  2  2  4  0  1  0  0  0  148  66  0  0  19  2  3  0  1  0  0  0  2  0  0  0
        ' 750 -  0  0  0  0  44  1  0  0  1  0  0  0  44  1  0  0  1  0  0  0

        v = mTag.Value

        vOffset = 0
        If uuBound(v) < 80 Then Exit Sub
        If uuBound(v) > 50 AndAlso (v(0) = Asc("N") And v(1) = Asc("i")) Then
          For i = 0 To 50 ' there's an offset for newer cameras
            If v(i) = 73 And v(i + 1) = 73 And v(i + 2) = 42 Then
              intel = True
              vOffset = i + 8
              Exit For
            End If
            If v(i) = 77 And v(i + 1) = 0 And v(i + 2) = 42 Then
              intel = False
              vOffset = i + 7
              Exit For
            End If
          Next i
        End If

        i = word(v, vOffset, intel)
        If i > 0 And i < 200 Then
          If vOffset > 0 Then
            ReDim b(UBound(v))
            k = 0
            For i = vOffset - 8 To UBound(v)
              b(k) = v(i)
              k = k + 1
            Next i
            readMakernoteIFD(ux, mTag, b, 8, iintel, intel, relativeLinks)
          Else
            readMakernoteIFD(ux, mTag, fdata, mTag.Link, iintel, intel, relativeLinks)
          End If
        End If

      Case "minolta"
        ' 0 - 0  12  0  0  0  7  0  0  0  4  77  76  84  48  0  3  0  7  0  0  1  28  0  0  4  70  0  16  0  7  0  0  96  0  0  0  5  98  0  24  0  7  0  0  0  4  0  0  0  0
        ' 50 - 0  32  0  7  0  0  15  248  0  0  101  98  0  64  0  4  0  0  0  1  0  63  235  52  0  136  0  4  0  0  0  1  0  64  117  167  0  137  0  4  0  0  0  1  0  1  65  149  1  0
        ' 100 - 0  4  0  0  0  1  0  0  0  0  1  1  0  4  0  0  0  1  0  0  0  0  1  2  0  4  0  0  0  1  0  0  0  5  1  4  0  10  0  0  0  1  0  0  117  90  0  0  0  0
        ' 150 - 0  0  0  0  0  0  0  0  0  0  0  0  0  128  0  0  0  0  0  0  0  0  0  5  0  0  0  0  0  0  0  0  0  0  0  48  0  0  0  105  0  0  0  42  0  0  0  0  0  0
        ' 200 - 0  0  0  0  0  6  0  0  0  0  0  0  0  0  0  0  0  60  0  0  0  2  0  0  16  86  0  0  7  208  0  0  0  0  7  211  9  11  0  17  15  15  0  0  0  34  0  0  0  0
        ' 250 - 0  0  0  0  0  0  0  0  0  0  0  0  0  0  1  139  0  0  1  0  0  0  1  107  0  0  0  5  0  0  0  5  0  0  0  1  0  0  0  0  0  0  0  9  0  0  0  0  0  0
        ' 300 - 0  6  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  5  0  0  0  0  0  0  0  0  0  0  0  99  0  0  5  0  0  0  3  192  0  0  0  2  0  0  0  0  0  0  0  0
        ' 350 - 0  0  0  2  0  0  0  0  0  0  0  0  0  0  0  2  0  0  0  3  0  0  0  1  0  0  0  105  0  0  0  42  0  0  0  0  0  0  0  0  0  0  0  1  0  0  0  0  0  0
        ' 400 - 0  1  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  1  0  0  0  1  0  0  31  64  0  0  0  16  0  0  0  0  175  175  175  175  0  0  4  240  0  0  0  255  0  0  0  0
        ' 450 - 0  0  0  1  0  0  0  3  0  0  0  1  0  0  0  3  0  0  0  3  0  0  0  0  0  0  4  88  0  0  4  89  0  0  4  89  0  0  0  0  0  0  0  0  0  0  0  0  0  0
        ' 500 - 0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  17  0  0  0  17  0  0  0  17  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0
        ' 550 - 0  0  0  0  0  0  0  0  0  0  0  0  0  0  4  89  0  0  0  1  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  248  0  0  0  0  0  0  0  0  0  0  0  0  0  0
        ' 600 - 1  11  0  0  4  44  0  0  4  24  0  0  3  230  0  0  4  201  0  0  4  221  0  0  4  241  0  0  0  5  0  0  0  22  0  0  0  0  0  0  0  0  0  0  4  219  0  0  0  0
        ' 650 - 0  0  0  0  0  33  234  86  0  27  121  70  0  9  100  150  0  41  136  190  0  39  56  150  0  16  158  106  0  29  213  128  0  30  148  244  0  13  209  92  0  9  184  58  0  7  179  80  0  3
        ' 700 - 80  240  0  7  210  186  0  5  197  146  0  2  159  50  0  8  20  138  0  5  187  20  0  2  119  72  0  12  73  188  0  8  193  106  0  3  155  28  0  15  250  44  0  11  57  54  0  4  48  194
        ' 750 - 0  14  149  48  0  9  241  18  0  3  122  152  0  12  213  222  0  9  57  128  0  3  227  16  0  14  178  100  0  12  79  16  0  7  106  148  0  0  0  0  0  0  0  0  0  0  0  0  0  0
        ' 800 - 0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0
        ' 850 - 0  0  0  0  0  0  0  0  0  0  0  22  0  0  0  0  0  0  0  0  0  0  0  52  0  0  0  0  0  0  0  0  0  0  0  30  0  0  0  0  0  0  0  0  0  0  0  24  0  0
        ' 900 - 0  4  0  0  0  0  0  0  1  160  0  0  1  78  0  0  0  174  0  1  113  21  0  1  114  132  0  0  234  167  0  0  239  15  0  0  251  216  0  1  11  252  0  1  26  30  0  1  39  17
        ' 950 - 0  1  58  2  0  1  107  166  0  1  160  95  0  1  230  7  0  2  48  233  0  2  113  53  0  2  212  115  0  3  55  130  0  3  151  193  0  3  201  125  0  3  185  6  0  3  96  171  0  2
        ' 1000 - 239  234  0  2  122  86  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  1  71  85  0  1  72  41
        ' 1050 - 0  1  79  78  0  1  98  105  0  1  125  92  0  1  147  254  0  1  170  238  0  1  193  166  0  1  249  136  0  2  56  49  0  2  142  70  0  2  242  173  0  3  76  7  0  3  191  174  0  4
        ' 1100 - 66  25  0  4  184  10  0  4  233  16  0  4  202  105  0  4  91  45  0  3  206  148  0  3  63  204  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0
        ' 1150 - 0  0  0  0  0  0  0  0  0  0  0  0  0  1  91  194  0  1  93  243  0  1  100  61  0  1  122  248  0  1  152  61  0  1  175  143  0  1  204  118  0  1  230  243  0  2  32  49  0  2
        ' 1200 - 99  62  0  2  189  70  0  3  30  84  0  3  125  84  0  3  250  65  0  4  128  182  0  4  254  27  0  5  48  77  0  5  12  4  0  4  145  191  0  4  4  45  0  3  114  104  0  0  0  0
        ' for i = 0 to 49: print "' " & i * 50 & " - ";: for j = 0 to 49: print v(i*50+j);: next j: print: next i

        v = mTag.Value
        If v(0) = 0 Then
          intel = False
        ElseIf v(1) = 0 Then
          intel = True
        Else ' too many tags -- error
          Exit Sub
        End If

        If word(v, 0, intel) > 1 And word(v, 0, intel) < 100 And word(v, 4, intel) <= 12 And word(v, 4, intel) > 0 Then 'exif should be there
          readMakernoteIFD(ux, mTag, fdata, mTag.Link, iintel, intel, relativeLinks)
        End If


      Case "olympus"
        v = mTag.Value
        parm = UTF8bare.GetString(v, 0, 14)
        If eqstr(Mid(parm, 1, 5), "olymp") Then ' olympus tags
          If AscW(parm.Chars(5)) = 0 Then
            vOffset = 8
            olyNew = False
            mTag.onlyRead = True
          ElseIf parm.Substring(7, 3) = Chr(0) & "II" Then
            vOffset = 12
            iintel = 2
            olyNew = True
            relativeLinks = 0
          Else
            vOffset = 12
            olyNew = True ' for the 2010, 2020, etc. ifd links
          End If

          readMakernoteIFD(ux, mTag, fdata, mTag.Link + vOffset, iintel, intel, relativeLinks)

          If ux.Tags.Contains(sTag(&H2010)) Then
            tg = ux.Tags.Item(sTag(&H2010))
            k = tg.Link
            uz = New uExif
            If olyNew Then ' newer cameras have the ifd in the 2010 tag value
              b = mTag.Value
              uz.getIFDirectory(b, k, intel, relativeLinks)
            Else ' older cameras have the ifd in the makernote tag
              uz.getIFDirectory(fdata, k, intel, relativeLinks)
            End If
            tg.IFD = uz
          End If ' olympus tag 2010

          If ux.Tags.Contains(sTag(&H2020)) Then
            tg = ux.Tags.Item(sTag(&H2020))
            k = ux.Tags.Item(sTag(&H2020)).link
            uz = New uExif
            If olyNew Then ' newer cameras have the ifd in the 2020 tag value
              b = mTag.Value
              uz.getIFDirectory(b, k, intel, relativeLinks)
            Else ' older cameras have the ifd in the makernote tag
              uz.getIFDirectory(fdata, k, intel, relativeLinks)
            End If
            tg.IFD = uz
          End If ' olympus tag 2020

          If ux.Tags.Contains(sTag(&H2050)) Then
            tg = ux.Tags.Item(sTag(&H2050))
            k = ux.Tags.Item(sTag(&H2050)).link
            uz = New uExif
            If olyNew Then ' newer cameras have the ifd in the 2050 tag value
              b = mTag.Value
              uz.getIFDirectory(b, k, intel, relativeLinks)
            Else ' older cameras have the ifd in the makernote tag
              uz.getIFDirectory(fdata, k, intel, relativeLinks)
            End If
            tg.IFD = uz
          End If ' olympus tag 2050

        End If ' Olympus



      Case "sanyo"
        ' for i = 0 to 49: print "' " & i * 50 & " -  ";: for j = 0 to 49: print v(i*50 + j);: next j: print: next i
        ' 0 -  79  76  89  77  80  0  1  0  68  0  0  2  4  0  3  0  0  0  116  15  0  0  1  2  3  0  1  0  0  0  3  0  0  0  2  2  3  0  1  0  0  0  0  0  0  0  3  2  3  0
        ' 50 -  1  0  0  0  0  0  0  0  4  2  5  0  1  0  0  0  136  15  0  0  5  2  5  0  1  0  0  0  144  15  0  0  6  2  8  0  6  0  0  0  152  15  0  0  7  2  2  0  6  0
        ' 100 -  0  0  164  15  0  0  9  2  7  0  32  0  0  0  170  15  0  0  0  16  10  0  1  0  0  0  204  15  0  0  1  16  10  0  1  0  0  0  212  15  0  0  2  16  10  0  1  0  0  0
        ' 150 -  220  15  0  0  3  16  10  0  1  0  0  0  228  15  0  0  4  16  3  0  1  0  0  0  3  0  0  0  5  16  3  0  2  0  0  0  0  0  0  0  6  16  10  0  1  0  0  0  244  15
        ' 200 -  0  0  9  16  3  0  1  0  0  0  0  0  0  0  10  16  3  0  1  0  0  0  0  0  0  0  11  16  3  0  1  0  0  0  0  0  0  0  12  16  5  0  1  0  0  0  8  16  0  0
        ' 250 -  13  16  3  0  1  0  0  0  1  0  92  1  14  16  3  0  1  0  0  0  92  1  0  0  15  16  3  0  1  0  0  0  0  0  0  0  16  16  3  0  1  0  0  0  0  0  0  0  17  16
        ' 300 -  3  0  9  0  0  0  134  16  0  0  18  16  3  0  4  0  0  0  152  16  0  0  19  16  3  0  1  0  0  0  0  0  0  0  20  16  3  0  1  0  0  0  0  0  2  0  21  16  3  0
        ' 350 -  2  0  0  0  2  0  6  0  22  16  3  0  1  0  0  0  0  0  86  1  23  16  3  0  2  0  0  0  86  1  64  0  24  16  3  0  2  0  0  0  56  1  64  0  26  16  2  0  32  0
        ' 400 -  0  0  44  16  0  0  27  16  4  0  1  0  0  0  0  0  0  0  28  16  4  0  1  0  0  0  0  0  0  0  29  16  4  0  1  0  0  0  0  0  0  0  30  16  4  0  1  0  0  0
        ' 450 -  0  0  0  0  31  16  4  0  1  0  0  0  0  0  0  0  32  16  4  0  1  0  0  0  0  0  0  0  33  16  4  0  1  0  0  0  0  0  0  0  34  16  4  0  1  0  0  0  0  0
        ' 500 -  0  0  35  16  10  0  1  0  0  0  108  16  0  0  36  16  3  0  1  0  0  0  0  0  0  0  37  16  10  0  1  0  0  0  120  16  0  0  38  16  3  0  1  0  0  0  1  0  0  0
        ' 550 -  39  16  3  0  1  0  0  0  0  0  1  0  40  16  3  0  1  0  0  0  1  0  104  1  41  16  3  0  1  0  0  0  1  0  0  2  42  16  3  0  1  0  0  0  0  2  24  0  43  16
        ' 600 -  3  0  6  0  0  0  164  16  0  0  44  16  3  0  2  0  0  0  10  0  0  0  45  16  3  0  1  0  0  0  0  6  0  0  46  16  4  0  1  0  0  0  0  10  0  0  47  16  4  0
        ' 650 -  1  0  0  0  128  7  0  0  48  16  3  0  1  0  0  0  0  0  0  0  49  16  4  0  8  0  0  0  196  16  0  0  51  16  4  0  208  2  0  0  240  16  0  0  56  16  3  0  1  0
        ' 700 -  0  0  0  0  0  0  58  16  3  0  1  0  0  0  0  0  90  1  59  16  3  0  1  0  0  0  90  1  132  1  60  16  3  0  1  0  0  0  132  1  0  0  61  16  10  0  1  0  0  0
        ' 750 -  52  28  0  0  62  16  10  0  1  0  0  0  60  28  0  0  65  16  3  0  1  0  0  0  0  0  0  0  66  16  3  0  1  0  0  0  0  0  0  0  67  16  3  0  1  0  0  0  0  0
        ' 800 -  0  0  68  16  3  0  1  0  0  0  0  0  0  0  69  16  4  0  1  0  0  0  0  0  0  0  0  0  0  0

        v = mTag.Value
        If (v(0) = 83 And v(1) = 65 And v(2) = 78) Then ' Sayno makernote
          vOffset = 8
          readMakernoteIFD(ux, mTag, fdata, mTag.Link + vOffset, iintel, intel, relativeLinks)

          If ux IsNot Nothing AndAlso ux.Tags.Contains(sTag(512)) Then
            i = ux.Tags.Item(sTag(512)).Link
            If (i < 0 Or i > 2000) And relativeLinks = 1 Then ' this is a guess, but the links are probably not sequential so they can't be used in relative mode.
              readMakernoteIFD(ux, mTag, fdata, mTag.Link + vOffset, iintel, intel, 2)
            End If
          End If
        End If

        '=====================================
      Case "fujifilm"

        ' 0 -  70  85  74  73  70  73  76  77  12  0  0  0  21  0  0  0  7  0  4  0  0  0  48  49  51  48  0  16  2  0  8  0  0  0  14  1  0  0  1  16  3  0  1  0  0  0  3  0  0  0
        ' 50 -  2  16  3  0  1  0  0  0  0  0  0  0  3  16  3  0  1  0  0  0  0  0  0  0  16  16  3  0  1  0  0  0  2  0  0  0  17  16  10  0  1  0  0  0  22  1  0  0  32  16
        ' 100 -  3  0  1  0  0  0  0  0  0  0  33  16  3  0  1  0  0  0  0  0  0  0  34  16  3  0  1  0  0  0  1  0  0  0  35  16  3  0  2  0  0  0  120  5  36  4  48  16  3  0
        ' 150 -  1  0  0  0  0  0  0  0  49  16  3  0  1  0  0  0  6  0  0  0  50  16  3  0  1  0  0  0  1  0  0  0  0  17  3  0  1  0  0  0  0  0  0  0  1  17  3  0  1  0
        ' 200 -  0  0  0  0  0  0  0  18  3  0  1  0  0  0  0  0  0  0  16  18  3  0  1  0  0  0  0  0  0  0  0  19  3  0  1  0  0  0  0  0  0  0  1  19  3  0  1  0  0  0
        ' 250 -  0  0  0  0  2  19  3  0  1  0  0  0  0  0  0  0  0  0  0  0  78  79  82  77  65  76  32  0  0  0  0  0  10  0  0  0

        v = mTag.Value
        If v(0) = 70 And v(1) = 85 And v(2) = 74 And v(3) = 73 Then ' fuji makernote

          vOffset = DWord(v, 8, True)

          ReDim b(UBound(v))
          k = 0
          For i = 0 To UBound(v)
            b(k) = v(i)
            k = k + 1
          Next i
          readMakernoteIFD(ux, mTag, b, vOffset, iintel, intel, 1) ' always relative links
        End If

      Case "epson", "seiko"

        ' 0 -  69  80  83  79  78  0  1  0  12  0  0  2  4  0  3  0  0  0  50  4  0  0  1  2  3  0  1  0  0  0  3  0  0  0  2  2  3  0  1  0  0  0  1  0  0  0  3  2  3  0
        ' 50 -  1  0  0  0  0  0  0  0  4  2  5  0  1  0  0  0  62  4  0  0  9  2  7  0  32  0  0  0  70  4  0  0  10  2  7  0  0  2  0  0  102  4  0  0  11  2  4  0  1  0
        ' 100 -  0  0  0  8  0  0  12  2  4  0  1  0  0  0  0  6  0  0  13  2  2  0  6  0  0  0  102  6  0  0  0  3  5  0  1  0  0  0  116  6  0  0  0  15  7  0  174  0  0  0
        ' 150 -  124  6  0  0  0  0  0  0  0  1  0  0  208  0  0  0  0  0  0  0  100  0  0  0  100  0  0  0  69  80  83  79  78  32  68  73  71  73  84  65  76  32  67  65  77  69  82  65  0  0
        ' 200 -  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32
        ' 250 -  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32
        ' 300 -  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32
        ' 350 -  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32
        ' 400 -  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32
        ' 450 -  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32
        ' 500 -  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32
        ' 550 -  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32
        ' 600 -  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32
        ' 650 -  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32
        ' 700 -  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  32  0  83  88  51  50  49  0  0  0  0  0  0  0  0  0  0  0  0  0  10  0  0  0  1  45  0  128  1  72
        ' 750 -  0  0  0  0  255  1  0  1  0  0  17  203  35  120  0  0  0  0  18  163  0  0  36  75  0  0  18  163  0  0  36  75  0  0  0  0  6  11  0  0  17  6  0  20  11  233  1  3  1  86
        ' 800 -  1  102  1  141  1  191  0  0  4  95  17  0  0  0  36  37  6  0  0  0  0  0  0  0  0  0  0  0  0  0  66  35  0  116  71  4  19  148  0  50  48  41  0  32  1  0  20  19  28  10
        ' 850 -  20  13  0  0  100  1  36  87  0  0  0  2  255  240  80  5  0  0  0  1  0  0  0  0  15  3  194  255  141  139  18  34  255  0  83  98  0  252  1  66  32  32  11  0  32  12  74  170  4  95
        ' 900 -  78  32  112  250  236  83  13  17  6  6  0  100  0  160  18  52  67  33

        v = mTag.Value
        If v(0) = 69 And v(1) = 80 And v(2) = 83 And v(3) = 79 And v(4) = 78 Then ' Epson
          vOffset = 8
          readMakernoteIFD(ux, mTag, fdata, mTag.Link + vOffset, iintel, intel, relativeLinks)
        End If

      Case "casio"
        'for i = 0 to 49: print "' " & i * 50 & " -  ";: for j = 0 to 49: print v(i*50 + j);: next j: print: next i
        '  0  -  0  28  0  2  0  3  0  0  0  1  0  3  0  0  0  3  0  3  0  0  0  1  0  7  0  0  0  4  0  3  0  0  0  1  0  4  0  0  0  5  0  3  0  0  0  1  0  13  0  0
        '  50  -  0  6  0  4  0  0  0  1  0  0  3  232  0  7  0  3  0  0  0  1  0  1  0  0  0  8  0  3  0  0  0  1  0  0  0  0  0  9  0  3  0  0  0  1  0  1  0  0  0  10
        '  100  -  0  4  0  0  0  1  0  1  0  0  0  11  0  3  0  0  0  1  0  16  0  0  0  12  0  3  0  0  0  1  0  16  0  0  0  13  0  3  0  0  0  1  0  16  0  0  0  14  0  3
        '  150  -  0  0  0  1  0  0  0  0  0  15  0  3  0  0  0  1  0  0  0  0  0  16  0  3  0  0  0  1  0  0  0  0  0  17  0  4  0  0  0  1  0  115  0  86  0  18  0  3  0  0
        '  200  -  0  1  0  16  0  0  0  19  0  3  0  0  0  1  0  21  0  0  0  21  0  2  0  0  0  18  0  0  3  118  0  22  0  3  0  0  0  1  0  1  0  0  0  23  0  3  0  0  0  1
        '  250  -  0  1  0  0  0  24  0  3  0  0  0  1  0  1  0  0  0  25  0  3  0  0  0  1  0  1  0  0  0  26  0  7  0  0  0  20  0  0  3  136  0  28  0  3  0  0  0  1  0  2
        '  300  -  0  0  0  29  0  3  0  0  0  1  0  1  0  0  0  30  0  3  0  0  0  1  0  5  0  0  14  0  0  7  0  0  0  52  0  0  0  54  0  0  0  0  48  49  48  56  0  0  48  49
        '  350  -  49  56  0  0  51  48  48  48  0  0  0  0  0  16  1  0  0  0  0  0  2  0  0  0  0  0  0  0  0  0  80  114  105  110  116  73  77  0  48  49  48  48  0  0  0  4  0  2  1  0
        '  400  -  0  0  0  14  0  0  0  94  1  0  5  0  0  0  1  1  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0  0

        ' 0 -   81  86  67  0  0  0  0  35  0  2  0  3  0  0  0  2  1  64  0  240  0  3  0  4  0  0  0  1  0  0  116  182  0  4  0  4  0  0  0  1  0  0  4  236  32  0  0  7  0  0
        ' 50 -   116  182  0  0  4  236  32  1  0  2  0  0  0  18  0  0  4  158  32  2  0  2  0  0  0  20  0  0  4  176  32  3  0  7  0  0  0  8  0  0  4  196  32  17  0  3  0  0  0  2
        ' 100 -   0  89  0  75  32  18  0  3  0  0  0  1  0  1  0  0  32  19  0  3  0  0  0  1  0  2  0  0  32  33  0  3  0  0  0  4  0  0  4  204  32  34  0  4  0  0  0  1  0  0
        ' 150 -   39  16  32  35  0  3  0  0  0  1  0  1  0  0  32  49  0  7  0  0  0  2  0  0  0  0  32  50  0  7  0  0  0  2  0  124  0  0  32  51  0  3  0  0  0  1  0  1  0  0
        ' 200 -   32  52  0  3  0  0  0  1  0  0  0  0  48  0  0  3  0  0  0  1  0  2  0  0  48  1  0  3  0  0  0  1  0  1  0  0  48  2  0  3  0  0  0  1  0  2  0  0  48  3
        ' 250 -   0  3  0  0  0  1  0  3  0  0  48  5  0  3  0  0  0  2  0  0  0  1  48  6  0  2  0  0  0  24  0  0  4  212  48  7  0  3  0  0  0  1  0  0  0  0  48  17  0  7

        v = mTag.Value
        If word(v, 0, False) < 200 Then
          vOffset = 0
        ElseIf v(0) = 81 And v(1) = 86 And v(2) = 67 Then  ' qvc
          Exit Sub
        End If

        readMakernoteIFD(ux, mTag, fdata, mTag.Link + vOffset, iintel, False, relativeLinks) ' intel is false even though the rest of the file is true


      Case "kodak", "eastman"

        ux = New uExif ' makernote holder
        mTag.IFD = ux

        k = mTag.Link
        If eqstr(UTF8bare.GetString(fdata, k, 3), "kdk") Then
          k = k + 8
          If word(fdata, k + 16, intel, False) > 2200 Then intel = Not intel ' some kodak cameras have different numeric format than the rest of the exif tags

          i = fdata(k + 9)
          tg = New uTag
          tg.dataType = 1
          tg.tag = 9
          tg.Value = i
          ux.Tags.Add(tg, tg.key)

          i = fdata(k + 10)
          tg = New uTag
          tg.dataType = 1
          tg.tag = 10
          tg.Value = i
          ux.Tags.Add(tg, tg.key)

          i = fdata(k + 11) ' macro mode
          tg = New uTag
          tg.dataType = 1
          tg.tag = 11
          tg.Value = i
          ux.Tags.Add(tg, tg.key)

          ReDim ii(1) ' drawing size
          ii(0) = word(fdata, k + 12, intel, False)
          ii(1) = word(fdata, k + 14, intel, False)
          tg = New uTag
          tg.dataType = 8
          tg.tag = 12
          tg.Value = ii
          ux.Tags.Add(tg, tg.key)

          ' date
          s = Format(fdata(k + 18), "00") & "/" & Format(fdata(k + 19), "00") & "/" & Format(word(fdata, k + 16, intel, False), "0000") ' date
          tg = New uTag
          tg.dataType = 2
          tg.tag = 16
          tg.Value = s
          ux.Tags.Add(tg, tg.key)

          ' time
          s = Format(fdata(k + 20), "00") & ":" & Format(fdata(k + 21), "00") & ":" & Format(fdata(k + 22), "00") & ":" & Format(fdata(k + 23), "###")
          tg = New uTag
          tg.dataType = 2
          tg.tag = 20
          tg.Value = s
          ux.Tags.Add(tg, tg.key)

          ' shutter mode
          i = fdata(k + 27) ' macro mode
          tg = New uTag
          tg.dataType = 1
          tg.tag = 27
          tg.Value = i
          ux.Tags.Add(tg, tg.key)

          ' metering mode
          i = fdata(k + 28) ' macro mode
          tg = New uTag
          tg.dataType = 1
          tg.tag = 28
          tg.Value = i
          ux.Tags.Add(tg, tg.key)

          'Burst Sequence Index
          If fdata(k + 29) > 0 Then
            s = fdata(k + 29)
            tg = New uTag
            tg.dataType = 2
            tg.tag = 29
            tg.Value = s
            ux.Tags.Add(tg, tg.key)
          End If

          'F-Number
          i = word(fdata, k + 30, intel, False)
          If i > 0 Then
            tg = New uTag
            tg.dataType = 8
            tg.tag = 30
            tg.Value = i
            ux.Tags.Add(tg, tg.key)
          End If

          'Exposure Time
          i = DWord(fdata, k + 32, intel, False)
          tg = New uTag
          tg.dataType = 8
          tg.tag = 32
          tg.Value = i
          ux.Tags.Add(tg, tg.key)

          'Exposure Bias
          i = word(fdata, k + 36, intel, False)
          tg = New uTag
          tg.dataType = 8
          tg.tag = 36
          tg.Value = i
          ux.Tags.Add(tg, tg.key)

          ' focus mode
          i = fdata(k + 56)
          tg = New uTag
          tg.dataType = 1
          tg.tag = 56
          tg.Value = i
          ux.Tags.Add(tg, tg.key)

          ' Panorama Mode
          i = word(fdata, k + 60, intel, True)
          tg = New uTag
          tg.dataType = 8
          tg.tag = 60
          tg.Value = i
          ux.Tags.Add(tg, tg.key)

          ' Subject Distance (Inches)
          i = word(fdata, k + 62, intel, False)
          If i > 0 Then
            tg = New uTag
            tg.dataType = 8
            tg.tag = 62
            tg.Value = i
            ux.Tags.Add(tg, tg.key)
          End If

          ' White Balance
          i = fdata(k + 64)
          tg = New uTag
          tg.dataType = 1
          tg.tag = 64
          tg.Value = i
          ux.Tags.Add(tg, tg.key)

          ' Flash Mode
          i = fdata(k + 92)
          tg = New uTag
          tg.dataType = 1
          tg.tag = 92
          tg.Value = i
          ux.Tags.Add(tg, tg.key)

          ' Flash Fired
          i = fdata(k + 93)
          tg = New uTag
          tg.dataType = 1
          tg.tag = 93
          tg.Value = i
          ux.Tags.Add(tg, tg.key)

          ' ISO Setting
          i = word(fdata, k + 94, intel, False)
          tg = New uTag
          tg.dataType = 8
          tg.tag = 94
          tg.Value = i
          ux.Tags.Add(tg, tg.key)

          ' ISO
          i = word(fdata, k + 96, intel, False)
          tg = New uTag
          tg.dataType = 8
          tg.tag = 96
          tg.Value = i
          ux.Tags.Add(tg, tg.key)

          ' Total Zoom
          i = word(fdata, k + 98, intel, False)
          tg = New uTag
          tg.dataType = 8
          tg.tag = 98
          tg.Value = i
          ux.Tags.Add(tg, tg.key)

          ' Date-Time Stamp Mode
          i = word(fdata, k + 100, intel, False)
          tg = New uTag
          tg.dataType = 8
          tg.tag = 100
          tg.Value = i
          ux.Tags.Add(tg, tg.key)

          ' Color Mode
          i = word(fdata, k + 102, intel, False)
          tg = New uTag
          tg.dataType = 8
          tg.tag = 102
          tg.Value = i
          ux.Tags.Add(tg, tg.key)

          ' Digital Zoom Factor
          i = word(fdata, k + 104, intel, False)
          tg = New uTag
          tg.dataType = 8
          tg.tag = 104
          tg.Value = i
          ux.Tags.Add(tg, tg.key)

          ' Sharpness
          i = fdata(k + 107)
          tg = New uTag
          tg.dataType = 1
          tg.tag = 107
          tg.Value = i
          ux.Tags.Add(tg, tg.key)
        End If ' kodak


      Case "panasonic"
        ' for i = 0 to 49: print "' " & i * 50 & " -  ";: for j = 0 to 49: print v(i*50 + j);: next j: print: next i
        ' 0 -   80  97  110  97  115  111  110  105  99  0  0  0  24  0  1  0  3  0  1  0  0  0  2  0  0  0  2  0  7  0  4  0  0  0  0  1  0  18  3  0  3  0  1  0  0  0  1  0  0  0
        ' 50 -   7  0  3  0  1  0  0  0  1  0  0  0  15  0  1  0  2  0  0  0  16  0  0  0  26  0  3  0  1  0  0  0  2  0  0  0  28  0  3  0  1  0  0  0  2  0  0  0  31  0
        ' 100 -   3  0  1  0  0  0  6  0  0  0  32  0  3  0  1  0  0  0  2  0  0  0  33  0  7  0  30  21  0  0  198  4  0  0  34  0  3  0  1  0  0  0  0  0  0  0  35  0  3  0
        ' 150 -   1  0  0  0  0  0  0  0  36  0  3  0  1  0  0  0  0  0  0  0  37  0  7  0  16  0  0  0  228  25  0  0  38  0  7  0  4  0  0  0  48  49  48  48  39  0  3  0  1  0
        ' 200 -   0  0  0  0  0  0  40  0  3  0  1  0  0  0  1  0  0  0  41  0  4  0  1  0  0  0  72  26  0  0  42  0  3  0  1  0  0  0  0  0  0  0  43  0  4  0  1  0  0  0
        ' 250 -   0  0  0  0  44  0  3  0  1  0  0  0  0  0  0  0  45  0  3  0  1  0  0  0  0  0  0  0  46  0  3  0  1  0  0  0  1  0  0  0  47  0  3  0  1  0  0  0  2  0
        ' 300 -   0  0
        vOffset = 12
        readMakernoteIFD(ux, mTag, fdata, mTag.Link + vOffset, iintel, True, relativeLinks) ' intel is true even if the rest of the file is false

      Case "sony"
        vOffset = 12
        ''vOffset = 40
        i = getWord(fdata, 0, False)
        If i = &H4949 Then relativeLinks = 0 Else relativeLinks = 3 ' 3 if saved by Windows
        iintel = 2
        readMakernoteIFD(ux, mTag, fdata, mTag.Link + vOffset, iintel, intel, relativeLinks)

    End Select

  End Sub

  Function word(ByRef v As Object, ByVal i As Integer, ByRef intel As Boolean, Optional ByRef dSigned As Boolean = False) As Integer

    If intel Then
      word = v(i) + v(i + 1) * 256
    Else
      word = v(i + 1) + v(i) * 256
    End If
    If dSigned AndAlso word >= 32768 Then word = word - 65536

  End Function

  Function DWord(ByRef v As Object, ByVal i As Integer, ByRef intel As Boolean, Optional ByRef dSigned As Boolean = False) As Integer

    Dim x As Double

    If intel Then
      x = v(i) + v(i + 1) * 256 + v(i + 2) * 65536 + v(i + 3) * 65535 * 256
    Else
      x = v(i + 3) + v(i + 2) * 256 + v(i + 1) * 65536 + v(i) * 65535 * 256
    End If
    If x > 2 ^ 31 Then x = x - 2 ^ 32 ' always signed (prevents overflows)
    DWord = x

  End Function

  Sub infoMatrix(ByRef v As Object, ByRef parm As String)

    Dim j, k As Integer
    Dim i1, i2 As Integer
    Dim s As String
    Dim nCols, nrows As Integer
    Dim x As Double

    parm = ""

    nCols = v(0) + v(1) * 256
    nrows = v(2) + v(3) * 256
    k = 3
    parm = parm & tb

    For i2 = 1 To nCols
      k = k + 1
      s = ""
      Do While v(k) <> 0 And k < UBound(v)
        s = s & ChrW(v(k))
        k = k + 1
      Loop
      parm = parm & s
      If i2 <> nCols Then parm = parm & ",  "
    Next i2
    parm = parm & "\par "

    k = k + 1 ' add the data
    If k + nrows * nCols * 8 <= UBound(v) Then
      For j = 1 To nrows
        parm = parm & tb
        For i2 = 1 To nCols
          i1 = DWord(v, k + 4, True)
          If i1 <> 0 Then x = DWord(v, k, True) / i1 Else x = 0
          parm = parm & Format(x, "##,###0.0#")
          If i2 <> nCols Then parm = parm & ",  "
          k = k + 8
        Next i2
        parm = parm & "\par "
      Next j
    End If

  End Sub

  Public Function sTag(ByVal tag As Integer) As String
    sTag = Right("0000" & Hex(tag), 4)
  End Function

  Function splitString(ByRef s As String) As String

    ' replace chrw(0) with "\par " in strings, except for the last character
    ''If s Is Nothing OrElse s = "" Then Return ""

    splitString = s.Replace(ChrW(0), "\par " & tb)

    Do While Right(splitString, 6) = "\par " & tb
      splitString = Left(splitString, Len(splitString) - 6)
      If Len(splitString) < 6 Then Exit Do
    Loop
    splitString = Trim(splitString)

  End Function

  Function uuBound(ByRef v As Object) As Integer
    ' get the upper bound of v, or -1 if v is no array
    If IsArray(v) Then Return UBound(v) Else Return -1
  End Function

  Function dumpTags(ByRef Tags As Collection) As String

    Dim ut As uTag
    Dim s As String = ""
    Dim v As Object
    Dim i, n As Integer

    For Each ut In Tags

      s = s & sTag(ut.tag) & tb
      v = ut.Value
      n = uuBound(v)
      If n < 0 Then
        If Not IsArray(v) Then s = s & v
      Else
        If n > 500 Then n = 500
        For i = 0 To n
          s = s & v(i) & " "
        Next i
      End If
      If uuBound(v) > 200 Then s = s & "... (" & uuBound(v) & " total bytes)"
      s = s & "\par "
      s = s.Replace(ChrW(0), " ")
    Next ut

    dumpTags = s

  End Function

  Private Sub tagVerify(ByRef Tags As Collection)
    ' this works for the main or exif tags
    ' checks and corrects the array (or not) of values
    Dim uValue As Object
    Dim tg As uTag
    Dim v As Object
    Dim tagName As String = ""
    Dim tagDataType As Integer
    Dim tagCount As Integer

    For Each tg In Tags
      v = tg.Value

      TagInfo(tg.tag, tagName, tagDataType, tagCount)
      If tagCount = 1 Or tagDataType = 2 Then ' single value, no array
        If IsArray(v) Then
          If UBound(v) >= 0 Then tg.Value = v(0) Else tg.Value = Nothing
        End If

      ElseIf tagCount = 0 Then
        If Not IsArray(v) Then ' any number of elements
          ReDim uValue(0)
          uValue(0) = v
          tg.Value = uValue
        End If

      Else
        If Not IsArray(v) Then ' fixed count
          ReDim uValue(tagCount - 1)
          uValue(0) = v
          For i As Integer = 1 To UBound(uValue) : uValue(i) = 0 : Next i
        End If
      End If

    Next tg

  End Sub

  Sub TagInfo(tag As Integer, ByRef tagname As String, ByRef tagdatatype As Integer, ByRef tagdatacount As Integer)

    Select Case tag
      Case &H0
        tagname = "GPS tag version"
        tagdatatype = 1
        tagdatacount = 4
      Case &H1
        tagname = "North or South Latitude"
        tagdatatype = 2
        tagdatacount = 2
      Case &H10
        tagname = "Reference for direction of image"
        tagdatatype = 2
        tagdatacount = 2
      Case &H100
        tagname = "Image width"
        tagdatatype = 3
        tagdatacount = 1
      Case &H101
        tagname = "Image height"
        tagdatatype = 3
        tagdatacount = 1
      Case &H102
        tagname = "Number of bits per component"
        tagdatatype = 3
        tagdatacount = 1
      Case &H103
        tagname = "Compression scheme"
        tagdatatype = 3
        tagdatacount = 1
      Case &H106
        tagname = "Pixel composition"
        tagdatatype = 3
        tagdatacount = 1
      Case &H10E
        tagname = "Image description"
        tagdatatype = 2
        tagdatacount = 0
      Case &H10F
        tagname = "Equipment manufacturer"
        tagdatatype = 2
        tagdatacount = 0
      Case &H11
        tagname = "Direction of image"
        tagdatatype = 5
        tagdatacount = 1
      Case &H110
        tagname = "Equipment model"
        tagdatatype = 2
        tagdatacount = 0
      Case &H111
        tagname = "Image data location"
        tagdatatype = 3
        tagdatacount = 0
      Case &H112
        tagname = "Orientation of image"
        tagdatatype = 3
        tagdatacount = 1
      Case &H115
        tagname = "Number of components"
        tagdatatype = 3
        tagdatacount = 1
      Case &H116
        tagname = "Number of rows per strip"
        tagdatatype = 3
        tagdatacount = 1
      Case &H117
        tagname = "Bytes per compressed strip"
        tagdatatype = 3
        tagdatacount = 0
      Case &H11A
        tagname = "Horizontal Resolution"
        tagdatatype = 5
        tagdatacount = 1
      Case &H11B
        tagname = "Vertical resolution"
        tagdatatype = 5
        tagdatacount = 1
      Case &H11C
        tagname = "Image data arrangement"
        tagdatatype = 3
        tagdatacount = 1
      Case &H12
        tagname = "Geodetic survey data used"
        tagdatatype = 2
        tagdatacount = 0
      Case &H128
        tagname = "Unit of X and Y resolution"
        tagdatatype = 3
        tagdatacount = 1
      Case &H12D
        tagname = "Transfer function"
        tagdatatype = 3
        tagdatacount = 768
      Case &H13
        tagname = "Reference for latitude of destination"
        tagdatatype = 2
        tagdatacount = 2
      Case &H131
        tagname = "Software used"
        tagdatatype = 2
        tagdatacount = 0
      Case &H132
        tagname = "File change date and time"
        tagdatatype = 2
        tagdatacount = 20
      Case &H13B
        tagname = "Image creator"
        tagdatatype = 2
        tagdatacount = 0
      Case &H13E
        tagname = "White point chromaticity"
        tagdatatype = 5
        tagdatacount = 2
      Case &H13F
        tagname = "Chromaticities of primaries"
        tagdatatype = 5
        tagdatacount = 6
      Case &H14
        tagname = "Latitude of destination"
        tagdatatype = 5
        tagdatacount = 3
      Case &H15
        tagname = "Reference for longitude of destination"
        tagdatatype = 2
        tagdatacount = 2
      Case &H16
        tagname = "Longitude of destination"
        tagdatatype = 5
        tagdatacount = 3
      Case &H17
        tagname = "Reference for bearing of destination"
        tagdatatype = 2
        tagdatacount = 2
      Case &H18
        tagname = "Bearing of destination"
        tagdatatype = 5
        tagdatacount = 1
      Case &H19
        tagname = "Reference for distance to destination"
        tagdatatype = 2
        tagdatacount = 2
      Case &H1A
        tagname = "Distance to destination"
        tagdatatype = 5
        tagdatacount = 1
      Case &H1B
        tagname = "Name of GPS processing method"
        tagdatatype = 7
        tagdatacount = 0
      Case &H1C
        tagname = "Name of GPS area"
        tagdatatype = 7
        tagdatacount = 0
      Case &H1D
        tagname = "GPS date"
        tagdatatype = 2
        tagdatacount = 11
      Case &H1E
        tagname = "GPS differential correction"
        tagdatatype = 3
        tagdatacount = 1
      Case &H1F
        tagname = "Horizontal positioning error"
        tagdatatype = 5
        tagdatacount = 1
      Case &H2
        tagname = "Latitude"
        tagdatatype = 5
        tagdatacount = 3
      Case &H201
        tagname = "Offset to JPEG SOI"
        tagdatatype = 4
        tagdatacount = 1
      Case &H202
        tagname = "Bytes of JPEG data"
        tagdatatype = 4
        tagdatacount = 1
      Case &H211
        tagname = "Color space transformation matrix coefficients"
        tagdatatype = 5
        tagdatacount = 3
      Case &H212
        tagname = "Subsampling ratio of Y to C"
        tagdatatype = 3
        tagdatacount = 2
      Case &H213
        tagname = "Y and C positioning"
        tagdatatype = 3
        tagdatacount = 1
      Case &H214
        tagname = "Pair of black and white reference values"
        tagdatatype = 5
        tagdatacount = 6
      Case &H3
        tagname = "East or West Longitude"
        tagdatatype = 2
        tagdatacount = 2
      Case &H4
        tagname = "Longitude"
        tagdatatype = 5
        tagdatacount = 3
      Case &H5
        tagname = "Altitude reference"
        tagdatatype = 1
        tagdatacount = 1
      Case &H6
        tagname = "Altitude"
        tagdatatype = 5
        tagdatacount = 1
      Case &H7
        tagname = "GPS time (atomic clock)"
        tagdatatype = 5
        tagdatacount = 3
      Case &H8
        tagname = "GPS satellites used for measurement"
        tagdatatype = 2
        tagdatacount = 0
      Case &H8298
        tagname = "Copyright holder"
        tagdatatype = 2
        tagdatacount = 0
      Case &H829A
        tagname = "Exposure time"
        tagdatatype = 5
        tagdatacount = 1
      Case &H829D
        tagname = "F number"
        tagdatatype = 5
        tagdatacount = 1
      Case &H8769
        tagname = "Pointer to Exif Data"
        tagdatatype = 4
        tagdatacount = 1
      Case &H8822
        tagname = "Exposure program"
        tagdatatype = 3
        tagdatacount = 1
      Case &H8824
        tagname = "Spectral sensitivity"
        tagdatatype = 2
        tagdatacount = 0
      Case &H8825
        tagname = "Pointer to GPS Data"
        tagdatatype = 4
        tagdatacount = 1
      Case &H8827
        tagname = "ISO speed rating"
        tagdatatype = 3
        tagdatacount = 0
      Case &H8828
        tagname = "Optoelectric conversion factor"
        tagdatatype = 7
        tagdatacount = 0
      Case &H8830
        tagname = "Sensitivity Type"
        tagdatatype = 3
        tagdatacount = 1
      Case &H8831
        tagname = "Standard Output Sensitivity"
        tagdatatype = 4
        tagdatacount = 1
      Case &H8832
        tagname = "Recommended Exposure Index"
        tagdatatype = 4
        tagdatacount = 1
      Case &H8833
        tagname = "ISO Speed"
        tagdatatype = 4
        tagdatacount = 1
      Case &H8834
        tagname = "ISO Speed Latitude yyy"
        tagdatatype = 4
        tagdatacount = 1
      Case &H8835
        tagname = "ISO Speed Latitude zzz"
        tagdatatype = 4
        tagdatacount = 1
      Case &H9
        tagname = "GPS receiver status"
        tagdatatype = 2
        tagdatacount = 2
      Case &H9000
        tagname = "exif version"
        tagdatatype = 7
        tagdatacount = 4
      Case &H9003
        tagname = "Date and time of original data"
        tagdatatype = 2
        tagdatacount = 20
      Case &H9004
        tagname = "Date and time of digital data"
        tagdatatype = 2
        tagdatacount = 20
      Case &H9101
        tagname = "Meaning of each component"
        tagdatatype = 7
        tagdatacount = 4
      Case &H9102
        tagname = "Image compression mode"
        tagdatatype = 5
        tagdatacount = 1
      Case &H9201
        tagname = "Shutter speed"
        tagdatatype = 10
        tagdatacount = 1
      Case &H9202
        tagname = "Aperture"
        tagdatatype = 5
        tagdatacount = 1
      Case &H9203
        tagname = "Brightness"
        tagdatatype = 10
        tagdatacount = 1
      Case &H9204
        tagname = "Exposure bias"
        tagdatatype = 10
        tagdatacount = 1
      Case &H9205
        tagname = "Maximum lens aperture"
        tagdatatype = 5
        tagdatacount = 1
      Case &H9206
        tagname = "Subject distance"
        tagdatatype = 5
        tagdatacount = 1
      Case &H9207
        tagname = "Metering mode"
        tagdatatype = 3
        tagdatacount = 1
      Case &H9208
        tagname = "Light source"
        tagdatatype = 3
        tagdatacount = 1
      Case &H9209
        tagname = "Flash"
        tagdatatype = 3
        tagdatacount = 1
      Case &H920A
        tagname = "Lens focal length"
        tagdatatype = 5
        tagdatacount = 1
      Case &H9214
        tagname = "Subject area"
        tagdatatype = 3
        tagdatacount = 0
      Case &H927C
        tagname = "Manufacturer notes"
        tagdatatype = 7
        tagdatacount = 0
      Case &H9286
        tagname = "User comments"
        tagdatatype = 7
        tagdatacount = 0
      Case &H9290
        tagname = "Time subseconds"
        tagdatatype = 2
        tagdatacount = 0
      Case &H9291
        tagname = "Original time subseconds"
        tagdatatype = 2
        tagdatacount = 0
      Case &H9292
        tagname = "Time digitized subseconds"
        tagdatatype = 2
        tagdatacount = 0
      Case &H9C9B
        tagname = "Windows title field"
        tagdatatype = 2
        tagdatacount = 0
      Case &H9C9C
        tagname = "Windows comment field"
        tagdatatype = 2
        tagdatacount = 0
      Case &H9C9D
        tagname = "Windows author field"
        tagdatatype = 2
        tagdatacount = 0
      Case &H9C9E
        tagname = "Windows keywords field"
        tagdatatype = 2
        tagdatacount = 0
      Case &H9C9F
        tagname = "Windows subject field"
        tagdatatype = 2
        tagdatacount = 0
      Case &HA
        tagname = "GPS measurement mode"
        tagdatatype = 2
        tagdatacount = 2
      Case &HA000
        tagname = "Supported Flashpix version"
        tagdatatype = 7
        tagdatacount = 4
      Case &HA001
        tagname = "Color space information"
        tagdatatype = 3
        tagdatacount = 1
      Case &HA002
        tagname = "Valid image width"
        tagdatatype = 3
        tagdatacount = 1
      Case &HA003
        tagname = "Valid image height"
        tagdatatype = 3
        tagdatacount = 1
      Case &HA004
        tagname = "Related audio file"
        tagdatatype = 2
        tagdatacount = 13
      Case &HA005
        tagname = "Pointer to Interop Data"
        tagdatatype = 4
        tagdatacount = 1
      Case &HA20B
        tagname = "Flash energy"
        tagdatatype = 5
        tagdatacount = 1
      Case &HA20C
        tagname = "Spatial frequency response"
        tagdatatype = 7
        tagdatacount = 0
      Case &HA20E
        tagname = "Focal plane X resolution"
        tagdatatype = 5
        tagdatacount = 1
      Case &HA20F
        tagname = "Focal plane Y resolution"
        tagdatatype = 5
        tagdatacount = 1
      Case &HA210
        tagname = "Focal plane resolution unit"
        tagdatatype = 3
        tagdatacount = 1
      Case &HA214
        tagname = "Subject location"
        tagdatatype = 3
        tagdatacount = 2
      Case &HA215
        tagname = "Exposure index"
        tagdatatype = 5
        tagdatacount = 1
      Case &HA217
        tagname = "Sensing method"
        tagdatatype = 3
        tagdatacount = 1
      Case &HA300
        tagname = "File source"
        tagdatatype = 7
        tagdatacount = 1
      Case &HA301
        tagname = "Scene type"
        tagdatatype = 7
        tagdatacount = 1
      Case &HA302
        tagname = "CFA pattern"
        tagdatatype = 7
        tagdatacount = 0
      Case &HA401
        tagname = "Custom image processing"
        tagdatatype = 3
        tagdatacount = 1
      Case &HA402
        tagname = "Exposure mode"
        tagdatatype = 3
        tagdatacount = 1
      Case &HA403
        tagname = "White balance"
        tagdatatype = 3
        tagdatacount = 1
      Case &HA404
        tagname = "Digital zoom ratio"
        tagdatatype = 5
        tagdatacount = 1
      Case &HA405
        tagname = "Focal length in 35 mm film"
        tagdatatype = 3
        tagdatacount = 1
      Case &HA406
        tagname = "Scene capture type"
        tagdatatype = 3
        tagdatacount = 1
      Case &HA407
        tagname = "Gain control"
        tagdatatype = 3
        tagdatacount = 1
      Case &HA408
        tagname = "Contrast"
        tagdatatype = 3
        tagdatacount = 1
      Case &HA409
        tagname = "Saturation"
        tagdatatype = 3
        tagdatacount = 1
      Case &HA40A
        tagname = "Sharpness"
        tagdatatype = 3
        tagdatacount = 1
      Case &HA40B
        tagname = "Device settings description"
        tagdatatype = 7
        tagdatacount = 0
      Case &HA40C
        tagname = "Subject distance range"
        tagdatatype = 3
        tagdatacount = 1
      Case &HA420
        tagname = "Unique image ID"
        tagdatatype = 2
        tagdatacount = 33
      Case &HA430
        tagname = "Camera owner name"
        tagdatatype = 2
        tagdatacount = 33
      Case &HA431
        tagname = "Body serial number"
        tagdatatype = 2
        tagdatacount = 33
      Case &HA432
        tagname = "Lens specification"
        tagdatatype = 5
        tagdatacount = 33
      Case &HA433
        tagname = "Lens make"
        tagdatatype = 2
        tagdatacount = 33
      Case &HA434
        tagname = "Lens model"
        tagdatatype = 2
        tagdatacount = 33
      Case &HA435
        tagname = "Lens serial number"
        tagdatatype = 2
        tagdatacount = 33
      Case &HB
        tagname = "Measurement precision"
        tagdatatype = 5
        tagdatacount = 1
      Case &HC
        tagname = "Speed unit"
        tagdatatype = 2
        tagdatacount = 2
      Case &HD
        tagname = "Speed of GPS receiver"
        tagdatatype = 5
        tagdatacount = 1
      Case &HE
        tagname = "Reference for direction of movement"
        tagdatatype = 2
        tagdatacount = 2
      Case &HF
        tagname = "Direction of movement"
        tagdatatype = 5
        tagdatacount = 1
      Case 120
        tagname = "IPTC Caption"
        tagdatatype = 2
        tagdatacount = 0
      Case Else
        tagname = "Nonstandard Tag"
        tagdatatype = 7
        tagdatacount = 0

    End Select

  End Sub

  ' these are missing from lead comments. Some not listed here are in codecs.imaginfo instead
  '  bitspersample = &H102
  '  ComponentsConfiguration = &H9101
  '  CompressedBitsPerPixel = &H9102
  '  Compression = &H103
  '  exifpointer = &H8769
  '  gpspointer = &H8825
  '  InteropIndex = &H1
  '  interopField = &H2
  '  interoppointer = &HA005
  '  JPEGInterchangeFormat = &H201
  '  JPEGInterchangeFormatLength = &H202
  '  Orientation = &H112
  '  PhotometricInterpretation = &H106
  '  PixelXDimension = &HA002
  '  PixelYDimension = &HA003
  '  PlanarConfiguration = &H11C
  '  PrimaryChromaticities = &H13F
  '  ReferenceBlackWhite = &H214
  '  ResolutionUnit = &H128
  '  RowsPerStrip = &H116
  '  SamplesPerPixel = &H115
  '  StripByteCounts = &H117
  '  StripOffsets = &H111
  '  TransferFunction = &H12D
  '  WhitePoint = &H13E
  '  XPAuthor = &H9C9D
  '  XPComment = &H9C9C
  '  XPKeywords = &H9C9E
  '  XPSubject = &H9C9F
  '  XPTitle = &H9C9B
  '  YCbCrCoefficients = &H211
  '  YCbCrPositioning = &H213
  '  YCbCrSubSampling = &H212

  Sub pentaxMakernote(ByRef makertags As Collection, ByRef note As String)

    Dim i As Integer
    Dim x As Double
    Dim parm As String = ""
    Dim v As Object
    Dim pentaxCity(70) As String

    If makertags.Contains(sTag(0)) Then
      v = makertags.Item(sTag(0)).Value
      If IsArray(v) AndAlso UBound(v) = 3 Then parm = v(0) & v(1) & v(2) & v(3) Else parm = ""
      If Trim(parm) <> "" Then note = note & "Pentax Version:" & tb & parm & "\par "
    End If

    If makertags.Contains(sTag(1)) Then
      i = makertags.Item(sTag(1)).singleValue
      If i > 0 Then note = note & "Pentax Model Type:" & tb & i & "\par "
    End If

    If makertags.Contains(sTag(2)) Then
      v = makertags.Item(sTag(2)).Value
      If IsArray(v) AndAlso UBound(v) >= 1 Then parm = v(0) & " x " & v(1) Else parm = ""
      If Trim(parm) <> "" Then note = note & "Preview Image Size:" & tb & parm & "\par "
    End If

    If makertags.Contains(sTag(5)) Then
      i = makertags.Item(sTag(5)).singleValue
      note = note & "Pentax Model ID:" & tb & i & " (" & Format(i, "X") & ")\par "
    End If

    If makertags.Contains(sTag(6)) Then
      v = makertags.Item(sTag(6)).Value
      parm = Format(v(2), "00") & "/" & Format(v(3), "00") & "/" & Format(v(1) + v(0) * 256, "0000") ' date
      If Trim(parm) <> "" Then note = note & "Date:" & tb & parm & "\par "
    End If

    If makertags.Contains(sTag(7)) Then
      v = makertags.Item(sTag(7)).Value
      parm = Format(v(0), "00") & ":" & Format(v(1), "00") & ":" & Format(v(2), "00") ' time
      If Trim(parm) <> "" Then note = note & "Time:" & tb & parm & "\par "
    End If

    If makertags.Contains(sTag(8)) Then
      i = makertags.Item(sTag(8)).singleValue
      Select Case i
        Case 0
          parm = "Good"
        Case 1
          parm = "Better"
        Case 2
          parm = "Best"
        Case 3
          parm = "TIFF"
        Case 4
          parm = "Raw"
        Case 5
          parm = "Premium"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Quality:" & tb & parm & "\par "
    End If

    If makertags.Contains(sTag(9)) Then
      i = makertags.Item(sTag(9)).singleValue
      note = note & "Pentax Image Size Code:" & tb & i & "\par "
    End If

    If makertags.Contains(sTag(11)) Then
      i = makertags.Item(sTag(11)).singleValue
      Select Case i
        Case 0 : parm = "Program"
        Case 1 : parm = "Shutter Speed Priority"
        Case 2 : parm = "Program AE"
        Case 3 : parm = "Manual"
        Case 5 : parm = "Portrait"
        Case 6 : parm = "Landscape"
        Case 8 : parm = "Sport"
        Case 9 : parm = "Night Scene"
        Case 11 : parm = "Soft"
        Case 12 : parm = "Surf & Snow"
        Case 13 : parm = "Candlelight"
        Case 14 : parm = "Autumn"
        Case 15 : parm = "Macro"
        Case 17 : parm = "Fireworks"
        Case 18 : parm = "Text"
        Case 19 : parm = "Panorama"
        Case 30 : parm = "Self Portrait"
        Case 31 : parm = "Illustrations"
        Case 33 : parm = "Digital Filter"
        Case 37 : parm = "Museum"
        Case 38 : parm = "Food"
        Case 40 : parm = "Green Mode"
        Case 49 : parm = "Light Pet"
        Case 50 : parm = "Dark Pet"
        Case 51 : parm = "Medium Pet"
        Case 53 : parm = "Underwater"
        Case 54 : parm = "Candlelight"
        Case 55 : parm = "Natural Skin Tone"
        Case 56 : parm = "Synchro Sound Record"
        Case 58 : parm = "Frame Composite"
        Case 60 : parm = "Kids"
        Case 61 : parm = "Blur Reduction"
        Case 255 : parm = "Digital Filter"
        Case Else : parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Picture Mode:" & tb & parm & "\par "
    End If

    If makertags.Contains(sTag(12)) Then
      v = makertags.Item(sTag(12)).Value
      If IsArray(v) AndAlso UBound(v) >= 1 Then
        Select Case v(0)
          Case 0 : parm = "Auto, Did not fire"
          Case 1 : parm = "Off"
          Case 2 : parm = "On, Did not fire"
          Case 3 : parm = "Auto, Did not fire, Red-eye reduction"
          Case &H100 : parm = "Auto, Fired"
          Case &H102 : parm = "On"
          Case &H103 : parm = "Auto, Fired, Red-eye reduction"
          Case &H104 : parm = "On, Red-eye reduction"
          Case &H105 : parm = "On, Wireless (Master)"
          Case &H106 : parm = "On, Wireless (Control)"
          Case &H108 : parm = "On, Soft"
          Case &H109 : parm = "On, Slow-sync"
          Case &H10A : parm = "On, Slow-sync, Red-eye reduction"
          Case &H10B : parm = "On, Trailing-curtain Sync"
          Case Else : parm = ""
        End Select

        Select Case v(1)
          Case &H3F : parm = parm & ", Internal"
          Case &H100 : parm = parm & ", External Auto "
          Case &H23F : parm = parm & ", External, Flash Problem"
          Case &H300 : parm = parm & ", External Manual "
          Case &H304 : parm = parm & ", External P-TTL Auto "
          Case &H305 : parm = parm & ", External Contrast-control Sync "
          Case &H306 : parm = parm & ", External High-speed Sync "
          Case &H30C : parm = parm & ", External Wireless "
          Case &H30D : parm = parm & ", External Wireless High-speed Sync"
        End Select
        If Len(parm) > 0 Then note = note & "Flash Mode:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(13)) Then
        i = makertags.Item(sTag(13)).singleValue
        Select Case i
          Case 0 : parm = "Normal"
          Case 1 : parm = "Macro"
          Case 2 : parm = "Infinity"
          Case 3 : parm = "Manual"
          Case 4 : parm = "Super Macro"
          Case 5 : parm = "Pan"
          Case 6 : parm = "Premium"
          Case 16 : parm = "AF-S"
          Case 17 : parm = "AF-C"
          Case 18 : parm = "AF-A"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Focus Mode:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(14)) Then
        i = makertags.Item(sTag(14)).singleValue
        Select Case i
          Case 1 : parm = "Upper-left"
          Case 2 : parm = "Top"
          Case 3 : parm = "Upper-right"
          Case 4 : parm = "Left"
          Case 5 : parm = "Mid-left"
          Case 6 : parm = "Center"
          Case 7 : parm = "Mid-right"
          Case 8 : parm = "Right"
          Case 9 : parm = "Lower-left"
          Case 10 : parm = "Bottom"
          Case 11 : parm = "Lower-right"
          Case 65532 : parm = "Face Recognition AF"
          Case 65533 : parm = "Automatic Tracking AF"
          Case 65534 : parm = "Fixed Center"
          Case 65535 : parm = "Auto"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "AF Point Selected:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(15)) Then
        i = makertags.Item(sTag(15)).singleValue
        parm = ""
        If i And 1 Then parm = parm & " Fixed Center or Multiple"
        If i And 2 Then parm = parm & " top-left"
        If i And 4 Then parm = parm & " top-center"
        If i And 8 Then parm = parm & " top-right"
        If i And 16 Then parm = parm & " mid-left"
        If i And 32 Then parm = parm & " mid-center"
        If i And 64 Then parm = parm & " mid-right"
        If i And 128 Then parm = parm & " bottom-left"
        If i And 256 Then parm = parm & " bottom-center"
        If i And 512 Then parm = parm & " bottom-right"
        If i = 65535 Then parm = "(none)"
        If Len(parm) > 0 Then note = note & "AF Points in Focus:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(16)) Then
        i = makertags.Item(sTag(16)).singleValue
        note = note & "Focus Position:" & tb & i & "\par "
      End If

      If makertags.Contains(sTag(18)) Then
        x = uz.TagValue(uExif.TagID.exposuretime, 0) / 100000
        If x > 0.00000000001 Then
          If x < 1 And x <> 0 Then
            note = note & "Exposure Time:" & tb & Format(x, "##0.0###") & " (1/" & Format(1 / x, "##,###") & ") sec." & "\par "
          ElseIf x <> 0 Then
            note = note & "Exposure Time:" & tb & Format(x, "##0.0") & " sec." & "\par "
          End If
        End If
      End If

      If makertags.Contains(sTag(19)) Then
        i = makertags.Item(sTag(19)).singleValue
        note = note & "F-Number:" & tb & i / 10 & "\par "
      End If

      If makertags.Contains(sTag(20)) Then
        i = makertags.Item(sTag(20)).singleValue
        note = note & "ISO Code:" & tb & i & "\par "
      End If

      If makertags.Contains(sTag(21)) Then
        i = makertags.Item(sTag(21)).singleValue
        note = note & "Light Reading:" & tb & i & "\par "
      End If

      If makertags.Contains(sTag(22)) Then
        i = makertags.Item(sTag(22)).singleValue
        note = note & "Exposure Compensation:" & tb & Format(i / 10 - 5, "##0.#") & "\par "
      End If

      If makertags.Contains(sTag(23)) Then
        i = makertags.Item(sTag(23)).singleValue
        Select Case i
          Case 0
            parm = "Multi-segment "
          Case 1
            parm = "Center-weighted average"
          Case 2
            parm = "Spot"
          Case Else
            parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Metering Mode:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(24)) Then
        v = makertags.Item(sTag(24)).Value
        parm = ""
        For i = 0 To UBound(v)
          parm = parm & " " & v(i)
        Next i
        If Len(parm) > 0 Then note = note & "Auto Bracketing:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(25)) Then
        i = makertags.Item(sTag(25)).singleValue
        Select Case i
          Case 0 : parm = "Auto"
          Case 1 : parm = "Daylight"
          Case 2 : parm = "Shade"
          Case 3 : parm = "Fluorescent"
          Case 4 : parm = "Tungsten"
          Case 5 : parm = "Manual"
          Case 6 : parm = "Daylight Fluorescent"
          Case 7 : parm = "Daywhite Fluorescent"
          Case 8 : parm = "White Fluorescent"
          Case 9 : parm = "Flash"
          Case 10 : parm = "Cloudy"
          Case 17 : parm = "Kelvin"
          Case 65534 : parm = "Unknown"
          Case 65535 : parm = "User-selected"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(26)) Then
        i = makertags.Item(sTag(26)).singleValue
        Select Case i
          Case 1 : parm = "(Auto) Daylight"
          Case 2 : parm = "(Auto) Shade"
          Case 3 : parm = "(Auto) Fluorescent"
          Case 4 : parm = "(Auto) Tungsten"
          Case 5 : parm = "(Auto) Manual"
          Case 6 : parm = "(Auto) Daylight Fluorescent"
          Case 7 : parm = "(Auto) Daywhite Fluorescent"
          Case 8 : parm = "(Auto) White Fluorescent"
          Case 9 : parm = "(Auto) Flash"
          Case 10 : parm = "(Auto) Cloudy"
          Case 17 : parm = "(Auto) Kelvin"
          Case 65534 : parm = "Unknown"
          Case 65535 : parm = "User-selected"
          Case Else
            parm = ""
        End Select
        note = note & "White Balance Mode:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(27)) Then
        i = makertags.Item(sTag(27)).singleValue
        note = note & "Blue Balance:" & tb & i & "\par "
      End If

      If makertags.Contains(sTag(28)) Then
        i = makertags.Item(sTag(28)).singleValue
        note = note & "Red Balance:" & tb & i & "\par "
      End If

      If makertags.Contains(sTag(29)) Then
        i = makertags.Item(sTag(29)).singleValue
        note = note & "Focal Length:" & tb & i / 100 & " mm\par "
      End If

      If makertags.Contains(sTag(30)) Then
        i = makertags.Item(sTag(30)).singleValue
        note = note & "Digital Zoom:" & tb & i & "\par "
      End If

      If makertags.Contains(sTag(31)) Then
        v = makertags.Item(sTag(31)).Value
        Select Case v(0)
          Case 0 : parm = "Low"
          Case 1 : parm = "Normal"
          Case 2 : parm = "High"
          Case 3 : parm = "Medium Low"
          Case 4 : parm = "Medium High"
          Case 5 : parm = "Very Low"
          Case 6 : parm = "Very High"
          Case 65535 : parm = "(none)"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Saturation:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(32)) Then
        v = makertags.Item(sTag(32)).Value
        Select Case v(0)
          Case 0 : parm = "Low"
          Case 1 : parm = "Normal"
          Case 2 : parm = "High"
          Case 3 : parm = "Medium Low"
          Case 4 : parm = "Medium High"
          Case 5 : parm = "Very Low"
          Case 6 : parm = "Very High"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Contrast:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(33)) Then
        v = makertags.Item(sTag(33)).Value
        Select Case v(0)
          Case 0 : parm = "Soft"
          Case 1 : parm = "Normal"
          Case 2 : parm = "Hard"
          Case 3 : parm = "Medium Soft"
          Case 4 : parm = "Medium Hard"
          Case 5 : parm = "Very Soft"
          Case 6 : parm = "Very Hard"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Sharpness:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(34)) Then
        v = makertags.Item(sTag(34)).Value
        Select Case v(0)
          Case 0 : parm = "Home"
          Case 1 : parm = "Destination"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "World Time Location:" & tb & parm & "\par "
      End If

      pentaxCity(0) = "Pago Pago" : pentaxCity(1) = "Honolulu" : pentaxCity(2) = "Anchorage" : pentaxCity(3) = "Vancouver"
      pentaxCity(4) = "San Fransisco" : pentaxCity(5) = "Los Angeles" : pentaxCity(6) = "Calgary" : pentaxCity(7) = "Denver"
      pentaxCity(8) = "Mexico City" : pentaxCity(9) = "Chicago" : pentaxCity(10) = "Miami" : pentaxCity(11) = "Toronto"
      pentaxCity(12) = "New York" : pentaxCity(13) = "Santiago" : pentaxCity(14) = "Caracus" : pentaxCity(15) = "Halifax"
      pentaxCity(16) = "Buenos Aires" : pentaxCity(17) = "Sao Paulo" : pentaxCity(18) = "Rio de Janeiro" : pentaxCity(19) = "Madrid"
      pentaxCity(20) = "London" : pentaxCity(21) = "Paris" : pentaxCity(22) = "Milan" : pentaxCity(23) = "Rome" : pentaxCity(24) = "Berlin"
      pentaxCity(25) = "Johannesburg" : pentaxCity(26) = "Istanbul" : pentaxCity(27) = "Cairo" : pentaxCity(28) = "Jerusalem"
      pentaxCity(29) = "Moscow" : pentaxCity(30) = "Jeddah" : pentaxCity(31) = "Tehran" : pentaxCity(32) = "Dubai"
      pentaxCity(33) = "Karachi" : pentaxCity(34) = "Kabul" : pentaxCity(35) = "Male" : pentaxCity(36) = "Delhi" : pentaxCity(37) = "Colombo"
      pentaxCity(38) = "Kathmandu" : pentaxCity(39) = "Dacca" : pentaxCity(40) = "Yangon" : pentaxCity(41) = "Bangkok"
      pentaxCity(42) = "Kuala Lumpur" : pentaxCity(43) = "Vientiane" : pentaxCity(44) = "Singapore" : pentaxCity(45) = "Phnom Penh"
      pentaxCity(46) = "Ho Chi Minh" : pentaxCity(47) = "Jakarta" : pentaxCity(48) = "Hong Kong" : pentaxCity(49) = "Perth"
      pentaxCity(50) = "Beijing" : pentaxCity(51) = "Shanghai" : pentaxCity(52) = "Manila" : pentaxCity(53) = "Taipei"
      pentaxCity(54) = "Seoul" : pentaxCity(55) = "Adelaide" : pentaxCity(56) = "Tokyo" : pentaxCity(57) = "Guam"
      pentaxCity(58) = "Sydney" : pentaxCity(59) = "Noumea" : pentaxCity(60) = "Wellington" : pentaxCity(61) = "Auckland"
      pentaxCity(62) = "Lima" : pentaxCity(63) = "Dakar" : pentaxCity(64) = "Algiers" : pentaxCity(65) = "Helsinki" : pentaxCity(66) = "Athens"
      pentaxCity(67) = "Nairobi" : pentaxCity(68) = "Amsterdam" : pentaxCity(69) = "Stockholm" : pentaxCity(70) = "Lisbon"

      If makertags.Contains(sTag(35)) Then
        i = makertags.Item(sTag(35)).singleValue
        note = note & "Home City Code:" & tb & pentaxCity(i) & "\par "
      End If

      If makertags.Contains(sTag(36)) Then
        i = makertags.Item(sTag(36)).singleValue
        note = note & "Destination City Code:" & tb & pentaxCity(i) & "\par "
      End If

      If makertags.Contains(sTag(37)) Then
        i = makertags.Item(sTag(37)).singleValue
        If i = 0 Then parm = "No" Else parm = "Yes"
        If Trim(parm) <> "" Then note = note & "Home Daylight Savings Time:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(38)) Then
        i = makertags.Item(sTag(38)).singleValue
        If i = 0 Then parm = "No" Else parm = "Yes"
        If Trim(parm) <> "" Then note = note & "Dest. Daylight Savings Time:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(39)) Then
        v = makertags.Item(sTag(39)).Value
        parm = ""
        For i = 0 To UBound(v) : parm = parm & 255 - v(i) : Next i
        If Trim(parm) <> "" Then note = note & "DSP Firmware Version:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(40)) Then
        v = makertags.Item(sTag(40)).Value
        parm = ""
        For i = 0 To UBound(v) : parm = parm & 255 - v(i) : Next i
        If Trim(parm) <> "" Then note = note & "CPU Firmware Version:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(41)) Then
        i = makertags.Item(sTag(41)).singlevalue
        If i > 0 Then note = note & "Frame Number:" & tb & i & "\par "
      End If

      If makertags.Contains(sTag(45)) Then
        i = makertags.Item(sTag(45)).singlevalue
        If i > 0 Then note = note & "Effective LV:" & tb & i & "\par "
      End If

      If makertags.Contains(sTag(50)) Then
        v = makertags.Item(sTag(50)).Value
        parm = ""
        If v(0) = 0 And v(UBound(v)) = 0 Then
          parm = "Unprocessed"
        ElseIf UBound(v) = 3 Then
          If v(3) = 4 Then parm = "Digital Filter"
          If v(0) = 2 Then parm = "Cropped"
          If v(0) = 4 Then parm = "Color Filter"
          If v(0) = 16 Then parm = "Frame Synthesis"
        End If
        If Len(parm) > 0 Then note = note & "Image Processing:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(51)) Then
        v = makertags.Item(sTag(51)).Value
        parm = ""
        If UBound(v) >= 1 Then
          If v(0) = 0 And v(1) = 0 Then parm = "Program"
          If v(0) = 0 And v(1) = 1 Then parm = "Hi-speed Program"
          If v(0) = 0 And v(1) = 2 Then parm = "DOF Program"
          If v(0) = 0 And v(1) = 3 Then parm = "MTF Program"
          If v(0) = 0 And v(1) = 4 Then parm = "Standard"
          If v(0) = 0 And v(1) = 5 Then parm = "Portrait"
          If v(0) = 0 And v(1) = 6 Then parm = "Landscape"
          If v(0) = 0 And v(1) = 7 Then parm = "Macro"
          If v(0) = 0 And v(1) = 8 Then parm = "Sport"
          If v(0) = 0 And v(1) = 9 Then parm = "Night Scene Portrait"
          If v(0) = 0 And v(1) = 10 Then parm = "No Flash"
          If v(0) = 0 And v(1) = 11 Then parm = "Night Scene"
          If v(0) = 0 And v(1) = 12 Then parm = "Surf & Snow"
          If v(0) = 0 And v(1) = 13 Then parm = "Text"
          If v(0) = 0 And v(1) = 14 Then parm = "Sunset"
          If v(0) = 0 And v(1) = 15 Then parm = "Kids"
          If v(0) = 0 And v(1) = 16 Then parm = "Pet"
          If v(0) = 0 And v(1) = 17 Then parm = "Candlelight"
          If v(0) = 0 And v(1) = 18 Then parm = "Museum"
          If v(0) = 0 And v(1) = 19 Then parm = "Food"
          If v(0) = 0 And v(1) = 20 Then parm = "Stage Lighting"
          If v(0) = 0 And v(1) = 21 Then parm = "Night Snap"
          If v(0) = 1 And v(1) = 4 Then parm = "Auto PICT (Standard)"
          If v(0) = 1 And v(1) = 5 Then parm = "Auto PICT (Portrait)"
          If v(0) = 1 And v(1) = 6 Then parm = "Auto PICT (Landscape)"
          If v(0) = 1 And v(1) = 7 Then parm = "Auto PICT (Macro)"
          If v(0) = 1 And v(1) = 8 Then parm = "Auto PICT (Sport)"
          If v(0) = 2 And v(1) = 0 Then parm = "Program (HyP)"
          If v(0) = 2 And v(1) = 1 Then parm = "Hi-speed Program (HyP)"
          If v(0) = 2 And v(1) = 2 Then parm = "DOF Program (HyP)"
          If v(0) = 2 And v(1) = 3 Then parm = "MTF Program (HyP)"
          If v(0) = 3 And v(1) = 0 Then parm = "Green Mode"
          If v(0) = 4 And v(1) = 0 Then parm = "Shutter Speed Priority"
          If v(0) = 5 And v(1) = 0 Then parm = "Aperture Priority"
          If v(0) = 6 And v(1) = 0 Then parm = "Program Tv Shift"
          If v(0) = 7 And v(1) = 0 Then parm = "Program Av Shift"
          If v(0) = 8 And v(1) = 0 Then parm = "Manual"
          If v(0) = 9 And v(1) = 0 Then parm = "Bulb"
          If v(0) = 10 And v(1) = 0 Then parm = "Aperture Priority, Off-Auto-Aperture"
          If v(0) = 11 And v(1) = 0 Then parm = "Manual, Off-Auto-Aperture"
          If v(0) = 12 And v(1) = 0 Then parm = "Bulb, Off-Auto-Aperture"
          If v(0) = 13 And v(1) = 0 Then parm = "Shutter & Aperture Priority AE"
          If v(0) = 15 And v(1) = 0 Then parm = "Sensitivity Priority AE"
          If v(0) = 16 And v(1) = 0 Then parm = "Flash X-Sync Speed AE"
        End If
        If UBound(v) >= 2 And Len(parm) <> 0 Then
          If v(2) = 0 Then parm = parm & ", 1/2 EV steps"
          If v(2) = 1 Then parm = parm & ", 1/3 EV steps"
        End If
        If Len(parm) > 0 Then note = note & "Picture Mode:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(52)) Then
        v = makertags.Item(sTag(52)).Value
        parm = ""
        If v(0) = 0 Then parm = "Single-frame"
        If v(0) = 1 Then parm = "Continuous"
        If v(0) = 2 Then parm = "Continuous (Hi)"
        If v(0) = 3 Then parm = "Burst"

        If UBound(v) >= 1 Then
          If v(1) = 0 Then parm = ", No Timer"
          If v(1) = 1 Then parm = ", Self-timer (12 s)"
          If v(1) = 2 Then parm = ", Self-timer (2 s)"
        End If

        If UBound(v) >= 1 Then
          If v(2) = 0 Then parm = ", Shutter Button"
          If v(2) = 1 Then parm = ", Remote Control (3 s delay)"
          If v(2) = 2 Then parm = ", Remote Control"
        End If

        If UBound(v) >= 3 Then
          If v(3) = 0 Then parm = ", Single Exposure"
          If v(3) = 1 Then parm = ", Multiple Exposure"
        End If

        If Left(parm, 2) = ", " Then parm = Mid(parm, 3)
        If Len(parm) > 0 Then note = note & "Drive Mode:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(55)) Then
        i = makertags.Item(sTag(55)).singleValue
        If i = 0 Then parm = "sRGB" Else If i = 1 Then parm = "Adobe RGB" Else parm = ""
        If Len(parm) > 0 Then note = note & "Color Space:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(60)) Then
        i = makertags.Item(sTag(60)).singleValue
        parm = ""
        If i And 1 Then parm = parm & " Upper-left"
        If i And 2 Then parm = parm & " Top"
        If i And 4 Then parm = parm & " Upper-right"
        If i And 8 Then parm = parm & " Left"
        If i And 16 Then parm = parm & " Mid-left"
        If i And 32 Then parm = parm & " Center"
        If i And 64 Then parm = parm & " Mid-right"
        If i And 128 Then parm = parm & " Right"
        If i And 256 Then parm = parm & " Lower-left"
        If i And 512 Then parm = parm & " Bottom"
        If i And 1024 Then parm = parm & " Lower-right"
        If Len(parm) > 0 Then note = note & "AF Points in Focus:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(62)) Then
        v = makertags.Item(sTag(62)).Value
        If UBound(v) >= 3 Then parm = v(0) & " " & v(1) & " " & v(2) & " " & v(3)
        If Len(parm) > 0 Then note = note & "Preview Image Borders:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(63)) Then
        v = makertags.Item(sTag(63)).Value
        If UBound(v) >= 1 Then parm = v(0) & " " & v(1)
        If Len(parm) > 0 Then note = note & "Pentax Lens ID:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(64)) Then
        i = makertags.Item(sTag(64)).singleValue
        note = note & "Sensitivity Adjust:" & tb & i & "\par "
      End If

      If makertags.Contains(sTag(65)) Then
        i = makertags.Item(sTag(65)).singleValue
        note = note & "Image Processing Count:" & tb & i & "\par "
      End If

      If makertags.Contains(sTag(71)) Then
        i = makertags.Item(sTag(71)).singleValue
        note = note & "Camera Temperature:" & tb & i & "\par "
      End If

      If makertags.Contains(sTag(72)) Then
        i = makertags.Item(sTag(72)).singleValue
        If i = 0 Then parm = "Off" Else If i = 1 Then parm = "On" Else parm = ""
        If Len(parm) > 0 Then note = note & "AE Lock:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(73)) Then
        i = makertags.Item(sTag(73)).singleValue
        If i = 0 Then parm = "Off" Else If i = 1 Then parm = "On" Else parm = ""
        If Len(parm) > 0 Then note = note & "Noise Reduction:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(77)) Then
        i = makertags.Item(sTag(77)).singleValue
        note = note & "Flash Exposure Compensation:" & tb & i & "\par "
      End If

      If makertags.Contains(sTag(79)) Then
        v = makertags.Item(sTag(79)).Value
        Select Case v(0)
          Case 0
            parm = "Natural"
          Case 1
            parm = "Bright"
          Case 2
            parm = "Portrait"
          Case 3
            parm = "Landscape"
          Case 4
            parm = "Vibrant"
          Case 5
            parm = "Monotone"
          Case Else
            parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Image Tone:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(80)) Then
        i = makertags.Item(sTag(80)).singleValue
        note = note & "Color Temperature:" & tb & i & "\par "
      End If

      If makertags.Contains(sTag(92)) Then
        v = makertags.Item(sTag(92)).Value
        i = v(0)
        If v(0) = 0 Then parm = "Not Stabilized"
        If i And 1 Then parm = "Stabilized"
        If i And 64 Then parm = parm & ", Not Ready"
        If UBound(v) >= 1 Then
          Select Case v(1)
            Case 0
              parm = "Off, " & parm
            Case 1
              parm = "On, " & parm
            Case 4
              parm = "Off (4), " & parm
            Case 5
              parm = "On (5), " & parm
            Case 7
              parm = "On (7), " & parm
          End Select
        End If
        note = note & "Shake Reduction:" & tb & i & "\par "

        If UBound(v) >= 2 Then note = note & "Half Press Time:" & tb & v(2) & "\par "
        If UBound(v) >= 3 Then note = note & "SR Focal Length:" & tb & v(3) & "\par "
      End If

      If makertags.Contains(sTag(93)) Then
        v = makertags.Item(sTag(93)).Value
        parm = ""
        For i = 0 To UBound(v) : parm = parm & v(i) & " " : Next
        If Len(parm) > 0 Then note = note & "Shutter Count:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(105)) Then
        v = makertags.Item(sTag(105)).Value
        If v(0) = 0 Then parm = "Off" Else If v(0) = 1 Then parm = "On" Else parm = ""
        If Len(parm) > 0 Then note = note & "Dynamic Range Expansion:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(&H71)) Then
        i = makertags.Item(sTag(&H71)).singleValue
        Select Case i
          Case 0
            parm = "Off"
          Case 1
            parm = "Weakest"
          Case 2
            parm = "Weak"
          Case 3
            parm = "Strong"
          Case Else
            parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "High ISO Noise Reduction:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(&H72)) Then
        i = makertags.Item(sTag(&H72)).singleValue
        note = note & "Autofocus Adjustment:" & tb & i & "\par "
      End If

      If makertags.Contains(sTag(&H200)) Then
        v = makertags.Item(sTag(&H200)).Value
        If UBound(v) >= 3 Then parm = v(0) & " " & v(1) & " " & v(2) & " " & v(3) Else parm = ""
        If Len(parm) > 0 Then note = note & "Black Point:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(&H201)) Then
        v = makertags.Item(sTag(&H201)).Value
        If UBound(v) >= 3 Then parm = v(0) & " " & v(1) & " " & v(2) & " " & v(3) Else parm = ""
        If Len(parm) > 0 Then note = note & "White Point:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(&H203)) Then
        v = makertags.Item(sTag(&H203)).Value
        parm = ""
        If UBound(v) >= 8 Then parm = v(0) & " " & v(1) & " " & v(2) & crlf & tb & v(3) & " " & v(4) & " " & v(5) & crlf & tb & v(6) & " " & v(7) & " " & v(8)
        If Len(parm) > 0 Then note = note & "Color Matrix A:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(&H204)) Then
        v = makertags.Item(sTag(&H204)).Value
        parm = ""
        If UBound(v) >= 8 Then parm = v(0) & " " & v(1) & " " & v(2) & crlf & tb & v(3) & " " & v(4) & " " & v(5) & crlf & tb & v(6) & " " & v(7) & " " & v(8)
        If Len(parm) > 0 Then note = note & "Color Matrix B:" & tb & parm & "\par "
      End If

      If makertags.Contains(sTag(&H205)) Then
        v = makertags.Item(sTag(&H205)).Value
        If UBound(v) >= 10 Then
          Select Case v(0)
            Case 0 : parm = "Scene Mode"
            Case 1 : parm = "Auto(PICT)"
            Case 2 : parm = "Program(AE)"
            Case 3 : parm = "Green(Mode)"
            Case 4 : parm = "Shutter Speed Priority"
            Case 5 : parm = "Aperture(Priority)"
            Case 6 : parm = "Program Tv Shift"
            Case 7 : parm = "Program Av Shift"
            Case 8 : parm = "Manual"
            Case 9 : parm = "Bulb"
            Case 10 : parm = "Aperture(Priority, Off - Auto - Aperture) "
            Case 11 : parm = "Manual, Off - Auto - Aperture "
            Case 12 : parm = "Bulb, Off - Auto - Aperture "
            Case 13 : parm = "Shutter & Aperture Priority AE "
            Case 15 : parm = "Sensitivity Priority AE "
            Case 16 : parm = "Flash X-Sync Speed AE "
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "Picture Mode:" & tb & parm & "\par "

          i = v(1)
          Select Case (i And 3)
            Case 0 : parm = "Normal"
            Case 1 : parm = "High Speed"
            Case 2 : parm = "Depth"
            Case 3 : parm = "MTF"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "Program Line:" & tb & parm & "\par "

          If i And &H20 Then parm = "1/3 Step" Else parm = "1/2 Step"
          If Len(parm) > 0 Then note = note & "EV Steps:" & tb & parm & "\par "

          If i And &H40 Then parm = "P Shift" Else parm = "Tv or Av"
          If Len(parm) > 0 Then note = note & "E-Dial-In Program:" & tb & parm & "\par "

          If i And &H80 Then parm = "Permitted" Else parm = "Prohibited"
          If Len(parm) > 0 Then note = note & "Aparture Ring Use:" & tb & parm & "\par "

          i = v(2)
          Select Case (i And 15)
            Case 0 : parm = "Normal"
            Case 1 : parm = "Red-eye reduction"
            Case 2 : parm = "Auto"
            Case 3 : parm = "Auto, Red-eye reduction"
            Case 5 : parm = "Wireless (Master)"
            Case 6 : parm = "Wireless (Control)"
            Case 8 : parm = "Slow-sync"
            Case 9 : parm = "Slow-sync, Red-eye reduction"
            Case 15 : parm = "Trailing-curtain Sync"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "Flash Options:" & tb & parm & "\par "

          i = v(3)
          parm = ""
          If (i And &HF0) = 0 Then
            parm = "Auto"
          ElseIf (i And &H10) Then
            parm = "Select"
          ElseIf (i And &H20) Then
            parm = "Fixed Center"
          End If
          If Len(parm) > 0 Then note = note & "AF Point Mode:" & tb & parm & "\par "

          i = v(3)
          Select Case (i And 15)
            Case 0 : parm = "Manual"
            Case 1 : parm = "AF-S"
            Case 2 : parm = "AF-C"
            Case 3 : parm = "AF-A"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "Focus Mode:" & tb & parm & "\par "

          i = v(4)
          If i = 0 Then parm = "Auto" Else parm = ""
          If i And 1 Then parm = parm & " Upper-left"
          If i And 2 Then parm = parm & " Top"
          If i And 4 Then parm = parm & " Upper-right"
          If i And 8 Then parm = parm & " Left"
          If i And 16 Then parm = parm & " Mid-left"
          If i And 32 Then parm = parm & " Center"
          If i And 64 Then parm = parm & " Mid-right"
          If i And 128 Then parm = parm & " Right"
          If i And 256 Then parm = parm & " Lower-left"
          If i And 512 Then parm = parm & " Bottom"
          If i And 1024 Then parm = parm & " Lower-right"
          If Len(parm) > 0 Then note = note & "AF Points Selected:" & tb & parm & "\par "

          note = note & "ISO Floor:" & tb & v(6) & "\par "

          i = v(7)
          If i = 0 Then parm = "Single-frame" Else parm = ""
          If i And 1 Then parm = parm & " Continuous"
          If i And 4 Then parm = parm & " Self-timer (12 s)"
          If i And 8 Then parm = parm & " Self-timer (2 s)"
          If i And 16 Then parm = parm & " Remote Control (3 s delay)"
          If i And 32 Then parm = parm & " Remote Control"
          If i And 64 Then parm = parm & " Exposure Bracket"
          If i And 128 Then parm = parm & " Multiple Exposure"
          If Len(parm) > 0 Then note = note & "Drive Mode:" & tb & parm & "\par "

          Select Case v(8)
            Case 3 : parm = "0.3"
            Case 4 : parm = "0.5"
            Case 5 : parm = "0.7"
            Case 8 : parm = "1.0"
            Case 11 : parm = "1.3"
            Case 12 : parm = "1.5"
            Case 13 : parm = "1.7"
            Case 16 : parm = "2.0"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "Exposure Bracket Step Size:" & tb & parm & "\par "

          Select Case v(9)
            Case &H0 : parm = "n/a"
            Case &H3 : parm = "1 of 3"
            Case &H5 : parm = "1 of 5"
            Case &H13 : parm = "2 of 3"
            Case &H15 : parm = "2 of 5"
            Case &H23 : parm = "3 of 3"
            Case &H25 : parm = "3 of 5"
            Case &H35 : parm = "4 of 5"
            Case &H45 : parm = "5 of 5"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "Bracket Shot Number:" & tb & parm & "\par "

          i = v(10)
          Select Case (i And &HF0)
            Case &H0 : parm = "Auto"
            Case &H10 : parm = "Daylight"
            Case &H20 : parm = "Shade"
            Case &H30 : parm = "Cloudy"
            Case &H40 : parm = "DaylightFluorescent"
            Case &H50 : parm = "DaywhiteFluorescent"
            Case &H60 : parm = "WhiteFluorescent"
            Case &H70 : parm = "Tungsten"
            Case &H80 : parm = "Flash"
            Case &H90 : parm = "Manual"
            Case &HC0 : parm = "Set Color Temperature 1"
            Case &HD0 : parm = "Set Color Temperature 2"
            Case &HE0 : parm = "Set Color Temperature 3"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "White Balance Set:" & tb & parm & "\par "
          If (i And 1) Then parm = "On" Else parm = "Off"
          If Len(parm) > 0 Then note = note & "Multiple Exposure Set:" & tb & parm & "\par "
        End If ' ubound(v) >= 10

        If UBound(v) >= 21 Then
          Select Case v(13)
            Case 1 : parm = "JPEG (Best)"
            Case 4 : parm = "RAW (PEF, Best)"
            Case 5 : parm = "RAW+JPEG (PEF, Best)"
            Case 8 : parm = "RAW (DNG, Best)"
            Case 9 : parm = "RAW+JPEG (DNG, Best)"
            Case 33 : parm = "JPEG (Better)"
            Case 36 : parm = "RAW (PEF, Better)"
            Case 37 : parm = "RAW+JPEG (PEF, Better)"
            Case 40 : parm = "RAW (DNG, Better)"
            Case 41 : parm = "RAW+JPEG (DNG, Better)"
            Case 65 : parm = "JPEG (Good)"
            Case 68 : parm = "RAW (PEF, Good)"
            Case 69 : parm = "RAW+JPEG (PEF, Good)"
            Case 72 : parm = "RAW (DNG, Good)"
            Case 73 : parm = "RAW+JPEG (DNG, Good)"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "Raw and JPG Recording:" & tb & parm & "\par "
        End If ' ubound(10)

        If UBound(v) >= 21 Then
          Select Case v(14)
            Case 0 : parm = "10 MP"
            Case 1 : parm = "6 MP"
            Case 2 : parm = "2 MP"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "JPG Recorded Pixels:" & tb & parm & "\par "

          i = v(16)
          Select Case (i And &HF0)
            Case &H0 : parm = "Normal"
            Case &H10 : parm = "Red-eye reduction"
            Case &H20 : parm = "Auto"
            Case &H30 : parm = "Auto, Red-eye reduction"
            Case &H50 : parm = "Wireless (Master)"
            Case &H60 : parm = "Wireless (Control)"
            Case &H80 : parm = "Slow-sync"
            Case &H90 : parm = "Slow-sync, Red-eye reduction"
            Case &HA0 : parm = "Trailing-curtain Sync"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "Flash Options:" & tb & parm & "\par "

          Select Case (i And 15)
            Case 0 : parm = "Multi-segment"
            Case 1 : parm = "Center-weighted average"
            Case 2 : parm = "Spot"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "Metering Mode:" & tb & parm & "\par "

          i = v(17)
          If i And &H80 Then parm = "Yes" Else parm = "No"
          If Len(parm) > 0 Then note = note & "Shake Reduction Active:" & tb & parm & "\par "

          Select Case (i And &H60)
            Case 0 : parm = "Upright"
            Case &H20 : parm = "Upside Down"
            Case &H40 : parm = "Rotated Right"
            Case &H60 : parm = "Rotated Left"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "Rotation:" & tb & parm & "\par "

          If i And 4 Then parm = "Auto" Else parm = "Manual"
          If Len(parm) > 0 Then note = note & "ISO Setting:" & tb & parm & "\par "

          If i And 2 Then parm = "As EV Steps" Else parm = "1 EV Step"
          If Len(parm) > 0 Then note = note & "Sensitivity Steps:" & tb & parm & "\par "

          note = note & "TV Exposure Time Setting:" & tb & v(18) & "\par "
          note = note & "AV Aperture Setting:" & tb & v(19) & "\par "
          note = note & "SV ISO Setting:" & tb & v(20) & "\par "
          note = note & "Base Exposure Compensation:" & tb & v(21) & "\par "
        End If ' ubound(21)

      End If ' pentax tag 205

      If makertags.Contains(sTag(&H206)) Then ' AE Info
        v = makertags.Item(sTag(&H206)).Value
        If UBound(v) >= 14 Then
          x = 24 * 2 ^ ((32 - v(0)) / 8)
          note = note & "AV Exposure Time:" & tb & Format(x, "###,##0.###") & "\par "

          x = 2 ^ ((v(1) - 68) / 16)
          note = note & "AE Aperture:" & tb & Format(x, "###,##0.###") & "\par "

          x = 100 * 2 ^ ((v(2) - 32) / 8)
          note = note & "AE ISO:" & tb & Format(x, "###,##0.###") & "\par "

          x = (v(3) - 64) / 8
          note = note & "AEXv:" & tb & Format(x, "###,##0.###") & "\par "

          x = v(4) / 8
          note = note & "AEBXv:" & tb & Format(x, "###,##0.###") & "\par "

          x = 24 * 2 ^ ((32 - v(5)) / 8)
          note = note & "AE Minimum Exposure Time:" & tb & Format(x, "###,##0.###") & "\par "

          Select Case v(6)
            Case 0 : parm = "M, P or TAv"
            Case 1 : parm = "Av, B or X"
            Case 2 : parm = "Tv"
            Case 3 : parm = "Sv or Green Mode"
            Case 8 : parm = "Hi-speed Program"
            Case 11 : parm = "Hi-speed Program (P-Shift)"
            Case 16 : parm = "DOF Program"
            Case 19 : parm = "DOF Program (P-Shift)"
            Case 24 : parm = "MTF Program"
            Case 27 : parm = "MTF Program (P-Shift)"
            Case 35 : parm = "Standard"
            Case 43 : parm = "Portrait"
            Case 51 : parm = "Landscape"
            Case 59 : parm = "Macro"
            Case 67 : parm = "Sport"
            Case 75 : parm = "Night Scene Portrait"
            Case 83 : parm = "No Flash"
            Case 91 : parm = "Night Scene"
            Case 99 : parm = "Surf & Snow"
            Case 107 : parm = "Text"
            Case 115 : parm = "Sunset"
            Case 123 : parm = "Kids"
            Case 131 : parm = "Pet"
            Case 139 : parm = "Candlelight"
            Case 147 : parm = "Museum"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "AE Program Mode:" & tb & parm & "\par "

          x = 2 ^ ((v(9) - 68) / 16)
          note = note & "AE Maximum Aperture:" & tb & Format(x, "###,##0.###") & "\par "
          x = 2 ^ ((v(10) - 68) / 16)
          note = note & "AE Maximum Aperture 2:" & tb & Format(x, "###,##0.###") & "\par "
          x = 2 ^ ((v(11) - 68) / 16)
          note = note & "AE Minimum Aperture:" & tb & Format(x, "###,##0.###") & "\par "

          i = v(12)
          If i = 0 Then parm = "Multi-segment" Else If i And 16 Then parm = "Center-weighted average" Else If i And 32 Then parm = "Spot" Else parm = ""
          If Len(parm) > 0 Then note = note & "AE Metering Mode:" & tb & parm & "\par "

          note = note & "AE Flash Exp Comp Set:" & tb & v(14) & "\par "
        End If ' ubound 14

      End If ' pentax tag 206

    End If

  End Sub ' pentaxMakernote

  Sub ricohMakernote(ByRef makerTags As Collection, ByRef note As String, ByVal intel As Boolean)

    Dim i, j As Integer
    Dim parm As String = ""
    Dim v As Object

    If makerTags.Contains(sTag(1)) Then
      parm = makerTags.Item(sTag(1)).singleValue
      If Trim(parm) <> "" Then note = note & "Maker Note Type:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(2)) Then
      parm = makerTags.Item(sTag(2)).singleValue
      If Trim(parm) <> "" Then note = note & "Maker Note Version:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1001)) Then
      v = makerTags.Item(sTag(&H1001)).value
      i = word(v, 0, intel, False)
      j = word(v, 2, intel, False)
      If i > 0 And j > 0 Then note = note & "Image Size:" & tb & i & " x " & j & "\par "

      If uuBound(v) >= 32 Then
        Select Case v(32)
          Case 0
            parm = "Off"
          Case 1
            parm = "Auto"
          Case 2
            parm = "On"
          Case Else
            parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Flash Mode:" & tb & parm & "\par "
      End If

      If uuBound(v) >= 33 Then
        Select Case v(33)
          Case 0
            parm = "Off"
          Case 1
            parm = "On"
          Case Else
            parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Macro:" & tb & parm & "\par "
      End If

      If uuBound(v) >= 34 Then
        Select Case v(34)
          Case 0
            parm = "Sharp"
          Case 1
            parm = "Normal"
          Case 2
            parm = "Soft"
          Case Else
            parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Sharpness:" & tb & parm & "\par "
      End If

      If uuBound(v) >= 38 Then
        Select Case v(38)
          Case 0
            parm = "Auto"
          Case 1
            parm = "Daylight"
          Case 2
            parm = "Cloudy"
          Case 3
            parm = "Tungsten"
          Case 4
            parm = "Fluorescent"
          Case Else
            parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "
      End If

      If uuBound(v) >= 39 Then
        Select Case v(39)
          Case 0
            parm = "Auto"
          Case 1
            parm = "64"
          Case 2
            parm = "100"
          Case 4
            parm = "200"
          Case 6
            parm = "400"
          Case 7
            parm = "800"
          Case 8
            parm = "1600"
          Case Else
            parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "ISO Setting:" & tb & parm & "\par "
      End If

      If uuBound(v) >= 40 Then
        Select Case v(40)
          Case 0
            parm = "High"
          Case 1
            parm = "Normal"
          Case 2
            parm = "Low"
          Case 3
            parm = "(none) (B&W)"
          Case Else
            parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Saturation:" & tb & parm & "\par "
      End If
    End If ' 1001

    If makerTags.Contains(sTag(&H1003)) Then
      i = makerTags.Item(sTag(&H1003)).singleValue
      Select Case i
        Case 0
          parm = "Sharp"
        Case 1
          parm = "Normal"
        Case 2
          parm = "Soft"
      End Select
      If Len(parm) > 0 Then note = note & "Sharpness:" & tb & parm & "\par "
    End If
  End Sub ' ricohMakernote

  Sub canonMakernote(ByRef makerTags As Collection, ByRef note As String, ByVal intel As Boolean)

    Dim i, k As Integer
    Dim kw As UInteger
    Dim parm As String = ""
    Dim v As Object
    Dim x As Double
    Dim canonModel As Long
    Dim canonFirmware As String = ""
    Dim b() As Byte
    Dim dt As DateTime

    If makerTags.Contains(sTag(1)) Then
      v = makerTags.Item(sTag(1)).Value
      If uuBound(v) >= 17 Then

        If v(1) = 1 Then note = note & "Macro Mode: " & tb & "On" & "\par "

        note = note & "Self Timer: " & tb & v(2) & "\par "

        Select Case v(3)
          Case 1 : parm = "Economy"
          Case 2 : parm = "Normal"
          Case 3 : parm = "Fine"
          Case 4 : parm = "Raw"
          Case 5 : parm = "Superfine"
          Case 130 : parm = "Normal Movie"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Image Quality:" & tb & parm & "\par "

        Select Case v(4)
          Case 0 : parm = "Off"
          Case 1 : parm = "Auto"
          Case 2 : parm = "On"
          Case 3 : parm = "Red-eye reduction"
          Case 4 : parm = "Slow Sync"
          Case 5 : parm = "Red-eye reduction (Auto)"
          Case 6 : parm = "Red-eye reduction (On)"
          Case 16 : parm = "External Flash"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Flash Mode:" & tb & parm & "\par "

        Select Case v(5)
          Case 0 : parm = "Single"
          Case 1 : parm = "Continuous"
          Case 2 : parm = "Movie"
          Case 3 : parm = "Continuous, Speed Priority"
          Case 4 : parm = "Continuous, Low"
          Case 5 : parm = "Continuous, High"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Continuous Drive:" & tb & parm & "\par "

        Select Case v(7)
          Case 0 : parm = "One-shot AF"
          Case 1 : parm = "AI Servo AF"
          Case 2 : parm = "AI Focus AF"
          Case 3, 6 : parm = "Manual Focus"
          Case 4 : parm = "Single"
          Case 5 : parm = "Continuous"
          Case 16 : parm = "Pan Focus"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Focus Mode:" & tb & parm & "\par "

        Select Case v(9)
          Case 1 : parm = "JPEG"
          Case 2 : parm = "CRW+THM"
          Case 3 : parm = "AVI+THM"
          Case 4 : parm = "TIF"
          Case 5 : parm = "TIF+JPEG"
          Case 6 : parm = "CR2 "
          Case 7 : parm = "CR2+JPEG"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Record Mode:" & tb & parm & "\par "

        Select Case v(10)
          Case 0 : parm = "Large"
          Case 1 : parm = "Medium"
          Case 2 : parm = "Small"
          Case 5 : parm = "Medium 1"
          Case 6 : parm = "Medium 2"
          Case 7 : parm = "Medium 3"
          Case 8 : parm = "Postcard"
          Case 9 : parm = "Widescreen"
          Case 128 : parm = "Large Movie"
          Case 129 : parm = "Medium Movie"
          Case 130 : parm = "Small Movie"
          Case Else : parm = ""
        End Select
        If Trim(parm) <> "" Then note = note & "Image Size:" & tb & parm & "\par "

        Select Case v(11)
          Case 0 : parm = "Auto"
          Case 1 : parm = "Manual"
          Case 2 : parm = "Landscape"
          Case 3 : parm = "Fast shutter"
          Case 4 : parm = "Slow shutter"
          Case 5 : parm = "Night"
          Case 6 : parm = "Gray Scale"
          Case 7 : parm = "Sepia"
          Case 8 : parm = "Portrait"
          Case 9 : parm = "Sports"
          Case 10 : parm = "Macro"
          Case 11 : parm = "Black & White"
          Case 12 : parm = "Pan focus"
          Case 13 : parm = "Vivid"
          Case 14 : parm = "Neutral"
          Case 15 : parm = "Flash Off"
          Case 16 : parm = "Long Shutter"
          Case 17 : parm = "Super Macro"
          Case 18 : parm = "Foliage"
          Case 19 : parm = "Indoor"
          Case 20 : parm = "Fireworks"
          Case 21 : parm = "Beach"
          Case 22 : parm = "Underwater"
          Case 23 : parm = "Snow"
          Case 24 : parm = "Kids & Pets"
          Case 25 : parm = "Night Snapshot"
          Case 26 : parm = "Digital Macro"
          Case 27 : parm = "My Colors"
          Case 28 : parm = "Still Image"
          Case 30 : parm = "Color Accent"
          Case 31 : parm = "Color Swap"
          Case 32 : parm = "Aquarium"
          Case 33 : parm = "ISO 3200"
          Case Else : parm = ""
        End Select
        If Trim(parm) <> "" Then note = note & "Shooting Mode:" & tb & parm & "\par "

        Select Case v(12)
          Case 0 : parm = "(none)"
          Case 1 : parm = "2x"
          Case 2 : parm = "4x"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Digital Zoom:" & tb & parm & "\par "

        Select Case v(13)
          Case -1 : parm = "Low"
          Case 0 : parm = "Normal"
          Case 1 : parm = "High"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Contrast:" & tb & parm & "\par "

        Select Case v(14)
          Case -1 : parm = "Low"
          Case 0 : parm = "Normal"
          Case 1 : parm = "High"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Saturation:" & tb & parm & "\par "

        Select Case v(15)
          Case -1 : parm = "Low"
          Case 0 : parm = "Normal"
          Case 1 : parm = "High"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Sharpness:" & tb & parm & "\par "

        Select Case v(16)
          Case 15 : parm = "Auto"
          Case 16 : parm = "50"
          Case 17 : parm = "100"
          Case 18 : parm = "200"
          Case 19 : parm = "400"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 And parm <> "0" Then note = note & "ISO Speed:" & tb & parm & "\par "

        Select Case v(17)
          Case 0 : parm = "Default"
          Case 1 : parm = "Spot"
          Case 2 : parm = "Average"
          Case 3 : parm = "Evaluative"
          Case 4 : parm = "Partial"
          Case 5 : parm = "Center Weighted Average"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Metering Mode:" & tb & parm & "\par "

        If UBound(v) >= 18 Then
          Select Case v(18)
            Case 0 : parm = "Manual"
            Case 1 : parm = "Auto"
            Case 2 : parm = "Unknown"
            Case 3 : parm = "Macro"
            Case 4 : parm = "Very Close"
            Case 5 : parm = "Close"
            Case 6 : parm = "Middle Range"
            Case 7 : parm = "Far Range"
            Case 8 : parm = "Pan Focus"
            Case 9 : parm = "Super Macro"
            Case 10 : parm = "Infinity"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "Focus Range:" & tb & parm & "\par "
        End If

        If UBound(v) >= 19 Then
          Select Case v(19)
            Case &H2005 : parm = "Manual AF point selection"
            Case &H3000 : parm = "(none - MF)"
            Case &H3001 : parm = "Auto AF point selection"
            Case &H3002 : parm = "Right"
            Case &H3003 : parm = "Center"
            Case &H3004 : parm = "Left"
            Case &H4001 : parm = "Auto AF point selection"
            Case &H4006 : parm = "Face Detect"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "Autofocus Point:" & tb & parm & "\par "
        End If

        If UBound(v) >= 20 Then
          Select Case v(20)
            Case 0 : parm = "Automatic"
            Case 1 : parm = "Program"
            Case 2 : parm = "Shutter Speed Priority"
            Case 3 : parm = "Aperture Priority"
            Case 4 : parm = "Manual"
            Case 5 : parm = "Depth of Field"
            Case 6 : parm = "M-Dep"
            Case 7 : parm = "Bulb"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "Exposure Mode:" & tb & parm & "\par "
        End If

        If UBound(v) >= 22 AndAlso v(22) <> 65535 AndAlso v(22) <> 0 Then note = note & "Lens Type: " & tb & v(22) & "\par "
        If UBound(v) >= 23 AndAlso v(23) <> 65535 AndAlso v(23) <> 0 Then note = note & "Long Focal Length: " & tb & v(23) & "\par "
        If UBound(v) >= 24 AndAlso v(24) <> 65535 AndAlso v(24) <> 0 Then note = note & "Short Focal Length: " & tb & v(24) & "\par "
        If UBound(v) >= 25 AndAlso v(25) <> 65535 AndAlso v(25) <> 0 Then note = note & "Focal Units: " & tb & v(25) & "\par "
        If UBound(v) >= 26 AndAlso v(26) <> 65535 AndAlso v(26) <> 0 Then note = note & "Max Aperture: " & tb & v(26) & "\par "
        If UBound(v) >= 27 AndAlso v(27) <> 65535 AndAlso v(27) <> 0 Then note = note & "Min Aperture: " & tb & v(27) & "\par "
        If UBound(v) >= 28 AndAlso v(28) <> 65535 Then note = note & "Flash Activity: " & tb & v(28) & "\par "

        If UBound(v) >= 29 Then
          i = v(29)
          If i And 1 Then parm = parm & "Manual "
          If i And 2 Then parm = parm & "TTL "
          If i And 4 Then parm = parm & "A-TTL "
          If i And 8 Then parm = parm & "E-TTL "
          If i And 16 Then parm = parm & "FP sync enabled "
          If i And 128 Then parm = parm & "2nd-curtain sync used "
          If i And 2048 Then parm = parm & "FP sync used "
          If i And 8192 Then parm = parm & "Built-in "
          If i And 16384 Then parm = parm & "External "
          If Trim(parm) <> "" Then note = note & "Flash:" & tb & Trim(parm) & "\par "
        End If

        If UBound(v) >= 32 Then
          Select Case v(32)
            Case 0 : parm = "Single"
            Case 1 : parm = "Continuous"
            Case 8 : parm = "Manual"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "Focus Continuous:" & tb & parm & "\par "
        End If

        If UBound(v) >= 32 Then
          Select Case v(32)
            Case 0 : parm = "Normal AE"
            Case 1 : parm = "Exposure Compensation"
            Case 2 : parm = "AE Lock"
            Case 3 : parm = "AE Lock + Exposure Comp."
            Case 4 : parm = "No AE"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "AE Setting:" & tb & parm & "\par "
        End If


        If UBound(v) >= 34 Then
          Select Case v(34)
            Case 0 : parm = "Off"
            Case 1 : parm = "On"
            Case 2 : parm = "On, Shot Only"
            Case 3 : parm = "On, Panning"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "Image Stabilization:" & tb & parm & "\par "
        End If

        If UBound(v) >= 35 AndAlso v(35) > 0 Then note = note & "Display Aperture: " & tb & v(35) & "\par "
        If UBound(v) >= 36 AndAlso v(36) > 0 Then note = note & "Zoom Source Width: " & tb & v(36) & "\par "
        If UBound(v) >= 37 AndAlso v(37) > 0 Then note = note & "Zoom Target Width: " & tb & v(37) & "\par "

        If UBound(v) >= 39 Then
          Select Case v(39)
            Case 0 : parm = "Center"
            Case 1 : parm = "AF Point"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "Spot Metering Mode:" & tb & parm & "\par "
        End If

        If UBound(v) >= 40 Then
          Select Case v(40)
            Case 0 : parm = "Off"
            Case 1 : parm = "Vivid"
            Case 2 : parm = "Neutral"
            Case 3 : parm = "Smooth"
            Case 4 : parm = "Sepia"
            Case 5 : parm = "B&W"
            Case 6 : parm = "Custom"
            Case 100 : parm = "My Color Data"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "Photo Effect:" & tb & parm & "\par "
        End If

        If UBound(v) >= 41 Then
          Select Case v(41)
            Case &H500 : parm = "Full"
            Case &H502 : parm = "Medium"
            Case &H504 : parm = "Low"
            Case Else : parm = ""
          End Select
          If Len(parm) > 0 Then note = note & "Manual Flash Output:" & tb & parm & "\par "
        End If

        If UBound(v) >= 42 AndAlso v(42) <> 32767 Then note = note & "Color Tone: " & tb & v(42) & "\par "

      End If ' tag 1 for canon
    End If ' tag 1 for canon

    If makerTags.Contains(sTag(2)) Then
      v = makerTags.Item(sTag(2)).Value
      If UBound(v) >= 1 Then
        If v(0) = 1 Then parm = "Fixed" Else If v(0) = 2 Then parm = "Zoom" Else parm = ""
        If Len(parm) > 0 Then note = note & "Focal Type:" & tb & parm & "\par "
        note = note & "Focal length:" & tb & v(1) & "\par "
      End If
    End If

    If makerTags.Contains(sTag(4)) Then
      v = makerTags.Item(sTag(4)).Value

      If v(1) < 60000 Then note = note & "Auto ISO:" & tb & v(1) & "\par "
      If v(2) < 60000 Then note = note & "Base ISO:" & tb & v(2) & "\par "
      note = note & "Measured EV:" & tb & v(3) & "\par "
      note = note & "Target Aperture:" & tb & v(4) & "\par "
      note = note & "Target Exposure Time:" & tb & v(5) & "\par "
      note = note & "Exposure Compensation:" & tb & v(6) & "\par "

      Select Case v(7)
        Case 0 : parm = "Auto"
        Case 1 : parm = "Sunny"
        Case 2 : parm = "Cloudy"
        Case 3 : parm = "Tungsten"
        Case 4 : parm = "Fluorescent"
        Case 5 : parm = "Flash"
        Case 6 : parm = "Custom"
        Case Else : parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "

      Select Case v(8)
        Case 0 : parm = "Off"
        Case 1 : parm = "Night Scene"
        Case 2 : parm = "On"
        Case 3 : parm = "(none)"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Slow Shutter:" & tb & parm & "\par "

      If UBound(v) >= 9 AndAlso v(9) > 0 Then note = note & "Sequence Number:" & tb & v(9) & "\par "
      If UBound(v) >= 10 Then note = note & "Optical Zoom Code:" & tb & v(10) & "\par "
      If UBound(v) >= 13 Then note = note & "Flash Guide Number:" & tb & v(13) & "\par "

      If UBound(v) >= 14 Then
        Select Case v(14)
          Case &H3000 : parm = "(none - MF)"
          Case &H3001 : parm = "Right"
          Case &H3002 : parm = "Center"
          Case &H3003 : parm = "Center+Right"
          Case &H3004 : parm = "Left"
          Case &H3005 : parm = "Left+Right"
          Case &H3006 : parm = "Left+Center"
          Case &H3007 : parm = "All"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "AF Points in Focus:" & tb & parm & "\par "
      End If

      If UBound(v) >= 16 Then note = note & "Flash Exposure Compensation:" & tb & v(16) & "\par "
      If UBound(v) >= 17 Then note = note & "AEB Bracket Value:" & tb & v(17) & "\par "

      If UBound(v) >= 18 Then
        Select Case v(18)
          Case 1 : parm = "Camera Local"
          Case 3 : parm = "Computer Remote"
          Case Else
            parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Control Mode:" & tb & parm & "\par "
      End If

      If UBound(v) >= 19 AndAlso v(19) > 0 Then note = note & "Upper Focus Distance:" & tb & Format(v(19) / 100, "####,##0.##") & " m\par "
      If UBound(v) >= 20 AndAlso v(20) > 0 Then note = note & "Lower Focus Distance:" & tb & Format(v(20) / 100, "####,##0.##") & " m\par "
      If UBound(v) >= 21 AndAlso v(21) > 0 Then note = note & "F-Number Code:" & tb & v(21) & "\par "
      If UBound(v) >= 22 AndAlso v(22) > 0 Then
        x = Exp(-(v(22) / 32) * Log(2))
        If x > 0.00000000001 Then
          If x < 1 And x <> 0 Then
            parm = Format(x, "##0.0###") & " (1/" & Format(1 / x, "##,###") & ") sec."
          Else
            parm = Format(x, "##0.0") & " sec."
          End If
          note = note & "Exposure Time:" & tb & parm & "\par "
        End If
      End If
      If UBound(v) >= 24 AndAlso v(24) > 0 Then note = note & "Bulb Duration:" & tb & v(24) & "\par "

      If UBound(v) >= 26 Then
        Select Case v(26)
          Case 248 : parm = "EOS High-End"
          Case 250 : parm = "Compact"
          Case 252 : parm = "EOS Midrange"
          Case 255 : parm = "DV Camera"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Camera Type:" & tb & parm & "\par "
      End If

      If UBound(v) >= 27 Then
        Select Case v(27)
          Case 0 : parm = "Upright"
          Case 1 : parm = "Rotated Right"
          Case 2 : parm = "Upside Down"
          Case 3 : parm = "Rotated Left"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Auto Rotate:" & tb & parm & "\par "
      End If

      If UBound(v) >= 28 Then
        Select Case v(28)
          Case 0 : parm = "Off"
          Case 1 : parm = "On"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "ND Filter:" & tb & parm & "\par "
      End If

      If UBound(v) >= 29 AndAlso v(29) <> 65535 Then note = note & "Self Timer:" & tb & v(29) & "\par "
      If UBound(v) >= 33 AndAlso v(33) <> 65535 Then note = note & "Flash Output:" & tb & v(33) & "\par "

    End If ' tag 4 for canon

    If makerTags.Contains(sTag(6)) Then
      parm = makerTags.Item(sTag(6)).singleValue
      If InStr(parm, ChrW(0)) > 0 Then parm = Trim(Mid(parm, 1, InStr(parm, ChrW(0)) - 1))
      'parm = splitString(parm)
      If Trim(parm) <> "" Then note = note & "Image Type:" & tb & Trim(parm) & "\par "
    End If ' tag 6 for Canon

    If makerTags.Contains(sTag(7)) Then
      parm = makerTags.Item(sTag(7)).singleValue
      If InStr(parm, ChrW(0)) > 0 Then parm = Trim(Mid(parm, 1, InStr(parm, ChrW(0)) - 1))
      ' parm = splitString(parm)
      If Trim(parm) <> "" Then
        note = note & "Firmware:" & tb & Trim(parm) & "\par "
        canonFirmware = Trim(parm)
      End If
    End If

    If makerTags.Contains(sTag(8)) Then
      k = makerTags.Item(sTag(8)).singleValue
      i = Int(k / 10000)
      note = note & "Image Number:" & tb & Format(i, "0000") & "-" & Format(k - i * 10000, "0000") & "\par "
    End If ' tag 8 for Canon

    If makerTags.Contains(sTag(9)) Then
      parm = makerTags.Item(sTag(9)).singleValue
      If InStr(parm, ChrW(0)) > 0 Then parm = Mid(parm, 1, InStr(parm, ChrW(0)) - 1)
      If Trim(parm) <> "" Then note = note & "Owner Name:" & tb & Trim(parm) & "\par "
    End If ' tag 9 for Canon

    If makerTags.Contains(sTag(12)) Then
      kw = makerTags.Item(sTag(12)).singleValue
      i = kw Mod 65536
      kw = kw / 65536
      If kw <> 0 Then note = note & "Camera Serial Number:" & tb & Right("000" & Hex(kw), 4) & Format(i, "0000") & "\par "
    End If ' tag 12 for Canon

    If makerTags.Contains(sTag(13)) Then
      If makerTags.Contains(sTag(16)) Then canonModel = makerTags.Item(sTag(16)).singleValue Else canonModel = 0 ' model number
      v = makerTags.Item(sTag(13)).Value
      ReDim b(-1)
      Try
        b = v.clone
      Catch
        canonModel = 0
      End Try

      Select Case canonModel And &HFFFF
        Case &H1, &H167  ' EOS-1D, 1DS
          If UBound(v) >= 81 Then
            note = note & "{\b Canon EOS-1D information\b0 }" & "\par "
            x = Exp(4 * Log(2) * (1 - (b(4) - 24) / 32))
            If x > 0.00000000001 Then
              If x < 1 And x <> 0 Then
                parm = Format(x, "##0.0###") & " (1/" & Format(1 / x, "##,###") & ") sec."
              Else
                parm = Format(x, "##0.0") & " sec."
              End If
              note = note & "Exposure Time:" & tb & parm & "\par "
            End If
            note = note & "Focal Length:" & tb & word(b, 10, intel) & " mm\par "
            note = note & "Lens Type:" & tb & canonDescr(4, b(13)) & "\par "
            note = note & "Short Focal:" & tb & word(b, 14, intel) & " mm\par "
            note = note & "Long Focal:" & tb & word(b, 16, intel) & " mm\par "
            If k = &H1 Then ' 1D
              Select Case b(65)
                Case 1 : parm = "Lowest"
                Case 2 : parm = "Low"
                Case 3 : parm = "Standard"
                Case 4 : parm = "High"
                Case 5 : parm = "Highest"
                Case Else : parm = ""
              End Select
              If Len(parm) > 0 Then note = note & "Sharpness Frequency:" & tb & parm & "\par "
              note = note & "Sharpness:" & tb & b(66) & "\par "
              parm = canonDescr(1, b(68))
              If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "
              note = note & "Color Temperature:" & tb & word(b, 72, intel) & "\par "
              parm = canonDescr(1, b(75))
              If Len(parm) > 0 Then note = note & "Picture Style:" & tb & parm & "\par "
            ElseIf k = &H167 Then ' 1DS
              Select Case b(71)
                Case 1 : parm = "Lowest"
                Case 2 : parm = "Low"
                Case 3 : parm = "Standard"
                Case 4 : parm = "High"
                Case 5 : parm = "Highest"
                Case Else : parm = ""
              End Select
              If Len(parm) > 0 Then note = note & "Sharpness Frequency:" & tb & parm & "\par "
              note = note & "Sharpness:" & tb & b(72) & "\par "
              parm = canonDescr(1, b(74))
              If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "
              note = note & "Color Temperature:" & tb & word(b, 78, intel) & "\par "
              parm = canonDescr(2, b(81))
              If Len(parm) > 0 Then note = note & "Picture Style:" & tb & parm & "\par "
            End If
          End If ' EOS-1D, 1DS

        Case &H174, &H188  ' EOS-1D Mark II, EOS-1Ds Mark II
          If UBound(v) >= 117 Then
            note = note & "{\b Canon EOS-1D Mark II information\b0 }" & "\par "
            x = Exp(4 * Log(2) * (1 - (b(4) - 24) / 32))
            If x > 0.00000000001 Then
              If x < 1 And x <> 0 Then
                parm = Format(x, "##0.0###") & " (1/" & Format(1 / x, "##,###") & ") sec."
              Else
                parm = Format(x, "##0.0") & " sec."
              End If
              note = note & "Exposure Time:" & tb & parm & "\par "
            End If
            note = note & "Focal Length:" & tb & word(b, 9, False) & " mm\par "
            note = note & "Lens Type:" & tb & canonDescr(4, b(13)) & "\par "
            note = note & "Short Focal:" & tb & word(b, 17, intel) & " mm\par "
            note = note & "Long Focal:" & tb & word(b, 19, intel) & " mm\par "
            If b(45) = 0 Then parm = "Fixed" Else If b(45) = 2 Then parm = "Zoom" Else parm = ""
            If Len(parm) > 0 Then note = note & "Focal Type:" & tb & parm & "\par "
            parm = canonDescr(1, b(54))
            If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "
            note = note & "Color Temperature:" & tb & word(b, 55, intel) & "\par "
            Select Case b(57)
              Case 1 : parm = "Large"
              Case 2, 5, 6, 7 : parm = "Medium"
              Case 3 : parm = "Small"
              Case 8 : parm = "Postcard"
              Case 9 : parm = "Widescreen"
              Case 129 : parm = "Medium Movie"
              Case 130 : parm = "Small Movie"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "Image Size:" & tb & parm & "\par "
            note = note & "Jpeg Quality:" & tb & b(102) & "\par "
            parm = canonDescr(2, b(108))
            If Len(parm) > 0 Then note = note & "Picture Style:" & tb & parm & "\par "
            note = note & "Saturation:" & tb & b(110) & "\par "
            note = note & "Color Tone:" & tb & b(111) & "\par "
            note = note & "Sharpness:" & tb & b(114) & "\par "
            note = note & "Contrast:" & tb & b(115) & "\par "
            parm = UTF8bare.GetString(b, 117, 5)
            If InStr(parm, ChrW(0)) > 0 Then parm = Mid(parm, 1, InStr(parm, ChrW(0)) - 1)
            If Len(parm) > 0 Then note = note & "ISO:" & tb & parm & "\par "
          End If ' EOS-1D Mark II, EOS-1Ds Mark II

        Case &H232 ' EOS-1D Mark II N
          If UBound(v) >= 117 Then
            note = note & "{\b Canon EOS-1D Mark II N information\b0 }" & "\par "
            x = Exp(4 * Log(2) * (1 - (b(4) - 24) / 32))
            If x > 0.00000000001 Then
              If x < 1 And x <> 0 Then
                parm = Format(x, "##0.0###") & " (1/" & Format(1 / x, "##,###") & ") sec."
              Else
                parm = Format(x, "##0.0") & " sec."
              End If
              note = note & "Exposure Time:" & tb & parm & "\par "
            End If
            note = note & "Focal Length:" & tb & word(b, 9, False) & " mm\par "
            note = note & "Lens Type:" & tb & canonDescr(4, b(13)) & "\par "
            note = note & "Short Focal:" & tb & word(b, 17, intel) & " mm\par "
            note = note & "Long Focal:" & tb & word(b, 19, intel) & " mm\par "
            parm = canonDescr(1, b(54))
            If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "
            note = note & "Color Temperature:" & tb & word(b, 55, intel) & "\par "
            parm = canonDescr(2, v(115))
            If Len(parm) > 0 Then note = note & "Picture Style:" & tb & parm & "\par "
            note = note & "Sharpness:" & tb & b(116) & "\par "
            note = note & "Contrast:" & tb & b(117) & "\par "
            note = note & "Saturation:" & tb & b(118) & "\par "
            note = note & "Color Tone:" & tb & b(119) & "\par "
            parm = UTF8bare.GetString(b, 121, 5)
            If InStr(parm, ChrW(0)) > 0 Then parm = Mid(parm, 1, InStr(parm, ChrW(0)) - 1)
            If Len(parm) > 0 Then note = note & "ISO:" & tb & parm & "\par "
          End If ' EOS-1D Mark II N

        Case &H169, &H215 ' EOS-1D Mark III, EOS-1Ds Mark III
          If UBound(v) >= 382 Then
            note = note & "{\b Canon EOS-1D Mark III information\b0 }" & "\par "
            x = Exp((b(3) - 8) / 16 * Log(2))
            note = note & "F-Number:" & tb & Format(x, "#,##0.0") & "\par "
            x = Exp(4 * Log(2) * (1 - (b(4) - 24) / 32))
            If x > 0.00000000001 Then
              If x < 1 And x <> 0 Then
                parm = Format(x, "##0.0###") & " (1/" & Format(1 / x, "##,###") & ") sec."
              Else
                parm = Format(x, "##0.0") & " sec."
              End If
              note = note & "Exposure Time:" & tb & parm & "\par "
            End If
            x = 100 * Exp((b(6) / 8 - 9) * Log(2))
            note = note & "ISO:" & tb & Format(x, "######") & "\par "
            note = note & "Camera Temperature:" & tb & word(b, 24, intel) & "\par "
            note = note & "Focal Length:" & tb & word(b, 29, intel) & " mm\par "
            note = note & "Upper Focal Distance:" & tb & word(b, 67, intel) / 100 & " m\par "
            note = note & "Lower Focal Distance:" & tb & word(b, 79, intel) / 100 & " m\par "
            parm = canonDescr(1, word(b, 94, intel))
            If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "
            note = note & "Color Temperature:" & tb & word(b, 98, intel) & "\par "
            parm = canonDescr(2, b(134))
            If Len(parm) > 0 Then note = note & "Picture Style:" & tb & parm & "\par "
            note = note & "Lens Type:" & tb & canonDescr(4, b(274)) & "\par "
            note = note & "Short Focal:" & tb & word(b, 274, intel) & " mm\par "
            note = note & "Long Focal:" & tb & word(b, 277, intel) & " mm\par "
            parm = UTF8bare.GetString(b, 310, 6)
            If InStr(parm, ChrW(0)) > 0 Then parm = Mid(parm, 1, InStr(parm, ChrW(0)) - 1)
            If Len(parm) > 0 Then note = note & "Firmware Version:" & tb & parm & "\par "
            note = note & "File Index:" & tb & DWord(b, 370, intel) & "\par "
            note = note & "Directory Index:" & tb & DWord(b, 382, intel) & "\par "
          End If ' EOS-1D Mark III, EOS-1Ds Mark III

        Case &H213 ' eos 5D
          If UBound(v) >= 382 Then
            note = note & "{\b Canon EOS 5D information\b0 }" & "\par "
            x = Exp((b(3) - 8) / 16 * Log(2))
            note = note & "F-Number:" & tb & Format(x, "#,##0.0") & "\par "
            x = Exp(4 * Log(2) * (1 - (b(4) - 24) / 32))
            If x > 0.00000000001 Then
              If x < 1 And x <> 0 Then
                parm = Format(x, "##0.0###") & " (1/" & Format(1 / x, "##,###") & ") sec."
              Else
                parm = Format(x, "##0.0") & " sec."
              End If
              note = note & "Exposure Time:" & tb & parm & "\par "
            End If
            x = 100 * Exp((b(6) / 8 - 9) * Log(2))
            note = note & "ISO:" & tb & Format(x, "######") & "\par "
            Select Case b(39)
              Case 0 : parm = "Upright"
              Case 1 : parm = "Rotated Right"
              Case 2 : parm = "Rotated Left"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "Camera Orientation:" & tb & parm & "\par "
            note = note & "Focal Length:" & tb & Format(word(b, 40, intel), "###,###,###") & " mm\par "
            note = note & "Short Focal:" & tb & Format(word(b, 147, intel), "###,###,###") & " mm\par "
            note = note & "Long Focal:" & tb & Format(word(b, 149, intel), "###,###,###") & " mm\par "
            i = word(b, 56, intel, False)
            parm = ""
            If i And 2 ^ 0 Then parm = parm & " Center"
            If i And 2 ^ 1 Then parm = parm & " Top"
            If i And 2 ^ 2 Then parm = parm & " Bottom"
            If i And 2 ^ 3 Then parm = parm & " Upper-left"
            If i And 2 ^ 4 Then parm = parm & " Upper-right"
            If i And 2 ^ 5 Then parm = parm & " Lower-left"
            If i And 2 ^ 6 Then parm = parm & " Lower-right"
            If i And 2 ^ 7 Then parm = parm & " Left"
            If i And 2 ^ 8 Then parm = parm & " Right"
            If i And 2 ^ 9 Then parm = parm & " AI Servo1"
            If i And 2 ^ 10 Then parm = parm & " AI Servo2"
            If i And 2 ^ 11 Then parm = parm & " AI Servo3"
            If i And 2 ^ 12 Then parm = parm & " AI Servo4"
            If i And 2 ^ 13 Then parm = parm & " AI Servo5"
            If i And 2 ^ 14 Then parm = parm & " AI Servo6"
            If Len(parm) > 0 Then note = note & "AF Points in Focus:" & tb & parm & "\par "
            parm = canonDescr(1, word(b, 84, intel))
            If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "
            note = note & "Color Temperature:" & tb & word(b, 88, intel) & "\par "
            parm = canonDescr(2, b(108))
            If Len(parm) > 0 Then note = note & "Picture Style:" & tb & parm & "\par "
            note = note & "Lens Type:" & tb & canonDescr(4, b(152)) & "\par "
            parm = UTF8bare.GetString(b, 164, 8)
            If InStr(parm, ChrW(0)) > 0 Then parm = Mid(parm, 1, InStr(parm, ChrW(0)) - 1)
            If Len(parm) > 0 Then note = note & "Firmware Version:" & tb & parm & "\par "
            parm = UTF8bare.GetString(b, 72, 16)
            If InStr(parm, ChrW(0)) > 0 Then parm = Mid(parm, 1, InStr(parm, ChrW(0)) - 1)
            If Len(parm) > 0 Then note = note & "Short Owner Name:" & tb & parm & "\par "
            note = note & "Image Number:" & tb & word(b, 208, intel, False) & "\par "
            parm = ""
            For i = 232 To 240 : parm = parm & b(i) & " " : Next i
            note = note & "Contrast Settings:" & tb & Trim(parm) & "\par "
            parm = ""
            For i = 241 To 249 : parm = parm & b(i) & " " : Next i
            note = note & "Sharpness Settings:" & tb & parm & "\par "
            parm = ""
            For i = 250 To 254 : parm = parm & b(i) & " " : Next i
            For i = 256 To 258 : parm = parm & b(i) & " " : Next i
            note = note & "Sharpness Settings:" & tb & Trim(parm) & "\par "
            Select Case b(255)
              Case 0 : parm = "(none)"
              Case 1 : parm = "Yellow"
              Case 2 : parm = "Orange"
              Case 3 : parm = "Red"
              Case 4 : parm = "Green"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "Filter Effect Monochrome:" & tb & parm & "\par "
            parm = ""
            For i = 259 To 263 : parm = parm & b(i) & " " : Next i
            For i = 265 To 267 : parm = parm & b(i) & " " : Next i
            note = note & "Color Tone Settings:" & tb & Trim(parm) & "\par "
            Select Case b(264)
              Case 0 : parm = "(none)"
              Case 1 : parm = "Sepia"
              Case 2 : parm = "Blue"
              Case 3 : parm = "Purple"
              Case 4 : parm = "Green"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "Toning Effect Monochrome:" & tb & parm & "\par "
            parm = canonDescr(3, word(b, 268, intel))
            If Len(parm) > 0 Then note = note & "User Picture Style 1:" & tb & parm & "\par "
            parm = canonDescr(3, word(b, 270, intel))
            If Len(parm) > 0 Then note = note & "User Picture Style 2:" & tb & parm & "\par "
            parm = canonDescr(3, word(b, 272, intel))
            If Len(parm) > 0 Then note = note & "User Picture Style 3:" & tb & parm & "\par "
            x = DWord(b, 274, intel)
            If x > 0 Then
              dt = New DateTime(1970, 1, 1)
              parm = Format(dt.AddSeconds(x), "G")
              note = note & "Time Stamp:" & tb & parm & "\par "
            End If
          End If ' EOS 5D

        Case &H213 ' eos 5D Mark II
          If UBound(v) >= 382 Then
            note = note & "{\b Canon EOS 5D Mark II information\b0 }" & "\par "
            x = Exp((b(3) - 8) / 16 * Log(2))
            note = note & "F-Number:" & tb & Format(x, "#,##0.0") & "\par "
            x = Exp(4 * Log(2) * (1 - (b(4) - 24) / 32))
            If x > 0.00000000001 Then
              If x < 1 And x <> 0 Then
                parm = Format(x, "##0.0###") & " (1/" & Format(1 / x, "##,###") & ") sec."
              Else
                parm = Format(x, "##0.0") & " sec."
              End If
              note = note & "Exposure Time:" & tb & parm & "\par "
            End If
            x = 100 * Exp((b(6) / 8 - 9) * Log(2))
            note = note & "ISO:" & tb & Format(x, "######") & "\par "

            If b(7) = 0 Then parm = "Off" Else If b(7) = 1 Then parm = "On" Else parm = ""
            If Len(parm) > 0 Then note = note & "Highlight Tone Priority:" & tb & parm & "\par "

            note = note & "Camera Temperature:" & tb & b(25) & "\par "
            note = note & "Focal Length:" & tb & Format(word(b, 30, intel), "###,###,###") & " mm\par "
            note = note & "Upper Focus Distance:" & tb & Format(word(b, 80, intel), "###,##0.##") / 100 & " m\par "
            note = note & "Lower Focus Distance:" & tb & Format(word(b, 82, intel), "###,##0.##") / 100 & " m\par "
            note = note & "Short Focal:" & tb & Format(word(b, 232, intel), "###,###,###") & " mm\par "
            note = note & "Long Focal:" & tb & Format(word(b, 234, intel), "###,###,###") & " mm\par "
            Select Case b(49)
              Case 0 : parm = "Upright"
              Case 1 : parm = "Rotated Right"
              Case 2 : parm = "Rotated Left"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "Camera Orientation:" & tb & parm & "\par "
            parm = canonDescr(1, word(b, 111, intel))
            If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "
            note = note & "Color Temperature:" & tb & word(b, 115, intel) & "\par "
            parm = canonDescr(2, b(167))
            If Len(parm) > 0 Then note = note & "Picture Style:" & tb & parm & "\par "
            Select Case b(189)
              Case 0 : parm = "Standard"
              Case 1 : parm = "Low"
              Case 2 : parm = "Strong"
              Case 3 : parm = "Off"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "High ISO Noise Reduction:" & tb & parm & "\par "
            Select Case b(191)
              Case 0 : parm = "Standard"
              Case 1 : parm = "Low"
              Case 2 : parm = "Strong"
              Case 3 : parm = "Off"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "Auto Lighting Optimizer:" & tb & parm & "\par "
            note = note & "Lens Type:" & tb & canonDescr(4, b(231)) & "\par "
            parm = UTF8bare.GetString(b, 346, 5)
            If AscW(parm.Chars(0)) < 32 Then parm = ""
            If InStr(parm, ChrW(0)) > 0 Then parm = Mid(parm, 1, InStr(parm, ChrW(0)) - 1)
            If Len(parm) > 0 Then note = note & "Firmware Version:" & tb & parm & "\par " : k = 0
            parm = UTF8bare.GetString(b, 382, 5)
            If AscW(parm.Chars(0)) < 32 Then parm = ""
            If InStr(parm, ChrW(0)) > 0 Then parm = Mid(parm, 1, InStr(parm, ChrW(0)) - 1)
            If Len(parm) > 0 Then note = note & "Firmware Version:" & tb & parm & "\par " : k = 36
            x = DWord(b, 407 + k, intel)
            If x > 0 Then note = note & "File Index:" & tb & Format(x, "###,###,###") & "\par "
            x = DWord(b, 419 + k, intel)
            If x > 0 Then note = note & "Directory Index:" & tb & Format(x, "###,###,###") & "\par "
          End If ' EOS 5D Mark II

        Case &H190 ' EOS 40D
          If UBound(v) >= 382 Then
            note = note & "{\b Canon EOS 40D information\b0 }" & "\par "
            x = Exp((b(3) - 8) / 16 * Log(2))
            note = note & "F-Number:" & tb & Format(x, "#,##0.0") & "\par "
            x = Exp(4 * Log(2) * (1 - (b(4) - 24) / 32))
            If x > 0.00000000001 Then
              If x < 1 And x <> 0 Then
                parm = Format(x, "##0.0###") & " (1/" & Format(1 / x, "##,###") & ") sec."
              Else
                parm = Format(x, "##0.0") & " sec."
              End If
              note = note & "Exposure Time:" & tb & parm & "\par "
            End If
            x = 100 * Exp((b(6) / 8 - 9) * Log(2))
            note = note & "ISO:" & tb & Format(x, "######") & "\par "
            note = note & "Camera Temperature:" & tb & b(24) & "\par "
            note = note & "Focal Length:" & tb & Format(word(b, 29, intel), "###,###,###") & " mm\par "
            note = note & "Short Focal:" & tb & Format(word(b, 216, intel), "###,###,###") & " mm\par "
            note = note & "Long Focal:" & tb & Format(word(b, 218, intel), "###,###,###") & " mm\par "
            note = note & "Upper Focus Distance:" & tb & Format(word(b, 67, intel) / 100, "###,##0.##") & " m\par "
            note = note & "Lower Focus Distance:" & tb & Format(word(b, 69, intel) / 100, "###,##0.##") & " m\par "
            Select Case b(48)
              Case 0 : parm = "Upright"
              Case 1 : parm = "Rotated Right"
              Case 2 : parm = "Rotated Left"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "Camera Orientation:" & tb & parm & "\par "
            parm = canonDescr(1, word(b, 111, intel))
            If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "
            note = note & "Color Temperature:" & tb & word(b, 115, intel) & "\par "
            parm = UTF8bare.GetString(b, 255, 5)
            If InStr(parm, ChrW(0)) > 0 Then parm = Mid(parm, 1, InStr(parm, ChrW(0)) - 1)
            If Len(parm) > 0 Then note = note & "Firmware Version:" & tb & parm & "\par "
            x = DWord(b, 407, intel)
            If x > 0 Then note = note & "File Index:" & tb & Format(x, "###,###,###") & "\par "
            x = DWord(b, 419, intel)
            If x > 0 Then note = note & "Directory Index:" & tb & Format(x, "###,###,###") & "\par "
            note = note & "Lens Type:" & tb & canonDescr(4, b(215)) & "\par "
            parm = UTF8bare.GetString(b, 2347, 64)
            If InStr(parm, ChrW(0)) > 0 Then parm = Mid(parm, 1, InStr(parm, ChrW(0)) - 1)
            If Len(parm) > 0 Then note = note & "Lens Model:" & tb & parm & "\par "
          End If ' EOS 40D

        Case &H261 ' EOS 50D
          If UBound(v) >= 382 Then
            note = note & "{\b Canon EOS 50D information\b0 }" & "\par "
            x = Exp((b(3) - 8) / 16 * Log(2))
            note = note & "F-Number:" & tb & Format(x, "#,##0.0") & "\par "
            x = Exp(4 * Log(2) * (1 - (b(4) - 24) / 32))
            If x > 0.00000000001 Then
              If x < 1 And x <> 0 Then
                parm = Format(x, "##0.0###") & " (1/" & Format(1 / x, "##,###") & ") sec."
              Else
                parm = Format(x, "##0.0") & " sec."
              End If
              note = note & "Exposure Time:" & tb & parm & "\par "
            End If
            x = 100 * Exp((b(6) / 8 - 9) * Log(2))
            note = note & "ISO:" & tb & Format(x, "######") & "\par "
            If b(7) = 0 Then parm = "Off" Else If b(7) = 1 Then parm = "On" Else parm = ""
            If Len(parm) > 0 Then note = note & "Highlight Tone Priority:" & tb & parm & "\par "
            note = note & "Camera Temperature:" & tb & b(25) & "\par "
            note = note & "Focal Length:" & tb & Format(word(b, 30, intel), "###,###,##0") & " mm\par "
            note = note & "Short Focal:" & tb & Format(word(b, 236, intel), "###,###,##0") & " mm\par "
            note = note & "Long Focal:" & tb & Format(word(b, 238, intel), "###,###,##0") & " mm\par "
            x = word(b, 80, intel) / 100
            If x > 0 Then note = note & "Upper Focus Distance:" & tb & Format(x, "###,##0.##") & " m\par "
            x = word(b, 82, intel) / 100
            If x > 0 Then note = note & "Lower Focus Distance:" & tb & Format(x, "###,##0.##") & " m\par "
            Select Case b(49)
              Case 0 : parm = "Upright"
              Case 1 : parm = "Rotated Right"
              Case 2 : parm = "Rotated Left"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "Camera Orientation:" & tb & parm & "\par "
            parm = canonDescr(1, word(b, 111, intel))
            If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "
            note = note & "Color Temperature:" & tb & word(b, 115, intel) & "\par "
            parm = canonDescr(2, b(167))
            If Len(parm) > 0 Then note = note & "Picture Style:" & tb & parm & "\par "
            Select Case b(189)
              Case 0 : parm = "Standard"
              Case 1 : parm = "Low"
              Case 2 : parm = "Strong"
              Case 3 : parm = "Off"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "High ISO Noise Reduction:" & tb & parm & "\par "
            Select Case b(191)
              Case 0 : parm = "Standard"
              Case 1 : parm = "Low"
              Case 2 : parm = "Strong"
              Case 3 : parm = "Off"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "Auto Lighting Optimizer:" & tb & parm & "\par "
            note = note & "Lens Type:" & tb & canonDescr(4, b(235)) & "\par "
            parm = UTF8bare.GetString(b, 346, 5)
            If AscW(parm.Chars(0)) < 32 Then parm = ""
            If InStr(parm, ChrW(0)) > 0 Then parm = Mid(parm, 1, InStr(parm, ChrW(0)) - 1)
            If Len(parm) > 0 Then note = note & "Firmware Version:" & tb & parm & "\par " : k = 0
            parm = UTF8bare.GetString(b, 350, 5)
            If AscW(parm.Chars(0)) < 32 Then parm = ""
            If InStr(parm, ChrW(0)) > 0 Then parm = Mid(parm, 1, InStr(parm, ChrW(0)) - 1)
            If Len(parm) > 0 Then note = note & "Firmware Version:" & tb & parm & "\par " : k = 4
            x = DWord(b, 407 + k, intel)
            If x > 0 Then note = note & "File Index:" & tb & Format(x, "###,###,###") & "\par "
            x = DWord(b, 419 + k, intel)
            If x > 0 Then note = note & "Directory Index:" & tb & Format(x, "###,###,###") & "\par "
          End If ' EOS 50D

        Case &H176, &H254  ' EOS Digital Rebel XSi / 450D / Kiss X2,  EOS Rebel XS / 1000D / Kiss F
          If UBound(v) >= 382 Then
            If canonModel And &HFFFF = &H176 Then
              note = note & "{\b Canon EOS 450D information\b0 }" & "\par "
            Else
              note = note & "{\b Canon EOS 1000D information\b0 }" & "\par "
            End If
            x = Exp((b(3) - 8) / 16 * Log(2))
            note = note & "F-Number:" & tb & Format(x, "#,##0.0") & "\par "
            x = Exp(4 * Log(2) * (1 - (b(4) - 24) / 32))
            If x > 0.00000000001 Then
              If x < 1 And x <> 0 Then
                parm = Format(x, "##0.0###") & " (1/" & Format(1 / x, "##,###") & ") sec."
              Else
                parm = Format(x, "##0.0") & " sec."
              End If
              note = note & "Exposure Time:" & tb & parm & "\par "
            End If
            x = 100 * Exp((b(6) / 8 - 9) * Log(2))
            note = note & "ISO:" & tb & Format(x, "######") & "\par "
            note = note & "Camera Temperature:" & tb & b(24) & "\par "
            note = note & "Focal Length:" & tb & Format(word(b, 29, intel), "###,###,##0") & " mm\par "
            x = word(b, 67, intel) / 100
            If x > 0 Then note = note & "Upper Focus Distance:" & tb & Format(x, "###,##0.##") & " m\par "
            x = word(b, 69, intel) / 100
            If x > 0 Then note = note & "Lower Focus Distance:" & tb & Format(x, "###,##0.##") & " m\par "
            Select Case b(48)
              Case 0 : parm = "Upright"
              Case 1 : parm = "Rotated Right"
              Case 2 : parm = "Rotated Left"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "Camera Orientation:" & tb & parm & "\par "
            parm = canonDescr(1, word(b, 111, intel))
            If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "
            note = note & "Color Temperature:" & tb & word(b, 115, intel) & "\par "
            note = note & "Lens Type:" & tb & canonDescr(4, b(227)) & "\par "
            parm = UTF8bare.GetString(b, 267, 5)
            If AscW(parm.Chars(0)) < 32 Then parm = ""
            If InStr(parm, ChrW(0)) > 0 Then parm = Mid(parm, 1, InStr(parm, ChrW(0)) - 1)
            If Len(parm) > 0 Then note = note & "Firmware Version:" & tb & parm & "\par " : k = 0
            x = DWord(b, 311, intel)
            If x > 0 Then note = note & "Directory Index:" & tb & Format(x, "###,###,###") & "\par "
            x = DWord(b, 323, intel)
            If x > 0 Then note = note & "File Index:" & tb & Format(x, "###,###,###") & "\par "
            parm = UTF8bare.GetString(b, 2359, 64)
            If InStr(parm, ChrW(0)) > 0 Then parm = Mid(parm, 1, InStr(parm, ChrW(0)) - 1)
            If Len(parm) > 0 Then note = note & "Lens Model:" & tb & parm & "\par "
          End If ' EOS 450D, 1000D

        Case &H250 ' canon 7D
          If UBound(v) >= 503 Then
            note = note & "{\b Canon EOS 7D information\b0 }" & "\par "
            x = Exp((b(3) - 8) / 16 * Log(2))
            note = note & "F-Number:" & tb & Format(x, "#,##0.0") & "\par "
            x = Exp(4 * Log(2) * (1 - (b(4) - 24) / 32))
            If x > 0.00000000001 Then
              If x < 1 And x <> 0 Then
                parm = Format(x, "##0.0###") & " (1/" & Format(1 / x, "##,###") & ") sec."
              Else
                parm = Format(x, "##0.0") & " sec."
              End If
              note = note & "Exposure Time:" & tb & parm & "\par "
            End If
            x = 100 * Exp((b(6) / 8 - 9) * Log(2))
            note = note & "ISO:" & tb & Format(x, "######") & "\par "
            If b(7) = 0 Then parm = "Off" Else If b(7) = 1 Then parm = "On" Else parm = ""
            If Len(parm) > 0 Then note = note & "Highlight Tone Priority:" & tb & parm & "\par "
            Select Case b(21)
              Case 0 : parm = "E-TTL"
              Case 3 : parm = "TTL"
              Case 4 : parm = "External Auto"
              Case 5 : parm = "External Manual"
              Case 6 : parm = "Off"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "Flash Metering Mode:" & tb & parm & "\par "
            note = note & "Camera Temperature:" & tb & b(25) & "\par "
            note = note & "Focal Length:" & tb & Format(word(b, 30, intel), "###,###,###") & " mm\par "

            If InStr(canonFirmware, "3.7.5") > 0 Then k = 32 Else k = 36
            Select Case b(k + 17)
              Case 0 : parm = "Horizontal"
              Case 1 : parm = "90 Degrees Right"
              Case 2 : parm = "90 Degrees Left"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "Camera Orientation:" & tb & parm & "\par "
            i = word(b, k + 48, intel)
            If i > 0 Then note = note & "Upper Focus Distance:" & tb & Format(i, "###,##0.##") / 100 & " m\par "
            i = word(b, k + 50, intel)
            If i > 0 Then note = note & "Lower Focus Distance:" & tb & Format(i, "###,##0.##") / 100 & " m\par "
            note = note & "Short Focal:" & tb & Format(word(b, k + 240, intel), "###,###,###") & " mm\par "
            note = note & "Long Focal:" & tb & Format(word(b, k + 242, intel), "###,###,###") & " mm\par "
            parm = canonDescr(1, word(b, k + 83, intel))
            If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "

            note = note & "Color Temperature:" & tb & word(b, k + 87, intel) & "\par "
            Select Case b(k + 165)
              Case 0 : parm = "Standard"
              Case 1 : parm = "Low"
              Case 2 : parm = "Strong"
              Case 3 : parm = "Off"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "High ISO Noise Reduction:" & tb & parm & "\par "
            note = note & "Lens Type:" & tb & canonDescr(4, b(k + 239)) & "\par "
            If InStr(parm, ChrW(0)) > 0 Then parm = Mid(parm, 1, InStr(parm, ChrW(0)) - 1)
            If Len(parm) > 0 Then note = note & "Firmware Version:" & tb & parm & "\par " : k = 0
            parm = UTF8bare.GetString(b, k + 392, 35)
            If AscW(parm.Chars(0)) < 32 Then parm = ""
            If InStr(parm, ChrW(0)) > 0 Then parm = Mid(parm, 1, InStr(parm, ChrW(0)) - 1)
            If Len(parm) > 0 Then note = note & "Firmware Version:" & tb & parm & "\par " : k = 0
            x = DWord(b, k + 455 + k, intel)
            If x > 0 Then note = note & "File Index:" & tb & Format(x, "###,###,###") & "\par "
            x = DWord(b, k + 467 + k, intel)
            If x > 0 Then note = note & "Directory Index:" & tb & Format(x, "###,###,###") & "\par "
          End If ' EOS 7D

        Case &H252 ' canon 500D
          If UBound(v) >= 482 Then
            note = note & "{\b Canon EOS 500D information\b0 }" & "\par "
            x = Exp((b(3) - 8) / 16 * Log(2))
            note = note & "F-Number:" & tb & Format(x, "#,##0.0") & "\par "
            x = Exp(4 * Log(2) * (1 - (b(4) - 24) / 32))
            If x > 0.00000000001 Then
              If x < 1 And x <> 0 Then
                parm = Format(x, "##0.0###") & " (1/" & Format(1 / x, "##,###") & ") sec."
              Else
                parm = Format(x, "##0.0") & " sec."
              End If
              note = note & "Exposure Time:" & tb & parm & "\par "
            End If
            x = 100 * Exp((b(6) / 8 - 9) * Log(2))
            note = note & "ISO:" & tb & Format(x, "######") & "\par "
            If b(7) = 0 Then parm = "Off" Else If b(7) = 1 Then parm = "On" Else parm = ""
            If Len(parm) > 0 Then note = note & "Highlight Tone Priority:" & tb & parm & "\par "
            Select Case b(21)
              Case 0 : parm = "E-TTL"
              Case 3 : parm = "TTL"
              Case 4 : parm = "External Auto"
              Case 5 : parm = "External Manual"
              Case 6 : parm = "Off"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "Flash Metering Mode:" & tb & parm & "\par "
            note = note & "Camera Temperature:" & tb & b(25) & "\par "
            note = note & "Focal Length:" & tb & Format(word(b, 30, intel), "###,###,###") & " mm\par "
            Select Case b(49)
              Case 0 : parm = "Horizontal"
              Case 1 : parm = "90 Degrees Right"
              Case 2 : parm = "90 Degrees Left"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "Camera Orientation:" & tb & parm & "\par "
            i = word(b, 80, intel)
            If i > 0 Then note = note & "Upper Focus Distance:" & tb & Format(i, "###,##0.##") / 100 & " m\par "
            i = word(b, 82, intel)
            If i > 0 Then note = note & "Lower Focus Distance:" & tb & Format(i, "###,##0.##") / 100 & " m\par "
            note = note & "Short Focal:" & tb & Format(word(b, 248, intel), "###,###,###") & " mm\par "
            note = note & "Long Focal:" & tb & Format(word(b, 250, intel), "###,###,###") & " mm\par "
            parm = canonDescr(1, word(b, 115, intel))
            If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "
            note = note & "Color Temperature:" & tb & word(b, 119, intel) & "\par "
            parm = canonDescr(2, b(171))
            If Len(parm) > 0 Then note = note & "Picture Style:" & tb & parm & "\par "
            Select Case b(188)
              Case 0 : parm = "Standard"
              Case 1 : parm = "Low"
              Case 2 : parm = "Strong"
              Case 3 : parm = "Off"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "High ISO Noise Reduction:" & tb & parm & "\par "
            Select Case b(190)
              Case 0 : parm = "Standard"
              Case 1 : parm = "Low"
              Case 2 : parm = "Strong"
              Case 3 : parm = "Off"
              Case Else : parm = ""
            End Select
            If Len(parm) > 0 Then note = note & "Auto Lighting Optimizer:" & tb & parm & "\par "
            note = note & "Lens Type:" & tb & canonDescr(4, b(247)) & "\par "
            parm = UTF8bare.GetString(b, 400, 35)
            If AscW(parm.Chars(0)) < 32 Then parm = ""
            If InStr(parm, ChrW(0)) > 0 Then parm = Mid(parm, 1, InStr(parm, ChrW(0)) - 1)
            If Len(parm) > 0 Then note = note & "Firmware Version:" & tb & parm & "\par " : k = 0
            x = DWord(b, k + 467 + k, intel)
            If x > 0 Then note = note & "File Index:" & tb & Format(x, "###,###,###") & "\par "
            x = DWord(b, k + 479 + k, intel)
            If x > 0 Then note = note & "Directory Index:" & tb & Format(x, "###,###,###") & "\par "
          End If ' EOS 500D

      End Select

    End If

    If makerTags.Contains(sTag(&H95)) Then
      parm = makerTags.Item(sTag(&H95)).singleValue
      If InStr(parm, ChrW(0)) > 0 Then parm = Mid(parm, 1, InStr(parm, ChrW(0)) - 1)
      If Trim(parm) <> "" Then note = note & "Lens Model:" & tb & Trim(parm) & "\par "
    End If ' tag 95 for Canon

  End Sub ' canonMakernote

  Sub nikonMakernote(ByRef makerTags As Collection, ByRef note As String, ByVal intel As Boolean)

    Dim i, k As Integer
    Dim parm As String = ""
    Dim v As Object
    Dim x As Double

    If makerTags.Contains(sTag(1)) Then
      v = makerTags.Item(sTag(1)).Value
      k = UBound(v)
      parm = ""
      For i = 0 To k
        If v(i) >= 32 Then
          parm = parm & ChrW(v(i))
        Else
          parm = parm & v(i)
        End If
      Next i
      If Trim(parm) <> "" Then note = note & "Makernote Version:" & tb & Trim(parm) & "\par "
    End If ' tag 1 for nikon

    If makerTags.Contains(sTag(15)) Then
      parm = makerTags.Item(sTag(15)).singleValue
      If Trim(parm) <> "" Then note = note & "ISO Selection:" & tb & Trim(parm) & "\par "
    End If ' tag 15 for nikon

    If makerTags.Contains(sTag(19)) Then
      v = makerTags.Item(sTag(19)).Value
      If IsArray(v) AndAlso UBound(v) >= 1 Then
        note = note & "ISO Setting:" & tb
        For i = 0 To 1
          If IsNumeric(v(i)) Then note = note & Format(v(i), " ##,##0")
        Next i
        note = note & "\par "
      End If
    End If ' tag 19 for nikon

    If makerTags.Contains(sTag(2)) Then
      v = makerTags.Item(sTag(2)).Value
      i = uuBound(v)
      If i >= 0 Then
        k = v(i)
      Else
        k = v
      End If
      If k > 0 Then note = note & "ISO:" & tb & k & "\par "
    End If

    If makerTags.Contains(sTag(3)) Then
      parm = makerTags.Item(sTag(3)).singleValue
      If Trim(parm) <> "" Then note = note & "Color Mode:" & tb & Trim(parm) & "\par "
    End If

    If makerTags.Contains(sTag(4)) Then
      parm = makerTags.Item(sTag(4)).singleValue
      If Trim(parm) <> "" Then note = note & "Quality:" & tb & Trim(parm) & "\par "
    End If ' tag 4 for nikon

    If makerTags.Contains(sTag(5)) Then
      parm = makerTags.Item(sTag(5)).singleValue
      If Trim(parm) <> "" Then note = note & "White Balance:" & tb & Trim(parm) & "\par "
    End If ' tag 5 for nikon

    If makerTags.Contains(sTag(6)) Then
      parm = makerTags.Item(sTag(6)).singleValue
      If Trim(parm) <> "" Then note = note & "Image Sharpening:" & tb & Trim(parm) & "\par "
    End If ' tag 6 for nikon

    If makerTags.Contains(sTag(7)) Then
      parm = makerTags.Item(sTag(7)).singleValue
      If Trim(parm) <> "" Then note = note & "Focus Mode:" & tb & Trim(parm) & "\par "
    End If ' tag 7 for nikon

    If makerTags.Contains(sTag(8)) Then
      parm = makerTags.Item(sTag(8)).singleValue
      If Trim(parm) <> "" Then note = note & "Flash Setting:" & tb & Trim(parm) & "\par "
    End If ' tag 8 for nikon

    If makerTags.Contains(sTag(9)) Then
      parm = makerTags.Item(sTag(9)).singleValue
      If Trim(parm) <> "" Then note = note & "Flash Type:" & tb & Trim(parm) & "\par "
    End If ' tag 9 for nikon

    If makerTags.Contains(sTag(11)) Then
      x = makerTags.Item(sTag(11)).singleValue
      note = note & "White Balance Fine Tune:" & tb & Format(x, "###,##0") & "\par "
    End If ' tag 11 for nikon

    If makerTags.Contains(sTag(12)) Then
      v = makerTags.Item(sTag(12)).Value
      If IsArray(v) AndAlso UBound(v) >= 3 Then
        note = note & "Color Balance 1:" & tb
        For i = 0 To 3
          If IsNumeric(v(i)) Then note = note & Format(v(i), " #0.0###")
        Next i
        note = note & "\par "
      End If
    End If ' tag 12 for nikon

    If makerTags.Contains(sTag(13)) Then
      v = makerTags.Item(sTag(13)).Value
      k = UBound(v)
      parm = ""
      For i = 0 To k
        parm = parm & v(i)
      Next i
      If Trim(parm) <> "" Then note = note & "Program Shift:" & tb & Trim(parm) & "\par "
    End If ' tag 13 for nikon

    If makerTags.Contains(sTag(14)) Then
      v = makerTags.Item(sTag(14)).Value
      k = UBound(v)
      parm = ""
      For i = 0 To k
        parm = parm & v(i)
      Next i
      If Trim(parm) <> "" Then note = note & "Exposure Difference:" & tb & Trim(parm) & "\par "
    End If ' tag 14 for nikon

    If makerTags.Contains(sTag(18)) Then
      v = makerTags.Item(sTag(18)).Value
      k = UBound(v)
      parm = ""
      For i = 0 To k
        parm = parm & v(i)
      Next i
      If Trim(parm) <> "" Then note = note & "Flash Exposure Compensation:" & tb & Trim(parm) & "\par "
    End If ' tag 18 for nikon

    If makerTags.Contains(sTag(22)) Then
      v = makerTags.Item(sTag(22)).Value
      If IsArray(v) AndAlso UBound(v) >= 3 Then
        note = note & "Image Boundary:" & tb
        For i = 0 To 3
          If IsNumeric(v(i)) Then note = note & Format(v(i), " ###,##0")
        Next i
        note = note & "\par "
      End If
    End If ' tag 22 for nikon

    If makerTags.Contains(sTag(23)) Then
      v = makerTags.Item(sTag(23)).Value
      k = UBound(v)
      parm = ""
      For i = 0 To k
        parm = parm & v(i)
      Next i
      If Trim(parm) <> "" Then note = note & "Flash Exposure Bracket:" & tb & Trim(parm) & "\par "
    End If ' tag 23 for nikon

    If makerTags.Contains(sTag(24)) Then
      v = makerTags.Item(sTag(24)).Value
      k = UBound(v)
      parm = ""
      For i = 0 To k
        parm = parm & " " & v(i)
      Next i
      If Trim(parm) <> "" Then note = note & "Flash Exposure Bracket:" & tb & Trim(parm) & "\par "
    End If ' tag 24 for nikon

    If makerTags.Contains(sTag(25)) Then
      x = makerTags.Item(sTag(25)).singleValue
      note = note & "Exposure Bracket Value:" & tb & Format(x, "###,##0.####") & "\par "
    End If ' tag 25 for nikon

    If makerTags.Contains(sTag(26)) Then
      parm = makerTags.Item(sTag(26)).singleValue
      If Trim(parm) <> "" Then note = note & "Image Processing:" & tb & Trim(parm) & "\par "
    End If ' tag 26 for nikon

    If makerTags.Contains(sTag(27)) Then
      v = makerTags.Item(sTag(27)).Value
      If IsArray(v) Then
        note = note & "Crop High Speed:" & tb
        For i = 0 To UBound(v)
          If IsNumeric(v(i)) Then note = note & Format(v(i), " ###,##0")
        Next i
        note = note & "\par "
      End If
    End If ' tag 27 for nikon

    If makerTags.Contains(sTag(29)) Then ' used for decryption
      parm = makerTags.Item(sTag(29)).singleValue
      note = note & "Internal Serial Number:" & tb & parm & "\par "
    End If ' tag 29 for nikon

    If makerTags.Contains(sTag(30)) Then
      v = makerTags.Item(sTag(30)).singleValue
      If v = 2 Then parm = "Adobe RGB" Else parm = "sRGB"
      If IsNumeric(v) Then note = note & "Color Space:" & tb & parm & "\par "
    End If ' tag 30 for nikon

    If makerTags.Contains(sTag(31)) Then
      v = makerTags.Item(sTag(31)).Value
      If IsArray(v) AndAlso UBound(v) >= 4 Then
        parm = ""
        For i = 0 To 3 : parm = parm & ChrW(v(i)) : Next i
        note = note & "Vibration Reduction Version:" & tb & parm & "\par "
        Select Case v(4)
          Case 1 : parm = "on"
          Case 2 : parm = "off"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Vibration Reduction:" & tb & parm & "\par "
      End If
    End If ' tag 31 for nikon

    If makerTags.Contains(sTag(32)) Then
      v = makerTags.Item(sTag(32)).singleValue
      If v = 1 Then parm = "On" Else parm = "Off"
      If IsNumeric(v) Then note = note & "Image Authentication:" & tb & parm & "\par "
    End If ' tag 32 for nikon

    If makerTags.Contains(sTag(34)) Then
      v = makerTags.Item(sTag(34)).singleValue
      Select Case v
        Case 0 : parm = "off"
        Case 1 : parm = "low"
        Case 3 : parm = "normal"
        Case 5 : parm = "high"
        Case 7 : parm = "extra high"
        Case Else : parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Active D-Lighting:" & tb & parm & "\par "
    End If ' tag 34 for nikon

    If makerTags.Contains(sTag(35)) Then
      v = makerTags.Item(sTag(35)).Value
      If IsArray(v) AndAlso UBound(v) >= 57 Then
        parm = ""
        For i = 0 To 3 : parm = parm & ChrW(v(i)) : Next i
        If InStr(parm, ChrW(0)) > 0 Then parm = vb.Left(parm, InStr(parm, ChrW(0)) - 1)
        If Len(parm) > 0 Then note = note & "Picture Control Version:" & tb & parm & "\par "
        parm = ""
        For i = 4 To 23 : parm = parm & ChrW(v(i)) : Next i
        If InStr(parm, ChrW(0)) > 0 Then parm = vb.Left(parm, InStr(parm, ChrW(0)) - 1)
        If Len(parm) > 0 Then note = note & "Picture Control Name:" & tb & parm & "\par "
        For i = 24 To 43 : parm = parm & ChrW(v(i)) : Next i
        If InStr(parm, ChrW(0)) > 0 Then parm = vb.Left(parm, InStr(parm, ChrW(0)) - 1)
        If Len(parm) > 0 Then note = note & "Picture Control Base:" & tb & parm & "\par "
        Select Case v(48)
          Case 0 : parm = "Default Settings"
          Case 1 : parm = "Quick Adjust"
          Case 2 : parm = "Full Control"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Picture Control Adjust:" & tb & parm & "\par "
        note = note & "Picture Control Sharpness:" & tb & v(50) & "\par "
        note = note & "Picture Control Contrast:" & tb & v(51) & "\par "
        note = note & "Picture Control Brightness:" & tb & v(52) & "\par "
        note = note & "Picture Control Saturation:" & tb & v(53) & "\par "
        note = note & "Picture Control Hue Adjustment:" & tb & v(54) & "\par "
        Select Case v(48)
          Case 128 : parm = "Off"
          Case 129 : parm = "Yellow"
          Case 130 : parm = "Orange"
          Case 131 : parm = "Red"
          Case 132 : parm = "Green"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Filter Effect:" & tb & parm & "\par "
        Select Case v(48)
          Case 128 : parm = "Black and White"
          Case 129 : parm = "Sepia"
          Case 130 : parm = "Cyanotype"
          Case 131 : parm = "Red"
          Case 132 : parm = "Yellow"
          Case 133 : parm = "Green"
          Case 134 : parm = "Blue-Green"
          Case 135 : parm = "Blue"
          Case 136 : parm = "Purple-Blue"
          Case 137 : parm = "Red-Purple"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Toning Effect:" & tb & parm & "\par "
        note = note & "Picture Control Toning Saturation:" & tb & v(57) & "\par "
      End If
    End If ' Nikon tag 35

    If makerTags.Contains(sTag(36)) Then
      v = makerTags.Item(sTag(36)).Value
      If IsArray(v) AndAlso UBound(v) >= 3 Then
        note = note & "Time Zone:" & tb & word(v, 0, intel) & "\par "
        Select Case v(2)
          Case 0 : parm = "No"
          Case 1 : parm = "Yes"
        End Select
        If Len(parm) > 0 Then note = note & "Daylight Savings:" & tb & parm & "\par "
        Select Case v(3)
          Case 0 : parm = "Y/M/D"
          Case 1 : parm = "M/D/Y"
          Case 2 : parm = "D/M/Y"
        End Select
        If Len(parm) > 0 Then note = note & "Date Display Format:" & tb & parm & "\par "
      End If
    End If ' tag 36

    If makerTags.Contains(sTag(42)) Then
      v = makerTags.Item(sTag(42)).singleValue
      parm = ""
      Select Case v
        Case 0
          parm = "off"
        Case 1
          parm = "low"
        Case 3
          parm = "normal"
        Case 5
          parm = "high"
      End Select
      If Trim(parm) <> "" Then note = note & "Vignette Control:" & tb & parm & "\par "
    End If ' tag 42 for nikon

    If makerTags.Contains(sTag(128)) Then
      parm = makerTags.Item(sTag(128)).singleValue
      If Trim(parm) <> "" Then note = note & "Image Adjustment:" & tb & Trim(parm) & "\par "
    End If ' tag 128 for nikon

    If makerTags.Contains(sTag(129)) Then
      parm = makerTags.Item(sTag(129)).singleValue
      If Trim(parm) <> "" Then note = note & "Tone Component:" & tb & Trim(parm) & "\par "
    End If ' tag 129 for nikon

    If makerTags.Contains(sTag(130)) Then
      parm = makerTags.Item(sTag(130)).singleValue
      If Trim(parm) <> "" Then note = note & "Auxiliary Lens:" & tb & Trim(parm) & "\par "
    End If ' tag 130 for nikon

    If makerTags.Contains(sTag(131)) Then
      i = makerTags.Item(sTag(131)).singleValue
      parm = ""
      If i And 8 Then parm = parm & "VR "
      If i And 4 Then parm = parm & "G "
      If i And 2 Then parm = parm & "D "
      If i And 1 Then parm = parm & "MF "
      If Trim(parm) <> "" Then note = note & "Lens Type:" & tb & Trim(parm) & "\par "
    End If ' tag 131 for nikon

    If makerTags.Contains(sTag(132)) Then
      v = makerTags.Item(sTag(132)).Value
      If IsArray(v) AndAlso UBound(v) >= 3 Then
        note = note & "Lens Minimum Focal Length:" & tb & Format(v(0), " ####0") & " mm\par "
        note = note & "Maximum Aperture at " & Format(v(0), " ####0") & " mm:" & tb & Format(v(2), " ####0.##") & "\par "
        note = note & "Lens Maximum Focal Length:" & tb & Format(v(1), " ####0") & " mm\par "
        note = note & "Maximum Aperture at " & Format(v(1), " ####0") & " mm:" & tb & Format(v(3), " ####0.##") & "\par "
      End If
    End If ' tag 132 for nikon

    If makerTags.Contains(sTag(133)) Then
      x = makerTags.Item(sTag(133)).singleValue
      If x <> 0 Then note = note & "Manual Focus Distance:" & tb & Format(x, "###,##0") & "\par "
    End If ' tag 133 for nikon

    If makerTags.Contains(sTag(134)) Then
      x = makerTags.Item(sTag(134)).singleValue
      note = note & "Digital Zoom:" & tb & Format(x, "##0.0#") & "\par "
    End If ' tag 134 for nikon

    If makerTags.Contains(sTag(135)) Then
      v = makerTags.Item(sTag(135)).singleValue
      Select Case v
        Case 0 : parm = "Did Not Fire"
        Case 1 : parm = "Fired, Manual"
        Case 7 : parm = "Fired, External"
        Case 8 : parm = "Fired, Commander Mode"
        Case 9 : parm = "Fired, TTL Mode"
        Case Else : parm = ""
      End Select
      If Trim(parm) <> "" Then note = note & "Flash Mode:" & tb & parm & "\par "
    End If ' tag 135 for nikon

    If makerTags.Contains(sTag(136)) Then
      v = makerTags.Item(sTag(136)).Value
      If IsArray(v) And UBound(v) >= 3 Then
        Select Case v(0)
          Case 0 : parm = "Single Area"
          Case 1 : parm = "Dynamic Area"
          Case 2 : parm = "Dynamic Area (Closest Subject)"
          Case 3 : parm = "Group Dynamic"
          Case 4 : parm = "Single Area (Wide)"
          Case 5 : parm = "Dynamic Area (Wide)"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Autofocus Area Mode:" & tb & Trim(parm) & "\par "
        Select Case v(1)
          Case 0 : parm = "Center"
          Case 1 : parm = "Top"
          Case 2 : parm = "Bottom"
          Case 3 : parm = "Mid-left"
          Case 4 : parm = "Mid-right"
          Case 5 : parm = "Upper-left"
          Case 6 : parm = "Upper-right"
          Case 7 : parm = "Lower-left"
          Case 8 : parm = "Lower-right"
          Case 9 : parm = "Far-left"
          Case 10 : parm = "Far-right"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Autofocus Point:" & tb & Trim(parm) & "\par "
        i = word(v, 2, intel)
        If i And 2 ^ 0 Then note = note & "Point in Focus:" & tb & "Center" & "\par "
        If i And 2 ^ 1 Then note = note & "Point in Focus:" & tb & "Top" & "\par "
        If i And 2 ^ 2 Then note = note & "Point in Focus:" & tb & "Bottom" & "\par "
        If i And 2 ^ 3 Then note = note & "Point in Focus:" & tb & "Mid-left" & "\par "
        If i And 2 ^ 4 Then note = note & "Point in Focus:" & tb & "Mid-right" & "\par "
        If i And 2 ^ 5 Then note = note & "Point in Focus:" & tb & "Upper-left" & "\par "
        If i And 2 ^ 6 Then note = note & "Point in Focus:" & tb & "Upper-right" & "\par "
        If i And 2 ^ 7 Then note = note & "Point in Focus:" & tb & "Lower-left" & "\par "
        If i And 2 ^ 8 Then note = note & "Point in Focus:" & tb & "Lower-right" & "\par "
        If i And 2 ^ 9 Then note = note & "Point in Focus:" & tb & "Far-left" & "\par "
        If i And 2 ^ 10 Then note = note & "Point in Focus:" & tb & "Far-right" & "\par "
      End If
    End If ' tag 136 for nikon

    If makerTags.Contains(sTag(137)) Then
      i = makerTags.Item(sTag(137)).singleValue
      parm = ""
      If i And 64 Then parm = parm & "IR Control "
      If i And 32 Then parm = parm & "White Balance Bracketing "
      If i And 16 Then parm = parm & "Auto ISO "
      If i And 8 Then parm = parm & "Exposure Bracketing "
      If i And 4 Then parm = parm & "PC Control "
      If i And 2 Then parm = parm & "Delay "
      If i And 1 Then parm = parm & "Continuous "
      If Trim(parm) <> "" Then note = note & "Shooting Mode:" & tb & Trim(parm) & "\par "
    End If ' tag 137 for nikon

    If makerTags.Contains(sTag(138)) Then
      v = makerTags.Item(sTag(138)).singleValue
      parm = ""
      Select Case v
        Case 0
          parm = "(none)"
        Case 1
          parm = "Auto Release"
        Case 2
          parm = "Manual Release"
      End Select
      If Trim(parm) <> "" Then note = note & "Autobracket Release:" & tb & parm & "\par "
    End If ' tag 138 for nikon

    If makerTags.Contains(sTag(139)) Then
      v = makerTags.Item(sTag(139)).Value
      If IsArray(v) Then
        k = UBound(v)
        parm = ""
        For i = 0 To k
          parm = parm & " " & v(i)
        Next i
        If Trim(parm) <> "" Then note = note & "Lens F-Stops:" & tb & Trim(parm) & "\par "
      End If
    End If ' tag 139 for nikon

    If makerTags.Contains(sTag(141)) Then
      parm = makerTags.Item(sTag(141)).singleValue
      If Trim(parm) <> "" Then note = note & "Color Hue:" & tb & Trim(parm) & "\par "
    End If ' tag 141 for nikon

    If makerTags.Contains(sTag(143)) Then
      parm = makerTags.Item(sTag(143)).singleValue
      If Trim(parm) <> "" Then note = note & "Scene Mode:" & tb & Trim(parm) & "\par "
    End If ' tag 143 for nikon

    If makerTags.Contains(sTag(144)) Then
      parm = makerTags.Item(sTag(144)).singleValue
      If Trim(parm) <> "" Then note = note & "Light Source:" & tb & Trim(parm) & "\par "
    End If ' tag 144 for nikon

    If makerTags.Contains(sTag(146)) Then
      x = makerTags.Item(sTag(146)).singleValue
      note = note & "Hue Adjustment:" & tb & Format(x, "###,##0.0###") & " degrees\par "
    End If ' tag 146 for nikon

    If makerTags.Contains(sTag(147)) Then
      v = makerTags.Item(sTag(147)).singleValue
      parm = ""
      Select Case v
        Case 1
          parm = "Lossy (type 1)"
        Case 2
          parm = "Uncompressed"
        Case 3
          parm = "Lossless"
        Case 4
          parm = "Lossy (type 2)"
      End Select
      If Trim(parm) <> "" Then note = note & "NEF Compression:" & tb & parm & "\par "
    End If ' tag 147 for nikon

    If makerTags.Contains(sTag(148)) Then
      x = makerTags.Item(sTag(148)).singleValue
      note = note & "Saturation:" & tb & Format(x, "###,##0.####") & "\par "
    End If ' tag 148 for nikon

    If makerTags.Contains(sTag(149)) Then
      parm = makerTags.Item(sTag(149)).singleValue
      If Trim(parm) <> "" Then note = note & "Noise Reduction:" & tb & Trim(parm) & "\par "
    End If ' tag 149 for nikon

    If makerTags.Contains(sTag(154)) Then
      x = makerTags.Item(sTag(154)).singleValue
      note = note & "Sensor Pixel Size:" & tb & Format(x, "###,##0.####") & "\par "
    End If ' tag 154 for nikon

    If makerTags.Contains(sTag(156)) Then
      parm = makerTags.Item(sTag(156)).singleValue
      If Trim(parm) <> "" Then note = note & "Scene Assist:" & tb & Trim(parm) & "\par "
    End If ' tag 156 for nikon

    If makerTags.Contains(sTag(158)) Then
      v = makerTags.Item(sTag(158)).Value
      If IsArray(v) Then
        parm = ""
        For i = 0 To UBound(v)
          Select Case v(i)
            Case 3
              parm = parm & " B & W"
            Case 4
              parm = parm & " Sepia"
            Case 5
              parm = parm & " Trim"
            Case 6
              parm = parm & " Small Picture"
            Case 7
              parm = parm & " D-Lighting"
            Case 8
              parm = parm & " Red-Eye"
            Case 9
              parm = parm & " Cyanotype"
            Case 10
              parm = parm & " SkyLight"
            Case 11
              parm = parm & " WarmTone"
            Case 12
              parm = parm & " ColorCustom"
            Case 13
              parm = parm & " ImageOverlay"
            Case 14
              parm = parm & " Red Intensifier"
            Case 15
              parm = parm & " Green Intensifier"
            Case 16
              parm = parm & " Blue Intensifier"
            Case 17
              parm = parm & " Cross Screen"
            Case 18
              parm = parm & " Quick Retouch"
            Case 19
              parm = parm & " NEF Processing"
            Case 23
              parm = parm & " Distortion Control"
            Case 25
              parm = parm & " Fisheye"
            Case 26
              parm = parm & " Straighten"
            Case 29
              parm = parm & " Perspective Control"
            Case 30
              parm = parm & " Color Outline"
            Case 31
              parm = parm & " Soft Filter"
            Case 33
              parm = parm & " Miniature Effect"
            Case Else
          End Select
        Next i
        If Trim(parm) <> "" Then note = note & "Retouch History:" & tb & parm & "\par "
      End If
    End If ' tag 158 for nikon

    If makerTags.Contains(sTag(160)) Then
      parm = makerTags.Item(sTag(160)).singleValue
      If Trim(parm) <> "" Then note = note & "Serial Number:" & tb & Trim(parm) & "\par "
    End If ' tag 160 for nikon

    If makerTags.Contains(sTag(162)) Then
      x = makerTags.Item(sTag(162)).singleValue
      note = note & "Image Data Size:" & tb & Format(x, "######,##0") & "\par "
    End If ' tag 162 for nikon

    If makerTags.Contains(sTag(165)) Then
      x = makerTags.Item(sTag(165)).singleValue
      note = note & "Image Count:" & tb & Format(x, "######,##0") & "\par "
    End If ' tag 165 for nikon

    If makerTags.Contains(sTag(166)) Then
      x = makerTags.Item(sTag(166)).singleValue
      note = note & "Deleted Image Count:" & tb & Format(x, "######,##0") & "\par "
    End If ' tag 166 for nikon

    If makerTags.Contains(sTag(169)) Then
      v = makerTags.Item(sTag(169)).singleValue
      parm = v
      If Trim(parm) <> "" Then note = note & "Image Optimization:" & tb & parm & "\par "
    End If ' tag 169 for nikon

    If makerTags.Contains(sTag(170)) Then
      parm = makerTags.Item(sTag(170)).singleValue
      If Trim(parm) <> "" Then note = note & "Saturation:" & tb & Trim(parm) & "\par "
    End If ' tag 170 for nikon

    If makerTags.Contains(sTag(171)) Then
      parm = makerTags.Item(sTag(171)).singleValue
      If Trim(parm) <> "" Then note = note & "Vari Program:" & tb & Trim(parm) & "\par "
    End If ' tag 171 for nikon

    If makerTags.Contains(sTag(172)) Then
      parm = makerTags.Item(sTag(172)).singleValue
      If Trim(parm) <> "" Then note = note & "Image Stabilization:" & tb & Trim(parm) & "\par "
    End If ' tag 172 for nikon

    If makerTags.Contains(sTag(173)) Then
      parm = makerTags.Item(sTag(173)).singleValue
      If Trim(parm) <> "" Then note = note & "Autofocus Response:" & tb & Trim(parm) & "\par "
    End If ' tag 173 for nikon

    If makerTags.Contains(sTag(177)) Then
      v = makerTags.Item(sTag(177)).singleValue
      parm = ""
      Select Case v
        Case 0
          parm = "Off"
        Case 1
          parm = "Minimal"
        Case 2
          parm = "Low"
        Case 4
          parm = "Normal"
        Case 6
          parm = "High"
      End Select
      If Trim(parm) <> "" Then note = note & "High ISO Noise Reduction:" & tb & parm & "\par "
    End If ' tag 177 for nikon

    If makerTags.Contains(sTag(179)) Then
      parm = makerTags.Item(sTag(179)).singleValue
      If Trim(parm) <> "" Then note = note & "Toning Effect:" & tb & Trim(parm) & "\par "
    End If ' tag 179 for nikon

  End Sub ' nikonMakernote

  Sub minoltaMakernote(ByRef makerTags As Collection, ByRef note As String, ByVal model As String)

    Dim i, k As Integer
    Dim parm As String = ""
    Dim v As Object
    Dim x As Double

    Select Case LCase(model)
      Case "dimage s404", "dimage 5", "dimage 7"
        i = 1
      Case "dimage a1", "dimage 7hi", "dimage 7i", "dimage s414"
        i = 3
      Case Else
        i = 3 ' new models?
    End Select

    If makerTags.Contains(sTag(i)) Then
      v = makerTags.Item(sTag(i)).Value
      ' v has a byte array, but the values are 4-byte numbers all intel=false.
      ' a bunch of 4-byte values is in this tag.
      i = DWord(v, 1 * 4, False) ' 2 = Exposure mode
      Select Case i
        Case 0 : parm = "Program"
        Case 1 : parm = "Aperture Priority"
        Case 2 : parm = "Shutter Priority"
        Case 3 : parm = "Manual"
        Case 4 : parm = "Auto"
        Case 5 : parm = "Program Shift A"
        Case 6 : parm = "Program Shift S"
        Case Else : parm = CStr(i)
      End Select
      note = note & "Exposure Mode:" & tb & parm & "\par "

      i = DWord(v, 2 * 4, False) '  Flash mode
      Select Case i
        Case 0 : parm = "Fill-Flash"
        Case 1 : parm = "Red-Eye Reduction"
        Case 2 : parm = "Rear Flash Sync"
        Case 3 : parm = "Wireless"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Flash Mode:" & tb & parm & "\par "

      Select Case i
        Case 0, &H800000 : parm = "Auto"
        Case 1, &H1800000 : parm = "Daylight"
        Case 2, &H2800000 : parm = "Cloudy"
        Case 3, &H3800000 : parm = "Tungsten"
        Case 4, &H4800000 : parm = "Flash"
        Case 5, &H7800000 : parm = "Custom 1"
        Case 6, &H6800000 : parm = "Shadow"
        Case 7, &H5800000 : parm = "Fluorescent"
        Case 8 : parm = "Fluorescent 2"
        Case 11, &H8800000 : parm = "Custom 2"
        Case 12, &H9800000 : parm = "Custom 3"
        Case Else : parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "

      i = DWord(v, 4 * 4, False) '  Image Size
      Select Case i
        Case 0
          If InStr(LCase(model), "d5") > 0 Or eqstr(model, "dimage s304") Then parm = "2048x1535" Else parm = "2560x1920"
        Case 1 : parm = "1600x1200"
        Case 2 : parm = "1280x960"
        Case 3 : parm = "640x480"
        Case 6 : parm = "2080x1560"
        Case 7 : parm = "2560x1920"
        Case 8 : parm = "3264x2176"
        Case Else : parm = CStr(i)
      End Select
      note = note & "Image Size:" & tb & parm & "\par "

      i = DWord(v, 5 * 4, False) '  Image Quality
      Select Case i
        Case 0 : parm = "Raw"
        Case 1 : parm = "Superfine"
        Case 2 : parm = "Fine"
        Case 3 : parm = "Standard"
        Case 4 : parm = "Economy"
        Case 5 : parm = "Extra Fine"
        Case Else : parm = CStr(i)
      End Select
      note = note & "Image Quality:" & tb & parm & "\par "

      i = DWord(v, 6 * 4, False) '  Drive Mode
      Select Case i
        Case 0 : parm = "Single"
        Case 1 : parm = "Continuous"
        Case 2 : parm = "Self Timer"
        Case 4 : parm = "Bracketing"
        Case 5 : parm = "Interval"
        Case 6 : parm = "UHS Continuous"
          parm = "HS Continuous"
        Case Else : parm = CStr(i)
      End Select
      note = note & "Drive Mode:" & tb & parm & "\par "

      i = DWord(v, 7 * 4, False) '  Metering Mode
      Select Case i
        Case 0 : parm = "Multisegment"
        Case 1 : parm = "Center Weighted"
        Case 2 : parm = "Spot"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Metering Mode:" & tb & parm & "\par "

      i = DWord(v, 8 * 4, False) '  Film Speed
      If Abs(i) <= 500 Then
        i = (2 ^ (i / 8 - 1)) * 3.125
        If eqstr(model, "dimage 7") Then i = (2 ^ (i / 8 - 1)) * 3.125
        note = note & "Film Speed:" & tb & "ISO " & i & "\par "
      End If

      i = DWord(v, 9 * 4, False) '  Shutter Speed
      If Abs(i) < 1000 Then x = 2 ^ ((48 - i) / 8) Else x = 0
      If x < 1 And x > 0 Then
        note = note & "Shutter Speed:" & tb & Format(x, "##0.0###") & " (1/" & Format(1 / x, "##,###") & ") sec." & "\par "
      Else
        note = note & "Shutter Speed:" & tb & Format(x, "##0.0") & " sec." & "\par "
      End If

      i = DWord(v, 10 * 4, False) '  Aperture Value
      If Abs(i) < 10000 Then
        x = 2 ^ (i / 16 - 0.5)
        note = note & "Aperture Value:" & tb & Format(x, "#0.0") & "\par "
      End If

      i = DWord(v, 11 * 4, False) '  Macro Mode
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Macro Mode:" & tb & parm & "\par "

      i = DWord(v, 12 * 4, False) '  Digital Zoom
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case 2
          parm = "2x"
        Case Else
          parm = "Unknown"
      End Select
      note = note & "Digital Zoom:" & tb & parm & "\par "

      i = DWord(v, 13 * 4, False) '  Exposure Compensation
      note = note & "Exposure Compensation:" & tb & Format(i / 3 - 2, "##0.0#") & "\par "

      i = DWord(v, 14 * 4, False) '  Bracket Step
      Select Case i
        Case 0
          parm = "1/3 EV"
        Case 1
          parm = "2/3 EV"
        Case 2
          parm = "1 EV"
        Case Else
          parm = "Unknown"
      End Select
      note = note & "Bracket Step:" & tb & parm & "\par "

      i = DWord(v, 16 * 4, False) '  Interval Length
      note = note & "Interval Length:" & tb & i + 1 & "\par "

      i = DWord(v, 17 * 4, False) '  Interval Number
      note = note & "Interval Number:" & tb & i & "\par "

      i = DWord(v, 18 * 4, False) '  Focal Length
      note = note & "Focal Length :" & tb & Format(i / 256, "###0.0") & " mm" & "\par "

      i = DWord(v, 19 * 4, False) '  Focus Distance
      If i <> 0 Then parm = Format(i / 1000, "####0.0##") & " m" Else parm = "Infinity"
      note = note & "Focus Distance:" & tb & parm & "\par "

      i = DWord(v, 20 * 4, False) '  Flash Fired
      If i = 1 Then parm = "Yes" Else parm = "No"
      note = note & "Flash Fired:" & tb & parm & "\par "

      i = word(v, 21 * 4, False) '  Date
      parm = v(21 * 4 + 2) & "/" & v(21 * 4 + 3) & "/" & i
      note = note & "Date:        " & tb & parm & "\par "

      i = word(v, 22 * 4, False) '  Time
      parm = Format(i, "00") & ":" & Format(v(22 * 4 + 2), "00") & ":" & Format(v(22 * 4 + 3), "00")
      note = note & "Time:        " & tb & parm & "\par "

      i = DWord(v, 23 * 4, False) '  Max Aperture
      If Abs(i) < 10000 Then
        note = note & "Max. Aperture:" & tb & Format(2 ^ (i / 16 - 0.5), "##0.0") & " mm" & "\par "
      End If

      i = DWord(v, 26 * 4, False) '  File Number Memory
      If i = 1 Then parm = "On" Else parm = "Off"
      note = note & "File Number Memory:" & tb & parm & "\par "

      If i = 1 Then
        i = DWord(v, 27 * 4, False) '  Last File Number
        note = note & "Last File Number:" & tb & i & "\par "
      End If

      i = DWord(v, 28 * 4, False) '  White Balance Red
      note = note & "White Balance Red:" & tb & Format(i / 256, "##0.0#") & "\par "
      i = DWord(v, 29 * 4, False) '  White Balance Green
      note = note & "White Balance Green:" & tb & Format(i / 256, "##0.0#") & "\par "
      i = DWord(v, 30 * 4, False) '  White Balance Blue
      note = note & "White Balance Blue:" & tb & Format(i / 256, "##0.0#") & "\par "

      i = DWord(v, 31 * 4, False) '  Saturation
      If eqstr(model, "dimage a1") Then k = 5 Else k = 3
      If i > k Then
        note = note & "Saturation (0=normal):" & tb & "+" & i - k & "\par "
      Else
        note = note & "Saturation (0=normal):" & tb & i - k & "\par "
      End If

      i = DWord(v, 32 * 4, False) '  Contrast
      If eqstr(model, "dimage a1") Then k = 5 Else k = 3
      If i > k Then
        note = note & "Contrast (0=normal):" & tb & "+" & i - k & "\par "
      Else
        note = note & "Contrast (0=normal):" & tb & i - k & "\par "
      End If

      i = DWord(v, 33 * 4, False) '  Sharpness
      Select Case i
        Case 0
          parm = "Hard"
        Case 1
          parm = "Normal"
        Case 2
          parm = "Soft"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Sharpness:" & tb & parm & "\par "

      i = DWord(v, 34 * 4, False) '  Subject Program
      Select Case i
        Case 0
          parm = "(none)"
        Case 1
          parm = "Portrait"
        Case 2
          parm = "Text"
        Case 3
          parm = "Night Portrait"
        Case 4
          parm = "Sunset"
        Case 5
          parm = "Sports Action"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Subject Program:" & tb & parm & "\par "

      i = DWord(v, 35 * 4, False) '  Flash Compensation
      If i >= -50 And i <= 50 Then note = note & "Flash Compensation (0-12):" & tb & (i - 6) / 3 & "\par "

      i = DWord(v, 36 * 4, False) '  ISO Setting
      Select Case i
        Case 0
          parm = "ISO 100"
        Case 1
          parm = "ISO 200"
        Case 2
          parm = "ISO 400"
        Case 3
          parm = "ISO 800"
        Case 4
          parm = "Auto"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "ISO Setting:" & tb & parm & "\par "

      i = DWord(v, 37 * 4, False) '  Camera Model
      Select Case i
        Case 0
          parm = "DiMage 7"
        Case 1
          parm = "DiMage 5"
        Case 2
          parm = "DiMage S304"
        Case 3
          parm = "DiMage S404"
        Case 4
          parm = "DiMage 7i"
        Case 5
          parm = "DiMage 7hi"
        Case 6
          parm = "DiMage A1"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Camera Model:" & tb & parm & "\par "

      i = DWord(v, 39 * 4, False) '  Folder Name
      If i = 1 Then parm = "Data Mode" Else parm = "Standard Mode"
      note = note & "Folder Name:" & tb & parm & "\par "

      i = DWord(v, 40 * 4, False) '  Color Mode
      Select Case i
        Case 0
          parm = "Natural Color"
        Case 1
          parm = "Black and White"
        Case 2
          parm = "Vivid Color"
        Case 3
          parm = "Solarization"
        Case 4
          parm = "Adobe RGB"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Color Mode:" & tb & parm & "\par "

      i = DWord(v, 41 * 4, False) '  Color Filter
      If eqstr(model, "dimage a1") Then k = 5 Else k = 3
      If i > k Then
        note = note & "Color Filter (0=normal):" & tb & "+" & i - k & "\par "
      Else
        note = note & "Color Filter (0=normal):" & tb & i - k & "\par "
      End If

      If UBound(v) >= 203 Then
        i = DWord(v, 42 * 4, False) '  Black and White Filter
        note = note & "Black and White Filter (0-10):" & tb & i & "\par "

        i = DWord(v, 43 * 4, False) '  Internal Flash
        If i = 1 Then parm = "Fired" Else parm = "Not Fired"
        note = note & "Internal Flash:" & tb & parm & "\par "

        i = DWord(v, 44 * 4, False) '  Brightness Value
        note = note & "APEX Brightness Value:" & tb & Format(i / 8 - 6, "##0.0#") & "\par "

        x = DWord(v, 45 * 4, False) '  Spot Focus Point
        i = DWord(v, 46 * 4, False) '  Spot Focus Point
        note = note & "Spot Focus Point:" & tb & "(" & x & ", " & i & ")" & "\par "

        i = DWord(v, 47 * 4, False) '  Wide Focus Zone
        Select Case i
          Case 0
            parm = "No Zone"
          Case 1
            parm = "Center Zone, Horizontal Orientation"
          Case 2
            parm = "Center Zone, Vertical Orientation"
          Case 3
            parm = "Left Zone"
          Case 4
            parm = "Right Zone"
          Case Else
            parm = CStr(i)
        End Select
        note = note & "Wide Focus Zone:" & tb & parm & "\par "

        i = DWord(v, 48 * 4, False) '  Focus Mode
        If i = 1 Then parm = "MF" Else parm = "AF"
        note = note & "Focus Mode:" & tb & parm & "\par "

        i = DWord(v, 49 * 4, False) '  Focus Area
        If i = 1 Then parm = "Spot Focus" Else parm = "Wide Focus"
        note = note & "Focus Area:" & tb & parm & "\par "

        i = DWord(v, 50 * 4, False) '  DEC Position
        Select Case i
          Case 0
            parm = "Exposure"
          Case 1
            parm = "Contrast"
          Case 2
            parm = "Saturation"
          Case 3
            parm = "Filter"
          Case Else
            parm = CStr(i)
        End Select
        note = note & "DEC Position:" & tb & parm & "\par "
      End If

      If makerTags.Contains(sTag(256)) Then
        i = makerTags.Item(sTag(256)).singleValue
        Select Case i
          Case 0
            parm = "Standard"
          Case 1
            parm = "Portrait"
          Case 2
            parm = "Text"
          Case 3
            parm = "Night Scene"
          Case 4
            parm = "Sunset"
          Case 5
            parm = "Sports"
          Case 6
            parm = "Landscape"
          Case 7
            parm = "Night Portrait"
          Case 8
            parm = "Macro"
          Case 9
            parm = "Super Macro"
          Case 16
            parm = "Auto"
          Case 17
            parm = "Night View / Portrait"
          Case Else
            parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Scene Mode:" & tb & parm & "\par "
      End If
    End If

  End Sub ' minoltaMakernote

  Sub olympusMakernote(ByRef makerTags As Collection, ByRef note As String)

    Dim i As Integer
    Dim parm As String = ""
    Dim v As Object
    Dim x As Double

    If makerTags.Contains(sTag(&H201)) Then
      i = makerTags.Item(sTag(&H201)).singleValue
      note = note & "Quality:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H202)) Then
      i = makerTags.Item(sTag(&H202)).singleValue
      Select Case i
        Case 0 : parm = "Off"
        Case 1 : parm = "On"
        Case 2 : parm = "Super Macro"
        Case 3 : parm = "Manual"
        Case Else : parm = CStr(i)
      End Select
      note = note & "Macro Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H204)) Then ' digital zoom, rational
      x = makerTags.Item(sTag(&H204)).singleValue
      If x > 0 Then parm = Format(x, "###0.0") Else parm = "Off"
      note = note & "Digital Zoom:" & tb & parm & "\par "
    End If

    'If makerTags.Contains(sTag(&H207)) Then ' Camera Type, string
    '  parm = makerTags.Item(sTag(&H207)).singleValue
    '  v = Len(parm)
    '  If InStr(parm, ChrW(0)) > 0 Then parm = Trim(Mid(parm, 1, InStr(parm, ChrW(0)) - 1))
    '  If Len(parm) > 0 Then note = note & "Camera Type:" & tb & parm & "\par "
    '  End If

    If makerTags.Contains(sTag(520)) Then ' [picture info] [camera info], string
      note = note & "Picture and Camera Info:" & tb & makerTags.Item(sTag(520)).singleValue & "\par "
    End If

    If makerTags.Contains(sTag(521)) Then ' camera ID, undefined
      v = makerTags.Item(sTag(521)).Value
      parm = ""
      For i = 0 To uuBound(v)
        If v(i) = 0 Then Exit For ' only the first string makes sense
        parm = parm & ChrW(v(i))
      Next i
      parm = Trim(parm)
      If Len(parm) > 0 Then note = note & "Camera ID Data:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H20D)) Then ' camera ID, undefined
      parm = makerTags.Item(sTag(&H20D)).singleValue
      If InStr(parm, ChrW(0)) > 0 Then parm = Trim(Mid(parm, 1, InStr(parm, ChrW(0)) - 1))
      If Len(parm) > 0 Then note = note & "Epson Software:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H300)) Then
      i = makerTags.Item(sTag(&H300)).singleValue
      note = note & "Precapture Frames:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H301)) Then
      i = makerTags.Item(sTag(&H301)).singleValue
      If Len(parm) > 0 Then note = note & "White Board:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H302)) Then
      i = makerTags.Item(sTag(&H302)).singleValue
      Select Case i
        Case 0 : parm = "Off"
        Case 1 : parm = "On"
        Case 2 : parm = "On (preset)"
        Case Else : parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "One Touch White Balance:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H303)) Then
      i = makerTags.Item(sTag(&H303)).singleValue
      note = note & "White Balance Bracket:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H304)) Then
      i = makerTags.Item(sTag(&H304)).singleValue
      note = note & "White Balance Bias:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H403)) Then
      i = makerTags.Item(sTag(&H403)).singleValue
      Select Case i
        Case 0 : parm = "Normal"
        Case 1 : parm = "Standard"
        Case 2 : parm = "Auto"
        Case 4 : parm = "Portrait"
        Case 5 : parm = "Landscape+Portrait"
        Case 6 : parm = "Landscape"
        Case 7 : parm = "Night Scene"
        Case 8 : parm = "Night+Portrait"
        Case 9 : parm = "Sport"
        Case 10 : parm = "Self Portrait"
        Case 11 : parm = "Indoor"
        Case 12 : parm = "Beach & Snow"
        Case 13 : parm = "Beach"
        Case 14 : parm = "Snow"
        Case 15 : parm = "Self Portrait+Self Timer"
        Case 16 : parm = "Sunset"
        Case 17 : parm = "Cuisine"
        Case 18 : parm = "Documents"
        Case 19 : parm = "Candle"
        Case 20 : parm = "Fireworks"
        Case 21 : parm = "Available Light"
        Case 22 : parm = "Vivid"
        Case 23 : parm = "Underwater Wide1"
        Case 24 : parm = "Underwater Macro"
        Case 25 : parm = "Museum"
        Case 26 : parm = "Behind Glass"
        Case 27 : parm = "Auction"
        Case 28 : parm = "Shoot & Select1"
        Case 29 : parm = "Shoot & Select2"
        Case 30 : parm = "Underwater Wide2"
        Case 31 : parm = "Digital Image Stabilization"
        Case 32 : parm = "Face Portrait"
        Case 33 : parm = "Pet"
        Case 34 : parm = "Smile Shot"
        Case Else : parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Scene Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H404)) Then
      v = makerTags.Item(sTag(&H404)).singleValue
      parm = ""
      For i = 1 To Len(v)
        If AscW(v.chars(i - 1)) >= 32 Then parm = parm & v.chars(i - 1)
      Next i
      If InStr(parm, ChrW(0)) > 0 Then parm = Trim(Mid(parm, 1, InStr(parm, ChrW(0)) - 1))
      If Len(parm) > 0 Then note = note & "Serial Number:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H405)) Then ' camera ID, undefined
      parm = makerTags.Item(sTag(&H405)).singleValue
      If InStr(parm, ChrW(0)) > 0 Then parm = Trim(Mid(parm, 1, InStr(parm, ChrW(0)) - 1))
      If Len(parm) > 0 Then note = note & "Firmware:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1000)) Then
      x = makerTags.Item(sTag(&H1000)).singleValue
      If x > 0 And x < 500 Then note = note & "Shutter Speed:" & tb & Format(x, "###,##0.#") & "\par "
    End If

    If makerTags.Contains(sTag(&H1001)) Then
      x = makerTags.Item(sTag(&H1001)).singleValue
      note = note & "ISO:" & tb & Format(x, "###,##0.#") & "\par "
    End If

    If makerTags.Contains(sTag(&H1002)) Then
      x = makerTags.Item(sTag(&H1002)).singleValue
      note = note & "Aperture:" & tb & Format(x, "###,##0.#") & "\par "
    End If

    If makerTags.Contains(sTag(&H1003)) Then
      x = makerTags.Item(sTag(&H1003)).singleValue
      If x > 0 And x < 500000 Then note = note & "Brightness:" & tb & Format(x, "###,##0.#") & "\par "
    End If

    If makerTags.Contains(sTag(&H1004)) Then
      i = makerTags.Item(sTag(&H1004)).singleValue
      If i = 2 Then parm = "On" Else If i = 3 Then parm = "Off" Else parm = ""
      If Len(parm) > 0 Then note = note & "Flash Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1005)) Then
      i = makerTags.Item(sTag(&H1005)).singleValue
      Select Case i
        Case 0 : parm = "(none)"
        Case 1 : parm = "Internal"
        Case 4 : parm = "External"
        Case 5 : parm = "Internal + External"
        Case Else : parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Flash Device:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1006)) Then
      x = makerTags.Item(sTag(&H1006)).singleValue
      note = note & "Exposure Compensation:" & tb & Format(x, "###,##0.#") & "\par "
    End If

    If makerTags.Contains(sTag(&H1007)) Then
      i = makerTags.Item(sTag(&H1007)).singleValue
      note = note & "Sensor Temperature:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H1008)) Then
      i = makerTags.Item(sTag(&H1008)).singleValue
      note = note & "Lens Temperature:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H1009)) Then
      i = makerTags.Item(sTag(&H1009)).singleValue
      note = note & "Light Condition:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H100A)) Then
      i = makerTags.Item(sTag(&H100A)).singleValue
      If i = 0 Then parm = "Normal" Else If i = 1 Then parm = "Macro" Else parm = ""
      If Len(parm) > 0 Then note = note & "Focus Range:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H100B)) Then
      i = makerTags.Item(sTag(&H100B)).singleValue
      If i = 0 Then parm = "Auto" Else If i = 1 Then parm = "Manual" Else parm = ""
      If Len(parm) > 0 Then note = note & "Focus Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H100C)) Then
      x = makerTags.Item(sTag(&H100C)).singleValue
      note = note & "Manual Focus Distance:" & tb & Format(x, "###,##0.#") & "\par "
    End If

    If makerTags.Contains(sTag(&H100D)) Then
      i = makerTags.Item(sTag(&H100D)).singleValue
      note = note & "Zoom Step Count:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H100E)) Then
      i = makerTags.Item(sTag(&H100E)).singleValue
      note = note & "Focus Step Count:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H100F)) Then
      i = makerTags.Item(sTag(&H100F)).singleValue
      Select Case i
        Case 0 : parm = "Normal"
        Case 1 : parm = "Hard"
        Case 2 : parm = "Soft"
        Case Else : parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Sharpness:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1010)) Then
      i = makerTags.Item(sTag(&H1010)).singleValue
      note = note & "Flash Charge Level:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H1011)) Then
      v = makerTags.Item(sTag(&H1011)).Value
      parm = ""
      If UBound(v) >= 8 Then parm = v(0) & " " & v(1) & " " & v(2) & crlf & tb & v(3) & " " & v(4) & " " & v(5) & crlf & tb & v(6) & " " & v(7) & " " & v(8)
      If Len(parm) > 0 Then note = note & "Color Matrix:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1012)) Then
      v = makerTags.Item(sTag(&H1012)).Value
      parm = ""
      If UBound(v) >= 3 Then parm = v(0) & " " & v(1) & " " & v(2) & " " & v(3)
      If Len(parm) > 0 Then note = note & "Black Level:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1013)) Then
      i = makerTags.Item(sTag(&H1013)).singleValue
      note = note & "Color Temperature BG:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H1014)) Then
      i = makerTags.Item(sTag(&H1014)).singleValue
      note = note & "Color Temperature RG:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H1015)) Then
      v = makerTags.Item(sTag(&H1015)).value
      parm = ""
      Select Case v(0)
        Case 1 : parm = "Auto"
        Case 2
          If UBound(v) >= 1 Then
            Select Case v(1)
              Case 2 : parm = "3000 Kelvin"
              Case 3 : parm = "3700 Kelvin"
              Case 4 : parm = "4000 Kelvin"
              Case 5 : parm = "4500 Kelvin"
              Case 6 : parm = "5500 Kelvin"
              Case 7 : parm = "6500 Kelvin"
              Case 8 : parm = "7500 Kelvin"
            End Select
          End If
        Case 3 : parm = "One-Touch"
      End Select
      If Len(parm) > 0 Then note = note & "White Balance Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1017)) Then
      v = makerTags.Item(sTag(&H1017)).Value
      parm = ""
      If UBound(v) >= 2 Then parm = v(0) & " " & v(1) & " " & v(2)
      If Len(parm) > 0 Then note = note & "Red Balance:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1018)) Then
      v = makerTags.Item(sTag(&H1018)).Value
      parm = ""
      If UBound(v) >= 2 Then parm = v(0) & " " & v(1) & " " & v(2)
      If Len(parm) > 0 Then note = note & "Blue Balance:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1019)) Then
      i = makerTags.Item(sTag(&H1019)).singleValue
      note = note & "Color Matrix Number:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H101A)) Then
      v = makerTags.Item(sTag(&H101A)).singleValue
      parm = ""
      For i = 1 To Len(v)
        If AscW(v.chars(i - 1)) >= 32 Then parm = parm & v.chars(i - 1)
      Next i
      If InStr(parm, ChrW(0)) > 0 Then parm = Trim(Mid(parm, 1, InStr(parm, ChrW(0)) - 1))
      If Len(parm) > 0 Then note = note & "Serial Number:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1023)) Then
      x = makerTags.Item(sTag(&H1023)).singleValue
      note = note & "Flash Exposure Compensation:" & tb & Format(x, "###,##0.#") & "\par "
    End If

    If makerTags.Contains(sTag(&H1026)) Then
      i = makerTags.Item(sTag(&H1026)).singleValue
      If i = 0 Then parm = "No" Else If i = 1 Then parm = "Yes" Else parm = ""
      If Len(parm) > 0 Then note = note & "External Flash Bounce:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1027)) Then
      i = makerTags.Item(sTag(&H1027)).singleValue
      note = note & "External Flash Zoom:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H1028)) Then
      i = makerTags.Item(sTag(&H1028)).singleValue
      note = note & "External Flash Zoom:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H1029)) Then
      v = makerTags.Item(sTag(&H1029)).value
      parm = ""
      Select Case v(0)
        Case 1 : parm = "High"
        Case 2 : parm = "Normal"
        Case 3 : parm = "Low"
        Case Else : parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Contrast:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H102A)) Then
      i = makerTags.Item(sTag(&H102A)).singleValue
      note = note & "Sharpness Factor:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H1030)) Then
      i = makerTags.Item(sTag(&H1030)).singleValue
      note = note & "Scene Detect:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H1039)) Then
      i = makerTags.Item(sTag(&H1039)).singleValue
      If i = 0 Then parm = "Interlaced" Else If i = 1 Then parm = "Progressive" Else parm = ""
      If Len(parm) > 0 Then note = note & "CCD Scan Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H103A)) Then
      i = makerTags.Item(sTag(&H103A)).singleValue
      If i = 0 Then parm = "Off" Else If i = 1 Then parm = "On" Else parm = ""
      If Len(parm) > 0 Then note = note & "Noise Reduction:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H103B)) Then
      i = makerTags.Item(sTag(&H103B)).singleValue
      note = note & "Infinity Lens Step:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H103C)) Then
      i = makerTags.Item(sTag(&H103C)).singleValue
      note = note & "Near Lens Step:" & tb & i & "\par "
    End If

    If makerTags.Contains(sTag(&H103D)) Then
      x = makerTags.Item(sTag(&H103D)).singleValue
      note = note & "Light Value, Center:" & tb & Format(x, "###,##0.#") & "\par "
    End If

    If makerTags.Contains(sTag(&H103E)) Then
      x = makerTags.Item(sTag(&H103E)).singleValue
      note = note & "Light Value, Periphery:" & tb & Format(x, "###,##0.#") & "\par "
    End If

    If makerTags.Contains(sTag(&H2010)) Then
      uz = makerTags.Item(sTag(&H2010)).ifd

      If uz.Tags.Contains(sTag(0)) Then
        v = uz.Tags(sTag(0)).value
        parm = ""
        For i = 0 To UBound(v) : parm = parm & ChrW(v(i)) : Next i
        If InStr(parm, ChrW(0)) > 0 Then parm = Trim(Mid(parm, 1, InStr(parm, ChrW(0)) - 1))
        If Len(parm) > 0 Then note = note & "Equipment Tag Version:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H100)) Then
        v = uz.Tags.Item(sTag(&H100)).singleValue
        If InStr(v, ChrW(0)) > 0 Then v = Trim(Mid(v, 1, InStr(v, ChrW(0)) - 1))
        parm = ""
        For i = 1 To Len(v)
          If AscW(v.chars(i - 1)) >= 32 Then parm = parm & v.chars(i - 1)
        Next i
        If Len(parm) > 1 Then
          If olympusCamera.ContainsKey(parm) Then parm = olympusCamera(parm)
          note = note & "Camera Type:" & tb & parm & "\par "
        End If
      End If

      If uz.Tags.Contains(sTag(&H101)) Then
        v = uz.Tags.Item(sTag(&H101)).singleValue
        If InStr(v, ChrW(0)) > 0 Then v = Trim(Mid(v, 1, InStr(v, ChrW(0)) - 1))
        parm = ""
        For i = 1 To Len(v)
          If AscW(v.chars(i - 1)) >= 32 Then parm = parm & v.chars(i - 1)
        Next i
        If InStr(parm, ChrW(0)) > 0 Then parm = Trim(Mid(parm, 1, InStr(parm, ChrW(0)) - 1))
        If Len(parm) > 0 Then note = note & "Serial Number:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H102)) Then
        v = uz.Tags.Item(sTag(&H102)).singleValue
        If InStr(v, ChrW(0)) > 0 Then v = Trim(Mid(v, 1, InStr(v, ChrW(0)) - 1))
        parm = ""
        For i = 1 To Len(v)
          If AscW(v.chars(i - 1)) >= 32 Then parm = parm & v.chars(i - 1)
        Next i
        If InStr(parm, ChrW(0)) > 0 Then parm = Trim(Mid(parm, 1, InStr(parm, ChrW(0)) - 1))
        If Len(parm) > 0 Then note = note & "Internal Serial Number:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H103)) Then
        x = uz.Tags.Item(sTag(&H103)).singleValue
        note = note & "Focal Plane Diagonal:" & tb & Format(x, "####,##0.###") & " mm\par "
      End If

      If uz.Tags.Contains(sTag(&H104)) Then
        i = uz.Tags.Item(sTag(&H104)).singleValue
        parm = Format(i, "x2")
        parm = parm.Insert(1, ".")
        If i <> 0 Then note = note & "Body Firmware Version:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H201)) Then
        v = uz.Tags.Item(sTag(&H201)).Value
        parm = Format(v(0), "x2") & Format(v(2), "x2") & Format(v(3), "x2")
        If olympusLens.ContainsKey(parm) Then parm = olympusLens(parm) Else parm = ""
        If Len(parm) > 0 Then note = note & "Lens Type:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H202)) Then
        v = uz.Tags.Item(sTag(&H202)).singleValue
        parm = v
        If Len(parm) > 0 Then note = note & "Lens Serial Number:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H205)) Then
        i = uz.Tags.Item(sTag(&H205)).singleValue
        If i > 0 Then
          parm = Format(Sqrt(2) ^ (i / 256), "#.0")
          note = note & "Max Aperture at Min. Focal:" & tb & parm & "\par "
        End If
      End If

      If uz.Tags.Contains(sTag(&H206)) Then
        i = uz.Tags.Item(sTag(&H206)).singleValue
        If i > 0 Then
          parm = Format(Sqrt(2) ^ (i / 256), "#.0")
          note = note & "Max Aperture at Max. Focal:" & tb & parm & "\par "
        End If
      End If

      If uz.Tags.Contains(sTag(&H20A)) Then
        i = uz.Tags.Item(sTag(&H20A)).singleValue
        If i > 0 Then
          parm = Format(Sqrt(2) ^ (i / 256), "#.0")
          note = note & "Max Aperture at Current Focal:" & tb & parm & "\par "
        End If
      End If

      If uz.Tags.Contains(sTag(&H207)) Then
        i = uz.Tags.Item(sTag(&H207)).singleValue
        note = note & "Minimum Focal Length:" & tb & i & " mm \par "
      End If

      If uz.Tags.Contains(sTag(&H208)) Then
        i = uz.Tags.Item(sTag(&H208)).singleValue
        note = note & "Maximum Focal Length:" & tb & i & " mm \par "
      End If

      If uz.Tags.Contains(sTag(&H20B)) Then
        i = uz.Tags.Item(sTag(&H20B)).singleValue
        note = note & "Lens Properties:" & tb & Format(i, "x4") & "\par "
      End If

      If uz.Tags.Contains(sTag(&H301)) Then
        v = uz.Tags.Item(sTag(&H301)).Value
        parm = ""
        If v(0) = 0 Then
          Select Case v(2)
            Case 0
              parm = "(none)"
            Case 4
              parm = "Olympus Zuiko Digital EC-14 1.4x Teleconverter"
            Case 8
              parm = "Olympus EX-25 Extension Tube"
            Case 10
              parm = "Olympus Zuiko Digital EC-20 2.0x Teleconverter"
          End Select
          If Len(parm) > 0 Then note = note & "Extender:" & tb & parm & "\par "
        End If
      End If

      If uz.Tags.Contains(sTag(&H302)) Then ' Extender Serial Number
        v = uz.Tags.Item(sTag(&H302)).singleValue
        parm = v
        If Len(parm) > 0 Then note = note & "Extender Serial Number:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H303)) Then ' Extender Model
        v = uz.Tags.Item(sTag(&H302)).singleValue
        parm = v
        If Len(parm) > 0 Then note = note & "Extender Model:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H304)) Then
        i = uz.Tags.Item(sTag(&H304)).singleValue
        If i <> 0 Then note = note & "Extender Firmware Version:" & tb & i & "\par "
      End If

      If uz.Tags.Contains(sTag(&H1000)) Then
        i = uz.Tags.Item(sTag(&H1000)).singlevalue
        Select Case i
          Case 0 : parm = "(none)"
          Case 2 : parm = "Simple E System"
          Case 3 : parm = "E System"
          Case 4 : parm = "E-System (body powered)"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Flash Type:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H1001)) Then
        i = uz.Tags.Item(sTag(&H1001)).singlevalue
        Select Case i
          Case 0 : parm = "(none)"
          Case 1 : parm = "FL-20"
          Case 2 : parm = "FL-50"
          Case 3 : parm = "RF-11"
          Case 4 : parm = "TF-22"
          Case 5 : parm = "FL-36"
          Case 6 : parm = "FL-50R"
          Case 7 : parm = "FL-36R"
          Case 9 : parm = "FL-14"
          Case 11 : parm = "FL-600R"
          Case 13 : parm = "FL-LM3"
          Case 15 : parm = "FL-900R"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Flash Model:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H1002)) Then
        i = uz.Tags.Item(sTag(&H1002)).singleValue
        If i <> 0 Then note = note & "Flash Firmware Version:" & tb & i & "\par "
      End If

      If uz.Tags.Contains(sTag(&H1003)) Then
        v = uz.Tags.Item(sTag(&H1003)).singleValue
        If InStr(v, ChrW(0)) > 0 Then v = Trim(Mid(v, 1, InStr(v, ChrW(0)) - 1))
        parm = ""
        For i = 1 To Len(v)
          If AscW(v.chars(i - 1)) >= 32 Then parm = parm & v.chars(i - 1)
        Next i
        If Len(parm) > 0 Then note = note & "Flash Serial Number:" & tb & parm & "\par "
      End If
    End If ' olympus tag 2010

    If makerTags.Contains(sTag(&H2020)) Then
      uz = makerTags.Item(sTag(&H2020)).ifd

      If uz.Tags.Contains(sTag(0)) Then
        v = uz.Tags(sTag(0)).value
        parm = ""
        For i = 0 To UBound(v) : parm = parm & ChrW(v(i)) : Next i
        If InStr(parm, ChrW(0)) > 0 Then parm = Trim(Mid(parm, 1, InStr(parm, ChrW(0)) - 1))
        If Len(parm) > 0 Then note = note & "Camera Settings Version:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H100)) Then
        i = uz.Tags.Item(sTag(&H100)).singleValue
        If i = 0 Then parm = "No" Else If i = 1 Then parm = "Yes" Else parm = ""
        If Len(parm) > 0 Then note = note & "Preview Image Valid:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H200)) Then
        i = uz.Tags.Item(sTag(&H200)).singlevalue
        parm = ""
        Select Case i
          Case 1 : parm = "Manual"
          Case 2 : parm = "Program"
          Case 3 : parm = "Aperture-priority AE"
          Case 4 : parm = "Shutter speed priority AE"
          Case 5 : parm = "Program Shift"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Exposure Mode:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H201)) Then
        i = uz.Tags.Item(sTag(&H201)).singleValue
        If i = 0 Then parm = "Off" Else If i = 1 Then parm = "On" Else parm = ""
        If Len(parm) > 0 Then note = note & "AE Lock:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H202)) Then
        i = uz.Tags.Item(sTag(&H202)).singlevalue
        parm = ""
        Select Case i
          Case 2 : parm = "Center-weighted average"
          Case 3 : parm = "Spot"
          Case 5 : parm = "ESP"
          Case 261 : parm = "Pattern+AF"
          Case 515 : parm = "Spot+Highlight control"
          Case 1027 : parm = "Spot+Shadow control"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Metering Mode:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H203)) Then
        x = uz.Tags.Item(sTag(&H203)).singlevalue
        If Len(parm) > 0 Then note = note & "Exposure shift:" & tb & Format(x, "#0.##") & "\par "
      End If

      If uz.Tags.Contains(sTag(&H204)) Then
        i = uz.Tags.Item(sTag(&H204)).singleValue
        If i = 0 Then parm = "Off" Else If i = 1 Then parm = "On" Else parm = ""
        If Len(parm) > 0 Then note = note & "ND filter:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H300)) Then
        i = uz.Tags.Item(sTag(&H300)).singlevalue
        parm = ""
        Select Case i
          Case 0 : parm = "Off"
          Case 1 : parm = "On"
          Case 2 : parm = "Super Macro"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Macro Mode:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H301)) Then
        v = uz.Tags.Item(sTag(&H301)).value
        Select Case v(0)
          Case 0 : parm = "Single AF"
          Case 1 : parm = "Sequential shooting AF"
          Case 2 : parm = "Continuous AF"
          Case 3 : parm = "Multi AF"
          Case 4 : parm = "Face detect"
          Case 10 : parm = "MF"
          Case Else : parm = ""
        End Select

        i = v(1)
        If i And 1 Then parm &= ", S-AF"
        If i And 4 Then parm &= ", C-AF"
        If i And 16 Then parm &= ", MF"
        If i And 32 Then parm &= ", Face detect"
        If i And 64 Then parm &= ", Imager AF"
        If i And 128 Then parm &= ", Live View Magnification Frame"
        If i And 256 Then parm &= ", AF sensor"

        If parm.StartsWith(", ") Then parm = parm.Substring(2)
        If Len(parm) > 0 Then note = note & "Focus Mode:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H302)) Then
        v = uz.Tags.Item(sTag(&H302)).Value
        If v(0) = 0 Then parm = "AF Not Used" Else If v(0) = 1 Then parm = "AF Used" Else parm = ""
        If Len(parm) > 0 Then note = note & "Focus Process:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H303)) Then
        i = uz.Tags.Item(sTag(&H303)).singleValue
        If i = 0 Then parm = "Not Ready" Else If i = 1 Then parm = "Ready" Else parm = ""
        If Len(parm) > 0 Then note = note & "AF Search:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H306)) Then
        i = uz.Tags.Item(sTag(&H306)).singleValue
        If i = 0 Then parm = "Off" Else If i = 1 Then parm = "On" Else parm = ""
        If Len(parm) > 0 Then note = note & "AF Fine Tune:" & tb & parm & "\par "
      End If


      If uz.Tags.Contains(sTag(&H400)) Then
        i = uz.Tags.Item(sTag(&H400)).singlevalue
        parm = ""
        If i = 0 Then parm = "Off"
        If i And 2 ^ 0 Then parm = parm & "On"
        If i And 2 ^ 1 Then parm = parm & ", fill-in"
        If i And 2 ^ 2 Then parm = parm & ", red-eye"
        If i And 2 ^ 3 Then parm = parm & ", slow-sync"
        If i And 2 ^ 4 Then parm = parm & ", forced On"
        If i And 2 ^ 5 Then parm = parm & ", 2nd curtain"
        If parm.StartsWith(", ") Then parm = parm.Substring(2)
        If Len(parm) > 0 Then note = note & "Flash Mode:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H403)) Then
        i = uz.Tags.Item(sTag(&H403)).singlevalue
        Select Case i
          Case 0 : parm = "Off"
          Case 1 : parm = "Channel 1, low"
          Case 2 : parm = "Channel 2, low"
          Case 3 : parm = "Channel 3, low"
          Case 4 : parm = "Channel 4, low"
          Case 9 : parm = "Channel 1, mid"
          Case 10 : parm = "Channel 2, mid"
          Case 11 : parm = "Channel 3, mid"
          Case 12 : parm = "Channel 4, mid"
          Case 17 : parm = "Channel 1, high"
          Case 18 : parm = "Channel 2, high"
          Case 19 : parm = "Channel 3, high"
          Case 20 : parm = "Channel 4, high"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Flash remote control:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H404)) Then
        i = uz.Tags.Item(sTag(&H404)).singlevalue
        Select Case i
          Case 0 : parm = "off"
          Case 3 : parm = "TTL"
          Case 4 : parm = "Auto"
          Case 5 : parm = "Manual"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Flash control mode:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H500)) Then
        i = uz.Tags.Item(sTag(&H500)).singlevalue
        parm = ""
        Select Case i
          Case 0 : parm = "Auto"
          Case 1 : parm = "Auto (Keep Warm Color Off)"
          Case 16 : parm = "7500K (Fine Weather with Shade)"
          Case 17 : parm = "6000K (Cloudy)"
          Case 18 : parm = "5300K (Fine Weather)"
          Case 20 : parm = "3000K (Tungsten light)"
          Case 21 : parm = "3600K (Tungsten light-like)"
          Case 22 : parm = "Auto Setup"
          Case 23 : parm = "5500K (Flash) "
          Case 33 : parm = "6600K (Daylight fluorescent)"
          Case 34 : parm = "4500K (Neutral white fluorescent)"
          Case 35 : parm = "4000K (Cool white fluorescent)"
          Case 36 : parm = "White fluorescent"
          Case 48 : parm = "3600K (Tungsten light-like)"
          Case 67 : parm = "Underwater"
          Case 256 : parm = "One touch WB 1"
          Case 257 : parm = "One touch WB 2"
          Case 258 : parm = "One touch WB 3"
          Case 259 : parm = "One touch WB 4"
          Case 512 : parm = "Custom WB 1"
          Case 513 : parm = "Custom WB 2"
          Case 514 : parm = "Custom WB 3"
          Case 515 : parm = "Custom WB 4"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H501)) Then
        i = uz.Tags.Item(sTag(&H501)).singleValue
        note = note & "White Balance Temperature:" & tb & i & "\par "
      End If

      If uz.Tags.Contains(sTag(&H502)) Then
        i = uz.Tags.Item(sTag(&H502)).singleValue
        note = note & "White Balance Bracket:" & tb & i & "\par "
      End If

      If uz.Tags.Contains(sTag(&H503)) Then
        v = uz.Tags.Item(sTag(&H503)).Value
        If UBound(v) >= 2 Then
          note = note & "Custom Saturation Setting:" & tb & v(0) & "\par "
          'note = note & "Custom Saturation Minimum:" & tb & v(1) & "\par "
          'note = note & "Custom Saturation Maximum:" & tb & v(2) & "\par "
        End If
      End If

      If uz.Tags.Contains(sTag(&H504)) Then
        i = uz.Tags.Item(sTag(&H504)).singlevalue
        parm = ""
        Select Case i
          Case 0 : parm = "Off"
          Case 1 : parm = "CM1 (Red Enhance)"
          Case 2 : parm = "CM2 (Green Enhance)"
          Case 3 : parm = "CM3 (Blue Enhance)"
          Case 4 : parm = "CM4 (Skin Tones)"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Modified Saturation:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H505)) Then
        v = uz.Tags.Item(sTag(&H505)).Value
        If UBound(v) >= 2 Then
          note = note & "Contrast Setting:" & tb & v(0) & "\par "
          'note = note & "Custom Contrast Minimum:" & tb & v(1) & "\par "
          'note = note & "Custom Contrast Maximum:" & tb & v(2) & "\par "
        End If
      End If

      If uz.Tags.Contains(sTag(&H506)) Then
        v = uz.Tags.Item(sTag(&H506)).Value
        If UBound(v) >= 2 Then
          note = note & "Sharpness Setting:" & tb & v(0) & "\par "
          'note = note & "Custom Sharpness Minimum:" & tb & v(1) & "\par "
          'note = note & "Custom Sharpness Maximum:" & tb & v(2) & "\par "
        End If
      End If

      If uz.Tags.Contains(sTag(&H507)) Then
        i = uz.Tags.Item(sTag(&H507)).singlevalue
        parm = ""
        Select Case i
          Case 0 : parm = "sRGB"
          Case 1 : parm = "Adobe RGB"
          Case 2 : parm = "Pro Photo RGB"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Color Space:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H509)) Then
        i = uz.Tags.Item(sTag(&H509)).singlevalue
        If olympusSceneMode.ContainsKey(i) Then parm = olympusSceneMode(i) Else parm = ""
        If Len(parm) > 0 Then note = note & "Scene Mode:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H50A)) Then
        i = uz.Tags.Item(sTag(&H50A)).singlevalue
        parm = ""
        If i = 0 Then parm = "Off"
        If i And 2 ^ 0 Then parm = parm & "On"
        If i And 2 ^ 1 Then parm = parm & ", Noise Filter"
        If i And 2 ^ 2 Then parm = parm & ", Noise Filter, ISO Boost"
        If i And 2 ^ 3 Then parm = parm & ", Auto"
        If parm.StartsWith(", ") Then parm = parm.Substring(2)
        If Len(parm) > 0 Then note = note & "Noise Reduction:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H50B)) Then
        i = uz.Tags.Item(sTag(&H50B)).singleValue
        If i = 0 Then parm = "Off" Else If i = 1 Then parm = "On" Else parm = ""
        If Len(parm) > 0 Then note = note & "Distortion Correction:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H50C)) Then
        i = uz.Tags.Item(sTag(&H50C)).singleValue
        If i = 0 Then parm = "Off" Else If i = 1 Then parm = "On" Else parm = ""
        If Len(parm) > 0 Then note = note & "Shading Compensation:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H50D)) Then
        x = uz.Tags.Item(sTag(&H50D)).singleValue
        note = note & "Compression Factor:" & tb & Format(x, "####,##0.###") & "\par "
      End If

      If uz.Tags.Contains(sTag(&H50F)) Then
        v = uz.Tags.Item(sTag(&H50F)).Value
        If v.length >= 3 Then
          If v(0) = -1 And v(1) = -1 And v(2) = 1 Then parm = "Low key"
          If v(0) = 0 And v(1) = -1 And v(2) = 1 Then parm = "Normal"
          If v(0) = 0 And v(1) = 0 And v(2) = 0 Then parm = "n/a"
          If v(0) = 1 And v(1) = -1 And v(2) = 1 Then parm = "High key"
          If v.length = 4 Then parm &= "; " & v(3)
          If Len(parm) > 0 Then note = note & "Gradation:" & tb & parm & "\par "
        End If
      End If

      If uz.Tags.Contains(sTag(&H520)) Then
        v = uz.Tags.Item(sTag(&H520)).value
        parm = ""
        Select Case v(0)
          Case 1 : parm = "Vivid"
          Case 2 : parm = "Natural"
          Case 3 : parm = "Muted"
          Case 4 : parm = "Portrait"
          Case 5 : parm = "i-Enhance"
          Case 6 : parm = "e-Portrait"
          Case 7 : parm = "Color Creator"
          Case 9 : parm = "Color Profile 1"
          Case 10 : parm = "Color Profile 2"
          Case 11 : parm = "Color Profile 3"
          Case 12 : parm = "Monochrome Profile 1"
          Case 13 : parm = "Monochrome Profile 2"
          Case 14 : parm = "Monochrome Profile 3 "
          Case 256 : parm = "Monotone"
          Case 512 : parm = "Sepia"
          Case Else : parm = ""
        End Select
        If v.length = 2 Then parm = parm & "; " & v(1)
        If Len(parm) > 0 Then note = note & "Picture Mode:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H521)) Then
        v = uz.Tags.Item(sTag(&H521)).Value
        i = v(1) : If i > 32767 Then i = i - 65536
        If UBound(v) >= 2 Then
          note = note & "Picture Mode Saturation:" & tb & v(0) & " (min " & i & ", max " & v(2) & ")\par "
        End If
      End If

      If uz.Tags.Contains(sTag(&H523)) Then
        v = uz.Tags.Item(sTag(&H523)).Value
        i = v(1) : If i > 32767 Then i = i - 65536
        If UBound(v) >= 2 Then
          note = note & "Picture Mode Contrast:" & tb & v(0) & " (min " & i & ", max " & v(2) & ")\par "
        End If
      End If

      If uz.Tags.Contains(sTag(&H524)) Then
        v = uz.Tags.Item(sTag(&H524)).Value
        i = v(1) : If i > 32767 Then i = i - 65536
        If UBound(v) >= 2 Then
          note = note & "Picture Mode Sharpness:" & tb & v(0) & " (min " & i & ", max " & v(2) & ")\par "
        End If
      End If

      If uz.Tags.Contains(sTag(&H525)) Then
        i = uz.Tags.Item(sTag(&H525)).singlevalue
        parm = ""
        Select Case i
          Case 1 : parm = "Neutral"
          Case 2 : parm = "Yellow"
          Case 3 : parm = "Orange"
          Case 4 : parm = "Red"
          Case 5 : parm = "Green"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Picture Mode BW Filter:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H526)) Then
        i = uz.Tags.Item(sTag(&H526)).singlevalue
        parm = ""
        Select Case i
          Case 1 : parm = "Neutral"
          Case 2 : parm = "Sepia"
          Case 3 : parm = "Blue"
          Case 4 : parm = "Purple"
          Case 5 : parm = "Green"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Picture mode tone:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H527)) Then
        v = uz.Tags.Item(sTag(&H527)).value
        parm = ""
        If v.length >= 3 AndAlso v(1) = -2 AndAlso v(2) = 1 Then
          If v(0) = -1 Then parm = "Low"
          If v(0) = -2 Then parm = "Off"
          If v(0) = 0 Then parm = "Standard"
          If v(0) = 1 Then parm = "High"
        End If
        If Len(parm) > 0 Then note = note & "Noise filter:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H529)) Then
        v = uz.Tags.Item(sTag(&H529)).value
        i = v(0)
        If olympusArtFilter.ContainsKey(i) Then
          parm = olympusArtFilter(i) & "; " & v(1) & "; " & v(2) & "; " & v(3)
          note = note & "Art filter:" & tb & parm & "\par "
        End If
      End If

      If uz.Tags.Contains(sTag(&H52C)) Then
        v = uz.Tags.Item(sTag(&H52C)).value
        i = v(0)
        If olympusMagicFilter.ContainsKey(i) Then
          parm = olympusMagicFilter(i) & "; " & v(1) & "; " & v(2) & "; " & v(3)
          note = note & "Magic filter:" & tb & parm & "\par "
        End If
      End If

      If uz.Tags.Contains(sTag(&H52D)) Then
        v = uz.Tags.Item(sTag(&H52D)).Value
        If v.length >= 3 Then
          If v(0) = -1 And v(1) = -1 And v(2) = 1 Then parm = "Low"
          If v(0) = 0 And v(1) = -1 And v(2) = 1 Then parm = "Normal"
          If v(0) = 1 And v(1) = -1 And v(2) = 1 Then parm = "High"
          If Len(parm) > 0 Then note = note & "Picture mode effect:" & tb & parm & "\par "
        End If
      End If

      If uz.Tags.Contains(sTag(&H52F)) Then
        v = uz.Tags.Item(sTag(&H52F)).value
        i = v(0)
        If olympusArtFilterEffect.ContainsKey(i) AndAlso v.length >= 7 Then
          parm = olympusArtFilterEffect(i) & "; " & v(4) & "; " & v(6)
          note = note & "Art filter effect:" & tb & parm & "\par "
        End If
      End If

      If uz.Tags.Contains(sTag(&H537)) Then
        v = uz.Tags.Item(sTag(&H537)).value
        Select Case v(0)
          Case 0 : parm = "no filter"
          Case 1 : parm = "Yellow"
          Case 2 : parm = "Orange"
          Case 3 : parm = "Red"
          Case 4 : parm = "Magenta"
          Case 5 : parm = "Blue"
          Case 6 : parm = "Cyan"
          Case 7 : parm = "Green"
          Case 8 : parm = "Yellow-green"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Monochrome profile settings:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H538)) Then
        v = uz.Tags.Item(sTag(&H538)).value
        Select Case v(0)
          Case 0 : parm = "off"
          Case 1 : parm = "Low"
          Case 2 : parm = "Medium"
          Case 3 : parm = "High"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Film grain effect:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H53A)) Then
        i = uz.Tags.Item(sTag(&H53A)).singleValue
        If i > 0 Then parm = i Else parm = "(none)"
        note = note & "Monochrome vignetting:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H53B)) Then
        i = uz.Tags.Item(sTag(&H53B)).singlevalue
        parm = ""
        Select Case i
          Case 0 : parm = "(none)"
          Case 1 : parm = "Normal"
          Case 2 : parm = "Sepia"
          Case 3 : parm = "Blue"
          Case 4 : parm = "Purple"
          Case 5 : parm = "Green"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Monochrome color:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H600)) Then
        v = uz.Tags.Item(sTag(&H600)).Value
        note = note & "Drive Mode:" & tb & v(0) & "\par "
        If v(0) <> 0 Then note = note & "Drive Shot Number:" & tb & v(1) & "\par "
      End If

      If uz.Tags.Contains(sTag(&H601)) Then
        v = uz.Tags.Item(sTag(&H601)).Value
        note = note & "Panorama Mode:" & tb & v(0) & "\par "
        If v(0) <> 0 Then note = note & "Panorama Shot Number:" & tb & v(1) & "\par "
      End If

      If uz.Tags.Contains(sTag(&H603)) Then
        i = uz.Tags.Item(sTag(&H603)).singlevalue
        parm = ""
        Select Case i
          Case 1 : parm = "Standard"
          Case 2 : parm = "High"
          Case 3 : parm = "Super High"
          Case 4 : parm = "Raw"
          Case 4 : parm = "Standard (5)"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Image Quality:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H604)) Then
        i = uz.Tags.Item(sTag(&H604)).singlevalue
        parm = ""
        Select Case i
          Case 0 : parm = "(off)"
          Case 1 : parm = "Mode 1"
          Case 2 : parm = "Mode 2"
          Case 3 : parm = "Mode 3"
          Case 4 : parm = "Mode 4"
          Case Else : parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "Image stabilization:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H804)) Then
        v = uz.Tags.Item(sTag(&H804)).value
        parm = ""
        If v.length = 2 Then
          If v(0) = 0 And v(1) = 0 Then parm = "(none)"
          If v(0) = 5 And v(1) = 4 Then parm = "HDR1"
          If v(0) = 6 And v(1) = 4 Then parm = "HDR2"
          If v(0) = 9 And v(1) = 8 Then parm = "Focus stacked, 8 images"
          If Len(parm) > 0 Then note = note & "Stacked image:" & tb & parm & "\par "
        End If
      End If

      If uz.Tags.Contains(sTag(&H900)) Then
        x = uz.Tags.Item(sTag(&H900)).singleValue
        note = note & "Manometer pressure:" & tb & Format(x / 10, "#0") & " kPa\par "

        If x <> 0 AndAlso uz.Tags.Contains(sTag(&H901)) Then
          v = uz.Tags.Item(sTag(&H901)).Value
          If v.length > 1 Then note = note & "Manometer reading:" & tb & Format(v(0) / 10, "#0") & " m, " & Format(v(1) / 10, "#0") & " ft \par "
        End If
      End If

      If uz.Tags.Contains(sTag(&H902)) Then
        i = uz.Tags.Item(sTag(&H902)).singleValue
        If i = 0 Then parm = "Off" Else If i = 1 Then parm = "On" Else parm = ""
        If Len(parm) > 0 Then note = note & "Extended white balance detect:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H903)) Then
        x = uz.Tags.Item(sTag(&H903)).singleValue
        v = uz.Tags.Item(sTag(&H903)).Value
        If x > 32767 Then x -= 65536
        note = note & "Roll angle:" & tb & Format(x, "#0") & "°\par "
      End If

      If uz.Tags.Contains(sTag(&H904)) Then
        x = uz.Tags.Item(sTag(&H904)).singleValue
        If x > 32767 Then x -= 65536
        x = -x ' positive pitch is up
        note = note & "Pitch angle:" & tb & Format(x, "#0") & "°\par "
      End If

      If uz.Tags.Contains(sTag(&H908)) Then
        v = uz.Tags.Item(sTag(&H908)).singleValue
        parm = v
        If parm <> "" AndAlso Not parm.StartsWith("0000") Then note = note & "UTC date and time:" & tb & parm & "\par "
      End If

    End If ' olympus tag 2020

    If makerTags.Contains(sTag(&H2050)) Then
      uz = makerTags.Item(sTag(&H2050)).ifd

      If uz.Tags.Contains(sTag(0)) Then
        v = uz.Tags(sTag(0)).value
        parm = ""
        For i = 0 To UBound(v) : parm = parm & ChrW(v(i)) : Next i
        If InStr(parm, ChrW(0)) > 0 Then parm = Trim(Mid(parm, 1, InStr(parm, ChrW(0)) - 1))
        If Len(parm) > 0 Then note = note & "Focus Settings Version:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H100)) Then
        i = uz.Tags.Item(sTag(&H100)).singleValue
        If i = 0 Then parm = "Off" Else If i = 1 Then parm = "On" Else parm = ""
        If Len(parm) > 0 Then note = note & "Autofocus:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H210)) Then
        i = uz.Tags.Item(sTag(&H210)).singleValue
        note = note & "Scene Detect:" & tb & i & "\par "
      End If

      If uz.Tags.Contains(sTag(&H300)) Then
        i = uz.Tags.Item(sTag(&H300)).singleValue
        note = note & "Zoom Step Count:" & tb & i & "\par "
      End If

      If uz.Tags.Contains(sTag(&H301)) Then
        i = uz.Tags.Item(sTag(&H301)).singleValue
        note = note & "Focus Step Count:" & tb & i & "\par "
      End If

      If uz.Tags.Contains(sTag(&H303)) Then
        i = uz.Tags.Item(sTag(&H303)).singleValue
        note = note & "Focus Step Infinity:" & tb & i & "\par "
      End If

      If uz.Tags.Contains(sTag(&H304)) Then
        i = uz.Tags.Item(sTag(&H304)).singleValue
        note = note & "Focus Step Near:" & tb & i & "\par "
      End If

      If uz.Tags.Contains(sTag(&H305)) Then
        ' "this rational value looks like it is in mm when the denominator is 1 (E-1), "
        ' "and cm when denominator is 10 (E-300), so if we ignore the denominator we are consistently in mm - PH"
        x = uz.Tags.Item(sTag(&H305)).singleValue
        x = x / 100 ' sometimes this should be / 10
        If x > 0 Then note = note & "Focus Distance:" & tb & Format(x, "####,##0.##") & " m\par "
      End If

      If uz.Tags.Contains(sTag(&H308)) Then
        v = uz.Tags.Item(sTag(&H308)).value
        If UBound(v) >= 1 Then
          i = v(0)
          Select Case (i And &H1F)
            Case &H0 : parm = "(none)"
            Case &H1 : parm = "Top-left (horizontal)"
            Case &H2 : parm = "Top-center (horizontal)"
            Case &H3 : parm = "Top-right (horizontal)"
            Case &H4 : parm = "Left (horizontal)"
            Case &H5 : parm = "Mid-left (horizontal)"
            Case &H6 : parm = "Center (horizontal)"
            Case &H7 : parm = "Mid-right (horizontal)"
            Case &H8 : parm = "Right (horizontal)"
            Case &H9 : parm = "Bottom-left (horizontal)"
            Case &HA : parm = "Bottom-center (horizontal)"
            Case &HB : parm = "Bottom-right (horizontal)"
            Case &HC : parm = "Top-left (vertical)"
            Case &HD : parm = "Top-center (vertical)"
            Case &HE : parm = "Top-right (vertical)"
            Case &HF : parm = "Left (vertical)"
            Case &H10 : parm = "Mid-left (vertical)"
            Case &H11 : parm = "Center (vertical)"
            Case &H12 : parm = "Mid-right (vertical)"
            Case &H13 : parm = "Right (vertical)"
            Case &H14 : parm = "Bottom-left (vertical)"
            Case &H15 : parm = "Bottom-center (vertical)"
            Case &H16 : parm = "Bottom-right (vertical) "
            Case Else : parm = ""
          End Select
        Else
          parm = ""
          'Select Case v(0)
          '  Case 0 : parm = "Left"
          '  Case 1 : parm = "Center (horizontal)"
          '  Case 2 : parm = "Right"
          '  Case 3 : parm = "Center (vertical)"
          '  Case 255 : parm = "(none)"
          '  Case Else : parm = ""
          'End Select
        End If
        If Len(parm) > 0 Then note = note & "Autofocus Point:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H1201)) Then
        v = uz.Tags.Item(sTag(&H1201)).value
        If v(0) = 0 Then parm = "Off" Else If v(0) = 1 Then parm = "On" Else parm = ""
        If Len(parm) > 0 Then note = note & "External Flash:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H1204)) Then
        v = uz.Tags.Item(sTag(&H1204)).value
        If v(0) = 0 Then parm = "Bounce or Off" Else If v(0) = 1 Then parm = "Direct" Else parm = ""
        If Len(parm) > 0 Then note = note & "External Flash Bounce:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H1205)) Then
        x = uz.Tags.Item(sTag(&H1205)).singleValue
        If x > 0 Then note = note & "External Flash Zoom:" & tb & Format(x, "####,##0.###") & "\par "
      End If

      If uz.Tags.Contains(sTag(&H1208)) Then
        v = uz.Tags.Item(sTag(&H1208)).value
        If v(0) = 0 Then parm = "Off" Else If v(0) = 1 Then parm = "On" Else parm = ""
        If Len(parm) > 0 Then note = note & "Internal Flash:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H1209)) Then
        v = uz.Tags.Item(sTag(&H1209)).value
        If v(0) = 0 Then parm = "Off" Else If v(0) = 1 Then parm = "On" Else parm = ""
        If v.length >= 2 AndAlso v(1) > 0 Then
          x = 1 / v(1)
          If x < 1 Then parm &= " (" & Format(x, "#.##") & " strength)" Else parm &= " (full strength)"
        End If
        If Len(parm) > 0 Then note = note & "Manual Flash:" & tb & parm & "\par "
      End If

      If uz.Tags.Contains(sTag(&H120A)) Then
        v = uz.Tags.Item(sTag(&H120A)).value
        If v(0) = 0 Then parm = "Off" Else If v(0) = 1 Then parm = "On" Else parm = ""
        If Len(parm) > 0 Then note = note & "Macro LED:" & tb & parm & "\par "
      End If


    End If ' olympus tag 2050

  End Sub ' olympusMakernote

  Sub sanyoMakernote(ByRef makerTags As Collection, ByRef note As String)

    Dim i As Integer
    Dim parm As String = ""
    Dim v As Object
    Dim x As Double

    If makerTags.Contains(sTag(513)) Then
      i = makerTags.Item(sTag(513)).singleValue
      Select Case i Mod 256
        'Case 0
        '  parm = "Very Low"
        'Case 1
        '  parm = "Low"
        'Case 2
        '  parm = "Medium Low"
        'Case 3
        '  parm = "Medium"
        'Case 4
        '  parm = "Medium High"
        'Case 5
        '  parm = "High"
        'Case 6
        '  parm = "Very High"
        'Case 7
        '  parm = "Super High"
        Case Else
          parm = CStr(i Mod 256)
      End Select
      note = note & "JPG Quality:" & tb & parm & "\par "
      Select Case i \ 256
        Case 0
          parm = "Normal"
        Case 1
          parm = "Fine"
        Case 2
          parm = "Superfine"
        Case Else
          parm = CStr(i \ 256)
      End Select
      note = note & "Detail:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(514)) Then
      i = makerTags.Item(sTag(514)).singleValue
      Select Case i
        Case 0
          parm = "Normal"
        Case 1
          parm = "Macro"
        Case 2
          parm = "View"
        Case 3
          parm = "Manual"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Macro Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H204)) Then ' digital zoom, rational
      x = makerTags.Item(sTag(&H204)).singleValue
      If x > 0 Then parm = Format(x, "###0.0") Else parm = "Off"
      note = note & "Digital Zoom:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H207)) Then ' Firmware version, string
      parm = makerTags.Item(sTag(&H207)).singleValue
      If InStr(parm, ChrW(0)) > 0 Then parm = Trim(Mid(parm, 1, InStr(parm, ChrW(0)) - 1))
      If Trim(parm) <> "" Then note = note & "Firmware Version:" & tb & Trim(parm) & "\par "
    End If

    If makerTags.Contains(sTag(520)) Then ' [picture info] [camera info], string
      note = note & "Picture and Camera Info:" & tb & makerTags.Item(sTag(520)).singleValue & "\par "
    End If

    If makerTags.Contains(sTag(521)) Then ' camera ID, undefined
      v = makerTags.Item(sTag(521)).Value
      parm = ""
      For i = 0 To uuBound(v)
        If v(i) = 0 Then Exit For ' only the first string makes sense
        parm = parm & ChrW(v(i))
      Next i
      parm = Trim(parm)
      If Len(parm) > 0 Then note = note & "Camera ID Data:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H20E)) Then ' [picture info] [camera info], string
      i = makerTags.Item(sTag(&H20E)).singleValue
      Select Case i
        Case 0 : parm = "(none)"
        Case 1 : parm = "Standard"
        Case 2 : parm = "Best"
        Case 3 : parm = "Adjust Exposure"
        Case Else : parm = CStr(i)
      End Select
      note = note & "Sequential Shot Method:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H20F)) Then ' [picture info] [camera info], string
      i = makerTags.Item(sTag(&H20F)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Wide Range:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H210)) Then ' [picture info] [camera info], string
      i = makerTags.Item(sTag(&H210)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Color Adjustment Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H213)) Then ' [picture info] [camera info], string
      i = makerTags.Item(sTag(&H213)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Quick Shot:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H214)) Then ' [picture info] [camera info], string
      i = makerTags.Item(sTag(&H214)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Self Timer:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H216)) Then ' [picture info] [camera info], string
      i = makerTags.Item(sTag(&H216)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Memo Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H217)) Then ' [picture info] [camera info], string
      i = makerTags.Item(sTag(&H217)).singleValue
      Select Case i
        Case 0
          parm = "Record While Pressed"
        Case 1
          parm = "Press to Start, Press to Stop"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Record Shutter Release:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H218)) Then ' [picture info] [camera info], string
      i = makerTags.Item(sTag(&H218)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Flicker Reduction:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H219)) Then ' [picture info] [camera info], string
      i = makerTags.Item(sTag(&H219)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Optical Zoom:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H21B)) Then ' [picture info] [camera info], string
      i = makerTags.Item(sTag(&H21B)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Digital Zoom:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H21D)) Then ' [picture info] [camera info], string
      i = makerTags.Item(sTag(&H21D)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Special Light Source:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H21E)) Then ' [picture info] [camera info], string
      i = makerTags.Item(sTag(&H21E)).singleValue
      Select Case i
        Case 0
          parm = "No"
        Case 1
          parm = "Yes"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Image Resaved?" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H21F)) Then ' [picture info] [camera info], string
      i = makerTags.Item(sTag(&H21F)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "Sport"
        Case 2
          parm = "TV"
        Case 3
          parm = "Night"
        Case 4
          parm = "User 1"
        Case 5
          parm = "User 2"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Scene Selection:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H223)) Then ' [picture info] [camera info], string
      x = makerTags.Item(sTag(&H223)).singleValue
      note = note & "Manual Focus Distance:" & tb & Format(x, "##,##0.0") & "\par "
    End If

    If makerTags.Contains(sTag(&H224)) Then ' [picture info] [camera info], string
      i = makerTags.Item(sTag(&H224)).singleValue
      Select Case i
        Case 0
          parm = "5 frames / sec."
        Case 1
          parm = "10 frames / sec."
        Case 2
          parm = "15 frames / sec."
        Case 3
          parm = "20 frames / sec."
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Sequential Shot Interval:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H225)) Then ' [picture info] [camera info], string
      i = makerTags.Item(sTag(&H225)).singleValue
      Select Case i
        Case 0
          parm = "Auto"
        Case 1
          parm = "On"
        Case 2
          parm = "Off"
        Case 3
          parm = "Red-Eye"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Flash Mode:" & tb & parm & "\par "
    End If

  End Sub ' sanyo makernote

  Sub fujiMakernote(ByRef makerTags As Collection, ByRef note As String)

    Dim i, k As Integer
    Dim parm As String = ""
    Dim v As Object
    Dim x As Double

    If makerTags.Contains(sTag(0)) Then
      v = makerTags.Item(sTag(0)).Value
      k = uuBound(v)
      parm = ""
      If k >= 0 Then
        For i = 0 To k
          If v(i) >= 1 And v(i) <= 9 Then v(i) = v(i) + 48
          parm = parm & ChrW(v(i))
        Next i
        parm = splitString(parm)
      Else
        parm = v
      End If
      If Trim(parm) <> "" Then note = note & "Makernote Version:" & tb & Trim(parm) & "\par "
    End If

    If makerTags.Contains(sTag(4096)) Then
      parm = makerTags.Item(sTag(4096)).singleValue
      note = note & "Quality Setting:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(4097)) Then
      i = makerTags.Item(sTag(4097)).singleValue
      Select Case i
        Case 1, 2
          parm = "Soft"
        Case 3
          parm = "Normal"
        Case 4, 5
          parm = "Hard"
        Case &H82
          parm = "Medium Soft"
        Case &H84
          parm = "Medium Hard"
        Case &H8000
          parm = "Film Simulation"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Sharpness Setting:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(4098)) Then
      i = makerTags.Item(sTag(4098)).singleValue
      Select Case i
        Case 0
          parm = "Auto"
        Case 256
          parm = "Daylight"
        Case 512
          parm = "Cloudy"
        Case 768
          parm = "Daylight Color Fluorescent"
        Case 769
          parm = "Daywhite Color Fluorescent"
        Case 770
          parm = "White Fluorescent"
        Case 771
          parm = "Warm White Fluorescent"
        Case 772
          parm = "Living Room Fluorescent"
        Case 1024
          parm = "Incandescent"
        Case 1280
          parm = "Flash"
        Case 3840, 3841, 3842, 3843, 3844
          parm = "Custom"
        Case 4080
          parm = "Kelvin"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "White Balance Setting:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(4099)) Then
      i = makerTags.Item(sTag(4099)).singleValue
      Select Case i
        Case 0
          parm = "Normal"
        Case 128
          parm = "Medium High"
        Case 256
          parm = "High"
        Case 384
          parm = "Medium Low"
        Case 512
          parm = "Low"
        Case 768
          parm = "(none - B&W)"
        Case &H8000
          parm = "Film Simulation"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Color Saturation Setting:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(4100)) Then
      i = makerTags.Item(sTag(4100)).singleValue
      Select Case i
        Case 0
          parm = "Normal"
        Case 128
          parm = "Medium High"
        Case 256
          parm = "High"
        Case 384
          parm = "Medium Low"
        Case 512
          parm = "Low"
        Case &H8000
          parm = "Film Simulation"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Contrast Setting:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1005)) Then
      v = makerTags.Item(sTag(&H1005)).singleValue
      note = note & "Color Temperature:" & tb & v & "\par "
    End If

    If makerTags.Contains(sTag(&H1006)) Then
      i = makerTags.Item(sTag(&H1006)).singleValue
      Select Case i
        Case 0
          parm = "Normal"
        Case 256
          parm = "High"
        Case 512
          parm = "Low"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Contrast:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H100A)) Then
      v = makerTags.Item(sTag(&H100A)).Value
      If UBound(v) >= 1 Then note = note & "White Balance Fine Tune:" & tb & v(0) & " " & v(1) & "\par "
    End If

    If makerTags.Contains(sTag(&H100B)) Then
      i = makerTags.Item(sTag(&H100B)).singleValue
      Select Case i
        Case &H40
          parm = "Low"
        Case &H80
          parm = "Normal"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Noise Reduction:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1010)) Then
      i = makerTags.Item(sTag(&H1010)).singleValue
      Select Case i
        Case 0
          parm = "Auto"
        Case 1
          parm = "On"
        Case 2
          parm = "Off"
        Case 3
          parm = "Red Eye Reduction"
        Case 4
          parm = "External"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Flash Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1011)) Then
      x = makerTags.Item(sTag(&H1011)).singleValue
      note = note & "Flash Compensation:" & tb & Format(x, "##0.0#") & "\par "
    End If

    If makerTags.Contains(sTag(&H1020)) Then
      i = makerTags.Item(sTag(&H1020)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Macro Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1021)) Then
      i = makerTags.Item(sTag(&H1021)).singleValue
      Select Case i
        Case 0
          parm = "Auto"
        Case 1
          parm = "Manual"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Focus Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1030)) Then
      i = makerTags.Item(sTag(&H1030)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Slow Synchro Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1031)) Then
      i = makerTags.Item(sTag(&H1031)).singleValue
      Select Case i
        Case 0
          parm = "Auto"
        Case 1
          parm = "Portrait Scene"
        Case 2
          parm = "Landscape Scene"
        Case 3
          parm = "Macro"
        Case 4
          parm = "Sports Scene"
        Case 5
          parm = "Night Scene"
        Case 6
          parm = "Program AE"
        Case 7
          parm = "Natural Light"
        Case 8
          parm = "Anti-blur"
        Case 9
          parm = "Beach & Snow"
        Case 10
          parm = "Sunset"
        Case 11
          parm = "Museum"
        Case 12
          parm = "Party"
        Case 13
          parm = "Flower"
        Case 14
          parm = "Text"
        Case 15
          parm = "Natural Light & Flash"
        Case 16
          parm = "Beach"
        Case 17
          parm = "Snow"
        Case 18
          parm = "Fireworks"
        Case 19
          parm = "Underwater"
        Case 256
          parm = "Aperture Priority AE"
        Case 512
          parm = "Shutter Priority AE"
        Case 768
          parm = "Manual Exposure"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Picture Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1100)) Then
      i = makerTags.Item(sTag(&H1100)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Continuous or Auto Bracket Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1300)) Then
      i = makerTags.Item(sTag(&H1300)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Blur Warning:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1301)) Then
      i = makerTags.Item(sTag(&H1301)).singleValue
      Select Case i
        Case 0
          parm = "Autofocus Good"
        Case 1
          parm = "Focus Warning"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Focus Warning Status:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1302)) Then
      i = makerTags.Item(sTag(&H1302)).singleValue
      Select Case i
        Case 0
          parm = "AE Good"
        Case 1
          parm = "Over Exposure"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Exposure Warning Status:" & tb & parm & "\par "
    End If
    ' Fuji done

  End Sub ' fujiMakernote

  Sub epsonMakernote(ByRef makerTags As Collection, ByRef note As String)

    Dim i, j As Integer
    Dim parm As String = ""
    Dim v As Object
    Dim x As Double

    If makerTags.Contains(sTag(&H201)) Then
      i = makerTags.Item(sTag(&H201)).singleValue
      Select Case i
        Case 1
          parm = "Standard"
        Case 2
          parm = "Fine"
        Case 3
          parm = "Super-Fine"
        Case 4
          parm = "HyPict"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Image Quality:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H202)) Then
      i = makerTags.Item(sTag(&H202)).singleValue
      Select Case i
        Case 0
          parm = "Normal"
        Case 1
          parm = "Macro"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Macro Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H204)) Then
      x = makerTags.Item(sTag(&H204)).singleValue
      If x > 0 Then parm = Format(x, "##0.0") Else parm = "Off"
      note = note & "Digital Zoom:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H209)) Then
      v = makerTags.Item(sTag(&H209)).Value
      parm = ""
      For i = 0 To UBound(v)
        parm = parm & ChrW(v(i))
      Next i
      'parm = splitString(parm)
      If InStr(parm, ChrW(0)) > 0 Then parm = Trim(Mid(parm, 1, InStr(parm, ChrW(0)) - 1))
      If Len(parm) > 0 Then note = note & "Camera:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H20B)) And makerTags.Contains(sTag(&H20C)) Then
      i = makerTags.Item(sTag(&H20B)).singleValue
      j = makerTags.Item(sTag(&H20C)).singleValue
      note = note & "Image Size:" & tb & i & "x" & j & "\par "
    End If

    If makerTags.Contains(sTag(&H20D)) Then
      parm = makerTags.Item(sTag(&H20D)).singleValue
      If InStr(parm, ChrW(0)) > 0 Then parm = Trim(Mid(parm, 1, InStr(parm, ChrW(0)) - 1))
      ' parm = splitString(parm)
      If Len(parm) > 0 Then note = note & "Hardware Type:" & tb & parm & "\par "
    End If
    ' epson done


  End Sub ' epsonMakernote

  Sub casioMakernote(ByRef makerTags As Collection, ByRef note As String)

    Dim i As Integer
    Dim parm As String = ""

    If makerTags.Contains(sTag(1)) Then
      i = makerTags.Item(sTag(1)).singleValue
      Select Case i
        Case 1
          parm = "Single Shutter"
        Case 2
          parm = "Panorama"
        Case 3
          parm = "Night Scene"
        Case 4
          parm = "Portrait"
        Case 5
          parm = "Landscape"
        Case 7
          parm = "Panorama"
        Case 10
          parm = "Night Scene"
        Case 15
          parm = "Portrait"
        Case 16
          parm = "Landscape"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Recording Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(2)) Then
      i = makerTags.Item(sTag(2)).singleValue
      Select Case i
        Case 1
          parm = "Economy"
        Case 2
          parm = "Normal"
        Case 3
          parm = "Fine"
        Case 4
          parm = "Tiff"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "JPG Quality:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(3)) Then
      i = makerTags.Item(sTag(3)).singleValue
      Select Case i
        Case 1
          parm = "Economy"
        Case 2
          parm = "Macro"
        Case 3
          parm = "Autofocus  "
        Case 4
          parm = "Manual Focus"
        Case 5
          parm = "Infinity"
        Case 7
          parm = "Spot AF"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Focus Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(4)) Then
      i = makerTags.Item(sTag(4)).singleValue
      Select Case i
        Case 1
          parm = "Auto"
        Case 2
          parm = "On"
        Case 3
          parm = "Off"
        Case 4, 5
          parm = "Red Eye"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Flash Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(5)) Then
      i = makerTags.Item(sTag(5)).singleValue
      Select Case i
        Case 11
          parm = "Weak"
        Case 13
          parm = "Normal"
        Case 14
          parm = "High"
        Case 15
          parm = "Strong"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Flash Intensity:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(6)) Then
      i = makerTags.Item(sTag(6)).singleValue
      note = note & "Object Distance:" & tb & Format(i / 1000, "##,##0.0#") & " m" & "\par "
    End If

    If makerTags.Contains(sTag(7)) Then
      i = makerTags.Item(sTag(7)).singleValue
      Select Case i
        Case 1
          parm = "Auto"
        Case 2
          parm = "Tungsten"
        Case 3
          parm = "Daylight"
        Case 4
          parm = "Fluorescent"
        Case 5
          parm = "Shade"
        Case 129
          parm = "Manual"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "White Balance:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(10)) Then
      i = makerTags.Item(sTag(10)).singleValue
      Select Case i
        Case 65536
          parm = "Off"
        Case 65537, 131072
          parm = "2x"
        Case 262144
          parm = "4x"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Digital Zoom:" & tb & parm & "\par "
    End If


    If makerTags.Contains(sTag(11)) Then
      i = makerTags.Item(sTag(11)).singleValue
      Select Case i Mod 16
        Case 0
          parm = "Normal"
        Case 1
          parm = "Soft"
        Case 2
          parm = "Hard"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Sharpness:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(12)) Then
      i = makerTags.Item(sTag(12)).singleValue
      Select Case i Mod 16
        Case 0
          parm = "Normal"
        Case 1
          parm = "Low"
        Case 2
          parm = "High"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Contrast:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(13)) Then
      i = makerTags.Item(sTag(13)).singleValue
      Select Case i Mod 16
        Case 0
          parm = "Normal"
        Case 1
          parm = "Low"
        Case 2
          parm = "High"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Saturation:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(20)) Then
      i = makerTags.Item(sTag(20)).singleValue
      note = note & "CCD Sensitivity:" & tb & i & "\par "
    End If

    'vOffset = 0
    'i = getTagOffset(v, 21, k, vOffset, False)
    'If i >= 0 Then
    '  parm = ""
    '  For i = i To i + k - 1
    '    If v(i) = 0 Then Exit For
    '    parm = parm & chrw(v(i))
    '      next i
    '  note = note & "21:     " & tb & parm & "\par "
    '    end If

    If makerTags.Contains(sTag(22)) Then
      i = makerTags.Item(sTag(22)).singleValue
      Select Case i
        Case 1
          parm = "Off"
        Case 2
          parm = "Red"
        Case 3
          parm = "Green"
        Case 4
          parm = "Blue"
        Case 5
          parm = "Fleshtones"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Enhancement:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(23)) Then
      i = makerTags.Item(sTag(23)).singleValue
      Select Case i
        Case 1
          parm = "Off"
        Case 2
          parm = "B/W"
        Case 3
          parm = "Sepia"
        Case 4
          parm = "Red"
        Case 5
          parm = "Green"
        Case 6
          parm = "Blue"
        Case 7
          parm = "Yellow"
        Case 8
          parm = "Pink"
        Case 9
          parm = "Purple"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Filter:         " & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(24)) Then
      i = makerTags.Item(sTag(24)).singleValue
      Select Case i
        Case 1
          parm = "Center"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Focus Area:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(25)) Then
      i = makerTags.Item(sTag(25)).singleValue
      Select Case i
        Case 1
          parm = "Normal"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Flash Intensity:" & tb & parm & "\par "
    End If
    ' casio done

  End Sub ' casioMakernote

  Sub kodakMakernote(ByRef makerTags As Collection, ByRef note As String)

    Dim i, k As Integer
    Dim parm As String = ""
    Dim v As Object
    Dim x As Double

    ' these tags are arbitrarily given IDs according to their offsets

    If makerTags.Contains(sTag(9)) Then
      i = makerTags.Item(sTag(9)).singleValue
      If i = 1 Then parm = "Fine" Else If i = 2 Then parm = "Normal" Else parm = ""
      If Len(parm) > 0 Then note = note & "Quality:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(10)) Then
      i = makerTags.Item(sTag(10)).singleValue
      If i = 0 Then parm = "Off" Else If i = 1 Then parm = "On" Else parm = ""
      If Len(parm) > 0 Then note = note & "Burst Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(11)) Then
      i = makerTags.Item(sTag(11)).singleValue
      If i = 0 Then parm = "Normal" Else If i = 1 Then parm = "Close Up" Else parm = ""
      If Len(parm) > 0 Then note = note & "Macro Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(12)) Then
      v = makerTags.Item(sTag(12)).value
      If uuBound(v) = 1 AndAlso (v(0) > 0 And v(1) > 0) Then note = note & "Image Size:" & tb & v(0) & " x " & v(1) & "\par "
    End If

    If makerTags.Contains(sTag(16)) Then
      parm = makerTags.Item(sTag(16)).singleValue
      note = note & "Date:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(20)) Then
      parm = makerTags.Item(sTag(20)).singleValue
      note = note & "Time:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(27)) Then
      i = makerTags.Item(sTag(27)).singleValue
      Select Case i
        Case 0
          parm = "Auto"
        Case 8
          parm = "Aperture Priority"
        Case 2, 32
          parm = "Manual"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Shutter Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(28)) Then
      i = makerTags.Item(sTag(28)).singleValue
      Select Case i
        Case 0
          parm = "Multi Pattern"
        Case 1
          parm = "Center Weight"
        Case 2
          parm = "Center Spot"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Metering Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(29)) Then
      i = makerTags.Item(sTag(29)).singleValue
      If i > 0 Then parm = i Else parm = ""
      If Len(parm) > 0 Then note = note & "Burst Sequence Index:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(30)) Then
      i = makerTags.Item(sTag(30)).singleValue
      If i > 0 Then parm = Format(i / 100, "##0.00") Else parm = ""
      If Len(parm) > 0 Then note = note & "F-Number:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(32)) Then
      i = makerTags.Item(sTag(32)).singleValue
      x = i / 100000
      If x < 1 And x <> 0 Then
        parm = Format(x, "##0.0###") & " (1/" & Format(1 / x, "##,###") & ") sec."
      Else
        parm = Format(x, "##0.0") & " sec."
      End If
      If Len(parm) > 0 Then note = note & "Exposure Time:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(36)) Then
      i = makerTags.Item(sTag(36)).singleValue
      parm = Format(i / 1000, "##0.0##")
      If Len(parm) > 0 Then note = note & "Exposure Bias:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(56)) Then
      i = makerTags.Item(sTag(56)).singleValue
      Select Case i
        Case 0
          parm = "Auto"
        Case 2
          parm = "Macro"
        Case 3
          parm = "Infinity"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Focus Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(60)) Then
      i = makerTags.Item(sTag(60)).singleValue
      If i = 0 Then parm = "Normal" Else If i = -1 Then parm = "Infinity" Else parm = ""
      If Len(parm) > 0 Then note = note & "Panorama Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(62)) Then
      i = makerTags.Item(sTag(62)).singleValue
      If i > 0 Then
        parm = Format(i - 25, "####,##0.#")
        If Len(parm) > 0 Then note = note & "Subject Distance (Inches):" & tb & parm & "\par "
      End If
    End If

    If makerTags.Contains(sTag(64)) Then
      i = makerTags.Item(sTag(64)).singleValue
      Select Case i
        Case 0
          parm = "Normal"
        Case 1
          parm = "Fluorescent"
        Case 2
          parm = "Tungsten"
        Case 3
          parm = "Daylight"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(92)) Then
      i = makerTags.Item(sTag(92)).singleValue
      Select Case i
        Case 0
          parm = "Auto"
        Case 1, 16
          parm = "Fill"
        Case 2, 32
          parm = "Off"
        Case 3, 64
          parm = "Red Eye"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Flash Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(93)) Then
      i = makerTags.Item(sTag(93)).singleValue
      If i = 0 Then parm = "No" Else If i = 1 Then parm = "Yes" Else parm = ""
      If Len(parm) > 0 Then note = note & "Flash Fired:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(94)) Then
      i = makerTags.Item(sTag(94)).singleValue
      parm = Format(i, "####,##0")
      If Len(parm) > 0 Then note = note & "ISO Setting:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(96)) Then
      i = makerTags.Item(sTag(96)).singleValue
      parm = Format(i, "####,##0")
      If Len(parm) > 0 Then note = note & "ISO:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(98)) Then
      i = makerTags.Item(sTag(98)).singleValue
      parm = Format(i / 100, "####,##0.##")
      If Len(parm) > 0 Then note = note & "Total Zoom:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(100)) Then
      i = makerTags.Item(sTag(100)).singleValue
      parm = Format(i, "####,##0")
      If Len(parm) > 0 Then note = note & "Date-Time Stamp Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(102)) Then
      i = makerTags.Item(sTag(102)).singleValue
      Select Case i
        Case 1, &H2000
          parm = "B&W"
        Case 2, &H4000
          parm = "Sepia"
        Case 3
          parm = "B&W Yellow Filter"
        Case 4
          parm = "B&W Red Filter"
        Case 32, 256
          parm = "Saturated Color"
        Case 64, 512
          parm = "Neutral Color"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Color Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(104)) Then
      i = makerTags.Item(sTag(104)).singleValue
      parm = Format(i / 100, "####,##0.##")
      If Len(parm) > 0 Then note = note & "Digital Zoom Factor:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(107)) Then
      i = makerTags.Item(sTag(107)).singleValue
      Select Case i
        Case 0
          parm = "Standard"
        Case 1
          parm = "Sharp"
        Case 255
          parm = "Soft"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Sharpness:" & tb & parm & "\par "
      k = k + 1
    End If
    ' kodak done

  End Sub ' kodakMakernote

  Sub panasonicMakernote(ByRef makerTags As Collection, ByRef note As String)

    Dim i As Integer
    Dim parm As String = ""
    Dim v As Object
    Dim x As Double
    Dim s As String
    Dim makerVersion As Integer = 0

    If makerTags.Contains(sTag(&H8000)) Then
      v = makerTags.Item(sTag(&H8000)).Value
      parm = ""
      If IsArray(v) Then
        For i = 0 To UBound(v)
          parm = parm & ChrW(v(i))
        Next i
      Else
        parm = ChrW(v)
      End If
      If Trim(parm) <> "" Then
        note = note & "Maker Note Version:" & tb & Trim(parm) & "\par "
        makerVersion = Val(Trim(parm))
      End If
    End If

    If makerTags.Contains(sTag(1)) Then
      i = makerTags.Item(sTag(1)).singleValue
      Select Case i
        Case 2
          parm = "High"
        Case 3
          parm = "Normal"
        Case 6
          parm = "Very High"
        Case 7
          parm = "Raw"
        Case 8, 9
          parm = "Motion Picture"
        Case 11
          parm = "Full HD Movie"
        Case 12
          parm = "4K Movie"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Image Quality:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(3)) Then
      i = makerTags.Item(sTag(3)).singleValue
      Select Case i
        Case 1
          parm = "Auto"
        Case 2
          parm = "Daylight"
        Case 3
          parm = "Cloudy"
        Case 4
          parm = "Incandescent"
        Case 5
          parm = "Manual"
        Case 6, 8
          parm = "Flash"
        Case 10
          parm = "Black & White"
        Case 11
          parm = "Manual"
        Case 12
          parm = "Shade"
        Case 13
          parm = "Kelvin"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "White Balance:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(7)) Then
      i = makerTags.Item(sTag(7)).singleValue
      Select Case i
        Case 1
          parm = "Auto"
        Case 2
          parm = "Manual"
        Case 4
          parm = "Auto, Focus Button"
        Case 5
          parm = "Auto, Continuous"
        Case 6
          parm = "AF-S"
        Case 7
          parm = "AF-C"
        Case 8
          parm = "AF-F"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Focus Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(15)) Then
      v = makerTags.Item(sTag(15)).Value
      parm = "Normal"
      If IsArray(v) AndAlso UBound(v) >= 1 Then
        If v(0) = 0 And v(1) = 1 Then parm = "Spot Mode On"
        If v(0) = 0 And v(1) = 16 Then parm = "Spot Mode Off or 3-area Hi-speed"
        If v(0) = 1 And v(1) = 0 Then parm = "Spot Focusing"
        If v(0) = 1 And v(1) = 1 Then parm = "5-area"
        If v(0) = 16 And v(1) = 0 Then parm = "1-area"
        If v(0) = 16 And v(1) = 16 Then parm = "1-area Hi-speed"
        If v(0) = 32 And v(1) = 0 Then parm = "3-area Auto"
        If v(0) = 32 And v(1) = 1 Then parm = "3-area Left"
        If v(0) = 32 And v(1) = 2 Then parm = "3-area Center"
        If v(0) = 32 And v(1) = 3 Then parm = "3-area Right"
        If v(0) = 64 And v(1) = 0 Then parm = "Face Detect"
        If v(0) = 128 And v(1) = 0 Then parm = "Spot Focusing 2"
      End If
      note = note & "Autofocus Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(26)) Then
      i = makerTags.Item(sTag(26)).singleValue
      Select Case i
        Case 2
          parm = "Mode 1"
        Case 3
          parm = "Off"
        Case 4
          parm = "Mode 2"
        Case 5
          parm = "Panning"
        Case 6
          parm = "Mode 3"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Image Stabilizer:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(28)) Then
      i = makerTags.Item(sTag(28)).singleValue
      Select Case i
        Case 1
          parm = "Macro"
        Case 2
          parm = "Normal"
        Case 257
          parm = "Tele-Macro"
        Case 513
          parm = "Macro Zoom"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Macro Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(31)) Then
      i = makerTags.Item(sTag(31)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "Normal"
        Case 2
          parm = "Portrait"
        Case 3
          parm = "Scenery"
        Case 4
          parm = "Sports"
        Case 5
          parm = "Night Portrait"
        Case 6
          parm = "Program"
        Case 7
          parm = "Aperture Priority"
        Case 8
          parm = "Shutter Priority"
        Case 9
          parm = "Macro"
        Case 10
          parm = "Spot"
        Case 11
          parm = "Manual"
        Case 12
          parm = "Movie Preview"
        Case 13
          parm = "Panning"
        Case 14
          parm = "Simple"
        Case 15
          parm = "Color Effects"
        Case 18
          parm = "Fireworks"
        Case 19
          parm = "Party"
        Case 20
          parm = "Snow"
        Case 21
          parm = "Night Scenery"
        Case 22
          parm = "Food"
        Case 23
          parm = "Baby"
        Case 24
          parm = "Soft Skin"
        Case 25
          parm = "Candlelight"
        Case 26
          parm = "Starry Night"
        Case 27
          parm = "High Sensitivity"
        Case 28
          parm = "Panorama Assist"
        Case 29
          parm = "Underwater"
        Case 30
          parm = "Beach"
        Case 31
          parm = "Aerial Photo"
        Case 32
          parm = "Sunset"
        Case 33
          parm = "Pet"
        Case 34
          parm = "Intelligent ISO"
        Case 36
          parm = "High Speed Continuous Shooting"
        Case 37
          parm = "Intelligent Auto"
        Case 39
          parm = "Multi-aspect"
        Case 41
          parm = "Transform"
        Case 42
          parm = "Flash Burst"
        Case 43
          parm = "Pin Hole"
        Case 44
          parm = "Film Grain"
        Case 45
          parm = "My Color"
        Case 46
          parm = "Photo Frame"
        Case 48
          parm = "Movie"
        Case 51
          parm = "HDR"
        Case 52
          parm = "Peripheral Defocus"
        Case 55
          parm = "Handheld Night Shot"
        Case 57
          parm = "3D"
        Case 59
          parm = "Creative Control"
        Case 62
          parm = "Panorama"
        Case 63
          parm = "Glass Through"
        Case 64
          parm = "HDR"
        Case 66
          parm = "Digital Filter"
        Case 67
          parm = "Clear Portrait"
        Case 68
          parm = "Silky Skin"
        Case 69
          parm = "Backlit Softness"
        Case 70
          parm = "Clear in Backlight"
        Case 71
          parm = "Relaxing Tone"
        Case 72
          parm = "Sweet Child's Face"
        Case 73
          parm = "Distinct Scenery"
        Case 74
          parm = "Bright Blue Sky"
        Case 75
          parm = "Romantic Sunset Glow"
        Case 76
          parm = "Vivid Sunset Glow"
        Case 77
          parm = "Glistening Water"
        Case 78
          parm = "Clear Nightscape"
        Case 79
          parm = "Cool Night Sky"
        Case 80
          parm = "Warm Glowing Nightscape"
        Case 81
          parm = "Artistic Nightscape"
        Case 82
          parm = "Glittering Illuminations"
        Case 83
          parm = "Clear Night Portrait"
        Case 84
          parm = "Soft Image of a Flower"
        Case 85
          parm = "Appetizing Food"
        Case 86
          parm = "Cute Desert"
        Case 87
          parm = "Freeze Animal Motion"
        Case 88
          parm = "Clear Sports Shot"
        Case 89
          parm = "Monochrome"
        Case 90
          parm = "Creative Control"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Shooting Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(32)) Then
      i = makerTags.Item(sTag(32)).singleValue
      Select Case i
        Case 1
          parm = "Yes"
        Case 2
          parm = "No"
        Case 2
          parm = "Stereo"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Audio:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(35)) Then
      x = makerTags.Item(sTag(35)).singleValue
      parm = Format(x, "###0.0#")
      note = note & "White Balance Bias:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(36)) Then
      x = makerTags.Item(sTag(36)).singleValue
      If x > 32767 Then x = x - 65536
      parm = Format(x, "###0.0#")
      note = note & "Flash Bias:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(37)) Then
      x = makerTags.Item(sTag(37)).singleValue
      parm = x
      note = note & "Internal Serial Number:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(38)) Then
      x = makerTags.Item(sTag(38)).singleValue
      parm = x
      note = note & "Panasonic Exif Version:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(40)) Then
      i = makerTags.Item(sTag(40)).singleValue
      Select Case i
        Case 1
          parm = "Off"
        Case 2
          parm = "Warm"
        Case 3
          parm = "Cool"
        Case 4
          parm = "Black and White"
        Case 5
          parm = "Sepia"
        Case 6
          parm = "Happy"
        Case 8
          parm = "Vivid"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Color Effect:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(41)) Then
      x = makerTags.Item(sTag(41)).singleValue
      parm = Format(x / 100, "###0.0#")
      note = note & "Seconds Since Power On:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(42)) Then
      i = makerTags.Item(sTag(42)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "Low/Hi-quality or On"
        Case 2
          parm = "Infinite or AEB"
        Case 4
          parm = "Unlimited"
        Case 8
          parm = "White Balance Bracketing"
        Case 17
          parm = "On (with flash)"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Burst Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(43)) Then
      x = makerTags.Item(sTag(43)).singleValue
      parm = Format(x, "###,##0")
      If x > 0 Then note = note & "Sequence Number:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(44)) Then
      s = uz.TagValue(uExif.TagID.model, 0)
      i = makerTags.Item(sTag(44)).singleValue
      If s IsNot Nothing AndAlso (InStr(s, "DMC-LC") > 0 Or InStr(s, "DMC-LX") > 0 Or InStr(s, "DMC-FZ") > 0) Then
        ' LC1, LX2, FZ7, FZ8, FZ18 And FZ50
        Select Case i
          Case 0
            parm = "Normal"
          Case 1
            parm = "Low"
          Case 2
            parm = "High"
          Case 6
            parm = "Medium Low"
          Case 7
            parm = "Medium High"
          Case 13
            parm = "High Dynamic"
          Case &H100
            parm = "Low"
          Case &H110
            parm = "Normal"
          Case &H120
            parm = "High"

          Case Else
            parm = CStr(i)
        End Select
      Else ' gf1 and newer cameras?
        Select Case i
          Case 0
            parm = "-2"
          Case 1
            parm = "-1"
          Case 2
            parm = "Normal"
          Case 3
            parm = "+1"
          Case 4
            parm = "+2"
          Case 5
            parm = "Normal 2"
          Case 7
            parm = "Nature"
          Case 9
            parm = "Expressive"
          Case 12
            parm = "Smooth or Pure"
          Case 17
            parm = "Dynamic (black and white)"
          Case 22
            parm = "Smooth (black and white)"
          Case 25
            parm = "High Dynamic"
          Case 26, 42
            parm = "Retro"
          Case 27
            parm = "Dynamic"
          Case 28
            parm = "Low Key"
          Case 29
            parm = "Toy Effect"
          Case 32
            parm = "Vibrant or Expressive"
          Case 33
            parm = "Elegant"
          Case 37
            parm = "Nostalgic"
          Case 41
            parm = "Dynamic Art"
          Case 42
            parm = "Retro (My Color)"
          Case 45
            parm = "Cinema"
          Case 47
            parm = "Dynamic Mono"
          Case 50
            parm = "Impressive Art"
          Case 51
            parm = "Cross Process"
          Case 100
            parm = "High Dynamic 2"
          Case 101
            parm = "Retro 2"
          Case 102
            parm = "High Key 2"
          Case 103
            parm = "Low Key 2"
          Case 104
            parm = "Toy Effect 2"
          Case 107
            parm = "Expressive 2"
          Case 112
            parm = "Sepia"
          Case 117
            parm = "Miniature"
          Case 122
            parm = "Dynamic Monochrome"
          Case 127
            parm = "Old Days"
          Case 132
            parm = "Dynamic Monochrome 2"
          Case 135
            parm = "Impressive Art 2"
          Case 136
            parm = "Cross Process 2"
          Case 137
            parm = "Toy Pop"
          Case 138
            parm = "Fantasy"
          Case 256
            parm = "Normal 3"
          Case 272
            parm = "Standard"
          Case 288
            parm = "High"
          Case Else
            parm = CStr(i)
        End Select
      End If

      note = note & "Contrast:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(45)) Then
      i = makerTags.Item(sTag(45)).singleValue
      Select Case i
        Case 0
          parm = "Standard"
        Case 1
          parm = "Low (-1)"
        Case 2
          parm = "High (+1)"
        Case 3
          parm = "Lowest (-2)"
        Case 4
          parm = "Highest (+2)"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Noise Reduction:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(46)) Then
      i = makerTags.Item(sTag(46)).singleValue
      v = makerTags.Item(sTag(46)).Value
      Select Case i
        Case 1
          parm = "Off (or 10x3)"
        Case 2
          parm = "10 Seconds"
        Case 3
          parm = "2 Seconds"
        Case 4
          parm = "10 Seconds, 3 Pictures"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Self Timer:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(48)) Then
      i = makerTags.Item(sTag(48)).singleValue
      Select Case i
        Case 1
          parm = "Upright"
        Case 3
          parm = "Upside Down"
        Case 6
          parm = "Rotated Right"
        Case 8
          parm = "Rotated Left"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Rotation:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(49)) Then
      i = makerTags.Item(sTag(49)).singleValue
      Select Case i
        Case 1
          parm = "Fired"
        Case 2
          parm = "Enabled but Not Used"
        Case 3
          parm = "Disabled but Required"
        Case 4
          parm = "Disabled and Not Required"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Autofocus Assist Lamp:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(50)) Then
      i = makerTags.Item(sTag(50)).singleValue
      Select Case i
        Case 0
          parm = "Normal"
        Case 1
          parm = "Natural"
        Case 2
          parm = "Vivid"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Color Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(51)) Then
      parm = makerTags.Item(sTag(51)).singleValue
      If parm Is Nothing Then parm = "" ' prevents error if zero-length array
      If InStr(parm, ChrW(0)) > 0 Then parm = Left(parm, InStr(parm, ChrW(0)) - 1)
      parm = parm.Trim
      If Len(parm) > 0 AndAlso parm.IndexOf("00:00:00") < 0 AndAlso Asc(parm.Chars(0)) > 31 Then note = note & "Baby or Pet Age:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(52)) Then
      i = makerTags.Item(sTag(52)).singleValue
      Select Case i
        Case 1
          parm = "Standard"
        Case 2
          parm = "Extended"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Optical Zoom Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(53)) Then
      i = makerTags.Item(sTag(53)).singleValue
      Select Case i
        Case 1
          parm = "Off"
        Case 2
          parm = "Wide"
        Case 3
          parm = "Telephoto"
        Case 4
          parm = "Macro"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Conversion Lens:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(54)) Then
      x = makerTags.Item(sTag(54)).singleValue
      If x <> 65535 Then
        parm = Format(x, "####,##0")
        note = note & "Travel Day:" & tb & parm & "\par "
      End If
    End If

    If makerTags.Contains(sTag(57)) Then
      x = makerTags.Item(sTag(57)).singleValue
      If x > 32767 And x < 65536 Then x = x - 65536
      parm = Format(x, "##,##0")
      note = note & "Contrast:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(58)) Then
      i = makerTags.Item(sTag(58)).singleValue
      Select Case i
        Case 1
          parm = "Home"
        Case 2
          parm = "Destination"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "World Time Location:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(60)) Then
      x = makerTags.Item(sTag(60)).singleValue
      If x > 32767 And x < 65536 Then x = x - 65536
      If x = -2 Then parm = "Intelligent ISO" Else x = parm = Format(x, "##,##0")
      note = note & "Program:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(61)) Then
      i = makerTags.Item(sTag(61)).singleValue
      Select Case i
        Case 1
          parm = "Normal"
        Case 2
          parm = "Outdoor/Illuminations/Flower/HDR Art"
        Case 3
          parm = "Indoor/Architecture/Objects/HDR B&W"
        Case 4
          parm = "Creative"
        Case 5
          parm = "Auto"
        Case 7
          parm = "Expressive"
        Case 8
          parm = "Retro"
        Case 9
          parm = "Pure"
        Case 10
          parm = "Elegant"
        Case 12
          parm = "Monochrome"
        Case 13
          parm = "Dynamic Art"
        Case 14
          parm = "Silhouette"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Advanced Scene Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(62)) Then
      i = makerTags.Item(sTag(62)).singleValue
      Select Case i
        Case 1
          parm = "Off"
        Case 2
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Text Stamp:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(63)) Then
      i = makerTags.Item(sTag(63)).singleValue
      parm = CStr(i)
      note = note & "Faces Detected:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(64)) Then
      x = makerTags.Item(sTag(64)).singleValue
      If x > 32767 And x < 65536 Then x = x - 65536
      parm = Format(x, "##,##0")
      note = note & "Saturation:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(65)) Then
      x = makerTags.Item(sTag(65)).singleValue
      If x > 32767 And x < 65536 Then x = x - 65536
      parm = Format(x, "##,##0")
      note = note & "Sharpness:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(66)) Then
      i = makerTags.Item(sTag(66)).singleValue
      Select Case i
        Case -1
          parm = "Custom"
        Case 1
          parm = "Standard (color)"
        Case 2
          parm = "Dynamic (color)"
        Case 3
          parm = "Nature (color)"
        Case 4
          parm = "Smooth (color)"
        Case 5
          parm = "Standard (B&W)"
        Case 6
          parm = "Dynamic (B&W)"
        Case 7
          parm = "Smooth (B&W)"
        Case 10
          parm = "Nostalgic (color)"
        Case 11
          parm = "Vibrant (color)"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Film Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(68)) Then
      x = makerTags.Item(sTag(68)).singleValue
      parm = Format(x, "##,##0")
      note = note & "Color Temperature (Kelvin):" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(69)) Then
      i = makerTags.Item(sTag(69)).singleValue
      Select Case i
        Case 0
          parm = "No Bracket"
        Case 1
          parm = "3 Images, Sequence 0/-/+"
        Case 2
          parm = "3 Images, Sequence -/0/+"
        Case 3
          parm = "5 Images, Sequence 0/-/+"
        Case 4
          parm = "5 Images, Sequence -/0/+"
        Case 5
          parm = "7 Images, Sequence 0/-/+"
        Case 6
          parm = "7 Images, Sequence -/0/+"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Bracket Settings:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(70)) Then
      x = makerTags.Item(sTag(70)).singleValue
      parm = Format(x, "##,##0")
      note = note & "White Balance Blue Shift:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(71)) Then
      x = makerTags.Item(sTag(71)).singleValue
      parm = Format(x, "##,##0")
      note = note & "White Balance Green Shift:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(72)) Then
      i = makerTags.Item(sTag(72)).singleValue
      Select Case i
        Case 0
          parm = "n/a"
        Case 1
          parm = "First"
        Case 2
          parm = "Second"
        Case Else
          parm = CStr(i)
      End Select
      If i > 0 Then note = note & "Flash Curtain:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(73)) Then
      i = makerTags.Item(sTag(73)).singleValue
      Select Case i
        Case 1
          parm = "On"
        Case 2
          parm = "Off"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Long Exposure N.R.:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(75)) And makerTags.Contains(sTag(76)) Then
      parm = Format(makerTags.Item(sTag(75)).singleValue, "##,##0") & " x " &
             Format(makerTags.Item(sTag(76)).singleValue, "##,##0") &
      note = note & "Panasonic Image Size:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(77)) Then
      v = makerTags.Item(sTag(77)).value
      If IsArray(v) AndAlso UBound(v) = 1 AndAlso (v(0) >= 0 And v(0) <= 1 And v(1) >= 0 And v(1) <= 1) Then
        parm = "(" & Format(v(0), "##,##0") & ", " & Format(v(1), "##,##0") & ")"
        note = note & "AF Point Position:" & tb & parm & "\par "
      End If
    End If

    If makerTags.Contains(sTag(112)) Then
      i = makerTags.Item(sTag(112)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "Low"
        Case 2
          parm = "Standard"
        Case 3
          parm = "High"
        Case 4
          parm = "Extended"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Intelligent Resolution:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(119)) Then
      x = makerTags.Item(sTag(119)).singleValue
      parm = Format(x, "##,##0")
      note = note & "Burst Speed:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(121)) Then
      i = makerTags.Item(sTag(121)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "Low"
        Case 2
          parm = "Standard"
        Case 3
          parm = "High"
        Case 4
          parm = "Extended"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Intelligent D-Range:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(124)) Then
      i = makerTags.Item(sTag(124)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Clear Retouch:" & tb & parm & "\par "

      If i = 1 AndAlso makerTags.Contains(sTag(163)) Then
        x = makerTags.Item(sTag(163)).singleValue
        parm = Format(x, "##,##0.###########")
        note = note & "Clear Retouch Value:" & tb & parm & "\par "
      End If

    End If

    If makerTags.Contains(sTag(134)) Then
      x = makerTags.Item(sTag(134)).singleValue
      If x > 32767 Then x = x - 65536
      parm = Format(x, "##,##0")
      If x <> -1 Then note = note & "Air Pressure (hPa):" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(137)) Then
      i = makerTags.Item(sTag(137)).singleValue
      Select Case i
        Case 0
          parm = "Auto"
        Case 1
          parm = "Standard or Custom"
        Case 2
          parm = "Vivid"
        Case 3
          parm = "Natural"
        Case 4
          parm = "Monochrome"
        Case 5
          parm = "Scenery"
        Case 6
          parm = "Portrait"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Photo Style:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(138)) Then
      i = makerTags.Item(sTag(138)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Clear Retouch:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(140)) Then
      x = makerTags.Item(sTag(140)).singleValue
      If x > 32767 Then x -= 65536
      parm = Format(x / 256, "##,##0.00") & " g"
      note = note & "Camera Acceleration Up:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(141)) Then
      x = makerTags.Item(sTag(141)).singleValue
      If x > 32767 Then x -= 65536
      parm = Format(x / 256, "##,##0.00") & " g"
      note = note & "Camera Acceleration Left:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(142)) Then
      x = makerTags.Item(sTag(142)).singleValue
      If x > 32767 Then x -= 65536
      parm = Format(x / 256, "##,##0.00") & " g"
      note = note & "Camera Acceleration Back:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(144)) Then
      x = makerTags.Item(sTag(144)).singleValue
      If x > 32767 Then x -= 65536
      parm = Format(x / 10, "##,##0.0") & "°"
      note = note & "Camera Roll Angle:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(145)) Then
      x = makerTags.Item(sTag(145)).singleValue
      If x > 32767 Then x -= 65536
      parm = Format(x / 10, "##,##0.0") & "°"
      note = note & "Camera Pitch Angle:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(143)) Then
      i = makerTags.Item(sTag(143)).singleValue
      Select Case i
        Case 0
          parm = "Normal"
        Case 1
          parm = "Rotated Clockwise"
        Case 2
          parm = "Upside Down"
        Case 3
          parm = "Rotated Counterclockcwise"
        Case 4
          parm = "Tilted Up"
        Case 5
          parm = "Tilted Down"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Camera Orientation:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(147)) Then
      i = makerTags.Item(sTag(147)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "Left to Right"
        Case 2
          parm = "Right to Left"
        Case 3
          parm = "Top to Bottom"
        Case 4
          parm = "Bottom to Top"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Sweep Panorama Direction:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(148)) Then
      x = makerTags.Item(sTag(148)).singleValue
      parm = Format(x, "##,##0")
      note = note & "Sweep Panorama Field of View:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(150)) Then
      i = makerTags.Item(sTag(150)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "Time Lapse"
        Case 2
          parm = "Stop-Motion Animation"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Timer Recording:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(157)) Then
      x = makerTags.Item(sTag(157)).singleValue
      parm = Format(x, "##,##0.###")
      note = note & "Internal ND Filter:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(158)) Then
      i = makerTags.Item(sTag(158)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 100
          parm = "1 EV"
        Case 200
          parm = "2 EV"
        Case 300
          parm = "3 EV	   	"
        Case 32868
          parm = "1 EV (Auto)"
        Case 32968
          parm = "2 EV (Auto)"
        Case 33068
          parm = "3 EV (Auto)"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "HDR:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(159)) Then
      i = makerTags.Item(sTag(159)).singleValue
      Select Case i
        Case 0
          parm = "Mechanical"
        Case 100
          parm = "Electronic"
        Case 200
          parm = "Hybrid"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Shutter Type:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(171)) Then
      i = makerTags.Item(sTag(171)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Touch AE:" & tb & parm & "\par "
    End If

    '    If makerTags.Contains(sTag(78)) Then
    ' v = makerTags.Item(sTag(78)).value
    ' If IsArray(v) AndAlso UBound(v) = 41 Then
    ' i = BitConverter.ToInt16(v, 0)
    ' parm = i
    ' For k As Integer = 0 To i - 1
    ' parm &= "\par " & tb & "(" & BitConverter.ToInt32(v, k * 16 + 4) & ", " & BitConverter.ToInt32(v, k * 16 + 8) & ", " & _
    '   BitConverter.ToInt32(v, k * 16 + 12) & ", " & BitConverter.ToInt32(v, k * 16 + 16) & ")"
    ' Next k
    ' note = note & "Faces Detected:" & tb & parm & "\par "
    ' End If
    ' End If

    If makerVersion > 0 AndAlso makerVersion <= 145 Then
      ' newer versions don't use these tags

      If makerTags.Contains(sTag(37)) Then
        v = makerTags.Item(sTag(37)).value
        If IsArray(v) Then
          parm = ""
          For i = 0 To UBound(v)
            If v(i) = 0 Then Exit For
            parm = parm & ChrW(v(i))
          Next i
          parm = Trim(parm)
          If Len(parm) > 0 Then note = note & "Internal Serial Number:" & tb & parm & "\par "
        End If
      End If

      If makerTags.Contains(sTag(81)) Then
        ' parm = makerTags.Item(sTag(81)).Value
        parm = makerTags.Item(sTag(81)).singleValue
        i = parm.IndexOf(Chr(0))
        If i >= 0 Then parm = parm.Substring(0, i)
        If Trim(parm) <> "" Then note = note & "Lens Type:" & tb & Trim(parm) & "\par "
      End If

      If makerTags.Contains(sTag(82)) Then
        parm = makerTags.Item(sTag(82)).singleValue
        i = parm.IndexOf(Chr(0))
        If i >= 0 Then parm = parm.Substring(0, i)
        If Trim(parm) <> "" Then note = note & "Lens Serial Number:" & tb & Trim(parm) & "\par "
      End If

      If makerTags.Contains(sTag(83)) Then
        parm = makerTags.Item(sTag(83)).singleValue
        i = parm.IndexOf(Chr(0))
        If i >= 0 Then parm = parm.Substring(0, i)
        If Trim(parm) <> "" Then note = note & "Accessory Type:" & tb & Trim(parm) & "\par "
      End If

      If makerTags.Contains(sTag(&H8010)) Then
        parm = makerTags.Item(sTag(&H8010)).singleValue
        parm = Trim(parm)
        If Len(parm) > 0 AndAlso parm.IndexOf("00:00:00") < 0 Then note = note & "Baby or Pet Age:" & tb & parm & "\par "
      End If


    ElseIf makerVersion >= 147 Then ' 33 has serial numbers, lens
      If makerTags.Contains(sTag(33)) Then
        v = makerTags.Item(sTag(33)).Value
        parm = ""
        If IsArray(v) Then
          For i = 0 To UBound(v)
            parm = parm & ChrW(v(i))
          Next i
        Else
          parm = ChrW(v)
        End If

        s = parm.Substring(0, 12)
        i = s.IndexOf(Chr(0))
        If i >= 0 Then s = s.Substring(0, i)
        s = s.Trim
        If Len(s) > 0 Then note = note & "Internal Serial Number:" & tb & s & "\par "

        s = parm.Substring(90, 35)
        i = s.IndexOf(Chr(0))
        If i >= 0 Then s = s.Substring(0, i)
        s = s.Trim
        If Len(s) > 0 Then note = note & "Lens Type:" & tb & s & "\par "

        s = parm.Substring(126, 13)
        i = s.IndexOf(Chr(0))
        If i >= 0 Then s = s.Substring(0, i)
        s = s.Trim
        If Len(s) > 0 Then note = note & "Lens Serial Number:" & tb & s & "\par "

      End If
    End If

    If makerTags.Contains(sTag(&H8001)) Then
      i = makerTags.Item(sTag(&H8001)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "Normal"
        Case 2
          parm = "Portrait"
        Case 3
          parm = "Scenery"
        Case 4
          parm = "Sports"
        Case 5
          parm = "Night Portrait"
        Case 6
          parm = "Program"
        Case 7
          parm = "Aperture Priority"
        Case 8
          parm = "Shutter Priority"
        Case 9
          parm = "Macro"
        Case 10
          parm = "Spot"
        Case 11
          parm = "Manual"
        Case 12
          parm = "Movie Preview"
        Case 13
          parm = "Panning"
        Case 14
          parm = "Simple"
        Case 15
          parm = "Color Effects"
        Case 18
          parm = "Fireworks"
        Case 19
          parm = "Party"
        Case 20
          parm = "Snow"
        Case 21
          parm = "Night Scenery"
        Case 22
          parm = "Food"
        Case 23
          parm = "Baby"
        Case 24
          parm = "Soft Skin"
        Case 25
          parm = "Candlelight"
        Case 26
          parm = "Starry Night"
        Case 27
          parm = "High Sensitivity"
        Case 28
          parm = "Panorama Assist"
        Case 29
          parm = "Underwater"
        Case 30
          parm = "Beach"
        Case 31
          parm = "Aerial Photo"
        Case 32
          parm = "Sunset"
        Case 33
          parm = "Pet"
        Case 34
          parm = "Intelligent ISO"
        Case 36
          parm = "High Speed Continuous Shooting"
        Case 37
          parm = "Intelligent Auto"
        Case 39
          parm = "Multi-aspect"
        Case 41
          parm = "Transform	   	"
        Case 42
          parm = "Flash Burst"
        Case 43
          parm = "Pin Hole"
        Case 44
          parm = "Film Grain"
        Case 45
          parm = "My Color"
        Case 46
          parm = "Photo Frame"
        Case 48
          parm = "Movie"
        Case 51
          parm = "HDR"
        Case 52
          parm = "Peripheral Defocus"
        Case 55
          parm = "Handheld Night Shot"
        Case 57
          parm = "3D"
        Case 59
          parm = "Creative Control"
        Case 62
          parm = "Panorama"
        Case 63
          parm = "Glass Through"
        Case 64
          parm = "HDR"
        Case 66
          parm = "Digital Filter"
        Case 67
          parm = "Clear Portrait"
        Case 68
          parm = "Silky Skin"
        Case 69
          parm = "Backlit Softness"
        Case 70
          parm = "Clear in Backlight"
        Case 71
          parm = "Relaxing Tone"
        Case 72
          parm = "Sweet Child's Face"
        Case 73
          parm = "Distinct Scenery"
        Case 74
          parm = "Bright Blue Sky"
        Case 75
          parm = "Romantic Sunset Glow"
        Case 76
          parm = "Vivid Sunset Glow"
        Case 77
          parm = "Glistening Water"
        Case 78
          parm = "Clear Nightscape"
        Case 79
          parm = "Cool Night Sky"
        Case 80
          parm = "Warm Glowing Nightscape"
        Case 81
          parm = "Artistic Nightscape"
        Case 82
          parm = "Glittering Illuminations"
        Case 83
          parm = "Clear Night Portrait"
        Case 84
          parm = "Soft Image of a Flower"
        Case 85
          parm = "Appetizing Food"
        Case 86
          parm = "Cute Desert"
        Case 87
          parm = "Freeze Animal Motion"
        Case 88
          parm = "Clear Sports Shot"
        Case 89
          parm = "Monochrome"
        Case 90
          parm = "Creative Control"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Scene Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H8004)) Then
      x = makerTags.Item(sTag(&H8004)).singleValue
      parm = Format(x, "####,##0")
      note = note & "White Balance Red Level:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H8005)) Then
      x = makerTags.Item(sTag(&H8005)).singleValue
      parm = Format(x, "####,##0")
      note = note & "White Balance Green Level:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H8006)) Then
      x = makerTags.Item(sTag(&H8006)).singleValue
      parm = Format(x, "####,##0")
      note = note & "White Balance Blue Level:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H8007)) Then
      i = makerTags.Item(sTag(&H8007)).singleValue
      Select Case i
        Case 1
          parm = "No"
        Case 2
          parm = "Yes"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Flash Fired:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H8008)) Then
      i = makerTags.Item(sTag(&H8008)).singleValue
      Select Case i
        Case 1
          parm = "Off"
        Case 2
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Text Stamp:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H8009)) Then
      i = makerTags.Item(sTag(&H8009)).singleValue
      Select Case i
        Case 1
          parm = "Off"
        Case 2
          parm = "On"
        Case Else
          parm = CStr(i)
      End Select
      note = note & "Text Stamp:" & tb & parm & "\par "
    End If

  End Sub ' panasonicMakernote

  Sub sonyMakernote(ByRef makerTags As Collection, ByRef note As String, intel As Boolean)

    Dim i, j, k As Integer
    Dim iu As Long
    Dim parm As String = ""
    Dim s As String
    Dim x As Double
    Dim bb() As Byte
    Dim v As Object
    Dim decode(255) As Byte
    Dim encode(255) As Byte

    ' get decode array
    For i1 As Integer = 0 To 248
      decode(i1) = (i1 ^ 3) Mod 249
      'encode((i1 ^ 3) Mod 249) = i1
    Next i1
    For i1 As Integer = 249 To 255 : encode(i1) = i1 : decode(i1) = i1 : Next i1

    If makerTags.Contains(sTag(&H102)) Then
      i = makerTags.Item(sTag(&H102)).singleValue
      Select Case i
        Case 0
          parm = "Raw"
        Case 1
          parm = "Super Fine"
        Case 2
          parm = "Fine"
        Case 3
          parm = "Standard"
        Case 4
          parm = "Economy"
        Case 5
          parm = "Extra Fine"
        Case 6
          parm = "raw+jpg"
        Case 7
          parm = "Compressed Raw"
        Case 8
          parm = "Compressed raw+jpg"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Quality:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H104)) Then
      x = makerTags.Item(sTag(&H104)).singleValue
      parm = Format(x, "##,##0.0###")
      note = note & "Flash Exposure Compensation:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H105)) Then
      i = makerTags.Item(sTag(&H105)).singleValue
      Select Case i
        Case 0
          parm = "(none)"
        Case &H48
          parm = "Minolta AF 2x APO (D) "
        Case &H50
          parm = "Minolta AF 2x APO II"
        Case &H88
          parm = "Minolta AF 1.4x APO (D)"
        Case &H90
          parm = "Minolta AF 1.4x APO II"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Teleconverter:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H112)) Then
      x = makerTags.Item(sTag(&H112)).singleValue
      parm = Format(x, "##,##0")
      note = note & "White Balance Fine Tune:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H115)) Then
      i = makerTags.Item(sTag(&H115)).singleValue
      Select Case i
        Case 0
          parm = "Auto"
        Case 1
          parm = "Color Temperature/Color Filter"
        Case 16
          parm = "Daylight"
        Case 32
          parm = "Cloudy"
        Case 48
          parm = "Shade"
        Case 64
          parm = "Tungsten"
        Case 80
          parm = "Flash"
        Case 96
          parm = "Fluorescent"
        Case 112
          parm = "Custom"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H1003)) Then
      v = makerTags.Item(sTag(&H1003)).value
      If IsArray(v) Then
        v = makerTags.Item(sTag(&H1003)).value
        k = 0
        For Each x In v
          If x < 100000 Then i = x Else i = 0 ' prevent overflow
          If i <> 0 Then
            k = 1
            Exit For
          End If
        Next x
        If k > 0 AndAlso UBound(v) >= 10 Then
          note = note & "Panorama Full Width:" & tb & Format(v(0), "##,##0") & "\par "
          note = note & "Panorama Full Height:" & tb & Format(v(1), "##,##0") & "\par "
          note = note & "Panorama Direction:" & tb & Format(v(2), "##,##0") & "\par "
          note = note & "Panorama Crop Left:" & tb & Format(v(3), "##,##0") & "\par "
          note = note & "Panorama Crop Top:" & tb & Format(v(4), "##,##0") & "\par "
          note = note & "Panorama Crop Right:" & tb & Format(v(5), "##,##0") & "\par "
          note = note & "Panorama Crop Bottom:" & tb & Format(v(6), "##,##0") & "\par "
          note = note & "Panorama Frame Width:" & tb & Format(v(7), "##,##0") & "\par "
          note = note & "Panorama Frame Height:" & tb & Format(v(8), "##,##0") & "\par "
          note = note & "Panorama Source Width:" & tb & Format(v(9), "##,##0") & "\par "
          note = note & "Panorama Source Height:" & tb & Format(v(10), "##,##0") & "\par "
        End If
      End If
    End If

    If makerTags.Contains(sTag(&H2002)) Then
      x = makerTags.Item(sTag(&H2002)).singleValue
      parm = Format(x, "##,##0")
      note = note & "Rating:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H2004)) Then
      x = makerTags.Item(sTag(&H2004)).singleValue
      parm = Format(x, "##,##0")
      note = note & "Contrast:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H2005)) Then
      x = makerTags.Item(sTag(&H2006)).singleValue
      parm = Format(x, "##,##0")
      note = note & "Saturation:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H2006)) Then
      x = makerTags.Item(sTag(&H2006)).singleValue
      parm = Format(x, "##,##0")
      note = note & "Sharpness:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H2007)) Then
      x = makerTags.Item(sTag(&H2007)).singleValue
      parm = Format(x, "##,##0")
      note = note & "Brightness:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H2008)) Then
      iu = makerTags.Item(sTag(&H2008)).singleValue
      Select Case iu
        Case 0
          parm = "Off"
        Case 1
          parm = "On (unused)"
        Case &H10001
          parm = "On (Dark Subtracted)"
        Case &HFFFF0000
          parm = "Off (65535)"
        Case &H90
          parm = "On (65535)"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Long Exposure Noise Reduction:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H2009)) Then
      iu = makerTags.Item(sTag(&H2009)).singleValue
      Select Case iu
        Case 0
          parm = "Off"
        Case 1
          parm = "Low"
        Case 2
          parm = "Normal"
        Case 3
          parm = "High"
        Case 256
          parm = "Auto"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "High ISO Noise Reduction:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H200A)) Then
      iu = makerTags.Item(sTag(&H200A)).singleValue
      i = iu \ 65536
      k = iu Mod 65536
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "Auto"
        Case 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26
          parm = Format((i - 16) * 0.5 + 1, "###0.0")
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "HDR:" & tb & parm & "\par "
      If i > 0 Then
        Select Case k
          Case 0
            parm = "Uncorrected Image"
          Case 1
            parm = "Good"
          Case 2
            parm = "Fail 1"
          Case 3
            parm = "Fail 2"
          Case Else
            parm = ""
        End Select
        If Len(parm) > 0 Then note = note & "HDR Image:" & tb & parm & "\par "
      End If
    End If

    If makerTags.Contains(sTag(&H200B)) Then
      iu = makerTags.Item(sTag(&H200B)).singleValue
      Select Case iu
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Multiframe Noise Reduction:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H200E)) Then
      iu = makerTags.Item(sTag(&H200E)).singleValue
      If sonyPictureEffect.ContainsKey(iu) Then
        note = note & "Picture effect:" & tb & sonyPictureEffect(i) & "\par "
      End If
    End If

    If makerTags.Contains(sTag(&H200F)) Then
      iu = makerTags.Item(sTag(&H200F)).singleValue
      Select Case iu
        Case 0
          parm = "Off"
        Case 1
          parm = "Low"
        Case 2
          parm = "Mid"
        Case 3
          parm = "High"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Soft Skin Effect:" & tb & parm & "\par "
    End If


    'If makerTags.Contains(sTag(&H2010)) Then ' doesn't work with rx10/iv?
    'v = makerTags.Item(sTag(&H2010)).value
    'bb = v
    'For i1 As Integer = 0 To UBound(bb)
    ' i = bb(i1)
    ' bb(i1) = encode(bb(i1))
    ' Next i1
    ' File.WriteAllBytes("c:\tmp.txt", bb)
    ' uz = New uExif
    ' uz.getIFDirectory(bb, k, True, True)
    ' tg.IFD = uz
    ' End If

    If makerTags.Contains(sTag(&H2011)) Then
      iu = makerTags.Item(sTag(&H2011)).singleValue
      Select Case iu
        Case 0
          parm = "Off"
        Case 2
          parm = "Auto"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Vignetting Correction:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H2012)) Then
      iu = makerTags.Item(sTag(&H2012)).singleValue
      Select Case iu
        Case 0
          parm = "Off"
        Case 2
          parm = "Auto"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Lateral Chromatic Aberration:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H2013)) Then
      iu = makerTags.Item(sTag(&H2013)).singleValue
      Select Case iu
        Case 0
          parm = "Off"
        Case 2
          parm = "Auto"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Distortion Correction Setting:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H2014)) Then
      v = makerTags.Item(sTag(&H2014)).value
      If IsArray(v) AndAlso UBound(v) >= 1 Then
        note = note & "White Balance Amber Shift:" & tb & Format(v(0), "##,##0") & "\par "
        note = note & "White Balance Magenta Shift:" & tb & Format(v(1), "##,##0") & "\par "
      End If
    End If

    If makerTags.Contains(sTag(&H2026)) Then
      v = makerTags.Item(sTag(&H2026)).value
      If IsArray(v) AndAlso UBound(v) >= 1 Then
        note = note & "White Balance Amber Precice Shift:" & tb & Format(v(0), "##,##0") & "\par "
        note = note & "White Balance Magenta Precise Shift:" & tb & Format(v(1), "##,##0") & "\par "
      End If
    End If

    If makerTags.Contains(sTag(&H2016)) Then
      iu = makerTags.Item(sTag(&H2016)).singleValue
      Select Case iu
        Case 0
          parm = "No"
        Case 1
          parm = "Yes"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Auto Portrait Framed:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H2017)) Then
      iu = makerTags.Item(sTag(&H2017)).singleValue
      Select Case iu
        Case 0
          parm = "Did Not Fire"
        Case 1
          parm = "Flash Fired"
        Case 2
          parm = "External Flash Fired"
        Case 3
          parm = "Wireless Controlled Flash"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Flash Action:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H201A)) Then
      iu = makerTags.Item(sTag(&H201A)).singleValue
      Select Case iu
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Electronic Front Curtain Shutter:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H201B)) Then
      iu = makerTags.Item(sTag(&H201B)).singleValue
      Select Case iu
        Case 0
          parm = "Manual"
        Case 2
          parm = "AF-S"
        Case 3
          parm = "AF-C"
        Case 4
          parm = "AF-A"
        Case 6
          parm = "DMF"
        Case 7
          parm = "AF-D"
        Case Else
          parm = iu
      End Select
      If Len(parm) > 0 Then note = note & "Focus Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H201C)) Then
      iu = makerTags.Item(sTag(&H201C)).singleValue
      Select Case iu
        Case 0
          parm = "Multi"
        Case 1
          parm = "Center"
        Case 3
          parm = "Flexible Spot"
        Case 4
          parm = "Flexible Spot (or local)"
        Case 8, 11
          parm = "Zone"
        Case 9
          parm = "Center (or spot)"
        Case 11
          parm = "Zone"
        Case 12
          parm = "Expanded Flexible Spot"
        Case Else
          parm = iu
      End Select
      If Len(parm) > 0 Then note = note & "Auto Focus Area Mode:" & tb & parm & "\par "
    End If


    If makerTags.Contains(sTag(&H201D)) Then
      v = makerTags.Item(sTag(&H201D)).value
      If IsArray(v) AndAlso UBound(v) = 1 AndAlso (v(0) >= 0 And v(0) <= 1 And v(1) >= 0 And v(1) <= 1) Then
        parm = "(" & Format(v(0), "##,##0") & ", " & Format(v(1), "##,##0") & ")"
        note = note & "Flexible Spot Position:" & tb & parm & "\par "
      End If
    End If

    If makerTags.Contains(sTag(&H201E)) Then
      iu = makerTags.Item(sTag(&H201E)).singleValue
      If Len(parm) > 0 Then note = note & "Autofocus Point Select:" & tb & iu & "\par "
    End If

    If makerTags.Contains(sTag(&H2020)) Then
      v = makerTags.Item(sTag(&H2020)).value
      If IsArray(v) Then
        k = 0
        For Each i In v
          If i > 0 Then
            k = 1
            Exit For
          End If
        Next i

        If k >= 1 Then
          For Each i In v
            parm &= Hex$(i) & " "
          Next i
          note = note & "Auto Focus Points Used:" & tb & parm & "\par "
        End If
      End If
    End If

    If makerTags.Contains(sTag(&H2021)) Then
      iu = makerTags.Item(sTag(&H2021)).singleValue
      Select Case iu
        Case 0
          parm = "Off"
        Case 1
          parm = "Face tracking"
        Case 2
          parm = "Lock on Autofocus"
        Case Else
          parm = iu
      End Select
      If Len(parm) > 0 Then note = note & "Auto Focus Area Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H2023)) Then
      iu = makerTags.Item(sTag(&H2023)).singleValue
      Select Case iu
        Case 0
          parm = "Normal"
        Case 1
          parm = "High"
        Case Else
          parm = iu
      End Select
      If Len(parm) > 0 Then note = note & "Multiframe NR Effect:" & tb & parm & "\par "
    End If




    If makerTags.Contains(sTag(&H2027)) Then
      v = makerTags.Item(sTag(&H2027)).value
      parm = ""
      If IsArray(v) Then
        k = 0
        For Each i In v
          If i > 0 Then
            k = 1
            Exit For
          End If
        Next i

        If k >= 1 Then
          For Each i In v
            parm &= i & " "
          Next i
          note = note & "Focus Location:" & tb & parm & "\par "
        End If
      End If
    End If

    If makerTags.Contains(sTag(&H2028)) Then
      v = makerTags.Item(sTag(&H2028)).value
      If v.length = 2 Then s = Str(v(0)) & Str(v(1)) Else s = ""
      Select Case s
        Case "00"
          parm = ""
        Case "10"
          parm = "off"
        Case "11"
          parm = "standard"
        Case "12"
          parm = "high"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Variable Low Pass Filter:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H2028)) Then
      iu = makerTags.Item(sTag(&H2028)).singlevalue
      If iu = 0 Then parm = "compressed" Else If iu = 1 Then parm = "compressed" Else parm = ""
      If Len(parm) > 0 Then note = note & "Raw File Type:" & tb & parm & "\par "
    End If

    'If makerTags.Contains(sTag(&H202A)) Then
    'v = makerTags.Item(sTag(&H202A)).value
    'If IsArray(v) AndAlso UBound(v) >= 62 Then
    '
    '    'k = v(0)
    '    'note = note & "Focal Plane AF Points Used:" & tb & k & "\par "
    '    'i = getWord(v, 1, False)
    '    'k = getWord(v, 3, False)
    '    'parm = "(" & Format(i, "##,##0") & ", " & Format(k, "##,##0") & ")"
    '    'note = note & "Focal Plane AF Point Area:" & tb & parm & "\par "
    '    'parm = ""
    '    'For i1 As Integer = 0 To 14
    '    ' i = getWord(v, 5 + i1 * 4, False)
    '    ' k = getWord(v, 7 + i1 * 4, False)
    '    ' parm = "(" & Format(i, "##,##0") & ", " & Format(k, "##,##0") & ")"
    '    ' note = note & "Focal Plane AF Point " & i1 + 1 & ":" & tb & parm & "\par "
    '    ' Next i1
    '
    '    k = 0
    '    For Each i In v
    ' If i > 0 Then
    ' k = 1
    ' Exit For
    ' End If
    ' Next i

    'If k >= 1 Then
    ' parm = ""
    ' For Each i In v
    ' parm &= i & " "
    ' Next i
    ' note = note & "Focus Plane AF Point Location:" & tb & parm & "\par "
    ' End If
    ' End If
    'End If

    If makerTags.Contains(sTag(&H202B)) Then
      iu = makerTags.Item(sTag(&H202B)).singleValue
      Select Case iu
        Case 0
          parm = "Standard"
        Case 1
          parm = "Ambience"
        Case 2
          parm = "White"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Priority Set in AWB:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H202C)) Then
      iu = makerTags.Item(sTag(&H202C)).singleValue
      Select Case iu
        Case &H100
          parm = "Multi-segment"
        Case &H200
          parm = "Center-weighted average"
        Case &H301
          parm = "Spot (standard)"
        Case &H302
          parm = "Spot (large)"
        Case &H400
          parm = "Average"
        Case &H500
          parm = "Highlight"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Metering Mode 2:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H202D)) Then
      x = makerTags.Item(sTag(&H202D)).singlevalue
      parm = Format(x, "#0.##")
      If Len(parm) > 0 Then note = note & "Exposure Standard Adjustment:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H202E)) Then
      v = makerTags.Item(sTag(&H202E)).value
      If v.length = 2 Then s = Str(v(0)) & Str(v(1)) Else s = ""
      Select Case s
        Case "00"
          parm = ""
        Case "01"
          parm = "standard jpg"
        Case "02"
          parm = "fine jpg"
        Case "03"
          parm = "extra fine jpg"
        Case "10"
          parm = "raw"
        Case "11"
          parm = "raw = standard jpg"
        Case "12"
          parm = "raw + fine jpg"
        Case "13"
          parm = "raw + extra fine jpg"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Quality:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H2031)) Then
      v = makerTags.Item(sTag(&H2031)).singlevalue
      parm = v
      If Len(parm) > 0 Then note = note & "Serial number:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&H3000)) Then
      v = makerTags.Item(sTag(&H3000)).value
      parm = ""
      If IsArray(v) Then
        k = 0
        For Each i In v
          If i > 0 Then
            k = 1
            Exit For
          End If
        Next i

        If k >= 1 And UBound(v) >= 90 Then
          parm = UTF8bare.GetString(v, 6, 19)
          parm = parm.Trim(whiteSpace)
          If Len(parm) > 0 AndAlso parm.Contains(":") And parm.Contains("20") Then
            note = note & "Sony Date and Time:" & tb & parm & "\par "
          End If

          j = getWord(v, 26, True)
          k = getWord(v, 28, True)
          parm = Format(k, "##,##0") & " x " & Format(j, "##,##0")
          If Len(parm) > 0 Then note = note & "Sony Image Size:" & tb & parm & "\par "

          k = getWord(v, 48, True)
          If Len(parm) > 0 Then note = note & "Faces Detected:" & tb & k & "\par "

          parm = UTF8bare.GetString(v, 52, 16)
          parm = parm.Trim(whiteSpace)
          If Len(parm) > 0 Then note = note & "Meta Version:" & tb & parm & "\par "

        End If
      End If
    End If

    'If makerTags.Contains(sTag(&H9050)) Then ' doesn't work for rx10m4? couldn't get any of the encrypted tags.
    'v = makerTags.Item(sTag(&H9050)).value
    'bb = v
    'For i1 As Integer = 0 To UBound(bb)
    ' 'bb(i1) = decode(bb(i1))
    ' Next i1
    ' File.WriteAllBytes("c:\tmp.txt", bb)
    ' End If

    If makerTags.Contains(sTag(&HB000)) Then
      v = makerTags.Item(sTag(&HB000)).Value
      If v.length = 4 Then
        If v(0) = 0 And v(1) = 0 And v(2) = 0 And v(3) = 2 Then parm = "JPG"
        If v(0) = 1 And v(1) = 0 And v(2) = 0 And v(3) = 0 Then parm = "SR2"
        If v(0) = 2 And v(1) = 0 And v(2) = 0 And v(3) = 0 Then parm = "ARW 1.0"
        If v(0) = 3 And v(1) = 0 And v(2) = 0 And v(3) = 0 Then parm = "ARW 2.0"
        If v(0) = 3 And v(1) = 1 And v(2) = 0 And v(3) = 0 Then parm = "ARW 2.1"
        If v(0) = 3 And v(1) = 2 And v(2) = 0 And v(3) = 0 Then parm = "ARW 2.2"
        If v(0) = 3 And v(1) = 3 And v(2) = 0 And v(3) = 0 Then parm = "ARW 2.3"
        If v(0) = 3 And v(1) = 3 And v(2) = 1 And v(3) = 0 Then parm = "ARW 2.3.1"
        If v(0) = 3 And v(1) = 3 And v(2) = 2 And v(3) = 0 Then parm = "ARW 2.3.2"
        If v(0) = 3 And v(1) = 3 And v(2) = 3 And v(3) = 0 Then parm = "ARW 2.3.3"
        If v(0) = 3 And v(1) = 3 And v(2) = 5 And v(3) = 0 Then parm = "ARW 2.3.5"
        note = note & "File format:" & tb & parm & "\par "
      End If
    End If

    If makerTags.Contains(sTag(&HB001)) Then
      iu = makerTags.Item(sTag(&HB001)).singleValue
      If sonyModelID.ContainsKey(iu) Then
        note = note & "Sony model ID:" & tb & sonyModelID(iu) & "\par "
      End If
    End If

    If makerTags.Contains(sTag(&HB020)) Then
      parm = makerTags.Item(sTag(&HB020)).singleValue
      parm = parm.Trim
      If Trim(parm) <> "" Then note = note & "Creative Style:" & tb & Trim(parm) & "\par "
    End If

    If makerTags.Contains(sTag(&HB021)) Then
      x = makerTags.Item(sTag(&HB021)).singleValue
      parm = Format(x, "####,##0")
      note = note & "Color Temperature:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&HB022)) Then
      x = makerTags.Item(sTag(&HB022)).singleValue
      parm = Format(x, "####,##0")
      note = note & "Color Compensation Filter:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&HB023)) Then
      i = makerTags.Item(sTag(&HB023)).singleValue
      Select Case i
        Case 0
          parm = "Standard"
        Case 1
          parm = "Portrait"
        Case 2
          parm = "Text"
        Case 3
          parm = "Night Scene"
        Case 4
          parm = "Sunset"
        Case 5
          parm = "Sports"
        Case 6
          parm = "Landscape"
        Case 7
          parm = "Night Portrait"
        Case 8
          parm = "Macro"
        Case 9
          parm = "Super Macro"
        Case 16
          parm = "Auto"
        Case 17
          parm = "Night View/Portrait"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Scene Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&HB024)) Then
      i = makerTags.Item(sTag(&HB024)).singleValue
      Select Case i
        Case 0
          parm = "ISO Setting Used"
        Case 1
          parm = "High Key"
        Case 2
          parm = "Low Key"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Zone Matching:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&HB025)) Then
      i = makerTags.Item(sTag(&HB025)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "Standard"
        Case 2
          parm = "Advanced Auto"
        Case 8
          parm = "Advanced Lv1"
        Case 9
          parm = "Advanced Lv2"
        Case 10
          parm = "Advanced Lv3"
        Case 11
          parm = "Advanced Lv4"
        Case 12
          parm = "Advanced Lv5"
        Case 16
          parm = "Lv1"
        Case 17
          parm = "Lv2"
        Case 18
          parm = "Lv3"
        Case 19
          parm = "Lv4"
        Case 20
          parm = "Lv5"
        Case Else
          parm = i
      End Select
      If Len(parm) > 0 Then note = note & "Dynamic Range Optimizer:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&HB026)) Then
      i = makerTags.Item(sTag(&HB026)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Image Stabilization:" & tb & parm & "\par "
    End If

    'If makerTags.Contains(sTag(&HB027)) Then
    'x = makerTags.Item(sTag(&HB027)).singleValue
    'parm = Format(x, "##,##0.#")
    'note = note & "Lens Type:" & tb & parm & "\par "
    'End If

    If makerTags.Contains(sTag(&HB029)) Then
      iu = makerTags.Item(sTag(&HB029)).singleValue
      If sonyColorMode.ContainsKey(iu) Then
        note = note & "Color mode:" & tb & sonyColorMode(iu) & "\par "
      End If
    End If


    If makerTags.Contains(sTag(&HB02A)) Then
      v = makerTags.Item(sTag(&HB02A)).Value
      parm = ""
      If v.length = 8 And IsNumeric(v(0)) Then
        parm = Format(v(1) * 256 + v(2), "x1") & " - " & Format(v(3) * 256 + v(4), "x1") & " mm, f" & Format(Val(Hex$(v(5))) / 10, "#0.0") & " - f" & Format(Val(Hex$(v(6))) / 10, "#0.0")
      End If
      If parm <> "" Then note &= "Lens Specification:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&HB02B)) Then
      v = makerTags.Item(sTag(&HB02B)).Value
      If v.length = 2 Then
        note = note & "Full image size:" & tb & v(0) & " x " & v(1) & "\par "
      End If
    End If

    If makerTags.Contains(sTag(&HB02C)) Then
      v = makerTags.Item(sTag(&HB02C)).Value
      If v.length = 2 Then
        note = note & "Preview image size:" & tb & v(0) & " x " & v(1) & "\par "
      End If
    End If

    If makerTags.Contains(sTag(&HB040)) Then
      i = makerTags.Item(sTag(&HB040)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Macro:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&HB041)) Then
      iu = makerTags.Item(sTag(&HB041)).singleValue
      Select Case iu
        Case 0
          parm = "Auto "
        Case 5
          parm = "Landscape "
        Case 6
          parm = "Program  "
        Case 7
          parm = "Aperture Priority "
        Case 8
          parm = "Shutter Priority"
        Case 9
          parm = "Night Scene"
        Case 15
          parm = "Manual"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Exposure Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&HB042)) Then
      iu = makerTags.Item(sTag(&HB042)).singleValue
      Select Case iu
        Case 1
          parm = "AF-S"
        Case 2
          parm = "AF-C"
        Case 4
          parm = "Permanent AF"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Focus Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&HB043)) Then
      iu = makerTags.Item(sTag(&HB043)).singleValue
      Select Case iu
        Case 0
          parm = "Multi"
        Case 1
          parm = "Center"
        Case 2
          parm = "Spot"
        Case 3
          parm = "Flexible Spot"
        Case 10
          parm = "Selective"
        Case 14
          parm = "Tracking"
        Case 15
          parm = "Face Tracking"
        Case 255
          parm = "Manual"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "AF Area Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&HB044)) Then
      iu = makerTags.Item(sTag(&HB044)).singleValue
      Select Case iu
        Case 0
          parm = "Off"
        Case 1
          parm = "Auto"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Focus Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&HB047)) Then
      iu = makerTags.Item(sTag(&HB047)).singleValue
      Select Case iu
        Case 0
          parm = "Normal"
        Case 1
          parm = "Fine"
        Case 1
          parm = "Extra Fine"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "JPG Quality:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&HB048)) Then
      i = makerTags.Item(sTag(&HB048)).singleValue
      If i = -32768 Then
        parm = "low"
      ElseIf i = 32767 Then
        parm = "high"
      Else
        parm = Str(i \ 3).Trim
        If i Mod 3 <> 0 Then parm &= " " & Str(Abs(i Mod 3)) & "/3" Else If i <> 0 Then parm &= ".0"
        If i > 0 Then parm = "+" & parm
      End If
      If Len(parm) > 0 Then note = note & "Flash Level:" & tb & parm.Trim & "\par "
    End If

    If makerTags.Contains(sTag(&HB049)) Then
      iu = makerTags.Item(sTag(&HB049)).singleValue
      Select Case iu
        Case 0
          parm = "Normal"
        Case 2
          parm = "Continuous"
        Case 5
          parm = "Exposure Bracketing"
        Case 6
          parm = "White Balance Bracketing"
        Case 8
          parm = "DRO Bracketing"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Release Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&HB04B)) Then
      i = makerTags.Item(sTag(&HB04B)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On (Continuous)"
        Case 2
          parm = "On (Shooting)"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "Anti-Blur:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&HB04E)) Then
      i = makerTags.Item(sTag(&HB04E)).singleValue

      Select Case i
        Case 0
          parm = "Manual"
        Case 2
          parm = "AF-S"
        Case 3
          parm = "AF-C"
        Case 5
          parm = "Semi-manual"
        Case 6
          parm = "DMF"
        Case Else
          parm = i
      End Select
      If Len(parm) > 0 Then note = note & "Focus Mode:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&HB04F)) Then
      i = makerTags.Item(sTag(&HB04F)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "Standard"
        Case 2
          parm = "Plus"
        Case Else
          parm = i
      End Select
      If Len(parm) > 0 Then note = note & "Dynamic Range Optimizer:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&HB050)) Then
      i = makerTags.Item(sTag(&HB050)).singleValue
      Select Case i
        Case 0
          parm = "Normal"
        Case 1
          parm = "High"
        Case 2
          parm = "Low"
        Case 3
          parm = "Off"
        Case Else
          parm = ""
      End Select
      If Len(parm) > 0 Then note = note & "High ISO Noise Reduction 2:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&HB052)) Then
      i = makerTags.Item(sTag(&HB052)).singleValue
      Select Case i
        Case 0
          parm = "Off"
        Case 1
          parm = "On"
        Case 2
          parm = "Advanced"
        Case Else
          parm = i
      End Select
      If Len(parm) > 0 Then note = note & "Intelligent Auto:" & tb & parm & "\par "
    End If

    If makerTags.Contains(sTag(&HB054)) Then
      i = makerTags.Item(sTag(&HB054)).singleValue
      Select Case i
        Case 0
          parm = "Auto"
        Case 4
          parm = "Custom"
        Case 5
          parm = "Daylight"
        Case 6
          parm = "Cloudy"
        Case 7
          parm = "Cool White Fluorescent"
        Case 8
          parm = "Day White Fluorescent"
        Case 9
          parm = "Daylight Fluorescent"
        Case 10
          parm = "Incandescent2"
        Case 11
          parm = "Warm White Fluorescent"
        Case 14
          parm = "Incandescent"
        Case 15
          parm = "Flash"
        Case 17
          parm = "Underwater 1 (Blue Water)"
        Case 18
          parm = "Underwater 2 (Green Water)"
        Case 19
          parm = "Underwater Auto"
        Case Else
          parm = i
      End Select
      If Len(parm) > 0 Then note = note & "White Balance:" & tb & parm & "\par "
    End If


  End Sub ' sonyMakernote

  Function canonDescr(ByVal Mode As Integer, ByVal Value As Integer) As String
    ' gets a description for a canon makernote parameter
    ' mode = 1: White Balance
    ' mode = 2: Picture Style
    ' mode = 3: Custom Picture Style

    canonDescr = ""

    If Mode = 1 Then ' white balance
      Select Case Value
        Case 0 : canonDescr = "Auto "
        Case 1, &H100 : canonDescr = "Daylight "
        Case 2, &H200 : canonDescr = "Cloudy "
        Case 3 : canonDescr = "Tungsten "
        Case 4 : canonDescr = "Fluorescent "
        Case 5 : canonDescr = "Flash "
        Case 6, &H600 : canonDescr = "Custom "
        Case 7 : canonDescr = "Black & White "
        Case 8 : canonDescr = "Shade "
        Case 9, &H900 : canonDescr = "Manual Temperature (Kelvin) "
        Case 10 : canonDescr = "PC Set1"
        Case 11 : canonDescr = "PC Set2"
        Case 12 : canonDescr = "PC Set3"
        Case 14 : canonDescr = "Daylight Fluorescent"
        Case 15 : canonDescr = "Custom 1"
        Case 17 : canonDescr = "Underwater"
        Case 16 : canonDescr = "Custom 2"
        Case 18 : canonDescr = "Custom 3"
        Case 19 : canonDescr = "Custom 4"
        Case 20 : canonDescr = "PC Set 4"
        Case 21 : canonDescr = "PC Set 5"
        Case Else : canonDescr = ""
      End Select

    ElseIf Mode = 2 Then ' picture style
      Select Case Value
        Case &H0 : canonDescr = "(none) "
        Case &H1 : canonDescr = "Standard "
        Case &H2 : canonDescr = "Portrait "
        Case &H3 : canonDescr = "High Saturation "
        Case &H4 : canonDescr = "Adobe RGB "
        Case &H5 : canonDescr = "Low Saturation "
        Case &H6 : canonDescr = "CM Set 1 "
        Case &H7 : canonDescr = "CM Set 2 "
        Case &H21 : canonDescr = "User Defined 1 "
        Case &H22 : canonDescr = "User Defined 2 "
        Case &H23 : canonDescr = "User Defined 3"
        Case &H41 : canonDescr = "PC 1"
        Case &H42 : canonDescr = "PC 2"
        Case &H43 : canonDescr = "PC 3"
        Case &H81 : canonDescr = "Standard"
        Case &H82 : canonDescr = "Portrait"
        Case &H83 : canonDescr = "Landscape"
        Case &H84 : canonDescr = "Neutral"
        Case &H85 : canonDescr = "Faithful"
        Case &H86 : canonDescr = "Monochrome"
        Case Else : canonDescr = ""
      End Select

    ElseIf Mode = 3 Then ' custom picture Style
      Select Case Value
        Case &H41 : canonDescr = "Nostalgia"
        Case &H42 : canonDescr = "Clear"
        Case &H43 : canonDescr = "Twilight"
        Case &H81 : canonDescr = "Standard"
        Case &H82 : canonDescr = "Portrait"
        Case &H83 : canonDescr = "Landscape"
        Case &H84 : canonDescr = "Neutral"
        Case &H85 : canonDescr = "Faithful"
        Case &H86 : canonDescr = "Monochrome"
        Case Else : canonDescr = ""
      End Select

    ElseIf Mode = 4 Then ' lens type
      Select Case Value
        Case 1 : canonDescr = "Canon EF 50mm f/1.8"
        Case 2 : canonDescr = "Canon EF 28mm f/2.8"
        Case 3 : canonDescr = "Canon EF 135mm f/2.8 Soft"
        Case 4 : canonDescr = "Canon EF 35-105mm f/3.5-4.5 or Sigma Lens"
        Case 5 : canonDescr = "Canon EF 35-70mm f/3.5-4.5"
        Case 6 : canonDescr = "Canon EF 28-70mm f/3.5-4.5 or Sigma or Tokina Lens"
        Case 7 : canonDescr = "Canon EF 100-300mm f/5.6L"
        Case 8 : canonDescr = "Canon EF 100-300mm f/5.6 or Sigma or Tokina Lens"
        Case 9 : canonDescr = "Canon EF 70-210mm f/4 or Sigma Lens"
        Case 10 : canonDescr = "Canon EF 50mm f/2.5 Macro or Sigma Lens"
        Case 11 : canonDescr = "Canon EF 35mm f/2"
        Case 13 : canonDescr = "Canon EF 15mm f/2.8 Fisheye"
        Case 14 : canonDescr = "Canon EF 50-200mm f/3.5-4.5L"
        Case 15 : canonDescr = "Canon EF 50-200mm f/3.5-4.5"
        Case 16 : canonDescr = "Canon EF 35-135mm f/3.5-4.5"
        Case 17 : canonDescr = "Canon EF 35-70mm f/3.5-4.5A"
        Case 18 : canonDescr = "Canon EF 28-70mm f/3.5-4.5"
        Case 20 : canonDescr = "Canon EF 100-200mm f/4.5A"
        Case 21 : canonDescr = "Canon EF 80-200mm f/2.8L"
        Case 22 : canonDescr = "Canon EF 20-35mm f/2.8L or Tokina Lens"
        Case 23 : canonDescr = "Canon EF 35-105mm f/3.5-4.5"
        Case 24 : canonDescr = "Canon EF 35-80mm f/4-5.6 Power Zoom"
        Case 25 : canonDescr = "Canon EF 35-80mm f/4-5.6 Power Zoom"
        Case 26 : canonDescr = "Canon EF 100mm f/2.8 Macro or Other Lens"
        Case 27 : canonDescr = "Canon EF 35-80mm f/4-5.6"
        Case 28 : canonDescr = "Canon EF 80-200mm f/4.5-5.6 or Tamron Lens"
        Case 29 : canonDescr = "Canon EF 50mm f/1.8 MkII"
        Case 30 : canonDescr = "Canon EF 35-105mm f/4.5-5.6"
        Case 31 : canonDescr = "Canon EF 75-300mm f/4-5.6 or Tamron Lens"
        Case 32 : canonDescr = "Canon EF 24mm f/2.8 or Sigma Lens"
        Case 35 : canonDescr = "Canon EF 35-80mm f/4-5.6"
        Case 36 : canonDescr = "Canon EF 38-76mm f/4.5-5.6"
        Case 37 : canonDescr = "Canon EF 35-80mm f/4-5.6 or Tamron Lens"
        Case 38 : canonDescr = "Canon EF 80-200mm f/4.5-5.6"
        Case 39 : canonDescr = "Canon EF 75-300mm f/4-5.6"
        Case 40 : canonDescr = "Canon EF 28-80mm f/3.5-5.6"
        Case 41 : canonDescr = "Canon EF 28-90mm f/4-5.6"
        Case 42 : canonDescr = "Canon EF 28-200mm f/3.5-5.6 or Tamron Lens"
        Case 43 : canonDescr = "Canon EF 28-105mm f/4-5.6"
        Case 44 : canonDescr = "Canon EF 90-300mm f/4.5-5.6"
        Case 45 : canonDescr = "Canon EF-S 18-55mm f/3.5-5.6"
        Case 46 : canonDescr = "Canon EF 28-90mm f/4-5.6"
        Case 48 : canonDescr = "Canon EF-S 18-55mm f/3.5-5.6 IS"
        Case 49 : canonDescr = "Canon EF-S 55-250mm f/4-5.6 IS"
        Case 50 : canonDescr = "Canon EF-S 18-200mm f/3.5-5.6 IS"
        Case 51 : canonDescr = "Canon EF-S 18-135mm f/3.5-5.6 IS"
        Case 94 : canonDescr = "Canon TS-E 17mm f/4L"
        Case 95 : canonDescr = "Canon TS-E 24.0mm f/3.5 L II"
        Case 124 : canonDescr = "Canon MP-E 65mm f/2.8 1-5x Macro Photo"
        Case 125 : canonDescr = "Canon TS-E 24mm f/3.5L"
        Case 126 : canonDescr = "Canon TS-E 45mm f/2.8"
        Case 127 : canonDescr = "Canon TS-E 90mm f/2.8"
        Case 129 : canonDescr = "Canon EF 300mm f/2.8L"
        Case 130 : canonDescr = "Canon EF 50mm f/1.0L"
        Case 131 : canonDescr = "Canon EF 28-80mm f/2.8-4L or Sigma Lens"
        Case 132 : canonDescr = "Canon EF 1200mm f/5.6L"
        Case 134 : canonDescr = "Canon EF 600mm f/4L IS"
        Case 135 : canonDescr = "Canon EF 200mm f/1.8L"
        Case 136 : canonDescr = "Canon EF 300mm f/2.8L"
        Case 137 : canonDescr = "Canon EF 85mm f/1.2L or Sigma Lens"
        Case 138 : canonDescr = "Canon EF 28-80mm f/2.8-4L"
        Case 139 : canonDescr = "Canon EF 400mm f/2.8L"
        Case 140 : canonDescr = "Canon EF 500mm f/4.5L"
        Case 141 : canonDescr = "Canon EF 500mm f/4.5L"
        Case 142 : canonDescr = "Canon EF 300mm f/2.8L IS"
        Case 143 : canonDescr = "Canon EF 500mm f/4L IS"
        Case 144 : canonDescr = "Canon EF 35-135mm f/4-5.6 USM"
        Case 145 : canonDescr = "Canon EF 100-300mm f/4.5-5.6 USM"
        Case 146 : canonDescr = "Canon EF 70-210mm f/3.5-4.5 USM"
        Case 147 : canonDescr = "Canon EF 35-135mm f/4-5.6 USM"
        Case 148 : canonDescr = "Canon EF 28-80mm f/3.5-5.6 USM"
        Case 149 : canonDescr = "Canon EF 100mm f/2 USM"
        Case 150 : canonDescr = "Canon EF 14mm f/2.8L or Sigma Lens"
        Case 151 : canonDescr = "Canon EF 200mm f/2.8L"
        Case 152 : canonDescr = "Canon EF 300mm f/4L IS or Sigma Lens"
        Case 153 : canonDescr = "Canon EF 35-350mm f/3.5-5.6L or Sigma or Tamron Lens"
        Case 154 : canonDescr = "Canon EF 20mm f/2.8 USM"
        Case 155 : canonDescr = "Canon EF 85mm f/1.8 USM"
        Case 156 : canonDescr = "Canon EF 28-105mm f/3.5-4.5 USM"
        Case 160 : canonDescr = "Canon EF 20-35mm f/3.5-4.5 USM or Tamron Lens"
        Case 161 : canonDescr = "Canon EF 28-70mm f/2.8L or Sigma or Tamron Lens"
        Case 162 : canonDescr = "Canon EF 200mm f/2.8L"
        Case 163 : canonDescr = "Canon EF 300mm f/4L"
        Case 164 : canonDescr = "Canon EF 400mm f/5.6L"
        Case 165 : canonDescr = "Canon EF 70-200mm f/2.8 L"
        Case 166 : canonDescr = "Canon EF 70-200mm f/2.8 L + 1.4x"
        Case 167 : canonDescr = "Canon EF 70-200mm f/2.8 L + 2x"
        Case 168 : canonDescr = "Canon EF 28mm f/1.8 USM"
        Case 169 : canonDescr = "Canon EF 17-35mm f/2.8L or Sigma Lens"
        Case 170 : canonDescr = "Canon EF 200mm f/2.8L II"
        Case 171 : canonDescr = "Canon EF 300mm f/4L"
        Case 172 : canonDescr = "Canon EF 400mm f/5.6L"
        Case 173 : canonDescr = "Canon EF 180mm Macro f/3.5L or Sigma Lens"
        Case 174 : canonDescr = "Canon EF 135mm f/2L"
        Case 175 : canonDescr = "Canon EF 400mm f/2.8L"
        Case 176 : canonDescr = "Canon EF 24-85mm f/3.5-4.5 USM"
        Case 177 : canonDescr = "Canon EF 300mm f/4L IS"
        Case 178 : canonDescr = "Canon EF 28-135mm f/3.5-5.6 IS"
        Case 179 : canonDescr = "Canon EF 24mm f/1.4L"
        Case 180 : canonDescr = "Canon EF 35mm f/1.4L"
        Case 181 : canonDescr = "Canon EF 100-400mm f/4.5-5.6L IS + 1.4x"
        Case 182 : canonDescr = "Canon EF 100-400mm f/4.5-5.6L IS + 2x"
        Case 183 : canonDescr = "Canon EF 100-400mm f/4.5-5.6L IS"
        Case 184 : canonDescr = "Canon EF 400mm f/2.8L + 2x"
        Case 185 : canonDescr = "Canon EF 600mm f/4L IS"
        Case 186 : canonDescr = "Canon EF 70-200mm f/4L"
        Case 187 : canonDescr = "Canon EF 70-200mm f/4L + 1.4x"
        Case 188 : canonDescr = "Canon EF 70-200mm f/4L + 2x"
        Case 189 : canonDescr = "Canon EF 70-200mm f/4L + 2.8x"
        Case 190 : canonDescr = "Canon EF 100mm f/2.8 Macro"
        Case 191 : canonDescr = "Canon EF 400mm f/4 DO IS"
        Case 193 : canonDescr = "Canon EF 35-80mm f/4-5.6 USM"
        Case 194 : canonDescr = "Canon EF 80-200mm f/4.5-5.6 USM"
        Case 195 : canonDescr = "Canon EF 35-105mm f/4.5-5.6 USM"
        Case 196 : canonDescr = "Canon EF 75-300mm f/4-5.6 USM"
        Case 197 : canonDescr = "Canon EF 75-300mm f/4-5.6 IS USM"
        Case 198 : canonDescr = "Canon EF 50mm f/1.4 USM"
        Case 199 : canonDescr = "Canon EF 28-80mm f/3.5-5.6 USM"
        Case 200 : canonDescr = "Canon EF 75-300mm f/4-5.6 USM"
        Case 201 : canonDescr = "Canon EF 28-80mm f/3.5-5.6 USM"
        Case 202 : canonDescr = "Canon EF 28-80mm f/3.5-5.6 USM IV"
        Case 208 : canonDescr = "Canon EF 22-55mm f/4-5.6 USM"
        Case 209 : canonDescr = "Canon EF 55-200mm f/4.5-5.6"
        Case 210 : canonDescr = "Canon EF 28-90mm f/4-5.6 USM"
        Case 211 : canonDescr = "Canon EF 28-200mm f/3.5-5.6 USM"
        Case 212 : canonDescr = "Canon EF 28-105mm f/4-5.6 USM"
        Case 213 : canonDescr = "Canon EF 90-300mm f/4.5-5.6 USM"
        Case 214 : canonDescr = "Canon EF-S 18-55mm f/3.5-4.5 USM"
        Case 215 : canonDescr = "Canon EF 55-200mm f/4.5-5.6 II USM"
        Case 224 : canonDescr = "Canon EF 70-200mm f/2.8L IS"
        Case 225 : canonDescr = "Canon EF 70-200mm f/2.8L IS + 1.4x"
        Case 226 : canonDescr = "Canon EF 70-200mm f/2.8L IS + 2x"
        Case 227 : canonDescr = "Canon EF 70-200mm f/2.8L IS + 2.8x"
        Case 228 : canonDescr = "Canon EF 28-105mm f/3.5-4.5 USM"
        Case 229 : canonDescr = "Canon EF 16-35mm f/2.8L"
        Case 230 : canonDescr = "Canon EF 24-70mm f/2.8L"
        Case 231 : canonDescr = "Canon EF 17-40mm f/4L"
        Case 232 : canonDescr = "Canon EF 70-300mm f/4.5-5.6 DO IS USM"
        Case 233 : canonDescr = "Canon EF 28-300mm f/3.5-5.6L IS"
        Case 234 : canonDescr = "Canon EF-S 17-85mm f4-5.6 IS USM"
        Case 235 : canonDescr = "Canon EF-S 10-22mm f/3.5-4.5 USM"
        Case 236 : canonDescr = "Canon EF-S 60mm f/2.8 Macro USM"
        Case 237 : canonDescr = "Canon EF 24-105mm f/4L IS"
        Case 238 : canonDescr = "Canon EF 70-300mm f/4-5.6 IS USM"
        Case 239 : canonDescr = "Canon EF 85mm f/1.2L II"
        Case 240 : canonDescr = "Canon EF-S 17-55mm f/2.8 IS USM"
        Case 241 : canonDescr = "Canon EF 50mm f/1.2L"
        Case 242 : canonDescr = "Canon EF 70-200mm f/4L IS"
        Case 243 : canonDescr = "Canon EF 70-200mm f/4L IS + 1.4x"
        Case 244 : canonDescr = "Canon EF 70-200mm f/4L IS + 2x"
        Case 245 : canonDescr = "Canon EF 70-200mm f/4L IS + 2.8x"
        Case 246 : canonDescr = "Canon EF 16-35mm f/2.8L II"
        Case 247 : canonDescr = "Canon EF 14mm f/2.8L II USM"
        Case 248 : canonDescr = "Canon EF 200mm f/2L IS"
        Case 249 : canonDescr = "Canon EF 800mm f/5.6L IS"
        Case 250 : canonDescr = "Canon EF 24 f/1.4L II"
        Case 254 : canonDescr = "Canon EF 100mm f/2.8L Macro IS USM"
        Case 488 : canonDescr = "Canon EF-S 15-85mm f/3.5-5.6 IS USM"
      End Select

    End If

  End Function


  Function formatXmp(ByVal xmp As Byte()) As String

    Dim i, i1, k As Integer
    Dim s, sx As String
    Dim c As Char

    Dim it(14, 14) As Integer
    Dim iState, newState As Integer
    Dim id1 As String = ""
    Dim id2 As String = ""
    Dim sVal As String = ""
    Dim x1, x2 As Double

    '  start, id           ><                  id2                =                 "                 value           "        
    it(0, 0) = 0 : it(0, 1) = 1 : it(0, 2) = 2 : it(0, 3) = 0 : it(0, 4) = 4 : it(0, 5) = 5 : it(0, 6) = 0 : it(0, 7) = 0 : it(0, 8) = 0 : it(0, 9) = 0 '  " "
    it(1, 0) = 0 : it(1, 1) = -1 : it(1, 2) = 2 : it(1, 3) = 0 : it(1, 4) = -1 : it(1, 5) = 5 : it(1, 6) = 0 : it(1, 7) = 0 : it(1, 8) = 0 : it(1, 9) = 0 '  alpha 
    it(2, 0) = 0 : it(2, 1) = -1 : it(2, 2) = 4 : it(2, 3) = 0 : it(2, 4) = -1 : it(2, 5) = 5 : it(2, 6) = 0 : it(2, 7) = 0 : it(2, 8) = 0 : it(2, 9) = 0 '   =
    it(3, 0) = 2 : it(3, 1) = -1 : it(3, 2) = -1 : it(3, 3) = 0 : it(3, 4) = -1 : it(3, 5) = 5 : it(3, 6) = 0 : it(3, 7) = 0 : it(3, 8) = 0 : it(3, 9) = 0 '   :
    it(4, 0) = -1 : it(4, 1) = -1 : it(4, 2) = -1 : it(4, 3) = 0 : it(4, 4) = -1 : it(4, 5) = 5 : it(4, 6) = 0 : it(4, 7) = 0 : it(4, 8) = 0 : it(4, 9) = 0 '   other
    it(5, 0) = -1 : it(5, 1) = -1 : it(5, 2) = -1 : it(5, 3) = 0 : it(5, 4) = 5 : it(5, 5) = 0 : it(5, 6) = 0 : it(5, 7) = 0 : it(5, 8) = 0 : it(5, 9) = 0 '   "
    it(6, 0) = -2 : it(6, 1) = -1 : it(6, 2) = -1 : it(6, 3) = -1 : it(6, 4) = -1 : it(6, 5) = 5 : it(6, 6) = -1 : it(6, 7) = -1 : it(6, 8) = -1 : it(6, 9) = -1 '   >
    it(7, 0) = -1 : it(7, 1) = 0 : it(7, 2) = -1 : it(7, 3) = -1 : it(7, 4) = -1 : it(7, 5) = 5 : it(7, 6) = -1 : it(7, 7) = -1 : it(7, 8) = -1 : it(7, 9) = -1 '   < not used
    it(8, 0) = 0 : it(8, 1) = 0 : it(8, 2) = 0 : it(8, 3) = 0 : it(8, 4) = 0 : it(8, 5) = 0 : it(8, 6) = 0 : it(8, 7) = 0 : it(8, 8) = 0 : it(8, 9) = 0 '   

    sx = UTF8bare.GetString(xmp)
    k = sx.IndexOf("<rdf:Description", StringComparison.OrdinalIgnoreCase)
    If k > 0 Then sx = sx.Substring(k + 17)
    k = sx.IndexOf("<?xpacket end", StringComparison.OrdinalIgnoreCase)
    If k > 0 Then sx = vb.Left(sx, k - 2)
    sx = sx.Trim(whiteSpace)
    sx = sx.Replace(ChrW(0), crlf)

    formatXmp = ""
    iState = 0
    s = ""
    For i = 0 To Len(sx) - 1
      c = sx.Chars(i)
      If Char.IsLetterOrDigit(c) Then
        k = 1
      Else
        Select Case c
          Case " ", ChrW(9), ChrW(0) : k = 0
          Case "=" : k = 2
          Case ":" : k = 3
          Case """" : k = 5
          Case ">" : k = 6
          Case "<" : k = 7
          Case Else : k = 4
        End Select
      End If

      newState = it(k, iState)
      If iState <> newState Then
        Select Case newState
          Case 1 ' >
            If id1 <> "" And id2 <> "" Then formatXmp = formatXmp & crlf & id1 & " " & id2 & ":" & ChrW(9) & sVal
            sVal = ""
            id1 = ""
            id2 = ""
          Case 2 ' id1
            id1 = s.Trim(whiteSpace)
          Case 4 ' id2
            id2 = s.Trim(whiteSpace)
          Case 0, -2 ' value
            sVal = s.Trim(whiteSpace)
            If Len(sVal) > 200 Then sVal = vb.Left(sVal, 200) & "... (" & Len(sVal) & " total bytes)"
            i1 = InStr(sVal, "/")
            If i1 > 1 AndAlso i1 < Len(sVal) _
              AndAlso IsNumeric(vb.Left(sVal, i1 - 1)) AndAlso IsNumeric(Mid(sVal, i1 + 1)) AndAlso Val(Mid(sVal, i1 + 1)) <> 0 Then
              x1 = vb.Left(sVal, i1 - 1)
              x2 = Mid(sVal, i1 + 1)
              ' divide the rational
              formatXmp = formatXmp & crlf & id1 & " " & id2 & ":" & ChrW(9) & Format(x1 / x2, "#,0.0##")
            Else
              If sVal <> "" Then formatXmp = formatXmp & crlf & id1 & " " & id2 & ":" & ChrW(9) & sVal
            End If

            sVal = ""
            id1 = ""
            id2 = ""
            If newState = -2 Then Exit For ' done with description
          Case -1
            newState = 0
        End Select
        s = ""
        iState = newState
      Else
        s = s & c
      End If
    Next i

  End Function

  Function pCommentsToMagick(pComments As List(Of PropertyItem)) As ExifProfile
    ' copy exif data from uExif to ExifProfile
    Dim exif As New ExifProfile
    Dim p As PropertyItem
    Dim exTags() As Integer
    Dim v As Object
    Dim tagName As String = ""
    Dim Count, dataType As Integer

    exTags = System.Enum.GetValues(GetType(ImageMagick.ExifTag)) ' magick misses some tags, errors with setvalue

    For Each p In pComments
      v = getBmpComment(p.Id, pComments)
      If v IsNot Nothing AndAlso exTags.Contains(p.Id) Then
        TagInfo(p.Id, tagName, dataType, Count)
        Try
          If dataType = 2 OrElse (Count = 0 Or Count > 1) Then
            exif.SetValue(p.Id, v)
          Else
            exif.SetValue(p.Id, v(0))
          End If
        Catch ex As Exception
        End Try
      End If
    Next p

    Return exif

  End Function

  Function uExifToMagick(ux As uExif) As ExifProfile
    ' copy exif data from uExif to ExifProfile
    Dim ex As New ExifProfile
    Dim tg As uTag
    Dim exTags() As Integer

    exTags = System.Enum.GetValues(GetType(ImageMagick.ExifTag)) ' magick misses some tags, errors with setvalue

    For Each tg In ux.Tags
      If exTags.Contains(tg.tag) Then ex.SetValue(tg.tag, tg.Value)
    Next tg

    If ux.tagExists(uExif.TagID.exifpointer) Then
      tg = ux.Tags.Item(sTag(uExif.TagID.exifpointer))
      If tg.IFD IsNot Nothing Then
        For Each tgg As uTag In tg.IFD.Tags
          If exTags.Contains(tgg.tag) Then ex.SetValue(tgg.tag, tgg.Value)
        Next tgg
      End If
    End If

    If ux.tagExists(uExif.TagID.GPSpointer) Then
      tg = ux.Tags.Item(sTag(uExif.TagID.GPSpointer))
      If tg.IFD IsNot Nothing Then
        For Each tgg As uTag In tg.IFD.Tags
          If exTags.Contains(tgg.tag) Then ex.SetValue(tgg.tag, tgg.Value)
        Next tgg
      End If
    End If

    Return ex

  End Function

  Public Enum propID
    ' IDs for GDI .propertyitems
    GpsVersion = &H0
    GpsLatitudeRef = &H1
    GpsLatitude = &H2
    GpsLongitudeRef = &H3
    GpsLongitude = &H4
    GpsAltitudeRef = &H5
    GpsAltitude = &H6
    GpsTimeStamp = &H7
    GpsSatellites = &H8
    GpsStatus = &H9
    GpsMeasureMode = &HA
    GpsDop = &HB
    GpsSpeedRef = &HC
    GpsSpeed = &HD
    GpsTrackRef = &HE
    GpsTrack = &HF
    GpsImgDirectionRef = &H10
    GpsImgDirection = &H11
    GpsMapDatum = &H12
    GpsDestinationLatitudeRef = &H13
    GpsDestinationLatitude = &H14
    GpsDestinationLongitudeRef = &H15
    GpsDestinationLongitude = &H16
    GpsDestBearingRef = &H17
    GpsDestBearing = &H18
    GpsDestinationDistanceRef = &H19
    GpsDestinationDistance = &H1A
    GPSProcessingMethod = &H1B
    GPSAreaInformation = &H1C
    GpsDateStamp = &H1D
    GPSDifferential = &H1E
    GPSHPositioningError = &H1F
    NewSubfileType = &HFE
    SubfileType = &HFF
    ImageWidth = &H100
    ImageHeight = &H101
    BitsPerSample = &H102
    Compression = &H103
    PhotometricInterpretation = &H106
    ThreshHolding = &H107
    CellWidth = &H108
    CellHeight = &H109
    FillOrder = &H10A
    DocumentName = &H10D
    ImageDescription = &H10E
    EquipmentMake = &H10F
    EquipmentModel = &H110
    StripOffsets = &H111
    Orientation = &H112
    SamplesPerPixel = &H115
    RowsPerStrip = &H116
    StripBytesCount = &H117
    MinSampleValue = &H118
    MaxSampleValue = &H119
    XResolution = &H11A
    YResolution = &H11B
    PlanarConfiguration = &H11C
    PageName = &H11D
    XPosition = &H11E
    YPosition = &H11F
    FreeOffset = &H120
    FreeByteCounts = &H121
    GrayResponseUnit = &H122
    GrayResponseCurve = &H123
    T4Option = &H124
    T6Option = &H125
    ResolutionUnit = &H128
    PageNumber = &H129
    TransferFunction = &H12D
    SoftwareUsed = &H131
    DateTime = &H132
    Artist = &H13B
    HostComputer = &H13C
    Predictor = &H13D
    WhitePoint = &H13E
    PrimaryChromaticities = &H13F
    ColorMap = &H140
    HalftoneHints = &H141
    TileWidth = &H142
    TileLength = &H143
    TileOffset = &H144
    TileByteCounts = &H145
    InkSet = &H14C
    InkNames = &H14D
    NumberOfInks = &H14E
    DotRange = &H150
    TargetPrinter = &H151
    ExtraSamples = &H152
    SampleFormat = &H153
    SMinSampleValue = &H154
    SMaxSampleValue = &H155
    TransferRange = &H156
    JPEGProc = &H200
    JPEGInterchangeFormat = &H201
    JPEGInterchangeLength = &H202
    JPEGRestartInterval = &H203
    JPEGLosslessPredictors = &H205
    JPEGPointTransforms = &H206
    JPEGQTables = &H207
    JPEGDCTables = &H208
    JPEGACTables = &H209
    YCbCrCoefficients = &H211
    YCbCrSubsampling = &H212
    YCbCrPositioning = &H213
    REFBlackWhite = &H214
    Gamma = &H301
    ICCProfileDescriptor = &H302
    SRGBRenderingIntent = &H303
    ImageTitle = &H320
    ResolutionXUnit = &H5001
    ResolutionYUnit = &H5002
    ResolutionXLengthUnit = &H5003
    ResolutionYLengthUnit = &H5004
    PrintFlags = &H5005
    PrintFlagsVersion = &H5006
    PrintFlagsCrop = &H5007
    PrintFlagsBleedWidth = &H5008
    PrintFlagsBleedWidthScale = &H5009
    HalftoneLPI = &H500A
    HalftoneLPIUnit = &H500B
    HalftoneDegree = &H500C
    HalftoneShape = &H500D
    HalftoneMisc = &H500E
    HalftoneScreen = &H500F
    JPEGQuality = &H5010
    GridSize = &H5011
    ThumbnailFormat = &H5012
    ThumbnailWidth = &H5013
    ThumbnailHeight = &H5014
    ThumbnailColorDepth = &H5015
    ThumbnailPlanes = &H5016
    ThumbnailRawBytes = &H5017
    ThumbnailSize = &H5018
    ThumbnailCompressedSize = &H5019
    ColorTransferFunction = &H501A
    ThumbnailData = &H501B
    ThumbnailImageWidth = &H5020
    ThumbnailImageHeight = &H5021
    ThumbnailBitsPerSample = &H5022
    ThumbnailCompression = &H5023
    ThumbnailPhotometricInterp = &H5024
    ThumbnailImageDescription = &H5025
    ThumbnailEquipMake = &H5026
    ThumbnailEquipModel = &H5027
    ThumbnailStripOffsets = &H5028
    ThumbnailOrientation = &H5029
    ThumbnailSamplesPerPixel = &H502A
    ThumbnailRowsPerStrip = &H502B
    ThumbnailStripBytesCount = &H502C
    ThumbnailResolutionX = &H502D
    ThumbnailResolutionY = &H502E
    ThumbnailPlanarConfig = &H502F
    ThumbnailResolutionUnit = &H5030
    ThumbnailTransferFunction = &H5031
    ThumbnailSoftwareUsed = &H5032
    ThumbnailDateTime = &H5033
    ThumbnailArtist = &H5034
    ThumbnailWhitePoint = &H5035
    ThumbnailPrimaryChromaticities = &H5036
    ThumbnailYCbCrCoefficients = &H5037
    ThumbnailYCbCrSubsampling = &H5038
    ThumbnailYCbCrPositioning = &H5039
    ThumbnailRefBlackWhite = &H503A
    ThumbnailCopyRight = &H503B
    LuminanceTable = &H5090
    ChrominanceTable = &H5091
    FrameDelay = &H5100
    LoopCount = &H5101
    GlobalPalette = &H5102
    IndexBackground = &H5103
    IndexTransparent = &H5104
    PixelUnit = &H5110
    PixelPerUnitX = &H5111
    PixelPerUnitY = &H5112
    PaletteHistogram = &H5113
    Copyright = &H8298
    ExposureTime = &H829A
    FNumber = &H829D
    ExifPointer = &H8769
    ICCProfile = &H8773
    ExposureProgram = &H8822
    SpectralSensitivity = &H8824
    GpsIFDPointer = &H8825
    PhotographicSensitivity = &H8827
    OECF = &H8828
    SensitivityType = &H8830
    StandardOutputSensitivity = &H8831
    RecommendedExposureIndex = &H8832
    ISOSpeed = &H8833
    ISOSpeedLatitudeyyy = &H8834
    ISOSpeedLatitudezzz = &H8835
    ExifVersion = &H9000
    DateTimeOriginal = &H9003
    DateTimeDigitized = &H9004
    ComponentConfiguration = &H9101
    CompressedBPP = &H9102
    ShutterSpeed = &H9201
    Aperture = &H9202
    Brightness = &H9203
    ExposureBias = &H9204
    MaxAperture = &H9205
    SubjectDistanceRange = &H9206
    MeteringMode = &H9207
    LightSource = &H9208
    Flash = &H9209
    FocalLength = &H920A
    SubjectArea = &H9214
    MakerNote = &H927C
    UserComment = &H9286
    DateTimeSubseconds = &H9290
    DateTimeOriginalSubseconds = &H9291
    DateTimeDigitizedSubSeconds = &H9292
    FPXVersion = &HA000
    ColorSpace = &HA001
    PixelXDimension = &HA002
    PixelYDimension = &HA003
    RelatedWavFile = &HA004
    InteropPointer = &HA005
    FlashEnergy = &HA20B
    SpatialFrequencyResponse = &HA20C
    FocalXResolution = &HA20E
    FocalYResolution = &HA20F
    FocalResolutionUnit = &HA210
    SubjectLocation = &HA214
    ExposureIndex = &HA215
    SensingMethod = &HA217
    FileSource = &HA300
    SceneType = &HA301
    CFAPattern = &HA302
    CustomRendered = &HA401
    ExposureMode = &HA402
    WhiteBalance = &HA403
    DigitalZoomRatio = &HA404
    FocalLengthIn35mmFilm = &HA405
    SceneCaptureType = &HA406
    GainControl = &HA407
    Contrast = &HA408
    Saturation = &HA409
    Sharpness = &HA40A
    DeviceSettingDescription = &HA40B
  End Enum


  Sub exifInit()

    olympusLens.Add("000000", "(none)")
    olympusLens.Add("000100", "Olympus Zuiko Digital ED 50mm F2.0 Macro")
    olympusLens.Add("000101", "Olympus Zuiko Digital 40-150mm F3.5-4.5")
    olympusLens.Add("000110", "Olympus M.Zuiko Digital ED 14-42mm F3.5-5.6")
    olympusLens.Add("000200", "Olympus Zuiko Digital ED 150mm F2.0")
    olympusLens.Add("000210", "Olympus M.Zuiko Digital 17mm F2.8 Pancake")
    olympusLens.Add("000300", "Olympus Zuiko Digital ED 300mm F2.8")
    olympusLens.Add("000310", "Olympus M.Zuiko Digital ED 14-150mm F4.0-5.6 [II]")
    olympusLens.Add("000410", "Olympus M.Zuiko Digital ED 9-18mm F4.0-5.6")
    olympusLens.Add("000500", "Olympus Zuiko Digital 14-54mm F2.8-3.5")
    olympusLens.Add("000501", "Olympus Zuiko Digital Pro ED 90-250mm F2.8")
    olympusLens.Add("000510", "Olympus M.Zuiko Digital ED 14-42mm F3.5-5.6 L")
    olympusLens.Add("000600", "Olympus Zuiko Digital ED 50-200mm F2.8-3.5")
    olympusLens.Add("000601", "Olympus Zuiko Digital ED 8mm F3.5 Fisheye")
    olympusLens.Add("000610", "Olympus M.Zuiko Digital ED 40-150mm F4.0-5.6")
    olympusLens.Add("000700", "Olympus Zuiko Digital 11-22mm F2.8-3.5")
    olympusLens.Add("000701", "Olympus Zuiko Digital 18-180mm F3.5-6.3")
    olympusLens.Add("000710", "Olympus M.Zuiko Digital ED 12mm F2.0")
    olympusLens.Add("000801", "Olympus Zuiko Digital 70-300mm F4.0-5.6")
    olympusLens.Add("000810", "Olympus M.Zuiko Digital ED 75-300mm F4.8-6.7")
    olympusLens.Add("000910", "Olympus M.Zuiko Digital 14-42mm F3.5-5.6 II")
    olympusLens.Add("001001", "Kenko Tokina Reflex 300mm F6.3 MF Macro")
    olympusLens.Add("001010", "Olympus M.Zuiko Digital ED 12-50mm F3.5-6.3 EZ")
    olympusLens.Add("001110", "Olympus M.Zuiko Digital 45mm F1.8")
    olympusLens.Add("001210", "Olympus M.Zuiko Digital ED 60mm F2.8 Macro")
    olympusLens.Add("001310", "Olympus M.Zuiko Digital 14-42mm F3.5-5.6 II R")
    olympusLens.Add("001410", "Olympus M.Zuiko Digital ED 40-150mm F4.0-5.6 R")
    olympusLens.Add("001500", "Olympus Zuiko Digital ED 7-14mm F4.0")
    olympusLens.Add("001510", "Olympus M.Zuiko Digital ED 75mm F1.8")
    olympusLens.Add("001610", "Olympus M.Zuiko Digital 17mm F1.8")
    olympusLens.Add("001700", "Olympus Zuiko Digital Pro ED 35-100mm F2.0")
    olympusLens.Add("001800", "Olympus Zuiko Digital 14-45mm F3.5-5.6")
    olympusLens.Add("001810", "Olympus M.Zuiko Digital ED 75-300mm F4.8-6.7 II")
    olympusLens.Add("001910", "Olympus M.Zuiko Digital ED 12-40mm F2.8 Pro")
    olympusLens.Add("002000", "Olympus Zuiko Digital 35mm F3.5 Macro")
    olympusLens.Add("002010", "Olympus M.Zuiko Digital ED 40-150mm F2.8 Pro")
    olympusLens.Add("002110", "Olympus M.Zuiko Digital ED 14-42mm F3.5-5.6 EZ")
    olympusLens.Add("002200", "Olympus Zuiko Digital 17.5-45mm F3.5-5.6")
    olympusLens.Add("002210", "Olympus M.Zuiko Digital 25mm F1.8")
    olympusLens.Add("002300", "Olympus Zuiko Digital ED 14-42mm F3.5-5.6")
    olympusLens.Add("002310", "Olympus M.Zuiko Digital ED 7-14mm F2.8 Pro")
    olympusLens.Add("002400", "Olympus Zuiko Digital ED 40-150mm F4.0-5.6")
    olympusLens.Add("002410", "Olympus M.Zuiko Digital ED 300mm F4.0 IS Pro")
    olympusLens.Add("002510", "Olympus M.Zuiko Digital ED 8mm F1.8 Fisheye Pro")
    olympusLens.Add("002610", "Olympus M.Zuiko Digital ED 12-100mm F4.0 IS Pro")
    olympusLens.Add("002710", "Olympus M.Zuiko Digital ED 30mm F3.5 Macro")
    olympusLens.Add("002810", "Olympus M.Zuiko Digital ED 25mm F1.2 Pro")
    olympusLens.Add("002910", "Olympus M.Zuiko Digital ED 17mm F1.2 Pro")
    olympusLens.Add("003000", "Olympus Zuiko Digital ED 50-200mm F2.8-3.5 SWD")
    olympusLens.Add("003010", "Olympus M.Zuiko Digital ED 45mm F1.2 Pro")
    olympusLens.Add("003100", "Olympus Zuiko Digital ED 12-60mm F2.8-4.0 SWD")
    olympusLens.Add("003200", "Olympus Zuiko Digital ED 14-35mm F2.0 SWD")
    olympusLens.Add("003300", "Olympus Zuiko Digital 25mm F2.8")
    olympusLens.Add("003400", "Olympus Zuiko Digital ED 9-18mm F4.0-5.6")
    olympusLens.Add("003500", "Olympus Zuiko Digital 14-54mm F2.8-3.5 II")
    olympusLens.Add("010100", "Sigma 18-50mm F3.5-5.6 DC")
    olympusLens.Add("010110", "Sigma 30mm F2.8 EX DN")
    olympusLens.Add("010200", "Sigma 55-200mm F4.0-5.6 DC")
    olympusLens.Add("010210", "Sigma 19mm F2.8 EX DN")
    olympusLens.Add("010300", "Sigma 18-125mm F3.5-5.6 DC")
    olympusLens.Add("010310", "Sigma 30mm F2.8 DN | A")
    olympusLens.Add("010400", "Sigma 18-125mm F3.5-5.6 DC")
    olympusLens.Add("010410", "Sigma 19mm F2.8 DN | A")
    olympusLens.Add("010500", "Sigma 30mm F1.4 EX DC HSM")
    olympusLens.Add("010510", "Sigma 60mm F2.8 DN | A")
    olympusLens.Add("010600", "Sigma APO 50-500mm F4.0-6.3 EX DG HSM")
    olympusLens.Add("010610", "Sigma 30mm F1.4 DC DN | C")
    olympusLens.Add("010700", "Sigma Macro 105mm F2.8 EX DG")
    olympusLens.Add("010710", "Sigma 16mm F1.4 DC DN | C (017)")
    olympusLens.Add("010800", "Sigma APO Macro 150mm F2.8 EX DG HSM")
    olympusLens.Add("010900", "Sigma 18-50mm F2.8 EX DC Macro")
    olympusLens.Add("011000", "Sigma 24mm F1.8 EX DG Aspherical Macro")
    olympusLens.Add("011100", "Sigma APO 135-400mm F4.5-5.6 DG")
    olympusLens.Add("011200", "Sigma APO 300-800mm F5.6 EX DG HSM")
    olympusLens.Add("011300", "Sigma 30mm F1.4 EX DC HSM")
    olympusLens.Add("011400", "Sigma APO 50-500mm F4.0-6.3 EX DG HSM")
    olympusLens.Add("011500", "Sigma 10-20mm F4.0-5.6 EX DC HSM")
    olympusLens.Add("011600", "Sigma APO 70-200mm F2.8 II EX DG Macro HSM")
    olympusLens.Add("011700", "Sigma 50mm F1.4 EX DG HSM")
    olympusLens.Add("020100", "Leica D Vario Elmarit 14-50mm F2.8-3.5 Asph.")
    olympusLens.Add("020110", "Lumix G Vario 14-45mm F3.5-5.6 Asph. Mega OIS")
    olympusLens.Add("020200", "Leica D Summilux 25mm F1.4 Asph.")
    olympusLens.Add("020210", "Lumix G Vario 45-200mm F4.0-5.6 Mega OIS")
    olympusLens.Add("020300", "Leica D Vario Elmar 14-50mm F3.8-5.6 Asph. Mega OIS")
    olympusLens.Add("020301", "Leica D Vario Elmar 14-50mm F3.8-5.6 Asph.")
    olympusLens.Add("020310", "Lumix G Vario HD 14-140mm F4.0-5.8 Asph. Mega OIS")
    olympusLens.Add("020400", "Leica D Vario Elmar 14-150mm F3.5-5.6")
    olympusLens.Add("020410", "Lumix G Vario 7-14mm F4.0 Asph.")
    olympusLens.Add("020510", "Lumix G 20mm F1.7 Asph.")
    olympusLens.Add("020610", "Leica DG Macro-Elmarit 45mm F2.8 Asph. Mega OIS")
    olympusLens.Add("020710", "Lumix G Vario 14-42mm F3.5-5.6 Asph. Mega OIS")
    olympusLens.Add("020810", "Lumix G Fisheye 8mm F3.5")
    olympusLens.Add("020910", "Lumix G Vario 100-300mm F4.0-5.6 Mega OIS")
    olympusLens.Add("021010", "Lumix G 14mm F2.5 Asph.")
    olympusLens.Add("021110", "Lumix G 12.5mm F12 3D")
    olympusLens.Add("021210", "Leica DG Summilux 25mm F1.4 Asph.")
    olympusLens.Add("021310", "Lumix G X Vario PZ 45-175mm F4.0-5.6 Asph. Power OIS")
    olympusLens.Add("021410", "Lumix G X Vario PZ 14-42mm F3.5-5.6 Asph. Power OIS")
    olympusLens.Add("021510", "Lumix G X Vario 12-35mm F2.8 Asph. Power OIS")
    olympusLens.Add("021610", "Lumix G Vario 45-150mm F4.0-5.6 Asph. Mega OIS")
    olympusLens.Add("021710", "Lumix G X Vario 35-100mm F2.8 Power OIS")
    olympusLens.Add("021810", "Lumix G Vario 14-42mm F3.5-5.6 II Asph. Mega OIS")
    olympusLens.Add("021910", "Lumix G Vario 14-140mm F3.5-5.6 Asph. Power OIS")
    olympusLens.Add("022010", "Lumix G Vario 12-32mm F3.5-5.6 Asph. Mega OIS")
    olympusLens.Add("022110", "Leica DG Nocticron 42.5mm F1.2 Asph. Power OIS")
    olympusLens.Add("022210", "Leica DG Summilux 15mm F1.7 Asph.")
    olympusLens.Add("022310", "Lumix G Vario 35-100mm F4.0-5.6 Asph. Mega OIS")
    olympusLens.Add("022410", "Lumix G Macro 30mm F2.8 Asph. Mega OIS")
    olympusLens.Add("022510", "Lumix G 42.5mm F1.7 Asph. Power OIS")
    olympusLens.Add("022610", "Lumix G 25mm F1.7 Asph.")
    olympusLens.Add("022710", "Leica DG Vario-Elmar 100-400mm F4.0-6.3 Asph. Power OIS")
    olympusLens.Add("022810", "Lumix G Vario 12-60mm F3.5-5.6 Asph. Power OIS")
    olympusLens.Add("030100", "Leica D Vario Elmarit 14-50mm F2.8-3.5 Asph.")
    olympusLens.Add("030200", "Leica D Summilux 25mm F1.4 Asph.")
    olympusLens.Add("050110", "Tamron 14-150mm F3.5-5.8 Di III")

    olympusCamera.Add("D4028", "X-2,C-50Z")
    olympusCamera.Add("D4029", "E-20,E-20N,E-20P")
    olympusCamera.Add("D4034", "C720UZ")
    olympusCamera.Add("D4040", "E-1")
    olympusCamera.Add("D4041", "E-300")
    olympusCamera.Add("D4083", "C2Z,D520Z,C220Z")
    olympusCamera.Add("D4106", "u20D,S400D,u400D")
    olympusCamera.Add("D4120", "X-1")
    olympusCamera.Add("D4122", "u10D,S300D,u300D")
    olympusCamera.Add("D4125", "AZ-1")
    olympusCamera.Add("D4141", "C150,D390")
    olympusCamera.Add("D4193", "C-5000Z")
    olympusCamera.Add("D4194", "X-3,C-60Z")
    olympusCamera.Add("D4199", "u30D,S410D,u410D")
    olympusCamera.Add("D4205", "X450,D535Z,C370Z")
    olympusCamera.Add("D4210", "C160,D395")
    olympusCamera.Add("D4211", "C725UZ")
    olympusCamera.Add("D4213", "FerrariMODEL2003")
    olympusCamera.Add("D4216", "u15D")
    olympusCamera.Add("D4217", "u25D")
    olympusCamera.Add("D4220", "u-miniD,Stylus V")
    olympusCamera.Add("D4221", "u40D,S500,uD500")
    olympusCamera.Add("D4231", "FerrariMODEL2004")
    olympusCamera.Add("D4240", "X500,D590Z,C470Z")
    olympusCamera.Add("D4244", "uD800,S800")
    olympusCamera.Add("D4256", "u720SW,S720SW")
    olympusCamera.Add("D4261", "X600,D630,FE5500")
    olympusCamera.Add("D4262", "uD600,S600")
    olympusCamera.Add("D4301", "u810/S810")
    olympusCamera.Add("D4302", "u710,S710")
    olympusCamera.Add("D4303", "u700,S700")
    olympusCamera.Add("D4304", "FE100,X710")
    olympusCamera.Add("D4305", "FE110,X705")
    olympusCamera.Add("D4310", "FE-130,X-720")
    olympusCamera.Add("D4311", "FE-140,X-725")
    olympusCamera.Add("D4312", "FE150,X730")
    olympusCamera.Add("D4313", "FE160,X735")
    olympusCamera.Add("D4314", "u740,S740")
    olympusCamera.Add("D4315", "u750,S750")
    olympusCamera.Add("D4316", "u730/S730")
    olympusCamera.Add("D4317", "FE115,X715")
    olympusCamera.Add("D4321", "SP550UZ")
    olympusCamera.Add("D4322", "SP510UZ")
    olympusCamera.Add("D4324", "FE170,X760")
    olympusCamera.Add("D4326", "FE200")
    olympusCamera.Add("D4327", "FE190/X750")
    olympusCamera.Add("D4328", "u760,S760")
    olympusCamera.Add("D4330", "FE180/X745")
    olympusCamera.Add("D4331", "u1000/S1000")
    olympusCamera.Add("D4332", "u770SW,S770SW")
    olympusCamera.Add("D4333", "FE240/X795")
    olympusCamera.Add("D4334", "FE210,X775")
    olympusCamera.Add("D4336", "FE230/X790")
    olympusCamera.Add("D4337", "FE220,X785")
    olympusCamera.Add("D4338", "u725SW,S725SW")
    olympusCamera.Add("D4339", "FE250/X800")
    olympusCamera.Add("D4341", "u780,S780")
    olympusCamera.Add("D4343", "u790SW,S790SW")
    olympusCamera.Add("D4344", "u1020,S1020")
    olympusCamera.Add("D4346", "FE15,X10")
    olympusCamera.Add("D4348", "FE280,X820,C520")
    olympusCamera.Add("D4349", "FE300,X830")
    olympusCamera.Add("D4350", "u820,S820")
    olympusCamera.Add("D4351", "u1200,S1200")
    olympusCamera.Add("D4352", "FE270,X815,C510")
    olympusCamera.Add("D4353", "u795SW,S795SW")
    olympusCamera.Add("D4354", "u1030SW,S1030SW")
    olympusCamera.Add("D4355", "SP560UZ")
    olympusCamera.Add("D4356", "u1010,S1010")
    olympusCamera.Add("D4357", "u830,S830")
    olympusCamera.Add("D4359", "u840,S840")
    olympusCamera.Add("D4360", "FE350WIDE,X865")
    olympusCamera.Add("D4361", "u850SW,S850SW")
    olympusCamera.Add("D4362", "FE340,X855,C560")
    olympusCamera.Add("D4363", "FE320,X835,C540")
    olympusCamera.Add("D4364", "SP570UZ")
    olympusCamera.Add("D4366", "FE330,X845,C550")
    olympusCamera.Add("D4368", "FE310,X840,C530")
    olympusCamera.Add("D4370", "u1050SW,S1050SW")
    olympusCamera.Add("D4371", "u1060,S1060")
    olympusCamera.Add("D4372", "FE370,X880,C575")
    olympusCamera.Add("D4374", "SP565UZ")
    olympusCamera.Add("D4377", "u1040,S1040")
    olympusCamera.Add("D4378", "FE360,X875,C570")
    olympusCamera.Add("D4379", "FE20,X15,C25")
    olympusCamera.Add("D4380", "uT6000,ST6000")
    olympusCamera.Add("D4381", "uT8000,ST8000")
    olympusCamera.Add("D4382", "u9000,S9000")
    olympusCamera.Add("D4384", "SP590UZ")
    olympusCamera.Add("D4385", "FE3010,X895")
    olympusCamera.Add("D4386", "FE3000,X890")
    olympusCamera.Add("D4387", "FE35,X30")
    olympusCamera.Add("D4388", "u550WP,S550WP")
    olympusCamera.Add("D4390", "FE5000,X905")
    olympusCamera.Add("D4391", "u5000")
    olympusCamera.Add("D4392", "u7000,S7000")
    olympusCamera.Add("D4396", "FE5010,X915")
    olympusCamera.Add("D4397", "FE25,X20")
    olympusCamera.Add("D4398", "FE45,X40")
    olympusCamera.Add("D4401", "XZ-1")
    olympusCamera.Add("D4402", "uT6010,ST6010")
    olympusCamera.Add("D4406", "u7010,S7010 / u7020,S7020")
    olympusCamera.Add("D4407", "FE4010,X930")
    olympusCamera.Add("D4408", "X560WP")
    olympusCamera.Add("D4409", "FE26,X21")
    olympusCamera.Add("D4410", "FE4000,X920,X925")
    olympusCamera.Add("D4411", "FE46,X41,X42")
    olympusCamera.Add("D4412", "FE5020,X935")
    olympusCamera.Add("D4413", "uTough-3000")
    olympusCamera.Add("D4414", "StylusTough-6020")
    olympusCamera.Add("D4415", "StylusTough-8010")
    olympusCamera.Add("D4417", "u5010,S5010")
    olympusCamera.Add("D4418", "u7040,S7040")
    olympusCamera.Add("D4419", "u9010,S9010")
    olympusCamera.Add("D4423", "FE4040")
    olympusCamera.Add("D4424", "FE47,X43")
    olympusCamera.Add("D4426", "FE4030,X950")
    olympusCamera.Add("D4428", "FE5030,X965,X960")
    olympusCamera.Add("D4430", "u7030,S7030")
    olympusCamera.Add("D4432", "SP600UZ")
    olympusCamera.Add("D4434", "SP800UZ")
    olympusCamera.Add("D4439", "FE4020,X940")
    olympusCamera.Add("D4442", "FE5035")
    olympusCamera.Add("D4448", "FE4050,X970")
    olympusCamera.Add("D4450", "FE5050,X985")
    olympusCamera.Add("D4454", "u-7050")
    olympusCamera.Add("D4464", "T10,X27")
    olympusCamera.Add("D4470", "FE5040,X980")
    olympusCamera.Add("D4472", "TG-310")
    olympusCamera.Add("D4474", "TG-610")
    olympusCamera.Add("D4476", "TG-810")
    olympusCamera.Add("D4478", "VG145,VG140,D715")
    olympusCamera.Add("D4479", "VG130,D710")
    olympusCamera.Add("D4480", "VG120,D705")
    olympusCamera.Add("D4482", "VR310,D720")
    olympusCamera.Add("D4484", "VR320,D725")
    olympusCamera.Add("D4486", "VR330,D730")
    olympusCamera.Add("D4488", "VG110,D700")
    olympusCamera.Add("D4490", "SP-610UZ")
    olympusCamera.Add("D4492", "SZ-10")
    olympusCamera.Add("D4494", "SZ-20")
    olympusCamera.Add("D4496", "SZ-30MR")
    olympusCamera.Add("D4498", "SP-810UZ")
    olympusCamera.Add("D4500", "SZ-11")
    olympusCamera.Add("D4504", "TG-615")
    olympusCamera.Add("D4508", "TG-620")
    olympusCamera.Add("D4510", "TG-820")
    olympusCamera.Add("D4512", "TG-1")
    olympusCamera.Add("D4516", "SH-21")
    olympusCamera.Add("D4519", "SZ-14")
    olympusCamera.Add("D4520", "SZ-31 MR")
    olympusCamera.Add("D4521", "SH-25 MR")
    olympusCamera.Add("D4523", "SP-720 UZ")
    olympusCamera.Add("D4529", "VG170")
    olympusCamera.Add("D4531", "XZ-2")
    olympusCamera.Add("D4535", "SP-620 UZ")
    olympusCamera.Add("D4536", "TG-320")
    olympusCamera.Add("D4537", "VR340,D750")
    olympusCamera.Add("D4538", "VG160,X990,D745")
    olympusCamera.Add("D4541", "SZ-12")
    olympusCamera.Add("D4545", "VH410")
    olympusCamera.Add("D4546", "XZ-10")
    olympusCamera.Add("D4547", "TG-2")
    olympusCamera.Add("D4548", "TG-830")
    olympusCamera.Add("D4549", "TG-630")
    olympusCamera.Add("D4550", "SH-50")
    olympusCamera.Add("D4553", "SZ-16,DZ-105")
    olympusCamera.Add("D4562", "SP-820 UZ")
    olympusCamera.Add("D4566", "SZ-15")
    olympusCamera.Add("D4572", "STYLUS1")
    olympusCamera.Add("D4574", "TG-3")
    olympusCamera.Add("D4575", "TG-850")
    olympusCamera.Add("D4579", "SP-100EE")
    olympusCamera.Add("D4580", "SH-60")
    olympusCamera.Add("D4581", "SH-1")
    olympusCamera.Add("D4582", "TG-835")
    olympusCamera.Add("D4585", "SH-2 / SH-3")
    olympusCamera.Add("D4586", "TG-4")
    olympusCamera.Add("D4587", "TG-860")
    olympusCamera.Add("D4591", "TG-870")
    olympusCamera.Add("D4593", "TG-5")
    olympusCamera.Add("D4809", "C2500L")
    olympusCamera.Add("D4842", "E-10")
    olympusCamera.Add("D4856", "C-1")
    olympusCamera.Add("D4857", "C-1Z,D-150Z")
    olympusCamera.Add("DCHC", "D500L")
    olympusCamera.Add("DCHT", "D600L / D620L")
    olympusCamera.Add("K0055", "AIR-A01")
    olympusCamera.Add("S0003", "E-330")
    olympusCamera.Add("S0004", "E-500")
    olympusCamera.Add("S0009", "E-400")
    olympusCamera.Add("S0010", "E-510  ")
    olympusCamera.Add("S0011", "E-3")
    olympusCamera.Add("S0013", "E-410")
    olympusCamera.Add("S0016", "E-420")
    olympusCamera.Add("S0017", "E-30")
    olympusCamera.Add("S0018", "E-520")
    olympusCamera.Add("S0019", "E-P1")
    olympusCamera.Add("S0023", "E-620")
    olympusCamera.Add("S0026", "E-P2")
    olympusCamera.Add("S0027", "E-PL1")
    olympusCamera.Add("S0029", "E-450")
    olympusCamera.Add("S0030", "E-600")
    olympusCamera.Add("S0032", "E-P3")
    olympusCamera.Add("S0033", "E-5")
    olympusCamera.Add("S0034", "E-PL2")
    olympusCamera.Add("S0036", "E-M5")
    olympusCamera.Add("S0038", "E-PL3")
    olympusCamera.Add("S0039", "E-PM1")
    olympusCamera.Add("S0040", "E-PL1s")
    olympusCamera.Add("S0042", "E-PL5")
    olympusCamera.Add("S0043", "E-PM2")
    olympusCamera.Add("S0044", "E-P5")
    olympusCamera.Add("S0045", "E-PL6")
    olympusCamera.Add("S0046", "E-PL7")
    olympusCamera.Add("S0047", "E-M1")
    olympusCamera.Add("S0051", "E-M10")
    olympusCamera.Add("S0052", "E-M5 Mark II")
    olympusCamera.Add("S0059", "E-M10 Mark II")
    olympusCamera.Add("S0061", "PEN-F")
    olympusCamera.Add("S0065", "E-PL8")
    olympusCamera.Add("S0067", "E-M1 Mark II")
    olympusCamera.Add("S0068", "E-M10 Mark III")
    olympusCamera.Add("S0076", "E-PL9")
    olympusCamera.Add("SR45", "D220")
    olympusCamera.Add("SR55", "D320L")
    olympusCamera.Add("SR83", "D340L")
    olympusCamera.Add("SR85", "C830L,D340R")
    olympusCamera.Add("SR852", "C860L,D360L")
    olympusCamera.Add("SR872", "C900Z,D400Z")
    olympusCamera.Add("SR874", "C960Z,D460Z")
    olympusCamera.Add("SR951", "C2000Z")
    olympusCamera.Add("SR952", "C21")
    olympusCamera.Add("SR953", "C21T.commu")
    olympusCamera.Add("SR954", "C2020Z")
    olympusCamera.Add("SR955", "C990Z,D490Z")
    olympusCamera.Add("SR956", "C211Z")
    olympusCamera.Add("SR959", "C990ZS,D490Z")
    olympusCamera.Add("SR95A", "C2100UZ")
    olympusCamera.Add("SR971", "C100,D370")
    olympusCamera.Add("SR973", "C2,D230")
    olympusCamera.Add("SX151", "E100RS")
    olympusCamera.Add("SX351", "C3000Z / C3030Z")
    olympusCamera.Add("SX354", "C3040Z")
    olympusCamera.Add("SX355", "C2040Z")
    olympusCamera.Add("SX357", "C700UZ")
    olympusCamera.Add("SX358", "C200Z,D510Z")
    olympusCamera.Add("SX374", "C3100Z,C3020Z")
    olympusCamera.Add("SX552", "C4040Z")
    olympusCamera.Add("SX553", "C40Z,D40Z")
    olympusCamera.Add("SX556", "C730UZ")
    olympusCamera.Add("SX558", "C5050Z")
    olympusCamera.Add("SX571", "C120,D380")
    olympusCamera.Add("SX574", "C300Z,D550Z")
    olympusCamera.Add("SX575", "C4100Z,C4000Z")
    olympusCamera.Add("SX751", "X200,D560Z,C350Z")
    olympusCamera.Add("SX752", "X300,D565Z,C450Z")
    olympusCamera.Add("SX753", "C750UZ")
    olympusCamera.Add("SX754", "C740UZ")
    olympusCamera.Add("SX755", "C755UZ")
    olympusCamera.Add("SX756", "C5060WZ")
    olympusCamera.Add("SX757", "C8080WZ")
    olympusCamera.Add("SX758", "X350,D575Z,C360Z")
    olympusCamera.Add("SX759", "X400,D580Z,C460Z")
    olympusCamera.Add("SX75A", "AZ-2ZOOM")
    olympusCamera.Add("SX75B", "D595Z,C500Z")
    olympusCamera.Add("SX75C", "X550,D545Z,C480Z")
    olympusCamera.Add("SX75D", "IR-300")
    olympusCamera.Add("SX75F", "C55Z,C5500Z")
    olympusCamera.Add("SX75G", "C170,D425")
    olympusCamera.Add("SX75J", "C180,D435")
    olympusCamera.Add("SX771", "C760UZ")
    olympusCamera.Add("SX772", "C770UZ")
    olympusCamera.Add("SX773", "C745UZ")
    olympusCamera.Add("SX774", "X250,D560Z,C350Z")
    olympusCamera.Add("SX775", "X100,D540Z,C310Z")
    olympusCamera.Add("SX776", "C460ZdelSol")
    olympusCamera.Add("SX777", "C765UZ")
    olympusCamera.Add("SX77A", "D555Z,C315Z")
    olympusCamera.Add("SX851", "C7070WZ")
    olympusCamera.Add("SX852", "C70Z,C7000Z")
    olympusCamera.Add("SX853", "SP500UZ")
    olympusCamera.Add("SX854", "SP310")
    olympusCamera.Add("SX855", "SP350")
    olympusCamera.Add("SX873", "SP320")
    olympusCamera.Add("SX875", "FE180/X745")
    olympusCamera.Add("SX876", "FE190/X750")

    olympusSceneMode.Add(0, "Standard")
    olympusSceneMode.Add(6, "Auto")
    olympusSceneMode.Add(7, "Sport")
    olympusSceneMode.Add(8, "Portrait")
    olympusSceneMode.Add(9, "Landscape+Portrait")
    olympusSceneMode.Add(10, "Landscape")
    olympusSceneMode.Add(11, "Night Scene")
    olympusSceneMode.Add(12, "Self Portrait")
    olympusSceneMode.Add(13, "Panorama")
    olympusSceneMode.Add(14, "2 in 1")
    olympusSceneMode.Add(15, "Movie")
    olympusSceneMode.Add(16, "Landscape+Portrait")
    olympusSceneMode.Add(17, "Night+Portrait")
    olympusSceneMode.Add(18, "Indoor")
    olympusSceneMode.Add(19, "Fireworks")
    olympusSceneMode.Add(20, "Sunset")
    olympusSceneMode.Add(21, "Beauty Skin")
    olympusSceneMode.Add(22, "Macro")
    olympusSceneMode.Add(23, "Super Macro")
    olympusSceneMode.Add(24, "Food")
    olympusSceneMode.Add(25, "Documents")
    olympusSceneMode.Add(26, "Museum")
    olympusSceneMode.Add(27, "Shoot & Select")
    olympusSceneMode.Add(28, "Beach & Snow")
    olympusSceneMode.Add(29, "Self Protrait+Timer")
    olympusSceneMode.Add(30, "Candle")
    olympusSceneMode.Add(31, "Available Light")
    olympusSceneMode.Add(32, "Behind Glass")
    olympusSceneMode.Add(33, "My Mode")
    olympusSceneMode.Add(34, "Pet")
    olympusSceneMode.Add(35, "Underwater Wide1")
    olympusSceneMode.Add(36, "Underwater Macro")
    olympusSceneMode.Add(37, "Shoot & Select1")
    olympusSceneMode.Add(38, "Shoot & Select2")
    olympusSceneMode.Add(39, "High Key")
    olympusSceneMode.Add(40, "Digital Image Stabilization")
    olympusSceneMode.Add(41, "Auction")
    olympusSceneMode.Add(42, "Beach")
    olympusSceneMode.Add(43, "Snow")
    olympusSceneMode.Add(44, "Underwater Wide2")
    olympusSceneMode.Add(45, "Low Key")
    olympusSceneMode.Add(46, "Children")
    olympusSceneMode.Add(47, "Vivid")
    olympusSceneMode.Add(48, "Nature Macro")
    olympusSceneMode.Add(49, "Underwater Snapshot")
    olympusSceneMode.Add(50, "Shooting Guide")
    olympusSceneMode.Add(54, "Face Portrait")
    olympusSceneMode.Add(57, "Bulb")
    olympusSceneMode.Add(59, "Smile Shot")
    olympusSceneMode.Add(60, "Quick Shutter")
    olympusSceneMode.Add(63, "Slow Shutter")
    olympusSceneMode.Add(64, "Bird Watching")
    olympusSceneMode.Add(65, "Multiple Exposure")
    olympusSceneMode.Add(66, "e-Portrait")
    olympusSceneMode.Add(67, "Soft Background Shot")
    olympusSceneMode.Add(142, "Hand-held Starlight")
    olympusSceneMode.Add(154, "HDR")

    olympusArtFilter.Add(0, "Off")
    olympusArtFilter.Add(1, "Soft Focus")
    olympusArtFilter.Add(2, "Pop Art")
    olympusArtFilter.Add(3, "Pale & Light Color")
    olympusArtFilter.Add(4, "Light Tone")
    olympusArtFilter.Add(5, "Pin Hole")
    olympusArtFilter.Add(6, "Grainy Film")
    olympusArtFilter.Add(9, "Diorama")
    olympusArtFilter.Add(10, "Cross Process")
    olympusArtFilter.Add(12, "Fish Eye")
    olympusArtFilter.Add(13, "Drawing")
    olympusArtFilter.Add(14, "Gentle Sepia")
    olympusArtFilter.Add(15, "Pale & Light Color II")
    olympusArtFilter.Add(16, "Pop Art II")
    olympusArtFilter.Add(17, "Pin Hole II")
    olympusArtFilter.Add(18, "Pin Hole III")
    olympusArtFilter.Add(19, "Grainy Film II")
    olympusArtFilter.Add(20, "Dramatic Tone")
    olympusArtFilter.Add(21, "Punk")
    olympusArtFilter.Add(22, "Soft Focus 2")
    olympusArtFilter.Add(23, "Sparkle")
    olympusArtFilter.Add(24, "Watercolor")
    olympusArtFilter.Add(25, "Key Line")
    olympusArtFilter.Add(26, "Key Line II")
    olympusArtFilter.Add(27, "Miniature")
    olympusArtFilter.Add(28, "Reflection")
    olympusArtFilter.Add(29, "Fragmented")
    olympusArtFilter.Add(31, "Cross Process II")
    olympusArtFilter.Add(32, "Dramatic Tone II")
    olympusArtFilter.Add(33, "Watercolor I")
    olympusArtFilter.Add(34, "Watercolor II")
    olympusArtFilter.Add(35, "Diorama II")
    olympusArtFilter.Add(36, "Vintage")
    olympusArtFilter.Add(37, "Vintage II")
    olympusArtFilter.Add(38, "Vintage III")
    olympusArtFilter.Add(39, "Partial Color")
    olympusArtFilter.Add(40, "Partial Color II")
    olympusArtFilter.Add(41, "Partial Color III")

    olympusMagicFilter.Add(0, "Off")
    olympusMagicFilter.Add(1, "Soft Focus")
    olympusMagicFilter.Add(2, "Pop Art")
    olympusMagicFilter.Add(3, "Pale & Light Color")
    olympusMagicFilter.Add(4, "Light Tone")
    olympusMagicFilter.Add(5, "Pin Hole")
    olympusMagicFilter.Add(6, "Grainy Film")
    olympusMagicFilter.Add(9, "Diorama")
    olympusMagicFilter.Add(10, "Cross Process")
    olympusMagicFilter.Add(12, "Fish Eye")
    olympusMagicFilter.Add(13, "Drawing")
    olympusMagicFilter.Add(14, "Gentle Sepia")
    olympusMagicFilter.Add(15, "Pale & Light Color II")
    olympusMagicFilter.Add(16, "Pop Art II")
    olympusMagicFilter.Add(17, "Pin Hole II")
    olympusMagicFilter.Add(18, "Pin Hole III")
    olympusMagicFilter.Add(19, "Grainy Film II")
    olympusMagicFilter.Add(20, "Dramatic Tone")
    olympusMagicFilter.Add(21, "Punk")
    olympusMagicFilter.Add(22, "Soft Focus 2")
    olympusMagicFilter.Add(23, "Sparkle")
    olympusMagicFilter.Add(24, "Watercolor")
    olympusMagicFilter.Add(25, "Key Line")
    olympusMagicFilter.Add(26, "Key Line II")
    olympusMagicFilter.Add(27, "Miniature")
    olympusMagicFilter.Add(28, "Reflection")
    olympusMagicFilter.Add(29, "Fragmented")
    olympusMagicFilter.Add(31, "Cross Process II")
    olympusMagicFilter.Add(32, "Dramatic Tone II")
    olympusMagicFilter.Add(33, "Watercolor I")
    olympusMagicFilter.Add(34, "Watercolor II")
    olympusMagicFilter.Add(35, "Diorama II")
    olympusMagicFilter.Add(36, "Vintage")
    olympusMagicFilter.Add(37, "Vintage II")
    olympusMagicFilter.Add(38, "Vintage III")
    olympusMagicFilter.Add(39, "Partial Color")
    olympusMagicFilter.Add(40, "Partial Color II")
    olympusMagicFilter.Add(41, "Partial Color III")

    olympusArtFilterEffect.Add(&H0, "Off")
    olympusArtFilterEffect.Add(&H1, "Soft Focus")
    olympusArtFilterEffect.Add(&H2, "Pop Art")
    olympusArtFilterEffect.Add(&H3, "Pale & Light Color")
    olympusArtFilterEffect.Add(&H4, "Light Tone")
    olympusArtFilterEffect.Add(&H5, "Pin Hole")
    olympusArtFilterEffect.Add(&H6, "Grainy Film")
    olympusArtFilterEffect.Add(&H9, "Diorama")
    olympusArtFilterEffect.Add(&HA, "Cross Process")
    olympusArtFilterEffect.Add(&HC, "Fish Eye")
    olympusArtFilterEffect.Add(&HD, "Drawing")
    olympusArtFilterEffect.Add(&HE, "Gentle Sepia")
    olympusArtFilterEffect.Add(&HF, "Pale & Light Color II")
    olympusArtFilterEffect.Add(&H10, "Pop Art II")
    olympusArtFilterEffect.Add(&H11, "Pin Hole II")
    olympusArtFilterEffect.Add(&H12, "Pin Hole III")
    olympusArtFilterEffect.Add(&H13, "Grainy Film II")
    olympusArtFilterEffect.Add(&H14, "Dramatic Tone")
    olympusArtFilterEffect.Add(&H15, "Punk")
    olympusArtFilterEffect.Add(&H16, "Soft Focus 2")
    olympusArtFilterEffect.Add(&H17, "Sparkle")
    olympusArtFilterEffect.Add(&H18, "Watercolor")
    olympusArtFilterEffect.Add(&H19, "Key Line")
    olympusArtFilterEffect.Add(&H1A, "Key Line II")
    olympusArtFilterEffect.Add(&H1B, "Miniature")
    olympusArtFilterEffect.Add(&H1C, "Reflection")
    olympusArtFilterEffect.Add(&H1D, "Fragmented")
    olympusArtFilterEffect.Add(&H1F, "Cross Process II")
    olympusArtFilterEffect.Add(&H20, "Dramatic Tone II")
    olympusArtFilterEffect.Add(&H21, "Watercolor I")
    olympusArtFilterEffect.Add(&H22, "Watercolor II")
    olympusArtFilterEffect.Add(&H23, "Diorama II")
    olympusArtFilterEffect.Add(&H24, "Vintage")
    olympusArtFilterEffect.Add(&H25, "Vintage II")
    olympusArtFilterEffect.Add(&H26, "Vintage III")
    olympusArtFilterEffect.Add(&H27, "Partial Color")
    olympusArtFilterEffect.Add(&H28, "Partial Color II")
    olympusArtFilterEffect.Add(&H29, "Partial Color III")

    sonyPictureEffect.Add(0, "Off")
    sonyPictureEffect.Add(1, "Toy Camera")
    sonyPictureEffect.Add(2, "Pop Color")
    sonyPictureEffect.Add(3, "Posterization")
    sonyPictureEffect.Add(4, "Posterization B/W")
    sonyPictureEffect.Add(5, "Retro Photo")
    sonyPictureEffect.Add(6, "Soft High Key")
    sonyPictureEffect.Add(7, "Partial Color (red)")
    sonyPictureEffect.Add(8, "Partial Color (green)")
    sonyPictureEffect.Add(9, "Partial Color (blue)")
    sonyPictureEffect.Add(10, "Partial Color (yellow)")
    sonyPictureEffect.Add(13, "High Contrast Monochrome")
    sonyPictureEffect.Add(16, "Toy Camera (normal)")
    sonyPictureEffect.Add(17, "Toy Camera (cool)")
    sonyPictureEffect.Add(18, "Toy Camera (warm)")
    sonyPictureEffect.Add(19, "Toy Camera (green)")
    sonyPictureEffect.Add(20, "Toy Camera (magenta)")
    sonyPictureEffect.Add(32, "Soft Focus (low)")
    sonyPictureEffect.Add(33, "Soft Focus")
    sonyPictureEffect.Add(34, "Soft Focus (high)")
    sonyPictureEffect.Add(48, "Miniature (auto)")
    sonyPictureEffect.Add(49, "Miniature (top)")
    sonyPictureEffect.Add(50, "Miniature (middle horizontal)")
    sonyPictureEffect.Add(51, "Miniature (bottom)")
    sonyPictureEffect.Add(52, "Miniature (left)")
    sonyPictureEffect.Add(53, "Miniature (middle vertical)")
    sonyPictureEffect.Add(54, "Miniature (right)")
    sonyPictureEffect.Add(64, "HDR Painting (low)")
    sonyPictureEffect.Add(65, "HDR Painting")
    sonyPictureEffect.Add(66, "HDR Painting (high)")
    sonyPictureEffect.Add(80, "Rich-tone Monochrome")
    sonyPictureEffect.Add(97, "Water Color")
    sonyPictureEffect.Add(98, "Water Color 2")
    sonyPictureEffect.Add(112, "Illustration (low)")
    sonyPictureEffect.Add(113, "Illustration")
    sonyPictureEffect.Add(114, "Illustration (high)")

    sonyColorMode.Add(0, "Standard")
    sonyColorMode.Add(1, "Vivid")
    sonyColorMode.Add(2, "Portrait")
    sonyColorMode.Add(3, "Landscape")
    sonyColorMode.Add(4, "Sunset")
    sonyColorMode.Add(5, "Night View/Portrait")
    sonyColorMode.Add(6, "B&W")
    sonyColorMode.Add(7, "Adobe RGB")
    sonyColorMode.Add(12, "Neutral")
    sonyColorMode.Add(13, "Clear")
    sonyColorMode.Add(14, "Deep")
    sonyColorMode.Add(15, "Light")
    sonyColorMode.Add(16, "Autumn Leaves")
    sonyColorMode.Add(17, "Sepia")
    sonyColorMode.Add(100, "Neutral")
    sonyColorMode.Add(101, "Clear")
    sonyColorMode.Add(102, "Deep")
    sonyColorMode.Add(103, "Light")
    sonyColorMode.Add(104, "Night View")
    sonyColorMode.Add(105, "Autumn Leaves")

    sonyModelID.Add(2, "DSC-R1")
    sonyModelID.Add(256, "DSLR-A100")
    sonyModelID.Add(257, "DSLR-A900")
    sonyModelID.Add(258, "DSLR-A700")
    sonyModelID.Add(259, "DSLR-A200")
    sonyModelID.Add(260, "DSLR-A350")
    sonyModelID.Add(261, "DSLR-A300")
    sonyModelID.Add(262, "DSLR-A900 (APS-C mode)")
    sonyModelID.Add(263, "DSLR-A380/A390")
    sonyModelID.Add(264, "DSLR-A330")
    sonyModelID.Add(265, "DSLR-A230")
    sonyModelID.Add(266, "DSLR-A290")
    sonyModelID.Add(269, "DSLR-A850")
    sonyModelID.Add(270, "DSLR-A850 (APS-C mode)")
    sonyModelID.Add(273, "DSLR-A550")
    sonyModelID.Add(274, "DSLR-A500")
    sonyModelID.Add(275, "DSLR-A450")
    sonyModelID.Add(278, "NEX-5")
    sonyModelID.Add(279, "NEX-3")
    sonyModelID.Add(280, "SLT-A33")
    sonyModelID.Add(281, "SLT-A55 / SLT-A55V")
    sonyModelID.Add(282, "DSLR-A560")
    sonyModelID.Add(283, "DSLR-A580")
    sonyModelID.Add(284, "NEX-C3")
    sonyModelID.Add(285, "SLT-A35")
    sonyModelID.Add(286, "SLT-A65 / SLT-A65V")
    sonyModelID.Add(287, "SLT-A77 / SLT-A77V")
    sonyModelID.Add(288, "NEX-5N")
    sonyModelID.Add(289, "NEX-7")
    sonyModelID.Add(290, "NEX-VG20E")
    sonyModelID.Add(291, "SLT-A37")
    sonyModelID.Add(292, "SLT-A57")
    sonyModelID.Add(293, "NEX-F3")
    sonyModelID.Add(294, "SLT-A99 / SLT-A99V")
    sonyModelID.Add(295, "NEX-6")
    sonyModelID.Add(296, "NEX-5R")
    sonyModelID.Add(297, "DSC-RX100")
    sonyModelID.Add(298, "DSC-RX1")
    sonyModelID.Add(299, "NEX-VG900")
    sonyModelID.Add(300, "NEX-VG30E")
    sonyModelID.Add(302, "ILCE-3000 / ILCE-3500")
    sonyModelID.Add(303, "SLT-A58")
    sonyModelID.Add(305, "NEX-3N")
    sonyModelID.Add(306, "ILCE-7")
    sonyModelID.Add(307, "NEX-5T")
    sonyModelID.Add(308, "DSC-RX100M2")
    sonyModelID.Add(309, "DSC-RX10")
    sonyModelID.Add(310, "DSC-RX1R")
    sonyModelID.Add(311, "ILCE-7R")
    sonyModelID.Add(312, "ILCE-6000")
    sonyModelID.Add(313, "ILCE-5000")
    sonyModelID.Add(317, "DSC-RX100M3")
    sonyModelID.Add(318, "ILCE-7S")
    sonyModelID.Add(319, "ILCA-77M2")
    sonyModelID.Add(339, "ILCE-5100")
    sonyModelID.Add(340, "ILCE-7M2")
    sonyModelID.Add(341, "DSC-RX100M4")
    sonyModelID.Add(342, "DSC-RX10M2")
    sonyModelID.Add(344, "DSC-RX1RM2")
    sonyModelID.Add(346, "ILCE-QX1")
    sonyModelID.Add(347, "ILCE-7RM2")
    sonyModelID.Add(350, "ILCE-7SM2")
    sonyModelID.Add(353, "ILCA-68")
    sonyModelID.Add(354, "ILCA-99M2")
    sonyModelID.Add(355, "DSC-RX10M3")
    sonyModelID.Add(356, "DSC-RX100M5")
    sonyModelID.Add(357, "ILCE-6300")
    sonyModelID.Add(358, "ILCE-9")
    sonyModelID.Add(360, "ILCE-6500")
    sonyModelID.Add(362, "ILCE-7RM3")
    sonyModelID.Add(363, "ILCE-7M3")
    sonyModelID.Add(364, "DSC-RX0")
    sonyModelID.Add(365, "DSC-RX10M4")
    sonyModelID.Add(366, "DSC-RX100M6")
    sonyModelID.Add(367, "DSC-HX99")
    sonyModelID.Add(369, "DSC-RX100M5A")
    sonyModelID.Add(371, "ILCE-6400")

  End Sub


End Module


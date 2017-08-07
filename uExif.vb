Imports vb = Microsoft.VisualBasic
Imports System.Text
Imports System.IO
Imports System.Math
Imports System

' handles exif manually, for makernote

Public Class uExif
  Implements IDisposable

  Dim fStream As FileStream
  Dim binRead As BinaryReader
  Dim binWrite As BinaryWriter

  'Private Structure ifdEntry
  'Dim tag As UInteger
  'Dim dataType As UShort
  'Dim count As UInteger
  'Dim Value As UInteger
  'End Structure

  Dim fBase As Integer
  Dim intel As Boolean
  Dim app1Size As Integer

  Public Tags As New Collection
  Public iptcTags As New Collection
  Public xmp() As Byte = Nothing
  Dim uNextIFD As uExif = Nothing
  Dim ufData() As Byte

  Public Enum TagID
    aperture = &H9202
    artist = &H13B
    bitspersample = &H102
    brightness = &H9203
    cfapattern = &HA302
    colorspace = &HA001
    ComponentsConfiguration = &H9101
    CompressedBitsPerPixel = &H9102
    Compression = &H103
    Contrast = &HA408
    Copyright = &H8298
    CustomRendered = &HA401
    DateTime = &H132
    DateTimeDigitized = &H9004
    DateTimeOriginal = &H9003
    Description = &H10E
    devicesettingdescription = &HA40B
    DigitalZoomRatio = &HA404
    DocumentName = &H10D
    exifpointer = &H8769
    exifversion = &H9000
    exposurebias = &H9204
    exposureindex = &HA215
    exposuremode = &HA402
    exposureprogram = &H8822
    exposuretime = &H829A
    filesource = &HA300
    flash = &H9209
    flashenergy = &HA20B
    FlashpixVersion = &HA000
    fnumber = &H829D
    focallength = &H920A
    focallengthin35mmfilm = &HA405
    FocalPlaneResolutionUnit = &HA210
    focalplaneXresolution = &HA20E
    focalplaneYresolution = &HA20F
    gaincontrol = &HA407
    gpsaltitude = &H6
    gpsaltituderef = &H5
    GPSAreaInformation = &H1C
    gpsDateStamp = &H1D
    gpsdestbearing = &H18
    gpsdestbearingref = &H17
    gpsdestdistance = &H1A
    gpsdestdistanceref = &H19
    gpsdestlatitude = &H14
    gpsdestlatituderef = &H13
    gpsdestlongitude = &H16
    gpsdestlongituderef = &H15
    GPSDifferential = &H1E
    gpsdop = &HB
    gpsimgdirection = &H11
    gpsimgdirectionref = &H10
    gpslatitude = &H2
    gpslatituderef = &H1
    gpslongitude = &H4
    gpslongituderef = &H3
    gpsmapdatum = &H12
    gpsmeasuremode = &HA
    gpspointer = &H8825
    GPSProcessingMethod = &H1B
    gpssatellites = &H8
    gpsspeed = &HD
    gpsspeedref = &HC
    gpsstatus = &H9
    gpstimestamp = &H7
    gpstrack = &HF
    gpstrackref = &HE
    gpsversionid = &H0
    hostcomputer = &H13C
    ImageHeight = &H101
    imageuniqueid = &HA420
    ImageWidth = &H100
    'InteropIndex = &H1
    'interopField = &H2
    'interoppointer = &HA005
    isospeedratings = &H8827
    JPEGInterchangeFormat = &H201
    JPEGInterchangeFormatLength = &H202
    lightsource = &H9208
    Make = &H10F
    makernote = &H927C
    maxaperture = &H9205
    meteringmode = &H9207
    Model = &H110
    oecf = &H8828
    Orientation = &H112
    pagename = &H11D
    PageNumber = &H297
    PhotometricInterpretation = &H106
    PixelXDimension = &HA002
    PixelYDimension = &HA003
    PlanarConfiguration = &H11C
    PrimaryChromaticities = &H13F
    ReferenceBlackWhite = &H214
    relatedsoundfile = &HA004
    ResolutionUnit = &H128
    RowsPerStrip = &H116
    SamplesPerPixel = &H115
    Saturation = &HA409
    scenecapturetype = &HA406
    scenetype = &HA301
    sensingmethod = &HA217
    sharpness = &HA40A
    shutterspeed = &H9201
    software = &H131
    spatialfrequencyresponse = &HA20C
    spectralsensitivity = &H8824
    StripByteCounts = &H117
    StripOffsets = &H111
    subjectarea = &H9214
    subjectdistance = &H9206
    subjectdistancerange = &HA40C
    subjectlocation = &HA214
    subsectime = &H9290
    subsectimedigitized = &H9292
    subsectimeoriginal = &H9291
    TransferFunction = &H12D
    usercomment = &H9286
    whitebalance = &HA403
    whitePoint = &H13E
    xpAuthor = &H9C9D
    xpComment = &H9C9C
    xpKeywords = &H9C9E
    xpSubject = &H9C9F
    xpTitle = &H9C9B
    XResolution = &H11A
    YCbCrCoefficients = &H211
    YCbCrPositioning = &H213
    YCbCrSubSampling = &H212
    YResolution = &H11B

  End Enum

  'ReadOnly Property Tags() As Collection
  'Get
  '  Tags = tags
  'End Get
  'End Property

  ReadOnly Property fdata() As Byte()
    Get
      fdata = ufData ' assigns a pointer (hopefully)
    End Get
  End Property

  ReadOnly Property IntelNumbers() As Boolean
    Get
      IntelNumbers = intel
    End Get
  End Property

  ReadOnly Property Nextifd() As uExif
    Get
      Nextifd = uNextIFD
    End Get
  End Property

  Public Function readExif(ByRef fName As String) As Integer

    ' read exif, return 0 of successful

    Dim b1, b2 As Byte
    Dim k As Integer
    Dim fpos As Integer
    Dim b() As Byte
    Dim s As String

    readExif = -1 ' default

    Try
      fStream = File.Open(fName, FileMode.Open, FileAccess.Read, FileShare.Read)
      binRead = New BinaryReader(fStream)
    Catch ex As Exception
      readExif = -1
      MsgBox("There was an error reading comments for " & fName & ". " & crlf & ex.Message)
      Try
        If fStream IsNot Nothing Then fStream.Close()
      Catch
      End Try
      Exit Function
    End Try

    fpos = 1
    b1 = readByte(fpos)
    b2 = readByte()

    ufData = Nothing
    fBase = 1

    s = ChrW(b1) & ChrW(b2)
    If s = "II" Or s = "MM" Then ' the file is not .jpg, try tiff/raw
      readExif = readTif() ' uses fStream
      fStream.Close()
      Exit Function
    End If


    If b1 = 255 And b2 = &HD8 Then ' jpg file SOI Marker
      fpos = 3
      b1 = readByte(fpos)
      b2 = readByte()

      Do While b1 = 255 And (b2 And &HE0) = &HE0 And (fStream.Position < fStream.Length)
        ' read the app sections
        app1Size = readWord(False)

        If b2 = &HE1 Then ' read app1 for exif stuff
          Try
            b = binRead.ReadBytes(23)
          Catch
            ReDim b(22)
          End Try

          If eqstr(UTF8bare.GetString(b, 0, 4), "exif") Then ' get exif app1
            fBase = fpos + 10
            fStream.Seek(fBase - 1, SeekOrigin.Begin)
            Try
              ufData = binRead.ReadBytes(app1Size - 8)
            Catch
              ReDim ufData(app1Size - 9)
            End Try

            ' Here it should be jpg exif, at ifd 1.
            If eqstr(ChrW(ufData(0)) & ChrW(ufData(1)), "ii") Then
              intel = True
            ElseIf eqstr(ChrW(ufData(0)) & ChrW(ufData(1)), "mm") Then
              intel = False
            Else
              Exit Do ' no good
            End If

            Tags = New Collection
            k = getDWord(ufData, 4, intel) ' first ifd

            getIFDirectory(ufData, k, intel, 0) ' k is link to   next ifd, absolute links
            readExif = Tags.Count

          ElseIf eqstr(UTF8bare.GetString(b, 0, 23), "http://ns.adobe.com/xap") Then ' xmp
            Try
              xmp = binRead.ReadBytes(app1Size)
            Catch
              xmp = Nothing
            End Try
          End If

        ElseIf b2 = &HED Then ' iptc?
          Try
            b = binRead.ReadBytes(app1Size)
          Catch
            b = Nothing
          End Try
          k = UBound(b)
          If k > 40 Then k = 40
          k = InStr(UTF8bare.GetString(b, 0, k), "8BIM" & ChrW(4) & ChrW(4) & ChrW(0) & ChrW(0) & ChrW(0) & ChrW(0))
          If k > 0 Then
            k = k + 11
            getIPTC(b, k)
          End If

        End If

        fpos = fpos + app1Size + 2
        b1 = readByte(fpos)
        b2 = readByte()
      Loop

    End If

    fStream.Close()

  End Function

  Sub getIPTC(ByRef b As Byte(), ByRef ipos As Integer)

    Dim k As Integer
    Dim ii(0) As Integer
    Dim ss(0) As String
    Dim iType As Integer
    Dim tg As uTag

    Do While ipos < UBound(b) AndAlso (b(ipos) = &H1C And b(ipos + 1) = 2)
      iType = b(ipos + 2) ' type
      tg = New uTag
      tg.tag = iType
      If iType = 0 Then ' integer
        k = getWord(b, ipos + 3, False) ' length - should be 2
        ii(0) = getWord(b, ipos + 5, False) ' value -- assume only one integer
        tg.dataType = 0 ' integer
        tg.Value = ii
      Else
        k = getWord(b, ipos + 3, False) ' length
        ss(0) = UTF8bare.GetString(b, ipos + 5, k)
        ss(0) = ss(0).Trim(whiteSpace)
        tg.dataType = 2 ' string
        tg.Value = ss
      End If
      ipos = ipos + k + 7

      If Not iptcTagExists(tg.tag) Then iptcTags.Add(tg, tg.key) ' key is a four character hex value for tag - Right("0000" & Hex(tag), 4)

    Loop

  End Sub


  Private Function readTif() As Integer
    ' read exif, return 0 of successful

    Dim k As Integer

    readTif = -1 ' default
    ' stream is already open
    app1Size = 5000 ' just a guess. Should be big enough
    fStream.Seek(0, SeekOrigin.Begin)

    Try
      ufData = binRead.ReadBytes(app1Size)
    Catch
      ReDim ufData(app1Size - 1)
    End Try

    ' Here it should be tiff exif.
    If eqstr(ChrW(ufData(0)) & ChrW(ufData(1)), "ii") Then
      intel = True
    ElseIf eqstr(ChrW(ufData(0)) & ChrW(ufData(1)), "mm") Then
      intel = False
    Else
      Exit Function ' no good
    End If

    Tags = New Collection
    k = getDWord(ufData, 4, intel) ' first ifd
    If k >= 8 And k < 500 Then
      getIFDirectory(ufData, k, intel, 0) ' k is link to   next ifd, absolute links
    End If
    readTif = Tags.Count

    fStream.Close()

  End Function

  Sub getIFDirectory(ByRef ufdata() As Byte, ByVal ifdPointer As Integer, ByVal intel As Boolean, ByVal relativeLinks As Short)

    ' relativelinks: 0 = absolute, 1 = relative, 2 = disable links

    ' ufdata comes from this instance only if called from readexif.
    ' This is also called recursively and from imageinfo.bas for makernotes

    Dim Nifd As UInteger
    Dim IFD As ifdEntry
    Dim i, k As Integer
    Dim ii1 As Long
    Dim tg As uTag
    Dim noLinks As Boolean
    Dim linkOffset As Integer
    Dim n As Integer

    noLinks = True
    If relativeLinks = 2 Then linkOffset = -1 Else linkOffset = 0 ' linkoffset = -1 to disable links
    Tags = New Collection
    If ifdPointer >= UBound(ufdata) Then Exit Sub
    Nifd = getWord(ufdata, ifdPointer, intel)
    If Nifd > 1000 Or Nifd * 12 + ifdPointer + 5 > UBound(ufdata) Then Exit Sub ' must be an error

    For i = 0 To Nifd - 1
      IFD.tag = getWord(ufdata, i * 12 + ifdPointer + 2, intel)
      IFD.dataType = getWord(ufdata, i * 12 + ifdPointer + 4, intel)
      IFD.count = getDWord(ufdata, i * 12 + ifdPointer + 6, intel)
      IFD.Value = getDWord(ufdata, i * 12 + ifdPointer + 10, intel)
      'If IFD.count > 65535 Then Exit For

      If noLinks And relativeLinks = 1 Then ' set the link offset assuming the first data is immediately after the last tag
        Select Case IFD.dataType
          Case 1, 2, 6, 7
            n = IFD.count
          Case 3, 8
            n = IFD.count * 2
          Case 4, 9, 11
            n = IFD.count * 4
          Case 5, 10, 12
            n = IFD.count * 8
        End Select
        If n > 4 Then ' 1st link - get fileoffset
          k = IFD.Value
          linkOffset = k - (Nifd * 12 + 2 + 4) - ifdPointer
          noLinks = False
        End If
      End If

      tg = New uTag
      tg.Link = IFD.Value ' this is the link if there is a link -- if n > 4, else tg.link is the value (undefined)
      tg.tag = IFD.tag

      tg.dataType = IFD.dataType
      tg.Value = GetTagValue(ufdata, IFD, intel, linkOffset)  ''''

      If Not tagExists(IFD.tag) Then Tags.Add(tg, tg.key) ' key is a four character hex value for tag - Right("0000" & Hex(tag), 4)
      ' If IFD.tag = TagID.exifpointer Or IFD.tag = TagID.gpspointer Then ' Or IFD.tag = TagID.interoppointer Then
      If IFD.tag = TagID.exifpointer Or IFD.tag = TagID.gpspointer Then ' Or IFD.tag = TagID.interoppointer Then
        If relativeLinks <> 0 Or IFD.Value > ifdPointer Then
          tg.IFD = New uExif
          tg.IFD.getIFDirectory(ufdata, IFD.Value, intel, relativeLinks) ' recursive
        End If
      End If
    Next i

    ii1 = getDWord(ufdata, Nifd * 12 + ifdPointer + 2, intel)
    If ii1 > 0 And ii1 < UBound(ufdata) Then
      uNextIFD = New uExif
      '  uNextIFD.getIFDirectory(ufdata, ii1, intel, relativeLinks)
    End If

  End Sub

  Private Function readDWord(ByRef intel As Boolean, Optional ByRef iPos As Integer = -1, Optional ByRef dSigned As Boolean = False) As Integer

    Dim b(3) As Byte

    If iPos > 0 Then fStream.Seek(iPos - 1, SeekOrigin.Begin)
    If intel Then
      If dSigned Then
        Try
          readDWord = binRead.ReadInt32()
        Catch
          readDWord = 0
        End Try
      Else
        Try
          readDWord = binRead.ReadUInt32()
        Catch
          readDWord = 0
        End Try
      End If

    Else ' backwards bytes
      Try
        b(3) = binRead.ReadByte() ' read 3 before 2, etc, then do an intel conversion
        b(2) = binRead.ReadByte()
        b(1) = binRead.ReadByte()
        b(0) = binRead.ReadByte
      Catch
      End Try
      If dSigned Then
        readDWord = BitConverter.ToInt32(b, 0)
      Else
        readDWord = BitConverter.ToUInt32(b, 0)
      End If
    End If

  End Function

  Private Function readWord(ByRef intel As Boolean, Optional ByRef iPos As Integer = -1, Optional ByRef dSigned As Boolean = False) As Integer

    Dim b(1) As Byte

    If iPos > 0 Then fStream.Seek(iPos - 1, SeekOrigin.Begin)
    If intel Then
      If dSigned Then
        Try
          readWord = binRead.ReadInt16()
        Catch
          readWord = 0
        End Try
      Else
        Try
          readWord = binRead.ReadUInt16()
        Catch
          readWord = 0
        End Try
      End If

    Else ' backwards bytes
      Try
        b(1) = binRead.ReadByte() ' read 1 before 0, then do an intel conversion
        b(0) = binRead.ReadByte
      Catch
      End Try
      If dSigned Then
        readWord = BitConverter.ToInt16(b, 0)
      Else
        readWord = BitConverter.ToUInt16(b, 0)
      End If
    End If

  End Function

  Private Function readByte(Optional ByRef iPos As Integer = -1) As Byte

    If iPos > 0 Then fStream.Seek(iPos - 1, SeekOrigin.Begin)

    Try
      readByte = binRead.ReadByte()
    Catch
      readByte = 0
    End Try

  End Function

  Private Function readString(ByRef n As Integer, Optional ByRef iPos As Integer = -1) As String

    Dim b(n - 1) As Byte
    If iPos > 0 Then fStream.Seek(iPos - 1, SeekOrigin.Begin)
    Try
      b = binRead.ReadBytes(n)
    Catch
      ReDim b(0)
    End Try

    readString = UTF8bare.GetString(b)
    readString = readString.Replace(ChrW(0), vbCrLf).Trim ' lines may be separated by zero

  End Function

  Private Function GetTagValue(ByRef ufdata As Byte(), ByRef IFD As ifdEntry, ByVal intel As Boolean, ByRef linkOffset As Integer) As Object

    Dim v As Object = Nothing
    Dim i As Integer
    Dim ii1, ii2 As Long
    Dim n As Long
    Dim b() As Byte
    Dim bi As Byte
    Dim vs() As String
    Dim vx() As Double
    Dim vn() As UInteger
    Dim vnSigned() As Integer
    Dim vw() As UShort
    Dim dSigned As Boolean


    Select Case IFD.dataType
      Case 1, 2, 6, 7
        n = IFD.count
      Case 3, 8
        n = IFD.count * 2
      Case 4, 9, 11
        n = IFD.count * 4
      Case 5, 10, 12
        n = IFD.count * 8
      Case Else
        ReDim v(-1) ' return empty array
        Return v
    End Select

    If n <= 0 Or n > 65535 Then
      ReDim v(-1) ' return empty array
      Return v
    End If

    ReDim b(n - 1)

    If n <= 4 Then
      ReDim b(4)
      b = BitConverter.GetBytes(IFD.Value)
      If Not intel Then ' reverse bytes
        bi = b(0) : b(0) = b(3) : b(3) = bi
        bi = b(1) : b(1) = b(2) : b(2) = bi
      End If
      ReDim Preserve b(n - 1)
    Else
      If (IFD.Value < 0) Or (IFD.Value + (n - 1) - linkOffset > UBound(ufdata)) Or (IFD.Value < linkOffset) Or linkOffset = -1 Then ' changed 7/21/17
        ReDim v(-1) ' return empty array
        Return v
      End If
      For i = 0 To n - 1
        b(i) = ufdata(i + IFD.Value - linkOffset)
      Next i
    End If

    If IFD.dataType = 8 Or IFD.dataType = 9 Or IFD.dataType = 10 Then dSigned = True Else dSigned = False

    Select Case IFD.dataType
      Case 1, 6, 7 ' byte, sbyte, undefined
        v = b.Clone

      Case 2 ' ascii (string)
        ReDim vs(0) ' always a single string, separate multiples by 0
        vs(0) = UTF8bare.GetString(b, 0, UBound(b) + 1)
        vs(0) = vs(0).Trim(whiteSpace)
        v = vs.Clone

      Case 3, 8 ' unsigned short (signed short is not defined)
        ReDim vw(IFD.count - 1)
        For i = 0 To IFD.count - 1
          vw(i) = getWord(b, i * 2, intel)
        Next i
        v = vw.Clone

      Case 4 ' unsigned int32
        ReDim vn(IFD.count - 1)
        For i = 0 To IFD.count - 1
          vn(i) = getDWord(b, i * 2, intel)
        Next i
        v = vn.Clone

      Case 9 ' int32, signed in32
        ReDim vnSigned(IFD.count - 1)
        For i = 0 To IFD.count - 1
          vnSigned(i) = getDWordSigned(b, i * 2, intel)
        Next i
        v = vnSigned.Clone

      Case 5, 10 ' rational, srational
        ReDim vx(IFD.count - 1)
        For i = 0 To IFD.count - 1
          If dSigned Then
            ii1 = getDWordSigned(b, i * 8, intel)
            ii2 = getDWordSigned(b, i * 8 + 4, intel)
          Else
            ii1 = getDWord(b, i * 8, intel)
            If ii1 > 2 ^ 31 + 2 ^ 30 Then ii1 = ii1 - 2 ^ 32 '  this is probably because someone (like GPSStamp) got the signs wrong. It's technically incorrect.
            ii2 = getDWord(b, i * 8 + 4, intel)
          End If
          If ii2 <> 0 Then vx(i) = ii1 / ii2 Else vx(i) = 0
        Next i
        v = vx.Clone

      Case 11 ' float (single) ' read again from file
        ReDim vx(IFD.count - 1)
        For i = 0 To IFD.count - 1
          vx(i) = BitConverter.ToSingle(ufdata, IFD.Value + i * 4)
        Next i
        v = vx.Clone

      Case 12 ' float (single), double ' read again from file
        ReDim vx(IFD.count - 1)
        For i = 0 To IFD.count - 1
          vx(i) = BitConverter.ToDouble(ufdata, IFD.Value + i * 8)
        Next i
        v = vx.Clone

    End Select

    GetTagValue = v

  End Function

  Private Function getDWord(ByRef b() As Byte, ByRef i As Integer, ByRef intel As Boolean) As UInteger

    Dim k As UInteger

    If intel Then
      Return BitConverter.ToUInt32(b, i)
    Else
      k = b(i)
      For j As Integer = 1 To 3
        k = k << 8
        k = k + b(i + j)
      Next j
      Return k
      'x = b(i + 0) * 65536 * 256.0#
      'x = x + b(i + 1) * 65536.0#
      'x = x + b(i + 2) * 256.0#
      'x = x + b(i + 3)
    End If

  End Function

  Private Function getDWordSigned(ByRef b() As Byte, ByRef i As Integer, ByRef intel As Boolean) As Integer

    Dim k As Integer

    If intel Then
      Return BitConverter.ToInt32(b, i)
    Else
      k = b(i)
      For j As Integer = 1 To 3
        k = k << 8
        k = k + b(i + j)
      Next j
      Return k
      'x = b(i + 0) * 65536 * 256.0#
      'x = x + b(i + 1) * 65536.0#
      'x = x + b(i + 2) * 256.0#
      'x = x + b(i + 3)
    End If

    'If x >= 2 ^ 31 Then x = x - 2 ^ 32

  End Function

  Private Function getWordSigned(ByRef b() As Byte, ByRef i As Integer, ByRef intel As Boolean) As Short

    Dim k As Integer = 0

    If intel Then
      'k = k + b(i + 1) * 256
      'k = k + b(i)
      k = b(i + 1)
      k = (k << 8) + b(i)
      Return k
    Else
      'k = k + b(i) * 256
      'k = k + b(i + 1)
      k = b(i) * 256
      k = k + b(i + 1)
      Return k
    End If

    'If k >= 32768 Then k = k - 65536

  End Function
  Private Function getWord(ByRef b() As Byte, ByRef i As Integer, ByRef intel As Boolean) As UShort

    Dim k As Integer

    If intel Then
      k = b(i + 1)
      k = (k << 8) + b(i)
      Return k
    Else
      k = b(i)
      k = (k << 8) + b(i + 1)
      Return k
    End If

    getWord = k

  End Function

  Public Function TagValue(ByVal tag As TagID, Optional ByVal Subscript As Integer = -1) As Object

    Dim v As Object

    TagValue = Nothing

    If tagExists(tag) Then
      If Subscript < 0 Then
        TagValue = Tags.Item(sTag(tag)).Value
      Else
        TagValue = Tags.Item(sTag(tag)).singleValue(Subscript)
      End If
    Else
      If Subscript < 0 Then
        ' return array if not found, to prevent error
        ReDim v(-1)
        TagValue = v
      End If
    End If

  End Function

  Public Function tagIFD(ByVal tag As TagID) As uExif

    If tagExists(tag) Then
      tagIFD = Tags.Item(sTag(tag)).IFD
    Else
      tagIFD = Nothing
    End If

  End Function

  Public Function tagExists(ByVal tag As Integer) As Boolean
    tagExists = Tags.Contains(sTag(tag))
  End Function

  Public Function iptcTagExists(ByVal tag As Integer) As Boolean
    iptcTagExists = iptcTags.Contains(sTag(tag))
  End Function


  Protected Overridable Overloads Sub Dispose(disposing As Boolean)
    If disposing Then
      ' dispose managed resources
    End If

    If uNextIFD IsNot Nothing Then uNextIFD.Dispose() : uNextIFD = Nothing
    If fStream IsNot Nothing Then fStream.Dispose() : fStream = Nothing
    If binRead IsNot Nothing Then binRead.Dispose() : binRead = Nothing
    If binWrite IsNot Nothing Then binWrite.Dispose() : binWrite = Nothing

  End Sub 'Dispose

  Public Overloads Sub Dispose() Implements IDisposable.Dispose
    Dispose(True)
    GC.SuppressFinalize(Me)
  End Sub 'Dispose

End Class
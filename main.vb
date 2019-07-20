Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Drawing.Imaging
Imports System.Text.RegularExpressions
Imports System.Math
Imports Microsoft.Win32
Imports System.ServiceModel
Imports System.Text
Imports System.Collections.Generic

Imports System.ComponentModel

Imports System.Net.Mail
Imports System.Net.Mime

Imports ImageMagick

Public Module main

  Public Enum mergeOp
    None = 0
    opAdd
    opXor
    opAnd
    opOr
    opMaximum
    opMinimum
    opAverage
    opMultiply
    opDivideSource
    opDivideTarget
    opSubtractFromSource
    opSubtractFromTarget
    opReplace
    opReplaceNonzero
  End Enum

  Public Enum cmd ' interactive Command from frmMain
    None = 0
    Animate
    Brightness
    ColorSample
    Crop
    DrawArrow
    DrawBox
    DrawCircle
    DrawCurve
    DrawEllipse
    DrawFill
    DrawLine
    DrawMeasure
    DrawSketch
    DrawText
    Floater
    ImageAlign
    ImageStretch
    Pan
    PasteV
    SelectEllipse
    Selection
    SelectRectangle
    SelectSimilar
    SelectSketch
    ZoomWindow
  End Enum

  Public Enum exifType
    typeByte = 1
    typeAscii = 2
    typeShort = 3
    typeUnsignedLong = 4
    typeUnsignedRational = 5
    typeUndefined = 7
    typeSignedLong = 9
    typeSignedRational = 10
  End Enum


  Class ImageFormat
    Property ID As String ' (format)
    Property Description As String
    Property Ext As String ' ".jpg;.jpeg"
    Property isReadable As Boolean
    Property isWritable As Boolean
    Property isMultiframe As Boolean
    Property MagickFmt As MagickFormat
    Sub New(Id As String, Description As String, Ext As String, isReadable As Boolean, isWritable As Boolean, _
            isMultiframe As Boolean, MagickFmt As MagickFormat)
      Me.ID = Id.ToLower
      Me.Description = Description
      Me.Ext = Ext.ToLower
      Me.isReadable = isReadable
      Me.isWritable = isWritable
      Me.isMultiframe = isMultiframe
      Me.MagickFmt = MagickFmt
    End Sub

    'MagickNET.SupportedFormats

  End Class

  Structure ifdEntry
    Dim tag As Integer
    Dim dataType As UShort
    Dim count As Integer
    Dim Value As UInteger
  End Structure

  Class pictureInfo
    Property FormatID As MagickFormat = MagickFormat.Unknown
    Property FormatDescription As String = "" ' magickFormat
    Property Width As Integer = 0
    Property Height As Integer = 0
    Property fileSize As Integer = 0
    Property colorSpace As ImageMagick.ColorSpace = ImageMagick.ColorSpace.Undefined
    Property colorDepth As Integer = 0
    Property hasPages As Boolean = False
    Property hasAlpha As Boolean = False
    Property Compression As CompressionMethod = CompressionMethod.Undefined
    Property Orientation As Integer = 1
    Property resolutionUnit As Integer = 2
    Property ResolutionX As Integer = 0
    Property ResolutionY As Integer = 0
    Property Aspect As Double = 0.75
    Property ErrMessage As String = ""
    Property isNull As Boolean = True
  End Class

  Public Structure LOGFONT
    Dim lfHeight As Integer
    Dim lfWidth As Integer
    Dim lfEscapement As Integer
    Dim lfOrientation As Integer
    Dim lfWeight As Integer
    Dim lfItalic As Byte
    Dim lfUnderline As Byte
    Dim lfStrikeOut As Byte
    Dim lfCharSet As Byte
    Dim lfOutPrecision As Byte
    Dim lfClipPrecision As Byte
    Dim lfQuality As Byte
    Dim lfPitchAndFamily As Byte
    <VBFixedArray(32)> Dim lfFaceName() As Byte
    Public Sub Initialize()
      ReDim lfFaceName(32)
    End Sub
  End Structure

  Public frmMain As frmMainf
  Public frmExplore As frmExploref

  Public WithEvents colorDialog1 As New ColorDialog
  Public WithEvents folderDialog1 As New FolderBrowserDialog
  Public WithEvents saveDialog1 As New SaveFileDialog
  Public WithEvents openDialog1 As New OpenFileDialog
  Public helpProvider1 As New HelpProvider

  ' parameters between fmconvert and fmcoloradjust
  Public colorAdjust As Boolean  ' flag for fileconvert
  Public clrValue(7) As Integer  ' parameters for fileconvert from frmColorBatchAdjust

  Public iniWindowSizeX As Integer
  Public iniWindowSizeY As Integer
  Public iniWindowLocationX As Integer
  Public iniWindowLocationY As Integer

  Public iniSavePath As String
  Public iniExplorePath As String
  Public iniWebPath As String
  Public loadPath As String ' not a saved parameter
  Public tagMatchPath As String ' not a saved parameter

  Public iniViewStyle As Integer
  Public iniThumbX As Integer
  Public iniThumbY As Integer
  Public iniShadowSize As Integer
  Public iniListTop As Double
  Public iniFolderWidth As Double

  Public iniWebBackColor As System.Drawing.Color
  Public iniWebForeColor As System.Drawing.Color
  Public iniWebnColumns As Integer
  Public iniWebTarget As Integer ' 0 = none, 1 = _blank
  Public iniWebThumbX As Integer
  Public iniWebThumbY As Integer
  Public iniWebShadowSize As Integer
  Public iniWebTableBorder As Integer
  Public iniWebCellPadding As Integer
  Public iniWebCellSpacing As Integer
  Public iniWebTitleSize As Integer
  Public iniWebCaptionSize As Integer
  Public iniWebCaptionAlign As String
  Public iniWebConvertUTCtoLocal As Integer
  Public iniWebFont As String
  Public iniWebResize As Integer
  Public iniWebImageX As Integer
  Public iniWebImageY As Integer
  Public iniWebSaveThumbnail As Integer ' for savehtml
  Public iniWebSaveImage As Integer ' for savehtml
  Public iniWebGoogleAnalytics As String
  Public iniWebGoogleEvents As Boolean

  Public iniDBhost As String
  Public iniDBdatabase As String
  Public iniDBuser As String
  Public iniDBpassword As String
  Public iniDBConnStr As String ' assembled from the others during init

  Public iniSaveResize As Integer
  Public iniSaveXSize As Double
  Public iniSaveYSize As Double
  Public iniSavePct As Double
  Public iniSaveFiletype As String

  Public iniEmailAccount As String
  Public iniEmailPassword As String
  Public iniEmailHost As String
  Public iniEmailPort As String

  Public Mailer As SmtpClient
  Public mailMsg As MailMessage

  Public rotateTrim As Boolean = False

  Public MultiFile As Boolean
  Public forceConverted As Boolean

  Public Const MaxRes As Integer = 20000
  Public Const Maxzoom As Integer = 100000
  Public Const bigMegapix As Double = 20000000

  Public AppName As String
  Public appLongTitle As String
  Public iniFileType As New List(Of String)
  Public urlMain As String
  Public urlRegister As String
  Public urlUpdate As String

  Public UndoPath As String
  Public dataPath As String
  Public exePath As String

  Public pczCellFolder As String ' picturize cell image folder
  Public pczCellRes(2) As Integer ' picturize cell x size
  Public pczCellDiv(2) As Integer ' number of color samples to do in each cell
  Public pczColorAdjust As Boolean
  Public pczNPic(2) As Integer ' number of cells in the resulting image
  Public pczRetc As Integer ' picturize return code
  Public pczCellAspect As Double ' aspect ration of a cell in image folder
  Public pcznCellPics As Integer ' number of pictures in the cell folder
  Public pczUsePicsOnce As Boolean ' whether to use each photo only once

  Public iniJpgQuality As Integer
  Public iniJpgThumb As Boolean
  Public iniJpgExif As Boolean
  Public iniPngCompression As Integer
  Public iniPngCompressionMethod As ImageMagick.CompressionMethod
  Public iniPngCompressionFilter As Integer
  Public iniPngIndexed As Boolean

  Public iniDelRawFiles As Boolean

  Public iniInfoMaker As Boolean
  Public iniInfoJpeg As Boolean
  Public iniInfoXmp As Boolean
  Public iniInfoDump As Boolean

  Public iniColorTolerance As Integer
  Public iniTipNumber As Integer
  Public iniShowTips As Boolean

  Public iniDrawPenWidth As Integer
  Public iniDrawPenStyle As Integer
  Public iniForeColor As System.Drawing.Color
  Public iniBackColor As System.Drawing.Color
  Public iniBorderColor As System.Drawing.Color
  Public iniBorderOuterThickness As Integer
  Public iniBorderInnerThickness As Integer
  Public iniBorderStyle As Integer

  Public mainColor As System.Drawing.Color
  Public mBackColor As System.Drawing.Color

  Public DefaultExt As String
  Public iniMultiUndo As Boolean
  Public iniDisableUndo As Boolean
  Public iniZoomOne As Boolean
  Public iniClassicHelp As Boolean
  Public iniAdjustIndColor As Boolean
  Public iniAdjustIndintensity As Boolean
  Public iniAdjustPreserveIntensity As Boolean

  Public iniPrintFit As Integer
  Public iniPrintWidth As Integer
  Public iniPrintHeight As Integer
  Public iniPrintOrientation As Integer
  Public iniPrintNCopies As Integer
  Public iniPrintHJustify As Integer
  Public iniPrintVJustify As Integer
  Public iniPrintUnits As Double

  Public iniNewXres As Integer
  Public iniNewYres As Integer

  Public iniSendJPGQuality As Integer
  Public iniSendFiletype As String
  Public iniSendResize As Boolean
  Public iniSendXSize As Double
  Public iniSendYSize As Double
  Public iniSendOriginal As Boolean

  Public iniViewToolbar As Boolean

  Public iniTextAngle As Double
  Public iniTextColor As System.Drawing.Color
  Public iniFontName As String
  Public iniFontSize As Double
  Public iniFontBold As Boolean
  Public iniFontitalic As Boolean
  Public iniFontUnderline As Boolean
  Public iniTextMultiline As Boolean
  Public iniTextBackFill As Boolean
  Public iniTextFixedSize As Boolean
  Public iniTextBackColor As System.Drawing.Color

  Public iniSharpen(3) As Integer
  Public iniArtEffect As Integer

  Public iniSlideRate As Integer
  Public iniSlideFadeTime As Integer
  Public iniSlideOrder As String
  Public iniSlideShowName As Boolean
  Public iniSlideShowDescription As Boolean
  Public iniSlideShowPhotoDate As Boolean

  Public OverwriteResponse As String

  Public fmtCommon As New List(Of ImageFormat)
  Public extWindows As New List(Of String)
  Public extMagick As New List(Of String)

  Public mru(20) As String
  Public nmru As Integer
  Public nextMview As Integer = 0 ' counter used to assign unique mview tag

  ' parameter to fmOverwrite
  Public strOverWriteFile As String

  Public iniAskAssociate As Boolean
  Public iniButtonSize As Integer
  Public iniMultiTagPath As Boolean

  ' frmMain tools
  Public iniButton As New Collection ' 80 toolButton ' available tools
  Public iniToolButton(80) As String ' arranges tools in toolbar
  Public nToolButtons As Integer
  Public iniToolbarText As Boolean
  Public iniToolbarPic As Collection

  ' frmExplore tools
  Public iniVButton As Collection ' available tools
  Public iniVToolButton(80) As String ' arranges tools in toolbar
  Public nVToolButtons As Integer

  Public callingForm As Form

  Public abort As Boolean  ' abort a command
  Public abortClosing As Boolean  ' abort closing the program
  Public currentpicPath As String

  Public tagPath As New List(Of String)
  Public oldTagPath As New List(Of String)

  'Public XPtag(4) As Integer ' for comments

  Public Const pi As Double = 3.14159265358979
  Public Const piOver180 As Double = 0.0174532925199433
  Public crlf As String = ChrW(13) & ChrW(10) ' newline
  Public tb As String = ChrW(9) ' tab

  Public iniDateinCommentCommand As Boolean

  Public cursorSelRectangle As Cursor
  Public cursorSketch As Cursor
  Public cursorSelEllipse As Cursor
  Public cursorDropper As Cursor
  ' Public cursorBlank As Cursor
  Public cursorCrop As Cursor
  Public cursorZoom As Cursor
  Public cursorFill As Cursor
  Public cursorDrawLine As Cursor
  Public cursorDrawBox As Cursor
  Public cursorDrawCurve As Cursor
  Public cursorDrawArrow As Cursor
  Public cursorDrawCircle As Cursor
  Public cursorStretchPaste As Cursor
  Public cursorSelectSimilar As Cursor

  Public Guid As String = "{F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4}"

  Public whiteSpace() As Char = {ChrW(0), ChrW(13), ChrW(10), ChrW(9), " "}

  'Public gCodecs As RasterCodecs

  Public customCalCat As String = "Birthdays and Anniversaries" ' default category name
  Public customCalFile As String = "CalendarEvents.dat" ' custom category file name

  Public OptionTab As TabPage ' for the Options form
  'Public pImage As Bitmap ' used as a parameter from forms
  'Public pImage2 As Bitmap ' used as a parameter from forms
  Public qImage As Bitmap

  Public helpFile As String

  Public Clock As Stopwatch
  Public milli(20) As Long ' for debug time tests

  Dim fuzCircle(12) As PointF ' circular path points for fuzzyShadow corners
  Dim fuzType(12) As Byte ' for the circular path points for fuzzyShadow corners

  Public appTag As String ' string for application in registry

  Public UTF8bare As New UTF8Encoding

  Public mViews As Collection ' all the open mudviewers

  ' for picload service
  Dim ServiceBaseAddress As Uri
  Public LoadPicService As ServiceHost

  Public gdiCodecs() As ImageCodecInfo

  ' for undo tiff save
  'Public undoImageCodecInfo As ImageCodecInfo
  'Public undoEncoderParameter As EncoderParameter
  'Public undoEncoderParameters As EncoderParameters

  Public tiffImageCodecInfo As ImageCodecInfo

  Public jpgImageCodecInfo As ImageCodecInfo
  Public jpgEncoderParameter As EncoderParameter
  Public jpgEncoderParameters As EncoderParameters

  Public gifImageCodecInfo As ImageCodecInfo
  Public gifEncoderParameter As EncoderParameter
  Public gifEncoderParameters As EncoderParameters

  Sub main()

    frmMain = New frmMainf
    frmExplore = New frmExploref

    ProgramInit()
    buginit()
    setDefaults()
    getini()

    iniDBConnStr = getConnStr(iniDBhost, iniDBdatabase, iniDBuser, iniDBpassword)

    ' set the initial form sizes before loading
    If iniWindowSizeX = 0 Then
      frmMain.WindowState = FormWindowState.Maximized
    Else
      frmMain.WindowState = FormWindowState.Normal
      frmMain.Size = New Size(iniWindowSizeX, iniWindowSizeY)
      frmMain.Location = New Point(iniWindowLocationX, iniWindowLocationY)
    End If
    If iniWindowSizeX = 0 Then
      frmExplore.WindowState = FormWindowState.Maximized
    Else
      frmExplore.WindowState = FormWindowState.Normal
      frmExplore.Size = New Size(iniWindowSizeX, iniWindowSizeY)
      frmExplore.Location = New Point(iniWindowLocationX, iniWindowLocationY)
    End If

    Application.Run(frmMain)

  End Sub

  Sub ProgramInit()

    Dim i, k As Integer
    Dim s As String

    ' file system objects
    Dim dirInfo As DirectoryInfo

    tagPath = New List(Of String)

    iniFileType = New List(Of String)

    mViews = New Collection

    ' set circular path for fuzzy shadows
    k = 0
    For i = 0 To 360 Step 30
      fuzCircle(k) = New PointF(Sin(i * piOver180), Cos(i * piOver180))
      fuzType(k) = 1
      k = k + 1
    Next i
    fuzType(0) = 0

    AppName = My.Application.Info.Title
    appLongTitle = AppName
    appTag = AppName.Replace(" ", "") & "." & My.Application.Info.Version.Major
    urlMain = "http://xpda.com/photomud"
    urlRegister = "http://xpda.com/photomud/register"
    urlUpdate = "http://xpda.com/photomud/update"

    ' common image formats
    fmtCommon.Add(New ImageFormat(".jpg", "JPEG", ".jpg;.jpeg", True, True, False, MagickFormat.Jpg))
    fmtCommon.Add(New ImageFormat(".png", "Portable Network Graphics", ".png", True, True, False, MagickFormat.Png))
    fmtCommon.Add(New ImageFormat(".cur", "Windows Cursor", ".cur", True, False, False, MagickFormat.Cur))
    fmtCommon.Add(New ImageFormat(".fits", "Flexible Image Transport System", ".fits", True, True, False, MagickFormat.Fits))
    fmtCommon.Add(New ImageFormat(".gif", "GIF", ".gif", True, True, False, MagickFormat.Gif))
    fmtCommon.Add(New ImageFormat(".ico", "Windows Icon", ".ico;.icon", True, False, False, MagickFormat.Ico))
    fmtCommon.Add(New ImageFormat(".mat", "Matlab Image", ".mat", True, False, False, MagickFormat.Mat))
    fmtCommon.Add(New ImageFormat(".pcd", "Photo CD", ".pcd;.pcds", True, True, False, MagickFormat.Pcd))
    fmtCommon.Add(New ImageFormat(".pcx", "PC Paintbrush", ".pcx", True, True, False, MagickFormat.Pcx))
    fmtCommon.Add(New ImageFormat(".pdf", "PDF", ".pdf", True, True, True, MagickFormat.Pdf))
    fmtCommon.Add(New ImageFormat(".pict", "Apple Quickdraw", ".pct", True, True, False, MagickFormat.Pict))
    fmtCommon.Add(New ImageFormat(".psd", "Photo Shop", ".psd", True, True, False, MagickFormat.Psd))
    fmtCommon.Add(New ImageFormat(".svg", "Scalable Vector Graphics", ".svg", True, True, False, MagickFormat.Svg))
    fmtCommon.Add(New ImageFormat(".tga", "Targa", ".tga;.targa", True, True, False, MagickFormat.Tga))
    fmtCommon.Add(New ImageFormat(".tif", "TIFF", ".tif;.tiff", True, True, False, MagickFormat.Tif))
    fmtCommon.Add(New ImageFormat(".raw", "Photo Raw", ".rw2;.cr2;.crw;.nef;.orf;.arw;.mrw;.dcr;.raf", True, False, False, MagickFormat.Raw))
    fmtCommon.Add(New ImageFormat(".wpg", "Word Perfect Graphics", ".wpg", True, False, False, MagickFormat.Wpg))

    exifInit()

    '    s = ".aai;.art;.arw;.avi;.avs;.bmp;.cals;.cr2;.crw;.cur;.cut;.dcr;.dcx;.dib;.djvu;.emf;.epdf;.fax;.fits;.gif;.hdr;" & _
    '       ".hrz;.ico;.icon;.jp2;.jpt;.j2c;.j2k;.jpg;.jpeg;.mat;.miff;.mrw;.mtv;.nef;.orf;.otb;.p7;.palm;.clipboard;.pbm;.pcd;.pcds;" & _
    '       ".pcx;.pdb;.pdf;.pef;.pfa;.pfb;.pfm;.pgm;.picon;.pict;.pix;.png;.pnm;.ppm;.ps;.ps2;.ps3;.psb;.psd;.ptif;" & _
    '       ".pwp;.raf;.rgb;.rgba;.rfg;.rla;.rle;.sct;.sfw;.sgi;.sun;.svg;.tga;.tif;.tiff;.tim;" & _
    '       ".vicar;.viff;.wbmp;.wpg;.xbm;.xcf;.xpm;.xwd;.x3f"
    '    extReadable = s.Split(";").ToList

    '    s = ".aai;.art;.avs;.bmp;.dcx;.dib;.epdf;.fax;.fits;.gif;.hdr;.hrz;.jp2;.jpt;.j2c;.j2k;.jpg;.jpeg;.miff;" & _
    '       ".mtv;.otb;.p7;.palm;.clipboard;.pam;.pbm;.pcd;.pcds;.pcx;.pdb;.pdf;.pfm;.pgm;.picon;.pict;.png;.pnm;.ppm;" & _
    '       ".ps;.ps2;.ps3;.psb;.psd;.ptif;.rgb;.rgba;.rfg;.rla;.rle;.sgi;.sun;.svg;.tga;.tif;.tiff;" & _
    '       ".vicar;.viff;.wbmp;.xbm;.xpm;.xwd"
    '    extWritable = s.Split(";").ToList

    s = ".jpg;.jpeg;.png;.gif;.tif;.tiff;.bmp;.dib"
    extWindows = s.Split(";").ToList

    ' get all the formats supported by ImageMagick, saving the extension ".jpg"
    extMagick = System.Enum.GetNames(GetType(MagickFormat)).ToList
    For i = 0 To extMagick.Count - 1
      extMagick(i) = "." & extMagick(i).ToLower
    Next i

    ' get special cursors
    Try
      cursorSelRectangle = New Cursor("selBox.cur")
    Catch ex As Exception
      cursorSelRectangle = Cursors.Default
    End Try
    Try
      cursorSketch = New Cursor("sketch.cur")
    Catch ex As Exception
      cursorSketch = Cursors.Default
    End Try
    Try
      cursorSelEllipse = New Cursor("selEllipse.cur")
    Catch ex As Exception
      cursorSelEllipse = Cursors.Default
    End Try
    Try
      cursorDropper = New Cursor("dropper.cur")
    Catch ex As Exception
      cursorDropper = Cursors.Default
    End Try
    Try
      cursorCrop = New Cursor("crop.cur")
    Catch ex As Exception
      cursorCrop = Cursors.Default
    End Try
    Try
      cursorZoom = New Cursor("zoomWindow.cur")
    Catch ex As Exception
      cursorZoom = Cursors.Default
    End Try
    Try
      cursorFill = New Cursor("fill.cur")
    Catch ex As Exception
      cursorFill = Cursors.Default
    End Try
    Try
      cursorDrawLine = New Cursor("drawLine.cur")
    Catch ex As Exception
      cursorDrawLine = Cursors.Default
    End Try
    Try
      cursorDrawCircle = New Cursor("drawcircle.cur")
    Catch ex As Exception
      cursorDrawCircle = Cursors.Default
    End Try
    Try
      cursorDrawArrow = New Cursor("drawarrow.cur")
    Catch ex As Exception
      cursorDrawArrow = Cursors.Default
    End Try
    Try
      cursorDrawCurve = New Cursor("drawcurve.cur")
    Catch ex As Exception
      cursorDrawCurve = Cursors.Default
    End Try
    Try
      cursorDrawBox = New Cursor("drawbox.cur")
    Catch ex As Exception
      cursorDrawBox = Cursors.Default
    End Try
    Try
      cursorStretchPaste = New Cursor("stretchPaste.cur")
    Catch ex As Exception
      cursorStretchPaste = Cursors.Default
    End Try
    Try
      cursorSelectSimilar = New Cursor("selectSimilar.cur")
    Catch ex As Exception
      cursorSelectSimilar = Cursors.Default
    End Try

    If Directory.Exists(System.Windows.Forms.Application.StartupPath) Then
      exePath = System.Windows.Forms.Application.StartupPath
      If Right(exePath, 1) <> "\" Then exePath = exePath & "\"
    Else
      exePath = ""
    End If

    initTools()
    resetTools()
    iniButtonSize = 0 ' 0 = 24, 1 = 32 bit icons
    iniToolbarText = False

    UndoPath = Path.GetTempPath
    If Right(UndoPath, 1) <> "\" Then UndoPath = UndoPath & "\"
    UndoPath = UndoPath & AppName & "\"

    ' make undo directory in temp files if possible, otherwise use program files\photomud\tmp
    If Not Directory.Exists(UndoPath) Then
      Try
        dirInfo = Directory.CreateDirectory(UndoPath)
      Catch
        dirInfo = Nothing
      End Try
      If dirInfo Is Nothing Then
        UndoPath = exePath & "tmp\"
        Try
          dirInfo = Directory.CreateDirectory(UndoPath)
        Catch
          UndoPath = ""
        End Try
      End If
    End If

    Try
      dataPath = System.Windows.Forms.Application.UserAppDataPath
      ' dataPath = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData
    Catch
    End Try

    ' dataPath = Application.UserAppDataPath ' user application data
    If dataPath = "" Then dataPath = exePath
    If Right(dataPath, 1) <> "\" Then dataPath = dataPath & "\"

    If Not Directory.Exists(dataPath) Then
      Try
        Directory.CreateDirectory(dataPath)
      Catch
        dataPath = ""
      End Try
    End If

    helpFile = exePath & "photomud.chm"
    If File.Exists(helpFile) Then
      helpProvider1.HelpNamespace = helpFile
    Else
      helpFile = ""
    End If

    nmru = 9
    For i = 1 To nmru
      mru(i) = ""
    Next i

    'XPtag(0) = &H9C9C ' XPComment
    'XPtag(1) = &H9C9E ' XPKeywords
    'XPtag(2) = &H9C9B ' XPTitle
    'XPtag(3) = &H9C9F ' XPSubject
    'XPtag(4) = &H9C9D ' XPAuthor

    ' get codec information, used in gdi saves.
    gdiCodecs = ImageCodecInfo.GetImageEncoders()

    ' undo encoder
    For Each codec As ImageCodecInfo In gdiCodecs
      'If codec.MimeType = "image/tiff" Then
      'undoImageCodecInfo = codec
      'End If
      If codec.MimeType = "image/tiff" Then
        tiffImageCodecInfo = codec
      End If
      If codec.MimeType = "image/jpeg" Then
        jpgImageCodecInfo = codec
      End If
      If codec.MimeType = "image/gif" Then
        gifImageCodecInfo = codec
      End If
    Next codec

    ' encoder parameters supported: 
    '    image/jpeg: Transformation, Quality, LuminanceTable, ChrominanceTable.
    '    image/tiff: Compression, ColorDepth, SaveFlag.

    'undoEncoderParameters = New EncoderParameters(1)
    'undoEncoderParameter = New EncoderParameter(Imaging.Encoder.Compression, Fix(EncoderValue.CompressionNone))
    'undoEncoderParameters.Param(0) = undoEncoderParameter

    jpgEncoderParameters = New EncoderParameters(1)
    jpgEncoderParameter = New EncoderParameter(Imaging.Encoder.Quality, CByte(97)) ' high quality
    jpgEncoderParameters.Param(0) = jpgEncoderParameter

    ' start picload service
    ServiceBaseAddress = New Uri("net.pipe://localhost/PhotoMudPicLoad")
    LoadPicService = New ServiceHost(GetType(PicLoadService), ServiceBaseAddress)

    Try
      LoadPicService.AddServiceEndpoint(GetType(IPhotoMudPicLoad), New NetNamedPipeBinding, ServiceBaseAddress)
      LoadPicService.Open()
    Catch ex As Exception
      'MsgBox("Load Service Error: " & ex.Message)
      LoadPicService.Abort()
    End Try

  End Sub

  Sub programClose()
    saveini()
  End Sub

  Sub setDefaults()

    Dim i As Integer
    Dim separator() As Char = {" "} ' for raw file ext

    Try
      iniExplorePath = My.Computer.FileSystem.SpecialDirectories.MyPictures
      If iniExplorePath = "" Then iniExplorePath = My.Computer.FileSystem.SpecialDirectories.MyDocuments
    Catch
    End Try

    iniWindowSizeX = 0
    iniWindowSizeY = 0

    loadPath = iniExplorePath
    iniSavePath = iniExplorePath
    iniViewStyle = 0 ' thumbnails
    iniJpgQuality = 97
    iniPngIndexed = False
    iniPngCompression = 7
    iniDelRawFiles = False

    iniViewToolbar = True
    iniThumbX = 160
    iniThumbY = 160
    iniShadowSize = 6
    iniFolderWidth = 0.2
    iniListTop = 0.75

    iniInfoMaker = True
    iniInfoJpeg = False
    iniInfoXmp = False
    iniInfoDump = False

    iniTipNumber = 1
    iniShowTips = True
    iniColorTolerance = 5
    iniDrawPenWidth = 6
    iniDrawPenStyle = DashStyle.Solid
    iniForeColor = System.Drawing.Color.FromArgb(255, 0, 0, 128)
    iniBackColor = System.Drawing.Color.FromArgb(255, 128, 128, 128)
    iniBorderColor = System.Drawing.Color.FromArgb(255, 40, 90, 120)
    iniBorderOuterThickness = 30
    iniBorderInnerThickness = 20
    iniBorderStyle = 2 ' 0 = 2D, 1 = 3D, 2 = Bevel, 3 = Button

    ' these are set by frmPageSetup
    iniPrintFit = 1
    iniPrintWidth = 7.5
    iniPrintHeight = 10
    iniPrintOrientation = 1 ' landscape
    iniPrintNCopies = 1
    iniPrintHJustify = 0
    iniPrintVJustify = 0
    iniPrintUnits = 1 ' inches

    DefaultExt = ".jpg"
    iniMultiUndo = True
    iniDisableUndo = False
    iniZoomOne = False
    iniClassicHelp = False
    iniAdjustIndColor = True
    iniAdjustIndintensity = False
    iniAdjustPreserveIntensity = True

    iniNewXres = 2000
    iniNewYres = 1500

    iniTextAngle = 0
    iniTextColor = iniForeColor
    iniTextBackColor = iniBackColor
    iniFontName = "Arial"
    iniFontSize = 36
    iniFontBold = False
    iniFontitalic = False
    iniFontUnderline = False
    iniTextMultiline = True
    iniTextBackFill = False
    iniTextFixedSize = False

    iniSharpen(0) = 70   ' sharpen amount
    iniSharpen(1) = 3     ' radius
    iniSharpen(2) = 50       ' threshold
    iniSharpen(3) = 1      ' 0 for sharpen (only uses first 2 parameters), 1 for unsharp mask
    iniArtEffect = 0

    pczCellRes(0) = 80
    pczCellRes(1) = 60
    pczNPic(0) = 50
    pczNPic(1) = 50
    pczCellDiv(0) = 3
    pczCellDiv(1) = 2
    pczColorAdjust = False

    iniDBhost = "localhost"
    iniDBdatabase = "taxa"
    iniDBuser = "bugs"
    iniDBpassword = "password"

    iniSaveResize = False
    iniSaveXSize = 2000
    iniSaveYSize = 2000
    iniSavePct = 100
    iniSaveFiletype = ".jpg"

    iniSendJPGQuality = 95
    iniSendFiletype = ".jpg"
    iniSendResize = True
    iniSendXSize = 800
    iniSendYSize = 600
    iniSendOriginal = True

    iniSlideRate = 1 ' sixty per minute
    iniSlideFadeTime = 30 ' milliseconds
    iniSlideShowDescription = True
    iniSlideShowPhotoDate = False
    iniSlideShowName = True
    iniSlideOrder = "filename"

    iniDateinCommentCommand = False

    iniFileType = New List(Of String)

    For Each fmt As ImageFormat In fmtCommon
      For Each s As String In fmtCommon(i).Ext.Split(";")
        iniFileType.Add(s)
      Next s
    Next fmt

    If iniFileType.Count = 0 Then
      iniFileType.Add(".jpg")
      iniFileType.Add(".jpeg")
    End If

    iniEmailAccount = "name@gmail.com"
    iniEmailPassword = ""
    iniEmailHost = "smtp.gmail.com"
    iniEmailPort = "465"

    SetWebDefaults()

  End Sub

  Public Sub getini()

    ' load settings from ini file.

    Dim j, i, k As Integer
    Dim n As Integer
    Dim sKey As String
    Dim sValue As String
    Dim ss() As String
    Dim iLine As Integer

    Dim b As ToolStripItem

    If Not File.Exists(dataPath & AppName & ".ini") Then Exit Sub

    Try
      ss = File.ReadAllLines(dataPath & AppName & ".ini")
    Catch
      ReDim ss(-1)
    End Try

    For iLine = 0 To UBound(ss)
      i = InStr(ss(iLine), "=")
      If i > 0 And i < Len(ss(iLine)) Then
        sKey = Left(ss(iLine), i - 1)
        sValue = Trim(Mid(ss(iLine), i + 1))

        Select Case Trim(LCase(sKey))

          Case "jpgquality"
            If IsNumeric(sValue) Then iniJpgQuality = CInt(sValue)
            If iniJpgQuality < 1 Or iniJpgQuality > 100 Then iniJpgQuality = 97
          Case "delrawfiles"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniDelRawFiles = CBool(sValue)

          Case "infomaker"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniInfoMaker = CBool(sValue)
          Case "infojpeg"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniInfoJpeg = CBool(sValue)
          Case "infoxmp"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniInfoXmp = CBool(sValue)
          Case "infodump"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniInfoDump = CBool(sValue)
          Case "viewtoolbar"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then If IsNumeric(sValue) Then iniViewToolbar = CBool(sValue)
          Case "colortolerance"
            If IsNumeric(sValue) Then iniColorTolerance = CInt(sValue)
          Case "tipnumber"
            If IsNumeric(sValue) Then iniTipNumber = CInt(sValue)
          Case "showtips"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniShowTips = CBool(sValue)
          Case "penwidth"
            If IsNumeric(sValue) Then iniDrawPenWidth = CInt(sValue)
          Case "penstyle"
            If IsNumeric(sValue) Then iniDrawPenStyle = CInt(sValue)
          Case "forecolor"
            If IsNumeric(sValue) Then iniForeColor = Color.FromArgb(CInt(sValue))
          Case "backcolor"
            If IsNumeric(sValue) Then iniBackColor = Color.FromArgb(CInt(sValue))
          Case "bordercolor"
            If IsNumeric(sValue) Then iniBorderColor = Color.FromArgb(CInt(sValue))
          Case "borderouterthickness"
            If IsNumeric(sValue) Then iniBorderOuterThickness = CInt(sValue)
          Case "borderinnerthickness"
            If IsNumeric(sValue) Then iniBorderInnerThickness = CInt(sValue)
          Case "borderstyle"
            If IsNumeric(sValue) Then iniBorderStyle = CInt(sValue)

          Case "sliderate"
            If IsNumeric(sValue) Then iniSlideRate = CDbl(sValue)
          Case "slidefadetime"
            If IsNumeric(sValue) Then iniSlideFadeTime = CInt(sValue)
          Case "slideshowname"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniSlideShowName = CBool(sValue)
          Case "slideshowdescription"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniSlideShowDescription = CBool(sValue)
          Case "slideshowphotodate"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniSlideShowPhotoDate = CBool(sValue)
          Case "slideorder"
            iniSlideOrder = LCase(sValue)
          Case "dateincommentcommand"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniDateinCommentCommand = CBool(sValue)

          Case "printfit"
            If IsNumeric(sValue) Then iniPrintFit = CInt(sValue)
          Case "printwidth"
            If IsNumeric(sValue) Then iniPrintWidth = CInt(sValue)
          Case "printheight"
            If IsNumeric(sValue) Then iniPrintHeight = CInt(sValue)
          Case "printorientation"
            If IsNumeric(sValue) Then iniPrintOrientation = CInt(sValue)
          Case "printncopies"
            If IsNumeric(sValue) Then iniPrintNCopies = CInt(sValue)
          Case "printhjustify"
            If IsNumeric(sValue) Then iniPrintHJustify = CInt(sValue)
          Case "printvjustify"
            If IsNumeric(sValue) Then iniPrintVJustify = CInt(sValue)
          Case "printunits"
            If IsNumeric(sValue) Then iniPrintUnits = CDbl(sValue)

          Case "defaultext"
            DefaultExt = sValue
          Case "multiundo"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniMultiUndo = CBool(sValue)
          Case "disableundo"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniDisableUndo = CBool(sValue)
          Case "zoomone"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniZoomOne = CBool(sValue)
          Case "classichelp"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniClassicHelp = CBool(sValue)
          Case "adjustindcolor"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniAdjustIndColor = CBool(sValue)
          Case "adjustindintensity"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniAdjustIndintensity = CBool(sValue)
          Case "adjustpreserveintensity"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniAdjustPreserveIntensity = CBool(sValue)

          Case "sendjpgquality"
            If IsNumeric(sValue) Then iniSendJPGQuality = CInt(sValue)
          Case "sendfiletype"
            iniSendFiletype = sValue
          Case "sendresize"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniSendResize = CBool(sValue)
          Case "sendxsize"
            If IsNumeric(sValue) Then iniSendXSize = CSng(sValue)
          Case "sendysize"
            If IsNumeric(sValue) Then iniSendYSize = CSng(sValue)
          Case "sendoriginal"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniSendOriginal = CBool(sValue)

          Case "filetypes"
            If IsNumeric(sValue) Then n = CInt(sValue)
            If n > 0 Then
              iniFileType = New List(Of String)
              If iLine + n <= UBound(ss) Then
                For i = 1 To n
                  iniFileType.Add(ss(iLine + i).Trim.ToLower)
                Next i
                iLine = iLine + n
              End If
            End If
          Case "savepath"
            iniSavePath = sValue
          Case "explorepath"
            iniExplorePath = sValue
            loadPath = iniExplorePath
          Case "webpath"
            iniWebPath = sValue
          Case "windowsizex"
            If IsNumeric(sValue) Then iniWindowSizeX = CInt(sValue)
          Case "windowsizey"
            If IsNumeric(sValue) Then iniWindowSizeY = CInt(sValue)
          Case "windowlocationx"
            If IsNumeric(sValue) Then iniWindowLocationX = CInt(sValue)
          Case "windowlocationy"
            If IsNumeric(sValue) Then iniWindowLocationY = CInt(sValue)
          Case "viewstyle"
            If IsNumeric(sValue) Then iniViewStyle = CInt(sValue)
            If iniViewStyle < 0 Or iniViewStyle > 2 Then iniViewStyle = 0
          Case "thumbx"
            If IsNumeric(sValue) Then iniThumbX = CInt(sValue)
          Case "thumby"
            If IsNumeric(sValue) Then iniThumbY = CInt(sValue)
          Case "shadowsize"
            If IsNumeric(sValue) Then iniShadowSize = CInt(sValue)
          Case "folderwidth"
            If IsNumeric(sValue) Then iniFolderWidth = CDbl(sValue)
          Case "listtop"
            If IsNumeric(sValue) Then iniListTop = CDbl(sValue)

          Case "webconvertutctolocal"
            If IsNumeric(sValue) Then iniWebConvertUTCtoLocal = CInt(sValue)
          Case "webbackcolor"
            If IsNumeric(sValue) Then iniWebBackColor = System.Drawing.Color.FromArgb(CInt(sValue))
          Case "webforecolor"
            If IsNumeric(sValue) Then iniWebForeColor = System.Drawing.Color.FromArgb(CInt(sValue))
          Case "webncolumns"
            If IsNumeric(sValue) Then iniWebnColumns = CInt(sValue)
          Case "webtarget"
            If IsNumeric(sValue) Then iniWebTarget = CInt(sValue)
          Case "webthumbx"
            If IsNumeric(sValue) Then iniWebThumbX = CInt(sValue)
          Case "webthumby"
            If IsNumeric(sValue) Then iniWebThumbY = CInt(sValue)
          Case "webshadowsize"
            If IsNumeric(sValue) Then iniWebShadowSize = CInt(sValue)
          Case "webtableborder"
            If IsNumeric(sValue) Then iniWebTableBorder = CInt(sValue)
          Case "webcellpadding"
            If IsNumeric(sValue) Then iniWebCellPadding = CInt(sValue)
          Case "webcellspacing"
            If IsNumeric(sValue) Then iniWebCellSpacing = CInt(sValue)
          Case "webtitlesize"
            If IsNumeric(sValue) Then iniWebTitleSize = CInt(sValue)
          Case "webcaptionsize"
            If IsNumeric(sValue) Then iniWebCaptionSize = CInt(sValue)
          Case "webcaptionalign"
            iniWebCaptionAlign = sValue
          Case "webfont"
            iniWebFont = sValue
          Case "webresize"
            If IsNumeric(sValue) Then iniWebResize = CInt(sValue)
          Case "webimagex"
            If IsNumeric(sValue) Then iniWebImageX = CInt(sValue)
          Case "webimagey"
            If IsNumeric(sValue) Then iniWebImageY = CInt(sValue)
          Case "saveimage"
            If IsNumeric(sValue) Then iniWebSaveImage = CInt(sValue)
          Case "webgoogleanalytics"
            iniWebGoogleAnalytics = sValue
          Case "webgoogleevents"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniWebGoogleEvents = CBool(sValue)

          Case "arteffect"
            If IsNumeric(sValue) Then iniArtEffect = CInt(sValue)

          Case "textangle"
            If IsNumeric(sValue) Then iniTextAngle = CDbl(sValue)
          Case "textcolor"
            If IsNumeric(sValue) Then iniTextColor = System.Drawing.Color.FromArgb(CInt(sValue))
          Case "textbackcolor"
            If IsNumeric(sValue) Then iniTextBackColor = System.Drawing.Color.FromArgb(CInt(sValue))
          Case "textfixedsize"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniTextFixedSize = CBool(sValue)
          Case "textmultiline"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniTextMultiline = CBool(sValue)
          Case "textbackfill"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniTextBackFill = CBool(sValue)

          Case "fontname"
            iniFontName = sValue
          Case "fontsize"
            If IsNumeric(sValue) Then iniFontSize = CInt(sValue)
          Case "fontbold"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniFontBold = CBool(sValue)
          Case "fontitalic"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniFontitalic = CBool(sValue)
          Case "fontunderline"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniFontUnderline = CBool(sValue)

          Case "dbhost"
            iniDBhost = sValue
          Case "dbdatabase"
            iniDBdatabase = sValue
          Case "dbuser"
            iniDBuser = sValue
          Case "dbpassword"
            iniDBpassword = Encoding.UTF8.GetString(Convert.FromBase64String(sValue))

          Case "emailaccount"
            iniEmailAccount = sValue
          Case "emailpassword"
            iniEmailPassword = sValue
          Case "emailhost"
            iniEmailHost = sValue
          Case "emailport"
            iniEmailPort = sValue

          Case "saveresize"
            If IsNumeric(sValue) Then iniSaveResize = CInt(sValue)
          Case "savexsize"
            If IsNumeric(sValue) Then iniSaveXSize = CDbl(sValue)
          Case "saveysize"
            If IsNumeric(sValue) Then iniSaveYSize = CDbl(sValue)
          Case "savepct"
            If IsNumeric(sValue) Then iniSavePct = CDbl(sValue)
          Case "savefiletype"
            iniSaveFiletype = sValue

          Case "nmru"
            If IsNumeric(sValue) Then nmru = CInt(sValue)
            If iLine + nmru <= UBound(ss) Then
              For i = 1 To nmru
                mru(i) = ss(iLine + i)
              Next i
              iLine = iLine + nmru
            End If

          Case "askassociate"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniAskAssociate = CBool(sValue)

          Case "multitagpath"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniMultiTagPath = CBool(sValue)

          Case "toolsize"
            If IsNumeric(sValue) Then iniButtonSize = CInt(sValue)

          Case "toolbartext"
            If eqstr(sValue, "true") Or eqstr(sValue, "false") Then iniToolbarText = CBool(sValue)

          Case "ntoolbuttons"
            If IsNumeric(sValue) Then nToolButtons = CInt(sValue)
            If nToolButtons + iLine <= UBound(ss) Then
              For i = 1 To nToolButtons
                iniToolButton(i) = ss(i + iLine)
              Next i
              iLine = iLine + nToolButtons
            End If

          Case "nvtoolbuttons"
            If IsNumeric(sValue) Then nVToolButtons = CInt(sValue)
            If nVToolButtons + iLine <= UBound(ss) Then
              For i = 1 To nVToolButtons
                iniVToolButton(i) = ss(i + iLine)
              Next i
              iLine = iLine + nVToolButtons
            End If

            For i = nVToolButtons To 1 Step -1
              k = 0
              For Each b In iniVButton
                If eqstr(b.Tag, iniVToolButton(i)) Then
                  k = 1
                  Exit For
                End If
              Next b
              If k = 0 Then ' remove button - invalid
                nVToolButtons = nVToolButtons - 1
                For j = i To nVToolButtons
                  iniVToolButton(j) = iniVToolButton(j + 1)
                Next j
              End If
            Next i

          Case "bugpath"
            iniBugPath = sValue

          Case "bugpixelspermm"
            If IsNumeric(sValue) Then iniBugPixelsPerMM = Val(sValue)

        End Select

      End If
    Next iLine

  End Sub
  Public Sub saveini()

    Dim i As Integer
    Dim sf As New List(Of String)
    Dim s As String

    sf.Add("savepath=" & iniSavePath)
    s = Path.GetTempPath
    If Right(s, 1) <> "\" And Right(iniExplorePath, 1) = "\" Then s = s & "\"
    If Right(s, 1) = "\" And Right(iniExplorePath, 1) <> "\" Then s = Left(s, Len(s) - 1)
    If String.Compare(s, iniExplorePath, True) <> 0 Then ' skip if it's on the temp folder, from email reading
      sf.Add("explorepath=" & iniExplorePath)
    End If
    sf.Add("webpath=" & iniWebPath)
    sf.Add("jpgquality=" & iniJpgQuality)
    sf.Add("delrawfiles=" & iniDelRawFiles)

    sf.Add("infomaker=" & iniInfoMaker)
    sf.Add("infojpeg=" & iniInfoJpeg)
    sf.Add("infoxmp=" & iniInfoXmp)
    sf.Add("infodump=" & iniInfoDump)
    sf.Add("viewtoolbar=" & iniViewToolbar)
    sf.Add("colortolerance=" & iniColorTolerance)
    sf.Add("tipnumber=" & iniTipNumber)
    sf.Add("showtips=" & iniShowTips)

    sf.Add("printfit=" & iniPrintFit)
    sf.Add("printwidth=" & iniPrintWidth)
    sf.Add("printheight=" & iniPrintHeight)
    sf.Add("printorientation=" & iniPrintOrientation)
    sf.Add("printncopies=" & iniPrintNCopies)
    sf.Add("printhjustify=" & iniPrintHJustify)
    sf.Add("printvjustify=" & iniPrintVJustify)
    sf.Add("printunits=" & iniPrintUnits)

    sf.Add("penwidth=" & iniDrawPenWidth)
    sf.Add("penstyle=" & iniDrawPenStyle)
    sf.Add("forecolor=" & iniForeColor.ToArgb)
    sf.Add("backcolor=" & iniBackColor.ToArgb)
    sf.Add("bordercolor=" & iniBorderColor.ToArgb)
    sf.Add("borderinnerthickness=" & iniBorderInnerThickness)
    sf.Add("borderouterthickness=" & iniBorderOuterThickness)
    sf.Add("borderstyle=" & iniBorderStyle)

    sf.Add("sliderate=" & iniSlideRate)
    sf.Add("slidefadetime=" & iniSlideFadeTime)
    sf.Add("slideorder=" & iniSlideOrder)
    sf.Add("slideshowdescription=" & iniSlideShowDescription)
    sf.Add("slideshowphotodate=" & iniSlideShowPhotoDate)
    sf.Add("slideshowname=" & iniSlideShowName)
    sf.Add("dateincommentcommand=" & iniDateinCommentCommand)

    sf.Add("defaultext=" & DefaultExt)
    sf.Add("multiundo=" & iniMultiUndo)
    sf.Add("disableundo=" & iniDisableUndo)
    sf.Add("zoomone=" & iniZoomOne)
    sf.Add("classichelp=" & iniClassicHelp)
    sf.Add("adjustindcolor=" & iniAdjustIndColor)
    sf.Add("adjustindintensity=" & iniAdjustIndintensity)
    sf.Add("adjustpreserveintensity=" & iniAdjustPreserveIntensity)

    sf.Add("sendjpgquality=" & iniSendJPGQuality)
    sf.Add("sendfiletype=" & iniSendFiletype)
    sf.Add("sendresize=" & iniSendResize)
    sf.Add("sendxsize=" & iniSendXSize)
    sf.Add("sendysize=" & iniSendYSize)
    sf.Add("sendoriginal=" & iniSendOriginal)

    '======= imagev

    sf.Add("filetypes=" & iniFileType.Count)
    For Each s In iniFileType
      sf.Add(s)
    Next s

    sf.Add("windowsizex=" & iniWindowSizeX)
    sf.Add("windowsizey=" & iniWindowSizeY)
    sf.Add("windowlocationx=" & iniWindowLocationX)
    sf.Add("windowlocationy=" & iniWindowLocationY)
    sf.Add("viewstyle=" & iniViewStyle)
    sf.Add("thumbx=" & iniThumbX)
    sf.Add("thumby=" & iniThumbY)
    sf.Add("shadowsize=" & iniShadowSize)
    sf.Add("folderwidth=" & iniFolderWidth)  ' frmExplore.shtree1
    sf.Add("listtop=" & iniListTop)  ' frmExplore.shlist1

    sf.Add("webconvertutctolocal=" & iniWebConvertUTCtoLocal)
    sf.Add("webbackcolor=" & iniWebBackColor.ToArgb)
    sf.Add("webforecolor=" & iniWebForeColor.ToArgb)
    sf.Add("webncolumns=" & iniWebnColumns)
    sf.Add("webtarget=" & iniWebTarget)
    sf.Add("webthumbx=" & iniWebThumbX)
    sf.Add("webthumby=" & iniWebThumbY)
    sf.Add("webshadowsize=" & iniWebShadowSize)
    sf.Add("webtableborder=" & iniWebTableBorder)
    sf.Add("webcellpadding=" & iniWebCellPadding)
    sf.Add("webcellspacing=" & iniWebCellSpacing)
    sf.Add("webtitlesize=" & iniWebTitleSize)
    sf.Add("webcaptionsize=" & iniWebCaptionSize)
    sf.Add("webcaptionalign=" & iniWebCaptionAlign)
    sf.Add("webfont=" & iniWebFont)
    sf.Add("webresize=" & iniWebResize)
    sf.Add("webimagex=" & iniWebImageX)
    sf.Add("webimagey=" & iniWebImageY)
    sf.Add("websaveimage=" & iniWebSaveImage)
    sf.Add("webgoogleanalytics=" & iniWebGoogleAnalytics)
    sf.Add("webgoogleevents=" & iniWebGoogleEvents)
    sf.Add("websavethumbnail=" & iniWebSaveThumbnail)

    sf.Add("arteffect=" & iniArtEffect)

    sf.Add("textangle=" & iniTextAngle)
    sf.Add("textcolor=" & iniTextColor.ToArgb)
    sf.Add("textbackcolor=" & iniTextBackColor.ToArgb)
    sf.Add("textmultiline=" & iniTextMultiline)
    sf.Add("textbackfill=" & iniTextBackFill)
    sf.Add("textfixedsize=" & iniTextFixedSize)

    sf.Add("fontname=" & iniFontName)
    sf.Add("fontsize=" & iniFontSize)
    sf.Add("fontbold=" & iniFontBold)
    sf.Add("fontitalic=" & iniFontitalic)
    sf.Add("fontunderline=" & iniFontUnderline)

    sf.Add("dbhost=" & iniDBhost)
    sf.Add("dbdatabase=" & iniDBdatabase)
    sf.Add("dbuser=" & iniDBuser)

    sf.Add("dbpassword=" & Convert.ToBase64String(Encoding.UTF8.GetBytes(iniDBpassword))) ' encode password

    sf.Add("emailaccount=" & iniEmailAccount)
    sf.Add("emailpassword=" & iniEmailPassword)
    sf.Add("emailhost=" & iniEmailHost)
    sf.Add("emailport=" & iniEmailPort)

    sf.Add("saveresize=" & iniSaveResize)
    sf.Add("savexsize=" & iniSaveXSize)
    sf.Add("saveysize=" & iniSaveYSize)
    sf.Add("savepct=" & iniSavePct)
    sf.Add("savefiletype=" & iniSaveFiletype)

    sf.Add("nmru=" & nmru)
    For i = 1 To nmru
      sf.Add(mru(i))
    Next

    sf.Add("askassociate=" & iniAskAssociate)
    sf.Add("multitagpath=" & iniMultiTagPath)
    sf.Add("toolsize=" & iniButtonSize)
    sf.Add("toolbartext=" & iniToolbarText)
    sf.Add("nToolButtons=" & nToolButtons)
    For i = 1 To nToolButtons
      sf.Add(iniToolButton(i))
    Next i

    sf.Add("nVToolButtons=" & nVToolButtons)
    For i = 1 To nVToolButtons
      sf.Add(iniVToolButton(i))
    Next i

    sf.Add("bugpath=" & iniBugPath)
    sf.Add("bugpixelspermm=" & iniBugPixelsPerMM)

    Try
      File.WriteAllLines(dataPath & AppName & ".ini", sf.ToArray, UTF8bare)
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

  End Sub

  Function askOverwrite(ByVal fName As String, ByVal multi As Boolean, ByRef checkOverwrite As String) As Integer

    ' checkOverwrite (might change here)
    ' "all" - overwrite
    ' "none" - don't overwrite
    ' "" - ask overwrite

    ' returns: 
    ' 1 - write the file
    ' 0 - don't write the file
    ' -1 - cancel

    If checkOverwrite = "all" OrElse Not File.Exists(fName) Then Return 1 ' write the file
    If checkOverwrite = "none" Then Return 0 ' file exists, don't overwrite

    frmOverwrite.chkAll.Visible = multi
    strOverWriteFile = fName ' strOverWriteFile is used by frmOverwrite
    Using frm As Form = New frmOverwrite
      frm.ShowDialog()
    End Using

    Select Case OverwriteResponse ' overwriteresponse is from fmOverwrite
      Case "yes"
        Return 1
      Case "no"
        Return 0
      Case "cancel"
        Return -1
      Case "all"
        checkOverwrite = "all" ' overwrite all files
        Return 1
      Case "none"
        checkOverwrite = "none" ' overwrite no files
        Return 0
      Case Else
        Return 0
    End Select

  End Function

  Function loadNew(ByRef fName As String) As mudViewer
    ' load a file into a new tab page

    Dim ext As String = ""
    Dim s As String
    Dim msg As String = ""
    Dim picInfo As pictureInfo
    Dim rview As mudViewer
    Dim nFrames As Integer
    Dim fd As FrameDimension
    Dim bmpFrames As List(Of Bitmap)

    If Not File.Exists(fName) Then
      DelMruFile(fName)
      Return Nothing
    End If

    rview = New mudViewer
    rview.Caption = Path.GetFileName(fName)

    picInfo = getPicinfo(fName, True)

    frmMain.Cursor = Cursors.WaitCursor
    frmMain.Refresh()

    If Not picInfo.hasPages Then
      Using bmp As Bitmap = readBitmap(fName, msg)
        If bmp Is Nothing Then
          MsgBox("Could not load " & fName & crlf & msg, MsgBoxStyle.OkOnly)
          rview.Close()
          Return Nothing
        End If

        rview.setBitmap(bmp)
        fd = New FrameDimension(bmp.FrameDimensionsList(0))
        nFrames = bmp.GetFrameCount(fd)
        If nFrames > 1 Then rview.pageBmp = splitFrames(fd, nFrames, fName) ' should not happen?
      End Using
    Else
      bmpFrames = readMultiframeBitmap(fName, msg)
      If bmpFrames Is Nothing OrElse bmpFrames.Count = 0 Then
        MsgBox("Could not load " & fName & crlf & msg, MsgBoxStyle.OkOnly)
        rview.Close()
        Return Nothing
      End If
      rview.setBitmap(bmpFrames(0))
      rview.pageBmp = bmpFrames
    End If

    If Not picInfo.isNull Then rview.originalFormat = picInfo.FormatID
    rview.originalFileName = fName

    rview.picName = fName
    rview.picPage = ""

    frmMain.redraw(rview, 3, False)
    If rview.pComments.Count = 0 Then rview.pComments = readPropertyItems(fName)
    AddMruFile(fName)

    s = Path.GetDirectoryName(fName)
    If String.Compare(Right(s, 4), "temp", True) <> 0 Then loadPath = s ' don't set temp to default path

    frmMain.setupPanelPage(rview)
    rview.picModified = False

    frmMain.Cursor = Cursors.Default
    Return rview

  End Function

  Function splitFrames(fd As FrameDimension, nFrames As Integer, fName As String) As List(Of Bitmap)
    ' copy the frames in bmp into the bitmap array fBmp
    ' read the bitmap again because stream read doesn't work with multipage

    Dim fBmp As New List(Of Bitmap)
    Dim k As Integer

    If nFrames > 1 Then
      Using xBmp As Bitmap = Bitmap.FromFile(fName)
        For i As Integer = 0 To nFrames - 1
          k = xBmp.SelectActiveFrame(fd, i)
          fBmp.Add(New Bitmap(xBmp.Width, xBmp.Height, PixelFormat.Format32bppPArgb))
          Using g As Graphics = Graphics.FromImage(fBmp(fBmp.Count - 1))
            g.DrawImage(xBmp, New Rectangle(0, 0, xBmp.Width, xBmp.Height))
          End Using
        Next i
      End Using
    End If

    Return fBmp

  End Function

  Function readThumbnail(width As Integer, height As Integer, fName As String, ByRef msg As String, _
    thumbStampOnly As Boolean, Optional Quality As InterpolationMode = InterpolationMode.Low) As Bitmap

    ' set width and height to zero to use original thumbstamp size.
    Dim img2 As Bitmap = Nothing
    Dim thumb As Bitmap = Nothing
    Dim aspect As Double
    Dim w, h As Integer
    Dim ext As String
    Dim picInfo As pictureInfo

    Try
      ext = LCase(Path.GetExtension(fName))

      Using iStream As New FileStream(fName, FileMode.Open, FileAccess.Read)
        If (ext = ".jpg" Or ext = ".jpeg" Or ext = ".tif" Or ext = ".tiff" Or ext = ".gif" Or ext = ".png") Then ' windows read
          Using bmp As Bitmap = Image.FromStream(iStream, True, False)
            For Each item As Imaging.PropertyItem In bmp.PropertyItems
              If item.Id = 20507 Then ' thumbnail code in exif
                img2 = Image.FromStream(New MemoryStream(item.Value))
                Exit For
              End If
            Next item
          End Using
        End If
        If img2 Is Nothing And thumbStampOnly Then Return Nothing

        If Not thumbStampOnly OrElse img2 Is Nothing Then
          Select Case ext
            Case ".jpg", ".jpeg", ".tif", ".tiff", ".gif", ".png" ' windows read
              img2 = New Bitmap(iStream)
            Case ".pdf" ' too slow, use blank
              img2 = New Bitmap(width, height, PixelFormat.Format32bppPArgb)
              Using g As Graphics = Graphics.FromImage(img2)
                g.Clear(Color.White)
                g.DrawRectangle(New Pen(Color.Black), 0, 0, width - 1, height - 1)
              End Using
              Return img2
            Case Else
              img2 = readBitmap(fName, msg, 72) ' read the entire bitmap in other cases.
          End Select

          If img2 Is Nothing Then
            msg = "Could not read " & fName
            Return Nothing
          End If
        End If

        picInfo = getPicinfo(fName, True, 1)
        If picInfo IsNot Nothing Then
          Select Case picInfo.Orientation
            Case ImageMagick.OrientationType.BottomRight
              img2.RotateFlip(RotateFlipType.Rotate180FlipNone)
            Case ImageMagick.OrientationType.RightTop
              img2.RotateFlip(RotateFlipType.Rotate90FlipNone)
            Case ImageMagick.OrientationType.LeftBotom
              img2.RotateFlip(RotateFlipType.Rotate270FlipNone)
          End Select
        End If


        w = img2.Width : h = img2.Height
        If h <> 0 And height <> 0 And height <> 0 And width <> 0 Then
          aspect = h / w
          If aspect > height / width Then
            h = height
            w = width / aspect
          Else
            h = height * aspect
            w = width
          End If
        End If

        thumb = New Bitmap(w, h)
        Using g As Graphics = Graphics.FromImage(thumb)
          g.InterpolationMode = Quality
          g.DrawImage(img2, New Rectangle(0, 0, w, h))
        End Using

        clearBitmap(img2)
        Return thumb
      End Using

    Catch ex As Exception
      msg = ex.Message
      clearBitmap(thumb)
      clearBitmap(img2)
      Return Nothing
    End Try

  End Function

  Function readBitmap(ByRef fName As String, ByRef msg As String, Optional pdfDensity As Integer = 300) As Bitmap

    ' error: returns nothing. Msg contains error message or, if multipage, the number of pages

    Dim ext As String
    Dim rs As New MagickReadSettings
    Dim gBmp As Bitmap = Nothing
    Dim bmps As New List(Of Bitmap)
    Dim Orientation As Integer

    msg = ""
    ext = Path.GetExtension(fName).ToLower

    Try
      If extWindows.IndexOf(ext) >= 0 Then
        ' windows should be faster, when possible
        Using iStream As New FileStream(fName, FileMode.Open, FileAccess.Read)
          gBmp = Bitmap.FromStream(iStream, False, False) ' fromfile leaves the file open.
          ' xbmp prevents error in multipage files
          ''gBmp = xBmp.Clone
          'Dim nFrames As Integer
          'Dim fd As FrameDimension
          'fd = New FrameDimension(gBmp.FrameDimensionsList(0))
          'nFrames = gBmp.GetFrameCount(fd)
          'If nFrames <= 1 Or Not allpages Then Set32bppPArgb(gBmp)

          Orientation = 1 ' default no rotation
          For Each prop As PropertyItem In gBmp.PropertyItems
            If prop.Id = 274 Then
              Orientation = prop.Value(0)
              Exit For
            End If
          Next prop

          'Try
          '  Orientation = gBmp.GetPropertyItem(274).Value(0)
          'Catch ex As Exception
          '  Orientation = 1
          'End Try
          Select Case Orientation
            Case 1 ' none
            Case 2
              gBmp.RotateFlip(RotateFlipType.RotateNoneFlipX)
            Case 3
              gBmp.RotateFlip(RotateFlipType.Rotate180FlipNone)
            Case 4
              gBmp.RotateFlip(RotateFlipType.Rotate180FlipX)
            Case 5
              gBmp.RotateFlip(RotateFlipType.Rotate90FlipX)
            Case 6
              gBmp.RotateFlip(RotateFlipType.Rotate90FlipNone)
            Case 7
              gBmp.RotateFlip(RotateFlipType.Rotate270FlipX)
            Case 8
              gBmp.RotateFlip(RotateFlipType.Rotate270FlipNone)
          End Select

          'gBmp.RemovePropertyItem(274)
          Set32bppPArgb(gBmp)
          Return gBmp
        End Using

      Else ' use imagemagick
        If ext = ".pdf" Then rs.Density = New ImageMagick.Density(pdfDensity, pdfDensity) ' 300 is a reasonably high density - should make it a user parameter.
        If ext = ".pcd" Then
          'rs.ColorSpace = ColorSpace.sRGB
          fName &= "[5]" ' read hi-res
        End If

        Using img As MagickImage = New MagickImage() ' stream requires format to be specified in readsettings.
          img.Read(fName, rs)
          If ext = ".pdf" Then img.HasAlpha = False
          If ext = ".pcd" Then
            fName = Left(fName, Len(fName) - 3)
            img.ColorSpace = ColorSpace.sRGB ' bug in imagemagick
          End If
          gBmp = img.ToBitmap
          Set32bppPArgb(gBmp)
          Return gBmp
        End Using
      End If

    Catch ex As Exception
      msg = ex.Message & " The file could not be read."
    End Try

    clearBitmap(gBmp)
    Return Nothing

  End Function

  Function readMultiframeBitmap(ByRef fName As String, ByRef msg As String, Optional pdfDensity As Integer = 300) As List(Of Bitmap)

    ' error: returns nothing. Msg contains error message or, if multipage, the number of pages
    Dim ext As String
    Dim rs As New MagickReadSettings
    Dim gBmp As Bitmap = Nothing
    Dim bmpFrames As New List(Of Bitmap)

    msg = ""
    ext = Path.GetExtension(fName).ToLower
    If ext = ".pdf" Then rs.Density = New Density(pdfDensity, pdfDensity) ' reasonably high density - should make it a user parameter.

    Try ' use imagemagick
      Using imgFrames As New MagickImageCollection(fName, rs) ' stream requires format to be specified in readsettings.
        For Each img As MagickImage In imgFrames
          If ext = ".pdf" Then img.HasAlpha = False
          gBmp = img.ToBitmap
          Set32bppPArgb(gBmp)
          bmpFrames.Add(gBmp.Clone)
          clearBitmap(gBmp)
        Next img
      End Using

    Catch ex As Exception
      msg = ex.Message & " The file could not be read."
      clearBitmap(gBmp)
      For Each bmp As Bitmap In bmpFrames
        clearBitmap(bmp)
      Next bmp
      Return Nothing
    End Try

    Return bmpFrames

  End Function



  Function getPicinfo(ByVal fName As String, ByVal totalPages As Boolean, Optional ByVal pageNo As Integer = 1) As pictureInfo

    Dim picInfo As New pictureInfo

    Using img As MagickImage = New MagickImage

      Try
        Select Case Path.GetExtension(fName).ToLower
          Case ".pcd" ' photo CD, pdf check last page only
            img.Ping(fName & "[-1]")
          Case ".pdf" ' photo CD, pdf check last page only
            img.Ping(fName & "[0]")
          Case Else
            img.Ping(fName)
        End Select

        picInfo.FormatID = img.Format
        picInfo.FormatDescription = img.FormatInfo.Description
        picInfo.Width = img.Width
        picInfo.Height = img.Height
        picInfo.fileSize = img.FileSize
        picInfo.colorSpace = img.ColorSpace
        picInfo.colorDepth = img.Depth
        picInfo.hasPages = img.FormatInfo.IsMultiFrame
        picInfo.hasAlpha = img.HasAlpha
        picInfo.Compression = img.CompressionMethod
        picInfo.Orientation = img.Orientation
        picInfo.ResolutionX = img.Density.X
        picInfo.ResolutionY = img.Density.Y
        picInfo.fileSize = img.FileSize
        picInfo.isNull = False
      Catch ex As Exception
        picInfo.isNull = True
        picInfo.ErrMessage = ex.Message
        Return picInfo
      End Try

    End Using

    If picInfo.Width <> 0 Then picInfo.Aspect = picInfo.Height / picInfo.Width Else picInfo.Aspect = 0.75

    Return picInfo

  End Function

  Function newWindow(qImage As Bitmap) As mudViewer

    Dim rview As mudViewer
    rview = New mudViewer

    rview.setBitmap(qImage)
    frmMain.redraw(rview, 3)
    frmMain.setupPanelPage(rview)

    Return rview

  End Function


  Function newWindow(Optional ByVal x As Integer = 2000, Optional ByVal y As Integer = 1500) As mudViewer

    Dim iwidth, iheight As Integer
    Dim mView As mudViewer

    mView = New mudViewer

    If x > 0 And y > 0 Then
      iwidth = x : iheight = y
    Else
      iwidth = 2000 : iheight = 1500
    End If

    mView.newBitmap(iwidth, iheight, mBackColor)
    frmMain.redraw(mView, 3)
    frmMain.setupPanelPage(mView)

    Return mView

  End Function

  Sub AddMruFile(ByRef filename As String)

    Dim i As Integer
    Dim k As Integer

    If Trim(filename) = "" Then Exit Sub

    k = 0
    ' look for filename in mru files
    For i = 1 To nmru
      If eqstr(filename, mru(i)) Then
        k = i
        Exit For
      End If
    Next i

    ' put it on top and shift others down
    If k = 0 Then k = UBound(mru)
    For i = k To 2 Step -1
      mru(i) = mru(i - 1)
    Next i
    mru(1) = filename

  End Sub

  Sub DelMruFile(ByRef filename As String)

    Dim i As Integer
    Dim k As Integer

    k = 0
    ' look for filename in mru files
    For i = 1 To nmru
      If filename = mru(i) Then
        k = i
        Exit For
      End If
    Next i

    ' put it on top and shift others down
    If k = 0 Then Exit Sub

    For i = k To nmru - 1
      mru(i) = mru(i + 1)
    Next i
    mru(nmru) = ""

  End Sub

  Sub loadMru(ByRef filemenu As ToolStripMenuItem)

    Dim i As Integer
    Dim k As Integer
    Dim item As ToolStripMenuItem

    k = 0
    For i = 1 To nmru
      If mru(i) = "" Then Exit For
      item = getMenuItem(filemenu, "mnufilemru" & i)
      item.Text = "&" & i & " " & abbrevFilename(mru(i))
      item.Visible = True
      item.Enabled = True
      k = i
    Next i

    For i = k + 1 To nmru
      item = getMenuItem(filemenu, "mnufilemru" & i)
      item.Visible = False
      item.Enabled = False
    Next i

  End Sub

  Function getMenuItem(ByRef filemenu As ToolStripMenuItem, ByRef mName As String) As ToolStripMenuItem

    Dim item As ToolStripMenuItem
    Dim childItem As ToolStripMenuItem
    Dim q As ToolStripItem

    For Each q In filemenu.DropDownItems
      If TypeOf q Is ToolStripMenuItem Then
        item = q
        If eqstr(item.Name, mName) Then Return item
      End If
    Next q

    For Each q In filemenu.DropDownItems
      If TypeOf q Is ToolStripMenuItem Then
        item = q
        childItem = checkChildItems(item, mName)
        If childItem IsNot Nothing Then Return childItem
      End If
    Next q

    Return Nothing

  End Function

  Function checkChildItems(ByRef item As ToolStripMenuItem, ByRef mname As String) As ToolStripMenuItem

    Dim subitem As ToolStripMenuItem
    Dim itemx As Object

    checkChildItems = Nothing

    If eqstr(item.Name, mname) Then
      checkChildItems = item
    Else
      For Each itemx In item.DropDownItems
        If TypeOf (itemx) Is ToolStripMenuItem Then
          subitem = itemx
          checkChildItems = checkChildItems(subitem, mname)
          If checkChildItems IsNot Nothing Then Exit For
        End If
      Next itemx
    End If

  End Function

  Function abbrevFilename(ByRef fName As String) As String

    ' replace everything between the second and the last slash with "..."

    Dim k1, i, k2 As Integer

    k1 = 0 : k2 = 0
    i = InStr(fName, "\")
    If i > 0 Then k1 = InStr(i + 1, fName, "\")
    If k1 = 2 Then k1 = InStr(k1 + 1, fName, "\") ' go past \\

    For i = Len(fName) To 1 Step -1
      If fName.Chars(i - 1) = "\" Then
        k2 = i
        Exit For
      End If
    Next i

    If Len(fName) > 35 And k1 > 0 And k2 - k1 > 3 Then
      abbrevFilename = Left(fName, k1) & "..." & Mid(fName, k2)
    Else
      abbrevFilename = fName
    End If

  End Function

  Function InputFilename(ByRef MultiFile As Boolean) As String()

    ' returns n=count, and filenames 0-based

    Dim i As Integer
    Dim result As DialogResult
    Dim filter As String = Nothing
    Dim openDlg As New OpenFileDialog
    Dim s As String

    filter = "All Files|*.*"
    s = ""
    For Each fmt As MagickFormatInfo In MagickNET.SupportedFormats
      If fmt.IsReadable Then s &= ";*." & System.Enum.GetName(GetType(MagickFormat), fmt.Format)
    Next fmt
    s = s.Remove(0, 1).ToLower

    filter &= "|Image Files|" & s

    For i = 0 To fmtCommon.Count - 1
      If fmtCommon(i).ID = DefaultExt Then openDlg.FilterIndex = i + 2
      filter &= "|" & fmtCommon(i).Description & "|" & fmtCommon(i).Ext.Replace(".", "*.")
    Next i

    openDlg.Filter = filter
    openDlg.InitialDirectory = loadPath
    openDlg.Multiselect = True
    openDlg.Title = "Open Image File"

    Try
      result = openDlg.ShowDialog
    Catch ex As Exception
      result = DialogResult.Abort
    End Try

    If result = DialogResult.OK Then
      Return openDlg.FileNames
    Else
      Return Nothing
    End If

  End Function

  Sub openPicFile()
    ' open file(s) in new forms
    ' this is called by mnuFileOpen_click in parent and child forms

    Dim n As Integer
    Dim i As Integer
    Dim filenames() As String
    Dim rview As mudViewer = Nothing
    Dim tab As TabPage = Nothing

    filenames = InputFilename(True) ' get files to open
    If filenames Is Nothing Then Exit Sub

    For i = 0 To UBound(filenames)
      rview = FileIsOpen(filenames(i))
      If rview Is Nothing Then
        rview = loadNew(filenames(i))
      Else
        If i = n Then rview.Activate(Nothing, Nothing)
      End If
    Next i

  End Sub
  Sub LoadMruFile(ByVal index As Integer)

    Dim strFileName As String
    Dim mv As mudViewer
    Dim rview As mudViewer = Nothing

    strFileName = mru(index)
    For Each mv In mViews
      If eqstr(mv.picName, strFileName) Then ' already open
        rview = mv
        Exit For
      End If
    Next mv

    If rview IsNot Nothing Then
      AddMruFile(strFileName) ' move to top of list
      rview.Activate(Nothing, Nothing)
    Else
      rview = loadNew(strFileName)
    End If

  End Sub


  Sub HelpAbout()
    Using frm As New frmAbout
      frm.ShowDialog()
    End Using
  End Sub

  Sub HelpBrowse(ByVal url As String)

    Dim s As String

    If url <> "" Then
      s = GetWebData(url)
      Try
        System.Diagnostics.Process.Start(s) ' show the url in a browser
      Catch ex As Exception
        MsgBox("Couldn't launch the browser." & crlf & ex.Message)
      End Try
    End If

  End Sub

  Function GetWebData(ByRef url As String) As String
    ' get parameters from registry to pass to the web site for updpate check
    Return url & "?version=" & My.Application.Info.Version.Major & "." & _
      My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build
  End Function

  Function checknumber(ByRef txt As String, ByVal min As Double, ByVal max As Double, ByRef x As Double) As Boolean

    ' returns true if the number is between min and max inclusive, x has the value

    If Not IsNumeric(txt) Then
      Return False
    End If

    x = Val(txt)

    If x < min Or x > max Then
      Return False
    Else
      Return True
    End If

  End Function
  Function SetWallPaper(ByVal fName As String) As String
    ' nothing newer in .net
    Dim i As Integer

    i = api.SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, fName, SPIF_UPDATEINIFILE + SPIF_SENDWININICHANGE)
    If i <> 0 Then
      SetWallPaper = ""
    Else
      i = Marshal.GetLastWin32Error()
      SetWallPaper = New System.ComponentModel.Win32Exception(i).Message
    End If
  End Function

  Sub SetScreenSaver(ByVal saver0 As Boolean, ByVal saver1 As Boolean, ByVal saver2 As Boolean, _
    ByRef settings() As Boolean)
    ' changes "settings"

    Dim i As Integer
    Dim s As String
    Dim msg As String = ""
    ' windows 7: only the SPI_SETSCREENSAVEACTIVE to SystemParametersInfo is supposed to work, and it doesn't.
    ' the registry works for both XP and Windows 7

    If saver0 <> settings(0) Then
      If saver0 Then s = "1" Else s = "0"
      Try
        Registry.SetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveActive", s)
      Catch ex As Exception
        msg = ex.Message
      End Try
      If msg = "" Then settings(0) = (s = "1")
    End If

    If saver1 <> settings(1) Then
      Try
        i = api.SystemParametersInfo(SPI_SETLOWPOWERACTIVE, saver1, Nothing, SPIF_SENDWININICHANGE)
        If i = 0 Then
          i = Marshal.GetLastWin32Error()
          msg = New System.ComponentModel.Win32Exception(i).Message
        End If
      Catch ex As Exception
        msg = ex.Message
      End Try
      If msg = "" Then settings(1) = saver1
    End If

    If saver2 <> settings(2) Then
      Try
        i = api.SystemParametersInfo(SPI_SETPOWEROFFACTIVE, saver2, Nothing, SPIF_SENDWININICHANGE)
        If i = 0 Then
          i = Marshal.GetLastWin32Error()
          msg = New System.ComponentModel.Win32Exception(i).Message
        End If
      Catch ex As Exception
        msg = ex.Message
      End Try
      If msg = "" Then settings(2) = saver2
    End If

  End Sub

  Sub GetScreenSaverSettings(ByRef saver0 As Boolean, ByRef saver1 As Boolean, ByRef saver2 As Boolean)

    Dim i As Integer
    Dim Active As Boolean = False
    Dim s As String
    Dim msg As String

    ' windows 7: these calls to SystemParametersInfo don't work any more. Set save1 and the others don't matter except on xp
    Try
      s = Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "Scrnsave.exe", "missing")
      If s = "missing" Or s Is Nothing Then
        saver0 = False
      Else
        s = Registry.GetValue("HKEY_CURRENT_USER\Control Panel\Desktop", "ScreenSaveActive", "missing")
        If s = "missing" Or s Is Nothing Then saver0 = False Else saver0 = True
      End If
    Catch ex As Exception
      msg = ex.Message
    End Try

    'i = SystemParametersInfo(SPI_GETSCREENSAVEACTIVE, Nothing, Active, 0)
    'i = GetLastError
    'If i = 0 Then saver1 = Active

    i = api.SystemParametersInfo(SPI_GETLOWPOWERACTIVE, Nothing, Active, 0)
    If i = 0 Then i = Marshal.GetLastWin32Error()
    If i = 0 Then saver1 = Active

    i = api.SystemParametersInfo(SPI_GETPOWEROFFACTIVE, Nothing, Active, 0)
    If i = 0 Then i = Marshal.GetLastWin32Error()
    If i = 0 Then saver2 = Active

  End Sub

  Function FileIsOpen(ByRef fileName As String) As mudViewer

    Dim fName As String
    Dim mv As mudViewer

    FileIsOpen = Nothing
    fName = Trim(fileName)

    For Each mv In mViews
      If eqstr(mv.picName, fName) Then
        FileIsOpen = mv
        Exit For
      End If
    Next mv

  End Function

  Function getLinkTarget(ByRef sq As String) As String

    Dim s As Object
    Dim sl As Object

    s = CreateObject("wscript.shell")
    sl = s.CreateShortcut(sq)
    getLinkTarget = sl.TargetPath

  End Function

  Function ErrorDescription(ByRef ErrorCode As Integer) As String

    Dim infoSize As Integer
    Dim msgs As String

    msgs = New String(" ", 2048)
    infoSize = api.FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM, 0, ErrorCode, 0, msgs, Len(msgs), 0)
    ErrorDescription = Left(msgs, infoSize)

  End Function

  Public Sub SetWebDefaults()

    iniWebConvertUTCtoLocal = 0
    iniWebBackColor = System.Drawing.Color.Ivory
    iniWebForeColor = System.Drawing.Color.DarkGray
    iniWebnColumns = 4
    iniWebTarget = 0
    iniWebThumbX = 200
    iniWebThumbY = 200
    iniWebShadowSize = 6
    iniWebTableBorder = 0
    iniWebCellPadding = 25
    iniWebCellSpacing = 0
    iniWebTitleSize = 0
    iniWebCaptionSize = 9
    iniWebCaptionAlign = "left"
    iniWebFont = "Arial"
    iniWebResize = 1
    iniWebImageX = 1600
    iniWebImageY = 1600

    iniWebSaveThumbnail = 1
    iniWebSaveImage = 0
    iniWebGoogleAnalytics = ""
    iniWebGoogleEvents = False

  End Sub

  Sub showPicture(ByVal fName As String, ByRef rview As pViewer, _
    ByVal Thumbnail As Boolean, ByRef pComments As List(Of PropertyItem), Optional ByVal redrawZoom As Integer = 0)
    ' zoomin = 0 for max size, no zoomin allowed.
    ' zoomin < 0 for scale to rview
    ' zoomin > 0 for zoom 
    ' always zoom centered

    Dim msg As String = ""

    If rview.ClientSize.Height <= 0 Then Exit Sub ' view size zero
    rview.setBitmap(Nothing)

    If Thumbnail Then ' try to get a thumbnail image first
      Using bmp As Bitmap = readThumbnail(0, 0, fName, msg, True)
        rview.setBitmap(bmp)
      End Using
    End If

    If Not Thumbnail OrElse Len(msg) <> 0 OrElse rview.Bitmap Is Nothing Then
      Using bmp As Bitmap = readBitmap(fName, msg)
        rview.setBitmap(bmp)
      End Using
    End If

    If rview.Bitmap Is Nothing Then
      If msg <> "" Then MsgBox(msg)
      pComments = New List(Of PropertyItem)
      Exit Sub
    End If

    pComments = readPropertyItems(fName)
    rview.Zoom(redrawZoom)

  End Sub

  Public Sub OpenDoc(ByRef DocName As String)

    ' open a photo

    Dim mView As mudViewer

    mView = FileIsOpen(DocName)
    If mView Is Nothing Then ' open new file
      mView = loadNew(DocName)
    Else ' file already open -- show it
      'If Form.ActiveForm IsNot frmMain Then
      '  mView.Activate()
      '  showfrmmain()
      'Else
      '  mView.Activate()
      '  End If
      mView.Activate(Nothing, Nothing)
    End If

  End Sub

  Sub readToolButtons(ByVal buttonSize As Integer)
    ' reads the icons: toolsize 0 = 24x24, 1 = 32x32

    Dim fPath As String
    Dim ss() As String
    Dim s As String
    Dim fName As String

    iniToolbarPic = New Collection

    fPath = exePath & "\picons\"
    If buttonSize = 0 Then
      s = "24*.png"
    Else
      s = "32*.png"
    End If

    Try
      ss = Directory.GetFiles(fPath, s)
    Catch ex As Exception
      MsgBox("Error reading icon. " & ex.Message)
      Exit Sub
    End Try

    For Each fName In ss
      Try
        Using img As Bitmap = Image.FromFile(fName)
          s = Mid(Path.GetFileNameWithoutExtension(fName), 3)
          iniToolbarPic.Add(img.Clone, s)
        End Using

      Catch ex As Exception
        MsgBox("Error reading icon. " & ex.Message)
        Exit For
      End Try

    Next fName

  End Sub

  Sub initTools()

    Dim b As ToolStripItem
    Dim bd As ToolStripDropDownItem

    ' first, initialize all the available buttons
    ' these tags have to agree with what's in the imagelists.

    ' readToolButtons(iniButtonSize)

    iniButton = New Collection

    b = New ToolStripButton
    b.Text = "New Image"
    b.Tag = "mnufilenew"
    b.Name = "tsFilenew"
    b.ToolTipText = "New Image"
    b.TextImageRelation = TextImageRelation.ImageAboveText
    ' b.Image = iniToolbarPic(b.Tag)

    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Open File"
    b.Tag = "mnufileopen"
    b.Name = "tsFileopen"
    b.ToolTipText = "Open File"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Save File"
    b.Tag = "mnufilesave"
    b.Name = "tsFilesave"
    b.ToolTipText = "Save the Current Image"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Print"
    b.Tag = "mnufileprint"
    b.Name = "tsFileprint"
    b.ToolTipText = "Print the Current Image"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Email Photo"
    b.Tag = "mnufilesend"
    b.Name = "tsFilesend"
    b.ToolTipText = "Send as Email Attachment"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Delete File"
    b.Tag = "mnueditdelete"
    b.Name = "tsEditdelete"
    b.ToolTipText = "Delete the Current Image File"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Cut to Clipboard"
    b.Tag = "mnueditcut"
    b.Name = "tsEditcut"
    b.ToolTipText = "Cut (move) to Clipboard"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Copy to Clipboard"
    b.Tag = "mnueditcopy"
    b.Name = "tsEditcopy"
    b.ToolTipText = "Copy to Clipboard"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Paste"
    b.Tag = "mnueditpaste"
    b.Name = "tsEditpaste"
    b.ToolTipText = "Paste from the Clipboard into the Image"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Undo"
    b.Tag = "mnueditundo"
    b.Name = "tsEditundo"
    b.ToolTipText = "Undo"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Redo"
    b.Tag = "mnueditredo"
    b.Name = "tsEditredo"
    b.ToolTipText = "Redo"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Explore"
    b.Tag = "mnufileexplore"
    b.Name = "tsFileexplore"
    b.ToolTipText = AppName & " Explorer"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Zoom In"
    b.Tag = "mnuviewzoomin"
    b.Name = "tsViewzoomin"
    b.ToolTipText = "Zoom In"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Zoom Out"
    b.Tag = "mnuviewzoomout"
    b.Name = "tsViewzoomout"
    b.ToolTipText = "Zoom Out"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Zoom Window"
    b.Tag = "mnuviewzoom"
    b.Name = "tsViewzoom"
    b.ToolTipText = "Zoom into a Window or Box"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Full Screen"
    b.Tag = "mnuviewfullscreen"
    b.Name = "tsViewfullscreen"
    b.ToolTipText = "Full Screen View"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Rotate Left"
    b.Tag = "mnuimagerotateleft"
    b.Name = "tsImagerotateleft"
    b.ToolTipText = "Rotate Left"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Rotate Right"
    b.Tag = "mnuimagerotateright"
    b.Name = "tsImagerotateright"
    b.ToolTipText = "Rotate Right"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Crop"
    b.Tag = "mnuimagesetcrop"
    b.Name = "tsImagesetcrop"
    b.ToolTipText = "Crop (trim) the Image"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Select Rectangle"
    b.Tag = "mnueditselrectangle"
    b.Name = "tsEditselrectangle"
    b.ToolTipText = "Select Rectangle"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Select Ellipse"
    b.Tag = "mnueditselellipse"
    b.Name = "tsEditselellipse"
    b.ToolTipText = "Select Ellipse"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Freehand Select"
    b.Tag = "mnueditselfreehand"
    b.Name = "tsEditselfreehand"
    b.ToolTipText = "Select Freehand"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Select Similar Color"
    b.Tag = "mnueditselectsimilar"
    b.Name = "tsEditselectsimilar"
    b.ToolTipText = "Select Similar Colors"
    b.TextImageRelation = TextImageRelation.ImageAboveText
    b.Visible = False ' needs to be added eventually
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Text"
    b.Tag = "mnudrawtext"
    b.Name = "tsDrawtext"
    b.ToolTipText = "Text"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Draw Line"
    b.Tag = "mnudrawline"
    b.Name = "tsDrawline"
    b.ToolTipText = "Draw a Line"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    'b = New ToolStripButton
    'b.Tag = "mnudrawshape"
    'b.Name = "tsDrawshape"
    'b.ToolTipText = "Draw a Shape"
    'b.imagekey = b.tag: ' b.Image = iniToolbarPic(b.Tag)
    ' b.TextImageRelation = TextImageRelation.ImageAboveText
    'iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Sketch"
    b.Tag = "mnudrawsketch"
    b.Name = "tsDrawsketch"
    b.ToolTipText = "Freehand Sketch"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Flood Fill"
    b.Tag = "mnudrawfill"
    b.Name = "tsDrawfill"
    b.ToolTipText = "Fill Area of Similar Color"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Fill Selection"
    b.Tag = "mnudrawfillselection"
    b.Name = "tsDrawfillselection"
    b.ToolTipText = "Fill Selected Area"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Color Sample"
    b.Tag = "mnucolorsample"
    b.Name = "tsColorsample"
    b.ToolTipText = "Select Color from Image"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Foreground Color"
    b.Tag = "mnudrawforecolor"
    b.Name = "tsDrawforecolor"
    b.ToolTipText = "Drawing Color"
    b.ImageKey = ""
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Background Color"
    b.Tag = "mnudrawbackcolor"
    b.Name = "tsDrawbackcolor"
    b.ToolTipText = "Background Color"
    b.ImageKey = ""
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Help"
    b.Tag = "mnuhelphelp"
    b.Name = "tsHelphelp"
    b.ToolTipText = "Help Topics"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "View Refresh"
    b.Tag = "mnuviewrefresh"
    b.Name = "tsViewrefresh"
    b.ToolTipText = "View Refresh"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)

    b = New ToolStripSeparator
    b.Text = ""
    b.Tag = "---"
    b.Name = "tsSeparator"
    b.ToolTipText = ""
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniButton.Add(b, b.Tag)


    ' now the explorer buttons
    iniVButton = New Collection

    b = New ToolStripButton
    b.Text = "Open File"
    b.Tag = "mnufileopen"
    b.Name = "tsFileopen"
    b.ToolTipText = "Edit the Current Image"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniVButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Save As"
    b.Tag = "mnufilesave"
    b.Name = "tsFilesave"
    b.ToolTipText = "Save the Current Image"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniVButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Print"
    b.Tag = "mnufileprint"
    b.Name = "tsFileprint"
    b.ToolTipText = "Print the Current Image"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniVButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Email Photo"
    b.Tag = "mnufilesend"
    b.Name = "tsfilesend"
    b.ToolTipText = "Send as Email Attachment"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniVButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Delete File"
    b.Tag = "mnueditdelete"
    b.Name = "tsEditelete"
    b.ToolTipText = "Delete the selected photo"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniVButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Web Page"
    b.Tag = "mnuwebpage"
    b.Name = "tsWebpage"
    b.ToolTipText = "Make a web page of the selected photos"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniVButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Slide Show"
    b.Tag = "mnuslideshow"
    b.Name = "tsSlideshow"
    b.ToolTipText = "Play a slide show of selected photos"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniVButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Up One Level"
    b.Tag = "mnuuponelevel"
    b.Name = "tsUponelevel"
    b.ToolTipText = "Move Up One Level"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniVButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Previous Folder"
    b.Tag = "mnuviewprevious"
    b.Name = "tsViewprevious"
    b.ToolTipText = "View Previous Folder"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniVButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Next Folder"
    b.Tag = "mnuviewnext"
    b.Name = "tsViewnext"
    b.ToolTipText = "View Next Folder"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniVButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Rotate Left"
    b.Tag = "mnuimagerotateleft"
    b.Name = "tsImagerotateleft"
    b.ToolTipText = "Rotate the Image Left 90°"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniVButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Rotate Right"
    b.Tag = "mnuimagerotateright"
    b.Name = "tsImagerotateright"
    b.ToolTipText = "Rotate the Image Right 90°"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniVButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Full Screen"
    b.Tag = "mnuviewfullscreen"
    b.Name = "tsViewfullscreen"
    b.ToolTipText = "Full Screen View"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniVButton.Add(b, b.Tag)

    bd = New ToolStripDropDownButton
    bd.Text = "View Style"
    bd.Tag = "mnuviewstyle"
    bd.Name = "tsViewstyle"
    bd.ToolTipText = "Select the View Style"
    ' bd.Image = iniToolbarPic(bd.Tag)
    ' bd.DropDown  =  ' add this in assignvtoolbar
    bd.TextImageRelation = TextImageRelation.ImageAboveText
    iniVButton.Add(bd, bd.Tag)

    b = New ToolStripButton
    b.Text = "Help"
    b.Tag = "mnuhelphelp"
    b.Name = "tsHelphelp"
    b.ToolTipText = "Help Topics"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniVButton.Add(b, b.Tag)

    b = New ToolStripButton
    b.Text = "Copy Image"
    b.Tag = "mnueditcopy"
    b.Name = "tsEditcopy"
    b.ToolTipText = "Copy Image to the Windows Clipboard"
    ' b.Image = iniToolbarPic(b.Tag)
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniVButton.Add(b, b.Tag)

    b = New ToolStripSeparator
    b.Text = ""
    b.Tag = "---"
    b.Name = "tsvSeparator"
    b.ToolTipText = ""
    b.TextImageRelation = TextImageRelation.ImageAboveText
    iniVButton.Add(b, b.Tag)

  End Sub

  Sub resetTools()

    ' assign the array of toolbar buttons
    iniToolButton(1) = "---"
    iniToolButton(2) = "mnufileopen"
    iniToolButton(3) = "mnufilesave"
    iniToolButton(4) = "mnufileprint"
    iniToolButton(5) = "mnufilesend"
    iniToolButton(6) = "mnueditdelete"
    iniToolButton(7) = "---"
    iniToolButton(8) = "mnueditcopy"
    iniToolButton(9) = "mnueditundo"
    iniToolButton(10) = "mnueditredo"
    iniToolButton(11) = "---"
    iniToolButton(12) = "mnufileexplore"
    iniToolButton(13) = "---"
    iniToolButton(14) = "mnuviewzoomin"
    iniToolButton(15) = "mnuviewzoomout"
    iniToolButton(16) = "mnuviewzoom"
    iniToolButton(17) = "mnuviewfullscreen"
    iniToolButton(18) = "mnuimagerotateleft"
    iniToolButton(19) = "mnuimagerotateright"
    iniToolButton(20) = "mnuimagesetcrop"
    iniToolButton(21) = "---"
    iniToolButton(22) = "mnueditselrectangle"
    iniToolButton(23) = "mnueditselellipse"
    iniToolButton(24) = "mnueditselfreehand"
    iniToolButton(25) = "mnueditselectsimilar"
    iniToolButton(26) = "---"
    iniToolButton(27) = "mnudrawtext"
    iniToolButton(28) = "mnudrawfill"
    iniToolButton(29) = "mnudrawfillselection"
    iniToolButton(30) = "---"
    iniToolButton(31) = "mnucolorsample"
    iniToolButton(32) = "mnudrawforecolor"
    iniToolButton(33) = "mnudrawbackcolor"
    iniToolButton(34) = "---"
    iniToolButton(35) = "mnuhelphelp"

    nToolButtons = 35

    ' assign explorer tool buttons
    iniVToolButton(1) = "---"
    iniVToolButton(2) = "mnufileopen"
    iniVToolButton(3) = "mnufilesave"
    iniVToolButton(4) = "mnufileprint"
    iniVToolButton(5) = "mnufilesend"
    iniVToolButton(6) = "mnueditdelete"
    iniVToolButton(7) = "---"
    iniVToolButton(8) = "mnuwebpage"
    iniVToolButton(9) = "mnuslideshow"
    iniVToolButton(10) = "---"
    iniVToolButton(11) = "mnuuponelevel"
    iniVToolButton(12) = "mnuviewprevious"
    iniVToolButton(13) = "mnuviewnext"
    iniVToolButton(14) = "---"
    iniVToolButton(15) = "mnuimagerotateleft"
    iniVToolButton(16) = "mnuimagerotateright"
    iniVToolButton(17) = "mnuviewfullscreen"
    iniVToolButton(18) = "---"
    iniVToolButton(19) = "mnuviewstyle"
    iniVToolButton(20) = "---"
    iniVToolButton(21) = "mnuhelphelp"

    nVToolButtons = 21

  End Sub

  Sub assignAllToolbars()

    frmMain.assignToolbar()
    frmExplore.assignVToolbar()

  End Sub

  Sub distance(ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double, ByRef dx As Double, ByRef dy As Double, ByRef distance As Double)

    dx = x2 - x1
    dy = y2 - y1

    If dx <> 0 Or dy <> 0 Then
      distance = Sqrt(dx * dx + dy * dy)
    Else
      distance = 0
    End If

  End Sub

  Function Yintercept(ByVal x As Double, ByVal x1 As Double, ByVal y1 As Double, ByVal x2 As Double, ByVal y2 As Double, ByRef Yint As Double) As Integer

    Dim dx, dy As Double

    If (x <= x1 And x >= x2) Or (x >= x1 And x <= x2) Then
      ' get intercept y2 at x
      dx = x2 - x1
      dy = y2 - y1
      If dx = 0 Then
        Yint = y1
      Else
        Yint = y1 + (x - x1) * dy / dx
      End If
      Yintercept = 1 ' success
    Else
      Yintercept = 0
    End If

  End Function

  Function getArrow(xP As List(Of Point), lineWidth As Double) As List(Of Point)

    ' adds the arrowhead points to the end of xp

    Dim p1, p2 As Point
    Dim pArrow As New List(Of Point)
    Dim pHead As New List(Of Point)
    Dim rP As New List(Of Point)
    Dim dy, dx, d As Double
    Dim i As Integer

    If xP.Count < 2 Then Return rP

    p1 = xP(xP.Count - 2)
    p2 = xP(xP.Count - 1)
    If p1 = p2 AndAlso xP.Count > 2 Then p1 = xP(xP.Count - 3) ' double click adds a point sometimes

    distance(p1.X, p1.Y, p2.X, p2.Y, dx, dy, d)
    If d <= 0 Then Return rP
    dx = dx / d ' cosine
    dy = dy / d ' sine

    rP.AddRange(xP)
    i = rP.Count
    rP.Add(New Point(p2.X - lineWidth * 7, p2.Y + lineWidth * 2))
    rP.Add(New Point(p2.X, p2.Y))
    rP.Add(New Point(rP(i).X, p2.Y - lineWidth * 2))

    rP(i) = rotatePoint(rP(i), rP(i + 1), dy, dx)
    rP(i + 2) = rotatePoint(rP(i + 2), rP(i + 1), dy, dx)

    Return rP

  End Function

  Function getCurve(zP As List(Of Point), xInc As Double) As List(Of Point)

    ' get the points that make up a curve

    Dim xP As New List(Of Point)
    Dim ix, iy As Integer
    Dim dx, d, dy As Double
    Dim n As Integer
    Dim x As Double
    Dim i As Integer
    Dim iKnot As Integer
    Dim np As Integer

    Dim xk(zP.Count + 4) As Double
    Dim yk(zP.Count + 4) As Double
    Dim xd(zP.Count + 4) As Double
    Dim sx(zP.Count + 4) As Double
    Dim sy(zP.Count + 4) As Double
    np = zP.Count

    For i = 1 To np
      xk(i) = zP(i - 1).X : yk(i) = zP(i - 1).Y ' knots
    Next i

    n = np
    If n < 2 Then
      xk(2) = xk(1) : yk(2) = yk(1)
    End If

    If n < 3 Then
      xk(3) = xk(2) : yk(3) = yk(2)
      n = 3
    End If

    xd(1) = 0
    For i = 2 To n
      distance(xk(i - 1), yk(i - 1), xk(i), yk(i), dx, dy, d)
      xd(i) = xd(i - 1) + d
      If xd(i) = xd(i - 1) Then xd(i) = xd(i) + 0.01
    Next i

    spcoeff(xd, xk, sx, n)
    spcoeff(xd, yk, sy, n)

    xP.Add(New Point(xk(1), yk(1)))

    If n >= 3 Then
      iKnot = 1
      For x = xd(1) + xInc To xd(n) - xInc Step xInc
        If x > xd(iKnot + 1) Then iKnot = iKnot + 1
        ix = spline(x, xd, xk, sx, iKnot)
        iy = spline(x, xd, yk, sy, iKnot)
        xP.Add(New Point(ix, iy))
      Next x
    End If

    xP.Add(New Point(xk(n), yk(n)))

    Return xP

  End Function


  Sub MergeSort(v As Object, ByRef ix As List(Of Integer), min As Integer, max As Integer)

    Dim half As Integer
    Dim isString As Boolean
    Dim j, i, k As Integer

    If v.count - 1 < min Then Exit Sub
    isString = TypeOf v(min) Is String

    If max - min > 1 Then
      Dim tix As New List(Of Integer)
      tix.AddRange(ix)  ' copy index array
      half = (max + min) * 0.5
      If min < half Then MergeSort(v, tix, min, half) ' sort lower half
      If half + 1 < max Then MergeSort(v, tix, half + 1, max) ' sort upper half
      ' now merge the two sorted halves
      i = min : j = half + 1 : k = min - 1
      Do While i <= half Or j <= max
        k = k + 1
        If j > max Then
          ix(k) = tix(i)
          i = i + 1
        ElseIf i > half Then
          ix(k) = tix(j)
          j = j + 1
          ' ignore case when comparing strings 
        ElseIf (isString AndAlso String.Compare(v(tix(i)), v(tix(j)), True) <= 0) OrElse _
          (Not isString AndAlso v(tix(i)) <= v(tix(j))) Then
          ix(k) = tix(i)
          i = i + 1
        Else
          ix(k) = tix(j)
          j = j + 1
        End If
      Loop
    Else ' 1 or 2 elements -- do by hand
      If max - min >= 1 Then
        k = min
        ' If v(ix(k)) > v(ix(k + 1)) Then ' compare first and second items
        If isString AndAlso String.Compare(v(ix(k)), v(ix(k + 1)), True) > 0 OrElse _
          (Not isString) AndAlso v(ix(k)) > v(ix(k + 1)) Then
          i = ix(k) : ix(k) = ix(k + 1) : ix(k + 1) = i ' swap
        End If
      End If
    End If

  End Sub

  Function getFilePaths(ByVal fPath As String, ByRef fileNames As List(Of String), ByVal subFolders As Boolean) As Integer
    ' gets file names from folder fPath into fileNames()
    ' returns -1 for error

    Dim i1 As Integer
    Dim fNames As New List(Of String)
    Dim dirNames As New List(Of String)

    If Not Directory.Exists(fPath) Then Return -1

    abort = False

    fNames = dirGetfiles(fPath, iniFileType)
    fileNames.AddRange(fNames)

    If Not abort And subFolders Then
      Try
        dirNames = Directory.GetDirectories(fPath).ToList
      Catch
        dirNames = Nothing
        Return -1
      End Try
      If dirNames IsNot Nothing Then
        For Each s As String In dirNames
          i1 = getFilePaths(s, fileNames, subFolders)
        Next s
      End If
    End If

    Return 0

  End Function

  Function CheckFolder(ByVal dirPath As String, ByRef Ask As Boolean) As Integer
    ' 0 means folder exists or was created, 1 means not created, error, or escape

    Dim k As Integer
    Dim s As String
    Dim mResult As MsgBoxResult

    If Left(dirPath, 1) = "\" Then dirPath = Left(dirPath, Len(dirPath) - 1) ' remove trailing "\"

    If Directory.Exists(dirPath) Then
      Return 0
    End If

    ' not found - return 1 if created, -1 of "no" or error.
    Try
      s = Path.GetDirectoryName(dirPath)
    Catch
      Return 1
    End Try

    k = CheckFolder(s, Ask)
    If k >= 0 Then  ' recursive.
      ' parent dirPath is there.
      If Not Ask Then
        mResult = MsgBoxResult.Yes
      Else
        mResult = MsgBox("The folder """ & dirPath & """ was not found. Do you want to create it?", MsgBoxStyle.YesNoCancel)
      End If

      If mResult = MsgBoxResult.Yes Then
        Try
          Directory.CreateDirectory(dirPath)
          Return 1
        Catch
          Return -1
        End Try
      Else
        Return -1
      End If
    Else
      Return k  ' 1 or 0
    End If

  End Function

  Sub spcoeff(ByRef xn() As Double, ByRef fn() As Double, ByRef s() As Double, ByRef np As Integer)

    ' SPLINE COEFFICIENTS N, XN, FM, S - SEE SHAMPINE & ALLEN

    ' this once for each curve.
    '
    ' XN is the X, FN is f(X) for all knots
    ' NP is the number of knots
    ' S is an array of spline coefficients for this curve

    Dim rho(UBound(xn)) As Double
    Dim tau(UBound(xn)) As Double
    Dim rx1, rx2 As Double
    Dim x, d As Double
    Dim i As Integer

    rho(2) = 0 : tau(2) = 0
    rx1 = xn(2) - xn(1)

    For i = 2 To np - 1
      rx2 = rx1 ' last rx1
      rx1 = xn(i + 1) - xn(i)
      x = (rx2 / rx1) * (rho(i) + 2) + 2
      rho(i + 1) = -1 / x
      d = 6 * ((fn(i + 1) - fn(i)) / rx1 - (fn(i) - fn(i - 1)) / rx2) / rx1
      tau(i + 1) = (d - rx2 * tau(i) / rx1) / x
    Next i

    s(1) = 0 : s(np) = 0
    For i = np - 1 To 2 Step -1
      s(i) = rho(i + 1) * s(i + 1) + tau(i + 1)
    Next i

  End Sub

  Function spline(ByRef x As Double, ByRef xn() As Double, ByRef fn() As Double, ByRef s() As Double, ByRef L As Integer) As Double

    ' SPLINE FUNCTION    NP, XN, FN, S, X, ANSWER   X IS WITHIN RANGE

    ' This subroutine calculates f(X) for the value of X.  It is called
    ' for each point to be determined on the curve.
    '
    ' XN is the X, FN is f(X) for all knots (initial points).
    ' NP is the number of knots (points) defining the curve
    ' S is an array of spline coefficients for this curve.
    '
    ' SPCOEFF must have been called to get the values of S.  XN, FN, and S
    '  are used to get the value of X.
    '
    ' L tells which knot (point in XN and FN) that X lies after.

    Dim x2, x1, y1 As Double
    Dim rx2, sl, rx1, ry1 As Double

    x1 = xn(L + 1) - x
    x2 = x - xn(L)
    y1 = xn(L + 1) - xn(L)
    sl = s(L)
    rx1 = fn(L)
    rx2 = fn(L + 1)
    ry1 = s(L + 1)

    spline = x1 * sl * (x1 * x1 / y1 - y1) / 6 + x2 * ry1 * (x2 * x2 / y1 - y1) / 6 + (x1 * rx1 + x2 * rx2) / y1

  End Function

  Sub rotateXY(ByRef x As Double, ByRef y As Double, ByVal xCenter As Double, ByVal yCenter As Double, ByVal sine As Double, ByVal cosine As Double)

    Dim newX As Double
    Dim newY As Double

    newX = xCenter + ((x - xCenter) * cosine - (y - yCenter) * sine)
    newY = yCenter + ((y - yCenter) * cosine + (x - xCenter) * sine)

    x = newX
    y = newY

  End Sub

  Function rotatePoint(p As Point, pCenter As Point, sine As Double, cosine As Double) As Point

    Dim pNew As Point
    Dim xCenter, yCenter, x, y As Double

    xCenter = pCenter.X
    yCenter = pCenter.Y
    x = p.X
    y = p.Y

    pNew.X = xCenter + ((x - xCenter) * cosine - (y - yCenter) * sine)
    pNew.Y = yCenter + ((y - yCenter) * cosine + (x - xCenter) * sine)

    Return pNew

  End Function

  Function rotateRectangle(r As Rectangle, textAngle As Double) As List(Of Point)
    ' rotate the rectangle, return 4 points in rotated box

    Dim pp As New List(Of Point)
    Dim sine, cosine As Double
    Dim pCenter As Point

    pCenter = New Point(r.X + r.Width \ 2, r.Y + r.Height \ 2)
    sine = Sin(-textAngle * piOver180) : cosine = Cos(-textAngle * piOver180)

    pp.Add(New Point(r.X, r.Y))
    pp.Add(New Point(r.X + r.Width - 1, r.Y))
    pp.Add(New Point(r.X + r.Width - 1, r.Y + r.Height = 1))
    pp.Add(New Point(r.X, r.Y + r.Height = 1))

    For i As Integer = 0 To pp.Count - 1
      rotatePoint(pp(i), pCenter, sine, cosine)
    Next i

    Return pp

  End Function

  Function lineIntersect(ByRef x1 As Double, ByRef y1 As Double, ByRef x2 As Double, ByRef y2 As Double, _
    ByRef x3 As Double, ByRef y3 As Double, ByRef x4 As Double, ByRef y4 As Double, _
    ByRef xi As Double, ByRef yi As Double) As Integer

    ' get the intersecting point of two lines
    ' returns -1 if parallel, 0 if intersection is outside the line segments, 1 if intersection is inside

    Dim ua As Double
    Dim ud As Double
    Dim dx1, dx2, dy1, dy2 As Double

    lineIntersect = 0
    ' slope the same?
    dx1 = x2 - x1 : dy1 = y2 - y1
    dx2 = x4 - x3 : dy2 = y4 - y3
    If dx1 = 0 And dx2 = 0 Or dy1 = 0 And dy2 = 0 Then
      lineIntersect = -1
    ElseIf dx1 <> 0 And dx2 <> 0 And dy1 <> 0 And dy2 <> 0 Then
      If dx1 / dy1 = dx2 / dy2 Then lineIntersect = -1
    End If

    If lineIntersect >= 0 Then
      ud = ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1))
      If ud = 0 Then ua = 0 Else ua = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / ud
      'ub = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / ud

      xi = x1 + ua * (x2 - x1)
      yi = y1 + ua * (y2 - y1)

      If xi < x1 And xi < x2 Or xi > x1 And xi > x2 Then lineIntersect = 0 Else lineIntersect = 1

    Else
      ' Parallel. set it at one of the endpoints.
      xi = x2 : yi = y2
    End If

  End Function

  Public Sub checkToolButton(ByRef tStrip As ToolStrip, ByVal buttonTag As String, ByVal check As Boolean)
    ' check or uncheck a tool button

    Dim item As ToolStripItem
    Dim b As ToolStripButton

    For Each item In tStrip.Items
      If TypeOf (item) Is ToolStripButton Then
        b = item
        If b.Tag = buttonTag Then
          b.Checked = check
          Exit For
        End If
      End If
    Next item

  End Sub

  Sub alignDraw(ByRef mView As mudViewer, ByRef qBmp As Bitmap, _
    ByRef rX() As Double, ByRef rY() As Double, npts As Integer)
    ' move the four points to the four corners. Similar to stretchDraw, but backwards.
    ' paste onto mView. qBmp is used as a temporary field
    ' sets qBmp to nothing
    ' npts is the number of points set by the user
    ' there should be 4 points in rX, rY

    Dim xx(npts * 4 - 1) As Double

    For i As Integer = 0 To 3
      xx(i * 4) = rX(i) : xx(i * 4 + 1) = rY(i)
    Next i
    ' move to ll, lr, ul, ur
    xx(2) = 0 : xx(3) = qBmp.Height - 1
    xx(6) = qBmp.Width - 1 : xx(7) = qBmp.Height - 1
    xx(10) = 0 : xx(11) = 0
    xx(14) = qBmp.Width - 1 : xx(15) = 0

    Using img As New MagickImage(qBmp)
      img.Distort(DistortMethod.Affine, xx)
      mView.setBitmap(img.ToBitmap)
    End Using
    clearBitmap(qBmp)
  End Sub


  Sub stretchDraw(ByRef mView As mudViewer, ByRef qBmp As Bitmap, _
    ByRef destX() As Double, ByRef destY() As Double, npts As Integer)

    ' paste onto mView. qBmp is used as a temporary field
    ' sets qBmp to nothing
    ' npts is the number of points set by the user

    Dim i, j As Integer
    Dim k1, k2 As Integer
    Dim x, y As Double
    Dim ix, iy As Integer
    Dim x1, y1, x2, y2 As Double
    Dim dx, dy, d As Double
    Dim dx1, dy1, dx2, dy2 As Double
    Dim destPoints(2) As PointF

    Dim hasRegion As Boolean
    Dim regionWidth, regionHeight As Integer
    Dim regionLeft, regionTop As Integer

    Dim z As Double

    Dim v1() As Byte = Nothing ' destination
    Dim v2() As Byte = Nothing ' source

    ''If qBmp.HasRegion Then
    'hasRegion = True
    ''  r = qBmp.GetRegionBounds(Nothing)
    'regionWidth = r.Width
    'regionHeight = r.Height
    'regionLeft = r.Left
    'regionTop = r.Top
    ''Else
    hasRegion = False
    regionWidth = qBmp.Width
    regionHeight = qBmp.Height
    regionLeft = 0
    regionTop = 0
    ''  End If

    If npts < 4 Then
      Using g As Graphics = Graphics.FromImage(mView.Bitmap)
        destPoints(0).X = destX(2)
        destPoints(0).Y = destY(2)
        destPoints(1).X = destX(3)
        destPoints(1).Y = destY(3)
        destPoints(2).X = destX(0)
        destPoints(2).Y = destY(0)
        mView.InterpolationMode = InterpolationMode.High
        g.DrawImage(qBmp, destPoints)
      End Using
      Exit Sub
    End If

    ' resize larger to make sure there are not holes in destination bitmap, or smaller to go faster.
    distance(destX(0), destY(0), destX(1), destY(1), dx, dy, d)
    z = d / regionWidth
    distance(destX(2), destY(2), destX(3), destY(3), dx, dy, d)
    If d / regionWidth > z Then z = d / regionWidth
    distance(destX(0), destY(0), destX(2), destY(2), dx, dy, d)
    If d / regionHeight > z Then z = d / regionHeight
    distance(destX(1), destY(1), destX(3), destY(3), dx, dy, d)
    If d / regionHeight > z Then z = d / regionHeight
    If destY(0) <> destY(1) Or destY(2) <> destY(3) Then z = z * 1.4

    mView.ResizeBitmap(New Size(qBmp.Width * z, qBmp.Height * z), qBmp, qBmp)

    ''If qBmp.HasRegion Then
    ''  r = qBmp.GetRegionBounds(Nothing)
    'regionWidth = r.Width
    'regionHeight = r.Height
    'regionLeft = r.Left
    'regionTop = r.Top
    ''Else
    regionWidth = qBmp.Width
    regionHeight = qBmp.Height
    ''  End If

    If qBmp.PixelFormat <> PixelFormat.Format32bppPArgb Then Set32bppPArgb(qBmp)

    ''If hasRegion Then
    '  rgnHandle = lead2.GetRgnHandle
    '  lead2.SetRgnHandle(rgnHandle, 0, 0, L_RGN_SETNOT)
    '  lead2.DeleteRgnHandle(rgnHandle)
    '  lead2.fill(RGB(252, 0, 254))  ' arbitrary color for transparency
    '  rgnHandle = lead2.GetRgnHandle
    '  lead2.SetRgnHandle(rgnHandle, 0, 0, L_RGN_SETNOT)
    '  lead2.DeleteRgnHandle(rgnHandle)
    '  End If

    ' dx1, dy1 is the increment along the left vertical side, dx2, dy2 along the right.
    dx1 = (destX(0) - destX(2)) / regionHeight
    dy1 = (destY(0) - destY(2)) / regionHeight
    dx2 = (destX(1) - destX(3)) / regionHeight
    dy2 = (destY(1) - destY(3)) / regionHeight

    i = 0

    v1 = getBmpBytes(mView.Bitmap) ' destination
    v2 = getBmpBytes(qBmp)

    'ProgressBar1.Visible = True
    x1 = destX(2) : y1 = destY(2) ' starting points
    x2 = destX(3) : y2 = destY(3)

    ' move the bits over from v2 to v1
    For i = regionTop To regionTop + regionHeight - 1

      'ProgressBar1.Value = (i - regionTop) * 100 / (regionHeight - 1)

      x1 = x1 + dx1
      y1 = y1 + dy1
      x2 = x2 + dx2
      y2 = y2 + dy2

      dx = (x2 - x1) / regionWidth
      dy = (y2 - y1) / regionWidth
      x = x1
      y = y1
      k1 = (i * qBmp.Width + regionLeft) * 4 ' source address.
      For j = regionLeft To regionLeft + regionWidth - 1
        x = x + dx
        y = y + dy
        ix = x  ' destination coordinates ix, iy
        iy = y

        If hasRegion Then
          If v2(k1) <> 254 Or v2(k1 + 1) <> 0 Or v2(k1 + 2) <> 252 Then  ' arbitrary non-region color
            'If lead2.IsPtInRgn(j, i) Then
            If ix >= 0 And ix < mView.Bitmap.Width And iy >= 0 And iy < mView.Bitmap.Height Then
              k2 = (iy * mView.Bitmap.Width + ix) * 4 ' 4 bytes per pixel
              v1(k2) = v2(k1)
              v1(k2 + 1) = v2(k1 + 1)
              v1(k2 + 2) = v2(k1 + 2)
            End If
          End If

        Else ' no region
          If ix >= 0 And ix < mView.Bitmap.Width And iy >= 0 And iy < mView.Bitmap.Height Then
            k2 = (iy * mView.Bitmap.Width + ix) * 4 ' 4 bytes per pixel
            v1(k2) = v2(k1)
            v1(k2 + 1) = v2(k1 + 1)
            v1(k2 + 2) = v2(k1 + 2)
          End If
        End If

        k1 = k1 + 4
      Next j
    Next i

    v2 = Nothing
    setBmpBytes(mView.Bitmap, v1)
    v1 = Nothing
    mView.Zoom()
    'ProgressBar1.Visible = False

  End Sub

  Sub mouseWheelZoom(ByRef rview As pViewer, ByRef e As MouseEventArgs, ByRef timer1 As Timer, _
    ByVal zoomFac As Double, Optional ByVal paintQuality As InterpolationMode = InterpolationMode.Low)
    ' use paintquality. If there is a timer1, then set it to 350 ms so it can be redrawn in the calling form at bicubic.
    ' zoomfac is how much to zoom per wheel click

    Dim p, pb, pMin, pMax As Point
    Dim x, z As Double

    If timer1 IsNot Nothing Then timer1.Stop()
    rview.InterpolationMode = paintQuality

    If e.Delta > 0 Then z = zoomFac Else z = 1 / zoomFac
    x = rview.ZoomFactor * z

    If Abs(x - 1) < Abs(zoomFac - 1) / 2 Then x = 1
    If Abs(x - 2) < Abs(zoomFac - 1) / 2 Then x = 2

    If x <= Maxzoom And x >= 1 / Maxzoom Then
      pMax = rview.ControlToBitmap(New Point(rview.ClientSize))
      pMin = rview.ControlToBitmap(New Point(0, 0))

      p = rview.ControlToBitmap(e.Location)
      pb.X = p.X - ((pMax.X + pMin.X) \ 2 - p.X) * z
      pb.Y = p.Y - ((pMax.Y + pMin.Y) \ 2 - p.Y) * z

      'If pMin.X >= 0 And pMax.X <= rview.Bitmap.Width And pMin.Y >= 0 And pMax.Y <= rview.Bitmap.Height Then ' normal zoom
      p = rview.ControlToBitmap(New Point(e.X, e.Y))
      rview.Zoom(x, p)

      'Else ' center zoom
      '  pb.X = rview.Bitmap.Width / 2
      '  pb.Y = rview.Bitmap.Height / 2
      '  rview.setCenterPoint(pb, False)
      '  rview.Zoom(x)
      'End If

    Else ' zoom in -- always to cursor
      p = rview.ControlToBitmap(New Point(e.X, e.Y))
      rview.Zoom(x, p)
    End If

    If timer1 IsNot Nothing Then timer1.Interval = 350 : timer1.Start() ' redraw high quality in 350 milliseconds
  End Sub

  Function getShadow(ByRef gImage As Bitmap, ByVal shadowOffset As Integer, ByVal backColor As System.Drawing.Color, _
    ByVal thumbX As Integer, ByVal thumbY As Integer) As Bitmap
    ' adds a fuzzy shadow around an image, returns gdi image.
    ' Called by frmexplore and frmwebpage
    ' frmexplore needs to fit the image in a larger rectangle, filling with background if the aspect is different.

    Dim p(2) As PointF
    Dim xoff, yoff As Integer

    If gImage Is Nothing Then Return Nothing

    Using _
        shadowImg As Bitmap = New Bitmap(thumbX + shadowOffset, thumbY + shadowOffset, _
          System.Drawing.Imaging.PixelFormat.Format32bppPArgb), _
        shadowG As Graphics = Graphics.FromImage(shadowImg)

      xoff = (thumbX - gImage.Width) / 2
      yoff = (thumbY - gImage.Height) / 2
      shadowG.FillRectangle(New SolidBrush(backColor), New Rectangle(0, 0, shadowImg.Width, shadowImg.Height))
      p(0).X = gImage.Width + xoff : p(0).Y = shadowOffset + yoff
      p(1).X = gImage.Width + xoff : p(1).Y = gImage.Height + yoff
      p(2).X = shadowOffset + xoff : p(2).Y = gImage.Height + yoff
      drawFuzzyShadow(p, System.Drawing.Color.FromArgb(255, 60, 60, 60), backColor, shadowOffset, shadowG)
      ' this line covers up a bug in the lineargradientbrush.
      shadowG.FillRectangle(New SolidBrush(System.Drawing.Color.FromArgb(255, 60, 60, 60)), xoff + 1, yoff + 1, _
        gImage.Width, gImage.Height)

      Try
        shadowG.DrawImage(gImage, New Rectangle(xoff, yoff, gImage.Width, gImage.Height))
      Catch ex As Exception
        MsgBox(ex.Message)
      End Try
      getShadow = shadowImg.Clone
    End Using


  End Function

  Sub drawFuzzyShadow(ByVal pCorner() As PointF, ByVal color1 As System.Drawing.Color, ByVal color2 As System.Drawing.Color, _
    ByVal offset As Integer, ByVal g As Graphics)

    Dim i, j As Integer
    Dim pz(12) As PointF
    Dim fcolor(0) As System.Drawing.Color
    Dim dx, dy As Integer
    Dim r As Rectangle
    Dim c1, c2 As System.Drawing.Color
    Dim mode As LinearGradientMode

    For j = 0 To UBound(pCorner) ' clockwise
      fcolor(0) = color2
      ' add offset to the circle points
      For i = 0 To 12 : pz(i).X = fuzCircle(i).X * offset + pCorner(j).X : pz(i).Y = fuzCircle(i).Y * offset + pCorner(j).Y : Next i
      Using _
        fuzzyGPath As GraphicsPath = New GraphicsPath(pz, fuzType), _
        fuzzyPBrush As PathGradientBrush = New PathGradientBrush(fuzzyGPath)

        fuzzyPBrush.SurroundColors = fcolor
        fuzzyPBrush.CenterColor = color1
        fuzzyPBrush.CenterPoint = New Point(pCorner(j).X, pCorner(j).Y)
        g.FillPath(fuzzyPBrush, fuzzyGPath)
      End Using
      'fuzzyGPath.Dispose()
      'fuzzyPBrush.Dispose()
    Next j

    For j = 1 To UBound(pCorner) ' clockwise
      ' draw the sides of the shadows
      dx = pCorner(j).X - pCorner(j - 1).X
      dy = pCorner(j).Y - pCorner(j - 1).Y
      r = Nothing

      If dx > 0 And dy = 0 Then ' top
        r = New Rectangle(pCorner(j - 1).X, pCorner(j - 1).Y - offset, dx, offset)
        c1 = color2
        c2 = color1
        mode = LinearGradientMode.Vertical
        'fuzzyLBrush = New LinearGradientBrush(r, color2, color1, LinearGradientMode.Vertical)
      ElseIf dx = 0 And dy > 0 Then ' right side
        r = New Rectangle(pCorner(j - 1).X, pCorner(j - 1).Y, offset, dy)
        c1 = color1
        c2 = color2
        mode = LinearGradientMode.Horizontal
        'fuzzyLBrush = New LinearGradientBrush(r, color1, color2, LinearGradientMode.Horizontal)
      ElseIf dx < 0 And dy = 0 Then ' bottom
        r = New Rectangle(pCorner(j).X, pCorner(j).Y, -dx, offset)
        c1 = color1
        c2 = color2
        mode = LinearGradientMode.Vertical
        'fuzzyLBrush = New LinearGradientBrush(r, color1, color2, LinearGradientMode.Vertical)
        ' ' fuzzyLBrush = New LinearGradientBrush(New PointF(pCorner(j).X, pCorner(j).Y), New PointF(pCorner(j).X, pCorner(j).Y + offset), color1, color2)
      ElseIf dx = 0 And dy < 0 Then ' left side
        r = New Rectangle(pCorner(j).X - offset, pCorner(j).Y, offset, -dy)
        c1 = color2
        c2 = color1
        mode = LinearGradientMode.Horizontal
        'fuzzyLBrush = New LinearGradientBrush(r, color2, color1, LinearGradientMode.Horizontal)
      End If
      Using fuzzyLBrush As LinearGradientBrush = New LinearGradientBrush(r, c1, c2, mode)
        If r <> Nothing Then g.FillRectangle(fuzzyLBrush, r)
      End Using
    Next j

  End Sub


  Public Sub batchAdjust(ByRef gBitmap As Bitmap, pView As pViewer)
    ' runs the batch color conversions on gBitmap
    ' this is called by frmColorBatchAdjust and frmConvert
    Dim gammaRed, gammaGreen, gammaBlue As Double

    Using img As New MagickImage(gBitmap)

      If clrValue(6) <> 0 Then ' autoadjust only
        img.Normalize()
      Else ' change specific stuff
        gammaRed = (clrValue(3) / 100 + 1) ^ 3 ' gamma value for img.level function
        gammaGreen = (clrValue(4) / 100 + 1) ^ 3
        gammaBlue = (clrValue(5) / 100 + 1) ^ 3

        Try
          img.BackgroundColor = Color.White
          img.Level(New Percentage(0), New Percentage(100), gammaRed, Channels.Red)
          img.Level(New Percentage(0), New Percentage(100), gammaGreen, Channels.Green)
          img.Level(New Percentage(0), New Percentage(100), gammaBlue, Channels.Blue)

        Catch ex As Exception
          MsgBox(ex.Message)
        End Try

      End If

      Using b1 As New Bitmap(img.ToBitmap)
        pView.BrightConSat(clrValue(0), clrValue(1), clrValue(2), b1, gBitmap, Nothing)
      End Using
    End Using

  End Sub

  Public Sub EditPasteNew()

    Dim rview As mudViewer
    Dim bmp As Bitmap = Nothing

    If Clipboard.ContainsImage Then
      Try
        bmp = Clipboard.GetImage
      Catch ex As Exception
        MsgBox("Paste Error: " & ex.Message)
      End Try

      If bmp IsNot Nothing Then
        Set32bppPArgb(bmp)
        rview = newWindow(bmp.Width, bmp.Height)
        rview.setBitmap(bmp)
        rview.picModified = True
        rview.Zoom(0)
        clearBitmap(bmp)
      End If
    End If

  End Sub

  Function docProp(ByRef mode As Integer, ByRef pd As PrintDocument, ByRef callingForm As Form) As Integer
    ' the printer driver dialog DocumentProperties, a Win32 call.

    Dim hDevMode As IntPtr
    Dim pDevMode As IntPtr
    Dim devModeData As IntPtr
    Dim hPrinter As New System.IntPtr()
    Dim i As Integer

    If pd.PrinterSettings.PrinterName = pd.DefaultPageSettings.PrinterSettings.PrinterName Then ' don't assign if there's a different printer
      pd.PrinterSettings.DefaultPageSettings.Landscape = pd.DefaultPageSettings.Landscape
    End If
    hDevMode = pd.PrinterSettings.GetHdevmode(pd.PrinterSettings.DefaultPageSettings)
    pDevMode = api.GlobalLock(hDevMode)

    ' with mode 0 for size needed in the output devmode in the next call

    i = api.DocumentProperties(callingForm.Handle, hPrinter, pd.PrinterSettings.PrinterName, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero)
    If i < 0 Then Return -1

    devModeData = Marshal.AllocHGlobal(i)

    docProp = api.DocumentProperties(callingForm.Handle, IntPtr.Zero, pd.PrinterSettings.PrinterName, devModeData, pDevMode, mode)
    i = api.GlobalUnlock(hDevMode)

    If docProp = IDOK Then ' save the results if OK was hit (and no error)
      pd.PrinterSettings.SetHdevmode(devModeData)
      pd.PrinterSettings.DefaultPageSettings.SetHdevmode(devModeData)
      pd.DefaultPageSettings.SetHdevmode(devModeData)
    End If

    i = api.GlobalFree(hDevMode)
    Marshal.FreeHGlobal(devModeData)

  End Function

  Sub fitFile(fName As String, ByRef pView As pViewer)
    ' loads a file into pview at zoom 0

    Dim msg As String = ""

    Try
      Using bmp As Bitmap = readBitmap(fName, msg)
        If bmp IsNot Nothing Then
          pView.setBitmap(bmp)
          pView.Zoom(0)
        End If
      End Using
    Catch ex As Exception
      msg = ex.Message
    End Try

    If pView.Bitmap Is Nothing AndAlso Len(msg) <> 0 Then MsgBox(msg)

  End Sub

  Function findIndex(ByRef v() As Object, ByRef vx As Object) As Integer

    Dim i As Integer
    findIndex = -1

    For i = 0 To UBound(v)
      If v(i) Is vx Then
        findIndex = i
        Exit For
      End If
    Next i

  End Function

  Function dirGetfiles(ByVal fPath As String, ByVal ext As List(Of String)) As List(Of String)

    ' returns the files in the folder fpath with an extension in ext().
    ' directory.getfiles strange searching for things like *.tif - don't use the second parameter

    Dim s, sx As String
    Dim fNames As New List(Of String)
    Dim outNames As New List(Of String)

    Try
      fNames = Directory.GetFiles(fPath).ToList
    Catch ex As Exception
      'MsgBox(ex.Message)
      Return outNames
    End Try

    For Each s In fNames
      For Each sx In ext
        If sx IsNot Nothing AndAlso (Len(sx) <> 0 And s.EndsWith(sx, True, Nothing)) Then
          outNames.Add(s)
          Exit For
        End If
      Next sx
    Next s

    Return outNames

  End Function

  Public Function getColor(ByVal OriginalColor As System.Drawing.Color, ByVal colorDialog1 As ColorDialog) As System.Drawing.Color
    Dim result As DialogResult

    getColor = OriginalColor
    colorDialog1.Color = OriginalColor
    colorDialog1.FullOpen = True

    Try
      result = colorDialog1.ShowDialog()
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    If result = DialogResult.OK Then getColor = colorDialog1.Color

  End Function

  Sub getStateImages(ByRef imgLviewState As ImageList, backColor As System.Drawing.Color, iWidth As Integer, iHeight As Integer)
    ' create checkboxes for ListView1 because the originals are ugly. isize is the size of imgsmallicons

    Dim p() As Point

    imgLviewState.Images.Clear()
    imgLviewState.ImageSize = New Size(iWidth, iHeight)
    Using _
      img As Bitmap = New Bitmap(imgLviewState.ImageSize.Width, imgLviewState.ImageSize.Height, PixelFormat.Format32bppPArgb), _
      g As Graphics = Graphics.FromImage(img), _
      gBrush As SolidBrush = New SolidBrush(backColor)

      g.FillRectangle(gBrush, 0, 0, img.Width, img.Height)

      ReDim p(4)
      p(0) = New Point(1, 1)
      p(1) = New Point(imgLviewState.ImageSize.Width - 2, p(0).Y)
      p(2) = New Point(p(1).X, imgLviewState.ImageSize.Height - 2)
      p(3) = New Point(p(0).X, p(2).Y)
      p(4) = New Point(p(0).X, p(0).Y)
      Using gPen As Pen = New Pen(System.Drawing.Color.DarkGray, 2)
        Try
          g.DrawLines(gPen, p)
          imgLviewState.Images.Add("unchecked", img.Clone)
        Catch ex As Exception
        End Try
      End Using

      ReDim p(2)
      p(0) = New Point(2, imgLviewState.ImageSize.Height / 2 - 1)
      p(1) = New Point(imgLviewState.ImageSize.Width / 4, imgLviewState.ImageSize.Height - 5)
      p(2) = New Point(imgLviewState.ImageSize.Width - 3, imgLviewState.ImageSize.Height / 4 - 1)
      g.SmoothingMode = SmoothingMode.HighQuality
      Using gPen As Pen = New Pen(System.Drawing.Color.DarkGreen, 2)
        Try
          g.DrawLines(gPen, p)
          imgLviewState.Images.Add("checked", img.Clone)
        Catch ex As Exception
        End Try
      End Using
    End Using

  End Sub


  Public Sub ClearAllUndo()

    ' erase all the undo files
    Dim fName As String
    Dim dirFiles As String()

    dirFiles = Directory.GetFiles(UndoPath, "~" & AppName & "*.~tmp")  ' the extension search is weird.

    For Each fName In dirFiles
      File.Delete(fName)
    Next fName

  End Sub

  Public Sub closeAll()
    Dim mv As mudViewer
    ' close all the child forms except frmExplore
    For Each mv In mViews
      mv.Close()
    Next mv

  End Sub

  Function getMuddViewer(ByVal tab As TabPage) As mudViewer

    For Each ctl As Control In tab.Controls
      If TypeOf ctl Is mudViewer Then
        Return ctl
      End If
    Next ctl

    Return Nothing

  End Function

  Function readPhotoDate(ByRef fName As String) As String

    Dim s As String
    Dim pComments As List(Of PropertyItem)

    pComments = readPropertyItems(fName)
    s = getBmpComment(propID.DateTimeOriginal, pComments)
    If s = "" Then s = getBmpComment(propID.DateTime, pComments)

    If s = "" Or s = "0000:00:00 00:00:00" Then Return ""
    ' convert to datetime type
    Try
      s = Replace(s, ":", "/", 1, 2)
      s = Format(s, "short date") & " " & Format(s, "medium time")
      Return s
    Catch
      Return ""
    End Try

  End Function

  Function readPhotoDescription(ByRef fName As String) As String

    Dim s As String
    Dim pComments As List(Of PropertyItem)

    pComments = readPropertyItems(fName)
    s = getBmpComment(propID.ImageDescription, pComments) ' get comment
    If s = Nothing Then s = ""
    Return s.Trim

  End Function

  Sub getGPSLocation(ByRef pComments As List(Of PropertyItem), ByRef Location As String,
                     ByRef Altitude As String, ByRef xLat As Double, ByRef xLon As Double,
                     ByRef altitudeFeet As Integer)

    ' get the gps location and altitude from ux.

    Dim v1 As Object = Nothing
    Dim v2 As Object = Nothing
    Dim v3 As Object = Nothing
    Dim v4 As Object = Nothing
    Dim k As Integer
    Dim msg As String = ""
    Dim v As Object
    Dim x As Double

    Location = ""
    Altitude = ""

    If pComments IsNot Nothing Then

      Try
        v = getBmpComment(propID.GpsLatitude, pComments)
        If uuBound(v) >= 2 Then
          xLat = v(0) + v(1) / 60 + v(2) / 3600
          Location = v(0) & "°" & v(1) & "'"
          If v(2) <> 0 Then Location = Location & v(2) & """"
          v = getBmpComment(propID.GpsLatitudeRef, pComments)
          If LCase(v(0)) = "s" Then xLat = -xLat
          Location &= v(0)
        End If
        v = getBmpComment(propID.GpsLongitude, pComments)
        If uuBound(v) >= 2 Then
          xLon = v(0) + v(1) / 60 + v(2) / 3600
          Location &= " " & v(0) & "°" & v(1) & "'"
          If v(2) <> 0 Then Location = Location & v(2) & """"
          v = getBmpComment(propID.GpsLongitudeRef, pComments)
          Location &= v(0)
          If LCase(v(0)) = "w" Then xLon = -xLon
        End If
      Catch ex As Exception
        msg = ex.Message
        MsgBox(msg)
      End Try

    End If

    ' gps altitude
    Altitude = ""
    k = 0
    v = getBmpComment(propID.GpsAltitude, pComments)
    If v IsNot Nothing Then
      x = v(0)
      v = getBmpComment(propID.GpsAltitudeRef, pComments)
      If v IsNot Nothing Then k = v(0)
      If k = 1 And x > 0 Then x = -x
      Altitude = Format(x / 0.3048, "###,##0") & " ft " & "(" & Format(x, "###,##0") & " m)"
      altitudeFeet = Int(x / 0.3048)
    End If

  End Sub

  Sub changeForm(ByRef fromForm As Form, ByRef toForm As Form)
    ' leaving fromForm for toForm

    If fromForm Is toForm OrElse fromForm Is Nothing Then Exit Sub

    toForm.Show() ' windowstate etc. only work after the form is shown. It looks a little funny.
    If fromForm.WindowState <> toForm.WindowState Then toForm.WindowState = fromForm.WindowState
    If toForm.WindowState = FormWindowState.Normal Then
      If toForm.Location <> fromForm.Location Then toForm.Location = fromForm.Location
      If toForm.Size <> fromForm.Size Then toForm.Size = fromForm.Size
    End If
    fromForm.Hide()

  End Sub

  Sub setiniWindowsize(ByRef frm As Form)

    If frm.WindowState <> FormWindowState.Normal Then
      iniWindowSizeX = 0
      iniWindowSizeY = 0
    Else
      iniWindowSizeX = frm.Size.Width
      iniWindowSizeY = frm.Size.Height
      iniWindowLocationX = frm.Location.X
      iniWindowLocationY = frm.Location.Y
    End If

  End Sub

  Function latlonVerify(ByVal inputLoc As String, ByRef xLat As Double, ByRef xLon As Double) As String

    ' return xLat and xLon values from a string lat-lon position. Returns null string if successful.

    Dim s As String
    Dim c As Char
    Dim msg As String = ""
    Dim i, k As Integer
    Dim x, x1 As Double

    Dim ipos, lpos As Integer
    Dim iState, iClass As Integer
    Dim gVal(6) As Double
    Dim sym(6) As Char
    Dim nVal As Integer
    Dim nHemi As Integer
    Dim hemi(1) As Char
    Dim hemiPos(1) As Integer

    Static tb(30, 30) As Integer
    ' start            number         N S E W          spare       ° ' "            space
    tb(0, 0) = 1 : tb(0, 1) = 1 : tb(0, 2) = -2 : tb(0, 3) = 0 : tb(0, 4) = -4 : tb(0, 5) = -5 : tb(0, 6) = 0 : tb(0, 7) = 0 : tb(0, 8) = 0 : tb(0, 9) = 0 ' +-0-9.
    tb(1, 0) = 2 : tb(1, 1) = -1 : tb(1, 2) = 0 : tb(1, 3) = 0 : tb(1, 4) = -4 : tb(1, 5) = -5 : tb(1, 6) = 0 : tb(1, 7) = 0 : tb(1, 8) = 0 : tb(1, 9) = 0 ' N S E W
    tb(2, 0) = 4 : tb(2, 1) = -1 : tb(2, 2) = 0 : tb(2, 3) = 0 : tb(2, 4) = 0 : tb(2, 5) = -5 : tb(2, 6) = 0 : tb(2, 7) = 0 : tb(2, 8) = 0 : tb(2, 9) = 0 ' ° ' " 
    tb(3, 0) = 5 : tb(3, 1) = -1 : tb(3, 2) = -2 : tb(3, 3) = 0 : tb(3, 4) = 4 : tb(3, 5) = 5 : tb(3, 6) = 0 : tb(3, 7) = 0 : tb(3, 8) = 0 : tb(3, 9) = 0 ' space
    tb(4, 0) = 0 : tb(4, 1) = 0 : tb(4, 2) = 0 : tb(4, 3) = 0 : tb(4, 4) = 0 : tb(4, 5) = 0 : tb(4, 6) = 0 : tb(4, 7) = 0 : tb(4, 8) = 0 : tb(4, 9) = 0 ' other
    tb(5, 0) = 0 : tb(5, 1) = -1 : tb(5, 2) = -2 : tb(5, 3) = 0 : tb(5, 4) = -4 : tb(5, 5) = -5 : tb(5, 6) = 0 : tb(5, 7) = 0 : tb(5, 8) = 0 : tb(5, 9) = 0 ' eof
    tb(6, 0) = 0 : tb(6, 1) = 0 : tb(6, 2) = 0 : tb(6, 3) = 0 : tb(6, 4) = 0 : tb(6, 5) = 0 : tb(6, 6) = 0 : tb(6, 7) = 0 : tb(6, 8) = 0 : tb(6, 9) = 0

    nVal = -1
    nHemi = -1
    ipos = 0
    lpos = ipos

    s = Trim(UCase(inputLoc)) & Chr(0)

    Do While ipos <= s.Length - 1
      c = s.Chars(ipos)
      Select Case c
        Case "0" To "9", "+", "-", "."
          iClass = 0
        Case "N", "S", "E", "W"
          iClass = 1
        Case "°", "'", """"
          iClass = 2
        Case " ", ","
          iClass = 3
        Case Chr(0) ' eof
          iClass = 5
        Case Else
          iClass = 4
      End Select

      iState = tb(iClass, iState)

      If iState < 0 Then
        Select Case iState
          Case 0 ' error
            msg = "Invalid latitude / longitude."
            Exit Do
          Case -1 ' number
            Try
              gVal(nVal + 1) = Val(s.Substring(lpos, ipos - lpos + 1))
              nVal = nVal + 1
            Catch
              msg = "Invalid number."
              Exit Do
            End Try

          Case -2 ' N S E W
            c = s.Chars(lpos)
            nHemi = nHemi + 1
            If nHemi > 1 Then
              msg = "Please use N or S and E or W."
              Exit Do
            End If
            hemi(nHemi) = c
            hemiPos(nHemi) = nVal

          Case -3 ' not used

          Case -4 ' ° ' "
            sym(nVal) = s.Chars(lpos)

          Case -5 ' space

          Case Else ' get more stuff
            ipos = ipos + 1
        End Select
        iState = 0
        lpos = ipos

      Else
        ipos = ipos + 1
      End If
    Loop


    If msg = "" Then

      Select Case nHemi
        Case Is < 0  ' no N S and W E
          If nVal = 1 Then ' two first lat, second lon
            xLat = gVal(0)
            xLon = gVal(1)
          Else
            msg = "Please use N or S and E or W."
          End If

        Case 0 ' only one - invalid
          msg = "Please use N or S and E or W."

        Case 1 ' both

          If (hemi(0) = "N" Or hemi(0) = "S") And (hemi(1) = "N" Or hemi(1) = "S") Or _
              (hemi(0) = "E" Or hemi(0) = "W") And (hemi(1) = "E" Or hemi(1) = "W") Then
            msg = "Please use N or S and E or W."
          End If

          k = hemiPos(0) ' i is the last value for the first number
          If k < 0 Then k = hemiPos(1)
          If k >= nVal Then Return "Invalid location"
          x = 0
          For i = 0 To k
            x1 = gpsVal(gVal(i), sym(i), hemi(0), i)
            If x1 = -1000 Then Return "Invalid location"
            x = x + x1
          Next i
          If hemi(0) = "N" Or hemi(0) = "S" Then xLat = x Else xLon = x
          x = 0
          For i = k + 1 To nVal
            x1 = gpsVal(gVal(i), sym(i), hemi(1), i - (k + 1))
            If x1 = -1000 Then Return "Invalid location"
            x = x + x1
          Next i
          If hemi(1) = "N" Or hemi(1) = "S" Then xLat = x Else xLon = x

      End Select

      If xLat > 90 Or xLat < -90 Then
        msg = "Invalid latitude."
      ElseIf xLon > 180 Or xLon < -180 Then
        msg = "Invalid longitude."
      End If

    End If

    If msg <> "" Then
      xLat = 0 : xLon = 0
    End If

    Return msg

  End Function

  Function gpsVal(ByVal val As Double, ByVal sym As Char, ByVal hemi As Char, ByVal iVal As Integer) As Double
    ' returns the value of minutes or seconds. Returns -1000 for error

    gpsVal = val
    If iVal > 0 And (gpsVal > 60 Or gpsVal < 0) Then Return -1000 ' error
    If sym = "'" Then
      If iVal <> 1 Then Return -1000 ' minutes have to be the second number
      gpsVal = gpsVal / 60
    ElseIf sym = """" Then
      If iVal <> 2 Then Return -1000 ' seconds have to be the third number
      gpsVal = gpsVal / 3600
    Else
      gpsVal = gpsVal / (60 ^ iVal)
    End If

    If hemi = "W" Or hemi = "S" Then gpsVal = -gpsVal

  End Function

  Sub setGPSLatLon(ByRef xLat As Double, ByRef xLon As Double, pcomments As List(Of PropertyItem))

    ' save the lat and lon into pcomments

    Dim x As Double
    Dim deg(2) As Double
    Dim s As String

    If xLat >= 0 Then s = "N" Else s = "S"
    setBmpComment(propID.GpsLatitudeRef, pcomments, s, exifType.typeAscii)

    x = Abs(xLat)
    deg(0) = Int(x)
    deg(1) = Round((x - deg(0)) * 60, 3)
    deg(2) = 0
    setBmpComment(propID.GpsLatitude, pcomments, deg, exifType.typeUnsignedRational)

    If xLon >= 0 Then s = "E" Else s = "W"
    setBmpComment(propID.GpsLongitudeRef, pcomments, s, exifType.typeAscii)

    x = Abs(xLon)
    deg(0) = Int(x)
    deg(1) = Round((x - deg(0)) * 60, 3)
    deg(2) = 0
    setBmpComment(propID.GpsLongitude, pcomments, deg, exifType.typeUnsignedRational)

  End Sub

  Sub delMatchingRawFile(ByVal jpgFile As String)

    ' delete a matching raw file

    Dim fdir As String
    Dim fname As String
    Dim ss() As String
    Dim s As String
    Dim j As Integer

    Try
      fdir = Path.GetDirectoryName(jpgFile)
      fname = Path.GetFileNameWithoutExtension(jpgFile)
      For Each fmt As ImageFormat In fmtCommon
        If fmt.ID = ".raw" Then
          ss = fmt.Ext.Split(";")
          For j = 0 To UBound(ss)
            s = fdir & "\" & fname & ss(j)
            If File.Exists(s) Then
              File.Delete(s)
              Exit For
            End If
          Next j
        End If
      Next fmt
    Catch
    End Try

  End Sub

  Function eqstr(ByRef s1 As String, ByRef s2 As String) As Boolean
    ' case insensitive string equals
    Return String.Equals(s1, s2, StringComparison.OrdinalIgnoreCase)
  End Function

  Sub SetControlColor(ctl As Control, Fore As System.Drawing.Color, Back As System.Drawing.Color)
    ctl.BackColor = Fore
    ctl.ForeColor = Back
    If ctl.HasChildren Then
      For Each childControl As Control In ctl.Controls
        SetControlColor(childControl, Fore, Back)
      Next childControl
    End If
  End Sub

  Public Sub Set32bppPArgb(ByRef bmpTarget As Bitmap, Optional scale As Double = 1)

    Dim msg As String

    If bmpTarget.PixelFormat = PixelFormat.Format32bppPArgb Then Exit Sub

    Using bmp As Bitmap = New Bitmap(bmpTarget.Width * scale, bmpTarget.Height * scale, PixelFormat.Format32bppPArgb)
      Using g As Graphics = Graphics.FromImage(bmp)
        g.InterpolationMode = InterpolationMode.NearestNeighbor
        Try
          g.DrawImage(bmpTarget, New Rectangle(0, 0, bmp.Width, bmp.Height))
        Catch ex As Exception
          msg = ex.Message
        End Try

        bmpTarget.Dispose()
        bmpTarget = bmp.Clone
      End Using
    End Using

  End Sub

  Function getSmallerImage(ByRef sourceBitmap As Bitmap, qView As pViewer, Optional quarterSize As Boolean = False) As Double
    ' resize sourceImage and save it in qview
    ' returns the amount it was scaled down

    Dim newX, newY As Integer
    Dim x As Double
    Dim tmp As InterpolationMode
    Dim newbmp As Bitmap = Nothing

    If sourceBitmap Is Nothing OrElse (sourceBitmap.Width = 0 Or sourceBitmap.Height = 0) Then Return 1 ' should never happen

    x = bigMegapix / (sourceBitmap.Width * sourceBitmap.Height)
    If quarterSize Then x = x / 4
    If x < 1 Then
      newX = sourceBitmap.Width * Sqrt(x) / 2
      newY = sourceBitmap.Height * Sqrt(x) / 2
    Else ' no change
      qView.setBitmap(sourceBitmap)
      Return 1
    End If

    tmp = qView.InterpolationMode
    qView.InterpolationMode = InterpolationMode.High
    qView.ResizeBitmap(New Size(newX, newY), sourceBitmap, newbmp) ' use the qview resize function
    qView.setBitmap(newbmp)
    clearBitmap(newbmp)

    qView.InterpolationMode = tmp
    qView.setCenterPoint(New Point(qView.Bitmap.Width \ 2, qView.Bitmap.Height \ 2), True) ' center bitmap, don't redraw

    Return newX / sourceBitmap.Width

  End Function

  Public Sub saveStuff(ByRef bmp As Bitmap, pView As pViewer, gPath As GraphicsPath, fullbitmap As Boolean)
    ' saves img to pview1 if fullbitmap is false, or qimage if fullbitmap is true.
    ' rg is the floater region from frmmain
    Dim r As Rectangle
    Dim mx As New Matrix

    If fullbitmap Then
      clearBitmap(qImage)
      qImage = bmp.Clone
    Else
      If gPath Is Nothing Then ' no region in original bitmap
        pView.setFloaterBitmap(bmp)

      Else ' use the original region for clipping
        Using _
            g As Graphics = Graphics.FromImage(pView.FloaterBitmap), _
            rgPath As GraphicsPath = gPath.Clone

          mx.Translate(-pView.FloaterPosition.X, -pView.FloaterPosition.Y)
          rgPath.Transform(mx)
          g.SetClip(rgPath)
          r = New Rectangle(0, 0, pView.FloaterBitmap.Width, pView.FloaterBitmap.Height)
          g.DrawImage(bmp, r, r, GraphicsUnit.Pixel) ' copy the magickImage to bmp
        End Using
      End If
    End If

    clearBitmap(bmp)

  End Sub

  Public Sub saveStuff(ByRef img As MagickImage, pView As pViewer, gPath As GraphicsPath, fullbitmap As Boolean)
    ' saves img to pview1 if fullbitmap is false, or qimage if fullbitmap is true.
    ' rg is the floater path from frmmain
    Dim r As Rectangle
    Dim mx As New Matrix

    If fullbitmap Then
      clearBitmap(qImage)
      qImage = img.ToBitmap
    Else
      If gPath Is Nothing Then ' no path in original bitmap
        Using bmp As Bitmap = img.ToBitmap
          pView.setFloaterBitmap(bmp)
        End Using

      Else ' use the original path for clipping
        Using _
            g As Graphics = Graphics.FromImage(pView.FloaterBitmap), _
            rgPath As GraphicsPath = gPath.Clone

          mx.Translate(-pView.FloaterPosition.X, -pView.FloaterPosition.Y)
          rgPath.Transform(mx)
          g.SetClip(rgPath)
          r = New Rectangle(0, 0, pView.FloaterBitmap.Width, pView.FloaterBitmap.Height)
          g.DrawImage(img.ToBitmap, r, r, GraphicsUnit.Pixel) ' copy the magickImage to bmp
        End Using
      End If
    End If

    clearBitmap(img)

  End Sub

  Sub clearBitmap(ByRef img As MagickImage)
    ' shorthand to dispose and set a bitmap to nothing
    If img IsNot Nothing Then img.Dispose() : img = Nothing
  End Sub

  Sub clearBitmap(ByRef bmp As Bitmap)
    ' shorthand to dispose and set a bitmap to nothing
    If bmp IsNot Nothing Then bmp.Dispose() : bmp = Nothing
  End Sub

  Function getFormat(ByVal fName As String) As MagickFormat
    ' get fmtCommon for .ext or path for a given extension ext.

    Dim ext As String

    fName = fName.Trim.ToLower
    If Len(fName) <= 0 Then Return -1
    If Not fName.StartsWith(".") Then
      ext = Path.GetExtension(fName)
    Else
      ext = fName
    End If

    ' For Each fmt As MagickFormatInfo In MagickNET.SupportedFormats
    Return extMagick.IndexOf(ext)

  End Function

  Function bitmapToMagick(bmpSource As Bitmap) As MagickImage

    If bmpSource.PixelFormat = PixelFormat.Format32bppPArgb Or bmpSource.PixelFormat = PixelFormat.Format32bppArgb Then
      ' convert to 24bpp first
      Using bmp As Bitmap = New Bitmap(bmpSource.Width, bmpSource.Height, PixelFormat.Format24bppRgb)
        Using g As Graphics = Graphics.FromImage(bmp)
          g.DrawImage(bmpSource, New Rectangle(0, 0, bmp.Width, bmp.Height))
          ''bmpSource.Dispose()  ' check all calls
          Return New MagickImage(bmp)
        End Using
      End Using

    Else
      Return New MagickImage(bmpSource)
    End If

  End Function

  Function getBmpBytes(bmp As Bitmap, Optional colorPlane As Integer = 0) As Byte()
    ' return bmp bytes. Values are b-g-r-a.
    ' colorPlane should be 0 for all colors, or 1, 2, 3, 4 for B, R, G, A
    ' bitmap should be 32-bit format

    Dim bmpData As BitmapData
    Dim ptr As IntPtr
    Dim nBytes As Integer
    Dim k As Integer

    bmpData = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat)
    ptr = bmpData.Scan0
    nBytes = Math.Abs(bmpData.Stride) * bmp.Height
    Dim rgb(nBytes - 1) As Byte
    System.Runtime.InteropServices.Marshal.Copy(ptr, rgb, 0, nBytes)
    bmp.UnlockBits(bmpData)

    If colorPlane <> 0 Then ' only return one color
      k = colorPlane - 1
      For i As Integer = 0 To (nBytes - 1) \ 4
        rgb(i) = rgb(k)
        k = k + 4
      Next i
      ReDim Preserve rgb(nBytes \ 4)
    End If

    Return rgb

  End Function

  Sub setBmpBytes(bmp As Bitmap, rgb() As Byte, Optional colorPlane As Integer = 0)
    ' copy rgb to bmp. Values are b-g-r-a.
    ' colorPlane should be 0 for all colors, or 1, 2, 3, 4 for B, R, G, A
    ' bitmap should be 32-bit format

    Dim bmpData As BitmapData
    Dim ptr As IntPtr
    Dim nBytes As Integer
    Dim k As Integer
    Dim bb() As Byte = Nothing

    If colorPlane <> 0 Then
      bb = getBmpBytes(bmp, 0) ' all bytes
      k = colorPlane - 1
      For i As Integer = 0 To UBound(bb) \ 4 ' assign color plane bytes
        bb(k) = rgb(i)
        k = k + 4
      Next i
    End If

    bmpData = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat)
    ptr = bmpData.Scan0
    If colorPlane = 0 Then ' copy original byte array
      nBytes = UBound(rgb) + 1
      System.Runtime.InteropServices.Marshal.Copy(rgb, 0, ptr, nBytes)
    Else ' rgb is color plane, use bb
      nBytes = UBound(bb) + 1
      System.Runtime.InteropServices.Marshal.Copy(bb, 0, ptr, nBytes)
    End If
    bmp.UnlockBits(bmpData)

  End Sub

  Function getHisto(bmp As Bitmap) As Integer(,)

    Static busy As Boolean = False
    Dim hist(2, 255) As Integer
    Dim bb() As Byte

    If bmp Is Nothing Then Return Nothing
    If busy Then Return Nothing
    busy = True

    bb = getBmpBytes(bmp)

    ' put the counts into hist
    For i As Integer = 0 To 2
      For j As Integer = 0 To 255
        hist(i, j) = 0
      Next j
    Next i

    For i As Integer = 0 To UBound(bb) Step 4
      hist(2, bb(i)) += 1
      hist(1, bb(i + 1)) += 1
      hist(0, bb(i + 2)) += 1
    Next i

    busy = False

    Return hist

  End Function









  Class ImageSave
    Implements IDisposable

    ''Property Imgs As MagickImageCollection = Nothing ' multipage
    Property Format As MagickFormat = MagickFormat.Unknown ' any
    Property ColorSpace As ImageMagick.ColorSpace = ImageMagick.ColorSpace.sRGB ' any
    Property ColorDepth As Integer = 8 ' any
    Property hasAlpha As Boolean = True ' any
    Property Quality As Integer = iniJpgQuality ' jpg
    Property compressionAmount As Integer = iniPngCompression  ' png
    Property compressionMethod As ImageMagick.CompressionMethod ' png, tif
    Property compressionFilter As Integer = iniPngCompressionFilter    ' png
    Property PngIndexed As Boolean = False ' png
    Property pixelDPI As Integer = 300 ' any
    Property PdfFitpage As Boolean = True ' pdf
    Property sourceFilename As String = "" ' any
    Property saveExif As Boolean = True ' jpg, tif
    Property copyProfiles As Boolean = False ' jpg, tif
    Property pComments As List(Of PropertyItem) = New List(Of PropertyItem) ' jpg, tif, png
    Property saveSize As Size = Size.Empty ' any
    Property expandSize As Boolean = False ' any

    Function write(bmp As Bitmap, fName As String, overWrite As Boolean, Optional frames As List(Of Bitmap) = Nothing) As String

      Dim sourcePropertyitems As New List(Of PropertyItem)
      Dim bmpsized As Bitmap = Nothing ' resized bitmap
      Dim bmpq As Bitmap ' either bmp or bmpsized, name for the bitmap assigned properties and saved.
      Dim mExif As ExifProfile

      If File.Exists(fName) And Not overWrite Then Return fName & " exists."

      If Format = MagickFormat.Unknown Then ' get the format from the extension
        Format = extMagick.IndexOf(Path.GetExtension(fName).ToLower)
        If Format < 0 Then Format = MagickFormat.Unknown
      End If

      If saveSize.Width <> 0 And saveSize.Height <> 0 AndAlso _
        (saveSize.Width < bmp.Width Or saveSize.Height < bmp.Height) Or expandSize Then
        ' resize and save
        Using pview As New pViewer
          pview.ResizeBitmap(saveSize.Width, saveSize.Height, bmp, bmpsized) ' keep aspect ratio
          bmpq = bmpsized ' use bmpq to save the resized bitmap
          'bmpq = New Bitmap(bmpsized.Width, bmpsized.Height, bmpsized.PixelFormat)
          'Using g As Graphics = Graphics.FromImage(bmpq)
          ' g.DrawImage(bmpsized, New Rectangle(0, 0, bmp.Width, bmpsized.Height))
          'End Using
        End Using
      Else
        bmpq = bmp ' use bmpq to save the original bitmap
      End If

      ' save exif priority is:  pcomments, copy from sourcefilename
      ' if copyprofiles is true and pcomments is present, only use pcomments instead of source
      If saveExif And pComments.Count > 0 Then
        attachPropertyItems(bmpq, pComments)
      ElseIf copyProfiles And sourceFilename <> "" Then
        ' copy profiles from the original image
        sourcePropertyitems = readPropertyItems(sourceFilename)
        attachPropertyItems(bmpq, sourcePropertyitems)
      ElseIf Not saveExif Then ' don't save exif, thumbnail, or other propertyitems
        For Each i As Integer In bmpq.PropertyIdList
          bmpq.RemovePropertyItem(i)
        Next i
      End If

      For Each prop As PropertyItem In bmpq.PropertyItems
        If prop.Id = 274 Then
          bmpq.RemovePropertyItem(274) ' don't save rotate flag -- it was rotated when read.
          Exit For
        End If
      Next prop

      bmpq.SetResolution(pixelDPI, pixelDPI)

      Select Case Format
        ' encoder parameters supported: 
        '    image/jpeg: Transformation, Quality, LuminanceTable, ChrominanceTable.
        '    image/tiff: Compression, ColorDepth, SaveFlag.
        Case MagickFormat.Jpg, MagickFormat.Jpeg
          ' use Windows Save
          jpgEncoderParameters = New EncoderParameters(1)
          jpgEncoderParameter = New EncoderParameter(Imaging.Encoder.Quality, Quality) ' jpg quality
          jpgEncoderParameters.Param(0) = jpgEncoderParameter

          Try
            bmpq.Save(fName, jpgImageCodecInfo, jpgEncoderParameters)
          Catch ex As Exception
            clearBitmap(bmpsized)
            Return ex.Message
          End Try
          clearBitmap(bmpsized)
          Return ""

          'Case MagickFormat.Tif, MagickFormat.Tiff
          ' ' use Windows Save
          '
          'Try
          ' bmpq.Save(fName, Imaging.ImageFormat.Tiff)
          ' Catch ex As Exception
          'clearBitmap(bmpsized)
          'Return ex.Message
          'End Try
          'clearBitmap(bmpsized)
          'Return ""

        Case Else ' use imagemagick to save
          If frames IsNot Nothing AndAlso frames.Count > 1 Then

            Using imgs As New MagickImageCollection ' save multipage image
              For i As Integer = 0 To frames.Count - 1
                Using img As New MagickImage(frames(i))
                  If PngIndexed AndAlso (img.Format = MagickFormat.Png32 Or img.Format = MagickFormat.Png) _
                    Then img.Format = MagickFormat.Png8
                  img.Quality = iniJpgQuality
                  img.Density = New Density(pixelDPI, pixelDPI)

                  img.ColorSpace = ColorSpace
                  img.Depth = ColorDepth
                  img.HasAlpha = hasAlpha
                  img.CompressionMethod = compressionMethod
                  img.Density = New Density(pixelDPI, pixelDPI)

                  img.AnimationDelay = 100
                  imgs.Add(img.Clone)
                End Using
              Next i

              'Dim settings As New QuantizeSettings()
              'settings.Colors = 256
              'imgs.Quantize(settings)
              'imgs.Optimize()

              Try
                imgs.Write(fName)
              Catch ex As Exception
                Return ex.Message
              End Try

              For Each img As MagickImage In imgs
                clearBitmap(img)
              Next img
            End Using

            Return "" ' success
          End If

          Using img As New MagickImage(bmpq) ' save single image
            ' add comments from bmp
            mExif = pCommentsToMagick(bmpq.PropertyItems.ToList)
            clearBitmap(bmpsized)
            If mExif IsNot Nothing AndAlso mExif.Values.Count > 0 Then img.AddProfile(mExif)

            img.Format = Format

            If PngIndexed AndAlso (img.Format = MagickFormat.Png32 Or img.Format = MagickFormat.Png) Then img.Format = MagickFormat.Png8
            img.Quality = iniJpgQuality
            img.Density = New Density(pixelDPI, pixelDPI)
            img.ColorSpace = ColorSpace
            img.Depth = ColorDepth
            img.HasAlpha = hasAlpha
            img.CompressionMethod = compressionMethod
            img.Density = New Density(pixelDPI, pixelDPI)

            img.Settings.SetDefine(MagickFormat.Pdf, "pdf:fit-to-page", True) ' untested

            Try
              img.Write(fName)
            Catch ex As Exception
              Return ex.Message
            End Try
          End Using

          Return "" ' success

      End Select
    End Function

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
    End Sub

    Public Overloads Sub Dispose() Implements System.IDisposable.Dispose
      Dispose(True)
    End Sub
  End Class

  Sub bmpMerge(bmpSource As Bitmap, bmpTarget As Bitmap, op As mergeOp, destRect As Rectangle)

    ' merges bitmaps
    Dim r As Rectangle
    Dim destBytes() As Byte
    Dim sourceBytes() As Byte
    Dim rowLenTarget, rowLenSource, ixSource As Integer
    Dim targetOffset, sourceOffset, i1 As Integer
    Dim targetPos, sourcePos As Integer
    Dim bytesPerPix As Integer

    Dim clock As New Stopwatch

    clock.Reset() : clock.Start()
    bytesPerPix = 4

    milli(0) = clock.ElapsedMilliseconds : clock.Stop() : clock.Reset() : clock.Start()


    If destRect = Rectangle.Empty Then ' default to source dimensions
      destRect.Width = bmpSource.Width
      destRect.Height = bmpSource.Height
      destRect.X = 0 : destRect.Y = 0
    End If

    ' ix1, iy1, ix2, iy2 are coordinate ranges for the destination, adjusted to bounds
    r = destRect
    r.Intersect(New Rectangle(0, 0, bmpTarget.Width, bmpTarget.Height))

    milli(1) = clock.ElapsedMilliseconds : clock.Stop() : clock.Reset() : clock.Start()

    destBytes = getBmpBytes(bmpTarget)

    milli(2) = clock.ElapsedMilliseconds : clock.Stop() : clock.Reset() : clock.Start()

    sourceBytes = getBmpBytes(bmpSource)

    milli(3) = clock.ElapsedMilliseconds : clock.Stop() : clock.Reset() : clock.Start()

    rowLenTarget = bmpTarget.Width * bytesPerPix
    rowLenSource = bmpSource.Width * bytesPerPix

    targetOffset = r.Y * rowLenTarget + r.X * bytesPerPix
    sourceOffset = (r.Y - destRect.Y) * rowLenSource + (r.X - destRect.X) * bytesPerPix

    milli(4) = clock.ElapsedMilliseconds : clock.Stop() : clock.Reset() : clock.Start()

    For iyDest As Integer = 0 To r.Height - 1
      If sourceOffset > UBound(sourceBytes) Then Exit For

      For ixDest As Integer = 0 To r.Width - 1
        If destRect.X < 0 Then ixSource = ixDest - destRect.X Else ixSource = ixDest ' intersect above causes this if
        If ixSource >= bmpSource.Width Then Exit For

        targetPos = targetOffset + ixDest * bytesPerPix ' Offset is the begginning byte of the line (used)
        sourcePos = sourceOffset + ixDest * bytesPerPix ' Pos is the current byte of the line being used.

        For k As Integer = 0 To 2

          Select Case op
            Case mergeOp.opAdd
              i1 = CInt(destBytes(targetPos)) + sourceBytes(sourcePos)
              If i1 >= 256 Then i1 = 255
              destBytes(targetPos) = i1

            Case mergeOp.opMultiply
              destBytes(targetPos) = CInt(destBytes(targetPos)) * sourceBytes(sourcePos) >> 8

            Case mergeOp.opXor
              destBytes(targetPos) = destBytes(targetPos) Xor sourceBytes(sourcePos)

            Case mergeOp.opAnd
              destBytes(targetPos) = destBytes(targetPos) And sourceBytes(sourcePos)

            Case mergeOp.opOr
              destBytes(targetPos) = destBytes(targetPos) Or sourceBytes(sourcePos)

            Case mergeOp.opMaximum
              If destBytes(targetPos) > sourceBytes(sourcePos) Then
                destBytes(targetPos) = sourceBytes(sourcePos)
              End If

            Case mergeOp.opMinimum
              If destBytes(targetPos) < sourceBytes(sourcePos) Then
                destBytes(targetPos) = sourceBytes(sourcePos)
              End If

            Case mergeOp.opAverage
              destBytes(targetPos) = (CInt(destBytes(targetPos)) + CInt(sourceBytes(sourcePos))) \ 2

            Case mergeOp.opSubtractFromSource
              i1 = CInt(sourceBytes(sourcePos)) - CInt(destBytes(targetPos))
              If i1 < 0 Then i1 = 0
              destBytes(targetPos) = i1

            Case mergeOp.opSubtractFromTarget
              i1 = CInt(destBytes(targetPos)) - CInt(sourceBytes(sourcePos))
              If i1 < 0 Then i1 = 0
              destBytes(targetPos) = i1

            Case mergeOp.opDivideSource
              destBytes(targetPos) = (sourceBytes(sourcePos)) / (destBytes(targetPos) + 1)

            Case mergeOp.opDivideTarget
              destBytes(targetPos) = (destBytes(targetPos)) / (sourceBytes(sourcePos) + 1)

            Case mergeOp.opReplace
              destBytes(targetPos) = sourceBytes(sourcePos)

            Case mergeOp.opReplaceNonzero
              If sourceBytes(sourcePos) <> 0 Then destBytes(targetPos) = (destBytes(targetPos) And (Not sourceBytes(sourcePos))) + sourceBytes(sourcePos)

          End Select
          sourcePos += 1
          targetPos += 1
        Next k
      Next ixDest

      targetOffset += rowLenTarget
      sourceOffset += rowLenSource
    Next iyDest
    milli(5) = clock.ElapsedMilliseconds : clock.Stop() : clock.Reset() : clock.Start()

    setBmpBytes(bmpTarget, destBytes) ' copy the final bytes
    milli(6) = clock.ElapsedMilliseconds : clock.Stop() : clock.Reset() : clock.Start()

  End Sub

  '================================================
  ' This is a very stupid way to sort a listview!
  Class lvSort
    Implements IComparer

    Dim uColumn As Integer
    Dim uDataType As Integer
    Dim uSortOrder As Integer

    Dim s1, s2 As String
    Dim x1, x2 As Double
    Dim d1, d2 As DateTime
    Dim zeroDate As DateTime = Date.FromBinary(0)

    Property sortkey() As Integer
      Set(ByVal value As Integer)
        uColumn = value
      End Set
      Get
        sortkey = uColumn
      End Get
    End Property

    Property sortOrder() As Integer
      ' -1 = descending, 1 = ascending
      Set(ByVal value As Integer)
        uSortOrder = value
      End Set
      Get
        sortOrder = uSortOrder
      End Get
    End Property

    Property dataType() As String
      Set(ByVal value As String)
        Select Case Trim(LCase(value))
          Case "number"
            uDataType = 1
          Case "date"
            uDataType = 2
          Case Else ' string
            uDataType = 0
        End Select
      End Set
      Get
        Select Case uDataType
          Case 1
            dataType = "number"
          Case 2
            dataType = "date"
          Case Else
            dataType = "string"
        End Select
      End Get
    End Property

    Public Sub New()
      uColumn = 0
      uDataType = 0 ' string
      uSortOrder = 1 ' ascending
    End Sub

    Public Sub New(ByVal column As Integer)
      uColumn = column
      uDataType = 0 ' string
      uSortOrder = 1 ' ascending
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
    Implements IComparer.Compare

      If CType(x, ListViewItem).SubItems.Count - 1 < uColumn Or uColumn < 0 Then Return 0
      s1 = CType(x, ListViewItem).SubItems(uColumn).Text
      s2 = CType(y, ListViewItem).SubItems(uColumn).Text

      Select Case uDataType
        Case 0
          Compare = String.Compare(s1, s2, True)
        Case 1 ' number, but there may be KB or some units on the string
          s1 = s1.Split(" ")(0)
          s2 = s2.Split(" ")(0)
          x1 = CDbl(s1) : x2 = CDbl(s2)
          If x1 < x2 Then Compare = -1 Else If x1 > x2 Then Compare = 1 Else Compare = 0
        Case 2
          If Trim(s1) = "" Then
            d1 = zeroDate
          Else
            Try
              d1 = CDate(s1)
            Catch
              d1 = zeroDate
            End Try
          End If

          If Trim(s2) = "" Then
            d2 = zeroDate
          Else
            Try
              d2 = CDate(s2)
            Catch
              d2 = zeroDate
            End Try
          End If
          If d1 < d2 Then Compare = -1 Else If d1 > d2 Then Compare = 1 Else Compare = 0

        Case Else
          Compare = 0
      End Select

      If uSortOrder < 0 Then Compare = -Compare

    End Function

  End Class

  Function validemail(ByVal s As String) As Boolean

    Dim c As String
    Dim i As Integer
    Dim k As Integer
    Dim state As Integer

    Dim it(4, 7) As Integer

    '        start               body             dot                  at         body2            dot          body3
    it(1, 1) = 2 : it(1, 2) = 2 : it(1, 3) = 2 : it(1, 4) = 5 : it(1, 5) = 5 : it(1, 6) = 7 : it(1, 7) = 7 ' alpha
    it(2, 1) = 0 : it(2, 2) = 3 : it(2, 3) = 0 : it(2, 4) = 0 : it(2, 5) = 6 : it(2, 6) = 0 : it(2, 7) = 6 ' dot
    it(3, 1) = 0 : it(3, 2) = 4 : it(3, 3) = 0 : it(3, 4) = 0 : it(3, 5) = 0 : it(3, 6) = 0 : it(3, 7) = 0 ' at
    it(4, 1) = 0 : it(4, 2) = 0 : it(4, 3) = 0 : it(4, 4) = 0 : it(4, 5) = 0 : it(4, 6) = 0 : it(4, 7) = -1 ' eof

    state = 1
    i = 0

    Do While i <= Len(s)
      i = i + 1
      If i > Len(s) Then
        k = 4
      Else
        c = LCase(Mid(s, i, 1))
        If c = "." Then
          k = 2
        ElseIf c = "@" Then
          k = 3
        ElseIf InStr("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890~-!$%^*-_=+[]{}|<>", c) > 0 Then
          k = 1 ' alpha
          c = ".:"
        End If
      End If
      state = it(k, state)
      If state <= 0 Then Exit Do
    Loop

    If state = -1 Then validemail = True Else validemail = False

  End Function

  Function sendMail(fromAddress As String, toAddress As String,
                    emailHost As String, emailPort As String, emailAccount As String, emailPassword As String,
                    body As String, subject As String, fromName As String, fNames As List(Of String)) As String

    ' mailer amd mailMsg as global
    ' sends async, uses handler

    Dim pic As Attachment

    Try
      mailMsg = New MailMessage(New MailAddress(fromAddress, fromName), New MailAddress(toAddress))
      Mailer = New SmtpClient
      mailMsg.BodyEncoding = System.Text.Encoding.UTF8
      mailMsg.Subject = subject
      mailMsg.Body = body
      mailMsg.Priority = MailPriority.Normal
      mailMsg.IsBodyHtml = False

      If fNames IsNot Nothing Then
        For Each fName As String In fNames
          If LCase(Path.GetExtension(fName)) = ".jpg" Or LCase(Path.GetExtension(fName)) = ".jpeg" Then
            pic = New Attachment(fName, MediaTypeNames.Image.Jpeg)
          Else
            pic = New Attachment(fName, MediaTypeNames.Application.Octet)
          End If
          pic.Name = Path.GetFileName(fName)
          pic.ContentDisposition.CreationDate = File.GetCreationTime(fName)
          pic.ContentDisposition.ModificationDate = File.GetLastWriteTime(fName)
          mailMsg.Attachments.Add(pic)
        Next fName
      End If

      Mailer.EnableSsl = False
      Mailer.Host = emailHost
      Mailer.Port = emailPort
      Mailer.Credentials = New System.Net.NetworkCredential(emailAccount, emailPassword)
      'mailer.Send(msg)
      AddHandler Mailer.SendCompleted, AddressOf SendFinished
      Mailer.SendAsync(mailMsg, "sendMail")

    Catch ex As Exception
      Return ex.Message
    End Try

    Return ""

  End Function

  Sub SendFinished(ByVal sender As Object, ByVal e As AsyncCompletedEventArgs)

    Dim msg As String = ""

    If e.Error IsNot Nothing Then
      msg = "Email Error: " & e.Error.Message
      If e.Cancelled Then msg &= vbCrLf & "Email was canceled."
      MsgBox(msg)
    End If

  End Sub

  Function makeMessage(ByVal msg As String) As String
    Dim s As String
    s = "<div Style = ""text-size:12; color:black; background-color:yellow; padding:5pt; border:solid 1px black; font-weight:bold; text-align:center"">"
    s += msg & "</div><br>"
    Return s
  End Function

  Function readPropertyItems(fName As String) As List(Of PropertyItem)

    ' read a list of propertyitems from a bitmap file, using windows .PropertyItems

    Dim p As New List(Of PropertyItem)

    Try
      Using iStream As New FileStream(fName, FileMode.Open, FileAccess.Read), _
        bmp As Bitmap = Image.FromStream(iStream, True, False)

        For Each prop As PropertyItem In bmp.PropertyItems
          p.Add(prop)
        Next prop

      End Using
    Catch ex As Exception
      'MsgBox(ex.Message)
      Return New List(Of PropertyItem)
    End Try

    Return p

  End Function

  Sub attachPropertyItems(bmp As Bitmap, propertyItems As List(Of PropertyItem))
    ' copy the list of propertyitems to the bitmap

    For Each prop As PropertyItem In bmp.PropertyItems
      propertyItems.Remove(prop)
    Next prop

    Try
      For Each prop As PropertyItem In propertyItems
        If prop.Id = propID.ThumbnailData Then updateThumbnail(prop, bmp) ' slow - 90+ ms
        If prop.Id = propID.MakerNote Then

        End If

        bmp.SetPropertyItem(prop)
      Next prop
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try
  End Sub

  Sub updateThumbnail(prop As PropertyItem, bmp As Bitmap)
    ' update the thumbnail in the bitmap's exif data
    Dim thumb As Bitmap = Nothing
    Dim pView As pViewer
    Dim bb() As Byte

    pView = New pViewer
    pView.InterpolationMode = InterpolationMode.High
    pView.ResizeBitmap(160, 160, bmp, thumb)

    Using mStream As New MemoryStream()
      thumb.Save(mStream, System.Drawing.Imaging.ImageFormat.Jpeg)
      bb = mStream.ToArray
    End Using

    prop.Value = bb
    prop.Len = bb.Length

  End Sub

  Sub setBmpComment(id As propID, pComments As List(Of PropertyItem), value As Object, type As exifType)
    ' convert value to byte array, regardless of type. Strings need a terminating zero.

    Dim prop As PropertyItem = Nothing
    Dim bb() As Byte
    Dim s As String
    Dim i, n As Integer

    If pComments.Count <= 0 Then Exit Sub

    If TypeOf value Is String Then
      s = value
      bb = System.Text.Encoding.UTF8.GetBytes(s)
      i = UBound(bb) + 1
      ReDim Preserve bb(i)
      bb(i) = 0
    Else
      If TypeOf value Is Double Or TypeOf value Is Double() Then value = getRational(value) ' convert to integer array
      If IsArray(value) Then n = Len(value(0)) * (UBound(value) + 1) Else n = Len(value)
      ReDim bb(n - 1)
      System.Buffer.BlockCopy(value, 0, bb, 0, n) ' copy value to bb, n bytes
    End If

    For i = 0 To pComments.Count - 1
      If pComments(i).Id = id Then ' update the existing item
        pComments(i).Len = bb.Length
        pComments(i).Value = bb.Clone
        pComments(i).Type = type
        Exit Sub
      End If
    Next i

    ' item not found -- create one
    Using bmpx As New Bitmap(1, 1) ' this is stupid, but there's no "new"
      bmpx.SetPropertyItem(pComments(0))
      prop = bmpx.GetPropertyItem(pComments(0).Id)
    End Using

    prop.Id = id
    prop.Len = bb.Length
    prop.Value = bb.Clone
    prop.Type = type
    pComments.Add(prop)

  End Sub

  Sub showHisto(pView As pViewer, ByRef hist(,) As Integer, ByRef histXscale As Double, ByRef histYscale As Double)
    ' draws the histograms in the pictureboxes.

    Static busy As Boolean = False

    Dim k As Integer
    Dim maxH As Integer
    Dim RP(257) As PointF
    Dim bmp1 As Bitmap


    If busy Then Exit Sub
    busy = True

    ' If getScale Then getHistoScale(bmp, picBox)
    maxH = 1
    For j As Integer = 0 To 255
      k = Max(Max(hist(0, j), hist(1, j)), hist(2, j))
      If k > maxH Then maxH = k
    Next j

    histYscale = pView.ClientSize.Height / maxH
    histXscale = pView.ClientSize.Width / 255

    'For j As Integer = 0 To 255
    ' k = (hist(0, j) + hist(1, j) + hist(2, j)) \ 3
    ' RP(j).X = j * histXscale
    ' RP(j).Y = PicBox.ClientSize.Height - (k * histYscale)
    ' Next j

    bmp1 = New Bitmap(pView.Width, pView.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
    Using g As Graphics = Graphics.FromImage(bmp1)
      g.Clear(Color.Black)
    End Using

    For i As Integer = 0 To 2
      For j As Integer = 0 To 255
        k = hist(i, j)
        RP(j).X = j * histXscale
        RP(j).Y = pView.ClientSize.Height - (k * histYscale)
      Next j
      Using bmp2 As Bitmap = New Bitmap(pView.Width, pView.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
        If i = 0 Then redrawHisto(bmp2, RP, Color.FromArgb(255, 0, 0))
        If i = 1 Then redrawHisto(bmp2, RP, Color.FromArgb(0, 255, 0))
        If i = 2 Then redrawHisto(bmp2, RP, Color.FromArgb(0, 0, 255))
        bmpMerge(bmp2, bmp1, mergeOp.opAdd, New Rectangle(0, 0, bmp2.Width, bmp2.Height))
      End Using
    Next i

    pView.Image = bmp1
    pView.Invalidate()

    busy = False

  End Sub

  Sub redrawHisto(bmp As Bitmap, rp() As PointF, col As Color)

    rp(256).X = rp(255).X
    rp(256).Y = bmp.Height
    rp(257).X = rp(0).X
    rp(257).Y = rp(256).Y

    Using g As Graphics = Graphics.FromImage(bmp),
      gBrush As New solidbrush(col)
      g.SmoothingMode = SmoothingMode.HighQuality
      g.PixelOffsetMode = PixelOffsetMode.HighQuality
      g.Clear(Color.Black)
      g.FillPolygon(gBrush, rp)
    End Using

  End Sub



  Function getRational(value As Object) As Integer()
    ' returns an array of numerators and denominators that match the double(s) "value".
    Dim rational() As Integer
    Dim ii(1) As Integer

    If IsArray(value) Then
      ReDim rational(UBound(value) * 2 + 1)
      For j As Integer = 0 To UBound(value)
        ii = getSingleRational(value(j))
        rational(j * 2) = ii(0)
        rational(j * 2 + 1) = ii(1)
      Next j

    Else ' scalar
      rational = getSingleRational(value)
    End If

    Return rational

  End Function

  Function getSingleRational(x As Double) As Integer()

    Dim k As Integer
    Dim rational(1) As Integer

    Try
      If x < 0 Then x = -x
      If x > 0 Then k = 9 - Ceiling(Log10(x)) Else k = 0
      If k < 0 Then k = 0 Else If k > 9 Then k = 9
      k = 10 ^ k
      rational(1) = k
      rational(0) = x * k
    Catch ex As Exception
      MsgBox("Error in getSingleRational: " & ex.Message & " - " & x)
      rational(0) = 0
    End Try

    Return rational

  End Function


  Function getBmpComment(id As propID, pComments As List(Of PropertyItem)) As Object
    ' returns an array except for strings
    Dim v As Object
    Dim tagName As String = ""
    Dim datatype As Integer
    Dim count As Integer

    For Each prop As PropertyItem In pComments
      If prop.Id = id Then
        v = getTagValue(prop)
        If prop.Type = 2 Then ' string - return single string 
          If v Is Nothing OrElse UBound(v) < 0 Then Return ""
          Return v(0)
        Else
          Return v
        End If
      End If
    Next prop

    TagInfo(id, tagName, datatype, count)
    If datatype = 2 Then
      Return "" ' return empty string if ascii, rather than nothing.
    Else
      Return Nothing
    End If

  End Function


  Function getTagValue(prop As PropertyItem) As Object
    ' converts prop.value to the appropriate data type.
    ' always returns an array, single values in a single-element array.

    Dim bb() As Byte
    Dim dSigned As Boolean
    Dim vs() As String
    Dim vx() As Double
    Dim vn() As UInteger
    Dim vnSigned() As Integer
    Dim vw() As UShort
    Dim intel As Boolean = True
    Dim ii1, ii2 As Int64

    bb = prop.Value.Clone

    If prop.Type = 8 Or prop.Type = 9 Or prop.Type = 10 Then dSigned = True Else dSigned = False

    Select Case prop.Type
      Case 1, 6, 7 ' byte, sbyte, undefined
        Return bb

      Case 2 ' ascii (string)
        ReDim vs(0) ' always a single string, separate multiples by 0
        vs(0) = UTF8bare.GetString(bb, 0, UBound(bb) + 1)
        vs(0) = vs(0).Trim(whiteSpace)
        Return vs

      Case 3, 8 ' unsigned short (signed short is not defined)
        ReDim vw(prop.Len \ 2 - 1)
        For i As Integer = 0 To prop.Len \ 2 - 1
          vw(i) = getWord(bb, i * 2, intel)
        Next i
        Return vw

      Case 4 ' unsigned int32
        ReDim vn(prop.Len \ 4 - 1)
        For i As Integer = 0 To prop.Len \ 4 - 1
          vn(i) = getDWord(bb, i * 2, intel)
        Next i
        Return vn

      Case 9 ' int32, signed in32
        ReDim vnSigned(prop.Len \ 4 - 1)
        For i As Integer = 0 To prop.Len \ 4 - 1
          vnSigned(i) = getDWordSigned(bb, i * 2, intel)
        Next i
        Return vnSigned

      Case 5, 10 ' rational, srational
        ReDim vx(prop.Len \ 8 - 1)
        For i As Integer = 0 To prop.Len \ 8 - 1
          If dSigned Then
            ii1 = getDWordSigned(bb, i * 8, intel)
            ii2 = getDWordSigned(bb, i * 8 + 4, intel)
          Else
            ii1 = getDWord(bb, i * 8, intel)
            If ii1 > 2 ^ 31 + 2 ^ 30 Then ii1 = ii1 - 2 ^ 32 '  this is probably because someone (like GPSStamp) got the signs wrong. It's technically incorrect.
            ii2 = getDWord(bb, i * 8 + 4, intel)
          End If
          If ii2 <> 0 Then vx(i) = ii1 / ii2 Else vx(i) = 0
        Next i
        Return vx

    End Select

    Return Nothing

  End Function

  Function getDWord(ByRef b() As Byte, ByRef i As Integer, ByRef intel As Boolean) As UInteger

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
    End If

  End Function

  Function getDWordSigned(ByRef b() As Byte, ByRef i As Integer, ByRef intel As Boolean) As Integer

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
    End If

  End Function

  Function getWordSigned(ByRef b() As Byte, ByRef i As Integer, ByRef intel As Boolean) As Short

    Dim k As Integer = 0

    If intel Then
      k = b(i + 1)
      k = (k << 8) + b(i)
      Return k
    Else
      k = b(i) * 256
      k = k + b(i + 1)
      Return k
    End If

    'If k >= 32768 Then k = k - 65536

  End Function

  Function getWord(ByRef b() As Byte, ByRef i As Integer, ByRef intel As Boolean) As UShort

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

  Function alphaToPath(bmp As Bitmap) As GraphicsPath

    Dim bb() As Byte
    Dim pp As List(Of Point)
    Dim p As Point
    Dim gPath As New GraphicsPath

    bb = getBmpBytes(bmp, 4) ' get alpha channel bytes
    bb = get4Edges(bb, bmp.Width, bmp.Height)

    p = nextdot(bb, New Point(0, 0), bmp.Width, bmp.Height)
    Do While p.X >= 0
      pp = follow8(bb, p, bmp.Width, bmp.Height)
      If pp.Count > 0 Then
        gPath.AddLines(pp.ToArray)
        gPath.CloseFigure()
      End If
      p = nextdot(bb, New Point(0, 0), bmp.Width, bmp.Height)
    Loop
    'setBmpBytes(bmp, bb, 4)
    'bmp.Save("c:\tmp\tmp.png")

    Return gPath
  End Function

  Function get4Edges(bb() As Byte, w As Integer, h As Integer) As Byte()
    ' clears every pixel that is not zero and doesn't have a 0 or 4 4-neighbors
    Dim b4(UBound(bb)) As Byte
    Dim qbyte As Integer
    Dim b As Byte
    Dim i As Integer

    For ix As Integer = 0 To w - 1
      For iy As Integer = 0 To h - 1
        qbyte = iy * w + ix
        If bb(qbyte) <> 0 Then
          b4(qbyte) = 255
        Else
          i = get4Neighbor(bb, New Point(ix, iy), w, h, b)
          If b = 0 Or b = 4 Then b4(qbyte) = 255 Else b4(qbyte) = 0
        End If
      Next iy
    Next ix

    Return b4

  End Function

  Function get4Neighbor(bb() As Byte, p As Point, w As Integer, h As Integer,
                        ByRef neighbors As Byte) As Integer
    ' returns number of 4-neighbors
    ' returns the 4-neighbor value (non-zero neighbors) in "neighbor", 1-top, 2-right, 4-bottom, 8-left
    ' beyond edges = non-zero pixels
    ' get4Neighbor recognizes non-zero, get8Neighbor recognizes zeros
    Dim qbyte As Integer
    Dim count As Integer = 0

    neighbors = 0
    qbyte = p.Y * w + p.X

    neighbors = 0
    If p.Y > 0 AndAlso bb(qbyte - w) <> 0 Then
      neighbors += 1 ' top
      count += 1
    End If
    If p.X < w - 1 AndAlso bb(qbyte + 1) <> 0 Then
      neighbors += 2 ' right
      count += 1
    End If
    If p.Y < h - 1 AndAlso bb(qbyte + w) <> 0 Then
      neighbors += 4 ' bottom
      count += 1
    End If
    If p.X > 0 AndAlso bb(qbyte - 1) <> 0 Then
      neighbors += 8 ' left
      count += 1
    End If
    Return count
  End Function

  Function get8Neighbor(bb() As Byte, p As Point, w As Integer, h As Integer,
                        ByRef neighbors As Byte) As Integer
    ' returns number of 8-neighbors (zero values)
    ' returns the 8-neighbor value (zero neighbors) in "neighbor",
    '     1-top, 2-right, 4-bottom, 8-left, 16 UL, 32 UR, 64 BR, 128 BL
    ' beyond edges = zero pixels
    ' get4Neighbor recognizes non-zero, get8Neighbor recognizes zeros
    Dim count As Integer = 0
    Dim qbyte As Integer

    neighbors = 0
    qbyte = p.Y * w + p.X

    If p.Y > 0 AndAlso bb(qbyte - w) = 0 Then
      neighbors += 1 ' top
      count += 1
    End If
    If p.X < w - 1 AndAlso bb(qbyte + 1) = 0 Then
      neighbors += 2 ' right
      count += 1
    End If
    If p.Y < h - 1 AndAlso bb(qbyte + w) = 0 Then
      neighbors += 4 ' bottom
      count += 1
    End If
    If p.X > 0 AndAlso bb(qbyte - 1) = 0 Then
      neighbors += 8 ' left
      count += 1
    End If

    If p.X > 0 AndAlso p.Y > 0 AndAlso bb(qbyte - 1 - w) = 0 Then
      neighbors += 16 ' UL
      count += 1
    End If
    If p.X < w - 1 AndAlso p.Y > 0 AndAlso bb(qbyte + 1 - w) = 0 Then
      neighbors += 32 ' UR
      count += 1
    End If
    If p.X < w - 1 AndAlso p.Y < h - 1 AndAlso bb(qbyte + 1 + w) = 0 Then
      neighbors += 64 ' BR
      count += 1
    End If
    If p.X > 0 AndAlso p.Y < h - 1 AndAlso bb(qbyte - 1 + w) = 0 Then
      neighbors += 128 ' BL
      count += 1
    End If

    Return count
  End Function

  Function nextdot(bb() As Byte, p1 As Point, w As Integer, h As Integer) As Point
    ' returns the next zero in bb(), starts search at p1
    For iy As Integer = p1.Y To h - 1
      For ix As Integer = p1.X To w - 1
        If bb(ix + iy * w) = 0 Then Return New Point(ix, iy)
      Next ix
    Next iy
    Return New Point(-1, -1) ' finished

  End Function

  Function follow8(ByRef bb() As Byte, p1 As Point, w As Integer, h As Integer) As List(Of Point)
    ' follow the zero points, return in list
    Dim neighbors As Byte
    Dim n As Integer
    Dim pp As New List(Of Point)
    Dim p As Point
    Dim deletedPrevious As Boolean

    n = get8Neighbor(bb, p1, w, h, neighbors)
    If n = 0 Then
      bb(p1.X + p1.Y * w) = 255 ' turn on (reset) point if it's got no neighbors
      Return pp
    End If
    If 1 = 1 Then
      bb(p1.X + p1.Y * w) = 255 ' turn on (reset) point if it's only got one neighbor
      deletedPrevious = True
    Else
      deletedPrevious = False
    End If
    pp.Add(p1)
    p = pickNeighbor(p1, neighbors) ' get an arbitrary neighbor

    Do While p.X >= 0
      n = get8Neighbor(bb, p, w, h, neighbors)
      If deletedPrevious Then
        n += 1
        deletedPrevious = False
      End If
      pp.Add(p)
      If n = 2 Then
        bb(p.X + p.Y * w) = 255 ' turn on (reset) point if it's only got one neighbor
        deletedPrevious = True
      Else
        Return pp
      End If
      p = pickNeighbor(p, neighbors) ' get an arbitrary neighbor
    Loop

    Return pp

  End Function

  Function pickNeighbor(p1 As Point, neighbors As Byte) As Point
    ' return an arbitrary neighbor
    If neighbors And 1 Then Return New Point(p1.X, p1.Y - 1) ' T
    If neighbors And 2 Then Return New Point(p1.X + 1, p1.Y) ' R
    If neighbors And 4 Then Return New Point(p1.X, p1.Y + 1) ' B
    If neighbors And 8 Then Return New Point(p1.X - 1, p1.Y) ' L
    If neighbors And 16 Then Return New Point(p1.X - 1, p1.Y - 1) ' UL
    If neighbors And 32 Then Return New Point(p1.X + 1, p1.Y - 1) ' UR
    If neighbors And 64 Then Return New Point(p1.X + 1, p1.Y + 1) ' BR
    If neighbors And 128 Then Return New Point(p1.X - 1, p1.Y + 1) ' BL
    Return New Point(-1, -1)
  End Function


  Sub shuffle(ByRef ix As List(Of Integer), Optional seed As Integer = -1)

    Dim n As Integer = ix.Count - 1
    Dim ix1 As New List(Of Integer)
    Dim iy1 As New List(Of Integer)
    Dim i, k As Integer

    Dim rand As Random
    If seed <> -1 Then rand = New Random(seed) Else rand = New Random

    For i = 0 To n
      iy1.Add(i)
    Next i

    For i = 0 To n
      k = rand.Next(0, iy1.Count)
      ix1.Add(ix(iy1(k)))
      iy1.RemoveAt(k)
    Next i

    ix = New List(Of Integer)
    ix.AddRange(ix1)

  End Sub

  Function getFont(zoom As Double, fontName As String, fontSize As Integer, fBold As Boolean,
                   fItalic As Boolean, fUnderline As Boolean) As Font

    Dim style As Integer

    If fontSize <= 0 Then fontSize = 36

    style = 0
    If fBold Then style = style Or FontStyle.Bold
    If fItalic Then style = style Or FontStyle.Italic
    If fUnderline Then style = style Or FontStyle.Underline
    Try
      Using gfont As Font = New Font(fontName, fontSize * zoom, style, GraphicsUnit.Pixel)
        Return gfont.Clone
      End Using
    Catch ex As Exception
      MsgBox(ex.Message)
    End Try

    Return Nothing

  End Function



End Module

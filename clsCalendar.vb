' history: military, science, math, political, US, countries, mountaineering, aerospace, astronomy, tides
' birthdays: major, music, math, science, political, military, aerospace, mountaineering, world peace, nobel winners, nature, etc.

Imports vb = Microsoft.VisualBasic
Imports System.Runtime.InteropServices
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Globalization
Imports System.Math
Imports System.IO

Public Class clsCalendar
Implements IDisposable

Dim uCategories As String
Dim startDay, nWeeks As Integer
Dim cal As Calendar = CultureInfo.CurrentCulture.Calendar
Dim gFont As Font
Dim gBrush As SolidBrush
Dim xSize, ySize As Single
Dim calComment(366) As String
Dim lunarComment(366) As String

Dim gPen As Pen

Property Categories() As String
' categories, separated by a space
Get
  Categories = uCategories
End Get
Set(ByVal value As String)
  uCategories = value
End Set
End Property

Public Sub showMonth(ByRef g As Graphics, ByRef startDate As Date, ByRef rBox As RectangleF, ByRef calTitle As String, _
  ByRef fontname As String)

Dim i, j, k As Integer
Dim iday, preday, postday As Integer
Dim x, y As Single
Dim xs, ys As Single
Dim ix, iy As Single
Dim s As String
Dim n As Integer
Dim d As DateTime
Dim r As RectangleF
Dim tsizeTitle As Single
Dim tsizeHeader As Single
Dim tsizeFooter As Single
'Dim tsizeCaption As Single
Dim tsizeNumbers As Single
Dim tsizeSmallMonths As Single
Dim tsizeLunar As Single
Dim tsizeNotes As Single
Dim fmt As New StringFormat
Dim dayNames() As String = {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"}
Dim tScale As Single

Call getHolidays(startDate.Year) ' read holidays for the year

gPen = New Pen(Color.Gray, 0)
g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias  ' not sure which of these works
g.SmoothingMode = SmoothingMode.AntiAlias  ' not sure which of these works
g.PixelOffsetMode = PixelOffsetMode.HighQuality  ' not sure which of these works

n = cal.GetDaysInMonth(startDate.Year, startDate.Month)
startDay = Weekday(New Date(startDate.Year, startDate.Month, 1)) - 1  ' day of week (0-6) of the first day of the month
nWeeks = Ceiling((n + startDay) / 7)
' If nWeeks = 4 Then nWeeks = nWeeks + 1

xSize = rBox.Width / 7
ySize = rBox.Height / (nWeeks + 1.5) ' add another for the title line and half for the footer
tScale = Max(rBox.Width, rBox.Height)

' text sizes, pixels
tsizeTitle = 6.45 * tScale / 100
tsizeHeader = 1.5 * tScale / 100
tsizeFooter = 3.0 * tScale / 100
'tsizeCaption = 1.2 * tScale / 100
tsizeNumbers = 2.2 * tScale / 100
tsizeSmallMonths = 1.0 * tScale / 100
tsizeLunar = 0.9 * tScale / 100
tsizeNotes = 0.9 * tScale / 100

'r.Width = xSize
'r.Height = ySize
'If startDay >= 2 Then ' add small months at the beginning
'  r.X = rBox.X : r.Y = rBox.Y
'Else ' add the small months at the end of the month
'  r.X = rBox.X + xSize * 5 : r.Y = rBox.Y + (nWeeks - 1) * ySize
'  End If
'Call showSmallMonth(g, cal.AddMonths(startDate, -1), r)
'r.X = r.X + xSize
'Call showSmallMonth(g, cal.AddMonths(startDate, 1), r)

gFont = New Font(fontname, tsizeTitle, FontStyle.Italic, GraphicsUnit.Pixel, CharSet.Unicode)
gBrush = New SolidBrush(Color.Gray)
fmt.LineAlignment = StringAlignment.Center
fmt.Alignment = StringAlignment.Center
g.DrawString(Format(startDate, "MMMM"), gFont, gBrush, New RectangleF(rBox.X, rBox.Y + ySize * 0.05, rBox.Width, ySize * 0.8), fmt)

'gFont = New Font(fontname, tsizeCaption, FontStyle.Regular, GraphicsUnit.Pixel, CharSet.Unicode)
'gBrush = New SolidBrush(Color.Gray)
'fmt.LineAlignment = StringAlignment.Center
'fmt.Alignment = StringAlignment.Center
'g.DrawString(calCaption, gFont, gBrush, New RectangleF(rBox.X, rBox.Y, rBox.Width, tsizeCaption), fmt)

gFont = New Font(fontname, tsizeFooter, FontStyle.Regular, GraphicsUnit.Pixel, CharSet.Unicode)
gBrush = New SolidBrush(Color.Gray)
fmt.LineAlignment = StringAlignment.Center
fmt.Alignment = StringAlignment.Center
g.DrawString(calTitle, gFont, gBrush, New RectangleF(rBox.X, rBox.Y + rBox.Height - ySize * 0.5, rBox.Width, ySize * 0.5), fmt)

' add small months at the top left and top right
gFont = New Font(fontname, tsizeSmallMonths, FontStyle.Regular, GraphicsUnit.Pixel, CharSet.Unicode)
gBrush = New SolidBrush(Color.Gray)
r.Width = xSize * 0.8
r.Height = ySize * 0.65
r.X = rBox.X + ySize * 0.1 : r.Y = rBox.Y
Call showSmallMonth(g, cal.AddMonths(startDate, -1), r)
r.X = rBox.X + rBox.Width - r.Width - ySize * 0.1
Call showSmallMonth(g, cal.AddMonths(startDate, 1), r)

' horizontal lines
For i = 0 To nWeeks
' For i = rBox.Y + ySize To rBox.Y + rBox.Y + rBox.Height - ySize * 0.5 Step ySize
  iy = rBox.Y + ySize + i * ySize
  g.DrawLine(gPen, rBox.X, iy, rBox.X + rBox.Width, iy)
  Next i

' vertical lines
For i = 1 To 6 ' skip 0 and 7
' For i = rBox.X + xSize To rBox.X + rBox.Width - xSize Step xSize
  ' iy = rBox.Y + ySize - tsizeHeader * ySize
  ix = rBox.X + i * xSize
  g.DrawLine(gPen, ix, rBox.Y + ySize - tsizeHeader, ix, rBox.Y + ySize + nWeeks * ySize)
  Next i

gFont = New Font(fontname, tsizeHeader, FontStyle.Italic, GraphicsUnit.Pixel, CharSet.Unicode)
fmt.LineAlignment = StringAlignment.Center
fmt.Alignment = StringAlignment.Center
gBrush = New SolidBrush(Color.Black)
For i = 0 To 6
  ix = rBox.X + i * xSize
  iy = rBox.Y + ySize - tsizeHeader * 1.2
  g.DrawString(dayNames(i), gFont, gBrush, New RectangleF(ix, iy, xSize, tsizeHeader), fmt)
  Next i

gFont = New Font(fontname, tsizeNumbers, FontStyle.Italic, GraphicsUnit.Pixel, CharSet.Unicode)
iday = 1
preday = cal.GetDaysInMonth(startDate.Year, startDate.Month) - startDay + 1
postday = 1
ix = rBox.X + tsizeNumbers * 0.2   ' upper left corner of the first number
iy = rBox.Y + ySize + tsizeNumbers * 0.2  ' upper left corner
For i = 0 To nWeeks - 1

  For j = 0 To 6
    If i * 7 + j >= startDay And iday <= n Then
      s = iday
      x = ix + xSize * j
      y = iy + ySize * i
      gBrush = New SolidBrush(Color.Black)
      g.DrawString(s, gFont, gBrush, x, y)
      iday = iday + 1

    ElseIf iday > n Then ' add postday - next month
      s = postday
      x = ix + xSize * j
      y = iy + ySize * i
      gBrush = New SolidBrush(Color.LightGray)
      g.DrawString(s, gFont, gBrush, x, y)
      postday = postday + 1

    Else ' add preday - last month
      s = preday
      x = ix + xSize * j
      y = iy + ySize * i
      gBrush = New SolidBrush(Color.LightGray)
      g.DrawString(s, gFont, gBrush, x, y)
      preday = preday + 1
      End If
    Next j
  If iday > n Then Exit For
  Next i

gFont = New Font(fontname, tsizeLunar, FontStyle.Italic, GraphicsUnit.Pixel, CharSet.Unicode)
gBrush = New SolidBrush(Color.Black)
fmt.LineAlignment = StringAlignment.Near
fmt.Alignment = StringAlignment.Near
fmt.Trimming = StringTrimming.EllipsisCharacter
' lunar comments, upper lines
ix = rBox.X + tsizeLunar + tsizeNumbers * 1.1  ' upper left corner of the lunar comments
iy = rBox.Y + ySize + tsizeLunar  ' upper left corner
xs = xSize - (tsizeLunar + tsizeNumbers * 1.1)
ys = tsizeLunar * 4
d = New Date(startDate.Year, startDate.Month, 1)
k = d.DayOfYear
For j = 1 To DateTime.DaysInMonth(d.Year, d.Month)
      If lunarComment(k) <> "" Then
        i = j + startDay - 1 ' day of year
        x = ix + xSize * (i Mod 7)
        y = iy + ySize * Int(i / 7)
        r = New Rectangle(x, y, xs, ys)
        g.DrawString(lunarComment(k), gFont, gBrush, r, fmt)
      End If
      k = k + 1
    Next j

    gFont = New Font(fontname, tsizeNotes, FontStyle.Regular, GraphicsUnit.Pixel, CharSet.Unicode)
    gBrush = New SolidBrush(Color.Black)
    fmt.LineAlignment = StringAlignment.Far
    fmt.Alignment = StringAlignment.Near
    ' notes, on the bottom
    ix = rBox.X + tsizeNotes / 2 ' upper left corner of the notes
    iy = rBox.Y + ySize + tsizeNumbers * 1.13 ' upper left corner
    xs = xSize - tsizeNotes
    ys = ySize - (tsizeNotes + tsizeNumbers * 1.1)
    d = New Date(startDate.Year, startDate.Month, 1)
    k = d.DayOfYear
    For j = 1 To DateTime.DaysInMonth(d.Year, d.Month)
      If calComment(k) <> "" Then
        i = j + startDay - 1 ' day of year
        x = ix + xSize * (i Mod 7)
        y = iy + ySize * Int(i / 7)
        r = New Rectangle(x, y, xs, ys)
        g.DrawString(calComment(k), gFont, gBrush, r, fmt)
      End If
      k = k + 1
    Next j

End Sub

Public Sub showSmallMonth(ByRef g As Graphics, ByVal startDate As Date, ByVal rBox As RectangleF)

Dim i, j, iday As Integer
Dim ix, iy As Single
Dim s As String
Dim n As Integer
Dim xs, ys As Integer
Dim sDay As Integer
Dim nwk As Integer
Dim r As RectangleF
Dim fmt As New StringFormat

g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias  ' not sure which of these works
g.SmoothingMode = SmoothingMode.AntiAlias  ' not sure which of these works
g.PixelOffsetMode = PixelOffsetMode.HighQuality  ' not sure which of these works

n = cal.GetDaysInMonth(startDate.Year, startDate.Month)
sDay = Weekday(New Date(startDate.Year, startDate.Month, 1)) - 1
nwk = Ceiling((n + sDay) / 7)

fmt.LineAlignment = StringAlignment.Center
fmt.Alignment = StringAlignment.Center
xs = rBox.Width / 7
ys = rBox.Height / (nwk + 2)

r.X = rBox.X
r.Y = rBox.Y
r.Width = rBox.Width
r.Height = ys
g.DrawString(Format(startDate, "MMMM yyyy"), gFont, gBrush, r, fmt)

For j = 0 To 6
  s = "SMTWTFS".Chars(j)
  r.X = rBox.X + xs * j
  r.Y = rBox.Y + ys
  ix = rBox.X + xs * (j + 0.5)
  iy = rBox.Y + ys * 1.5
  g.DrawString(s, gFont, gBrush, ix, iy, fmt)
 Next j

iday = 1

For i = 0 To nwk - 1
  For j = 0 To 6
    If i * 7 + j >= sDay Then
      s = iday
      ix = rBox.X + xs * (j + 0.5)
      iy = rBox.Y + ys * (i + 2.5)
      g.DrawString(s, gFont, gBrush, ix, iy, fmt)
      iday = iday + 1
      If iday > n Then Exit For
      End If
    Next j
  If iday > n Then Exit For
  Next i

'iy = rBox.Y + ys * 0.95
'g.DrawLine(gPen, rBox.X, iy, rBox.X + rBox.Width, iy)
'iy = iy + ys
'g.DrawLine(gPen, rBox.X, iy, rBox.X + rBox.Width, iy)

End Sub

Private Sub saveDate()
' utility to convert the text file to binary. not used by users.

Dim d As Date
Dim s As String
Dim itype As Integer
Dim bt(4) As Byte ' type
Dim bd(8) As Byte ' date
Dim i64 As Long
Dim i As Integer
Dim bf(-1) As Byte
Dim ipos As Integer
Dim ss() As String
Dim iLine As Integer

ipos = 0

Try
  ss = File.ReadAllLines("c:\moon.txt")
  Catch
  ReDim ss(-1)
  End Try

For iLine = 0 To UBound(ss)
  s = ss(iLine)
  i = InStr(s, "'")
  If i > 0 Then s = Trim(Mid(s, 1, i - 1))
  If Len(s) > 0 Then
    itype = Trim(Mid(s, 1, 2))
    d = CDate(Mid(s, 3))

    bt = BitConverter.GetBytes(itype)
    i64 = d.ToBinary
    bd = BitConverter.GetBytes(i64)
    ReDim Preserve bf(ipos + 11)
    For i = 0 To 3
      bf(i + ipos) = bt(i)
      Next i
    For i = 0 To 7
      bf(i + ipos + 4) = bd(i)
      Next i
    ipos = ipos + 12

    End If
  Next iLine

File.WriteAllBytes("c:\moon.dat", bf)

End
End Sub

Private Sub getHolidays(ByVal iYear As Integer)
' read the .dat and holiday files, apply to this year.

Dim i, j, k As Integer
Dim iFile As Integer
Dim iLine As Integer
Dim category As String = ""
Dim d, dOrg As DateTime
Dim b() As Byte = Nothing
Dim i64 As Long
Dim s As String
Dim ss() As String = Nothing
Dim sn() As String
Dim iDay As Integer
Dim defaultCategory(20) As String
Dim fNames(20) As String
Dim nFiles As Integer

For i = 0 To UBound(calComment) : calComment(i) = "" : Next i
For i = 0 To UBound(lunarComment) : lunarComment(i) = "" : Next i

If InStr(uCategories, "lunar" & crlf) Then

  Try
  b = File.ReadAllBytes(exePath & "moon.dat")
  i = 0
    Catch
  i = -1
    End Try

  If i = 0 Then ' read lunar data
    For i = 0 To UBound(b) Step 12
      k = BitConverter.ToInt32(b, i)
      i64 = BitConverter.ToInt64(b, i + 4)
      d = DateTime.FromBinary(i64)
      If d.Year = iYear Then
        Select Case k
          Case 0
            s = "New Moon"
          Case 2
            s = "Full Moon"
          Case 4
            s = "Vernal Equinox"
          Case 5
            s = "Autumnal Equinox"
          Case 6
            s = "Summer Solstice"
          Case 7
            s = "Winter Solstice"
          Case 8
            s = "Solar Eclipse"
          Case 9
            s = "Lunar Eclipse"
          Case Else
            s = ""
          End Select

        If Len(s) > 0 Then
          k = d.DayOfYear
              If lunarComment(k) = "" Then
                lunarComment(k) = s
              Else
                lunarComment(k) = lunarComment(k) & crlf & s
              End If
            End If
          End If
        Next i
      End If
    End If ' lunar events

    fNames(1) = exePath & "holidays.dat"
    defaultCategory(1) = "general"
    fNames(2) = dataPath & customCalFile
    defaultCategory(2) = customCalCat
    nFiles = 2

    For iFile = 1 To nFiles
      If File.Exists(fNames(iFile)) Then
        Try
          ss = File.ReadAllLines(fNames(iFile))
          i = 0
        Catch ex As Exception
          MsgBox(ex.Message & crlf & fNames(iFile) & " could not be read.")
          i = -1
        End Try
        category = defaultCategory(iFile)
      Else
        ReDim ss(-1) ' file not found
      End If

      If i = 0 Then
        iLine = 0
        Do While iLine < UBound(ss)
          d = Nothing
          s = ss(iLine) : iLine = iLine + 1

          i = InStr(s, "'") ' comment marker
          If i > 0 Then s = Trim(Mid(s, 1, i - 1))
          s = Trim(s)
          If Len(s) > 0 Then
            sn = s.Split(" ")
            If IsNumeric(sn(0)) Then i = sn(0) Else i = 90000
            s = ss(iLine) : iLine = iLine + 1

            Select Case i ' get d (date) and s (description)
              Case -1 ' category
                category = LCase(Trim(s))
                d = Nothing

              Case 1 ' annual
                d = sn(1) & "/" & sn(2) & "/" & iYear

              Case 2 ' xth x-day of x month
                d = sn(3) & "/1/" & iYear ' first day of the month
                iDay = d.DayOfWeek
                i = sn(2) - iDay + 1 ' day-of-month of first x-day
                If i <= 0 Then i = i + 7
                i = i + (sn(1) - 1) * 7 ' add weeks 
                d = sn(3) & "/" & i & "/" & iYear

              Case 3 ' general date - date and description, annually displayed
                dOrg = CDate(s)
                d = CDate(dOrg.Month & "/" & dOrg.Day & "/" & iYear)
                s = ss(iLine) : iLine = iLine + 1
                processString(d, dOrg, s)

              Case 4 ' general date - date and description, displayed only in the year specified
                d = CDate(s)
                s = ss(iLine) : iLine = iLine + 1

              Case 5 ' relative to easter
                d = easter(iYear)
                k = sn(1)
                d = d.Add(TimeSpan.FromDays(k))

              Case 6 ' last x-day in x
                d = sn(2) & "/1/" & iYear ' first day of the month
                iDay = d.DayOfWeek
                i = sn(1) - iDay + 1 ' day-of-month of first x-day
                If i < 0 Then i = i + 7
                k = DateTime.DaysInMonth(iYear, d.Month)
                k = ((k - i) \ 7) * 7 + i
                d = sn(2) & "/" & k & "/" & iYear

              Case 7 ' non-annual (4 0) date
                i = iYear Mod CInt(sn(1))
                If i = CInt(sn(2)) Then
                  d = sn(3) & "/" & sn(4) & "/" & iYear
                Else
                  d = Nothing ' not this year
                End If

              Case 8 ' first x day after x day in x
                d = sn(3) & "/1/" & iYear ' first day of the month
                iDay = d.DayOfWeek
                i = sn(1) - iDay + 1 ' day-of-month of first x-day
                If i < 0 Then i = i + 7
                k = sn(2)
                j = sn(1)
                If j < k Then i = i + k - j Else i = i + k - j + 7
            End Select

            ' add d and s
            If InStr(LCase(uCategories), LCase(category) & crlf) > 0 Then ' include the category
              If d <> Nothing AndAlso d.Year = iYear Then
                k = d.DayOfYear
                If calComment(k) = "" Then
                  calComment(k) = s
                Else
                  calComment(k) = calComment(k) & crlf & crlf & s
                End If
              End If
            End If
          End If
        Loop
      End If
    Next iFile

End Sub

Sub processString(ByVal d As DateTime, ByVal dOrg As DateTime, ByRef s As String)

' replace field codes with values. Assume the same day of the year and month.
' <year> <yth> <years>

Dim y, yth, yearsAgo As String
Dim i As Integer
Dim iy, iyearsAgo As Integer

If InStr(s, "<") = 0 Then Exit Sub

iyearsAgo = d.Year - dOrg.Year
yearsAgo = iyearsAgo

iy = dOrg.Year
y = iy

y = Format(dOrg.Year, "0000")
If y < 100 And y >= 1 Then
  y = dOrg.Year & " CE"
ElseIf y < 0 Then
  y = dOrg.Year & " BCE"
  End If

i = iyearsAgo Mod 10
If i = 2 Then
  yth = iyearsAgo & "nd"
ElseIf i = 3 Then
  yth = iyearsAgo & "rd"
Else
  yth = iyearsAgo & "th"
  End If

s = s.Replace("<year>", y)
s = s.Replace("<yth>", yth)
s = s.Replace("<years>", yearsAgo)

End Sub

Function easter(ByRef yearr As Integer) As Date
Dim c As Integer
Dim y As Integer
Dim n As Integer
Dim k As Integer
Dim i As Integer
Dim j As Integer
Dim L As Integer
Dim m As Integer
Dim d As Integer

y = yearr
c = Int(y / 100)
n = y - 19 * Int(y / 19)
k = Int((c - 17) / 25)
i = c - Int(c / 4) - Int((c - k) / 3) + 19 * n + 15
i = i - 30 * Int(i / 30)
i = i - Int(i / 28) * (1 - Int(i / 28) * Int(29 / (i + 1)) * Int((21 - n) / 11))
j = y + Int(y / 4) + i + 2 - c + Int(c / 4)
j = j - 7 * Int(j / 7)
L = i - j
m = 3 + Int((L + 40) / 44)
d = L + 28 - 31 * Int(m / 4)

easter = CDate(Format(m, "00") & "/" & Format(d, "00") & "/" & Format(y, "0000"))

End Function

Protected Overrides Sub Finalize()
'MyBase.Finalize()
Dispose(False)
End Sub

Protected Overridable Overloads Sub Dispose(disposing As Boolean)
If gPen IsNot Nothing Then gPen.Dispose() : gPen = Nothing
If gBrush IsNot Nothing Then gBrush.Dispose() : gBrush = Nothing
If gFont IsNot Nothing Then gFont.Dispose() : gFont = Nothing
End Sub 'Dispose


Public Overloads Sub Dispose() Implements IDisposable.Dispose
Dispose(True)
GC.SuppressFinalize(Me)
End Sub 'Dispose

End Class

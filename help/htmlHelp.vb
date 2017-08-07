Imports VB = Microsoft.VisualBasic
Imports System.IO
Imports System.Text

Friend Class Form1
Inherits Form
' This program reads the photomud.hpj file and the files in the html folder to get the [alias] and [map] sections for photomud.hhp.
' it renames the html files to something more reasonable, too, and makes those changes to photomud.hhp.

Dim path1 As String
Dim path2 As String
Dim idh(500) As String
Dim oldh(500) As String
Dim fname() As String
Dim nfnames As Integer
Dim idhNumber(500) As Integer
Dim hhp() As String
Dim hhc() As String
Dim hhk() As String
Dim ndh As Integer
Dim n As Integer

' file rename stuff ====================================

Private Sub Command1_Click(ByVal eventSender As Object, ByVal e As EventArgs) Handles Command1.Click
' rename files

Dim i As Integer
Dim ic, istate As Integer
Dim k As Integer
Dim c As String
Dim iLine As Integer
Dim lin As String = ""
Dim j As Integer
Dim map As Boolean
Dim iq As Integer
Dim s2, s1, tmpstr As String

Dim ss() As String
Dim s As String

Me.Cursor = Cursors.WaitCursor
' get filenames first, otherwise renaming gets some twice.
fname = Directory.GetFiles(path1, "*.htm")

ndh = 0

For iq = 0 To UBound(fname)
  hhp = File.ReadAllLines(fname(iq), Encoding.Default)

  For i = 0 To UBound(hhp)
    lin = hhp(i)
    k = InStr(lin, "<H1><A NAME=", CompareMethod.Text)
    If k > 0 Then Exit For
    Next i
  
  If k > 0 Then ' found name
    ndh = ndh + 1
    k = k + 13
    idh(ndh) = ""
    For i = k To Len(lin)
      c = Mid(lin, i, 1)
      If c = """" Then Exit For
      idh(ndh) = idh(ndh) & c
      Next i

    k = 0
    For iLine = 0 To UBound(hhp)
      k = InStr(hhp(iLine), "{")
      Do While k > 0 ' convert to hyperlink
        i = InStr(k + 1, hhp(iLine), "*")
        j = InStr(k + 1, hhp(iLine), "}")
        If i > 0 And j > 0 Then
          s1 = Mid(hhp(iLine), k + 1, i - k - 1)
          s2 = Mid(hhp(iLine), i + 1, j - i - 1)

          ' remove <..> from s1
          istate = 0
          ic = 1
          tmpstr = ""
          Do While ic <= Len(s1)
            c = Mid(s1, ic, 1)
            If c = "<" Then istate = 1
            If istate = 0 Then tmpstr = tmpstr & c
            If c = ">" Then istate = 0
            ic = ic + 1
          Loop
          s1 = tmpstr

          hhp(iLine) = VB.Left(hhp(iLine), k - 1) & "<a href=""" & s2 & ".htm"">" & s1 & "</a>" & Mid(hhp(iLine), j + 1)

          End If
        k = InStr(hhp(iLine), "{")
        Application.DoEvents()
      Loop
      Next iLine
    File.WriteAllLines(fname(iq), hhp, Encoding.Default)

    oldh(ndh) = IO.Path.GetFileNameWithoutExtension(fname(iq))
    s = Path.GetDirectoryName(fname(iq)) & "\" & idh(ndh) & ".htm"
    If s <> fname(iq) Then
      If File.Exists(s) Then File.Delete(s) ' delete target file if necessary.
      My.Computer.FileSystem.RenameFile(fname(iq), idh(ndh) & ".htm")
      End If
    End If

  Next iq

'======================================
' files are renamed,  new and old names in idh and oldh.
' now change the names in the hhp file.
' read hhp file

hhp = File.ReadAllLines(path2 & Text1.Text & ".hhp", Encoding.Default)

'======================================
' read numbers from hpj file
ss = File.ReadAllLines(path2 & Text1.Text & ".hpj", Encoding.Default)

map = False
For iLine = 0 To UBound(ss)
  lin = ss(iLine)
  If VB.Left(lin, 5) = "[MAP]" Then
    map = True ' in map section
  ElseIf VB.Left(lin, 1) = "[" Then
    map = False
    End If

  If map Then ' map section
    k = 0
    For j = 1 To ndh
      If InStr(LCase(lin), LCase(idh(j))) > 0 Then
        k = j
        Exit For
        End If
      Next j
    If k > 0 Then ' found it
      i = InStr(lin, "=")
      If i > 0 Then idhNumber(k) = CInt(Mid(lin, i + 1))
      End If
    End If
  Next iLine

'======================================
' translate the hhp file
FileOpen(1, path2 & Text1.Text & ".hhp", OpenMode.Output)

k = 0
For i = 0 To UBound(hhp)
  For j = 1 To ndh
    k = InStr(hhp(i), oldh(j) & ".htm")
    If k > 0 Then Exit For
    Next j

  If k > 0 Then ' found match
    hhp(i) = VB.Left(hhp(i), k - 1) & idh(j) & Mid(hhp(i), k + Len(oldh(j)))
    End If

  PrintLine(1, hhp(i))
  Next i

PrintLine(1)
PrintLine(1, "[ALIAS]")
For i = 1 To ndh
  PrintLine(1, idh(i) & " = html\" & idh(i) & ".htm")
  Next i

PrintLine(1)
PrintLine(1, "[MAP]")
For i = 1 To ndh
  PrintLine(1, "#define " & idh(i) & " " & idhNumber(i))
  Next i

FileClose(1)
'======================================
' translate the index file
hhk = File.ReadAllLines(path2 & Text1.Text & ".hhk", Encoding.Default)

k = 0
For i = 0 To UBound(hhk)
  For j = 1 To ndh
    If InStr(hhk(i), oldh(j) & ".htm", CompareMethod.Text) > 0 Then
      hhk(i) = hhk(i).Replace(oldh(j), idh(j)) ' found match
      Exit For
      End If
    Next j
  Next i

File.WriteAllLines(path2 & Text1.Text & ".hhk", hhk, Encoding.Default)

'======================================
' translate the contents file
hhc = File.ReadAllLines(path2 & Text1.Text & ".hhc", Encoding.Default)

k = 0
For i = 0 To UBound(hhc)
  For j = 1 To ndh
    If InStr(hhc(i), oldh(j) & ".htm", CompareMethod.Text) > 0 Then
      hhc(i) = hhc(i).Replace(oldh(j), idh(j)) ' found match
      Exit For
      End If
    Next j
  Next i

File.WriteAllLines(path2 & Text1.Text & ".hhc", hhc, Encoding.Default)

Me.Cursor = Cursors.Default

End
End Sub

Private Sub Form1_Load(ByVal eventSender As Object, ByVal e As EventArgs) Handles MyBase.Load

Call Text1_Leave(Text1, New System.EventArgs())

End Sub

Private Sub Text1_Leave(ByVal eventSender As Object, ByVal e As EventArgs) Handles Text1.Leave

path1 = "c:\" & Text1.Text & "\help\html\"
path2 = "c:\" & Text1.Text & "\help\"

End Sub


End Class
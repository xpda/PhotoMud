VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   7872
   ClientLeft      =   48
   ClientTop       =   372
   ClientWidth     =   10584
   BeginProperty Font 
      Name            =   "Arial"
      Size            =   9
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   LinkTopic       =   "Form1"
   ScaleHeight     =   7872
   ScaleWidth      =   10584
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox Text1 
      Height          =   312
      Left            =   6660
      TabIndex        =   1
      Text            =   "Photomud"
      Top             =   780
      Width           =   2712
   End
   Begin VB.CommandButton Command1 
      Caption         =   "rename files"
      Height          =   732
      Left            =   840
      TabIndex        =   0
      Top             =   660
      Width           =   1812
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
' This program reads the photomud.hpj file and the files in the html folder to get the [alias] and [map] sections for photomud.hhp.
' it renames the html files to something more reasonable, too, and makes those changes to photomud.hhp.

Dim path As String
Dim path2 As String
Dim idh(500) As String
Dim oldh(500) As String
Dim fname(500) As String
Dim nfnames As Long
Dim idhNumber(500) As Long
Dim hhp(2000) As String
Dim ndh As Long
Dim n As Long

' file rename stuff ====================================

Private Declare Function SHFileOperation Lib "Shell32.DLL" (ByRef lpFileOp As SHFILEOPSTRUCT) As Long
'Declare Function SetCurrentDirectory Lib "kernel32" (ByVal LPCTSTR As String) As Boolean ' not found in library

Const FO_COPY = &H2
Const FO_DELETE = &H3
Const FO_MOVE = &H1
Const FO_RENAME = &H4
Const FOF_ALLOWUNDO = &H40
Const FOF_CONFIRMMOUSE = &H2
Const FOF_FILESONLY = &H80
Const FOF_MULTIDESTFILES = &H1
Const FOF_NOCONFIRMATION = &H10
Const FOF_NOCONFIRMMKDIR = &H200
Const FOF_RENAMEONCOLLISION = &H8
Const FOF_SILENT = &H4
Const FOF_SIMPLEPROGRESS = &H100
Const FOF_WANTMAPPINGHANDLE = &H20

Private Type SHFILEOPSTRUCT
        hwnd As Long
        wFunc As Long
        pFrom As String
        pTo As String
        fFlags As Integer
        fAnyOperationsAborted As Long
        hNameMappings As Long
        lpszProgressTitle As String '  only used if FOF_SIMPLEPROGRESS
End Type


Private Sub Command1_Click()

Dim i As Long
Dim ic As Long, istate As Long
Dim k As Long
Dim c As String
Dim iLine As Long
Dim lin As String
Dim j As Long
Dim map As Boolean
Dim iq As Long
Dim s1 As String, s2 As String, tmpstr As String

Me.MousePointer = vbHourglass
'======================================
' rename files

' get filenames first, otherwise renaming gets some twice.
nfnames = 0
fname(nfnames + 1) = Dir(path & "*.htm")
Do While fname(nfnames + 1) <> ""
  nfnames = nfnames + 1
  fname(nfnames + 1) = Dir
  Loop

ndh = 0

For iq = 1 To nfnames
  Open path & fname(iq) For Input As 1
  k = 0
  Do While Not EOF(1)
    Line Input #1, lin
    k = InStr(lin, "<H1><A NAME=")
    If k > 0 Then Exit Do
    Loop
  Close 1

  If k > 0 Then ' found name
    ndh = ndh + 1
    k = k + 13
    idh(ndh) = ""
    For i = k To Len(lin)
      c = Mid(lin, i, 1)
      If c = """" Then Exit For
      idh(ndh) = idh(ndh) & c
      Next i
    If idh(ndh) = "toolbar" Then
      c = c
      End If
  
    ' inefficient to have a second loop here, but I'm lazy.
    Open path & fname(iq) For Input As 1
    n = 0
    Do While Not EOF(1)
      n = n + 1
      Line Input #1, hhp(n)
      Loop
    Close 1
    
    Open path & fname(iq) For Output As 1
    k = 0
    For iLine = 1 To n
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
          
          hhp(iLine) = Left(hhp(iLine), k - 1) & "<a href=""" & s2 & ".htm"">" & s1 & "</a>" & Mid(hhp(iLine), j + 1)
         
          End If
        k = InStr(hhp(iLine), "{")
        DoEvents
        Loop
      Print #1, hhp(iLine)
      Next iLine
    Close 1
  
    oldh(ndh) = Left(fname(iq), InStr(fname(iq), ".") - 1)
    i = Rename(path & fname(iq), path & idh(ndh) & ".htm")
    End If
  
  Next iq
  
'======================================
' files are renamed,  new and old names in idh and oldh.
' now change the names in the hhp file.
' read hhp file
Open path2 & Text1.Text & ".hhp" For Input As 1
n = 0
Do While Not EOF(1)
  n = n + 1
  Line Input #1, hhp(n)
  Loop

Close 1
'======================================
' read numbers from hpj file
Open path2 & Text1.Text & ".hpj" For Input As 1

map = False
Do While Not EOF(1)
  Line Input #1, lin
  If Left(lin, 5) = "[MAP]" Then
    map = True ' in map section
  ElseIf Left(lin, 1) = "[" Then
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
      If i > 0 Then idhNumber(k) = Mid(lin, i + 1)
      End If
    End If
  
  Loop
Close 1

'======================================
' translate the hhp file
Open path2 & Text1.Text & ".hp2" For Output As 1
  
k = 0
For i = 1 To n
  For j = 1 To ndh
    k = InStr(hhp(i), oldh(j) & ".htm")
    If k > 0 Then Exit For
    Next j
  
  If k > 0 Then ' found match
    hhp(i) = Left(hhp(i), k - 1) & idh(j) & Mid(hhp(i), k + Len(oldh(j)))
    End If
  
  Print #1, hhp(i)
  Next i
  
Print #1,
Print #1, "[ALIAS]"
For i = 1 To ndh
  Print #1, idh(i) & " = html\" & idh(i) & ".htm"
  Next i

Print #1,
Print #1, "[MAP]"
For i = 1 To ndh
  Print #1, "#define " & idh(i) & " " & idhNumber(i)
  Next i
  
Close
'======================================
' translate the index file
Open path2 & Text1.Text & ".hhk" For Input As 1
n = 0
Do While Not EOF(1)
  n = n + 1
  Line Input #1, hhp(n)
  Loop
Close 1

Open path2 & Text1.Text & ".hk2" For Output As 1

k = 0
For i = 1 To n
  For j = 1 To ndh
    k = InStr(hhp(i), oldh(j) & ".htm")
    If k > 0 Then Exit For
    Next j
  
  If k > 0 Then ' found match
    hhp(i) = Left(hhp(i), k - 1) & idh(j) & Mid(hhp(i), k + Len(oldh(j)))
    End If
  
  Print #1, hhp(i)
  Next i

Close 1

'======================================
' translate the contents file
Open path2 & Text1.Text & ".hhc" For Input As 1
n = 0
Do While Not EOF(1)
  n = n + 1
  Line Input #1, hhp(n)
  Loop
Close 1

Open path2 & Text1.Text & ".hc2" For Output As 1

k = 0
For i = 1 To n
  For j = 1 To ndh
    k = InStr(hhp(i), oldh(j) & ".htm")
    If k > 0 Then Exit For
    Next j
  
  If k > 0 Then ' found match
    hhp(i) = Left(hhp(i), k - 1) & idh(j) & Mid(hhp(i), k + Len(oldh(j)))
    End If
  
  Print #1, hhp(i)
  Next i

Close 1

Me.MousePointer = vbNormal

End
End Sub

Private Sub Form_Load()

Call Text1_lostfocus

End Sub

Private Sub Text1_lostfocus()

path = "c:\" & Text1.Text & "\help\html\"
path2 = "c:\" & Text1.Text & "\help\"

End Sub
Function Rename(ByVal strFromName As String, ByVal strToName As String) As Long
    
Dim CFileStruct As SHFILEOPSTRUCT

If strFromName = strToName Then Exit Function

CFileStruct.hwnd = Me.hwnd
CFileStruct.fFlags = FOF_ALLOWUNDO
CFileStruct.pFrom = strFromName & Chr(0) & Chr(0)
CFileStruct.pTo = strToName & Chr(0) & Chr(0)
CFileStruct.wFunc = FO_RENAME

Rename = SHFileOperation(CFileStruct)

End Function



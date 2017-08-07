Imports System.Net
Imports vb = Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.IO

Imports System.Data
Imports MySql.Data.MySqlClient

Imports System.Xml
Imports System.Xml.Serialization

Public Module raceMain

  Structure competitorRec
    Dim raceNumber As String
    Dim name As String
    Dim division As String
    Dim gender As String
    Dim age As Integer
    Dim id As Integer
    Dim imageCounter As Integer
  End Structure

  Public nodeMatchColor As Color = Color.Cornsilk

  Public iniracePath As String
  Public raceDBEnabled As Boolean = True

  Public racePrevFilename As String = ""

  Public lastRaceNumber As String = ""
  Public lastRaceName As String = ""



  Sub linkracePhotos()
    ' link the tagged images in the race database -- changes the database only.

    Dim setID As Integer
    Dim fName As String
    Dim i As Integer
    Dim nLinked As Integer = 0
    Dim exists As Boolean = False

    setID = 0
    For i = 0 To tagPath.Count - 1
      fName = Path.GetFileName(tagPath(i))
      exists = True
      If String.Compare(Path.GetDirectoryName(tagPath(i)), iniracePath, True) <> 0 Then ' different source and target folders
        fName = getRaceTargetFilename(iniracePath, tagPath(i), exists) ' get the output filename (last match, if more than one)
      End If
      If exists Then
        saveRaceImageset(fName, setID) ' setID is assigned in the first call, byref
        nLinked = nLinked + 1
      End If
    Next i

    MsgBox(nLinked & " images were linked.", MsgBoxStyle.OkCancel)

  End Sub

  Sub getcompetitorByID(ByVal id As Integer, ByRef match As competitorRec)

    Dim dset As New DataSet

    dset = getDS("select * from taxatable where id = @parm1 limit 1", id)

    If dset IsNot Nothing AndAlso dset.Tables(0).Rows.Count >= 1 Then
      getcompetitor(dset.Tables(0).Rows(0), match)
    Else
      match.name = ""
    End If

  End Sub

  Sub getcompetitor(ByRef drow As DataRow, ByRef match As competitorRec)

    ' load drow into match
    If IsDBNull(drow.Item("id")) Then match.id = 0 Else match.id = drow.Item("id")
    If IsDBNull(drow.Item("racenumber")) Then match.raceNumber = "" Else match.raceNumber = drow.Item("racenumber")
    If IsDBNull(drow.Item("name")) Then match.name = "" Else match.name = drow.Item("name")
    If IsDBNull(drow.Item("division")) Then match.division = 0 Else match.division = drow.Item("division")
    If IsDBNull(drow.Item("age")) Then match.age = 0 Else match.age = drow.Item("age")
    If IsDBNull(drow.Item("gender")) Then match.gender = "" Else match.gender = drow.Item("gender")
    If IsDBNull(drow.Item("imagecounter")) Then match.imageCounter = 0 Else match.imageCounter = drow.Item("imagecounter")

  End Sub

  Sub linkRaceImageset(ByVal fName As String, ByVal linktoFname As String)
    ' adds fname's imageid to an imageSet with linktoFname's id. Creates a new imageset if it doesn't exist.
    ' 3 ids in this table: image, set, and unique ID

    Dim setid As Integer
    Dim linktoID As Integer = 0

    ' get imageset.imageid from filenames
    linktoID = getScalar("select id from images where filename = @parm1 limit 1", linktoFname)
    If linktoID <= 0 Then Exit Sub

    ' find existing previous link
    setid = getScalar("select setid from imagesets where imageid = @parm1 limit 1", linktoID)

    If setid <= 0 Then ' no existing imageset - make one
      saveRaceImagesetID(linktoID, setid) ' assigns setid
    End If

    Call saveRaceImageset(fName, setid) ' save the one for the new file

  End Sub

  Sub saveRaceImagesetID(ByVal imageID As Integer, ByRef setID As Integer)
    ' make a new imagset record.
    ' if setID is zero, it replaces it with the new setID value.
    ' deletes other setIDs for this imageid

    Dim i As Integer

    If imageID <= 0 Then Exit Sub

    i = nonQuery("delete from imagesets where imageid = @parm1", imageID)
    i = nonQuery("insert into imagesets (imageid, setid) values (@parm1, @parm2)", imageID, setID)

    If setID <= 0 Then ' no existing imageset - make a unique setID
      ' set setid to the id of the imageset record just created, to make sure it's not in use
      setID = getScalar("select id from imagesets where setid = @parm1 and imageid = @parm2", setID, imageID)
      i = nonQuery("update imagesets set setid = @parm1 where id = @parm1", setID)
    End If

  End Sub

  Sub saveRaceImageset(ByVal fName As String, ByRef setID As Integer)
    ' save an imageset record for fName with setID. May replace an existing one.

    Dim dset As New DataSet

    Dim i As Integer
    Dim newUid, oldSetid As Integer
    Dim imageID As Integer = 0

    imageID = getScalar("select id from images where filename = @parm1 limit 1", fName)

    If imageID <= 0 Then Exit Sub

    If setID <= 0 Then ' create a record with a new setid
      saveRaceImagesetID(imageID, setID)
      Exit Sub
    End If

    ' find existing link for current image
    dset = getDS("select id, setid from imagesets where imageid = @parm1", imageID)
    If dset Is Nothing Then Exit Sub

    If dset.Tables.Count > 0 And dset.Tables(0).Rows.Count > 0 Then
      newUid = dset.Tables(0).Rows(0)("id")
      oldSetid = dset.Tables(0).Rows(0)("setid")
    Else
      newUid = 0
    End If

    If newUid <= 0 Then
      ' add current image to new imageset record
      i = nonQuery("insert into imagesets (imageid, setid) values (@parm1, @parm2)", imageID, setID)
    Else
      ' add current image to existing imageset record
      i = nonQuery("update imagesets set imageid = @parm1, setid = @parm2 where id = @parm3", imageID, setID, newUid)
      ' delete the rest of the old imageset if there's only one image left
      i = getScalar("select count(*) from imagesets where setid = @parm1", oldSetid)
      If i = 1 Then ' delete it
        i = nonQuery("delete from imagesets where setid = @parm1", oldSetid)
      End If
    End If

  End Sub

  Sub raceDelete(ByVal fName As String)

    Dim i, imageID, setID, taxID As Integer

    imageID = getScalar("select id from images where filename = @parm1 limit 1", fName)
    taxID = getScalar("select competitorid from images where id = @parm1 limit 1", imageID)
    setID = getScalar("select setid from imagesets where imageid = @parm1", imageID) ' save to check 1-member sets
    i = nonQuery("delete from imagesets where imageid = @parm1", imageID) ' delete the imageset record
    i = nonQuery("delete from images where filename = @parm1", fName) ' delete the image record

    i = getScalar("select count(*) from imagesets where setid = @parm1", setID) ' how many left in the imageset
    If i = 1 Then ' delete the set if there is only one left
      i = nonQuery("delete from imagesets where setid = @parm1", setID)
    End If

    Call incRaceImageCounter(taxID, -1)

  End Sub

  Sub deleteRaceImagesets(ByVal fName As String)
    ' delete all imagesets for a filename

    Dim imageID As Integer = 0
    Dim i As Integer

    imageID = getScalar("select id from images where filename = @parm1 limit 1", fName)
    If imageID > 0 Then i = nonQuery("delete from imagesets where imageid = @parm1", imageID)

  End Sub

  Function getRaceTargetFilename(ByVal targetFolder As String, ByVal picpath As String, ByRef exists As Boolean) As String

    ' gets a file name for when the source(picpath) and targets(folder) are/have different folders (assumed)

    Dim i As Integer
    Dim s, s1, matchPath, lastpicmatch As String
    Dim c As String

    ' get txfilename.text - generate or find the file name
    s1 = targetFolder
    If vb.Right(s1, 1) <> "\" Then s1 = s1 & "\"
    s = Path.GetFileNameWithoutExtension(picpath)
    ' append a-z... use a, or the last one that exists (if any do).
    c = "a"
    lastpicmatch = ""

    If File.Exists(s1 & s & c & ".jpg") Then
      Do While File.Exists(s1 & s & c & ".jpg")
        ' find the last match with matching originalpath
        matchPath = picpath.Substring(1) ' any drive
        i = getScalar("SELECT count(*) FROM images WHERE substr(originalpath, 2) = @parm1 and filename = @parm2", matchPath, s & c & ".jpg")
        If i > 0 Then lastpicmatch = c

        c = Chr(Asc(c) + 1)
      Loop

      If lastpicmatch <> "" Then
        c = lastpicmatch
        exists = True
      Else
        exists = False
      End If

    Else ' file does not exist
      exists = False
    End If

    Return (s & c & ".jpg")  ' filename without path

  End Function

  Sub incRaceImageCounter(ByVal taxid As Integer, ByVal inc As Integer)

    ' add inc to imagecounter and childimagecounter for taxid and its ancestors

    Dim imageCounter As Integer
    Dim dset As New DataSet

    dset = getDS("select * from taxatable where id = @parm1", taxid)

    If dset Is Nothing OrElse dset.Tables.Count <> 1 Then Exit Sub ' error
    imageCounter = dset.Tables(0).Rows(0)("imagecounter")
    imageCounter += inc

  End Sub

  Public Class pixClass
    Public fName As String
    Public photoDate As DateTime
    Public ID As Integer
    Public Remarks As String
    Public match As competitorRec

    Sub New(ByRef dr As DataRow)

      fName = dr("filename")
      photoDate = dr("photodate")
      ID = dr("id")
      Remarks = dr("remarks")

      match = New competitorRec
      match.name = dr("name")
      match.id = dr("competitorid")
      match.imageCounter = dr("imageCounter")

    End Sub

  End Class

End Module

﻿Imports System.Net
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

Public Module bugMain

  Structure GeocodeResponse
    Dim address_component As String
    Dim type As String
  End Structure

  Structure taxrec
    Dim rank As String
    Dim taxon As String
    Dim descr As String
    Dim id As Integer
    Dim parentid As Integer
    Dim imageCounter As Integer
    Dim childimageCounter As Integer
    Dim link As String
    Dim authority As String
    Dim taxonkey As String
  End Structure


  Public nodeMatchColor As Color = Color.Cornsilk
  Public queryNames As New List(Of String) ' used by frmexpore and frmbugphotos
  Public useQuery As Boolean = False ' for frmExplore and frmbugphotos

  Public iniBugPath As String
  Public lastbugTaxon As String = ""
  Public lastbugTaxonID As Integer = 0
  Public lastbugCommon As String = ""
  Public lastbugLocation As String = ""
  Public lastbugCounty As String = ""
  Public lastbugState As String = ""
  Public lastbugCountry As String = ""
  Public lastbugGPS As String = ""
  Public lastbugRating As String = ""
  Public lastbugSize As String = ""
  Public lastbugConfidence As String = ""
  Public lastbugRemarks As String = ""
  Public lastbugBugguide As String = ""
  Public bugDBEnabled As Boolean = True
  Public lastbugLocationAutocomplete As AutoCompleteStringCollection = Nothing
  Public iniBugPixelsPerMM As Double = 274.5 ' gx1: 268.4 macro, 56.5 for zoom, gh4: 274.5 macro, 57.8 zoom

  Public bugPrevFilename As String = ""

  'Public QueryTaxon As String = ""  ' for shortcut -- temporary!


  Sub linkBugPhotos()
    ' link the tagged images in the bug database -- changes the database only.

    Dim setID As Integer
    Dim fName As String
    Dim i As Integer
    Dim nLinked As Integer = 0
    Dim exists As Boolean = False

    setID = 0
    For i = 0 To tagPath.Count - 1
      fName = Path.GetFileName(tagPath(i))
      exists = True
      If String.Compare(Path.GetDirectoryName(tagPath(i)), iniBugPath, True) <> 0 Then ' different source and target folders
        fName = getTargetFilename(iniBugPath, tagPath(i), exists) ' get the output filename (last match, if more than one)
      End If
      If exists Then
        saveImageSet(fName, setID) ' setID is assigned in the first call, byref
        nLinked = nLinked + 1
      End If
    Next i

    MsgBox(nLinked & " images were linked.", MsgBoxStyle.OkCancel)

  End Sub

  Sub getTaxonByID(ByVal id As Integer, ByRef match As taxrec)

    Dim dset As New DataSet

    dset = getDS("select * from taxatable where id = @parm1 limit 1", id)

    If dset IsNot Nothing AndAlso dset.Tables(0).Rows.Count >= 1 Then
      getTaxon(dset.Tables(0).Rows(0), match)
    Else
      match.taxon = ""
      match.taxonkey = ""
    End If

  End Sub

  Sub getTaxon(ByRef drow As DataRow, ByRef match As taxrec)

    ' load drow into match
    If IsDBNull(drow.Item("rank")) Then match.rank = "" Else match.rank = drow.Item("rank")
    If IsDBNull(drow.Item("taxon")) Then match.taxon = "" Else match.taxon = drow.Item("taxon")
    If IsDBNull(drow.Item("descr")) Then match.descr = "" Else match.descr = drow.Item("descr")
    If IsDBNull(drow.Item("id")) Then match.id = 0 Else match.id = drow.Item("id")
    If IsDBNull(drow.Item("parentid")) Then match.parentid = 0 Else match.parentid = drow.Item("parentid")
    If IsDBNull(drow.Item("imagecounter")) Then match.imageCounter = 0 Else match.imageCounter = drow.Item("imagecounter")
    If IsDBNull(drow.Item("childimagecounter")) Then match.childimageCounter = 0 Else match.childimageCounter = drow.Item("childimagecounter")
    If IsDBNull(drow.Item("link")) Then match.link = "" Else match.link = drow.Item("link")
    If IsDBNull(drow.Item("authority")) Then match.authority = "" Else match.authority = drow.Item("authority")

    match.taxonkey = getTaxonKey(match.parentid, match.rank, match.taxon)

  End Sub

  Function getTaxonKey(ByVal parentid As Integer, ByVal rank As String, ByVal taxon As String) As String

    ' adds the genus to species, and both of those to subspecies
    If String.Compare(rank, "species", True) <> 0 And String.Compare(rank, "subspecies", True) <> 0 Then Return taxon

    getTaxonKey = getScalar("SELECT getTaxonkey(@parm1, @parm2, @parm3)", parentid, rank, taxon)

  End Function

  Function taxaLabel(ByVal match As taxrec, ByVal verbose As Boolean, ByVal isQuery As Boolean) As String

    ' get a taxon label for treeview, etc.

    Dim s, descr As String

    s = match.taxonkey

    If verbose Then
      If String.Compare(match.rank, "species", True) <> 0 And String.Compare(match.rank, "subspecies", True) <> 0 Then
        If match.rank <> "No Taxon" Then s = s & " " & match.rank
      End If
    End If

    descr = getDescr(match, True)
    If descr <> "" Then s = s & " (" & descr & ")"
    If isQuery Then s = s & "  " & match.imageCounter & "/" & match.childimageCounter

    Return s

  End Function

  Public Sub populate(ByRef node As TreeNode, ByVal isQuery As Boolean)
    ' populate a single tree node from the database

    Dim dset As New DataSet
    Dim drow As DataRow
    Dim match As New taxrec

    Dim nd As TreeNode
    Dim found As Boolean

    If isQuery Then
      dset = getDS("select * from taxatable where parentid = @parm1 and childimagecounter > 0 order by taxon", node.Tag)
    Else
      dset = getDS("select * from taxatable where parentid = @parm1 order by taxon", node.Tag)
    End If

    If dset IsNot Nothing Then
      For Each drow In dset.Tables(0).Rows
        getTaxon(drow, match)
        found = False
        For Each nd In node.Nodes
          If Int(nd.Tag) = match.id Then ' don't add duplicate
            found = True
            Exit For
          End If
        Next nd

        If Not found Then
          nd = node.Nodes.Add(taxaLabel(match, False, isQuery))
          nd.ToolTipText = match.rank
          nd.Tag = match.id
        End If
      Next drow
    End If

  End Sub

  Function getDescr(ByRef inMatch As taxrec, ByVal shortForm As Boolean) As String

    ' start at taxonkey, then ascend through the parents until a description is found.
    ' shortform is true to omit "Family: Brushfoot etc."

    Dim match As New taxrec
    Dim parent As Integer
    Dim iter As Integer = 0
    Dim i As Integer

    If inMatch.parentid <= 0 Then ' inmatch might only have the taxonid
      ' load everything else into inmatch
      i = inMatch.id
      getTaxonByID(i, inMatch)
    End If

    If inMatch.descr <> "" Or shortForm Then Return inMatch.descr
    parent = inMatch.parentid

    Do While parent >= 0 And iter < 25
      iter = iter + 1
      getTaxonByID(parent, match)
      If match.taxon = "" Then Return ""

      If match.descr <> "" AndAlso match.rank <> "No Taxon" And (match.rank <> "Species" Or inMatch.rank = "Subspecies") And match.rank <> "Subspecies" Then
        Return match.rank & ": " & match.descr
      End If
      parent = match.parentid
    Loop

    Return ""

  End Function

  Function popuTaxon(ByVal xtaxi As String, ByRef tvtaxon As TreeView, ByVal isQuery As Boolean) As taxrec
    ' populate the tvTaxon and open it to taxi

    Dim nd As TreeNode = Nothing
    Dim ndc As TreeNode = Nothing
    Dim ndParent As TreeNode = Nothing
    Dim ids As New List(Of Integer)
    Dim match, topMatch As New taxrec
    Dim ancestor As New List(Of taxrec)
    Dim i As Integer

    Dim dset As New DataSet
    Dim drow As DataRow

    Dim ndTarget As TreeNode = Nothing
    Dim targetLevel As Integer ' level in tvTaxon of the match

    tvtaxon.Visible = True

    ' search for the taxon
    dset = TaxonkeySearch(xtaxi, isQuery)

    ndTarget = Nothing
    targetLevel = 999

    If dset IsNot Nothing Then
      For Each drow In dset.Tables(0).Rows
        getTaxon(drow, match)

        ancestor.Clear()
        ancestor.Add(match)
        getancestors(ancestor, False, "arthropoda")  ' retrieve ancestors of ancestor(0). false = don't exclude "no taxons"

        'ndParent = Nothing
        'For Each nd In tvtaxon.Nodes(0).Nodes
        '  If InStr(nd.Text, "Hexapoda") > 0 Then
        '    ndParent = nd
        '    Exit For
        '    End If
        '  Next nd

        'If ndParent Is Nothing Then Exit For ' should never happen

        ndParent = tvtaxon.Nodes(0) ' 9/25/14

        For i = ancestor.Count - 2 To 0 Step -1  ' go through ancestors top down, starting at arthropoda children
          ndc = Nothing
          For Each nd In ndParent.Nodes          ' search for next match
            If Int(nd.Tag) = ancestor(i).id Then
              ndc = nd
              Exit For
            End If
          Next nd

          ' add node to treeview if it's not already there
          If ndc Is Nothing Then
            ndc = ndParent.Nodes.Add(taxaLabel(ancestor(i), False, isQuery))
            'If ancestor(i).descr = "" Then
            '  ndc = ndParent.Nodes.Add(ancestor(i).taxon)
            'Else
            '  ndc = ndParent.Nodes.Add(ancestor(i).taxon & " -- " & ancestor(i).descr)
            '  End If
            ndc.ToolTipText = ancestor(i).rank
            ndc.Tag = ancestor(i).id
          End If

          If ndc.Nodes.Count = 0 Then populate(ndc, isQuery)
          ndc.Expand()
          ndParent = ndc  ' descend one level
        Next i

        If ndc IsNot Nothing Then
          ndc.BackColor = nodeMatchColor
        Else
          ndParent.BackColor = nodeMatchColor
        End If

        If ancestor.Count < targetLevel Then ' save the top match
          ndTarget = ndc
          targetLevel = ancestor.Count
          topMatch = match
        End If

      Next drow
    End If

    ' highlight the top match
    tvtaxon.SelectedNode = ndTarget
    tvtaxon.Select()

    Return topMatch

  End Function

  Sub getancestors(ByVal ancestor As List(Of taxrec), ByVal excludeNoTaxon As Boolean, ByVal StopAt As String)

    Dim match As New taxrec
    Dim iter As Integer = 0
    Dim id As Integer

    If ancestor.Count <> 1 Then Exit Sub
    id = ancestor(0).parentid

    Do While id > 0 And iter < 50
      getTaxonByID(id, match)
      ancestor.Add(match)
      If eqstr(match.taxon, StopAt) Then Exit Do
      id = match.parentid
      iter = iter + 1
    Loop

  End Sub

  Sub getQueryPaths(ByRef fileNames As List(Of String), ByVal initialize As Boolean)
    ' gets file names from bugquery into fileNames()

    'If Clipboard.ContainsText Then QueryTaxon = Clipboard.GetText '` temporary!!!

    If initialize Or queryNames Is Nothing Then
      If useQuery Then ' bugs
        Using frm As New frmBugQuery
          frm.ShowDialog()
        End Using
      End If
    End If
    fileNames.AddRange(queryNames)

  End Sub
  Sub gotoNextNode(ByVal tvtaxon As TreeView)

    ' select the next node with treeMatchColor

    Dim ndStart As TreeNode
    Dim done As Boolean

    ndStart = tvtaxon.SelectedNode

    done = traverse(tvtaxon.Nodes(0), ndStart, 0, tvtaxon) ' 0 means to skip the current node nd

    If Not done Then done = traverse(tvtaxon.Nodes(0), ndStart, 1, tvtaxon) ' ignore nd -- wrap

  End Sub

  Function traverse(ByRef nd As TreeNode, ByRef startNode As TreeNode, ByRef mode As Integer, ByRef tvtaxon As TreeView) As Boolean

    Dim ndc As TreeNode
    Dim done As Boolean

    If nd.BackColor = nodeMatchColor Then
      done = done
    End If

    If mode = 0 AndAlso nd Is startNode Then
      mode = 1
    Else
      If mode = 1 AndAlso nd.BackColor = nodeMatchColor Then
        tvtaxon.SelectedNode = nd
        nd.EnsureVisible()
        ' tvtaxon.Select()
        Return True
      End If
    End If

    For Each ndc In nd.Nodes
      done = traverse(ndc, startNode, mode, tvtaxon)
      If done Then Return True
    Next ndc

    Return False

  End Function

  Sub linkImageSet(ByVal fName As String, ByVal linktoFname As String)
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
      saveImageSetID(linktoID, setid) ' assigns setid
    End If

    Call saveImageSet(fName, setid) ' save the one for the new file

  End Sub

  Sub saveImageSetID(ByVal imageID As Integer, ByRef setID As Integer)
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

  Sub saveImageSet(ByVal fName As String, ByRef setID As Integer)
    ' save an imageset record for fName with setID. May replace an existing one.

    Dim dset As New DataSet

    Dim i As Integer
    Dim newUid, oldSetid As Integer
    Dim imageID As Integer = 0

    imageID = getScalar("select id from images where filename = @parm1 limit 1", fName)

    If imageID <= 0 Then Exit Sub

    If setID <= 0 Then ' create a record with a new setid
      saveImageSetID(imageID, setID)
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

  Sub bugDelete(ByVal fName As String)

    Dim i, imageID, setID, taxID As Integer

    imageID = getScalar("select id from images where filename = @parm1 limit 1", fName)
    taxID = getScalar("select taxonid from images where id = @parm1 limit 1", imageID)
    setID = getScalar("select setid from imagesets where imageid = @parm1", imageID) ' save to check 1-member sets
    i = nonQuery("delete from imagesets where imageid = @parm1", imageID) ' delete the imageset record
    i = nonQuery("delete from images where filename = @parm1", fName) ' delete the image record

    i = getScalar("select count(*) from imagesets where setid = @parm1", setID) ' how many left in the imageset
    If i = 1 Then ' delete the set if there is only one left
      i = nonQuery("delete from imagesets where setid = @parm1", setID)
    End If

    Call incImageCounter(taxID, -1)

  End Sub

  Sub deleteImageSets(ByVal fName As String)
    ' delete all imagesets for a filename

    Dim imageID As Integer = 0
    Dim i As Integer

    imageID = getScalar("select id from images where filename = @parm1 limit 1", fName)
    If imageID > 0 Then i = nonQuery("delete from imagesets where imageid = @parm1", imageID)

  End Sub

  Function getTargetFilename(ByVal targetFolder As String, ByVal picpath As String, ByRef exists As Boolean) As String

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

  Public Sub GPSLocate(ByVal latlon As String, ByRef locale As String, ByRef county As String, ByRef state As String, ByRef country As String)

    ' gets the County, State from Google for the latlon

    Dim link As String
    Dim s As String
    Dim dLat, dLon As Double
    Dim xmldoc As New XmlDocument
    Dim nodes As XmlNodeList

    Dim deserializer As New XmlSerializer(GetType(GeocodeResponse))

    Dim client As New cWebClient

    s = LCase(latlon)
    Call latlonVerify(s, dLat, dLon)

    link = "http://maps.googleapis.com/maps/api/geocode/xml?sensor=true&latlng=" & Format(dLat, "###.#####") & "," & Format(dLon, "###.#####")

    Try
      s = client.DownloadString(link)
    Catch ex As Exception
      MsgBox(ex.Message)
      Exit Sub
    End Try

    xmldoc.LoadXml(s)

    nodes = xmldoc.GetElementsByTagName("result")
    nodes = xmldoc.SelectNodes("GeocodeResponse/result[type='political']/address_component[type='administrative_area_level_2']/short_name") ' added political 9/2015
    If nodes.Count >= 1 Then county = nodes(0).InnerText Else county = ""
    nodes = xmldoc.SelectNodes("GeocodeResponse/result/address_component[type='administrative_area_level_1']/short_name")
    If nodes.Count >= 1 Then state = nodes(0).InnerText Else state = ""
    nodes = xmldoc.SelectNodes("GeocodeResponse/result/address_component[type='country']/long_name")
    If nodes.Count >= 1 Then country = nodes(0).InnerText Else country = ""
    nodes = xmldoc.SelectNodes("GeocodeResponse/result/address_component[type='locality']/short_name")
    If nodes.Count >= 1 Then locale = nodes(0).InnerText Else locale = ""

    If LCase(county).EndsWith(" county") Then county = county.Substring(0, county.Length - 7)
    If LCase(locale) = "pryor creek" Then locale = "Pryor"

    If country = "United States" Then country = ""
    If country = "United Kingdom" Then country = "UK"

  End Sub

  Function getDS(ByVal scmd As String, Optional ByRef parm1 As Object = "", Optional ByRef parm2 As Object = "", Optional ByRef parm3 As Object = "") As DataSet

    ' returns ds, uses @parm1 and @parm2 in query

    Dim cmd As MySqlCommand
    Dim da As New MySqlDataAdapter
    Dim ds As New DataSet

    ds.Clear()
    Try
      Using conn As New MySqlConnection(iniDBConnStr)
        conn.Open()
        cmd = New MySqlCommand(scmd, conn)
        cmd.Parameters.AddWithValue("@parm1", parm1)
        cmd.Parameters.AddWithValue("@parm2", parm2)
        cmd.Parameters.AddWithValue("@parm3", parm3)
        da.SelectCommand = cmd
        da.Fill(ds)
      End Using
    Catch ex As Exception
      MsgBox("Error, getDS: " & ex.Message)
      Return Nothing
    End Try

    Return ds

  End Function

  Function getScalar(ByVal scmd As String, _
    Optional ByRef parm1 As Object = "", Optional ByRef parm2 As Object = "", Optional ByRef parm3 As Object = "") As Object

    ' returns ds, uses @parm1 and @parm2 in query

    Dim cmd As MySqlCommand
    Dim q As Object

    Try
      Using conn As New MySqlConnection(iniDBConnStr)
        conn.Open()
        cmd = New MySqlCommand(scmd, conn)
        cmd.Parameters.AddWithValue("@parm1", parm1)
        cmd.Parameters.AddWithValue("@parm2", parm2)
        cmd.Parameters.AddWithValue("@parm3", parm3)
        q = cmd.ExecuteScalar
        Return q
      End Using
    Catch ex As Exception
      MsgBox("Error, getScalar: " & ex.Message)
      Return Nothing
    End Try

  End Function

  Function nonQuery(ByVal scmd As String, _
    Optional ByRef parm1 As Object = "", Optional ByRef parm2 As Object = "", Optional ByRef parm3 As Object = "") As Object

    ' returns ds, uses @parm1 and @parm2 in query

    Dim cmd As MySqlCommand
    Dim i As Integer

    Try
      Using conn As New MySqlConnection(iniDBConnStr)
        conn.Open()
        cmd = New MySqlCommand(scmd, conn)
        cmd.Parameters.AddWithValue("@parm1", parm1)
        cmd.Parameters.AddWithValue("@parm2", parm2)
        cmd.Parameters.AddWithValue("@parm3", parm3)
        i = cmd.ExecuteNonQuery
        Return i
      End Using
    Catch ex As Exception
      Return 0
      MsgBox("Error, nonQuery: " & ex.Message)
    End Try

  End Function

  Function TaxonkeySearch(ByVal findme As String, ByVal isQuery As Boolean) As DataSet

    ' get dataset taxatable record for taxonkey or common name findme

    Dim qspecies As String
    Dim s As String
    Dim suffix, suffixp As String
    Dim ds As New DataSet
    Dim ss() As String

    findme = findme.Trim

    If isQuery Then
      suffix = " and (childimagecounter > 0) order by taxon"
      suffixp = " and (childimagecounter > 0) order by @p"
    Else
      suffix = " order by taxon"
      suffixp = " order by @p"
    End If

    ss = Split(findme, " ")
    If UBound(ss) = 0 Then ' no space -- on-word search
      ds = getDS("select * from taxatable where taxon = @parm1" & suffix, findme)
      If ds Is Nothing OrElse ds.Tables(0).Rows.Count = 0 Then
        ds = getDS("select * from taxatable where (taxon like @parm1)" & suffix, findme & "%")
      End If

    Else ' multi-word search, search genus and species, for example
      qspecies = ss(UBound(ss)) ' last word, could be subspecies
      ds = getDS("select * from taxatable where taxon = @parm2 and @p := gettaxonkey(parentid, rank, taxon) = @parm1" & suffixp, findme, qspecies)
    End If

    If ds Is Nothing OrElse ds.Tables(0).Rows.Count <= 0 Then ' search descr
      ds.Clear()
      s = findme.Replace("-", "`") ' accept either space or dash, so "eastern tailed blue" finds "eastern tailed-blue". Only works with rlike (mysql bug).
      s = s.Replace(" ", "[- ]")
      s = s.Replace("`", "[- ]")
      ds = getDS("select * from taxatable where (descr rlike @parm1)" & suffix, s)
    End If

    Return ds

  End Function

  Function checkDB(ByVal connStr As String) As Boolean

    Try
      Using conn As New MySqlConnection(connStr)
        conn.Open()
      End Using
    Catch ex As Exception
      Return False
    End Try

    Return True

  End Function

  Function getConnStr(ByVal host As String, ByVal database As String, ByVal user As String, ByVal password As String) As String
    Return "server=" & host & "; database=" & database & "; uid=" & user & "; pwd=" & password & "; allowuservariables=true;"
  End Function

  Sub incImageCounter(ByVal taxid As Integer, ByVal inc As Integer)

    ' add inc to imagecounter and childimagecounter for taxid and its ancestors

    Dim i As Integer
    Dim imageCounter, childImageCounter, parentID As Integer
    Dim dset As New DataSet

    dset = getDS("select * from taxatable where id = @parm1", taxid)

    If dset Is Nothing OrElse dset.Tables.Count <> 1 Then Exit Sub ' error
    imageCounter = dset.Tables(0).Rows(0)("imagecounter")
    childImageCounter = dset.Tables(0).Rows(0)("childimagecounter")
    imageCounter += inc
    childImageCounter += inc

    i = nonQuery("update taxatable set imagecounter = @parm1, childimagecounter = @parm2 where id = @parm3", _
      imageCounter, childImageCounter, taxid)

    parentID = dset.Tables(0).Rows(0)("parentid")
    ' follow parentID up the taxon tree, incrementing childImageCounter
    For k As Integer = 1 To 200
      If parentID <= 0 Then Exit For

      dset = getDS("select * from taxatable where id = @parm1", parentID)
      If dset Is Nothing OrElse dset.Tables.Count <> 1 Then Exit For ' error

      childImageCounter = dset.Tables(0).Rows(0)("childimagecounter")
      childImageCounter += inc

      i = nonQuery("update taxatable set childimagecounter = @parm1 where id = @parm2", childImageCounter, parentID)
      If i = 1 Then
        parentID = dset.Tables(0).Rows(0)("parentid")
      End If
    Next k

  End Sub

  Function getCaption(ByRef pic As pixClass) As String

    Dim s As String
    Dim s1 As String
    Dim taxonkey As String

    s = ""

    taxonkey = getTaxonKey(pic.match.parentid, pic.match.rank, pic.match.taxon)
    s += "<i>" & taxonkey & "</i>"
    If String.Compare(pic.match.rank, "species", True) <> 0 And String.Compare(pic.match.rank, "subspecies", True) <> 0 And _
      String.Compare(pic.match.rank, "no taxon", True) <> 0 Then
      s = s & " " & pic.match.rank
    End If
    s += "<br>"

    s1 = getDescr(pic.match, False)
    If s1 <> "" Then s += s1 & "<br>"

    s1 = LocationLabel(pic)
    If s1 <> "" Then s += s1 & "<br>"

    If pic.Remarks <> Nothing AndAlso pic.Remarks <> "" Then s += "Remarks: " & pic.Remarks & "<br>"
    If pic.Size <> Nothing AndAlso pic.Size <> "" Then s += "Size: " & pic.Size & "<br>"
    If pic.photoDate <> Nothing Then s += Format(pic.photoDate, "d")

    s += vbCrLf & "</div>"

    Return (s)

  End Function

  Function getCalCaption(ByRef pic As pixClass) As String

    ' get a caption for the calendar, not html

    Dim s As String
    Dim s1 As String
    Dim taxonkey As String

    s = ""

    taxonkey = getTaxonKey(pic.match.parentid, pic.match.rank, pic.match.taxon)
    s += taxonkey ' tags will be removed on output
    If String.Compare(pic.match.rank, "species", True) <> 0 And String.Compare(pic.match.rank, "subspecies", True) <> 0 And _
      String.Compare(pic.match.rank, "no taxon", True) <> 0 Then
      s = s & " " & pic.match.rank
    End If

    s1 = getDescr(pic.match, False) ' get next higher rank description
    If s1 <> "" Then s &= ", " & s1

    s += vbCrLf

    s1 = ""
    s1 = LocationLabel(pic)
    If pic.photoDate <> Nothing Then
      If s1 <> "" Then s1 &= ", "
      s1 &= Format(pic.photoDate, "MMMM") & " " & Format(pic.photoDate, "yyyy")
    End If
    If s1 <> "" Then s &= s1 & vbCrLf

    s1 = ""
    If pic.Size <> Nothing AndAlso pic.Size <> "" Then s1 = pic.Size
    If pic.Remarks <> Nothing AndAlso pic.Remarks <> "" Then
      If s1 <> "" Then s1 &= ", "
      s1 &= "Remarks: " & pic.Remarks & vbCrLf
    End If
    If s1 <> "" Then s &= s1

    Return (s)

  End Function

  Function LocationLabel(ByVal pic As pixClass) As String

    Dim s As String

    s = ""
    If pic.Location <> "" Then
      s += pic.Location
      If pic.County <> "" Or pic.State <> "" Or pic.Country <> "" Then s += ", "
    End If
    If pic.County <> "" Then
      s += pic.County
      If pic.Country = "" Then s += " County" ' U.S. only.
      If pic.State <> "" Or pic.Country <> "" Then s += ", "
    End If
    If pic.State <> "" Then
      s += pic.State
      If pic.Country <> "" Then s += ", "
    End If
    If pic.Country <> "" Then s += pic.Country

    Return s

  End Function

End Module


Public Class pixClass
  Public fName As String
  Public photoDate As DateTime
  Public Location As String
  Public County As String
  Public State As String
  Public Country As String
  Public Size As String
  Public ID As Integer
  Public Remarks As String
  Public match As taxrec

  Sub New(ByRef dr As DataRow)

    fName = dr("filename")
    photoDate = dr("photodate")
    Location = dr("location")
    County = dr("county")
    State = dr("state")
    Country = dr("country")
    Size = dr("size")
    ID = dr("id")
    Remarks = dr("remarks")

    match = New taxrec
    match.taxon = dr("taxon")
    ' match.taxonkey = dr("taxonkey")
    match.descr = dr("descr")
    match.rank = dr("rank")
    match.id = dr("taxonid")
    match.parentid = dr("parentid")
    match.imageCounter = dr("imageCounter")
    match.childimageCounter = dr("childimageCounter")

  End Sub

End Class


Imports System.Net
Imports System.Net.Http
Imports vb = Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.IO

Imports System.Data
Imports MySql.Data.MySqlClient

Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq


Public Module bugMain

  Structure GeocodeResponse
    Dim address_component As String
    Dim type As String
  End Structure

  '  Structure taxrec
  ' Dim rank As String
  ' Dim taxon As String
  ' Dim descr As String
  ' Dim taxid As String
  ' Dim parentid As String
  ' Dim imageCounter As Integer
  ' Dim childimageCounter As Integer
  ' Dim link As String
  ' Dim authority As String
  ' End Structure

  Public Class taxrec
    Public rank As String
    Public taxon As String
    Public descr As String
    Public taxid As String
    Public parentid As String
    Public imageCounter As Integer ' from gbifplus, if gbif record
    Public childimageCounter As Integer ' from gbifplus, if gbif record
    Public link As String ' from gbifplus, if gbif record
    Public authority As String

    Public gbifUsable As String ' from gbif

    ' old wikirec stuff, now in oddinfo
    Public wikipediaPageID As String
    Public commonNames As List(Of String)
    Public commonWikiLink As String

    Sub New()
      rank = ""
      taxon = ""
      descr = ""
      taxid = ""
      parentid = ""
      link = ""
      authority = ""
      commonNames = New List(Of String)

      ' old wikirec stuff, now in oddinfo
      wikipediaPageID = 0
      commonWikiLink = ""

    End Sub
  End Class

  Public itisRankID As New Dictionary(Of String, Integer)(System.StringComparer.OrdinalIgnoreCase)
  Public itisRanks As New Dictionary(Of Integer, String)
  Public states As New Dictionary(Of String, String)(System.StringComparer.OrdinalIgnoreCase)
  Public mainRank As New List(Of String)

  Public nodeMatchColor As Color = Color.Cornsilk
  Public queryNames As New List(Of String) ' used by frmexpore and frmbugphotos
  Public useQuery As Boolean = False ' for frmExplore and frmbugphotos

  Public iniBugPath As String
  Public lastbugTaxon As String = ""
  Public lastbugTaxonID As String = "0"
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

  Public cookies As CookieContainer
  Public handler As HttpClientHandler
  Public qClient As HttpClient ' need these for cookies


  'Public QueryTaxon As String = ""  ' for shortcut -- temporary!

  Sub buginit()

    states.Add("Alabama", "AL")
    states.Add("Alaska", "AK")
    states.Add("Arizona", "AZ")
    states.Add("Arkansas", "AR")
    states.Add("California", "CA")
    states.Add("Colorado", "CO")
    states.Add("Connecticut", "CT")
    states.Add("Delaware", "DL")
    states.Add("Florida", "FL")
    states.Add("Georgia", "GA")
    states.Add("Hawaii", "HI")
    states.Add("Idaho", "ID")
    states.Add("Illinois", "IL")
    states.Add("Indiana", "IN")
    states.Add("Iowa", "IA")
    states.Add("Kansas", "KS")
    states.Add("Kentucky", "KY")
    states.Add("Louisiana", "LA")
    states.Add("Maine", "ME")
    states.Add("Maryland", "MD")
    states.Add("Massachusetts", "MA")
    states.Add("Michigan", "MI")
    states.Add("Minnesota", "MN")
    states.Add("Mississippi", "MS")
    states.Add("Missouri", "MO")
    states.Add("Montana", "MT")
    states.Add("Nebraska", "NE")
    states.Add("Nevada", "NV")
    states.Add("New Hampshire", "NH")
    states.Add("New Jersey", "NJ")
    states.Add("New Mexico", "NM")
    states.Add("New York", "NY")
    states.Add("North Carolina", "NC")
    states.Add("North Dakota", "ND")
    states.Add("Ohio", "OH")
    states.Add("Oklahoma", "OK")
    states.Add("Oregon", "OR")
    states.Add("Pennsylvania", "PA")
    states.Add("Rhode Island", "RI")
    states.Add("South Carolina", "SC")
    states.Add("South Dakota", "SD")
    states.Add("Tennessee", "TN")
    states.Add("Texas", "TX")
    states.Add("Utah", "UT")
    states.Add("Vermont", "VT")
    states.Add("Virginia", "VA")
    states.Add("Washington", "WA")
    states.Add("West Virginia", "WV")
    states.Add("Wisconsin", "WI")
    states.Add("Wyoming", "WY")
    states.Add("Puerto Rico", "PR")

    itisRankID.Add("kingdom", 10)
    itisRankID.Add("subkingdom", 20)
    itisRankID.Add("infrakingdom", 25)
    itisRankID.Add("superphylum", 27)
    itisRankID.Add("phylum", 30)
    itisRankID.Add("subphylum", 40)
    itisRankID.Add("infraphylum", 45)
    itisRankID.Add("superclass", 50)
    itisRankID.Add("class", 60)
    itisRankID.Add("subclass", 70)
    itisRankID.Add("infraclass", 80)
    itisRankID.Add("superorder", 90)
    itisRankID.Add("order", 100)
    itisRankID.Add("suborder", 110)
    itisRankID.Add("infraorder", 120)
    itisRankID.Add("section", 124)
    itisRankID.Add("subsection", 126)
    itisRankID.Add("superfamily", 130)
    itisRankID.Add("epifamily", 135)
    itisRankID.Add("family", 140)
    itisRankID.Add("subfamily", 150)
    itisRankID.Add("supertribe", 155)
    itisRankID.Add("tribe", 160)
    itisRankID.Add("subtribe", 170)
    itisRankID.Add("genus", 180)
    itisRankID.Add("subgenus", 190)
    itisRankID.Add("species", 220)
    itisRankID.Add("subspecies", 230)

    itisRanks.Add(10, "kingdom")
    itisRanks.Add(20, "subkingdom")
    itisRanks.Add(25, "infrakingdom")
    itisRanks.Add(27, "superphylum")
    itisRanks.Add(30, "phylum")
    itisRanks.Add(40, "subphylum")
    itisRanks.Add(45, "infraphylum")
    itisRanks.Add(50, "superclass")
    itisRanks.Add(60, "class")
    itisRanks.Add(70, "subclass")
    itisRanks.Add(80, "infraclass")
    itisRanks.Add(90, "superorder")
    itisRanks.Add(100, "order")
    itisRanks.Add(110, "suborder")
    itisRanks.Add(120, "infraorder")
    itisRanks.Add(124, "section")
    itisRanks.Add(126, "subsection")
    itisRanks.Add(130, "superfamily")
    itisRanks.Add(135, "epifamily")
    itisRanks.Add(140, "family")
    itisRanks.Add(150, "subfamily")
    itisRanks.Add(155, "supertribe")
    itisRanks.Add(160, "tribe")
    itisRanks.Add(170, "subtribe")
    itisRanks.Add(180, "genus")
    itisRanks.Add(190, "subgenus")
    itisRanks.Add(220, "species")
    itisRanks.Add(230, "subspecies")

    mainRank.Add("phylum")
    mainRank.Add("class")
    mainRank.Add("order")
    mainRank.Add("superfamily")
    mainRank.Add("family")
    mainRank.Add("tribe")
    mainRank.Add("genus")
    mainRank.Add("species")
    mainRank.Add("subspecies")

    cookies = New CookieContainer
    handler = New HttpClientHandler
    handler.CookieContainer = cookies
    qClient = New HttpClient(handler) ' need this for cookies

  End Sub

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

  Function mergeMatches(matches As List(Of taxrec), gmatches As List(Of taxrec)) As List(Of taxrec)

    ' merges taxrec lists, removes dups from second, must be sorted on input

    Dim ms As New List(Of taxrec)
    Dim taxa As New List(Of String)
    Dim i1, i2 As Integer

    i1 = 0 : i2 = 0
    Do While i1 < matches.Count
      Do While i2 < gmatches.Count AndAlso gmatches(i2).taxon < matches(i1).taxon
        ms.Add(gmatches(i2))
        i2 += 1
      Loop
      If i2 < gmatches.Count AndAlso gmatches(i2).taxon = matches(i1).taxon Then i2 += 1

      ms.Add(matches(i1))
      i1 += 1
    Loop

    If i2 < gmatches.Count AndAlso i1 < matches.Count AndAlso gmatches(i2).taxon = matches(i1).taxon Then i2 += 1
    Do While i2 < gmatches.Count
      ms.Add(gmatches(i2))
      i2 += 1
    Loop

    Return ms


  End Function


  Function getTaxrecByID(ByVal taxid As String) As List(Of taxrec)

    ' gbif ids start with g: g1234, for example.

    Dim ds As New DataSet
    Dim m As New taxrec
    Dim matches As New List(Of taxrec)

    If taxid = "" Then Return matches ' empty list

    If eqstr(taxid.Substring(0, 1), "g") Then ' gbif database
      ds = getDS("select * from gbif.tax where taxid = @parm1 and usable <> '';",
                 taxid.Substring(1).Trim)
      If ds IsNot Nothing Then
        For Each dr As DataRow In ds.Tables(0).Rows
          m = getTaxrecg(dr)
          matches.Add(m)
        Next dr
      End If

    Else ' taxatable database
      ds = getDS("select * from taxatable where taxid = @parm1", taxid)
      If ds IsNot Nothing Then
        For Each dr As DataRow In ds.Tables(0).Rows
          m = getTaxrec(dr)
          matches.Add(m)
        Next dr
      End If
    End If

    Return matches

  End Function

  Function getTaxrec(ByRef dr As DataRow) As taxrec

    Dim match As New taxrec

    ' load drow into match
    If IsDBNull(dr("rank")) Then match.rank = "" Else match.rank = dr("rank")
    If IsDBNull(dr("taxon")) Then match.taxon = "" Else match.taxon = dr("taxon")
    If IsDBNull(dr("descr")) Then match.descr = "" Else match.descr = dr("descr")
    If IsDBNull(dr("taxid")) Then match.taxid = "" Else match.taxid = dr("taxid")
    If IsDBNull(dr("parentid")) Then match.parentid = "" Else match.parentid = dr("parentid")
    If IsDBNull(dr("imagecounter")) Then match.imageCounter = 0 Else match.imageCounter = dr("imagecounter")
    If IsDBNull(dr("childimagecounter")) Then match.childimageCounter = 0 Else match.childimageCounter = dr("childimagecounter")
    If IsDBNull(dr("link")) Then match.link = "" Else match.link = dr("link")
    If IsDBNull(dr("authority")) Then match.authority = "" Else match.authority = dr("authority")

    Return match

  End Function

  Function getTaxrecg(ByRef dr As DataRow) As taxrec
    ' get a taxref from gbif database
    Dim match As New taxrec
    Dim matches As New List(Of taxrec)
    Dim vnames As New List(Of String)
    Dim taxid As String
    Dim ds As DataSet
    Dim s As String
    Dim ss() As String

    If IsDBNull(dr("taxid")) Then taxid = "" Else taxid = dr("taxid")
    If taxid <> "" Then match.taxid = "g" & taxid ' gbif id prefix

    ' load dr into match
    If IsDBNull(dr("rank")) Then match.rank = "" Else match.rank = dr("rank")
    If IsDBNull(dr("name")) Then match.taxon = "" Else match.taxon = dr("name")
    If IsDBNull(dr("parent")) Then match.parentid = "" Else match.parentid = "g" & dr("parent")
    If IsDBNull(dr("authority")) Then match.authority = "" Else match.authority = dr("authority")
    match.authority = match.authority.Replace(" and ", " & ")
    If IsDBNull(dr("usable")) Then match.gbifUsable = "" Else match.gbifUsable = dr("usable")

    ' get image counters and links, if possible
    ds = getDS("select * from gbifplus where taxid = @parm1", taxid)
    For Each dr2 As DataRow In ds.Tables(0).Rows
      If IsDBNull(dr2("imagecounter")) Then match.imageCounter = 0 Else match.imageCounter = dr2("imagecounter")
      If IsDBNull(dr2("childimagecounter")) Then match.childimageCounter = 0 Else match.childimageCounter = dr2("childimagecounter")
      If IsDBNull(dr2("link")) Then match.link = "" Else match.link = dr2("link")
    Next dr2

    ' get common names from oddinfo
    ds = getDS("select * from oddinfo where name = @parm1", match.taxon)
    For Each dr2 As DataRow In ds.Tables(0).Rows
      If dr2("commonnames") <> "" Then
        s = dr2("commonnames")
        ss = s.Split("|")
        If ss.Count >= 1 AndAlso ss(0) <> "" Then
          match.commonNames = ss.ToList
        End If
        Exit For
      End If
    Next dr2

    ' get descr from taxatable, if possible
    ds = getDS("select * from taxatable where taxon = @parm1", match.taxon)
    For Each dr2 As DataRow In ds.Tables(0).Rows
      If dr2("descr") <> "" Then
        match.descr = dr2("descr")
        Exit For
      End If
    Next dr2

    Return match

  End Function

  Function taxaLabel(ByVal match As taxrec, ByVal verbose As Boolean, ByVal isQuery As Boolean) As String

    ' get a taxon label for treeview, etc.

    Dim s, descr As String

    s = match.taxon

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

  Function queryTax(cmd As String, val As String) As List(Of taxrec)

    Dim ds As New DataSet
    Dim match As New taxrec
    Dim matches As New List(Of taxrec)
    '```
    ds = getDS(cmd, val)
    If ds IsNot Nothing Then
      For Each dr As DataRow In ds.Tables(0).Rows
        If cmd.Contains("gbif.") Then
          match = getTaxrecg(dr)
        Else
          match = getTaxrec(dr)
        End If
        If match.taxid <> "" Then matches.Add(match)
      Next dr
    End If

    Return matches

  End Function


  Public Sub populate(ByRef node As TreeNode, ByVal isQuery As Boolean)
    ' populate a single tree node from the database

    Dim matches As List(Of taxrec)
    Dim childImages As Integer

    Dim nd As TreeNode
    Dim found As Boolean

    If isQuery Then
      If node.Tag.startswith("g") Then
        matches = queryTax("select * from gbif.tax where parent = @parm1 and usable <> '' order by name", node.Tag.substring(1))
        For i As Integer = matches.Count - 1 To 0 Step -1
          childImages = getScalar("select * from gbifplus where taxid = @parm1", matches(i).taxid.Substring(1)) ' omit leading g
          If childImages <= 0 Then matches.RemoveAt(i)
        Next i
      Else
        matches = queryTax("select * from taxatable where parentid = @parm1 and childimagecounter > 0 order by taxon", node.Tag)
      End If

    Else
      If node.Tag.startswith("g") Then
        matches = queryTax("select * from gbif.tax where parent = @parm1 and usable <> '' order by name", node.Tag.substring(1))
      Else
        matches = queryTax("select * from taxatable where parentid = @parm1 order by taxon", node.Tag)
      End If
    End If

    For Each m As taxrec In matches
      found = False
      For Each nd In node.Nodes
        If nd.Tag = m.taxid Then ' don't add duplicate
          found = True
          Exit For
        End If
      Next nd

      If Not found Then
        nd = node.Nodes.Add(taxaLabel(m, False, isQuery))
        nd.ToolTipText = m.rank
        nd.Tag = m.taxid
      End If
    Next m

  End Sub

  Function getDescr(ByRef inMatch As taxrec, ByVal shortForm As Boolean) As String

    ' start at taxonkey, then ascend through the parents until a description is found.
    ' shortform is true to omit "Family: Brushfoot etc."

    Dim match As New taxrec
    Dim matches As List(Of taxrec)
    Dim parent As String
    Dim iter As Integer = 0
    Dim taxid As String

    If inMatch.taxid = "" Then Return ""

    If inMatch.parentid = "" Then ' inmatch might only have the taxonid
      ' load everything else into inmatch
      taxid = inMatch.taxid
      matches = getTaxrecByID(taxid)
      If matches.Count > 0 Then inMatch = matches(0)
    End If

    If inMatch.descr <> "" Or shortForm Then Return inMatch.descr
    parent = inMatch.parentid

    Do While parent > "" And iter < 25
      iter = iter + 1
      matches = getTaxrecByID(parent)
      If matches.Count <= 0 OrElse matches(0).taxon = "" Then Return ""
      match = matches(0)

      If match.descr <> "" AndAlso match.rank <> "No Taxon" And (match.rank <> "Species" Or inMatch.rank = "Subspecies") And
           match.rank <> "Subspecies" Then
        Return match.rank & ": " & match.descr
      End If
      parent = match.parentid
    Loop

    Return ""

  End Function

  Function popuTaxon(ByVal xtaxi As String, ByRef tvTax As TreeView, ByVal isQuery As Boolean) As taxrec
    ' populate the tvTax and open it to taxi

    Dim nd As TreeNode = Nothing
    Dim ndc As TreeNode = Nothing
    Dim ndParent As TreeNode = Nothing
    Dim ids As New List(Of Integer)
    Dim topMatch As New taxrec
    Dim matches As New List(Of taxrec)
    Dim ancestor As New List(Of taxrec)
    Dim i As Integer

    Dim ndTarget As TreeNode = Nothing
    Dim targetLevel As Integer ' level in tvTax of the match

    tvTax.Visible = True

    ' search for the taxon
    matches = TaxonSearch(xtaxi, isQuery)

    ndTarget = Nothing
    targetLevel = 999

    For Each match As taxrec In matches
      ancestor = getancestors(match, False, "kingdom")  ' retrieve ancestors of ancestor(0). false = don't exclude "no taxons"

      ndParent = tvTax.Nodes(0) ' 9/25/14

      For i = ancestor.Count - 2 To 0 Step -1  ' go through ancestors top down, starting at arthropoda children
        ndc = Nothing
        For Each nd In ndParent.Nodes          ' search for next match
          If nd.Tag = ancestor(i).taxid Then
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
          ndc.Tag = ancestor(i).taxid
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

    Next match

    ' highlight the top match
    tvTax.SelectedNode = ndTarget
    tvTax.Select()

    Return topMatch

  End Function

  Function getancestors(m As taxrec, excludeNoTaxon As Boolean, StopAt As String) As List(Of taxrec)

    Dim matches As List(Of taxrec)
    Dim ancestor As New List(Of taxrec)
    Dim iter As Integer = 0
    Dim taxid As String

    ancestor.Add(m)
    taxid = m.parentid

    Do While taxid <> "" And iter < 50
      matches = getTaxrecByID(taxid)
      If matches.Count <= 0 Then Return ancestor
      ancestor.Add(matches(0))
      If eqstr(matches(0).rank, StopAt) Then Return ancestor
      taxid = matches(0).parentid
      iter = iter + 1
    Loop

    Return ancestor

  End Function

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
  Sub gotoNextNode(tvTax As TreeView)

    ' select the next node with treeMatchColor

    Dim ndStart As TreeNode
    Dim done As Boolean
    Dim mode As Integer

    ndStart = tvTax.SelectedNode

    mode = 0
    done = traverse(tvTax.Nodes(0), ndStart, mode, tvTax) ' 0 means to skip the current node nd

    mode = 1
    If Not done Then done = traverse(tvTax.Nodes(0), ndStart, mode, tvTax) ' wrap

  End Sub

  Function traverse(nd As TreeNode, startNode As TreeNode, ByRef mode As Integer, tvTax As TreeView) As Boolean

    ' mode = 0 means to skip nd

    Dim done As Boolean

    If mode = 0 AndAlso nd Is startNode Then
      mode = 1
    Else

      If nd.BackColor = nodeMatchColor Then
        done = done
      End If

      If mode = 1 AndAlso nd.BackColor = nodeMatchColor Then
        tvTax.SelectedNode = nd
        nd.EnsureVisible()
        ' tvTax.Select()
        Return True
      End If
    End If

    For Each ndc As TreeNode In nd.Nodes
      done = traverse(ndc, startNode, mode, tvTax)
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
    linktoID = getScalar("select imageid from images where filename = @parm1 limit 1", linktoFname)
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

    imageID = getScalar("select imageid from images where filename = @parm1 limit 1", fName)

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

    Dim i, imageID, setID As Integer
    Dim taxID As String

    imageID = getScalar("select imageid from images where filename = @parm1 limit 1", fName)
    taxID = getScalar("select taxonid from images where imageid = @parm1 limit 1", imageID)
    setID = getScalar("select setid from imagesets where imageid = @parm1", imageID) ' save to check 1-member sets
    i = nonQuery("delete from imagesets where imageid = @parm1", imageID) ' delete the imageset record
    i = nonQuery("delete from images where filename = @parm1", fName) ' delete the image record

    i = getScalar("select count(*) from imagesets where setid = @parm1", setID) ' how many left in the imageset
    If i = 1 Then ' delete the set if there is only one left
      i = nonQuery("delete from imagesets where setid = @parm1", setID)
    End If

    incImageCounter(taxID, -1)

  End Sub

  Sub deleteImageSets(ByVal fName As String)
    ' delete all imagesets for a filename

    Dim imageID As Integer = 0
    Dim i As Integer

    imageID = getScalar("select imageid from images where filename = @parm1 limit 1", fName)
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

    Dim url As String
    Dim s As String
    Dim dLat, dLon As Double
    Dim jq As JObject

    s = LCase(latlon)
    Call latlonVerify(s, dLat, dLon)

    'link = "http://maps.googleapis.com/maps/api/geocode/xml?sensor=true&latlng=" & Format(dLat, "###.#####") & "," & Format(dLon, "###.#####")
    url = "https://nominatim.openstreetmap.org/reverse?format=json&lat=" & Format(dLat, "###.#####") & "&lon=" & Format(dLon, "###.#####") &
           "&zoom=18&addressdetails=1&email=bob-mud@xpda.com"

    Try
      s = qClient.GetStringAsync(url).Result
    Catch ex As Exception
      MsgBox(ex.Message)
      Exit Sub
    End Try

    jq = JObject.Parse(s)

    If jq.SelectToken("address") IsNot Nothing Then
      If jq.SelectToken("address.city") IsNot Nothing Then
        locale = jq.SelectToken("address.city").ToString
      ElseIf jq.SelectToken("address.town") IsNot Nothing Then
        locale = jq.SelectToken("address.town").ToString
        'ElseIf jq.SelectToken("address.village") IsNot Nothing Then
        '  locale = jq.SelectToken("address.village").ToString
      End If
      If jq.SelectToken("address.county") IsNot Nothing Then county = jq.SelectToken("address.county").ToString Else county = ""
      If jq.SelectToken("address.state") IsNot Nothing Then state = jq.SelectToken("address.state").ToString Else state = ""
      If jq.SelectToken("address.country") IsNot Nothing Then country = jq.SelectToken("address.country").ToString Else country = ""
    End If

    If LCase(county).EndsWith(" county") Then county = county.Substring(0, county.Length - 7)
    If LCase(locale) = "pryor creek" Then locale = "Pryor"
    If states.ContainsKey(state) Then state = states(state) ' get state abbreviation

    If country = "United States" Or country = "USA" Or country = "U.S." Or country = "United States of America" Then country = ""
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

  Function TaxonSearch(findme As String, isQuery As Boolean) As List(Of taxrec)

    ' get dataset taxatable record for taxon or common name findme

    Dim cmd As String
    Dim m As New taxrec
    Dim matches As New List(Of taxrec)
    Dim s As String
    Dim suffix, suffixg As String
    Dim ds As New DataSet
    Dim taxid As String

    findme = findme.Trim

    If findme = "OLYMPUS DIGITAL CAMERA" Then Return matches ' empty list

    If isQuery Then
      suffix = " and (childimagecounter > 0) order by taxon"
      suffixg = " and (childimagecounter > 0) and usable <> '' order by name"
    Else
      suffix = " order by taxon"
      suffixg = "  and usable <> '' order by name"
    End If

    matches = queryTax("select * from taxatable where taxon = @parm1" & suffix, findme)
    If matches.Count = 0 Then
      matches = queryTax("select * from gbif.tax where name = @parm1" & suffixg, findme)
    End If
    If matches.Count = 0 Then
      matches = queryTax("select * from taxatable where (taxon like @parm1)" & suffix, "%" & findme & "%")
      ' If matches.Count = 0 Then matches = queryTax("select * from gbif.tax where (name like @parm1)" & suffixg, findme & "%") ' exact search only for gbif
    End If

    If matches.Count = 0 Then  ' search descr
      s = findme.Replace("-", "`") ' accept either space or dash, so "eastern tailed blue" finds "eastern tailed-blue". Only works with rlike (mysql bug).
      s = s.Replace(" ", "[- ]")
      s = s.Replace("`", "[- ]")
      s = s.Replace("(", "\(")
      s = s.Replace(")", "\)")

      matches = queryTax("select * from oddinfo join taxatable using (taxid) where commonnames rlike @parm1", s)

      If matches.Count = 0 Then matches = queryTax("select * from taxatable where (descr rlike @parm1)" & suffix, s)

      If matches.Count = 0 Then
        ds = getDS("select * from gbif.vernacularname where vernacularname rlike @parm1 and (language = 'en' or language = '')", s)
        'ds = getDS("select * from gbif.vernacularname where vernacularname = @parm1 and (language = 'en' or language = '')", s)
        If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
          cmd = "select * from gbif.tax where ("
          For Each dr As DataRow In ds.Tables(0).Rows
            taxid = dr("taxid")
            If taxid <> "" AndAlso cmd.IndexOf(taxid) < 0 Then cmd &= "taxid = '" & taxid & "' or "
          Next dr
          If cmd.EndsWith(" or ") Then
            cmd = cmd.Substring(0, cmd.Length - 4)
            cmd &= ") and usable <> '' order by name"
            matches = queryTax(cmd, "")
          End If
        End If
      End If

    End If

    Return matches

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

  Sub incImageCounter(ByVal taxid As String, ByVal inc As Integer)

    ' add inc to imagecounter and childimagecounter for taxid and its ancestors

    Dim i As Integer
    Dim imageCounter, childImageCounter As Integer
    Dim parentID As String
    Dim m As taxrec
    Dim matches As List(Of taxrec)

    matches = getTaxrecByID(taxid)
    If matches.Count <> 1 Then Exit Sub ' error
    m = matches(0)

    imageCounter = m.imageCounter
    childImageCounter = m.childimageCounter
    imageCounter += inc
    childImageCounter += inc

    If taxid.StartsWith("g") Then
      i = nonQuery("update gbifplus set imagecounter = @parm1, childimagecounter = @parm2 where taxid = @parm3", _
        imageCounter, childImageCounter, taxid.Substring(1)) ' remove "g"
      If i = 0 Then ' gbifplus doesn't contain all the gbif records
        i = nonQuery("insert into gbifplus (taxid, imagecounter, childimagecounter) values " &
                      "(@parm1, @parm2, @parm3)", taxid.Substring(1), imageCounter, childImageCounter)
      End If
    Else
      i = nonQuery("update taxatable set imagecounter = @parm1, childimagecounter = @parm2 where taxid = @parm3", _
        imageCounter, childImageCounter, taxid)
    End If
    If i <> 1 Then Stop

    parentID = m.parentid
    ' follow parentID up the taxon tree, incrementing childImageCounter
    For k As Integer = 1 To 200
      If parentID = "" Or parentID = "g" Then Exit For

      matches = getTaxrecByID(parentID)
      If matches.Count <> 1 Then Exit For ' error
      m = matches(0)

      childImageCounter = m.childimageCounter
      childImageCounter += inc

      If parentID.StartsWith("g") Then
        i = nonQuery("update gbifplus set childimagecounter = @parm1 where taxid = @parm2",
                     childImageCounter, parentID.Substring(1)) ' remove "g"
        If i = 0 Then ' gbifplus doesn't contain all the gbif records
          i = nonQuery("insert into gbifplus (taxid, childimagecounter) values " &
                        "(@parm1, @parm2)", parentID.Substring(1), childImageCounter)
        End If
        If i <> 1 Then Stop
      Else
        i = nonQuery("update taxatable set childimagecounter = @parm1 where taxid = @parm2", childImageCounter, parentID)
        If i <> 1 Then Stop
      End If
      If i = 1 Then parentID = m.parentid
    Next k

  End Sub

  Function getCaption(ByRef pic As pixClass) As String

    Dim s As String
    Dim s1 As String
    Dim descr As String
    Dim taxonkey As String

    s = ""

    descr = getDescr(pic.match, False)
    If descr <> "" AndAlso Not descr.Contains(":") Then s += descr & "<br>"

    taxonkey = pic.match.taxon
    s += "<i>" & taxonkey & "</i>"
    If String.Compare(pic.match.rank, "species", True) <> 0 And String.Compare(pic.match.rank, "subspecies", True) <> 0 And _
      String.Compare(pic.match.rank, "no taxon", True) <> 0 Then
      s = s & " " & pic.match.rank
    End If
    s += "<br>"

    If descr <> "" AndAlso descr.Contains(":") Then s += descr & "<br>"

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

    taxonkey = pic.match.taxon
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

  Function getChildren(tMatch As taxrec, addon As Boolean, dballowed As Integer, withPics As Boolean) As List(Of taxrec)

    ' get all the immediate children of tmatch, in all database tables

    Dim desc As New List(Of taxrec)
    Dim descg As New List(Of taxrec)
    Dim matches As List(Of taxrec)
    Dim childNames As New List(Of String)
    Dim pics As String = ""

    If withPics Then pics = " and childimagecounter > 0 "

    If (dballowed And 1) Then
      If tMatch.taxid.StartsWith("g") Then
        matches = queryTax("select * from taxatable where taxon = @parm1" & pics, tMatch.taxon)
        For Each m As taxrec In matches
          desc = queryTax("select * from taxatable where parentid = @parm1" & pics, matches(0).taxid)
        Next m
      Else
        desc = queryTax("select * from taxatable where parentid = @parm1" & pics, tMatch.taxid)
      End If

      For Each m As taxrec In desc
        childNames.Add(m.taxon)
      Next m
    End If

    If (dballowed And 8) Then
      If Not tMatch.taxid.StartsWith("g") Then
        matches = queryTax("select * from gbif.tax where name = @parm1 and usable <> ''", tMatch.taxon)
        For Each m As taxrec In matches ' should be 1
          If (Not withPics Or m.childimageCounter > 0) Then
            descg = queryTax("select * from gbif.tax where parent = @parm1", m.taxid.Substring(1))
          End If
        Next m
      Else
        descg = queryTax("select * from gbif.tax where parent = @parm1", tMatch.taxid.Substring(1))
      End If

      For Each m As taxrec In descg
        If (Not withPics Or m.childimageCounter > 0) AndAlso childNames.IndexOf(m.taxon) < 0 Then
          childNames.Add(m.taxon)
          desc.Add(m)
        End If
      Next m
    End If

    Return desc

  End Function

  Function validTaxon(m As taxrec) As String

    Dim ds As DataSet = Nothing
    Dim ds2 As DataSet = Nothing
    Dim ds3 As DataSet = Nothing
    Dim dr As DataRow = Nothing
    Dim dr2 As DataRow = Nothing
    'Dim n, iRow As Integer
    Dim s As String

    s = LCase(m.taxon)
    If s.Split(" ").Length >= 3 OrElse
        s.Contains(" ") OrElse
        s.Contains("""") OrElse
        s.Contains("(") OrElse
        s.Contains("--") OrElse
        s.Contains("-cf-") OrElse
        s.Contains("-new-") OrElse
        s.Contains("-non-") OrElse
        s.Contains("-nr-") OrElse
        s.Contains("-or-") OrElse
        s.Contains("-sp-") OrElse
        s.Contains("-idae") OrElse
        s.Contains(".") OrElse
        s.Contains("/") OrElse
        s.Contains("assigned") OrElse
        s.Contains("adventive") OrElse
        s.Contains("established") OrElse
        s.Contains("incertae") OrElse
        s.Contains("introduction") OrElse
        s.Contains("maybe") OrElse
        s.Contains("near-") OrElse
        s.Contains("possible") OrElse
        s.Contains("possibly") OrElse
        s.Contains("likely") OrElse
        s.Contains("probably") OrElse
        s.Contains("sensu lato") OrElse
        s.Contains("suspected") OrElse
        s.Contains("undescribed") OrElse
        s.Contains("undetermined") OrElse
        s.Contains("known") OrElse
        s.Contains("unnamed") OrElse
        s.Contains("placed") OrElse
        s.EndsWith("-cf") OrElse
        s.EndsWith("-like") OrElse
        s.EndsWith("-sp") OrElse
        s.EndsWith("complex") OrElse
        s.EndsWith("group") OrElse
        s.EndsWith("pseudo") OrElse
        s.StartsWith("cf-") OrElse
        s.StartsWith("n-") OrElse
        s.StartsWith("new-") OrElse
        s.StartsWith("non-") OrElse
        s.StartsWith("nr-") OrElse
        s.StartsWith("on-") OrElse
        s.StartsWith("-xxxx") OrElse
        s.StartsWith("sp-") Then Return "non-taxonomic text in name."

    If Not itisRankID.ContainsKey(m.rank) Then Return "Invalid rank in taxa."

    Return ""

  End Function

  Function allDescendants(tMatch As taxrec, rank As String) As List(Of taxrec)
    ' returns a sorted list of itis + bugguide descendant names, at rank (or all descendants if rank is "")

    Dim children As New List(Of taxrec)
    Dim chil As New List(Of taxrec)
    Dim desc As New List(Of taxrec) ' all the descendants to return
    Dim childName As New List(Of String)
    Dim descName As New List(Of String)
    'Dim validName As String
    Dim recRank As String

    children = getChildren(tMatch, True, 9, False) ' get immediate children, sources = dballowed, include with or without pics

    For i1 As Integer = children.Count - 1 To 0 Step -1
      'validName = validTaxon(children(i1))
      'If validName = "" Then
      recRank = children(i1).rank
      If rank = "" OrElse eqstr(rank, recRank) Then
        If descName.IndexOf(children(i1).taxon) < 0 Then ' add new taxrec
          descName.Add(children(i1).taxon)
          desc.Add(children(i1))
        Else
          children.RemoveAt(i1)
        End If

      ElseIf (itisRankID.ContainsKey(recRank) AndAlso itisRankID(rank) <= itisRankID(recRank)) Then ' rank is as low as target
        children.RemoveAt(i1)
      End If

      'End If
    Next i1

    For Each m As taxrec In children
      chil = allDescendants(m, rank)
      For Each m2 As taxrec In chil
        If descName.IndexOf(m2.taxon) < 0 Then ' add new taxrec
          descName.Add(m2.taxon)
          desc.Add(m2)
        End If
      Next m2
    Next m

    sortTaxrec(desc)

    Return desc

  End Function

  Sub sortTaxrec(ByRef children As List(Of taxrec))
    ' sort a list of taxrecs
    ' this is ugly. I am lazy.

    Dim ix As New List(Of Integer)
    Dim keys As New List(Of String)
    Dim sorted As New List(Of taxrec)

    For i1 As Integer = 0 To children.Count - 1
      ix.Add(i1)
      keys.Add(children(i1).taxon)
      sorted.Add(New taxrec)
    Next i1
    MergeSort(keys, ix, 0, ix.Count - 1)
    For i1 As Integer = 0 To ix.Count - 1
      sorted(i1) = children(ix(i1))
    Next i1
    children = New List(Of taxrec)
    children.AddRange(sorted)

  End Sub

End Module



Public Class pixClass
  Public fName As String
  Public photoDate As DateTime
  Public Location As String
  Public County As String
  Public State As String
  Public Country As String
  Public Size As String
  Public imageid As Integer
  Public Remarks As String
  Public match As taxrec

  Sub New(ByRef dr As DataRow)

    ' sourceDB is blank for taxatable
    Dim s As String

    fName = dr("filename")
    photoDate = dr("photodate")
    Location = dr("location")
    County = dr("county")
    State = dr("state")
    Country = dr("country")
    Size = dr("size")
    imageid = dr("imageid")
    Remarks = dr("remarks")

    s = dr("taxonid")

    If s.StartsWith("g") Then
      match = getTaxrecg(dr)
    Else
      match = getTaxrec(dr)
    End If

  End Sub

End Class

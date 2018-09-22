Imports System.Net
Imports System.Collections.Generic
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Math
Imports System.IO

Imports System.Data
Imports MySql.Data.MySqlClient

Public Class frmBugQuery


  Dim processing As Boolean
  Dim nn As Integer = 0
  Dim folderPath As String

  Private Sub frmBugQuery_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    'QueryTaxon = ""
  End Sub

  Private Sub frmBugQuery_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    Dim s As String = ""
    Dim s1 As String = ""

    processing = True

    'helpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
    'helpProvider1.SetHelpKeyword(Me, Me.Name & ".html")

    tvTaxon.ShowNodeToolTips = True

    txTaxon.Select()

    processing = False
    Me.Cursor = Cursors.Default

  End Sub

  Private Sub cmdTaxon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTaxon.Click

    Dim match As taxrec
    Dim matches As New List(Of taxrec)
    Dim gmatches As New List(Of taxrec)
    Dim nd As TreeNode = Nothing
    Dim ndc As TreeNode = Nothing

    Me.Cursor = Cursors.WaitCursor

    tvTaxon.Nodes.Clear()

    matches = queryTax("select * from taxatable where taxon = @parm1", "arthropoda")
    gmatches = queryTax("select * from gbif.tax where name = @parm1 and usable = 'yes'", "arthropoda")
    matches = mergeMatches(matches, gmatches)

    If matches.Count > 0 Then
      nd = tvTaxon.Nodes.Add(taxaLabel(matches(0), True, True))
      nd.Tag = matches(0).id
    End If

    populate(nd, True)  ' load Arthropoda
    nd.ExpandAll()
    ' Now get down through hexapoda
    For Each ndc In nd.Nodes
      If Mid(ndc.Text, 1, 7) = "Hexapoda" Then
        populate(ndc, True)  ' load Hexapoda
        ndc.Expand()
        Exit For
      End If
    Next ndc

    If txTaxon.Text <> "" Then
      match = popuTaxon(txTaxon.Text, tvTaxon, True)
      txCommon.Text = getDescr(match, False)
    End If

    Me.Cursor = Cursors.Default

  End Sub

  Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

    If txTaxon.Text.Trim = "" Then
      ' txTaxon.Text = "Arthropoda"
      chkDescendants.Checked = True
    End If
    queryExec()

  End Sub

  Sub queryExec()

    Dim cmd As MySqlCommand = Nothing
    Dim imgCmd As MySqlCommand = Nothing
    Dim adapt As New MySqlDataAdapter
    Dim dset As New DataSet
    Dim drow As DataRow
    Dim matches As New List(Of taxrec)
    Dim gmatches As New List(Of taxrec)
    Dim sql As String
    Dim newNames As New List(Of String)
    Dim s, s1, fname As String
    Dim sTaxon() As String
    Dim id As Integer
    Dim setid As Integer

    If tvTaxon.Focused Then Exit Sub
    Me.Cursor = Cursors.WaitCursor

    Using conn As New MySqlConnection(iniDBConnStr)
      conn.Open()
      sql = "select images.filename, taxatable.parentid, taxatable.rank, taxatable.taxon " &
            "  from images, taxatable where images.taxonid = taxatable.id "
      cmd = queryparms(sql, "photodate", True, True, conn)
      If cmd Is Nothing Then
        Me.Cursor = Cursors.Default()
        Exit Sub
      End If

      adapt.SelectCommand = cmd
      adapt.Fill(dset)

      queryNames = New List(Of String)
      folderPath = iniBugPath
      If Not folderPath.EndsWith("\") Then folderPath &= "\"

      s = txTaxon.Text.Trim
      For Each drow In dset.Tables(0).Rows
        s1 = getTaxonKey(drow("parentid"), drow("rank"), drow("taxon"))
        If s = "" OrElse eqstr(s, s1) Then ' taxonkey matches
          If Not IsDBNull(drow("filename")) Then queryNames.Add(folderPath & drow("filename"))
        End If
      Next drow

      If chkDescendants.Checked And txTaxon.Text.Trim <> "" Then
        sTaxon = Split(txTaxon.Text.Trim, " ", 2) ' separate 1st word
        matches = queryTax("select * from taxatable where taxon = @parm1", sTaxon(UBound(sTaxon)))
        gmatches = queryTax(
          "select * from gbif.tax where name like = @parm1 and usable = 'yes'", " %" & sTaxon(UBound(sTaxon)))
        matches = mergeMatches(matches, gmatches)

        'sql = "select images.*, taxatable.parentid, taxatable.rank, taxatable.taxon " &
        '    "  from images, taxatable where images.taxonid = @id "
        'queryparms(sql, "photodate", imgCmd, False, True, conn) ' @id is set in addchildren

        imgCmd = queryparms("select * from images where taxonid = @id ", "photodate", False, False, conn) ' @id is set in addchildren

        s = txTaxon.Text.Trim
        For Each match As taxrec In matches
          s1 = getTaxonKey(match.parentid, match.rank, match.taxon)
          If eqstr(s, s1) Then ' taxonkey matches
            addChildren(match, queryNames, imgCmd)
          End If
        Next match
      End If

      ' add all the images from associated imagesets for queries using filename
      If txFilename.Text <> "" Then
        newNames = New List(Of String)
        For Each fname In queryNames
          id = getScalar("select id from images where filename = @parm1", Path.GetFileName(fname))
          setid = getScalar("select setid from imagesets WHERE imageid = @parm1 limit 1", id)
          dset = getDS("select * from imagesets where setid = @parm1", setid)

          If dset IsNot Nothing Then
            For Each drow In dset.Tables(0).Rows
              If Not IsDBNull(drow("imageid")) Then
                s = folderPath & getScalar("SELECT filename FROM images WHERE id = @parm1", drow("imageid"))
                If Not queryNames.Contains(s) Then newNames.Add(s)
              End If
            Next drow
          End If
        Next fname

        For Each s In newNames ' add the names to querylist - done separately for previous loop
          queryNames.Add(s)
        Next s

      End If

    End Using

    Me.Cursor = Cursors.Default
    Me.DialogResult = DialogResult.OK
    Me.Close()

  End Sub

  Sub addChildren(ByRef inmatch As taxrec, ByRef queryNames As List(Of String), ByRef imgCmd As MySqlCommand)

    ' adds the current tid filename, and recurses for the children

    Dim adapt As New MySqlDataAdapter
    Dim dset As New DataSet
    Dim drow As DataRow
    Dim matches As List(Of taxrec)
    Dim gmatches As List(Of taxrec)
    Dim i As Integer
    Dim s As String

    nn = nn + 1

    ' process the children
    ' change to include children from both databases
    matches = queryTax("select * from taxatable where parentid = @parm1 and childimagecounter > 0", inmatch.id)
    gmatches = queryTax("select * from gbif.tax where parent = @parm1 and childimagecounter > 0", inmatch.id)
    matches = mergeMatches(matches, gmatches)

    For Each match As taxrec In matches
      If match.childimageCounter > 0 Then
        ' grab filenames
        dset.Clear()
        i = imgCmd.Parameters.IndexOf("@id")
        If i >= 0 Then imgCmd.Parameters.RemoveAt(i)
        imgCmd.Parameters.AddWithValue("@id", match.id)
        adapt.SelectCommand = imgCmd
        adapt.Fill(dset)
        For Each drow In dset.Tables(0).Rows
          If Not IsDBNull(drow("filename")) Then
            s = folderPath & drow("filename")
            If Not queryNames.Contains(s) Then queryNames.Add(s)
          End If
        Next drow

        ' process child
        addChildren(match, queryNames, imgCmd)
      End If

    Next match
  End Sub

  Function taxonQsearch(ByVal findme As String, ByVal isQuery As Boolean) As List(Of taxrec)

    Dim taxi As String
    Dim cmd As String
    Dim i As Integer
    Dim suffixg, suffix, suffixp As String
    Dim ds As New DataSet
    Dim matches As List(Of taxrec)

    findme = findme.Trim

    If isQuery Then
      suffix = " and (childimagecounter > 0) order by taxon"
      suffixg = " and (childimagecounter > 0) and usable = 'yes' order by name"
      suffixp = " and (childimagecounter > 0) order by @p"
    Else
      suffix = " order by taxonkey"
      suffixg = "  and usable = 'yes' order by name"
      suffixp = " order by @p"
    End If

    i = InStr(findme, " ")
    If i <= 0 Then ' no space -- one-word search
      matches = queryTax("select * from taxatable where taxon = @parm1" & suffix, findme)
      If matches.Count = 0 Then matches = queryTax("select * from gbif.tax where name = @parm1" & suffixg, findme)
      If matches.Count = 0 Then
        matches = queryTax("select * from taxatable where (taxon like @parm1)" & suffix, findme & "%")
        If matches.Count = 0 Then matches = queryTax("select * from gbif.tax where (name like @parm1)" & suffixg, findme & "%")
      End If

    Else ' multi-word search
      i = InStr(findme, " ")
      taxi = Mid(findme, i + 1) & "%" ' taxi is all but the first word
      ' search genus and species
      matches = queryTax("select * from taxatable where taxon like @parm2 and " &
                 "substr(@p := gettaxonkey(parentid, rank, taxon), 1, instr(@p, ' ') - 1) = @parm1" & suffixp, findme, taxi)
      If matches.Count = 0 Then matches = queryTax("select * from gbif.tax where name = @parm1" & suffixg, findme) ' name includes genus
    End If

    If matches.Count = 0 Then ' search descr
      taxi = findme.Replace("-", "`") ' accept either space or dash, so "eastern tailed blue" finds "eastern tailed-blue". Only works with rlike (mysql bug).
      taxi = taxi.Replace(" ", "[- ]")
      taxi = taxi.Replace("`", "[- ]")
      taxi = "%" & taxi & "%"
      matches = queryTax("select * from taxatable where (descr like @parm1)" & suffix, taxi)

      If matches.Count = 0 Then
        ds = getDS("select * from gbif.vernacularname where vernacularname like @parm1 and language = 'en'", taxi)
        If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
          cmd = "select * from gbif.tax where ("
          For Each dr As DataRow In ds.Tables(0).Rows
            cmd = "taxid = g" & (dr("taxonid")) & " or "
          Next dr
          If cmd.EndsWith(" or ") Then
            cmd = cmd.Substring(0, cmd.Length - 4)
            cmd &= ") and usable = 'yes' order by name"
            matches = queryTax(cmd, "")
          End If
        End If
      End If
    End If

    Return matches

  End Function

  Function queryparms(sql As String, orderBy As String, useTaxon As Boolean, useRank As Boolean,
                      ByRef conn As MySqlConnection) As MySqlCommand

    ' input sql, output cmd with all the query parameters from the text fields appended

    Dim qlist As New List(Of String)
    Dim s As String
    Dim i As Integer
    Dim cmd As New MySqlCommand

    If useTaxon AndAlso txTaxon.Text.Trim <> "" Then qlist.Add("taxatable.taxon = @taxon")
    If useRank AndAlso txRank.Text.Trim <> "" Then qlist.Add("taxatable.rank = @rank")
    If txLocation.Text.Trim <> "" Then qlist.Add("images.location like @location")
    If txCounty.Text.Trim <> "" Then qlist.Add("images.county like @county")
    If txState.Text.Trim <> "" Then qlist.Add("images.state like @state")
    If txCountry.Text.Trim <> "" Then qlist.Add("images.country like @country")
    If txRemarks.Text.Trim <> "" Then qlist.Add("images.remarks like @remarks")
    If txFilename.Text.Trim <> "" Then qlist.Add("images.filename like @filename")
    If IsDate(txDateMin.Text) Then qlist.Add("datediff(@photodatemin, images.photodate) <= 0")
    If IsDate(txDateMax.Text) Then qlist.Add("datediff(@photodatemax, images.photodate) >= 0")
    If IsDate(txModMin.Text) Then qlist.Add("datediff(@modifiedmin, images.modified) <= 0")
    If IsDate(txModMax.Text) Then qlist.Add("datediff(@modifiedmax, images.modified) >= 0")
    If IsNumeric(txRatingMin.Text.Trim) Then qlist.Add("images.rating >= @ratingmin")
    If IsNumeric(txRatingMax.Text.Trim) Then qlist.Add("images.rating <= @ratingmax")
    If IsNumeric(txConfidenceMin.Text.Trim) Then qlist.Add("images.confidence >= @confidencemin")
    If IsNumeric(txConfidenceMax.Text.Trim) Then qlist.Add("images.confidence <= @confidencemax")
    If IsNumeric(txElevationMin.Text.Trim) Then qlist.Add("images.elevation >= @elevationmin")
    If IsNumeric(txElevationMax.Text.Trim) Then qlist.Add("images.elevation <= @elevationmax")

    'If qlist.Count = 0 Then
    '  cmd = Nothing
    '  Exit Sub
    '  End If

    For Each s In qlist
      sql = sql & " and " & s
    Next s
    If orderBy <> "" Then sql = sql & " order by " & orderBy

    cmd = New MySqlCommand(sql, conn)
    If txTaxon.Text.Trim <> "" Then
      s = txTaxon.Text.Trim
      i = s.LastIndexOf(" ")
      If i > 0 Then s = s.Substring(i + 1)
      cmd.Parameters.AddWithValue("@taxon", s)
    End If
    If txLocation.Text.Trim <> "" Then cmd.Parameters.AddWithValue("@location", "%" & txLocation.Text.Trim & "%")
    If txCounty.Text.Trim <> "" Then cmd.Parameters.AddWithValue("@county", "%" & txCounty.Text.Trim & "%")
    If txState.Text.Trim <> "" Then cmd.Parameters.AddWithValue("@state", "%" & txState.Text.Trim & "%")
    If txCountry.Text.Trim <> "" Then cmd.Parameters.AddWithValue("@country", "%" & txCountry.Text.Trim & "%")
    If txRemarks.Text.Trim <> "" Then cmd.Parameters.AddWithValue("@remarks", "%" & txRemarks.Text.Trim & "%")
    If txFilename.Text.Trim <> "" Then cmd.Parameters.AddWithValue("@filename", "%" & txFilename.Text.Trim & "%")
    If txRank.Text.Trim <> "" Then cmd.Parameters.AddWithValue("@rank", txRank.Text.Trim)

    Try
      If txDateMin.Text.Trim <> "" Then cmd.Parameters.AddWithValue("@photodatemin", Format(CDate(txDateMin.Text.Trim), "yyyy-MM-dd"))
    Catch ex As Exception
      MsgBox(ex.Message)
      cmd.Parameters.AddWithValue("@photodate", "1776-07-04")
    End Try
    Try
      If txDateMax.Text.Trim <> "" Then cmd.Parameters.AddWithValue("@photodatemax", Format(CDate(txDateMax.Text.Trim), "yyyy-MM-dd"))
    Catch ex As Exception
      MsgBox(ex.Message)
      cmd.Parameters.AddWithValue("@photodate", "2776-07-04")
    End Try

    Try
      If txModMin.Text.Trim <> "" Then cmd.Parameters.AddWithValue("@modifiedmin", Format(CDate(txModMin.Text.Trim), "yyyy-MM-dd"))
    Catch ex As Exception
      MsgBox(ex.Message)
      cmd.Parameters.AddWithValue("@modified", "1776-07-04")
    End Try
    Try
      If txModMax.Text.Trim <> "" Then cmd.Parameters.AddWithValue("@modifiedmax", Format(CDate(txModMax.Text.Trim), "yyyy-MM-dd"))
    Catch ex As Exception
      MsgBox(ex.Message)
      cmd.Parameters.AddWithValue("@modified", "2776-07-04")
    End Try

    If IsNumeric(txRatingMin.Text) Then cmd.Parameters.AddWithValue("@ratingmin", txRatingMin.Text)
    If IsNumeric(txRatingMax.Text) Then cmd.Parameters.AddWithValue("@ratingmax", txRatingMax.Text)
    If IsNumeric(txConfidenceMin.Text) Then cmd.Parameters.AddWithValue("@confidencemin", txConfidenceMin.Text)
    If IsNumeric(txConfidenceMax.Text) Then cmd.Parameters.AddWithValue("@confidencemax", txConfidenceMax.Text)
    If IsNumeric(txElevationMin.Text) Then cmd.Parameters.AddWithValue("@elevationmin", txElevationMin.Text)
    If IsNumeric(txElevationMax.Text) Then cmd.Parameters.AddWithValue("@elevationmax", txElevationMax.Text)

    Return cmd

  End Function

  Private Sub tvTaxon_AfterCollapse(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles tvTaxon.AfterCollapse

    ' clear the children

    Dim nd As TreeNode

    For Each nd In e.Node.Nodes
      nd.Nodes.Clear()
    Next nd

  End Sub

  Private Sub tvTaxon_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles tvTaxon.AfterSelect
    getTreeviewItem()
  End Sub


  Private Sub tvTaxon_BeforeSelect(ByVal sender As Object, ByVal e As TreeViewCancelEventArgs) Handles tvTaxon.BeforeSelect

    ' load the children

    If e.Node.Nodes.Count = 0 Then
      populate(e.Node, True)
    End If

  End Sub

  Private Sub tvTaxon_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles tvTaxon.KeyDown

    Select Case e.KeyCode

      Case Keys.Space
        gotoNextNode(tvTaxon)
        e.Handled = True

      Case Else

    End Select

  End Sub

  Private Sub getTreeviewItem()

    Dim taxonid As String
    Dim match As New bugMain.taxrec
    Dim matches As List(Of taxrec)

    If processing Or tvTaxon.SelectedNode Is Nothing Then Exit Sub

    taxonid = tvTaxon.SelectedNode.Tag
    matches = getTaxrecByID(taxonid)
    If matches.count <= 0 Then match = New taxrec Else match = matches(0)

    txTaxon.Text = match.taxonkey
    txCommon.Text = getDescr(match, False)

  End Sub

  Private Sub frmQuery_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

    If Not checkDB(iniDBConnStr) Then
      MsgBox("The database could not be found.", MsgBoxStyle.OkOnly)
      Me.DialogResult = DialogResult.Cancel
      Me.Close()
      Exit Sub
    End If

    cmdTaxon_Click(sender, e)

    'If QueryTaxon <> "" Then
    ' txTaxon.Text = QueryTaxon
    'cmdOK_Click(sender, e)
    'End If

  End Sub

  Private Sub tvTaxon_PreviewKeyDown(ByVal sender As Object, ByVal e As PreviewKeyDownEventArgs) Handles tvTaxon.PreviewKeyDown

    If e.KeyValue = Keys.Enter Then
      cmdOK.Select()
      cmdOK_Click(sender, e)
    End If

  End Sub

  Private Sub tx_Enter(ByVal Sender As Object, ByVal e As EventArgs) Handles txCountry.Enter, txConfidenceMin.Enter, txConfidenceMax.Enter, _
    txCounty.Enter, txDateMin.Enter, txDateMax.Enter, txModMin.Enter, txModMax.Enter, txElevationMin.Enter, txElevationMax.Enter, txFilename.Enter, txLocation.Enter, _
    txRatingMin.Enter, txRatingMax.Enter, txRemarks.Enter, txState.Enter, txTaxon.Enter

    Dim tx As TextBox
    Dim rtx As RichTextBox

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


  Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
    Me.DialogResult = DialogResult.Cancel
    Me.Close()
  End Sub

End Class
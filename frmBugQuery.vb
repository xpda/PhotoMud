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

    Dim cmd As MySqlCommand = Nothing
    Dim adapt As New MySqlDataAdapter
    Dim dset As New DataSet

    Dim match As New taxrec
    Dim nd As TreeNode = Nothing
    Dim ndc As TreeNode = Nothing

    Me.Cursor = Cursors.WaitCursor

    tvTaxon.Nodes.Clear()

    dset = getDS("SELECT * FROM taxatable WHERE taxon = @parm1", "Arthropoda")

    If dset IsNot Nothing AndAlso dset.Tables(0).Rows.Count > 0 Then
      getTaxon(dset.Tables(0).Rows(0), match)
      nd = tvTaxon.Nodes.Add(taxaLabel(match, True, True))
      nd.Tag = match.id
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
      queryparms(sql, "photodate", cmd, True, True, conn)
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
        dset = getDS("SELECT * FROM taxatable WHERE taxon = @parm1", sTaxon(UBound(sTaxon))) ' search for the last words
        If dset Is Nothing Then Exit Sub ' error

        'sql = "select images.*, taxatable.parentid, taxatable.rank, taxatable.taxon " &
        '    "  from images, taxatable where images.taxonid = @id "
        'queryparms(sql, "photodate", imgCmd, False, True, conn) ' @id is set in addchildren

        queryparms("select * from images where taxonid = @id ", "photodate", imgCmd, False, False, conn) ' @id is set in addchildren

        s = txTaxon.Text.Trim
        For Each drow In dset.Tables(0).Rows
          s1 = getTaxonKey(drow("parentid"), drow("rank"), drow("taxon"))
          If eqstr(s, s1) Then ' taxonkey matches
            addChildren(drow, queryNames, imgCmd)
          End If
        Next drow
      End If

      ' add all the images from associated imagesets for queries using filename
      If txFilename.Text <> "" Then
        newNames = New List(Of String)
        For Each fname In queryNames
          id = getScalar("SELECT id FROM images WHERE filename = @parm1", Path.GetFileName(fname))
          setid = getScalar("SELECT setid FROM imagesets WHERE imageid = @parm1 limit 1", id)
          dset = getDS("SELECT * FROM imagesets WHERE setid = @parm1", setid)

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

  Sub addChildren(ByRef taxRow As DataRow, ByRef queryNames As List(Of String), ByRef imgCmd As MySqlCommand)

    ' adds the current tid filename, and recurses for the children

    Dim adapt As New MySqlDataAdapter
    Dim dset, tdset As New DataSet
    Dim drow, tdrow As DataRow
    Dim match As taxrec = Nothing
    Dim i As Integer
    Dim s As String

    nn = nn + 1

    ' process the children
    tdset = getDS("SELECT * FROM taxatable WHERE parentid = @parm1 and childimagecounter > 0", taxRow("id"))

    If tdset IsNot Nothing Then
      For Each tdrow In tdset.Tables(0).Rows
        If Not IsDBNull(tdrow("childimagecounter")) AndAlso tdrow("childimagecounter") > 0 Then
          ' grab filenames
          dset.Clear()
          i = imgCmd.Parameters.IndexOf("@id")
          If i >= 0 Then imgCmd.Parameters.RemoveAt(i)
          imgCmd.Parameters.AddWithValue("@id", tdrow("id"))
          adapt.SelectCommand = imgCmd
          adapt.Fill(dset)
          For Each drow In dset.Tables(0).Rows
            If Not IsDBNull(drow("filename")) Then
              s = folderPath & drow("filename")
              If Not queryNames.Contains(s) Then queryNames.Add(s)
            End If
          Next drow

          ' process child
          addChildren(tdrow, queryNames, imgCmd)
        End If

      Next tdrow
    End If
  End Sub

  Function TaxonkeySearch(ByVal findme As String, ByVal isQuery As Boolean) As DataSet

    Dim taxi As String
    Dim i As Integer
    Dim suffix, suffixp As String
    Dim ds As New DataSet

    findme = findme.Trim

    If isQuery Then
      suffix = " and (childimagecounter > 0) order by taxon"
      suffixp = " and (childimagecounter > 0) order by @p"
    Else
      suffix = " order by taxonkey"
      suffixp = " order by @p"
    End If

    i = InStr(findme, " ")
    If i <= 0 Then ' no space -- on-word search
      ds = getDS("select * from taxatable where taxon = @parm1" & suffix, findme)
      If ds Is Nothing OrElse ds.Tables(0).Rows.Count = 0 Then
        taxi = findme & "%"
        ds = getDS("select * from taxatable where (taxon like @parm1)" & suffix, taxi)
      End If

    Else ' multi-word search
      i = InStr(findme, " ")
      taxi = Mid(findme, i + 1) & "%" ' taxi is all but the first word
      ' search genus and species
      ds = getDS("select * from taxatable where taxon like @parm2 and substr(@p := gettaxonkey(parentid, rank, taxon), 1, instr(@p, ' ') - 1) = @parm1" & suffixp, findme, taxi)
    End If

    If ds Is Nothing OrElse ds.Tables(0).Rows.Count <= 0 Then ' search descr
      taxi = findme.Replace("-", "`") ' accept either space or dash, so "eastern tailed blue" finds "eastern tailed-blue". Only works with rlike (mysql bug).
      taxi = taxi.Replace(" ", "[- ]")
      taxi = taxi.Replace("`", "[- ]")
      taxi = "%" & taxi & "%"
      ds = getDS("select * from taxatable where (descr like @parm1)" & suffix, taxi)
    End If

    Return ds

  End Function

  Sub queryparms(ByRef sql As String, ByVal orderBy As String, ByRef cmd As MySqlCommand, ByVal useTaxon As Boolean, ByVal useRank As Boolean, ByRef conn As MySqlConnection)

    ' input sql, output cmd with all the query parameters from the text fields appended

    Dim qlist As New List(Of String)
    Dim s As String
    Dim i As Integer

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

  End Sub

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

    Dim taxonid As Integer
    Dim match As New bugMain.taxrec

    If processing Or tvTaxon.SelectedNode Is Nothing Then Exit Sub

    taxonid = Int(tvTaxon.SelectedNode.Tag)
    getTaxonByID(taxonid, match)

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
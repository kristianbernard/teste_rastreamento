Imports clsSmart
Imports System.IO
Partial Class restrito_permissoes
    Inherits System.Web.UI.Page
    Dim tb1 As New Data.DataTable
    Dim tab1 As New clsBanco
    Dim tbtodos As New Data.DataTable
    Private Sub restrito_permissoes_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack Then Exit Sub

        'tb1 = tab1.conectar("select * from tbgrupos where ativo = true order by descricao")
        'cbogrupo.Items.Add("")
        'For x As Integer = 0 To tb1.Rows.Count - 1
        '    cbogrupo.Items.Add(tb1.Rows(x)("descricao").ToString)
        'Next

        Session("operacao") = "Novo"
        txtdescricao.ReadOnly = True
        GradeForms.Enabled = False
        GradeFormsSel.Enabled = False
        btnadd.Enabled = False
        btnremove.Enabled = False
        Session("nrseqpermissao") = 0
        carregargrade()
        tbtodos.Rows.Clear()
        tbtodos.Columns.Clear()
        tbtodos.Columns.Add("nome")

        For x As Integer = 0 To Directory.GetFiles(Server.MapPath("~\restrito\"), "*.aspx").Length - 1
            If mARQUIVO(Directory.GetFiles(Server.MapPath("~\restrito\"), "*.aspx")(x).ToString).ToLower.Contains("erro.aspx") OrElse mARQUIVO(Directory.GetFiles(Server.MapPath("~\restrito\"), "*.aspx")(x).ToString).ToLower.Contains("ops2.aspx") Then
                Continue For
            End If
            tbtodos.Rows.Add()
            tbtodos.Rows(tbtodos.Rows.Count - 1)("nome") = mARQUIVO(Directory.GetFiles(Server.MapPath("~\restrito\"), "*.aspx")(x).ToString)
        Next
        carregarlistasem(Session("nrseqpermissao"))
        carregarlistacom(Session("nrseqpermissao"))
    End Sub
    Private Sub carregarlistacom(nrseq As Integer)
        Dim tb2 As New Data.DataTable
        Dim tbnaotem As New Data.DataTable
        tbnaotem.Rows.Clear()
        tbnaotem.Columns.Clear()
        tbnaotem.Columns.Add("arquivo")
        Dim tab2 As New clsBanco
        tb2 = tab2.conectar("select * from tbpermissoesdth where nrseqpermissao = " & nrseq & " and ativo = true ")

        GradeFormsSel.DataSource = tb2
        GradeFormsSel.DataBind()
    End Sub
    Private Sub carregarlistasem(nrseq As Integer)
        tbtodos.Rows.Clear()
        tbtodos.Columns.Clear()
        tbtodos.Columns.Add("nome")

        For x As Integer = 0 To Directory.GetFiles(Server.MapPath("~\restrito\"), "*.aspx").Length - 1
            If mARQUIVO(Directory.GetFiles(Server.MapPath("~\restrito\"), "*.aspx")(x).ToString).ToLower.Contains("erro.aspx") OrElse mARQUIVO(Directory.GetFiles(Server.MapPath("~\restrito\"), "*.aspx")(x).ToString).ToLower.Contains("ops2.aspx") Then
                Continue For
            End If
            tbtodos.Rows.Add()
            tbtodos.Rows(tbtodos.Rows.Count - 1)("nome") = mARQUIVO(Directory.GetFiles(Server.MapPath("~\restrito\"), "*.aspx")(x).ToString)
        Next

        Dim tb2 As New Data.DataTable
        Dim tbnaotem As New Data.DataTable
        tbnaotem.Rows.Clear()
        tbnaotem.Columns.Clear()
        tbnaotem.Columns.Add("pagina")
        Dim tab2 As New clsBanco
        tb2 = tab2.conectar("select * from tbpermissoesdth where nrseqpermissao = " & nrseq & " and ativo = true ")
        Dim sqlcompara As String = ""
            For x As Integer = 0 To tbtodos.Rows.Count - 1
            Dim tbproc As Data.DataRow()
            tbproc = tb2.Select(" pagina = '" & tbtodos.Rows(x)("nome").ToString & "'")
            If tbproc.Length = 0 Then
                tbnaotem.Rows.Add()
                tbnaotem.Rows(tbnaotem.Rows.Count - 1)("pagina") = tbtodos.Rows(x)("nome").ToString
            End If
        Next

        GradeForms.DataSource = tbnaotem
        GradeForms.DataBind()




    End Sub
    Private Sub carregargrade()
        tb1 = tab1.conectar("select * from tbpermissoes where " & IIf(chkExibirExcluidos.Checked, "1=1", " ativo = true ") & " order by descricao")
        grade.DataSource = tb1
        grade.DataBind()
    End Sub

    Private Sub btnNova_Click(sender As Object, e As EventArgs) Handles btnNova.Click
        Dim wcnrseqctrl As String = gerarnrseqcontrole()
        tb1 = tab1.IncluirAlterarDados("insert into tbpermissoes (nrseqctrl, ativo, dtcad, empresa, usercad) values ('" & wcnrseqctrl & "', false, '" & formatadatamysql(data) & "','" & zeros(Session("idempresaemuso"), 2) & "','" & Session("usuario") & "')")
        tb1 = tab1.conectar("select * from tbpermissoes where nrseqctrl = '" & wcnrseqctrl & "'")
        txtcodigo.Text = tb1.Rows(0)("nrseq").ToString
        Session("nrseqpermissao") = txtcodigo.Text
        Session("operacao") = "Novo"
        txtdescricao.ReadOnly = False
        btnSalvar.Enabled = True
        btnNova.Enabled = False
        GradeForms.Enabled = True
        GradeFormsSel.Enabled = True
        btnadd.Enabled = True
        btnremove.Enabled = True
        txtdescricao.ReadOnly = False
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "escondeexibiralerta2()", "escondeexibiralerta2()", True)
        tb1 = tab1.conectar("Select * from tbpermissoes where empresa = '" & zeros(Session("idempresaemuso"), 2) & "' and descricao = '" & txtdescricao.Text & "'")
        If tb1.Rows.Count <> 0 AndAlso Session("operacao") = "Novo" Then
            lblerroconta1.Text = "Essa permissão já existe !"
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "exibiralerta2()", "exibiralerta2()", True)
            Exit Sub
        End If
        If Session("operacao") = "Novo" Then
            tb1 = tab1.IncluirAlterarDados("update tbpermissoes set usercad = '" & Session("usuario") & "', descricao = '" & txtdescricao.Text & "', ativo = true where nrseq = " & txtcodigo.Text)
        Else
            tb1 = tab1.IncluirAlterarDados("update tbpermissoes set  ativo = true where nrseq = " & txtcodigo.Text)
        End If
        txtdescricao.ReadOnly = False
        txtdescricao.Text = ""
        txtcodigo.Text = ""
        txtdescricao.ReadOnly = True
        btnNova.Enabled = True
        btnSalvar.Enabled = False
        GradeForms.Enabled = False
        GradeFormsSel.Enabled = False
        btnadd.Enabled = False
        btnremove.Enabled = False
        Session("nrseqpermissao") = 0
        carregargrade()
        carregarlistasem(Session("nrseqpermissao"))
        carregarlistacom(Session("nrseqpermissao"))
        Session("operacao") = "Novo"
    End Sub

    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click



        Dim tbadd As New Data.DataTable
        Dim tabadd As New clsBanco
        For x As Integer = 0 To GradeForms.Rows.Count - 1

            Dim linha As GridViewRow = GradeForms.Rows(x)


            Dim sel As CheckBox = CType(linha.FindControl("sel"), CheckBox)
            Dim lblnomegrade As Label = CType(linha.FindControl("lblnomegrade"), Label)
            If sel.Checked Then

                tb1 = tab1.IncluirAlterarDados("insert into tbpermissoesdth (nrseqpermissao, ativo, pagina) values (" & Session("nrseqpermissao") & ", true, '" & lblnomegrade.Text & "')")


            End If
        Next

        carregarlistasem(Session("nrseqpermissao"))
        carregarlistacom(Session("nrseqpermissao"))


    End Sub

    Private Sub btnremove_Click(sender As Object, e As EventArgs) Handles btnremove.Click
        Dim tbadd As New Data.DataTable
        Dim tabadd As New clsBanco
        For x As Integer = 0 To GradeFormsSel.Rows.Count - 1

            Dim linha As GridViewRow = GradeFormsSel.Rows(x)


            Dim sel As CheckBox = CType(linha.FindControl("sel"), CheckBox)
            If sel.Checked Then

                tb1 = tab1.IncluirAlterarDados("update tbpermissoesdth set ativo = false where nrseqpermissao = " & Session("nrseqpermissao") & " and  pagina = '" & linha.Cells(1).Text & "'")



            End If
        Next

        carregarlistasem(Session("nrseqpermissao"))
        carregarlistacom(Session("nrseqpermissao"))
    End Sub

    Private Sub grade_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grade.RowCommand
        Dim existe As Boolean = False
        'If e.CommandArgument = "" Then Exit Sub
        ' If multiple buttons are used in a GridView control, use the
        ' CommandName property to determine which button was clicked.

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim txtlinha As Integer
        ' Retrieve the row that contains the button clicked 
        ' by the user from the Rows collection.
        Dim row As GridViewRow = grade.Rows(index)
        txtlinha = index
        ' Create a new ListItem object for the contact in the row.     

        Dim lblnrseq As Label = row.FindControl("lblnrseq")

        Dim linha As GridViewRow = grade.Rows(txtlinha)

        ' For Each rowh As GridViewRow In Grade.Rows

        Dim tbsalvar As New Data.DataTable
        Dim tabsalvar As New clsBanco
        Select Case e.CommandName.ToLower
            Case Is = "excluir"
                tb1 = tab1.IncluirAlterarDados("update tbpermissoes set ativo = false, dtexclui = '" & Formatadatamysql(data) & "', userexclui = '" & Session("usuario") & "' where nrseq = " & numeros(lblnrseq.Text))
                carregargrade()
                GradeFormsSel.DataSource = Nothing
                GradeFormsSel.DataBind()
                txtcodigo.Text = ""
                txtdescricao.Text = ""

            Case Is = "alterar"

                tb1 = tab1.conectar("select * from tbpermissoes where nrseq = " & numeros(lblnrseq.Text))
                If tb1.Rows.Count <> 0 Then
                    txtdescricao.Text = tb1.Rows(0)("descricao").ToString
                    Session("nrseqpermissao") = tb1.Rows(0)("nrseq").ToString
                    txtcodigo.Text = tb1.Rows(0)("nrseq").ToString
                    carregarlistasem(Session("nrseqpermissao"))
                    carregarlistacom(Session("nrseqpermissao"))
                    txtdescricao.ReadOnly = False
                    btnSalvar.Enabled = True
                    btnNova.Enabled = False
                    GradeForms.Enabled = True
                    GradeFormsSel.Enabled = True
                    txtdescricao.ReadOnly = True
                    btnadd.Enabled = True
                    btnremove.Enabled = True
                    Session("operacao") = tb1.Rows(0)("nrseq").ToString
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "ir_topo()", "ir_topo()", True)

                End If
            Case Is = "ver"
                tb1 = tab1.conectar("select * from tbpermissoes where nrseq = " & row.Cells(1).Text)
                If tb1.Rows.Count <> 0 Then
                    txtdescricao.Text = tb1.Rows(0)("descricao").ToString
                    Session("nrseqpermissao") = tb1.Rows(0)("nrseq").ToString
                    txtcodigo.Text = tb1.Rows(0)("nrseq").ToString
                    carregarlistasem(Session("nrseqpermissao"))
                    carregarlistacom(Session("nrseqpermissao"))
                    txtdescricao.ReadOnly = False

                    GradeForms.Enabled = True
                    GradeFormsSel.Enabled = True

                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "ir_topo()", "ir_topo()", True)
                End If
        End Select
    End Sub

    Private Sub grade_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grade.RowDataBound
        If e.Row.RowIndex < 0 Then Exit Sub
        Dim hdativo As HiddenField = e.Row.FindControl("hdativo")

        If hdativo.Value <> "1" Then
            e.Row.ForeColor = System.Drawing.Color.Gray
            e.Row.Font.Strikeout = True
        End If
        If IsDate(e.Row.Cells(2).Text) Then e.Row.Cells(2).Text = FormatDateTime(e.Row.Cells(2).Text, DateFormat.ShortDate)
    End Sub

    Private Sub chkExibirExcluidos_CheckedChanged(sender As Object, e As EventArgs) Handles chkExibirExcluidos.CheckedChanged
        carregargrade()
    End Sub

    Private Sub GradeForms_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GradeForms.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            Dim selcabecalho As CheckBox = e.Row.FindControl("selcabecalho")
            ' AddHandler selcabecalho.CheckedChanged, AddressOf seltodos0
        End If

    End Sub
    Public Sub seltodos0(sender As Object, e As EventArgs)
        Dim selgrade As New CheckBox
        For Each rowh As GridViewRow In GradeForms.Rows
            If rowh.RowType = DataControlRowType.Header Then
                selgrade = rowh.FindControl("selcabecalho")
            End If

        Next
        For Each rowh As GridViewRow In GradeForms.Rows

            If rowh.RowType = DataControlRowType.DataRow Then
                Dim sel As CheckBox = rowh.FindControl("sel")
                sel.Checked = selgrade.Checked
            End If
        Next
    End Sub

    Private Sub chkseltodos01_CheckedChanged(sender As Object, e As EventArgs) Handles chkseltodos01.CheckedChanged
        For Each rowh As GridViewRow In GradeForms.Rows

            If rowh.RowType = DataControlRowType.DataRow Then
                Dim sel As CheckBox = rowh.FindControl("sel")
                sel.Checked = chkseltodos01.Checked
            End If
        Next
    End Sub

    Private Sub chkseltodos02_CheckedChanged(sender As Object, e As EventArgs) Handles chkseltodos02.CheckedChanged
        For Each rowh As GridViewRow In GradeFormsSel.Rows

            If rowh.RowType = DataControlRowType.DataRow Then
                Dim sel As CheckBox = rowh.FindControl("sel")
                sel.Checked = chkseltodos02.Checked
            End If
        Next
    End Sub
End Class

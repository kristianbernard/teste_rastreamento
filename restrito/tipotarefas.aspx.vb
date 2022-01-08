Imports clsSmart
Partial Class tipotarefas
    Inherits System.Web.UI.Page

    Private Sub tipotarefas_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsPostBack Then
            Exit Sub
        End If


        carregagrade()
        carregatarefas()
        txtdescricao.Enabled = False
        cbotarefas.Enabled = False
        btnsalvar.Enabled = False
        btnNovo.Enabled = True
        rpttarefas.DataSource = Nothing
        rpttarefas.DataBind()
    End Sub
    Private Sub carregatarefas(Optional xnrseq As Integer = 0)
        Dim tbx As New Data.DataTable
        Dim tabx As New clsBanco

        Dim xsql As String = ""

        If xnrseq <> 0 Then
            xsql &= " and nrseq = " & xnrseq
        End If
        If cbotarefas.Text <> "" Then
            xsql &= " and descricao = '" & cbotarefas.Text & "'"
        End If

        tbx = tabx.conectar("select * from tbtarefas where ativo = true" & xsql)
        cbotarefas.Items.Clear()
        cbotarefas.Items.Add("")
        For x As Integer = 0 To tbx.Rows.Count - 1
            cbotarefas.Items.Add(tbx.Rows(x)("descricao").ToString)
        Next
        If tbx.Rows.Count = 1 Then
            hdnrseqtarefa.Value = numeros(tbx.Rows(0)("nrseq").ToString)
        End If

    End Sub
    Private Sub carregagrade()
        Dim tbdados As New Data.DataTable
        tbdados.Columns.Add("nrseq")
        tbdados.Columns.Add("descricao")

        Dim tbx As New Data.DataTable
        Dim tabx As New clsBanco
        Dim tb1 As New Data.DataTable
        Dim tab1 As New clsBanco
        tbx = tabx.conectar("select * from tbtipotarefa where ativo = true")
        Grade.DataSource = tbx
        Grade.DataBind()
        For Each row As GridViewRow In Grade.Rows
            Dim rpttarefasrpt As Repeater = row.FindControl("rpttarefasrpt")
            Dim hdnrseqrpt As HiddenField = row.FindControl("hdnrseqrpt")

            tbx = tabx.conectar("select * from tbtipo_tarefa where ativo = true and nrseqtipotarefa = " & numeros(hdnrseqrpt.Value))
            tb1 = tab1.conectar("select * from tbtarefas where ativo = true")
            Dim tbproc As Data.DataRow()
            For x As Integer = 0 To tbx.Rows.Count - 1
                tbproc = tb1.Select(" nrseq = " & numeros(tbx.Rows(x)("nrseqtarefa").ToString))
                If tbproc.Length > 0 Then
                    tbdados.Rows.Add()
                    tbdados.Rows(tbdados.Rows.Count - 1)("nrseq") = tbproc(0)("nrseq").ToString
                    tbdados.Rows(tbdados.Rows.Count - 1)("descricao") = tbproc(0)("descricao").ToString
                End If
            Next
            rpttarefasrpt.DataSource = tbdados
            rpttarefasrpt.DataBind()
        Next
    End Sub
    Private Sub limpar()
        txtdescricao.Text = ""
        cbotarefas.Text = ""
        hdnrseq.Value = 0
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Dim tb1 As New Data.DataTable
        Dim tab1 As New clsBanco

        Dim wcnrseqctrl As String = gerarnrseqcontrole()
        tb1 = tab1.IncluirAlterarDados("insert into tbtipotarefa (nrseqctrl, dtcad, usercad, ativo) values ('" & wcnrseqctrl & "','" & hoje() & "','" & Session("usuario") & "', false)")
        tb1 = tab1.conectar("Select * from  tbtipotarefa where nrseqctrl = '" & wcnrseqctrl & "'")
        hdnrseq.Value = numeros(tb1.Rows(0)("nrseq").ToString)

        txtdescricao.Enabled = True
        cbotarefas.Enabled = True
        btnsalvar.Enabled = True
        btnNovo.Enabled = False

    End Sub
    Private Sub sm(funcao As String, Optional nome As String = "")
        If nome = "" Then
            nome = funcao
        End If
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), nome, funcao, True)
    End Sub
    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        Dim tb1 As New Data.DataTable
        Dim tab1 As New clsBanco
        Dim tbx As New Data.DataTable
        Dim tabx As New clsBanco

        tbx = tabx.conectar("select * from tbtipo_tarefa where ativo = true and nrseqtarefa = " & numeros(hdnrseqtarefa.Value) & " and nrseqtipotarefa = " & numeros(hdnrseq.Value))

        If tbx.Rows.Count = 1 Then
            sm("swal({title: 'Atenção',text: 'o tipo de tarefa já existe!',type: 'error',confirmButtonText: 'OK'})", "swal")
            Exit Sub
        End If

        Dim wcnrseqctrl As String = gerarnrseqcontrole()
        tb1 = tab1.IncluirAlterarDados("insert into tbtipo_tarefa (nrseqctrl, dtcad, usercad, ativo, nrseqtarefa, nrseqtipotarefa) values ('" & wcnrseqctrl & "','" & hoje() & "','" & Session("usuario") & "', true, " & numeros(hdnrseqtarefa.Value) & ", " & numeros(hdnrseq.Value) & ")")
    End Sub
    Private Sub carregarpttarefas(Optional xnrseq As Integer = 0)
        Dim tbx As New Data.DataTable
        Dim tabx As New clsBanco
        Dim tb1 As Data.DataTable
        Dim tab1 As New clsBanco

        Dim tbdados As New Data.DataTable

        tbdados.Columns.Add("nrseq")
        tbdados.Columns.Add("descricao")

        If xnrseq = 0 Then
            xnrseq = numeros(hdnrseqtarefa.Value)
        End If

        tbx = tabx.conectar("select * from tbtipo_tarefa where ativo = true and nrseqtipotarefa = " & xnrseq)
        tb1 = tab1.conectar("select * from tbtarefas where ativo = true")
        Dim tbproc As Data.DataRow()
        For x As Integer = 0 To tbx.Rows.Count - 1
            tbproc = tb1.Select(" nrseq = " & numeros(tbx.Rows(x)("nrseqtarefa").ToString))
            If tbproc.Length > 0 Then
                tbdados.Rows.Add()
                tbdados.Rows(tbdados.Rows.Count - 1)("nrseq") = tbproc(0)("nrseq").ToString
                tbdados.Rows(tbdados.Rows.Count - 1)("descricao") = tbproc(0)("descricao").ToString
            End If
        Next
        rpttarefas.DataSource = tbdados
        rpttarefas.DataBind()

    End Sub
    Private Sub btnsalvar_Click(sender As Object, e As EventArgs) Handles btnsalvar.Click
        Dim tb1 As New Data.DataTable
        Dim tab1 As New clsBanco
        Dim tbx As New Data.DataTable
        Dim tabx As New clsBanco

        'tbx = tabx.conectar("select * from tbtipotarefa where ativo = true and descricao = '" & txtdescricao.Text & "'")

        'If tbx.Rows.Count = 1 Then
        '    sm("swal({title: 'Atenção',text: 'o tipo de tarefa já existe!',type: 'error',confirmButtonText: 'OK'})", "swal")
        '    Exit Sub
        'End If

        Dim xsql As String = " update tbtipotarefa set ativo = true, descricao = '" & txtdescricao.Text & "' where nrseq = " & numeros(hdnrseq.Value)

        tb1 = tab1.IncluirAlterarDados(xsql)



        txtdescricao.Enabled = False
        btnsalvar.Enabled = False
        btnNovo.Enabled = True
        cbotarefas.Enabled = False
        limpar()
        carregagrade()
        rpttarefas.DataSource = Nothing
        rpttarefas.DataBind()

    End Sub

    Private Sub cbotarefas_TextChanged(sender As Object, e As EventArgs) Handles cbotarefas.TextChanged
        carregatarefas()
    End Sub
    Private Sub Grade_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Grade.RowCommand
        Dim existe As Boolean = False
        If e.CommandArgument = "" Then Exit Sub

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)

        Dim row As GridViewRow = Grade.Rows(index)

        Dim tbsalvar As New Data.DataTable
        Dim tabsalvar As New clsBanco

        Dim lbldecricaograde As Label = row.FindControl("lbldecricaograde")
        Dim hdnrseqrpt As HiddenField = row.FindControl("hdnrseqrpt")

        Dim tbx As New Data.DataTable
        Dim tabx As New clsBanco

        Select Case e.CommandName.ToLower
            Case Is = "editar"
                tbx = tabx.conectar("select * from tbtipotarefa where ativo = true and descricao = '" & lbldecricaograde.Text & "'")

                txtdescricao.Text = tbx.Rows(0)("descricao").ToString
                hdnrseq.Value = numeros(tbx.Rows(0)("nrseq").ToString)
                carregarpttarefas()
                btnNovo.Enabled = False
                btnsalvar.Enabled = True
                txtdescricao.Enabled = True
                cbotarefas.Enabled = True
            Case Is = "excluir"


                tbsalvar = tabsalvar.IncluirAlterarDados("update tbtipotarefa set ativo=false, dtexclui = '" & Formatadatamysql(data) & "', userexclui = '" & Session("usuario") & "' where nrseq = " & numeros(hdnrseqrpt.Value))
                carregagrade()

        End Select
    End Sub
End Class

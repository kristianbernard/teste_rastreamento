Imports clsSmart
Partial Class perfil
    Inherits System.Web.UI.Page

    Private Sub perfil_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsPostBack Then
            Exit Sub
        End If


        carregagrade()
        txtperfil.Enabled = False

        btnsalvar.Enabled = False
        btnNovo.Enabled = True
    End Sub
    Private Sub carregagrade()
        Dim tbx As New Data.DataTable
        Dim tabx As New clsBanco
        tbx = tabx.conectar("select * from tbperfil where ativo = true")
        Grade.DataSource = tbx
        Grade.DataBind()
    End Sub
    Private Sub limpar()
        txtperfil.Text = ""
        hdnrseq.Value = 0
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Dim tb1 As New Data.DataTable
        Dim tab1 As New clsBanco

        Dim wcnrseqctrl As String = gerarnrseqcontrole()
        tb1 = tab1.IncluirAlterarDados("insert into tbperfil (nrseqctrl, dtcad, usercad, ativo) values ('" & wcnrseqctrl & "','" & hoje() & "','" & Session("usuario") & "', false)")
        tb1 = tab1.conectar("Select * from  tbperfil where nrseqctrl = '" & wcnrseqctrl & "'")
        hdnrseq.Value = numeros(tb1.Rows(0)("nrseq").ToString)

        txtperfil.Enabled = True

        btnsalvar.Enabled = True
        btnNovo.Enabled = False

    End Sub
    Private Sub sm(funcao As String, Optional nome As String = "")
        If nome = "" Then
            nome = funcao
        End If
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), nome, funcao, True)
    End Sub
    Private Sub btnsalvar_Click(sender As Object, e As EventArgs) Handles btnsalvar.Click
        Dim tb1 As New Data.DataTable
        Dim tab1 As New clsBanco
        Dim tbx As New Data.DataTable
        Dim tabx As New clsBanco

        'tbx = tabx.conectar("select * from tbperfil where ativo = true and descricao = '" & txtperfil.Text & "'")

        'If tbx.Rows.Count = 1 Then
        '    sm("swal({title: 'Atenção',text: 'O perfil já existe!',type: 'error',confirmButtonText: 'OK'})", "swal")
        '    Exit Sub
        'End If

        Dim xsql As String = " update tbperfil set ativo = true, descricao = '" & txtperfil.Text & "' where nrseq = " & numeros(hdnrseq.Value)
        tb1 = tab1.IncluirAlterarDados(xsql)
        txtperfil.Enabled = False
        btnsalvar.Enabled = False
        btnNovo.Enabled = True

        limpar()
        carregagrade()

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
                tbx = tabx.conectar("select * from tbperfil where ativo = true and descricao = '" & lbldecricaograde.Text & "'")

                txtperfil.Text = tbx.Rows(0)("descricao").ToString
                hdnrseq.Value = numeros(tbx.Rows(0)("nrseq").ToString)
                txtperfil.Enabled = True
                btnNovo.Enabled = False
                btnsalvar.Enabled = True

            Case Is = "excluir"


                tbsalvar = tabsalvar.IncluirAlterarDados("update tbperfil set ativo=false, dtexclui = '" & Formatadatamysql(data) & "', userexclui = '" & Session("usuario") & "' where nrseq = " & numeros(hdnrseqrpt.Value))
                carregagrade()

        End Select
    End Sub
End Class

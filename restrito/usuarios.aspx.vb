Imports clsSmart

' ##############################
' Alterado po Kristian Bernard
' Alterado dia: 29/07/2021
' ##############################
Partial Class restrito_usuarios
    Inherits System.Web.UI.Page
    Dim tb1 As New Data.DataTable
    Dim tab1 As New clsBanco
    Dim tberro As New Data.DataTable
    Private Sub restrito_usuarios_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack Then Exit Sub


        Session("nrseq") = 0
        Session("opera") = ""
        carregapermissao()
        carregargrade()
        carregaperfil()
        carregatipoapontamento()
    End Sub
    Private Sub carregargrade()

        tb1 = tab1.conectar("select * from tbusuarios where ativo = true and usuario <> '' order by usuario")

        Grade.DataSource = tb1
        Grade.DataBind()

        Dim tbx As New Data.DataTable
        Dim tabx As New clsBanco

        tbx = tabx.conectar("select * from tbpermissoes where ativo = true")

        Dim tbproc As Data.DataRow()

        Dim xcont As Integer = 0

        For Each row As GridViewRow In Grade.Rows
            If Session("rastreamento") = 0 Then
                Dim btnalterargrade As LinkButton = row.FindControl("btnalterargrade")
                Dim btnexcluirgrade As LinkButton = row.FindControl("btnexcluirgrade")
                Dim btnsuspendergrade As LinkButton = row.FindControl("btnsuspendergrade")

                Dim lblusuariograde As Label = row.FindControl("lblusuariograde")
                Dim lblpermissaograde As Label = row.FindControl("lblpermissaograde")

                tbproc = tbx.Select(" nrseq = " & numeros(tb1.Rows(xcont)("nrseqpermissao".ToString)))
                lblpermissaograde.Text = tbproc(0)("descricao").ToString

                If lblpermissaograde.Text <> "Adm" Then
                    btnalterargrade.Enabled = False
                    btnexcluirgrade.Enabled = False
                    btnsuspendergrade.Enabled = False

                End If

            End If
        Next

    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        btnsalvar.Enabled = True
        btnNovo.Enabled = False
        Session("opera") = "inclui"
        Dim wcnrseqctrl As String = gerarnrseqcontrole()
        tb1 = tab1.IncluirAlterarDados("insert into tbusuarios (dtcad, usercad, ativo, nrseqctrl) values ('" & Formatadatamysql(data) & "','" & Session("usuario") & "', false, '" & wcnrseqctrl & "')")
        tb1 = tab1.conectar("select * from tbusuarios where nrseqctrl = '" & wcnrseqctrl & "'")
        Session("nrseq") = tb1.Rows(0)("nrseq").ToString
        txtnome.Focus()
    End Sub

    Private Sub btnsalvar_Click(sender As Object, e As EventArgs) Handles btnsalvar.Click
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "escondeexibiralerta2()", "escondeexibiralerta2()", True)
        Session("erro") = "nao"
        tberro.Columns.Clear()
        tberro.Rows.Clear()
        tberro.Columns.Add("erro")
        If txtnome.Text = "" Then
            tberro.Rows.Add()
            tberro.Rows(tberro.Rows.Count - 1)("erro") = "Digite um nome de usuário válido !"
            Session("erro") = "sim"
        Else
            If Session("opera") = "inclui" Then
                tb1 = tab1.conectar("select * from tbusuarios where usuario = '" & txtnome.Text & "'")
                If tb1.Rows.Count <> 0 Then
                    tberro.Rows.Add()
                    tberro.Rows(tberro.Rows.Count - 1)("erro") = "O usuário " & txtnome.Text & " já existe ! Digite um nome diferente !"
                    Session("erro") = "sim"
                End If
            End If
        End If
        If cbopermissao.Text = "" Then
            tberro.Rows.Add()
            tberro.Rows(tberro.Rows.Count - 1)("erro") = "Selecione uma permissão válida !"
            Session("erro") = "sim"
        End If
        If txtsenha.Text = "" Then
            tberro.Rows.Add()
            tberro.Rows(tberro.Rows.Count - 1)("erro") = "Digite uma senha válida !"
            Session("erro") = "sim"
        End If
        If Session("erro") = "sim" Then
            gradeerro.DataSource = tberro
            gradeerro.DataBind()
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "exibiralerta2()", "exibiralerta2()", True)
            Exit Sub
        End If


        btnNovo.Enabled = True
        btnsalvar.Enabled = False
        tb1 = tab1.IncluirAlterarDados("update tbusuarios set ativo = true, usuario = '" & txtnome.Text & "', senha = '" & txtsenha.Text & "', email = '" & txtemail.Text & "', nrseqpermissao = '" & numeros(hdnrseqpermissao.Value) & "', nrseqperfil = " & numeros(hdnrseqperfil.Value) & ", nrseqtipoapontamento = " & numeros(cboapontamento.SelectedValue) & " where nrseq = " & Session("nrseq"))
        carregargrade()
        limpar()

    End Sub
    Private Sub limpar()
        txtnome.Text = ""
        cboapontamento.Text = ""
        txtsenha.Text = ""
        cbopermissao.Text = ""
        txtemail.Text = ""
        Session("nrseq") = 0
    End Sub

    Private Sub Grade_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Grade.RowCommand
        Dim existe As Boolean = False
        If e.CommandArgument = "" Then Exit Sub
        ' If multiple buttons are used in a GridView control, use the
        ' CommandName property to determine which button was clicked.

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim txtlinha As Integer
        ' Retrieve the row that contains the button clicked 
        ' by the user from the Rows collection.
        Dim row As GridViewRow = Grade.Rows(index)
        txtlinha = index
        ' Create a new ListItem object for the contact in the row.     



        Dim linha As GridViewRow = Grade.Rows(txtlinha)

        ' For Each rowh As GridViewRow In Grade.Rows
        Dim arquivo As String = linha.Cells(0).Text
        Dim tbsalvar As New Data.DataTable
        Dim tabsalvar As New clsBanco


        Select Case e.CommandName.ToLower
            Case Is = "alterar"
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "irtopo()", "irtopo()", True)
                tbsalvar = tabsalvar.conectar("select * from tbusuarios where ativo = true and nrseq = " & linha.Cells(1).Text)
                txtemail.Text = tbsalvar.Rows(0)("email").ToString
                txtnome.Text = tbsalvar.Rows(0)("usuario").ToString
                cbopermissao.Text = tbsalvar.Rows(0)("permissao").ToString
                cboapontamento.SelectedValue = numeros(tbsalvar.Rows(0)("nrseqtipoapontamento").ToString)
                btnNovo.Enabled = False
                btnsalvar.Enabled = True
                Session("nrseq") = tbsalvar.Rows(0)("nrseq").ToString
                Session("opera") = "altera"

            Case Is = "suspender"


                tbsalvar = tabsalvar.IncluirAlterarDados("update tbusuarios set suspenso=true where nrseq = " & linha.Cells(1).Text)
                carregargrade()

            Case Is = "excluir"


                tbsalvar = tabsalvar.IncluirAlterarDados("update tbusuarios set ativo=false, dtexclui = '" & Formatadatamysql(data) & "', userexclui = '" & Session("usuario") & "' where nrseq = " & linha.Cells(1).Text)
                carregargrade()

        End Select
    End Sub

    Private Sub Grade_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Grade.RowDataBound
        If e.Row.RowIndex >= 0 Then
            Dim hdativo As HiddenField = e.Row.FindControl("hdativo")
            Dim hdsuspenso As HiddenField = e.Row.FindControl("hdsuspenso")

            If logico(hdsuspenso.Value) Then
                e.Row.ForeColor = System.Drawing.Color.White
                e.Row.BackColor = System.Drawing.Color.Red

            End If
            If logico(hdativo.Value) = "False" Then
                e.Row.BackColor = System.Drawing.Color.Gray
                e.Row.ForeColor = System.Drawing.Color.White
                e.Row.Font.Strikeout = True
            End If
            If logico(hdsuspenso.Value) = "False" AndAlso logico(hdativo.Value) Then
                e.Row.ForeColor = System.Drawing.Color.White
                e.Row.BackColor = System.Drawing.Color.Green
            End If
        End If



    End Sub

    Private Sub cbopermissao_TextChanged(sender As Object, e As EventArgs) Handles cbopermissao.TextChanged
        Dim tbx As New Data.DataTable
        Dim tabx As New clsBanco
        tbx = tabx.conectar("select * from tbpermissoes where descricao = '" & cbopermissao.Text & "' ativo = true")
        hdnrseqpermissao.Value = numeros(tbx.Rows(0)("nrseq").ToString)
    End Sub
    Private Sub cboperfil_TextChanged(sender As Object, e As EventArgs) Handles cboperfil.TextChanged
        Dim tbx As New Data.DataTable
        Dim tabx As New clsBanco
        tbx = tabx.conectar("select * from tbperfil where descricao = '" & cboperfil.Text & "' and ativo = true")
        hdnrseqperfil.Value = numeros(tbx.Rows(0)("nrseq").ToString)
    End Sub
    Private Sub carregatipoapontamento()
        Dim tbx As New Data.DataTable
        Dim tabx As New clsBanco

        Dim xsql As String = "select * from tbtipo_apontamento where ativo = true"

        xsql &= " order by descricao"

        tbx = tabx.conectar(xsql)
        cboapontamento.Items.Clear()
        cboapontamento.Items.Add("")
        For x As Integer = 0 To tbx.Rows.Count - 1
            cboapontamento.Items.Add(tbx.Rows(x)("descricao").ToString)
            cboapontamento.Items(cboapontamento.items.count - 1).value = numeros(tbx.Rows(x)("nrseq").ToString)
        Next
    End Sub
    Private Sub carregaperfil(Optional xnrseq As Integer = 0)
        Dim tbx As New Data.DataTable
        Dim tabx As New clsBanco

        Dim xsql As String = "select * from tbperfil where ativo = true"
        If xnrseq <> 0 Then
            xsql &= " and nrseq = " & xnrseq
        End If

        xsql &= " order by descricao"

        tbx = tabx.conectar(xsql)
        cboperfil.Items.Clear()
        cboperfil.Items.Add("")
        For x As Integer = 0 To tbx.Rows.Count - 1
            cboperfil.Items.Add(tbx.Rows(x)("descricao").ToString)
        Next
    End Sub
    Private Sub carregapermissao(Optional xnrseq As Integer = 0)


        Dim tbx As New Data.DataTable
        Dim tabx As New clsBanco
        Dim xsql As String = "select * from tbpermissoes where ativo = true"
        If xnrseq <> 0 Then
            xsql &= " and nrseq = " & xnrseq
        End If

        xsql &= " order by descricao"

        tbx = tabx.conectar(xsql)
        cbopermissao.Items.Clear()
        cbopermissao.Items.Add("")
        For x As Integer = 0 To tbx.Rows.Count - 1
            cbopermissao.Items.Add(tbx.Rows(x)("descricao").ToString)
        Next
    End Sub
End Class

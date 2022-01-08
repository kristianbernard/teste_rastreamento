Imports clsSmart
Partial Class restrito_relatoriolog
    Inherits System.Web.UI.Page
    Dim xmensagem As String = ""
    Dim xapontamento As String = ""


    Private Sub restrito_relatoriolog_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack Then
            Exit Sub
        End If
        carregatarefas()
        cbotipo.Enabled = False
        lblusuario.Text = HttpContext.Current.Session("usuario")
    End Sub
    Private Sub carregatarefas()
        Dim tb1 As New Data.DataTable
        Dim tab1 As New clsBanco
        tb1 = tab1.conectar("select * from tbtarefas where ativo = true")
        cbotarefas.Items.Clear()
        cbotarefas.Items.Add("")
        For x As Integer = 0 To tb1.Rows.Count - 1
            cbotarefas.Items.Add(tb1.Rows(x)("descricao").ToString)
        Next
    End Sub

    Private Sub sm(funcao As String, Optional nome As String = "")
        If nome = "" Then
            nome = funcao
        End If
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), nome, funcao, True)
    End Sub

    Private Sub cbotarefas_TextChanged(sender As Object, e As EventArgs) Handles cbotarefas.TextChanged
        Dim tb1 As New Data.DataTable
        Dim tab1 As New clsBanco
        Dim tbx As New Data.DataTable
        Dim tabx As New clsBanco
        Dim tbz As New Data.DataTable
        Dim tabz As New clsBanco

        tb1 = tab1.conectar("select * from tbtarefas where ativo = true and descricao = '" & cbotarefas.Text & "'")

        hdnrseqtarefa.value = numeros(tb1.Rows(0)("nrseq").ToString)

        tbx = tabx.conectar("select * from tbtipo_tarefa where ativo = true and nrseqtarefa = " & numeros(tb1.Rows(0)("nrseq").ToString))

        tbz = tabz.conectar("select * from tbtipotarefa where ativo = true")

        Dim tbproc As Data.DataRow()

        cbotipo.Items.Clear()
        cbotipo.Items.Add("")

        For x As Integer = 0 To tbx.Rows.Count - 1
            tbproc = tbz.Select(" nrseq = " & numeros(tbx.Rows(x)("nrseqtipotarefa").ToString))
            cbotipo.Items.Add(tbproc(0)("descricao").ToString)
            cbotipo.Items(cbotipo.Items.Count - 1).Value = tbproc(0)("nrseq").ToString
        Next
        sm("formatacampo();")
        cbotipo.Enabled = True
    End Sub

    Private Sub btnsalvar_Click(sender As Object, e As EventArgs) Handles btnsalvar.Click
        'If cbotarefas.Text = "" Then
        '    sm("swal({title: 'Atenção!',text: 'O campo tarefa é obrigatório!',type: 'error',confirmButtonText: 'OK'})", "swal")
        '    Exit Sub
        'End If
        'If cbotipo.Text = "" Then
        '    sm("swal({title: 'Atenção!',text: 'O campo tipo é obrigatório!',type: 'error',confirmButtonText: 'OK'})", "swal")
        '    Exit Sub
        'End If
        'If txtdata.Text = "" Then
        '    sm("swal({title: 'Atenção!',text: 'O campo data é obrigatório!',type: 'error',confirmButtonText: 'OK'})", "swal")
        '    Exit Sub
        'End If
        'If txthrfim.Text = "" Then
        '    sm("swal({title: 'Atenção!',text: 'O campo fim é obrigatório!',type: 'error',confirmButtonText: 'OK'})", "swal")
        '    Exit Sub
        'End If
        'If txthrinicio.Text = "" Then
        '    sm("swal({title: 'Atenção!',text: 'O campo início é obrigatório!',type: 'error',confirmButtonText: 'OK'})", "swal")
        '    Exit Sub
        'End If
        Dim tb1 As New Data.DataTable
        Dim tab1 As New clsBanco

        tb1 = tab1.conectar("select * from tbtarefas_agendadas where ativo = true and nrsequsuario = " & numeros(HttpContext.Current.Session("clienteID")))

        If tb1.Rows.Count = 0 Then
        Else
            Dim xhorainicio As Date = CType(txthrinicio.Text, Date)
            Dim xhorafim As Date = CType(txthrfim.Text, Date)

            If numeros(txthrfim.Text.Substring(0, 2)) >= numeros(txthrinicio.Text.Substring(0, 2)) Then
            Else
                xhorafim = xhorafim.AddDays(1)
            End If


            Dim xduracao = xhorafim.Subtract(xhorainicio)

            If Not verificahoras(xduracao.TotalMinutes) Then
                sm("swal({title: 'Atenção!',text: '" & xmensagem & "',type: 'error',confirmButtonText: 'OK'})", "swal")
                Exit Sub
            Else

                Dim a As Integer = numeros(xduracao.TotalMinutes) / 60
                Dim b As Integer = numeros(xduracao.TotalMinutes) Mod 60

                Dim tbz As New Data.DataTable
                Dim tabz As New clsBanco

                tbz = tabz.conectar("select * from tbtarefas_agendadas where ativo = true and nrsequsuario = " & numeros(HttpContext.Current.Session("clienteID")) & " and hrinicio = '" & txthrinicio.Text & "' and hrfim = '" & txthrfim.Text & "' and data = '" & txtdata.Text & "'")

                If tbz.Rows.Count > 0 Then
                    sm("swal({title: 'Atenção!',text: 'Horário indiponível!',type: 'error',confirmButtonText: 'OK'})", "swal")
                    Exit Sub
                End If

                tb1 = tab1.IncluirAlterarDados("insert into tbtarefas_agendadas (nrsequsuario, nrseqtarefa, nrseqtipotarefa, descricao, hrinicio, hrfim, duracao, ativo, usercad, dtcad, data) values (" & numeros(HttpContext.Current.Session("clienteID")) & ", " & numeros(hdnrseqtarefa.Value) & ", " & numeros(cbotipo.SelectedValue) & ", '" & txtdescricao.Text & "', '" & txthrinicio.Text & "', '" & txthrfim.Text & "', '" & Format(a, "##0") & ":" & Format(b, "00") & "', TRUE, '" & HttpContext.Current.Session("usuario") & "', '" & hoje() & "', '" & txtdata.Text & "')")

                sm("swal({title: 'Ótimo!',text: 'Log time realizado com sucesso!',type: 'success',confirmButtonText: 'OK'})", "swal")

                lblaviso.Text = xmensagem
            End If
        End If


    End Sub

    Private Sub txthrfim_TextChanged(sender As Object, e As EventArgs) Handles txthrfim.TextChanged
        divlblaviso.Style.Add("display", "none")
        lblaviso.Text = ""
    End Sub

    Private Sub txthrinicio_TextChanged(sender As Object, e As EventArgs) Handles txthrinicio.TextChanged
        divlblaviso.Style.Add("display", "none")
        lblaviso.Text = ""
    End Sub

    Private Sub txtdata_TextChanged(sender As Object, e As EventArgs) Handles txtdata.TextChanged
        divlblaviso.Style.Add("display", "none")
        lblaviso.Text = ""
    End Sub

    Private Sub cbotipo_TextChanged(sender As Object, e As EventArgs) Handles cbotipo.TextChanged
        sm("formatacampo();")
    End Sub

    Private Function verificahoras(xminadd As Integer) As Boolean
        Dim tb1 As New Data.DataTable
        Dim tab1 As New clsBanco


        tb1 = tab1.conectar("select * from tbtarefas_agendadas where ativo = true and nrsequsuario = " & numeros(HttpContext.Current.Session("clienteID")))


        Dim xlisthoras As New List(Of String)

        For x As Integer = 0 To tb1.Rows.Count - 1
            xlisthoras.Add(tb1.Rows(x)("duracao").ToString)

        Next

        Dim xmin As Integer = 0

        Dim xduracao = ""
        Dim xhoras As Integer
        Dim xhorastotais As Date


        For x As Integer = 0 To xlisthoras.Count - 1

            xhoras = xhoras + CType((xlisthoras(x).ToString.Substring(0, 2)), Integer)
            xmin = xmin + CType((xlisthoras(x).ToString.Substring(3)), Integer)

        Next


        Dim xhoratotal As Integer = ((xhoras * 60) + xmin) + xminadd



        If xhoratotal > 480 Then
            If verificapermissao() = "banco" Then
                If Not verificasaldohoras(xminadd) Then
                    xmensagem = "A duração de atividades excede 40hr mensais!"
                    Return False
                End If
            ElseIf verificapermissao() = "extra" Then
                If Not verificasaldohoras(xminadd) Then
                    xmensagem = "A duração de atividades excede 80hr mensais!"
                    Return False
                End If
            ElseIf verificapermissao() = "" Then
                xmensagem = "A duração de atividades excede 8hr diárias!"
                Return False
            End If
        Else
            Dim xminutosfalta As Integer = 480 - xhoratotal
            Dim a As Integer = xminutosfalta / 60
            Dim b As Integer = xminutosfalta Mod 60

            xmensagem = "Você tem " & Format(a, "##0") & ":" & Format(b, "00") & " horas restantes!"
            Return True
        End If


    End Function

    Private Function verificapermissao() As String
        If numeros(HttpContext.Current.Session("nrseqtipoapontamento")) = 2 Then
            xapontamento = "banco"
            Return "banco"
        ElseIf numeros(HttpContext.Current.Session("nrseqtipoapontamento")) = 3 Then
            xapontamento = "extra"
            Return "extra"
        Else
            Return ""
        End If

    End Function
    Private Function verificasaldohoras(xminadd As Integer)
        Try
            Dim tb1 As New Data.DataTable
            Dim tab1 As New clsBanco

            Dim xdata As String = valordata(txtdata.Text)

            Dim xsql As String = "select * from tbtarefas_agendadas where ativo = true and month(data) = '" & xdata.Substring(3, 2) & "' and year(data) = '" & xdata.Substring(6) & "' and nrsequsuario = " & numeros(HttpContext.Current.Session("clienteID"))

            tb1 = tab1.conectar(xsql)

            Dim xmin As Integer = 0

            Dim xduracao = ""
            Dim xhoras As Integer
            Dim xhorastotais As Date

            If tb1.Rows.Count > 0 Then
                Dim xlisthoras As New List(Of String)

                For x As Integer = 0 To tb1.Rows.Count - 1
                    xlisthoras.Add(tb1.Rows(x)("duracao").ToString)
                Next

                For x As Integer = 0 To xlisthoras.Count - 1

                    xhoras = xhoras + CType((xlisthoras(x).ToString.Substring(0, 2)), Integer)
                    xmin = xmin + CType((xlisthoras(x).ToString.Substring(3)), Integer)

                Next

                Dim xhoratotal As Integer = ((xhoras * 60) + xmin) + xminadd

                If xapontamento = "banco" Then
                    If xhoratotal > 2400 Then
                        Return False
                    Else
                        Dim xminutosfalta As Integer = 2400 - xhoratotal
                        Dim a As Integer = xminutosfalta / 60
                        Dim b As Integer = xminutosfalta Mod 60

                        xmensagem = "Você tem " & Format(a, "##0") & ":" & Format(b, "00") & " horas restantes!"
                    End If
                ElseIf xapontamento = "extra" Then
                    If xhoratotal > 4800 Then
                        Return False
                    Else
                        Dim xminutosfalta As Integer = 4800 - xhoratotal
                        Dim a As Integer = xminutosfalta / 60
                        Dim b As Integer = xminutosfalta Mod 60

                        xmensagem = "Você tem " & Format(a, "##0") & ":" & Format(b, "00") & " horas restantes!"
                    End If
                End If


            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub carregarptarefas()
        Dim tb1 As New Data.DataTable
        Dim tab1 As New clsBanco

        Dim xsql As String = "select * from tbtarefas_agendadas where ativo = true and nrsequsuario = " & numeros(HttpContext.Current.Session("clienteID"))

        If txtdtinicio.Text <> "" Then
            If valordata(txtdtinicio.Text) <= valordata(txtdtfim.Text) Then
                xsql &= " and data >= '" & Formatadatamysql(txtdtinicio.Text).ToString.Replace("/", "-") & "'"
            Else
                sm("swal({title: 'Atenção!',text: 'A data de inicio selecionada é maior que a data de fim',type: 'error',confirmButtonText: 'OK'})", "swal")
                Exit Sub
            End If
        End If

        If txtdtfim.Text <> "" Then
            If valordata(txtdtinicio.Text) <= valordata(txtdtfim.Text) Then
                xsql &= " and data <= '" & Formatadatamysql(txtdtfim.Text).ToString.Replace("/", "-") & "'"
            Else
                sm("swal({title: 'Atenção!',text: 'A data de fim selecionada é menor que a data de inicio',type: 'error',confirmButtonText: 'OK'})", "swal")
                Exit Sub
            End If
        End If

        tb1 = tab1.conectar(xsql)

        rpttarefas.DataSource = tb1
        rpttarefas.DataBind()

        Dim tbx As New Data.DataTable
        Dim tabx As New clsBanco

        For Each row As RepeaterItem In rpttarefas.Items
            Dim lbldatarpt As Label = row.FindControl("lbldatarpt")
            lbldatarpt.Text = valordata(lbldatarpt.Text)

            Dim rptarefasdth As Repeater = row.FindControl("rptarefasdth")

            xsql = "select tbtarefas_agendadas.*, tbtarefas.descricao as tarefa from tbtarefas_agendadas inner join tbtarefas on tbtarefas.nrseq = tbtarefas_agendadas.nrseqtarefa where tbtarefas_agendadas.ativo = true and tbtarefas_agendadas.nrsequsuario = " & numeros(HttpContext.Current.Session("clienteID")) & " and tbtarefas_agendadas.data = '" & Formatadatamysql(lbldatarpt.Text).ToString.Replace("/", "-") & "'"

            tbx = tabx.conectar(xsql)

            rptarefasdth.DataSource = tbx
            rptarefasdth.DataBind()

        Next
    End Sub
    Private Sub btnpesquisa_Click(sender As Object, e As EventArgs) Handles btnpesquisa.Click
        carregarptarefas()
    End Sub

    Private Sub rpttarefas_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rpttarefas.ItemCommand
        Dim divtasks As HtmlContainerControl = e.Item.FindControl("divtasks")
        Select Case e.CommandName
            Case Is = "abrir"
                divtasks.Style.Remove("display")
        End Select
    End Sub

    Private Sub rpttarefas_ItemCreated(sender As Object, e As RepeaterItemEventArgs) Handles rpttarefas.ItemCreated
        If Not e.Item.ItemType = ListItemType.Item And Not e.Item.ItemType = ListItemType.AlternatingItem Then
            Exit Sub
        End If

        Dim rptarefasdth As Repeater = e.Item.FindControl("rptarefasdth")


        AddHandler rptarefasdth.ItemCommand, AddressOf rptarefasdth_ItemCommand
    End Sub
    Private Sub rptarefasdth_ItemCommand(sender As Object, e As RepeaterCommandEventArgs)

        Dim tb1 As New Data.DataTable
        Dim tab1 As New clsBanco

        Dim hdnrseqtarefasrpt As HiddenField = e.Item.FindControl("hdnrseqtarefasrpt")

        Select Case e.CommandName
            Case Is = "excluir"
                tb1 = tab1.IncluirAlterarDados("update tbtarefas_agendadas set ativo = false where nrseq = " & numeros(hdnrseqtarefasrpt.Value))
                carregarptarefas()

        End Select


    End Sub
End Class


Partial Class masterPage_master

    Inherits System.Web.UI.MasterPage

    Private Sub MasterPage_Init(sender As Object, e As EventArgs) Handles Me.Init
        'clssessoes.versessao()
        Dim metadescription As HtmlMeta = New HtmlMeta
        metadescription.Name = "Description"
        metadescription.Content = "RASTREAMENTO"
        Page.Header.Controls.Add(metadescription)

    End Sub

    Private Sub sm(funcao As String, Optional nome As String = "")
        If nome = "" Then
            nome = funcao
        End If
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), nome, funcao, True)
    End Sub

    Private Sub masterPage_master_Load(sender As Object, e As EventArgs) Handles Me.Load


        Response.Redirect("login.aspx")

    End Sub

End Class


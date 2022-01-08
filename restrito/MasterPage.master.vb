Imports clsSmart
Imports clssessoes
Imports dllfrases
Partial Class restrito_MasterPage
    Inherits System.Web.UI.MasterPage

    Private Sub restrito_MasterPage_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack Then Exit Sub

        If Session("usuario") = "" Then
            Response.Redirect("~/login.aspx")
        End If
        lblusuario.Text = Session("usuario")

    End Sub


    Private Sub restrito_MasterPage_Init(sender As Object, e As EventArgs) Handles Me.Init
        clssessoes.versessao()
    End Sub


End Class


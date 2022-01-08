
Partial Class restrito_acessonegado
    Inherits System.Web.UI.Page

    Private Sub restrito_acessonegado_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblnome.Text = HttpContext.Current.Session("nomeusuario")
    End Sub
End Class

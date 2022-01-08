Imports clsSmart
Imports System.IO
Imports clssessoes

Partial Class restrito_Default
    Inherits System.Web.UI.Page

    Private Sub restrito_Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack Then Exit Sub


    End Sub

    Private Sub sm(funcao As String, Optional nome As String = "")
        If nome = "" Then
            nome = funcao
        End If
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), nome, funcao, True)
    End Sub

End Class

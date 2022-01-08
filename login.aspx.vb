Imports System.IO
Imports clsSmart
Imports System.Threading
Partial Class login
    Inherits System.Web.UI.Page
    Private Sub login_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack Then Exit Sub


        divdadosentrada.Style.Remove("display")


        carregaimagens()

    End Sub

    Private Sub carregaimagens()
        Dim Files = SearchForFiles(Server.MapPath("~/imglogin/"), {"*.png", "*.jpg", "*.jpeg", "*.gif"})

        For i As Integer = 0 To Files.Count - 1
            Files.Item(i) = Files.Item(i).Replace(Files.Item(i).Substring(0, Files.Item(i).IndexOf("\imglogin") + 1), "")
        Next

        If Files.Count > 0 Then

            For i As Integer = 0 To Files.Count - 1
                Dim divcontrol As HtmlGenericControl = New HtmlGenericControl("div")
                Dim imgcontrol As HtmlGenericControl = New HtmlGenericControl("img")
                slideshow.Controls.Add(divcontrol)
                imgcontrol.Attributes.Add("src", Files.Item(i))
                divcontrol.Controls.Add(imgcontrol)
            Next


        End If




        If File.Exists(Server.MapPath("~") & "\social\" & Session("logocliente")) Then
            'imlogo.ImageUrl = Server.MapPath("~") & "img\cevanovo.png" Request.Url.OriginalString.Replace(Request.Path, "/social/" & Session("logocliente"))

            Dim xred As New clsredimensiona

            xred.Arquivo = (Server.MapPath("~") & "social\" & Session("logocliente"))

            If xred.redimensionar Then
                imlogo.Style.Remove("height")
                imlogo.Style.Remove("width")
                imlogo.Style.Add("height", xred.Novaaltura & "rem")
                imlogo.Style.Add("width", xred.Novalargura & "rem")
            End If
            imlogo.ImageUrl = Request.Url.OriginalString.Replace(Request.Path, "/social/" & Session("logocliente"))

        Else
            imlogo.Visible = False
        End If
    End Sub

    Private Sub btnentrar_Click(sender As Object, e As EventArgs) Handles btnentrar.Click
        Dim login As New clslogin

        login.Usuario = txtuser.Text
        login.Senha = txtpassword.Text

        If Not login.logar Then
            sm("swal({title: 'Atenção',text: '" & login.Mensagemerro & "',type: 'error',confirmButtonText: 'OK'})", "swal")

            Exit Sub
        End If

        Response.Redirect(login.Urlretorno)
    End Sub
    Private Sub sm(funcao As String, Optional nome As String = "")
        If nome = "" Then
            nome = funcao
        End If
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), nome, funcao, True)
    End Sub
End Class

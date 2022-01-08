<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="assets/plugins/sweetalert2/sweetalert2.all.min.js"></script>
    <title>Rastreamento - Login</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script
        src="https://code.jquery.com/jquery-3.4.1.slim.js"
        integrity="sha256-BTlTdQO9/fascB1drekrDVkaKd9PkwBymMlHOiG+qLI="
        crossorigin="anonymous"></script>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <script src="https://code.jquery.com/jquery-3.4.1.js"></script>
      <script src="/js/jquery-1.9.0.js"></script>
      
</head>

<body>
    <form runat="server">
        <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
        <div class="row" style="width: 100%;">


            <style>
                #slideshow > div img {
                    width: 100%;
                    height: 100vh;
                    position: absolute;
                    object-fit: cover;
                }
            </style>



            <div class="col-xl-8 col-lg-8 col-md-8 col-sm-6 col-4">

                <div runat="server" id="slideshow">
                </div>


            </div>

            <script type="text/javascript">

                $(document).ready(function () {
                    $("#slideshow > div:gt(0)").hide();

                    var count = $("#slideshow").children().length;

                    if (count > 1) {

                        setInterval(function () {
                            $('#slideshow > div:first')
                                .fadeOut(2000)
                                .next()
                                .fadeIn(2000)
                                .end()
                                .appendTo('#slideshow');
                        }, 8000);
                    }
                });

                function executa() {
                    $("#slideshow > div:gt(0)").hide();

                    var count = $("#slideshow").children().length;

                    if (count > 1) {

                        setInterval(function () {
                            $('#slideshow > div:first')
                                .fadeOut(2000)
                                .next()
                                .fadeIn(2000)
                                .end()
                                .appendTo('#slideshow');
                        }, 8000);
                    }
                };

            </script>


            <div class="col-xl-4 col-lg-4 col-md-4 <%--col-sm-6 col-8--%>" style="background-color: white; position: absolute; top: 0; bottom: 0; right: 0; background-color: white;">
                <div class="container" style="padding: 8%;">


                    <div style="margin-bottom: 5%; margin-top: 5%;" class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xl-12">
                            <center>
                            <asp:Image ID="imlogo" runat="server" />
                                </center>
                            <%--<img style="width: 200px;" src="favicon.ico" />--%>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xl-12 text-center">
                            <p><asp:label runat="server"   Text="Bem-Vindo(a)!" ID="lblbemvindo"></asp:label> </p> 
                            <br />
                            <b>
                                <%--<asp:label runat="server"   Text="<%$Resources:Resource,idpadextenso%>" ID="lblmensagemextenso"></asp:label>--%>
                                
                              </b>
                        </div>
                        <br />

                    </div>
                    <div id="divdadosentrada" runat="server">
                        <asp:Panel ID="panellogin" runat="server"  DefaultButton="btnentrar">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xl-12 text-center">
                                <br />
                                <p>
                                    <asp:label runat="server"   Text="Por favor, entre com seu login e senha" ID="lblidacceslogin"></asp:label>
                                    </p>
                                
                            </div>
                            <br />

                        </div> 

                        <div class="row">
                            <div class="col-lg-11 col-md-11 col-sm-11 col-xl-11">
                                 <asp:label runat="server" Text="Usuário" ID="lblmatricula"></asp:label>
                                 
                                
                            <br />
                                <asp:TextBox runat="server" ID="txtuser" CssClass="form-control"  Style="text-align: center;"> </asp:TextBox >
                            </div>
                            <div class="col-lg-11 col-md-11 col-sm-11 col-xl-11">
                               
                                   <asp:label runat="server"   Text="Senha" ID="lblsenha"></asp:label>
                                <br />
                                <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" CssClass="form-control" Style="text-align: center;"></asp:TextBox>
                            </div>

                        </div>
                             
                        <br />
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xl-12 text-center">
                                <asp:LinkButton runat="server" CssClass="btn btn-primary" Text="Entrar"  ID="btnentrar">  </asp:LinkButton>
                                <br />
                                <br />
                                
                             
                            </div>




                        </div>
                            </asp:Panel> 
                    </div>
                    
                      

                    <%--</div>--%>
                </div>
            </div>

        </div>
    </form>
</body>
</html>

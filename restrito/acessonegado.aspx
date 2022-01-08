<%@ Page Title="" Language="VB" MasterPageFile="~/restrito/MasterPage.master" AutoEventWireup="false" CodeFile="acessonegado.aspx.vb" Inherits="restrito_acessonegado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <div class="box box-primary" style="margin-left: 15px; margin-right: 15px;">

                    <div class="box-header" style="background: linear-gradient(to right, #D1E7F5, #184E70); color: white;">
                        <div class="row">
                   
                            <div class="col-lg-12 ">
                                <center>
                                <div class="row">
                                    <div class="col-lg-12 ">
                                        <center>
                              <asp:Label ID="lbltitlocalizarpropostas" runat="server" Text="Atenção !"  Font-Size="XX-Large"></asp:Label>
                          </center>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12 ">
                                        <center>
                               <%-- <asp:Label ID="Label2" runat="server" Text="Por gentileza, informe seus dados para criar seu cadastro !"></asp:Label></center>--%>
                                    </div>
                                </div>
                                    </center>
                            </div>
                        </div>
                        <center>
                        
                            
                                        
                                               
                    <br />
                 </center>

                    </div>
                    <div class="box-body">
              
                        <div class="row">
                            
                            <div class="col-lg-8">
                                <div class="form-group">


                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />


                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <center>
                                                <asp:Label ID="Label4" runat="server" Text="Desculpe, " Font-Size="XX-Large" forecolor="DarkBlue" ></asp:Label> <asp:Label ID="lblnome" runat="server" Text="Nome" Font-Size="XX-Large" forecolor="darkred"></asp:Label> <asp:Label ID="Label1" runat="server" Text=" !" Font-Size="XX-Large" forecolor="DarkBlue" ></asp:Label> 
                                              </center>
                                            </div>
                                        </div>
                                        
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <asp:Label ID="Label5" runat="server"  Font-Size="X-Large" forecolor="DarkBlue" Text="Infelizmente seu acesso não é permitido nessa opção! Verifique com o administrador e tente novamente !"></asp:Label>
                                               
                                            </div>
                                        </div>
                                    
                                    </div>
                                  
                                     <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                              <%--  <asp:Label ID="Label3" runat="server"  Font-Size="X-Large" forecolor="DarkBlue" Text="Você receberá um e-mail com sua aprovação."></asp:Label>--%>
                                               
                                            </div>
                                        </div>
                                    
                                    </div>

                                      <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <center>
                                                    <asp:Image runat="server" ImageUrl="~/img/simbolo.jpg" style="width:4rem;height:3.5rem;" />
                                             <%--   <asp:Label ID="Label6" runat="server"  Font-Size="X-Large" forecolor="DarkBlue" Text="Equipe MM Cobranças"></asp:Label>--%>
                                               </center>
                                            </div>
                                        </div>
                                    
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/img/mulherpasta.png" Height="70%" Width="70%" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
</asp:Content>


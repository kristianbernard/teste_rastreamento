<%@ Page Title="" Language="VB" MasterPageFile="~/restrito/masterpage.master" AutoEventWireup="false" CodeFile="usuarios.aspx.vb" Inherits="restrito_usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
       <script language="javascript" type="text/javascript">

       

      

        function irtopo() {

            $("html, body").animate({
                scrollTop: 0
            }, 1200);
           

        }

        function fecharmodalstatus() {
            //$('.fechar').click(function (ev) {

            $(".windowsdth").hide();
            $(".windowsdth").closest;

            //}

        }
        function exibirnovostatus() {

            $("html, body").animate({
                scrollTop: 0
            }, 1200);
            $('#baixarmensalidades').show();
            //    $('#alterarststatus').load();
        }


        function fecharmodaldocs() {
            //$('.fechar').click(function (ev) {

            $(".windowsdocs").hide();
            $(".windowsdocs").closest;

            //}

        }
        function exibirdocs() {
            $("html, body").animate({
                scrollTop: 0
            }, 1200);

            $('#verdocumentos').show();
            //    $('#alterarststatus').load();
        }
        function exibiralerta1() {




            $('#alerta_conta').show();
            $('#alerta_conta').load();
        }
        function escondeexibiralerta1() {




            $('#alerta_conta').hide();
            $('#alerta_conta').hide();
        }
        function exibiralerta2() {
            $("html, body").animate({
                scrollTop: 0
            }, 1200);



            $('#alerta1').show();
            $('#alerta1').load();
        }
        function escondeexibiralerta2() {




            $('#alerta1').hide();
            $('#alerta1').hide();
        }
    </script>
   
    <div class="panel panel-primary" style="margin-right:1rem; margin-left:1rem;">
        <div class="panel-heading">
            <center>Usuários</center>
        </div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div id="alerta1" style="display: none;">


                        <div class="alert alert-danger alert-dismissable">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                            <strong>Atenção: Verifique os itens abaixo !</strong>
                            <asp:GridView ID="gradeerro" BackColor="Transparent" ForeColor="white" runat="server" AutoGenerateColumns="False" BorderColor="White" BorderStyle="none" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None">
                                <Columns>


                                    <asp:BoundField DataField="erro">
                                        <ItemStyle BackColor="Transparent" ForeColor="White" />
                                    </asp:BoundField>


                                </Columns>


                            </asp:GridView>
                        </div>
                    </div>
                     <asp:HiddenField ID="hdnrseqperfil" runat="server" />
                     <asp:HiddenField ID="hdnrseqpermissao" runat="server" />
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <center>
                                   <asp:Button ID="btnNovo" runat="server" Text="     Incluir Novo Usuário      " CssClass="btn btn-primary" Visible ="true" Enabled ="true"/>
                                     </center>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-2">
                            <div class="form-group">

                                <asp:Label ID="Label2" runat="server" Text="Usuário"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtnome" runat="server" class="form-control"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-lg-3">

                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" Text="Email"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtemail" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">

                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" Text="Senha"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtsenha" TextMode="Password" runat="server" class="form-control"></asp:TextBox>

                            </div>

                        </div>
                        <div class="col-lg-2">

                            <div class="form-group">
                                 Permissão
                                <br />
                                <asp:DropDownList ID="cbopermissao" runat="server" class="form-control" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>

                        </div>
                        <div class="col-lg-2">

                            <div class="form-group">
                             Perfil 
                                <br />
                                <asp:DropDownList ID="cboperfil" runat="server" class="form-control" AutoPostBack="true">
                                </asp:DropDownList>

                            </div>

                        </div>
                        <div class="col-lg-2">

                            <div class="form-group">
                             Tipo Apontamento 
                                <br />
                                <asp:DropDownList ID="cboapontamento" runat="server" class="form-control">
                                </asp:DropDownList>

                            </div>

                        </div> 


                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <center>

                                  <asp:Button ID="btnsalvar" runat="server" Text="Salvar" enabled="false" CssClass="btn btn-warning"/>
                                     </center>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <asp:GridView ID="Grade" runat="server" AutoGenerateColumns="False" CssClass="table" GridLines="Horizontal" ShowHeaderWhenEmpty="True" EmptyDataText="Nenhum grupo localizado !"  Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sel" HeaderStyle-CssClass="text-center">
                                       
                                            <ItemTemplate>
                                                <asp:CheckBox ID="sel2" runat="server" />
                                                <asp:HiddenField ID="hdsuspenso" runat="server" value='<%# Bind("suspenso") %>'/>
                                                <asp:HiddenField ID="hdativo" runat="server" value='<%# Bind("ativo") %>'/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         
                                        <asp:TemplateField HeaderText="Usuário" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblusuariograde" runat="server" Text='<%#Eval("usuario") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Permissão" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnrseqpermissaorpt" runat="server" value='<%# Bind("nrseqpermissao") %>'/>
                                                <asp:Label ID="lblpermissaograde" runat="server"></asp:Label>
                                                <%--<asp:Label ID="lblpermissaograde" runat="server" Text='<%#Eval("permissao") %>'></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblemailgrade" runat="server" Text='<%#Eval("email") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Alterar" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnalterargrade" runat="server" CommandName="Alterar"><img src="../img/alterarverde.png" style="width: 23px;"/></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:ButtonField ButtonType="Image"  CommandName="Alterar" HeaderText="  Alterar  " ImageUrl ="~/img/alterarverde.png">
                                            <ControlStyle Width="27px" Height="27px" ></ControlStyle>
                                        </asp:ButtonField>--%>
                                        
                                         <asp:TemplateField HeaderText="Remover" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnexcluirgrade" runat="server" CommandName="Excluir"><img src="../img/imgnao.png" style="width: 23px;"/></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                       <%-- <asp:ButtonField ButtonType="Image" CommandName="Excluir" HeaderText="  Remover   " ImageUrl="~/img/imgnao.png" >
                                          <ControlStyle Width="27px" Height="27px" ></ControlStyle>
                                        </asp:ButtonField>--%>

                                        <asp:TemplateField HeaderText="Suspender" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnsuspendergrade" runat="server" CommandName="Suspender"><img src="../img/error.png" style="width: 23px;"/></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                                                                <%--<asp:ButtonField ButtonType="Image" CommandName="Suspender" HeaderText ="  Suspender  " ImageUrl="~/img/error.png">
                                              <ControlStyle Width="27px" Height="27px" ></ControlStyle>
                                        </asp:ButtonField>--%>
                                    </Columns>
                                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>


<%@ Page Title="" Language="VB" MasterPageFile="~/restrito/MasterPage.master" AutoEventWireup="false" CodeFile="permissoes.aspx.vb" Inherits="restrito_permissoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        function ir_topo() {

            $("html, body").animate({
                scrollTop: 0
            }, 1200);
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
            var alturaTela = $(document).height() * 2;
            var larguraTela = $(window).width();

            // colocando o fundo preto


            //      alert('Registro gravado com sucesso2!');

            var left = ($(window).width() / 2) - ($('#alterarststatus').width() / 4);
            var top = 100;


            $('#informarcarteira').show();
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




            $('#alerta1').show();
            $('#alerta1').load();
        }
        function escondeexibiralerta2() {




            $('#alerta1').hide();
            $('#alerta1').hide();
        }
    </script>
    <style>
        .frame {
            height: 90%;
            width: 100%;
        }

        .windowsdth {
            border-width: 2px;
            border-style: solid;
            border-color: #800000;
            display: none;
            width: 400px;
            height: 250px;
            position: absolute;
            left: 45px;
            top: 65px;
            background: white;
            z-index: 9900;
            padding: 5px;
            text-align: left;
            /*margin-top: 430px;*/
            -webkit-box-shadow: 9px 7px 5px rgba(50, 50, 50, 0.77);
            -moz-box-shadow: 9px 7px 5px rgba(50, 50, 50, 0.77);
            box-shadow: 9px 7px 5px rgba(50, 50, 50, 0.77);
        }
    </style>

    <div class="box box-primary" style="-webkit-box-shadow: 9px 7px 5px rgba(50, 50, 50, 0.77); -moz-box-shadow: 9px 7px 5px rgba(50, 50, 50, 0.77); box-shadow: 9px 7px 5px rgba(50, 50, 50, 0.77);">

        <div class="box box-header ">
            <b>Permissões de Acesso</b>
        </div>
        <div class="box-body">

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="alerta1" style="display: none;">

                        <div class="alert alert-danger alert-dismissable">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                            <strong>Atenção:</strong>
                            <asp:Label ID="lblerroconta1" runat="server" Text="Por favor, selecione uma carteira de clientes válida !"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <center>
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <asp:Button ID="btnNova" runat="server" Text="   Incluir Nova Permissão    " CssClass="btn btn-primary" Enabled="true" />
                                </div>
                            </div>
                        </center>
                    </div>
                    <div class="row">
                        <div class="col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Text="Código"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtcodigo" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-6">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" Text="Descrição"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtdescricao" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <%--<div class="col-lg-3">
                                Grupo de acesso
                                <br />
                                <asp:DropDownList ID="cbogrupo" runat="server" CssClass="form-control "></asp:DropDownList>
                                </div> --%>
                    </div>



                    <div class="row">

                        <div class="col-lg-5">
                            <div class="box box-danger ">
                                <div class="box box-header "><b>Formulários sem acesso</b></div>
                                <div class="box-body ">

                                    <asp:CheckBox ID="chkseltodos01" runat="server" Text="Selecionar Todos" AutoPostBack="true" />
                                    <br />
                                    <div style="overflow-y: scroll; background-color: transparent; overflow-x: hidden; height: 400px;">
                                        <asp:GridView ID="GradeForms" CssClass="table" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="Nenhuma página localizada" Style="text-align: center; margin-bottom: 0px;" Width="100%" GridLines="none">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sel">
                                                    <HeaderTemplate>
                                                        <div class="row text-center">
                                                            <asp:CheckBox ID="selcabecalho" runat="server" AutoPostBack="true" OnCheckedChanged="seltodos0" Visible="false" />
                                                        </div>

                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="sel" runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nome" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblnomegrade" runat="server" Text='<%#Eval("pagina") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%-- <asp:BoundField DataField="pagina" HeaderText="Nome" />--%>
                                            </Columns>

                                            <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                            <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                            <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                            <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 text-center">
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnadd" runat="server" Text="Adicionar" CssClass="btn btn-success" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                    <ProgressTemplate>
                                        <img src="../img/carregando1.gif" width="50px" height="50px" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <br />
                                <br />
                                <br />
                                <br />
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnremove" runat="server" Text="Remover" CssClass="btn btn-warning " />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                    <ProgressTemplate>
                                        <img src="../img/carregando1.gif" width="50px" height="50px" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                        <div class="col-lg-5">
                            <div class="box box-success ">
                                <div class="box box-header "><b>Formulários com acesso liberado</b></div>
                                <div class="panel-body ">

                                    <asp:CheckBox ID="chkseltodos02" runat="server" Text="Selecionar Todos" AutoPostBack="true" />
                                    <br />
                                    <div style="overflow-y: scroll; background-color: transparent; overflow-x: hidden; height: 400px;">
                                        <asp:GridView ID="GradeFormsSel" CssClass="table" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="Nenhuma página localizada" Style="text-align: center; margin-bottom: 0px;" Width="100%" GridLines="none">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sel">
                                                    <HeaderTemplate>
                                                        <div class="row text-center">
                                                            <asp:CheckBox ID="selcabecalho" runat="server" AutoPostBack="true" Visible="false" />
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="sel" runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nome" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblnomegrade" runat="server" Text='<%#Eval("pagina") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="pagina" HeaderText="Nome" />--%>
                                            </Columns>

                                            <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                            <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                            <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                            <SortedDescendingHeaderStyle BackColor="#7E0000" />

                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <center>

                            <div class="col-lg-12">
                                <div class="form-group">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSalvar" runat="server" Text="   Salvar     " CssClass="btn btn-danger " Enabled="false" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel4">
                                        <ProgressTemplate>
                                            <img src="/img/carregando1.gif" width="50px" height="50px" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                        <ProgressTemplate>
                                            <img src="/img/carregando1.gif" width="24px" height="24px" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>


                                </div>
                            </div>
                        </center>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <asp:GridView ID="grade" runat="server" AutoGenerateColumns="False" CssClass="table" GridLines="none" ShowHeaderWhenEmpty="True" EmptyDataText="Nenhum lançamento localizado !" Style="text-align: center; margin-bottom: 0px;" Width="100%" PageSize="6">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sel" HeaderStyle-CssClass="text-center">

                                            <ItemTemplate>
                                                <asp:CheckBox ID="sel2" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nr Interno" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnrseq" runat="server" Text='<%#Eval("nrseq") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Data de Cadastro" HeaderStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldtcad" runat="server" Text='<%#Eval("dtcad") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Permissão" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldescricao" runat="server" Text='<%#Eval("descricao") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>

                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdativo" runat="server" Value='<%# Bind("ativo")%>'></asp:HiddenField>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Alterar" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnalterar" runat="server" CommandName="alterar" CommandArgument='<%#Container.DataItemIndex%>'>
                                                        <img src="..\img\alterarverde.png" style="Width:27px"/>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Excluir" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnexcluir" runat="server" CommandName="excluir" CommandArgument='<%#Container.DataItemIndex%>'>
                                                        <img src="..\img\imgnao.png" style="Width:27px"/>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:ButtonField ButtonType="Image" runat="server" HeaderText="Abrir" CommandName="ver" ImageUrl="~\img\placaolho.png" Visible="false">
                                            <ControlStyle Width="27px" Height="27px"></ControlStyle>

                                        </asp:ButtonField>
                                        <%--  <asp:ButtonField ButtonType="Button" CommandName="Alterar" Text="  Alterar  ">
                                            <ControlStyle CssClass="botaoredondo" />
                                            <ItemStyle CssClass="botaored" />
                                        </asp:ButtonField>--%>
                                        <%--  <asp:ButtonField ButtonType="Button" CommandName="Excluir" Text="  Remover  ">
                                            <ControlStyle CssClass="botaoredondo" />
                                            <ItemStyle CssClass="botaored" />
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
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <asp:CheckBox ID="chkExibirExcluidos" Text="Exibir Excluídos" runat="server" AutoPostBack="true" />

                            </div>
                        </div>

                        <div class="col-lg-2">
                            <div class="form-group">

                                <asp:Button ID="btnRemover" runat="server" Text="Excluir Selecionados" CssClass="btn btn-primary" Enabled="true" Visible="false" />

                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">

                                <asp:Button ID="btnBaixar" runat="server" Text="Baixar Selecionados" CssClass="btn btn-primary" Enabled="true" Visible="false" />

                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">

                                <asp:Button ID="btnProcurar" runat="server" Text="Localizar" CssClass="btn btn-primary" Enabled="true" Visible="false" />

                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">

                                <asp:Button ID="btnanexar" runat="server" Text="Anexar Documentos" CssClass="btn btn-primary" Enabled="true" Visible="false" />

                            </div>
                        </div>
                        </center>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>


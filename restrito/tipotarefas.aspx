<%@ Page Title="" Language="VB" MasterPageFile="~/restrito/MasterPage.master" AutoEventWireup="false" CodeFile="tipotarefas.aspx.vb" Inherits="tipotarefas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="panel panel-primary" style="margin-right:1rem; margin-left:1rem;">
        <div class="panel-heading">
            <center>Cadastro Tipo de Tarefas</center>
        </div>
        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                     
                     <asp:HiddenField ID="hdnrseq" runat="server" /> 
                     <asp:HiddenField ID="hdnrseqtarefa" runat="server" /> 
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <center>
                                   <asp:Button ID="btnNovo" runat="server" Text=" Novo" CssClass="btn btn-primary" Visible ="true" Enabled ="true"/>
                                     </center>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group">

                               Tipo de tarefa
                                <br />
                                <asp:TextBox ID="txtdescricao" runat="server" class="form-control"></asp:TextBox>

                            </div>
                        </div> 
                        <div class="col-lg-4">
                            <div class="form-group">

                               Tarefas
                                <br />
                                <asp:DropDownList ID="cbotarefas" runat="server" class="form-control"></asp:DropDownList>
                                <asp:Repeater ID="rpttarefas" runat="server">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldescricao" runat="server" Text='<%#Eval("descricao") %>' CssClass="label label-success"></asp:Label>
                                        <asp:HiddenField ID="hdnrseqtarefarpt" runat="server" Value='<%#Bind("nrseq") %>' />
                                       <asp:LinkButton ID="btnexcluirpt" runat="server" CssClass="label label-danger"><i class="fa fa-times" aria-hidden="true"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div> 
                        <div class="col-lg-1 ">
                            <br />
                            <asp:LinkButton ID="btnadd" runat="server" CssClass="btn btn-success">Add</asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <center>

                                  <asp:LinkButton ID="btnsalvar" runat="server" Text="Salvar" enabled="false" CssClass="btn btn-warning"></asp:LinkButton>
                                     </center>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <asp:GridView ID="Grade" runat="server" AutoGenerateColumns="False" CssClass="table" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="Nenhum grupo localizado !"  Width="100%">
                                    <Columns> 
                                         
                                        <asp:TemplateField  HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnrseqrpt" runat="server" value='<%#Bind("nrseq") %>'/>
                                                <asp:Label ID="lblnrseqrpt" runat="server" CssClass="badge" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Descricao" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldecricaograde" runat="server" Text='<%#Eval("descricao") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tarefas" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Repeater ID="rpttarefasrpt" runat="server">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldescricao" runat="server" Text='<%#Eval("descricao") %>' CssClass="label label-success"></asp:Label>
                                        <asp:HiddenField ID="hdnrseqtarefarptgrade" runat="server" Value='<%#Bind("nrseq") %>' /> 
                                    </ItemTemplate>
                                </asp:Repeater>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ações" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="bnteditar" runat="server" CommandName="editar" ToolTip="Editar" CommandArgument="<%#Container.DataItemIndex %>"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnexcluir" runat="server" CommandName="excluir" ToolTip="Excluir" CommandArgument="<%#Container.DataItemIndex %>"><i class="fa fa-trash" aria-hidden="true"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         
                                    </Columns> 
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>


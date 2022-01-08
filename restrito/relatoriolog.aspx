<%@ Page Title="" Language="VB" MasterPageFile="~/restrito/MasterPage.master" AutoEventWireup="false" CodeFile="relatoriolog.aspx.vb" Inherits="restrito_relatoriolog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        #divrptdth:hover #divdescricao {
            display:block!important; 
        }
    </style>

    <div class="panel panel-primary" style="margin-right: 1rem; margin-left: 1rem;">
        <div class="panel-heading">
            <center>Relatório de Log</center>
        </div>
        <div class="panel-body">

            <div class="row">
                <div class="col-lg-12">
                    <asp:HiddenField ID="hdnrseqtarefa" runat="server" />
                    <div class="col-lg-12">
                        <div class="col-lg-5" style="border: solid 1px white; padding: 15px; border-radius: 15px; -webkit-box-shadow: 9px 7px 5px rgba(50, 50, 50, 0.77); -moz-box-shadow: 9px 7px 5px rgba(50, 50, 50, 0.77); box-shadow: 9px 7px 5px rgba(50, 50, 50, 0.77);">
                            <div class="col-lg-12">
                                <div class="col-lg-12 text-left ">
                                    <asp:Image ImageUrl="~/img/task.png" runat="server" Style="width: 50px;" />
                                    <asp:Label runat="server" Style="font-size: 25px; color: gray;"><b>Log Time</b></asp:Label>
                                </div>
                                <br />
                                <div class="col-lg-12 text-center">
                                    <div class="col-lg-12">
                                        Tarefa <span style="color: red;">*</span>
                                        <br />
                                        <asp:DropDownList ID="cbotarefas" runat="server" Style="width: 100%;" CssClass="js-example-responsive" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-12">
                                        Tipo <span style="color: red;">*</span>
                                        <br />
                                        <asp:DropDownList ID="cbotipo" CssClass="js-example-responsive" Style="border-radius: 10px; width: 100%;" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-7">
                                        Data <span style="color: red;">*</span>
                                        <br />
                                        <asp:TextBox ID="txtdata" type="date" Style="border-radius: 10px;" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-6">
                                        Início <span style="color: red;">*</span>
                                        <br />
                                        <asp:TextBox ID="txthrinicio" type="time" Style="border-radius: 10px;" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-6">
                                        Fim <span style="color: red;">*</span>
                                        <br />
                                        <asp:TextBox ID="txthrfim" type="time" Style="border-radius: 10px;" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div class="col-lg-12" id="divlblaviso" runat="server" style="display: none;">
                                        <small>
                                            <asp:Label ID="lblaviso" runat="server"></asp:Label></small>
                                    </div>
                                    <br />
                                    <div class="col-lg-12">
                                        Descrição
                                                        <br />
                                        <asp:TextBox ID="txtdescricao" TextMode="MultiLine" Style="resize: none; height: 70px; border-radius: 10px" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-12 text-right">
                                        <br />
                                        <asp:LinkButton ID="btncancelar" runat="server" CssClass="btn btn-warning">Cancelar</asp:LinkButton>
                                        <asp:LinkButton ID="btnsalvar" runat="server" CssClass="btn btn-primary">Log Time</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-7">
                            <div class="col-lg-12">
                                <div class="col-lg-1">
                                    <br />
                                    <asp:Image ID="imgusuario" runat="server" ImageUrl="~/img/usuario.png" Style="width: 50px" />
                                </div>
                                <div class="col-lg-3">
                                    <br />
                                    <asp:Label ID="lblusuario" runat="server" Style="font-size: 23px"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    Data inicio
                                    <br />
                                    <asp:TextBox ID="txtdtinicio" runat="server" Style="width: 100%" type="date" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-4">
                                    Data fim
                                    <br />
                                    <asp:TextBox ID="txtdtfim" runat="server" type="date" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-12">
                                    <asp:LinkButton ID="btnpesquisa" runat="server" CssClass="btn btn-primary">Pesquisar</asp:LinkButton>
                                </div>
                                <br />
                                <div class="row">
                                    <br />
                                    <div class="col-lg-12">
                                        <br />
                                        <asp:Repeater ID="rpttarefas" runat="server">
                                            <ItemTemplate>
                                                <div class="col-lg-12" style="border-radius: 13px; border: solid 1px red; padding: 10px;">
                                                    <div class="col-lg-4">
                                                        <i class="fa fa-calendar" aria-hidden="true"></i>
                                                        <asp:Label ID="lbldatarpt" Style="font-size: 20px" runat="server" Text='<%#Bind("data") %>'></asp:Label>
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <small>
                                                            <asp:Label ID="lblhriniciorpt" runat="server" Text='<%#Eval("hrinicio") %>'></asp:Label>
                                                            -
                                                            <asp:Label ID="lblhrfimrpt" runat="server" Text='<%#Eval("hrfim") %>'></asp:Label>
                                                        </small>
                                                    </div>
                                                    <div class="col-lg-5 text-right">
                                                        <asp:LinkButton ID="btnabrir" runat="server" CommandName="abrir" Style="color: cornflowerblue;"><i class="fa fa-chevron-down" aria-hidden="true"></i></asp:LinkButton>
                                                    </div>
                                                    <br />
                                                    <div class="row" id="divtasks" runat="server" style="display:none;">
                                                        <div class="col-lg-12">
                                                            <br />
                                                            <asp:Repeater ID="rptarefasdth" runat="server">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdnrseqtarefasrpt" runat="server" Value='<%#Eval("nrseq") %>' />
                                                                    <div class="col-lg-12" id="divrptdth" style="border: solid 1px gray;border-radius: 11px;padding: 5px;"">
                                                                        <div class="col-lg-1">
                                                                            <img src="../img/task-32.png" style="width: 30px" />
                                                                        </div>
                                                                        <div class="col-lg-5">
                                                                            <asp:Label ID="lbltarefarpt" runat="server" Text='<%#Eval("tarefa") %>'></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-3">
                                                                            <asp:Label ID="lblduracaorpt" runat="server" Text='<%#Eval("duracao") %>'></asp:Label>
                                                                        </div>
                                                                        <div class="col-lg-3 text-right">
                                                                            <asp:LinkButton ID="btnremove" CommandName="excluir" runat="server" Style="color: red;"><i class="fa fa-times" aria-hidden="true"></i></asp:LinkButton>
                                                                        </div>
                                                                        <div class="col-lg-12 text-left" id="divdescricao" style="display:none;">
                                                                            Descrição
                                                                            <br />
                                                                            &nbsp;&nbsp;<small> <asp:Label ID="lbldescricaorpt" runat="server" Text='<%#Eval("descricao") %>'></asp:Label></small>
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                    </div>

                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </div>
    </div>
    <script type="text/javascript">
        function formatacampo() {
            $(".js-example-responsive").select2({
                width: 'resolve' // need to override the changed default
            });
        }
        $(".js-example-responsive").select2({
            width: 'resolve' // need to override the changed default
        });
    </script>

</asp:Content>


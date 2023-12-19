<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Pagamento.master" AutoEventWireup="true"
    CodeFile="indexEvento.aspx.cs" Inherits="index_evento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function AbrirMapa(id, href) {
            console.log(href);
            $('html,body').scrollTop(0);
            $.fancybox.open({
                href: href + id,
                type: 'iframe',
                scrolling: 'no',
                width: 1600,


                'afterClose': function () {
                    window.location.reload();
                },
                backFocus: false,
                minHeight: 1200,
                preload: true,
                padding: 5,
                overlayShow: false,

                autoSize: true
            });
            return false;
        }

    </script>
    <style>
        .fancybox-iframe {
            width: 100%;
            height: 100%;
        }
    </style>
    <script type="text/javascript">
        function closeFancyboxAndRedirectToUrl(url) {
            $.fancybox.close();
            window.location = url;
        }
    </script>
    <script>
        function PopUp(string1, string2) {
            Swal.fire(
                string1,
                '',
                string2
            )
        }
        function PopUpNegativo(string1) {
            Swal.fire(
                string1,
                '',
                'error'
            )
        }
    </script>
    <style>
        .fancybox-wrap {
            -moz-box-shadow: 0 0 5px #999;
            -webkit-box-shadow: 0 0 5px #999;
            box-shadow: 0 0 5px #999;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <form action="indexEvento.aspx">
                <div style="align-self: center" class="modal fade mr-3" id="myModal" role="dialog">
                    <center>
                        <div style="margin-right: 1000px; align-self: center" class="modal-dialog">

                            <!-- Modal content-->
                            <div style="width: 1300px" class="modal-content">
                                <div style="width: 100%" class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title"></h4>
                                </div>
                                <div style="width: 100%" class="modal-body">
                                    <asp:Label Width="100%" runat="server" ID="lblDetalhes"></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-primary" data-dismiss="modal">Fechar</button>
                                </div>
                            </div>

                        </div>
                    </center>
                </div>
                <div class="container BannerEvento">
                    <img runat="server" class="img-responsive img" id="imgbanner" src="#" alt="First slide">
                    <button class="btn btn-outline-primary btn-more-info" data-toggle="modal" data-target="#myModal">MAIS INFORMAÇÕES</button>
                </div>

                <div class="container pt-5 mt-5">
                    <h1 class="text-dark text-bolder" id="tituloEvento" runat="server"></h1>
                    <h4 class="text-primary" id="localEvento" runat="server"></h4>
                    <aside class="evento mb-5">
                        <div class="event-title">
                        </div>
                        <div class="event-info col-md-12 pl-0 pr-0">
                            <div class="jumbotron d-md-flex mb-5">
                                <div class="col-md-8 col-sm-12">
                                    <h3>Informações gerais do evento</h3>
                                    <button style="visibility: hidden" type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal">Detalhes</button>
                                    <p>
                                        <asp:Label runat="server" Font-Size="Medium" ID="lblResumo"></asp:Label>
                                        <a name="top"></a>
                                    </p>
                                </div>

                            </div>
                            <div class="col-md-4 col-sm-12 pr-0 pl-0 ticket posiciona-absolute-100 mt-md-n1">

                                <div class="ticket-body border">
                                    <div class="ticket-resumo">
                                        <asp:LinkButton runat="server" ID="LinkButtonAbrirMapaInteira" OnClientClick="">
                                            <div class="text-white bg-primary" style=" border-radius:10px ">
                                            <h2 class="ticket-resumo-title text-white" style="height:10px" >INGRESSOS COM MESA </h2>
                                            <p class="ticket-resumo-title" style="font-size:10px;height:25px" >CLIQUE AQUI</p>
                                           </div>
                                        </asp:LinkButton>
                                        <div class="resumo-valores col-12 pl-0 pr-0 d-flex">
                                            <%--<asp:Label runat="server" CssClass="resumo-valores-item d-flex col-md-6 text-bolder mt-2"
                                                Text="Total: R$ "><span id="lblCarrinhoCadeira" runat="server">0,00</span></asp:Label>--%>
                                        </div>
                                        <div class="ticket-resumo-content">
                                            <%--<asp:Label runat="server" ID="lblIngressoMesa" Text="" class="resumo-item"></asp:Label>--%>

                                            <asp:GridView ID="grdMesa" CssClass="col-12" runat="server" AutoGenerateColumns="False"
                                                OnRowCommand="grdMesa_RowCommand" GridLines="None"
                                                DataKeyNames="Ticket">
                                                <RowStyle Font-Size="13px" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:BoundField Visible="false" DataField="Ticket" />
                                                    <asp:BoundField DataField="Tipo" />
                                                    <asp:BoundField Visible="false" DataField="Valor" />
                                                    <asp:ButtonField ButtonType="Image" ControlStyle-CssClass="ml-10" CommandName="Excluir" ImageUrl="~/images/error.png" />
                                                </Columns>
                                                <HeaderStyle CssClass="grid_header" HorizontalAlign="Left" />
                                                <AlternatingRowStyle BackColor="#dcecf4" CssClass="border-0" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="ticket-resumo">
                                        <div class="text-white  bg-warning-opacity" style="border-radius: 10px">
                                            <h2 runat="server" id="ingressoAvulso" class="ticket-resumo-title bg-warning-opacity text-white" style="height: 10px">INGRESSOS SEM MESA</h2>
                                            <p class="ticket-resumo-title" style="font-size: 10px; height: 25px">SELECIONE ABAIXO</p>
                                        </div>

                                        <select id="dropTipoAvulso" runat="server" class="form-control mb-2 mt-2 col-md-12">
                                        </select>

                                        <div class="resumo-valores col-12 pl-0 pr-0 d-inline-flex">
                                            <%--<asp:Label runat="server" CssClass="resumo-valores-item d-flex col-md-6 text-bolder mt-2"
                                                Text="Total: R$ "><span id="lblCarrinhoTotal" runat="server">0,00</span></asp:Label>--%>

                                            <div class="ticket-quantity row col-12 pr-0">
                                                <div class="d-inline-block col-9 justify-content-start mb-1">
                                                    <div class="ticket-quantity-input d-inline-flex pr-3 mb-3">
                                                        <asp:Button runat="server" ID="BtnMin" Text="-" OnClick="BtnMin_Click" Font-Size="Medium" Font-Bold="true" class=" AlignBtn btn btn-outline-primary btn-sm plus  mr-1 decrease-btn"></asp:Button>
                                                        <input id="input" runat="server" readonly style="text-align: center" type="number" class="form-control form-sm quantity" name="quantity" value="0">
                                                        &nbsp
                                                        <asp:Button runat="server" Text="+" OnClick="BtnPlus_Click" Font-Size="Medium" ID="BtnPlus" Font-Bold="true" class="btn btn-outline-primary btn-sm minus increase-btn"></asp:Button>
                                                        <asp:LinkButton runat="server" ID="adicionarAvulso" Text="Incluir" OnClick="btnAdicionarAvulso_Click" class="btn btn-sm btn-primary btn-incluir justify-content-start float-left"></asp:LinkButton>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="ticket-resumo-content">
                                            <%--<asp:Label runat="server" ID="lblIngressoSemMesa" class="resumo-item" />--%>

                                            <asp:GridView ID="grdSemMesa" CssClass="col-12" runat="server" AutoGenerateColumns="False"
                                                OnRowCommand="grdSemMesa_RowCommand" GridLines="None"
                                                DataKeyNames="Ticket">
                                                <RowStyle Font-Size="13px" Font-Names="Verdana" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:BoundField Visible="false" DataField="Ticket" />
                                                    <asp:BoundField DataField="Tipo" />
                                                    <asp:BoundField Visible="false" DataField="Valor" />
                                                    <asp:ButtonField ButtonType="Image" ControlStyle-CssClass="ml-4" CommandName="Excluir" ImageUrl="~/images/error.png" />
                                                </Columns>
                                                <HeaderStyle CssClass="grid_header" HorizontalAlign="Left" />
                                                <AlternatingRowStyle BackColor="#dcecf4" CssClass="border-0" />
                                            </asp:GridView>

                                        </div>
                                    </div>
                                    <a name="bottom"></a>
                                    <div class="ticket-header col-md-12 d-flex">
                                        <div class="ticked-title col-md-6 pr-0 pl-0">TOTAL INGRESSOS</div>
                                        <div class="ticked-price col-md-6 pr-0 pl-0 text-right">
                                            <i class="fa fa-cart-shopping"></i>
                                            <asp:Label runat="server" ID="lblValor" Text="R$ 0,00"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="d-inline-flex">
                                    <asp:LinkButton ID="BtnLimpar" OnClick="btnLimparCarrinho_Click" runat="server">
                                            <div class="btn btn-sm btn-danger text-white btn-block ml-2 mr-3">
                                               LIMPAR CARRINHO
                                            </div>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="BtnPAG" OnClick="btnEfetuarPagamento_Click" runat="server">
                                            <div class="btn btn-sm btn-warning text-white btn-block ml-5">
                                                IR PARA PAGAMENTO
                                            </div>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />

                        <br />
                        <br />
                        <br />
                        <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

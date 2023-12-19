<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Pagamento.master" AutoEventWireup="true"
    CodeFile="checkout.aspx.cs" Inherits="pagamento_checkout" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <form action="checkout.aspx">
                <div class="container bg-primary mb-5 p-3 text-center">
                    <h1 class="text-white">RESUMO DA COMPRA / PAGAMENTO</h1>
                </div>
                <div class="container">
                    <aside class="evento pb-5">
                        <div class="col-md-12 row mb-5">
                            <h1>Informações de pagamento</h1>
                        </div>
                        <div class="event-info d-md-flex">
                            <div class="col-md-8 col-sm-12 pl-0">
                                <div class="jumbotron jumbo-checkout">
                                    <span class="badge badge-secondary event-iniciais float-left mt-1 mr-3" runat="server" id="lblCustomName"></span>
                                    <span class="d-block text-bolder mb-0 pb-0" id="lblUsuario" runat="server"></span>
                                    <span class="mt-0 pt-0" runat="server" id="lblEmail"></span>
                                </div>
                                <p>
                                    Os ingressos serão enviados por e-mail a partir da data prevista nas <a href="#"
                                        class="text-primary" data-toggle="modal" data-target="#exampleModal">regras do
                            evento.</a>.<br>
                                    Basta selecionar os ingressos e enviá-los para o e-mail cadastrado
                                </p>
                                <div class="col-md-12 d-md-flex mb-4">
                                    <label class="card card-default p-1 m-1 col-md-6 col-sm-6">
                                        <span class="text-center">
                                            <b>CARTÃO DE CRÉDITO/DÉBITO</b><br>
                                            <span class="text-success">em até 5x no cartão de crédito</span>
                                        </span>
                                        <img src="https://static.wixstatic.com/media/902b9c_b15c372bbbe54b7c95bd2a41bf969eca~mv2.png/v1/fill/w_560,h_238,al_c,lg_1,q_85,enc_auto/bandeira-dos-cartoes-de-credito.png"
                                            alt="Metodos de pagamento">
                                        <input id="metodoCartao" type="radio" class="form-radio" name="metodo_pagamento">
                                        <asp:TextBox ID="checkpagamento" runat="server" type="text" class="form-radio d-none" ClientIDMode="Static" value="-1" />
                                    </label>
                                    <label runat="server" id="CotaDebito" class="card card-default p-1 m-1 col-md-6 col-sm-6">
                                        <span class="text-center"><b>DÉBITO EM COTA</b><br>
                                        </span>
                                        <div class="text-center text-secondary mt-5">
                                            <img height="70" src="data:image/png;base64,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" alt="">
                                        </div>
                                        <input id="metodoCota" type="radio" class="form-radio mt-5" name="metodo_pagamento">
                                    </label>

                                </div>
                                <div id="cardPayment" class="form-payment">
                                    <div class="col-12 mr-0 ml-0">
                                        <div class="col-md-6 mb-3 pl-0">
                                            <select name="parcelas" runat="server" id="dropParcelas" class="form-control">
                                                <option value="-1">Selecione a quantidade de parcelas</option>
                                            </select>
                                        </div>

                                        <legend>Dados do Titular do cartão</legend>
                                        <div class="form-group form-row">
                                            <div class="col-md-8">
                                                <label for="">Nome Impresso no Cartão <span class="text-danger">*</span></label><input type="text" id="txtTitular" runat="server" class="form-control">
                                            </div>
                                            <div class="col-md-4">
                                                <label for="">Número do cartão <span class="text-danger">*</span></label><input type="text" runat="server" id="txtNumeroCartao" class="form-control formCard">
                                            </div>
                                        </div>
                                        <%--<div class="form-group form-row">
                                            <div class="col-md-4">
                                                <label for="">Data de nascimento <span class="text-danger">*</span></label><input type="text" runat="server" id="txtDataNascimento" class="form-control formDate">
                                            </div>
                                            <div class="col-md-4">
                                                <label for="">CPF <span class="text-danger">*</span></label><input type="text" runat="server" id="txtCPF" class="form-control formCpf">
                                            </div>
                                            <div class="col-md-4">
                                                <label for="">CEP <span class="text-danger">*</span></label><input type="text" runat="server" id="txtCEP" class="form-control formCep">
                                            </div>
                                        </div>--%>
                                    </div>

                                    <div class="form-group form-row ml-2">
                                        <div class="col-md-3">
                                            <label for="">Bandeira <span class="text-danger">*</span></label>
                                            <select type="text" runat="server" id="dropBandeira" class="form-control">
                                                <option value="-1">Selecionar</option>
                                                <option value="1">Visa</option>
                                                <option value="2">Mastercard</option>
                                                <option value="3">Elo</option>
                                                <option value="4">Diners</option>
                                            </select>
                                        </div>
                                        <div class="col-md-3">
                                            <label for="">Data Validade <span class="text-danger">*</span></label><input type="text" runat="server" id="txtValidade" class="form-control formDateValidate">
                                        </div>
                                        <div class="col-md-2">
                                            <label for="">CVV <span class="text-danger">*</span></label><input type="text" maxlength="3" runat="server" id="txtCodigoSeguranca" class="form-control formCVV">
                                        </div>
                                    </div>
                                </div>

                                <div id="cotaPayment" class="form-payment">
                                    <div class="col-12 mr-0 ml-0">
                                        <div class="col-md-6 mb-3 pl-0">
                                            <select name="parcelas" runat="server" id="dropParcelasCota" class="form-control">
                                                <option value="-1">Selecione a quantidade de parcelas</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group form-row">
                                    <div class="col-md-12 d-flex justify-content-center">
                                        <span style="font-size: smaller">Ao prosseguir, você concorda com os</span><a href="#" class="text-primary" style="font-size: smaller" data-toggle="modal" data-target="#exampleModal">&nbsp termos e políticas&nbsp</a> <span style="font-size: smaller">do evento e do PIC</span>.
                                         <asp:Button runat="server" Font-Size="Smaller" OnClick="btnPagar_Click" class="btn btn-md btn-success ml-2 mr-2 col-2" Text="FINALIZAR" />
                                        <asp:LinkButton ID="BtnAlterarCompra" OnClick="btnAterarCompra_Click" runat="server">
                                        <div class="btn btn-sm btn-danger mr-2 col-s">ALTERAR COMPRA</div>
                                        </asp:LinkButton>
                                    </div>

                                </div>

                                <div class="form-group form-row">
                                    <div class="col-md-12 d-flex justify-content-center">
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-4 col-sm-12 pr-0 pl-0 ticket posiciona-absolute-50">
                                <div class="ticket-header ml-3 col-12 d-inline-flex">
                                    <div class="ticked-title col-md-6 pr-0 pl-0" runat="server" id="lblProduto">INGRESSOS</div>
                                    <div class="ticked-price col-md-6 pr-0 pl-0 text-right">
                                        <i class="fa fa-cart-shopping"></i>
                                        <span runat="server" id="lblValor"></span>
                                    </div>
                                </div>
                                <div class="ticket-body border">
                                    <div class="ticket-resumo">
                                        <h2 class="ticket-resumo-title bg-primary text-white">INGRESSOS COM MESA</h2>
                                        <div class="ticket-resumo-content">
                                            <div class="resumo-item" style="font-size: small">
                                                <%--<asp:Label runat="server" ID="divResumoIngressoMesa"></asp:Label>--%>
                                                <asp:GridView ID="grdMesa" CssClass="col-12" runat="server" AutoGenerateColumns="False"
                                                    GridLines="None"
                                                    DataKeyNames="Ticket">
                                                    <RowStyle Font-Size="Small" HorizontalAlign="Left" />
                                                    <Columns>
                                                        <asp:BoundField Visible="false" DataField="Ticket" />
                                                        <asp:BoundField DataField="Tipo" />
                                                        <asp:BoundField DataField="Valor" />
                                                        <asp:ButtonField ButtonType="Image" Visible="false" CommandName="Excluir" ImageUrl="~/images/error.png" />
                                                    </Columns>
                                                    <HeaderStyle CssClass="grid_header" HorizontalAlign="Left" />
                                                    <AlternatingRowStyle BackColor="#dcecf4" CssClass="border-0" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="ticket-resumo">
                                        <h2 class="ticket-resumo-title bg-warning-opacity text-white">INGRESSOS SEM MESA</h2>
                                        <div class="ticket-resumo-content">
                                            <%-- <asp:Label runat="server" ID="divResumoIngressoSemMesa"></asp:Label>--%>
                                            <asp:GridView ID="grdSemMesa" CssClass="col-12" runat="server" AutoGenerateColumns="False"
                                                GridLines="None"
                                                DataKeyNames="Ticket">
                                                <RowStyle Font-Size="Small" HorizontalAlign="Left" />
                                                <Columns>
                                                    <asp:BoundField Visible="false" DataField="Ticket" />
                                                    <asp:BoundField DataField="Tipo" />
                                                    <asp:BoundField DataField="Valor" />
                                                    <asp:ButtonField ButtonType="Image" Visible="false" CommandName="Excluir" ImageUrl="~/images/error.png" />
                                                </Columns>
                                                <HeaderStyle CssClass="grid_header" HorizontalAlign="Left" />
                                                <AlternatingRowStyle BackColor="#dcecf4" CssClass="border-0" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <asp:LinkButton ID="BtnAlterar" OnClick="btnAterarCompra_Click" runat="server">
                                            <div class="btn btn-sm btn-danger text-white btn-block ml-0 mt-4">
                                                ALTERAR COMPRA
                                            </div>
                                    </asp:LinkButton>
                                </div>
                                <div class="mt-5 mb-5 card border">
                                    <div class="card-body">
                                        <h6 class="text-bolder">Cartão de Crédito</h6>
                                        <span class="text-muted">*Compra segura. Os dados sensíveis não serão armazenados em nossos servidores.
                                        </span>
                                    </div>
                                </div>
                                <div class="mt-5 mb-5 card border border-danger">
                                    <div class="card-body">
                                        <span class="text-danger">Para auditorias e futuras verificações, estamos salvando seu IP na nossa base de dados.
                                        </span>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </aside>
                    <br />
                    <br />
                    <br />
                    <br />
            </form>

            <script type="text/javascript">
                $(document).ready(function () {

                    $("#cotaPayment").hide();
                    $("#cardPayment").hide();
                    document.getElementById("checkpagamento").value = "-1";

                    /* $('.formDate').mask('99/99/9999');*/
                    $('.formDateValidate').mask('99/9999');
                    $('.formCVV').mask('999');
                    //$('.formCep').mask('99999-999');
                    //$('.formCpf').mask('999.999.999-99', { reverse: true });
                    $('.formCard').mask('9999 9999 9999 9999', { reverse: true });

                    $("#metodoCartao").click(function () {
                        $("#cardPayment").show();
                        $("#cotaPayment").hide();
                        document.getElementById("checkpagamento").value = "1";
                    });

                    $("#metodoCota").click(function () {
                        $("#cardPayment").hide();
                        $("#cotaPayment").show();
                        document.getElementById("checkpagamento").value = "2";
                    });
                });
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

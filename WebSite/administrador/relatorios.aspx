<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master" AutoEventWireup="true"
    CodeFile="relatorios.aspx.cs" Inherits="administrador_relatorios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--ABERTURA DA DIV BOX HOME-->
    <div class="box_home" style="margin-left: 1%;">
        <h1>
            Relatórios</h1>
        <div class="texto_box_home_site">
            <div class="texto_box_home_candidato">
                <asp:HyperLink ID="hlkCadastro" runat="server" NavigateUrl="~/relatorios/vendasSintetico.aspx"
                    CssClass="link_relatorio">01 - Vendas Sintético</asp:HyperLink>

                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/relatorios/vendasDetalhado.aspx"
                    CssClass="link_relatorio">02 - Vendas Detalhado</asp:HyperLink>

                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/relatorios/resumoIngressos.aspx"
                    CssClass="link_relatorio">03 - Resumo por Tipo de Ingressos</asp:HyperLink>

                <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/relatorios/resumoPagamento.aspx"
                    CssClass="link_relatorio">04 - Resumo por Forma de Pagamento</asp:HyperLink>

                <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/relatorios/compradosSaldo.aspx"
                    CssClass="link_relatorio">05 - Compras e Saldo</asp:HyperLink>

                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/relatorios/clientesCadastrados.aspx"
                    CssClass="link_relatorio">06 - Clientes Cadastrados</asp:HyperLink>

                <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/relatorios/logAcessos.aspx"
                    CssClass="link_relatorio">07 - Log de Acessos</asp:HyperLink>

                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/relatorios/socios.aspx"
                    CssClass="link_relatorio">08 - Base de Sócios</asp:HyperLink>

                <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/relatorios/voucherDetalhado.aspx"
                    CssClass="link_relatorio">09 - Voucher Detalhado</asp:HyperLink>

                <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/relatorios/pagamentoCartao.aspx"
                    CssClass="link_relatorio">10 - Pagamentos com Cartão</asp:HyperLink>

                 <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/relatorios/cancelamentoVoucher.aspx"
                    CssClass="link_relatorio">11 - Cancelamentos Voucher</asp:HyperLink>

                    <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/relatorios/vouchersBaixados.aspx"
                    CssClass="link_relatorio">12 - Vouchers baixados e não baixados</asp:HyperLink>

                     <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/relatorios/recibosPendentes.aspx"
                    CssClass="link_relatorio">13 - Recibos Pendentes</asp:HyperLink>

                     <asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="~/relatorios/vendaNaoAprovada.aspx"
                    CssClass="link_relatorio">14 - Venda Não Aprovada (Cartão)</asp:HyperLink>

                    <asp:HyperLink ID="HyperLink15" runat="server" NavigateUrl="~/relatorios/ticket.aspx"
                    CssClass="link_relatorio">15 - Tickets / Ingressos (compra concluída)</asp:HyperLink>

                    <asp:HyperLink ID="HyperLink16" runat="server" NavigateUrl="~/relatorios/solicitacaoIngresso.aspx"
                    CssClass="link_relatorio">16 - Solicitações de Ingressos (por e-mail)</asp:HyperLink>

                    <asp:HyperLink ID="HyperLink17" runat="server" NavigateUrl="~/relatorios/mapaMesas.aspx"
                    CssClass="link_relatorio">17 - Mapa de Mesas</asp:HyperLink>

                    <asp:HyperLink ID="HyperLink18" runat="server" NavigateUrl="~/relatorios/alteracaoEmail.aspx"
                    CssClass="link_relatorio">18 - Alterações de E-mail</asp:HyperLink>

                    <asp:HyperLink ID="HyperLink20" runat="server" NavigateUrl="~/relatorios/retornoCatraca_app.aspx"
                    CssClass="link_relatorio">19 - Registro de Entrada (App leitor de QRCode)</asp:HyperLink>

                <asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="~/relatorios/retornoCatraca_fisica.aspx"
                    CssClass="link_relatorio">20 - Registro de Entrada (catraca física)</asp:HyperLink>

                    <asp:HyperLink ID="HyperLink19" runat="server" NavigateUrl="~/relatorios/ingressos_condominio.aspx"
                    CssClass="link_relatorio">21 - Ingressos pagos via "Condomínio"</asp:HyperLink>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
    <!--FECHAMENTO DA DIV BOX HOME-->
</asp:Content>

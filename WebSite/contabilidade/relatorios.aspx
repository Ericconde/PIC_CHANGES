<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Contabilidade.master" AutoEventWireup="true"
    CodeFile="relatorios.aspx.cs" Inherits="contabilidade_relatorios" %>

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

                <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/relatorios/cancelamentoVoucher.aspx"
                    CssClass="link_relatorio">05 - Cancelamentos Voucher</asp:HyperLink>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
    <!--FECHAMENTO DA DIV BOX HOME-->
</asp:Content>

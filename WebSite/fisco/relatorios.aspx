<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Fisco.master" AutoEventWireup="true"
    CodeFile="relatorios.aspx.cs" Inherits="fisco_relatorios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--ABERTURA DA DIV BOX HOME-->
    <div class="box_home" style="margin-left: 1%;">
        <h1>
            Relatórios</h1>
        <div class="texto_box_home_site">
            <div class="texto_box_home_candidato">
                 <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/relatorios/vendas_fisco.aspx"
                    CssClass="link_relatorio">01 - Vendas (efetivada ou não)</asp:HyperLink>

                 <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/relatorios/ingressos_fisco.aspx"
                    CssClass="link_relatorio">02 - Ingressos</asp:HyperLink>

                 <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/relatorios/resumoPontoVenda_fisco.aspx"
                    CssClass="link_relatorio">03 - Resumo por ponto de venda</asp:HyperLink>

                 <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/relatorios/vendasConsolidado_fisco.aspx"
                    CssClass="link_relatorio">04 - Vendas consolidado</asp:HyperLink>

                 <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/relatorios/resumoCortesia_fisco.aspx"
                    CssClass="link_relatorio">05 - Resumo de cortesias</asp:HyperLink>

                    <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/relatorios/lotes_fisco.aspx"
                    CssClass="link_relatorio">06 - Valores dos ingressos (lotes)</asp:HyperLink>

            </div>
        </div>
    </div>
    <div class="clear">
    </div>
    <!--FECHAMENTO DA DIV BOX HOME-->
</asp:Content>

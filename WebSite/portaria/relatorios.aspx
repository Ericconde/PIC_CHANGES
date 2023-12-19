<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Portaria.master" AutoEventWireup="true"
    CodeFile="relatorios.aspx.cs" Inherits="portaria_relatorios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--ABERTURA DA DIV BOX HOME-->
    <div class="box_home" style="margin-left: 1%;">
        <h1>
            Relatórios</h1>
        <div class="texto_box_home_site">
            <div class="texto_box_home_candidato">
                <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/relatorios/voucherDetalhado.aspx"
                    CssClass="link_relatorio">01 - Voucher Detalhado</asp:HyperLink>

                <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/relatorios/vouchersBaixados.aspx"
                    CssClass="link_relatorio">02 - Vouchers baixados e não baixados</asp:HyperLink>

                 <asp:HyperLink ID="HyperLink20" runat="server" NavigateUrl="~/relatorios/retornoCatraca_app.aspx"
                    CssClass="link_relatorio">03 - Registro de Entrada (App leitor de QRCode)</asp:HyperLink>

                <asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="~/relatorios/retornoCatraca_fisica.aspx"
                    CssClass="link_relatorio">04 - Registro de Entrada (catraca física)</asp:HyperLink>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
    <!--FECHAMENTO DA DIV BOX HOME-->
</asp:Content>

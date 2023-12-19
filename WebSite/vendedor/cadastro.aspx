<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master" AutoEventWireup="true"
    CodeFile="cadastro.aspx.cs" Inherits="vendedor_cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--ABERTURA DA DIV BOX HOME-->
    <div class="box_home" style="margin-left: 1%;">
        <h1>
            Cadastro</h1>
        <div class="texto_box_home_site">
            <div class="texto_box_home_candidato">
                <asp:HyperLink ID="hlkCadastro" runat="server" NavigateUrl="~/vendedor/clienteCadastro.aspx"
                    CssClass="link_relatorio">01 - Cadastrar Cliente</asp:HyperLink>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/vendedor/clienteCompra.aspx"
                    CssClass="link_relatorio">02 - Realizar Venda</asp:HyperLink>
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/vendedor/aprovarVenda.aspx"
                    CssClass="link_relatorio">03 - Aprovar Venda</asp:HyperLink> 
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/vendedor/baixaVoucher.aspx"
                    CssClass="link_relatorio">04 - Baixar Voucher (entrega de ingressos)</asp:HyperLink>   
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
    <!--FECHAMENTO DA DIV BOX HOME-->
</asp:Content>

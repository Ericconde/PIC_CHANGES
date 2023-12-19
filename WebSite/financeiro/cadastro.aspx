<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Financeiro.master" AutoEventWireup="true"
    CodeFile="cadastro.aspx.cs" Inherits="financeiro_cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script type="text/javascript">
    function AbrirMapa(id, href) {
        $.fancybox.open({
            href: href + id,
            type: 'iframe',
            width: 1500,
            'afterClose': function () {
                window.location.reload();
            },
            closeBtn: false,
            closeClick: false,
            helpers: {
                overlay: {
                    closeClick: false // prevents closing when clicking OUTSIDE fancybox
                }
            },
            keys: {
                close: null // prevents close when clicking escape button
            },
            minHeight: 400,
            preload: true,
            padding: 5
        });
        return false;
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--ABERTURA DA DIV BOX HOME-->
    <div class="box_home" style="margin-left: 1%;">
        <h1>
            Cadastro</h1>
        <div class="texto_box_home_site">
            <div class="texto_box_home_candidato">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/financeiro/cancelarVenda.aspx"
                    CssClass="link_relatorio">01 - Cancelar Venda</asp:HyperLink>
                <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/administrador/transacaoCartao.aspx"
                    CssClass="link_relatorio">02 - Transação Cartão</asp:HyperLink>   
                <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/financeiro/cancelarTicket.aspx"
                    CssClass="link_relatorio" >03 - Cancelar Ticket/Ingresso</asp:HyperLink>
                <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/administrador/arquivoCatraca.aspx"
                    CssClass="link_relatorio">04 - Arquivo para Catraca</asp:HyperLink>
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/administrador/usuario.aspx"
                    CssClass="link_relatorio">05 - Usuários</asp:HyperLink>
                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/financeiro/clienteDebito.aspx"
                    CssClass="link_relatorio">06 - Débito Sócio</asp:HyperLink>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
    <!--FECHAMENTO DA DIV BOX HOME-->
</asp:Content>

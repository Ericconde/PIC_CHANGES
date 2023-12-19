<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master" AutoEventWireup="true"
    CodeFile="cadastro.aspx.cs" Inherits="administrador_cadastro" %>

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
                <asp:HyperLink ID="hlkCadastro" runat="server" NavigateUrl="~/administrador/eventos.aspx"
                    CssClass="link_relatorio">01 - Eventos</asp:HyperLink>
                
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/administrador/usuario.aspx"
                    CssClass="link_relatorio">02 - Usuários</asp:HyperLink>     

                 <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/administrador/alterarEstacionamento.aspx"
                    CssClass="link_relatorio">03 - Alterar Estacionamento</asp:HyperLink> 

                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/vendedor/clienteCompra.aspx"
                    CssClass="link_relatorio">04 - Realizar Venda</asp:HyperLink> 
                    
                <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/administrador/transacaoCartao.aspx"
                    CssClass="link_relatorio">05 - Transação Cartão</asp:HyperLink>   

                     <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/vendedor/clienteCompra.aspx"
                    CssClass="link_relatorio">06 - Reservar Mesa</asp:HyperLink> 

                <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/administrador/mesasReservadas.aspx"
                    CssClass="link_relatorio">07 - Mesas Reservadas</asp:HyperLink>

                <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/administrador/arquivoCatraca.aspx"
                    CssClass="link_relatorio">08 - Arquivo para Catraca (catraca física)</asp:HyperLink>

                <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/administrador/estacionamentoAvulso.aspx"
                    CssClass="link_relatorio">09 - Estacionamento Avulso</asp:HyperLink>

                <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/administrador/cargaCatraca_app.aspx"
                    CssClass="link_relatorio">10 - Dar Carga no App - Portaria e Estacionamento (App leitor de QRCode)</asp:HyperLink>

                <asp:HyperLink ID="HyperLink15" runat="server" NavigateUrl="~/administrador/retornoCatraca_fisica.aspx"
                    CssClass="link_relatorio">11 - Retorno das Catracas (importação da catraca física)</asp:HyperLink>

                <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/administrador/baseSocios.aspx"
                    CssClass="link_relatorio">12 - Atualizar base de sócios</asp:HyperLink>

                <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/administrador/blackList.aspx"
                    CssClass="link_relatorio">13 - Blacklist</asp:HyperLink>

                 <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/vendedor/aprovarVenda.aspx"
                    CssClass="link_relatorio">14 - Aprovar Venda</asp:HyperLink> 

                <asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="~/administrador/enviarIngresso.aspx"
                    CssClass="link_relatorio">15 - Enviar Ingresso</asp:HyperLink>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
    <!--FECHAMENTO DA DIV BOX HOME-->
</asp:Content>

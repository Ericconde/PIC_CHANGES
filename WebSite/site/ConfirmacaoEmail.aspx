<%@ Page Title="" Language="C#" MasterPageFile="~/controls/Cliente.master" AutoEventWireup="true"
    CodeFile="ConfirmacaoEmail.aspx.cs" Inherits="site_ConfirmacaoEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
    function AbrirSolicitacaoIngresso(IDVenda) {
        $.fancybox.open({
            href: "../cliente/SolicitacaoIngresso.aspx?IDVenda=" + IDVenda,
            type: "iframe",
            maxWidth: 630,
            minHeight: 430,
            padding: 5
        });
        return false;
    }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1>
                    Inicial</h1>
                <div class="texto_box_home_site">

                </div>
                <div runat=server id="divMensagemSucesso" visible="false" class="alert-box success">
                    <span>Conta confirmada com sucesso!</span>
                    </div>
                    <div runat=server id="divMensagemErro" visible="false" class="alert-box error">
                    <span>Confirmação de conta inválido!</span>
                    </div><br />
                    <asp:Button ID="cmdEntrar" runat="server" Visible="false" CssClass="botao" Text="Entrar" 
                    onclick="cmdEntrar_Click" /><br />
                <asp:Label  ID="lblMensagem" runat="server" />
            </div>
            
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

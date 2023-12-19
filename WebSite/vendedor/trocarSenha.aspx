<%@ Page Title="PIC" Language="C#"  MasterPageFile="~/controls/Vendedor.master"
    AutoEventWireup="true" CodeFile="trocarSenha.aspx.cs" Inherits="vendedor_trocarSenha" %>
<%@ Register assembly="PontoBr" namespace="PontoBr.CustomWebControls" tagprefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home" style="margin-left: 1%;">
                <h1>Trocar Senha</h1>
                <div class="texto_box_home_site">
                    <table class="style4" > 
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label3" runat="server" CssClass="label" Text="Senha Atual:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <cc2:CustomTextBox ID="txtSenhaAtual" runat="server" CssClass="textbox" TextMode="Password"
                                    Width="155px"></cc2:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label4" runat="server" CssClass="label" Text="Nova Senha:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <cc2:CustomTextBox ID="txtNovaSenha" runat="server" CssClass="textbox" TextMode="Password"
                                    Width="155px"></cc2:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label5" runat="server" CssClass="label" 
                                    Text="Confirmar Nova Senha:" Width="82px"></asp:Label>
                            </td>
                            <td class="celula_campo" width="400">
                                <cc2:CustomTextBox ID="txtConfNovaSenha" runat="server" CssClass="textbox" TextMode="Password"
                                    Width="155px"></cc2:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="15">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style5" colspan="2">
                                <asp:Button ID="cmdSalvar" runat="server" CssClass="botao" 
                                    onclick="cmdSalvar_Click" Text="Salvar" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style5" colspan="2">
                                <asp:Label  ID="lblMensagem" runat="server" CssClass="label_alerta"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

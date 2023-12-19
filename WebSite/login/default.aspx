<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Site.master" AutoEventWireup="true"
    CodeFile="default.aspx.cs" Inherits="login_default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           <div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1>Acessar Sistema</h1>
                <div class="texto_box_home_site">
                    <table class="style4" > 
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label3" runat="server" CssClass="label" Text="E-mail:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <cc2:CustomTextBox ID="txtEmail" runat="server" CssClass="textbox"
                                    Width="395px"></cc2:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label4" runat="server" CssClass="label" Text="Senha:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <cc2:CustomTextBox ID="txtSenha" runat="server" CssClass="textbox" TextMode="Password"
                                    Width="155px"></cc2:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="15">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style5" colspan="2">
                            <a onclick="parent.closeFancyboxAndRedirectToUrl('../cliente/compra.aspx');">
                                <asp:Button ID="cmdLogar" runat="server" CssClass="botao" 
                                    onclick="cmdLogar_Click" Text="Logar" />
                                    </a>
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
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

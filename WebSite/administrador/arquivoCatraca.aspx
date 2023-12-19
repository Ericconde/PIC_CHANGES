<%@ Page Title="PIC" Language="C#"  MasterPageFile="~/controls/Admin.master"
    AutoEventWireup="true" CodeFile="arquivoCatraca.aspx.cs" Inherits="administrador_arquivoCatraca" %>

<%@ Register assembly="PontoBr" namespace="PontoBr.CustomWebControls" tagprefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
            <asp:PostBackTrigger ControlID="cmdGerarArquivo" />
        </Triggers>
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1>Arquivo para Catraca (catraca física)</h1>
                <div class="texto_box_home_site">
                    <table class="style4" > 
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="lblEvento" runat="server" CssClass="label" Text="Evento:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <asp:DropDownList ID="dropEdicao" runat="server" 
                                    CssClass="dropdown" 
                                    Width="400px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="lblTipo" runat="server" CssClass="label" Text="Tipo:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <asp:RadioButtonList ID="radTipo" runat="server" CssClass="radioButton" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="Inteira">Inteira</asp:ListItem>
                                    <asp:ListItem Value="Meio Adolescente">Meio Adolescente</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="15">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style5" colspan="2">
                                <asp:Button ID="cmdGerarArquivo" runat="server" CssClass="botao" 
                                    Text="Gerar Arquivo" onclick="cmdGerarArquivo_Click" />
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

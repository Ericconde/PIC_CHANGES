<%@ Page Title="PIC" Language="C#"  MasterPageFile="~/controls/Admin.master"
    AutoEventWireup="true" CodeFile="cargaCatraca_app.aspx.cs" Inherits="administrador_cargaCatraca_app" %>

<%@ Register assembly="PontoBr" namespace="PontoBr.CustomWebControls" tagprefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
            
        </Triggers>
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1>Dar Carga no App - Portaria e Estacionamento (App leitor de QRCode)</h1>
                <div class="texto_box_home_site">
                    <table class="style4" > 
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="lblEvento" runat="server" CssClass="label" Text="Evento:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <asp:DropDownList ID="dropEdicao" runat="server" 
                                    CssClass="dropdown" 
                                    Width="400px" AutoPostBack="True" OnSelectedIndexChanged="dropEdicao_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="15">
                                <asp:Label  ID="lblDados" runat="server" CssClass="label_explicacao"></asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td class="style5" colspan="2">
                                <asp:Label  ID="lblInformacao" runat="server" CssClass="label_alerta">Essa a��o ir� limpar todos os ingressos de catraca e estacionamento que ser�o lidos pelo app (smartphone). Os ingressos estar�o com status �n�o entrou�. Tenha certeza que a leitura e registro das entradas n�o foram iniciados. </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="15">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2" height="15">
                                <asp:Button ID="cmdCarga" runat="server" CssClass="botao" OnClick="cmdCarga_Click" OnClientClick="return confirm('Tem certeza que deseja limpar a base de ingressos da catraca e cadastrar uma nova?');" Text="Dar carga" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

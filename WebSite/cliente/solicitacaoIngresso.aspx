<%@ Page Language="C#" AutoEventWireup="true" CodeFile="solicitacaoIngresso.aspx.cs"
    Inherits="cliente_solicitacaoIngresso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/fancybox.css" rel="stylesheet" type="text/css" />
    <script src="../fancy/jquery-1.8.3.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" style="width: 500px; height: 150px;" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnableScriptGlobalization="True">
        <Scripts>
            <asp:ScriptReference Path="~/scripts/browser.js" />
        </Scripts>
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="cmdEnviarIngressos" />
        </Triggers>
        <ContentTemplate>
            <div class="box_home" style="width: 500px; height: 150px;">
                <h1>
                    Solicitação de Ingressos
                </h1>
                <div class="texto_box_home_site" style="width: 500px;">
                    <fieldset class="fieldset_l_Solicitacao" style="width: 600px;">
                        <legend>Confirmar a solicitação de envio dos ingressos</legend>
                        <table>
                            <tr>
                                <td class="celula_nome_campo">
                                    <asp:Label  ID="lblEmail" runat="server" CssClass="label" Text="E-mail:" Width="82px"></asp:Label>
                                </td>
                                <td class="celula_campo" width="500">
                                    <cc2:CustomTextBox ID="txtEmail" runat="server" CssClass="textbox" Width="300px"
                                        ReadOnly="True"></cc2:CustomTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td class="celula_nome_campo" colspan="2" align="center">
                                    <asp:Label  ID="lblInformativo" runat="server" CssClass="label_compra_valor"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:CheckBox ID="chkConfirmacao" runat="server" AutoPostBack="True" 
                                        CssClass="checkbox" oncheckedchanged="chkConfirmacao_CheckedChanged" 
                                        Text="Confirmo que desejo receber meus ingressos no e-mail acima citado e, ainda, que li e compreendi as orientações." />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    &nbsp;&nbsp;<asp:Button ID="cmdEnviarIngressos" runat="server" TabIndex="2"
                        Text="Enviar Ingressos" OnClick="cmdEnviarIngressos_Click" 
                        Enabled="False" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

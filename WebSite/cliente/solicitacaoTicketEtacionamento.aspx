<%@ Page Language="C#" AutoEventWireup="true" CodeFile="solicitacaoTicketEtacionamento.aspx.cs"
    Inherits="cliente_SolicitacaoTicketEstacionamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../fancy/jquery-1.8.3.js" type="text/javascript"></script>

    <Triggers>
            <asp:PostBackTrigger ControlID="cmdEnviarIngressos" />
        </Triggers>
</head>
<body>
    <form id="form1" style="width:500px; height:150px;" runat="server">
    <div class="box_home" style=" width: 500px;  height:150px;">
        <h1>
            Solicitação de Ticket Estacionamento
        </h1>
        <div class="texto_box_home_site" style="width:500px;">
            <fieldset class="fieldset_l_Solicitacao" style="width: 500px;">
            <legend>Informar e-mail para envio de Tickets</legend>
                <table class="style4">
                    <tr>
                        <td class="celula_nome_campo">
                            <asp:Label  ID="lblEmail" runat="server" CssClass="label" Text="Email:"
                                Width="82px"></asp:Label>
                        </td>
                        <td class="celula_campo" width="500">
                            <cc2:CustomTextBox ID="txtEmail" runat="server" CssClass="textbox" Width="300px"></cc2:CustomTextBox>
                        </td>
                    </tr>
                    <tr>
                    <td class="celula_nome_campo" colspan="2">
                         <asp:Label  ID="lblInformativo" runat="server" CssClass="label_compra_valor"></asp:Label>
                       </td>
                    </tr>
                </table>
            </fieldset>&nbsp;&nbsp;<asp:Button ID="cmdEnviarIngressos" runat="server" 
                CssClass="botao" TabIndex="2" Text="Enviar Ingressos" 
                onclick="cmdEnviarIngressos_Click" />
        </div>
    </div>
    </form>
</body>
</html>

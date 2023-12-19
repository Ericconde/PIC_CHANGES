<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ticketMesa.aspx.cs" Title="PIC"
    Inherits="cliente_TicketMesa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/ticketMesa.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="formEstacionamento" runat="server">
    <div id="Box_TicketMesa" style="padding-top: 50px;" runat="server">
        <%--INICIO DO LAYOUT--%>
        <asp:Repeater ID="rptTicketMesa" runat="server">
        <ItemTemplate>
        <div style="padding: 30px 30px 20px 30px; font-size:22px; page-break-inside: avoid; float: left;" align="left">
                            <table border="1" cellpadding="0" cellspacing="0" style="border-right: 1px solid; border-top: 1px solid;padding: 0px 10px 0px 10px; border-collapse: collapse;width:187pt" width="249">
                    <colgroup>
                        <col style="mso-width-source:userset;mso-width-alt:4790;width:98pt;" 
                            width="131" />
                        <col style="mso-width-source:userset;mso-width-alt:512;width:11pt;" width="14" />
                        <col style="mso-width-source:userset;mso-width-alt:3803;width:78pt;" width="104" />
                    </colgroup>
                    <tr height="20">
                        <td class="edicao" colspan="3" height="80" width="249" style="border-left: 1px solid; border-bottom: 1px solid; padding: 0px 10px 0px 10px;">
                            <span style="mso-spacerun:yes">&nbsp; </span><%# DataBinder.Eval(Container.DataItem, "Edicao")%></td>
                    </tr>
                    <tr height="20">
                        <td class="assento" colspan="3" height="80" width="249" style="border-left: 1px solid; border-bottom: 1px solid; padding: 0px 10px 0px 10px;">
                            UM<span style="mso-spacerun:yes">&nbsp; </span>ASSENTO NA MESA <%# DataBinder.Eval(Container.DataItem, "Mesa") %></td>
                    </tr>
                    <tr height="54">
                        <td class="voucher" height="54" width="131" style="border-left: 1px solid ; border-bottom: 1px solid;padding: 0px 10px 0px 10px;">
                            NÚMERO DO PEDIDO</td>
                        <td class="valor" colspan="2" width="118" style="border-left: 1px solid; border-bottom: 1px solid; padding: 0px 10px 0px 10px;">
                            <%# DataBinder.Eval(Container.DataItem, "IDVenda") %></td>
                    </tr>
                    <tr height="68">
                        <td class="voucher" height="68" width="131" style="border-left: 1px solid; border-bottom: 1px solid; padding: 0px 10px 0px 10px;">
                            COMPRADOR</td>
                        <td class="valor" colspan="2" width="118" style="border-left: 1px solid; border-bottom: 1px solid; padding: 0px 10px 0px 10px;">
                            <%# DataBinder.Eval(Container.DataItem, "Nome") %></td>
                    </tr>
                </table>
        </div>
        </ItemTemplate>
        </asp:Repeater>
        <%--FIM DO LAYOUT--%>
    </div>
    </form>
</body>
</html>

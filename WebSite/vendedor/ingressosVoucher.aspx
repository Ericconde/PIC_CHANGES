<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ingressosVoucher.aspx.cs" 
Inherits="vendedor_ingressosVoucher" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <script src="../scripts/mascara.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 750px; height: 300px; overflow-y: auto;">
    <div class="box_home">
                <h1>INGRESSOS</h1>
                <p>
                    <asp:Label  ID="lblVoucher" runat="server"></asp:Label>
                </p>
                <p>&nbsp;</p>
                    <asp:GridView ID="grdIngresso" runat="server" CssClass="grid" 
                        AutoGenerateColumns="False" Width="750px" EmptyDataText="Nenhum ingresso comprado">
                        <RowStyle CssClass="grid" HorizontalAlign="left" />
                        <Columns>
                            <asp:BoundField DataField="Ticket" HeaderText="Ticket" />
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                            <asp:BoundField DataField="Valor" HeaderText="Valor" />
                        </Columns>
                        <HeaderStyle CssClass="grid_header" HorizontalAlign="left" />
                        <AlternatingRowStyle CssClass="grid_alternative_row" />
                    </asp:GridView>
    </div>
    </div>
    </form>
</body>
</html>

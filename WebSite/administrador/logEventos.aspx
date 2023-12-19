<%@ Page Language="C#" AutoEventWireup="true" CodeFile="logEventos.aspx.cs" Inherits="administrador_logEventos" %>

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
                <h1>LOG DE ALTERAÇÕES</h1>
                    <asp:GridView ID="grdLogEvento" runat="server" CssClass="grid" DataKeyNames="IDLog"
                        AutoGenerateColumns="False" Width="750px">
                        <RowStyle CssClass="grid" HorizontalAlign="left" />
                        <Columns>
                            <asp:BoundField DataField="TextoLog" HeaderText="Log de Alterações" />
                            <asp:BoundField DataField="Usuário" HeaderText="Usuário" />
                            <asp:BoundField DataField="Data/Hora Alteração" HeaderText="Data/Hora Alteração" />
                        </Columns>
                        <HeaderStyle CssClass="grid_header" HorizontalAlign="left" />
                        <AlternatingRowStyle CssClass="grid_alternative_row" />
                    </asp:GridView>
    </div>
    </div>
    </form>
</body>
</html>

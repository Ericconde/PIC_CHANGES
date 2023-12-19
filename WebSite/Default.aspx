<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PIC</title>
    </head>

    

<body>
    <form id="form1" runat="server">
    

        Tipo de ingresso a ser lido:<br />
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Selected="True">Inteira</asp:ListItem>
            <asp:ListItem>Meia</asp:ListItem>
            <asp:ListItem>Estacionamento</asp:ListItem>
        </asp:RadioButtonList>
    

    </form>
    </body>
</html>


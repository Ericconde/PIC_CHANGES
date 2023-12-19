<%@ Page Language="C#" AutoEventWireup="true" CodeFile="historicoCadastroCliente.aspx.cs" Inherits="relatorios_historicocadastrocliente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <script src="../scripts/mascara.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 750px; height: 300px; overflow-y: auto;">
    <div class="box_home">
                <h1>LOG DE ALTERAÇÕES</h1>
                    <asp:GridView ID="grdHistoricoCadastro" runat="server" CssClass="grid" DataKeyNames="IDCliente"
                        AutoGenerateColumns="False" Width="750px" EmptyDataText="Não existe alteração para esse Cliente!">
                        <RowStyle CssClass="grid" HorizontalAlign="left" />
                        <Columns>
                            <asp:BoundField DataField="TipoRegistro" HeaderText="Tipo Alteração" />
                            <asp:BoundField DataField="RegistroAntigo" HeaderText="Registro Antigo" />
                            <asp:BoundField DataField="RegistroAtual" HeaderText="Registro Atual" />
                            <asp:BoundField DataField="Cadastro" HeaderText="Data Hora/ Alteração" />
                        </Columns>
                        <HeaderStyle CssClass="grid_header" HorizontalAlign="left" />
                        <AlternatingRowStyle CssClass="grid_alternative_row" />
                    </asp:GridView>
                <br />
    </div>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reciboPagamento.aspx.cs"
    Inherits="cliente_reciboPagamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../scripts/mascara.js" type="text/javascript"></script>
    <script src="../scripts/browser.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ImprimirDiv(divNome) {
            var printContents = document.getElementById(divNome).innerHTML;
            w = window.open();
            w.document.write(printContents);
            w.print();
            w.close();
        }
    </script>
    <style>
         .inicial1 {
                        background-color: black;
                        color: white;
                        border: 0px solid;
                        width: 150px;
                        font-size: 12px;
                        font-weight: bold;
                        border-radius: 5px;
                        height: 28px;
                       
                    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server" EnableScriptGlobalization="True">
            <Scripts>
                <asp:ScriptReference Path="~/scripts/browser.js" />
            </Scripts>
        </asp:ScriptManager>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <!--ABERTURA DA DIV BOX HOME-->
                    <center>
                        <div style=" border: 1px solid black; font-family:Calibri; width: 300px; align-items: center" id="recibo">
                            <h1>Ingressos PIC</h1>
                            <h3>Recibo para Pagamento</h3>
                            <div style="margin-left:5px" class="texto_box_home_site">
                                <asp:Label ID="lblVoucher1" runat="server"></asp:Label>
                            </div>
                            <asp:GridView ID="grdRelatorio" runat="server" Width="300px"
                                AutoGenerateColumns="False">

                                <Columns>
                                    <asp:BoundField  ItemStyle-HorizontalAlign="Center" DataField="Tipo de Ingresso" HeaderText="Tipo de Ingresso" />
                                    <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#0089fe" HeaderStyle-BorderColor="#0089fe" DataField="Quantidade" HeaderText="Quantidade" />
                                </Columns>
                                <HeaderStyle ForeColor="White"  BackColor="#0089fe" BorderColor="#0089fe" HorizontalAlign="Center"  />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="grid_page" />
                            </asp:GridView>
                            <div style="margin-left:5px"  class="texto_box_home_site">
                                <asp:Label ID="lblVoucher2" runat="server"></asp:Label>
                            </div>
                            <br />
                            <center>
                                <input type="button" style="background-color:#0089fe"; class="inicial1"    onclick="ImprimirDiv('recibo')" value="Imprimir" />
                                <br />
                            </center>
                            <br />
                        </div>
                    </center>
                    <!--FECHAMENTO DA DIV BOX HOME-->
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>

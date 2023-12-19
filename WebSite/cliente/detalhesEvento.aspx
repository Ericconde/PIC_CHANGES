<%@ Page Language="C#" AutoEventWireup="true" CodeFile="detalhesEvento.aspx.cs" Inherits="cliente_detalhesEvento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <script src="../scripts/mascara.js" type="text/javascript"></script>
    <script src="../scripts/browser.js" type="text/javascript"></script>
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
                <div class="box_home">
                   <div class="texto_box_home_site">
                        
                        <asp:Label  ID="lblDetalhes" runat="server"></asp:Label>
                        
                    </div>
                </div>
                
                <!--FECHAMENTO DA DIV BOX HOME-->
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>

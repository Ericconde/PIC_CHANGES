<%@ Page Language="C#" AutoEventWireup="true" CodeFile="voucher.aspx.cs" Inherits="relatorios_voucher" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <CR:CrystalReportViewer ID="IngressosPIC" runat="server" 
                                ToolbarStyle-BackColor="#E0E0E0" 
            HasZoomFactorList="False" 
                                HasToggleGroupTreeButton="False" 
            HasSearchButton="False" HasGotoPageButton="False"
                                HasDrillUpButton="False" HasCrystalLogo="False" EnableParameterPrompt="False"
                                EnableDatabaseLogonPrompt="False" 
             BestFitPage="False"
                                AutoDataBind="true"  ></CR:CrystalReportViewer>
    </div>
    </form>
</body>
</html>

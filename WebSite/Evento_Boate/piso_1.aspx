<%@ Page Language="C#" AutoEventWireup="true" CodeFile="piso_1.aspx.cs" Inherits="eventos_Boate_piso_1" validateRequest="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Compra de Ingressos para o Reveillon do PIC</title>
    <link href="css/bootstrap.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">   
    <script src="scripts/web.js"></script>
</head>
<body style="background-color: #009652;">
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnableScriptGlobalization="True">
        <Scripts>
            <asp:ScriptReference Path="~/scripts/browser.js" />
        </Scripts>
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
             
                <div id="menu">
                    <ul>
                        <li><a href="piso_1.aspx" class="ativo">1º PISO</a></li>
                        <li><a href="piso_2.aspx">2º PISO</a></li>
                    </ul>
                    <div align="right" style="margin-right: 5px">
                        <button class="botao" style="color: black;background-color: white; border-color: white; border: solid 1px white; border-radius: 10px; text-align: center; width: 30px; height: 30px" onclick="parent.jQuery.fancybox.close()"><b>X</b></button>
                    </div>
                </div>
            </div>
            <table>
                <tr>
                    <td rowspan="3">
                        <div class="container-fluid" runat="server" id="cadeiras">
                            <img class="img-responsive" src="../../images/imgBoate/piso_1.jpg" alt="" />


                              <asp:ImageButton ID="ImageButton_mesa_001_1" runat="server" Style="left: 232px; position: absolute;
                            top: 466px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_001_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_001_2" runat="server" Style="left: 243px; position: absolute;
                            top: 466px; width: 10px; height: 11px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_001_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_001_3" runat="server" Style="left: 232px; position: absolute;
                            top: 440px; width: 9px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_001_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_001_4" runat="server" Style="left: 244px; position: absolute;
                            top: 440px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_001_4_Click"/>

                                <asp:ImageButton ID="ImageButton_mesa_002_1" runat="server" Style="left: 232px; position: absolute;
                            top: 422px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_002_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_002_2" runat="server" Style="left: 243px; position: absolute;
                            top: 422px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_002_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_002_3" runat="server" Style="left: 232px; position: absolute;
                            top: 397px; width: 9px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_002_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_002_4" runat="server" Style="left: 244px; position: absolute;
                            top: 397px; width: 9px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_002_4_Click"/>

                                <asp:ImageButton ID="ImageButton_mesa_003_1" runat="server" Style="left: 268px; position: absolute;
                            top: 446px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_003_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_003_2" runat="server" Style="left: 280px; position: absolute;
                            top: 446px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_003_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_003_3" runat="server" Style="left: 268px; position: absolute;
                            top: 421px; width: 9px; height: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_003_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_003_4" runat="server" Style="left: 280px; position: absolute;
                            top: 421px; width: 9px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_003_4_Click"/>

                                <asp:ImageButton ID="ImageButton_mesa_004_1" runat="server" Style="left: 255px; position: absolute;
                            top: 326px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_004_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_004_2" runat="server" Style="left: 265px; position: absolute;
                            top: 331px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_004_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_004_3" runat="server" Style="left: 268px; position: absolute;
                            top: 304px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_004_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_004_4" runat="server" Style="left: 279px; position: absolute;
                            top: 310px; width: 9px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_004_4_Click"/>

                                <asp:ImageButton ID="ImageButton_mesa_005_1" runat="server" Style="left: 306px; position: absolute;
                            top: 315px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_005_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_005_2" runat="server" Style="left: 313px; position: absolute;
                            top: 305px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_005_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_005_3" runat="server" Style="left: 327px; position: absolute;
                            top: 328px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_005_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_005_4" runat="server" Style="left: 335px; position: absolute;
                            top: 320px; width: 9px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_005_4_Click"/>

                                <asp:ImageButton ID="ImageButton_mesa_006_1" runat="server" Style="left: 348px; position: absolute;
                            top: 332px; width: 9px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_006_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_006_2" runat="server" Style="left: 357px; position: absolute;
                            top: 322px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_006_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_006_3" runat="server" Style="left: 376px; position: absolute;
                            top: 336px; width: 9px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_006_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_006_4" runat="server" Style="left: 370px; position: absolute;
                            top: 346px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_006_4_Click"/>

                                <asp:ImageButton ID="ImageButton_mesa_007_1" runat="server" Style="left: 367px; position: absolute;
                            top: 371px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_007_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_007_2" runat="server" Style="left: 372px; position: absolute;
                            top: 360px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_007_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_007_3" runat="server" Style="left: 388px; position: absolute;
                            top: 384px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_007_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_007_4" runat="server" Style="left: 395px; position: absolute;
                            top: 374px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_007_4_Click"/>

                                <asp:ImageButton ID="ImageButton_mesa_008_1" runat="server" Style="left: 365px; position: absolute;
                            top: 215px; height: 10px; width: 9px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_008_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_008_2" runat="server" Style="left: 364px; position: absolute;
                            top: 227px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_008_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_008_3" runat="server" Style="left: 390px; position: absolute;
                            top: 215px; height: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_008_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_008_4" runat="server" Style="left: 390px; position: absolute;
                            top: 227px; height: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_008_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_009_1" runat="server" Style="left: 364px; position: absolute;
                            top: 248px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_009_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_009_2" runat="server" Style="left: 364px; position: absolute;
                            top: 261px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_009_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_009_3" runat="server" Style="left: 390px; position: absolute;
                            top: 248px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_009_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_009_4" runat="server" Style="left: 390px; position: absolute;
                            top: 261px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_009_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_010_1" runat="server" Style="left: 415px; position: absolute;
                            top: 215px; width: 9px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_010_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_010_2" runat="server" Style="left: 414px; position: absolute;
                            top: 227px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_010_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_010_3" runat="server" Style="left: 442px; position: absolute;
                            top: 215px; height: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_010_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_010_4" runat="server" Style="left: 442px; position: absolute;
                            top: 227px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_010_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_011_1" runat="server" Style="left: 415px; position: absolute;
                            top: 248px; width: 9px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_011_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_011_2" runat="server" Style="left: 414px; position: absolute;
                            top: 261px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_011_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_011_3" runat="server" Style="left: 442px; position: absolute;
                            top: 248px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_011_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_011_4" runat="server" Style="left: 442px; position: absolute;
                            top: 261px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_011_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_012_1" runat="server" Style="left: 471px; position: absolute;
                            top: 228px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_012_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_012_2" runat="server" Style="left: 482px; position: absolute;
                            top: 228px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_012_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_012_3" runat="server" Style="left: 471px; position: absolute;
                            top: 254px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_012_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_012_4" runat="server" Style="left: 483px; position: absolute;
                            top: 255px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_012_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_013_1" runat="server" Style="left: 519px; position: absolute;
                            top: 215px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_013_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_013_2" runat="server" Style="left: 530px; position: absolute;
                            top: 215px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_013_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_013_3" runat="server" Style="left: 519px; position: absolute;
                            top: 240px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_013_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_013_4" runat="server" Style="left: 530px; position: absolute;
                            top: 240px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_013_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_014_1" runat="server" Style="left: 556px; position: absolute;
                            top: 215px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_014_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_014_2" runat="server" Style="left: 568px; position: absolute;
                            top: 215px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_014_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_014_3" runat="server" Style="left: 556px; position: absolute;
                            top: 240px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_014_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_014_4" runat="server" Style="left: 568px; position: absolute;
                            top: 240px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_014_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_015_1" runat="server" Style="left: 594px; position: absolute;
                            top: 215px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_015_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_015_2" runat="server" Style="left: 606px; position: absolute;
                            top: 215px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_015_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_015_3" runat="server" Style="left: 594px; position: absolute;
                            top: 240px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_015_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_015_4" runat="server" Style="left: 606px; position: absolute;
                            top: 240px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_015_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_016_1" runat="server" Style="left: 640px; position: absolute;
                            top: 184px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_016_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_016_2" runat="server" Style="left: 652px; position: absolute;
                            top: 184px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_016_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_016_3" runat="server" Style="left: 640px; position: absolute;
                            top: 209px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_016_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_016_4" runat="server" Style="left: 652px; position: absolute;
                            top: 209px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_016_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_017_1" runat="server" Style="left: 640px; position: absolute;
                            top: 232px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_017_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_017_2" runat="server" Style="left: 652px; position: absolute;
                            top: 232px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_017_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_017_3" runat="server" Style="left: 640px; position: absolute;
                            top: 257px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_017_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_017_4" runat="server" Style="left: 652px; position: absolute;
                            top: 257px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_017_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_018_1" runat="server" Style="left: 678px; position: absolute;
                            top: 184px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_018_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_018_2" runat="server" Style="left: 690px; position: absolute;
                            top: 184px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_018_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_018_3" runat="server" Style="left: 678px; position: absolute;
                            top: 209px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_018_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_018_4" runat="server" Style="left: 690px; position: absolute;
                            top: 209px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_018_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_019_1" runat="server" Style="left: 678px; position: absolute;
                            top: 232px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_019_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_019_2" runat="server" Style="left: 690px; position: absolute;
                            top: 232px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_019_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_019_3" runat="server" Style="left: 678px; position: absolute;
                            top: 257px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_019_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_019_4" runat="server" Style="left: 690px; position: absolute;
                            top: 257px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_019_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_020_1" runat="server" Style="left: 678px; position: absolute;
                            top: 279px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_020_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_020_2" runat="server" Style="left: 690px; position: absolute;
                            top: 279px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_020_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_020_3" runat="server" Style="left: 678px; position: absolute;
                            top: 304px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_020_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_020_4" runat="server" Style="left: 690px; position: absolute;
                            top: 304px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_020_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_021_1" runat="server" Style="left: 678px; position: absolute;
                            top: 328px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_021_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_021_2" runat="server" Style="left: 690px; position: absolute;
                            top: 328px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_021_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_021_3" runat="server" Style="left: 678px; position: absolute;
                            top: 354px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_021_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_021_4" runat="server" Style="left: 690px; position: absolute;
                            top: 354px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_021_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_022_1" runat="server" Style="left: 678px; position: absolute;
                            top: 374px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_022_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_022_2" runat="server" Style="left: 690px; position: absolute;
                            top: 374px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_022_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_022_3" runat="server" Style="left: 678px; position: absolute;
                            top: 400px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_022_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_022_4" runat="server" Style="left: 690px; position: absolute;
                            top: 400px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_022_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_023_1" runat="server" Style="left: 678px; position: absolute;
                            top: 424px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_023_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_023_2" runat="server" Style="left: 690px; position: absolute;
                            top: 424px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_023_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_023_3" runat="server" Style="left: 678px; position: absolute;
                            top: 450px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_023_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_023_4" runat="server" Style="left: 690px; position: absolute;
                            top: 450px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_023_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_024_1" runat="server" Style="left: 718px; position: absolute;
                            top: 184px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_024_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_024_2" runat="server" Style="left: 730px; position: absolute;
                            top: 184px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_024_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_024_3" runat="server" Style="left: 718px; position: absolute;
                            top: 209px; " ImageUrl="~/images/cadeiraLivre.png"
                                onclick="ImageButton_mesa_024_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_024_4" runat="server" Style="left: 730px; position: absolute;
                            top: 209px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_024_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_025_1" runat="server" Style="left: 718px; position: absolute;
                            top: 232px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_025_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_025_2" runat="server" Style="left: 730px; position: absolute;
                            top: 232px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_025_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_025_3" runat="server" Style="left: 718px; position: absolute;
                            top: 257px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_025_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_025_4" runat="server" Style="left: 730px; position: absolute;
                            top: 257px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_025_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_026_1" runat="server" Style="left: 718px; position: absolute;
                            top: 279px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_026_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_026_2" runat="server" Style="left: 730px; position: absolute;
                            top: 279px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_026_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_026_3" runat="server" Style="left: 718px; position: absolute;
                            top: 304px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_026_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_026_4" runat="server" Style="left: 730px; position: absolute;
                            top: 304px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_026_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_027_1" runat="server" Style="left: 718px; position: absolute;
                            top: 328px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_027_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_027_2" runat="server" Style="left: 730px; position: absolute;
                            top: 328px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_027_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_027_3" runat="server" Style="left: 718px; position: absolute;
                            top: 354px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_027_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_027_4" runat="server" Style="left: 730px; position: absolute;
                            top: 354px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_027_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_028_1" runat="server" Style="left: 718px; position: absolute;
                            top: 374px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_028_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_028_2" runat="server" Style="left: 730px; position: absolute;
                            top: 374px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_028_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_028_3" runat="server" Style="left: 718px; position: absolute;
                            top: 400px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_028_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_028_4" runat="server" Style="left: 730px; position: absolute;
                            top: 400px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_028_4_Click"/>

                            <asp:ImageButton ID="ImageButton_mesa_029_1" runat="server" Style="left: 718px; position: absolute;
                            top: 424px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_029_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_029_2" runat="server" Style="left: 730px; position: absolute;
                            top: 424px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_029_2_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_029_3" runat="server" Style="left: 718px; position: absolute;
                            top: 450px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_029_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_029_4" runat="server" Style="left: 730px; position: absolute;
                            top: 450px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_029_4_Click"/>

                        </div>
                        <spam style="color: White; padding-left: 15px;"> Mapa apenas ilustrativo. As mesas poderão sofrer alterações em sua localização no dia do evento.</spam>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td valign="top" align="left">
                        <table class="nav-justified">
                            <tr style="height: 20px;">
                                <td align="center">
                                    <asp:Label  ID="lblOrientacao" runat="server" CssClass="label_ingresso_vermelho" Text="Primeiro escolha o tipo de ingresso que queira comprar e em seguida clique na cadeira desejada"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label  ID="lblTipoIngresso" runat="server" CssClass="label" Text="Comprar tipo de ingresso:"></asp:Label>
                                </td>
                                <td align="center">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <%--<asp:RadioButtonList ID="radTipoCadeira" runat="server" CssClass="radioButton" RepeatDirection="Horizontal">
                                        <asp:ListItem>Masculino</asp:ListItem>
                                        <asp:ListItem>Feminino</asp:ListItem>
                                    </asp:RadioButtonList>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label  ID="lblValor" runat="server" CssClass="label" 
                                        Text="Escolha o valor:" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:DropDownList ID="dropValorIngresso" runat="server" CssClass="dropdown" 
                                        Width="400px" Visible="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label  ID="lblSocio" runat="server" CssClass="label" Text="Saldo Ingressos Sócio"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label  ID="lblSocio_valor" runat="server" CssClass="label_saldo" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label  ID="lblNaoSocio" runat="server" CssClass="label" Text="Saldo Ingressos Não Sócio"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label  ID="lblNaoSocio_valor" runat="server" CssClass="label_saldo" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left">
                                <td colspan="2">
                                    <asp:Label  ID="lblNumeroIngressosCadeira" runat="server" CssClass="label_ingresso"
                                        Text="Regra: Número de ingressos disponíveis<br/>em mesa por cota/título: 4"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" height="20">
                                    <asp:Label  ID="lblLegenda" runat="server" CssClass="label" Text="Legenda:"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left">
                                <td align="left" colspan="2">
                                    <table class="nav-justified">
                                        <tr>
                                            <td align="left">
                                                <img alt="" class="circleborder" src="../../images/cadeiraLivre.png" />
                                            </td>
                                            <td>
                                                <asp:Label  ID="lblLegenda1" runat="server" CssClass="label_legenda" Text="Disponível"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <img alt="" class="style1" src="../../images/cadeiraProcessoCompra.png" />
                                            </td>
                                            <td>
                                                <asp:Label  ID="Label1" runat="server" CssClass="label_legenda" Text="Selecionada"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <img alt="" class="style1" src="../../images/cadeiraBloqueada.png" />
                                            </td>
                                            <td>
                                                <asp:Label  ID="Label3" runat="server" CssClass="label_legenda" Text="Em processo de compra por outro cliente"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <img alt="" class="style1" src="../../images/cadeiraVendida.png" />
                                            </td>
                                            <td>
                                                <asp:Label  ID="Label2" runat="server" CssClass="label_legenda" Text="Vendida"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2" height="20">
                                                <asp:LinkButton ID="LinkButtonAbrirMapaInteira" runat="server" CssClass="linkBotao"
                                                    OnClientClick="parent.jQuery.fancybox.close();" Width="100px">Avançar</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

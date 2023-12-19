<%@ Page Language="C#" AutoEventWireup="true" CodeFile="setor_salao_de_festas.aspx.cs"
    Inherits="eventos_festaJunina_setor_salao_de_festas" validateRequest="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Compra de Ingressos para Festa Junina do PIC</title>
    <link href="css/bootstrap.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <script src="scripts/web.js"></script>
</head>
<body style="background-color: #009652;">
    <form id="form1" runat="server">
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
                        <li><a href="index.aspx">MAPA GERAL DO EVENTO</a></li>
                        <li><a href="setor_salao_de_festas.aspx" class="ativo">SETOR SALÃO DE FESTAS</a></li>
                        <li><a href="setor_golodromo.aspx">SETOR GOLÓDROMO</a></li>
                        <li><a href="setor_pergula.aspx">SETOR PÉRGULA</a></li>
                        <li><a href="setor_portinari.aspx">SETOR PORTINARI</a></li>
                        <li><a href="setor_ipanema.aspx">SETOR IPANEMA</a></li>
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
                            <img class="img-responsive" src="../../images/imgFestaJunina/bg-salao-de-festas.jpg" />
                            <asp:ImageButton ID="ImageButton_mesa_001_1" runat="server" Style="left: 67px; position: absolute;
                                top: 329px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_001_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_001_2" runat="server" Style="left: 82px; position: absolute;
                                top: 338px; width: 9px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_001_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_001_3" runat="server" Style="left: 81px; position: absolute;
                                top: 354px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_001_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_001_4" runat="server" Style="left: 68px; position: absolute;
                                top: 362px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_001_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_001_5" runat="server" Style="left: 53px; position: absolute;
                                top: 355px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_001_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_001_6" runat="server" Style="left: 53px; position: absolute;
                                top: 339px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_001_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_002_1" runat="server" Style="left: 123px; position: absolute;
                                top: 329px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_002_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_002_2" runat="server" Style="left: 136px; position: absolute;
                                top: 336px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_002_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_002_3" runat="server" Style="left: 137px; position: absolute;
                                top: 352px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_002_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_002_4" runat="server" Style="left: 122px; position: absolute;
                                top: 360px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_002_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_002_5" runat="server" Style="left: 107px; position: absolute;
                                top: 353px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_002_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_002_6" runat="server" Style="left: 108px; position: absolute;
                                top: 336px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_002_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_003_1" runat="server" Style="left: 177px; position: absolute;
                                top: 326px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_003_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_003_2" runat="server" Style="left: 191px; position: absolute;
                                top: 333px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_003_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_003_3" runat="server" Style="left: 192px; position: absolute;
                                top: 348px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_003_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_003_4" runat="server" Style="left: 177px; position: absolute;
                                top: 357px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_003_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_003_5" runat="server" Style="left: 162px; position: absolute;
                                top: 350px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_003_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_003_6" runat="server" Style="left: 163px; position: absolute;
                                top: 334px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_003_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_004_1" runat="server" Style="left: 318px; position: absolute;
                                top: 318px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_004_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_004_2" runat="server" Style="left: 332px; position: absolute;
                                top: 325px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_004_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_004_3" runat="server" Style="left: 332px; position: absolute;
                                top: 341px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_004_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_004_4" runat="server" Style="left: 319px; position: absolute;
                                top: 349px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_004_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_004_5" runat="server" Style="left: 305px; position: absolute;
                                top: 341px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_004_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_004_6" runat="server" Style="left: 304px; position: absolute;
                                top: 326px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_004_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_005_1" runat="server" Style="left: 374px; position: absolute;
                                top: 315px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_005_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_005_2" runat="server" Style="left: 387px; position: absolute;
                                top: 322px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_005_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_005_3" runat="server" Style="left: 387px; position: absolute;
                                top: 338px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_005_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_005_4" runat="server" Style="left: 373px; position: absolute;
                                top: 346px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_005_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_005_5" runat="server" Style="left: 359px; position: absolute;
                                top: 339px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_005_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_005_6" runat="server" Style="left: 358px; position: absolute;
                                top: 323px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_005_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_006_1" runat="server" Style="left: 428px; position: absolute;
                                top: 313px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_006_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_006_2" runat="server" Style="left: 442px; position: absolute;
                                top: 321px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_006_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_006_3" runat="server" Style="left: 445px; position: absolute;
                                top: 336px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_006_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_006_4" runat="server" Style="left: 428px; position: absolute;
                                top: 343px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_006_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_006_5" runat="server" Style="left: 414px; position: absolute;
                                top: 336px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_006_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_006_6" runat="server" Style="left: 414px; position: absolute;
                                top: 320px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_006_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_007_1" runat="server" Style="left: 483px; position: absolute;
                                top: 311px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_007_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_007_2" runat="server" Style="left: 496px; position: absolute;
                                top: 318px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_007_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_007_3" runat="server" Style="left: 497px; position: absolute;
                                top: 332px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_007_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_007_4" runat="server" Style="left: 483px; position: absolute;
                                top: 342px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_007_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_007_5" runat="server" Style="left: 469px; position: absolute;
                                top: 333px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_007_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_007_6" runat="server" Style="left: 470px; position: absolute;
                                top: 317px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_007_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_008_1" runat="server" Style="left: 538px; position: absolute;
                                top: 308px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_008_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_008_2" runat="server" Style="left: 552px; position: absolute;
                                top: 315px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_008_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_008_3" runat="server" Style="left: 555px; position: absolute;
                                top: 332px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_008_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_008_4" runat="server" Style="left: 538px; position: absolute;
                                top: 338px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_008_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_008_5" runat="server" Style="left: 524px; position: absolute;
                                top: 330px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_008_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_008_6" runat="server" Style="left: 524px; position: absolute;
                                top: 315px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_008_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_009_1" runat="server" Style="left: 593px; position: absolute;
                                top: 304px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_009_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_009_2" runat="server" Style="left: 607px; position: absolute;
                                top: 312px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_009_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_009_3" runat="server" Style="left: 608px; position: absolute;
                                top: 329px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_009_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_009_4" runat="server" Style="left: 593px; position: absolute;
                                top: 336px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_009_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_009_5" runat="server" Style="left: 579px; position: absolute;
                                top: 328px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_009_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_009_6" runat="server" Style="left: 579px; position: absolute;
                                top: 312px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_009_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_010_1" runat="server" Style="left: 67px; position: absolute;
                                top: 401px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_010_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_010_2" runat="server" Style="left: 82px; position: absolute;
                                top: 407px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_010_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_010_3" runat="server" Style="left: 81px; position: absolute;
                                top: 423px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_010_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_010_4" runat="server" Style="left: 68px; position: absolute;
                                top: 432px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_010_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_010_5" runat="server" Style="left: 53px; position: absolute;
                                top: 426px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_010_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_010_6" runat="server" Style="left: 53px; position: absolute;
                                top: 408px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_010_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_011_1" runat="server" Style="left: 123px; position: absolute;
                                top: 398px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_011_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_011_2" runat="server" Style="left: 136px; position: absolute;
                                top: 405px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_011_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_011_3" runat="server" Style="left: 137px; position: absolute;
                                top: 421px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_011_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_011_4" runat="server" Style="left: 122px; position: absolute;
                                top: 429px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_011_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_011_5" runat="server" Style="left: 107px; position: absolute;
                                top: 422px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_011_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_011_6" runat="server" Style="left: 108px; position: absolute;
                                top: 406px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_011_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_012_1" runat="server" Style="left: 177px; position: absolute;
                                top: 395px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_012_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_012_2" runat="server" Style="left: 191px; position: absolute;
                                top: 403px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_012_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_012_3" runat="server" Style="left: 192px; position: absolute;
                                top: 419px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_012_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_012_4" runat="server" Style="left: 177px; position: absolute;
                                top: 426px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_012_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_012_5" runat="server" Style="left: 162px; position: absolute;
                                top: 419px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_012_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_012_6" runat="server" Style="left: 163px; position: absolute;
                                top: 404px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_012_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_013_1" runat="server" Style="left: 318px; position: absolute;
                                top: 388px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_013_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_013_2" runat="server" Style="left: 332px; position: absolute;
                                top: 396px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_013_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_013_3" runat="server" Style="left: 332px; position: absolute;
                                top: 410px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_013_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_013_4" runat="server" Style="left: 319px; position: absolute;
                                top: 419px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_013_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_013_5" runat="server" Style="left: 305px; position: absolute;
                                top: 411px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_013_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_013_6" runat="server" Style="left: 304px; position: absolute;
                                top: 395px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_013_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_014_1" runat="server" Style="left: 374px; position: absolute;
                                top: 384px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_014_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_014_2" runat="server" Style="left: 388px; position: absolute;
                                top: 391px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_014_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_014_3" runat="server" Style="left: 389px; position: absolute;
                                top: 409px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_014_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_014_4" runat="server" Style="left: 374px; position: absolute;
                                top: 416px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_014_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_014_5" runat="server" Style="left: 359px; position: absolute;
                                top: 408px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_014_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_014_6" runat="server" Style="left: 359px; position: absolute;
                                top: 393px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_014_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_015_1" runat="server" Style="left: 428px; position: absolute;
                                top: 382px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_015_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_015_2" runat="server" Style="left: 442px; position: absolute;
                                top: 390px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_015_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_015_3" runat="server" Style="left: 445px; position: absolute;
                                top: 406px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_015_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_015_4" runat="server" Style="left: 428px; position: absolute;
                                top: 413px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_015_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_015_5" runat="server" Style="left: 414px; position: absolute;
                                top: 406px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_015_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_015_6" runat="server" Style="left: 414px; position: absolute;
                                top: 389px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_015_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_016_1" runat="server" Style="left: 483px; position: absolute;
                                top: 380px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_016_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_016_2" runat="server" Style="left: 496px; position: absolute;
                                top: 388px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_016_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_016_3" runat="server" Style="left: 497px; position: absolute;
                                top: 403px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_016_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_016_4" runat="server" Style="left: 483px; position: absolute;
                                top: 410px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_016_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_016_5" runat="server" Style="left: 469px; position: absolute;
                                top: 403px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_016_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_016_6" runat="server" Style="left: 470px; position: absolute;
                                top: 388px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_016_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_017_1" runat="server" Style="left: 538px; position: absolute;
                                top: 377px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_017_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_017_2" runat="server" Style="left: 552px; position: absolute;
                                top: 384px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_017_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_017_3" runat="server" Style="left: 551px; position: absolute;
                                top: 399px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_017_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_017_4" runat="server" Style="left: 538px; position: absolute;
                                top: 408px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_017_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_017_5" runat="server" Style="left: 524px; position: absolute;
                                top: 400px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_017_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_017_6" runat="server" Style="left: 524px; position: absolute;
                                top: 385px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_017_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_018_1" runat="server" Style="left: 593px; position: absolute;
                                top: 374px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_018_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_018_2" runat="server" Style="left: 607px; position: absolute;
                                top: 381px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_018_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_018_3" runat="server" Style="left: 608px; position: absolute;
                                top: 397px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_018_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_018_4" runat="server" Style="left: 593px; position: absolute;
                                top: 404px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_018_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_018_5" runat="server" Style="left: 579px; position: absolute;
                                top: 398px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_018_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_018_6" runat="server" Style="left: 579px; position: absolute;
                                top: 383px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_018_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_019_1" runat="server" Style="left: 67px; position: absolute;
                                top: 469px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_019_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_019_2" runat="server" Style="left: 82px; position: absolute;
                                top: 475px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_019_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_019_3" runat="server" Style="left: 81px; position: absolute;
                                top: 493px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_019_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_019_4" runat="server" Style="left: 68px; position: absolute;
                                top: 500px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_019_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_019_5" runat="server" Style="left: 53px; position: absolute;
                                top: 492px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_019_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_019_6" runat="server" Style="left: 53px; position: absolute;
                                top: 478px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_019_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_020_1" runat="server" Style="left: 123px; position: absolute;
                                top: 467px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_020_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_020_2" runat="server" Style="left: 136px; position: absolute;
                                top: 473px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_020_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_020_3" runat="server" Style="left: 137px; position: absolute;
                                top: 490px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_020_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_020_4" runat="server" Style="left: 122px; position: absolute;
                                top: 498px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_020_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_020_5" runat="server" Style="left: 107px; position: absolute;
                                top: 491px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_020_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_020_6" runat="server" Style="left: 108px; position: absolute;
                                top: 475px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_020_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_021_1" runat="server" Style="left: 177px; position: absolute;
                                top: 464px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_021_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_021_2" runat="server" Style="left: 191px; position: absolute;
                                top: 471px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_021_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_021_3" runat="server" Style="left: 192px; position: absolute;
                                top: 488px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_021_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_021_4" runat="server" Style="left: 177px; position: absolute;
                                top: 495px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_021_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_021_5" runat="server" Style="left: 162px; position: absolute;
                                top: 487px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_021_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_021_6" runat="server" Style="left: 163px; position: absolute;
                                top: 472px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_021_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_022_1" runat="server" Style="left: 318px; position: absolute;
                                top: 456px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_022_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_022_2" runat="server" Style="left: 332px; position: absolute;
                                top: 463px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_022_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_022_3" runat="server" Style="left: 332px; position: absolute;
                                top: 479px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_022_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_022_4" runat="server" Style="left: 319px; position: absolute;
                                top: 487px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_022_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_022_5" runat="server" Style="left: 305px; position: absolute;
                                top: 478px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_022_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_022_6" runat="server" Style="left: 304px; position: absolute;
                                top: 464px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_022_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_023_1" runat="server" Style="left: 374px; position: absolute;
                                top: 453px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_023_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_023_2" runat="server" Style="left: 388px; position: absolute;
                                top: 460px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_023_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_023_3" runat="server" Style="left: 389px; position: absolute;
                                top: 478px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_023_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_023_4" runat="server" Style="left: 374px; position: absolute;
                                top: 484px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_023_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_023_5" runat="server" Style="left: 359px; position: absolute;
                                top: 476px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_023_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_023_6" runat="server" Style="left: 359px; position: absolute;
                                top: 461px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_023_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_024_1" runat="server" Style="left: 428px; position: absolute;
                                top: 449px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_024_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_024_2" runat="server" Style="left: 442px; position: absolute;
                                top: 459px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_024_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_024_3" runat="server" Style="left: 445px; position: absolute;
                                top: 476px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_024_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_024_4" runat="server" Style="left: 428px; position: absolute;
                                top: 480px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_024_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_024_5" runat="server" Style="left: 414px; position: absolute;
                                top: 474px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_024_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_024_6" runat="server" Style="left: 414px; position: absolute;
                                top: 458px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_024_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_025_1" runat="server" Style="left: 483px; position: absolute;
                                top: 448px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_025_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_025_2" runat="server" Style="left: 496px; position: absolute;
                                top: 456px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_025_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_025_3" runat="server" Style="left: 497px; position: absolute;
                                top: 471px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_025_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_025_4" runat="server" Style="left: 483px; position: absolute;
                                top: 479px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_025_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_025_5" runat="server" Style="left: 469px; position: absolute;
                                top: 471px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_025_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_025_6" runat="server" Style="left: 470px; position: absolute;
                                top: 456px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_025_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_026_1" runat="server" Style="left: 538px; position: absolute;
                                top: 445px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_026_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_026_2" runat="server" Style="left: 552px; position: absolute;
                                top: 452px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_026_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_026_3" runat="server" Style="left: 551px; position: absolute;
                                top: 469px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_026_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_026_4" runat="server" Style="left: 538px; position: absolute;
                                top: 476px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_026_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_026_5" runat="server" Style="left: 524px; position: absolute;
                                top: 468px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_026_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_026_6" runat="server" Style="left: 524px; position: absolute;
                                top: 453px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_026_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_027_1" runat="server" Style="left: 593px; position: absolute;
                                top: 442px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_027_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_027_2" runat="server" Style="left: 607px; position: absolute;
                                top: 449px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_027_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_027_3" runat="server" Style="left: 608px; position: absolute;
                                top: 466px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_027_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_027_4" runat="server" Style="left: 593px; position: absolute;
                                top: 474px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_027_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_027_5" runat="server" Style="left: 579px; position: absolute;
                                top: 466px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_027_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_027_6" runat="server" Style="left: 579px; position: absolute;
                                top: 451px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_027_6_Click" />
                        </div>
                        <spam style="color: White;"> Mapa apenas ilustrativo. As mesas poderão sofrer alterações em sua localização no dia do evento.</spam>
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
                                    <asp:RadioButtonList ID="radTipoCadeira" runat="server" CssClass="radioButton" RepeatDirection="Horizontal">
                                        <asp:ListItem>Inteiro</asp:ListItem>
                                        <asp:ListItem>Meio Adolescente</asp:ListItem>
                                    </asp:RadioButtonList>
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
                                        Text="Regra: Número de ingressos disponíveis<br/>em mesa por cota/título: 6"></asp:Label>
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
                                            <td>
                                                <img alt="" src="../../images/cadeiraLivre.png" 
                                                class="circleborder"/>
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

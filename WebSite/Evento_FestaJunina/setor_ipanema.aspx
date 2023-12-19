<%@ Page Language="C#" AutoEventWireup="true" CodeFile="setor_ipanema.aspx.cs" Inherits="eventos_festaJunina_setor_ipanema" 
    validateRequest="false"%>

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
                        <li><a href="index.aspx">MAPA GERAL DO EVENTO</a></li>
                        <li><a href="setor_salao_de_festas.aspx">SETOR SALÃO DE FESTAS</a></li>
                        <li><a href="setor_golodromo.aspx">SETOR GOLÓDROMO</a></li>
                        <li><a href="setor_pergula.aspx">SETOR PÉRGULA</a></li>
                        <li><a href="setor_portinari.aspx">SETOR PORTINARI</a></li>
                        <li><a href="setor_ipanema.aspx" class="ativo">SETOR IPANEMA</a></li>
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
                            <img class="img-responsive" src="../../images/imgFestaJunina/bg-ipanema.jpg" />
                            <asp:ImageButton ID="ImageButton_mesa_156_1" runat="server" Style="left: 101px; position: absolute;
                                top: 230px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_156_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_156_2" runat="server" Style="left: 115px; position: absolute;
                                top: 238px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_156_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_156_3" runat="server" Style="left: 115px; position: absolute;
                                top: 254px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_156_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_156_4" runat="server" Style="left: 101px; position: absolute;
                                top: 261px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_156_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_156_5" runat="server" Style="left: 87px; position: absolute;
                                top: 254px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_156_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_156_6" runat="server" Style="left: 87px; position: absolute;
                                top: 238px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_156_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_157_1" runat="server" Style="left: 158px; position: absolute;
                                top: 228px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_157_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_157_2" runat="server" Style="left: 171px; position: absolute;
                                top: 235px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_157_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_157_3" runat="server" Style="left: 172px; position: absolute;
                                top: 251px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_157_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_157_4" runat="server" Style="left: 158px; position: absolute;
                                top: 258px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_157_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_157_5" runat="server" Style="left: 144px; position: absolute;
                                top: 251px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_157_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_157_6" runat="server" Style="left: 144px; position: absolute;
                                top: 235px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_157_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_158_1" runat="server" Style="left: 214px; position: absolute;
                                top: 224px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_158_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_158_2" runat="server" Style="left: 228px; position: absolute;
                                top: 232px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_158_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_158_3" runat="server" Style="left: 228px; position: absolute;
                                top: 248px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_158_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_158_4" runat="server" Style="left: 214px; position: absolute;
                                top: 255px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_158_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_158_5" runat="server" Style="left: 201px; position: absolute;
                                top: 248px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_158_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_158_6" runat="server" Style="left: 200px; position: absolute;
                                top: 232px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_158_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_159_1" runat="server" Style="left: 271px; position: absolute;
                                top: 221px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_159_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_159_2" runat="server" Style="left: 285px; position: absolute;
                                top: 229px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_159_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_159_3" runat="server" Style="left: 285px; position: absolute;
                                top: 245px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_159_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_159_4" runat="server" Style="left: 271px; position: absolute;
                                top: 253px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_159_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_159_5" runat="server" Style="left: 257px; position: absolute;
                                top: 245px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_159_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_159_6" runat="server" Style="left: 257px; position: absolute;
                                top: 229px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_159_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_160_1" runat="server" Style="left: 328px; position: absolute;
                                top: 218px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_160_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_160_2" runat="server" Style="left: 341px; position: absolute;
                                top: 225px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_160_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_160_3" runat="server" Style="left: 342px; position: absolute;
                                top: 242px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_160_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_160_4" runat="server" Style="left: 328px; position: absolute;
                                top: 249px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_160_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_160_5" runat="server" Style="left: 314px; position: absolute;
                                top: 241px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_160_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_160_6" runat="server" Style="left: 313px; position: absolute;
                                top: 226px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_160_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_161_1" runat="server" Style="left: 384px; position: absolute;
                                top: 215px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_161_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_161_2" runat="server" Style="left: 398px; position: absolute;
                                top: 223px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_161_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_161_3" runat="server" Style="left: 398px; position: absolute;
                                top: 239px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_161_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_161_4" runat="server" Style="left: 384px; position: absolute;
                                top: 246px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_161_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_161_5" runat="server" Style="left: 370px; position: absolute;
                                top: 239px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_161_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_161_6" runat="server" Style="left: 370px; position: absolute;
                                top: 223px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_161_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_162_1" runat="server" Style="left: 441px; position: absolute;
                                top: 212px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_162_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_162_2" runat="server" Style="left: 455px; position: absolute;
                                top: 220px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_162_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_162_3" runat="server" Style="left: 455px; position: absolute;
                                top: 235px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_162_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_162_4" runat="server" Style="left: 441px; position: absolute;
                                top: 243px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_162_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_162_5" runat="server" Style="left: 427px; position: absolute;
                                top: 236px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_162_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_162_6" runat="server" Style="left: 427px; position: absolute;
                                top: 220px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_162_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_163_1" runat="server" Style="left: 497px; position: absolute;
                                top: 209px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_163_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_163_2" runat="server" Style="left: 511px; position: absolute;
                                top: 216px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_163_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_163_3" runat="server" Style="left: 512px; position: absolute;
                                top: 233px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_163_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_163_4" runat="server" Style="left: 498px; position: absolute;
                                top: 240px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_163_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_163_5" runat="server" Style="left: 484px; position: absolute;
                                top: 233px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_163_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_163_6" runat="server" Style="left: 483px; position: absolute;
                                top: 217px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_163_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_164_1" runat="server" Style="left: 554px; position: absolute;
                                top: 206px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_164_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_164_2" runat="server" Style="left: 568px; position: absolute;
                                top: 214px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_164_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_164_3" runat="server" Style="left: 568px; position: absolute;
                                top: 230px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_164_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_164_4" runat="server" Style="left: 554px; position: absolute;
                                top: 237px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_164_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_164_5" runat="server" Style="left: 540px; position: absolute;
                                top: 230px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_164_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_164_6" runat="server" Style="left: 540px; position: absolute;
                                top: 214px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_164_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_165_1" runat="server" Style="left: 611px; position: absolute;
                                top: 203px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_165_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_165_2" runat="server" Style="left: 625px; position: absolute;
                                top: 210px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_165_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_165_3" runat="server" Style="left: 625px; position: absolute;
                                top: 227px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_165_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_165_4" runat="server" Style="left: 611px; position: absolute;
                                top: 234px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_165_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_165_5" runat="server" Style="left: 597px; position: absolute;
                                top: 227px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_165_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_165_6" runat="server" Style="left: 596px; position: absolute;
                                top: 211px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_165_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_166_1" runat="server" Style="left: 667px; position: absolute;
                                top: 200px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_166_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_166_2" runat="server" Style="left: 681px; position: absolute;
                                top: 207px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_166_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_166_3" runat="server" Style="left: 682px; position: absolute;
                                top: 224px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_166_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_166_4" runat="server" Style="left: 667px; position: absolute;
                                top: 231px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_166_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_166_5" runat="server" Style="left: 653px; position: absolute;
                                top: 224px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_166_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_166_6" runat="server" Style="left: 653px; position: absolute;
                                top: 208px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_166_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_167_1" runat="server" Style="left: 724px; position: absolute;
                                top: 197px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_167_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_167_2" runat="server" Style="left: 738px; position: absolute;
                                top: 204px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_167_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_167_3" runat="server" Style="left: 738px; position: absolute;
                                top: 221px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_167_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_167_4" runat="server" Style="left: 725px; position: absolute;
                                top: 228px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_167_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_167_5" runat="server" Style="left: 710px; position: absolute;
                                top: 221px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_167_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_167_6" runat="server" Style="left: 710px; position: absolute;
                                top: 205px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_167_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_168_1" runat="server" Style="left: 781px; position: absolute;
                                top: 194px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_168_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_168_2" runat="server" Style="left: 795px; position: absolute;
                                top: 201px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_168_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_168_3" runat="server" Style="left: 795px; position: absolute;
                                top: 218px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_168_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_168_4" runat="server" Style="left: 781px; position: absolute;
                                top: 225px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_168_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_168_5" runat="server" Style="left: 767px; position: absolute;
                                top: 218px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_168_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_168_6" runat="server" Style="left: 766px; position: absolute;
                                top: 202px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_168_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_169_1" runat="server" Style="left: 837px; position: absolute;
                                top: 191px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_169_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_169_2" runat="server" Style="left: 851px; position: absolute;
                                top: 198px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_169_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_169_3" runat="server" Style="left: 852px; position: absolute;
                                top: 215px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_169_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_169_4" runat="server" Style="left: 838px; position: absolute;
                                top: 222px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_169_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_169_5" runat="server" Style="left: 823px; position: absolute;
                                top: 215px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_169_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_169_6" runat="server" Style="left: 823px; position: absolute;
                                top: 199px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_169_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_170_1" runat="server" Style="left: 52px; position: absolute;
                                top: 284px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_170_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_170_2" runat="server" Style="left: 66px; position: absolute;
                                top: 292px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_170_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_170_3" runat="server" Style="left: 67px; position: absolute;
                                top: 308px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_170_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_170_4" runat="server" Style="left: 52px; position: absolute;
                                top: 315px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_170_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_170_5" runat="server" Style="left: 38px; position: absolute;
                                top: 308px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_170_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_170_6" runat="server" Style="left: 38px; position: absolute;
                                top: 292px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_170_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_171_1" runat="server" Style="left: 101px; position: absolute;
                                top: 282px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_171_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_171_2" runat="server" Style="left: 115px; position: absolute;
                                top: 290px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_171_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_171_3" runat="server" Style="left: 115px; position: absolute;
                                top: 305px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_171_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_171_4" runat="server" Style="left: 101px; position: absolute;
                                top: 313px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_171_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_171_5" runat="server" Style="left: 87px; position: absolute;
                                top: 305px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_171_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_171_6" runat="server" Style="left: 87px; position: absolute;
                                top: 290px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_171_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_172_1" runat="server" Style="left: 158px; position: absolute;
                                top: 279px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_172_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_172_2" runat="server" Style="left: 171px; position: absolute;
                                top: 287px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_172_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_172_3" runat="server" Style="left: 172px; position: absolute;
                                top: 303px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_172_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_172_4" runat="server" Style="left: 158px; position: absolute;
                                top: 309px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_172_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_172_5" runat="server" Style="left: 144px; position: absolute;
                                top: 302px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_172_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_172_6" runat="server" Style="left: 144px; position: absolute;
                                top: 287px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_172_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_173_1" runat="server" Style="left: 214px; position: absolute;
                                top: 276px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_173_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_173_2" runat="server" Style="left: 228px; position: absolute;
                                top: 283px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_173_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_173_3" runat="server" Style="left: 228px; position: absolute;
                                top: 299px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_173_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_173_4" runat="server" Style="left: 214px; position: absolute;
                                top: 306px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_173_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_173_5" runat="server" Style="left: 201px; position: absolute;
                                top: 299px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_173_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_173_6" runat="server" Style="left: 200px; position: absolute;
                                top: 284px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_173_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_174_1" runat="server" Style="left: 271px; position: absolute;
                                top: 273px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_174_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_174_2" runat="server" Style="left: 285px; position: absolute;
                                top: 280px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_174_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_174_3" runat="server" Style="left: 285px; position: absolute;
                                top: 296px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_174_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_174_4" runat="server" Style="left: 271px; position: absolute;
                                top: 304px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_174_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_174_5" runat="server" Style="left: 257px; position: absolute;
                                top: 296px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_174_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_174_6" runat="server" Style="left: 257px; position: absolute;
                                top: 281px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_174_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_175_1" runat="server" Style="left: 328px; position: absolute;
                                top: 270px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_175_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_175_2" runat="server" Style="left: 341px; position: absolute;
                                top: 278px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_175_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_175_3" runat="server" Style="left: 342px; position: absolute;
                                top: 293px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_175_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_175_4" runat="server" Style="left: 328px; position: absolute;
                                top: 300px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_175_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_175_5" runat="server" Style="left: 314px; position: absolute;
                                top: 293px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_175_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_175_6" runat="server" Style="left: 313px; position: absolute;
                                top: 278px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_175_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_176_1" runat="server" Style="left: 384px; position: absolute;
                                top: 267px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_176_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_176_2" runat="server" Style="left: 398px; position: absolute;
                                top: 274px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_176_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_176_3" runat="server" Style="left: 398px; position: absolute;
                                top: 290px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_176_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_176_4" runat="server" Style="left: 384px; position: absolute;
                                top: 297px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_176_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_176_5" runat="server" Style="left: 370px; position: absolute;
                                top: 290px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_176_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_176_6" runat="server" Style="left: 370px; position: absolute;
                                top: 274px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_176_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_177_1" runat="server" Style="left: 441px; position: absolute;
                                top: 264px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_177_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_177_2" runat="server" Style="left: 455px; position: absolute;
                                top: 271px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_177_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_177_3" runat="server" Style="left: 455px; position: absolute;
                                top: 287px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_177_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_177_4" runat="server" Style="left: 441px; position: absolute;
                                top: 294px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_177_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_177_5" runat="server" Style="left: 427px; position: absolute;
                                top: 287px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_177_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_177_6" runat="server" Style="left: 427px; position: absolute;
                                top: 272px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_177_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_178_1" runat="server" Style="left: 497px; position: absolute;
                                top: 261px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_178_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_178_2" runat="server" Style="left: 511px; position: absolute;
                                top: 268px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_178_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_178_3" runat="server" Style="left: 512px; position: absolute;
                                top: 285px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_178_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_178_4" runat="server" Style="left: 498px; position: absolute;
                                top: 291px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_178_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_178_5" runat="server" Style="left: 484px; position: absolute;
                                top: 284px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_178_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_178_6" runat="server" Style="left: 483px; position: absolute;
                                top: 268px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_178_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_179_1" runat="server" Style="left: 554px; position: absolute;
                                top: 258px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_179_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_179_2" runat="server" Style="left: 568px; position: absolute;
                                top: 265px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_179_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_179_3" runat="server" Style="left: 568px; position: absolute;
                                top: 281px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_179_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_179_4" runat="server" Style="left: 554px; position: absolute;
                                top: 288px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_179_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_179_5" runat="server" Style="left: 540px; position: absolute;
                                top: 281px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_179_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_179_6" runat="server" Style="left: 540px; position: absolute;
                                top: 265px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_179_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_180_1" runat="server" Style="left: 611px; position: absolute;
                                top: 255px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_180_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_180_2" runat="server" Style="left: 625px; position: absolute;
                                top: 262px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_180_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_180_3" runat="server" Style="left: 625px; position: absolute;
                                top: 279px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_180_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_180_4" runat="server" Style="left: 611px; position: absolute;
                                top: 286px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_180_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_180_5" runat="server" Style="left: 597px; position: absolute;
                                top: 278px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_180_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_180_6" runat="server" Style="left: 596px; position: absolute;
                                top: 262px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_180_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_181_1" runat="server" Style="left: 667px; position: absolute;
                                top: 251px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_181_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_181_2" runat="server" Style="left: 681px; position: absolute;
                                top: 259px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_181_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_181_3" runat="server" Style="left: 682px; position: absolute;
                                top: 275px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_181_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_181_4" runat="server" Style="left: 667px; position: absolute;
                                top: 282px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_181_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_181_5" runat="server" Style="left: 653px; position: absolute;
                                top: 275px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_181_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_181_6" runat="server" Style="left: 653px; position: absolute;
                                top: 260px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_181_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_182_1" runat="server" Style="left: 724px; position: absolute;
                                top: 249px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_182_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_182_2" runat="server" Style="left: 738px; position: absolute;
                                top: 256px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_182_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_182_3" runat="server" Style="left: 738px; position: absolute;
                                top: 272px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_182_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_182_4" runat="server" Style="left: 725px; position: absolute;
                                top: 280px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_182_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_182_5" runat="server" Style="left: 710px; position: absolute;
                                top: 272px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_182_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_182_6" runat="server" Style="left: 710px; position: absolute;
                                top: 257px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_182_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_183_1" runat="server" Style="left: 781px; position: absolute;
                                top: 245px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_183_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_183_2" runat="server" Style="left: 795px; position: absolute;
                                top: 253px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_183_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_183_3" runat="server" Style="left: 795px; position: absolute;
                                top: 269px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_183_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_183_4" runat="server" Style="left: 781px; position: absolute;
                                top: 275px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_183_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_183_5" runat="server" Style="left: 767px; position: absolute;
                                top: 269px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_183_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_183_6" runat="server" Style="left: 766px; position: absolute;
                                top: 254px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_183_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_184_1" runat="server" Style="left: 837px; position: absolute;
                                top: 243px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_184_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_184_2" runat="server" Style="left: 851px; position: absolute;
                                top: 250px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_184_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_184_3" runat="server" Style="left: 852px; position: absolute;
                                top: 267px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_184_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_184_4" runat="server" Style="left: 838px; position: absolute;
                                top: 274px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_184_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_184_5" runat="server" Style="left: 823px; position: absolute;
                                top: 266px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_184_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_184_6" runat="server" Style="left: 823px; position: absolute;
                                top: 251px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_184_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_185_1" runat="server" Style="left: 893px; position: absolute;
                                top: 242px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_185_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_185_2" runat="server" Style="left: 907px; position: absolute;
                                top: 248px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_185_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_185_3" runat="server" Style="left: 907px; position: absolute;
                                top: 265px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_185_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_185_4" runat="server" Style="left: 893px; position: absolute;
                                top: 272px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_185_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_185_5" runat="server" Style="left: 879px; position: absolute;
                                top: 265px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_185_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_185_6" runat="server" Style="left: 879px; position: absolute;
                                top: 249px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_185_6_Click" />
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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="setor_portinari.aspx.cs"
    Inherits="eventos_festaJunina_setor_portinari" validateRequest="false"%>

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
                        <li><a href="setor_portinari.aspx" class="ativo">SETOR PORTINARI</a></li>
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
                            <img class="img-responsive" src="../../images/imgFestaJunina/bg-portinari.jpg" />
                            <asp:ImageButton ID="ImageButton_mesa_132_1" runat="server" Style="left: 143px; position: absolute;
                                top: 339px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_132_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_132_2" runat="server" Style="left: 156px; position: absolute;
                                top: 346px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_132_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_132_3" runat="server" Style="left: 156px; position: absolute;
                                top: 363px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_132_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_132_4" runat="server" Style="left: 142px; position: absolute;
                                top: 371px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_132_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_132_5" runat="server" Style="left: 128px; position: absolute;
                                top: 363px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_132_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_132_6" runat="server" Style="left: 128px; position: absolute;
                                top: 349px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_132_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_133_1" runat="server" Style="left: 200px; position: absolute;
                                top: 336px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_133_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_133_2" runat="server" Style="left: 212px; position: absolute;
                                top: 346px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_133_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_133_3" runat="server" Style="left: 214px; position: absolute;
                                top: 361px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_133_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_133_4" runat="server" Style="left: 200px; position: absolute;
                                top: 369px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_133_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_133_5" runat="server" Style="left: 185px; position: absolute;
                                top: 360px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_133_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_133_6" runat="server" Style="left: 185px; position: absolute;
                                top: 345px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_133_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_134_1" runat="server" Style="left: 256px; position: absolute;
                                top: 336px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_134_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_134_2" runat="server" Style="left: 268px; position: absolute;
                                top: 342px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_134_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_134_3" runat="server" Style="left: 271px; position: absolute;
                                top: 358px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_134_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_134_4" runat="server" Style="left: 256px; position: absolute;
                                top: 366px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_134_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_134_5" runat="server" Style="left: 242px; position: absolute;
                                top: 357px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_134_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_134_6" runat="server" Style="left: 243px; position: absolute;
                                top: 343px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_134_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_135_1" runat="server" Style="left: 313px; position: absolute;
                                top: 332px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_135_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_135_2" runat="server" Style="left: 326px; position: absolute;
                                top: 339px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_135_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_135_3" runat="server" Style="left: 328px; position: absolute;
                                top: 355px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_135_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_135_4" runat="server" Style="left: 313px; position: absolute;
                                top: 362px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_135_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_135_5" runat="server" Style="left: 299px; position: absolute;
                                top: 354px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_135_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_135_6" runat="server" Style="left: 299px; position: absolute;
                                top: 339px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_135_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_136_1" runat="server" Style="left: 370px; position: absolute;
                                top: 330px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_136_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_136_2" runat="server" Style="left: 384px; position: absolute;
                                top: 335px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_136_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_136_3" runat="server" Style="left: 386px; position: absolute;
                                top: 354px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_136_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_136_4" runat="server" Style="left: 371px; position: absolute;
                                top: 360px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_136_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_136_5" runat="server" Style="left: 356px; position: absolute;
                                top: 354px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_136_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_136_6" runat="server" Style="left: 356px; position: absolute;
                                top: 337px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_136_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_137_1" runat="server" Style="left: 427px; position: absolute;
                                top: 326px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_137_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_137_2" runat="server" Style="left: 440px; position: absolute;
                                top: 335px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_137_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_137_3" runat="server" Style="left: 441px; position: absolute;
                                top: 349px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_137_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_137_4" runat="server" Style="left: 428px; position: absolute;
                                top: 357px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_137_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_137_5" runat="server" Style="left: 414px; position: absolute;
                                top: 349px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_137_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_137_6" runat="server" Style="left: 414px; position: absolute;
                                top: 335px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_137_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_138_1" runat="server" Style="left: 485px; position: absolute;
                                top: 326px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_138_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_138_2" runat="server" Style="left: 497px; position: absolute;
                                top: 332px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_138_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_138_3" runat="server" Style="left: 500px; position: absolute;
                                top: 347px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_138_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_138_4" runat="server" Style="left: 485px; position: absolute;
                                top: 357px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_138_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_138_5" runat="server" Style="left: 471px; position: absolute;
                                top: 349px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_138_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_138_6" runat="server" Style="left: 470px; position: absolute;
                                top: 331px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_138_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_139_1" runat="server" Style="left: 541px; position: absolute;
                                top: 321px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_139_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_139_2" runat="server" Style="left: 556px; position: absolute;
                                top: 328px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_139_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_139_3" runat="server" Style="left: 557px; position: absolute;
                                top: 345px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_139_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_139_4" runat="server" Style="left: 542px; position: absolute;
                                top: 353px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_139_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_139_5" runat="server" Style="left: 528px; position: absolute;
                                top: 344px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_139_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_139_6" runat="server" Style="left: 528px; position: absolute;
                                top: 330px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_139_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_140_1" runat="server" Style="left: 143px; position: absolute;
                                top: 404px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_140_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_140_2" runat="server" Style="left: 156px; position: absolute;
                                top: 412px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_140_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_140_3" runat="server" Style="left: 156px; position: absolute;
                                top: 428px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_140_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_140_4" runat="server" Style="left: 142px; position: absolute;
                                top: 436px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_140_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_140_5" runat="server" Style="left: 128px; position: absolute;
                                top: 428px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_140_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_140_6" runat="server" Style="left: 128px; position: absolute;
                                top: 413px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_140_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_141_1" runat="server" Style="left: 200px; position: absolute;
                                top: 402px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_141_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_141_2" runat="server" Style="left: 212px; position: absolute;
                                top: 411px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_141_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_141_3" runat="server" Style="left: 214px; position: absolute;
                                top: 427px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_141_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_141_4" runat="server" Style="left: 200px; position: absolute;
                                top: 433px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_141_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_141_5" runat="server" Style="left: 185px; position: absolute;
                                top: 426px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_141_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_141_6" runat="server" Style="left: 185px; position: absolute;
                                top: 411px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_141_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_142_1" runat="server" Style="left: 256px; position: absolute;
                                top: 399px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_142_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_142_2" runat="server" Style="left: 271px; position: absolute;
                                top: 406px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_142_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_142_3" runat="server" Style="left: 271px; position: absolute;
                                top: 424px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_142_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_142_4" runat="server" Style="left: 256px; position: absolute;
                                top: 431px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_142_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_142_5" runat="server" Style="left: 242px; position: absolute;
                                top: 423px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_142_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_142_6" runat="server" Style="left: 243px; position: absolute;
                                top: 408px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_142_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_143_1" runat="server" Style="left: 313px; position: absolute;
                                top: 397px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_143_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_143_2" runat="server" Style="left: 326px; position: absolute;
                                top: 406px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_143_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_143_3" runat="server" Style="left: 328px; position: absolute;
                                top: 420px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_143_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_143_4" runat="server" Style="left: 313px; position: absolute;
                                top: 428px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_143_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_143_5" runat="server" Style="left: 299px; position: absolute;
                                top: 420px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_143_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_143_6" runat="server" Style="left: 299px; position: absolute;
                                top: 405px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_143_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_144_1" runat="server" Style="left: 370px; position: absolute;
                                top: 394px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_144_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_144_2" runat="server" Style="left: 384px; position: absolute;
                                top: 402px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_144_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_144_3" runat="server" Style="left: 386px; position: absolute;
                                top: 418px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_144_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_144_4" runat="server" Style="left: 371px; position: absolute;
                                top: 425px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_144_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_144_5" runat="server" Style="left: 356px; position: absolute;
                                top: 418px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_144_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_144_6" runat="server" Style="left: 356px; position: absolute;
                                top: 402px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_144_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_145_1" runat="server" Style="left: 427px; position: absolute;
                                top: 392px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_145_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_145_2" runat="server" Style="left: 440px; position: absolute;
                                top: 400px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_145_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_145_3" runat="server" Style="left: 441px; position: absolute;
                                top: 414px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_145_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_145_4" runat="server" Style="left: 428px; position: absolute;
                                top: 424px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_145_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_145_5" runat="server" Style="left: 414px; position: absolute;
                                top: 414px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_145_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_145_6" runat="server" Style="left: 414px; position: absolute;
                                top: 400px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_145_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_146_1" runat="server" Style="left: 485px; position: absolute;
                                top: 389px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_146_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_146_2" runat="server" Style="left: 497px; position: absolute;
                                top: 398px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_146_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_146_3" runat="server" Style="left: 500px; position: absolute;
                                top: 413px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_146_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_146_4" runat="server" Style="left: 485px; position: absolute;
                                top: 420px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_146_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_146_5" runat="server" Style="left: 471px; position: absolute;
                                top: 412px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_146_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_146_6" runat="server" Style="left: 470px; position: absolute;
                                top: 396px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_146_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_147_1" runat="server" Style="left: 541px; position: absolute;
                                top: 386px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_147_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_147_2" runat="server" Style="left: 556px; position: absolute;
                                top: 394px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_147_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_147_3" runat="server" Style="left: 557px; position: absolute;
                                top: 410px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_147_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_147_4" runat="server" Style="left: 542px; position: absolute;
                                top: 417px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_147_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_147_5" runat="server" Style="left: 528px; position: absolute;
                                top: 410px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_147_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_147_6" runat="server" Style="left: 528px; position: absolute;
                                top: 394px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_147_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_148_1" runat="server" Style="left: 111px; position: absolute;
                                top: 472px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_148_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_148_2" runat="server" Style="left: 124px; position: absolute;
                                top: 478px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_148_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_148_3" runat="server" Style="left: 124px; position: absolute;
                                top: 494px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_148_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_148_4" runat="server" Style="left: 111px; position: absolute;
                                top: 501px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_148_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_148_5" runat="server" Style="left: 96px; position: absolute;
                                top: 495px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_148_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_148_6" runat="server" Style="left: 95px; position: absolute;
                                top: 478px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_148_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_149_1" runat="server" Style="left: 175px; position: absolute;
                                top: 469px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_149_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_149_2" runat="server" Style="left: 188px; position: absolute;
                                top: 475px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_149_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_149_3" runat="server" Style="left: 187px; position: absolute;
                                top: 492px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_149_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_149_4" runat="server" Style="left: 174px; position: absolute;
                                top: 500px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_149_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_149_5" runat="server" Style="left: 158px; position: absolute;
                                top: 494px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_149_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_149_6" runat="server" Style="left: 159px; position: absolute;
                                top: 477px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_149_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_150_1" runat="server" Style="left: 230px; position: absolute;
                                top: 466px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_150_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_150_2" runat="server" Style="left: 242px; position: absolute;
                                top: 473px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_150_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_150_3" runat="server" Style="left: 246px; position: absolute;
                                top: 490px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_150_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_150_4" runat="server" Style="left: 232px; position: absolute;
                                top: 499px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_150_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_150_5" runat="server" Style="left: 219px; position: absolute;
                                top: 494px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_150_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_150_6" runat="server" Style="left: 217px; position: absolute;
                                top: 473px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_150_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_151_1" runat="server" Style="left: 288px; position: absolute;
                                top: 464px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_151_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_151_2" runat="server" Style="left: 302px; position: absolute;
                                top: 471px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_151_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_151_3" runat="server" Style="left: 302px; position: absolute;
                                top: 489px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_151_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_151_4" runat="server" Style="left: 288px; position: absolute;
                                top: 496px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_151_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_151_5" runat="server" Style="left: 273px; position: absolute;
                                top: 489px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_151_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_151_6" runat="server" Style="left: 273px; position: absolute;
                                top: 472px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_151_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_152_1" runat="server" Style="left: 345px; position: absolute;
                                top: 461px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_152_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_152_2" runat="server" Style="left: 360px; position: absolute;
                                top: 469px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_152_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_152_3" runat="server" Style="left: 360px; position: absolute;
                                top: 486px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_152_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_152_4" runat="server" Style="left: 345px; position: absolute;
                                top: 493px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_152_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_152_5" runat="server" Style="left: 331px; position: absolute;
                                top: 486px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_152_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_152_6" runat="server" Style="left: 331px; position: absolute;
                                top: 468px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_152_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_153_1" runat="server" Style="left: 402px; position: absolute;
                                top: 459px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_153_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_153_2" runat="server" Style="left: 417px; position: absolute;
                                top: 465px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_153_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_153_3" runat="server" Style="left: 418px; position: absolute;
                                top: 483px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_153_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_153_4" runat="server" Style="left: 402px; position: absolute;
                                top: 490px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_153_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_153_5" runat="server" Style="left: 387px; position: absolute;
                                top: 484px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_153_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_153_6" runat="server" Style="left: 387px; position: absolute;
                                top: 466px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_153_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_154_1" runat="server" Style="left: 460px; position: absolute;
                                top: 455px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_154_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_154_2" runat="server" Style="left: 474px; position: absolute;
                                top: 463px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_154_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_154_3" runat="server" Style="left: 474px; position: absolute;
                                top: 482px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_154_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_154_4" runat="server" Style="left: 460px; position: absolute;
                                top: 488px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_154_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_154_5" runat="server" Style="left: 446px; position: absolute;
                                top: 482px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_154_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_154_6" runat="server" Style="left: 445px; position: absolute;
                                top: 463px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_154_6_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_155_1" runat="server" Style="left: 520px; position: absolute;
                                top: 453px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_155_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_155_2" runat="server" Style="left: 534px; position: absolute;
                                top: 460px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_155_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_155_3" runat="server" Style="left: 534px; position: absolute;
                                top: 479px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_155_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_155_4" runat="server" Style="left: 520px; position: absolute;
                                top: 485px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_155_4_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_155_5" runat="server" Style="left: 506px; position: absolute;
                                top: 479px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_155_5_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_155_6" runat="server" Style="left: 505px; position: absolute;
                                top: 461px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_155_6_Click" />
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

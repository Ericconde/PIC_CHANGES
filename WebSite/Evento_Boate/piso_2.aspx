<%@ Page Language="C#" AutoEventWireup="true" CodeFile="piso_2.aspx.cs"
    Inherits="eventos_Boate_piso_2" validateRequest="false"%>

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
                        <li><a href="piso_1.aspx">1º PISO</a></li>
                        <li><a href="piso_2.aspx" class="ativo">2º PISO</a></li>
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
                            <img class="img-responsive" src="../../images/imgBoate/piso_2.jpg" />

                            <asp:ImageButton ID="ImageButton_mesa_030_1" runat="server" Style="left: 295px; position: absolute;
                            top: 224px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_030_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_030_2" runat="server" Style="left: 295px; position: absolute;
                            top: 237px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_030_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_030_3" runat="server" Style="left: 323px; position: absolute;
                            top: 223px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_030_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_030_4" runat="server" Style="left: 323px; position: absolute;
                            top: 236px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_030_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_031_1" runat="server" Style="left: 347px; position: absolute;
                            top: 224px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_031_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_031_2" runat="server" Style="left: 347px; position: absolute;
                            top: 237px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_031_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_031_3" runat="server" Style="left: 374px; position: absolute;
                            top: 224px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_031_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_031_4" runat="server" Style="left: 374px; position: absolute;
                            top: 236px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_031_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_032_1" runat="server" Style="left: 398px; position: absolute;
                            top: 224px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_032_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_032_2" runat="server" Style="left: 398px; position: absolute;
                            top: 237px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_032_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_032_3" runat="server" Style="left: 426px; position: absolute;
                            top: 224px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_032_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_032_4" runat="server" Style="left: 426px; position: absolute;
                            top: 236px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_032_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_033_1" runat="server" Style="left: 449px; position: absolute;
                            top: 224px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_033_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_033_2" runat="server" Style="left: 449px; position: absolute;
                            top: 236px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_033_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_033_3" runat="server" Style="left: 478px; position: absolute;
                            top: 224px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_033_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_033_4" runat="server" Style="left: 478px; position: absolute;
                            top: 236px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_033_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_034_1" runat="server" Style="left: 250px; position: absolute;
                            top: 323px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_034_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_034_2" runat="server" Style="left: 247px; position: absolute;
                            top: 310px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_034_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_034_3" runat="server" Style="left: 278px; position: absolute;
                            top: 316px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_034_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_034_4" runat="server" Style="left: 275px; position: absolute;
                            top: 303px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_034_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_035_1" runat="server" Style="left: 295px; position: absolute;
                            top: 299px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_035_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_035_2" runat="server" Style="left: 298px; position: absolute;
                            top: 311px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_035_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_035_3" runat="server" Style="left: 321px; position: absolute;
                            top: 292px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_035_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_035_4" runat="server" Style="left: 325px; position: absolute;
                            top: 305px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_035_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_036_1" runat="server" Style="left: 342px; position: absolute;
                            top: 289px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_036_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_036_2" runat="server" Style="left: 345px; position: absolute;
                            top: 301px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_036_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_036_3" runat="server" Style="left: 369px; position: absolute;
                            top: 282px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_036_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_036_4" runat="server" Style="left: 373px; position: absolute;
                            top: 295px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_036_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_037_1" runat="server" Style="left: 389px; position: absolute;
                            top: 277px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_037_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_037_2" runat="server" Style="left: 392px; position: absolute;
                            top: 290px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_037_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_037_3" runat="server" Style="left: 415px; position: absolute;
                            top: 271px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_037_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_037_4" runat="server" Style="left: 418px; position: absolute;
                            top: 283px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_037_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_038_1" runat="server" Style="left: 436px; position: absolute;
                            top: 267px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_038_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_038_2" runat="server" Style="left: 438px; position: absolute;
                            top: 280px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_038_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_038_3" runat="server" Style="left: 463px; position: absolute;
                            top: 260px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_038_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_038_4" runat="server" Style="left: 466px; position: absolute;
                            top: 273px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_038_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_039_1" runat="server" Style="left: 696px; position: absolute;
                            top: 224px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_039_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_039_2" runat="server" Style="left: 708px; position: absolute;
                            top: 224px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_039_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_039_3" runat="server" Style="left: 696px; position: absolute;
                            top: 250px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_039_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_039_4" runat="server" Style="left: 708px; position: absolute;
                            top: 250px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_039_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_040_1" runat="server" Style="left: 733px; position: absolute;
                            top: 224px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_040_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_040_2" runat="server" Style="left: 745px; position: absolute;
                            top: 224px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_040_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_040_3" runat="server" Style="left: 733px; position: absolute;
                            top: 250px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_040_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_040_4" runat="server" Style="left: 745px; position: absolute;
                            top: 250px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_040_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_041_1" runat="server" Style="left: 731px; position: absolute;
                            top: 277px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_041_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_041_2" runat="server" Style="left: 743px; position: absolute;
                            top: 277px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_041_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_041_3" runat="server" Style="left: 731px; position: absolute;
                            top: 303px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_041_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_041_4" runat="server" Style="left: 743px; position: absolute;
                            top: 303px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_041_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_042_1" runat="server" Style="left: 731px; position: absolute;
                            top: 330px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_042_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_042_2" runat="server" Style="left: 743px; position: absolute;
                            top: 330px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_042_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_042_3" runat="server" Style="left: 731px; position: absolute;
                            top: 356px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_042_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_042_4" runat="server" Style="left: 743px; position: absolute;
                            top: 356px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_042_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_043_1" runat="server" Style="left: 703px; position: absolute;
                            top: 392px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_043_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_043_2" runat="server" Style="left: 712px; position: absolute;
                            top: 383px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_043_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_043_3" runat="server" Style="left: 723px; position: absolute;
                            top: 410px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_043_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_043_4" runat="server" Style="left: 732px; position: absolute;
                            top: 401px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_043_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_044_1" runat="server" Style="left: 769px; position: absolute;
                            top: 224px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_044_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_044_2" runat="server" Style="left: 782px; position: absolute;
                            top: 224px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_044_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_044_3" runat="server" Style="left: 769px; position: absolute;
                            top: 250px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_044_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_044_4" runat="server" Style="left: 782px; position: absolute;
                            top: 250px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_044_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_045_1" runat="server" Style="left: 769px; position: absolute;
                            top: 277px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_045_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_045_2" runat="server" Style="left: 782px; position: absolute;
                            top: 277px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_045_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_045_3" runat="server" Style="left: 769px; position: absolute;
                            top: 303px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_045_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_045_4" runat="server" Style="left: 782px; position: absolute;
                            top: 303px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_045_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_046_1" runat="server" Style="left: 769px; position: absolute;
                            top: 329px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_046_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_046_2" runat="server" Style="left: 782px; position: absolute;
                            top: 329px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_046_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_046_3" runat="server" Style="left: 769px; position: absolute;
                            top: 355px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_046_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_046_4" runat="server" Style="left: 782px; position: absolute;
                            top: 355px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_046_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_047_1" runat="server" Style="left: 769px; position: absolute;
                            top: 382px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_047_1_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_047_2" runat="server" Style="left: 782px; position: absolute;
                            top: 382px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_047_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_047_3" runat="server" Style="left: 769px; position: absolute;
                            top: 407px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_047_3_Click"/>
                            <asp:ImageButton ID="ImageButton_mesa_047_4" runat="server" Style="left: 782px; position: absolute;
                            top: 407px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_047_4_Click" />

                         </div>
                        <spam style="color: White; padding-left: 15px;"> Mapa apenas ilustrativo. As mesas poderão sofrer alterações em sua localização no dia do evento.</spam>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                     <td valign="top" align="left">
                        <table class="nav-justified">
                            <tr style="height: 20px;">
                                <td align="center"  >
                                    <asp:Label  ID="lblOrientacao" runat="server" CssClass="label_ingresso_vermelho"  Text="Primeiro escolha o tipo de ingresso que queira comprar e em seguida clique na cadeira desejada"></asp:Label>
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

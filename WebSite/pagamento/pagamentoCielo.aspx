<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pagamentoCielo.aspx.cs" Inherits="pagamento_pagamentoCielo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Pagamento Ingressos</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <script src="../scripts/mascara.js" type="text/javascript"></script>
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,700' rel='stylesheet'
        type='text/css'>
    <script src="/Scripts/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="/Scripts/functions.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.maskedinput-1.2.2.js" type="text/javascript"></script>
    <link href="/Styles/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body, html {
            background: #FFF;
        }

        .ul-TipoPagamento {
            margin: 0px;
            padding: 0px;
        }

            .ul-TipoPagamento li {
                list-style: none;
                display: inline-block;
                width: 100px;
                text-align: center;
            }

        div.informacoes {
            height: auto;
            width: 464px;
            text-align: left;
        }

            div.informacoes div.label * {
                font-size: 20px !important;
                font-weight: bold;
            }

        div.label span.block {
            width: auto;
            text-align: left;
        }

        div.spaceH {
            height: 5px;
        }

        .uppercase {
            text-transform: uppercase;
        }
    </style>
    <script type="text/javascript">
        function ProcessarVenda(url) {
            parent.window.location.href = url;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scm">
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="upPagamento">
            <ContentTemplate>
                <div style="height: 470px; padding: 10px;">
                    <h1 class="tlt">Pagamento de Ingressos</h1>
                    <div class="informacoes">
                        <img src="../images/cielo.png" />
                        <br />
                        <div style="font-size:16px !important; font-weight:bold; padding-top:15px;">
                            <span class="block">Produto:</span> <span class="field" >
                                <asp:Label runat="server" ID="lblProduto" ></asp:Label></span>
                        </div>
                        <div style="font-size:16px !important; font-weight:bold; ">
                            <span class="block">Valor:</span> <span class="field">
                                <asp:Label runat="server" ID="lblValor"></asp:Label></span>
                        </div>
                        <div class="label" style="padding-top: 15px;">
                            <ul class="ul-TipoPagamento">
                                <li>
                                    <asp:RadioButton runat="server" GroupName="cartao" ID="rdVisa" Checked="true" Text="<div class='spaceH'></div><img src='../images/cartaovisa.png' style='width: 70%;'/>" /></li>
                                <li>
                                    <asp:RadioButton runat="server" GroupName="cartao" ID="rdMaster" Text="<div class='spaceH'></div><img src='../images/cartaomaster.png' style='width: 70%;'/>" /></li>
                                <li>
                                    <asp:RadioButton runat="server" GroupName="cartao" ID="rdElo" Text="<div class='spaceH'></div><img src='../images/cartaoelo.png' style='width: 70%;'/>" /></li>
                                <li>
                                    <asp:RadioButton runat="server" GroupName="cartao" ID="rdDiners" Text="<div class='spaceH'></div><img src='../images/cartaodiners.png' style='width: 70%;'/>" /></li>
                            </ul>
                        </div>

                        <div class="label">
                            <span class="block" style="font-size: 14px !important">Nome Titular:</span>
                            <span class="field">
                                <asp:TextBox runat="server" ID="txtTitular"
                                    MaxLength="300" CssClass="uppercase"></asp:TextBox></span>
                        </div>

                        <div class="label">
                            <span class="block" style="font-size: 14px !important">Número do cartão:</span>
                            <span class="field">
                                <asp:TextBox runat="server" ID="txtNumeroCartao" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)"
                                    onkeyup="mascara(this,soNumeros)" MaxLength="16"></asp:TextBox></span>
                        </div>
                        <div class="label">
                            <span class="block" style="font-size: 14px !important">Código Segurança:</span>
                            <span class="field">
                                <asp:TextBox runat="server" ID="txtCodigoSeguranca" MaxLength="4" Width="44px" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)"
                                    onkeyup="mascara(this,soNumeros)"></asp:TextBox></span>
                        </div>
                        <div class="label">
                            <span class="block" style="font-size: 14px !important">Vencimento:</span>
                            <asp:DropDownList ID="dropMes" runat="server">
                                <asp:ListItem Selected="True">01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<asp:DropDownList ID="dropAno" runat="server">
                                <asp:ListItem Selected="True">2018</asp:ListItem>
                                <asp:ListItem>2019</asp:ListItem>
                                <asp:ListItem>2020</asp:ListItem>
                                <asp:ListItem>2021</asp:ListItem>
                                <asp:ListItem>2022</asp:ListItem>
                                <asp:ListItem>2023</asp:ListItem>
                                <asp:ListItem>2024</asp:ListItem>
                                <asp:ListItem>2025</asp:ListItem>
                                <asp:ListItem>2026</asp:ListItem>
                                <asp:ListItem>2027</asp:ListItem>
                                <asp:ListItem>2028</asp:ListItem>
                                <asp:ListItem>2029</asp:ListItem>
                                <asp:ListItem>2030</asp:ListItem>
                                <asp:ListItem>2031</asp:ListItem>
                                <asp:ListItem>2032</asp:ListItem>
                                <asp:ListItem>2033</asp:ListItem>
                                <asp:ListItem>2034</asp:ListItem>
                                <asp:ListItem>2035</asp:ListItem>
                                <asp:ListItem>2036</asp:ListItem>
                                <asp:ListItem>2037</asp:ListItem>
                                <asp:ListItem>2038</asp:ListItem>
                                <asp:ListItem>2039</asp:ListItem>
                                <asp:ListItem>2040</asp:ListItem>
                                <asp:ListItem>2041</asp:ListItem>
                                <asp:ListItem>2042</asp:ListItem>
                                <asp:ListItem>2043</asp:ListItem>
                                <asp:ListItem>2044</asp:ListItem>
                                <asp:ListItem>2045</asp:ListItem>
                                <asp:ListItem>2046</asp:ListItem>
                                <asp:ListItem>2047</asp:ListItem>
                                <asp:ListItem>2048</asp:ListItem>
                                <asp:ListItem>2049</asp:ListItem>
                                <asp:ListItem>2050</asp:ListItem>
                            </asp:DropDownList>

                            <span class="block" style="font-size: 14px !important">Parcela(s):</span>
                            <asp:DropDownList ID="dropParcelas" runat="server">
                            </asp:DropDownList>

                        </div>

                        <div class="label">
                            
                        </div>


                        <br />
                        <div class="label">
                            <asp:Button runat="server" ID="btnPagar" Text="Efetuar pagamento" class="button"
                                OnClick="btnPagar_Click" />
                        </div>
                        <asp:Label ID="lblMensagem" runat="server" CssClass="label_alerta"
                            Width="400px"></asp:Label>

                        <div style="padding-top:15px;">
                            <asp:Label ID="lblInformacao" runat="server" CssClass="label_auditoria"
                                Width="400px">Para auditorias e futuras verificações, estamos salvando seu IP na nossa base de dados.
                            </asp:Label>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- window mask loading -->
        <div id="mask">
        </div>
        <div id="loading">
            <img src="../images/loading.gif" />
        </div>
    </form>
</body>
</html>

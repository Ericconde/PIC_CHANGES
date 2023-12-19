<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master" AutoEventWireup="true"
    CodeFile="eventos.aspx.cs" Inherits="administrador_eventos" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function ExibirLogEventos(id) {
            $.fancybox.open({
                href: "../administrador/logEventos.aspx?id=" + id,
                type: "iframe",
                width: 750,
                height: "auto",
                padding: 5
            });

            return false;
        }
    </script>

    <script type="text/javascript">
        function AbrirEventos(id) {
            $.fancybox.open({
                href: "../administrador/lote.aspx?id=" + id,
                type: "iframe",
                width: 850,
                height: 500,
                modal: false,
                autoDimensions: false,
                autoSize: false,
                padding: 5
            });

            return false;
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1>Eventos</h1>
               
                <div class="texto_box_home_site">
                    <div class="bg_titulo titulo_claro" style="width: 892px;">
                        <div class="titulo_left bg_title_color1">
                            <asp:Label  ID="Label9" runat="server" Text="Edição"></asp:Label>
                        </div>
                    </div>
                    <table style="width: 924px;">
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label4" runat="server" CssClass="label" Text="Evento:" Width="49px"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <asp:RadioButtonList ID="radEventos" runat="server" CssClass="radioButton"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">Festa Junina</asp:ListItem>
                                    <asp:ListItem Value="2">Reveillon</asp:ListItem>
                                    <asp:ListItem Value="3">Boate</asp:ListItem>
                                    <asp:ListItem Value="4">Outro</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label135" runat="server" CssClass="label" Text="Edição:"
                                    Width="90px"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <cc2:CustomTextBox ID="txtEdicao" runat="server" CssClass="textbox" MaxLength="20"
                                    TabIndex="1" Width="150px"></cc2:CustomTextBox>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label150" runat="server" CssClass="label" Text="Status:" Width="43px"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <asp:RadioButtonList ID="radStatus" runat="server" CssClass="radioButton" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">Ativo</asp:ListItem>
                                    <asp:ListItem Value="0">Inativo</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label136" runat="server" CssClass="label" Text="Data/Hora:"
                                    Width="78px"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <asp:TextBox ID="txtDataEvento" runat="server" CssClass="textbox"
                                    onkeydown="mascara(this,data)" onkeypress="mascara(this,data)"
                                    onkeyup="mascara(this,data)" Width="67px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDataEvento_CalendarExtender" runat="server"
                                    CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="txtDataEvento">
                                </cc1:CalendarExtender>
                                <asp:DropDownList ID="dropHoraEvento" runat="server" CssClass="dropdown">
                                    <asp:ListItem>00:00</asp:ListItem>
                                    <asp:ListItem>00:30</asp:ListItem>
                                    <asp:ListItem>01:00</asp:ListItem>
                                    <asp:ListItem>01:30</asp:ListItem>
                                    <asp:ListItem>02:00</asp:ListItem>
                                    <asp:ListItem>02:30</asp:ListItem>
                                    <asp:ListItem>03:00</asp:ListItem>
                                    <asp:ListItem>03:30</asp:ListItem>
                                    <asp:ListItem>04:00</asp:ListItem>
                                    <asp:ListItem>04:30</asp:ListItem>
                                    <asp:ListItem>05:00</asp:ListItem>
                                    <asp:ListItem>05:30</asp:ListItem>
                                    <asp:ListItem>06:00</asp:ListItem>
                                    <asp:ListItem>06:30</asp:ListItem>
                                    <asp:ListItem>07:00</asp:ListItem>
                                    <asp:ListItem>07:30</asp:ListItem>
                                    <asp:ListItem>08:00</asp:ListItem>
                                    <asp:ListItem>08:30</asp:ListItem>
                                    <asp:ListItem>09:00</asp:ListItem>
                                    <asp:ListItem>09:30</asp:ListItem>
                                    <asp:ListItem>10:00</asp:ListItem>
                                    <asp:ListItem>10:30</asp:ListItem>
                                    <asp:ListItem>11:00</asp:ListItem>
                                    <asp:ListItem>11:30</asp:ListItem>
                                    <asp:ListItem>12:00</asp:ListItem>
                                    <asp:ListItem>12:30</asp:ListItem>
                                    <asp:ListItem>13:00</asp:ListItem>
                                    <asp:ListItem>13:30</asp:ListItem>
                                    <asp:ListItem>14:00</asp:ListItem>
                                    <asp:ListItem>14:30</asp:ListItem>
                                    <asp:ListItem>15:00</asp:ListItem>
                                    <asp:ListItem>15:30</asp:ListItem>
                                    <asp:ListItem>16:00</asp:ListItem>
                                    <asp:ListItem>16:30</asp:ListItem>
                                    <asp:ListItem>17:00</asp:ListItem>
                                    <asp:ListItem>17:30</asp:ListItem>
                                    <asp:ListItem>18:00</asp:ListItem>
                                    <asp:ListItem>18:30</asp:ListItem>
                                    <asp:ListItem>19:00</asp:ListItem>
                                    <asp:ListItem>19:30</asp:ListItem>
                                    <asp:ListItem>20:00</asp:ListItem>
                                    <asp:ListItem>20:30</asp:ListItem>
                                    <asp:ListItem>21:00</asp:ListItem>
                                    <asp:ListItem>21:30</asp:ListItem>
                                    <asp:ListItem>22:00</asp:ListItem>
                                    <asp:ListItem>22:30</asp:ListItem>
                                    <asp:ListItem>23:00</asp:ListItem>
                                    <asp:ListItem>23:30</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label158" runat="server" CssClass="label"
                                    Text="Início venda para não sócio:" Width="78px"></asp:Label>
                            </td>
                            <td class="celula_campo" colspan="1">
                                <asp:TextBox ID="txtDataVendaNaoSocio" runat="server" CssClass="textbox"
                                    onkeydown="mascara(this,data)" onkeypress="mascara(this,data)"
                                    onkeyup="mascara(this,data)" Width="67px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDataVendaNaoSocio_CalendarExtender" runat="server"
                                    CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="txtDataVendaNaoSocio">
                                </cc1:CalendarExtender>
                                <asp:DropDownList ID="dropHoraVendaNaoSocio" runat="server" CssClass="dropdown">
                                    <asp:ListItem>00:00</asp:ListItem>
                                    <asp:ListItem>00:30</asp:ListItem>
                                    <asp:ListItem>01:00</asp:ListItem>
                                    <asp:ListItem>01:30</asp:ListItem>
                                    <asp:ListItem>02:00</asp:ListItem>
                                    <asp:ListItem>02:30</asp:ListItem>
                                    <asp:ListItem>03:00</asp:ListItem>
                                    <asp:ListItem>03:30</asp:ListItem>
                                    <asp:ListItem>04:00</asp:ListItem>
                                    <asp:ListItem>04:30</asp:ListItem>
                                    <asp:ListItem>05:00</asp:ListItem>
                                    <asp:ListItem>05:30</asp:ListItem>
                                    <asp:ListItem>06:00</asp:ListItem>
                                    <asp:ListItem>06:30</asp:ListItem>
                                    <asp:ListItem>07:00</asp:ListItem>
                                    <asp:ListItem>07:30</asp:ListItem>
                                    <asp:ListItem>08:00</asp:ListItem>
                                    <asp:ListItem>08:30</asp:ListItem>
                                    <asp:ListItem>09:00</asp:ListItem>
                                    <asp:ListItem>09:30</asp:ListItem>
                                    <asp:ListItem>10:00</asp:ListItem>
                                    <asp:ListItem>10:30</asp:ListItem>
                                    <asp:ListItem>11:00</asp:ListItem>
                                    <asp:ListItem>11:30</asp:ListItem>
                                    <asp:ListItem>12:00</asp:ListItem>
                                    <asp:ListItem>12:30</asp:ListItem>
                                    <asp:ListItem>13:00</asp:ListItem>
                                    <asp:ListItem>13:30</asp:ListItem>
                                    <asp:ListItem>14:00</asp:ListItem>
                                    <asp:ListItem>14:30</asp:ListItem>
                                    <asp:ListItem>15:00</asp:ListItem>
                                    <asp:ListItem>15:30</asp:ListItem>
                                    <asp:ListItem>16:00</asp:ListItem>
                                    <asp:ListItem>16:30</asp:ListItem>
                                    <asp:ListItem>17:00</asp:ListItem>
                                    <asp:ListItem>17:30</asp:ListItem>
                                    <asp:ListItem>18:00</asp:ListItem>
                                    <asp:ListItem>18:30</asp:ListItem>
                                    <asp:ListItem>19:00</asp:ListItem>
                                    <asp:ListItem>19:30</asp:ListItem>
                                    <asp:ListItem>20:00</asp:ListItem>
                                    <asp:ListItem>20:30</asp:ListItem>
                                    <asp:ListItem>21:00</asp:ListItem>
                                    <asp:ListItem>21:30</asp:ListItem>
                                    <asp:ListItem>22:00</asp:ListItem>
                                    <asp:ListItem>22:30</asp:ListItem>
                                    <asp:ListItem>23:00</asp:ListItem>
                                    <asp:ListItem>23:30</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Lbl_Ingressos" runat="server" CssClass="label" Text="Ingressos:" Width="58px"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <asp:RadioButtonList ID="radIngressos" runat="server" CssClass="radioButton"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">Indisponível</asp:ListItem>
                                    <asp:ListItem Value="1">Disponível</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="lblPossuiMapa" runat="server" CssClass="label" Text="Possui mapa?" Width="43px"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <asp:RadioButtonList ID="radPossuiMapa" runat="server" CssClass="radioButton" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">Sim</asp:ListItem>
                                    <asp:ListItem Value="0">Não</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="lblLocal" runat="server" CssClass="label" Text="Local do Evento:" Width="78px"></asp:Label>
                            </td>
                            <td class="celula_campo" colspan="3">
                                <asp:DropDownList ID="dropLocal" runat="server" CssClass="dropdown" Width="300px">
                                    <asp:ListItem Selected="True" Value="-1">Selecione...</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <br />

                    <div class="bg_titulo titulo_claro" style="width: 892px;">
                        <div class="titulo_left bg_title_color1">
                            <asp:Label  ID="Label1" runat="server" Text="Quantidades"></asp:Label>
                        </div>
                    </div>
                    <table style="width: 924px;">
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label128" runat="server" CssClass="label"
                                    Text="Ingressos extras por sócio (cota):"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <cc2:CustomTextBox ID="txtNumeroIngressoExtraSocioCota" runat="server" CssClass="textbox"
                                    MaxLength="2" Width="50px" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)"
                                    onkeyup="mascara(this,soNumeros)"></cc2:CustomTextBox>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label157" runat="server" CssClass="label"
                                    Text="Ingressos extras por sócio (título):"></asp:Label>
                            </td>
                            <td class="celula_nome_campo">
                                <cc2:CustomTextBox ID="txtNumeroIngressoExtraSocioTitulo" runat="server"
                                    CssClass="textbox" MaxLength="2" Width="50px" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)"
                                    onkeyup="mascara(this,soNumeros)"></cc2:CustomTextBox>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label129" runat="server" CssClass="label"
                                    Text="Ingressos com valor de não sócio:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <cc2:CustomTextBox ID="txtNumeroIngressosValorNaoSocio" runat="server" CssClass="textbox"
                                    MaxLength="2" Width="50px"></cc2:CustomTextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtNumeroIngressosValorNaoSocio"
                                    ValidChars="0,1,2,3,4,5,6,7,8,9">
                                </cc1:FilteredTextBoxExtender>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label155" runat="server" CssClass="label"
                                    Text="Ingressos tipo cadeira:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <cc2:CustomTextBox ID="txtNumeroIngressosCadeira" runat="server"
                                    CssClass="textbox" MaxLength="2" Width="50px"></cc2:CustomTextBox>
                                <cc1:FilteredTextBoxExtender ID="txtNumeroIngressosCadeira_FilteredTextBoxExtender"
                                    runat="server" TargetControlID="txtNumeroIngressosCadeira"
                                    ValidChars="0,1,2,3,4,5,6,7,8,9">
                                </cc1:FilteredTextBoxExtender>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label143" runat="server" CssClass="label"
                                    Text="Total ingressos tipo Cadeira:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <cc2:CustomTextBox ID="txtTotalCadeiras" runat="server" CssClass="textbox" MaxLength="4"
                                    Width="50px"></cc2:CustomTextBox>
                                <cc1:FilteredTextBoxExtender ID="txtTotalCadeiras_FilteredTextBoxExtender" runat="server"
                                    TargetControlID="txtTotalCadeiras" ValidChars="0,1,2,3,4,5,6,7,8,9">
                                </cc1:FilteredTextBoxExtender>
                            </td>
                            <div id="DivAniversariante" runat="server" visible="false">
                                <td class="celula_nome_campo">
                                    <asp:Label  ID="Label2" runat="server" CssClass="label"
                                        Text="Mínimo para Promoção Aniversariante:"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtMinimoAniversariante" runat="server" CssClass="textbox" MaxLength="4"
                                        Width="50px"></cc2:CustomTextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtMinimoAniversariante_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txtMinimoAniversariante" ValidChars="0,1,2,3,4,5,6,7,8,9">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </div>
                        </tr>
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label166" runat="server" CssClass="label" Text="Ingressos camarote:"></asp:Label>
                            </td>
                            <td class="celula_campo" colspan="11">
                                <cc2:CustomTextBox ID="txtNumeroIngressosCamarote" runat="server" CssClass="textbox" MaxLength="3" Width="50px"></cc2:CustomTextBox>
                                <cc1:FilteredTextBoxExtender ID="txtNumeroIngressosCamarote_FilteredTextBoxExtender" runat="server" TargetControlID="txtNumeroIngressosCamarote" ValidChars="0,1,2,3,4,5,6,7,8,9">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:FilteredTextBoxExtender ID="txtNumeroIngressosCamarote_FilteredTextBoxExtender0" runat="server" TargetControlID="txtNumeroIngressosCamarote" ValidChars="0,1,2,3,4,5,6,7,8,9">
                                </cc1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <%--Quantidades para Boate--%>
                    <table style="width: 924px; display: none">
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="lblValorSocioMasculino" runat="server" CssClass="label"
                                    Text="Valor Sócio Masculino:"></asp:Label>
                            </td>
                            <td class="celula_campo" style="padding-top: 10px;">
                                <cc2:CustomTextBox ID="txtValorSocioMasculino" runat="server" CssClass="textbox"
                                    MaxLength="2" Width="50px" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)"
                                    onkeyup="mascara(this,soNumeros)"></cc2:CustomTextBox>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="lblValorSocioFeminino" runat="server" CssClass="label"
                                    Text="Valor Sócio Feminimo:"></asp:Label>
                            </td>
                            <td class="celula_campo" style="padding-top: 10px;">
                                <cc2:CustomTextBox ID="txtValorSocioFeminino" runat="server"
                                    CssClass="textbox" MaxLength="2" Width="50px" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)"
                                    onkeyup="mascara(this,soNumeros)"></cc2:CustomTextBox>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="lblValorNaoSocioMasculino" runat="server" CssClass="label"
                                    Text="Valor Não Sócio Masculino:"></asp:Label>
                            </td>
                            <td class="celula_campo" style="padding-top: 10px;">
                                <cc2:CustomTextBox ID="txtValorNaoSocioMasculino" runat="server" CssClass="textbox"
                                    MaxLength="2" Width="50px"></cc2:CustomTextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtNumeroIngressosValorNaoSocio"
                                    ValidChars="0,1,2,3,4,5,6,7,8,9">
                                </cc1:FilteredTextBoxExtender>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="lblValorNãoSocioFeminino" runat="server" CssClass="label"
                                    Text="Valor Não Sócio Masculino:"></asp:Label>
                            </td>
                            <td class="celula_campo" style="padding-top: 10px;">
                                <cc2:CustomTextBox ID="txtValorNãoSocioFeminino" runat="server"
                                    CssClass="textbox" MaxLength="2" Width="50px"></cc2:CustomTextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3"
                                    runat="server" TargetControlID="txtNumeroIngressosCadeira"
                                    ValidChars="0,1,2,3,4,5,6,7,8,9">
                                </cc1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                    </table>
                   
                    <div class="bg_titulo titulo_claro" style="width: 892px;">
                        <div class="titulo_left bg_title_color1">
                            <asp:Label  ID="Label5" runat="server" Text="Data limite de vencimento - pagamento em cota"></asp:Label>
                        </div>
                    </div>
                    <table style="width: 924px;">
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label6" runat="server" CssClass="label"
                                    Text="Pagando em 4 parcelas:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="txtDataLimite3ParcelaCota">
                                </cc1:CalendarExtender>
                                <asp:TextBox ID="txtDataLimite4ParcelaCota" runat="server" CssClass="textbox" MaxLength="10" onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" onkeyup="mascara(this,data)" Width="67px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDataLimite4ParcelaCota_CalendarExtender" runat="server" CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDataLimite4ParcelaCota">
                                </cc1:CalendarExtender>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label7" runat="server" CssClass="label"
                                    Text="Pagando em 3 parcelas:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                    CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="txtDataLimite2ParcelaCota">
                                </cc1:CalendarExtender>
                                <asp:TextBox ID="txtDataLimite3ParcelaCota" runat="server" CssClass="textbox" MaxLength="10" onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" onkeyup="mascara(this,data)" Width="67px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDataLimite3ParcelaCota_CalendarExtender" runat="server" CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDataLimite3ParcelaCota">
                                </cc1:CalendarExtender>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label8" runat="server" CssClass="label"
                                    Text="Pagando em 2 parcelas:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server"
                                    CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="txtDataLimite1ParcelaCota">
                                </cc1:CalendarExtender>
                                <asp:TextBox ID="txtDataLimite2ParcelaCota" runat="server" CssClass="textbox" MaxLength="10" onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" onkeyup="mascara(this,data)" Width="67px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDataLimite2ParcelaCota_CalendarExtender" runat="server" CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDataLimite2ParcelaCota">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label159" runat="server" CssClass="label" Text="Pagando em 1 parcela:"></asp:Label>
                            </td>
                            <td class="celula_campo" colspan="5">
                                <asp:TextBox ID="txtDataLimite1ParcelaCota" runat="server" CssClass="textbox" MaxLength="10" onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" onkeyup="mascara(this,data)" Width="67px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDataLimite1ParcelaCota_CalendarExtender" runat="server" CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDataLimite1ParcelaCota">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                    <br />

                    <div class="bg_titulo titulo_claro" style="width: 892px;">
                        <div class="titulo_left bg_title_color1">
                            <asp:Label  ID="Label10" runat="server" Text="Data limite de vencimento - pagamento com cartão de crédito"></asp:Label>
                        </div>
                    </div>
                    <table style="width: 924px;">
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label161" runat="server" CssClass="label" Text="Pagando em 5 parcelas:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server"
                                    CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="txtDataLimite3ParcelaCota">
                                </cc1:CalendarExtender>
                                <asp:TextBox ID="txtDataLimite5ParcelaCredito" runat="server" CssClass="textbox" MaxLength="10" onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" onkeyup="mascara(this,data)" Width="67px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDataLimite5ParcelaCredito_CalendarExtender" runat="server" CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDataLimite5ParcelaCredito">
                                </cc1:CalendarExtender>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label160" runat="server" CssClass="label" Text="Pagando em 4 parcelas:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <cc1:CalendarExtender ID="CalendarExtender5" runat="server"
                                    CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="txtDataLimite2ParcelaCota">
                                </cc1:CalendarExtender>
                                <asp:TextBox ID="txtDataLimite4ParcelaCredito" runat="server" CssClass="textbox" MaxLength="10" onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" onkeyup="mascara(this,data)" Width="67px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDataLimite4ParcelaCredito_CalendarExtender" runat="server" CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDataLimite4ParcelaCredito">
                                </cc1:CalendarExtender>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label163" runat="server" CssClass="label" Text="Pagando em 3 parcelas:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <cc1:CalendarExtender ID="CalendarExtender6" runat="server"
                                    CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="txtDataLimite1ParcelaCota">
                                </cc1:CalendarExtender>
                                <asp:TextBox ID="txtDataLimite3ParcelaCredito" runat="server" CssClass="textbox" MaxLength="10" onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" onkeyup="mascara(this,data)" Width="67px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDataLimite3ParcelaCredito_CalendarExtender" runat="server" CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDataLimite3ParcelaCredito">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label162" runat="server" CssClass="label" Text="Pagando em 2 parcelas:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <asp:TextBox ID="txtDataLimite2ParcelaCredito" runat="server" CssClass="textbox" MaxLength="10" onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" onkeyup="mascara(this,data)" Width="67px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDataLimite2ParcelaCredito_CalendarExtender" runat="server" CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDataLimite2ParcelaCredito">
                                </cc1:CalendarExtender>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label164" runat="server" CssClass="label" Text="Pagando em 1 parcela:"></asp:Label>
                            </td>
                            <td class="celula_campo" colspan="3">
                                <asp:TextBox ID="txtDataLimite1ParcelaCredito" runat="server" CssClass="textbox" MaxLength="10" onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" onkeyup="mascara(this,data)" Width="67px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDataLimite1ParcelaCredito_CalendarExtender" runat="server" CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDataLimite1ParcelaCredito">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                    <br />



                    <div class="bg_titulo titulo_claro" style="width: 892px;">
                        <div class="titulo_left bg_title_color1">
                            <asp:Label  ID="Label3" runat="server" Text="Outras Configurações"></asp:Label>
                        </div>
                    </div>
                   
                    <table style="width: 924px;">
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label165" runat="server" CssClass="label" Text="Data de retirada:"></asp:Label>
                            </td>
                            <td class="celula_campo" colspan="5">
                                <cc2:CustomTextBox ID="txtDataRetirada" runat="server" CssClass="textbox" MaxLength="25" TabIndex="1" Width="234px"></cc2:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label133" runat="server" CssClass="label" Text="Vaga estacionamento:"
                                    Width="89px"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <asp:RadioButtonList ID="radVagaEstacionamento" runat="server" CssClass="radioButton"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">Sim</asp:ListItem>
                                    <asp:ListItem Value="0">Não</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label134" runat="server" CssClass="label"
                                    Text="Número de Ingressos para ter direito à vaga:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <asp:DropDownList ID="dropNumeroIngressoEstacionamento" runat="server" CssClass="dropdown"
                                    Height="16px">
                                    <asp:ListItem Selected="True" Value="-1">Selecione...</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="Label149" runat="server" CssClass="label"
                                    Text="Vagas Estacionamento:"></asp:Label>
                            </td>
                            <td class="celula_campo">
                                <cc2:CustomTextBox ID="txtVagasEstacionamento" runat="server" CssClass="textbox"
                                    MaxLength="6" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)"
                                    onkeyup="mascara(this,soNumeros)" Width="92px"></cc2:CustomTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label  ID="lblDetalhes" runat="server" CssClass="label" Text="Detalhes:"
                                    Width="89px"></asp:Label>
                            </td>
                            <td class="celula_campo" colspan="5">
                                <FTB:FreeTextBox ID="txtDetalhes" runat="server" Height="200px"
                                    Width="670px">
                                </FTB:FreeTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style14" colspan="6">
                                <asp:Button ID="cmdSalvar" runat="server" CssClass="botao" OnClick="cmdSalvar_Click"
                                    TabIndex="2" Text="Salvar" />
                                &nbsp;<asp:Button ID="cmdCancelar" runat="server" CssClass="botao" OnClick="cmdCancelar_Click"
                                    TabIndex="3" Text="Cancelar" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label  ID="lblMensagem" runat="server" CssClass="label_alerta"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label  ID="lblNumeroLinhas" runat="server" CssClass="label_numero_linhas"></asp:Label>
                                        </td>
                                        <td>
                                            <img alt="" src="../images/eraser.png" style="width: 16px; height: 16px" />
                                        </td>
                                        <td>
                                            <asp:Label  ID="lblSumario0" runat="server" CssClass="label_sumario">Editar</asp:Label>
                                        </td>
                                        <td>
                                            <img alt="" src="../images/documentos_lotes.png" style="width: 16px; height: 16px" />
                                        </td>
                                        <td>
                                            <asp:Label  ID="lblSumario1" runat="server" CssClass="label_sumario">Configurar lotes de ingressos</asp:Label>
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hddIdEdicao" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    
                    <asp:GridView ID="grdEvento" runat="server" CssClass="grid" DataKeyNames="IDEdicao"
                        AutoGenerateColumns="False" OnRowCommand="grdEvento_RowCommand" OnRowDataBound="grdEvento_RowDataBound">
                        <RowStyle CssClass="grid" HorizontalAlign="left" />
                        <Columns >
                            <asp:BoundField DataField="Evento" HeaderText="Evento" HeaderStyle-BackColor="#006CB5" />
                            <asp:BoundField DataField="Edicao" HeaderText="Edição" HeaderStyle-BackColor="#006CB5"/>
                            <asp:BoundField DataField="Data/Hora Evento" HeaderText="Data/Hora Evento" HeaderStyle-BackColor="#006CB5"/>
                            <asp:BoundField DataField="NumeroIngressoExtraSocioCota" HeaderText="Extras por sócio (cota)" HeaderStyle-BackColor="#006CB5"/>
                            <asp:BoundField DataField="NumeroIngressoExtraSocioTitulo" HeaderText="Extras por sócio (título)" HeaderStyle-BackColor="#006CB5"/>
                            <asp:BoundField DataField="NumeroIngressosValorNaoSocio" HeaderText="Ingressos com valor de não sócio" HeaderStyle-BackColor="#006CB5"/>
                            <asp:BoundField DataField="NumeroIngressosCadeira" HeaderText="Limite de ingressos tipo cadeira" HeaderStyle-BackColor="#006CB5"/>
                            <asp:BoundField DataField="IngressosCadeira" HeaderText="Total ingressos tipo cadeira" HeaderStyle-BackColor="#006CB5"/>

                            <asp:BoundField DataField="VagaEstacionamento" HeaderText="Estac." HeaderStyle-BackColor="#006CB5"/>
                            <asp:BoundField DataField="NumIngressoEstacionamento" HeaderText="Núm. Ingr. Estac." HeaderStyle-BackColor="#006CB5"/>
                            <asp:BoundField DataField="VagasEstacionamento" HeaderText="Vagas" HeaderStyle-BackColor="#006CB5"/>
                            <asp:BoundField DataField="PossuiMapa" HeaderText="Possui mapa?" HeaderStyle-BackColor="#006CB5"/>

                            <asp:BoundField DataField="Ativo" HeaderText="Ativo" HeaderStyle-BackColor="#006CB5"/>
                            <asp:BoundField DataField="StatusIngresso" HeaderText="Ingresso Disponível" HeaderStyle-BackColor="#006CB5"/>

                            <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/images/eraser.png" HeaderStyle-BackColor="#006CB5"/>
                            <asp:ButtonField ButtonType="Image" CommandName="ConfigurarLotes" ImageUrl="~/images/documentos_lotes.png" HeaderStyle-BackColor="#006CB5"/>
                        </Columns>
                        <HeaderStyle CssClass="grid_header" HorizontalAlign="left" />
                        <AlternatingRowStyle CssClass="grid_alternative_row" />
                    </asp:GridView>
                </div>
            </div>
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lote.aspx.cs" Inherits="administrador_lote" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <script src="../scripts/mascara.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 792px;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnableScriptGlobalization="True">
        
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 850px; height: 500px; overflow-y: auto;">
                <div class="box_home">
                    <h1>
                        LOTE</h1>
                    <div class="texto_box_home_site">
                        <div class="bg_titulo titulo_claro" style="width: 792px;">
                            <div class="titulo_left bg_title_color1">
                                <asp:Label  ID="lblLote" runat="server" Text="Lote"></asp:Label></div>
                        </div>
                        <table style="width: 820px;">
                            <tr>
                                <td class="celula_nome_campo" width="100">
                                    <asp:Label  ID="Label127" runat="server" CssClass="label" Text="Lote:" Width="43px"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtLote" runat="server" CssClass="textbox" MaxLength="1"
                                        TabIndex="1" Width="33px" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)" 
                                onkeyup="mascara(this,soNumeros)"></cc2:CustomTextBox>
                                </td>
                                <td class="celula_nome_campo" width="30">
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
                                <td class="celula_nome_campo" width="100">
                                    <asp:Label  ID="Label124" runat="server" CssClass="label" Text="Período de Vendas:"
                                        Width="80px"></asp:Label>
                                </td>
                                <td class="celula_campo" colspan="3">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtInicioVendas" runat="server" CssClass="textbox" onkeydown="mascara(this,data)"
                                                    onkeypress="mascara(this,data)" onkeyup="mascara(this,data)" Width="67px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtInicioVendas_CalendarExtender" runat="server" CssClass="cal_Theme1"
                                                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtInicioVendas">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dropHoraInicial" runat="server" CssClass="dropdown">
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
                                            <td>
                                                <asp:TextBox ID="txtFimVendas" runat="server" CssClass="textbox" 
                                                    onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" 
                                                    onkeyup="mascara(this,data)" Width="67px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtFimVendas_CalendarExtender" runat="server" 
                                                    CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy" 
                                                    TargetControlID="txtFimVendas">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dropHoraFinal" runat="server" CssClass="dropdown">
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
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <div class="bg_titulo titulo_claro" style="width: 792px;">
                            <div class="titulo_left bg_title_color1">
                                <asp:Label  ID="Label1" runat="server" Text="Quantidades"></asp:Label></div>
                        </div>
                        <table style="width: 820px;">
                            <tr>
                                <td class="celula_nome_campo" width="211px">
                                    <asp:Label  ID="Label144" runat="server" CssClass="label" Text="Total de ingressos tipo sem cadeira:"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtTotalAvulsos" runat="server" CssClass="textbox" MaxLength="4"
                                        Width="50px"></cc2:CustomTextBox>
                                    <cc1:FilteredTextBoxExtender ID="txtTotalAvulsos_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txtTotalAvulsos" ValidChars="0,1,2,3,4,5,6,7,8,9">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                            </tr>
                        </table>
                        

                        <div class="bg_titulo titulo_claro" style="width: 792px;">
                            <div class="titulo_left bg_title_color1">
                                <asp:Label  ID="Label12" runat="server" Text="Valores de Ingressos"></asp:Label></div>
                        </div>

                        <table style="width: 820px;">
                            <tr>
                                <td class="" colspan="2">
                                    <div class="titulo_left bg_title_color1">
                                <asp:Label  ID="Label10" runat="server" Text="Sócio" Width="400px"></asp:Label></div>
                                </td>
                                <td class="" colspan="2">
                                <div class="titulo_left bg_title_color1">
                                <asp:Label  ID="Label11" runat="server" Text="Não sócio" Width="400px"></asp:Label></div>    
                                </td>
                            </tr>
                            <tr>
                                <td class="celula_nome_campo">
                                    <asp:Label ID="Label130" runat="server" CssClass="label" Text="Inteira (cadeira):"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtValorInteiraSocioCadeira" runat="server" CssClass="textbox" MaxLength="6" onkeydown="mascara(this,decimal)" onkeypress="mascara(this,decimal)" onkeyup="mascara(this,decimal)" Width="50px"></cc2:CustomTextBox>
                                    <asp:CheckBox ID="CheckInteiraSocioCadeira" runat="server" CssClass="checkbox" />
                                </td>
                                <td class="celula_nome_campo">
                                    <asp:Label ID="Label137" runat="server" CssClass="label" Text="Inteira (cadeira):"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtValorInteiraNaoSocioCadeira" runat="server" CssClass="textbox" MaxLength="6" onkeydown="mascara(this,decimal)" onkeypress="mascara(this,decimal)" onkeyup="mascara(this,decimal)" Width="50px"></cc2:CustomTextBox>
                                    <asp:CheckBox ID="CheckInteiraNaoSocioCadeira" runat="server" CssClass="checkbox" />
                                </td>
                            </tr>
                                <tr>
                                <td class="celula_nome_campo">
                                    <asp:Label ID="Label138" runat="server" CssClass="label" Text="Inteira (sem cadeira):"></asp:Label>
                                    </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtValorInteiraSocioAvulso" runat="server" CssClass="textbox" MaxLength="6" onkeydown="mascara(this,decimal)" onkeypress="mascara(this,decimal)" onkeyup="mascara(this,decimal)" Width="50px"></cc2:CustomTextBox>
                                    <asp:CheckBox ID="CheckInteiraSocioAvulso" runat="server" CssClass="checkbox" />
                                    </td>
                                <td class="celula_nome_campo">
                                    <asp:Label ID="Label139" runat="server" CssClass="label" Text="Inteira (sem cadeira):"></asp:Label>
                                    </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtValorInteiraNaoSocioAvulso" runat="server" CssClass="textbox" MaxLength="6" onkeydown="mascara(this,decimal)" onkeypress="mascara(this,decimal)" onkeyup="mascara(this,decimal)" Width="50px"></cc2:CustomTextBox>
                                    <asp:CheckBox ID="CheckInteiraNaoSocioAvulso" runat="server" CssClass="checkbox" />
                                    </td>
                            </tr>
                            <tr>
                                <td class="celula_nome_campo">
                                    <asp:Label ID="Label145" runat="server" CssClass="label" Text="Meio Adolescente (cadeira):"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtValorMeiaSocioCadeira" runat="server" CssClass="textbox" MaxLength="6" onkeydown="mascara(this,decimal)" onkeypress="mascara(this,decimal)" onkeyup="mascara(this,decimal)" Width="50px"></cc2:CustomTextBox>
                                    <asp:CheckBox ID="CheckMeiaSocioCadeira" runat="server" CssClass="checkbox" />
                                </td>
                                <td class="celula_nome_campo">
                                    <asp:Label ID="Label146" runat="server" CssClass="label" Text="Meio Adolescente (cadeira):"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtValorMeiaNaoSocioCadeira" runat="server" CssClass="textbox" MaxLength="6" onkeydown="mascara(this,decimal)" onkeypress="mascara(this,decimal)" onkeyup="mascara(this,decimal)" Width="50px"></cc2:CustomTextBox>
                                    <asp:CheckBox ID="CheckMeiaNaoSocioCadeira" runat="server" CssClass="checkbox" />
                                </td>
                            </tr>
                            <tr>
                                <td class="celula_nome_campo">
                                    <asp:Label ID="Label147" runat="server" CssClass="label" Text="Meio Adolescente (sem cadeira):"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtValorMeiaSocioAvulso" runat="server" CssClass="textbox" MaxLength="6" onkeydown="mascara(this,decimal)" onkeypress="mascara(this,decimal)" onkeyup="mascara(this,decimal)" Width="50px"></cc2:CustomTextBox>
                                    <asp:CheckBox ID="CheckMeiaSocioAvulso" runat="server" CssClass="checkbox" />
                                </td>
                                <td class="celula_nome_campo">
                                    <asp:Label ID="Label148" runat="server" CssClass="label" Text="Meio Adolescente (sem cadeira):"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtValorMeiaNaoSocioAvulso" runat="server" CssClass="textbox" MaxLength="6" onkeydown="mascara(this,decimal)" onkeypress="mascara(this,decimal)" onkeyup="mascara(this,decimal)" Width="50px"></cc2:CustomTextBox>
                                    <asp:CheckBox ID="CheckMeiaNaoSocioAvulso" runat="server" CssClass="checkbox" />
                                </td>
                            </tr>
                            <tr>
                                <td class="celula_nome_campo">
                                    <asp:Label ID="Label151" runat="server" CssClass="label" Text="Camarote:"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtValorCamaroteSocio" runat="server" CssClass="textbox" MaxLength="6" onkeydown="mascara(this,decimal)" onkeypress="mascara(this,decimal)" onkeyup="mascara(this,decimal)" Width="50px"></cc2:CustomTextBox>
                                    <asp:CheckBox ID="CheckCamaroteSocio" runat="server" CssClass="checkbox" />
                                </td>
                                <td class="celula_nome_campo">
                                    <asp:Label ID="Label152" runat="server" CssClass="label" Text="Camarote:"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtValorCamaroteNaoSocio" runat="server" CssClass="textbox" MaxLength="6" onkeydown="mascara(this,decimal)" onkeypress="mascara(this,decimal)" onkeyup="mascara(this,decimal)" Width="50px"></cc2:CustomTextBox>
                                    <asp:CheckBox ID="CheckCamaroteNaoSocio" runat="server" CssClass="checkbox" />
                                </td>
                            </tr>
                        </table>






                        <div class="bg_titulo titulo_claro" style="width: 792px;">
                            <div class="titulo_left bg_title_color1">
                                <asp:Label  ID="Label3" runat="server" Text="Valores de Setores (será somado aos valores acima)"></asp:Label></div>
                        </div>
                        <table style="width: 820px;">
                            <tr>
                                <td class="celula_nome_campo">
                                    <asp:Label  ID="Label4" runat="server" CssClass="label" 
                                        Text="Ipanema:"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtValorIpanema" runat="server" CssClass="textbox"
                                        MaxLength="6" onkeydown="mascara(this,decimal)" onkeypress="mascara(this,decimal)"
                                        onkeyup="mascara(this,decimal)" Width="50px"></cc2:CustomTextBox>
                                </td>
                                <td class="celula_nome_campo">
                                    <asp:Label  ID="Label5" runat="server" CssClass="label" 
                                        Text="Golódromo:"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtValorGolodromo" runat="server" CssClass="textbox"
                                        MaxLength="6" onkeydown="mascara(this,decimal)" onkeypress="mascara(this,decimal)"
                                        onkeyup="mascara(this,decimal)" Width="50px"></cc2:CustomTextBox>
                                </td>
                                <td class="celula_nome_campo">
                                    <asp:Label  ID="Label6" runat="server" CssClass="label" 
                                        Text="Portinari:"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtValorPortinari" runat="server" CssClass="textbox"
                                        MaxLength="6" onkeydown="mascara(this,decimal)" onkeypress="mascara(this,decimal)"
                                        onkeyup="mascara(this,decimal)" Width="50px"></cc2:CustomTextBox>
                                </td>
                                <td class="celula_nome_campo">
                                    <asp:Label  ID="Label7" runat="server" CssClass="label" 
                                        Text="Pérgula:"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtValorPergula" runat="server" CssClass="textbox"
                                        MaxLength="6" onkeydown="mascara(this,decimal)" onkeypress="mascara(this,decimal)"
                                        onkeyup="mascara(this,decimal)" Width="50px"></cc2:CustomTextBox>
                                </td>
                                <td class="celula_nome_campo">
                                    <asp:Label  ID="Label8" runat="server" CssClass="label" 
                                        Text="Salão de Festas:"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtValorSalaoDeFestas" runat="server" CssClass="textbox"
                                        MaxLength="6" onkeydown="mascara(this,decimal)" onkeypress="mascara(this,decimal)"
                                        onkeyup="mascara(this,decimal)" Width="50px"></cc2:CustomTextBox>
                                </td>
                                <td class="celula_nome_campo">
                                    <asp:Label  ID="Label9" runat="server" CssClass="label" 
                                        Text="Academia:"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <cc2:CustomTextBox ID="txtValorAcademia" runat="server" CssClass="textbox"
                                        MaxLength="6" onkeydown="mascara(this,decimal)" onkeypress="mascara(this,decimal)"
                                        onkeyup="mascara(this,decimal)" Width="50px"></cc2:CustomTextBox>
                                </td>
                            </tr>
                        </table>















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
                                            <asp:Label  ID="lblSumario0" runat="server" CssClass="label_sumario">Editar</asp:Label>
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hddIdLote" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        </table>
                        <asp:GridView ID="grdLote" runat="server" CssClass="grid" DataKeyNames="IDLote" AutoGenerateColumns="False"
                            OnRowCommand="grdLote_RowCommand">
                            <RowStyle CssClass="grid" HorizontalAlign="left" />
                            <Columns>
                                <asp:BoundField DataField="Lote" HeaderText="Lote" />
                                <asp:BoundField DataField="Início Venda" HeaderText="Início Vendas" />
                                <asp:BoundField DataField="Fim Venda" HeaderText="Fim Vendas" />
                                
                                <asp:BoundField DataField="ValorInteiraCadeiraSocio" HeaderText="Valor Inteira Cadeira (sócio)" />
                                <asp:BoundField DataField="ValorInteiraCadeiraNaoSocio" HeaderText="Valor Inteira Cadeira (não sócio)" />
                                <asp:BoundField DataField="ValorInteiraAvulsoSocio" HeaderText="Valor Inteira sem cadeira (sócio)" />
                                <asp:BoundField DataField="ValorInteiraAvulsoNaoSocio" HeaderText="Valor Inteira sem cadeira (não sócio)" />
                                <asp:BoundField DataField="ValorMeiaCadeiraSocio" HeaderText="Valor Meio Adol. Cadeira (sócio)" />
                                <asp:BoundField DataField="ValorMeiaCadeiraNaoSocio" HeaderText="Valor Meio Adol. Cadeira (não sócio)" />
                                <asp:BoundField DataField="ValorMeiaAvulsoSocio" HeaderText="Valor Meio Adol. sem cadeira (sócio)" />
                                <asp:BoundField DataField="ValorMeiaAvulsoNaoSocio" HeaderText="Valor Meio Adol. sem cadeira (não sócio)" />

                                <asp:BoundField DataField="IngressosAvulsos" HeaderText="Núm. Ingressos Avulsos" />
                                <asp:BoundField DataField="Ativo" HeaderText="Ativo" />
                                <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/images/eraser.png" />
                            </Columns>
                            <HeaderStyle CssClass="grid_header" HorizontalAlign="left" />
                            <AlternatingRowStyle CssClass="grid_alternative_row" />
                        </asp:GridView>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

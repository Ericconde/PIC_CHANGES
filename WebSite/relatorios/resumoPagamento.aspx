<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master"
    AutoEventWireup="true" CodeFile="resumoPagamento.aspx.cs" Inherits="relatorios_resumoPagamento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
            <asp:PostBackTrigger ControlID="IngressosPIC" />
        </Triggers>
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1>RELATÓRIO - RESUMO POR FORMA DE PAGAMENTO</h1>
                <div class="texto_box_home_site">
                    <table style="margin-top: 5px;">
                        <tr>
                            <td>
                                <table class="style4">
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblEvento" runat="server" CssClass="label" Text="Evento:"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <asp:DropDownList ID="dropEdicao" runat="server" CssClass="dropdown" 
                                                Width="400px" AutoPostBack="True" 
                                                onselectedindexchanged="dropEdicao_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblLote" runat="server" CssClass="label" Text="Lote:"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <asp:DropDownList ID="dropLote" runat="server" CssClass="dropdown" Width="100px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblPeriodo" runat="server" CssClass="label" Text="Período da Compra:" 
                                               ></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <asp:TextBox ID="txtDataInicial" runat="server" CssClass="textbox" 
                                                onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" 
                                                onkeyup="mascara(this,data)" Width="67px" MaxLength="10"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDataInicial_CalendarExtender" runat="server" 
                                                CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy" 
                                                TargetControlID="txtDataInicial">
                                            </cc1:CalendarExtender>
                                            &nbsp;a
                                            <asp:TextBox ID="txtDataFinal" runat="server" CssClass="textbox" 
                                                onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" 
                                                onkeyup="mascara(this,data)" Width="67px" MaxLength="10"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDataFinal_CalendarExtender" runat="server" 
                                                CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy" 
                                                TargetControlID="txtDataFinal">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblCliente" runat="server" CssClass="label" Text="Cliente:" 
                                                Width="48px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <asp:CheckBoxList ID="chkCliente" runat="server" CssClass="checkbox" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="3">Sócio</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="4">Não sócio</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblFormaPagamento" runat="server" CssClass="label" 
                                                Text="Forma Pagamento:"></asp:Label>
                                        </td>
                                        <td class="celula_campo" colspan="3">
                                            <asp:DropDownList ID="dropFormaPagamento" runat="server" CssClass="dropdown" 
                                                Width="300px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="2">
                                            <asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" 
                                                OnClick="cmdPesquisar_Click" TabIndex="2" Text="Pesquisar" />
                                            &nbsp;<asp:Button ID="cmdVoltar" runat="server" CssClass="botao" 
                                                OnClick="cmdVoltar_Click" TabIndex="3" Text="Voltar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="4">
                                            <asp:Label  ID="lblMensagem" runat="server" CssClass="label_alerta"></asp:Label>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>
                        </tr>
                        
                    </table>
                    
                </div>
            </div>
            <CR:CrystalReportViewer ID="IngressosPIC" runat="server" AutoDataBind="true" 
                                                BestFitPage="False" EnableDatabaseLogonPrompt="False" 
                                                EnableParameterPrompt="False" HasCrystalLogo="False" HasDrillUpButton="False" 
                                                HasGotoPageButton="False" HasSearchButton="False" 
                                                HasToggleGroupTreeButton="False"  HasZoomFactorList="False" 
                                                ToolbarStyle-BackColor="#E0E0E0" />
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

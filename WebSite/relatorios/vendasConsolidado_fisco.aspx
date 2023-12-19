﻿<%@ Page Title="" Language="C#" MasterPageFile="~/controls/Fisco.master" AutoEventWireup="true" 
CodeFile="vendasConsolidado_fisco.aspx.cs" Inherits="relatorios_vendasConsolidado_fisco" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>

<%--<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <Triggers>
            
        </Triggers>
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1>RELATÓRIO - VENDAS CONSOLIDADO FISCO</h1>
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
                                                Width="400px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblPeriodo" runat="server" CssClass="label" Text="Período:" 
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
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" 
                                                OnClick="cmdPesquisar_Click" TabIndex="2" Text="Pesquisar" />
                                            &nbsp;<asp:Button ID="cmdVoltar" runat="server" CssClass="botao" 
                                                OnClick="cmdVoltar_Click" TabIndex="3" Text="Voltar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label  ID="lblMensagem" runat="server" 
                                                CssClass="label_alerta"></asp:Label>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>
                        </tr>
                        
                    </table>

                    <asp:GridView ID="grdVenda" runat="server"
                                    AutoGenerateColumns="True" CssClass="grid" 
                                    EmptyDataText="Não existe registro para esse intervalo de data!" 
                                     PageSize="25" 
                                    Width="124%" onrowdatabound="grdVenda_RowDataBound" ShowFooter="True">
                                    <RowStyle CssClass="grid" HorizontalAlign="left" />
                                    <Columns>
                                        
                                    </Columns>
                                    <HeaderStyle CssClass="grid_header" HorizontalAlign="left" />
                                    <AlternatingRowStyle CssClass="grid_alternative_row" />
                                    <FooterStyle CssClass="grid_Footer" />
                                    <PagerStyle CssClass="grid_page" />
                                </asp:GridView>
                    
                </div>
            </div>
           
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



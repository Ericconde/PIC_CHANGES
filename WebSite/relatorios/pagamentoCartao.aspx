<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master" AutoEventWireup="true"
    CodeFile="pagamentoCartao.aspx.cs" Inherits="relatorios_pagamentoCartao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="cmdExportarExcel" />
        </Triggers>
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1>RELATÓRIO - PAGAMENTOS COM CARTÃO</h1>
                <div class="texto_box_home_site">
                    <table style="margin-top: 5px;">
                        <tr>
                            <td>
                                <div class="bg_titulo titulo_claro" style="width: 892px;">
                                    <div class="titulo_left bg_title_color1">
                                        <asp:Label ID="Label9" runat="server" Text="Dados do Evento"></asp:Label>
                                    </div>
                                </div>
                                <table class="style4" style="width: 925px;">
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label ID="lblEvento" runat="server" CssClass="label" Text="Edição:"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <asp:DropDownList ID="dropEdicao" runat="server" CssClass="dropdown" Width="400px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label ID="lblNome0" runat="server" CssClass="label" Text="Período:" Width="48px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <asp:TextBox ID="txtDataInicial" runat="server" CssClass="textbox" MaxLength="10" onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" onkeyup="mascara(this,data)" Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDataInicial_CalendarExtender" runat="server" CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDataInicial">
                                            </cc1:CalendarExtender>
                                            &nbsp;a
                                            <asp:TextBox ID="txtDataFinal" runat="server" CssClass="textbox" MaxLength="10" onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" onkeyup="mascara(this,data)" Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDataFinal_CalendarExtender" runat="server" CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDataFinal">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label ID="lblNome" runat="server" CssClass="label" Text="Nome (usuário do sistema):"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtNome" runat="server" CssClass="textbox" MaxLength="100" TabIndex="3" Width="294px"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label ID="Label125" runat="server" CssClass="label" Height="16px" Text="Voucher:"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtVoucher" runat="server" CssClass="textbox" MaxLength="8" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)" onkeyup="mascara(this,soNumeros)" TabIndex="6" Width="120px"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                </table>

                                <div class="bg_titulo titulo_claro" style="width: 892px;">
                                    <div class="titulo_left bg_title_color1">
                                        <asp:Label ID="Label1" runat="server" Text="Dados do Cartão"></asp:Label>
                                    </div>
                                </div>
                                <table style="width: 925px;">
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label ID="lblCodigo" runat="server" CssClass="label" Text="Cód. Autorização:"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtCodigoAutorizacao" runat="server" CssClass="textbox" MaxLength="10" TabIndex="1" Width="100px"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label ID="lblTid" runat="server" CssClass="label" Text="TID:"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtTid" runat="server" CssClass="textbox" MaxLength="30" TabIndex="2" Width="294px"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label ID="lblTitular" runat="server" CssClass="label" Text="Titular (escrito no cartão):"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtTitular" runat="server" CssClass="textbox" MaxLength="100"
                                                Width="294px" TabIndex="3"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label ID="Label124" runat="server" CssClass="label" Height="16px" Text="Cota:"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtNumCota" runat="server" CssClass="textbox" MaxLength="6"
                                                onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)" onkeyup="mascara(this,soNumeros)"
                                                Width="120px" TabIndex="4"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label ID="lblNumeroCartao" runat="server" CssClass="label" Text="Núm. Cartão:"></asp:Label>
                                        </td>
                                        <td class="celula_campo" colspan="3">
                                            <cc2:CustomTextBox ID="txtNumeroCartao" runat="server" CssClass="textbox" MaxLength="16"
                                                onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)" onkeyup="mascara(this,soNumeros)"
                                                Width="250px" TabIndex="5"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="4">
                                            <asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" OnClick="cmdPesquisar_Click"
                                                TabIndex="7" Text="Pesquisar" />
                                            &nbsp;<asp:Button ID="cmdExportarExcel" runat="server" CssClass="botao" TabIndex="8"
                                                Text="Exportar Excel" OnClick="cmdExportarExcel_Click" />
                                            &nbsp;<asp:Button ID="cmdVoltar" runat="server" CssClass="botao" OnClick="cmdVoltar_Click"
                                                TabIndex="9" Text="Voltar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="4">
                                            <asp:Label ID="lblMensagem" runat="server" CssClass="label_alerta"></asp:Label>
                                        </td>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <asp:Label ID="lblNumeroRegistros" runat="server" CssClass="label_numero_linhas"></asp:Label>
                    <asp:GridView ID="grdRelatorio" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="grid" OnPageIndexChanging="grdRelatorio_PageIndexChanging" PageSize="25">
                        <RowStyle CssClass="grid" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundField DataField="Voucher" HeaderText="Voucher" HeaderStyle-BackColor="#4d94c4"/>
                            <asp:BoundField DataField="Nome Cliente" HeaderText="Nome (usuário do sistema)" HeaderStyle-BackColor="#4d94c4"/>
                            <asp:BoundField DataField="Cota" HeaderText="Cota" HeaderStyle-BackColor="#4d94c4"/>
                            <asp:BoundField DataField="Titular" HeaderText="Titular (escrito no cartão)" HeaderStyle-BackColor="#4d94c4"/>
                            <asp:BoundField DataField="Número Cartão" HeaderText="Número Cartão" HeaderStyle-BackColor="#4d94c4"/>
                            <asp:BoundField DataField="Bandeira" HeaderText="Bandeira" HeaderStyle-BackColor="#4d94c4"/>
                            <asp:BoundField DataField="Cód. Autorização" HeaderText="Cód. Autorização" HeaderStyle-BackColor="#4d94c4"/>
                            <asp:BoundField DataField="TID Cielo" HeaderText="TID" HeaderStyle-BackColor="#4d94c4"/>
                            <asp:BoundField DataField="Aprovação Cielo" HeaderText="Aprovação Cielo" HeaderStyle-BackColor="#4d94c4"/>
                            <asp:BoundField DataField="Captura Cielo" HeaderText="Captura Cielo" HeaderStyle-BackColor="#4d94c4"/>
                            <asp:BoundField DataField="Data/Hora Transação" HeaderText="Data/Hora Transação" HeaderStyle-BackColor="#4d94c4"/>
                            <asp:BoundField DataField="Status da Compra" HeaderText="Status da Compra" HeaderStyle-BackColor="#4d94c4"/>
                        </Columns>
                        <HeaderStyle CssClass="grid_header" HorizontalAlign="Left" />
                        <AlternatingRowStyle CssClass="grid_alternative_row" />
                        <PagerStyle CssClass="grid_page" />
                    </asp:GridView>

                </div>
            </div>
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

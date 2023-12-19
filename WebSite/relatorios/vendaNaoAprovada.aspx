<%@ Page Title="" Language="C#" MasterPageFile="~/controls/Admin.master" AutoEventWireup="true" CodeFile="vendaNaoAprovada.aspx.cs" Inherits="relatorios_vendaNaoAprovada" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>

<%--<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>--%>

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
                <h1>RELATÓRIO - VENDA NÃO APROVADA (Cartão)</h1>
                <div class="texto_box_home_site">
                    <table style="margin-top: 5px;">
                        <tr>
                            <td>
                                <table class="style4">
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblEvento" runat="server" CssClass="label" Text="Evento:"></asp:Label>
                                        </td>
                                        <td class="celula_campo" colspan="3">
                                            <asp:DropDownList ID="dropEdicao" runat="server" CssClass="dropdown" 
                                                Width="400px" >
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblPeriodo" runat="server" CssClass="label" Text="Período da Compra:" 
                                               ></asp:Label>
                                        </td>
                                        <td class="celula_campo" colspan="3">
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
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblNome3" runat="server" CssClass="label" 
                                                Text="Núm. Voucher:" Width="52px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtVoucher" runat="server" CssClass="textbox" 
                                                MaxLength="6" onkeydown="mascara(this,soNumeros)" 
                                                onkeypress="mascara(this,soNumeros)" onkeyup="mascara(this,soNumeros)" 
                                                TabIndex="1" Width="80px"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblCliente0" runat="server" CssClass="label" Text="Cliente:" 
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
                                        <td colspan="4">
                                            <asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" 
                                                OnClick="cmdPesquisar_Click" TabIndex="2" Text="Pesquisar" />
                                            &nbsp;<asp:Button ID="cmdExportarExcel" runat="server" CssClass="botao" 
                                                onclick="cmdExportarExcel_Click" TabIndex="2" Text="Exportar Excel" />
                                            &nbsp;<asp:Button ID="cmdVoltar" runat="server" CssClass="botao" 
                                                OnClick="cmdVoltar_Click" TabIndex="3" Text="Voltar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label  ID="lblMensagem" runat="server" CssClass="label_alerta"></asp:Label>
                                            <asp:Label  ID="lblNumeroRegistros" runat="server" 
                                                CssClass="label_numero_linhas"></asp:Label>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>
                        </tr>
                        
                    </table>

                    <asp:GridView ID="grdVendaNaoAprovada" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" CssClass="grid" 
                                    EmptyDataText="Não existe registro para esse intervalo de data!" 
                                    onpageindexchanging="grdVendaNaoAprovada_PageIndexChanging" PageSize="25" 
                                    Width="124%">
                                    <RowStyle CssClass="grid" HorizontalAlign="left" />
                                    <Columns>
                                        <asp:BoundField DataField="Núm Voucher" HeaderText="Núm Voucher" />
                                        <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                        <asp:BoundField DataField="CPF" HeaderText="CPF" />

                                        <asp:BoundField DataField="Titular" HeaderText="Titular" />
                                        <asp:BoundField DataField="Número Cartão" HeaderText="Número Cartão" />
                                        <asp:BoundField DataField="Bandeira" HeaderText="Bandeira" />
                                        <asp:BoundField DataField="Valor" HeaderText="Valor" />
                                        <asp:BoundField DataField="Parcelas" HeaderText="Parcelas" />

                                        <asp:BoundField DataField="Data/Hora Cadastro" 
                                            HeaderText="Data/Hora Cadastro" />
                                        <asp:BoundField DataField="Edicao" HeaderText="Edição" />
                                        <asp:BoundField DataField="Perfil" HeaderText="Tipo Cliente" />
                                    </Columns>
                                    <HeaderStyle CssClass="grid_header" HorizontalAlign="left" />
                                    <AlternatingRowStyle CssClass="grid_alternative_row" />
                                    <PagerStyle CssClass="grid_page" />
                                </asp:GridView>
                    
                </div>
            </div>
           
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



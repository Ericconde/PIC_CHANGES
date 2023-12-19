<%@ Page Title="" Language="C#" MasterPageFile="~/controls/Admin.master" AutoEventWireup="true"
    CodeFile="ticket.aspx.cs" Inherits="relatorios_tickets" %>

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
                <h1>
                    RELATÓRIO - TICKETS / INGRESSOS (COMPRA CONCLUÍDA)</h1>
                <div class="texto_box_home_site">
                    <table style="margin-top: 5px;">
                        <tr>
                            <td>
                                <table class="style4">
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblEvento0" runat="server" CssClass="label" Text="Evento:"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <asp:DropDownList ID="dropEdicao" runat="server" CssClass="dropdown" 
                                                Width="400px" >
                                            </asp:DropDownList>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblVoucher" runat="server" CssClass="label" Text="Núm. Voucher:" 
                                                Width="52px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtVoucher" runat="server" CssClass="textbox" 
                                                MaxLength="6" onkeydown="mascara(this,soNumeros)" 
                                                onkeypress="mascara(this,soNumeros)" onkeyup="mascara(this,soNumeros)" 
                                                TabIndex="1" Width="110px"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblPeriodo" runat="server" CssClass="label" 
                                                Text="Período da Compra:"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <asp:TextBox ID="txtDataInicial" runat="server" CssClass="textbox" 
                                                MaxLength="10" onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" 
                                                onkeyup="mascara(this,data)" Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDataInicial_CalendarExtender" runat="server" 
                                                CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy" 
                                                TargetControlID="txtDataInicial">
                                            </cc1:CalendarExtender>
                                            &nbsp;a
                                            <asp:TextBox ID="txtDataFinal" runat="server" CssClass="textbox" MaxLength="10" 
                                                onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" 
                                                onkeyup="mascara(this,data)" Width="67px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDataFinal_CalendarExtender" runat="server" 
                                                CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy" 
                                                TargetControlID="txtDataFinal">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblCota" runat="server" CssClass="label" Text="Cota:" 
                                                Width="52px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtCota" runat="server" CssClass="textbox" MaxLength="6" 
                                                onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)" 
                                                onkeyup="mascara(this,soNumeros)" TabIndex="1" Width="110px"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblCliente" runat="server" CssClass="label" Text="Cliente:" 
                                                Width="48px"></asp:Label>
                                        </td>
                                        <td class="celula_campo" colspan="3">
                                            <asp:CheckBoxList ID="chkCliente" runat="server" CssClass="checkbox" 
                                                Height="16px" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="3">Sócio</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="4">Não sócio</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblNome" runat="server" CssClass="label" Text="Nome:"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtNome" runat="server" CssClass="textbox" 
                                                MaxLength="500" 
                                                TabIndex="1" Width="400px"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblCPF" runat="server" CssClass="label" Text="CPF:"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtCPF" runat="server" CssClass="textbox" MaxLength="11" 
                                                onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)" 
                                                onkeyup="mascara(this,soNumeros)" TabIndex="1" Width="200px"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" 
                                                OnClick="cmdPesquisar_Click" TabIndex="2" Text="Pesquisar" />
                                            <asp:Button ID="cmdExportarExcel" runat="server" CssClass="botao" 
                                                OnClick="cmdExportarExcel_Click" TabIndex="2" Text="Exportar Excel" />
                                            <asp:Button ID="cmdVoltar" runat="server" CssClass="botao" 
                                                OnClick="cmdVoltar_Click" TabIndex="3" Text="Voltar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label  ID="lblMensagem" runat="server" CssClass="label_alerta"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label  ID="lblNumeroRegistros" runat="server" 
                                                CssClass="label_numero_linhas"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                 <asp:GridView ID="grdTicket" runat="server" AllowPaging="True" 
                                                AutoGenerateColumns="False" CssClass="grid" 
                                                EmptyDataText="Não existe tickets com esse(s) parâmetro(s)!" 
                                                OnPageIndexChanging="grdTicket_PageIndexChanging" 
                                    PageSize="25" Width="950px">
                                                <RowStyle CssClass="grid" HorizontalAlign="left" />
                                                <Columns>
                                                <asp:BoundField DataField="Tipo de Ingresso" HeaderText="Tipo de Ingresso" />
                                                <asp:BoundField DataField="Ticket" HeaderText="Ticket" />
                                                <asp:BoundField DataField="Valor" HeaderText="Valor" />
                                                <asp:BoundField DataField="Voucher" HeaderText="Voucher" />
                                                <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                                <asp:BoundField DataField="Cota" HeaderText="Cota" />
                                                <asp:BoundField DataField="CPF" HeaderText="CPF" />
                                                <asp:BoundField DataField="Forma de Pagamento" HeaderText="Forma de Pagamento" />
                                                <asp:BoundField DataField="Data/Hora Compra" HeaderText="Data/Hora Compra" />
                                                <asp:BoundField DataField="Responsável pela Compra" HeaderText="Responsável pela Compra" />
                                                </Columns>
                                                <HeaderStyle CssClass="grid_header" HorizontalAlign="left" />
                                                <AlternatingRowStyle CssClass="grid_alternative_row" />
                                                <PagerStyle CssClass="grid_page" />
                                            </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

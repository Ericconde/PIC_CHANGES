<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master" AutoEventWireup="true"
    CodeFile="cancelamentoVoucher.aspx.cs" Inherits="relatorios_cancelamentoVoucher" %>

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
                <h1>
                    RELATÓRIO - CANCELAMENTO VOUCHER</h1>
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
                                            <asp:DropDownList ID="dropEdicao" runat="server" CssClass="dropdown" Width="400px"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblPeriodo" runat="server" CssClass="label" Text="Período da Compra:"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <asp:TextBox ID="txtDataInicial" runat="server" CssClass="textbox" onkeydown="mascara(this,data)"
                                                onkeypress="mascara(this,data)" onkeyup="mascara(this,data)" Width="67px" MaxLength="10"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDataInicial_CalendarExtender" runat="server" CssClass="cal_Theme1"
                                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDataInicial">
                                            </cc1:CalendarExtender>
                                            &nbsp;a
                                            <asp:TextBox ID="txtDataFinal" runat="server" CssClass="textbox" onkeydown="mascara(this,data)"
                                                onkeypress="mascara(this,data)" onkeyup="mascara(this,data)" Width="67px" MaxLength="10"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDataFinal_CalendarExtender" runat="server" CssClass="cal_Theme1"
                                                Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDataFinal">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblFormaPagamento" runat="server" CssClass="label" Text="Forma Pagamento:"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <asp:DropDownList ID="dropFormaPagamento" runat="server" CssClass="dropdown" Width="300px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="Label124" runat="server" CssClass="label" Height="16px" Text="Cota:"
                                                Width="45px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <table>
                                                <tr>
                                                    <td class="celula_campo">
                                                        <cc2:CustomTextBox ID="txtNumCota" runat="server" CssClass="textbox" MaxLength="6"
                                                            onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)" onkeyup="mascara(this,soNumeros)"
                                                            Width="120px"></cc2:CustomTextBox>
                                                    </td>
                                                    <td class="celula_nome_campo">
                                                        <asp:Label  ID="Label1" runat="server" CssClass="label" Height="16px" Text="Dígito:"
                                                            Width="45px"></asp:Label>
                                                    </td>
                                                    <td class="celula_campo">
                                                        <cc2:CustomTextBox ID="txtDigito" runat="server" CssClass="textbox" MaxLength="2"
                                                            onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)" onkeyup="mascara(this,soNumeros)"
                                                            Width="53px"></cc2:CustomTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" OnClick="cmdPesquisar_Click"
                                                TabIndex="2" Text="Pesquisar" />
                                            &nbsp;<asp:Button ID="cmdVoltar" runat="server" CssClass="botao" OnClick="cmdVoltar_Click"
                                                TabIndex="3" Text="Voltar" />
                                            &nbsp;<asp:Button ID="cmdExportarExcel" runat="server" CssClass="botao" TabIndex="4"
                                                Text="Exportar Excel" OnClick="cmdExportarExcel_Click" />
                                                <asp:Label  ID="lblMensagem" runat="server" CssClass="label_alerta"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="4">
                                            <asp:Label  ID="lblNumeroRegistros" runat="server" CssClass="label_numero_linhas"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <tr>
                                    <td valign="top">
                                        <asp:GridView ID="grdRelatorio" runat="server" CssClass="grid" AllowPaging="True"
                                            PageSize="25" OnPageIndexChanging="grdRelatorio_PageIndexChanging" AutoGenerateColumns="False">
                                            <RowStyle CssClass="grid" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="Voucher" HeaderText="Voucher" />
                                                <asp:BoundField DataField="Nome do Cliente" HeaderText="Nome do Cliente" />
                                                <asp:BoundField DataField="Cota" HeaderText="Cota" />
                                                <asp:BoundField DataField="Dígito" HeaderText="Dígito" />
                                                <asp:BoundField DataField="Data da Compra" HeaderText="Data da Compra" />
                                                <asp:BoundField DataField="Local de retirada" HeaderText="Local de retirada" />
                                                <asp:BoundField DataField="Status de Compra" HeaderText="Status de Compra" />
                                                <asp:BoundField DataField="Edição" HeaderText="Edição" />
                                                <asp:BoundField DataField="Forma de Pagamento" HeaderText="Forma de Pagamento" />
                                                <asp:BoundField DataField="Detalhes do Pagamento" HeaderText="Detalhes do Pagamento" />

                                                <asp:BoundField DataField="Número do Cartão" HeaderText="Número do Cartão" />
                                                <asp:BoundField DataField="Cód. Autorização" HeaderText="Cód. Autorização" />
                                                <asp:BoundField DataField="Valor total no cartão" HeaderText="Valor total no cartão" />

                                                <asp:BoundField DataField="Data do Cancelamento" HeaderText="Data do Cancelamento" />
                                                <asp:BoundField DataField="Responsável pelo cancelamento" HeaderText="Responsável pelo cancelamento" />
                                            </Columns>
                                            <HeaderStyle CssClass="grid_header" HorizontalAlign="Left" />
                                            <AlternatingRowStyle CssClass="grid_alternative_row" />
                                            <PagerStyle CssClass="grid_page" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                    </table>
                    </td> </tr>
                </div>
            </div>
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

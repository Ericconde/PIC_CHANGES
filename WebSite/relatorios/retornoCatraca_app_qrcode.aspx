<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master" AutoEventWireup="true"
    CodeFile="retornoCatraca_app_qrcode.aspx.cs" Inherits="relatorios_retornoCatraca_app_qrcode" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">       
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home" style="width: 975px;">
                <h1>RELATÓRIO - Registro de Entrada (App leitor de QRCode)</h1>
                <div class="texto_box_home_site">

                    <table>
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="QR Code:"></asp:Label>
                            </td>
                            <td class="celula_campo" width="700">
                                <cc2:CustomTextBox ID="txtQRCode" runat="server" CssClass="textbox" MaxLength="30"
                                    TabIndex="1" Width="300px" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)"
                                    onkeyup="mascara(this,soNumeros)"></cc2:CustomTextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="celula_nome_campo">
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Nº do Ingresso / Ticket:"></asp:Label>
                            </td>
                            <td class="celula_campo" width="700">
                                <cc2:CustomTextBox ID="txtTicket" runat="server" CssClass="textbox" MaxLength="30"
                                    TabIndex="1" Width="170px" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)"
                                    onkeyup="mascara(this,soNumeros)"></cc2:CustomTextBox>

                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td  colspan="2">
                                <asp:Button ID="cmdPesquisar" runat="server" CssClass="botao"
                                    TabIndex="2" Text="Pesquisar" OnClick="cmdPesquisar_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="15"></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="grdRelatorio" runat="server" CssClass="grid"
                                    AutoGenerateColumns="False" EmptyDataText="Nenhum registro encontrado">
                                    <RowStyle CssClass="grid" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="Ticket (Núm. Ingresso)" HeaderText="Ticket (Núm. Ingresso)" />
                                        <asp:BoundField DataField="Tipo de Ingresso" HeaderText="Tipo de Ingresso" />
                                        <asp:BoundField DataField="Nome do Cliente" HeaderText="Nome do Cliente" />
                                        <asp:BoundField DataField="CPF" HeaderText="CPF" />
                                        <asp:BoundField DataField="Cota" HeaderText="Cota" />
                                        <asp:BoundField DataField="Data/Hora da Entrada" HeaderText="Data/Hora da Entrada" />
                                        <asp:BoundField DataField="Responsável Entrada" HeaderText="Responsável Entrada" />
                                    </Columns>
                                    <HeaderStyle CssClass="grid_header" HorizontalAlign="Left" />
                                    <AlternatingRowStyle CssClass="grid_alternative_row" />
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

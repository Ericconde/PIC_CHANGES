<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master" AutoEventWireup="true"
    CodeFile="retornoCatraca_fisica.aspx.cs" Inherits="relatorios_retornoCatraca_fisica" %>

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
            <div class="box_home" style="margin-left: 1%;">
                <h1> RELAT�RIO - Registro de Entrada (catraca f�sica)</h1>
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
                                            <asp:Label  ID="lblNomeCliente" runat="server" CssClass="label" Text="Nome Cliente:"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtNome" runat="server" CssClass="textbox" MaxLength="100"
                                                Width="294px"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblVoucher" runat="server" CssClass="label" Text="N�m. Voucher:" Width="52px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtVoucher" runat="server" CssClass="textbox" MaxLength="6"
                                                onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)" onkeyup="mascara(this,soNumeros)"
                                                TabIndex="1" Width="110px"></cc2:CustomTextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" OnClick="cmdPesquisar_Click"
                                                TabIndex="2" Text="Pesquisar" />
                                            &nbsp;<asp:Button ID="cmdVoltar" runat="server" CssClass="botao" OnClick="cmdVoltar_Click"
                                                TabIndex="3" Text="Voltar" />
                                                &nbsp;<asp:Button ID="cmdExportarExcel" runat="server" CssClass="botao" 
                                                 TabIndex="4" Text="Exportar Excel" onclick="cmdExportarExcel_Click" />
                                            <asp:Label  ID="lblMensagem" runat="server" CssClass="label_alerta"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="2">
                                            <asp:Label  ID="lblNumeroRegistros" runat="server" 
                                                CssClass="label_numero_linhas"></asp:Label>
                                        </td>
                                    </tr>
                                    </table>
                                    <tr>
                            <td valign="top">
                                <asp:GridView ID="grdRelatorio" runat="server" CssClass="grid" 
                                    AllowPaging="True" PageSize="25" 
                                    onpageindexchanging="grdRelatorio_PageIndexChanging" 
                                    AutoGenerateColumns="False">
                                    <RowStyle CssClass="grid" HorizontalAlign="Left" />
                                    <Columns>                                        
                                        <asp:BoundField DataField="Ticket (N�m. Ingresso)" HeaderText="Ticket (N�m. Ingresso)" />
                                        <asp:BoundField DataField="Tipo de Ingresso" HeaderText="Tipo de Ingresso" />
                                        <asp:BoundField DataField="Voucher" HeaderText="Voucher" />
                                        <asp:BoundField DataField="Nome do Cliente" HeaderText="Nome do Cliente" />
                                        <asp:BoundField DataField="Cota" HeaderText="Cota" />
                                        <asp:BoundField DataField="Identidade Eletr�nica" HeaderText="Identidade Eletr�nica" />
                                        <asp:BoundField DataField="Data/Hora Entrada" HeaderText="Data/Hora Entrada" />
                                        <asp:BoundField DataField="Respons�vel Leitura" HeaderText="Respons�vel Leitura" />
                                        <asp:BoundField DataField="Catraca" HeaderText="Catraca" />
                                    </Columns>
                                    <HeaderStyle CssClass="grid_header" HorizontalAlign="Left" />
                                    <AlternatingRowStyle CssClass="grid_alternative_row" />
                                    <PagerStyle CssClass="grid_page" />
                                </asp:GridView>
                            </td>
                        </tr>
                                </table>
                            </td>
                        </tr>
                    
                </div>
            </div>
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

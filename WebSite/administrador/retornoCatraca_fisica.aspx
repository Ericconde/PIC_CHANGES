<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master" AutoEventWireup="true"
    CodeFile="retornoCatraca_fisica.aspx.cs" Inherits="administrador_retornoCatraca_fisica" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="cmdExportarExcel" />
            <asp:PostBackTrigger ControlID="cmdProcessar" />
        </Triggers>
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1> Retorno das Catracas (importação da catraca física)</h1>
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
                                        <td rowspan="4" width="40">
                                            &nbsp;</td>
                                        <td rowspan="4">
                                            <asp:Label  ID="lblFormato" runat="server" CssClass="label_explicacao">Formato do arquivo TXT:<br /><br />000805210042240329; 19/01/2017 22:00:08; 1<br />052399651983737586; 19/01/2017 22:00:12; 5<br />052106062444444261; 19/01/2017 22:00:22; 3</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblArquivo" runat="server" CssClass="label" Text="Arquivo da Catraca:" ></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <asp:FileUpload ID="fileDocumento" runat="server" CssClass="textbox" 
                                                Width="300px" BorderColor="black" ForeColor="black" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="cmdProcessar" runat="server" CssClass="botao" 
                                                TabIndex="2" Text="Processar" onclick="cmdProcessar_Click" />
                                            &nbsp;<asp:Button ID="cmdVoltar" runat="server" CssClass="botao" OnClick="cmdVoltar_Click"
                                                TabIndex="3" Text="Voltar" />
                                                &nbsp;<asp:Button ID="cmdExportarExcel" runat="server" CssClass="botao" 
                                                 TabIndex="4" Text="Exportar Excel" onclick="cmdExportarExcel_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="2">
                                            <asp:Label  ID="lblMensagem" runat="server" CssClass="label_alerta"></asp:Label>
                                        </td>
                                    </tr>
                                    </table>
                                    <tr>
                            <td valign="top">
                                <asp:GridView ID="grdRelatorio" runat="server" CssClass="grid" 
                                    AutoGenerateColumns="False">
                                    <RowStyle CssClass="grid" HorizontalAlign="Left" />
                                    <Columns>                                        
                                        <asp:BoundField DataField="Ticket (Núm. Ingresso)" HeaderText="Ticket (Núm. Ingresso)" />
                                        <asp:BoundField DataField="Tipo de Ingresso" HeaderText="Tipo de Ingresso" />
                                        <asp:BoundField DataField="Voucher" HeaderText="Voucher" />
                                        <asp:BoundField DataField="Nome do Cliente" HeaderText="Nome do Cliente" />
                                        <asp:BoundField DataField="Cota" HeaderText="Cota" />
                                        <asp:BoundField DataField="Identidade Eletrônica" HeaderText="Identidade Eletrônica" />
                                        <asp:BoundField DataField="Data/Hora da Entrada" HeaderText="Data/Hora da Entrada" />
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

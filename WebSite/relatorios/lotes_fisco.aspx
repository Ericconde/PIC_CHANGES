<%@ Page Title="" Language="C#" MasterPageFile="~/controls/Fisco.master" AutoEventWireup="true" 
CodeFile="lotes_fisco.aspx.cs" Inherits="relatorios_lotes_fisco" %>

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
                <h1>RELATÓRIO - VALORES DOS INGRESSOS (LOTES)</h1>
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
                                                Width="400px" AutoPostBack="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" 
                                                OnClick="cmdPesquisar_Click" TabIndex="2" Text="Pesquisar" />
                                            &nbsp;<asp:Button ID="cmdExportarExcel" runat="server" CssClass="botao" 
                                                onclick="cmdExportarExcel_Click" TabIndex="2" Text="Exportar Excel" />
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
                                    
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label  ID="lblNumeroRegistros" runat="server" 
                                                CssClass="label_numero_linhas"></asp:Label>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>
                        </tr>
                        
                    </table>

                    <asp:GridView ID="grdLote" runat="server" AutoGenerateColumns="False" 
                        CssClass="grid" DataKeyNames="IDLote">
                        <RowStyle CssClass="grid" HorizontalAlign="left" />
                        <Columns>
                            <asp:BoundField DataField="Lote" HeaderText="Lote" />
                            <asp:BoundField DataField="Início Venda" HeaderText="Início Vendas" />
                            <asp:BoundField DataField="Fim Venda" HeaderText="Fim Vendas" />
                            <asp:BoundField DataField="ValorInteiraCadeiraSocio" 
                                HeaderText="Valor Inteira Cadeira (sócio)" />
                            <asp:BoundField DataField="ValorInteiraCadeiraNaoSocio" 
                                HeaderText="Valor Inteira Cadeira (não sócio)" />
                            <asp:BoundField DataField="ValorInteiraAvulsoSocio" 
                                HeaderText="Valor Inteira sem cadeira (sócio)" />
                            <asp:BoundField DataField="ValorInteiraAvulsoNaoSocio" 
                                HeaderText="Valor Inteira sem cadeira (não sócio)" />
                            <asp:BoundField DataField="ValorMeiaCadeiraSocio" 
                                HeaderText="Valor Meio Adol. Cadeira (sócio)" />
                            <asp:BoundField DataField="ValorMeiaCadeiraNaoSocio" 
                                HeaderText="Valor Meio Adol. Cadeira (não sócio)" />
                            <asp:BoundField DataField="ValorMeiaAvulsoSocio" 
                                HeaderText="Valor Meio Adol. sem cadeira (sócio)" />
                            <asp:BoundField DataField="ValorMeiaAvulsoNaoSocio" 
                                HeaderText="Valor Meio Adol. sem cadeira (não sócio)" />
                            <asp:BoundField DataField="IngressosAvulsos" 
                                HeaderText="Núm. Ingressos Avulsos" />                           
                        </Columns>
                        <HeaderStyle CssClass="grid_header" HorizontalAlign="left" />
                        <AlternatingRowStyle CssClass="grid_alternative_row" />
                    </asp:GridView>
                    
                </div>
            </div>
           
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



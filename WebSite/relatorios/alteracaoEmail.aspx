<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master" AutoEventWireup="true"
    CodeFile="alteracaoEmail.aspx.cs" Inherits="relatorios_AlteracaoEmail" %>

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
                <h1> RELATÓRIO - ALTERAÇÕES DE E-MAIL</h1>
                <div class="texto_box_home_site">
                    <table style="margin-top: 5px;">
                        <tr>
                            <td>
                                <table class="style4">
                                    <tr>
                                        <td>
                                            <asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" 
                                                OnClick="cmdPesquisar_Click" TabIndex="2" Text="Pesquisar" />
                                            &nbsp;<asp:Button ID="cmdVoltar" runat="server" CssClass="botao" 
                                                OnClick="cmdVoltar_Click" TabIndex="3" Text="Voltar" />
                                            &nbsp;<asp:Button ID="cmdExportarExcel" runat="server" CssClass="botao" 
                                                onclick="cmdExportarExcel_Click" TabIndex="4" Text="Exportar Excel" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9">
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
                                        <asp:BoundField DataField="Responsável Alteração" HeaderText="Responsável Alteração" HeaderStyle-BackColor="#4d94c4"/>
                                        <asp:BoundField DataField="Data/Hora Alteração" HeaderText="Data/Hora Alteração" HeaderStyle-BackColor="#4d94c4"/>
                                        <asp:BoundField DataField="E-mail Novo" HeaderText="E-mail Novo" HeaderStyle-BackColor="#4d94c4"/>
                                        <asp:BoundField DataField="E-mail Substituído" HeaderText="E-mail Substituído" HeaderStyle-BackColor="#4d94c4"/>
                                        <asp:BoundField DataField="Nome do Cliente" HeaderText="Nome do Cliente" HeaderStyle-BackColor="#4d94c4"/>
                                        <asp:BoundField DataField="Cota" HeaderText="Cota" HeaderStyle-BackColor="#4d94c4"/>
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

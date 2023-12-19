<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master" AutoEventWireup="true"
    CodeFile="cancelarTicket.aspx.cs" Inherits="financeiro_cancelarTicket" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1>
                    Cancelar Ticket/Ingresso</h1>
                <div class="texto_box_home_site">
                    <table style="margin-top: 5px;">
                        <tr>
                            <td>
                                <table class="style4">
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblVoucher" runat="server" CssClass="label" Text="Núm. Voucher:" Width="52px"></asp:Label>
                                        </td>
                                        <td class="celula_campo" width="600">
                                            <cc2:CustomTextBox ID="txtVoucher" runat="server" CssClass="textbox" MaxLength="6"
                                                TabIndex="1" Width="80px" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)"
                                                onkeyup="mascara(this,soNumeros)"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="2">
                                            <asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" OnClick="cmdPesquisar_Click"
                                                TabIndex="2" Text="Pesquisar" />
                                            &nbsp;<asp:Button ID="cmdSalvar" runat="server" CssClass="botao" 
                                                TabIndex="2" Text="Atualizar" onclick="cmdSalvar_Click" />
                                            &nbsp;<asp:Button ID="cmdVoltar" runat="server" CssClass="botao" OnClick="cmdVoltar_Click"
                                                TabIndex="3" Text="Voltar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label  ID="lblNumeroRegistros" runat="server" CssClass="label_numero_linhas"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7" valign="top">
                                <asp:GridView ID="grdIngressos" runat="server" CssClass="grid" AutoGenerateColumns="False"
                                    DataKeyNames="Ticket" OnRowDataBound="grdIngressos_RowDataBound">
                                    <RowStyle CssClass="grid" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="IDVenda" HeaderText="Voucher" />
                                        <asp:BoundField DataField="Lote" HeaderText="Lote" />
                                        <asp:BoundField DataField="Ticket" HeaderText="Ticket" />
                                        <asp:BoundField DataField="Tipo" HeaderText="Tipo de Ingresso" />
                                        <asp:BoundField DataField="Valor" HeaderText="Valor" />
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:RadioButtonList ID="radStatus" runat="server" CssClass="radioButton" 
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Ativo</asp:ListItem>
                                                    <asp:ListItem Value="0">Inativo</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Ativo" HeaderText="Ativo" />
                                    </Columns>
                                    <HeaderStyle CssClass="grid_header" HorizontalAlign="Left" />
                                    <AlternatingRowStyle CssClass="grid_alternative_row" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

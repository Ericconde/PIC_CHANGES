<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Vendedor.master" AutoEventWireup="true"
    CodeFile="clienteDebito.aspx.cs" Inherits="financeiro_clienteDebito" %>

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
                    Sócio Débito</h1>
                <div class="texto_box_home_site">
                    <table style="margin-top: 5px;">
                        <tr>
                            <td>
                                <table class="style4">
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblNome" runat="server" CssClass="label" Text="Nome:" Width="48px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtNome" runat="server" CssClass="textbox" MaxLength="100"
                                                Width="446px"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="Label1" runat="server" CssClass="label" Text="CPF:" Width="40px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtCPF" runat="server" CssClass="textbox" MaxLength="11" TabIndex="1"
                                                Width="170px"></cc2:CustomTextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCPF"
                                                ValidChars="0,1,2,3,4,5,6,7,8,9">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="Label128" runat="server" CssClass="label" Text="E-mail:" Width="48px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="50"
                                                TabIndex="1" Width="446px"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="Label124" runat="server" CssClass="label" Height="16px" Text="Cota:"
                                                Width="45px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtNumCota" runat="server" CssClass="textbox" MaxLength="6"
                                                Width="120px" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)"
                                                onkeyup="mascara(this,soNumeros)"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="4">
                                            <asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" TabIndex="2" Text="Pesquisar"
                                                OnClick="cmdPesquisar_Click" />
                                            &nbsp;<asp:Button ID="cmdSalvar" runat="server" CssClass="botao" 
                                                onclick="cmdSalvar_Click" TabIndex="2" Text="Atualizar" />
                                            &nbsp;<asp:Button ID="cmdCancelar" runat="server" CssClass="botao" OnClick="cmdCancelar_Click"
                                                TabIndex="3" Text="Cancelar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7" valign="top" align="left">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label  ID="lblNumeroLinhas" runat="server" CssClass="label_numero_linhas"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7" valign="top">
                                <asp:GridView ID="grdCliente" runat="server" CssClass="grid" DataKeyNames="IDUsuario,Debito,Núm. Cota"
                                    Width="127%" AutoGenerateColumns="False" OnRowDataBound="grdCliente_RowDataBound">
                                    <RowStyle CssClass="grid" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:ButtonField DataTextField="Nome" HeaderText="Nome" />
                                        <asp:ButtonField DataTextField="CPF" HeaderText="CPF" />
                                        <asp:ButtonField DataTextField="Email" HeaderText="E-mail" />
                                        <asp:ButtonField DataTextField="Perfil" HeaderText="Perfil" />
                                        <asp:ButtonField DataTextField="Núm. Cota" HeaderText="Núm. Cota" />
                                        <asp:TemplateField HeaderText="Débito">
                                            <ItemTemplate>
                                                <asp:RadioButtonList ID="radDebito" runat="server" CssClass="radioButton" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0">Não</asp:ListItem>
                                                    <asp:ListItem Value="1">Sim</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField DataTextField="Data Cadastro" HeaderText="Data Cadastro" />
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

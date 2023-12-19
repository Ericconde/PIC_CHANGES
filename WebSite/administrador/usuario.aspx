<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master"
    AutoEventWireup="true" CodeFile="usuario.aspx.cs" Inherits="administrador_usuario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script type="text/javascript">
    // Utilizando JQuery
    jQuery('txtEmail').attr('autocomplete', 'off');
    // Ou Javascript padrão
    campo.setAttribute('autocomplete', 'off');
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1>Cadastro de Usuários</h1>
                <div class="texto_box_home_site">
                    <table style="margin-top: 5px;">
                        <tr>
                            <td>
                                <table class="style4">
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="Label125" runat="server" CssClass="label" Text="Perfil&lt;sup&gt;1&lt;/sup&gt;:" 
                                                Width="41px"></asp:Label>
                                        </td>
                                        <td class="celula_campo" colspan="3">
                                            <asp:RadioButtonList ID="radPerfil" runat="server" CssClass="radioButton" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1">Administrador</asp:ListItem>
                                                <asp:ListItem Value="2">Vendedor</asp:ListItem>
                                                <asp:ListItem Value="5">Contabilidade</asp:ListItem>
                                                <asp:ListItem Value="3">Sócio</asp:ListItem>
                                                <asp:ListItem Value="4">Não Sócio</asp:ListItem>
                                                <asp:ListItem Value="6">Clube</asp:ListItem>
                                                <asp:ListItem Value="8">Portaria</asp:ListItem>
                                                <asp:ListItem Value="9">Fisco</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:HiddenField ID="hddIdUsuario" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblNome" runat="server" CssClass="label" Text="Nome&lt;sup&gt;1&lt;/sup&gt;:" 
                                                Width="48px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtNome" runat="server" CssClass="textbox" 
                                                MaxLength="100" Width="446px"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="Label127" runat="server" CssClass="label" Text="Status:" 
                                                Width="45px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <asp:RadioButtonList ID="radStatus" runat="server" CssClass="radioButton" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="1">Ativo</asp:ListItem>
                                                <asp:ListItem Value="0">Inativo</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="Label128" runat="server" CssClass="label" Text="E-mail:" 
                                                Width="48px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <asp:TextBox ID="txtEmailUsuario" runat="server" CssClass="textbox" AutoCompleteType="Disabled" 
                                                MaxLength="50" TabIndex="1" Width="446px" autocomplete="off"></asp:TextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="Label124" runat="server" CssClass="label" Height="16px" 
                                                Text="Senha:" Width="45px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtSenhaUsuario" runat="server" CssClass="textbox" autocomplete="off"
                                                MaxLength="50" TabIndex="2" TextMode="Password" Width="139px"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblNumCota" runat="server" CssClass="label" Text="Cota&lt;sup&gt;1&lt;/sup&gt;:"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtNumCota" runat="server" CssClass="textbox" 
                                                MaxLength="6" onkeydown="mascara(this,soNumeros)" 
                                                onkeypress="mascara(this,soNumeros)" onkeyup="mascara(this,soNumeros)" 
                                                TabIndex="30" Width="80px"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="Label4" runat="server" CssClass="label" Text="Dígito:"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtDigitoCota" runat="server" CssClass="textbox" 
                                                MaxLength="2" TabIndex="31" Width="20px"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label ID="lblCPF" runat="server" CssClass="label" Text="CPF&lt;sup&gt;1&lt;/sup&gt;:"></asp:Label>
                                        </td>
                                        <td class="celula_campo" colspan="3">
                                            <cc2:CustomTextBox ID="txtCPF" runat="server" CssClass="textbox" MaxLength="11" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)" onkeyup="mascara(this,soNumeros)" TabIndex="30" Width="150px"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    
                                </table>
                                <table class="style4">
                                    <tr>
                                        <td class="style9">
                                            <asp:Label  ID="lblOrientacao" runat="server" CssClass="label_numero_linhas"><sup>1</sup>Campos usados para pesquisa</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9">
                                            <asp:Button ID="cmdSalvar" runat="server" CssClass="botao" OnClick="cmdSalvar_Click" TabIndex="2" Text="Salvar" />
                                            &nbsp;<asp:Button ID="cmdCancelar" runat="server" CssClass="botao" OnClick="cmdCancelar_Click" TabIndex="3" Text="Cancelar" />
                                            &nbsp;<asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" onclick="cmdPesquisar_Click" TabIndex="3" Text="Pesquisar" />
                                            <asp:Label ID="lblMensagem" runat="server" CssClass="label_alerta"></asp:Label>
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
                                            <img alt="" src="../images/eraser.png" style="width: 16px; height: 16px" />
                                            <asp:Label  ID="lblEditar" runat="server" CssClass="label_sumario">Editar</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7" valign="top">
                                <asp:GridView ID="grdUsuario" runat="server" CssClass="grid"  
                                    DataKeyNames="IDUsuario" Width="127%" AutoGenerateColumns="False"
                                    OnRowCommand="grdUsuario_RowCommand"  EmptyDataText="Não existe esse Usuario para esse Perfil!"
                                    onpageindexchanging="grdUsuario_PageIndexChanging" PageSize="25" 
                                    AllowPaging="True">
                                    <RowStyle CssClass="grid" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="Nome" HeaderText="Nome" HeaderStyle-BackColor="#4d94c4"/>
                                        <asp:BoundField DataField="Email" HeaderText="E-mail" HeaderStyle-BackColor="#4d94c4"/>
                                        <asp:BoundField DataField="CPF" HeaderText="CPF" HeaderStyle-BackColor="#4d94c4"/>
                                        <asp:BoundField DataField="Perfil" HeaderText="Perfil" HeaderStyle-BackColor="#4d94c4"/>
                                        <asp:BoundField DataField="Cota" HeaderText="Cota" HeaderStyle-BackColor="#4d94c4"/>
                                        <asp:BoundField DataField="Ativo" HeaderText="Ativo" HeaderStyle-BackColor="#4d94c4"/>
                                        <asp:BoundField DataField="Cadastro" HeaderText="Data Cadastro" HeaderStyle-BackColor="#4d94c4"/>
                                        <asp:ButtonField ButtonType="Image" CommandName="Editar" ImageUrl="~/images/eraser.png" HeaderStyle-BackColor="#4d94c4"
                                            Text="Editar" />
                                    </Columns>
                                    <HeaderStyle CssClass="grid_header" HorizontalAlign="Left" />
                                    <AlternatingRowStyle CssClass="grid_alternative_row" />
                                    <PagerStyle CssClass="grid_page" />
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

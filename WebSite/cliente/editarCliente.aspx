<%@ Page Title="PIC" Language="C#"  MasterPageFile="~/controls/Cliente.master"
    AutoEventWireup="true" CodeFile="editarCliente.aspx.cs" Inherits="cliente_editarCliente" %>
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
                    Editar meus Dados</h1>
                <div id="conteudo">
                    <table>
                        <tr>
                            <td>
                                
                                <div class="bg_titulo titulo_claro" style="width: 600px;">
                                    <div class="titulo_left bg_title_color1">
                                        <asp:Label  ID="Label9" runat="server" Text="Dados Pessoais"></asp:Label></div>
                                </div>
                                <table>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblNome" runat="server" CssClass="label" Text="Nome:" Width="47px"></asp:Label>
                                        </td>
                                        <td  class="celula_campo">
                                            <cc2:CustomTextBox ID="txtNome" runat="server" CssClass="textbox" MaxLength="70"
                                                Width="500px" TabIndex="1" ReadOnly="True"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblRG" runat="server" CssClass="label" Text="RG:" Width="76px"></asp:Label>
                                        </td>
                                        <td  class="celula_campo">
                                            <cc2:CustomTextBox ID="txtRG" runat="server" CssClass="textbox" MaxLength="20" TabIndex="2"
                                                Width="265px" ReadOnly="True"></cc2:CustomTextBox>
                                            <asp:Label  ID="Label1" runat="server" CssClass="label" Text="CPF:" Width="40px"></asp:Label>
                                            <cc2:CustomTextBox ID="txtCPF" runat="server" CssClass="textbox" MaxLength="11" TabIndex="3"
                                                Width="170px" ReadOnly="True"></cc2:CustomTextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCPF"
                                                ValidChars="0,1,2,3,4,5,6,7,8,9">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblDataNasc" runat="server" CssClass="label" Text="Data de Nascimento"
                                                Width="84px"></asp:Label>
                                        </td>
                                        <td  class="celula_campo">
                                            <cc2:CustomTextBox ID="txtDataNascimento" runat="server" CssClass="textbox" Style="text-align: left"
                                                onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" onkeyup="mascara(this,data)"
                                                MaxLength="10" Width="75px" TabIndex="4" ReadOnly="True"></cc2:CustomTextBox>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblTelefone1" runat="server" CssClass="label" Text="Tel. Residencial:"
                                                Width="78px"></asp:Label>
                                        </td>
                                        <td  class="celula_campo">
                                            <cc2:CustomTextBox ID="txtTelefoneResidencial" runat="server" Width="80px" CssClass="textbox"
                                                MaxLength="15" onkeydown="mascara(this,telefone)" onkeypress="mascara(this,telefone)"
                                                onkeyup="mascara(this,telefone)" TabIndex="5" ReadOnly="True"></cc2:CustomTextBox>
                                            <asp:Label  ID="lblTelefoneComercial" runat="server" CssClass="label" Text="Tel. Comercial:"
                                                Width="140px"></asp:Label>
                                            <cc2:CustomTextBox ID="txtTelefoneComercial" runat="server" Width="80px" CssClass="textbox"
                                                MaxLength="15" onkeydown="mascara(this,telefone)" onkeypress="mascara(this,telefone)"
                                                onkeyup="mascara(this,telefone)" TabIndex="6" ReadOnly="True"></cc2:CustomTextBox>
                                            <asp:Label  ID="lblCelular" runat="server" CssClass="label" Text="Celular:" Width="65px"></asp:Label>
                                            <cc2:CustomTextBox ID="txtTelefoneCelular" runat="server" Width="80px" CssClass="textbox"
                                                MaxLength="15" onkeydown="mascara(this,telefone)" onkeypress="mascara(this,telefone)"
                                                onkeyup="mascara(this,telefone)" TabIndex="7" ReadOnly="True"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div class="bg_titulo titulo_claro" style="width: 600px;">
                                    <div class="titulo_left bg_title_color1">
                                        <asp:Label  ID="Label2" runat="server" Text="Endereço"></asp:Label></div>
                                </div>
                                <table>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblCEP" runat="server" CssClass="label" Text="CEP:" Width="84px"></asp:Label>
                                        </td>
                                        <td  class="celula_campo">
                                            <cc2:CustomTextBox ID="txtCEP" runat="server" CssClass="textbox" MaxLength="8" TabIndex="8"
                                                Width="100px" ReadOnly="True"></cc2:CustomTextBox>
                                            <asp:Label  ID="lblLogradouro" runat="server" CssClass="label" Text="Logradouro:"
                                                Width="85px"></asp:Label>
                                            <cc2:CustomTextBox ID="txtLogradouro" runat="server" CssClass="textbox" MaxLength="50"
                                                TabIndex="9" Width="290px" ReadOnly="True"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblNumero" runat="server" CssClass="label" Text="Número:" Width="84px"></asp:Label>
                                        </td>
                                        <td  class="celula_campo">
                                            <cc2:CustomTextBox ID="txtNumero" runat="server" CssClass="textbox" MaxLength="6"
                                                TabIndex="5" Width="100px" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)" 
                                onkeyup="mascara(this,soNumeros)" ReadOnly="True"></cc2:CustomTextBox>
                                            <asp:Label  ID="lblComplemento" runat="server" CssClass="label" Text="Complemento:"
                                                Width="100px"></asp:Label>
                                            <cc2:CustomTextBox ID="txtComplemento" runat="server" CssClass="textbox" MaxLength="50"
                                                TabIndex="11" Width="275px" ReadOnly="True"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblBairro" runat="server" CssClass="label" Text="Bairro:" Width="84px" ></asp:Label>
                                        </td>
                                        <td  class="celula_campo">
                                            <cc2:CustomTextBox ID="txtBairro" runat="server" CssClass="textbox" MaxLength="25"
                                                TabIndex="12" Width="500px" ReadOnly="True"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblEstado" runat="server" CssClass="label" Text="Estado:" Width="84px"></asp:Label>
                                        </td>
                                        <td  class="celula_campo">
                                            <asp:DropDownList ID="dropEstado" runat="server" CssClass="dropdown" Height="24px"
                                                Width="513px" AutoPostBack="True" 
                                                OnSelectedIndexChanged="dropEstado_SelectedIndexChanged" TabIndex="13" Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblCidade" runat="server" CssClass="label" Text="Cidade:" Width="84px"></asp:Label>
                                        </td>
                                        <td  class="celula_campo">
                                            <asp:DropDownList ID="dropCidade" runat="server" CssClass="dropdown" Height="24px"
                                                Width="513px" TabIndex="14" Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    </table>

                                    <div class="bg_titulo titulo_claro" style="width: 600px;">
                                    <div class="titulo_left bg_title_color1">
                                        <asp:Label  ID="Label3" runat="server" Text="Dados de Acesso"></asp:Label></div>
                                </div>
                                <table>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="Label128" runat="server" CssClass="label" Text="E-mail:" Width="84px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="200"
                                                TabIndex="15" Width="500px" ReadOnly="True"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    </table>


                                    <tr>
                                        <td colspan="2">
                                            &nbsp;<asp:Button ID="cmdCancelar" runat="server" CssClass="botao" 
                                                OnClick="cmdCancelar_Click" TabIndex="16" Text="Voltar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label  ID="lblMensagem0" runat="server" CssClass="label_sumario" 
                                                Text="Caso deseja alterar seu cadastro de não sócio para sócio, entre em contato com Clube."></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label  ID="lblMensagem" runat="server" CssClass="label_alerta"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            </div>
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

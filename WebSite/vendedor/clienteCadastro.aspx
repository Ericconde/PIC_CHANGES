<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Vendedor.master" AutoEventWireup="true"
    CodeFile="clienteCadastro.aspx.cs" Inherits="vendedor_clienteCadastro" %>

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
                    Cadastro - Cliente</h1>
                <div id="conteudo">
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="radPerfil" runat="server" CssClass="radiobutton" 
                                                RepeatDirection="Horizontal" AutoPostBack="True" Height="25px" 
                                                onselectedindexchanged="radPerfil_SelectedIndexChanged" Width="157px">
                                                <asp:ListItem Value="4">Não Sócio</asp:ListItem>
                                                <asp:ListItem Value="3" Selected="True">Sócio</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" Visible="True">
                                        <ContentTemplate>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblNumCota" runat="server" CssClass="label" Text="Núm. da Cota" Width="84px"></asp:Label>
                                        </td>
                                        <td class="celula_campo" >
                                            <cc2:CustomTextBox ID="txtNumCota_pesquisa" runat="server" CssClass="textbox" MaxLength="6"
                                                Width="120px" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)" 
                                onkeyup="mascara(this,soNumeros)"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="Label4" runat="server" CssClass="label" Text="Dígito da Cota:" ></asp:Label>
                                        </td>
                                        <td class="celula_campo" >
                                            <cc2:CustomTextBox ID="txtDigitoCota_pesquisa" runat="server" CssClass="textbox" MaxLength="2"
                                                Width="20px"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="Label124" runat="server" CssClass="label" Text="CPF:" 
                                                Width="37px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtCPF_pesquisa" runat="server" CssClass="textbox" 
                                                MaxLength="11" TabIndex="1" Width="160px"></cc2:CustomTextBox>
                                            <cc1:FilteredTextBoxExtender ID="ftbCPF" runat="server" 
                                                TargetControlID="txtCPF_pesquisa" ValidChars="0,1,2,3,4,5,6,7,8,9">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td class="celula_campo">
                                            <asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" 
                                                OnClick="cmdPesquisar_Click" TabIndex="2" Text="Pesquisar" />
                                        </td>
                                    </tr>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label  ID="lblMensagemPesquisa" runat="server" CssClass="label_alerta"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div class="bg_titulo titulo_claro" style="width: 600px;">
                                    <div class="titulo_left bg_title_color1">
                                        <asp:Label  ID="Label9" runat="server" Text="Dados Pessoais"></asp:Label></div>
                                </div>
                                <table>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblNome" runat="server" CssClass="label" Text="Nome:" Width="47px"></asp:Label>
                                        </td>
                                        <td  class="celula_campo" colspan="5">
                                            <cc2:CustomTextBox ID="txtNome" runat="server" CssClass="textbox" MaxLength="200"
                                                Width="500px"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblRG" runat="server" CssClass="label" Text="RG:" Width="76px"></asp:Label>
                                        </td>
                                        <td  class="celula_campo">
                                            <cc2:CustomTextBox ID="txtRG" runat="server" CssClass="textbox" MaxLength="20" TabIndex="1"
                                                Width="141px"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="Label1" runat="server" CssClass="label" Text="CPF:" Width="40px"></asp:Label>
                                        </td>
                                        <td class="celula_campo" colspan="3">
                                            <cc2:CustomTextBox ID="txtCPF" runat="server" CssClass="textbox" MaxLength="11" 
                                                TabIndex="1" Width="170px"></cc2:CustomTextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" 
                                                TargetControlID="txtCPF" ValidChars="0,1,2,3,4,5,6,7,8,9">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblDataNasc" runat="server" CssClass="label" Text="Data de Nascimento"
                                                Width="84px"></asp:Label>
                                        </td>
                                        <td  class="celula_campo" colspan="5">
                                            <cc2:CustomTextBox ID="txtDataNascimento" runat="server" CssClass="textbox" Style="text-align: left"
                                                onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" onkeyup="mascara(this,data)"
                                                MaxLength="10" Width="75px"></cc2:CustomTextBox>
                                            <cc1:CalendarExtender ID="txtDataNascimento_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtDataNascimento">
                                            </cc1:CalendarExtender>
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
                                                onkeyup="mascara(this,telefone)"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblTelefoneComercial" runat="server" CssClass="label" 
                                                Text="Tel. Comercial:" Width="107px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtTelefoneComercial" runat="server" CssClass="textbox" 
                                                MaxLength="15" onkeydown="mascara(this,telefone)" 
                                                onkeypress="mascara(this,telefone)" onkeyup="mascara(this,telefone)" 
                                                Width="80px"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblCelular" runat="server" CssClass="label" Text="Celular:" 
                                                Width="56px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtTelefoneCelular" runat="server" CssClass="textbox" 
                                                MaxLength="15" onkeydown="mascara(this,telefone)" 
                                                onkeypress="mascara(this,telefone)" onkeyup="mascara(this,telefone)" 
                                                Width="80px"></cc2:CustomTextBox>
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
                                            <cc2:CustomTextBox ID="txtCEP" runat="server" CssClass="textbox" MaxLength="8" TabIndex="1"
                                                Width="100px"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblLogradouro" runat="server" CssClass="label" 
                                                Text="Logradouro:" Width="85px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtLogradouro" runat="server" CssClass="textbox" 
                                                MaxLength="200" TabIndex="1" Width="290px"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblNumero" runat="server" CssClass="label" Text="Número:" Width="84px"></asp:Label>
                                        </td>
                                        <td  class="celula_campo">
                                            <cc2:CustomTextBox ID="txtNumero" runat="server" CssClass="textbox" MaxLength="6"
                                                TabIndex="1" Width="100px" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)" 
                                onkeyup="mascara(this,soNumeros)"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblComplemento" runat="server" CssClass="label" 
                                                Text="Complemento:" Width="100px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtComplemento" runat="server" CssClass="textbox" 
                                                MaxLength="50" TabIndex="1" Width="275px"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblBairro" runat="server" CssClass="label" Text="Bairro:" Width="84px" ></asp:Label>
                                        </td>
                                        <td  class="celula_campo" colspan="3">
                                            <cc2:CustomTextBox ID="txtBairro" runat="server" CssClass="textbox" MaxLength="200"
                                                TabIndex="1" Width="500px"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblEstado" runat="server" CssClass="label" Text="Estado:" Width="84px"></asp:Label>
                                        </td>
                                        <td  class="celula_campo" colspan="3">
                                            <asp:DropDownList ID="dropEstado" runat="server" CssClass="dropdown" Height="24px"
                                                Width="513px" AutoPostBack="True" OnSelectedIndexChanged="dropEstado_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblCidade" runat="server" CssClass="label" Text="Cidade:" Width="84px"></asp:Label>
                                        </td>
                                        <td  class="celula_campo" colspan="3">
                                            <asp:DropDownList ID="dropCidade" runat="server" CssClass="dropdown" Height="24px"
                                                Width="513px">
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
                                                TabIndex="1" Width="345px"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblSenha" runat="server" CssClass="label" Text="Senha:" Width="45px"></asp:Label>
                                        </td>
                                        <td  class="celula_campo">
                                            <cc2:CustomTextBox ID="txtSenha" runat="server" CssClass="textbox" MaxLength="30"
                                                TabIndex="1" Width="100px" TextMode="Password"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Button ID="cmdNovo" runat="server" CssClass="botao" OnClick="cmdNovo_Click"
                                                TabIndex="2" Text="Salvar" /> 
                                            &nbsp;<asp:Button ID="cmdCancelar" runat="server" CssClass="botao" OnClick="cmdCancelar_Click"
                                                TabIndex="3" Text="Cancelar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
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

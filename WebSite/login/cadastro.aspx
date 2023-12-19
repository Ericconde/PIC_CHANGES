<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Site.master" AutoEventWireup="true"
    CodeFile="cadastro.aspx.cs" Inherits="cadastro_usuario" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <head runat="server" />
    <style>
        .Margin {
            margin-top: 10px;
        }

        .card {
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
            transition: 0.3s;
            border-radius: 5px; /* 5px rounded corners */
        }

        /* Add rounded corners to the top left and the top right corner of the image */
        img {
            border-radius: 5px 5px 0 0;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <center>
                    <h1><b>CADASTRE-SE</b></h1>
                    <br />
                    <div id="conteudo">
                        <table>
                            <tr>
                                <td>
                                    <div style="background-color: #d7e7f2; margin-bottom: -0.6vh;">
                                        <br />
                                        <table style="font-family: Calibri; margin-left: 5px; margin-right: 5px">
                                            <tr>
                                                <td>
                                                    <asp:RadioButtonList ID="radPerfil" runat="server" CssClass="radiobutton"
                                                        RepeatDirection="Horizontal" AutoPostBack="True" Height="25px"
                                                        OnSelectedIndexChanged="radPerfil_SelectedIndexChanged" Width="157px">
                                                        <asp:ListItem Value="4">Não Sócio</asp:ListItem>
                                                        <asp:ListItem Value="3" Selected="True">Sócio</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" Visible="True">
                                                <ContentTemplate>
                                                    <tr>
                                                        <td style="width: 150px" class="Margin">
                                                            <asp:Label ID="lblNumCota" runat="server" Text="Núm. da Cota:" Width="150px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <cc2:CustomTextBox ID="txtNumCota_pesquisa" runat="server" CssClass="textbox"
                                                                Width="100px" Height="30px" Style="border-color: gray; border-radius: 3px;" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)"
                                                                onkeyup="mascara(this,soNumeros)"></cc2:CustomTextBox>
                                                            &nbsp
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Dígito da Cota:"></asp:Label>
                                                            &nbsp
                                                        </td>
                                                        <td>
                                                            <cc2:CustomTextBox ID="txtDigitoCota_pesquisa" runat="server"
                                                                CssClass="textbox"
                                                                Height="30px" Style="border-color: gray; border-radius: 3px;"
                                                                MaxLength="2"
                                                                Width="40px"></cc2:CustomTextBox>
                                                        </td>
                                                        <td>&nbsp
                                                        <asp:Label ID="Label124" runat="server" Text="CPF:"
                                                            Width="37px"></asp:Label>
                                                            &nbsp
                                                        </td>
                                                        <td>
                                                            <cc2:CustomTextBox ID="txtCPF_pesquisa" runat="server" CssClass="textbox"
                                                                Height="30px" Style="border-color: gray; border-radius: 3px;"
                                                                MaxLength="11" TabIndex="1" Width="160px"></cc2:CustomTextBox>
                                                            <cc1:FilteredTextBoxExtender ID="ftbCPF" runat="server"
                                                                TargetControlID="txtCPF_pesquisa" ValidChars="0,1,2,3,4,5,6,7,8,9">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                        <td>&nbsp
                                                        <asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" TabIndex="2" Style="color: white; background-color: green; border-color: white; border: solid 1px white; border-radius: 10px; text-align: center; width: 100px; height: 30px"
                                                            OnClick="cmdPesquisar_Click" Text="Pesquisar" />
                                                        </td>
                                                    </tr>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Label ID="lblMensagemPesquisa" runat="server" CssClass="label_alerta"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </div>
                                    <center>
                                        <br />

                                        <div class="bg_titulo titulo_claro" style="width: 600px">
                                            <div class="titulo_left bg_title_color1">
                                                <asp:Label ID="Label9" runat="server" Font-Size="25px" Font-Names="Calibri" Font-Bold="true" Text="DADOS PESSOAIS"></asp:Label>
                                            </div>
                                        </div>
                                        <br />
                                    </center>
                                    <div style="background-color: #d7e7f2; margin-bottom: -0.6vh;">
                                        <br />
                                        <table style="margin-left: 5px">
                                            <tr>
                                                <td class="Margin">
                                                    <asp:Label ID="lblNome" runat="server" CssClass="label" Text="Nome:" Width="47px"></asp:Label>
                                                </td>
                                                <td colspan="5">
                                                    <cc2:CustomTextBox ID="txtNome" runat="server" CssClass="textbox"
                                                        Height="30px" Style="border-color: gray; border-radius: 3px;" MaxLength="200"
                                                        Width="505px"></cc2:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td class="Margin">

                                                    <asp:Label ID="lblRG" runat="server" CssClass="label" Text="RG:" Width="76px"></asp:Label>
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtRG" runat="server" CssClass="textbox"
                                                        Height="30px" Style="border-color: gray; border-radius: 3px;" MaxLength="20" TabIndex="1"
                                                        Width="141px"></cc2:CustomTextBox>
                                                </td>
                                                <td>&nbsp
                                                &nbsp
                                                &nbsp
                                                &nbsp
                                               
                                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="CPF:" Width="40px"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <cc2:CustomTextBox ID="txtCPF" runat="server" CssClass="textbox" Width="230px"
                                                        Height="30px" Style="border-color: gray; border-radius: 3px;" MaxLength="11"
                                                        TabIndex="1"></cc2:CustomTextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                        TargetControlID="txtCPF" ValidChars="0,1,2,3,4,5,6,7,8,9">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDataNasc" runat="server" CssClass="label" Text="Data de Nascimento:"></asp:Label>
                                                    &nbsp
                                                </td>
                                                <td colspan="5">
                                                    <cc2:CustomTextBox ID="txtDataNascimento" runat="server" CssClass="textbox"
                                                        Height="30px" Style="border-color: gray; border-radius: 3px;"
                                                        onkeydown="mascara(this,data)" onkeypress="mascara(this,data)" onkeyup="mascara(this,data)"
                                                        MaxLength="10"></cc2:CustomTextBox>
                                                    <cc1:CalendarExtender ID="txtDataNascimento_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDataNascimento">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTelefone1" runat="server" CssClass="label" Text="Tel. Residencial:"></asp:Label>
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtTelefoneResidencial" runat="server" CssClass="textbox"
                                                        Height="30px" Style="border-color: gray; border-radius: 3px;" Width="110px"
                                                        MaxLength="15" onkeydown="mascara(this,telefone)" onkeypress="mascara(this,telefone)"
                                                        onkeyup="mascara(this,telefone)"></cc2:CustomTextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTelefoneComercial" runat="server" CssClass="label"
                                                        Text="Tel. Comercial:"></asp:Label>
                                                    &nbsp
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtTelefoneComercial" runat="server" CssClass="textbox"
                                                        Height="30px" Style="border-color: gray; border-radius: 3px;"
                                                        MaxLength="15" onkeydown="mascara(this,telefone)"
                                                        onkeypress="mascara(this,telefone)" onkeyup="mascara(this,telefone)"
                                                        Width="110px"></cc2:CustomTextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCelular" runat="server" CssClass="label" Text="Celular:"
                                                        Width="56px"></asp:Label>
                                                    &nbsp
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtTelefoneCelular" runat="server" CssClass="textbox"
                                                        Height="30px" Style="border-color: gray; border-radius: 3px;"
                                                        MaxLength="15" onkeydown="mascara(this,telefone)"
                                                        onkeypress="mascara(this,telefone)" onkeyup="mascara(this,telefone)"
                                                        Width="110px"></cc2:CustomTextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </div>
                                    <center>
                                        <br />
                                        <div class="bg_titulo titulo_claro" style="width: 600px">
                                            <div class="titulo_left bg_title_color1">
                                                <asp:Label ID="Label2" runat="server" Font-Size="25px" Font-Names="Calibri" Font-Bold="true" Text="ENDEREÇO"></asp:Label>
                                            </div>
                                        </div>
                                        <br />
                                    </center>
                                    <div style="background-color: #d7e7f2; margin-bottom: -0.6vh;">
                                        <br />
                                        <table style="margin-left: 5px">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCEP" runat="server" CssClass="label" Text="CEP:" Width="84px"></asp:Label>
                                                </td>
                                                <td>

                                                    <cc2:CustomTextBox ID="txtCEP" runat="server" CssClass="textbox"
                                                        Height="30px" Style="border-color: gray; border-radius: 3px;" MaxLength="8" TabIndex="1"></cc2:CustomTextBox>
                                                    &nbsp
                                                &nbsp
                                                &nbsp
                                                &nbsp
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLogradouro" runat="server" CssClass="label"
                                                        Text="Logradouro:" Width="85px"></asp:Label>
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtLogradouro" runat="server" CssClass="textbox"
                                                        Height="30px" Style="border-color: gray; border-radius: 3px;"
                                                        MaxLength="200" TabIndex="1"></cc2:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblNumero" runat="server" CssClass="label" Text="Número:" Width="84px"></asp:Label>
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtNumero" runat="server" CssClass="textbox"
                                                        Height="30px" Style="border-color: gray; border-radius: 3px;" MaxLength="6"
                                                        TabIndex="1" Width="100px" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)"
                                                        onkeyup="mascara(this,soNumeros)"></cc2:CustomTextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblComplemento" runat="server" CssClass="label"
                                                        Text="Complemento:"></asp:Label>
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtComplemento" runat="server" CssClass="textbox"
                                                        Height="30px" Style="border-color: gray; border-radius: 3px;"
                                                        MaxLength="50" TabIndex="1"></cc2:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblBairro" runat="server" CssClass="label" Text="Bairro:" Width="84px"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <cc2:CustomTextBox ID="txtBairro" runat="server" CssClass="textbox"
                                                        Height="30px" Style="border-color: gray; border-radius: 3px;" MaxLength="200"
                                                        TabIndex="1"></cc2:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEstado" runat="server" CssClass="label" Text="Estado:" Width="84px"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:DropDownList ID="dropEstado" runat="server" CssClass="dropdown" Height="24px"
                                                        Width="513px" AutoPostBack="True" OnSelectedIndexChanged="dropEstado_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCidade" runat="server" CssClass="label" Text="Cidade:" Width="84px"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:DropDownList ID="dropCidade" runat="server" CssClass="dropdown" Height="24px"
                                                        Width="513px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </div>
                                    <center>
                                        <br />
                                        <div class="bg_titulo titulo_claro" style="width: 600px;">
                                            <div class="titulo_left bg_title_color1">
                                                <asp:Label Font-Size="25px" Font-Names="Calibri" ID="Label3" runat="server" Font-Bold="true" Text="DADOS DE ACESSO"></asp:Label>
                                            </div>
                                        </div>
                                        <br />
                                    </center>
                                    <div style="background-color: #d7e7f2; margin-bottom: -0.6vh;">
                                        <br />
                                        <table style="margin-left: 5px">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label128" runat="server" CssClass="label" Text="E-mail:" Width="84px"></asp:Label>
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtEmail" runat="server" CssClass="textbox"
                                                        Height="30px" Style="border-color: gray; border-radius: 3px;" MaxLength="200"
                                                        TabIndex="1" Width="345px"></cc2:CustomTextBox>
                                                    &nbsp
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSenha" runat="server" CssClass="label" Text="Senha:"></asp:Label>
                                                    &nbsp
                                                </td>
                                                <td>
                                                    <cc2:CustomTextBox ID="txtSenha" runat="server" CssClass="textbox"
                                                        Height="30px" Style="border-color: gray; border-radius: 3px;" MaxLength="30"
                                                        TabIndex="1" Width="180px" TextMode="Password"></cc2:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Label ID="lblMensagem" runat="server" CssClass="label_alerta"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="cmdNovo" runat="server" CssClass="botao" TabIndex="2" Style="color: white; margin-top: 2vh; background-color: dodgerblue; border-color: white; border: solid 1px white; border-radius: 10px; text-align: center; width: 150px; height: 30px" OnClick="cmdNovo_Click"
                            Text="Salvar" />
                        &nbsp;<asp:Button ID="cmdCancelar" runat="server" CssClass="botao" Style="color: white; margin-top: 2vh; background-color: red; border-color: white; border: solid 1px white; border-radius: 10px; text-align: center; width: 150px; height: 30px" OnClick="cmdCancelar_Click"
                            TabIndex="3" Text="Cancelar" />
                    </div>
                </center>
                <!--FECHAMENTO DA DIV BOX HOME-->
                <script>
                    function PopUp(string1, string2) {
                        Swal.fire(
                            string1,
                            '',
                            string2
                        )
                    }
                </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

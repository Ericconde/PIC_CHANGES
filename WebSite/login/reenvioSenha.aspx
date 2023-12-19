<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Site.master" AutoEventWireup="true"
    CodeFile="reenvioSenha.aspx.cs" Inherits="login_reenvioSenha" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div style="font-family: Verdana" class="box_home">
                <div class="texto_box_home_site">
                    <center>
                        <h1 style="font-family: Calibri"><b>RECUPERAR SENHA</b></h1>
                    </center>
                    <center>
                        <div style="background-color: #d7e7f2; margin-bottom: -0.6vh;">
                            <table style="margin-top: 30px;">
                                <tr>
                                    <td>
                                        <br />
                                        <table>
                                            <tr>
                                                <td style="font-family: Verdana" class="celula_nome_campo">
                                                    <asp:Label ID="Label1" runat="server" Text="E-mail informado no seu cadastro:"></asp:Label>
                                                </td>
                                                <td class="celula_campo">
                                                    <cc2:CustomTextBox ID="txtEmail" runat="server" CssClass="textbox"
                                                        Width="300px" Height="30px" Style="margin-top: 1vh; border-color: gray; border-radius: 3px;"></cc2:CustomTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="celula_nome_campo">
                                                    <asp:Label ID="Label23" runat="server"
                                                        Text="Digite o texto da imagem abaixo no campo:"></asp:Label></td>
                                                <td class="celula_campo"></td>
                                                <td class="td">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="row form-group">
                                                    <cc3:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low"
                                                        CaptchaHeight="60" CaptchaLength="5" CaptchaLineNoise="None" 
                                                        CaptchaMaxTimeout="240" CaptchaMinTimeout="5" CaptchaWidth="300"
                                                        FontColor="#529E00" />
                                                    <asp:ImageButton Width="40px" Height="30px" ImageUrl="~/images/refreshpic.png" runat="server" CausesValidation="false" />                                                    
                                                </td>
                                                <td class="celula_campo">&nbsp
                                                    &nbsp
                                                    &nbsp
                                                    <asp:TextBox ID="txtCaptcha" runat="server" CssClass="textbox"
                                                        Width="300px" Height="30px" Style="margin-top: 1vh; border-color: gray; border-radius: 3px;"></asp:TextBox>
                                                </td>
                                                <td class="td">&nbsp;</td>
                                            </tr>
                                        </table>
                                        <center>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </center>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="left">
                                        <asp:Label ID="lblMensagem" runat="server" CssClass="label_alerta"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <center>
                            <asp:Button ID="cmdReenviar" runat="server" CssClass="botao" TabIndex="2" Style="color: white; margin-top: 2vh; background-color: dodgerblue; border-color: white; border: solid 1px white; border-radius: 10px; text-align: center; width: 200px; height: 30px" Text="ENVIAR"
                                OnClick="cmdReenviar_Click" />
                            &nbsp;
                        </center>
                    </center>
                </div>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />



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

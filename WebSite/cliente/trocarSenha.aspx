<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Cliente.master"
    AutoEventWireup="true" CodeFile="trocarSenha.aspx.cs" Inherits="cliente_trocarSenha" %>

<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style7 {
            width: 34px;
            height: 30px;
        }

        .auto-style8 {
            background-color: #c5cbce;
            border-bottom: 2px solid #fff;
            height: 34px;
            width: 165px;
        }

        .auto-style9 {
            background-color: #c5cbce;
            border-bottom: 2px solid #fff;
            width: 165px;
        }

        .auto-style10 {
            background-color: #dee1e3;
            border-bottom: 2px solid #fff;
            width: 165px;
        }

        .auto-style11 {
            width: 165px
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <!--ABERTURA DA DIV BOX HOME-->
            <div style="margin-bottom: 61px;">
                <center>
                    
                    <h1 style="margin-bottom: 4vh;font-family:Calibri"><b>TROCAR SENHA</b></h1>
                    <div style="background-color: #d7e7f2; margin-bottom: -0.6vh;">
                        <br />
                        <div class="texto_box_home_site">
                            <table class="style4">
                                <tr>
                                    <td style="width: 200px">
                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Senha Atual:" Style=" font-size: 14px; margin-left: -10px" Font-Bold="true"></asp:Label>

                                    </td>
                                    <td>
                                        <cc2:CustomTextBox ID="txtSenhaAtual" runat="server" CssClass="textbox" TextMode="Password"
                                            Width="300px" Height="30px" Style="margin-top: 1vh; border-color: gray; border-radius: 2px;"></cc2:CustomTextBox>
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
                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Nova Senha:" Style=" font-size: 14px; margin-left: -10px" Font-Bold="true"></asp:Label>
                                    </td>

                                    <td>
                                        <cc2:CustomTextBox ID="txtNovaSenha" runat="server" CssClass="textbox" TextMode="Password"
                                            Width="300px" Height="30px" Style="background-color: white; border-color: gray; border-radius: 3px;"></cc2:CustomTextBox>
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
                                        <asp:Label ID="Label5" runat="server" CssClass="label"
                                            Text="Confirmar Nova Senha:" Width="100%" Style="margin-top: 1vh; font-size: 14px; margin-left: -10px" Font-Bold="true"></asp:Label>
                                    </td>

                                    <td>
                                        <cc2:CustomTextBox ID="txtConfNovaSenha" runat="server" CssClass="textbox" TextMode="Password"
                                            Width="300px" Height="30px" Style="margin-top: 1vh; border-color: gray; border-radius: 3px;"></cc2:CustomTextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2" class="auto-style11"></td>
                                </tr>

                                <tr>
                                    <td colspan="2" class="auto-style11">
                                        <asp:Label ID="lblMensagem" BorderWidth="0" runat="server" CssClass="label_alerta"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <center>
                        <asp:Button ID="cmdSalvar" runat="server" CssClass="botao"
                            OnClick="cmdSalvar_Click" Text="Salvar" Style="color: white; margin-top: 2vh; background-color: dodgerblue; border-color: white; border-radius: 10px; border: solid 1px white; text-align: center; width: 100px; height: 40px" />
                    </center>
                </center>
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
                <br />
               
                <br />
                 <script>
                    function PopUp(string1, string2) {
                        Swal.fire(
                            string1,
                            '',
                            string2
                        )
                    }
                 </script>

                <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

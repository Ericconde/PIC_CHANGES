<%@ Page Title="PIC" Language="C#"  MasterPageFile="~/controls/Cliente.master"
    AutoEventWireup="true" CodeFile="consultarIngressos.aspx.cs" Inherits="cliente_consultarIngressos" %>
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
                    Consultar - Ingressos QRCode</h1>
                <div id="conteudo">
                    <table>
                        <tr>
                            <td>
                                
                                <div class="bg_titulo titulo_claro" style="width: 450px;">
                                    <div class="titulo_left bg_title_color1">
                                        <asp:Label  ID="Label9" runat="server" Text="Venda"></asp:Label></div>
                                </div>
                                <table>
                                    <tr>
                                        <td class="celula_nome_campo" style="width: 100px;">
                                            <asp:Label  ID="lblNome" runat="server" CssClass="label" Text="Voucher:" Width="20px"></asp:Label>
                                        </td>
                                        <td  class="celula_campo"  style="width: 450px; padding-left: 10px; padding-top: 5px;">
                                            <asp:DropDownList ID="DropVendas" runat="server" CssClass="dropdown" Height="24px"
                                                Width="350px" AutoPostBack="True" 
                                                onselectedindexchanged="DropVendas_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>


                                <div class="bg_titulo titulo_claro" style="width: 450px;">
                                    <div class="titulo_left bg_title_color1">
                                        <asp:Label  ID="lblQRCode" runat="server" Text="QR CODE"></asp:Label></div>
                                </div>
                                <table>
                                    <tr>
                                         <td colspan="2"  class="celula_campo" style="width: 490px; padding: 10px 10px 10px 30px;">
                                           <asp:Label  ID="Label4" runat="server" CssClass="label" Text="Aproxime o aparelho ao QR CODE para realizar a leitura:"></asp:Label>
                                         </td>
                                    </tr>
                                    <tr>
                                    <td colspan="2">
                                    <asp:Repeater ID="rptQRCode" runat="server">
                                        <ItemTemplate>
                                        <br />
                                            <div align="center" style="min-width: 420px; border-width: 2px; border-style: dashed; padding-top: 5px; padding-bottom: 5px;">
                                                <asp:Image ID="imgQrCode" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "QRUrl")%>' /><br />
                                                <asp:Label  ID="lblNomeTicket" runat="server" CssClass="label" Text="Ticket:"></asp:Label>
                                                <asp:Label  ID="lblValorTicket" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "Ticket")%>'></asp:Label><br />
                                                <asp:Label  ID="Label2" runat="server" CssClass="label" Text="Tipo:"></asp:Label>
                                                <asp:Label  ID="Label3" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "Tipo")%>'></asp:Label>
                                            </div>
                                            </ItemTemplate>
                                    </asp:Repeater>
                                    </td>
                                    </tr>
                                </table>

                                <div class="bg_titulo titulo_claro" style="width: 450px;">
                                    <div class="titulo_left bg_title_color1">
                                        <asp:Label  ID="Label1" runat="server" Text="Enviar Ingressos"></asp:Label></div>
                                </div>
                                <table>
                                    <tr>
                                        <td colspan="2" class="celula_campo" width="450">
                                            <asp:CheckBoxList ID="chkIngressos" runat="server" CssClass="chekBoxList" RepeatColumns="1">
                                            </asp:CheckBoxList>
                                            </td>
                                    </tr>

                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblEmail" runat="server" CssClass="label" Text="E-mail:" Width="82px"></asp:Label>
                                        </td>
                                        <td class="celula_campo" width="450" style="padding-left: 10px; padding-top: 10px;">
                                            <cc2:CustomTextBox ID="txtEmail" runat="server" CssClass="textbox" Width="270px"></cc2:CustomTextBox>
                                                &nbsp;<asp:Button ID="cmdEnviarIngresso" runat="server" CssClass="botao" 
                                                TabIndex="2" Text="enviar" onclick="cmdEnviarIngresso_Click"/>
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

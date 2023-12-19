<%@ Page Title="" Language="C#" MasterPageFile="~/controls/Clube.master" AutoEventWireup="true" CodeFile="CatracaVirtual.aspx.cs" Inherits="clube_CatracaVirtual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        //        function bloquear_ctrl_j() {
        //            if (window.event.ctrlKey && window.event.keyCode == 74) {
        //                event.keyCode = 0;
        //                event.returnValue = false;
        //            }
        //        }
    </script>
    <style type="text/css">
        .style1
        {
            background-color: #dee1e3;
            border-bottom: 2px solid #fff;
            height: 40px;
            width: 492px;
        }
        .style2
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home" style="margin-left: 1%;">
                <h1>MÓDULO DE VALIDAÇÃO | PÓS CATRACAS</h1>
                <div class="texto_box_home_site">
                    <fieldset class="fieldset_l_Solicitacao" style="padding:5px; width: 776px;">
                        <legend>&nbsp;Ingressos</legend>
                        <table align="center">
                            <tr>
                                <td class="celula_nome_campo" style="vertical-align:top; padding-top:13px;">
                                    <asp:Label  ID="Label3" runat="server" CssClass="label" 
                                        Text="Identidade Eletrônica:"></asp:Label>
                                </td>
                                <td class="style1" 
                                    style="vertical-align:top; padding-top:5px;padding-left:10px;">
                                    <asp:TextBox ID="txtIdentidadeEletronica" runat="server" onKeyDown="bloquear_ctrl_j()" MaxLength="20"
                                        onkeypress="mascara(this,soNumeros)" onkeyup="mascara(this,soNumeros)"></asp:TextBox>
                                    &nbsp;&nbsp; &nbsp;<asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" Text="Pesquisar" OnClick="cmdPesquisar_Click" />
                                    &nbsp;<asp:Button ID="cmdLimpar" runat="server" CssClass="botao" 
                                        Text="Limpar" onclick="cmdLimpar_Click"  />
                                </td>
                            </tr>
                     </table>
                        &nbsp;&nbsp;&nbsp;
                        <br />
                        <table>
                            <asp:Label  ID="lblMensagem" runat="server" CssClass="label_alerta"></asp:Label>
                        </table>
                        <table class="style2">
                            <tr>
                                <td colspan="2">
                                    <asp:Label  ID="lblNomeTela" runat="server" CssClass="label" Text="Nome:"></asp:Label>
                                    <asp:Label  ID="lblNome" runat="server"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:Label  ID="lblCotaTela" runat="server" CssClass="label" Text="Cota:"></asp:Label>
                                    <asp:Label  ID="lblCota" runat="server"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:Label  ID="lblTicketTela" runat="server" CssClass="label" Text="Ticket:"></asp:Label>
                                    <asp:Label  ID="lblTicket" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label  ID="lblResumoTela" runat="server" CssClass="label" Text="Resumo:"></asp:Label>
                                    <asp:Label  ID="lblResumo" runat="server"></asp:Label>
                                </td>
                                <td colspan="2">
                                    &nbsp;</td>
                                <td colspan="2">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>

                            </tr>

                             <tr>
                                <td colspan="5">
                                    <asp:Label  ID="lblDataTela" runat="server" CssClass="label" 
                                        Text="Data Hora Entrada:"></asp:Label>
                                    <asp:Label  ID="lblDataHoraEntrada" runat="server"></asp:Label>
                                 </td>
                                <td>
                                    &nbsp;</td>

                            </tr>
                        </table>
                        </tr>
                        </table>
                    </fieldset>
                    <%--<div style="float: right;">
                        <fieldset class="fieldset_l_Solicitacao" style="padding:5px;">
                            <legend>Informações</legend>
                            <table>
                                <tr>
                                    <td class="celula_nome_campo">
                                        <asp:Label  ID="lblEvento" runat="server" CssClass="label" Text="Evento:"></asp:Label>
                                    </td>
                                    <td class="celula_campo" style="padding-left: 5px; padding-top: 10px; padding-right: 5px; width: 410px;">
                                        <asp:DropDownList ID="dropEdicao" runat="server" AutoPostBack="True" 
                                            CssClass="dropdown" OnSelectedIndexChanged="dropEdicao_SelectedIndexChanged" 
                                            Width="320px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="celula_nome_campo">
                                        <asp:Label  ID="Label1" runat="server" CssClass="label" Text="Total:"></asp:Label>
                                    </td>
                                    <td class="celula_campo">
                                        <asp:Label  ID="lblTotal" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="celula_nome_campo">
                                        <asp:Label  ID="Label2" runat="server" CssClass="label" Text=" Já entraram:"></asp:Label>
                                    </td>
                                    <td class="celula_campo">
                                        <asp:Label  ID="lblNaoEntraram" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="celula_nome_campo">
                                        <asp:Label  ID="Label4" runat="server" CssClass="label" Text="Não entraram:"></asp:Label>
                                    </td>
                                    <td class="celula_campo">
                                        <asp:Label  ID="lblJaEntraram" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        </div>--%>
                        <br />
                        <table>
                            <tr>
                                <%--<td style="padding-left: 50px;padding-top: 10px;">
                                      &nbsp;</td>--%>
                            </tr>
                        </table>
                   </div>
                </div>
            </div>
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
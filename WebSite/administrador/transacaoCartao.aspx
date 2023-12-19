<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master"
    AutoEventWireup="true" CodeFile="transacaoCartao.aspx.cs" Inherits="administrador_transacaoCartao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  

  
    <style type="text/css">
        .auto-style1 {
            border: 1px solid #262626;
            color: #333;
            font-family: Calibri;
            font-size: 11px;
            margin: 54 0 10px 0;
            padding: 5px;
        }
    </style>
  

  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

 

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1>TRANSAÇÃO CARTÃO</h1>
                <div class="texto_box_home_site">
                    <table style="margin-top: 5px;">
                        <tr>
                            <td>
                                <table class="style4">
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblTid" runat="server" CssClass="label" Text="TID:" 
                                                Width="48px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtTid" runat="server" CssClass="textbox" 
                                                MaxLength="100" Width="294px"></cc2:CustomTextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" OnClick="cmdPesquisar_Click" TabIndex="2" Text="Pesquisar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" height="15">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblID" runat="server" CssClass="label" Text="PaymentId:" Width="80px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtPaymentId" runat="server" CssClass="auto-style1" MaxLength="100" Width="294px"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="2">
                                            &nbsp;<asp:Button ID="cmdCancelar" runat="server" CssClass="botao" 
                                                 TabIndex="2" Text="Cancelar Venda" onclick="cmdCancelar_Click" />
                                            &nbsp;<asp:Button ID="cmdVoltar" runat="server" CssClass="botao" 
                                                OnClick="cmdVoltar_Click" TabIndex="3" Text="Voltar" />
                                        </td>
                                    </tr>
                                    
                                    
                                </table>
                            </td>
                        </tr>
                        
                        <tr>
                            <td valign="top">
                                <asp:Label  ID="lblRetorno" runat="server" CssClass="label_mensagem_candidato"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

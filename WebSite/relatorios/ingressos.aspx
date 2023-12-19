<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master"
    AutoEventWireup="true" CodeFile="Ingressos.aspx.cs" Inherits="relatorios_Ingressos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

 <script type="text/javascript">
     function ExibirIngressos(id) {
         $.fancybox.open({
             href: "../vendedor/ingressosVoucher.aspx?id=" + id,
             type: "iframe",
             width: 750,
             height: "auto",
             padding: 5
         });

         return false;
     }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1>IMPRESSÃO DE INGRESSOS</h1>
                <div class="texto_box_home_site">
                    <table style="margin-top: 5px;">
                        <tr>
                            <td>
                                <table class="style4">
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblVoucher" runat="server" CssClass="label" Text="Núm. Voucher:" 
                                                Width="52px"></asp:Label>
                                        </td>
                                        <td class="celula_campo" colspan="3">
                                            <cc2:CustomTextBox ID="txtVoucher" runat="server" CssClass="textbox" 
                                                MaxLength="6" TabIndex="1" Width="80px" 
                                                onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)" 
                                onkeyup="mascara(this,soNumeros)"></cc2:CustomTextBox>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblNome" runat="server" CssClass="label" Text="Nome Usuário:" 
                                                Width="48px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtNome" runat="server" CssClass="textbox" 
                                                MaxLength="100" Width="294px"></cc2:CustomTextBox>
                                            </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblCPF" runat="server" CssClass="label" Text="CPF:" 
                                                Width="32px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtCPF" runat="server" CssClass="textbox" MaxLength="11" 
                                                TabIndex="1" Width="170px" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)" 
                                onkeyup="mascara(this,soNumeros)"></cc2:CustomTextBox>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="2">
                                            <asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" 
                                                OnClick="cmdPesquisar_Click" TabIndex="2" Text="Pesquisar" />
                                            &nbsp;<asp:Button ID="cmdVoltar" runat="server" CssClass="botao" 
                                                OnClick="cmdVoltar_Click" TabIndex="3" Text="Voltar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="2">
                                            <table >
                                                <tr>
                                                    <td>
                                                        <asp:Label  ID="lblNumeroRegistros" runat="server" 
                                                            CssClass="label_numero_linhas">| 0 registro(s) |</asp:Label>
                                                    </td>
                                                    <td>
                                                        <img alt="" src="../images/QR_Code_icon.png" style="width: 16px; height: 16px" />
                                                    </td>
                                                    <td>
                                                        <asp:Label  ID="lblAbrir" runat="server" CssClass="label_sumario">Abrir Ingressos</asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="style7" valign="top">
                                <asp:GridView ID="grdRelatorio" runat="server" CssClass="grid"  
                                     AutoGenerateColumns="False" DataKeyNames="IDVenda" 
                                    onrowdatabound="grdRelatorio_RowDataBound">
                                    <RowStyle CssClass="grid" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="Evento" HeaderText="Evento" />
                                        <asp:BoundField DataField="Edicao" HeaderText="Edição" />
                                        <asp:BoundField DataField="IDVenda" HeaderText="Voucher" />
                                        <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                        <asp:BoundField DataField="CPF" HeaderText="CPF" />
                                        <asp:BoundField DataField="NumeroCota" HeaderText="Cota" />
                                        <asp:BoundField DataField="FormaPagamento" HeaderText="Forma Pagamento" />
                                        <asp:BoundField DataField="Data/Hora Compra" HeaderText="Data/Hora Compra" />
                                        <asp:BoundField DataField="NumeroIngressos" HeaderText="Núm. Ingressos" />
                                        <asp:BoundField DataField="StatusCompra" HeaderText="Status da Compra" />
                                        <asp:BoundField DataField="NomeRetirada2" HeaderText="2º Nome Retirada" />
                                        <asp:BoundField DataField="RgRetirada2" HeaderText="2º Rg Retirada " />
                                        
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hypImprimirIngressos" runat="server" 
                                                    ImageUrl="~/images/QR_Code_grid.png"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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

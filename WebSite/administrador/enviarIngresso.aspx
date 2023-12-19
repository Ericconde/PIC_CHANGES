<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master"
    AutoEventWireup="true" CodeFile="enviarIngresso.aspx.cs" Inherits="administrador_enviarIngresso" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  

  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script type="text/javascript">
    function AbrirSolicitacaoIngresso(IDVenda) {
            $.fancybox.open({
                href: "../cliente/SolicitacaoIngresso.aspx?IDVenda=" + IDVenda,
                type: "iframe",
                maxWidth: 630,
                minHeight: 430,
                padding: 5
            });
            return false;
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1>ENVIAR INGRESSO</h1>
                <div class="texto_box_home_site">
                    <table style="margin-top: 5px;">
                        <tr>
                            <td>
                                <table class="style4">
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblNome2" runat="server" CssClass="label" Text="Núm. Voucher:" 
                                                Width="52px"></asp:Label>
                                        </td>
                                        <td class="celula_campo" colspan="3">
                                            <cc2:CustomTextBox ID="txtVoucher" runat="server" CssClass="textbox" 
                                                MaxLength="6" TabIndex="1" Width="110px" 
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
                                            <asp:Label  ID="lblNome1" runat="server" CssClass="label" Text="CPF:" 
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
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="2">
                                            <table >
                                                <tr>
                                                    <td>
                                                        <asp:Label  ID="lblNumeroRegistros" runat="server" 
                                                            CssClass="label_numero_linhas"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <img alt="" src="../images/198.png" style="width: 16px; height: 16px" />
                                                    </td>
                                                    <td>
                                                        <asp:Label  ID="lblSumario0" runat="server" CssClass="label_sumario">Enviar ingresso</asp:Label>
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
                                     AutoGenerateColumns="False" DataKeyNames="IDVenda, IDStatusCompra" 
                                    onrowdatabound="grdRelatorio_RowDataBound" Width="900px" >
                                    <RowStyle CssClass="grid" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="Evento" HeaderText="Evento" HeaderStyle-BackColor="#006CB5"/>
                                        <asp:BoundField DataField="Edicao" HeaderText="Edição" HeaderStyle-BackColor="#006CB5" />
                                        <asp:BoundField DataField="IDVenda" HeaderText="Voucher" HeaderStyle-BackColor="#006CB5"/>
                                        <asp:BoundField DataField="Nome" HeaderText="Nome" HeaderStyle-BackColor="#006CB5"/>
                                        <asp:BoundField DataField="CPF" HeaderText="CPF" HeaderStyle-BackColor="#006CB5"/>
                                        <asp:BoundField DataField="NumeroCota" HeaderText="Cota" HeaderStyle-BackColor="#006CB5"/>
                                        <asp:BoundField DataField="FormaPagamento" HeaderText="Forma Pagamento" HeaderStyle-BackColor="#006CB5"/>
                                        <asp:BoundField DataField="Data/Hora Compra" HeaderText="Data/Hora Compra" HeaderStyle-BackColor="#006CB5"/>
                                        <asp:BoundField DataField="StatusCompra" HeaderText="Status da Compra" HeaderStyle-BackColor="#006CB5"/>    
                                        
                                        <asp:TemplateField HeaderStyle-BackColor="#006CB5">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hypEnviarIngresso" runat="server" ImageUrl="~/images/198.png"></asp:HyperLink>
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

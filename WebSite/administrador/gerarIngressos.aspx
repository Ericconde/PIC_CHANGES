<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master"
    AutoEventWireup="true" CodeFile="gerarIngressos.aspx.cs" Inherits="administrador_gerarIngressos" %>

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
    <asp:Timer runat="server" ID="TimerProgresso" Interval="1000" Enabled="false" ontick="TimerProgresso_Tick" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
    <Triggers>
            <asp:PostBackTrigger ControlID="cmdExportar"/>
            <asp:PostBackTrigger ControlID="grdIngressos" />
            <asp:AsyncPostBackTrigger ControlID="TimerProgresso" EventName="Tick" />
        </Triggers>

        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1>Gerar Ingressos</h1>
                <div class="texto_box_home_site">
                    <table style="margin-top: 5px;">
                        <tr>
                            <td>
                                <table class="style4">
                                <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="Label1" runat="server" CssClass="label" Text="Evento:"></asp:Label>
                                        </td>
                                        <td class="celula_campo" style="padding-top: 5px; padding-right: 10px;">
                                            <asp:DropDownList ID="dropEvento" runat="server" AutoPostBack="true" 
                                                CssClass="dropdown" Width="320px" 
                                                onselectedindexchanged="dropEvento_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                
                                <tr id="linha_TipoMeiaInteira" Visible="false" runat="server">
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="Label2" runat="server" CssClass="label" Text="Tipo Ingresso:"></asp:Label>
                                        </td>
                                        <td class="celula_campo" style="padding-left: 5px; padding-right: 10px;">
                                            <asp:RadioButtonList ID="radTipoMeiaInteira" runat="server" CssClass="radioButton" 
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True">Inteira</asp:ListItem>
                                                        <asp:ListItem Value="Meio">Meia</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                <tr id="linha_TipoGenero" Visible="false" runat="server">
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="LblGenero" runat="server" CssClass="label" Text="Tipo Ingresso:"></asp:Label>
                                        </td>
                                        <td class="celula_campo" style="padding-left: 5px; padding-right: 10px;">
                                            <asp:RadioButtonList ID="radTipoIngresso" runat="server" CssClass="radioButton" 
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True">Sócio</asp:ListItem>
                                                        <asp:ListItem Value="Nao sócio">Não sócio</asp:ListItem>
                                                        <asp:ListItem Value="Cortesia">Cortesia</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <%--<asp:RadioButtonList ID="radTipoGenero" runat="server" CssClass="radioButton" 
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True">Masculino</asp:ListItem>
                                                        <asp:ListItem Value="Meio">Feminino</asp:ListItem>
                                            </asp:RadioButtonList>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo" >
                                            <asp:Label  ID="lblQteIngressos" runat="server" CssClass="label" Text="Qte. Ingressos:"></asp:Label>
                                        </td>
                                        <td class="celula_campo" style="padding-left: 5px; padding-right: 10px;">
                                            <cc2:CustomTextBox ID="txtQteIngressos" runat="server" CssClass="textbox" 
                                                MaxLength="3" TabIndex="1" Width="60px"
                                                onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)" 
                                onkeyup="mascara(this,soNumeros)"></cc2:CustomTextBox>
                                            
                                            &nbsp;<asp:Button ID="cmdGerarIngresso" runat="server" CssClass="botao" 
                                                TabIndex="2" Text="Gerar Ingressos" onclick="cmdGerarIngresso_Click" />
                                            
                                        </td>
                                    </tr>
                                    </table>
                                    <table>
                                    <tr>
                                        <td class="celula_campo" width="437">
                                        <asp:GridView ID="grdIngressos" runat="server" AutoGenerateColumns="False" 
                                            CssClass="grid" onrowcommand="grdIngressos_RowCommand" 
                                            DataKeyNames="Ticket" >
                                            <RowStyle CssClass="grid" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="Ticket" HeaderText="Núm. Ingresso" />
                                                <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                                                <asp:BoundField DataField="Valor" HeaderText="Valor" />
                                                <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/images/error.png"
                                                    Text="Excluir" />
                                            </Columns>
                                            <HeaderStyle CssClass="grid_header" HorizontalAlign="Left" />
                                            <AlternatingRowStyle CssClass="grid_alternative_row" />
                                        </asp:GridView>
                                        <asp:Label  ID="lblIngressosTotal" runat="server" CssClass="labelTotalCarrinho" 
                                            Text="Total: 0"></asp:Label>
                                    </td>
                                    </tr>
                                    </table>
                                    <tr>
                                        <td class="style9" colspan="2">
                                            &nbsp;<asp:Button ID="cmdExportar" runat="server" CssClass="botao" TabIndex="3" 
                                                Text="Exportar PDF" onclick="cmdExportar_Click" />
                                            &nbsp;<asp:Button ID="cmdCancelar" runat="server" CssClass="botao"
                                                OnClick="cmdCancelar_Click" TabIndex="3" Text="Cancelar" />
                                        </td>
                                    </tr>
                                        <tr>
                                            <td colspan="5">
                                                <asp:Label  ID="lblMensagem" runat="server" CssClass="label"></asp:Label>
                                            </td>
                                </tr>
                                </table>
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="5" >
                                <asp:Label  ID="lblInfo" runat="server" style="color: Red;"></asp:Label>
                            </td>
                        </tr>
                    </table> 
                </div>
            </div>
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

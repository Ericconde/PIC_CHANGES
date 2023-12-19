<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Cliente.master" AutoEventWireup="true"
    CodeFile="compra.aspx.cs" Inherits="cliente_compra" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">   
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function AbrirMapa(id, href) {
            $.fancybox.open({                
                href: href + id,
                type: 'iframe',
                width: 1500,
                'afterClose': function () {
                    window.location.reload();
                },
                //closeBtn : false,
                closeClick : false,
                helpers : { 
                    overlay : {
                        closeClick: false // prevents closing when clicking OUTSIDE fancybox
                    } 
                },
                keys : {
                    close: null // prevents close when clicking escape button
                }, 
                minHeight: 400,
                preload: true,
                padding: 5,
                
                autoSize : false
            });
            return false;
        }
    </script>
    <script type="text/javascript">
        function ExibirDetalhesEvento(id) {
            $.fancybox.open({
                href: "../cliente/detalhesEvento.aspx?id=" + id,
                type: "iframe",
                minWidth: 940,
                minHeight: 350,
                padding: 5
            });
            return false;
        }
    </script>

    <script type="text/javascript">
        function AbrirReciboPagamento(id) {
            $.fancybox.open({
                href: "../cliente/reciboPagamento.aspx?id=" + id,
                type: "iframe",
                minWidth: 840,
                minHeight: 350,
                padding: 5
            });
            return false;
        }
    </script>

    <script type="text/javascript">
        function closeFancyboxAndRedirectToUrl(url) {
            $.fancybox.close();
            window.location = url;
        }
    </script>

    <script type="text/javascript">
        function RealizarPagamento(id) {
            $.fancybox.open({
                href: '../pagamento/pagamentoCielo.aspx?id=' + id,
                type: 'iframe',
                width: '450px',
                height: 'auto',
                preload: true,
                scrolling: "no",
                closeClick: false,
                padding: 5
            });
            return false;
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1>
                    Compra</h1>
                <div class="texto_box_home_site">
                    <fieldset class="fieldset_l_compra" style="width: 926px;">
                        <legend>Passo 1 - Identificação do Cliente</legend>
                        <div id="identificacao" style="width: 926px; height: 73px;">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="celula_nome_campo">
                                        <asp:Label  ID="lblNome" runat="server" CssClass="label" Text="Nome:"></asp:Label>
                                    </td>
                                    <td class="celula_campo" width="750">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label  ID="lblNome_valor" runat="server" CssClass="label_compra_valor"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="celula_nome_campo">
                                        <asp:Label  ID="lblEmail" runat="server" CssClass="label" Text="E-mail:"></asp:Label>
                                    </td>
                                    <td class="celula_campo" width="860">
                                        <asp:Label  ID="lblEmail_valor" runat="server" CssClass="label_compra_valor"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="celula_nome_campo">
                                        <asp:Label  ID="lblCPF" runat="server" CssClass="label" Text="CPF:"></asp:Label>
                                    </td>
                                    <td class="celula_campo">
                                        <asp:Label  ID="lblCPF_valor" runat="server" CssClass="label_compra_valor"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>

                    <fieldset class="fieldset_l_compra " style="width: 926px;">
                        <legend>Passo 2 - Selecionar Evento</legend>
                        <div id="selecionarEvento" style="width: 926px; height: auto;">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="celula_nome_campo">
                                        <asp:Label  ID="lblEvento" runat="server" CssClass="label" Text="Evento:"></asp:Label>
                                    </td>
                                    <td class="celula_campo" width="900">
                                        <asp:DropDownList ID="dropEdicao" runat="server" AutoPostBack="true" 
                                                        CssClass="dropdown" OnSelectedIndexChanged="dropEdicao_SelectedIndexChanged" 
                                                        Width="600px">
                                                    </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table >
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="LinkButtonAbrirDetalhes" runat="server" 
                                                        CssClass="botao_detalhes" 
                                                        OnClientClick="alert('Selecione um evento para visualizar os detalhes (Passo 2).')">Normas do Evento</asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkDeclaracao" runat="server" 
                                                        CssClass="checkbox"
                                                        Text="Declaro que li e aceito as normas deste evento do Clube" 
                                                        AutoPostBack="True" oncheckedchanged="chkDeclaracao_CheckedChanged" />
                                                   
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="celula_nome_campo" colspan="2">
                                        <asp:Label  ID="lblMensagemFila" runat="server" CssClass="label_fila"></asp:Label>
                                        <asp:Label  ID="lblResumo" runat="server" CssClass="label_compra_valor"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>

                    <fieldset class="fieldset_l_compra " style="width: 926px;">
                        <legend>Passo 3 - Escolher Ingressos</legend>
                        <div id="escolherCadeiras" style="width: 926px; height: auto;">
                            <table style="width: 100%;">
                            <div id="divAniversariante" runat="server">
                                        <tr>
                                                <td class="celula_nome_campo" align="right">
                                                    <asp:Label  ID="Label1" CssClass="label" Text="Aniversariante:" runat="server" />
                                            </td>
                                            <td class="celula_campo">
                                                <asp:CheckBox Text="Sim" ID="chkAniversariante" runat="server" 
                                                    oncheckedchanged="chkAniversariante_CheckedChanged" AutoPostBack="true" />
                                            </td>
                                        </tr>
                                    </div>
                                <tr>
                                    <td align="right" colspan="2">



                                        <asp:Panel ID="pnlCadeira" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td class="celula_nome_campo">
                                                        <asp:Label  ID="lblCadeira" runat="server" CssClass="label" Text="Ingresso com cadeira/mesa:"></asp:Label>
                                                    </td>
                                                    <td class="celula_campo">
                                                        <asp:LinkButton ID="LinkButtonAbrirMapaInteira" runat="server" CssClass="botao_venda_ingresso" OnClientClick="alert('Selecione um evento antes de abrir o mapa (Passo 2).')">Abrir Mapa</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>



                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="2">
                                        <asp:Panel ID="pnlAvulso" runat="server">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td class="celula_nome_campo"><asp:Label  ID="lblEscolhaValor" runat="server" CssClass="label" Text="Escolha o Valor:" Visible="False"></asp:Label></td>
                                                    <td colspan="3" class="celula_campo">
                                                        
                                                        <asp:DropDownList ID="dropValorIngresso" runat="server" CssClass="dropdown" Visible="False" Width="400px">
                                                        </asp:DropDownList>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="celula_nome_campo">
                                                        <asp:Label  ID="lblAvulso" runat="server" CssClass="label" Text="Ingresso sem cadeira:"></asp:Label>
                                                    </td>
                                                    <td class="celula_campo">
                                                        <asp:RadioButtonList ID="radTipoAvulso" runat="server" CssClass="radioButton" RepeatDirection="Horizontal" RepeatColumns="2">
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td class="celula_campo">
                                                        <cc2:CustomTextBox ID="txtAvulso" runat="server" CssClass="textbox" MaxLength="2" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)" onkeyup="mascara(this,soNumeros)" Width="42px"></cc2:CustomTextBox>
                                                    </td>
                                                    <td class="celula_campo">
                                                        <asp:Button ID="cmdAdicionarAvulso" runat="server" CssClass="botao_venda_ingresso" onclick="cmdAdicionarAvulso_Click" Text="Adicionar ao carrinho" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td colspan="2" headers="10" align="right">
                                        <asp:Label  ID="lblMensagemMeia" runat="server" CssClass="label_alerta_destacado" Visible="False">Atenção! Meio ingresso: apenas para adolescentes com idade entre 12 e 17 anos</asp:Label>
                                        </td>
                                </tr>
                                <tr>
                                    <td class="celula_nome_campo">
                                        <asp:Label  ID="lblCarrinho" runat="server" CssClass="label" 
                                            Text="Carrinho de Compras:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="celula_campo" width="770">
                                        <asp:GridView ID="grdCarrinho" runat="server" AutoGenerateColumns="False" 
                                            CssClass="grid" onrowcommand="grdCarrinho_RowCommand" 
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
                                        <asp:Label  ID="lblCarrinhoTotal" runat="server" CssClass="labelTotalCarrinho" 
                                            Text="Total: R$ 0,00"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td  colspan="2">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label  ID="lblDesejaEstacionamento" runat="server" CssClass="label" 
                                                        Text="Número de vagas de estacionamento que deseja:" Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dropVagasEstacionamento" runat="server" 
                                                        CssClass="dropdown" Visible="False" style="width:100px;margin-top: 7px;" 
                                                        AutoPostBack="True" 
                                                        onselectedindexchanged="dropVagasEstacionamento_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Nenhuma</asp:ListItem>
                                                        <asp:ListItem>1</asp:ListItem>
                                                        <asp:ListItem>2</asp:ListItem>
                                                        <asp:ListItem>3</asp:ListItem>
                                                        <asp:ListItem>4</asp:ListItem>
                                                        <asp:ListItem>5</asp:ListItem>
                                                        <asp:ListItem>6</asp:ListItem>
                                                        <asp:ListItem>7</asp:ListItem>
                                                        <asp:ListItem>8</asp:ListItem>
                                                        <asp:ListItem>9</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>

                    <fieldset id="fieldCriancas" runat="server" class="fieldset_l_compra " style="width: 926px;">
                        <legend>Passo 4 - Informações Complementares</legend>
                        <div id="Div1" style="width: 926px; min-height: 47px;">
                            <table width="100%">
                                <tr>
                                    <td class="celula_nome_campo">
                                        <asp:Label  ID="Label2" runat="server" CssClass="label" 
                                            Text="Núm. de crianças não pagantes (0 a 11 anos):" Width="300px"></asp:Label>

                                        <br/>

                                    </td>
                                    <td class="celula_campo" width="700">
                                        <asp:DropDownList ID="dropNumeroCriancas" runat="server" CssClass="dropdown" Width="50px">
                                            <asp:ListItem Selected="True">0</asp:ListItem>
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            
                        </div>
                    </fieldset>

                    <fieldset id="fieldFormaPagamento" runat="server" class="fieldset_l_compra " style="width: 926px;">
                        <legend runat="server" id="formaPagamento">Passo 5 - Forma de Pagamento</legend>
                        <div id="formaPagamento" style="width: 926px; min-height: 47px;">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="celula_nome_campo">
                                        <asp:Label  ID="lblPagamento" runat="server" CssClass="label" Text="Pagamento:"></asp:Label>
                                    </td>
                                    <td class="celula_campo" width="900">
                                        <asp:RadioButtonList ID="radFormaPagamento" runat="server" CssClass="radioButton"
                                            RepeatDirection="Horizontal" RepeatColumns="4">                                           
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="celula_nome_campo">
                                        <asp:Label  ID="lblDetalhesPagamento" runat="server" CssClass="label" Text="Detalhes:" Visible="false"></asp:Label>
                                    </td>
                                    <td class="celula_campo" width="900">
                                        <cc2:CustomTextBox ID="txtDetalhesFormaPagamento" runat="server" CssClass="textbox" 
                                            MaxLength="500" Width="600px" Visible="false"></cc2:CustomTextBox>
                                    </td>
                                </tr>
                            </table>
                            
                        </div>
                    </fieldset>

                    <asp:Button ID="cmdFinalizar" runat="server" CssClass="botao" Text="Finalizar" onclick="cmdFinalizar_Click" 
                         />
                    &nbsp;<asp:Button ID="cmdReservar" runat="server" CssClass="botao" Visible="false"
                        Text="Reservar" onclick="cmdReservar_Click" />
                    &nbsp;<asp:Button ID="cmdCancelar" runat="server" CssClass="botao" 
                         Text="Cancelar Compra" onclick="cmdCancelar_Click" />
                    <br />  
                    <asp:Label  ID="lblMensagem" runat="server" CssClass="label_alerta"></asp:Label>
                    <div class="div_RetornoPagamento" runat="server" id="divRetornoPagamento" visible="false">
                    
                        <asp:HyperLink ID="lblRetorno" runat="server" CssClass="not-active" 
                            Width="500px">[lblRetorno]</asp:HyperLink>
                        <br/><br/>
                        <asp:HyperLink ID="LinkButtonImprimirVoucher" runat="server" CssClass="linkBotao"
                         Width="170px" Visible="false">Imprimir Voucher</asp:HyperLink>
                        &nbsp;<asp:HyperLink ID="LinkButtonReciboPagamento" runat="server" 
                            CssClass="linkBotao" Visible="false" Width="170px">Recibo para Pagamento</asp:HyperLink>
                    </div>
                </div> 
            </div>
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

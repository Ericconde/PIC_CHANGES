<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master"
    AutoEventWireup="true" CodeFile="estacionamentoAvulso.aspx.cs" Inherits="administrador_estacionamentoAvulso" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  

  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1>ESTACIONAMENTO AVULSO</h1>
                <div class="texto_box_home_site">
                    <table style="margin-top: 5px;">
                        <tr>
                            <td>
                                <table class="style4">
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblEvento" runat="server" CssClass="label" Text="Evento:"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <asp:DropDownList ID="dropEdicao" runat="server" AutoPostBack="True" 
                                                CssClass="dropdown" Width="400px" 
                                                onselectedindexchanged="dropEdicao_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblNome" runat="server" CssClass="label" Text="Nome:" 
                                                Width="47px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtNome" runat="server" CssClass="textbox" 
                                                MaxLength="200" TabIndex="1" Width="500px"></cc2:CustomTextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="Label1" runat="server" CssClass="label" Text="CPF:" Width="40px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtCPF" runat="server" CssClass="textbox" MaxLength="11" 
                                                TabIndex="3" Width="170px"></cc2:CustomTextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" 
                                                TargetControlID="txtCPF" ValidChars="0,1,2,3,4,5,6,7,8,9">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="2">
                                            &nbsp;<asp:Button ID="cmdSalvar" runat="server" CssClass="botao" 
                                                OnClick="cmdSalvar_Click" TabIndex="3" Text="Salvar" />
                                            &nbsp;<asp:Button ID="cmdVoltar" runat="server" CssClass="botao" 
                                                OnClick="cmdVoltar_Click" TabIndex="3" Text="Voltar" />
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="style9" colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label  ID="lblNumeroLinhas" runat="server" CssClass="label_numero_linhas"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <img alt="" src="../images/pdf.gif" style="width: 16px; height: 16px" />
                                                        <asp:Label  ID="lblSumario" runat="server" CssClass="label_sumario">Abrir Ticket de Estacionamento</asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>
                        </tr>
                        
                        <tr>
                            <td  valign="top">
                                <asp:GridView ID="grdEstacionamento" runat="server" 
                                    AutoGenerateColumns="False" CssClass="grid" DataKeyNames="IdentidadeEletronica" 
                                    EmptyDataText="Nenhum registro encontrado!" 
                                    PageSize="25" onrowdatabound="grdEstacionamento_RowDataBound" >
                                    <RowStyle CssClass="grid" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="IdentidadeEletronica" HeaderText="Identidade Eletrônica" />
                                        <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                        <asp:BoundField DataField="CPF" HeaderText="CPF" />
                                        <asp:BoundField DataField="Evento" HeaderText="Evento" />
                                        <asp:BoundField DataField="Data/Hora Cadastro" HeaderText="Data/Hora Cadastro" />
                                        <asp:BoundField DataField="Usuário Responsável" HeaderText="Usuário Responsável" />
                                        <asp:ButtonField ButtonType="Image" CommandName="Abrir" 
                                            ImageUrl="~/images/pdf.gif" Text="Editar" />
                                    </Columns>
                                    <HeaderStyle CssClass="grid_header" HorizontalAlign="Left" />
                                    <AlternatingRowStyle CssClass="grid_alternative_row" />
                                    <PagerStyle CssClass="grid_page" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table> 
                </div>
            </div>
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master" AutoEventWireup="true"
    CodeFile="baseSocios.aspx.cs" Inherits="administrador_baseSocios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="cmdExportarExcel" />
            <asp:PostBackTrigger ControlID="cmdProcessar" />
        </Triggers>
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1> BASE DE SÓCIOS</h1>
                <div class="texto_box_home_site">
                    <table style="margin-top: 5px;">
                        <tr>
                            <td>
                                <table class="style4">
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblNome" runat="server" CssClass="label" Text="Nome:" 
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
                                                TabIndex="1" Width="170px"></cc2:CustomTextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" 
                                                TargetControlID="txtCPF" ValidChars="0,1,2,3,4,5,6,7,8,9">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="Label124" runat="server" CssClass="label" Height="16px" 
                                                Text="Cota:" Width="45px"></asp:Label>
                                        </td>
                                        <td class="celula_campo" colspan="3">
                                            <cc2:CustomTextBox ID="txtNumCota" runat="server" CssClass="textbox" 
                                                MaxLength="6" onkeydown="mascara(this,soNumeros)" 
                                                onkeypress="mascara(this,soNumeros)" onkeyup="mascara(this,soNumeros)" 
                                                Width="120px"></cc2:CustomTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" height="15">
                                            <asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" 
                                                OnClick="cmdPesquisar_Click" TabIndex="2" Text="Pesquisar" />
                                            &nbsp;<asp:Button ID="cmdExportarExcel" runat="server" CssClass="botao" 
                                                onclick="cmdExportarExcel_Click" TabIndex="4" Text="Exportar Excel" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" height="15">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label  ID="lblArquivo" runat="server" CssClass="label" Text="Arquivo dos Sócios:"></asp:Label>
                                        </td>
                                        <td class="celula_campo" colspan="3">
                                            <asp:FileUpload ID="fileDocumento" runat="server" CssClass="textbox" 
                                                Width="300px" ForeColor="black" BorderColor="black" />
                                            <asp:Button ID="cmdProcessar" runat="server" CssClass="botao" 
                                                onclick="cmdProcessar_Click" TabIndex="2"  Text="Processar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="4">
                                            <asp:Label  ID="lblMensagem" runat="server" CssClass="label_alerta"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="4">
                                            <asp:Label  ID="lblNumeroRegistros" runat="server" 
                                                CssClass="label_numero_linhas"></asp:Label>
                                        </td>
                                    </tr>
                                    </table>
                                    <tr>
                            <td valign="top">
                                <asp:GridView ID="grdRelatorio" runat="server" CssClass="grid" 
                                    AutoGenerateColumns="False" AllowPaging="True" 
                                    onpageindexchanging="grdRelatorio_PageIndexChanging" PageSize="25">
                                    <RowStyle CssClass="grid" HorizontalAlign="Left" />
                                    <Columns>                                        
                                       <asp:BoundField DataField="Num_Cota" HeaderText="Num_Cota" HeaderStyle-BackColor="#4d94c4"/>
                                       <asp:BoundField DataField="Digito" HeaderText="Digito" HeaderStyle-BackColor="#4d94c4"/>
                                       <asp:BoundField DataField="Nome" HeaderText="Nome" HeaderStyle-BackColor="#4d94c4"/>
                                       <asp:BoundField DataField="Dat_Nasc" HeaderText="Dat_Nasc" HeaderStyle-BackColor="#4d94c4"/>
                                       <asp:BoundField DataField="CPF" HeaderText="CPF" HeaderStyle-BackColor="#4d94c4"/>
                                       <asp:BoundField DataField="RG" HeaderText="RG" HeaderStyle-BackColor="#4d94c4"/>
                                       <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-BackColor="#4d94c4"/>
                                       <asp:BoundField DataField="fone1" HeaderText="fone1" HeaderStyle-BackColor="#4d94c4"/>
                                       <asp:BoundField DataField="fone2" HeaderText="fone2" HeaderStyle-BackColor="#4d94c4"/>
                                       <asp:BoundField DataField="Logradouro" HeaderText="Logradouro" HeaderStyle-BackColor="#4d94c4"/>
                                       <asp:BoundField DataField="Numero" HeaderText="Numero" HeaderStyle-BackColor="#4d94c4"/>
                                       <asp:BoundField DataField="Complemento" HeaderText="Complemento" HeaderStyle-BackColor="#4d94c4"/>
                                       <asp:BoundField DataField="Bairro" HeaderText="Bairro" HeaderStyle-BackColor="#4d94c4"/>
                                       <asp:BoundField DataField="CEP" HeaderText="CEP" HeaderStyle-BackColor="#4d94c4"/>
                                       <asp:BoundField DataField="Nom_Cidade" HeaderText="Nom_Cidade" HeaderStyle-BackColor="#4d94c4"/>
                                       <asp:BoundField DataField="Nom_Estado" HeaderText="Nom_Estado" HeaderStyle-BackColor="#4d94c4"/>
                                       <asp:BoundField DataField="Abrev_Estado" HeaderText="Abrev_Estado" HeaderStyle-BackColor="#4d94c4"/>
                                       <asp:BoundField DataField="Debito" HeaderText="Debito" HeaderStyle-BackColor="#4d94c4"/>
                                        <asp:BoundField DataField="Data de atualização" HeaderText="Data de atualização" HeaderStyle-BackColor="#4d94c4"/>
                                    </Columns>
                                    <HeaderStyle CssClass="grid_header" HorizontalAlign="Left" />
                                    <AlternatingRowStyle CssClass="grid_alternative_row" />
                                    <PagerStyle CssClass="grid_page" />
                                </asp:GridView>
                            </td>
                        </tr>
                                </table>
                            </td>
                        </tr>
                    
                </div>
            </div>
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

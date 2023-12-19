<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master" AutoEventWireup="true"
    CodeFile="blackList.aspx.cs" Inherits="administrador_blackList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 18px;
            height: 17px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home" style="margin-left: 1%;">
                <h1>Black List</h1>
                <div class="texto_box_home_site">
                    <table style="margin-top: 5px;">
                        <tr>
                            <td>
                                <table class="style4">
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label ID="lblNome" runat="server" CssClass="label" Text="Nome:"
                                                Width="48px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtNome" runat="server" CssClass="textbox"
                                                MaxLength="100" Width="294px"></cc2:CustomTextBox>
                                        </td>
                                        <td class="celula_nome_campo">
                                            <asp:Label ID="lblNome1" runat="server" CssClass="label" Text="IP:"
                                                Width="32px"></asp:Label>
                                        </td>
                                        <td class="celula_campo">
                                            <cc2:CustomTextBox ID="txtIP" runat="server" CssClass="textbox" MaxLength="11"
                                                TabIndex="1" Width="170px"></cc2:CustomTextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                TargetControlID="txtIP" ValidChars="0,1,2,3,4,5,6,7,8,9">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="celula_nome_campo">
                                            <asp:Label ID="Label124" runat="server" CssClass="label" Height="16px"
                                                Text="Data do Bloqueio:"></asp:Label>
                                        </td>
                                        <td class="celula_campo" colspan="3">
                                            <asp:TextBox ID="txtDataInicial" runat="server" CssClass="textbox"
                                                onkeydown="mascara(this,data)" onkeypress="mascara(this,data)"
                                                onkeyup="mascara(this,data)" Width="67px" MaxLength="10"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDataInicial_CalendarExtender" runat="server"
                                                CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy"
                                                TargetControlID="txtDataInicial">
                                            </cc1:CalendarExtender>
                                            &nbsp;a
                                            <asp:TextBox ID="txtDataFinal" runat="server" CssClass="textbox"
                                                onkeydown="mascara(this,data)" onkeypress="mascara(this,data)"
                                                onkeyup="mascara(this,data)" Width="67px" MaxLength="10"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDataFinal_CalendarExtender" runat="server"
                                                CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy"
                                                TargetControlID="txtDataFinal">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="4">
                                            <asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" OnClick="cmdPesquisar_Click" TabIndex="2" Text="Pesquisar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style9" colspan="4">
                                            <table class="auto-style1">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblNumeroRegistros" runat="server" CssClass="label_numero_linhas"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <img alt="" class="auto-style2" src="../images/status_verde.png" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblLiberado" runat="server" CssClass="label_sumario">Acesso liberado, não há bloqueio</asp:Label>
                                                    </td>
                                                    <td>
                                                        <img alt="" class="auto-style2" src="../images/status_vermelho.png" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBloqueado" runat="server" CssClass="label_sumario">Acesso bloqueado, sem acesso</asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <tr>
                                    <td valign="top">
                                        <asp:GridView ID="grdRelatorio" runat="server" CssClass="grid"
                                            AutoGenerateColumns="False" DataKeyNames="IDBlackList" OnRowDataBound="grdRelatorio_RowDataBound" OnRowCommand="grdRelatorio_RowCommand">
                                            <RowStyle CssClass="grid" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:BoundField DataField="IP" HeaderText="IP" HeaderStyle-BackColor="#4d94c4"/>
                                                <asp:BoundField DataField="MotivoBloqueio" HeaderText="Motivo do Bloqueio" HeaderStyle-BackColor="#4d94c4"/>
                                                <asp:BoundField DataField="Bloqueado" HeaderText="Bloqueado" HeaderStyle-BackColor="#4d94c4"/>

                                                <asp:TemplateField HeaderText="Bloqueado" HeaderStyle-BackColor="#4d94c4">
                                                    <ItemTemplate>
                                                        <center>
                                                            <asp:Image ID="imgBloqueado" runat="server"
                                                                ImageUrl="~/images/status_vermelho.png" class="img-status" /></center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="Data Bloqueio" HeaderText="Data Bloqueio" HeaderStyle-BackColor="#4d94c4"/>
                                                <asp:BoundField DataField="Cliente" HeaderText="Cliente" HeaderStyle-BackColor="#4d94c4"/>
                                                <asp:BoundField DataField="Data Alteração" HeaderText="Data Alteração" HeaderStyle-BackColor="#4d94c4"/>
                                                <asp:BoundField DataField="Usuário Alteração" HeaderText="Usuário Alteração" HeaderStyle-BackColor="#4d94c4"/>

                                                <asp:ButtonField ButtonType="Button" HeaderStyle-BackColor="#4d94c4" CommandName="Bloquear"  
                                                    ControlStyle-CssClass="botao"
                                                    ControlStyle-Width="100px"                                                    
                                                    Text="Bloquear" />

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

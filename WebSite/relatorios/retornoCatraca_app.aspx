<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Admin.master" AutoEventWireup="true"
    CodeFile="retornoCatraca_app.aspx.cs" Inherits="relatorios_retornoCatraca_app" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = new google.visualization.DataTable();

            data.addColumn('string');
            data.addColumn('number');

            data.addRows([<%RetornarCatracas(); %>]);

            var options = {
                title: ''
            };

            var chart = new google.visualization.PieChart(document.getElementById('Catraca'));
            chart.draw(data, options);
        }
    </script>

    <script type="text/javascript">
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = new google.visualization.DataTable();

            data.addColumn('string');
            data.addColumn('number');

            data.addRows([<%RetornarEstacionamentos(); %>]);

            var options = {
                title: ''
            };

            var chart = new google.visualization.PieChart(document.getElementById('Estacionamento'));
            chart.draw(data, options);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="cmdExportarExcel" />
        </Triggers>
        <ContentTemplate>--%>
    <!--ABERTURA DA DIV BOX HOME-->
    <div class="box_home" style="width: 975px;">
        <h1>RELATÓRIO - Registro de Entrada (App leitor de QRCode)</h1>
        <div class="texto_box_home_site">
            
                        <table width="975">
                            <tr>
                                <td class="celula_nome_campo">
                                    <asp:Label  ID="lblEvento" runat="server" CssClass="label" Text="Evento:"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <asp:DropDownList ID="dropEdicao" runat="server" CssClass="dropdown" Width="400px"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="celula_nome_campo">
                                    <asp:Label  ID="lblTipo" runat="server" CssClass="label" Text="Tipo:"></asp:Label>
                                </td>
                                <td class="celula_campo">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="radTipoIngresso" runat="server" CssClass="radioButton" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True">Todos</asp:ListItem>
                                                    <asp:ListItem>Catraca</asp:ListItem>
                                                    <asp:ListItem>Estacionamento</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td class="celula_nome_campo">
                                                <asp:Label  ID="lblStatus" runat="server" CssClass="label" Text="Status:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="radStatusIngresso" runat="server" CssClass="radioButton" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True">Todos</asp:ListItem>
                                                    <asp:ListItem>Não entrou</asp:ListItem>
                                                    <asp:ListItem>Entrou</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td>
                                                <asp:Button ID="cmdExportarExcel" runat="server" CssClass="botao"
                                        TabIndex="4" Text="Exportar Excel" OnClick="cmdExportarExcel_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="cmdAtualizar" runat="server" CssClass="botao" OnClick="cmdAtualizar_Click"
                                        TabIndex="2" Text="Atualizar" />
                                    &nbsp;<asp:Button ID="cmdVoltar" runat="server" CssClass="botao" OnClick="cmdVoltar_Click"
                                        TabIndex="3" Text="Voltar" />
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2"></td>
                            </tr>
                        </table>
           
            <fieldset class="fieldset_l_dashboard " style="width: 475px;">
                <legend>Catraca</legend>
                <div id="Catraca" style="width: 475px; height: 200px;">
                </div>
            </fieldset>

            <fieldset class="fieldset_l_dashboard " style="width: 475px;">
                <legend>Estacionamento</legend>
                <div id="Estacionamento" style="width: 475px; height: 200px;">
                </div>
            </fieldset>




            <table >
                            <tr>
                                <td class="celula_nome_campo">
                                    <asp:Label  ID="Label1" runat="server" CssClass="label" Text="Nº do Ingresso / Ticket:"></asp:Label>
                                </td>
                                <td class="celula_campo" width="700">
                                            <cc2:CustomTextBox ID="txtTicket" runat="server" CssClass="textbox" MaxLength="30" 
                                                TabIndex="1" Width="170px" onkeydown="mascara(this,soNumeros)" onkeypress="mascara(this,soNumeros)" 
                                onkeyup="mascara(this,soNumeros)"></cc2:CustomTextBox>
                                            
                                    &nbsp;<asp:Button ID="cmdPesquisar" runat="server" CssClass="botao" 
                                        TabIndex="2" Text="Pesquisar" OnClick="cmdPesquisar_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" height="15">
                                    </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                <asp:GridView ID="grdRelatorio" runat="server" CssClass="grid"  
                                     AutoGenerateColumns="False" >
                                    <RowStyle CssClass="grid" HorizontalAlign="Left" />
                                    <Columns>
                                        <asp:BoundField DataField="Ticket (Núm. Ingresso)" HeaderText="Ticket (Núm. Ingresso)" />
                                        <asp:BoundField DataField="Tipo de Ingresso" HeaderText="Tipo de Ingresso" />
                                        <asp:BoundField DataField="Nome do Cliente" HeaderText="Nome do Cliente" />
                                        <asp:BoundField DataField="CPF" HeaderText="CPF" />
                                        <asp:BoundField DataField="Cota" HeaderText="Cota" />
                                        <asp:BoundField DataField="Data/Hora da Entrada" HeaderText="Data/Hora da Entrada" />
                                        <asp:BoundField DataField="Responsável Entrada" HeaderText="Responsável Entrada" />
                                    </Columns>
                                    <HeaderStyle CssClass="grid_header" HorizontalAlign="Left" />
                                    <AlternatingRowStyle CssClass="grid_alternative_row" />
                                </asp:GridView>
                                </td>
                            </tr>
                        </table>




        </div>
    </div>
    <!--FECHAMENTO DA DIV BOX HOME-->
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

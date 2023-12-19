<%@ Page Title="" Language="C#" MasterPageFile="~/controls/Clube.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="clube_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
        var data = new google.visualization.DataTable();

        data.addColumn('string');
        data.addColumn('number');

        data.addRows([<%RetornarClientes(); %>]);

            var options = {
                title: ''
            };

            var chart = new google.visualization.PieChart(document.getElementById('clientes'), '1.1');
            chart.draw(data, options);
        }
    </script>

     <script type="text/javascript">
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {        
         var data = google.visualization.arrayToDataTable([
	        <%RetornarAcessos(); %>]);

            var options = {
                title: '',
                legend: { position: 'none' },
                hAxis: {slantedText:true, slantedTextAngle:90 }
            };

            var chart = new google.visualization.ColumnChart(document.getElementById('acessos'));
            chart.draw(data, options);
        }
    </script>

    <script type="text/javascript">
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {        
         var data = google.visualization.arrayToDataTable([
	        <%RetornarVendasQuantidade(); %>]);

            var options = {
                title: '',
                legend: { position: 'none' },
                hAxis: {slantedText:true, slantedTextAngle:90 }
            };

            var chart = new google.visualization.ColumnChart(document.getElementById('vendasQuantidade'));
            chart.draw(data, options);
        }
    </script>

    <script type="text/javascript">
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {        
         var data = google.visualization.arrayToDataTable([
	        <%RetornarVendasValor(); %>]);

            var options = {
                title: '',
                legend: { position: 'none' },
                hAxis: {slantedText:true, slantedTextAngle:90 }
            };

            var chart = new google.visualization.ColumnChart(document.getElementById('vendasValor'));
            chart.draw(data, options);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!--ABERTURA DA DIV BOX HOME-->
    <div class="box_home" style="width: 975px; ">

        <div>
            <h1>
                <asp:Label ID="lblUsuario" runat="server" CssClass="nome_usuario" Style="color: #006CB5; font-size: 14px; margin-left:10px;"></asp:Label>

           <br>
                <asp:Label ID="lblIP" runat="server" CssClass="nome_usuario" Style="color: #006CB5; font-size: 14px; margin-top:10px; margin-left:10px;"></asp:Label></h1>

        </div>

        <h1 style="color: #006CB5; margin-left:10px;">INICIAL</h1>
        <div class="texto_box_home_site">



            <legend style="color: #006CB5; margin-left:10px;"><b>EDIÇÃO</b></legend>
            <div class="label">
                <asp:DropDownList ID="dropEdicao" runat="server" CssClass="dropdown"
                    Style="width: 400px; margin-left: 10px; border: 2px solid #006CB5" AutoPostBack="True"
                    OnSelectedIndexChanged="dropEdicao_SelectedIndexChanged">
                </asp:DropDownList>
            </div>


            <fieldset class="fieldset_l_dashboard " style="width: 475px;">
                <table>
                    <td style="background: #006CB5; width: 475px; height: 20px;">
                        <label style="color: #fff"><b>&nbsp;CLIENTES CADASTRADOS</b></label>
                    </td>
                </table>
                <div id="clientes" style="width: 475px; height: 200px;">
                </div>
            </fieldset>

            <fieldset class="fieldset_l_dashboard " style="width: 475px;">
                <table>
                    <td style="background: #006CB5; width: 475px; height: 20px;">
                        <label style="color: #fff"><b>&nbsp;ACESSOS AO SISTEMA (12 ÚLTIMOS MESES)</b></label>
                    </td>
                </table>
                <div id="acessos" style="width: 475px; height: 200px;">
                </div>
            </fieldset>

            <fieldset class="fieldset_l_dashboard " style="width: 475px;">
                <table>
                    <td style="background: #006CB5; width: 475px; height: 20px;">
                        <label style="color: #fff"><b>&nbsp;VENDAS (QUANTIDADE)</b></label>
                    </td>
                </table>
                <div id="vendasQuantidade" style="width: 475px; height: 200px;">
                </div>
            </fieldset>

            <fieldset class="fieldset_l_dashboard " style="width: 475px;">
                <table>
                    <td style="background: #006CB5; width: 475px; height: 20px;">
                        <label style="color: #fff"><b>&nbsp;VENDAS (VALOR)</b></label>
                    </td>
                </table>
                <div id="vendasValor" style="width: 475px; height: 200px;">
                </div>
            </fieldset>


        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>


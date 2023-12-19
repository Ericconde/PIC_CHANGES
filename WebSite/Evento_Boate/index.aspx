<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="eventos_Boate_index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>Compra de  Ingressos para Reveillon do PIC</title>.
<link href="css/bootstrap.css" rel="stylesheet">
<link href="css/style.css" rel="stylesheet">
</head>

<body style="background-color: #009652;">
	<div class="container-fluid">
     
        <div id="menu">
            <ul>
                <li><a href="piso_1.aspx" class="ativo>1º PISO</a></li>
                <li><a href="piso_2.aspx">2º PISO</a></li>
            </ul>
            <div align="right" style="margin-right: 5px">
                        <button class="botao" style="color: black;background-color: white; border-color: white; border: solid 1px white; border-radius: 10px; text-align: center; width: 30px; height: 30px" onclick="parent.jQuery.fancybox.close()"><b>X</b></button>
                    </div>
        </div>
    </div>
    <div style="align:center; text-align:center !important; background-color:yellow; border-radius:10px; width: 500px; left: 30% !important; position: absolute !important;">
        <spam style="color: Black; text-align:center !important; "> Selecione acima o setor que deseja comprar suas cadeiras.</spam>
    </div>
	<div class="container-fluid">
    	<img class="img-responsive" src="../../images/imgBoate/bg-reveillon.jpg" style="max-width: 100%; !important"/>  
       <%-- <img class="img-responsive" src="img/Mapa_Reveillon_2016.jpg" style="max-width: 100%; !important"/>  --%>
    </div>
    <spam style="color: White;"> Mapa apenas ilustrativo. As mesas poderão sofrer alterações em sua localização no dia do evento.</spam>
</body>
</html>


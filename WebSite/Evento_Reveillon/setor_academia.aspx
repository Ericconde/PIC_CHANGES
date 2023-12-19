<%@ Page Language="C#" AutoEventWireup="true" CodeFile="setor_academia.aspx.cs" Inherits="eventos_reveillon_setor_academia" validateRequest="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Compra de Ingressos para o Reveillon do PIC</title>
    <link href="css/bootstrap.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <script src="scripts/web.js"></script>
</head>
<body style="background-color: #009652;">
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnableScriptGlobalization="True">
        <Scripts>
            <asp:ScriptReference Path="~/scripts/browser.js" />
        </Scripts>
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                
                <div id="menu">
                    <ul>
                        <li><a href="index.aspx">MAPA GERAL DO EVENTO</a></li>
                        <li><a href="setor_academia.aspx" class="ativo">SETOR ACADEMIA</a></li>
                        <li><a href="setor_salao_de_festas.aspx">SETOR SALÃO DE FESTAS</a></li>
                        <li><a href="setor_pergula.aspx">SETOR PÉRGULA</a></li>
                    </ul>
                    <div align="right" style="margin-right: 5px">
                        <button class="botao" style="color: black;background-color: white; border-color: white; border: solid 1px white; border-radius: 10px; text-align: center; width: 30px; height: 30px" onclick="parent.jQuery.fancybox.close()"><b>X</b></button>
                    </div>
                </div>
            </div>
            <table>
                <tr>
                    <td rowspan="3">
                        <div class="container-fluid" runat="server" id="cadeiras">
                            <img class="img-responsive" src="../../images/imgReveillon/bg-academia.png" />


                               <asp:ImageButton ID="ImageButton_mesa_076_1" runat="server" Style="left: 61px; position: absolute;
                            top: 455px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_076_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_076_2" runat="server" Style="left: 78px; position: absolute;
                            top: 470px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_076_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_076_3" runat="server" Style="left: 61px; position: absolute;
                            top: 490px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_076_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_076_4" runat="server" Style="left: 42px; position: absolute;
                            top: 475px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_076_4_Click" />


                              <asp:ImageButton ID="ImageButton_mesa_077_1" runat="server" Style="left: 61px; position: absolute;
                            top: 512px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_077_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_077_2" runat="server" Style="left: 78px; position: absolute;
                            top: 527px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_077_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_077_3" runat="server" Style="left: 61px; position: absolute;
                            top: 543px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_077_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_077_4" runat="server" Style="left: 42px; position: absolute;
                            top: 530px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_077_4_Click" />


                               <asp:ImageButton ID="ImageButton_mesa_078_1" runat="server" Style="left: 120px; position: absolute;
                            top: 452px; width: 10px; height: 11px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_078_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_078_2" runat="server" Style="left: 143px; position: absolute;
                            top: 465px; width: 10px; height: 11px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_078_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_078_3" runat="server" Style="left: 123px; position: absolute;
                            top: 483px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_078_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_078_4" runat="server" Style="left: 104px; position: absolute;
                            top: 469px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_078_4_Click" />



                               <asp:ImageButton ID="ImageButton_mesa_079_1" runat="server" Style="left: 123px; position: absolute;
                            top: 508px; width: 10px; height: 11px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_079_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_079_2" runat="server" Style="left: 142px; position: absolute;
                            top: 524px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_079_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_079_3" runat="server" Style="left: 125px; position: absolute;
                            top: 539px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_079_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_079_4" runat="server" Style="left: 103px; position: absolute;
                            top: 526px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_079_4_Click" />

 
                            <asp:ImageButton ID="ImageButton_mesa_080_1" runat="server" Style="left: 186px; position: absolute;
                            top: 448px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_080_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_080_2" runat="server" Style="left: 205px; position: absolute;
                            top: 463px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_080_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_080_3" runat="server" Style="left: 186px; position: absolute;
                            top: 480px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_080_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_080_4" runat="server" Style="left: 166px; position: absolute;
                            top: 466px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_080_4_Click" />



                            <asp:ImageButton ID="ImageButton_mesa_081_1" runat="server" Style="left: 188px; position: absolute;
                            top: 503px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_081_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_081_2" runat="server" Style="left: 206px; position: absolute;
                            top: 518px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_081_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_081_3" runat="server" Style="left: 185px; position: absolute;
                            top: 535px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_081_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_081_4" runat="server" Style="left: 169px; position: absolute;
                            top: 521px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_081_4_Click" />


                          <asp:ImageButton ID="ImageButton_mesa_082_1" runat="server" Style="left:250px; position: absolute;
                            top:  443px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_082_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_082_2" runat="server" Style="left: 268px; position: absolute;
                            top:  461px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_082_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_082_3" runat="server" Style="left: 249px; position: absolute;
                            top:  477px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_082_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_082_4" runat="server" Style="left: 230px; position: absolute;
                            top: 462px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_082_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_083_1" runat="server" Style="left: 250px; position: absolute;
                            top: 500px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_083_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_083_2" runat="server" Style="left:272px; position: absolute;
                            top: 516px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_083_2_Click" />   
                            <asp:ImageButton ID="ImageButton_mesa_083_3" runat="server" Style="left: 250px; position: absolute;
                            top: 535px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_083_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_083_4" runat="server" Style="left: 232px; position: absolute;
                            top: 519px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_083_4_Click" />                               

                         
                     <asp:ImageButton ID="ImageButton_mesa_084_1" runat="server" Style="left: 310px; position: absolute;
                            top: 321px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_084_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_084_2" runat="server" Style="left:330px; position: absolute;
                            top: 336px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_084_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_084_3" runat="server" Style="left: 311px; position: absolute;
                            top: 353px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_084_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_084_4" runat="server" Style="left: 289px; position: absolute;
                            top: 341px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_084_4_Click" />

                          
                      <asp:ImageButton ID="ImageButton_mesa_085_1" runat="server" Style="left: 314px; position: absolute;
                                top: 441px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_085_1_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_085_2" runat="server" Style="left: 333px; position: absolute;
                                top: 455px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_085_2_Click" />

                             <asp:ImageButton ID="ImageButton_mesa_085_3" runat="server" Style="left: 313px; position: absolute;
                                top: 473px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_085_3_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_085_4" runat="server" Style="left: 292px; position: absolute;
                                top: 460px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" OnClick="ImageButton_mesa_085_4_Click" />


 
                             
                          <asp:ImageButton ID="ImageButton_mesa_086_1" runat="server" Style="left: 315px; position: absolute;
                            top: 497px; height: 11px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_086_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_086_2" runat="server" Style="left: 334px; position: absolute;
                            top: 513px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_086_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_086_3" runat="server" Style="left: 316px; position: absolute;
                            top: 531px; width: 9px; height: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_086_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_086_4" runat="server" Style="left: 297px; position: absolute;
                            top: 517px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_086_4_Click" />


                        
                            <asp:ImageButton ID="ImageButton_mesa_087_1" runat="server" Style="left: 372px; position: absolute;
                            top: 319px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_087_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_087_2" runat="server" Style="left:392px; position: absolute;
                            top: 332px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_087_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_087_3" runat="server" Style="left: 373px; position: absolute;
                            top: 351px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_087_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_087_4" runat="server" Style="left: 355px; position: absolute;
                            top: 337px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_087_4_Click" />

                             <asp:ImageButton ID="ImageButton_mesa_088_1" runat="server" Style="left: 375px; position: absolute;
                            top: 438px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_088_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_088_2" runat="server" Style="left: 396px; position: absolute;
                            top: 453px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_088_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_088_3" runat="server" Style="left: 377px; position: absolute;
                            top: 471px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_088_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_088_4" runat="server" Style="left: 359px; position: absolute;
                            top: 457px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_088_4_Click" />


                            <asp:ImageButton ID="ImageButton_mesa_089_1" runat="server" Style="left: 377px; position: absolute;
                            top: 494px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_089_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_089_2" runat="server" Style="left: 396px; position: absolute;
                            top: 509px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_089_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_089_3" runat="server" Style="left: 378px; position: absolute;
                            top: 527px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_089_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_089_4" runat="server" Style="left: 357px; position: absolute;
                            top: 512px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_089_4_Click" />

                            
                            <asp:ImageButton ID="ImageButton_mesa_090_1" runat="server" Style="left: 436px; position: absolute;
                            top: 316px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_090_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_090_2" runat="server" Style="left: 454px; position: absolute;
                            top: 329px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_090_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_090_3" runat="server" Style="left: 437px; position: absolute;
                            top: 347px; width: 10px; height: 11px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_090_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_090_4" runat="server" Style="left: 419px; position: absolute;
                            top: 333px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_090_4_Click" />

                       <asp:ImageButton ID="ImageButton_mesa_091_1" runat="server" Style="left: 440px; position: absolute;
                            top: 437px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_091_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_091_2" runat="server" Style="left: 458px; position: absolute;
                            top: 451px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_091_2_Click" />
                                <asp:ImageButton ID="ImageButton_mesa_091_3" runat="server" Style="left: 441px; position: absolute;
                            top: 464px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_091_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_091_4" runat="server" Style="left: 422px; position: absolute;
                            top: 453px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_091_4_Click" />
                          
                            <asp:ImageButton ID="ImageButton_mesa_092_1" runat="server" Style="left: 442px; position: absolute;
                            top: 490px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_092_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_092_2" runat="server" Style="left: 462px; position: absolute;
                            top: 504px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_092_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_092_3" runat="server" Style="left: 442px; position: absolute;
                            top: 523px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_092_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_092_4" runat="server" Style="left: 423px; position: absolute;
                            top: 508px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_092_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_093_1" runat="server" Style="left: 498px; position: absolute;
                            top: 312px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_093_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_093_2" runat="server" Style="left: 519px; position: absolute;
                            top: 325px; width: 10px; height: 11px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_093_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_093_3" runat="server" Style="left: 498px; position: absolute;
                            top: 346px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_093_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_093_4" runat="server" Style="left: 479px; position: absolute;
                            top: 329px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_093_4_Click" />


  
                            <asp:ImageButton ID="ImageButton_mesa_094_1" runat="server" Style="left: 502px; position: absolute;
                            top: 429px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_094_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_094_2" runat="server" Style="left: 523px; position: absolute;
                            top: 447px; width: 10px; height: 11px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_094_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_094_3" runat="server" Style="left: 500px; position: absolute;
                            top: 465px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_094_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_094_4" runat="server" Style="left: 482px; position: absolute;
                            top: 452px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_094_4_Click" />



                            <asp:ImageButton ID="ImageButton_mesa_095_1" runat="server" Style="left: 503px; position: absolute;
                            top: 488px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_095_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_095_2" runat="server" Style="left: 523px; position: absolute;
                            top: 502px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_095_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_095_3" runat="server" Style="left: 507px; position: absolute;
                            top: 520px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_095_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_095_4" runat="server" Style="left: 484px; position: absolute;
                            top: 508px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_095_4_Click" />



                            <asp:ImageButton ID="ImageButton_mesa_096_1" runat="server" Style="left: 588px; position: absolute;
                            top: 301px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_096_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_096_2" runat="server" Style="left: 607px; position: absolute;
                            top: 317px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_096_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_096_3" runat="server" Style="left: 589px; position: absolute;
                            top: 336px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_096_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_096_4" runat="server" Style="left: 571px; position: absolute;
                            top: 322px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_096_4_Click" />


                            <asp:ImageButton ID="ImageButton_mesa_097_1" runat="server" Style="left: 593px; position: absolute;
                            top: 418px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_097_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_097_2" runat="server" Style="left: 610px; position: absolute;
                            top: 433px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_097_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_097_3" runat="server" Style="left: 595px; position: absolute;
                            top: 449px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_097_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_097_4" runat="server" Style="left: 572px; position: absolute;
                            top: 438px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_097_4_Click" />


                            <asp:ImageButton ID="ImageButton_mesa_098_1" runat="server" Style="left: 594px; position: absolute;
                            top: 476px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_098_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_098_2" runat="server" Style="left: 617px; position: absolute;
                            top: 489px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_098_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_098_3" runat="server" Style="left: 597px; position: absolute;
                            top: 508px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_098_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_098_4" runat="server" Style="left: 574px; position: absolute;
                            top: 494px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_098_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_099_1" runat="server" Style="left: 647px; position: absolute;
                            top: 299px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_099_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_099_2" runat="server" Style="left: 665px; position: absolute;
                            top: 315px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_099_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_099_3" runat="server" Style="left: 650px; position: absolute;
                            top: 336px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_099_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_099_4" runat="server" Style="left: 629px; position: absolute;
                            top: 317px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_099_4_Click" />



                            <asp:ImageButton ID="ImageButton_mesa_100_1" runat="server" Style="left: 650px; position: absolute;
                            top: 413px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0100_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_100_2" runat="server" Style="left: 671px; position: absolute;
                            top: 427px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0100_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_100_3" runat="server" Style="left: 654px; position: absolute;
                            top: 448px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0100_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_100_4" runat="server" Style="left: 634px; position: absolute;
                            top: 431px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0100_4_Click" />


                                  <asp:ImageButton ID="ImageButton_mesa_101_1" runat="server" Style="left: 655px; position: absolute;
                            top: 469px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0101_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_101_2" runat="server" Style="left: 674px; position: absolute;
                            top: 489px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0101_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_101_3" runat="server" Style="left: 658px; position: absolute;
                            top: 504px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0101_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_101_4" runat="server" Style="left: 636px; position: absolute;
                            top: 489px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0101_4_Click" />
 
                            <asp:ImageButton ID="ImageButton_mesa_102_1" runat="server" Style="left: 714px; position: absolute;
                            top: 293px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0102_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_102_2" runat="server" Style="left: 733px; position: absolute;
                            top: 312px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0102_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_102_3" runat="server" Style="left: 715px; position: absolute;
                            top: 330px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0102_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_102_4" runat="server" Style="left: 696px; position: absolute;
                            top: 313px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0102_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_103_1" runat="server" Style="left: 718px; position: absolute;
                            top: 406px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0103_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_103_2" runat="server" Style="left: 738px; position: absolute;
                            top: 422px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0103_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_103_3" runat="server" Style="left: 720px; position: absolute;
                            top: 443px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0103_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_103_4" runat="server" Style="left: 698px; position: absolute;
                            top: 428px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0103_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_104_1" runat="server" Style="left: 722px; position: absolute;
                            top: 464px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0104_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_104_2" runat="server" Style="left: 742px; position: absolute;
                            top: 481px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0104_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_104_3" runat="server" Style="left: 722px; position: absolute;
                            top: 499px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0104_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_104_4" runat="server" Style="left: 702px; position: absolute;
                            top: 486px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0104_4_Click" />
<%--
                            <asp:ImageButton ID="ImageButton_mesa_105_1" runat="server" Style="left: 222px; position: absolute;
                            top: 483px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0105_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_105_2" runat="server" Style="left: 238px; position: absolute;
                            top: 492px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0105_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_105_3" runat="server" Style="left: 223px; position: absolute;
                            top: 506px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0105_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_105_4" runat="server" Style="left: 208px; position: absolute;
                            top: 494px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0105_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_106_1" runat="server" Style="left: 259px; position: absolute;
                            top: 483px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0106_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_106_2" runat="server" Style="left: 275px; position: absolute;
                            top: 492px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0106_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_106_3" runat="server" Style="left: 258px; position: absolute;
                            top: 503px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0106_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_106_4" runat="server" Style="left: 247px; position: absolute;
                            top: 497px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0106_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_107_1" runat="server" Style="left: 296px; position: absolute;
                            top: 481px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0107_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_107_2" runat="server" Style="left: 310px; position: absolute;
                            top: 490px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0107_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_107_3" runat="server" Style="left: 296px; position: absolute;
                            top:502px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0107_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_107_4" runat="server" Style="left: 283px; position: absolute;
                            top: 496px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0107_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_108_1" runat="server" Style="left: 332px; position: absolute;
                            top: 482px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0108_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_108_2" runat="server" Style="left: 347px; position: absolute;
                            top: 490px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0108_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_108_3" runat="server" Style="left: 332px; position: absolute;
                            top: 504px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0108_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_108_4" runat="server" Style="left: 319px; position: absolute;
                            top: 494px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0108_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_109_1" runat="server" Style="left: 369px; position: absolute;
                            top: 481px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0109_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_109_2" runat="server" Style="left: 385px; position: absolute;
                            top: 489px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0109_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_109_3" runat="server" Style="left: 369px; position: absolute;
                            top: 501px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0109_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_109_4" runat="server" Style="left: 356px; position: absolute;
                            top: 494px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0109_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_110_1" runat="server" Style="left: 405px; position: absolute;
                            top: 482px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0110_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_110_2" runat="server" Style="left: 421px; position: absolute;
                            top: 486px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0110_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_110_3" runat="server" Style="left: 405px; position: absolute;
                            top: 502px; " ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0110_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_110_4" runat="server" Style="left: 392px; position: absolute;
                            top: 493px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0110_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_111_1" runat="server" Style="left: 444px; position: absolute;
                            top: 480px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0111_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_111_2" runat="server" Style="left: 456px; position: absolute;
                            top: 488px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0111_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_111_3" runat="server" Style="left: 442px; position: absolute;
                            top: 501px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0111_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_111_4" runat="server" Style="left: 430px; position: absolute;
                            top: 491px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0111_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_112_1" runat="server" Style="left: 478px; position: absolute;
                            top: 480px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0112_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_112_2" runat="server" Style="left: 495px; position: absolute;
                            top: 489px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0112_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_112_3" runat="server" Style="left: 479px; position: absolute;
                            top: 500px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0112_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_112_4" runat="server" Style="left: 465px; position: absolute;
                            top: 490px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0112_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_113_1" runat="server" Style="left: 533px; position: absolute;
                            top: 472px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0113_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_113_2" runat="server" Style="left: 546px; position: absolute;
                            top: 481px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0113_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_113_3" runat="server" Style="left: 533px; position: absolute;
                            top: 496px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0113_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_113_4" runat="server" Style="left: 517px; position: absolute;
                            top: 486px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0113_4_Click" />

                            <asp:ImageButton ID="ImageButton_mesa_114_1" runat="server" Style="left: 566px; position: absolute;
                            top: 473px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0114_1_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_114_2" runat="server" Style="left: 582px; position: absolute;
                            top: 483px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0114_2_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_114_3" runat="server" Style="left: 567px; position: absolute;
                            top: 497px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0114_3_Click" />
                            <asp:ImageButton ID="ImageButton_mesa_114_4" runat="server" Style="left: 553px; position: absolute;
                            top: 486px; width: 10px;" ImageUrl="~/images/cadeiraLivre.png" 
                                onclick="ImageButton_mesa_0114_4_Click" />--%>
                        </div>
                        <spam style="color: White;"> Mapa apenas ilustrativo. As mesas poderão sofrer alterações em sua localização no dia do evento.</spam>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td valign="top" align="left">
                        <table class="nav-justified">
                            <tr style="height: 20px;">
                                <td align="center">
                                    <asp:Label  ID="lblOrientacao" runat="server" CssClass="label_ingresso_vermelho" Text="Primeiro escolha o tipo de ingresso que queira comprar e em seguida clique na cadeira desejada"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label  ID="lblTipoIngresso" runat="server" CssClass="label" Text="Comprar tipo de ingresso:"></asp:Label>
                                </td>
                                <td align="center">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:RadioButtonList ID="radTipoCadeira" runat="server" CssClass="radioButton" RepeatDirection="Horizontal">
                                        <asp:ListItem>Inteiro</asp:ListItem>
                                        <asp:ListItem>Meio Adolescente</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label  ID="lblValor" runat="server" CssClass="label" 
                                        Text="Escolha o valor:" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:DropDownList ID="dropValorIngresso" runat="server" CssClass="dropdown" 
                                        Width="400px" Visible="False">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label  ID="lblSocio" runat="server" CssClass="label" Text="Saldo Ingressos Sócio"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label  ID="lblSocio_valor" runat="server" CssClass="label_saldo" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label  ID="lblNaoSocio" runat="server" CssClass="label" Text="Saldo Ingressos Não Sócio"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label  ID="lblNaoSocio_valor" runat="server" CssClass="label_saldo" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left">
                                <td colspan="2">
                                    <asp:Label  ID="lblNumeroIngressosCadeira" runat="server" CssClass="label_ingresso"
                                        Text="Regra: Número de ingressos disponíveis<br/>em mesa por cota/título: 6"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" height="20">
                                    <asp:Label  ID="lblLegenda" runat="server" CssClass="label" Text="Legenda:"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left">
                                <td align="left" colspan="2">
                                    <table class="nav-justified">
                                        <tr>
                                            <td align="left">
                                                <img alt="" class="circleborder" src="../../images/cadeiraLivre.png" />
                                            </td>
                                            <td>
                                                <asp:Label  ID="lblLegenda1" runat="server" CssClass="label_legenda" Text="Disponível"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <img alt="" class="style1" src="../../images/cadeiraProcessoCompra.png" />
                                            </td>
                                            <td>
                                                <asp:Label  ID="Label1" runat="server" CssClass="label_legenda" Text="Selecionada"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <img alt="" class="style1" src="../../images/cadeiraBloqueada.png" />
                                            </td>
                                            <td>
                                                <asp:Label  ID="Label3" runat="server" CssClass="label_legenda" Text="Em processo de compra por outro cliente"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style1">
                                                <img alt="" class="style1" src="../../images/cadeiraVendida.png" />
                                            </td>
                                            <td class="auto-style1">
                                                <asp:Label  ID="Label2" runat="server" CssClass="label_legenda" Text="Vendida"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2" height="20">
                                                <asp:LinkButton ID="LinkButtonAbrirMapaInteira" runat="server" CssClass="linkBotao"
                                                    OnClientClick="parent.jQuery.fancybox.close();" Width="100px">Avançar</asp:LinkButton>
                                                </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

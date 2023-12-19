<%@ Page Language="C#" AutoEventWireup="true" Title="PIC" CodeFile="QRCode.aspx.cs"
    Inherits="administrador_QRCode" EnableEventValidation="false" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>

<html xmlns="http://www.w3.org/1999/xhtml" id="html" runat="server">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" media="print" href="../css/print.css" />
</head>
<body style="background-attachment: fixed;">
    <form id="form1" runat="server">
    <div id="Box_divQRCode">
        <%--       folha A4 2480 px de largura 
                    3508 px de altura --%>
        <%--Inicio do Repeater ( cada valor é chamado no container)--%>
        <asp:Repeater ID="rptTicketsIngresso" runat="server">
            <ItemTemplate>
            <%--INICIO DO LAYOUT DO INGRESSO--%>
                <%--<div id="divIngressoPrint" style="padding-top: 100px;padding-left: 70px; page-break-inside: avoid; float: left;
                    font-family: verdana, sans-serif;">--%>
                    <%--<table id="TabelaIngresso" style='border: 1px; border-color: Gray; border-style: solid;
                        page-break-iside: avoid;' cellspacing="0" bgcolor="#FFFFFF">
                        <tr id="Linha_Informaçao">
                            <td id="td_Informacoes" valign="top" style="border-right-width: thin; border-right-color: black;
                                border-right-style: solid; width: 300 px; height: 100px; min-width: 300px; max-width: 300px;
                                min-height: 100px; max-height: 100px;">
                                <img style="padding: 10px;" id="cabecalhoIngressos1" runat="server" src="~/images/cabecalhoIngressos1.png" />
                                <div id="InformaçoesTicket" style="line-height: 1.9; font-size: 9px; padding-top: 8px;
                                    padding-left: 15px;">
                                    <p>
                                        <b>
                                            <asp:Label  ID="LblTituloNome" runat="server" Text="NOME: "></asp:Label>
                                    </b>
                                    <asp:Label  ID="LblNome" runat="server" Verticalalign="top" Text='<%# DataBinder.Eval(Container.DataItem, "Nome") %>'></asp:Label>
                                    <br />
                                    <b>
                                        <asp:Label  ID="LblTituloSocio" runat="server" Text="SOCIO: " CssClass="Strong-Text"></asp:Label></b>
                                    <asp:Label  ID="LblSocio" runat="server" Verticalalign="top" Text='<%# DataBinder.Eval(Container.DataItem, "Socio") %>'></asp:Label>
                                    <br />
                                    <b>
                                        <asp:Label  ID="LblTituloCPF" runat="server" Text="CPF: " CssClass="Strong-Text"></asp:Label></b>
                                    <asp:Label  ID="LblCPF" runat="server" Verticalalign="top" Text='<%# DataBinder.Eval(Container.DataItem, "CPF") %>'></asp:Label>
                                    <br />
                                    <b>
                                        <asp:Label  ID="LblTituloVoucher" runat="server" Text="VOUCHER: " CssClass="Strong-Text"></asp:Label></b>
                                    <asp:Label  ID="LblVoucher" runat="server" Verticalalign="top" Text='<%# DataBinder.Eval(Container.DataItem, "IDVenda") %>'></asp:Label>
                                    <br />
                                    <b>
                                        <asp:Label  ID="LblTituloNIngresso" runat="server" Text="Nº INGRESSO: " CssClass="Strong-Text"></asp:Label></b>
                                    <asp:Label  ID="LblNIngresso" runat="server" Verticalalign="top" Text='<%# DataBinder.Eval(Container.DataItem, "Ticket") %>'></asp:Label>
                                    <asp:Label  ID="Label1" runat="server" Text="(v2)" style="text-align:right; font-size:10px; padding-left:50px; "/>
                                    </p>
                                    <p>
                                        <b>
                                            <asp:Label  ID="LblTituloLocal" runat="server" Text="LOCAL DO EVENTO: " Style="padding: 10px 0;"
                                                CssClass="Strong-Text"></asp:Label></b>
                                        <asp:Label  ID="LblLocal" runat="server" Verticalalign="top" Text='<%# DataBinder.Eval(Container.DataItem, "EnderecoEvento") %>'></asp:Label>
                                        <br />
                                        <b>
                                            <asp:Label  ID="LblTituloHorario" runat="server" Text="HORÁRIO: " Style="padding: 10px 0;"
                                                CssClass="Strong-Text"></asp:Label></b>
                                        <asp:Label  ID="LblHorario" runat="server" Verticalalign="top" Style="padding-right: 35px;"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "Hora") %>'></asp:Label>
                                        <b>
                                            <asp:Label  ID="LblTituloData" runat="server" Text="DATA: " Style="padding: 10 0px;"
                                                CssClass="Strong-Text"></asp:Label></b>
                                        <asp:Label  ID="LblData" runat="server" Verticalalign="top" Text='<%# DataBinder.Eval(Container.DataItem, "Data") %>'></asp:Label>
                                        <br />
                                        <b>
                                            <asp:Label  ID="LblTituloValor" runat="server" Text="VALOR: " CssClass="Strong-Text"></asp:Label></b>
                                        <asp:Label  ID="LblR" runat="server" Text="R$ " CssClass="Strong-Text"></asp:Label>
                                        <asp:Label  ID="LblValor" runat="server" Verticalalign="top" Style="padding-right: 51px;"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "Valor") %>'></asp:Label>
                                        <b>
                                            <asp:Label  ID="LblTituloAvulso" runat="server" Text="AVULSO: " Style="padding: 10px 0;"
                                                CssClass="Strong-Text"></asp:Label></b>
                                        <asp:Label  ID="LblAvulso" runat="server" Verticalalign="top" Text='<%# DataBinder.Eval(Container.DataItem, "Avulso") %>'></asp:Label>
                                        <br />
                                        <b>
                                            <asp:Label  ID="LbLTituloSetor" runat="server" Text="SETOR: " Style="padding: 10px 0;"
                                                CssClass="Strong-Text"></asp:Label></b>
                                        <asp:Label  ID="LblSetor" runat="server" Verticalalign="top" Text='<%# DataBinder.Eval(Container.DataItem, "Setor") %>'></asp:Label>
                                        <br />
                                        <b>
                                            <asp:Label  ID="LblTituloMesa" runat="server" Text="MESA: " Style="padding: 10px 0;"
                                                CssClass="Strong-Text"></asp:Label></b>
                                        <asp:Label  ID="LblMesa" runat="server" Verticalalign="top" Text='<%# DataBinder.Eval(Container.DataItem, "Mesa") %>'></asp:Label>
                                        <br />
                                        <b>
                                            <asp:Label  ID="LblTituloAssento" runat="server" Text="ASSENTO: " Style="padding: 10px 0;"
                                                CssClass="Strong-Text"></asp:Label></b>
                                        <asp:Label  ID="LblAssento" runat="server" Verticalalign="top" Text='<%# DataBinder.Eval(Container.DataItem, "Cadeira") %>'></asp:Label>
                                    </p>
                                </div>
                            </td>
                            <td id="Orientação" valign="top" style="width: 310px; min-width: 310px; max-width: 310px;">
                                <img style="padding-left: 50px; padding-top: 10px; padding-bottom: 10px" id="ImgLogo"
                                    runat="server" src="~/images/cabecalhoIngressos2.png" /><br />
                                <ul type="disc" style=" margin-bottom: 0px; padding-left: 30px; font-size: 8px;
                                    page-break-inside: avoid; text-align: justify; min-width: 270px; max-width: 270px;
                                    width: 270px;">
                                    <li style="margin-bottom: 0px;">Esse ingresso garantirá sua entrada no evento do PIC.
                                        Ele possui um QR Code que é único e intransferível.</li><br />
                                    <li style="margin-bottom: 0px;">Caso tenha comprado mais de um ingresso, não esqueça
                                        de separá-los. Você deverá ter um e-ticket para cada um.</li><br />
                                    <li style="margin-bottom: 0px;">Para meio ingresso, no dia do evento, será necessário
                                        a apresentação de um documento de indentidade que comprove a sua idade, e a presença
                                        de um responsável legal como acompanhante. O acesso de meio ingresso se dará por
                                        catracas específicas.</li><br />
                                    <li style="margin-bottom: 0px;">Caso você tenha direito a estacionamento, um outro ticket
                                        específico deverá ser impresso.</li><br />
                                    <li style="margin-bottom: 0px;"><b>Importante:</b> Não faça cópias desse ingresso. Ele
                                        é impresso apenas através de sua senha e somente o primeiro a passar nos nossos
                                        leitores terá acesso ao evento. </li>
                                    <br />
                                    <li style="margin-bottom: 0px;">O PIC não se responsabiliza por ingressos que tenham
                                        sido reimpressos.</li><br />                                    
                                </ul>
                            </td>
                        </tr>
                        <tr bgcolor='<%# DataBinder.Eval(Container.DataItem, "CorIngresso") %>' style='<%# "background-color:" + DataBinder.Eval(Container.DataItem, "CorIngresso") + ";" %>'>
                            <td style="border-right-width: thin; border-right-color: black; border-right-style: solid;">
                                <div id="divTipo" valign="center" style="padding-left: 15px; height: 22px;
                                    padding-top: 6px;">
                                    <b>
                                        <asp:Label  ID="LblTituloTipo" runat="server" Text="TIPO: " Style="font-size: 11px;"
                                            CssClass="Strong-Text"></asp:Label></b>
                                    <asp:Label  ID="LblTipo" runat="server" Verticalalign="top" Style="font-size: 11px;"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "TipoSocio") %>'></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div style="font-size: 8px; page-break-inside: avoid; text-align: justify; height: 22px;">
                                    <div style="min-width: 300px; max-width: 300px; width: 300px;">
                                        <ul type="disc" style="padding-left: 30px;">
                                            <li style="margin-bottom: 0px;">Classificação livre. É expressamente probida a ingestão
                                                de bebida alcoólica por menores de 18 anos.</li><br />
                                        </ul>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-right-width: thin; border-right-color: black; border-right-style: solid;">
                                <div style="padding: 0px; padding-bottom:1px;">
                                    <img align="left" valign="bottom" id="ImgAtencao" runat="server" src="~/images/Rodape_Atencao.png" />
                                    <asp:Image ID="imgQrCode" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "QRUrl")%>' />
                                </div>
                            </td>
                            <td>
                                <img align="left" style="padding: 10px" id="ImgLogo_Evento" runat="server" src='<%# DataBinder.Eval(Container.DataItem, "EventoImagem") %>' />
                            </td>
                        </tr>
                    </table>
                </div>--%>

                <div id="divIngressoPrint" style="padding-left: 30px; float: left; page-break-inside: avoid;">
                <asp:Image ID="imgQrCode" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "QRUrl")%>' />
                </div>

                <%--FIM DO LAYOUT DO INGRESSO--%>
            </ItemTemplate>
        </asp:Repeater>
        <%--fim do Repeater--%>
    </div>
    </form>
</body>
</html>

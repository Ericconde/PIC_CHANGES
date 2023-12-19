<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ticketEstacionamento.aspx.cs"
    Title="PIC" Inherits="cliente_TicketEstacionamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" media="print" href="../css/print.css" />
</head>
<body>
    <form id="formEstacionamento" runat="server">
    <div id="Box_TicketEstacionamento" runat="server">
        <%--        <div id="OcultarDIV" class="OcultarDIV" runat="server">
            <br />
            &nbsp;<asp:Button ID="botaoImprimir" runat="server" CssClass="botao" OnClick="cmdImprimir_Click"
                TabIndex="16" Text="Imprimir" />
            <hr />
        </div>--%>
        <asp:Repeater ID="rptTicketsEstacionamento" runat="server">
            <ItemTemplate>
                <%--INICIO DO LAYOUT--%>
                <div style="padding-top: 30px;padding-left: 80px; page-break-inside: avoid;">
                    <table id="TabelaEstacionamento" style="width: 403px;" cellspacing="0">
                        <tr id="Linha_Dados" style="padding-left: 100px;">
                            <img id="imgcabeçalhoEstacionamento" runat="server" src="~/images/cabeçalhoEstacionamento.png" /><br />
                            <div style="line-height: 1.6; font-size: 12px; font-family: verdana, sans-serif;
                                background-color: #D8D8D8; width: 375px; padding-left: 27px; padding-bottom: 17px;
                                padding-top: 15px;">
                                <b>
                                    <asp:Label  ID="LblTituloNome" runat="server" Text="NOME: "></asp:Label>
                                </b>
                                <asp:Label  ID="LblNome" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Nome") %>'
                                    Verticalalign="top"></asp:Label>
                                <br />
                                <b>
                                    <asp:Label  ID="LblTituloSocio" runat="server" Text="SÓCIO: " CssClass="Strong-Text"></asp:Label></b>
                                <asp:Label  ID="LblSocio" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NumeroCota") %>'
                                    Verticalalign="top"></asp:Label>
                                <br />
                                <b>
                                    <asp:Label  ID="LblTituloCPF" runat="server" Text="CPF: " CssClass="Strong-Text"></asp:Label></b>
                                <asp:Label  ID="LblCPF" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CPF") %>'
                                    Verticalalign="top"></asp:Label>
                                <br />
                                <b>
                                    <asp:Label  ID="LblTituloVoucher" runat="server" Text="VOUCHER: " CssClass="Strong-Text"></asp:Label></b>
                                <asp:Label  ID="LblVoucher" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IDVenda") %>'
                                    Verticalalign="top"></asp:Label>
                                <br />
                                <b>
                                    <asp:Label  ID="Label1" runat="server" Text="Nº INGRESSO: " CssClass="Strong-Text"></asp:Label></b>
                                <asp:Label  ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Ticket") %>'
                                    Verticalalign="top"></asp:Label>
                                <br />
                                <b>
                                    <asp:Label  ID="LblTituloLocal" runat="server" Text="LOCAL: " Style="padding: 10px 0;"
                                        CssClass="Strong-Text"></asp:Label></b>
                                <asp:Label  ID="LblLocal" runat="server" Verticalalign="top" Text='Rua Ilha Grande, 555'></asp:Label>
                                <br />
                                <b>
                                    <asp:Label  ID="LblTituloHorario" runat="server" Text="HORÁRIO: " Style="padding: 10px 0;"
                                        CssClass="Strong-Text"></asp:Label></b>
                                <asp:Label  ID="LblHorario" runat="server" Verticalalign="top" Style="padding-right: 50px;"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "HoraEvento") %>'></asp:Label>
                                <b>
                                    <asp:Label  ID="LblTituloData" runat="server" Text="DATA: " Style="padding: 10 0px;"
                                        CssClass="Strong-Text"></asp:Label></b>
                                <asp:Label  ID="LblData" runat="server" Verticalalign="top" Text='<%# DataBinder.Eval(Container.DataItem, "DataEvento") %>'></asp:Label>
                                <br />
                            </div>
                            <img id="imgEstacionamentoCentro" runat="server" src="~/images/EstacionamentoCentro.png" /><br />
                            <div align="center" style="background-color: #D8D8D8; width: 403px;
                                height: 140px;">
                                <br />
                                <div runat="server" align="center" style="overflow: hidden; width: 170px; background-color: White;">
                                    <asp:Image ID="imgQRCode" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "IdentidadeEletronica")%>' />
                                </div>
                                <br />
                                <asp:Label  ID="LblRodape" runat="server" Text="Tel PIC: (31) 3516-8282. Copyright 2019 PIC. Todos os direitos reservados."
                                    Style="color: #585858; font-size: 8px; padding: 10 0px; vertical-align: bottom;"
                                    CssClass="Strong-Text"></asp:Label>
                            </div>
                        </tr>
                    </table>
                </div>
                <%--FIM DO LAYOUT--%>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>

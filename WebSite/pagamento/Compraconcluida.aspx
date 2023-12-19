<%@ Page Title="" Language="C#" MasterPageFile="~/controls/Cliente.master" AutoEventWireup="true" CodeFile="Compraconcluida.aspx.cs" Inherits="cliente_Compraconcluida" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div>
                <style type="text/css">
                    .tentar {
                        background-color: #0089fe;
                        color: white;
                        border: 0px solid;
                        width: 300px;
                        border-radius: 5px;
                        font-size: 12px;
                        font-weight: bold;
                        height: 28px;
                        margin-left: 150px;
                    }

                    .inicial1 {
                        background-color: black;
                        color: white;
                        border: 0px solid;
                        width: 300px;
                        font-size: 12px;
                        font-weight: bold;
                        border-radius: 5px;
                        height: 28px;
                        margin-left: 50px;
                    }
                </style>

                <form action="Compraconcluida.aspx">

                    <center>
                        <h2 style="font-weight: bold; text-align: center; width: 900px; color: #0089fe; margin-bottom: 5vh; margin-top: 5vh;">COMPRA CONCLUÍDA COM SUCESSO</h2>

                        <div style="margin-left: -20px; height: 50px; background-color: rgb(207, 230, 246); margin-bottom: -0.6vh;" class="boxconcluida">
                            <h5 style="font-size: 16px; padding: 10px; text-align: center; font-weight: bold;">IMPRIMA SEU COMPROVANTE AO FINAL DESTA PÁGINA</h5>
                        </div>
                    </center>
                    <div style="margin-left: -20px;margin-top:-12px; background-color: rgb(207, 230, 246); display: block;" class="boxconcluida2">
                        <p></p>
                        <div style="margin-left: 15px; padding: 10px; color: black;">
                            <asp:Label runat="server" ID="lblResumo"></asp:Label>
                        </div>
                    </div>
                    <div style="margin-left: -20px; margin-top:-12px; background-color: rgb(207, 230, 246); display: block;" class="boxconcluida2">
                        <p></p>
                        <div style="margin-left: 15px; padding: 10px; color: black;">
                            <h2 style="font-size: 25px; padding: 10px; text-align: center; font-weight: bold;color:red">ATENÇÃO</h2>
                            <h5 style="font-size: 18px; padding: 10px; text-align: center; font-weight: bold;">Este comprovante não funciona como ingresso para o evento. Para impressão dos ingressos, vá até sua página inicial e, após a data informada nas regras do evento, clique em cada compra para solicitar o envio dos ingressos para seu email cadastrado.<br /> Fique atento: todo ingresso possui um Qrcode que será lido na portaria do evento.</h5>
                        </div>
                    </div>
                    <center>
                        <div style="margin-left: -20px; align-items: center; margin-top: 5vh; margin-bottom: 4.7vh;">
                            <asp:Button runat="server" OnClick="btnGerarRecibo_Click" BackColor="#0089fe" class="inicial1" Text="IMPRIMIR VOUCHER" />
                            <asp:Button runat="server" OnClick="btnInicial_Click" class="inicial1" Text="PÁGINA INICIAL" />
                        </div>
                    </center>






                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />

                </form>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


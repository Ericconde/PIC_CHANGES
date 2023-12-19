<%@ Page Title="" Language="C#" MasterPageFile="~/controls/Cliente.master" AutoEventWireup="true" CodeFile="Compranaoconcluida.aspx.cs" Inherits="cliente_Compranaoconcluida" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>

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

            <form action="Compranaoconcluida.aspx">
                <center>
                    <br />
                    <br />
                    <br />
                    <h2 style="font-weight: bold; text-align: center; width: 900px; color: red; margin-bottom: 5vh; margin-top: 9vh;">COMPRA NÃO CONCLUÍDA</h2>

                    <div style="margin-left: -20px;  background-color: rgb(207, 230, 246)" class="boxnaoconcluida">
                        <h5 style="font-size: 17px; padding: 40px; text-align: center; font-weight: bold;">Sua compra não foi efetivada. Verifique os dados do seu cartão e tente novamente.</h5>
                        <p></p>
                        <p></p>
                        <p></p>
                        <p></p>
                        <h5 style="font-size: 17px; text-align: center; color: red; font-weight: bold;">Atenção: várias tentativas erradas podem ocasionar<br /> o bloqueio do seu cartão pela sua operadora</h5>
                        <br />
                    </div>

                    <div style="margin-left: -20px; margin-top: 5vh; margin-bottom: 4.7vh;">
                        <asp:Button runat="server"  OnClick="btnTentarNovamente_Click" BackColor="#0089fe" class="inicial1" Text="TENTAR NOVAMENTE" />
                        <asp:Button runat="server"  OnClick="btnInicial_Click" class="inicial1" Text="PÁGINA INICIAL" />
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

            </form>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


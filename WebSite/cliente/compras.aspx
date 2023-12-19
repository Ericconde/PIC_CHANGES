<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Cliente.master" AutoEventWireup="true"
    CodeFile="compras.aspx.cs" Inherits="cliente_compras" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <style>


        </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       
        <ContentTemplate>

             <style type="text/css">
                 .ingresso_enviar {
                     color: black;
                     background-color: ghostwhite;
                     border-color: white;
                     border: solid 1px white;
                     border-radius: 10px;
                     text-align: center;
                     width: 140px;
                     height: 30px;
                     font-size: 10px;
                     
                 }
                 </style>
            <form action="compras.aspx">
                <br />
                <div class="container em-alta">
                    <aside>
                        <h3 style="font-family: Calibri"><b>Em alta</b></h3>

                        <div class="controls">
                            <div class="swiper-button-prev"></div>
                            <div class="swiper-button-next"></div>
                        </div>

                        <div class="swiper mySwiper">
                            <div class="swiper-wrapper mb-3 text-center">
                                <asp:Literal runat="server" ID="swipeItens"></asp:Literal>
                            </div>
                        </div>
                </div>

                <div class="compras">
                    <h2>Minhas compras</h2>
                    <div class="table-responsive-sm">
                        <asp:Table Width="100%" class="table table-sm table-primary text-bolder text-center" GridLines="None" runat="server" ID="tblIngressos">
                            <asp:TableRow>
                            </asp:TableRow>
                            <%--<tr>
                                            <td>49659 - Reveillon do PIC 2022 - 2023</td>
                                            <td>04 ingressos disponíveis</td>
                                            <td>Data da compra: 20/09/2022</td>
                                        </tr>
                                        <tr>
                                            <td>50652 Arraía do PIC 2023</td>
                                            <td class="text-danger">04 ingressos ainda não disponíveis</td>
                                            <td>Data da compra: 20/09/2022</td>
                                        </tr>--%>
                            <%--</tbody>--%>
                        </asp:Table>

                        <asp:GridView ID="grdCompras" CssClass="table table-sm table-primary text-bolder text-center" runat="server" AutoGenerateColumns="False"
                            GridLines="None" ShowHeader="false" OnRowCreated="OrderGridView_RowCreated"
                            DataKeyNames="evento">
                            <RowStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:BoundField DataField="idVenda" />
                                <asp:BoundField DataField="evento" />
                                <asp:BoundField DataField="qntd" />
                                <asp:BoundField DataField="dataHora" />
                                <asp:HyperLinkField ControlStyle-CssClass="ingresso_enviar" />

                            </Columns>                            
                            <AlternatingRowStyle CssClass="border-0" />
                        </asp:GridView>
                    </div>
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
                    


              
                </aside>
            </form>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


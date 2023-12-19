<%@ Page Title="" Language="C#" MasterPageFile="~/controls/Cliente.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="cliente_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function AbrirSolicitacaoIngresso(IDVenda) {
            $.fancybox.open({
                href: "../cliente/SolicitacaoIngresso.aspx?IDVenda=" + IDVenda,
                type: "iframe",
                maxWidth: 630,
                minHeight: 530,
                padding: 5
            });
            return false;
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <style type="text/css">
                .swiper {
                    width: 950px;
                }

                .gridviewitem {
                    text-align: center;
                }

                .card .name-profession {
                    display: flex;
                    flex-direction: column;
                    align-items: initial;
                }

                .name-profession .data {
                    font-size: 17px;
                    font-weight: 600;
                    color: #0089fe;
                }

                .minhascompras123 {
                    background-color: black;
                    color: white;
                    display: flex;
                    justify-content: center;
                    align-items: center;
                    font-size: 20px;
                    margin-right: auto;
                    height: 50px;
                    width: 900px;
                    margin-top: 40vh;
                    margin-left: 30px;
                    margin-bottom: -10vh;
                    text-transform: capitalize;
                }

                .name-profession .nome {
                    font-size: 20px;
                    font-weight: bold;
                    color: black;
                }

                .name-profession .descricao {
                    font-size: 10px;
                    font-weight: 600;
                    color: darkgrey;
                }

              

                :root {
                    --swiper-navigation-size: 25px;  
                    border-radius: 100%;
                }

                .swiper-button-prev, .swiper-button-next {
                    background-color: white;
                    background-color: rgba(255, 255, 255, 0.5);
                    border-radius: 50%;                 
                    right: -270px;                  
                    padding: 30px;
                    font-size: 100px;
                    margin-top: -16vh;
                    margin-left: 830px;
                    color: #0089fe !important;
                    fill: black !important;
                    stroke: black !important;
                }


                    .swiper-button-next:hover, .swiper-button-prev:hover {
                        box-shadow: 1px 1px 4px rgba(0, 0, 0, 0.3);
                        opacity: 1;
                        color: #0089fe;
                        font-size: 5px;
                    }
            </style>


            <form action="Default.aspx">
                <div class="box_home">

                    <h1 style="font-weight: bold; color: gray; font-size: 30px; margin-left: -25px; text-transform: capitalize;">&nbsp;&nbsp; Em Alta</h1>

                    <!-- Inicio do carousel  -->
                    <div class="swiper mySwiper container">
                        <div class="swiper-wrapper content">

                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;">
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;">
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;">
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;">
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;">
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;">
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;">
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;">
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;">
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="swiper-button-prev"></div>
                    <div class="swiper-button-next"></div>



                    <!-- final do carousel  -->
                </div>

                <h1 style="visibility: hidden; background-color: white; color: white; width: 900px;">teste</h1>

                <!-- Inicio do gridview  -->
                <div style="margin-top: 20vh; margin-bottom: -170vh">
                    <h1 class="minhascompras123">Minhas Compras</h1>
                    <asp:GridView ID="gvitems" runat="server" AutoGenerateColumns="False" Style="margin-bottom: -30vh; margin-top: 10vh; width: 900px; align-items: center; height: 200px; margin-left: 30px;" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="IDvenda" ItemStyle-CssClass="gridviewitem" HeaderStyle-Width="100px" ItemStyle-Font-Bold="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="white" HeaderText="" ItemStyle-Width="30" ItemStyle-BackColor="#d7ebff" FooterStyle-BackColor="red">
                                <FooterStyle BackColor="white" />
                                <HeaderStyle BackColor="white" Width="100px" HorizontalAlign="Center" BorderColor="White" />
                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Evento" ItemStyle-CssClass="gridviewitem" HeaderStyle-Width="100px" HeaderStyle-BackColor="white" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="false" HeaderText="" ItemStyle-Width="150" ItemStyle-BackColor="#d7ebff" ItemStyle-ForeColor="Black">
                                <HeaderStyle BackColor="white" Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Edicao" ItemStyle-CssClass="gridviewitem" HeaderStyle-Width="100px" HeaderStyle-BackColor="white" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="false" HeaderText="" ItemStyle-Width="150" ItemStyle-BackColor="#d7ebff" ItemStyle-ForeColor="Black">
                                <HeaderStyle BackColor="white" Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DataHoraCompra" ItemStyle-CssClass="gridviewitem" HeaderStyle-Width="100px" HeaderStyle-BackColor="white" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="false" HeaderText="" ItemStyle-Width="150" ItemStyle-BackColor="#d7ebff" ItemStyle-ForeColor="Black">
                                <HeaderStyle BackColor="white" Width="100px" ForeColor="White" HorizontalAlign="Center" />
                                <ItemStyle Width="150px" />
                            </asp:BoundField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" BorderColor="white" ForeColor="Black" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" BorderColor="white" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" BorderColor="white" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" BorderColor="white" />
                        <RowStyle BackColor="#d7ebff" BorderColor="white" Font-Bold="True" ForeColor="Black" BorderWidth="2px" />
                        <SelectedRowStyle BackColor="#E2DED6" ForeColor="white" BorderColor="white" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>

                <!-- fechamento do gridview  -->

            </form>





            <!-- Swiper JS -->
            <script type="text/javascript" src="https://unpkg.com/swiper/swiper-bundle.min.js"></script>

            <!--  Initialize Swiper -->
            <script type="text/javascript">

                var swiper = new Swiper(".mySwiper", {
                    slidesPerView: 3,
                    spaceBetween: 30,
                    slidesPerGroup: 3,
                    loop: true,
                    loopFillGroupWithBlank: true,
                    pagination: {
                        el: ".swiper-pagination",
                        clickable: true,
                    },
                    navigation: {
                        nextEl: ".swiper-button-next",
                        prevEl: ".swiper-button-prev",

                    },
                });

            </script>

            <!--ABERTURA DA DIV BOX HOME-->
            <div style="visibility: hidden; margin-bottom: -19vh" class="box_home">
                <div>
                    <h1>
                        <asp:Label ID="lblUsuario" runat="server" CssClass="nome_usuario" Style="color: #006CB5; font-size: 14px; margin-left: -1px;"></asp:Label>

                        <br>
                        <asp:Label ID="lblIP" runat="server" CssClass="nome_usuario" Style="color: #006CB5; font-size: 14px; margin-top: 10px; margin-left: -1px;"></asp:Label></h1>

                </div>

                <h1>Inicial</h1>
                <div class="texto_box_home_site">

                    <div id="meus_dados">
                        <div class="bg_titulo_dashboard" style="padding: 0 50px;">
                            <div class="titulo_div_dashboard">
                                <asp:Label ID="lblTitulo" runat="server">Dados</asp:Label>
                            </div>
                        </div>
                        <div class="texto_dashboard" runat="server" id="divDadosUsuario">
                            <div class="troca_senha">
                                <a href="../cliente/trocarSenha.aspx">Alterar Senha</a>
                            </div>
                            <div class="link_dashboard">
                                <a href="../cliente/editarCliente.aspx">Editar</a>
                            </div>
                        </div>
                        <div class="icones_deshboard">
                            <img alt="" style="top: -184px;" src="../images/1.png" />
                        </div>
                    </div>



                    <div id="comprovante">
                        <div class="bg_titulo_dashboard" style="padding: 0 45px;">
                            <div class="titulo_div_dashboard">
                                <asp:Label ID="Label1" runat="server">Comprovantes de Compras</asp:Label>
                            </div>
                        </div>
                        <div class="texto_dashboard" runat="server" id="divComprovante"
                            style="position: relative; height: 119px !important; width: 370px !important; z-index: 1; overflow: scroll">
                        </div>
                        <div class="icones_deshboard">
                            <img style="top: -193px;" alt="" src="../images/2.png" />
                        </div>
                    </div>
                    <div id="eventos" style="margin-top: 0px; margin-right: -150px;">
                        <div class="bg_titulo_dashboard" style="padding: 0 68px;">
                            <div class="titulo_div_dashboard">
                                <asp:Label ID="Label2" runat="server">Eventos</asp:Label>
                            </div>
                        </div>
                        <div class="texto_dashboard" style="width: 290px;" runat="server" id="divEventos">
                            <div class="troca_senha">
                                <a href="../cliente/compra.aspx">Comprar</a>
                            </div>
                            <div class="link_dashboard">
                                <a href="../cliente/default.aspx">Mais...</a>
                            </div>
                        </div>
                        <div class="icones_deshboard">
                            <img style="top: -205px;" alt="" src="../images/9.png" />
                        </div>
                    </div>

                    <%--inicio sessão Ingressos--%>
                    <div id="Ingressos" style="margin-left: 410px; margin-top: -203px;">
                        <div class="bg_titulo_dashboard" style="padding: 0 50px;">
                            <div class="titulo_div_dashboard">
                                <asp:Label ID="Label3" runat="server">Ingressos</asp:Label>
                            </div>
                        </div>
                        <div class="texto_dashboard" runat="server" id="divComprasIngresso"
                            style="position: relative; height: 119px !important; width: 370px !important; z-index: 1; overflow: scroll">
                        </div>
                        <div class="icones_deshboard">
                            <img style='top: -193px' alt="" src="../images/QR_Code_icon.png" width="47px" height="52px" />
                        </div>
                    </div>
                    <%--fim sessão Ingressos--%>
                    <div class="clear">
                    </div>
                </div>
                <div runat="server" id="divMensagemSucesso" visible="false" class="alert-box success">
                    <span>Cadastro realizado com sucesso!</span>
                </div>
            </div>

            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

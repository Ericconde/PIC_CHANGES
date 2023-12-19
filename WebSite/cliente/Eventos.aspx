<%@ Page Title="" Language="C#" MasterPageFile="~/controls/Cliente.master" AutoEventWireup="true"
    CodeFile="Eventos.aspx.cs" Inherits="cliente_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <style type="text/css">
                .swiper {
                    width: 950px;
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

                .name-profession .nome {
                    font-size: 20px;
                    font-weight: bold;
                    color: dimgray;
                }

                .name-profession .descricao {
                    font-size: 10px;
                    font-weight: 600;
                    color: darkgrey;
                }


                :root {
                    --swiper-navigation-size: 25px;
                }

                .swiper-button-prev, .swiper-button-next {
                    background-color: white;
                    background-color: rgba(255, 255, 255, 0.5);
                    border-radius: 50%;
                    border-color: grey;
                    border-width: 1px;
                    padding: 30px;
                    font-size: 10px;
                    margin-top: 0.5vh;
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

                <a href="../site/default.aspx">
                    <img alt="" src="../images/ingressospic3.png" style="margin-left: 180px; border-radius: 50%; border: none 1px gray; width: 110px; height: 110px; margin-bottom: 5vh;" />
                </a>
                <a href="../site/default.aspx">
                    <img alt="" src="../images/ingressospic3.png" style="margin-left: 40px; border-radius: 50%; border: none 1px gray; width: 110px; height: 110px; margin-bottom: 5vh;" />
                </a><a href="../site/default.aspx">
                    <img alt="" src="../images/ingressospic3.png" style="margin-left: 40px; border-radius: 50%; border: none 1px gray; width: 110px; height: 110px; margin-bottom: 5vh;" />
                </a>
                <a href="../site/default.aspx">
                    <img alt="" src="../images/ingressospic3.png" style="margin-left: 40px; border-radius: 50%; border: none 1px gray; width: 110px; height: 110px; margin-bottom: 5vh;" />
                </a>
                <h1 style="background-color: white; color: gray; font-size: 17px; margin-right: auto; height: 40px; width: 900px; margin-left: 170px; text-transform: capitalize; margin-top: -2vh;">&nbsp; Festas Junina &nbsp; &nbsp;  &nbsp;  &nbsp;  &nbsp; &nbsp; &nbsp;  &nbsp; Boate  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp;  &nbsp;  &nbsp;  &nbsp; &nbsp; Reveillon &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp;  &nbsp;  &nbsp; Outros</h1>


                <div class="box_home" style="margin-top: 3vh;">

                    <h1 style="font-weight: bold; color: gray; font-size: 22px; margin-left: -25px; text-transform: capitalize;">&nbsp;&nbsp; Neste final de semana</h1>
                    <div class="swiper mySwiper container">
                        <div class="swiper-wrapper content">

                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="swiper-button-prev"></div>
                        <div class="swiper-button-next"></div>
                    </div>


                    <br />
                    <br />
                    <br />
                    <br />
                    <br />



                    <h1 style="font-weight: bold; color: gray; width: 500px; font-size: 22px; margin-top: -4vh; margin-left: -25px; text-transform: capitalize;">&nbsp;&nbsp; Na próxima semana</h1>


                    <div class="swiper mySwiper container">
                        <div class="swiper-wrapper content">

                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="swiper-button-prev"></div>
                        <div class="swiper-button-next"></div>
                    </div>


                    <br />
                    <br />
                    <br />
                    <br />
                    <br />


                    <h1 style="font-weight: bold; color: gray; font-size: 22px; margin-top: -4vh; margin-left: -25px; text-transform: capitalize;">&nbsp;&nbsp; No próximo mês</h1>

                    <div class="swiper mySwiper container" style="margin-bottom: -40vh">
                        <div class="swiper-wrapper content">

                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                            <div class="swiper-slide card">
                                <img src="../images/ingressospic1.png" alt="" style="width: 100%; border-radius: 20px;" />
                                <div class="card-content">
                                    <div class="name-profession">
                                        <span class="data">04 JUN.</span>
                                        <span class="nome">ARRAIÁ DO PIC 2022</span>
                                        <span class="descricao">COM LUAN SANTANA + OPEN BAR E OPEN FOOD</span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="swiper-button-prev"></div>
                        <div class="swiper-button-next"></div>
                    </div>


                </div>

            </form>


            <!-- Swiper JS -->
            <script type="text/javascript" src="https://unpkg.com/swiper/swiper-bundle.min.js"></script>

            <!--  Initialize Swiper -->
            <script type="text/javascript">
                var swiper = new Swiper(".mySwiper", {
                    slidesPerView: 4,
                    spaceBetween: 30,
                    slidesPerGroup: 4,
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

            <script type="text/javascript">
                var swiper = new Swiper(".mySwiper", {
                    slidesPerView: 4,
                    spaceBetween: 30,
                    slidesPerGroup: 4,
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

            <script type="text/javascript">
                var swiper = new Swiper(".mySwiper", {
                    slidesPerView: 4,
                    spaceBetween: 30,
                    slidesPerGroup: 4,
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
            <div style="visibility: hidden; margin-bottom: -20vh" class="box_home">
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

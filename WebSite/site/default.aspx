<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Site.master" AutoEventWireup="true"
    CodeFile="default.aspx.cs" Inherits="site_default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta charset="utf-8" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <body>
                <form action="default.aspx">

                    <div class="container BannerEvento mb-5">
                        <asp:ImageButton ID="img1" OnClick="img1_Click" runat="server" class="img-responsive img"
                            ImageUrl="../images/BANNER_SITE_INGRESSO_ARRAIA_PIC2022.jpg"
                            alt="First slide" />
                    </div>

                    <div class="container em-alta">
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

                        <h2 style="font-family: Calibri"><b>Tutorial</b></h2>
                        <center>
                            <div class="card-deck mb-3 text-center font-75">
                                <div class="card border-0 w-100 p-0">
                                    <a target="_blank" href="../documentos/TUTORIAL_COMPLETO_INGRESSOS_PIC.PDF">
                                        <img class="card-img-top embed-responsive" src="../images/BANNER_TUTORIAL_INGRESSOS_PIC2.JPG" alt="">
                                    </a>
                                </div>
                            </div>
                        </center>
                </form>

                <script src="../scripts/scripts.js"></script>
             
              <script>
                  $(document).ready(function () {
                      function PopUp(string1, string2) {
                          Swal.fire(
                              string1,
                              '',
                              string2
                          )
                      }
                      function PopUpNegativo(string1) {
                          Swal.fire(
                              string1,
                              '',
                              'error'
                          )
                      }
                  });
              </script>
            </body>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

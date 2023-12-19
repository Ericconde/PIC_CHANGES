<%@ Page Title="PIC" Language="C#" MasterPageFile="~/controls/Site.master" AutoEventWireup="true"
    CodeFile="clube.aspx.cs" Inherits="site_clube" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PontoBr" Namespace="PontoBr.CustomWebControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!--ABERTURA DA DIV BOX HOME-->
            <div class="box_home">
                <h1 style="margin:0 23px 15px;">
                    O Clube</h1>
                <div id="conteudo" style="width:100% !important;">
        <p style="margin: 0px; padding: 0px 23px 15px; outline: 0px; text-align: justify; font-size: 13px; color: rgb(114, 114, 114); font-family: 'Trebuchet MS', Arial, Verdana, sans-serif; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 22px; orphans: auto; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px;">
            Foi na Belo Horizonte do final da década de 50 que surgiu a idéia de se 
            construir um novo clube na cidade voltado para a alta sociedade mineira. E não 
            deveria ser apenas um clube que reunisse as pessoas por castas, mas que se 
            tornasse conhecido nacionalmente pela sofisticação e importância social. Um 
            clube diferente de todos os demais inclusive no espaço físico e que garantisse 
            lazer, esporte e recreação de qualidade, sobretudo para os jovens.</p>
       
        <p style="margin: 0px; padding: 0px 23px 15px; outline: 0px; text-align: justify; font-size: 13px; color: rgb(114, 114, 114); font-family: 'Trebuchet MS', Arial, Verdana, sans-serif; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 22px; orphans: auto; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px;">
            A obra seria imponente e três grandes mestres assinaram os projetos: Oscar 
            Niemeyer, Roberto Burle Marx e Cândido Portinari. O local escolhido media 300 
            metros ao longo da Av. Otacílio Negrão de Lima, de frente para a Lagoa da 
            Pampulha e englobando uma área total de 44.000 metros quadrados. Um projeto 
            arrojado, de grandes proporções e que contou com o apoio dos principais 
            dirigentes da cidade e do Estado, o Governador José de Magalhães Pinto, o 
            Prefeito Aminthas de Barros e os vereadores da Câmara Municipal.</p>
       
                    <p style="margin: 0px; padding: 0px 23px 15px; outline: 0px; text-align: justify; font-size: 13px; color: rgb(114, 114, 114); font-family: 'Trebuchet MS', Arial, Verdana, sans-serif; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 22px; orphans: auto; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px;">
                        Tudo foi programado para oferecer um cenário grandioso e exuberante, a começar 
                        pela piscina que, com cento e dois metros de extensão, até hoje é um dos 
                        principais cartões postais do Clube. Niemeyer criou a forma insuperável, Burle 
                        Marx utilizou mudas e flores de todo o mundo para criar um maravilhoso jardim e 
                        Portinari enfeitou, com sua arte esplendorosa, o interior do salão de festas e o 
                        cenário dos jardins, com o quadro “Frevo” e com um painel de azulejos intitulado 
                        “Peixes”. O quadro, quando da fundação do PIC Cidade, foi removido da Pampulha e 
                        instalado no hall principal do novo prédio, na rua Cláudio Manoel, Praça da 
                        Liberdade.</p>
                    <p style="margin: 0px; padding: 0px 23px 15px; outline: 0px; text-align: justify; font-size: 13px; color: rgb(114, 114, 114); font-family: 'Trebuchet MS', Arial, Verdana, sans-serif; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 22px; orphans: auto; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px;">
                        A primeira diretoria que se responsabilizou pela administração na fase de 
                        construção e instalação do PIC teve como presidente José Olímpio de Castro 
                        Filho, então advogado do Banco da Lavoura que, com seus companheiros, deram a 
                        primeira grande contribuição à causa hoje cinquentenária. O PIC foi inaugurado 
                        dez meses após o início das obras, graças a um financiamento obtido junto ao 
                        Banco da Lavoura, que foi garantido com o compromisso do Clube de autorizá-lo a 
                        vender as primeiras cotas para resgatar o empréstimo.</p>
                    <p style="margin: 0px; padding: 0px 23px 15px; outline: 0px; text-align: justify; font-size: 13px; color: rgb(114, 114, 114); font-family: 'Trebuchet MS', Arial, Verdana, sans-serif; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 22px; orphans: auto; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px;">
                        Enquanto era deflagrada a campanha de venda de cotas, a cidade aguardava ansiosa 
                        a inauguração do novo clube, que aconteceu em 26 de janeiro 1961 com a presença 
                        de vinte mil pessoas, entre elas o então senador Juscelino Kubitscheck, 
                        paraninfo da cerimônia. A grande atração da noite foi a presença dos atores 
                        Grande Otelo e Anilza Leoni, que animaram uma festa de glamour e de beleza 
                        ímpar. Por muitos dias os grandes jornais nacionais repetiram a notícia de que 
                        &quot;... foi inaugurado o maior e mais luxuoso clube do Brasil, o PIC.</p>
       
                </div>
            </div>
            </div>
            <!--FECHAMENTO DA DIV BOX HOME-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

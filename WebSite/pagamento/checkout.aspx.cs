using Cielo.Enums;
using Cielo.Requests.Entites;
using Cielo.Requests.Entites.Common;
using Cielo.Responses.Exceptions;
using Controller;
using Model.objetos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class pagamento_checkout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CarregarNomeUsuario();
            PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
            string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
            string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
            int iIDVenda = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["id"], sChave, sVetorInicializacao));
            //int iIDVenda = 24812;

            CarregarDadosProduto(iIDVenda);
            CarregarNumeroParcelas(iIDVenda);
           
        }

        if (ConfigurationManager.AppSettings["TestandoSistema"].ToString() == "Sim")
        {
            txtTitular.Value = "TESTE";
            txtNumeroCartao.Value = "4024007153763191";
            txtValidade.Value = "12/2099";
            txtCodigoSeguranca.Value = "999";
            //txtCPF.Value = "999.999.999-99";
            //txtCEP.Value = "31.230-770";
            //txtDataNascimento.Value = "20/01/1985";
        }
    }
    private void CarregarNomeUsuario()
    {
        try
        {
            usuario Usuario = new usuario();
            Usuario = (usuario)HttpContext.Current.Session["Usuario"];

            var customName = Usuario.Nome.ToUpper().Split(' ')[0].Substring(0, 1) + Usuario.Nome.ToUpper().Split(' ')[1].Substring(0, 1);
            lblCustomName.InnerText = customName;
            lblUsuario.InnerText = Usuario.Nome.ToUpper();
            lblEmail.InnerText = Usuario.Email;
        }
        catch { }
    }

    private void CarregarNumeroParcelas(int iIDVenda)
    {
        double valor = Convert.ToDouble(lblValor.InnerText.Replace("R$ ", ""));
        venda Venda = new venda();
        vendaCTL CVenda = new vendaCTL();
        Venda = CVenda.RetornarVenda(iIDVenda, "1");

        evento Evento = new evento();
        eventoCTL CEvento = new eventoCTL();
        Evento = CEvento.RetornarEdicao(Convert.ToInt32(Venda.IDEdicao));
        usuario UsuarioLogado = (usuario)Session["usuario"];

        usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
        if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
            Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];


        if (Evento.Evento == "Boate")
        {
            if (DateTime.Now < Evento.DateLimite1ParcelaCredito)
                dropParcelas.Items.Add(new ListItem("1x R$ " + String.Format("{0:0.00}", valor), "1"));

            if (UsuarioLogado.Perfil == "Sócio")
            {
                if (DateTime.Now < Evento.DateLimite2ParcelaCredito)
                    dropParcelas.Items.Add(new ListItem("2x R$ " + String.Format("{0:0.00}", valor / 2), "2"));
            }
        }
        else
        {
            if (DateTime.Now < Evento.DateLimite1ParcelaCredito)
                dropParcelas.Items.Add(new ListItem("1x R$ " + String.Format("{0:0.00}", valor), "1"));
            if (DateTime.Now < Evento.DateLimite2ParcelaCredito)
                dropParcelas.Items.Add(new ListItem("2x R$ " + String.Format("{0:0.00}", valor / 2), "2"));

            if (DateTime.Now < Evento.DateLimite3ParcelaCredito && UsuarioLogado.Perfil != "Não Sócio")
                dropParcelas.Items.Add(new ListItem("3x R$ " + String.Format("{0:0.00}", valor / 3), "3"));
            if (DateTime.Now < Evento.DateLimite4ParcelaCredito && UsuarioLogado.Perfil != "Não Sócio")
                dropParcelas.Items.Add(new ListItem("4x R$ " + String.Format("{0:0.00}", valor / 4), "4"));
            if (DateTime.Now < Evento.DateLimite5ParcelaCredito && UsuarioLogado.Perfil != "Não Sócio")
                dropParcelas.Items.Add(new ListItem("5x R$ " + String.Format("{0:0.00}", valor / 5), "5"));
        }

        if (Cliente.Perfil == "Sócio")
        {
            //sIDFormaPagamento = "0";

            //if (DateTime.Now < Evento.DateLimite1ParcelaCredito
            //    || DateTime.Now < Evento.DateLimite2ParcelaCredito
            //    || DateTime.Now < Evento.DateLimite3ParcelaCredito
            //    || DateTime.Now < Evento.DateLimite4ParcelaCredito
            //    || DateTime.Now < Evento.DateLimite5ParcelaCredito)
            //    sIDFormaPagamento = "1";

            if (Cliente.Debito != 1)
            {
                if (DateTime.Now < Evento.DateLimite4ParcelaCota)
                {
                    dropParcelasCota.Items.Add(new ListItem("4x R$ " + String.Format("{0:0.00}", valor / 4), "4"));
                }
                if (DateTime.Now < Evento.DateLimite3ParcelaCota)
                {
                    dropParcelasCota.Items.Add(new ListItem("3x R$ " + String.Format("{0:0.00}", valor / 3), "3"));

                }
                if (DateTime.Now < Evento.DateLimite2ParcelaCota)
                {
                    dropParcelasCota.Items.Add(new ListItem("2x R$ " + String.Format("{0:0.00}", valor / 2), "2"));
                }
                if (DateTime.Now < Evento.DateLimite1ParcelaCota)
                {
                    dropParcelasCota.Items.Add(new ListItem("1x R$ " + String.Format("{0:0.00}", valor), "1"));
                }
            }
            else
            {
                dropParcelasCota.Items.Add(new ListItem("Pendência Financeira junto ao Clube", "-1"));
                dropParcelasCota.Style.Add("color", "red");
                dropParcelasCota.Disabled = true;
            }
        }

        if (Cliente.Perfil == "Não Sócio")
        {
            CotaDebito.Visible = false;
            checkpagamento.Text = "1";            

            //sIDFormaPagamento = "0";

            //if (DateTime.Now < Evento.DateLimite1ParcelaCredito
            //    || DateTime.Now < Evento.DateLimite2ParcelaCredito
            //    || DateTime.Now < Evento.DateLimite3ParcelaCredito
            //    || DateTime.Now < Evento.DateLimite4ParcelaCredito
            //    || DateTime.Now < Evento.DateLimite5ParcelaCredito)
            //    sIDFormaPagamento = "1";

            //CVenda.CarregarRadioButtonListFormasPagamento(radFormaPagamento, sIDFormaPagamento);
        }

        if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
        {
            //if (UsuarioLogado.Perfil != "Administrador")
            //    sIDFormaPagamento = "5,6,7";
            //else
            //    sIDFormaPagamento = "5,6,7,8";

            if (Cliente.Perfil == "Sócio")
            {
                if (Cliente.Debito != 1)
                {
                    if (DateTime.Now < Evento.DateLimite4ParcelaCota)
                    {
                        dropParcelasCota.Items.Add(new ListItem("4x R$ " + String.Format("{0:0.00}", valor / 4), "10"));
                        //sIDFormaPagamento += ", 10";
                    }
                    if (DateTime.Now < Evento.DateLimite3ParcelaCota)
                    {
                        dropParcelasCota.Items.Add(new ListItem("3x R$ " + String.Format("{0:0.00}", valor / 3), "2"));
                        //sIDFormaPagamento += ", 2";
                    }
                    if (DateTime.Now < Evento.DateLimite2ParcelaCota)
                    {
                        dropParcelasCota.Items.Add(new ListItem("2x R$ " + String.Format("{0:0.00}", valor / 2), "3"));
                        //sIDFormaPagamento += ", 3";
                    }
                    if (DateTime.Now < Evento.DateLimite1ParcelaCota)
                    {
                        dropParcelasCota.Items.Add(new ListItem("1x R$ " + String.Format("{0:0.00}", valor), "4"));
                        //sIDFormaPagamento += ", 4";
                    }
                }
                else
                {
                    dropParcelasCota.Items.Add(new ListItem("Pendência Financeira junto ao Clube", "-1"));
                    dropParcelasCota.Style.Add("color", "red");
                    dropParcelasCota.Disabled = true;
                }
            }
        }
    }



    private void EfetuarPagamento(int iIDVenda)
    {
        try
        {
            venda Venda = new venda();
            vendaCTL CVenda = new vendaCTL();
            Venda = CVenda.RetornarVenda(iIDVenda, "1");

            usuario UsuarioLogado = (usuario)Session["usuario"];

            usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
            if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
                Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

            decimal dValor = Convert.ToDecimal(Venda.ValorTotal);
            string sProduto = "Cadeiras";

            int iIDBandeira = 1;
            string sBandeira = "-";

            if (checkpagamento.Text == "1")
            {
                if (dropBandeira.Value != "-1")
                {
                    iIDBandeira = Convert.ToInt32(dropBandeira.Value);
                    if (iIDBandeira == 1) sBandeira = "Visa";
                    if (iIDBandeira == 2) sBandeira = "Mastercard";
                    if (iIDBandeira == 3) sBandeira = "Elo";
                    if (iIDBandeira == 4) sBandeira = "Diners";
                }

                //Salvar dados do cartão
                string sNumeroDocumento;
                try
                {
                    string sTitular = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtTitular.Value.Trim().ToUpper());

                    string sNumeroCartao = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNumeroCartao.Value.Trim());
                    sNumeroCartao = "********" + sNumeroCartao.Substring(8);

                    string sCodigoSeguranca = txtCodigoSeguranca.Value;
                    string sValidade = txtValidade.Value;
                    int iParcelas = Convert.ToInt32(dropParcelas.Value);

                    sNumeroDocumento = CVenda.CadastrarCartao(iIDVenda, sNumeroCartao, sCodigoSeguranca, sValidade, sBandeira, iParcelas, Convert.ToDouble(dValor), sTitular).ToString();
                }
                catch (Exception ex)
                {
                    PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Ocorreu uma falha na validação do cartão.\n\n" + ex.Message, this.Page);
                    return;
                }

                IniciarPagamentoCartaoCredito(dValor, sNumeroDocumento, sProduto, iIDBandeira, iIDVenda);
            }
            else
            {

                int iIDFormaPagamento = Convert.ToInt32(dropParcelasCota.Value);
                iIDVenda = Venda.IDVenda;
                int iNumeroCriancas = Convert.ToInt32(0);
                string sNomeRetirada = "";
                string sRgRetirada = "";
                int iIDLocalRetirada = 1;
                string sDetalhesFormaPagamento = "Débito em cota";
                string sRetorno = String.Empty;

                int iNumeroVagas = 0;
                try
                {
                    iNumeroVagas = Convert.ToInt32(0);
                }
                catch { }
                Venda.NumeroVagas = iNumeroVagas;

                string sNomeRetirada2 = "";
                string sRgRetirada2 = "";

                if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
                {
                    //if (radFormaPagamento.SelectedValue == "8") //Cortesia
                    //{
                    //    CVenda.ConcluirVenda(iIDVenda, iIDFormaPagamento, sDetalhesFormaPagamento, iNumeroVagas,
                    //    iNumeroCriancas, sNomeRetirada, sRgRetirada, iIDLocalRetirada, sNomeRetirada2, sRgRetirada2);
                    //    HttpContext.Current.Session["Venda"] = null;

                    //    //CadastrarEstacionamento(Venda);

                    //    sRetorno =
                    //    "<h3 style='color:green;'>TRANSAÇÃO APROVADA!!!!!</h3><br/><br/>" +
                    //    "Imprima seu Comprovante de Compra. Imprima seus ingressos a partir do dia informado nas regras do evento.<br /><br />" +
                    //    "Número do Pedido: " + Venda.IDVenda.ToString() + "<br />" +
                    //    "Nome do titular: " + Venda.Nome + "<br />" +
                    //    "CPF do Titular: " + Venda.CPF + "<br />";
                    //}
                    //else
                    //{
                    CVenda.AtualizarVendaComprovanteCaixa(iIDVenda, iIDFormaPagamento, sDetalhesFormaPagamento, iNumeroVagas,
                        iNumeroCriancas, sNomeRetirada, sRgRetirada, iIDLocalRetirada, sNomeRetirada2, sRgRetirada2);
                    HttpContext.Current.Session["Venda"] = null;

                    sRetorno =
                    "<h3 style='color:green;'>RECIBO GERADO!!!!!</h3><br/><br/>" +
                    "Imprima o recibo e entregue ao cliente para pagamento. Depois de pago, aprove a compra no menu Cadastro/Aprovar Venda.<br /><br />" +
                    "Número do Pedido: " + Venda.IDVenda.ToString() + "<br />" +
                    "Nome do titular: " + Venda.Nome + "<br />" +
                    "CPF do Titular: " + Venda.CPF + "<br />";
                    //}
                }
                else
                {
                    CVenda.ConcluirVenda(iIDVenda, iIDFormaPagamento, sDetalhesFormaPagamento, iNumeroVagas,
                        iNumeroCriancas, sNomeRetirada, sRgRetirada, iIDLocalRetirada, sNomeRetirada2, sRgRetirada2);

                    HttpContext.Current.Session["Venda"] = null;

                    //CadastrarEstacionamento(Venda);                   
                }


                PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
                string sNumeroDocumentoCriptografado = Criptografia.Criptografar("0", sChave, sVetorInicializacao);

                Response.Redirect("../pagamento/compraconcluida.aspx?NumeroDocumento= " + sNumeroDocumentoCriptografado + "&id=" + iIDVenda.ToString(), false);
            }

        }
        catch (Exception ex)
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Ocorreu uma falha no sistema.\n\n" + ex.Message, this.Page);
        }
    }

    private void IniciarPagamentoCartaoCredito(decimal dValor, string sNumeroDocumento, string sProduto, int iIDBandeira, int iIDVenda)
    {
        string sIDVenda = "";
        //DesabilitaBotaoFecharJanelaPagamento();

        //Dados do cartão de crédito
        CardBrand BandeiraCartao = CardBrand.Visa;
        if (dropBandeira.Value == "1") BandeiraCartao = CardBrand.Visa;
        else if (dropBandeira.Value == "2") BandeiraCartao = CardBrand.MasterCard;
        else if (dropBandeira.Value == "3") BandeiraCartao = CardBrand.Elo;
        else if (dropBandeira.Value == "4") BandeiraCartao = CardBrand.DinersClub;

        DateTime validExpirationDate = new DateTime(Convert.ToInt32(txtValidade.Value.Split('/')[1]), Convert.ToInt32(txtValidade.Value.Split('/')[0]), 1);
        string sTitular = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtTitular.Value.Trim().ToUpper());
        string sNumeroCartao = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNumeroCartao.Value.Trim());
        string sCodigoSeguranca = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtCodigoSeguranca.Value.Trim());
        int iParcelas = Convert.ToInt32(dropParcelas.Value);

        var customer = new Customer(sTitular.Trim().ToUpper());

        var creditCard = new CreditCard(sNumeroCartao,
                                        sTitular,
                                        new CardExpiration(Convert.ToInt16(validExpirationDate.Year), Convert.ToByte(validExpirationDate.Month)), sCodigoSeguranca, BandeiraCartao);

        dValor = 1;
        var payment = new Payment(PaymentType.CreditCard, dValor, Convert.ToByte(iParcelas), sProduto, creditCard: creditCard);

        var transaction = new TransactionRequest(sNumeroDocumento, customer, payment);

        Cielo.Configuration.IConfiguration customConfiguration = new Cielo.Configuration.CustomConfiguration()
        {
            DefaultEndpoint = ConfigurationManager.AppSettings["cielo.endpoint.default"],
            QueryEndpoint = ConfigurationManager.AppSettings["cielo.endpoint.query"],
            MerchantId = ConfigurationManager.AppSettings["cielo.customer.id"],
            MerchantKey = ConfigurationManager.AppSettings["cielo.customer.key"],
            ReturnUrl = ConfigurationManager.AppSettings["cielo.return.url"],
        };

        var cieloService = new Cielo.CieloService(customConfiguration);

        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        string sNumeroDocumentoCriptografado = Criptografia.Criptografar(sNumeroDocumento, sChave, sVetorInicializacao);

        //URL de retorno (pra onde vai depois de aprovar ou não a compra)
        string sUrlRetorno = ConfigurationManager.AppSettings["cielo.Hypertext.Transfer.Protocol"].ToString();
        sUrlRetorno += HttpContext.Current.Request.Url.Authority + "/" + System.Configuration.ConfigurationManager.AppSettings["cielo.return.url"].ToString();

        vendaCTL CVenda = new vendaCTL();
        int iAprovada = 0;

        Cielo.Responses.NewTransactionResponse responseTransacao = null;
        try
        {
            responseTransacao = cieloService.CreateTransaction(transaction);

            usuarioCTL CUsuario = new usuarioCTL();

            if (responseTransacao.Status == Status.Authorized) //Aprovada
            {
                usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];

                //var responseCaptura = cieloService.CaptureTransaction((Guid)responseTransacao.PaymentId, 0);
                iAprovada = 1;
                CVenda.AtualizarPagamentoCartao(sNumeroDocumento, responseTransacao.PaymentId.ToString(), responseTransacao.Tid, responseTransacao.AuthorizationCode.ToString(), iAprovada);
                CVenda.AprovarVenda(iIDVenda, Cliente.IDUsuario);

                sIDVenda = Criptografia.Criptografar(iIDVenda.ToString(), sChave, sVetorInicializacao);
                Response.Redirect("../pagamento/compraconcluida.aspx?NumeroDocumento= " + sNumeroDocumentoCriptografado + "&id=" + sIDVenda, false);
            }
            else
            {
                CVenda.AtualizarPagamentoCartao(sNumeroDocumento, responseTransacao.PaymentId.ToString(), responseTransacao.Tid, responseTransacao.AuthorizationCode.ToString(), iAprovada);

                sIDVenda = Criptografia.Criptografar(iIDVenda.ToString(), sChave, sVetorInicializacao);
                Response.Redirect("../pagamento/compranaoconcluida.aspx?id= " + sIDVenda, false);
            }

        }
        catch (ResponseException ex)
        {

            sIDVenda = Criptografia.Criptografar(iIDVenda.ToString(), sChave, sVetorInicializacao);
            Response.Redirect("../pagamento/compranaoconcluida.aspx?id= " + sIDVenda, false);
        }
        catch (Exception ex)
        {

            sIDVenda = Criptografia.Criptografar(iIDVenda.ToString(), sChave, sVetorInicializacao);
            Response.Redirect("../pagamento/compranaoconcluida.aspx?id= " + sIDVenda, false);
        }
    }

    public string CreatedIndentedXmlString(string inXml)
    {
        if (inXml.Length == 0) return "";
        XElement x = XElement.Parse(inXml);
        return Server.HtmlEncode(x.ToString());
    }

    protected void btnPagar_Click(object sender, EventArgs e)
    {
        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        int iIDVenda = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["id"], sChave, sVetorInicializacao));
        //int iIDVenda = 24812;

        CarregarDadosProduto(iIDVenda);

        if (PodeSalvar())
            EfetuarPagamento(iIDVenda);
    }

    private void CarregarFormasPagamento(string iIDEdicao)
    {
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
        if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
            Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

        vendaCTL CVenda = new vendaCTL();

        evento Evento = new evento();
        eventoCTL CEvento = new eventoCTL();
        Evento = CEvento.RetornarEdicao(Convert.ToInt32(iIDEdicao));

        string sIDFormaPagamento;

        if (Evento.Evento == "Outros")
        {
            DataTable dataTableLotes = null;
            dataTableLotes = CEvento.RetornarLotes(Evento.IDEdicao, true);
            if (UsuarioLogado.Perfil == "Administrador")
                dataTableLotes = CEvento.RetornarLotes(Evento.IDEdicao, false);

            double dValor = 0;
            dValor += Convert.ToDouble(dataTableLotes.Rows[0]["ValorInteiraCadeiraSocio"].ToString());
            dValor += Convert.ToDouble(dataTableLotes.Rows[0]["ValorInteiraCadeiraNaoSocio"].ToString());
            dValor += Convert.ToDouble(dataTableLotes.Rows[0]["ValorInteiraAvulsoSocio"].ToString());
            dValor += Convert.ToDouble(dataTableLotes.Rows[0]["ValorInteiraAvulsoNaoSocio"].ToString());
            dValor += Convert.ToDouble(dataTableLotes.Rows[0]["ValorMeiaCadeiraSocio"].ToString());
            dValor += Convert.ToDouble(dataTableLotes.Rows[0]["ValorMeiaCadeiraNaoSocio"].ToString());
            dValor += Convert.ToDouble(dataTableLotes.Rows[0]["ValorMeiaAvulsoSocio"].ToString());
            dValor += Convert.ToDouble(dataTableLotes.Rows[0]["ValorMeiaAvulsoNaoSocio"].ToString());

            if (dValor == 0) //Se não colocou nenhum valor para os tipos de ingresso
            {
                sIDFormaPagamento = "9"; //Entrada franca
                //CVenda.CarregarRadioButtonListFormasPagamento(radFormaPagamento, sIDFormaPagamento);
                return;
            }
        }

        if (Cliente.Perfil == "Sócio")
        {
            sIDFormaPagamento = "0";

            if (DateTime.Now < Evento.DateLimite1ParcelaCredito
                || DateTime.Now < Evento.DateLimite2ParcelaCredito
                || DateTime.Now < Evento.DateLimite3ParcelaCredito
                || DateTime.Now < Evento.DateLimite4ParcelaCredito
                || DateTime.Now < Evento.DateLimite5ParcelaCredito)
                sIDFormaPagamento = "1";

            if (Cliente.Debito != 1)
            {
                if (DateTime.Now < Evento.DateLimite4ParcelaCota)
                    sIDFormaPagamento += ", 10";
                if (DateTime.Now < Evento.DateLimite3ParcelaCota)
                    sIDFormaPagamento += ", 2";
                if (DateTime.Now < Evento.DateLimite2ParcelaCota)
                    sIDFormaPagamento += ", 3";
                if (DateTime.Now < Evento.DateLimite1ParcelaCota)
                    sIDFormaPagamento += ", 4";
            }

            //CVenda.CarregarRadioButtonListFormasPagamento(radFormaPagamento, sIDFormaPagamento);
        }

        if (Cliente.Perfil == "Não Sócio")
        {
            sIDFormaPagamento = "0";

            if (DateTime.Now < Evento.DateLimite1ParcelaCredito
                || DateTime.Now < Evento.DateLimite2ParcelaCredito
                || DateTime.Now < Evento.DateLimite3ParcelaCredito
                || DateTime.Now < Evento.DateLimite4ParcelaCredito
                || DateTime.Now < Evento.DateLimite5ParcelaCredito)
                sIDFormaPagamento = "1";

            //CVenda.CarregarRadioButtonListFormasPagamento(radFormaPagamento, sIDFormaPagamento);
        }

        if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
        {
            if (UsuarioLogado.Perfil != "Administrador")
                sIDFormaPagamento = "5,6,7";
            else
                sIDFormaPagamento = "5,6,7,8";

            if (Cliente.Perfil == "Sócio")
            {
                if (Cliente.Debito != 1)
                {
                    if (DateTime.Now < Evento.DateLimite4ParcelaCota)
                        sIDFormaPagamento += ", 10";
                    if (DateTime.Now < Evento.DateLimite3ParcelaCota)
                        sIDFormaPagamento += ", 2";
                    if (DateTime.Now < Evento.DateLimite2ParcelaCota)
                        sIDFormaPagamento += ", 3";
                    if (DateTime.Now < Evento.DateLimite1ParcelaCota)
                        sIDFormaPagamento += ", 4";

                    //formaPagamento.Style.Add("color", "black");
                }
                else
                {
                    //formaPagamento.InnerText = "Passo 5 - Forma de Pagamento (Pendência Financeira junto ao Clube)";
                    //formaPagamento.Style.Add("color", "red");
                }
            }

            //CVenda.CarregarRadioButtonListFormasPagamento(radFormaPagamento, sIDFormaPagamento);

            ////Detalhes do pagamento
            //lblDetalhesPagamento.Visible = true;
            //txtDetalhesFormaPagamento.Visible = true;
        }

        //if (lblCarrinhoTotal.Text.IndexOf("R$ 0,00") != -1)
        //    foreach (ListItem item in radFormaPagamento.Items)
        //        if (item.Text == "Cartão (e-commerce)")
        //        {
        //            radFormaPagamento.Items.Remove(item);
        //            break;
        //        }
    }

    private void CarregarDadosProduto(int iIDVenda)
    {
        venda Venda = new venda();
        vendaCTL CVenda = new vendaCTL();        
        Venda = (venda)HttpContext.Current.Session["Venda"];

        DataSet dataSet = CVenda.RetornarCarrinhoCompras(iIDVenda);

        evento Evento = new evento();
        eventoCTL CEvento = new eventoCTL();
        Evento = CEvento.RetornarEdicao(Convert.ToInt32(Venda.IDEdicao));

        //ConfigurarCampos(Venda.Evento);

        //lblProduto.InnerText = Venda.Edicao;
        lblValor.InnerText = "R$ " + String.Format("{0:0.00}", Venda.ValorTotal);

        //Verificar se é Aniversariante e gerar a cortesia.
        if (Venda.Evento == "Boate")
            VerificarAniversariante(dataSet, Evento);

        //Vagas de estacionamento
        //int iQuantidadeIngressosTipoCadeira = Convert.ToInt32(dataSet.Tables[1].Rows[0]["IngressosCadeira"].ToString());
        //HabilitarVagaEstacionamento(Convert.ToBoolean(Evento.VagaEstacionamento), iQuantidadeIngressosTipoCadeira, Evento.NumIngressoEstacionamento);


        DataTable dataSetMesa = new DataTable();
        dataSetMesa = dataSet.Tables[0].Clone();
        DataTable dataSetSemMesa = new DataTable();
        dataSetSemMesa = dataSet.Tables[0].Clone();
        DataTable dataAvulsos = new DataTable();
        DataTable dataCadeira = new DataTable();

        dataAvulsos.Columns.Add("Ticket", typeof(string));
        dataAvulsos.Columns.Add("Tipo", typeof(string));
        dataAvulsos.Columns.Add("Valor", typeof(string));

        dataCadeira.Columns.Add("Ticket", typeof(string));
        dataCadeira.Columns.Add("Tipo", typeof(string));
        dataCadeira.Columns.Add("Valor", typeof(string));

        DataRow dr = dataAvulsos.NewRow();
        DataRow dr1 = dataCadeira.NewRow();
        string tipoIngresso = "";

        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
        {
            if (dataRow["Tipo"].ToString().Contains("mesa"))
            {
                tipoIngresso = dataRow["Tipo"].ToString().Split(';')[2] + " - " + dataRow["TipoIngresso"].ToString().Replace("cadeira", "") + " - " + dataRow["Tipo"].ToString().Split(';')[3];

                dataCadeira.Rows.Add(dataRow["Ticket"].ToString(), tipoIngresso, " R$ " + String.Format("{0:0.00}", Convert.ToDouble(dataRow["Valor"].ToString())));               
                
            }
            else
            {
                string ingressoAvulso = dataRow["Tipo"].ToString().Contains("avulso") ? dataRow["Tipo"].ToString().Replace("avulso", "") : dataRow["Tipo"].ToString();
                tipoIngresso = ingressoAvulso;

                dataAvulsos.Rows.Add(dataRow["Ticket"].ToString(), tipoIngresso, " R$ " + String.Format("{0:0.00}", Convert.ToDouble(dataRow["Valor"].ToString())));               
            }
        }

        grdMesa.DataSource = dataCadeira;
        grdMesa.DataBind();

        grdSemMesa.DataSource = dataAvulsos;
        grdSemMesa.DataBind();
    }

    private void HabilitarVagaEstacionamento(bool bHabilitarVagaEstacionamento, int iQuantidadeIngressosTipoCadeira, int iNumIngressoEstacionamento)
    {
        venda Venda = (venda)HttpContext.Current.Session["Venda"];
        vendaCTL CVenda = new vendaCTL();

        evento Evento = new evento();
        eventoCTL CEvento = new eventoCTL();
        Evento = CEvento.RetornarEdicao(Convert.ToInt32(Venda.IDEdicao));

        try
        {
            int iVagasEstacionamentoReservadas = CVenda.RetornarVagasEstacionamentoReservadas(Venda.IDEdicao);

            if (bHabilitarVagaEstacionamento
                && iQuantidadeIngressosTipoCadeira >= iNumIngressoEstacionamento
                && iVagasEstacionamentoReservadas < Evento.VagasEstacionamento)
            {
                //lblDesejaEstacionamento.Visible = true;
                //dropVagasEstacionamento.Visible = true;

                int iNumeroMaximoVagas = iQuantidadeIngressosTipoCadeira / iNumIngressoEstacionamento;

                //dropVagasEstacionamento.Items.Clear();

                //for (int i = 0; i <= iNumeroMaximoVagas; i++)
                //{
                //    dropVagasEstacionamento.Items.Add(new ListItem(i.ToString()));
                //    dropVagasEstacionamento.SelectedValue = iNumeroMaximoVagas.ToString();
                //}

                //if (Venda != null)
                //{
                //    Venda.NumeroVagas = Convert.ToInt32(dropVagasEstacionamento.SelectedValue);
                //    HttpContext.Current.Session["Venda"] = Venda;
                //}
            }
            else
            {
                //lblDesejaEstacionamento.Visible = false;
                //dropVagasEstacionamento.Visible = false;
                //dropVagasEstacionamento.SelectedValue = "0";
            }
        }
        catch (Exception ex)
        {
            //lblMensagem.Text = "Erro - " + ex.Message;
        }
    }

    private void VerificarAniversariante(DataSet dataSet, evento Evento)
    {
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        clienteCTL CCliente = new clienteCTL();
        cliente Cliente = CCliente.RetornarCliente(UsuarioLogado.IDUsuario);
        vendaCTL CVenda = new vendaCTL();

        //int iMesAtual = DateTime.Now.Month;
        //int iMesAtual = 2;
        //int iMesCliente = Convert.ToInt32(Cliente.DataNascimento.Substring(3, 2));
        int iQteIngressosComprados = dataSet.Tables[0].Rows.Count;
        int iQteCortersiasAdquiridos = 0;
        List<int> IngressosAniversariante = new List<int>();

        foreach (DataRow dtRowIngressos in dataSet.Tables[0].Rows)
        {
            if (dtRowIngressos["TipoIngresso"].ToString() == "Cortesia não sócio Aniversariante" || dtRowIngressos["TipoIngresso"].ToString() == "Cortesia sócio Aniversariante")
            {
                IngressosAniversariante.Add(Convert.ToInt32(dtRowIngressos["Ticket"].ToString()));
                iQteCortersiasAdquiridos++;
            }
        }

        iQteIngressosComprados -= iQteCortersiasAdquiridos;
        int iQteExistente = iQteCortersiasAdquiridos * Evento.MinimoAniversariante;

        //retira o ingresso cortesia Aniversariante caso tenha menos do que o minimo Aniversariante.
        for (int i = 0; i < iQteIngressosComprados; i++)
        {
            if (iQteExistente > iQteIngressosComprados)
            {
                CVenda.CancelarTicket(IngressosAniversariante[i], "Ticket " + IngressosAniversariante[i], Cliente.IDUsuario);
                iQteExistente -= Evento.MinimoAniversariante;
                iQteCortersiasAdquiridos--;
            }
        }

        int iNovosIngressos = iQteIngressosComprados - iQteExistente;

        //para verificar se o cliente selecionou aniversariante.
        //if (chkAniversariante.Checked == true && Evento.MinimoAniversariante != 0)
        //{   // percorre a quantidade de ingressos e adiciona os de cortesia para o Aniversariante.
        //    while (iNovosIngressos >= Evento.MinimoAniversariante)
        //    {
        //        if (iNovosIngressos % Evento.MinimoAniversariante == 0)
        //        {
        //            GerarIngressoAniversariante();
        //            iNovosIngressos -= Evento.MinimoAniversariante;
        //        }
        //    }
        //}
    }

    //private void ConfigurarCampos(string sEvento)
    //{
    //    divAniversariante.Visible = false;
    //    radTipoAvulso.Visible = false;
    //    fieldCriancas.Visible = false;
    //    lblMensagemMeia.Visible = false;

    //    if (sEvento == "Boate")
    //        divAniversariante.Visible = true;
    //    else
    //    {
    //        divAniversariante.Visible = false;
    //        radTipoAvulso.Visible = true;
    //        fieldCriancas.Visible = true;
    //    }

    //    if (sEvento == "Festa Junina" || sEvento == "Reveillon")
    //        lblMensagemMeia.Visible = true;
    //}

    private bool PodeSalvar()
    {

        if (checkpagamento.Text == "-1")
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Selecione uma forma de pagamento!.", this.Page);
            return false;
        }

        if (checkpagamento.Text == "1")//forma de pagamento Cartão de credito
        {
            if (dropBandeira.Value == "-1")
            {
                PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Selecione uma [Bandeira] de seu cartão de crédito.", this.Page);
                return false;
            }
            if (txtTitular.Value.Trim() == "")
            {
                PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Informe o [Nome do Titular].", this.Page);
                return false;
            }
            if (txtNumeroCartao.Value.Trim() == "")
            {
                PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Informe o [Número do cartão].", this.Page);
                return false;
            }
            if (txtNumeroCartao.Value.Trim().Length < 14)
            {
                PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Informe todos os números do cartão.", this.Page);
                return false;
            }
            if (txtCodigoSeguranca.Value.Trim() == "")
            {
                PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Informe o [Código de segurança].", this.Page);
                return false;
            }
            if (dropParcelas.Value == "-1")
            {
                PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Selecione o [Número de parcelas].", this.Page);
                return false;
            }
        }
        if (checkpagamento.Text == "2")//forma de pagamento Cota
        {
            if (dropParcelasCota.Value == "-1")
            {
                PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Selecione o [Número de parcelas].", this.Page);
                return false;
            }
        }

        //Verifica se ele está "clicando" mais de uma vez no botão ou outro erro 
        //que gere mais de uma aprovação no cartão do cliente
        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        int iIDVenda = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["id"], sChave, sVetorInicializacao));
        //int iIDVenda = 24812;
        int iSegundos = 10;

        if (ConfigurationManager.AppSettings["TestandoSistema"].ToString() == "Sim") iSegundos = 1;

        vendaCTL CVenda = new vendaCTL();
        if (CVenda.VerificarExistenciaTentativaPagamentoCartao(iIDVenda, iSegundos))
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Última tentativa de aprovação em menos de " + iSegundos.ToString() + " segundos, aguarde um instante por favor.", this.Page);
            return false;
        }

        usuarioCTL CUsuario = new usuarioCTL();
        string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

        //Blacklist. Se for IP do PIC (White List), libera para compra
        usuario UsuarioLogado = (usuario)Session["usuario"];
        if (UsuarioLogado.Perfil == "Não Sócio"
            && !CUsuario.VerificarWhiteList(sIP))
        {
            venda Venda = new venda();
            Venda = CVenda.RetornarVenda(iIDVenda, "1");

            string sMotivo = "";
            string sUrlRetorno = ConfigurationManager.AppSettings["cielo.Hypertext.Transfer.Protocol"].ToString();
            sUrlRetorno += HttpContext.Current.Request.Url.Authority + "/" + System.Configuration.ConfigurationManager.AppSettings["cielo.return.url"].ToString();

            //Verificar se o IP está bloqueado
            if (CUsuario.VerificarIPBloqueadoBlackList(sIP))
            {
                PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Seu dispositivo está bloqueado para acessar o sistema de ingresso. Favor entrar em contato com o clube.", this.Page);
                return false;
            }

            //Verifica se o IP foi liberado e a partir de que horas
            DateTime dataLiberacao = new DateTime(1900, 01, 01);
            DataTable dataTableLiberacao = CUsuario.RetonarIPLiberadoBlackList(sIP);

            if (dataTableLiberacao.Rows.Count != 0)
                dataLiberacao = Convert.ToDateTime(dataTableLiberacao.Rows[0]["DataAlteracao"]);

            if (CVenda.VerificarComprasNaoAprovadasCartao(UsuarioLogado.IDUsuario, Venda.IDEdicao, dataLiberacao))
            {
                sMotivo = "5 vendas não aprovadas (mesmo cliente)";
                CUsuario.CadastrarBlackList(sIP, sMotivo, UsuarioLogado.IDUsuario);

                CUsuario.BloquearUsuario(UsuarioLogado.IDUsuario);

                PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Seu usuário e sua máquina foram bloqueados pela nossa política de segurança. Para liberar, favor entrar em contato com o clube.", this.Page);

                sUrlRetorno += "?NumeroDocumento=Cancelar";
                //ScriptManager.RegisterClientScriptBlock(upPagamento, this.GetType(), DateTime.Now.Ticks.ToString(), "ProcessarVenda('" + sUrlRetorno + "')", true);
                return false;
            }

            if (CVenda.VerificarComprasAprovadasCartao(UsuarioLogado.IDUsuario, Venda.IDEdicao, dataLiberacao))
            {
                sMotivo = "5 vendas já aprovadas (mesmo cliente)";
                CUsuario.CadastrarBlackList(sIP, sMotivo, UsuarioLogado.IDUsuario);

                CUsuario.BloquearUsuario(UsuarioLogado.IDUsuario);

                PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Seu usuário e sua máquina foram bloqueados pela nossa política de segurança. Para liberar, favor entrar em contato com o clube.", this.Page);

                sUrlRetorno += "?NumeroDocumento=Cancelar";
                //ScriptManager.RegisterClientScriptBlock(upPagamento, this.GetType(), DateTime.Now.Ticks.ToString(), "ProcessarVenda('" + sUrlRetorno + "')", true);
                return false;
            }

            string sNumeroCartao = txtNumeroCartao.Value.Substring(txtNumeroCartao.Value.Length - 4, 4);
            if (CVenda.VerificarComprasAprovadasCartao(sNumeroCartao, txtCodigoSeguranca.Value, dataLiberacao))
            {
                sMotivo = "5 compras aprovadas para o cartão (mesmo cliente ou não)";
                CUsuario.CadastrarBlackList(sIP, sMotivo, UsuarioLogado.IDUsuario);

                CUsuario.BloquearUsuario(UsuarioLogado.IDUsuario);

                PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Seu usuário e sua máquina foram bloqueados pela nossa política de segurança. Para liberar, favor entrar em contato com o clube.", this.Page);

                sUrlRetorno += "?NumeroDocumento=Cancelar";
                //ScriptManager.RegisterClientScriptBlock(upPagamento, this.GetType(), DateTime.Now.Ticks.ToString(), "ProcessarVenda('" + sUrlRetorno + "')", true);
                return false;
            }
        }

        return true;
    }

    protected void grdMesa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sTipoIngresso = grdMesa.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
        if (e.CommandName == "Excluir")
        {
            if (sTipoIngresso != "Cortesia n&#227;o s&#243;cio Aniversariante" &&
                sTipoIngresso != "Cortesia s&#243;cio Aniversariante")
            {
                usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

                usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
                if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
                    Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

                vendaCTL CVenda = new vendaCTL();

                double dTicket = Convert.ToDouble(grdMesa.DataKeys[int.Parse((string)e.CommandArgument)]["Ticket"].ToString());
                string sTextoLog = "Ticket " + dTicket.ToString();
                sTextoLog += "; " + Server.HtmlDecode(grdMesa.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text);


                venda Venda = (venda)HttpContext.Current.Session["Venda"];
                sTextoLog += "; Voucher " + Venda.IDVenda.ToString();


                //Implementar aqui...

                CVenda.CancelarTicket(dTicket, sTextoLog, Cliente.IDUsuario);
                CarregarDadosProduto(Venda.IDVenda);
            }
        }
    }

    protected void grdSemMesa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sTipoIngresso = grdSemMesa.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
        if (e.CommandName == "Excluir")
        {
            if (sTipoIngresso != "Cortesia n&#227;o s&#243;cio Aniversariante" &&
                sTipoIngresso != "Cortesia s&#243;cio Aniversariante")
            {
                usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

                usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
                if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
                    Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

                vendaCTL CVenda = new vendaCTL();

                double dTicket = Convert.ToDouble(grdSemMesa.DataKeys[int.Parse((string)e.CommandArgument)]["Ticket"].ToString());
                string sTextoLog = "Ticket " + dTicket.ToString();
                sTextoLog += "; " + Server.HtmlDecode(grdSemMesa.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text);


                venda Venda = (venda)HttpContext.Current.Session["Venda"];
                sTextoLog += "; Voucher " + Venda.IDVenda.ToString();


                //Implementar aqui...

                CVenda.CancelarTicket(dTicket, sTextoLog, Cliente.IDUsuario);
                CarregarDadosProduto(Venda.IDVenda);
            }
        }
    }

    protected void btnAterarCompra_Click(object sender, EventArgs e)
    {
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        if (HttpContext.Current.Session["Venda"] != null)
        {
            venda Venda = (venda)HttpContext.Current.Session["Venda"];

            Response.Redirect("../cliente/indexEvento.aspx?idedicao=" + Venda.IDEdicao, false);
        }
        else 
        {
            ExibirMensagem("Sua sessão expirou! Faça novo acesso ao sistema");
            return;
        }
    }

    private void ExibirMensagem(string sMensagem)
    {
        sMensagem = sMensagem.Replace("'", "");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + sMensagem + "')", true);
    }
}
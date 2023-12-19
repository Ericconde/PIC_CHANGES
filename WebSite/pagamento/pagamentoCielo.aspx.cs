using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Configuration;
using Model.objetos;
using Controller;
using Cielo;
using System.Xml.Linq;
using Cielo.Enums;
using Cielo.Requests.Entites.Common;
using Cielo.Requests.Entites;
using Cielo.Responses.Exceptions;

public partial class pagamento_pagamentoCielo : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
            string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
            string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
            int iIDVenda = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["id"], sChave, sVetorInicializacao));

            CarregarDadosProduto(iIDVenda);
            CarregarNumeroParcelas(iIDVenda);

            dropMes.SelectedValue = DateTime.Now.Month.ToString("00");
            dropAno.SelectedValue = DateTime.Now.Year.ToString("0000");

            if (ConfigurationManager.AppSettings["TestandoSistema"].ToString() == "Sim")
            {
                txtTitular.Text = "TESTE";
                txtNumeroCartao.Text = "4024007153763191";
                dropMes.SelectedValue = "12";
                dropAno.SelectedValue = "2099";
                txtCodigoSeguranca.Text = "999";
            }
        }
    }

    private void CarregarDadosProduto(int iIDVenda)
    {
        venda Venda = new venda();
        vendaCTL CVenda = new vendaCTL();
        Venda = CVenda.RetornarVenda(iIDVenda, "1");

        lblProduto.Text = "Ingressos";
        lblValor.Text = "R$ " + String.Format("{0:0.00}", Venda.ValorTotal);
    }

    private void CarregarNumeroParcelas(int iIDVenda)
    {
        venda Venda = new venda();
        vendaCTL CVenda = new vendaCTL();
        Venda = CVenda.RetornarVenda(iIDVenda, "1");

        evento Evento = new evento();
        eventoCTL CEvento = new eventoCTL();
        Evento = CEvento.RetornarEdicao(Convert.ToInt32(Venda.IDEdicao));
        usuario UsuarioLogado = (usuario)Session["usuario"];

        if (Evento.Evento == "Boate")
        {
            if (DateTime.Now < Evento.DateLimite1ParcelaCredito)
                dropParcelas.Items.Add(new ListItem("1", "1"));
            
            if (UsuarioLogado.Perfil == "Sócio")
            {
                if (DateTime.Now < Evento.DateLimite2ParcelaCredito)
                    dropParcelas.Items.Add(new ListItem("2", "2"));
            }
        }
        else
        {
            if (DateTime.Now < Evento.DateLimite1ParcelaCredito)
                dropParcelas.Items.Add(new ListItem("1", "1"));
            if (DateTime.Now < Evento.DateLimite2ParcelaCredito)
                dropParcelas.Items.Add(new ListItem("2", "2"));

            //if (DateTime.Now < Evento.DateLimite3ParcelaCredito && UsuarioLogado.Perfil != "Não Sócio")
            //    dropParcelas.Items.Add(new ListItem("3", "3"));
            //if (DateTime.Now < Evento.DateLimite4ParcelaCredito && UsuarioLogado.Perfil != "Não Sócio")
            //    dropParcelas.Items.Add(new ListItem("4", "4"));
            //if (DateTime.Now < Evento.DateLimite5ParcelaCredito && UsuarioLogado.Perfil != "Não Sócio")
            //    dropParcelas.Items.Add(new ListItem("5", "5"));

            if (DateTime.Now < Evento.DateLimite3ParcelaCredito)
                dropParcelas.Items.Add(new ListItem("3", "3"));
            if (DateTime.Now < Evento.DateLimite4ParcelaCredito)
                dropParcelas.Items.Add(new ListItem("4", "4"));
            if (DateTime.Now < Evento.DateLimite5ParcelaCredito)
                dropParcelas.Items.Add(new ListItem("5", "5"));
        }
    }

    private void EfetuarPagamento(int iIDVenda)
    {
        try
        {
            venda Venda = new venda();
            vendaCTL CVenda = new vendaCTL();
            Venda = CVenda.RetornarVenda(iIDVenda, "1");

            decimal dValor = Convert.ToDecimal(Venda.ValorTotal);
            string sProduto = "Cadeiras";

            int iIDBandeira = 1;
            string sBandeira = "-"; 
            if (rdVisa.Checked)
            {
                iIDBandeira = 1;
                sBandeira = "visa";
            }
            if (rdMaster.Checked)
            {
                iIDBandeira = 2;
                sBandeira = "mastercard";
            }
            if (rdElo.Checked)
            {
                iIDBandeira = 3;
                sBandeira = "elo";
            }
            if (rdDiners.Checked)
            {
                iIDBandeira = 4;  
                sBandeira = "diners";
            }

            //Salvar dados do cartão
            string sNumeroDocumento;
            try
            {
                string sTitular = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtTitular.Text.Trim().ToUpper()); 
                
                string sNumeroCartao = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNumeroCartao.Text.Trim());
                sNumeroCartao = "********" + sNumeroCartao.Substring(8);

                string sCodigoSeguranca = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtCodigoSeguranca.Text.Trim());
                string sValidade = dropAno.SelectedValue + dropMes.SelectedValue;
                int iParcelas = Convert.ToInt32(dropParcelas.SelectedValue);

                sNumeroDocumento = CVenda.CadastrarCartao(iIDVenda, sNumeroCartao, sCodigoSeguranca, sValidade, sBandeira, iParcelas, Convert.ToDouble(dValor), sTitular).ToString();
            }
            catch (Exception ex)
            {
                PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Ocorreu uma falha na validação do cartão.\n\n" + ex.Message, this.Page);
                return;
            }

            IniciarPagamentoCartaoCredito(dValor, sNumeroDocumento, sProduto, iIDBandeira);
        }
        catch (Exception ex)
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Ocorreu uma falha no sistema.\n\n" + ex.Message, this.Page);
        }
    }    

    private void DesabilitaBotaoFecharJanelaPagamento()
    {
        ScriptManager.RegisterClientScriptBlock(upPagamento, this.GetType(), DateTime.Now.Ticks.ToString(), "parent.$('.fancybox-close').hide();", true);
    }

    private bool PodeSalvar()
    {
        if ((!rdMaster.Checked) 
            && (!rdVisa.Checked)
            && (!rdElo.Checked)
            && (!rdDiners.Checked))
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Selecione uma [Bandeira] de seu cartão de crédito.", this.Page);
            return false;
        }
        if (txtTitular.Text.Trim() == "")
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Informe o [Nome do Titular].", this.Page);
            return false;
        }
        if (txtNumeroCartao.Text.Trim() == "")
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Informe o [Número do cartão].", this.Page);
            return false;
        }
        if (txtNumeroCartao.Text.Trim().Length < 14)
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Informe todos os números do cartão.", this.Page);
            return false;
        }
        if (txtCodigoSeguranca.Text.Trim() == "")
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Informe o [Código de segurança].", this.Page);
            return false;
        }
        if (dropParcelas.SelectedValue == "")
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Selecione o [Número de parcelas].", this.Page);
            return false;
        }

        //Verifica se ele está "clicando" mais de uma vez no botão ou outro erro 
        //que gere mais de uma aprovação no cartão do cliente
        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        int iIDVenda = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["id"], sChave, sVetorInicializacao));
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
                ScriptManager.RegisterClientScriptBlock(upPagamento, this.GetType(), DateTime.Now.Ticks.ToString(), "ProcessarVenda('"+ sUrlRetorno + "')", true);
                return false;
            }

            if (CVenda.VerificarComprasAprovadasCartao(UsuarioLogado.IDUsuario, Venda.IDEdicao, dataLiberacao))
            {
                sMotivo = "5 vendas já aprovadas (mesmo cliente)";
                CUsuario.CadastrarBlackList(sIP, sMotivo, UsuarioLogado.IDUsuario);

                CUsuario.BloquearUsuario(UsuarioLogado.IDUsuario);

                PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Seu usuário e sua máquina foram bloqueados pela nossa política de segurança. Para liberar, favor entrar em contato com o clube.", this.Page);

                sUrlRetorno += "?NumeroDocumento=Cancelar";
                ScriptManager.RegisterClientScriptBlock(upPagamento, this.GetType(), DateTime.Now.Ticks.ToString(), "ProcessarVenda('" + sUrlRetorno + "')", true);
                return false;
            }

            string sNumeroCartao = txtNumeroCartao.Text.Substring(txtNumeroCartao.Text.Length - 4, 4);            
            if (CVenda.VerificarComprasAprovadasCartao(sNumeroCartao, txtCodigoSeguranca.Text, dataLiberacao))
            {
                sMotivo = "5 compras aprovadas para o cartão (mesmo cliente ou não)";
                CUsuario.CadastrarBlackList(sIP, sMotivo, UsuarioLogado.IDUsuario);

                CUsuario.BloquearUsuario(UsuarioLogado.IDUsuario);

                PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Seu usuário e sua máquina foram bloqueados pela nossa política de segurança. Para liberar, favor entrar em contato com o clube.", this.Page);

                sUrlRetorno += "?NumeroDocumento=Cancelar";
                ScriptManager.RegisterClientScriptBlock(upPagamento, this.GetType(), DateTime.Now.Ticks.ToString(), "ProcessarVenda('" + sUrlRetorno + "')", true);
                return false;
            }
        }

        return true;
    }

    private void IniciarPagamentoCartaoCredito(decimal dValor, string sNumeroDocumento, string sProduto, int iIDBandeira)
    {
        DesabilitaBotaoFecharJanelaPagamento();

        //Dados do cartão de crédito
        CardBrand BandeiraCartao = CardBrand.Visa;
        if (rdVisa.Checked) BandeiraCartao = CardBrand.Visa;
        else if (rdMaster.Checked) BandeiraCartao = CardBrand.MasterCard;
        else if (rdElo.Checked) BandeiraCartao = CardBrand.Elo;
        else if (rdDiners.Checked) BandeiraCartao = CardBrand.DinersClub;

        DateTime validExpirationDate = new DateTime(Convert.ToInt32(dropAno.SelectedValue), Convert.ToInt32(dropMes.SelectedValue), 1);
        string sTitular = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtTitular.Text.Trim().ToUpper());
        string sNumeroCartao = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNumeroCartao.Text.Trim());
        string sCodigoSeguranca = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtCodigoSeguranca.Text.Trim());
        int iParcelas = Convert.ToInt32(dropParcelas.SelectedValue);

        var customer = new Customer(sTitular.Trim().ToUpper());

        var creditCard = new CreditCard(sNumeroCartao,
                                        sTitular,
                                        new CardExpiration(Convert.ToInt16(validExpirationDate.Year), Convert.ToByte(validExpirationDate.Month)), sCodigoSeguranca, BandeiraCartao);

        //dValor = 1;
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
                //var responseCaptura = cieloService.CaptureTransaction((Guid)responseTransacao.PaymentId, 0);
                iAprovada = 1;
                CVenda.AtualizarPagamentoCartao(sNumeroDocumento, responseTransacao.PaymentId.ToString(), responseTransacao.Tid, responseTransacao.AuthorizationCode.ToString(), iAprovada);

                sUrlRetorno += "?NumeroDocumento=" + sNumeroDocumentoCriptografado;
                sUrlRetorno += "&id=" + responseTransacao.PaymentId;
            }
            else
            {
                CVenda.AtualizarPagamentoCartao(sNumeroDocumento, responseTransacao.PaymentId.ToString(), responseTransacao.Tid, responseTransacao.AuthorizationCode.ToString(), iAprovada);

                sNumeroDocumento = Criptografia.Criptografar(sNumeroDocumento, sChave, sVetorInicializacao);
                sUrlRetorno += "?numerodocumento=" + sNumeroDocumento;
                sUrlRetorno += "&message=" + responseTransacao.ReturnMessage;
            }
            ScriptManager.RegisterClientScriptBlock(upPagamento, this.GetType(), DateTime.Now.Ticks.ToString(), "ProcessarVenda('" + sUrlRetorno + "')", true);
        }
        catch (ResponseException ex)
        {
            sNumeroDocumento = Criptografia.Criptografar(sNumeroDocumento, sChave, sVetorInicializacao);
            sUrlRetorno += "?numerodocumento=" + sNumeroDocumento;
            sUrlRetorno += "&message=" + responseTransacao.ReturnMessage + "; " + ex.Message;
            ScriptManager.RegisterClientScriptBlock(upPagamento, this.GetType(), DateTime.Now.Ticks.ToString(), "ProcessarVenda('" + sUrlRetorno + "')", true);
        }
        catch (Exception ex)
        {
            sNumeroDocumento = Criptografia.Criptografar(sNumeroDocumento, sChave, sVetorInicializacao);
            sUrlRetorno += "?numerodocumento=" + sNumeroDocumento;
            sUrlRetorno += "&message=" + responseTransacao.ReturnMessage + "; " + ex.Message;
            ScriptManager.RegisterClientScriptBlock(upPagamento, this.GetType(), DateTime.Now.Ticks.ToString(), "ProcessarVenda('" + sUrlRetorno + "')", true);
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

        CarregarDadosProduto(iIDVenda);

        if (PodeSalvar())
            EfetuarPagamento(iIDVenda);
    }
}
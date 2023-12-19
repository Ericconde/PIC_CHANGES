using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Model.objetos;
using Controller;
using System.Configuration;
//using Cielo.Messages;

public partial class administrador_transacaoCartao : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, false, false, false, false, false, true, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            HttpContext.Current.Session["Venda"] = null;

            if(ConfigurationManager.AppSettings["TestandoSistema"].ToString() == "Sim")
            {
            }
        }
    }

    protected override void OnPreInit(EventArgs e)
    {
        if (Session["usuario"] == null) Response.Redirect("../login/logout.aspx");

        usuario UsuarioLogado = (usuario)Session["usuario"];

        if (UsuarioLogado.Perfil == "Administrador")
        {
            MasterPageFile = "~/controls/Admin.master";
        }
        else if (UsuarioLogado.Perfil == "Vendedor")
        {
            MasterPageFile = "~/controls/Vendedor.master";
        }
        else if (UsuarioLogado.Perfil == "Financeiro")
        {
            MasterPageFile = "~/controls/Financeiro.master";
        }
    }

    private void CarregarRetornoTransacaoCartao()
    {
        try
        {
            Cielo.Configuration.IConfiguration customConfiguration = new Cielo.Configuration.CustomConfiguration()
            {
                DefaultEndpoint = ConfigurationManager.AppSettings["cielo.endpoint.default"],
                QueryEndpoint = ConfigurationManager.AppSettings["cielo.endpoint.query"],
                MerchantId = ConfigurationManager.AppSettings["cielo.customer.id"],
                MerchantKey = ConfigurationManager.AppSettings["cielo.customer.key"],
                ReturnUrl = ConfigurationManager.AppSettings["cielo.return.url"],
            };

            var cieloService = new Cielo.CieloService(customConfiguration);

            string sPaymentId = "";

            vendaCTL CVenda = new vendaCTL();
            DataTable dataTable = CVenda.RetornarDadosCielo(txtPaymentId.Text, txtTid.Text);

            if (dataTable.Rows.Count == 0)
            {
                PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Compra não encontrada!", this.Page);
                return;
            }
            else
                sPaymentId = dataTable.Rows[0]["PaymentId"].ToString();

            Guid PaymentId;
            PaymentId = new Guid(sPaymentId);

            var responseCaptura = cieloService.CheckTransaction(PaymentId, null);

            lblRetorno.Text = "responseCaptura.Customer.Name: " + responseCaptura.Customer.Name;
            lblRetorno.Text += "<br/>responseCaptura.MerchantOrderId: " + responseCaptura.MerchantOrderId;
            lblRetorno.Text += "<br/>responseCaptura.Payment.Amount: " + responseCaptura.Payment.Amount;
            lblRetorno.Text += "<br/>responseCaptura.Payment.AuthorizationCode: " + responseCaptura.Payment.AuthorizationCode;
            lblRetorno.Text += "<br/>responseCaptura.Payment.Installments: " + responseCaptura.Payment.Installments;
            lblRetorno.Text += "<br/>responseCaptura.Payment.PaymentId: " + responseCaptura.Payment.PaymentId;
            lblRetorno.Text += "<br/>responseCaptura.Payment.ProofOfSale: " + responseCaptura.Payment.ProofOfSale;
            lblRetorno.Text += "<br/>responseCaptura.Payment.Tid: " + responseCaptura.Payment.Tid;
            lblRetorno.Text += "<br/>responseCaptura.Payment.Status: " + responseCaptura.Payment.Status;            
        }
        catch (Exception ex)
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Erro: " + ex.Message + " A compra pode não ter sido encontrada!", this.Page);
        }
    }

    protected void cmdVoltar_Click(object sender, EventArgs e)
    {
        usuario UsuarioLogado = (usuario)Session["usuario"];

        if (UsuarioLogado.Perfil == "Administrador")
            Response.Redirect("../administrador/default.aspx");
        else if (UsuarioLogado.Perfil == "Vendedor")
            Response.Redirect("../vendedor/default.aspx");
        else if (UsuarioLogado.Perfil == "Contabilidade")
            Response.Redirect("../Contabilidade/Default.aspx");
        else if (UsuarioLogado.Perfil == "Financeiro")
            Response.Redirect("../financeiro/default.aspx");
    }
    
    protected void cmdPesquisar_Click(object sender, EventArgs e)
    {
        if (PodePesquisar())
        {
            CarregarRetornoTransacaoCartao();
        }
    }

    private bool PodePesquisar()
    {
        if (String.IsNullOrEmpty(txtTid.Text.Trim()))
        {
            ExibirMensagem("Preencha [TID].");
            return false;
        }
        return true;
    }

    private bool PodeCancelar()
    {
        if (String.IsNullOrEmpty(txtPaymentId.Text.Trim()))
        {
            ExibirMensagem("Preencha [PaymentId].");
            return false;
        }
        return true;
    }

    private void ExibirMensagem(string sMensagem)
    {
        sMensagem = sMensagem.Replace("'", "");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + sMensagem + "')", true);
    }

    protected void cmdCancelar_Click(object sender, EventArgs e)
    {
        if (PodeCancelar())
        {
            try
            {
                string sPaymentId = "";

                vendaCTL CVenda = new vendaCTL();
                DataTable dataTable = CVenda.RetornarDadosCielo(txtPaymentId.Text, txtTid.Text);

                if (dataTable.Rows.Count == 0)
                {
                    PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Compra não encontrada!", this.Page);
                    return;
                }
                else
                    sPaymentId = dataTable.Rows[0]["PaymentId"].ToString();

                Cielo.Configuration.IConfiguration customConfiguration = new Cielo.Configuration.CustomConfiguration()
                {
                    DefaultEndpoint = ConfigurationManager.AppSettings["cielo.endpoint.default"],
                    QueryEndpoint = ConfigurationManager.AppSettings["cielo.endpoint.query"],
                    MerchantId = ConfigurationManager.AppSettings["cielo.customer.id"],
                    MerchantKey = ConfigurationManager.AppSettings["cielo.customer.key"],
                    ReturnUrl = ConfigurationManager.AppSettings["cielo.return.url"],
                };

                var cieloService = new Cielo.CieloService(customConfiguration);

                Guid PaymentId;
                PaymentId = new Guid(sPaymentId);

                var responseCaptura = cieloService.CancelTransaction(PaymentId, null);
            }
            catch (Exception ex)
            {
                PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Ocorreu uma falha no sistema.\n\n" + ex.Message, this.Page);
            }

            CarregarRetornoTransacaoCartao();
        }
    }
}

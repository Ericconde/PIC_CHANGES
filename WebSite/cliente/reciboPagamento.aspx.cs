using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Controller;
using Model.objetos;
using System.Data;

public partial class cliente_reciboPagamento : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //RetornarVenda(22848);
            //return;

            PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
            string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
            string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
            int iIDVenda = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["id"], sChave, sVetorInicializacao));

            RetornarVenda(iIDVenda);
        }
    }

    private void RetornarVenda(int iIDVenda)
    {
        venda Venda = new venda();
        vendaCTL CVenda = new vendaCTL();

        Venda = CVenda.RetornarVenda(iIDVenda, "2,3,4,5");

        lblVoucher1.Text = "<br/>Núm. Voucher: " + Venda.IDVenda.ToString();
        lblVoucher1.Text += "<br/>Evento: " + Venda.Evento + " ("+Venda.Edicao+")";
        lblVoucher1.Text += "<br/>Data da Compra: " + Venda.DataHoraCompra.ToString();

        if (Venda.NumeroCota == 0)
            lblVoucher1.Text += "<br/><br/>Cliente: " + Venda.Nome;
        if (Venda.NumeroCota != 0 && Venda.NumeroCota > 0)
            lblVoucher1.Text += "<br/><br/>Cliente: " + Venda.Nome + " (cota " + Venda.NumeroCota.ToString() + ")";

        lblVoucher1.Text += "<br/>CPF: " + Venda.CPF;

        lblVoucher1.Text += "<br/><br/>Núm. Ingressos: " + Venda.NumeroIngressos;
        lblVoucher1.Text += "<br/>Valor Total: " + Venda.ValorTotal;
        lblVoucher1.Text += "<br/>Forma de Pagamento: " + Venda.FormaPagamento;
        string sDetalhesFormaPagamento = String.IsNullOrEmpty(Venda.DetalhesFormaPagamento.Trim()) ? "-" : Venda.DetalhesFormaPagamento;
        lblVoucher1.Text += "<br/>Detalhes: " + sDetalhesFormaPagamento + "<br/><br/>";

        //Ingressos
        DataTable dataTable = CVenda.RetornarIngressosRecibo(iIDVenda);
        grdRelatorio.DataSource = dataTable;
        grdRelatorio.DataBind();

        lblVoucher2.Text = "";
        if (Venda.NumeroVagas > 0) lblVoucher2.Text += "<br/>Estacionamento: " + Venda.NumeroVagas.ToString();
        lblVoucher2.Text += "<br/>Vendedor: " + Venda.ResponsavelCompra;

        lblVoucher2.Text += "<br/><br/><br/><img src='../images/logoPIC_email.png'/>";
        lblVoucher2.Text += "<br/>" + ConfigurationManager.AppSettings["EnderecoSistema"].ToString();
        lblVoucher2.Text += "<br/>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    }
    
}
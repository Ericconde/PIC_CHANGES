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

public partial class cliente_Compraconcluida : System.Web.UI.Page
{
    private string sIDVenda = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
            string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
            string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
            double iNumeroDocumento = Convert.ToDouble(Criptografia.Descriptografar(Request.QueryString["NumeroDocumento"], sChave, sVetorInicializacao));
            sIDVenda = Request.QueryString["id"];
            ExibirInformacoes(iNumeroDocumento, sIDVenda);
        }
    }

    private void ExibirInformacoes(double iNumeroDocumento, string sIDVenda)
    {
        vendaCTL CVenda = new vendaCTL();
        Model.objetos.pagamento statusVenda = new Model.objetos.pagamento();
        if(iNumeroDocumento != 0)
        {
            statusVenda = CVenda.RetornarPagamento(iNumeroDocumento);
            lblResumo.Text += "Número do Pedido: " + statusVenda.NumeroDocumento + "<br>";
            lblResumo.Text += "Nome do titular: " + statusVenda.Titular + "<br>";
            //lblResumo.Text += "CPF do Titular: " + "<br>";
            //lblResumo.Text += "Número do Documento: " + "<br>";
            lblResumo.Text += "Cartão: " + statusVenda.Bandeira + "<br>";
            lblResumo.Text += "Valor pago: R$ " + statusVenda.Valor.ToString().Replace(".", ",") + "<br>";
            lblResumo.Text += "Número de parcelas: " + statusVenda.Parcelas + "<br>";
            lblResumo.Text += "Data/Hora autorização: " + statusVenda.Cadastro.ToString("dd/MM/yyyy 00:00") + "<br>";
            lblResumo.Text += "Código TID: " + statusVenda.tid + "<br>";
        }
        else
        {
            DataTable dataSet = CVenda.RetornarPagamentoBysIDVenda(sIDVenda);

            lblResumo.Text += "Número do Pedido: " + (dataSet.Rows[0]["IDVenda"].ToString()) + "<br>";
            lblResumo.Text += "Nome do titular: " + (dataSet.Rows[0]["Nome"].ToString()) + "<br>";
            lblResumo.Text += "CPF do Titular: " + (dataSet.Rows[0]["CPF"].ToString()) + "<br>";

            lblResumo.Text += "Valor pago: R$ " + (dataSet.Rows[0]["ValorTotal"].ToString().Replace(".", ",")) + "<br>";
            lblResumo.Text += "Data/Hora autorização: " + (Convert.ToDateTime(dataSet.Rows[0]["DataHoraCompra"])).ToString("dd/MM/yyyy 00:00") + "<br>";
        }

    }

    protected void btnInicial_Click(object sender, EventArgs e)
    {
        Response.Redirect("../cliente/compras.aspx", false);
    }

    protected void btnGerarRecibo_Click(object sender, EventArgs e)
    {

        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        string IDVenda = Criptografia.Criptografar(Request.QueryString["id"], sChave, sVetorInicializacao);

        Response.Redirect("../cliente/reciboPagamento.aspx?id=" + IDVenda, false);
    }
}
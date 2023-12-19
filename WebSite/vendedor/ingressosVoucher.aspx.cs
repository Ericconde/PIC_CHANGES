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

public partial class vendedor_ingressosVoucher : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
            string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
            string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
            int iIDVenda = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["id"], sChave, sVetorInicializacao));

            CarregarIngressos(iIDVenda);
            HttpContext.Current.Session["Venda"] = null;
        }
    }

    private void CarregarIngressos(int iIDVenda)
    {
        venda Venda = new venda();
        
        vendaCTL CVenda = new vendaCTL();
        CVenda.CarregarGridViewIngressosVoucher(grdIngresso, iIDVenda);

        Venda = CVenda.RetornarVenda(iIDVenda, "2,3,4,5");
        lblVoucher.Text = "Núm. Voucher: " + Venda.IDVenda.ToString();
        lblVoucher.Text += "<br/>Cliente: " + Venda.Nome;
    }
}
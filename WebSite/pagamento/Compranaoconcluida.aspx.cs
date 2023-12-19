using Controller;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cliente_Compranaoconcluida : System.Web.UI.Page
{
    private string sIDVenda = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        sIDVenda = Request.QueryString["id"];        
    }    

    protected void btnInicial_Click(object sender, EventArgs e)
    {
        Response.Redirect("../cliente/compras.aspx", false);
    }

    protected void btnTentarNovamente_Click(object sender, EventArgs e)
    {
        sIDVenda = Request.QueryString["id"];
        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        string sIDEdicao = Criptografia.Criptografar(sIDVenda, sChave, sVetorInicializacao);
        Response.Redirect("../pagamento/checkout.aspx?id=" + sIDEdicao, false);
    }
}
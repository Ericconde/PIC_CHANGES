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

public partial class administrador_logEventos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
            string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
            string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
            int iIDEdicao = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["id"], sChave, sVetorInicializacao));

            CarregarLogAlteracoesEvento(iIDEdicao);
        }
    }

    private void CarregarLogAlteracoesEvento(int iIDEdicao)
    {
        eventoCTL CEvento = new eventoCTL();
        CEvento.CarregarGridViewLogEdicao(grdLogEvento, iIDEdicao);
    }
}
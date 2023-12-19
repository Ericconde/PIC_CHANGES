using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Controller;
using Model.objetos;

public partial class cliente_detalhesEvento : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
            string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
            string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
            int iIDEdicao = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["id"], sChave, sVetorInicializacao));

            CarregarDetalhesEvento(iIDEdicao);
        }
    }

    private void CarregarDetalhesEvento(int iIDEvento)
    {
        eventoCTL evento = new eventoCTL();
        evento Evento = new evento();

        Evento = evento.RetornarEdicao(iIDEvento);

        lblDetalhes.Text = Evento.Detalhes;
    }
}
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

public partial class relatorios_historicocadastrocliente : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, true, false, false, false, false, true, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
            string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
            string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
            int iIDCliente = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["id"], sChave, sVetorInicializacao));

            CarregarHistoricoCadastroSocio(iIDCliente);         
        }
    }

    private void CarregarHistoricoCadastroSocio(int iIDCliente)
    {
        clienteCTL CCliente = new clienteCTL();
        CCliente.CarregarAlteracoesDados(grdHistoricoCadastro, iIDCliente);
    }



 }
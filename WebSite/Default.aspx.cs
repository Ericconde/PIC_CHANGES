using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controller;
using System.IO;
using Model.objetos;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        string sVersaoSistema = "1.7"; 
        string sVersaoFramework = PontoBr.Configuracao.Versao;
        
        Response.Redirect("site/Default.aspx");

        /*PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        string sSenha = Criptografia.Descriptografar("ABA462EDB71AD90A", sChave, sVetorInicializacao);
        Response.Write(sSenha);*/
    }
}

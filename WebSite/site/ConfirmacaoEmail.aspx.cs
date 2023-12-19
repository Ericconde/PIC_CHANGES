using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controller;
using Model.objetos;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;
using System.Web.Security;

public partial class site_ConfirmacaoEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!usuarioCTL.PermitirAcesso(false, false, true, true, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        if (!IsPostBack)
        {
            try
            {
                PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
                usuarioCTL CUsuario = new usuarioCTL();

                if (Request.QueryString["Token"] != null && Request.QueryString["IDUsuario"] != null)
                {
                    string sToken = Request.QueryString["Token"];
                    int iIDUsuario = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["IDUsuario"], sChave, sVetorInicializacao));

                    if (!CUsuario.VerificarExitenciaToken(sToken, iIDUsuario))
                    {
                        usuario UsuarioLogado = CUsuario.RetornarUsuario(iIDUsuario);
                        HttpContext.Current.Session["Usuario"] = UsuarioLogado;

                        divMensagemSucesso.Visible = false;
                        divMensagemErro.Visible = true;
                        cmdEntrar.Visible = false;
                    }
                    else
                    {
                        CUsuario.AtivarUsuario(iIDUsuario);
                        divMensagemSucesso.Visible = true;
                        divMensagemErro.Visible = false;
                        cmdEntrar.Visible = true;
                    }
                }
                else
                {
                    divMensagemSucesso.Visible = false;
                    divMensagemErro.Visible = true;
                    cmdEntrar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divMensagemSucesso.Visible = false;
                divMensagemErro.Visible = true;
                cmdEntrar.Visible = false;

                lblMensagem.Text = "Erro: " + ex.Message;
            }
        }
    }

    protected void cmdEntrar_Click(object sender, EventArgs e)
    {
        Response.Redirect("../site/Default.aspx");
    }
}
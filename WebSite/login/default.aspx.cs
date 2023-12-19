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
using System.Web.Security;

public partial class login_default : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
        }
        Page.Form.DefaultButton = cmdLogar.UniqueID;
    }

    private bool PodeLogar()
    {
        string sMensagem;
        lblMensagem.Text = String.Empty;

        if (txtEmail.Text == "")
        {
            sMensagem = "Preencha [E-mail].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (txtSenha.Text == "")
        {
            sMensagem = "Preencha [Senha].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        string sSenha = Criptografia.Criptografar(txtSenha.Text, sChave, sVetorInicializacao);

        usuarioCTL CUsuario = new usuarioCTL();
        string sEmail = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtEmail.Text);
        if (CUsuario.RetornarUsuario(sEmail, sSenha) == false)
        {
            sMensagem = "[E-mail] e/ou [Senha] incorreto(s).";
            //sMensagem = "Fila excedida, tente mais tarde!";
            lblMensagem.Text = sMensagem;

            return false;
        }

        return true;
    }

    protected void cmdLogar_Click(object sender, EventArgs e)
    {
        try
        {
            if (PodeLogar())
            {
                usuario Usuario = new usuario();
                Usuario = (usuario)HttpContext.Current.Session["Usuario"];

                //Blacklist
                if (Usuario.Perfil == "Não Sócio")
                {
                    usuarioCTL CUsuario = new usuarioCTL();
                    string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];
                    if (CUsuario.VerificarIPBloqueadoBlackList(sIP))
                    {
                        lblMensagem.Text = "Seu dispositivo está bloqueado para acessar o sistema de ingresso. Favor entrar em contato com o clube.";
                        return;
                    }
                }

                //Liberar ingressos reservados
                vendaCTL CVenda = new vendaCTL();
                CVenda.LiberarIngressos(Usuario.IDCliente);

                if (Usuario.Perfil == "Sócio")
                    Response.Redirect("../cliente/compra.aspx");
                else if (Usuario.Perfil == "Não Sócio")
                    Response.Redirect("../cliente/compra.aspx");
                else
                    lblMensagem.Text = "Perfil sem acesso à compra de ingressos!";
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Erro: " + ex.Message;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controller;
using System.Data;
using Model.objetos;
using System.Configuration;

public partial class financeiro_trocarSenha : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(false, false, false, false, false, false, true, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            txtSenhaAtual.Focus();
            Page.Form.DefaultButton = cmdSalvar.UniqueID;
        }
    }

    private bool PodeSalvar()
    {
        string sMensagem;
        lblMensagem.Text = String.Empty;

        if (string.IsNullOrEmpty(txtSenhaAtual.Text.Trim()))
        {
            txtSenhaAtual.Focus();
            sMensagem = "Preencha [Senha Atual].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (string.IsNullOrEmpty(txtNovaSenha.Text.Trim()))
        {
            txtNovaSenha.Focus();
            sMensagem = "Preencha [Nova Senha].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtConfNovaSenha.Text.Trim()))
        {
            txtConfNovaSenha.Focus();
            sMensagem = "Preencha [Confirmar Nova Senha].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (txtNovaSenha.Text != txtConfNovaSenha.Text)
        {
            txtNovaSenha.Focus();
            sMensagem = "[Nova Senha] não confere com [Confirmar Nova Senha].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (txtNovaSenha.Text.IndexOf("'") > -1)
        {
            txtConfNovaSenha.Focus();
            sMensagem = "A [Nova Senha] não pode conter aspas simples (').";
            lblMensagem.Text = sMensagem;
            return false;
        }

        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        string sSenha = Criptografia.Criptografar(txtSenhaAtual.Text, sChave, sVetorInicializacao);

        usuario Usuario = (usuario)HttpContext.Current.Session["Usuario"];
        usuarioCTL CUsuario = new usuarioCTL();
        if (CUsuario.VerificarSenha(Usuario.IDUsuario, sSenha) == false)
        {
            txtSenhaAtual.Focus();
            sMensagem = "A [Senha Atual] está incorreta.";
            lblMensagem.Text = sMensagem;
            return false;
        }

        return true;
    }   

    protected void cmdSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            if (PodeSalvar())
            {
                usuario Usuario = (usuario)HttpContext.Current.Session["Usuario"];

                PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
                string sNovaSenha = Criptografia.Criptografar(txtConfNovaSenha.Text, sChave, sVetorInicializacao);
                int iResetarSenha = 0;

                usuarioCTL CUsuario = new usuarioCTL();
                CUsuario.AlterarSenha(Usuario.Email, sNovaSenha, iResetarSenha);

                lblMensagem.Text = "Senha alterada com sucesso!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redireciona", "window.document.location.href = \"Default.aspx\" ", true);
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Erro: " + ex.Message;
        }
    }
}

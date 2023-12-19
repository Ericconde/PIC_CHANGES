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
using System.Text.RegularExpressions;

public partial class cliente_trocarSenha : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(false, false, true, true, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            txtSenhaAtual.Focus();
            VerificarResetSenha();

            Page.Form.DefaultButton = cmdSalvar.UniqueID;
        }
    }
    private void ExibirMensagemPopUpPositivo(string sMensagem)
    {
        sMensagem = sMensagem.Replace("'", "");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "PopUp('" + sMensagem + "','success')", true);
    }
    private void ExibirMensagemPopUpNegativo(string sMensagem)
    {
        sMensagem = sMensagem.Replace("'", "");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "PopUp('" + sMensagem + "','error')", true);
    }
    private void VerificarResetSenha()
    {
        usuario Usuario = (usuario)HttpContext.Current.Session["Usuario"];
        if (Usuario.ResetarSenha == 1)
            lblMensagem.Text = "Sua senha foi resetada pelo sistema. Favor realizar a alteração da mesma.";
    }

    private bool PodeSalvar()
    {
        string sMensagem;
        lblMensagem.Text = String.Empty;

        if (string.IsNullOrEmpty(txtSenhaAtual.Text.Trim()))
        {
            txtSenhaAtual.Focus();
            sMensagem = "Preencha [Senha Atual].";
            ExibirMensagemPopUpNegativo(sMensagem);
            return false;
        }
        if (string.IsNullOrEmpty(txtNovaSenha.Text.Trim()))
        {
            txtNovaSenha.Focus();
            sMensagem = "Preencha [Nova Senha].";
            ExibirMensagemPopUpNegativo(sMensagem);

            return false;
        }
        if (string.IsNullOrEmpty(txtConfNovaSenha.Text.Trim()))
        {
            txtConfNovaSenha.Focus();
            sMensagem = "Preencha [Confirmar Nova Senha].";
            ExibirMensagemPopUpNegativo(sMensagem);
            return false;
        }
        if (txtNovaSenha.Text != txtConfNovaSenha.Text)
        {
            txtNovaSenha.Focus();
            sMensagem = "[Nova Senha] não confere com [Confirmar Nova Senha].";
            ExibirMensagemPopUpNegativo(sMensagem);
            return false;
        }
        if (txtNovaSenha.Text.IndexOf("'") > -1)
        {
            txtConfNovaSenha.Focus();
            sMensagem = "A [Nova Senha] não pode conter aspas simples (').";
            ExibirMensagemPopUpNegativo(sMensagem);
            return false;
        }

        if (txtNovaSenha.Text.Trim().Length < 6)
        {
            sMensagem = "A [Senha] deve ter, no mínimo, 6 caracteres.";
            ExibirMensagemPopUpNegativo(sMensagem);
            return false;
        }

        if (string.IsNullOrEmpty(txtNovaSenha.Text.Trim()))
        {
            sMensagem = "Preencha [Senha].";
            ExibirMensagemPopUpNegativo(sMensagem);
            return false;
        }

        string[] sSenhasNaoPermitidas = { "000000", "111111", "222222", "333333", "444444", "555555", "666666", "777777", "888888", "999999", "123456", "654321" };
        foreach (var sSenhas in sSenhasNaoPermitidas)
        {
            if (txtNovaSenha.Text.IndexOf(sSenhas) > -1)
            {
                sMensagem = "[Senha] não permitida (número sequencial ou com repetições).";
                ExibirMensagemPopUpNegativo(sMensagem);
                return false;
            }
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
            ExibirMensagemPopUpNegativo(sMensagem);
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

                CUsuario.RetornarUsuario(Usuario.Email, sNovaSenha);

                string sMensagem = "Senha alterada com sucesso!";
                ExibirMensagemPopUpPositivo(sMensagem);
            }
        }
        catch (Exception ex)
        {
            string sMensagem = "Erro: " + ex.Message;
            ExibirMensagemPopUpNegativo(sMensagem);
        }
    }

    private bool GetPontoPorRepeticao(string senha)
    {
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(\w)*.*\1");
        bool repete = regex.IsMatch(senha);
        if (repete)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

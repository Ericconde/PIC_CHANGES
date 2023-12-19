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

public partial class login_reenvioSenha : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
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
    private bool PodeEnviar()
    {
        if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
        {
            string lblMensagem = "Preencha ou selecione [E-mail].";
            ExibirMensagemPopUpNegativo(lblMensagem);
            return false;
        }

        usuarioCTL CUsuario = new usuarioCTL();
        if (!CUsuario.VerificarExistenciaEmail(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtEmail.Text.Trim()), -1))
        {
            string lblMensagem = "[E-mail] não encontrado.";
            ExibirMensagemPopUpNegativo(lblMensagem);
            return false;
        }

        Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());

        if (!Captcha1.UserValidated)
        {
            string lblMensagem = "[Imagem de confirmação] inválida.";
            ExibirMensagemPopUpNegativo(lblMensagem);
            return false;
        }

        return true;
    }

    protected void cmdReenviar_Click(object sender, EventArgs e)
    {
        if (PodeEnviar())
        {
            usuarioCTL CUsuario = new usuarioCTL();

            string sEmail = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtEmail.Text.Trim());

            string sSenha = PontoBr.Seguranca.Senha.GerarSenhaNumerica(6);

            PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
            string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
            string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
            int iResetarSenha = 1;

            //Envia e-mail com os dados de acesso
            string sRetorno = CUsuario.EnviarEmailSenhaAcesso(sEmail, sSenha);

            sSenha = Criptografia.Criptografar(sSenha, sChave, sVetorInicializacao);
            CUsuario.AlterarSenha(sEmail, sSenha, iResetarSenha);

            txtEmail.Text = String.Empty;

            string sMensagem = "Sua senha foi alterada e enviada para seu e-mail.";
            ExibirMensagemPopUpPositivo(sMensagem);
        }
    }
}
    

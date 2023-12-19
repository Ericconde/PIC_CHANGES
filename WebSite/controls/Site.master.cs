using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controller;
using Model.objetos;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data;
using System.Web.Security;

public partial class controls_Site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SelecionarMenu();
            OcultarAreaUsuario();

            if (ConfigurationManager.AppSettings["TestandoSistema"].ToString() == "Sim")
            {
                //txtEmail.Attributes.Add("value", "victor@pontobrsistemas.com.br");
                txtEmail.Attributes.Add("value", "victor.geraldo@hotmail.com");
                txtSenha.Attributes.Add("value", "pic1*");
            }
        }
        AdpaterMenu();

        string sPagina = this.Page.AppRelativeVirtualPath.ToString();
        if (sPagina.IndexOf(@"~/login/default.aspx") == -1)
            Page.Form.DefaultButton = cmdEntrar.UniqueID;
    }

    private void AdpaterMenu()
    {
        if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
        {
            Request.Browser.Adapters.Clear();
        }
    }

    private void OcultarAreaUsuario()
    {
        System.IO.FileInfo fileInfo = new System.IO.FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        string sPageName = fileInfo.Name.ToLower();

        //if (sPageName == "cliente.aspx")
            //divLoginCandidato.Visible = false;
    }

    private void SelecionarMenu()
    {
        foreach (Control c in menus.Controls)
        {
            if (c.GetType().ToString().Equals("System.Web.UI.WebControls.HyperLink"))
            {
                HyperLink hyperLink = (HyperLink)c;

                System.IO.FileInfo fileInfo = new System.IO.FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath);
                string sPageName = fileInfo.Name.ToLower();

                if (hyperLink.NavigateUrl.ToLower().IndexOf(sPageName) > -1)
                    hyperLink.CssClass = "menu_selecionado";
            }
        }
    }

    protected void cmdEntrar_Click(object sender, EventArgs e)
    {
        Criptografar();
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

                if (Usuario.Perfil == "Administrador")
                {
                    if (ConfigurationManager.AppSettings["TestandoSistema"].ToString() == "Sim") 
                        Response.Redirect("../administrador/enviarIngresso.aspx"); //Testando sistema
                    else
                        Response.Redirect("../administrador/Default.aspx");
                }
                else if (Usuario.Perfil == "Vendedor")
                    Response.Redirect("../vendedor/Default.aspx");

                else if (Usuario.Perfil == "Sócio")
                {
                    if (Usuario.ResetarSenha == 1)
                        Response.Redirect("../cliente/trocarSenha.aspx");

                    //Response.Redirect("../cliente/Default.aspx");
                    Response.Redirect("../cliente/compras.aspx");
                }
                else if (Usuario.Perfil == "Não Sócio")
                {
                    if (Usuario.ResetarSenha == 1)
                        Response.Redirect("../cliente/trocarSenha.aspx");

                    Response.Redirect("../cliente/Default.aspx");
                }

                else if (Usuario.Perfil == "Contabilidade")
                {
                    if (Usuario.ResetarSenha == 1)
                        Response.Redirect("../contabilidade/trocarSenha.aspx");

                    Response.Redirect("../contabilidade/relatorios.aspx");
                }
                else if (Usuario.Perfil == "Clube")
                    Response.Redirect("../clube/Default.aspx");
                else if (Usuario.Perfil == "Financeiro")
                {
                    if (ConfigurationManager.AppSettings["TestandoSistema"].ToString() == "Sim")
                        Response.Redirect("../financeiro/clienteDebito.aspx");
                    else
                        Response.Redirect("../financeiro/Default.aspx");
                }
                else if (Usuario.Perfil == "Portaria")
                    Response.Redirect("../portaria/Default.aspx");
                else if (Usuario.Perfil == "Fisco")
                    Response.Redirect("../fisco/Default.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Erro: " + ex.Message;
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
    private static void Criptografar()
    {
        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();

        string sSenha = Criptografia.Criptografar("pic", sChave, sVetorInicializacao);        
    }

    private bool PodeLogar()
    {
        string sMensagem;
        lblMensagem.Text = String.Empty;

        if (txtEmail.Text == "")
        {
            sMensagem = "Preencha [E-mail].";
            lblMensagem.Text=(sMensagem);
            return false;
        }
        if (txtSenha.Text == "")
        {
            sMensagem = "Preencha [Senha].";
            lblMensagem.Text = (sMensagem);
            return false;
        }

        usuarioCTL CUsuario = new usuarioCTL();

        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        string sSenha = Criptografia.Criptografar(txtSenha.Text, sChave, sVetorInicializacao);       
        
        string sEmail = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtEmail.Text);
        if (CUsuario.RetornarUsuario(sEmail, sSenha) == false)
        {
            sMensagem = "[E-mail] e/ou [Senha] incorreto(s).";
            lblMensagem.Text = (sMensagem);
            //lblMensagem.Text = sMensagem;
            return false;
        }

        return true;
    }
}

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

public partial class administrador_usuario : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, false, false, false, false, false, true, true, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        txtNome.Focus();

        if (!IsPostBack)
        {
            DefinirPermissoesPerfil();
            CarregarGridViewUsuarios();

            txtEmailUsuario.Attributes.Add("autocomplete", "off");
            txtEmailUsuario.Text = "";
        }
    }

    protected override void OnPreInit(EventArgs e)
    {
        if (Session["usuario"] == null) Response.Redirect("../login/logout.aspx");

        usuario UsuarioLogado = (usuario)Session["usuario"];

        if (UsuarioLogado.Perfil == "Administrador")
        {
            MasterPageFile = "~/controls/Admin.master";
        }
        else if (UsuarioLogado.Perfil == "Financeiro")
        {
            MasterPageFile = "~/controls/Financeiro.master";
        }
        else if (UsuarioLogado.Perfil == "Portaria")
        {
            MasterPageFile = "~/controls/Portaria.master";
        }
    }

    protected void cmdCancelar_Click(object sender, EventArgs e)
    {
        LimparCampos();
        CarregarGridViewUsuarios();
    }

    private void LimparCampos()
    {
        hddIdUsuario.Value = null;
        txtNome.Text = "";
        txtEmailUsuario.Text = "";
        txtSenhaUsuario.Text = "";
        txtSenhaUsuario.Attributes.Add("value", "");
        radStatus.SelectedValue = "1";
        lblMensagem.Text = "";
        txtNumCota.Text = "";
        txtDigitoCota.Text = "";
        txtCPF.Text = "";
    }

    protected void grdUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gridViewRow = grdUsuario.Rows[Convert.ToInt32(e.CommandArgument)];
        ListItem listItem = new ListItem();
        listItem.Text = Server.HtmlDecode(gridViewRow.Cells[0].Text);
        string sItem = listItem.Text;
        lblMensagem.Text = "";

        if (e.CommandName == "Editar")
        {
            int iIDUsuario = Convert.ToInt32(grdUsuario.DataKeys[int.Parse((string)e.CommandArgument)]["IDUsuario"].ToString());

            usuario Usuario = new usuario();
            usuarioCTL CUsuario = new usuarioCTL();

            Usuario = CUsuario.RetornarUsuario(iIDUsuario);

            hddIdUsuario.Value = Usuario.IDUsuario.ToString();
            txtNome.Text = Usuario.Nome;
            txtEmailUsuario.Text = Usuario.Email;
            txtNumCota.Text = Usuario.NumeroCota.ToString();
            txtDigitoCota.Text = Usuario.DigitoCota.ToString();
            txtCPF.Text = Usuario.CPF;

            PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
            string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
            string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
            Usuario.Senha = Criptografia.Descriptografar(Usuario.Senha, sChave, sVetorInicializacao);

            txtSenhaUsuario.Attributes.Add("value", Usuario.Senha);

            radStatus.SelectedValue = Convert.ToInt32(Usuario.Ativo).ToString();
            radPerfil.SelectedValue = Usuario.IDPerfil.ToString();
        }
    }

    private void DefinirPermissoesPerfil()
    {
        //Permissões do perfil Portaria
        usuario UsuarioLogado = (usuario)Session["usuario"];
        if (UsuarioLogado.Perfil == "Portaria")
        {
            radPerfil.Items.Clear();
            radPerfil.DataBind();

            radPerfil.Items.Add(new ListItem("Sócio", "3"));
            radPerfil.Items.Add(new ListItem("Não Sócio", "4"));
            radPerfil.SelectedValue = "3";
        }
    }

    protected void cmdSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            usuario UsuarioLogado = (usuario)Session["usuario"];
            
            string sMensagem = "";
            if (PodeSalvar())
            {
                usuario UsuarioCadastro = (usuario)HttpContext.Current.Session["Usuario"];

                usuario Usuario = new usuario();
                usuarioCTL CUsuario = new usuarioCTL();
                clienteCTL CCliente = new clienteCTL();

                Usuario.Email = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtEmailUsuario.Text.Trim().ToLower());
                Usuario.Nome = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNome.Text.Trim().ToUpper());
                Usuario.CPF = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtCPF.Text.Trim().ToUpper());

                PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
                Usuario.Senha = Criptografia.Criptografar(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtSenhaUsuario.Text), sChave, sVetorInicializacao);

                Usuario.IDPerfil = Convert.ToInt32(radPerfil.SelectedValue);
                Usuario.Ativo = Convert.ToInt32(radStatus.SelectedValue);
                Usuario.DataCadastro = PontoBr.Conversoes.Data.ConverterDataDMMAAAAComBarraParaFormatoBancoAAAAMMDD(DateTime.Now.ToString("dd/MM/yyyy"));

                Usuario.NumeroCota = String.IsNullOrEmpty(txtNumCota.Text.Trim()) ? -1 : Convert.ToInt32(txtNumCota.Text);
                Usuario.DigitoCota = String.IsNullOrEmpty(txtDigitoCota.Text.Trim()) ? null : txtDigitoCota.Text;

                //Editar
                if (!String.IsNullOrEmpty(hddIdUsuario.Value))
                {
                    Usuario.IDUsuario = Convert.ToInt32(hddIdUsuario.Value);

                    //Registrar log de alteração de e-mail ///////////////////////////
                    usuario UsuarioCadastrado = new usuario();
                    UsuarioCadastrado = CUsuario.RetornarUsuario(Usuario.IDUsuario);
                    if (UsuarioCadastrado.Email.Trim() != Usuario.Email)
                        CUsuario.CadastrarLogAlteracaoEmail(Usuario.IDUsuario, Usuario.Email, UsuarioCadastrado.Email, UsuarioLogado.IDUsuario);
                    ///////////////////////////////////////////////////////////////////

                   Usuario.IDUsuario = Convert.ToInt32(hddIdUsuario.Value);
                    CUsuario.AtualizarUsuario(Usuario, UsuarioLogado.IDUsuario);

                    sMensagem = "Alterações salvas com sucesso!";
                }
                else //Salvar novo
                {
                    CUsuario.CadastrarUsuario(Usuario);
                    sMensagem = "Usuário salvo com sucesso!";
                }

                LimparCampos();
                CarregarGridViewUsuarios();

                lblMensagem.Text = sMensagem;
            }
        }
        catch { }
    }

    private bool PodeSalvar()
    {
        string sMensagem;
        lblMensagem.Text = String.Empty;

        if (String.IsNullOrEmpty(radPerfil.SelectedValue))
        {
            sMensagem = "Selecione [Pefil].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtNome.Text.Trim()))
        {
            sMensagem = "Preencha [Nome].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtEmailUsuario.Text.Trim()))
        {
            sMensagem = "Preencha [Email].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (string.IsNullOrEmpty(txtSenhaUsuario.Text.Trim()))
        {
            sMensagem = "Preencha [Senha].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        int iIDUsuario = -1;
        if (!String.IsNullOrEmpty(hddIdUsuario.Value)) iIDUsuario = Convert.ToInt32(hddIdUsuario.Value);

        usuarioCTL CUsuario = new usuarioCTL();
        if (CUsuario.VerificarExistenciaEmail(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtEmailUsuario.Text.Trim()), iIDUsuario))
        {
            sMensagem = "[E-mail] já cadastrado.";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (radPerfil.SelectedValue != "3")//Sócio
        {
            if (!String.IsNullOrEmpty(txtNumCota.Text.Trim()))
            {
                sMensagem = "Para o perfil [" + radPerfil.SelectedItem + "], não há [Núm. da Cota].";
                lblMensagem.Text = sMensagem;
                return false;
            }
            if (!String.IsNullOrEmpty(txtDigitoCota.Text.Trim()))
            {
                sMensagem = "Para o perfil [" + radPerfil.SelectedItem + "], não há [Dígito da Cota].";
                lblMensagem.Text = sMensagem;
                return false;
            }
        }
        if (radPerfil.SelectedValue == "3")//Sócio
        {
            if (String.IsNullOrEmpty(txtNumCota.Text.Trim()))
            {
                sMensagem = "Informe [Núm. da Cota].";
                lblMensagem.Text = sMensagem;
                return false;
            }
            if (String.IsNullOrEmpty(txtDigitoCota.Text.Trim()))
            {
                sMensagem = "Informe [Dígito da Cota].";
                lblMensagem.Text = sMensagem;
                return false;
            }
        }
        if (PontoBr.Utilidades.String.ValidarCPF(txtCPF.Text) == false)
        {
            sMensagem = "[CPF] inválido.";
            lblMensagem.Text = sMensagem;
            return false;
        }
        string sCPF = PontoBr.Utilidades.String.RemoverCaracteresMascara(txtCPF.Text);
        clienteCTL CCliente = new clienteCTL();
        if (CCliente.VerificarExistenciaCPF(sCPF, iIDUsuario))
        {
            sMensagem = "[CPF] já cadastrado.";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (String.IsNullOrEmpty(hddIdUsuario.Value))
        {
            if (radPerfil.SelectedValue == "3" || radPerfil.SelectedValue == "4")
            {
                sMensagem = "Não é possível cadastrar usuários do perfil Sócio e não sócio.";
                lblMensagem.Text = sMensagem;
                return false;
            }
        }

        return true;
    }

    protected void radPerfil_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregarGridViewUsuarios();
    }

    protected void grdUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdUsuario.PageIndex = e.NewPageIndex;
        CarregarGridViewUsuarios();
    }

    private void CarregarGridViewUsuarios()
    {
        usuarioCTL CUsuario = new usuarioCTL();

        int iIDPerfil = -1;
        if (!String.IsNullOrEmpty(radPerfil.SelectedValue)) iIDPerfil = Convert.ToInt32(radPerfil.SelectedValue);

        int iNumeroRegistros = CUsuario.CarregarGridViewUsuarios(grdUsuario, txtNome.Text, null, iIDPerfil, txtNumCota.Text, txtCPF.Text);

        lblNumeroLinhas.Text = "| " + iNumeroRegistros.ToString() + " registro(s) |";
    }

    protected void cmdPesquisar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = String.Empty;
        CarregarGridViewUsuarios();
    }
}

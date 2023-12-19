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
using System.Text.RegularExpressions;

public partial class vendedor_clienteCadastro : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(false, true, false, false, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        
        
        if (!IsPostBack)
        {
            CarregarEstados();
            HttpContext.Current.Session["Venda"] = null;
        }
    }

    protected void cmdPesquisar_Click(object sender, EventArgs e)
    {
        string sMensagem;
        lblMensagemPesquisa.Text = String.Empty;

        if (PodePesquisar())
        {
            socioCTL CSocio = new socioCTL();
            string sNumeroCota = txtNumCota_pesquisa.Text.ToString().Trim();
            string sCPF = PontoBr.Utilidades.String.RemoverCaracterEspecialCpfCnpj(txtCPF_pesquisa.Text.ToString());

            socio Socio = new socio();

            Socio = CSocio.RetornarSocio(sNumeroCota, sCPF);
            if (Socio.Num_Cota != 0)
            {
                txtNome.Text = Socio.Nome.ToString();
                txtCPF.Text = PontoBr.Utilidades.String.RemoverCaracterEspecialCpfCnpj(Socio.CPF.ToString());
                txtRG.Text = Socio.RG.ToString();
                txtDataNascimento.Text = Socio.Dat_Nasc.ToString();
                txtEmail.Text = Socio.Email.ToString();
                txtTelefoneResidencial.Text = Socio.fone1.ToString();
                txtTelefoneComercial.Text = Socio.fone2.ToString();
                txtLogradouro.Text = Socio.Logradouro.ToString();
                txtNumero.Text = Socio.Numero.ToString();
                txtComplemento.Text = Socio.Complemento.ToString();
                txtBairro.Text = Socio.Bairro.ToString();
                txtCEP.Text = Socio.CEP.ToString();

                int iIDEstado = CSocio.RetornarIDEstado(Socio.Nom_Estado.ToString());
                if (iIDEstado != -1)
                {
                    dropEstado.SelectedValue = iIDEstado.ToString();
                    CarregarCidades();

                    int iIDCidade = CSocio.RetornarIDCidade(iIDEstado, Socio.Nom_Cidade.ToString());
                    dropCidade.SelectedValue = iIDCidade.ToString();
                }
            }
            else
            {
                sMensagem = "Sócio não localizado!";
                LimparFormulario();
                lblMensagemPesquisa.Text = sMensagem;
            }
        }
    }

    private bool PodePesquisar()
    {
        string sMensagem;
        lblMensagemPesquisa.Text = String.Empty;

        if (string.IsNullOrEmpty(txtNumCota_pesquisa.Text.Trim()))
        {
            sMensagem = "Preencha ou selecione [Número da cota].";
            lblMensagemPesquisa.Text = sMensagem;
            return false;
        }
        if (txtDigitoCota_pesquisa.Text.Trim() != "00" && txtDigitoCota_pesquisa.Text.Trim() != "01")
        {
            sMensagem = "O [Dígito da Cota] deve ser 00 ou 01.";
            lblMensagemPesquisa.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtCPF_pesquisa.Text.Trim()))
        {
            sMensagem = "Preencha ou selecione [CPF].";
            lblMensagemPesquisa.Text = sMensagem;
            return false;
        }
        if (PontoBr.Utilidades.String.ValidarCPF(txtCPF_pesquisa.Text) == false)
        {
            sMensagem = "[CPF] inválido.";
            lblMensagemPesquisa.Text = sMensagem;
            return false;
        }

        string sCPF = PontoBr.Utilidades.String.RemoverCaracteresMascara(txtCPF_pesquisa.Text);
        int iNumeroCota = Convert.ToInt32(txtNumCota_pesquisa.Text);
        string sDigitoCota = txtDigitoCota_pesquisa.Text;

        socioCTL CSocio = new socioCTL();
        if (!CSocio.VerificarExistenciaSocio(iNumeroCota, sDigitoCota, sCPF))
        {
            LimparFormulario();

            sMensagem = "O [Núm. da Cota] não corresponde ao [CPF]. Entre em contato com o Clube para atualizar imediatamente seu cadastro.";
            lblMensagemPesquisa.Text = sMensagem;
            return false;
        }

        return true;
    }

    protected void cmdCancelar_Click(object sender, EventArgs e)
    {
        LimparFormulario();
    }


    protected void cmdNovo_Click(object sender, EventArgs e)
    {
        try
        {
            if (PodeSalvar())
            {
                cliente Cliente = new cliente();
                clienteCTL CCliente = new clienteCTL();

                Cliente.Nome = PontoBr.Utilidades.String.RemoverCaracterInvalido(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNome.Text.Trim().ToUpper()));
                Cliente.CPF = PontoBr.Utilidades.String.RemoverCaracteresMascara(txtCPF.Text);
                Cliente.DataNascimento = PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(txtDataNascimento.Text);

                Cliente.RG = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtRG.Text);

                Cliente.Logradouro = PontoBr.Utilidades.String.RemoverCaracterInvalido(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtLogradouro.Text.Trim().Trim().ToUpper()));
                Cliente.Numero = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNumero.Text);
                Cliente.Complemento = PontoBr.Utilidades.String.RemoverCaracterInvalido(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtComplemento.Text.Trim().ToUpper()));
                Cliente.Bairro = PontoBr.Utilidades.String.RemoverCaracterInvalido(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtBairro.Text.Trim().ToUpper()));
                Cliente.IDCidade = Convert.ToInt32(dropCidade.SelectedValue);
                Cliente.CEP = PontoBr.Utilidades.String.RemoverCaracteresMascara(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtCEP.Text));
                Cliente.IDPerfil = Convert.ToInt32(radPerfil.SelectedValue);

                if (radPerfil.SelectedValue == "3") // 3 = Sócio
                {
                    Cliente.NumCota = Convert.ToInt32(txtNumCota_pesquisa.Text);
                    Cliente.DigitoCota = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtDigitoCota_pesquisa.Text);
                }
                else
                    Cliente.NumCota = 0;

                try
                {
                    double dTelefoneCelular = Convert.ToDouble(PontoBr.Utilidades.String.RemoverCaracteresMascara(txtTelefoneCelular.Text.Trim()));
                    Cliente.TelefoneCelular = dTelefoneCelular;
                }
                catch
                {
                    double dTelefoneCelular = 0;
                    Cliente.TelefoneCelular = dTelefoneCelular;
                }
                try
                {
                    double dTelefoneComercial = Convert.ToDouble(PontoBr.Utilidades.String.RemoverCaracteresMascara(txtTelefoneComercial.Text.Trim()));
                    Cliente.TelefoneComercial = dTelefoneComercial;
                }
                catch
                {
                    double dTelefoneComercial = 0;
                    Cliente.TelefoneComercial = dTelefoneComercial;
                }
                try
                {
                    double dTelefoneResidencial = Convert.ToDouble(PontoBr.Utilidades.String.RemoverCaracteresMascara(txtTelefoneResidencial.Text.Trim()));
                    Cliente.TelefoneResidencial = dTelefoneResidencial;
                }
                catch
                {
                    double dTelefoneResidencial = 0;
                    Cliente.TelefoneResidencial = dTelefoneResidencial;
                }

                PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
                string sSenha = Criptografia.Criptografar(txtSenha.Text, sChave, sVetorInicializacao);

                usuario Usuario = new usuario();
                usuarioCTL CUsuario = new usuarioCTL();

                Usuario.Nome = Cliente.Nome;
                Usuario.Email = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtEmail.Text.ToLower().Trim());
                Usuario.Senha = sSenha;
                Usuario.IDPerfil = Convert.ToInt32(radPerfil.SelectedValue);
                Usuario.Ativo = 1;

                //Salva o usuário
                Cliente.IDUsuario = CUsuario.CadastrarUsuario(Usuario);
                //Cadastra o cliente
                CCliente.CadastrarCliente(Cliente);

                LimparFormulario();
                lblMensagem.Text = "Cliente cadastrado com sucesso!";
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Erro: " + ex.Message;
        }
    }

    private bool PodeSalvar()
    {

        string sMensagem;
        lblMensagem.Text = String.Empty;
        usuarioCTL CUsuario = new usuarioCTL();

        if (radPerfil.SelectedValue == "")
        {
            sMensagem = "Selecione se é sócio ou não sócio.";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (radPerfil.SelectedValue == "3") // 3 = Sócio
        {
            if (string.IsNullOrEmpty(txtNumCota_pesquisa.Text.Trim()))
            {
                sMensagem = "Preencha ou selecione [Número da Cota].";
                lblMensagem.Text = sMensagem;
                return false;
            }
            if (txtDigitoCota_pesquisa.Text.Trim() != "00" && txtDigitoCota_pesquisa.Text.Trim() != "01")
            {
                sMensagem = "O [Dígito da Cota] deve ser 00 ou 01.";
                lblMensagemPesquisa.Text = sMensagem;
                return false;
            }

            int iNumeroCota = Convert.ToInt32(txtNumCota_pesquisa.Text);
            string sDigitoCota = txtDigitoCota_pesquisa.Text;
            if (CUsuario.VerificarExistenciaNumeroCota(iNumeroCota, sDigitoCota, -1))
            {
                sMensagem = "[Número da Cota] já cadastrado.";
                lblMensagem.Text = sMensagem;
                return false;
            }
            string sCPFPesquisa = PontoBr.Utilidades.String.RemoverCaracteresMascara(txtCPF_pesquisa.Text);
            socioCTL CSocio = new socioCTL();
            if (!CSocio.VerificarExistenciaSocio(iNumeroCota, sDigitoCota, sCPFPesquisa))
            {
                LimparFormulario();

                sMensagem = "O [Núm. da Cota] não corresponde ao [CPF].";
                lblMensagemPesquisa.Text = sMensagem;
                return false;
            }
        }

        if (string.IsNullOrEmpty(txtNome.Text.Trim()))
        {
            sMensagem = "Preencha ou selecione [Nome].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtRG.Text.Trim()))
        {
            sMensagem = "Preencha ou selecione [RG].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (txtRG.Text.Trim().Length < 5)
        {
            sMensagem = "[RG] inválido.";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (string.IsNullOrEmpty(txtCPF.Text))
        {
            sMensagem = "Preencha [CPF].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (PontoBr.Utilidades.String.ValidarCPF(txtCPF.Text) == false)
        {
            sMensagem = "[CPF] inválido.";
            lblMensagem.Text = sMensagem;
            return false;
        }

        string sCPF = PontoBr.Utilidades.String.RemoverCaracteresMascara(txtCPF.Text);
        clienteCTL CCliente = new clienteCTL();
        if (CCliente.VerificarExistenciaCPF(sCPF, -1))
        {
            sMensagem = "[CPF] já cadastrado.";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (radPerfil.SelectedValue == "3") //Sócio
        {
            sCPF = PontoBr.Utilidades.String.RemoverCaracteresMascara(txtCPF.Text);
            int iNumeroCota = Convert.ToInt32(txtNumCota_pesquisa.Text);
            string sDigitoCota = txtDigitoCota_pesquisa.Text;

            socioCTL CSocio = new socioCTL();
            if (!CSocio.VerificarExistenciaSocio(iNumeroCota, sDigitoCota, sCPF))
            {
                sMensagem = "O [Núm. da Cota] não corresponde ao [CPF].";
                lblMensagemPesquisa.Text = sMensagem;
                return false;
            }
        }

        if (string.IsNullOrEmpty(txtDataNascimento.Text))
        {
            sMensagem = "Preencha ou selecione [Data de Nascimento].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (!PontoBr.Conversoes.Data.ValidarDataBr(txtDataNascimento.Text))
        {
            sMensagem = "[Data de Nascimento] inválida.";
            lblMensagem.Text = sMensagem;
            return false;
        }

        string sTelefoneResidencial = PontoBr.Utilidades.String.RemoverCaracteresMascara(txtTelefoneResidencial.Text.Trim());
        if (!string.IsNullOrEmpty(sTelefoneResidencial) || txtTelefoneResidencial.Text.Trim() == "")
        {
            if (sTelefoneResidencial.Length != 10
            && sTelefoneResidencial.Length != 11)
            {
                sMensagem = "Preencha ou selecione [Telefone Residencial].";
                lblMensagem.Text = sMensagem;
                return false;
            }
        }

        if (string.IsNullOrEmpty(txtCEP.Text.Trim()))
        {
            sMensagem = "Preencha ou selecione [CEP].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (txtCEP.Text.Length < 8)
        {
            sMensagem = "Preencha ou selecione [CEP].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (string.IsNullOrEmpty(txtLogradouro.Text.Trim()))
        {
            sMensagem = "Preencha ou selecione [Logradouro].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (string.IsNullOrEmpty(txtNumero.Text.Trim()))
        {
            sMensagem = "Preencha ou selecione [Número].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (string.IsNullOrEmpty(txtBairro.Text.Trim()))
        {
            sMensagem = "Preencha ou selecione [Bairro].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (dropEstado.SelectedValue == "-1")
        {
            sMensagem = "Preencha ou selecione [Estado].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (dropCidade.SelectedValue == "-1")
        {
            sMensagem = "Preencha ou selecione [Cidade].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
        {
            sMensagem = "Preencha ou selecione [E-mail].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (CUsuario.VerificarExistenciaEmail(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtEmail.Text.Trim()), -1))
        {
            sMensagem = "[E-mail] já cadastrado.";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (txtSenha.Text.Trim().Length < 6)
        {
            sMensagem = "A [Senha] deve ter, no mínimo, 6 caracteres.";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (string.IsNullOrEmpty(txtSenha.Text.Trim()))
        {
            sMensagem = "Preencha [Senha].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        string[] sSenhasNaoPermitidas = { "000000", "111111", "222222", "333333", "444444", "555555", "666666", "777777", "888888", "999999", "123456", "654321" };
        foreach (var sSenha in sSenhasNaoPermitidas)
        {
            if (txtSenha.Text.IndexOf(sSenha) > -1)
            {
                sMensagem = "[Senha] não permitida (número sequencial ou com repetições).";
                lblMensagem.Text = sMensagem;
                return false;
            }
        }

        if (txtCPF.Text.IndexOf(txtSenha.Text) > -1)
        {
            sMensagem = "A [Senha] está similar ao [CPF].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        string sDataNascimento = txtDataNascimento.Text.Replace("/", "");
        if (sDataNascimento.IndexOf(txtSenha.Text) > -1)
        {
            sMensagem = "A [Senha] está similar à [Data de Nascimento].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        return true;
    }

    protected void radPerfil_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radPerfil.SelectedValue == "4") //Não sócio
        {
            txtNumCota_pesquisa.Text = "";
            txtCPF_pesquisa.Text = "";
            txtNumCota_pesquisa.ReadOnly = true;
            cmdPesquisar.Enabled = false;
            UpdatePanel2.Visible = false;
        }
        else
        {
            txtNumCota_pesquisa.ReadOnly = false;
            cmdPesquisar.Enabled = true;
            UpdatePanel2.Visible = true;
        }
    }

    private void CarregarEstados()
    {
        clienteCTL CCliente = new clienteCTL();
        CCliente.CarregarDropDownEstado(dropEstado);

        dropEstado.SelectedValue = "11";
        CarregarCidades();
    }

    protected void dropEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregarCidades();
    }

    private void CarregarCidades()
    {
        clienteCTL CCliente = new clienteCTL();
        CCliente.CarregarDropDownCidade(dropCidade, Convert.ToInt32(dropEstado.SelectedValue));
        dropCidade.Focus();
    }

    private void LimparFormulario()
    {
        txtNome.Text = string.Empty;
        txtRG.Text = string.Empty;
        txtCPF.Text = string.Empty;
        txtDataNascimento.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtTelefoneResidencial.Text = string.Empty;
        txtTelefoneComercial.Text = string.Empty;
        txtTelefoneCelular.Text = string.Empty;
        txtLogradouro.Text = string.Empty;
        txtNumero.Text = string.Empty;
        txtComplemento.Text = string.Empty;
        txtBairro.Text = string.Empty;
        CarregarEstados();
        txtCEP.Text = string.Empty;
        txtCPF_pesquisa.Text = string.Empty;
        txtNumCota_pesquisa.Text = String.Empty;
        lblMensagem.Text = string.Empty;

        txtEmail.Text = string.Empty;
        txtSenha.Text = string.Empty;
    }
}
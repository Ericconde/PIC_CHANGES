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

public partial class cliente_editarCliente : App_Code.BaseWeb
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
            CarregarEstados();
            CarredarDadosCliente();   //Robson 20180313         
        }
    }

    private void CarredarDadosCliente()
    {
        string sMensagem;
        lblMensagem.Text = String.Empty;

        try
        {
            clienteCTL CClientes = new clienteCTL();
            cliente Cliente = new cliente();
            usuario Usuario = (usuario)HttpContext.Current.Session["Usuario"];
            int iIDUsuario = Usuario.IDUsuario;

            Cliente = CClientes.RetornarCliente(iIDUsuario);
            if (Cliente.IDCliente != 0)
            {
                txtNome.Text = Cliente.Nome.ToString();
                txtRG.Text = Cliente.RG.ToString();
                txtCPF.Text = Cliente.CPF.ToString();
                txtDataNascimento.Text = Cliente.DataNascimento.ToString();
                txtTelefoneResidencial.Text = Cliente.TelefoneResidencial.ToString();
                txtTelefoneComercial.Text = Cliente.TelefoneComercial.ToString();
                txtTelefoneCelular.Text = Cliente.TelefoneCelular.ToString();
                txtCEP.Text = Cliente.CEP.ToString();
                txtLogradouro.Text = Cliente.Logradouro.ToString();
                txtNumero.Text = Cliente.Numero.ToString();
                txtComplemento.Text = Cliente.Complemento.ToString();
                txtBairro.Text = Cliente.Bairro.ToString();
                dropEstado.SelectedValue = Cliente.IDEstado.ToString();
                CarregarCidades();
                dropCidade.SelectedValue = Cliente.IDCidade.ToString();
                txtEmail.Text = Cliente.Email;
            }
            else
            {
                sMensagem = "Usuário não localizado!";
                lblMensagem.Text = sMensagem;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Erro: " + ex.Message;
        }
    }

    protected void cmdCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }

    protected void cmdSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            if (PodeSalvar())
            {
                cliente Cliente = new cliente();
                clienteCTL CCliente = new clienteCTL();
                usuarioCTL CUsuario = new usuarioCTL();
                usuario Usuario = (usuario)HttpContext.Current.Session["Usuario"];
                int iIDUsuario = Usuario.IDUsuario;
                Cliente = CCliente.RetornarCliente(iIDUsuario);

                if (txtNome.Text != Cliente.Nome)
                    CCliente.CadastrarAlteracaoDados("Nome", Cliente.Nome, PontoBr.Utilidades.String.TirarAcentos(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNome.Text)), Cliente.IDCliente);

                if (PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(txtDataNascimento.Text) != PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(Cliente.DataNascimento))
                    CCliente.CadastrarAlteracaoDados("DataNascimento", PontoBr.Conversoes.Data.ConverterDataBancoFormatoDDMMAAAAComBarra(Cliente.DataNascimento), PontoBr.Conversoes.Data.ConverterDataBancoFormatoDDMMAAAAComBarra(txtDataNascimento.Text), Cliente.IDCliente);

                if (txtRG.Text != Cliente.RG)
                    CCliente.CadastrarAlteracaoDados("RG",Cliente.RG, PontoBr.Utilidades.String.RemoverCaracterInvalido(txtRG.Text), Cliente.IDCliente);

                if (txtLogradouro.Text != Cliente.Logradouro)
                    CCliente.CadastrarAlteracaoDados("Logradouro",Cliente.Logradouro, PontoBr.Utilidades.String.TirarAcentos(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtLogradouro.Text)), Cliente.IDCliente);

                if (txtNumero.Text != Cliente.Numero)
                    CCliente.CadastrarAlteracaoDados("Numero",Cliente.Numero, PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNumero.Text), Cliente.IDCliente);

                if (txtComplemento.Text != Cliente.Complemento)
                    CCliente.CadastrarAlteracaoDados("Complemento",Cliente.Complemento, PontoBr.Utilidades.String.TirarAcentos(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtComplemento.Text)), Cliente.IDCliente);

                if (txtBairro.Text != Cliente.Bairro)
                    CCliente.CadastrarAlteracaoDados("Bairro", Cliente.Bairro, PontoBr.Utilidades.String.TirarAcentos(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtBairro.Text)), Cliente.IDCliente);

                string sEstado = Convert.ToString(dropEstado.SelectedItem);
                if(sEstado != Cliente.Estado)
                    CCliente.CadastrarAlteracaoDados("Estado", Cliente.Estado, sEstado, Cliente.IDCliente);

                string sCidade = Convert.ToString(dropCidade.SelectedItem);
                if (sCidade != Cliente.Cidade)
                    CCliente.CadastrarAlteracaoDados("Cidade", Cliente.Cidade, sCidade, Cliente.IDCliente);             

                if (txtCEP.Text != Cliente.CEP)
                    CCliente.CadastrarAlteracaoDados("CEP",Cliente.CEP,  PontoBr.Utilidades.String.RemoverCaracteresMascara(txtCEP.Text), Cliente.IDCliente);

                if (txtCPF.Text != Cliente.CPF)
                    CCliente.CadastrarAlteracaoDados("CPF", Cliente.CPF, PontoBr.Utilidades.String.RemoverCaracteresMascara(txtCPF.Text), Cliente.IDCliente);

                if (PontoBr.Utilidades.String.RemoverCaracteresMascara(txtTelefoneCelular.Text.Trim()) != Convert.ToString(Cliente.TelefoneCelular))
                    CCliente.CadastrarAlteracaoDados("Telefone Celular", Convert.ToString(Cliente.TelefoneCelular), PontoBr.Utilidades.String.RemoverCaracteresMascara(txtTelefoneCelular.Text.Trim()) == "" ? "0" : PontoBr.Utilidades.String.RemoverCaracteresMascara(txtTelefoneCelular.Text.Trim()), Cliente.IDCliente);

                if (PontoBr.Utilidades.String.RemoverCaracteresMascara(txtTelefoneComercial.Text.Trim()) != Convert.ToString(Cliente.TelefoneComercial))
                    CCliente.CadastrarAlteracaoDados("Telefone Comercial", Convert.ToString(Cliente.TelefoneComercial), PontoBr.Utilidades.String.RemoverCaracteresMascara(txtTelefoneComercial.Text.Trim()) == "" ? "0" : PontoBr.Utilidades.String.RemoverCaracteresMascara(txtTelefoneComercial.Text.Trim()), Cliente.IDCliente);

                if (PontoBr.Utilidades.String.RemoverCaracteresMascara(txtTelefoneResidencial.Text.Trim()) != Convert.ToString(Cliente.TelefoneResidencial))
                    CCliente.CadastrarAlteracaoDados("Telefone Residencial", Convert.ToString(Cliente.TelefoneResidencial), PontoBr.Utilidades.String.RemoverCaracteresMascara(txtTelefoneResidencial.Text.Trim()) == "" ? "0" : PontoBr.Utilidades.String.RemoverCaracteresMascara(txtTelefoneResidencial.Text.Trim()), Cliente.IDCliente);
                
                Cliente.Nome = PontoBr.Utilidades.String.TirarAcentos(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNome.Text.Trim().ToUpper()));
                Cliente.DataNascimento = PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(txtDataNascimento.Text);
                Cliente.RG = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtRG.Text);
                Cliente.Logradouro = PontoBr.Utilidades.String.TirarAcentos(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtLogradouro.Text.Trim().ToUpper()));
                Cliente.Numero = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNumero.Text);
                Cliente.Complemento = PontoBr.Utilidades.String.TirarAcentos(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtComplemento.Text.ToUpper().Trim()));
                Cliente.Bairro = PontoBr.Utilidades.String.TirarAcentos(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtBairro.Text.Trim().ToUpper()));
                Cliente.IDCidade = Convert.ToInt32(dropCidade.SelectedValue);
                Cliente.CEP = PontoBr.Utilidades.String.RemoverCaracteresMascara(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtCEP.Text));
                Cliente.CPF = PontoBr.Utilidades.String.RemoverCaracteresMascara(txtCPF.Text);

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
               
                Cliente.IDUsuario = Usuario.IDUsuario;

                CCliente.AtualizarCliente(Cliente);

                string sMensagem = "Dados atualizados com sucesso!";
                lblMensagem.Text = sMensagem;
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

        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];
        string sCPF = PontoBr.Utilidades.String.RemoverCaracteresMascara(txtCPF.Text);
        clienteCTL CCliente = new clienteCTL();
        if (CCliente.VerificarExistenciaCPF(sCPF, UsuarioLogado.IDUsuario))
        {
            sMensagem = "[CPF] já cadastrado.";
            lblMensagem.Text = sMensagem;
            return false;
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

        return true;
    }

    private void CarregarEstados()
    {
        clienteCTL CCliente = new clienteCTL();
        CCliente.CarregarDropDownEstado(dropEstado);

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
    }
}

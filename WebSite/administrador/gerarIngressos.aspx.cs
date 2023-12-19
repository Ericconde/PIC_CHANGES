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
using System.Net.Mime;
using SelectPdf;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO.Compression;
using Ionic.Zip;

public partial class administrador_gerarIngressos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");

        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, false, false, false, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            HttpContext.Current.Session["Venda"] = null;
            CarregarEventosEdicao();
            CarregarGridIngressos();
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
        else if (UsuarioLogado.Perfil == "Vendedor")
        {
            MasterPageFile = "~/controls/Vendedor.master";
        }
    }

    private void CarregarEventosEdicao()
    {
        try
        {
            eventoCTL evento = new eventoCTL();

            usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

            if (UsuarioLogado.Perfil == "Administrador")
                evento.CarregarDropDownListEventosVenda(dropEvento, false, true);

            if (!String.IsNullOrEmpty(Request.QueryString["idedicao"]))
            {
                PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
                string sIDEdicao = Criptografia.Descriptografar(Request.QueryString["idedicao"], sChave, sVetorInicializacao);
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Erro - " + ex.Message;
        }
    }

    protected void cmdCancelar_Click(object sender, EventArgs e)
    {
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        if (HttpContext.Current.Session["Venda"] != null)
        {
            venda Venda = (venda)HttpContext.Current.Session["Venda"];

            vendaCTL CVenda = new vendaCTL();
            CVenda.LiberarIngressosPresenciais(Venda.IDCliente, Venda.IDEdicao, Venda.IDVenda);
            HttpContext.Current.Session["Venda"] = null;

            if (UsuarioLogado.Perfil == "Administrador")
                PontoBr.Utilidades.Diversos.ExibirAlertaERedirecionarScriptManager("../administrador/default.aspx", this.Page, "Ingressos para portaria cancelados com sucesso!");
        }
        else
        {
            Response.Redirect("default.aspx");
        }
    }

    private bool PodeGerar()
    {
        if (dropEvento.SelectedValue == "-1")
        {
            ExibirMensagem("selecione o campo [Evento].");
            return false;
        }
        if (String.IsNullOrEmpty(txtQteIngressos.Text))
        {
            ExibirMensagem("preencha o campo [Qte. Ingressos].");
            return false;
        }
        venda Venda = (venda)HttpContext.Current.Session["Venda"];
        if (Venda == null)
        {
            ExibirMensagem("Venda já encerrada (selecione novamente o evento ou clique em voltar).");
            return false;
        }

        if (grdIngressos.Rows.Count + Convert.ToInt32(txtQteIngressos.Text) > 300)
        {
            ExibirMensagem("Limite máximo 300 ingressos por vez.");
            txtQteIngressos.Text = "";
            txtQteIngressos.Focus();
            return false;
        }
        return true;
    }

    private bool PodeExportar()
    {
        if (grdIngressos.Rows.Count == 0)
        {
            PontoBr.Utilidades.Diversos.ExibirAlerta("Nenhum ingresso encontrado!");
            return false;
        }
        venda Venda = (venda)HttpContext.Current.Session["Venda"];
        if (Venda == null)
        {
            PontoBr.Utilidades.Diversos.ExibirAlerta("Venda já encerrada (selecione novamente o evento ou clique em voltar).");
            return false;
        }
        return true;
    }

    private void ExibirMensagem(string sMensagem)
    {
        sMensagem = sMensagem.Replace("'", "");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + sMensagem + "')", true);
    }

    protected void dropEvento_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Session["Venda"] = null;
            lblInfo.Text = "";
            lblMensagem.Text = "";
            if (dropEvento.SelectedValue != "-1")
            {
                cmdCancelar.Text = "Cancelar";
                usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];
                usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];

                evento Evento = new evento();
                eventoCTL CEvento = new eventoCTL();
                Evento = CEvento.RetornarEdicao(Convert.ToInt32(dropEvento.SelectedValue));

                venda Venda = new venda();
                Venda.IDEdicao = Convert.ToInt32(dropEvento.SelectedValue);
                Venda.Evento = Evento.Evento;

                clienteCTL CCliente = new clienteCTL();

                cliente ClientePortaria = CCliente.RetornarCliente("VENDA PRESENCIAL"); // para obter o IDCliente portaria (venda presencial)
                Venda.IDCliente = ClientePortaria.IDCliente; // IDCliente venda presencial
                Venda.IDUsuario = Cliente.IDUsuario;
                Venda.Nome = Cliente.Nome;
                Venda.CPF = Cliente.CPF;

                Venda.IDUsuarioCadastro = UsuarioLogado.IDUsuario;
                Venda.IP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

                DataTable dataTable = CEvento.RetornarLotes(Convert.ToInt32(dropEvento.SelectedValue), false);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    DateTime dateInicioVendas = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataRow["Início Venda"].ToString());
                    DateTime dateFimVendas = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataRow["Fim Venda"].ToString());

                    if (DateTime.Now > dateInicioVendas
                        && DateTime.Now < dateFimVendas)
                    {
                        Venda.Lote = Convert.ToInt32(dataRow["Lote"].ToString());
                        Venda.IDLote = Convert.ToInt32(dataRow["IDLote"].ToString());

                        Venda.ValorInteiraCadeiraSocio = Convert.ToDouble(dataRow["ValorInteiraCadeiraSocio"].ToString());
                        Venda.ValorInteiraCadeiraNaoSocio = Convert.ToDouble(dataRow["ValorInteiraCadeiraNaoSocio"].ToString());
                        Venda.ValorInteiraAvulsoSocio = Convert.ToDouble(dataRow["ValorInteiraAvulsoSocio"].ToString());
                        Venda.ValorInteiraAvulsoNaoSocio = Convert.ToDouble(dataRow["ValorInteiraAvulsoNaoSocio"].ToString());

                        Venda.ValorMeiaCadeiraSocio = Convert.ToDouble(dataRow["ValorMeiaCadeiraSocio"].ToString());
                        Venda.ValorMeiaCadeiraNaoSocio = Convert.ToDouble(dataRow["ValorMeiaCadeiraNaoSocio"].ToString());
                        Venda.ValorMeiaAvulsoSocio = Convert.ToDouble(dataRow["ValorMeiaAvulsoSocio"].ToString());
                        Venda.ValorMeiaAvulsoNaoSocio = Convert.ToDouble(dataRow["ValorMeiaAvulsoNaoSocio"].ToString());

                        Venda.ValorSocioMasculino = dataTable.Rows[0]["ValorSocioMasculino"].ToString() == "" ? 0 : Convert.ToDouble(dataRow["ValorSocioMasculino"].ToString());
                        Venda.ValorSocioFeminino = dataTable.Rows[0]["ValorSocioFeminino"].ToString() == "" ? 0 : Convert.ToDouble(dataRow["ValorSocioFeminino"].ToString());
                        Venda.ValorNaoSocioMasculino = dataTable.Rows[0]["ValorNaoSocioMasculino"].ToString() == "" ? 0 : Convert.ToDouble(dataRow["ValorNaoSocioMasculino"].ToString());
                        Venda.ValorNaoSocioFeminino = dataTable.Rows[0]["ValorNaoSocioFeminino"].ToString() == "" ? 0 : Convert.ToDouble(dataRow["ValorNaoSocioFeminino"].ToString());
                    }

                    if (Venda.Lote != 0
                        && Convert.ToInt32(dataRow["Lote"].ToString()) > Venda.Lote)
                    {
                        if (Convert.ToBoolean(dataRow["InteiraCadeiraSocio"])) Venda.ValorInteiraCadeiraSocio = Convert.ToDouble(dataRow["ValorInteiraCadeiraSocio"].ToString());
                        if (Convert.ToBoolean(dataRow["InteiraCadeiraNaoSocio"])) Venda.ValorInteiraCadeiraNaoSocio = Convert.ToDouble(dataRow["ValorInteiraCadeiraNaoSocio"].ToString());
                        if (Convert.ToBoolean(dataRow["InteiraAvulsoSocio"])) Venda.ValorInteiraAvulsoSocio = Convert.ToDouble(dataRow["ValorInteiraAvulsoSocio"].ToString());
                        if (Convert.ToBoolean(dataRow["InteiraAvulsoNaoSocio"])) Venda.ValorInteiraAvulsoNaoSocio = Convert.ToDouble(dataRow["ValorInteiraAvulsoNaoSocio"].ToString());

                        if (Convert.ToBoolean(dataRow["MeiaCadeiraSocio"])) Venda.ValorMeiaCadeiraSocio = Convert.ToDouble(dataRow["ValorMeiaCadeiraSocio"].ToString());
                        if (Convert.ToBoolean(dataRow["MeiaCadeiraNaoSocio"])) Venda.ValorMeiaCadeiraNaoSocio = Convert.ToDouble(dataRow["ValorMeiaCadeiraNaoSocio"].ToString());
                        if (Convert.ToBoolean(dataRow["MeiaAvulsoSocio"])) Venda.ValorMeiaAvulsoSocio = Convert.ToDouble(dataRow["ValorMeiaAvulsoSocio"].ToString());
                        if (Convert.ToBoolean(dataRow["MeiaAvulsoNaoSocio"])) Venda.ValorMeiaAvulsoNaoSocio = Convert.ToDouble(dataRow["ValorMeiaAvulsoNaoSocio"].ToString());

                        if (Convert.ToBoolean(dataRow["SocioMasculino"])) Venda.ValorSocioMasculino = dataTable.Rows[0]["ValorSocioMasculino"].ToString() == "" ? 0 : Convert.ToDouble(dataRow["ValorSocioMasculino"].ToString());
                        if (Convert.ToBoolean(dataRow["SocioFeminino"])) Venda.ValorSocioFeminino = dataTable.Rows[0]["ValorSocioFeminino"].ToString() == "" ? 0 : Convert.ToDouble(dataRow["ValorSocioFeminino"].ToString());
                        if (Convert.ToBoolean(dataRow["NaoSocioMasculino"])) Venda.ValorNaoSocioMasculino = dataTable.Rows[0]["ValorNaoSocioMasculino"].ToString() == "" ? 0 : Convert.ToDouble(dataRow["ValorNaoSocioMasculino"].ToString());
                        if (Convert.ToBoolean(dataRow["NaoSocioFeminino"])) Venda.ValorNaoSocioFeminino = dataTable.Rows[0]["ValorNaoSocioFeminino"].ToString() == "" ? 0 : Convert.ToDouble(dataRow["ValorNaoSocioFeminino"].ToString());
                    }
                }

                vendaCTL CVenda = new vendaCTL();
                Venda.IDVenda = CVenda.CadastrarVenda(Venda);
                HttpContext.Current.Session["Venda"] = Venda;

                CarregarGridIngressos();

                if (Evento.Evento == "Boate")
                {
                    linha_TipoGenero.Visible = true;
                    linha_TipoMeiaInteira.Visible = false;
                }
                else
                {
                    linha_TipoGenero.Visible = false;
                    linha_TipoMeiaInteira.Visible = true;
                }
            }
            else
            {
                linha_TipoGenero.Visible = false;
                linha_TipoMeiaInteira.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Erro - " + ex.Message;
        }
    }
    protected void cmdGerarIngresso_Click(object sender, EventArgs e)
    {
        if (PodeGerar())
        {
            venda Venda = (venda)HttpContext.Current.Session["Venda"];
            usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];
            usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
            venda IngressoAvulso = (venda)HttpContext.Current.Session["Venda"];
            vendaCTL CVenda = new vendaCTL();

            /////////////////////////////////////////
            evento Evento = new evento();
            eventoCTL CEvento = new eventoCTL();
            Evento = CEvento.RetornarEdicao(Venda.IDEdicao);    

            int iQuantidadeIngressos = Convert.ToInt32(txtQteIngressos.Text);

            for (int i = 0; i < iQuantidadeIngressos; i++)
            {
                //QUANDO FOR BOATE
                if (Evento.Evento == "Boate")
                {
                    if (radTipoIngresso.SelectedValue == "Sócio")
                    {
                        IngressoAvulso.Valor = IngressoAvulso.ValorInteiraAvulsoSocio;
                        IngressoAvulso.IDTipoIngresso = 15;
                    }
                    else if(radTipoIngresso.SelectedValue == "Nao sócio")
                    {
                        IngressoAvulso.Valor = IngressoAvulso.ValorInteiraAvulsoNaoSocio;
                        IngressoAvulso.IDTipoIngresso = 16;
                    }
                    else if (radTipoIngresso.SelectedValue == "Cortesia")
                    {
                        IngressoAvulso.Valor = 0;
                        IngressoAvulso.IDTipoIngresso = 17;
                    }
                }

                else
                {
                    if (radTipoMeiaInteira.SelectedValue == "Inteira")
                    {
                        IngressoAvulso.Valor = IngressoAvulso.ValorInteiraAvulsoNaoSocio;
                        IngressoAvulso.IDTipoIngresso = 4;
                    }
                    else
                    {
                        IngressoAvulso.Valor = IngressoAvulso.ValorMeiaAvulsoNaoSocio;
                        IngressoAvulso.IDTipoIngresso = 8;
                    }
                }

                clienteCTL CCliente = new clienteCTL();

                cliente ClientePortaria = CCliente.RetornarCliente("VENDA PRESENCIAL"); // para obter o IDCliente portaria (venda presencial)
                IngressoAvulso.IDCliente = ClientePortaria.IDCliente; 
                CVenda.CadastrarVendaAvulso(IngressoAvulso);
            }

            txtQteIngressos.Text = String.Empty;
            CarregarGridIngressos();
        }
    }

    private void CarregarGridIngressos()
    {
        try
        {
            if (HttpContext.Current.Session["Venda"] != null)
            {
                venda Venda = (venda)HttpContext.Current.Session["Venda"];

                vendaCTL CVenda = new vendaCTL();
                DataSet dataSet = CVenda.RetornarCarrinhoCompras(Venda.IDVenda);

                if (Venda.Evento == "Boate")
                {
                    linha_TipoGenero.Visible = true;
                    linha_TipoMeiaInteira.Visible = false;
                }
                else
                {
                    linha_TipoGenero.Visible = false;
                    linha_TipoMeiaInteira.Visible = true;
                }

                usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

                clienteCTL CCliente = new clienteCTL();
                cliente Cliente = CCliente.RetornarCliente(UsuarioLogado.IDUsuario);

                evento Evento = new evento();
                eventoCTL CEvento = new eventoCTL();
                Evento = CEvento.RetornarEdicao(Convert.ToInt32(Venda.IDEdicao));

                grdIngressos.DataSource = dataSet.Tables[0];
                grdIngressos.DataBind();

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    lblIngressosTotal.Text = "<br/>" + dataSet.Tables[0].Rows.Count.ToString() + " ingresso(s)";
                    lblIngressosTotal.Text += "<br/>Total: R$ ";
                    lblIngressosTotal.Text += dataSet.Tables[1].Rows[0]["Valor"].ToString();
                }
                else
                    lblIngressosTotal.Text = "Total: R$ 0,00";

                dropEvento.SelectedValue = Venda.IDEdicao.ToString();

                PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
                string sIDEdicao = Criptografia.Criptografar(dropEvento.SelectedValue, sChave, sVetorInicializacao);
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Erro - " + ex.Message;
        }
    }

    protected void cmdExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (PodeExportar())
            {
                venda Venda = (venda)HttpContext.Current.Session["Venda"];
                usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

                PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
                string sIDVenda = Criptografia.Criptografar(Venda.IDVenda.ToString(), sChave, sVetorInicializacao);

                Venda.IDLocalRetirada = 1;
                Venda.NomeResponsavelRetirada = "";
                Venda.RgResponsavelRetirada = "";
                Venda.NomeResponsavelRetirada2 = "";
                Venda.RgResponsavelRetirada2 = "";

                HttpContext.Current.Session["Venda"] = Venda;

                vendaCTL CVenda = new vendaCTL();
                int iIDFormaPagamento = 6;
                int iIDVenda = Venda.IDVenda;
                int iNumeroCriancas = 0;
                string sNomeRetirada = "";
                string sRgRetirada = "";
                int iIDLocalRetirada = 1;
                string sDetalhesFormaPagamento = PontoBr.Utilidades.String.RemoverCaracterInvalido("Venda presencial");
                string sRetorno = String.Empty;
                int iNumeroVagas = 0;

                string sNomeRetirada2 = "";
                string sRgRetirada2 = "";

                if (UsuarioLogado.Perfil == "Administrador")
                {
                    CVenda.ConcluirVenda(iIDVenda, iIDFormaPagamento, sDetalhesFormaPagamento, iNumeroVagas,
                    iNumeroCriancas, sNomeRetirada, sRgRetirada, iIDLocalRetirada, sNomeRetirada2, sRgRetirada2);
                    GerarPDF(Venda);
                    HttpContext.Current.Session["Venda"] = null;

                    sRetorno =
                    "<h3 style='color:green;'>INGRESSOS GERADOS!!!!!</h3><br/><br/>" +
                    "<h3 style='color:red;'>IMPRIMA APENAS DOIS INGRESSOS POR PÁGINA.<br/>" +
                    "Imprima os ingressos com boa qualidade, na cor preta, assim evitará erros de leitura do QRCode.</h3><br /><br />" +
                    "Número do Pedido: " + Venda.IDVenda.ToString() + "<br />" +
                    "Nome do titular: " + Venda.Nome + "<br />";
                }

                lblMensagem.Visible = true;
                lblMensagem.Text = sRetorno;
                cmdCancelar.Text = "Voltar";
                grdIngressos.DataSource = null;
                grdIngressos.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Erro: " + ex.Message;
        }
    }

    private void GerarPDF(venda Venda)
    {
        //Verifica se há ingressos no Voucher
        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();

        vendaCTL CVenda = new vendaCTL();
        DataTable dataTable = CVenda.RetornarIngressos(Venda.IDVenda, null);

        if (dataTable.Rows.Count == 0)
        {
            PontoBr.Utilidades.Diversos.ExibirAlerta("Não há ingressos para essa venda!");
            return;
        }

        //Gerar token
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        string sToken = Criptografia.Criptografar(DateTime.Now.Ticks.ToString(), sChave, sVetorInicializacao);
        usuarioCTL CUsuario = new usuarioCTL();

        int iIDUsuario = Convert.ToInt32(CUsuario.RetornarIDUsuario(Venda.IDCliente));
        CUsuario.AtualizarToken(iIDUsuario, sToken);

        RetornarIngressos(sToken, Venda);
    }

    public void RetornarIngressos(string sToken, venda Venda)
    {
        string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];
        vendaCTL CVenda = new vendaCTL();

        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        string sIDVenda = Venda.IDVenda.ToString();
        DataTable dataTable = CVenda.RetornarIngressos(Venda.IDVenda, null);

        string sUrl = HttpContext.Current.Request.Url.AbsoluteUri.ToLower();

        //URL dos ingresso
        List<string> sUrlIngressos = new List<string>();
        List<string> sUrlQRCodes = new List<string>();

        //Para Ingressos
        int iNumeroMaximoIngressosArquivo = 10;
        if (dataTable.Rows.Count <= iNumeroMaximoIngressosArquivo)
        {
            if (Venda.Evento == "Boate")
                sUrlIngressos.Add(sUrl.Substring(0, sUrl.IndexOf("administrador/gerarIngressos.aspx".ToLower())) + "cliente/IngressosBoate.aspx?IDvenda=" + Criptografia.Criptografar(sIDVenda, sChave, sVetorInicializacao) + "&token=" + sToken);
            else
                sUrlIngressos.Add(sUrl.Substring(0, sUrl.IndexOf("administrador/gerarIngressos.aspx".ToLower())) + "cliente/Ingressos.aspx?IDvenda=" + Criptografia.Criptografar(sIDVenda, sChave, sVetorInicializacao) + "&token=" + sToken);
        }
        else //Mais de um arquivo de ingressos 
        {
            int iNumeroIngressosArquivo = 0;
            int iNumeroIngressosTodosArquivos = 0;
            int iArquivo = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                iNumeroIngressosArquivo++;
                iNumeroIngressosTodosArquivos++;
                if (iNumeroIngressosArquivo == iNumeroMaximoIngressosArquivo
                    || iNumeroIngressosTodosArquivos == dataTable.Rows.Count)
                {
                    iArquivo++;
                    if (Venda.Evento == "Boate")
                        sUrlIngressos.Add(sUrl.Substring(0, sUrl.IndexOf("administrador/gerarIngressos.aspx".ToLower())) + "cliente/IngressosBoate.aspx?IDvenda=" + Criptografia.Criptografar(sIDVenda, sChave, sVetorInicializacao) + "&token=" + sToken + "&arquivo=" + iArquivo.ToString());
                    else
                        sUrlIngressos.Add(sUrl.Substring(0, sUrl.IndexOf("administrador/gerarIngressos.aspx".ToLower())) + "cliente/Ingressos.aspx?IDvenda=" + Criptografia.Criptografar(sIDVenda, sChave, sVetorInicializacao) + "&token=" + sToken + "&arquivo=" + iArquivo.ToString());

                    iNumeroIngressosArquivo = 0;
                }
            }
        }

        //Para QRCodes
        int iNumeroMaximoQRCodeArquivo = 250;
        if (dataTable.Rows.Count <= iNumeroMaximoQRCodeArquivo)
        {
            sUrlQRCodes.Add(sUrl.Substring(0, sUrl.IndexOf("administrador/gerarIngressos.aspx")) + "administrador/QRCode.aspx?IDvenda=" + Criptografia.Criptografar(sIDVenda, sChave, sVetorInicializacao) + "&token=" + sToken);
        }
        else //Mais de um arquivo de ingressos 
        {
            int iNumeroQRCodeArquivo = 0;
            int iNumeroQRCodeTodosArquivos = 0;
            int iArquivo = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                iNumeroQRCodeArquivo++;
                iNumeroQRCodeTodosArquivos++;
                if (iNumeroQRCodeArquivo == iNumeroMaximoQRCodeArquivo
                    || iNumeroQRCodeTodosArquivos == dataTable.Rows.Count)
                {
                    iArquivo++;
                    sUrlQRCodes.Add(sUrl.Substring(0, sUrl.IndexOf("administrador/gerarIngressos.aspx")) + "administrador/QRCode.aspx?IDvenda=" + Criptografia.Criptografar(sIDVenda, sChave, sVetorInicializacao) + "&token=" + sToken + "&arquivo=" + iArquivo.ToString());
                    iNumeroQRCodeArquivo = 0;
                }
            }
        }

        ExportarPDF(Venda.IDVenda.ToString(), sUrlIngressos, sUrlQRCodes);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected static string sResumoProcessamento;
    protected static int iQuantidadeProcessado = 0;
    protected static bool bProcessar = false;
    protected static Thread workerThreadCompactar;
    protected static ArrayList sListatArquivos;

    public void ExportarPDF(string sIDVenda, List<string> sURLIngressos, List<string> sUrlQRCodes)
    {
        if (PodeConverter())
        {
            TimerProgresso.Enabled = true;
            bProcessar = true;
            sResumoProcessamento = String.Empty;
            iQuantidadeProcessado = 0;
            usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];
            ArrayList sArquivos = new ArrayList();

            string sPageSize = "A4";
            PdfPageSize pPageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
                sPageSize, true);

            string sPdfOrientacao = "Portrait";
            PdfPageOrientation pPdfOrientation =
                (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
                sPdfOrientacao, true);

            int iWebPageWidth = 1024;
            try
            {
                iWebPageWidth = Convert.ToInt32("870");
            }
            catch { }

            int iWebPageHeight = 0;
            try
            {
                iWebPageHeight = Convert.ToInt32("0");
            }
            catch { }

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = pPageSize;
            converter.Options.PdfPageOrientation = pPdfOrientation;
            converter.Options.WebPageWidth = iWebPageWidth;
            converter.Options.WebPageHeight = iWebPageHeight;

            int iArquivo = 0;

            //Arquivos Ingresso
            foreach (string sURLIngresso in sURLIngressos)
            {
                iArquivo++;

                PdfDocument pDocIngresso = converter.ConvertUrl(sURLIngresso);
                byte[] pdfBufferIngresso = pDocIngresso.Save();

                //gera numero aleatorio
                Random randNum = new Random();
                randNum.Next();
                string sEnderecoArquivoIngresso;

                if (sURLIngressos.Count == 1)
                    sEnderecoArquivoIngresso = System.Web.Hosting.HostingEnvironment.MapPath("~/documentos/ingressos_presenciais/PIC_Ingressos_" + sIDVenda + ".pdf");
                else //Mais de um arquivo de ingressos 
                    sEnderecoArquivoIngresso = System.Web.Hosting.HostingEnvironment.MapPath("~/documentos/ingressos_presenciais/PIC_Ingressos_" + sIDVenda + "_Arquivo" + iArquivo.ToString() + ".pdf");

                FileStream StreamIngresso = new FileStream(sEnderecoArquivoIngresso, FileMode.Create);
                StreamIngresso.Write(pdfBufferIngresso, 0, pdfBufferIngresso.Length);
                StreamIngresso.Close();
                pDocIngresso.Close();
                sArquivos.Add(sEnderecoArquivoIngresso);
            }

            //Arquivos QRCode
            foreach (string sUrlQRCode in sUrlQRCodes)
            {
                iArquivo++;

                PdfDocument pDocQRCode = converter.ConvertUrl(sUrlQRCode);
                byte[] pdfBufferQRCode = pDocQRCode.Save();

                //gera numero aleatorio
                Random randNum = new Random();
                randNum.Next();
                string sEnderecoArquivoQRCode;

                if (sUrlQRCodes.Count == 1)
                    sEnderecoArquivoQRCode = System.Web.Hosting.HostingEnvironment.MapPath("~/documentos/ingressos_presenciais/PIC_qrcode_" + sIDVenda + ".pdf");
                else //Mais de um arquivo de ingressos 
                    sEnderecoArquivoQRCode = System.Web.Hosting.HostingEnvironment.MapPath("~/documentos/ingressos_presenciais/PIC_qrcode_" + sIDVenda + "_Arquivo" + iArquivo.ToString() + ".pdf");

                FileStream StreamQRCode = new FileStream(sEnderecoArquivoQRCode, FileMode.Create);
                StreamQRCode.Write(pdfBufferQRCode, 0, pdfBufferQRCode.Length);
                StreamQRCode.Close();
                pDocQRCode.Close();
                sArquivos.Add(sEnderecoArquivoQRCode);
            }

            sListatArquivos = sArquivos;
            Compactar();

            //Deleta os arquivos criados.
            //DeletarArquivoPDF(sArquivos);
        }
    }

    private void Compactar()
    {
        string sEnderecoArquivo = System.Web.Hosting.HostingEnvironment.MapPath("~/documentos/ingressos_presenciais/");
        string sZipName = String.Empty;

        try
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Default;
                zip.AddDirectoryByName("Ingressos");

                for (int i = 0; i < sListatArquivos.Count; i++)
                {
                    iQuantidadeProcessado++;
                    zip.AddFile(sListatArquivos[i].ToString(), "Ingressos");
                    Thread.Sleep(50);
                }

                sZipName = String.Format("Ingressos_{0}.zip", DateTime.Now.ToString("dd-MMM-yyyy-HHmmss"));
                string sEnderecoArquivoCompleto = sEnderecoArquivo + sZipName;
                zip.Save(sEnderecoArquivoCompleto);

                System.IO.FileInfo promptfile = new FileInfo(sEnderecoArquivoCompleto);
                string name = promptfile.Name;
                {
                    Response.Clear();
                    Response.BufferOutput = false;
                    Response.ContentType = "application/zip";
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + sZipName);
                    Response.AddHeader("Content-Length", promptfile.Length.ToString());
                    Response.WriteFile(sEnderecoArquivoCompleto);
                    Response.Flush();
                }
            }
            Response.Close();
        }
        catch (Exception ex)
        {
            sResumoProcessamento = "Erro: " + ex.InnerException + "<br/>" + ex.Message;
        }

        bProcessar = false;
    }

    private bool PodeConverter()
    {
        lblInfo.Text = String.Empty;

        if (grdIngressos.Rows.Count == 0)
        {
            lblInfo.Text = "Para exportar é preciso gerar ingressos.";
            return false;
        }
        return true;
    }

    protected void TimerProgresso_Tick(object sender, EventArgs e)
    {
        lblInfo.Text = sResumoProcessamento;

        if (sListatArquivos.Count == iQuantidadeProcessado
            && !bProcessar)
        {
            TimerProgresso.Enabled = false;
        }
    }

    protected void grdIngressos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sTipoIngresso = grdIngressos.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
        if (e.CommandName == "Excluir")
        {
            usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

            usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
            vendaCTL CVenda = new vendaCTL();

            double dTicket = Convert.ToDouble(grdIngressos.DataKeys[int.Parse((string)e.CommandArgument)]["Ticket"].ToString());
            string sTextoLog = "Ticket " + dTicket.ToString();
            sTextoLog += "; " + Server.HtmlDecode(grdIngressos.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text);

            CVenda.CancelarTicket(dTicket, sTextoLog, Cliente.IDUsuario);
            CarregarGridIngressos();
        }
    }

    private void DeletarArquivoPDF(ArrayList sArquivos)
    {
        foreach (string sArquivo in sArquivos)
        {
            if (System.IO.File.Exists(sArquivo))
            {
                try
                {
                    System.IO.File.Delete(sArquivo);
                }
                catch (System.IO.IOException e)
                {
                    PontoBr.Utilidades.Diversos.ExibirAlerta(e.Message);
                    return;
                }
            }
        }
    }
}

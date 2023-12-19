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
using System.Text;

public partial class cliente_compra : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, true, true, true, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            divAniversariante.Visible = false;
            CarregarDadosUsuario();
            CarregarEventosEdicao();
            CarregarCarrinhoCompras();
            CarregarTipoIngressoAvulso();

            if (Request.QueryString["NumeroDocumento"] != null)
            {
                string sNumeroDocumento = Request.QueryString["NumeroDocumento"].ToString();

                if (sNumeroDocumento == "Cancelar")
                    Response.Redirect("~/login/logout.aspx");
                else
                    ConsultarTransacao(sNumeroDocumento);
            }
            else if (Request.QueryString["idedicao"] != null)
            {
                PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
                string sIDEdicao = Request.QueryString["idedicao"].ToString();

                dropEdicao.SelectedValue = sIDEdicao;

                if (HttpContext.Current.Session["Venda"] == null)
                    IniciarVenda();
            }
        }

        usuario UsuarioLogado = (usuario)Session["usuario"];
        if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["EmailPermitidoTeste"].ToString())
            && ConfigurationManager.AppSettings["EmailPermitidoTeste"].ToString() != UsuarioLogado.Email)
        {
            dropEdicao.Enabled = false;
            cmdFinalizar.Enabled = false;
        }
    }

    private void CarregarTipoIngressoAvulso()
    {
        radTipoAvulso.Items.Clear();

        usuario Usuario = (usuario)Session["usuario"];

        usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
        if (Usuario.Perfil == "Vendedor" || Usuario.Perfil == "Administrador")
            Usuario = (usuario)HttpContext.Current.Session["ClienteCompra"];

        if (HttpContext.Current.Session["Venda"] != null)
        {
            venda Venda = (venda)HttpContext.Current.Session["Venda"];

            bool bInteiraCadeiraSocio = false;
            bool bInteiraCadeiraNaoSocio = false;
            bool bInteiraAvulsoSocio = false;
            bool bInteiraAvulsoNaoSocio = false;
            bool bMeiaCadeiraSocio = false;
            bool bMeiaCadeiraNaoSocio = false;
            bool bMeiaAvulsoSocio = false;
            bool bMeiaAvulsoNaoSocio = false;
            bool bCamaroteSocio = false;
            bool bCamaroteNaoSocio = false;

            eventoCTL CEvento = new eventoCTL();
            DataTable dataTable = CEvento.RetornarLotes(Venda.IDEdicao, true);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (Convert.ToBoolean(dataRow["InteiraCadeiraSocio"])) bInteiraCadeiraSocio = true;
                if (Convert.ToBoolean(dataRow["InteiraCadeiraNaoSocio"])) bInteiraCadeiraNaoSocio = true;
                if (Convert.ToBoolean(dataRow["InteiraAvulsoSocio"])) bInteiraAvulsoSocio = true;
                if (Convert.ToBoolean(dataRow["InteiraAvulsoNaoSocio"])) bInteiraAvulsoNaoSocio = true;

                if (Convert.ToBoolean(dataRow["MeiaCadeiraSocio"])) bMeiaCadeiraSocio = true;
                if (Convert.ToBoolean(dataRow["MeiaCadeiraNaoSocio"])) bMeiaCadeiraNaoSocio = true;
                if (Convert.ToBoolean(dataRow["MeiaAvulsoSocio"])) bMeiaAvulsoSocio = true;
                if (Convert.ToBoolean(dataRow["MeiaAvulsoNaoSocio"])) bMeiaAvulsoNaoSocio = true;

                if (Convert.ToBoolean(dataRow["CamaroteSocio"])) bCamaroteSocio = true;
                if (Convert.ToBoolean(dataRow["CamaroteNaoSocio"])) bCamaroteNaoSocio = true;
            }

            if (bInteiraAvulsoSocio && Usuario.Perfil == "Sócio") radTipoAvulso.Items.Add("Inteiro sem cadeira");
            if (bInteiraAvulsoNaoSocio) radTipoAvulso.Items.Add("Não sócio Inteiro sem cadeira");

            if (bMeiaAvulsoSocio && Usuario.Perfil == "Sócio") radTipoAvulso.Items.Add("Meio adolescente sem cadeira");
            if (bMeiaAvulsoNaoSocio) radTipoAvulso.Items.Add("Não sócio Meio adolescente sem cadeira");

            if (bCamaroteSocio && Usuario.Perfil == "Sócio") radTipoAvulso.Items.Add("Camarote sócio");
            if (bCamaroteNaoSocio) radTipoAvulso.Items.Add("Camarote não sócio");
        }

        cmdAdicionarAvulso.Visible = false;
        lblAvulso.Visible = false;
        radTipoAvulso.Visible = false;
        txtAvulso.Visible = false;
        if (radTipoAvulso.Items.Count != 0)
        {
            cmdAdicionarAvulso.Visible = true;
            lblAvulso.Visible = true;
            radTipoAvulso.Visible = true;
            txtAvulso.Visible = true;
        }
    }

    private void VerificarSaldoIngressos()
    {
        //Ingressos já vendidos
        vendaCTL CVenda = new vendaCTL();
        int iVendidosCadeira = CVenda.RetornarQuantidadeVendidaCadeira(Convert.ToInt32(dropEdicao.SelectedValue));
        int iVendidosAvulso = CVenda.RetornarQuantidadeVendidaAvulso(Convert.ToInt32(dropEdicao.SelectedValue));

        venda Venda = (venda)HttpContext.Current.Session["Venda"];

        evento Lote = new evento();
        eventoCTL CEvento = new eventoCTL();
        Lote = CEvento.RetornarLote(Venda.IDLote);

        if (iVendidosCadeira >= Lote.IngressosCadeira)
        {
            LinkButtonAbrirMapaInteira.OnClientClick = "alert('Os ingressos cadeira/mesa esgotaram.')";
            LinkButtonAbrirMapaInteira.ToolTip = "Ingressos esgostados";
        }

        if (iVendidosAvulso >= Lote.IngressosAvulsos)
        {
            cmdAdicionarAvulso.ToolTip = "Ingressos esgostados";
        }
    }

    protected override void OnPreInit(EventArgs e)
    {
        if (Session["usuario"] == null) Response.Redirect("../login/logout.aspx");

        usuario UsuarioLogado = (usuario)Session["usuario"];

        //if (UsuarioLogado.Email != "victor.geraldo@hotmail.com") Response.Redirect("../login/logout.aspx");

        if (UsuarioLogado.Perfil == "Sócio")
        {
            MasterPageFile = "~/controls/Cliente.master";
        }
        else if (UsuarioLogado.Perfil == "Não Sócio")
        {
            MasterPageFile = "~/controls/Cliente.master";
        }
        else if (UsuarioLogado.Perfil == "Vendedor")
        {
            MasterPageFile = "~/controls/Vendedor.master";
        }
        if (UsuarioLogado.Perfil == "Administrador")
        {
            MasterPageFile = "~/controls/Admin.master";
        }
    }

    private void CarregarFormasPagamento()
    {
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
        if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
            Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

        vendaCTL CVenda = new vendaCTL();

        evento Evento = new evento();
        eventoCTL CEvento = new eventoCTL();
        Evento = CEvento.RetornarEdicao(Convert.ToInt32(dropEdicao.SelectedValue));

        string sIDFormaPagamento;

        if (Evento.Evento == "Outros")
        {
            DataTable dataTableLotes = null;
            dataTableLotes = CEvento.RetornarLotes(Evento.IDEdicao, true);
            if (UsuarioLogado.Perfil == "Administrador")
                dataTableLotes = CEvento.RetornarLotes(Evento.IDEdicao, false);

            double dValor = 0;
            dValor += Convert.ToDouble(dataTableLotes.Rows[0]["ValorInteiraCadeiraSocio"].ToString());
            dValor += Convert.ToDouble(dataTableLotes.Rows[0]["ValorInteiraCadeiraNaoSocio"].ToString());
            dValor += Convert.ToDouble(dataTableLotes.Rows[0]["ValorInteiraAvulsoSocio"].ToString());
            dValor += Convert.ToDouble(dataTableLotes.Rows[0]["ValorInteiraAvulsoNaoSocio"].ToString());
            dValor += Convert.ToDouble(dataTableLotes.Rows[0]["ValorMeiaCadeiraSocio"].ToString());
            dValor += Convert.ToDouble(dataTableLotes.Rows[0]["ValorMeiaCadeiraNaoSocio"].ToString());
            dValor += Convert.ToDouble(dataTableLotes.Rows[0]["ValorMeiaAvulsoSocio"].ToString());
            dValor += Convert.ToDouble(dataTableLotes.Rows[0]["ValorMeiaAvulsoNaoSocio"].ToString());

            if (dValor == 0) //Se não colocou nenhum valor para os tipos de ingresso
            {
                sIDFormaPagamento = "9"; //Entrada franca
                CVenda.CarregarRadioButtonListFormasPagamento(radFormaPagamento, sIDFormaPagamento);
                return;
            }
        }

        if (Cliente.Perfil == "Sócio")
        {
            sIDFormaPagamento = "0";

            if (DateTime.Now < Evento.DateLimite1ParcelaCredito
                || DateTime.Now < Evento.DateLimite2ParcelaCredito
                || DateTime.Now < Evento.DateLimite3ParcelaCredito
                || DateTime.Now < Evento.DateLimite4ParcelaCredito
                || DateTime.Now < Evento.DateLimite5ParcelaCredito)
                sIDFormaPagamento = "1";

            if (Cliente.Debito != 1)
            {
                if (DateTime.Now < Evento.DateLimite4ParcelaCota)
                    sIDFormaPagamento += ", 10";
                if (DateTime.Now < Evento.DateLimite3ParcelaCota)
                    sIDFormaPagamento += ", 2";
                if (DateTime.Now < Evento.DateLimite2ParcelaCota)
                    sIDFormaPagamento += ", 3";
                if (DateTime.Now < Evento.DateLimite1ParcelaCota)
                    sIDFormaPagamento += ", 4";
            }

            CVenda.CarregarRadioButtonListFormasPagamento(radFormaPagamento, sIDFormaPagamento);
        }

        if (Cliente.Perfil == "Não Sócio")
        {
            sIDFormaPagamento = "0";

            if (DateTime.Now < Evento.DateLimite1ParcelaCredito
                || DateTime.Now < Evento.DateLimite2ParcelaCredito
                || DateTime.Now < Evento.DateLimite3ParcelaCredito
                || DateTime.Now < Evento.DateLimite4ParcelaCredito
                || DateTime.Now < Evento.DateLimite5ParcelaCredito)
                sIDFormaPagamento = "1";

            CVenda.CarregarRadioButtonListFormasPagamento(radFormaPagamento, sIDFormaPagamento);
        }

        if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
        {
            if (UsuarioLogado.Perfil != "Administrador")
                sIDFormaPagamento = "5,6,7";
            else
                sIDFormaPagamento = "5,6,7,8";

            if (Cliente.Perfil == "Sócio")
            {
                if (Cliente.Debito != 1)
                {
                    if (DateTime.Now < Evento.DateLimite4ParcelaCota)
                        sIDFormaPagamento += ", 10";
                    if (DateTime.Now < Evento.DateLimite3ParcelaCota)
                        sIDFormaPagamento += ", 2";
                    if (DateTime.Now < Evento.DateLimite2ParcelaCota)
                        sIDFormaPagamento += ", 3";
                    if (DateTime.Now < Evento.DateLimite1ParcelaCota)
                        sIDFormaPagamento += ", 4";

                    formaPagamento.Style.Add("color", "black");
                }
                else
                {
                    formaPagamento.InnerText = "Passo 5 - Forma de Pagamento (Pendência Financeira junto ao Clube)";
                    formaPagamento.Style.Add("color", "red");
                }
            }

            CVenda.CarregarRadioButtonListFormasPagamento(radFormaPagamento, sIDFormaPagamento);

            //Detalhes do pagamento
            lblDetalhesPagamento.Visible = true;
            txtDetalhesFormaPagamento.Visible = true;
        }

        if (lblCarrinhoTotal.Text.IndexOf("R$ 0,00") != -1)
            foreach (ListItem item in radFormaPagamento.Items)
                if (item.Text == "Cartão (e-commerce)")
                {
                    radFormaPagamento.Items.Remove(item);
                    break;
                }
    }

    private void HabilitarVagaEstacionamento(bool bHabilitarVagaEstacionamento, int iQuantidadeIngressosTipoCadeira, int iNumIngressoEstacionamento)
    {
        venda Venda = (venda)HttpContext.Current.Session["Venda"];
        vendaCTL CVenda = new vendaCTL();

        evento Evento = new evento();
        eventoCTL CEvento = new eventoCTL();
        Evento = CEvento.RetornarEdicao(Convert.ToInt32(Venda.IDEdicao));

        try
        {
            int iVagasEstacionamentoReservadas = CVenda.RetornarVagasEstacionamentoReservadas(Venda.IDEdicao);

            if (bHabilitarVagaEstacionamento
                && iQuantidadeIngressosTipoCadeira >= iNumIngressoEstacionamento
                && iVagasEstacionamentoReservadas < Evento.VagasEstacionamento)
            {
                lblDesejaEstacionamento.Visible = true;
                dropVagasEstacionamento.Visible = true;

                int iNumeroMaximoVagas = iQuantidadeIngressosTipoCadeira / iNumIngressoEstacionamento;

                dropVagasEstacionamento.Items.Clear();

                for (int i = 0; i <= iNumeroMaximoVagas; i++)
                {
                    dropVagasEstacionamento.Items.Add(new ListItem(i.ToString()));
                    dropVagasEstacionamento.SelectedValue = iNumeroMaximoVagas.ToString();
                }

                if (Venda != null)
                {
                    Venda.NumeroVagas = Convert.ToInt32(dropVagasEstacionamento.SelectedValue);
                    HttpContext.Current.Session["Venda"] = Venda;
                }
            }
            else
            {
                lblDesejaEstacionamento.Visible = false;
                dropVagasEstacionamento.Visible = false;
                dropVagasEstacionamento.SelectedValue = "0";
            }
        }
        catch (Exception ex)
        {
            //lblMensagem.Text = "Erro - " + ex.Message;
        }
    }

    private void ConfigurarCampos(string sEvento)
    {
        divAniversariante.Visible = false;
        radTipoAvulso.Visible = false;
        fieldCriancas.Visible = false;
        lblMensagemMeia.Visible = false;

        if (sEvento == "Boate")
            divAniversariante.Visible = true;
        else
        {
            divAniversariante.Visible = false;
            radTipoAvulso.Visible = true;
            fieldCriancas.Visible = true;
        }

        if (sEvento == "Festa Junina" || sEvento == "Reveillon")
            lblMensagemMeia.Visible = true;
    }

    private void CarregarCarrinhoCompras()
    {
        try
        {
            if (HttpContext.Current.Session["Venda"] != null)
            {
                venda Venda = (venda)HttpContext.Current.Session["Venda"];

                vendaCTL CVenda = new vendaCTL();
                DataSet dataSet = CVenda.RetornarCarrinhoCompras(Venda.IDVenda);

                ConfigurarCampos(Venda.Evento);

                usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

                evento Evento = new evento();
                eventoCTL CEvento = new eventoCTL();
                Evento = CEvento.RetornarEdicao(Convert.ToInt32(Venda.IDEdicao));

                //Mapa
                if (!Convert.ToBoolean(Evento.PossuiMapa))
                    pnlCadeira.Visible = false;

                if (Venda.Aniversariante) chkAniversariante.Checked = Venda.Aniversariante;
                //Verificar se é Aniversariante e gerar a cortesia.
                if (Venda.Evento == "Boate")
                    VerificarAniversariante(dataSet, Evento);

                grdCarrinho.DataSource = dataSet.Tables[0];
                grdCarrinho.DataBind();

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    lblCarrinhoTotal.Text = "<br/>" + dataSet.Tables[0].Rows.Count.ToString() + " ingresso(s)";
                    lblCarrinhoTotal.Text += "<br/>Total: R$ ";
                    lblCarrinhoTotal.Text += dataSet.Tables[1].Rows[0]["Valor"].ToString();
                }
                else
                    lblCarrinhoTotal.Text = "Total: R$ 0,00";

                dropEdicao.SelectedValue = Venda.IDEdicao.ToString();

                //Vagas de estacionamento
                int iQuantidadeIngressosTipoCadeira = Convert.ToInt32(dataSet.Tables[2].Rows[0]["IngressosCadeira"].ToString());
                HabilitarVagaEstacionamento(Convert.ToBoolean(Evento.VagaEstacionamento), iQuantidadeIngressosTipoCadeira, Evento.NumIngressoEstacionamento);

                ExibirResumoInformacoes();
                CarregarFormasPagamento();

                PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
                string sIDEdicao = Criptografia.Criptografar(dropEdicao.SelectedValue, sChave, sVetorInicializacao);

                string sUrl = "";
                if (Evento.Evento == "Festa Junina")
                    sUrl = "../Evento_FestaJunina/index.aspx?id=";
                else if (Evento.Evento == "Reveillon")
                    sUrl = "../Evento_Reveillon/index.aspx?id=";
                else if (Evento.Evento == "Boate")
                    sUrl = "../Evento_Boate/piso_1.aspx?id=";
                else if (Evento.Evento == "Outros")
                    sUrl = "../Evento_Outros/setor_pergula.aspx?id=";

                LinkButtonAbrirMapaInteira.OnClientClick = "AbrirMapa('" + sIDEdicao + "', '" + sUrl + "');";
                LinkButtonAbrirDetalhes.OnClientClick = "ExibirDetalhesEvento('" + sIDEdicao + "');";

                VerificarSaldoIngressos();

                if (Venda.Declaracao) chkDeclaracao.Checked = Venda.Declaracao;

                try
                {
                    dropVagasEstacionamento.SelectedValue = Venda.NumeroVagas.ToString();
                    dropNumeroCriancas.SelectedValue = Venda.NumeroCriancas.ToString();
                }
                catch { }
                                
                CarregarTipoIngressoAvulso();

                //Carregar valores avulsos - só para administrador
                if (UsuarioLogado.Perfil == "Administrador")
                {
                    lblEscolhaValor.Visible = true;
                    dropValorIngresso.Visible = true;
                    CVenda.CarregarDropDownListValoresIngressosAvulsos(dropValorIngresso, Convert.ToInt32(dropEdicao.SelectedValue), Evento.IDEvento, true);

                    cmdAdicionarAvulso.Visible = true;
                    lblAvulso.Visible = true;
                    radTipoAvulso.Visible = true;
                    txtAvulso.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Erro - " + ex.Message;
        }
    }

    private void VerificarAniversariante(DataSet dataSet, evento Evento)
    {
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        clienteCTL CCliente = new clienteCTL();
        cliente Cliente = CCliente.RetornarCliente(UsuarioLogado.IDUsuario);
        vendaCTL CVenda = new vendaCTL();

        //int iMesAtual = DateTime.Now.Month;
        //int iMesAtual = 2;
        //int iMesCliente = Convert.ToInt32(Cliente.DataNascimento.Substring(3, 2));
        int iQteIngressosComprados = dataSet.Tables[0].Rows.Count;
        int iQteCortersiasAdquiridos = 0;
        List<int> IngressosAniversariante = new List<int>();

        foreach (DataRow dtRowIngressos in dataSet.Tables[0].Rows)
        {
            if (dtRowIngressos["TipoIngresso"].ToString() == "Cortesia não sócio Aniversariante" || dtRowIngressos["TipoIngresso"].ToString() == "Cortesia sócio Aniversariante")
            {
                IngressosAniversariante.Add(Convert.ToInt32(dtRowIngressos["Ticket"].ToString()));
                iQteCortersiasAdquiridos++;
            }
        }

        iQteIngressosComprados -= iQteCortersiasAdquiridos;
        int iQteExistente = iQteCortersiasAdquiridos * Evento.MinimoAniversariante;

        //retira o ingresso cortesia Aniversariante caso tenha menos do que o minimo Aniversariante.
        for (int i = 0; i < iQteIngressosComprados; i++)
        {
            if (iQteExistente > iQteIngressosComprados)
            {
                CVenda.CancelarTicket(IngressosAniversariante[i], "Ticket " + IngressosAniversariante[i], Cliente.IDUsuario);
                iQteExistente -= Evento.MinimoAniversariante;
                iQteCortersiasAdquiridos--;
            }
        }

        int iNovosIngressos = iQteIngressosComprados - iQteExistente;

        //para verificar se o cliente selecionou aniversariante.
        if (chkAniversariante.Checked == true && Evento.MinimoAniversariante != 0)
        {   // percorre a quantidade de ingressos e adiciona os de cortesia para o Aniversariante.
            while (iNovosIngressos >= Evento.MinimoAniversariante)
            {
                if (iNovosIngressos % Evento.MinimoAniversariante == 0)
                {
                    GerarIngressoAniversariante();
                    iNovosIngressos -= Evento.MinimoAniversariante;
                }
            }
        }
    }

    private void GerarIngressoAniversariante()
    {
        venda Venda = (venda)HttpContext.Current.Session["Venda"];

        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
        if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
            Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

        venda IngressoAvulso = (venda)HttpContext.Current.Session["Venda"];
        vendaCTL CVenda = new vendaCTL();

        /////////////////////////////////////////
        evento Evento = new evento();
        eventoCTL CEvento = new eventoCTL();
        Evento = CEvento.RetornarEdicao(Venda.IDEdicao);

        if (Evento.Evento == "Boate")
        {
            if (Cliente.Perfil == "Sócio")
            {
                IngressoAvulso.Valor = 0;
                IngressoAvulso.IDTipoIngresso = 13;
            }
            else if (Cliente.Perfil == "Não Sócio")
            {
                IngressoAvulso.Valor = 0;
                IngressoAvulso.IDTipoIngresso = 14;
            }
        }

        CVenda.CadastrarVendaAvulso(IngressoAvulso);
    }

    private void CarregarDadosUsuario()
    {
        try
        {
            usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

            usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
            if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
                Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

            //Reserva
            if (UsuarioLogado.Perfil == "Administrador")
                cmdReservar.Visible = true;

            if (Cliente.Perfil == "Sócio")
                lblNome_valor.Text = Cliente.Nome.ToUpper() + " (" + Cliente.Perfil + " - cota " + Cliente.NumeroCota.ToString() + "-" + Cliente.DigitoCota.ToString() + ")";
            else
                lblNome_valor.Text = Cliente.Nome.ToUpper() + " (" + Cliente.Perfil + ")";

            lblEmail_valor.Text = Cliente.Email.ToLower();
            lblCPF_valor.Text = Cliente.CPF.ToUpper();
        }
        catch { }
    }

    private void CarregarEventosEdicao()
    {
        try
        {
            eventoCTL evento = new eventoCTL();

            usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

            if (UsuarioLogado.Perfil == "Administrador")
                evento.CarregarDropDownListEventosVendaAdministrador(dropEdicao, false, true);
            else
                evento.CarregarDropDownListEventosVenda(dropEdicao, false, true);

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

    private void IniciarVenda()
    {
        try
        {
            HttpContext.Current.Session["Venda"] = null;

            if (dropEdicao.SelectedValue != "-1")
            {
                vendaCTL CVenda = new vendaCTL();

                usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

                usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
                if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
                    Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

                //Compras em andamento simultâneas
                if (ConfigurationManager.AppSettings["LimitarComprasSimultaneas"].ToString() == "Sim")
                {
                    if (UsuarioLogado.Perfil != "Vendedor" && UsuarioLogado.Perfil != "Administrador")
                    {
                        DataSet dataSet = CVenda.RetornarComprasEmAndamento(Convert.ToInt32(dropEdicao.SelectedValue));
                        int iComprasEmAndamento = Convert.ToInt32(dataSet.Tables[0].Rows[0][0].ToString());
                        int iComprasSimultaneasPermitidas = Convert.ToInt32(dataSet.Tables[1].Rows[0][0].ToString());

                        if (iComprasEmAndamento >= iComprasSimultaneasPermitidas)
                        {
                            if (HttpContext.Current.Session["Venda"] != null)
                            {
                                venda VendaAtual = (venda)HttpContext.Current.Session["Venda"];
                                CVenda.LiberarIngressos(VendaAtual.IDCliente, VendaAtual.IDEdicao, -1);
                            }

                            HttpContext.Current.Session["Venda"] = null;
                            dropEdicao.SelectedValue = "-1";

                            PontoBr.Utilidades.Diversos.ExibirAlertaERedirecionarScriptManager("Default.aspx", this.Page, "Fila de compra excedida! Tente daqui a pouco.");
                        }
                    }
                }

                //Liberar ingressos reservados
                CVenda.LiberarIngressos(Cliente.IDCliente, Convert.ToInt32(dropEdicao.SelectedValue), -1);

                evento Evento = new evento();
                eventoCTL CEvento = new eventoCTL();
                Evento = CEvento.RetornarEdicao(Convert.ToInt32(dropEdicao.SelectedValue));

                //Camarote
                //if (Evento.Evento == "Festa Junina")
                //{
                //    if (Cliente.Perfil == "Sócio")
                //        radTipoAvulso.Items.Add("Camarote sócio");
                //    else
                //        radTipoAvulso.Items.Add("Camarote não sócio");
                //}

                //Verifica se há evento disponível e se o não sócio pode comprar
                if (dropEdicao.Items.Count > 1)
                {
                    if (Cliente.Perfil == "Não Sócio" && UsuarioLogado.Perfil != "Administrador")
                    {
                        DateTime dataHoraInicioVendaNaoSocio = PontoBr.Conversoes.Data.ConverterDataBancoParaDateTime(Evento.InicioVendaNaoSocio);
                        if (DateTime.Now < dataHoraInicioVendaNaoSocio)
                        {
                            string sMensagem = "As compras estão abertas apenas para sócio.\\n\\n";
                            sMensagem += "Você poderá comprar a partir de " + dataHoraInicioVendaNaoSocio.ToString("dd/MM/yyyy HH:mm") + " h.";

                            LinkButtonAbrirMapaInteira.OnClientClick = "";
                            LinkButtonAbrirMapaInteira.Enabled = false;
                            cmdAdicionarAvulso.Enabled = false;

                            dropEdicao.SelectedValue = "-1";

                            ExibirMensagem(sMensagem);
                            return;
                        }
                    }
                }

                //Mapa
                if (!Convert.ToBoolean(Evento.PossuiMapa))
                    pnlCadeira.Visible = false;

                CarregarFormasPagamento();

                PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
                string sIDEdicao = Criptografia.Criptografar(dropEdicao.SelectedValue, sChave, sVetorInicializacao);

                ConfigurarCampos(Evento.Evento);

                string sUrl = "";
                if (Evento.Evento == "Festa Junina")
                    sUrl = "../Evento_FestaJunina/index.aspx?id=";
                else if (Evento.Evento == "Reveillon")
                    sUrl = "../Evento_Reveillon/index.aspx?id=";
                else if (Evento.Evento == "Boate")
                    sUrl = "../Evento_Boate/piso_1.aspx?id=";
                else if (Evento.Evento == "Outros")
                    sUrl = "../Evento_Outros/setor_pergula.aspx?id=";

                LinkButtonAbrirMapaInteira.OnClientClick = "AbrirMapa('" + sIDEdicao + "', '" + sUrl + "');";
                LinkButtonAbrirDetalhes.OnClientClick = "ExibirDetalhesEvento('" + sIDEdicao + "');";

                /////////////////////////////////////////////////////////////////////////////////////////////////////

                venda Venda = new venda();
                Venda.IDEdicao = Convert.ToInt32(dropEdicao.SelectedValue);
                Venda.Evento = Evento.Evento;

                Venda.IDCliente = Cliente.IDCliente;
                Venda.IDUsuario = Cliente.IDUsuario;
                Venda.Nome = Cliente.Nome;
                Venda.CPF = Cliente.CPF;

                Venda.IDUsuarioCadastro = UsuarioLogado.IDUsuario;
                Venda.IP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

                Venda.NumeroIngressoExtraSocioCota = Evento.NumeroIngressoExtraSocioCota;
                Venda.NumeroIngressoExtraSocioTitulo = Evento.NumeroIngressoExtraSocioTitulo;
                Venda.NumeroIngressosValorNaoSocio = Evento.NumeroIngressosValorNaoSocio;
                Venda.NumeroIngressosCadeira = Evento.NumeroIngressosCadeira;

                DataTable dataTable = null;
                dataTable = CEvento.RetornarLotes(Convert.ToInt32(dropEdicao.SelectedValue), true);
                if (UsuarioLogado.Perfil == "Administrador")
                    dataTable = CEvento.RetornarLotes(Convert.ToInt32(dropEdicao.SelectedValue), false);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    DateTime dateInicioVendas = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataRow["Início Venda"].ToString());
                    DateTime dateFimVendas = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataRow["Fim Venda"].ToString());

                    if ((DateTime.Now > dateInicioVendas
                        && DateTime.Now < dateFimVendas)
                        || UsuarioLogado.Perfil == "Administrador")
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

                        Venda.ValorIpanema = Convert.ToDouble(dataRow["ValorIpanema"].ToString());
                        Venda.ValorGolodromo = Convert.ToDouble(dataRow["ValorGolodromo"].ToString());
                        Venda.ValorPortinari = Convert.ToDouble(dataRow["ValorPortinari"].ToString());
                        Venda.ValorPergula = Convert.ToDouble(dataRow["ValorPergula"].ToString());
                        Venda.ValorSalaoDeFestas = Convert.ToDouble(dataRow["ValorSalaoDeFestas"].ToString());
                        Venda.ValorAcademia = Convert.ToDouble(dataRow["ValorAcademia"].ToString());

                        Venda.ValorCamaroteSocio = Convert.ToDouble(dataRow["ValorCamaroteSocio"].ToString());
                        Venda.ValorCamaroteNaoSocio = Convert.ToDouble(dataRow["ValorCamaroteNaoSocio"].ToString());
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

                        Venda.ValorIpanema = Convert.ToDouble(dataRow["ValorIpanema"].ToString());
                        Venda.ValorGolodromo = Convert.ToDouble(dataRow["ValorGolodromo"].ToString());
                        Venda.ValorPortinari = Convert.ToDouble(dataRow["ValorPortinari"].ToString());
                        Venda.ValorPergula = Convert.ToDouble(dataRow["ValorPergula"].ToString());
                        Venda.ValorSalaoDeFestas = Convert.ToDouble(dataRow["ValorSalaoDeFestas"].ToString());
                        Venda.ValorAcademia = Convert.ToDouble(dataRow["ValorAcademia"].ToString());

                        if (Convert.ToBoolean(dataRow["CamaroteSocio"])) Venda.ValorCamaroteSocio = Convert.ToDouble(dataRow["ValorCamaroteSocio"].ToString());
                        if (Convert.ToBoolean(dataRow["CamaroteNaoSocio"])) Venda.ValorCamaroteNaoSocio = Convert.ToDouble(dataRow["ValorCamaroteNaoSocio"].ToString());
                    }
                }

                Venda.IDVenda = CVenda.CadastrarVenda(Venda);
                HttpContext.Current.Session["Venda"] = Venda;

                //Resumo de informações
                ExibirResumoInformacoes();
            VerificarSaldoIngressos();
                CarregarTipoIngressoAvulso();

                //Carregar valores avulsos - só para administrador
                if (UsuarioLogado.Perfil == "Administrador")
                {
                    lblEscolhaValor.Visible = true;
                    dropValorIngresso.Visible = true;
                    CVenda.CarregarDropDownListValoresIngressosAvulsos(dropValorIngresso, Convert.ToInt32(dropEdicao.SelectedValue), Evento.IDEvento, true);

                    cmdAdicionarAvulso.Visible = true;
                    lblAvulso.Visible = true;
                    radTipoAvulso.Visible = true;
                    txtAvulso.Visible = true;
                }
            }
            else
            {
                LinkButtonAbrirDetalhes.OnClientClick = "alert('Selecione um evento antes de abrir o mapa (Passo 2).')";
                LinkButtonAbrirMapaInteira.OnClientClick = "alert('Selecione um evento antes de abrir o mapa (Passo 2).')";
            }            
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Erro - " + ex.Message;
        }
    }

    protected void dropEdicao_SelectedIndexChanged(object sender, EventArgs e)
    {
        IniciarVenda();
    }

    private void ExibirResumoInformacoes()
    {
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
        if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
            Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

        venda Venda = (venda)HttpContext.Current.Session["Venda"];

        vendaCTL CVenda = new vendaCTL();

        saldo Saldo = new saldo();
        Saldo = CVenda.RetornarSaldoIngressos(Venda.IDEdicao, Cliente.NumeroCota, Cliente.IDCliente, Venda.IDVenda);

        lblResumo.Text = String.Empty;
        if (Cliente.Perfil == "Sócio")
        {
            if (Cliente.NumeroCota < 10000) /*cota*/
            {
                Venda.IngressosPrecoSocio = Cliente.Dependentes + Venda.NumeroIngressoExtraSocioCota;
                Venda.IngressosPrecoNaoSocio = Venda.NumeroIngressosValorNaoSocio;

                lblResumo.Text += "<br/>Ingressos disponíveis para compra a preço de sócio: " + (Cliente.Dependentes + Venda.NumeroIngressoExtraSocioCota).ToString() + "";
                lblResumo.Text += "<br/>Ingressos disponíveis para compra a preço de não sócio: " + Venda.NumeroIngressosValorNaoSocio.ToString() + "";
                lblResumo.Text += "<br/>Total que poderá comprar: " + (Cliente.Dependentes + Venda.NumeroIngressoExtraSocioCota + Venda.NumeroIngressosValorNaoSocio).ToString() + " ingressos";
                lblResumo.Text += "<br/>Ingressos já comprados: " + Saldo.QuantidadeComprado.ToString();
                lblResumo.Text += "<br/>Saldo (ingressos tipo sócio): " + (Venda.IngressosPrecoSocio - Saldo.QuantidadeValorSocioComprado).ToString();
                lblResumo.Text += "<br/>Saldo (ingressos tipo não sócio): " + (Venda.IngressosPrecoNaoSocio - Saldo.QuantidadeValorNaoSocioComprado).ToString();
                lblResumo.Text += "<br/><spam style='font-style: italic; color:blue;'>Número total de ingressos disponíveis em mesa por cota/título: " + Venda.NumeroIngressosCadeira.ToString() + "</spam>";
            }
            else
            {
                Venda.IngressosPrecoSocio = Cliente.Dependentes + Venda.NumeroIngressoExtraSocioTitulo;
                Venda.IngressosPrecoNaoSocio = Venda.NumeroIngressosValorNaoSocio;

                lblResumo.Text += "<br/>Ingressos disponíveis para compra a preço de sócio: " + (Cliente.Dependentes + Venda.NumeroIngressoExtraSocioTitulo).ToString() + "";
                lblResumo.Text += "<br/>Ingressos disponíveis para compra a preço de não sócio: " + Venda.NumeroIngressosValorNaoSocio.ToString() + "";
                lblResumo.Text += "<br/>Total que poderá comprar: " + (Cliente.Dependentes + Venda.NumeroIngressoExtraSocioTitulo + Venda.NumeroIngressosValorNaoSocio).ToString() + " ingressos";
                lblResumo.Text += "<br/>Ingressos já comprados: " + Saldo.QuantidadeComprado.ToString();
                lblResumo.Text += "<br/>Saldo (ingressos tipo sócio): " + (Venda.IngressosPrecoSocio - Saldo.QuantidadeValorSocioComprado).ToString();
                lblResumo.Text += "<br/>Saldo (ingressos tipo não sócio): " + (Venda.IngressosPrecoNaoSocio - Saldo.QuantidadeValorNaoSocioComprado).ToString();
                lblResumo.Text += "<br/><spam style='font-style: italic; color:blue;'>Número total de ingressos disponíveis em mesa por cota/título: " + Venda.NumeroIngressosCadeira.ToString() + "</spam>";
            }
        }
        else
        {
            Venda.IngressosPrecoSocio = 0;
            Venda.IngressosPrecoNaoSocio = Venda.NumeroIngressosValorNaoSocio;

            lblResumo.Text += "<br/>Ingressos disponíveis para compra a preço de não sócio: " + Venda.NumeroIngressosValorNaoSocio.ToString() + "";
            lblResumo.Text += "<br/>Total que poderá comprar: " + Venda.NumeroIngressosValorNaoSocio.ToString() + " ingressos";
            lblResumo.Text += "<br/>Ingressos já comprados: " + Saldo.QuantidadeComprado.ToString();
            lblResumo.Text += "<br/>Saldo (ingressos tipo não sócio): " + (Venda.IngressosPrecoNaoSocio - Saldo.QuantidadeValorNaoSocioComprado).ToString();
            lblResumo.Text += "<br/><spam style='font-style: italic; color:blue;'>Número total de ingressos disponíveis em mesa por cota/título: " + Venda.NumeroIngressosCadeira.ToString() + "</spam>";
        }

        //Teste do sistema
        if (ConfigurationManager.AppSettings["ExibirResumoPrecos"].ToString() == "Sim")
        {
            lblResumo.Text += "<br/><span style='color:red'>";
            lblResumo.Text += "<br/>*************************************Testes************************************";
            lblResumo.Text += "<br/>Lote: " + Venda.Lote;
            lblResumo.Text += "<br/>Tipo de ingresso [Inteira Sócio (cadeira)]: R$ " + Venda.ValorInteiraCadeiraSocio.ToString("0.00");
            lblResumo.Text += "<br/>Tipo de ingresso [Inteira Não sócio (cadeira)]: R$ " + Venda.ValorInteiraCadeiraNaoSocio.ToString("0.00");
            lblResumo.Text += "<br/>Tipo de ingresso [Inteira Sócio (sem cadeira)]: R$ " + Venda.ValorInteiraAvulsoSocio.ToString("0.00");
            lblResumo.Text += "<br/>Tipo de ingresso [Inteira Não sócio (ingresso sem cadeira)]: R$ " + Venda.ValorInteiraAvulsoNaoSocio.ToString("0.00");
            lblResumo.Text += "<br/>";
            lblResumo.Text += "<br/>Tipo de ingresso [Meio Adolescente Sócio (cadeira)]: R$ " + Venda.ValorMeiaCadeiraSocio.ToString("0.00");
            lblResumo.Text += "<br/>Tipo de ingresso [Meio Adolescente Não sócio (cadeira)]: R$ " + Venda.ValorMeiaCadeiraNaoSocio.ToString("0.00");
            lblResumo.Text += "<br/>Tipo de ingresso [Meio Adolescente Sócio (ingresso sem cadeira)]: R$ " + Venda.ValorMeiaAvulsoSocio.ToString("0.00");
            lblResumo.Text += "<br/>Tipo de ingresso [Meio Adolescente Não sócio (ingresso sem cadeira)]: R$ " + Venda.ValorMeiaAvulsoNaoSocio.ToString("0.00");
            lblResumo.Text += "<br/>*************************************Testes************************************";
            lblResumo.Text += "</span>";
        }

        HttpContext.Current.Session["Venda"] = Venda;
    }

    private void AdicionarIngressoAvulso()
    {
        venda Venda = (venda)HttpContext.Current.Session["Venda"];

        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
        if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
            Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

        venda IngressoAvulso = (venda)HttpContext.Current.Session["Venda"];
        vendaCTL CVenda = new vendaCTL();

        /////////////////////////////////////////
        evento Evento = new evento();
        eventoCTL CEvento = new eventoCTL();
        Evento = CEvento.RetornarEdicao(Venda.IDEdicao);

        int iIngressosPodeComprarValorSocio = 0;

        if (Cliente.Perfil == "Sócio")
        {
            if (Cliente.NumeroCota < 10000) /*Cota*/
                iIngressosPodeComprarValorSocio = Cliente.Dependentes + Evento.NumeroIngressoExtraSocioCota;
            else /*Título*/
                iIngressosPodeComprarValorSocio = Cliente.Dependentes + Evento.NumeroIngressoExtraSocioTitulo;
        }

        int iQuantidadeIngressos = Convert.ToInt32(txtAvulso.Text);

        for (int i = 0; i < iQuantidadeIngressos; i++)
        {
            saldo Saldo = new saldo();
            Saldo = CVenda.RetornarSaldoIngressos(Venda.IDEdicao, Cliente.NumeroCota, Cliente.IDCliente, Venda.IDVenda);

            if (Evento.Evento != "Boate")
            {
                if (Cliente.Perfil == "Sócio")
                {
                    if (radTipoAvulso.SelectedValue == "Não sócio Inteiro sem cadeira")
                    {
                        IngressoAvulso.Valor = IngressoAvulso.ValorInteiraAvulsoNaoSocio;
                        IngressoAvulso.IDTipoIngresso = 4;
                    }
                    else if (radTipoAvulso.SelectedValue == "Não sócio Meio adolescente sem cadeira")
                    {
                        IngressoAvulso.Valor = IngressoAvulso.ValorMeiaAvulsoNaoSocio;
                        IngressoAvulso.IDTipoIngresso = 8;
                    }
                    else if (radTipoAvulso.SelectedValue == "Inteiro sem cadeira")
                    {
                        if ((Saldo.QuantidadeValorSocioComprado + Saldo.QuantidadeValorSocioVendaAtual) >= iIngressosPodeComprarValorSocio) //Valor de não sócio
                        {
                            IngressoAvulso.Valor = IngressoAvulso.ValorInteiraAvulsoNaoSocio;
                            IngressoAvulso.IDTipoIngresso = 4;
                        }
                        else
                        {
                            IngressoAvulso.Valor = IngressoAvulso.ValorInteiraAvulsoSocio;
                            IngressoAvulso.IDTipoIngresso = 3;
                        }
                    }
                    else if (radTipoAvulso.SelectedValue == "Meio adolescente sem cadeira")
                    {
                        if ((Saldo.QuantidadeValorSocioComprado + Saldo.QuantidadeValorSocioVendaAtual) >= iIngressosPodeComprarValorSocio) //Valor de não sócio
                        {
                            IngressoAvulso.Valor = IngressoAvulso.ValorMeiaAvulsoNaoSocio;
                            IngressoAvulso.IDTipoIngresso = 8;
                        }
                        else
                        {
                            IngressoAvulso.Valor = IngressoAvulso.ValorMeiaAvulsoSocio;
                            IngressoAvulso.IDTipoIngresso = 7;
                        }
                    }
                    else if (radTipoAvulso.SelectedValue == "Camarote sócio")
                    {
                        IngressoAvulso.Valor = IngressoAvulso.ValorCamaroteSocio;
                        IngressoAvulso.IDTipoIngresso = 18;
                    }
                }
                else if (Cliente.Perfil == "Não Sócio")
                {
                    if (radTipoAvulso.SelectedValue == "Não sócio Inteiro sem cadeira")
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

                if (UsuarioLogado.Perfil == "Administrador"
                    && dropValorIngresso.SelectedValue != "-1"
                     && dropValorIngresso.Visible == true)
                {
                    IngressoAvulso.Valor = Convert.ToDouble(dropValorIngresso.SelectedValue);

                    if (dropValorIngresso.SelectedItem.ToString().IndexOf("Inteira Não Sócio") > -1)
                        IngressoAvulso.IDTipoIngresso = 4;
                    else if (dropValorIngresso.SelectedItem.ToString().IndexOf("Inteira Sócio") > -1)
                        IngressoAvulso.IDTipoIngresso = 3;
                    else if (dropValorIngresso.SelectedItem.ToString().IndexOf("Meia Não Sócio") > -1)
                        IngressoAvulso.IDTipoIngresso = 8;
                    else if (dropValorIngresso.SelectedItem.ToString().IndexOf("Meia Sócio") > -1)
                        IngressoAvulso.IDTipoIngresso = 7;
                }
                else if (radTipoAvulso.SelectedValue == "Camarote não sócio")
                {
                    IngressoAvulso.Valor = IngressoAvulso.ValorCamaroteNaoSocio;
                    IngressoAvulso.IDTipoIngresso = 19;
                }
            }

            //QUANDO FOR BOATE
            else if (Evento.Evento == "Boate")
            {
                if (Cliente.Perfil == "Sócio")
                {
                    // sempre inteiro
                    if (((Saldo.QuantidadeValorSocioComprado + Saldo.QuantidadeValorSocioVendaAtual) >= iIngressosPodeComprarValorSocio)
                        || radTipoAvulso.SelectedValue == "Não sócio Inteiro sem cadeira")
                    {
                        IngressoAvulso.Valor = IngressoAvulso.ValorInteiraAvulsoNaoSocio; //Valor de não sócio
                        IngressoAvulso.IDTipoIngresso = 10;
                    }
                    else
                    {
                        IngressoAvulso.Valor = IngressoAvulso.ValorInteiraAvulsoSocio;
                        IngressoAvulso.IDTipoIngresso = 9;
                    }
                }
                else if (Cliente.Perfil == "Não Sócio")
                {   // sempre inteiro
                    IngressoAvulso.Valor = IngressoAvulso.ValorInteiraAvulsoNaoSocio;
                    IngressoAvulso.IDTipoIngresso = 10;
                }

                if (UsuarioLogado.Perfil == "Administrador"
                    && dropValorIngresso.SelectedValue != "-1"
                     && dropValorIngresso.Visible == true)
                {
                    IngressoAvulso.Valor = Convert.ToDouble(dropValorIngresso.SelectedValue);

                    if (dropValorIngresso.SelectedItem.ToString().IndexOf("Inteira Não Sócio") > -1) 
                        IngressoAvulso.IDTipoIngresso = 4;
                    else if (dropValorIngresso.SelectedItem.ToString().IndexOf("Inteira Sócio") > -1)
                        IngressoAvulso.IDTipoIngresso = 3;
                    else if (dropValorIngresso.SelectedItem.ToString().IndexOf("Meia Não Sócio") > -1)
                        IngressoAvulso.IDTipoIngresso = 8;
                    else if (dropValorIngresso.SelectedItem.ToString().IndexOf("Meia Sócio") > -1)
                        IngressoAvulso.IDTipoIngresso = 7;
                }
            }// FIM QUANDO FOR BOATE PIC

            CVenda.CadastrarVendaAvulso(IngressoAvulso);
        }

        txtAvulso.Text = String.Empty;
        CarregarCarrinhoCompras();
    }

    protected void cmdAdicionarAvulso_Click(object sender, EventArgs e)
    {
        if (PodeAdicionarAvulso())
        {
            AdicionarIngressoAvulso();
        }
    }

    private void ExibirMensagem(string sMensagem)
    {
        sMensagem = sMensagem.Replace("'", "");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + sMensagem + "')", true);
    }

    protected void grdCarrinho_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sTipoIngresso = grdCarrinho.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
        if (e.CommandName == "Excluir")
        {
            if (sTipoIngresso != "Cortesia n&#227;o s&#243;cio Aniversariante" &&
                sTipoIngresso != "Cortesia s&#243;cio Aniversariante")
            {
                usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

                usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
                if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
                    Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

                vendaCTL CVenda = new vendaCTL();

                double dTicket = Convert.ToDouble(grdCarrinho.DataKeys[int.Parse((string)e.CommandArgument)]["Ticket"].ToString());
                string sTextoLog = "Ticket " + dTicket.ToString();
                sTextoLog += "; " + Server.HtmlDecode(grdCarrinho.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text);

                if (HttpContext.Current.Session["Venda"] != null)
                {
                    venda Venda = (venda)HttpContext.Current.Session["Venda"];
                    sTextoLog += "; Voucher " + Venda.IDVenda.ToString();
                }

                //Implementar aqui...

                CVenda.CancelarTicket(dTicket, sTextoLog, Cliente.IDUsuario);
                CarregarCarrinhoCompras();
            }
        }
    }

    private void CadastrarEstacionamento(venda Venda)
    {
        //Vagas de estacionamento
        vendaCTL CVenda = new vendaCTL();
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        for (int iNumeroVaga = 1; iNumeroVaga <= Venda.NumeroVagas; iNumeroVaga++)
        {
            string sIDIdentidadeEletronica = "EST" + iNumeroVaga.ToString() + Venda.IDVenda.ToString() + DateTime.Now.Ticks.ToString();
            sIDIdentidadeEletronica = sIDIdentidadeEletronica.Substring(0, 18);
            CVenda.CadastrarIngressoEstacionamento(Venda.IDVenda, Venda.IDEdicao, 0, sIDIdentidadeEletronica, Venda.Nome, Venda.CPF, UsuarioLogado.IDUsuario);
        }
    }

    protected void cmdFinalizar_Click(object sender, EventArgs e)
    {
        try
        {
            if (PodeFinalizar(false))
            {
                venda Venda = (venda)HttpContext.Current.Session["Venda"];
                usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

                PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
                string sIDVenda = Criptografia.Criptografar(Venda.IDVenda.ToString(), sChave, sVetorInicializacao);

                Venda.NumeroVagas = 0;
                try
                {
                    Venda.NumeroVagas = Convert.ToInt32(dropVagasEstacionamento.SelectedValue);
                }
                catch { }

                Venda.Declaracao = Convert.ToBoolean(chkDeclaracao.Checked);
                Venda.Aniversariante = Convert.ToBoolean(chkAniversariante.Checked);
                Venda.NumeroCriancas = Convert.ToInt32(dropNumeroCriancas.SelectedValue);
                Venda.IDLocalRetirada = 1;
                Venda.NomeResponsavelRetirada = "";
                Venda.RgResponsavelRetirada = "";
                Venda.NomeResponsavelRetirada2 = "";
                Venda.RgResponsavelRetirada2 = "";

                HttpContext.Current.Session["Venda"] = Venda;

                if (radFormaPagamento.SelectedValue == "1") //Cartão (e-commerce)
                    ScriptManager.RegisterStartupScript(cmdFinalizar, this.GetType(), DateTime.Now.Ticks.ToString(), "RealizarPagamento('" + sIDVenda + "');", true);
                else
                {
                    vendaCTL CVenda = new vendaCTL();
                    int iIDFormaPagamento = Convert.ToInt32(radFormaPagamento.SelectedValue);
                    int iIDVenda = Venda.IDVenda;
                    int iNumeroCriancas = Convert.ToInt32(dropNumeroCriancas.SelectedValue);
                    string sNomeRetirada = "";
                    string sRgRetirada = "";
                    int iIDLocalRetirada = 1;
                    string sDetalhesFormaPagamento = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtDetalhesFormaPagamento.Text.Trim());
                    string sRetorno = String.Empty;

                    int iNumeroVagas = 0;
                    try
                    {
                        iNumeroVagas = Convert.ToInt32(dropVagasEstacionamento.SelectedValue);
                    }
                    catch { }
                    Venda.NumeroVagas = iNumeroVagas;

                    string sNomeRetirada2 = "";
                    string sRgRetirada2 = "";

                    if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
                    {
                        if (radFormaPagamento.SelectedValue == "8") //Cortesia
                        {
                            CVenda.ConcluirVenda(iIDVenda, iIDFormaPagamento, sDetalhesFormaPagamento, iNumeroVagas,
                            iNumeroCriancas, sNomeRetirada, sRgRetirada, iIDLocalRetirada, sNomeRetirada2, sRgRetirada2);
                            HttpContext.Current.Session["Venda"] = null;

                            CadastrarEstacionamento(Venda);

                            sRetorno =
                            "<h3 style='color:green;'>TRANSAÇÃO APROVADA!!!!!</h3><br/><br/>" +
                            "Imprima seu Comprovante de Compra. Imprima seus ingressos a partir do dia informado nas regras do evento.<br /><br />" +
                            "Número do Pedido: " + Venda.IDVenda.ToString() + "<br />" +
                            "Nome do titular: " + Venda.Nome + "<br />" +
                            "CPF do Titular: " + Venda.CPF + "<br />";
                        }
                        else
                        {
                            CVenda.AtualizarVendaComprovanteCaixa(iIDVenda, iIDFormaPagamento, sDetalhesFormaPagamento, iNumeroVagas,
                                iNumeroCriancas, sNomeRetirada, sRgRetirada, iIDLocalRetirada, sNomeRetirada2, sRgRetirada2);
                            HttpContext.Current.Session["Venda"] = null;

                            sRetorno =
                            "<h3 style='color:green;'>RECIBO GERADO!!!!!</h3><br/><br/>" +
                            "Imprima o recibo e entregue ao cliente para pagamento. Depois de pago, aprove a compra no menu Cadastro/Aprovar Venda.<br /><br />" +
                            "Número do Pedido: " + Venda.IDVenda.ToString() + "<br />" +
                            "Nome do titular: " + Venda.Nome + "<br />" +
                            "CPF do Titular: " + Venda.CPF + "<br />";
                        }
                    }
                    else
                    {
                        CVenda.ConcluirVenda(iIDVenda, iIDFormaPagamento, sDetalhesFormaPagamento, iNumeroVagas,
                            iNumeroCriancas, sNomeRetirada, sRgRetirada, iIDLocalRetirada, sNomeRetirada2, sRgRetirada2);

                        ExibirResumoInformacoes();
                        HttpContext.Current.Session["Venda"] = null;

                        CadastrarEstacionamento(Venda);

                        sRetorno =
                        "<h3 style='color:green;'>TRANSAÇÃO APROVADA!!!!!</h3><br/><br/>" +
                        "Imprima seu Comprovante de Compra. Imprima seus ingressos a partir do dia informado nas regras do evento.<br /><br />" +
                        "Número do Pedido: " + Venda.IDVenda.ToString() + "<br />" +
                        "Nome do titular: " + Venda.Nome + "<br />" +
                        "CPF do Titular: " + Venda.CPF + "<br />";
                    }

                    ExibirMensagemConclusaoCompra(iIDVenda, true, sRetorno);
                    cmdCancelar.Text = "Voltar";
                }
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Erro: " + ex.Message;
        }
    }

    private bool PodeAdicionarAvulso()
    {
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        if (dropEdicao.SelectedValue == "-1")
        {
            ExibirMensagem("Selecione algum Evento.");
            return false;
        }

        if (UsuarioLogado.Perfil != "Administrador")
            if (radTipoAvulso.SelectedValue == "")
            {
                ExibirMensagem("Selecione o tipo de ingresso sem cadeira.");
                return false;
            }

        if (String.IsNullOrEmpty(txtAvulso.Text))
        {
            ExibirMensagem("Informe a quantidade de ingressos.");
            return false;
        }
        if (HttpContext.Current.Session["Venda"] == null)
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaERedirecionarScriptManager("Default.aspx", this.Page, "Sua sessão expirou ou a venda foi concluída! Caso deseja, você deve reiniciar o processo de compra.");
            return false;
        }

        //Limites de ingressos////////////////////////////////////////////////////////////
        usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
        if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
            Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

        venda Venda = (venda)HttpContext.Current.Session["Venda"];

        vendaCTL CVenda = new vendaCTL();

        saldo Saldo = new saldo();
        Saldo = CVenda.RetornarSaldoIngressos(Venda.IDEdicao, Cliente.NumeroCota, Cliente.IDCliente, Venda.IDVenda);

        evento Lote = new evento();
        eventoCTL CEvento = new eventoCTL();
        Lote = CEvento.RetornarLote(Venda.IDLote);

        evento Evento = new evento();
        Evento = CEvento.RetornarEdicao(Venda.IDEdicao);

        int iIngressosComprados = Saldo.QuantidadeComprado + Saldo.QuantidadeCarrinhoVendaAtual;
        int iIngressosPodeComprarValorSocio = 0;

        if (Cliente.Perfil == "Sócio")
        {
            if (Cliente.NumeroCota < 10000) /*Cota*/
                iIngressosPodeComprarValorSocio = Cliente.Dependentes + Evento.NumeroIngressoExtraSocioCota;
            else /*Título*/
                iIngressosPodeComprarValorSocio = Cliente.Dependentes + Evento.NumeroIngressoExtraSocioTitulo;
        }

        int iQuantidadeIngressos = Convert.ToInt32(txtAvulso.Text);

        //O Administrador pode comprar ilimitado
        if (UsuarioLogado.Perfil != "Administrador")
        {
            if ((iIngressosComprados + iQuantidadeIngressos) > (iIngressosPodeComprarValorSocio + Evento.NumeroIngressosValorNaoSocio))
            {
                if ((iIngressosPodeComprarValorSocio + Evento.NumeroIngressosValorNaoSocio) - iIngressosComprados > 0)
                {
                    ExibirMensagem("Você só pode adicionar mais " + ((iIngressosPodeComprarValorSocio + Evento.NumeroIngressosValorNaoSocio) - iIngressosComprados) + " ingresso(s) no seu carrinho.");
                    return false;
                }
                else
                {
                    ExibirMensagem("Você não pode adicionar mais ingressos no seu carrinho.");
                    return false;
                }
            }

            //Ingressos já vendidos avulsos        
            int iVendidosAvulso = CVenda.RetornarQuantidadeVendidaAvulso(Convert.ToInt32(dropEdicao.SelectedValue));
            if (iVendidosAvulso >= Lote.IngressosAvulsos)
            {
                ExibirMensagem("Os ingressos do tipo sem cadeira esgotaram para este lote.");
                return false;
            }
            if ((iVendidosAvulso + Saldo.QuantidadeAvulsoVendaAtual + Convert.ToInt32(txtAvulso.Text)) > Lote.IngressosAvulsos)
            {
                ExibirMensagem("Os ingressos estão esgostando. Você só pode comprar " + (Lote.IngressosAvulsos - iVendidosAvulso).ToString() + " ingresso(s) sem cadeira para este lote.");
                return false;
            }

            //Ingressos já vendidos camarote 
            int iVendidosCamarote = CVenda.RetornarQuantidadeVendidaCamarote(Convert.ToInt32(dropEdicao.SelectedValue));
            if (Lote.NumeroIngressosCamarote != 0)
            {
                if (iVendidosCamarote >= Lote.NumeroIngressosCamarote)
                {
                    ExibirMensagem("Os ingressos do tipo camarote esgotaram para este lote.");
                    return false;
                }
                if ((iVendidosCamarote + Saldo.QuantidadeCamaroteVendaAtual + Convert.ToInt32(txtAvulso.Text)) > Lote.NumeroIngressosCamarote)
                {
                    ExibirMensagem("Os ingressos estão esgostando. Você só pode comprar " + (Lote.NumeroIngressosCamarote - iVendidosCamarote).ToString() + " ingresso(s) camarote para este lote.");
                    return false;
                }
            }
        }

        return true;
    }

    private bool PodeFinalizar(bool bReserva)
    {
        if (PontoBr.Utilidades.String.ValidarCPF(lblCPF_valor.Text) == false)
        {
            ExibirMensagem("CPF do cliente inválido.");
            return false;
        }
        if (dropEdicao.SelectedValue == "-1")
        {
            ExibirMensagem("Selecione algum Evento.");
            return false;
        }
        if (HttpContext.Current.Session["Venda"] == null)
        {
            ExibirMensagem("Sua sessão expirou! Você deve reiniciar o processo de compra.");
            return false;
        }
        if (dropEdicao.SelectedValue == "-1" && grdCarrinho.Rows.Count > 0)
        {
            ExibirMensagem("Sua sessão expirou! Você deve reiniciar o processo de compra.");
            return false;
        }

        chkDeclaracao.ForeColor = System.Drawing.Color.Black;
        if (!chkDeclaracao.Checked)
        {
            ExibirMensagem("Declare que leu e aceitou as normas deste evento.");
            chkDeclaracao.Focus();
            chkDeclaracao.ForeColor = System.Drawing.Color.Red;
            return false;
        }

        if (grdCarrinho.Rows.Count == 0)
        {
            ExibirMensagem("Escolha os ingressos (cadeira ou sem cadeira) e adicione no carrinho de compras.");
            return false;
        }

        venda Venda = (venda)HttpContext.Current.Session["Venda"];
        vendaCTL CVenda = new vendaCTL();

        int iNumeroMinimoReserva = 0;
        if (Venda.Evento == "Reveillon" || Venda.Evento == "Boate")
        {
            iNumeroMinimoReserva = 4;
            DataTable dataMesasSemReservaCompleta = CVenda.RetornarMesasSemReservaCompleta(Venda.IDVenda, iNumeroMinimoReserva);

            if (dataMesasSemReservaCompleta.Rows.Count > 0)
            {
                string sMensagemMesas = "";

                foreach (DataRow dataRow in dataMesasSemReservaCompleta.Rows)
                {
                    if (!String.IsNullOrEmpty(sMensagemMesas)) sMensagemMesas += ", ";
                    sMensagemMesas += dataRow["Mesa"].ToString();
                }

                sMensagemMesas = "Há cadeira(s) não selecionada(s) na(s) mesa(s): " + sMensagemMesas + ".\\n\\nA mesa é vendida fechada. É necessário selecionar todas as cadeiras da mesa.";

                ExibirMensagem(sMensagemMesas);
                return false;
            }
        }

        if (radFormaPagamento.SelectedValue == "" && !bReserva)
        {
            ExibirMensagem("Selecione a forma de pagamento.");
            return false;
        }

        //Limites de ingressos////////////////////////////////////////////////////////////
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
        if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
            Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

        string sMensagem;
        if (CVenda.VerificarExistenciaVendaEmAndamento(Cliente.IDCliente, Venda.IDVenda))
        {
            ExibirMensagem("Há outra venda em andamento para seu usuário.");
            return false;
        }

        saldo Saldo = new saldo();
        Saldo = CVenda.RetornarSaldoIngressos(Venda.IDEdicao, Cliente.NumeroCota, Cliente.IDCliente, Venda.IDVenda);

        int iNumeroIngressosPodeComprar = 0;
        if (Cliente.Perfil == "Sócio")
        {
            if (Cliente.NumeroCota < 10000) /*cota*/
                iNumeroIngressosPodeComprar = Cliente.Dependentes + Venda.NumeroIngressoExtraSocioCota + Venda.NumeroIngressosValorNaoSocio;
            else /*título*/
                iNumeroIngressosPodeComprar = Cliente.Dependentes + Venda.NumeroIngressoExtraSocioTitulo + Venda.NumeroIngressosValorNaoSocio;
        }
        else if (Cliente.Perfil == "Não Sócio")
            iNumeroIngressosPodeComprar = Venda.NumeroIngressosValorNaoSocio;

        //O Administrador pode comprar ilimitado
        if (UsuarioLogado.Perfil != "Administrador")
        {
            if ((Saldo.QuantidadeCarrinhoVendaAtual + Saldo.QuantidadeComprado) > iNumeroIngressosPodeComprar)
            {
                sMensagem = "No seu carrinho há " + Saldo.QuantidadeCarrinhoVendaAtual.ToString() + " ingresso(s)";

                if (Saldo.QuantidadeComprado > 0)
                {
                    sMensagem = "No seu carrinho há " + Saldo.QuantidadeCarrinhoVendaAtual.ToString() + " ingresso(s)";
                    sMensagem += " e você já comprou " + Saldo.QuantidadeComprado.ToString() + " ingresso(s) em outro momento.";
                }
                else
                    sMensagem = "No seu carrinho há " + Saldo.QuantidadeCarrinhoVendaAtual.ToString() + " ingresso(s).";

                sMensagem += "\\n\\nVocê pode comprar apenas " + iNumeroIngressosPodeComprar.ToString() + " ingresso(s).";

                ExibirMensagem(sMensagem);
                return false;
            }
        }

        if (Cliente.Debito == 1 &&
            (radFormaPagamento.SelectedValue == "2" || radFormaPagamento.SelectedValue == "3" || radFormaPagamento.SelectedValue == "4"))
        {
            ExibirMensagem("Há uma pendência na sua cota do clube.\\n\\nAté que seja regularizada, você poderá comprar apenas com cartão de crédito.");
            return false;
        }

        //Para reservar, só pode ingressos do tipo cadeira
        if (bReserva)
        {
            if (Saldo.QuantidadeAvulsoVendaAtual > 0)
            {
                ExibirMensagem("[Ingressos sem cadeira] não podem ser reservados.");
                return false;
            }
        }

        evento Lote = new evento();
        eventoCTL CEvento = new eventoCTL();
        Lote = CEvento.RetornarLote(Venda.IDLote);

        evento Edicao = new evento();
        Edicao = CEvento.RetornarEdicao(Convert.ToInt32(dropEdicao.SelectedValue));

        //O Administrador pode comprar ilimitado
        if (UsuarioLogado.Perfil != "Administrador")
        {
            //Limite de ingressos tipo cadeira
            if ((Saldo.QuantidadeCadeiraComprado + Saldo.QuantidadeCadeiraVendaAtual) > Edicao.NumeroIngressosCadeira)
            {
                ExibirMensagem("Você só pode comprar " + Edicao.NumeroIngressosCadeira.ToString() + " ingressos do tipo cadeira.");
                return false;
            }
        }

        //Sessão expirou e não tem mais ingressos para esta venda
        if (Saldo.QuantidadeCarrinhoVendaAtual == 0)
        {
            ExibirMensagem("Seu tempo para finalizar a compra expirou.\n\nEscolha outros ingressos (cadeira ou sem cadeira) e adicione no carrinho de compras.");
            CarregarCarrinhoCompras();
            return false;
        }

        //Ingressos já vendidos        
        int iVendidosAvulso = CVenda.RetornarQuantidadeVendidaAvulso(Convert.ToInt32(dropEdicao.SelectedValue));
        //if (iVendidosAvulso >= Lote.IngressosAvulsos)
        //{
        //    ExibirMensagem("Os ingressos do tipo sem cadeira esgotaram para este lote.");
        //    return false;
        //}
        if ((iVendidosAvulso + Saldo.QuantidadeAvulsoVendaAtual) > Lote.IngressosAvulsos)
        {
            ExibirMensagem("Os ingressos estão esgostando. Você só pode comprar " + (Lote.IngressosAvulsos - iVendidosAvulso).ToString() + " ingresso(s) sem cadeira para este lote.");
            return false;
        }

        if (radFormaPagamento.SelectedValue == "5") //Maquineta cartão
        {
            if (String.IsNullOrEmpty(txtDetalhesFormaPagamento.Text.Trim()))
            {
                ExibirMensagem("Detalhe a forma de pagamento.");
                return false;
            }
        }

        return true;
    }

    protected void cmdCancelar_Click(object sender, EventArgs e)
    {
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        if (HttpContext.Current.Session["Venda"] != null)
        {
            venda Venda = (venda)HttpContext.Current.Session["Venda"];

            vendaCTL CVenda = new vendaCTL();
            CVenda.LiberarIngressos(Venda.IDCliente, Venda.IDEdicao, -1);
            HttpContext.Current.Session["Venda"] = null;

            if (UsuarioLogado.Perfil == "Vendedor")
                PontoBr.Utilidades.Diversos.ExibirAlertaERedirecionarScriptManager("../vendedor/default.aspx", this.Page, "Compra cancelada com sucesso!");
            else if (UsuarioLogado.Perfil == "Administrador")
                PontoBr.Utilidades.Diversos.ExibirAlertaERedirecionarScriptManager("../administrador/default.aspx", this.Page, "Compra cancelada com sucesso!");
            else
                PontoBr.Utilidades.Diversos.ExibirAlertaERedirecionarScriptManager("default.aspx", this.Page, "Compra cancelada com sucesso!");
        }
        else
        {
            if (UsuarioLogado.Perfil != "Vendedor")
                Response.Redirect("default.aspx");
            else
                Response.Redirect("~/vendedor/default.aspx");
        }
    }

    private void ConsultarTransacao(string sNumeroDocumento)
    {
        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        sNumeroDocumento = Criptografia.Descriptografar(sNumeroDocumento, sChave, sVetorInicializacao);

        vendaCTL CVenda = new vendaCTL();
        pagamento Pagamento = CVenda.RetornarPagamento(Convert.ToDouble(sNumeroDocumento));

        int iIDVenda = -1;
        if (Pagamento.Aprovada == 1)
        {
            venda Venda = (venda)HttpContext.Current.Session["Venda"];

            Venda = CVenda.RetornarVenda(Venda.IDVenda, "1");

            string sRetorno =
                "<h3 style='color:green; text-decoration: none;'>TRANSAÇÃO APROVADA!!!!!</h3><br/><br/>" +
                "Imprima seu Comprovante de Compra. Imprima seus ingressos a partir do dia informado nas regras do evento.<br /><br />" +
                "Número do Pedido: " + Venda.IDVenda.ToString() + "<br />" +
                "Nome do titular: " + Venda.Nome + "<br />" +
                "CPF do Titular: " + Venda.CPF + "<br />" +
                "Número do Documento: " + sNumeroDocumento + "<br />" +
                "Cartão: " + Pagamento.Bandeira + "<br />" +
                "Valor pago: R$ " + Pagamento.Valor + "<br />" +
                "Número de parcelas: " + Pagamento.Parcelas.ToString() + "<br />" +
                "Data/Hora autorização: " + Pagamento.Cadastro.ToString("dd/MM/yyyy HH:mm:ss") + "<br />" +
                "Código TID: " + Pagamento.tid.ToString();

            //Finalizar Venda
            int iIDFormaPagamento = 1;
            iIDVenda = Venda.IDVenda;
            int iNumeroCriancas = Convert.ToInt32(dropNumeroCriancas.SelectedValue);
            string sNomeRetirada = "";
            string sRgRetirada = "";
            int iIDLocalRetirada = 1;
            string sDetalhesFormaPagamento = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtDetalhesFormaPagamento.Text.Trim());

            int iNumeroVagas = 0;
            try
            {
                iNumeroVagas = Convert.ToInt32(dropVagasEstacionamento.SelectedValue);
            }
            catch { }
            Venda.NumeroVagas = iNumeroVagas;

            string sNomeSegundoRetirada = "";
            string sRgSegundoRetirada = "";

            usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

            CVenda.ConcluirVenda(iIDVenda, iIDFormaPagamento, sDetalhesFormaPagamento, iNumeroVagas,
                iNumeroCriancas, sNomeRetirada, sRgRetirada, iIDLocalRetirada, sNomeSegundoRetirada, sRgSegundoRetirada);

            //Capturar a transação///////////////////////////////////////////////////////
            if (CVenda.VerificarExistenciaVoucher(iIDVenda)) //Só captura (registra no cartão do cliente), se existir voucher
            {
                Cielo.Configuration.IConfiguration customConfiguration = new Cielo.Configuration.CustomConfiguration()
                {
                    DefaultEndpoint = ConfigurationManager.AppSettings["cielo.endpoint.default"],
                    QueryEndpoint = ConfigurationManager.AppSettings["cielo.endpoint.query"],
                    MerchantId = ConfigurationManager.AppSettings["cielo.customer.id"],
                    MerchantKey = ConfigurationManager.AppSettings["cielo.customer.key"],
                    ReturnUrl = ConfigurationManager.AppSettings["cielo.return.url"],
                };

                var cieloService = new Cielo.CieloService(customConfiguration);

                string sPaymentId = Request.QueryString["id"].ToString();
                Guid guidPaymentId = new Guid(sPaymentId);

                var responseCaptura = cieloService.CaptureTransaction(guidPaymentId, 0);
                CVenda.CapturarTransacao(Convert.ToInt32(sNumeroDocumento));
            }
            //Capturar a transação///////////////////////////////////////////////////////

            ExibirResumoInformacoes();

            HttpContext.Current.Session["Venda"] = null;
            
            CadastrarEstacionamento(Venda);

            ExibirMensagemConclusaoCompra(iIDVenda, true, sRetorno);
            cmdCancelar.Text = "Voltar";
        }
        else
        {
            string sRetorno = "";
            sRetorno = "<h3 style='color:red; text-decoration: none;'>TRANSAÇÃO NÃO APROVADA!!!!!</h3><br />";

            if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                radFormaPagamento.ToolTip = Request.QueryString["message"].ToString();

            ExibirMensagemConclusaoCompra(iIDVenda, false, sRetorno);
        }
    }

    private void ExibirMensagemConclusaoCompra(int iIDVenda, bool bVendaAprovada, string sMensagem)
    {
        divRetornoPagamento.Visible = true;
        lblRetorno.Text = sMensagem;

        if (bVendaAprovada)
        {
            PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
            string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
            string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();

            venda Venda = new venda();
            vendaCTL CVenda = new vendaCTL();
            Venda = CVenda.RetornarVenda(iIDVenda, "3,2,5");

            if (Venda.StatusCompra == "Pagamento efetuado")
            {
                //Imprimir Voucher
                LinkButtonImprimirVoucher.NavigateUrl = "../relatorios/voucher.aspx?idvenda=" + Criptografia.Criptografar(iIDVenda.ToString(), sChave, sVetorInicializacao);
                LinkButtonImprimirVoucher.ToolTip = "Número do Pedido " + iIDVenda.ToString();
                LinkButtonImprimirVoucher.Target = "_blank";
                LinkButtonImprimirVoucher.Visible = true;
                LinkButtonImprimirVoucher.Focus();
            }

            usuario UsuarioLogado = (usuario)Session["usuario"];
            if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
            {
                LinkButtonReciboPagamento.Visible = true;
                LinkButtonReciboPagamento.ToolTip = "Número do Pedido " + iIDVenda.ToString();
                LinkButtonReciboPagamento.Attributes.Add("onclick", "AbrirReciboPagamento('" + Criptografia.Criptografar(iIDVenda.ToString(), sChave, sVetorInicializacao) + "');");
                LinkButtonReciboPagamento.Focus();
            }

            cmdFinalizar.Visible = false;
            cmdReservar.Visible = false;
            grdCarrinho.Enabled = false;
            cmdAdicionarAvulso.Enabled = false;
            LinkButtonAbrirMapaInteira.OnClientClick = "alert('Venda finalizada!')";
            dropEdicao.Enabled = false;
            radFormaPagamento.Enabled = false;
            dropNumeroCriancas.Enabled = false;
            chkDeclaracao.Enabled = false;
        }
        else
            lblRetorno.Focus();
    }

    protected void chkDeclaracao_CheckedChanged(object sender, EventArgs e)
    {
        venda Venda = (venda)HttpContext.Current.Session["Venda"];
        if (Venda != null)
        {
            Venda.Declaracao = Convert.ToBoolean(chkDeclaracao.Checked);
            HttpContext.Current.Session["Venda"] = Venda;
        }
    }

    protected void cmdReservar_Click(object sender, EventArgs e)
    {
        try
        {
            if (PodeFinalizar(true))
            {
                venda Venda = (venda)HttpContext.Current.Session["Venda"];
                usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

                PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
                string sIDVenda = Criptografia.Criptografar(Venda.IDVenda.ToString(), sChave, sVetorInicializacao);

                Venda.NumeroVagas = 0;
                try
                {
                    Venda.NumeroVagas = Convert.ToInt32(dropVagasEstacionamento.SelectedValue);
                }
                catch { }

                Venda.Declaracao = Convert.ToBoolean(chkDeclaracao.Checked);
                Venda.NumeroCriancas = Convert.ToInt32(dropNumeroCriancas.SelectedValue);
                Venda.IDLocalRetirada = 1;
                Venda.NomeResponsavelRetirada = "";
                Venda.RgResponsavelRetirada = "";
                Venda.NomeResponsavelRetirada2 = "";
                Venda.RgResponsavelRetirada2 = "";

                HttpContext.Current.Session["Venda"] = Venda;

                vendaCTL CVenda = new vendaCTL();
                int iIDFormaPagamento = 6; //Dinheiro
                int iIDVenda = Venda.IDVenda;
                int iNumeroCriancas = Convert.ToInt32(dropNumeroCriancas.SelectedValue);
                string sNomeRetirada = "";
                string sRgRetirada = "";
                int iIDLocalRetirada = 1;
                string sDetalhesFormaPagamento = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtDetalhesFormaPagamento.Text.Trim());
                string sRetorno = String.Empty;

                int iNumeroVagas = 0;
                try
                {
                    iNumeroVagas = Convert.ToInt32(dropVagasEstacionamento.SelectedValue);
                }
                catch { }

                string sNomeRetirada2 = "";
                string sRgRetirada2 = "";

                if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
                {
                    CVenda.AtualizarVendaReservada(iIDVenda, iIDFormaPagamento, sDetalhesFormaPagamento, iNumeroVagas,
                        iNumeroCriancas, sNomeRetirada, sRgRetirada, iIDLocalRetirada, sNomeRetirada2, sRgRetirada2);
                    HttpContext.Current.Session["Venda"] = null;

                    sRetorno =
                    "<h3 style='color:green;'>RESERVA FEITA COM SUCESSO!!!!!</h3><br/><br/>" +
                    "Número do Pedido: " + Venda.IDVenda.ToString() + "<br />" +
                    "Nome do titular: " + Venda.Nome + "<br />" +
                    "CPF do Titular: " + Venda.CPF + "<br />";
                }

                ExibirMensagemConclusaoCompra(iIDVenda, true, sRetorno);
                LinkButtonReciboPagamento.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Erro: " + ex.Message;
        }
    }

    protected void dropVagasEstacionamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            venda Venda = (venda)HttpContext.Current.Session["Venda"];
            if (Venda != null)
            {
                Venda.NumeroVagas = Convert.ToInt32(dropVagasEstacionamento.SelectedValue);
                HttpContext.Current.Session["Venda"] = Venda;
            }
        }
        catch { }
    }

    protected void chkAniversariante_CheckedChanged(object sender, EventArgs e)
    {
        venda Venda = (venda)HttpContext.Current.Session["Venda"];
        if (Venda != null)
        {
            Venda.Aniversariante = Convert.ToBoolean(chkAniversariante.Checked);
            HttpContext.Current.Session["Venda"] = Venda;
        }

        vendaCTL CVenda = new vendaCTL();
        DataSet dataSet = CVenda.RetornarCarrinhoCompras(Venda.IDVenda);

        if (chkAniversariante.Checked == true)
        {
            evento Evento = new evento();
            eventoCTL CEvento = new eventoCTL();
            Evento = CEvento.RetornarEdicao(Convert.ToInt32(Venda.IDEdicao));

            VerificarAniversariante(dataSet, Evento);
        }
        else
        {
            usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

            usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
            if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
                Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

            int iQteIngressosComprados = dataSet.Tables[0].Rows.Count;
            int iQteCortersiasAdquiridos = 0;
            List<int> IngressosAniversariante = new List<int>();

            foreach (DataRow dtRowIngressos in dataSet.Tables[0].Rows)
            {
                if (dtRowIngressos["TipoIngresso"].ToString() == "Cortesia não sócio Aniversariante" || dtRowIngressos["TipoIngresso"].ToString() == "Cortesia sócio Aniversariante")
                {
                    CVenda.CancelarTicket(Convert.ToInt32(dtRowIngressos["Ticket"].ToString()), "Ticket " + dtRowIngressos["Ticket"].ToString(), Cliente.IDUsuario);
                    iQteCortersiasAdquiridos++;
                }
            }
        }

        dataSet = CVenda.RetornarCarrinhoCompras(Venda.IDVenda);
        grdCarrinho.DataSource = dataSet.Tables[0];
        grdCarrinho.DataBind();
    }
}

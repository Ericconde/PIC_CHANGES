using Controller;
using Model.objetos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index_evento : System.Web.UI.Page
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
            if (Request.QueryString["idedicao"] != null)
            {
                PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
                string sIDEdicao = Request.QueryString["idedicao"].ToString();
                ExibirInformacoesEvento(sIDEdicao);
                CarregarCarrinhoCompras();

                if (HttpContext.Current.Session["Venda"] == null)
                    IniciarVenda(sIDEdicao);
                else
                {
                    venda Venda = (venda)HttpContext.Current.Session["Venda"];
                    if (Venda.IDEdicao == Convert.ToInt32(sIDEdicao))
                    {
                        ExibirResumoInformacoes();
                    }
                    else
                        IniciarVenda(sIDEdicao);
                }
            }

            CarregarTipoIngressoAvulso();
            ClientScript.RegisterStartupScript(this.GetType(), "Anchor", "location.hash = '#bottom';", true);
        }
    }

    private void IniciarVenda(string IDEdicao)
    {
        try
        {
            HttpContext.Current.Session["Venda"] = null;


            vendaCTL CVenda = new vendaCTL();
            clienteCTL cliente = new clienteCTL();
            usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
            usuarioCTL Cusuario = new usuarioCTL();

            usuario UsuarioLogado = Cusuario.RetornarUsuarioByEmail(Cliente.Email);
            UsuarioLogado.IDCliente = UsuarioLogado.IDCliente;
            if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
                Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

            //Compras em andamento simultâneas
            if (ConfigurationManager.AppSettings["LimitarComprasSimultaneas"].ToString() == "Sim")
            {
                if (UsuarioLogado.Perfil != "Vendedor" && UsuarioLogado.Perfil != "Administrador")
                {
                    DataSet dataSet = CVenda.RetornarComprasEmAndamento(Convert.ToInt32(IDEdicao));
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

                        PontoBr.Utilidades.Diversos.ExibirAlertaERedirecionarScriptManager("compras.aspx", this.Page, "Fila de compra excedida! Tente daqui a pouco.");
                    }
                }
            }

            //Liberar ingressos reservados
            CVenda.LiberarIngressos(UsuarioLogado.IDCliente, Convert.ToInt32(IDEdicao), -1);

            evento Evento = new evento();
            eventoCTL CEvento = new eventoCTL();
            Evento = CEvento.RetornarEdicao(Convert.ToInt32(IDEdicao));

            //Camarote
            //if (Evento.Evento == "Festa Junina")
            //{
            //    if (Cliente.Perfil == "Sócio")
            //        radTipoAvulso.Items.Add("Camarote sócio");
            //    else
            //        radTipoAvulso.Items.Add("Camarote não sócio");
            //}



            if (Cliente.Perfil == "Não Sócio" && UsuarioLogado.Perfil != "Administrador")
            {
                DateTime dataHoraInicioVendaNaoSocio = PontoBr.Conversoes.Data.ConverterDataBancoParaDateTime(Evento.InicioVendaNaoSocio);
                if (DateTime.Now < dataHoraInicioVendaNaoSocio)
                {
                    string sMensagem = "As compras estão abertas apenas para sócio.\\n\\n";
                    sMensagem += "Você poderá comprar a partir de " + dataHoraInicioVendaNaoSocio.ToString("dd/MM/yyyy HH:mm") + " h.";

                    LinkButtonAbrirMapaInteira.OnClientClick = "";
                    LinkButtonAbrirMapaInteira.Enabled = false;


                    ExibirMensagemPopUpNegativo(sMensagem);
                    return;
                }
            }

            //Mapa
            //if (!Convert.ToBoolean(Evento.PossuiMapa))
            //    pnlCadeira.Visible = false;

            PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
            string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
            string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
            string sIDEdicao = Criptografia.Criptografar(IDEdicao, sChave, sVetorInicializacao);

            //ConfigurarCampos(Evento.Evento);

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

            if (string.IsNullOrEmpty(sUrl))
            {
                LinkButtonAbrirMapaInteira.OnClientClick = "PopUpNegativo('Este evento não possui mapa.')";
                LinkButtonAbrirMapaInteira.ToolTip = "Este evento não possui mapas";
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////

            venda Venda = new venda();
            Venda.IDEdicao = Convert.ToInt32(IDEdicao);
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
            dataTable = CEvento.RetornarLotes(Convert.ToInt32(IDEdicao), true);
            if (UsuarioLogado.Perfil == "Administrador")
                dataTable = CEvento.RetornarLotes(Convert.ToInt32(IDEdicao), false);

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
            VerificarSaldoIngressos(Venda.IDEdicao);
            CarregarTipoIngressoAvulso();
            CarregarCarrinhoCompras();
        }
        catch (Exception ex)
        {
            //lblMensagem.Text = "Erro - " + ex.Message;
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
    private void ExibirMensagem(string sMensagem)
    {
        sMensagem = sMensagem.Replace("'", "");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + sMensagem + "')", true);
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


                lblResumo.Text += "<br/>Ingressos já comprados: " + Saldo.QuantidadeComprado.ToString();

            }
            else
            {
                Venda.IngressosPrecoSocio = Cliente.Dependentes + Venda.NumeroIngressoExtraSocioTitulo;
                Venda.IngressosPrecoNaoSocio = Venda.NumeroIngressosValorNaoSocio;


                lblResumo.Text += "<br/>Ingressos já comprados: " + Saldo.QuantidadeComprado.ToString();

            }
        }
        else
        {
            Venda.IngressosPrecoSocio = 0;
            Venda.IngressosPrecoNaoSocio = Venda.NumeroIngressosValorNaoSocio;


            lblResumo.Text += "<br/>Ingressos já comprados: " + Saldo.QuantidadeComprado.ToString();
        }

        //Teste do sistema
        //if (ConfigurationManager.AppSettings["ExibirResumoPrecos"].ToString() == "Sim")
        //{
        //    lblResumo.Text += "<br/><span style='color:red'>";
        //    lblResumo.Text += "<br/>*************************************Testes************************************";
        //    lblResumo.Text += "<br/>Lote: " + Venda.Lote;
        //    lblResumo.Text += "<br/>Tipo de ingresso [Inteira Sócio (cadeira)]: R$ " + Venda.ValorInteiraCadeiraSocio.ToString("0.00");
        //    lblResumo.Text += "<br/>Tipo de ingresso [Inteira Não sócio (cadeira)]: R$ " + Venda.ValorInteiraCadeiraNaoSocio.ToString("0.00");
        //    lblResumo.Text += "<br/>Tipo de ingresso [Inteira Sócio (sem cadeira)]: R$ " + Venda.ValorInteiraAvulsoSocio.ToString("0.00");
        //    lblResumo.Text += "<br/>Tipo de ingresso [Inteira Não sócio (ingresso sem cadeira)]: R$ " + Venda.ValorInteiraAvulsoNaoSocio.ToString("0.00");
        //    lblResumo.Text += "<br/>";
        //    lblResumo.Text += "<br/>Tipo de ingresso [Meio Adolescente Sócio (cadeira)]: R$ " + Venda.ValorMeiaCadeiraSocio.ToString("0.00");
        //    lblResumo.Text += "<br/>Tipo de ingresso [Meio Adolescente Não sócio (cadeira)]: R$ " + Venda.ValorMeiaCadeiraNaoSocio.ToString("0.00");
        //    lblResumo.Text += "<br/>Tipo de ingresso [Meio Adolescente Sócio (ingresso sem cadeira)]: R$ " + Venda.ValorMeiaAvulsoSocio.ToString("0.00");
        //    lblResumo.Text += "<br/>Tipo de ingresso [Meio Adolescente Não sócio (ingresso sem cadeira)]: R$ " + Venda.ValorMeiaAvulsoNaoSocio.ToString("0.00");
        //    lblResumo.Text += "<br/>*************************************Testes************************************";
        //    lblResumo.Text += "</span>";
        //}

        HttpContext.Current.Session["Venda"] = Venda;

        VerificarSaldoIngressos(Venda.IDEdicao);
        CarregarTipoIngressoAvulso();
    }

    private void ExibirInformacoesEvento(string sIDEdicao)
    {
        eventoCTL CEvento = new eventoCTL();
        evento Evento = new evento();

        Evento = CEvento.RetornarEdicao(Convert.ToInt32(sIDEdicao));

        string sUrl = "../Evento_Outros/setor_pergula.aspx?id=";
        string src = "../images/banner1.jpg"; ;
        if (Evento.Evento == "Festa Junina")
        {
            sUrl = "../Evento_FestaJunina/index.aspx?id=";
            src = "../images/BANNER_SITE_INGRESSO_ARRAIA_PIC2022.jpg";
        }
        else if (Evento.Evento == "Reveillon")
        {
            sUrl = "../Evento_Reveillon/index.aspx?id=";
            src = "../images/banner222.jpg";
        }
        else if (Evento.Evento == "Boate")
        {
            sUrl = "../Evento_Boate/piso_1.aspx?id=";
            src = "../images/banner10.jpg";
        }
        else if (Evento.Evento == "Outros")
        {
            sUrl = "../Evento_Outros/setor_pergula.aspx?id=";
            src = "../images/banner1.jpg";
        }

        imgbanner.Src = src;


        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        sIDEdicao = Criptografia.Criptografar(sIDEdicao, sChave, sVetorInicializacao);

        tituloEvento.InnerText = Evento.Evento.ToUpper().ToString();
        lblDetalhes.Text = Evento.Detalhes;
        localEvento.InnerText = Evento.IDLocal == 1 ? "PIC PAMPULHA" : "PIC CIDADE";
        LinkButtonAbrirMapaInteira.OnClientClick = "AbrirMapa('" + sIDEdicao + "', '" + sUrl + "');";
    }

    private void VerificarSaldoIngressos(int sIDEdicao)
    {
        //Ingressos já vendidos
        vendaCTL CVenda = new vendaCTL();
        int iVendidosCadeira = CVenda.RetornarQuantidadeVendidaCadeira(Convert.ToInt32(sIDEdicao));
        int iVendidosAvulso = CVenda.RetornarQuantidadeVendidaAvulso(Convert.ToInt32(sIDEdicao));

        venda Venda = (venda)HttpContext.Current.Session["Venda"];

        evento Lote = new evento();
        eventoCTL CEvento = new eventoCTL();
        Lote = CEvento.RetornarLote(Venda.IDLote);

        if (iVendidosCadeira >= Lote.IngressosCadeira)
        {
            LinkButtonAbrirMapaInteira.OnClientClick = "PopUpNegativo('Os ingressos cadeira/mesa esgotaram.')";
            LinkButtonAbrirMapaInteira.ToolTip = "INGRESSOS ESGOTADOS";
            LinkButtonAbrirMapaInteira.Enabled = false;
        }
    }

    private void CarregarTipoIngressoAvulso()
    {
        dropTipoAvulso.Items.Clear();

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

            if (bInteiraAvulsoSocio && Usuario.Perfil == "Sócio") dropTipoAvulso.Items.Add("Inteiro sem cadeira");
            if (bInteiraAvulsoNaoSocio) dropTipoAvulso.Items.Add("Não sócio Inteiro sem cadeira");

            if (bMeiaAvulsoSocio && Usuario.Perfil == "Sócio") dropTipoAvulso.Items.Add("Meio adolescente sem cadeira");
            if (bMeiaAvulsoNaoSocio) dropTipoAvulso.Items.Add("Não sócio Meio adolescente sem cadeira");

            if (bCamaroteSocio && Usuario.Perfil == "Sócio") dropTipoAvulso.Items.Add("Camarote sócio");
            if (bCamaroteNaoSocio) dropTipoAvulso.Items.Add("Camarote não sócio");

            if (dropTipoAvulso.Items.Count < 1)
            {
                dropTipoAvulso.Items.Add("INGRESSOS ESGOTADOS");
                dropTipoAvulso.Disabled = true;
            }
        }
    }

    protected void btnAdicionarAvulso_Click(object sender, EventArgs e)
    {
        double totalAvulso = 0.00;
        int iQtdIngressos = 0;
        double valorIngresso = 0.00;
        vendaCTL CVenda = new vendaCTL();
        eventoCTL CEvento = new eventoCTL();
        double valorTotal = Convert.ToDouble(lblValor.Text.Replace("R$ ", ""));
        string tipoIngresso = "";


        //if (!string.IsNullOrEmpty(lblCarrinhoTotal.InnerText))
        //{
        //    totalAvulso = Convert.ToDouble(lblCarrinhoTotal.InnerText);
        //}

        venda venda = (venda)HttpContext.Current.Session["Venda"];
        DataTable dataTableLotes = CEvento.RetornarLotes(venda.IDEdicao, false);
        DataTable dataAvulsos = new DataTable();
        dataAvulsos.Columns.Add("Ticket", typeof(string));
        dataAvulsos.Columns.Add("Tipo", typeof(string));
        dataAvulsos.Columns.Add("Valor", typeof(string));
        if (grdSemMesa.Rows.Count > 0)
        {
            dataAvulsos.Rows.Add(grdSemMesa.Rows);
        }

        DataRow dr = dataAvulsos.NewRow();

        if (!string.IsNullOrEmpty(dropTipoAvulso.Value) && Convert.ToInt32(input.Value) > 0)
        {
            if (dropTipoAvulso.Value == "Inteiro sem cadeira")
            {
                iQtdIngressos = Convert.ToInt32(input.Value);
                venda.IDTipoIngresso = 3;
                valorIngresso = Convert.ToDouble(dataTableLotes.Rows[0]["ValorInteiraAvulsoSocio"].ToString());
                valorTotal += Convert.ToDouble(dataTableLotes.Rows[0]["ValorInteiraAvulsoSocio"].ToString()) * Convert.ToInt32(input.Value);
                totalAvulso += Convert.ToDouble(dataTableLotes.Rows[0]["ValorInteiraAvulsoSocio"].ToString()) * Convert.ToInt32(input.Value);
                tipoIngresso = "Inteiro sócio - Valor R$ " + String.Format("{0:0.00}", valorIngresso);
            }

            if (dropTipoAvulso.Value == "Não sócio Inteiro sem cadeira")
            {

                totalAvulso += Convert.ToDouble(dataTableLotes.Rows[0]["ValorInteiraAvulsoNaoSocio"].ToString()) * Convert.ToInt32(input.Value);
                venda.IDTipoIngresso = 4;
                iQtdIngressos = Convert.ToInt32(input.Value);
                valorIngresso = Convert.ToDouble(dataTableLotes.Rows[0]["ValorInteiraAvulsoNaoSocio"].ToString());
                valorTotal += Convert.ToDouble(dataTableLotes.Rows[0]["ValorInteiraAvulsoNaoSocio"].ToString()) * Convert.ToInt32(input.Value);
                tipoIngresso = "Inteiro não sócio - Valor R$ " + String.Format("{0:0.00}", valorIngresso);
            }

            if (dropTipoAvulso.Value == "Meio adolescente sem cadeira")
            {

                totalAvulso += Convert.ToDouble(dataTableLotes.Rows[0]["ValorMeiaAvulsoSocio"].ToString()) * Convert.ToInt32(input.Value);
                venda.IDTipoIngresso = 7;
                iQtdIngressos = Convert.ToInt32(input.Value);
                valorIngresso = Convert.ToDouble(dataTableLotes.Rows[0]["ValorMeiaAvulsoSocio"].ToString());
                valorTotal += Convert.ToDouble(dataTableLotes.Rows[0]["ValorMeiaAvulsoSocio"].ToString()) * Convert.ToInt32(input.Value);
                tipoIngresso = "Meio sócio - Valor R$ " + String.Format("{0:0.00}", valorIngresso);
            }


            if (dropTipoAvulso.Value == "Não sócio Meio adolescente sem cadeira")
            {

                totalAvulso += Convert.ToDouble(dataTableLotes.Rows[0]["ValorMeiaAvulsoNaoSocio"].ToString()) * Convert.ToInt32(input.Value);
                venda.IDTipoIngresso = 8;
                iQtdIngressos = Convert.ToInt32(input.Value);
                valorIngresso = Convert.ToDouble(dataTableLotes.Rows[0]["ValorMeiaAvulsoNaoSocio"].ToString());
                valorTotal += Convert.ToDouble(dataTableLotes.Rows[0]["ValorMeiaAvulsoNaoSocio"].ToString()) * Convert.ToInt32(input.Value);
                tipoIngresso = "Meio não sócio - Valor R$ " + String.Format("{0:0.00}", valorIngresso);
            }

            if (dropTipoAvulso.Value == "Camarote sócio")
            {

                totalAvulso += venda.ValorCamaroteSocio * Convert.ToInt32(input.Value);
                venda.IDTipoIngresso = 18;
                iQtdIngressos = Convert.ToInt32(input.Value);
                valorIngresso = venda.ValorCamaroteSocio;
                valorTotal += venda.ValorCamaroteSocio * Convert.ToInt32(input.Value);
                tipoIngresso = "Camarote sócio - Valor R$ " + String.Format("{0:0.00}", valorIngresso);
            }
            if (dropTipoAvulso.Value == "Camarote não sócio")
            {
                totalAvulso += venda.ValorCamaroteNaoSocio * Convert.ToInt32(input.Value);
                venda.IDTipoIngresso = 19;
                iQtdIngressos = Convert.ToInt32(input.Value);
                valorIngresso = venda.ValorCamaroteNaoSocio;
                valorTotal += venda.ValorCamaroteNaoSocio * Convert.ToInt32(input.Value);
                tipoIngresso = "Camarote não sócio - Valor R$ " + String.Format("{0:0.00}", valorIngresso);
            }


            //if (Convert.ToInt32(input.Value) > 1)
            //{
            //    for (int i = 0; i < Convert.ToInt32(input.Value); i++)
            //    {
            //        dataAvulsos.Rows.Add("", tipoIngresso, String.Format("{0:0.00}", Convert.ToDouble(valorIngresso.ToString())));
            //    }
            //}
            //else
            //{
            //    dataAvulsos.Rows.Add("", tipoIngresso, String.Format("{0:0.00}", Convert.ToDouble(valorIngresso.ToString())));
            //}


            venda.Valor = valorIngresso;
            lblValor.Text = "R$ " + valorTotal;

            HttpContext.Current.Session["Venda"] = venda;

            if (iQtdIngressos > 0)
            {
                for (int i = 0; i < iQtdIngressos; i++)
                {
                    CVenda.CadastrarVendaAvulso(venda);
                }
            }

            //grdSemMesa.DataSource = dataAvulsos;
            //grdSemMesa.DataBind();

            //lblCarrinhoTotal.InnerText = " " + String.Format("{0:0.00}", totalAvulso.ToString());
            input.Value = "0";

            CarregarCarrinhoCompras();
        }
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
                double totalAvulso = 0;
                double totalCadeira = 0;
                usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

                DataTable dataSetMesa = new DataTable();
                dataSetMesa = dataSet.Tables[0].Clone();
                DataTable dataSetSemMesa = new DataTable();
                dataSetSemMesa = dataSet.Tables[0].Clone();
                DataTable dataAvulsos = new DataTable();
                DataTable dataCadeira = new DataTable();

                dataAvulsos.Columns.Add("Ticket", typeof(string));
                dataAvulsos.Columns.Add("Tipo", typeof(string));
                dataAvulsos.Columns.Add("Valor", typeof(string));

                dataCadeira.Columns.Add("Ticket", typeof(string));
                dataCadeira.Columns.Add("Tipo", typeof(string));
                dataCadeira.Columns.Add("Valor", typeof(string));

                DataRow dr = dataAvulsos.NewRow();
                DataRow dr1 = dataCadeira.NewRow();
                string tipoIngresso = "";

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    if (dataRow["Tipo"].ToString().Contains("mesa"))
                    {
                        tipoIngresso = dataRow["Tipo"].ToString().Split(';')[2] + " - " + dataRow["TipoIngresso"].ToString().Replace("cadeira", "") + " - " + dataRow["Tipo"].ToString().Split(';')[3] + " - R$ " + String.Format("{0:0.00}", Convert.ToDouble(dataRow["Valor"].ToString()));

                        dataCadeira.Rows.Add(dataRow["Ticket"].ToString(), tipoIngresso, String.Format("{0:0.00}", Convert.ToDouble(dataRow["Valor"].ToString())));
                        //dataSetMesa.ImportRow(dataRow);
                        totalCadeira += Convert.ToDouble(dataRow["Valor"].ToString());
                    }
                    else
                    {
                        string ingressoAvulso = dataRow["Tipo"].ToString().Contains("avulso") ? dataRow["Tipo"].ToString().Replace("avulso", "") : dataRow["Tipo"].ToString();
                        tipoIngresso = ingressoAvulso + " - R$" + String.Format("{0:0.00}", Convert.ToDouble(dataRow["Valor"].ToString()));

                        dataAvulsos.Rows.Add(dataRow["Ticket"].ToString(), tipoIngresso, String.Format("{0:0.00}", Convert.ToDouble(dataRow["Valor"].ToString())));

                        totalAvulso += Convert.ToDouble(dataRow["Valor"].ToString());
                    }
                }

                //if (totalAvulso > 0)
                //    lblCarrinhoTotal.InnerText = String.Format("{0:0.00}", totalAvulso);
                //if (totalCadeira > 0)
                //    lblCarrinhoCadeira.InnerText = String.Format("{0:0.00}", totalCadeira);

                evento Evento = new evento();
                eventoCTL CEvento = new eventoCTL();
                Evento = CEvento.RetornarEdicao(Convert.ToInt32(Venda.IDEdicao));

                //Mapa              

                //if (Venda.Aniversariante) chkAniversariante.Checked = Venda.Aniversariante;
                ////Verificar se é Aniversariante e gerar a cortesia.
                //if (Venda.Evento == "Boate")
                //    VerificarAniversariante(dataSet, Evento);              


                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    lblValor.Text = "R$ " + String.Format("{0:0.00}", dataSet.Tables[1].Rows[0]["Valor"].ToString());
                }
                else
                    lblValor.Text = "R$ 0,00";

                if ((dataSet.Tables[1].Rows.Count > 0) && !string.IsNullOrEmpty(dataSet.Tables[1].Rows[0]["Valor"].ToString()))
                {
                    Venda.ValorTotal = Convert.ToDecimal(dataSet.Tables[1].Rows[0]["Valor"]);
                }
                else
                    Venda.ValorTotal = 0;

                HttpContext.Current.Session["Venda"] = Venda;

                grdMesa.DataSource = dataCadeira;
                grdMesa.DataBind();

                grdSemMesa.DataSource = dataAvulsos;
                grdSemMesa.DataBind();

                //Vagas de estacionamento
                //int iQuantidadeIngressosTipoCadeira = Convert.ToInt32(dataSet.Tables[2].Rows[0]["IngressosCadeira"].ToString());
                //HabilitarVagaEstacionamento(Convert.ToBoolean(Evento.VagaEstacionamento), iQuantidadeIngressosTipoCadeira, Evento.NumIngressoEstacionamento);              

                //Carregar valores avulsos - só para administrador                
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnEfetuarPagamento_Click(object sender, EventArgs e)
    {
        venda venda = (venda)HttpContext.Current.Session["Venda"];
        vendaCTL CVenda = new vendaCTL();


        if (grdMesa.Rows.Count <= 0 && grdSemMesa.Rows.Count <= 0)
        {
            string sMensagem = "Favor selecionar os ingressos antes de prosseguir!";
            ExibirMensagemPopUpNegativo(sMensagem);
            return;
        }

        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        string sIDVenda = Criptografia.Criptografar(venda.IDVenda.ToString(), sChave, sVetorInicializacao);

        if (PodeFinalizar(true))
        {
            Response.Redirect("../pagamento/checkout.aspx?id=" + sIDVenda, false);
        }
    }

    private bool PodeFinalizar(bool bReserva)
    {
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

                ExibirMensagemPopUpNegativo(sMensagemMesas);
                return false;
            }
        }

        //Limites de ingressos////////////////////////////////////////////////////////////
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
        if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
            Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

        string sMensagem;
        if (CVenda.VerificarExistenciaVendaEmAndamento(Cliente.IDCliente, Venda.IDVenda))
        {
            ExibirMensagemPopUpNegativo("Há outra venda em andamento para seu usuário.");
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

                ExibirMensagemPopUpNegativo(sMensagem);
                return false;
            }
        }

        //Para reservar, só pode ingressos do tipo cadeira
        //if (bReserva)
        //{
        //    if (Saldo.QuantidadeAvulsoVendaAtual > 0)
        //    {
        //        ExibirMensagem("[Ingressos sem cadeira] não podem ser reservados.");
        //        return false;
        //    }
        //}

        evento Lote = new evento();
        eventoCTL CEvento = new eventoCTL();
        Lote = CEvento.RetornarLote(Venda.IDLote);

        evento Edicao = new evento();
        Edicao = CEvento.RetornarEdicao(Convert.ToInt32(Venda.IDEdicao));

        //O Administrador pode comprar ilimitado
        if (UsuarioLogado.Perfil != "Administrador")
        {
            //Limite de ingressos tipo cadeira
            if ((Saldo.QuantidadeCadeiraComprado + Saldo.QuantidadeCadeiraVendaAtual) > Edicao.NumeroIngressosCadeira)
            {
                ExibirMensagemPopUpNegativo("Você só pode comprar " + Edicao.NumeroIngressosCadeira.ToString() + " ingressos do tipo cadeira.");
                return false;
            }
        }

        //Sessão expirou e não tem mais ingressos para esta venda
        if (Saldo.QuantidadeCarrinhoVendaAtual == 0)
        {
            ExibirMensagemPopUpNegativo("Seu tempo para finalizar a compra expirou.\n\nEscolha outros ingressos (cadeira ou sem cadeira) e adicione no carrinho de compras.");
            CarregarCarrinhoCompras();
            return false;
        }

        //Ingressos já vendidos        
        int iVendidosAvulso = CVenda.RetornarQuantidadeVendidaAvulso(Edicao.IDEdicao);
        //if (iVendidosAvulso >= Lote.IngressosAvulsos)
        //{
        //    ExibirMensagem("Os ingressos do tipo sem cadeira esgotaram para este lote.");
        //    return false;
        //}
        if ((iVendidosAvulso + Saldo.QuantidadeAvulsoVendaAtual) > Lote.IngressosAvulsos)
        {
            ExibirMensagemPopUpNegativo("Os ingressos estão esgostando. Você só pode comprar " + (Lote.IngressosAvulsos - iVendidosAvulso).ToString() + " ingresso(s) sem cadeira para este lote.");
            return false;
        }
        return true;
    }
    protected void BtnPlus_Click(object sender, EventArgs e)
    {
        input.Value = (Convert.ToInt32(input.Value) + 1).ToString();
    }

    protected void BtnMin_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(input.Value) > 0)
            input.Value = (Convert.ToInt32(input.Value) - 1).ToString();
    }
    protected void grdMesa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sTipoIngresso = grdMesa.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
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

                double dTicket = Convert.ToDouble(grdMesa.DataKeys[int.Parse((string)e.CommandArgument)]["Ticket"].ToString());
                string sTextoLog = "Ticket " + dTicket.ToString();
                sTextoLog += "; " + Server.HtmlDecode(grdMesa.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text);


                venda Venda = (venda)HttpContext.Current.Session["Venda"];
                sTextoLog += "; Voucher " + Venda.IDVenda.ToString();

                CVenda.CancelarTicket(dTicket, sTextoLog, Cliente.IDUsuario);
                CarregarCarrinhoCompras();
            }
        }
    }

    protected void grdSemMesa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sTipoIngresso = grdSemMesa.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
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

                double dTicket = Convert.ToDouble(grdSemMesa.DataKeys[int.Parse((string)e.CommandArgument)]["Ticket"].ToString());
                string sTextoLog = "Ticket " + dTicket.ToString();
                sTextoLog += "; " + Server.HtmlDecode(grdSemMesa.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text);


                venda Venda = (venda)HttpContext.Current.Session["Venda"];
                sTextoLog += "; Voucher " + Venda.IDVenda.ToString();


                //Implementar aqui...

                CVenda.CancelarTicket(dTicket, sTextoLog, Cliente.IDUsuario);
                CarregarCarrinhoCompras();
            }
        }
    }

    protected void btnLimparCarrinho_Click(object sender, EventArgs e)
    {
        usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
        venda Venda = (venda)HttpContext.Current.Session["Venda"];

        vendaCTL CVenda = new vendaCTL();
        DataSet dataSet = CVenda.RetornarCarrinhoCompras(Venda.IDVenda);

        if (HttpContext.Current.Session["Venda"] != null)
        {

            foreach (DataRow item in dataSet.Tables[0].Rows)
            {
                double dTicket = Convert.ToDouble(item["Ticket"].ToString());
                string sTextoLog = "Ticket " + dTicket.ToString();
                sTextoLog += "; " + Server.HtmlDecode(item["Tipo"].ToString());

                sTextoLog += "; Voucher " + Venda.IDVenda.ToString();

                CVenda.CancelarTicket(dTicket, sTextoLog, Cliente.IDUsuario);
            }
          
            CVenda.LiberarIngressos(Venda.IDCliente, Venda.IDEdicao, -1);
          
            lblValor.Text = "R$ 0,00";
            CarregarCarrinhoCompras();
        }
    }
}
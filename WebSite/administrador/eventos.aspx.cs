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

public partial class administrador_eventos : App_Code.BaseWeb 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, false, false, false, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            CarregarLocais();
            txtDetalhes.Text = "";
            CarregarEventos();
        }
    }

    private void CarregarLocais()
    {
        eventoCTL CEvento = new eventoCTL();
        CEvento.CarregarDropDownListLocaisEvento(dropLocal, false, true);
    }

    private void CarregarEventos()
    {
        eventoCTL CEvento = new eventoCTL();
        CEvento.CarregarGridViewEventos(grdEvento);

        lblNumeroLinhas.Text = "| " + grdEvento.Rows.Count.ToString() + " registro(s) |";
    }

    protected void cmdCancelar_Click(object sender, EventArgs e)
    {
        LimparFormulario();
    }

    protected void cmdSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            if (PodeSalvar())
            {
                evento Evento = new evento();
                eventoCTL CEvento = new eventoCTL();

                Evento.IDEvento = Convert.ToInt32(radEventos.SelectedValue);
                Evento.Edicao = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtEdicao.Text);
                Evento.DataHoraEvento = PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(txtDataEvento.Text) + " " + dropHoraEvento.SelectedValue;
                Evento.InicioVendaNaoSocio = PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(txtDataVendaNaoSocio.Text) + " " + dropHoraVendaNaoSocio.SelectedValue;
                Evento.NumeroIngressoExtraSocioCota = Convert.ToInt32(txtNumeroIngressoExtraSocioCota.Text);
                Evento.NumeroIngressoExtraSocioTitulo = Convert.ToInt32(txtNumeroIngressoExtraSocioTitulo.Text);
                Evento.NumeroIngressosValorNaoSocio = Convert.ToInt32(txtNumeroIngressosValorNaoSocio.Text);
                Evento.NumeroIngressosCadeira = Convert.ToInt32(txtNumeroIngressosCadeira.Text);
                Evento.IngressosCadeira = Convert.ToInt32(txtTotalCadeiras.Text);
                Evento.NumeroIngressosCamarote = Convert.ToInt32(txtNumeroIngressosCamarote.Text);
                Evento.MinimoAniversariante = String.IsNullOrEmpty(txtMinimoAniversariante.Text.Trim()) ? 0 : Convert.ToInt32(txtMinimoAniversariante.Text);
                Evento.IDLocal = Convert.ToInt32(dropLocal.SelectedValue);

                Evento.DataLimite4ParcelaCota = PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(txtDataLimite4ParcelaCota.Text);
                Evento.DataLimite3ParcelaCota = PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(txtDataLimite3ParcelaCota.Text);
                Evento.DataLimite2ParcelaCota = PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(txtDataLimite2ParcelaCota.Text);
                Evento.DataLimite1ParcelaCota = PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(txtDataLimite1ParcelaCota.Text);

                Evento.DataLimite5ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(txtDataLimite5ParcelaCredito.Text);
                Evento.DataLimite4ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(txtDataLimite4ParcelaCredito.Text);
                Evento.DataLimite3ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(txtDataLimite3ParcelaCredito.Text);
                Evento.DataLimite2ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(txtDataLimite2ParcelaCredito.Text);
                Evento.DataLimite1ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(txtDataLimite1ParcelaCredito.Text);

                Evento.VagaEstacionamento = Convert.ToInt32(radVagaEstacionamento.SelectedValue);
                Evento.NumIngressoEstacionamento = Convert.ToInt32(dropNumeroIngressoEstacionamento.SelectedValue);
                Evento.VagasEstacionamento = String.IsNullOrEmpty(txtVagasEstacionamento.Text.Trim()) ? 0 : Convert.ToInt32(txtVagasEstacionamento.Text);
                if (Evento.VagaEstacionamento == 0) Evento.NumIngressoEstacionamento = -1;

                Evento.Detalhes = txtDetalhes.Text.Replace("'", "\"");
                Evento.DataRetirada = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtDataRetirada.Text);
                Evento.Ativo = Convert.ToInt32(radStatus.SelectedValue);
                Evento.IDStatusIngresso = Convert.ToInt32(radIngressos.SelectedValue);
                Evento.PossuiMapa = Convert.ToInt32(radPossuiMapa.SelectedValue);

                string sMensagem = String.Empty;
                if (!String.IsNullOrEmpty(hddIdEdicao.Value))
                {
                    usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

                    Evento.IDEdicao = Convert.ToInt32(hddIdEdicao.Value);
                    Evento.IDUsuarioAlteracao = UsuarioLogado.IDUsuario;

                    RegistrarLogAlteracoes(Evento);

                    //Atualizar Evento
                    CEvento.AtualizarEdicao(Evento);
                    sMensagem = "Evento atualizado com sucesso!";
                }
                else
                {
                    usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

                    Evento.IDUsuarioCadastro = UsuarioLogado.IDUsuario;

                    //Atualizar Evento
                    CEvento.CadastrarEdicao(Evento);
                    sMensagem = "Evento cadastrado com sucesso!";
                }

                LimparFormulario();
                lblMensagem.Text = sMensagem;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Erro: " + ex.Message;
        }
    }

    private void LimparFormulario()
    {
        hddIdEdicao.Value = null;

        txtEdicao.Text = String.Empty;
        txtDataEvento.Text = String.Empty;
        dropHoraEvento.SelectedValue = "00:00";
        txtNumeroIngressoExtraSocioCota.Text = String.Empty;
        txtNumeroIngressoExtraSocioTitulo.Text = String.Empty;
        txtNumeroIngressosValorNaoSocio.Text = String.Empty;
        txtNumeroIngressosCadeira.Text = String.Empty;
        txtNumeroIngressosCamarote.Text = String.Empty;
        txtTotalCadeiras.Text = String.Empty;
        txtMinimoAniversariante.Text = String.Empty;
        dropLocal.SelectedValue = "-1";

        txtDataLimite4ParcelaCota.Text = String.Empty;
        txtDataLimite3ParcelaCota.Text = String.Empty;
        txtDataLimite2ParcelaCota.Text = String.Empty;
        txtDataLimite1ParcelaCota.Text = String.Empty;

        txtDataLimite5ParcelaCredito.Text = String.Empty;
        txtDataLimite4ParcelaCredito.Text = String.Empty;
        txtDataLimite3ParcelaCredito.Text = String.Empty;
        txtDataLimite2ParcelaCredito.Text = String.Empty;
        txtDataLimite1ParcelaCredito.Text = String.Empty;

        radVagaEstacionamento.SelectedValue = "1";
        radIngressos.SelectedValue = "0";
        dropNumeroIngressoEstacionamento.SelectedValue = "-1";
        txtVagasEstacionamento.Text = String.Empty;
        txtDetalhes.Text = String.Empty;
        txtDataRetirada.Text = String.Empty;

        txtDataVendaNaoSocio.Text = String.Empty;
        dropHoraVendaNaoSocio.SelectedValue = "00:00";

        CarregarEventos();
        lblMensagem.Text = "";
    }

    private void RegistrarLogAlteracoes(evento EventoAlterado)
    {
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        int iIDEdicao = Convert.ToInt32(hddIdEdicao.Value);
        evento EventoAtual = new evento();
        eventoCTL CEvento = new eventoCTL();

        EventoAtual = CEvento.RetornarEdicao(iIDEdicao);

        string sTextoLog;
        if (EventoAtual.Edicao != EventoAlterado.Edicao)
        {
            sTextoLog = "Edição alterada de [" + EventoAtual.Edicao + "] para [" + EventoAlterado.Edicao + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.Ativo != EventoAlterado.Ativo)
        {
            string sStatusAtual = EventoAtual.Ativo == 1 ? "Ativo" : "Inativo";
            string sStatusAlterado = EventoAlterado.Ativo == 1 ? "Ativo" : "Inativo";

            sTextoLog = "Status alterado de [" + sStatusAtual + "] para [" + sStatusAlterado + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.IDStatusIngresso != EventoAlterado.IDStatusIngresso)
        {
            string sStatusAtual = EventoAtual.IDStatusIngresso == 1 ? " Disponível" : "Indisponível";
            string sStatusAlterado = EventoAlterado.IDStatusIngresso == 1 ? "Disponível" : "Indisponível";

            sTextoLog = "Status alterado de [" + sStatusAtual + "] para [" + sStatusAlterado + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.DataHoraEvento != txtDataEvento.Text + " " + dropHoraEvento.SelectedValue + ":00")
        {
            sTextoLog = "Data/Hora do evento alterado de [" + EventoAtual.DataHoraEvento + "] para [" + txtDataEvento.Text + " " + dropHoraEvento.SelectedValue + ":00" + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.Lote != EventoAlterado.Lote)
        {
            sTextoLog = "Lote alterado de [" + EventoAtual.Lote + "] para [" + EventoAlterado.Lote + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.NumeroIngressoExtraSocioCota != EventoAlterado.NumeroIngressoExtraSocioCota)
        {
            sTextoLog = "Núm. de ingressos extra por sócio (cota) alterado de [" + EventoAtual.NumeroIngressoExtraSocioCota + "] para [" + EventoAlterado.NumeroIngressoExtraSocioCota + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.NumeroIngressoExtraSocioTitulo != EventoAlterado.NumeroIngressoExtraSocioTitulo)
        {
            sTextoLog = "Núm. de ingressos extra por sócio (título) alterado de [" + EventoAtual.NumeroIngressoExtraSocioTitulo + "] para [" + EventoAlterado.NumeroIngressoExtraSocioTitulo + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.NumeroIngressosValorNaoSocio != EventoAlterado.NumeroIngressosValorNaoSocio)
        {
            sTextoLog = "Núm. de ingressos com valor de não sócio alterado de [" + EventoAtual.NumeroIngressosValorNaoSocio + "] para [" + EventoAlterado.NumeroIngressosValorNaoSocio + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.IngressosAvulsos != EventoAlterado.IngressosAvulsos)
        {
            sTextoLog = "Núm. de ingressos avulsos alterado de [" + EventoAtual.IngressosAvulsos + "] para [" + EventoAlterado.IngressosAvulsos + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.NumeroIngressosCadeira != EventoAlterado.NumeroIngressosCadeira)
        {
            sTextoLog = "Núm. de ingressos tipo cadeira alterado de [" + EventoAtual.NumeroIngressosCadeira + "] para [" + EventoAlterado.NumeroIngressosCadeira + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.ValorInteiraCadeiraSocio != EventoAlterado.ValorInteiraCadeiraSocio)
        {
            sTextoLog = "Valor Inteira Sócio (cadeira) alterado de [" + EventoAtual.ValorInteiraCadeiraSocio + "] para [" + EventoAlterado.ValorInteiraCadeiraSocio + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.ValorInteiraCadeiraNaoSocio != EventoAlterado.ValorInteiraCadeiraNaoSocio)
        {
            sTextoLog = "Valor Inteira Não sócio (cadeira) alterado de [" + EventoAtual.ValorInteiraCadeiraNaoSocio + "] para [" + EventoAlterado.ValorInteiraCadeiraNaoSocio + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.ValorInteiraAvulsoSocio != EventoAlterado.ValorInteiraAvulsoSocio)
        {
            sTextoLog = "Valor Inteira Sócio (sem cadeira) alterado de [" + EventoAtual.ValorInteiraAvulsoSocio + "] para [" + EventoAlterado.ValorInteiraAvulsoSocio + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.ValorInteiraAvulsoNaoSocio != EventoAlterado.ValorInteiraAvulsoNaoSocio)
        {
            sTextoLog = "Valor Inteira Não sócio (sem cadeira) alterado de [" + EventoAtual.ValorInteiraAvulsoNaoSocio + "] para [" + EventoAlterado.ValorInteiraAvulsoNaoSocio + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.ValorMeiaCadeiraSocio != EventoAlterado.ValorMeiaCadeiraSocio)
        {
            sTextoLog = "Valor Meio Adolescente Sócio (cadeira) alterado de [" + EventoAtual.ValorMeiaCadeiraSocio + "] para [" + EventoAlterado.ValorMeiaCadeiraSocio + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.ValorMeiaCadeiraNaoSocio != EventoAlterado.ValorMeiaCadeiraNaoSocio)
        {
            sTextoLog = "Valor Meio Adolescente Não sócio (cadeira) alterado de [" + EventoAtual.ValorMeiaCadeiraNaoSocio + "] para [" + EventoAlterado.ValorMeiaCadeiraNaoSocio + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.ValorMeiaAvulsoSocio != EventoAlterado.ValorMeiaAvulsoSocio)
        {
            sTextoLog = "Valor Meio Adolescente Sócio (sem cadeira) alterado de [" + EventoAtual.ValorMeiaAvulsoSocio + "] para [" + EventoAlterado.ValorMeiaAvulsoSocio + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.ValorMeiaAvulsoNaoSocio != EventoAlterado.ValorMeiaAvulsoNaoSocio)
        {
            sTextoLog = "Valor Meio Adolescente Não sócio (sem cadeira) alterado de [" + EventoAtual.ValorMeiaAvulsoNaoSocio + "] para [" + EventoAlterado.ValorMeiaAvulsoNaoSocio + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }

        if (EventoAtual.DataLimite4ParcelaCota != txtDataLimite4ParcelaCota.Text)
        {
            sTextoLog = "Data limite (cota) para 4 parcelas alterado de [" + EventoAtual.DataLimite4ParcelaCota + "] para [" + txtDataLimite4ParcelaCota.Text + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.DataLimite3ParcelaCota != txtDataLimite3ParcelaCota.Text)
        {
            sTextoLog = "Data limite (cota) para 3 parcelas alterado de [" + EventoAtual.DataLimite3ParcelaCota + "] para [" + txtDataLimite3ParcelaCota.Text + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.DataLimite2ParcelaCota != txtDataLimite2ParcelaCota.Text)
        {
            sTextoLog = "Data limite (cota) para 2 parcelas alterado de [" + EventoAtual.DataLimite2ParcelaCota + "] para [" + txtDataLimite2ParcelaCota.Text + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.DataLimite1ParcelaCota != txtDataLimite1ParcelaCota.Text)
        {
            sTextoLog = "Data limite (cota) para 1 parcela alterado de [" + EventoAtual.DataLimite1ParcelaCota + "] para [" + txtDataLimite1ParcelaCota.Text + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }

        if (EventoAtual.DataLimite5ParcelaCredito != txtDataLimite5ParcelaCredito.Text)
        {
            sTextoLog = "Data limite (crédito) para 5 parcelas alterado de [" + EventoAtual.DataLimite5ParcelaCredito + "] para [" + txtDataLimite5ParcelaCredito.Text + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.DataLimite4ParcelaCredito != txtDataLimite4ParcelaCredito.Text)
        {
            sTextoLog = "Data limite (crédito) para 4 parcelas alterado de [" + EventoAtual.DataLimite4ParcelaCredito + "] para [" + txtDataLimite4ParcelaCredito.Text + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.DataLimite3ParcelaCredito != txtDataLimite3ParcelaCredito.Text)
        {
            sTextoLog = "Data limite (crédito) para 3 parcelas alterado de [" + EventoAtual.DataLimite3ParcelaCredito + "] para [" + txtDataLimite3ParcelaCredito.Text + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.DataLimite2ParcelaCredito != txtDataLimite2ParcelaCredito.Text)
        {
            sTextoLog = "Data limite (crédito) para 2 parcelas alterado de [" + EventoAtual.DataLimite2ParcelaCredito + "] para [" + txtDataLimite2ParcelaCredito.Text + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.DataLimite1ParcelaCredito != txtDataLimite1ParcelaCredito.Text)
        {
            sTextoLog = "Data limite (crédito) para 1 parcela alterado de [" + EventoAtual.DataLimite1ParcelaCredito + "] para [" + txtDataLimite1ParcelaCredito.Text + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }

        if (EventoAtual.VagaEstacionamento != EventoAlterado.VagaEstacionamento)
        {
            string sVagaEstacionamentoAtual = EventoAtual.VagaEstacionamento == 1 ? "Sim" : "Não";
            string sVagaEstacionamentoAlterado = EventoAlterado.VagaEstacionamento == 1 ? "Sim" : "Não";

            sTextoLog = "Vagas de estacionamento alterado de [" + sVagaEstacionamentoAtual + "] para [" + sVagaEstacionamentoAlterado + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.NumIngressoEstacionamento != EventoAlterado.NumIngressoEstacionamento)
        {
            sTextoLog = "Número de Ingressos para ter direito à vaga alterado de [" + EventoAtual.NumIngressoEstacionamento + "] para [" + EventoAlterado.NumIngressoEstacionamento + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.VagasEstacionamento != EventoAlterado.VagasEstacionamento)
        {
            sTextoLog = "Vagas de Estacionamento alterado de [" + EventoAtual.VagasEstacionamento + "] para [" + EventoAlterado.VagasEstacionamento + "]";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
    }

    private bool PodeSalvar()
    {
        string sMensagem;
        lblMensagem.Text = String.Empty;

        if (String.IsNullOrEmpty(radEventos.SelectedValue))
        {
            sMensagem = "Selecione [Evento].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtEdicao.Text.Trim()))
        {
            sMensagem = "Preencha [Edição].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        int iIDEdicao = -1;
        eventoCTL CEvento = new eventoCTL();
        if (!String.IsNullOrEmpty(hddIdEdicao.Value)) iIDEdicao = Convert.ToInt32(hddIdEdicao.Value);
        if (CEvento.VerificarExistenciaEdicao(iIDEdicao, Convert.ToInt32(radEventos.SelectedValue), PontoBr.Utilidades.String.RemoverCaracterInvalido(txtEdicao.Text.Trim())))
        {
            sMensagem = "[Edição] já está cadastrada.";
            lblMensagem.Text = sMensagem;
            return false;
        } 

        if (string.IsNullOrEmpty(txtDataEvento.Text.Trim()))
        {
            sMensagem = "Preencha [Data/Hora].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (!PontoBr.Conversoes.Data.ValidarDataBr(txtDataEvento.Text))
        {
            sMensagem = "[Data/Hora] inválida.";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (string.IsNullOrEmpty(txtDataVendaNaoSocio.Text.Trim()))
        {
            sMensagem = "Preencha [Início venda para não sócio].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (!PontoBr.Conversoes.Data.ValidarDataBr(txtDataVendaNaoSocio.Text))
        {
            sMensagem = "[Início venda para não sócio] inválida.";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (dropLocal.SelectedValue == "-1")
        {
            sMensagem = "Selecione [Local do Evento].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (string.IsNullOrEmpty(txtNumeroIngressoExtraSocioCota.Text.Trim()))
        {
            sMensagem = "Preencha [Ingressos extras por sócio (cota)].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtNumeroIngressoExtraSocioTitulo.Text.Trim()))
        {
            sMensagem = "Preencha [Ingressos extras por sócio (título)].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtNumeroIngressosValorNaoSocio.Text.Trim()))
        {
            sMensagem = "Preencha [Ingressos com valor de não sócio].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtTotalCadeiras.Text.Trim()))
        {
            sMensagem = "Preencha [Total ingressos tipo Cadeira].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtNumeroIngressosCadeira.Text.Trim()))
        {
            sMensagem = "Preencha [Ingressos tipo cadeira].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtNumeroIngressosCamarote.Text.Trim()))
        {
            sMensagem = "Preencha [Ingressos camarote].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (!PontoBr.Conversoes.Data.ValidarDataBr(txtDataLimite4ParcelaCota.Text))
        {
            sMensagem = "[Limite Venc. em 4 parcelas (cota)] inválido.";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (!PontoBr.Conversoes.Data.ValidarDataBr(txtDataLimite3ParcelaCota.Text))
        {
            sMensagem = "[Limite Venc. em 3 parcelas (cota)] inválido.";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (!PontoBr.Conversoes.Data.ValidarDataBr(txtDataLimite2ParcelaCota.Text))
        {
            sMensagem = "[Limite Venc. em 2 parcelas (cota)] inválido.";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (!PontoBr.Conversoes.Data.ValidarDataBr(txtDataLimite1ParcelaCota.Text))
        {
            sMensagem = "[Limite Venc. em 1 parcela (cota)] inválido.";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (!PontoBr.Conversoes.Data.ValidarDataBr(txtDataLimite5ParcelaCredito.Text))
        {
            sMensagem = "[Limite Venc. em 5 parcelas (crédito)] inválido.";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (!PontoBr.Conversoes.Data.ValidarDataBr(txtDataLimite4ParcelaCredito.Text))
        {
            sMensagem = "[Limite Venc. em 4 parcelas (crédito)] inválido.";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (!PontoBr.Conversoes.Data.ValidarDataBr(txtDataLimite3ParcelaCredito.Text))
        {
            sMensagem = "[Limite Venc. em 3 parcelas (crédito)] inválido.";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (!PontoBr.Conversoes.Data.ValidarDataBr(txtDataLimite2ParcelaCredito.Text))
        {
            sMensagem = "[Limite Venc. em 2 parcelas (crédito)] inválido.";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (!PontoBr.Conversoes.Data.ValidarDataBr(txtDataLimite1ParcelaCredito.Text))
        {
            sMensagem = "[Limite Venc. em 1 parcela (crédito)] inválido.";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtDataRetirada.Text.Trim()))
        {
            sMensagem = "Preencha [Data de Retirada].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (radVagaEstacionamento.SelectedValue == "1")
        {
            if (dropNumeroIngressoEstacionamento.SelectedValue == "-1")
            {
                sMensagem = "Preencha [Número de Ingressos para ter direito à vaga].";
                lblMensagem.Text = sMensagem;
                return false;
            }
            if (string.IsNullOrEmpty(txtVagasEstacionamento.Text.Trim()))
            {
                sMensagem = "Preencha [Vagas Estacionamento].";
                lblMensagem.Text = sMensagem;
                return false;
            }
        }
        return true;
    }

    protected void grdEvento_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
        {
            LimparFormulario();

            int iIDEdicao = Convert.ToInt32(grdEvento.DataKeys[int.Parse((string)e.CommandArgument)]["IDEdicao"].ToString());
            evento Evento = new evento();
            eventoCTL CEvento = new eventoCTL();

            Evento = CEvento.RetornarEdicao(iIDEdicao);

            hddIdEdicao.Value = iIDEdicao.ToString();
            radEventos.SelectedValue = Evento.IDEvento.ToString();
            txtEdicao.Text = Evento.Edicao;

            DateTime dataHoraEvento = PontoBr.Conversoes.Data.ConverterDataBancoParaDateTime(Evento.DataHoraEvento);
            txtDataEvento.Text = dataHoraEvento.ToString("dd/MM/yyyy");
            dropHoraEvento.SelectedValue = dataHoraEvento.ToString("HH:mm");

            DateTime dataHoraInicioVendaNaoSocio = PontoBr.Conversoes.Data.ConverterDataBancoParaDateTime(Evento.InicioVendaNaoSocio);
            txtDataVendaNaoSocio.Text = dataHoraInicioVendaNaoSocio.ToString("dd/MM/yyyy");
            dropHoraVendaNaoSocio.SelectedValue = dataHoraInicioVendaNaoSocio.ToString("HH:mm");
            dropLocal.SelectedValue = Evento.IDLocal.ToString();

            txtNumeroIngressoExtraSocioCota.Text = Evento.NumeroIngressoExtraSocioCota.ToString();
            txtNumeroIngressoExtraSocioTitulo.Text = Evento.NumeroIngressoExtraSocioTitulo.ToString();
            txtNumeroIngressosValorNaoSocio.Text = Evento.NumeroIngressosValorNaoSocio.ToString();
            txtNumeroIngressosCadeira.Text = Evento.NumeroIngressosCadeira.ToString();
            txtNumeroIngressosCamarote.Text = Evento.NumeroIngressosCamarote.ToString();
            txtTotalCadeiras.Text = Evento.IngressosCadeira.ToString();
            txtMinimoAniversariante.Text = Evento.MinimoAniversariante.ToString();

            txtDataLimite4ParcelaCota.Text = Evento.DataLimite4ParcelaCota.ToString();
            txtDataLimite3ParcelaCota.Text = Evento.DataLimite3ParcelaCota.ToString();
            txtDataLimite2ParcelaCota.Text = Evento.DataLimite2ParcelaCota.ToString();
            txtDataLimite1ParcelaCota.Text = Evento.DataLimite1ParcelaCota.ToString();

            txtDataLimite5ParcelaCredito.Text = Evento.DataLimite5ParcelaCredito.ToString();
            txtDataLimite4ParcelaCredito.Text = Evento.DataLimite4ParcelaCredito.ToString();
            txtDataLimite3ParcelaCredito.Text = Evento.DataLimite3ParcelaCredito.ToString();
            txtDataLimite2ParcelaCredito.Text = Evento.DataLimite2ParcelaCredito.ToString();
            txtDataLimite1ParcelaCredito.Text = Evento.DataLimite1ParcelaCredito.ToString();

            radStatus.SelectedValue = Evento.Ativo.ToString();
            radIngressos.SelectedValue = Evento.IDStatusIngresso.ToString();
            radVagaEstacionamento.SelectedValue = Evento.VagaEstacionamento.ToString();
            dropNumeroIngressoEstacionamento.SelectedValue = Evento.NumIngressoEstacionamento.ToString();
            txtVagasEstacionamento.Text = Evento.VagasEstacionamento.ToString();

            radPossuiMapa.SelectedValue = Evento.PossuiMapa.ToString();
            txtDetalhes.Text = Evento.Detalhes;
            txtDataRetirada.Text = Evento.DataRetirada;

            if (Evento.Evento == "Boate")
                DivAniversariante.Visible = true;
            else
                DivAniversariante.Visible = false;


            //Verifica se há 8 tipos de ingressos configurados
            string sMensagem = RetornarTipoIngressoInativo(iIDEdicao);
            if (!String.IsNullOrEmpty(sMensagem))
                lblMensagem.Text += sMensagem;
        }
    }

    private string RetornarTipoIngressoInativo(int iIDEdicao)
    {
        string sMensagem = String.Empty;

        //Verifica se há 8 tipos de ingressos configurados
        bool bInteiraCadeiraSocio = false;
        bool bInteiraCadeiraNaoSocio = false;
        bool bInteiraAvulsoSocio = false;
        bool bInteiraAvulsoNaoSocio = false;
        bool bMeiaCadeiraSocio = false;
        bool bMeiaCadeiraNaoSocio = false;
        bool bMeiaAvulsoSocio = false;
        bool bMeiaAvulsoNaoSocio = false;

        eventoCTL CEvento = new eventoCTL();
        DataTable dataTable = CEvento.RetornarLotes(iIDEdicao, false);
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
        }

        if (!bInteiraCadeiraSocio) sMensagem += "<br/>Tipo de ingresso [Inteira Sócio (cadeira)]";
        if (!bInteiraCadeiraNaoSocio) sMensagem += "<br/>Tipo de ingresso [Inteira Não sócio (cadeira)]";
        if (!bInteiraAvulsoSocio) sMensagem += "<br/>Tipo de ingresso [Inteira Sócio (sem cadeira)]";
        if (!bInteiraAvulsoNaoSocio) sMensagem += "<br/>Tipo de ingresso [Inteira Não sócio (sem cadeira)]";

        if (!bMeiaCadeiraSocio) sMensagem += "<br/>Tipo de ingresso [Meio Adolescente Sócio (cadeira)]";
        if (!bMeiaCadeiraNaoSocio) sMensagem += "<br/>Tipo de ingresso [Meio Adolescente Não sócio (cadeira)]";
        if (!bMeiaAvulsoSocio) sMensagem += "<br/>Tipo de ingresso [Meio Adolescente Sócio (sem cadeira)]";
        if (!bMeiaAvulsoNaoSocio) sMensagem += "<br/>Tipo de ingresso [Meio Adolescente Não sócio (sem cadeira)]";

        if (!String.IsNullOrEmpty(sMensagem))
            sMensagem = "<br/><br/>O(s) seguinte(s) tipo(s) de ingresso(s) não está(ão) ativo(s) em nenhum lote:<br/>" + sMensagem;

        return sMensagem;
    }


    protected void grdEvento_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
            string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
            string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
            string sIDEdicao = Criptografia.Criptografar(grdEvento.DataKeys[e.Row.RowIndex].Values[0].ToString(), sChave, sVetorInicializacao);

            for (int i = 0; i <= 13; i++)
            {
                e.Row.Cells[i].Attributes.Add("onclick", "ExibirLogEventos('" + sIDEdicao + "')");
            }

            e.Row.Cells[15].Attributes.Add("onclick", "AbrirEventos('" + sIDEdicao + "')");
        }
    }
}

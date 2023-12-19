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
using System.Drawing;

public partial class administrador_lote : App_Code.BaseWeb
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
            PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
            string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
            string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
            int iIDEdicao = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["id"], sChave, sVetorInicializacao));

            CarregarCampos(iIDEdicao);
            CarregarEdicao(iIDEdicao);
            CarregarLotes(iIDEdicao);
        }
    }

    private void CarregarCampos(int iIDEdicao)
    {
        eventoCTL CEvento = new eventoCTL();
        evento Evento = CEvento.RetornarEdicao(iIDEdicao);
        if (Evento.Evento == "Boate")
        {
            txtValorMeiaNaoSocioAvulso.Text = "0";
            txtValorMeiaNaoSocioCadeira.Text = "0";
            txtValorMeiaSocioAvulso.Text = "0";
            txtValorMeiaSocioCadeira.Text = "0";

            //divMeia.Visible = false;
        }
        //else
            //divMeia.Visible = true;
    }

    private void CarregarLotes(int iIDEdicao)
    {
        eventoCTL CEvento = new eventoCTL();
        CEvento.CarregarGridViewLotes(grdLote, iIDEdicao);

        lblNumeroLinhas.Text = "| " + grdLote.Rows.Count.ToString() + " registro(s) |";

        ///////////////////////////////////////////////////
        DataTable dataTable = CEvento.RetornarLotes(iIDEdicao, false);
        foreach (GridViewRow gridViewRow in grdLote.Rows)
        {
            foreach(DataRow dataRow in dataTable.Rows)
            {
                if (grdLote.DataKeys[gridViewRow.RowIndex].Value.ToString() == dataRow["IDLote"].ToString())
                {
                    gridViewRow.Cells[3].ForeColor = Convert.ToBoolean(dataRow["InteiraCadeiraSocio"]) ? Color.Green : Color.Red;
                    gridViewRow.Cells[4].ForeColor = Convert.ToBoolean(dataRow["InteiraCadeiraNaoSocio"]) ? Color.Green : Color.Red;
                    gridViewRow.Cells[5].ForeColor = Convert.ToBoolean(dataRow["InteiraAvulsoSocio"]) ? Color.Green : Color.Red;
                    gridViewRow.Cells[6].ForeColor = Convert.ToBoolean(dataRow["InteiraAvulsoNaoSocio"]) ? Color.Green : Color.Red;
                    gridViewRow.Cells[7].ForeColor = Convert.ToBoolean(dataRow["MeiaCadeiraSocio"]) ? Color.Green : Color.Red;
                    gridViewRow.Cells[8].ForeColor = Convert.ToBoolean(dataRow["MeiaCadeiraNaoSocio"]) ? Color.Green : Color.Red;
                    gridViewRow.Cells[9].ForeColor = Convert.ToBoolean(dataRow["MeiaAvulsoSocio"]) ? Color.Green : Color.Red;
                    gridViewRow.Cells[10].ForeColor = Convert.ToBoolean(dataRow["MeiaAvulsoNaoSocio"]) ? Color.Green : Color.Red;
                }
            }
        }
    }

    private void CarregarEdicao(int iIDEdicao)
    {
        evento Evento = new evento();
        eventoCTL CEvento = new eventoCTL();

        Evento = CEvento.RetornarEdicao(iIDEdicao);
        lblLote.Text = "Lote - " + Evento.Evento + " ("+Evento.Edicao+")";

        //Verifica se há 8 tipos de ingressos configurados
        string sMensagem = RetornarTipoIngressoInativo();
        if (!String.IsNullOrEmpty(sMensagem))
            lblMensagem.Text += sMensagem;
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

                PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
                int iIDEdicao = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["id"], sChave, sVetorInicializacao));

                Evento.IDEdicao = iIDEdicao;
                Evento.Lote = Convert.ToInt32(txtLote.Text);
                Evento.InicioVenda = PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(txtInicioVendas.Text) + " " + dropHoraInicial.SelectedValue;
                Evento.FimVenda = PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(txtFimVendas.Text) + " " + dropHoraFinal.SelectedValue;
                Evento.ValorInteiraCadeiraSocio = Convert.ToDouble(txtValorInteiraSocioCadeira.Text);
                Evento.ValorInteiraCadeiraNaoSocio = Convert.ToDouble(txtValorInteiraNaoSocioCadeira.Text);
                Evento.ValorInteiraAvulsoSocio = Convert.ToDouble(txtValorInteiraSocioAvulso.Text);
                Evento.ValorInteiraAvulsoNaoSocio = Convert.ToDouble(txtValorInteiraNaoSocioAvulso.Text);
                Evento.ValorMeiaCadeiraSocio = Convert.ToDouble(txtValorMeiaSocioCadeira.Text);
                Evento.ValorMeiaCadeiraNaoSocio = Convert.ToDouble(txtValorMeiaNaoSocioCadeira.Text);
                Evento.ValorMeiaAvulsoSocio = Convert.ToDouble(txtValorMeiaSocioAvulso.Text);
                Evento.ValorMeiaAvulsoNaoSocio = Convert.ToDouble(txtValorMeiaNaoSocioAvulso.Text);
                Evento.ValorIpanema = txtValorIpanema.Text == "" ? 0 : Convert.ToDouble(txtValorIpanema.Text);
                Evento.ValorGolodromo = txtValorGolodromo.Text == "" ? 0 : Convert.ToDouble(txtValorGolodromo.Text);
                Evento.ValorPortinari = txtValorPortinari.Text == "" ? 0 : Convert.ToDouble(txtValorPortinari.Text);
                Evento.ValorPergula = txtValorPergula.Text == "" ? 0 : Convert.ToDouble(txtValorPergula.Text);
                Evento.ValorSalaoDeFestas = txtValorSalaoDeFestas.Text == "" ? 0 : Convert.ToDouble(txtValorSalaoDeFestas.Text);
                Evento.ValorAcademia = txtValorAcademia.Text == "" ? 0 : Convert.ToDouble(txtValorAcademia.Text);
                Evento.InteiraCadeiraSocio = Convert.ToInt32(CheckInteiraSocioCadeira.Checked);
                Evento.InteiraCadeiraNaoSocio = Convert.ToInt32(CheckInteiraNaoSocioCadeira.Checked);
                Evento.InteiraAvulsoSocio = Convert.ToInt32(CheckInteiraSocioAvulso.Checked);
                Evento.InteiraAvulsoNaoSocio = Convert.ToInt32(CheckInteiraNaoSocioAvulso.Checked);
                Evento.MeiaCadeiraSocio = Convert.ToInt32(CheckMeiaSocioCadeira.Checked);
                Evento.MeiaCadeiraNaoSocio = Convert.ToInt32(CheckMeiaNaoSocioCadeira.Checked);
                Evento.MeiaAvulsoSocio = Convert.ToInt32(CheckMeiaSocioAvulso.Checked);
                Evento.MeiaAvulsoNaoSocio = Convert.ToInt32(CheckMeiaNaoSocioAvulso.Checked);
                Evento.CamaroteSocio = Convert.ToInt32(CheckCamaroteSocio.Checked);
                Evento.CamaroteNaoSocio = Convert.ToInt32(CheckCamaroteNaoSocio.Checked);
                Evento.ValorCamaroteSocio = txtValorCamaroteSocio.Text == "" ? 0 : Convert.ToDouble(txtValorCamaroteSocio.Text);
                Evento.ValorCamaroteNaoSocio = txtValorCamaroteNaoSocio.Text == "" ? 0 : Convert.ToDouble(txtValorCamaroteNaoSocio.Text);
                Evento.IngressosAvulsos = Convert.ToInt32(txtTotalAvulsos.Text);
                Evento.Ativo = Convert.ToInt32(radStatus.SelectedValue);

                string sMensagem = String.Empty;
                if (!String.IsNullOrEmpty(hddIdLote.Value))
                {
                    usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

                    Evento.IDLote = Convert.ToInt32(hddIdLote.Value);
                    Evento.IDUsuarioAlteracao = UsuarioLogado.IDUsuario;

                    RegistrarLogAlteracoes(Evento);

                    //Atualizar Lote
                    CEvento.AtualizarLote(Evento);
                    sMensagem = "Lote atualizado com sucesso!";
                }
                else
                {
                    usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

                    Evento.IDUsuarioCadastro = UsuarioLogado.IDUsuario;

                    //Atualizar Lote
                    CEvento.CadastrarLote(Evento);
                    sMensagem = "Lote cadastrado com sucesso!";
                }

                LimparFormulario();
                lblMensagem.Text = sMensagem;

                //Verifica se há 8 tipos de ingressos configurados
                sMensagem = RetornarTipoIngressoInativo();
                if (!String.IsNullOrEmpty(sMensagem))
                    lblMensagem.Text += sMensagem;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Erro: " + ex.Message;
        }
    }

    private string RetornarTipoIngressoInativo()
    {
        string sMensagem = String.Empty;

        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        int iIDEdicao = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["id"], sChave, sVetorInicializacao));

        //Verifica se há 8 tipos de ingressos configurados
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

            if (Convert.ToBoolean(dataRow["CamaroteSocio"])) bCamaroteSocio = true;
            if (Convert.ToBoolean(dataRow["CamaroteNaoSocio"])) bCamaroteNaoSocio = true;
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

    private void LimparFormulario()
    {
        hddIdLote.Value = null;

        txtLote.Text = String.Empty;
        txtInicioVendas.Text = String.Empty;
        txtFimVendas.Text = String.Empty;
        dropHoraInicial.SelectedValue = "00:00";
        dropHoraFinal.SelectedValue = "00:00";
        txtTotalAvulsos.Text = String.Empty;

        txtValorInteiraSocioCadeira.Text = String.Empty;
        txtValorInteiraNaoSocioCadeira.Text = String.Empty;
        txtValorInteiraSocioAvulso.Text = String.Empty;
        txtValorInteiraNaoSocioAvulso.Text = String.Empty;
        txtValorMeiaSocioCadeira.Text = String.Empty;
        txtValorMeiaNaoSocioCadeira.Text = String.Empty;
        txtValorMeiaSocioAvulso.Text = String.Empty;
        txtValorMeiaNaoSocioAvulso.Text = String.Empty;
        txtValorCamaroteSocio.Text = String.Empty;
        txtValorCamaroteNaoSocio.Text = String.Empty;

        txtValorIpanema.Text = String.Empty;
        txtValorGolodromo.Text = String.Empty;
        txtValorPortinari.Text = String.Empty;
        txtValorPergula.Text = String.Empty;
        txtValorSalaoDeFestas.Text = String.Empty;
        txtValorAcademia.Text = String.Empty;

        CheckInteiraSocioCadeira.Checked = false;
        CheckInteiraNaoSocioCadeira.Checked = false;
        CheckInteiraSocioAvulso.Checked = false;
        CheckInteiraNaoSocioAvulso.Checked = false;
        CheckMeiaSocioCadeira.Checked = false;
        CheckMeiaNaoSocioCadeira.Checked = false;
        CheckMeiaSocioAvulso.Checked = false;
        CheckMeiaNaoSocioAvulso.Checked = false;
        CheckCamaroteSocio.Checked = false;
        CheckCamaroteNaoSocio.Checked = false;

        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        int iIDEdicao = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["id"], sChave, sVetorInicializacao));
        CarregarLotes(iIDEdicao);

        lblMensagem.Text = "";
    }

    private void RegistrarLogAlteracoes(evento EventoAlterado)
    {
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        int iIDLote = Convert.ToInt32(hddIdLote.Value);
        evento EventoAtual = new evento();
        eventoCTL CEvento = new eventoCTL();

        EventoAtual = CEvento.RetornarLote(iIDLote);

        string sTextoLog;
        if (EventoAtual.Ativo != EventoAlterado.Ativo)
        {
            string sStatusAtual = EventoAtual.Ativo == 1 ? "Ativo" : "Inativo";
            string sStatusAlterado = EventoAlterado.Ativo == 1 ? "Ativo" : "Inativo";

            sTextoLog = "Status alterado de [" + sStatusAtual + "] para [" + sStatusAlterado + "]; lote " + EventoAtual.Lote + "";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }       
        if (EventoAtual.InicioVenda != txtInicioVendas.Text + " " + dropHoraInicial.SelectedValue + ":00")
        {
            sTextoLog = "Data de início das vendas alterada de [" + EventoAtual.InicioVenda + "] para [" + txtInicioVendas.Text + " " + dropHoraInicial.SelectedValue + ":00" + "]; lote " + EventoAtual.Lote + "";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.FimVenda != txtFimVendas.Text + " " + dropHoraFinal.SelectedValue + ":00")
        {
            sTextoLog = "Data de fim das vendas alterada de [" + EventoAtual.FimVenda + "] para [" + txtFimVendas.Text + " " + dropHoraFinal.SelectedValue + ":00" + "]; lote " + EventoAtual.Lote + "";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.Lote != EventoAlterado.Lote)
        {
            sTextoLog = "Lote alterado de [" + EventoAtual.Lote + "] para [" + EventoAlterado.Lote + "]; lote " + EventoAtual.Lote + "";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.IngressosAvulsos != EventoAlterado.IngressosAvulsos)
        {
            sTextoLog = "Núm. de ingressos avulsos alterado de [" + EventoAtual.IngressosAvulsos + "] para [" + EventoAlterado.IngressosAvulsos + "]; lote " + EventoAtual.Lote + "";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.ValorInteiraCadeiraSocio != EventoAlterado.ValorInteiraCadeiraSocio)
        {
            sTextoLog = "Valor Inteira Sócio (cadeira) alterado de [" + EventoAtual.ValorInteiraCadeiraSocio + "] para [" + EventoAlterado.ValorInteiraCadeiraSocio + "]; lote " + EventoAtual.Lote + "";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.ValorInteiraCadeiraNaoSocio != EventoAlterado.ValorInteiraCadeiraNaoSocio)
        {
            sTextoLog = "Valor Inteira Não sócio (cadeira) alterado de [" + EventoAtual.ValorInteiraCadeiraNaoSocio + "] para [" + EventoAlterado.ValorInteiraCadeiraNaoSocio + "]; lote " + EventoAtual.Lote + "";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.ValorInteiraAvulsoSocio != EventoAlterado.ValorInteiraAvulsoSocio)
        {
            sTextoLog = "Valor Inteira Sócio (sem cadeira) alterado de [" + EventoAtual.ValorInteiraAvulsoSocio + "] para [" + EventoAlterado.ValorInteiraAvulsoSocio + "]; lote " + EventoAtual.Lote + "";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.ValorInteiraAvulsoNaoSocio != EventoAlterado.ValorInteiraAvulsoNaoSocio)
        {
            sTextoLog = "Valor Inteira Não sócio (sem cadeira) alterado de [" + EventoAtual.ValorInteiraAvulsoNaoSocio + "] para [" + EventoAlterado.ValorInteiraAvulsoNaoSocio + "]; lote " + EventoAtual.Lote + "";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.ValorMeiaCadeiraSocio != EventoAlterado.ValorMeiaCadeiraSocio)
        {
            sTextoLog = "Valor Meio Adolescente Sócio (cadeira) alterado de [" + EventoAtual.ValorMeiaCadeiraSocio + "] para [" + EventoAlterado.ValorMeiaCadeiraSocio + "]; lote " + EventoAtual.Lote + "";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.ValorMeiaCadeiraNaoSocio != EventoAlterado.ValorMeiaCadeiraNaoSocio)
        {
            sTextoLog = "Valor Meio Adolescente Não sócio (cadeira) alterado de [" + EventoAtual.ValorMeiaCadeiraNaoSocio + "] para [" + EventoAlterado.ValorMeiaCadeiraNaoSocio + "]; lote " + EventoAtual.Lote + "";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.ValorMeiaAvulsoSocio != EventoAlterado.ValorMeiaAvulsoSocio)
        {
            sTextoLog = "Valor Meio Adolescente Sócio (sem cadeira) alterado de [" + EventoAtual.ValorMeiaAvulsoSocio + "] para [" + EventoAlterado.ValorMeiaAvulsoSocio + "]; lote " + EventoAtual.Lote + "";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }
        if (EventoAtual.ValorMeiaAvulsoNaoSocio != EventoAlterado.ValorMeiaAvulsoNaoSocio)
        {
            sTextoLog = "Valor Meio Adolescente Não sócio (sem cadeira) alterado de [" + EventoAtual.ValorMeiaAvulsoNaoSocio + "] para [" + EventoAlterado.ValorMeiaAvulsoNaoSocio + "]; lote " + EventoAtual.Lote + "";
            CEvento.CadastrarLogEdicao(EventoAtual.IDEdicao, sTextoLog, UsuarioLogado.IDUsuario);
        }        
    }

    private bool PodeSalvar()
    {
        string sMensagem;
        lblMensagem.Text = String.Empty;

        if (string.IsNullOrEmpty(txtLote.Text.Trim()))
        {
            sMensagem = "Preencha [Lote].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        int iIDEdicao = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["id"], sChave, sVetorInicializacao));

        int iIDLote = -1;
        eventoCTL CEvento = new eventoCTL();
        if (!String.IsNullOrEmpty(hddIdLote.Value)) iIDLote = Convert.ToInt32(hddIdLote.Value);
        if (CEvento.VerificarExistenciaLote(iIDLote, iIDEdicao, Convert.ToInt32(txtLote.Text.Trim())))
        {
            sMensagem = "[Lote] já está cadastrado.";
            lblMensagem.Text = sMensagem;
            return false;
        } 

        if (string.IsNullOrEmpty(txtInicioVendas.Text))
        {
            sMensagem = "Preencha [Período de Venda].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (!PontoBr.Conversoes.Data.ValidarDataBr(txtInicioVendas.Text))
        {
            sMensagem = "[Período de Venda] inválido.";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtFimVendas.Text))
        {
            sMensagem = "Preencha [Período de Venda].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (!PontoBr.Conversoes.Data.ValidarDataBr(txtFimVendas.Text))
        {
            sMensagem = "[Período de Venda] inválido.";
            lblMensagem.Text = sMensagem;
            return false;
        }        
        if (string.IsNullOrEmpty(txtTotalAvulsos.Text.Trim()))
        {
            sMensagem = "Preencha [Total de ingressos tipo sem cadeira].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (string.IsNullOrEmpty(txtValorInteiraSocioCadeira.Text.Trim()))
        {
            sMensagem = "Preencha valor [Inteira Sócio (cadeira)].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtValorInteiraNaoSocioCadeira.Text.Trim()))
        {
            sMensagem = "Preencha valor [Inteira Não sócio (cadeira)].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtValorInteiraSocioAvulso.Text.Trim()))
        {
            sMensagem = "Preencha valor [Inteira Sócio (sem cadeira)].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtValorInteiraNaoSocioAvulso.Text.Trim()))
        {
            sMensagem = "Preencha valor [Inteira Não sócio (sem cadeira)].";
            lblMensagem.Text = sMensagem;
            return false;
        }

        if (string.IsNullOrEmpty(txtValorMeiaSocioCadeira.Text.Trim())) 
        {
            sMensagem = "Preencha valor [Meio Adolescente Sócio (cadeira)].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtValorMeiaNaoSocioCadeira.Text.Trim())) 
        {
            sMensagem = "Preencha valor [Meio Adolescente Não sócio (cadeira)].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtValorMeiaSocioAvulso.Text.Trim())) 
        {
            sMensagem = "Preencha valor [Meio Adolescente Sócio (sem cadeira)].";
            lblMensagem.Text = sMensagem;
            return false;
        }
        if (string.IsNullOrEmpty(txtValorMeiaNaoSocioAvulso.Text.Trim())) 
        {
            sMensagem = "Preencha valor [Meio Adolescente Não sócio (sem cadeira)].";
            lblMensagem.Text = sMensagem;
        }
        return true;
    }

    protected void grdLote_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
        {
            LimparFormulario();

            int iIDLote = Convert.ToInt32(grdLote.DataKeys[int.Parse((string)e.CommandArgument)]["IDLote"].ToString());
            evento Evento = new evento();
            eventoCTL CEvento = new eventoCTL();

            Evento = CEvento.RetornarLote(iIDLote);

            hddIdLote.Value = iIDLote.ToString();
            txtLote.Text = Evento.Lote.ToString();

            txtInicioVendas.Text = Evento.DataInicioVenda;
            txtFimVendas.Text = Evento.DataFimVenda;
            dropHoraInicial.SelectedValue = Evento.HoraInicioVenda;
            dropHoraFinal.SelectedValue = Evento.HoraFimVenda;
            txtValorInteiraSocioCadeira.Text = Evento.ValorInteiraCadeiraSocio.ToString();
            txtValorInteiraNaoSocioCadeira.Text = Evento.ValorInteiraCadeiraNaoSocio.ToString();
            txtValorInteiraSocioAvulso.Text = Evento.ValorInteiraAvulsoSocio.ToString();
            txtValorInteiraNaoSocioAvulso.Text = Evento.ValorInteiraAvulsoNaoSocio.ToString();
            txtValorMeiaSocioCadeira.Text = Evento.ValorMeiaCadeiraSocio.ToString();
            txtValorMeiaNaoSocioCadeira.Text = Evento.ValorMeiaCadeiraNaoSocio.ToString();
            txtValorMeiaSocioAvulso.Text = Evento.ValorMeiaAvulsoSocio.ToString();
            txtValorMeiaNaoSocioAvulso.Text = Evento.ValorMeiaAvulsoNaoSocio.ToString();
            txtValorIpanema.Text = Evento.ValorIpanema.ToString();
            txtValorGolodromo.Text = Evento.ValorGolodromo.ToString();
            txtValorPortinari.Text = Evento.ValorPortinari.ToString();
            txtValorPergula.Text = Evento.ValorPergula.ToString();
            txtValorSalaoDeFestas.Text = Evento.ValorSalaoDeFestas.ToString();
            txtValorAcademia.Text = Evento.ValorAcademia.ToString();
            CheckInteiraSocioCadeira.Checked = Convert.ToBoolean(Evento.InteiraCadeiraSocio);
            CheckInteiraNaoSocioCadeira.Checked = Convert.ToBoolean(Evento.InteiraCadeiraNaoSocio);
            CheckInteiraSocioAvulso.Checked = Convert.ToBoolean(Evento.InteiraAvulsoSocio);
            CheckInteiraNaoSocioAvulso.Checked = Convert.ToBoolean(Evento.InteiraAvulsoNaoSocio);
            CheckMeiaSocioCadeira.Checked = Convert.ToBoolean(Evento.MeiaCadeiraSocio);
            CheckMeiaNaoSocioCadeira.Checked = Convert.ToBoolean(Evento.MeiaCadeiraNaoSocio);
            CheckMeiaSocioAvulso.Checked = Convert.ToBoolean(Evento.MeiaAvulsoSocio);
            CheckMeiaNaoSocioAvulso.Checked = Convert.ToBoolean(Evento.MeiaAvulsoNaoSocio);
            CheckCamaroteSocio.Checked = Convert.ToBoolean(Evento.CamaroteSocio);
            CheckCamaroteNaoSocio.Checked = Convert.ToBoolean(Evento.CamaroteNaoSocio);
            txtValorCamaroteSocio.Text = Evento.ValorCamaroteSocio.ToString();
            txtValorCamaroteNaoSocio.Text = Evento.ValorCamaroteNaoSocio.ToString();

            radStatus.SelectedValue = Evento.Ativo.ToString();

            txtTotalAvulsos.Text = Evento.IngressosAvulsos.ToString();
            CarregarCampos(Evento.IDEdicao);
        }
    }    
}

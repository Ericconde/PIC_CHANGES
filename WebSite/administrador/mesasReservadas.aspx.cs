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

public partial class administrador_mesasReservadas : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, false, false, false, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        txtNome.Focus();

        if (!IsPostBack)
        {
            CarregarGridViewVendasReservadas();
            HttpContext.Current.Session["Venda"] = null;
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
        else if (UsuarioLogado.Perfil == "Contabilidade")
        {
            MasterPageFile = "~/controls/Contabilidade.master";
        }
    }

    private void CarregarGridViewVendasReservadas()
    {
        string sNome = PontoBr.Utilidades.String.TirarAcentos(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNome.Text.Trim().ToUpper()));
        string sCPF = PontoBr.Utilidades.String.RemoverCaracteresMascara(txtCPF.Text);
        string sEmail = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtEmail.Text.ToLower().Trim());
        
        int iNumeroCota = 0;
        if (!String.IsNullOrEmpty(txtNumCota.Text.Trim()))
            iNumeroCota = Convert.ToInt32(txtNumCota.Text);

        vendaCTL CVenda = new vendaCTL();
        CVenda.CarregarGridViewVendasReservadas(grdVenda, sNome, sCPF, sEmail, iNumeroCota);

        lblNumeroLinhas.Text = "| " + grdVenda.Rows.Count.ToString() + " registro(s) |";
    }

    protected void cmdCancelar_Click(object sender, EventArgs e)
    {
        LimparCampos();
        CarregarGridViewVendasReservadas();
    }

    private void LimparCampos()
    {
        txtNome.Text = String.Empty;
        txtEmail.Text = String.Empty;
        txtCPF.Text = String.Empty;
        txtNumCota.Text = String.Empty;
    }

    protected void cmdPesquisar_Click(object sender, EventArgs e)
    {
        CarregarGridViewVendasReservadas();
    }

    protected void grdVenda_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Selecionar")
        {
            int iIDVenda = Convert.ToInt32(grdVenda.DataKeys[int.Parse((string)e.CommandArgument)]["IDVenda"].ToString());
            int iIDUsuario = Convert.ToInt32(grdVenda.DataKeys[int.Parse((string)e.CommandArgument)]["IDUsuario"].ToString());

            usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

            usuarioCTL CUsuario = new usuarioCTL();
            usuario Usuario = new usuario();
            Usuario = CUsuario.RetornarUsuario(iIDUsuario);

            HttpContext.Current.Session["ClienteCompra"] = Usuario;

            /////////////////////////////////////////////////////////////////
            vendaCTL CVenda = new vendaCTL();
            venda Venda = new venda();

            Venda = CVenda.RetornarVenda(iIDVenda, null);

            evento Evento = new evento();
            eventoCTL CEvento = new eventoCTL();
            Evento = CEvento.RetornarEdicao(Venda.IDEdicao);

            Venda.IDEdicao = Convert.ToInt32(Venda.IDEdicao);
            Venda.Evento = Evento.Evento;

            Venda.IDUsuarioCadastro = UsuarioLogado.IDUsuario;
            Venda.IP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            Venda.NumeroIngressoExtraSocioCota = Evento.NumeroIngressoExtraSocioCota;
            Venda.NumeroIngressoExtraSocioTitulo = Evento.NumeroIngressoExtraSocioTitulo;
            Venda.NumeroIngressosValorNaoSocio = Evento.NumeroIngressosValorNaoSocio;
            Venda.NumeroIngressosCadeira = Evento.NumeroIngressosCadeira;
            Venda.Declaracao = true;

            DataTable dataTable = CEvento.RetornarLotes(Venda.IDEdicao, false);

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
                }
            }

            HttpContext.Current.Session["Venda"] = Venda;

            Response.Redirect("~/cliente/compra.aspx");
        }
    }

    protected void grdVenda_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int iIDVenda = Convert.ToInt32(grdVenda.DataKeys[e.Row.RowIndex].Values[0].ToString());
            
            vendaCTL CVenda = new vendaCTL();
            DataTable dataTable = CVenda.RetornarMesasReservadas(iIDVenda);
            string sMesas = "";

            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (!String.IsNullOrEmpty(sMesas)) sMesas += "<br/>";
                sMesas += dataRow["Setor"].ToString() + ": mesa " + dataRow["Mesa"].ToString();
            }
            e.Row.Cells[6].Text = sMesas;
        }
    }
}

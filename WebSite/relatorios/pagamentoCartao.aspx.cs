using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Model.objetos;
using Controller;

public partial class relatorios_pagamentoCartao : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, true, false, false, false, false, true, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            CarregarEventosEdicao();
            CarregarRelatorio();
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
        else if (UsuarioLogado.Perfil == "Financeiro")
        {
            MasterPageFile = "~/controls/Financeiro.master";
        }
    }

    private void CarregarEventosEdicao()
    {
        eventoCTL evento = new eventoCTL();
        evento.CarregarDropDownListEventosVendaAdministrador(dropEdicao, true, false);
    }

    private DataTable CarregarRelatorio()
    {
        DataTable dataTable = null;
        try
        {
            string sCodigoAutorizacao = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtCodigoAutorizacao.Text.Trim());
            string sTid = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtTid.Text.Trim());
            string sNomeCliente = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNome.Text.Trim());
            string sTitular = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtTitular.Text.Trim());
            string sNumeroCartao = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNumeroCartao.Text.Trim());
            int iNumeroCota = String.IsNullOrEmpty(txtNumCota.Text.Trim()) ? -1 : Convert.ToInt32(txtNumCota.Text.Trim());
            int iIDVenda = String.IsNullOrEmpty(txtVoucher.Text.Trim()) ? -1 : Convert.ToInt32(txtVoucher.Text.Trim());
            string sDataInicial = txtDataInicial.Text.Trim() == "" ? "1900/01/01" : PontoBr.Conversoes.Data.ConverterDataDMMAAAAComBarraParaFormatoBancoAAAAMMDD(txtDataInicial.Text);
            string sDataFinal = txtDataFinal.Text.Trim() == "" ? "2050/01/01" : PontoBr.Conversoes.Data.ConverterDataDMMAAAAComBarraParaFormatoBancoAAAAMMDD(txtDataFinal.Text) + " 23:59:59";
            int iIDEdicao = Convert.ToInt32(dropEdicao.SelectedValue) ;

            relatorioCTL CRelatorio = new relatorioCTL();
            dataTable = CRelatorio.RetornarPagamentosCartao(iIDVenda, sNumeroCartao, sCodigoAutorizacao, sTid, sNomeCliente, iNumeroCota, sTitular, iIDEdicao, sDataInicial, sDataFinal);

            grdRelatorio.DataSource = dataTable;
            grdRelatorio.DataBind();

            lblNumeroRegistros.Text = "| " + dataTable.Rows.Count.ToString() + " registro(s) |";
        }
        catch (Exception ex)
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager(ex.Message, this.Page);
        }
        return dataTable;
    }

    protected void cmdVoltar_Click(object sender, EventArgs e)
    {
        usuario UsuarioLogado = (usuario)Session["usuario"];

        if (UsuarioLogado.Perfil == "Administrador")
            Response.Redirect("../administrador/default.aspx");
        else if (UsuarioLogado.Perfil == "Vendedor")
            Response.Redirect("../vendedor/default.aspx");
        else if (UsuarioLogado.Perfil == "Contabilidade")
            Response.Redirect("../Contabilidade/Default.aspx");
        else if (UsuarioLogado.Perfil == "Financeiro")
            Response.Redirect("../financeiro/default.aspx");
    }
    
    protected void cmdPesquisar_Click(object sender, EventArgs e)
    {
        CarregarRelatorio();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    private void ExportarExcel(DataTable dataTable, string sNomeArquivo)
    {
        if (dataTable.Rows.Count > 0)
        {
            Response.Clear();
            sNomeArquivo += "_" + DateTime.Now.Ticks.ToString() + ".xls";

            Response.AddHeader("content-disposition", "attachment;filename=" + sNomeArquivo + "");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter sWr = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hWr = new HtmlTextWriter(sWr);

            GridView gridExcel = new GridView();
            gridExcel.DataSource = dataTable;
            gridExcel.DataBind();

            gridExcel.RenderControl(hWr);
            Response.Write(sWr.ToString());
            Response.End();
        }
    }

    protected void cmdExportarExcel_Click(object sender, EventArgs e)
    {
        DataTable dataTable = CarregarRelatorio();
        ExportarExcel(dataTable, "PagamentosCartao");
    }

    protected void grdRelatorio_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdRelatorio.PageIndex = e.NewPageIndex;
        CarregarRelatorio();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Model.objetos;
using Controller;
using CrystalDecisions.CrystalReports.Engine;

public partial class relatorios_vendasDetalhado : System.Web.UI.Page
{
    private ReportDocument reportDocument;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, true, false, false, true, false, true, false, true, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            CarregarEventosEdicao();
            CarregarFormasPagamento();
        }

        if (reportDocument == null && PodePesquisar(false))
            CarregarRelatorio();
        else
            IngressosPIC.ReportSource = null;
    }

    private void CarregarEventosEdicao()
    {
        eventoCTL evento = new eventoCTL();
        evento.CarregarDropDownListEventosVendaAdministrador(dropEdicao, false, true);
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
        else if (UsuarioLogado.Perfil == "Fisco")
        {
            MasterPageFile = "~/controls/Fisco.master";
        }
    }

    private void CarregarFormasPagamento()
    {
        vendaCTL CVenda = new vendaCTL();
        CVenda.CarregarDropDownListFormasPagamento(dropFormaPagamento);
    }

    private void CarregarRelatorio()
    {
        try
        {
            int iIDEdicao = Convert.ToInt32(dropEdicao.SelectedValue);
            int iLote = Convert.ToInt32(dropLote.SelectedValue);
            string sDataInicial = txtDataInicial.Text.Trim() == "" ? "1900/01/01" : PontoBr.Conversoes.Data.ConverterDataDMMAAAAComBarraParaFormatoBancoAAAAMMDD(txtDataInicial.Text);
            string sDataFinal = txtDataFinal.Text.Trim() == "" ? "2050/01/01" : PontoBr.Conversoes.Data.ConverterDataDMMAAAAComBarraParaFormatoBancoAAAAMMDD(txtDataFinal.Text) + " 23:59:59";
            int iIDFormaPagamento = Convert.ToInt32(dropFormaPagamento.SelectedValue);
            int iNumeroCota = String.IsNullOrEmpty(txtNumCota.Text.Trim()) ? -1 : Convert.ToInt32(txtNumCota.Text);

            string sIDPerfil = "";
            string sPerfil = "";
            foreach (ListItem listItem in chkCliente.Items)
            {
                if (listItem.Selected == true)
                {
                    if (sIDPerfil != "") sIDPerfil += ", ";
                    sIDPerfil += listItem.Value;

                    if (sPerfil != "") sPerfil += ", ";
                    sPerfil += listItem.Text;
                }
            }

            relatorioCTL CRelatorio = new relatorioCTL();
            DataTable dataTable = CRelatorio.RetornarVendasDetalhado(sDataInicial, sDataFinal, iIDEdicao, iLote, sIDPerfil, iIDFormaPagamento, iNumeroCota);

            reportDocument = new ReportDocument();
            reportDocument.Load(Server.MapPath("~/relatorios/vendasDetalhado.rpt"));
            reportDocument.SetDataSource(dataTable);

            sDataInicial = txtDataInicial.Text == "" ? "-" : txtDataInicial.Text;
            sDataFinal = txtDataFinal.Text == "" ? "-" : txtDataFinal.Text;

            string sFiltro = "Filtro: ";
            sFiltro += "Evento " + dropEdicao.SelectedItem;
            sFiltro += "; Lote " + dropLote.SelectedItem;
            sFiltro += "; Forma de Pagamento " + dropFormaPagamento.SelectedItem;
            sFiltro += "; Cliente " + sPerfil;
            sFiltro += "; Data Inicial " + sDataInicial;
            sFiltro += "; Data Final " + sDataFinal;

            PontoBr.Utilidades.Crystal.AtribuirParameterField(reportDocument.DataDefinition.ParameterFields, "Filtro", sFiltro);

            IngressosPIC.ReportSource = reportDocument; IngressosPIC.ShowLastPage(); IngressosPIC.ShowFirstPage();
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
        }
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
        if (PodePesquisar(true))
        {
            CarregarRelatorio();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    private bool PodePesquisar(bool bExibirMensagem)
    {
        lblMensagem.Text = String.Empty;

        if (dropEdicao.SelectedValue == "-1")
        {
            if (bExibirMensagem) lblMensagem.Text = "Selecione [Evento].";
            return false;
        }

        if (!String.IsNullOrEmpty(txtDataInicial.Text.Trim())
            || !String.IsNullOrEmpty(txtDataFinal.Text.Trim()))
        {
            if (PontoBr.Conversoes.Data.ValidarDataBr(txtDataInicial.Text) == false)
            {
                if (bExibirMensagem) lblMensagem.Text = "[Data Inicial] inválida.";
                return false;
            }
            if (PontoBr.Conversoes.Data.ValidarDataBr(txtDataFinal.Text) == false)
            {
                if (bExibirMensagem) lblMensagem.Text = "[Data Final] inválida.";
                return false;
            }
        }

        if (dropLote.Items.Count == 0) return false;

        return true;
    }

    private bool PodeExportar(bool bExibirMensagem)
    {
        lblMensagem.Text = String.Empty;

        if (dropEdicao.SelectedValue == "-1")
        {
            if (bExibirMensagem) lblMensagem.Text = "Selecione [Evento].";
            return false;
        }

        if (!String.IsNullOrEmpty(txtDataInicial.Text.Trim())
            || !String.IsNullOrEmpty(txtDataFinal.Text.Trim()))
        {
            if (PontoBr.Conversoes.Data.ValidarDataBr(txtDataInicial.Text) == false)
            {
                if (bExibirMensagem) lblMensagem.Text = "[Data Inicial] inválida.";
                return false;
            }
            if (PontoBr.Conversoes.Data.ValidarDataBr(txtDataFinal.Text) == false)
            {
                if (bExibirMensagem) lblMensagem.Text = "[Data Final] inválida.";
                return false;
            }
        }

        if (dropLote.Items.Count == 0) return false;

        return true;
    }

    protected void dropEdicao_SelectedIndexChanged(object sender, EventArgs e)
    {
        eventoCTL CEvento = new eventoCTL();
        CEvento.CarregarDropDownListLotes(dropLote, Convert.ToInt32(dropEdicao.SelectedValue), false);
    }

    protected void cmdExportarExcel_Click(object sender, EventArgs e)
    {
        if (PodeExportar(true))
        {
            DataTable dataTable = CarregarRelatorioVendaDetalhado();
            ExportarExcel(dataTable, "VendasDetalhado");
        }
    }

    private DataTable CarregarRelatorioVendaDetalhado()
    {
        DataTable dataTable = null;
        try
        {
            int iIDEdicao = Convert.ToInt32(dropEdicao.SelectedValue);
            int iIDFormaPagamento = Convert.ToInt32(dropFormaPagamento.SelectedValue);
            int iIDLote = Convert.ToInt32(dropLote.SelectedValue);
            int iIDSocio = Convert.ToInt32(chkCliente.SelectedValue);

            string sDataInicial = txtDataInicial.Text.Trim() == "" ? "1900/01/01" : PontoBr.Conversoes.Data.ConverterDataDMMAAAAComBarraParaFormatoBancoAAAAMMDD(txtDataInicial.Text);
            string sDataFinal = txtDataFinal.Text.Trim() == "" ? "2050/01/01" : PontoBr.Conversoes.Data.ConverterDataDMMAAAAComBarraParaFormatoBancoAAAAMMDD(txtDataFinal.Text) + " 23:59:59";

            string sIDPerfil = "";
            string sPerfil = "";
            foreach (ListItem listItem in chkCliente.Items)
            {
                if (listItem.Selected == true)
                {
                    if (sIDPerfil != "") sIDPerfil += ", ";
                    sIDPerfil += listItem.Value;

                    if (sPerfil != "") sPerfil += ", ";
                    sPerfil += listItem.Text;
                }
            }

            relatorioCTL CRelatorio = new relatorioCTL();
            dataTable = CRelatorio.RetornarRelatorioVendasDetalhado(iIDEdicao, sIDPerfil, iIDFormaPagamento, iIDLote, sDataInicial, sDataFinal);
            dataTable.Columns.RemoveAt(14);
        }
        catch (Exception ex)
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager(ex.Message, this.Page);
        }
        return dataTable;
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

            //Não remove o zero a esquerda e deixa a coluna como texto
            foreach (GridViewRow gridViewRow in gridExcel.Rows)
            {
                gridViewRow.Cells[3].Attributes["style"] = "mso-number-format:\\@";
                gridViewRow.Cells[4].Attributes["style"] = "mso-number-format:\\@";
            }

            gridExcel.RenderControl(hWr);
            Response.Write(sWr.ToString());
            Response.End();
        }
    }
}

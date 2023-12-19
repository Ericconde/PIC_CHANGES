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

public partial class relatorios_lotes_fisco : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(false, false, false, false, false, false, false, false, true, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            CarregarEventosEdicao();
        }
    }

    private void CarregarEventosEdicao()
    {
        eventoCTL evento = new eventoCTL();
        evento.CarregarDropDownListEventosVendaAdministrador(dropEdicao, false, true);
    }

    protected void cmdVoltar_Click(object sender, EventArgs e)
    {
        usuario UsuarioLogado = (usuario)Session["usuario"];

        if (UsuarioLogado.Perfil == "Administrador")
            Response.Redirect("../administrador/default.aspx");
        else if (UsuarioLogado.Perfil == "Vendedor")
            Response.Redirect("../vendedor/default.aspx");
        else if (UsuarioLogado.Perfil == "Contabilidade")
            Response.Redirect("../Contabilidade/default.aspx");
        else if (UsuarioLogado.Perfil == "Financeiro")
            Response.Redirect("../financeiro/default.aspx");
        else if (UsuarioLogado.Perfil == "Fisco")
            Response.Redirect("../Fisco/Default.aspx");
    }

    protected void cmdPesquisar_Click(object sender, EventArgs e)
    {
        if (PodePesquisar(true))
        {
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
        else if (UsuarioLogado.Perfil == "Fisco")
        {
            MasterPageFile = "~/controls/Fisco.master";
        }
    }

    private GridView CarregarRelatorio()
    {
        try
        {
            int iIDEdicao = Convert.ToInt32(dropEdicao.SelectedValue);
            eventoCTL CEvento = new eventoCTL();
            CEvento.CarregarGridViewLotes(grdLote, iIDEdicao);

            lblNumeroRegistros.Text = "| " + grdLote.Rows.Count.ToString() + " registro(s) |"; 
        }
        catch (Exception ex)
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager(ex.Message, this.Page);
        }
        return grdLote;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void cmdExportarExcel_Click(object sender, EventArgs e)
    {
        ExportarExcel(CarregarRelatorio(), "LotesFisco");
    }

    private void ExportarExcel(GridView gridExcel, string sNomeArquivo)
    {
        Response.Clear();
        sNomeArquivo += "_" + DateTime.Now.Ticks.ToString() + ".xls";

        Response.AddHeader("content-disposition", "attachment;filename=" + sNomeArquivo + "");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter sWr = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hWr = new HtmlTextWriter(sWr);

        gridExcel.RenderControl(hWr);
        Response.Write(sWr.ToString());
        Response.End();
    }

    private bool PodePesquisar(bool bExibirMensagem)
    {
        lblMensagem.Text = String.Empty;

        if (dropEdicao.SelectedValue == "-1")
        {
            if (bExibirMensagem) lblMensagem.Text = "Selecione [Evento].";
            return false;
        }

        return true;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Model.objetos;
using Controller;
using System.IO;
using System.Configuration;

public partial class administrador_baseSocios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, false, false, false, false, false, true, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
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
        else if (UsuarioLogado.Perfil == "Financeiro")
        {
            MasterPageFile = "~/controls/Financeiro.master";
        }
        else if (UsuarioLogado.Perfil == "Clube")
        {
            MasterPageFile = "~/controls/Clube.master";
        }
    }

    private void AtualizarBaseSocios()
    {
        try
        {
            //string sEnderecoPasta = "~/@ArquivoSocios/Vendas_On_Line/";
            string sEnderecoPasta = ConfigurationManager.AppSettings["EnderecoArquivoSocios"].ToString(); ;

            if (fileDocumento.HasFile)
             {
                //fileDocumento.SaveAs(MapPath(@sEnderecoPasta) + fileDocumento.FileName);
                fileDocumento.SaveAs(sEnderecoPasta + fileDocumento.FileName);

                socioCTL CSocio = new socioCTL();
                 CSocio.AtualizarBaseSocios();

                 CarregarRelatorio();

                 lblMensagem.Text = "Base atualizada com sucesso!";
             }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Erro: " + ex.Message;
        }
    }
    
    
    private bool PodeProcessar()
    {
        lblMensagem.Text = String.Empty;

        if (fileDocumento.PostedFile == null || fileDocumento.PostedFile.FileName == "")
        {
            lblMensagem.Text = "Selecione o [Arquivo].";
            return false;
        }

        return true;
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
        ExportarExcel(dataTable, "BaseSocios");
    }
    
    protected void cmdProcessar_Click(object sender, EventArgs e)
    {
        if (PodeProcessar())
        {
            AtualizarBaseSocios();
        }
    }

    protected void cmdPesquisar_Click(object sender, EventArgs e)
    {
        CarregarRelatorio();
    }

    private DataTable CarregarRelatorio()
    {
        DataTable dataTable = null;
        try
        {
            string sNomeUsuario = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNome.Text.Trim());
            string sCPF = PontoBr.Utilidades.String.RemoverCaracterInvalido(PontoBr.Utilidades.String.RemoverCaracteresMascara(txtCPF.Text.Trim()));
            int iNumeroCota = String.IsNullOrEmpty(txtNumCota.Text.Trim()) ? -1 : Convert.ToInt32(txtNumCota.Text.Trim());

            relatorioCTL CRelatorio = new relatorioCTL();
            dataTable = CRelatorio.RetornarSocios(sNomeUsuario, sCPF, iNumeroCota);

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

    protected void grdRelatorio_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdRelatorio.PageIndex = e.NewPageIndex;
        CarregarRelatorio();
    }
}

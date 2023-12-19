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

public partial class administrador_retornoCatraca_fisica : System.Web.UI.Page
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
            CarregarEventosEdicao();
        }
    }

    private void CarregarEventosEdicao()
    {
        eventoCTL evento = new eventoCTL();
        evento.CarregarDropDownListEventosEdicao(dropEdicao, false, true);
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

    private void ProcessarRetornoCatracas()
    {
        try
        {
            int iIDEdicao = Convert.ToInt32(dropEdicao.SelectedValue);
            usuario UsuarioLogado = (usuario)Session["usuario"];

            catracaCTL CCatraca = new catracaCTL();
            CCatraca.LimparBaseCatracaFisica(iIDEdicao, UsuarioLogado.IDUsuario);

            LerArquivo(fileDocumento.PostedFile.FileName);

            DataTable dataTable = CCatraca.RetornarIngressosCatracaFisica(iIDEdicao);

            grdRelatorio.DataSource = dataTable;
            grdRelatorio.DataBind();
        }
        catch (Exception ex)
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager(ex.Message, this.Page);
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

    private void LerArquivo(string sPathFile)
    {
        string sEnderecoPasta = "~/documentos/catraca/";

        if (fileDocumento.HasFile)
        {
            fileDocumento.SaveAs(MapPath(@sEnderecoPasta) + fileDocumento.FileName);

            catracaCTL CCatraca = new catracaCTL();
            string sDataHoraEntrada = "";
            string sCatraca = "";
            string sIdentidadeEletronica = "";

            StreamReader streamReader = new StreamReader(MapPath(@sEnderecoPasta) + fileDocumento.FileName);
            while (true)
            {
                try
                {
                    string sLinha = streamReader.ReadLine();
                    if (sLinha == null) break;
                    sLinha = sLinha.ToString();

                    if (!String.IsNullOrEmpty(sLinha) && sLinha.Split(';').Length == 3)
                    {
                        string[] sCampo = sLinha.Split(';');

                        sIdentidadeEletronica = sCampo[0].ToString().Trim();
                        sDataHoraEntrada = sCampo[1].ToString().Trim();
                        sCatraca = sCampo[2].ToString().Trim();

                        if (!String.IsNullOrEmpty(sDataHoraEntrada) && !String.IsNullOrEmpty(sCatraca))
                        {
                            sDataHoraEntrada = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(sDataHoraEntrada).ToString("yyyy/MM/dd HH:mm:ss");
                            CCatraca.AtualizarRetornoCatracaFisica(sIdentidadeEletronica, sDataHoraEntrada, sCatraca);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Erro: " + ex.Message;
                }
            }
            
            streamReader.Close();
        }
    }
    
    private bool PodeProcessar()
    {
        lblMensagem.Text = String.Empty;

        if (dropEdicao.SelectedValue == "-1")
        {
            lblMensagem.Text = "Selecione [Evento].";
            return false;
        }
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

    private void ExportarExcel(string sNomeArquivo)
    {
        if (grdRelatorio.Rows.Count > 0)
        {
            Response.Clear();
            sNomeArquivo += "_" + DateTime.Now.Ticks.ToString() + ".xls";

            Response.AddHeader("content-disposition", "attachment;filename=" + sNomeArquivo + "");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter sWr = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hWr = new HtmlTextWriter(sWr);

            //Eliminar número em notação científica no Excel
            foreach (GridViewRow gridViewRow in grdRelatorio.Rows)
                gridViewRow.Cells[5].Attributes["style"] = "mso-number-format:\\@";

            grdRelatorio.RenderControl(hWr);
            Response.Write(sWr.ToString());
            Response.End();
        }
    }

    protected void cmdExportarExcel_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = String.Empty;
        if (grdRelatorio.Rows.Count != 0)
            ExportarExcel("RetornoCatraca");
        else
            lblMensagem.Text = "Não há registros para exportar!";
    }
    
    protected void cmdProcessar_Click(object sender, EventArgs e)
    {
        if (PodeProcessar())
        {
            ProcessarRetornoCatracas();
        }
    }
}

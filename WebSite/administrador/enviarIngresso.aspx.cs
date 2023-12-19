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

public partial class administrador_enviarIngresso : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, true, false, false, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            HttpContext.Current.Session["Venda"] = null;
            CarregarVendas();
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

    private void CarregarVendas()
    {
        try
        {
            string sNome = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNome.Text.Trim());
            string sCPF = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtCPF.Text.Trim());
            int iIDVenda = String.IsNullOrEmpty(txtVoucher.Text.Trim()) ? -1 : Convert.ToInt32(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtVoucher.Text.Trim()));
            string sIDStatusCompra = "2,3,5";

            vendaCTL CVenda = new vendaCTL();
            DataTable dataTable = CVenda.RetornarVendas(iIDVenda, sNome, sCPF, sIDStatusCompra);

            grdRelatorio.DataSource = dataTable;
            grdRelatorio.DataBind();

            lblNumeroRegistros.Text = "| " + dataTable.Rows.Count.ToString() + " registro(s) |";
        }
        catch (Exception ex)
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager(ex.Message, this.Page);
        }
    }    
    
    protected void cmdPesquisar_Click(object sender, EventArgs e)
    {
        CarregarVendas();
    }

    protected void grdRelatorio_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
            string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
            string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
            string sIDVenda = Criptografia.Criptografar(grdRelatorio.DataKeys[e.Row.RowIndex].Values[0].ToString(), sChave, sVetorInicializacao);

            for (int i = 0; i <= 9; i++)
                e.Row.Attributes.Add("onclick", "javascript:AbrirSolicitacaoIngresso('" + sIDVenda + "')");
        }
    }  
   
    private void ExibirMensagem(string sMensagem)
    {
        sMensagem = sMensagem.Replace("'", "");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + sMensagem + "')", true);
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Model.objetos;
using Controller;

public partial class relatorios_retornoCatraca_app_qrcode : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, false, false, false, false, false, true, true, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
        }
        txtQRCode.Focus();
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
        else if (UsuarioLogado.Perfil == "Portaria")
        {
            MasterPageFile = "~/controls/Portaria.master";
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
        try
        {
            string sTicket = txtTicket.Text;
            string sQrCode = txtQRCode.Text;

            if (String.IsNullOrEmpty(sTicket) && String.IsNullOrEmpty(sQrCode))
            {
                PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Preencha algum campo!", this.Page);
                return;
            }

            catracaCTL CCatraca = new catracaCTL();
            DataTable dataTable = CCatraca.RetornarIngressoApp(Convert.ToInt32(txtTicket.Text));

            grdRelatorio.DataSource = dataTable;
            grdRelatorio.DataBind();

            if (grdRelatorio.Rows.Count != 0)
            {
                txtTicket.Text = String.Empty;
                txtQRCode.Text = String.Empty;
            }
        }
        catch { }
    }
}

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

public partial class administrador_blackList : App_Code.BaseWeb
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
            CarregarBlackList();
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
    
    protected void cmdPesquisar_Click(object sender, EventArgs e)
    {
        CarregarBlackList();
    }

    private DataTable CarregarBlackList()
    {
        DataTable dataTable = null;
        try
        {
            string sNome = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNome.Text.Trim());
            string sIP = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtIP.Text.Trim());
            string sDataInicial = txtDataInicial.Text;
            string sDataFinal = txtDataFinal.Text;

            usuarioCTL CUsuario = new usuarioCTL();
            dataTable = CUsuario.RetornarBlackList(sIP, sNome, sDataInicial, sDataFinal);

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

    protected void grdRelatorio_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            grdRelatorio.Columns[2].Visible = true;
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image imgBloqueado = (Image)e.Row.FindControl("imgBloqueado");
            Button cmdBloquear = (Button)e.Row.Cells[8].Controls[0];
           
            if (e.Row.Cells[2].Text == "True")
            {
                imgBloqueado.ImageUrl = "~/images/status_vermelho.png";
                cmdBloquear.Text = "Desbloquear";
            }
            else
            {
                imgBloqueado.ImageUrl = "~/images/status_verde.png";
                cmdBloquear.Text = "Bloquear";
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            grdRelatorio.Columns[2].Visible = false;
        }
    }

    protected void grdRelatorio_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Bloquear")
        {
            GridViewRow gridViewRow = grdRelatorio.Rows[Convert.ToInt32(e.CommandArgument)];
            ListItem listItem = new ListItem();
            listItem.Text = Server.HtmlDecode(gridViewRow.Cells[0].Text);
            string sIP = listItem.Text;

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdRelatorio.Rows[index];

            int iBloqueado = 0;
            string sMensagem = "";

            Button cmdBloquear = (Button)row.Cells[8].Controls[0];
            usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

            if (cmdBloquear.Text == "Bloquear")
            {
                iBloqueado = 1;
                sMensagem = "IP bloqueado com sucesso!";
            }
            else
            {
                iBloqueado = 0;
                sMensagem = "IP liberado com sucesso!";
            }

            usuarioCTL CUsuario = new usuarioCTL();
            CUsuario.AtualizarBlackList(iBloqueado, UsuarioLogado.IDUsuario, sIP);
            CarregarBlackList();

            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager(sMensagem, this.Page);
        }
    }
}

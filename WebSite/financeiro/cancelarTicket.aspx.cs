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

public partial class financeiro_cancelarTicket : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(false, false, false, false, false, false, true, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            if (ConfigurationManager.AppSettings["TestandoSistema"].ToString() == "Sim")
            {
                txtVoucher.Text = "10656";
            }
        }
        txtVoucher.Focus();
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
        else if (UsuarioLogado.Perfil == "Financeiro")
        {
            MasterPageFile = "~/controls/Financeiro.master";
        }
    }

    private void CarregarIngressos()
    {
        try
        {
            int iIDVenda = String.IsNullOrEmpty(txtVoucher.Text.Trim()) ? -1 : Convert.ToInt32(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtVoucher.Text.Trim()));

            vendaCTL CVenda = new vendaCTL();
            DataTable dataTable = CVenda.RetornarTickets(iIDVenda);

            grdIngressos.DataSource = dataTable;
            grdIngressos.DataBind();

            lblNumeroRegistros.Text = "| " + dataTable.Rows.Count.ToString() + " registro(s) |";
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
    
    protected void cmdPesquisar_Click(object sender, EventArgs e)
    {
        //ExibirMensagem("Função desabilitada!"); return;
        if (PodePesquisar())
            CarregarIngressos();
        }

    private bool PodePesquisar()
    {
        if (String.IsNullOrEmpty(txtVoucher.Text.Trim()))
        {
            ExibirMensagem("Preencha algum campo para pesquisar.");
            return false;
        }
        return true;
    }

    protected void grdIngressos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            grdIngressos.Columns[6].Visible = true;
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            RadioButtonList radStatus = (RadioButtonList)e.Row.FindControl("radStatus");
            radStatus.SelectedValue = e.Row.Cells[6].Text;

            if (e.Row.Cells[6].Text == "0")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
                radStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            grdIngressos.Columns[6].Visible = false;
        }
    }    
        
    private bool PodeBaixar(int iIDVenda)
    {
        venda Venda = new venda();

        vendaCTL CVenda = new vendaCTL();
        Venda = CVenda.RetornarVenda(iIDVenda, "1,2,3,4");

        if (Venda.IDStatusCompra == 4)
        {
            ExibirMensagem("Essa venda já foi cancelada.");
            return false;
        }

        return true;
    }

    private void ExibirMensagem(string sMensagem)
    {
        sMensagem = sMensagem.Replace("'", "");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + sMensagem + "')", true);
    }

    protected void cmdSalvar_Click(object sender, EventArgs e)
    {
        vendaCTL CVenda = new vendaCTL();
        ingresso Ingresso = new ingresso();
        usuario UsuarioLogado = (usuario)Session["usuario"];

        int iIngressosInativados = 0;
        foreach (GridViewRow grid in grdIngressos.Rows)
        {
            RadioButtonList radStatus = (RadioButtonList)grid.FindControl("radStatus");
            int iAtivo = Convert.ToInt32(radStatus.SelectedValue);
            
            if (iAtivo == 0)
            {
                iIngressosInativados++;
                
                Ingresso.Ticket = Convert.ToDouble(grid.Cells[2].Text);
                Ingresso.IDUsuario = UsuarioLogado.IDUsuario;

                CVenda.CancelarTicket(Ingresso);
            }
        }
        CarregarIngressos();
        ExibirMensagem(iIngressosInativados.ToString() + " ingresso(s) inativado(s)!");
    }
}

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

public partial class relatorios_Ingressos : App_Code.BaseWeb
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
        else if (UsuarioLogado.Perfil == "Financeiro")
        {
            MasterPageFile = "~/controls/Financeiro.master";
        }
    }

    private void CarregarVendas()
    {
        try
        {
            string sNome = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNome.Text.Trim());
            string sCPF = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtCPF.Text.Trim());
            int iIDVenda = String.IsNullOrEmpty(txtVoucher.Text.Trim()) ? -1 : Convert.ToInt32(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtVoucher.Text.Trim()));

            vendaCTL CVenda = new vendaCTL();
            DataTable dataTable = CVenda.RetornarVendas(iIDVenda, sNome, sCPF, "2,3,4,5,6");

            grdRelatorio.DataSource = dataTable;
            grdRelatorio.DataBind();

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
        usuario UsuarioLogado = (usuario)Session["usuario"];
        if (UsuarioLogado.Perfil != "Financeiro")
        {
            ExibirMensagem("Função desabilitada!");
            return;
        }

        if (PodePesquisar())
        {
            CarregarVendas();
        }
    }

    private bool PodePesquisar()
    {
        if (String.IsNullOrEmpty(txtVoucher.Text.Trim())
            && String.IsNullOrEmpty(txtNome.Text.Trim())
            && String.IsNullOrEmpty(txtCPF.Text.Trim()))
        {
            ExibirMensagem("Preencha algum campo para pesquisar.");
            return false;
        }
        return true;
    }

    protected void grdRelatorio_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
            string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
            string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
            string sIDVenda = Criptografia.Criptografar(grdRelatorio.DataKeys[e.Row.RowIndex].Values[0].ToString(), sChave, sVetorInicializacao);

            for (int i = 0; i <= 11; i++)
            {
                e.Row.Cells[i].Attributes.Add("onclick", "ExibirIngressos('" + sIDVenda + "')");
            }

            HyperLink hypIngresso = (HyperLink)e.Row.Cells[12].FindControl("hypImprimirIngressos");
            if (e.Row.Cells[9].Text == "Cancelada" || e.Row.Cells[9].Text == "Reservada")
            {
                hypIngresso.Attributes.Add("onclick", "alert('Ingresso não disponível!')");
            }
            else
            {
                hypIngresso.NavigateUrl = "../cliente/ingressos.aspx?idvenda=" + sIDVenda;
                hypIngresso.ToolTip = "Voucher nº " + grdRelatorio.DataKeys[e.Row.RowIndex].Values[0].ToString();
                hypIngresso.Target = "_blank";
            }
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

}

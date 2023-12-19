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
using System.Windows.Forms;

public partial class vendedor_baixaVoucher : App_Code.BaseWeb
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

            vendaCTL CVenda = new vendaCTL();
            DataTable dataTable = CVenda.RetornarVendas(iIDVenda, sNome, sCPF, "2,3,4,5");

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
        Response.Redirect("default.aspx");
    }
    
    protected void cmdPesquisar_Click(object sender, EventArgs e)
    {
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

            for (int i = 0; i <= 12; i++)
            {
                e.Row.Cells[i].Attributes.Add("onclick", "ExibirIngressos('" + sIDVenda + "')");
            }

            HyperLink hypVoucher = (HyperLink)e.Row.Cells[12].FindControl("hypBaixarVoucher");
            hypVoucher.NavigateUrl = "../relatorios/voucher.aspx?idvenda=" + sIDVenda;
            hypVoucher.ToolTip = "Voucher nº " + grdRelatorio.DataKeys[e.Row.RowIndex].Values[0].ToString();
            hypVoucher.Target = "_blank";
        }
    }

    protected void grdRelatorio_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Baixar")
        {
            int iIDVenda = int.Parse(e.CommandArgument.ToString());
            if (PodeBaixar(iIDVenda))
            {
                usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

                vendaCTL CVenda = new vendaCTL();
                CVenda.EntregarIngresso(iIDVenda, UsuarioLogado.IDUsuario);

                CarregarVendas();
                ExibirMensagem("Venda baixada com sucesso!");
            }
        }
    }

    private bool PodeBaixar(int iIDVenda)
    {
        venda Venda = new venda();

        vendaCTL CVenda = new vendaCTL();
        Venda = CVenda.RetornarVenda(iIDVenda, "2,3,4,5");

        if (Venda.IDStatusCompra == 3)
        {
            ExibirMensagem("Os ingressos já foram entregues.");
            return false;
        }
        if (Venda.IDStatusCompra == 4)
        {
            ExibirMensagem("Essa venda foi cancelada.");
            return false;
        }
        if (Venda.IDStatusCompra == 5)
        {
            ExibirMensagem("Primeiramente, essa venda precisa ser aprovada.");
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

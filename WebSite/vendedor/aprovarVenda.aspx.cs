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

public partial class vendedor_aprovarVenda : App_Code.BaseWeb
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
            string sIDStatusCompra = "2,3,4,5";

            if (chkVendasAprovar.Checked) sIDStatusCompra = "5";

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
            {
                e.Row.Cells[i].Attributes.Add("onclick", "ExibirIngressos('" + sIDVenda + "')");
            }

            int iIDStatusCompra = Convert.ToInt32(grdRelatorio.DataKeys[e.Row.RowIndex].Values[1].ToString());

            if (iIDStatusCompra == 2
                || iIDStatusCompra == 3)
            {
                HyperLink hypVoucher = (HyperLink)e.Row.Cells[10].FindControl("hypBaixarVoucher");
                hypVoucher.NavigateUrl = "../relatorios/voucher.aspx?idvenda=" + sIDVenda;
                hypVoucher.ToolTip = "Voucher nº " + grdRelatorio.DataKeys[e.Row.RowIndex].Values[0].ToString();
                hypVoucher.Target = "_blank";
            }
            else
            {
                HyperLink hypVoucher = (HyperLink)e.Row.Cells[10].FindControl("hypBaixarVoucher");
                hypVoucher.Attributes.Add("onclick", "alert('O voucher poderá ser baixado apenas depois da aprovação da compra.')");
            }
        }
    }

    protected void grdRelatorio_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Aprovar")
        {
            int iIDVenda = int.Parse(e.CommandArgument.ToString());
            if (PodeAprovar(iIDVenda))
            {
                usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

                vendaCTL CVenda = new vendaCTL();
                CVenda.AprovarVenda(iIDVenda, UsuarioLogado.IDUsuario);

                venda Venda = CVenda.RetornarVenda(iIDVenda, null);
                CadastrarEstacionamento(Venda);

                CarregarVendas();
                ExibirMensagem("Venda aprovada com sucesso!");
            }
        }
    }

    private void CadastrarEstacionamento(venda Venda)
    {
        //Vagas de estacionamento
        vendaCTL CVenda = new vendaCTL();
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        for (int iNumeroVaga = 1; iNumeroVaga <= Venda.NumeroVagas; iNumeroVaga++)
        {
            string sIDIdentidadeEletronica = "EST" + iNumeroVaga.ToString() + Venda.IDVenda.ToString() + DateTime.Now.Ticks.ToString();
            sIDIdentidadeEletronica = sIDIdentidadeEletronica.Substring(0, 18);
            CVenda.CadastrarIngressoEstacionamento(Venda.IDVenda, Venda.IDEdicao, 0, sIDIdentidadeEletronica, Venda.Nome, Venda.CPF, UsuarioLogado.IDUsuario);
        }
    }

    private bool PodeAprovar(int iIDVenda)
    {
        venda Venda = new venda();

        vendaCTL CVenda = new vendaCTL();
        Venda = CVenda.RetornarVenda(iIDVenda, "2,3,4,5");

        if (Venda.IDStatusCompra == 2)
        {
            ExibirMensagem("Essa venda já foi aprovada.");
            return false;
        }
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

        return true;
    }

    private void ExibirMensagem(string sMensagem)
    {
        sMensagem = sMensagem.Replace("'", "");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + sMensagem + "')", true);
    }

}

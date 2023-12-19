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

public partial class financeiro_cancelarVenda : App_Code.BaseWeb
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

            for (int i = 0; i <= 13; i++)
            {
                e.Row.Cells[i].Attributes.Add("onclick", "ExibirIngressos('" + sIDVenda + "')");
            }

            HyperLink hypVoucher = (HyperLink)e.Row.Cells[12].FindControl("hypBaixarVoucher");
            if (e.Row.Cells[9].Text == "Cancelada" || e.Row.Cells[9].Text == "Reservada")
            {
                hypVoucher.Attributes.Add("onclick", "alert('Voucher não disponível!')");
            }
            else
            {
                hypVoucher.NavigateUrl = "../relatorios/voucher.aspx?idvenda=" + sIDVenda;
                hypVoucher.ToolTip = "Voucher nº " + grdRelatorio.DataKeys[e.Row.RowIndex].Values[0].ToString();
                hypVoucher.Target = "_blank";
            }
        }
    }

    protected void grdRelatorio_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Cancelar")
        {
            try
            {
                int iIDVenda = int.Parse(e.CommandArgument.ToString());
                if (PodeBaixar(iIDVenda))
                {
                    usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

                    vendaCTL CVenda = new vendaCTL();
                    CVenda.CancelarVenda(iIDVenda, UsuarioLogado.IDUsuario);

                    //Cancelar venda na Cielo
                    DataTable dataTable = CVenda.RetornarTidCartao(iIDVenda);
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        try
                        {
                            //Cancelamento na Cielo
                            Cielo.Configuration.IConfiguration customConfiguration = new Cielo.Configuration.CustomConfiguration()
                            {
                                DefaultEndpoint = ConfigurationManager.AppSettings["cielo.endpoint.default"],
                                QueryEndpoint = ConfigurationManager.AppSettings["cielo.endpoint.query"],
                                MerchantId = ConfigurationManager.AppSettings["cielo.customer.id"],
                                MerchantKey = ConfigurationManager.AppSettings["cielo.customer.key"],
                                ReturnUrl = ConfigurationManager.AppSettings["cielo.return.url"],
                            };

                            var cieloService = new Cielo.CieloService(customConfiguration);

                            Guid PaymentId;
                            PaymentId = new Guid(dataRow["PaymentId"].ToString());

                            var responseCaptura = cieloService.CancelTransaction(PaymentId, null);
                        }
                        catch { }

                        CVenda.CancelarVendaCartao(Convert.ToInt32(dataRow["Numerodocumento"].ToString()), UsuarioLogado.IDUsuario);
                    }

                    CarregarVendas();

                    string sMensagem = "Venda cancelada com sucesso! Confirme se a venda foi cancelada no site da Cielo (caso tenha sido paga através de cartão).";

                    ExibirMensagem(sMensagem);
                }
            }
            catch (Exception ex)
            {
                PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Ocorreu uma falha no sistema.\n\n" + ex.Message, this.Page);
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

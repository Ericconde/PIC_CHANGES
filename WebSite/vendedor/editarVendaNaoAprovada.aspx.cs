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

public partial class vendedor_editarVendaNaoAprovada : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("cadastro.aspx");

        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(false, true, false, false, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
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
        usuario UsuarioLogado = (usuario)Session["usuario"];

        if (UsuarioLogado.Perfil == "Administrador")
            Response.Redirect("../administrador/default.aspx");
        else if (UsuarioLogado.Perfil == "Vendedor")
            Response.Redirect("../vendedor/default.aspx");
        else if (UsuarioLogado.Perfil == "Contabilidade")
            Response.Redirect("../Contabilidade/Default.aspx");
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

            e.Row.Cells[10].Attributes.Add("onclick", "ExibirIngressos('" + sIDVenda + "')");
        }
    }

    protected void grdRelatorio_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
        {
            int iIDVenda = int.Parse(e.CommandArgument.ToString());
            if (PodeEditar(iIDVenda))
            {
                venda Venda = new venda();
                vendaCTL CVenda = new vendaCTL();

                Venda = CVenda.RetornarVenda(iIDVenda, "5");

                /////////////////////////////////////////////////////////////////
                
                usuarioCTL CUsuario = new usuarioCTL();
                usuario Usuario = new usuario();
                Usuario = CUsuario.RetornarUsuario(Venda.IDUsuario);

                HttpContext.Current.Session["ClienteCompra"] = Usuario;
                
                /////////////////////////////////////////////////////////////////                

                evento Evento = new evento();
                eventoCTL CEvento = new eventoCTL();
                Evento = CEvento.RetornarEdicao(Venda.IDEdicao);

                usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];
                Venda.IDUsuarioCadastro = UsuarioLogado.IDUsuario;
                Venda.IP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

                Venda.NumeroIngressoExtraSocioCota = Evento.NumeroIngressoExtraSocioCota;
                Venda.NumeroIngressoExtraSocioTitulo = Evento.NumeroIngressoExtraSocioTitulo;
                Venda.NumeroIngressosValorNaoSocio = Evento.NumeroIngressosValorNaoSocio;
                Venda.NumeroIngressosCadeira = Evento.NumeroIngressosCadeira;

                DataTable dataTable = CEvento.RetornarLotes(Venda.IDEdicao, true);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    DateTime dateInicioVendas = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataRow["Início Venda"].ToString());
                    DateTime dateFimVendas = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataRow["Fim Venda"].ToString());

                    if (DateTime.Now > dateInicioVendas
                        && DateTime.Now < dateFimVendas)
                    {
                        Venda.Lote = Convert.ToInt32(dataRow["Lote"].ToString());
                        Venda.IDLote = Convert.ToInt32(dataRow["IDLote"].ToString());

                        Venda.ValorInteiraCadeiraSocio = Convert.ToDouble(dataRow["ValorInteiraCadeiraSocio"].ToString());
                        Venda.ValorInteiraCadeiraNaoSocio = Convert.ToDouble(dataRow["ValorInteiraCadeiraNaoSocio"].ToString());
                        Venda.ValorInteiraAvulsoSocio = Convert.ToDouble(dataRow["ValorInteiraAvulsoSocio"].ToString());
                        Venda.ValorInteiraAvulsoNaoSocio = Convert.ToDouble(dataRow["ValorInteiraAvulsoNaoSocio"].ToString());

                        Venda.ValorMeiaCadeiraSocio = Convert.ToDouble(dataRow["ValorMeiaCadeiraSocio"].ToString());
                        Venda.ValorMeiaCadeiraNaoSocio = Convert.ToDouble(dataRow["ValorMeiaCadeiraNaoSocio"].ToString());
                        Venda.ValorMeiaAvulsoSocio = Convert.ToDouble(dataRow["ValorMeiaAvulsoSocio"].ToString());
                        Venda.ValorMeiaAvulsoNaoSocio = Convert.ToDouble(dataRow["ValorMeiaAvulsoNaoSocio"].ToString());
                    }

                    if (Venda.Lote != 0
                        && Convert.ToInt32(dataRow["Lote"].ToString()) > Venda.Lote)
                    {
                        if (Convert.ToBoolean(dataRow["InteiraCadeiraSocio"])) Venda.ValorInteiraCadeiraSocio = Convert.ToDouble(dataRow["ValorInteiraCadeiraSocio"].ToString());
                        if (Convert.ToBoolean(dataRow["InteiraCadeiraNaoSocio"])) Venda.ValorInteiraCadeiraNaoSocio = Convert.ToDouble(dataRow["ValorInteiraCadeiraNaoSocio"].ToString());
                        if (Convert.ToBoolean(dataRow["InteiraAvulsoSocio"])) Venda.ValorInteiraAvulsoSocio = Convert.ToDouble(dataRow["ValorInteiraAvulsoSocio"].ToString());
                        if (Convert.ToBoolean(dataRow["InteiraAvulsoNaoSocio"])) Venda.ValorInteiraAvulsoNaoSocio = Convert.ToDouble(dataRow["ValorInteiraAvulsoNaoSocio"].ToString());

                        if (Convert.ToBoolean(dataRow["MeiaCadeiraSocio"])) Venda.ValorMeiaCadeiraSocio = Convert.ToDouble(dataRow["ValorMeiaCadeiraSocio"].ToString());
                        if (Convert.ToBoolean(dataRow["MeiaCadeiraNaoSocio"])) Venda.ValorMeiaCadeiraNaoSocio = Convert.ToDouble(dataRow["ValorMeiaCadeiraNaoSocio"].ToString());
                        if (Convert.ToBoolean(dataRow["MeiaAvulsoSocio"])) Venda.ValorMeiaAvulsoSocio = Convert.ToDouble(dataRow["ValorMeiaAvulsoSocio"].ToString());
                        if (Convert.ToBoolean(dataRow["MeiaAvulsoNaoSocio"])) Venda.ValorMeiaAvulsoNaoSocio = Convert.ToDouble(dataRow["ValorMeiaAvulsoNaoSocio"].ToString());
                    }
                }

                Venda.Declaracao = true;
                HttpContext.Current.Session["Venda"] = Venda;
                
                Response.Redirect("../cliente/compra.aspx");
            }
        }
    }

    private bool PodeEditar(int iIDVenda)
    {
        venda Venda = new venda();

        vendaCTL CVenda = new vendaCTL();
        Venda = CVenda.RetornarVenda(iIDVenda, "1,2,3,4,5");

        if (Venda.IDStatusCompra == 1)
        {
            ExibirMensagem("Essa venda ainda está em andamento, não pode ser alterada.");
            return false;
        }
        if (Venda.IDStatusCompra == 2)
        {
            ExibirMensagem("Essa venda já foi aprovada, não pode ser alterada.");
            return false;
        }
        if (Venda.IDStatusCompra == 3)
        {
            ExibirMensagem("Os ingressos já foram entregues, a venda não pode ser alterada.");
            return false;
        }
        if (Venda.IDStatusCompra == 4)
        {
            ExibirMensagem("Essa venda foi cancelada, não pode ser alterada.");
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

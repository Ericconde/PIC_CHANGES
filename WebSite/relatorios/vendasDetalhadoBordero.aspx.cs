﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Model.objetos;
using Controller;
using System.Configuration;

public partial class relatorios_vendasDetalhadoBordero : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, true, false, false, false, false, true, false, true, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            CarregarEventosEdicao();
        }
    }

    private void CarregarEventosEdicao()
    {
        eventoCTL evento = new eventoCTL();
        evento.CarregarDropDownListEventosVendaAdministrador(dropEdicao, false, true);
    }
    protected void cmdVoltar_Click(object sender, EventArgs e)
    {
        usuario UsuarioLogado = (usuario)Session["usuario"];

        if (UsuarioLogado.Perfil == "Administrador")
            Response.Redirect("../administrador/default.aspx");
        else if (UsuarioLogado.Perfil == "Vendedor")
            Response.Redirect("../vendedor/default.aspx");
        else if (UsuarioLogado.Perfil == "Contabilidade")
            Response.Redirect("../Contabilidade/default.aspx");
        else if (UsuarioLogado.Perfil == "Financeiro")
            Response.Redirect("../financeiro/default.aspx");
        else if (UsuarioLogado.Perfil == "Fisco")
            Response.Redirect("../Fisco/Default.aspx");
    }

    protected void cmdPesquisar_Click(object sender, EventArgs e)
    {
        if (PodePesquisar(true))
        {
            CarregarRelatorio();
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
        else if (UsuarioLogado.Perfil == "Financeiro")
        {
            MasterPageFile = "~/controls/Financeiro.master";
        }
        else if (UsuarioLogado.Perfil == "Fisco")
        {
            MasterPageFile = "~/controls/Fisco.master";
        }
    }

    protected void grdVendaDetalhadoBordero_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdVendaDetalhadoBordero.PageIndex = e.NewPageIndex;
        CarregarRelatorio();
    }

    private DataTable CarregarRelatorio()
    {
        DataTable dataTable = null;
        try
        {
            int iIDEdicao = Convert.ToInt32(dropEdicao.SelectedValue);
            string sDataInicial = txtDataInicial.Text.Trim() == "" ? "1900/01/01" : PontoBr.Conversoes.Data.ConverterDataDMMAAAAComBarraParaFormatoBancoAAAAMMDD(txtDataInicial.Text);
            string sDataFinal = txtDataFinal.Text.Trim() == "" ? "2050/01/01" : PontoBr.Conversoes.Data.ConverterDataDMMAAAAComBarraParaFormatoBancoAAAAMMDD(txtDataFinal.Text) + " 23:59:59";
        
            relatorioCTL CRelatorio = new relatorioCTL();
            dataTable = CRelatorio.RetornarVendasDetalhadoBordero(sDataInicial, sDataFinal, iIDEdicao);

            dataTable.Columns.Remove("IDVenda");

            grdVendaDetalhadoBordero.DataSource = dataTable;
            grdVendaDetalhadoBordero.DataBind();


            lblNumeroRegistros.Text = "| " + dataTable.Rows.Count.ToString() + " registro(s) |";
        }
        catch (Exception ex)
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager(ex.Message, this.Page);
        }
        return dataTable;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void cmdExportarExcel_Click(object sender, EventArgs e)
    {    
            DataTable dataTable = CarregarRelatorio();
            ExportarExcel(dataTable, "VendaDetalhadoBordero");        
    }

    private void ExportarExcel(DataTable dataTable, string sNomeArquivo)
    {
        if (dataTable.Rows.Count > 0)
        {
            Response.Clear();
            sNomeArquivo += "_" + DateTime.Now.Ticks.ToString() + ".xls";

            Response.AddHeader("content-disposition", "attachment;filename=" + sNomeArquivo + "");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter sWr = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hWr = new HtmlTextWriter(sWr);

            GridView gridExcel = new GridView();
            gridExcel.DataSource = dataTable;
            gridExcel.DataBind();

            gridExcel.RenderControl(hWr);
            Response.Write(sWr.ToString());
            Response.End();
        }
    }

    private bool PodePesquisar(bool bExibirMensagem)
    {
        lblMensagem.Text = String.Empty;

        if (dropEdicao.SelectedValue == "-1")
        {
            if (bExibirMensagem) lblMensagem.Text = "Selecione [Evento].";
            return false;
        }

        if (!String.IsNullOrEmpty(txtDataInicial.Text.Trim())
            || !String.IsNullOrEmpty(txtDataFinal.Text.Trim()))
        {
            if (PontoBr.Conversoes.Data.ValidarDataBr(txtDataInicial.Text) == false)
            {
                if (bExibirMensagem) lblMensagem.Text = "[Data Inicial] inválida.";
                return false;
            }
            if (PontoBr.Conversoes.Data.ValidarDataBr(txtDataFinal.Text) == false)
            {
                if (bExibirMensagem) lblMensagem.Text = "[Data Final] inválida.";
                return false;
            }
        }
      
        return true;
    }
    
}
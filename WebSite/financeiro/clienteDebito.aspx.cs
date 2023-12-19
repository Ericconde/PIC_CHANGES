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

public partial class financeiro_clienteDebito : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(false, false, false, false, false, false, true, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        txtNome.Focus();

        if (!IsPostBack)
        {
            CarregarGridViewClientes();
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
        else if (UsuarioLogado.Perfil == "Contabilidade")
        {
            MasterPageFile = "~/controls/Contabilidade.master";
        }
        else if (UsuarioLogado.Perfil == "Financeiro")
        {
            MasterPageFile = "~/controls/Financeiro.master";
        }
    }

    private void CarregarGridViewClientes()
    {
        string sNome = PontoBr.Utilidades.String.TirarAcentos(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNome.Text.Trim().ToUpper()));
        string sCPF = PontoBr.Utilidades.String.RemoverCaracteresMascara(txtCPF.Text);
        string sEmail = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtEmail.Text.ToLower().Trim());
        
        int iNumeroCota = 0;
        if (!String.IsNullOrEmpty(txtNumCota.Text.Trim()))
            iNumeroCota = Convert.ToInt32(txtNumCota.Text);
        
        clienteCTL CCliente = new clienteCTL();
        CCliente.CarregarGridViewClientes(grdCliente, sNome, sCPF, sEmail, iNumeroCota, "3");

        lblNumeroLinhas.Text = "| " + grdCliente.Rows.Count.ToString() + " registro(s) |";
    }

    protected void cmdCancelar_Click(object sender, EventArgs e)
    {
        LimparCampos();
        CarregarGridViewClientes();
    }

    private void LimparCampos()
    {
        txtNome.Text = String.Empty;
        txtEmail.Text = String.Empty;
        txtCPF.Text = String.Empty;
        txtNumCota.Text = String.Empty;
    }

    protected void cmdPesquisar_Click(object sender, EventArgs e)
    {
        CarregarGridViewClientes();
    }    

    protected void grdCliente_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            bool bDebito = false;
            if (!String.IsNullOrEmpty(grdCliente.DataKeys[e.Row.RowIndex].Values[1].ToString()))
                bDebito = Convert.ToBoolean(grdCliente.DataKeys[e.Row.RowIndex].Values[1]);

            RadioButtonList radDebito = (RadioButtonList)e.Row.FindControl("radDebito");
            radDebito.SelectedValue = "0";
            
            if (bDebito)
            {
                radDebito.SelectedValue = "1";
                e.Row.ForeColor = System.Drawing.Color.Red;
                
                ////////////////////////////////////////////////////////////////

                socioCTL CSocio = new socioCTL();
                DataTable dataTable = CSocio.RetornarDigitos(grdCliente.DataKeys[e.Row.RowIndex].Values[2].ToString());

                e.Row.ToolTip = "";
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    if (!String.IsNullOrEmpty(e.Row.ToolTip)) e.Row.ToolTip += "\n";

                    e.Row.ToolTip += dataRow["Nome"];
                    e.Row.ToolTip += " (" + dataRow["Num_Cota"];
                    e.Row.ToolTip += "-" + dataRow["Digito"] + ")";
                    e.Row.ToolTip += " - Débito: " + dataRow["Débito"];
                }
            }
        }
    }

    protected void cmdSalvar_Click(object sender, EventArgs e)
    {
        if (grdCliente.Rows.Count > 15)
        {
            ExibirMensagem("Antes de atualizar, localize o sócio que deseja remover a pendência financeira.");
            return;
        }
        
        socioCTL CSocio = new socioCTL();
        usuario UsuarioLogado = (usuario)Session["usuario"];

        foreach (GridViewRow grid in grdCliente.Rows)
        {
            RadioButtonList radDebito = (RadioButtonList)grid.FindControl("radDebito");
            int iAtivo = Convert.ToInt32(radDebito.SelectedValue);

            if (iAtivo == 0)
            {
                string sNumeroCota = grdCliente.DataKeys[grid.RowIndex].Values[2].ToString();
                CSocio.AtualizarDebito(sNumeroCota);
            }
        }
        CarregarGridViewClientes();
        ExibirMensagem("Cotas(s) com débito atualizado(s)!");
    }

    private void ExibirMensagem(string sMensagem)
    {
        sMensagem = sMensagem.Replace("'", "");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + sMensagem + "')", true);
    }
}

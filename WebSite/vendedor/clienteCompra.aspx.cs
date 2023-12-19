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

public partial class vendedor_clienteCompra : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, true, false, false, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");

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
        CCliente.CarregarGridViewClientes(grdCliente, sNome, sCPF, sEmail, iNumeroCota, "3,4");

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

    protected void grdCliente_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Selecionar")
        {
            int iIDUsuario = Convert.ToInt32(grdCliente.DataKeys[int.Parse((string)e.CommandArgument)]["IDUsuario"].ToString());

            usuarioCTL CUsuario = new usuarioCTL();
            usuario Usuario = new usuario();
            Usuario = CUsuario.RetornarUsuario(iIDUsuario);

            HttpContext.Current.Session["ClienteCompra"] = Usuario;

            Response.Redirect("~/cliente/compra.aspx");
        }
    }

    protected void grdCliente_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            bool bDebito = false;
            if (!String.IsNullOrEmpty(grdCliente.DataKeys[e.Row.RowIndex].Values[1].ToString()))
                bDebito = Convert.ToBoolean(grdCliente.DataKeys[e.Row.RowIndex].Values[1]);

            if (bDebito)
            {
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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controller;
using System.Data;
using Model.objetos;
using System.Configuration;
using System.IO;

public partial class administrador_cargaCatraca_app : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, false, false, false, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            CarregarEventosEdicao();
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

    private void CarregarEventosEdicao()
    {
        eventoCTL evento = new eventoCTL();
        evento.CarregarDropDownListEventosEdicao(dropEdicao, false, true);
    }

    protected void cmdCarga_Click(object sender, EventArgs e)
    {
        if (dropEdicao.SelectedValue == "-1")
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Selecione [Evento]!", this.Page);
            return;
        }

        usuario UsuarioLogado = (usuario)Session["usuario"];
        catracaCTL CCatraca = new catracaCTL();

        CCatraca.DarCargaIngressosApp(Convert.ToInt32(dropEdicao.SelectedValue), UsuarioLogado.IDUsuario);
        PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Base de ingressos das catracas e estacionamento carregada com sucesso!", this.Page);
    }

    protected void dropEdicao_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblDados.Text = "";

        int iIDEdicao = Convert.ToInt32(dropEdicao.SelectedValue);

        if (iIDEdicao != -1)
        {
            lblDados.Text = "<br/>Total de ingressos:<br/><br/>";

            catracaCTL CCatraca = new catracaCTL();
            DataSet dataSet = CCatraca.RetornarIngressosApp(iIDEdicao, "", "");

            DataTable dataTable = dataSet.Tables[3];
            foreach (DataRow dataRow in dataTable.Rows)
                lblDados.Text += "Catraca - " + dataRow["Quantidade"].ToString() + "<br/>";

            dataTable = dataSet.Tables[4];
            foreach (DataRow dataRow in dataTable.Rows)
                lblDados.Text += "Estacionamento - " + dataRow["Quantidade"].ToString() + "<br/>";

            lblDados.Text += "<br/>";
        }
    }
}

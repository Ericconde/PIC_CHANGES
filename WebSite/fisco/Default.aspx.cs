using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controller;
using System.Data;
using Model.objetos;

public partial class fisco_Default : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(false, true, false, false, false, false, false, false, true, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            CarregarNomeUsuario();
            CarregarEventosEdicao();
            RetornarDashboard();
        }
    }

    private void CarregarEventosEdicao()
    {
        eventoCTL evento = new eventoCTL();
        evento.CarregarDropDownListEventosVendaAdministrador(dropEdicao, true, false);
    }

    private void RetornarDashboard()
    {
        int iIDEdicao = Convert.ToInt32(dropEdicao.SelectedValue);
        
        relatorioCTL BRelatorio = new relatorioCTL();
        DataSet dataSet = BRelatorio.RetornarDashboard(iIDEdicao);

        HttpContext.Current.Session["Dashboard"] = dataSet;
    }

    public void RetornarClientes()
    {
        DataTable dataTable = ((DataSet)HttpContext.Current.Session["Dashboard"]).Tables[0];

        string sDados = "";
        foreach (DataRow dataRow in dataTable.Rows)
        {
            sDados += "['" + dataRow["Tipo de Cliente"].ToString() + " (" + dataRow["Quantidade"].ToString() + ")', " + dataRow["Quantidade"].ToString() + "],";
        }

        Response.Write(sDados);
    }

    public void RetornarAcessos()
    {
        DataTable dataTable = ((DataSet)HttpContext.Current.Session["Dashboard"]).Tables[1];

        string sDados = "['Mês/Ano', 'Quantidade', { role: 'annotation' }]";
        string sLabelValor;

        foreach (DataRow dataRow in dataTable.Rows)
        {
            sLabelValor = dataRow["Quantidade"].ToString() == "0" ? "" : dataRow["Quantidade"].ToString();
            sDados += ", ['" + dataRow["Mes"].ToString() + "/" + dataRow["Ano"].ToString() + "', " + dataRow["Quantidade"].ToString() + ", '" + sLabelValor + "'] ";
        }

        Response.Write(sDados);
    }

    public void RetornarVendasQuantidade()
    {
        DataTable dataTable = ((DataSet)HttpContext.Current.Session["Dashboard"]).Tables[2];

        string sDados = "['Evento (Tipo Ingresso)', 'Quantidade', { role: 'annotation' }]";
        string sLabelValor;

        if (dataTable.Rows.Count > 0)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                sLabelValor = dataRow["Quantidade"].ToString() == "0" ? "" : dataRow["Quantidade"].ToString();
                sDados += ", ['" + dataRow["Evento / Edição"].ToString() + " - " + dataRow["Tipo Ingresso"].ToString() + "', " + dataRow["Quantidade"].ToString() + ", '" + sLabelValor + "'] ";
            }
        }
        else
            sDados += ", ['',''] ";

        Response.Write(sDados);
    }

    public void RetornarVendasValor()
    {
        DataTable dataTable = ((DataSet)HttpContext.Current.Session["Dashboard"]).Tables[3];

        string sDados = "['Evento', 'Valor', { role: 'annotation' }]";
        string sLabelValor;

        if (dataTable.Rows.Count > 0)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                sLabelValor = dataRow["Valor"].ToString() == "0" ? "" : dataRow["Valor"].ToString();
                sDados += ", ['" + dataRow["Evento / Edição"].ToString() + " - " + dataRow["Tipo Ingresso"].ToString() + "', " + dataRow["Valor"].ToString().Replace(",", ".") + ", '" + sLabelValor + "'] ";
            }
        }
        else
            sDados += ", ['',''] ";

        Response.Write(sDados);
    }

    protected void dropEdicao_SelectedIndexChanged(object sender, EventArgs e)
    {
        RetornarDashboard();
    }

    private void CarregarNomeUsuario()
    {
        try
        {
            usuario Usuario = new usuario();
            Usuario = (usuario)HttpContext.Current.Session["Usuario"];
            lblUsuario.Text = "USUÁRIO: " + Usuario.Nome.ToUpper() + " - PERFIL: " + Usuario.Perfil.ToUpper();
            lblIP.Text = "ACESSANDO PELO IP: " + HttpContext.Current.Request.ServerVariables["REMOTE_HOST"].ToString();
        }
        catch { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controller;
using Model.objetos;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Data.SqlClient;

public partial class cliente_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(false, false, true, true, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            usuario UsuarioLogado = (usuario)Session["usuario"];
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["EmailPermitidoTeste"].ToString())
                && ConfigurationManager.AppSettings["EmailPermitidoTeste"].ToString() != UsuarioLogado.Email)
            {
                PontoBr.Utilidades.Diversos.ExibirAlertaERedirecionar("../login/logout.aspx", this.Page, "Acesso inativado! Tente mais tarde!");
            }

            CarregarNomeUsuario();
            CarregarDadosCliente();
            CarregarEventos();
            CarregarComprovantes();
            CarregarIngressos();

            if (Request.QueryString["acao"] != null
                && Request.QueryString["acao"].ToString() == "cadastroinicial")
                divMensagemSucesso.Visible = true;
        }
    }



    private void CarregarDadosCliente()
    {
        usuario Usuario = (usuario)HttpContext.Current.Session["Usuario"];

        HyperLink hyperLink;
        hyperLink = new HyperLink();
        hyperLink.Text = Usuario.Nome.Substring(0, Usuario.Nome.Length > 35 ? 35 : Usuario.Nome.Length) + "<br/>";
        divDadosUsuario.Controls.Add(hyperLink);

        hyperLink = new HyperLink();
        hyperLink.Text = Usuario.Email.Substring(0, Usuario.Email.Length > 35 ? 35 : Usuario.Email.Length) + "<br/>";
        divDadosUsuario.Controls.Add(hyperLink);

        if (Convert.ToBoolean(Usuario.Debito))
        {
            //Dados
            hyperLink = new HyperLink();
            hyperLink.ForeColor = System.Drawing.Color.Red;
            hyperLink.Text = "Pendência financeira junto ao Clube. Compra só com cartão." + "<br/><br/><br/>";
            divDadosUsuario.Controls.Add(hyperLink);
        }
        else
        {
            hyperLink = new HyperLink();
            hyperLink.Text = "<br/><br/><br/><br/>";
            divDadosUsuario.Controls.Add(hyperLink);
        }
    }

    private void CarregarEventos()
    {
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        eventoCTL CEvento = new eventoCTL();
        DataTable dataTable = CEvento.RetornarEventosVenda(UsuarioLogado.Perfil);

        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();

        HyperLink hyperLink;
        int iNumeroRegistros = 0;
        foreach (DataRow dataRow in dataTable.Rows)
        {
            string sTexto = dataRow["EventoEdicao"].ToString();
            if (sTexto.Length > 40) sTexto = sTexto.Substring(0, 40) + "...";

            iNumeroRegistros++;

            hyperLink = new HyperLink();

            hyperLink.Text = sTexto + "<br/>";
            hyperLink.NavigateUrl = "../cliente/compra.aspx?idedicao=" + Criptografia.Criptografar(dataRow["IDEdicao"].ToString(), sChave, sVetorInicializacao);
            hyperLink.ToolTip = dataRow["EventoEdicao"].ToString();
            divEventos.Controls.Add(hyperLink);

            if (iNumeroRegistros == 5) break;
        }

        //Sem registros
        if (dataTable.Rows.Count == 0)
        {
            hyperLink = new HyperLink();
            hyperLink.Text = "Nenhum evento disponível";
            divEventos.Controls.Add(hyperLink);
        }

        for (int i = iNumeroRegistros; i < 5; i++)
        {
            hyperLink = new HyperLink();
            hyperLink.Text = "<br/>";
            divEventos.Controls.Add(hyperLink);
        }
    }

    private void CarregarComprovantes()
    {
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        vendaCTL CVenda = new vendaCTL();
        DataTable dataTable = CVenda.RetornarVendasCliente(UsuarioLogado.IDCliente);

        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();

        HyperLink hyperLink;
        int iNumeroRegistros = 0;
        foreach (DataRow dataRow in dataTable.Rows)
        {
            string sTexto = dataRow["IDVenda"].ToString() + " - " + dataRow["Evento"].ToString() + " (" + dataRow["Edicao"].ToString() + ")";
            sTexto += " - Comprado em " + dataRow["DataHoraCompra"].ToString();

            if (sTexto.Length > 40) sTexto = sTexto.Substring(0, 40) + "...";

            iNumeroRegistros++;

            hyperLink = new HyperLink();
            hyperLink.Text = sTexto + "<br/>";
            hyperLink.NavigateUrl = "../relatorios/voucher.aspx?idvenda=" + Criptografia.Criptografar(dataRow["IDVenda"].ToString(), sChave, sVetorInicializacao);
            hyperLink.ToolTip = dataRow["Evento"].ToString() + " (" + dataRow["Edicao"].ToString() + ")";
            hyperLink.ToolTip += " - Comprado em " + dataRow["DataHoraCompra"].ToString();
            hyperLink.Target = "_blank";
            divComprovante.Controls.Add(hyperLink);
        }

        //Sem registros
        if (dataTable.Rows.Count == 0)
        {
            hyperLink = new HyperLink();
            hyperLink.Text = "Nenhum comprovante disponível";
            divComprovante.Controls.Add(hyperLink);

            iNumeroRegistros++;
        }
    }

    private void CarregarIngressos()
    {
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        vendaCTL CVenda = new vendaCTL();
        DataTable dataTable = CVenda.RetornarIngressosCliente(UsuarioLogado.IDCliente);

        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();

        HyperLink hyperLink;
        int iNumeroRegistros = 0;

        if (dataTable.Rows.Count == 0)
        {
            DataTable datatable1 = new DataTable();

            datatable1.Columns.Add("IDvenda", typeof(string));
            datatable1.Columns.Add("Evento", typeof(string));
            datatable1.Columns.Add("Edicao", typeof(string));
            datatable1.Columns.Add("DataHoraCompra", typeof(string));

            datatable1.Rows.Add("22222", "Arraial", "2 ingressos disponiveis", "10/10/2022");
            datatable1.Rows.Add("33333", "Arraial", "2 ingressos disponiveis", "10/10/2022");
            datatable1.Rows.Add("44444", "Arraial", "2 ingressos disponiveis", "10/10/2022");
            datatable1.Rows.Add("55555", "Arraial", "2 ingressos disponiveis", "10/10/2022");

            gvitems.DataSource = datatable1;
            gvitems.DataBind();


        }

        else
        {
            gvitems.DataSource = dataTable;
            gvitems.DataBind();
        }

        

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


    protected void gvitems_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
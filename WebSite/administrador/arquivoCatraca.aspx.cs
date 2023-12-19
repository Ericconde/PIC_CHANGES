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

public partial class administrador_arquivoCatraca : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, false, false, false, false, false, true, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
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

    protected void cmdGerarArquivo_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = String.Empty;
        if (dropEdicao.SelectedValue == "-1")
        {
            lblMensagem.Text = "Selecione [Evento].";
            return;
        }

        vendaCTL CVenda = new vendaCTL();
        DataTable dataTable = CVenda.RetornarTicketsCatraca(Convert.ToInt32(dropEdicao.SelectedValue), radTipo.SelectedValue);

        string sArquivo = "PIC_ArquivoCatraca_";

        if (radTipo.SelectedValue == "Inteira")
            sArquivo += "Inteira_";
        else
            sArquivo += "MeioAdolescente_";

        sArquivo += DateTime.Now.Ticks.ToString() + ".txt";
        string sRegistros = "";

        foreach (DataRow dataRow in dataTable.Rows)
        {
            sRegistros += dataRow["IdentidadeEletronica"].ToString() + "\r\n";
        }

        MemoryStream memoryStream = new MemoryStream();
        TextWriter textWriter = new StreamWriter(memoryStream);
        textWriter.WriteLine(sRegistros);
        textWriter.Flush();
        byte[] bytes = memoryStream.ToArray();
        memoryStream.Close();

        Response.Clear();
        Response.ContentType = "application/force-download";
        Response.AddHeader("content-disposition", "attachment; filename=" + sArquivo + "");
        Response.BinaryWrite(bytes);
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}

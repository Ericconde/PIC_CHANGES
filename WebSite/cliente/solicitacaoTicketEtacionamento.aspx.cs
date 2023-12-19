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
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Net;

using System.Collections;
using System.Net.Mime;
using SelectPdf;
using System.Diagnostics;
using System.Threading;
using System.Net.Mail;
using System.Text.RegularExpressions;

public partial class cliente_SolicitacaoTicketEstacionamento : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            //if (!usuarioCTL.PermitirAcesso(true, false, true, true, false, false, true, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            CarregarDados();
        }
    }

    private void CarregarDados()
    {
        usuario Usuario = (usuario)HttpContext.Current.Session["Usuario"];
        txtEmail.Text = Usuario.Email;
        
        lblInformativo.Text = "<ul><li>*Ao receber o e-mail, imprima com boa qualidade , na cor preta, assim evitará erros de leitura do código de barras.</li>";
        lblInformativo.Text += "*Guarde com cuidado seus tickets, não permita que façam cópia dele, apenas um ticket conseguirá acessar o evento e somente você tem acesso a impressão dos seus tickets.</li></ul>";
    }

    protected void cmdEnviarIngressos_Click(object sender, EventArgs e)
    {
        EnviarTicketEstacionamento();
    }

    public void EnviarTicketEstacionamento()
    {
        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        string sIDVenda = Criptografia.Criptografar(Request.QueryString["IDVenda"], sChave, sVetorInicializacao);
        string sURL = "http://localhost:54302/Web/cliente/ticketEstacionamento.aspx?IDvenda=" + sIDVenda;

        GerarPDF(sURL, sIDVenda);
        salvarSolicitaçãoTicketEstacionamento(sIDVenda);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        
    }

    private void salvarSolicitaçãoTicketEstacionamento(string sIDVenda)
    {
        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        sIDVenda = Criptografia.Descriptografar(sIDVenda, sChave, sVetorInicializacao);

        vendaCTL CVenda = new vendaCTL();
        string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

        CVenda.CadastrarSolicitacaoIngresso(sIDVenda, sIP, txtEmail.Text, "2");
    }

    public static bool ValidaEnderecoEmail(string enderecoEmail)
    {
        try
        {
            //define a expressão regulara para validar o email
            string texto_Validar = enderecoEmail;
            Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

            // testa o email com a expressão
            if (expressaoRegex.IsMatch(texto_Validar))
            {
                // o email é valido
                return true;
            }
            else
            {
                // o email é inválido
                return false;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void GerarPDF(string sURL, string sIDVenda)
    {

        // read parameters from the webpage
        string pdf_page_size = "A4";
        PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
            pdf_page_size, true);

        string pdf_orientation = "Portrait";
        PdfPageOrientation pdfOrientation =
            (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
            pdf_orientation, true);

        int webPageWidth = 1024;
        try
        {
            webPageWidth = Convert.ToInt32("1200");
        }
        catch { }

        int webPageHeight = 0;
        try
        {
            webPageHeight = Convert.ToInt32("0");
        }
        catch { }

        // instantiate a html to pdf converter object
        HtmlToPdf converter = new HtmlToPdf();

        // set converter options
        converter.Options.PdfPageSize = pageSize;
        converter.Options.PdfPageOrientation = pdfOrientation;
        converter.Options.WebPageWidth = webPageWidth;
        converter.Options.WebPageHeight = webPageHeight;

        // create a new pdf document converting an url
        PdfDocument docEstacionamento = converter.ConvertUrl(sURL);

        byte[] pdfBufferEstacionamento = docEstacionamento.Save();

        string sEnderecoArquivoEstacionamento = System.Web.Hosting.HostingEnvironment.MapPath("~/documentos/estacionamento_" + sIDVenda + ".pdf");

        FileStream Stream1 = new FileStream(sEnderecoArquivoEstacionamento, FileMode.Create);
        Stream1.Write(pdfBufferEstacionamento, 0, pdfBufferEstacionamento.Length);
        Stream1.Close();

        docEstacionamento.Close();

        ArrayList sAnexos = new ArrayList();
        sAnexos.Add(sEnderecoArquivoEstacionamento);

        //EnviarPDF(sAnexos);
        DeletarArquivoPDF(sAnexos);
    }

    private void EnviarPDF(ArrayList anexos)
    {
        string sTitulo = "PIC - INGRESSOS";

        StringBuilder sCorpo = new StringBuilder();
        sCorpo.Append("<span style=font-size: 10pt; font-family: Verdana>E-mail enviado pelo site www.ingressospic.com.br<br /><br /> ");
        sCorpo.Append("<span style=font-size: 10pt; font-family: Verdana>Email: teste</b></span><br /> ");

        sCorpo.Append("<br /> ");
        sCorpo.Append("</span></span><font face=Verdana size=1><b>____________________________________________________________</b><br /> ");
        sCorpo.Append("<b>PIC - www.ingressos.com.br</b><br /> ");
        sCorpo.Append("Mensagem gerada em " + (DateTime.Now) + "<br /> ");
        sCorpo.Append("<b>____________________________________________________________</b></font> ");

        //(String)Session["DestinatarioEmail"].ToString()
        string sDestinatarioEmail = ConfigurationManager.AppSettings["DestinatarioEmail"].ToString();
        string sRemetente = "Iluminare <contato@pontobrsistemas.com.br>";

        //string sRetorno = EnviarEmailComAnexo(sDestinatarioEmail, sRemetente, sTitulo, sCorpo.ToString(), anexos);
    }

    private void DeletarArquivoPDF(ArrayList sArquivos)
    {
        foreach (string sArquivo in sArquivos)
        {
            if (System.IO.File.Exists(sArquivo))
            {
                try
                {
                    System.IO.File.Delete(sArquivo);
                }
                catch (System.IO.IOException e)
                {
                    PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager(e.Message, this.Page);
                    return;
                }
            }
        }
    }
}
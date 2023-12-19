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

public partial class cliente_solicitacaoIngresso : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, false, true, true, false, false, true, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            CarregarDados();

            //if (ConfigurationManager.AppSettings["AmbienteHomologacao"].ToString() == "Sim")
            //    txtEmail.ReadOnly = false;

            //if (ConfigurationManager.AppSettings["TestandoSistema"].ToString() == "Sim")
            //    txtEmail.Text = "victor@pontobrsistemas.com.br";
        }
    }

    private void CarregarDados()
    {
        usuario Usuario = (usuario)HttpContext.Current.Session["Usuario"];
        txtEmail.Text = Usuario.Email;

        if (Usuario.Perfil == "Administrador")
            txtEmail.ReadOnly = false;

        lblInformativo.Text = "<br/><span style='font-size: 10pt; font-family: Calibri; color: Red; font-weight:bold'><u>O PIC seguirá as normas de enfrentamento à Covid estabelecidas pela Prefeitura de Belo Horizonte e vigentes no dia da Festa Junina. Fique atento e acompanhe em nosso site e redes sociais as regras específicas para acesso ao evento.</u></b></span>";
        lblInformativo.Text += "<br/><br/>*Ao receber os ingressos por email, imprima com boa qualidade, na cor preta, assim evitará erros de leitura do QRCode.";
        lblInformativo.Text += "<br/><br/><span style='font-size: 10pt; font-family: Calibri; color: Red; font-weight:bold'><u>Caso o QRCode não esteja presente nos ingressos, reenvio-os.</u></b></span>";
        lblInformativo.Text += "<br/><br/><span style='font-size: 10pt; font-family: Calibri; color: Red; font-weight:bold'><u>IMPRIMA APENAS DOIS INGRESSOS POR PÁGINA.</u></b></span>";
        lblInformativo.Text += "<br/><br/>*Guarde com cuidado seus ingressos, não permita que façam cópias dele, apenas um ingresso conseguirá acessar o evento e <span style='color:Red;'><strong>somente você tem acesso a impressão dos seus ingressos.</strong></span>";

        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();

        vendaCTL CVenda = new vendaCTL();
        DataTable dataTable = CVenda.RetornarIngressos(Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["IDVenda"], sChave, sVetorInicializacao)), null);

        lblInformativo.Text += "<br/><br/>Você irá receber no seu e-mail os seguintes documentos:";
        lblInformativo.Text += "<br/><br/>Ingressos (" + dataTable.Rows.Count.ToString() + ")";

        venda Venda = CVenda.RetornarVenda(Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["IDVenda"], sChave, sVetorInicializacao)), "");
        if (Venda.Evento != "Boate")
        {
            int iIDVenda = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["IDVenda"], sChave, sVetorInicializacao));

            dataTable = CVenda.RetornarIngressosMesa(iIDVenda);
            if (dataTable.Rows.Count > 0) lblInformativo.Text += "<br/>Ticket de mesa (" + dataTable.Rows.Count + ")";

            dataTable = CVenda.RetornarIngressosEstacionamento(iIDVenda);
            if (dataTable.Rows.Count > 0) lblInformativo.Text += "<br/>Ticket de estacionamento (" + dataTable.Rows.Count + ")";
        }
    }

    protected void cmdEnviarIngressos_Click(object sender, EventArgs e)
    {
        //Verifica se há ingressos no Voucher
        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();

        vendaCTL CVenda = new vendaCTL();
        int iIDVenda = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["IDVenda"], sChave, sVetorInicializacao));
        DataTable dataTable = CVenda.RetornarIngressos(iIDVenda, null);

        if (dataTable.Rows.Count == 0)
        {
            PontoBr.Utilidades.Diversos.ExibirAlerta("Não há ingressos nesse voucher!");

            Response.Write("<script>parent.location.reload(true);</script>");
            Response.Write("<script>parent.jQuery.fancybox.close();</script>");

            return;
        }

        //Gerar token
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        string sToken = Criptografia.Criptografar(DateTime.Now.Ticks.ToString(), sChave, sVetorInicializacao);
        usuarioCTL CUsuario = new usuarioCTL();

        if (UsuarioLogado.Perfil == "Administrador")
        {
            venda Venda = CVenda.RetornarVenda(iIDVenda, null);
            CUsuario.AtualizarToken(Venda.IDUsuario, sToken);
        }
        else
            CUsuario.AtualizarToken(UsuarioLogado.IDUsuario, sToken);

        EnviarIngressos(sToken);
    }

    public void EnviarIngressos(string sToken)
    {
        string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];
        vendaCTL CVenda = new vendaCTL();

        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        string sIDVenda = Request.QueryString["IDVenda"];
        DataTable dataTable = CVenda.RetornarIngressos(Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["IDVenda"], sChave, sVetorInicializacao)), null);

        venda Venda = CVenda.RetornarVenda(Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["IDVenda"], sChave, sVetorInicializacao)), "");

        string sUrl = HttpContext.Current.Request.Url.AbsoluteUri.ToLower();

        //URL dos ingresso
        List<string> sUrlIngressos = new List<string>();

        int iNumeroMaximoIngressosArquivo = 10;
        if (dataTable.Rows.Count <= iNumeroMaximoIngressosArquivo)
        {
            if (Venda.Evento == "Boate")
                sUrlIngressos.Add(sUrl.Substring(0, sUrl.IndexOf("SolicitacaoIngresso.aspx".ToLower())) + "IngressosBoate.aspx?IDvenda=" + sIDVenda + "&token=" + sToken);
            else
                sUrlIngressos.Add(sUrl.Substring(0, sUrl.IndexOf("SolicitacaoIngresso.aspx".ToLower())) + "Ingressos.aspx?IDvenda=" + sIDVenda + "&token=" + sToken);
        }
        else //Mais de um arquivo de ingressos 
        {
            int iNumeroIngressosArquivo = 0;
            int iNumeroIngressosTodosArquivos = 0;
            int iArquivo = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                iNumeroIngressosArquivo++;
                iNumeroIngressosTodosArquivos++;
                if (iNumeroIngressosArquivo == iNumeroMaximoIngressosArquivo
                    || iNumeroIngressosTodosArquivos == dataTable.Rows.Count)
                {
                    iArquivo++;
                    if (Venda.Evento == "Boate")
                        sUrlIngressos.Add(sUrl.Substring(0, sUrl.IndexOf("SolicitacaoIngresso.aspx".ToLower())) + "IngressosBoate.aspx?IDvenda=" + sIDVenda + "&token=" + sToken + "&arquivo=" + iArquivo.ToString());
                    else
                        sUrlIngressos.Add(sUrl.Substring(0, sUrl.IndexOf("SolicitacaoIngresso.aspx".ToLower())) + "Ingressos.aspx?IDvenda=" + sIDVenda + "&token=" + sToken + "&arquivo=" + iArquivo.ToString());

                    iNumeroIngressosArquivo = 0;
                }
            }
        }

        CVenda.CadastrarSolicitacaoIngresso(Criptografia.Descriptografar(Request.QueryString["IDVenda"], sChave, sVetorInicializacao), sIP, txtEmail.Text, "1");

        string sUrlMesa = "";
        string sUrlEstacionamento = "";
        if (Venda.Evento != "Boate")
        {
            //URL das mesas
            dataTable = CVenda.RetornarIngressosMesa(Venda.IDVenda);
            if (dataTable.Rows.Count > 0)
            {
                sUrlMesa = sUrl.Substring(0, sUrl.IndexOf("SolicitacaoIngresso.aspx".ToLower())) + "ticketMesa.aspx?IDvenda=" + sIDVenda + "&token=" + sToken;
                CVenda.CadastrarSolicitacaoIngresso(Criptografia.Descriptografar(Request.QueryString["IDVenda"], sChave, sVetorInicializacao), sIP, txtEmail.Text, "3");
            }

            //URL dos tickets de Estacionamento
            dataTable = CVenda.RetornarIngressosEstacionamento(Venda.IDVenda);
            if (dataTable.Rows.Count > 0)
            {
                sUrlEstacionamento = sUrl.Substring(0, sUrl.IndexOf("SolicitacaoIngresso.aspx".ToLower())) + "ticketEstacionamento.aspx?IDvenda=" + sIDVenda + "&token=" + sToken;
                CVenda.CadastrarSolicitacaoIngresso(Criptografia.Descriptografar(Request.QueryString["IDVenda"], sChave, sVetorInicializacao), sIP, txtEmail.Text, "2");
            }
        }

        GerarPDF(Criptografia.Descriptografar(Request.QueryString["IDVenda"], sChave, sVetorInicializacao), sUrlIngressos, sUrlMesa, sUrlEstacionamento, Venda);
    }

    public void GerarPDF(string sIDVenda, List<string> sURLIngressos, string sURLMesa, string sURLEstacionamento, venda Venda)
    {
        string sPageSize = "A4";
        PdfPageSize pPageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
            sPageSize, true);

        string sPdfOrientacao = "Portrait";
        PdfPageOrientation pPdfOrientation =
            (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
            sPdfOrientacao, true);

        int iWebPageWidth = 1024;
        try
        {
            iWebPageWidth = Convert.ToInt32("870");
        }
        catch { }

        int iWebPageHeight = 0;
        try
        {
            iWebPageHeight = Convert.ToInt32("0");
        }
        catch { }

        // instantiate a html to pdf converter object
        HtmlToPdf converter = new HtmlToPdf();

        // set converter options
        converter.Options.PdfPageSize = pPageSize;
        converter.Options.PdfPageOrientation = pPdfOrientation;
        converter.Options.WebPageWidth = iWebPageWidth;
        converter.Options.WebPageHeight = iWebPageHeight;
        converter.Options.RenderPageOnTimeout = true;

        ArrayList sAnexos = new ArrayList();

        // Gerar o pdf dos ingressos
        int iArquivo = 0;
        foreach (string sURLIngresso in sURLIngressos)
        {
            iArquivo++;

            PdfDocument pDocIngresso = converter.ConvertUrl(sURLIngresso);
            byte[] pdfBufferIngresso = pDocIngresso.Save();

            string sEnderecoArquivoIngresso;

            if (sURLIngressos.Count == 1)
                sEnderecoArquivoIngresso = System.Web.Hosting.HostingEnvironment.MapPath("~/documentos/PIC_Ingressos_" + sIDVenda + ".pdf");
            else //Mais de um arquivo de ingressos 
                sEnderecoArquivoIngresso = System.Web.Hosting.HostingEnvironment.MapPath("~/documentos/PIC_Ingressos_" + sIDVenda + "_Arquivo" + iArquivo.ToString() + ".pdf");

            FileStream StreamIngresso = new FileStream(sEnderecoArquivoIngresso, FileMode.Create);
            StreamIngresso.Write(pdfBufferIngresso, 0, pdfBufferIngresso.Length);
            StreamIngresso.Close();
            pDocIngresso.Close();
            sAnexos.Add(sEnderecoArquivoIngresso);
        }

        //Gera o pdf dos tickets de mesa caso tenha algum.
        if (!String.IsNullOrEmpty(sURLMesa))
        {
            PdfDocument pDocMesa = converter.ConvertUrl(sURLMesa);
            byte[] pdfBufferMesa = pDocMesa.Save();
            string sEnderecoArquivoMesa = System.Web.Hosting.HostingEnvironment.MapPath("~/documentos/PIC_TicketMesa_" + sIDVenda + ".pdf");
            FileStream StreamMesa = new FileStream(sEnderecoArquivoMesa, FileMode.Create);
            StreamMesa.Write(pdfBufferMesa, 0, pdfBufferMesa.Length);
            StreamMesa.Close();
            pDocMesa.Close();
            sAnexos.Add(sEnderecoArquivoMesa);
        }

        //Gera o pdf dos tickets de estacionamento caso tenha algum.
        if (!String.IsNullOrEmpty(sURLEstacionamento))
        {
            PdfDocument pDocEstacionamento = converter.ConvertUrl(sURLEstacionamento);
            byte[] pdfBufferEstacionamento = pDocEstacionamento.Save();
            string sEnderecoArquivoEstacionamento = System.Web.Hosting.HostingEnvironment.MapPath("~/documentos/PIC_TicketEstacionamento_" + sIDVenda + ".pdf");
            FileStream StreamEstacionamento = new FileStream(sEnderecoArquivoEstacionamento, FileMode.Create);
            StreamEstacionamento.Write(pdfBufferEstacionamento, 0, pdfBufferEstacionamento.Length);
            StreamEstacionamento.Close();
            pDocEstacionamento.Close();
            sAnexos.Add(sEnderecoArquivoEstacionamento);
        }

        //Envia os pdf's em anexo por e-mail.
        EnviarPDF(sAnexos, Venda);

        //Deleta os arquivos criados.
        DeletarArquivoPDF(sAnexos);

        Response.Write("<script>parent.location.reload(true);</script>");
        Response.Write("<script>parent.jQuery.fancybox.close();</script>");
    }

    private void EnviarPDF(ArrayList aAnexos, venda Venda)
    {
        string Assunto = "PIC - INGRESSOS";

        StringBuilder sCorpo = new StringBuilder();
        sCorpo.Append("<span style='font-size: 10pt; font-family: Verdana'>Observações:</span><br /><br /> ");
        sCorpo.Append("<span style='font-size: 10pt; font-family: Verdana'>Ao receber o e-mail, imprima com boa qualidade, na cor preta, assim evitará erros de leitura do QRCode.</span> ");
        sCorpo.Append("<span style='font-size: 10pt; font-family: Verdana'>Caso o QRCode não esteja presente nos ingressos, reenvio-os.</span> ");
        sCorpo.Append("<br/><br/><span style='font-size: 10pt; font-family: Calibri; color: Red; font-weight:bold; text-decoration: underline'>IMPRIMA APENAS DOIS INGRESSOS POR PÁGINA.</span><br />");
        sCorpo.Append("<span style='font-size: 10pt; font-family: Calibri; color: Red; font-weight:bold'><br />Guarde com cuidado seus ingressos, não permita que façam cópias dele, apenas um ingresso conseguirá acessar o evento e somente você tem acesso a impressão dos seus ingressos.</span><br /><br /> ");

        sCorpo.Append("<span style='font-size: 10pt; font-family: Calibri; font-style:italic;'>Atenção,</span><br /><br /> ");
        sCorpo.Append("<span style='font-size: 10pt; font-family: Calibri; font-style:italic;'>Abaixo todos os itens que você poderá receber e deverá imprimir para ter acesso ao evento do PIC:</span><br /><br /> ");
        sCorpo.Append("<span style='font-size: 10pt; font-family: Calibri; font-style:italic;'>- Ingressos – Imprima apenas dois por página e apresente na entrada do evento.</span><br /> ");

        if (Venda.Evento != "Boate")
        {
            sCorpo.Append("<span style='font-size: 10pt; font-family: Calibri; font-style:italic;'>- Tickets de mesa - Somente para quem adquiriu mesas. Serve para localização e identificação da sua mesa.</span><br /> ");
            sCorpo.Append("<span style='font-size: 10pt; font-family: Calibri; font-style:italic;'>- Ticket de Estacionamento – Somente para quem adquiriu 6 ingressos em uma única mesa. Imprima e apresente na entrada do estacionamento.</span><br /> ");
        }

        sCorpo.Append("<br /> ");
        sCorpo.Append("</span></span><font face=Verdana size=1><b>____________________________________________________________</b><br /> ");
        sCorpo.Append("<b>PIC - www.ingressos.com.br</b><br /> ");
        sCorpo.Append("Mensagem gerada em " + (DateTime.Now) + "<br /> ");
        sCorpo.Append("<b>____________________________________________________________</b></font> ");

        string sRemetente = ConfigurationManager.AppSettings["RemetenteEmail"].ToString();
        string sDestinatarioEmail = txtEmail.Text;

        string sRetorno = EnviarEmailComAnexo(sDestinatarioEmail, sRemetente, Assunto, sCorpo.ToString(), aAnexos);

        if (sRetorno != "Sucesso!")
        {
            PontoBr.Utilidades.Diversos.ExibirAlerta(sRetorno);
        }
        else
        {
            PontoBr.Utilidades.Diversos.ExibirAlerta("Ingressos enviados com sucesso! Os documentos estarão no seu e-mail daqui uns minutos.\n\nVerifique na sua caixa de spam ou lixo eletrônico caso não tenha recebido.");
        }
    }

    public static string EnviarEmailComAnexo(string sDestinatario, string sRemetente, string sAssunto, string sCorpo, ArrayList aAnexos)
    {
        try
        {
            /*var objClientSmtp = new SmtpClient(ConfigurationManager.AppSettings["Host"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["Porta"].ToString())); // sendo o host e a port que você vai usar
            objClientSmtp.UseDefaultCredentials = false;
            objClientSmtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["Usuario"].ToString(), ConfigurationManager.AppSettings["Senha"].ToString()); // sendo user e pass suas credenciais
            objClientSmtp.EnableSsl = true;

            //Cria o endereço de email do remetente
            MailAddress de = new MailAddress(sRemetente);
            //Cria o endereço de email do destinatário -->
            MailAddress para = new MailAddress(sDestinatario);
            MailMessage mensagem = new MailMessage(de, para);
            mensagem.IsBodyHtml = true;
            //Assunto do email
            mensagem.Subject = sAssunto;
            //Conteúdo do email
            mensagem.Body = sCorpo;

            foreach (string anexo in aAnexos)
            {
                Attachment anexado = new Attachment(anexo);
                mensagem.Attachments.Add(anexado);
            }
            //Envia o email
            objClientSmtp.Send(mensagem);

            //liberar memoria.
            objClientSmtp.Dispose();
            mensagem.Dispose();
            return null;*/

            string sRetorno = PontoBr.Utilidades.Email.EnviarEmail(sAssunto, sRemetente, sDestinatario, sCorpo.ToString(), null, aAnexos, false);            
            return sRetorno;
        }
        catch
        {
            string erro = "Devido à algum problema técnico, o e-mail não pode ser enviado. Favor entrar em contato com a TI do PIC."; // ex.InnerException.ToString();
            return erro;
        }
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
                    PontoBr.Utilidades.Diversos.ExibirAlerta(e.Message);
                    return;
                }
            }
        }
    }
    protected void chkConfirmacao_CheckedChanged(object sender, EventArgs e)
    {
        if (chkConfirmacao.Checked)
        {
            cmdEnviarIngressos.Enabled = true;
            cmdEnviarIngressos.CssClass = "botao";
        }
        else
        {
            cmdEnviarIngressos.Enabled = false;
            cmdEnviarIngressos.CssClass = "";
        }
    }
}
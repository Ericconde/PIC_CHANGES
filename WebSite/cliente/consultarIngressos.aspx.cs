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

public partial class cliente_consultarIngressos : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");

        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(false, false, true, true, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            CarregarDropVendas();       
        }
    }

    private void CarregarDropVendas()
    {
        usuario UsuarioLogado = (usuario)Session["usuario"];
        vendaCTL CVenda = new vendaCTL();

        CVenda.CarregarDropVendas(DropVendas, UsuarioLogado.IDCliente);
    }

    private void CarregarCheckIngressos(int iIDVenda)
    {
        usuario UsuarioLogado = (usuario)Session["usuario"];
        vendaCTL CVenda = new vendaCTL();

        CVenda.CarregarCheckIngressos(chkIngressos, iIDVenda, null);
    }

    protected void DropVendas_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iIDVenda = Convert.ToInt32(DropVendas.SelectedValue);
        vendaCTL CVenda = new vendaCTL();
        DataTable dataTable = CVenda.RetornarIngressos(iIDVenda, null);

        CarregarCheckIngressos(iIDVenda);
        CarregarQRCode(dataTable);
    }

    private string RetornarUrlQRCode(string codigo)
    {
        string APIurl = "http://chart.apis.google.com/chart?cht=qr&chl=";

        if (codigo != null && codigo != "" && codigo != " ")
        {
            if (codigo.ToCharArray().Length >= 3 && codigo.Substring(0, 3) == "www") APIurl += "http://" + codigo + "&chs= 116x116";
            else
                APIurl += codigo + "&chs=200x200";
        }
        else
            Response.Write("<script>alert('Número de documento inválido! Não há nenhum ingresso vínculado a esse pagamento.');</script>");

        return APIurl;
    }

    private void CarregarQRCode(DataTable dtIngressos)
    {
        dtIngressos.Columns.Add("QRUrl", typeof(string));

        foreach (DataRow dataRow in dtIngressos.Rows)
        {
            dataRow["QRUrl"] = RetornarUrlQRCode(dataRow["IdentidadeEletronica"].ToString()); //Retorna o endereço de cada QRCode.
        }

        rptQRCode.DataSource = dtIngressos;
        rptQRCode.DataBind();
    }

    private void ExibirMensagem(string sMensagem)
    {
        sMensagem = sMensagem.Replace("'", "");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + sMensagem + "')", true);
    }
    protected void cmdEnviarIngresso_Click(object sender, EventArgs e)
    {
        if (podeEnviar())
        {
            int iIDVenda = Convert.ToInt32(DropVendas.SelectedValue);
            vendaCTL CVenda = new vendaCTL();
            DataTable dataTable = CVenda.RetornarIngressos(iIDVenda, null);
            int iQteSelecionados = 0;
            string sTickets = String.Empty;

            foreach (ListItem listItem in chkIngressos.Items)
            {
                if (listItem.Selected == true)
                {
                    iQteSelecionados++;
                    if (iQteSelecionados == 1)
                        sTickets += listItem.Value;
                    else
                        sTickets += "," + listItem.Value;
                }
            }

            if (!String.IsNullOrEmpty(txtEmail.Text))
            {
                PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();

                //Gerar token
                usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

                string sToken = Criptografia.Criptografar(DateTime.Now.Ticks.ToString(), sChave, sVetorInicializacao);
                usuarioCTL CUsuario = new usuarioCTL();
                CUsuario.AtualizarToken(UsuarioLogado.IDUsuario, sToken);

                EnviarIngressos(sToken, sTickets, iIDVenda.ToString());
                txtEmail.Text = String.Empty;
            }
        }
    }

    private bool podeEnviar()
    {
        if (DropVendas.SelectedValue == "-1")
        {
            ExibirMensagem("selecione o campo [Voucher].");
            return false;
        }

        int iQteSelecionados = 0;
        foreach (ListItem listItem in chkIngressos.Items)
        {
            if (listItem.Selected == true)
            {
                iQteSelecionados++;
            }
        }
        if (iQteSelecionados == 0)
        {
            ExibirMensagem("selecione pelo menos 1 [Ingresso].");
            return false;
        }

        if (iQteSelecionados > 10)
        {
            ExibirMensagem("Não é possível enviar acima de 10 ingressos selecionados.");
            return false;
        }

        if (String.IsNullOrEmpty(txtEmail.Text))
        {
            ExibirMensagem("Preencha o campo [E-mail].");
            return false;
        }

        return true;
    }

    public void EnviarIngressos(string sToken, string sTickets, string sIDVenda)
    {
        string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];
        vendaCTL CVenda = new vendaCTL();

        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
        DataTable dataTable = CVenda.RetornarIngressos(Convert.ToInt32(sIDVenda), null);

        venda Venda = CVenda.RetornarVenda(Convert.ToInt32(sIDVenda), "");

        string sUrl = HttpContext.Current.Request.Url.AbsoluteUri.ToLower();

        //URL dos ingresso
        List<string> sUrlIngressos = new List<string>();

        int iNumeroMaximoIngressosArquivo = 10;
        if (dataTable.Rows.Count <= iNumeroMaximoIngressosArquivo)
        {
            if (Venda.Evento == "Boate")
                sUrlIngressos.Add(sUrl.Substring(0, sUrl.IndexOf("consultarIngressos.aspx".ToLower())) + "IngressosBoate.aspx?IDvenda=" + Criptografia.Criptografar(sIDVenda, sChave, sVetorInicializacao) + "&token=" + sToken + "&TicketsConvidado=" + sTickets);
            else
                sUrlIngressos.Add(sUrl.Substring(0, sUrl.IndexOf("consultarIngressos.aspx".ToLower())) + "Ingressos.aspx?IDvenda=" + Criptografia.Criptografar(sIDVenda, sChave, sVetorInicializacao) + "&token=" + sToken + "&TicketsConvidado=" + sTickets);
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
                        sUrlIngressos.Add(sUrl.Substring(0, sUrl.IndexOf("consultarIngressos.aspx".ToLower())) + "IngressosBoate.aspx?IDvenda=" + Criptografia.Criptografar(sIDVenda, sChave, sVetorInicializacao) + "&token=" + sToken + "&TicketsConvidado=" + sTickets);
                    else
                        sUrlIngressos.Add(sUrl.Substring(0, sUrl.IndexOf("consultarIngressos.aspx".ToLower())) + "Ingressos.aspx?IDvenda=" + Criptografia.Criptografar(sIDVenda, sChave, sVetorInicializacao) + "&token=" + sToken + "&TicketsConvidado=" + sTickets);
                    iNumeroIngressosArquivo = 0;
                }
            }
        }

        CVenda.CadastrarSolicitacaoIngressoConvidado(sIDVenda, sTickets, sIP, txtEmail.Text, "4");

        if (HttpContext.Current.Session["Venda"] == null)
            HttpContext.Current.Session["Venda"] = Venda;

        GerarPDF(sIDVenda, sUrlIngressos);
    }

    public void GerarPDF(string sIDVenda, List<string> sURLIngressos)
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

        ArrayList sAnexos = new ArrayList();

        // gerar o pdf dos ingressos
        int iArquivo = 0;
        foreach (string sURLIngresso in sURLIngressos)
        {
            iArquivo++;

            PdfDocument pDocIngresso = converter.ConvertUrl(sURLIngresso);
            byte[] pdfBufferIngresso = pDocIngresso.Save();

            //gera numero aleatorio
            Random randNum = new Random();
            randNum.Next();
            string sEnderecoArquivoIngresso;

            if (sURLIngressos.Count == 1)
                sEnderecoArquivoIngresso = System.Web.Hosting.HostingEnvironment.MapPath("~/documentos/PIC_Ingressos_" + sIDVenda + ".pdf");
            else //Mais de um arquivo de ingressos 
                sEnderecoArquivoIngresso = System.Web.Hosting.HostingEnvironment.MapPath("~/documentos/PIC_Ingressos_" + sIDVenda + "_Arquivo" + iArquivo.ToString() + ".pdf");

            FileStream StreamIngresso = new FileStream(sEnderecoArquivoIngresso, FileMode.Create);
            StreamIngresso.Write(pdfBufferIngresso, 0, pdfBufferIngresso.Length);
            StreamIngresso.Close();
            StreamIngresso.Close();
            sAnexos.Add(sEnderecoArquivoIngresso);
        }

        //envia os pdf's em anexo por e-mail.
        EnviarPDF(sAnexos);

        //Deleta os arquivos criados.
        DeletarArquivoPDF(sAnexos);
    }

    private void EnviarPDF(ArrayList aAnexos)
    {
        string Assunto = "PIC - INGRESSOS";

        StringBuilder sCorpo = new StringBuilder();
        sCorpo.Append("<span style='font-size: 10pt; font-family: Verdana'>Observações:</span><br /><br /> ");
        sCorpo.Append("<span style='font-size: 10pt; font-family: Verdana'>Ao receber o e-mail, imprima com boa qualidade , na cor preta, assim evitará erros de leitura do QRCode.</span> ");
        sCorpo.Append("<br/><br/><span style='font-size: 10pt; font-family: Calibri; color: Red; font-weight:bold; text-decoration: underline'>IMPRIMA APENAS DOIS INGRESSOS POR PÁGINA.</span><br />");
        sCorpo.Append("<span style='font-size: 10pt; font-family: Calibri; color: Red; font-weight:bold'><br />Guarde com cuidado seus ingressos, não permita que façam cópias dele, apenas um ingresso conseguirá acessar o evento e somente você tem acesso a impressão dos seus ingressos.</span><br /><br /> ");

        sCorpo.Append("<span style='font-size: 10pt; font-family: Calibri; font-style:italic;'>Atenção,</span><br /><br /> ");
        sCorpo.Append("<span style='font-size: 10pt; font-family: Calibri; font-style:italic;'>Abaixo todos os itens que você poderá receber e deverá imprimir para ter acesso ao Arraiá do PIC:</span><br /><br /> ");
        sCorpo.Append("<span style='font-size: 10pt; font-family: Calibri; font-style:italic;'>- Ingressos – Imprima apenas dois por página e apresente na entrada do evento.</span><br /> ");

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
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager(sRetorno, this.Page);
        }
        else
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager("Ingressos enviados com sucesso! Os documentos estarão no e-mail do destinatário em alguns minutos.\n\nSolicite a verificação na caixa de spam ou lixo eletrônico caso o destinatário não tenha recebido.", this.Page);
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
        catch (Exception ex)
        {
            string erro = ex.InnerException.ToString();
            return ex.Message.ToString() + erro;
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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;
using Model.objetos;
using Controller;
using System.Data;

public partial class relatorios_voucher : System.Web.UI.Page
{
    private ReportDocument reportDocument;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (reportDocument == null)
            GerarComprovante();
        else
            IngressosPIC.ReportSource = null;
    }

    private void GerarComprovante()
    {
        try
        {
            relatorioCTL CRelatorio = new relatorioCTL();
            DataTable dataTable = null;

            usuario Usuario = new usuario();
            Usuario = (usuario)HttpContext.Current.Session["Usuario"];
            int iIDVenda = -1;

            PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
            string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
            string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();

            iIDVenda = Convert.ToInt32(Criptografia.Descriptografar(Request.QueryString["idvenda"], sChave, sVetorInicializacao));

            dataTable = CRelatorio.RetornarVoucher(iIDVenda);

            reportDocument = new ReportDocument();
            reportDocument.Load(Server.MapPath("~/relatorios/ComprovanteCompra.rpt"));
            reportDocument.SetDataSource(dataTable);

            IngressosPIC.ReportSource = reportDocument; IngressosPIC.ShowLastPage(); IngressosPIC.ShowFirstPage();

            ReportDocument reportDocumentPDF = reportDocument;
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                reportDocumentPDF.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "PIC_ComprovanteCompra_" + iIDVenda.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ex = null;
            }
        }
        catch (Exception ex)
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager(ex.Message, this.Page);
        }
    }
}
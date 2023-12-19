using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.SessionState;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controller;
using Model.objetos;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.IO;

public partial class cliente_TicketMesa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(Request.QueryString["IDVenda"]))
            {
                PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
                
                string sIDVenda = Criptografia.Descriptografar(Request.QueryString["IDVenda"], sChave, sVetorInicializacao);
                GerarTicketMesa(Convert.ToInt32(sIDVenda));
            }
            else
                Response.Write("Erro ao gerar ticket de mesa!");
        }
        catch
        {
            Response.Write("Erro ao gerar ticket de mesa!");
        }
    }

    protected void cmdImprimir_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.print();</script>");
    }

    private void GerarTicketMesa(int iIDVenda)
    {
        vendaCTL CVenda = new vendaCTL();
        DataTable dataTable = CVenda.RetornarTicketMesa(Convert.ToString(iIDVenda));
       
        CarregarDadosTicket(dataTable);
    }

    private void CarregarDadosTicket(DataTable dataTable)
    {
        rptTicketMesa.DataSource = dataTable;
        rptTicketMesa.DataBind();
    }
}
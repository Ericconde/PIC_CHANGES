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

public partial class cliente_TicketEstacionamento : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //GerarTicketEstacionamento("EST712251706308344");
                //return;

                if (!String.IsNullOrEmpty(Request.QueryString["IDVenda"]))
                {
                    string sIDVenda = "";
                    PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                    string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                    string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();

                    sIDVenda = Criptografia.Descriptografar(Request.QueryString["IDVenda"], sChave, sVetorInicializacao);
                    GerarTicketEstacionamento(Convert.ToInt32(sIDVenda));
                }
                else if (!String.IsNullOrEmpty(Request.QueryString["IdentidadeEletronica"]))
                {
                    string sIdentidadeEletronica = "";
                    PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                    string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                    string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();

                    sIdentidadeEletronica = Criptografia.Descriptografar(Request.QueryString["IdentidadeEletronica"], sChave, sVetorInicializacao);
                    GerarTicketEstacionamento(sIdentidadeEletronica);
                }
                else
                    Response.Write("Erro ao gerar tickets de estacionamento!");
            }
            catch (Exception ex)
            {
                Response.Write("Erro ao gerar tickets de estacionamento! " + ex.Message);
            }
        }
    }

    protected void cmdImprimir_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.print();</script>");
    }
    
    private void GerarTicketEstacionamento(int iIDVenda)
    {
        vendaCTL CVenda = new vendaCTL();
        DataTable dataTable = CVenda.RetornarIngressosEstacionamento(iIDVenda);

        if (dataTable.Rows.Count > 0) //Ticket de estacionamento
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (Convert.ToInt32(dataRow["NumeroCota"]) != 0 && dataRow["DigitoCota"] != null)
                    dataRow["NumeroCota"] = dataRow["NumeroCota"].ToString() + "-" + dataRow["DigitoCota"].ToString();
                else
                    dataRow["NumeroCota"] = "-";

                dataRow["CPF"] = dataRow["CPF"].ToString().Substring(0, 9) + "-" + dataRow["CPF"].ToString().Substring(dataRow["CPF"].ToString().Length - 2);

                //Retorna o endereço de cada QRCode.
                dataRow["IdentidadeEletronica"] = RetornarUrlQRCode(dataRow["IdentidadeEletronica"].ToString()); ;
            }
        }

        CarregarDadosTicket(dataTable);
    }

    private void GerarTicketEstacionamento(string sIdentidadeEletronica)
    {
        vendaCTL CVenda = new vendaCTL();
        DataTable dataTable = CVenda.RetornarIngressosEstacionamento(sIdentidadeEletronica);

        if (dataTable.Rows.Count > 0) //Ticket de estacionamento
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (Convert.ToInt32(dataRow["NumeroCota"]) != 0 && dataRow["DigitoCota"] != null)
                    dataRow["NumeroCota"] = dataRow["NumeroCota"].ToString() + "-" + dataRow["DigitoCota"].ToString();
                else
                    dataRow["NumeroCota"] = "-";

                dataRow["CPF"] = dataRow["CPF"].ToString().Substring(0, 9) + "-" + dataRow["CPF"].ToString().Substring(dataRow["CPF"].ToString().Length - 2);

                //Retorna o endereço de cada QRCode.
                dataRow["IdentidadeEletronica"] = RetornarUrlQRCode(dataRow["IdentidadeEletronica"].ToString()); ;
            }
        }

        CarregarDadosTicket(dataTable);
    }

    //Gera a Url do QRCode.
    private string RetornarUrlQRCode(string sIdentidadeEletronica)
    {
        string sUrl = "http://chart.apis.google.com/chart?cht=qr&chl=";

        if (sIdentidadeEletronica != null && sIdentidadeEletronica != "" && sIdentidadeEletronica != " ")
        {
            if (sIdentidadeEletronica.ToCharArray().Length >= 3 && sIdentidadeEletronica.Substring(0, 3) == "www") sUrl += "http://" + sIdentidadeEletronica + "&chs= 116x116";
            else
                sUrl += sIdentidadeEletronica + "&chs=116x116";
        }
        else
            Response.Write("<script>alert('Número de documento inválido! Não há nenhum ingresso vínculado a esse pagamento.');</script>");

        return sUrl;
    }

    private void CarregarDadosTicket(DataTable dataTable)
    {
        rptTicketsEstacionamento.DataSource = dataTable;
        rptTicketsEstacionamento.DataBind();
    }
}
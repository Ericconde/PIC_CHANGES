using Controller;
using FreeTextBoxControls;
using Model.objetos;
using PontoBr.Seguranca;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cliente_compras : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, true, true, true, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }
        if (!IsPostBack)
        {
            RetornarIngressos();
            CarregarEventos();
        }
    }

    private void RetornarIngressos()
    {

        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();

        usuario Usuario = new usuario();
        Usuario = (usuario)HttpContext.Current.Session["Usuario"];
        vendaCTL CVenda = new vendaCTL();

        DataTable dataTable = CVenda.RetornarVendasCliente(Usuario.IDCliente);


        DataTable data = new DataTable();

        data.Columns.Add("idVenda", typeof(int));
        data.Columns.Add("evento", typeof(string));
        data.Columns.Add("qntd", typeof(string));
        data.Columns.Add("dataHora", typeof(string));       
        int saldoIngressos = 0;

        foreach (DataRow item in dataTable.Rows)
        {

            int iIDVenda = (int)item.Field<Int64>(0);
            DataTable dataTable2 = CVenda.RetornarVendas(iIDVenda, "", "", "2");
            DataTable dataTable3 = CVenda.RetornarVendasCliente(Usuario.IDCliente);
            DataTable dataTableIngressos = CVenda.RetornarIngressosCliente(iIDVenda);


            DataRow NewRow = data.NewRow();
            NewRow[0] = iIDVenda;
            NewRow[1] = /*item.Field<Int64>(0) + " - " +*/ item.Field<string>(1) + " - " + DateTime.Now.Date.Year;


            foreach (DataRow dr in dataTable2.Rows)
            {
                saldoIngressos += (int)dr["NumeroIngressos"];
            }
            if (dataTableIngressos.Rows.Count > 0)
            {
                NewRow[2] = saldoIngressos > 1 ? saldoIngressos + " ingressos disponíveis" : saldoIngressos + " ingresso disponível";
            }
            else
            {
                NewRow[2] = saldoIngressos > 1 ? saldoIngressos + " ingressos ainda não disponíveis para impressão" : saldoIngressos + " ingresso ainda não disponível para impressão";

            }
            NewRow[3] = "Data da compra: " + item.Field<string>(3).Split(' ')[0];
            data.Rows.Add(NewRow);
        }

        grdCompras.DataSource = data;
        grdCompras.DataBind();
    }

    private void CarregarEventos()
    {
        eventoCTL CEvento = new eventoCTL();
        evento Evento = new evento();
        DataTable edicoesAtivas = CEvento.RetornarEventosEdicao();

        foreach (DataRow item in edicoesAtivas.Rows)
        {
            Evento = CEvento.RetornarEdicao(Convert.ToInt32(item.Field<int>(0)));

            string sUrl = "../cliente/indexEvento.aspx?idedicao=" + Evento.IDEdicao;
            string src = "../images/banner1.jpg";

            if (Evento.Evento == "Festa Junina")
            {
                sUrl = "../cliente/indexEvento.aspx?idedicao=" + Evento.IDEdicao;
                src = "../images/BANNER_SITE_INGRESSO_ARRAIA_PIC2022.jpg";
            }
            else if (Evento.Evento == "Reveillon")
            {
                sUrl = "../cliente/indexEvento.aspx?idedicao=" + Evento.IDEdicao;
                src = "../images/banner222.jpg";
            }
            else if (Evento.Evento == "Boate")
            {
                sUrl = "../cliente/indexEvento.aspx?idedicao=" + Evento.IDEdicao;
                src = "../images/banner10.jpg";
            }
            else if (Evento.Evento == "Outros")
            {
                sUrl = "../cliente/indexEvento.aspx?idedicao=" + Evento.IDEdicao;
                src = "../images/banner1.jpg";
            }
            swipeItens.Text += @"<div class='swiper-slide card mb-4 box-shadow'>" +
                                    "<a href='" + sUrl + "'>" +
                                      "<img class='card-img-top' src='" + src + "' alt=''>" +
                                    "</a>" +
                                      "<a target='_Self' href='" + sUrl + "'>" +
                                          "<div class='card-body text-uppercase text-left'>" +
                                              "<span class='d-block text-primary data'>" + Evento.DataHoraEvento.Split(' ')[0] + "</span>" +
                                              "<span class='titulo text-bolder'>" + Evento.Edicao + "</span>" +
                                              "<p class='card-text text-muted descricao'>" +
                                                  Evento.Edicao +
                                              "</p>" +
                                          "</div>" +
                                      "</a>" +
                                  "</div></br> ";
        }
    }

    //protected void grdCompras_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    string siIDVenda = grdCompras.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
    //    if (e.CommandName == "enviar")
    //    {
    //        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
    //        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
    //        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
    //        string IDVenda = Criptografia.Criptografar(siIDVenda, sChave, sVetorInicializacao);

    //        Response.Redirect("../cliente/reciboPagamento.aspx?id=" + IDVenda, false);

    //    }
    //}

    protected void OrderGridView_RowCreated(Object sender, GridViewRowEventArgs e)
    {

        PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
        string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
        string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            string field1 = dr[2].ToString();

            if (field1.Contains("não"))
            {
                e.Row.Cells[2].ForeColor = Color.Red;
                //e.Row.Cells[4].Text = "IMPRIMIR VOUCHER";
                //e.Row.Cells[4].CssClass = "ingresso_enviar";                
                HyperLink hyperLink;
                hyperLink = new HyperLink();
                hyperLink.Text = "IMPRIMIR VOUCHER";
                hyperLink.NavigateUrl = "../cliente/reciboPagamento.aspx?id=" + Criptografia.Criptografar(dr[0].ToString(), sChave, sVetorInicializacao);
                hyperLink.CssClass = "ingresso_enviar";
                hyperLink.Target = "_blank";
                e.Row.Cells[4].Controls.Add(hyperLink);

            }
            else
            {
                HyperLink hyperLink;
                hyperLink = new HyperLink();
                hyperLink.Text = "ENVIAR INGRESSO";               
                hyperLink.CssClass = "ingresso_enviar";
                hyperLink.Target = "_blank";
                hyperLink.Attributes.Add("onclick", "javascript:AbrirSolicitacaoIngresso('" + Criptografia.Criptografar(dr[0].ToString(), sChave, sVetorInicializacao) + "')");
                e.Row.Cells[4].Controls.Add(hyperLink);
            }
        }

    }
}
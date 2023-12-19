using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Model.objetos;
using Controller;
using System.Configuration;

public partial class site_default : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
        }

        CarregarEventos();
    }
    private void ExibirMensagemPopUpPositivo(string sMensagem)
    {
        sMensagem = sMensagem.Replace("'", "");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "PopUp('" + sMensagem + "','success')", true);
    }
    private void ExibirMensagemPopUpNegativo(string sMensagem)
    {
        sMensagem = sMensagem.Replace("'", "");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "PopUp('" + sMensagem + "','error')", true);
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
            swipeItens.Text += $"<div class='swiper-slide card mb-4 box-shadow'>" +
                                      "<a href='" + sUrl + "'>" +
                                      "<img class='card-img-top' src='" + src + "' alt=''>" +
                                      "</a>" +
                                      "<a target='_blank' href='" + sUrl + "'>" +
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

    protected void img1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Evento_FestaJunina/index.aspx");
    }
}

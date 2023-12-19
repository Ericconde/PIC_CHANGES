using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.objetos;
using System.Configuration;
using System.Data;
using Controller;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["LimitarComprasSimultaneas"].ToString() == "Sim")
        {
            vendaCTL CVenda = new vendaCTL();

            DataSet dataSet = CVenda.RetornarComprasEmAndamento(Convert.ToInt32(ConfigurationManager.AppSettings["LimitarIDEdicao"].ToString()));
            int iComprasEmAndamento = Convert.ToInt32(dataSet.Tables[0].Rows[0][0].ToString());
            int iComprasSimultaneasPermitidas = Convert.ToInt32(dataSet.Tables[1].Rows[0][0].ToString());

            if (iComprasEmAndamento >= iComprasSimultaneasPermitidas)
            {
                Response.Redirect("fila.aspx");
            }
        }

        usuarioCTL CUsuario = new usuarioCTL();
        string sSiteCarga = CUsuario.RetornarSiteMenorCarga();

        string sEnderecoBalanceamentoCarga = ConfigurationManager.AppSettings["EnderecoBalanceamentoCarga"].ToString();
        string sUrl = "http://" + sSiteCarga + "." + sEnderecoBalanceamentoCarga;

        CUsuario.AtualizarSiteCarga(sSiteCarga);

        Response.Redirect(sUrl);
    }
}
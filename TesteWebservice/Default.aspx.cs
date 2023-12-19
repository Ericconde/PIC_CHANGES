using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sRetorno = "";

        webservice.ingressospic CatracaVirtual = new webservice.ingressospic();
        webservice.usuario Usuario = new webservice.usuario();
        webservice.ingresso Ingresso = new webservice.ingresso();

        //Usuario = CatracaVirtual.AutenticarUsuario("rodrigolmti@gmail.com", "123456", "10.10.10.10", "Estacionamento");

        Ingresso = CatracaVirtual.RegistrarEntrada("EST074514826348196", 6618, "10.10.10", "Estacionamento");
        sRetorno = Ingresso.qrcode + "; " + Ingresso.mensagem + "; " + Ingresso.tipo;

        Response.Write(sRetorno);
    }
}
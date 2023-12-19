using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.objetos;
using System.Web.Security;
using Controller;

public partial class login_logout : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LiberarIngressos();
        }
        catch { }
        
        HttpContext.Current.Session.Abandon();
        HttpContext.Current.Session.Clear();

        Response.Redirect("../site/Default.aspx");
    }

    private void LiberarIngressos()
    {
        if (HttpContext.Current.Session["Venda"] != null)
        {
            venda Venda = (venda)HttpContext.Current.Session["Venda"];

            vendaCTL CVenda = new vendaCTL();
            CVenda.LiberarIngressos(-1, -1, Venda.IDVenda);
        }
    }
}

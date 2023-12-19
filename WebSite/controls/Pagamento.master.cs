using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controller;
using Model.objetos;
using System.Web.Security;

public partial class controls_Pagamento : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CarregarNomeUsuario();
            SelecionarMenu();
        }
        AdpaterMenu();
    }

    private void AdpaterMenu()
    {
        if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
        {
            Request.Browser.Adapters.Clear();
        }
    }

    private void SelecionarMenu()
    {
        foreach (Control c in menus.Controls)
        {
            if (c.GetType().ToString().Equals("System.Web.UI.WebControls.HyperLink"))
            {
                HyperLink hyperLink = (HyperLink)c;

                System.IO.FileInfo fileInfo = new System.IO.FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath);
                string sPageName = fileInfo.Name.ToLower();

                if (hyperLink.NavigateUrl.ToLower().IndexOf(sPageName) > -1)
                    hyperLink.CssClass = "menu_selecionado";
            }
        }
    }

    private void CarregarNomeUsuario()
    {
        try
        {
            usuario Usuario = new usuario();
            Usuario = (usuario)HttpContext.Current.Session["Usuario"];
            var customName = Usuario.Nome.ToUpper().Split(' ')[0].Substring(0, 1) + Usuario.Nome.ToUpper().Split(' ')[1].Substring(0, 1);
            lblCustomName.InnerText = customName;
            lblUsuario.InnerText = Usuario.Nome.ToUpper();
            if (Usuario.Nome.Length < 20)
                lblUsuario.Style.Add("font-size", "16px");
            else if (Usuario.Nome.Length <= 30)
                lblUsuario.Style.Add("font-size", "12px");
            else
                lblUsuario.Style.Add("font-size", "10px");
            var lblIP = "ACESSANDO PELO IP: " + HttpContext.Current.Request.ServerVariables["REMOTE_HOST"].ToString();
        }
        catch { }
    }
}

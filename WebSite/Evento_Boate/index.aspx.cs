using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class eventos_Boate_index : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsPostBack)
            {
                string sPagina = Request.AppRelativeCurrentExecutionFilePath;
                string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

                if (!usuarioCTL.PermitirAcesso(true, true, true, true, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");

                LiberarCadeiras();
            }
        }
    }

    private void LiberarCadeiras()
    {
        vendaCTL CVenda = new vendaCTL();
        CVenda.LiberarCadeiras();
    }
}
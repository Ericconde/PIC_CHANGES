<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        //c�digo para pegar a informa��o de conex�o com o banco
        //de dados para inicializar a conex�o com o banco pelo 
        //framework 
        //a string de conex�o est� no web.config
        try
        {
            string strConexao = "";
            strConexao = ConfigurationManager.AppSettings["Con"].ToString();
            PontoBr.Banco.SqlServer.Inicializar(strConexao);
        }
        catch
        {
            throw new Exception("String de conex�o inv�lida. Verifique a configura��o da aplica��o.");
        }
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        PontoBr.Banco.SqlServer.Finalizar();
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
       
    }

    void Session_OnEnd(object sender, EventArgs e)
    {
        
    }

    protected void Application_AcquireRequestState(Object sender, EventArgs e)
    {
       
    }
       
</script>

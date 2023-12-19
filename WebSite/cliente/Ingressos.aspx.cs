using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controller;
using Model.objetos;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;
using System.Web.Security;

public partial class cliente_Ingressos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //GerarIngressos(40916, null);
        //return;

        if (!IsPostBack)
        {
            try
            {
                if (!String.IsNullOrEmpty(Request.QueryString["IDVenda"]))
                {
                    string sIDVenda = "";
                    PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                    string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                    string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
                    
                    sIDVenda = Criptografia.Descriptografar(Request.QueryString["IDVenda"], sChave, sVetorInicializacao);
                    
                    if (!ValidarToken(sIDVenda))
                    {
                        Response.Write("Token inválido!<br/><br/>Acesse o sistema e tente novamente.");
                        return;
                    }

                    string sTicketsConvidado = String.Empty;
                    if (!String.IsNullOrEmpty(Request.QueryString["TicketsConvidado"]))
                        sTicketsConvidado = Request.QueryString["TicketsConvidado"];

                    GerarIngressos(Convert.ToInt32(sIDVenda), sTicketsConvidado);
                }
                else
                    Response.Write("Erro ao gerar ingressos!");
            }
            catch
            {
                Response.Write("Erro ao gerar ingresso!");
            }
        }
    }

    private bool ValidarToken(string sIDVenda)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["fisco"])) //Perfil "Fisco" não precisa de token
            return true;

        if (!String.IsNullOrEmpty(Request.QueryString["token"]))
        {
            usuario Usuario = new usuario();
            usuarioCTL CUsuario = new usuarioCTL();

            venda Venda = new venda();
            vendaCTL CVenda = new vendaCTL();

            Venda = CVenda.RetornarVenda(Convert.ToInt32(sIDVenda), null);
            Usuario = CUsuario.RetornarUsuario(Venda.IDUsuario);

            if (Request.QueryString["token"].ToString() != Usuario.Token) 
                return false;
            else
                return true;
        }
        else
            return false;
    }

    private void GerarIngressos(int iIDVenda, string sTicketsConvidado)
    {
        vendaCTL CVenda = new vendaCTL();
        DataTable dataTable = new DataTable();

        if (!String.IsNullOrEmpty(sTicketsConvidado))
        {
            dataTable = CVenda.RetornarIngressos(iIDVenda, sTicketsConvidado);
        }
        else
        {
            dataTable = CVenda.RetornarIngressos(iIDVenda, null);
            if (!String.IsNullOrEmpty(Request.QueryString["arquivo"]))
            {
                int iIngressoFinal = Convert.ToInt32(Request.QueryString["arquivo"]) * 10;
                int iIngressoInicial = iIngressoFinal - 9;

                string sTickets = "";
                int iIngressoAtual = 0;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    iIngressoAtual++;
                    if (iIngressoAtual >= iIngressoInicial && iIngressoAtual <= iIngressoFinal)
                    {
                        if (!String.IsNullOrEmpty(sTickets)) sTickets += ",";
                        sTickets += dataRow["Ticket"].ToString();
                    }
                }
                dataTable = CVenda.RetornarIngressos(iIDVenda, sTickets);
            }
        }

        dataTable = RetornarColunasDescricao(dataTable);
        dataTable.Columns.Add("QRUrl", typeof(string));
        dataTable.Columns.Add("Image", typeof(string));

        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        //foreach para gerar os endereços de QRCode e os diretórios das imagens de cada evento
        if (String.IsNullOrEmpty(Request.QueryString["fisco"])) //Perfil "Fisco" não pode visualizar o QRCode
            foreach (DataRow dataRow in dataTable.Rows)
                dataRow["QRUrl"] = RetornarUrlQRCode(dataRow["IdentidadeEletronica"].ToString()); //Retorna o endereço de cada QRCode.

        //Envia a tabela para o repeater do aspx.
        rptTicketsIngresso.DataSource = dataTable;
        rptTicketsIngresso.DataBind();
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

    protected void cmdImprimir_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.print();</script>");
    }

    private DataTable RetornarColunasDescricao(DataTable dataTable)
    {
        dataTable.Columns.Add("Setor", typeof(string));
        dataTable.Columns.Add("Mesa", typeof(string));
        dataTable.Columns.Add("Cadeira", typeof(string));
        dataTable.Columns.Add("Avulso", typeof(string));
        dataTable.Columns.Add("TipoSocio", typeof(string));
        dataTable.Columns.Add("TipoIngressoTitulo", typeof(string));
        dataTable.Columns.Add("Socio", typeof(string));
        dataTable.Columns.Add("CorIngresso", typeof(string));

        foreach (DataRow dataRow in dataTable.Rows)
        {
            dataRow["Hora"] = dataRow["Hora"].ToString().Substring(0, 2) + "h";
            string[] descriçao = dataRow["Tipo"].ToString().Split(';');
            string sTipo = descriçao[0].Substring(descriçao[0].Length - 6);
            dataRow["TipoSocio"] = descriçao[0].ToString();

            if (Convert.ToInt32(dataRow["NumeroCota"]) != 0 && dataRow["DigitoCota"] != null)
                dataRow["Socio"] = dataRow["NumeroCota"].ToString() + "-" + dataRow["DigitoCota"].ToString();
            else
                dataRow["Socio"] = "-";

            if (dataRow["CPF"].ToString() != "Cortesia")
                dataRow["CPF"] = dataRow["CPF"].ToString().Substring(0, 9) + "-" + dataRow["CPF"].ToString().Substring(dataRow["CPF"].ToString().Length - 2);

            if (descriçao.Length > 1)
            {
                dataRow["Setor"] = descriçao[1].Replace("setor", String.Empty).Trim();
                dataRow["Mesa"] = descriçao[2].Replace("mesa", String.Empty).Trim();
                dataRow["Cadeira"] = descriçao[3].Replace("cadeira", String.Empty).Trim();
            }
            else
            {
                dataRow["Setor"] = "Não possui";
                dataRow["Mesa"] = "Não possui";
                dataRow["Cadeira"] = "Não possui";
            }

            if (dataRow["Evento"].ToString() == "Boate")
            {
                dataRow["TipoIngressoTitulo"] = dataRow["TipoSocio"].ToString();
                // para definir o titulo do tipo de Ingresso
                if (dataRow["TipoIngressoTitulo"].ToString() == "Inteira sócio cadeira" || dataRow["TipoIngressoTitulo"].ToString() == "Inteira sócio  avulso" || dataRow["TipoIngressoTitulo"].ToString() == "Cortesia sócio Aniversariante")
                {
                    dataRow["TipoIngressoTitulo"] = "SÓCIO";
                }

                if (dataRow["TipoIngressoTitulo"].ToString() == "Inteira não sócio cadeira" || dataRow["TipoIngressoTitulo"].ToString() == "Inteira não sócio avulso" || dataRow["TipoIngressoTitulo"].ToString() == "Cortesia não sócio Aniversariante")
                {
                    dataRow["TipoIngressoTitulo"] = "NÃO SÓCIO";
                }

                if (dataRow["Setor"].ToString() == "Não possui" && dataRow["Mesa"].ToString() == "Não possui" && dataRow["Cadeira"].ToString() == "Não possui")
                    dataRow["Avulso"] = "Sim";
                else
                    dataRow["Avulso"] = "Não";

                // esses tipos não estão sendo usados no momento (masculino e feminino).
                if (dataRow["TipoSocio"].ToString() == "Sócio Masculino" || dataRow["TipoSocio"].ToString() == "Não sócio Masculino" || dataRow["TipoSocio"].ToString() == "Venda presencial Masculino")
                {
                    dataRow["CorIngresso"] = "#A9BCF5";
                }
                else if (dataRow["TipoSocio"].ToString() == "Sócio Feminino" || dataRow["TipoSocio"].ToString() == "Não sócio Feminino" || dataRow["TipoSocio"].ToString() == "Venda presencial Feminino")
                {
                    dataRow["CorIngresso"] = "#ECCEF5";
                }
                else
                {
                    dataRow["CorIngresso"] = "#D8D8D8";
                }

                // retira o termo INTEIRA
                string sTipoIngresso = dataRow["TipoSocio"].ToString().Replace("Inteira", "");
                dataRow["TipoSocio"] = sTipoIngresso;
            }
            else
            {
                if (sTipo == "avulso")
                    dataRow["Avulso"] = "Sim";
                else
                    dataRow["Avulso"] = "Não";

                dataRow["CorIngresso"] = "#D8D8D8";
            }
        }
        return dataTable;
    }
}
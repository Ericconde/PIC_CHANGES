using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Model.objetos;
using Model.dados;
using Model.negocios;
using Controller;

public partial class clube_CatracaVirtual : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(false, false, false, false, false, true, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            LimparLabels();
        }
        txtIdentidadeEletronica.Focus();
    }

    private void LimparLabels()
    {
        lblDataHoraEntrada.Text = string.Empty;
        lblNome.Text = string.Empty;
        lblDataHoraEntrada.Text = string.Empty;
        lblTicket.Text = string.Empty;
        lblCota.Text = string.Empty;
        lblResumo.Text = string.Empty;
        lblMensagem.Text = string.Empty;

        lblTicketTela.Visible = false;
        lblNomeTela.Visible = false;
        lblDataTela.Visible = false;
        lblCotaTela.Visible = false;
        lblResumoTela.Visible = false;
        lblMensagem.Visible = false;
    }

    protected void cmdPesquisar_Click(object sender, EventArgs e)
    {
        if (PodePesquisar())
        {
            Pesquisar();
        }
    }

    private bool PodePesquisar()
    {
        if (txtIdentidadeEletronica.Text == "")
        {
            lblMensagem.ForeColor = System.Drawing.Color.Red;
            lblMensagem.Font.Size = 14;
            lblMensagem.Visible = true;
            lblMensagem.Text = "Favor Preencher o Campo Identidade Eletrônica.";
            lblMensagem.Focus();
            return false;
        }

        return true;
    }

    private void Pesquisar()
    {
        LimparLabels();

        string sIDentidadeEletronica = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtIdentidadeEletronica.Text);
        vendaCTL CVenda = new vendaCTL();
        catracaCTL CCatraca = new catracaCTL();
        DataTable dtIngresso = CCatraca.VerificarIngressoPosCatraca(sIDentidadeEletronica);

        if (dtIngresso.Rows.Count > 0)
        {
            string sDataHora = Convert.ToString(dtIngresso.Rows[0]["DataHoraEntrada"]);
            string sCatraca = Convert.ToString(dtIngresso.Rows[0]["Catraca"]);

            if (sDataHora != "" && sCatraca != "")
            {
                lblMensagem.Visible = true;
                lblMensagem.Text = " <p style='font-family: Arial, Helvetica, sans-serif; ";
                lblMensagem.Text += " color: RED !important; ";
                lblMensagem.Text += " font-size: 20pt; ";
                lblMensagem.Text += " text-align: center;'> ";
                lblMensagem.Text += "     <strong>ENTRADA NÃO AUTORIZADA!</strong></p> ";
                lblMensagem.Text += " <p style='font-family: Arial, Helvetica, sans-serif; ";
                lblMensagem.Text += "         color: RED !important; ";
                lblMensagem.Text += "         font-size: 14pt; ";
                lblMensagem.Text += "         text-align: center;'> ";
                lblMensagem.Text += "     (Ticket  ";
                lblMensagem.Text += "     de Ingresso não localizado)</p> ";
                lblMensagem.Text += " <p style='text-align: center;'> ";
                lblMensagem.Text += "     <img alt='' src='../images/check_Invalido.png'  /></p> ";
                lblMensagem.Text += " <p style='font-family: Arial, Helvetica, sans-serif; ";
                lblMensagem.Text += "         text-align: center; ";
                lblMensagem.Text += "         font-size: 14pt;'> ";
                lblMensagem.Text += "     &nbsp;</p> ";
                lblMensagem.Text += " <p style='font-family: Arial, Helvetica, sans-serif; ";
                lblMensagem.Text += "         text-align: center; ";
                lblMensagem.Text += "         font-size: 14pt;'> ";
                lblMensagem.Text += "     </p> ";

                lblTicketTela.Visible = true;
                lblNomeTela.Visible = true;
                lblDataTela.Visible = true;
                lblCotaTela.Visible = true;
                lblResumoTela.Visible = true;

                lblTicket.Text = Convert.ToString(dtIngresso.Rows[0]["Ticket"]);
                lblNome.Text = Convert.ToString(dtIngresso.Rows[0]["Nome"]);
                lblDataHoraEntrada.Text = Convert.ToString(dtIngresso.Rows[0]["DataHoraEntrada"]);

                lblCota.Text = Convert.ToString(dtIngresso.Rows[0]["NumeroCota"]);
                lblResumo.Text = Convert.ToString(dtIngresso.Rows[0]["Tipo"]);
            }
            else
            {
                usuario Usuario = (usuario)HttpContext.Current.Session["Usuario"];

                lblMensagem.Visible = true;
                lblMensagem.Text = " <p style='font-family: Arial, Helvetica, sans-serif; ";
                lblMensagem.Text += " color: #009933 !important; ";
                lblMensagem.Text += " font-size: 20pt; ";
                lblMensagem.Text += " text-align: center;'> ";
                lblMensagem.Text += " <strong>ENTRADA AUTORIZADA!</strong></p> ";
                lblMensagem.Text += " <p style='text-align: center;'> ";
                lblMensagem.Text += "     <img alt='' src='../images/check_Valido.png'  /></p> ";
                lblMensagem.Text += " <p style='font-family: Arial, Helvetica, sans-serif; ";
                lblMensagem.Text += "         text-align: center;'> ";
                lblMensagem.Text += "    </p> ";
                CCatraca.AtualizarIngressoPosCatraca(sIDentidadeEletronica, Usuario.IDUsuario);
                DataTable dtRetorno = CCatraca.VerificarIngressoPosCatraca(sIDentidadeEletronica);

                lblTicketTela.Visible = true;
                lblNomeTela.Visible = true;
                lblDataTela.Visible = true;
                lblCotaTela.Visible = true;
                lblResumoTela.Visible = true;

                lblTicket.Text = Convert.ToString(dtRetorno.Rows[0]["Ticket"]);
                lblNome.Text = Convert.ToString(dtRetorno.Rows[0]["Nome"]);
                lblDataHoraEntrada.Text = Convert.ToString(dtRetorno.Rows[0]["DataHoraEntrada"]);

                lblCota.Text = Convert.ToString(dtRetorno.Rows[0]["NumeroCota"]);
                lblResumo.Text = Convert.ToString(dtRetorno.Rows[0]["Tipo"]);
            }
        }
        else
            if (dtIngresso.Rows.Count == 0)
        {
            lblMensagem.Visible = true;
            lblMensagem.ForeColor = System.Drawing.Color.Red;
            lblMensagem.Font.Size = 14;
            lblMensagem.Text = "Identidade Eletrônica não encontrada.";
        }

        txtIdentidadeEletronica.Text = "";
        txtIdentidadeEletronica.Focus();
    }

    private void Limparcampos()
    {
        txtIdentidadeEletronica.Text = string.Empty;
    }


    protected void cmdLimpar_Click(object sender, EventArgs e)
    {
        Limparcampos();
        LimparLabels();
    }
}
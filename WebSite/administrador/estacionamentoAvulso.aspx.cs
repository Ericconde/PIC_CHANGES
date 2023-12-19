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

public partial class administrador_estacionamentoAvulso : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, false, false, false, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }

        if (!IsPostBack)
        {
            CarregarEventosEdicao();
            CarregarEstacionamentosAvulsos();
        }
    }

    private void CarregarEventosEdicao()
    {
        eventoCTL evento = new eventoCTL();
        evento.CarregarDropDownListEventosEdicao(dropEdicao, false, true);
    }

    protected override void OnPreInit(EventArgs e)
    {
        if (Session["usuario"] == null) Response.Redirect("../login/logout.aspx");

        usuario UsuarioLogado = (usuario)Session["usuario"];

        if (UsuarioLogado.Perfil == "Administrador")
        {
            MasterPageFile = "~/controls/Admin.master";
        }
        else if (UsuarioLogado.Perfil == "Vendedor")
        {
            MasterPageFile = "~/controls/Vendedor.master";
        }
    }
   
    protected void cmdVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }

    private void LimparCampos()
    {
        txtNome.Text = String.Empty;
        txtCPF.Text = String.Empty;
        CarregarEstacionamentosAvulsos();
    }

    private bool PodeSalvar()
    {
        if (dropEdicao.SelectedValue == "-1")
        {
            ExibirMensagem("Selecione [Evento].");
            return false;
        }
        if (string.IsNullOrEmpty(txtNome.Text.Trim()))
        {
            ExibirMensagem("Preencha ou selecione [Nome].");
            return false;
        }
        if (PontoBr.Utilidades.String.ValidarCPF(txtCPF.Text) == false)
        {
            ExibirMensagem("[CPF] inválido.");
            return false;
        }

        return true;
    }

    private void CarregarEstacionamentosAvulsos()
    {
        vendaCTL CVenda = new vendaCTL();
        CVenda.CarregarGridViewEstacionamentosAvulsos(grdEstacionamento, Convert.ToInt32(dropEdicao.SelectedValue));

        lblNumeroLinhas.Text = "| " + grdEstacionamento.Rows.Count.ToString() + " registro(s) |";
    }

    private void ExibirMensagem(string sMensagem)
    {
        sMensagem = sMensagem.Replace("'", "");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + sMensagem + "')", true);
    }

    protected void cmdSalvar_Click(object sender, EventArgs e)
    {
        if (PodeSalvar())
        {
            int iIDEdicao = Convert.ToInt32(dropEdicao.SelectedValue);
            string sNome = PontoBr.Utilidades.String.RemoverCaracterInvalido(txtNome.Text.Trim());
            string sCPF = PontoBr.Utilidades.String.RemoverCaracteresMascara(txtCPF.Text);
            usuario UsuarioLogado = (usuario)Session["usuario"];

            Random random = new Random();
            string sIDIdentidadeEletronica = "EST" + sCPF + iIDEdicao.ToString() + random.Next(100, 999).ToString();
            sIDIdentidadeEletronica = sIDIdentidadeEletronica.Substring(0, 18);

            vendaCTL CVenda = new vendaCTL();
            CVenda.CadastrarEstacionamentoAvulso(sIDIdentidadeEletronica, iIDEdicao, sNome, sCPF, UsuarioLogado.IDUsuario);

            LimparCampos();
            ExibirMensagem("Ticket de estacionamento gerado com sucesso!");     
        }
    }

    protected void grdEstacionamento_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
            string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
            string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
            string sIdentidadeEletronica = Criptografia.Criptografar(grdEstacionamento.DataKeys[e.Row.RowIndex].Values[0].ToString(), sChave, sVetorInicializacao);

            e.Row.Cells[6].Attributes.Add("onClick", "window.open('../cliente/ticketEstacionamento.aspx?IdentidadeEletronica=" + sIdentidadeEletronica + "', 'Pagina')");
         }
    }

    protected void dropEdicao_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregarEstacionamentosAvulsos();
    }
}

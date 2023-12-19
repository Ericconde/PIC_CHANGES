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
using Cielo.Enums;
using Cielo.Responses.Exceptions;
using Cielo.Requests.Entites.Common;
using Cielo.Requests.Entites;

public partial class administrador_alterarEstacionamento : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpContext.Current.Session["Venda"] = null;
        }

        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, false, false, false, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
        }
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

    private void CarregarVenda()
    {
        try
        {
            venda Venda = new venda();
            vendaCTL CVenda = new vendaCTL();
            Venda = CVenda.RetornarVenda(Convert.ToInt32(txtVoucher.Text), "1,2,3,4,5");

            if (Venda.IDVenda != 0 && Venda.IDVenda != -1)
            {
                dropVagasEstacionamento.SelectedValue = Venda.NumeroVagas.ToString();
                HabilitarCampos();
            }
        }
        catch (Exception ex)
        {
            PontoBr.Utilidades.Diversos.ExibirAlertaScriptManager(ex.Message, this.Page);
        }
    }

    private void HabilitarCampos()
    {
        lblDesejaEstacionamento.Visible = true;
        dropVagasEstacionamento.Visible = true;
    }
    private void desabilitarCampos()
    {
        lblDesejaEstacionamento.Visible = false;
        dropVagasEstacionamento.Visible = false;
    }

    protected void cmdVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("default.aspx");
    }
    
    protected void cmdPesquisar_Click(object sender, EventArgs e)
    {
        if (PodePesquisar())
        {
            CarregarVenda();
        }
    }

    private bool PodePesquisar()
    {
        if (String.IsNullOrEmpty(txtVoucher.Text.Trim()))
        {
            ExibirMensagem("Preencha [Núm. Voucher] para pesquisar.");
            return false;
        }
        return true;
    }

    private bool PodeSalvar()
    {
        if (String.IsNullOrEmpty(txtVoucher.Text.Trim()))
        {
            ExibirMensagem("Preencha [Núm. Voucher] para pesquisar.");
            return false;
        }

        venda Venda = new venda();
        vendaCTL CVenda = new vendaCTL();
        Venda = CVenda.RetornarVenda(Convert.ToInt32(txtVoucher.Text), "1,2,3,4,5");
        if (Venda.IDVenda == 0 || Venda.IDVenda == -1)
        {
            ExibirMensagem("[Núm. Voucher] não localizado.");
            return false;
        }

        return true;
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
            int iIDVenda = String.IsNullOrEmpty(txtVoucher.Text.Trim()) ? -1 : Convert.ToInt32(PontoBr.Utilidades.String.RemoverCaracterInvalido(txtVoucher.Text.Trim()));
            int iNumeroVagas = Convert.ToInt32(dropVagasEstacionamento.SelectedValue);
           
            usuario UsuarioLogado = (usuario)Session["usuario"];

            vendaCTL CVenda = new vendaCTL();
            CVenda.AtualizarVenda(iNumeroVagas, UsuarioLogado.IDUsuario, iIDVenda);

            CVenda.ExcluirIngressoEstacionamento(iIDVenda);

            venda Venda = CVenda.RetornarVenda(iIDVenda, null);
            CadastrarEstacionamento(Venda);

            txtVoucher.Text = String.Empty;
            dropVagasEstacionamento.SelectedValue = "0";
            desabilitarCampos();

            ExibirMensagem("Venda atualizada com sucesso!");     
        }
    }

    private void CadastrarEstacionamento(venda Venda)
    {
        //Vagas de estacionamento
        vendaCTL CVenda = new vendaCTL();
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        for (int iNumeroVaga = 1; iNumeroVaga <= Venda.NumeroVagas; iNumeroVaga++)
        {
            string sIDIdentidadeEletronica = "EST" + iNumeroVaga.ToString() + Venda.IDVenda.ToString() + DateTime.Now.Ticks.ToString();
            sIDIdentidadeEletronica = sIDIdentidadeEletronica.Substring(0, 18);
            CVenda.CadastrarIngressoEstacionamento(Venda.IDVenda, Venda.IDEdicao, 0, sIDIdentidadeEletronica, Venda.Nome, Venda.CPF, UsuarioLogado.IDUsuario);
        }
    }
}

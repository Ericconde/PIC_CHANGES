using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Model.negocios;
using System.Data;
using Model.objetos;

namespace Controller
{
    public class relatorioCTL
    {
        public DataSet RetornarDashboard(int iIDEdicao)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarDashboard(iIDEdicao);
        }

        public DataTable RetornarVoucher(int iIDVenda)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarVoucher(iIDVenda);
        }

        public DataTable RetornarIngressosCatracas(int iIDEdicao, string sNomeCliente, int iIDVenda)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarIngressosCatracas(iIDEdicao, sNomeCliente, iIDVenda);
        }

        public DataTable RetornarLogAcesso(string sDataInicial, string sDataFinal, string sNomeUsuario)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarLogAcesso(sDataInicial, sDataFinal, sNomeUsuario);
        }

        public DataTable RetornarClientesCadastrados(string sDataInicial, string sDataFinal, string sNomeUsuario, string sCPF)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarClientesCadastrados(sDataInicial, sDataFinal, sNomeUsuario, sCPF);
        }

        public DataTable RetornarVendasDetalhado(string sDataInicial, string sDataFinal, int iIDEdicao, int iLote, string sPerfil, int iIDFormaPagamento, int iNumeroCota)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarVendasDetalhado(sDataInicial, sDataFinal, iIDEdicao, iLote, sPerfil, iIDFormaPagamento, iNumeroCota);
        }

        public DataTable RetornarVendasSintetico(string sDataInicial, string sDataFinal, string sIDEdicao)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarVendasSintetico(sDataInicial, sDataFinal, sIDEdicao);
        }

        public DataTable RetornarSocios(string sNome, string sCPF, int iNumeroCota)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarSocios(sNome, sCPF, iNumeroCota);
        }

        public DataTable RetornarResumoFormaPagamento(string sDataInicial, string sDataFinal, int iIDEdicao, int iLote, string sPerfil, int iIDFormaPagamento)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarResumoFormaPagamento(sDataInicial, sDataFinal, iIDEdicao, iLote, sPerfil, iIDFormaPagamento);
        }

        public DataTable RetornarResumoTipoIngresso(string sDataInicial, string sDataFinal, int iIDEdicao, int iLote, string sPerfil, int iIDFormaPagamento)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarResumoTipoIngresso(sDataInicial, sDataFinal, iIDEdicao, iLote, sPerfil, iIDFormaPagamento);
        }

        public DataTable RetornarCompradosSaldo(string sDataInicial, string sDataFinal, int iIDEdicao, int iLote, string sPerfil, int iIDFormaPagamento)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarCompradosSaldo(sDataInicial, sDataFinal, iIDEdicao, iLote, sPerfil, iIDFormaPagamento);
        }

        public DataTable RetornarVoucherDetalhado(int iIDVenda, string sDataInicial, string sDataFinal, int iIDEdicao, int iLote, string sPerfil, int iIDFormaPagamento)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarVoucherDetalhado(iIDVenda, sDataInicial, sDataFinal, iIDEdicao, iLote, sPerfil, iIDFormaPagamento);
        }

        public DataTable RetornarPagamentosCartao(int iIDVenda, string sNumeroCartao, string sCodigoAutorizacao, string sTid, string sNomeCliente, int iNumeroCota, string sTitular, int iIDEdicao, string sDataInicial, string sDataFinal)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarPagamentosCartao(iIDVenda, sNumeroCartao, sCodigoAutorizacao, sTid, sNomeCliente, iNumeroCota, sTitular, iIDEdicao, sDataInicial, sDataFinal);
        }

        public DataTable RetornarCancelamentosVoucher(string sDataInicial, string sDataFinal, int iIDEdicao, int iIDFormaPagamento, double dNumeroCota, string sDigito)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarCancelamentosVoucher(sDataInicial, sDataFinal, iIDEdicao, iIDFormaPagamento, dNumeroCota, sDigito);
        }

        public DataTable RetornarVouchersBaixados(string sDataInicial, string sDataFinal, int iIDEdicao, int iIDFormaPagamento, int iStatusVoucher, double dNumeroCota, string sDigito)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarVouchersBaixados(sDataInicial, sDataFinal, iIDEdicao, iIDFormaPagamento, iStatusVoucher, dNumeroCota, sDigito);
        }

        public DataTable RetornarRecibosPendentes(string sDataInicial, string sDataFinal, int iIDEdicao, int iIDFormaPagamento, double dNumeroCota, string sDigito)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarRecibosPendentes(sDataInicial, sDataFinal, iIDEdicao, iIDFormaPagamento, dNumeroCota, sDigito);
        }

        public DataTable RetornarVendaNaoAprovadaCartao(string sDataInicial, string sDataFinal, int iIDEdicao, string sIDPerfil, int iIDVenda)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornaVendaNaoAprovadaCartao(sDataInicial, sDataFinal, iIDEdicao, sIDPerfil, iIDVenda);
        }

        //public DataTable RetornarEstacionamento(string sNome, int iIDEdicao, int iIDVenda, string sStatus)
        //{
        //    relatorioBLL BRelatorio = new relatorioBLL();
        //    return BRelatorio.RetornarEstacionamento(sNome, iIDEdicao, iIDVenda, sStatus);
        //}

        //public DataTable RetornarEntradaIngresso(string sNome, int iIDEdicao, int iIDVenda, string sStatus)
        //{
        //    relatorioBLL BRelatorio = new relatorioBLL();
        //    return BRelatorio.RetornarEntradaIngresso(sNome, iIDEdicao, iIDVenda, sStatus);
        //}

        public DataTable RetornarTickets(int iIDEdicao, string sNome, string sCPF, int iNumeroCota, string sDataInicial, string sDataFinal, int iIDVenda, string sIDPerfil)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarTickets(iIDEdicao, sNome, sCPF, iNumeroCota, sDataInicial, sDataFinal, iIDVenda, sIDPerfil);
        }

        public DataTable RetornarCancelamentosTicket(string sDataInicial, string sDataFinal, int iIDEdicao, int iIDFormaPagamento, double dNumeroCota, string sDigito)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarCancelamentosTicket(sDataInicial, sDataFinal, iIDEdicao, iIDFormaPagamento, dNumeroCota, sDigito);
        }

        public DataTable RetornarIngressoCancelamento(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarIngressoCancelamento(sDataInicial, sDataFinal, iIDEdicao);
        }

        public DataTable RetornarSolicitacoesIngressos(int iIDEdicao)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarSolicitacoesIngressos(iIDEdicao);
        }

        public DataTable RetornarMapaMesas(int iIDEdicao)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarMapaMesas(iIDEdicao);
        }

        public DataTable RetornarAlteracoesEmail()
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarAlteracoesEmail();
        }

        //public DataSet RetornarStatusEstacionamento()
        //{
        //    relatorioBLL BRelatorio = new relatorioBLL();
        //    return BRelatorio.RetornarStatusEstacionamento();
        //}

        public DataSet RetornarStatusEntradaIngresso(int iIDEdicao)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarStatusEntradaIngresso(iIDEdicao);
        }

        //public DataTable RetornarIngressosCatracas(int iIDEdicao)
        //{
        //    relatorioBLL BRelatorio = new relatorioBLL();
        //    return BRelatorio.RetornarIngressosCatracas(iIDEdicao);
        //}

        public DataTable RetornarVendasDetalhadoBordero(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarVendasDetalhadoBordero(sDataInicial, sDataFinal, iIDEdicao);
        }

        public DataTable RetornarDadosEventoBordero(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarDadosEventoBordero(sDataInicial, sDataFinal, iIDEdicao);
        }

        public DataTable RetornarVendasSinteticoBordero(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarVendasSinteticoBordero(sDataInicial, sDataFinal, iIDEdicao);
        }

        public DataTable RetornarControleEntradaBordero(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarControleEntradaBordero(sDataInicial, sDataFinal, iIDEdicao);
        }

        public DataTable RetornarVendasFisco(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarVendasFisco(sDataInicial, sDataFinal, iIDEdicao);
        }

        public DataTable RetornarIngressosFisco(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarIngressosFisco(sDataInicial, sDataFinal, iIDEdicao);
        }

        public DataTable RetornarResumoPontoVendaFisco(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarResumoPontoVendaFisco(sDataInicial, sDataFinal, iIDEdicao);
        }

        public DataTable RetornarVendasConsolidadoFisco(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarVendasConsolidadoFisco(sDataInicial, sDataFinal, iIDEdicao);
        }

        public DataTable RetornarResumoCortesiaFisco(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarResumoCortesiaFisco(sDataInicial, sDataFinal, iIDEdicao);
        }

        public DataTable RetornarEntradaIngresso(string sNome, int iIDEdicao, int iIDVenda, string sStatus)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarEntradaIngresso(sNome, iIDEdicao, iIDVenda, sStatus);
        }

        public DataTable RetornarRelatorioVendasDetalhado(int iIDEdicao,string  sIDPerfil, int iIDFormaPagamento, int iIDLote, string sDataInicial, string sDataFinal)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarRelatorioVendasDetalhado(iIDEdicao, sIDPerfil, iIDFormaPagamento, iIDLote, sDataInicial, sDataFinal);
        }

        public DataTable RetornarVendasCondominio(int iIDEdicao, string sDataInicial, string sDataFinal, int iExportando)
        {
            relatorioBLL BRelatorio = new relatorioBLL();
            return BRelatorio.RetornarVendasCondominio(iIDEdicao, sDataInicial, sDataFinal, iExportando);
        }
    }
}

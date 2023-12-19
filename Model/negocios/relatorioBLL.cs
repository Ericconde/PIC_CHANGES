using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.objetos;
using Model.dados;
using System.Data;
using System.Web;

namespace Model.negocios
{
    public class relatorioBLL
    {
        public DataSet RetornarDashboard(int iIDEdicao)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarDashboard(iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataSet(sSql);
        }

        public DataTable RetornarVoucher(int iIDVenda)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarVoucher(iIDVenda);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarLogAcesso(string sDataInicial, string sDataFinal, string sNomeUsuario)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarLogAcesso(sDataInicial, sDataFinal, sNomeUsuario);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarIngressosCatracas(int iIDEdicao, string sNomeCliente, int iIDVenda)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarIngressosCatracas(iIDEdicao, sNomeCliente, iIDVenda);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarClientesCadastrados(string sDataInicial, string sDataFinal, string sNomeUsuario, string sCPF)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarClientesCadastrados(sDataInicial, sDataFinal, sNomeUsuario, sCPF);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarVendasDetalhado(string sDataInicial, string sDataFinal, int iIDEdicao, int iLote, string sPerfil, int iIDFormaPagamento, int iNumeroCota)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarVendasDetalhado(sDataInicial, sDataFinal, iIDEdicao, iLote, sPerfil, iIDFormaPagamento, iNumeroCota);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarVendasSintetico(string sDataInicial, string sDataFinal, string sIDEdicao)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarVendasSintetico(sDataInicial, sDataFinal, sIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarSocios(string sNome, string sCPF, int iNumeroCota)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarSocios(sNome, sCPF, iNumeroCota);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarResumoFormaPagamento(string sDataInicial, string sDataFinal, int iIDEdicao, int iLote, string sPerfil, int iIDFormaPagamento)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarResumoFormaPagamento(sDataInicial, sDataFinal, iIDEdicao, iLote, sPerfil, iIDFormaPagamento);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarResumoTipoIngresso(string sDataInicial, string sDataFinal, int iIDEdicao, int iLote, string sPerfil, int iIDFormaPagamento)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarResumoTipoIngresso(sDataInicial, sDataFinal, iIDEdicao, iLote, sPerfil, iIDFormaPagamento);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarCompradosSaldo(string sDataInicial, string sDataFinal, int iIDEdicao, int iLote, string sPerfil, int iIDFormaPagamento)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarCompradosSaldo(sDataInicial, sDataFinal, iIDEdicao, iLote, sPerfil, iIDFormaPagamento);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarVoucherDetalhado(int iIDVenda, string sDataInicial, string sDataFinal, int iIDEdicao, int iLote, string sPerfil, int iIDFormaPagamento)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarVoucherDetalhado(iIDVenda, sDataInicial, sDataFinal, iIDEdicao, iLote, sPerfil, iIDFormaPagamento);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarPagamentosCartao(int iIDVenda, string sNumeroCartao, string sCodigoAutorizacao, string sTid, string sNomeCliente, int iNumeroCota, string sTitular, int iIDEdicao, string sDataInicial, string sDataFinal)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarPagamentosCartao(iIDVenda, sNumeroCartao, sCodigoAutorizacao, sTid, sNomeCliente, iNumeroCota, sTitular, iIDEdicao, sDataInicial, sDataFinal);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarCancelamentosVoucher(string sDataInicial, string sDataFinal, int iIDEdicao, int iIDFormaPagamento, double dNumeroCota, string sDigito)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarCancelamentosVoucher(sDataInicial, sDataFinal, iIDEdicao, iIDFormaPagamento, dNumeroCota, sDigito);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarVouchersBaixados(string sDataInicial, string sDataFinal, int iIDEdicao, int iIDFormaPagamento, int iStatusVoucher, double dNumeroCota, string sDigito)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarVouchersBaixados(sDataInicial, sDataFinal, iIDEdicao, iIDFormaPagamento, iStatusVoucher, dNumeroCota, sDigito);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }


        public DataTable RetornarRecibosPendentes(string sDataInicial, string sDataFinal, int iIDEdicao, int iIDFormaPagamento, double dNumeroCota, string sDigito)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarRecibosPendentes(sDataInicial, sDataFinal, iIDEdicao, iIDFormaPagamento, dNumeroCota, sDigito);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornaVendaNaoAprovadaCartao(string sDataInicial, string sDataFinal, int iIDEdicao, string sIDPerfil, int iIDVenda)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornaVendaNaoAprovadaCartao(sDataInicial, sDataFinal, iIDEdicao, sIDPerfil, iIDVenda);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);

        }

        //public DataTable RetornarEstacionamento(string sNome, int iIDEdicao, int iIDVenda, string sStatus)
        //{
        //    relatorioDAL DRelatorio = new relatorioDAL();
        //    string sSql = DRelatorio.RetornarEstacionamento(sNome, iIDEdicao, iIDVenda, sStatus);

        //    return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        //}

        //public DataTable RetornarEntradaIngresso(string sNome, int iIDEdicao, int iIDVenda, string sStatus)
        //{
        //    relatorioDAL DRelatorio = new relatorioDAL();
        //    string sSql = DRelatorio.RetornarEntradaIngresso(sNome, iIDEdicao, iIDVenda, sStatus);

        //    return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        //}

        public DataTable RetornarTickets(int iIDEdicao, string sNome, string sCPF, int iNumeroCota, string sDataInicial, string sDataFinal, int iIDVenda, string sIDPerfil)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarTickets(iIDEdicao, sNome, sCPF, iNumeroCota, sDataInicial, sDataFinal, iIDVenda, sIDPerfil);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarCancelamentosTicket(string sDataInicial, string sDataFinal, int iIDEdicao, int iIDFormaPagamento, double dNumeroCota, string sDigito)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarCancelamentosTicket(sDataInicial, sDataFinal, iIDEdicao, iIDFormaPagamento, dNumeroCota, sDigito);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarIngressoCancelamento(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarIngressoCancelamento(sDataInicial, sDataFinal, iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarSolicitacoesIngressos(int iIDEdicao)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarSolicitacoesIngressos(iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarMapaMesas(int iIDEdicao)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarMapaMesas(iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarAlteracoesEmail()
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarAlteracoesEmail();

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        //public DataSet RetornarStatusEstacionamento()
        //{
        //    relatorioDAL DRelatorio = new relatorioDAL();
        //    string sSql = DRelatorio.RetornarStatusEstacionamento();

        //    return PontoBr.Banco.SqlServer.RetornarDataSet(sSql);
        //}

        public DataSet RetornarStatusEntradaIngresso(int iIDEdicao)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarStatusEntradaIngresso(iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataSet(sSql);
        }

        //public DataTable RetornarIngressosCatracas(int iIDEdicao)
        //{
        //    relatorioDAL DRelatorio = new relatorioDAL();
        //    string sSql = DRelatorio.RetornarIngressosCatracas(iIDEdicao);

        //    return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        //}

        public DataTable RetornarVendasDetalhadoBordero(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarVendasDetalhadoBordero(sDataInicial, sDataFinal, iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarDadosEventoBordero(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarDadosEventoBordero(sDataInicial, sDataFinal, iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarVendasSinteticoBordero(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarVendasSinteticoBordero(sDataInicial, sDataFinal, iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarControleEntradaBordero(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarControleEntradaBordero(sDataInicial, sDataFinal, iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarVendasFisco(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarVendasFisco(sDataInicial, sDataFinal, iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarIngressosFisco(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarIngressosFisco(sDataInicial, sDataFinal, iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarResumoPontoVendaFisco(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarResumoPontoVendaFisco(sDataInicial, sDataFinal, iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarVendasConsolidadoFisco(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarVendasConsolidadoFisco(sDataInicial, sDataFinal, iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarResumoCortesiaFisco(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarResumoCortesiaFisco(sDataInicial, sDataFinal, iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarEntradaIngresso(string sNome, int iIDEdicao, int iIDVenda, string sStatus)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarEntradaIngresso(sNome, iIDEdicao, iIDVenda, sStatus);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarRelatorioVendasDetalhado(int iIDEdicao, string sIDPerfil, int iIDFormaPagamento, int iIDLote, string sDataInicial, string sDataFinal)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarRelatorioVendasDetalhado(iIDEdicao, sIDPerfil, iIDFormaPagamento, iIDLote, sDataInicial, sDataFinal);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarVendasCondominio(int iIDEdicao, string sDataInicial, string sDataFinal, int iExportando)
        {
            relatorioDAL DRelatorio = new relatorioDAL();
            string sSql = DRelatorio.RetornarVendasCondominio(iIDEdicao, sDataInicial, sDataFinal, iExportando);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }
    }
}


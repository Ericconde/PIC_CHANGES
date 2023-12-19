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
    public class vendaBLL
    {
        public DataSet RetornarCarrinhoCompras(int iIDVenda)
        { 
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarCarrinhoCompras(iIDVenda);

            return PontoBr.Banco.SqlServer.RetornarDataSet(sSql);
        }        

        public DataSet RetornarCarrinhoComprasTesteCarga(int iIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarCarrinhoComprasTesteCarga(iIDVenda);

            return PontoBr.Banco.SqlServer.RetornarDataSet(sSql);
        }

        public void ReservarCadeira(venda Venda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.ReservarCadeira(Venda);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public DataTable RetornarStatusCadeiras(int iIDEdicao, string sSetor)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarStatusCadeiras(iIDEdicao, sSetor);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarCadeira(venda Venda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarCadeira(Venda);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public int CadastrarVenda(venda Venda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.CadastrarVenda(Venda);

            return Convert.ToInt32(PontoBr.Banco.SqlServer.ExecutarSqlComRetornoDeIdentity(sSql));
        }

        public void CadastrarVendaAvulso(venda Venda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.CadastrarVendaAvulso(Venda);

            PontoBr.Banco.SqlServer.ExecutarSqlComRetornoDeIdentity(sSql);
        }
        public void CancelarTicket(double dTicket, string sTextoLog, int iIDUsuario)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.CancelarTicket(dTicket, sTextoLog, iIDUsuario);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public void LiberarIngressos(int iIDCliente, int iIDEdicao, int iIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.LiberarIngressos(iIDCliente, iIDEdicao, iIDVenda);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }
        public void LiberarIngressosPresenciais(int iIDCliente, int iIDEdicao, int iIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.LiberarIngressosPresenciais(iIDCliente, iIDEdicao, iIDVenda);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public void LiberarIngressos(int iIDCliente)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.LiberarIngressos(iIDCliente);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public void LiberarCadeiras()
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.LiberarCadeiras();

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public void CancelarVenda(int iIDVenda, int iIDUsuarioCancelamento)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.CancelarVenda(iIDVenda, iIDUsuarioCancelamento);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public void LiberarCadeira(venda Cadeira)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.LiberarCadeira(Cadeira);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }        

        public void ConcluirVenda(int iIDVenda, int iIDFormaPagamento, string sDetalhesFormaPagamento, int iNumeroVagas,
            int iNumeroCriancas, string sNomeRetirada, string sRgRetirada, int iIDLocalRetirada, string sNomeRetirada2, string sRgRetirada2)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.ConcluirVenda(iIDVenda, iIDFormaPagamento, sDetalhesFormaPagamento, iNumeroVagas, iNumeroCriancas, sNomeRetirada, sRgRetirada, iIDLocalRetirada, sNomeRetirada2, sRgRetirada2);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public void AtualizarVendaComprovanteCaixa(int iIDVenda, int iIDFormaPagamento, string sDetalhesFormaPagamento, int iNumeroVagas,
            int iNumeroCriancas, string sNomeRetirada, string sRgRetirada, int iIDLocalRetirada, string sNomeSegundoRetirada, string sRgSegundoRetirada)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.AtualizarVendaComprovanteCaixa(iIDVenda, iIDFormaPagamento, sDetalhesFormaPagamento, iNumeroVagas, iNumeroCriancas, sNomeRetirada, sRgRetirada, iIDLocalRetirada, sNomeSegundoRetirada, sRgSegundoRetirada);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public void AtualizarVendaReservada(int iIDVenda, int iIDFormaPagamento, string sDetalhesFormaPagamento, int iNumeroVagas,
            int iNumeroCriancas, string sNomeRetirada, string sRgRetirada, int iIDLocalRetirada, string sNomeSegundoRetirada, string sRgSegundoRetirada)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.AtualizarVendaReservada(iIDVenda, iIDFormaPagamento, sDetalhesFormaPagamento, iNumeroVagas, iNumeroCriancas, sNomeRetirada, sRgRetirada, iIDLocalRetirada, sNomeSegundoRetirada, sRgSegundoRetirada);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public DataTable RetornarVendasCliente(int iIDCliente)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarVendasCliente(iIDCliente);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarVendas(int iIDVenda, string sNome, string sCPF, string sIDStatusCompra)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarVendas(iIDVenda, sNome, sCPF, sIDStatusCompra);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarVenda(int iIDVenda, string sIDStatusCompra)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarVenda(iIDVenda, sIDStatusCompra);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarIngressosRecibo(int iIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarIngressosRecibo(iIDVenda);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public void EntregarIngresso(int iIDVenda, int iIDUsuarioEntregaIngresso)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.EntregarIngresso(iIDVenda, iIDUsuarioEntregaIngresso);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public string RetornarFormasPagamento(string sIDFormaPagamento)
        {
            vendaDAL DVenda = new vendaDAL();
            return DVenda.RetornarFormasPagamento(sIDFormaPagamento);
        }

        public int RetornarQuantidadeVendidaCadeira(int iIDEdicao)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarQuantidadeVendidaCadeira(iIDEdicao);

            return Convert.ToInt32(PontoBr.Banco.SqlServer.RetornarDadoUnicoDoBanco(sSql));
        }

        public int RetornarQuantidadeVendidaAvulso(int iIDEdicao)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarQuantidadeVendidaAvulso(iIDEdicao);

            return Convert.ToInt32(PontoBr.Banco.SqlServer.RetornarDadoUnicoDoBanco(sSql));
        }

        public int RetornarQuantidadeVendidaCamarote(int iIDEdicao)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarQuantidadeVendidaCamarote(iIDEdicao);

            return Convert.ToInt32(PontoBr.Banco.SqlServer.RetornarDadoUnicoDoBanco(sSql));
        }

        public DataTable RetornarSaldoIngressos(int iIDEdicao, int iNumeroCota, int iIDCliente, int iIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarSaldoIngressos(iIDEdicao, iNumeroCota, iIDCliente, iIDVenda);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public void AprovarVenda(int iIDVenda, int iIDUsuarioAprovacao)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.AprovarVenda(iIDVenda, iIDUsuarioAprovacao);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public int RetornarVagasEstacionamentoReservadas(int iIDEdicao)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarVagasEstacionamentoReservadas(iIDEdicao);
            string sNumeroVagas = PontoBr.Banco.SqlServer.RetornarDadoUnicoDoBanco(sSql);

            return String.IsNullOrEmpty(sNumeroVagas) ? 0 : Convert.ToInt32(sNumeroVagas);
        }

        public int CadastrarCartao(int iIDVenda, string sNumeroCartao, string sCodigoSeguranca, string sValidade,
            string sBandeira, int iParcelas, double dValor, string sTitular)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.CadastrarCartao(iIDVenda, sNumeroCartao, sCodigoSeguranca, sValidade, sBandeira, iParcelas, dValor, sTitular);

            return Convert.ToInt32(PontoBr.Banco.SqlServer.ExecutarSqlComRetornoDeIdentity(sSql));
        }

        public void CapturarTransacao(int iNumeroDocumento)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.CapturarTransacao(iNumeroDocumento);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public bool VerificarExistenciaVendaEmAndamento(int iIDCliente, int iIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.VerificarExistenciaVendaEmAndamento(iIDCliente, iIDVenda);

            return PontoBr.Banco.SqlServer.VerificarExistenciaDeDados(sSql);
        }

        public bool VerificarExistenciaTentativaPagamentoCartao(int iIDVenda, int iSegundos)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.VerificarExistenciaTentativaPagamentoCartao(iIDVenda, iSegundos);

            return PontoBr.Banco.SqlServer.VerificarExistenciaDeDados(sSql);
        }

        public DataTable RetornarTidCartao(int iIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarTidCartao(iIDVenda);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarDadosCielo(string sPaymentId, string sTid)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarDadosCielo(sPaymentId, sTid);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public void CancelarVendaCartao(int iNumeroDocumento, int iIDUsuarioCancelamento)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.CancelarVendaCartao(iNumeroDocumento, iIDUsuarioCancelamento);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public void AtualizarVenda(int iNumeroVagas, int iIDUsuarioAlteracao, int iIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.AtualizarVenda(iNumeroVagas, iIDUsuarioAlteracao, iIDVenda);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public string RetornarValoresIngressosAvulsos(int iIDEdicao)
        {
            vendaDAL DVenda = new vendaDAL();
            return DVenda.RetornarValoresIngressosAvulsos(iIDEdicao);
        }

        public string RetornarValoresIngressosAvulsos(int iIDEdicao, int iIDEvento)
        {
            vendaDAL DVenda = new vendaDAL();
            return DVenda.RetornarValoresIngressosAvulsos(iIDEdicao, iIDEvento);
        }

        public string RetornarValoresIngressosCadeiras(int iIDEdicao)
        {
            vendaDAL DVenda = new vendaDAL();
            return DVenda.RetornarValoresIngressosCadeiras(iIDEdicao);
        }

        public string RetornarValoresIngressosCadeiras(int iIDEdicao, int iIDEvento)
        {
            vendaDAL DVenda = new vendaDAL();
            return DVenda.RetornarValoresIngressosCadeiras(iIDEdicao, iIDEvento);
        }

        public DataTable RetornarMesasSemReservaCompleta(int iIDVenda, int iNumeroMinimoReserva)
        {
            vendaDAL DVenda = new vendaDAL();
            String sSql = DVenda.RetornarMesasSemReservaCompleta(iIDVenda, iNumeroMinimoReserva);
            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarMesasSemReservaCompleta(int iIDVenda, int iNumeroMinimoReserva, int iMesa)
        {
            vendaDAL DVenda = new vendaDAL();
            return PontoBr.Banco.SqlServer.RetornarDataTable(DVenda.RetornarMesasSemReservaCompleta(iIDVenda, iNumeroMinimoReserva, iMesa));
        }

        public DataTable RetornarIngressosCliente(int iIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarIngressosCliente(iIDVenda);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarTickets(int iIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarTickets(iIDVenda);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarVendasReservadas(string sNome, string sCPF, string sEmail, int iNumeroCota)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarVendasReservadas(sNome, sCPF, sEmail, iNumeroCota);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarMesasReservadas(int iIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarMesasReservadas(iIDVenda);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornaEstacionamentos(int iIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarEstacionamentos(iIDVenda);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarIngressosEstacionamento(int iIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarIngressosEstacionamento(iIDVenda);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarIngressosEstacionamento(string sIdentidadeEletronica)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarIngressosEstacionamento(sIdentidadeEletronica);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarEstacionamentos(int iIDCliente)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarEstacionamentos(iIDCliente);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarIngressos(int iIDVenda, string sTickets)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarIngressos(iIDVenda, sTickets);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarIngressos(string sTickets)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarIngressos(sTickets);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        //public void RegistrarEntradaEstacionamento(int iIDVenda)
        //{
        //    vendaDAL DVenda = new vendaDAL();
        //    string sSql = DVenda.RegistrarEntradaEstacionamento(iIDVenda);

        //    PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        //}

        public void RegistrarEntradaIngresso(int Ticket)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RegistrarEntradaIngresso(Ticket);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        //public DataSet CancelarEntradaEstacionamento(int iIDVenda)
        //{
        //    vendaDAL DVenda = new vendaDAL();
        //    string sSql = DVenda.CancelarEntradaEstacionamento(iIDVenda);

        //    return PontoBr.Banco.SqlServer.RetornarDataSet(sSql);
        //}

        public DataSet CancelarEntradaIngresso(int iTicket)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.CancelarEntradaIngresso(iTicket);

            return PontoBr.Banco.SqlServer.RetornarDataSet(sSql);
        }

        public DataTable RetornarTicketsCatraca(int iIDEdicao, string sTipo)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarTicketsCatraca(iIDEdicao, sTipo);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public void CadastrarEstacionamentoAvulso(string sIdentidadeEletronica, int iIDEdicao, string sNome, string sCPF, int iIDUsuarioCadastro)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.CadastrarEstacionamentoAvulso(sIdentidadeEletronica, iIDEdicao, sNome, sCPF, iIDUsuarioCadastro);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public DataTable RetornarEstacionamentosAvulsos(int iIDEdicao)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarEstacionamentosAvulsos(iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarIngressosVoucher(int iIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarIngressosVoucher(iIDVenda);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarIngresso(string sTicket)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarIngresso(sTicket);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarIngressoAvulso(string sIngresso, int iIDEdicao)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarIngressoAvulso(sIngresso, iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarIngressoCadeira(string sIngresso, int iIDEdicao)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarIngressoCadeira(sIngresso, iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public void CancelarTicket(ingresso Ingresso)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.CancelarTicket(Ingresso);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public void CadastrarSolicitacaoRegistro(string sIDVenda, string sIP, string sEmail, string sIDTipoSolicitacao)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.CadastrarSolicitacaoIngresso(sIDVenda, sIP, sEmail, sIDTipoSolicitacao);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public void CadastrarSolicitacaoRegistroConvidado(string sIDVenda, string sTickets, string sIP, string sEmail, string sIDTipoSolicitacao)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.CadastrarSolicitacaoIngressoConvidado(sIDVenda, sTickets, sIP, sEmail, sIDTipoSolicitacao);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public DataTable RetornarTicketMesa(string sIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarTicketMesa(sIDVenda);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarIngressosMesa(int iIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarIngressosMesa(iIDVenda);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public string RetornarVendasConcluidas(int iIDCliente)
        {
            vendaDAL DVenda = new vendaDAL();
            return DVenda.RetornarVendasConcluidas(iIDCliente);
        }

        public string RetornarIngressosConvidado(int iIDVenda, string sTickets)
        {
            vendaDAL DVenda = new vendaDAL();
            return DVenda.RetornarIngressosConvidado(iIDVenda, sTickets);
        }

        public DataTable RetornarCadeirasAleatorio(int iIDEdicao, string sSetor)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarCadeirasAleatorio(iIDEdicao, sSetor);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataSet RetornarComprasEmAndamento(int iIDEdicao)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarComprasEmAndamento(iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataSet(sSql);
        }

        public void CadastrarIngressoEstacionamento(int iIDVenda, int iIDEdicao, int iAvulso, string sIdentidadeEletronica, string sNome, string sCPF, int iIDUsuarioCadastro)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.CadastrarIngressoEstacionamento(iIDVenda, iIDEdicao, iAvulso, sIdentidadeEletronica, sNome, sCPF, iIDUsuarioCadastro);
            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public void ExcluirIngressoEstacionamento(int iIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.ExcluirIngressoEstacionamento(iIDVenda);
            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public pagamento RetornarPagamento(double dNumeroDocumento)
        {
            pagamento Pagamento = new pagamento();

            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarPagamento(dNumeroDocumento);
            DataTable dataTable = PontoBr.Banco.SqlServer.RetornarDataTable(sSql);

            if (dataTable.Rows.Count > 0)
            {
                Pagamento.NumeroDocumento = dataTable.Rows[0]["NumeroDocumento"].ToString();
                Pagamento.IDUsuario = !String.IsNullOrEmpty(dataTable.Rows[0]["IDUsuario"].ToString()) ? Convert.ToInt32(dataTable.Rows[0]["IDUsuario"]) : 0;
                Pagamento.NumeroCartao = dataTable.Rows[0]["NumeroCartao"].ToString();
                Pagamento.Titular = dataTable.Rows[0]["Titular"].ToString();
                Pagamento.CodigoSeguranca = dataTable.Rows[0]["CodigoSeguranca"].ToString();
                Pagamento.Validade = dataTable.Rows[0]["Validade"].ToString();
                Pagamento.Bandeira = dataTable.Rows[0]["Bandeira"].ToString();
                Pagamento.Parcelas = Convert.ToInt32(dataTable.Rows[0]["Parcelas"]);
                Pagamento.Valor = Convert.ToDouble(dataTable.Rows[0]["Valor"]);
                Pagamento.PaymentId = dataTable.Rows[0]["PaymentId"].ToString();
                Pagamento.tid = dataTable.Rows[0]["tid"].ToString();
                Pagamento.Aprovada = Convert.ToInt32(dataTable.Rows[0]["Aprovada"]);
                Pagamento.Cadastro = Convert.ToDateTime(dataTable.Rows[0]["Cadastro"]);
            }

            return Pagamento;
        }
        public DataTable RetornarPagamentoBysIDVenda(string sIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarPagamentoBysIDVenda(sIDVenda);
            DataTable dataTable = PontoBr.Banco.SqlServer.RetornarDataTable(sSql);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public void AtualizarPagamentoCartao(string sNumeroDocumento, string sPaymentId, string stid, string sAuthorizationCode, int iAprovada)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.AtualizarPagamentoCartao(sNumeroDocumento, sPaymentId, stid, sAuthorizationCode, iAprovada);
            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public void ExcluirFila(int iIDEdicao)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.ExcluirFila(iIDEdicao);
            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public void CadastrarUsuarioFila(int iIDUsuario, int iIDEdicao)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.CadastrarUsuarioFila(iIDUsuario, iIDEdicao);
            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public void ExcluirUsuarioFila(int iIDUsuario)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.ExcluirUsuarioFila(iIDUsuario);
            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public string RetornarPosicaoFila(int iIDUsuario, int iIDEdicao)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.RetornarPosicaoFila(iIDUsuario, iIDEdicao);
            return PontoBr.Banco.SqlServer.RetornarDadoUnicoDoBanco(sSql);
        }

        public bool VerificarExistenciaVoucher(int iIDVenda)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.VerificarExistenciaVoucher(iIDVenda);
            return PontoBr.Banco.SqlServer.VerificarExistenciaDeDados(sSql);
        }

        public bool VerificarComprasNaoAprovadasCartao(int iIDUsuario, int iIDEdicao, DateTime dataLiberacao)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.VerificarComprasNaoAprovadasCartao(iIDUsuario, iIDEdicao, dataLiberacao);
            return PontoBr.Banco.SqlServer.VerificarExistenciaDeDados(sSql);
        }

        public bool VerificarComprasAprovadasCartao(int iIDUsuario, int iIDEdicao, DateTime dataLiberacao)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.VerificarComprasAprovadasCartao(iIDUsuario, iIDEdicao, dataLiberacao);
            return PontoBr.Banco.SqlServer.VerificarExistenciaDeDados(sSql);
        }

        public bool VerificarComprasAprovadasCartao(string sNumeroCartao, string sCodigoSegura, DateTime dataLiberacao)
        {
            vendaDAL DVenda = new vendaDAL();
            string sSql = DVenda.VerificarComprasAprovadasCartao(sNumeroCartao, sCodigoSegura, dataLiberacao);
            return PontoBr.Banco.SqlServer.VerificarExistenciaDeDados(sSql);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.objetos;
using System.Data;
using System.Web;
using Model.negocios;
using System.Web.UI.WebControls;

namespace Controller
{
    public class vendaCTL
    {
        public DataSet RetornarCarrinhoCompras(int iIDVenda)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarCarrinhoCompras(iIDVenda);
        }        

        public DataSet RetornarCarrinhoComprasTesteCarga(int iIDVenda)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarCarrinhoComprasTesteCarga(iIDVenda);
        }

        public void ReservarCadeira(venda Venda)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.ReservarCadeira(Venda);
        }

        public DataTable RetornarStatusCadeiras(int iIDEdicao, string sSetor)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarStatusCadeiras(iIDEdicao, sSetor);
        }

        public venda RetornarCadeira(venda Venda)
        {
            vendaBLL BVenda = new vendaBLL();
            DataTable dataTable = BVenda.RetornarCadeira(Venda);

            Venda.IDCadeira = Convert.ToInt32(dataTable.Rows[0]["IDCadeira"].ToString());
            Venda.IDStatusCadeira = Convert.ToInt32(dataTable.Rows[0]["IDStatusCadeira"].ToString());
            Venda.IDCliente = dataTable.Rows[0]["IDCliente"].ToString() == "" ? -1 : Convert.ToInt32(dataTable.Rows[0]["IDCliente"].ToString());

            return Venda;
        }

        public int CadastrarVenda(venda Venda)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.CadastrarVenda(Venda);
        }

        public void CadastrarVendaAvulso(venda Venda)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.CadastrarVendaAvulso(Venda);
        }

        public void CancelarTicket(double dTicket, string sTextoLog, int iIDUsuario)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.CancelarTicket(dTicket, sTextoLog, iIDUsuario);
        }

        public void LiberarIngressos(int iIDCliente, int iIDEdicao, int iIDVenda)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.LiberarIngressos(iIDCliente, iIDEdicao, iIDVenda);

            BVenda.ExcluirUsuarioFila(iIDCliente);
        }

        public void LiberarIngressosPresenciais(int iIDCliente, int iIDEdicao, int iIDVenda)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.LiberarIngressosPresenciais(iIDCliente, iIDEdicao, iIDVenda);
        }

        public void LiberarIngressos(int iIDCliente)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.LiberarIngressos(iIDCliente);
        }

        public void LiberarCadeiras()
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.LiberarCadeiras();
        }

        public void CancelarVenda(int iIDVenda, int iIDUsuarioCancelamento)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.CancelarVenda(iIDVenda, iIDUsuarioCancelamento);
        }

        public void LiberarCadeira(venda Cadeira)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.LiberarCadeira(Cadeira);
        }

        public void ConcluirVenda(int iIDVenda, int iIDFormaPagamento, string sDetalhesFormaPagamento, int iNumeroVagas,
            int iNumeroCriancas, string sNomeRetirada, string sRgRetirada, int iIDLocalRetirada, string sNomeRetirada2, string sRgRetirada2)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.ConcluirVenda(iIDVenda, iIDFormaPagamento, sDetalhesFormaPagamento, iNumeroVagas, iNumeroCriancas, sNomeRetirada, sRgRetirada, iIDLocalRetirada, sNomeRetirada2, sRgRetirada2);
        }

        public void AtualizarVendaComprovanteCaixa(int iIDVenda, int iIDFormaPagamento, string sDetalhesFormaPagamento, int iNumeroVagas,
            int iNumeroCriancas, string sNomeRetirada, string sRgRetirada, int iIDLocalRetirada, string sNomeSegundoRetirada, string sRgSegundoRetirada)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.AtualizarVendaComprovanteCaixa(iIDVenda, iIDFormaPagamento, sDetalhesFormaPagamento, iNumeroVagas, iNumeroCriancas, sNomeRetirada, sRgRetirada, iIDLocalRetirada, sNomeSegundoRetirada, sRgSegundoRetirada);
        }

        public void AtualizarVendaReservada(int iIDVenda, int iIDFormaPagamento, string sDetalhesFormaPagamento, int iNumeroVagas,
            int iNumeroCriancas, string sNomeRetirada, string sRgRetirada, int iIDLocalRetirada, string sNomeSegundoRetirada, string sRgSegundoRetirada)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.AtualizarVendaReservada(iIDVenda, iIDFormaPagamento, sDetalhesFormaPagamento, iNumeroVagas, iNumeroCriancas, sNomeRetirada, sRgRetirada, iIDLocalRetirada, sNomeSegundoRetirada, sRgSegundoRetirada);
        }

        public DataTable RetornarVendasCliente(int iIDCliente)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarVendasCliente(iIDCliente);
        }

        public DataTable RetornarVendas(int iIDVenda, string sNome, string sCPF, string sIDStatusCompra)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarVendas(iIDVenda, sNome, sCPF, sIDStatusCompra);
        }

        public venda RetornarVenda(int iIDVenda, string sIDStatusCompra)
        {
            venda Venda = new venda();
            
            vendaBLL BVenda = new vendaBLL();
            DataTable dataTable = BVenda.RetornarVenda(iIDVenda, sIDStatusCompra);

            if (dataTable.Rows.Count > 0)
            {
                Venda.IDVenda = Convert.ToInt32(dataTable.Rows[0]["IDVenda"].ToString());
                Venda.IDEdicao = Convert.ToInt32(dataTable.Rows[0]["IDEdicao"].ToString());
                Venda.IDCliente = Convert.ToInt32(dataTable.Rows[0]["IDCliente"].ToString());
                Venda.IDUsuario = Convert.ToInt32(dataTable.Rows[0]["IDUsuario"].ToString());
                Venda.Nome = dataTable.Rows[0]["Nome"].ToString();
                Venda.CPF = dataTable.Rows[0]["CPF"].ToString();
                Venda.NumeroCota = Convert.ToInt32(dataTable.Rows[0]["NumeroCota"].ToString());
                Venda.DataHoraCompra = dataTable.Rows[0]["Data/Hora Compra"].ToString();
                Venda.FormaPagamento = dataTable.Rows[0]["FormaPagamento"].ToString();
                Venda.DetalhesFormaPagamento = dataTable.Rows[0]["DetalhesFormaPagamento"].ToString();
                Venda.StatusCompra = dataTable.Rows[0]["StatusCompra"].ToString();
                Venda.Evento = dataTable.Rows[0]["Evento"].ToString();
                Venda.Edicao = dataTable.Rows[0]["Edicao"].ToString();
                Venda.DataHoraEmpresa = dataTable.Rows[0]["Data/Hora Entrega"].ToString();
                Venda.ResponsavelEntrega = dataTable.Rows[0]["Responsável Baixa/Entrega"].ToString();
                Venda.ResponsavelCompra = dataTable.Rows[0]["Responsável pela Compra"].ToString();
                Venda.IDStatusCompra = Convert.ToInt32(dataTable.Rows[0]["IDStatusCompra"].ToString());

                if (Venda.FormaPagamento != "Cortesia")
                    Venda.ValorTotal = dataTable.Rows[0]["ValorTotal"].ToString() == "" ? 0 : Convert.ToDecimal(dataTable.Rows[0]["ValorTotal"]);
                else
                    Venda.ValorTotal = 0;

                Venda.NumeroIngressos = dataTable.Rows[0]["NumeroIngressos"].ToString() == "" ? 0 : Convert.ToInt32(dataTable.Rows[0]["NumeroIngressos"].ToString());

                Venda.IDLocalRetirada = dataTable.Rows[0]["IDLocalRetirada"].ToString() == "" ? 0 : Convert.ToInt32(dataTable.Rows[0]["IDLocalRetirada"].ToString());
                Venda.NumeroVagas = Convert.ToInt32(dataTable.Rows[0]["NumeroVagas"].ToString());
                Venda.NomeResponsavelRetirada = dataTable.Rows[0]["NomeRetirada"].ToString();
                Venda.RgResponsavelRetirada = dataTable.Rows[0]["RgRetirada"].ToString();

                Venda.NomeResponsavelRetirada2 = dataTable.Rows[0]["NomeRetirada2"].ToString();
                Venda.RgResponsavelRetirada2 = dataTable.Rows[0]["RgRetirada2"].ToString();
            }

            return Venda;
        }

        public DataTable RetornarIngressosRecibo(int iIDVenda)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarIngressosRecibo(iIDVenda);
        }

        public void EntregarIngresso(int iIDVenda, int iIDUsuarioEntregaIngresso)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.EntregarIngresso(iIDVenda, iIDUsuarioEntregaIngresso);
        }

        public void CarregarRadioButtonListFormasPagamento(RadioButtonList radFormaPagamento, string sIDFormaPagamento)
        {
            vendaBLL BVenda = new vendaBLL();
            string sSql = BVenda.RetornarFormasPagamento(sIDFormaPagamento);

            PontoBr.Utilidades.WCL.CarregarRadioButtonList(radFormaPagamento, sSql, "FormaPagamento", "IDFormaPagamento");
        }

        public void CarregarDropDownListFormasPagamento(DropDownList dropFormaPagamento)
        {
            vendaBLL BVenda = new vendaBLL();
            string sSql = BVenda.RetornarFormasPagamento(null);

            PontoBr.Utilidades.WCL.CarregarDropDown(dropFormaPagamento, sSql, "FormaPagamento", "IDFormaPagamento", true, false);
        }
       
        public int RetornarQuantidadeVendidaCadeira(int iIDEdicao)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarQuantidadeVendidaCadeira(iIDEdicao);
        }

        public int RetornarQuantidadeVendidaAvulso(int iIDEdicao)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarQuantidadeVendidaAvulso(iIDEdicao);
        }

        public int RetornarQuantidadeVendidaCamarote(int iIDEdicao)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarQuantidadeVendidaCamarote(iIDEdicao);
        }

        public saldo RetornarSaldoIngressos(int iIDEdicao, int iNumeroCota, int iIDCliente, int iIDVenda)
        {
            saldo Saldo = new saldo();
            vendaBLL BVenda = new vendaBLL();

            DataTable dataTable = BVenda.RetornarSaldoIngressos(iIDEdicao, iNumeroCota, iIDCliente, iIDVenda);

            Saldo.QuantidadeCarrinhoVendaAtual = Convert.ToInt32(dataTable.Rows[0][0].ToString());
            Saldo.QuantidadeComprado = Convert.ToInt32(dataTable.Rows[0][1].ToString());
            Saldo.QuantidadeCadeiraVendaAtual = Convert.ToInt32(dataTable.Rows[0][2].ToString());
            Saldo.QuantidadeCadeiraComprado = Convert.ToInt32(dataTable.Rows[0][3].ToString());
            Saldo.QuantidadeValorSocioVendaAtual = Convert.ToInt32(dataTable.Rows[0][4].ToString());
            Saldo.QuantidadeValorNaoSocioVendaAtual = Convert.ToInt32(dataTable.Rows[0][5].ToString());
            Saldo.QuantidadeValorSocioComprado = Convert.ToInt32(dataTable.Rows[0][6].ToString());
            Saldo.QuantidadeValorNaoSocioComprado = Convert.ToInt32(dataTable.Rows[0][7].ToString());
            Saldo.QuantidadeAvulsoVendaAtual = Convert.ToInt32(dataTable.Rows[0][8].ToString());
            Saldo.QuantidadeCamaroteVendaAtual = Convert.ToInt32(dataTable.Rows[0][9].ToString());

            return Saldo;
        }

        public void AprovarVenda(int iIDVenda, int iIDUsuarioAprovacao)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.AprovarVenda(iIDVenda, iIDUsuarioAprovacao);
        }

        public int RetornarVagasEstacionamentoReservadas(int iIDEdicao)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarVagasEstacionamentoReservadas(iIDEdicao);
        }

        public int CadastrarCartao(int iIDVenda, string sNumeroCartao, string sCodigoSeguranca, string sValidade,
            string sBandeira, int iParcelas, double dValor, string sTitular)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.CadastrarCartao(iIDVenda, sNumeroCartao, sCodigoSeguranca, sValidade, sBandeira, iParcelas, dValor, sTitular);
        }

        public void CapturarTransacao(int iNumeroDocumento)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.CapturarTransacao(iNumeroDocumento);
        }

        public bool VerificarExistenciaVendaEmAndamento(int iIDCliente, int iIDVenda)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.VerificarExistenciaVendaEmAndamento(iIDCliente, iIDVenda);
        }

        public bool VerificarExistenciaTentativaPagamentoCartao(int iIDVenda, int iSegundos)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.VerificarExistenciaTentativaPagamentoCartao(iIDVenda, iSegundos);
        }

        public DataTable RetornarTidCartao(int iIDVenda)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarTidCartao(iIDVenda);
        }

        public DataTable RetornarDadosCielo(string sPaymentId, string sTid)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarDadosCielo(sPaymentId, sTid);
        }

        public void CancelarVendaCartao(int iNumeroDocumento, int iIDUsuarioCancelamento)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.CancelarVendaCartao(iNumeroDocumento, iIDUsuarioCancelamento);
        }

        public void AtualizarVenda(int iNumeroVagas, int iIDUsuarioAlteracao, int iIDVenda)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.AtualizarVenda(iNumeroVagas, iIDUsuarioAlteracao, iIDVenda);
        }

        public void CarregarDropDownListValoresIngressosAvulsos(DropDownList dropIngresso, int iIDEdicao, bool bAdicionarItemInicial)
        {
            vendaBLL BVenda = new vendaBLL();
            string sSql = BVenda.RetornarValoresIngressosAvulsos(iIDEdicao);

            if (bAdicionarItemInicial)
            {
                PontoBr.Utilidades.WCL.CarregarDropDown(dropIngresso, sSql, "Ingresso", "Valor", false, false);
                
                dropIngresso.Items.Insert(0, new ListItem("-", "-1"));
                dropIngresso.SelectedIndex = 0;
            }
            else
                PontoBr.Utilidades.WCL.CarregarDropDown(dropIngresso, sSql, "Ingresso", "Ingresso", false, true);
        }

        public void CarregarDropDownListValoresIngressosAvulsos(DropDownList dropIngresso, int iIDEdicao, int iIDEvento, bool bAdicionarItemInicial)
        {
            vendaBLL BVenda = new vendaBLL();
            string sSql = BVenda.RetornarValoresIngressosAvulsos(iIDEdicao, iIDEvento);

            if (bAdicionarItemInicial)
            {
                PontoBr.Utilidades.WCL.CarregarDropDown(dropIngresso, sSql, "Ingresso", "Valor", false, false);

                dropIngresso.Items.Insert(0, new ListItem("-", "-1"));
                dropIngresso.SelectedIndex = 0;
            }
            else
                PontoBr.Utilidades.WCL.CarregarDropDown(dropIngresso, sSql, "Ingresso", "Ingresso", false, true);
        }

        public void CarregarDropDownListValoresIngressosCadeiras(DropDownList dropIngresso, int iIDEdicao, bool bAdicionarItemInicial)
        {
            vendaBLL BVenda = new vendaBLL();
            string sSql = BVenda.RetornarValoresIngressosCadeiras(iIDEdicao);

            if (bAdicionarItemInicial)
            {
                PontoBr.Utilidades.WCL.CarregarDropDown(dropIngresso, sSql, "Ingresso", "Valor", false, false);

                dropIngresso.Items.Insert(0, new ListItem("-", "-1"));
                dropIngresso.SelectedIndex = 0;
            }
            else
                PontoBr.Utilidades.WCL.CarregarDropDown(dropIngresso, sSql, "Ingresso", "Ingresso", false, true);
        }

        public void CarregarDropDownListValoresIngressosCadeiras(DropDownList dropIngresso, int iIDEdicao, int iIDEvento, bool bAdicionarItemInicial)
        {
            vendaBLL BVenda = new vendaBLL();
            string sSql = BVenda.RetornarValoresIngressosCadeiras(iIDEdicao, iIDEvento);

            if (bAdicionarItemInicial)
            {
                PontoBr.Utilidades.WCL.CarregarDropDown(dropIngresso, sSql, "Ingresso", "Valor", false, false);

                dropIngresso.Items.Insert(0, new ListItem("-", "-1"));
                dropIngresso.SelectedIndex = 0;
            }
            else
                PontoBr.Utilidades.WCL.CarregarDropDown(dropIngresso, sSql, "Ingresso", "Ingresso", false, true);
        }

        public DataTable RetornarMesasSemReservaCompleta(int iIDVenda, int iNumeroMinimoReserva)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarMesasSemReservaCompleta(iIDVenda, iNumeroMinimoReserva);
        }

        public DataTable RetornarMesasSemReservaCompleta(int iIDVenda, int iNumeroMinimoReserva, int iMesa)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarMesasSemReservaCompleta(iIDVenda, iNumeroMinimoReserva, iMesa);
        }

        public DataTable RetornarIngressosCliente(int iIDVenda)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarIngressosCliente(iIDVenda);
        }

        public DataTable RetornarTickets(int iIDVenda)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarTickets(iIDVenda);
        }

        public DataTable RetornarIngressos(int iIDVenda, string sTickets)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarIngressos(iIDVenda, sTickets);
        }

        public DataTable RetornarIngressos(string sTickets)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarIngressos(sTickets);
        }

        public void CarregarGridViewVendasReservadas(GridView grdVendas, string sNome, string sCPF, string sEmail, int iNumeroCota)
        {
            vendaBLL BVenda = new vendaBLL();
            DataTable dataTable = BVenda.RetornarVendasReservadas(sNome, sCPF, sEmail, iNumeroCota);

            grdVendas.DataSource = dataTable;
            grdVendas.DataBind();
        }

        public DataTable RetornarMesasReservadas(int iIDVenda)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarMesasReservadas(iIDVenda);
        }

        public DataTable RetornarEstacionamentos(int iIDCliente)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarEstacionamentos(iIDCliente);
        }

        public DataTable RetornarIngressosEstacionamento(int iIDVenda)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarIngressosEstacionamento(iIDVenda);
        }

        public DataTable RetornarIngressosEstacionamento(string sIdentidadeEletronica)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarIngressosEstacionamento(sIdentidadeEletronica);
        }

        //public void RegistrarEntradaEstacionamento(int iIDVenda)
        //{
        //    vendaBLL BVenda = new vendaBLL();
        //    BVenda.RegistrarEntradaEstacionamento(iIDVenda);
        //}

        public void RegistrarEntradaIngresso(int Ticket)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.RegistrarEntradaIngresso(Ticket);
        }

        //public void CancelarEntradaEstacionamento(int iIDVenda)
        //{
        //    vendaBLL BVenda = new vendaBLL();
        //    BVenda.CancelarEntradaEstacionamento(iIDVenda);
        //}

        public void CancelarEntradaIngresso(int iTicket)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.CancelarEntradaIngresso(iTicket);
        }

        public DataTable RetornarTicketsCatraca(int iIDEdicao, string sTipo)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarTicketsCatraca(iIDEdicao, sTipo);
        }

        public void CadastrarEstacionamentoAvulso(string sIdentidadeEletronica, int iIDEdicao, string sNome, string sCPF, int iIDUsuarioCadastro)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.CadastrarEstacionamentoAvulso(sIdentidadeEletronica, iIDEdicao, sNome, sCPF, iIDUsuarioCadastro);
        }

        public void CarregarGridViewEstacionamentosAvulsos(GridView grdEstacionamento, int iIDEdicao)
        {
            vendaBLL BVenda = new vendaBLL();
            DataTable dataTable = BVenda.RetornarEstacionamentosAvulsos(iIDEdicao);

            grdEstacionamento.DataSource = dataTable;
            grdEstacionamento.DataBind();
        }

        public void CarregarGridViewIngressosVoucher(GridView grdVendas, int iIDVenda)
        {
            vendaBLL BVenda = new vendaBLL();
            DataTable dataTable = BVenda.RetornarIngressosVoucher(iIDVenda);

            grdVendas.DataSource = dataTable;
            grdVendas.DataBind();
        }

        public ingresso RetornarIngresso(string sTicket)
        {
            ingresso Ingresso = new ingresso();

            vendaBLL BVenda = new vendaBLL();
            DataTable dataTable = BVenda.RetornarIngresso(sTicket);

            if (dataTable.Rows.Count > 0)
            {
                Ingresso.IDVenda = Convert.ToInt32(dataTable.Rows[0]["IDVenda"].ToString());
                Ingresso.IDEdicao = Convert.ToInt32(dataTable.Rows[0]["IDEdicao"].ToString());
                Ingresso.IDCliente = Convert.ToInt32(dataTable.Rows[0]["IDCliente"].ToString());
                Ingresso.Nome = dataTable.Rows[0]["Nome"].ToString();
                Ingresso.CPF = dataTable.Rows[0]["CPF"].ToString();
                Ingresso.NumeroCota = Convert.ToInt32(dataTable.Rows[0]["NumeroCota"].ToString());
                Ingresso.DataHoraCompra = dataTable.Rows[0]["Data/Hora Compra"].ToString();
                Ingresso.FormaPagamento = dataTable.Rows[0]["FormaPagamento"].ToString();
                Ingresso.IDStatusCompra = Convert.ToInt32(dataTable.Rows[0]["IDStatusCompra"].ToString());
                Ingresso.IDTipoIngresso = Convert.ToInt32(dataTable.Rows[0]["IDTipoIngresso"].ToString());

                Ingresso.Lote = Convert.ToInt32(dataTable.Rows[0]["Lote"].ToString());
                Ingresso.Valor = Convert.ToDouble(dataTable.Rows[0]["Valor"].ToString());
                Ingresso.FormaPagamento = dataTable.Rows[0]["FormaPagamento"].ToString();
                Ingresso.TipoIngresso = dataTable.Rows[0]["Tipo"].ToString();
            }

            return Ingresso;
        }

        public ingresso RetornarIngressoAvulso(string sIngresso, int iIDEdicao)
        {
            ingresso Ingresso = new ingresso();

            vendaBLL BVenda = new vendaBLL();
            DataTable dataTable = BVenda.RetornarIngressoAvulso(sIngresso, iIDEdicao);

            if (dataTable.Rows.Count > 0)
            {
                Ingresso.Ingresso = dataTable.Rows[0]["Ingresso"].ToString();
                Ingresso.Valor = Convert.ToDouble(dataTable.Rows[0]["Valor"].ToString());
                Ingresso.Lote = Convert.ToInt32(dataTable.Rows[0]["Lote"].ToString());
                Ingresso.IDEdicao = Convert.ToInt32(dataTable.Rows[0]["IDEdicao"].ToString());
                Ingresso.IDTipoIngresso = Convert.ToInt32(dataTable.Rows[0]["IDTipoIngresso"].ToString());
            }

            return Ingresso;
        }

        public ingresso RetornarIngressoCadeira(string sIngresso, int iIDEdicao)
        {
            ingresso Ingresso = new ingresso();

            vendaBLL BVenda = new vendaBLL();
            DataTable dataTable = BVenda.RetornarIngressoCadeira(sIngresso, iIDEdicao);

            if (dataTable.Rows.Count > 0)
            {
                Ingresso.Ingresso = dataTable.Rows[0]["Ingresso"].ToString();
                Ingresso.Valor = Convert.ToDouble(dataTable.Rows[0]["Valor"].ToString());
                Ingresso.Lote = Convert.ToInt32(dataTable.Rows[0]["Lote"].ToString());
                Ingresso.IDEdicao = Convert.ToInt32(dataTable.Rows[0]["IDEdicao"].ToString());
                Ingresso.IDTipoIngresso = Convert.ToInt32(dataTable.Rows[0]["IDTipoIngresso"].ToString());
            }

            return Ingresso;
        }

        public void CancelarTicket(ingresso Ingresso)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.CancelarTicket(Ingresso);
        }

        public void CadastrarSolicitacaoIngresso(string sIDVenda, string sIP, string sEmail, string sIDTipoSolicitacao)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.CadastrarSolicitacaoRegistro(sIDVenda, sIP, sEmail, sIDTipoSolicitacao);
        }

        public void CadastrarSolicitacaoIngressoConvidado(string sIDVenda, string sTickets, string sIP, string sEmail, string sIDTipoSolicitacao)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.CadastrarSolicitacaoRegistroConvidado(sIDVenda, sTickets, sIP, sEmail, sIDTipoSolicitacao);
        }

        public DataTable RetornarTicketMesa(string sIDVenda)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarTicketMesa(sIDVenda);
        }

        public DataTable RetornarIngressosMesa(int iIDVenda)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarIngressosMesa(iIDVenda);
        }        

        public void CarregarDropVendas(DropDownList dropVendas, int iIDCliente)
        {
            vendaBLL BVenda = new vendaBLL();
            string sSql = BVenda.RetornarVendasConcluidas(iIDCliente);

            PontoBr.Utilidades.WCL.CarregarDropDown(dropVendas, sSql, "Venda", "IDVenda", false, true);
        }       

        public void CarregarCheckIngressos(CheckBoxList checkIngressos, int iIDVenda, string sTickets)
        {
            vendaBLL BVenda = new vendaBLL();
            string sSql = BVenda.RetornarIngressosConvidado(iIDVenda, sTickets);

            PontoBr.Utilidades.WCL.CarregarCheckBoxList(checkIngressos, sSql, "Ingresso", "Ticket");
        }

        public DataTable RetornarCadeirasAleatorio(int iIDEdicao, string sSetor)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarCadeirasAleatorio(iIDEdicao, sSetor);
        }

        public DataSet RetornarComprasEmAndamento(int iIDEdicao)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarComprasEmAndamento(iIDEdicao);
        }

        public void CadastrarIngressoEstacionamento(int iIDVenda, int iIDEdicao, int iAvulso, string sIdentidadeEletronica, string sNome, string sCPF, int iIDUsuarioCadastro)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.CadastrarIngressoEstacionamento(iIDVenda, iIDEdicao, iAvulso, sIdentidadeEletronica, sNome, sCPF, iIDUsuarioCadastro);
        }

        public void ExcluirIngressoEstacionamento(int iIDVenda)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.ExcluirIngressoEstacionamento(iIDVenda);
        }

        public pagamento RetornarPagamento(double dNumeroDocumento)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarPagamento(dNumeroDocumento);
        }
        public DataTable RetornarPagamentoBysIDVenda(string dNumeroDocumento)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarPagamentoBysIDVenda(dNumeroDocumento);
        }

        public void AtualizarPagamentoCartao(string sNumeroDocumento, string sPaymentId, string stid, string sAuthorizationCode, int iAprovada)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.AtualizarPagamentoCartao(sNumeroDocumento, sPaymentId, stid, sAuthorizationCode, iAprovada);
        }

        public void ExcluirFila(int iIDEdicao)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.ExcluirFila(iIDEdicao);
        }

        public void CadastrarUsuarioFila(int iIDUsuario, int iIDEdicao)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.CadastrarUsuarioFila(iIDUsuario, iIDEdicao);
        }

        public void ExcluirUsuarioFila(int iIDUsuario)
        {
            vendaBLL BVenda = new vendaBLL();
            BVenda.ExcluirUsuarioFila(iIDUsuario);
        }

        public string RetornarPosicaoFila(int iIDUsuario, int iIDEdicao)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.RetornarPosicaoFila(iIDUsuario, iIDEdicao);
        }

        public bool VerificarExistenciaVoucher(int iIDVenda)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.VerificarExistenciaVoucher(iIDVenda);
        }

        //Blacklist - Verifica se já existe mais de duas vendas não aprovadas para o cliente (não sócio)
        public bool VerificarComprasNaoAprovadasCartao(int iIDUsuario, int iIDEdicao, DateTime dataLiberacao)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.VerificarComprasNaoAprovadasCartao(iIDUsuario, iIDEdicao, dataLiberacao);
        }

        //Blacklist - Verifica se já existe mais de duas vendas aprovadas para o cliente (não sócio)
        public bool VerificarComprasAprovadasCartao(int iIDUsuario, int iIDEdicao, DateTime dataLiberacao)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.VerificarComprasAprovadasCartao(iIDUsuario, iIDEdicao, dataLiberacao);
        }

        //Blacklist - O não sócio conseguirá fazer apenas 01 compra com o mesmo cartão de crédito, independente se é o mesmo usuário ou não
        public bool VerificarComprasAprovadasCartao(string sNumeroCartao, string sCodigoSegura, DateTime dataLiberacao)
        {
            vendaBLL BVenda = new vendaBLL();
            return BVenda.VerificarComprasAprovadasCartao(sNumeroCartao, sCodigoSegura, dataLiberacao);
        }
    }
}

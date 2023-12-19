using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.objetos;
using System.Configuration;

namespace Model.dados
{
    public class vendaDAL
    {
        internal string RetornarCarrinhoCompras(int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from vVendaTicket ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");

            sSql.Append(" select SUM(Valor) Valor ");
            sSql.Append(" from vVendaTicket ");
            sSql.Append(" where IDVenda = " + iIDVenda + "");

            sSql.Append(" select count(*) IngressosCadeira ");
            sSql.Append(" from tVendaCadeira ");
            sSql.Append(" where IDTipoIngresso in (1,2,5,6,9,10,11,12) "); // inserido ,9,10,11,12 para a Boate
            sSql.Append(" and IDVenda = " + iIDVenda + "");

            return sSql.ToString();
        }        

        internal string RetornarCarrinhoComprasTesteCarga(int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from vVendaTicket ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");

            return sSql.ToString();
        }

        internal string ReservarCadeira(venda Venda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" Declare ");
            sSql.Append(" @IDCadeira int ");

            sSql.Append(" select @IDCadeira = c.IDCadeira ");
            sSql.Append(" from tCadeira c ");
            sSql.Append(" inner join tEvento e on c.IDEvento = e.IDEvento ");
            sSql.Append(" where e.Evento = '" + Venda.Evento + "' ");
            sSql.Append(" and c.Setor = '" + Venda.Setor + "'   ");
            sSql.Append(" and c.Mesa = " + Venda.Mesa + " ");
            sSql.Append(" and c.Cadeira = " + Venda.Cadeira + " ");

            sSql.Append(" update tVendaCadeira ");
            sSql.Append(" set IDStatusCadeira = 2, ");
            sSql.Append(" IDVenda = " + Venda.IDVenda + ", ");
            sSql.Append(" IDTipoIngresso = " + Venda.IDTipoIngresso + ", ");
            sSql.Append(" Valor = '" + Venda.Valor.ToString().Replace(",", ".") + "', ");
            sSql.Append(" Lote = " + Venda.Lote + ", ");
            sSql.Append(" DataHoraReserva = GETDATE() ");
            sSql.Append(" where IDStatusCadeira in (1,4,5) ");
            sSql.Append(" and (IDVenda is null or IDVenda = " + Venda.IDVenda + ") ");
            sSql.Append(" and IDEdicao = " + Venda.IDEdicao + " ");
            sSql.Append(" and IDCadeira = @IDCadeira ");

            //Bloquear cadeiras da mesa
            sSql.Append(" update tVendaCadeira  ");
            sSql.Append(" set IDStatusCadeira = 4, ");
            sSql.Append(" IDVenda = " + Venda.IDVenda + ", ");
            sSql.Append(" DataHoraReserva = GETDATE() ");
            sSql.Append(" where IDStatusCadeira = 1 ");
            sSql.Append(" and IDVenda is null ");
            sSql.Append(" and IDEdicao = " + Venda.IDEdicao + " ");
            sSql.Append(" and IDCadeira in (	select c.IDCadeira ");
            sSql.Append(" 		            from tCadeira c ");
            sSql.Append(" 		            inner join tEvento e on c.IDEvento = e.IDEvento ");
            sSql.Append(" 		            where c.Mesa = " + Venda.Mesa + " ");
            sSql.Append(" 		            and c.Cadeira != " + Venda.Cadeira + " ");
            sSql.Append(" 		            and c.Setor = '" + Venda.Setor + "' ");
            sSql.Append(" 		            and e.Evento = '" + Venda.Evento + "') ");

            return sSql.ToString();
        }

        internal string RetornarStatusCadeiras(int iIDEdicao, string sSetor)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select vc.IDCadeira, vc.IDStatusCadeira, s.Status, ");
            sSql.Append(" c.Cadeira, c.Mesa, s.Imagem, v.IDCliente ");
            sSql.Append(" from tVendaCadeira vc ");
            sSql.Append(" inner join tCadeira c on vc.IDCadeira = c.IDCadeira ");
            sSql.Append(" inner join tStatusCadeira s on vc.IDStatusCadeira = s.IDStatus ");
            sSql.Append(" left join tVenda v on vc.IDVenda = v.IDVenda ");
            sSql.Append(" where c.Setor = '" + sSetor + "'  ");
            sSql.Append(" and vc.IDEdicao = " + iIDEdicao + " ");

            return sSql.ToString();
        }

        internal string RetornarCadeira(venda Venda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" Declare ");
            sSql.Append(" @IDCadeira int ");

            sSql.Append(" select @IDCadeira = c.IDCadeira ");
            sSql.Append(" from tCadeira c ");
            sSql.Append(" inner join tEvento e on c.IDEvento = e.IDEvento ");
            sSql.Append(" where e.Evento = '" + Venda.Evento + "' ");
            sSql.Append(" and c.Setor = '" + Venda.Setor + "'   ");
            sSql.Append(" and c.Mesa = " + Venda.Mesa + " ");
            sSql.Append(" and c.Cadeira = " + Venda.Cadeira + " ");

            sSql.Append(" select vc.*, v.IDCliente ");
            sSql.Append(" from tVendaCadeira vc ");
            sSql.Append(" left join tVenda v on vc.IDVenda = v.IDVenda ");
            sSql.Append(" where vc.IDEdicao = " + Venda.IDEdicao + " ");
            sSql.Append(" and IDCadeira = @IDCadeira ");

            return sSql.ToString();
        }

        internal string CadastrarVenda(venda Venda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" insert into tVenda ");
            sSql.Append(" (IDEdicao, IDCliente, IP, IDUsuarioCadastro) ");
            sSql.Append(" values ");
            sSql.Append(" (" + Venda.IDEdicao + ", " + Venda.IDCliente + ", '" + Venda.IP + "', " + Venda.IDUsuarioCadastro + ") ");

            return sSql.ToString();
        }

        internal string CadastrarVendaAvulso(venda Venda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" insert into tVendaAvulso ");
            sSql.Append(" (IDVenda, IDTipoIngresso, Valor, Lote) ");
            sSql.Append(" values ");
            sSql.Append(" (" + Venda.IDVenda + ", " + Venda.IDTipoIngresso + ", '" + Venda.Valor.ToString().Replace(",", ".") + "', " + Venda.Lote + ") ");

            return sSql.ToString();
        }

        internal string CancelarTicket(double dTicket, string sTextoLog, int iIDUsuario)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" Declare ");
            sSql.Append(" @IDVenda bigint = -1, ");
            sSql.Append(" @Mesa int = -1 ");

            sSql.Append(" select @IDVenda = IDVenda, ");
            sSql.Append(" @Mesa = c.Mesa  ");
            sSql.Append(" from tVendaCadeira vc ");
            sSql.Append(" inner join tCadeira c on vc.IDCadeira = c.IDCadeira ");
            sSql.Append(" where vc.Ticket = " + dTicket + " ");

            /////////////////////////////////////////////////////////////////////////

            sSql.Append(" delete tVendaAvulso ");
            sSql.Append(" where Ticket = " + dTicket + " ");

            sSql.Append(" update tVendaCadeira  ");
            sSql.Append(" set IDStatusCadeira = 1,  ");
            sSql.Append(" IDVenda = null,  ");
            sSql.Append(" Valor = null,  ");
            sSql.Append(" IDTipoIngresso = null, ");
            sSql.Append(" Lote = null, ");
            sSql.Append(" DataHoraReserva = null ");
            sSql.Append(" where Ticket = " + dTicket + "  ");

            sSql.Append(" insert into tLogCancelamento ");
            sSql.Append(" (TextoLog, IDUsuarioCadastro) ");
            sSql.Append(" values ");
            sSql.Append(" ('" + sTextoLog + "', " + iIDUsuario + ") ");

            /////////////////////////////////////////////////////////////////////////
            //Verifica se não tem cadeira selecionada na mesa. Se não tiver, libera todas as cadeiras bloqueadas.            

            sSql.Append("     if not exists (	select * ");
            sSql.Append(" 		            from tVendaCadeira ");
            sSql.Append(" 		            where IDVenda = @IDVenda ");
            sSql.Append(" 		            and IDStatusCadeira = 2 ");
            sSql.Append(" 		            and IDCadeira  in (select IDCadeira from tCadeira where Mesa =  @Mesa)) ");
            sSql.Append("         update tVendaCadeira  ");
            sSql.Append("         set IDStatusCadeira = 1, ");
            sSql.Append("         IDVenda = null,   ");
            sSql.Append("         Valor = null,   ");
            sSql.Append("         IDTipoIngresso = null,   ");
            sSql.Append("         Lote = null,   ");
            sSql.Append("         DataHoraReserva = null   ");
            sSql.Append("         where IDVenda = @IDVenda ");
            sSql.Append("         and IDStatusCadeira = 4 ");
            sSql.Append("         and IDCadeira in (	select IDCadeira  ");
            sSql.Append(" 				            from tCadeira  ");
            sSql.Append(" 				            where Mesa = @Mesa)  ");
            sSql.Append("   else ");
            sSql.Append("       update tVendaCadeira  ");
            sSql.Append("       set IDStatusCadeira = 4, ");
            sSql.Append("       IDVenda = @IDVenda ");
            sSql.Append("       where Ticket = " + dTicket + "  ");

            return sSql.ToString();
        }

        internal string LiberarIngressos(int iIDCliente, int iIDEdicao, int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            if (iIDCliente != -1
                && iIDEdicao != -1
                && iIDCliente != 0
                && iIDEdicao != 0)
            {
                sSql.Append(" delete tVendaAvulso ");
                sSql.Append(" where IDVenda in (select IDVenda ");
                sSql.Append(" 		            from tVenda ");
                sSql.Append(" 		            where IDStatusCompra = 1 ");
                sSql.Append(" 		            and IDCliente = " + iIDCliente + " ");
                sSql.Append(" 		            and IDEdicao = " + iIDEdicao + ") ");

                sSql.Append(" update tVendaCadeira  ");
                sSql.Append(" set IDStatusCadeira = 1,  ");
                sSql.Append(" IDVenda = null,  ");
                sSql.Append(" Valor = null,  ");
                sSql.Append(" IDTipoIngresso = null, ");
                sSql.Append(" Lote = null, ");
                sSql.Append(" DataHoraReserva = null ");
                sSql.Append(" where IDVenda in (select IDVenda ");
                sSql.Append(" 		            from tVenda ");
                sSql.Append(" 		            where IDStatusCompra = 1 ");
                sSql.Append(" 		            and IDCliente = " + iIDCliente + " ");
                sSql.Append(" 		            and IDEdicao = " + iIDEdicao + ") ");

                sSql.Append(" update tVenda ");
                sSql.Append(" set IDStatusCompra = 4 /*Cancelada*/ ");
                sSql.Append(" where IDCliente = " + iIDCliente + " ");
                sSql.Append(" and IDStatusCompra = 1 ");
            }

            if (iIDVenda != -1
                && iIDVenda != 0)
            {
                sSql.Append(" delete tVendaAvulso ");
                sSql.Append(" where IDVenda = " + iIDVenda + " ");
                sSql.Append(" and IDVenda in (select IDVenda ");
                sSql.Append(" 		            from tVenda ");
                sSql.Append(" 		            where IDStatusCompra = 1 ");

                sSql.Append(" update tVendaCadeira  ");
                sSql.Append(" set IDStatusCadeira = 1,  ");
                sSql.Append(" IDVenda = null,  ");
                sSql.Append(" Valor = null,  ");
                sSql.Append(" IDTipoIngresso = null, ");
                sSql.Append(" Lote = null, ");
                sSql.Append(" DataHoraReserva = null ");
                sSql.Append(" where IDVenda = " + iIDVenda + " ");
                sSql.Append(" and IDVenda in (select IDVenda ");
                sSql.Append(" 		            from tVenda ");
                sSql.Append(" 		            where IDStatusCompra = 1 ");
            }

            return sSql.ToString();
        }

        internal string LiberarIngressosPresenciais(int iIDCliente, int iIDEdicao, int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            if (iIDEdicao != -1
                && iIDCliente != 0
                && iIDEdicao != 0)
            {
                sSql.Append(" delete tVendaAvulso ");
                sSql.Append(" where IDVenda in (select IDVenda ");
                sSql.Append(" 		            from tVenda ");
                sSql.Append(" 		            where IDStatusCompra = 1 ");
                sSql.Append(" 		            and IDCliente = " + iIDCliente + " ");
                sSql.Append(" 		            and IDEdicao = " + iIDEdicao + ") ");
            }

            if (iIDVenda != -1
                && iIDVenda != 0)
            {
                sSql.Append(" delete tVendaAvulso ");
                sSql.Append(" where IDVenda = " + iIDVenda + " ");

                sSql.Append(" delete tVenda ");
                sSql.Append(" where IDVenda = " + iIDVenda + " ");
            }
            return sSql.ToString();
        }

        internal string LiberarCadeiras()
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tVendaCadeira  ");
            sSql.Append(" set IDStatusCadeira = 1,  ");
            sSql.Append(" IDVenda = null,  ");
            sSql.Append(" Valor = null,  ");
            sSql.Append(" IDTipoIngresso = null, ");
            sSql.Append(" Lote = null, ");
            sSql.Append(" DataHoraReserva = null ");
            sSql.Append(" where IDVenda in (select IDVenda ");
            sSql.Append(" 		            from tVenda ");
            sSql.Append(" 		            where (dateadd(minute, " + Convert.ToInt32(ConfigurationManager.AppSettings["TempoReservaIngresso"].ToString()) + ", DataHoraReserva) < Getdate()) ");
            sSql.Append(" 		            and IDStatusCompra = (1)) ");
            sSql.Append(" and IDStatusCadeira in (2,4) ");
            sSql.Append(" and (dateadd(minute, " + Convert.ToInt32(ConfigurationManager.AppSettings["TempoReservaIngresso"].ToString()) + ", DataHoraReserva) < Getdate() or DataHoraReserva is null) ");

            return sSql.ToString();
        }

        internal string LiberarCadeira(venda Cadeira)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" Declare ");
            sSql.Append(" @IDCadeira bigint = -1, @IDEdicao int ");

            sSql.Append(" select @IDCadeira = IDCadeira, @IDEdicao = IDEdicao ");
            sSql.Append(" from tVendaCadeira vc ");
            sSql.Append(" where IDVenda = " + Cadeira.IDVenda + " ");
            sSql.Append(" and IDCadeira in (select IDCadeira ");
            sSql.Append(" 		            from tCadeira ");
            sSql.Append(" 		            where Mesa = " + Cadeira.Mesa + " and Cadeira = " + Cadeira.Cadeira + ") ");

            /////////////////////////////////////////////////////////////////////////////////////////

            sSql.Append(" update tVendaCadeira ");
            sSql.Append(" set IDStatusCadeira = 1,  ");
            sSql.Append(" IDVenda = null,  ");
            sSql.Append(" Valor = null,  ");
            sSql.Append(" IDTipoIngresso = null,  ");
            sSql.Append(" Lote = null,  ");
            sSql.Append(" DataHoraReserva = null  ");
            sSql.Append(" where IDVenda = " + Cadeira.IDVenda + " ");
            sSql.Append(" and IDCadeira in (select IDCadeira ");
            sSql.Append(" 		            from tCadeira ");
            sSql.Append(" 		            where Mesa = " + Cadeira.Mesa + " and Cadeira = " + Cadeira.Cadeira + ") ");

            //Verifica se não tem cadeira selecionada na mesa. Se não tiver, libera todas as cadeiras bloqueadas.
            sSql.Append(" if not exists (	select * ");
            sSql.Append(" 	            from tVendaCadeira ");
            sSql.Append(" 	            where IDVenda = " + Cadeira.IDVenda + " ");
            sSql.Append(" 	            and IDStatusCadeira = 2 ");
            sSql.Append(" 	            and IDCadeira  in (select IDCadeira from tCadeira where Mesa = " + Cadeira.Mesa + ")) ");
            sSql.Append("     update tVendaCadeira  ");
            sSql.Append("     set IDStatusCadeira = 1, ");
            sSql.Append("     IDVenda = null,   ");
            sSql.Append("     Valor = null,   ");
            sSql.Append("     IDTipoIngresso = null,   ");
            sSql.Append("     Lote = null,   ");
            sSql.Append("     DataHoraReserva = null  ");
            sSql.Append("     where IDVenda = " + Cadeira.IDVenda + " ");
            sSql.Append("     and IDStatusCadeira = 4 ");
            sSql.Append("     and IDCadeira in (	select IDCadeira  ");
            sSql.Append(" 			            from tCadeira  ");
            sSql.Append(" 			            where Mesa = " + Cadeira.Mesa + ")  ");
            sSql.Append(" else ");
            sSql.Append("   update tVendaCadeira ");
            sSql.Append("   set IDStatusCadeira = 4,  ");
            sSql.Append("   IDVenda = " + Cadeira.IDVenda + "  ");
            sSql.Append("   where IDCadeira = @IDCadeira ");
            sSql.Append("   and IDEdicao = @IDEdicao ");

            return sSql.ToString();
        }

        internal string LiberarIngressos(int iIDCliente)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" delete tVendaAvulso ");
            sSql.Append(" where IDVenda in (select IDVenda ");
            sSql.Append(" 		            from tVenda ");
            sSql.Append(" 		            where IDStatusCompra = 1 ");
            sSql.Append(" 		            and IDCliente = " + iIDCliente + ") ");

            sSql.Append(" update tVendaCadeira  ");
            sSql.Append(" set IDStatusCadeira = 1,  ");
            sSql.Append(" IDVenda = null,  ");
            sSql.Append(" Valor = null,  ");
            sSql.Append(" IDTipoIngresso = null, ");
            sSql.Append(" Lote = null, ");
            sSql.Append(" DataHoraReserva = null ");
            sSql.Append(" where IDVenda in (select IDVenda ");
            sSql.Append(" 		            from tVenda ");
            sSql.Append(" 		            where IDStatusCompra in (1, 4) "); //Em andamento, Cancelada
            sSql.Append(" 		            and IDCliente = " + iIDCliente + ") ");

            return sSql.ToString();
        }

        internal string CancelarVenda(int iIDVenda, int iIDUsuarioCancelamento)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" insert into tIngressoCancelamento ");
            sSql.Append(" (Ticket, IDVenda, IDTipoIngresso, Valor, Lote, Tipo, IDUsuarioCancelamento) ");
            sSql.Append(" select Ticket, IDVenda, IDTipoIngresso, Valor, Lote, TipoIngresso, " + iIDUsuarioCancelamento + " ");
            sSql.Append(" from vVendaTicket vv ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");

            sSql.Append(" delete tVendaAvulso ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");

            sSql.Append(" update tVendaCadeira  ");
            sSql.Append(" set IDStatusCadeira = 1,  ");
            sSql.Append(" IDVenda = null,  ");
            sSql.Append(" Valor = null,  ");
            sSql.Append(" IDTipoIngresso = null, ");
            sSql.Append(" Lote = null, ");
            sSql.Append(" DataHoraReserva = null ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");

            sSql.Append(" update tVenda ");
            sSql.Append(" set IDStatusCompra = 4, ");
            sSql.Append(" IDUsuarioCancelamento = " + iIDUsuarioCancelamento + ", ");
            sSql.Append(" DataHoraCancelamento = GETDATE() ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");

            sSql.Append(" delete tIngressoEstacionamento ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");

            return sSql.ToString();
        }

        internal string ConcluirVenda(int iIDVenda, int iIDFormaPagamento, string sDetalhesFormaPagamento, int iNumeroVagas,
            int iNumeroCriancas, string sNomeRetirada, string sRgRetirada, int iIDLocalRetirada, string sNomeRetirada2, string sRgRetirada2)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tVenda ");
            sSql.Append(" set DataHoraCompra = GETDATE(), ");
            sSql.Append(" IDFormaPagamento = " + iIDFormaPagamento + ", ");
            sSql.Append(" DetalhesFormaPagamento = '" + sDetalhesFormaPagamento + "', ");
            sSql.Append(" IDStatusCompra = 2, ");
            sSql.Append(" NumeroVagas = " + iNumeroVagas + ", ");
            sSql.Append(" NumeroCriancas = " + iNumeroCriancas + ", ");
            sSql.Append(" NomeRetirada = '" + sNomeRetirada + "', ");
            sSql.Append(" RgRetirada = '" + sRgRetirada + "', ");
            sSql.Append(" IDLocalRetirada = " + iIDLocalRetirada + ",");
            sSql.Append(" NomeRetirada2 = '" + sNomeRetirada2 + "',");
            sSql.Append(" RgRetirada2 = '" + sRgRetirada2 + "'");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");

            sSql.Append(" update tVendaCadeira ");
            sSql.Append(" set IDStatusCadeira = 3 ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");
            sSql.Append(" and IDStatusCadeira = 2 /*Em processo de compra*/ ");

            sSql.Append(" update tVendaCadeira  ");
            sSql.Append(" set IDStatusCadeira = 1,  ");
            sSql.Append(" IDVenda = null,  ");
            sSql.Append(" Valor = null,  ");
            sSql.Append(" IDTipoIngresso = null, ");
            sSql.Append(" Lote = null, ");
            sSql.Append(" DataHoraReserva = null ");
            sSql.Append(" where IDStatusCadeira = 4 ");
            sSql.Append(" and IDVenda = " + iIDVenda + " ");

            if (iIDFormaPagamento == 8) //Cortesia
            {
                sSql.Append(" update tVendaAvulso ");
                sSql.Append(" set Valor = 0 ");
                sSql.Append(" where IDVenda = " + iIDVenda + " ");

                sSql.Append(" update tVendaCadeira ");
                sSql.Append(" set Valor = 0 ");
                sSql.Append(" where IDVenda = " + iIDVenda + " ");
            }

            return sSql.ToString();
        }

        internal string AtualizarVendaComprovanteCaixa(int iIDVenda, int iIDFormaPagamento, string sDetalhesFormaPagamento, int iNumeroVagas,
            int iNumeroCriancas, string sNomeRetirada, string sRgRetirada, int iIDLocalRetirada, string sNomeSegundoRetirada, string sRgSegundoRetirada)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tVenda ");
            sSql.Append(" set DataHoraCompra = GETDATE(), ");
            sSql.Append(" IDFormaPagamento = " + iIDFormaPagamento + ", ");
            sSql.Append(" DetalhesFormaPagamento = '" + sDetalhesFormaPagamento + "', ");
            sSql.Append(" IDStatusCompra = 5, ");
            sSql.Append(" NumeroVagas = " + iNumeroVagas + ", ");
            sSql.Append(" NumeroCriancas = " + iNumeroCriancas + ", ");
            sSql.Append(" NomeRetirada = '" + sNomeRetirada + "', ");
            sSql.Append(" RgRetirada = '" + sRgRetirada + "', ");
            sSql.Append(" IDLocalRetirada = " + iIDLocalRetirada + ",");
            sSql.Append(" NomeRetirada2 = '" + sNomeSegundoRetirada + "',");
            sSql.Append(" RgRetirada2 = '" + sRgSegundoRetirada + "'");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");

            sSql.Append(" update tVendaCadeira ");
            sSql.Append(" set IDStatusCadeira = 3 ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");
            sSql.Append(" and IDStatusCadeira = 2 /*Em processo de compra*/ ");

            sSql.Append(" update tVendaCadeira ");
            sSql.Append(" set IDStatusCadeira = 1, ");
            sSql.Append(" IDVenda = null ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");
            sSql.Append(" and IDStatusCadeira = 4 /*Bloqueada*/ ");

            return sSql.ToString();
        }

        internal string AtualizarVendaReservada(int iIDVenda, int iIDFormaPagamento, string sDetalhesFormaPagamento, int iNumeroVagas,
            int iNumeroCriancas, string sNomeRetirada, string sRgRetirada, int iIDLocalRetirada, string sNomeSegundoRetirada, string sRgSegundoRetirada)
        {
            StringBuilder sSql = new StringBuilder();

            sSql.Append(" update tVenda ");
            sSql.Append(" set DataHoraCompra = GETDATE(), ");
            sSql.Append(" IDFormaPagamento = " + iIDFormaPagamento + ", ");
            sSql.Append(" DetalhesFormaPagamento = '" + sDetalhesFormaPagamento + "', ");
            sSql.Append(" IDStatusCompra = 6, "); //Reservada
            sSql.Append(" NumeroVagas = " + iNumeroVagas + ", ");
            sSql.Append(" NumeroCriancas = " + iNumeroCriancas + ", ");
            sSql.Append(" NomeRetirada = '" + sNomeRetirada + "', ");
            sSql.Append(" RgRetirada = '" + sRgRetirada + "', ");
            sSql.Append(" IDLocalRetirada = " + iIDLocalRetirada + ",");
            sSql.Append(" NomeRetirada2 = '" + sNomeSegundoRetirada + "',");
            sSql.Append(" RgRetirada2 = '" + sRgSegundoRetirada + "'");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");

            sSql.Append(" update tVendaCadeira ");
            sSql.Append(" set IDStatusCadeira = 5 ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");
            sSql.Append(" and IDStatusCadeira = 2 /*Em processo de compra*/ ");

            sSql.Append(" update tVendaCadeira ");
            sSql.Append(" set IDStatusCadeira = 1, ");
            sSql.Append(" IDVenda = null ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");
            sSql.Append(" and IDStatusCadeira = 4 /*Bloqueada*/ ");

            return sSql.ToString();
        }

        internal string RetornarVendasCliente(int iIDCliente)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select v.IDVenda, ");
            sSql.Append(" ev.Evento, ed.Edicao, ");
            sSql.Append(" CONVERT(varchar, v.DataHoraCompra, 103) + ' ' + CONVERT(varchar, v.DataHoraCompra, 108) DataHoraCompra ");
            sSql.Append(" from tVenda v ");
            sSql.Append(" inner join tEdicao ed on v.IDEdicao = ed.IDEdicao ");
            sSql.Append(" inner join tEvento ev on ed.IDEvento = ev.IDEvento ");
            sSql.Append(" where v.IDCliente = " + iIDCliente + " ");
            sSql.Append(" and v.IDStatusCompra in (2,3) ");
            sSql.Append(" and (ed.DataHoraEvento + 1) > GETDATE() ");
            sSql.Append(" order by v.IDVenda desc ");

            return sSql.ToString();
        }

        internal string RetornarVendas(int iIDVenda, string sNome, string sCPF, string sIDStatusCompra)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select top 50 v.IDVenda, c.Nome, c.CPF, c.NumeroCota, ");
            sSql.Append(" CONVERT(varchar, v.DataHoraCompra, 103) + ' ' + CONVERT(varchar, v.DataHoraCompra, 108) [Data/Hora Compra], ");
            sSql.Append(" fp.FormaPagamento, sc.Status StatusCompra, ev.Evento, ed.Edicao, vou.NumeroIngressos, ");
            sSql.Append(" CONVERT(varchar, v.DataHoraEntregaIngresso, 103) + ' ' + CONVERT(varchar, v.DataHoraEntregaIngresso, 108) [Data/Hora Entrega], ");
            sSql.Append(" ue.Nome [Responsável Baixa/Entrega], ");
            sSql.Append(" CONVERT(varchar, v.DataHoraCancelamento, 103) + ' ' + CONVERT(varchar, v.DataHoraCancelamento, 108) [Data/Hora Cancelamento], ");
            sSql.Append(" uc.Nome [Responsável Cancelamento], v.IDStatusCompra, c.IDUsuario,v.NomeRetirada2,RgRetirada2, ");
            sSql.Append(" uco.Nome [Responsável pela Compra] ");
            sSql.Append(" from tVenda v ");
            sSql.Append(" inner join tCliente c on v.IDCliente = c.IDCliente ");
            sSql.Append(" inner join tFormaPagamento fp on v.IDFormaPagamento = fp.IDFormaPagamento ");
            sSql.Append(" inner join tStatusCompra sc on v.IDStatusCompra = sc.IDStatus ");
            sSql.Append(" inner join tEdicao ed on v.IDEdicao = ed.IDEdicao ");
            sSql.Append(" inner join tEvento ev on ed.IDEvento = ev.IDEvento ");
            sSql.Append(" left join tUsuario ue on ue.IDUsuario = v.IDUsuarioEntregaIngresso ");
            sSql.Append(" inner join tUsuario uco on uco.IDUsuario = v.IDUsuarioCadastro ");
            sSql.Append(" left join tUsuario uc on uc.IDUsuario = v.IDUsuarioCancelamento ");
            sSql.Append(" left join (	select IDVenda, COUNT(*) NumeroIngressos ");
            sSql.Append("             from vVendaVoucher ");
            sSql.Append("             group by IDVenda) vou on vou.IDVenda = v.IDVenda ");
            sSql.Append(" where v.IDStatusCompra in (" + sIDStatusCompra + ") ");

            if (!String.IsNullOrEmpty(sNome))
                sSql.Append(" and c.Nome like '%" + sNome + "%' ");

            if (!String.IsNullOrEmpty(sCPF))
                sSql.Append(" and c.CPF like '%" + sCPF + "%' ");

            if (iIDVenda != 0 && iIDVenda != -1)
                sSql.Append(" and v.IDVenda = " + iIDVenda + "  ");

            sSql.Append(" order by v.IDVenda desc ");

            return sSql.ToString();
        }

        internal string RetornarIngressos(int iIDVenda, string sTickets)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select case when (vv.FormaPagamento = 'Cortesia') then 'Cortesia' else vv.Nome end Nome, ");
            sSql.Append(" vv.IDCliente, vv.IDVenda, ");
            sSql.Append(" case when (vv.FormaPagamento = 'Cortesia') then 'Cortesia' else vv.CPF end CPF, ");
            sSql.Append(" vv.Ticket, vv.Valor,vv.Tipo,  ");
            sSql.Append(" c.NumeroCota, c.DigitoCota, ");
            sSql.Append(" Convert(varchar,e.DataHoraEvento,103) as Data, Convert(varchar,e.DataHoraEvento,108) as Hora, ");
            sSql.Append(" case when (vv.Evento = 'Reveillon') then '~/images/rodape_Reveillon.png' ");
            sSql.Append(" when (vv.Evento = 'Boate') then '~/images/boate_pic.png' when (vv.Evento = 'Festa Junina') then '~/images/junina_logo2.png' when (vv.Evento = 'Festa Junina') then '~/images/Outros.png' end EventoImagem, ");
            sSql.Append(" le.Endereco EnderecoEvento, ");
            sSql.Append(" vv.IdentidadeEletronica, vv.Evento, vv.Edicao ");
            sSql.Append(" from vVendaVoucher vv");
            sSql.Append(" inner join tEdicao e on vv.IDEdicao = e.IDEdicao ");
            sSql.Append(" inner join tCliente c on c.IDCliente = vv.IDCliente");
            sSql.Append(" left join tLocalEvento le on e.IDLocal = le.IDLocal  ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");

            if (!String.IsNullOrEmpty(sTickets))
                sSql.Append(" and vv.Ticket in (" + sTickets + ") ");

            sSql.Append(" order by vv.Ticket ");

            return sSql.ToString();
        }

        internal string RetornarIngressos(string sTickets)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select case when (vv.FormaPagamento = 'Cortesia') then 'Cortesia' else vv.Nome end Nome, ");
            sSql.Append(" vv.IDCliente, vv.IDVenda, ");
            sSql.Append(" case when (vv.FormaPagamento = 'Cortesia') then 'Cortesia' else vv.CPF end CPF, ");
            sSql.Append(" vv.Ticket, vv.Valor,vv.Tipo,  ");
            sSql.Append(" c.NumeroCota, c.DigitoCota, ");
            sSql.Append(" Convert(varchar,e.DataHoraEvento,103) as Data, Convert(varchar,e.DataHoraEvento,108) as Hora, ");
            sSql.Append(" case when (vv.Evento = 'Reveillon') then '~/images/rodape_Reveillon.png' ");
            sSql.Append(" when (vv.Evento = 'Boate') then '~/images/boate_pic.png' when (vv.Evento = 'Festa Junina') then '~/images/junina_logo2.png' when (vv.Evento = 'Festa Junina') then '~/images/Outros.png' end EventoImagem, ");
            sSql.Append(" le.Endereco EnderecoEvento, ");
            sSql.Append(" vv.IdentidadeEletronica, vv.Evento, vv.Edicao ");
            sSql.Append(" from vVendaVoucher vv");
            sSql.Append(" inner join tEdicao e on vv.IDEdicao = e.IDEdicao ");
            sSql.Append(" inner join tCliente c on c.IDCliente = vv.IDCliente");
            sSql.Append(" left join tLocalEvento le on e.IDLocal = le.IDLocal  ");
            sSql.Append(" where vv.Ticket in (" + sTickets + ") ");
            sSql.Append(" order by vv.Ticket ");

            return sSql.ToString();
        }

        internal string RetornarTickets(int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select vv.IDVenda, vv.Lote, vv.Ticket, vv.Tipo, vv.Valor, ");
            sSql.Append(" 1 Ativo ");
            sSql.Append(" from vVendaVoucher vv ");
            sSql.Append(" inner join tEdicao e on vv.IDEdicao = e.IDEdicao ");
            sSql.Append(" inner join tCliente c on c.IDCliente = vv.IDCliente ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");

            sSql.Append(" union ");

            sSql.Append(" select ic.IDVenda, ic.Lote, ic.Ticket, ic.Tipo, ic.Valor,  ");
            sSql.Append(" 0 Ativo  ");
            sSql.Append(" from tIngressoCancelamento ic  ");
            sSql.Append(" where ic.IDVenda = " + iIDVenda + " ");

            return sSql.ToString();
        }

        internal string RetornarVenda(int iIDVenda, string sIDStatusCompra)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select vv.* ");
            sSql.Append(" from vVenda vv ");
            sSql.Append(" where vv.IDVenda = " + iIDVenda + " ");

            if (!String.IsNullOrEmpty(sIDStatusCompra))
                sSql.Append(" and vv.IDStatusCompra in (" + sIDStatusCompra + ") ");

            return sSql.ToString();
        }

        internal string RetornarIngressosRecibo(int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select Resumo [Tipo de Ingresso], COUNT(*) Quantidade ");
            sSql.Append(" from vVendaTicket ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");
            sSql.Append(" group by Resumo ");

            return sSql.ToString();
        }

        internal string EntregarIngresso(int iIDVenda, int iIDUsuarioEntregaIngresso)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tVenda ");
            sSql.Append(" set IDStatusCompra = 3,  "); //Ingresso entregue
            sSql.Append(" DataHoraEntregaIngresso = GETDATE(), ");
            sSql.Append(" IDUsuarioEntregaIngresso = " + iIDUsuarioEntregaIngresso + " ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");

            return sSql.ToString();
        }

        internal string RetornarFormasPagamento(string sIDFormaPagamento)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from tFormaPagamento ");

            if (!String.IsNullOrEmpty(sIDFormaPagamento))
                sSql.Append(" where IDFormaPagamento in (" + sIDFormaPagamento + ") ");

            return sSql.ToString();
        }

        internal string RetornarSaldoIngressos(int iIDEdicao, int iNumeroCota, int iIDCliente, int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" exec sRetornarSaldoIngressos ");
            sSql.Append(" @IDEdicao = " + iIDEdicao + ", ");
            sSql.Append(" @NumeroCota = " + iNumeroCota + ", ");
            sSql.Append(" @IDCliente = " + iIDCliente + ", ");
            sSql.Append(" @IDVenda = " + iIDVenda + " ");

            return sSql.ToString();
        }

        internal string RetornarSaldoIngressosBKP(int iIDEdicao, int iNumeroCota, int iIDCliente, int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            //0 - ingressos da venda atual
            sSql.Append(" select count(*) QuantidadeCarrinhoVendaAtual ");
            sSql.Append(" from vVendaTicket ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");

            //1 - ingressos já comprados pelo cliente
            sSql.Append(" select COUNT(*) QuantidadeComprado ");
            sSql.Append(" from vVendaTicket t ");
            sSql.Append(" inner join tVenda v on t.IDVenda = v.IDVenda ");
            sSql.Append(" left join tCliente c on v.IDCliente = c.IDCliente ");
            sSql.Append(" where v.IDStatusCompra in (2,3,5) ");
            sSql.Append(" and (v.IDCliente = " + iIDCliente + " ");
            if (iNumeroCota != 0 && iNumeroCota != -1) sSql.Append(" or c.NumeroCota = " + iNumeroCota + " ");
            sSql.Append(" ) and v.IDEdicao = " + iIDEdicao + "");

            //2 - cadeiras da venda atual
            sSql.Append(" select count(*) QuantidadeCadeiraVendaAtual ");
            sSql.Append(" from vVendaTicket vt ");
            sSql.Append(" where vt.IDTipoIngresso in (1,2,5,6,9,10,11,12)  "); // inserido 9,10,11,12 para Boate PIC
            sSql.Append(" and vt.IDVenda = " + iIDVenda + " ");

            //3 - cadeiras já compradas pelo cliente
            sSql.Append(" select COUNT(*) QuantidadeComprado ");
            sSql.Append(" from vVendaTicket t ");
            sSql.Append(" inner join tVenda v on t.IDVenda = v.IDVenda ");
            sSql.Append(" left join tCliente c on v.IDCliente = c.IDCliente ");
            sSql.Append(" where v.IDStatusCompra in (2,3,5) ");
            sSql.Append(" and t.IDTipoIngresso in (1,2,5,6,9,10,11,12)  "); // inserido 9,10,11,12 para Boate PIC
            sSql.Append(" and (v.IDCliente = " + iIDCliente + " ");
            if (iNumeroCota != 0 && iNumeroCota != -1) sSql.Append(" or c.NumeroCota = " + iNumeroCota + " ");
            sSql.Append(" ) and v.IDEdicao = " + iIDEdicao + "");

            //4 - ingressos com valor de sócio da venda atual
            sSql.Append(" select COUNT(*) QuantidadeValorSocioVendaAtual ");
            sSql.Append(" from vVendaTicket vt ");
            sSql.Append(" inner join tTipoIngresso te on vt.IDTipoIngresso = te.IDTipoIngresso ");
            sSql.Append(" where vt.IDVenda = " + iIDVenda + "  ");
            sSql.Append(" and te.TipoCliente = 'Sócio' ");

            //5 - ingressos com valor de não sócio da venda atual
            sSql.Append(" select COUNT(*) QuantidadeValorNaoSocioVendaAtual ");
            sSql.Append(" from vVendaTicket vt ");
            sSql.Append(" inner join tTipoIngresso te on vt.IDTipoIngresso = te.IDTipoIngresso ");
            sSql.Append(" where vt.IDVenda = " + iIDVenda + "  ");
            sSql.Append(" and te.TipoCliente = 'Não sócio' ");

            //6 - ingressos já comprados com valor de sócio
            sSql.Append(" select COUNT(*) QuantidadeValorSocioComprado ");
            sSql.Append(" from vVendaTicket vt ");
            sSql.Append(" inner join tTipoIngresso te on vt.IDTipoIngresso = te.IDTipoIngresso ");
            sSql.Append(" inner join vVenda v on vt.IDVenda = v.IDVenda  ");
            sSql.Append(" where (v.IDCliente = " + iIDCliente + " ");
            if (iNumeroCota != 0 && iNumeroCota != -1) sSql.Append(" or v.NumeroCota = " + iNumeroCota + " ");
            sSql.Append(" ) and v.IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" and v.IDStatusCompra in (2,3,5) ");
            sSql.Append(" and te.TipoCliente = 'Sócio' ");

            //7 - ingressos já comprados com valor de não sócio
            sSql.Append(" select COUNT(*) QuantidadeValorNaoSocioComprado ");
            sSql.Append(" from vVendaTicket vt ");
            sSql.Append(" inner join tTipoIngresso te on vt.IDTipoIngresso = te.IDTipoIngresso ");
            sSql.Append(" inner join vVenda v on vt.IDVenda = v.IDVenda  ");
            sSql.Append(" where (v.IDCliente = " + iIDCliente + " ");
            if (iNumeroCota != 0 && iNumeroCota != -1) sSql.Append(" or v.NumeroCota = " + iNumeroCota + " ");
            sSql.Append(" ) and v.IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" and v.IDStatusCompra in (2,3,5) ");
            sSql.Append(" and te.TipoCliente = 'Não sócio' ");

            //8 - avulsos da venda atual
            sSql.Append(" select count(*) QuantidadeCadeiraVendaAtual ");
            sSql.Append(" from vVendaTicket vt ");
            sSql.Append(" where vt.IDTipoIngresso in (3,4,7,6)  ");
            sSql.Append(" and vt.IDVenda = " + iIDVenda + " ");

            return sSql.ToString();
        }

        internal string RetornarQuantidadeVendidaCadeira(int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select COUNT(*) QuantidadeVendido ");
            sSql.Append(" from tVendaCadeira vc ");
            sSql.Append(" inner join tVenda v on vc.IDVenda = v.IDVenda ");
            sSql.Append(" where v.IDStatusCompra in (2,3,6) ");
            sSql.Append(" and v.IDEdicao = " + iIDEdicao + " ");

            return sSql.ToString();
        }

        internal string RetornarQuantidadeVendidaAvulso(int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select COUNT(*) QuantidadeVendido ");
            sSql.Append(" from tVendaAvulso va ");
            sSql.Append(" inner join tVenda v on va.IDVenda = v.IDVenda ");
            sSql.Append(" where v.IDStatusCompra in (2,3,6) ");
            sSql.Append(" and va.IDTipoIngresso not in (15,16) ");
            sSql.Append(" and v.IDEdicao = " + iIDEdicao + " ");

            return sSql.ToString();
        }

        internal string RetornarQuantidadeVendidaCamarote(int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select COUNT(*) QuantidadeVendido ");
            sSql.Append(" from tVendaAvulso va ");
            sSql.Append(" inner join tVenda v on va.IDVenda = v.IDVenda ");
            sSql.Append(" where v.IDStatusCompra in (2,3,6) ");
            sSql.Append(" and va.IDTipoIngresso in (18,19) ");
            sSql.Append(" and v.IDEdicao = " + iIDEdicao + " ");

            return sSql.ToString();
        }

        internal string AprovarVenda(int iIDVenda, int iIDUsuarioAprovacao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tVenda ");
            sSql.Append(" set IDStatusCompra = 2, ");
            sSql.Append(" DataHoraCompra = getdate(), ");
            sSql.Append(" IDUsuarioAprovacao = " + iIDUsuarioAprovacao + ", ");
            sSql.Append(" DataHoraAprovacao = getdate() ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");

            return sSql.ToString();
        }

        internal string RetornarVagasEstacionamentoReservadas(int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select sum(NumeroVagas) ");
            sSql.Append(" from tVenda ");
            sSql.Append(" where IDStatusCompra in (2,3,5) ");
            sSql.Append(" and IDEdicao = " + iIDEdicao + " ");

            return sSql.ToString();
        }

        internal string CadastrarCartao(int iIDVenda, string sNumeroCartao, string sCodigoSeguranca, string sValidade,
            string sBandeira, int iParcelas, double dValor, string sTitular)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" insert into tCartao ");
            sSql.Append(" (IDVenda, NumeroCartao, CodigoSeguranca, Validade, Bandeira, Parcelas, Valor, Titular) ");
            sSql.Append(" values ");
            sSql.Append(" (" + iIDVenda + ", '" + sNumeroCartao + "', '" + sCodigoSeguranca + "', '" + sValidade + "', '" + sBandeira + "', " + iParcelas + ", '" + dValor.ToString().Replace(",", ".") + "', '" + sTitular + "') ");

            return sSql.ToString();
        }

        internal string CapturarTransacao(int iNumeroDocumento)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tCartao set Capturada = 1 where NumeroDocumento = " + iNumeroDocumento + " ");

            return sSql.ToString();
        }

        internal string VerificarExistenciaVendaEmAndamento(int iIDCliente, int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select v.* ");
            sSql.Append(" from vVendaTicket vt ");
            sSql.Append(" inner join tVenda v on vt.IDVenda = v.IDVenda ");
            sSql.Append(" where v.IDStatusCompra = 1 ");
            sSql.Append(" and v.IDCliente = " + iIDCliente + " ");
            sSql.Append(" and v.IDVenda != " + iIDVenda + " ");

            return sSql.ToString();
        }

        internal string VerificarExistenciaTentativaPagamentoCartao(int iIDVenda, int iSegundos)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from tCartao ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");
            sSql.Append(" and DATEDIFF(second, Cadastro, GETDATE()) <= " + iSegundos + " ");

            return sSql.ToString();
        }

        internal string RetornarTidCartao(int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select tid, IDVenda, Numerodocumento, PaymentId ");
            sSql.Append(" from tCartao ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");
            sSql.Append(" and Aprovada = 1 ");

            return sSql.ToString();
        }

        internal string RetornarDadosCielo(string sPaymentId, string sTid)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select tid, IDVenda, Numerodocumento, PaymentId ");
            sSql.Append(" from tCartao ");
            sSql.Append(" where tid like '%" + sTid + "%' ");
            sSql.Append(" and PaymentId like '%" + sPaymentId + "%' ");

            return sSql.ToString();
        }

        internal string CancelarVendaCartao(int iNumeroDocumento, int iIDUsuarioCancelamento)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tCartao ");
            sSql.Append(" set Cancelamento = getdate(), ");
            sSql.Append(" IDUsuarioCancelamento = " + iIDUsuarioCancelamento + " ");
            sSql.Append(" where Numerodocumento = " + iNumeroDocumento + " ");

            return sSql.ToString();
        }

        internal string AtualizarVenda(int iNumeroVagas, int iIDUsuarioAlteracao, int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tVenda ");
            sSql.Append(" set NumeroVagas = " + iNumeroVagas + ", ");
            sSql.Append(" IDUsuarioAlteracao = " + iIDUsuarioAlteracao + ", ");
            sSql.Append(" DataHoraAlteracao = getdate() ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");

            return sSql.ToString();
        }

        internal string RetornarValoresIngressosAvulsos(int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from vIngressoAvulso ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " ");

            return sSql.ToString();
        }

        internal string RetornarValoresIngressosAvulsos(int iIDEdicao, int iIDEvento)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from vIngressoAvulso ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " ");

            if (iIDEvento == 3) // se for Boate não mostra as meias.
                sSql.Append(" and IDTipoIngresso not in (7, 8) ");

            return sSql.ToString();
        }

        internal string RetornarValoresIngressosCadeiras(int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from vIngressoCadeira ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " ");

            return sSql.ToString();
        }

        internal string RetornarValoresIngressosCadeiras(int iIDEdicao, int iIDEvento)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from vIngressoCadeira ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " ");

            if (iIDEvento == 3) // se for Boate não mostra as meias.
                sSql.Append(" and IDTipoIngresso not in (5,6) ");

            return sSql.ToString();
        }

        internal string RetornarMesasSemReservaCompleta(int iIDVenda, int iNumeroMinimoReserva)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select Mesa, COUNT(*) Quantidade   ");
            sSql.Append(" from vVendaTicket vt ");
            sSql.Append(" inner join tVendaCadeira vc on vt.Ticket = vc.Ticket ");
            sSql.Append(" inner join tCadeira c on vc.IDCadeira = c.IDCadeira ");
            sSql.Append(" where vt.IDTipoIngresso in (1,2,5,6,9,10,11,12)   "); // inserido ,9,10,11,12 para a Boate
            sSql.Append(" and vt.IDVenda = " + iIDVenda + " ");
            sSql.Append(" group by Mesa ");
            sSql.Append(" having count(*) < " + iNumeroMinimoReserva + " ");

            return sSql.ToString();
        }

        internal string RetornarMesasSemReservaCompleta(int iIDVenda, int iNumeroMinimoReserva, int iMesa)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select Mesa, COUNT(*) Quantidade   ");
            sSql.Append(" from vVendaTicket vt ");
            sSql.Append(" inner join tVendaCadeira vc on vt.Ticket = vc.Ticket ");
            sSql.Append(" inner join tCadeira c on vc.IDCadeira = c.IDCadeira ");
            sSql.Append(" where vt.IDTipoIngresso in (1,2,5,6,9,10,11,12)   "); // inserido ,9,10,11,12 para a Boate
            sSql.Append(" and vt.IDVenda = " + iIDVenda + " ");
            sSql.Append(" and Mesa != " + iMesa + " ");
            sSql.Append(" group by Mesa ");
            sSql.Append(" having count(*) < " + iNumeroMinimoReserva + " ");

            return sSql.ToString();
        }

        internal string RetornarIngressosCliente(int iIDCliente)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select v.IDVenda, ");
            sSql.Append(" ev.Evento, ed.Edicao, ");
            sSql.Append(" CONVERT(varchar, v.DataHoraCompra, 103) + ' ' + CONVERT(varchar, v.DataHoraCompra, 108) DataHoraCompra ");
            sSql.Append(" from tVenda v ");
            sSql.Append(" inner join tEdicao ed on v.IDEdicao = ed.IDEdicao ");
            sSql.Append(" inner join tEvento ev on ed.IDEvento = ev.IDEvento ");
            sSql.Append(" inner join tCliente c on v.IDCliente = c.IDCliente ");
            sSql.Append(" where v.IDCliente = " + iIDCliente + " ");
            sSql.Append(" and v.IDStatusCompra in (2,3) ");
            sSql.Append(" and ed.IDStatusIngresso in (1) ");
            sSql.Append(" and (ed.DataHoraEvento + 1) > GETDATE() ");
            sSql.Append(" and c.NumeroCota not in (select Num_Cota from tSocio where Debito = 1) ");
            sSql.Append(" order by v.IDVenda desc ");

            return sSql.ToString();
        }

        internal string RetornarVendasReservadas(string sNome, string sCPF, string sEmail, int iNumeroCota)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select IDVenda, v.Nome, v.CPF, Evento, Edicao,  ");
            sSql.Append(" NumeroIngressos, [Responsável pela Compra], [Data/Hora Compra] [Data/Hora Reserva], ");
            sSql.Append(" '-' [Mesa(s)], u.IDUsuario, v.NumeroCota [Núm. Cota] ");
            sSql.Append(" from vVenda v ");
            sSql.Append(" inner join tCliente c on v.IDCliente = c.IDCliente ");
            sSql.Append(" inner join tUsuario u on u.IDUsuario = c.IDUsuario ");
            sSql.Append(" where v.IDVenda in (select IDVenda from tVendaCadeira c where c.IDStatusCadeira = 5 /*Reservada*/)  ");
            sSql.Append(" and v.IDStatusCompra = 6 /*Reservada*/  ");

            if (!String.IsNullOrEmpty(sNome))
                sSql.Append(" and u.Nome like '%" + sNome + "%' COLLATE SQL_Latin1_General_CP1_CI_AI ");

            if (!String.IsNullOrEmpty(sCPF))
                sSql.Append(" and c.CPF like '%" + sCPF + "%' ");

            if (!String.IsNullOrEmpty(sEmail))
                sSql.Append(" and u.Email like '%" + sEmail + "%' ");

            if (iNumeroCota != 0 && iNumeroCota != -1)
                sSql.Append(" and c.NumeroCota = " + iNumeroCota + " ");

            sSql.Append(" order by v.IDVenda desc  ");

            return sSql.ToString();
        }

        internal string RetornarMesasReservadas(int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select c.Mesa, Setor ");
            sSql.Append(" from tVendaCadeira vc ");
            sSql.Append(" inner join tCadeira c on vc.IDCadeira = c.IDCadeira ");
            sSql.Append(" where vc.IDStatusCadeira = 5 ");
            sSql.Append(" and vc.IDVenda = " + iIDVenda + " ");
            sSql.Append(" group by c.Mesa, Setor ");

            return sSql.ToString();
        }

        internal string RetornarEstacionamentos(int iIDCliente)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select v.IDVenda, ");
            sSql.Append(" ev.Evento, ed.Edicao, ");
            sSql.Append(" CONVERT(varchar, v.DataHoraCompra, 103) + ' ' + CONVERT(varchar, v.DataHoraCompra, 108) DataHoraCompra ");
            sSql.Append(" from tVenda v ");
            sSql.Append(" inner join tEdicao ed on v.IDEdicao = ed.IDEdicao ");
            sSql.Append(" inner join tEvento ev on ed.IDEvento = ev.IDEvento ");
            sSql.Append(" inner join tCliente c on v.IDCliente = c.IDCliente ");
            sSql.Append(" where v.IDCliente = " + iIDCliente + " ");
            sSql.Append(" and v.IDStatusCompra in (2,3) ");
            sSql.Append(" and ed.IDStatusIngresso in (1) ");
            sSql.Append(" and v.NumeroVagas > 0 ");
            sSql.Append(" and (ed.DataHoraEvento + 1) > GETDATE() ");
            sSql.Append(" and c.NumeroCota not in (select Num_Cota from tSocio where Debito = 1) ");
            sSql.Append(" order by v.IDVenda desc ");

            return sSql.ToString();
        }

        internal string RetornarIngressosEstacionamento(int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select ie.IdentidadeEletronica, v.IDCliente, v.IDVenda, v.Nome, CONVERT(varchar(50), v.NumeroCota) NumeroCota, c.DigitoCota, v.NumeroVagas, v.NumeroVagas, v.CPF, Avulso, ");
            sSql.Append(" CONVERT(varchar, ed.DataHoraEvento, 103) DataEvento, CONVERT(varchar(5), ed.DataHoraEvento, 108) HoraEvento,  ");
            sSql.Append(" ie.Ticket ");
            sSql.Append(" from tIngressoEstacionamento ie ");
            sSql.Append(" left join vVenda v on ie.IDVenda = v.IDVenda ");
            sSql.Append(" left join tCliente c on v.IDCliente = c.IDCliente ");
            sSql.Append(" left join tEdicao ed on v.IDEdicao = ed.IDEdicao ");
            sSql.Append(" where v.IDVenda = " + iIDVenda + " ");
            sSql.Append(" and v.NumeroVagas > 0 ");
            sSql.Append(" and ie.Ativo = 1 ");

            return sSql.ToString();
        }

        internal string RetornarIngressosEstacionamento(string sIdentidadeEletronica)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select ie.IdentidadeEletronica, 0 IDCliente, 0 IDVenda, ie.Nome, '0' NumeroCota, ");
            sSql.Append(" 0 DigitoCota, 1 NumeroVagas, ie.CPF, Avulso, ");
            sSql.Append(" CONVERT(varchar, ed.DataHoraEvento, 103) DataEvento, CONVERT(varchar(5), ed.DataHoraEvento, 108) HoraEvento, ");
            sSql.Append(" ie.Ticket ");
            sSql.Append(" from tIngressoEstacionamento ie ");
            sSql.Append(" left join vVenda v on ie.IDVenda = v.IDVenda ");
            sSql.Append(" left join tCliente c on v.IDCliente = c.IDCliente ");
            sSql.Append(" left join tEdicao ed on ie.IDEdicao = ed.IDEdicao ");
            sSql.Append(" where ie.IdentidadeEletronica = '" + sIdentidadeEletronica + "' ");
            sSql.Append(" and Avulso = 1 ");
            sSql.Append(" and ie.Ativo = 1 ");

            return sSql.ToString();
        }

        //internal string RegistrarEntradaEstacionamento(int iIDVenda)
        //{
        //    StringBuilder sSql = new StringBuilder();
        //    sSql.Append(" update tVenda ");
        //    sSql.Append(" set DataHoraEntradaEstacionamento = GETDATE() ");
        //    sSql.Append(" where IDVenda = " + iIDVenda);

        //    sSql.Append(" update tEstacionamentoAvulso ");
        //    sSql.Append(" set DataHoraEntradaEstacionamento = GETDATE() ");
        //    sSql.Append(" where IDEstacionamento = " + iIDVenda);

        //    return sSql.ToString();
        //}

        internal string RegistrarEntradaIngresso(int iTicket)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tVendaCadeira ");
            sSql.Append(" set DataHoraEntradaIngresso = GETDATE() ");
            sSql.Append(" where Ticket = " + iTicket);

            sSql.Append(" update tVendaAvulso ");
            sSql.Append(" set DataHoraEntradaIngresso = GETDATE() ");
            sSql.Append(" where Ticket = " + iTicket);

            return sSql.ToString();
        }

        //internal string CancelarEntradaEstacionamento(int iIDVenda)
        //{
        //    StringBuilder sSql = new StringBuilder();
        //    sSql.Append(" update tVenda ");
        //    sSql.Append(" set DataHoraEntradaEstacionamento = null ");
        //    sSql.Append(" where IDVenda = " + iIDVenda);

        //    sSql.Append(" update tEstacionamentoAvulso ");
        //    sSql.Append(" set DataHoraEntradaEstacionamento = null ");
        //    sSql.Append(" where IDEstacionamento = " + iIDVenda);

        //    return sSql.ToString();
        //}

        internal string CancelarEntradaIngresso(int iTicket)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tVendaCadeira ");
            sSql.Append(" set DataHoraEntradaIngresso = null ");
            sSql.Append(" where Ticket = " + iTicket);

            sSql.Append(" update tVendaAvulso ");
            sSql.Append(" set DataHoraEntradaIngresso = null ");
            sSql.Append(" where Ticket = " + iTicket);

            return sSql.ToString();
        }

        internal string RetornarTicketsCatraca(int iIDEdicao, string sTipo)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select Ticket, IdentidadeEletronica ");
            sSql.Append(" from vVendaVoucher ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " ");

            if (sTipo == "Inteira")
                sSql.Append(" and IDTipoIngresso in (1,2,3,4) ");

            else if (sTipo == "Meio Adolescente")
                sSql.Append(" and IDTipoIngresso in (5,6,7,8) ");

            sSql.Append(" order by Ticket ");

            return sSql.ToString();
        }

        internal string CadastrarEstacionamentoAvulso(string sIdentidadeEletronica, int iIDEdicao, string sNome, string sCPF, int iIDUsuarioCadastro)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" insert into tIngressoEstacionamento ");
            sSql.Append(" (IdentidadeEletronica, IDEdicao, Avulso, Nome, CPF, IDUsuarioCadastro) ");
            sSql.Append(" values ");
            sSql.Append(" ('" + sIdentidadeEletronica + "', " + iIDEdicao + ", 1, '" + sNome + "', '" + sCPF + "', " + iIDUsuarioCadastro + ") ");

            return sSql.ToString();
        }

        internal string RetornarEstacionamentosAvulsos(int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select IdentidadeEletronica, es.Nome, es.CPF,  ");
            sSql.Append(" ev.Evento + ' - ' + ed.Edicao Evento,  ");
            sSql.Append(" CONVERT(varchar, es.DataHoraCadastro, 103) + ' ' + CONVERT(varchar, es.DataHoraCadastro, 108)[Data/Hora Cadastro],  ");
            sSql.Append(" u.Nome[Usuário Responsável] ");
            sSql.Append(" from tIngressoEstacionamento es ");
            sSql.Append(" inner join tEdicao ed on es.IDEdicao = ed.IDEdicao ");
            sSql.Append(" inner join tEvento ev on ed.IDEvento = ev.IDEvento ");
            sSql.Append(" inner join tUsuario u on es.IDUsuarioCadastro = u.IDUsuario ");
            sSql.Append(" where es.IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" and Avulso = 1 ");
            sSql.Append(" and es.Ativo = 1 ");
            sSql.Append(" order by es.DataHoraCadastro desc ");

            return sSql.ToString();
        }

        internal string RetornarIngressosVoucher(int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from vVendaVoucher ");
            sSql.Append(" where IDVenda = " + iIDVenda);

            return sSql.ToString();
        }

        internal string RetornarIngresso(string sTicket)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from vVendaVoucher ");
            sSql.Append(" where Ticket = '" + sTicket + "'");

            return sSql.ToString();
        }

        internal string RetornarIngressoAvulso(string sIngresso, int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select *  ");
            sSql.Append(" from vIngressoAvulso  ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" and Ingresso = '" + sIngresso + "' ");

            return sSql.ToString();
        }

        internal string RetornarIngressoCadeira(string sIngresso, int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select *  ");
            sSql.Append(" from vIngressoCadeira  ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" and Ingresso = '" + sIngresso + "' ");

            return sSql.ToString();
        }

        internal string CancelarTicket(ingresso Ingresso)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" insert into tIngressoCancelamento ");
            sSql.Append(" (Ticket, IDVenda, IDTipoIngresso, Valor, Lote, Tipo, IDUsuarioCancelamento) ");
            sSql.Append(" select Ticket, IDVenda, IDTipoIngresso, Valor, Lote, TipoIngresso, " + Ingresso.IDUsuario + " ");
            sSql.Append(" from vVendaTicket vv ");
            sSql.Append(" where Ticket = " + Ingresso.Ticket + " ");

            sSql.Append(" delete tVendaAvulso ");
            sSql.Append(" where Ticket = " + Ingresso.Ticket + " ");

            sSql.Append(" update tVendaCadeira ");
            sSql.Append(" set IDStatusCadeira = 1,  ");
            sSql.Append(" IDVenda = null,  ");
            sSql.Append(" Valor = null,  ");
            sSql.Append(" IDTipoIngresso = null,  ");
            sSql.Append(" Lote = null,  ");
            sSql.Append(" DataHoraReserva = null  ");
            sSql.Append(" where Ticket = " + Ingresso.Ticket + " ");

            return sSql.ToString();
        }

        internal string CadastrarSolicitacaoIngresso(string sIDVenda, string sIP, string sEmail, string sIDTipoSolicitacao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" insert into tSolicitacaoIngresso ");
            sSql.Append(" ( IDVenda, IP, Email, IDTipoSolicitacao) ");
            sSql.Append(" values ");
            sSql.Append(" (" + sIDVenda + ", '" + sIP + "', '" + sEmail + "', " + sIDTipoSolicitacao + ") ");

            return sSql.ToString();
        }

        internal string CadastrarSolicitacaoIngressoConvidado(string sIDVenda, string sTickets, string sIP, string sEmail, string sIDTipoSolicitacao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" insert into tSolicitacaoIngresso ");
            sSql.Append(" ( IDVenda, Tickets, IP, Email, IDTipoSolicitacao) ");
            sSql.Append(" values ");
            sSql.Append(" (" + sIDVenda + ", '" + sTickets + "', '" + sIP + "', '" + sEmail + "', " + sIDTipoSolicitacao + ") ");

            return sSql.ToString();
        }

        internal string RetornarTicketMesa(string sIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select c.mesa,vv.IDVenda, vv.Ticket, vv.Nome, upper(vv.Evento + ' ' + vv.Edicao) Edicao ");
            sSql.Append(" from vVendaVoucher vv ");
            sSql.Append(" inner join tVendaCadeira vc on vv.Ticket = vc.Ticket ");
            sSql.Append(" inner join tCadeira c on vc.IDCadeira = c.IDCadeira ");
            sSql.Append(" where vv.IDTipoIngresso in (1,2,5,6,9,10,11,12) "); // inserido ,9,10,11,12 para a Boate
            sSql.Append("  and vv.IDVenda = " + sIDVenda + " ");

            return sSql.ToString();
        }

        internal string RetornarIngressosMesa(int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from vVendaVoucher ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");
            sSql.Append(" and IDTipoIngresso in (1,2,5,6,9,10,11,12) "); // inserido ,9,10,11,12 para a Boate

            return sSql.ToString();
        }

        internal string RetornarVendasConcluidas(int iIDCliente)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select v.IDVenda, ");
            sSql.Append(" CONVERT(varchar, v.IDVenda) + ' - ' + ev.Evento + ' ' + ed.Edicao + ' ' + ");
            sSql.Append(" CONVERT(varchar, v.DataHoraCompra, 103) + ' ' + CONVERT(varchar, v.DataHoraCompra, 108) as [Venda] ");
            sSql.Append(" from tVenda v ");
            sSql.Append(" inner join tEdicao ed on v.IDEdicao = ed.IDEdicao ");
            sSql.Append(" inner join tEvento ev on ed.IDEvento = ev.IDEvento ");
            sSql.Append(" inner join tCliente c on v.IDCliente = c.IDCliente ");
            sSql.Append(" where v.IDCliente = " + iIDCliente + " ");
            sSql.Append(" and v.IDStatusCompra in (2,3) ");
            sSql.Append(" and ed.IDStatusIngresso in (1) ");
            sSql.Append(" and (ed.DataHoraEvento + 1) > GETDATE() ");
            sSql.Append(" and c.NumeroCota not in (select Num_Cota from tSocio where Debito = 1) ");
            sSql.Append(" order by v.IDVenda desc ");

            return sSql.ToString();
        }

        internal string RetornarIngressosConvidado(int iIDVenda, string sTickets)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select vv.Nome, vv.IDCliente, vv.IDVenda, ");
            sSql.Append(" vv.CPF, vv.Ticket, vv.Valor,vv.Tipo, CONVERT(varchar, vv.Ticket) + ' ' + vv.Tipo as Ingresso, ");
            sSql.Append(" c.NumeroCota, c.DigitoCota, ");
            sSql.Append(" Convert(varchar,e.DataHoraEvento,103) as Data, Convert(varchar,e.DataHoraEvento,108) as Hora, ");
            sSql.Append(" case when (vv.Evento = 'Reveillon') then '~/images/rodape_Reveillon.png' ");
            sSql.Append(" when (vv.Evento = 'Boate') then '~/images/boate_pic.png' else '~/images/junina_logo2.png' end EventoImagem, ");
            sSql.Append(" le.Endereco EnderecoEvento, ");
            sSql.Append(" vv.IdentidadeEletronica, vv.Evento ");
            sSql.Append(" from vVendaVoucher vv");
            sSql.Append(" inner join tEdicao e on vv.IDEdicao = e.IDEdicao ");
            sSql.Append(" inner join tCliente c on c.IDCliente = vv.IDCliente");
            sSql.Append(" left join tLocalEvento le on e.IDLocal = le.IDLocal  ");
            sSql.Append(" where IDVenda = " + iIDVenda + " ");

            if (!String.IsNullOrEmpty(sTickets))
                sSql.Append(" and vv.Ticket in (" + sTickets + ") ");

            sSql.Append(" order by vv.Ticket ");

            return sSql.ToString();
        }

        internal string RetornarCadeirasAleatorio(int iIDEdicao, string sSetor)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select top 1 Mesa, Cadeira ");
            sSql.Append(" from tVendaCadeira vc ");
            sSql.Append(" inner join tCadeira c on vc.IDCadeira = c.IDCadeira ");
            sSql.Append(" where vc.IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" and Setor = '" + sSetor + "' ");
            sSql.Append(" order by NEWID() ");

            return sSql.ToString();
        }

        internal string RetornarComprasEmAndamento(int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select COUNT(IDVenda) from tVenda ");
            sSql.Append(" where IDStatusCompra = 1 ");
            sSql.Append(" and IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" and dateadd(minute, 5, DataHoraReserva) > Getdate() ");

            sSql.Append(" select ev.ComprasSimultaneas ");
            sSql.Append(" from tEvento ev ");
            sSql.Append(" inner join tEdicao ed on ed.IDEvento = ev.IDEvento ");
            sSql.Append(" where ed.IDEdicao = " + iIDEdicao + " ");

            return sSql.ToString();
        }

        internal string CadastrarIngressoEstacionamento(int iIDVenda, int iIDEdicao, int iAvulso, string sIdentidadeEletronica, string sNome, string sCPF, int iIDUsuarioCadastro)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" if not exists (select * from tIngressoEstacionamento where IdentidadeEletronica = '" + sIdentidadeEletronica + "' ) ");
            sSql.Append(" insert into tIngressoEstacionamento ");
            sSql.Append(" (IdentidadeEletronica, IDEdicao, IDVenda, Avulso, Nome, CPF, IDUsuarioCadastro) ");
            sSql.Append(" values ");
            sSql.Append(" ('" + sIdentidadeEletronica + "', " + iIDEdicao + ", " + iIDVenda + ", " + iAvulso + ", '" + sNome + "', '" + sCPF + "', " + iIDUsuarioCadastro + ") ");

            return sSql.ToString();
        }

        internal string ExcluirIngressoEstacionamento(int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tIngressoEstacionamento set Ativo = 0 where IDVenda = " + iIDVenda + " ");

            return sSql.ToString();
        }

        internal string RetornarPagamento(double dNumeroDocumento)
        {
            string sSql = " select * from tCartao where NumeroDocumento = " + dNumeroDocumento + " ";
            return sSql;
        }
        internal string RetornarPagamentoBysIDVenda(string dNumeroDocumento)
        {
            string sSql = " select * from vVenda where IDVenda = " + dNumeroDocumento + " ";
            return sSql;
        }

        internal string AtualizarPagamentoCartao(string sNumeroDocumento, string sPaymentId, string stid, string sAuthorizationCode, int iAprovada)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tCartao ");
            sSql.Append(" set PaymentId = '" + sPaymentId + "', ");
            sSql.Append(" tid = '" + stid + "', ");
            sSql.Append(" AuthorizationCode = '" + sAuthorizationCode + "', ");
            sSql.Append(" Aprovada = " + iAprovada + " ");
            sSql.Append(" where NumeroDocumento = '" + sNumeroDocumento + "' ");

            return sSql.ToString();
        }

        internal string ExcluirFila(int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" delete tFila where IDEdicao = " + iIDEdicao + " ");

            return sSql.ToString();
        }

        internal string CadastrarUsuarioFila(int iIDUsuario, int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" if not exists(select * from tFila where IDUsuario = " + iIDUsuario + " and IDEdicao = " + iIDEdicao + ") ");
            sSql.Append("     insert into tFila ");
            sSql.Append("     (IDUsuario, IDEdicao) ");
            sSql.Append("     values ");
            sSql.Append("     (" + iIDUsuario + ", " + iIDEdicao + ") ");

            return sSql.ToString();
        }

        internal string ExcluirUsuarioFila(int iIDUsuario)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" delete tFila where IDUsuario = " + iIDUsuario + " ");

            return sSql.ToString();
        }

        internal string RetornarPosicaoFila(int iIDUsuario, int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select Posicao ");
            sSql.Append(" from(   SELECT ROW_NUMBER() OVER(ORDER BY Cadastro) AS Posicao, IDUsuario ");
            sSql.Append("         FROM tFila ");
            sSql.Append("         where IDEdicao = " + iIDEdicao + ") Posicao ");
            sSql.Append(" where Posicao.IDUsuario = " + iIDUsuario + " ");

            return sSql.ToString();
        }

        internal string VerificarExistenciaVoucher(int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select IDVenda from vVendaVoucher where IDVenda = " + iIDVenda + " ");

            return sSql.ToString();
        }

        //Blacklist - Verifica se já existe mais de 4 vendas não aprovadas para o cliente (não sócio)
        internal string VerificarComprasNaoAprovadasCartao(int iIDUsuario, int iIDEdicao, DateTime dataLiberacao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select IDUsuario, count(*) ");
            sSql.Append(" from vVendaCartao ");
            sSql.Append(" where Aprovada = 0 ");
            sSql.Append(" and IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" and IDUsuario = " + iIDUsuario + " ");
            sSql.Append(" and IDPerfil = 4 ");
            sSql.Append(" and Cadastro > '" + dataLiberacao.ToString("yyyy/MM/dd HH:mm:ss") + "' ");
            sSql.Append(" group by IDUsuario ");
            sSql.Append(" having count(*) >= 5 ");

            return sSql.ToString();
        }

        //Blacklist - Verifica se já existe mais de 4 vendas aprovadas para o cliente (não sócio)
        internal string VerificarComprasAprovadasCartao(int iIDUsuario, int iIDEdicao, DateTime dataLiberacao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select IDUsuario, count(*) ");
            sSql.Append(" from vVendaCartao ");
            sSql.Append(" where Aprovada = 1 ");
            sSql.Append(" and IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" and IDUsuario = " + iIDUsuario + " ");
            sSql.Append(" and IDPerfil = 4 ");
            sSql.Append(" and Cadastro > '" + dataLiberacao.ToString("yyyy/MM/dd HH:mm:ss") + "' ");
            sSql.Append(" group by IDUsuario ");
            sSql.Append(" having count(*) >= 5 ");

            return sSql.ToString();
        }

        //Blacklist - O não sócio conseguirá fazer apenas 4 compras com o mesmo cartão de crédito, independente se é o mesmo usuário ou não
        internal string VerificarComprasAprovadasCartao(string sNumeroCartao, string sCodigoSegura, DateTime dataLiberacao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select [Número Cartão], CodigoSeguranca, count(*) ");
            sSql.Append(" from vVendaCartao ");
            sSql.Append(" where [Número Cartão] like '%" + sNumeroCartao + "%' and CodigoSeguranca = '" + sCodigoSegura + "' ");
            sSql.Append(" and Aprovada = 1 ");
            sSql.Append(" and Cadastro > '" + dataLiberacao.ToString("yyyy/MM/dd HH:mm:ss") + "' ");
            sSql.Append(" group by [Número Cartão], CodigoSeguranca ");
            sSql.Append(" having count(*) >= 5 ");

            return sSql.ToString();
        }

    }
}

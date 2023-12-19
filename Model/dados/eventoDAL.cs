using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.objetos;

namespace Model.dados
{
    public class eventoDAL
    {
        internal string CadastrarEdicao(evento Evento)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" INSERT INTO tEdicao ");
            sSql.Append(" (IDEvento, ");
            sSql.Append(" Edicao, ");
            sSql.Append(" DataHoraEvento, ");
            sSql.Append(" InicioVendaNaoSocio, ");
            sSql.Append(" NumeroIngressoExtraSocioCota, NumeroIngressoExtraSocioTitulo, ");
            sSql.Append(" NumeroIngressosValorNaoSocio, ");
            sSql.Append(" NumeroIngressosCadeira, ");
            sSql.Append(" NumeroIngressosCamarote, ");
            sSql.Append(" DataLimite4ParcelaCota, ");
            sSql.Append(" DataLimite3ParcelaCota, ");
            sSql.Append(" DataLimite2ParcelaCota, ");
            sSql.Append(" DataLimite1ParcelaCota, ");
            sSql.Append(" DataLimite5ParcelaCredito, ");
            sSql.Append(" DataLimite4ParcelaCredito, ");
            sSql.Append(" DataLimite3ParcelaCredito, ");
            sSql.Append(" DataLimite2ParcelaCredito, ");
            sSql.Append(" DataLimite1ParcelaCredito, ");
            sSql.Append(" VagaEstacionamento, ");
            sSql.Append(" NumIngressoEstacionamento, ");
            sSql.Append(" VagasEstacionamento, ");
            sSql.Append(" IngressosCadeira, ");
            sSql.Append(" Detalhes, ");
            sSql.Append(" DataRetirada, ");
            sSql.Append(" Ativo, ");
            sSql.Append(" IDStatusIngresso, ");
            sSql.Append(" IDUsuarioCadastro, ");
            sSql.Append(" MinimoAniversariante, PossuiMapa, IDLocal) ");
            sSql.Append(" VALUES ");
            sSql.Append(" (" + Evento.IDEvento + ", ");
            sSql.Append(" '" + Evento.Edicao + "', ");
            sSql.Append(" '" + Evento.DataHoraEvento + "', ");
            sSql.Append(" '" + Evento.InicioVendaNaoSocio + "', ");
            sSql.Append(" " + Evento.NumeroIngressoExtraSocioCota + ", ");
            sSql.Append(" " + Evento.NumeroIngressoExtraSocioTitulo + ", ");
            sSql.Append(" " + Evento.NumeroIngressosValorNaoSocio + ", ");
            sSql.Append(" " + Evento.NumeroIngressosCadeira + ", ");
            sSql.Append(" " + Evento.NumeroIngressosCamarote + ", ");
            sSql.Append(" '" + Evento.DataLimite4ParcelaCota + "', ");
            sSql.Append(" '" + Evento.DataLimite3ParcelaCota + "', ");
            sSql.Append(" '" + Evento.DataLimite2ParcelaCota + "', ");
            sSql.Append(" '" + Evento.DataLimite1ParcelaCota + "', ");
            sSql.Append(" '" + Evento.DataLimite5ParcelaCredito + "', ");
            sSql.Append(" '" + Evento.DataLimite4ParcelaCredito + "', ");
            sSql.Append(" '" + Evento.DataLimite3ParcelaCredito + "', ");
            sSql.Append(" '" + Evento.DataLimite2ParcelaCredito + "', ");
            sSql.Append(" '" + Evento.DataLimite1ParcelaCredito + "', ");
            sSql.Append(" " + Evento.VagaEstacionamento + ", ");
            sSql.Append(" " + Evento.NumIngressoEstacionamento + ", ");
            sSql.Append(" " + Evento.VagasEstacionamento + ", ");
            sSql.Append(" " + Evento.IngressosCadeira + ", ");
            sSql.Append(" '" + Evento.Detalhes + "', ");
            sSql.Append(" '" + Evento.DataRetirada + "', ");
            sSql.Append(" " + Evento.Ativo + ",  ");
            sSql.Append(" " + Evento.IDStatusIngresso + ", ");
            sSql.Append(" " + Evento.IDUsuarioCadastro + ", ");
            sSql.Append(" " + Evento.MinimoAniversariante + ", " + Evento.PossuiMapa + ", "+Evento.IDLocal+") "); 

            sSql.Append(" insert into tVendaCadeira ");
            sSql.Append(" (IDEdicao, IDCadeira) ");
            sSql.Append(" select @@IDENTITY, IDCadeira ");
            sSql.Append(" from tCadeira ");
            sSql.Append(" where IDEvento = " + Evento.IDEvento + " and Ativo = 1 ");

            return sSql.ToString();
        }

        internal string CadastrarLote(evento Evento)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" INSERT INTO tLote ");
            sSql.Append(" (Lote, ");
            sSql.Append(" IDEdicao, ");
            sSql.Append(" InicioVenda, ");
            sSql.Append(" FimVenda, ");
            sSql.Append(" ValorInteiraCadeiraSocio, ");
            sSql.Append(" ValorInteiraCadeiraNaoSocio, ");
            sSql.Append(" ValorInteiraAvulsoSocio, ");
            sSql.Append(" ValorInteiraAvulsoNaoSocio, ");
            sSql.Append(" ValorMeiaCadeiraSocio, ");
            sSql.Append(" ValorMeiaCadeiraNaoSocio, ");
            sSql.Append(" ValorMeiaAvulsoSocio, ");
            sSql.Append(" ValorMeiaAvulsoNaoSocio, ");
            sSql.Append(" InteiraCadeiraSocio, ");
            sSql.Append(" InteiraCadeiraNaoSocio, ");
            sSql.Append(" InteiraAvulsoSocio, ");
            sSql.Append(" InteiraAvulsoNaoSocio, ");
            sSql.Append(" MeiaCadeiraSocio, ");
            sSql.Append(" MeiaCadeiraNaoSocio, ");
            sSql.Append(" MeiaAvulsoSocio, ");
            sSql.Append(" MeiaAvulsoNaoSocio, ");
            sSql.Append(" IngressosAvulsos, ");
            sSql.Append(" ValorIpanema, ");
            sSql.Append(" ValorGolodromo, ");
            sSql.Append(" ValorPortinari, ");
            sSql.Append(" ValorPergula, ");
            sSql.Append(" ValorSalaoDeFestas, ");
            sSql.Append(" ValorAcademia, ");
            sSql.Append(" CamaroteSocio, ");
            sSql.Append(" CamaroteNaoSocio, ");
            sSql.Append(" ValorCamaroteSocio, ");
            sSql.Append(" ValorCamaroteNaoSocio, ");
            sSql.Append(" Ativo, ");
            sSql.Append(" IDUsuarioCadastro) ");
            sSql.Append(" VALUES ");
            sSql.Append(" (" + Evento.Lote + ", ");
            sSql.Append(" " + Evento.IDEdicao + ", ");
            sSql.Append(" '" + Evento.InicioVenda + "', ");
            sSql.Append(" '" + Evento.FimVenda + "', ");
            sSql.Append(" '" + Evento.ValorInteiraCadeiraSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" '" + Evento.ValorInteiraCadeiraNaoSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" '" + Evento.ValorInteiraAvulsoSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" '" + Evento.ValorInteiraAvulsoNaoSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" '" + Evento.ValorMeiaCadeiraSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" '" + Evento.ValorMeiaCadeiraNaoSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" '" + Evento.ValorMeiaAvulsoSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" '" + Evento.ValorMeiaAvulsoNaoSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" " + Evento.InteiraCadeiraSocio + ", ");
            sSql.Append(" " + Evento.InteiraCadeiraNaoSocio + ", ");
            sSql.Append(" " + Evento.InteiraAvulsoSocio + ", ");
            sSql.Append(" " + Evento.InteiraAvulsoNaoSocio + ", ");
            sSql.Append(" " + Evento.MeiaCadeiraSocio + ", ");
            sSql.Append(" " + Evento.MeiaCadeiraNaoSocio + ", ");
            sSql.Append(" " + Evento.MeiaAvulsoSocio + ", ");
            sSql.Append(" " + Evento.MeiaAvulsoNaoSocio + ", ");
            sSql.Append(" " + Evento.IngressosAvulsos + ", ");
            sSql.Append(" '" + Evento.ValorIpanema.ToString().Replace(",", ".") + "', ");
            sSql.Append(" '" + Evento.ValorGolodromo.ToString().Replace(",", ".") + "', ");
            sSql.Append(" '" + Evento.ValorPortinari.ToString().Replace(",", ".") + "', ");
            sSql.Append(" '" + Evento.ValorPergula.ToString().Replace(",", ".") + "', ");
            sSql.Append(" '" + Evento.ValorSalaoDeFestas.ToString().Replace(",", ".") + "', ");
            sSql.Append(" '" + Evento.ValorAcademia.ToString().Replace(",", ".") + "', ");
            sSql.Append(" " + Evento.CamaroteSocio + ", ");
            sSql.Append(" " + Evento.CamaroteNaoSocio + ", ");
            sSql.Append(" '" + Evento.ValorCamaroteSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" '" + Evento.ValorCamaroteNaoSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" " + Evento.Ativo + ", ");
            sSql.Append(" " + Evento.IDUsuarioCadastro + ") ");

            return sSql.ToString();
        }

        internal string AtualizarEdicao(evento Evento)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" UPDATE tEdicao ");
            sSql.Append(" SET IDEvento = " + Evento.IDEvento + ", ");
            sSql.Append(" Edicao = '" + Evento.Edicao + "', ");
            sSql.Append(" DataHoraEvento = '" + Evento.DataHoraEvento + "', ");
            sSql.Append(" InicioVendaNaoSocio = '" + Evento.InicioVendaNaoSocio + "', ");
            sSql.Append(" NumeroIngressoExtraSocioCota = " + Evento.NumeroIngressoExtraSocioCota + ", ");
            sSql.Append(" NumeroIngressoExtraSocioTitulo = " + Evento.NumeroIngressoExtraSocioTitulo + ", ");
            sSql.Append(" NumeroIngressosValorNaoSocio = " + Evento.NumeroIngressosValorNaoSocio + ", ");
            sSql.Append(" NumeroIngressosCadeira = " + Evento.NumeroIngressosCadeira + ", ");
            sSql.Append(" NumeroIngressosCamarote = " + Evento.NumeroIngressosCamarote + ", ");
            sSql.Append(" DataLimite4ParcelaCota = '" + Evento.DataLimite4ParcelaCota + "', ");
            sSql.Append(" DataLimite3ParcelaCota = '" + Evento.DataLimite3ParcelaCota + "', ");
            sSql.Append(" DataLimite2ParcelaCota = '" + Evento.DataLimite2ParcelaCota + "', ");
            sSql.Append(" DataLimite1ParcelaCota = '" + Evento.DataLimite1ParcelaCota + "', ");
            sSql.Append(" DataLimite5ParcelaCredito = '" + Evento.DataLimite5ParcelaCredito + "', ");
            sSql.Append(" DataLimite4ParcelaCredito = '" + Evento.DataLimite4ParcelaCredito + "', ");
            sSql.Append(" DataLimite3ParcelaCredito = '" + Evento.DataLimite3ParcelaCredito + "', ");
            sSql.Append(" DataLimite2ParcelaCredito = '" + Evento.DataLimite2ParcelaCredito + "', ");
            sSql.Append(" DataLimite1ParcelaCredito = '" + Evento.DataLimite1ParcelaCredito + "', ");
            sSql.Append(" VagaEstacionamento = " + Evento.VagaEstacionamento + ", ");
            sSql.Append(" NumIngressoEstacionamento = " + Evento.NumIngressoEstacionamento + ", ");
            sSql.Append(" VagasEstacionamento = " + Evento.VagasEstacionamento + ", ");
            sSql.Append(" IngressosCadeira = " + Evento.IngressosCadeira + ", ");
            sSql.Append(" PossuiMapa = " + Evento.PossuiMapa + ", ");
            sSql.Append(" Detalhes = '" + Evento.Detalhes + "', ");
            sSql.Append(" DataRetirada = '" + Evento.DataRetirada + "', ");
            sSql.Append(" Ativo = " + Evento.Ativo + ", ");
            sSql.Append(" IDStatusIngresso = " + Evento.IDStatusIngresso + ", ");
            sSql.Append(" IDLocal = " + Evento.IDLocal + ", ");
            sSql.Append(" Alteracao = getdate(), ");
            sSql.Append(" IDUsuarioAlteracao = " + Evento.IDUsuarioAlteracao + ", ");
            sSql.Append(" MinimoAniversariante = " + Evento.MinimoAniversariante + " ");
            sSql.Append(" WHERE IDEdicao = " + Evento.IDEdicao + " ");

            return sSql.ToString();
        }

        internal string AtualizarLote(evento Evento)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" UPDATE tLote ");
            sSql.Append(" SET Lote = " + Evento.Lote + ", ");
            sSql.Append(" InicioVenda = '" + Evento.InicioVenda + "', ");
            sSql.Append(" FimVenda = '" + Evento.FimVenda + "', ");
            sSql.Append(" ValorInteiraCadeiraSocio = '" + Evento.ValorInteiraCadeiraSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" ValorInteiraCadeiraNaoSocio = '" + Evento.ValorInteiraCadeiraNaoSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" ValorInteiraAvulsoSocio = '" + Evento.ValorInteiraAvulsoSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" ValorInteiraAvulsoNaoSocio = '" + Evento.ValorInteiraAvulsoNaoSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" ValorMeiaCadeiraSocio = '" + Evento.ValorMeiaCadeiraSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" ValorMeiaCadeiraNaoSocio = '" + Evento.ValorMeiaCadeiraNaoSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" ValorMeiaAvulsoSocio = '" + Evento.ValorMeiaAvulsoSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" ValorMeiaAvulsoNaoSocio = '" + Evento.ValorMeiaAvulsoNaoSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" InteiraCadeiraSocio = " + Evento.InteiraCadeiraSocio + ", ");
            sSql.Append(" InteiraCadeiraNaoSocio = " + Evento.InteiraCadeiraNaoSocio + ", ");
            sSql.Append(" InteiraAvulsoSocio = " + Evento.InteiraAvulsoSocio + ", ");
            sSql.Append(" InteiraAvulsoNaoSocio = " + Evento.InteiraAvulsoNaoSocio + ", ");
            sSql.Append(" MeiaCadeiraSocio = " + Evento.MeiaCadeiraSocio + ", ");
            sSql.Append(" MeiaCadeiraNaoSocio = " + Evento.MeiaCadeiraNaoSocio + ", ");
            sSql.Append(" MeiaAvulsoSocio = " + Evento.MeiaAvulsoSocio + ", ");
            sSql.Append(" MeiaAvulsoNaoSocio = " + Evento.MeiaAvulsoNaoSocio + ", ");
            sSql.Append(" IngressosAvulsos = " + Evento.IngressosAvulsos + ", ");
            sSql.Append(" Ativo = " + Evento.Ativo + ", ");
            sSql.Append(" Alteracao = getdate(), ");
            sSql.Append(" IDUsuarioAlteracao = " + Evento.IDUsuarioAlteracao + ", ");
            sSql.Append(" ValorIpanema = '" + Evento.ValorIpanema.ToString().Replace(",", ".") + "', ");
            sSql.Append(" ValorGolodromo = '" + Evento.ValorGolodromo.ToString().Replace(",", ".") + "', ");
            sSql.Append(" ValorPortinari = '" + Evento.ValorPortinari.ToString().Replace(",", ".") + "', ");
            sSql.Append(" ValorPergula = '" + Evento.ValorPergula.ToString().Replace(",", ".") + "', ");
            sSql.Append(" ValorSalaoDeFestas = '" + Evento.ValorSalaoDeFestas.ToString().Replace(",", ".") + "', ");
            sSql.Append(" ValorAcademia = '" + Evento.ValorAcademia.ToString().Replace(",", ".") + "', ");
            sSql.Append(" CamaroteSocio = " + Evento.CamaroteSocio + ", ");
            sSql.Append(" CamaroteNaoSocio = " + Evento.CamaroteNaoSocio + ", ");
            sSql.Append(" ValorCamaroteSocio = '" + Evento.ValorCamaroteSocio.ToString().Replace(",", ".") + "', ");
            sSql.Append(" ValorCamaroteNaoSocio = '" + Evento.ValorCamaroteNaoSocio.ToString().Replace(",", ".") + "' ");
            sSql.Append(" WHERE IDLote = " + Evento.IDLote + " ");

            return sSql.ToString();
        }

        internal string RetornarEdicao(int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" SELECT IDEdicao,  ");
            sSql.Append(" edi.IDEvento,  ");
            sSql.Append(" edi.Edicao, e.Evento, ");
            sSql.Append(" edi.DataHoraEvento, edi.InicioVendaNaoSocio, ");
            sSql.Append(" edi.MapaEvento,  ");
            sSql.Append(" edi.NumeroIngressoExtraSocioCota, edi.NumeroIngressoExtraSocioTitulo, ");
            sSql.Append(" edi.NumeroIngressosValorNaoSocio,  edi.NumeroIngressosCadeira, edi.NumeroIngressosCamarote, ");
            sSql.Append(" convert(varchar, edi.DataLimite4ParcelaCota, 103) DataLimite4ParcelaCota, ");
            sSql.Append(" convert(varchar, edi.DataLimite3ParcelaCota, 103) DataLimite3ParcelaCota, ");
            sSql.Append(" convert(varchar, edi.DataLimite2ParcelaCota, 103) DataLimite2ParcelaCota, ");
            sSql.Append(" convert(varchar, edi.DataLimite1ParcelaCota, 103) DataLimite1ParcelaCota, ");
            sSql.Append(" convert(varchar, edi.DataLimite5ParcelaCredito, 103) DataLimite5ParcelaCredito, ");
            sSql.Append(" convert(varchar, edi.DataLimite4ParcelaCredito, 103) DataLimite4ParcelaCredito, ");
            sSql.Append(" convert(varchar, edi.DataLimite3ParcelaCredito, 103) DataLimite3ParcelaCredito, ");
            sSql.Append(" convert(varchar, edi.DataLimite2ParcelaCredito, 103) DataLimite2ParcelaCredito, ");
            sSql.Append(" convert(varchar, edi.DataLimite1ParcelaCredito, 103) DataLimite1ParcelaCredito, ");
            sSql.Append(" edi.VagaEstacionamento,  ");
            sSql.Append(" case when (edi.VagaEstacionamento = 1) then 'Sim' else 'Não' end VagaEstacionamento_descricao,  ");
            sSql.Append(" edi.NumIngressoEstacionamento,  ");
            sSql.Append(" edi.VagasEstacionamento, edi.Detalhes, edi.DataRetirada, ");
            sSql.Append(" edi.Cadastro,  ");
            sSql.Append(" edi.IDUsuarioCadastro, edi.IDLocal, ");
            sSql.Append(" edi.Alteracao,  ");
            sSql.Append(" edi.IDUsuarioAlteracao, edi.Ativo, edi.IDStatusIngresso, ");
            sSql.Append(" edi.IngressosCadeira, edi.MinimoAniversariante, PossuiMapa ");
            sSql.Append(" FROM tEdicao edi  ");
            sSql.Append(" inner join tEvento e on edi.IDEvento = e.IDEvento  ");
            sSql.Append(" where edi.IDEdicao = " + iIDEdicao + " ");

            return sSql.ToString();
        }

        internal string RetornarEdicao(int iIDEvento, string sEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" SELECT IDEdicao,  ");
            sSql.Append(" edi.IDEvento,  ");
            sSql.Append(" edi.Edicao, e.Evento, ");
            sSql.Append(" edi.DataHoraEvento, edi.InicioVendaNaoSocio, ");
            sSql.Append(" edi.MapaEvento,  ");
            sSql.Append(" edi.NumeroIngressoExtraSocioCota, edi.NumeroIngressoExtraSocioTitulo, ");
            sSql.Append(" edi.NumeroIngressosValorNaoSocio,  edi.NumeroIngressosCadeira, ");
            sSql.Append(" convert(varchar, edi.DataLimite4ParcelaCota, 103) DataLimite4ParcelaCota, ");
            sSql.Append(" convert(varchar, edi.DataLimite3ParcelaCota, 103) DataLimite3ParcelaCota, ");
            sSql.Append(" convert(varchar, edi.DataLimite2ParcelaCota, 103) DataLimite2ParcelaCota, ");
            sSql.Append(" convert(varchar, edi.DataLimite1ParcelaCota, 103) DataLimite1ParcelaCota, ");
            sSql.Append(" convert(varchar, edi.DataLimite5ParcelaCredito, 103) DataLimite5ParcelaCredito, ");
            sSql.Append(" convert(varchar, edi.DataLimite4ParcelaCredito, 103) DataLimite4ParcelaCredito, ");
            sSql.Append(" convert(varchar, edi.DataLimite3ParcelaCredito, 103) DataLimite3ParcelaCredito, ");
            sSql.Append(" convert(varchar, edi.DataLimite2ParcelaCredito, 103) DataLimite2ParcelaCredito, ");
            sSql.Append(" convert(varchar, edi.DataLimite1ParcelaCredito, 103) DataLimite1ParcelaCredito, ");
            sSql.Append(" edi.VagaEstacionamento,  ");
            sSql.Append(" case when (edi.VagaEstacionamento = 1) then 'Sim' else 'Não' end VagaEstacionamento_descricao,  ");
            sSql.Append(" edi.NumIngressoEstacionamento,  ");
            sSql.Append(" edi.VagasEstacionamento, edi.Detalhes, edi.DataRetirada, ");
            sSql.Append(" edi.Cadastro,  ");
            sSql.Append(" edi.IDUsuarioCadastro,  ");
            sSql.Append(" edi.Alteracao, edi.IDLocal, ");
            sSql.Append(" edi.IDUsuarioAlteracao, edi.Ativo, ");
            sSql.Append(" edi.IngressosCadeira, edi.IDStatusIngresso ");
            sSql.Append(" FROM tEdicao edi  ");
            sSql.Append(" inner join tEvento e on edi.IDEvento = e.IDEvento  ");
            sSql.Append(" where edi.Edicao = '" + sEdicao + "' ");
            sSql.Append(" and edi.IDEvento = " + iIDEvento + " ");

            return sSql.ToString();
        }

        internal string RetornarLote(int iIDLote)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" SELECT l.IDLote, l.Lote, l.IDEdicao, ");
            sSql.Append(" edi.IDEvento, ");
            sSql.Append(" edi.Edicao,  ");
            sSql.Append(" edi.DataHoraEvento, ");
            sSql.Append(" l.InicioVenda,  ");
            sSql.Append(" l.FimVenda,  ");
            sSql.Append(" convert(varchar, l.InicioVenda, 103) DataInicioVenda, ");
            sSql.Append(" convert(varchar, l.FimVenda, 103) DataFimVenda, ");
            sSql.Append(" convert(varchar(5), l.InicioVenda, 108) HoraInicioVenda, ");
            sSql.Append(" convert(varchar(5), l.FimVenda, 108) HoraFimVenda, ");
            sSql.Append(" edi.MapaEvento,  ");
            sSql.Append(" edi.NumeroIngressoExtraSocioCota, edi.NumeroIngressoExtraSocioTitulo, ");
            sSql.Append(" edi.NumeroIngressosValorNaoSocio, edi.NumeroIngressosCadeira, edi.NumeroIngressosCamarote, ");
            sSql.Append(" l.ValorInteiraCadeiraSocio, ");
            sSql.Append(" l.ValorInteiraCadeiraNaoSocio, ");
            sSql.Append(" l.ValorInteiraAvulsoSocio, ");
            sSql.Append(" l.ValorInteiraAvulsoNaoSocio, ");
            sSql.Append(" l.ValorMeiaCadeiraSocio,  ");
            sSql.Append(" l.ValorMeiaCadeiraNaoSocio, ");
            sSql.Append(" l.ValorMeiaAvulsoSocio,  ");
            sSql.Append(" l.ValorMeiaAvulsoNaoSocio, ");
            sSql.Append(" l.ValorSocioMasculino, l.ValorSocioFeminino, l.ValorNaoSocioMasculino, l.ValorNaoSocioFeminino, ");
            sSql.Append(" l.InteiraCadeiraSocio, ");
            sSql.Append(" l.InteiraCadeiraNaoSocio,  ");
            sSql.Append(" l.InteiraAvulsoSocio,  ");
            sSql.Append(" l.InteiraAvulsoNaoSocio, ");
            sSql.Append(" l.MeiaCadeiraSocio, ");
            sSql.Append(" l.MeiaCadeiraNaoSocio, ");
            sSql.Append(" l.MeiaAvulsoSocio,  ");
            sSql.Append(" l.MeiaAvulsoNaoSocio, ");
            sSql.Append(" l.CamaroteSocio, ");
            sSql.Append(" l.CamaroteNaoSocio, ");
            sSql.Append(" l.ValorCamaroteSocio,  ");
            sSql.Append(" l.ValorCamaroteNaoSocio,  ");
            sSql.Append(" l.SocioMasculino, l.SocioFeminino, l.NaoSocioMasculino, l.NaoSocioFeminino, ");
            sSql.Append(" convert(varchar, edi.DataLimite4ParcelaCota, 103) DataLimite4ParcelaCota, ");
            sSql.Append(" convert(varchar, edi.DataLimite3ParcelaCota, 103) DataLimite3ParcelaCota, ");
            sSql.Append(" convert(varchar, edi.DataLimite2ParcelaCota, 103) DataLimite2ParcelaCota, ");
            sSql.Append(" convert(varchar, edi.DataLimite1ParcelaCota, 103) DataLimite1ParcelaCota, ");
            sSql.Append(" convert(varchar, edi.DataLimite5ParcelaCredito, 103) DataLimite5ParcelaCredito, ");
            sSql.Append(" convert(varchar, edi.DataLimite4ParcelaCredito, 103) DataLimite4ParcelaCredito, ");
            sSql.Append(" convert(varchar, edi.DataLimite3ParcelaCredito, 103) DataLimite3ParcelaCredito, ");
            sSql.Append(" convert(varchar, edi.DataLimite2ParcelaCredito, 103) DataLimite2ParcelaCredito, ");
            sSql.Append(" convert(varchar, edi.DataLimite1ParcelaCredito, 103) DataLimite1ParcelaCredito, ");
            sSql.Append(" edi.VagaEstacionamento,  ");
            sSql.Append(" case when (edi.VagaEstacionamento = 1) then 'Sim' else 'Não' end VagaEstacionamento_descricao, ");
            sSql.Append(" edi.NumIngressoEstacionamento, ");
            sSql.Append(" edi.VagasEstacionamento, edi.Detalhes, edi.DataRetirada, ");
            sSql.Append(" l.Cadastro,  ");
            sSql.Append(" l.IDUsuarioCadastro,  ");
            sSql.Append(" l.Alteracao,  ");
            sSql.Append(" l.IDUsuarioAlteracao, l.Ativo, ");
            sSql.Append(" l.ValorIpanema, l.ValorGolodromo, l.ValorPortinari, l.ValorPergula, l.ValorSalaoDeFestas, l.ValorAcademia, ");
            sSql.Append(" edi.IngressosCadeira, l.IngressosAvulsos  ");
            sSql.Append(" FROM tLote l ");
            sSql.Append(" inner join tEdicao edi on edi.IDEdicao = l.IDEdicao ");
            sSql.Append(" inner join tEvento e on edi.IDEvento = e.IDEvento  ");
            sSql.Append(" where l.IDLote = " + iIDLote + " ");

            return sSql.ToString();
        }

        internal string RetornarEventos()
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select edi.IDEdicao, ev.Evento, ");
            sSql.Append(" Edicao, CONVERT(varchar, edi.DataHoraEvento, 103) + ' ' + CONVERT(varchar, edi.DataHoraEvento, 108) [Data/Hora Evento], ");
            sSql.Append(" NumeroIngressoExtraSocioCota, NumeroIngressoExtraSocioTitulo, NumeroIngressosValorNaoSocio, ");
            sSql.Append(" case when (VagaEstacionamento = 1) then 'Sim' else 'Não' end VagaEstacionamento, VagasEstacionamento, case when (edi.Ativo = 1) then 'Sim' else 'Não' end Ativo, ");
            sSql.Append(" NumIngressoEstacionamento, IngressosCadeira, ");
            sSql.Append(" case when (PossuiMapa = 1) then 'Sim' else 'Não' end PossuiMapa, ");
            sSql.Append(" NumeroIngressosCadeira, DataRetirada, case when (edi.IDStatusIngresso = 1) then 'Sim' else 'Não' end StatusIngresso ");
            sSql.Append(" from tEdicao edi ");
            sSql.Append(" inner join tEvento ev on edi.IDEvento = ev.IDEvento ");
            sSql.Append(" order by edi.DataHoraEvento desc ");

            return sSql.ToString();
        }

        internal string RetornarLotes(int iIDEdicao, bool bApenasLoteInicioFimVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select l.IDLote, l.Lote, edi.IDEdicao, ev.Evento, ");
            sSql.Append(" Edicao, CONVERT(varchar, edi.DataHoraEvento, 103) + ' ' + CONVERT(varchar, edi.DataHoraEvento, 108) [Data/Hora Evento], ");
            sSql.Append(" CONVERT(varchar, l.InicioVenda, 103) + ' ' + CONVERT(varchar, l.InicioVenda, 108) [Início Venda], ");
            sSql.Append(" CONVERT(varchar, l.FimVenda, 103) + ' ' + CONVERT(varchar, l.FimVenda, 108) [Fim Venda], ");
            sSql.Append(" NumeroIngressoExtraSocioCota, NumeroIngressoExtraSocioTitulo, NumeroIngressosValorNaoSocio, ValorInteiraCadeiraSocio, ValorInteiraCadeiraNaoSocio, ");
            sSql.Append(" ValorInteiraAvulsoSocio, ValorInteiraAvulsoNaoSocio, ");
            sSql.Append(" ValorMeiaCadeiraSocio, ValorMeiaCadeiraNaoSocio, ValorMeiaAvulsoSocio, ValorMeiaAvulsoNaoSocio, ");
            sSql.Append(" l.ValorSocioMasculino, l.ValorSocioFeminino, l.ValorNaoSocioMasculino, l.ValorNaoSocioFeminino, ");
            sSql.Append(" l.InteiraCadeiraSocio, ");
            sSql.Append(" l.InteiraCadeiraNaoSocio, ");
            sSql.Append(" l.InteiraAvulsoSocio, ");
            sSql.Append(" l.InteiraAvulsoNaoSocio, ");
            sSql.Append(" l.MeiaCadeiraSocio, ");
            sSql.Append(" l.MeiaCadeiraNaoSocio, ");
            sSql.Append(" l.MeiaAvulsoSocio, ");
            sSql.Append(" l.MeiaAvulsoNaoSocio, ");
            sSql.Append(" l.CamaroteSocio, ");
            sSql.Append(" l.CamaroteNaoSocio, ");
            sSql.Append(" l.ValorCamaroteSocio,  ");
            sSql.Append(" l.ValorCamaroteNaoSocio,  ");
            sSql.Append(" l.ValorIpanema, l.ValorGolodromo, l.ValorPortinari, l.ValorPergula, l.ValorSalaoDeFestas, l.ValorAcademia, ");
            sSql.Append(" l.SocioMasculino, l.SocioFeminino, l.NaoSocioMasculino, l.NaoSocioFeminino, ");
            sSql.Append(" case when (VagaEstacionamento = 1) then 'Sim' else 'Não' end VagaEstacionamento, VagasEstacionamento, case when (l.Ativo = 1) then 'Sim' else 'Não' end Ativo, ");
            sSql.Append(" NumIngressoEstacionamento, IngressosCadeira, ");
            sSql.Append(" IngressosAvulsos ");
            sSql.Append(" from tLote l ");
            sSql.Append(" inner join tEdicao edi on edi.IDEdicao = l.IDEdicao ");
            sSql.Append(" inner join tEvento ev on edi.IDEvento = ev.IDEvento ");
            sSql.Append(" where l.IDEdicao = " + iIDEdicao + " ");
            if (bApenasLoteInicioFimVenda) sSql.Append(" and getdate() between l.InicioVenda and l.FimVenda ");
            sSql.Append(" order by l.Lote ");

            return sSql.ToString();
        }

        /*internal string RetornarLoteDisponivelParaVenda(int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select top 1 l.IDLote, l.Lote, edi.IDEdicao, ev.Evento, ");
            sSql.Append(" Edicao, CONVERT(varchar, edi.DataHoraEvento, 103) + ' ' + CONVERT(varchar, edi.DataHoraEvento, 108) [Data/Hora Evento], ");
            sSql.Append(" CONVERT(varchar, l.InicioVenda, 103) + ' ' + CONVERT(varchar, l.InicioVenda, 108) [Início Venda], ");
            sSql.Append(" CONVERT(varchar, l.FimVenda, 103) + ' ' + CONVERT(varchar, l.FimVenda, 108) [Fim Venda], ");
            sSql.Append(" NumeroIngressoExtraSocioCota, NumeroIngressoExtraSocioTitulo, NumeroIngressosValorNaoSocio, ValorInteiraCadeiraSocio, ValorInteiraCadeiraNaoSocio, ");
            sSql.Append(" ValorInteiraAvulsoSocio, ValorInteiraAvulsoNaoSocio, ");
            sSql.Append(" ValorMeiaCadeiraSocio, ValorMeiaCadeiraNaoSocio, ValorMeiaAvulsoSocio, ValorMeiaAvulsoNaoSocio, ");
            sSql.Append(" l.ValorSocioMasculino, l.ValorSocioFeminino, l.ValorNaoSocioMasculino, l.ValorNaoSocioFeminino, ");
            sSql.Append(" l.InteiraCadeiraSocio, ");
            sSql.Append(" l.InteiraCadeiraNaoSocio, ");
            sSql.Append(" l.InteiraAvulsoSocio, ");
            sSql.Append(" l.InteiraAvulsoNaoSocio, ");
            sSql.Append(" l.MeiaCadeiraSocio, ");
            sSql.Append(" l.MeiaCadeiraNaoSocio, ");
            sSql.Append(" l.MeiaAvulsoSocio, ");
            sSql.Append(" l.MeiaAvulsoNaoSocio, ");
            sSql.Append(" l.CamaroteSocio, ");
            sSql.Append(" l.CamaroteNaoSocio, ");
            sSql.Append(" l.ValorCamaroteSocio,  ");
            sSql.Append(" l.ValorCamaroteNaoSocio,  ");
            sSql.Append(" l.ValorIpanema, l.ValorGolodromo, l.ValorPortinari, l.ValorPergula, l.ValorSalaoDeFestas, l.ValorAcademia, ");
            sSql.Append(" l.SocioMasculino, l.SocioFeminino, l.NaoSocioMasculino, l.NaoSocioFeminino, ");
            sSql.Append(" case when (VagaEstacionamento = 1) then 'Sim' else 'Não' end VagaEstacionamento, VagasEstacionamento, case when (l.Ativo = 1) then 'Sim' else 'Não' end Ativo, ");
            sSql.Append(" NumIngressoEstacionamento, IngressosCadeira, ");
            sSql.Append(" IngressosAvulsos ");
            sSql.Append(" from tLote l ");
            sSql.Append(" inner join tEdicao edi on edi.IDEdicao = l.IDEdicao ");
            sSql.Append(" inner join tEvento ev on edi.IDEvento = ev.IDEvento ");
            sSql.Append(" where l.IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" and getdate() between l.InicioVenda and l.FimVenda ");
            sSql.Append(" order by l.Lote ");

            return sSql.ToString();
        }*/

        internal string RetornarEventosVenda()
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select ev.IDEdicao, e.Evento + ' - edição ' + ev.Edicao + ' - ' + ");
            sSql.Append(" convert(varchar, DataHoraEvento, 103) + ' ' + convert(varchar, DataHoraEvento, 108)  as Evento, ");
            sSql.Append(" e.Evento + ' - ' + ev.Edicao EventoEdicao ");
            sSql.Append(" from tEdicao ev ");
            sSql.Append(" inner join tEvento e on ev.IDEvento = e.IDEvento ");
            sSql.Append(" where ev.Ativo = 1 ");
            sSql.Append(" and ev.IDEdicao in (select IDEdicao ");
            sSql.Append(" 		from tLote ");
            sSql.Append(" 		where GETDATE() between InicioVenda and FimVenda ");
            sSql.Append(" 		and Ativo = 1)  ");
            sSql.Append(" order by ev.IDEdicao desc ");

            return sSql.ToString();
        }

        internal string RetornarEventosVenda(string sPerfil)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select ev.IDEdicao, e.Evento + ' - edição ' + ev.Edicao + ' - ' + ");
            sSql.Append(" convert(varchar, DataHoraEvento, 103) + ' ' + convert(varchar, DataHoraEvento, 108)  as Evento, ");
            sSql.Append(" e.Evento + ' - ' + ev.Edicao EventoEdicao, ev.InicioVendaNaoSocio ");
            sSql.Append(" from tEdicao ev ");
            sSql.Append(" inner join tEvento e on ev.IDEvento = e.IDEvento ");
            sSql.Append(" where ev.Ativo = 1 ");
            sSql.Append(" and ev.IDEdicao in (select IDEdicao ");
            sSql.Append(" 		from tLote ");
            sSql.Append(" 		where GETDATE() between InicioVenda and FimVenda ");
            sSql.Append(" 		and Ativo = 1)  ");
            if (sPerfil == "Não Sócio") sSql.Append(" and ev.InicioVendaNaoSocio < GETDATE() ");
            sSql.Append(" order by ev.IDEdicao desc ");

            return sSql.ToString();
        }

        internal string RetornarEventosVendaAdministrador()
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select ev.IDEdicao, e.Evento + ' - edição ' + ev.Edicao + ' - ' + ");
            sSql.Append(" convert(varchar, DataHoraEvento, 103) + ' ' + convert(varchar, DataHoraEvento, 108) ");
            sSql.Append(" + case when (ev.Ativo = 0) then ' *****Inativo' else '' end as Evento, ");
            sSql.Append(" e.Evento + ' - ' + ev.Edicao EventoEdicao ");
            sSql.Append(" from tEdicao ev ");
            sSql.Append(" inner join tEvento e on ev.IDEvento = e.IDEvento ");
            sSql.Append(" order by ev.IDEdicao desc ");

            return sSql.ToString();
        }

        internal string RetornarEventosEdicaoVenda()
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select ev.IDEdicao, e.Evento + ' - edição ' + ev.Edicao + ' - ' + ");
            sSql.Append(" convert(varchar, DataHoraEvento, 103) + ' ' + convert(varchar, DataHoraEvento, 108)  as Evento, ");
            sSql.Append(" e.Evento + ' - ' + ev.Edicao EventoEdicao ");
            sSql.Append(" from tEdicao ev ");
            sSql.Append(" inner join tEvento e on ev.IDEvento = e.IDEvento ");
            sSql.Append(" where ev.Ativo = 1 ");
            sSql.Append(" and ev.IDEdicao in (select IDEdicao ");
            sSql.Append(" 		from tLote ");
            sSql.Append(" 		where GETDATE() between InicioVenda and FimVenda ");
            sSql.Append(" 		and Ativo = 1)  ");
            sSql.Append(" order by ev.IDEdicao desc ");

            return sSql.ToString();
        }

        internal string RetornarEventosEdicao()
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select ev.IDEdicao, e.Evento + ' - edição ' + ev.Edicao + ' - ' + ");
            sSql.Append(" convert(varchar, DataHoraEvento, 103) + ' ' + convert(varchar, DataHoraEvento, 108)  as Evento, ");
            sSql.Append(" e.Evento + ' - ' + ev.Edicao EventoEdicao ");
            sSql.Append(" from tEdicao ev ");
            sSql.Append(" inner join tEvento e on ev.IDEvento = e.IDEvento ");
            sSql.Append(" where (ev.DataHoraEvento + 60) > GETDATE() ");
            sSql.Append(" order by ev.IDEdicao desc ");

            return sSql.ToString();
        }

        internal string CadastrarLogEdicao(int iIDEdicao, string sTextoLog, int iIDUsuarioCadastro)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" insert into tLogEdicao ");
            sSql.Append(" (IDEdicao, TextoLog, IDUsuarioCadastro) ");
            sSql.Append(" values ");
            sSql.Append(" (" + iIDEdicao + ", '" + sTextoLog + "', " + iIDUsuarioCadastro + ") ");

            return sSql.ToString();
        }

        internal string RetornarLogEdicao(int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select l.IDLog, l.TextoLog, u.Nome [Usuário], ");
            sSql.Append(" CONVERT(varchar, l.Cadastro, 103) + ' ' + CONVERT(varchar, l.Cadastro, 108) [Data/Hora Alteração]  ");
            sSql.Append(" from tLogEdicao l ");
            sSql.Append(" inner join tUsuario u on u.IDUsuario = l.IDUsuarioCadastro ");
            sSql.Append(" where l.IDEdicao = " + iIDEdicao + " ");

            return sSql.ToString();
        }

        internal string VerificarExistenciaLote(int iIDLote, int iIDEdicao, int iLote)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from tLote ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" and Lote = " + iLote + " ");

            if (iIDLote != -1 && iIDLote != 0)
                sSql.Append(" and IDLote != " + iIDLote + " ");

            return sSql.ToString();
        }

        internal string VerificarExistenciaEdicao(int iIDEdicao, int iIDEvento, string sEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from tEdicao ");
            sSql.Append(" where Edicao = '" + sEdicao + "' ");
            sSql.Append(" and IDEvento = " + iIDEvento + " ");

            if (iIDEdicao != -1 && iIDEdicao != 0)
                sSql.Append(" and IDEdicao != " + iIDEdicao + "  ");

            return sSql.ToString();
        }

        internal string RetornarTotalCadeirasEvento(int iIDEvento)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select count(*) ");
            sSql.Append(" from tCadeira ");
            sSql.Append(" where IDEvento = " + iIDEvento + " ");

            return sSql.ToString();
        }

        internal string RetornarLocaisEvento()
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * from tLocalEvento ");

            return sSql.ToString();
        }

        internal string RetornarLotesVenda(int iIDEdicao, string sDataAtual)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select l.IDLote, l.Lote, edi.IDEdicao, ev.Evento, ");
            sSql.Append(" Edicao, CONVERT(varchar, edi.DataHoraEvento, 103) + ' ' + CONVERT(varchar, edi.DataHoraEvento, 108) [Data/Hora Evento], ");
            sSql.Append(" CONVERT(varchar, l.InicioVenda, 103) + ' ' + CONVERT(varchar, l.InicioVenda, 108) [Início Venda], ");
            sSql.Append(" CONVERT(varchar, l.FimVenda, 103) + ' ' + CONVERT(varchar, l.FimVenda, 108) [Fim Venda], ");
            sSql.Append(" NumeroIngressoExtraSocioCota, NumeroIngressoExtraSocioTitulo, NumeroIngressosValorNaoSocio, ValorInteiraCadeiraSocio, ValorInteiraCadeiraNaoSocio, ");
            sSql.Append(" ValorInteiraAvulsoSocio, ValorInteiraAvulsoNaoSocio, ");
            sSql.Append(" ValorMeiaCadeiraSocio, ValorMeiaCadeiraNaoSocio, ValorMeiaAvulsoSocio, ValorMeiaAvulsoNaoSocio, ");
            sSql.Append(" l.ValorSocioMasculino, l.ValorSocioFeminino, l.ValorNaoSocioMasculino, l.ValorNaoSocioFeminino, ");
            sSql.Append(" l.InteiraCadeiraSocio, ");
            sSql.Append(" l.InteiraCadeiraNaoSocio, ");
            sSql.Append(" l.InteiraAvulsoSocio, ");
            sSql.Append(" l.InteiraAvulsoNaoSocio, ");
            sSql.Append(" l.MeiaCadeiraSocio, ");
            sSql.Append(" l.MeiaCadeiraNaoSocio, ");
            sSql.Append(" l.MeiaAvulsoSocio, ");
            sSql.Append(" l.MeiaAvulsoNaoSocio, ");
            sSql.Append(" l.ValorIpanema, l.ValorGolodromo, l.ValorPortinari, l.ValorPergula, l.ValorSalaoDeFestas, l.ValorAcademia, ");
            sSql.Append(" l.SocioMasculino, l.SocioFeminino, l.NaoSocioMasculino, l.NaoSocioFeminino, ");
            sSql.Append(" case when (VagaEstacionamento = 1) then 'Sim' else 'Não' end VagaEstacionamento, VagasEstacionamento, case when (l.Ativo = 1) then 'Sim' else 'Não' end Ativo, ");
            sSql.Append(" NumIngressoEstacionamento, IngressosCadeira, ");
            sSql.Append(" IngressosAvulsos ");
            sSql.Append(" from tLote l ");
            sSql.Append(" inner join tEdicao edi on edi.IDEdicao = l.IDEdicao ");
            sSql.Append(" inner join tEvento ev on edi.IDEvento = ev.IDEvento ");
            sSql.Append(" where l.IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" and  CONVERT(varchar,'" + sDataAtual + "', 103) > CONVERT(varchar, l.InicioVenda, 103) + ' ' + CONVERT(varchar, l.InicioVenda, 108) ");
            sSql.Append(" and  CONVERT(varchar,'" + sDataAtual + "', 103) < CONVERT(varchar, l.FimVenda, 103) + ' ' + CONVERT(varchar, l.FimVenda, 108) ");
            sSql.Append(" order by l.Lote ");

            return sSql.ToString();

        }

    }


}

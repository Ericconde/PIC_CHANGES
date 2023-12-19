using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.objetos;
using System.Configuration;

namespace Model.dados
{
    public class catracaDAL
    {
        internal string LimparBaseCatracaFisica(int iIDEdicao, int iIDUsuario)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" delete tIngressosCatraca where IDEdicao = " + iIDEdicao + " and Tipo not in ('Estacionamento') "); /*Não deletará o estacionamento*/

            sSql.Append(" insert into tIngressosCatraca ");
            sSql.Append(" (IdentidadeEletronica, Tipo, IDEdicao, Nome, CPF, NumeroCota, Ticket, IDUsuarioCadastro) ");
            sSql.Append(" select IdentidadeEletronica, Case when (Tipo like '%Meio%') then 'Meia' else 'Inteira' end, ");
            sSql.Append(" " + iIDEdicao + ", Nome, CPF, NumeroCota, Ticket, " + iIDUsuario + " ");
            sSql.Append(" from vVendaVoucher ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " "); 

            return sSql.ToString();
        }

        internal string RetornarIngressosCatracaFisica(int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select ic.Ticket [Ticket (Núm. Ingresso)], ic.Tipo [Tipo de Ingresso], v.IDVenda Voucher, ");
            sSql.Append(" ic.Nome [Nome do Cliente], ic.NumeroCota Cota, ic.IdentidadeEletronica [Identidade Eletrônica],    ");
            sSql.Append(" convert(varchar, ic.DataHoraEntrada, 103) + ' ' + convert(varchar, ic.DataHoraEntrada, 108) [Data/Hora da Entrada], Catraca ");
            sSql.Append(" from tIngressosCatraca ic ");
            sSql.Append(" left join vVendaVoucher v on ic.IdentidadeEletronica = v.IdentidadeEletronica ");
            sSql.Append(" where ic.IDEdicao = "+ iIDEdicao + " ");

            return sSql.ToString();
        }

        internal string DarCargaIngressosApp(int iIDEdicao, int iIDUsuario)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" /*Limpar ingressos catracas*/ ");
            sSql.Append(" delete tIngressosCatraca ");
            sSql.Append(" where IDEdicao = "+ iIDEdicao + " ");

            sSql.Append(" /*Popular ingressos catracas*/ ");
            sSql.Append(" insert into tIngressosCatraca ");
            sSql.Append(" (IdentidadeEletronica, Tipo, IDEdicao, Nome, CPF, NumeroCota, Ticket, IDUsuarioCadastro) ");
            sSql.Append(" select IdentidadeEletronica, Case when (Tipo like '%Meio%') then 'Meia' else 'Inteira' end, ");
            sSql.Append(" " + iIDEdicao + ", Nome, CPF, NumeroCota, Ticket, " + iIDUsuario + " ");
            sSql.Append(" from vVendaVoucher ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " ");

            sSql.Append(" /*Popular ingressos catracas - estacionamento*/ ");
            sSql.Append(" insert into tIngressosCatraca ");
            sSql.Append(" (IdentidadeEletronica, Tipo, IDEdicao, Nome, CPF, NumeroCota, Ticket, IDUsuarioCadastro) ");
            sSql.Append(" select ie.IdentidadeEletronica, 'Estacionamento',  " + iIDEdicao + ", ie.Nome, ie.CPF,  ");
            sSql.Append(" case when (NumeroCota is null) then 0 else NumeroCota end, ");
            sSql.Append(" ie.Ticket, " + iIDUsuario + " ");
            sSql.Append(" from tIngressoEstacionamento ie ");
            sSql.Append(" left join vVenda v on v.IDVenda = ie.IDVenda ");
            sSql.Append(" where ie.IDEdicao = " + iIDEdicao + " and ie.Ativo = 1 ");

            return sSql.ToString();
        }

        internal string RetornarIngressosApp(int iIDEdicao, string sTipo, string sStatus)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select [Ticket (Núm. Ingresso)], [Tipo de Ingresso], Detalhe, Voucher, [Nome do Cliente], CPF, Cota, [Identidade Eletrônica], [Data/Hora da Entrada], [Responsável Entrada] ");
            sSql.Append(" from vIngressoCatraca ");
            sSql.Append(" where IDEdicao = "+ iIDEdicao + " ");
            if (sTipo != "Todos") sSql.Append(" and [Tipo de Ingresso] = '"+ sTipo + "' ");
            if (sStatus == "Não entrou") sSql.Append(" and [Data/Hora da Entrada] is null ");
            else if (sStatus == "Entrou") sSql.Append(" and [Data/Hora da Entrada] is not null ");

            sSql.Append(" select case when([Data/Hora da Entrada] is null) then 'Não entrou' else 'Entrou' end Status, ");
            sSql.Append(" COUNT(*) Quantidade ");
            sSql.Append(" from vIngressoCatraca ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" and [Tipo de Ingresso] = 'Catraca' ");
            sSql.Append(" group by case when([Data/Hora da Entrada] is null) then 'Não entrou' else 'Entrou' end ");

            sSql.Append(" select case when([Data/Hora da Entrada] is null) then 'Não entrou' else 'Entrou' end Status, ");
            sSql.Append(" COUNT(*) Quantidade ");
            sSql.Append(" from vIngressoCatraca ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" and [Tipo de Ingresso] = 'Estacionamento' ");
            sSql.Append(" group by case when([Data/Hora da Entrada] is null) then 'Não entrou' else 'Entrou' end ");

            sSql.Append(" select count(IdentidadeEletronica) Quantidade ");
            sSql.Append(" from vVendaVoucher ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " ");

            sSql.Append(" select count(IdentidadeEletronica) Quantidade ");
            sSql.Append(" from tIngressoEstacionamento ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " and Ativo = 1 ");

            return sSql.ToString();
        }

        internal string AtualizarRetornoCatracaFisica(string sIdentidadeEletronica, string sDataEntrada, string sCatraca)
        {
            StringBuilder sSql = new StringBuilder();
            
            sSql.Append(" update tIngressosCatraca ");
            sSql.Append(" set DataHoraEntrada = '" + sDataEntrada + "', ");
            sSql.Append(" Catraca = '"+sCatraca+"' ");
            sSql.Append(" where IdentidadeEletronica = '" + sIdentidadeEletronica + "' ");

            return sSql.ToString();
        }

        internal string AtualizarRetornoApp(string sIdentidadeEletronica, string sDataEntrada, string sIP, int iIDUsuario)
        {
            StringBuilder sSql = new StringBuilder();

            sSql.Append(" update tIngressosCatraca ");
            sSql.Append(" set DataHoraEntrada = '" + sDataEntrada + "', ");
            sSql.Append(" IP = '" + sIP + "', ");
            sSql.Append(" IDUsuario = " + iIDUsuario + " ");
            sSql.Append(" where IdentidadeEletronica = '" + sIdentidadeEletronica + "' ");

            return sSql.ToString();
        }

        internal string RetornarIngressoApp(string sIdentidadeEletronica)
        {
            StringBuilder sSql = new StringBuilder();

            sSql.Append(" select c.IdentidadeEletronica, ");
            sSql.Append(" u.Nome [Usuario], ");
            sSql.Append(" c.Nome Cliente, Tipo, ");
            sSql.Append(" CONVERT(varchar, DataHoraEntrada, 103) +' ' + CONVERT(varchar, DataHoraEntrada, 108) DataHoraEntrada ");
            sSql.Append(" from tIngressosCatraca c ");
            sSql.Append(" left join tUsuario u on c.IDUsuario = u.IDUsuario ");
            sSql.Append(" where IdentidadeEletronica = '"+ sIdentidadeEletronica + "' ");

            return sSql.ToString();
        }

        internal string RetornarIngressoApp(int iTicket)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from vIngressoCatraca c ");
            sSql.Append(" where [Ticket (Núm. Ingresso)] = " + iTicket + " ");

            return sSql.ToString();
        }

        internal string VerificarIngressoPosCatraca(string sIDentidadeEletronica)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append("  select * from tIngressosCatraca ");
            sSql.Append(" where IdentidadeEletronica = '" + sIDentidadeEletronica + "'");

            return sSql.ToString();
        }

        internal string AtualizarIngressoPosCatraca(string sIDentidadeEletronica, int iIDUsuario)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tIngressosCatraca ");
            sSql.Append(" set DataHoraEntrada = getdate(),");
            sSql.Append(" Catraca = -1, IDUsuario = "+ iIDUsuario + " ");
            sSql.Append(" where IdentidadeEletronica = '" + sIDentidadeEletronica + "'");

            return sSql.ToString();
        }
    }
}

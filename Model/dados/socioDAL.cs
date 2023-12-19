using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.objetos;

namespace Model.dados
{
    public class socioDAL
    {
        internal string RetornarSocio(string sNumeroCota, string sCPF)
        {
            StringBuilder sSql = new StringBuilder();            
            sSql.Append(" select * from tSocio ");
            sSql.Append(" where Num_Cota = '" + sNumeroCota + "' ");
            sSql.Append(" and REPLACE(REPLACE(CPF, '.',''), '-','') = '" + sCPF + "' ");

            return sSql.ToString();
        }

        internal string RetornarIDEstado(string sEstado)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select IDEstado ");
            sSql.Append(" from tEstado ");
            sSql.Append(" where Estado = '" + sEstado + "' ");

            return sSql.ToString();
        }

        internal string RetornarIDCidade(int iIDEstado, string sCidade)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select IDCidade ");
            sSql.Append(" from tCidade ");
            sSql.Append(" where Cidade = '" + sCidade + "' ");
            sSql.Append(" and IDEstado = " + iIDEstado + " ");

            return sSql.ToString();
        }

        internal string VerificarExistenciaSocio(int iNumeroCota, string sDigitoCota, string sCPF)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from tSocio ");
            sSql.Append(" where Num_Cota = " + iNumeroCota + " ");
            sSql.Append(" and Digito = '" + sDigitoCota + "' ");
            sSql.Append(" and REPLACE(REPLACE(CPF, '-', ''), '.', '') = '" + sCPF + "' ");

            return sSql.ToString();
        }

        internal string VerificarExistenciaSocio(int iNumeroCota, string sCPF)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from tSocio ");
            sSql.Append(" where Num_Cota = " + iNumeroCota + " ");
            sSql.Append(" and REPLACE(REPLACE(CPF, '-', ''), '.', '') = '" + sCPF + "' ");

            return sSql.ToString();
        }

        internal string RetornarDigitos(string sNumeroCota)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select Nome, Num_Cota, Digito,  ");
            sSql.Append(" case when (Debito = 1) then 'Sim' else 'Não' end [Débito] ");
            sSql.Append(" from tSocio ");
            sSql.Append(" where Num_Cota = '" + sNumeroCota + "' ");
            sSql.Append(" order by Digito ");

            return sSql.ToString();
        }

        internal string AtualizarDebito(string sNumeroCota)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tSocio ");
            sSql.Append(" set Debito = 0 ");
            sSql.Append(" where Num_Cota = " + sNumeroCota + " ");

            return sSql.ToString();
        }

        internal string AtualizarBaseSocios()
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" exec sAtualizarBaseSocios ");

            return sSql.ToString();
        }

    }
}

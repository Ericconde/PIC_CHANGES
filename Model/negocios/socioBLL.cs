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
    public class socioBLL
    {
        public socio RetornarSocio(string sNumeroCota, string sCPF)
        {
            socioDAL DSocio = new socioDAL();
            string sSql = DSocio.RetornarSocio(sNumeroCota, sCPF);
            DataTable dataTable = PontoBr.Banco.SqlServer.RetornarDataTable(sSql);

            socio Socio = new socio();

            if (dataTable.Rows.Count > 0)
            {
                Socio.Num_Cota = Convert.ToInt32(dataTable.Rows[0]["Num_Cota"]);
                Socio.Digito = dataTable.Rows[0]["Digito"].ToString();
                Socio.Nome = dataTable.Rows[0]["Nome"].ToString();
                Socio.Dat_Nasc = dataTable.Rows[0]["Dat_Nasc"].ToString();
                Socio.CPF = dataTable.Rows[0]["CPF"].ToString();
                Socio.RG = dataTable.Rows[0]["RG"].ToString();
                Socio.Email = dataTable.Rows[0]["Email"].ToString();
                Socio.fone1 = dataTable.Rows[0]["fone1"].ToString();
                Socio.fone2 = dataTable.Rows[0]["fone2"].ToString();
                Socio.Logradouro = dataTable.Rows[0]["Logradouro"].ToString();
                Socio.Numero = dataTable.Rows[0]["Numero"].ToString();
                Socio.Complemento = dataTable.Rows[0]["Complemento"].ToString();
                Socio.Bairro = dataTable.Rows[0]["Bairro"].ToString();
                Socio.CEP = dataTable.Rows[0]["CEP"].ToString();
                Socio.Nom_Cidade = dataTable.Rows[0]["Nom_Cidade"].ToString();
                Socio.Nom_Estado = dataTable.Rows[0]["Nom_Estado"].ToString();
                Socio.Abrev_Estado = dataTable.Rows[0]["Abrev_Estado"].ToString();
                Socio.Debito = Convert.ToInt32(dataTable.Rows[0]["Debito"]);
            }

            return Socio;
        }

        public int RetornarIDEstado(string sEstado)
        {
            socioDAL DSocio = new socioDAL();
            string sSql = DSocio.RetornarIDEstado(sEstado);
            string sIDEstado = PontoBr.Banco.SqlServer.RetornarDadoUnicoDoBanco(sSql);

            return sIDEstado == "" ? -1 : Convert.ToInt32(sIDEstado);
        }

        public int RetornarIDCidade(int iIDEstado, string sCidade)
        {
            socioDAL DSocio = new socioDAL();
            string sSql = DSocio.RetornarIDCidade(iIDEstado, sCidade);
            string sIDCidade = PontoBr.Banco.SqlServer.RetornarDadoUnicoDoBanco(sSql);

            return sIDCidade == "" ? -1 : Convert.ToInt32(sIDCidade);
        }

        public bool VerificarExistenciaSocio(int iNumeroCota, string sDigitoCota, string sCPF)
        {
            socioDAL DSocio = new socioDAL();
            string sSql = DSocio.VerificarExistenciaSocio(iNumeroCota, sDigitoCota, sCPF);
            return PontoBr.Banco.SqlServer.VerificarExistenciaDeDados(sSql);
        }

        public bool VerificarExistenciaSocio(int iNumeroCota, string sCPF)
        {
            socioDAL DSocio = new socioDAL();
            string sSql = DSocio.VerificarExistenciaSocio(iNumeroCota, sCPF);
            return PontoBr.Banco.SqlServer.VerificarExistenciaDeDados(sSql);
        }

        public DataTable RetornarDigitos(string sNumeroCota)
        {
            socioDAL DSocio = new socioDAL();
            string sSql = DSocio.RetornarDigitos(sNumeroCota);
            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public void AtualizarDebito(string sNumeroCota)
        {
            socioDAL DSocio = new socioDAL();
            string sSql = DSocio.AtualizarDebito(sNumeroCota);
            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public void AtualizarBaseSocios()
        {
            socioDAL DSocio = new socioDAL();
            string sSql = DSocio.AtualizarBaseSocios();
            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }
    }
}

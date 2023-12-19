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
    public class catracaBLL
    {
        public void LimparBaseCatracaFisica(int iIDEdicao, int iIDUsuario)
        {
            catracaDAL DCatraca = new catracaDAL();
            string sSql = DCatraca.LimparBaseCatracaFisica(iIDEdicao, iIDUsuario);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public DataTable RetornarIngressosCatracaFisica(int iIDEdicao)
        {
            catracaDAL DCatraca = new catracaDAL();
            string sSql = DCatraca.RetornarIngressosCatracaFisica(iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public void DarCargaIngressosApp(int iIDEdicao, int iIDUsuario)
        {
            catracaDAL DCatraca = new catracaDAL();
            string sSql = DCatraca.DarCargaIngressosApp(iIDEdicao, iIDUsuario);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }
        
        public DataSet RetornarIngressosApp(int iIDEdicao, string sTipo, string sStatus)
        {
            catracaDAL DCatraca = new catracaDAL();
            string sSql = DCatraca.RetornarIngressosApp(iIDEdicao, sTipo, sStatus);

            return PontoBr.Banco.SqlServer.RetornarDataSet(sSql);
        }

        public void AtualizarRetornoCatracaFisica(string sIdentidadeEletronica, string sDataEntrada, string sCatraca)
        {
            catracaDAL DCatraca = new catracaDAL();
            string sSql = DCatraca.AtualizarRetornoCatracaFisica(sIdentidadeEletronica, sDataEntrada, sCatraca);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public void AtualizarRetornoApp(string sIdentidadeEletronica, string sDataEntrada, string sIP, int iIDUsuario)
        {
            catracaDAL DCatraca = new catracaDAL();
            string sSql = DCatraca.AtualizarRetornoApp(sIdentidadeEletronica, sDataEntrada, sIP, iIDUsuario);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public DataTable RetornarIngressoApp(string sIdentidadeEletronica)
        {
            catracaDAL DCatraca = new catracaDAL();
            string sSql = DCatraca.RetornarIngressoApp(sIdentidadeEletronica);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarIngressoApp(int iTicket)
        {
            catracaDAL DCatraca = new catracaDAL();
            string sSql = DCatraca.RetornarIngressoApp(iTicket);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable VerificarIngressoPosCatraca(string sIDentidadeEletronica)
        {
            catracaDAL DCatraca = new catracaDAL();
            string sSql = DCatraca.VerificarIngressoPosCatraca(sIDentidadeEletronica);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public void AtualizarIngressoPosCatraca(string sIDentidadeEletronica, int iIDUsuario)
        {
            catracaDAL DCatraca = new catracaDAL();
            string sSql = DCatraca.AtualizarIngressoPosCatraca(sIDentidadeEletronica, iIDUsuario);
            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }
    }
}

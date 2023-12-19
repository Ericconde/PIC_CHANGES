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
    public class usuarioBLL
    {
        public string VerificarExistenciaEmail(string sEmail, int iIDUsuario)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.VerificarExistenciaEmail(sEmail, iIDUsuario);

            return sSql;
        }

        public string VerificarSenha(int iIDUsuario, string sSenha)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.VerificarSenha(iIDUsuario, sSenha);

            return sSql;
        }

        public DataTable RetornarUsuario(string sEmail, string sSenha)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.RetornarUsuario(sEmail, sSenha);

            DataTable dataTable = PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
            return dataTable;
        }
        
        public DataTable RetornarUsuarioByEmail(string sEmail)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.RetornarUsuarioByEmail(sEmail);

            DataTable dataTable = PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
            return dataTable;
        }

        public DataTable RetornarUsuario(string sEmail, string sSenha, int iIDPerfil)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.RetornarUsuario(sEmail, sSenha, iIDPerfil);

            DataTable dataTable = PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
            return dataTable;
        }

        public void AlterarSenha(string sEmail, string sSenha, int iResetarSenha)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.AlterarSenha(sEmail, sSenha, iResetarSenha);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public void ExcluirUsuario(int iIDUsuario)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.ExcluirUsuario(iIDUsuario);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public DataTable RetornarUsuario(int iIDUsuario)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.RetornarUsuario(iIDUsuario);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public int CadastrarUsuario(usuario Usuario)//--
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.CadastrarUsuario(Usuario);

            return Convert.ToInt32(PontoBr.Banco.SqlServer.ExecutarSqlComRetornoDeIdentity(sSql));
        }

        public void CadastrarLogAcesso(int iIDUsuario, string sIP, string sPagina, string sEvento)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            PontoBr.Banco.SqlServer.ExecutarSql(DUsuario.CadastrarLogAcesso(iIDUsuario, sIP, sPagina, sEvento));
        }

        public void AtualizarUsuario(usuario Usuario, int iIDUsuarioAlteracao)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.AtualizarUsuario(Usuario, iIDUsuarioAlteracao);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public string VerificarExistenciaNumeroCota(int iNumeroCota, string sDigitoCota, int iIDUsuario)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.VerificarExistenciaNumeroCota(iNumeroCota, sDigitoCota, iIDUsuario);

            return sSql;
        }

        public DataTable RetornarUsuarios(string sNome, string sEmail, int iIDPerfil, string sNumeroCota, string sCPF)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.RetornarUsuarios(sNome, sEmail, iIDPerfil, sNumeroCota, sCPF);
            DataTable dtUsuarios = PontoBr.Banco.SqlServer.RetornarDataTable(sSql);

            return dtUsuarios;
        }

        public void CadastrarLogAlteracaoEmail(int iIDUsuario, string sEmailNovo, string sEmailAntigo, int iIDUsuarioAlteracao)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            PontoBr.Banco.SqlServer.ExecutarSql(DUsuario.CadastrarLogAlteracaoEmail(iIDUsuario, sEmailNovo, sEmailAntigo, iIDUsuarioAlteracao));
        }

        public void AtualizarToken(int iIDUsuario, string sToken)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.AtualizarToken(iIDUsuario, sToken);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public string RetornarIDUsuario(int iIDCliente)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.RetornarIDUsuario(iIDCliente);

            return PontoBr.Banco.SqlServer.ExecutarSqlComRetornoDeIdentity(sSql);
        }

        public DataTable RetornarUsuarioAleatorio()
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.RetornarUsuarioAleatorio();

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public bool VerificarExitenciaToken(string sToken, int iIDUsuario)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.VerificarExitenciaToken(sToken, iIDUsuario);

            return PontoBr.Banco.SqlServer.VerificarExistenciaDeDados(sSql);
        }

        public void AtivarUsuario(int iIDUsuario)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.AtivarUsuario(iIDUsuario);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public string RetornarSiteMenorCarga()
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.RetornarSiteMenorCarga();

            return PontoBr.Banco.SqlServer.RetornarDadoUnicoDoBanco(sSql);
        }

        public void AtualizarSiteCarga(string sSiteCarga)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.AtualizarSiteCarga(sSiteCarga);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        //Blacklist
        public void CadastrarBlackList(string sIP, string sMotivo, int iIDUsuario)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.CadastrarBlackList(sIP, sMotivo, iIDUsuario);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        //Blacklist
        public void AtualizarBlackList(int iBloqueado, int iIDUsuarioAlteracao, string sIP)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.AtualizarBlackList(iBloqueado, iIDUsuarioAlteracao, sIP);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        //Blacklist
        public bool VerificarIPBloqueadoBlackList(string sIP)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.VerificarIPBloqueadoBlackList(sIP);

            return PontoBr.Banco.SqlServer.VerificarExistenciaDeDados(sSql);
        }

        //Blacklist
        public DataTable RetonarIPLiberadoBlackList(string sIP)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.RetonarIPLiberadoBlackList(sIP);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        //Blacklist
        public DataTable RetornarBlackList(string sIP, string sNome, string sDataInicial, string sDataFinal)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.RetornarBlackList(sIP, sNome, sDataInicial, sDataFinal);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        //Blacklist - Bloqueia o usuário caso haja suspeita de fraude na compra com cartão de crédito
        public void BloquearUsuario(int iIDUsuario)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.BloquearUsuario(iIDUsuario);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public bool VerificarWhiteList(string sIP)
        {
            usuarioDAL DUsuario = new usuarioDAL();
            string sSql = DUsuario.VerificarWhiteList(sIP);

            return PontoBr.Banco.SqlServer.VerificarExistenciaDeDados(sSql);
        }
    }
}

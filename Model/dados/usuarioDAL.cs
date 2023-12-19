using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.objetos;

namespace Model.dados
{
    public class usuarioDAL
    {
        internal string VerificarExistenciaEmail(string sEmail, int iIDUsuario)
        {
            string sSql = " SELECT IDUsuario FROM tUsuario WHERE Email = '" + sEmail + "'";

            if (iIDUsuario != 0 && iIDUsuario != -1)
                sSql += " and IDUsuario != "+iIDUsuario+" ";

            return sSql;
        }

        internal string VerificarSenha(int iIDUsuario, string sSenha)
        {
            string sSql = " SELECT IDUsuario FROM tUsuario WHERE Senha = '" + sSenha + "' and IDUsuario = " + iIDUsuario + " ";

            return sSql;
        }

        internal string RetornarUsuario(string sEmail, string sSenha)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select u.IDUsuario, u.Nome,u.Senha, u.Email, tp.Perfil, u.Ativo, CONVERT(varchar, u.Cadastro, 103) [Cadastro], u.IDPerfil, ");
            sSql.Append(" c.CPF, c.RG, u.ResetarSenha, c.IDCliente, sd.Debito, sd.Dependentes, ");
            sSql.Append(" c.NumeroCota, c.DigitoCota, u.Token ");
            sSql.Append(" from tusuario u ");
            sSql.Append(" inner join tPerfil tp on tp.IDPerfil = u.IDPerfil ");
            sSql.Append(" left join tCliente c on u.IDUsuario = c.IDUsuario ");
            sSql.Append(" left join vSocioDependentes sd on sd.Num_cota = c.NumeroCota ");
            sSql.Append(" where u.Email = '" + sEmail + "' and u.Senha = '" + sSenha + "' and u.Ativo = 1  order by u.Nome ");

            return sSql.ToString();
        }internal string RetornarUsuarioByEmail(string sEmail)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select u.IDUsuario, u.Nome,u.Senha, u.Email, tp.Perfil, u.Ativo, CONVERT(varchar, u.Cadastro, 103) [Cadastro], u.IDPerfil, ");
            sSql.Append(" c.CPF, c.RG, u.ResetarSenha, c.IDCliente, sd.Debito, sd.Dependentes, ");
            sSql.Append(" c.NumeroCota, c.DigitoCota, u.Token ");
            sSql.Append(" from tusuario u ");
            sSql.Append(" inner join tPerfil tp on tp.IDPerfil = u.IDPerfil ");
            sSql.Append(" left join tCliente c on u.IDUsuario = c.IDUsuario ");
            sSql.Append(" left join vSocioDependentes sd on sd.Num_cota = c.NumeroCota ");
            sSql.Append(" where u.Email = '" + sEmail + "' and u.Ativo = 1  order by u.Nome ");

            return sSql.ToString();
        }

        internal string RetornarUsuario(string sEmail, string sSenha, int iIDPerfil)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select u.IDUsuario, u.Nome, u.Email, tp.Perfil ");
            sSql.Append(" from tusuario u ");
            sSql.Append(" inner join tPerfil tp on tp.IDPerfil = u.IDPerfil ");
            sSql.Append(" where u.Email = '" + sEmail + "' and u.Senha = '" + sSenha + "' ");
            sSql.Append(" and u.IDPerfil = "+iIDPerfil+" and u.Ativo = 1  order by u.Nome ");

            return sSql.ToString();
        }

        internal string AlterarSenha(string sEmail, string sSenha, int iResetarSenha)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" UPDATE TUSUARIO SET SENHA = '" + sSenha + "', ResetarSenha = " + iResetarSenha + " WHERE Email = '" + sEmail + "'");

            return sSql.ToString();
        }

        internal string ExcluirUsuario(int iIDUsuario)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" delete tUsuario where IDUsuario = "+iIDUsuario+" ");

            return sSql.ToString();
        } 

        internal string RetornarUsuarios(string sNome, string sEmail)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select top 25 a.IDUsuario, ");
            sSql.Append(" a.Nome, a.Login, case when a.Ativo = 1 then 'Sim' else 'Não' end Ativo, ");
            sSql.Append(" b.Perfil, CONVERT(varchar, a.DataCadastro, 103) + ' ' + CONVERT(varchar, a.DataCadastro, 108) [Data Cadastro] ");
            sSql.Append(" from tUsuario a ");
            sSql.Append(" inner join tPerfil b on a.IDPerfil = b.IDPerfil ");
            sSql.Append(" where a.IDPerfil not in (1) ");

            if (sNome != "")
                sSql.Append(" and a.Nome like '%" + sNome + "%' ");
            if (sEmail != "")
                sSql.Append(" and a.Login like '%" + sEmail + "%' ");

            sSql.Append(" order by a.Nome ");


            return sSql.ToString();
        }

        internal string CadastrarUsuario(usuario Usuario)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" insert into tUsuario ");
            sSql.Append(" (Nome, Senha, Email, IDPerfil, Ativo) ");
            sSql.Append(" values ");
            sSql.Append(" ('" + Usuario.Nome + "', '" + Usuario.Senha + "', '" + Usuario.Email + "', " + Usuario.IDPerfil + ", " + Usuario.Ativo + ") ");

            return sSql.ToString();
        }

        internal string RetornarUsuario(int iIDUsuario)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select u.IDUsuario, u.Nome,u.Senha, u.Email, tp.Perfil, u.Ativo, CONVERT(varchar, u.Cadastro, 103) [Cadastro],tp.IDPerfil, ");
            sSql.Append(" c.CPF, c.RG, u.ResetarSenha, c.IDCliente, sd.Debito, sd.Dependentes, ");
            sSql.Append(" c.NumeroCota, c.DigitoCota, u.Token ");
            sSql.Append(" from tusuario u ");
            sSql.Append(" inner join tPerfil tp on tp.IDPerfil = u.IDPerfil ");
            sSql.Append(" left join tCliente c on u.IDUsuario = c.IDUsuario ");
            sSql.Append(" left join vSocioDependentes sd on sd.Num_cota = c.NumeroCota ");
            sSql.Append(" where u.IDUsuario = " + iIDUsuario + " ");

            return sSql.ToString();
        }      

        internal string CadastrarLogAcesso(int iIDUsuario, string sIP, string sPagina, string sEvento)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" Declare @IDPagina int ");
            sSql.Append(" ");
            sSql.Append(" if not exists (select * from tPagina where Pagina = '" + sPagina + "') ");
            sSql.Append(" Begin ");
            sSql.Append("     insert into tPagina ");
            sSql.Append("     (Pagina) ");
            sSql.Append("     values ");
            sSql.Append("     ('" + sPagina + "') ");
            sSql.Append("  ");
            sSql.Append("     set @IDPagina = @@IDENTITY ");
            sSql.Append(" End ");
            sSql.Append(" else ");
            sSql.Append("     select @IDPagina = IDPagina from tPagina  where Pagina = '" + sPagina + "' ");

            sSql.Append(" insert into tLogAcesso ");
            sSql.Append(" (IDUsuario, IP, IDPagina, Evento) ");
            sSql.Append(" values ");
            sSql.Append(" (" + iIDUsuario + ", '" + sIP + "', @IDPagina, '" + sEvento + "') ");

            return sSql.ToString();
        }

        internal string AtualizarUsuario(usuario Usuario, int iIDUsuarioAlteracao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tUsuario ");
            sSql.Append(" set Email = '" + Usuario.Email + "', ");
            sSql.Append(" Nome = '" + Usuario.Nome + "', ");
            sSql.Append(" Senha = '" + Usuario.Senha + "', ");
            sSql.Append(" IDPerfil = '" + Usuario.IDPerfil + "', ");
            sSql.Append(" Ativo = " + Usuario.Ativo + ", ");
            sSql.Append(" IDUsuarioAlteracao = " + iIDUsuarioAlteracao + ", ");            
            sSql.Append(" Alteracao = getdate() ");
            sSql.Append(" where IDUsuario = " + Usuario.IDUsuario + " ");

            if (Usuario.NumeroCota != -1
                && !String.IsNullOrEmpty(Usuario.DigitoCota))
            {
                sSql.Append(" update tCliente set NumeroCota = " + Usuario.NumeroCota + ", DigitoCota = '" + Usuario.DigitoCota + "' ");
                if (Usuario.CPF != "" && Usuario.CPF != "0" && Usuario.CPF != "-1") sSql.Append(" , CPF = '" + Usuario.CPF + "' ");
                sSql.Append(" where IDUsuario = " + Usuario.IDUsuario + " ");
            }
            else
            {
                sSql.Append(" update tCliente set NumeroCota = 0, DigitoCota = null ");
                if (Usuario.CPF != "" && Usuario.CPF != "0" && Usuario.CPF != "-1") sSql.Append(" , CPF = '" + Usuario.CPF + "' ");
                sSql.Append(" where IDUsuario = " + Usuario.IDUsuario + " ");
            }

            return sSql.ToString();
        }

        internal string VerificarExistenciaNumeroCota(int iNumeroCota, string sDigitoCota, int iIDUsuario)
        {
            string sSql = " SELECT IDUsuario FROM tcliente WHERE NumeroCota = '" + iNumeroCota + "'";
            sSql += " and DigitoCota = '" + sDigitoCota + "'";

            if (iIDUsuario != 0 && iIDUsuario != -1)
                sSql += " and IDUsuario != " + iIDUsuario + " ";

            return sSql;
        }

        internal string RetornarUsuarios(string sNome, string sEmail, int iIDPerfil, string sNumeroCota, string sCPF)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select u.IDUsuario, u.Nome, c.CPF, case when u.Ativo = 1 then 'Sim' else 'Não' end Ativo,  ");
            sSql.Append(" u.Email, tp.Perfil, ");
            sSql.Append(" CONVERT(varchar, u.Cadastro, 103) + ' ' + CONVERT(varchar, u.Cadastro, 108) [Cadastro], ");
            sSql.Append(" CONVERT(varchar, c.NumeroCota) + '-' + CONVERT(varchar, c.DigitoCota) Cota ");
            sSql.Append(" from tusuario u ");
            sSql.Append(" inner join tPerfil tp on tp.IDPerfil = u.IDPerfil ");
            sSql.Append(" left join tCliente c on u.IDUsuario = c.IDUsuario ");
            sSql.Append(" where u.Nome like '" + sNome + "%' collate SQL_Latin1_General_CP1_CI_AI ");
            if (iIDPerfil != -1) sSql.Append(" and u.IDPerfil = " + iIDPerfil + " ");
            
            if (!String.IsNullOrEmpty(sEmail))
                sSql.Append(" and u.Email like '"+ sEmail +"%'");

            if (!String.IsNullOrEmpty(sNumeroCota))
                sSql.Append(" and c.NumeroCota = '" + sNumeroCota + "'");

            if (!String.IsNullOrEmpty(sCPF))
                sSql.Append(" and c.CPF = '" + sCPF + "'");

            return sSql.ToString();
        }

        internal string CadastrarLogAlteracaoEmail(int iIDUsuario, string sEmailNovo, string sEmailAntigo, int iIDUsuarioAlteracao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" insert into tLogAlteracaoEmail ");
            sSql.Append(" (IDUsuario, EmailNovo, EmailAntigo, IDUsuarioAlteracao) ");
            sSql.Append(" values ");
            sSql.Append(" (" + iIDUsuario + ", '" + sEmailNovo + "', '" + sEmailAntigo + "', " + iIDUsuarioAlteracao + ") ");

            return sSql.ToString();
        }

        internal string AtualizarToken(int iIDUsuario, string sToken)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tUsuario ");
            sSql.Append(" set Token = '"+sToken+"' ");
            sSql.Append(" where IDUsuario = "+iIDUsuario+" ");

            return sSql.ToString();
        }

        internal string RetornarIDUsuario(int iIDCliente)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select IDUsuario from tCliente ");
            sSql.Append(" where IDCliente = " + iIDCliente + " ");
            
            return sSql.ToString();
        }

        internal string RetornarUsuarioAleatorio()
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select top 1 u.IDUsuario, u.Nome,u.Senha, u.Email, tp.Perfil, u.Ativo, CONVERT(varchar, u.Cadastro, 103) [Cadastro], tp.IDPerfil, ");
            //sSql.Append(" c.CPF, c.RG, u.ResetarSenha, c.IDCliente, sd.Debito, sd.Dependentes, ");
            sSql.Append(" c.CPF, c.RG, u.ResetarSenha, c.IDCliente, 0 Debito, 0 Dependentes, ");
            sSql.Append(" c.NumeroCota, c.DigitoCota, u.Token ");
            sSql.Append(" from tusuario u ");
            sSql.Append(" inner join tPerfil tp on tp.IDPerfil = u.IDPerfil ");
            sSql.Append(" left join tCliente c on u.IDUsuario = c.IDUsuario ");
            //sSql.Append(" left join vSocioDependentes sd on sd.Num_cota = c.NumeroCota ");
            sSql.Append(" where u.IDPerfil = 3 ");
            //sSql.Append(" and u.IDUsuario not in (select IDUsuario from tLogAcesso where Acesso > CONVERT(varchar, getdate(), 111)) ");
            sSql.Append(" order by newid() ");

            return sSql.ToString();
        }
        
        internal string VerificarExitenciaToken(string sToken, int iIDUsuario)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select IDUsuario ");
            sSql.Append(" from tusuario ");
            sSql.Append(" where IDUsuario = " + iIDUsuario + " ");
            sSql.Append(" and Token = '" + sToken + "' ");

            return sSql.ToString();
        }

        internal string AtivarUsuario(int iIDUsuario)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tUsuario ");
            sSql.Append(" set Ativo = 1 ");
            sSql.Append(" where IDUsuario = " + iIDUsuario + " ");;

            return sSql.ToString();
        }

        internal string RetornarSiteMenorCarga()
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select top 1 SiteCarga ");
            sSql.Append(" from tBalanceamentoCarga ");
            sSql.Append(" order by Conectados ");

            return sSql.ToString();
        }

        internal string AtualizarSiteCarga(string sSiteCarga)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tBalanceamentoCarga ");
            sSql.Append(" set Conectados = Conectados + 1 ");
            sSql.Append(" where SiteCarga = '"+ sSiteCarga + "' ");

            return sSql.ToString();
        }

        //Blacklist
        internal string CadastrarBlackList(string sIP, string sMotivo, int iIDUsuario)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" insert into tBlackList ");
            sSql.Append(" (IP, MotivoBloqueio, IDUsuario) ");
            sSql.Append(" values ");
            sSql.Append(" ('"+ sIP + "', '"+ sMotivo + "', "+ iIDUsuario + ") ");

            return sSql.ToString();
        }

        //Blacklist
        internal string AtualizarBlackList(int iBloqueado, int iIDUsuarioAlteracao, string sIP)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tBlackList ");
            sSql.Append(" set Bloqueado = "+ iBloqueado + ", ");
            sSql.Append(" IDUsuarioAlteracao = "+ iIDUsuarioAlteracao + ", ");
            sSql.Append(" DataAlteracao = getdate() ");
            sSql.Append(" where IP = '"+ sIP + "' ");

            return sSql.ToString();
        }

        //Blacklist
        internal string VerificarIPBloqueadoBlackList(string sIP)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from tBlackList ");
            sSql.Append(" where IP = '"+ sIP + "' ");
            sSql.Append(" and Bloqueado = 1 ");

            return sSql.ToString();
        }

        //Blacklist
        internal string RetonarIPLiberadoBlackList(string sIP)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from tBlackList ");
            sSql.Append(" where IP = '" + sIP + "' ");
            sSql.Append(" and Bloqueado = 0 ");

            return sSql.ToString();
        }

        //Blacklist
        internal string RetornarBlackList(string sIP, string sNome, string sDataInicial, string sDataFinal)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select b.*, ");
            sSql.Append(" convert(varchar, DataCadastro, 103) + ' ' + convert(varchar, DataCadastro, 108) [Data Bloqueio], ");
            sSql.Append(" convert(varchar, DataAlteracao, 103) + ' ' + convert(varchar, DataAlteracao, 108) [Data Alteração], ");
            sSql.Append(" cliente.Nome Cliente, ua.Nome [Usuário Alteração] ");
            sSql.Append(" from tBlackList b ");
            sSql.Append(" inner join tUsuario cliente on b.IDUsuario = cliente.IDUsuario ");
            sSql.Append(" left join tUsuario ua on b.IDUsuarioAlteracao = ua.IDUsuario ");
            sSql.Append(" where b.IP like '%"+ sIP + "%' ");
            sSql.Append(" and cliente.Nome like '%"+ sNome + "%' ");

            if (PontoBr.Conversoes.Data.ValidarDataBr(sDataInicial)
                && PontoBr.Conversoes.Data.ValidarDataBr(sDataFinal))
            {
                sDataInicial = PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(sDataInicial);
                sDataFinal = PontoBr.Conversoes.Data.ConverterDataFormatoDDMMAAAAComBarraParaAAAAMMDDComBarra(sDataFinal) + " 23:59:59";

                sSql.Append(" and DataCadastro between '" + sDataInicial + "' and '" + sDataFinal + "' ");
            }

            return sSql.ToString();
        }

        //Blacklist - Bloqueia o usuário caso haja suspeita de fraude na compra com cartão de crédito
        internal string BloquearUsuario(int iIDUsuario)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tUsuario ");
            sSql.Append(" set Ativo = 0 ");
            sSql.Append(" where IDUsuario = " + iIDUsuario + " "); ;

            return sSql.ToString();
        }

        internal string VerificarWhiteList(string sIP)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from tWhitelist ");
            sSql.Append(" where IP = '"+ sIP + "' ");

            return sSql.ToString();
        }
    }
}

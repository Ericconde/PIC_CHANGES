using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.objetos;

namespace Model.dados
{
    public class clientesDAL
    {
        internal string CadastrarCliente(cliente Cliente)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" INSERT INTO tCliente ");
            sSql.Append(" (IDUsuario, ");
            sSql.Append(" NumeroCota, ");
            if (Cliente.IDPerfil == 3) sSql.Append(" DigitoCota, ");
            sSql.Append(" Nome, ");
            sSql.Append(" RG, ");
            sSql.Append(" CPF, ");
            sSql.Append(" DataNascimento, ");
            sSql.Append(" TelefoneResidencial, ");
            sSql.Append(" TelefoneComercial, ");
            sSql.Append(" TelefoneCelular, ");
            sSql.Append(" Logradouro, ");
            sSql.Append(" Numero, ");
            sSql.Append(" Complemento, ");
            sSql.Append(" Bairro, ");
            sSql.Append(" IDCidade, ");
            sSql.Append(" CEP) ");
            sSql.Append(" VALUES ");
            sSql.Append(" ('" + Cliente.IDUsuario + "', ");
            sSql.Append(" '" + Cliente.NumCota + "', ");
            if (Cliente.IDPerfil == 3) sSql.Append(" '" + Cliente.DigitoCota + "', ");
            sSql.Append(" '" + Cliente.Nome + "', ");
            sSql.Append(" '" + Cliente.RG + "', ");
            sSql.Append(" '" + Cliente.CPF + "', ");
            sSql.Append(" '" + Cliente.DataNascimento + "', ");
            sSql.Append(" '" + Cliente.TelefoneResidencial + "', ");
            sSql.Append(" '" + Cliente.TelefoneComercial + "', ");
            sSql.Append(" '" + Cliente.TelefoneCelular + "', ");
            sSql.Append(" '" + Cliente.Logradouro + "', ");
            sSql.Append(" '" + Cliente.Numero + "', ");
            sSql.Append(" '" + Cliente.Complemento + "', ");
            sSql.Append(" '" + Cliente.Bairro + "', ");
            sSql.Append(" '" + Cliente.IDCidade + "', ");
            sSql.Append(" '" + Cliente.CEP + "') ");

            return sSql.ToString();
        }

        internal string RetornarEstados()
        {
            string sSql = "SELECT * FROM TESTADO WHERE Ativo = 1 ORDER BY ESTADO ";
            return sSql;
        }

        internal string RetornarCidades(int iIDEstado)
        {
            string sSql = "SELECT * FROM TCIDADE WHERE IDESTADO = " + iIDEstado + " ORDER BY CIDADE";
            return sSql;
        }

        internal string RetornarCliente(int iIDUsuario)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select c.NumeroCota,cd.Cidade,c.IDCliente, c.Nome,c.CPF, c.RG, convert(varchar, c.DataNascimento, 103) DataNascimento, u.Email, u.Senha, ");
            sSql.Append(" c.TelefoneResidencial, c.TelefoneCelular, c.TelefoneComercial, ");
            sSql.Append(" c.CEP, c.Logradouro, c.Numero, c.Complemento, c.Bairro, c.IDCidade, cd.IDEstado, ");
            sSql.Append(" c.IDCliente, c.IDUsuario, sd.Debito, u.IDPerfil, e.Estado ");
            sSql.Append(" from tCliente c ");
            sSql.Append(" left join tUsuario u on u.IDUsuario = c.IDUsuario ");
            sSql.Append(" left join tcidade cd on cd.IDCidade = c.IDCidade ");
            sSql.Append("  left join tEstado e on e.IDEstado = cd.IDEstado ");
            sSql.Append(" left join vSocioDependentes sd on sd.Num_cota = c.NumeroCota ");
            sSql.Append(" where u.idusuario =  " + iIDUsuario + "  ");

            return sSql.ToString();
        }

        internal string RetornarCliente(string sNome)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select c.NumeroCota,cd.Cidade,c.IDCliente, c.Nome,c.CPF, c.RG, convert(varchar, c.DataNascimento, 103) DataNascimento, u.Email, u.Senha, ");
            sSql.Append(" c.TelefoneResidencial, c.TelefoneCelular, c.TelefoneComercial, ");
            sSql.Append(" c.CEP, c.Logradouro, c.Numero, c.Complemento, c.Bairro, c.IDCidade, cd.IDEstado, ");
            sSql.Append(" c.IDCliente, c.IDUsuario, sd.Debito, u.IDPerfil, e.Estado ");
            sSql.Append(" from tCliente c ");
            sSql.Append(" left join tUsuario u on u.IDUsuario = c.IDUsuario ");
            sSql.Append(" left join tcidade cd on cd.IDCidade = c.IDCidade ");
            sSql.Append("  left join tEstado e on e.IDEstado = cd.IDEstado ");
            sSql.Append(" left join vSocioDependentes sd on sd.Num_cota = c.NumeroCota ");
            sSql.Append(" where c.Nome =  '" + sNome + "'  ");

            return sSql.ToString();
        }

        internal string AtualizarCliente(cliente Cliente)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" update tCliente ");
            sSql.Append(" set Nome = '" + Cliente.Nome + "', ");
            sSql.Append(" RG = '" + Cliente.RG + "', ");
            sSql.Append(" CPF = '" + Cliente.CPF + "', ");
            sSql.Append(" DataNascimento = '" + Cliente.DataNascimento + "', ");
            sSql.Append(" TelefoneResidencial = '" + Cliente.TelefoneResidencial + "', ");
            sSql.Append(" TelefoneCelular = '" + Cliente.TelefoneCelular + "', ");
            sSql.Append(" TelefoneComercial = '" + Cliente.TelefoneComercial + "', ");
            sSql.Append(" CEP = '" + Cliente.CEP + "', ");
            sSql.Append(" Logradouro = '" + Cliente.Logradouro + "', ");
            sSql.Append(" Numero = '" + Cliente.Numero + "', ");
            sSql.Append(" Complemento = '" + Cliente.Complemento + "', ");
            sSql.Append(" Bairro = '" + Cliente.Bairro + "', ");
            sSql.Append(" IDCidade = '" + Cliente.IDCidade + "' ");
            sSql.Append(" where IDUsuario = " + Cliente.IDUsuario + " ");

            sSql.Append(" update tUsuario ");
            sSql.Append(" set Nome = '" + Cliente.Nome + "' ");
            sSql.Append(" where IDUsuario = " + Cliente.IDUsuario + " ");

            return sSql.ToString();
        }

        internal string VerificarExistenciaCPF(string sCPF, int iIDUsuario)
        {
            string sSql = " SELECT IDCliente FROM tCliente WHERE CPF = '" + sCPF + "'";

            if (iIDUsuario != 0 && iIDUsuario != -1)
                sSql += " and IDUsuario != " + iIDUsuario + " ";

            return sSql;
        }

        internal string VerificarExistenciaCPF(string sCPF)
        {
            string sSql = " SELECT IDCliente FROM tCliente WHERE CPF = '" + sCPF + "'";
            return sSql;
        }

        internal string RetornarClientes(string sNome, string sCPF, string sEmail, int iNumeroCota, string sPerfil)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select top 50 u.IDUsuario, u.Nome,  ");
            sSql.Append(" c.CPF, u.Email, p.Perfil, c.NumeroCota [Núm. Cota], ");
            sSql.Append(" convert(varchar, u.Cadastro, 103) + ' ' + convert(varchar, u.Cadastro, 108) [Data Cadastro], ");
            sSql.Append(" sd.Debito ");
            sSql.Append(" from tUsuario u  ");
            sSql.Append(" left join tCliente c on u.IDUsuario = c.IDUsuario ");
            sSql.Append(" inner join tPerfil p on u.IDPerfil = p.IDPerfil ");
            sSql.Append(" left join vSocioDependentes sd on sd.Num_cota = c.NumeroCota ");
            sSql.Append(" where u.IDPerfil in ("+sPerfil+") ");

            if (!String.IsNullOrEmpty(sNome))
                sSql.Append(" and u.Nome like '%" + sNome + "%' COLLATE SQL_Latin1_General_CP1_CI_AI ");

            if (!String.IsNullOrEmpty(sCPF))
                sSql.Append(" and c.CPF like '%" + sCPF + "%' ");

            if (!String.IsNullOrEmpty(sEmail))
                sSql.Append(" and u.Email like '%" + sEmail + "%' ");

            if (iNumeroCota != 0 && iNumeroCota != -1)
                sSql.Append(" and c.NumeroCota = "+iNumeroCota+" ");

            sSql.Append(" order by u.IDUsuario desc ");

            return sSql.ToString();
        }

        internal string CadastrarAlteracaoDados(string sTipoRegistro, string sRegistroAntigo, string sRegistroAtual, int iIDCliente)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" insert into tHistoricoCadastroCliente (TipoRegistro,RegistroAntigo,RegistroAtual,IDCliente)values ");
            sSql.Append(" ('" + sTipoRegistro + "','" + sRegistroAntigo + "','" + sRegistroAtual + "'," + iIDCliente + ")");

            return sSql.ToString();

        }

        internal string RetornarAlteracoesDados(int iIDCliente)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" Select IDcliente, TipoRegistro, RegistroAntigo, RegistroAtual, Cadastro [Cadastro] from tHistoricoCadastroCliente ");
            sSql.Append(" where IDCliente = " + iIDCliente + " order by IDHistorico desc ");

            return sSql.ToString();

        }
    }
}

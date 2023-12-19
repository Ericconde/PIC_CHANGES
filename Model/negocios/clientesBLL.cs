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
    public class clientesBLL
    {
        public void CadastrarCliente(cliente Cliente)
        {
            clientesDAL DClientes = new clientesDAL();
            string sSql = DClientes.CadastrarCliente(Cliente);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }       

        public string RetornarEstados()
        {
            clientesDAL DClientes = new clientesDAL();
            string sSql = DClientes.RetornarEstados();

            return sSql;
        }

        public string RetornarCidades(int iIDEstado)
        {
            clientesDAL DClientes = new clientesDAL();
            string sSql = DClientes.RetornarCidades(iIDEstado);

            return sSql;
        }

        public cliente RetornarCliente(int iIDUsuario)
        {
            clientesDAL DClientes = new clientesDAL();
            string sSql = DClientes.RetornarCliente(iIDUsuario);
            DataTable dataTable = PontoBr.Banco.SqlServer.RetornarDataTable(sSql);

            cliente Cliente = new cliente();

            if (dataTable.Rows.Count > 0)
            {
                Cliente.Estado = dataTable.Rows[0]["Estado"].ToString();
                Cliente.NumeroCota = Convert.ToInt32(dataTable.Rows[0]["NumeroCota"].ToString());
                Cliente.Cidade = dataTable.Rows[0]["Cidade"].ToString();
                Cliente.IDCliente = Convert.ToInt32(dataTable.Rows[0]["IDCliente"].ToString());
                Cliente.IDUsuario = Convert.ToInt32(dataTable.Rows[0]["IDUsuario"].ToString());
                Cliente.Email = dataTable.Rows[0]["Email"].ToString();
                Cliente.Nome = dataTable.Rows[0]["Nome"].ToString();
                Cliente.RG = dataTable.Rows[0]["RG"].ToString();
                Cliente.CPF = dataTable.Rows[0]["CPF"].ToString();
                Cliente.DataNascimento = dataTable.Rows[0]["DataNascimento"].ToString();
                Cliente.Senha = dataTable.Rows[0]["Senha"].ToString();
                Cliente.TelefoneResidencial = Convert.ToDouble(dataTable.Rows[0]["TelefoneResidencial"].ToString());
                Cliente.TelefoneComercial = Convert.ToDouble(dataTable.Rows[0]["TelefoneComercial"].ToString());
                Cliente.TelefoneCelular = Convert.ToDouble(dataTable.Rows[0]["TelefoneCelular"].ToString());
                Cliente.CEP = dataTable.Rows[0]["Cep"].ToString();
                Cliente.Logradouro = dataTable.Rows[0]["Logradouro"].ToString();
                Cliente.Numero = dataTable.Rows[0]["Numero"].ToString();
                Cliente.Complemento = dataTable.Rows[0]["Complemento"].ToString();
                Cliente.Bairro = dataTable.Rows[0]["Bairro"].ToString();
                Cliente.IDCidade = Convert.ToInt32(dataTable.Rows[0]["IDCidade"].ToString());
                Cliente.IDEstado = Convert.ToInt32(dataTable.Rows[0]["IDEstado"].ToString());

                Cliente.Debito = !String.IsNullOrEmpty(dataTable.Rows[0]["Debito"].ToString()) ? Convert.ToInt32(dataTable.Rows[0]["Debito"]) : 0;
                Cliente.IDPerfil = Convert.ToInt32(dataTable.Rows[0]["IDPerfil"].ToString());
            }
            return Cliente;
        }

        public cliente RetornarCliente(string sNome)
        {
            clientesDAL DClientes = new clientesDAL();
            string sSql = DClientes.RetornarCliente(sNome);
            DataTable dataTable = PontoBr.Banco.SqlServer.RetornarDataTable(sSql);

            cliente Cliente = new cliente();

            if (dataTable.Rows.Count > 0)
            {
                Cliente.Estado = dataTable.Rows[0]["Estado"].ToString();
                Cliente.NumeroCota = Convert.ToInt32(dataTable.Rows[0]["NumeroCota"].ToString());
                Cliente.Cidade = dataTable.Rows[0]["Cidade"].ToString();
                Cliente.IDCliente = Convert.ToInt32(dataTable.Rows[0]["IDCliente"].ToString());
                Cliente.IDUsuario = Convert.ToInt32(dataTable.Rows[0]["IDUsuario"].ToString());
                Cliente.Email = dataTable.Rows[0]["Email"].ToString();
                Cliente.Nome = dataTable.Rows[0]["Nome"].ToString();
                Cliente.RG = dataTable.Rows[0]["RG"].ToString();
                Cliente.CPF = dataTable.Rows[0]["CPF"].ToString();
                Cliente.DataNascimento = dataTable.Rows[0]["DataNascimento"].ToString();
                Cliente.Senha = dataTable.Rows[0]["Senha"].ToString();
                Cliente.TelefoneResidencial = Convert.ToDouble(dataTable.Rows[0]["TelefoneResidencial"].ToString());
                Cliente.IDCidade = Convert.ToInt32(dataTable.Rows[0]["IDCidade"].ToString());
                Cliente.Debito = !String.IsNullOrEmpty(dataTable.Rows[0]["Debito"].ToString()) ? Convert.ToInt32(dataTable.Rows[0]["Debito"]) : 0;
                Cliente.IDPerfil = Convert.ToInt32(dataTable.Rows[0]["IDPerfil"].ToString());
            }
            return Cliente;
        }

        public void AtualizarCliente(cliente Cliente)
        {
            clientesDAL DClientes = new clientesDAL();
            string sSql = DClientes.AtualizarCliente(Cliente);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public bool VerificarExistenciaCPF(string sCPF, int iIDUsuario)
        {
            clientesDAL DClientes = new clientesDAL();
            string sSql = DClientes.VerificarExistenciaCPF(sCPF, iIDUsuario);

            return PontoBr.Banco.SqlServer.VerificarExistenciaDeDados(sSql);
        }

        public bool VerificarExistenciaCPF(string sCPF)
        {
            clientesDAL DClientes = new clientesDAL();
            string sSql = DClientes.VerificarExistenciaCPF(sCPF);

            return PontoBr.Banco.SqlServer.VerificarExistenciaDeDados(sSql);
        }

        public DataTable RetornarClientes(string sNome, string sCPF, string sEmail, int iNumeroCota, string sPerfil)
        {
            clientesDAL DClientes = new clientesDAL();
            string sSql = DClientes.RetornarClientes(sNome, sCPF, sEmail, iNumeroCota, sPerfil);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public void CadastrarAlteracaoDados(string sTipoRegistro, string sRegistroAntigo, string sRegistroAtual, int iIDCliente)
        {
            clientesDAL DClientes = new clientesDAL();
            PontoBr.Banco.SqlServer.ExecutarSql(DClientes.CadastrarAlteracaoDados(sTipoRegistro, sRegistroAntigo, sRegistroAtual, iIDCliente));
        }

        public DataTable RetornarAlteracoesDados(int iIDCliente)
        {
            clientesDAL DClientes = new clientesDAL();
            string sSql = DClientes.RetornarAlteracoesDados(iIDCliente);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);

        }
    }
}

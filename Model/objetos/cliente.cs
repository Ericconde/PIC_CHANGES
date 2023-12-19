using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.objetos
{
    public class cliente
    {
        private int _IDUsuario;
        public int IDUsuario
        {
            get { return _IDUsuario; }
            set { _IDUsuario = value; }
        }
        
        private string _Login;
        public string Login
        {
            get { return _Login; }
            set { _Login = value; }
        }

        private string _Senha;
        public string Senha
        {
            get { return _Senha; }
            set { _Senha = value; }
        }

        private int _IDPerfil;
        public int IDPerfil
        {
            get { return _IDPerfil; }
            set { _IDPerfil = value; }
        }
        
        private string _Perfil;
        public string Perfil
        {
            get { return _Perfil; }
            set { _Perfil = value; }
        }

        private string _Nome;
        public string Nome
        {
            get { return _Nome; }
            set { _Nome = value; }
        }

        private int _Ativo;
        public int Ativo
        {
            get { return _Ativo; }
            set { _Ativo = value; }
        }

        private string _CPF;
        public string CPF
        {
            get { return _CPF; }
            set { _CPF = value; }
        }

        private string _RG;
        public string RG
        {
            get { return _RG; }
            set { _RG = value; }
        }


        private string _DataCadastro;
        public string DataCadastro
        {
            get { return _DataCadastro; }
            set { _DataCadastro = value; }
        }

        private string _UsuarioAdministrador;
        public string UsuarioAdministrador
        {
            get { return _UsuarioAdministrador; }
            set { _UsuarioAdministrador = value; }
        }

        private string _DataNascimento;
        public string DataNascimento
        {
            get { return _DataNascimento; }
            set { _DataNascimento = value; }
        }

        private double _TelefoneComercial;
        public double TelefoneComercial
        {
            get { return _TelefoneComercial; }
            set { _TelefoneComercial = value; }
        }

        private double _TelefoneResidencial;
        public double TelefoneResidencial
        {
            get { return _TelefoneResidencial; }
            set { _TelefoneResidencial = value; }
        }

        private double _TelefoneCelular;
        public double TelefoneCelular
        {
            get { return _TelefoneCelular; }
            set { _TelefoneCelular = value; }
        }

        private string _Logradouro;
        public string Logradouro
        {
            get { return _Logradouro; }
            set { _Logradouro = value; }
        }

        private string _Numero;
        public string Numero
        {
            get { return _Numero; }
            set { _Numero = value; }
        }

        private string _Complemento;
        public string Complemento
        {
            get { return _Complemento; }
            set { _Complemento = value; }
        }

        private string _Bairro;
        public string Bairro
        {
            get { return _Bairro; }
            set { _Bairro = value; }
        }

        private int _IDCidade;
        public int IDCidade
        {
            get { return _IDCidade; }
            set { _IDCidade = value; }
        }

        private string _Cidade;
        public string Cidade
        {
            get { return _Cidade; }
            set { _Cidade = value; }
        }

        private string _CEP;
        public string CEP
        {
            get { return _CEP; }
            set { _CEP = value; }
        }

        private int _NumCota;
        public int NumCota
        {
            get { return _NumCota; }
            set { _NumCota = value; }
        }

        private string _DigitoCota; public string DigitoCota { get { return _DigitoCota; } set { _DigitoCota = value; } }

        private string _UF;
        public string UF
        {
            get { return _UF; }
            set { _UF = value; }
        }

        private int _Debito;
        public int Debito
        {
            get { return _Debito; }
            set { _Debito = value; }
        }

        private int _IDCliente;
        public int IDCliente
        {
            get { return _IDCliente; }
            set { _IDCliente = value; }
        }

        private int _IDEstado;
        public int IDEstado
        {
            get { return _IDEstado; }
            set { _IDEstado = value; }
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private int _NumeroCota;
        public int NumeroCota
        {
            get { return _NumeroCota; }
            set { _NumeroCota = value; }
        }

        private string _Estado;
        public string Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }
    }
}

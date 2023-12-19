using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.objetos
{
    public class usuario
    {
        private int _IDUsuario;
        public int IDUsuario
        {
            get { return _IDUsuario; }
            set { _IDUsuario = value; }
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

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
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

        private int _IDSexo;
        public int IDSexo
        {
            get { return _IDSexo; }
            set { _IDSexo = value; }
        }

        private string _DataCadastro;
        public string DataCadastro
        {
            get { return _DataCadastro; }
            set { _DataCadastro = value; }
        }

        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private string _Usuario;
        public string Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }

        private int _ResetarSenha;
        public int ResetarSenha
        {
            get { return _ResetarSenha; }
            set { _ResetarSenha = value; }
        }

        private int _IDCliente; public int IDCliente { get { return _IDCliente; } set { _IDCliente = value; } }
        private int _Debito; public int Debito { get { return _Debito; } set { _Debito = value; } }
        private int _Dependentes; public int Dependentes { get { return _Dependentes; } set { _Dependentes = value; } }

        private int _NumeroCota; public int NumeroCota { get { return _NumeroCota; } set { _NumeroCota = value; } }
        private string _DigitoCota; public string DigitoCota { get { return _DigitoCota; } set { _DigitoCota = value; } }

        private string _Token; public string Token { get { return _Token; } set { _Token = value; } }
    }
}

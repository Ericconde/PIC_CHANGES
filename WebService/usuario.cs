using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService
{
    public class usuario
    {
        private bool _autenticado; public bool autenticado { get { return _autenticado; } set { _autenticado = value; } }
        private int _idusuario; public int idusuario { get { return _idusuario; } set { _idusuario = value; } }
        private string _nome; public string nome { get { return _nome; } set { _nome = value; } }
        private string _mensagem; public string mensagem { get { return _mensagem; } set { _mensagem = value; } }
        private string _tipoingresso_a_ser_lido; public string tipoingresso_a_ser_lido { get { return _tipoingresso_a_ser_lido; } set { _tipoingresso_a_ser_lido = value; } }
    }
}
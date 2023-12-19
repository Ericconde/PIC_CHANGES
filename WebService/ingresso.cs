using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService
{
    public class ingresso
    {
        private bool _entradaliberada; public bool entradaliberada { get { return _entradaliberada; } set { _entradaliberada = value; } }
        private string _usuario; public string usuario { get { return _usuario; } set { _usuario = value; } }
        private string _cliente; public string cliente { get { return _cliente; } set { _cliente = value; } }
        private string _datahoraentrada; public string datahoraentrada { get { return _datahoraentrada; } set { _datahoraentrada = value; } }
        private string _qrcode; public string qrcode { get { return _qrcode; } set { _qrcode = value; } }
        private string _mensagem; public string mensagem { get { return _mensagem; } set { _mensagem = value; } }
        private string _tipo; public string tipo { get { return _tipo; } set { _tipo = value; } }
        private string _tipoingresso_a_ser_lido; public string tipoingresso_a_ser_lido { get { return _tipoingresso_a_ser_lido; } set { _tipoingresso_a_ser_lido = value; } }
    }
}
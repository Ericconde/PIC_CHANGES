using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.objetos
{
    public class socio
    {
        private int _Num_Cota; public int Num_Cota { get { return _Num_Cota; } set { _Num_Cota = value; } }
        private string _Digito; public string Digito { get { return _Digito; } set { _Digito = value; } }
        private string _Nome; public string Nome { get { return _Nome; } set { _Nome = value; } }
        private string _Dat_Nasc; public string Dat_Nasc { get { return _Dat_Nasc; } set { _Dat_Nasc = value; } }
        private string _CPF; public string CPF { get { return _CPF; } set { _CPF = value; } }
        private string _RG; public string RG { get { return _RG; } set { _RG = value; } }
        private string _Email; public string Email { get { return _Email; } set { _Email = value; } }
        private string _fone1; public string fone1 { get { return _fone1; } set { _fone1 = value; } }
        private string _fone2; public string fone2 { get { return _fone2; } set { _fone2 = value; } }
        private string _Logradouro; public string Logradouro { get { return _Logradouro; } set { _Logradouro = value; } }
        private string _Numero; public string Numero { get { return _Numero; } set { _Numero = value; } }
        private string _Complemento; public string Complemento { get { return _Complemento; } set { _Complemento = value; } }
        private string _Bairro; public string Bairro { get { return _Bairro; } set { _Bairro = value; } }
        private string _CEP; public string CEP { get { return _CEP; } set { _CEP = value; } }
        private string _Nom_Cidade; public string Nom_Cidade { get { return _Nom_Cidade; } set { _Nom_Cidade = value; } }
        private string _Nom_Estado; public string Nom_Estado { get { return _Nom_Estado; } set { _Nom_Estado = value; } }
        private string _Abrev_Estado; public string Abrev_Estado { get { return _Abrev_Estado; } set { _Abrev_Estado = value; } }
        private int _Debito; public int Debito { get { return _Debito; } set { _Debito = value; } }
    }
}

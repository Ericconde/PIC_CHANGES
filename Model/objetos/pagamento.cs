using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.objetos
{
    public class pagamento
    {
        private string _NumeroDocumento; public string NumeroDocumento { get { return _NumeroDocumento; } set { _NumeroDocumento = value; } }
        private int _IDUsuario; public int IDUsuario { get { return _IDUsuario; } set { _IDUsuario = value; } }
        private string _NumeroCartao; public string NumeroCartao { get { return _NumeroCartao; } set { _NumeroCartao = value; } }
        private string _Titular; public string Titular { get { return _Titular; } set { _Titular = value; } }
        private string _CodigoSeguranca; public string CodigoSeguranca { get { return _CodigoSeguranca; } set { _CodigoSeguranca = value; } }
        private string _Validade; public string Validade { get { return _Validade; } set { _Validade = value; } }
        private string _Bandeira; public string Bandeira { get { return _Bandeira; } set { _Bandeira = value; } }
        private int _Parcelas; public int Parcelas { get { return _Parcelas; } set { _Parcelas = value; } }
        private Double _Valor; public Double Valor { get { return _Valor; } set { _Valor = value; } }
        private string _PaymentId; public string PaymentId { get { return _PaymentId; } set { _PaymentId = value; } }
        private string _tid; public string tid { get { return _tid; } set { _tid = value; } }
        private int _Aprovada; public int Aprovada { get { return _Aprovada; } set { _Aprovada = value; } }
        private DateTime _Cadastro; public DateTime Cadastro { get { return _Cadastro; } set { _Cadastro = value; } }
    }
}

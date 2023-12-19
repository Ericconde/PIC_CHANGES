using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.objetos
{
    public class saldo
    {
        private int _QuantidadeCarrinhoVendaAtual; public int QuantidadeCarrinhoVendaAtual { get { return _QuantidadeCarrinhoVendaAtual; } set { _QuantidadeCarrinhoVendaAtual = value; } }
        private int _QuantidadeComprado; public int QuantidadeComprado { get { return _QuantidadeComprado; } set { _QuantidadeComprado = value; } }
        private int _QuantidadeCadeiraVendaAtual; public int QuantidadeCadeiraVendaAtual { get { return _QuantidadeCadeiraVendaAtual; } set { _QuantidadeCadeiraVendaAtual = value; } }
        private int _QuantidadeCadeiraComprado; public int QuantidadeCadeiraComprado { get { return _QuantidadeCadeiraComprado; } set { _QuantidadeCadeiraComprado = value; } }
        private int _QuantidadeValorSocioVendaAtual; public int QuantidadeValorSocioVendaAtual { get { return _QuantidadeValorSocioVendaAtual; } set { _QuantidadeValorSocioVendaAtual = value; } }
        private int _QuantidadeValorNaoSocioVendaAtual; public int QuantidadeValorNaoSocioVendaAtual { get { return _QuantidadeValorNaoSocioVendaAtual; } set { _QuantidadeValorNaoSocioVendaAtual = value; } }
        private int _QuantidadeValorSocioComprado; public int QuantidadeValorSocioComprado { get { return _QuantidadeValorSocioComprado; } set { _QuantidadeValorSocioComprado = value; } }
        private int _QuantidadeValorNaoSocioComprado; public int QuantidadeValorNaoSocioComprado { get { return _QuantidadeValorNaoSocioComprado; } set { _QuantidadeValorNaoSocioComprado = value; } }
        private int _QuantidadeAvulsoVendaAtual; public int QuantidadeAvulsoVendaAtual { get { return _QuantidadeAvulsoVendaAtual; } set { _QuantidadeAvulsoVendaAtual = value; } }
        private int _QuantidadeCamaroteVendaAtual; public int QuantidadeCamaroteVendaAtual { get { return _QuantidadeCamaroteVendaAtual; } set { _QuantidadeCamaroteVendaAtual = value; } }
    }
    
}

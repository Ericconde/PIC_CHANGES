using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.objetos
{
    public class venda
    {
        private string _Evento; public string Evento { get { return _Evento; } set { _Evento = value; } }
        private string _Setor; public string Setor { get { return _Setor; } set { _Setor = value; } }
        private int _Mesa; public int Mesa { get { return _Mesa; } set { _Mesa = value; } }
        private int _Cadeira; public int Cadeira { get { return _Cadeira; } set { _Cadeira = value; } }
        private int _IDVenda; public int IDVenda { get { return _IDVenda; } set { _IDVenda = value; } }
        private int _IDTipoIngresso; public int IDTipoIngresso { get { return _IDTipoIngresso; } set { _IDTipoIngresso = value; } }
        private string _TipoIngresso; public string TipoIngresso { get { return _TipoIngresso; } set { _TipoIngresso = value; } }
        private double _Valor; public double Valor { get { return _Valor; } set { _Valor = value; } }
        private int _IDEdicao; public int IDEdicao { get { return _IDEdicao; } set { _IDEdicao = value; } }
        private double _Ticket; public double Ticket { get { return _Ticket; } set { _Ticket = value; } }
        private int _IDCadeira; public int IDCadeira { get { return _IDCadeira; } set { _IDCadeira = value; } }
        private int _IDStatusCadeira; public int IDStatusCadeira { get { return _IDStatusCadeira; } set { _IDStatusCadeira = value; } }
        private string _IP; public string IP { get { return _IP; } set { _IP = value; } }
        private int _IDCliente; public int IDCliente { get { return _IDCliente; } set { _IDCliente = value; } }
        private int _IDUsuario; public int IDUsuario { get { return _IDUsuario; } set { _IDUsuario = value; } }
        private int _IDUsuarioCadastro; public int IDUsuarioCadastro { get { return _IDUsuarioCadastro; } set { _IDUsuarioCadastro = value; } }

        private int _NumeroIngressoExtraSocioCota; public int NumeroIngressoExtraSocioCota { get { return _NumeroIngressoExtraSocioCota; } set { _NumeroIngressoExtraSocioCota = value; } }
        private int _NumeroIngressoExtraSocioTitulo; public int NumeroIngressoExtraSocioTitulo { get { return _NumeroIngressoExtraSocioTitulo; } set { _NumeroIngressoExtraSocioTitulo = value; } }
        private int _NumeroIngressosValorNaoSocio; public int NumeroIngressosValorNaoSocio { get { return _NumeroIngressosValorNaoSocio; } set { _NumeroIngressosValorNaoSocio = value; } }
        private int _NumeroIngressosCadeira; public int NumeroIngressosCadeira { get { return _NumeroIngressosCadeira; } set { _NumeroIngressosCadeira = value; } }

        private double _ValorInteiraCadeiraSocio; public double ValorInteiraCadeiraSocio { get { return _ValorInteiraCadeiraSocio; } set { _ValorInteiraCadeiraSocio = value; } }
        private double _ValorInteiraCadeiraNaoSocio; public double ValorInteiraCadeiraNaoSocio { get { return _ValorInteiraCadeiraNaoSocio; } set { _ValorInteiraCadeiraNaoSocio = value; } }
        private double _ValorInteiraAvulsoSocio; public double ValorInteiraAvulsoSocio { get { return _ValorInteiraAvulsoSocio; } set { _ValorInteiraAvulsoSocio = value; } }
        private double _ValorInteiraAvulsoNaoSocio; public double ValorInteiraAvulsoNaoSocio { get { return _ValorInteiraAvulsoNaoSocio; } set { _ValorInteiraAvulsoNaoSocio = value; } }
        private double _ValorMeiaCadeiraSocio; public double ValorMeiaCadeiraSocio { get { return _ValorMeiaCadeiraSocio; } set { _ValorMeiaCadeiraSocio = value; } }
        private double _ValorMeiaCadeiraNaoSocio; public double ValorMeiaCadeiraNaoSocio { get { return _ValorMeiaCadeiraNaoSocio; } set { _ValorMeiaCadeiraNaoSocio = value; } }
        private double _ValorMeiaAvulsoSocio; public double ValorMeiaAvulsoSocio { get { return _ValorMeiaAvulsoSocio; } set { _ValorMeiaAvulsoSocio = value; } }
        private double _ValorMeiaAvulsoNaoSocio; public double ValorMeiaAvulsoNaoSocio { get { return _ValorMeiaAvulsoNaoSocio; } set { _ValorMeiaAvulsoNaoSocio = value; } }
        private double _ValorCamaroteSocio; public double ValorCamaroteSocio { get { return _ValorCamaroteSocio; } set { _ValorCamaroteSocio = value; } }
        private double _ValorCamaroteNaoSocio; public double ValorCamaroteNaoSocio { get { return _ValorCamaroteNaoSocio; } set { _ValorCamaroteNaoSocio = value; } }

        //para Boate
        private double _ValorSocioMasculino; public double ValorSocioMasculino { get { return _ValorSocioMasculino; } set { _ValorSocioMasculino = value; } }
        private double _ValorSocioFeminino; public double ValorSocioFeminino { get { return _ValorSocioFeminino; } set { _ValorSocioFeminino = value; } }
        private double _ValorNaoSocioMasculino; public double ValorNaoSocioMasculino { get { return _ValorNaoSocioMasculino; } set { _ValorNaoSocioMasculino = value; } }
        private double _ValorNaoSocioFeminino; public double ValorNaoSocioFeminino { get { return _ValorNaoSocioFeminino; } set { _ValorNaoSocioFeminino = value; } }

        private int _InteiraCadeiraSocio; public int InteiraCadeiraSocio { get { return _InteiraCadeiraSocio; } set { _InteiraCadeiraSocio = value; } }
        private int _InteiraCadeiraNaoSocio; public int InteiraCadeiraNaoSocio { get { return _InteiraCadeiraNaoSocio; } set { _InteiraCadeiraNaoSocio = value; } }
        private int _InteiraAvulsoSocio; public int InteiraAvulsoSocio { get { return _InteiraAvulsoSocio; } set { _InteiraAvulsoSocio = value; } }
        private int _InteiraAvulsoNaoSocio; public int InteiraAvulsoNaoSocio { get { return _InteiraAvulsoNaoSocio; } set { _InteiraAvulsoNaoSocio = value; } }
        private int _MeiaCadeiraSocio; public int MeiaCadeiraSocio { get { return _MeiaCadeiraSocio; } set { _MeiaCadeiraSocio = value; } }
        private int _MeiaCadeiraNaoSocio; public int MeiaCadeiraNaoSocio { get { return _MeiaCadeiraNaoSocio; } set { _MeiaCadeiraNaoSocio = value; } }
        private int _MeiaAvulsoSocio; public int MeiaAvulsoSocio { get { return _MeiaAvulsoSocio; } set { _MeiaAvulsoSocio = value; } }
        private int _MeiaAvulsoNaoSocio; public int MeiaAvulsoNaoSocio { get { return _MeiaAvulsoNaoSocio; } set { _MeiaAvulsoNaoSocio = value; } }
        private int _CamaroteSocio; public int CamaroteSocio { get { return _CamaroteSocio; } set { _CamaroteSocio = value; } }
        private int _CamaroteNaoSocio; public int CamaroteNaoSocio { get { return _CamaroteNaoSocio; } set { _CamaroteNaoSocio = value; } }

        //para Boate
        private int _SocioMasculino; public int SocioMasculino { get { return _SocioMasculino; } set { _SocioMasculino = value; } }
        private int _SocioFeminino; public int SocioFeminino { get { return _SocioFeminino; } set { _SocioFeminino = value; } }
        private int _NaoSocioMasculino; public int NaoSocioMasculino { get { return _NaoSocioMasculino; } set { _NaoSocioMasculino = value; } }
        private int _NaoSocioFeminino; public int NaoSocioFeminino { get { return _NaoSocioFeminino; } set { _NaoSocioFeminino = value; } }

        private bool _InteiraCadeira; public bool InteiraCadeira { get { return _InteiraCadeira; } set { _InteiraCadeira = value; } }
        private bool _MasculinoCadeira; public bool MasculinoCadeira { get { return _MasculinoCadeira; } set { _MasculinoCadeira = value; } }
        private int _Lote; public int Lote { get { return _Lote; } set { _Lote = value; } }
        private int _IDLote; public int IDLote { get { return _IDLote; } set { _IDLote = value; } }

        private string _Nome; public string Nome { get { return _Nome; } set { _Nome = value; } }
        private string _CPF; public string CPF { get { return _CPF; } set { _CPF = value; } }
        private int _NumeroCota; public int NumeroCota { get { return _NumeroCota; } set { _NumeroCota = value; } }
        private string _DataHoraCompra; public string DataHoraCompra { get { return _DataHoraCompra; } set { _DataHoraCompra = value; } }
        private string _FormaPagamento; public string FormaPagamento { get { return _FormaPagamento; } set { _FormaPagamento = value; } }
        private string _DetalhesFormaPagamento; public string DetalhesFormaPagamento { get { return _DetalhesFormaPagamento; } set { _DetalhesFormaPagamento = value; } }
        private string _StatusCompra; public string StatusCompra { get { return _StatusCompra; } set { _StatusCompra = value; } }
        private string _Edicao; public string Edicao { get { return _Edicao; } set { _Edicao = value; } }
        private string _DataHoraEmpresa; public string DataHoraEmpresa { get { return _DataHoraEmpresa; } set { _DataHoraEmpresa = value; } }
        private string _ResponsavelEntrega; public string ResponsavelEntrega { get { return _ResponsavelEntrega; } set { _ResponsavelEntrega = value; } }
        private int _IDStatusCompra; public int IDStatusCompra { get { return _IDStatusCompra; } set { _IDStatusCompra = value; } }
        private decimal _ValorTotal; public decimal ValorTotal { get { return _ValorTotal; } set { _ValorTotal = value; } }

        private int _NumeroVagas; public int NumeroVagas { get { return _NumeroVagas; } set { _NumeroVagas = value; } }
        private int _NumeroIngressos; public int NumeroIngressos { get { return _NumeroIngressos; } set { _NumeroIngressos = value; } }

        private int _IngressosPrecoSocio; public int IngressosPrecoSocio { get { return _IngressosPrecoSocio; } set { _IngressosPrecoSocio = value; } }
        private int _IngressosPrecoNaoSocio; public int IngressosPrecoNaoSocio { get { return _IngressosPrecoNaoSocio; } set { _IngressosPrecoNaoSocio = value; } }

        private string _ResponsavelCompra; public string ResponsavelCompra { get { return _ResponsavelCompra; } set { _ResponsavelCompra = value; } }
        private bool _Declaracao; public bool Declaracao { get { return _Declaracao; } set { _Declaracao = value; } }
        private bool _Aniversariante; public bool Aniversariante { get { return _Aniversariante; } set { _Aniversariante = value; } }
        private int _NumeroCriancas; public int NumeroCriancas { get { return _NumeroCriancas; } set { _NumeroCriancas = value; } }
        private int _IDLocalRetirada; public int IDLocalRetirada { get { return _IDLocalRetirada; } set { _IDLocalRetirada = value; } }
        private string _NomeResponsavelRetirada; public string NomeResponsavelRetirada { get { return _NomeResponsavelRetirada; } set { _NomeResponsavelRetirada = value; } }
        private string _RgResponsavelRetirada; public string RgResponsavelRetirada { get { return _RgResponsavelRetirada; } set { _RgResponsavelRetirada = value; } }

        private string _NomeResponsavelRetirada2; public string NomeResponsavelRetirada2 { get { return _NomeResponsavelRetirada2; } set { _NomeResponsavelRetirada2 = value; } }
        private string _RgResponsavelRetirada2; public string RgResponsavelRetirada2 { get { return _RgResponsavelRetirada2; } set { _RgResponsavelRetirada2 = value; } }

        private string _UsuarioPerfilLogado; public string UsuarioPerfilLogado { get { return _UsuarioPerfilLogado; } set { _UsuarioPerfilLogado = value; } }

        private int _TotalCadeirasEvento; public int TotalCadeirasEvento { get { return _TotalCadeirasEvento; } set { _TotalCadeirasEvento = value; } }

        private double _ValorIpanema; public double ValorIpanema { get { return _ValorIpanema; } set { _ValorIpanema = value; } }
        private double _ValorGolodromo; public double ValorGolodromo { get { return _ValorGolodromo; } set { _ValorGolodromo = value; } }
        private double _ValorPortinari; public double ValorPortinari { get { return _ValorPortinari; } set { _ValorPortinari = value; } }
        private double _ValorPergula; public double ValorPergula { get { return _ValorPergula; } set { _ValorPergula = value; } }
        private double _ValorSalaoDeFestas; public double ValorSalaoDeFestas { get { return _ValorSalaoDeFestas; } set { _ValorSalaoDeFestas = value; } }
        private double _ValorAcademia; public double ValorAcademia { get { return _ValorAcademia; } set { _ValorAcademia = value; } }
    }
    
}

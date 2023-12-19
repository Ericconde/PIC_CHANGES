using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.objetos
{
    public class evento
    {
        private int _IDEdicao; public int IDEdicao { get { return _IDEdicao; } set { _IDEdicao = value; } }
        private int _IDLote; public int IDLote { get { return _IDLote; } set { _IDLote = value; } }
        private int _IDEvento; public int IDEvento { get { return _IDEvento; } set { _IDEvento = value; } }
        private string _Evento; public string Evento { get { return _Evento; } set { _Evento = value; } }
        private string _Edicao; public string Edicao { get { return _Edicao; } set { _Edicao = value; } }
        private string _DataHoraEvento; public string DataHoraEvento { get { return _DataHoraEvento; } set { _DataHoraEvento = value; } }
        private string _InicioVendaNaoSocio; public string InicioVendaNaoSocio { get { return _InicioVendaNaoSocio; } set { _InicioVendaNaoSocio = value; } }

        private string _InicioVenda; public string InicioVenda { get { return _InicioVenda; } set { _InicioVenda = value; } }
        private string _FimVenda; public string FimVenda { get { return _FimVenda; } set { _FimVenda = value; } }
        private string _DataInicioVenda; public string DataInicioVenda { get { return _DataInicioVenda; } set { _DataInicioVenda = value; } }
        private string _DataFimVenda; public string DataFimVenda { get { return _DataFimVenda; } set { _DataFimVenda = value; } }
        private string _HoraInicioVenda; public string HoraInicioVenda { get { return _HoraInicioVenda; } set { _HoraInicioVenda = value; } }
        private string _HoraFimVenda; public string HoraFimVenda { get { return _HoraFimVenda; } set { _HoraFimVenda = value; } }

        private int _Lote; public int Lote { get { return _Lote; } set { _Lote = value; } }
        private string _MapaEvento; public string MapaEvento { get { return _MapaEvento; } set { _MapaEvento = value; } }
        private int _NumeroIngressoExtraSocioCota; public int NumeroIngressoExtraSocioCota { get { return _NumeroIngressoExtraSocioCota; } set { _NumeroIngressoExtraSocioCota = value; } }
        private int _NumeroIngressoExtraSocioTitulo; public int NumeroIngressoExtraSocioTitulo { get { return _NumeroIngressoExtraSocioTitulo; } set { _NumeroIngressoExtraSocioTitulo = value; } }
        private int _NumeroIngressosValorNaoSocio; public int NumeroIngressosValorNaoSocio { get { return _NumeroIngressosValorNaoSocio; } set { _NumeroIngressosValorNaoSocio = value; } }
        private int _NumeroIngressosCadeira; public int NumeroIngressosCadeira { get { return _NumeroIngressosCadeira; } set { _NumeroIngressosCadeira = value; } }
        private int _NumeroIngressosCamarote; public int NumeroIngressosCamarote { get { return _NumeroIngressosCamarote; } set { _NumeroIngressosCamarote = value; } }

        private double _ValorInteiraCadeiraSocio; public double ValorInteiraCadeiraSocio { get { return _ValorInteiraCadeiraSocio; } set { _ValorInteiraCadeiraSocio = value; } }
        private double _ValorInteiraCadeiraNaoSocio; public double ValorInteiraCadeiraNaoSocio { get { return _ValorInteiraCadeiraNaoSocio; } set { _ValorInteiraCadeiraNaoSocio = value; } }
        private double _ValorInteiraAvulsoSocio; public double ValorInteiraAvulsoSocio { get { return _ValorInteiraAvulsoSocio; } set { _ValorInteiraAvulsoSocio = value; } }
        private double _ValorInteiraAvulsoNaoSocio; public double ValorInteiraAvulsoNaoSocio { get { return _ValorInteiraAvulsoNaoSocio; } set { _ValorInteiraAvulsoNaoSocio = value; } }
        private double _ValorMeiaCadeiraSocio; public double ValorMeiaCadeiraSocio { get { return _ValorMeiaCadeiraSocio; } set { _ValorMeiaCadeiraSocio = value; } }
        private double _ValorMeiaCadeiraNaoSocio; public double ValorMeiaCadeiraNaoSocio { get { return _ValorMeiaCadeiraNaoSocio; } set { _ValorMeiaCadeiraNaoSocio = value; } }
        private double _ValorMeiaAvulsoSocio; public double ValorMeiaAvulsoSocio { get { return _ValorMeiaAvulsoSocio; } set { _ValorMeiaAvulsoSocio = value; } }
        private double _ValorMeiaAvulsoNaoSocio; public double ValorMeiaAvulsoNaoSocio { get { return _ValorMeiaAvulsoNaoSocio; } set { _ValorMeiaAvulsoNaoSocio = value; } }

        //Para Boate
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

        //Para Boate
        private int _SocioMasculino; public int SocioMasculino { get { return _SocioMasculino; } set { _SocioMasculino = value; } }
        private int _SocioFeminino; public int SocioFeminino { get { return _SocioFeminino; } set { _SocioFeminino = value; } }
        private int _NaoSocioMasculino; public int NaoSocioMasculino { get { return _NaoSocioMasculino; } set { _NaoSocioMasculino = value; } }
        private int _NaoSocioFeminino; public int NaoSocioFeminino { get { return _NaoSocioFeminino; } set { _NaoSocioFeminino = value; } }

        private string _DataLimite4ParcelaCota; public string DataLimite4ParcelaCota { get { return _DataLimite4ParcelaCota; } set { _DataLimite4ParcelaCota = value; } }
        private string _DataLimite3ParcelaCota; public string DataLimite3ParcelaCota { get { return _DataLimite3ParcelaCota; } set { _DataLimite3ParcelaCota = value; } }
        private string _DataLimite2ParcelaCota; public string DataLimite2ParcelaCota { get { return _DataLimite2ParcelaCota; } set { _DataLimite2ParcelaCota = value; } }
        private string _DataLimite1ParcelaCota; public string DataLimite1ParcelaCota { get { return _DataLimite1ParcelaCota; } set { _DataLimite1ParcelaCota = value; } }
        private string _DataLimite5ParcelaCredito; public string DataLimite5ParcelaCredito { get { return _DataLimite5ParcelaCredito; } set { _DataLimite5ParcelaCredito = value; } }
        private string _DataLimite4ParcelaCredito; public string DataLimite4ParcelaCredito { get { return _DataLimite4ParcelaCredito; } set { _DataLimite4ParcelaCredito = value; } }
        private string _DataLimite3ParcelaCredito; public string DataLimite3ParcelaCredito { get { return _DataLimite3ParcelaCredito; } set { _DataLimite3ParcelaCredito = value; } }
        private string _DataLimite2ParcelaCredito; public string DataLimite2ParcelaCredito { get { return _DataLimite2ParcelaCredito; } set { _DataLimite2ParcelaCredito = value; } }
        private string _DataLimite1ParcelaCredito; public string DataLimite1ParcelaCredito { get { return _DataLimite1ParcelaCredito; } set { _DataLimite1ParcelaCredito = value; } }

        private DateTime _DateLimite4ParcelaCota; public DateTime DateLimite4ParcelaCota { get { return _DateLimite4ParcelaCota; } set { _DateLimite4ParcelaCota = value; } }
        private DateTime _DateLimite3ParcelaCota; public DateTime DateLimite3ParcelaCota { get { return _DateLimite3ParcelaCota; } set { _DateLimite3ParcelaCota = value; } }
        private DateTime _DateLimite2ParcelaCota; public DateTime DateLimite2ParcelaCota { get { return _DateLimite2ParcelaCota; } set { _DateLimite2ParcelaCota = value; } }
        private DateTime _DateLimite1ParcelaCota; public DateTime DateLimite1ParcelaCota { get { return _DateLimite1ParcelaCota; } set { _DateLimite1ParcelaCota = value; } }
        private DateTime _DateLimite5ParcelaCredito; public DateTime DateLimite5ParcelaCredito { get { return _DateLimite5ParcelaCredito; } set { _DateLimite5ParcelaCredito = value; } }
        private DateTime _DateLimite4ParcelaCredito; public DateTime DateLimite4ParcelaCredito { get { return _DateLimite4ParcelaCredito; } set { _DateLimite4ParcelaCredito = value; } }
        private DateTime _DateLimite3ParcelaCredito; public DateTime DateLimite3ParcelaCredito { get { return _DateLimite3ParcelaCredito; } set { _DateLimite3ParcelaCredito = value; } }
        private DateTime _DateLimite2ParcelaCredito; public DateTime DateLimite2ParcelaCredito { get { return _DateLimite2ParcelaCredito; } set { _DateLimite2ParcelaCredito = value; } }
        private DateTime _DateLimite1ParcelaCredito; public DateTime DateLimite1ParcelaCredito { get { return _DateLimite1ParcelaCredito; } set { _DateLimite1ParcelaCredito = value; } }
        
        private int _VagaEstacionamento; public int VagaEstacionamento { get { return _VagaEstacionamento; } set { _VagaEstacionamento = value; } }
        private string _VagaEstacionamento_descricao; public string VagaEstacionamento_descricao { get { return _VagaEstacionamento_descricao; } set { _VagaEstacionamento_descricao = value; } }
        private int _NumIngressoEstacionamento; public int NumIngressoEstacionamento { get { return _NumIngressoEstacionamento; } set { _NumIngressoEstacionamento = value; } }
        private int _VagasEstacionamento; public int VagasEstacionamento { get { return _VagasEstacionamento; } set { _VagasEstacionamento = value; } }
        
        private string _Cadastro; public string Cadastro { get { return _Cadastro; } set { _Cadastro = value; } }
        private int _IDUsuarioCadastro; public int IDUsuarioCadastro { get { return _IDUsuarioCadastro; } set { _IDUsuarioCadastro = value; } }
        private string _Alteracao; public string Alteracao { get { return _Alteracao; } set { _Alteracao = value; } }
        private int _IDUsuarioAlteracao; public int IDUsuarioAlteracao { get { return _IDUsuarioAlteracao; } set { _IDUsuarioAlteracao = value; } }
        private int _Ativo; public int Ativo { get { return _Ativo; } set { _Ativo = value; } }
        private int _IDStatusIngresso; public int IDStatusIngresso { get { return _IDStatusIngresso; } set { _IDStatusIngresso = value; } } 

        private int _IngressosCadeira; public int IngressosCadeira { get { return _IngressosCadeira; } set { _IngressosCadeira = value; } }
        private int _IngressosAvulsos; public int IngressosAvulsos { get { return _IngressosAvulsos; } set { _IngressosAvulsos = value; } }

        private int _MinimoAniversariante; public int MinimoAniversariante { get { return _MinimoAniversariante; } set { _MinimoAniversariante = value; } }

        private string _Detalhes; public string Detalhes { get { return _Detalhes; } set { _Detalhes = value; } }
        private string _DataRetirada; public string DataRetirada { get { return _DataRetirada; } set { _DataRetirada = value; } }

        /////////////////////////////////////////////////////////////

        private int _IDCadeira; public int IDCadeira { get { return _IDCadeira; } set { _IDCadeira = value; } }
        private int _IDStatusCadeira; public int IDStatusCadeira { get { return _IDStatusCadeira; } set { _IDStatusCadeira = value; } }

        private string _Status; public string Status { get { return _Status; } set { _Status = value; } }
        private string _Cadeira; public string Cadeira { get { return _Cadeira; } set { _Cadeira = value; } }
        private string _Mesa; public string Mesa { get { return _Mesa; } set { _Mesa = value; } }

        private double _ValorIpanema; public double ValorIpanema { get { return _ValorIpanema; } set { _ValorIpanema = value; } }
        private double _ValorGolodromo; public double ValorGolodromo { get { return _ValorGolodromo; } set { _ValorGolodromo = value; } }
        private double _ValorPortinari; public double ValorPortinari { get { return _ValorPortinari; } set { _ValorPortinari = value; } }
        private double _ValorPergula; public double ValorPergula { get { return _ValorPergula; } set { _ValorPergula = value; } }
        private double _ValorSalaoDeFestas; public double ValorSalaoDeFestas { get { return _ValorSalaoDeFestas; } set { _ValorSalaoDeFestas = value; } }
        private double _ValorAcademia; public double ValorAcademia { get { return _ValorAcademia; } set { _ValorAcademia = value; } }

        private int _PossuiMapa; public int PossuiMapa { get { return _PossuiMapa; } set { _PossuiMapa = value; } }
        private int _IDLocal; public int IDLocal { get { return _IDLocal; } set { _IDLocal = value; } }

        private int _CamaroteSocio; public int CamaroteSocio { get { return _CamaroteSocio; } set { _CamaroteSocio = value; } }
        private int _CamaroteNaoSocio; public int CamaroteNaoSocio { get { return _CamaroteNaoSocio; } set { _CamaroteNaoSocio = value; } }
        private double _ValorCamaroteSocio; public double ValorCamaroteSocio { get { return _ValorCamaroteSocio; } set { _ValorCamaroteSocio = value; } }
        private double _ValorCamaroteNaoSocio; public double ValorCamaroteNaoSocio { get { return _ValorCamaroteNaoSocio; } set { _ValorCamaroteNaoSocio = value; } }
    }    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.objetos;
using System.Data;
using System.Web;
using Model.negocios;
using System.Web.UI.WebControls;

namespace Controller
{
    public class eventoCTL
    {
        public void CadastrarEdicao(evento Evento)
        {
            eventoBLL BEvento = new eventoBLL();
            BEvento.CadastrarEdicao(Evento);
        }

        public void CadastrarLote(evento Evento)
        {
            eventoBLL BEvento = new eventoBLL();
            BEvento.CadastrarLote(Evento);
        }

        public void AtualizarEdicao(evento Evento)
        {
            eventoBLL BEvento = new eventoBLL();
            BEvento.AtualizarEdicao(Evento);
        }

        public void AtualizarLote(evento Evento)
        {
            eventoBLL BEvento = new eventoBLL();
            BEvento.AtualizarLote(Evento);
        }

        public evento RetornarEdicao(int iIDEdicao)
        {
            eventoBLL BEvento = new eventoBLL();
            DataTable dataTable = BEvento.RetornarEdicao(iIDEdicao);

            evento Evento = new evento();

            if (dataTable.Rows.Count > 0)
            {
                Evento.IDEdicao = Convert.ToInt32(dataTable.Rows[0]["IDEdicao"].ToString());
                Evento.IDEvento = Convert.ToInt32(dataTable.Rows[0]["IDEvento"].ToString());
                Evento.Evento = dataTable.Rows[0]["Evento"].ToString().Trim();
                Evento.Edicao = dataTable.Rows[0]["Edicao"].ToString().Trim();
                Evento.DataHoraEvento = dataTable.Rows[0]["DataHoraEvento"].ToString();
                Evento.InicioVendaNaoSocio = dataTable.Rows[0]["InicioVendaNaoSocio"].ToString();
                Evento.IDLocal = Convert.ToInt32(dataTable.Rows[0]["IDLocal"].ToString());

                Evento.MapaEvento = dataTable.Rows[0]["MapaEvento"].ToString();
                Evento.NumeroIngressoExtraSocioCota = Convert.ToInt32(dataTable.Rows[0]["NumeroIngressoExtraSocioCota"].ToString());
                Evento.NumeroIngressoExtraSocioTitulo = Convert.ToInt32(dataTable.Rows[0]["NumeroIngressoExtraSocioTitulo"].ToString());
                Evento.NumeroIngressosValorNaoSocio = Convert.ToInt32(dataTable.Rows[0]["NumeroIngressosValorNaoSocio"].ToString());
                Evento.NumeroIngressosCadeira = Convert.ToInt32(dataTable.Rows[0]["NumeroIngressosCadeira"].ToString());
                Evento.MinimoAniversariante = dataTable.Rows[0]["MinimoAniversariante"].ToString() == "" ? 0 : Convert.ToInt32(dataTable.Rows[0]["MinimoAniversariante"].ToString());
                Evento.NumeroIngressosCamarote = Convert.ToInt32(dataTable.Rows[0]["NumeroIngressosCamarote"].ToString());

                Evento.DataLimite4ParcelaCota = dataTable.Rows[0]["DataLimite4ParcelaCota"].ToString();
                Evento.DataLimite3ParcelaCota = dataTable.Rows[0]["DataLimite3ParcelaCota"].ToString();
                Evento.DataLimite2ParcelaCota = dataTable.Rows[0]["DataLimite2ParcelaCota"].ToString();
                Evento.DataLimite1ParcelaCota = dataTable.Rows[0]["DataLimite1ParcelaCota"].ToString();

                Evento.DataLimite5ParcelaCredito = dataTable.Rows[0]["DataLimite5ParcelaCredito"].ToString();
                Evento.DataLimite4ParcelaCredito = dataTable.Rows[0]["DataLimite4ParcelaCredito"].ToString();
                Evento.DataLimite3ParcelaCredito = dataTable.Rows[0]["DataLimite3ParcelaCredito"].ToString();
                Evento.DataLimite2ParcelaCredito = dataTable.Rows[0]["DataLimite2ParcelaCredito"].ToString();
                Evento.DataLimite1ParcelaCredito = dataTable.Rows[0]["DataLimite1ParcelaCredito"].ToString();

                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite4ParcelaCota"].ToString())) Evento.DateLimite4ParcelaCota = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite4ParcelaCota"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite3ParcelaCota"].ToString())) Evento.DateLimite3ParcelaCota = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite3ParcelaCota"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite2ParcelaCota"].ToString())) Evento.DateLimite2ParcelaCota = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite2ParcelaCota"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite1ParcelaCota"].ToString())) Evento.DateLimite1ParcelaCota = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite1ParcelaCota"].ToString() + " 23:59:59");

                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite5ParcelaCredito"].ToString())) Evento.DateLimite5ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite5ParcelaCredito"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite4ParcelaCredito"].ToString())) Evento.DateLimite4ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite4ParcelaCredito"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite3ParcelaCredito"].ToString())) Evento.DateLimite3ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite3ParcelaCredito"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite2ParcelaCredito"].ToString())) Evento.DateLimite2ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite2ParcelaCredito"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite1ParcelaCredito"].ToString())) Evento.DateLimite1ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite1ParcelaCredito"].ToString() + " 23:59:59");

                Evento.VagaEstacionamento = Convert.ToInt32(dataTable.Rows[0]["VagaEstacionamento"]);
                Evento.VagaEstacionamento_descricao = dataTable.Rows[0]["VagaEstacionamento_descricao"].ToString();
                Evento.NumIngressoEstacionamento = Convert.ToInt32(dataTable.Rows[0]["NumIngressoEstacionamento"].ToString());
                Evento.VagasEstacionamento = Convert.ToInt32(dataTable.Rows[0]["VagasEstacionamento"].ToString());
                Evento.Detalhes = dataTable.Rows[0]["Detalhes"].ToString();
                Evento.DataRetirada = dataTable.Rows[0]["DataRetirada"].ToString();
                Evento.Ativo = Convert.ToInt32(dataTable.Rows[0]["Ativo"]);
                Evento.IDStatusIngresso = Convert.ToInt32(dataTable.Rows[0]["IDStatusIngresso"]);
                Evento.PossuiMapa = Convert.ToInt32(dataTable.Rows[0]["PossuiMapa"]);
                Evento.Cadastro = dataTable.Rows[0]["Cadastro"].ToString();
                Evento.IDUsuarioCadastro = Convert.ToInt32(dataTable.Rows[0]["IDUsuarioCadastro"].ToString());
                Evento.Alteracao = dataTable.Rows[0]["Alteracao"].ToString();
                Evento.IDUsuarioAlteracao = dataTable.Rows[0]["IDUsuarioAlteracao"].ToString() == "" ? 0 : Convert.ToInt32(dataTable.Rows[0]["IDUsuarioAlteracao"].ToString());

                Evento.IngressosCadeira = String.IsNullOrEmpty(dataTable.Rows[0]["IngressosCadeira"].ToString()) ? 0 : Convert.ToInt32(dataTable.Rows[0]["IngressosCadeira"]);                
            }

            return Evento;
        }

        public evento RetornarEdicao(int iIDEvento, string sEdicao)
        {
            eventoBLL BEvento = new eventoBLL();
            DataTable dataTable = BEvento.RetornarEdicao(iIDEvento, sEdicao);

            evento Evento = new evento();

            if (dataTable.Rows.Count > 0)
            {
                Evento.IDEdicao = Convert.ToInt32(dataTable.Rows[0]["IDEdicao"].ToString());
                Evento.IDEvento = Convert.ToInt32(dataTable.Rows[0]["IDEvento"].ToString());
                Evento.Evento = dataTable.Rows[0]["Evento"].ToString();
                Evento.Edicao = dataTable.Rows[0]["Edicao"].ToString();
                Evento.DataHoraEvento = dataTable.Rows[0]["DataHoraEvento"].ToString();
                Evento.InicioVendaNaoSocio = dataTable.Rows[0]["InicioVendaNaoSocio"].ToString();
                Evento.IDLocal = Convert.ToInt32(dataTable.Rows[0]["IDLocal"].ToString());

                Evento.MapaEvento = dataTable.Rows[0]["MapaEvento"].ToString();
                Evento.NumeroIngressoExtraSocioCota = Convert.ToInt32(dataTable.Rows[0]["NumeroIngressoExtraSocioCota"].ToString());
                Evento.NumeroIngressoExtraSocioTitulo = Convert.ToInt32(dataTable.Rows[0]["NumeroIngressoExtraSocioTitulo"].ToString());
                Evento.NumeroIngressosValorNaoSocio = Convert.ToInt32(dataTable.Rows[0]["NumeroIngressosValorNaoSocio"].ToString());
                Evento.NumeroIngressosCadeira = Convert.ToInt32(dataTable.Rows[0]["NumeroIngressosCadeira"].ToString());

                Evento.DataLimite4ParcelaCota = dataTable.Rows[0]["DataLimite4ParcelaCota"].ToString();
                Evento.DataLimite3ParcelaCota = dataTable.Rows[0]["DataLimite3ParcelaCota"].ToString();
                Evento.DataLimite2ParcelaCota = dataTable.Rows[0]["DataLimite2ParcelaCota"].ToString();
                Evento.DataLimite1ParcelaCota = dataTable.Rows[0]["DataLimite1ParcelaCota"].ToString();

                Evento.DataLimite5ParcelaCredito = dataTable.Rows[0]["DataLimite5ParcelaCredito"].ToString();
                Evento.DataLimite4ParcelaCredito = dataTable.Rows[0]["DataLimite4ParcelaCredito"].ToString();
                Evento.DataLimite3ParcelaCredito = dataTable.Rows[0]["DataLimite3ParcelaCredito"].ToString();
                Evento.DataLimite2ParcelaCredito = dataTable.Rows[0]["DataLimite2ParcelaCredito"].ToString();
                Evento.DataLimite1ParcelaCredito = dataTable.Rows[0]["DataLimite1ParcelaCredito"].ToString();

                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite4ParcelaCota"].ToString())) Evento.DateLimite4ParcelaCota = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite4ParcelaCota"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite3ParcelaCota"].ToString())) Evento.DateLimite3ParcelaCota = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite3ParcelaCota"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite2ParcelaCota"].ToString())) Evento.DateLimite2ParcelaCota = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite2ParcelaCota"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite1ParcelaCota"].ToString())) Evento.DateLimite1ParcelaCota = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite1ParcelaCota"].ToString() + " 23:59:59");

                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite5ParcelaCredito"].ToString())) Evento.DateLimite5ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite5ParcelaCredito"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite4ParcelaCredito"].ToString())) Evento.DateLimite4ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite4ParcelaCredito"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite3ParcelaCredito"].ToString())) Evento.DateLimite3ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite3ParcelaCredito"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite2ParcelaCredito"].ToString())) Evento.DateLimite2ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite2ParcelaCredito"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite1ParcelaCredito"].ToString())) Evento.DateLimite1ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite1ParcelaCredito"].ToString() + " 23:59:59");

                Evento.VagaEstacionamento = Convert.ToInt32(dataTable.Rows[0]["VagaEstacionamento"]);
                Evento.VagaEstacionamento_descricao = dataTable.Rows[0]["VagaEstacionamento_descricao"].ToString();
                Evento.NumIngressoEstacionamento = Convert.ToInt32(dataTable.Rows[0]["NumIngressoEstacionamento"].ToString());
                Evento.VagasEstacionamento = Convert.ToInt32(dataTable.Rows[0]["VagasEstacionamento"].ToString());
                Evento.Detalhes = dataTable.Rows[0]["Detalhes"].ToString();
                Evento.DataRetirada = dataTable.Rows[0]["DataRetirada"].ToString();
                Evento.Ativo = Convert.ToInt32(dataTable.Rows[0]["Ativo"]);
                Evento.Cadastro = dataTable.Rows[0]["Cadastro"].ToString();
                Evento.IDUsuarioCadastro = Convert.ToInt32(dataTable.Rows[0]["IDUsuarioCadastro"].ToString());
                Evento.Alteracao = dataTable.Rows[0]["Alteracao"].ToString();
                Evento.IDUsuarioAlteracao = dataTable.Rows[0]["IDUsuarioAlteracao"].ToString() == "" ? 0 : Convert.ToInt32(dataTable.Rows[0]["IDUsuarioAlteracao"].ToString());

                Evento.IngressosCadeira = String.IsNullOrEmpty(dataTable.Rows[0]["IngressosCadeira"].ToString()) ? 0 : Convert.ToInt32(dataTable.Rows[0]["IngressosCadeira"]);
            }

            return Evento;
        }

        public evento RetornarLote(int iIDLote)
        {
            eventoBLL BEvento = new eventoBLL();
            DataTable dataTable = BEvento.RetornarLote(iIDLote);

            evento Evento = new evento();

            if (dataTable.Rows.Count > 0)
            {
                Evento.IDLote = Convert.ToInt32(dataTable.Rows[0]["IDLote"].ToString());
                Evento.IDEdicao = Convert.ToInt32(dataTable.Rows[0]["IDEdicao"].ToString());
                Evento.IDEvento = Convert.ToInt32(dataTable.Rows[0]["IDEvento"].ToString());
                Evento.Edicao = dataTable.Rows[0]["Edicao"].ToString();
                Evento.DataHoraEvento = dataTable.Rows[0]["DataHoraEvento"].ToString();

                Evento.InicioVenda = dataTable.Rows[0]["InicioVenda"].ToString();
                Evento.FimVenda = dataTable.Rows[0]["FimVenda"].ToString();
                Evento.DataInicioVenda = dataTable.Rows[0]["DataInicioVenda"].ToString();
                Evento.DataFimVenda = dataTable.Rows[0]["DataFimVenda"].ToString();
                Evento.HoraInicioVenda = dataTable.Rows[0]["HoraInicioVenda"].ToString();
                Evento.HoraFimVenda = dataTable.Rows[0]["HoraFimVenda"].ToString();

                Evento.Lote = Convert.ToInt32(dataTable.Rows[0]["Lote"].ToString());
                Evento.MapaEvento = dataTable.Rows[0]["MapaEvento"].ToString();
                Evento.NumeroIngressoExtraSocioCota = Convert.ToInt32(dataTable.Rows[0]["NumeroIngressoExtraSocioCota"].ToString());
                Evento.NumeroIngressoExtraSocioTitulo = Convert.ToInt32(dataTable.Rows[0]["NumeroIngressoExtraSocioTitulo"].ToString());
                Evento.NumeroIngressosValorNaoSocio = Convert.ToInt32(dataTable.Rows[0]["NumeroIngressosValorNaoSocio"].ToString());
                Evento.NumeroIngressosCadeira = Convert.ToInt32(dataTable.Rows[0]["NumeroIngressosCadeira"].ToString());
                Evento.NumeroIngressosCamarote = Convert.ToInt32(dataTable.Rows[0]["NumeroIngressosCamarote"].ToString());

                Evento.ValorInteiraCadeiraSocio = dataTable.Rows[0]["ValorInteiraCadeiraSocio"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorInteiraCadeiraSocio"].ToString());
                Evento.ValorInteiraCadeiraNaoSocio = dataTable.Rows[0]["ValorInteiraCadeiraNaoSocio"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorInteiraCadeiraNaoSocio"].ToString());
                Evento.ValorInteiraAvulsoSocio = dataTable.Rows[0]["ValorInteiraAvulsoSocio"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorInteiraAvulsoSocio"].ToString());
                Evento.ValorInteiraAvulsoNaoSocio = dataTable.Rows[0]["ValorInteiraAvulsoNaoSocio"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorInteiraAvulsoNaoSocio"].ToString());
                Evento.ValorMeiaCadeiraSocio = dataTable.Rows[0]["ValorMeiaCadeiraSocio"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorMeiaCadeiraSocio"].ToString());
                Evento.ValorMeiaCadeiraNaoSocio = dataTable.Rows[0]["ValorMeiaCadeiraNaoSocio"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorMeiaCadeiraNaoSocio"].ToString());
                Evento.ValorMeiaAvulsoSocio = dataTable.Rows[0]["ValorMeiaAvulsoSocio"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorMeiaAvulsoSocio"].ToString());
                Evento.ValorMeiaAvulsoNaoSocio = dataTable.Rows[0]["ValorMeiaAvulsoNaoSocio"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorMeiaAvulsoNaoSocio"].ToString());

                Evento.ValorIpanema = dataTable.Rows[0]["ValorIpanema"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorIpanema"].ToString());
                Evento.ValorGolodromo = dataTable.Rows[0]["ValorGolodromo"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorGolodromo"].ToString());
                Evento.ValorPortinari = dataTable.Rows[0]["ValorPortinari"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorPortinari"].ToString());
                Evento.ValorPergula = dataTable.Rows[0]["ValorPergula"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorPergula"].ToString());
                Evento.ValorSalaoDeFestas = dataTable.Rows[0]["ValorSalaoDeFestas"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorSalaoDeFestas"].ToString());
                Evento.ValorAcademia = dataTable.Rows[0]["ValorAcademia"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorAcademia"].ToString());

                //Para Boate
                Evento.ValorSocioMasculino = dataTable.Rows[0]["ValorSocioMasculino"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorSocioMasculino"].ToString());
                Evento.ValorSocioFeminino = dataTable.Rows[0]["ValorSocioFeminino"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorSocioFeminino"].ToString());
                Evento.ValorNaoSocioMasculino = dataTable.Rows[0]["ValorNaoSocioMasculino"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorNaoSocioMasculino"].ToString());
                Evento.ValorNaoSocioFeminino = dataTable.Rows[0]["ValorNaoSocioFeminino"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorNaoSocioFeminino"].ToString());
                
                Evento.InteiraCadeiraSocio = Convert.ToInt32(dataTable.Rows[0]["InteiraCadeiraSocio"]);
                Evento.InteiraCadeiraNaoSocio = Convert.ToInt32(dataTable.Rows[0]["InteiraCadeiraNaoSocio"]);
                Evento.InteiraAvulsoSocio = Convert.ToInt32(dataTable.Rows[0]["InteiraAvulsoSocio"]);
                Evento.InteiraAvulsoNaoSocio = Convert.ToInt32(dataTable.Rows[0]["InteiraAvulsoNaoSocio"]);
                Evento.MeiaCadeiraSocio = Convert.ToInt32(dataTable.Rows[0]["MeiaCadeiraSocio"]);
                Evento.MeiaCadeiraNaoSocio = Convert.ToInt32(dataTable.Rows[0]["MeiaCadeiraNaoSocio"]);
                Evento.MeiaAvulsoSocio = Convert.ToInt32(dataTable.Rows[0]["MeiaAvulsoSocio"]);
                Evento.MeiaAvulsoNaoSocio = Convert.ToInt32(dataTable.Rows[0]["MeiaAvulsoNaoSocio"]);

                Evento.CamaroteSocio = Convert.ToInt32(dataTable.Rows[0]["CamaroteSocio"]);
                Evento.CamaroteNaoSocio = Convert.ToInt32(dataTable.Rows[0]["CamaroteNaoSocio"]);
                Evento.ValorCamaroteSocio = dataTable.Rows[0]["ValorCamaroteSocio"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorCamaroteSocio"].ToString());
                Evento.ValorCamaroteNaoSocio = dataTable.Rows[0]["ValorCamaroteNaoSocio"].ToString() == "" ? 0 : Convert.ToDouble(dataTable.Rows[0]["ValorCamaroteNaoSocio"].ToString());

                //Para Boate
                //Evento.SocioMasculino = Convert.ToInt32(dataTable.Rows[0]["SocioMasculino"]);
                //Evento.SocioFeminino = Convert.ToInt32(dataTable.Rows[0]["SocioFeminino"]);
                //Evento.NaoSocioMasculino = Convert.ToInt32(dataTable.Rows[0]["NaoSocioMasculino"]);
                //Evento.NaoSocioFeminino = Convert.ToInt32(dataTable.Rows[0]["NaoSocioFeminino"]);

                Evento.DataLimite4ParcelaCota = dataTable.Rows[0]["DataLimite4ParcelaCota"].ToString();
                Evento.DataLimite3ParcelaCota = dataTable.Rows[0]["DataLimite3ParcelaCota"].ToString();
                Evento.DataLimite2ParcelaCota = dataTable.Rows[0]["DataLimite2ParcelaCota"].ToString();
                Evento.DataLimite1ParcelaCota = dataTable.Rows[0]["DataLimite1ParcelaCota"].ToString();

                Evento.DataLimite5ParcelaCredito = dataTable.Rows[0]["DataLimite5ParcelaCredito"].ToString();
                Evento.DataLimite4ParcelaCredito = dataTable.Rows[0]["DataLimite4ParcelaCredito"].ToString();
                Evento.DataLimite3ParcelaCredito = dataTable.Rows[0]["DataLimite3ParcelaCredito"].ToString();
                Evento.DataLimite2ParcelaCredito = dataTable.Rows[0]["DataLimite2ParcelaCredito"].ToString();
                Evento.DataLimite1ParcelaCredito = dataTable.Rows[0]["DataLimite1ParcelaCredito"].ToString();

                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite4ParcelaCota"].ToString())) Evento.DateLimite4ParcelaCota = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite4ParcelaCota"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite3ParcelaCota"].ToString())) Evento.DateLimite3ParcelaCota = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite3ParcelaCota"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite2ParcelaCota"].ToString())) Evento.DateLimite2ParcelaCota = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite2ParcelaCota"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite1ParcelaCota"].ToString())) Evento.DateLimite1ParcelaCota = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite1ParcelaCota"].ToString() + " 23:59:59");

                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite5ParcelaCredito"].ToString())) Evento.DateLimite5ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite5ParcelaCredito"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite4ParcelaCredito"].ToString())) Evento.DateLimite4ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite4ParcelaCredito"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite3ParcelaCredito"].ToString())) Evento.DateLimite3ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite3ParcelaCredito"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite2ParcelaCredito"].ToString())) Evento.DateLimite2ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite2ParcelaCredito"].ToString() + " 23:59:59");
                if (!String.IsNullOrEmpty(dataTable.Rows[0]["DataLimite1ParcelaCredito"].ToString())) Evento.DateLimite1ParcelaCredito = PontoBr.Conversoes.Data.ConverterDataDDMMAAAAComBarraeHoraComSegundosParaDateTime(dataTable.Rows[0]["DataLimite1ParcelaCredito"].ToString() + " 23:59:59");

                Evento.VagaEstacionamento = Convert.ToInt32(dataTable.Rows[0]["VagaEstacionamento"]);
                Evento.VagaEstacionamento_descricao = dataTable.Rows[0]["VagaEstacionamento_descricao"].ToString();
                Evento.NumIngressoEstacionamento = Convert.ToInt32(dataTable.Rows[0]["NumIngressoEstacionamento"].ToString());
                Evento.VagasEstacionamento = Convert.ToInt32(dataTable.Rows[0]["VagasEstacionamento"].ToString());
                Evento.Detalhes = dataTable.Rows[0]["Detalhes"].ToString();
                Evento.DataRetirada = dataTable.Rows[0]["DataRetirada"].ToString();
                Evento.Ativo = Convert.ToInt32(dataTable.Rows[0]["Ativo"]);
                Evento.Cadastro = dataTable.Rows[0]["Cadastro"].ToString();
                Evento.IDUsuarioCadastro = Convert.ToInt32(dataTable.Rows[0]["IDUsuarioCadastro"].ToString());
                Evento.Alteracao = dataTable.Rows[0]["Alteracao"].ToString();
                Evento.IDUsuarioAlteracao = dataTable.Rows[0]["IDUsuarioAlteracao"].ToString() == "" ? 0 : Convert.ToInt32(dataTable.Rows[0]["IDUsuarioAlteracao"].ToString());

                Evento.IngressosCadeira = String.IsNullOrEmpty(dataTable.Rows[0]["IngressosCadeira"].ToString()) ? 0 : Convert.ToInt32(dataTable.Rows[0]["IngressosCadeira"]);
                Evento.IngressosAvulsos = String.IsNullOrEmpty(dataTable.Rows[0]["IngressosAvulsos"].ToString()) ? 0 : Convert.ToInt32(dataTable.Rows[0]["IngressosAvulsos"]);
            }

            return Evento;
        }

        public void CarregarGridViewEventos(GridView grdDados)
        {
            eventoBLL BEvento = new eventoBLL();
            DataTable dataTable = BEvento.RetornarEventos();

            grdDados.DataSource = dataTable;
            grdDados.DataBind();
        }

        public void CarregarGridViewLotes(GridView grdDados, int iIDEdicao)
        {
            eventoBLL BEvento = new eventoBLL();
            string sSql = BEvento.RetornarLotes(iIDEdicao, false);

            DataTable dataTable = PontoBr.Banco.SqlServer.RetornarDataTable(sSql);

            grdDados.DataSource = dataTable;
            grdDados.DataBind();
        }

        public DataTable RetornarLotes(int iIDEdicao, bool bApenasLoteInicioFimVenda)
        {
            eventoBLL BEvento = new eventoBLL();
            string sSql = BEvento.RetornarLotes(iIDEdicao, bApenasLoteInicioFimVenda);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        /*public venda RetornarLoteDisponivelParaVenda(int iIDEdicao, venda Venda)
        {
            eventoBLL BEvento = new eventoBLL();
            DataTable dataTable = BEvento.RetornarLoteDisponivelParaVenda(iIDEdicao);

            if (dataTable.Rows.Count > 0)
            {
                Venda.Lote = Convert.ToInt32(dataTable.Rows[0]["Lote"].ToString());
                Venda.IDLote = Convert.ToInt32(dataTable.Rows[0]["IDLote"].ToString());

                Venda.ValorInteiraCadeiraSocio = Convert.ToDouble(dataTable.Rows[0]["ValorInteiraCadeiraSocio"].ToString());
                Venda.ValorInteiraCadeiraNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorInteiraCadeiraNaoSocio"].ToString());
                Venda.ValorInteiraAvulsoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorInteiraAvulsoSocio"].ToString());
                Venda.ValorInteiraAvulsoNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorInteiraAvulsoNaoSocio"].ToString());

                Venda.ValorMeiaCadeiraSocio = Convert.ToDouble(dataTable.Rows[0]["ValorMeiaCadeiraSocio"].ToString());
                Venda.ValorMeiaCadeiraNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorMeiaCadeiraNaoSocio"].ToString());
                Venda.ValorMeiaAvulsoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorMeiaAvulsoSocio"].ToString());
                Venda.ValorMeiaAvulsoNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorMeiaAvulsoNaoSocio"].ToString());

                Venda.ValorIpanema = Convert.ToDouble(dataTable.Rows[0]["ValorIpanema"].ToString());
                Venda.ValorGolodromo = Convert.ToDouble(dataTable.Rows[0]["ValorGolodromo"].ToString());
                Venda.ValorPortinari = Convert.ToDouble(dataTable.Rows[0]["ValorPortinari"].ToString());
                Venda.ValorPergula = Convert.ToDouble(dataTable.Rows[0]["ValorPergula"].ToString());
                Venda.ValorSalaoDeFestas = Convert.ToDouble(dataTable.Rows[0]["ValorSalaoDeFestas"].ToString());
                Venda.ValorAcademia = Convert.ToDouble(dataTable.Rows[0]["ValorAcademia"].ToString());

                Venda.ValorCamaroteSocio = Convert.ToDouble(dataTable.Rows[0]["ValorCamaroteSocio"].ToString());
                Venda.ValorCamaroteNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorCamaroteNaoSocio"].ToString());

                if (Convert.ToBoolean(dataTable.Rows[0]["InteiraCadeiraSocio"])) Venda.ValorInteiraCadeiraSocio = Convert.ToDouble(dataTable.Rows[0]["ValorInteiraCadeiraSocio"].ToString());
                if (Convert.ToBoolean(dataTable.Rows[0]["InteiraCadeiraNaoSocio"])) Venda.ValorInteiraCadeiraNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorInteiraCadeiraNaoSocio"].ToString());
                if (Convert.ToBoolean(dataTable.Rows[0]["InteiraAvulsoSocio"])) Venda.ValorInteiraAvulsoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorInteiraAvulsoSocio"].ToString());
                if (Convert.ToBoolean(dataTable.Rows[0]["InteiraAvulsoNaoSocio"])) Venda.ValorInteiraAvulsoNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorInteiraAvulsoNaoSocio"].ToString());

                if (Convert.ToBoolean(dataTable.Rows[0]["MeiaCadeiraSocio"])) Venda.ValorMeiaCadeiraSocio = Convert.ToDouble(dataTable.Rows[0]["ValorMeiaCadeiraSocio"].ToString());
                if (Convert.ToBoolean(dataTable.Rows[0]["MeiaCadeiraNaoSocio"])) Venda.ValorMeiaCadeiraNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorMeiaCadeiraNaoSocio"].ToString());
                if (Convert.ToBoolean(dataTable.Rows[0]["MeiaAvulsoSocio"])) Venda.ValorMeiaAvulsoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorMeiaAvulsoSocio"].ToString());
                if (Convert.ToBoolean(dataTable.Rows[0]["MeiaAvulsoNaoSocio"])) Venda.ValorMeiaAvulsoNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorMeiaAvulsoNaoSocio"].ToString());

                Venda.ValorIpanema = Convert.ToDouble(dataTable.Rows[0]["ValorIpanema"].ToString());
                Venda.ValorGolodromo = Convert.ToDouble(dataTable.Rows[0]["ValorGolodromo"].ToString());
                Venda.ValorPortinari = Convert.ToDouble(dataTable.Rows[0]["ValorPortinari"].ToString());
                Venda.ValorPergula = Convert.ToDouble(dataTable.Rows[0]["ValorPergula"].ToString());
                Venda.ValorSalaoDeFestas = Convert.ToDouble(dataTable.Rows[0]["ValorSalaoDeFestas"].ToString());
                Venda.ValorAcademia = Convert.ToDouble(dataTable.Rows[0]["ValorAcademia"].ToString());

                if (Convert.ToBoolean(dataTable.Rows[0]["CamaroteSocio"])) Venda.ValorCamaroteSocio = Convert.ToDouble(dataTable.Rows[0]["ValorCamaroteSocio"].ToString());
                if (Convert.ToBoolean(dataTable.Rows[0]["CamaroteNaoSocio"])) Venda.ValorCamaroteNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorCamaroteNaoSocio"].ToString());
                Venda.CamaroteSocio = Convert.ToInt32(dataTable.Rows[0]["CamaroteSocio"]);
                Venda.CamaroteNaoSocio = Convert.ToInt32(dataTable.Rows[0]["CamaroteNaoSocio"]);

                //Venda.Lote = Convert.ToInt32(dataTable.Rows[0]["Lote"].ToString());
                //Venda.IDLote = Convert.ToInt32(dataTable.Rows[0]["IDLote"].ToString());

                //Venda.ValorInteiraCadeiraSocio = Convert.ToDouble(dataTable.Rows[0]["ValorInteiraCadeiraSocio"].ToString());
                //Venda.ValorInteiraCadeiraNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorInteiraCadeiraNaoSocio"].ToString());
                //Venda.ValorInteiraAvulsoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorInteiraAvulsoSocio"].ToString());
                //Venda.ValorInteiraAvulsoNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorInteiraAvulsoNaoSocio"].ToString());

                //Venda.ValorMeiaCadeiraSocio = Convert.ToDouble(dataTable.Rows[0]["ValorMeiaCadeiraSocio"].ToString());
                //Venda.ValorMeiaCadeiraNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorMeiaCadeiraNaoSocio"].ToString());
                //Venda.ValorMeiaAvulsoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorMeiaAvulsoSocio"].ToString());
                //Venda.ValorMeiaAvulsoNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorMeiaAvulsoNaoSocio"].ToString());

                //Venda.ValorIpanema = Convert.ToDouble(dataTable.Rows[0]["ValorIpanema"].ToString());
                //Venda.ValorGolodromo = Convert.ToDouble(dataTable.Rows[0]["ValorGolodromo"].ToString());
                //Venda.ValorPortinari = Convert.ToDouble(dataTable.Rows[0]["ValorPortinari"].ToString());
                //Venda.ValorPergula = Convert.ToDouble(dataTable.Rows[0]["ValorPergula"].ToString());
                //Venda.ValorSalaoDeFestas = Convert.ToDouble(dataTable.Rows[0]["ValorSalaoDeFestas"].ToString());
                //Venda.ValorAcademia = Convert.ToDouble(dataTable.Rows[0]["ValorAcademia"].ToString());

                //if (Convert.ToBoolean(dataTable.Rows[0]["InteiraCadeiraSocio"])) Venda.ValorInteiraCadeiraSocio = Convert.ToDouble(dataTable.Rows[0]["ValorInteiraCadeiraSocio"].ToString());
                //if (Convert.ToBoolean(dataTable.Rows[0]["InteiraCadeiraNaoSocio"])) Venda.ValorInteiraCadeiraNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorInteiraCadeiraNaoSocio"].ToString());
                //if (Convert.ToBoolean(dataTable.Rows[0]["InteiraAvulsoSocio"])) Venda.ValorInteiraAvulsoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorInteiraAvulsoSocio"].ToString());
                //if (Convert.ToBoolean(dataTable.Rows[0]["InteiraAvulsoNaoSocio"])) Venda.ValorInteiraAvulsoNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorInteiraAvulsoNaoSocio"].ToString());

                //if (Convert.ToBoolean(dataTable.Rows[0]["MeiaCadeiraSocio"])) Venda.ValorMeiaCadeiraSocio = Convert.ToDouble(dataTable.Rows[0]["ValorMeiaCadeiraSocio"].ToString());
                //if (Convert.ToBoolean(dataTable.Rows[0]["MeiaCadeiraNaoSocio"])) Venda.ValorMeiaCadeiraNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorMeiaCadeiraNaoSocio"].ToString());
                //if (Convert.ToBoolean(dataTable.Rows[0]["MeiaAvulsoSocio"])) Venda.ValorMeiaAvulsoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorMeiaAvulsoSocio"].ToString());
                //if (Convert.ToBoolean(dataTable.Rows[0]["MeiaAvulsoNaoSocio"])) Venda.ValorMeiaAvulsoNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorMeiaAvulsoNaoSocio"].ToString());

                //Venda.ValorCamaroteSocio = Convert.ToDouble(dataTable.Rows[0]["ValorCamaroteSocio"].ToString());
                //Venda.ValorCamaroteNaoSocio = Convert.ToDouble(dataTable.Rows[0]["ValorCamaroteNaoSocio"].ToString());
                //Venda.CamaroteSocio = Convert.ToInt32(dataTable.Rows[0]["CamaroteSocio"]);
                //Venda.CamaroteNaoSocio = Convert.ToInt32(dataTable.Rows[0]["CamaroteNaoSocio"]);
            }

            return Venda;
        }*/

        public void CarregarDropDownListLotes(DropDownList dropLote, int iIDEdicao, bool bApenasLoteInicioFimVenda)
        {
            eventoBLL BEvento = new eventoBLL();
            string sSql = BEvento.RetornarLotes(iIDEdicao, bApenasLoteInicioFimVenda);

            PontoBr.Utilidades.WCL.CarregarDropDown(dropLote, sSql, "Lote", "IDLote", true, false);
        }

        public void CarregarDropDownListEventosVenda(DropDownList dropEvento, bool bTodos, bool bSelecione)
        {
            eventoBLL BEventoEdicao = new eventoBLL();
            string sSql = BEventoEdicao.RetornarEventosVenda();

            PontoBr.Utilidades.WCL.CarregarDropDown(dropEvento, sSql, "Evento", "IDEdicao", bTodos, bSelecione);
        }

        public void CarregarDropDownListEventosVendaAdministrador(DropDownList dropEvento, bool bTodos, bool bSelecione)
        {
            eventoBLL BEventoEdicao = new eventoBLL();
            string sSql = BEventoEdicao.RetornarEventosVendaAdministrador();

            PontoBr.Utilidades.WCL.CarregarDropDown(dropEvento, sSql, "Evento", "IDEdicao", bTodos, bSelecione);
        }

        public DataTable RetornarEventosVendaAdministrador()
        {
            eventoBLL BEventoEdicao = new eventoBLL();
            string sSql = BEventoEdicao.RetornarEventosVendaAdministrador();

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public void CarregarDropDownListEventosEdicao(DropDownList dropEvento, bool bTodos, bool bSelecione)
        {
            eventoBLL BEventoEdicao = new eventoBLL();
            string sSql = BEventoEdicao.RetornarEventosEdicao();

            PontoBr.Utilidades.WCL.CarregarDropDown(dropEvento, sSql, "Evento", "IDEdicao", bTodos, bSelecione);
        }

        public void CarregarDropDownListEventosEdicaoVenda(DropDownList dropEvento, bool bTodos, bool bSelecione)
        {
            eventoBLL BEventoEdicao = new eventoBLL();
            string sSql = BEventoEdicao.RetornarEventosEdicaoVenda();

            PontoBr.Utilidades.WCL.CarregarDropDown(dropEvento, sSql, "Evento", "IDEdicao", bTodos, bSelecione);
        }

        public DataTable RetornarEventosEdicao()
        {
            eventoBLL BEventoEdicao = new eventoBLL();
            string sSql = BEventoEdicao.RetornarEventosEdicao();

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarEventosVenda()
        {
            eventoBLL BEventoEdicao = new eventoBLL();
            string sSql = BEventoEdicao.RetornarEventosVenda();

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarEventosVenda(string sPerfil)
        {
            eventoBLL BEventoEdicao = new eventoBLL();
            string sSql = BEventoEdicao.RetornarEventosVenda(sPerfil);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public void CadastrarLogEdicao(int iIDEdicao, string sTextoLog, int iIDUsuarioCadastro)
        {
            eventoBLL BEvento = new eventoBLL();
            BEvento.CadastrarLogEdicao(iIDEdicao, sTextoLog, iIDUsuarioCadastro);
        }

        public void CarregarGridViewLogEdicao(GridView grdDados, int iIDEdicao)
        {
            eventoBLL BEvento = new eventoBLL();
            DataTable dataTable = BEvento.RetornarLogEdicao(iIDEdicao);

            grdDados.DataSource = dataTable;
            grdDados.DataBind();
        }

        public bool VerificarExistenciaLote(int iIDLote, int iIDEdicao, int iLote)
        {
            eventoBLL BEvento = new eventoBLL();
            return BEvento.VerificarExistenciaLote(iIDLote, iIDEdicao, iLote);
        }

        public bool VerificarExistenciaEdicao(int iIDEdicao, int iIDEvento, string sEdicao)
        {
            eventoBLL BEvento = new eventoBLL();
            return BEvento.VerificarExistenciaEdicao(iIDEdicao, iIDEvento, sEdicao);
        }

        public int RetornarTotalCadeirasEvento(int iIDEvento)
        {
            eventoBLL BEvento = new eventoBLL();
            return BEvento.RetornarTotalCadeirasEvento(iIDEvento);
        }

        public void CarregarDropDownListLocaisEvento(DropDownList dropLocal, bool bTodos, bool bSelecione)
        {
            eventoBLL BEvento = new eventoBLL();
            string sSql = BEvento.RetornarLocaisEvento();

            PontoBr.Utilidades.WCL.CarregarDropDown(dropLocal, sSql, "Local", "IDLocal", bTodos, bSelecione);
        }
    }
}

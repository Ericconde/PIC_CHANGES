﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Cielo.Messages;
using Cielo.Helpers;

namespace Cielo
{
    public class CieloClient
    {
        #region "Private"

        private string Numero;
        private string Chave;
        private Uri Endpoint;

        #endregion

        #region "Constructor"

        public CieloClient()
        {
            if (ConfigurationManager.AppSettings["CieloNumeroAfiliado"] == null)
                throw new ArgumentNullException("CieloNumeroAfiliado");

            if (ConfigurationManager.AppSettings["CieloChaveAcesso"] == null)
                throw new ArgumentNullException("CieloChaveAcesso");

            if (ConfigurationManager.AppSettings["CieloAmbiente"] == null)
                throw new ArgumentNullException("CieloAmbiente");

            Numero = ConfigurationManager.AppSettings["CieloNumeroAfiliado"];
            Chave = ConfigurationManager.AppSettings["CieloChaveAcesso"];

            if (ConfigurationManager.AppSettings["CieloAmbiente"].Equals("Producao"))
                Endpoint = new Uri(ConfigurationManager.AppSettings["CieloURL"]);
            else
                Endpoint = new Uri(ConfigurationManager.AppSettings["CieloURLTeste"]);
        }

        #endregion

        #region "Public Methods"

        public Retorno CriarTransacao(DadosCartao dadosportadorField, DadosPedido dadosPedido, Bandeira bandeira, Uri urlRetorno)
        {
            var formaPagamento = new FormaPagamento { bandeira = bandeira, parcelas = 1, produto = FormaPagamentoProduto.CreditoAVista };
            var req = RequisicaoNovaTransacaoAutorizar.AutorizarSomenteSeAutenticada;
            var capturar = true;

            return CriarTransacao(dadosportadorField, dadosPedido, formaPagamento, urlRetorno, req, capturar);
        }

        public Retorno CriarTransacao(DadosCartao dadosportadorField, DadosPedido dadosPedido, FormaPagamento formaPagamento,
                                      Uri urlRetorno, RequisicaoNovaTransacaoAutorizar reqAutorizar, bool capturar)
        {
            var dadosEc = new DadosEcAutenticacao { numero = Numero, chave = Chave };
            var ret = new Retorno();

            var msg = new RequisicaoNovaTransacao
            {
                id = dadosPedido.numero,
                versao = MensagemVersao.v110,
                dadosportador = dadosportadorField,
                dadosec = dadosEc,
                dadospedido = dadosPedido,
                formapagamento = formaPagamento,
                urlretorno = urlRetorno.AbsoluteUri,
                autorizar = reqAutorizar,
                capturar = capturar
            };

            try
            {
                var xml = msg.ToXml<RequisicaoNovaTransacao>(Encoding.GetEncoding("iso-8859-1"));

                //Retorno do XML  - CriarTransacao
                var res = EnviarMensagem(xml);

                ret = XmlToRetorno(res);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        public Retorno ConsultarTransacao(string tid)
        {
            var ret = new Retorno();

            var dadosEc = new DadosEcAutenticacao { numero = Numero, chave = Chave };

            var msg = new RequisicaoConsulta
            {
                id = DateTime.Now.ToString("yyyyMMdd"),
                versao = MensagemVersao.v110,
                tid = tid,
                dadosec = dadosEc
            };

            try
            {
                var xml = msg.ToXml<RequisicaoConsulta>(Encoding.GetEncoding("iso-8859-1"));

                var res = EnviarMensagem(xml);

                ret = XmlToRetorno(res);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        public Retorno AutorizarTransacao(string tid)
        {
            var ret = new Retorno();

            var dadosEc = new DadosEcAutenticacao { numero = Numero, chave = Chave };

            var msg = new RequisicaoAutorizacaoTid
            {
                id = DateTime.Now.ToString("yyyyMMdd"),
                versao = MensagemVersao.v110,
                tid = tid,
                dadosec = dadosEc
            };

            try
            {
                var xml = msg.ToXml<RequisicaoAutorizacaoTid>(Encoding.GetEncoding("iso-8859-1"));

                var res = EnviarMensagem(xml);

                ret = XmlToRetorno(res);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        public Retorno CancelarTransacao(string tid)
        {
            var ret = new Retorno();

            var dadosEc = new DadosEcAutenticacao { numero = Numero, chave = Chave };

            var msg = new RequisicaoCancelamento
            {
                id = DateTime.Now.ToString("yyyyMMdd"),
                versao = MensagemVersao.v110,
                tid = tid,
                dadosec = dadosEc
            };

            try
            {
                var xml = msg.ToXml<RequisicaoCancelamento>(Encoding.GetEncoding("iso-8859-1"));

                var res = EnviarMensagem(xml);

                ret = XmlToRetorno(res);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        public Retorno CapturarTransacao(string tid)
        {
            return CapturarTransacao(tid, -1, string.Empty);
        }

        public Retorno CapturarTransacao(string tid, decimal valor, string anexo)
        {
            var ret = new Retorno();

            var dadosEc = new DadosEcAutenticacao { numero = Numero, chave = Chave };

            var msg = new RequisicaoCaptura
            {
                id = DateTime.Now.ToString("yyyyMMdd"),
                versao = MensagemVersao.v110,
                tid = tid,
                dadosec = dadosEc
            };

            if (valor > -1)
                msg.valor = valor.ToFormatoCielo();

            if (!string.IsNullOrWhiteSpace(anexo))
                msg.anexo = anexo;

            try
            {
                var xml = msg.ToXml<RequisicaoCaptura>(Encoding.GetEncoding("iso-8859-1"));

                //Retorno do XML  - CapturarTransacao
                var res = EnviarMensagem(xml);

                ret = XmlToRetorno(res);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        #endregion

        #region "Private Methods"

        private Retorno XmlToRetorno(string xml)
        {
            var ret = new Retorno();

            if (!string.IsNullOrEmpty(xml))
            {
                RetornoTransacao transacao;
                RetornoErro erro;

                if (xml.Contains("</transacao>"))
                {
                    transacao = xml.ToType<RetornoTransacao>(Encoding.GetEncoding("iso-8859-1"));
                    ret.Transacao = transacao;
                    ret.Transacao.rawXml = xml;
                }
                else if (xml.Contains("</erro>"))
                {
                    erro = xml.ToType<RetornoErro>(Encoding.GetEncoding("iso-8859-1"));
                    ret.Erro = erro;
                }
            }

            return ret;
        }

        private string EnviarMensagem(string xml)
        {
            var http = new EasyHttpClient("iso-8859-1", "application/x-www-form-urlencoded", "CieloClient");

            return http.Post(Endpoint.AbsoluteUri, string.Format("mensagem={0}", xml));
        }

        #endregion
    }
}

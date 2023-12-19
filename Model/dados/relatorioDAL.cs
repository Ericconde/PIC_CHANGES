using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.objetos;

namespace Model.dados
{
    public class relatorioDAL
    {
        internal string RetornarDashboard(int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select Perfil [Tipo de Cliente], count(*) Quantidade ");
            sSql.Append(" from tUsuario u ");
            sSql.Append(" inner join tPerfil p on u.IDPerfil = p.IDPerfil ");
            sSql.Append(" where u.IDPerfil in (3,4) ");
            sSql.Append(" group by Perfil ");

            sSql.Append(" select top 12 DATEPART(YEAR, Acesso) Ano, DATEPART(MONTH, Acesso) Mes, ");
            sSql.Append(" COUNT(*) Quantidade ");
            sSql.Append(" from tLogAcesso ");
            sSql.Append(" group by DATEPART(YEAR, Acesso), DATEPART(MONTH, Acesso)  ");
            sSql.Append(" order by DATEPART(YEAR, Acesso) desc, DATEPART(MONTH, Acesso) desc ");

            sSql.Append(" select top 12 [Evento / Edição], ");
            sSql.Append(" case when (TipoIngresso like '%cadeira%') then 'Cadeira' else 'Avulso' end [Tipo Ingresso], ");
            sSql.Append(" COUNT(*) Quantidade ");
            sSql.Append(" from vVendaVoucher vv ");
            sSql.Append(" inner join tTipoIngresso t on vv.IDTipoIngresso = t.IDTipoIngresso ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" group by case when (TipoIngresso like '%cadeira%') then 'Cadeira' else 'Avulso' end, [Evento / Edição] ");
            sSql.Append(" order by case when (TipoIngresso like '%cadeira%') then 'Cadeira' else 'Avulso' end, [Evento / Edição] ");

            sSql.Append(" select top 12 [Evento / Edição],  ");
            sSql.Append(" case when (TipoIngresso like '%cadeira%') then 'Cadeira' else 'Avulso' end [Tipo Ingresso], ");
            sSql.Append(" sum(Valor) Valor ");
            sSql.Append(" from vVendaVoucher vv ");
            sSql.Append(" inner join tTipoIngresso t on vv.IDTipoIngresso = t.IDTipoIngresso ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" group by case when (TipoIngresso like '%cadeira%') then 'Cadeira' else 'Avulso' end, [Evento / Edição] ");
            sSql.Append(" order by case when (TipoIngresso like '%cadeira%') then 'Cadeira' else 'Avulso' end, [Evento / Edição] ");

            return sSql.ToString();
        }

        internal string RetornarVoucher(int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" exec sRetornarVoucher @IDvenda = " + iIDVenda + " ");

            return sSql.ToString();
        }

        internal string RetornarIngressosCatracas(int iIDEdicao, string sNomeCliente, int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select v.Ticket [Ticket (Núm. Ingresso)], rc.Tipo [Tipo de Ingresso], ");
            sSql.Append(" v.IDVenda [Voucher], v.Nome [Nome do Cliente],   ");
            sSql.Append(" v.NumeroCota Cota, rc.IdentidadeEletronica [Identidade Eletrônica], ");
            sSql.Append(" CONVERT(varchar, rc.DataHoraEntrada, 103) + ' ' + CONVERT(varchar, rc.DataHoraEntrada, 108) [Data/Hora Entrada], ");
            sSql.Append(" u.Nome [Responsável Leitura], Catraca ");
            sSql.Append(" from tIngressosCatraca rc ");
            sSql.Append(" inner join vVendaVoucher v on v.IdentidadeEletronica = rc.IdentidadeEletronica ");
            sSql.Append(" left join tUsuario u on u.IDUsuario = rc.IDUsuario ");
            sSql.Append(" where rc.IDEdicao = " + iIDEdicao + " ");

            if (!String.IsNullOrEmpty(sNomeCliente)) sSql.Append(" and v.Nome like '%" + sNomeCliente + "%' ");
            if (iIDVenda != 0 && iIDVenda != -1) sSql.Append(" and v.IDVenda = " + iIDVenda + " ");

            return sSql.ToString();
        }

        internal string RetornarLogAcesso(string sDataInicial, string sDataFinal, string sNomeUsuario)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select convert(varchar, Acesso, 103) + ' ' + convert(varchar, Acesso, 108) [Data/Hora Acesso], Pagina [Página], ");
            sSql.Append(" IP, ");
            sSql.Append(" Nome + ' (' + upper(Perfil) + ')' Usuario, [E-mail], CPF, Cota ");
            sSql.Append(" from vLogAcesso ");
            sSql.Append(" where Acesso between '" + sDataInicial + "' and '" + sDataFinal + "' ");

            if (!String.IsNullOrEmpty(sNomeUsuario))
                sSql.Append(" and Nome like '%" + sNomeUsuario + "%' ");

            sSql.Append(" order by IDLog desc ");

            return sSql.ToString();
        }

        internal string RetornarClientesCadastrados(string sDataInicial, string sDataFinal, string sNomeUsuario, string sCPF)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" exec sRetornarClientes ");
            sSql.Append(" @Nome = '" + sNomeUsuario + "',  ");
            sSql.Append(" @CPF = '" + sCPF + "', ");
            sSql.Append(" @DataInicial = '" + sDataInicial + "', ");
            sSql.Append(" @DataFinal = '" + sDataFinal + "' ");

            return sSql.ToString();
        }

        internal string RetornarVendasDetalhado(string sDataInicial, string sDataFinal, int iIDEdicao, int iLote, string sPerfil, int iIDFormaPagamento, int iNumeroCota)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" exec sRetornarVendasDetalhado ");
            sSql.Append(" @DataInicial = '" + sDataInicial + "', ");
            sSql.Append(" @DataFinal = '" + sDataFinal + "', ");
            sSql.Append(" @IDEdicao = " + iIDEdicao + ", ");
            sSql.Append(" @Lote = " + iLote + ", ");
            sSql.Append(" @Perfil = '" + sPerfil + "', ");
            sSql.Append(" @IDFormaPagamento = " + iIDFormaPagamento + ", ");
            sSql.Append(" @NumeroCota = " + iNumeroCota + " ");

            return sSql.ToString();
        }

        internal string RetornarVendasSintetico(string sDataInicial, string sDataFinal, string sIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" exec sRetornarVendasSintetico ");
            sSql.Append(" @DataInicial = '" + sDataInicial + "', ");
            sSql.Append(" @DataFinal = '" + sDataFinal + "', ");
            sSql.Append(" @IDEdicao = '" + sIDEdicao + "' ");

            return sSql.ToString();
        }

        internal string RetornarSocios(string sNome, string sCPF, int iNumeroCota)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select Num_Cota, Digito, Nome, Dat_Nasc, CPF ,RG ,Email, fone1 ,fone2 ,Logradouro, Numero, ");
            sSql.Append(" Complemento, Bairro, CEP, Nom_Cidade, Nom_Estado, Abrev_Estado, ");
            sSql.Append(" case when (Debito = 1) then 'Sim' else 'Não' end Debito, ");
            sSql.Append(" convert(varchar, DataAtualizacao, 103) + ' ' + convert(varchar, DataAtualizacao, 108) [Data de atualização] ");
            sSql.Append(" from tSocio where Num_Cota is not null  ");
            if (!String.IsNullOrEmpty(sNome.Trim())) sSql.Append(" and Nome like '%" + sNome + "%' ");
            if (!String.IsNullOrEmpty(sCPF.Trim())) sSql.Append(" and replace(replace(CPF, '.',''), '-','') like '%" + sCPF + "%' ");

            if (iNumeroCota != -1 && iNumeroCota != 0)
                sSql.Append(" and Num_Cota = " + iNumeroCota + " ");

            return sSql.ToString();
        }

        internal string RetornarResumoFormaPagamento(string sDataInicial, string sDataFinal, int iIDEdicao, int iLote, string sPerfil, int iIDFormaPagamento)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" exec sRetornarResumoFormaPagamento ");
            sSql.Append(" @DataInicial = '" + sDataInicial + "', ");
            sSql.Append(" @DataFinal = '" + sDataFinal + "', ");
            sSql.Append(" @IDEdicao = " + iIDEdicao + ", ");
            sSql.Append(" @Lote = " + iLote + ", ");
            sSql.Append(" @Perfil = '" + sPerfil + "', ");
            sSql.Append(" @IDFormaPagamento = " + iIDFormaPagamento + " ");

            return sSql.ToString();
        }

        internal string RetornarResumoTipoIngresso(string sDataInicial, string sDataFinal, int iIDEdicao, int iLote, string sPerfil, int iIDFormaPagamento)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" exec sRetornarResumoTipoIngresso ");
            sSql.Append(" @DataInicial = '" + sDataInicial + "', ");
            sSql.Append(" @DataFinal = '" + sDataFinal + "', ");
            sSql.Append(" @IDEdicao = " + iIDEdicao + ", ");
            sSql.Append(" @Lote = " + iLote + ", ");
            sSql.Append(" @Perfil = '" + sPerfil + "', ");
            sSql.Append(" @IDFormaPagamento = " + iIDFormaPagamento + " ");

            return sSql.ToString();
        }

        internal string RetornarCompradosSaldo(string sDataInicial, string sDataFinal, int iIDEdicao, int iLote, string sPerfil, int iIDFormaPagamento)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" exec sRetornarCompradosSaldo ");
            sSql.Append(" @DataInicial = '" + sDataInicial + "', ");
            sSql.Append(" @DataFinal = '" + sDataFinal + "', ");
            sSql.Append(" @IDEdicao = " + iIDEdicao + ", ");
            sSql.Append(" @Lote = " + iLote + ", ");
            sSql.Append(" @Perfil = '" + sPerfil + "', ");
            sSql.Append(" @IDFormaPagamento = " + iIDFormaPagamento + " ");

            return sSql.ToString();
        }

        internal string RetornarVoucherDetalhado(int iIDVenda, string sDataInicial, string sDataFinal, int iIDEdicao, int iLote, string sPerfil, int iIDFormaPagamento)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" exec sRetornarVoucherDetalhado ");
            sSql.Append(" @IDVenda = " + iIDVenda + ", ");
            sSql.Append(" @DataInicial = '" + sDataInicial + "', ");
            sSql.Append(" @DataFinal = '" + sDataFinal + "', ");
            sSql.Append(" @IDEdicao = " + iIDEdicao + ", ");
            sSql.Append(" @Lote = " + iLote + ", ");
            sSql.Append(" @Perfil = '" + sPerfil + "', ");
            sSql.Append(" @IDFormaPagamento = " + iIDFormaPagamento + " ");

            return sSql.ToString();
        }

        internal string RetornarPagamentosCartao(int iIDVenda, string sNumeroCartao, string sCodigoAutorizacao, string sTid, string sNomeCliente, int iNumeroCota, string sTitular, int iIDEdicao, string sDataInicial, string sDataFinal)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" SELECT vc.Voucher, v.Nome [Nome Cliente], v.NumeroCota Cota, ");
            sSql.Append(" vc.Titular, vc.[Número Cartão], vc.[Bandeira], vc.[Cód. Autorização], vc.[TID Cielo], ");
            sSql.Append(" case when (vc.Aprovada = 1) then 'Sim' else 'Não' end [Aprovação Cielo], ");
            sSql.Append(" case when (vc.Capturada = 1) then 'Sim' else 'Não' end [Captura Cielo], ");
            sSql.Append(" CONVERT(varchar, vc.Cadastro, 103) + ' ' + CONVERT(varchar, vc.Cadastro, 108) [Data/Hora Transação], ");
            sSql.Append(" v.StatusCompra [Status da Compra] ");
            sSql.Append(" FROM vVendaCartao vc ");
            sSql.Append(" inner join vVenda v on vc.Voucher = v.IDVenda ");
            sSql.Append(" where vc.Aprovada in (0,1) ");
            if (iIDVenda != -1) sSql.Append(" and vc.Voucher = " + iIDVenda + " ");
            if (!String.IsNullOrEmpty(sNumeroCartao)) sSql.Append(" and [Número Cartão] like '%" + sNumeroCartao + "%' ");
            if (!String.IsNullOrEmpty(sCodigoAutorizacao)) sSql.Append(" and [Cód. Autorização] like '%" + sCodigoAutorizacao + "%' ");
            if (!String.IsNullOrEmpty(sTid)) sSql.Append(" and [TID Cielo] like '%" + sTid + "%' ");
            if (!String.IsNullOrEmpty(sNomeCliente)) sSql.Append(" and v.Nome like '%" + sNomeCliente + "%' ");
            if (!String.IsNullOrEmpty(sTitular)) sSql.Append(" and vc.Titular like '%" + sTitular + "%' ");
            if (iNumeroCota != -1) sSql.Append(" and v.NumeroCota = " + iNumeroCota + " ");
            if (iIDEdicao != -1) sSql.Append(" and v.IDEdicao = " + iIDEdicao + " ");
            if (!String.IsNullOrEmpty(sDataInicial) && !String.IsNullOrEmpty(sDataFinal)) sSql.Append(" and vc.Cadastro between '"+ sDataInicial + "' and '"+ sDataFinal + "' ");
            sSql.Append(" order by vc.Cadastro desc ");

            return sSql.ToString();
        }

        internal string RetornarCancelamentosVoucher(string sDataInicial, string sDataFinal, int iIDEdicao, int iIDFormaPagamento, double dNumeroCota, string sDigito)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select v.IDVenda [Voucher], c.NumeroCota [Cota], c.DigitoCota [Dígito], c.Nome[Nome do Cliente], ");
            sSql.Append(" convert(varchar,v.DataHoraCompra,103) + ' ' + convert(varchar,v.DataHoraCompra,108) [Data da Compra],lr.Local [Local de retirada], sc.Status[Status de compra], ");
            sSql.Append(" e.Edicao [Edição],fp.FormaPagamento[Forma de pagamento], v.DetalhesFormaPagamento[Detalhes do pagamento],");
            sSql.Append(" convert(varchar,v.DataHoraCancelamento,103) + ' ' + convert(varchar,v.DataHoraCancelamento,108) [Data do cancelamento], ");
            sSql.Append(" u.Nome [Responsável pelo cancelamento], ");
            sSql.Append(" car.NumeroCartao [Número do Cartão], car.AuthorizationCode [Cód. Autorização], car.Valor [Valor total no cartão] ");
            sSql.Append(" from tVenda v ");
            sSql.Append(" inner join tCliente c on v.IDCliente = c.IDCliente ");
            sSql.Append(" inner join tStatusCompra sc on v.IDStatusCompra = sc.IDStatus ");
            sSql.Append(" inner join tUsuario u on v.IDUsuarioCancelamento = u.IDUsuario ");
            sSql.Append(" left join tLocalRetirada lr on v.IDLocalRetirada = lr.IDLocal ");
            sSql.Append(" inner join tEdicao e on v.IDEdicao = e.IDEdicao ");
            sSql.Append(" inner join tFormaPagamento fp on v.IDFormaPagamento = fp.IDFormaPagamento ");
            sSql.Append(" left join tCartao car on car.IDVenda = v.IDVenda and car.Aprovada = 1 ");
            sSql.Append(" where DataHoraCompra between '" + sDataInicial + "' and '" + sDataFinal + "' ");
            sSql.Append(" and v.IDEdicao = " + iIDEdicao + " ");
            if (iIDFormaPagamento != -1)
                sSql.Append(" and v.IDFormaPagamento = " + iIDFormaPagamento + " ");

            if (dNumeroCota != 0)
                sSql.Append(" and c.NumeroCota = " + dNumeroCota + " ");

            if (!String.IsNullOrEmpty(sDigito))
                sSql.Append(" and c.DigitoCota = '" + sDigito + "' ");

            return sSql.ToString();
        }

        internal string RetornarVouchersBaixados(string sDataInicial, string sDataFinal, int iIDEdicao, int iIDFormaPagamento, int iStatusVoucher, double dNumeroCota, string sDigito)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append("select v.IDVenda [Voucher], c.NumeroCota [Cota], c.DigitoCota [Dígito], c.Nome[Nome do Cliente], ");
            sSql.Append("convert(varchar,v.DataHoraCompra,103) + ' ' + convert(varchar,v.DataHoraCompra,108) [Data da compra] ,lr.Local [Local de retirada], sc.Status [Status de compra], ");
            sSql.Append("e.Edicao [Edição],fp.FormaPagamento [Forma de pagamento], v.DetalhesFormaPagamento [Detalhes do pagamento],");
            sSql.Append("convert(varchar,v.DataHoraEntregaIngresso,103) + ' ' + convert(varchar,v.DataHoraEntregaIngresso,108) [Data da entrega], ");
            sSql.Append("u.Nome[Responsável pela entrega] from tVenda v ");
            sSql.Append("inner join tCliente c on v.IDCliente = c.IDCliente ");
            sSql.Append("inner join tStatusCompra sc on v.IDStatusCompra = sc.IDStatus ");
            sSql.Append("left join tUsuario u on v.IDUsuarioEntregaIngresso = u.IDUsuario ");
            sSql.Append("inner join tLocalRetirada lr on v.IDLocalRetirada = lr.IDLocal ");
            sSql.Append("inner join tEdicao e on v.IDEdicao = e.IDEdicao ");
            sSql.Append("inner join tFormaPagamento fp on v.IDFormaPagamento = fp.IDFormaPagamento ");
            sSql.Append("where DataHoraCompra between '" + sDataInicial + "' and '" + sDataFinal + "' ");
            sSql.Append("and v.IDEdicao = " + iIDEdicao + " ");
            if (iIDFormaPagamento != -1)
                sSql.Append(" and v.IDFormaPagamento = " + iIDFormaPagamento + " ");

            if (iStatusVoucher == 2)
                sSql.Append(" and v.IDStatusCompra = 3 ");
            if (iStatusVoucher == 3)
                sSql.Append(" and v.IDStatusCompra = 2 ");

            if (dNumeroCota != 0)
                sSql.Append(" and c.NumeroCota = " + dNumeroCota + " ");

            if (!String.IsNullOrEmpty(sDigito))
                sSql.Append(" and c.DigitoCota = '" + sDigito + "' ");

            return sSql.ToString();
        }

        internal string RetornarRecibosPendentes(string sDataInicial, string sDataFinal, int iIDEdicao, int iIDFormaPagamento, double dNumeroCota, string sDigito)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append("select v.IDVenda [Voucher], c.NumeroCota [Cota], c.DigitoCota [Dígito], c.Nome[Nome do Cliente], ");
            sSql.Append("convert(varchar,v.DataHoraCompra,103) + ' ' + convert(varchar,v.DataHoraCompra,108) [Data da compra] ,lr.Local[Local de retirada], sc.Status [Status de compra], ");
            sSql.Append("e.Edicao [Edição],fp.FormaPagamento [Forma de pagamento], v.DetalhesFormaPagamento [Detalhes do pagamento], ");
            sSql.Append("u.Nome[Nome do vendedor] from tVenda v ");
            sSql.Append("inner join tCliente c on v.IDCliente = c.IDCliente ");
            sSql.Append("inner join tStatusCompra sc on v.IDStatusCompra = sc.IDStatus ");
            sSql.Append("inner join tUsuario u on v.IDUsuarioCadastro = u.IDUsuario ");
            sSql.Append("inner join tLocalRetirada lr on v.IDLocalRetirada = lr.IDLocal ");
            sSql.Append("inner join tEdicao e on v.IDEdicao = e.IDEdicao ");
            sSql.Append("inner join tFormaPagamento fp on v.IDFormaPagamento = fp.IDFormaPagamento ");
            sSql.Append("where v.IDStatusCompra = 5 ");
            sSql.Append("and DataHoraCompra between '" + sDataInicial + "' and '" + sDataFinal + "' ");
            sSql.Append("and v.IDEdicao = " + iIDEdicao + " ");

            if (dNumeroCota != 0)
                sSql.Append(" and c.NumeroCota = " + dNumeroCota + " ");

            if (!String.IsNullOrEmpty(sDigito))
                sSql.Append(" and c.DigitoCota = '" + sDigito + "' ");

            return sSql.ToString();

        }

        internal string RetornaVendaNaoAprovadaCartao(string sDataInicial, string sDataFinal, int iIDEdicao, string sIDPerfil, int iIDVenda)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select v.IDVenda [Núm Voucher], c.Nome,c.CPF, ");
            sSql.Append(" vc.Titular, vc.[Número Cartão], vc.Bandeira, vc.Valor, vc.Parcelas, vc.Cadastro [Data/Hora Cadastro], ");
            sSql.Append(" e.Edicao, p.Perfil  ");
            sSql.Append(" from vVendaCartao vc ");
            sSql.Append(" inner join tVenda v on v.IDVenda = vc.Voucher ");
            sSql.Append(" inner join tCliente c on c.IDCliente = v.IDCliente ");
            sSql.Append(" inner join tEdicao e on e.IDEdicao = v.IDEdicao ");
            sSql.Append(" inner join tUsuario u on u.IDUsuario = v.IDUsuarioCadastro ");
            sSql.Append(" inner join tPerfil p on p.IDPerfil = u.IDPerfil ");
            sSql.Append(" where vc.Aprovada in (0) ");
            sSql.Append(" and vc.Cadastro between '" + sDataInicial + "' and '" + sDataFinal + "'");
            sSql.Append(" and v.IDEdicao = " + iIDEdicao + " and u.IDPerfil in (" + sIDPerfil + ")");

            if (iIDVenda != -1)
                sSql.Append(" and v.IDVenda =" + iIDVenda + " ");

            return sSql.ToString();
        }

        internal string RetornarTickets(int iIDEdicao, string sNome, string sCPF, int iNumeroCota, string sDataInicial, string sDataFinal, int iIDVenda,
            string sIDPerfil)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select vv.Tipo [Tipo de Ingresso], vv.Ticket, Valor, vv.IDVenda Voucher, ");
            sSql.Append(" vv.Nome, vv.NumeroCota Cota, vv.CPF, vv.FormaPagamento [Forma de Pagamento], ");
            sSql.Append(" [Data/Hora Compra], uco.Nome [Responsável pela Compra], uco.Email [E-mail], ");
            sSql.Append(" TelefoneResidencial [Telefone Residencial], TelefoneCelular [Telefone Celular], TelefoneComercial [Telefone Comercial] ");
            sSql.Append(" from vVendaVoucher vv ");
            sSql.Append(" inner join tVenda v on v.IDVenda = vv.IDVenda ");
            sSql.Append(" inner join tUsuario uco on uco.IDUsuario = v.IDUsuarioCadastro ");
            sSql.Append(" inner join tCliente cl on cl.IDCliente = vv.IDCliente ");
            sSql.Append(" inner join tUsuario uc on cl.IDUsuario = uc.IDUsuario ");
            sSql.Append(" where vv.IDEdicao = " + iIDEdicao + " ");

            if (!String.IsNullOrEmpty(sNome))
                sSql.Append(" and vv.Nome like '%" + sNome + "%' ");

            if (!String.IsNullOrEmpty(sCPF))
                sSql.Append(" and vv.CPF like '%" + sCPF + "%' ");

            if (!String.IsNullOrEmpty(sIDPerfil))
                sSql.Append("and uc.IDPerfil in ( " + sIDPerfil + ")");

            if (iNumeroCota != 0 && iNumeroCota != -1)
                sSql.Append(" and vv.NumeroCota = " + iNumeroCota + " ");

            if (iIDVenda != 0 && iIDVenda != -1)
                sSql.Append(" and vv.IDVenda = " + iIDVenda + " ");

            if (!String.IsNullOrEmpty(sDataInicial) && !String.IsNullOrEmpty(sDataFinal))
                sSql.Append(" and vv.DataHoraCompra between '" + sDataInicial + "' and '" + sDataFinal + "'");

            return sSql.ToString();
        }

        internal string RetornarCancelamentosTicket(string sDataInicial, string sDataFinal, int iIDEdicao, int iIDFormaPagamento, double dNumeroCota, string sDigito)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select Ticket, ic.Tipo Resumo, ic.Valor, v.IDVenda Voucher, c.Nome, ");
            sSql.Append(" c.NumeroCota Cota, c.DigitoCota [Dígito],  ");
            sSql.Append(" convert(varchar, v.DataHoraCompra, 103) + ' ' + convert(varchar, v.DataHoraCompra, 108)  [Data da Compra],  ");
            sSql.Append(" ed.Edicao [Edição],  ");
            sSql.Append(" convert(varchar,ic.DataHoraCancelamento,103) + ' ' + convert(varchar,ic.DataHoraCancelamento,108) [Data do cancelamento],  ");
            sSql.Append(" uc.Nome [Responsável pelo cancelamento] ");
            sSql.Append(" from tIngressoCancelamento ic ");
            sSql.Append(" inner join tVenda v on ic.IDVenda = v.IDVenda ");
            sSql.Append(" inner join tEdicao ed on v.IDEdicao = ed.IDEdicao ");
            sSql.Append(" inner join tCliente c on v.IDCliente = c.IDCliente ");
            sSql.Append(" inner join tStatusCompra sc on v.IDStatusCompra = sc.IDStatus ");
            sSql.Append(" inner join tUsuario uc on ic.IDUsuarioCancelamento = uc.IDUsuario ");
            sSql.Append(" where v.IDEdicao = " + iIDEdicao + " ");

            if (iIDFormaPagamento != -1)
                sSql.Append(" and v.IDFormaPagamento = " + iIDFormaPagamento + " ");

            if (dNumeroCota != 0)
                sSql.Append(" and c.NumeroCota = " + dNumeroCota + " ");

            if (!String.IsNullOrEmpty(sDigito))
                sSql.Append(" and c.DigitoCota = '" + sDigito + "'  ");

            sSql.Append(" and v.DataHoraCompra between '" + sDataInicial + "' and '" + sDataFinal + "' ");

            return sSql.ToString();
        }

        internal string RetornarIngressoCancelamento(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" exec sRetornarIngressoCancelamento ");
            sSql.Append(" @DataInicial = '" + sDataInicial + "', ");
            sSql.Append(" @DataFinal = '" + sDataFinal + "', ");
            sSql.Append(" @IDEdicao = " + iIDEdicao + " ");

            return sSql.ToString();
        }

        internal string RetornarSolicitacoesIngressos(int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select *  ");
            sSql.Append(" from vSolicitacaoIngresso ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " ");

            return sSql.ToString();
        }

        internal string RetornarMapaMesas(int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select c.Setor, Mesa,  ");
            sSql.Append(" count(*) [Lugares na mesa], ");
            sSql.Append(" cl.Nome Cliente, v.IDVenda Voucher ");
            sSql.Append(" from tVendaCadeira vc ");
            sSql.Append(" inner join tVenda v on vc.IDVenda = v.IDVenda ");
            sSql.Append(" inner join tCadeira c on vc.IDCadeira = c.IDCadeira ");
            sSql.Append(" inner join tCliente cl on v.IDCliente = cl.IDCliente ");
            sSql.Append(" where v.IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" group by c.Setor, Mesa, cl.Nome, v.IDVenda ");
            sSql.Append(" order by Mesa, v.IDVenda ");

            return sSql.ToString();
        }

        internal string RetornarAlteracoesEmail()
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select ua.Nome [Responsável Alteração], ");
            sSql.Append(" convert(varchar, l.DataAlteracao, 103) + ' ' + convert(varchar, l.DataAlteracao, 108) [Data/Hora Alteração], ");
            sSql.Append(" l.EmailNovo [E-mail Novo], l.EmailAntigo [E-mail Substituído], ");
            sSql.Append(" c.Nome [Nome do Cliente],  ");
            sSql.Append(" case when (c.NumeroCota = 0) then '-' else convert(varchar, c.NumeroCota) end Cota ");
            sSql.Append(" from tLogAlteracaoEmail l ");
            sSql.Append(" inner join tUsuario uc on l.IDUsuario = uc.IDUsuario ");
            sSql.Append(" inner join tUsuario ua on l.IDUsuarioAlteracao = ua.IDUsuario ");
            sSql.Append(" inner join tCliente c on uc.IDUsuario = c.IDUsuario ");

            return sSql.ToString();
        }

        //internal string RetornarStatusEstacionamento()
        //{
        //    StringBuilder sSql = new StringBuilder();
        //    sSql.Append(" Declare ");
        //    sSql.Append(" @IDEdicao int ");

        //    sSql.Append(" select @IDEdicao = MAX(IDEdicao) ");
        //    sSql.Append(" from tEdicao ");

        //    sSql.Append(" select count(*) [Total] ");
        //    sSql.Append(" from vEstacionamento ");
        //    sSql.Append(" where IDEdicao = @IDEdicao ");

        //    sSql.Append(" select count(*) [Não entraram] ");
        //    sSql.Append(" from vEstacionamento ");
        //    sSql.Append(" where IDEdicao = @IDEdicao ");
        //    sSql.Append(" and DataHoraEntradaEstacionamento is null ");

        //    sSql.Append(" select count(*) [Já entraram] ");
        //    sSql.Append(" from vEstacionamento ");
        //    sSql.Append(" where IDEdicao = @IDEdicao ");
        //    sSql.Append(" and DataHoraEntradaEstacionamento is not null ");

        //    return sSql.ToString();
        //}

        internal string RetornarStatusEntradaIngresso(int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select count(*) [Total] ");
            sSql.Append(" from vVendaVoucher ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " ");

            sSql.Append(" select count(*) [Não entraram] ");
            sSql.Append(" from vVendaVoucher ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + "  ");
            sSql.Append(" and DataHoraEntradaIngresso is null ");

            sSql.Append(" select count(*) [Já entraram] ");
            sSql.Append(" from vVendaVoucher ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + "  ");
            sSql.Append(" and DataHoraEntradaIngresso is not null ");

            return sSql.ToString();
        }

        //internal string RetornarIngressosCatracas(int iIDEdicao)
        //{
        //    StringBuilder sSql = new StringBuilder();
        //    sSql.Append(" select v.Ticket [Ticket (Núm. Ingresso)], Tipo [Tipo de Ingresso], ");
        //    sSql.Append(" v.IDVenda [Voucher], v.Nome [Nome do Cliente],   ");
        //    sSql.Append(" v.NumeroCota Cota, rc.IdentidadeEletronica [Identidade Eletrônica], ");
        //    sSql.Append(" CONVERT(varchar, rc.DataHoraEntrada, 103) + ' ' + CONVERT(varchar, rc.DataHoraEntrada, 108) [Data/Hora Entrada], ");
        //    sSql.Append(" u.Nome [Responsável Leitura]  ");
        //    sSql.Append(" from tIngressosCatraca rc ");
        //    sSql.Append(" inner join vVendaVoucher v on v.IdentidadeEletronica = rc.IdentidadeEletronica ");
        //    sSql.Append(" left join tUsuario u on u.IDUsuario = rc.IDUsuario ");
        //    sSql.Append(" where IDEdicao = " + iIDEdicao + " ");

        //    return sSql.ToString();
        //}

        internal string RetornarDadosEventoBordero(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" exec sRetornarDadosEventoBordero ");
            sSql.Append(" @DataInicial = '" + sDataInicial + "', ");
            sSql.Append(" @DataFinal = '" + sDataFinal + "', ");
            sSql.Append(" @IDEdicao = " + iIDEdicao + " ");

            return sSql.ToString();
        }

        internal string RetornarVendasDetalhadoBordero(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" Select  vt.IDVenda, Ticket as [Número Ingresso], Tipo, vt.Valor, ");
            sSql.Append(" vt.[Data/Hora Compra], FormaPagamento ");
            sSql.Append(" from vVendaVoucher vt ");
            sSql.Append(" where vt.DataHoraCompra between '" + sDataInicial + "' and '" + sDataFinal + "'");
            sSql.Append(" and vt.IDEdicao = " + iIDEdicao + " ");


            return sSql.ToString();
        }

        internal string RetornarVendasSinteticoBordero(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select  Tipo, count(*) Quantidade, sum(valor) Total ");
            sSql.Append(" from vVendaVoucher ");
            sSql.Append(" where DataHoraCompra between '" + sDataInicial + "' and '" + sDataFinal + "' ");
            sSql.Append(" and IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" group by Tipo, Lote, Valor ");

            return sSql.ToString();
        }

        internal string RetornarControleEntradaBordero(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" Select  Ticket [Número do ingresso],Tipo [tipo de ingresso], Valor,DataHoraCompra [Data/Hora Compra], ");
            sSql.Append(" FormaPagamento [Forma de pagamento] ,DataHoraEntradaIngresso [Data/Hora entrada] ");
            sSql.Append(" From vVendaVoucher ");
            sSql.Append(" where DataHoraCompra between '" + sDataInicial + "' and '" + sDataFinal + "' ");
            sSql.Append(" and IDEdicao = " + iIDEdicao + " ");

            return sSql.ToString();
        }

        internal string RetornarVendasFisco(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select v.Evento, v.Edicao [Edição], v.IDVenda [Voucher], ");
            sSql.Append(" v.Nome, v.CPF, v.NumeroCota [Cota], ");
            sSql.Append(" CONVERT(varchar, v.DataHoraReserva, 103) + ' ' + CONVERT(varchar, v.DataHoraReserva, 108) [Data/Hora Reserva], ");
            sSql.Append(" case when (v.IDStatusCompra IN (2, 3)) then 'Sim' else 'Não' end [Venda Efetivada], ");
            sSql.Append(" voucher.Valor [Valor Total]  ");
            sSql.Append(" from vVenda v ");
            sSql.Append(" left join (select IDVenda, SUM(Valor) Valor from vVendaVoucher group by IDVenda) voucher on voucher.IDVenda = v.IDVenda ");
            sSql.Append(" where v.IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" and v.DataHoraReserva between '" + sDataInicial + "' and '" + sDataFinal + "' ");
            sSql.Append(" order by v.IDVenda ");

            return sSql.ToString();
        }

        internal string RetornarIngressosFisco(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select IDVenda Voucher, Nome, CPF, NumeroCota Cota, count(*) [Núm. Ingressos], FormaPagamento [Forma de Pagamento], sum(Valor) [Valor total] ");
            sSql.Append(" from vVendaVoucher ");
            sSql.Append(" where DataHoraCompra between '" + sDataInicial + "' and '" + sDataFinal + "' ");
            sSql.Append(" and IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" group by IDVenda, Nome, CPF, NumeroCota, FormaPagamento ");
            sSql.Append(" order by IDVenda desc ");

            return sSql.ToString();
        }

        internal string RetornarResumoPontoVendaFisco(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select case when (uc.IDPerfil in (3,4)) then 'Site' else 'Sede do Clube' end [Ponto de Venda], ");
            sSql.Append(" COUNT(*) [Quantidade Vendida] ");
            sSql.Append(" from tVenda v ");
            sSql.Append(" inner join tUsuario uc on uc.IDUsuario = v.IDUsuarioCadastro ");
            sSql.Append(" where v.IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" and v.DataHoraCompra between '" + sDataInicial + "' and '" + sDataFinal + "' ");
            sSql.Append(" and IDStatusCompra in (2,3) ");
            sSql.Append(" group by case when (uc.IDPerfil in (3,4)) then 'Site' else 'Sede do Clube' end ");

            return sSql.ToString();
        }

        internal string RetornarVendasConsolidadoFisco(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select TipoIngresso [Tipo de Ingresso], ");
            sSql.Append(" COUNT(*) [Quantidade Vendida], Valor [Valor Unitário], SUM(Valor) [Valor Total] ");
            sSql.Append(" from vVendaTicket vt ");
            sSql.Append(" inner join tVenda v on v.IDVenda = vt.IDVenda ");
            sSql.Append(" where v.IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" and v.DataHoraCompra between '" + sDataInicial + "' and '" + sDataFinal + "' ");
            sSql.Append(" group by TipoIngresso, Valor ");

            return sSql.ToString();
        }

        internal string RetornarResumoCortesiaFisco(string sDataInicial, string sDataFinal, int iIDEdicao)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select IDVenda Voucher, Nome, CPF, NumeroCota Cota, count(*) [Núm. Ingressos], sum(Valor) [Valor total] ");
            sSql.Append(" from vVendaVoucher ");
            sSql.Append(" where DataHoraCompra between '" + sDataInicial + "' and '" + sDataFinal + "' ");
            sSql.Append(" and IDEdicao = " + iIDEdicao + " ");
            sSql.Append(" and FormaPagamento = 'Cortesia' ");
            sSql.Append(" group by IDVenda, Nome, CPF, NumeroCota ");
            sSql.Append(" order by IDVenda desc ");

            return sSql.ToString();
        }

        internal string RetornarEntradaIngresso(string sNome, int iIDEdicao, int iIDVenda, string sStatus)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" select * ");
            sSql.Append(" from vEntradaIngresso ");
            sSql.Append(" where IDEdicao = " + iIDEdicao + " ");

            if (sStatus == "Processado (já entrou)") sSql.Append(" and DataHoraEntradaIngresso is not null ");
            else if (sStatus == "Não processado") sSql.Append(" and DataHoraEntradaIngresso is null ");

            if (!String.IsNullOrEmpty(sNome))
                sSql.Append(" and Nome like '%" + sNome + "%' ");

            if (iIDVenda != -1 && iIDVenda != 0)
                sSql.Append(" and IDVenda = " + iIDVenda + " ");

            return sSql.ToString();
        }

        internal string RetornarRelatorioVendasDetalhado(int iIDEdicao, string  sIDPerfil, int iIDFormaPagamento,int iIDLote, string sDataInicial, string sDataFinal)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" exec sRetornarVendasDetalhadoExcel ");
            sSql.Append(" @DataInicial = '" + sDataInicial + "', ");
            sSql.Append(" @DataFinal = '" + sDataFinal + "', ");
            sSql.Append(" @IDLote = " + iIDLote + ", ");
            sSql.Append(" @IDPerfil = '" + sIDPerfil + "', ");
            sSql.Append(" @IDFormaPagamento = " + iIDFormaPagamento + ", ");
            sSql.Append(" @IDEdicao = " + iIDEdicao + " ");

            return sSql.ToString();
        }

        internal string RetornarVendasCondominio(int iIDEdicao, string sDataInicial, string sDataFinal, int iExportando)
        {
            StringBuilder sSql = new StringBuilder();
            sSql.Append(" exec sRetornarVendasCondominio ");
            sSql.Append(" @IDEdicao = " + iIDEdicao + ", ");
            sSql.Append(" @DataInicial = '" + sDataInicial + "', ");
            sSql.Append(" @DataFinal = '" + sDataFinal + "', ");
            sSql.Append(" @Exportando = " + iExportando + " ");

            return sSql.ToString();
        }
    }
}



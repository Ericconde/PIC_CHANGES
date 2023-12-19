using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.objetos;
using Model.dados;
using System.Data;
using System.Web;

namespace Model.negocios
{
    public class eventoBLL
    {
        public void CadastrarEdicao(evento Evento)
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.CadastrarEdicao(Evento);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public void CadastrarLote(evento Evento)
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.CadastrarLote(Evento);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public DataTable AtualizarEdicao(evento Evento)
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.AtualizarEdicao(Evento);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable AtualizarLote(evento Evento)
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.AtualizarLote(Evento);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarEdicao(int iIDEdicao)
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.RetornarEdicao(iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarEdicao(int iIDEvento, string sEdicao)
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.RetornarEdicao(iIDEvento, sEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarLote(int iIDLote)
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.RetornarLote(iIDLote);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public DataTable RetornarEventos()
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.RetornarEventos();

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public string RetornarLotes(int iIDEdicao, bool bApenasLoteInicioFimVenda)
        {
            eventoDAL DEvento = new eventoDAL();
            return DEvento.RetornarLotes(iIDEdicao, bApenasLoteInicioFimVenda);
        }

        /*public DataTable RetornarLoteDisponivelParaVenda(int iIDEdicao)
        {
            eventoDAL DEvento = new eventoDAL();
            return PontoBr.Banco.SqlServer.RetornarDataTable(DEvento.RetornarLoteDisponivelParaVenda(iIDEdicao));
        }*/

        public string RetornarEventosEdicao()
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.RetornarEventosEdicao();

            return sSql;
        }

        public string RetornarEventosEdicaoVenda()
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.RetornarEventosEdicaoVenda();

            return sSql;
        }

        public string RetornarEventosVenda()
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.RetornarEventosVenda();

            return sSql;
        }

        public string RetornarEventosVenda(string sPerfil)
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.RetornarEventosVenda(sPerfil);

            return sSql;
        }

        public string RetornarEventosVendaAdministrador()
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.RetornarEventosVendaAdministrador();

            return sSql;
        }

        public void CadastrarLogEdicao(int iIDEdicao, string sTextoLog, int iIDUsuarioCadastro)
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.CadastrarLogEdicao(iIDEdicao, sTextoLog, iIDUsuarioCadastro);

            PontoBr.Banco.SqlServer.ExecutarSql(sSql);
        }

        public DataTable RetornarLogEdicao(int iIDEdicao)
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.RetornarLogEdicao(iIDEdicao);

            return PontoBr.Banco.SqlServer.RetornarDataTable(sSql);
        }

        public bool VerificarExistenciaLote(int iIDLote, int iIDEdicao, int iLote)
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.VerificarExistenciaLote(iIDLote, iIDEdicao, iLote);

            return PontoBr.Banco.SqlServer.VerificarExistenciaDeDados(sSql);
        }

        public bool VerificarExistenciaEdicao(int iIDEdicao, int iIDEvento, string sEdicao)
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.VerificarExistenciaEdicao(iIDEdicao, iIDEvento, sEdicao);

            return PontoBr.Banco.SqlServer.VerificarExistenciaDeDados(sSql);
        }

        public int RetornarTotalCadeirasEvento(int iIDEvento)
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.RetornarTotalCadeirasEvento(RetornarTotalCadeirasEvento(iIDEvento));

            return Convert.ToInt32(PontoBr.Banco.SqlServer.RetornarDadoUnicoDoBanco(sSql));
        }

        public string RetornarLocaisEvento()
        {
            eventoDAL DEvento = new eventoDAL();
            string sSql = DEvento.RetornarLocaisEvento();

            return sSql;
        }
    }
}

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
    public class catracaCTL
    {
        public void LimparBaseCatracaFisica(int iIDEdicao, int iIDUsuario)
        {
            catracaBLL BCatraca = new catracaBLL();
            BCatraca.LimparBaseCatracaFisica(iIDEdicao, iIDUsuario);
        }

        public DataTable RetornarIngressosCatracaFisica(int iIDEdicao)
        {
            catracaBLL BCatraca = new catracaBLL();
            return BCatraca.RetornarIngressosCatracaFisica(iIDEdicao);
        }

        public void DarCargaIngressosApp(int iIDEdicao, int iIDUsuario)
        {
            catracaBLL BCatraca = new catracaBLL();
            BCatraca.DarCargaIngressosApp(iIDEdicao, iIDUsuario);
        }
        
        public DataSet RetornarIngressosApp(int iIDEdicao, string sTipo, string sStatus)
        {
            catracaBLL BCatraca = new catracaBLL();
            return BCatraca.RetornarIngressosApp(iIDEdicao, sTipo, sStatus);
        }

        public void AtualizarRetornoCatracaFisica(string sIdentidadeEletronica, string sDataEntrada, string sCatraca)
        {
            catracaBLL BCatraca = new catracaBLL();
            BCatraca.AtualizarRetornoCatracaFisica(sIdentidadeEletronica, sDataEntrada, sCatraca);
        }

        public void AtualizarRetornoApp(string sIdentidadeEletronica, string sDataEntrada, string sIP, int iIDUsuario)
        {
            catracaBLL BCatraca = new catracaBLL();
            BCatraca.AtualizarRetornoApp(sIdentidadeEletronica, sDataEntrada, sIP, iIDUsuario);
        }

        public DataTable RetornarIngressoApp(string sIdentidadeEletronica)
        {
            catracaBLL BCatraca = new catracaBLL();
            return BCatraca.RetornarIngressoApp(sIdentidadeEletronica);
        }

        public DataTable RetornarIngressoApp(int iTicket)
        {
            catracaBLL BCatraca = new catracaBLL();
            return BCatraca.RetornarIngressoApp(iTicket);
        }

        public DataTable VerificarIngressoPosCatraca(string sIDentidadeEletronica)
        {
            catracaBLL BCatraca = new catracaBLL();
            return BCatraca.VerificarIngressoPosCatraca(sIDentidadeEletronica);
        }

        public void AtualizarIngressoPosCatraca(string sIDentidadeEletronica, int iIDUsuario)
        {
            catracaBLL BCatraca = new catracaBLL();
            BCatraca.AtualizarIngressoPosCatraca(sIDentidadeEletronica, iIDUsuario);
        }
    }
}

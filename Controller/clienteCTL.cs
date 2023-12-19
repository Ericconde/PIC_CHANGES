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
    public class clienteCTL
    {
        public void CadastrarCliente(cliente Cliente)
        {
            clientesBLL BClientes = new clientesBLL(); 
            BClientes.CadastrarCliente(Cliente);
        }
        
        public void CarregarDropDownEstado(DropDownList dropEstado)
        {
            clientesBLL BClientes = new clientesBLL();
            string sSql = BClientes.RetornarEstados();

            PontoBr.Utilidades.WCL.CarregarDropDown(dropEstado, sSql, "ESTADO", "IDESTADO", false, true);
        }

        public void CarregarDropDownCidade(DropDownList dropCidade, int iIDEstado)
        {
            clientesBLL BClientes = new clientesBLL();
            string sSql = BClientes.RetornarCidades(iIDEstado);

            PontoBr.Utilidades.WCL.CarregarDropDown(dropCidade, sSql, "CIDADE", "IDCIDADE", false, true);
        }

        public cliente RetornarCliente(int iIDusuario)
        {
            clientesBLL BClientes = new clientesBLL();
            return BClientes.RetornarCliente(iIDusuario);
        }

        public cliente RetornarCliente(string sNome)
        {
            clientesBLL BClientes = new clientesBLL();
            return BClientes.RetornarCliente(sNome);
        }

        public void AtualizarCliente(cliente Cliente)
        {
            clientesBLL BClientes = new clientesBLL();
            BClientes.AtualizarCliente(Cliente);
        }

        public bool VerificarExistenciaCPF(string sCPF, int iIDUsuario)
        {
            clientesBLL BClientes = new clientesBLL();
            return BClientes.VerificarExistenciaCPF(sCPF, iIDUsuario);
        }

        public bool VerificarExistenciaCPF(string sCPF)
        {
            clientesBLL BClientes = new clientesBLL();
            return BClientes.VerificarExistenciaCPF(sCPF);
        }

        public void CarregarGridViewClientes(GridView grdCliente, string sNome, string sCPF, string sEmail, int iNumeroCota, string sPerfil)
        {
            clientesBLL BClientes = new clientesBLL();

            grdCliente.DataSource = BClientes.RetornarClientes(sNome, sCPF, sEmail, iNumeroCota, sPerfil);
            grdCliente.DataBind();
        }

        public void CadastrarAlteracaoDados(string sTipoRegistro, string sRegistroAntigo, string sRegistroAtual, int iIDCliente)
        {
            clientesBLL BClientes = new clientesBLL();
            BClientes.CadastrarAlteracaoDados(sTipoRegistro, sRegistroAntigo, sRegistroAtual, iIDCliente);
        }

        public void CarregarAlteracoesDados(GridView grdHistoricoCadastro, int iIDCliente)
        {
            clientesBLL BClientes = new clientesBLL();
            DataTable dtSocio = BClientes.RetornarAlteracoesDados(iIDCliente);

            grdHistoricoCadastro.DataSource = dtSocio;
            grdHistoricoCadastro.DataBind();
        }
    }
}

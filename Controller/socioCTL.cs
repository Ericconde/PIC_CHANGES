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
    public class socioCTL
    {
        public socio RetornarSocio(string sNumeroCota, string sCPF)
        {
            socioBLL BSocio = new socioBLL();
            return BSocio.RetornarSocio(sNumeroCota, sCPF); 
        }

        public int RetornarIDEstado(string sEstado)
        {
            socioBLL BSocio = new socioBLL();
            return BSocio.RetornarIDEstado(sEstado);
        }

        public int RetornarIDCidade(int iIDEstado, string sCidade)
        {
            socioBLL BSocio = new socioBLL();
            return BSocio.RetornarIDCidade(iIDEstado, sCidade);
        }

        public bool VerificarExistenciaSocio(int iNumeroCota, string sDigitoCota, string sCPF)
        {
            socioBLL BSocio = new socioBLL();
            return BSocio.VerificarExistenciaSocio(iNumeroCota, sDigitoCota, sCPF);
        }

        public bool VerificarExistenciaSocio(int iNumeroCota, string sCPF)
        {
            socioBLL BSocio = new socioBLL();
            return BSocio.VerificarExistenciaSocio(iNumeroCota, sCPF);
        }

        public DataTable RetornarDigitos(string sNumeroCota)
        {
            socioBLL BSocio = new socioBLL();
            return BSocio.RetornarDigitos(sNumeroCota);
        }

        public void AtualizarDebito(string sNumeroCota)
        {
            socioBLL BSocio = new socioBLL();
            BSocio.AtualizarDebito(sNumeroCota);
        }

        public void AtualizarBaseSocios()
        {
            socioBLL BSocio = new socioBLL();
            BSocio.AtualizarBaseSocios();
        }
    }
}

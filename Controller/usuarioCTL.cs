using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.objetos;
using System.Data;
using System.Web;
using Model.negocios;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Controller
{
    public class usuarioCTL
    {
        public bool VerificarExistenciaEmail(string sEmail, int iIDUsuario)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            string sSql = BUsuario.VerificarExistenciaEmail(sEmail, iIDUsuario);

            if (PontoBr.Banco.SqlServer.VerificarExistenciaDeDados(sSql))
                return true;
            else
                return false;
        }

        public bool VerificarSenha(int iIDUsuario, string sSenha)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            string sSql = BUsuario.VerificarSenha(iIDUsuario, sSenha);

            if (PontoBr.Banco.SqlServer.VerificarExistenciaDeDados(sSql))
                return true;
            else
                return false;
        }

        public bool RetornarUsuario(string sEmail, string sSenha)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            DataTable dataTable = BUsuario.RetornarUsuario(sEmail, sSenha);
            if (dataTable.Rows.Count > 0)
            {
                RetornarUsuario(dataTable);
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable RetornarUsuario(string sEmail, string sSenha, int iIDPerfil)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            return BUsuario.RetornarUsuario(sEmail, sSenha, iIDPerfil);
        }

        public void RetornarUsuario(DataTable dataTable)
        {
            usuario Usuario = new usuario();
            Usuario.IDUsuario = Convert.ToInt32(dataTable.Rows[0]["IDUsuario"].ToString());
            Usuario.Nome = dataTable.Rows[0]["Nome"].ToString();
            Usuario.Email = dataTable.Rows[0]["Email"].ToString();
            Usuario.Senha = dataTable.Rows[0]["Senha"].ToString();
            Usuario.IDPerfil = Convert.ToInt32(dataTable.Rows[0]["IDPerfil"].ToString());
            Usuario.Perfil = dataTable.Rows[0]["Perfil"].ToString();
            Usuario.CPF = dataTable.Rows[0]["CPF"].ToString();
            Usuario.RG = dataTable.Rows[0]["RG"].ToString();
            Usuario.ResetarSenha = Convert.ToInt32(dataTable.Rows[0]["ResetarSenha"]);
            Usuario.IDCliente = String.IsNullOrEmpty(dataTable.Rows[0]["IDCliente"].ToString()) ? -1 : Convert.ToInt32(dataTable.Rows[0]["IDCliente"].ToString());

            Usuario.Debito = !String.IsNullOrEmpty(dataTable.Rows[0]["Debito"].ToString()) ? Convert.ToInt32(dataTable.Rows[0]["Debito"]) : 0;
            Usuario.Dependentes = !String.IsNullOrEmpty(dataTable.Rows[0]["Dependentes"].ToString()) ? Convert.ToInt32(dataTable.Rows[0]["Dependentes"]) : 0;
            Usuario.NumeroCota = !String.IsNullOrEmpty(dataTable.Rows[0]["NumeroCota"].ToString()) ? Convert.ToInt32(dataTable.Rows[0]["NumeroCota"]) : 0;
            Usuario.DigitoCota = dataTable.Rows[0]["DigitoCota"].ToString();
            Usuario.Token = dataTable.Rows[0]["Token"].ToString();

            HttpContext.Current.Session["Usuario"] = Usuario;
        }

        public static bool PermitirAcesso(bool bAdministrador, bool bVendedor, bool bSocio, bool bNaoSocio, bool bContabilidade, bool bClube, bool bFinanceiro, bool bPortaria, bool bFisco,
            string sIP, string sPagina)
        {
            if (HttpContext.Current.Session["usuario"] == null)
                return false;

            usuario Usuario = new usuario();
            Usuario = (usuario)HttpContext.Current.Session["Usuario"];

            //Log de acesso
            CadastrarLogAcesso(Usuario.IDUsuario, sIP, sPagina, null);

            if (Usuario.Perfil == "Administrador")
            {
                if (bAdministrador == false)
                    return false;
            }
            else if (Usuario.Perfil == "Vendedor")
            {
                if (bVendedor == false)
                    return false;
            }
            else if (Usuario.Perfil == "Sócio")
            {
                if (bSocio == false)
                    return false;
            }
            else if (Usuario.Perfil == "Não Sócio")
            {
                if (bNaoSocio == false)
                    return false;
            }
            else if (Usuario.Perfil == "Contabilidade")
            {
                if (bContabilidade == false)
                    return false;
            }
            else if (Usuario.Perfil == "Clube")
            {
                if (bClube == false)
                    return false;
            }
            else if (Usuario.Perfil == "Financeiro")
            {
                if (bFinanceiro == false)
                    return false;
            }
            else if (Usuario.Perfil == "Portaria")
            {
                if (bPortaria == false)
                    return false;
            }
            else if (Usuario.Perfil == "Fisco")
            {
                if (bFisco == false)
                    return false;
            }
            return true;
        }

        public void AlterarSenha(string sEmail, string sSenha, int iResetarSenha)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            BUsuario.AlterarSenha(sEmail, sSenha.ToLower(),iResetarSenha);
        }

        public void ExcluirUsuario(int iIDUsuario)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            BUsuario.ExcluirUsuario(iIDUsuario);
        }

        public int CadastrarUsuario(usuario Usuario)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            return BUsuario.CadastrarUsuario(Usuario);
        }

        public usuario RetornarUsuario(int iIDUsuario)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            DataTable dataTable = BUsuario.RetornarUsuario(iIDUsuario);

            usuario Usuario = new usuario();
            if (dataTable.Rows.Count != 0)
            {
                Usuario.IDUsuario = iIDUsuario;
                Usuario.Nome = dataTable.Rows[0]["Nome"].ToString();
                Usuario.Email = dataTable.Rows[0]["Email"].ToString();
                Usuario.Senha = dataTable.Rows[0]["Senha"].ToString();
                Usuario.Ativo = Convert.ToInt32(dataTable.Rows[0]["Ativo"]);
                Usuario.DataCadastro = dataTable.Rows[0]["Cadastro"].ToString();
                Usuario.Perfil = dataTable.Rows[0]["Perfil"].ToString();
                Usuario.IDPerfil = Convert.ToInt32(dataTable.Rows[0]["IDPerfil"].ToString());
                Usuario.CPF = dataTable.Rows[0]["CPF"].ToString();
                Usuario.RG = dataTable.Rows[0]["RG"].ToString();
                Usuario.ResetarSenha = Convert.ToInt32(dataTable.Rows[0]["ResetarSenha"]);
                Usuario.IDCliente = String.IsNullOrEmpty(dataTable.Rows[0]["IDCliente"].ToString()) ? -1 : Convert.ToInt32(dataTable.Rows[0]["IDCliente"].ToString());

                Usuario.Debito = !String.IsNullOrEmpty(dataTable.Rows[0]["Debito"].ToString()) ? Convert.ToInt32(dataTable.Rows[0]["Debito"]) : 0;
                Usuario.Dependentes = !String.IsNullOrEmpty(dataTable.Rows[0]["Dependentes"].ToString()) ? Convert.ToInt32(dataTable.Rows[0]["Dependentes"]) : 0;
                Usuario.NumeroCota = !String.IsNullOrEmpty(dataTable.Rows[0]["NumeroCota"].ToString()) ? Convert.ToInt32(dataTable.Rows[0]["NumeroCota"]) : 0;
                Usuario.DigitoCota = dataTable.Rows[0]["DigitoCota"].ToString();
                Usuario.Token = dataTable.Rows[0]["Token"].ToString();
            }

            return Usuario;
        }public usuario RetornarUsuarioByEmail(string Email)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            DataTable dataTable = BUsuario.RetornarUsuarioByEmail(Email);

            usuario Usuario = new usuario();
            if (dataTable.Rows.Count != 0)
            {
                Usuario.IDUsuario = Convert.ToInt32( dataTable.Rows[0]["IDUsuario"]);
                Usuario.Nome = dataTable.Rows[0]["Nome"].ToString();
                Usuario.Email = dataTable.Rows[0]["Email"].ToString();
                Usuario.Senha = dataTable.Rows[0]["Senha"].ToString();
                Usuario.Ativo = Convert.ToInt32(dataTable.Rows[0]["Ativo"]);
                Usuario.DataCadastro = dataTable.Rows[0]["Cadastro"].ToString();
                Usuario.Perfil = dataTable.Rows[0]["Perfil"].ToString();
                Usuario.IDPerfil = Convert.ToInt32(dataTable.Rows[0]["IDPerfil"].ToString());
                Usuario.CPF = dataTable.Rows[0]["CPF"].ToString();
                Usuario.RG = dataTable.Rows[0]["RG"].ToString();
                Usuario.ResetarSenha = Convert.ToInt32(dataTable.Rows[0]["ResetarSenha"]);
                Usuario.IDCliente = String.IsNullOrEmpty(dataTable.Rows[0]["IDCliente"].ToString()) ? -1 : Convert.ToInt32(dataTable.Rows[0]["IDCliente"].ToString());

                Usuario.Debito = !String.IsNullOrEmpty(dataTable.Rows[0]["Debito"].ToString()) ? Convert.ToInt32(dataTable.Rows[0]["Debito"]) : 0;
                Usuario.Dependentes = !String.IsNullOrEmpty(dataTable.Rows[0]["Dependentes"].ToString()) ? Convert.ToInt32(dataTable.Rows[0]["Dependentes"]) : 0;
                Usuario.NumeroCota = !String.IsNullOrEmpty(dataTable.Rows[0]["NumeroCota"].ToString()) ? Convert.ToInt32(dataTable.Rows[0]["NumeroCota"]) : 0;
                Usuario.DigitoCota = dataTable.Rows[0]["DigitoCota"].ToString();
                Usuario.Token = dataTable.Rows[0]["Token"].ToString();
            }

            return Usuario;
        }

        public static void CadastrarLogAcesso(int iIDUsuario, string sIP, string sPagina, string sEvento)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            BUsuario.CadastrarLogAcesso(iIDUsuario, sIP, sPagina, sEvento);
        }

        public string EnviarEmailSenhaAcesso(string sEmail, string sSenha)
        {
            string sTitulo = "PIC Ingressos on-line - Dados para Acesso";

            StringBuilder sCorpo = new StringBuilder();
            sCorpo.Append("<table style='width:800px; font-family:Verdana;'> ");
            sCorpo.Append("<tr><td colspan='2' style='background-color:Black; color:White; height:40px; padding-left:15px; font-weight:bold; font-size:18px;'> ");
            sCorpo.Append("Dados para Acesso</td></tr> ");
            sCorpo.Append("<tr><td colspan='2' style='height:20px; background-color:White;'></td></tr> ");
            sCorpo.Append("<tr><td colspan='2' style=' font-size:13px; background-color:White;'>Seguem seus dados para acesso ao portal " + ConfigurationManager.AppSettings["EnderecoSistema"].ToString() + ":</td></tr> ");
            sCorpo.Append("<tr><td colspan='2' style='height:20px; background-color:White;'></td></tr> ");
            sCorpo.Append("<tr><td colspan='2' style='font-size:12px; background-color:White;'>E-mail: <b>" + sEmail + "</b></td></tr> ");
            sCorpo.Append("<tr><td colspan='2' style='font-size:12px; background-color:White;'>Senha: <b>" + sSenha + "</b></td></tr> ");
            sCorpo.Append("<tr><td colspan='2' style='height:20px; background-color:White;'></td></tr> ");
            sCorpo.Append("<tr><td style='width:50px; background-color:White;'><img src='http://www.ingressospic.com.br/images/logoPIC_email.png'/></td><td style='font-size:11px; width:750px; background-color:White;'>");
            sCorpo.Append("<b>" + ConfigurationManager.AppSettings["EnderecoSistema"].ToString() + "</b><br/>Mensagem gerada em " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "</td></tr></table> ");

            return PontoBr.Utilidades.Email.EnviarEmail(sTitulo, ConfigurationManager.AppSettings["RemetenteEmail"].ToString(), sEmail, sCorpo.ToString(), null, false);
        }

        public void AtualizarUsuario(usuario Usuario, int iIDUsuarioAlteracao)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            BUsuario.AtualizarUsuario(Usuario, iIDUsuarioAlteracao);
        }

        public bool VerificarExistenciaNumeroCota(int iNumeroCota, string sDigitoCota, int iIDUsuario)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            string sSql = BUsuario.VerificarExistenciaNumeroCota(iNumeroCota, sDigitoCota, iIDUsuario);

            if (PontoBr.Banco.SqlServer.VerificarExistenciaDeDados(sSql))
                return true;
            else
                return false;
        }

        public int CarregarGridViewUsuarios(GridView grdUsuarios, string sNome, string sEmail, int iIDPerfil, string sNumeroCota, string sCPF)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            DataTable dataTable = BUsuario.RetornarUsuarios(sNome, sEmail, iIDPerfil, sNumeroCota, sCPF);

            grdUsuarios.DataSource = dataTable;
            grdUsuarios.DataBind();

            return dataTable.Rows.Count;
        }

        public void CadastrarLogAlteracaoEmail(int iIDUsuario, string sEmailNovo, string sEmailAntigo, int iIDUsuarioAlteracao)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            BUsuario.CadastrarLogAlteracaoEmail(iIDUsuario, sEmailNovo, sEmailAntigo, iIDUsuarioAlteracao);
        }

        public void AtualizarToken(int iIDUsuario, string sToken)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            BUsuario.AtualizarToken(iIDUsuario, sToken);
        }

        public string RetornarIDUsuario(int iIDCliente)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            return BUsuario.RetornarIDUsuario(iIDCliente);
        }

        public usuario RetornarUsuarioAleatorio()
        {
            usuarioBLL BUsuario = new usuarioBLL();
            DataTable dataTable = BUsuario.RetornarUsuarioAleatorio();

            usuario Usuario = new usuario();
            Usuario.IDUsuario = Convert.ToInt32(dataTable.Rows[0]["IDUsuario"].ToString());
            Usuario.Nome = dataTable.Rows[0]["Nome"].ToString();
            Usuario.Email = dataTable.Rows[0]["Email"].ToString();
            Usuario.Senha = dataTable.Rows[0]["Senha"].ToString();
            Usuario.Ativo = Convert.ToInt32(dataTable.Rows[0]["Ativo"]);
            Usuario.DataCadastro = dataTable.Rows[0]["Cadastro"].ToString();
            Usuario.Perfil = dataTable.Rows[0]["Perfil"].ToString();
            Usuario.IDPerfil = Convert.ToInt32(dataTable.Rows[0]["IDPerfil"].ToString());
            Usuario.CPF = dataTable.Rows[0]["CPF"].ToString();
            Usuario.RG = dataTable.Rows[0]["RG"].ToString();
            Usuario.ResetarSenha = Convert.ToInt32(dataTable.Rows[0]["ResetarSenha"]);
            Usuario.IDCliente = String.IsNullOrEmpty(dataTable.Rows[0]["IDCliente"].ToString()) ? -1 : Convert.ToInt32(dataTable.Rows[0]["IDCliente"].ToString());

            Usuario.Debito = !String.IsNullOrEmpty(dataTable.Rows[0]["Debito"].ToString()) ? Convert.ToInt32(dataTable.Rows[0]["Debito"]) : 0;
            Usuario.Dependentes = !String.IsNullOrEmpty(dataTable.Rows[0]["Dependentes"].ToString()) ? Convert.ToInt32(dataTable.Rows[0]["Dependentes"]) : 0;
            Usuario.NumeroCota = !String.IsNullOrEmpty(dataTable.Rows[0]["NumeroCota"].ToString()) ? Convert.ToInt32(dataTable.Rows[0]["NumeroCota"]) : 0;
            Usuario.DigitoCota = dataTable.Rows[0]["DigitoCota"].ToString();
            Usuario.Token = dataTable.Rows[0]["Token"].ToString();

            return Usuario;
        }

        public bool VerificarExitenciaToken(string sToken, int iIDUsuario)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            return BUsuario.VerificarExitenciaToken(sToken, iIDUsuario);
        }

        public void AtivarUsuario(int iIDUsuario)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            BUsuario.AtivarUsuario(iIDUsuario);
        }

        public string RetornarSiteMenorCarga()
        {
            usuarioBLL BUsuario = new usuarioBLL();
            return BUsuario.RetornarSiteMenorCarga();
        }

        public void AtualizarSiteCarga(string sSiteCarga)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            BUsuario.AtualizarSiteCarga(sSiteCarga);
        }

        //Blacklist
        public void CadastrarBlackList(string sIP, string sMotivo, int iIDUsuario)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            BUsuario.CadastrarBlackList(sIP, sMotivo, iIDUsuario);
        }

        //Blacklist
        public void AtualizarBlackList(int iBloqueado, int iIDUsuarioAlteracao, string sIP)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            BUsuario.AtualizarBlackList(iBloqueado, iIDUsuarioAlteracao, sIP);
        }

        //Blacklist
        public bool VerificarIPBloqueadoBlackList(string sIP)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            return BUsuario.VerificarIPBloqueadoBlackList(sIP);
        }

        //Blacklist
        public DataTable RetonarIPLiberadoBlackList(string sIP)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            return BUsuario.RetonarIPLiberadoBlackList(sIP);
        }

        //Blacklist
        public DataTable RetornarBlackList(string sIP, string sNome, string sDataInicial, string sDataFinal)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            return BUsuario.RetornarBlackList(sIP, sNome, sDataInicial, sDataFinal);
        }

        //Blacklist - Bloqueia o usuário caso haja suspeita de fraude na compra com cartão de crédito
        public void BloquearUsuario(int iIDUsuario)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            BUsuario.BloquearUsuario(iIDUsuario);
        }

        public bool VerificarWhiteList(string sIP)
        {
            usuarioBLL BUsuario = new usuarioBLL();
            return BUsuario.VerificarWhiteList(sIP);
        }
    }
}

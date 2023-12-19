using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Model.objetos;
using Controller;
using System.Collections.Generic;
using System.Web.Hosting;
using System.Configuration;

namespace WebService
{
    /// <summary>
    /// Summary description for ingressospic
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ingressospic : System.Web.Services.WebService
    {
        [WebMethod]
        public usuario AutenticarUsuario(string email, string senha, string ip, string tipoingresso_a_ser_lido)
        {
            usuario Usuario = new usuario();
            try
            {
                if (String.IsNullOrEmpty(email))
                {
                    Usuario.autenticado = false;
                    Usuario.mensagem = "E-mail não informado";
                    return Usuario;
                }

                if (String.IsNullOrEmpty(senha))
                {
                    Usuario.autenticado = false;
                    Usuario.mensagem = "Senha não informada";
                    return Usuario;
                }

                if (String.IsNullOrEmpty(ip))
                {
                    Usuario.autenticado = false;
                    Usuario.mensagem = "IP não informado";
                    return Usuario;
                }

                PontoBr.Seguranca.Criptografia Criptografia = new PontoBr.Seguranca.Criptografia();
                string sChave = ConfigurationManager.AppSettings["Chave"].ToString();
                string sVetorInicializacao = ConfigurationManager.AppSettings["VetorInicializacao"].ToString();
                senha = Criptografia.Criptografar(senha, sChave, sVetorInicializacao);

                usuarioCTL CUsuario = new usuarioCTL();
                DataTable dataTable = CUsuario.RetornarUsuario(email, senha, 8); /*Portaria*/

                if (dataTable.Rows.Count != 0)
                {
                    Usuario.idusuario = Convert.ToInt32(dataTable.Rows[0]["IDUsuario"]);
                    Usuario.nome = dataTable.Rows[0]["Nome"].ToString();
                    Usuario.tipoingresso_a_ser_lido = tipoingresso_a_ser_lido;
                    //CUsuario.CadastrarLogAcesso(Usuario.idusuario, ip, null); /*Corrigir victor*/

                    Usuario.autenticado = true;
                    return Usuario;
                }
                else
                {
                    Usuario.autenticado = false;
                    Usuario.mensagem = "E-mail e/ou senha incorreto(s)";
                    return Usuario;
                }
            }
            catch (Exception ex)
            {
                Usuario.autenticado = false;
                Usuario.mensagem = "Erro na autenticação: " + ex.Message;
                return Usuario;
            }
        }

        [WebMethod]
        public ingresso RegistrarEntrada(string codigoqrcode, int idusuario, string ip, string tipoingresso_a_ser_lido)
        {
            ingresso Ingresso = new ingresso();
            try
            {
                if (String.IsNullOrEmpty(codigoqrcode)
                    || codigoqrcode.Trim().Length <= 5)
                {
                    Ingresso.entradaliberada = false;
                    Ingresso.mensagem = "Número de ingresso não informado";
                    return Ingresso;
                }

                if (idusuario == 0 || idusuario == -1)
                {
                    Ingresso.entradaliberada = false;
                    Ingresso.mensagem = "Código do usuário não informado";
                    return Ingresso;
                }

                if (String.IsNullOrEmpty(ip))
                {
                    Ingresso.entradaliberada = false;
                    Ingresso.mensagem = "IP não informado";
                    return Ingresso;
                }

                usuarioCTL CUsuario = new usuarioCTL();
                Model.objetos.usuario Usuario = CUsuario.RetornarUsuario(idusuario);
                if (Usuario.IDUsuario == 0 || Usuario.IDPerfil != 8) /*Portaria*/
                {
                    Ingresso.entradaliberada = false;
                    Ingresso.mensagem = "Código do usuário inválido";
                    return Ingresso;
                }

                catracaCTL CCatraca = new catracaCTL();
                DataTable dataTable = CCatraca.RetornarIngressoApp(codigoqrcode);

                if (dataTable.Rows.Count != 0)
                {
                    Ingresso.usuario = dataTable.Rows[0]["Usuario"].ToString();
                    Ingresso.cliente = dataTable.Rows[0]["Cliente"].ToString();
                    Ingresso.datahoraentrada = dataTable.Rows[0]["DataHoraEntrada"].ToString();
                    Ingresso.qrcode = dataTable.Rows[0]["IdentidadeEletronica"].ToString();
                    Ingresso.tipo = dataTable.Rows[0]["Tipo"].ToString();

                    if (tipoingresso_a_ser_lido == "Inteira" && Ingresso.tipo != "Inteira")
                    { 
                        Ingresso.entradaliberada = false;
                        Ingresso.mensagem = "O ingresso não é do tipo Inteira";
                        return Ingresso;
                    }
                    if (tipoingresso_a_ser_lido == "Meia" && Ingresso.tipo != "Meia")
                    {
                        Ingresso.entradaliberada = false;
                        Ingresso.mensagem = "O ingresso não é do tipo Meia";
                        return Ingresso;
                    }
                    if (tipoingresso_a_ser_lido == "Estacionamento" && Ingresso.tipo != "Estacionamento")
                    {
                        Ingresso.entradaliberada = false;
                        Ingresso.mensagem = "O ingresso não é do tipo Estacionamento";
                        return Ingresso;
                    }

                    if (String.IsNullOrEmpty(Ingresso.datahoraentrada))
                    {
                        CCatraca.AtualizarRetornoApp(Ingresso.qrcode, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ip, idusuario);

                        Ingresso.entradaliberada = true;
                        Ingresso.mensagem = "Entrada liberada";
                        return Ingresso;
                    }
                    else
                    {
                        Ingresso.entradaliberada = false;
                        Ingresso.mensagem = "Já entrou";                        
                        return Ingresso;
                    }
                }
                else
                {
                    Ingresso.entradaliberada = false;
                    Ingresso.mensagem = "Número de ingresso não localizado";
                    return Ingresso;
                }
            }
            catch (Exception ex)
            {
                Ingresso.entradaliberada = false;
                Ingresso.mensagem = "Erro no registro da entrada: " + ex.Message;
                return Ingresso;
            }
        }
    }
}


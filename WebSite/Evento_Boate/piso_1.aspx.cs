using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controller;
using System.Data;
using Model.objetos;
using System.Threading;
using System.Configuration;
using System.Xml;

public partial class eventos_Boate_piso_1 : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, true, true, true, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");

            LiberarCadeiras();

            AtualizarStatusCadeiras();
            AtualizarSaldo();
            CarregarNumeroIngressosCadeira();
            CarregarDropDownListValoresIngressosCadeiras(); 
        }
    }

    private void CarregarNumeroIngressosCadeira()
    {
        if (HttpContext.Current.Session["Venda"] == null)
        {
            ExibirMensagem("Sua sessão expirou! Faça novo acesso ao sistema");
            return;
        }

        venda Venda = (venda)HttpContext.Current.Session["Venda"];

        eventoCTL CEvento = new eventoCTL();
        evento Edicao = new evento();
        Edicao = CEvento.RetornarEdicao(Venda.IDEdicao);

        lblNumeroIngressosCadeira.Text = "Regra: Número de ingressos disponíveis<br/>em mesa por cota/título: " + Edicao.NumeroIngressosCadeira.ToString() + "";
    }

    private void AtualizarSaldo()
    {
        if (HttpContext.Current.Session["Venda"] == null)
        {
            ExibirMensagem("Sua sessão expirou! Faça novo acesso ao sistema");
            return;
        }

        venda Venda = (venda)HttpContext.Current.Session["Venda"];

        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];
        usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
        if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
            Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

        vendaCTL CVenda = new vendaCTL();
         
        saldo Saldo = new saldo();
        Saldo = CVenda.RetornarSaldoIngressos(Venda.IDEdicao, Cliente.NumeroCota, Cliente.IDCliente, Venda.IDVenda);

        lblSocio_valor.Text = Venda.IngressosPrecoSocio.ToString();
        if (Venda.IngressosPrecoSocio - (Saldo.QuantidadeValorSocioComprado + Saldo.QuantidadeValorSocioVendaAtual) >= 0)
            lblSocio_valor.Text = (Venda.IngressosPrecoSocio - (Saldo.QuantidadeValorSocioComprado + Saldo.QuantidadeValorSocioVendaAtual)).ToString();

        lblNaoSocio_valor.Text = Venda.IngressosPrecoNaoSocio.ToString();
        if (Venda.IngressosPrecoNaoSocio - (Saldo.QuantidadeValorNaoSocioComprado + Saldo.QuantidadeValorNaoSocioVendaAtual) >= 0)
            lblNaoSocio_valor.Text = (Venda.IngressosPrecoNaoSocio - (Saldo.QuantidadeValorNaoSocioComprado + Saldo.QuantidadeValorNaoSocioVendaAtual)).ToString();

        //Verifica se selecionou todas as cadeiras da mesa
        int iNumeroMinimoReserva = 0;
        if (Venda.Evento == "Festa Junina")
            iNumeroMinimoReserva = 6;
        else if (Venda.Evento == "Reveillon")
            iNumeroMinimoReserva = 4;
        else if (Venda.Evento == "Boate") // Verificar qual será o numero mínimo de reserva
            iNumeroMinimoReserva = 4;

        DataTable dataMesasSemReservaCompleta = CVenda.RetornarMesasSemReservaCompleta(Venda.IDVenda, iNumeroMinimoReserva);

        if (dataMesasSemReservaCompleta.Rows.Count > 0)
        {
            string sMensagemMesas = "";

            foreach (DataRow dataRow in dataMesasSemReservaCompleta.Rows)
            {
                if (!String.IsNullOrEmpty(sMensagemMesas)) sMensagemMesas += ", ";
                sMensagemMesas += dataRow["Mesa"].ToString();
            }

            sMensagemMesas = "Há cadeira(s) não selecionada(s) na(s) mesa(s): " + sMensagemMesas + ".\\n\\nA mesa é vendida fechada. É necessário selecionar todas as cadeiras da mesa.";
            LinkButtonAbrirMapaInteira.Attributes.Add("onclick", "alert('" + sMensagemMesas + "')");
            LinkButtonAbrirMapaInteira.OnClientClick = "";
        }
        else
        {
            LinkButtonAbrirMapaInteira.Attributes.Remove("onclick");
            LinkButtonAbrirMapaInteira.OnClientClick = "parent.jQuery.fancybox.close();";
        }
    }

    private void ReservarCadeira(int iMesa, int iCadeira)
    {
        if (HttpContext.Current.Session["Venda"] == null)
        {
            ExibirMensagem("Sua sessão expirou! Faça novo acesso ao sistema.");
            return;
        }
        //if (radTipoCadeira.SelectedValue == "")
        //{
        //    ExibirMensagem("Selecione o tipo de ingresso.");
        //    return;
        //}

        AtualizarStatusCadeiras();

        if (PodeReservar(iMesa, iCadeira))
        {
            venda Venda = (venda)HttpContext.Current.Session["Venda"];
            Venda.InteiraCadeira = true;
            vendaCTL CVenda = new vendaCTL();

            venda Cadeira = new venda();
            Cadeira.Evento = "Boate";
            Cadeira.Setor = "Piso 1";
            Cadeira.Mesa = iMesa;
            Cadeira.Cadeira = iCadeira;
            Cadeira.IDEdicao = Venda.IDEdicao;
            Cadeira.IDVenda = Venda.IDVenda;
            Cadeira.Lote = Venda.Lote;

            usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

            usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
            if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
                Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

            saldo Saldo = new saldo();
            Saldo = CVenda.RetornarSaldoIngressos(Venda.IDEdicao, Cliente.NumeroCota, Cliente.IDCliente, Venda.IDVenda);

            if (Cliente.Perfil == "Sócio")
            {
                evento Evento = new evento();
                eventoCTL CEvento = new eventoCTL();
                Evento = CEvento.RetornarEdicao(Venda.IDEdicao);

                int iTotalIngressosCarrinho = Saldo.QuantidadeComprado + Saldo.QuantidadeCarrinhoVendaAtual;
                int iNumeroIngressosPodeComprar = 0;

                if (Cliente.NumeroCota < 10000) /*Cota*/
                    iNumeroIngressosPodeComprar = Cliente.Dependentes + Evento.NumeroIngressoExtraSocioCota;
                else /*Título*/
                    iNumeroIngressosPodeComprar = Cliente.Dependentes + Evento.NumeroIngressoExtraSocioTitulo;

                if (Venda.InteiraCadeira)
                {
                    if (iTotalIngressosCarrinho >= iNumeroIngressosPodeComprar
                        && (Saldo.QuantidadeValorSocioVendaAtual + Saldo.QuantidadeValorSocioComprado) >= iNumeroIngressosPodeComprar)
                    {
                        Cadeira.Valor = Venda.ValorInteiraCadeiraNaoSocio;
                        Cadeira.IDTipoIngresso = 12;
                    }
                    else
                    {
                        Cadeira.Valor = Venda.ValorInteiraCadeiraSocio;
                        Cadeira.IDTipoIngresso = 11;
                    }
                }
                //else
                //{
                //    if (iTotalIngressosCarrinho >= iNumeroIngressosPodeComprar
                //        && Saldo.QuantidadeValorSocioVendaAtual >= iNumeroIngressosPodeComprar)
                //    {
                //        Cadeira.Valor = Venda.ValorMeiaCadeiraNaoSocio;
                //        Cadeira.IDTipoIngresso = 6;
                //    }
                //    else
                //    {
                //        Cadeira.Valor = Venda.ValorMeiaCadeiraSocio;
                //        Cadeira.IDTipoIngresso = 5;
                //    }
                //}
            }
            else if (Cliente.Perfil == "Não Sócio")
            {
                if (Venda.InteiraCadeira)
                {
                    Cadeira.Valor = Venda.ValorInteiraCadeiraNaoSocio;
                    Cadeira.IDTipoIngresso = 12;
                }
                //else
                //{
                //    Cadeira.Valor = Venda.ValorMeiaCadeiraNaoSocio;
                //    Cadeira.IDTipoIngresso = 6;
                //}
            }

            if (UsuarioLogado.Perfil == "Administrador"
                    && dropValorIngresso.SelectedValue != "-1"
                     && dropValorIngresso.Visible == true)
            {
                Cadeira.Valor = Convert.ToDouble(dropValorIngresso.SelectedValue);

                // REVER ESSA ETAPA
                if (dropValorIngresso.SelectedItem.ToString().IndexOf("Inteira Sócio") > -1)
                    Cadeira.IDTipoIngresso = 1;
                else if (dropValorIngresso.SelectedItem.ToString().IndexOf("Inteira Não Sócio") > -1)
                    Cadeira.IDTipoIngresso = 2;
                else if (dropValorIngresso.SelectedItem.ToString().IndexOf("Meia Sócio") > -1)
                    Cadeira.IDTipoIngresso = 5;
                else if (dropValorIngresso.SelectedItem.ToString().IndexOf("Meia Não Sócio") > -1)
                    Cadeira.IDTipoIngresso = 6;
            }

            CVenda.ReservarCadeira(Cadeira);
        }

        AtualizarStatusCadeiras();
        AtualizarSaldo();
    }

    private bool PodeReservar(int iMesa, int iCadeira)
    {
        venda Venda = (venda)HttpContext.Current.Session["Venda"];

        venda Cadeira = new venda();
        Cadeira.Evento = "Boate";
        Cadeira.Setor = "Piso 1";
        Cadeira.Mesa = iMesa;
        Cadeira.Cadeira = iCadeira;
        Cadeira.IDEdicao = Venda.IDEdicao;
        Cadeira.IDVenda = Venda.IDVenda;

        int iMesaSetor;
        int iCadeiraSetor;
        foreach (Control c in cadeiras.Controls)
        {
            if (c.GetType().ToString().Equals("System.Web.UI.WebControls.ImageButton"))
            {
                ImageButton imageButton = (ImageButton)c;

                if (imageButton.ID.IndexOf("ImageButton_mesa") > -1)
                {
                    iMesaSetor = Convert.ToInt32(imageButton.ID.Substring(17, 3));
                    iCadeiraSetor = Convert.ToInt32(imageButton.ID.Substring(21, 1));

                    if (iMesa == iMesaSetor
                        && iCadeira == iCadeiraSetor)
                    {
                        if (imageButton.ImageUrl.IndexOf("cadeiraBloqueada.png") > -1 || imageButton.ImageUrl.IndexOf("cadeiraReservada.png") > -1)
                        {
                            ExibirMensagem("Esta cadeira da mesa " + iMesa.ToString() + " não está disponível.");
                            return false;
                        }
                    }
                }
            }
        }

        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        usuario Cliente = (usuario)HttpContext.Current.Session["Usuario"];
        if (UsuarioLogado.Perfil == "Vendedor" || UsuarioLogado.Perfil == "Administrador")
            Cliente = (usuario)HttpContext.Current.Session["ClienteCompra"];

        vendaCTL CVenda = new vendaCTL();
        Cadeira = CVenda.RetornarCadeira(Cadeira);

        if ((Cadeira.IDStatusCadeira == 2 || Cadeira.IDStatusCadeira == 4)
            && Cadeira.IDCliente != Cliente.IDCliente)
        {
            ExibirMensagem("Esta cadeira da mesa " + iMesa.ToString() + " não está disponível.");
            return false;
        }
        if (Cadeira.IDStatusCadeira == 2 && Cadeira.IDCliente == Cliente.IDCliente)
        {
            CVenda.LiberarCadeira(Cadeira);
            return false;
        }
        if (Cadeira.IDStatusCadeira == 3)
        {
            ExibirMensagem("Esta cadeira da mesa " + iMesa.ToString() + " não está disponível.");
            return false;
        }

        eventoCTL CEvento = new eventoCTL();
        evento Edicao = new evento();
        Edicao = CEvento.RetornarEdicao(Venda.IDEdicao);

        saldo Saldo = new saldo();
        Saldo = CVenda.RetornarSaldoIngressos(Venda.IDEdicao, Cliente.NumeroCota, Cliente.IDCliente, Venda.IDVenda);

        if (UsuarioLogado.Perfil != "Administrador")
        {
            //Limite de ingressos tipo cadeira
            if ((Saldo.QuantidadeCadeiraComprado + Saldo.QuantidadeCadeiraVendaAtual + 1) > Edicao.NumeroIngressosCadeira)
            {
                ExibirMensagem("Você só pode comprar " + Edicao.NumeroIngressosCadeira.ToString() + " ingressos do tipo cadeira.");
                return false;
            }
            //Saldo para compras
            if ((Saldo.QuantidadeComprado + Saldo.QuantidadeCarrinhoVendaAtual) >= (Venda.IngressosPrecoSocio + Venda.IngressosPrecoNaoSocio)) // estava (Venda.IngressosPrecoSocio + Venda.IngressosPrecoNaoSocio) colocado NumeroIngressosCadeira=4 para teste
            {
                ExibirMensagem("Saldo indisponível, você não pode reservar mais cadeiras.");
                return false;
            }
        }

        //Verifica se selecionou todas as cadeiras da mesa
        int iNumeroMinimoReserva = 0;
        if (Venda.Evento == "Festa Junina")
            iNumeroMinimoReserva = 6;
        else if (Venda.Evento == "Reveillon")
            iNumeroMinimoReserva = 4;
        else if (Venda.Evento == "Boate") // verificar depois o numero minimo de reserva
            iNumeroMinimoReserva = 4;

        DataTable dataMesasSemReservaCompleta = CVenda.RetornarMesasSemReservaCompleta(Venda.IDVenda, iNumeroMinimoReserva, iMesa);
        if (dataMesasSemReservaCompleta.Rows.Count > 0)
        {
            ExibirMensagem("Há cadeira(s) não selecionada(s) na mesa: " + dataMesasSemReservaCompleta.Rows[0]["Mesa"].ToString() + ".\\n\\nA mesa é vendida fechada. É necessário selecionar todas as cadeiras da mesa.");
            return false;
        }

        return true;
    }

    private void AtualizarStatusCadeiras()
    {
        if (HttpContext.Current.Session["Venda"] == null)
        {
            ExibirMensagem("Sua sessão expirou! Faça novo acesso ao sistema");
            return;
        }

        venda Venda = (venda)HttpContext.Current.Session["Venda"];
        vendaCTL CVenda = new vendaCTL();

        DataTable dataTable = CVenda.RetornarStatusCadeiras(Venda.IDEdicao, "Piso 1");

        int iMesa;
        int iCadeira;

        foreach (Control c in cadeiras.Controls)
        {
            if (c.GetType().ToString().Equals("System.Web.UI.WebControls.ImageButton"))
            {
                ImageButton imageButton = (ImageButton)c;

                if (imageButton.ID.IndexOf("ImageButton_mesa") > -1)
                {
                    iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
                    iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

                    foreach (DataRow dataRowCadeira in dataTable.Rows)
                    {
                        if (Convert.ToInt32(dataRowCadeira["Mesa"].ToString()) == iMesa
                            && Convert.ToInt32(dataRowCadeira["Cadeira"].ToString()) == iCadeira)
                        {
                            imageButton.ImageUrl = "~/images/" + dataRowCadeira["Imagem"].ToString();

                            if (dataRowCadeira["IDStatusCadeira"].ToString() == "4") /*Bloqueada*/
                            {
                                if (dataRowCadeira["IDCliente"].ToString() == Venda.IDCliente.ToString())
                                    imageButton.ImageUrl = "~/images/cadeiraLivre.png";
                            }

                            if (dataRowCadeira["IDStatusCadeira"].ToString() == "2") /*Em processo de compra*/
                            {
                                if (dataRowCadeira["IDCliente"].ToString() != Venda.IDCliente.ToString())
                                    imageButton.ImageUrl = "~/images/cadeiraBloqueada.png";
                            }

                            if (dataRowCadeira["IDStatusCadeira"].ToString() == "5") /*Reservada*/
                            {
                                if (dataRowCadeira["IDCliente"].ToString() == Venda.IDCliente.ToString())
                                    imageButton.ImageUrl = "~/images/cadeiraReservada.png";
                            }
                        }
                    }
                }
            }
        }
    }

    private void ExibirMensagem(string sMensagem)
    {
        sMensagem = sMensagem.Replace("'", "");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alerta", "alert('" + sMensagem + "')", true);
    }

    private void CarregarDropDownListValoresIngressosCadeiras()
    {
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];

        if (HttpContext.Current.Session["Venda"] == null)
        {
            ExibirMensagem("Sua sessão expirou! Faça novo acesso ao sistema");
            return;
        }

        venda Venda = (venda)HttpContext.Current.Session["Venda"];

        eventoCTL CEvento = new eventoCTL();
        evento Edicao = new evento();
        Edicao = CEvento.RetornarEdicao(Venda.IDEdicao);

        if (UsuarioLogado.Perfil == "Administrador")
        {
            lblValor.Visible = true;
            dropValorIngresso.Visible = true;

            vendaCTL CVenda = new vendaCTL();
            CVenda.CarregarDropDownListValoresIngressosCadeiras(dropValorIngresso, Edicao.IDEdicao, Edicao.IDEvento, true);
        }
        else
        {
            lblValor.Visible = false;
            dropValorIngresso.Visible = false;
        }
    }
    protected void ImageButton_mesa_001_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_001_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_001_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_001_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_002_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_002_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_002_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_002_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_003_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_003_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_003_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_003_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_004_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_004_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_004_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_004_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_005_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_005_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_005_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_005_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_006_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_006_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_006_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_006_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_007_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_007_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_007_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_007_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_008_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_008_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_008_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_008_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_009_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_009_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_009_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_009_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_010_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_010_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_010_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_010_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_011_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_011_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_011_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_011_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_012_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_012_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_012_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_012_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_013_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_013_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_013_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_013_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_014_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_014_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_014_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_014_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_015_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_015_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_015_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_015_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_016_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_016_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_016_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_016_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_017_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_017_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_017_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_017_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_018_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_018_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_018_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_018_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_019_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_019_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_019_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_019_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_020_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_020_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_020_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_020_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_021_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_021_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_021_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_021_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_022_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_022_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_022_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_022_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_023_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_023_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_023_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_023_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_024_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_024_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_024_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_024_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_025_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_025_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_025_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_025_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_026_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_026_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_026_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_026_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_027_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_027_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_027_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_027_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_028_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_028_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_028_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_028_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_029_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_029_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_029_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_029_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    private void LiberarCadeiras()
    {
        vendaCTL CVenda = new vendaCTL();
        CVenda.LiberarCadeiras();
    }

}
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

public partial class eventos_festaJunina_setor_golodromo : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sPagina = Request.AppRelativeCurrentExecutionFilePath;
            string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

            if (!usuarioCTL.PermitirAcesso(true, true, true, true, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");

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
    }

    private void ReservarCadeira(int iMesa, int iCadeira)
    {
        if (HttpContext.Current.Session["Venda"] == null)
        {
            ExibirMensagem("Sua sessão expirou! Faça novo acesso ao sistema.");
            return;
        }
        if (radTipoCadeira.SelectedValue == "")
        {
            ExibirMensagem("Selecione o tipo de ingresso.");
            return;
        }

        AtualizarStatusCadeiras();
        
        if (PodeReservar(iMesa, iCadeira))
        {
            venda Venda = (venda)HttpContext.Current.Session["Venda"];
            Venda.InteiraCadeira = radTipoCadeira.SelectedValue.IndexOf("Inteiro") != -1 ? true : false;

            venda Cadeira = new venda();
            Cadeira.Evento = "Festa Junina";
            Cadeira.Setor = "Golódromo";
            Cadeira.Mesa = iMesa;
            Cadeira.Cadeira = iCadeira;
            Cadeira.IDEdicao = Venda.IDEdicao;
            Cadeira.IDVenda = Venda.IDVenda;
            Cadeira.Lote = Venda.Lote;

            vendaCTL CVenda = new vendaCTL();

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
                        && (Saldo.QuantidadeValorSocioVendaAtual + Saldo.QuantidadeValorSocioComprado)  >= iNumeroIngressosPodeComprar)
                    {
                        Cadeira.Valor = Venda.ValorInteiraCadeiraNaoSocio + Venda.ValorGolodromo;
                        Cadeira.IDTipoIngresso = 2;
                    }
                    else
                    {
                        Cadeira.Valor = Venda.ValorInteiraCadeiraSocio + Venda.ValorGolodromo;
                        Cadeira.IDTipoIngresso = 1;
                    }
                }
                else
                {
                    if (iTotalIngressosCarrinho >= iNumeroIngressosPodeComprar
                        && Saldo.QuantidadeValorSocioVendaAtual >= iNumeroIngressosPodeComprar)
                    {
                        Cadeira.Valor = Venda.ValorMeiaCadeiraNaoSocio + Venda.ValorGolodromo;
                        Cadeira.IDTipoIngresso = 6;
                    }
                    else
                    {
                        Cadeira.Valor = Venda.ValorMeiaCadeiraSocio + Venda.ValorGolodromo;
                        Cadeira.IDTipoIngresso = 5;
                    }
                }
            }
            else if (Cliente.Perfil == "Não Sócio")
            {
                if (Venda.InteiraCadeira)
                {
                    Cadeira.Valor = Venda.ValorInteiraCadeiraNaoSocio + Venda.ValorGolodromo;
                    Cadeira.IDTipoIngresso = 2;
                }
                else
                {
                    Cadeira.Valor = Venda.ValorMeiaCadeiraNaoSocio + Venda.ValorGolodromo;
                    Cadeira.IDTipoIngresso = 6;
                }
            }

            if (UsuarioLogado.Perfil == "Administrador"
                    && dropValorIngresso.SelectedValue != "-1"
                     && dropValorIngresso.Visible == true)
            {
                Cadeira.Valor = Convert.ToDouble(dropValorIngresso.SelectedValue);

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
        Cadeira.Evento = "Festa Junina";
        Cadeira.Setor = "Golódromo";
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
            if ((Saldo.QuantidadeComprado + Saldo.QuantidadeCarrinhoVendaAtual) >= (Venda.IngressosPrecoSocio + Venda.IngressosPrecoNaoSocio))
            {
                ExibirMensagem("Saldo indisponível, você não pode reservar mais cadeiras.");
                return false;
            }
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
        DataTable dataTable = CVenda.RetornarStatusCadeiras(Venda.IDEdicao, "Golódromo");

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
                                if (dataRowCadeira["IDCliente"].ToString() == Venda.IDCliente.ToString())//r
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

    protected void ImageButton_mesa_028_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_028_6_Click(object sender, ImageClickEventArgs e)
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
    protected void ImageButton_mesa_029_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_029_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_030_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_030_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_030_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_030_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_030_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_030_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_031_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_031_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_031_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_031_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_031_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_031_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }


    protected void ImageButton_mesa_032_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_032_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_032_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_032_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_032_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_032_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_033_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_033_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_033_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_033_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_033_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_033_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }



    protected void ImageButton_mesa_034_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_034_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_034_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_034_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_034_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_034_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_035_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_035_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_035_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_035_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_035_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_035_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_036_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_036_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_036_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_036_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_036_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_036_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_037_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_037_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_037_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_037_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_037_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_037_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_038_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_038_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_038_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_038_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_038_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_038_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_039_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_039_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_039_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_039_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_039_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_039_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_040_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_040_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_040_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_040_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_040_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_040_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_041_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_041_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_041_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_041_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_041_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_041_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_042_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_042_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_042_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_042_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_042_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_042_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_043_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_043_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_043_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_043_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_043_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_043_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_044_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_044_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_044_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_044_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_044_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_044_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_045_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_045_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_045_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_045_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_045_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_045_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_046_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_046_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_046_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_046_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_046_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_046_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_047_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_047_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_047_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_047_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_047_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_047_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_048_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_048_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_048_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_048_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_048_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_048_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_049_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_049_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_049_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_049_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_049_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_049_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_050_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_050_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_050_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_050_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_050_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_050_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_051_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_051_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_051_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_051_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_051_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_051_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_052_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_052_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_052_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_052_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_052_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_052_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_053_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_053_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_053_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_053_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_053_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_053_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_054_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_054_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_054_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_054_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_054_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_054_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_055_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_055_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_055_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_055_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_055_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_055_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }

    protected void ImageButton_mesa_056_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_056_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_056_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_056_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_056_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_056_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_057_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_057_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_057_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_057_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_057_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_057_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }

    protected void ImageButton_mesa_058_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_058_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_058_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_058_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_058_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_058_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }


    protected void ImageButton_mesa_059_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_059_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_059_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_059_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_059_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_059_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_060_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_060_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_060_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_060_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_060_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_060_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_061_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_061_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_061_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_061_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_061_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_061_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_062_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_062_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_062_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_062_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_062_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_062_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_063_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_063_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_063_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_063_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_063_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_063_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }



    protected void ImageButton_mesa_064_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_064_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_064_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_064_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_064_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_064_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_065_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_065_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_065_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_065_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_065_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_065_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_066_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_066_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_066_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_066_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_066_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_066_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_067_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_067_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_067_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_067_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_067_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_067_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_068_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_068_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_068_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_068_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_068_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_068_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_069_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_069_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_069_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_069_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_069_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_069_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_070_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_070_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_070_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_070_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_070_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_070_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_071_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_071_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_071_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_071_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_071_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_071_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_072_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_072_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_072_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_072_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_072_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_072_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_073_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_073_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_073_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_073_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_073_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_073_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_074_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_074_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_074_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_074_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_074_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_074_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_075_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_075_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_075_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_075_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_075_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_075_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_076_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_076_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_076_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_076_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_076_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_076_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_077_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_077_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_077_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_077_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_077_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_077_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_078_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_078_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_078_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_078_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_078_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_078_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_079_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_079_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_079_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_079_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_079_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_079_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_080_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_080_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_080_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_080_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_080_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_080_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_081_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_081_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_081_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_081_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_081_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_081_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_082_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_082_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_082_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_082_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_082_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_082_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_083_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_083_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_083_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_083_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_083_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_083_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_084_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_084_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_084_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_084_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_084_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_084_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_085_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_085_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_085_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_085_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_085_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_085_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }

    protected void ImageButton_mesa_086_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_086_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_086_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_086_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_086_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_086_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_087_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_087_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_087_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_087_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_087_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_087_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }

    protected void ImageButton_mesa_088_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_088_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_088_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_088_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_088_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_088_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }

    protected void ImageButton_mesa_089_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }

    protected void ImageButton_mesa_089_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }

    protected void ImageButton_mesa_089_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_089_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_089_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_089_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }


    protected void ImageButton_mesa_090_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_090_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_090_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_090_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_090_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_090_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }


    protected void ImageButton_mesa_091_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_091_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_091_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_091_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_091_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_091_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }


    protected void ImageButton_mesa_092_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_092_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_092_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_092_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_092_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_092_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }



    protected void ImageButton_mesa_093_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_093_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_093_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_093_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_093_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_093_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }


    protected void ImageButton_mesa_094_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_094_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_094_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_094_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_094_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_094_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }


    protected void ImageButton_mesa_095_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_095_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_095_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_095_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_095_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_095_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }


    protected void ImageButton_mesa_096_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_096_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_096_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_096_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_096_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_096_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }


    protected void ImageButton_mesa_097_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_097_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_097_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_097_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_097_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_097_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }


    protected void ImageButton_mesa_098_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_098_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_098_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_098_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_098_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);

    }
    protected void ImageButton_mesa_098_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }


    protected void ImageButton_mesa_099_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_099_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    
    protected void ImageButton_mesa_099_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_099_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_099_5_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    protected void ImageButton_mesa_099_6_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }

    private void CarregarDropDownListValoresIngressosCadeiras()
    {
        usuario UsuarioLogado = (usuario)HttpContext.Current.Session["Usuario"];
        if (UsuarioLogado.Perfil == "Administrador")
        {
            lblValor.Visible = true;
            dropValorIngresso.Visible = true;

            venda Venda = (venda)HttpContext.Current.Session["Venda"];

            vendaCTL CVenda = new vendaCTL();
            CVenda.CarregarDropDownListValoresIngressosCadeiras(dropValorIngresso, Venda.IDEdicao, true);
        }
        else
        {
            lblValor.Visible = false;
            dropValorIngresso.Visible = false;
        }
    }
}
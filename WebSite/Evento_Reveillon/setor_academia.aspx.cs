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

public partial class eventos_reveillon_setor_academia : App_Code.BaseWeb
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsPostBack)
            {
                string sPagina = Request.AppRelativeCurrentExecutionFilePath;
                string sIP = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];

                if (!usuarioCTL.PermitirAcesso(true, true, true, true, false, false, false, false, false, sIP, sPagina)) Response.Redirect("../login/logout.aspx");
            }

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
            vendaCTL CVenda = new vendaCTL();

            venda Cadeira = new venda();
            Cadeira.Evento = "Reveillon";
            Cadeira.Setor = "Academia";
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
                        Cadeira.Valor = Venda.ValorInteiraCadeiraNaoSocio + Venda.ValorAcademia;
                        Cadeira.IDTipoIngresso = 2;
                    }
                    else
                    {
                        Cadeira.Valor = Venda.ValorInteiraCadeiraSocio + Venda.ValorAcademia;
                        Cadeira.IDTipoIngresso = 1;
                    }
                }
                else
                {
                    if (iTotalIngressosCarrinho >= iNumeroIngressosPodeComprar
                        && Saldo.QuantidadeValorSocioVendaAtual >= iNumeroIngressosPodeComprar)
                    {
                        Cadeira.Valor = Venda.ValorMeiaCadeiraNaoSocio + Venda.ValorAcademia;
                        Cadeira.IDTipoIngresso = 6;
                    }
                    else
                    {
                        Cadeira.Valor = Venda.ValorMeiaCadeiraSocio + Venda.ValorAcademia;
                        Cadeira.IDTipoIngresso = 5;
                    }
                }
            }
            else if (Cliente.Perfil == "Não Sócio")
            {
                if (Venda.InteiraCadeira)
                {
                    Cadeira.Valor = Venda.ValorInteiraCadeiraNaoSocio + Venda.ValorAcademia;
                    Cadeira.IDTipoIngresso = 2;
                }
                else
                {
                    Cadeira.Valor = Venda.ValorMeiaCadeiraNaoSocio + Venda.ValorAcademia;
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
        Cadeira.Evento = "Reveillon";
        Cadeira.Setor = "Academia";
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

        //Verifica se selecionou todas as cadeiras da mesa
        int iNumeroMinimoReserva = 0;
        if (Venda.Evento == "Festa Junina")
            iNumeroMinimoReserva = 6;
        else if (Venda.Evento == "Reveillon")
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

        DataTable dataTable = CVenda.RetornarStatusCadeiras(Venda.IDEdicao, "Academia");

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
    protected void ImageButton_mesa_0100_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0100_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0100_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0100_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0101_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0101_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0101_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0101_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0102_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0102_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0102_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0102_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0103_1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0103_2_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0103_3_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0103_4_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0104_1_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0104_2_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0104_3_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
    protected void ImageButton_mesa_0104_4_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton imageButton = (ImageButton)sender;

        int iMesa = Convert.ToInt32(imageButton.ID.Substring(17, 3));
        int iCadeira = Convert.ToInt32(imageButton.ID.Substring(21, 1));

        ReservarCadeira(iMesa, iCadeira);
    }
}
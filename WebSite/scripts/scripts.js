$(document).ready(function () {
    /**
     * @description Função responsável por adicionar a classe rounded nos elementos img
     * @function addClassImg
     * @returns {void}
     * @example addClassImg();
     * @since 1.0.0
     * @version 1.0.0
     */
    $('.swiper .card').find('img').addClass("rounded");
    /**
     * @description Função responsável por adicionar a classe border-0 nos elementos img
     * @function addClassImg
     * @returns {void}
     * @example addClassImg();
     * @since 1.0.0
     * @version 1.0.0
     * 
     */
    $('.swiper .card').addClass('border-0');

    slideSwiper();

    /**
     * @description Função responsável pelo controle de quantidade
     * @function compras
     * @returns {void}
     * @example compras();
     * @since 1.0.0
     * @version 1.0.0
     */
    compras();

    // INICIALIZA A FUNÇÃO DE CARREGAR O CONTROLE DE QUANTIDADE E PARCELAS
    /**
     * @description Função responsável pelo controle de quantidade e parcelas
     * @function carregarControle
     * @returns {void}
     * @example carregarControle();
     * @since 1.0.0
     * @version 1.0.0
     */
    carregarControle();
});


/**
 * @description Função responsável pelo recarregamento da página quando a largura ou altura da tela for alterada
 * @function carregarControle
 * @returns {void}
 * @example carregarControle();
 * @since 1.0.0
 * @version 1.0.0
 */
$(window).resize(function () {

    // PEGA A LARGURA E ALTURA DA TELA
    var windowWidth = $(window).width();
    var windowHeight = $(window).height();
    if (windowWidth != $(window).width() || windowHeight != $(window).height()) {
        location.reload();
        return;
    }
});

// Função responsável pelo slide
/**
 * @description Função responsável pelo slide
 * @function slideSwiper
 * @returns {void}
 * @example slideSwiper();
 * @since 1.0.0
 * @version 1.0.0
 */
function slideSwiper() {
    if ($('.mySwiper').length) {

        let sizeWindow = $(window).width();
        let slidePreview = 3;
        if (sizeWindow < 768) {
            slidePreview = 1;
        }

        let swiper = new Swiper(".mySwiper", {
            slidesPerView: slidePreview,
            spaceBetween: 30,
            slidesPerGroup: slidePreview,
            loop: true,
            loopFillGroupWithBlank: true,
            pagination: {
                el: ".swiper-pagination",
                clickable: true,
            },
            navigation: {
                nextEl: ".swiper-button-next",
                prevEl: ".swiper-button-prev",
            },
        });

    }
}

// funcao para adicionar e remover quantidade nos botões minus e plus
/**
 * 
 * @description Função responsável pelo controle de quantidade
 * @function compras
 * @returns {void}
 * @example compras();
 * @since 1.0.0
 * @version 1.0.0
 */
function compras() {
    if ($('.ticket-quantity').length) {
        const minus = document.querySelectorAll('.minus');
        const plus = document.querySelectorAll('.plus');

        minus.forEach(btn => btn.addEventListener('click', () => {
            const input = btn.parentNode.querySelector('input');
            decrementValue(input);
        }));

        plus.forEach(btn => btn.addEventListener('click', () => {
            const input = btn.parentNode.querySelector('input');
            incrementValue(input);
        }));
    }
}

/**
 * @param {*} input 
 * @description Função responsável pelo decremento de quantidade
 * @function decrementValue
 * @returns {void}
 * @example decrementValue();
 * @since 1.0.0
 * @version 1.0.0
 * 
 */
function decrementValue(input) {
    let value = parseInt(input.value);
    if (value > 0) {
        value--;
    }
    input.value = value;
}

/**
 * 
 * @param {} input 
 * @description Função responsável pelo incremento de quantidade
 * @function incrementValue
 * @returns {void}
 * @example incrementValue();
 * @since 1.0.0
 * @version 1.0.0
 */
function incrementValue(input) {
    let value = parseInt(input.value);
    if (value < 100) {
        value++;
    }
    input.value = value;
}

// funcao para pegar o tamanho da lateral e adicionar no jumbo-checkout
/**
 * 
 * @returns {void}
 * @description Função responsável por pegar o tamanho da lateral e adicionar no jumbo-checkout
 * @function pegarTamanhoLateral
 * @example pegarTamanhoLateral();
 * @since 1.0.0
 * @version 1.0.0
 * 
 */
function pegarTamanhoLateral() {
    let tamanhoLateral = $(".ticket").width();
    return tamanhoLateral;
}

// funcao para carregar o controle de quantidade e parcelas
/**
 * 
 * @returns {void}
 * @description Função responsável por carregar o controle de quantidade e parcelas
 * @function carregarControle
 * @example carregarControle();
 * @since 1.0.0
 * @version 1.0.0
 *  
 */
function carregarControle() {
    var tamanhoLateral = pegarTamanhoLateral();
    if (tamanhoLateral) {
        var tamanhoLateral = tamanhoLateral.offsetWidth;
    }

    $(".evento").each(function () {
        if ($(".jumbo-checkout").length) {
            $(".jumbo-checkout").css("width", "calc(100% - " + tamanhoLateral + "px)");
        }
    });

    var totalCompra = 90;
    var totalAParcelar = 5;

    for (var inicial = 1; inicial <= totalAParcelar; inicial++) {
        var option = $("<option>");
        option.val(totalCompra / inicial);
        option.text(inicial + 'x de R$ ' + number_format(totalCompra / inicial, 2, ',', '.'));
        if ($("#parcelas").length) {
            $("#parcelas").append(option);
        }
    }

}

/**
 * 
 * @param {*} number 
 * @param {*} decimals 
 * @param {*} decPoint 
 * @param {*} thousandsSep 
 * @returns {string}
 * @description Função responsável por formatar o número
 * @function number_format
 * @example number_format();
 * @since 1.0.0
 * @version 1.0.0
 * 
 */
function number_format(number, decimals = 0, decPoint = '.', thousandsSep = ',') {
    // Strip all characters but numerical ones.
    number = (number + '').replace(/[^0-9+\-Ee.]/g, '');
    const n = !isFinite(+number) ? 0 : +number,
        prec = !isFinite(+decimals) ? 0 : Math.abs(decimals);
    return n.toFixed(prec).replace(/\B(?=(\d{3})+(?!\d))/g, thousandsSep).replace('.', decPoint);
}
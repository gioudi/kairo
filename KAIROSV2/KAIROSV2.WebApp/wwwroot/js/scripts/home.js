/*
* Pagina Principal 
*/
$(function () {

    $('.carousel.carousel-slider.carousel-indicators').carousel({
        fullWidth: true,
        indicators: true,
        duration: 1000
    });

    setInterval(function () {
        $('.carousel.carousel-slider.carousel-indicators').carousel('next');
    }, 15000);

});

$(document).ready(function () {

    $(".collapsible-body").css({ 'display': '' });
    $('.nav-list li a').removeClass('active');
    $('.listaPadre').removeClass('active');

});


export class SelectorMenu {

    public static SeleccionEnElMenu(): void {
        //Poner active a la opción especifica del menu
        let url = location.href;
        let contador = url.indexOf("/", 8);
        let cade = url.substr(contador, url.length);
        let selector = "a[href$='" + cade + "']";
        $('.nav-list li a').removeClass('active');

        //Desplegar segundo nivel
        $(".collapsible-body").css({ 'display': '' });
        $('.listaPadre').removeClass('active');
        $(selector).addClass('active');
        let identi = $(selector).attr('name');

        $('#' + identi).addClass('active'); // los que estan debajo de este, con una funcion poner .css({ 'display': 'block' })
        $("#" + identi + " .collapsible-body").css({ 'display': 'block' });

        //Desplegar primer nivel
        let identi2 = $('#' + identi).attr('name');
        $('#' + identi2).addClass('active');
        $("#" + identi2 + " #" + identi + " .collapsible-body").css({ 'display': 'block' });
        $("#" + identi2 + " .nivel2").css({ 'display': 'block' });
        $("#" + identi2 + " .collapsible-body").first().css({ 'display': 'block' });
    }
}
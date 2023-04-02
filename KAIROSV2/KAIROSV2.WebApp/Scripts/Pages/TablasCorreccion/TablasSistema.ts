import { IPage } from '../../Shared/Components/IPage';
import { TablaCorreccionPage } from './TablaCorreccion';
import { SelectorMenu } from '../../Shared/Utils/SelectorMenu';

import PerfectScrollbar from 'perfect-scrollbar';
import { Page } from '../../Core/Page';

//var PerfectScrollbar:any;

export class TablasSistemaPage extends Page {

    private Table: DataTables.Api;
    BaseURL: "/TablasCorreccion/Index";
    private tablaCorreccion: TablaCorreccionPage;

    constructor() {
        super();
        this.Init();
    }

    public Destroy() {
        super.Destroy();
        this.tablaCorreccion.Destroy();
    }

    public Init() {
        this.InicializarControles();
    }

    private InicializarControles() {
        SelectorMenu.SeleccionEnElMenu();
        this.tablaCorreccion = new TablaCorreccionPage(
            [
                { "data": "ApiObservado", "name": "ApiObservado", "title": "ApiObservado", render: $.fn.dataTable.render.number(',', '.', 2)},
                { "data": "Temperatura", "name": "Temperatura", render: $.fn.dataTable.render.number(',', '.', 2) },
                { "data": "ApiCorregido", "name": "ApiCorregido", render: $.fn.dataTable.render.number(',', '.', 2) }],
            "/ObtenerDatosCorreccion5b");

        this.InicializarSideBar();
        this.EventosSideBar();
    }
    private InicializarSideBar() {

        // Toggle class of sidenav
        let sideNav = M.Sidenav.init(document.getElementById("contact-sidenav"), {
            onOpenStart: function () {
                $("#sidebar-list").addClass("sidebar-show");
            },
            onCloseEnd: function () {
                $("#sidebar-list").removeClass("sidebar-show");
            }
        });

        $("#contact-sidenav li").on("click", function () {
            var $this = $(this);
            if (!$this.hasClass("sidebar-title")) {
                $("li").removeClass("active");
                $this.addClass("active");
            }
        });

        // Close other sidenav on click of any sidenav
        if ($(window).width() > 900) {
            $("#contact-sidenav").removeClass("sidenav");
        }

        //  Notifications & messages scrollable
        //if ($("#sidebar-list").length > 0) {
        //    var ps_sidebar_list = new PerfectScrollbar("#sidebar-list", {
        //        //theme: "dark",
        //        wheelPropagation: false
        //    });
        //}

        // Sidenav
        $("#tables-sidenav-trigger").on("click", function () {
            if ($(window).width() < 960) {
                //$(".sidenav").sidenav("close");
                //$(".app-sidebar").sidenav("close");
                if (sideNav.isOpen)
                    sideNav.close();
                else
                    sideNav.open();
            }
        });

        // for rtl
        if ($("html[data-textdirection='rtl']").length > 0) {
            // Toggle class of sidenav
            M.Sidenav.init(document.getElementById("contact-sidenav"), {
                edge: "right",
                onOpenStart: function () {
                    $("#sidebar-list").addClass("sidebar-show");
                },
                onCloseEnd: function () {
                    $("#sidebar-list").removeClass("sidebar-show");
                }
            });
        }

        $(window).on("resize", function () {
            resizetable();

            if ($(window).width() > 899) {
                $("#contact-sidenav").removeClass("sidenav");
            }

            if ($(window).width() < 900) {
                $("#contact-sidenav").addClass("sidenav");
            }
        });

        function resizetable() {
            $(".app-page .dataTables_scrollBody").css({
                maxHeight: $(window).height() - 420 + "px"
            });
        }
        resizetable();

        // For contact sidebar on small screen
        if ($(window).width() < 900) {
            $(".sidebar-left.sidebar-fixed").removeClass("animate fadeUp animation-fast");
            $(".sidebar-left.sidebar-fixed .sidebar").removeClass("animate fadeUp");
        }

    }

    private EventosSideBar() {
        let tabla5b = document.getElementById("table-5b");
        let tabla6b = document.getElementById("table-6b");
        let tabla6cAlcohol = document.getElementById("table-6c-alcohol");

        tabla5b.addEventListener('click', event => this.ConfigurarTabla5b());
        tabla6b.addEventListener('click', event => this.ConfigurarTabla6b());
        tabla6cAlcohol.addEventListener('click', event => this.ConfigurarTabla6cAlcohol());
    }

    private ConfigurarTabla5b() {
        this.tablaCorreccion.ReconfigureTable(
            [
                { "data": "ApiObservado", "name": "ApiObservado", "title": "ApiObservado", render: $.fn.dataTable.render.number(',', '.', 2) },
                { "data": "Temperatura", "name": "Temperatura", render: $.fn.dataTable.render.number(',', '.', 2) },
                { "data": "ApiCorregido", "name": "ApiCorregido", render: $.fn.dataTable.render.number(',', '.', 2) }],
            "/ObtenerDatosCorreccion5b", "5b")
    }

    private ConfigurarTabla6b() {
        this.tablaCorreccion.ReconfigureTable(
            [
                { "data": "ApiCorregido", "name": "ApiCorregido", "title": "Api Corregido", render: $.fn.dataTable.render.number(',', '.', 2) },
                { "data": "Temperatura", "name": "Temperatura", render: $.fn.dataTable.render.number(',', '.', 2) },
                { "data": "FactorCorreccion", "name": "FactorCorreccion", render: $.fn.dataTable.render.number(',', '.', 2) }],
            "/ObtenerDatosCorreccion6b", "6b")
    }

    private ConfigurarTabla6cAlcohol() {
        this.tablaCorreccion.ReconfigureTable(
            [
                { "data": "ApiCorregido", "name": "ApiCorregido", "title": "Api Corregido", render: $.fn.dataTable.render.number(',', '.', 2) },
                { "data": "Temperatura", "name": "Temperatura", render: $.fn.dataTable.render.number(',', '.', 2,) },
                { "data": "FactorCorreccion", "name": "FactorCorreccion", render: $.fn.dataTable.render.number(',', '.', 2) }],
            "/ObtenerDatosCorreccion6cAlcohol", "6cAlcohol")
    }

}

var tablasSistemaPage = new TablasSistemaPage();


// Called after every non-initial page load
document.addEventListener('turbolinks:render', (e: any) => {
    if (document.URL.indexOf('/TablasCorreccion/Index') != -1) {
        if (tablasSistemaPage) {
            tablasSistemaPage.Init();
        }
        else {
            tablasSistemaPage = new TablasSistemaPage();
        }
        //console.log(e.data.url);
    }
    else
        tablasSistemaPage?.Destroy();
});
import { HttpFetchService, IMessageResponse, Tanque } from '../../Shared';
import { ConfirmModalMessage } from '../../Shared/Components/ConfirmModalMessage';
import { ConfigureDataTable } from '../../Shared/Components/ConfigureDataTable';
import { TanquesGestionPage } from './GestionTanques';
import { JQValidations } from '../../Shared/Utils/JQValidations';
import { SelectorMenu } from '../../Shared/Utils/SelectorMenu';
import { IPage } from '../../Shared/Components/IPage';
import { MaskFormats, IMaskFormat } from '../../Shared/Utils/MaskFormats';
import IMask from "imask";
import { MaskFormatsManager } from '../../Shared/Utils';
import PerfectScrollbar from 'perfect-scrollbar';
import { format, parseISO } from 'date-fns';
import dayjs = require('dayjs');
import { Page } from '../../Core/Page';

export class TanquesPage extends Page {

    private Table: DataTables.Api;
    private modalInstance: M.Modal;
    private gestionTanques: TanquesGestionPage;

    constructor() {
        super();
        this.BaseUrl = "/Tanques/Index";
        this.Init();
        this.RegistrarFormatos();
    }

    public Destroy() {
        super.Destroy();
        this.Table.destroy(false);
    }

    public Init() {
        this.InicializarButtons();
        this.InicializarControls();
        this.GetPermissions("/Tanques/ObtenerPermisos");
    }

    private InicializarControls() {
        SelectorMenu.SeleccionEnElMenu();
        this.Table = new ConfigureDataTable().Configure("#data-table-tanques", [{ "width": "5%", "targets": 0 }]);
        this.gestionTanques = new TanquesGestionPage(document.getElementById("tanque-modal"));
        this.gestionTanques.onTanqueCreado = (tanque) => this.AgregarFilaDatatable(tanque);
        this.gestionTanques.onTanqueActualizado = (tanque) => this.ActualizarFilaDatatable(tanque);
        this.gestionTanques.onTanqueDetalle = (tanque) => this.DetallesTanque(tanque);
        JQValidations.MaxFileSizeValidation();
        JQValidations.AllowedExtensionsValidation();
    }

    private InicializarButtons() {
        var table = document.getElementById("data-table-tanques");
        var tanqueAdd = document.getElementById("tanque-add");
        var detailClose = document.getElementById("detail-close");
        var detailOverlay = $(".detail-overlay");
        var composeSidebar = $(".compose-sidebar");

        table.on('click', '.tanque-action', event => {
            if (event.target.matches(".tanque-edit"))
                this.EditarTanque(event.target as HTMLElement);

            else if (event.target.matches(".tanque-delete"))
                this.BorrarTanque(event.target as HTMLElement);

            else if (event.target.matches(".tanque-detail"))
                this.BuscarDetallesTanque(event.target as HTMLElement);
        });
        tanqueAdd.addEventListener('click', event => this.CrearTanque());
        detailClose.addEventListener('click', event => this.CerrarDetalle(detailOverlay, composeSidebar));
    }

    private async BorrarTanque(element: HTMLElement) {
        let tanqueId = element.dataset.tanqueid;
        let terminalId = element.dataset.terminalid;
        let terminal = element.dataset.terminal;
        var payload = { Tanque: tanqueId, IdTerminal: terminalId };
        const confirm = new ConfirmModalMessage("Eliminar Tanque", "¿Desea eliminar el Tanque " + tanqueId + " de la terminal " + terminal + "?", "Aceptar", "Cancelar");
        const shouldDelete = await confirm.Confirm();
        if (shouldDelete) {
            const httpService = new HttpFetchService();
            httpService.Post<IMessageResponse>('/Tanques/BorrarTanque', payload)
                .then((data) => {
                    if (data.Result)
                        this.BorrarFilaDatatable(element);
                    M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

                }).catch((error) => {
                    M.toast({ html: error, classes: "error" });
                });
        }
    }

    private CrearTanque() {
        this.gestionTanques.NuevoTanque();
    }

    private EditarTanque(element: HTMLElement): void {
        this.gestionTanques.DatosTanque(element.dataset.tanqueid, element.dataset.terminalid, false);
    }

    private BuscarDetallesTanque(element: HTMLElement): void {
        this.gestionTanques.DatosDetalleTanque(element.dataset.tanqueid, element.dataset.terminalid, true);

    }

    private fechaActual(fecha) {
        let date: Date = new Date(fecha);

        let dia = date.getDate()
        let mes = date.getMonth() + 1 + '';
        let year = date.getFullYear()

        let actual = `${dia}/${this.FormatoFecha(mes)}/${year}`;
        return actual;
    }

    private DetallesTanque(tanque: Tanque) {
        document.getElementById("body-html").style.overflow = "hidden";
        var detailOverlay = $(".detail-overlay");
        var composeSidebar = $(".compose-sidebar");
        detailOverlay.addClass("show");
        composeSidebar.addClass("show");
        let fechaAf = this.fechaActual(tanque.FechaAforo);

        $("#tanque").val(tanque.Tanque);
        $("#terminal").val(tanque.Terminal);
        $("#tipoTanque").val(tanque.TipoTanque);
        $("#capacidadNominal").val(tanque.CapacidadNominal.toLocaleString("en-US"));
        $("#capacidadOper").val(tanque.CapacidadOperativa.toLocaleString("en-US"));
        $("#volumenNoBombeable").val(tanque.VolumenNoBombeable.toLocaleString("en-US"));
        $("#alturaMaxAforo").val(tanque.AlturaMaximaAforo.toLocaleString("en-US"));
        $("#aforadoPor").val(tanque.AforadoPor);
        $("#fechaAforo").val(fechaAf+'');
        $("#observaciones").val(tanque.Observaciones);

        if (tanque.PantallaFlotante) {
            var switchPF = document.getElementById('pantallaFlotante'); 
            switchPF.setAttribute("checked", "");

            let ocultarDatosPF = document.getElementById('datosPantallaFlotant');
            ocultarDatosPF.removeAttribute("hidden");

            $("#densidadAforo").val(tanque.DensidadAforo.toLocaleString("en-US"));
            $("#nivelCorreccionInicial").val(tanque.NivelCorreccionInicial.toLocaleString("en-US"));
            $("#nivelCorreccionFinal").val(tanque.NivelCorreccionFinal.toLocaleString("en-US"));
            $("#galonesPorGrado").val(tanque.GalonesPorGrado.toLocaleString("en-US"));

        } else {
            var switchPF = document.getElementById('pantallaFlotante');
            switchPF.removeAttribute("checked");

            let ocultarDatosPF = document.getElementById('datosPantallaFlotant');
            ocultarDatosPF.setAttribute("hidden", "");
        }

        if (composeSidebar.length > 0) {
            var ps_compose_sidebar = new PerfectScrollbar(".compose-sidebar", {
                //theme: "dark",
                wheelPropagation: false
            });
        }
    }

    private CerrarDetalle(detailOverlay, composeSidebar) {
        detailOverlay.removeClass("show");
        composeSidebar.removeClass("show");
        document.getElementById("body-html").style.overflow = "auto";
    }

    //Funcion para borrar tanque del datatable
    private BorrarFilaDatatable(element: HTMLElement) {
        var $tr = $(element).closest("tr");
        if ($tr.prev().hasClass("parent")) {
            let row = this.Table.row($tr.prev());
            row.remove();
            row.draw();
        }
        let row = this.Table.row($tr);
        row.remove();
        row.draw();
    }

    // agregar un nuevo tanque
    private AgregarFilaDatatable(tanque: Tanque) {
        let icono = (tanque.IconoProducto != "") ?
            '<i style="color: ' + tanque.IconoColor + '" class="material-icons icono">' + tanque.IconoProducto + '</i>' :
            '<i class="material-icons icono">format_color_reset</i>';

        let newTanque = this.Table.row.add([
            tanque.Terminal,
            tanque.Tanque,
            icono,
            tanque.Producto,
            '<span class="bullet ' + tanque.ClaseColor + '"></span>' + tanque.ClaseTanque,
            '<span class="chip lighten-5 ' + tanque.EstadoColor + ' ' + tanque.EstadoColor + '-text">' + tanque.Estado + '</span>',
            '<a class="cursor-point" data-turbolinks="false">' +
            '<i class="material-icons tanque-action tanque-edit" data-tanqueid="' + tanque.Tanque + '" data-terminalid="' + tanque.IdTerminal + '" data-terminal="' + tanque.Terminal + '">edit</i>' +
            '</a>' +
            '<a class= "cursor-point" data-turbolinks="false" >' +
            '<i class="material-icons tanque-action tanque-delete" data-tanqueid="' + tanque.Tanque + '" data-terminalid="' + tanque.IdTerminal + '" data-terminal="' + tanque.Terminal + '">delete</i>' +
            '</a>' +
            '<a class= "cursor-point" data-turbolinks="false" >' +
            '<i class="material-icons tanque-action tanque-detail" data-tanqueid="' + tanque.Tanque + '" data-terminalid="' + tanque.IdTerminal + '" data-terminal="' + tanque.Terminal + '">remove_red_eye</i>' +
            '</a>'
        ]);
        (this.Table.row(newTanque).node() as HTMLElement).id = tanque.Tanque + "-" + tanque.IdTerminal;
        newTanque.draw(false);
    }

    // Actualizar un tanque
    private ActualizarFilaDatatable(tanque: Tanque) {
        let row = $("tr#" + tanque.Tanque + "-" + tanque.IdTerminal);
        var tanq = this.Table.row(row).data();
        if (tanque.IconoProducto != "") {
            tanq[2] = '<i style="color: ' + tanque.IconoColor + '" class="material-icons icono">' + tanque.IconoProducto + '</i>';
        } else {
            tanq[2] = '<i class="material-icons icono" >format_color_reset</i>';
        }
        tanq[3] = tanque.Producto;
        tanq[4] = '<span class="bullet ' + tanque.ClaseColor + '"></span>' + tanque.ClaseTanque;
        tanq[5] = '<span class="chip lighten-5 ' + tanque.EstadoColor + ' ' + tanque.EstadoColor + '-text">' + tanque.Estado + '</span>';
        this.Table.row(row).data(tanq);
    }

    //Formatear inputs
    private RegistrarFormatos() {
        //registrar mascaras 
        //this.RegisterMasks(
        //    [
        //        MaskFormats.IntegerFormat(),
        //        MaskFormats.DecimalFormat()
        //    ]
        //)
    }

    private FormatoFecha(fechaOriginal) {
        var MesActual = fechaOriginal;
        switch (MesActual) {
            case "ene":
                fechaOriginal = fechaOriginal.replace(MesActual, "ene");
                break;
            case "feb":
                fechaOriginal = fechaOriginal.replace(MesActual, "feb");
                break;
            case "mar":
                fechaOriginal = fechaOriginal.replace(MesActual, "mar");
                break;
            case "abr":
                fechaOriginal = fechaOriginal.replace(MesActual, "abr");
                break;
            case "may":
                fechaOriginal = fechaOriginal.replace(MesActual, "may");
                break;
            case "jun":
                fechaOriginal = fechaOriginal.replace(MesActual, "jun");
                break;
            case "jul":
                fechaOriginal = fechaOriginal.replace(MesActual, "jul");
                break;
            case "ago":
                fechaOriginal = fechaOriginal.replace(MesActual, "ago");
                break;
            case "sep":
                fechaOriginal = fechaOriginal.replace(MesActual, "sep");
                break;
            case "oct":
                fechaOriginal = fechaOriginal.replace(MesActual, "oct");
                break;
            case "nov":
                fechaOriginal = fechaOriginal.replace(MesActual, "nov");
                break;
            default:
                fechaOriginal = fechaOriginal.replace(MesActual, "dic");
                break;
        }
        return fechaOriginal;
    }

    //Obtiene clase disable dependiendo del permiso
    private DisableAction(habilitado: boolean): string {
        return habilitado ? "" : "disable-action";
    }

}

var tanquesPage = new TanquesPage();


// Called after every non-initial page load
document.addEventListener('turbolinks:render', (e: any) => {
    if (document.URL.indexOf('/Tanques/Index') != -1) {
        if (tanquesPage) {
            tanquesPage.Init();
        }
        else {
            tanquesPage = new TanquesPage();
        }
    }
    else
        tanquesPage?.Destroy();
});
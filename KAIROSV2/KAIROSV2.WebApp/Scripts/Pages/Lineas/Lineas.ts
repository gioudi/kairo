import { HttpFetchService, IMessageResponse, Linea } from '../../Shared';
import { ConfirmModalMessage } from '../../Shared/Components/ConfirmModalMessage';
import { ConfigureDataTable } from '../../Shared/Components/ConfigureDataTable';

import { LineasGestionPage } from './GestionLineas';
import { JQValidations } from '../../Shared/Utils/JQValidations';
import { SelectorMenu } from '../../Shared/Utils/SelectorMenu';

import { MaskFormats, IMaskFormat } from '../../Shared/Utils/MaskFormats';
import IMask from "imask";
import { MaskFormatsManager } from '../../Shared/Utils';
import { Page } from '../../Core/Page';


export class LineasPage extends Page {

    private Table: DataTables.Api;
    private modalInstance: M.Modal;
    private gestionLineas: LineasGestionPage;
    private _maskManager: MaskFormatsManager;
    

    constructor() {
        super();
        this.BaseUrl = "/Lineas/Index";
        this._maskManager = new MaskFormatsManager();
        this.Init();
        this.RegistrarFormatos();
    }

    public Destroy() {
        super.Destroy();
        this.Table.destroy(false);
        //this.Table.rows().invalidate().draw();
    }

    public Init() {

        this.InicializarButtons();
        this.InicializarControls();
        super.GetPermissions("/Lineas/ObtenerPermisos");
    }

    private InicializarControls() {

        SelectorMenu.SeleccionEnElMenu();
        this.Table = new ConfigureDataTable().Configure("#data-table-lineas", [{ "width": "5%", "targets": 0 }]);
        this.gestionLineas = new LineasGestionPage(document.getElementById("linea-modal"));
        this.gestionLineas.onLineasCreado = (linea) => this.AgregarFilaDatatable(linea);
        this.gestionLineas.onLineasActualizado = (linea) => this.ActualizarFilaDatatable(linea);
        JQValidations.MaxFileSizeValidation();
        JQValidations.AllowedExtensionsValidation();

    }

    private InicializarButtons() {

        var table = document.getElementById("data-table-lineas");
        var lineaAdd = document.getElementById("linea-add");

        table.on('click', '.linea-action', event => {

            if (event.target.matches(".linea-edit"))
                this.EditarLinea(event.target as HTMLElement);

            else if (event.target.matches(".linea-delete"))
                this.BorrarLinea(event.target as HTMLElement);

        });

        lineaAdd.addEventListener('click', event => this.CrearLinea());
    }

    private async BorrarLinea(element: HTMLElement) {

        let lineaId    = element.dataset.lineaid;
        let terminalId = element.dataset.terminalid;
        let terminal   = element.dataset.terminal;
        var payload    = { IdLinea: lineaId, IdTerminal: terminalId };

        const confirm = new ConfirmModalMessage("Eliminar Línea", "¿Desea eliminar la Línea " + lineaId + " de la terminal " + terminal + "?", "Aceptar", "Cancelar");
        const shouldDelete = await confirm.Confirm();

        if (shouldDelete) {

            const httpService = new HttpFetchService();
            httpService.Post<IMessageResponse>('/Lineas/BorrarLinea', payload)
                .then((data) => {
                    if (data.Result)
                        this.BorrarFilaDatatable(element);
                    M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

                }).catch((error) => {
                    M.toast({ html: error, classes: "error" });
                });
        }
    }

    private CrearLinea() {
        this.gestionLineas.NuevoLinea();
    }

    private EditarLinea(element: HTMLElement): void {
        this.gestionLineas.DatosLinea(element.dataset.lineaid, element.dataset.terminalid, true);
    }

    //Funcion para borrar linea del datatable
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

    // agregar una nueva linea
    private AgregarFilaDatatable(linea: Linea) {

        let newLinea = this.Table.row.add([
            linea.Terminal,
            linea.Idlinea,
            linea.Producto,
            '<span class="chip lighten-5 ' + linea.EstadoColor + ' ' + linea.EstadoColor + '-text">' + linea.Estado + '</span>',
            linea.Capacidad,
            linea.DensidadAforo,
            linea.Observaciones,
            '<a class="cursor-point ' + this.DisableAction(this.Permisions.Editar) + '" data-turbolinks="false">' +
            '<i class="material-icons linea-action linea-edit" data-lineaid="' + linea.Idlinea + '" data-terminalid="' + linea.IdTerminal + '" data-terminal="' + linea.Terminal + '">edit</i>' +
            '</a>' +
            '<a class="cursor-point ' + this.DisableAction(this.Permisions.Borrar) + '" data-turbolinks="false">' +
            '<i class="material-icons linea-action linea-delete" data-lineaid="' + linea.Idlinea + '" data-terminalid="' + linea.IdTerminal + '" data-terminal="' + linea.Terminal + '">delete</i>' +
            '</a>'
        ]);
        (this.Table.row(newLinea).node() as HTMLElement).id = linea.Idlinea + "-" + linea.IdTerminal;
        newLinea.draw(false);
    }

    // Actualizar una linea
    private ActualizarFilaDatatable(linea: Linea) {

        let row = $("tr#" + linea.Idlinea + "-" + linea.IdTerminal);
        var line = this.Table.row(row).data();
        line[0] = linea.Terminal;
        line[1] = linea.Idlinea;
        line[2] = linea.Producto;
        line[3] = '<span class="chip lighten-5 ' + linea.EstadoColor + ' ' + linea.EstadoColor + '-text">' + linea.Estado + '</span>';
        line[4] = linea.Capacidad;
        line[5] = linea.DensidadAforo;
        line[6] = linea.Observaciones;
        this.Table.row(row).data(line);
    }

    //Formatear inputs
    private RegistrarFormatos() {

        //registrar mascaras
        this._maskManager.RegisterMasks(
            [
                MaskFormats.IntegerFormat(),
                MaskFormats.DecimalFormat()
            ]);

        //registrar mascaras 
        //this._maskManager.RegisterMasks(
        //    [
        //        MaskFormats.IntegerFormatRang('#linea-Capacidad', 0, 5000000),
        //        MaskFormats.DecimalFormatRang('#linea-densidad', 0, 100),
        //    ]);
    }

    //Obtiene clase disable dependiendo del permiso
    private DisableAction(habilitado: boolean): string {
        return habilitado ? "" : "disable-action";
    }

}


var lineasPage = new LineasPage();

// Called after every non-initial page load
document.addEventListener('turbolinks:render', (e: any) => {
    if (document.URL.indexOf('/Lineas/Index') != -1) {
        if (lineasPage) {
            lineasPage.Init();
        }
        else {
            lineasPage = new LineasPage();
        }
        //console.log(e.data.url);
    }
    else
        lineasPage?.Destroy();
});
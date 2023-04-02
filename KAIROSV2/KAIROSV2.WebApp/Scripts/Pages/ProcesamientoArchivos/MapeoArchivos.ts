import { HttpFetchService, IMessageResponse, MapeoArchivos } from '../../Shared';
import { ConfirmModalMessage } from '../../Shared/Components/ConfirmModalMessage';
import { GestionMapeoArchivo } from './GestionMapeoArchivo';
import { JQValidations } from '../../Shared/Utils/JQValidations';
import { IPage } from '../../Shared/Components/IPage';

export class MapeoArchivosPage implements IPage {

    private modalInstance: M.Modal;
    private gestionMapeoArchivo: GestionMapeoArchivo;
    private tooltipInstances: M.Tooltip[];

    BaseURL: "/ProcesamientoArchivos/Index";

    constructor() {
        this.Init();
    }

    public Destroy() {
        //this.Table.destroy(false);
    }

    public Init() {
        this.InicializarButtons();
        this.InicializarControls();
    }

    private InicializarControls() {
        this.gestionMapeoArchivo = new GestionMapeoArchivo(document.getElementById("img-modal"));
        this.gestionMapeoArchivo.onProcesamientoArchivosGuardado = () => this.ActualizarListaMapeos();
        JQValidations.MaxFileSizeValidation();
        JQValidations.AllowedExtensionsValidation();
    }

    private InicializarButtons() {
        let mappers = document.getElementById("files-mapped");
        let search = document.getElementById("search-page");
        var MapeoArchivosAdd = document.getElementById("mapeoArchivos-add");
        var elems = document.querySelectorAll('.dropdown-trigger');
        var instances = M.Dropdown.init(elems);
        this.tooltipInstances = M.Tooltip.init(document.querySelectorAll('.tooltipped'));

        search.on('click', '.filter-action', event => {
            this.BuscarMapeo(event.target as HTMLElement);
        });

        mappers.on('click', '.files-action', event => {
            if (event.target.matches(".files-edit"))
                this.EditarMaperArchivo(event.target as HTMLElement);

            else if (event.target.matches(".files-delete"))
                this.BorrarMaperArchivos(event.target as HTMLElement);

        });
        MapeoArchivosAdd.addEventListener('click', event => this.CrearMaperArchivo());
    }

    private async BorrarMaperArchivos(element: HTMLElement) {
        let idMapeo = element.dataset.mapperid;
        const confirm = new ConfirmModalMessage("Eliminar Mapeo", "¿Desea eliminar el Mapeo " + idMapeo + "?", "Aceptar", "Cancelar");
        const shouldDelete = await confirm.Confirm();
        if (shouldDelete) {
            const httpService = new HttpFetchService();
            httpService.Post<IMessageResponse>('/ProcesamientoArchivos/BorrarProcesamientoArchivos', idMapeo)
                .then((data) => {
                    if (data.Result) {
                        this.BorrarMapeo(idMapeo);
                    }
                    M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

                }).catch((error) => {
                    M.toast({ html: error, classes: "error" });
                });
        }
    }

    private EditarMaperArchivo(element: HTMLElement): void {
        this.gestionMapeoArchivo.DatosProcesamientoArchivos(element.dataset.mapperid, false);
    }

    private CrearMaperArchivo() {
        this.gestionMapeoArchivo.NuevoProcesamientoArchivos();
    }

    private BuscarMapeo(element: HTMLElement): void {
        let filtro = element.textContent;
        let buscar = document.querySelector<HTMLInputElement>("#map-busqueda");
        if (filtro == "Todos")
            buscar.value = "";
        else if (filtro != "Todos" && (buscar.value == null || buscar.value == "")) {
            M.toast({ html: "Debes escribir algún término", classes: "error" });
            return;
        }

        var payload = { filtro: filtro, buscar: buscar.value };
        let httpService = new HttpFetchService();
        httpService.Post<string>("/ProcesamientoArchivos/ObtenerListado", payload, false)
            .then((data) => {
                document.querySelector(".file-process").innerHTML = data;
                this.tooltipInstances?.forEach(e => e?.destroy());
                this.tooltipInstances = M.Tooltip.init(document.querySelectorAll('.tooltipped'));
            })
            .catch((err) => console.log(err));
    }

    //Funcion para borrar Mapeo
    private BorrarMapeo(idMapeo) {
        let card = document.getElementById(idMapeo);
        card.parentElement.removeChild(card);
    }

    //Actualiza lista de mapeos
    private ActualizarListaMapeos() {
        const httpService = new HttpFetchService();
        httpService.Post<string>("/ProcesamientoArchivos/ObtenerListado", null, false)
            .then((data) => {
                document.querySelector(".file-process").innerHTML = data;
                this.tooltipInstances?.forEach(e => e?.destroy());
                this.tooltipInstances = M.Tooltip.init(document.querySelectorAll('.tooltipped'));
            })
            .catch((err) => console.log(err));
    }

}

var procesamientoArchivosPage = new MapeoArchivosPage();

// Called after every non-initial page load
document.addEventListener('turbolinks:render', (e: any) => {
    if (document.URL.indexOf('/ProcesamientoArchivos/Index') != -1) {
        if (procesamientoArchivosPage) {
            procesamientoArchivosPage.Init();
        }
        else {
            procesamientoArchivosPage = new MapeoArchivosPage();
        }
        //console.log(e.data.url);
    }
    else
        procesamientoArchivosPage?.Destroy();
});
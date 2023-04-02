import { HttpFetchService, IMessageResponse, Trailer } from '../../Shared';
import { ConfirmModalMessage } from '../../Shared/Components/ConfirmModalMessage';
import { ConfigureDataTable } from '../../Shared/Components/ConfigureDataTable';
import { TrailersGestionPage } from './GestionTrailers';
import { JQValidations } from '../../Shared/Utils/JQValidations';
import { SelectorMenu } from '../../Shared/Utils/SelectorMenu';
import { IPage } from '../../Shared/Components/IPage';
import { Page } from '../../Core/Page';

export class TrailersPage extends Page {

    private Table: DataTables.Api;
    private modalInstance: M.Modal;
    private gestionTrailers: TrailersGestionPage;

    constructor() {
        super();
        this.BaseUrl = "/Trailers/Index";
        this.Init();
    }

    public Destroy() {
        super.Destroy();
        this.Table.destroy(false);
        //this.Table.rows().invalidate().draw();
    }

    public Init() {
        this.InicializarButtons();
        this.InicializarControls();
        this.GetPermissions("/Trailers/ObtenerPermisos");
    }

    private InicializarControls() {
        SelectorMenu.SeleccionEnElMenu();
        this.Table = new ConfigureDataTable().Configure("#data-table-trailer", [{ "width": "50%", "targets": 0 }]);
        this.gestionTrailers = new TrailersGestionPage(document.getElementById("trailer-modal"));
        this.gestionTrailers.onTrailerCreado = (trailer) => this.AgregarFilaDatatable(trailer);
        JQValidations.MaxFileSizeValidation();
        JQValidations.AllowedExtensionsValidation();
    }

    private InicializarButtons() {
        var table = document.getElementById("data-table-trailer");
        var trailerAdd = document.getElementById("trailer-add");

        table.on('click', '.trailer-action', event => {

            if (event.target.matches(".trailer-delete"))
                this.BorrarTrailer(event.target as HTMLElement);
        });
        trailerAdd.addEventListener('click', event => this.CrearTrailer());
    }

    private async BorrarTrailer(element: HTMLElement) {
        let trailerId = element.dataset.trailerid;
        const confirm = new ConfirmModalMessage("Eliminar Trailer", "¿Desea eliminar el Trailer " + trailerId + "?", "Aceptar", "Cancelar");
        const shouldDelete = await confirm.Confirm();
        if (shouldDelete) {
            const httpService = new HttpFetchService();
            httpService.Post<IMessageResponse>('/Trailers/BorrarTrailer', trailerId)
                .then((data) => {
                    if (data.Result)
                        this.BorrarFilaDatatable(element);
                    M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

                }).catch((error) => {
                    M.toast({ html: error, classes: "error" });
                });
        }
    }

    private CrearTrailer() {
        this.gestionTrailers.NuevoTrailer();
    }


    //Funcion para borrar Trailer del datatable
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

    // agregar un nuevo Trailer
    private AgregarFilaDatatable(trailer: Trailer) {
        this.Table.row.add([
            trailer.Placa,
            '<a href="#" class="' + this.DisableAction(this.Permisions.Borrar) + '" data-turbolinks="false"><i class="material-icons trailer-action trailer-delete" data-trailerid="' + trailer.Placa + '">delete</i></a> '
        ]).draw(false);

    }

    //Obtiene clase disable dependiendo del permiso
    private DisableAction(habilitado: boolean): string {
        return habilitado ? "" : "disable-action";
    }
}

var trailersPage = new TrailersPage();



// Called after every non-initial page load
document.addEventListener('turbolinks:render', (e: any) => {
    if (document.URL.indexOf('/Trailers/Index') != -1) {
        if (trailersPage) {
            trailersPage.Init();
        }
        else {
            trailersPage = new TrailersPage();
        }
        //console.log(e.data.url);
    }
    else
        trailersPage?.Destroy();
});
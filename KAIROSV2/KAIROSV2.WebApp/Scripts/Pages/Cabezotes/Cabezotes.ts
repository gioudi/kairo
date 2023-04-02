import { HttpFetchService, IMessageResponse, Cabezote } from '../../Shared';
import { ConfirmModalMessage } from '../../Shared/Components/ConfirmModalMessage';
import { ConfigureDataTable } from '../../Shared/Components/ConfigureDataTable';
import { CabezotesGestionPage } from './GestionCabezotes';
import { JQValidations } from '../../Shared/Utils/JQValidations';
import { SelectorMenu } from '../../Shared/Utils/SelectorMenu';
import { Page } from '../../Core/Page';

export class CabezotesPage extends Page {

    private Table: DataTables.Api;
    private modalInstance: M.Modal;
    private gestionCabezotes: CabezotesGestionPage;

    constructor() {
        super();
        this.BaseUrl = "/Cabezotes/Index";
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
        super.GetPermissions("/Cabezotes/ObtenerPermisos");
    }

    private InicializarControls() {
        SelectorMenu.SeleccionEnElMenu();
        this.Table = new ConfigureDataTable().Configure("#data-table-cabezote", [{ "width": "50%", "targets": 0 }]);
        this.gestionCabezotes = new CabezotesGestionPage(document.getElementById("cabezote-modal"));
        this.gestionCabezotes.onCabezoteCreado = (cabezote) => this.AgregarFilaDatatable(cabezote);
        JQValidations.MaxFileSizeValidation();
        JQValidations.AllowedExtensionsValidation();
    }

    private InicializarButtons() {
        var table = document.getElementById("data-table-cabezote");
        var cabezoteAdd = document.getElementById("cabezote-add");

        table.on('click', '.cabezote-action', event => {

            if (event.target.matches(".cabezote-delete"))
                this.BorrarCabezote(event.target as HTMLElement);
        });
        cabezoteAdd.addEventListener('click', event => this.CrearCabezote());
    }

    private async BorrarCabezote(element: HTMLElement) {
        let cabezoteId = element.dataset.cabezoteid;
        const confirm = new ConfirmModalMessage("Eliminar Cabezote", "¿Desea eliminar el Cabezote " + cabezoteId + "?", "Aceptar", "Cancelar");
        const shouldDelete = await confirm.Confirm();
        if (shouldDelete) {
            const httpService = new HttpFetchService();
            httpService.Post<IMessageResponse>('/Cabezotes/BorrarCabezote', cabezoteId)
                .then((data) => {
                    if (data.Result)
                        this.BorrarFilaDatatable(element);
                    M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

                }).catch((error) => {
                    M.toast({ html: error, classes: "error" });
                });
        }
    }

    private CrearCabezote() {
        this.gestionCabezotes.NuevoCabezote();
    }


    //Funcion para borrar Cabezote del datatable
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

    // agregar un nuevo Cabezote
    private AgregarFilaDatatable(cabezote: Cabezote) {
        this.Table.row.add([
            cabezote.Placa,
            '<a href="#" class="' + this.DisableAction(this.Permisions.Borrar) + '" data-turbolinks="false"><i class="material-icons cabezote-action cabezote-delete" data-cabezoteid="' + cabezote.Placa + '">delete</i></a> '
        ]).draw(false);

    }

    //Obtiene clase disable dependiendo del permiso
    private DisableAction(habilitado: boolean): string {
        return habilitado ? "" : "disable-action";
    }
}

var cabezotesPage = new CabezotesPage();



// Called after every non-initial page load
document.addEventListener('turbolinks:render', (e: any) => {
    if (document.URL.indexOf('/Cabezotes/Index') != -1) {
        if (cabezotesPage) {
            cabezotesPage.Init();
        }
        else {
            cabezotesPage = new CabezotesPage();
        }
        //console.log(e.data.url);
    }
    else
        cabezotesPage?.Destroy();
});
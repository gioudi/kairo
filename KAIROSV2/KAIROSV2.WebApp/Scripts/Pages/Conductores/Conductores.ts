import { HttpFetchService, IMessageResponse, Conductor } from '../../Shared';
import { ConfirmModalMessage } from '../../Shared/Components/ConfirmModalMessage';
import { ConfigureDataTable } from '../../Shared/Components/ConfigureDataTable';
import { ConductoresGestionPage } from './GestionConductores';
import { JQValidations } from '../../Shared/Utils/JQValidations';
import { SelectorMenu } from '../../Shared/Utils/SelectorMenu';
import { Page } from '../../Core/Page';
import { App } from '../../app';

export class ConductoresPage extends Page {

    private Table: DataTables.Api;
    private modalInstance: M.Modal;
    private gestionConductores: ConductoresGestionPage;

    constructor() {
        super();
        this.BaseUrl = "/Conductores/Index";
        this.Init();
    }
 
    public Init() {
        this.InicializarButtons();
        this.InicializarControls();
        super.GetPermissions("/Conductores/ObtenerPermisos");
    }

    public Destroy() {
        this.Table.destroy(false);
    }

    private InicializarControls() {
        SelectorMenu.SeleccionEnElMenu();
        this.Table = new ConfigureDataTable().Configure("#data-table-conductor");
        this.gestionConductores = new ConductoresGestionPage(document.getElementById("conductor-modal"));
        this.gestionConductores.onConductorCreado = (conductor) => this.AgregarFilaDatatable(conductor);
        this.gestionConductores.onConductorActualizado = (conductor) => this.ActualizarFilaDatatable(conductor);
        JQValidations.MaxFileSizeValidation();
        JQValidations.AllowedExtensionsValidation();
    }

    private InicializarButtons() {
        var table = document.getElementById("data-table-conductor");
        var userAdd = document.getElementById("conductor-add");

        table.on('click', '.conductor-action', event => {
            if (event.target.matches(".conductor-edit"))
                this.EditarConductor(event.target as HTMLElement);

            else if (event.target.matches(".conductor-delete"))
                this.BorrarConductor(event.target as HTMLElement);

            else if (event.target.matches(".conductor-detail"))
                this.DetallesConductor(event.target as HTMLElement);
        });
        userAdd.addEventListener('click', event => this.CrearConductor());
    }

    private async BorrarConductor(element: HTMLElement) {
        let conduId = element.dataset.conducid;
        let name = element.dataset.name;
        const confirm = new ConfirmModalMessage("Eliminar Conductor", "Desea eliminar el Conductor " + name + " con cédula "+ conduId, "Aceptar", "Cancelar");
        const shouldDelete = await confirm.Confirm();
        if (shouldDelete) {
            const httpService = new HttpFetchService();
            httpService.Post<IMessageResponse>('/Conductores/BorrarConductor', parseInt(conduId))
                .then((data) => {
                    if (data.Result)
                        this.BorrarFilaDatatable(element);
                    M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

                }).catch((error) => {
                    M.toast({ html: error, classes: "error" });
                });
        }
    }


    private CrearConductor() {
        this.gestionConductores.NuevoConductor();
    }

    private EditarConductor(element: HTMLElement): void {
        console.log(element);
        this.gestionConductores.DatosConductor(element.dataset.conducid, true);
    }

    private DetallesConductor(element: HTMLElement): void {
        this.gestionConductores.DatosConductor(element.dataset.conducid, true);
    }


    //Funcion para borrar conductor del datatable
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

    // agregar un nuevo conductor
    private AgregarFilaDatatable(conductor: Conductor) {
        let newRow = this.Table.row.add([
            conductor.Nombre,
            conductor.Cedula,
            '<a href="#" data-turbolinks="false"><i class="material-icons conductor-action conductor-edit" data-conducid="' + conductor.Cedula + '">edit</i></a> ' +
            '<a href="#" data-turbolinks="false"><i class="material-icons conductor-action conductor-delete" data-conducid="' + conductor.Cedula + '">delete</i></a> '
        ]);
        (this.Table.row(newRow).node() as HTMLElement).id = conductor.Cedula;
        newRow.draw(false);
    }

    // Actualizar un conductor
    private ActualizarFilaDatatable(conductor: Conductor) {
        let row = $("tr#" + conductor.Cedula);
        var cond = this.Table.row(row).data();
        cond[0] = conductor.Nombre;
        cond[1] = conductor.Cedula;
        this.Table.row(row).data(cond);
    }

    //Obtiene clase disable dependiendo del permiso
    private DisableAction(habilitado: boolean): string {
        return habilitado ? "" : "disable-action";
    }

}



var conductoresPage = new ConductoresPage();

// Called after every non-initial page load
document.addEventListener('turbolinks:render', (e: any) => {
    if (document.URL.indexOf('/Conductores/Index') != -1) {
        if (conductoresPage) {
            conductoresPage.Init();
        }
        else {
            conductoresPage = new ConductoresPage();
        }
    } else {
        conductoresPage.Destroy();
    }
});
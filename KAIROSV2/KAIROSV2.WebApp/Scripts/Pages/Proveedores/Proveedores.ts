import { HttpFetchService, IMessageResponse, Proveedor } from '../../Shared';
import { ConfirmModalMessage } from '../../Shared/Components/ConfirmModalMessage';
import { ConfigureDataTable } from '../../Shared/Components/ConfigureDataTable';

import { ProveedoresGestionPage } from './GestionProveedores';
import { JQValidations } from '../../Shared/Utils/JQValidations';
import { SelectorMenu } from '../../Shared/Utils/SelectorMenu';
import { App } from '../../app';
import { Page } from '../../Core/Page';

export class ProveedoresPage extends Page {

    private Table: DataTables.Api;
    private modalInstance: M.Modal;
    private gestionProveedores: ProveedoresGestionPage;

    constructor() {
        super();
        this.BaseUrl = "/Proveedores/Index";
        this.Init();
    }
 
    public Init() {
        this.InicializarButtons();
        this.InicializarControls();
        this.GetPermissions("/Proveedores/ObtenerPermisos");
    }

    public Destroy() {
        super.Destroy();
        this.Table.destroy(false);
    }

    private InicializarControls() {
        SelectorMenu.SeleccionEnElMenu();
        this.Table = new ConfigureDataTable().Configure("#data-table-proveedor");
        this.gestionProveedores = new ProveedoresGestionPage(document.getElementById("proveedor-modal"));
        this.gestionProveedores.onProveedorCreado = (proveedor) => this.AgregarFilaDatatable(proveedor);
        this.gestionProveedores.onProveedorActualizado = (proveedor) => this.ActualizarFilaDatatable(proveedor);
        JQValidations.MaxFileSizeValidation();
        JQValidations.AllowedExtensionsValidation();
    }

    private InicializarButtons() {
        var table = document.getElementById("data-table-proveedor");
        var userAdd = document.getElementById("proveedor-add");

        table.on('click', '.proveedor-action', event => {
            if (event.target.matches(".proveedor-edit"))
                this.EditarProveedor(event.target as HTMLElement);

            else if (event.target.matches(".proveedor-delete"))
                this.BorrarProveedor(event.target as HTMLElement);

            else if (event.target.matches(".proveedor-detail"))
                this.DetallesProveedor(event.target as HTMLElement);
        });
        userAdd.addEventListener('click', event => this.CrearProveedor());
    }

    private async BorrarProveedor(element: HTMLElement) {
        let conduId = element.dataset.proveedorid;
        const confirm = new ConfirmModalMessage("Eliminar Proveedor", "Desea eliminar el Proveedor " + conduId, "Aceptar", "Cancelar");
        const shouldDelete = await confirm.Confirm();
        if (shouldDelete) {
            const httpService = new HttpFetchService();
            httpService.Post<IMessageResponse>('/Proveedores/BorrarProveedor', conduId)
                .then((data) => {
                    if (data.Result)
                        this.BorrarFilaDatatable(element);
                    M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

                }).catch((error) => {
                    M.toast({ html: error, classes: "error" });
                });
        }
    }

    private CrearProveedor() {
        this.gestionProveedores.NuevoProveedor();
    }

    private EditarProveedor(element: HTMLElement): void {
        console.log(element);
        this.gestionProveedores.DatosProveedor(element.dataset.proveedorid, false);
    }

    private DetallesProveedor(element: HTMLElement): void {
        this.gestionProveedores.DatosProveedor(element.dataset.proveedorid, true);
    }


    //Funcion para borrar Proveedor del datatable
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

    // agregar un nuevo Proveedor
    private AgregarFilaDatatable(proveedor: Proveedor) {
        let newRow = this.Table.row.add([
            proveedor.IdProveedor,
            proveedor.NombreProveedor,
            proveedor.SicomProveedor,
            '<a href="#" class="' + this.DisableAction(this.Permisions.Editar) + '" data-turbolinks="false"><i class="material-icons proveedor-action proveedor-edit" data-proveedorid="' + proveedor.IdProveedor + '">edit</i></a> ' +
            '<a href="#" class="' + this.DisableAction(this.Permisions.Borrar) + '" data-turbolinks="false"><i class="material-icons proveedor-action proveedor-delete" data-proveedorid="' + proveedor.IdProveedor + '">delete</i></a> ' +
            '<a class="cursor-point ' + this.DisableAction(this.Permisions.Detalles) + '" data-turbolinks="false"><i class="material-icons proveedor-action proveedor-detail" data-proveedorid="' + proveedor.IdProveedor + '">remove_red_eye</i></a>'            
        ]);
        (this.Table.row(newRow).node() as HTMLElement).id = proveedor.IdProveedor;
        newRow.draw(false);
    }

    // Actualizar un proveedor
    private ActualizarFilaDatatable(proveedor: Proveedor) {
        let row = $("tr#" + proveedor.IdProveedor);
        var cond = this.Table.row(row).data();
        cond[0] = proveedor.IdProveedor;
        cond[1] = proveedor.NombreProveedor;
        cond[2] = proveedor.SicomProveedor;
        this.Table.row(row).data(cond);
    }

    //Obtiene clase disable dependiendo del permiso
    private DisableAction(habilitado: boolean): string {
        return habilitado ? "" : "disable-action";
    }
}



var proveedoresPage = new ProveedoresPage();

// Called after every non-initial page load
document.addEventListener('turbolinks:render', (e: any) => {
    if (document.URL.indexOf('/Proveedores/Index') != -1) {
        if (proveedoresPage) {
            proveedoresPage.Init();
        }
        else {
            proveedoresPage = new ProveedoresPage();
        }
    } else {
        proveedoresPage.Destroy();
    }
});
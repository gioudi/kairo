import { HttpFetchService, IMessageResponse, Rol } from '../../Shared';
import { ConfirmModalMessage } from '../../Shared/Components/ConfirmModalMessage';
import { ConfigureDataTable } from '../../Shared/Components/ConfigureDataTable';
import { RolesGestionPage } from './GestionRoles';
import { JQValidations } from '../../Shared/Utils/JQValidations';
import { SelectorMenu } from '../../Shared/Utils/SelectorMenu';
import { Page } from '../../Core/Page';

export class RolesPage extends Page {

    private Table: DataTables.Api;
    private modalInstance: M.Modal;
    private gestionRoles: RolesGestionPage;

    constructor() {
        super();
        this.BaseUrl = "/Roles/Index";
        this.Init();
    }

    public Destroy() {
        super.Destroy();
        this.Table.destroy(false);
    }

    public Init() {
        this.InicializarButtons();
        this.InicializarControls();
        this.GetPermissions("/Roles/ObtenerPermisos");
    }

    private InicializarControls() {
        SelectorMenu.SeleccionEnElMenu();
        this.Table = new ConfigureDataTable().Configure("#data-table-rol", [{ "width": "5%", "targets": 0 }]);
        this.gestionRoles = new RolesGestionPage(document.getElementById("rol-modal"));
        this.gestionRoles.onRolCreado = (rol) => this.AgregarFilaDatatable(rol);
        this.gestionRoles.onRolActualizado = (rol) => this.ActualizarFilaDatatable(rol);
        JQValidations.NotEqualValidation();
    }

    private InicializarButtons() {
        var table = document.getElementById("data-table-rol");
        var rolAdd = document.getElementById("rol-add");

        table.on('click', '.rol-action', event => {
            if (event.target.matches(".rol-edit"))
                this.EditarRol(event.target as HTMLElement);

            else if (event.target.matches(".rol-delete"))
                this.BorrarRol(event.target as HTMLElement);

            else if (event.target.matches(".rol-detail"))
                this.DetallesRol(event.target as HTMLElement);
        });
        rolAdd.addEventListener('click', event => this.CrearRol());
    }

    private async BorrarRol(element: HTMLElement) {
        let rolId = element.dataset.rolid;
        let rolName = element.dataset.rolname;
        const confirm = new ConfirmModalMessage("Eliminar Rol", "¿Desea eliminar el Rol " + rolName + "?", "Aceptar", "Cancelar");
        const shouldDelete = await confirm.Confirm();
        if (shouldDelete) {
            const httpService = new HttpFetchService();
            httpService.Post<IMessageResponse>('/Roles/BorrarRol', rolId)
                .then((data) => {
                    if (data.Result)
                        this.BorrarFilaDatatable(element);
                    M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

                }).catch((error) => {
                    M.toast({ html: error, classes: "error" });
                });
        }
    }

    private CrearRol() {
        this.gestionRoles.NuevoRol();
    }

    private EditarRol(element: HTMLElement): void {
        console.log(element);
        this.gestionRoles.DatosRol(element.dataset.rolid, false);
    }

    private DetallesRol(element: HTMLElement): void {
        this.gestionRoles.DatosRol(element.dataset.rolid, true);
    }

    //Funcion para borrar rol del datatable
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

    // agregar un nuevo rol
    private AgregarFilaDatatable(rol: Rol) {
        let newRol = this.Table.row.add([
            rol.IdRol,
            rol.Nombre,
            rol.Descripcion,
            '<a class="cursor-point ' + this.DisableAction(this.Permisions.Editar) + '" data-turbolinks="false">' +
            '<i class= "material-icons rol-action rol-edit" data-rolid="' + rol.IdRol + '" data-rolname="' + rol.Nombre + '">edit</i></a>' +
            '<a class="cursor-point ' + this.DisableAction(this.Permisions.Borrar) + '" data-turbolinks="false">' +
            '<i class="material-icons rol-action rol-delete" data-rolid="' + rol.IdRol + '" data-rolname="' + rol.Nombre + '">delete</i></a>' +
            '<a class="cursor-point ' + this.DisableAction(this.Permisions.Detalles) + '" data-turbolinks="false">' +
            '<i class="material-icons rol-action rol-detail" data-rolid="' + rol.IdRol + '" data-rolname="' + rol.Nombre + '">remove_red_eye</i></a>'
        ]);
        (this.Table.row(newRol).node() as HTMLElement).id = rol.IdRol;
        newRol.draw(false);
    }

    // Actualizar un rol
    private ActualizarFilaDatatable(rol: Rol) {
        let row = $("tr#" + rol.IdRol);
        var role = this.Table.row(row).data();
        role[1] = rol.Nombre;
        role[2] = rol.Descripcion;
        this.Table.row(row).data(role);
    }

    //Obtiene clase disable dependiendo del permiso
    private DisableAction(habilitado: boolean): string {
        return habilitado ? "" : "disable-action";
    }

}

var rolesPage = new RolesPage();


// Called after every non-initial page load
document.addEventListener('turbolinks:render', (e: any) => {
    if (document.URL.indexOf('/Roles/Index') != -1) {
        if (rolesPage) {
            rolesPage.Init();
        }
        else {
            rolesPage = new RolesPage();
        }
        //console.log(e.data.url);
    }
    else
        rolesPage?.Destroy();
});
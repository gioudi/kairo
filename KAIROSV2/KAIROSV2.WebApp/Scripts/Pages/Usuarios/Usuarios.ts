import { HttpFetchService, IMessageResponse, Usuario } from '../../Shared';
import { ConfirmModalMessage } from '../../Shared/Components/ConfirmModalMessage';
import { ConfigureDataTable } from '../../Shared/Components/ConfigureDataTable';
import { UsuariosGestionPage } from './GestionUsuarios';
import { JQValidations } from '../../Shared/Utils/JQValidations';
import { SelectorMenu } from '../../Shared/Utils/SelectorMenu';
import { Page } from '../../Core/Page';

export class UsuariosPage extends Page {

    private Table: DataTables.Api;
    private modalInstance: M.Modal;
    private gestionUsuarios: UsuariosGestionPage;

    constructor() {
        super();
        this.BaseUrl = "/Usuarios/Index";
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
        super.GetPermissions("/Usuarios/ObtenerPermisos");
    }

    private InicializarControls() {
        SelectorMenu.SeleccionEnElMenu();
        this.Table = new ConfigureDataTable().Configure("#data-table-usuario", [{ "width": "5%", "targets": 0 }]);
        this.gestionUsuarios = new UsuariosGestionPage(document.getElementById("user-modal"));
        this.gestionUsuarios.onUsuarioCreado = (usuario) => this.AgregarFilaDatatable(usuario);
        this.gestionUsuarios.onUsuarioActualizado = (usuario) => this.ActualizarFilaDatatable(usuario);
        JQValidations.MaxFileSizeValidation();
        JQValidations.AllowedExtensionsValidation();
    }

    private InicializarButtons() {
        var table = document.getElementById("data-table-usuario");
        var userAdd = document.getElementById("user-add");

        table.on('click', '.user-action', event => {
            if (event.target.matches(".user-edit"))
                this.EditarUsuario(event.target as HTMLElement);

            else if (event.target.matches(".user-delete"))
                this.BorrarUsuario(event.target as HTMLElement);

            else if (event.target.matches(".user-detail"))
                this.DetallesUsuario(event.target as HTMLElement);
        });
        userAdd.addEventListener('click', event => this.CrearUsuario());
    }

    private async BorrarUsuario(element: HTMLElement) {
        let userId = element.dataset.userid;
        let userName = element.dataset.username;
        const confirm = new ConfirmModalMessage("Eliminar Usuario", "¿Desea eliminar el Usuario " + userName + "?", "Aceptar", "Cancelar");
        const shouldDelete = await confirm.Confirm();
        if (shouldDelete) {
            const httpService = new HttpFetchService();
            httpService.Post<IMessageResponse>('/Usuarios/BorrarUsuario', userId)
                .then((data) => {
                    if (data.Result)
                        this.BorrarFilaDatatable(element);
                    M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

                }).catch((error) => {
                    M.toast({ html: error, classes: "error" });
                });
        }
    }

    private CrearUsuario() {
        this.gestionUsuarios.NuevoUsuario();
    }

    private EditarUsuario(element: HTMLElement): void {
        console.log(element);
        this.gestionUsuarios.DatosUsuario(element.dataset.userid, false);
    }

    private DetallesUsuario(element: HTMLElement): void {
        this.gestionUsuarios.DatosUsuario(element.dataset.userid, true);
    }

    //Funcion para borrar usuario del datatable
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

    // agregar un nuevo usuario
    private AgregarFilaDatatable(usuario: Usuario) {
        let image = usuario.Imagen.name;
        console.log(image);
        if (usuario.Imagen.name != "") {
            this.Table.row.add([
                '<span class="avatar-contact avatar-online circle"><img src="' + URL.createObjectURL(usuario.Imagen) + '" alt="avatar"></span>',
                usuario.IdUsuario,
                usuario.Nombre,
                usuario.Rol,
                usuario.Email,
                usuario.Telefono,
                '<a href="#" class="' + this.DisableAction(this.Permisions.Editar) + '" data-turbolinks="false"><i class="material-icons user-action user-edit" data-userid="' + usuario.IdUsuario + '" data-username="' + usuario.Nombre + '">edit</i></a> ' +
                '<a href="#" class="' + this.DisableAction(this.Permisions.Borrar) + '" data-turbolinks="false"><i class="material-icons user-action user-delete" data-userid="' + usuario.IdUsuario + '" data-username="' + usuario.Nombre + '">delete</i></a> ' +
                '<a href="#" class="' + this.DisableAction(this.Permisions.Detalles) + '" data-turbolinks="false"><i class="material-icons user-action user-detail" data-userid="' + usuario.IdUsuario + '" data-username="' + usuario.Nombre + '">remove_red_eye</i></a>'
            ]).draw(false);
        } else {
            this.Table.row.add([
                '<span class="avatar-contact avatar-online circle"><img src="/images/avatar/account_circle-black-48dp.svg" alt="avatar"></span>',
                usuario.IdUsuario,
                usuario.Nombre,
                usuario.Rol,
                usuario.Email,
                usuario.Telefono,
                '<a href="#" class="' + this.DisableAction(this.Permisions.Editar) + '" data-turbolinks="false"><i class="material-icons user-action user-edit" data-userid="' + usuario.IdUsuario + '" data-username="' + usuario.Nombre + '">edit</i></a> ' +
                '<a href="#" class="' + this.DisableAction(this.Permisions.Borrar) + '" data-turbolinks="false"><i class="material-icons user-action user-delete" data-userid="' + usuario.IdUsuario + '" data-username="' + usuario.Nombre + '">delete</i></a> ' +
                '<a href="#" class="' + this.DisableAction(this.Permisions.Detalles) + '" data-turbolinks="false"><i class="material-icons user-action user-detail" data-userid="' + usuario.IdUsuario + '" data-username="' + usuario.Nombre + '">remove_red_eye</i></a>'
            ]).draw(false);
        }

    }

    // Actualizar un usuario
    private ActualizarFilaDatatable(usuario: Usuario) {
        let row = $("tr#" + usuario.IdUsuario);
        var user = this.Table.row(row).data();
        if (usuario.Imagen.name != "") {
            user[0] = '<span class="avatar-contact avatar-online"><img src="' + URL.createObjectURL(usuario.Imagen) + '" alt="avatar"></span>';
        } else {
            user[0] = '<span class="avatar-contact avatar-online"><img src="/images/avatar/account_circle-black-48dp.svg" alt="avatar"></span>';
        }
        user[3] = usuario.Rol;
        this.Table.row(row).data(user);
    }

    //Obtiene clase disable dependiendo del permiso
    private DisableAction(habilitado: boolean): string {
        return habilitado ? "" : "disable-action";
    }

}

var usuariosPage = new UsuariosPage();


// Called after every non-initial page load
document.addEventListener('turbolinks:render', (e: any) => {
    if (document.URL.indexOf('/Usuarios/Index') != -1) {
        if (usuariosPage) {
            usuariosPage.Init();
        }
        else {
            usuariosPage = new UsuariosPage();
        }
        //console.log(e.data.url);
    }
    else
        usuariosPage?.Destroy();
});
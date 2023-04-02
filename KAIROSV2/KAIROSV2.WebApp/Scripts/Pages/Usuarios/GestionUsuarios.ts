import { HttpFetchService, IMessageResponse } from '../../Shared';
import { Usuario } from '../../Shared/Models/Usuario';

import 'jquery-qubit';
import 'jquery-bonsai';
import 'jquery-bonsai/jquery.bonsai.css';
import 'select2';
import 'select2/dist/css/select2.css';

export class UsuariosGestionPage {

    //PROPIEDADES
    public onUsuarioCreado?: (usuario: Usuario) => void;
    public onUsuarioActualizado?: (usuario: Usuario) => void;

    //CAMPOS
    private _baseUrl = "/Usuarios";
    private _httpService: HttpFetchService;
    private _modalInstance: M.Modal;
    private _modalBase: HTMLElement;
    private _formUser: HTMLFormElement;
    private _selects: M.FormSelect[];
    private _collaps: M.Collapsible[];
    private _formData: FormData;
    private _usuarioIdActualizar: string;

    //CONSTRUCTOR
    constructor(modalBase: HTMLElement) {
        this._httpService = new HttpFetchService();
        this._modalBase = modalBase;
        this.InicializarModal();
    }

    //METODOS

    //Creacion de nuevo usuario
    public NuevoUsuario() {
        this._httpService.Post<string>(this._baseUrl + "/NuevoUsuario", null, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormUsuario(true);
            });

        this._modalInstance.open();
    }

    //Guardar usuario
    private GuardarUsuario(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/CrearUsuario", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    this.LimpiarFormulario();
                    if (this.onUsuarioCreado)
                        this.onUsuarioCreado(this.ExtraerUsuario());
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

            })
            .catch((err) => console.log(err));
    }

    //Edicion y detalles de usuario
    public DatosUsuario(idUsuario: string, lectura: boolean) {
        var payload = { idEntidad: idUsuario, lectura: lectura };
        this._usuarioIdActualizar = idUsuario;
        this._httpService.Post<string>(this._baseUrl + "/DatosUsuario", payload, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormUsuario(false);
            });

        this._modalInstance.open();
    }

    //Actualizar usuario
    private ActualizarUsuario(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._formData.set("IdUsuario", this._usuarioIdActualizar);
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/ActualizarUsuario", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    if (this.onUsuarioActualizado)
                        this.onUsuarioActualizado(this.ExtraerUsuario());
                    this.LimpiarFormulario();
                    this.CerrarModal();
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });
            })
            .catch((err) => console.log(err));
    }

    //Inicializar control modal
    private InicializarModal() {

        this._modalInstance = M.Modal.init(this._modalBase, {
            dismissible: false,
            opacity: .5,
            inDuration: 300,
            outDuration: 200,
            startingTop: '6%',
            endingTop: '8%'
        });

    }

    //Inicializar controles de formulario
    private InicilizarFormUsuario(modoCreacion: boolean): void {
        $(".select2").select2({ dropdownAutoWidth: true, width: '100%', language: "es" });
        $('.select2').select2().change(function () {
            $(this).valid();
        });
        this._selects = M.FormSelect.init(document.querySelectorAll("select"));
        this._collaps = M.Collapsible.init(document.querySelectorAll(".collapsible.user"));
        M.updateTextFields();
        let imogly = ImoglayInput((document.getElementById("user-img") as HTMLInputElement), "camera_alt", "Subir foto", true);
        let bonsai = $("ul#user-terminals").bonsai({
            expandAll: true,
            checkboxes: true
        });

        this._formUser = document.querySelector("#user-form");
        this._formData = new FormData(this._formUser);
        $(this._formUser).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(this._formUser);
        this.ConfigurarBusquedaUsuario();
        document.getElementById("user-modal-cancel").addEventListener('click', (ev) => this.CerrarModal());

        this._formUser.onsubmit = (e) => {
            e.preventDefault();

            if ($(this._formUser).valid()) {
                this._formData = new FormData(this._formUser);
                //formData.forEach((value, key) => { console.log(key + ": " + value) });

                if (modoCreacion)
                    this.GuardarUsuario();
                else
                    this.ActualizarUsuario();
            }

            return false; // prevent reload
        };

        document.getElementById("user-collapsible").addEventListener('click', event => this.BajarScroll());
    }

    //Extraer usuario de formulario
    private ExtraerUsuario(): Usuario {
        let user = new Usuario();

        user.Imagen = (this._formData.get("Imagen") as File);
        user.Email = this._formData.get("Email")?.toString();
        user.Telefono = this._formData.get("Telefono")?.toString();
        user.IdUsuario = this._formData.get("IdUsuario")?.toString();
        user.Nombre = this._formData.get("Nombre")?.toString();
        user.Rol = this._formData.get("RolId")?.toString();

        return user;
    }

    //Reset de formulario
    private LimpiarFormulario() {
        this._formUser.reset();
        $('input').not("input[type= 'hidden']").val(null);
        $('select').val(null);
        $('#user-id').val(null).trigger('change');
        let img = document.getElementById("user-img") as HTMLInputElement;
        img.value = null;
        const e = new Event("change");
        img.dispatchEvent(e);
        M.updateTextFields();
        $("ul#user-terminals").bonsai('update');
    }

    //Cerrar control modal
    private CerrarModal() {
        this._selects.every((selects) => selects.destroy());
        this._collaps.every((collaps) => collaps.destroy());
        this._modalInstance.close();
        this._formUser.parentNode.removeChild(this._formUser);
        this._usuarioIdActualizar = "";
    }

    //Baja el scroll
    private BajarScroll() {
        $("#user-modalcontent").animate(
            { scrollTop: $('#user-modalcontent').prop("scrollHeight") }, 1000
        );
    }

    //Configuracion select2 para busqueda usuario AD
    private ConfigurarBusquedaUsuario() {
        var select2UsuarioId = $("#user-id").on("select2:select", (e) => {
            let data: any;
            data = e.params.data;

            (document.getElementById("user-name") as HTMLInputElement).value = data.text;
            (document.getElementById("user-phone") as HTMLInputElement).value = data.telefono;
            (document.getElementById("user-email") as HTMLInputElement).value = data.email;

            M.updateTextFields();

            $(this._formUser).valid();
        });

        select2UsuarioId.select2({
            language: "es",
            dropdownAutoWidth: true,
            width: '100%',
            ajax: {
                url: "/Usuarios/ObtenerUsuariosActiveDirectory",
                type: 'Post',
                dataType: 'json',
                delay: 250,
                data: function (params) {
                    return { busqueda: params.term };
                },
                processResults: function (data) {
                    data.results = data.map(function (obj) {
                        return {
                            "text": obj.Text,
                            "id": obj.Id,
                            "email": obj.Email,
                            "telefono": obj.Telefono
                        };
                    });
                    return { results: data.results };
                },
                cache: true
            },
            placeholder: 'Buscar usuario',
            escapeMarkup: function (markup) { return markup; }, /* let our custom formatter work */
            minimumInputLength: 3,
            templateResult: this.FormatearSelect2,
            templateSelection: (usuarioAD) => { return usuarioAD.id; }
        });
    }

    //Formato de resultados select2 usuario AD
    private FormatearSelect2(usuarioAD): string {
        let markup: string;

        if (!usuarioAD.id) {
            return '<div class="preloader-wrapper small active center-align center">' +
                '<div class="spinner-layer spinner-green-only" >' +
                '<div class="circle-clipper left" >' +
                '<div class="circle" > </div>' +
                '</div><div class="gap-patch">' +
                '<div class="circle" > </div>' +
                '</div><div class="circle-clipper right">' +
                '<div class="circle" > </div> < /div> < /div> </div>';
        }

        markup = '<div> <h6>' + usuarioAD.id + '</h6>' +
            '<div class="light">' + usuarioAD.text + '</div>' +
            '<div>' + usuarioAD.email + '</div>' +
            '<div>' + usuarioAD.telefono + '</div> </div>';

        return markup;
    }
}
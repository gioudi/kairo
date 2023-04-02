import { HttpFetchService, IMessageResponse } from '../../Shared';
import { Rol } from '../../Shared/Models/Rol';

import 'jquery-qubit';
import 'jquery-bonsai';
import 'jquery-bonsai/jquery.bonsai.css';
import 'select2';
import 'select2/dist/css/select2.css';

export class RolesGestionPage {

    //PROPIEDADES
    public onRolCreado?: (rol: Rol) => void;
    public onRolActualizado?: (rol: Rol) => void;

    //CAMPOS
    private _baseUrl = "/Roles";
    private _httpService: HttpFetchService;
    private _modalInstance: M.Modal;
    private _modalBase: HTMLElement;
    private _formRol: HTMLFormElement;
    private _collaps: M.Collapsible[];
    private _formData: FormData;
    private _rolIdActualizar: string;

    //CONSTRUCTOR
    constructor(modalBase: HTMLElement) {
        this._httpService = new HttpFetchService();
        this._modalBase = modalBase;
        this.InicializarModal();
    }

    //METODOS

    //Creacion de nuevo rol
    public NuevoRol() {
        this._httpService.Post<string>(this._baseUrl + "/NuevoRol", null, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormRol(true);
            });

        this._modalInstance.open();
    }

    //Guardar Rol
    private GuardarRol(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/CrearRol", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    this.LimpiarFormulario();
                    if (this.onRolCreado)
                        this.onRolCreado(this.ExtraerRol());
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

            })
            .catch((err) => console.log(err));
    }

    //Edicion y detalles de rol
    public DatosRol(idRol: string, lectura: boolean) {
        var payload = { idEntidad: idRol, lectura: lectura };
        this._rolIdActualizar = idRol;
        this._httpService.Post<string>(this._baseUrl + "/DatosRol", payload, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormRol(false);
            });

        this._modalInstance.open();
    }

    //Actualizar rol
    private ActualizarRol(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._formData.set("IdRol", this._rolIdActualizar);
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/ActualizarRol", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    if (this.onRolActualizado)
                        this.onRolActualizado(this.ExtraerRol());
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
    private InicilizarFormRol(modoCreacion: boolean): void {
        document.getElementById("rol-modal-cancel").addEventListener('click', (ev) => this.CerrarModal());
        this._formRol = document.querySelector("#rol-form");
        if (!this._formRol)
            return;

        this._collaps = M.Collapsible.init(document.querySelectorAll(".collapsible.rol"));
        M.updateTextFields();
        this.InicializarJerarquiaPermisos();
        this._formData = new FormData(this._formRol);
        $(this._formRol).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(this._formRol);


        this._formRol.onsubmit = (e) => {
            e.preventDefault();

            if ($(this._formRol).valid()) {
                this._formData = new FormData(this._formRol);

                //this._formData.forEach((value, key) => {
                //    console.log(key + " " + value)
                //});

                if (modoCreacion)
                    this.GuardarRol();
                else
                    this.ActualizarRol();
            }

            return false; // prevent reload
        };

        document.getElementById("rol-collapsible").addEventListener('click', event => this.BajarScroll());
    }

    private InicializarJerarquiaPermisos(): void {

        let permisosList = document.querySelector<HTMLUListElement>("ul#rol-permisos");
        permisosList.on('change', 'input', event => {
            let checkbox = (event.target as HTMLInputElement)
            if (checkbox.dataset.class == "5") {

                let parentList = checkbox.closest<HTMLElement>(".bonsai");
                let IMCheckbox = parentList.querySelector<HTMLInputElement>("input[data-class='-1']");

                
                if (checkbox.checked == true && IMCheckbox) {
                    console.log("True value");
                    IMCheckbox.checked = true;
                    IMCheckbox.parentElement.classList.add("disable-action");
                }
                if (checkbox.checked == false && IMCheckbox) {
                    console.log("False value");
                    if (!Array.from(parentList.querySelectorAll<HTMLInputElement>("input[data-class='5']")).some(e => e.checked == true)) {
                        console.log("Remove class");
                        IMCheckbox.parentElement.classList.remove("disable-action");
                    }
                }
            }
        });

        $("ul#rol-permisos").bonsai({
            expandAll: true,
            addExpandAll: false, // add a link to expand all items
            addSelectAll: false, // add a link to select all checkboxes
            checkboxes: true
        });

    }
    //Extraer rol de formulario
    private ExtraerRol(): Rol {
        let role = new Rol();

        role.IdRol = this._formData.get("IdRol")?.toString();
        role.Nombre = this._formData.get("Nombre")?.toString();
        role.Descripcion = this._formData.get("Descripcion")?.toString();

        return role;
    }

    //Baja el scroll
    private BajarScroll() {
        $("#rol-modalcontent").animate(
            { scrollTop: $('#rol-modalcontent').prop("scrollHeight") }, 1000
        );
    }

    //Reset de formulario
    private LimpiarFormulario() {
        this._formRol.reset();
        $('input').not("input[type= 'hidden']").val(null);
        $('#rol-id').val(null).trigger('change');
        M.updateTextFields();
        let bonsai = $("ul#rol-permisos").data('bonsai');
        bonsai.update();
        $("ul#rol-permisos").bonsai('update');
    }

    //Cerrar control modal
    private CerrarModal() {
        this._collaps?.every((collaps) => collaps.destroy());
        this._modalInstance.close();
        this._formRol?.parentNode.removeChild(this._formRol);
        this._rolIdActualizar = "";
    }
}
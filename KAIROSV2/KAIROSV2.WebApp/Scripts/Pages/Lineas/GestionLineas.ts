import { HttpFetchService, IMessageResponse, Log } from '../../Shared';
import { Linea } from '../../Shared/Models/Linea';

import 'jquery-qubit';
import 'jquery-bonsai';
import 'jquery-bonsai/jquery.bonsai.css';
import 'select2';
import 'select2/dist/css/select2.css';
import { MaskFormats, IMaskFormat } from '../../Shared/Utils/MaskFormats';
import IMask from "imask";
import { MaskFormatsManager } from '../../Shared/Utils';

export class LineasGestionPage {

    //PROPIEDADES
    public onLineasCreado?: (linea: Linea) => void;
    public onLineasActualizado?: (linea: Linea) => void;

    //CAMPOS
    private _baseUrl = "/Lineas";
    private _httpService: HttpFetchService;
    private _modalInstance: M.Modal;
    private _modalBase: HTMLElement;
    private _formLinea: HTMLFormElement;
    //private _selects: M.FormSelect[];
    //private _collaps: M.Collapsible[];
    private _formData: FormData;
    private _lineaIdActualizar: string;
    private _maskManager: MaskFormatsManager;

    //CONSTRUCTOR
    constructor(modalBase: HTMLElement) {

        this._httpService = new HttpFetchService();
        this._maskManager = new MaskFormatsManager();
        this._modalBase = modalBase;
        this.InicializarModal();
        this.RegistrarFormatos();
    }

    //METODOS

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

    //Creación de nueva linea
    public NuevoLinea() {

        this._httpService.Post<string>(this._baseUrl + "/NuevaLinea", null, false)
            .then((data) => {

                this._modalBase.innerHTML = data;
                this.InicilizarFormLinea(true);
                this._maskManager.ApplyMasks();
            });

        this._modalInstance.open();
    }

    //Edicion de linea
    public DatosLinea(idLinea: string, idTerminal: string, lectura: boolean) {

        var payload = { IdLinea: idLinea, IdTerminal: idTerminal, lectura: lectura };
        this._lineaIdActualizar = idLinea;

        this._httpService.Post<string>(this._baseUrl + "/EditarLinea", payload, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormLinea(false);
                this._maskManager.ApplyMasks();
            });

        this._modalInstance.open();
    }

    //Inicializar controles de formulario
    private InicilizarFormLinea(modoCreacion: boolean): void {

        $(".select2").select2({ dropdownAutoWidth: true, width: '100%' });

        M.updateTextFields();

        this._formLinea = document.querySelector("#linea-form");
        this._formData = new FormData(this._formLinea);
        $(this._formLinea).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(this._formLinea);
        document.getElementById("linea-modal-cancel").addEventListener('click', (ev) => this.CerrarModal());

        this._formLinea.onsubmit = (e) => {

            e.preventDefault();

            if ($(this._formLinea).valid()) {

                this._formData = new FormData(this._formLinea);
                this._maskManager.SetUnmaskedFormValue(this._formData);

                //formData.forEach((value, key) => { console.log(key + ": " + value) });

                if (modoCreacion)
                    this.GuardarLinea();
                else
                    this.ActualizarLinea();
            }

            return false; // prevent reload
        };

    }

    //Guardar linea
    private GuardarLinea(): void {

        let token = this._formData.get("RequestVerificationToken")?.toString(); // FormData accede a los campos del formulario para hacer get/set de valores

        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/CrearLinea", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    this.LimpiarFormulario();
                    if (this.onLineasCreado)
                        this.onLineasCreado((data.Payload as Linea));
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

            })
            .catch((err) => console.log(err));
    }

    //Actualizar linea
    private ActualizarLinea(): void {

        let token = this._formData.get("RequestVerificationToken")?.toString();

        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/ActualizarLinea", this._formData, token)
            .then((data) => {
                if (data.Result) {

                    if (this.onLineasActualizado)
                        this.onLineasActualizado((data.Payload as Linea));
                    this.LimpiarFormulario();
                    this.CerrarModal();
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });
            })
            .catch((err) => console.log(err));
    }

    //Reset de formulario
    private LimpiarFormulario() {

        this._formLinea.reset();
        $('input').not("input[type= 'hidden']").val(null); // Elementos input que no son type= 'hidden'
        $('.select2').val(null).trigger('change');
        $('#linea-observaciones').focus();
        M.updateTextFields();
        $("ul#linea-terminals").bonsai('update');
        $('#linea-observaciones').blur();
        //this._maskManager.ApplyMasks();
    }

    //Cerrar control modal
    private CerrarModal() {

        //this._collaps.every((collaps) => collaps.destroy());
        this._modalInstance.close();
        this._formLinea.parentNode.removeChild(this._formLinea);
        this._lineaIdActualizar = "";
    }

    //Formatear inputs
    private RegistrarFormatos() {

        //registrar mascaras 
        this._maskManager.RegisterMasks(
            [
                MaskFormats.IntegerFormatRang('#linea-Capacidad', 0, 5000000),
                MaskFormats.DecimalFormatRang('#linea-densidad', 0, 100),
            ]);

    }

}
import { HttpFetchService, IMessageResponse } from '../../Shared';
import { Contador } from '../../Shared/Models/Contador';

import 'jquery-qubit';
import 'jquery-bonsai';
import 'jquery-bonsai/jquery.bonsai.css';
import 'select2';
import 'select2/dist/css/select2.css';
import { MaskFormats, IMaskFormat } from '../../Shared/Utils/MaskFormats';
import { MaskFormatsManager } from '../../Shared/Utils';
import { Page } from '../../Core/Page';


export class ContadoresGestionPage extends Page {

    //PROPIEDADES
    public onContadorCreado?: (contador: Contador) => void;
    public onContadorActualizado?: (contador: Contador) => void;

    //CAMPOS
    private _baseUrl = "/Contadores";
    private _httpService: HttpFetchService;
    private _modalInstance: M.Modal;
    private _modalBase: HTMLElement;
    private _formContador: HTMLFormElement;
    private _formData: FormData;
    private _contadorIdActualizar: string;
    //private _maskManager: MaskFormatsManager;

    //CONSTRUCTOR
    constructor(modalBase: HTMLElement) {
        super();
        this._httpService = new HttpFetchService();
        //this._maskManager = new MaskFormatsManager();
        this._modalBase = modalBase;
        this.InicializarModal();
        this.RegistrarFormatos();
    }

    //METODOS

    //Creacion de nuevo contador
    public NuevoContador() {
        this._httpService.Post<string>(this._baseUrl + "/NuevoContador", null, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormContador(true);
                this.MaskManager.ApplyMasks();
            });

        this._modalInstance.open();
    }

    //Guardar contador
    private GuardarContador(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/CrearContador", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    this.LimpiarFormulario();
                    if (this.onContadorCreado) {
                        this.onContadorCreado((data.Payload as Contador));
                        this.MaskManager.ApplyMasks();
                    }
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

            })
            .catch((err) => console.log(err));
    }

    ////Edicion de contador
    //public DatosContador(idContador: string, lectura: boolean) {
    //    var payload = { Contador: idContador, lectura: lectura };
    //    this._contadorIdActualizar = idContador;
    //    this._httpService.Post<string>(this._baseUrl + "/EditarContador", payload, false)
    //        .then((data) => {
    //            this._modalBase.innerHTML = data;
    //            this.InicilizarFormContador(false);
    //        });

    //    this._modalInstance.open();
    //}

    ////Actualizar contador
    //private ActualizarContador(): void {
    //    let token = this._formData.get("RequestVerificationToken")?.toString();
    //    this._formData.set("IdContador", this._contadorIdActualizar);
    //    this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/ActualizarContador", this._formData, token)
    //        .then((data) => {
    //            if (data.Result) {
    //                if (this.onContadorActualizado)
    //                    this.onContadorActualizado((data.Payload as Contador));
    //                this.LimpiarFormulario();
    //                this.CerrarModal();
    //            }
    //            M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });
    //        })
    //        .catch((err) => console.log(err));
    //}

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
    private InicilizarFormContador(modoCreacion: boolean): void {
        $(".select2").select2({ dropdownAutoWidth: true, width: '100%' });
        M.updateTextFields();

        this._formContador = document.querySelector("#contador-form");
        this._formData = new FormData(this._formContador);
        $(this._formContador).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(this._formContador);
        document.getElementById("contador-modal-cancel").addEventListener('click', (ev) => this.CerrarModal());

        this._formContador.onsubmit = (e) => {
            e.preventDefault();

            if ($(this._formContador).valid()) {
                this._formData = new FormData(this._formContador);
                //formData.forEach((value, key) => { console.log(key + ": " + value) });

                if (modoCreacion)
                    this.GuardarContador();
                //else
                    //this.ActualizarContador();
            }

            return false; // prevent reload
        };
    }

    //Formatear inputs
    private RegistrarFormatos() {
        //registrar mascaras 
        this.RegisterMasks(
            [
                MaskFormats.Alfanumerico('#contador-id'),
            ]);
    }

    //Reset de formulario
    private LimpiarFormulario() {
        this._formContador.reset();
        $('input').not("input[type= 'hidden']").val(null);
        $('#select2').val(null).trigger('change');
        M.updateTextFields();
        $("ul#contador-terminals").bonsai('update');
    }

    //Cerrar control modal
    private CerrarModal() {
        this._modalInstance.close();
        this._formContador.parentNode.removeChild(this._formContador);
        this._contadorIdActualizar = "";
    }
}
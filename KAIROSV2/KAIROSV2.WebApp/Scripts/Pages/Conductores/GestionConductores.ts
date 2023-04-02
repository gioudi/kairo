
import { HttpFetchService, IMessageResponse } from '../../Shared';
import { Conductor } from '../../Shared/Models/Conductor';

export class ConductoresGestionPage {

    //PROPIEDADES
    public onConductorCreado?: (conductor: Conductor) => void;
    public onConductorActualizado?: (conductor: Conductor) => void;

    //CAMPOS
    private _baseUrl = "/Conductores";
    private _httpService: HttpFetchService;
    private _modalInstance: M.Modal;
    private _modalBase: HTMLElement;
    private _formConductor: HTMLFormElement;
    private _formData: FormData;
    private _conductorIdActualizar: string;

    //CONSTRUCTOR
    constructor(modalBase: HTMLElement) {
        this._httpService = new HttpFetchService();
        this._modalBase = modalBase;
        this.InicializarModal();
    }

    //METODOS

    //Creacion de nuevo conductor
    public NuevoConductor() {
        this._httpService.Post<string>(this._baseUrl + "/NuevoConductor", null, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormConductor(true);
            });

        this._modalInstance.open();
    }

    //Guardar conductor
    private GuardarConductor(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/CrearConductor", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    this.LimpiarFormulario();
                    if (this.onConductorCreado)
                        this.onConductorCreado(this.ExtraerConductor());
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

            })
            .catch((err) => console.log(err));
    }

    //Edicion y detalles de conductor
    public DatosConductor(cedula: string, lectura: boolean) {
        var payload = { idNumEntidad: parseInt(cedula), lectura: lectura };
        this._conductorIdActualizar = cedula;
        this._httpService.Post<string>(this._baseUrl + "/DatosConductor", payload, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormConductor(false);
            });

        this._modalInstance.open();
    }

    //Actualizar conductor
    private ActualizarConductor(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._formData.set("Cedula", this._conductorIdActualizar);
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/ActualizarConductor", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    if (this.onConductorActualizado)
                        this.onConductorActualizado(this.ExtraerConductor());
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
    private InicilizarFormConductor(modoCreacion: boolean): void {
        M.updateTextFields();

        this._formConductor = document.querySelector("#conductor-form");
        this._formData = new FormData(this._formConductor);
        $(this._formConductor).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(this._formConductor);
        document.getElementById("conductor-modal-cancel").addEventListener('click', (ev) => this.CerrarModal());
        document.getElementById("name").addEventListener('keydown', (ev) => this.SoloLetras(ev) )

        this._formConductor.onsubmit = (e) => {
            e.preventDefault();

            if ($(this._formConductor).valid()) {
                this._formData = new FormData(this._formConductor);
                //formData.forEach((value, key) => { console.log(key + ": " + value) });

                if (modoCreacion)
                    this.GuardarConductor();
                else
                    this.ActualizarConductor();
            }

            return false; // prevent reload
        };
    }


    private SoloLetras(event) {
        var regex = new RegExp("^[0-9]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (regex.test(key)) {
            event.preventDefault();
            return false;
        }
    }

    //Extraer conductor de formulario
    private ExtraerConductor(): Conductor {
        let conductor = new Conductor();
        conductor.Nombre = this._formData.get("Nombre")?.toString();
        conductor.Cedula = this._formData.get("Cedula")?.toString();
        return conductor;
    }

    //Reset de formulario
    private LimpiarFormulario() {
        this._formConductor.reset();
        M.updateTextFields();
    }

    //Cerrar control modal
    private CerrarModal() {
        this._modalInstance.close();
        this._formConductor.parentNode.removeChild(this._formConductor);
        this._conductorIdActualizar = "";
    }  


}
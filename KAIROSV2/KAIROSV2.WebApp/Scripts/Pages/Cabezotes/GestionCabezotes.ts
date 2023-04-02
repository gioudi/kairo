
import { HttpFetchService, IMessageResponse } from '../../Shared';
import { Cabezote } from '../../Shared/Models/Cabezote';

export class CabezotesGestionPage {

    //PROPIEDADES
    public onCabezoteCreado?: (cabezote: Cabezote) => void;
    public onCabezoteActualizado?: (cabezote: Cabezote) => void;

    //CAMPOS
    private _baseUrl = "/Cabezotes";
    private _httpService: HttpFetchService;
    private _modalInstance: M.Modal;
    private _modalBase: HTMLElement;
    private _formCabezote: HTMLFormElement;
    private _formData: FormData;

    //CONSTRUCTOR
    constructor(modalBase: HTMLElement) {
        this._httpService = new HttpFetchService();
        this._modalBase = modalBase;
        this.InicializarModal();
    }

    //METODOS

    //Creacion de nuevo cabezote
    public NuevoCabezote() {
        this._httpService.Post<string>(this._baseUrl + "/NuevoCabezote", null, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormCabezote();
            });

        this._modalInstance.open();
    }

    //Guardar cabezote
    private GuardarCabezote(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/CrearCabezote", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    this.LimpiarFormulario();
                    if (this.onCabezoteCreado)
                        this.onCabezoteCreado(this.ExtraerCabezote());
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
            startingTop: '10%',
            endingTop: '15%'
        });

    }

    //Inicializar controles de formulario
    private InicilizarFormCabezote(): void {
        M.updateTextFields();

        this._formCabezote = document.querySelector("#cabezote-form");
        this._formData = new FormData(this._formCabezote);
        $(this._formCabezote).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(this._formCabezote);
        document.getElementById("cabezote-modal-cancel").addEventListener('click', (ev) => this.CerrarModal());

        this._formCabezote.onsubmit = (e) => {
            e.preventDefault();

            if ($(this._formCabezote).valid()) {
                this._formData = new FormData(this._formCabezote);
                //formData.forEach((value, key) => { console.log(key + ": " + value) });
                this.GuardarCabezote();
            }

            return false; // prevent reload
        };
    }

    //Extraer cabezote de formulario
    private ExtraerCabezote(): Cabezote {
        let cab = new Cabezote();
        cab.Placa = this._formData.get("Placa")?.toString();

        return cab;
    }

    //Reset de formulario
    private LimpiarFormulario() {
        this._formCabezote.reset();
        $('#cabezote-id').val(null).trigger('change');
        M.updateTextFields();
    }

    //Cerrar control modal
    private CerrarModal() {
        this._modalInstance.close();
        this._formCabezote.parentNode.removeChild(this._formCabezote);
    }


}

import { HttpFetchService, IMessageResponse } from '../../Shared';
import { Trailer } from '../../Shared/Models/Trailer';

export class TrailersGestionPage {

    //PROPIEDADES
    public onTrailerCreado?: (trailer: Trailer) => void;
    public onTrailerActualizado?: (trailer: Trailer) => void;

    //CAMPOS
    private _baseUrl = "/Trailers";
    private _httpService: HttpFetchService;
    private _modalInstance: M.Modal;
    private _modalBase: HTMLElement;
    private _formTrailer: HTMLFormElement;
    private _formData: FormData;

    //CONSTRUCTOR
    constructor(modalBase: HTMLElement) {
        this._httpService = new HttpFetchService();
        this._modalBase = modalBase;
        this.InicializarModal();
    }

    //METODOS

    //Creacion de nuevo Trailer
    public NuevoTrailer() {
        this._httpService.Post<string>(this._baseUrl + "/NuevoTrailer", null, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormTrailer();
            });

        this._modalInstance.open();
    }

    //Guardar Trailer
    private GuardarTrailer(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/CrearTrailer", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    this.LimpiarFormulario();
                    if (this.onTrailerCreado)
                        this.onTrailerCreado(this.ExtraerTrailer());
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
    private InicilizarFormTrailer(): void {
        M.updateTextFields();

        this._formTrailer = document.querySelector("#trailer-form");
        this._formData = new FormData(this._formTrailer);
        $(this._formTrailer).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(this._formTrailer);
        document.getElementById("trailer-modal-cancel").addEventListener('click', (ev) => this.CerrarModal());

        this._formTrailer.onsubmit = (e) => {
            e.preventDefault();

            if ($(this._formTrailer).valid()) {
                this._formData = new FormData(this._formTrailer);
                //formData.forEach((value, key) => { console.log(key + ": " + value) });
                this.GuardarTrailer();
            }

            return false; // prevent reload
        };
    }

    //Extraer Trailer de formulario
    private ExtraerTrailer(): Trailer {
        let cab = new Trailer();
        cab.Placa = this._formData.get("Placa")?.toString();

        return cab;
    }

    //Reset de formulario
    private LimpiarFormulario() {
        this._formTrailer.reset();
        $('#trailer-id').val(null).trigger('change');
        M.updateTextFields();
    }

    //Cerrar control modal
    private CerrarModal() {
        this._modalInstance.close();
        this._formTrailer.parentNode.removeChild(this._formTrailer);
    }


}
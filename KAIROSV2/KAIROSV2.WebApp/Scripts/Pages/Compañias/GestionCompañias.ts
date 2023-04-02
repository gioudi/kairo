
import { HttpFetchService, IMessageResponse } from '../../Shared';
import { Compañia } from '../../Shared/Models/Compañia';

export class CompañiasGestionPage {

    //PROPIEDADES
    public onCompañiaCreado?: (compañia: Compañia) => void;
    public onCompañiaActualizado?: (compañia: Compañia) => void;

    //CAMPOS
    private _baseUrl = "/Compañias";
    private _httpService: HttpFetchService;
    private _modalInstance: M.Modal;
    private _modalBase: HTMLElement;
    private _formCompañia: HTMLFormElement;
    private _formData: FormData;
    private _compañiaIdActualizar: string;

    //CONSTRUCTOR
    constructor(modalBase: HTMLElement) {
        this._httpService = new HttpFetchService();
        this._modalBase = modalBase;
        this.InicializarModal();
    }

    //METODOS

    //Creacion de nueva compañia
    public NuevaCompañia() {
        this._httpService.Post<string>(this._baseUrl + "/NuevaCompañia", null, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormCompañia(true);
            });

        this._modalInstance.open();
    }

    //Guardar Compañia
    private GuardarCompañia(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/CrearCompañia", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    this.LimpiarFormulario();
                    if (this.onCompañiaCreado)
                        this.onCompañiaCreado(this.ExtraerCompañia());
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

            })
            .catch((err) => console.log(err));
    }

    //Edicion y detalles de Compañia
    public DatosCompañia(idCompañia: string, lectura: boolean) {
        var payload = { compañia: idCompañia, lectura: lectura };
        this._compañiaIdActualizar = idCompañia;
        this._httpService.Post<string>(this._baseUrl + "/DatosCompañia", payload, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormCompañia(false);
            });

        this._modalInstance.open();
    }

    //Actualizar Compañia
    private ActualizarCompañia(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._formData.set("IdCompañia", this._compañiaIdActualizar);
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/ActualizarCompañia", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    if (this.onCompañiaActualizado)
                        this.onCompañiaActualizado(this.ExtraerCompañia());
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
    private InicilizarFormCompañia(modoCreacion: boolean): void {
        M.updateTextFields();

        this._formCompañia = document.querySelector("#compañia-form");
        this._formData = new FormData(this._formCompañia);
        $(this._formCompañia).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(this._formCompañia);
        document.getElementById("compañia-modal-cancel").addEventListener('click', (ev) => this.CerrarModal());

        this._formCompañia.onsubmit = (e) => {
            e.preventDefault();

            if ($(this._formCompañia).valid()) {
                this._formData = new FormData(this._formCompañia);
                //formData.forEach((value, key) => { console.log(key + ": " + value) });

                if (modoCreacion)
                    this.GuardarCompañia();
                else
                    this.ActualizarCompañia();
            }

            return false; // prevent reload
        };
    }

    //Extraer compañia de formulario
    private ExtraerCompañia(): Compañia {
        let compañia = new Compañia();

        compañia.IdCompañia = this._formData.get("IdCompañia")?.toString();
        compañia.Nombre = this._formData.get("Compañia")?.toString();
        compañia.SalesOrganization = this._formData.get("SalesOrganization")?.toString();
        compañia.DistributionChannel = this._formData.get("DistributionChannel")?.toString();
        compañia.Division = this._formData.get("Division")?.toString();
        compañia.SupplierType = this._formData.get("SupplierType")?.toString();
        compañia.CodigoSICOM = this._formData.get("CodigoSICOM")?.toString();

        return compañia;
    }

    //Reset de formulario
    private LimpiarFormulario() {
        this._formCompañia.reset();
        $('#compañia-id').val(null).trigger('change');
        M.updateTextFields();
    }

    //Cerrar control modal
    private CerrarModal() {
        this._modalInstance.close();
        this._formCompañia.parentNode.removeChild(this._formCompañia);
        this._compañiaIdActualizar = "";
    }
}
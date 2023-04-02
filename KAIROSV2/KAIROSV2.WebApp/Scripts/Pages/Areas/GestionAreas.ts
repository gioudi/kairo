
import { HttpFetchService, IMessageResponse } from '../../Shared';
import { Area } from '../../Shared/Models/Area';

export class AreasGestionPage {

    //PROPIEDADES
    public onAreaCreado?: (area: Area) => void;
    public onAreaActualizado?: (area: Area) => void;

    //CAMPOS
    private _baseUrl = "/Areas";
    private _httpService: HttpFetchService;
    private _modalInstance: M.Modal;
    private _modalBase: HTMLElement;
    private _formArea: HTMLFormElement;
    private _formData: FormData;
    private _areaIdActualizar: string;

    //CONSTRUCTOR
    constructor(modalBase: HTMLElement) {
        this._httpService = new HttpFetchService();
        this._modalBase = modalBase;
        this.InicializarModal();
    }

    //METODOS

    //Creacion de nueva area
    public NuevaArea() {
        this._httpService.Post<string>(this._baseUrl + "/NuevaArea", null, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormArea(true);
            });

        this._modalInstance.open();
    }

    //Guardar Area
    private GuardarArea(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/CrearArea", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    this.LimpiarFormulario();
                    if (this.onAreaCreado)
                        this.onAreaCreado(this.ExtraerArea());
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

            })
            .catch((err) => console.log(err));
    }

    //Edicion y detalles de area
    public DatosArea(idArea: string, lectura: boolean) {
        var payload = { idEntidad: idArea, lectura: lectura };
        this._areaIdActualizar = idArea;
        this._httpService.Post<string>(this._baseUrl + "/DatosArea", payload, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormArea(false);
            });

        this._modalInstance.open();
    }

    //Actualizar area
    private ActualizarArea(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._formData.set("IdArea", this._areaIdActualizar);
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/ActualizarArea", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    if (this.onAreaActualizado)
                        this.onAreaActualizado(this.ExtraerArea());
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
    private InicilizarFormArea(modoCreacion: boolean): void {
        M.updateTextFields();

        this._formArea = document.querySelector("#area-form");
        this._formData = new FormData(this._formArea);
        $(this._formArea).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(this._formArea);
        //this.ConfigurarBusquedaUsuario();
        document.getElementById("area-modal-cancel").addEventListener('click', (ev) => this.CerrarModal());

        this._formArea.onsubmit = (e) => {
            e.preventDefault();

            if ($(this._formArea).valid()) {
                this._formData = new FormData(this._formArea);
                //formData.forEach((value, key) => { console.log(key + ": " + value) });

                if (modoCreacion)
                    this.GuardarArea();
                else
                    this.ActualizarArea();
            }

            return false; // prevent reload
        };
    }

    //Extraer area de formulario
    private ExtraerArea(): Area {
        let area = new Area();

        area.IdArea = this._formData.get("IdArea")?.toString();
        area.Nombre = this._formData.get("Area")?.toString();

        return area;
    }

    //Reset de formulario
    private LimpiarFormulario() {
        this._formArea.reset();
        $('#area-id').val(null).trigger('change');
        M.updateTextFields();
    }

    //Cerrar control modal
    private CerrarModal() {
        this._modalInstance.close();
        this._formArea.parentNode.removeChild(this._formArea);
        this._areaIdActualizar = "";
    }
}
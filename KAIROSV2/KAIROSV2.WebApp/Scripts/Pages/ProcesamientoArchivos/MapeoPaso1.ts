import { HttpFetchService, Paso1Mapeo } from "../../Shared";

export class MapeoPaso1 {

    //PROPIEDADES
    public onPaso1Cerrar?: () => void;
    public onPaso1Completado?: () => void;

    //CAMPOS
    private _baseUrl = "/ProcesamientoArchivos";
    private _httpService: HttpFetchService;
    private _formMapeoPaso1: HTMLFormElement;
    private _dataFormPaso1: FormData;
    private _select: M.FormSelect;
    private _mStepper: MStepper;
    private _step1Model: Paso1Mapeo;

    constructor(stepper: MStepper, step1: Paso1Mapeo) {
        this._httpService = new HttpFetchService();
        this._mStepper = stepper;
        this._step1Model = step1;
    }

    //--- METODOS -----------------------------------------------------------------

    //Inicializa controles paso 1
    public InicializarControlesPaso1(modoCreacion: boolean) {
        $('.btn.close').on('click', () => this.onPaso1Cerrar());
        let selectSeparator = document.querySelector<HTMLSelectElement>("#datos-iniciales-separador");
        this._select = M.FormSelect.init(selectSeparator);
        selectSeparator.addEventListener("change", (event) => this.CheckSepartorOption((event.target as HTMLSelectElement)));

        $("#select-files").on("click", function () { $("#mapeo-archivo").click(); })
        this.InicilizarFormMapeoPaso1(modoCreacion);
    }

    //Inicializa formulario paso 1
    private InicilizarFormMapeoPaso1(modoCreacion: boolean): void {
        this._formMapeoPaso1 = document.querySelector('#mapeo-form-step1');
        this._dataFormPaso1 = new FormData(this._formMapeoPaso1);
        $(this._formMapeoPaso1).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(this._formMapeoPaso1);

        this._formMapeoPaso1.onsubmit = (e) => {
            e.preventDefault();
            if ($(this._formMapeoPaso1).valid()) {
                this._dataFormPaso1 = new FormData(this._formMapeoPaso1);
                //this._dataFormPaso1.forEach((value, key) => { console.log(key + ": " + value) });
                this.ProcesarDatosIniciales();
            }
            else {
                this._mStepper.wrongStep();
            }

            return false; // prevent reload
        };
    }

    //-- OPERACIONES SERVIDOR -----------------------------------------------------

    //Procesar archivo
    private ProcesarDatosIniciales(): void {
        this.ExtraerDatosPaso1();
        let token = this._dataFormPaso1.get("RequestVerificationToken")?.toString();
        this._httpService.PostForm<string>(this._baseUrl + "/PrevisualizarArchivo", this._dataFormPaso1, token, false)
            .then((data) => {
                //console.log(data);
                document.getElementById("step-2-content").innerHTML = data;
                this._mStepper.nextStep(null);
                this._mStepper.updateStepper();
                if (this.onPaso1Completado)
                    this.onPaso1Completado();
            })
            .catch((err) => console.log(err));
    }

    //-- OPERACIONES DE DATOS  -----------------------------------------------------

    //Extraer datos paso1
    private ExtraerDatosPaso1(): void {
        this._step1Model.IdMapeo = this._dataFormPaso1.get("IdMapeo")?.toString();
        this._step1Model.Descripcion = this._dataFormPaso1.get("Descripcion")?.toString();
        this._step1Model.Separador = this._dataFormPaso1.get("Separador")?.toString();
        this._step1Model.OtroCaracter = this._dataFormPaso1.get("OtroCaracter")?.toString();
        this._step1Model.Archivo = (this._dataFormPaso1.get("Archivo") as File);
    }

    private CheckSepartorOption(element: HTMLSelectElement): void {

        if (element.value == "95")
            document.querySelector<HTMLInputElement>("#mapeo-other").removeAttribute("disabled");
        else
            document.querySelector<HTMLInputElement>("#mapeo-other").setAttribute("disabled", "true");
    }

    public Destroy() {
        this._select.destroy();
        this._step1Model = null;
    }
}

import { HttpFetchService, IMessageResponse } from '../../Shared';
import { Paso1Mapeo, Paso2Mapeo, MapeoArchivos } from '../../Shared/Models/ProcesamientoArchivos';
import 'materialize-stepper/dist/js/mstepper.js';
import 'materialize-stepper/dist/css/mstepper.css';
import 'datatables.net-responsive';
import { TablaSistemaColumna, TablasSistema } from '../../Shared/Models/TablasKairos';
import { ConfigureDataTable } from '../../Shared/Components';
import { MapeoPaso1 } from './MapeoPaso1';
import { MapeoPaso2 } from './MapeoPaso2';
import { MapeoPaso3 } from './MapeoPaso3';

export class GestionMapeoArchivo {

    //PROPIEDADES
    public onProcesamientoArchivosGuardado?: () => void;

    //CAMPOS
    private _baseUrl = "/ProcesamientoArchivos";
    private _httpService: HttpFetchService;
    private _modalInstance: M.Modal;
    private _modalBase: HTMLElement;
    private _mStepper: MStepper;
    private _dataMapeoArchivo: MapeoArchivos;
    private _defaultActiveStep: number;
    private _esNuevo: boolean;
    private _actualizarMapeo: boolean;
    private _paso1Vista: MapeoPaso1;
    private _paso2Vista: MapeoPaso2;
    private _paso3Vista: MapeoPaso3;

    //CONSTRUCTOR
    constructor(modalBase: HTMLElement) {
        this._httpService = new HttpFetchService();

        this._modalBase = modalBase;
        this.InicializarModal();
    }

    //METODOS

    //Creación de nueva Mapeo
    public NuevoProcesamientoArchivos() {
        this._defaultActiveStep = 0;
        this._esNuevo = true;
        this._actualizarMapeo = false;
        this._dataMapeoArchivo = new MapeoArchivos();
        this._httpService.Post<string>(this._baseUrl + "/NuevoProcesamientoArchivos", null, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicializarStepper();
                this.InicializarControles(1);
                this._modalInstance.open();
            });
    }

    //Edición y detalles de mapeo
    public DatosProcesamientoArchivos(idMapeo: string, lectura: boolean) {
        this._esNuevo = false;
        this._actualizarMapeo = true;
        this._defaultActiveStep = 2;
        this._dataMapeoArchivo = new MapeoArchivos();
        this._dataMapeoArchivo.Paso1Data = new Paso1Mapeo();
        this._dataMapeoArchivo.Paso2Data = new Paso2Mapeo();
        var payload = { idEntidad: idMapeo, lectura: lectura };
        this._httpService.Post<string>(this._baseUrl + "/DatosProcesamientoArchivos", payload, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicializarStepper();
                this.InicializarControles(3);
                this._modalInstance.open();
            });
    }

    //-- INICIALIZA CONTROLES GENERALES --

    //Inicializar control modal y stepper
    private InicializarModal() {

        this._modalInstance = M.Modal.init(this._modalBase, {
            inDuration: 300,
            outDuration: 200,
            onOpenEnd: () => {

            }
        });
    }

    private InicializarStepper() {
        this._mStepper = new MStepper(document.querySelector(".stepper"), {
            firstActive: this._defaultActiveStep,
            showFeedbackPreloader: true,
            stepTitleNavigation: false,
            autoFocusInput: false,
            autoFormCreation: false,
            feedbackPreloader: '<div class="spinner-layer spinner-blue-only">...</div>'
        });

        this._mStepper.resetStepper();
    }

    //Inicializa controles general
    private InicializarControles(paso: number): void {
        M.updateTextFields();
        //TODO: Review if is needed destroy steps instances
        if (paso == 1) {
            this._paso1Vista?.Destroy();
            this._dataMapeoArchivo.Paso1Data = new Paso1Mapeo();
            this._paso1Vista = new MapeoPaso1(this._mStepper, this._dataMapeoArchivo.Paso1Data)
            this._paso1Vista.InicializarControlesPaso1(this._esNuevo);
            this._paso1Vista.onPaso1Completado = () => this.InicializarControles(2);
            this._paso1Vista.onPaso1Cerrar = () => this.CerrarModal();
        }
        else if (paso == 2) {
            this._paso2Vista?.Destroy();
            this._dataMapeoArchivo.Paso2Data = new Paso2Mapeo();
            this._paso2Vista = new MapeoPaso2(this._mStepper, this._dataMapeoArchivo.Paso2Data);
            this._paso2Vista.InicializarControlesPaso2(this._esNuevo);
            this._paso2Vista.onPaso2Completado = () => this.NuevoMapeoArchivoTablas();
            this._paso2Vista.onPaso2Cerrar = () => this.CerrarModal();
        }
        else if (paso == 3) {
            this._paso3Vista?.Destroy();
            this._paso3Vista = new MapeoPaso3(this._mStepper, this._dataMapeoArchivo);
            this._paso3Vista.InicializarControlesPaso3(this._esNuevo, this._actualizarMapeo);
            this._paso3Vista.onPaso3Anterior = () => this.InicializarControles(1);
            this._paso3Vista.onPaso3Completado = () => this.CerrarModal();
            this._paso3Vista.onPaso3Cerrar = () => this.CerrarModal();
            //TODO: Destroy evertything
        }
    }

    //Crear nuevo mapeo archivo
    private NuevoMapeoArchivoTablas(): void {
        this._httpService.PostForm<string>(this._baseUrl + "/NuevoMapeoArchivoTablas", null, null, false)
            .then((data) => {
                document.getElementById("step-3-content").innerHTML = data;
                this._mStepper.updateStepper();
                this._actualizarMapeo = !this._esNuevo;
                this._esNuevo = true;
                this.InicializarControles(3);
            })
            .catch((err) => console.log(err));
    }

    //Cerrar control modal
    private CerrarModal() {
        if (this.onProcesamientoArchivosGuardado)
            this.onProcesamientoArchivosGuardado();


        this._modalInstance.close();
        //this._paso1Vista?.Destroy();
        this._paso1Vista = null;
        this._paso2Vista?.Destroy();
        this._paso2Vista = null;
        this._paso3Vista?.Destroy();
        this._paso3Vista = null;
        this._dataMapeoArchivo = null;
        //this._formMapeoPaso1.parentNode.removeChild(this._formMapeoPaso1);
    }
}
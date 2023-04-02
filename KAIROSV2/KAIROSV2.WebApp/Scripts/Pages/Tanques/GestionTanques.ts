import { HttpFetchService, IMessageResponse, Log } from '../../Shared';
import { Tanque } from '../../Shared/Models/Tanque';

import 'jquery-qubit';
import 'jquery-bonsai';
import 'jquery-bonsai/jquery.bonsai.css';
import 'select2';
import 'select2/dist/css/select2.css';
import { MaskFormats, IMaskFormat } from '../../Shared/Utils/MaskFormats';
import dayjs = require('dayjs');
import { MaskFormatsManager } from '../../Shared/Utils';
import { DatepickerComponent } from '../../Shared/Components/DatepickerComponent';
import { Page } from '../../Core/Page';

export class TanquesGestionPage extends Page {

    //PROPIEDADES
    public onTanqueCreado?: (tanque: Tanque) => void;
    public onTanqueActualizado?: (tanque: Tanque) => void;
    public onTanqueDetalle?: (tanque: Tanque) => void;

    //CAMPOS
    private _baseUrl = "/Tanques";
    private _httpService: HttpFetchService;
    private _modalInstance: M.Modal;
    private _modalBase: HTMLElement;
    private _formTanque: HTMLFormElement;
    private _selects: M.FormSelect[];
    private _collaps: M.Collapsible[];
    private _formData: FormData;
    private _tanqueIdActualizar: string;

    //CONSTRUCTOR
    constructor(modalBase: HTMLElement) {
        super();
        this._httpService = new HttpFetchService();
        this._modalBase = modalBase;
        this.InicializarModal();
        this.RegistrarFormatos();
    }

    //METODOS

    //Creacion de nuevo tanque
    public NuevoTanque() {
        this._httpService.Post<string>(this._baseUrl + "/NuevoTanque", null, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormTanque(true);
                this.MaskManager.ApplyMasks();
            });

        this._modalInstance.open();
    }

    //Guardar tanque
    private GuardarTanque(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        let PantallaFlotante = this._formData.get('PantallaFlotante')
        if (PantallaFlotante != 'true') {
            this._formData.set('DensidadAforo', '0');
            this._formData.set('GalonesPorGrado', '0');
            this._formData.set('NivelCorreccionInicial', '0');
            this._formData.set('NivelCorreccionFinal', '0');
        }
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/CrearTanque", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    this.LimpiarFormulario();
                    if (this.onTanqueCreado)
                        this.onTanqueCreado((data.Payload as Tanque));
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

            })
            .catch((err) => console.log(err));
    }

    //Edicion de tanque
    public DatosTanque(idTanque: string, idTerminal: string, lectura: boolean) {
        var payload = { Tanque: idTanque, IdTerminal: idTerminal, lectura: lectura };
        this._tanqueIdActualizar = idTanque;
        this._httpService.Post<string>(this._baseUrl + "/EditarTanque", payload, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormTanque(false);
                this.MaskManager.ApplyMasks();
            });

        this._modalInstance.open();
    }

    //Detalle de tanque
    public DatosDetalleTanque(idTanque: string, idTerminal: string, lectura: boolean) {
        var payload = { Tanque: idTanque, IdTerminal: idTerminal, lectura: lectura };
        this._tanqueIdActualizar = idTanque;
        this._httpService.Post<IMessageResponse>(this._baseUrl + "/DetalleTanque", payload, true)
            .then((data) => {
                //console.log(data);
                this.onTanqueDetalle((data.Payload as Tanque));
            });
    }

    //Actualizar tanque
    private ActualizarTanque(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        let PantallaFlotante = this._formData.get('PantallaFlotante')
        if (PantallaFlotante != 'true')
        {
            this._formData.set('DensidadAforo', '0');
            this._formData.set('GalonesPorGrado', '0');
            this._formData.set('NivelCorreccionInicial', '0');
            this._formData.set('NivelCorreccionFinal', '0');
        }
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/ActualizarTanque", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    if (this.onTanqueActualizado)
                        this.onTanqueActualizado((data.Payload as Tanque));
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
    private InicilizarFormTanque(modoCreacion: boolean): void {
        $(".select2").select2({ dropdownAutoWidth: true, width: '100%' });
        this._collaps = M.Collapsible.init(document.querySelectorAll(".collapsible"));
        var DatePickers = new DatepickerComponent().GetInstances('.datepicker', undefined, () => { this.UpdateMaskValue() });

        M.updateTextFields();
        this._formTanque = document.querySelector("#tanque-form");
        this._formData = new FormData(this._formTanque);
        $(this._formTanque).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(this._formTanque);
        document.getElementById("tanque-modal-cancel").addEventListener('click', (ev) => this.CerrarModal());

        this._formTanque.onsubmit = (e) => {
            e.preventDefault();

            if ($(this._formTanque).valid()) {
                this._formData = new FormData(this._formTanque);
                this.MaskManager.SetUnmaskedFormValue(this._formData);

                if (modoCreacion)
                    this.GuardarTanque();
                else
                    this.ActualizarTanque();
            }

            return false; // prevent reload
        };

        document.getElementById("switch").addEventListener('change', event => this.MostrarDatosPantallaFlotante(event));
        document.getElementById("tanque-collapsible").addEventListener('click', event => this.BajarScroll());
    }

    private MostrarDatosPantallaFlotante(event) {
        let pantallaFlotante = document.getElementById('ulPantallaFlotante');
        if (event.target['checked']) {
            pantallaFlotante.removeAttribute("hidden");
            this.MaskManager.ApplyMasks();
            this.BajarScroll()
        } else {
            pantallaFlotante.setAttribute("hidden", "");
        }
    }

    //Baja el scroll
    private BajarScroll() {
        $("#tanque-modalcontent").animate(
            { scrollTop: $('#tanque-modalcontent').prop("scrollHeight") }, 1000
        );
    }

    //Reset de formulario
    private LimpiarFormulario() {
        this._formTanque.reset();
        $('input').not("input[type= 'hidden']").val(null);
        $('.select2').val(null).trigger('change');
        M.updateTextFields();
        $("ul#tanque-terminals").bonsai('update');
        let pantallaFlotante = document.getElementById('ulPantallaFlotante');
        pantallaFlotante.setAttribute("hidden", "");
        //$('#tanque-observaciones').focus();
        //$('#tanque-observaciones').blur();
    }

    //Cerrar control modal
    private CerrarModal() {
        this._collaps.every((collaps) => collaps.destroy());
        this._modalInstance.close();
        this._formTanque.parentNode.removeChild(this._formTanque);
        this._tanqueIdActualizar = "";
    }

    //Formatear inputs
    private RegistrarFormatos() {
        //registrar mascaras 
        this.RegisterMasks(
            [
                MaskFormats.DateShortFormat(),
                //MaskFormats.DateLongFormat(),
                MaskFormats.IntegerFormatRang('#tanque-nivelCorreccionInicial', 0, 2500),
                MaskFormats.IntegerFormatRang('#tanque-nivelCorreccionFinal', 0, 2500),
                MaskFormats.IntegerFormatRang('#tanque-CapacidadNominal', 0, 50000),
                MaskFormats.IntegerFormatRang('#tanque-capacidadOperativa', 0, 43000),
                MaskFormats.IntegerFormatRang('#tanque-AlturaMaximaAforo', 0, 1700),
                MaskFormats.DecimalFormatRang('#tanque-densidadAforo', 0, 100),
                MaskFormats.DecimalFormatRang('#tanque-galonesPorGrado', 0, 100),
                MaskFormats.DecimalFormatRang('#tanque-volumenNoBombeable', 0, 8000),
                MaskFormats.Alfanumerico('#tanque-id')
            ]);
    }

    private UpdateMaskValue() {
        this.MaskManager.UpdateValue([".formato-fecha-corta"])
    }
}
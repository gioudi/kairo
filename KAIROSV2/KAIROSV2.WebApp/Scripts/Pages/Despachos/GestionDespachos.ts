import { HttpFetchService, IMessageResponse, Log } from '../../Shared';
import { Despacho } from '../../Shared/Models/Despacho';

import 'jquery-qubit';
import 'jquery-bonsai';
import 'jquery-bonsai/jquery.bonsai.css';
import 'select2';
import 'select2/dist/css/select2.css';
import { MaskFormats, IMaskFormat } from '../../Shared/Utils/MaskFormats';
import { ConfigureDataTable } from '../../Shared/Components/ConfigureDataTable';
import dayjs = require('dayjs');
import { MaskFormatsManager } from '../../Shared/Utils';
import { DatepickerComponent } from '../../Shared/Components/DatepickerComponent';
import { Page } from '../../Core/Page';

export class DespachosGestionPage extends Page {

    //PROPIEDADES
    public onDespachoCreado?: () => void;
    public onDespachoActualizado?: (despacho: Despacho) => void;

    //CAMPOS
    private _baseUrl = "/Despachos";
    private _httpService: HttpFetchService;
    private _modalInstance: M.Modal;
    private _modalBase: HTMLElement;
    private _formDespacho: HTMLFormElement;
    private _selects: M.FormSelect[];
    private _collaps: M.Collapsible[];
    private _formData: FormData;
    private _DespachoIdActualizar: string;
    private _VolumenBruto: string;
    private _despachosDatePicker: M.Datepicker;
    private _despachosFechaInicial: Date;
    private _crear: boolean;
    private _habilitarConsultaComp: boolean;
    private TableComponente: DataTables.Api;
    private _MaskFormatsComponentes: MaskFormatsManager;

    //CONSTRUCTOR
    constructor(modalBase: HTMLElement) {
        super();
        this._MaskFormatsComponentes = new MaskFormatsManager();
        this._httpService = new HttpFetchService();
        this._modalBase = modalBase;
        this.InicializarModal();
        this.RegistrarFormatos();
        this.GetInitialCloseDate();
    }

    //METODOS

    //Creacion de nuevo Despacho
    public NuevoDespacho(payload) {
        this._httpService.Post<string>(this._baseUrl + "/NuevoDespacho", payload, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormDespacho(true);
                this.MaskManager.ApplyMasks();
                this._MaskFormatsComponentes.ApplyMasks();
                this._crear = true;
                this._habilitarConsultaComp = true;
            });

        this._modalInstance.open();
    }

    private CalcularFactor() {
        let tabla = '#data-table-componentes tbody';
        $(tabla).find('tr').each(function () {
            let $tr = $(this),
                $volumenNeto = $tr.find('input.volumenNeto').val() + '',
                $volumenBruto = $tr.find('input.volumenBruto').val() + '',
                $factor = $tr.find('input.factor'),
                resFactor;
            $volumenNeto = $volumenNeto.replace(/,/g, "");
            $volumenBruto = $volumenBruto.replace(/,/g, "");
            let vn = parseFloat($volumenNeto);
            let vb = parseFloat($volumenBruto);
            resFactor = (vn / vb).toFixed(5);
            if (!isNaN(resFactor)) {
                $factor.val(resFactor);
                return;
            }
            $factor.val('0');
        })
    }

    //Guardar Despacho
    private GuardarDespacho(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        let compartimento = this._formData.get('Compartimento');
        this._formData.set('Terminal', (<HTMLInputElement>document.getElementById("despacho-terminal")).value);
        this._formData.set('Compañia', (<HTMLInputElement>document.getElementById("despacho-compañia")).value);
        this._formData.set('IdProducto', (<HTMLInputElement>document.getElementById("despacho-producto")).value);
        if (compartimento == "" || compartimento < "1" || compartimento > "5") {
            M.toast({ html: "Por favor seleccionar un compartimento", classes: "error" });
        }
        else {
            this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/CrearDespacho", this._formData, token)
                .then((data) => {
                    if (data.Result) {
                        this._habilitarConsultaComp = false;
                        if (this.onDespachoCreado)
                            this.onDespachoCreado();
                        this.CerrarModal();
                    }
                    M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

                })
                .catch((err) => console.log(err));
        }
    }

    //Edicion de Despacho
    public DatosDespacho(idDespacho: string, lectura: boolean) {
        var payload = { Despacho: idDespacho, lectura: lectura };
        this._DespachoIdActualizar = idDespacho;
        this._httpService.Post<string>(this._baseUrl + "/EditarDespacho", payload, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormDespacho(false);
                this.MaskManager.ApplyMasks();
                this._MaskFormatsComponentes.ApplyMasks();
            });

        this._modalInstance.open();
    }

    //Actualizar Despacho
    private ActualizarDespacho(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        let compartimento = this._formData.get('Compartimento');
        this._formData.set('Terminal', (<HTMLInputElement>document.getElementById("despacho-terminal")).value);
        this._formData.set('Compañia', (<HTMLInputElement>document.getElementById("despacho-compañia")).value);
        this._formData.set('IdProducto', (<HTMLInputElement>document.getElementById("despacho-producto")).value);
        if (compartimento == "" || compartimento < "1" || compartimento > "5") {
            M.toast({ html: "Por favor seleccionar un compartimento", classes: "error" });
        }
        else {
            this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/ActualizarDespacho", this._formData, token)
                .then((data) => {
                    if (data.Result) {
                        if (this.onDespachoActualizado)
                            this.onDespachoActualizado(this.ExtraerDespacho());
                        this.CerrarModal();
                    }
                    M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });
                })
                .catch((err) => console.log(err));
        }

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
    private InicilizarFormDespacho(modoCreacion: boolean): void {
        $(".select2").select2({ dropdownAutoWidth: true, width: '100%' });
        this.TableComponente = new ConfigureDataTable().ConfigureScrollXSinInfo("#data-table-componentes", [{ "width": "5%", "targets": 0 }]);
        document.getElementById('lista-componentes').addEventListener('keyup', event => {
            this.CalcularFactor();
        });
        this._despachosDatePicker = new DatepickerComponent().GetInstances2('#despacho-fecha',
            {
                onClose: () => { this.UpdateMaskValue() },
                onDraw: () => { this.ConfigClosedDates() },
                minDate: dayjs(this._despachosFechaInicial, "YYYY-MM-DD").toDate(),
                maxDate: new Date(),
                //defaultDate: new Date(),
                //setDefaultDate: true
            });

        M.updateTextFields();
        this._formDespacho = document.querySelector("#despacho-modal-form");
        this._formData = new FormData(this._formDespacho);
        $(this._formDespacho).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(this._formDespacho);

        document.getElementById("despacho-modal-cancel").addEventListener('click', (ev) => this.CerrarModal());

        this._formDespacho.onsubmit = (e) => {
            e.preventDefault();

            if ($(this._formDespacho).valid()) {
                this._formData = new FormData(this._formDespacho);
                this.MaskManager.SetUnmaskedFormValue(this._formData);

                if (modoCreacion)
                    this.GuardarDespacho();
                else
                    this.ActualizarDespacho();
            }
            return false; // prevent reload
        };

        var thes = this;
        $("#despacho-producto").change(function () { thes.ConsultarComponentes(); });
        $("#volumen-cargado").keyup(function () { thes.RecalcularVolBrutos(); });
    }

    private async ConsultarComponentes() {
        if (this._crear && this._habilitarConsultaComp) {
            try {
                let Terminal = (<HTMLInputElement>document.getElementById("despacho-terminal")).value;
                let Producto = (<HTMLInputElement>document.getElementById("despacho-producto")).value;
                let volCargado = (<HTMLInputElement>document.getElementById("volumen-cargado")).value;
                if (volCargado != "") {
                    var payload = { IdTerminal: Terminal, IdProducto: Producto, VolumenCargado: volCargado };
                    this._httpService.Post<string>("/Despachos/ObtenerRecetaActivaPorProductoTerminal", payload, false)
                        .then((data) => {
                            if (data) {
                                document.getElementById("lista-componentes").innerHTML = data;
                                $(".select2").select2({ dropdownAutoWidth: true, width: '100%' });
                                this.TableComponente = new ConfigureDataTable().ConfigureScrollXSinInfo("#data-table-componentes", [{ "width": "5%", "targets": 0 }]);
                                this._MaskFormatsComponentes.ApplyMasks();
                            }
                            else
                                M.toast({ html: data, classes: (false) ? "succes" : "error" });
                        }).catch((error) => {
                            M.toast({ html: "No fue posible cargar los componentes: " + error, classes: "error" });
                            console.log(error)
                        })
                }
                else {
                    M.toast({ html: "Por favor ingresar un volumen cargado para recalcular los componentes", classes: "error" });
                }
                return false; // prevent reload
            }
            catch (error) {
                M.toast({ html: "Algo salió mal y no fue posible consultar los componentes", classes: (false) ? "succes" : "error" });
            }
        }
    }

    private async RecalcularVolBrutos() {
        if (this._crear && this._habilitarConsultaComp) {
            try {
                let Terminal = (<HTMLInputElement>document.getElementById("despacho-terminal")).value;
                let Producto = (<HTMLInputElement>document.getElementById("despacho-producto")).value;
                let volCargado = (<HTMLInputElement>document.getElementById("volumen-cargado")).value;
                if (Producto != "") {
                    if (this._VolumenBruto != volCargado && volCargado != "") {
                        var payload = { IdTerminal: Terminal, IdProducto: Producto, VolumenCargado: volCargado };
                        this._httpService.Post<string>("/Despachos/ObtenerRecetaActivaPorProductoTerminal", payload, false)
                            .then((data) => {
                                if (data) {
                                    document.getElementById("lista-componentes").innerHTML = data;
                                    $(".select2").select2({ dropdownAutoWidth: true, width: '100%' });
                                    this.TableComponente = new ConfigureDataTable().ConfigureScrollXSinInfo("#data-table-componentes", [{ "width": "5%", "targets": 0 }]);
                                    this._MaskFormatsComponentes.ApplyMasks();
                                }
                                else
                                    M.toast({ html: data, classes: (false) ? "succes" : "error" });
                            }).catch((error) => {
                                M.toast({ html: "No fue posible cargar los componentes: " + error, classes: "error" });
                                console.log(error)
                            })
                    }
                }
                else {
                    M.toast({ html: "Por favor seleccionar un producto para recalcular los componentes", classes: "error" });
                }
                this._VolumenBruto = volCargado;
                return false; // prevent reload
            }
            catch (error) {
                M.toast({ html: "Algo salió mal y no fue posible consultar los componentes", classes: (false) ? "succes" : "error" });
            }
        }
    }

    //Extraer despacho de formulario
    private ExtraerDespacho(): Despacho {
        let despach = new Despacho();

        despach.Id_Despacho = this._formData.get("Id_Despacho")?.toString();
        despach.Placa_Cabezote = this._formData.get("Placa_Cabezote")?.toString();
        despach.Placa_Trailer = this._formData.get("Placa_Trailer")?.toString();
        despach.Volumen_Ordenado = this._formData.get("Volumen_Ordenado")?.toString();
        despach.Volumen_Cargado = this._formData.get("Volumen_Cargado")?.toString();
        despach.Cedula_Conductor = this._formData.get("Cedula_Conductor")?.toString();

        return despach;
    }

    //Reset de formulario
    private LimpiarFormulario() {
        this._formDespacho.reset();
        $('input').not("input[type= 'hidden']").val(null);
        $('.select2').val(null).trigger('change');
        M.updateTextFields();
        //$('#Despacho-observaciones').focus();
        //$('#Despacho-observaciones').blur();
    }

    //Cerrar control modal
    private CerrarModal() {
        if (this._crear)
            this._despachosDatePicker.destroy();

        this._habilitarConsultaComp = false;
        this._crear = false;
        this.TableComponente.destroy(false);
        this._modalInstance.close();
        this._formDespacho.parentNode.removeChild(this._formDespacho);
        this._DespachoIdActualizar = "";
    }

    //Formatear inputs
    private RegistrarFormatos() {
        //registrar mascaras 
        this.RegisterMasks(
            [
                MaskFormats.DateShortFormat(),
                //MaskFormats.NumericoSinSignos('#numero-orden'),
                MaskFormats.Alfanumerico('#placa-cabezote'),
                MaskFormats.Alfanumerico('#placa-trailer'),
                MaskFormats.DecimalFormatRang('#volumen-ordenado', 0, 1000000),
                MaskFormats.DecimalFormatRang('#volumen-cargado', 0, 1000000)
            ]);

        this._MaskFormatsComponentes.RegisterMasks([
            MaskFormats.DecimalFormatRang('#volumen_bruto_0', 0, 1000000), MaskFormats.DecimalFormatRang('#volumen_bruto_1', 0, 1000000),
            MaskFormats.DecimalFormatRang('#volumen_bruto_2', 0, 1000000), MaskFormats.DecimalFormatRang('#volumen_bruto_3', 0, 1000000),
            MaskFormats.DecimalFormatRang('#volumen_neto_0', 0, 1000000), MaskFormats.DecimalFormatRang('#volumen_neto_1', 0, 1000000),
            MaskFormats.DecimalFormatRang('#volumen_neto_2', 0, 1000000), MaskFormats.DecimalFormatRang('#volumen_neto_3', 0, 1000000),
            MaskFormats.DecimalFormatRang('#temperatura_0', 32, 150), MaskFormats.DecimalFormatRang('#temperatura_1', 32, 150),
            MaskFormats.DecimalFormatRang('#temperatura_2', 32, 150), MaskFormats.DecimalFormatRang('#temperatura_3', 32, 150),
            MaskFormats.DecimalFormatRang('#densidad_0', 10, 100), MaskFormats.DecimalFormatRang('#densidad_0', 10, 100),
            MaskFormats.DecimalFormatRang('#densidad_2', 10, 100), MaskFormats.DecimalFormatRang('#densidad_3', 10, 100),
        ])
    }

    private UpdateMaskValue() {
        this.MaskManager.UpdateValue([".formatos-fecha-corta"])
    }

    private async ConfigClosedDates() {
        let initialDate = dayjs(this._despachosFechaInicial);
        let currentDate = dayjs();
        let picker = document.querySelector<HTMLElement>(".datepicker-table");
        let selectYear = (document.querySelector(".select-year select.orig-select-year") as HTMLSelectElement);
        let selectMonth = (document.querySelector(".select-month select.orig-select-month") as HTMLSelectElement);
        let year = parseInt(selectYear.options[selectYear.selectedIndex].text);
        let month = selectMonth.selectedIndex;

        picker.classList.add("disable");
        let result = await this.GetCloseDates(new Date(year, month, 1));
        picker.classList.remove("disable");
        let pickerDays = document.querySelectorAll<HTMLElement>(".datepicker-table td .datepicker-day-button");

        pickerDays.forEach(
            function (currentValue, currentIndex, listObj) {
                //currentValue.parentElement.classList.remove("is-closed-date", "is-open-date");
                let day = parseInt(currentValue.dataset.day);
                if (result.some(d => dayjs(d).date() == day))
                    currentValue.parentElement.classList.add("is-closed-date");
                else {
                    let calendarDay = new Date(year, month, day);
                    if (!(currentDate.isSame(calendarDay, "day"))) {
                        if (currentDate.isAfter(calendarDay) && initialDate.isBefore(calendarDay))
                            currentValue.parentElement.classList.add("is-open-date");

                    }

                }
            });
    }

    private async GetCloseDates(fechaActual: Date) {
        return new Promise<Date[]>((resolve, reject) => {
            const httpService = new HttpFetchService();
            httpService.Post<Date[]>("/Despachos/ObtenerFechasCierreMes/", fechaActual, true)
                .then((data) => {
                    if (data) {
                        resolve(data);
                    }
                }).catch((error) => { reject("Error: " + error) });
        });

    }

    private GetInitialCloseDate() {
        const httpService = new HttpFetchService();
        httpService.Post<Date>("/Despachos/ObtenerFechaCierreInicial/", "", true)
            .then((data) => {
                if (data) {
                    this._despachosFechaInicial = data;
                }
            }).catch((error) => { });
    }
}
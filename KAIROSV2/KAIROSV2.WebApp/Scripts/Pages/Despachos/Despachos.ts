import { HttpFetchService, IMessageResponse, Despacho, FechasCorteDTO } from '../../Shared';
import { ConfirmModalMessage } from '../../Shared/Components/ConfirmModalMessage';
import { ConfigureDataTable } from '../../Shared/Components/ConfigureDataTable';
import { DespachosGestionPage } from './GestionDespachos';
import { JQValidations } from '../../Shared/Utils/JQValidations';
import { SelectorMenu } from '../../Shared/Utils/SelectorMenu';
import { IPage } from '../../Shared/Components/IPage';
import { MaskFormats, IMaskFormat } from '../../Shared/Utils/MaskFormats';
import { DatepickerComponent } from '../../Shared/Components/DatepickerComponent';
import IMask from "imask";
import { MaskFormatsManager } from '../../Shared/Utils';
import PerfectScrollbar from 'perfect-scrollbar';
import { format, parseISO, parseJSON } from 'date-fns';
import dayjs = require('dayjs');
import 'select2';
import 'select2/dist/css/select2.css';
import { Page } from '../../Core/Page';
import { id } from 'date-fns/esm/locale';
export class DespachosPage extends Page {

    private TableDetalle: DataTables.Api;
    private TableConsolidado: DataTables.Api;
    private modalInstance: M.Modal;
    private gestionDespachos: DespachosGestionPage;
    private _formDespacho: HTMLFormElement;
    private _formData: FormData;
    private _baseUrl = "/Despachos";
    private _httpService: HttpFetchService;
    private _arrayCompañias: Despacho[] = [];
    private _despachosDatePicker: M.Datepicker;
    private _despachosFechaInicial: Date;
    private _terminalSeleccionada: string;

    constructor() {
        super();
        this.FormattFields();
        this._httpService = new HttpFetchService();
        this.Init();
    }

    public Destroy() {
        super.Destroy();
        this.TableDetalle.destroy(false);
        this.TableConsolidado.destroy(false);
        this._despachosDatePicker?.destroy();
    }

    public Init() {
        this.InicializarDatePicker();
        this.SortExtention();
        this.InicializarButtons();
        this.InicializarControls();               
    }

    public SortExtention() {
        $.fn.dataTable.ext.order['dom-checkbox'] = function (settings, col) {
            return this.api().column(col, { order: 'index' }).nodes().map(function (td, i) {
                return $('input', td).prop('checked') ? '1' : '0';
            });
        };
    }

    private async InicializarControls() {
        SelectorMenu.SeleccionEnElMenu();
        this.TableDetalle = new ConfigureDataTable().ConfigureScrollX("#data-table-despacho-detalle", [{ "orderDataType": "dom-checkbox" }, null, null, null, null, null, null, null, null, null, null, null], [{ "width": "5%", "targets": 0 }], 'filtro_detalle');
        this.TableConsolidado = new ConfigureDataTable().ConfigureScrollX("#data-table-despacho-consolidado", null, [{ "width": "5%", "targets": 0 }], 'filtro_consolidado');
        $(".select2").select2({ dropdownAutoWidth: true, width: '100%' });
        
        
        M.Tabs.init(document.querySelectorAll('.tabs'));
        this.gestionDespachos = new DespachosGestionPage(document.getElementById("despacho-modal"));
        this.gestionDespachos.onDespachoCreado = () => this.LimpiarFormulario();
        this.gestionDespachos.onDespachoActualizado = (despacho) => this.ActualizarFilaDatatable(despacho);

        M.updateTextFields();
        this._formDespacho = document.querySelector("#despacho-form");
        this._formData = new FormData(this._formDespacho);
        $(this._formDespacho).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(this._formDespacho);

        this._formDespacho.onsubmit = (e) => {
            e.preventDefault();

            if ($(this._formDespacho).valid()) {
                this._formData = new FormData(this._formDespacho);
                this.MaskManager.SetUnmaskedFormValue(this._formData);
                this.BuscarDespachos();
            }
            return false; // prevent reload
        };

        
    }

    private async InicializarDatePicker() {

        this._despachosFechaInicial = await this.GetInitialCloseDate();
        console.log(this._despachosFechaInicial);
        this._despachosDatePicker = new DatepickerComponent().GetInstances2('#fecha-despacho',
            {
                onClose: () => { this.UpdateMaskValue() },
                onDraw: () => { this.ConfigClosedDates() },
                minDate: new Date(this._despachosFechaInicial),
                maxDate: new Date(),
                //defaultDate: new Date(),
                //setDefaultDate: true
            });

        this.RegistrarFormatos();
    }


    private InicializarButtons() {
        var kanba = document.getElementById("kanba");
        var despachoAdd = document.getElementById("despacho-add");
        var limpiarForm = document.getElementById("borrar-filtro");
        var tabDetalle = document.getElementById("tab_detalle");
        var tabConsolidado = document.getElementById("tab_consolidado");
        var thes = this;

        kanba.addEventListener('click', function (event) {
            if ((event.target as HTMLElement).classList.contains('despacho-detail')) {
                thes.BuscarDetalleDetalle(event.target['dataset'].iddespacho, event);
            }
            if ((event.target as HTMLElement).classList.contains('despacho-edit')) {
                thes.EditarDespacho(event.target['dataset'].iddespacho);
            }
            if ((event.target as HTMLElement).classList.contains('despacho-consolidated')) {
                thes.BuscarDetalleConsolidado(event.target['dataset'].idproducto, event);
            }

        });

        despachoAdd.addEventListener('click', event => this.CrearDespacho());
        limpiarForm.addEventListener('click', event => this.LimpiarFormulario());
        tabDetalle.addEventListener('click', event => this.RenderizarTablaDetalle());
        tabConsolidado.addEventListener('click', event => this.RenderizarTablaConsolidado());

        kanba.addEventListener('change', function (event) {
            if ((event.target as HTMLElement).classList.contains('despacho-estado')) {
                thes.CambioEstado(event.target['dataset'].iddespacho, event);
            }
        });

        $("#terminal-despacho").change(function () { thes.ConsultarCompañias(); });
        $("#compañia-despacho").change(function () { thes.VerificacionActivacionBtnCrear(); });
        $("#fecha-despacho").change(function () { thes.LimpiarTablas(); });
    }

    private RenderizarTablaDetalle() {
        setTimeout(() => { this.TableDetalle.draw(false); }, 600)
    }

    private RenderizarTablaConsolidado() {
        setTimeout(() => { this.TableConsolidado.draw(false); }, 600)
    }

    private VerificacionActivacionBtnCrear() {
        this.LimpiarTablas();
        var terminal = (<HTMLInputElement>document.getElementById("terminal-despacho")).value;
        var compañia = (<HTMLInputElement>document.getElementById("compañia-despacho")).value;
        var btnCrear = document.getElementById("despacho-add")

        if (terminal != "" && compañia != "" && compañia != "TODAS") {
            btnCrear.classList.remove('disabled');
            return
        }
        btnCrear.classList.add('disabled');
    }

    // Actualizar un despacho
    private ActualizarFilaDatatable(despacho: Despacho) {
        let row = $("tr#" + despacho.Id_Despacho);
        var despach = this.TableDetalle.row(row).data();
        despach[5] = despacho.Placa_Cabezote;
        despach[6] = despacho.Placa_Trailer;
        despach[7] = despacho.Cedula_Conductor;
        despach[9] = despacho.Volumen_Ordenado;
        despach[10] = despacho.Volumen_Cargado;
        this.TableDetalle.row(row).data(despach);
    }

    private BuscarDespachos() {
        try {
            var terminal = (<HTMLInputElement>document.getElementById("terminal-despacho")).value;
            var compañia = (<HTMLInputElement>document.getElementById("compañia-despacho")).value;
            var fecha = (<HTMLInputElement>document.getElementById("fecha-despacho")).value;

            if (terminal != "" && compañia != "" && fecha != "") {

                var payload = { terminal: terminal, compañia: compañia, fechaCorte: fecha };
                this.LimpiarTablas();

                this._httpService.Post<string>("/Despachos/BuscarDespachosDetallados", payload, false)
                    .then((data) => {
                        if (data) {
                            document.getElementById("lista-detalle").innerHTML = data;
                            this.TableDetalle = new ConfigureDataTable().ConfigureScrollX("#data-table-despacho-detalle", [{ "orderDataType": "dom-checkbox" }, null, null, null, null, null, null, null, null, null, null, null], [{ "width": "5%", "targets": 0 }], 'filtro_detalle');
                            M.toast({ html: "Búsqueda de detalles finalizada", classes: (true) ? "succes" : "error" });
                        }
                        else
                            M.toast({ html: data, classes: (false) ? "succes" : "error" });
                    }).catch((error) => {
                        M.toast({ html: "No es posible consultar los datos: " + error, classes: "error" });
                        console.log(error)
                    })

                this._httpService.Post<string>("/Despachos/BuscarDespachosConsolidados", payload, false)
                    .then((data) => {
                        if (data) {
                            document.getElementById("lista-consolidado").innerHTML = data;
                            this.TableConsolidado = new ConfigureDataTable().ConfigureScrollX("#data-table-despacho-consolidado", null, [{ "width": "5%", "targets": 0 }], 'filtro_consolidado');
                            //this.MaskManager.ApplyMasks();
                            M.toast({ html: "Búsqueda de consolidado finalizada", classes: (true) ? "succes" : "error" });
                        }
                        else
                            M.toast({ html: data, classes: (false) ? "succes" : "error" });
                    }).catch((error) => {
                        M.toast({ html: "No es posible consultar los datos: " + error, classes: "error" });
                        console.log(error)
                    })
            }
            else {
                M.toast({ html: "Algún campo del registro no fue ingresado", classes: (false) ? "succes" : "error" });
            }
            return false; // prevent reload
        }
        catch (error) {
            M.toast({ html: "Algo salió mal y no fue posible consultar los datos", classes: (false) ? "succes" : "error" });
        }
    }

    private async BuscarConsolidados() {
        try {
            var terminal = (<HTMLInputElement>document.getElementById("terminal-despacho")).value;
            var compañia = (<HTMLInputElement>document.getElementById("compañia-despacho")).value;
            var fecha = (<HTMLInputElement>document.getElementById("fecha-despacho")).value;
            var payload = { terminal: terminal, compañia: compañia, fechaCorte: fecha };
            this.TableConsolidado?.clear();
            this.TableConsolidado?.draw();

            if (terminal != "" && compañia != "" && fecha != "") {

                this._httpService.Post<string>("/Despachos/BuscarDespachosConsolidados", payload, false)
                    .then((data) => {
                        if (data) {
                            document.getElementById("lista-consolidado").innerHTML = data;
                            this.TableConsolidado = new ConfigureDataTable().ConfigureScrollX("#data-table-despacho-consolidado", null, [{ "width": "5%", "targets": 0 }], 'filtro_consolidado');
                            M.toast({ html: "Búsqueda de consolidado finalizada", classes: (true) ? "succes" : "error" });
                        }
                        else
                            M.toast({ html: data, classes: (false) ? "succes" : "error" });
                    }).catch((error) => {
                        M.toast({ html: "No es posible consultar los datos: " + error, classes: "error" });
                        console.log(error)
                    })
            }
            else {
                M.toast({ html: "Algún campo de la búsqueda fue modificado, por favor recargue la búsqueda", classes: (false) ? "succes" : "error" });
            }
            return false; // prevent reload
        }
        catch (error) {
            this.LimpiarFormulario();
            M.toast({ html: "Algo salió mal por favor realice nuevamente la consulta", classes: (false) ? "succes" : "error" });
        }
    }

    private CrearDespacho() {
        var terminal = (<HTMLInputElement>document.getElementById("terminal-despacho")).value;
        var compañia = (<HTMLInputElement>document.getElementById("compañia-despacho")).value;

        var payload = { Id_Terminal: terminal, Id_Compañia: compañia };

        this.gestionDespachos.NuevoDespacho(payload);
    }

    private EditarDespacho(iddespacho): void {
        this.gestionDespachos.DatosDespacho(iddespacho, true);
    }

    private async BuscarDetalleDetalle(iddespacho, event) {
        var tr = event.target.parentNode;
        var row = this.TableDetalle.row(tr);
        let self = this;
        if (row.child.isShown()) {
            // This row is already open - close it.
            row.child.hide();
            tr.classList.remove('shown');
        } else {
            // Open row.
            try {
                this._httpService.Post<string>("/Despachos/ObtenerDetalleDetalle", iddespacho, false)
                    .then((data) => {
                        let x = JSON.parse(data);
                        if (data) {
                            let tablaComponentes = '<tr style="background: lightgrey;"><td><table id="data-table-detalleDetalle" class="" style="width:35%; white-space:nowrap">' +
                                '<thead><tr>' +
                                '<th>Componente</th>' +
                                '<th>Volumen Bruto - Gal</th>' +
                                '<th>Volumen Neto - Gal</th>' +
                                '<th>Temperatura - °F</th>' +
                                '<th>Densidad - API</th>' +
                                '</tr></thead>' +
                                '<tbody>';

                            Object.values(x['Payload']).forEach(function (data) {
                                var tablaComten = '<tr>' +
                                    '<td>' + data['Componente'] + '</td>' +
                                    '<td>' + self.Formato(data['Volumen_Bruto']) + '</td>' +
                                    '<td>' + self.Formato(data['Volumen_Neto']) + '</td>' +
                                    '<td>' + data['Temperatura'] + '</td>' +
                                    '<td>' + data['Densidad'] + '</td></tr>';
                                tablaComponentes += tablaComten;
                            });
                            tablaComponentes += '</tbody></table></td></tr>';
                            row.child(tablaComponentes).show();
                            tr.classList.add('shown');
                        }
                    }).catch((error) => {
                        M.toast({ html: "No es posible consultar el detalle de detalle: " + error, classes: "error" });
                        console.log(error)
                    })
                return false; // prevent reload
            }
            catch (error) {
                M.toast({ html: "Algo salió mal y no fue posible consultar el detalle de detalle", classes: (false) ? "succes" : "error" });
            }
        }
    }

    private async BuscarDetalleConsolidado(idproducto, event) {
        var tr = event.target.parentNode;
        var row = this.TableConsolidado.row(tr);
        var terminal = (<HTMLInputElement>document.getElementById("terminal-despacho")).value;
        var compañia = (<HTMLInputElement>document.getElementById("compañia-despacho")).value;
        var fecha = (<HTMLInputElement>document.getElementById("fecha-despacho")).value;
        let self = this;

        var payload = { Terminal: terminal, Compañia: compañia, Fecha_Corte: fecha, IdProducto: idproducto };
        if (row.child.isShown()) {
            // This row is already open - close it.
            row.child.hide();
            tr.classList.remove('shown');
        } else {
            // Open row.
            try {
                this._httpService.Post<string>("/Despachos/ObtenerDetalleConsolidado", payload, false)
                    .then((data) => {
                        let x = JSON.parse(data);
                        if (data) {
                            let tablaComponentes = '<tr style="background: lightgrey;"><td><table id="data-table-detalleConsolidado" class="" style="width:50%; white-space:nowrap">' +
                                '<thead><tr>' +
                                '<th>Código Producto</th>' +
                                '<th>Componente</th>' +
                                '<th>Volumen Bruto - Gal</th>' +
                                '<th>Temperatura - °F</th>' +
                                '<th>Densidad - API</th>' +
                                '<th>Volumen Neto - Gal</th>' +
                                '<th>Factor</th>' +
                                '</tr></thead>' +
                                '<tbody>';
                            Object.values(x['Payload']).forEach(function (data) {
                                var x = 0;

                                var tablaComten = '<tr> ' +
                                    '<td>' + data['IdProducto'] + '</td>' +
                                    '<td>' + data['Producto'] + '</td>' +
                                    '<td>' + self.Formato(data['VolumenUnitarioBruto']) + '</td>' +
                                    '<td>' + data['TemperaturaPonderada'] + '</td>' +
                                    '<td>' + data['DensidadPonderada'] + '</td>' +
                                    '<td>' + self.Formato(data['VolumenUnitarioNeto']) + '</td>' +
                                    '<td>' + data['Factor'] + '</td></tr>';
                                tablaComponentes += tablaComten;
                            });
                            tablaComponentes += '</tbody></table></td></tr>';
                            row.child(tablaComponentes).show();
                            tr.classList.add('shown');
                        }
                    }).catch((error) => {
                        M.toast({ html: "No es posible consultar el detalle de consolidado: " + error, classes: "error" });
                        console.log(error)
                    })
                return false; // prevent reload
            }
            catch (error) {
                M.toast({ html: "Algo salió mal y no fue posible consultar el detalle de consolidado", classes: (false) ? "succes" : "error" });
            }
        }
    }

    private Formato(valor) {
        let cif = 3, dec = 2;
        // tomamos el valor que tiene el input
        let inputNum = valor;
        // Lo convertimos en texto
        inputNum = inputNum.toString()
        // separamos en un array los valores antes y después del punto
        inputNum = inputNum.split('.')
        // evaluamos si existen decimales
        if (!inputNum[1]) {
            inputNum[1] = '00'
        }

        let separados
        // se calcula la longitud de la cadena
        if (inputNum[0].length > cif) {
            let uno = inputNum[0].length % cif
            if (uno === 0) {
                separados = []
            } else {
                separados = [inputNum[0].substring(0, uno)]
            }
            let posiciones = Math.floor((inputNum[0].length / cif));
            for (let i = 0; i < posiciones; i++) {
                let pos = ((i * cif) + uno)
                separados.push(inputNum[0].substring(pos, (pos + 3)))
            }
        } else {
            separados = [inputNum[0]]
        }
        let res = separados.join(',') + '.' + inputNum[1];
        return res;
    };

    private async CambioEstado(idDespacho, event) {
        const confirm = new ConfirmModalMessage("Cambio de Estado", "¿Está seguro de cambiar el estado?", "Aceptar", "Cancelar");
        let shouldDelete = await confirm.Confirm();

        if (!shouldDelete) {
            if (event.target.checked) {
                event.target.checked = false;
                return;
            } else {
                event.target.checked = true;
                return;
            }
        };
        let cambio = event.target.checked ? "True" : "False";

        var payload = { Despacho: idDespacho, Lectura: event.target.checked };
        let lbl = (event.target.id + '').split('-');
        let index = lbl[2];
        //document.getElementById('label-estado-' + index).innerHTML = cambio;
        let row = $("tr#" + idDespacho);
        let currentRow = this.TableDetalle.row(row);
        var cond = this.TableDetalle.row(row).data();
        this._httpService.Post<string>("/Despachos/ActualizarEstadoDespacho", payload, false)
            .then((data) => {
                let resultado = JSON.parse(data);
                if (resultado['Result']) {
                    this.MaskManager.ApplyMasks();
                    cond[0] = this.HtmlCambioEstado(index, cambio, idDespacho);
                    this.TableDetalle.row(row).data(cond);
                    this.TableDetalle.row(row).invalidate();
                    //this.TableDetalle = new ConfigureDataTable().ConfigureScrollX("#data-table-despacho-detalle", [{ "width": "5%", "targets": 0 }], 'filtro_detalle');
                    M.toast({ html: "Se cambia el estado correctamente", classes: (true) ? "succes" : "error" });

                    this.BuscarConsolidados();
                    //this.RenderizarTablaDetalle();
                }
                else {
                    M.toast({ html: 'No fue posible actualizar el estado', classes: "error" });
                    if (event.target.checked) {
                        event.target.checked = false;
                    }
                }
            }).catch((error) => {
                M.toast({ html: "No es posible cambiar el estado: " + error, classes: "error" });
                if (event.target.checked) {
                    event.target.checked = false;
                }
                else {
                    event.target.checked = true;
                }
            })
    }

    private HtmlCambioEstado(index, cambio, idDespacho) {
        let check;
        if (cambio == 'True') {
            check = `<input id="switch-estado-${index}" class="despacho-estado" type="checkbox" checked="checked" data-idDespacho="${idDespacho}">`;
        } else {
            check = `<input id="switch-estado-${index}" class="despacho-estado" type="checkbox" data-idDespacho="${idDespacho}">`;
        }
        let html = '<div class="switch">' +
            '<label>' +
            `<label id="label-estado-${index}" data-order="${cambio}" style="display:none" data-sort="${cambio}" class="est">${cambio}</label>` +
            check +
            '<span class="lever"></span>' +
            '</label>' +
            '</div>';
        return html;
    }

    private async ConsultarCompañias() {
        let idTerminal = (<HTMLInputElement>document.getElementById("terminal-despacho")).value;
        this._terminalSeleccionada = idTerminal;
        if (idTerminal != "") {
            (this._despachosDatePicker.el as HTMLInputElement).removeAttribute("disabled");
        }
        this.VerificacionActivacionBtnCrear();
        $('#compañia-despacho').val(null).trigger('change');
        this._httpService.Post<Despacho[]>(this._baseUrl + '/ConsultarCompañias', idTerminal, true)
            .then((data) => {
                if (data) {
                    this.LimpiarOpciones();
                    this._arrayCompañias = data;
                    this.AgregarCompañias();
                }
            }).catch((error) => {
                M.toast({ html: error, classes: "error" });
            });
    }

    private LimpiarOpciones() {
        let selectClase = document.getElementById('compañia-despacho');
        let numOption = $('#compañia-despacho option').length;
        for (let j = numOption; j > 1; j--) {
            selectClase[j - 1].remove();
        }
    }

    private AgregarCompañias() {
        let selectClase = document.getElementById('compañia-despacho');
        for (let j = 0; j <= this._arrayCompañias.length; j++) {
            let option = document.createElement('option');
            if (j == 0) {
                option.value = 'TODAS';
                option.text = 'TODAS';
            } else {
                option.value = this._arrayCompañias[j - 1].Value;
                option.text = this._arrayCompañias[j - 1].Text;
            }
            selectClase.append(option);
        }
        M.FormSelect.init(document.querySelectorAll("select"));
    }

    //Reset de formulario
    private LimpiarFormulario() {
        this._formDespacho.reset();
        this.LimpiarOpciones();
        this.LimpiarTablas();
        var btnCrear = document.getElementById("despacho-add")
        btnCrear.classList.add('disabled');
        $('input').not("input[type= 'hidden']").val(null);
        $('.select2').val(null).trigger('change');
        M.updateTextFields();
        (this._despachosDatePicker.el as HTMLInputElement).setAttribute("disabled", "disabled");
    }

    //Limpia las tablas detalle y consolidado
    private LimpiarTablas() {
        this.TableDetalle?.clear();
        this.TableDetalle?.draw();
        this.TableConsolidado.clear();
        this.TableConsolidado.draw();
    }

    //Formatear inputs
    private RegistrarFormatos() {
        //registrar mascaras 
        this.RegisterMasks(
            [
                MaskFormats.DateShortFormat(),
            ]);
    }

    //Pone la mascara de fecha corta
    private UpdateMaskValue() {
        this.MaskManager.UpdateValue([".formato-fecha-corta"])
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
        let result = await this.GetCloseDates(new Date(year, month, 1), this._terminalSeleccionada);
        picker.classList.remove("disable");
        let pickerDays = document.querySelectorAll<HTMLElement>(".datepicker-table td .datepicker-day-button");

        pickerDays.forEach(
            function (currentValue, currentIndex, listObj) {
                //currentValue.parentElement.classList.remove("is-closed-date", "is-open-date");
                let day = parseInt(currentValue.dataset.day);
                let cutDate = result.find(d => dayjs(d.Fecha).date() == day);
                if (cutDate)
                    currentValue.parentElement.classList.add(cutDate.Cierre ? "is-closed-date" : "is-open-date");
                else {
                    let calendarDay = new Date(year, month, day);
                    if (currentDate.isSame(calendarDay, "day"))
                        currentValue.parentElement.classList.add("is-current-date");
                }
            });
    }

    private async GetCloseDates(fechaActual: Date, idTerminal: string) {
        var payload = { IdEntidad: idTerminal, Fecha: fechaActual };
        return new Promise<FechasCorteDTO[]>((resolve, reject) => {
            const httpService = new HttpFetchService();
            httpService.Post<FechasCorteDTO[]>("/Despachos/ObtenerFechasCierreMes/", payload, true)
                .then((data) => {
                    if (data) {
                        resolve(data);
                    }
                }).catch((error) => { reject("Error: " + error) });
        });

    }

    private async GetInitialCloseDate() {
        return new Promise<Date>((resolver, reject) => {
            const httpService = new HttpFetchService();
            httpService.Post<Date>("/Despachos/ObtenerFechaCierreInicial/", "", true)
                .then((data) => {
                    if (data) {
                        resolver(data);
                    }
                }).catch((error) => { reject("Error: " + error) });
        })

    }
   
    private FormattFields() {
        this.RegisterMasks(
            [
                MaskFormats.DecimalFormatRang(".volumen", 0, 1000000)
            ]
        )
        //formData.forEach((value, key) => { console.log(key + ": " + value) });
    }

}

var despachosPage = new DespachosPage();


// Called after every non-initial page load
document.addEventListener('turbolinks:render', (e: any) => {
    if (document.URL.indexOf('/Despachos/Index') != -1) {
        if (DespachosPage) {
            despachosPage.Init();
        }
        else {
            despachosPage = new DespachosPage();
        }
    }
    else
        despachosPage?.Destroy();
});
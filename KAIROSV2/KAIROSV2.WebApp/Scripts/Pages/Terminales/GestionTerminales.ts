
import { ConfigureDataTable } from '../../Shared/Components/ConfigureDataTable';
import { HttpFetchService, IMessageResponse } from '../../Shared';
import { ConfirmModalMessage } from '../../Shared/Components/ConfirmModalMessage';
import { Terminal } from '../../Shared/Models/Terminal';

import 'jquery-qubit';
import 'jquery-bonsai';
import 'jquery-bonsai/jquery.bonsai.css';
import 'select2';
import 'select2/dist/css/select2.css';
import { DatepickerComponent } from '../../Shared/Components/DatepickerComponent';
import { MaskFormats, IMaskFormat } from '../../Shared/Utils/MaskFormats';
import { Page } from '../../Core/Page';
import dayjs = require('dayjs');

export class TerminalesGestionPage extends Page {

    //PROPIEDADES
    public onTerminalCreado?: (terminal: Terminal) => void;
    public onTerminalActualizado?: (terminal: Terminal) => void;

    //CAMPOS
    private _baseUrl = "/Terminales";
    private _httpService: HttpFetchService;
    private _modalInstance: M.Modal;
    private _modalBase: HTMLElement;
    private _formTerminal: HTMLFormElement;
    private _formCompania: HTMLFormElement;
    private _formReceta: HTMLFormElement;
    private _formData: FormData;
    private _terminalIdActualizar: string;
    private _productoIdActualizar: string;
    private _estadoProducto: EventTarget;
    private _Table1: DataTables.Api;
    private _Table2: DataTables.Api;
    private _TableProductos: DataTables.Api;
    private _idCompDes;
    private _idCompHab;
    private _creado: boolean = false;
    private _opcion = { twelveHour: false };
    private _validador = [];
    private _index = 0;


    //CONSTRUCTOR
    constructor(modalBase: HTMLElement) {
        super();
        this._httpService = new HttpFetchService();
        this._modalBase = modalBase;
        this.InicializarModal();
        this.RegistrarFormatos();
    }


    //METODOS

    //Creación de nueva terminal

    public NuevaTerminal() {
        this._httpService.Post<string>(this._baseUrl + "/NuevaTerminal", null, false)
            .then((data) => {
                document.getElementById("teminales").innerHTML = data;
                this.InicilizarFormTerminal(true);
                this.InicializarButtons();
            });
    }

    //Guardar Terminal
    private GuardarTerminal(): void {        
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/CrearSoloTerminal", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    $('#fieldset-compania').prop('disabled', false);
                    $('#fieldset-producto').prop('disabled', false);
                    if (this.onTerminalCreado)
                        this.onTerminalCreado(this.ExtraerTerminal());
                    let button = document.getElementsByTagName('button');
                    button[0].innerHTML = 'Modificar <i class="material-icons left"> save </i>';
                    let disableId = document.getElementById('terminal-id');
                    disableId.setAttribute('readonly', 'true');
                    this._creado = true;
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

            })
            .catch((err) => console.log(err));
    }

    //Metodo para guardar la asignaicon de las vigencia de las recetas a un producto
    private AsignarVigenciaProducto(): void {
        let vigenciaEstado = this.validarVigencia();
        if (!vigenciaEstado['estado']) {
            M.toast({ html: vigenciaEstado['mensaje'], classes: "error" });
            return;
        }
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/CrearAsignacionProducto", this._formData, token)
            .then((data) => {
                console.log(data);
                if (data.Result) {
                    //this.ActualizarFilaDatatableProducto(this.ExtraerProducto());
                    this.CerrarModalRecetas();
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

            })
            .catch((err) => console.log(err));
    }

    //Actualizar Producto
    private ModificarAsignarVigencia(): void {
        let vigenciaEstado = this.validarVigencia();
        if (!vigenciaEstado['estado']) {
            M.toast({ html: vigenciaEstado['mensaje'], classes: "error" });
            return;
        }
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._formData.set("IdProducto", this._productoIdActualizar);
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/ActualizarAsignacionProducto", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    //this.ActualizarFilaDatatableProducto(this.ExtraerProducto());
                    //this.LimpiarFormulario();
                    this.CerrarModalRecetas();
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });
            })
            .catch((err) => console.log(err));
    }


    //Edicion de terminal
    public DatosTerminal(idTerminal: string, lectura: boolean) {

        var payload = { terminal: idTerminal, lectura: lectura };
        this._terminalIdActualizar = idTerminal;

        console.log('Terminal Id 1: ', this._terminalIdActualizar)

        this._httpService.Post<string>(this._baseUrl + "/EditarTerminal", payload, false)
            .then((data) => {
                if (data) {
                    document.getElementById("teminales").innerHTML = data;
                    this.InicilizarFormTerminal(false);
                    this.InicializarButtons();
                }
                else
                    M.toast({ html: data, classes: (false) ? "succes" : "error" });
            }).catch((error) => {
                M.toast({ html: "No es posible abrir la ventana correctamente: " + error, classes: "error" });
                console.log(error)
            })
    }

    //Actualizar terminal
    private ActualizarTerminal(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._formData.set("IdTerminal", this._terminalIdActualizar);
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/ActualizarSoloTerminal", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    if (this.onTerminalActualizado)
                        this.onTerminalActualizado(this.ExtraerTerminal());
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });
            })
            .catch((err) => console.log(err));
    }

    //Consulta Id de tablas
    private ConsultaId(selector: boolean) {

        if (selector) {
            this.ModalHabilitarCompania(this._idCompHab);
        } else {
            this.ModalDeshabilitarCompania(this._idCompDes)
        }

    }

    //Abre modal para habiliatar compañia
    private ModalHabilitarCompania(idCompania: string) {

        if (idCompania) {
            var Terminal = this.ExtraerTerminal().IdTerminal;
            var payload = { Compañia: idCompania, Terminal: Terminal };
            this._httpService.Post<string>(this._baseUrl + "/ModalHabilitarCompania", payload, false)
                .then((data) => {
                    this._modalBase.innerHTML = data;
                    this.InicilizarFormCompania();
                });

            this._modalInstance.open();

        } else {
            M.toast({ html: "No se ha seleccionado una compañia", classes: "error" });
        }
    }
    // Jeffer
    // Modal para asignar productos a las teminales
    private ModalAsignarProductos(element: HTMLElement) {
        let productoId = element.dataset.productoid;
        var payload = { IdProducto: productoId, IdTerminal: this._terminalIdActualizar };
        console.log('Terminal Id 1: ', this._terminalIdActualizar);
        this._httpService.Post<string>(this._baseUrl + "/AsignarProducto", payload, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormRecetas(true);
            });

        this._modalInstance.open();
    }

    //Habilita compañia
    private HabilitarCompania(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/AsignarCompañiaATerminal", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    this.RecargarVistaListaCompania();
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

            })
            .catch((err) => console.log(err));
    }

    //Abre modal para inhabiliatar compañia
    private async ModalDeshabilitarCompania(idCompania: string) {
        if (idCompania) {
            const confirm = new ConfirmModalMessage("Deshabilitar Compañia", "¿Desea deshabilitar la compañia " + idCompania + "?", "Aceptar", "Cancelar");
            const shouldDelete = await confirm.Confirm();
            if (shouldDelete) {
                var terminal = this.ExtraerTerminal();
                var payload = { Compañia: idCompania, Terminal: terminal.IdTerminal };
                const httpService = new HttpFetchService();
                httpService.Post<IMessageResponse>(this._baseUrl + "/EliminarCompañiaDeTerminal", payload)
                    .then((data) => {
                        if (data.Result) {
                            this.RecargarVistaListaCompania();
                        }
                        M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });



                    }).catch((error) => {
                        M.toast({ html: error, classes: "error" });
                    });
            }
        } else {
            M.toast({ html: "No se ha seleccionado una compañía", classes: "error" });
        }

    }

    //Recarga a vista de _ListaCompania
    public RecargarVistaListaCompania() {
        var terminal = this.ExtraerTerminal();
        var payload = { Terminal: terminal.IdTerminal };
        this._httpService.Post<string>(this._baseUrl + "/RecargarVistaListaCompania", payload, false)
            .then((data) => {
                document.getElementById("compania").innerHTML = data;
                this.InicilizarFormTerminal(false);
                this.InicializarButtons();
            });
    }

    //Inicializar control modal
    private InicializarModal() {

        this._modalInstance = M.Modal.init(this._modalBase, {
            dismissible: false,
            opacity: .5,
            inDuration: 300,
            outDuration: 200,
            startingTop: '6%',
            endingTop: '5%'
        });

    }

    //Inicializar controles de formulario terminal
    private InicilizarFormTerminal(modoCreacion: boolean): void {
        if (!modoCreacion) {
            $('#fieldset-producto').prop('disabled', false);
        }
        this._Table1 = new ConfigureDataTable().Configure("#companias-habilitadas", [{ "width": "5%", "targets": 0 }]);
        this._Table2 = new ConfigureDataTable().Configure("#companias-inhabilitadas", [{ "width": "5%", "targets": 0 }]);
        this._TableProductos = new ConfigureDataTable().Configure("#data-table-producto");
        $("#terminal-estado").select2({ dropdownAutoWidth: true, width: '100%' });
        $("#terminal-area").select2({ dropdownAutoWidth: true, width: '100%' });
        M.Tabs.init(document.querySelectorAll('.tabs'));

        M.updateTextFields();
        this._formTerminal = document.querySelector("#terminal-form");
        this._formData = new FormData(this._formTerminal);
        $(this._formTerminal).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(this._formTerminal);
        document.getElementById("companias-habilitar").addEventListener('click', (ev) => this.ConsultaId(true));
        document.getElementById("companias-deshabilitar").addEventListener('click', (ev) => this.ConsultaId(false));

        this._formTerminal.onsubmit = (e) => {
            e.preventDefault();

            if ($(this._formTerminal).valid()) {
                this._formData = new FormData(this._formTerminal);

                if (modoCreacion && this._creado == false) {
                    this.GuardarTerminal();

                } else {
                    this.ActualizarTerminal();
                }
            }

            return false; // prevent reload
        };
    }

    //Jeffer
    //Inicializar controles de formulario producto y recetas
    private InicilizarFormRecetas(modoCreacion: boolean): void {
        M.Collapsible.init(document.querySelectorAll(".collapsible"));
        var DatePickers = new DatepickerComponent().GetInstances('.datepicker', undefined, () => { this.UpdateMaskValue() });
        M.Timepicker.init(document.querySelectorAll(".timepicker"), this._opcion);
        M.updateTextFields();

        console.log('Terminal Id 3: ', this._terminalIdActualizar)
        // Jeffer
        var recetas = document.getElementById("recetas-container");
        recetas.on('click', '.receta-action', event => {
            if (event.target.matches(".receta-check")) {
                if (event.target['checked'])
                    this.ActivarVigencia(event.target as HTMLElement)
                else
                    this.DesActivarVigencia(event.target as HTMLElement);
            } else if (event.target.matches(".vigencia-add")) {
                this.AgregarVigencia(event.target as HTMLElement);

            } else if (event.target.matches(".vigencia-delete")) {
                this.validarVigencia();
            }

        });

        this._formReceta = document.querySelector("#receta-form");
        this._formData = new FormData(this._formReceta);
        $(this._formReceta).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(this._formReceta);

        this._formReceta.onsubmit = (e) => {
            e.preventDefault();

            if ($(this._formReceta).valid()) {
                this._formData = new FormData(this._formReceta);
                if (modoCreacion) {
                    this.AsignarVigenciaProducto();
                } else {
                    this.ModificarAsignarVigencia();
                }
            }

            return false; // prevent reload
        };

        document.getElementById("receta-modal-cancel").addEventListener('click', (ev) => this.CerrarModalRecetas(this._estadoProducto));
    }

    //Inicializar controles de formulario compañia
    private InicilizarFormCompania(): void {
        $("#compañia-agrupadora").select2({ dropdownAutoWidth: true, width: '100%' });
        M.updateTextFields();
        this._formCompania = document.querySelector("#compania-form");
        this._formData = new FormData(this._formCompania);
        $(this._formCompania).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(this._formCompania);
        document.getElementById("compania-modal-cancel").addEventListener('click', (ev) => this.CerrarModal());

        this._formCompania.onsubmit = (e) => {
            e.preventDefault();

            if ($(this._formCompania).valid()) {
                this._formData = new FormData(this._formCompania);
                this.HabilitarCompania();
            }

            return false; // prevent reload
        };
    }


    private OrdernarFechas() {
        this._validador.sort(function (o1, o2) {
            if (new Date(o1.fi).getTime() > new Date(o2.fi).getTime()) {
                //comparación lexicogŕafica
                return 1;
            } else if (new Date(o1.fi).getTime() < new Date(o2.fi).getTime()) {
                return -1;
            }
            return 0;
        });
    }

    // Este es el validador pendiente cambiar nombre
    private validarVigencia() {
        this._validador = [];
        let recetas = document.querySelectorAll('.receta');
        let self = this;
        recetas.forEach(receta => {
            let re = (receta as HTMLElement).dataset.recetaid;
            let tabla = '#table-vigencia-' + re + ' tbody';
            $(tabla).find('tr').each(function () {

                let tr = $(this),
                    fechaInicial = tr.find('input.fechaIni').val(),
                    horaInicial = tr.find('input.horaIni').val(),
                    fechaFinal = tr.find("input.fechaFin").val(),
                    horaFinal = tr.find('input.horaFin').val();

                fechaInicial = self.FormatoFecha(fechaInicial);
                fechaFinal = self.FormatoFecha(fechaFinal);

                let fechaInicialTotal = (fechaInicial + ' ' + horaInicial).trim();
                let fechaFinalTotal = (fechaFinal + ' ' + horaFinal).trim();

                let switchId = tr.find('input.activo').attr('id');

                let fechaObjet = { fi: fechaInicialTotal, ff: fechaFinalTotal, sw: switchId }

                self._validador.push(fechaObjet);


                let sw = document.getElementById(switchId);
                sw.removeAttribute('checked');

            });
        });

        this.OrdernarFechas();
        return this.validarCondiones();
    }

    private validarCondiones() {
        // Diferencia maxima permitida de 1 minuto
        let difMax = 0.0006944444444444445;

        // Diferencia maxima para validar que la fecha inicial sea mayor a la actual, de 2 minutos
        let timeLapse = -0.001388888888888889;
        // Contador que valida si hay mas de una vigencia sin fecha final
        let contador = 0;
        // Variable que indica la posicion de en el array de la vigencia sin fecha final
        let banderaSinVigencia = 0;
        // Variable que indica la posicion de en el array de la vigencia que debe estar activa
        let banderaActivo = 0;
        let tam = this._validador.length;
        let fa = this.fechaActual();

        let switchActivo;

        for (var i = 0; i < tam; i++) {
            if (this._validador[i].fi.length < 6) {
                return { estado: false, mensaje: 'Vigencia sin fecha inicial' };
            }
            if (this._validador[i].ff == "" && this._validador[i].fi != "") {
                contador++;
                banderaSinVigencia = i;
            }
        }
        //if (this.Comparar(fa, new Date(this._validador[0].fi)) < timeLapse) {
        //    console.log('La primer fecha inical es menor a la fecha actual por mas de 2 minutos');
        //    return { estado: false, mensaje: 'La primer fecha inical es menor a la fecha actual por mas de 2 minutos' };
        //}
        if (contador == 0)
            return { estado: false, mensaje: 'Debe haber por lo menos una vigencia sin fecha final' };
        else if (contador > 1)
            return { estado: false, mensaje: 'Hay mas o de una vigencia sin final' };        

        for (var j = 0; j < tam; j++) {
            if (j == banderaSinVigencia) {
                continue;
            }
            if (new Date(this._validador[j].fi).getTime() > new Date(this._validador[j].ff).getTime()) {
                return { estado: false, mensaje: 'Fecha final debe ser mayor a fecha inicial' };
            }
        }

        for (var k = 0; k < tam; k++) {
            if (k + 1 < tam) {
                if (new Date(this._validador[k].ff).getTime() <= new Date(this._validador[k + 1].fi).getTime()) {
                    if (this.Comparar(this._validador[k].ff, this._validador[k + 1].fi) < 0) {
                        return { estado: false, mensaje: 'Hay un hueco entre las fechas por menor' };;
                    } else if (this.Comparar(this._validador[k].ff, this._validador[k + 1].fi) > difMax) {
                        return { estado: false, mensaje: `El lapso de tiempo entre las fechas ${this._validador[k].ff} y ${this._validador[k + 1].fi} es mayor a 1 minuto` };
                    }
                    continue;
                }
                if (new Date(this._validador[k].fi).getTime() <= new Date(this._validador[k + 1].fi).getTime() || new Date(this._validador[k].ff).getTime() > new Date(this._validador[k + 1].fi).getTime()) {
                    return { estado: false, mensaje: 'Hay fechas cruzadas' };;
                }
            }
            if (k == banderaSinVigencia) {
                console.log('Secta final');
            }
        }

        for (var l = 0; l < tam; l++) {
            if (new Date(this._validador[l].fi).getTime() <= fa && new Date(this._validador[l].ff).getTime() > fa) {
                banderaActivo = l;
            }
        }

        let resultado = { activo: this._validador[banderaActivo], sinVigencia: this._validador[banderaSinVigencia] }

        console.log('Resultado', resultado);

        if (resultado.activo.sw == resultado.sinVigencia.sw) {
            switchActivo = document.getElementById(resultado.activo.sw);
            switchActivo.setAttribute('checked', 'checked');
        } else {
            switchActivo = document.getElementById(resultado.activo.sw);
            switchActivo.setAttribute('checked', 'checked');
        }
        return { estado: true, mensaje: 'Bien echo' };;
    }


    private Comparar(ff, fi) {
        let resta = new Date(fi).getTime() - new Date(ff).getTime();
        //resta = Math.round(resta / (1000 * 60 * 60 * 24));
        resta = resta / (1000 * 60 * 60 * 24);
        return resta;
    }

    /* 
         FI 2021/04/28
         HI 2021/04/28
     */
    private fechaActual() {
        let date: Date = new Date();

        let dia = date.getDate()
        let mes = date.getMonth() + 1 + '';
        let year = date.getFullYear()
        let hora = date.getHours();
        let minuto = date.getMinutes();

        let actual = `${dia}/${mes}/${year} ${hora}:${minuto}:00`;
        let ac = new Date(actual).getTime();
        return ac;
    }


    private InicializarButtons() {
        delete this._idCompDes;
        delete this._idCompHab;

        let tableProductos = document.getElementById("data-table-producto");
        let checksProductos = document.getElementsByClassName('producto-asignar');

        let checks = document.getElementsByClassName('chb');
        let checksInh = document.getElementsByClassName('chbi');
        let tableHab = document.getElementById("companias-habilitadas");
        let tableHabDesCheck = document.getElementById("companias-habilitadas_paginate");
        let tableInh = document.getElementById("companias-inhabilitadas");
        let tableInhDesCheck = document.getElementById("companias-inhabilitadas_paginate");
        for (let i = 0; i < checks.length; i++) {
            checks[i]['checked'] = false;
        }
        for (let i = 0; i < checksInh.length; i++) {
            checksInh[i]['checked'] = false;
        }

        tableHabDesCheck.on('click', '.paginate_button', event => {
            for (let i = 0; i < checks.length; i++) {
                checks[i]['checked'] = false;
            }
            delete this._idCompDes;
        });
        tableInhDesCheck.on('click', '.paginate_button', event => {
            for (let i = 0; i < checksInh.length; i++) {
                checksInh[i]['checked'] = false;
            }
            delete this._idCompHab;
        });
        tableHab.addEventListener('change', event => {
            let id = event.target as HTMLElement;
            this._idCompDes = id.dataset.compid;
            for (let i = 0; i < checks.length; i++) {
                if (checks[i]['checked'] == false) {
                    if (checks[i]['dataset'].compid == this._idCompDes) {
                        checks[i]['checked'] = true;
                    }
                }
                if (checks[i]['checked'] == true) {
                    if (checks[i]['dataset'].compid !== this._idCompDes) {
                        checks[i]['checked'] = false;
                    }
                }
            }
        });
        tableInh.addEventListener('change', event => {
            let id = event.target as HTMLElement;
            this._idCompHab = id.dataset.compid;
            for (let i = 0; i < checksInh.length; i++) {
                if (checksInh[i]['checked'] == false) {
                    if (checksInh[i]['dataset'].compid == this._idCompHab) {
                        checksInh[i]['checked'] = true;
                    }
                }
                if (checksInh[i]['checked'] == true) {
                    if (checksInh[i]['dataset'].compid !== this._idCompHab) {
                        checksInh[i]['checked'] = false;
                    }
                }
            }
        });

        // Jeffer 

        tableProductos.on('click', '.producto-action', event => {
            if (event.target.matches(".producto-asignar")) {
                if (!event.target['checked']) {
                    console.log(event.target);
                    this.DesAsignar(event.target, event.target.dataset.productoid);

                } else {
                    this._productoIdActualizar = event.target.dataset.productoid;
                    this.ModalAsignarProductos(event.target as HTMLElement);
                    this._estadoProducto = event.target;
                }

            } else if (event.target.matches(".producto-detail")) {
                this.DatosProductoVigencia(event.target.dataset.productoid, true);

            } else if (event.target.matches(".producto-edit")) {
                console.log("jeffer");
                console.log((event.target as HTMLElement).closest("tr").querySelector<HTMLInputElement>("input[type='checkbox']").checked);
                if ((event.target as HTMLElement).closest("tr").querySelector<HTMLInputElement>("input[type='checkbox']").checked) {
                    this.DatosProductoVigencia(event.target.dataset.productoid, false);
                } else {
                    M.toast({ html: "No se puede editar un producto sin asignación", classes: "error" });
                }
            }

        });
    }
    // Jeffer
    // ABRIR MODAL PARA EDITAR ASIGNACION PRODUCTO
    private DatosProductoVigencia(idProducto: string, lectura: boolean) {
        // Falta ID Terminal to do
        var payload = { IdProducto: idProducto, IdTerminal: this._terminalIdActualizar, Lectura: lectura };
        console.log("editando");
        console.log(payload);
        this._productoIdActualizar = idProducto;
        this._httpService.Post<string>(this._baseUrl + "/DatosProductoTerminal", payload, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormRecetas(false);
            });

        this._modalInstance.open();
    }

    // Jeffer
    private async DesAsignar(check, idProducto) {

        let shouldDelete = false;
        const confirm = new ConfirmModalMessage("Cambio de clase", "Esta acción borrara todas las recetas", "Aceptar", "Cancelar");
        shouldDelete = await confirm.Confirm();
        if (shouldDelete) {
            this.DesAsignarProducto(check, idProducto);
        } else {
            check.checked = true;
        }
    }

    // Jeffer, este es el metodo que llama al controlador
    private DesAsignarProducto(checkElement, idProducto) {
        // Falta id terminal
        var payload = { IdProducto: idProducto, IdTerminal: this._terminalIdActualizar };
        this._httpService.Post<IMessageResponse>(this._baseUrl + "/DesasignarProducto", payload, true)
            .then((data) => {
                if (data.Result) {
                    checkElement.checked = false;
                    M.toast({ html: "Se desasigno correctamente el producto", classes: "succes" });
                } else {
                    M.toast({ html: "No se pudo desasignar", classes: "error" });
                }
            });
    }


    // Borra una vigencia
    private DesActivarVigencia(element: HTMLElement): void {
        var padre = document.getElementById("vigencia-" + element.dataset.recetaid);
        var card = document.getElementById("responsible-table");
        padre.removeChild(card);
    }

    public Contar() {
        let index = document.getElementsByClassName('vigencia');
        this._index = index.length;
    }

    private ActivarVigencia(element: HTMLElement): void {
        this.Contar();
        let idReceta = element.dataset.recetaName;
        let recetaId = element.dataset.recetaid;
        let container = document.getElementById("vigencia-" + recetaId);
        var card = document.createElement("div");
        card.classList.add('card');
        card.classList.add('card-default');
        card.classList.add('scrollspy');
        card.setAttribute('id', 'responsible-table');
        card.innerHTML = '<div class="card-content vigencia">' +
            '<div class="row">' +
            '<div class="col s6">' +
            '<p class="mb-2">' +
            'Vigencia' +
            '</p>' +
            '</div>' +
            '<div class="col s6" style="text-align: end">' +
            '<a class="btn-floating waves-effect waves-light blue accent-2" style="text-align: center; margin-top: 10px; height: 30px; width: 30px;">' +
            `<i class="material-icons receta-action vigencia-add" data-vigenciaid="${recetaId}" data-receta-name="${recetaId}" style="font-size: 20px; display: flex; margin: -5px 0px 0px 5px"> add </i>` +
            '</a>' +
            '</div>' +
            '</div>' +
            '<div class="row">' +
            '<div class="col s12">' +
            `<table class="Highlight" id="table-vigencia-${recetaId}">` +
            '<thead>' +
            '<tr>' +
            '<th><b>Fecha Inicio </b></th> ' +
            '<th><b>Fecha Expiración </b></th> ' +
            '<th><b>Activo</b></th> ' +
            '<th><b>Acción</b></th> ' +
            '</tr>' +
            '</thead>' +
            '<tbody>' +
            '<tr>' +
            '<td>' +
            `<input type="hidden" name="Recetas[${this._index}].Vigencias.Index" value="${0}" />` +
            `<input type="hidden" name="Recetas[${this._index}].Vigencias[0].IdTerminal" value="${this._terminalIdActualizar}"/>` +
            `<input type="hidden" name="Recetas[${this._index}].Vigencias[0].IdProducto" value="${this._productoIdActualizar}"/>` +
            `<input type="hidden" name="Recetas[${this._index}].Vigencias[0].IdReceta" value="${idReceta}"/>` +
            `<input type="text" name="Recetas[${this._index}].Vigencias[0].FechaInicio" asp-format="{0:dd/MMM/yyyy}" placeholder="Fecha" style="width: 45%; margin-right: 5px;" class="datepicker formato-fecha-corta fechaIni">` +
            `<input type="text" name="Recetas[${this._index}].Vigencias[0].HoraInicio" class="timepicker horaIni" placeholder="Hora" style="width: 45%;">` +
            '</td>' +
            '<td>' +
            `<input type="text" name="Recetas[${this._index}].Vigencias[0].FechaExpiracion" asp-format="{0:dd/MMM/yyyy}" placeholder="Fecha" style="width: 45%; margin-right: 5px;" class="datepicker formato-fecha-corta fechaFin">` +
            `<input type="text" name="Recetas[${this._index}].Vigencias[0].HoraExpiracion" class="timepicker horaFin" placeholder="Hora" style="width: 45%;">` +
            '</td>' +
            '<td style="display: -webkit-box; margin-top: 10px;"> ' +
            '<label> No </label>' +
            '<div class="switch">' +
            '<label>' +
            `<input class="activo sv-${recetaId}" disabled type="checkbox" id="sw-${recetaId}-0">` +
            '<span class="lever"></span>' +
            '</label>' +
            '</div>' +
            '<label> Si </label> </td>' +
            `<td><a class="cursor-point"><i class="material-icons receta-action vigencia-delete" data-switchid='sw-${recetaId}-0' onclick="eliminarVigencia(this)">delete</i></a></td>` +
            '</tr>' +
            '</tbody>' +
            '</table>' +
            '</div>' +
            '</div>' +
            '</div>';
        container.appendChild(card);
        var DatePickers = new DatepickerComponent().GetInstances('.datepicker', undefined, () => { this.UpdateMaskValue() });
        M.Timepicker.init(document.querySelectorAll(".timepicker"), this._opcion);
        //M.updateTextFields();
    }

    private AgregarVigencia(element: HTMLElement): void {
        let vigenciaId = element.dataset.vigenciaid;
        let idReceta = element.dataset.recetaName;
        // Permite obtener el total de vigencias asignadas a una receta
        let index = document.getElementsByClassName(`sv-${vigenciaId}`).length;

        let table = document.getElementById("table-vigencia-" + vigenciaId) as HTMLTableElement;
        table.insertRow(-1).innerHTML = '<tr><td>' +
            `<input type="hidden" name="Recetas[${this._index}].Vigencias.Index" value="${index}" />` +
            `<input type="hidden" name="Recetas[${this._index}].Vigencias[${index}].IdTerminal" value="${this._terminalIdActualizar}" />` +
            `<input type="hidden" name="Recetas[${this._index}].Vigencias[${index}].IdProducto" value="${this._productoIdActualizar}" />` +
            `<input type="hidden" name="Recetas[${this._index}].Vigencias[${index}].IdReceta" value="${idReceta}"/>` +
            `<input type="text" name="Recetas[${this._index}].Vigencias[${index}].FechaInicio" asp-format="{0:dd/MMM/yyyy}" placeholder="Fecha" style="width: 45%; margin-right: 5px;" class="datepicker formato-fecha-corta fechaIni">` +
            `<input type="text" name="Recetas[${this._index}].Vigencias[${index}].HoraInicio" class="timepicker horaIni" placeholder="Hora" style="width: 45%;">` +
            '</td>' +
            '<td>' +
            `<input type="text" name="Recetas[${this._index}].Vigencias[${index}].FechaExpiracion" asp-format="{0:dd/MMM/yyyy}" placeholder="Fecha" style="width: 45%; margin-right: 5px;" class="datepicker formato-fecha-corta fechaFin">` +
            `<input type="text" name="Recetas[${this._index}].Vigencias[${index}].HoraExpiracion" class="timepicker horaFin" placeholder="Hora" style="width: 45%;">` +
            '</td>' +
            '<td style="display: -webkit-box; margin-top: 10px;">' +
            '<label> No </label>' +
            '<div class="switch">' +
            '<label> ' +
            `<input class="activo sv-${vigenciaId}" disabled type="checkbox" id="sw-${index}-${index}">` +
            '<span class="lever"> </span>' +
            '</label>' +
            '</div>' +
            '<label> Si </label>' +
            '</td>' +
            `<td><a class="cursor-point"><i class="material-icons receta-action vigencia-delete" data-switchid='sw-${vigenciaId}-${index}' onclick="eliminarVigencia(this)">delete</i></a></td></tr>`;
        var DatePickers = new DatepickerComponent().GetInstances('.datepicker', undefined, () => { this.UpdateMaskValue() });
        M.Timepicker.init(document.querySelectorAll(".timepicker"), this._opcion);
        //M.updateTextFields();
    }

    // Jeffer
    //Extraer terminal de formulario revisar
    private ExtraerTerminal(): Terminal {
        let terminal = new Terminal();

        terminal.IdTerminal = this._formData.get("IdTerminal")?.toString();
        console.log('Terminal Antes:', this._terminalIdActualizar);
        console.log('Terminal Despues:', terminal.IdTerminal);
        console.log(terminal.IdTerminal);
        this._terminalIdActualizar = terminal.IdTerminal;
        terminal.Nombre = this._formData.get("Nombre")?.toString();

        return terminal;
    }

    //Extraer Producto de formulario
    //private ExtraerProducto(): Producto {
    //    let producto = new Producto();
    private ExtraerProducto() {
        let producto;

        producto.IdProducto = this._productoIdActualizar;
        producto.NombreCorto = this._formData.get("NombreCorto")?.toString();
        producto.Estado = this._formData.get("Estado")?.toString();
        producto.Tipo = this._formData.get("IdTipo")?.toString();
        producto.Clase = this._formData.get("IdClase")?.toString();
        producto.SICOM = this._formData.get("Sicom")?.toString();

        return producto;
    }

    // Jeffer
    // Actualizar producto en datatable
    private ActualizarFilaDatatableProducto(producto) {
        let row = $("tr#" + producto.IdProducto);
        var cond = this._TableProductos.row(row).data();
        let recetas;
        for (var i = 0; i < producto.Recetas.lenght; i++) {
            if (true) {
                recetas = recetas + `<span class="chip lighten-5 green green-text">${producto.Recetas}</span>`
            } else {
                recetas = recetas + `<span class="chip lighten-5 red red-text">${producto.Recetas}</span>`
            }
        }
        cond[0] = '<label class="checkbox-label">' +
            '<input type="checkbox" name="foo" class="producto-action producto-asignar" data-productoid="1"/>' +
            '<span></span>' +
            '</label>';
        cond[1] = `<i class="material-icons" style="color: black">home</i>`;
        cond[4] = recetas;
        this._TableProductos.row(row).data(cond);
    }

    //Cerrar Modal
    private CerrarModal() {
        this._modalInstance.close();
        this._formCompania.parentNode.removeChild(this._formCompania);
    }

    private CerrarModalRecetas(estado?) {
        if (estado != undefined) {
            estado.checked = false;
        }
        this._modalInstance.close();
    }

    private FormatoFecha(fechaOriginal) {
        var MesActual = fechaOriginal.split("/")[1];
        switch (MesActual) {
            case "ene":
                fechaOriginal = fechaOriginal.replace(MesActual, "01");
                break;
            case "feb":
                fechaOriginal = fechaOriginal.replace(MesActual, "02");
                break;
            case "mar":
                fechaOriginal = fechaOriginal.replace(MesActual, "03");
                break;
            case "abr":
                fechaOriginal = fechaOriginal.replace(MesActual, "04");
                break;
            case "may":
                fechaOriginal = fechaOriginal.replace(MesActual, "05");
                break;
            case "jun":
                fechaOriginal = fechaOriginal.replace(MesActual, "06");
                break;
            case "jul":
                fechaOriginal = fechaOriginal.replace(MesActual, "07");
                break;
            case "ago":
                fechaOriginal = fechaOriginal.replace(MesActual, "08");
                break;
            case "sep":
                fechaOriginal = fechaOriginal.replace(MesActual, "09");
                break;
            case "oct":
                fechaOriginal = fechaOriginal.replace(MesActual, "10");
                break;
            case "nov":
                fechaOriginal = fechaOriginal.replace(MesActual, "11");
                break;
            default:
                fechaOriginal = fechaOriginal.replace(MesActual, "12");
                break;
        }
        return fechaOriginal;
    }

    //Formatear inputs
    private RegistrarFormatos() {
        //registrar mascaras 
        this.RegisterMasks(
            [
                MaskFormats.DateShortFormat()
            ]);
    }

    private UpdateMaskValue() {
        this.MaskManager.UpdateValue([".formato-fecha-corta"])
    }
}
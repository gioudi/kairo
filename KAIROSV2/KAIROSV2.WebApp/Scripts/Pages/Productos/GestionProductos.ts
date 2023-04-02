import { HttpFetchService, IMessageResponse } from '../../Shared';
import { Producto } from '../../Shared/Models/Producto';

import { ConfirmModalMessage } from '../../Shared/Components/ConfirmModalMessage';

import 'jquery-qubit';
import 'select2';
import 'select2/dist/css/select2.css';

export class ProductosGestionPage {

    //PROPIEDADES
    public onProductoCreado?: (producto: Producto) => void;
    public onProductoActualizado?: (producto: Producto) => void;

    public contador = 2;
    public _index: number = 0;

    //CAMPOS
    private _arrayClases: Producto[] = [];
    private _baseUrl = "/Productos";
    private _httpService: HttpFetchService;
    private _modalInstance: M.Modal;
    private _modalBase: HTMLElement;
    private _formUser: HTMLFormElement;
    private _selects: M.FormSelect[];
    private _collaps: M.Collapsible[];
    private _tabs: M.Tabs[];
    private _formData: FormData;
    private _productoIdActualizar: string;
    private Table: DataTables.Api;

    //CONSTRUCTOR
    constructor(modalBase: HTMLElement) {
        this._httpService = new HttpFetchService();
        this._modalBase = modalBase;
        this.InicializarModal();
    }

    //METODOS

    //Creacion de nuevo producto
    public NuevoProducto() {
        this._httpService.Post<string>(this._baseUrl + "/NuevoProducto", null, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormProducto(true);
            });

        this._modalInstance.open();

        
    }

    private async ObtenerComponentesDisponibles(claseId: string) {
        //this.LimpiarOpciones();
        this._httpService.Post<Producto[]>(this._baseUrl + '/ComponentesDisponibles', claseId, true)
            .then((data) => {
                if (data) {
                    this._arrayClases = data;
                }
            }).catch((error) => {
                M.toast({ html: error, classes: "error" });
            });
        //this.CambiarOpciones();
    }

    private LimpiarOpciones() {
        let selectClase = document.getElementsByClassName('clases-options');
        for (let i = 0; i < selectClase.length; i++) {
            for (let j = this._arrayClases.length; j > 0 ; j--) {
                selectClase[i][j].remove();
            }
        }
    }
    // Sin uso
    private CambiarOpciones() {
        let selectClase = document.getElementsByClassName('clases-options');
        for (let i = 0; i < selectClase.length; i++) {
            for (let j = 0; j < this._arrayClases.length; j++) {
                let option = document.createElement('option');
                option.value = this._arrayClases[j].IdProducto;
                option.text = this._arrayClases[j].NombreCorto;
                selectClase[i].append(option);
            }
        }
        this._selects = M.FormSelect.init(document.querySelectorAll("select"));
    }

    private AgregarOpciones() {
        let selectClase = document.getElementsByClassName('clases-options');
        let pos = selectClase.length - 1;
        for (let j = 0; j < this._arrayClases.length; j++) {
            let option = document.createElement('option');
            option.value = this._arrayClases[j].IdProducto;
            option.text = this._arrayClases[j].NombreCorto;
            selectClase[pos].append(option);
        }

        this._selects = M.FormSelect.init(document.querySelectorAll("select"));
        
    }

    //Guardar Producto
    private GuardarProducto(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/CrearProducto", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    this.LimpiarFormulario();
                    if (this.onProductoCreado)
                        this.onProductoCreado(this.ExtraerProducto());
                    this.LimpiarFormulario();
                    this.CerrarModal();
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

            })
            .catch((err) => console.log(err));
    }

    //Edicion y detalles de Producto
    public DatosProducto(idProducto: string, lectura: boolean) {
        var payload = { idEntidad: idProducto, lectura: lectura };
        this._productoIdActualizar = idProducto;
        this._httpService.Post<string>(this._baseUrl + "/DatosProducto", payload, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormProducto(false);
            });

        this._modalInstance.open();
    }

    //Actualizar Producto
    private ActualizarProducto(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._formData.set("IdProducto", this._productoIdActualizar);
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/ActualizarProducto", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    if (this.onProductoActualizado)
                        this.onProductoActualizado(this.ExtraerProducto());
                    //this.LimpiarFormulario();
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

    private SwitchEstado() {
        let switchA = (document.getElementById('aditivo') as HTMLInputElement).checked
        let aditivacion = document.getElementById('switchiden');
        if (switchA) {
            aditivacion.style.display = 'block'
        } else {
            aditivacion.style.display = 'none'
        }
    }

    private ClaseSeleccionada() {
        let clase = (document.getElementById('selectClase') as HTMLInputElement).value;
        if (clase != "") {
            document.getElementById('recetas-list').classList.remove('disabled');
        }
    }

    private async CambioClase(edit: boolean) {
        let recetas = document.querySelectorAll('.receta');
        let shouldDelete = false;
        let claseId = (document.getElementById('selectClase') as HTMLInputElement).value;
        if (claseId == '0') {
            this.EliminarRecetas();
            document.getElementById('recetas-list').classList.add('disabled');
            return;
        }
        if (edit) {
            await this.ObtenerComponentesDisponibles(claseId);
            return;
        }
        if (recetas.length > 0) {
            const confirm = new ConfirmModalMessage("Cambio de clase", "Esta accion borrara todas las recetas", "Aceptar", "Cancelar");
            shouldDelete = await confirm.Confirm();
        }        
        if (shouldDelete) {
            this.EliminarRecetas();            
        }

        await this.ObtenerComponentesDisponibles(claseId);
        document.getElementById('recetas-list').classList.remove('disabled');
    }

    //Inicializar controles de formulario
    private InicilizarFormProducto(modoCreacion: boolean): void {
        $(".select2").select2({ dropdownAutoWidth: true, width: '100%' });
        this._tabs = M.Tabs.init(document.querySelectorAll('.tabs'));
        this._selects = M.FormSelect.init(document.querySelectorAll("select"));
        this._collaps = M.Collapsible.init(document.querySelectorAll(".collapsible"));
        M.updateTextFields();

        this.calcularPorcentajeMezcla();
        this.SwitchEstado();
        let switchA = document.getElementById('aditivo').addEventListener('change', event => this.EsAditivado(event.target['checked']));

        // Activa el Tab
        this.ClaseSeleccionada();
        this.CambioClase(true);
        let selectClase = document.querySelector('#selectClase').addEventListener('change', event => {
            this.CambioClase(false)
        });

        let recetas = document.querySelector('#recetas-container').addEventListener('change', event => {
            this.SeleccionarProducto();
            this.calcularPorcentajeMezcla();
        });

        let recetAdd = document.getElementById('recetAdd').addEventListener('click', event => this.AgegarReceta());
        this._formUser = document.querySelector("#user-form");
        this._formData = new FormData(this._formUser);
        $(this._formUser).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(this._formUser);
        document.getElementById("user-modal-cancel").addEventListener('click', (ev) => this.CerrarModal());
        this.InicializarButtonsReceta();
        this.Contar();

        this._formUser.onsubmit = (e) => {
            e.preventDefault();

            if ($(this._formUser).valid()) {
                this._formData = new FormData(this._formUser);
                //this._formData.forEach((value, key) => { console.log(key + ": " + value) });

                if (modoCreacion)
                    this.GuardarProducto();
                else
                    this.ActualizarProducto();
            }

            return false; // prevent reload
        };
    }

    private InicializarButtonsReceta() {
        var recetas = document.getElementById("recetas-container");

        recetas.on('click', '.receta-action', event => {
            if(event.target.matches(".receta-delete"))
                this.EliminarReceta(event.target as HTMLElement);

            else if (event.target.matches(".receta-add"))
                this.AgegarComponente(event.target as HTMLElement);
        });

        recetas.on('click', '.componente-delete', event => {
            this.calcularPorcentajeMezcla();
        });

    }

    public Contar() {
        let index = document.getElementsByClassName('receta');
        this._index = index.length;
    }

    private Contar2(clase): Number {
        let index = document.getElementsByClassName(clase);
        return index.length;
    }

    private calcularPorcentajeMezcla() {
        let recetas = document.querySelectorAll('.receta');
        recetas.forEach(receta => {
            let total = 0;
            let indice = this.ObtenerIndice(receta.id)
            let tabla = '#data-table-' + receta.id + ' tbody';
            $(tabla).find('.ppm-'+ indice+'').each(function () {
                let $input = $(this),
                    $tr = $input.closest('tr'),
                    $tdPorc = $tr.find('td.porcent'),
                    $tdClase = $tr.find('td.clase-prod'),
                    value = parseFloat($input.val().toString());

                if ($tdClase[0].textContent == 'Aditivo') value = 0;
                if (isNaN(value)) value = 0;

                // Calculamos el % ("Regla de 3 simple")
                $tdPorc.text((value * 100 / 1000000) + '%');

                total += value;
            })
            let totalValor = document.getElementsByClassName('total-'+ indice+'');
            totalValor[0].innerHTML = 'Total ' + total + '';
            totalValor[1].innerHTML = 'Tolal ' + (total * 100 / 1000000) + '%';
        });
    }

    private ObtenerTipo(producto): string {
        let cls;
        this._arrayClases.forEach(clase => {
            if (clase.IdProducto == producto) {
                cls = clase.IdTipoNavigation.Descripcion;
            }
        });
        return cls;
    }

    // Acutalmente probando
    private SeleccionarProducto() {
        let recetas = document.querySelectorAll('.receta');
        recetas.forEach(receta => {
            let tabla = '#data-table-' + receta.id + ' tbody';
            let self = this;
            $(tabla).find('.clases-options').each(function () {
                let $productoSelect = $(this),
                    $tr = $productoSelect.closest('tr'),
                    $tdClase = $tr.find('td.clase-prod'),
                    $ppm = $tr.find('input'),
                    tipo,
                    value = $productoSelect.val();       
                if (value != null) {
                    $ppm[2].readOnly = false;
                }
                tipo = self.ObtenerTipo(value);
                $tdClase.text(tipo);
            })
        });
    }

    private ObtenerIndice(nombre): string {
        let slc = 1;
        if (nombre.length == 9) {
            slc = 2
        } else if (nombre.length == 10) {
            slc = 3
        }

        return nombre.slice(-slc);
    }


    private EsAditivado(estado) {
        let aditivacion = document.getElementById('switchiden');
        if (estado) {
            aditivacion.style.display = 'block'
        } else {
            aditivacion.style.display = 'none'
        }
    }

    private AgegarReceta() {
        this.Contar();
        
        let container = document.getElementById("recetas-container");
        var card = document.createElement("div");
        card.setAttribute('id', 'receta-' + this._index +'');
        card.classList.add('section');
        card.classList.add('receta');
        card.setAttribute('style', 'width: 80%; margin: 0 auto');
        card.innerHTML = '<ul class="collapsible">' +
            '<li>' +
            '<div class="collapsible-header" style="color: black; align-items: center;">' +
            `<input type="hidden" name="Recetas.Index" value="${this._index}" />` +
            `<input name=Recetas[${this._index}].IdReceta placeholder="Nombre Receta" maxlength="50" class="receta-name"/>` +
            '<div style="margin-left: auto;">' +
            '<a id="recetRemove" class="btn-floating waves-effect waves-light red accent-2" style="text-align: center">' +
            '<i class="material-icons receta-action receta-delete" data-recetaid="receta-' + this._index + '" style="margin: 0;">delete</i>' +
            '</a>' +
            '</div>' +
            '</div>' +
            '<div class="collapsible-body" style="background-color: white">' +
            '<div style="text-align: end; margin-top: -25px">' +
            '<a class="btn-floating waves-effect waves-light blue accent-2" style="text-align: center; margin-top: 10px; height: 30px; width: 30px;">' +
            '<i class="material-icons receta-action receta-add" data-recetaid="' + this._index + '" style="font-size: 20px; display: flex; margin: -5px 0px 0px 5px">add</i>' +
            '</a>' +
            '</div>' +
            '<div class="section row equal-col">' +
            '<div class="row" style="margin-right: auto; margin-left: auto; width: 90%">' +
            '<div class="col s12 m12 l12">' +
            '<div id="responsive-table" class="card card card-default scrollspy">' +
            '<div class="card-content">' +
            '<p class="mb-2">' + 'Composición' + '</p>' +
            '<div class="row">' +
            '<div class="col s12">' +
            '<table id="data-table-receta-' + this._index + '" class="Highlight">' +
            '<thead>' +
            '<tr>' +
            '<th>Producto </th>' +
            '<th>Tipo </th>' +
            '<th>Cantidad PPM </th>' +
            '<th>Porcentaje </th>' +
            '<th>Acción</th>' +
            '</tr>' +
            '</thead>' +
            '<tbody>' +
            '<td>' +
            `<input type="hidden" name = "Recetas[${this._index}].Componentes.Index" value = "${0}" /> ` +
            `<select class="clases-options" id="sr${this._index}0" name="Recetas[${this._index}].Componentes[0].IdComponente">` +
            '<option value="" selected disabled>Seleccionar producto</option>' +
            '</select>' +
            '</td>' +
            '<td class="clase-prod"></td>' +
            `<td><input type="text" style="width:70%" value="0" maxlength="7" class="ppm-${this._index}" name="Recetas[${this._index}].Componentes[0].ProporcionComponente" readonly></td>` +
            '<td class="porcent"></td>' +
            '<td>' +
            '<a class="cursor-point">' +
            '<i class="material-icons componente-delete" onclick="eliminarComponente(this, ' + this._index + ')">delete</i>'+
                                                            '</a>'+
                                                        '</td>'+
                                                    '</tbody>'+
                                                '</table>' +
                                            '</div>' +
                                        '</div>' +
                                    '</div>' +
                                '</div>' +
                            '</div>' +
                        '</div>' +
                        '<div class="row" style="width: 100%">'+
                            '<div class="col s6">'+
                            '</div>'+
                            '<div class="col s4" >'+
                                `<p class="total-${this._index}">Total: </p>` +                          
                            '</div>'+
                            '<div class="col s2" >' +
                                `<p class="total-${this._index}">Total: </p>` +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                '</div>' +
            '</li>' +
            '</ul>';
        container.appendChild(card);
        this._collaps = M.Collapsible.init(document.querySelectorAll(".collapsible"));
        this._selects = M.FormSelect.init(document.querySelectorAll("select"));
        $(".select2").select2({ dropdownAutoWidth: true, width: '100%' });
        this.AgregarOpciones();
    }

    private async EliminarReceta(element: HTMLElement) {
        let shouldDelete = false;
        let recetaId = element.dataset.recetaid;
        const confirm = new ConfirmModalMessage("Elminar Receta", "Esta accion borrara la receta", "Aceptar", "Cancelar");
        shouldDelete = await confirm.Confirm();
        if (!shouldDelete) return
        var padre = document.getElementById("recetas-container");
        var card = document.getElementById(recetaId);
        padre.removeChild(card);
    }

    private EliminarRecetas(): void {
        var padre = document.getElementById("recetas-container");
        let recetas = document.querySelectorAll('.receta');
        if (recetas.length == 0) {
            return;
        }

        recetas.forEach(r => {
            let receta = document.getElementById(r.id);
            padre.removeChild(receta);
        })
    }

    private AgegarComponente(element: HTMLElement): void {
        let recetaIndex = element.dataset.recetaid;
        let index2 = this.Contar2('ppm-' + recetaIndex);
        let table = document.getElementById("data-table-receta-" + recetaIndex) as HTMLTableElement;
        table.insertRow(-1).innerHTML = `<td><input type="hidden" name="Recetas[${recetaIndex}].Componentes.Index" value="${index2}"/>`+
            `<select class="clases-options" id="sr${recetaIndex}${index2}"  name="Recetas[${recetaIndex}].Componentes[${index2}].IdComponente">` +
                '<option value="" selected disabled>Seleccionar producto</option>'+
            '</select></td>'+
            '<td class="clase-prod"></td>'+
            `<td><input type="text" style="width:70%" value="0" maxlength="7" class="ppm-${recetaIndex}" name="Recetas[${recetaIndex}].Componentes[${index2}].ProporcionComponente" readonly></td>`+
            '<td class="porcent"></td>' +
            '<td>' +
                '<a class="cursor-point">' +
                    '<i class="material-icons componente-delete" onclick="eliminarComponente(this,'+ recetaIndex +')">delete</i>' +
                '</a>' +
            '</td>';
        this._selects = M.FormSelect.init(document.querySelectorAll("select"));
        $(".select2").select2({ dropdownAutoWidth: true, width: '100%' });
        this.AgregarOpciones();
    }

    //Extraer Producto de formulario
    private ExtraerProducto(): Producto {
        let producto = new Producto();

        producto.IdProducto = this._formData.get("IdProducto")?.toString();
        producto.NombreCorto = this._formData.get("NombreCorto")?.toString();
        producto.Estado = this._formData.get("Estado")?.toString();
        producto.Tipo = this._formData.get("IdTipo")?.toString();
        producto.Clase = this._formData.get("IdClase")?.toString();
        producto.SICOM = this._formData.get("Sicom")?.toString();

        return producto;
    }

    //Reset de formulario
    private LimpiarFormulario() {
        this._formUser.reset();
        $('input').not("input[type= 'hidden']").val(null);
        //$('select').val(null);
        const e = new Event("change");
        M.updateTextFields();
    }

    //Cerrar control modal
    private CerrarModal() {
        this._selects.every((selects) => selects.destroy());
        this._collaps.every((collaps) => collaps.destroy());
        this._modalInstance.close();
        this._formUser.parentNode.removeChild(this._formUser);
        this._productoIdActualizar = "";
    }
}
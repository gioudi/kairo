
import { HttpFetchService, IMessageResponse } from '../../Shared';
import { Proveedor } from '../../Shared/Models/Proveedor';

import 'select2';
import 'select2/dist/css/select2.css';
export class ProveedoresGestionPage {


    //PROPIEDADES
    public onProveedorCreado?: (proveedor: Proveedor) => void;
    public onProveedorActualizado?: (proveedor: Proveedor) => void;

    //CAMPOS
    private _baseUrl = "/Proveedores";
    private _httpService: HttpFetchService;
    private _modalInstance: M.Modal;
    private _selects: M.FormSelect[];
    private _collaps: M.Collapsible[];
    private _toast: M.Collapsible[];
    private _modalBase: HTMLElement;
    private _formProveedor: HTMLFormElement;
    private _formData: FormData;
    private _proveedorIdActualizar: string;

    //CONSTRUCTOR
    constructor(modalBase: HTMLElement) {
        this._httpService = new HttpFetchService();
        this._modalBase = modalBase;
        this.InicializarModal();
    }

    //METODOS

    //Creacion de nuevo proveedor
    public NuevoProveedor() {
        this._httpService.Post<string>(this._baseUrl + "/NuevoProveedor", null, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormProveedor(true);
            });

        this._modalInstance.open();
    }

    //Guardar Proveedor
    private GuardarProveedor(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/CrearProveedor", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    this.LimpiarFormulario();
                    if (this.onProveedorCreado)
                        this.onProveedorCreado(this.ExtraerProveedor());
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

            })
            .catch((err) => console.log(err));
    }

    //Edicion y detalles de Proveedor
    public DatosProveedor(cedula: string, lectura: boolean) {
        var payload = { IdEntidad: cedula, lectura: lectura };
        this._proveedorIdActualizar = cedula;
        this._httpService.Post<string>(this._baseUrl + "/DatosProveedor", payload, false)
            .then((data) => {
                this._modalBase.innerHTML = data;
                this.InicilizarFormProveedor(false);
            });

        this._modalInstance.open();
    }

    //Actualizar Proveedor
    private ActualizarProveedor(): void {
        let token = this._formData.get("RequestVerificationToken")?.toString();
        this._formData.set("IdProveedor", this._proveedorIdActualizar);
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/ActualizarProveedor", this._formData, token)
            .then((data) => {
                if (data.Result) {
                    if (this.onProveedorActualizado)
                        this.onProveedorActualizado(this.ExtraerProveedor());
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
    private InicilizarFormProveedor(modoCreacion: boolean): void {
        $(".select2").select2({ dropdownAutoWidth: true, width: '100%' });
        this._selects = M.FormSelect.init(document.querySelectorAll("select"));
        this._collaps = M.Collapsible.init(document.querySelectorAll(".collapsible"));
        M.updateTextFields();
        this._formProveedor = document.querySelector("#proveedor-form");
        this._formData = new FormData(this._formProveedor);
        $(this._formProveedor).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(this._formProveedor);
        document.getElementById("proveedor-modal-cancel").addEventListener('click', (ev) => this.CerrarModal());
        this.InicializarButtomsGestion();
        this._formProveedor.onsubmit = (e) => {
            e.preventDefault();

            if ($(this._formProveedor).valid()) {
                this._formData = new FormData(this._formProveedor);
                //formData.forEach((value, key) => { console.log(key + ": " + value) });

                if (modoCreacion)
                    this.GuardarProveedor();
                else
                    this.ActualizarProveedor();
            }

            return false; // prevent reload
        };
    }

    private InicializarButtomsGestion() {
        let proveedores = document.getElementById("collapsibles");
        
        let tablePr = document.getElementById("tabla-producto");
        let tablePl = document.getElementById("tabla-planta");

        proveedores.on('click', '.gestion-action', event => {
            if (event.target.matches(".tipo-add"))
                this.AgregarTipoProducto();
            else if (event.target.matches(".planta-add"))
                this.AgregarPlanta();
        });

        tablePr.on('click', '.producto-delete', event => {
            console.log(event);
            let row = event.target.parentNode.parentNode as JQuery;
            row.closest("tr").remove();
        });

        tablePl.on('click', '.planta-delete', event => {
            let row = event.target.parentNode.parentNode as JQuery;
            row.closest("tr").remove();
        });
    }


    private AgregarTipoProducto() {
        let element = (document.getElementById('tipo-producto') as HTMLSelectElement);
        let option = element.options[element.selectedIndex];
        let tipoId = element.value;
        let tipoDesc = option.text;
        let index = document.getElementsByClassName("proveedor-producto")?.length;
        let table = document.getElementById("tabla-producto") as HTMLTableElement;
        if (tipoId == '') {
            return;
        }
        if (this.Validar("tabla-producto", "proveedor-producto", tipoId)) {
            M.toast({
                html: 'No se puede asignar mas veces el mismo tipo',
                classes: 'error'
            });
            return;
        }        
        table.insertRow(-1).innerHTML = '<td>' + `<input type="hidden" name="ProveedoresProductos.Index" value="${index}" />` +
            `<input type="hidden" class="proveedor-producto" name="ProveedoresProductos[${index}].IdTipoProducto" value=${tipoId} /> ` + tipoDesc + '</td>' +
            '<td><a class="cursor-point"><i class="material-icons  producto-delete">delete</i></a></td>';
    }

    private AgregarPlanta() {
        let nombre = (document.getElementById('nombre-producto') as HTMLInputElement).value;
        let sicom = (document.getElementById('sicom-planta') as HTMLInputElement).value;
        let index = document.getElementsByClassName("proveedor-planta")?.length;
        let table = document.getElementById("tabla-planta") as HTMLTableElement;
        if (nombre == '' || sicom == '') {
            return;
        } 
        if (this.Validar("tabla-planta", "proveedor-planta", nombre)) {
            M.toast({
                html: 'No se puede asignar mas veces la misma planta',
                classes: 'error'
            });
            return;
        }
        table.insertRow(-1).innerHTML = '<td>' + `<input type="hidden" name="ProveedoresPlanta.Index" value="${index}" />` +
            `<input type="hidden" class="proveedor-planta" name="ProveedoresPlanta[${index}].PlantaProveedor" value="${nombre}" /> ` + nombre + '</td>' +
            '<td>' + `<input type="hidden" name="ProveedoresPlanta[${index}].SicomPlantaProveedor" value=${sicom} /> ` + sicom + '</td>' +
            '<td><a class="cursor-point"><i class="material-icons planta-delete">delete</i></a></td>';
              
    }

    private Validar(tabla?, clase?, registro? ): boolean {
        let res = false;
        $(`#${tabla}`).find(`.${clase}`).each(function () {
            let $input = $(this);
            console.log($(this));
            console.log('Input', $input.val());
            console.log('Compare', registro);

            if ($input.val() == registro) {
                res = true;
                return;
            }
        })

        return res;
        
    }

    //Extraer proveedor de formulario
    private ExtraerProveedor(): Proveedor {
        let proveedor = new Proveedor();
        proveedor.IdProveedor = this._formData.get("IdProveedor")?.toString();
        proveedor.NombreProveedor = this._formData.get("NombreProveedor")?.toString();
        proveedor.SicomProveedor = this._formData.get("SicomProveedor")?.toString();
        return proveedor;
    }

    //Reset de formulario
    private LimpiarFormulario() {
        this._formProveedor.reset();
        let tablePr = document.getElementById("tabla-producto") as HTMLTableElement;
        let tablePl = document.getElementById("tabla-planta") as HTMLTableElement;

        var rowCount = tablePr.rows.length;
        var rowCount2 = tablePl.rows.length;
        for (var i = 1; i < rowCount; i++) {
            tablePr.deleteRow(1);
        }
        for (var i = 1; i < rowCount2; i++) {
            tablePl.deleteRow(1);
        }
        M.updateTextFields();
    }

    //Cerrar control modal
    private CerrarModal() {
        this._modalInstance.close();
        this._formProveedor.parentNode.removeChild(this._formProveedor);
        this._proveedorIdActualizar = "";
    }  


}
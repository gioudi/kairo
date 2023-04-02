 import { HttpFetchService, IMessageResponse, Producto } from '../../Shared';
import { ConfirmModalMessage } from '../../Shared/Components/ConfirmModalMessage';
import { ConfigureDataTable } from '../../Shared/Components/ConfigureDataTable';
import { ProductosGestionPage } from './GestionProductos';
import { JQValidations } from '../../Shared/Utils/JQValidations';
import { SelectorMenu } from '../../Shared/Utils/SelectorMenu';
import { IPage } from '../../Shared/Components/IPage';
import { Tabs } from 'materialize-css';
import { Page } from '../../Core/Page';

export class ProductosPage extends Page {

    private Table: DataTables.Api;
    private modalInstance: M.Modal;
    private gestionProductos: ProductosGestionPage;

    constructor() {
        super();
        this.BaseUrl =  "/Productos/Index";
        this.Init();
    }

    public Destroy() {
        super.Destroy();
        this.Table.destroy(false);
        //this.Table.rows().invalidate().draw();
    }

    public Init() {
        this.InicializarButtons();
        this.InicializarControls();
        this.GetPermissions("/Productos/ObtenerPermisos");
    }

    private InicializarControls() {
        SelectorMenu.SeleccionEnElMenu();
        this.Table = new ConfigureDataTable().Configure("#data-table-producto", [{ "width": "5%", "targets": 0 }]);
        this.gestionProductos = new ProductosGestionPage(document.getElementById("producto-modal"));
        this.gestionProductos.onProductoCreado = (producto) => this.AgregarFilaDatatable(producto);
        this.gestionProductos.onProductoActualizado = (producto) => this.ActualizarFilaDatatable(producto);
        JQValidations.MaxFileSizeValidation();
        JQValidations.AllowedExtensionsValidation();
    }

    private InicializarButtons() {
        var table = document.getElementById("data-table-producto");
        var productoAdd = document.getElementById("producto-add");

        table.on('click', '.producto-action', event => {
            if (event.target.matches(".producto-edit"))
                this.EditarProducto(event.target as HTMLElement);

            else if (event.target.matches(".producto-delete"))
                this.BorrarProducto(event.target as HTMLElement);

            else if (event.target.matches(".producto-detail"))
                this.DetallesProducto(event.target as HTMLElement);
        });

        //listOverlay.on()

        productoAdd.addEventListener('click', event => this.CrearProducto());
    }

    private async BorrarProducto(element: HTMLElement) {
        let productoId = element.dataset.productoid;
        const confirm = new ConfirmModalMessage("Eliminar Producto", "¿Desea eliminar el Producto " + productoId + "?", "Aceptar", "Cancelar");
        const shouldDelete = await confirm.Confirm();
        if (shouldDelete) {
            const httpService = new HttpFetchService();
            httpService.Post<IMessageResponse>('/Productos/BorrarProducto', productoId)
                .then((data) => {
                    if (data.Result)
                        this.BorrarFilaDatatable(element);
                    M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

                }).catch((error) => {
                    M.toast({ html: error, classes: "error" });
                });
        }
    }

    private CrearProducto() {
        this.gestionProductos.NuevoProducto();
    }

    private EditarProducto(element: HTMLElement): void {
        console.log(element);
        this.gestionProductos.DatosProducto(element.dataset.productoid, false);
    }

    private DetallesProducto(element: HTMLElement): void {
        this.gestionProductos.DatosProducto(element.dataset.productoid, true);
    }

    //Funcion para borrar Producto del datatable
    private BorrarFilaDatatable(element: HTMLElement) {
        var $tr = $(element).closest("tr");
        if ($tr.prev().hasClass("parent")) {
            let row = this.Table.row($tr.prev());
            row.remove();
            row.draw();
        }
        let row = this.Table.row($tr);
        row.remove();
        row.draw();
    }

    // agregar un nuevo Producto
    private AgregarFilaDatatable(producto: Producto) {
        let estado, tipo, clase;
        if (producto.Estado == "true") {
            estado = '<span class="chip lighten-5 green green-text">Activo</span>';
        } else {
            estado = '<span class="chip lighten-5 red red-text">Inactivo</span>';
        }
        switch (producto.Clase) {
            case '1':
                clase = 'Base';
                break;
            case '2':
                clase = 'Mezcla';
                break;
            default:
                clase = 'Premezcla';
                break;
        };
        switch (producto.Tipo) {
            case '1':
                tipo = 'Gasolina';
                break;
            case '2':
                tipo = 'Diesel';
                break;
            case '3':
                tipo = 'Alcohol';
                break;
            case '4':
                tipo = 'Biodiesel';
                break;
            default:
                tipo = 'Aditivo';
                break;
        };
        let newRow = this.Table.row.add([
            '<i class="material-icons">format_color_reset</i>',
            producto.IdProducto,
            producto.NombreCorto,
            estado,
            clase,
            tipo,
            producto.SICOM,
            '<a class="cursor-point ' + this.DisableAction(this.Permisions.Editar) + '" data-turbolinks="false"><i class="material-icons producto-action producto-edit" data-productoid="' + producto.IdProducto + '">edit</i></a> ' +
            '<a class="cursor-point ' + this.DisableAction(this.Permisions.Borrar) + '" data-turbolinks="false"><i class="material-icons producto-action producto-delete" data-productoid="' + producto.IdProducto + '">delete</i></a> ' +
            '<a class="cursor-point ' + this.DisableAction(this.Permisions.Detalles) + '" data-turbolinks="false"><i class="material-icons producto-action producto-detail" data-productoid="' + producto.IdProducto + '">remove_red_eye</i></a>'
        ]);
        (this.Table.row(newRow).node() as HTMLElement).id = producto.IdProducto;
        (this.Table.row(newRow).node() as HTMLTableRowElement).cells[0].classList.add('aling-center');
        (this.Table.row(newRow).node() as HTMLTableRowElement).cells[3].classList.add('aling-center');
        newRow.draw(false);
    }

    // Actualizar un usuario
    private ActualizarFilaDatatable(producto: Producto) {
        let row = $("tr#" + producto.IdProducto);
        var cond = this.Table.row(row).data();
        //cond[0] = producto.Icono;
        cond[1] = producto.IdProducto;
        cond[2] = producto.NombreCorto;
        if (producto.Estado == "true") {
            cond[3] = '<span class="chip lighten-5 green green-text">Activo</span>';            
        } else {
            cond[3] = '<span class="chip lighten-5 red red-text">Inactivo</span>';
        }
        switch (producto.Clase) {
            case '1':
                cond[4] = 'Base';
                break;
            case '2':
                cond[4] = 'Mezcla';
                break;
            default:
                cond[4] = 'Premezcla';
                break;
        };
        switch (producto.Tipo) {
            case '1':
                cond[5] = 'Gasolina';
                break;
            case '2':
                cond[5] = 'Diesel';
                break;
            case '3':
                cond[5] = 'Alcohol';
                break;
            case '4':
                cond[5] = 'Biodiesel';
                break;
            default:
                cond[5] = 'Aditivo';
                break;
        };
        cond[6] = producto.SICOM;
        this.Table.row(row).data(cond);
    }

    //Obtiene clase disable dependiendo del permiso
    private DisableAction(habilitado: boolean): string {
        return habilitado ? "" : "disable-action";
    }
}

var productosPage = new ProductosPage();


// Called after every non-initial page load
document.addEventListener('turbolinks:render', (e: any) => {
    if (document.URL.indexOf('/Productos/Index') != -1) {
        if (productosPage) {
            productosPage.Init();
        }
        else {
            productosPage = new ProductosPage();
        }
        //console.log(e.data.url);
    }
    else
        productosPage?.Destroy();
});
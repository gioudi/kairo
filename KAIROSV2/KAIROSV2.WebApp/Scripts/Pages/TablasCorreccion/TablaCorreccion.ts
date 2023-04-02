import { HttpFetchService, IMessageResponse } from '../../Shared';
import PerfectScrollbar from 'perfect-scrollbar';
import 'datatables.net';
import 'datatables.net-responsive';
import 'datatables.net-responsive-dt/css/responsive.dataTables.css';
import IMask from 'imask';
import { Page } from '../../Core/Page';
import { MaskFormats } from '../../Shared/Utils/MaskFormats';

export class TablaCorreccionPage extends Page{

    //CAMPOS
    private _baseUrl = "/TablasCorreccion";
    private _dataUrl = ""
    private _table: DataTables.Api;
    private _columns: DataTables.ColumnSettings[];
    private _httpService: HttpFetchService;
    private _areaTablaSismtema: HTMLElement;
    private _reconfiguringTable: boolean;

    constructor(columns: DataTables.ColumnSettings[], urlData: string) {
        super();
        this.FormattFields();
        this._httpService = new HttpFetchService();
        this._areaTablaSismtema = document.getElementById("area-tabla-sistema");
        this.ReconfigureTable(columns, urlData, "");
    }

    public Destroy() {
        super.Destroy();
        this._table.destroy(true);
    }

    public ReconfigureTable(columns: DataTables.ColumnSettings[], urlData: string, tipoTabla: string) {

        this._dataUrl = urlData;
        this._columns = columns;

        if (this._table)
            this._table.destroy(false);

        if (tipoTabla)
            this.ObtenerPartialTablaCorreccion(tipoTabla);
        else {
            this.ConfigureDataTable("#data-table-contact");
            this.InicializarBusqueda();
            this.MaskManager.ApplyMasks();
        }
    }

    private ConfigureDataTable(id: string) {

        this._reconfiguringTable = true;

        var calcDataTableHeight = function () {
            return $(window).height() - 480 + "px";
        };

        this._table = $(id).DataTable({
            scrollY: calcDataTableHeight(),
            scrollCollapse: true,
            scrollX: false,
            columns: this._columns,
            paging: true,
            lengthMenu: [15],
            destroy: true,
            responsive: true,
            serverSide: true,
            ajax: {
                url: this._baseUrl + this._dataUrl,
                type: "POST",
                dataType: "json",
                beforeSend: () => {
                    if (this._reconfiguringTable) {
                        this._reconfiguringTable = false;
                        $('#data-table-contact > tbody').html(
                            '<tr class="odd dataTables_empty">' +
                            '<td colspan="3" valign="top">' +
                            '<div class="col l12 s12 m4 center">' +
                            '<div class="preloader-wrapper big active" > ' +
                            '<div class= "spinner-layer spinner-blue-only" >' +
                            '<div class="circle-clipper left" >' +
                            '<div class="circle" > </div>' +
                            '</div>' +
                            '<div class="gap-patch"> ' +
                            '<div class= "circle"> </div>' +
                            '</div>' +
                            '<div class="circle-clipper right">' +
                            '<div class= "circle"> </div>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</div>' +
                            '</td >' +
                            '</tr>'
                        );
                    }
                }
            },
            data: null,
            language: {
                "processing": '<div class="preloader- wrapper small active"> <div class= "spinner-layer spinner-green-only" > <div class="circle-clipper left"> <div class="circle"> </div> </div><div class="gap-patch"> <div class= "circle"> </div> </div><div class="circle-clipper right"> <div class= "circle" > </div> < /div> < /div> </div>',
                "lengthMenu": "Mostrar _MENU_ registros",
                "zeroRecords": "No se encontraron resultados",
                "emptyTable": "Ningún dato disponible en esta tabla",
                "info": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
                "infoEmpty": "Mostrando del 0 al 0 de 0 registros",
                "infoFiltered": "(filtrado de un total de _MAX_ registros)",
                "search": "Buscar:",
                "thousands": ",",
                "loadingRecords": "Cargando...",
                "paginate": {
                    "first": "Primero",
                    "last": "Último",
                    "next": "Siguiente",
                    "previous": "Anterior"
                },
                "aria": {
                    "sortAscending": ": Activar para ordenar la columna de manera ascendente",
                    "sortDescending": ": Activar para ordenar la columna de manera descendente"
                }
            }
        });
    }

    private InicializarBusqueda() {
        let formUser: HTMLFormElement = document.querySelector("#tablas-sistema-form");
        formUser.onsubmit = (e) => {
            e.preventDefault();

            this._table.column(0).search((document.getElementById("filtro_0") as HTMLInputElement).value);
            this._table.column(1).search((document.getElementById("filtro_1") as HTMLInputElement).value);
            this._table.column(2).search((document.getElementById("filtro_2") as HTMLInputElement).value);

            this._table.draw();
        }

        return false; // prevent reload
    };

    private ObtenerPartialTablaCorreccion(tipoTabla: string) {
        this._httpService.Post<string>(this._baseUrl + "/PartialTablaCorreccion", tipoTabla, false)
            .then((data) => {
                this._areaTablaSismtema.innerHTML = data;
                this.ConfigureDataTable("#data-table-contact");
                this.InicializarBusqueda();
                this.MaskManager.ApplyMasks();
            });
    }

    private FormattFields() {
        this.RegisterMasks(
            [
                MaskFormats.DecimalFormatRang(".filtro-tabla", 0, 1000000)
            ]
        )
        //formData.forEach((value, key) => { console.log(key + ": " + value) });
    }
}
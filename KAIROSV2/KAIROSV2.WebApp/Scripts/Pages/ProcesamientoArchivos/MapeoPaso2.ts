import { HttpFetchService, Paso2Mapeo } from "../../Shared";

export class MapeoPaso2 {

    //PROPIEDADES
    public onPaso2Cerrar?: () => void;
    public onPaso2Completado?: () => void;

    //CAMPOS
    private _mStepper: MStepper;
    private _step2Model: Paso2Mapeo;

    constructor(stepper: MStepper, step2: Paso2Mapeo) {
        this._mStepper = stepper;
        this._step2Model = step2;
    }

    //--- METODOS -----------------------------------------------------------------

    //Inicializa controles paso 2
    public InicializarControlesPaso2(modoCreacion: boolean) {
        $('.btn.close').on('click', () => this.onPaso2Cerrar());
        document.getElementById("step2-next").addEventListener('click', (ev) => {
            if (this.onPaso2Completado)
                this.onPaso2Completado();
        });
        this._step2Model.TableStep2 = $("#table-step2").DataTable({
            //scrollY: this.CalcDataTableHeight(),
            scrollX: false,
            search: false,
            lengthMenu: [4],
            destroy: true,
            responsive: true,
            paging: true,
            language: {
                "processing": "Procesando...",
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
        this.ExtraerDatosPaso2();
    }

    private ExtraerDatosPaso2(): void {
        this._step2Model.Columnas = new Array();
        this._step2Model.TableStep2.columns().every((coldx, tableLoop, colLoop) => {
            this._step2Model.Columnas.push(this._step2Model.TableStep2.column(coldx).header().innerHTML);
        });
    }

    public Destroy(): void {
        this._step2Model?.TableStep2?.destroy(true);
        this._step2Model = null;
    }
}
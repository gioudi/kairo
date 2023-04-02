//import 'datatables.net';
import 'datatables.net-responsive';

export class ConfigureDataTable {

    public Configure(id: string, columns?: DataTables.ColumnDefsSettings[]): DataTables.Api {
        
        let table = $(id).DataTable({
            columnDefs: columns,
            destroy: true,
            responsive: true,
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

        this.FilterGlobal(table);

        return table;
    }

    public ConfigureScrollX(id: string, columns?: DataTables.ColumnSettings[], columnsDef?: DataTables.ColumnDefsSettings[], inputId?): DataTables.Api {

        let table = $(id).DataTable({
            columnDefs: columnsDef,
            columns: columns,
            destroy: true,
            scrollX: true,
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

        this.FilterGlobal(table, inputId);

        return table;
    }

    public ConfigureScrollXSinInfo(id: string, columns?: DataTables.ColumnDefsSettings[]): DataTables.Api {

        let table = $(id).DataTable({
            columnDefs: columns,
            destroy: true,
            scrollX: true,
            paging: false,
            ordering: false,
            searching: false,
            info: false,
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
        return table;
    }

    private FilterGlobal(table: DataTables.Api, input?) {

        if (input) {
            $(`input#${input}`).on("keyup click", function () {
                table.search($(`#${input}`).val().toString(), $("#global_regex").prop("checked"), $("#global_smart").prop("checked")).draw();
            });
        } else {
            $("input#global_filter").on("keyup click", function () {
                table.search($("#global_filter").val().toString(), $("#global_regex").prop("checked"), $("#global_smart").prop("checked")).draw();
            });
        }

        $("input.column_filter").on("keyup click", function () {
            let column = $(this).parents("tr").attr("data-column");
            table.column(column)
                .search(
                    $("#col" + column + "_filter").val().toString(),
                    $("#col" + column + "_regex").prop("checked"),
                    $("#col" + column + "_smart").prop("checked")
                )
                .draw()
        });
    }

}
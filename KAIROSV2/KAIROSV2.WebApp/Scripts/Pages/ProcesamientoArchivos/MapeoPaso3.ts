import { HttpFetchService, IMessageResponse, MapeoArchivos } from "../../Shared";
import { TablaSistemaColumna, TablasSistema } from "../../Shared/Models/TablasKairos";

export class MapeoPaso3 {

    //PROPIEDADES
    public onPaso3Cerrar?: () => void;
    public onPaso3Completado?: () => void;
    public onPaso3Anterior?: () => void;

    //CAMPOS
    private _nuevoMapeo: boolean;
    private _baseUrl = "/ProcesamientoArchivos";
    private _httpService: HttpFetchService;
    private _formMapeoPaso3: HTMLFormElement;
    private _dataFormPaso3: FormData;
    private _mStepper: MStepper;
    private _step3Model: MapeoArchivos;
    private _optionsTablas: string[];
    private _tablasKairos: TablasSistema[];
    private _collapsibleInstance: M.Collapsible;
    private _selects: M.FormSelect[];
    private _actualizarMapeo: boolean;

    constructor(stepper: MStepper, mapeoModel: MapeoArchivos) {
        this._httpService = new HttpFetchService();
        this._mStepper = stepper;
        this._step3Model = mapeoModel;
        this._tablasKairos = new Array<TablasSistema>();
        this._selects = new Array<M.FormSelect>();
    }

    //--- METODOS -----------------------------------------------------------------

    public async InicializarControlesPaso3(esNuevo: boolean, actualizar: boolean) {
        this._nuevoMapeo = esNuevo;
        this._actualizarMapeo = actualizar;
        $('#add-map-table').on('click', () => { this.AddNewMapTable() });
        $('.btn.close').on('click', () => this.onPaso3Cerrar());
        let collapsible = document.querySelector<HTMLElement>('.section-tables-map');
        this._collapsibleInstance = M.Collapsible.init(collapsible, { accordion: true });
        $('.collapsible .collapsible-header .input-field').on('click', function () { return false; });
        this.InicilizarFormMapeoPaso3();
        this.InicializarControlesPaso2();
        this.InicializarEventosGenerales(collapsible);
        this.ObtenerNombresTablas();
    }

    //Inicializa formulario paso 3
    private InicilizarFormMapeoPaso3(): void {

        this._formMapeoPaso3 = document.querySelector<HTMLFormElement>('#mapeo-form-step3');
        this.InicializarData();
        this.ApplyValidation();


        this._formMapeoPaso3.onsubmit = (e) => {
            e.preventDefault();

            if ($(this._formMapeoPaso3).valid()) {
                this._dataFormPaso3 = new FormData(this._formMapeoPaso3);
                //this._dataFormPaso3.forEach((value, key) => { console.log(key + ": " + value) });
                if (this._actualizarMapeo)
                    this.ActualizarProcesamientoArchivos();
                else
                    this.CrearProcesamientoArchivos();
            }
            else {
                M.toast({ html: "Error de validación. Revisar los datos incompletos dentro de los desplegables.", classes: "error" });
                this._mStepper.wrongStep();
            }

            return false; // prevent reload
        };
    }

    private InicializarControlesPaso2(): void {

        if (this._nuevoMapeo) {
            document.getElementById("step3-previous").addEventListener('click', (ev) => {
                this._step3Model.Paso2Data.TableStep2.page.len(4).draw();
                document.getElementById("container-table-step2").appendChild(this._step3Model.Paso2Data.TableStep2.table("#table-step2").container());
            });
            this._step3Model.Paso2Data.TableStep2.page.len(1).draw();
            document.getElementById("container-table-step3").appendChild(this._step3Model.Paso2Data.TableStep2.table("#table-step2").container());
        }
        else {
            document.getElementById("step3-previous").addEventListener('click', (ev) => {
                if (this.onPaso3Anterior)
                    this.onPaso3Anterior();
                this._mStepper.openStep(0, null);
            });
        }
    }

    private InicializarData() {

        if (this._nuevoMapeo) {
            document.querySelector<HTMLInputElement>("#map-step3-separator").value = this._step3Model.Paso1Data.Separador;
            document.querySelector<HTMLInputElement>("#map-step3-otro-separator").value = this._step3Model.Paso1Data.OtroCaracter;
            document.querySelector<HTMLInputElement>("#mapeo-step3-id").value = this._step3Model.Paso1Data.IdMapeo;
            document.querySelector<HTMLInputElement>("#mapeo-step3-desc").value = this._step3Model.Paso1Data.Descripcion;
        }
        else {
            this._step3Model.Paso2Data.Columnas = new Array<string>();
            let fileList = document.querySelector<HTMLSelectElement>(".columns-file-options");
            Array.from(fileList.options).forEach((ele) => {
                if (!ele.disabled)
                    this._step3Model.Paso2Data.Columnas.push(ele.value);
            })
        }
    }

    private InicializarEventosGenerales(collapsible: HTMLElement): void {
        collapsible.on('change', 'select', async event => {
            if (event.target.matches(".list-table-names"))
                await this.CambioTablaKairos(event.target as HTMLSelectElement);

            if (event.target.matches(".columns-table-options"))
                await this.CambioColumnaTablaKairos(event.target as HTMLSelectElement);

            if (event.target.matches(".columns-file-options"))
                await this.CambioColumnaArchivoKairos(event.target as HTMLSelectElement);

        });

        collapsible.on('click', '.map-table-action', async event => {
            if (event.target.matches("[data-map-column-add]")) {
                await this.AgregarNuevaFilaMapeo(event.target as HTMLElement);
            };

            if (event.target.matches("[data-map-column-remove]")) {
                await this.BorrarFilaMapeo(event.target as HTMLElement);
            }

            if (event.target.matches("[data-map-table-delete]")) {
                await this.BorrarTablaMapeo(event.target as HTMLElement);
            }

        });

        collapsible.on('input', '.map-priority', async event => {
            await this.RevisarPrioridades(event.target as HTMLInputElement);
        });
    }

    private ApplyValidation() {
        this._dataFormPaso3 = new FormData(this._formMapeoPaso3);
        $(this._formMapeoPaso3).removeData("validator").removeData("unobtrusiveValidation");
        $.validator.setDefaults({ "ignore": "input[type=hidden]" });
        $.validator.unobtrusive.parse(this._formMapeoPaso3);
    }

    private RevisarPrioridades(element: HTMLInputElement): void {
        if (element.value != "") {
            let number = parseInt(element.value);
            if (isNaN(number)) {
                element.value = "";
            }
            else {
                element.value = number.toString();
                let priorities = document.querySelectorAll<HTMLInputElement>(".map-priority:not(#" + element.id + ")");
                if (Array.from(priorities).some(e => e.value == element.value)) {
                    M.toast({ html: "Prioridad (" + element.value + ") ya asignada.", classes: "error" });
                    element.value = "";
                }
            }

        }
    }

    private RevisarTablaRepetida(element: HTMLSelectElement): boolean {
        let selects = document.querySelectorAll<HTMLSelectElement>(".list-table-names:not(#" + element.id + ")");
        if (Array.from(selects).some(e => e.value == element.value)) {
            M.toast({ html: "La tabla (" + element.value + ") ya fue asignada", classes: "error" });
            element.selectedIndex = 0;
            M.FormSelect.getInstance(element).input.value = "";
            element.parentElement.firstChild.nodeValue = "";
            return false;
        }
        return true;
    }

    private async AgregarNuevaFilaMapeo(element: HTMLElement) {
        let card = element.closest<HTMLTableElement>("[data-map-table]");
        let tableId = parseInt(card.dataset.mapTable);
        let tableList = card.querySelector<HTMLSelectElement>(".list-table-names");
        let table = card.querySelector("table");
        let tablaKairos = await this.ObtenerTablaKairos(tableList.value);
        let columnList = tablaKairos.Columnas.map(e => e.Nombre);
        table.tBodies[0].insertAdjacentHTML("beforeend", this.GetRowTemplateColumnsTable(tableId, null, columnList));
        this._selects.concat(M.FormSelect.init(table.tBodies[0].querySelectorAll<HTMLSelectElement>("tr:last-child[data-map-column] select")));
        M.updateTextFields();
        this.ApplyValidation();
    }

    private BorrarFilaMapeo(element: HTMLElement) {
        element.closest<HTMLTableRowElement>("tr").remove();
        this.ErrorMaterializeCheckboxHidden();
    }

    private BorrarTablaMapeo(element: HTMLElement) {
        element.closest<HTMLLIElement>("li").remove();
        this.ErrorMaterializeCheckboxHidden();
    }

    private ErrorMaterializeCheckboxHidden(): void {
        document.querySelectorAll("input[name*='Llave'][type='hidden']").forEach(e => e.remove());
        document.querySelectorAll("input[name*='IsNull'][type='hidden']").forEach(e => e.remove());
    }
    private async CambioTablaKairos(elemtn: HTMLSelectElement) {
        if (!this.RevisarTablaRepetida(elemtn))
            return;

        let tablaKairos = await this.ObtenerTablaKairos(elemtn.value)
        let liElement = elemtn.closest("[data-map-table]");
        let tableColumns = liElement.querySelector<HTMLTableElement>("[data-map-column-list]");
        let idTable = parseInt(tableColumns.dataset.mapColumnList);
        let columnList = tablaKairos.Columnas.map(e => e.Nombre);
        liElement.querySelector("[data-map-column-add]").parentElement.classList.remove("disabled-btn");

        tableColumns.tBodies[0].innerHTML = "";
        tablaKairos.Columnas.filter(c => c.IsNull === false).forEach((columna) => {
            tableColumns.tBodies[0].insertAdjacentHTML("beforeend", this.GetRowTemplateColumnsTable(idTable, columna, columnList));
        });
        tableColumns.querySelectorAll<HTMLSelectElement>("tr td:nth-child(2) select").forEach(s => s.disabled = true);
        this._selects.concat(M.FormSelect.init(tableColumns.querySelectorAll("select")));
        M.updateTextFields();
        this.ApplyValidation();
    }

    private async CambioColumnaArchivoKairos(elemtn: HTMLSelectElement) {
        elemtn.closest("td").querySelector<HTMLInputElement>("input[type='hidden'][name*='IndexColumnaArchivo']").value = elemtn.selectedIndex.toString();
    }

    private async CambioColumnaTablaKairos(elemtn: HTMLSelectElement) {
        let tableName = elemtn.closest("li").querySelector<HTMLSelectElement>(".list-table-names").value;
        let tableIndex = parseInt(elemtn.closest("table").dataset.mapColumnList);
        let columnName = elemtn.value;
        let tablaKairos = await this.ObtenerTablaKairos(tableName);
        let columnSelected = tablaKairos.Columnas.find(e => e.Nombre == columnName);
        let row = elemtn.closest<HTMLTableRowElement>("tr");

        let isKey = row.cells[2].querySelector<HTMLInputElement>("input[type='checkbox']");
        isKey.value = columnSelected.Llave.toString();
        isKey.checked = columnSelected.Llave;

        let isnullCell = row.cells[3].querySelector<HTMLInputElement>("input[type='checkbox']");
        isnullCell.value = columnSelected.IsNull.toString();
        isnullCell.checked = columnSelected.IsNull;

        row.cells[4].querySelector<HTMLInputElement>("input[type='text']").value = columnSelected.Tipo;
        row.cells[5].innerHTML = this.GetRemoveColumnButton(columnSelected.IsNull, tableIndex);
    }

    private async ObtenerTablaKairos(nombreTabla: string) {
        let tablaKairos = this._tablasKairos?.find(t => t.NombreTabla == nombreTabla);
        if (tablaKairos == undefined) {
            tablaKairos = await this.ObtenerTablaColumnas(nombreTabla);
            this._tablasKairos.push(tablaKairos);
        }
        return tablaKairos;
    }

    //-- OPERACIONES SERVIDOR -----------------------------------------------------

    private async ObtenerTablaColumnas(nombre: string) {
        return new Promise<TablasSistema>((resolve, reject) => {

            this._httpService.Post<TablasSistema>(this._baseUrl + "/GetTablaColumnas", nombre, true)
                .then((data) => {
                    resolve(data);
                })
                .catch((err) => {
                    reject(err);
                });
        });
    }

    //Obtener nombre de tablas
    private ObtenerNombresTablas(): void {
        this._httpService.Post<string[]>(this._baseUrl + "/GetTablas", null, true)
            .then((data) => {

                this._optionsTablas = data;
                if (!this._actualizarMapeo)
                    document.querySelectorAll<HTMLSelectElement>(".list-table-names").forEach((el, num, parent) => {
                        this.AddTableOptionsToSelect(el);
                    });

                this._selects.concat(M.FormSelect.init(document.querySelectorAll("select")));
            })
            .catch((err) => console.log(err));
    }

    //Crear mapeo
    private CrearProcesamientoArchivos(): void {
        let token = this._dataFormPaso3.get("RequestVerificationToken")?.toString();
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/CrearProcesamientoArchivos", this._dataFormPaso3, token)
            .then((data) => {
                if (data.Result) {
                    if (this.onPaso3Completado)
                        this.onPaso3Completado();
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });
            })
            .catch((err) => console.log(err));
    }

    //Actualizar mapeo
    private ActualizarProcesamientoArchivos(): void {
        let token = this._dataFormPaso3.get("RequestVerificationToken")?.toString();
        //this._dataFormPaso3.forEach((value, key) => { console.log(key + ": " + value) });
        this._httpService.PostForm<IMessageResponse>(this._baseUrl + "/ActualizarProcesamientoArchivos", this._dataFormPaso3, token)
            .then((data) => {
                if (data.Result) {
                    if (this.onPaso3Completado)
                        this.onPaso3Completado();
                }
                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });
            })
            .catch((err) => console.log(err));
    }

    //-- OPERACIONES DOM --------------------------------------------------------

    private AddNewMapTable(): void {
        let newIndex = this.GetNewIndexListTable();

        let li = this.GetCardTemplateColumns();
        (this._collapsibleInstance.el as HTMLUListElement).insertAdjacentHTML("beforeend", li);
        let insertedLi = document.querySelector("[data-map-table='" + newIndex + "']");
        let selectItems = insertedLi.querySelectorAll("select");
        $("[data-map-table='" + newIndex + "']").find(".input-field").on('click', function () { return false; });
        this._selects.concat(M.FormSelect.init(selectItems));
        this.ApplyValidation();

    }

    private GetCardTemplateColumns(): string {
        let newIndex = this.GetNewIndexListTable();
        return ` <li data-map-table="${newIndex}">
                    <div class="collapsible-header">
                        <i class="material-icons">view_list</i>
                        <input type="hidden" name="TablasSistemas.Index" value="${newIndex}" />
                        <div class="input-field">
                            ${this.GetSelectTemplateTablesList()}
                        </div>
                        <div class="right mr-4">
                            <a class="mb-6 btn-floating waves-effect waves-light purple lightrn-1 btn-small " >
                                <i class="material-icons map-table-action" data-map-table-delete="${newIndex}">remove</i>
                            </a>
                        </div>
                    <small class="ml-3"><span class="red-text" data-valmsg-for="TablasSistemas[${newIndex}].NombreTabla" data-valmsg-replace="true"></span></small>
                    </div>
                    <div class="collapsible-body">
                        <div class="row">
                            <small><span class="right red-text mr-2" data-valmsg-for="TablasSistemas[${newIndex}].Prioridad" data-valmsg-replace="true"></span></small>
                            <div class="col s12 display-flex justify-content-end short-group">
                                Prioridad:
                                <input class="short-text map-priority" type="text" id="TablasSistemas_${newIndex}__Prioridad" name="TablasSistemas[${newIndex}].Prioridad" data-val='true' data-val-range='debe ser menor a 100' data-val-range-max='100' data-val-range-min='1' data-val-required='Prioridad es obligatorio' />
                                <a class="ml-2 btn-floating waves-effect waves-light light-green lightrn-1 btn-small disabled-btn" >
                                    <i class="material-icons map-table-action" data-map-column-add="${newIndex}">add</i>
                                </a>
                            </div>
                            ${this.GetTableTemplateColumns(newIndex)}
                        </div>
                    </div>
                </li>`
    }

    private GetSelectTemplateTablesList(): string {
        let newIndex = this.GetNewIndexListTable();
        let selectElement = document.createElement("select");
        this.SetValidationAttributes(selectElement, "Debes seleccionar una tabla");
        selectElement.id = `TablasSistemas_${newIndex}__NombreTabla`;
        selectElement.name = `TablasSistemas[${newIndex}].NombreTabla`;
        selectElement.classList.add("list-table-names");
        this.AddTableOptionsToSelect(selectElement);

        return selectElement.outerHTML;
    }

    private GetTableTemplateColumns(table: number): string {
        return `<div class="card card-default scrollspy border-radius-6 col s12">
                    <div class="card-content p-0">
                        <table data-map-column-list="${table}" class="display responsive" style="width:100%">
                            <thead>
                                <tr>
                                    <th>Col. Archivo</th>
                                    <th>Col. Tabla</th>
                                    <th>Es Llave</th>
                                    <th>Es Nulo</th>
                                    <th>Tipo</th>
                                    <th>Acción</th>
                                </tr>
                            </thead>
                            <tbody>
                                
                            </tbody>
                        </table>
                    </div>
                </div>`
    }

    private GetRowTemplateColumnsTable(table: number, columnaData: TablaSistemaColumna, columnsList: string[]): string {
        let indexColumn = this.GetNewIndexColumnListTable(table);

        return `<tr data-map-column="${indexColumn}">
                    <td>
                        <input type="hidden" name="TablasSistemas[${table}].Columnas.Index" value="${indexColumn}" />
                        <input type="hidden" name="TablasSistemas[${table}].Columnas[${indexColumn}].IndexColumnaArchivo" />
                        <div class="input-field">
                            ${this.GetSelectTemplateColumnsFile(table)}
                        </div>
                    </td>
                    <td>
                        <div class="input-field">
                            ${this.GetSelectTemplateColumnsTable(table, columnaData, columnsList)}
                        </div>
                    </td>
                    <td>
                        <label>
                            <input type="checkbox" name="TablasSistemas[${table}].Columnas[${indexColumn}].Llave" value="${columnaData?.Llave}" ${columnaData?.Llave ? 'checked' : ''} disabled/>
                            <span></span>
                        </label>
                    </td>
                    <td>
                        <label>
                            <input type="checkbox" name="TablasSistemas[${table}].Columnas[${indexColumn}].IsNull" value="${columnaData?.IsNull}" ${columnaData?.IsNull ? 'checked' : ''} disabled /> 
                            <span></span>
                        </label>
                    </td>
                    <td> <input type="text" name="TablasSistemas[${table}].Columnas[${indexColumn}].Tipo" value="${columnaData?.Tipo}" disabled/></td>
                    <td>
                        ${this.GetRemoveColumnButton(columnaData?.IsNull, table)}
                    </td>
                </tr>`
    }

    private GetSelectTemplateColumnsTable(table: number, columnData: TablaSistemaColumna, columnList: string[]): string {
        let column = this.GetNewIndexColumnListTable(table);
        let selectElement = document.createElement("select");
        selectElement.id = `TablasSistemas_${table}__Columnas_${column}__Nombre`;
        selectElement.name = `TablasSistemas[${table}].Columnas[${column}].Nombre`;
        selectElement.classList.add("columns-table-options");
        this.SetValidationAttributes(selectElement, "Columna tabla es obligatorio");
        var firstOption = new Option("Escoge columna", "", (columnData == undefined));
        firstOption.disabled = true;
        selectElement.add(firstOption);
        columnList.forEach((option) => {
            selectElement.options.add(new Option(option, option, (option == columnData?.Nombre)));
        });

        if (columnData) {
            let wrap = document.createElement("div");
            let hidden = document.createElement("input");
            hidden.name = `TablasSistemas[${table}].Columnas[${column}].Nombre`;
            hidden.hidden = true;
            hidden.value = columnData.Nombre;
            hidden.setAttribute("value", columnData.Nombre);
            wrap.appendChild(hidden);
            wrap.appendChild(selectElement);
            return wrap.outerHTML;
        }


        return selectElement.outerHTML + this.GetValidationMessage(selectElement.name);
    }

    private GetSelectTemplateColumnsFile(table: number): string {
        let column = this.GetNewIndexColumnListTable(table);
        let selectElement = document.createElement("select");
        selectElement.id = `TablasSistemas_${table}__Columnas_${column}__ColumnaArchivo`;
        selectElement.name = `TablasSistemas[${table}].Columnas[${column}].ColumnaArchivo`;
        selectElement.classList.add("columns-file-options");
        this.SetValidationAttributes(selectElement, "Columna archivo es obligatorio");

        var firstOption = new Option("Escoge columna", "", true);
        firstOption.disabled = true;
        selectElement.add(firstOption);
        this._step3Model.Paso2Data.Columnas.forEach((option) => selectElement.options.add(new Option(option, option)));
        return selectElement.outerHTML + this.GetValidationMessage(selectElement.name);
    }

    private GetValidationMessage(name: string): string {
        return ` <small>
                    <span class="red-text field-validation-valid" data-valmsg-for="${name}" data-valmsg-replace="true"></span>
                </small>`;
    }

    private SetValidationAttributes(elment: HTMLElement, message: string): void {
        elment.setAttribute("data-val", "true");
        elment.setAttribute("data-val-required", message);
    }

    private GetRemoveColumnButton(remove: boolean, table: number): string {

        if (remove === true || remove == null || remove === null || remove === undefined)
            return `<a class="mb-6 btn-floating waves-effect waves-light purple lightrn-1 btn-small" >
                    <i class="material-icons map-table-action" data-map-column-remove="${table}">remove</i>
                </a>`;
        else
            return "";
    }

    //-- DOM HELPERS -----------------------------------------------------

    private GetNewIndexListTable(): number {
        let mapTables = document.querySelectorAll("li[data-map-table]");
        if (mapTables) {
            let lastMap = (mapTables[mapTables.length - 1] as HTMLLIElement);
            return (parseInt(lastMap.dataset.mapTable) + 1);
        }
        else
            return 0;
    }

    private GetNewIndexColumnListTable(table: number): number {
        let mapColumnTable = document.querySelector<HTMLTableElement>("table[data-map-column-list='" + table + "']");
        let row = mapColumnTable.querySelector<HTMLTableRowElement>("tbody tr:last-child[data-map-column]");
        if (row)
            return parseInt(row.dataset.mapColumn) + 1;
        else
            return 0;
    }

    private AddTableOptionsToSelect(element: HTMLSelectElement) {
        element.innerHTML = '';
        var firstOption = new Option("Selecciona una tabla", "", true, true);
        firstOption.disabled = true;
        element.add(firstOption);
        this._optionsTablas.forEach((option) => element.options.add(new Option(option, option)));
    }

    public Destroy(): void {
        this._collapsibleInstance?.destroy();
        this._selects?.every(s => s.destroy());
        this._optionsTablas = null;
        this._tablasKairos = null;
    }
}
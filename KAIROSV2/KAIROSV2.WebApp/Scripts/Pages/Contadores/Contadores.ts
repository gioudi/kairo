import { HttpFetchService, IMessageResponse, Contador } from '../../Shared';
import { ConfirmModalMessage } from '../../Shared/Components/ConfirmModalMessage';
import { ConfigureDataTable } from '../../Shared/Components/ConfigureDataTable';
import { ContadoresGestionPage } from './GestionContadores';
import { JQValidations } from '../../Shared/Utils/JQValidations';
import { SelectorMenu } from '../../Shared/Utils/SelectorMenu';
import { MaskFormats, IMaskFormat } from '../../Shared/Utils/MaskFormats';
import IMask from "imask";
import { MaskFormatsManager } from '../../Shared/Utils';
import { Page } from '../../Core/Page';

export class ContadoresPage extends Page {

    private Table: DataTables.Api;
    private modalInstance: M.Modal;
    private gestionContadores: ContadoresGestionPage;
    private _maskManager: MaskFormatsManager;

    constructor() {
        super();
        this.BaseUrl =  "/Contadores/Index";
        this._maskManager = new MaskFormatsManager();
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
        super.GetPermissions("/Contadores/ObtenerPermisos");
    }

    private InicializarControls() {
        SelectorMenu.SeleccionEnElMenu();
        this.Table = new ConfigureDataTable().Configure("#data-table-contador", [{ "width": "5%", "targets": 0 }]);
        this.gestionContadores = new ContadoresGestionPage(document.getElementById("contador-modal"));
        this.gestionContadores.onContadorCreado = (contador) => this.AgregarFilaDatatable(contador);
        //this.gestionContadores.onContadorActualizado = (contador) => this.ActualizarFilaDatatable(contador);
        
    }

    private InicializarButtons() {
        var table = document.getElementById("data-table-contador");
        var contadorAdd = document.getElementById("contador-add");

        table.on('click', '.contador-action', event => {
            //if (event.target.matches(".contador-edit"))
            //    this.EditarContador(event.target as HTMLElement);

            if (event.target.matches(".contador-delete"))
                this.BorrarContador(event.target as HTMLElement);
        });
        contadorAdd.addEventListener('click', event => this.CrearContador());
    }

    private async BorrarContador(element: HTMLElement) {
        let contadorId = element.dataset.contadorid;
        var payload = { Contador: contadorId };
        const confirm = new ConfirmModalMessage("Eliminar Contador", "¿Desea eliminar el Contador " + contadorId + "?", "Aceptar", "Cancelar");
        const shouldDelete = await confirm.Confirm();
        if (shouldDelete) {
            const httpService = new HttpFetchService();
            httpService.Post<IMessageResponse>('/Contadores/BorrarContador', payload)
                .then((data) => {
                    if (data.Result)
                        this.BorrarFilaDatatable(element);
                    M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

                }).catch((error) => {
                    M.toast({ html: error, classes: "error" });
                });
        }
    }

    private CrearContador() {
        this.gestionContadores.NuevoContador();
    }

    //private EditarContador(element: HTMLElement): void {
    //    this.gestionContadores.DatosContador(element.dataset.contadorid, false);
    //}

    //Funcion para borrar contador del datatable
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

    // agregar un nuevo contador
    private AgregarFilaDatatable(contador: Contador) {
        let newContador = this.Table.row.add([
            contador.IdContador,
            '<a class="cursor-point ' + this.DisableAction(this.Permisions.Borrar) + '" data-turbolinks="false">' +
            '<i class="material-icons contador-action contador-delete "data-contadorid="' + contador.IdContador + '">delete</i></a>'
        ]);
        (this.Table.row(newContador).node() as HTMLElement).id = contador.IdContador;
        newContador.draw(false);
    }

    // Actualizar un contador
    //private ActualizarFilaDatatable(contador: Contador) {
    //    let row = $("tr#" + contador.IdContador);
    //    var contadoor = this.Table.row(row).data();
    //    contadoor[1] = '<span class="bullet ' + contador.ColorTipo + '"></span>' + contador.Tipo;
    //    this.Table.row(row).data(contadoor);
    //}

    //Obtiene clase disable dependiendo del permiso
    private DisableAction(habilitado: boolean): string {
        return habilitado ? "" : "disable-action";
    }
}

var contadoresPage = new ContadoresPage();


// Called after every non-initial page load
document.addEventListener('turbolinks:render', (e: any) => {
    if (document.URL.indexOf('/Contadores/Index') != -1) {
        if (contadoresPage) {
            contadoresPage.Init();
        }
        else {
            contadoresPage = new ContadoresPage();
        }
        //console.log(e.data.url);
    }
    else
        contadoresPage?.Destroy();
});
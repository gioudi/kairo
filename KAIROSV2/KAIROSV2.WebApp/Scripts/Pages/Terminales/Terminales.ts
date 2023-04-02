import { HttpFetchService, IMessageResponse, Terminal } from '../../Shared';
import { ConfirmModalMessage } from '../../Shared/Components/ConfirmModalMessage';
import { ConfigureDataTable } from '../../Shared/Components/ConfigureDataTable';
import { TerminalesGestionPage } from './GestionTerminales';
import { JQValidations } from '../../Shared/Utils/JQValidations';
import { SelectorMenu } from '../../Shared/Utils/SelectorMenu';
import { IPage } from '../../Shared/Components/IPage';
import { Page } from '../../Core/Page';

export class TerminalesPage extends Page {

    private gestionTerminales: TerminalesGestionPage;

    constructor() {
        super();
        this.BaseUrl = "/Terminales/Index";
        this.Init();
    }

    public Destroy() {
        super.Destroy();
        //this.Table.destroy(false);
    }

    public Init() {
        this.InicializarButtons();
        this.InicializarControls();
        this.GetPermissions("/Terminales/ObtenerPermisos");
    }

    private InicializarControls() {
        SelectorMenu.SeleccionEnElMenu();
        this.gestionTerminales = new TerminalesGestionPage(document.getElementById("terminal-modal"));
        this.gestionTerminales.onTerminalCreado = (terminal) => this.AgregarFilaDatatable(terminal);
        this.gestionTerminales.onTerminalActualizado = (terminal) => this.ActualizarFilaDatatable(terminal);
        JQValidations.MaxFileSizeValidation();
        JQValidations.AllowedExtensionsValidation();
    }

    private InicializarButtons() {
        var card = document.getElementById("kanba");
        var terminalAdd = document.getElementById("terminal-add");

        card.on('click', '.terminal-action', event => {
            if (event.target.matches(".terminal-edit"))
                this.EditarTerminal(event.target as HTMLElement);

            else if (event.target.matches(".terminal-delete"))
                this.BorrarTerminal(event.target as HTMLElement);
        });
        terminalAdd.addEventListener('click', event => this.CrearTerminal());
    }

    private async BorrarTerminal(element: HTMLElement) {
        let terminalId = element.dataset.terminalid;
        let terminalName = element.dataset.terminalname;
        const confirm = new ConfirmModalMessage("Eliminar Terminal", "¿Desea eliminar la Terminal " + terminalName + "?", "Aceptar", "Cancelar");
        const shouldDelete = await confirm.Confirm();
        if (shouldDelete) {
            const httpService = new HttpFetchService();
            httpService.Post<IMessageResponse>('/Terminales/BorrarTerminal', terminalId)
                .then((data) => {
                    if (data.Result)
                        this.BoorarTerminalKanba(element.dataset.terminalid);
                    M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

                }).catch((error) => {
                    M.toast({ html: error, classes: "error" });
                });
        }
    }


    private CrearTerminal() {
        this.gestionTerminales.NuevaTerminal();
    }

    private EditarTerminal(element: HTMLElement): void {
        this.gestionTerminales.DatosTerminal(element.dataset.terminalid, false);
    }

    //Funcion para borrar una terminal
    private BoorarTerminalKanba(idTerminal) {
        var padre = document.getElementById("kanba");
        var card = document.getElementById(idTerminal);
        padre.removeChild(card);
    }

    // agregar una nueva terminal
    private AgregarFilaDatatable(terminal: Terminal) {

    }

    // Actualizar una terminal
    private ActualizarFilaDatatable(terminal: Terminal) {

    }

    //Obtiene clase disable dependiendo del permiso
    private DisableAction(habilitado: boolean): string {
        return habilitado ? "" : "disable-action";
    }
}

var terminalesPage = new TerminalesPage();

// Called after every non-initial page load
document.addEventListener('turbolinks:render', (e: any) => {
    if (document.URL.indexOf('/Terminales/Index') != -1) {
        if (terminalesPage) {
            terminalesPage.Init();
        }
        else {
            terminalesPage = new TerminalesPage();
        }
        //console.log(e.data.url);
    }
    else
        terminalesPage?.Destroy();
});
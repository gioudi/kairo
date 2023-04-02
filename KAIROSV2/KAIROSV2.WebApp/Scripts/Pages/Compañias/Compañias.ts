import { HttpFetchService, IMessageResponse, Compañia } from '../../Shared';
import { ConfirmModalMessage } from '../../Shared/Components/ConfirmModalMessage';
import { CompañiasGestionPage } from './GestionCompañias';
import { JQValidations } from '../../Shared/Utils/JQValidations';
import { SelectorMenu } from '../../Shared/Utils/SelectorMenu';
import { Page } from '../../Core/Page';

export class CompañiasPage extends Page {

    private modalInstance: M.Modal;
    private gestionCompañias: CompañiasGestionPage;

    constructor() {
        super();
        this.BaseUrl = "/Compañias/Index";
        this.Init();
    }

    public Destroy() {
        super.Destroy();
        //this.Table.destroy(false);
    }

    public Init() {
        this.InicializarButtons();
        this.InicializarControls();
        super.GetPermissions("/Compañias/ObtenerPermisos");
    }

    private InicializarControls() {
        SelectorMenu.SeleccionEnElMenu();
        this.gestionCompañias = new CompañiasGestionPage(document.getElementById("compañia-modal"));
        this.gestionCompañias.onCompañiaCreado = (compañia) => this.AgregarCompañiaKanba(compañia);
        this.gestionCompañias.onCompañiaActualizado = (compañia) => this.ActualizarCompañiaKanba(compañia);
        JQValidations.MaxFileSizeValidation();
        JQValidations.AllowedExtensionsValidation();
    }

    private InicializarButtons() {
        var card = document.getElementById("kanba");
        var compañiaAdd = document.getElementById("compañia-add");

        card.on('click', '.compañia-action', event => {
            if (event.target.matches(".compañia-edit"))
                this.EditarCompañia(event.target as HTMLElement);

            else if (event.target.matches(".compañia-delete"))
                this.BorrarCompañia(event.target as HTMLElement);

        });
        compañiaAdd.addEventListener('click', event => this.CrearCompañia());
    }

    private async BorrarCompañia(element: HTMLElement) {
        let compañiaId = element.dataset.compañiaid;
        let compañiaName = element.dataset.compañianame;
        const confirm = new ConfirmModalMessage("Eliminar Compañia", "¿Desea eliminar la Compañia " + compañiaName + "?", "Aceptar", "Cancelar");
        const shouldDelete = await confirm.Confirm();
        if (shouldDelete) {
            const httpService = new HttpFetchService();
            httpService.Post<IMessageResponse>('/Compañias/BorrarCompañia', compañiaId)
                .then((data) => {
                    if (data.Result)
                        this.BorrarCompañiaKanba(compañiaId);
                    M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

                }).catch((error) => {
                    M.toast({ html: error, classes: "error" });
                });
        }
    }

    private CrearCompañia() {
        this.gestionCompañias.NuevaCompañia();
    }

    private EditarCompañia(element: HTMLElement): void {
        this.gestionCompañias.DatosCompañia(element.dataset.compañiaid, false);
    }

    //Funcion para borrar compañia
    private BorrarCompañiaKanba(idCompañia) {
        var padre = document.getElementById("kanba");
        var card = document.getElementById(idCompañia);
        padre.removeChild(card);
    }

    // agregar una nueva Compañia  
    private AgregarCompañiaKanba(compañia: Compañia) {
        let container = document.getElementById("kanba");
        var card = document.createElement("div");
        card.setAttribute('id', compañia.IdCompañia);
        card.classList.add('col');
        card.classList.add('s12');
        card.classList.add('m6');
        card.classList.add('l4');
        card.innerHTML = '<div id="profile-card" class="card animate fadeRight"> ' +
                '<div class="card-image waves-effect waves-block waves-light" style="background:lightgrey;">' +
                '</div>' +
            '<div class="card-content">' +
            '<a class="btn-floating activator btn-move-up waves-effect waves-light accent-2 z-depth-4 right ' + this.DisableAction(this.Permisions.Detalles) + '" style="background-color:#FF6900">' +
                        '<i class="material-icons compañia-action compañia-detail" data-compañiaid="' + compañia.IdCompañia + '" data-compañianame="' + compañia.Nombre + '">visibility</i>' +
                    '</a>' +
            '<a class="btn-floating btn-move-up waves-effect waves-light accent-2 z-depth-4 right ' + this.DisableAction(this.Permisions.Editar) + '" style = "background-color: #162b71">' +
                        '<i class="material-icons compañia-action compañia-edit" data-compañiaid="' + compañia.IdCompañia + '" data-compañianame="' + compañia.Nombre + '">edit</i>' +
            '</a>' +
            '<a class="btn-floating btn-move-up waves-effect waves-light grey accent-2 z-depth-4 right ' + this.DisableAction(this.Permisions.Borrar) + '">' +
                        '<i class="material-icons compañia-action compañia-delete" data-compañiaid="' + compañia.IdCompañia + '" data-compañianame="' + compañia.Nombre + '">delete</i>' +
                    '</a>' +
                    '<p> <i class="material-icons profile-card-i" style="color:#FF6900">apps</i>' + compañia.Nombre + '</p>' +
                    '<p><i class="material-icons profile-card-i" style="color:#FF6900">select_all</i>ID: ' + compañia.CodigoSICOM + '</p>' +
                '</div>' +
                '<div class="card-reveal">' +
                    '<span class="card-title grey-text text-darken-4">' +
                        ''+compañia.Nombre+'<i class="material-icons right">close</i>' +
                    '</span>' +
                    '<p>Id: <b>'+compañia.IdCompañia+'</b></p>' +
                    '<p>Sales Organization: <b>'+compañia.SalesOrganization+'</b></p>' +
                    '<p>Distribution Channel: <b>'+compañia.DistributionChannel+'</b></p>' +
                    '<p>Division: <b>'+compañia.Division+'</b></p>' +
                    '<p>Supplier Type: <b>'+compañia.SupplierType+'</b></p>' +
                    '<p>SICOM: <b>'+compañia.CodigoSICOM+'</b></p>' +
                '</div>';

        container.appendChild(card);
    }
   
    // Actualizar una Compañia
    private ActualizarCompañiaKanba(compañia: Compañia) {
        let card = document.getElementById(compañia.IdCompañia);
        card.innerHTML = '<div id="profile-card" class="card animate fadeRight">' +
                '<div class="card-image waves-effect waves-block waves-light" style="background:lightgrey;">' +
                '</div>' +
            '<div class="card-content">' +
            '<a class="btn-floating activator btn-move-up waves-effect waves-light accent-2 z-depth-4 right ' + this.DisableAction(this.Permisions.Detalles) + '" style="background-color:#FF6900">' +
                        '<i class="material-icons compañia-action compañia-detail" data-compañiaid="' + compañia.IdCompañia + '" data-compañianame="' + compañia.Nombre + '">visibility</i>' +
                    '</a>' +
            '<a class="btn-floating btn-move-up waves-effect waves-light accent-2 z-depth-4 right ' + this.DisableAction(this.Permisions.Editar) + '" style = "background-color: #162b71">' +
                        '<i class="material-icons compañia-action compañia-edit" data-compañiaid="' + compañia.IdCompañia + '" data-compañianame="' + compañia.Nombre + '">edit</i>' +
            '</a>' +
            '<a class="btn-floating btn-move-up waves-effect waves-light grey accent-2 z-depth-4 right ' + this.DisableAction(this.Permisions.Borrar) + '">' +
                        '<i class="material-icons compañia-action compañia-delete" data-compañiaid="' + compañia.IdCompañia + '" data-compañianame="' + compañia.Nombre +'">delete</i>' +
                    '</a>' +
                    '<p> <i class="material-icons profile-card-i" style="color:#FF6900">apps</i>' + compañia.Nombre +'</p>' +
                    '<p><i class="material-icons profile-card-i" style="color:#FF6900">select_all</i>ID: ' + compañia.CodigoSICOM + '</p>' +
                '</div>' +
                '<div class="card-reveal">' +
                    '<span class="card-title grey-text text-darken-4">' +
                        ''+compañia.Nombre+'<i class="material-icons right">close</i>' +
                    '</span>' +
                    '<p>Id: <b>'+compañia.IdCompañia+'</b></p>' +
                    '<p>Sales Organization: <b>'+compañia.SalesOrganization+'</b></p>' +
                    '<p>Distribution Channel: <b>'+compañia.DistributionChannel+'</b></p>' +
                    '<p>Division: <b>'+compañia.Division+'</b></p>' +
                    '<p>Supplier Type: <b>'+compañia.SupplierType+'</b></p>' +
                    '<p>SICOM: <b>'+compañia.CodigoSICOM+'</b></p>' +
                '</div>';
    }

    //Obtiene clase disable dependiendo del permiso
    private DisableAction(habilitado: boolean): string {
        return habilitado ? "" : "disable-action";
    }

}

var compañiasPage = new CompañiasPage();



// Called after every non-initial page load
document.addEventListener('turbolinks:render', (e: any) => {
    if (decodeURI(document.URL).indexOf('/Compañias/Index') != -1) {
        if (compañiasPage) {
            compañiasPage.Init();
        }
        else {
            compañiasPage = new CompañiasPage();
        }
        //console.log(e.data.url);
    }
    else
        compañiasPage?.Destroy();
});
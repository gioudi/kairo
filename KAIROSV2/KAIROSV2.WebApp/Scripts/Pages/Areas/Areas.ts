import { HttpFetchService, IMessageResponse, Area } from '../../Shared';
import { ConfirmModalMessage } from '../../Shared/Components/ConfirmModalMessage';
import { AreasGestionPage } from './GestionAreas';
import { JQValidations } from '../../Shared/Utils/JQValidations';
import { SelectorMenu } from '../../Shared/Utils/SelectorMenu';
import { Page } from '../../Core/Page';

export class AreasPage extends Page {

    private modalInstance: M.Modal;
    private gestionAreas: AreasGestionPage;

    constructor() {
        super();
        this.BaseUrl = "/Areas/Index";
        this.Init();
    }

    public Destroy() {
        super.Destroy();
        //this.Table.destroy(false);
    }

    public Init() {
        this.InicializarButtons();
        this.InicializarControls();
        super.GetPermissions("/Areas/ObtenerPermisos");
    }

    private InicializarControls() {
        SelectorMenu.SeleccionEnElMenu();
        this.gestionAreas = new AreasGestionPage(document.getElementById("area-modal"));
        this.gestionAreas.onAreaCreado = (area) => this.AgregarAreaKanba(area);
        this.gestionAreas.onAreaActualizado = (area) => this.ActualizarAreaKanba(area);
        JQValidations.MaxFileSizeValidation();
        JQValidations.AllowedExtensionsValidation();
    }

    private InicializarButtons() {
        var card = document.getElementById("kanba");
        var areaAdd = document.getElementById("area-add");

        card.on('click', '.area-action', event => {
            if (event.target.matches(".area-edit"))
                this.EditarArea(event.target as HTMLElement);

            else if (event.target.matches(".area-delete"))
                this.BorrarArea(event.target as HTMLElement);

        });
        areaAdd.addEventListener('click', event => this.CrearArea());
    }

    private async BorrarArea(element: HTMLElement) {
        let areaId = element.dataset.areaid;
        let areaName = element.dataset.areaname;
        const confirm = new ConfirmModalMessage("Eliminar Área", "¿Desea eliminar el Área " + areaName + "?", "Aceptar", "Cancelar");
        const shouldDelete = await confirm.Confirm();
        if (shouldDelete) {
            const httpService = new HttpFetchService();
            httpService.Post<IMessageResponse>('/Areas/BorrarArea', areaId)
                .then((data) => {
                    if (data.Result)
                        this.BorrarAreaKanba(areaId);
                    M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });

                }).catch((error) => {
                    M.toast({ html: error, classes: "error" });
                });
        }
    }

    private CrearArea() {
        this.gestionAreas.NuevaArea();
    }

    private EditarArea(element: HTMLElement): void {
        this.gestionAreas.DatosArea(element.dataset.areaid, false);
    }

    //Funcion para borrar area
    private BorrarAreaKanba(idArea) {
        var padre = document.getElementById("kanba");
        var card = document.getElementById(idArea);
        padre.removeChild(card);
    }

    // agregar una nueva area  
    private AgregarAreaKanba(area: Area) {
        let container = document.getElementById("kanba");
        var card = document.createElement("div");
        card.setAttribute('id', area.IdArea);
        card.classList.add('col');
        card.classList.add('s12');
        card.classList.add('m6');
        card.classList.add('l4');
        card.innerHTML ='<div id="profile-card" class="card animate custom-card fadeRight" data-turbolinks="false">' +
            '<div class="card-image waves-effect waves-block waves-light" style="background: lightgrey;">' +
            '<img src="/images/gallery/shutterstock_1106084129.jpg" alt="user bg">'+
            '</div>' +
            '<div class="card-content custom-card-content">' +
            '<a class="btn-floating btn-move-up waves-effect waves-light blue accent-2 z-depth-4 right  ' + this.DisableAction(this.Permisions.Editar) + '" data-turbolinks="false">' +
                '<i class="material-icons area-action area-edit" data-areaid="' + area.IdArea + '" data-areaname="' + area.Nombre + '">edit</i>' +
            '</a>' +
            '<a class="btn-floating btn-move-up waves-effect waves-light red accent-2 z-depth-4 right  ' + this.DisableAction(this.Permisions.Borrar) + '" data-turbolinks="false">' +
                '<i class="material-icons area-action area-delete" data-areaid="' + area.IdArea + '" data-areaname="' + area.Nombre + '">delete</i>' +
            '</a>' +
            '<p><i class="material-icons profile-card-i" style="color:#FF6900">apps</i>' + area.Nombre +'</p>'+
            '<p><i class="material-icons profile-card-i" style="color:#FF6900">select_all</i> ID: '+ area.IdArea +'</p>' +
            '</div>' +
       '</div>';
        container.appendChild(card);
    }
   
    // Actualizar un area
    private ActualizarAreaKanba(area: Area) {
        let card = document.getElementById(area.IdArea);
        card.innerHTML = '<div id="profile-card" class="card animate custom-card fadeRight " data-turbolinks="false">' +
            '<div class="card-image waves-effect waves-block waves-light" style="background: lightgrey;">' +   
            '<img src="~/images/gallery/shutterstock_1106084129.jpg" alt="user bg">' +
            '</div>' +
            '<div class="card-content custom-card-content">' +
            '<a class="btn-floating btn-move-up waves-effect waves-light blue accent-2 z-depth-4 right ' + this.DisableAction(this.Permisions.Editar) + '" data-turbolinks="false">' +
                '<i class="material-icons area-action area-edit" data-areaid="' + area.IdArea + '" data-areaname="' + area.Nombre + '">edit</i>' +
            '</a>' +
            '<a class="btn-floating btn-move-up waves-effect waves-light red accent-2 z-depth-4 right ' + this.DisableAction(this.Permisions.Borrar) + '" data-turbolinks="false">' +
                '<i class="material-icons area-action area-delete" data-areaid="' + area.IdArea + '" data-areaname="' + area.Nombre + '" >delete</i>' +
            '</a>' +
            '<p><i class="material-icons profile-card-i" style="color:#FF6900">apps</i>' + area.Nombre +'</p>'+
            '<p><i class="material-icons profile-card-i" style="color:#FF6900">select_all</i> ID: '+ area.IdArea +'</p>' +
            '</div>' +
       '</div>';
    }

    //Obtiene clase disable dependiendo del permiso
    private DisableAction(habilitado: boolean): string {
        return habilitado ? "" : "disable-action";
    }

}

var areasPage = new AreasPage();



// Called after every non-initial page load
document.addEventListener('turbolinks:render', (e: any) => {
    if (document.URL.indexOf('/Areas/Index') != -1) {
        if (areasPage) {
            areasPage.Init();
        }
        else {
            areasPage = new AreasPage();
        }
        //console.log(e.data.url);
    }
    else
        areasPage?.Destroy();
});
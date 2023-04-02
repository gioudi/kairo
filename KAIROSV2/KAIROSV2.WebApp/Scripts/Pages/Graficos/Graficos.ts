import { HttpFetchService, IMessageResponse, Despacho } from '../../Shared';
import { ConfirmModalMessage } from '../../Shared/Components/ConfirmModalMessage';
import { ConfigureDataTable } from '../../Shared/Components/ConfigureDataTable';

import { JQValidations } from '../../Shared/Utils/JQValidations';
import { SelectorMenu } from '../../Shared/Utils/SelectorMenu';
import { IPage } from '../../Shared/Components/IPage';
import { MaskFormats, IMaskFormat } from '../../Shared/Utils/MaskFormats';
import { DatepickerComponent } from '../../Shared/Components/DatepickerComponent';
import IMask from "imask";
import { MaskFormatsManager } from '../../Shared/Utils';
import PerfectScrollbar from 'perfect-scrollbar';
import { format, parseISO, parseJSON } from 'date-fns';
import dayjs = require('dayjs');
import 'select2';
import 'select2/dist/css/select2.css';
import { Page } from '../../Core/Page';
import { id } from 'date-fns/esm/locale';



export class GraficosPage extends Page {

    private Example: DataTables.Api;
    private _baseUrl = "/ComponentesGraficos";
    private _httpService: HttpFetchService;
   


    constructor() {
        super();  
        this._httpService = new HttpFetchService();
        this.Init();
    }

    public Destroy() {
        super.Destroy();
        this.Example.destroy(false);
       
        
    }

    public Init() {
        this.SortExtention();
        this.InicializarControls();
        
    }

    public SortExtention() {
        $.fn.dataTable.ext.order['dom-checkbox'] = function (settings, col) {
            return this.api().column(col, { order: 'index' }).nodes().map(function (td, i) {
                return $('input', td).prop('checked') ? '1' : '0';
            });
        };
    }

    private async InicializarControls() {
        SelectorMenu.SeleccionEnElMenu();
        console.log("antes de empezar");
        this.Example = new ConfigureDataTable().Configure("#Example", [{ "width": "5%", "targets": 0 }]); ;
        console.log(this.Example);
        console.log("termino");
   
        $("select").select2({ dropdownAutoWidth: true, width: '100%' });
        
        M.Tabs.init(document.querySelectorAll('.tabs'));
       
       
    }



}


var Graficos = new GraficosPage();



// Called after every non-initial page load
document.addEventListener('turbolinks:render', (e: any) => {
    if (document.URL.indexOf('/ComponentesGraficos/Index') != -1) {
        if (Graficos) {
            Graficos.Init();
        }
        else {
            Graficos = new GraficosPage();
        }
        console.log(e.data.url);
    }
    else
        Graficos?.Destroy();
});
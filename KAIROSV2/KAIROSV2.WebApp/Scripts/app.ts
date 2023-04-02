import Turbolinks = require('turbolinks');
import 'dayjs/locale/es-do';
import dayjs = require('dayjs');


export class App {

    constructor() {
        this.Init();
    }

    private Init() {
        Turbolinks.start();
        dayjs.locale('es-do');
        //document.addEventListener("turbolinks:load", () => {
        //    this.DefaultsMaterialize();
        //}, { once: true, },
        //);
    }

    private DefaultsMaterialize() {

        //initialize all modals    
        var elems = document.querySelectorAll('.modal');
        M.Modal.init(elems, {
            dismissible: false,
            opacity: .5,
            inDuration: 300,
            outDuration: 200,
            startingTop: '6%',
            endingTop: '8%'
        });
    }

}

var app = new App();
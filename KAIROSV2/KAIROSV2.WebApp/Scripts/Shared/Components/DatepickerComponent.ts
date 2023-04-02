export class DatepickerComponent {

    public GetInstances2(selector: string, options?: Partial<M.DatepickerOptions>) {
        let elements = document.querySelectorAll(selector);
        let instances = M.Datepicker.init(elements, this.ConfigBasics(options));
        return instances[0];
    }

    private ConfigBasics(options?: Partial<M.DatepickerOptions>) {
        if (!options) {
            options = {
                format: 'dd/mmm/yyyy',
                container: document.body,
                autoClose: true,
                i18n: {
                    cancel: 'Cancelar',
                    clear: 'Limpiar',
                    done: 'Aceptar',
                    months: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                    monthsShort: ['ene', 'feb', 'mar', 'abr', 'may', 'jun', 'jul', 'ago', 'sep', 'oct', 'nov', 'dic'],
                    weekdays: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sabado'],
                    weekdaysShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
                    weekdaysAbbrev: ['D', 'L', 'M', 'M', 'J', 'V', 'S']
                },
            };
        }
        else {
            options.format = 'dd/mmm/yyyy';
            options.container = document.body;
            options.autoClose = true;
            options.i18n = {
                cancel: 'Cancelar',
                clear: 'Limpiar',
                done: 'Aceptar',
                months: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthsShort: ['ene', 'feb', 'mar', 'abr', 'may', 'jun', 'jul', 'ago', 'sep', 'oct', 'nov', 'dic'],
                weekdays: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sabado'],
                weekdaysShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
                weekdaysAbbrev: ['D', 'L', 'M', 'M', 'J', 'V', 'S']
            }
        }

        return options;
    }

    public GetInstances(selector: string, onSelect?: (newDate: Date) => void, onClose?: () => void, onDraw?: () => void) {
        let elements = document.querySelectorAll(selector);
        let instances = M.Datepicker.init(elements, {
            format: 'dd/mmm/yyyy',
            container: document.body,
            autoClose: true,
            i18n: {
                cancel: 'Cancelar',
                clear: 'Limpiar',
                done: 'Aceptar',
                months: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthsShort: ['ene', 'feb', 'mar', 'abr', 'may', 'jun', 'jul', 'ago', 'sep', 'oct', 'nov', 'dic'],
                weekdays: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sabado'],
                weekdaysShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab']
            },            
            onSelect: function (selectedDate) {
                if (onSelect !== undefined) { onSelect(selectedDate) }
            },
            onClose: function () {
                if (onClose !== undefined) { onClose() }
            },
        });

        return instances;
    };
    public GetInstances3(selector: string, onSelect?: (newDate: Date) => void, onClose?: () => void) {
        let elements = document.querySelectorAll(selector);
        let instances = M.Datepicker.init(elements, {
            format: 'yyyy/mm/dd',
            container: document.body,
            autoClose: true,
            i18n: {
                cancel: 'Cancelar',
                clear: 'Limpiar',
                done: 'Aceptar',
                months: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                weekdays: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sabado'],
                weekdaysShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab']
            },
            onSelect: function (selectedDate) {
                if (onSelect !== undefined) { onSelect(selectedDate) }
            },
            onClose: function () {
                if (onClose !== undefined) { onClose() }
            },
        });

        return instances;
    }

}
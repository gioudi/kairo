"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.LineasGestionPage = void 0;
var Shared_1 = require("../../Shared");
var Linea_1 = require("../../Shared/Models/Linea");
var LineasGestionPage = /** @class */ (function () {
    //CONSTRUCTOR
    function LineasGestionPage(modalBase) {
        //CAMPOS
        this._baseUrl = "/Lineas";
        this._httpService = new Shared_1.HttpFetchService();
        this._modalBase = modalBase;
        this.InicializarModal();
    }
    //METODOS
    //Inicializar control modal
    LineasGestionPage.prototype.InicializarModal = function () {
        this._modalInstance = M.Modal.init(this._modalBase, {
            dismissible: false,
            opacity: .5,
            inDuration: 300,
            outDuration: 200,
            startingTop: '6%',
            endingTop: '8%'
        });
    };
    //Creación de nueva línea
    LineasGestionPage.prototype.NuevaLinea = function () {
        var _this = this;
        this._httpService.Post(this._baseUrl + "/NuevaLinea", null, false)
            .then(function (data) {
            _this._modalBase.innerHTML = data;
            _this.InicializarFormLinea(true);
        });
        this._modalInstance.open();
    };
    //Edición y detalles de línea
    LineasGestionPage.prototype.DatosLinea = function (lineaId, lectura) {
        var _this = this;
        var payload = { IdLinea: lineaId, lectura: lectura };
        this._lineaIdActualizar = lineaId;
        this._httpService.Post(this._baseUrl + "/DatosLinea", payload, false)
            .then(function (data) {
            _this._modalBase.innerHTML = data;
            _this.InicializarFormLinea(false);
        });
        this._modalInstance.open();
    };
    //Inicializar controles de formulario
    LineasGestionPage.prototype.InicializarFormLinea = function (modoCreacion) {
        var _this = this;
        M.updateTextFields();
        this._formLinea = document.querySelector("#linea-form");
        this._formData = new FormData(this._formLinea);
        $(this._formLinea).removeData("validator").removeData("unobtrusiveValidation"); // Elimina datos de validaciones en el Form
        $.validator.unobtrusive.parse(this._formLinea); // agrega la validación al form.
        document.getElementById("linea-modal-cancel").addEventListener('click', function (ev) { return _this.CerrarModal(); });
        this._formLinea.onsubmit = function (e) {
            e.preventDefault(); // evita la acción de formulario predeterminada, para envíos de formularios Ajax
            if ($(_this._formLinea).valid()) { // Comprobación si es valido el formulario
                _this._formData = new FormData(_this._formLinea);
                //formData.forEach((value, key) => { console.log(key + ": " + value) });
                if (modoCreacion)
                    _this.GuardarLinea();
                else
                    _this.ActualizarLinea();
            }
            return false; // prevent reload
        };
    };
    //Guardar línea
    LineasGestionPage.prototype.GuardarLinea = function () {
        var _this = this;
        var _a;
        var token = (_a = this._formData.get("RequestVerificationToken")) === null || _a === void 0 ? void 0 : _a.toString();
        this._httpService.PostForm(this._baseUrl + "/CrearLinea", this._formData, token)
            .then(function (data) {
            if (data.Result) {
                _this.LimpiarFormulario();
                if (_this.onLineaCreado)
                    _this.onLineaCreado(_this.ExtraerLinea());
            }
            M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });
        })
            .catch(function (err) { return console.log(err); });
    };
    //Actualizar línea
    LineasGestionPage.prototype.ActualizarLinea = function () {
        var _this = this;
        var _a;
        var token = (_a = this._formData.get("RequestVerificationToken")) === null || _a === void 0 ? void 0 : _a.toString();
        this._formData.set("IdLinea", this._lineaIdActualizar);
        this._httpService.PostForm(this._baseUrl + "/ActualizarLinea", this._formData, token)
            .then(function (data) {
            if (data.Result) {
                if (_this.onLineaActualizado)
                    _this.onLineaActualizado(_this.ExtraerLinea());
                _this.LimpiarFormulario();
                _this.CerrarModal();
            }
            M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });
        })
            .catch(function (err) { return console.log(err); });
    };
    //Extraer línea de formulario
    LineasGestionPage.prototype.ExtraerLinea = function () {
        var _a, _b, _c, _d, _e, _f, _g;
        var linea = new Linea_1.Linea();
        linea.Linea = (_a = this._formData.get("IdLinea")) === null || _a === void 0 ? void 0 : _a.toString();
        linea.Terminal = (_b = this._formData.get("Terminal")) === null || _b === void 0 ? void 0 : _b.toString();
        linea.Producto = (_c = this._formData.get("Producto")) === null || _c === void 0 ? void 0 : _c.toString();
        linea.Estado = (_d = this._formData.get("Estado")) === null || _d === void 0 ? void 0 : _d.toString();
        linea.Volumen = +((_e = this._formData.get("Volumen")) === null || _e === void 0 ? void 0 : _e.toString());
        linea.Densidad = +((_f = this._formData.get("Densidad")) === null || _f === void 0 ? void 0 : _f.toString());
        linea.Observaciones = (_g = this._formData.get("Observaciones")) === null || _g === void 0 ? void 0 : _g.toString();
        return linea;
    };
    //Reset de formulario
    LineasGestionPage.prototype.LimpiarFormulario = function () {
        this._formLinea.reset();
        M.updateTextFields(); // reinicializa las etiquetas (materialize), para inputs dinámicos
    };
    //Cerrar control modal
    LineasGestionPage.prototype.CerrarModal = function () {
        this._modalInstance.close();
        this._formLinea.parentNode.removeChild(this._formLinea); // Elimina el elemento hijo del formulario
        this._lineaIdActualizar = "";
    };
    return LineasGestionPage;
}());
exports.LineasGestionPage = LineasGestionPage;
//# sourceMappingURL=GestionLineas.js.map
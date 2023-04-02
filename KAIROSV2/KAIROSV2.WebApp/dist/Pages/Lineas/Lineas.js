"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.LineasPage = void 0;
var Shared_1 = require("../../Shared");
var ConfirmModalMessage_1 = require("../../Shared/Components/ConfirmModalMessage");
var ConfigureDataTable_1 = require("../../Shared/Components/ConfigureDataTable");
var GestionLineas_1 = require("./GestionLineas");
var JQValidations_1 = require("../../Shared/Utils/JQValidations");
var LineasPage = /** @class */ (function () {
    //CONSTRUCTOR
    function LineasPage() {
        this.Init();
    }
    //METODOS
    LineasPage.prototype.Init = function () {
        this.InicializarButtons();
        this.InicializarControls();
    };
    LineasPage.prototype.Destroy = function () {
        this.Table.destroy(false);
    };
    LineasPage.prototype.InicializarControls = function () {
        var _this = this;
        this.Table = new ConfigureDataTable_1.ConfigureDataTable().Configure("#data-table-lista");
        this.gestionLineas = new GestionLineas_1.LineasGestionPage(document.getElementById("lista-modal"));
        this.gestionLineas.onLineaCreado = function (linea) { return _this.AgregarFilaDatatable(linea); };
        this.gestionLineas.onLineaActualizado = function (linea) { return _this.ActualizarFilaDatatable(linea); };
        JQValidations_1.JQValidations.MaxFileSizeValidation();
        JQValidations_1.JQValidations.AllowedExtensionsValidation();
    };
    LineasPage.prototype.InicializarButtons = function () {
        var _this = this;
        var table = document.getElementById("data-table-lista");
        var lineaAdd = document.getElementById("linea-add");
        table.on('click', '.linea-action', function (event) {
            if (event.target.matches(".linea-edit"))
                _this.EditarLinea(event.target);
            else if (event.target.matches(".linea-delete"))
                _this.BorrarLinea(event.target);
            //else if (event.target.matches(".linea-detail"))
            //    this.DetallesConductor(event.target as HTMLElement);
        });
        lineaAdd.addEventListener('click', function (event) { return _this.CrearLinea(); });
    };
    LineasPage.prototype.EditarLinea = function (element) {
        console.log(element);
        this.gestionLineas.DatosLinea(element.dataset.lineaid, true);
    };
    LineasPage.prototype.BorrarLinea = function (element) {
        return __awaiter(this, void 0, void 0, function () {
            var lineId, confirm, shouldDelete, httpService;
            var _this = this;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        lineId = element.dataset.lineaid;
                        confirm = new ConfirmModalMessage_1.ConfirmModalMessage("Eliminar Línea", "Desea eliminar la Línea " + lineId, "Aceptar", "Cancelar");
                        return [4 /*yield*/, confirm.Confirm()];
                    case 1:
                        shouldDelete = _a.sent();
                        if (shouldDelete) {
                            httpService = new Shared_1.HttpFetchService();
                            httpService.Post('/Lineas/BorrarLinea', lineId) //.toString()
                                .then(function (data) {
                                if (data.Result)
                                    _this.BorrarFilaDatatable(element);
                                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });
                            }).catch(function (error) {
                                M.toast({ html: error, classes: "error" });
                            });
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    //private DetallesConductor(element: HTMLElement): void {
    //    this.gestionLineas.DatosLinea(element.dataset.lineaid, true);
    //}
    LineasPage.prototype.CrearLinea = function () {
        this.gestionLineas.NuevaLinea();
    };
    //private async BorrarConductor2(element: HTMLElement) {
    //    let lineId = element.dataset.lineaid;
    //    const confirm = new ConfirmModalMessage("Eliminar Conductor", "Desea eliminar el Conductor " + lineId, "Aceptar", "Cancelar");
    //    const shouldDelete = await confirm.Confirm();
    //    if (shouldDelete) {
    //        const httpService = new HttpFetchService();
    //        httpService.Post<IMessageResponse>('/Conductores/BorrarConductor', parseInt(lineId))
    //            .then((data) => {
    //                if (data.Result)
    //                    this.BorrarFilaDatatable(element);
    //                M.toast({ html: data.Message, classes: (data.Result) ? "succes" : "error" });
    //            }).catch((error) => {
    //                M.toast({ html: error, classes: "error" });
    //            });
    //    }
    //}
    //Función para borrar línea del datatable
    LineasPage.prototype.BorrarFilaDatatable = function (element) {
        var $tr = $(element).closest("tr");
        if ($tr.prev().hasClass("parent")) {
            var row_1 = this.Table.row($tr.prev());
            row_1.remove();
            row_1.draw();
        }
        var row = this.Table.row($tr);
        row.remove();
        row.draw();
    };
    // agregar una nueva línea
    LineasPage.prototype.AgregarFilaDatatable = function (linea) {
        this.Table.row.add([
            linea.Terminal,
            linea.Linea,
            linea.Producto,
            linea.Estado,
            linea.Volumen,
            linea.Densidad,
            linea.Observaciones,
            '<a href="#" data-turbolinks="false"><i class="material-icons linea-action linea-edit" data-lineaid="' + linea.Linea + '">edit</i></a> ' +
                '<a href="#" data-turbolinks="false"><i class="material-icons linea-action linea-delete" data-lineaid="' + linea.Linea + '">delete</i></a> '
        ]).draw(false);
    };
    // Actualizar una línea
    LineasPage.prototype.ActualizarFilaDatatable = function (linea) {
        var row = $("tr#" + linea.Linea);
        var cond = this.Table.row(row).data();
        cond[0] = linea.Terminal;
        cond[1] = linea.Linea;
        cond[2] = linea.Producto;
        cond[3] = linea.Estado;
        cond[4] = linea.Volumen;
        cond[5] = linea.Densidad;
        cond[6] = linea.Observaciones;
        this.Table.row(row).data(cond);
    };
    return LineasPage;
}());
exports.LineasPage = LineasPage;
var lineasPage = new LineasPage();
// Called after every non-initial page load
document.addEventListener('turbolinks:render', function (e) {
    if (document.URL.indexOf('/Lineas/Index') != -1) {
        if (lineasPage) {
            lineasPage.Init();
        }
        else {
            lineasPage = new LineasPage();
        }
    }
    else {
        lineasPage.Destroy();
    }
});
//# sourceMappingURL=Lineas.js.map
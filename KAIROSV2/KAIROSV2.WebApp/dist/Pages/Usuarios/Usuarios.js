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
exports.UsuariosPage = void 0;
var Shared_1 = require("../../Shared");
var ConfirmModalMessage_1 = require("../../Shared/Components/ConfirmModalMessage");
var ConfigureDataTable_1 = require("../../Shared/Components/ConfigureDataTable");
var GestionUsuarios_1 = require("./GestionUsuarios");
var JQValidations_1 = require("../../Shared/Utils/JQValidations");
var UsuariosPage = /** @class */ (function () {
    function UsuariosPage() {
        this.Init();
    }
    UsuariosPage.prototype.Destroy = function () {
        this.Table.destroy(false);
        //this.Table.rows().invalidate().draw();
    };
    UsuariosPage.prototype.Init = function () {
        this.InicializarButtons();
        this.InicializarControls();
    };
    UsuariosPage.prototype.InicializarControls = function () {
        var _this = this;
        this.Table = new ConfigureDataTable_1.ConfigureDataTable().Configure("#data-table-usuario", [{ "width": "5%", "targets": 0 }]);
        this.gestionUsuarios = new GestionUsuarios_1.UsuariosGestionPage(document.getElementById("user-modal"));
        this.gestionUsuarios.onUsuarioCreado = function (usuario) { return _this.AgregarFilaDatatable(usuario); };
        this.gestionUsuarios.onUsuarioActualizado = function (usuario) { return _this.ActualizarFilaDatatable(usuario); };
        JQValidations_1.JQValidations.MaxFileSizeValidation();
        JQValidations_1.JQValidations.AllowedExtensionsValidation();
    };
    UsuariosPage.prototype.InicializarButtons = function () {
        var _this = this;
        var table = document.getElementById("data-table-usuario");
        var userAdd = document.getElementById("user-add");
        table.on('click', '.user-action', function (event) {
            if (event.target.matches(".user-edit"))
                _this.EditarUsuario(event.target);
            else if (event.target.matches(".user-delete"))
                _this.BorrarUsuario(event.target);
            else if (event.target.matches(".user-detail"))
                _this.DetallesUsuario(event.target);
        });
        userAdd.addEventListener('click', function (event) { return _this.CrearUsuario(); });
    };
    UsuariosPage.prototype.BorrarUsuario = function (element) {
        return __awaiter(this, void 0, void 0, function () {
            var userId, confirm, shouldDelete, httpService;
            var _this = this;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        userId = element.dataset.userid;
                        confirm = new ConfirmModalMessage_1.ConfirmModalMessage("Eliminar Usuario", "¿Desea eliminar el Usuario " + userId + "?", "Aceptar", "Cancelar");
                        return [4 /*yield*/, confirm.Confirm()];
                    case 1:
                        shouldDelete = _a.sent();
                        if (shouldDelete) {
                            httpService = new Shared_1.HttpFetchService();
                            httpService.Post('/Usuarios/BorrarUsuario', userId)
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
    UsuariosPage.prototype.CrearUsuario = function () {
        this.gestionUsuarios.NuevoUsuario();
    };
    UsuariosPage.prototype.EditarUsuario = function (element) {
        console.log(element);
        this.gestionUsuarios.DatosUsuario(element.dataset.userid, false);
    };
    UsuariosPage.prototype.DetallesUsuario = function (element) {
        this.gestionUsuarios.DatosUsuario(element.dataset.userid, true);
    };
    //Funcion para borrar usuario del datatable
    UsuariosPage.prototype.BorrarFilaDatatable = function (element) {
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
    // agregar un nuevo usuario
    UsuariosPage.prototype.AgregarFilaDatatable = function (usuario) {
        var image = usuario.Imagen.name;
        console.log(image);
        if (usuario.Imagen.name != "") {
            this.Table.row.add([
                '<span class="avatar-contact avatar-online circle"><img src="' + URL.createObjectURL(usuario.Imagen) + '" alt="avatar"></span>',
                usuario.IdUsuario,
                usuario.Nombre,
                usuario.Rol,
                usuario.Email,
                usuario.Telefono,
                '<a href="#" data-turbolinks="false"><i class="material-icons user-action user-edit" data-userid="' + usuario.IdUsuario + '">edit</i></a> ' +
                    '<a href="#" data-turbolinks="false"><i class="material-icons user-action user-delete" data-userid="' + usuario.IdUsuario + '">delete</i></a> ' +
                    '<a href="#" data-turbolinks="false"><i class="material-icons user-action user-detail" data-userid="' + usuario.IdUsuario + '">remove_red_eye</i></a>'
            ]).draw(false);
        }
        else {
            this.Table.row.add([
                '<span class="avatar-contact avatar-online circle"><img src="/images/avatar/account_circle-black-48dp.svg" alt="avatar"></span>',
                usuario.IdUsuario,
                usuario.Nombre,
                usuario.Rol,
                usuario.Email,
                usuario.Telefono,
                '<a href="#" data-turbolinks="false"><i class="material-icons user-action user-edit" data-userid="' + usuario.IdUsuario + '">edit</i></a> ' +
                    '<a href="#" data-turbolinks="false"><i class="material-icons user-action user-delete" data-userid="' + usuario.IdUsuario + '">delete</i></a> ' +
                    '<a href="#" data-turbolinks="false"><i class="material-icons user-action user-detail" data-userid="' + usuario.IdUsuario + '">remove_red_eye</i></a>'
            ]).draw(false);
        }
    };
    // Actualizar un usuario
    UsuariosPage.prototype.ActualizarFilaDatatable = function (usuario) {
        var row = $("tr#" + usuario.IdUsuario);
        var user = this.Table.row(row).data();
        if (usuario.Imagen.name != "") {
            user[0] = '<span class="avatar-contact avatar-online"><img src="' + URL.createObjectURL(usuario.Imagen) + '" alt="avatar"></span>';
        }
        else {
            user[0] = '<span class="avatar-contact avatar-online"><img src="/images/avatar/account_circle-black-48dp.svg" alt="avatar"></span>';
        }
        user[3] = usuario.Rol;
        this.Table.row(row).data(user);
    };
    return UsuariosPage;
}());
exports.UsuariosPage = UsuariosPage;
var usuariosPage = new UsuariosPage();
// Called after every non-initial page load
document.addEventListener('turbolinks:render', function (e) {
    if (document.URL.indexOf('/Usuarios/Index') != -1) {
        if (usuariosPage) {
            usuariosPage.Init();
        }
        else {
            usuariosPage = new UsuariosPage();
        }
        //console.log(e.data.url);
    }
    else
        usuariosPage === null || usuariosPage === void 0 ? void 0 : usuariosPage.Destroy();
});
//# sourceMappingURL=Usuarios.js.map
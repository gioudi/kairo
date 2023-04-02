"use strict";
var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    Object.defineProperty(o, k2, { enumerable: true, get: function() { return m[k]; } });
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __exportStar = (this && this.__exportStar) || function(m, exports) {
    for (var p in m) if (p !== "default" && !Object.prototype.hasOwnProperty.call(exports, p)) __createBinding(exports, m, p);
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.lineas = void 0;
var GestionLineas_1 = require("./GestionLineas");
var Lineas_1 = require("./Lineas");
exports.lineas = [GestionLineas_1.LineasGestionPage, Lineas_1.LineasPage];
__exportStar(require("./GestionLineas"), exports);
__exportStar(require("./Lineas"), exports);
//# sourceMappingURL=index.js.map
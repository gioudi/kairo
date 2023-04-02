import { ProductoTipo } from "./ProductoTipo";

export class Producto {
    IdProducto: string;
    NombreCorto: string;
    NombreERP: string;
    Icono: string;
    Estado: string;
    Tipo: string;
    Clase: string;
    SICOM: string;
    IdTipoNavigation: ProductoTipo;
}
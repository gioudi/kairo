export class TablasSistema {
    NombreTabla: string;
    Columnas: TablaSistemaColumna[];
}

export class TablaSistemaColumna {
    Nombre: string;
    Llave: boolean;
    IsNull: boolean;
    Tipo: string;
    Longitud: number;
    IndexColumnaArchivo: number;
    ColumnaArchivo: string;
}
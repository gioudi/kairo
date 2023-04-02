export class MapeoArchivos {
    Paso1Data: Paso1Mapeo;
    Paso2Data: Paso2Mapeo;
}

export class Paso1Mapeo {
    IdMapeo: string;
    Descripcion: string;
    Separador: string;
    OtroCaracter: string;
    Archivo: File;
}

export class Paso2Mapeo {
    Columnas: string[];
    TableStep2: DataTables.Api;
}
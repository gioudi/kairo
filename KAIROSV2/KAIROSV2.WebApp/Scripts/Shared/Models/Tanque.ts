import { TanquesEstado } from "./TanquesEstado";

export class Tanque {
    IconoProducto: string;
    IconoColor: string;
    Tanque: string;
    Producto: string;
    Terminal: string;
    IdTerminal: string;
    ClaseTanque: string;
    ClaseColor: string;
    Estado: string;
    EstadoColor: string;

    TipoTanque: string;
    CapacidadNominal: number;
    CapacidadOperativa: number;
    VolumenNoBombeable: number;
    AlturaMaximaAforo: number;
    AforadoPor: string;
    FechaAforo: Date;
    PantallaFlotante: string;
    Observaciones: string;
    DensidadAforo: number;
    GalonesPorGrado: number;
    NivelCorreccionInicial: number;
    NivelCorreccionFinal: number;
    
}
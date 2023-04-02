using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TRecibosMedidaTanque
    {
        public long IdRecibo { get; set; }
        public string DocumentoRecibo { get; set; }
        public bool ReciboConAgua { get; set; }
        public bool PantallaFlotante { get; set; }
        public bool SalidaDuranteRecibo { get; set; }
        public double H1InicialProducto { get; set; }
        public double H2InicialProducto { get; set; }
        public double H3InicialProducto { get; set; }
        public double HInicialProductoUnificado { get; set; }
        public double H1FinalProducto { get; set; }
        public double H2FinalProducto { get; set; }
        public double H3FinalProducto { get; set; }
        public double HFinalProductoUnificado { get; set; }
        public double VolumenBrutoInicial { get; set; }
        public double VolumenBrutoFinal { get; set; }
        public double VolumenBrutoTotal { get; set; }
        public double VolumenNetoInicial { get; set; }
        public double VolumenNetoFinal { get; set; }
        public double VolumenNetoTotal { get; set; }
        public double TemperaturaPromedioInicial { get; set; }
        public double TemperaturaPromedioFinal { get; set; }
        public double DensidadInicial { get; set; }
        public double DensidadFinal { get; set; }
        public double FactorCorreccionInicial { get; set; }
        public double FactorCoreccionFinal { get; set; }
        public double VolumenVariacionTransito { get; set; }
        public double PorcentajeVariacionTransito { get; set; }
        public string SistemaProveedor { get; set; }
        public string Observaciones { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TRecibosBase IdReciboNavigation { get; set; }
        public virtual TRecibosMedidaTanqueAgua TRecibosMedidaTanqueAgua { get; set; }
        public virtual TRecibosMedidaTanquePantallaFlotante TRecibosMedidaTanquePantallaFlotante { get; set; }
        public virtual TRecibosSalidasDuranteBombeo TRecibosSalidasDuranteBombeo { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TRecibosFacturacion
    {
        public long IdRecibo { get; set; }
        public string DocumentoProveedor { get; set; }
        public string IdProveedor { get; set; }
        public double VolumenFacturadoBruto { get; set; }
        public double VolumenFacturadoNeto { get; set; }
        public string IdCompañiaFacturar { get; set; }
        public double TemperaturaRecibo { get; set; }
        public double DensidadRecibo { get; set; }
        public string Observaciones { get; set; }
        public int VariacionTransitoBruta { get; set; }
        public int VariacionTransitoNeta { get; set; }
        public int VolumenFacturadoBrutoBarriles { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TCompañia IdCompañiaFacturarNavigation { get; set; }
        public virtual TProveedor IdProveedorNavigation { get; set; }
        public virtual TRecibosBase IdReciboNavigation { get; set; }
    }
}

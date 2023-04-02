using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TRecibosContador
    {
        public long IdRecibo { get; set; }
        public string IdContador { get; set; }
        public string IdCompañia { get; set; }
        public string IdProducto { get; set; }
        public double VolumenContadorBruto { get; set; }
        public double VolumenContadorNeto { get; set; }
        public double VolumenVariacionTransito { get; set; }
        public double PorcentajeVariacionTransito { get; set; }
        public double? DensidadMuestra { get; set; }
        public double? TemperaturaPromedioMuestra { get; set; }
        public double FactorCorreccion { get; set; }
        public double VolumenOrdenado { get; set; }
        public double VolumenDespachado { get; set; }
        public string Shipment { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TCompañia IdCompañiaNavigation { get; set; }
        public virtual TContador IdContadorNavigation { get; set; }
        public virtual TProducto IdProductoNavigation { get; set; }
        public virtual TRecibosBase IdReciboNavigation { get; set; }
        public virtual TRecibosContadorAlcohol TRecibosContadorAlcohol { get; set; }
        public virtual TRecibosContadorMezclado TRecibosContadorMezclado { get; set; }
    }
}

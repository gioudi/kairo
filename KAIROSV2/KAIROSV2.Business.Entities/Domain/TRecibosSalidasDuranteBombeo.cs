using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TRecibosSalidasDuranteBombeo
    {
        public long IdRecibo { get; set; }
        public string IdContador { get; set; }
        public double VolumenSalidaBruto { get; set; }
        public double VolumenSalidaNeto { get; set; }
        public double Temperatura { get; set; }
        public double Densidad { get; set; }
        public double FactorCorreccion { get; set; }
        public string Observaciones { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TRecibosMedidaTanque IdReciboNavigation { get; set; }
    }
}

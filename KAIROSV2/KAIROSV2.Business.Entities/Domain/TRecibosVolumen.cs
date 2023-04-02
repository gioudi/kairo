using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TRecibosVolumen
    {
        public long IdRecibo { get; set; }
        public double TemperaturaRecibo { get; set; }
        public double VolumenBruto { get; set; }
        public double VolumenNeto { get; set; }
        public double Densidad { get; set; }
        public double VolumenRemisionado { get; set; }
        public string PlacaTrailer { get; set; }
        public string IdContador { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TContador IdContadorNavigation { get; set; }
        public virtual TRecibosBase IdReciboNavigation { get; set; }
    }
}

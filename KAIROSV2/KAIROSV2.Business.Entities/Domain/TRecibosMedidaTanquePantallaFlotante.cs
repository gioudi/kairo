using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TRecibosMedidaTanquePantallaFlotante
    {
        public long IdRecibo { get; set; }
        public double DensidadObservadaInicial { get; set; }
        public double DensidadObservadaFinal { get; set; }
        public double VolumenBrutoEfectoPantallaInicial { get; set; }
        public double VolumenBrutoEfectoPantallaFinal { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TRecibosMedidaTanque IdReciboNavigation { get; set; }
    }
}

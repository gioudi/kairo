using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TRecibosContadorAlcohol
    {
        public long IdRecibo { get; set; }
        public double PorcentajeAlcohol { get; set; }
        public double PorcentajeAgua { get; set; }
        public double PorcentajeDesnaturalizante { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TRecibosContador IdReciboNavigation { get; set; }
    }
}

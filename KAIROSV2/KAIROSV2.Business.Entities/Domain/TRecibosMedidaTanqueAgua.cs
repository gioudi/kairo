using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TRecibosMedidaTanqueAgua
    {
        public long IdRecibo { get; set; }
        public double VolumenInicialBrutoAgua { get; set; }
        public double VolumenFinalBrutoAgua { get; set; }
        public double VolumenBrutoAgua { get; set; }
        public int H1InicialAgua { get; set; }
        public int H2InicialAgua { get; set; }
        public int H3InicialAgua { get; set; }
        public int HInicialAguaUnificado { get; set; }
        public int H1FinalAgua { get; set; }
        public int H2FinalAgua { get; set; }
        public int H3FinalAgua { get; set; }
        public int HFinalAguaUnificado { get; set; }
        public int VolumenInicialBrutoSinAgua { get; set; }
        public int VolumenFinalBrutoSinAgua { get; set; }
        public int VolumenTotalBrutoSinAgua { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TRecibosMedidaTanque IdReciboNavigation { get; set; }
    }
}

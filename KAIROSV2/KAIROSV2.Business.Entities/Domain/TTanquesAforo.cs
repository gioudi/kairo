using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TTanquesAforo
    {
        public string IdTanque { get; set; }
        public string IdTerminal { get; set; }
        public double Nivel { get; set; }
        public double Volumen { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TTanque IdT { get; set; }
    }
}

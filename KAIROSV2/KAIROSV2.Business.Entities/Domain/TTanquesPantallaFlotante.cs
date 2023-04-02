using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TTanquesPantallaFlotante
    {
        public string IdTanque { get; set; }
        public string IdTerminal { get; set; }
        public double DensidadAforo { get; set; }
        public double GalonesPorGrado { get; set; }
        public int NivelCorreccionInicial { get; set; }
        public int NivelCorreccionFinal { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }
        public virtual TTanque IdTanqueNavigation { get; set; }
        
    }
}

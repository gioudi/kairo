using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TTanquesAforoResponsable
    {
        public int IdAforo { get; set; }
        public string IdTanque { get; set; }
        public string IdTerminal { get; set; }
        public string Responsable { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }

        public virtual TTanque IdT { get; set; }
    }
}

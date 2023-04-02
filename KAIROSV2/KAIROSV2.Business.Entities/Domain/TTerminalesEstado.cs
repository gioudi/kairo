using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TTerminalesEstado
    {
        public TTerminalesEstado()
        {
            TTerminales = new HashSet<TTerminal>();
        }

        public int IdEstado { get; set; }
        public string Descripcion { get; set; }
        public string ColorHex { get; set; }
        public string Icono { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }

        public virtual ICollection<TTerminal> TTerminales { get; set; }
    }
}

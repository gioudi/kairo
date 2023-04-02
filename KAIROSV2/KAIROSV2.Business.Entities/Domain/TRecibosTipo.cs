using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TRecibosTipo
    {
        public TRecibosTipo()
        {
            TRecibosBases = new HashSet<TRecibosBase>();
        }

        public int IdTipo { get; set; }
        public string Descripcion { get; set; }
        public string ColorHex { get; set; }
        public string Icono { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }

        public virtual ICollection<TRecibosBase> TRecibosBases { get; set; }
    }
}

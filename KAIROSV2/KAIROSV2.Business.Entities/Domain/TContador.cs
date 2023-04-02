using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TContador
    {
        public TContador()
        {
            TRecibosContadorMezclados = new HashSet<TRecibosContadorMezclado>();
            TRecibosContadors = new HashSet<TRecibosContador>();
            TRecibosVolumen = new HashSet<TRecibosVolumen>();
            TTerminalContador = new HashSet<TTerminalContador>();
        }

        public string IdContador { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual ICollection<TTerminalContador> TTerminalContador { get; set; }
        public virtual ICollection<TRecibosContadorMezclado> TRecibosContadorMezclados { get; set; }
        public virtual ICollection<TRecibosContador> TRecibosContadors { get; set; }
        public virtual ICollection<TRecibosVolumen> TRecibosVolumen { get; set; }
    }
}

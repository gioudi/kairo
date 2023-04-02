using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TArea
    {
        public TArea()
        {
            TTerminales = new HashSet<TTerminal>();
        }

        public string IdArea { get; set; }
        public string Area { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual ICollection<TTerminal> TTerminales { get; set; }
    }
}

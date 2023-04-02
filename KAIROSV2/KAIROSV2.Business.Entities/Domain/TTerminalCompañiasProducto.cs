using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TTerminalCompañiasProducto
    {
        public string IdProducto { get; set; }
        public string IdTerminal { get; set; }
        public string IdCompañia { get; set; }
        public bool ParticipaEnVariaciones { get; set; }
        public string IdCompañíaQueAsume { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TTerminalCompañia Id { get; set; }
        public virtual TCompañia IdCompañíaQueAsumeNavigation { get; set; }
        public virtual TProducto IdProductoNavigation { get; set; }
    }
}

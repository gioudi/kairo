using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAIROSV2.Business.Entities
{
    public partial class TTerminalesProductosReceta
    {
        public string IdTerminal { get; set; }
        public string IdProducto { get; set; }
        public string IdReceta { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TProductosReceta Id { get; set; }
        public virtual TTerminal IdTerminalNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TContadoresEstados
    {
        public TContadoresEstados()
        {
            TTerminalContadores = new HashSet<TTerminalContador>();
        }

        public int IdEstado { get; set; }
        public string Descripcion { get; set; }
        public string ColorHex { get; set; }
        public string Icono { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual ICollection<TTerminalContador> TTerminalContadores { get; set; }
    }
}

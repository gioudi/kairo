using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TTerminalContador
    {
        public string IdContador { get; set; }
        public string IdTerminal { get; set; }
        public string IdProducto { get; set; }
        public int IdTipo { get; set; }
        public int IdEstado { get; set; }
        public string Id_Contador_TAS { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TContador IdContadorNavigation { get; set; }
        public virtual TTerminal IdTerminalNavigation { get; set; }
        public virtual TProducto IdProductoNavigation { get; set; }
        public virtual TContadoresTipo IdTipoNavigation { get; set; }
        public virtual TContadoresEstados IdEstadoNavigation { get; set; }

    }
}

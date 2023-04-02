using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TLinea
    {
        public string IdLinea { get; set; }
        public string IdTerminal { get; set; }
        public string IdProducto { get; set; }
        public int Id_Estado { get; set; }
        public int Capacidad { get; set; }
        public double DensidadAforo { get; set; }
        public string Observaciones { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }
        public int FilaId { get; set; }

        public virtual TProducto IdProductoNavigation { get; set; }
        public virtual TTerminal IdTerminalNavigation { get; set; }
        public virtual TLineasEstado IdEstadoNavigation { get; set; }
    }
}

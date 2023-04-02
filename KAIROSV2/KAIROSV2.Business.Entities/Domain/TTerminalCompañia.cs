using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TTerminalCompañia
    {
        public TTerminalCompañia()
        {
            TTerminalCompañiasProductos = new HashSet<TTerminalCompañiasProducto>();
            TUUsuariosTerminalCompañia = new HashSet<TUUsuariosTerminalCompañia>();
        }

        public string IdTerminal { get; set; }
        public string IdCompañia { get; set; }
        public string Estado { get; set; }
        public string SicomCompañiaTerminal { get; set; }
        public bool Socia { get; set; }
        public string IdCompañiaAgrupadora { get; set; }
        public double PorcentajePropiedad { get; set; }

        public virtual TCompañia IdCompañiaNavigation { get; set; }
        public virtual TTerminal IdTerminalNavigation { get; set; }
        public virtual ICollection<TTerminalCompañiasProducto> TTerminalCompañiasProductos { get; set; }
        public virtual ICollection<TUUsuariosTerminalCompañia> TUUsuariosTerminalCompañia { get; set; }
    }
}

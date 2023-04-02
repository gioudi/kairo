using System;
using System.Collections.Generic;

#nullable disable

namespace KAIROSV2.Business.Entities
{
    public partial class TRecibosBase
    {
        public long IdRecibo { get; set; }
        public string IdTerminal { get; set; }
        public DateTime FechaRecibo { get; set; }
        public string DocumentoRecibo { get; set; }
        public int IdTipo { get; set; }
        public string IdTanque { get; set; }
        public string IdProducto { get; set; }
        public bool DatosTransporte { get; set; }
        public string EditadoPor { get; set; }
        public DateTime UltimaEdicion { get; set; }

        public virtual TProducto IdProductoNavigation { get; set; }
        public virtual TTanque IdT { get; set; }
        public virtual TTerminal IdTerminalNavigation { get; set; }
        public virtual TRecibosTipo IdTipoNavigation { get; set; }
        public virtual TRecibosContador TRecibosContador { get; set; }
        public virtual TRecibosFacturacion TRecibosFacturacion { get; set; }
        public virtual TRecibosMedidaTanque TRecibosMedidaTanque { get; set; }
        public virtual TRecibosTransporte TRecibosTransporte { get; set; }
        public virtual TRecibosVolumen TRecibosVolumen { get; set; }
    }
}
